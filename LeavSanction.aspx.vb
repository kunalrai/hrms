Imports System.Web.UI.TemplateBuilder
Partial Class LeavSanction
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
    Dim StrSql, sStr As String
    Dim BalObj As BAL.BLayer
    Public DtPendingLV As New DataTable

#Region "   On Load     "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If
        If Not IsPostBack Then
            Try
                'By Ravi 21 Nov

                Dim SrNo As Int16
                SrNo = Request.QueryString.Item("SrNo") + Request.QueryString.Item("MODULEID")
                Dim bSuccess As Boolean
                Select Case CheckRight(SrNo)
                    Case MdlHRMS.AccessType.FullAccess, MdlHRMS.AccessType.SuperUser
                        bSuccess = True
                    Case MdlHRMS.AccessType.ReadonlyAccess, MdlHRMS.AccessType.Restricted
                        bSuccess = False
                End Select
                cmdSave.Visible = bSuccess

                '------------------------------------
                cmbStatus.Items.Add(New ListItem("Santioned", "S"))
                cmbStatus.Items.Add(New ListItem("Rejected", "R"))
                cmbStatus.Items.Add(New ListItem("UnProcessed", "A"))
                cmbStatus.Items.Add(New ListItem("All", "B"))
                cmbStatus.SelectedIndex = cmbStatus.Items.Count - 1

                'StrSql = "Select Emp_Code, Emp_Name from hrdmastqry where Mngr_Code='" & Session("LoginUser").UserID & "' and Ltype=1"
                StrSql = "Select Emp_Code, Emp_Name from hrdmastqry where Ltype=1 and EMP_CODE in ( Select Distinct Emp_Code From LeavApp Where ProcessAuth = '" & Session("LoginUser").UserId & "' ) order by  Emp_Name "
                Session("BalObj").FillCombo(cmbJuniors, StrSql)
                cmbJuniors.Items.Add("")
                cmbJuniors.SelectedIndex = cmbJuniors.Items.Count - 1

                dtpFromDate.Text = Format(Date.Today, "dd/MMM/yyyy")
                dtpToDate.Text = Format(Date.Today, "dd/MMM/yyyy")
            Catch ex As Exception
                SetMsg(lblMsg, ex.Message & "  " & ex.Source)
            Finally

            End Try
        End If
    End Sub

#End Region

#Region "    Fill Leave Applicatiion Grid    "

    Private Sub cmdShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShow.Click
        Try
            lblMsg.Text = ""
            lblAlready.Text = ""

            '***************************************************Before Change In Sick And casaul leave 
            '''''ViewState("Query") = " Select HM.Emp_Name ,LA.Emp_code, AppDate, (case when left(L.LVTYPE,1) = right(L.LVTYPE,1) then '1' else '0.5' end) as LVDays, AtDate,  " & _
            '''''                     " left(datename(dw,AtDate),3) + ', ' + datename(M,AtDate) + ' ' + datename(D,AtDate) + ', ' + dateName(yy,AtDate) as AAtDate, Status,Case When Status = 'A' Then 0 When Status = 'S' Then 1 Else 2 End As SIndex, L.LVTYPE, L.LVDESC, " & _
            '''''                     " (case when LEFT(LA.lvtype,1) = 'L' OR right(L.LVTYPE,1) = 'L' then '1' else '0' end) AS Advance from leavapp LA " & _
            '''''                     " inner join hrdmastqry HM on LA.Emp_code = HM.emp_code inner join LvHelp L on La.LVTYPE=L.LVTYPE " & _
            '''''                     " where LA.ProcessAuth= '" & Session("LoginUser").UserID & "'"




            ViewState("Query") = " Select HM.Emp_Name ,LA.Emp_code, AppDate, (case when left(L.LVTYPE,1) = right(L.LVTYPE,1) then '1' else '0.5' end) as LVDays, AtDate,  " & _
                                 "left(datename(dw,AtDate),3) + ', ' + datename(M,AtDate) + ' ' + datename(D,AtDate) + ', ' + dateName(yy,AtDate) as AAtDate, Status,Case When Status = 'A' Then 0 When Status = 'S' Then 1 Else 2 End As SIndex, L.LVTYPE, L.LVDESC, " & _
                                 " (case when LEFT(LA.lvtype,1) = 'L' OR right(L.LVTYPE,1) = 'L' then '1'  when LEFT(LA.lvtype,1) = 'C' OR right(L.LVTYPE,1) = 'C' then '1'  when LEFT(LA.lvtype,1) = 'S' OR right(L.LVTYPE,1) = 'S' then '1' else '0' end) AS Advance from leavapp LA " & _
                                 " inner join hrdmastqry HM on LA.Emp_code = HM.emp_code inner join LvHelp L on La.LVTYPE=L.LVTYPE " & _
                                 " where LA.ProcessAuth= '" & Session("LoginUser").UserID & "'"



            If cmbJuniors.SelectedIndex <> cmbJuniors.Items.Count - 1 Then
                ViewState("Query") = ViewState("Query") & " and LA.Emp_code = '" & Chk(cmbJuniors.SelectedValue) & "' "

            End If
            If cmbStatus.SelectedIndex <> cmbStatus.Items.Count - 1 Then
                ViewState("Query") = ViewState("Query") & " and Status = '" & Chk(cmbStatus.SelectedValue) & "' "
            End If

            If cmbFilter.SelectedValue = "A" Then
                ViewState("Query") = ViewState("Query") & " and (AppDate >= '" & Format(CDate(dtpFromDate.Text), "dd/MMM/yyyy") & "'" & " and AppDate <= '" & Format(CDate(dtpToDate.Text), "dd/MMM/yyyy") & "') "
            Else
                ViewState("Query") = ViewState("Query") & " and (AtDate >= '" & Format(CDate(dtpFromDate.Text), "dd/MMM/yyyy") & "'" & " and AtDate <= '" & Format(CDate(dtpToDate.Text), "dd/MMM/yyyy") & "') "
            End If

            ViewState("Query") = ViewState("Query") & " order by appdate Desc"

            grdLeavPending.CurrentPageIndex = 0
            FillGrid(ViewState("Query"))
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " " & ex.Source)
        End Try
    End Sub

    Private Sub FillGrid(ByVal Query As String)
        Try
            Session("DalObj").GetSqlDataTable(DtPendingLV, Query)
            grdLeavPending.DataSource = DtPendingLV
            grdLeavPending.DataBind()
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " " & ex.Source)
        Finally
        End Try
    End Sub

#End Region

#Region "    Save Santioned Records   "

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        'Dim Tran As SqlClient.SqlTransaction
        Try
            Dim i, LDate, Count, IsChk As Int16, Code, Rest, ResultString As String
            Dim Bal As Single, Full As Boolean = True
            Dim Dt As New DataTable, Result, St As Char
            Count = 0

            If grdLeavPending.Items.Count = 0 Then
                SetMsg(lblMsg, "No Application found for this creteria.")
                Exit Sub
            End If

            For i = 0 To grdLeavPending.Items.Count - 1
                If CType(grdLeavPending.Items(i).FindControl("ChkSelect"), CheckBox).Checked Then
                    IsChk = 1
                End If
            Next

            If IsChk = 0 Then
                SetMsg(lblMsg, "Please select application to process action.")
                Exit Sub
            End If

            'Tran = Session("DalObj").StartTransaction("Save")
            For i = 0 To grdLeavPending.Items.Count - 1
                If CType(grdLeavPending.Items(i).FindControl("ChkSelect"), CheckBox).Checked Then
                    Code = Trim(grdLeavPending.Items(i).Cells(2).Text)
                    Rest = " where EMP_CODE='" & Code & "'" & _
                           " and LEVYEAR='" & Session("LeavYear") & "' and " & _
                           " AtDate='" & Format(CDate(grdLeavPending.Items(i).Cells(5).Text), "dd/MMM/yyyy") & "'"

                    If Left(Trim(grdLeavPending.Items(i).Cells(7).Text), 1) <> "P" And Right(Trim(grdLeavPending.Items(i).Cells(7).Text), 1) <> "P" Then
                        Result = Left(Trim(grdLeavPending.Items(i).Cells(7).Text), 1) : Full = True
                    ElseIf Left(Trim(grdLeavPending.Items(i).Cells(7).Text), 1) = "P" Then
                        Result = Right(Trim(grdLeavPending.Items(i).Cells(7).Text), 1) : Full = False
                    Else
                        Result = Left(Trim(grdLeavPending.Items(i).Cells(7).Text), 1) : Full = False
                    End If


                    Dim Cnt As Int16
                    Cnt = ChkN(CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(" Select Count(*) From LvType Where LVTYPE='" & Result & "' And isnull(AddHolidays,0)=1", , DAL.DataLayer.ExecutionType.ExecuteScalar))

                    '=================================If Leave is to be Sanctioned=================================
                    If CType(grdLeavPending.Items(i).Controls(9).Controls(1), DropDownList).SelectedValue = "S" Then

                        sStr = "Select count(*) from LeavTran where Emp_Code='" & Code & "' and LVTYPE='" & grdLeavPending.Items(i).Cells(7).Text & "' and AtDate='" & Format(CDate(grdLeavPending.Items(i).Cells(5).Text), "dd/MMM/yyyy") & "'"
                        Bal = Session("DalObj").ExecuteCommand(sStr, , DAL.DataLayer.ExecutionType.ExecuteScalar)

                        If Bal = 0 Then

                            '===========================Check Whether Leave is Rejected===========================
                            If CDate(Format(CDate(grdLeavPending.Items(i).Cells(5).Text), "dd-MMM-yyyy")) < CDate(Format(Date.Today, "dd-MMM-yyyy")) Then
                                sStr = " Select Status from LeavApp where Emp_Code='" & Code & "' and " & _
                                       " LVTYPE='" & grdLeavPending.Items(i).Cells(7).Text & "' and " & _
                                       " AtDate='" & Format(CDate(grdLeavPending.Items(i).Cells(5).Text), "dd/MMM/yyyy") & "'"
                                St = Session("DalObj").ExecuteCommand(sStr, , DAL.DataLayer.ExecutionType.ExecuteScalar)
                                If St = "R" Then
                                    ResultString = ResultString & "Record No : " & i + 1 & ", Unable to Modify Previous Record. <BR> "
                                    GoTo [Continue]
                                End If
                            End If

                            '================================Check For Available Leave balance====================

                            sStr = "Select ((isnull(Opening,0)+isnull(Earned,0))-isnull(AVAILED,0)) as Bal From LEAVMAST where EMP_CODE ='" & Code & _
                                   "' and LVTYPE='" & Result & "' and LEVYEAR='" & Format(CDate(grdLeavPending.Items(i).Cells(5).Text), "yyyy") & "'"
                            Bal = Session("DalObj").ExecuteCommand(sStr, , DAL.DataLayer.ExecutionType.ExecuteScalar)


                            If Bal = 0 Then
                                sStr = " Update LeavApp Set Status = 'R' " & Rest : Session("DalObj").ExecuteCommand(sStr)
                                SendMail(Session("LoginUser").UserID, Code, 1, "Rejected")
                            Else
                                If Cnt <> 0 Then
                                    sStr = "Select count(*) from Holidays where hdate='" & Format(CDate(grdLeavPending.Items(i).Cells(5).Text), "dd/MMM/yyyy") & "' and Isnull(Type,'') = 'N'"
                                    LDate = Session("DalObj").ExecuteCommand(sStr, , DAL.DataLayer.ExecutionType.ExecuteScalar)
                                    If LDate = 0 Then

                                        '----------------------------Insert Record in LeavTran------------------------------

                                        If CType(grdLeavPending.Items(i).FindControl("ChkIsAdvance"), CheckBox).Enabled And CType(grdLeavPending.Items(i).FindControl("ChkIsAdvance"), CheckBox).Checked Then
                                            sStr = " Insert LeavTran ( LEVYEAR, EMP_CODE, ATDATE, LVDAYS, LVTYPE, LVAPPLIED, AVAILED) Values ( '" & _
                                                    Session("LeavYear") & "', '" & _
                                                    Code & "', '" & Format(CDate(grdLeavPending.Items(i).Cells(5).Text), "dd/MMM/yyyy") & "', '"
                                            If Full = True Then sStr = sStr & "1.0" & "', '" Else sStr = sStr & "0.5" & "', '"

                                            If Left(grdLeavPending.Items(i).Cells(7).Text, 1) = Right(grdLeavPending.Items(i).Cells(7).Text, 1) Then
                                                sStr = sStr & "WW', '"
                                            ElseIf Left(grdLeavPending.Items(i).Cells(7).Text, 1) = "P" Then
                                                sStr = sStr & "PW', '"
                                            Else
                                                sStr = sStr & "WP', '"
                                            End If


                                            '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                                            sStr = sStr & grdLeavPending.Items(i).Cells(3).Text & "', '1.0', '"
                                            sStr = Mid(sStr, 1, Len(sStr.Trim) - 2) & " )"
                                            Session("DalObj").ExecuteCommand(sStr)

                                            '----------------------------------Updating LeavMast--------------------------------
                                            sStr = " Update LeavMast Set Availed ="
                                            If Full = True Then sStr = sStr & "(isnull(Availed,0) + 1)" Else sStr = sStr & "(isnull(Availed,0) + 0.5)"
                                            sStr = sStr & " where Emp_Code = '" & Code & "' and LVTYPE = 'W' and LEVYEAR='" & Session("LeavYear") & "'"
                                            Session("DalObj").ExecuteCommand(sStr)
                                        Else
                                            sStr = " Insert LeavTran ( LEVYEAR, EMP_CODE, ATDATE, LVDAYS, LVTYPE, LVAPPLIED, AVAILED ) Values ( '" & _
                                                    Session("LeavYear") & "', '" & _
                                                    Code & "', '" & Format(CDate(grdLeavPending.Items(i).Cells(5).Text), "dd/MMM/yyyy") & "', '"
                                            If Full = True Then sStr = sStr & "1.0" & "', '" Else sStr = sStr & "0.5" & "', '"
                                            sStr = sStr & grdLeavPending.Items(i).Cells(7).Text & "', '" & _
                                                   grdLeavPending.Items(i).Cells(3).Text & "', '1.0', '"
                                            sStr = Mid(sStr, 1, Len(sStr.Trim) - 2) & " )"
                                            Session("DalObj").ExecuteCommand(sStr)

                                            '----------------------------------Updating LeavMast--------------------------------
                                            sStr = " Update LeavMast Set Availed ="
                                            If Full = True Then sStr = sStr & "(isnull(Availed,0) + 1)" Else sStr = sStr & "(isnull(Availed,0) + 0.5)"
                                            sStr = sStr & " where Emp_Code = '" & Code & "' and LVTYPE = '" & Result & "' and LEVYEAR='" & Session("LeavYear") & "'"
                                            Session("DalObj").ExecuteCommand(sStr)
                                        End If

                                        '----------------------------------Updating LeavApp---------------------------------
                                        sStr = " Update LeavApp Set Status = 'S' " & _
                                               " where EMP_CODE='" & Code & "'" & _
                                               " and LVTYPE='" & grdLeavPending.Items(i).Cells(7).Text & "' and " & _
                                               " AtDate='" & Format(CDate(grdLeavPending.Items(i).Cells(5).Text), "dd/MMM/yyyy") & "'"

                                        Session("DalObj").ExecuteCommand(sStr)
                                        Count = Count + 1
                                    Else
                                        ResultString = ResultString & "Record No : " & i + 1 & ", Weekly Off/Holiday Off. <BR> "
                                    End If

                                Else

                                    '----------------------------Insert Record in LeavTran------------------------------
                                    sStr = " Insert LeavTran ( LEVYEAR, EMP_CODE, ATDATE, LVDAYS, LVTYPE, LVAPPLIED, AVAILED ) Values ( '" & _
                                            Session("LeavYear") & "', '" & _
                                            Code & "', '" & Format(CDate(grdLeavPending.Items(i).Cells(5).Text), "dd/MMM/yyyy") & "', '"
                                    If Full = True Then sStr = sStr & "1.0" & "', '" Else sStr = sStr & "0.5" & "', '"
                                    sStr = sStr & grdLeavPending.Items(i).Cells(7).Text & "', '" & _
                                           grdLeavPending.Items(i).Cells(3).Text & "', '1.0', '"
                                    sStr = Mid(sStr, 1, Len(sStr.Trim) - 2) & " )"
                                    Session("DalObj").ExecuteCommand(sStr)

                                    '----------------------------------Updating LeavMast--------------------------------
                                    sStr = " Update LeavMast Set Availed ="
                                    If Full = True Then sStr = sStr & "(isnull(Availed,0) + 1)" Else sStr = sStr & "(isnull(Availed,0) + 0.5)"
                                    sStr = sStr & " where Emp_Code = '" & Code & "' and LVTYPE = '" & Result & "' and LEVYEAR='" & Session("LeavYear") & "'"
                                    Session("DalObj").ExecuteCommand(sStr)

                                    '----------------------------------Updating LeavApp---------------------------------
                                    sStr = " Update LeavApp Set Status = 'S' " & _
                                           " where EMP_CODE='" & Code & "'" & _
                                           " and LVTYPE='" & grdLeavPending.Items(i).Cells(7).Text & "' and " & _
                                           " AtDate='" & Format(CDate(grdLeavPending.Items(i).Cells(5).Text), "dd/MMM/yyyy") & "'"

                                    Session("DalObj").ExecuteCommand(sStr)
                                    Count = Count + 1

                                End If
                                SendMail(Session("LoginUser").UserID, Code, 1, "Sanctioned")
                            End If
                        Else

                            If Format(CDate(grdLeavPending.Items(i).Cells(5).Text), "dd/MMM/yyyy") < Format(Date.Today, "dd/MMM/yyyy") Then
                                ResultString = ResultString & "Record No : " & i + 1 & ", Unable to Modify Previous Record. <BR>"
                                GoTo [Continue]
                            End If
                            ResultString = ResultString & "Record No : " & i + 1 & ", Application already exist. <BR> "
                        End If
                    Else
                        '=================================If Leave is to be Rejected=================================
                        sStr = " Select Count(*) from LeavTran " & Rest
                        LDate = Session("DalObj").ExecuteCommand(sStr, , DAL.DataLayer.ExecutionType.ExecuteScalar)

                        If LDate > 0 Then

                            If CDate(Format(CDate(grdLeavPending.Items(i).Cells(5).Text), "dd/MMM/yyyy")) < CDate(Format(Date.Today, "dd/MMM/yyyy")) Then
                                '======================if Leave Date is Less than Current Date=======================
                                ResultString = ResultString & "Record No : " & i + 1 & ", Unable to Modify Previous Record. <BR> "
                                GoTo [Continue]
                            End If

                            '----------------------------------Delete LeavTran & Updating LeavMast---------------------------------
                            sStr = "Delete LeavTran " & Rest
                            Session("DalObj").ExecuteCommand(sStr)

                            sStr = " Update LeavMast Set Availed ="
                            If Full = True Then sStr = sStr & "(isnull(Availed,0) - 1)" Else sStr = sStr & "(isnull(Availed,0) - 0.5)"
                            sStr = sStr & " where Emp_Code = '" & Code & "' and LVTYPE = '" & Result & "' and LEVYEAR='" & Session("LeavYear") & "'"
                            Session("DalObj").ExecuteCommand(sStr)
                        Else
                            If CDate(Format(CDate(grdLeavPending.Items(i).Cells(5).Text), "dd-MMM-yyyy")) < CDate(Format(Date.Today, "dd/MMM/yyyy")) Then
                                sStr = "Select Status from LeavApp where Emp_Code='" & Code & "' and LVTYPE='" & grdLeavPending.Items(i).Cells(7).Text & "' and AtDate='" & Format(CDate(grdLeavPending.Items(i).Cells(5).Text), "dd/MMM/yyyy") & "'"
                                St = Session("DalObj").ExecuteCommand(sStr, , DAL.DataLayer.ExecutionType.ExecuteScalar)
                                If St = "R" Then
                                    ResultString = ResultString & "Record No : " & i + 1 & ", Unable to Modify Previous Record. <BR> "
                                    GoTo [Continue]
                                End If
                            End If
                        End If

                        '--------------------------------------------Updating LeavApp----------------------------------------------
                        If CType(grdLeavPending.Items(i).Controls(9).Controls(1), DropDownList).SelectedValue = "R" Then
                            sStr = "Update LeavApp Set Status = 'R' " & _
                                   " where EMP_CODE='" & Code & "'" & _
                                   " and LVTYPE='" & grdLeavPending.Items(i).Cells(7).Text & "' and " & _
                                   " AtDate='" & Format(CDate(grdLeavPending.Items(i).Cells(5).Text), "dd/MMM/yyyy") & "'"
                            SendMail(Session("LoginUser").UserID, Code, 1, "Rejected")
                        Else
                            sStr = "Update LeavApp Set Status = 'A' " & _
                                   " where EMP_CODE='" & Code & "'" & _
                                   " and LVTYPE='" & grdLeavPending.Items(i).Cells(7).Text & "' and " & _
                                   " AtDate='" & Format(CDate(grdLeavPending.Items(i).Cells(5).Text), "dd/MMM/yyyy") & "'"
                        End If
                        Session("DalObj").ExecuteCommand(sStr)
                        Count = Count + 1
                    End If
[Continue]:
                End If
            Next
            'Tran.Commit()

            SetMsg(lblAlready, ResultString)
            'DtPendingLV.Rows.Clear()
            'grdLeavPending.DataBind()
            If Count <> 0 Then
                SetMsg(lblMsg, "Leave Sanctioned : " & Count & " Record Saved Successfully.")
            End If
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " " & ex.Source)
            'Tran.Rollback()
        Finally
            'If Not IsNothing(Tran) Then Tran.Dispose()
        End Try
    End Sub

#End Region

    Private Sub grdLeavPending_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles grdLeavPending.PageIndexChanged
        Try
            If (grdLeavPending.CurrentPageIndex > grdLeavPending.PageCount Or grdLeavPending.CurrentPageIndex < 0) Then
                grdLeavPending.CurrentPageIndex = 0
            Else
                grdLeavPending.CurrentPageIndex = e.NewPageIndex
            End If
            FillGrid(ViewState("Query"))
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " " & ex.Source)
        End Try
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Sub SendMail(ByVal FromID As String, ByVal ToID As String, ByVal Days As Short, ByVal Status As String)
        Try
            Dim Id1, Id2, Subject, Body, Name As String

            Dim CCMailId As String

            Id1 = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(" Select Isnull(EmailId,IsNull(PEmailId,'')) From Hrdmast Where EMP_CODE='" & FromID & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
            Id2 = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(" Select Isnull(EmailId,IsNull(PEmailId,'')) From Hrdmast Where EMP_CODE = '" & ToID & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
            Name = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(" Select IsNull(EMP_NAME,'') From HRDMASTQRY Where EMP_CODE='" & Session("Loginuser").userId & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
            CCMailId = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(" Select IsNull(IODSN,'') From COMPMAST ", , DAL.DataLayer.ExecutionType.ExecuteScalar)

            If Id1 = "" Then
                lblMsg.Text = lblMsg.Text & "<BR>" & " Employee's Email Id is not defined, Unable to send mail."
                Exit Sub
            End If

            If Id2 = "" Then
                lblMsg.Text = lblMsg.Text & "<BR>" & " Employee's Manager Email Id is not defined, Unable to send mail."
                Exit Sub
            End If

            Dim DTSetup As New DataTable
            CType(Session("DalObj"), DAL.DataLayer).GetSqlDataTable(DTSetup, " Select Subj, Message From EmailReminderSetup Where FormKey=8 and Active = 1")

            If DTSetup.Rows.Count <> 0 Then
                Subject = Chk(DTSetup.Rows(0).Item("Subj"))
                Body = Chk(DTSetup.Rows(0).Item("Message"))
                Body = Replace(Body, "@Days", Days)
                Body = Replace(Body, "@STATUS", Status)
            Else
                lblMsg.Text = lblMsg.Text & "<BR>" & " Email Reminder is not defined for Leave Application, Unable to send mail."
                Exit Sub
            End If

            Dim MyMail As New Mail.MailMessage, SmtpServer As String

            SmtpServer = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand("Select isnull(PLACE,'') From COMPMAST", , DAL.DataLayer.ExecutionType.ExecuteScalar)

            MyMail.From = Id1
            MyMail.To = Id2
            MyMail.Cc = Chk(CCMailId)
            MyMail.Subject = Subject
            MyMail.Body = Body
            Mail.SmtpMail.SmtpServer = SmtpServer.Trim.ToString
            Mail.SmtpMail.Send(MyMail)
        Catch ex As Exception
            lblMsg.Text = lblMsg.Text & ex.Message & " SendMail "
        End Try
    End Sub

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub

End Class