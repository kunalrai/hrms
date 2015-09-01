Partial Class IncTransfer
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

        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If
        Try
            If Not IsPostBack Then
                'By Ravi 21 Nov
                Dim SrNo As Int16
                SrNo = Request.QueryString.Item("SrNo") + Request.QueryString.Item("MODULEID")
                Dim bSuccess As Boolean
                Select Case CheckRight(SrNo)
                    Case MdlHRMS.AccessType.FullAccess, MdlHRMS.AccessType.SuperUser
                        bSuccess = True
                    Case MdlHRMS.AccessType.ReadonlyAccess, MdlHRMS.AccessType.Restricted
                        bSuccess = False
                End Select
                cmdSave.Visible = bSuccess
                CmdDelete.Visible = bSuccess

                '------------------------------------
                FillCombo()
                dtpWEF.Text = Format(Date.Today, "dd/MMM/yyyy")
                GetEarningsData()

            End If

        Catch ex As Exception
            LblMsg.Text = ex.Message & "Page Load"
        End Try

    End Sub
    Private Sub FillCombo()
        Try

            Dim LItem As ListItem, i As Int16
            Dim DtDate As Date
            DtDate = FY_Start
            For i = 1 To 12
                LItem = New ListItem
                LItem.Text = MonthName(Month(DtDate)) & " " & Year(DtDate)
                LItem.Value = Month(DtDate)
                CmbMonth.Items.Add(LItem)
                DtDate = DateAdd(DateInterval.Month, 1, DtDate)
            Next
            CmbMonth.SelectedValue = Month(Date.Today)
            '===================================================================================

            Dim iTemp As Int16, sTemp2 As String

            Session("BalObj").FillCombo(cmbEmp, "Select Emp_Code,Emp_Name as EMP_NAME From HrdMastQry Where  LType=1 " & Session("UserCodes") & " Order by Emp_Name", True)

            '=============================Section Master====================================
            'If InStr(UCase(Session("UserCodes")), "SECT_CODE") > 0 Then
            '    iTemp = InStr(UCase(Session("UserCodes")), "SECT_CODE")
            '    sTemp2 = Mid(UCase(Trim(Session("UserCodes"))), iTemp - 1, Len(Trim(Session("UserCodes"))) - (iTemp - 2))
            '    If InStr(sTemp2, "AND") > 0 Then
            '        sTemp2 = Mid(UCase(sTemp2), 1, Len(sTemp2) - (InStr(sTemp2, "AND") + 2))
            '    End If
            '    Session("BalObj").FillCombo(cmbPnSection, "Select Sect_Code,Sect_Name From SectMast Where " & sTemp2 & " Order By Sect_Name ", True)
            '    Session("BalObj").FillCombo(cmbPdSection, "Select Sect_Code,Sect_Name From SectMast Where " & sTemp2 & " Order By Sect_Name ", True)
            'Else
            Session("BalObj").FillCombo(cmbPnSection, "Select Sect_Code,Sect_Name From SectMast Order By Sect_Name", True)
            Session("BalObj").FillCombo(cmbPdSection, "Select Sect_Code,Sect_Name From SectMast Order By Sect_Name", True)
            'End If

            '=============================Department Master====================================
            'If InStr(UCase(Session("UserCodes")), "DEPT_CODE") > 0 Then
            '    iTemp = InStr(UCase(Session("UserCodes")), "DEPT_CODE")
            '    sTemp2 = Mid(UCase(Trim(Session("UserCodes"))), iTemp - 1, Len(Trim(Session("UserCodes"))) - (iTemp - 2))
            '    If InStr(sTemp2, "AND") > 0 Then
            '        sTemp2 = Mid(UCase(sTemp2), 1, Len(sTemp2) - (InStr(sTemp2, "AND") + 2))
            '    End If
            '    Session("BalObj").FillCombo(cmbPnDepartment, "Select Dept_Code, Dept_Name From DeptMast Where " & sTemp2 & " Order By Dept_Name ", True)
            '    Session("BalObj").FillCombo(CmbPdDepartment, "Select Dept_Code, Dept_Name From DeptMast Where " & sTemp2 & " Order By Dept_Name ", True)
            'Else
            Session("BalObj").FillCombo(CmbPdDepartment, "Select Dept_Code, Dept_Name From DeptMast Order By Dept_Name", True)
            Session("BalObj").FillCombo(cmbPnDepartment, "Select Dept_Code, Dept_Name From DeptMast Order By Dept_Name", True)
            'End If

            '=============================Location Master====================================
            'If InStr(UCase(Session("UserCodes")), "LOC_CODE") > 0 Then
            '    iTemp = InStr(UCase(Session("UserCodes")), "LOC_CODE")
            '    sTemp2 = Mid(UCase(Trim(Session("UserCodes"))), iTemp - 1, Len(Trim(Session("UserCodes"))) - (iTemp - 2))
            '    If InStr(sTemp2, "AND") > 0 Then
            '        sTemp2 = Mid(UCase(sTemp2), 1, Len(sTemp2) - (InStr(sTemp2, "AND") + 2))
            '    End If
            '    Session("BalObj").FillCombo(cmbPnLocation, "Select Loc_Code, Loc_Name From LocMast Where " & sTemp2 & " Order By Loc_Name ", True)
            '    Session("BalObj").FillCombo(CmbPdLocation, "Select Loc_Code, Loc_Name From LocMast Where " & sTemp2 & " Order By Loc_Name ", True)
            'Else
            Session("BalObj").FillCombo(cmbPnLocation, "Select Loc_Code, Loc_Name From LocMast Order by Loc_Name", True)
            Session("BalObj").FillCombo(CmbPdLocation, "Select Loc_Code, Loc_Name From LocMast Order by Loc_Name", True)
            'End If

            Session("BalObj").FillCombo(cmbPnALocation, "Select Loc_Code, Loc_Name From LocMast Order by Loc_Name", True)
            Session("BalObj").FillCombo(CmbPdALocation, "Select Loc_Code, Loc_Name From LocMast Order by Loc_Name", True)

            Session("BalObj").FillCombo(cmbPnPLocation, "Select Loc_Code, Loc_Name From LocMast Order by Loc_Name", True)
            Session("BalObj").FillCombo(CmbPdPLocation, "Select Loc_Code, Loc_Name From LocMast Order by Loc_Name", True)

            Session("BalObj").FillCombo(cmbPnManager, "Select Emp_Code,Emp_Name + ' (' + isnull(DSG_NAME,'') + ')' as EMP_NAME From HrdMastQry Where Emp_Code Not IN ('" & Chk(TxtCode.Text) & "') " & Session("UserCodes") & " And LType=1 Order by Emp_Name", True)
            Session("BalObj").FillCombo(CmbPdManager, "Select Emp_Code,Emp_Name + ' (' + isnull(DSG_NAME,'') + ')' as EMP_NAME From HrdMastQry Where Emp_Code Not IN ('" & Chk(TxtCode.Text) & "') " & Session("UserCodes") & " And LType=1 Order by Emp_Name", True)

            '=============================Region Master====================================
            'If InStr(UCase(Session("UserCodes")), "REGN_CODE") > 0 Then
            '    iTemp = InStr(UCase(Session("UserCodes")), "REGN_CODE")
            '    sTemp2 = Mid(UCase(Trim(Session("UserCodes"))), iTemp - 1, Len(Trim(Session("UserCodes"))) - (iTemp - 2))
            '    If InStr(sTemp2, "AND") > 0 Then
            '        sTemp2 = Mid(UCase(sTemp2), 1, Len(sTemp2) - (InStr(sTemp2, "AND") + 2))
            '    End If
            '    Session("BalObj").FillCombo(cmbPnRegion, " Select Regn_Code, Regn_Name From RegnMast Where " & sTemp2 & " Order By Regn_Name ", True)
            '    Session("BalObj").FillCombo(CmbPdRegion, " Select Regn_Code, Regn_Name From RegnMast Where " & sTemp2 & " Order By Regn_Name ", True)
            'Else
            Session("BalObj").FillCombo(cmbPnRegion, "Select Regn_Code, Regn_Name From RegnMast Order By Regn_Name", True)
            Session("BalObj").FillCombo(CmbPdRegion, "Select Regn_Code, Regn_Name From RegnMast Order By Regn_Name", True)
            'End If

            '=============================Designation Master====================================
            'If InStr(UCase(Session("UserCodes")), "DSG_CODE") > 0 Then
            '    iTemp = InStr(UCase(Session("UserCodes")), "DSG_CODE")
            '    sTemp2 = Mid(UCase(Trim(Session("UserCodes"))), iTemp - 1, Len(Trim(Session("UserCodes"))) - (iTemp - 2))
            '    If InStr(sTemp2, "AND") > 0 Then
            '        sTemp2 = Mid(UCase(sTemp2), 1, Len(sTemp2) - (InStr(sTemp2, "AND") + 2))
            '    End If
            '    CType(Session("BalObj"), BAL.BLayer).FillCombo(cmbPnDesignation, " Select DSG_CODE, Dsg_Name From DsgMast Where " & sTemp2 & " Order By Dsg_Name ", True)
            '    CType(Session("BalObj"), BAL.BLayer).FillCombo(CmbPdDesignation, " Select DSG_CODE, Dsg_Name From DsgMast Where " & sTemp2 & " Order By Dsg_Name ", True)
            'Else
            CType(Session("BalObj"), BAL.BLayer).FillCombo(cmbPnDesignation, " Select DSG_CODE, Dsg_Name From DsgMast Order By Dsg_Name", True)
            CType(Session("BalObj"), BAL.BLayer).FillCombo(CmbPdDesignation, " Select DSG_CODE, Dsg_Name From DsgMast Order By Dsg_Name", True)
            'End If


            '=============================Employee Type Master====================================
            'If InStr(UCase(Session("UserCodes")), "TYPE_CODE") > 0 Then
            '    iTemp = InStr(UCase(Session("UserCodes")), "TYPE_CODE")
            '    sTemp2 = Mid(UCase(Trim(Session("UserCodes"))), iTemp - 1, Len(Trim(Session("UserCodes"))) - (iTemp - 2))
            '    If InStr(sTemp2, "AND") > 0 Then
            '        sTemp2 = Mid(UCase(sTemp2), 1, Len(sTemp2) - (InStr(sTemp2, "AND") + 2))
            '    End If
            '    Session("BalObj").FillCombo(cmbPnEmpType, " Select Type_Code, Type_Name From EmpType Where " & sTemp2 & " Order By Type_Name ", True)
            '    Session("BalObj").FillCombo(CmbPdEmpType, " Select Type_Code, Type_Name From EmpType Where " & sTemp2 & " Order By Type_Name ", True)
            'Else
            Session("BalObj").FillCombo(cmbPnEmpType, "Select Type_Code, Type_Name From EmpType Order By Type_Name", True)
            Session("BalObj").FillCombo(CmbPdEmpType, "Select Type_Code, Type_Name From EmpType Order By Type_Name", True)
            'End If


            '=============================Cost Center Master====================================
            'If InStr(UCase(Session("UserCodes")), "COST_CODE") > 0 Then
            '    iTemp = InStr(UCase(Session("UserCodes")), "COST_CODE")
            '    sTemp2 = Mid(UCase(Trim(Session("UserCodes"))), iTemp - 1, Len(Trim(Session("UserCodes"))) - (iTemp - 2))
            '    If InStr(sTemp2, "AND") > 0 Then
            '        sTemp2 = Mid(UCase(sTemp2), 1, Len(sTemp2) - (InStr(sTemp2, "AND") + 2))
            '    End If
            '    Session("BalObj").FillCombo(cmbPnCostCenter, " Select Cost_Code, Cost_Name From CostMast Where " & sTemp2 & " Order By Cost_Name ", True)
            '    Session("BalObj").FillCombo(CmbPdCostCenter, " Select Cost_Code, Cost_Name From CostMast Where " & sTemp2 & " Order By Cost_Name ", True)
            'Else
            Session("BalObj").FillCombo(cmbPnCostCenter, " Select Cost_Code, Cost_Name From CostMast Order By Cost_Name", True)
            Session("BalObj").FillCombo(CmbPdCostCenter, " Select Cost_Code, Cost_Name From CostMast Order By Cost_Name", True)
            'End If

            '=============================PROC Master====================================
            'If InStr(UCase(Session("UserCodes")), "PROC_CODE") > 0 Then
            '    iTemp = InStr(UCase(Session("UserCodes")), "PROC_CODE")
            '    sTemp2 = Mid(UCase(Trim(Session("UserCodes"))), iTemp - 1, Len(Trim(Session("UserCodes"))) - (iTemp - 2))
            '    If InStr(sTemp2, "AND") > 0 Then
            '        sTemp2 = Mid(UCase(sTemp2), 1, Len(sTemp2) - (InStr(sTemp2, "AND") + 2))
            '    End If
            '    Session("BalObj").FillCombo(cmbPnProcess, "Select Proc_Code, Proc_Name From ProcMast Where " & sTemp2 & " Order By Proc_Name ", True)
            '    Session("BalObj").FillCombo(CmbPdProcess, "Select Proc_Code, Proc_Name From ProcMast Where " & sTemp2 & " Order By Proc_Name ", True)
            'Else
            Session("BalObj").FillCombo(cmbPnProcess, "Select Proc_Code, Proc_Name From ProcMast Order By Proc_Name", True)
            Session("BalObj").FillCombo(CmbPdProcess, "Select Proc_Code, Proc_Name From ProcMast Order By Proc_Name", True)
            'End If


            '=============================Division Master====================================
            'If InStr(UCase(Session("UserCodes")), "DIVI_CODE") > 0 Then
            '    iTemp = InStr(UCase(Session("UserCodes")), "DIVI_CODE")
            '    sTemp2 = Mid(UCase(Trim(Session("UserCodes"))), iTemp - 1, Len(Trim(Session("UserCodes"))) - (iTemp - 2))
            '    If InStr(sTemp2, "AND") > 0 Then
            '        sTemp2 = Mid(UCase(sTemp2), 1, Len(sTemp2) - (InStr(sTemp2, "AND") + 2))
            '    End If
            '    Session("BalObj").FillCombo(cmbPnDivision, " Select Divi_Code, Divi_Name From DiviMast Where " & sTemp2 & " Order By Divi_Name ", True)
            '    Session("BalObj").FillCombo(CmbPdDivision, " Select Divi_Code, Divi_Name From DiviMast Where " & sTemp2 & " Order By Divi_Name ", True)
            'Else
            Session("BalObj").FillCombo(cmbPnDivision, "Select Divi_Code, Divi_Name From DiviMast Order By Divi_Name", True)
            Session("BalObj").FillCombo(CmbPdDivision, "Select Divi_Code, Divi_Name From DiviMast Order By Divi_Name", True)
            'End If

            '=============================Grade Master====================================
            'If InStr(UCase(Session("UserCodes")), "GRD_CODE") > 0 Then
            '    iTemp = InStr(UCase(Session("UserCodes")), "GRD_CODE")
            '    sTemp2 = Mid(UCase(Trim(Session("UserCodes"))), iTemp - 1, Len(Trim(Session("UserCodes"))) - (iTemp - 2))
            '    If InStr(sTemp2, "AND") > 0 Then
            '        sTemp2 = Mid(UCase(sTemp2), 1, Len(sTemp2) - (InStr(sTemp2, "AND") + 2))
            '    End If
            '    Session("BalObj").FillCombo(cmbPnGrade, " Select Grd_Code, Grd_Name From GrdMast Where " & sTemp2 & " Order By Grd_Name ", True)
            '    Session("BalObj").FillCombo(CmbPdGrade, " Select Grd_Code, Grd_Name From GrdMast Where " & sTemp2 & " Order By Grd_Name ", True)
            'Else
            Session("BalObj").FillCombo(cmbPnGrade, "Select Grd_Code, Grd_Name From GrdMast Order By Grd_Name", True)
            Session("BalObj").FillCombo(CmbPdGrade, "Select Grd_Code, Grd_Name From GrdMast Order By Grd_Name", True)
            'End If

        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (FillCombo)")
        End Try
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            Dim Cnt As Single
            Dim strSQL As String
            Dim PayDate As Date
            PayDate = EOM(CDate("01-" & Left(CmbMonth.SelectedItem.Text, 3) & "-" & Right(CmbMonth.SelectedItem.Text, 4)))

            Cnt = Session("DalObj").ExecuteCommand("Select isNull(Count(Emp_Code),0) From Transfer Where Emp_Code = '" & Chk(TxtCode.Text) & "'", , DalObj.ExecutionType.ExecuteScalar)
            If Cnt = 0 Then
                strSQL = " Insert Into TRANSFER (Emp_Code,TransferWEF,paydate) Values ('" & Chk(TxtCode.Text) & "','" & Format(CDate(dtpWEF.Text), "dd/MMM/yyyy") & "','" & PayDate & "')"
                Session("DalObj").ExecuteCommand(strSQL)
            Else
                strSQL = " Update TRANSFER set Paydate= '" & PayDate & "', TransferWEF= '" & Format(CDate(dtpWEF.Text), "dd/MMM/yyyy") & "' where emp_code= '" & Chk(TxtCode.Text) & "'"
                Session("DalObj").ExecuteCommand(strSQL)
            End If

            strSQL = "Update Transfer Set " & _
                      " JobProfile  = '" & Chk(TxtPdJobProfile.Text) & "'," & _
                      " JobProfile_Prv  = '" & Chk(TxtPnJobProfile.Text) & "'," & _
                      " TransferReason  = '" & Chk(CmbReason.SelectedValue) & "'," & _
                      " Remarks  = '" & Chk(TxtReason.Text) & "'," & _
                      " TransferStatus  = '" & Chk(CmbTransferType.SelectedValue) & "'," & _
                      " Transfer_Date = '" & Format(CDate(dtpWEF.Text), "dd/MMM/yyyy") & "'," & _
                      " Dsg_Code  = '" & Chk(CmbPdDesignation.SelectedValue) & "'," & _
                      " Dsg_Prv  = '" & Chk(cmbPnDesignation.SelectedValue) & "'," & _
                      " Dept_Code = '" & Chk(CmbPdDepartment.SelectedValue) & "'," & _
                      " Dept_Prv = '" & Chk(cmbPnDepartment.SelectedValue) & "'," & _
                      " Loc_Code  = '" & Chk(CmbPdLocation.SelectedValue) & "'," & _
                      " Loc_Prv  = '" & Chk(cmbPnLocation.SelectedValue) & "'," & _
                      " Regn_Code = '" & Chk(CmbPdRegion.SelectedValue) & "'," & _
                      " Regn_Prv = '" & Chk(cmbPnRegion.SelectedValue) & "'," & _
                      " Type_Code = '" & Chk(CmbPdEmpType.SelectedValue) & "'," & _
                      " Type_Prv = '" & Chk(cmbPnEmpType.SelectedValue) & "'," & _
                      " Mngr_Code = '" & Chk(CmbPdManager.SelectedValue) & "'," & _
                      " Mngr_Prv = '" & Chk(cmbPnManager.SelectedValue) & "'," & _
                      " Grd_Code  = '" & Chk(CmbPdGrade.SelectedValue) & "'," & _
                      " Grd_Prv  = '" & Chk(cmbPnGrade.SelectedValue) & "'," & _
                      " Cost_Code = '" & Chk(CmbPdCostCenter.SelectedValue) & "'," & _
                      " Cost_Prv = '" & Chk(cmbPnCostCenter.SelectedValue) & "'," & _
                      " Proc_Code = '" & Chk(CmbPdProcess.SelectedValue) & "'," & _
                      " Proc_Prv = '" & Chk(cmbPnProcess.SelectedValue) & "'," & _
                      " Sect_Code = '" & Chk(cmbPdSection.SelectedValue) & "'," & _
                      " Sect_Prv = '" & Chk(cmbPnSection.SelectedValue) & "'," & _
                      " Divi_Code = '" & Chk(CmbPdDivision.SelectedValue) & "'," & _
                      " Divi_Prv = '" & Chk(cmbPnDivision.SelectedValue) & "' " & _
                      " Where Emp_Code = '" & Chk(TxtCode.Text) & "'"

            '" TransferFrom  = '" & Chk(cmbPnLocation.SelectedValue) & "'," & _
            '" TransferTo  = '" & Chk(CmbPdLocation.SelectedValue) & "'," & _
            Session("DalObj").ExecuteCommand(strSQL)


            '======================== For Compensation==================================
            Dim StrSql1 As String
            Dim StrInsert, StrUpdate As String
            Dim dtArrear As New DataTable
            Dim i As Int16
            StrSql1 = "SELECT * FROM ARREAR  WHERE FLD_WEF = '" & Format(CDate(dtpWEF.Text), "dd/MMM/yyyy") & "' AND PayDate = '" & PayDate & "' AND Emp_Code = '" & Chk(TxtCode.Text) & "'"
            Session("DalObj").GetSqlDataTable(dtArrear, StrSql1)

            If dtArrear.Rows.Count > 0 Then
                StrSql1 = "Delete FROM ARREAR  WHERE FLD_WEF = '" & Format(CDate(dtpWEF.Text), "dd/MMM/yyyy") & "' AND PayDate = '" & PayDate & "' AND Emp_Code = '" & Chk(TxtCode.Text) & "'"
                Session("DalObj").ExecuteCommand(StrSql1)
            End If

            StrInsert = ""
            StrUpdate = ""
            For i = 0 To GrdEarnings.Items.Count - 1
                StrInsert = StrInsert & " " & "Insert Into arrear(EMP_CODE, PAYDATE, FLD_WEF, FIELD_NAME, PRV_AMT, CUR_AMT)  Values('" & Chk(TxtCode.Text) & "' , '" & PayDate & "', '" & Format(CDate(dtpWEF.Text), "dd/MMM/yyyy") & "','" & Chk(CType(GrdEarnings.Items(i).Cells(2).Controls(1), TextBox).Text) & "'," & ChkN(CType(GrdEarnings.Items(i).Cells(1).Controls(1), TextBox).Text) & "," & ChkN(CType(GrdEarnings.Items(i).Cells(4).Controls(1), TextBox).Text) & ")"
                StrUpdate = StrUpdate & " " & "UPDATE PAYMAST SET " & Chk(CType(GrdEarnings.Items(i).Cells(2).Controls(1), TextBox).Text) & "=" & ChkN(CType(GrdEarnings.Items(i).Cells(4).Controls(1), TextBox).Text) & " WHERE EMP_CODE =" & "'" & Chk(TxtCode.Text) & "'"

            Next

            Session("DalObj").ExecuteCommand(StrInsert)
            Session("DalObj").ExecuteCommand(StrUpdate)
            SetMsg(LblMsg, "Records Has Been Saved Successfully.")


        Catch ex As Exception
            SetMsg(LblMsg, ex.Message)
        End Try
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Private Sub btnList_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnList.Click
        TxtCode.Visible = False
        btnList.Visible = False
        cmbEmp.Visible = True
    End Sub

    Sub DisplayRecords()
        Try
            Dim StrSQl As String, dtEmp As New DataTable
            StrSQl = "Select * From HrdMastQry Where Emp_Code = '" & Chk(TxtCode.Text) & "'" & Session("UserCodes")
            Session("DalObj").GetSqlDataTable(dtEmp, StrSQl)
            If dtEmp.Rows.Count > 0 Then
                With dtEmp.Rows(0)
                    LblName.Text = Chk(.Item("EMP_NAME"))

                    ChkCombo(cmbPnDesignation, .Item("Dsg_Code"))
                    ChkCombo(CmbPdDesignation, .Item("Dsg_Code"))

                    ChkCombo(cmbPnDepartment, .Item("Dept_Code"))
                    ChkCombo(CmbPdDepartment, .Item("Dept_Code"))

                    ChkCombo(cmbPnLocation, .Item("Loc_Code"))
                    ChkCombo(CmbPdLocation, .Item("Loc_Code"))

                    ChkCombo(cmbPnRegion, .Item("Regn_Code"))
                    ChkCombo(CmbPdRegion, .Item("Regn_Code"))

                    ChkCombo(cmbPnEmpType, .Item("Type_Code"))
                    ChkCombo(CmbPdEmpType, .Item("Type_Code"))

                    ChkCombo(cmbPnManager, .Item("Mngr_Code"))
                    ChkCombo(CmbPdManager, .Item("Mngr_Code"))

                    ChkCombo(cmbPnGrade, .Item("Grd_Code"))
                    ChkCombo(CmbPdGrade, .Item("Grd_Code"))

                    ChkCombo(cmbPnCostCenter, .Item("Cost_Code"))
                    ChkCombo(CmbPdCostCenter, .Item("Cost_Code"))

                    ChkCombo(cmbPnProcess, .Item("Proc_Code"))
                    ChkCombo(CmbPdProcess, .Item("Proc_Code"))

                    ChkCombo(cmbPnSection, .Item("Sect_Code"))
                    ChkCombo(cmbPdSection, .Item("Sect_Code"))

                    ChkCombo(cmbPnDivision, .Item("Divi_Code"))
                    ChkCombo(CmbPdDivision, .Item("Divi_Code"))

                    TxtPnJobProfile.Text = Chk(.Item("JobProfile"))
                    TxtPdJobProfile.Text = Chk(.Item("JobProfile"))
                End With
            Else
                ClearAll(Me)
                dtpWEF.Text = Format(Date.Today, "dd/MMM/yyyy")
                LblName.Text = ""
            End If
            dtEmp = Nothing


            '========================Compensation=================================
            Dim dtPayMast As New DataTable, i As Int16

            StrSQl = "SELECT * FROM PayMast Inner Join HrdMast On PayMast.Emp_Code = HrdMast.Emp_Code WHERE PayMast.FinYear = " & Year(FY_Start) & " AND PayMast.Emp_Code = '" & Chk(TxtCode.Text) & "'" & Session("UserCodes")
            Session("DalObj").GetSqlDataTable(dtPayMast, StrSQl)

            '*************  Earning  Data  Display  *************
            If dtPayMast.Rows.Count > 0 Then
                For i = 0 To ViewState("dtEarnings").Rows.Count - 1
                    ViewState("dtEarnings").Rows(i).Item("Amount") = ChkN(dtPayMast.Rows(0).Item(ViewState("dtEarnings").Rows(i).Item("FIELD_NAME")))
                Next
            Else
                For i = 0 To ViewState("dtEarnings").Rows.Count - 1
                    ViewState("dtEarnings").Rows(i).Item("Amount") = 0
                Next
            End If
            GrdEarnings.DataSource = ViewState("dtEarnings")
            GrdEarnings.DataBind()

        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " Display Records")
        End Try
    End Sub

    Private Sub cmbEmp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbEmp.SelectedIndexChanged
        If cmbEmp.SelectedValue <> "" Then
            TxtCode.Text = cmbEmp.SelectedValue
        Else
            TxtCode.Text = ""
        End If
        TxtCode_TextChanged(sender, e)
        TxtCode.Visible = True
        btnList.Visible = True
        cmbEmp.Visible = False
    End Sub

    Private Sub TxtCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtCode.TextChanged
        DisplayRecords()
    End Sub

    Private Sub GetEarningsData()
        Try
            ViewState("dtEarnings") = New DataTable
            Dim StrSql As String
            StrSql = " SELECT SNO,FIELD_NAME,FIELD_DESC,0 As Amount,FIELD_TYPE,FIELD_LEN FROM PaySetup Where FIELD_TYPE = 'N' And (FLD_PAYMAST = 'Y' or FLD_HRDMAST = 'Y') And FLD_Categ = 1 ORDER BY SNO"
            Session("DalObj").GetSqlDataTable(ViewState("dtEarnings"), StrSql)
            GrdEarnings.DataSource = ViewState("dtEarnings")
            GrdEarnings.DataBind()
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (GetEarningsData)")
        End Try
    End Sub
    'Private Sub GrdEarnings_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles GrdEarnings.ItemDataBound
    '    Try
    '        If e.Item.ItemIndex >= 0 Then
    '            If Session("LoginUser").UserGroup = "ADMIN" Then
    '                CType(e.Item.Cells(1).Controls(1), TextBox).Visible = True
    '            Else
    '                Dim tmpLbl As Label
    '                tmpLbl = e.Item.FindControl("LblAmount_Ern")
    '                If Not tmpLbl Is Nothing Then
    '                    tmpLbl.Visible = True
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        SetMsg(LblMsg, ex.Message & " : (GrdEarnings_ItemDataBound)")
    '    End Try
    'End Sub

End Class
