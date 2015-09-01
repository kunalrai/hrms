Imports System.Web.Mail
Public Class SendMail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents LblErrMsg As System.Web.UI.WebControls.Label
    Protected WithEvents CmbAcHead As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ChkZero As System.Web.UI.WebControls.CheckBox
    Protected WithEvents GrdLoanStatus As System.Web.UI.WebControls.DataGrid


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
        Dim DAL As DAL.DataLayer
        Dim BAL As BAL.BLayer
        If Not IsPostBack Then
            Dim cnt As Int16

            'SetMsg(LblMsg, "H R U")
        End If
    End Sub

End Class
