Partial Class SectionMast
    Inherits System.Web.UI.Page
    Dim DtTemp As DataTable
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If

        'Comment by Ravi
        'Dim SrNo As String
        'SrNo = Request.QueryString.Item("SrNo")
        'If CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules <> "" Then
        '    If InStr(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, SrNo & "-") <> 0 Then
        '        Dim int As Int16, st As String
        '        int = InStr(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, SrNo)
        '        st = Right(Mid(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, int, Len(SrNo & "-") + 1), 1)

        '        If st <> "S" Then
        '            cmdSave.Visible = False
        '            btnNew.Visible = False
        '        End If
        '    Else
        '        Response.Redirect("Main.aspx")
        '    End If
        'End If

        If Not IsPostBack() Then
            Try
                'By Ravi 17 nov 2006
                Dim SrNo As Int16
                SrNo = Request.QueryString.Item("SrNo") + Request.QueryString.Item("MODULEID")
                If Not IsPostBack Then
                    Dim bSuccess As Boolean
                    Select Case CheckRight(SrNo)
                        Case MdlHRMS.AccessType.FullAccess, MdlHRMS.AccessType.SuperUser
                            bSuccess = True
                        Case MdlHRMS.AccessType.ReadonlyAccess, MdlHRMS.AccessType.Restricted
                            bSuccess = False
                    End Select
                    cmdSave.Visible = bSuccess
                End If
                '---------------------------------------------
                Session("Balobj").FillCombo(cmbSection, " Select Sect_Code, Sect_Name From SectMast", True)
                FillComboBoxes()
                btnNew_Click(sender, Nothing)
            Catch ex As Exception
                SetMsg(lblMsg, ex.Message & " : On Load()")
            End Try
        End If

    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNew.Click
        ClearAll(Me)
        lblMsg.Text = ""
        TxtCode.Text = Session("BalObj").GetNextNumber("SECTMAST", "SECT_CODE")
        TxtCode.Text = TxtCode.Text.PadLeft(3, "0")
        DisplayRecords(Chk(TxtCode.Text))
        ViewState("Action") = "ADDNEW"
    End Sub
    Private Sub btnList_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnList.Click
        lblMsg.Text = ""
        cmbSection.Visible = True
        TxtCode.Visible = False
        btnList.Visible = False
        btnNew.Visible = False
    End Sub

    '----------------------Display Records---------------------'
    Private Sub cmbSection_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSection.SelectedIndexChanged
        Try
            cmbSection.Visible = False
            TxtCode.Visible = True
            btnList.Visible = True
            btnNew.Visible = True
            TxtCode.Text = Chk(cmbSection.SelectedValue)
            DisplayRecords(TxtCode.Text)
            Viewstate("Action") = "MODIFY"
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : Code Changed()")
        End Try
    End Sub
    Sub DisplayRecords(ByVal Code As String)
        Try
            DtTemp = New DataTable
            Session("DalObj").GetSqlDataTable(DtTemp, " Select * from SectMast Where Sect_Code='" & Code & "'")
            If DtTemp.Rows.Count = 0 Then Exit Sub
            TxtCode.Text = Chk(DtTemp.Rows(0).Item("Sect_Code"))
            TxtName.Text = Chk(DtTemp.Rows(0).Item("Sect_Name"))
            TxtCC.Text = Chk(DtTemp.Rows(0).Item("MailCC"))
            TxtDEmailId.Text = Chk(DtTemp.Rows(0).Item("DefEmailId"))
            TxtTo.Text = Chk(DtTemp.Rows(0).Item("EmailId"))
            'CmbUId.SelectedValue = Chk(DtTemp.Rows(0).Item("AdminUserId"))
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : DisplayRecords()")
        End Try
    End Sub
    Sub FillComboBoxes()
        Try
            Dim Strs As String, dt As New DataTable, i As Int16, LItem As ListItem
            dt = New DataTable
            Strs = "Select UserID, UserName, Type from WebUsers Where type = 'U' Order by USERNAME "
            Session("DalObj").GetSqlDataTable(dt, Strs)
            CmbUId.Items.Clear()
            CmbUId.Items.Insert(0, "")
            For i = 0 To dt.Rows.Count - 1
                dt.Rows(i).Item("UserName") = Decrypt(dt.Rows(i).Item("UserName"))
                dt.Rows(i).Item("UserID") = Decrypt(dt.Rows(i).Item("UserID"))
                dt.AcceptChanges()
            Next
            Dim DView As New DataView(dt)
            DView.Sort = " UserName ASC"
            CmbUId.DataValueField = "UserID"
            CmbUId.DataTextField = "UserName"
            CmbUId.DataSource = DView
            CmbUId.DataBind()
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : " & "Fill ComboBoxes")
        End Try
    End Sub
    Private Sub cmdsave_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            If Not IsValidate() Then Exit Sub
            Dim strqry As String
            If ViewState("Action") = "ADDNEW" Then
                strqry = " insert SectMast(Sect_Code, Sect_Name,DefEmailID, Emailid, MailCC,AdminUserId) values ('" & _
                        Chk(TxtCode.Text) & "', '" & _
                        Chk(TxtName.Text) & "', '" & _
                      Chk(TxtDEmailId.Text) & "', '" & _
                     Chk(TxtTo.Text) & "', '" & _
                    Chk(TxtCC.Text) & "', '" & _
                   Chk(CmbUId.SelectedItem.Value) & "' )"
                Session("dalobj").executecommand(strqry)
            Else
                strqry = " Update SectMast set Sect_Code='" & Chk(TxtCode.Text) & "', " & _
                         " Sect_Name='" & Chk(TxtName.Text) & "', " & _
                         " DefEmailId='" & Chk(TxtDEmailId.Text) & "' , " & _
                         " EMailID='" & Chk(TxtTo.Text) & "', " & _
                         " MailCC='" & Chk(TxtCC.Text) & "'," & _
                         " AdminUserID = '" & Chk(CmbUId.SelectedItem.Value) & "' where sect_code = '" & Chk(TxtCode.Text) & "'"
                Session("dalobj").executecommand(strqry)
            End If
            btnNew_Click(sender, Nothing)
            Session("balobj").fillcombo(cmbSection, " Select Sect_Code, Sect_Name from SectMast", True)
            SetMsg(lblMsg, "records saved successfully.")
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : save records()")
        End Try
    End Sub

    Function IsValidate() As Boolean
        Try
            If Chk(TxtCode.Text) = "" Then
                SetMsg(lblMsg, " Code can not be left blank.")
                Return False
            End If
            If Chk(TxtName.Text) = "" Then
                SetMsg(lblMsg, " Section Name can not be left blank.")
                Return False
            End If
            If Viewstate("Action") <> "MODIFY" Then
                Dim Count As Int16
                Count = Session("DalObj").ExecuteCommand(" Select Count(*) From SECTMAST Where SECT_CODE='" & Chk(TxtCode.Text) & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
                If Count <> 0 Then
                    SetMsg(lblMsg, " This Code already exist. Record Not Saved.")
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : Validate Records()")
            Return False
        End Try
    End Function
    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub
    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub
End Class
