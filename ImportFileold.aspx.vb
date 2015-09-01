Imports System.IO
Public Class ImportFile
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents LblErrMsg As System.Web.UI.WebControls.Label
    Protected WithEvents cmdSave As System.Web.UI.WebControls.Button
    Protected WithEvents cmdClose As System.Web.UI.WebControls.Button
    Protected WithEvents Table1 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents CmdUpload As System.Web.UI.WebControls.Button
    Protected WithEvents ImFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents CmbImport As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim dtInputFile As DataTable

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        'If IsNothing(Session("LoginUser")) Then
        'Response.Redirect("CompSel.aspx")
        'End If
        If Not IsPostBack Then
            Session("BalObj").FillCombo(CmbImport, "Select InputFileld, InputFileDesc From InputFileSetup ", True)
        End If



    End Sub


    Private Sub CmdUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdUpload.Click
        Try
            Dim Dt As DataTable, StrSql As String
            Dim StrInsert As String, StrValues As String
            Dim StrQuery As String

            Dim i As Integer, j As Integer

            StrSql = "Select I.InputFileld, I.InputFileDesc, I.CharType, I.FileType , I.TableName, D.ColumnNo, D.FieldName, D.DataType  From InputFileSetup I Inner Join InputFileDetail D On I.InputFileld = D.InputFileld Where I.InputFileld=" & ChkN(CmbImport.SelectedValue)
            Session("DalObj").GetSqlDataTable(dtInputFile, StrSql)

            Dt = ImportData(ImFile.PostedFile.InputStream)

            For i = 0 To Dt.Rows.Count - 1 Step 1
                StrInsert = " Insert Into " & Chk(dtInputFile.Rows(0).Item("TmpFileName")) & " ("
                StrValues = " Values ("
                For j = 0 To dtInputFile.Rows.Count - 1 Step 1
                    StrInsert = StrInsert & Chk(dtInputFile.Rows(j).Item("FieldName")) & ", "
                    StrValues = StrValues & ChkN(Dt.Rows(i).Item(ChkN(dtInputFile.Rows(j).Item("ColumnNo")))) & ", "
                Next
                StrInsert = Mid(StrInsert, 1, Len(StrInsert) - 2)
                StrInsert = StrInsert & ")"
                StrValues = Mid(StrValues, 1, Len(StrInsert) - 2)
                StrValues = StrValues & ")"
                StrQuery = StrInsert & " " & StrValues
                Response.Write(StrQuery)
            Next


            Dim dg As New DataGrid
            Panel1.Controls.Add(dg)
            dg.DataSource = Dt
            dg.DataBind()

        Catch ex As Exception

            Response.Write(ex.Message)
        End Try
    End Sub

    Private Function ImportData(ByVal FileName As Stream, Optional ByVal StartCol As Int32 = 0, Optional ByVal EndCol As Int32 = 0) As DataTable
        Try
            'Created By Mukesh 
            'As On 24-Nov-2006
            'This function is creared for upload from Peoplesoft for schneider
            'This function will accept a file name & return data in datatable

            Dim StrFile As String
            Dim inStream As Stream
            Dim SR As StreamReader
            Dim i As Integer, j, k As Integer
            Dim StrRows() As String, StrCols() As String
            Dim Dt As New DataTable
            Dim Dr As DataRow
            Dim Dc As DataColumn


            SR = New StreamReader(inStream)
            StrFile = SR.ReadToEnd
            StrRows = Split(StrFile, vbNewLine)
            StrCols = Split(Chk(StrRows(0)), vbTab)

            For j = 0 To StrCols.Length - 1 Step 1
                Dc = New DataColumn(StrCols(j), GetType(String))
                Dt.Columns.Add(Dc)
            Next
            For i = 1 To StrRows.Length - 1 Step 1
                StrCols = Split(Chk(StrRows(i)), vbTab)
                Dr = Dt.NewRow()
                For j = 0 To StrCols.Length - 1 Step 1
                    Dr.Item(j) = StrCols(j)
                Next
                Dt.Rows.Add(Dr)
            Next
            Return Dt
        Catch ex As Exception

        End Try
    End Function

End Class
