Partial Class EmpMast
    Inherits System.Web.UI.Page
    Public Ds As New DataSet
    Public Delegate Sub FillComboBox()
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Textbox1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents LblName As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents CmbSuperVisor As System.Web.UI.WebControls.DropDownList
    Protected WithEvents DtpBEDate As DITWebLibrary.DTPCombo
    Protected WithEvents ChkBEntry As System.Web.UI.HtmlControls.HtmlInputCheckBox
    Protected WithEvents HyperLink1 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents txtFull As System.Web.UI.WebControls.TextBox

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
        Ajax.Utility.RegisterTypeForAjax(GetType(EmpMast))
        If Server.GetLastError Is Nothing Then SetMsg(LblMsg, "")
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If
        Try
            'By Ravi 17 nov 2006
            'for save and read 
            Dim SrNo As Int16
            SrNo = Request.QueryString.Item("SrNo") + Request.QueryString.Item("MODULEID")
            If Not IsPostBack Then
                ' Dim bSuccess As Boolean
                Select Case CheckRight(SrNo)
                    Case MdlHRMS.AccessType.FullAccess, MdlHRMS.AccessType.SuperUser
                        'bSuccess = True
                        TxtRights.Text = "S"
                    Case MdlHRMS.AccessType.ReadonlyAccess
                        TxtRights.Text = "V"
                    Case MdlHRMS.AccessType.Restricted
                        Response.Redirect(Request.UrlReferrer.ToString)
                        Exit Sub

                        '  bSuccess = False
                End Select
                '  cmdSave.Enabled = bSuccess

            End If

            'comment by ravi 
            'If CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules <> "" Then
            '    If InStr(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, SrNo & "-") <> 0 Then
            '        Dim int As Int16, st As String
            '        int = InStr(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, SrNo)
            '        st = Right(Mid(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, int, Len(SrNo & "-") + 1), 1)

            '        If st = "S" Then
            '            txtEM_CD.ReadOnly = False
            '        Else
            '            txtEM_CD.ReadOnly = True
            '            ''''''BY RAVI''' cmdSave.Visible = False
            '        End If
            '    Else
            '        txtEM_CD.ReadOnly = True
            '        '''BY RAVI'''''''''cmdSave.Visible = False
            '        SetMsg(LblRights, "You are not authorised to view data on this page.")
            '        'Response.Write("<BR><center><B></B></center>")
            '        Exit Sub
            '    End If
            'End If


            'If Session("LoginUser").UserGroup <> "ADMIN" Then
            '    txtEM_CD.ReadOnly = True
            '    'EnableDisable(False, Me)
            '    cmdSave.Visible = False
            '    'cmdNew.Visible = False
            'Else
            '    txtEM_CD.ReadOnly = Falsefunction selChanged(ctrcom,ctrtxt)
            'End If

            If Not IsPostBack Then

                txtEM_CD.Text = Session("EM_CD")

                cmbDesignation.Attributes.Add("onchange", "selChanged('cmbDesignation','txtDesignation');")
                cmbDepartment.Attributes.Add("onchange", "selChanged('cmbDepartment','txtDepartment');")
                cmbLocation.Attributes.Add("onchange", "selChanged('cmbLocation','txtLocation');")
                cmbALocation.Attributes.Add("onchange", "selChanged('cmbALocation','txtALocation');")
                cmbPLocation.Attributes.Add("onchange", "selChanged('cmbPLocation','txtPLocation');")
                cmbEmpType.Attributes.Add("onchange", "selChanged('cmbEmpType','txtEmpType');")
                cmbJobName.Attributes.Add("onchange", "selChanged('cmbJobName','txtJobName');")
                cmbEmpClass.Attributes.Add("onchange", "selChanged('cmbEmpClass','txtEmpClass');")
                cmbHrMngr.Attributes.Add("onchange", "selChanged('cmbHrMngr','txtHrMngr');")
                cmbManager.Attributes.Add("onchange", "selChanged('cmbManager','txtManager');")
                cmbGrade.Attributes.Add("onchange", "selChanged('cmbGrade','txtGrade');")
                cmbCostCenter.Attributes.Add("onchange", "selChanged('cmbCostCenter','txtCostCenter');")
                cmbProcess.Attributes.Add("onchange", "selChanged('cmbProcess','txtProcess');")
                cmbRegion.Attributes.Add("onchange", "selChanged('cmbRegion','txtRegion');")
                cmbSection.Attributes.Add("onchange", "selChanged('cmbSection','txtSection');")
                cmbDivision.Attributes.Add("onchange", "selChanged('cmbDivision','txtDivision');")
                'cmbFull.Attributes.Add("onchange", "selChanged('cmbFull','txtFull');")
                cmbCosting.Attributes.Add("onchange", "selChanged('cmbCosting','txtCosting');")
                cmbUnit.Attributes.Add("onchange", "selChanged('cmbUnit','txtUnit');")
                cmbContType.Attributes.Add("onchange", "selChanged('cmbContType','txtContType');")




                ' FillCombo()
                ' DisplayRecord()

                '''M********************************
                'Response.Write("Process Started")
                'Dim CallBack As AsyncCallback
                'CallBack = New AsyncCallback(AddressOf FCallBack)
                'Dim dlgPay As FillComboBox
                'dlgPay = New FillComboBox(AddressOf FillDrop)
                ''**********
                'Application("isCalcOn") = True
                ''**********
                'dlgPay.BeginInvoke(CallBack, 0)


                '*M*****************************************


                'If Chk(txtEM_CD.Text) <> "" Then
                '    'DisplayRecord()
                'Else

                'End If
            End If


            ''''BY RAVI'''''''''   define in "Public Function Save_Record............"
            'cmdSave.Attributes.Add("onclick", "return ValidateCtrl();")


        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (Page_Load)")
        End Try
    End Sub

    Public Sub FCallBack(Optional ByVal result As IAsyncResult = Nothing)
        Response.Write("Process Completed")
    End Sub
    Public Sub FillDrop()
        Response.Write("Process Started111")
        FillCombo()
        Response.Write("Process Started123")
    End Sub
    '<Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
    'Public Function ShowRecord() As String

    'End Function
    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
    Public Function GetNextEmpRec(ByVal OEmpCode As String) As String
        Try

            Dim strSql As String
            Dim EmpCode As String
            'strSql = "Select Top 1 Emp_Code From HRDMASTQRY Where EMP_CODE>'" & OEmpCode & "' Order By Emp_Code "
            strSql = "Select Top 1 Emp_Code From HRDMASTQRY Where EMP_CODE >" & "'" & OEmpCode & "' " & Session("UserCodes") & "  Order By Emp_Code "
            EmpCode = Chk(Session("DalObj").ExecuteCommand(strSql, DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
            GetNextEmpRec = EmpCode
        Catch ex As Exception

        End Try
    End Function

    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
    Public Function GetPreviousEmpRec(ByVal OEmpCode As String) As String
        Try
            Dim strSql As String
            Dim EmpCode As String
            'strSql = "Select Top 1 Emp_Code From HRDMASTQRY Where EMP_CODE<'" & OEmpCode & "' Order By Emp_Code desc "
            strSql = "Select Top 1 Emp_Code From HRDMASTQRY Where EMP_CODE< " & "'" & OEmpCode & "'" & Session("UserCodes") & " Order By Emp_Code desc "
            EmpCode = Chk(Session("DalObj").ExecuteCommand(strSql, DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
            GetPreviousEmpRec = EmpCode
        Catch ex As Exception

        End Try
    End Function

    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
    Public Function GetFirstEmpRec() As String
        Try
            Dim strSql As String
            Dim EmpCode As String
            ' strSql = "Select Top 1 Emp_Code From HRDMASTQRY  Order By Emp_Code "
            strSql = "Select Top 1 Emp_Code From HRDMASTQRY where 1=1" & Session("UserCodes") & " Order By Emp_Code "
            EmpCode = Chk(Session("DalObj").ExecuteCommand(strSql, DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
            GetFirstEmpRec = EmpCode
        Catch ex As Exception

        End Try
    End Function

    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
    Public Function GetLastEmpRec() As String
        Try
            Dim strSql As String
            Dim EmpCode As String
            'strSql = "Select Top 1 Emp_Code From HRDMASTQRY  Order By Emp_Code desc"
            strSql = "Select Top 1 Emp_Code From HRDMASTQRY Where 1=1 " & Session("UserCodes") & " Order By Emp_Code desc"
            EmpCode = Chk(Session("DalObj").ExecuteCommand(strSql, DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
            GetLastEmpRec = EmpCode
        Catch ex As Exception

        End Try
    End Function

    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
    Public Function GetEmpRec() As DataSet
        Try
            Dim strSql As String

            'strSql = "Select * From HRDMASTQRY Where EMP_CODE='" & Session("EM_CD") & "'"
            strSql = "Select * From HRDMASTQRY Where EMP_CODE=" & "'" & Session("EM_CD") & "'" & Session("UserCodes")
            Session("DalObj").GetSqlDataSet(Ds, strSql)

            'Dim str2 As String = Ds.Tables(0).Rows(0).Item("EMP_CODE")
            'txtDepartment = Ds.Tables(0).Rows(0).Item("EMP_CODE")
            GetEmpRec = Ds


        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Function

    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
    Public Function SetEmpCode(ByVal EmpCode As String) As DataSet
        Try
            Session("EM_CD") = EmpCode
        Catch ex As Exception

        End Try
    End Function

    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
    Public Function SetCurrentEmpCode() As String
        Try
            SetCurrentEmpCode = Session("EM_CD")
        Catch ex As Exception

        End Try
    End Function
    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
    Public Function GetCombovalue(ByVal tabName As String, ByVal txtfield As String, ByVal valField As String) As String
        Try
            Dim strSql As String
            'strSql = "SELECT 'TT' AS UNIT_CODE,'TT' AS UNIT_DESC UNION "
            strSql &= "Select " & txtfield & "," & valField & " From " & tabName & " order by " & txtfield & "  ;select " & "'" & txtfield & "' as TF,'" & valField & "' as VF"
            'Session("DalObj").GetSqlDataSet(Ds, strSql)
            Session("strqry") = strSql
            GetCombovalue = strSql

        Catch ex As Exception

        End Try
    End Function
    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
    Public Function ComboBind() As DataSet
        Try
            Dim strSql As String
            Session("DalObj").GetSqlDataSet(Ds, Session("strqry"))
            ComboBind = Ds
            ' Dim str As String = Ds.Tables(0).Columns(0).ColumnName
            'Dim str2 As String = str
        Catch ex As Exception
            Dim str As String
            str = ex.Message
        End Try
    End Function
#Region " Employee Master Detail"

    Private Sub FillCombo(Optional ByVal result As IAsyncResult = Nothing)
        Try


            Dim iTemp As Int16, sTemp2 As String
            '=============================Section Master====================================
            If InStr(UCase(Session("UserCodes")), "SECT_CODE") > 0 Then
                iTemp = InStr(UCase(Session("UserCodes")), "SECT_CODE")
                sTemp2 = Mid(UCase(Trim(Session("UserCodes"))), iTemp - 1, Len(Trim(Session("UserCodes"))) - (iTemp - 2))
                If InStr(sTemp2, "AND") > 0 Then
                    sTemp2 = Mid(UCase(sTemp2), 1, Len(sTemp2) - (InStr(sTemp2, "AND") + 2))
                End If
                Session("BalObj").FillCombo(cmbSection, "Select Sect_Code,Sect_Name From SectMast Where " & sTemp2 & " Order By Sect_Name ", True)
            Else
                Session("BalObj").FillCombo(cmbSection, "Select Sect_Code,Sect_Name From SectMast Order By Sect_Name", True)
            End If

            '=============================Department Master====================================
            If InStr(UCase(Session("UserCodes")), "DEPT_CODE") > 0 Then
                iTemp = InStr(UCase(Session("UserCodes")), "DEPT_CODE")
                sTemp2 = Mid(UCase(Trim(Session("UserCodes"))), iTemp - 1, Len(Trim(Session("UserCodes"))) - (iTemp - 2))
                If InStr(sTemp2, "AND") > 0 Then
                    sTemp2 = Mid(UCase(sTemp2), 1, Len(sTemp2) - (InStr(sTemp2, "AND") + 2))
                End If
                Session("BalObj").FillCombo(cmbDepartment, "Select Dept_Code, Dept_Name From DeptMast Where " & sTemp2 & " Order By Dept_Name ", True)
            Else
                Session("BalObj").FillCombo(cmbDepartment, "Select Dept_Code, Dept_Name From DeptMast Order By Dept_Name", True)
            End If



            '=============================Sub Dept. Master====================================
            '''If InStr(UCase(Session("UserCodes")), "SUBDEPT_CODE") > 0 Then
            '''    iTemp = InStr(UCase(Session("UserCodes")), "SUBDEPT_CODE")
            '''    sTemp2 = Mid(UCase(Trim(Session("UserCodes"))), iTemp - 1, Len(Trim(Session("UserCodes"))) - (iTemp - 2))
            '''    If InStr(sTemp2, "AND") > 0 Then
            '''        sTemp2 = Mid(UCase(sTemp2), 1, Len(sTemp2) - (InStr(sTemp2, "AND") + 2))
            '''    End If
            '''    Session("BalObj").FillCombo(cmbProcess, "Select SUBDEPT_CODE, SUBDEPT_Name From SubDeptMast Where " & sTemp2 & " Order By SUBDEPT_Name ", True)
            '''Else
            '''    Session("BalObj").FillCombo(cmbProcess, "Select SUBDEPT_CODE, SUBDEPT_Name From SubDeptMast Order By SUBDEPT_Name", True)
            '''End If

            '=============================Location Master====================================
            If InStr(UCase(Session("UserCodes")), "LOC_CODE") > 0 Then
                iTemp = InStr(UCase(Session("UserCodes")), "LOC_CODE")
                sTemp2 = Mid(UCase(Trim(Session("UserCodes"))), iTemp - 1, Len(Trim(Session("UserCodes"))) - (iTemp - 2))
                If InStr(sTemp2, "AND") > 0 Then
                    sTemp2 = Mid(UCase(sTemp2), 1, Len(sTemp2) - (InStr(sTemp2, "AND") + 2))
                End If
                Session("BalObj").FillCombo(cmbLocation, "Select Loc_Code, Loc_Name From LocMast Where " & sTemp2 & " Order By Loc_Name ", True)
            Else
                Session("BalObj").FillCombo(cmbLocation, "Select Loc_Code, Loc_Name From LocMast Order by Loc_Name", True)
            End If

            Session("BalObj").FillCombo(cmbALocation, "Select Loc_Code, Loc_Name From LocMast Order by Loc_Name", True)
            Session("BalObj").FillCombo(cmbPLocation, "Select Loc_Code, Loc_Name From LocMast Order by Loc_Name", True)

            Session("BalObj").FillCombo(cmbJobName, "Select job_Code, job_Code From JobMast Order by Job_CODE", True)
            Session("BalObj").FillCombo(cmbUnit, "Select Unit_Code, Unit_DESC From BUnitMast Order by Unit_DESC", True)

            Session("BalObj").FillCombo(cmbContType, "Select CONT_Type, CONT_Type From ContractType  Order by CONT_Type", True)
            Session("BalObj").FillCombo(cmbEmpClass, "Select EMP_CLASS, EMP_DESC From EMPCLASSMAST Order by EMP_DESC", True)
            Session("BalObj").FillCombo(cmbCosting, "Select CostType_Code, CostType_Desc From COSTTYPEMAST Order by CostType_Desc", True)

            Session("BalObj").FillCombo(cmbManager, "Select Emp_Code,Emp_Name + ' (' + isnull(DSG_NAME,'') + ')' as EMP_NAME From HrdMastQry Where Emp_Code Not IN ('" & Chk(txtEM_CD.Text) & "') " & Session("UserCodes") & " And LType=1 Order by Emp_Name", True)
            Session("BalObj").FillCombo(cmbHrMngr, "Select Emp_Code,Emp_Name + ' (' + isnull(DSG_NAME,'') + ')' as EMP_NAME From HrdMastQry Where Emp_Code Not IN ('" & Chk(txtEM_CD.Text) & "') " & Session("UserCodes") & " And LType=1 Order by Emp_Name", True)

            '=============================Region Master====================================
            If InStr(UCase(Session("UserCodes")), "REGN_CODE") > 0 Then
                iTemp = InStr(UCase(Session("UserCodes")), "REGN_CODE")
                sTemp2 = Mid(UCase(Trim(Session("UserCodes"))), iTemp - 1, Len(Trim(Session("UserCodes"))) - (iTemp - 2))
                If InStr(sTemp2, "AND") > 0 Then
                    sTemp2 = Mid(UCase(sTemp2), 1, Len(sTemp2) - (InStr(sTemp2, "AND") + 2))
                End If
                Session("BalObj").FillCombo(cmbRegion, " Select Regn_Code, Regn_Name From RegnMast Where " & sTemp2 & " Order By Regn_Name ", True)
            Else
                Session("BalObj").FillCombo(cmbRegion, "Select Regn_Code, Regn_Name From RegnMast Order By Regn_Name", True)
            End If

            '=============================Designation Master====================================
            If InStr(UCase(Session("UserCodes")), "DSG_CODE") > 0 Then
                iTemp = InStr(UCase(Session("UserCodes")), "DSG_CODE")
                sTemp2 = Mid(UCase(Trim(Session("UserCodes"))), iTemp - 1, Len(Trim(Session("UserCodes"))) - (iTemp - 2))
                If InStr(sTemp2, "AND") > 0 Then
                    sTemp2 = Mid(UCase(sTemp2), 1, Len(sTemp2) - (InStr(sTemp2, "AND") + 2))
                End If
                CType(Session("BalObj"), BAL.BLayer).FillCombo(cmbDesignation, " Select DSG_CODE, Dsg_Name From DsgMast Where " & sTemp2 & " Order By Dsg_Name ", True)
            Else
                CType(Session("BalObj"), BAL.BLayer).FillCombo(cmbDesignation, " Select DSG_CODE, Dsg_Name From DsgMast Order By Dsg_Name", True)
            End If


            '=============================Employee Type Master====================================
            If InStr(UCase(Session("UserCodes")), "TYPE_CODE") > 0 Then
                iTemp = InStr(UCase(Session("UserCodes")), "TYPE_CODE")
                sTemp2 = Mid(UCase(Trim(Session("UserCodes"))), iTemp - 1, Len(Trim(Session("UserCodes"))) - (iTemp - 2))
                If InStr(sTemp2, "AND") > 0 Then
                    sTemp2 = Mid(UCase(sTemp2), 1, Len(sTemp2) - (InStr(sTemp2, "AND") + 2))
                End If
                Session("BalObj").FillCombo(cmbEmpType, " Select Type_Code, Type_Name From EmpType Where " & sTemp2 & " Order By Type_Name ", True)
            Else
                Session("BalObj").FillCombo(cmbEmpType, "Select Type_Code, Type_Name From EmpType Order By Type_Name", True)
            End If


            '=============================Cost Center Master====================================
            If InStr(UCase(Session("UserCodes")), "COST_CODE") > 0 Then
                iTemp = InStr(UCase(Session("UserCodes")), "COST_CODE")
                sTemp2 = Mid(UCase(Trim(Session("UserCodes"))), iTemp - 1, Len(Trim(Session("UserCodes"))) - (iTemp - 2))
                If InStr(sTemp2, "AND") > 0 Then
                    sTemp2 = Mid(UCase(sTemp2), 1, Len(sTemp2) - (InStr(sTemp2, "AND") + 2))
                End If
                Session("BalObj").FillCombo(cmbCostCenter, " Select Cost_Code, Cost_Name From CostMast Where " & sTemp2 & " Order By Cost_Name ", True)
            Else
                Session("BalObj").FillCombo(cmbCostCenter, " Select Cost_Code, Cost_Name From CostMast Order By Cost_Name", True)
            End If

            '=============================PROC Master====================================
            If InStr(UCase(Session("UserCodes")), "PROC_CODE") > 0 Then
                iTemp = InStr(UCase(Session("UserCodes")), "PROC_CODE")
                sTemp2 = Mid(UCase(Trim(Session("UserCodes"))), iTemp - 1, Len(Trim(Session("UserCodes"))) - (iTemp - 2))
                If InStr(sTemp2, "AND") > 0 Then
                    sTemp2 = Mid(UCase(sTemp2), 1, Len(sTemp2) - (InStr(sTemp2, "AND") + 2))
                End If
                Session("BalObj").FillCombo(cmbProcess, "Select Proc_Code, Proc_Name From ProcMast Where " & sTemp2 & " Order By Proc_Name ", True)
            Else
                Session("BalObj").FillCombo(cmbProcess, "Select Proc_Code, Proc_Name From ProcMast Order By Proc_Name", True)
            End If


            '=============================Division Master====================================
            If InStr(UCase(Session("UserCodes")), "DIVI_CODE") > 0 Then
                iTemp = InStr(UCase(Session("UserCodes")), "DIVI_CODE")
                sTemp2 = Mid(UCase(Trim(Session("UserCodes"))), iTemp - 1, Len(Trim(Session("UserCodes"))) - (iTemp - 2))
                If InStr(sTemp2, "AND") > 0 Then
                    sTemp2 = Mid(UCase(sTemp2), 1, Len(sTemp2) - (InStr(sTemp2, "AND") + 2))
                End If
                Session("BalObj").FillCombo(cmbDivision, " Select Divi_Code, Divi_Name From DiviMast Where " & sTemp2 & " Order By Divi_Name ", True)
            Else
                Session("BalObj").FillCombo(cmbDivision, "Select Divi_Code, Divi_Name From DiviMast Order By Divi_Name", True)
            End If

            '=============================Grade Master====================================
            If InStr(UCase(Session("UserCodes")), "GRD_CODE") > 0 Then
                iTemp = InStr(UCase(Session("UserCodes")), "GRD_CODE")
                sTemp2 = Mid(UCase(Trim(Session("UserCodes"))), iTemp - 1, Len(Trim(Session("UserCodes"))) - (iTemp - 2))
                If InStr(sTemp2, "AND") > 0 Then
                    sTemp2 = Mid(UCase(sTemp2), 1, Len(sTemp2) - (InStr(sTemp2, "AND") + 2))
                End If
                Session("BalObj").FillCombo(cmbGrade, " Select Grd_Code, Grd_Name From GrdMast Where " & sTemp2 & " Order By Grd_Name ", True)
            Else
                Session("BalObj").FillCombo(cmbGrade, "Select Grd_Code, Grd_Name From GrdMast Order By Grd_Name", True)
            End If

            dtpDOJ.DateValue = Format(Date.Today, "dd/MMM/yyyy")
            DtpDOC.DateValue = DateAdd(DateInterval.Month, 6, CDate(dtpDOJ.DateValue))

        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (FillCombo)")
        End Try
    End Sub
#Region " Validate & Save Employee "

    'Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
    '    Try
    '        If ViewState("Value") = "NULL" Then Exit Sub
    '        If isValidate() Then
    '            Dim strSQl As String




    '            'strSQl = "Update HrdMast Set " & _
    '            '     " FName  = '" & Chk(txtFName.Text) & "'," & _
    '            '     " LName  = '" & Chk(txtLName.Text) & "'," & _
    '            '     " Dsg_Code  = '" & Chk(cmbDesignation.SelectedValue) & "'," & _
    '            '     " Dept_Code = '" & Chk(cmbDepartment.SelectedValue) & "'," & _
    '            '     " Loc_Code  = '" & Chk(cmbLocation.SelectedValue) & "'," & _
    '            '     " Regn_Code = '" & Chk(cmbRegion.SelectedValue) & "'," & _
    '            '     " Type_Code = '" & Chk(cmbEmpType.SelectedValue) & "'," & _
    '            '     " Mngr_Code = '" & Chk(cmbManager.SelectedValue) & "'," & _
    '            '     " Grd_Code  = '" & Chk(cmbGrade.SelectedValue) & "'," & _
    '            '     " Cost_Code = '" & Chk(cmbCostCenter.SelectedValue) & "'," & _
    '            '     " Company = '" & Chk(TxtComp.Text) & "'," & _
    '            '     " JobRank = '" & Chk(TxtJobRanking.Text) & "'," & _
    '            '     " LSA_PLAN = '" & Chk(TxtSalAdminPlan.Text) & "'," & _
    '            '     " Buss_Title = '" & Chk(TxtBTitle.Text) & "'," & _
    '            '     " Proc_Code = '" & Chk(cmbProcess.SelectedValue) & "'," & _
    '            '     " HR_MNGR = '" & Chk(CmbHrMngr.SelectedValue) & "'," & _
    '            '     " COSTTYPE_CODE = '" & Chk(CmbCosting.SelectedValue) & "'," & _
    '            '     " EMP_CLASS = '" & Chk(CmbEmpClass.SelectedValue) & "'," & _
    '            '     " jOB_CODE = '" & Chk(CmbJobName.SelectedValue) & "'," & _
    '            '     " FULL_PART = '" & Chk(CmbFull.SelectedValue) & "'," & _
    '            '     " Unit_Code = '" & Chk(CmbUnit.SelectedValue) & "'," & _
    '            '     " Cont_Type = '" & Chk(CmbContType.SelectedValue) & "'," & _
    '            '     " Sect_Code = '" & Chk(cmbSection.SelectedValue) & "'," & _
    '            '     " Divi_Code = '" & Chk(cmbDivision.SelectedValue) & "'," & _
    '            '     " DOJ = '" & Format(CDate(dtpDOJ.DateValue), "dd/MMM/yyyy") & "'," & _
    '            '     " DOC_Due = " & IIf(ChkDOCDUE.Checked, "'" & dtpDOCDUE.DateValue & "', ", "Null, ") & _
    '            '     " SERV_DATE = " & IIf(ChkDDEDPE.Checked, "'" & DtpDDEDPE.DateValue & "', ", "Null, ") & _
    '            '     " DOC = " & IIf(ChkDOC.Checked, "'" & dtpDOCDUE.DateValue & "', ", "Null, ") & _
    '            '     " DOCExUPTO = " & IIf(ChkCEUPTO.Checked, "'" & DtpCEUPTO.DateValue & "', ", "Null, ") & _
    '            '     " DOCE = " & IIf(ChkDOCE.Checked, "'" & dtpDOCE.DateValue & "', ", "Null,") & _
    '            '     " DDOEPP = " & IIf(ChkDDEPP.Checked, "'" & DtpDDEPP.DateValue & "', ", "Null,") & _
    '            '     " DDOR = " & IIf(ChkDDR.Checked, "'" & DtpDDR.DateValue & "' ", "Null") & _
    '            '     " Where Emp_Code = '" & Chk(txtEM_CD.Text) & "'"




    '            strSQl = "Update HrdMast Set " & _
    '                " FName  = '" & Chk(txtFName.Text) & "'," & _
    '                " LName  = '" & Chk(txtLName.Text) & "'," & _
    '                " Dsg_Code  = '" & Chk(txtDesignation.Text) & "'," & _
    '                " Dept_Code = '" & Chk(txtDepartment.Text) & "'," & _
    '                " Loc_Code  = '" & Chk(txtLocation.Text) & "'," & _
    '                " Regn_Code = '" & Chk(txtRegion.Text) & "'," & _
    '                " Type_Code = '" & Chk(txtEmpType.Text) & "'," & _
    '                " Mngr_Code = '" & Chk(txtManager.Text) & "'," & _
    '                " Grd_Code  = '" & Chk(txtGrade.Text) & "'," & _
    '                " Cost_Code = '" & Chk(txtCostCenter.Text) & "'," & _
    '                " Company = '" & Chk(TxtComp.Text) & "'," & _
    '                " JobRank = '" & Chk(TxtJobRanking.Text) & "'," & _
    '                " LSA_PLAN = '" & Chk(TxtSalAdminPlan.Text) & "'," & _
    '                " Buss_Title = '" & Chk(TxtBTitle.Text) & "'," & _
    '                " Proc_Code = '" & Chk(txtProcess.Text) & "'," & _
    '                " HR_MNGR = '" & Chk(txtHrMngr.Text) & "'," & _
    '                " COSTTYPE_CODE = '" & Chk(txtCosting.Text) & "'," & _
    '                " EMP_CLASS = '" & Chk(txtEmpClass.Text) & "'," & _
    '                " jOB_CODE = '" & Chk(txtJobName.Text) & "'," & _
    '                " FULL_PART = '" & Chk(txtFull.Text) & "'," & _
    '                " Unit_Code = '" & Chk(txtUnit.Text) & "'," & _
    '                " Cont_Type = '" & Chk(txtContType.Text) & "'," & _
    '                " Sect_Code = '" & Chk(txtSection.Text) & "'," & _
    '                " Divi_Code = '" & Chk(txtDivision.Text) & "'," & _
    '                " DOJ = '" & Format(CDate(dtpDOJ.DateValue), "dd/MMM/yyyy") & "'," & _
    '                " DOC_Due = " & IIf(ChkDOCDUE.Checked, "'" & dtpDOCDUE.DateValue & "', ", "Null, ") & _
    '                " SERV_DATE = " & IIf(ChkDDEDPE.Checked, "'" & dtpDDEDPE.DateValue & "', ", "Null, ") & _
    '                " DOC = " & IIf(ChkDOC.Checked, "'" & dtpDOCDUE.DateValue & "', ", "Null, ") & _
    '                " DOCExUPTO = " & IIf(ChkCEUPTO.Checked, "'" & dtpCEUPTO.DateValue & "', ", "Null, ") & _
    '                " DOCE = " & IIf(ChkDOCE.Checked, "'" & dtpDOCE.DateValue & "', ", "Null,") & _
    '                " DDOEPP = " & IIf(ChkDDEPP.Checked, "'" & dtpDDEPP.DateValue & "', ", "Null,") & _
    '                " DDOR = " & IIf(ChkDDR.Checked, "'" & dtpDDR.DateValue & "' ", "Null") & _
    '                " Where Emp_Code = '" & Chk(txtEM_CD.Text) & "'"
    '            Dim RowsAffected As Long
    '            RowsAffected = Session("DalObj").ExecuteCommand(strSQl)
    '            SetMsg(LblMsg, "Records Has Been Saved Successfully.")
    '            ' DisplayRecord()
    '        End If
    '    Catch ex As Exception
    '        SetMsg(LblMsg, ex.Message & " : (cmdSave_Click)")
    '    End Try
    'End Sub


    Private Function isValidate() As Boolean
        Try
            'Dim i, j, k As Int16
            'If Trim(txtFName.Text) = "" Then
            '    isValidate = False
            '    SetMsg(LblMsg, "Please Enter First Name of Employee.")
            '    Exit Function
            'ElseIf IsNumeric(txtFName.Text) Then
            '    isValidate = False
            '    SetMsg(LblMsg, "First Name must be string type.")
            '    Exit Function
            'End If

            'If IsNumeric(txtLName.Text) Then
            '    isValidate = False
            '    SetMsg(LblMsg, "Last Name must be string type.")
            '    Exit Function
            'End If

            'If Not IsDate(dtpDOJ.DateValue) Then
            '    isValidate = False
            '    SetMsg(LblMsg, "Invalid Date of Joining.")
            '    Exit Function
            'End If
            'If ChkDOCE.Checked = True Then
            '    i = Date.Compare(dtpDOCE.DateValue, CDate(dtpDOJ.DateValue))
            '    If i < 0 Then
            '        SetMsg(LblMsg, "Contract End Date Can't Before the Date of Joining.")
            '        Return False
            '    End If
            'End If
            'If ChkDOCDue.Checked = True Then
            '    j = Date.Compare(dtpDOCDUE.DateValue, CDate(dtpDOJ.DateValue))
            '    If j < 0 Then
            '        SetMsg(LblMsg, "Due Date of Confirmation Can't Before the Date of Joining.")
            '        Return False
            '    End If
            'End If
            'If ChkDDEPP.Checked = True Then
            '    k = Date.Compare(DtpDDEPP.DateValue, CDate(dtpDOJ.DateValue))
            '    If k < 0 Then
            '        SetMsg(LblMsg, "Probation Period Extended Upto Can't Before the Date of Joining.")
            '        Return False
            '    End If
            'End If
            'If ChkCEUPTO.Checked = True Then
            '    k = Date.Compare(DtpCEUPTO.DateValue, CDate(dtpDOJ.DateValue))
            '    If k < 0 Then
            '        SetMsg(LblMsg, "Contract Extended Upto Can't Before the Date of Joining.")
            '        Return False
            '    End If
            'End If
            'If ChkDDR.Checked = True Then
            '    k = Date.Compare(DtpDDR.DateValue, CDate(dtpDOJ.DateValue))
            '    If k < 0 Then
            '        SetMsg(LblMsg, "Date of Regularisation Can't Before the Date of Joining.")
            '        Return False
            '    End If
            'End If
            'If Not IsDate(DtpDOC.Text) Then
            '    isValidate = False
            '    SetMsg(LblMsg, "Invalid Date of Confirmation.")
            '    Exit Function
            'End If
            isValidate = True
        Catch ex As Exception
            isValidate = False
        End Try
    End Function

#End Region

#Region "  Display Employee Record  "
    '    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
    '        Private Sub DisplayRecord()
    '        Dim dtEmp As New DataTable
    '        Try

    '            Dim StrSQl As String
    '            ViewState("Value") = "NULL"
    '            StrSQl = "Select * From HrdMastQry Where Emp_Code = '" & Chk(txtEM_CD.Text) & "'" & Session("UserCodes")
    '            Session("DalObj").GetSqlDataTable(dtEmp, StrSQl)
    '            If dtEmp.Rows.Count > 0 Then
    '                With dtEmp.Rows(0)
    '                    ViewState("Value") = "MODI"
    '                    txtFName.Text = Chk(.Item("FName"))
    '                    txtLName.Text = Chk(.Item("LName"))
    '                    TxtComp.Text = Chk(.Item("Company"))
    '                    TxtJobRanking.Text = Chk(.Item("JobRank"))
    '                    TxtSalAdminPlan.Text = Chk(.Item("LSA_PLAN"))
    '                    TxtBTitle.Text = Chk(.Item("Buss_Title"))
    '                    ChkCombo(cmbDesignation, .Item("Dsg_Code"))
    '                    ChkCombo(cmbDepartment, .Item("Dept_Code"))
    '                    ChkCombo(cmbLocation, .Item("Loc_Code"))
    '                    ChkCombo(cmbRegion, .Item("Regn_Code"))
    '                    ChkCombo(cmbEmpType, .Item("Type_Code"))
    '                    ChkCombo(cmbManager, .Item("Mngr_Code"))
    '                    ChkCombo(cmbGrade, .Item("Grd_Code"))
    '                    ChkCombo(cmbCostCenter, .Item("Cost_Code"))
    '                    ChkCombo(cmbProcess, .Item("Proc_Code"))
    '                    ChkCombo(cmbSection, .Item("Sect_Code"))
    '                    ChkCombo(cmbDivision, .Item("Divi_Code"))
    '                    ChkCombo(CmbCosting, .Item("CostType_Code"))
    '                    ChkCombo(CmbHrMngr, .Item("HR_MNGR"))
    '                    ChkCombo(CmbEmpClass, .Item("EMP_CLASS"))
    '                    ChkCombo(CmbFull, .Item("FULL_PART"))
    '                    ChkCombo(CmbUnit, .Item("Unit_Code"))
    '                    ChkCombo(CmbContType, .Item("Cont_Type"))
    '                    ChkCombo(CmbJobName, .Item("jOB_CODE"))


    '                    If IsDBNull(.Item("DOJ")) Then
    '                        dtpDOJ.DateValue = CDate("01/01/1900")
    '                    Else
    '                        dtpDOJ.DateValue = .Item("DOJ")
    '                    End If



    '                    'If IsDBNull(.Item("DOC")) Then
    '                    '    dtpDOC.DateValue = Date.Today
    '                    'Else
    '                    '    dtpDOC.DateValue = CDate(.Item("DOC"))
    '                    'End If

    '                    If IsDBNull(.Item("DOCE")) Then
    '                        ChkDOCE.Checked = False
    '                        dtpDOCE.Enabled = False
    '                        dtpDOCE.DateValue = Date.Today
    '                    Else
    '                        ChkDOCE.Checked = True
    '                        dtpDOCE.Enabled = True
    '                        dtpDOCE.DateValue = CDate(.Item("DOCE"))
    '                    End If

    '                    If IsDBNull(.Item("DOC")) Then
    '                        ChkDOC.Checked = False
    '                        DtpDOC.Enabled = False
    '                        DtpDOC.DateValue = Date.Today
    '                    Else
    '                        ChkDOC.Checked = True
    '                        DtpDOC.Enabled = True
    '                        DtpDOC.DateValue = CDate(.Item("DOC"))
    '                    End If


    '                    If IsDBNull(.Item("DOC_Due")) Then
    '                        ChkDOCDue.Checked = False
    '                        dtpDOCDUE.Enabled = False
    '                        dtpDOCDUE.DateValue = Date.Today
    '                    Else
    '                        ChkDOCDue.Checked = True
    '                        dtpDOCDUE.Enabled = True
    '                        dtpDOCDUE.DateValue = CDate(.Item("DOC_Due"))
    '                    End If

    '                    If IsDBNull(.Item("DOCExUPTO")) Then
    '                        ChkCEUPTO.Checked = False
    '                        DtpCEUPTO.Enabled = False
    '                        DtpCEUPTO.DateValue = Date.Today
    '                    Else
    '                        ChkCEUPTO.Checked = True
    '                        DtpCEUPTO.Enabled = True
    '                        DtpCEUPTO.DateValue = CDate(.Item("DOCExUPTO"))
    '                    End If

    '                    If IsDBNull(.Item("SERV_DATE")) Then
    '                        ChkDDEDPE.Checked = False
    '                        DtpDDEDPE.Enabled = False
    '                        DtpDDEDPE.DateValue = Date.Today
    '                    Else
    '                        ChkDDEDPE.Checked = True
    '                        DtpDDEDPE.Enabled = True
    '                        DtpDDEDPE.DateValue = CDate(.Item("SERV_DATE"))
    '                    End If

    '                    If IsDBNull(.Item("DDOEPP")) Then
    '                        ChkDDEPP.Checked = False
    '                        DtpDDEPP.Enabled = False
    '                        DtpDDEPP.DateValue = Date.Today
    '                    Else
    '                        ChkDDEPP.Checked = True
    '                        DtpDDEPP.Enabled = True
    '                        DtpDDEPP.DateValue = CDate(.Item("DDOEPP"))
    '                    End If

    '                    If IsDBNull(.Item("DDOR")) Then
    '                        ChkDDR.Checked = False
    '                        DtpDDR.Enabled = False
    '                        DtpDDR.DateValue = Date.Today
    '                    Else
    '                        ChkDDR.Checked = True
    '                        DtpDDR.Enabled = True
    '                        DtpDDR.DateValue = CDate(.Item("DDOR"))
    '                    End If

    '                    'If IsDBNull(.Item("DDOEDPE")) Then
    '                    '    ChkDDEDPE.Checked = False
    '                    '    DtpDDEDPE.Enabled = False
    '                    '    DtpDDEDPE.DateValue = Date.Today
    '                    'Else
    '                    '    ChkDDEDPE.Checked = True
    '                    '    DtpDDEDPE.Enabled = True
    '                    '    DtpDDEDPE.DateValue = CDate(.Item("DDOEDPE"))
    '                    'End If

    '                    'If IsDBNull(.Item("DDOETP")) Then
    '                    '    ChkDDETP.Checked = False
    '                    '    DtpDDETP.Enabled = False
    '                    '    DtpDDETP.DateValue = Date.Today
    '                    'Else
    '                    '    ChkDDETP.Checked = True
    '                    '    DtpDDETP.Enabled = True
    '                    '    DtpDDETP.DateValue = CDate(.Item("DDOETP"))
    '                    'End If

    '                End With
    '            Else
    '                Dim Code As Object
    '                Code = Session("DalObj").ExecuteCommand("Select Emp_Code From HrdMastQry Where Emp_Code = '" & Chk(txtEM_CD.Text) & "'" & Session("UserCodes"), , DAL.DataLayer.ExecutionType.ExecuteScalar)

    '                If Code <> "" Then
    '                    SetMsg(LblMsg, "This Employee Code Exist For Other Location.")
    '                Else
    '                    'If txtEM_CD.Text <> "" Then
    '                    '    SetMsg(LblMsg, "This Employee Code does not exist.")
    '                    'End If
    '                End If
    '                ViewState("Value") = "NULL"
    '                ClearAll(Me)
    '                txtEM_CD.Text = Session("EM_CD")
    '                dtpDOJ.DateValue = Format(Date.Today, "dd/MMM/yyyy")
    '            End If
    '        Catch ex As Exception
    '            SetMsg(LblMsg, ex.Message & " : (DisplayRec)")
    '        Finally
    '            dtEmp.Dispose()
    '        End Try
    '    End Sub

#End Region

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("EmpExplorer.aspx?SrNo=61")
    End Sub

    'Private Sub txtEM_CD_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEM_CD.TextChanged
    '    If Trim(txtEM_CD.Text) <> "" Then Session("EM_CD") = Trim(txtEM_CD.Text)
    '    If ViewState("Combo") = "Empty" Or ViewState("Combo") = Nothing Then
    '        If Trim(txtEM_CD.Text) <> "" Then

    '            ViewState("Combo") = "Filled"
    '        Else
    '            ViewState("Combo") = "Empty"
    '        End If
    '    End If

    '    DisplayRecord()
    '    RegisterStartupScript("txtEM_CD", "<SCRIPT language='javascript'>document.getElementById('txtFName').focus() </SCRIPT>")
    'End Sub

#End Region
    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub
    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
    Public Function Save_Record(ByVal ecode As String, ByVal fnam As String, ByVal lnme As String, ByVal comp As String, ByVal sap As String, ByVal jr As String, ByVal bt As String, ByVal deg As String, ByVal dep As String, ByVal loc As String, ByVal alo As String, ByVal plo As String, ByVal et As String, ByVal jn As String, ByVal ec As String, ByVal hm As String, ByVal man As String, ByVal gr As String, ByVal cc As String, ByVal pro As String, ByVal reg As String, ByVal sec As String, ByVal div As String, ByVal cos As String, ByVal unt As String, ByVal ct As String, ByVal full As String, ByVal doc As String, ByVal ddedpe As String, ByVal docdue As String, ByVal ddepp As String, ByVal doce As String, ByVal ceupto As String, ByVal ddr As String, ByVal doj As String) As String
        'NOT UPDATE alo,plo
        Try
            If ViewState("Value") = "NULL" Then Exit Function
            Dim strSQl As String

            'Validation checking
            If Trim(fnam) = "" Then
                Save_Record = "Please Enter First Name of Employee."
                Exit Function
            End If

            If IsNumeric(fnam) Then
                Save_Record = "First Name must be string type."
                Exit Function
            End If

            If IsNumeric(lnme) Then
                Save_Record = "Name must be string type."
                Exit Function
            End If

            If doj = "" Then
                Save_Record = "Please enter the Date Of Joining."

                Exit Function
            Else
                If doce <> "" Then
                    If DateDiff("d", doce, doj) > 0 Then
                        Save_Record = "Contract End Date Can't  be Before the Date of Joining."

                        Exit Function
                    End If
                End If
                '======================================
                If doc <> "" Then
                    If DateDiff("d", doc, doj) > 0 Then
                        Save_Record = "Confirmation Date can't be Before the Date of Joining."
                        Exit Function
                    End If
                End If


                If ddedpe <> "" Then
                    If DateDiff("d", ddedpe, doj) > 0 Then
                        Save_Record = "Service Date Can't be  Before the Date of Joining."
                        Exit Function
                    End If
                End If

                If ddepp <> "" Then
                    If DateDiff("d", ddepp, doj) > 0 Then
                        Save_Record = "Probation Period Extended Upto Can't be  Before the Date of Joining."
                        Exit Function
                    End If
                End If
                If docdue <> "" Then
                    If DateDiff("d", docdue, doj) > 0 Then
                        Save_Record = "Due Date of Confirmation Can't be Before the Date of Joining."
                        Exit Function
                    End If
                End If
                If ddepp <> "" Then
                    If DateDiff("d", ddepp, doj) > 0 Then
                        Save_Record = "Probation Period Extended Upto Can't be  Before the Date of Joining."
                        Exit Function
                    End If
                End If
                If doce <> "" Then
                    If DateDiff("d", doce, doj) > 0 Then
                        Save_Record = "Contract End Date Can't be  Before the Date of Joining."
                        Exit Function
                    End If
                End If

                If ceupto <> "" Then
                    If DateDiff("d", ceupto, doj) > 0 Then
                        Save_Record = "Contract Extended Upto Can't be  Before the Date of Joining."
                        Exit Function
                    End If
                    If doce = "" Then
                        Save_Record = "Contract Extended Upto Can't  be Before the Contract End Date."
                        Exit Function
                    End If
                End If
                If ddr <> "" Then
                    If DateDiff("d", ddr, doj) > 0 Then
                        Save_Record = "Date of Regularisation Can't  be Before the Date of Joining."
                        Exit Function
                    End If
                End If

                '------------------------
                'Date of Joining and Contract End Date 

                If doce <> "" Then
                    If DateDiff("d", doce, doj) = 0 Then
                        Save_Record = "Date of Joining and  Contract End Date Cannot be same."
                        Exit Function
                    End If
                End If
                'Contract End Date AND Contract Extended Upto

                If doce <> "" And ceupto <> "" Then
                    If DateDiff("d", ceupto, doce) > 0 Or DateDiff("d", ceupto, doce) = 0 Then
                        Save_Record = "Contract Extended Upto Can't  be Before or Equal to  Contract End Date."
                        Exit Function
                    End If
                End If

            End If

            '----------------------------------------------
            'If (docdue <> "" And doj <> "" And DateDiff("d", doce, doj) > 0) Then
            '    Save_Record = "Due Date of Confirmation Can't Before the Date of Joining."
            '    Exit Function
            'End If


            'Recors saving query
            strSQl = "Update HrdMast Set "
            strSQl &= "FName  = '" & Chk(fnam) & "',"
            strSQl &= " LName  = '" & Chk(lnme) & "',"
            strSQl &= " Dsg_Code  = '" & Chk(deg) & "',"
            strSQl &= " Dept_Code = '" & Chk(dep) & "',"
            strSQl &= " Loc_Code  = '" & Chk(loc) & "',"
            strSQl &= " ADMINLOC_CODE  = '" & Chk(alo) & "',"
            strSQl &= " PAYLOC_CODE  = '" & Chk(plo) & "',"
            strSQl &= " Regn_Code = '" & Chk(reg) & "',"
            strSQl &= " Type_Code = '" & Chk(et) & "',"
            strSQl &= " Mngr_Code = '" & Chk(man) & "',"
            strSQl &= " Grd_Code  = '" & Chk(gr) & "',"
            strSQl &= " Cost_Code = '" & Chk(cc) & "',"
            strSQl &= " Company = '" & Chk(comp) & "',"
            strSQl &= " JobRank = '" & Chk(jr) & "',"
            strSQl &= " LSA_PLAN = '" & Chk(sap) & "',"
            strSQl &= " Buss_Title = '" & Chk(bt) & "',"
            strSQl &= " Proc_Code = '" & Chk(pro) & "',"
            strSQl &= " HR_MNGR = '" & Chk(hm) & "',"
            strSQl &= " COSTTYPE_CODE = '" & Chk(cos) & "',"
            strSQl &= " EMP_CLASS = '" & Chk(ec) & "',"
            strSQl &= " jOB_CODE = '" & Chk(jn) & "',"
            strSQl &= " FULL_PART = '" & Chk(full) & "',"
            strSQl &= " Unit_Code = '" & Chk(unt) & "',"
            strSQl &= " Cont_Type = '" & Chk(ct) & "',"
            strSQl &= " Sect_Code = '" & Chk(sec) & "',"
            strSQl &= " Divi_Code = '" & Chk(div) & "',"
            strSQl &= " DOJ = " & IIf(Chk(doj) = "", "Null, ", "'" & doj & "',") & ""
            strSQl &= " DOC_Due = " & IIf(Chk(docdue) = "", "Null, ", "'" & docdue & "',") & ""
            strSQl &= " SERV_DATE = " & IIf(Chk(ddedpe) = "", "Null, ", "'" & ddedpe & "',") & ""
            strSQl &= " DOC = " & IIf(Chk(doc) = "", "Null, ", "'" & doc & "',") & ""
            strSQl &= " DOCExUPTO = " & IIf(Chk(ceupto) = "", "Null, ", "'" & ceupto & "',") & ""
            strSQl &= " DOCE = " & IIf(Chk(doce) = "", "Null, ", "'" & doce & "',") & ""
            strSQl &= " DDOEPP = " & IIf(Chk(ddepp) = "", "Null, ", "'" & ddepp & "',") & ""
            strSQl &= " DDOR = " & IIf(Chk(ddr) = "", "Null ", "'" & ddr & "'") & ""
            strSQl &= "Where Emp_Code = '" & ecode & "'"

            '   strSQl = "Update HrdMast Set "
            '  " FName  = '" & Chk(fnam) & "'," & _
            '     " LName  = '" & Chk(lnme) & "'," & _
            '   " Dsg_Code  = '" & Chk(deg) & "'," & _
            '   " Dept_Code = '" & Chk(dep) & "'," & _
            '     " Loc_Code  = '" & Chk(loc) & "'," & _
            '      " Regn_Code = '" & Chk(reg) & "'," & _
            ' " Type_Code = '" & Chk(et) & "'," & _
            '     " Mngr_Code = '" & Chk(man) & "'," & _
            '    " Grd_Code  = '" & Chk(gr) & "'," & _
            '   " Cost_Code = '" & Chk(cc) & "'," & _
            '    " Company = '" & Chk(comp) & "'," & _
            '    " JobRank = '" & Chk(jr) & "'," & _
            '" LSA_PLAN = '" & Chk(sap) & "'," & _
            '" Buss_Title = '" & Chk(bt) & "'," & _
            '" Proc_Code = '" & Chk(pro) & "'," & _
            ' " HR_MNGR = '" & Chk(hm) & "'," & _
            ' " COSTTYPE_CODE = '" & Chk(cos) & "'," & _
            '     " EMP_CLASS = '" & Chk(ec) & "'," & _
            '   " jOB_CODE = '" & Chk(jn) & "'," & _
            '   " FULL_PART = '" & Chk(full) & "'," & _
            '  " Unit_Code = '" & Chk(unt) & "'," & _
            '   " Cont_Type = '" & Chk(ct) & "'," & _
            '    " Sect_Code = '" & Chk(sec) & "'," & _
            '   " Divi_Code = '" & Chk(div) & "'," & _
            '         " DOJ =   " & IIf(ChkDOC.Checked, "'" & doJ & "', ", "Null, ") & _
            '   " DOC_Due = " & IIf(ChkDOCDUE.Checked, "'" & docdue & "', ", "Null,") & _
            '   " SERV_DATE = " & IIf(ChkDDEDPE.Checked, "'" & ddedpe & "', ", "Null, ") & _
            '   " DOC = " & IIf(ChkDOC.Checked, "'" & docdue & "', ", "Null, ") & _
            '   " DOCExUPTO = " & IIf(ChkCEUPTO.Checked, "'" & ceupto & "', ", "Null, ") & _
            '   " DOCE = " & IIf(ChkDOCE.Checked, "'" & doce & "', ", "Null,") & _
            '   " DDOEPP = " & IIf(ChkDDEPP.Checked, "'" & ddepp & "', ", "Null,") & _
            '   " DDOR = " & IIf(ChkDDR.Checked, "'" & ddr & "' ", "Null") & _
            '   " Where Emp_Code = '" & Chk(txtEM_CD.Text) & "'"

            Dim RowsAffected As Int16
            RowsAffected = Session("DalObj").ExecuteCommand(strSQl)
            If RowsAffected > 0 Then
                Save_Record = "Records Has Been Saved Successfully."

            Else
                Save_Record = "Records is not Saved Successfully."
            End If

            ' SetMsg(LblMsg, "Records Has Been Saved Successfully.")



        Catch ex As Exception
            ' SetMsg(LblMsg, ex.Message & " : (cmdSave_Click)")
            Dim STR As String = ex.Message
            Save_Record = ex.Message & "Save_Record()"
        End Try

    End Function



End Class
