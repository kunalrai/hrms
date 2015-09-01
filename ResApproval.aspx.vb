Partial Class ResApproval
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents grdRes As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim Dt As DataTable
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
                    ViewState("USER") = "ADMIN"
                End If
            End If
        End If

        If Not IsPostBack Then
            Session("BalObj").FillCombo(CmbDept, "Select Dept_Code, Dept_Name From DeptMast Order By Dept_Name", True)
            Session("BalObj").fillCombo(CmbStsName, "Select Status_Code,Status_Name from StatusMast order by Status_Name")
            CmbStsName.Items.Insert(0, "All")
            CmbStsName.SelectedIndex = 0

            If ViewState("USER") = "ADMIN" Then
                Session("BalObj").FillCombo(CmbReqId, "Select Vacancy_Code , Vacancy_RefNo From Vacancy", True)
            Else
                Dim DeptCode As String
                DeptCode = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand("Select DEPT_CODE From HRDMAST WHERE EMP_CODE='" & Session("LoginUser").UserId & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
                Session("BalObj").FillCombo(CmbReqId, "Select Vacancy_Code , Vacancy_RefNo From Vacancy Where Dept_Code='" & DeptCode & "'", True)
                'COMMENT BY RAVI ON 5 DEC 2006
                ' CmbStsName.SelectedValue = "114"
                CmbDept.Enabled = False
                CmbStsName.Enabled = False
            End If

            BindgrdResume()
        End If
    End Sub

    Private Sub CmbReqId_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbReqId.SelectedIndexChanged
        Dim DEPTCODE As String, Desc As String, dt As New DataTable
        Try
            If CmbReqId.SelectedValue = "" Then
                Exit Sub
            Else
                DEPTCODE = Session("DalObj").ExecuteCommand("Select Dept_Code From Vacancy Where Vacancy_Code = '" & CmbReqId.SelectedValue & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
                ChkCombo(CmbDept, DEPTCODE)
                Desc = Session("DalObj").ExecuteCommand("Select Vacancy_Desc From Vacancy Where Vacancy_Code = '" & CmbReqId.SelectedValue & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
                TxtDesc.Text = Desc

            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & ex.Source)
        End Try
    End Sub
    Private Sub BindgrdResume()
        Try
            Dt = New DataTable
            Dim StrSql As String
            StrSql = "select  RM.Res_Code, RM.Res_No as ResNo, (RM.Res_NameF +' '+ RM.Res_nameM+' '+RM.Res_NameL) as ResName, DM.Dsg_Name as DsgName,DPTM.Dept_Name as DeptName, RM.skills as Skills, RM.Status_Code as Status, RM.SalExpect as SalExpect from ResMast as RM inner join DsgMast as DM on RM.Dsg_code=DM.Dsg_Code inner join DeptMast as DPTM on RM.Dept_Code=DPTM.Dept_Code  where Vacancy_Code = '" & CmbReqId.SelectedValue & "' "
            Session("DalObj").GetSqlDataTable(Dt, StrSql)
            grdResume.DataSource = Dt
            grdResume.DataBind()
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (BindGrdresume)")
        Finally
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim i, Count As Int16, SqlStr As String
            For i = 0 To grdResume.Items.Count - 1
                If CType(grdResume.Items(i).FindControl("chkSelect"), CheckBox).Checked = True Then
                    If ViewState("USER") = "ADMIN" Then
                        SqlStr = " Update ResMast Set Status_code='114' Where Res_Code='" & grdResume.Items(i).Cells(1).Text & "'"
                    Else
                        SqlStr = " Update ResMast Set Status_code='102' Where Res_Code='" & grdResume.Items(i).Cells(1).Text & "'"
                    End If

                    CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(SqlStr)
                    Count = Count + 1
                End If
            Next
            SendMail(Count)
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (Save Records)")
        End Try
    End Sub

    Private Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow.Click
        DisplayRecords()
    End Sub

    Private Sub DisplayRecords()
        Try
            Dim SqlStr As String
            If CmbStsName.SelectedIndex = 0 Then
                SqlStr = "select RM.Res_Code, RM.Res_No as ResNo, (RM.Res_NameF +' '+ RM.Res_nameM+' '+RM.Res_NameL) as ResName, DM.Dsg_Name as DsgName,DPTM.Dept_Name as DeptName, RM.skills as Skills,SM.Status_Name as Status, RM.SalExpect as SalExpect from ResMast as RM inner join DsgMast as DM on RM.Dsg_code=DM.Dsg_Code inner join DeptMast as DPTM on RM.Dept_Code=DPTM.Dept_Code inner join StatusMast as SM on RM.Status_Code=SM.Status_Code where Vacancy_Code = '" & CmbReqId.SelectedValue & "'"
            Else
                SqlStr = "select RM.Res_Code, RM.Res_No as ResNo, (RM.Res_NameF +' '+ RM.Res_nameM+' '+RM.Res_NameL) as ResName, DM.Dsg_Name as DsgName,DPTM.Dept_Name as DeptName, RM.skills as Skills,SM.Status_Name as Status, RM.SalExpect as SalExpect from ResMast as RM inner join DsgMast as DM on RM.Dsg_code=DM.Dsg_Code inner join DeptMast as DPTM on RM.Dept_Code=DPTM.Dept_Code inner join StatusMast as SM on RM.Status_Code=SM.Status_Code where RM.Vacancy_Code = '" & CmbReqId.SelectedValue & "' and RM.Status_Code='" & CmbStsName.SelectedValue & "'"
            End If
            Dt = New DataTable
            Session("DalObj").GetSqlDataTable(Dt, SqlStr)
            grdResume.DataSource = Dt
            grdResume.DataBind()
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Display Records")
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Sub SendMail(ByVal Count As Int16)
        Dim Id1, Subject, Body, HeadEmailId As String

        Id1 = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(" Select Isnull(EmailId,IsNull(PEmailId,'')) From Hrdmast Where EMP_CODE='" & Session("Loginuser").userId & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
        HeadEmailId = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(" Select Isnull(HeadEmailId,'') From DeptMast Where Dept_Code='" & CmbDept.SelectedValue & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)

        If HeadEmailId = "" Then
            LblErrMsg.Text = LblErrMsg.Text & "<BR>" & " Email Id is not defined selected department head, Unable to send mail."
            Exit Sub
        End If

        Dim DTSetup As New DataTable
        If ViewState("USER") = "ADMIN" Then
            CType(Session("DalObj"), DAL.DataLayer).GetSqlDataTable(DTSetup, " Select Subj, Message, MailCC From EmailReminderSetup Where FormKey=11 and Active = 1")
        Else
            CType(Session("DalObj"), DAL.DataLayer).GetSqlDataTable(DTSetup, " Select Subj, Message, MailCC From EmailReminderSetup Where FormKey=12 and Active = 1")
        End If


        If DTSetup.Rows.Count <> 0 Then
            Subject = Chk(DTSetup.Rows(0).Item("Subj"))
            Subject = Replace(Subject, "@JOBID", CmbReqId.SelectedItem.Text)
            Body = Chk(DTSetup.Rows(0).Item("Message"))
            Body = Replace(Body, "@COUNT", Count)
            Body = Replace(Body, "@USER", Session("LoginUser").UserName)
            Body = Replace(Body, "@JOBID", CmbReqId.SelectedItem.Text)
        Else
            LblErrMsg.Text = LblErrMsg.Text & "<BR>" & " Email Reminder is not defined for Resume Shortlisting, Unable to send mail."
            Exit Sub
        End If

        Dim MyMail As New Mail.MailMessage, SmtpServer As String
        SmtpServer = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand("Select isnull(PLACE,'') From COMPMAST", , DAL.DataLayer.ExecutionType.ExecuteScalar)


        If ViewState("USER") = "ADMIN" Then
            MyMail.To = HeadEmailId
            If Id1 <> "" Then
                MyMail.From = Id1
            Else
                MyMail.From = Chk(DTSetup.Rows(0).Item("MailCC"))
            End If
        Else
            If Id1 <> "" Then
                MyMail.From = Id1
            Else
                MyMail.From = HeadEmailId
            End If
            MyMail.To = Chk(DTSetup.Rows(0).Item("MailCC"))
        End If

        MyMail.Subject = Subject
        MyMail.Body = Body
        Mail.SmtpMail.SmtpServer = SmtpServer.Trim.ToString
        Mail.SmtpMail.Send(MyMail)
    End Sub

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub

End Class
