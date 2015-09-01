Partial Class FrmPFChallanNew
#Region "Inhereted Controls"
    Inherits System.Web.UI.Page
    Dim dt As DataTable
    Dim ds As New DataSet
    Dim BAL As BAL.BLayer
    Dim DAL As DAL.DataLayer
    Dim DalUser As DAL.DataLayer.Users
    Protected WithEvents ChkShow As System.Web.UI.WebControls.CheckBox
    Protected WithEvents TxtTDS1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents TxtSurchage1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents TxtCess1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents TxtTotal1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents TxtCess2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Dim VarComp_Code As String
#End Region
    Dim DtDateS, DtDateE As Date
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
            DAL = Session("DalObj")
            BAL = Session("BalObj")
            DtDateS = Format(FY_Start, "dd/MMM/yyyy")
            DtDateE = Format(FY_End, "dd/MMM/yyyy")
            If Not IsPostBack Then
                'By Ravi 21 Nov
                Dim SrNo As String
                SrNo = Request.QueryString.Item("SrNo")
                Dim bSuccess As Boolean
                Select Case CheckRight(SrNo)
                    Case MdlHRMS.AccessType.FullAccess, MdlHRMS.AccessType.SuperUser
                        bSuccess = True
                    Case MdlHRMS.AccessType.ReadonlyAccess, MdlHRMS.AccessType.Restricted
                        bSuccess = False
                End Select
                CmdSave.Visible = bSuccess
                CmdDelete.Visible = bSuccess
                '------------------------------------

                BAL.FillCombo(cmbChallanNo, "select challanNo,challanNo from PfChallanMast", True)
                BAL.FillCombo(cmbMonth, "select (Right('0' + DateName(d,PayDate),2) + '/' + Left(DateName(mm,PayDate),3) + '/' + DateName(yy,PayDate)) As paydate, datename(MM,paydate) + ' - ' + datename(yyyy,paydate) as mon FROM monupdate where PayDate>='" & DtDateS & "' and PayDate<='" & DtDateE & "' group by paydate", True)
                BAL.FillCombo(cmbBankName, "Bank_Code", "Bank_Name", "BANKMAST", True)
                BAL.FillCombo(CmbLocation, "Loc_Code", "Loc_Name", "LocMast", True)
                DTPChallanDate.Text = Format(Date.Today, "dd/MMM/yyyy")
                DTPChequeDate.Text = Format(Date.Today, "dd/MMM/yyyy")
                'btnNew_Click(sender, Nothing)
            End If
            CmdSave.Attributes.Add("onclick", "return ValidateCtrl();")
            CmdDelete.Attributes.Add("onclick", "return ConfirmDelete();")
        Catch ex As Exception
            SetMsg(LblError, ex.Message)
        End Try

    End Sub
    Private Sub cmbChallanNo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbChallanNo.SelectedIndexChanged
        Try
            If cmbChallanNo.SelectedIndex <> cmbChallanNo.Items.Count - 1 Then
                Txtrefno.Visible = True
                btnList.Visible = True
                btnNew.Visible = True
                cmbChallanNo.Visible = False
                Txtrefno.ToolTip = cmbChallanNo.SelectedValue
                Txtrefno.Text = cmbChallanNo.SelectedItem.Text
                ViewState("ReqCode") = Txtrefno.Text
                ViewState("Action") = "MODIFY"
                DisplayRecord()
                fillDataGrid()
            End If
        Catch ex As Exception
            SetMsg(LblError, ex.Message)
        End Try
    End Sub
    Private Sub btnList_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnList.Click
        cmbChallanNo.Visible = True
        Txtrefno.Visible = False
        btnList.Visible = False
        btnNew.Visible = True
    End Sub
    Private Function fillDataGrid()
        Try
            Dim vardate As String
            vardate = cmbMonth.SelectedValue
            Dim dt As New DataTable
            Dim strsql As String
            strsql = "SELECT PayHist.Emp_Code,HrdHistQry.LType, HrdHistQry.Emp_Name, PayHist.PFSalary, PayHist.FPFSalary, PayHist.PF, PayHist.VPF, PayHist.FPF, PayHist.EPF, PayHist.EFPF FROM PayHist, HrdHistQry WHERE PayHist.Emp_Code=HrdHistQry.Emp_Code AND PayHist.PayDate='" & vardate & "'"
            DAL.GetSqlDataTable(dt, strsql)
            ' Session("dt") = dt

            If dt.Rows.Count > 0 Then
                GrdRecords.DataSource = dt
                GrdRecords.DataBind()
                TotalPage.Text = "Total Pages:->    " & GrdRecords.PageCount
                CurrentPage.Text = "Current Page:->   " & GrdRecords.CurrentPageIndex + 1
                NoEmp.Text = "Total Employee:->    " & dt.Rows.Count
                MonthOf.Text = "Month Of   " & cmbMonth.SelectedItem.Text
            End If

        Catch ex As Exception
            LblError.Text = ex.Message
        End Try
    End Function
    Private Sub cmbMonth_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbMonth.SelectedIndexChanged
        If cmbMonth.SelectedItem.Text = "" Then
            GrdRecords.Visible = False
            LblError.Text = "Sorry No Match(s) Found: "
        End If
        LblEmpDetails.Visible = True
        NoEmp.Visible = True
        TotalPage.Visible = True
        CurrentPage.Visible = True
        MonthOf.Visible = True
        fillDataGrid()
        DisplayComp()
        FieldSum()
        Cal()
    End Sub
    Private Sub GrdRecords_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles GrdRecords.PageIndexChanged
        GrdRecords.CurrentPageIndex = e.NewPageIndex
        fillDataGrid()
    End Sub
    Private Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdSave.Click
        Dim Tran As SqlClient.SqlTransaction
        Try
            Dim SqlStr, str1 As String
            Dim cnt As Integer
            Dim dt As New DataTable
            If Not IsValidate() Then Exit Sub
            VarComp_Code = Session("DalObj").ExecuteCommand(" Select Comp_code From Compmast", , DAL.ExecutionType.ExecuteScalar)
            str1 = "SELECT PayHist.Emp_Code,HrdHistQry.LType, HrdHistQry.Emp_Name, PayHist.PFSalary, PayHist.FPFSalary, PayHist.PF, PayHist.VPF, PayHist.FPF, PayHist.EPF, PayHist.EFPF FROM PayHist, HrdHistQry WHERE PayHist.Emp_Code=HrdHistQry.Emp_Code AND PayHist.PayDate='" & (cmbMonth.SelectedValue) & "'"
            DAL.GetSqlDataTable(dt, str1)
            If ViewState("Action") <> "MODIFY" Then
                Tran = DAL.StartTransaction("Save")
                SqlStr = " Insert PFChallanMast (ChallanNo, ChallanDate, PayDate, Comp_Code, Loc_Code, PFSalary, FPFSalary, PFPer, PF, FPFPer, FPF, EFPFPer, EFPF, EDLIPer, EDLI, PFAdminChargesPer, PFAdminCharges, EDLIAdminChargesPer, EDLIAdminCharges, HeadCount, Joinees, Resignees, RemittanceDate, Bank_Code, ChqNo, ChqDate) Values ('" & _
                                      Chk(Txtrefno.Text) & "', '" & _
                                      Format(CDate(DTPChallanDate.Text), "dd-MMM-yyyy") & "', '" & _
                                      cmbMonth.SelectedValue & "', '" & _
                                      (DalUser.CurrentCompID) & "', '" & _
                                      CmbLocation.SelectedValue & "', '" & _
                                      ChkN(TxtWages1.Text) & "', '" & _
                                      ChkN(TxtWages2.Text) & "', '" & _
                                      ChkN(TxtES1.Text) & "', '" & _
                                      ChkN(TxtES3.Text) & "', '" & _
                                      ChkN(TxtER1.Text) & "', '" & _
                                      ChkN(TxtER4.Text) & "', '" & _
                                      ChkN(TxtES2.Text) & "', '" & _
                                      ChkN(TxtES4.Text) & "', '" & _
                                      ChkN(TxtER3.Text) & "', '" & _
                                      ChkN(TxtER6.Text) & "', '" & _
                                      ChkN(TxtAC1.Text) & "', '" & _
                                      ChkN(TxtAC3.Text) & "', '" & _
                                      ChkN(TxtAC2.Text) & "', '" & _
                                      ChkN(TxtAC4.Text) & "', '" & _
                                      ChkN(TxtNetHead.Text) & "', '" & _
                                      ChkN(TxtJoinee.Text) & "', '" & _
                                      ChkN(TxtResignee.Text) & "', '" & _
                                      (DTPRemitance.DateValue) & "','" & _
                                      (cmbBankName.SelectedValue) & "', '" & _
                                      ChkN(TxtChequeNo.Text) & "', '" & _
                                      Chk(Format(CDate(DTPChequeDate.Text), "dd-MMM-yyyy")) & "')"
                DAL.ExecuteCommand(SqlStr, Tran)

                For cnt = 0 To dt.Rows.Count - 1
                    SqlStr = "Insert into PfChallanTran(ChallanNo,Emp_Code,PFSALARY,FPFSalary,PF,EPF,EFPF,FPF)"
                    SqlStr &= "Values('"
                    SqlStr &= Txtrefno.Text & "','"
                    SqlStr &= ChkN(dt.Rows(cnt).Item("Emp_Code")) & "','"
                    SqlStr &= ChkN(dt.Rows(cnt).Item("PFSALARY")) & "','"
                    SqlStr &= ChkN(dt.Rows(cnt).Item("FPFSalary")) & "','"
                    SqlStr &= ChkN(dt.Rows(cnt).Item("PF")) & "','"
                    SqlStr &= ChkN(dt.Rows(cnt).Item("EPF")) & "','"
                    SqlStr &= ChkN(dt.Rows(cnt).Item("EFPF")) & "','"
                    SqlStr &= ChkN(dt.Rows(cnt).Item("FPF")) & "')"
                    DAL.ExecuteCommand(SqlStr, Tran)
                Next
            Else
                '***********************Upadte PFCHALLANMAST
                If ViewState("Action") = "Modify" Then

                    Dim StrUpdate As String
                    Dim rsCmpMast As New DataTable
                    Dim DtDate As Date
                    '==========Update Query
                    StrUpdate = "Update PfChallanMast Set ChallanDate='" & Format(CDate(DTPChallanDate.Text)) & "',PayDate = '" & cmbMonth.SelectedValue & "'Comp_Code = '" & (DalUser.CurrentCompID) & "',Loc_Code = '" & CmbLocation.SelectedValue & "',PfSalary = '" & ChkN(TxtWages1.Text) & "',PfPer = '" & ChkN(TxtES1.Text) & "',PF = '" & ChkN(TxtWages2.Text) & "',FPFPer = '" & ChkN(TxtES3.Text) & "',FPF = '" & ChkN(TxtER1.Text) & "',EFPFPer = '" & ChkN(TxtER4.Text) & "',EFPF = '" & ChkN(TxtES2.Text) & "',EDLIPer = '" & ChkN(TxtES4.Text) & "',EDLI = '" & ChkN(TxtER3.Text) & "',PFAdminChargesPer = '" & ChkN(TxtER6.Text) & "',PFAdminCharges = '" & ChkN(TxtAC1.Text) & "',EDLIAdminCharges = '" & ChkN(TxtAC3.Text) & "',HeadCount = '" & ChkN(TxtNetHead.Text) & "',Joinees = '" & ChkN(TxtJoinee.Text) & "',RemittanceDate = '" & (DTPRemitance.DateValue) & "',Bank_Code = '" & (cmbBankName.SelectedValue) & "',ChqNo = '" & Chk(TxtChequeNo.Text) & "',Chqdate = '" & Format(CDate(DTPChequeDate.Text)) & "' Where ChallanNo= '" & Chk(Txtrefno.Text) & "'"
                    Session("DalObj").ExecuteCommand(StrUpdate)
                    '======
                    'StrUpdate = " Update PfChallanMast Set " & _
                    ' " ChallanDate = '" & Format(CDate(DTPChallanDate.Text) & "', " & _
                    ' " PayDate = '" & cmbMonth.SelectedValue & "', " & _
                    ' " Comp_Code = '" & (DalUser.CurrentCompID) & "', " & _
                    ' " Loc_Code = '" & CmbLocation.SelectedValue & "', " & _
                    ' " PfSalary = '" & ChkN(TxtWages1.Text) & "', " & _
                    ' " PfPer = '" & ChkN(TxtES1.Text) & "', " & _
                    ' " PF = '" & ChkN(TxtWages2.Text) & "', " & _
                    ' " FPFPer = '" & ChkN(TxtES3.Text) & "', " & _
                    ' " FPF = '" & ChkN(TxtER1.Text) & "', " & _
                    ' " EFPFPer = '" & ChkN(TxtER4.Text) & "', " & _
                    ' " EFPF = '" & ChkN(TxtES2.Text) & "', " & _
                    ' " EDLIPer = '" & ChkN(TxtES4.Text) & "', " & _
                    ' " EDLI = '" & ChkN(TxtER3.Text) & "', " & _
                    ' " PFAdminChargesPer = '" & ChkN(TxtER6.Text) & "', " & _
                    ' " PFAdminCharges = '" & ChkN(TxtAC1.Text) & "', " & _
                    ' " EDLIAdminCharges = '" & ChkN(TxtAC3.Text) & "', " & _
                    ' " HeadCount = '" & ChkN(TxtNetHead.Text) & "', " & _
                    ' " Joinees = '" & ChkN(TxtJoinee.Text) & "', " & _
                    ' " Resignees = '" & ChkN(TxtResignee.Text) & "', " & _
                    ' " RemittanceDate = '" & (DTPRemitance.DateValue) & "', " & _
                    ' " Bank_Code = '" & (cmbBankName.SelectedValue) & "', " & _
                    ' " ChqNo = '" & Chk(TxtChequeNo.Text) & "', " & _
                    ' " Chqdate = '" & Format(CDate(dtpChequeDate.Text) & "', " & _
                    ' " Where ChallanNo= '" & Chk(Txtrefno.Text) & "'"))
                    'Session("DalObj").ExecuteCommand(StrUpdate)
                End If
            End If
            Tran.Commit()
            SetMsg(LblError, "PF Challan Entry Has been Saved Successfully.")
        Catch ex As Exception
            SetMsg(LblError, ex.Message)
            Tran.Rollback()

        Finally
            If Not IsNothing(Tran) Then Tran.Dispose()
        End Try

        BAL.FillCombo(cmbChallanNo, "select challanNo,challanNo from PfChallanMast", True)
        GrdRecords.Visible = False
        ClearAll(Me)
        ClearLable()
    End Sub
    Private Function ClearLable()
        TotalPage.Visible = False
        CurrentPage.Visible = False
        NoEmp.Visible = False
        MonthOf.Visible = False
    End Function
    Function IsValidate() As Boolean
        'If Trim(Txtrefno.Text) = "" Then
        '    SetMsg(LblError, "Enter the Challan No.")
        '    Return False
        'End If
        'If Trim(cmbMonth.SelectedItem.Text) = "" Then
        '    SetMsg(LblError, "Please select a month")
        '    Return False
        'End If

        'If CmbLocation.SelectedItem.Text = "" Then
        '    SetMsg(LblError, "Please Select a location")
        '    Return False
        'End If
        Return True
    End Function
    Private Function DisplayComp()
        Try
            Dim sqlstr As String
            Dim dt As New DataTable
            sqlstr = "Select * from compmast"
            DAL.GetSqlDataTable(dt, sqlstr)
            If dt.Rows.Count > 0 Then
                TxtER3.Text = dt.Rows(0).Item("DLI_PER")
                TxtAC2.Text = dt.Rows(0).Item("DLI_ADM_CH")
                TxtES1.Text = dt.Rows(0).Item("EFPF_PER")
                TxtER1.Text = dt.Rows(0).Item("PF_PER")
                TxtER2.Text = dt.Rows(0).Item("PF_PER")
                TxtAC1.Text = dt.Rows(0).Item("PF_ADM_CH")
                TxtES2.Text = dt.Rows(0).Item("EPF_PER")
            End If
        Catch ex As Exception
            LblError.Text = ex.Message
        End Try
    End Function
    Private Function Cal()
        Try
            Dim I, j, h, q, k, l, m, n, o, p As Integer
            Dim a, b, c, d, e, f, g As Double

            I = CType(TxtWages1.Text, Integer)
            j = CType(TxtWages2.Text, Integer)
            h = CType(TxtWages3.Text, Integer)

            a = CType(TxtES1.Text, Double)
            b = CType(TxtER1.Text, Double)
            c = CType(TxtAC1.Text, Double)

            d = CType(TxtES2.Text, Double)
            e = CType(TxtER2.Text, Double)
            f = CType(TxtAC2.Text, Double)

            g = CType(TxtER3.Text, Double)

            k = (I * a) / 100
            TxtES3.Text = k

            l = (I * b) / 100
            TxtER4.Text = l

            m = (I * c) / 100
            TxtAC3.Text = m

            n = (j * d) / 100
            TxtES4.Text = n

            o = (j * e) / 100
            TxtER5.Text = o

            p = (h * g) / 100
            TxtER6.Text = p

            q = (h * f) / 100
            TxtAC4.Text = q


        Catch ex As Exception
            LblError.Text = ex.Message
        End Try
    End Function
    Public Function FieldSum()
        Try
            Dim cnt, cnt1 As Integer
            Dim dtdata1 As New DataTable
            Dim strsql As String
            Dim Mondate As String
            Mondate = cmbMonth.SelectedValue
            strsql = "SELECT PayHist.Emp_Code,HrdHistQry.LType, HrdHistQry.Emp_Name, PayHist.PFSalary, PayHist.FPFSalary, PayHist.PF, PayHist.VPF, PayHist.FPF, PayHist.EPF, PayHist.EFPF FROM PayHist, HrdHistQry WHERE PayHist.Emp_Code=HrdHistQry.Emp_Code AND PayHist.PayDate='" & Mondate & "'"
            DAL.GetSqlDataTable(dtdata1, strsql)
            cnt1 = dtdata1.Rows.Count
            Dim Temp1, Temp2, Temp3, temp4 As Integer
            Temp1 = 0
            Temp2 = 0
            Temp3 = 0
            For cnt = 0 To cnt1 - 1
                Temp1 = Temp1 + dtdata1.Rows(cnt).Item("PFSALARY")
                Temp2 = Temp2 + dtdata1.Rows(cnt).Item("FPFSALARY")
                temp4 = Temp1 + Temp3 + Temp2
                TxtTDS2.Text = Temp1
                TxtSurchage2.Text = Temp2
            Next
            TxtTDS2.Text = Temp1
            TxtSurchage2.Text = Temp2
            TxtTotal2.Text = temp4
            TxtNetHead.Text = cnt
            TxtWages1.Text = Temp1
            TxtWages2.Text = Temp2
            TxtWages3.Text = Temp2
        Catch ex As Exception
            LblError.Text = ex.Message
        End Try
    End Function
    Private Function DisplayRecord()
        Try
            Dim sqlstr As String
            Dim dtMast As New DataTable
            sqlstr = "Select * from PfChallanMast where ChallanNo='" & Chk(Txtrefno.Text) & "'"
            DAL.GetSqlDataTable(dtMast, sqlstr)

            If dtMast.Rows.Count > 0 Then
                With dtMast.Rows(0)
                    TxtChequeNo.Text = ChkN(.Item("ChqNo"))
                    TxtNetHead.Text = ChkN(.Item("HeadCount"))
                    TxtResignee.Text = ChkN(.Item("Resignees"))
                    TxtJoinee.Text = ChkN(.Item("Joinees"))
                    ChkCombo(CmbLocation, .Item("Loc_Code"))
                    ChkCombo(cmbBankName, .Item("Bank_Code"))

                    TxtWages1.Text = ChkN(.Item("PFSalary"))
                    TxtWages2.Text = ChkN(.Item("FPFSalary"))
                    TxtWages3.Text = ChkN(.Item("FPFSalary"))

                    TxtES1.Text = ChkN(.Item("PFPer"))
                    TxtES3.Text = ChkN(.Item("PF"))

                    TxtER1.Text = ChkN(.Item("EPFPer"))
                    TxtER4.Text = ChkN(.Item("EPF"))

                    TxtAC1.Text = ChkN(.Item("PFAdminChargesPer"))
                    TxtAC3.Text = ChkN(.Item("PFAdminCharges"))

                    TxtES2.Text = ChkN(.Item("PF"))
                    TxtES4.Text = ChkN(.Item("PF"))


                    TxtER2.Text = ChkN(.Item("EFPFPer"))
                    TxtER5.Text = ChkN(.Item("EFPF"))

                    TxtAC2.Text = ChkN(.Item("EDLIAdminChargesPer"))
                    TxtAC4.Text = ChkN(.Item("EDLIAdminCharges"))

                    TxtER3.Text = ChkN(.Item("EDLIPer"))
                    TxtER6.Text = ChkN(.Item("EDLI"))

                    If IsDBNull(.Item("ChallanDate")) Then
                        DTPChallanDate.Text = Format(Date.Today, "dd/MMM/yyyy")
                    Else
                        DTPChallanDate.Text = Format(CDate(.Item("ChallanDate")), "dd/MMM/yyyy")
                    End If
                    If IsDBNull(.Item("RemittanceDate")) Then
                        DTPRemitance.DateValue = Date.Today
                    Else
                        DTPRemitance.DateValue = CDate(.Item("RemittanceDate"))
                    End If
                    If IsDBNull(.Item("ChqDate")) Then
                        DTPChequeDate.Text = Format(Date.Today, "dd/MMM/yyyy")
                    Else
                        DTPChequeDate.Text = Format(CDate(.Item("ChqDate")), "dd/MMM/yyyy")
                    End If
                    TxtTDS2.Text = ChkN(.Item("PFSalary"))
                    TxtSurchage2.Text = ChkN(.Item("FPFSalary"))

                    Dim i, j As Integer
                    i = ChkN(.Item("PFSalary"))
                    j = ChkN(.Item("FPFSalary"))
                    TxtTotal2.Text = i + j
                End With
                'BAL.FillCombo(cmbMonth, "Select Distinct Paydate,datename(MM,paydate) + '-' +datename(YYYY,paydate) as Mon from PFCHALLANMast where ChallanNo='" & Chk(Txtrefno.Text) & "'")
            End If
            ShowGrdRecord()
        Catch ex As Exception
            LblError.Text = ex.Message
        End Try
    End Function
    Private Function ShowGrdRecord()
        Try
            Dim dt As New DataTable
            Dim strsql As String
            strsql = "Select PF.ChallanNo ,PF.Emp_Code, HrdHistQry.Emp_Name, PF.PFSalary, PF.FPFSalary, PF.PF, PF.FPF, PF.EPF, PF.EFPF  from PFCHALLANTRAN PF, HrdHistQry where  PF.Emp_Code=HrdHistQry.Emp_Code AND ChallanNo='" & Chk(Txtrefno.Text) & "'"
            DAL.GetSqlDataTable(dt, strsql)
            If dt.Rows.Count > 0 Then
                GrdRecords.DataSource = dt
                GrdRecords.DataBind()
                TotalPage.Text = "Total Pages:->    " & GrdRecords.PageCount
                CurrentPage.Text = "Current Page:->   " & GrdRecords.CurrentPageIndex + 1
                NoEmp.Text = "Total Employee:->    " & dt.Rows.Count
                MonthOf.Text = "Month Of   " & cmbMonth.SelectedItem.Text
            End If
        Catch ex As Exception
            LblError.Text = ex.Message
        End Try
    End Function
    Private Sub CmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNew.Click
        Try
            ClearAll(Me)
            cmbChallanNo.Visible = False
            Txtrefno.Visible = True
            Txtrefno.Text = ""
            LblError.Text = ""
            ViewState("Action") = "ADDNEW"
            '==================== old Qry ============== BAL.FillCombo(cmbMonth, "SELECT DISTINCT PayDate,datename(MM,paydate) + ' - ' + datename(yyyy,paydate) as mon FROM monupdate ORDER BY paydate", True)
            BAL.FillCombo(cmbMonth, "select (Right('0' + DateName(d,PayDate),2) + '/' + Left(DateName(mm,PayDate),3) + '/' + DateName(yy,PayDate)) As paydate, datename(MM,paydate) + ' - ' + datename(yyyy,paydate) as mon FROM monupdate where PayDate>='" & DtDateS & "' and PayDate<='" & DtDateE & "' group by paydate", True)
        Catch ex As Exception
            SetMsg(LblError, ex.Message & " : (btnNew_Click)")
        End Try
    End Sub
    Private Sub CmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdDelete.Click
        DeleteRecord()
    End Sub
    Private Function DeleteRecord()
        Try
            Dim Temp, Temp1 As String
            Dim ChNo As Integer
            ChNo = Txtrefno.Text
            Temp = "Delete From PFChallanMast where ChallanNo='" & ChNo & "'"
            DAL.ExecuteCommand(Temp)
            Temp1 = "Delete From Form16 where Challan_No='" & Txtrefno.Text & "'"
            DAL.ExecuteCommand(Temp1)
            LblError.Text = "Record Deleted Sucessfully."
            ClearAll(Me)
            BAL.FillCombo(cmbChallanNo, "select challanNo,challanNo from PfChallanMast", True)
            GrdRecords.Visible = False
            Txtrefno.Visible = True
            btnList.Visible = True
            TotalPage.Visible = False
            CurrentPage.Visible = False
            NoEmp.Visible = False
            MonthOf.Visible = False
        Catch ex As Exception
            SetMsg(LblError, ex.Message & "DeleteRecord")
        End Try
        ''''''==========BAL.FillCombo(cmbMonth, "SELECT DISTINCT PayDate,datename(MM,paydate) + ' - ' + datename(yyyy,paydate) as mon FROM monupdate ORDER BY paydate", True)
        BAL.FillCombo(cmbMonth, "select (Right('0' + DateName(d,PayDate),2) + '/' + Left(DateName(mm,PayDate),3) + '/' + DateName(yy,PayDate)) As paydate, datename(MM,paydate) + ' - ' + datename(yyyy,paydate) as mon FROM monupdate where PayDate>='" & DtDateS & "' and PayDate<='" & DtDateE & "' group by paydate", True)
    End Function
    Private Sub Txtrefno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txtrefno.TextChanged
        Try
            Dim SQLSTR As String
            Dim cnt As Int16
            cnt = ChkN(DAL.ExecuteCommand("select Count(ChallanNo)  from PFCHALLANMAST where ChallanNo='" & Chk(Txtrefno.Text) & "'", , DAL.ExecutionType.ExecuteScalar))
            If cnt > 0 Then
                Txtrefno.Visible = True
                btnList.Visible = True
                btnNew.Visible = True
                cmbChallanNo.Visible = False
                Txtrefno.ToolTip = cmbChallanNo.SelectedValue
                Txtrefno.Text = cmbChallanNo.SelectedItem.Text
                ViewState("ReqCode") = Txtrefno.Text
                ViewState("Action") = "MODIFY"
            Else
                SetMsg(LblError, "This Challan No. Does Not Exists.")
            End If
        Catch ex As Exception
            SetMsg(LblError, ex.Message)
        End Try
    End Sub

    Private Sub GrdRecords_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GrdRecords.SelectedIndexChanged

    End Sub
End Class
