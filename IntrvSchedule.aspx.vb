Partial Class IntrvSchedule
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

#Region "  On Load   "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If
        If Not IsPostBack() Then
            FillCombo()
            btnNew_Click(sender, Nothing)
            dtpInterview.Text = Format(Date.Today, "dd/MMM/yyyy")
        End If
    End Sub

    Private Sub FillCombo()
        Try
            Session("BalObj").FillCombo(cmbIntrvrefid, "Select IntrvID, IntrvNo from intrvshedule", True)
            Session("BalObj").FillCombo(cmbIntrvName, "Select Emp_Code, Fname+' '+isnull(lname,'') as NAME from HrdMast Order by FNAME", True)
            Session("BalObj").FillCombo(cmbReqNo, "Select Vacancy_Code, Vacancy_RefNo from Vacancy", True)
            Session("BalObj").FillCombo(cmbType, "Select TTMId, TTMDesc from TempTypeMast")
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (FillCombo)")
        End Try
    End Sub

#End Region

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNew.Click
        Try
            ClearAll(Me)
            TxtIntrvrefno.ToolTip = Session("BalObj").GetNextNumber("IntrvShedule", "IntrvID")
            TxtIntrvrefno.Text = TxtIntrvrefno.ToolTip.PadLeft(4, "0")
            DisplayRecords(TxtIntrvrefno.ToolTip)
            BindgrdIntrv(TxtIntrvrefno.ToolTip)
            dtpInterview.Text = Format(Date.Today, "dd/MMM/yyyy")
            ViewState("Action") = "ADDNEW"
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (btnNew_Click)")
        End Try
    End Sub

    Private Sub btnList_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnList.Click
        LblErrMsg.Text = ""
        cmbIntrvrefid.Visible = True
        TxtIntrvrefno.Visible = False
        btnList.Visible = False
        btnNew.Visible = False
    End Sub

    Private Sub cmbIntrvrefid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbIntrvrefid.SelectedIndexChanged
        Try
            If cmbIntrvrefid.SelectedIndex <> cmbIntrvrefid.Items.Count - 1 Then
                TxtIntrvrefno.Visible = True
                btnList.Visible = True
                btnNew.Visible = True
                cmbIntrvrefid.Visible = False
                TxtIntrvrefno.ToolTip = cmbIntrvrefid.SelectedValue
                TxtIntrvrefno.Text = cmbIntrvrefid.SelectedItem.Text
                ViewState("Action") = "MODIFY"
                DisplayRecords(TxtIntrvrefno.ToolTip)
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message)
        End Try
    End Sub

#Region "  Display Records  "

    Private Sub DisplayRecords(ByVal Code As String)
        Try
            If Code = "" Then Exit Sub
            Dim i As Int16
            Dim Dv As DataView
            Dim StrSql As String

            DtTemp = New DataTable
            StrSql = " Select IntrvNo, Intrviewer_Code,IntrvDate ,Vacancy_Code, Venue, Address1 ,VenueEmail,VenueContactNo, CommonNote, Vacancy_Code,TTMID  from IntrvShedule  Where IntrvID = '" & Code & "'"
            Session("DalObj").GetSqlDataTable(DtTemp, StrSql)
            If DtTemp.Rows.Count = 0 Then Exit Sub
            TxtIntrvrefno.Text = Chk(DtTemp.Rows(0).Item("IntrvNo"))
            TxtContectNo.Text = Chk(DtTemp.Rows(0).Item("VenueContactNo"))
            TxtCommonNote.Text = Chk(DtTemp.Rows(0).Item("CommonNote"))
            TxtAddress.Text = Chk(DtTemp.Rows(0).Item("Address1"))
            TxtEMail.Text = Chk(DtTemp.Rows(0).Item("VenueEmail"))
            TxtVenue.Text = Chk(DtTemp.Rows(0).Item("Venue"))
            ChkCombo(cmbIntrvName, Chk(DtTemp.Rows(0).Item("Intrviewer_Code")))
            cmbType.SelectedValue = DtTemp.Rows(0).Item("TTMID")

            If Not IsDBNull(DtTemp.Rows(0).Item("IntrvDate")) Then
                dtpInterview.Text = Format(CDate(DtTemp.Rows(0).Item("IntrvDate")), "dd/MMM/yyyy")
            End If

            BindgrdIntrv(Code)

            For i = 0 To grdIntrv.Items.Count - 1
                CType(grdIntrv.Items(i).Controls(0).Controls(1), CheckBox).Checked = True
            Next
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (Display Records)")
        End Try
    End Sub


    Private Sub BindgrdIntrv(ByVal Code As String)
        Try
            ViewState("Candidates") = New DataTable
            Dim StrSql As String
            StrSql = " Select  RM.Res_Code,RM.Res_No, (isnull(Res_NameF,'')+' '+isnull(Res_NameM,'')+' '+isnull(Res_NameL,'')) as EName,RM.Skills, " & _
                     " (right('00'+cast(datepart(hh,Time_From) as varchar),2)+':'+right('00'+cast(datepart(mi,Time_From) as varchar),2)) as Time_From, " & _
                     " (right('00'+cast(datepart(hh,Time_To) as varchar),2)+':'+right('00'+cast(datepart(mi,Time_To) as varchar),2)) as Time_To , " & _
                     " IC.Vacancy_Code,V.Vacancy_RefNo, Note from IntrvCandidates IC " & _
                     " Right Join ResMast RM On IC.Res_Code = RM.Res_Code " & _
                     " Inner Join vacancy V On IC.Vacancy_Code = V.Vacancy_Code Where IntrvID= " & Code

            Session("DalObj").GetSqlDataTable(ViewState("Candidates"), StrSql)
            grdIntrv.DataSource = ViewState("Candidates")
            grdIntrv.DataBind()
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (BindGrdIntrv)")
        End Try
    End Sub

#End Region

    Private Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click, cmdSMail.Click
        Dim Tran As SqlClient.SqlTransaction

        Try
            If Not isValidate() Then Exit Sub
            Dim strSQl As String, i As Int16, FromTime, ToTime As Date
            Tran = Session("DalObj").StartTransaction("Save")
            If ViewState("Action") = "ADDNEW" Then
                strSQl = " Insert InTo intrvshedule (IntrvID, IntrvNo,IntrvDate, Intrviewer_Code, Venue, Address1, VenueEmail, VenueContactNo, CommonNote, TTMId) Values ('" & _
                                 Chk(TxtIntrvrefno.ToolTip) & "', '" & _
                                 Chk(TxtIntrvrefno.Text) & "', '" & _
                                 Chk(dtpInterview.Text) & "', '" & _
                                 Chk(cmbIntrvName.SelectedValue) & "', '" & _
                                 Chk(TxtVenue.Text) & "', '" & _
                                 Chk(TxtAddress.Text) & "', '" & _
                                 Chk(TxtEMail.Text) & "', '" & _
                                 Chk(TxtContectNo.Text) & "', '" & _
                                 Chk(TxtCommonNote.Text) & "', " & _
                                 ChkN(cmbType.SelectedValue) & ")"
                Session("DalObj").ExecuteCommand(strSQl, Tran)

                For i = 0 To grdIntrv.Items.Count - 1
                    If CType(grdIntrv.Items(i).Controls(0).Controls(1), CheckBox).Checked = True Then
                        FromTime = Format(CDate(Chk(CType(grdIntrv.Items(i).Controls(6).Controls(1), TextBox).Text)), "hh:mm")
                        ToTime = Format(CDate(Chk(CType(grdIntrv.Items(i).Controls(7).Controls(1), TextBox).Text)), "hh:mm")
                        strSQl = " Insert IntrvCandidates(IntrvId,Vacancy_Code,TTMId,Res_Code,Time_From,Time_To,Note) Values(" & _
                                 Chk(TxtIntrvrefno.Text) & ", " & _
                                 ChkN(grdIntrv.Items(i).Cells(3).Text) & ", " & _
                                 ChkN(cmbType.SelectedValue) & " , " & _
                                 ChkN(grdIntrv.Items(i).Cells(1).Text) & ", '" & _
                                 FromTime & "' , '" & _
                                 ToTime & "' , '" & _
                                 Chk(CType(grdIntrv.Items(i).Controls(8).Controls(1), TextBox).Text) & "')"
                        Session("DalObj").ExecuteCommand(strSQl, Tran)
                    End If
                Next

            ElseIf ViewState("Action") = "MODIFY" Then
                strSQl = " Update IntrvShedule set " & _
                         " Intrviewer_Code = " & Chk(cmbIntrvName.SelectedValue) & ", " & _
                         " Venue = '" & Chk(TxtVenue.Text) & "', " & _
                         " Address1 = '" & Chk(TxtAddress.Text) & "', " & _
                         " IntrvDate = '" & Format(CDate(dtpInterview.Text), "dd/MMM/yyyy") & "', " & _
                         " VenueEmail  = '" & Chk(TxtEMail.Text) & "', " & _
                         " VenueContactNo = '" & Chk(TxtContectNo.Text) & "', " & _
                         " CommonNote = '" & Chk(TxtCommonNote.Text) & "', " & _
                         " TTMId = '" & ChkN(cmbType.SelectedValue) & "' " & _
                         " Where IntrvID = " & Chk(TxtIntrvrefno.ToolTip)

                Session("DalObj").ExecuteCommand(strSQl, Tran)

                Session("DalObj").ExecuteCommand("Delete From IntrvCandidates Where IntrvID = " & ChkN(TxtIntrvrefno.ToolTip), Tran)

                For i = 0 To grdIntrv.Items.Count - 1
                    If CType(grdIntrv.Items(i).Controls(0).Controls(1), CheckBox).Checked = True Then
                        FromTime = Format(CDate(Chk(CType(grdIntrv.Items(i).Controls(6).Controls(1), TextBox).Text)), "hh:mm")
                        ToTime = Format(CDate(Chk(CType(grdIntrv.Items(i).Controls(7).Controls(1), TextBox).Text)), "hh:mm")
                        strSQl = " Insert IntrvCandidates(IntrvId,Vacancy_Code,TTMId,Res_Code,Time_From,Time_To,Note) Values(" & _
                                 Chk(TxtIntrvrefno.Text) & ", " & _
                                 ChkN(grdIntrv.Items(i).Cells(3).Text) & ", " & _
                                 ChkN(cmbType.SelectedValue) & " , " & _
                                 ChkN(grdIntrv.Items(i).Cells(1).Text) & ", '" & _
                                 FromTime & "' , '" & _
                                 ToTime & "' , '" & _
                                 Chk(CType(grdIntrv.Items(i).Controls(8).Controls(1), TextBox).Text) & "')"
                        Session("DalObj").ExecuteCommand(strSQl, Tran)
                    End If
                Next

            End If

            Tran.Commit()
            If sender.id = "cmdSMail" Then
                cmdSMail_Click(sender, e)
            End If
            btnNew_Click(sender, Nothing)
            FillCombo()
            SetMsg(LblErrMsg, "Record Saved Successfully.")
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (CmdSave_Click)")
            Tran.Rollback()
        Finally
            If Not IsNothing(Tran) Then Tran.Dispose()
        End Try
    End Sub
    Private Function isValidate() As Boolean

        Try
            Dim j, i As Int16
            j = 0
            For i = 0 To grdIntrv.Items.Count - 1
                If CType(grdIntrv.Items(i).Controls(0).Controls(1), CheckBox).Checked = True Then
                    j = j + 1
                End If
            Next
            If j = 0 Then
                SetMsg(LblErrMsg, "Select Atleast One Resume.")
                Return False
            End If

            If TxtIntrvrefno.Text = "" Then
                SetMsg(LblErrMsg, "Interview Ref.No can't be Blank.")
                Return False
            End If
            If cmbIntrvName.SelectedValue = "" Then
                SetMsg(LblErrMsg, "Select The Interviewer Name.")
                Return False
            End If

            If TxtVenue.Text = "" Then
                SetMsg(LblErrMsg, "Venue can't be Blank.")
                Return False
            End If

            Return True
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : isValidate()")
            isValidate = False
        End Try
    End Function

    Private Sub cmbReqNo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbReqNo.SelectedIndexChanged
        Try

            Dim StrSql As String
            DtTemp = New DataTable
            ' For i = 0 To grdIntrv.Items.Count - 1
            If grdIntrv.Items.Count = 0 Then

                ViewState("Candidates") = New DataTable
                StrSql = " Select  Res_Code, Res_No, (isnull(Res_NameF,'')+' '+isnull(Res_NameM,'')+' '+isnull(Res_NameL,'')) as EName, " & _
                         " '00:00' as Time_From, '00:00' as Time_To, R.Vacancy_Code, V.Vacancy_RefNo, '' as Note From ResMast R " & _
                         " Inner Join Vacancy V On R.Vacancy_Code = V.Vacancy_Code Where R.Vacancy_Code = '" & ChkN(cmbReqNo.SelectedValue) & "' " & _
                         " AND Res_Code Not In (Select Res_Code From IntrvCandidates Where Vacancy_Code='" & ChkN(cmbReqNo.SelectedValue) & "' and TTMId ='" & ChkN(cmbType.SelectedValue) & "')"
                Session("DalObj").GetSqlDataTable(ViewState("Candidates"), StrSql)
                grdIntrv.DataSource = ViewState("Candidates")
                grdIntrv.DataBind()
            Else

                Dim DRow As DataRow, Dt As New DataTable, i As Int16
                Dim Dv As DataView

                Dv = New DataView(ViewState("Candidates"))
                Dv.RowFilter = " Vacancy_Code = '" & ChkN(cmbReqNo.SelectedValue) & "'"
                If Dv.Count <> 0 Then Exit Sub

                StrSql = " Select  Res_Code, Res_No, (isnull(Res_NameF,'')+' '+isnull(Res_NameM,'')+' '+isnull(Res_NameL,'')) as EName, " & _
                         " '00:00' as Time_From, '00:00' as Time_To, R.Vacancy_Code, V.Vacancy_RefNo, '' as Note From ResMast R " & _
                         " Inner Join Vacancy V On R.Vacancy_Code = V.Vacancy_Code Where R.Vacancy_Code = '" & ChkN(cmbReqNo.SelectedValue) & "' " & _
                         " AND Res_Code Not In (Select Res_Code From IntrvCandidates Where Vacancy_Code='" & ChkN(cmbReqNo.SelectedValue) & "' and TTMId ='" & ChkN(cmbType.SelectedValue) & "')"
                Session("DalObj").GetSqlDataTable(Dt, StrSql)

                For i = 0 To Dt.Rows.Count - 1
                    DRow = ViewState("Candidates").NewRow
                    DRow("Res_Code") = Dt.Rows(i).Item("Res_Code")
                    DRow("Res_No") = Dt.Rows(i).Item("Res_No")
                    DRow("EName") = Dt.Rows(i).Item("EName")
                    DRow("Time_From") = Dt.Rows(i).Item("Time_From")
                    DRow("Time_To") = Dt.Rows(i).Item("Time_To")
                    DRow("Vacancy_Code") = Dt.Rows(i).Item("Vacancy_Code")
                    DRow("Vacancy_RefNo") = Dt.Rows(i).Item("Vacancy_RefNo")
                    DRow("Note") = Dt.Rows(i).Item("Note")
                    ViewState("Candidates").Rows.Add(DRow)
                Next
                ViewState("Candidates").Acceptchanges()
                grdIntrv.DataSource = ViewState("Candidates")
                grdIntrv.DataBind()
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (Requisition Changed)")
        End Try
    End Sub

    Private Sub cmdClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Private Sub cmdSMail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSMail.Click
        Try
            Dim i As Int16, From, ResEmailId, Subject, Body, Name, SmtpServer, Timing As String

            SmtpServer = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand("Select isnull(PLACE,'') From COMPMAST", , DAL.DataLayer.ExecutionType.ExecuteScalar)


            '================================  Mail For Interviewer ====================================

            'SendMail(From, ResEmailId, Subject, Body, SmtpServer)


            Dim DTSetup As New DataTable
            CType(Session("DalObj"), DAL.DataLayer).GetSqlDataTable(DTSetup, " Select Subj, Message, MailCC From EmailReminderSetup Where FormKey=10 and Active = 1")

            If DTSetup.Rows.Count <> 0 Then
                For i = 0 To grdIntrv.Items.Count - 1
                    If CType(grdIntrv.Items(i).Controls(0).Controls(1), CheckBox).Checked = True Then
                        Subject = Chk(DTSetup.Rows(0).Item("Subj"))
                        Body = Chk(DTSetup.Rows(0).Item("Message"))
                        Body = Replace(Body, "@CNOTE", Chk(TxtCommonNote.Text))
                        Body = Replace(Body, "@LOCATION", Chk(TxtVenue.Text))
                        Body = Replace(Body, "@ADDRESS", Chk(TxtAddress.Text))
                        Body = Replace(Body, "@EMAIL", Chk(TxtEMail.Text))
                        Body = Replace(Body, "@CONTACT", Chk(TxtContectNo.Text))
                        Body = Replace(Body, "@FROM", Session("LoginUser").UserId)
                        From = Chk(DTSetup.Rows(0).Item("MailCC"))

                        ResEmailId = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand("Select Isnull(Res_EMailId,'') from ResOthers Where Res_Code='" & ChkN(grdIntrv.Items(i).Cells(1).Text) & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
                        Name = Chk(grdIntrv.Items(i).Cells(5).Text)
                        Timing = CType(grdIntrv.Items(i).FindControl("TxtFrom"), TextBox).Text & " hr."
                        Timing = Timing & " - " & CType(grdIntrv.Items(i).FindControl("TxtTo"), TextBox).Text & " hr."
                        Body = Replace(Body, "@TIMING", Timing)
                        Body = Replace(Body, "@NOTE", Chk(CType(grdIntrv.Items(i).FindControl("TxtNote"), TextBox).Text))
                        Body = Replace(Body, "@NAME", Name)
                        SendMail(From, ResEmailId, Subject, Body, SmtpServer)
                    End If
                Next
            Else
                LblErrMsg.Text = LblErrMsg.Text & "<BR>" & " Email Reminder is not defined for Interview Shedule, Unable to send mail."
                Exit Sub
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (Save & Mail)")
        End Try
    End Sub

    Sub SendMail(ByVal FromID As String, ByVal ToID As String, ByVal Subject As String, ByVal Body As String, ByVal MailServer As String)
        Dim MyMail As New Mail.MailMessage

        MyMail.From = FromID
        MyMail.To = ToID
        MyMail.Subject = Subject
        MyMail.Body = Body
        Mail.SmtpMail.SmtpServer = MailServer.Trim.ToString
        Mail.SmtpMail.Send(MyMail)
    End Sub

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub

End Class


