Partial Class Masters
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
    'Dim DAL As New DAL.DataLayer
    Dim DtCount As New DataTable
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            Dim i%
            Session("DalObj").GetSqlDataTable(DtCount, "Select Code, Description, TableName, ScreenName from GenMast")
            If DtCount.Rows.Count = 0 Then Exit Sub
            Response.Write("<table width=100% bgcolor=#cecbce>")
            Response.Write("<tr><td align=right>")
            For i = 0 To DtCount.Rows.Count - 1
                Response.Write("<A href=" & Chk(DtCount.Rows(i).Item("ScreenName")) & ".aspx?TblName=" & Chk(DtCount.Rows(i).Item("TableName")) & "&Desc=" & Chk(DtCount.Rows(i).Item("Description")) & "  >" & Chk(DtCount.Rows(i).Item("Description")) & "</A> | ")
            Next
            Response.Write("<A href =Main.aspx><b>Main</b></A>")
            Response.Write("</td>")
            Response.Write("</tr>")
            Response.Write("</table>")
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message)
        End Try

    End Sub

End Class
