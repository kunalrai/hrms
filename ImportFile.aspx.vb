Imports System.IO

Partial Class ImportFile
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

    Dim dtInputFile As New DataTable
    Dim i As Integer, j, k As Integer

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        'If IsNothing(Session("LoginUser")) Then
        'Response.Redirect("CompSel.aspx")
        'End If
        Try
            If Not IsPostBack Then
                Session("BalObj").FillCombo(CmbImport, "Select InputFileid, InputFileDesc From InputFileSetup ", True)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Private Sub CmdUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdUpload.Click
        Try
            LblErrMsg.Text = ""
            'Created By Mukesh 
            'As On 24-Nov-2006
            Viewstate("InvalidFile") = ""
            Dim Dt As New DataTable, StrSql As String
            Dim StrInsert As String, StrValues As String
            Dim StrQuery As String
            Dim StrExecute As String, strPreExec As String
            Dim FDate As Date, TDate As Date
            Dim dtMetaData As DataTable
            Dim DtInputOrder As New DataTable
            Dim k As Integer
            Dim StrFile As String
            Dim SR As StreamReader
            Dim StrSubPreExec As String
            Dim StrSubPostExec As String



            Dim i As Integer, j As Integer
            StrSql = "Select Distinct D.iOrder,I.InputFileId, I.ExecCommand, IsNull(I.PeriodFlag,0) as PeriodFlag, I.TmpTableName, I.PreExecCommand  From InputFileSetup I Inner Join InputFileDetail D On I.InputFileId = D.InputFileId Where I.InputFileId=" & ChkN(CmbImport.SelectedValue) & " "
            Session("DalObj").GetSqlDataTable(dtInputFile, StrSql)

            SR = New StreamReader(ImFile.PostedFile.InputStream)
            StrFile = SR.ReadToEnd
            If Len(StrFile) = 0 Then
                LblErrMsg.Text = "No data found"
                Exit Sub
            End If


            If dtInputFile.Rows.Count > 0 Then
                StrExecute = Chk(dtInputFile.Rows(0).Item("ExecCommand"))
                strPreExec = Chk(dtInputFile.Rows(0).Item("PreExecCommand"))
                If ChkN(dtInputFile.Rows(0).Item("PeriodFlag")) <> 0 Then
                    If (Not IsDate(TxtFrom.Text)) Or (Not IsDate(TxtTo.Text)) Then
                        LblErrMsg.Text = "Invalid Date ! Process failed"
                        Exit Sub
                    Else
                        LblErrMsg.Text = ""
                    End If
                End If
            End If
            If Trim(strPreExec) <> "" Then
                FDate = TxtFrom.Text
                TDate = TxtTo.Text

                strPreExec = Replace(strPreExec, "[FDATE]", "'" & Microsoft.VisualBasic.Format(FDate, "yyyy-MMM-dd") & "'")
                strPreExec = Replace(strPreExec, "[TDATE]", "'" & Microsoft.VisualBasic.Format(TDate, "yyyy-MMM-dd") & "'")
                Session("DalObj").ExecuteCommand(strPreExec, , DAL.DataLayer.ExecutionType.ExecuteNonQuery)
            End If

            ' MaxO = ChkN(Session("DalObj").ExecuteCommand("Select Count(distinct iOrder) from InputFileDetail Where iInputFileId=" & ChkN(CmbImport.SelectedValue), , DAL.DataLayer.ExecutionType.ExecuteScalar))

            ' Session("DalObj").GetSqlDataTable(dtInputFile, StrSql)

            '  For j = 1 To MaxO Step 1


            ' Next
            For k = 0 To dtInputFile.Rows.Count - 1 Step 1
                Dt = New DataTable
                DtInputOrder = New DataTable
                StrQuery = ""
                Session("DalObj").ExecuteCommand("Truncate Table " & Chk(dtInputFile.Rows(0).Item("TmpTableName")), , DAL.DataLayer.ExecutionType.ExecuteNonQuery)
                StrSql = "Select I.InputFileId, I.InputFileDesc, D.InitialString, I.CharType, I.FileType , I.TableName, D.ColumnNo, D.FieldName, D.TmpTableName, D.DataType, Isnull(D.PreExec,'') as PreExec, Isnull(D.PostExec,'') as PostExec, I.ExecCommand, IsNull(I.PeriodFlag,0) as PeriodFlag, I.PreExecCommand  From InputFileSetup I Inner Join InputFileDetail D On I.InputFileId = D.InputFileId Where I.InputFileId=" & ChkN(CmbImport.SelectedValue) & " And D.iOrder =" & ChkN(dtInputFile.Rows(k).Item("iOrder")) & " Order By D.ColumnNo"
                Session("DalObj").GetSqlDataTable(DtInputOrder, StrSql)
                Dt = ImportData(StrFile, Chk(DtInputOrder.Rows(0).Item("InitialString")), Chk(DtInputOrder.Rows(0).Item("CharType")))
                If Not Viewstate("InvalidFile") Is Nothing Then
                    If Viewstate("InvalidFile") = "False" Then Exit Sub
                End If

                StrSql = "Select  Max(Isnull(D.PreExec,'')) as PreExec From InputFileSetup I Inner Join InputFileDetail D On I.InputFileId = D.InputFileId Where I.InputFileId=" & ChkN(CmbImport.SelectedValue) & " And D.iOrder =" & ChkN(dtInputFile.Rows(k).Item("iOrder"))
                StrSubPreExec = Chk(Session("DalObj").ExecuteCommand(StrSql, , DAL.DataLayer.ExecutionType.ExecuteScalar))
                If StrSubPreExec <> "" Then
                    Session("DalObj").ExecuteCommand(StrSubPreExec, , DAL.DataLayer.ExecutionType.ExecuteNonQuery)
                End If

                For i = 0 To Dt.Rows.Count - 1 Step 1
                    StrInsert = " Insert Into " & Chk(DtInputOrder.Rows(0).Item("TmpTableName")) & " ("
                    StrValues = " Values ("
                    For j = 0 To DtInputOrder.Rows.Count - 1 Step 1
                        StrInsert = StrInsert & Chk(DtInputOrder.Rows(j).Item("FieldName")) & ", "
                        StrValues = StrValues & "'" & Replace(Chk(Dt.Rows(i).Item(CInt(ChkN(DtInputOrder.Rows(j).Item("ColumnNo"))))), "'", "~") & "', "
                    Next
                    StrInsert = Mid(StrInsert, 1, Len(StrInsert) - 2)
                    StrInsert = StrInsert & ")"
                    StrValues = Mid(StrValues, 1, Len(StrValues) - 2)
                    StrValues = StrValues & ")"
                    StrQuery = StrQuery & " " & vbCrLf & StrInsert & " " & StrValues
                Next
                If StrQuery <> "" Then
                    Session("DalObj").ExecuteCommand(StrQuery, , DAL.DataLayer.ExecutionType.ExecuteNonQuery)
                End If

                StrSql = "Select  Max(Isnull(D.PostExec,'')) as PostExec From InputFileSetup I Inner Join InputFileDetail D On I.InputFileId = D.InputFileId Where I.InputFileId=" & ChkN(CmbImport.SelectedValue) & " And D.iOrder =" & ChkN(dtInputFile.Rows(k).Item("iOrder"))
                StrSubPostExec = Chk(Session("DalObj").ExecuteCommand(StrSql, , DAL.DataLayer.ExecutionType.ExecuteScalar))
                If StrSubPostExec <> "" Then
                    Session("DalObj").ExecuteCommand(StrSubPostExec, , DAL.DataLayer.ExecutionType.ExecuteNonQuery)
                End If
            Next

            If Trim(StrExecute) <> "" Then
                Session("DalObj").ExecuteCommand(StrExecute, , DAL.DataLayer.ExecutionType.ExecuteNonQuery)
            End If


            LblErrMsg.Text = "Process Completed Successfully"

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Function ImportData(ByVal StrFile As String, Optional ByVal InitialStr As String = "", Optional ByVal SplitChar As String = "", Optional ByVal StartCol As Int32 = 0, Optional ByVal EndCol As Int32 = 0) As DataTable
        Try
            'Created By Mukesh 
            'As On 24-Nov-2006
            'This function is creared for upload from Peoplesoft for schneider
            'This function will accept a file name & return data in datatable
            StrFile = Replace(StrFile, "'", "~")

            Dim StrRows() As String
            Dim Dt As New DataTable, Dr As DataRow, Dc As DataColumn
            Dim SchUpd As Boolean = False
            Dim StrCols() As String
            StrRows = Split(StrFile, ControlChars.Lf)
            For i = 1 To StrRows.Length - 1 Step 1
                If InStr(StrRows(i), InitialStr) > 0 Then
                    If SchUpd = False Then
                        StrCols = GetValueArray(Chk(StrRows(i - 1)), SplitChar)

                        ViewState("HCols") = ChkN(StrCols.Length)
                        For j = 0 To StrCols.Length - 1 Step 1
                            Dc = New DataColumn("Col" & j, GetType(String))
                            Dt.Columns.Add(Dc)
                        Next
                        SchUpd = True
                    End If
                    StrCols = GetValueArray(Chk(StrRows(i)), SplitChar)
                    If Len(Trim(Chk(StrRows(i)))) > 0 Then
                        Dr = Dt.NewRow()
                        ViewState("VCols") = ChkN(StrCols.Length)
                        For j = 0 To StrCols.Length - 2 Step 1
                            Dr.Item(j) = StrCols(j)
                        Next
                        Dt.Rows.Add(Dr)
                    End If
                End If
            Next
            If ChkN(ViewState("HCols")) > ChkN(ViewState("VCols")) Then
                Viewstate("InvalidFile") = "False"
                Response.Write(" There is problem in Row No :" & i & " Header columns are more than value columns.")
            End If
            Return Dt
        Catch ex As Exception
            If ChkN(ViewState("HCols")) < ChkN(ViewState("VCols")) Then
                Response.Write(ex.Message & " There is problem in Row No :" & i & " Header columns are less than value columns.")
                Viewstate("InvalidFile") = "False"
            ElseIf ChkN(ViewState("HCols")) > ChkN(ViewState("VCols")) Then
                Viewstate("InvalidFile") = "False"
                Response.Write(ex.Message & " There is problem in Row No :" & i & " Header columns are more than value columns.")
            End If
        End Try
    End Function

    Private Function GetValueArray(ByVal Param As String, ByVal SplitChar As String) As String()
        Try
            Dim StrCols() As String
            If SplitChar = "TAB" Then
                StrCols = Split(Param, vbTab)
                If StrCols.Length <= 1 Then
                    LblErrMsg.Text = "File is empty or not a (TAB) seperated file."
                    Viewstate("InvalidFile") = "False"
                End If
            Else
                StrCols = Split(Param, SplitChar)
                If StrCols.Length <= 1 Then
                    LblErrMsg.Text = "File is empty or not a (" & Chk(SplitChar) & ") seperated file."
                    Viewstate("InvalidFile") = "False"
                End If
            End If
            Return StrCols
        Catch ex As Exception

        End Try

    End Function


    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

    End Sub

    Private Sub CmbImport_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbImport.SelectedIndexChanged
        Try
            Dim StrSql As String
            Dim prdFlag As Int16
            StrSql = "Select IsNull(PeriodFlag,0) From InputFileSetup I  Where I.InputFileId=" & ChkN(CmbImport.SelectedValue)
            prdFlag = ChkN(Session("DalObj").ExecuteCommand(StrSql, , DAL.DataLayer.ExecutionType.ExecuteScalar))
            If prdFlag <> 0 Then
                TxtFrom.Enabled = True
                TxtTo.Enabled = True
            Else
                TxtFrom.Enabled = False
                TxtTo.Enabled = False

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub TxtFrom_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtFrom.TextChanged

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class
