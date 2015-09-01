Partial Class ReimbursementPayment
    Inherits System.Web.UI.Page
    Dim rsRimTmp As New DataTable
    Dim bCheckOnYTD As Boolean
    Dim bCheckOnLWOP As Boolean
    Dim iCheckOnCALC As Int16
    Dim oDAL As DAL.DataLayer
    Dim oBal As BAL.BLayer
    Dim dtReim As DataTable
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lbldate As System.Web.UI.WebControls.Label
    Protected WithEvents cmbdate As DITWebLibrary.DTP
    Protected WithEvents lblcode As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label

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
            SetMsg(LblErrMsg, "")
            oBal = Session("BalObj")
            oDAL = Session("DalObj")
            If IsNothing(Session("LoginUser")) Then
                Response.Redirect("CompSel.aspx")
            End If
            If Not IsPostBack Then
                'By Ravi 17 nov 2006
                Dim SrNo As Int16
                SrNo = Request.QueryString.Item("SrNo") + Request.QueryString.Item("MODULEID")
                If Not IsPostBack Then
                    Dim bSuccess As Boolean
                    Select Case CheckRight(SrNo)
                        Case MdlHRMS.AccessType.FullAccess, MdlHRMS.AccessType.SuperUser
                            bSuccess = True
                        Case MdlHRMS.AccessType.ReadonlyAccess, MdlHRMS.AccessType.Restricted
                            bSuccess = False
                    End Select
                    BtnSave.Visible = bSuccess
                    BtnDelete.Visible = bSuccess

                End If
                '---------------------------------------------
                FillComboBox()
                LblErrMsg.Text = ""
                Session("BalObj").FillCombo(cmbEmp, "Select Emp_Code,Emp_Name as EMP_NAME From HrdMastQry Where  LType=1 " & Session("UserCodes") & " Order by Emp_Name", True)
                Session("BalObj").FillCombo(CmbPMode, "Select BANK_CODE,BANK_NAME From bankmast  Order by BANK_NAME", True)
                Dtpdate.Text = Format(Date.Today, "dd/MMM/yyyy")
                CmbPMode.Items.Add(New ListItem("THROUGH PAYSLIP", "PS"))
                ViewState("dtReim") = New DataTable
                getReimCur(ViewState("dtReim"))
                BindgrdReim()
            End If


        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & "On Load")
        End Try

    End Sub
    Sub DisplayRecords()
        Try
            Dim StrSQl As String, dtEmp As New DataTable
            Dim EmpName As Object
            StrSQl = "SELECT  EMP_CODE, Emp_Name, DSG_NAME,  DEPT_NAME , LOC_NAME, Grd_Name,DOL FROM HRDMASTQRY where emp_code='" & Chk(TxtCode.Text) & "'  Order By Emp_Name" & Session("UserCodes")
            oDAL.GetSqlDataTable(dtEmp, StrSQl)
            EmpName = CType(oDAL, DAL.DataLayer).ExecuteCommand(" Select Emp_Name From HrdMastQry Where Emp_Code = '" & Chk(TxtCode.Text) & "'" & Session("UserCodes"), , DAL.DataLayer.ExecutionType.ExecuteScalar)
            If dtEmp.Rows.Count > 0 Then
                TxtDept.Text = Chk(dtEmp.Rows(0).Item("DEPT_NAME"))
                TxtDesg.Text = Chk(dtEmp.Rows(0).Item("DSG_NAME"))
                TxtGrade.Text = Chk(dtEmp.Rows(0).Item("Grd_Name"))
                TxtLoc.Text = Chk(dtEmp.Rows(0).Item("LOC_NAME"))
            End If
            If (Not IsDBNull(EmpName)) And (Not IsNothing(EmpName)) Then
                LblName.Text = EmpName
            Else
                Dim Code As Object
                Code = oDAL.ExecuteCommand("Select Emp_Code From HrdMastQry Where Emp_Code = '" & Chk(TxtCode.Text) & "'" & Session("UserCodes"), , DAL.DataLayer.ExecutionType.ExecuteScalar)
                If Code <> "" Then
                    SetMsg(LblErrMsg, "This Employee Code Exist For Other Location.")
                End If
                LblName.Text = ""
            End If
            dtEmp = Nothing
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " DisplayRecords")
        End Try
    End Sub
    Private Sub TxtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCode.TextChanged
        Try
            DisplayRecords()
            BindgrdReim()
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " TxtCode_TextChanged")
        End Try
    End Sub
    Private Sub btnList_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnList.Click
        LblName.Visible = False
        LblEmpName.Visible = False
        TxtCode.Visible = False
        btnList.Visible = False
        cmbEmp.Visible = True
    End Sub
    Private Sub cmbEmp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbEmp.SelectedIndexChanged
        Try

            If cmbEmp.SelectedValue <> "" Then
                TxtCode.Text = cmbEmp.SelectedValue
            Else
                TxtCode.Text = ""
            End If
            TxtCode_TextChanged(sender, e)
            LblName.Visible = True
            LblEmpName.Visible = True
            TxtCode.Visible = True
            btnList.Visible = True
            cmbEmp.Visible = False
            BindgrdReim()
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & "cmbEmp_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub BindgrdReim()
        Try
            Dim StrSql As String
            Dim DtRimTran As New DataTable
            Dim dtReimMast As New DataTable
            Dim RY_Start_WEF, RY_End_WEF, dptRimDate As Date
            Dim i, j, k As Int16
            Dim dr As DataRow

            Dim objSCM As SCM.Payroll
            Dim frmDate As Date
            Dim ProrataAmt As Double
            Dim dtEmp As New DataTable
            Dim dvReimTran As DataView
            BlankGrid()
            RY_Start_WEF = DateAdd("m", -3, RY_Start)
            RY_End_WEF = DateAdd("m", 3, RY_End)

            StrSql = "SELECT Emp_Code,Emp_Name,DOL FROM HRDMASTQRY WHERE Emp_Code='" & TxtCode.Text & "'" & Session("UserCodes")
            oDAL.GetSqlDataTable(dtEmp, StrSql)

            StrSql = "SELECT ReimMast.*, Print_Name,FLD_MONTH FROM ReimMast, PaySetup WHERE RimYear='" & Year(RY_Start) & "' AND Emp_Code='" & TxtCode.Text & "' AND PaySetup.Field_Name=ReimMast.Field_Name"
            oDAL.GetSqlDataTable(dtReimMast, StrSql)
            objSCM = GetSCMPayroll(oDAL, LblErrMsg, Session("LoginUser"))
            SetReimSettings()
            For i = 0 To dtReimMast.Rows.Count - 1
                dr = ViewState("dtReim").NewRow
                dr.Item("Field_Name") = dtReimMast.Rows(i).Item("Field_Name")
                dr.Item("Print_Name") = dtReimMast.Rows(i).Item("Print_Name")
                dr.Item("RY_Start_WEF") = DateSerial(Year(RY_Start), dtReimMast.Rows(i).Item("Fld_Month"), 1)
                dr.Item("opn_bal") = ChkN(dtReimMast.Rows(i).Item("opn_bal"))
                dr.Item("Prorata") = ChkN(dtReimMast.Rows(i).Item("Prorata"))
                dr.Item("Tot_Opn") = ChkN(dtReimMast.Rows(i).Item("opn_bal")) + ChkN(dtReimMast.Rows(i).Item("Prorata")) + ChkN(dtReimMast.Rows(i).Item("SPLBudget"))
                dr.Item("CarryFwdAmt") = 0
                '********Calculation of LWPAmount and Prorata Amount*******************
                '****Calculation Till Date
                If iCheckOnCALC = 1 Then
                    frmDate = BOM(Dtpdate.Text)
                ElseIf iCheckOnCALC = 2 Then
                    frmDate = Dtpdate.Text
                Else
                    frmDate = EOM(Dtpdate.Text).AddDays(1)
                End If
                '***Calculation of LWOP Hit
                If bCheckOnLWOP Then
                    ProrataAmt = ChkN(dtReimMast.Rows(i).Item("Prorata")) + ChkN(dtReimMast.Rows(i).Item("LwpLoss")) - objSCM.calcLwopAmt(ChkN(dtReimMast.Rows(i).Item("Budget")), RY_Start, frmDate, Chk(dtReimMast.Rows(i).Item("Emp_Code")), True, Chk(dtReimMast.Rows(i).Item("Field_Name")), dtReimMast.Rows(i).Item("Budget_Wef"))
                Else
                    ProrataAmt = dr.Item("Prorata")
                End If
                '****Final YTD Prorata Calculation
                If IsDBNull(dtEmp.Rows(0).Item("DOL")) Then
                    dr.Item("YTD_Prorata") = FormatNumber(objSCM.Prorate(-ChkN(dtReimMast.Rows(i).Item("Budget")), ProrataAmt, frmDate, DateAdd(DateInterval.Month, 12, DateSerial(Year(RY_Start), ChkN(dtReimMast.Rows(i).Item("Fld_Month")), 1)).AddDays(-1), 5, ChkN(dtReimMast.Rows(i).Item("Fld_Month"))), 0)
                Else
                    dr.Item("YTD_Prorata") = FormatNumber(objSCM.Prorate(-ChkN(dtReimMast.Rows(i).Item("Budget")), ProrataAmt, frmDate, dtEmp.Rows(0).Item("DOL"), 5, ChkN(dtReimMast.Rows(i).Item("Fld_Month"))), 0)
                End If
                '**********************************************************************
                '*******Reimtran Records********
                DtRimTran = New DataTable
                StrSql = "SELECT ReimTran.* FROM ReimTran WHERE RimYear='" & Year(RY_Start) & "' AND Emp_Code='" & TxtCode.Text & "' AND FIELD_NAME='" & dtReimMast.Rows(i).Item("Field_Name") & "' AND (ReimDate >='" & Format(RY_Start, "dd/MMM/yyyy") & "' AND ReimDate <='" & (Dtpdate.Text) & "') ORDER BY Emp_Code, ReimDate"
                oDAL.GetSqlDataTable(DtRimTran, StrSql)
                For j = 0 To DtRimTran.Rows.Count - 1
                    If CDate(DtRimTran.Rows(j).Item("ReimDate")) = CDate(Dtpdate.Text) Then
                        dr.Item("Claimed") = ChkN(DtRimTran.Rows(j).Item("Claimed"))
                        dr.Item("Amount") = ChkN(DtRimTran.Rows(j).Item("Amount"))
                        dr.Item("Taxable") = ChkN(DtRimTran.Rows(j).Item("Taxable"))
                        dr.Item("TDS") = ChkN(DtRimTran.Rows(j).Item("TDS"))
                        dr.Item("Remarks") = Chk(DtRimTran.Rows(j).Item("Remarks"))
                        PayDate = DtRimTran.Rows(j).Item("Paydate")
                        CmbPMode.SelectedValue = Chk(DtRimTran.Rows(j).Item("Bank_Code"))
                    Else
                        dr.Item("reimbursed") = ChkN(dr.Item("reimbursed")) + ChkN(DtRimTran.Rows(j).Item("Amount"))
                        dr.Item("Claimed") = 0
                        dr.Item("Amount") = 0
                        dr.Item("Taxable") = 0
                        dr.Item("TDS") = 0
                        dr.Item("Remarks") = ""
                        dr.Item("CarryFwdAmt") = dr.Item("CarryFwdAmt") + (ChkN(DtRimTran.Rows(j).Item("Claimed")) - ChkN(DtRimTran.Rows(j).Item("Amount")))
                    End If
                Next
                '********************************
                dr.Item("Opening") = ChkN(dr.Item("opn_bal")) + ChkN(dr.Item("YTD_PRORATA")) + ChkN(dtReimMast.Rows(i).Item("SPLBudget")) - ChkN(dr.Item("reimbursed"))
                dr.Item("Tot_Opn") = ChkN(dr.Item("opn_bal")) + dr.Item("Prorata") + ChkN(dtReimMast.Rows(i).Item("SPLBudget")) - ChkN(dr.Item("reimbursed"))
                dr.Item("Closing") = ChkN(dr.Item("Tot_Opn")) - ChkN(dr.Item("Amount"))
                ViewState("dtReim").Rows.Add(dr)
            Next
            GrdReim.DataSource = ViewState("dtReim")
            GrdReim.DataBind()
            'DataGrid1.DataSource = ViewState("dtReim")
            'DataGrid1.DataBind()
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (BindGrdReim)")
        Finally
        End Try
    End Sub
    Private Function FillComboBox()
        Try
            Dim LItem As ListItem, i As Int16
            Dim DtDate As Date
            DtDate = FY_Start
            For i = 1 To 12
                LItem = New ListItem
                LItem.Text = MonthName(Month(DtDate)) & " " & Year(DtDate)
                LItem.Value = EOM(DtDate)
                CmbMonth.Items.Add(LItem)
                DtDate = LItem.Value
                DtDate = DateAdd(DateInterval.Month, 1, DtDate)
            Next
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & ": FillComboBox()")
        End Try
    End Function
    Private Function getReimCur(ByRef dt As DataTable)
        dt.Columns.Add("Field_Name", System.Type.GetType("System.String"))
        dt.Columns.Add("Print_Name", System.Type.GetType("System.String"))
        dt.Columns.Add("RY_Start_WEF", System.Type.GetType("System.DateTime"))
        dt.Columns.Add("Opn_Bal", System.Type.GetType("System.Double"))
        dt.Columns.Add("Prorata", System.Type.GetType("System.Double"))
        dt.Columns.Add("Tot_Opn", System.Type.GetType("System.Double"))
        dt.Columns.Add("YTD_Prorata", System.Type.GetType("System.Double"))
        dt.Columns.Add("Opening", System.Type.GetType("System.Double"))
        dt.Columns.Add("Reimbursed", System.Type.GetType("System.Double"))
        dt.Columns.Add("CarryFwdAmt", System.Type.GetType("System.Double"))
        dt.Columns.Add("Claimed", System.Type.GetType("System.Double"))
        dt.Columns.Add("Amount", System.Type.GetType("System.Double"))
        dt.Columns.Add("Taxable", System.Type.GetType("System.Double"))
        dt.Columns.Add("TDS", System.Type.GetType("System.Double"))
        dt.Columns.Add("Closing", System.Type.GetType("System.Double"))
        dt.Columns.Add("Remarks", System.Type.GetType("System.String"))
    End Function
    Private Sub SetReimSettings()
        Dim dtCompMast As New DataTable
        oDAL.GetSqlDataTable(dtCompMast, "SELECT Rating2,Rating4,Rating5 FROM CompMast WHERE COMP_CODE='" & Session("LoginUser").CurrentCompID & "'")
        If dtCompMast.Rows.Count > 0 Then
            bCheckOnYTD = IIf(Chk(dtCompMast.Rows(0).Item("Rating2")) = "Y", True, False)
            bCheckOnLWOP = IIf(Chk(dtCompMast.Rows(0).Item("Rating4")) = "Y", True, False)
            iCheckOnCALC = IIf(ChkN(dtCompMast.Rows(0).Item("Rating5")) = 0, 1, ChkN(dtCompMast.Rows(0).Item("Rating5")))
        Else
            bCheckOnYTD = False
            bCheckOnLWOP = False
            iCheckOnCALC = 1
        End If

    End Sub
    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        SaveRec("SAVE")
    End Sub

    Private Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        ClearAll(Me)
    End Sub

    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        SaveRec("DELETE")
    End Sub
    Private Sub SaveRec(ByVal sAction As String)
        Dim mdlPayCal As PayCal
        Dim strMessage, strSql As String
        Dim dtRimMast As New DataTable, dtRimTran As New DataTable
        Dim i, j, k As Int16
        Dim dr As DataRow
        Dim dt As DataTable
        Try
            dt = ViewState("dtReim")
            If Trim(CmbMonth.SelectedValue) = "" Then
                SetMsg(LblErrMsg, "Pay Month can't be blank")
                Exit Sub
            End If
            If Trim(TxtCode.Text) = "" Then
                SetMsg(LblErrMsg, "Employee Code can't be blank")
                Exit Sub
            End If
            mdlPayCal = New PayCal(Session("LoginUser"), oDAL)
            PayDate = DateAdd(DateInterval.Month, CmbMonth.SelectedIndex + 1, FY_Start).AddDays(-1)
            If Not mdlPayCal.chkMon(CType(Format(PayDate, "dd/MMM/yyyy"), Date), strMessage) Then
                SetMsg(LblErrMsg, strMessage)
                Exit Sub
            End If
            'If MsgBox("Do you want to save changes.", vbQuestion + vbYesNo, "SEMS") = vbNo Then Exit Sub
            'On Error Resume Next

            Dim blnPrevRec As Boolean   'Check For Prev. Record
            blnPrevRec = False
            oDAL.GetSqlDataTable(dtRimMast, "SELECT ReimMast.* FROM ReimMast WHERE RimYear=" & ChkN(Year(RY_Start)) & " AND Emp_Code='" & TxtCode.Text & "'")
            oDAL.GetSqlDataTable(dtRimTran, "SELECT ReimTran.* FROM ReimTran WHERE RimYear=" & ChkN(Year(RY_Start)) & " AND Emp_Code='" & TxtCode.Text & "' AND ReimDate='" & Format(CDate(Dtpdate.Text), "dd/MMM/yyyy") & "'")
            If dtRimTran.Rows.Count > 0 Then
                For i = 0 To dtRimTran.Rows.Count - 1
                    blnPrevRec = True
                    dr = dtRimMast.Select("Emp_Code='" & dtRimTran.Rows(i).Item("Emp_Code") & "' And Field_Name='" & dtRimTran.Rows(i).Item("Field_Name") & "'")(0)
                    If Not dr Is Nothing Then
                        strSql = "UPDATE REIMMAST SET Reimbursed=Reimbursed-" & dtRimTran.Rows(i).Item("Amount") & " WHERE RimYear=" & ChkN(Year(RY_Start)) & " AND Emp_Code='" & Chk(TxtCode.Text) & "' AND Field_Name='" & dtRimTran.Rows(i).Item("Field_Name") & "'"
                        oDAL.ExecuteCommand(strSql, 0, 0)
                    End If
                    strSql = "DELETE FROM REIMTRAN WHERE RimYear=" & ChkN(Year(RY_Start)) & " AND Emp_Code='" & Chk(TxtCode.Text) & "' AND Field_Name='" & Chk(dtRimTran.Rows(i).Item("Field_Name")) & "' AND ReimDate='" & Format(dtRimTran.Rows(i).Item("ReimDate"), "dd/MMM/yyyy") & "'"
                    oDAL.ExecuteCommand(strSql, 0, 0)
                Next
            End If
            If UCase(sAction) = "SAVE" Then
                For i = 0 To dt.Rows.Count - 1
                    strSql = "INSERT INTO ReimTran (RimYear,Paydate,Emp_Code,ReimDate,Field_Name,Claimed,Amount,Taxable,TDS,reimbursed,Remarks,Bank_Code) VALUES (" & _
                             "" & ChkN(Year(RY_Start)) & "," & _
                             "'" & Format(PayDate, "dd/MMM/yyyy") & "'," & _
                             "'" & Chk(TxtCode.Text) & "'," & _
                             "'" & Format(CDate(Dtpdate.Text), "dd/MMM/yyyy") & "'," & _
                             "'" & Chk(dt.Rows(i).Item("Field_Name")) & "'," & _
                             "" & ChkN(dt.Rows(i).Item("Claimed")) & "," & _
                             "" & ChkN(dt.Rows(i).Item("Amount")) & "," & _
                             "" & ChkN(dt.Rows(i).Item("Taxable")) & "," & _
                             "" & ChkN(dt.Rows(i).Item("TDS")) & "," & _
                             "" & ChkN(dt.Rows(i).Item("Reimbursed")) + ChkN(dt.Rows(i).Item("Amount")) & "," & _
                             "'" & Chk(dt.Rows(i).Item("Remarks")) & "'," & _
                             "'" & Chk(CmbPMode.SelectedValue) & "')"
                    oDAL.ExecuteCommand(strSql, 0, 0)
                    'strSql = "UPDATE ReimMast SET Reimbursed=" & ChkN(dt.Rows(i).Item("Reimbursed")) + ChkN(dt.Rows(i).Item("Amount")) & ",CarryFwdAmt=" & MaxV(0, ChkN(dt.Rows(i).Item("CarryFwdAmt")) + ChkN(dt.Rows(i).Item("Claimed")) - ChkN(dt.Rows(i).Item("Amount"))) & " WHERE Emp_Code='" & Chk(TxtCode.Text) & "' And Field_Name='" & Chk(dt.Rows(i).Item("Field_Name")) & "' AND RimYear=" & ChkN(Year(RY_Start))
                    strSql = "UPDATE ReimMast SET Reimbursed=" & ChkN(dt.Rows(i).Item("Reimbursed")) + ChkN(dt.Rows(i).Item("Amount")) & " WHERE Emp_Code='" & Chk(TxtCode.Text) & "' And Field_Name='" & Chk(dt.Rows(i).Item("Field_Name")) & "' AND RimYear=" & ChkN(Year(RY_Start))
                    oDAL.ExecuteCommand(strSql, 0, 0)
                Next
            End If
            ClearAll(Me)
            BlankGrid()
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message)
        Finally
            dtRimMast.Dispose()
            dtRimTran.Dispose()
        End Try
    End Sub

    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Response.Redirect("Main.aspx")
    End Sub
    Sub OnAmountChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Item As DataGridItem
        Dim dt As DataTable
        Dim dr As DataRow
        Try
            Item = CType(sender, Control).NamingContainer()
            dt = viewstate("dtReim")
            dr = dt.Select("FIELD_NAME='" & Item.Cells(12).Text & "'")(0)
            dr.Item("Amount") = ChkN(CType(Item.Cells(8).Controls(1), TextBox).Text)
            dr.Item("Claimed") = ChkN(CType(Item.Cells(7).Controls(1), TextBox).Text)
            dr.Item("Taxable") = ChkN(CType(Item.Cells(9).Controls(1), TextBox).Text)
            dr.Item("TDS") = ChkN(CType(Item.Cells(10).Controls(1), TextBox).Text)
            dr.AcceptChanges()
            dt.AcceptChanges()
            SetReimSettings()
            If Not dr Is Nothing Then
                If CDate(Dtpdate.Text) < dr.Item("RY_Start_WEF") Then
                    SetMsg(LblErrMsg, "Reimbursement Date Cannot Be Less Than " & Format(dr.Item("RY_Start_WEF"), "DD/mmm/yyyy"))
                    dr.Item("Amount") = 0
                    dr.AcceptChanges()
                    GoTo Final
                    'Exit Sub
                End If
                If CDate(Dtpdate.Text) >= DateAdd(DateInterval.Year, 1, dr.Item("RY_Start_WEF")).AddDays(-1) Then
                    SetMsg(LblErrMsg, "Reimbursement Date Cannot Be Greater Than " & DateAdd(DateInterval.Year, 1, dr.Item("RY_Start_WEF")).AddDays(-1))
                    dr.Item("Amount") = 0
                    dr.AcceptChanges()
                    GoTo Final
                    'Exit Sub
                End If
                'dr.Item("Closing") = ChkN(dr.Item("Opn_Bal")) - ChkN(dr.Item("Amount"))
                If ChkN(dr.Item("Amount")) > (ChkN(dr.Item("Claimed")) + ChkN(dr.Item("CarryFwdAmt"))) Then
                    SetMsg(LblErrMsg, "Reimbursed amount can't be more than Claimed amount")
                    dr.Item("Amount") = dr.Item("Claimed")
                    GoTo Final
                End If
                If ChkN(dr.Item("Amount")) > ChkN(dr.Item("Tot_Opn")) Then
                    SetMsg(LblErrMsg, "Reimbursed Amount can't be more then " & ChkN(dr.Item("Tot_Opn")))
                    dr.Item("Amount") = ChkN(dr.Item("Tot_Opn"))
                    dr.AcceptChanges()
                    GoTo Final
                End If
                If ChkN(dr.Item("Opening")) < ChkN(dr.Item("Amount")) Then
                    If bCheckOnYTD Then
                        SetMsg(LblErrMsg, "Reimbursed Amount can't be more than " & ChkN(dr.Item("Opening")))
                        dr.Item("Amount") = ChkN(dr.Item("Opening"))
                        dr.AcceptChanges()
                    End If
                    GoTo Final
                End If
Final:
                dr.Item("Closing") = ChkN(dr.Item("Tot_Opn")) - ChkN(dr.Item("Amount"))
                dr.AcceptChanges()
                dt.AcceptChanges()
                GrdReim.DataSource = ViewState("dtReim")
                GrdReim.DataBind()
                'DataGrid1.DataSource = ViewState("dtReim")
                'DataGrid1.DataBind()
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message)
        End Try
    End Sub
    Sub OnTaxableChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Item As DataGridItem
        Dim dt As DataTable
        Dim dr As DataRow
        Try
            Item = CType(sender, Control).NamingContainer()
            dt = viewstate("dtReim")
            dr = dt.Select("FIELD_NAME='" & Item.Cells(12).Text & "'")(0)
            dr.Item("Amount") = ChkN(CType(Item.Cells(8).Controls(1), TextBox).Text)
            dr.Item("Claimed") = ChkN(CType(Item.Cells(7).Controls(1), TextBox).Text)
            dr.Item("Taxable") = ChkN(CType(Item.Cells(9).Controls(1), TextBox).Text)
            dr.Item("TDS") = ChkN(CType(Item.Cells(10).Controls(1), TextBox).Text)
            dr.AcceptChanges()
            dt.AcceptChanges()
            If Not dr Is Nothing Then
                If ChkN(dr.Item("Taxable")) > ChkN(dr.Item("Amount")) Then
                    SetMsg(LblErrMsg, "Taxable amount can't be more than Reimbursed amount")
                    dr.Item("Taxable") = ChkN(dr.Item("Amount"))
                End If
Final:
                dr.AcceptChanges()
                dt.AcceptChanges()
                GrdReim.DataSource = ViewState("dtReim")
                GrdReim.DataBind()
                'DataGrid1.DataSource = ViewState("dtReim")
                'DataGrid1.DataBind()
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message)
        End Try
    End Sub
    Sub OnTDSChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Item As DataGridItem
        Dim dt As DataTable
        Dim dr As DataRow
        Try
            Item = CType(sender, Control).NamingContainer()
            dt = viewstate("dtReim")
            dr = dt.Select("FIELD_NAME='" & Item.Cells(12).Text & "'")(0)
            dr.Item("Amount") = ChkN(CType(Item.Cells(8).Controls(1), TextBox).Text)
            dr.Item("Claimed") = ChkN(CType(Item.Cells(7).Controls(1), TextBox).Text)
            dr.Item("Taxable") = ChkN(CType(Item.Cells(9).Controls(1), TextBox).Text)
            dr.Item("TDS") = ChkN(CType(Item.Cells(10).Controls(1), TextBox).Text)
            dr.AcceptChanges()
            dt.AcceptChanges()
            If Not dr Is Nothing Then
                If ChkN(dr.Item("TDS")) > ChkN(dr.Item("Taxable")) Then
                    SetMsg(LblErrMsg, "TDS amount can't be more than Taxable amount")
                    dr.Item("TDS") = ChkN(dr.Item("Taxable"))
                End If
Final:
                dr.AcceptChanges()
                dt.AcceptChanges()
                GrdReim.DataSource = ViewState("dtReim")
                GrdReim.DataBind()
                'DataGrid1.DataSource = ViewState("dtReim")
                'DataGrid1.DataBind()
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message)
        End Try
    End Sub
    Private Sub BlankGrid()
        Try
            Dim dt As DataTable
            Dim i As Int16
            dt = viewstate("dtReim")
            Do While dt.Rows.Count > 0
                dt.Rows(0).Delete()
            Loop
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " (BlankGrid)")
        End Try
    End Sub


End Class
