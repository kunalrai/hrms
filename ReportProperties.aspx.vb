Partial Class ReportProperties
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
        Try
            If Not IsPostBack Then
                Dim StrSql As String, Dt As New DataTable
                StrSql = " Select * From MstReports Where SrNo = " & ChkN(Request.QueryString.Item("Rpt"))
                Session("DalObj").GetSqlDataTable(Dt, StrSql)
                If Dt.Rows.Count <> 0 Then
                    TxtDefFilter.Text = Chk(Dt.Rows(0).Item("RptFor"))
                    TxtFileName.Text = Chk(Dt.Rows(0).Item("RptFile"))
                    TxtNoGroup.Text = ChkN(Dt.Rows(0).Item("Groups"))
                    TxtRptName.Text = Chk(Dt.Rows(0).Item("RptName"))
                    TxtSortOrder.Text = Chk(Dt.Rows(0).Item("RptOrders"))
                End If
            End If
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : On Load")
        End Try
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            Dim StrSql As String

            StrSql = " UpDate MstReports Set " & _
                     " RptFor = '" & Chk(TxtDefFilter.Text) & "', " & _
                     " RptName = '" & Chk(TxtRptName.Text) & "', " & _
                     " Groups = '" & Chk(TxtNoGroup.Text) & "', " & _
                     " RptOrders = '" & Chk(TxtSortOrder.Text) & "' Where RptFile = '" & Chk(TxtFileName.Text) & "'"


            Session("DalObj").ExecuteCommand(StrSql)

        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : On Save")
        End Try
    End Sub

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub

End Class
