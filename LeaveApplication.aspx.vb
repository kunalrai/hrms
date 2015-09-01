Partial Class LeaveApplication
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents DataGrid1 As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim StrSql As String
    Dim DalObj As DAL.DataLayer
    Public WOFF As String
    Dim Already As Int16

#Region "    On Load    "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        lblAlready.Text = ""
        lblMsg.Text = ""
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
                CType(Session("BalObj"), BAL.BLayer).FillCombo(cmbManager, " SELECT EMP_CODE, EMP_NAME FROM HRDMASTQRY Where Ltype=1 " & Session("UserCodes") & " and EMP_CODE<>'" & Session("LoginUser").UserId & "' Order By EMP_NAME", True)

                '====================Binding Leaves Grid=========================================
                ViewState("DTLeavBal") = New DataTable
                StrSql = "Select LM.levyear, LM.Lvtype as LvType , LT.LvDesc as LvDesc, isnull(LM.Opening,0) as Opening, isnull(LM.Earned,0)as Earned, isnull(LM.Availed,0) as Availed, " & _
                    " ((isnull(LM.Earned,0) + isnull(LM.Opening,0)) - isnull(LM.Availed,0)) as Balance " & _
                    " From leavmast LM inner join lvtype LT on LM.lvtype = LT.lvtype " & _
                    " Where Emp_Code='" & Session("Loginuser").userId & "' and levyear=" & Session("LeavYear")
                Session("DalObj").GetSqlDataTable(ViewState("DTLeavBal"), StrSql)
                grdLeavBal.DataSource = ViewState("DTLeavBal")
                grdLeavBal.DataBind()
                '====================================================

                Dim EMPCODE As String
                EMPCODE = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(" Select Isnull(Mngr_Code,'') From Hrdmast Where EMP_CODE='" & Session("Loginuser").userId & "' ", , DAL.DataLayer.ExecutionType.ExecuteScalar)

                If EMPCODE <> "" Then
                    cmbManager.SelectedValue = EMPCODE
                End If

                Session("BalObj").FillCombo(cmbLvType, "Select Lvtype, Lvdesc From Lvtype")

                cmbLvFor.Items.Add(New ListItem("Full Day", "11"))
                cmbLvFor.Items.Add(New ListItem("First Half", "01"))
                cmbLvFor.Items.Add(New ListItem("Second Half", "10"))

                dtpFromDate.DateValue = Format(Date.Today, "dd/MMM/yyyy")
                dtpToDate.DateValue = Format(Date.Today, "dd/MMM/yyyy")
            Catch ex As Exception
                SetMsg(lblMsg, ex.Message)
            End Try
            cmdSave.Attributes.Add("onclick", "return ValidateCtrl();")
        End If
    End Sub

#End Region

#Region "  Save Application Record  "

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If validation() Then
            SaveChanges()
        End If
    End Sub

    Private Function validation() As Boolean
        Try
            'If cmbLvType.SelectedValue = "" Then
            '    SetMsg(lblMsg, "Please select Leave Type From the list, Record Not Saved.")
            '    Return False
            'End If

            'If cmbManager.SelectedItem.Text = "" Then
            '    SetMsg(lblMsg, "Please select Manage from the list, Record Not Saved.")
            '    Return False
            'End If

            'If Format(dtpFromDate.DateValue, "dd/MMM/yyyy") > Format(dtpToDate.DateValue, "dd/MMM/yyyy") Then
            '    SetMsg(lblMsg, "From Date Can Not Be after To Date, Record Not Saved.")
            '    Return False
            'End If
            'If (cmbLvFor.SelectedValue <> "11") And (dtpFromDate.DateValue <> dtpToDate.DateValue) Then
            '    SetMsg(lblMsg, "From Date and To Date Should be Same For Half Day Leave, Record Not Saved.")
            '    Return False
            'End If

            If ChkN(Session("dvleave").count) > 0 Then
                If ChkN(txtDays.Text) > ChkN(Session("dvleave").Item(0).Item("Balance")) Then
                    SetMsg(lblMsg, "Available Balance is not Sufficient in this Leave Type, Record Not Saved.")
                    Return False
                End If
            Else
                SetMsg(lblMsg, "No Balance Available in this Leave Type, Record Not Saved.")
                Return False
            End If

            If Chk(TxtContAdd.Text) = "" Then
                SetMsg(lblMsg, "Please enter your Contact & E-mail details, Record Not Saved.")
                Return False
            End If

            Return True
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message)
        End Try
    End Function

    Private Sub SaveChanges()
        Dim LvType As String
        Dim i As Int16
        Dim j As Int16
        Dim add As Boolean
        Dim Atdate As DateTime
        Dim dvTemp As New DataView
        Dim TotalDays As Single
        Dim ResultString As String
        TotalDays = 0
        'Try

        Dim Cnt As Int16
        Cnt = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(" Select isnull(AddHolidays,0) From LvType Where LVTYPE='" & Chk(cmbLvType.SelectedValue) & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)

        If cmbLvFor.SelectedValue = "11" Then
            LvType = cmbLvType.SelectedValue & cmbLvType.SelectedValue
        ElseIf cmbLvFor.SelectedValue = "01" Then
            LvType = cmbLvType.SelectedValue & "P"
        ElseIf cmbLvFor.SelectedValue = "10" Then
            LvType = "P" & cmbLvType.SelectedValue
        End If
        'WOFF = WeekdayName(ChkN(Session("dalobj").ExecuteCommand(StrSql, , DAL.DataLayer.ExecutionType.ExecuteScalar)))
        For i = 0 To ChkN(DateDiff(DateInterval.Day, CDate(dtpFromDate.DateValue), CDate(dtpToDate.DateValue)))
            Atdate = DateAdd(DateInterval.Day, i, CDate(dtpFromDate.DateValue))
            StrSql = "select top 1 WeeklyOff from hrdshift where emp_code='" & Session("Loginuser").userId & "' and shift_WEF < '" & Format(Atdate, "dd/MMM/yyyy") & "' order by Shift_WEF desc"
            WOFF = Session("dalobj").ExecuteCommand(StrSql, , DAL.DataLayer.ExecutionType.ExecuteScalar)

            dvTemp = Session("DalObj").GetDataView(ViewState("dtHolidays"), "", "Hdate='" & Format(Atdate, "dd/MMM/yyyy") & "'")

            If Cnt <> 0 Then
                If dvTemp.Count = 0 And InStr(WOFF, Weekday(CDate(Atdate))) = 0 Then   'WeekdayName( Weekday(CDate(Atdate))) <> WOFF
                    Already = ChkN(Session("DalObj").ExecuteCommand("Select count(*) from LeavApp Where Atdate ='" & Format(Atdate, "dd/MMM/yyyy") & "' and EMP_CODE='" & Session("Loginuser").userId & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar))
                    If Already > 0 Then
                        ResultString = ResultString & "Leave For '" & Format(Atdate, "dd/MMM/yyyy") & "' Already Exist! <BR> "
                        add = False
                    Else
                        add = True
                        If cmbLvFor.SelectedValue = "01" Or cmbLvFor.SelectedValue = "01" Then
                            TotalDays = TotalDays + 0.5
                        Else
                            TotalDays = TotalDays + 1
                        End If
                    End If
                Else
                    add = False
                    ResultString = ResultString & "'" & Format(Atdate, "dd/MMM/yyyy") & "' is a Holiday / Weekly Off! <BR>"
                End If
            Else
                add = True
                If cmbLvFor.SelectedValue = "01" Or cmbLvFor.SelectedValue = "01" Then
                    TotalDays = TotalDays + 0.5
                Else
                    TotalDays = TotalDays + 1
                End If
            End If


            If add = True Then
                StrSql = "Insert Into LeavApp (LEVYEAR,EMP_CODE,AtDate,LV_FROM,LV_TO,LVDAYS,LVTYPE,AppDate,Status,ProcessAuth,Reason,ContAdd)" & _
                                " Values (" & _
                                Session("LeavYear") & ", '" & Session("Loginuser").userId & "', '" & Format(Atdate, "dd/MMM/yyyy") & "'," & _
                                "'" & dtpFromDate.DateValue & "', '" & dtpToDate.DateValue & "', " & 1 & ", '" & LvType & "', " & _
                                "'" & Format(Today.Now(), "dd/MMM/yyyy") & "', 'A','" & cmbManager.SelectedValue & "','" & txtReason.Text & "','" & TxtContAdd.Text & "')"
                Session("dalobj").ExecuteCommand(StrSql)
            End If
        Next

        If TotalDays > 0 Then
            SetMsg(lblMsg, "You Have Applied for " & ChkN(TotalDays) & " Days Leave Successfully")



            SendMail(Session("Loginuser").userId, cmbManager.SelectedValue, TotalDays)
        End If
        SetMsg(lblAlready, ResultString)
        'Catch ex As Exception
        '    SetMsg(lblMsg, ex.Message)
        'Finally
        '    dvTemp.Dispose()
        'End Try
    End Sub

#End Region

#Region "    Check Available Days     "

    Private Sub cmdCalDays_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCalDays.Click
        CheckAvailibility()
    End Sub

    Private Sub CheckAvailibility()
        If IsDate(dtpFromDate.DateValue) Or IsDate(dtpToDate.DateValue) Then
            Dim count As Int16
            Dim atdate As DateTime
            Dim TotalDays As Int16 = 0
            Dim dvTemp As New DataView
            Dim LvType As String
            Try
                lblAlready.Text = ""
                lblMsg.Text = ""
                If cmbLvFor.SelectedValue = "11" Then
                    LvType = cmbLvType.SelectedValue & cmbLvType.SelectedValue
                ElseIf cmbLvFor.SelectedValue = "01" Then
                    LvType = cmbLvType.SelectedValue & "P"
                ElseIf cmbLvFor.SelectedValue = "10" Then
                    LvType = "P" & cmbLvType.SelectedValue
                End If

                Dim Cnt As Int16
                Cnt = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(" Select isnull(AddHolidays,0) From LvType Where LVTYPE='" & Chk(cmbLvType.SelectedValue) & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)

                '====================Calculate Holidays =================
                ViewState("dtHolidays") = New DataTable
                StrSql = "select * from holidays where isnull(TYPE,'')='N' and loc_code='" & Session("Loginuser").UserLocationCode & "' and hdate between '" & dtpFromDate.DateValue & "' and '" & dtpToDate.DateValue & "'"
                Session("DalObj").GetSqlDataTable(ViewState("dtHolidays"), StrSql)

                '===========================Count Leave Days=================================================
                Session("dvleave") = Session("DalObj").GetDataView(ViewState("DTLeavBal"), "", "lvtype='" & Chk(cmbLvType.SelectedValue) & "'")

                For count = 0 To ChkN(DateDiff(DateInterval.Day, CDate(dtpFromDate.DateValue), CDate(dtpToDate.DateValue)))
                    atdate = DateAdd(DateInterval.Day, count, CDate(dtpFromDate.DateValue))

                    If Cnt <> 0 Then
                        StrSql = "select top 1 WeeklyOff from hrdshift where emp_code='" & Session("Loginuser").userId & "' and shift_WEF < '" & Format(atdate, "dd/MMM/yyyy") & "' order by Shift_WEF desc"
                        WOFF = Session("dalobj").ExecuteCommand(StrSql, , DAL.DataLayer.ExecutionType.ExecuteScalar)
                        dvTemp = Session("DalObj").GetDataView(ViewState("dtHolidays"), "", "Hdate='" & Format(atdate, "dd/MMM/yyyy") & "'")
                        If dvTemp.Count = 0 And InStr(WOFF, Weekday(CDate(atdate))) = 0 Then 'WeekdayName(Weekday(CDate(Atdate))) <> WOFF
                            Already = ChkN(Session("DalObj").ExecuteCommand("Select count(*) from LeavApp Where Atdate ='" & Format(atdate, "dd/MMM/yyyy") & "' and emp_code='" & Session("Loginuser").userId & "' AND LVTYPE='" & LvType & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar))
                            If Already = 0 Then
                                TotalDays = TotalDays + 1
                            End If
                        End If
                    Else
                        Already = ChkN(Session("DalObj").ExecuteCommand("Select count(*) from LeavApp Where Atdate ='" & Format(atdate, "dd/MMM/yyyy") & "' and emp_code='" & Session("Loginuser").userId & "' AND LVTYPE='" & LvType & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar))
                        If Already = 0 Then
                            TotalDays = TotalDays + 1
                        End If
                    End If
                Next
                txtDays.Text = ChkN(TotalDays)

                '=============================Check Available Balance====================================
                'Dim Availed As Int16
                'Availed = Session("DalObj").ExecuteCommand("Select count(*) from LeavApp Where LvType='" & Chk(LvType) & "' and Status='S'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
            Catch ex As Exception
                SetMsg(lblMsg, ex.Message)
            Finally
                dvTemp.Dispose()
            End Try
        End If
    End Sub

#End Region

    Private Sub cmdClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClear.Click
        cmbLvFor.SelectedIndex = 1
        cmbLvType.SelectedIndex = 1
        txtDays.Text = ""
        dtpFromDate.DateValue = Format(Date.Today, "dd/MMM/yyyy")
        dtpToDate.DateValue = Format(Date.Today, "dd/MMM/yyyy")
        txtReason.Text = ""

    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Sub SendMail(ByVal FromID As String, ByVal ToID As String, ByVal Days As Single)
        Dim Id1, Id2, Subject, Body, Name As String

        Id1 = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(" Select Isnull(EmailId,IsNull(PEmailId,'')) From Hrdmast Where EMP_CODE='" & Session("Loginuser").userId & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
        Id2 = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(" Select Isnull(EmailId,IsNull(PEmailId,'')) From Hrdmast Where EMP_CODE = '" & cmbManager.SelectedValue & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
        Name = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(" Select IsNull(EMP_NAME,'') From HRDMASTQRY Where EMP_CODE='" & Session("Loginuser").userId & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)

        If Id1 = "" Then
            lblMsg.Text = lblMsg.Text & "<BR>" & " Employee's Email Id is not defined, Unable to send mail."
            Exit Sub
        End If

        If Id2 = "" Then
            lblMsg.Text = lblMsg.Text & "<BR>" & " Employee's Manager Email Id is not defined, Unable to send mail."
            Exit Sub
        End If

        Dim DTSetup As New DataTable
        CType(Session("DalObj"), DAL.DataLayer).GetSqlDataTable(DTSetup, " Select Subj, Message From EmailReminderSetup Where FormKey=7 and Active = 1")

        If DTSetup.Rows.Count <> 0 Then
            Subject = Chk(DTSetup.Rows(0).Item("Subj"))
            Subject = Replace(Subject, "@EMPNAME", Name)
            Body = Chk(DTSetup.Rows(0).Item("Message"))
            Body = Replace(Body, "@Days", Days)
            Body = Replace(Body, "@REASON", IIf(txtReason.Text.Trim = "", "Not Defined", txtReason.Text.Trim))
            Body = Replace(Body, "@CONTACT", IIf(TxtContAdd.Text.Trim = "", "Not Defined", TxtContAdd.Text.Trim))
        Else
            lblMsg.Text = lblMsg.Text & "<BR>" & " Email Reminder is not defined for Leave Application, Unable to send mail."
            Exit Sub
        End If

        Dim MyMail As New Mail.MailMessage, SmtpServer As String
        SmtpServer = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand("Select isnull(PLACE,'') From COMPMAST", , DAL.DataLayer.ExecutionType.ExecuteScalar)
        ''Dim UserName As String
        ''Dim Pwd As String
        ''UserName = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand("Select isnull(UserName,'') From COMPMAST", , DAL.DataLayer.ExecutionType.ExecuteScalar)
        ''Pwd = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand("Select isnull(Password,'') From COMPMAST", , DAL.DataLayer.ExecutionType.ExecuteScalar)

        ''Pwd = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand("Select isnull(PLACE,'') From COMPMAST", , DAL.DataLayer.ExecutionType.ExecuteScalar)
        ''MyMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", SmtpServer)
        ''MyMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", 25)
        ''MyMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", 2)
        ''MyMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", 1)
        ''MyMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", userName)
        ''MyMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", Pwd)



        MyMail.From = Id1
        MyMail.To = Id2
        MyMail.Subject = Subject
        MyMail.Body = Body
        Mail.SmtpMail.SmtpServer = SmtpServer.Trim.ToString
        Mail.SmtpMail.Send(MyMail)
    End Sub

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub

End Class
