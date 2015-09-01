Imports System.Runtime.InteropServices
Partial Class Reminders
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
        Dim sMessage, tmp As String
        sMessage = SetReport(18, "", Session("DalObj"), Session("LoginUser"))
        If sMessage = "Ready" Then
            'Response.RedirectLocation = "_Blank"
            'Response.Redirect("ReportView.aspx")
            tmp = " <SCRIPT language=javascript >window.open ('ReportView.aspx','')</SCRIPT>"
            RegisterStartupScript("Rem", tmp)
        End If
        'Response.Redirect("EmpExplorer.aspx?SrNo=61")
    End Sub

End Class
