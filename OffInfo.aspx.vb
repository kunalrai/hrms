Partial Class OffInfo
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Server.GetLastError Is Nothing Then SetMsg(LblMsg, "")
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If
        Try
            'By Ravi 17 nov 2006
            Dim SrNo As String
            SrNo = Request.QueryString.Item("SrNo") + Request.QueryString.Item("MODULEID")
            If Not IsPostBack Then
                Dim bSuccess As Boolean
                Select Case CheckRight(SrNo)
                    Case MdlHRMS.AccessType.FullAccess, MdlHRMS.AccessType.SuperUser
                        bSuccess = True
                    Case MdlHRMS.AccessType.ReadonlyAccess, MdlHRMS.AccessType.Restricted
                        bSuccess = False
                End Select
                cmdSave.Enabled = bSuccess
            End If

            'If CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules <> "" Then
            '    If InStr(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, SrNo & "-") <> 0 Then
            '        Dim int As Int16, st As String
            '        int = InStr(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, SrNo)
            '        st = Right(Mid(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, int, Len(SrNo & "-") + 1), 1)

            '        If st = "S" Then
            '            txtEM_CD.ReadOnly = False
            '        Else
            '            txtEM_CD.ReadOnly = True
            '            cmdSave.Visible = False
            '        End If
            '    Else
            '        txtEM_CD.ReadOnly = True
            '        cmdSave.Visible = False
            '        SetMsg(LblRights, "You are not authorised to view data on this page.")
            '        Exit Sub
            '    End If
            'End If

            If Not IsPostBack Then
                txtEM_CD.Text = Session("EM_CD")
                If Chk(txtEM_CD.Text) <> "" Then
                    FillCombo()
                    DisplayRecord()
                Else

                End If
            End If
            cmdSave.Attributes.Add("onclick", "return ValidateCtrl();")

        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (Page_Load)")
        End Try

    End Sub

#Region " Employee Master Detail "

#Region "  Fill Combo Box  "

    Private Sub FillCombo()
        Try

            Dim iTemp As Int16, sTemp2 As String
            '=============================Section Master====================================
            Session("BalObj").FillCombo(cmbSection, "Select Sect_Code,Sect_Name From SectMast Order By Sect_Name", True)

            '=============================Department Master====================================
            Session("BalObj").FillCombo(cmbDepartment, "Select Dept_Code, Dept_Name From DeptMast Order By Dept_Name", True)
            '=============================Location Master====================================
            Session("BalObj").FillCombo(cmbLocation, "Select Loc_Code, Loc_Name From LocMast Order by Loc_Name", True)
            Session("BalObj").FillCombo(cmbALocation, "Select Loc_Code, Loc_Name From LocMast Order by Loc_Name", True)
            Session("BalObj").FillCombo(cmbPLocation, "Select Loc_Code, Loc_Name From LocMast Order by Loc_Name", True)
            Session("BalObj").FillCombo(cmbManager, "Select Emp_Code,Emp_Name + ' (' + isnull(DSG_NAME,'') + ')' as EMP_NAME From HrdMastQry Where Emp_Code Not IN ('" & Chk(txtEM_CD.Text) & "') " & Session("UserCodes") & " And LType=1 Order by Emp_Name", True)

            '=============================Region Master====================================

            Session("BalObj").FillCombo(cmbRegion, "Select Regn_Code, Regn_Name From RegnMast Order By Regn_Name", True)
            '=============================Designation Master====================================

            CType(Session("BalObj"), BAL.BLayer).FillCombo(cmbDesignation, " Select DSG_CODE, Dsg_Name From DsgMast Order By Dsg_Name", True)
            '=============================Employee Type Master====================================

            Session("BalObj").FillCombo(cmbEmpType, "Select Type_Code, Type_Name From EmpType Order By Type_Name", True)
            '=============================Cost Center Master====================================

            Session("BalObj").FillCombo(cmbCostCenter, " Select Cost_Code, Cost_Name From CostMast Order By Cost_Name", True)

            '=============================PROC Master====================================

            Session("BalObj").FillCombo(cmbProcess, "Select Proc_Code, Proc_Name From ProcMast Order By Proc_Name", True)
            '=============================Division Master====================================
            Session("BalObj").FillCombo(cmbDivision, "Select Divi_Code, Divi_Name From DiviMast Order By Divi_Name", True)

            '=============================Grade Master====================================
            Session("BalObj").FillCombo(cmbGrade, "Select Grd_Code, Grd_Name From GrdMast Order By Grd_Name", True)
            dtpDOJ.DateValue = Format(Date.Today, "dd/MMM/yyyy")
            DtpDOC.DateValue = DateAdd(DateInterval.Month, 6, CDate(dtpDOJ.DateValue))

        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (FillCombo)")
        End Try
    End Sub

#End Region

#Region " Validate & Save Employee "

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            If ViewState("Value") = "NULL" Then Exit Sub
            Dim strSQl As String

            strSQl = "Update HrdMast Set " & _
                 " FName  = '" & Chk(txtFName.Text) & "'," & _
                 " LName  = '" & Chk(txtLName.Text) & "'," & _
                 " Dsg_Code  = '" & Chk(cmbDesignation.SelectedValue) & "'," & _
                 " Dept_Code = '" & Chk(cmbDepartment.SelectedValue) & "'," & _
                 " Loc_Code  = '" & Chk(cmbLocation.SelectedValue) & "'," & _
                 " Regn_Code = '" & Chk(cmbRegion.SelectedValue) & "'," & _
                 " Type_Code = '" & Chk(cmbEmpType.SelectedValue) & "'," & _
                 " Mngr_Code = '" & Chk(cmbManager.SelectedValue) & "'," & _
                 " Grd_Code  = '" & Chk(cmbGrade.SelectedValue) & "'," & _
                 " Cost_Code = '" & Chk(cmbCostCenter.SelectedValue) & "'," & _
                 " Proc_Code = '" & Chk(cmbProcess.SelectedValue) & "'," & _
                 " Sect_Code = '" & Chk(cmbSection.SelectedValue) & "'," & _
                 " Divi_Code = '" & Chk(cmbDivision.SelectedValue) & "'," & _
                 " DOJ = '" & Format(CDate(dtpDOJ.DateValue), "dd/MMM/yyyy") & "'," & _
                 " DOC_Due = " & IIf(ChkDOCDue.Checked, "'" & dtpDOCDUE.DateValue & "', ", "Null, ") & _
                 " DOC = " & IIf(ChkDOC.Checked, "'" & dtpDOCDUE.DateValue & "', ", "Null, ") & _
                 " DOCExUPTO = " & IIf(ChkCEUPTO.Checked, "'" & DtpCEUPTO.DateValue & "', ", "Null, ") & _
                 " DOCE = " & IIf(ChkDOCE.Checked, "'" & dtpDOCE.DateValue & "', ", "Null,") & _
                 " DDOEPP = " & IIf(ChkDDEPP.Checked, "'" & DtpDDEPP.DateValue & "', ", "Null,") & _
                 " DDOR = " & IIf(ChkDDR.Checked, "'" & DtpDDR.DateValue & "' ", "Null") & _
                 " Where Emp_Code = '" & Chk(txtEM_CD.Text) & "'"
            Dim RowsAffected As Long
            RowsAffected = Session("DalObj").ExecuteCommand(strSQl)
            SetMsg(LblMsg, "Records Has Been Saved Successfully.")
            DisplayRecord()
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (cmdSave_Click)")
        End Try
    End Sub
#End Region

#Region "  Display Employee Record  "

    Private Sub DisplayRecord()
        Dim dtEmp As New DataTable
        Try

            Dim StrSQl As String
            ViewState("Value") = "NULL"
            StrSQl = "Select * From HrdMastQry Where Emp_Code = '" & Chk(txtEM_CD.Text) & "'" & Session("UserCodes")
            Session("DalObj").GetSqlDataTable(dtEmp, StrSQl)
            If dtEmp.Rows.Count > 0 Then
                With dtEmp.Rows(0)
                    ViewState("Value") = "MODI"
                    txtFName.Text = Chk(.Item("FName"))
                    txtLName.Text = Chk(.Item("LName"))

                    ChkCombo(cmbDesignation, .Item("Dsg_Code"))
                    ChkCombo(cmbDepartment, .Item("Dept_Code"))
                    ChkCombo(cmbLocation, .Item("Loc_Code"))
                    ChkCombo(cmbRegion, .Item("Regn_Code"))
                    ChkCombo(cmbEmpType, .Item("Type_Code"))
                    ChkCombo(cmbManager, .Item("Mngr_Code"))
                    ChkCombo(cmbGrade, .Item("Grd_Code"))
                    ChkCombo(cmbCostCenter, .Item("Cost_Code"))
                    ChkCombo(cmbProcess, .Item("Proc_Code"))
                    ChkCombo(cmbSection, .Item("Sect_Code"))
                    ChkCombo(cmbDivision, .Item("Divi_Code"))

                    If IsDBNull(.Item("DOJ")) Then
                        dtpDOJ.DateValue = Format(Date.Today, "dd/MMM/yyyy")
                    Else
                        dtpDOJ.DateValue = Format(CDate(.Item("DOJ")), "dd/MMM/yyyy")
                    End If



                    If IsDBNull(.Item("DOCE")) Then
                        ChkDOCE.Checked = False
                        dtpDOCE.Enabled = False
                        dtpDOCE.DateValue = Date.Today
                    Else
                        ChkDOCE.Checked = True
                        dtpDOCE.Enabled = True
                        dtpDOCE.DateValue = CDate(.Item("DOCE"))
                    End If

                    If IsDBNull(.Item("DOC")) Then
                        ChkDOC.Checked = False
                        DtpDOC.Enabled = False
                        DtpDOC.DateValue = Date.Today
                    Else
                        ChkDOC.Checked = True
                        DtpDOC.Enabled = True
                        DtpDOC.DateValue = CDate(.Item("DOC"))
                    End If


                    If IsDBNull(.Item("DOC_Due")) Then
                        ChkDOCDue.Checked = False
                        dtpDOCDUE.Enabled = False
                        dtpDOCDUE.DateValue = Date.Today
                    Else
                        ChkDOCDue.Checked = True
                        dtpDOCDUE.Enabled = True
                        dtpDOCDUE.DateValue = CDate(.Item("DOC_Due"))
                    End If

                    If IsDBNull(.Item("DOCExUPTO")) Then
                        ChkCEUPTO.Checked = False
                        DtpCEUPTO.Enabled = False
                        DtpCEUPTO.DateValue = Date.Today
                    Else
                        ChkCEUPTO.Checked = True
                        DtpCEUPTO.Enabled = True
                        DtpCEUPTO.DateValue = CDate(.Item("DOCExUPTO"))
                    End If

                    If IsDBNull(.Item("DDOEPP")) Then
                        ChkDDEPP.Checked = False
                        DtpDDEPP.Enabled = False
                        DtpDDEPP.DateValue = Date.Today
                    Else
                        ChkDDEPP.Checked = True
                        DtpDDEPP.Enabled = True
                        DtpDDEPP.DateValue = CDate(.Item("DDOEPP"))
                    End If

                    If IsDBNull(.Item("DDOR")) Then
                        ChkDDR.Checked = False
                        DtpDDR.Enabled = False
                        DtpDDR.DateValue = Date.Today
                    Else
                        ChkDDR.Checked = True
                        DtpDDR.Enabled = True
                        DtpDDR.DateValue = CDate(.Item("DDOR"))
                    End If

                End With
            Else
                Dim Code As Object
                Code = Session("DalObj").ExecuteCommand("Select Emp_Code From HrdMastQry Where Emp_Code = '" & Chk(txtEM_CD.Text) & "'" & Session("UserCodes"), , DAL.DataLayer.ExecutionType.ExecuteScalar)

                If Code <> "" Then
                    SetMsg(LblMsg, "This Employee Code Exist For Other Location.")
                Else

                End If
                ViewState("Value") = "NULL"
                ClearAll(Me)
                txtEM_CD.Text = Session("EM_CD")
                dtpDOJ.DateValue = Format(Date.Today, "dd/MMM/yyyy")
            End If
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (DisplayRec)")
        Finally
            dtEmp.Dispose()
        End Try
    End Sub

#End Region

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("EmpExplorer.aspx?SrNo=61")
    End Sub

    Private Sub txtEM_CD_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEM_CD.TextChanged
        If Trim(txtEM_CD.Text) <> "" Then Session("EM_CD") = Trim(txtEM_CD.Text)
        If ViewState("Combo") = "Empty" Or ViewState("Combo") = Nothing Then
            If Trim(txtEM_CD.Text) <> "" Then
                FillCombo()
                ViewState("Combo") = "Filled"
            Else
                ViewState("Combo") = "Empty"
            End If
        End If

        DisplayRecord()
        RegisterStartupScript("txtEM_CD", "<SCRIPT language='javascript'>document.getElementById('txtFName').focus() </SCRIPT>")
    End Sub
#End Region

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub
End Class
