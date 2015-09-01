Partial Class EmpLeavStatus
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
        If Not IsPostBack Then
            If Not IsNothing(Request.QueryString.Item("Code")) Then
                LblCode.Text = Request.QueryString.Item("Code")
                LblName.Text = Request.QueryString.Item("Name")
                Dim StrSql As String, DtTable As New DataTable
                StrSql = "Select LM.levyear, LM.Lvtype as LvType , LT.LvDesc as LvDesc, isnull(LM.Opening,0) as Opening, isnull(LM.Earned,0)as Earned, isnull(LM.Availed,0) as Availed, " & _
                                    " ((isnull(LM.Earned,0) + isnull(LM.Opening,0)) - isnull(LM.Availed,0)) as Balance " & _
                                    " From leavmast LM inner join lvtype LT on LM.lvtype = LT.lvtype " & _
                                    " Where Emp_Code='" & Request.QueryString.Item("Code") & "' and levyear=" & Session("LeavYear")
                Session("DalObj").GetSqlDataTable(DtTable, StrSql)
                grdLeavBal.DataSource = DtTable
                grdLeavBal.DataBind()
            End If
        End If
    End Sub
    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub

End Class
