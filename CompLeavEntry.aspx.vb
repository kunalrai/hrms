Partial Class CompLeavEntry
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents RdoF As System.Web.UI.WebControls.RadioButton
    Protected WithEvents RdoHalfbutton2 As System.Web.UI.WebControls.RadioButton

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim tran As SqlClient.SqlTransaction
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If

        Dim SrNo As String
        SrNo = Request.QueryString.Item("SrNo")

        If CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules <> "" Then
            If InStr(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, SrNo & "-") <> 0 Then
                Dim int As Int16, st As String
                int = InStr(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, SrNo)
                st = Right(Mid(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, int, Len(SrNo & "-") + 1), 1)
                If st = "S" Then
                Else
                    CmdSave.Visible = False
                End If
            End If
        End If
        If Not IsPostBack Then
            Session("BalObj").fillcombo(cmbCode, "Select Emp_Code, Fname+' '+isnull(lname,'') as FNAME from HrdMast Order by FNAME")
        End If
    End Sub
    Private Sub btnList_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnList.Click
        btnList.Visible = False
        TxtCode.Visible = False
        cmbCode.Visible = True

    End Sub
    Private Sub CmbCode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCode.SelectedIndexChanged
        If cmbCode.SelectedValue = "" Then Exit Sub
        btnList.Visible = True
        TxtCode.Visible = True
        cmbCode.Visible = False
        TxtCode.Text = cmbCode.SelectedValue
        TxtCode_TextChanged(sender, e)
    End Sub


    Private Sub CmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdSave.Click
        Try
            If Not isValidate() Then Exit Sub
            tran = Session("DalObj").StartTransaction("Save")

            Dim sqlstr As String, Code As String
            Code = Session("BalObj").GetNextNumber("COMPLEAVENTRY", "TRANID")
            sqlstr = " insert  COMPLEAVENTRY (TranId, Emp_Code, CompLeavDate, LeavYear, UserId, Remarks, LvType) values(" & _
             Chk(Code) & ", '" & _
             Chk(TxtCode.Text) & "', '" & _
             Format(dtpDOCompLv.DateValue, "dd/MMM/yyyy") & "', '" & _
             Session("LeavYear") & "', '" & _
             Chk(Session("LoginUser").UserId) & "', '" & _
             Chk(TxtRemarks.Text) & "', '"

            If RdoFull.Checked Then
                sqlstr = sqlstr & "1" & "' )"

                Session("DalObj").ExecuteCommand(sqlstr, tran)

                sqlstr = " Update LEAVMAST Set TRAN_IN = isnull(TRAN_IN,0) + 1 Where  " & _
                                          " LEVYEAR = '" & Session("LeavYear") & "' and LVTYPE = 'X' " & _
                                          " and EMP_CODE = '" & Chk(TxtCode.Text) & "'"

                Session("DalObj").ExecuteCommand(sqlstr, tran)
            Else
                sqlstr = sqlstr & "0" & "' )"

                Session("DalObj").ExecuteCommand(sqlstr, tran)

                sqlstr = " Update LEAVMAST Set TRAN_IN = isnull(TRAN_IN,0) + 0.5 Where  " & _
                                          " LEVYEAR = '" & Session("LeavYear") & "' and LVTYPE = 'X' " & _
                                          " and EMP_CODE = '" & Chk(TxtCode.Text) & "'"

                Session("DalObj").ExecuteCommand(sqlstr, tran)
            End If

            tran.Commit()
            ClearAll(Me)
            SetMsg(LblErrMsg, " Record Saved Successfully")
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & ex.Source)
            Tran.Rollback()
        Finally
            If Not IsNothing(Tran) Then Tran.Dispose()
        End Try
    End Sub

    Private Function isValidate() As Boolean
        Try
            Dim Count As Int16

            If Chk(TxtCode.Text) = "" Then
                SetMsg(LblErrMsg, "Please Enter The Code.")
                Return False
            End If

            Count = Session("DalObj").ExecuteCommand("Select Count(*) From LeavMast Where EMP_CODE = '" & Chk(TxtCode.Text) & "' AND LVTYPE='X' AND LEVYEAR = '" & Session("LeavYear") & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)

            If Count = 0 Then
                SetMsg(LblErrMsg, " Compensatory Leave doesn't exist for this employee.")
                Return False
            End If

            Return True
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : isValidate()")
            isValidate = False
        End Try
    End Function

    Private Sub TxtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCode.TextChanged
        Try
            ChkCombo(cmbCode, Chk(TxtCode.Text))
            LblName.Visible = True
            LblName.Text = cmbCode.SelectedItem.Text
            If LblName.Text <> "" Then
                LblName.Visible = True
            Else
                LblName.Visible = False
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : isValidate()")
        End Try
    End Sub

    Private Sub CmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub
End Class

