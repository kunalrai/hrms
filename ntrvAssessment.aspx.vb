Public Class ntrvAssessment
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents LblErrMsg As System.Web.UI.WebControls.Label
    Protected WithEvents cmbIntrvrefid As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cmbReqNo As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cmbResumes As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cmbResStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents TblIntrAss As System.Web.UI.WebControls.Table
    Protected WithEvents cmdSave As System.Web.UI.WebControls.Button
    Protected WithEvents cmdClose As System.Web.UI.WebControls.Button
    Protected WithEvents ChkChange As System.Web.UI.HtmlControls.HtmlInputCheckBox

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
    End Sub

End Class
