Partial Class ResumeStatus
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ReqFieldVal As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ChkPDOJ As System.Web.UI.HtmlControls.HtmlInputCheckBox
    Protected WithEvents dtpPDOJ As DITWebLibrary.DTPCombo

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
        If Not (IsPostBack) Then
            FillCombo()
            BindGrdStatus("None")
            ChkOfferDate.Checked = False
            ChkRDOJ.Checked = False
        End If
    End Sub
    Private Sub FillCombo()
        Try
            Session("BalObj").FillCombo(cmbCrntStatus, "Select status_Code,status_name from statusMast", True)
            Session("BalObj").FillCombo(cmbCngStatus, "Select status_Code,status_name from statusMast", True)
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (FillCombo)")
        End Try
    End Sub

    Private Sub BindGrdStatus(ByVal Code As String)
        Try
            ViewState("dtstatus") = New DataTable
            Dim StrSql As String
            StrSql = "select Res_No,RM.Res_Code ,(isnull(Res_NameF,'')+' '+isnull(Res_NameM,'')+' '+isnull(Res_NameL,'')) as EName, DPM.Dept_Name, DSM.Dsg_Name, RM.SalExpect, sum(RE.EXP_Years)as TotalExp From ResMast RM Inner Join DSGMAST DSM On RM.DSG_CODE = DSM.DSG_CODE Inner Join DEPTMAST DPM On RM.DEPT_CODE = DPM.DEPT_CODE inner join RESEXP RE on RE.RES_CODE=RM.RES_CODE  Where RM.Status_Code='" & Chk(Code) & "' group by Res_No,RM.Res_Code ,DPM.Dept_Name, DSM.Dsg_Name, RM.SalExpect, Res_NameF,Res_NameM,Res_NameL"
            Session("DalObj").GetSqlDataTable(ViewState("dtstatus"), StrSql)
            grdStatus.DataSource = ViewState("dtstatus")
            grdStatus.DataBind()
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (BindGrdStatus)")
        Finally
        End Try
    End Sub

    Private Sub cmbCrntStatus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCrntStatus.SelectedIndexChanged
        If cmbCrntStatus.SelectedValue = "" Then Exit Sub
        LblErrMsg.Text = ""
        BindGrdStatus(Chk(cmbCrntStatus.SelectedValue))
    End Sub

    Private Sub btnStatusClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStatusClose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Private Sub btnStatusSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStatusSave.Click
        Dim Tran As SqlClient.SqlTransaction
        Try
            If Not isValidate() Then Exit Sub
            Dim strSQl As String, i As Int16
            Tran = Session("DalObj").StartTransaction("Save")

            For i = 0 To grdStatus.Items.Count - 1

                If CType(grdStatus.Items(i).Controls(0).Controls(1), CheckBox).Checked = True Then
                    If ChkOfferDate.Checked = True Then
                        strSQl = " Update ResMast Set Status_Code = " & Chk(cmbCngStatus.SelectedValue) & ", OfferDate= " & "'" & dtpOfferDate.DateValue & "'" & " Where res_Code = '" & ChkN(grdStatus.Items(i).Cells(1).Text) & "'"
                    Else
                        strSQl = " Update ResMast Set Status_Code = " & Chk(cmbCngStatus.SelectedValue) & " Where res_Code = '" & ChkN(grdStatus.Items(i).Cells(1).Text) & "'"
                    End If
                    'strSQl = " Update ResMast Set Status_Code = " & Chk(cmbCngStatus.SelectedValue) & ", OfferDate= " & IIf(ChkPDOJ.Checked, "'" & dtpPDOJ.DateValue & "' ", "Null") & " Where res_Code = '" & ChkN(grdStatus.Items(i).Cells(1).Text) & "'"
                    Session("DalObj").ExecuteCommand(strSQl, Tran)

                    If ChkRDOJ.Checked = True Then
                        strSQl = " Update ResMast Set Status_Code = " & Chk(cmbCngStatus.SelectedValue) & ", ReadyToJoinDate= " & "'" & dtpRDOJ.DateValue & "'" & " Where res_Code = '" & ChkN(grdStatus.Items(i).Cells(1).Text) & "'"
                    Else
                        strSQl = " Update ResMast Set Status_Code = " & Chk(cmbCngStatus.SelectedValue) & " Where res_Code = '" & ChkN(grdStatus.Items(i).Cells(1).Text) & "'"
                    End If
                    Session("DalObj").ExecuteCommand(strSQl, Tran)

                    strSQl = " Insert ResStatusLog ( Res_Code,Status_Code,Status_ChangeDate,User_Code ) Values (" & _
                             ChkN(grdStatus.Items(i).Cells(1).Text) & ", '" & _
                             Chk(cmbCngStatus.SelectedValue) & "', '" & _
                             Format(Date.Today, "dd-MMM-yyyy") & "', '" & _
                             Chk(Session("LoginUser").UserId) & "' )"
                    Session("DalObj").ExecuteCommand(strSQl, Tran)
                End If
            Next
            Tran.Commit()
            ClearAll(Me)
            BindGrdStatus("ds")
            SetMsg(LblErrMsg, "Record Saved Successfully.")
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (btnstatusSave_Click)")
            Tran.Rollback()
        Finally
            If Not IsNothing(Tran) Then Tran.Dispose()
        End Try
    End Sub
    Private Function isValidate() As Boolean
        Try
            If cmbCrntStatus.SelectedValue = "" Then
                SetMsg(LblErrMsg, "Please Select The Current Status From The List.")
                Return False
            End If
            If cmbCngStatus.SelectedValue = "" Then
                SetMsg(LblErrMsg, "Please Select The Change Status From The List.")
                Return False
            End If
            Return True
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : isValidate()")
            isValidate = False
        End Try
    End Function

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub
End Class

