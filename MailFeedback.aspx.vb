Partial Class MailFeedback
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If

        If Not IsPostBack Then
            BindGrid()
        End If
    End Sub

    Sub BindGrid()
        Try
            Dim Dt As New DataTable, i As Int16, Drow As DataRow
            Dim SECTCODE As String

            SECTCODE = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(" Select isnull(Sect_Code,'') From SECTMAST Where AdminUserId = '" & Session("LoginUser").UserId & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)

            CType(Session("DalObj"), DAL.DataLayer).GetSqlDataTable(Dt, "Select EntryDate,MailMsg,E.EmailId,MailSubj,MailCC,E.UserId,W.UserName from EmailReminderList E Inner join WebUsers W On E.UserId = W.UserId Where E.Sect_Code ='" & SECTCODE & "'")

            For i = 0 To Dt.Rows.Count - 1
                Drow = Dt.Rows(i)
                Drow("UserName") = Encrypt(Drow("UserName"), "-")
                Drow.AcceptChanges()
                Dt.AcceptChanges()
            Next

            GrdEmail.DataSource = Dt
            GrdEmail.DataBind()
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message)
        End Try
    End Sub

End Class
