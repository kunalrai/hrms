Public Class Reports
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents LstReports As System.Web.UI.WebControls.ListBox
    Protected WithEvents cmbSelect As System.Web.UI.WebControls.DropDownList
    Protected WithEvents TxtCriteria As System.Web.UI.WebControls.TextBox
    Protected WithEvents cmdClose As System.Web.UI.WebControls.Button
    Protected WithEvents cmbFor As System.Web.UI.WebControls.DropDownList
    Protected WithEvents DtpFrom As DITWebLibrary.DTPCombo
    Protected WithEvents DtpTo As DITWebLibrary.DTPCombo
    Protected WithEvents TxtBox As System.Web.UI.WebControls.TextBox
    Protected WithEvents cmdPrintView As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents ee As System.Web.UI.WebControls.Button

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
            CType(Session("BalObj"), BAL.BLayer).FillCombo(LstReports, " Select RptFile,RptName from MstReports")
            DtpTo.DateValue = Date.Today
            DtpFrom.DateValue = Date.Today
        End If
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Private Sub LstReports_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LstReports.SelectedIndexChanged
        If LstReports.SelectedIndex = 0 Then
            Session("Query") = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(" Select RptQuery From MstReports where RptFile='" & Chk(LstReports.SelectedValue) & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
        End If
    End Sub

    Private Sub cmbFor_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFor.SelectedIndexChanged
        Dim Str As String
        Str = "SELECT EMP_CODE as Code, (FName+' '+lname) as Name From HrdMast"
        If cmbFor.SelectedValue = 1 Then
            CType(Session("BalObj"), BAL.BLayer).FillCombo(cmbSelect, Str)
        End If
    End Sub

    Private Sub ee_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ee.Click
        Session("Query") = Session("Query") & " Where Emp_Code='2434'"
        frmHTMLReports.argStrSql = Session("Query")
    End Sub
End Class
