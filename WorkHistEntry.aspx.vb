Partial Class WorkHistEntry
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
    Dim SQLStr As String
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Server.GetLastError Is Nothing Then SetMsg(LblErrMsg, "") 'By Ravi
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
            ' ElseIf Session("LoginUser").UserGroup <> "ADMIN" Then  'Comment by Ravi
            ' Response.Redirect("Error.htm")  'Comment by ravi
            Exit Sub
        End If

        If Not IsPostBack Then
            Try
                Session("BalObj").FillCombo(cmbBand, "GRD_CODE", "GRD_NAME", "GRDMAST", True)
                Session("BalObj").FillCombo(cmbDept, "DEPT_CODE", "DEPT_NAME", "DEPTMAST", True)
                Session("BalObj").FillCombo(cmbDivi, "DIVI_CODE", "DIVI_NAME", "DIVIMAST", True)
                Session("BalObj").FillCombo(cmbSect, "SECT_CODE", "SECT_NAME", "SECTMAST", True)
                Session("BalObj").FillCombo(cmbDesg, "DSG_CODE", "DSG_NAME", "DSGMAST", True)
                Session("BalObj").FillCombo(cmbResp, "PROC_CODE", "PROC_NAME", "PROCMAST", True)
                Session("BalObj").FillCombo(cmbCate, "TYPE_CODE", "TYPE_NAME", "EMPTYPE", True)
                Session("BalObj").FillCombo(cmbPayBuck, "PB_CODE", "PayBucket_Name", "PAYBUCKETMAST", True)
                Session("BalObj").FillCombo(cmbEmp, "Select EMP_CODE, Emp_Name as EMPName from HRDMASTQRY Where LTYPE = 1 " & Session("UserCodes") & " Order By FNAME", True)
                DtpEffective.Text = Format(Date.Today, "dd/MMM/yyyy")
                DtpHire.Text = Format(Date.Today, "dd/MMM/yyyy")
                dtpTo.Text = Format(Date.Today, "dd/MMM/yyyy")
                SetMsg(LblName, "")

                cmdSave.Attributes.Add("onclick", "return ValidateCtrl();")

            Catch ex As Exception
                SetMsg(LblErrMsg, ex.Message)
            End Try
        End If
    End Sub

    Private Sub cmbEmp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEmp.SelectedIndexChanged
        Try
            TxtCode.Text = cmbEmp.SelectedValue
            cmbEmp.Visible = False
            btnList.Visible = True
            TxtCode.Visible = True
            TxtCode_TextChanged(sender, e)
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message)
        End Try
    End Sub

    Private Sub TxtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCode.TextChanged
        Try
            LblErrMsg.Text = ""
            ViewState("Dt") = New DataTable
            'Session("Code") = Chk(TxtCode.Text)
            SQLStr = " Select * from HrdMastQry where Emp_Code='" & Chk(TxtCode.Text) & "' " & Session("UserCodes")
            Session("DalObj").GetSqlDataTable(ViewState("Dt"), SQLStr)
            ViewState("Value") = False
            If ViewState("Dt").Rows.Count <> 0 Then
                ViewState("Value") = True
                ChkCombo(cmbBand, ViewState("Dt").Rows(0).Item("GRD_CODE"))
                ChkCombo(cmbDivi, ViewState("Dt").Rows(0).Item("DIVI_CODE"))
                ChkCombo(cmbDesg, ViewState("Dt").Rows(0).Item("DSG_CODE"))
                ChkCombo(cmbDept, ViewState("Dt").Rows(0).Item("DEPT_CODE"))
                ChkCombo(cmbCate, ViewState("Dt").Rows(0).Item("TYPE_CODE"))
                ChkCombo(cmbSect, ViewState("Dt").Rows(0).Item("SECT_CODE"))
                ChkCombo(cmbResp, ViewState("Dt").Rows(0).Item("PROC_CODE"))
                ChkCombo(cmbPayBuck, ViewState("Dt").Rows(0).Item("PB_CODE"))
                SetMsg(LblName, Chk(ViewState("Dt").Rows(0).Item("FNAME")) & " " & Chk(ViewState("Dt").Rows(0).Item("LNAME")))
                If Not IsDBNull(ViewState("Dt").Rows(0).Item("DOJ")) Then
                    DtpHire.Text = Format(CDate(ViewState("Dt").Rows(0).Item("DOJ")), "dd/MMM/yyyy")
                End If
                If Not IsDBNull(ViewState("Dt").Rows(0).Item("LAST_APPR")) Then
                    DtpEffective.Text = Format(DateAdd(DateInterval.Day, 1, CDate(ViewState("Dt").Rows(0).Item("LAST_APPR"))), "dd/MMM/yyyy")
                End If
            Else
                ViewState("Value") = False
                SetMsg(LblName, "")
                cmbBand.SelectedValue = ""
                cmbDivi.SelectedValue = ""
                cmbDesg.SelectedValue = ""
                cmbDept.SelectedValue = ""
                cmbCate.SelectedValue = ""
                cmbSect.SelectedValue = ""
                cmbResp.SelectedValue = ""
                cmbPayBuck.SelectedValue = ""
                DtpHire.Text = Format(Date.Today, "dd/MMM/yyyy")
                DtpEffective.Text = Format(Date.Today, "dd/MMM/yyyy")
                Exit Sub
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message)
        End Try

    End Sub

    Private Sub btnList_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnList.Click
        TxtCode.Visible = False
        cmbEmp.Visible = True
        btnList.Visible = False
    End Sub

    Private Sub cmnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnClose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            If ViewState("Value") = False Then Exit Sub
            If Not IsValidate() Then Exit Sub

            SQLStr = " Insert WorkHistory (Emp_Code, FinYear, Grd_Name, AnnualSal, BandPerc, Dsg_Name, Dept_Name, Divi_Name, Sect_Name, Loc_Name, Comp_Name, Type_Name, Proc_Name, EffectiveDate, HireDate, ToDate, TotalMonths, Performance, Per_Annual, PerEquity, PerPromotion, PerOthers, Remarks, PerBR, PerPR, PerCI, PerIO, EmpStatus ) Values ('" & _
                       Chk(TxtCode.Text) & "', '" & _
                       Session("FINYEAR") & "' , '" & _
                       Chk(cmbBand.SelectedItem.Text) & "', '" & _
                       ChkN(TxtASalary.Text) & "', '" & _
                       ChkN(TxtPBand.Text) & "', '" & _
                       Chk(cmbDesg.SelectedItem.Text) & "', '" & _
                       Chk(cmbDept.SelectedItem.Text) & "', '" & _
                       Chk(cmbDivi.SelectedItem.Text) & "', '" & _
                       Chk(cmbSect.SelectedItem.Text) & "', '" & _
                       Chk(ViewState("Dt").Rows(0).Item("LOC_NAME")) & "', '" & _
                       Chk(ViewState("Dt").Rows(0).Item("COMP_NAME")) & "', '" & _
                       Chk(cmbCate.SelectedItem.Text) & "', '" & _
                       Chk(cmbResp.SelectedItem.Text) & "', '" & _
                       Format(CDate(DtpEffective.Text), "dd-MMM-yyyy") & "', '" & _
                       Format(CDate(DtpHire.Text), "dd-MMM-yyyy") & "', '" & _
                       Format(CDate(dtpTo.Text), "dd-MMM-yyyy") & "', '" & _
                       ChkN(TxtMonths.Value) & "', '" & _
                       Chk(TxtPerformance.Text) & "', '" & _
                       ChkN(TxtPAnnual.Text) & "', '" & _
                       ChkN(TxtPEquity.Text) & "', '" _
                       & ChkN(TxtPPromo.Text) & "', '" & _
                       ChkN(TxtPBand.Text) & "', '" & _
                       Chk(TxtRemarks.Text) & "', '" & _
                       ChkN(TxtBr.Text) & "', '" & _
                       ChkN(TxtPr.Text) & "', '" & _
                       ChkN(TxtCI.Text) & "', '" & _
                       ChkN(TxtIO.Text) & "', '" & _
                       Chk(cmbStatus.SelectedItem.Text) & "')"

            Session("DalObj").ExecuteCommand(SQLStr)
            SetMsg(LblErrMsg, "Employee Record Saved Successfully.")
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message)
        End Try
    End Sub


    Private Function IsValidate() As Boolean
        Try

            If Chk(TxtCode.Text) = "" Then
                SetMsg(LblErrMsg, "Please select employee code from the list.")
                Return False
            End If

            If (Not IsNumeric(TxtASalary.Text)) And TxtASalary.Text <> "" Then
                SetMsg(LblErrMsg, "Annual Salary must be numeric type.")
                Return False
            End If

            If (Not IsNumeric(TxtPAnnual.Text)) And TxtPAnnual.Text <> "" Then
                SetMsg(LblErrMsg, "Annual Percentage must be numeric type.")
                Return False
            End If

            If (Not IsNumeric(TxtPEquity.Text)) And TxtPEquity.Text <> "" Then
                SetMsg(LblErrMsg, "Equity Percentage must be numeric type.")
                Return False
            End If

            If (Not IsNumeric(TxtPBand.Text)) And TxtPBand.Text <> "" Then
                SetMsg(LblErrMsg, "Band Percentage must be numeric type.")
                Return False
            End If

            If (Not IsNumeric(TxtPMerit.Text)) And TxtPMerit.Text <> "" Then
                SetMsg(LblErrMsg, "Merit Percentage must be numeric type.")
                Return False
            End If

            If (Not IsNumeric(TxtPPromo.Text)) And TxtPPromo.Text <> "" Then
                SetMsg(LblErrMsg, "Promotion Percentage must be numeric type.")
                Return False
            End If
            Return True
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message)
        End Try
    End Function

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub

End Class
