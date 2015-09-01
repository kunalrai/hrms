Partial Class Joining
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
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If
        If Not IsPostBack Then
            FillCombo()
        End If
    End Sub


    Private Sub FillCombo()
        Try
            Session("BalObj").FillCombo(cmbResume, "select Res_Code, (Res_NameF +' '+ Res_nameM+' '+Res_NameL+ ' ('+Res_No+')')as ResName from resmast where status_code=111 order by ResName", True)
            Session("BalObj").FillCombo(cmbSection, "Select Sect_Code,Sect_Name From SectMast Order By Sect_Name", True)
            Session("BalObj").FillCombo(cmbDept, "Select Dept_Code, Dept_Name From DeptMast Order By Dept_Name", True)
            Session("BalObj").FillCombo(cmbLoc, "Select Loc_Code, Loc_Name From LocMast Order by Loc_Name", True)
            Session("BalObj").FillCombo(CmbRegion, "Select Regn_Code, Regn_Name From RegnMast Order By Regn_Name", True)
            Session("BalObj").FillCombo(cmbDesg, " Select DSG_CODE, Dsg_Name From DsgMast Order By Dsg_Name", True)
            Session("BalObj").FillCombo(cmbEmpType, "Select Type_Code, Type_Name From EmpType Order By Type_Name", True)
            Session("BalObj").FillCombo(cmbCostCenter, " Select Cost_Code, Cost_Name From CostMast Order By Cost_Name", True)
            Session("BalObj").FillCombo(cmbProcess, "Select Proc_Code, Proc_Name From ProcMast Order By Proc_Name", True)
            Session("BalObj").FillCombo(cmbDivision, "Select Divi_Code, Divi_Name From DiviMast Order By Divi_Name", True)
            Session("BalObj").FillCombo(cmbGrd, "Select Grd_Code, Grd_Name From GrdMast Order By Grd_Name", True)
            Session("BalObj").fillcombo(cmbReptMngr, "Select Emp_Code, Emp_Name as FNAME from HrdMastQry Where 1=1 " & Session("UserCodes") & " Order by FNAME", True)
        Catch ex As Exception
        End Try
    End Sub


    Private Sub cmbResume_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbResume.SelectedIndexChanged
        Try
            Dim DEPTCODE, desgCode As String
            If cmbResume.SelectedValue = "" Then
                Blank()
            Else
                DEPTCODE = Session("DalObj").ExecuteCommand("Select Dept_Code From ResMast Where Res_Code = '" & cmbResume.SelectedValue & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
                ChkCombo(cmbDept, DEPTCODE)
                desgCode = Session("DalObj").ExecuteCommand("Select DSG_CODE From ResMast Where Res_Code = '" & cmbResume.SelectedValue & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
                ChkCombo(cmbDesg, desgCode)
                cmbCostCenter.SelectedValue = ""
                cmbEmpType.SelectedValue = ""
                cmbGrd.SelectedValue = ""
                cmbLoc.SelectedValue = ""
                cmbProcess.SelectedValue = ""
                CmbRegion.SelectedValue = ""
                cmbReptMngr.SelectedValue = ""
                cmbSection.SelectedValue = ""
                cmbDivision.SelectedValue = ""
                TxtEmpCode.Text = ""
                TxtPFNo.Text = ""
                ChkDelete.Checked = False
                ChkADOJ.Checked = False
            End If
        Catch ex As Exception
        End Try
    End Sub


    Sub Blank()
        cmbCostCenter.SelectedValue = ""
        cmbDept.SelectedValue = ""
        cmbDesg.SelectedValue = ""
        cmbEmpType.SelectedValue = ""
        cmbGrd.SelectedValue = ""
        cmbLoc.SelectedValue = ""
        cmbProcess.SelectedValue = ""
        CmbRegion.SelectedValue = ""
        cmbReptMngr.SelectedValue = ""
        cmbSection.SelectedValue = ""
        cmbDivision.SelectedValue = ""
        TxtEmpCode.Text = ""
        TxtPFNo.Text = ""
        ChkDelete.Checked = False
        ChkADOJ.Checked = False
    End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim SqlStr As String, Count As Int16

        Dim Tran As SqlClient.SqlTransaction
        Try
            Dim dt As New DataTable, dt1 As New DataTable, dt2 As New DataTable, i As Int16

            SqlStr = " Select RO.Res_fathHusbName, RM.Res_NameL,RO.Res_MAddr1, RO.Res_MAddr2,RO.Res_MAddr3,RO.Res_MCity,RO.Res_MCountry,RM.Res_NameM, RM.Res_NameF,RO.Res_MobileNO, " & _
                     " RO.Res_MPhone,RO.Res_MPin,RO.Res_MState,RO.Res_MStatus,RO.Res_PAddr1,RO.Res_PAddr2,RO.Res_PAddr3,RO.Res_PCity," & _
                     " RO.Res_PCountry,RO.Res_PPin,RO.Res_PState,RO.res_Sex,RO.Res_PhoneNo,RO.Res_EMailId, RO.Res_Nationality " & _
                     " from resmast as RM inner join ResOthers As RO  on RM.Res_Code=RO.RES_CODE " & _
                     " where RM.Res_Code='" & Chk(cmbResume.SelectedValue) & "'"

            Session("DalObj").GetSqlDataTable(dt, SqlStr)

            SqlStr = "Select * from ResQual where Res_Code='" & Chk(cmbResume.SelectedValue) & "'"
            Session("DalObj").GetSqlDataTable(dt1, SqlStr)

            SqlStr = "Select * from ResExp where Res_Code='" & Chk(cmbResume.SelectedValue) & "'"
            Session("DalObj").GetSqlDataTable(dt2, SqlStr)

            Tran = Session("DalObj").StartTransaction("Save")

            If ChkDelete.Checked = True Then
                SqlStr = "Delete from HrdMast where emp_code= '" & Chk(TxtEmpCode.Text) & "' "
                Session("DalObj").ExecuteCommand(SqlStr, Tran)
                SqlStr = "Delete from HrdQual where emp_code= '" & Chk(TxtEmpCode.Text) & "' "
                Session("DalObj").ExecuteCommand(SqlStr, Tran)
                SqlStr = "Delete from HrdExp where emp_code= '" & Chk(TxtEmpCode.Text) & "' "
                Session("DalObj").ExecuteCommand(SqlStr, Tran)
            End If
            If Not IsValidate() Then Exit Sub
            SqlStr = "Insert HrdMast(Emp_Code,Comp_Code,Dsg_Code, Dept_Code,Loc_Code,Regn_Code,Type_Code,Mngr_Code,Grd_Code ,Cost_Code,Proc_Code,Sect_Code,Divi_Code, PFNO,DOJ,DOC,DOCE,FATHHUSBNAME,LNAME,MADDR1,MADDR2,MADDR3,MCITY,MCOUNTRY,MNAME,FNAME,MOBILENO,MPHONE,MPIN,MSTATE,MSTATUS,PADDR1,PADDR2,PADDR3,PCITY,PCOUNTRY,PPIN,PSTATE,SEX,EMAILID,PPHONE,NATIONALITY) values ('" & _
                Chk(TxtEmpCode.Text) & "', '" & _
                (Session("LoginUser").CurrentCompID) & "', '" & _
                (cmbDesg.SelectedValue) & "', '" & _
                (cmbDept.SelectedValue) & "', '" & _
                (cmbLoc.SelectedValue) & "', '" & _
                (CmbRegion.SelectedValue) & "', '" & _
                (cmbEmpType.SelectedValue) & "', '" & _
                (cmbReptMngr.SelectedValue) & "', '" & _
                (cmbGrd.SelectedValue) & "', '" & _
                (cmbCostCenter.SelectedValue) & "', '" & _
                (cmbProcess.SelectedValue) & "', '" & _
                (cmbSection.SelectedValue) & "', '" & _
                (cmbDivision.SelectedValue) & "', '" & _
                Chk(TxtPFNo.Text) & "', "

            SqlStr = SqlStr & IIf(ChkADOJ.Checked, "'" & dtpADOJ.DateValue & "', ", "Null,") & _
                IIf(ChkDOC.Checked, "'" & dtpDOC.DateValue & "', ", "Null, ") & _
                IIf(ChkDOCE.Checked, "'" & dtpDOCE.DateValue & "', ", "Null, ") & " '"

            SqlStr = SqlStr & Chk(dt.Rows(0).Item("Res_fathHusbName")) & "', '" & _
                Chk(dt.Rows(0).Item("Res_NameL")) & "', '" & _
                Chk(dt.Rows(0).Item("Res_MAddr1")) & "', '" & _
                Chk(dt.Rows(0).Item("Res_MAddr2")) & "', '" & _
                Chk(dt.Rows(0).Item("Res_MAddr3")) & "', '" & _
                Chk(dt.Rows(0).Item("Res_MCity")) & "', '" & _
                Chk(dt.Rows(0).Item("Res_MCountry")) & "', '" & _
                Chk(dt.Rows(0).Item("Res_NameM")) & "', '" & _
                Chk(dt.Rows(0).Item("Res_NameF")) & "', '" & _
                Chk(dt.Rows(0).Item("Res_MobileNO")) & "', '" & _
                Chk(dt.Rows(0).Item("Res_MPhone")) & "', '" & _
                Chk(dt.Rows(0).Item("Res_MPin")) & "', '" & _
                Chk(dt.Rows(0).Item("Res_MState")) & "', '" & _
                ChkN(dt.Rows(0).Item("Res_MStatus")) & "', '" & _
                Chk(dt.Rows(0).Item("Res_PAddr1")) & "', '" & _
                Chk(dt.Rows(0).Item("Res_PAddr2")) & "', '" & _
                Chk(dt.Rows(0).Item("Res_PAddr3")) & "', '" & _
                Chk(dt.Rows(0).Item("Res_PCity")) & "', '" & _
                Chk(dt.Rows(0).Item("Res_PCountry")) & "', '" & _
                Chk(dt.Rows(0).Item("Res_PPin")) & "', '" & _
                Chk(dt.Rows(0).Item("Res_PState")) & "', '" & _
                dt.Rows(0).Item("res_Sex") & "', '" & _
                Chk(dt.Rows(0).Item("Res_EMailId")) & "', '" & _
                Chk(dt.Rows(0).Item("Res_PhoneNo")) & "', '" & _
                Chk(dt.Rows(0).Item("Res_Nationality")) & "')"
            Session("DalObj").ExecuteCommand(SqlStr, Tran)


            '=====================Insert HrdQual=================================================
            For i = 0 To dt1.Rows.Count - 1
                SqlStr = "Insert HrdQual(Emp_Code,Qual_Code,Place,Passing_Year,Subjects,Marks_Per,Grade,Univ_Code,College) Values ( '" & _
                                    Chk(TxtEmpCode.Text) & "', '" & _
                                    Chk(dt1.Rows(i).Item("Qual_Code")) & "', '" & _
                                    Chk(dt1.Rows(i).Item("Place")) & "', '" & _
                                    Chk(dt1.Rows(i).Item("Passing_Year")) & "', '" & _
                                    Chk(dt1.Rows(i).Item("Subjects")) & "', '" & _
                                    dt1.Rows(i).Item("Marks_Per") & "', '" & _
                                    Chk(dt1.Rows(i).Item("Grade")) & "', '" & _
                                    Chk(dt1.Rows(i).Item("Univ_Code")) & "', '" & _
                                    Chk(dt1.Rows(i).Item("College")) & "')"
                Session("DalObj").ExecuteCommand(SqlStr, Tran)
            Next


            '=====================Insert HRDEXP=================================================
            For i = 0 To dt2.Rows.Count - 1
                SqlStr = "Insert HrdExp(Emp_Code,Org_Name,Exp_From, Exp_To,Exp_Years,Drawn_Sal,JobProfile,LeavingReason,Dsg_Name) Values ( '" & _
                                    Chk(TxtEmpCode.Text) & "', '" & _
                                    Chk(dt2.Rows(i).Item("Org_Name")) & "', '" & _
                                    dt2.Rows(i).Item("Exp_From") & "', '" & _
                                    dt2.Rows(i).Item("Exp_To") & "', '" & _
                                    dt2.Rows(i).Item("Exp_Years") & "', '" & _
                                    dt2.Rows(i).Item("Drawn_Sal") & "', '" & _
                                    Chk(dt2.Rows(i).Item("JobResponsiblities")) & "', '" & _
                                    Chk(dt2.Rows(i).Item("LeavingReason")) & "', '" & _
                                    Chk(dt2.Rows(i).Item("Dsg_Name")) & "')"
                Session("DalObj").ExecuteCommand(SqlStr, Tran)
            Next
            SqlStr = "update resmast set status_code='113' where res_code= '" & Chk(cmbResume.SelectedValue) & "' "
            Session("DalObj").ExecuteCommand(SqlStr, Tran)
            Tran.Commit()
            SetMsg(LblErrMsg, "Record Save Successfully")
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & ex.Source)
            If Not IsNothing(Tran) Then Tran.Rollback("Save")
        Finally
            If Not IsNothing(Tran) Then Tran.Dispose()
        End Try
    End Sub


    Function IsValidate()
        Dim Count As Int16
        If Chk(TxtEmpCode.Text) = "" Then
            SetMsg(LblErrMsg, " Employee Code Can not be left blank.")
            Return False
        End If
        If ChkDelete.Checked = False Then
            Count = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand("Select Count(*) From HRDMAST  Where EMP_Code =  '" & (Chk(TxtEmpCode.Text)) & "'" & Session("UserCodes"), , DAL.DataLayer.ExecutionType.ExecuteScalar)
            If Count <> 0 Then
                SetMsg(LblErrMsg, "This Employee Code Already Exist.")
                Return False
            End If
        End If
        If Chk(dtpADOJ.Text) = "" Then
            SetMsg(LblErrMsg, " Actual Date of Joining Can not be left blank.")
            Return False
        End If
        If Chk(cmbDept.SelectedValue) = "" Then
            SetMsg(LblErrMsg, " Department Can not be left blank.")
            Return False
        End If
        If Chk(cmbDesg.SelectedValue) = "" Then
            SetMsg(LblErrMsg, " Designation Can not be left blank.")
            Return False
        End If
        Return True
    End Function


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub
End Class
