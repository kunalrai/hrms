Partial Class RepChnl
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection

    End Sub
    Protected WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection

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
        If Request.QueryString.Count > 0 Then
            If Request.QueryString.Item("Emp_Code") = "" Then
                strSql = "SELECT Emp_Code,Emp_Name,Mngr_Code,Mngr_Name,Dsg_Name,Dept_Name,Grd_Name FROM HrdMastQry WHERE Mngr_Code = '*'"
            Else
                strSql = "SELECT Emp_Code,Emp_Name,Mngr_Code,Mngr_Name,Dsg_Name,Dept_Name,Grd_Name FROM HrdMastQry WHERE UPPER(Emp_Code) = '" & Request.QueryString.Item("Emp_Code").ToUpper & "'"
            End If
        Else
            strSql = "SELECT Emp_Code,Emp_Name,Mngr_Code,Mngr_Name,Dsg_Name,Dept_Name,Grd_Name FROM HrdMastQry WHERE Mngr_Code = '*'"
        End If
        objDal.GetSqlDataTable(dtOrg, strSql)
        tblRow = New TableRow
        For i = 0 To dtOrg.Rows.Count - 1
            tblCell = New TableCell
            tblCell.Height = System.Web.UI.WebControls.Unit.Pixel(100)
            tblCell.HorizontalAlign = HorizontalAlign.Center
            tblCell.VerticalAlign = VerticalAlign.Top
            tblCell.Text = "<A href=RepChnl.ASPX?Emp_Code=" & dtOrg.Rows(i).Item("Emp_Code") & ">" & dtOrg.Rows(i).Item("Emp_Name") & "</A>" & "<BR>" & dtOrg.Rows(i).Item("Dsg_Name") & "<BR>" & dtOrg.Rows(i).Item("Dept_Name") & "<BR>" & dtOrg.Rows(i).Item("Grd_Name")
            CreateChild(dtOrg.Rows(i).Item("Emp_Code"), dtOrg.Rows(i).Item("Emp_Name"), tblCell)
            tblRow.Cells.Add(tblCell)
        Next
        tblOrg.Rows.Add(tblRow)
    End Sub

    Private Sub CreateChild(ByVal Emp_Code As String, ByVal Emp_Name As String, ByRef tblCell As TableCell)
        Try
            Dim x As Int16
            Dim dtChild As New DataTable
            Dim tblChildRow As TableRow
            Dim tblChildCell As TableCell
            Dim tblChild As Table
            strSql = "SELECT Emp_Code,Emp_Name,Mngr_Code,Mngr_Name,Dsg_Name,Dept_Name,Grd_Name FROM HrdMastQry WHERE Mngr_Code = '" & Trim(Emp_Code) & "'"
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
                tblChildCell.Text = "<A href=RepChnl.ASPX?Emp_Code=" & Emp_Code & ">" & Emp_Name & "</A>"
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
                    tblChildCell.BorderStyle = BorderStyle.Solid
                    tblChildCell.BorderWidth = System.Web.UI.WebControls.Unit.Parse(1)
                    tblChildCell.HorizontalAlign = HorizontalAlign.Center
                    tblChildCell.VerticalAlign = VerticalAlign.Top
                    tblChildCell.Text = "<A href=RepChnl.ASPX?Emp_Code=" & dtChild.Rows(x).Item("Emp_Code") & ">" & dtChild.Rows(x).Item("Emp_Name") & "</A>" & "<BR>" & dtChild.Rows(x).Item("Dsg_Name") & "<BR>" & dtChild.Rows(x).Item("Dept_Name") & "<BR>" & dtChild.Rows(x).Item("Grd_Name")
                    tblChildRow.Cells.Add(tblChildCell)
                    CreateChild(dtChild.Rows(x).Item("Emp_Code"), dtChild.Rows(x).Item("Emp_Name"), tblChildCell)
                Next
                tblChild.Rows.Add(tblChildRow)
                tblCell.Controls.Add(tblChild)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub



End Class
