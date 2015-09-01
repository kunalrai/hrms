Partial Class TestForm
    Inherits System.Web.UI.Page

    Public Ds As New DataSet

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
        Ajax.Utility.RegisterTypeForAjax(GetType(TestForm))
        'Put user code to initialize the page here

    End Sub

    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
    Public Function GetList() As DataSet
        Try
            Dim strSql As String
            strSql = "Select Top 200 Emp_Code From HRDMAST"
            Session("DalObj").GetSqlDataSet(Ds, strSql)
            GetList = Ds
        Catch ex As Exception

        End Try
    End Function

End Class
