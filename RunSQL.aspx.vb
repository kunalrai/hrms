Partial Class RunSQL
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
    Dim oDal As DAL.DataLayer
    Dim oBal As BAL.BLayer
    Dim strSql As String
    Dim rsGrdData As DataTable
    Dim blnDirty As Boolean
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Dim dtFilterFlds As DataTable
        oDal = Session("DALObj")
        oBal = Session("BALObj")
        If Not IsPostBack Then
            oBal.FillCombo(cmbCommand, "ID", "cmdDesc", "SQLPROC", True)
            strSql = "SELECT PaySetup.Field_Name,paysetup.field_desc,replace(paysetup.field_name,'_CODE','_NAME') as display_field FROM SYSCOLUMNS INNER JOIN PAYSETUP on PAYSETUP.Field_Name=SYSCOLUMNS.[NAME]  WHERE [id]=object_id(N'HRDMASTQRY') and [Name] like ('%_CODE')"
            dtFilterFlds = New DataTable
            Session("DalObj").GetSqlDataTable(dtFilterFlds, strSql)
            For i As Int16 = 0 To dtFilterFlds.Rows.Count - 1
                CmbFor.Items.Add(New ListItem(Chk(dtFilterFlds.Rows(i).Item("Field_Desc")), Chk(dtFilterFlds.Rows(i).Item("Field_Name"))))
            Next
            CmbFor.Items.Add("All") : CmbFor.SelectedIndex = CmbFor.Items.Count - 1
            DtpFrom.Text = Format(Date.Today, "dd/MMM/yyyy")
            dtpTo.Text = Format(Date.Today, "dd/MMM/yyyy")
        Else
            'ViewState("rsGrdData") = ViewState("rsGrdData")")
        End If
    End Sub
    Private Sub cmbfor_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbFor.SelectedIndexChanged
        Dim sTable, sCode, sName As String
        If CmbFor.SelectedValue = "All" Then
        Else
            sTable = Replace(UCase(CmbFor.SelectedValue), "_CODE", "MAST")
            sName = Replace(UCase(CmbFor.SelectedValue), "_CODE", "_NAME")
            sCode = UCase(CmbFor.SelectedValue)
            Select Case sCode
                Case "EMP_CODE", "MNGR_CODE"
                    sTable = "HRDMASTQRY WHERE LTYPE=1"
                Case "TYPE_CODE"
                    sTable = "EMPTYPE"
            End Select
            oBal.FillCombo(CmbFilter, sCode, sName, sTable, True)
        End If
    End Sub

    Private Sub cmdDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDetail.Click
        Dim rsSqlCmd As New DataTable
        Dim rsView As DataTable
        Dim SqlRunCmd As String
        Dim columnNo() As String
        Dim txtCond As String
        Dim tmpTbl As String
        Try
            If Chk(cmbCommand.SelectedItem.Text) <> "" Then
                oDal.GetSqlDataTable(rsSqlCmd, "select [ID],ViewText,ViewQry,activeColumnNo,Date_FromTo, eomYN, tmpTable from SQLPROC where ID = " & ChkN(cmbCommand.SelectedValue))
                If Not rsSqlCmd.Rows.Count = 0 Then
                    SqlRunCmd = Chk(rsSqlCmd.Rows(0).Item("ViewText"))
                    '*******Filter Query********
                    txtCond = ""
                    If CmbFor.SelectedValue = "All" Then
                        txtCond = ""
                    Else
                        If rsSqlCmd.Rows(0).Item("ID") = 2 Then
                            txtCond = " AND HrdMastQry." & CmbFor.SelectedValue & " = ''" & CmbFilter.SelectedValue & "''"
                        Else
                            txtCond = " AND HrdMastQry." & CmbFor.SelectedValue & " = '" & CmbFilter.SelectedValue & "'"
                        End If
                    End If
                    SqlRunCmd = Replace(SqlRunCmd, "|$|", txtCond)
                    If ChkN(rsSqlCmd.Rows(0).Item("Date_FromTo")) <> 1 Then
                        If Chk(rsSqlCmd.Rows(0).Item("eomYN")) = "Y" Then
                            DtpFrom.Text = Format(EOM(CType(DtpFrom.Text, Date)), "dd/MMM/yyyy")
                        End If
                        SqlRunCmd = Replace(SqlRunCmd, "[PAYDATE]", "'" & DtpFrom.Text & "'")
                    Else
                        SqlRunCmd = Replace(SqlRunCmd, "[FDATE]", "'" & DtpFrom.Text & "'")
                        SqlRunCmd = Replace(SqlRunCmd, "[TDATE]", "'" & dtpTo.Text & "'")
                    End If
                    tmpTbl = Chk(rsSqlCmd.Rows(0).Item("tmpTable"), , True)
                    If tmpTbl <> "" Then
                        Try
                            oDal.ExecuteCommand("Drop Table " & tmpTbl, DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteNonQuery)
                        Catch ex As Exception
                        End Try
                    End If
                    oDal.ExecuteCommand(SqlRunCmd, DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteNonQuery)
                    Dim i As Integer ', cNo As Integer
                    If tmpTbl <> "" Then
                        ViewState("rsGrdData") = New DataTable
                        oDal.GetSqlDataTable(ViewState("rsGrdData"), "Select * From " & tmpTbl)
                        grdList.DataSource = ViewState("rsGrdData")
                        grdList.DataBind()
                    Else
                        SqlRunCmd = Chk(rsSqlCmd.Rows(0).Item("ViewQry"))
                        '*********Formating
                        txtCond = ""
                        If CmbFor.SelectedValue = "All" Then
                            txtCond = ""
                        Else
                            If rsSqlCmd.Rows(0).Item("ID") = 2 Then
                                txtCond = " AND HrdMastQry." & CmbFor.SelectedValue & " = ''" & CmbFilter.SelectedValue & "''"
                            Else
                                txtCond = " AND HrdMastQry." & CmbFor.SelectedValue & " = '" & CmbFilter.SelectedValue & "'"
                            End If
                        End If
                        SqlRunCmd = Replace(SqlRunCmd, "|$|", txtCond)
                        '***************************
                        'If dtpPayDate.Enabled = True Then
                        If ChkN(rsSqlCmd.Rows(0).Item("Date_FromTo")) <> 1 Then
                            If Chk(rsSqlCmd.Rows(0).Item("eomYN")) = "Y" Then
                                DtpFrom.Text = Format(EOM(CType(DtpFrom.Text, Date)), "dd/MMM/yyyy")
                            End If
                            SqlRunCmd = Replace(SqlRunCmd, "[PAYDATE]", "'" & DtpFrom.Text & "'")
                            'End If
                        Else
                            SqlRunCmd = Replace(SqlRunCmd, "[FDATE]", "'" & DtpFrom.Text & "'")
                            SqlRunCmd = Replace(SqlRunCmd, "[TDATE]", "'" & dtpTo.Text & "'")
                        End If
                        ViewState("rsGrdData") = New DataTable
                        oDal.GetSqlDataTable(ViewState("rsGrdData"), SqlRunCmd)
                        grdList.DataSource = ViewState("rsGrdData")
                        grdList.DataBind()
                    End If
                    blnDirty = False
                End If
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message)
        Finally
        End Try
    End Sub

    Private Sub cmdExcelView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExcelView.Click
        Dim sFileName As String
        sFileName = GetExcelFile(ViewState("rsGrdData"))
        Response.Write("<A Href=" & sFileName & ">Click Here To Download the file</A>")
    End Sub

    Private Sub cmdProceed_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdProceed.Click
        Dim rsSqlCmd As New DataTable
        Dim SqlRunCmd As String
        Try
            If Chk(cmbCommand.SelectedItem.Text) <> "" Then
                oDal.GetSqlDataTable(rsSqlCmd, "select cmdText, Date_FromTo, eomYN from SQLPROC where ID = " & ChkN(cmbCommand.SelectedValue))
                If Not rsSqlCmd.Rows.Count = 0 Then
                    SqlRunCmd = Chk(rsSqlCmd.Rows(0).Item("cmdtext"))
                    'If dtpPayDate.Enabled = True Then
                    '  SqlRunCmd = Replace(SqlRunCmd, "[PAYDATE]", "'" & Format(dtpPayDate.Value, "dd/mmm/yyyy") & "'")
                    'End If
                    If ChkN(rsSqlCmd.Rows(0).Item("Date_FromTo")) <> 1 Then
                        If Chk(rsSqlCmd.Rows(0).Item("eomYN")) = "Y" Then
                            DtpFrom.Text = Format(EOM(CType(DtpFrom.Text, Date)), "dd/MMM/yyyy")
                        End If
                        SqlRunCmd = Replace(SqlRunCmd, "[PAYDATE]", "'" & DtpFrom.Text & "'")
                        'End If
                    Else
                        SqlRunCmd = Replace(SqlRunCmd, "[FDATE]", "'" & DtpFrom.Text & "'")
                        SqlRunCmd = Replace(SqlRunCmd, "[TDATE]", "'" & dtpTo.Text & "'")
                    End If
                    'SetMsg("Running SQL Command", True)
                    oDal.ExecuteCommand(SqlRunCmd, DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteNonQuery)
                    'SetMsg("Ready", False)
                    SetMsg(LblErrMsg, "Transaction successfull.")
                End If
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message)
        Finally
            rsSqlCmd.Dispose()
        End Try
    End Sub

    Private Sub cmdCons_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCons.Click
        Dim rsSqlCmd As New DataTable
        Dim SqlRunCmd As String
        Try
            If IsNothing(viewstate("rsGrdData")) Then Exit Sub
            oDal.GetSqlDataTable(rsSqlCmd, "select cmdText, Date_FromTo, eomYN from SQLPROC where ID = " & ChkN(cmbCommand.SelectedValue))
            SqlRunCmd = Chk(rsSqlCmd.Rows(0).Item("ConsViewText"))
            If ChkN(rsSqlCmd.Rows(0).Item("Date_FromTo")) <> 1 Then
                If Chk(rsSqlCmd.Rows(0).Item("eomYN")) = "Y" Then
                    DtpFrom.Text = Format(EOM(CType(DtpFrom.Text, Date)), "dd/MMM/yyyy")
                End If
                SqlRunCmd = Replace(SqlRunCmd, "[PAYDATE]", "'" & DtpFrom.Text & "'")
                'End If
            Else
                SqlRunCmd = Replace(SqlRunCmd, "[FDATE]", "'" & DtpFrom.Text & "'")
                SqlRunCmd = Replace(SqlRunCmd, "[PAYDATE]", "'" & DtpFrom.Text & "'")
                SqlRunCmd = Replace(SqlRunCmd, "[TDATE]", "'" & dtpTo.Text & "'")
            End If
            If Chk(SqlRunCmd, , True) <> "" Then
                ViewState("rsGrdData") = New DataTable
                oDal.GetSqlDataTable(ViewState("rsGrdData"), SqlRunCmd)
                grdList.DataSource = ViewState("rsGrdData")
                grdList.DataBind()
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message)
        Finally
            rsSqlCmd.Dispose()
        End Try
    End Sub

    Private Sub cmbCommand_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCommand.SelectedIndexChanged
        Dim rsSqlCmd As New DataTable
        If Chk(cmbCommand.SelectedItem.Text) <> "" Then
            oDal.GetSqlDataTable(rsSqlCmd, "select ConsolidateYN,ParamYN,cmdText, Date_FromTo, eomYN from SQLPROC where ID = " & ChkN(cmbCommand.SelectedValue))
            If rsSqlCmd.Rows.Count > 0 Then
                If rsSqlCmd.Rows(0).Item("ParamYN") = True Then
                    DtpFrom.Enabled = True
                End If
                If ChkN(rsSqlCmd.Rows(0).Item("Date_FromTo")) = 0 Then
                    If rsSqlCmd.Rows(0).Item("eomYN") = "Y" Then
                        LbldtpFrom.Text = "Paydate"
                    Else
                        LbldtpFrom.Text = "Date"
                    End If
                    lblDtpTo.Visible = False
                    dtpTo.Visible = False
                Else
                    LbldtpFrom.Text = "From"
                    lblDtpTo.Visible = True
                    dtpTo.Visible = True
                End If
                If ChkN(rsSqlCmd.Rows(0).Item("ConsolidateYN")) = 1 Then
                    cmdCons.Enabled = True
                Else
                    cmdCons.Enabled = False
                End If
                'cmdOTDetails.Enabled = False
                If Chk(rsSqlCmd.Rows(0).Item("cmdtext"), , True) = "" Then
                    cmdProceed.Enabled = False
                Else
                    cmdProceed.Enabled = True
                End If
            End If
        End If

    End Sub
End Class
