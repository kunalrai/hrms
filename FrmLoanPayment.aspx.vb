
Partial Class FrmLoanPayment
    Inherits System.Web.UI.Page
    Dim sqlstr As String

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Dtpcombo2 As DITWebLibrary.DTPCombo
    Protected WithEvents DtpRecFrom As DITWebLibrary.DTPCombo

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

                End If
                '---------------------------------------------
                Session("BalObj").FillCombo(cmbEmp, "Select Emp_Code,Emp_Name as EMP_NAME From HrdMastQry Where  LType=1 " & Session("UserCodes") & " Order by Emp_Name", True)
                DtpSanction.DateValue = Format(Date.Today, "dd/MMM/yyyy")
                Session("BalObj").FillCombo(CmbLoanType, "Select Field_name, Field_desc from paysetup where fld_categ='3'", True)
                LblErrMsg.Text = ""
            End If
            BtnSave.Attributes.Add("onclick", "return ValidateCtrl();")
        Catch EX As Exception
            LblErrMsg.Text = EX.Message & "Page Load"
        End Try
    End Sub

    Private Sub btnList_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnList.Click
        TxtCode.Visible = False
        btnList.Visible = False
        cmbEmp.Visible = True
        LblErrMsg.Text = ""
    End Sub

    Private Sub cmbEmp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbEmp.SelectedIndexChanged
        Try
            LblErrMsg.Text = ""
            If cmbEmp.SelectedValue <> "" Then
                TxtCode.Text = cmbEmp.SelectedValue
            Else
                TxtCode.Text = ""
            End If
            TxtCode_TextChanged(sender, e)
            TxtCode.Visible = True
            btnList.Visible = True
            cmbEmp.Visible = False
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & "cmbEmp_SelectedIndexChanged")
        End Try
    End Sub
    Sub DisplayRecords()
        Try
            Dim StrSQl As String, dtEmp As New DataTable
            Dim EmpName As Object
            StrSQl = "Select * From HrdMastQry Where Emp_Code = '" & Chk(TxtCode.Text) & "'" & Session("UserCodes")
            Session("DalObj").GetSqlDataTable(dtEmp, StrSQl)
            EmpName = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(" Select Emp_Name From HrdMastQry Where Emp_Code = '" & Chk(TxtCode.Text) & "'" & Session("UserCodes"), , DAL.DataLayer.ExecutionType.ExecuteScalar)

            If (Not IsDBNull(EmpName)) And (Not IsNothing(EmpName)) Then
                LblName.Text = EmpName
            Else
                Dim Code As Object
                Code = Session("DalObj").ExecuteCommand("Select Emp_Code From HrdMastQry Where Emp_Code = '" & Chk(TxtCode.Text) & "'" & Session("UserCodes"), , DAL.DataLayer.ExecutionType.ExecuteScalar)
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
            TxtLBalance.Text = 0
            TxtIBalance.Text = 0
            LblErrMsg.Text = ""
            DisplayRecords()
            If CmbLoanType.SelectedValue = "" Then Exit Sub
            ShowRecord()
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " TxtCode_TextChanged")
        End Try

    End Sub

    Private Sub CmbLoanType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbLoanType.SelectedIndexChanged
        Try
            TxtLBalance.Text = 0
            LblErrMsg.Text = ""
            TxtIBalance.Text = 0
            If TxtCode.Text <> "" Then
                ShowRecord()
            Else
                SetMsg(LblErrMsg, "Please Select Employee Code.")
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " CmbLoanType_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ShowRecord()
        Try
            If cmbEmp.SelectedIndex = 0 Or CmbLoanType.SelectedValue = "" Then Exit Sub
            Dim DtLaMast As New DataTable

            sqlstr = "Select * from LaMast Where Emp_Code='" & Chk(TxtCode.Text) & "' AND L_Code='" & Chk(CmbLoanType.SelectedValue) & "' AND L_Date='" & (DtpSanction.DateValue) & "'"
            Session("DalObj").GetSqldatatable(DtLaMast, sqlstr)

            If DtLaMast.Rows.Count = 1 Then

                cmbinstmnttype.SelectedValue = ChkN(DtLaMast.Rows(0).Item("Inst_Type"))
                TxtIntRate.Text = ChkN(DtLaMast.Rows(0).Item("I_Rate"))

                txtloanamt.Text = ChkN(DtLaMast.Rows(0).Item("L_Amt"))
                txtintamt.Text = ChkN(DtLaMast.Rows(0).Item("I_Amt"))

                txtLinstlamt.Text = ChkN(DtLaMast.Rows(0).Item("L_Inst_Amt"))
                txtIinstlamt.Text = ChkN(DtLaMast.Rows(0).Item("I_Inst_Amt"))

                TxtLinstlNo.Text = ChkN(DtLaMast.Rows(0).Item("L_Inst_No"))
                TxtIinstlNo.Text = ChkN(DtLaMast.Rows(0).Item("I_Inst_No"))

                TxtLRecover.Text = ChkN(DtLaMast.Rows(0).Item("L_Rec"))
                TxtIRrecover.Text = ChkN(DtLaMast.Rows(0).Item("I_Rec"))

                TxtLBalance.Text = (Val(txtloanamt.Text) - Val(TxtLRecover.Text))
                TxtIBalance.Text = (Val(txtintamt.Text) - Val(TxtIRrecover.Text))

                LblExpDate.Text = Format((DateAdd("m", Val(TxtLinstlNo.Text), DtpLRecFrom.DateValue)), "dd/MMM/yyyy")

                If IsDBNull(DtLaMast.Rows(0).Item("L_SDATE")) Then
                    ChkLRecFrom.Checked = False
                    DtpLRecFrom.Enabled = False
                    DtpLRecFrom.DateValue = Date.Today
                Else
                    ChkLRecFrom.Checked = True
                    DtpLRecFrom.Enabled = True
                    DtpLRecFrom.DateValue = CDate(DtLaMast.Rows(0).Item("L_SDATE"))
                End If

                If IsDBNull(DtLaMast.Rows(0).Item("I_SDATE")) Then
                    ChkIRecFrom.Checked = False
                    DtpIRecFrom.Enabled = False
                    DtpIRecFrom.DateValue = Date.Today
                Else
                    ChkIRecFrom.Checked = True
                    DtpIRecFrom.Enabled = True
                    DtpIRecFrom.DateValue = CDate(DtLaMast.Rows(0).Item("I_SDATE"))
                End If
                viewstate("Action") = "Modify"
            Else
                txtLinstlamt.Text = "0"
                txtintamt.Text = "0"
                TxtIntRate.Text = "0"
                TxtLinstlNo.Text = "0"
                TxtLRecover.Text = "0"
                txtloanamt.Text = "0"
                TxtIinstlNo.Text = "0"
                txtIinstlamt.Text = "0"
                TxtIRrecover.Text = "0"
                LblExpDate.Text = ""
                ViewState("Action") = "AddNew"
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " ShowRecord")
        End Try
    End Sub

    Private Sub txtIinstlamt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIinstlamt.TextChanged
        LblErrMsg.Text = ""
        If Val(txtintamt.Text) > Val(txtIinstlamt.Text) Then
            If Val(txtIinstlamt.Text) > 0 Then
                I_Inst_Amt_No()
            End If
        Else
            SetMsg(LblErrMsg, "Installment Amount Can't be More Than Interest Amount.")
            txtIinstlamt.Text = 0
        End If
    End Sub

    Private Sub TxtIRrecover_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtIRrecover.TextChanged
        Try
            If Val(txtintamt.Text) - Val(TxtIRrecover.Text) < 0 Then
                SetMsg(LblErrMsg, "Balance can't be less than zero")
            Else
                TxtIBalance.Text = Val(txtIinstlamt.Text) - Val(TxtIRrecover.Text)
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " CmbLoanType_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub txtloanamt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtloanamt.TextChanged
        Try
            If Val(txtloanamt.Text) < Val(txtLinstlamt.Text) Then
                SetMsg(LblErrMsg, " Installment Amount will not Greater than Loan Amount")
                TxtLinstlNo.Text = ""

                Exit Sub
            End If
            If Val(txtloanamt.Text) - Val(TxtLRecover.Text) < 0 Then
                SetMsg(LblErrMsg, "Balance can't be less than zero")
            Else
                If Val(txtLinstlamt.Text) = 0 Or txtLinstlamt.Text = "" Then
                    TxtLinstlNo.Text = ""
                    Exit Sub
                Else
                    TxtLinstlNo.Text = ChkN(Val(txtloanamt.Text) / Val(txtLinstlamt.Text), True, 0)
                    TxtLBalance.Text = Val(txtloanamt.Text) - Val(TxtLRecover.Text)
                End If

            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " txtloanamt_TextChanged")
        End Try
    End Sub
    Private Sub txtLinstlamt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLinstlamt.TextChanged
        Try
            If Val(txtloanamt.Text) < Val(txtLinstlamt.Text) Then
                SetMsg(LblErrMsg, " Installment Amount will not Greater than Loan Amount")
                TxtLinstlNo.Text = ""
                Exit Sub
            Else
                If Val(txtLinstlamt.Text) = 0 Or txtLinstlamt.Text = "" Then
                    TxtLinstlNo.Text = ""
                    Exit Sub
                Else
                    TxtLinstlNo.Text = Int(Val(txtloanamt.Text) / Val(txtLinstlamt.Text))
                    TxtLinstlNo.Text = IIf(Int(TxtLinstlNo.Text) = Val(txtloanamt.Text) / Val(txtLinstlamt.Text), TxtLinstlNo.Text, Int(TxtLinstlNo.Text) + 1)
                    TxtLBalance.Text = Val(txtloanamt.Text) - Val(TxtLRecover.Text)
                End If

            End If

        Catch EX As Exception
            SetMsg(LblErrMsg, EX.Message & " txtLinstlamt_TextChanged")
        End Try

    End Sub
    Private Sub L_Inst_Amt_No()
        Dim i, J As Int16
        Try
            TxtLinstlNo.Text = FormatNumber(Val(txtloanamt.Text) / Val(txtLinstlamt.Text), 0)
            If Val(txtLinstlamt.Text) = 0 And Val(TxtLinstlNo.Text) > 0 Then
                i = (Val(txtloanamt.Text) / Val(TxtLinstlNo.Text))
                txtLinstlamt.Text = RndOff(i, 0, 0)
            End If
            J = TxtLinstlNo.Text
            LblExpDate.Text = Format((DateAdd("m", J, DtpLRecFrom.DateValue)), "dd/MMM/yyyy")
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & "L_Inst_Amt_No")
        End Try
    End Sub

    'Private Sub TxtLRecover_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtLRecover.TextChanged
    '    Try
    '        If Val(txtloanamt.Text) - Val(TxtLRecover.Text) < 0 Then
    '            SetMsg(LblErrMsg, "Balance can't be less than zero")
    '        Else
    '            TxtLBalance.Text = Val(txtloanamt.Text) - Val(TxtLRecover.Text)
    '        End If
    '    Catch ex As Exception
    '        SetMsg(LblErrMsg, ex.Message & "TxtLRecover_TextChanged")
    '    End Try
    'End Sub

    Private Sub I_Inst_Amt_No()
        Try
            TxtIinstlNo.Text = FormatNumber(Val(txtintamt.Text) / Val(txtIinstlamt.Text), 0)
            If Val(txtIinstlamt.Text) = 0 And Val(TxtIinstlNo.Text) > 0 Then
                txtIinstlamt.Text = RndOff((Val(txtintamt.Text) / Val(txtIinstlamt.Text)), 0, 0)
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & "I_Inst_Amt_No")
        End Try
    End Sub

    Private Sub BtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Try
            Dim dtlamast As New DataTable
            If Not IsValidate() Then Exit Sub

            sqlstr = "Select * from LaMast Where Emp_Code='" & Chk(TxtCode.Text) & "' AND L_Code='" & Chk(CmbLoanType.SelectedValue) & "' AND L_Date='" & CDate(DtpSanction.Text) & "'"
            Session("DalObj").GetSqldatatable(dtlamast, sqlstr)

            If dtlamast.Rows.Count > 0 Then

                sqlstr = "Update LaMast Set " & _
                        " I_AMT  = '" & ChkN(txtintamt.Text) & "'," & _
                        " I_INST_AMT  = '" & ChkN(txtIinstlamt.Text) & "'," & _
                        " I_INST_NO = '" & ChkN(TxtIinstlNo.Text) & "'," & _
                        " I_RATE  = '" & ChkN(TxtIntRate.Text) & "'," & _
                        " I_REC = '" & ChkN(TxtIRrecover.Text) & "'," & _
                        " Inst_Type = '" & ChkN(cmbinstmnttype.SelectedValue) & "'," & _
                        " L_AMT = '" & ChkN(txtloanamt.Text) & "'," & _
                        " L_INST_AMT  = '" & ChkN(txtLinstlamt.Text) & "'," & _
                        " L_INST_NO = '" & ChkN(TxtLinstlNo.Text) & "'," & _
                        " L_REC = '" & ChkN(TxtLRecover.Text) & "'," & _
                        " I_SDATE = " & IIf(ChkIRecFrom.Checked, "'" & DtpIRecFrom.DateValue & "', ", "Null, ") & _
                        " L_SDATE = " & IIf(ChkLRecFrom.Checked, "'" & DtpLRecFrom.DateValue & "'", "Null, ") & _
                        " Where Emp_Code = '" & Chk(TxtCode.Text) & "' AND L_Code='" & Chk(CmbLoanType.SelectedValue) & "' AND L_DATE= '" & Format(CDate(DtpSanction.Text), "dd/MMM/yyyy") & "'"

                '" L_DATE = '" & Format(CDate(DtpSanction.Text), "dd/MMM/yyyy") & "'," & _

                Session("DalObj").ExecuteCommand(sqlstr)
                SetMsg(LblErrMsg, "Record has Modified Successfully.")
                ClearAll(Me)
                DtpSanction.DateValue = Format(Date.Today, "dd/MMM/yyyy")
                CmbLoanType.SelectedIndex = 0
                cmbinstmnttype.SelectedIndex = 0
                LblName.Text = ""
                LblExpDate.Text = ""

            Else
                sqlstr = "Insert InTo LaMast(Emp_code, L_CODE, L_DATE, I_AMT, I_INST_AMT, I_INST_NO, I_RATE, I_REC, L_AMT, L_INST_AMT, L_INST_NO, L_REC, Inst_Type, I_SDATE, L_SDATE)"
                sqlstr &= "Values('"
                sqlstr &= Chk(TxtCode.Text) & "','"
                sqlstr &= Chk(CmbLoanType.SelectedValue) & "','"
                sqlstr &= Format(CDate(DtpSanction.Text), "dd/MMM/yyyy") & "','"
                sqlstr &= ChkN(txtintamt.Text) & "','"
                sqlstr &= ChkN(txtIinstlamt.Text) & "','"
                sqlstr &= ChkN(TxtIinstlNo.Text) & "','"
                sqlstr &= ChkN(TxtIntRate.Text) & "','"
                sqlstr &= ChkN(TxtIRrecover.Text) & "','"
                sqlstr &= ChkN(txtloanamt.Text) & "','"
                sqlstr &= ChkN(txtLinstlamt.Text) & "','"
                sqlstr &= ChkN(TxtLinstlNo.Text) & "','"
                sqlstr &= ChkN(TxtLRecover.Text) & "','"
                sqlstr &= ChkN(cmbinstmnttype.SelectedValue) & "',"
                sqlstr &= IIf(ChkIRecFrom.Checked, "'" & DtpIRecFrom.DateValue & "'", "Null") & ","
                sqlstr &= IIf(ChkLRecFrom.Checked, "'" & DtpLRecFrom.DateValue & "'", "Null") & ")"
                Session("DalObj").ExecuteCommand(sqlstr)
                SetMsg(LblErrMsg, "Record Saved Successfully.")
                ClearAll(Me)
                DtpSanction.DateValue = Format(Date.Today, "dd/MMM/yyyy")
                CmbLoanType.SelectedIndex = 0
                cmbinstmnttype.SelectedIndex = 0
                LblName.Text = ""
                LblExpDate.Text = ""
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & "BtnSave_Click")
        End Try
    End Sub

    Function IsValidate() As Boolean
        'If Chk(TxtCode.Text) = "" Then
        '    SetMsg(LblErrMsg, "Employee Code Can't be Left Blank.")
        '    Return False
        'End If

        'If CmbLoanType.SelectedValue = "" Then
        '    SetMsg(LblErrMsg, "Loan Type Can't be Left Blank.")
        '    Return False
        'End If
        'If cmbinstmnttype.SelectedValue = "" Then
        '    SetMsg(LblErrMsg, "Installment Type Can't be Left Blank.")
        '    Return False
        'End If
        Return True
    End Function

    Private Sub BtnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Private Sub BtnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        ClearAll(Me)
        DtpSanction.DateValue = Format(Date.Today, "dd/MMM/yyyy")
        CmbLoanType.SelectedValue = 0
        cmbinstmnttype.SelectedValue = 0
        LblName.Text = ""
    End Sub

    Private Sub txtintamt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtintamt.TextChanged
        Try
            If Val(txtintamt.Text) - Val(TxtIRrecover.Text) < 0 Then
                SetMsg(LblErrMsg, "Balance can't be less than zero")
            Else
                TxtIBalance.Text = Val(txtintamt.Text) - Val(TxtIRrecover.Text)
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " txtIinstlamt_TextChanged")
        End Try
    End Sub
    'Change By Arun
    Private Sub DtpSanction_DateChanged(ByVal source As System.Object, ByVal e As System.EventArgs)
        LblErrMsg.Text = ""
        If Val(txtloanamt.Text) > Val(txtLinstlamt.Text) Then
            If Val(txtLinstlamt.Text) > 0 Then
                L_Inst_Amt_No()
            End If
        Else
            SetMsg(LblErrMsg, "Installment Amount Can't be More Than Loan Amount.")
            txtLinstlamt.Text = 0
        End If
    End Sub

End Class
