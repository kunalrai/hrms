Public Class ResumeAssess
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents LblErrMsg As System.Web.UI.WebControls.Label
    Protected WithEvents TxtCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents cmbCode As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnList As System.Web.UI.WebControls.ImageButton
    Protected WithEvents LblName As System.Web.UI.WebControls.Label

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
            CType(Session("BalObj"), BAL.BLayer).FillCombo(cmbCode, "Select Res_Code, (Res_No +' - '+(Res_NameF+' '+Res_NameM+' '+Res_NameL)) as Res_Name From ResMast")
        End If
    End Sub

    Private Sub btnList_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnList.Click
        btnList.Visible = False
        TxtCode.Visible = False
        cmbCode.Visible = True
    End Sub

    Private Sub cmbCode_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCode.SelectedIndexChanged
        btnList.Visible = True
        TxtCode.Visible = True
        cmbCode.Visible = False
        TxtCode.Text = Chk(cmbCode.SelectedValue)
    End Sub
End Class
