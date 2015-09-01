Imports BAL.BLayer
Partial Class ORGCHART
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
    Dim strSql As String
    Dim objBal As BAL.BLayer
    Dim objDal As DAL.DataLayer
    Dim i, j, k As Int16
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Dim dtOrg As New DataTable
        Dim tblRow As TableRow
        Dim tblCell As TableCell
        objBal = Session("ObjBal")
        objDal = Session("DalObj")
        'Response.Write("PostBack -" & IsPostBack.ToString)
        If Not IsNothing(Request.QueryString.Item("Dsg_Code")) Then
            If Request.QueryString.Get("Dsg_Code").ToUpper = "" Then
                strSql = "SELECT * FROM DsgMast WHERE Under_Dsg = '*'"
            Else
                strSql = "SELECT * FROM DsgMast WHERE UPPER(Dsg_Code) = '" & Request.QueryString.Get("Dsg_Code").ToUpper & "'"
            End If
        Else
            strSql = "SELECT * FROM DsgMast WHERE Under_Dsg = '*'"
        End If
        objDal.GetSqlDataTable(dtOrg, strSql)
        tblRow = New TableRow
        For i = 0 To dtOrg.Rows.Count - 1
            tblCell = New TableCell
            tblCell.Text = dtOrg.Rows(i).Item("Dsg_Name")
            CreateChild(dtOrg.Rows(i).Item("Dsg_Code"), dtOrg.Rows(i).Item("Dsg_Name"), tblCell)
            tblRow.Cells.Add(tblCell)
        Next
        tblOrg.Rows.Add(tblRow)
    End Sub

    Private Sub CreateChild(ByVal Dsg_Code As String, ByVal dsg_Name As String, ByRef tblCell As TableCell)
        Try
            Dim x As Int16
            Dim dtChild As New DataTable
            Dim tblChildRow As TableRow
            Dim tblChildCell As TableCell
            Dim tblChild As Table
            strSql = "SELECT * FROM DsgMast WHERE Under_Dsg = '" & Trim(Dsg_Code) & "'"
            objDal.GetSqlDataTable(dtChild, strSql)
            If dtChild.Rows.Count > 0 Then
                tblChild = New Table
                tblChild.HorizontalAlign = HorizontalAlign.Center
                tblChild.BorderStyle = BorderStyle.Solid
                tblChild.BorderWidth = System.Web.UI.WebControls.Unit.Pixel(1)
                tblChild.CellPadding = 2
                tblChild.CellSpacing = 0
                '*********Creating Self Record for same designation
                tblChildRow = New TableRow
                tblChildRow.BorderStyle = BorderStyle.None
                tblChildRow.BorderWidth = System.Web.UI.WebControls.Unit.Pixel(1)
                tblChildCell = New TableCell
                'tblChildCell.Width = System.Web.UI.WebControls.Unit.Pixel(100)
                tblChildCell.Height = System.Web.UI.WebControls.Unit.Pixel(100)
                tblChildCell.HorizontalAlign = HorizontalAlign.Center
                tblChildCell.VerticalAlign = VerticalAlign.Top
                tblChildCell.ColumnSpan = dtChild.Rows.Count
                tblChildCell.Text = "<A href=ORGCHART.ASPX?Dsg_Code=" & Dsg_Code & ">" & dsg_Name & "</A>"
                tblChildRow.Cells.Add(tblChildCell)
                tblChild.Rows.Add(tblChildRow)
                '*****************************************************
                tblChildRow = New TableRow
                tblChildRow.BorderStyle = BorderStyle.None
                tblChildRow.BorderWidth = System.Web.UI.WebControls.Unit.Pixel(1)
                For x = 0 To dtChild.Rows.Count - 1
                    tblChildCell = New TableCell
                    tblChildCell.Width = System.Web.UI.WebControls.Unit.Pixel(100)
                    tblChildCell.Height = System.Web.UI.WebControls.Unit.Pixel(100)
                    tblChildCell.BorderStyle = BorderStyle.None
                    tblChildCell.BorderWidth = System.Web.UI.WebControls.Unit.Parse(1)
                    tblChildCell.HorizontalAlign = HorizontalAlign.Center
                    tblChildCell.VerticalAlign = VerticalAlign.Top
                    tblChildCell.Text = "<A href=ORGCHART.ASPX?Dsg_Code=" & dtChild.Rows(x).Item("Dsg_Code") & ">" & dtChild.Rows(x).Item("Dsg_Name") & "</A>"
                    tblChildRow.Cells.Add(tblChildCell)
                    CreateChild(dtChild.Rows(x).Item("Dsg_Code"), dtChild.Rows(x).Item("Dsg_Name"), tblChildCell)
                Next
                tblChild.Rows.Add(tblChildRow)
                tblCell.Controls.Add(tblChild)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub


End Class
