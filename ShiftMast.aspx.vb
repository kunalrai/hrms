Partial Class ShiftMast
    Inherits System.Web.UI.Page

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

    Dim DtTemp As DataTable

#Region " On Load "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If
        If Not IsPostBack() Then
            Try
                Session("Balobj").FillCombo(cmbShift, " Select Shift_Code, Shift_Name From ShiftMast", True)
                TxtLpTo.Text = "00:00"
                TxtLpFrom.Text = "00:00"
                TxtSpTo.Text = "00:00"
                TxtSpFrom.Text = "00:00"
                btnNew_Click(sender, Nothing)
            Catch ex As Exception
                SetMsg(lblMsg, ex.Message & " : On Load()")
            End Try
        End If
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNew.Click
        ClearAll(Me)
        lblMsg.Text = ""
        TxtCode.Text = Session("BalObj").GetNextNumber("SHIFTMAST", "SHIFT_CODE")
        TxtCode.Text = TxtCode.Text.PadLeft(2, "0")
        DisplayRecords(Chk(TxtCode.Text))
        ViewState("Action") = "ADDNEW"
    End Sub

#End Region

    Private Sub btnList_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnList.Click
        lblMsg.Text = ""
        cmbShift.Visible = True
        TxtCode.Visible = False
        btnList.Visible = False
        btnNew.Visible = False
    End Sub

#Region " Display Records  "

    Private Sub cmbShift_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbShift.SelectedIndexChanged
        Try
            cmbShift.Visible = False
            TxtCode.Visible = True
            btnList.Visible = True
            btnNew.Visible = True
            TxtCode.Text = Chk(cmbShift.SelectedValue)
            DisplayRecords(TxtCode.Text)
            Viewstate("Action") = "MODIFY"
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : Code Changed()")
        End Try
    End Sub

    Sub DisplayRecords(ByVal Code As String)
        Try
            DtTemp = New DataTable
            Session("DalObj").GetSqlDataTable(DtTemp, " Select * from ShiftMast Where Shift_Code='" & Code & "'")

            If DtTemp.Rows.Count = 0 Then Exit Sub
            TxtCode.Text = Chk(DtTemp.Rows(0).Item("Shift_Code"))
            TxtDesc.Text = Chk(DtTemp.Rows(0).Item("Shift_Name"))
            TxtSpFrom.Text = Chk(DtTemp.Rows(0).Item("Shift_From"))
            TxtSpTo.Text = Chk(DtTemp.Rows(0).Item("Shift_To"))
            TxtLpFrom.Text = Chk(DtTemp.Rows(0).Item("Lunch_From"))
            TxtLpTo.Text = Chk(DtTemp.Rows(0).Item("Lunch_To"))

        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : DisplayRecords()")
        End Try
    End Sub

#End Region

#Region " Save Records   "

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            If Not IsValidate() Then Exit Sub

            Dim StrQry As String

            If Viewstate("Action") = "ADDNEW" Then

                StrQry = " Insert ShiftMast(Shift_Code, Shift_Name,Shift_From,Shift_To,Lunch_From,Lunch_To) Values ('" & _
                        Chk(TxtCode.Text) & "', '" & _
                        Chk(TxtDesc.Text) & "', '" & _
                        Chk(TxtSpFrom.Text) & "', '" & _
                        Chk(TxtSpTo.Text) & "', '" & _
                        Chk(TxtLpFrom.Text) & "', '" & _
                        Chk(TxtLpTo.Text) & "' )"

                Session("DalObj").ExecuteCommand(StrQry)


            Else

                StrQry = " Update ShiftMast Set Shift_Code='" & Chk(TxtCode.Text) & "', " & _
                         " Shift_Name='" & Chk(TxtDesc.Text) & "', " & _
                         " Shift_From='" & Chk(TxtSpFrom.Text) & "' , " & _
                         " Shift_To='" & Chk(TxtSpTo.Text) & "', " & _
                         " Lunch_From='" & Chk(TxtLpFrom.Text) & "'," & _
                         " Lunch_To = '" & Chk(TxtLpTo.Text) & "' Where Shift_Code = '" & Chk(TxtCode.Text) & "'"

                Session("DalObj").ExecuteCommand(StrQry)

            End If

            btnNew_Click(sender, Nothing)
            Session("Balobj").FillCombo(cmbShift, " Select Shift_Code, Shift_Name From ShiftMast", True)
            SetMsg(lblMsg, "Records Saved Successfully.")
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : Save Records()")
        End Try
    End Sub

    Function IsValidate() As Boolean
        Try
            If Chk(TxtCode.Text) = "" Then
                SetMsg(lblMsg, " Code can not be left blank.")
                Return False
            End If

            If Chk(TxtDesc.Text) = "" Then
                SetMsg(lblMsg, " Description can not be left blank.")
                Return False
            End If

            If Viewstate("Action") <> "MODIFY" Then
                Dim Count As Int16
                Count = Session("DalObj").ExecuteCommand(" Select Count(*) From SHIFTMAST Where SHIFT_CODE='" & Chk(TxtCode.Text) & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
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

#End Region

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub

End Class
