Imports System.Data.SqlClient
Public Class GRIDTEST
    Inherits System.Web.UI.Page


    Protected WithEvents DropDownList1 As System.Web.UI.WebControls.DropDownList
    Dim oBAL As BAL.BLayer
    Dim oDAL As New DAL.DataLayer
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents DataGrid1 As System.Web.UI.WebControls.DataGrid

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
        oDAL = Session("DALObj")
        oBAL = Session("BALObj")
        Dim ds As New DataSet
        Dim str As String = "HSEIH0002"


        '  oDAL.GetSqlDataTable(dt, "Select top 5 Loc_Code,Loc_Name From HrdMastQry")

        oDAL.GetSqlDataSet(ds, "exec sp_sel_HrdMastQry '" & str & " ' ")
        DataGrid1.DataSource = ds.Tables(0)
        DataGrid1.DataBind()

        DropDownList1.DataSource = ds.Tables(1)
        DropDownList1.DataValueField = "EMP_CODE"
        DropDownList1.DataTextField = "EMP_CODE"
        DropDownList1.DataBind()

       

    End Sub

End Class
