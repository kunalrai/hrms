Partial Class LocMast
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim oDAL As DAL.DataLayer
    Dim oBAL As BAL.BLayer
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

#Region "     On Load    "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If
        oBAL = Session("BalObj")
        oDAL = Session("DalObj")
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

        '        End If
        '    Else
        '        Response.Redirect("Main.aspx")
        '    End If
        'End If
        If Not IsPostBack Then
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
                Session("BalObj").FillCombo(cmbCode, "Select LOC_CODE, LOC_NAME from LOCMAST ORDER BY LOC_NAME", True)
                Session("BalObj").FillCombo(cmbState, "Select STATE_CODE, STATE_NAME from STATEMAST ORDER BY STATE_NAME", True)
                'cmdNew_Click(sender, e) comment by ravi
                btnNew_Click(sender, Nothing)

            Catch ex As Exception
                SetMsg(LblErrMsg, ex.Message & " : On Load()")
            End Try
        End If
    End Sub

    'comment by Ravi

    'Private Sub cmdNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        LblErrMsg.Text = ""
    '        ClearAll(Me)
    '        TxtCode.Text = Session("BalObj").GetNextNumber("LOCMAST", "LOC_CODE")
    '        TxtCode.Text = TxtCode.Text.PadLeft(4, "0")
    '        DisplayRecords(TxtCode.Text)
    '        ViewState("Action") = "ADDNEW"
    '    Catch ex As Exception
    '        SetMsg(LblErrMsg, ex.Message & " : " & ex.Source)
    '    End Try
    'End Sub

#End Region

#Region "    Save Records    "

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            If Not IsValidate() Then Exit Sub
            Dim SqlStr As String, i As Int16
            If ViewState("Action") = "ADDNEW" Then
                SqlStr = " Insert LOCMAST ( LOC_CODE, LOC_NAME, STATE_CODE,PT_FORMULA ) Values ('" & _
                         Chk(TxtCode.Text) & "', '" & _
                         Chk(TxtDesc.Text) & "', '" & _
                         Chk(cmbState.SelectedValue) & "','" & Chk(txtPTax.Text) & "')"
                Session("DalObj").ExecuteCommand(SqlStr)
            ElseIf ViewState("Action") = "MODIFY" Then
                SqlStr = " Update LOCMAST Set " & _
                         " LOC_NAME = '" & Chk(TxtDesc.Text) & "', " & _
                         " PT_FORMULA = '" & Chk(txtPTax.Text) & "', " & _
                         " STATE_CODE = '" & Chk(cmbState.SelectedValue) & " ' Where LOC_CODE = '" & Chk(TxtCode.Text) & "'"
                Session("DalObj").ExecuteCommand(SqlStr)
            End If
            BlankRecords()
            Session("BalObj").FillCombo(cmbCode, "Select LOC_CODE, LOC_NAME from LOCMAST ORDER BY LOC_NAME", True)
            'cmdNew_Click(sender, e) comment by ravi
            btnNew_Click(sender, e)
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & ex.Source)
        End Try
    End Sub


    Function IsValidate() As Boolean
        Try
            Dim Code As Int16

            If TxtCode.Text = "" Then
                SetMsg(LblErrMsg, " Code can not be left blank, Record Not Saved.")
                Return False
            End If

            If ViewState("Action") = "ADDNEW" Then
                Code = Session("DalObj").Executecommand(" Select Count(*) From LOCMAST Where LOC_CODE = '" & Chk(TxtCode.Text) & "'")
                If Code > 0 Then
                    SetMsg(LblErrMsg, " This Location Code Already exist, Record Not Saved.")
                    Return False
                End If
            End If

            If TxtDesc.Text = "" Then
                SetMsg(LblErrMsg, " Location Name can not be left blank, Record Not Saved.")
                Return False
            End If
            Return True
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Validate Records")
        End Try
    End Function
#End Region



#Region "     Display Records    "

    Private Sub btnList_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnList.Click
        LblErrMsg.Text = ""
        cmbCode.Visible = True
        TxtCode.Visible = False
        btnList.Visible = False
        'cmdNew.Visible = False comment by ravi
    End Sub

    Private Sub cmbCode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCode.SelectedIndexChanged
        Try
            If cmbCode.SelectedValue = "" Then Exit Sub
            TxtCode.Visible = True
            btnList.Visible = True
            cmbCode.Visible = False
            'cmdNew.Visible = True   comment by ravi
            TxtCode.Text = cmbCode.SelectedValue
            DisplayRecords(Chk(TxtCode.Text))
            ViewState("Action") = "MODIFY"
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Code Changed()")
        End Try
    End Sub


    Sub DisplayRecords(ByVal Code As String)
        Try
            BlankRecords()
            Dim i As Int16, SqlStr As String
            Dim DtTemp As New DataTable
            SqlStr = " Select LOC_CODE, LOC_NAME, STATE_CODE,PT_FORMULA from LOCMAST Where LOC_CODE = '" & Code & "'"
            Session("DalObj").GetSqlDataTable(DtTemp, SqlStr)
            If DtTemp.Rows.Count = 0 Then Exit Sub
            TxtDesc.Text = Chk(DtTemp.Rows(0).Item("LOC_NAME"))
            txtPTax.Text = Chk(DtTemp.Rows(0).Item("PT_FORMULA"))
            ChkCombo(cmbState, Chk(DtTemp.Rows(0).Item("STATE_CODE")))
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Display Records")
        End Try
    End Sub

    Sub BlankRecords()
        Try
            TxtDesc.Text = ""
            txtPTax.Text = ""
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Blank Records")
        End Try
    End Sub

#End Region

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub
    Private Sub cmbState_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbState.SelectedIndexChanged
        Dim dtTmp As New DataTable
        Try
            strSql = "SELECT * FROM STATEMAST WHERE State_Code='" & Chk(cmbState.SelectedValue) & "'"
            oDAL.GetSqlDataTable(dtTmp, strSql)
            If Not dtTmp.Rows.Count = 0 Then
                txtPTax.Text = Chk(dtTmp.Rows(0).Item("PT_FORMULA"))
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & ex.Source)
        Finally
            dtTmp.Dispose()
        End Try
    End Sub

    Private Sub TxtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCode.TextChanged
        If Not IsNothing(cmbCode.Items.FindByValue(Chk(TxtCode.Text))) Then
            ChkCombo(cmbCode, Chk(TxtCode.Text))
            cmbCode_SelectedIndexChanged(sender, e)
        End If
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNew.Click
        Try
            LblErrMsg.Text = ""
            ClearAll(Me)
            TxtCode.Text = Session("BalObj").GetNextNumber("LOCMAST", "LOC_CODE")
            TxtCode.Text = TxtCode.Text.PadLeft(4, "0")
            DisplayRecords(TxtCode.Text)
            ViewState("Action") = "ADDNEW"
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & ex.Source)
        End Try
    End Sub
End Class
