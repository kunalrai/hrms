Imports System.Runtime.InteropServices
Imports System.Web.UI.Page

Partial Class RajTest
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ds As New DataSet
    Dim DAL As DAL.DataLayer
    Dim BAL As BAL.BLayer
    Dim StrSql As String

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
        DAL = Session("DALObj")
        BAL = Session("BALObj")
        'Put user code to initialize the page here
        Dim objSCM As New SCM.Payroll(DAL)
        LblErrMsg.Text = objSCM.CheckFox()
        'LblErrMsg.Text = objSCM.ErrMessage
        'Dim objSCM As Object
        'objSCM = CreateObject("SCM.Payroll")

        'If Not IsPostBack Then
        'FillDataGrid()
        'End If
    End Sub
    Private Function FillDataGrid()
        Try
            Dim dtTable As New DataTable
            Dim StrSql As String
            Dim str As String
            Dim Cnt As Integer
            StrSql = "select field_name, FIELD_DESC, SNo, Fld_Categ =(CASE Fld_Categ  WHEN '8' THEN 'Others' WHEN '1' THEN 'Earnings' WHEN '2' THEN 'Deductions' WHEN '3' THEN 'Loan & Advances' WHEN '4' THEN 'Reimbursments' WHEN '5' THEN 'Perquisities' WHEN '6' THEN 'Investments' WHEN '7' THEN 'Taxable'  END) from PaySetUp order By SNO"
            DAL.GetSqlDataTable(dtTable, StrSql)
            If dtTable.Rows.Count > 0 Then
                GrdSetUp.DataSource = dtTable
                GrdSetUp.DataBind()
            End If
        Catch ex As Exception
            LblErrMsg.Text = ex.Message
        End Try
    End Function
    Private Sub GrdSetUp_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles GrdSetUp.EditCommand
        'TextBox2.Text = Chk(e.Item.Cells(3).Text)
        Dim Tmp, srno As String
        Tmp = " <SCRIPT language=javascript >window.open ('sendmail.aspx?SrNo=" & e.Item.Cells(2).Text & "', '')</SCRIPT>"


        RegisterStartupScript("Rajeev", Tmp)


    End Sub
    '''''''''Private Sub GrdSetUp_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles GrdSetUp.Init
    '''''''''    Try
    '''''''''        Dim cmdSelect As New ButtonColumn
    '''''''''        cmdSelect.ButtonType = ButtonColumnType.LinkButton
    '''''''''        cmdSelect.Text = "Select"
    '''''''''        cmdSelect.HeaderText = "Select"
    '''''''''        cmdSelect.CommandName = "Edit"
    '''''''''        GrdSetUp.Columns.Add(cmdSelect)
    '''''''''    Catch ex As Exception
    '''''''''        LblErrMsg.Text = ex.Message & " " & ex.Source
    '''''''''    End Try
    '''''''''End Sub



    Private Sub GrdSetUp_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles GrdSetUp.ItemCommand
        If e.CommandName = "Edit" Then
            TextBox2.Text = Chk(e.Item.Cells(3).Text)
            'CType(e.Item.FindControl("Select"), LinkButton).Attributes.Add("onclick", "return PopUp(); ")
            'CType(e.Item.FindControl("Edit"), LinkButton).Attributes.Add("onclick", "return OpenWindow(); ")
        End If
    End Sub

    Private Sub Page_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Disposed

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class
