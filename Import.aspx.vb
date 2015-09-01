Imports System.IO
Partial Class Import
    Inherits System.Web.UI.Page
    Dim oDAL As DAL.DataLayer
    Dim oBAL As BAL.BLayer
    Dim strSql As String
    Dim dtData As DataTable
    Dim dtFormat As New DataTable
    Dim bEOM As Boolean
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
            oDAL = Session("DalObj")
            oBAL = Session("BalObj")
            If Not IsPostBack Then
                DtpFDate.Text = Format(Date.Today, "dd/MMM/yyyy")
                DtpTDate.Text = Format(Date.Today, "dd/MMM/yyyy")

                strSql = "SELECT Fmt_ID,Fmt_Desc FROM ImpType ORDER BY Fmt_Desc"
                oBAL.FillCombo(cmbFormat, strSql, True)
            End If


        Catch ex As Exception
            SetMsg(LblErrMsg, "Page_Load" & vbCrLf & ex.Source & vbCrLf & ex.Message)
        Finally

        End Try
    End Sub

    Private Sub cmdImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdImport.Click

        Dim sFileName As String
        Try
            If Trim(flImport.Value) <> "" Then
                ViewState("FileName") = flImport.Value
            End If

            If Trim(ViewState("FileName")) = "" Then
                SetMsg(LblErrMsg, "Please Select The File to set.")
                Exit Sub
            End If



            'Dim strFormat As String = Right(UCase(flImport.PostedFile.FileName), flImport.PostedFile.FileName.Length - flImport.PostedFile.FileName.LastIndexOf("."))

            Dim strFormat As String = Right(UCase(ViewState("FileName")), ViewState("FileName").Length - ViewState("FileName").LastIndexOf("."))

            If InStr(".XLS", UCase(strFormat)) = 0 Then
                SetMsg(LblErrMsg, "Invalid Format of File.")
                Exit Sub
            End If
            Dim strPath As String = HttpContext.Current.Request.ServerVariables("APPL_PHYSICAL_PATH")
            If Right(Trim(strPath), 1) = "\" Or Right(Trim(strPath), 1) = "/" Then
                strPath = Mid(strPath, 1, strPath.Length - 1)
            End If
            If viewstate("bEOM") Then
                DtpFDate.Text = Format(EOM(CType(DtpFDate.Text, Date)), "dd/MMM/yyyy")
                DtpTDate.Text = Format(EOM(CType(DtpTDate.Text, Date)), "dd/MMM/yyyy")
            End If
            sFileName = strPath & "\Upload\" & Format(Now(), "yyyyMMddhhmmss") & strFormat
            flImport.PostedFile.SaveAs(sFileName)
            ViewState("dtData") = New DataTable

            strSql = "SELECT * FROM ImpFormat WHERE FMT_ID=" & ChkN(cmbFormat.SelectedValue)
            oDAL.GetSqlDataTable(dtFormat, strSql)

            GetDataStruct(cmbFormat.SelectedValue, ViewState("dtData"))
            ImportFile(sFileName)

            If Not sender Is Nothing Then
                cmbFormat.SelectedValue = Nothing
                txtSheetName.Text = ""


            End If
            SetMsg(LblErrMsg, "Process completed successfully")
        Catch ex As Exception
            SetMsg(LblErrMsg, "CmdImport_Click" & vbCrLf & ex.Source & vbCrLf & ex.Message)
        End Try

    End Sub
    Private Sub ImportFile(ByVal strFileName As String)
        Dim xlCon As New System.Data.OleDb.OleDbConnection
        Dim strXlCon As String
        Dim dtXL As New DataTable, daXl As OleDb.OleDbDataAdapter
        Dim dr As DataRow
        Dim dt As DataTable
        Try
            strXlCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strFileName & ";Extended Properties=""Excel 8.0;HDR=YES;"""
            xlCon.ConnectionString = strXlCon
            xlCon.Open()
            daXl = New OleDb.OleDbDataAdapter("SELECT * FROM [" & Chk(txtSheetName.Text) & "$]", xlCon)
            daXl.Fill(dtXL)
            dt = viewstate("dtData")
            For j As Int32 = 0 To dtXL.Rows.Count - 1
                dr = dt.NewRow
                For i As Int16 = 0 To dtFormat.Rows.Count - 1
                    If dtFormat.Rows(i).Item("ColNo") <> 0 Then
                        Select Case dtFormat.Rows(i).Item("Field_Type")
                            Case "C"
                                dr.Item(dtFormat.Rows(i).Item("Field_Name")) = Mid(dtXL.Rows(j).Item(dtFormat.Rows(i).Item("ColNo") - 1).ToString, 1, dtFormat.Rows(i).Item("Field_Len"))
                            Case "N"
                                dr.Item(dtFormat.Rows(i).Item("Field_Name")) = ChkN(dtXL.Rows(j).Item(dtFormat.Rows(i).Item("ColNo") - 1))
                            Case "D", "T"
                                If IsDate(dtXL.Rows(j).Item(dtFormat.Rows(i).Item("ColNo") - 1)) Then
                                    dr.Item(dtFormat.Rows(i).Item("Field_Name")) = CType(dtXL.Rows(j).Item(dtFormat.Rows(i).Item("ColNo") - 1), Date)
                                Else
                                    dr.Item(dtFormat.Rows(i).Item("Field_Name")) = DBNull.Value
                                End If
                        End Select
                    Else
                        dr.Item(dtFormat.Rows(i).Item("Field_Name")) = GetVarValue(dtFormat.Rows(i).Item("Field_Name"))
                    End If
                Next
                'dr.AcceptChanges()
                dr.Item("RecordStatus") = "Ready To Import"
                dt.Rows.Add(dr)
            Next
            Session("dtData") = ViewState("dtData")
            'grdImport.DataSource = ViewState("dtData")

            'grdImport.DataBind()
        Catch ex As Exception
            SetMsg(LblErrMsg, "Page_Load" & vbCrLf & ex.Source & vbCrLf & ex.Message)
        Finally
            daXl.Dispose()
            xlCon.Close()
            xlCon.Dispose()
        End Try
    End Sub
    Private Sub GetDataStruct(ByVal fmtID As Int16, ByVal dtData As DataTable)
        Dim dc As DataColumn
        Dim sdt As System.Type
        Try
            For i As Int16 = 0 To dtFormat.Rows.Count - 1
                Select Case dtFormat.Rows(i).Item("Field_Type")
                    Case "C"
                        sdt = System.Type.GetType("System.String")
                    Case "N"
                        sdt = System.Type.GetType("System.Double")
                    Case "D", "T"
                        sdt = System.Type.GetType("System.DateTime")
                End Select
                dc = New DataColumn(dtFormat.Rows(i).Item("Field_Name"), sdt)
                If sdt Is System.Type.GetType("System.String") Then
                    dc.MaxLength = dtFormat.Rows(i).Item("Field_Len")
                End If
                dtData.Columns.Add(dc)
            Next
            dc = New DataColumn("RecordStatus", System.Type.GetType("System.String"))
            dc.MaxLength = 255
            CType(ViewState("dtData"), DataTable).Columns.Add(dc)
        Catch ex As Exception
            SetMsg(LblErrMsg, "GetDataStruct" & vbCrLf & ex.Source & vbCrLf & ex.Message)
        Finally
        End Try
    End Sub
    Private Sub UploadData()
        Dim dtImpType As New DataTable
        Dim dtImpFormat As New DataTable
        Dim dt As DataTable
        Dim sSql1, sSql2, sSqlUpdate1, sSqlUpdate2, sWhere, strIns, strUpd As String
        Dim i, RecCnt As Int32
        Try
            dt = viewstate("dtData")
            If IsNothing(dt) Then Exit Sub
            If dt.Rows.Count <= 0 Then Exit Sub
            strSql = "SELECT * FROM ImpType WHERE Fmt_ID=" & ChkN(cmbFormat.SelectedValue)
            oDAL.GetSqlDataTable(dtImpType, strSql)
            strSql = "SELECT * FROM ImpFormat WHERE Fmt_ID=" & ChkN(cmbFormat.SelectedValue) & " Order By Srno"
            oDAL.GetSqlDataTable(dtImpFormat, strSql)
            If dtImpType.Rows.Count < 1 Then Exit Sub
            If ChkN(dtImpType.Rows(0).Item("ImportMode")) = 1 Then
                If InStr(Chk(dtImpType.Rows(0).Item("Fmt_Table"), True, True), "TMP", CompareMethod.Text) > 0 Or InStr(Chk(dtImpType.Rows(0).Item("Fmt_Table"), True, True), "IMP", CompareMethod.Text) > 0 Then
                    strSql = "DELETE FROM " & dtImpType.Rows(0).Item("Fmt_Table")
                    oDAL.ExecuteCommand(strSql)
                    For i = 0 To dt.Rows.Count - 1
                        sSql1 = "INSERT INTO " & dtImpType.Rows(0).Item("Fmt_Table") & "("
                        sSql2 = "VALUES("
                        For j As Int16 = 0 To dtImpFormat.Rows.Count - 1
                            'If dtImpFormat.Rows(j).Item("Update_YN") Then
                            sSql1 += dtImpFormat.Rows(j).Item("Field_Name") & ","
                            Select Case dtImpFormat.Rows(j).Item("Field_Type")
                                Case "C"
                                    sSql2 += "'" & Chk(dt.Rows(i).Item(dtImpFormat.Rows(j).Item("Field_Name")), False, True, True) & "',"
                                Case "N"
                                    sSql2 += ChkN(dt.Rows(i).Item(dtImpFormat.Rows(j).Item("Field_Name"))) & ","
                                Case "D"
                                    If Not IsDBNull(dt.Rows(i).Item(dtImpFormat.Rows(j).Item("Field_Name"))) Then
                                        sSql2 += "'" & Format(dt.Rows(i).Item(dtImpFormat.Rows(j).Item("Field_Name")), "dd/MMM/yyyy") & "',"
                                    Else
                                        sSql2 += "NULL,"
                                    End If
                                Case "T"
                                    If Not IsDBNull(dt.Rows(i).Item(dtImpFormat.Rows(j).Item("Field_Name"))) Then
                                        sSql2 += "'" & Format(dt.Rows(i).Item(dtImpFormat.Rows(j).Item("Field_Name")), "dd/MMM/yyyy HH:MM") & "',"
                                    Else
                                        sSql2 += "NULL,"
                                    End If
                            End Select
                            'End If
                        Next
                        strIns += Mid(sSql1, 1, sSql1.Length - 1) & ") " & Mid(sSql2, 1, sSql2.Length - 1) & ")"
                    Next
                    oDAL.ExecuteCommand(strIns)
                    SetMsg(LblErrMsg, i.ToString & " Records Sucessfully Inserted.")
                Else
                    SetMsg(LblErrMsg, "Temporary Table Name not in Correct Format.")
                End If
            ElseIf ChkN(dtImpType.Rows(0).Item("ImportMode")) = 2 Then
                strIns = ""
                For i = 0 To dt.Rows.Count - 1
                    sSql1 = "INSERT INTO " & dtImpType.Rows(0).Item("Fmt_Table") & "("
                    sSql2 = "VALUES("
                    sSqlUpdate1 = "UPDATE " & dtImpType.Rows(0).Item("Fmt_Table") & " SET "
                    sWhere = ""
                    For j As Int16 = 0 To dtImpFormat.Rows.Count - 1
                        sSql1 += dtImpFormat.Rows(j).Item("Native_Col") & ","
                        Select Case dtImpFormat.Rows(j).Item("Field_Type")
                            Case "C"
                                sSql2 += "'" & dt.Rows(i).Item(dtImpFormat.Rows(j).Item("Field_Name")) & "',"
                                If dtImpFormat.Rows(j).Item("Update_YN") And Not dtImpFormat.Rows(j).Item("UniqueID") Then sSqlUpdate1 += dtImpFormat.Rows(j).Item("Native_Col") & "='" & dt.Rows(i).Item(dtImpFormat.Rows(j).Item("Field_Name")) & "',"
                                If dtImpFormat.Rows(j).Item("UniqueID") Then sWhere += dtImpFormat.Rows(j).Item("Native_Col") & "='" & dt.Rows(i).Item(dtImpFormat.Rows(j).Item("Field_Name")) & "' AND "
                            Case "N"
                                sSql2 += ChkN(dt.Rows(i).Item(dtImpFormat.Rows(j).Item("Field_Name"))) & ","
                                If dtImpFormat.Rows(j).Item("Update_YN") And Not dtImpFormat.Rows(j).Item("UniqueID") Then sSqlUpdate1 += dtImpFormat.Rows(j).Item("Native_Col") & "=" & ChkN(dt.Rows(i).Item(dtImpFormat.Rows(j).Item("Field_Name"))) & ","
                                If dtImpFormat.Rows(j).Item("UniqueID") Then sWhere += dtImpFormat.Rows(j).Item("Native_Col") & "=" & ChkN(dt.Rows(i).Item(dtImpFormat.Rows(j).Item("Field_Name"))) & " AND "
                            Case "D"
                                If IsDBNull(dt.Rows(i).Item(dtImpFormat.Rows(j).Item("Field_Name"))) Then
                                    sSql2 += "NULL,"
                                    If dtImpFormat.Rows(j).Item("Update_YN") And Not dtImpFormat.Rows(j).Item("UniqueID") Then sSqlUpdate1 += dtImpFormat.Rows(j).Item("Native_Col") & "=NULL,"
                                    If dtImpFormat.Rows(j).Item("UniqueID") Then sWhere += dtImpFormat.Rows(j).Item("Native_Col") & "=NULL AND "
                                Else
                                    sSql2 += "'" & Format(dt.Rows(i).Item(dtImpFormat.Rows(j).Item("Field_Name")), "dd/MMM/yyyy") & "',"
                                    If dtImpFormat.Rows(j).Item("Update_YN") And Not dtImpFormat.Rows(j).Item("UniqueID") Then sSqlUpdate1 += dtImpFormat.Rows(j).Item("Native_Col") & "='" & Format(dt.Rows(i).Item(dtImpFormat.Rows(j).Item("Field_Name")), "dd/MMM/yyyy") & "',"
                                    If dtImpFormat.Rows(j).Item("UniqueID") Then sWhere += dtImpFormat.Rows(j).Item("Native_Col") & "='" & Format(dt.Rows(i).Item(dtImpFormat.Rows(j).Item("Field_Name")), "dd/MMM/yyyy") & "' AND "
                                End If
                            Case "T"
                                If IsDBNull(dt.Rows(i).Item(dtImpFormat.Rows(j).Item("Field_Name"))) Then
                                    sSql2 += "NULL,"
                                    If dtImpFormat.Rows(j).Item("Update_YN") And Not dtImpFormat.Rows(j).Item("UniqueID") Then sSqlUpdate1 += dtImpFormat.Rows(j).Item("Native_Col") & "=NULL,"
                                    If dtImpFormat.Rows(j).Item("UniqueID") Then sWhere += dtImpFormat.Rows(j).Item("Native_Col") & "=NULL AND "
                                Else
                                    sSql2 += "'" & Format(dt.Rows(i).Item(dtImpFormat.Rows(j).Item("Field_Name")), "hh:mm") & "',"
                                    If dtImpFormat.Rows(j).Item("Update_YN") And Not dtImpFormat.Rows(j).Item("UniqueID") Then sSqlUpdate1 += dtImpFormat.Rows(j).Item("Native_Col") & "='" & Format(dt.Rows(i).Item(dtImpFormat.Rows(j).Item("Field_Name")), "hh:mm") & "',"
                                    If dtImpFormat.Rows(j).Item("UniqueID") Then sWhere += dtImpFormat.Rows(j).Item("Native_Col") & "='" & Format(dt.Rows(i).Item(dtImpFormat.Rows(j).Item("Field_Name")), "hh:mm") & "' AND "
                                End If
                        End Select
                    Next
                    If Chk(sWhere) <> "" Then
                        sWhere = "WHERE " & Mid(sWhere, 1, sWhere.Length - 4)
                        strSql = "SELECT COUNT(*) FROM " & dtImpType.Rows(0).Item("Fmt_Table") & " " & sWhere
                        RecCnt = oDAL.ExecuteCommand(strSql, DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar)
                        If IsNothing(RecCnt) Or ChkN(RecCnt) = 0 Then
                            strIns += Mid(sSql1, 1, sSql1.Length - 1) & ") " & Mid(sSql2, 1, sSql2.Length - 1) & ")" & vbCrLf
                            'oDAL.ExecuteCommand(strSql)
                            dt.Rows(i).Item("RecordStatus") = "Record Inserted"
                        Else
                            strIns += Mid(sSqlUpdate1, 1, sSqlUpdate1.Length - 1) & " " & sWhere & vbCrLf
                            'oDAL.ExecuteCommand(strSql)
                            dt.Rows(i).Item("RecordStatus") = "Record Updated"
                        End If
                    Else
                        strIns += Mid(sSql1, 1, sSql1.Length - 1) & ") " & Mid(sSql2, 1, sSql2.Length - 1) & ")" & vbCrLf
                        'oDAL.ExecuteCommand(strSql)
                        dt.Rows(i).Item("RecordStatus") = "Record Inserted"
                    End If
                    dt.Rows(i).AcceptChanges()
                    'Response.Write(i)
                Next
                oDAL.ExecuteCommand(strIns)
                dt.AcceptChanges()
                grdImport.DataSource = ViewState("dtData")
                grdImport.DataBind()
            Else
                SetMsg(LblErrMsg, "Import Mode Option has not been Set in Format Table.")
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, "UploadData" & vbCrLf & ex.Source & vbCrLf & ex.Message)
        Finally
            dtImpType.Dispose()
            dtImpFormat.Dispose()
        End Try
    End Sub

    Private Sub cmdUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpload.Click

        ViewState("dtData") = Nothing
        grdImport.DataSource = Nothing

        cmdImport_Click(Nothing, Nothing)
        UploadData()
    End Sub
    Private Sub cmbFormat_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFormat.SelectedIndexChanged
        Dim dtImpType As DataTable
        Try
            If Not cmbFormat.SelectedItem.Text.Trim = "" Then
                dtImpType = New DataTable
                strSql = "SELECT * FROM ImpType WHERE Fmt_ID=" & ChkN(cmbFormat.SelectedValue)
                oDAL.GetSqlDataTable(dtImpType, strSql)
                If dtImpType.Rows.Count > 0 Then
                    If dtImpType.Rows(0).Item("DateReq") Then
                        DtpFDate.Enabled = True
                        DtpTDate.Enabled = True
                        If Not dtImpType.Rows(0).Item("From_To_DT") Then
                            DtpTDate.Enabled = False
                        End If
                    Else
                        DtpFDate.Enabled = False
                        DtpTDate.Enabled = False
                    End If
                    If dtImpType.Rows(0).Item("EOM_YN") Then
                        viewstate("bEOM") = True
                    Else
                        viewstate("bEOM") = False
                    End If
                End If
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, "CmbFormat_Change" & vbCrLf & ex.Source & vbCrLf & ex.Message)
        Finally
            dtImpType.Dispose()
        End Try
    End Sub
    Private Function GetVarValue(ByVal Field_Name As String) As Object
        Try
            Select Case UCase(Field_Name)
                Case "PAYDATE"
                    GetVarValue = CType(DtpFDate.Text, Date)
                Case "FDATE"
                    GetVarValue = CType(DtpFDate.Text, Date)
                Case "TDATE"
                    GetVarValue = CType(DtpTDate.Text, Date)
                Case "FY_START"
                    GetVarValue = FY_Start
                Case "FY_END"
                    GetVarValue = FY_End
                Case "RY_START"
                    GetVarValue = RY_Start
                Case "RY_END"
                    GetVarValue = RY_End
                Case "LY_START"
                    GetVarValue = LY_Start
                Case "LY_END"
                    GetVarValue = LY_End
                Case "FINYEAR"
                    GetVarValue = Year(FY_Start)
                Case "RIMYEAR"
                    GetVarValue = Year(RY_Start)
                Case "LEVYEAR"
                    GetVarValue = Year(LY_Start)
                Case "GATDATE"
                    GetVarValue = Date.Today
            End Select
        Catch ex As Exception
            SetMsg(LblErrMsg, "GetVarValue" & vbCrLf & ex.Source & vbCrLf & ex.Message)
        Finally
        End Try
    End Function

    Private Sub cmdFormat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFormat.Click
        Dim dtImpFormat As DataTable
        Dim sFilePath As String
        Dim sFileName As String
        Dim sw As StreamWriter
        Dim iPrevCol As Int16
        Try
            sFilePath = HttpContext.Current.Request.ServerVariables("APPL_PHYSICAL_PATH")
            sFileName = "Export\Exp" & Format(Date.Now, "yyyyMMddhhmmss") & ".XLS"
            sFilePath += "\" & sFileName
            sw = New StreamWriter(sFilePath)
            strSql = "SELECT * FROM ImpFormat WHERE Fmt_ID=" & ChkN(cmbFormat.SelectedValue) & " AND ColNo>0 ORDER BY ColNo"
            dtImpFormat = New DataTable
            oDAL.GetSqlDataTable(dtImpFormat, strSql)
            iPrevCol = 0
            For i As Int16 = 0 To dtImpFormat.Rows.Count - 1
                If dtImpFormat.Rows(i).Item("ColNo") - 1 > iPrevCol Then
                    For j As Int16 = iPrevCol + 1 To dtImpFormat.Rows(i).Item("ColNo") - 1
                        sw.Write("" & vbTab)
                    Next
                End If
                sw.Write(dtImpFormat.Rows(i).Item("Field_Name") & vbTab)
                iPrevCol = dtImpFormat.Rows(i).Item("ColNo")
            Next
            sw.WriteLine("")
            Response.Write("<A Href=" & sFileName & ">Click Here To Download the file</A>")
        Catch ex As Exception
            SetMsg(LblErrMsg, "GetVarValue" & vbCrLf & ex.Source & vbCrLf & ex.Message)
        Finally
            sw.Close()
            If Not IsNothing(dtImpFormat) Then dtImpFormat.Dispose()
        End Try
    End Sub



    Private Sub Page_Error(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Error
        Try

            Response.Redirect("Import.aspx")

        Catch ex As Exception
            SetMsg(LblErrMsg, "Page_Error" & vbCrLf & ex.Source & vbCrLf & ex.Message)
        End Try
    End Sub
End Class

