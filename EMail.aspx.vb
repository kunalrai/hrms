Partial Class EMail
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
    'Shared OL As Outlook.Application
    'Shared mm As Outlook.MailItem
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If

        If Not IsPostBack Then
            'By Ravi 2 DEC

            Dim SrNo As Int16
            SrNo = Request.QueryString.Item("SrNo") + Request.QueryString.Item("MODULEID")
            Dim bSuccess As Boolean
            Select Case CheckRight(SrNo)
                Case MdlHRMS.AccessType.FullAccess, MdlHRMS.AccessType.SuperUser
                    TxtRights.Text = "S"
                Case MdlHRMS.AccessType.ReadonlyAccess, MdlHRMS.AccessType.Restricted
                    TxtRights.Text = "V"
            End Select

            '------------------------------------
            Dim Code As Object
            Code = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(" Select Count(*) From HrdMast Where Emp_Code ='" & Session("LoginUser").UserId & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
            If Code <> 0 Then
                txtTo.Enabled = False
                txtCC.Enabled = False
                SetMailAddress()
            Else
                txtTo.Enabled = True
                txtCC.Enabled = True
            End If
        End If
    End Sub

    Sub SetMailAddress()
        Try
            Dim FromEmailID, EMailID, MailCC As String, i As Int16
            EMailID = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand("Select isnull(EMailID,'') From SECTMAST WHERE SECT_CODE in (Select SECT_CODE FROM HRDMASTQRY WHERE EMP_CODE = '" & Session("LoginUser").UserId & "')", , DAL.DataLayer.ExecutionType.ExecuteScalar)
            MailCC = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand("Select isnull(MailCC,'') From SECTMAST WHERE SECT_CODE in (Select SECT_CODE FROM HRDMASTQRY WHERE EMP_CODE = '" & Session("LoginUser").UserId & "')", , DAL.DataLayer.ExecutionType.ExecuteScalar)
            FromEmailID = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand("Select isnull(EMAILID,isnull(PEMailID,'')) From HRDMAST WHERE EMP_CODE = '" & Session("LoginUser").UserId & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)

            'FromEmailID = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand("Select isnull(Sign_Place,'') From COMPMAST", , DAL.DataLayer.ExecutionType.ExecuteScalar)

            TxtFrom.Text = FromEmailID.Trim
            txtTo.Text = EMailID
            txtCC.Text = MailCC
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message)
        End Try
    End Sub

    Private Sub cmdSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSend.Click
        'Try
        If Not IsValidate() Then Exit Sub

        Dim MyMail As New Mail.MailMessage, SmtpServer, FromId As String

        SmtpServer = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand("Select isnull(PLACE,'') From COMPMAST", , DAL.DataLayer.ExecutionType.ExecuteScalar)

        'FromId = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand("Select isnull(Sign_Place,'') From COMPMAST", , DAL.DataLayer.ExecutionType.ExecuteScalar)

        MyMail.From = TxtFrom.Text
        MyMail.To = txtTo.Text
        MyMail.Cc = txtCC.Text
        MyMail.Subject = txtSubject.Text
        If TxtAttach.PostedFile.FileName <> "" Then
            Dim MailAttach As New Mail.MailAttachment(TxtAttach.PostedFile.FileName)
            MyMail.Attachments.Add(MailAttach)
        End If
        MyMail.Body = txtBody.Text
        Mail.SmtpMail.SmtpServer = SmtpServer.Trim.ToString
        Mail.SmtpMail.Send(MyMail)

        SendMail()
        SetMsg(lblMsg, "Mail Send Successfully.")
        Blank()
        'Catch ex As Exception
        '    SetMsg(lblMsg, ex.Message)
        'End Try
    End Sub

    Sub SendMail()
        Try
            Dim StrQry As String

            Dim SECTCODE As String

            SECTCODE = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(" Select SECT_CODE FROM HRDMAST Where EMP_CODE = '" & Session("LoginUser").UserId & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)

            StrQry = "Insert EmailReminderList(EntryDate,EmailID,MailSubj,MailMsg,MailCC,UserID,Status,SECT_CODE) Values ('" & _
                     Format(Date.Today, "dd/MMM/yyyy") & "', '" & _
                     Chk(txtTo.Text) & "', '" & _
                     Chk(txtSubject.Text) & "', '" & _
                     Chk(txtBody.Text) & "', '" & _
                     Chk(txtCC.Text) & "', '" & _
                     Encrypt(Session("LoginUser").UserId, "+") & "', 'Q','" & SECTCODE & "' )"

            Session("DalObj").ExecuteCommand(StrQry)
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & ": SendMail()")
        End Try
    End Sub
    Sub Blank()
        txtBody.Text = ""
        txtSubject.Text = ""
        'txtFrom.Text = ""
    End Sub

    Function IsValidate() As Boolean
        Try

            If Chk(txtSubject.Text) = "" Then
                SetMsg(lblMsg, "Email Subject Can not be left blank.")
                Return False
            End If

            If Chk(txtBody.Text) = "" Then
                SetMsg(lblMsg, "Email Message Can not be left blank.")
                Return False
            End If

            If TxtFrom.Text = "" Then
                Dim EMailID As String
                If txtTo.Enabled = False Then
                    EMailID = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand("Select isnull(DefEmailID,'') From SECTMAST WHERE SECT_CODE in (Select SECT_CODE FROM HRDMASTQRY WHERE EMP_CODE = '" & Session("LoginUser").UserId & "')", , DAL.DataLayer.ExecutionType.ExecuteScalar)
                    If EMailID.Trim = "" Then
                        SetMsg(lblMsg, "Unable to find Sender email Id, Mail Undelivered.")
                        Return False
                    Else
                        TxtFrom.Text = EMailID
                    End If
                Else
                    EMailID = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand("Select isnull(IO_TIME_FIELD,'') From COMPMAST", , DAL.DataLayer.ExecutionType.ExecuteScalar)
                    If EMailID.Trim = "" Then
                        SetMsg(lblMsg, "Unable to find Sender email Id, Mail Undelivered.")
                        Return False
                    Else
                        TxtFrom.Text = EMailID
                    End If
                End If
            End If

            Return True
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message)
        End Try
    End Function

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub

    'Sub CreateOutLook()

    '    OL = GetObject(, "Outlook.Application")
    '    If Err.Number <> 0 Then
    '        Dim s As String
    '        s = Err.Description '********>>>Here comes message = "Can't create activex component"
    '        Err.Clear()
    '        OL = Server.CreateObject("Outlook.Application")
    '    End If
    '    mm = OL.CreateItem(Outlook.OlItemType.olMailItem)
    '    mm.ReplyRecipients.Add("mr_mike_XXX@hotmail.com (mr_mike_XXX@hotmail.com)")
    '    'mm.SentOnBehalfOfName = mr_yyy (mr_yyyv@zzzzz.com)v@zzzzz.com (mr_yyyv@zzzzz.com) 
    '    'mm.To = mr_ccc (mr_ccc@zzzzz.com)@zzzzz.com (mr_ccc@zzzzz.com)"
    '    mm.Subject = "Testing EMAIL /using COM-DLL component/ from Outlook at Webserver"
    '    mm.Body = ">>>>>>***** TESTING EMAIL ******<<<<<<<<"
    '    mm.SaveAs("g:\xxxyyyzzzz\WebTEST_EMAIL_xx02.msg")
    '    mm.Send()
    '    OL = Nothing

    'End Sub

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub

End Class
