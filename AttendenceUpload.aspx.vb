Imports System.IO
Partial Class AttendenceUpload
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
    'Dim Dt As New DataTable
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If
        If Not IsPostBack Then
            ViewState("Dt") = New DataTable
        End If
    End Sub

    Private Sub CmdUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdUpload.Click
        Try
            Dim FileName As String
            Dim StrFile As String
            Dim inStream As Stream
            Dim SR As StreamReader
            Dim i As Integer, j, k As Integer
            Dim StrRows() As String, StrCols() As String
            Dim Dr As DataRow
            Dim Dc As DataColumn

            inStream = ImFile.PostedFile.InputStream
            SR = New StreamReader(inStream)
            StrFile = SR.ReadToEnd
            StrRows = Split(StrFile, vbNewLine)
            StrCols = Split(Chk(StrRows(0)), vbTab)


            For j = 0 To StrCols.Length - 1 Step 1
                Dc = New DataColumn("col" & j, GetType(String))
                ViewState("Dt").Columns.Add(Dc)
            Next
            For i = 0 To StrRows.Length - 1 Step 1
                StrCols = Split(Chk(StrRows(i)), vbTab)
                Dr = ViewState("Dt").NewRow()
                For j = 0 To StrCols.Length - 1 Step 1
                    Dr.Item(j) = StrCols(j)
                Next
                ViewState("Dt").Rows.Add(Dr)
            Next



            Dim dg As New DataGrid
            Panel1.Controls.Add(dg)
            dg.DataSource = ViewState("Dt")
            dg.DataBind()


        Catch ex As Exception
            LblErrMsg.Text = ex.Message
        Finally

        End Try
    End Sub
    Private Sub Page_Error(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Error
        Try
            Response.Redirect("AttendenceUpload.aspx")
        Catch ex As Exception
            SetMsg(LblErrMsg, "Page_Error" & vbCrLf & ex.Source & vbCrLf & ex.Message)
        End Try
    End Sub

#Region "    Save Records    "
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            Dim SqlStr As String = ""
            Dim values As String = ""
            Dim i, j As Integer
            For i = 1 To ViewState("Dt").Rows.Count - 2
                values = ""
                For j = 0 To ViewState("Dt").Columns.Count - 2
                    values = values & "','" & ViewState("Dt").Rows(i).Item(j) & ""
                Next
                SqlStr = SqlStr & " Insert into LEAVTRAN ( LEVYEAR,EMP_CODE,AtDate,LVDAYS,LVTYPE ,In_Time ,Out_Time ) Values ('2006" & values & "' ) "
            Next
            Session("DalObj").ExecuteCommand(SqlStr)
            SetMsg(LblErrMsg, "Record successfully saved")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
#End Region

End Class
