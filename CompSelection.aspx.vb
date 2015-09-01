Imports System.Data.OleDb
Imports System.Data.SqlClient
Partial Class CompSel
    Inherits System.Web.UI.Page
    Public var As String


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label
    Protected WithEvents trmsg As System.Web.UI.HtmlControls.HtmlTableRow
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
        Try
            If Server.GetLastError Is Nothing Then SetMsg(LblMsg, "")
            If Not IsNothing(Session("LogOutMsg")) Then Session("LogOutMsg") = Nothing
            If Not IsPostBack Then

                FillCompList()
            End If
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message)
        End Try
    End Sub

    Private Sub CmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If IsNothing(LstComp.SelectedItem) Then
            SetMsg(LblMsg, "Select a company to continue.")
            Exit Sub
        End If
        If LstComp.SelectedItem.Text = "" Then
            SetMsg(LblMsg, "Select a company to continue.")
            Exit Sub
        End If
        Try

            SetLoginUser()
            If Not IsNothing(Session("DalObj")) Then Session("DalObj") = Nothing

            Session("DalObj") = New DAL.DataLayer
            Session("DalObj").SqlConnectionString = Session("LoginUser").ConnectString
            Session("DalObj").OpenConnection(DalObj.ConnProvider.SQL)
            Response.Redirect("Login.aspx")

        Catch ex As Exception
            SetMsg(LblMsg, ex.Message)
        End Try
    End Sub
    Private Sub LstComp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LstComp.SelectedIndexChanged
        CmdOk_Click(sender, e)
    End Sub

    Private Sub FillCompList()
        Dim cnSys As New OleDbConnection
        Dim myAdapter As New OleDbDataAdapter
        Try
            Dim strPath As String = HttpContext.Current.Request.ServerVariables("APPL_PHYSICAL_PATH")
            MiracleString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strPath & "\SYS.MDB;User ID=Admin;Jet OLEDB:Database Password=DIT683"
            cnSys.ConnectionString = MiracleString
            If cnSys.State = ConnectionState.Open Then cnSys.Close()
            cnSys.Open()
            If cnSys.State = ConnectionState.Open Then
                Dim dsSetUp As New DataSet
                Dim dsSetUp2 As New DataSet

                myAdapter.SelectCommand = New OleDbCommand("Select uCase(SETUP.COMP_NAME) As Company,Comp_Code As [Code] From SetUp ORDER BY SetUp.Comp_Code", cnSys)
                myAdapter.Fill(dsSetUp)

                If dsSetUp.Tables(0).Rows.Count > 0 Then
                    LstComp.DataSource = dsSetUp
                    LstComp.DataTextField = "Company"
                    LstComp.DataValueField = "Code"
                    LstComp.DataBind()
                Else
                    SetMsg(LblMsg, "No Company Name Found in the Database.")
                End If
            End If
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message)
        Finally
            myAdapter.Dispose()
            If cnSys.State = ConnectionState.Open Then cnSys.Close()
            cnSys.Dispose()
        End Try
    End Sub

    Private Sub SetLoginUser()
        Dim cnSys As New OleDbConnection
        Try
            cnSys.ConnectionString = MiracleString
            If cnSys.State = ConnectionState.Open Then cnSys.Close()
            cnSys.Open()
            If cnSys.State = ConnectionState.Open Then

                Dim adptSys As New OleDbDataAdapter
                Dim DTSys As New DataTable

                adptSys.SelectCommand = New OleDbCommand("Select * From SetUP", cnSys)
                adptSys.Fill(DTSys)

                Dim dvCompany As New DataView(DTSys, " Comp_Code = '" & Chk(LstComp.SelectedValue) & "'", "Comp_Code", DataViewRowState.CurrentRows)
                If dvCompany.Count > 0 Then
                    Dim LoginUser As DAL.DataLayer.Users
                    LoginUser.CurrentCompID = Chk(dvCompany.Item(0).Item("COMP_CODE"))
                    LoginUser.CurrentCompanyName = Chk(dvCompany.Item(0).Item("COMP_NAME"))
                    LoginUser.DBName = Chk(dvCompany.Item(0).Item("DATABASENAME"))
                    LoginUser.DBType = Chk(dvCompany.Item(0).Item("DATABASETYPE"))
                    LoginUser.DSNName = Chk(dvCompany.Item(0).Item("DSN"))
                    LoginUser.ConnectString = Chk(dvCompany.Item(0).Item("CONNECTSTRING"))
                    LoginUser.ReportFolder = Chk(dvCompany.Item(0).Item("ReportFolder"))
                    LoginUser.ShowError = IIf(dvCompany.Item(0).Item("ShowError") = 1, True, False)
                    LoginUser.DBPassword = Chk(dvCompany.Item(0).Item("USERPASSWORD"))
                    LoginUser.DBUser = Chk(dvCompany.Item(0).Item("UserID"))
                    LoginUser.UserID = ""
                    LoginUser.Password = ""
                    LoginUser.UserGroup = ""
                    Session("LoginUser") = LoginUser
                    LoginUser = Nothing
                End If
            End If
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message)
        Finally
            If cnSys.State = ConnectionState.Open Then cnSys.Close()
            cnSys.Dispose()
        End Try
    End Sub

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub

End Class

