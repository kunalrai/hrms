Imports CrystalDecisions.Web
Partial Class FrmReports
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim BAlObj As BAL.BLayer
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents cmbGraphType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Textbox1 As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    'Dim RV As ReportView
    Dim PayDate As Date
    Dim ParArray() As String
    Dim IsPrint As Boolean = False
    Dim SRNO As Integer = 0
    Protected RptHead As String

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        BAlObj = Session("BalObj")
        Dim strQString As String
        'Response.Write(Session("UserCodesRep"))
        Try
            If Server.GetLastError Is Nothing Then SetMsg(LblMsg, "")
            If IsNothing(Session("LoginUser")) Then
                Response.Redirect("CompSel.aspx")
            End If
            If Not IsPostBack Then
                dtpAsOn.DateValue = Date.Today
                dtpFrom.DateValue = Date.Today
                dtpTo.DateValue = Date.Today
                cmbMonth.SelectedValue = Date.Today.Month
                CmdFor.Enabled = False
                'cmbYear.SelectedValue = Date.Today.Year

                TblMonthRange.Visible = False
                TblMonth.Visible = False
                TblDate.Visible = False
                TblDateRange.Visible = False
                TblLType.Visible = False

                Dim StrRpt, StrQry As String

                StrQry = " Select isnull(Reports,'') From WebUsers Where UserId In ( Select Group_Id From WebUsers Where UserId = '" & Encrypt(Session("LoginUser").UserId, "+") & "')"

                StrRpt = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(StrQry, , DAL.DataLayer.ExecutionType.ExecuteScalar)

                strQString = Request.QueryString.Item("RepType")
                If StrRpt <> "" Then
                    StrRpt = Replace(StrRpt, "|", ",")
                    BAlObj.FillCombo(LstReports, " Select SrNo,RptName from MstReports Where RptType = '" & strQString & "' and SrNo in ( " & StrRpt & " ) and Active = 1")
                Else
                    BAlObj.FillCombo(LstReports, " Select SrNo,RptName from MstReports Where RptType = '" & strQString & "' and Active = 1")
                End If

                If LstReports.Items.Count > 0 Then
                    LstReports.SelectedIndex = 0
                    LstReports_SelectedIndexChanged(Nothing, Nothing)
                End If
                If Session("LoginUser").UserGroup = "USER" Then
                    cmdProp.Visible = False
                    TblLType.Visible = False
                End If

                Session("BalObj").FillCombo(cmbFor, "Select Field_Code,Field_Desc from FieldFor")

                'By Ravi on 29 dec 2006
                '----For Showing Report name in heading --------------------------
                Select Case Request.QueryString.Item("RepType").ToString
                    Case "LSTHR"
                        RptHead = "Employee Listing Reports"
                    Case "GRAPH"
                        RptHead = "Graph Charts Reports"
                    Case "LETTER"
                        RptHead = "Letters "
                    Case "RIM"
                        RptHead = "Reimbursement Reports "
                    Case Else
                        RptHead = Request.QueryString.Item("RepType").ToString & " Reports"
                End Select
                '------------------------------------------------

            End If
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (Page_Load) ")
        End Try

    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Private Sub LstReports_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LstReports.SelectedIndexChanged
        Dim dtRpt As New DataTable
        Try

            If IsNothing(LstReports.SelectedItem) Then
                SetMsg(LblMsg, "Select a Report Name to Continue.")
                Exit Sub
            End If
            If LstReports.SelectedItem.Text = "" Then
                SetMsg(LblMsg, "Select a Report Name to Continue.")
                Exit Sub
            End If
            TxtProp.Text = Chk(LstReports.SelectedValue)
            Session("DalObj").GetSqlDataTable(dtRpt, "Select * From MstReports Where SrNo = " & Chk(LstReports.SelectedValue))
            If dtRpt.Rows.Count > 0 Then

                optCrystal.Enabled = True
                OptHtml.Enabled = True
                If Chk(dtRpt.Rows(0).Item("RptFile")) = "" Then
                    optCrystal.Enabled = False
                    optCrystal.Checked = False
                    OptHtml.Checked = True
                End If

                If Chk(dtRpt.Rows(0).Item("RptFile")) <> "" And Chk(dtRpt.Rows(0).Item("RptFile")) <> "" Then
                    OptHtml.Enabled = False
                    OptHtml.Checked = False
                    optCrystal.Checked = True
                End If

                If Chk(dtRpt.Rows(0).Item("RptQuery")) = "" Then
                    OptHtml.Enabled = False
                    OptHtml.Checked = False
                    optCrystal.Checked = True
                End If

                'Response.Write(HttpContext.Current.Request.ServerVariables("APPL_PHYSICAL_PATH") & Session("LoginUser").ReportFolder & "\" & dtRpt.Rows(0).Item("RptFile") & ".rpt")
                'SetMsg(LblMsg, HttpContext.Current.Request.ServerVariables("APPL_PHYSICAL_PATH") & Session("LoginUser").ReportFolder & "\" & dtRpt.Rows(0).Item("RptFile") & ".rpt")

                ' ************* Hidinging/Showing Date Filter on the Basis of RepFor Field

                TblDate.Visible = False
                TblDateRange.Visible = False
                TblMonth.Visible = False
                TblMonthRange.Visible = False
                TblLType.Visible = True
                If Session("LoginUser").UserGroup = "USER" Then
                    TBLFOR.Visible = False
                    TblOrderBy.Visible = False
                    TblLType.Visible = False
                    If InStr(Chk(dtRpt.Rows(0).Item("RptFor")), "{@FDATE}") > 0 Then
                        TblDateRange.Visible = True
                    Else
                        TblMonth.Visible = True
                    End If
                Else
                    If InStr(Chk(dtRpt.Rows(0).Item("RptFor")), "{@DATE}") > 0 Then
                        TblDate.Visible = True
                    ElseIf InStr(Chk(dtRpt.Rows(0).Item("RptFor")), "{@FDATE}") > 0 Then
                        TblDateRange.Visible = True
                    ElseIf InStr(Chk(dtRpt.Rows(0).Item("RptFor")), "{@MONTH}") > 0 Then
                        TblMonth.Visible = True
                    ElseIf InStr(Chk(dtRpt.Rows(0).Item("RptFor")), "{@FMONTH}") > 0 Then
                        TblMonthRange.Visible = True
                    End If
                End If



                ' ************* Setting Up Order By Field on the Basis of RptOrders Field
                GetGroups(Chk(dtRpt.Rows(0).Item("RptOrders")))

                dtpAsOn.DateValue = Date.Today
                dtpFrom.DateValue = Date.Today
                dtpTo.DateValue = Date.Today

            End If
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (LstReports_SelectedIndexChanged)")
        Finally
            dtRpt.Dispose()
        End Try
    End Sub

    Private Sub cmdSetValues_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSetValues.Click
        '' ***************  Calling This Event Because Event of View Button is Called in Scripting Side.
        Dim RepVars As ReportVars
        'RV = New ReportView
        Dim Str As String
        Dim dtRpt As New DataTable
        Try
            Session("RepVars") = Nothing
            If IsNothing(LstReports.SelectedItem) Then
                SetMsg(LblMsg, "Select a Report Name to Continue.")
                Exit Sub
            End If
            If LstReports.SelectedItem.Text = "" Then
                SetMsg(LblMsg, "Select a Report Name to Continue.")
                Exit Sub
            End If


            Session("DalObj").GetSqlDataTable(dtRpt, "Select * From MstReports Where SrNo = " & LstReports.SelectedValue)

            If optCrystal.Checked Then
                ' BY RAVI
                TxtCriteria.Text = ""
                '-------------------

                'RV = New ReportView
                RepVars.SelectionFormula = ""
                RepVars.strFormula = ""
                'strFormula()
                Dim RptName = Chk(dtRpt.Rows(0).Item("RptFile"))
                Dim txtCond As String = Chk(dtRpt.Rows(0).Item("RptFor"))


                If RptName = "" Then
                    SetMsg(LblMsg, "Report File Not Set.")
                    Exit Sub
                End If

                If Dir(HttpContext.Current.Request.ServerVariables("APPL_PHYSICAL_PATH") & Session("LoginUser").ReportFolder & "\" & RptName & ".rpt") = "" Then
                    SetMsg(LblMsg, "Report File [" & HttpContext.Current.Request.ServerVariables("APPL_PHYSICAL_PATH") & Session("LoginUser").ReportFolder & "\" & RptName & ".rpt" & "] Not Found .")
                    Exit Sub
                End If

                RepVars.ReportFileName = HttpContext.Current.Request.ServerVariables("APPL_PHYSICAL_PATH") & Session("LoginUser").ReportFolder & "\" & RptName & ".rpt"
                'RepVars.ReportFileName = Session("LoginUser").ReportFolder & "\" & RptName & ".rpt"
                'Response.Write(RepVars.ReportFileName)
                'SetMsg(LblMsg, RepVars.ReportFileName)

                If txtCond <> "" Then
                    txtCond = Replace(txtCond, "@FY_START", "DATE(" & Format(FY_Start, "yyyy,MM,dd") & ")")
                    txtCond = Replace(txtCond, "@FY_END", "DATE(" & Format(FY_End, "yyyy,MM,dd") & ")")
                    txtCond = Replace(txtCond, "@RY_START", "DATE(" & Format(RY_Start, "yyyy,MM,dd") & ")")
                    txtCond = Replace(txtCond, "@RY_END", "DATE(" & Format(RY_End, "yyyy,MM,dd") & ")")
                    txtCond = Replace(txtCond, "@LY_START", "DATE(" & Format(LY_Start, "yyyy,MM,dd") & ")")
                    txtCond = Replace(txtCond, "@LY_END", "DATE(" & Format(LY_End, "yyyy,MM,dd") & ")")
                End If

                If Trim(TxtCriteria.Text) <> "" Then
                    txtCond = txtCond & " AND " & Trim(TxtCriteria.Text)
                End If

                If Chk(dtRpt.Rows(0).Item("QryType")) <> "" Then
                    txtCond = txtCond & " AND {" & IIf(Chk(dtRpt.Rows(0).Item("QryType")) = "M", "HRDMASTQRY", "HRDHISTQRY") & ".Comp_Code}='" & Session("LoginUser").CurrentCompID & "'"
                    If cmbFor.SelectedValue.ToUpper <> "ALL" Then
                        txtCond = txtCond & " AND {" & IIf(Chk(dtRpt.Rows(0).Item("QryType")) = "M", "HRDMASTQRY", "HRDHISTQRY") & "." & cmbFor.SelectedValue & "}='" & Chk(cmbForVal.SelectedValue) & "'"
                    End If

                    If cmbLTYPE.SelectedValue <> 0 Then
                        txtCond = txtCond & " AND {" & IIf(Chk(dtRpt.Rows(0).Item("QryType")) = "M", "HRDMASTQRY", "HRDHISTQRY") & ".LTYPE}=" & Chk(cmbLTYPE.SelectedValue)
                    End If

                    txtCond = txtCond & " " & Replace(Session("UserCodesRep"), "|Q|", IIf(Chk(dtRpt.Rows(0).Item("QryType")) = "M", "HRDMASTQRY", "HRDHISTQRY"))

                    If Session("LoginUser").UserGroup = "USER" Then
                        If Chk(dtRpt.Rows(0).Item("QryType")) = "M" Then
                            txtCond = txtCond & " AND {HRDMASTQRY.EMP_CODE} = '" & Session("LoginUser").UserId & "'"
                        Else
                            txtCond = txtCond & " AND {HRDHISTQRY.EMP_CODE} = '" & Session("LoginUser").UserId & "'"
                        End If
                    End If

                    RepVars.SelectionFormula = txtCond

                    If InStr(Chk(dtRpt.Rows(0).Item("RptFor")), "{@DATE}") > 0 Then
                        RepVars.strFormula = "DATE^" & "DATE(" & Format(dtpAsOn.DateValue, "yyyy,MM,dd") & ")"
                    ElseIf InStr(Chk(dtRpt.Rows(0).Item("RptFor")), "{@FDATE}") > 0 Then
                        RepVars.strFormula = "FDATE^" & "DATE(" & Format(dtpFrom.DateValue, "yyyy,MM,dd") & ")|" & "TDATE^" & "DATE(" & Format(dtpTo.DateValue, "yyyy,MM,dd") & ")"
                    ElseIf InStr(Chk(dtRpt.Rows(0).Item("RptFor")), "{@MONTH}") > 0 Then
                        RepVars.strFormula = "MONTH^" & "Date(" & Format(DateAdd(DateInterval.Day, -Day(CDate("01/" & MonthName(cmbMonth.SelectedValue) & "/" & cmbYear.SelectedItem.Text)), DateAdd(DateInterval.Month, 1, CDate("01/" & MonthName(cmbMonth.SelectedValue) & "/" & cmbYear.SelectedItem.Text))), "yyyy,MM,dd") & ")"
                    ElseIf InStr(Chk(dtRpt.Rows(0).Item("RptFor")), "{@FMONTH}") > 0 Then
                        RepVars.strFormula = "FMONTH^" & "Date(" & Format(DateAdd(DateInterval.Day, -Day(CDate("01/" & MonthName(cmbMonthFrom.SelectedValue) & "/" & cmbYearFrom.SelectedItem.Text)), DateAdd(DateInterval.Month, 1, CDate("01/" & MonthName(cmbMonthFrom.SelectedValue) & "/" & cmbYearFrom.SelectedItem.Text))), "yyyy,MM,dd") & ")"
                        RepVars.strFormula = RepVars.strFormula & " | " & "TMONTH^" & "Date(" & Format(DateAdd(DateInterval.Day, -Day(CDate("01/" & MonthName(cmbMonthTo.SelectedValue) & "/" & cmbYearTo.SelectedItem.Text)), DateAdd(DateInterval.Month, 1, CDate("01/" & MonthName(cmbMonthTo.SelectedValue) & "/" & cmbYearTo.SelectedItem.Text))), "yyyy,MM,dd") & ")"
                    End If
                    PayDate = CDate(Format(DateAdd(DateInterval.Day, -Day(CDate("01/" & MonthName(cmbMonth.SelectedValue) & "/" & cmbYear.SelectedItem.Text)), DateAdd(DateInterval.Month, 1, CDate("01/" & MonthName(cmbMonth.SelectedValue) & "/" & cmbYear.SelectedItem.Text))), "yyyy,MM,dd"))
                Else
                    RepVars.SelectionFormula = txtCond
                    If InStr(Chk(dtRpt.Rows(0).Item("RptFor")), "{@DATE}") > 0 Then
                        RepVars.strFormula = "DATE^" & "DATE(" & Format(dtpAsOn.DateValue, "yyyy,MM,dd") & ")"
                    ElseIf InStr(Chk(dtRpt.Rows(0).Item("RptFor")), "{@FDATE}") > 0 Then
                        RepVars.strFormula = "FDATE^" & "DATE(" & Format(dtpFrom.DateValue, "yyyy,MM,dd") & ")|" & "TDATE^" & "DATE(" & Format(dtpTo.DateValue, "yyyy,MM,dd") & ")"
                    ElseIf InStr(Chk(dtRpt.Rows(0).Item("RptFor")), "{@MONTH}") > 0 Then
                        RepVars.strFormula = "MONTH^" & "Date(" & Format(DateAdd(DateInterval.Day, -Day(CDate("01/" & MonthName(cmbMonth.SelectedValue) & "/" & cmbYear.SelectedItem.Text)), DateAdd(DateInterval.Month, 1, CDate("01/" & MonthName(cmbMonth.SelectedValue) & "/" & cmbYear.SelectedItem.Text))), "yyyy,MM,dd") & ")"
                    ElseIf InStr(Chk(dtRpt.Rows(0).Item("RptFor")), "{@FMONTH}") > 0 Then
                        RepVars.strFormula = "FMONTH^" & "Date(" & Format(DateAdd(DateInterval.Day, -Day(CDate("01/" & MonthName(cmbMonthFrom.SelectedValue) & "/" & cmbYearFrom.SelectedItem.Text)), DateAdd(DateInterval.Month, 1, CDate("01/" & MonthName(cmbMonthFrom.SelectedValue) & "/" & cmbYearFrom.SelectedItem.Text))), "yyyy,MM,dd") & ")"
                        RepVars.strFormula = RepVars.strFormula & " |" & "TMONTH^" & "Date(" & Format(DateAdd(DateInterval.Day, -Day(CDate("01/" & MonthName(cmbMonthTo.SelectedValue) & "/" & cmbYearTo.SelectedItem.Text)), DateAdd(DateInterval.Month, 1, CDate("01/" & MonthName(cmbMonthTo.SelectedValue) & "/" & cmbYearTo.SelectedItem.Text))), "yyyy,MM,dd") & ")"
                    End If
                End If

                If Chk(dtRpt.Rows(0).Item("RptQuery")) <> "" Then
                    ReportFields(Chk(dtRpt.Rows(0).Item("RptQuery")), 1, Chk(dtRpt.Rows(0).Item("QryType")))
                    RepVars.Param = ParArray
                    RepVars.IsParam = True
                Else
                    RepVars.IsParam = False
                End If

                '***************** Passing Parameters For Groups********************8
                If cmbOrdBy.Items.Count > 0 Then
                    'by Ravi 
                    Dim order() As String
                    Dim orderqry As String
                    Dim I As Int16
                    ' order = Split(cmbOrdBy.SelectedValue, "+")
                    'For I = 0 To order.Length - 1
                    '    orderqry = orderqry & " {" & IIf(Chk(dtRpt.Rows(0).Item("QryType")) = "M", "HRDMASTQRY", "HRDHISTQRY") & "." & order(I) & "}"
                    'Next
                    RepVars.OrderBy = "{HRDHISTQRY.EMP_NAME}"
                    '----------------------
                    ' RepVars.OrderBy = cmbOrdBy.SelectedValue 'comment by Ravi
                    RepVars.totNumGrp = ChkN(dtRpt.Rows(0).Item("Groups"))
                    RepVars.QryType = Chk(dtRpt.Rows(0).Item("QryType"))
                End If

                ReportType.Text = "ReportView.aspx"


            Else
                '*************************************For HTML reporting ***************************************
                '                               WELCOME IN HTML REPORT CODING


                '********************************************************************************************


                Dim StrQ, StrR As String
                StrQ = Chk(dtRpt.Rows(0).Item("RptQuery"))
                TxtCriteria.Text = ""

                '================================== TO REPLACE THE SPECIAL CHARACTER FROM SQLQUERY===============


                If InStr(Chk(dtRpt.Rows(0).Item("RptFor")), "{@DATE}") > 0 Then
                    StrQ = Replace(StrQ, "{@DATE}", "'" & Format(dtpAsOn.DateValue, "dd/MMM/yyyy") & "'")
                ElseIf InStr(Chk(dtRpt.Rows(0).Item("RptFor")), "{@FDATE}") > 0 Then
                    StrQ = Replace(StrQ, "{@FDATE}", "'" & Format(dtpFrom.DateValue, "dd/MMM/yyyy") & "'")
                    StrQ = Replace(StrQ, "{@TDATE}", "'" & Format(dtpTo.DateValue, "dd/MMM/yyyy") & "'")
                ElseIf InStr(Chk(dtRpt.Rows(0).Item("RptFor")), "{@MONTH}") > 0 Then
                    StrQ = Replace(StrQ, "{@MONTH}", "'" & Format(DateAdd(DateInterval.Day, -Day(CDate("01/" & MonthName(cmbMonth.SelectedValue) & "/" & cmbYear.SelectedItem.Text)), DateAdd(DateInterval.Month, 1, CDate("01/" & MonthName(cmbMonth.SelectedValue) & "/" & cmbYear.SelectedItem.Text))), "dd/MMM/yyyy") & "'")
                End If



                '=========================================  ==END HERE================================

                If Chk(dtRpt.Rows(0).Item("RptFor")) <> "" Then
                    StrR = Chk(dtRpt.Rows(0).Item("RptFor"))
                    StrR = Replace(StrR, "@FY_START", "'" & Format(FY_Start, "dd/MMM/yyyy") & "'")
                    StrR = Replace(StrR, "@FY_END", "'" & Format(FY_End, "dd/MMM/yyyy") & "'")
                    StrR = Replace(StrR, "@RY_START", "'" & Format(RY_Start, "dd/MMM/yyyy") & "'")
                    StrR = Replace(StrR, "@RY_END", "'" & Format(RY_End, "dd/MMM/yyyy") & "'")
                    StrR = Replace(StrR, "@LY_START", "'" & Format(LY_Start, "dd/MMM/yyyy") & "'")
                    StrR = Replace(StrR, "@LY_END", "'" & Format(LY_End, "dd/MMM/yyyy") & "'")

                    If InStr(Chk(dtRpt.Rows(0).Item("RptFor")), "{@DATE}") > 0 Then
                        StrR = Replace(StrR, "{@DATE}", "'" & Format(dtpAsOn.DateValue, "dd/MMM/yyyy") & "'")
                    ElseIf InStr(Chk(dtRpt.Rows(0).Item("RptFor")), "{@FDATE}") > 0 Then
                        StrR = Replace(StrR, "{@FDATE}", "'" & Format(dtpFrom.DateValue, "dd/MMM/yyyy") & "'")
                        StrR = Replace(StrR, "{@TDATE}", "'" & Format(dtpTo.DateValue, "dd/MMM/yyyy") & "'")
                    ElseIf InStr(Chk(dtRpt.Rows(0).Item("RptFor")), "{@MONTH}") > 0 Then
                        StrR = Replace(StrR, "{@MONTH}", "'" & Format(DateAdd(DateInterval.Day, -Day(CDate("01/" & MonthName(cmbMonth.SelectedValue) & "/" & cmbYear.SelectedItem.Text)), DateAdd(DateInterval.Month, 1, CDate("01/" & MonthName(cmbMonth.SelectedValue) & "/" & cmbYear.SelectedItem.Text))), "dd/MMM/yyyy") & "'")
                    End If
                End If

                Dim StrCond As String

                If StrR <> "" Then
                    StrCond = " Where " & StrR
                    'StrQ = StrQ & " Where " & StrR
                Else

                End If


                If cmbFor.SelectedValue.ToUpper <> "ALL" Then
                    If InStr(UCase(StrCond), "WHERE") <> 0 Then
                        '===================================Rajeev Code ==================================
                        If ViewState("For") = "EMP_CODE" Then
                            If TxtFroVal.Text = "" Then
                                StrCond = StrCond & " AND " & IIf(Chk(dtRpt.Rows(0).Item("QryType")) = "M", "HRD", "HRDHISTQRY") & "." & cmbFor.SelectedValue & "='" & Chk(cmbForVal.SelectedValue) & "'"
                            Else
                                Dim Cnt As Int16
                                Cnt = Session("DalObj").ExecuteCommand("Select Count(*) From Hrdmast Where Emp_Code='" & Chk(TxtFroVal.Text) & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
                                If Cnt <> 1 Then
                                    SetMsg(LblMsg, "This Employee Code Does Not Exist.")
                                    Exit Sub
                                End If
                                StrCond = StrCond & " AND " & IIf(Chk(dtRpt.Rows(0).Item("QryType")) = "M", "HRD", "HRDHISTQRY") & "." & cmbFor.SelectedValue & "='" & Chk(TxtFroVal.Text) & "'"
                            End If
                        Else
                            StrCond = StrCond & " AND " & IIf(Chk(dtRpt.Rows(0).Item("QryType")) = "M", "HRD", "HRDHISTQRY") & "." & cmbFor.SelectedValue & "='" & Chk(cmbForVal.SelectedValue) & "'"
                        End If

                        '=========================================Code Ends Here ========================================
                        'StrCond = StrCond & " AND " & IIf(Chk(dtRpt.Rows(0).Item("QryType")) = "M", "HRDMASTQRY", "HRDHISTQRY") & "." & cmbFor.SelectedValue & "='" & Chk(cmbForVal.SelectedValue) & "'"
                    Else
                        StrCond = StrCond & " WHERE " & IIf(Chk(dtRpt.Rows(0).Item("QryType")) = "M", "HRD", "HRDHISTQRY") & "." & cmbFor.SelectedValue & "='" & Chk(cmbForVal.SelectedValue) & "'"
                    End If
                End If

                If TxtCriteria.Text <> "" Then
                    If InStr(UCase(StrCond), "WHERE") <> 0 Then
                        StrCond = StrCond & " AND " & TxtCriteria.Text.Trim
                    Else
                        StrCond = StrCond & " Where " & TxtCriteria.Text.Trim
                    End If
                End If

                If cmbLTYPE.SelectedValue <> 0 Then
                    If InStr(UCase(StrCond), "WHERE") <> 0 Then
                        StrCond = StrCond & " AND " & IIf(Chk(dtRpt.Rows(0).Item("QryType")) = "M", "HRD", "HRDHISTQRY") & ".LTYPE=" & Chk(cmbLTYPE.SelectedValue)
                    Else
                        StrCond = StrCond & " Where " & IIf(Chk(dtRpt.Rows(0).Item("QryType")) = "M", "HRD", "HRDHISTQRY") & ".LTYPE=" & Chk(cmbLTYPE.SelectedValue)
                    End If
                End If


                StrQ = Replace(StrQ, "[Where]", StrCond)
                ' StrQ = StrQ & " " & StrCond
                '  TextBox2.Text = StrQ
                ReportType.Text = "frmHTMLReports.aspx"
                frmHTMLReports.argStrSql = StrQ
            End If
            RepVars.ExportOpts = CmbExport.SelectedItem.Value
            Session("RepVars") = RepVars
            'oldcode before 07-04-2006
            'If cmbFor.SelectedValue.ToUpper <> "ALL" Then
            '    If InStr(UCase(StrQ), "WHERE") <> 0 Then
            '        StrQ = StrQ & " AND " & IIf(Chk(dtRpt.Rows(0).Item("QryType")) = "M", "HRDMASTQRY", "HRDHISTQRY") & "." & cmbFor.SelectedValue & "='" & Chk(cmbForVal.SelectedValue) & "'"
            '    Else
            '        StrQ = StrQ & " WHERE " & IIf(Chk(dtRpt.Rows(0).Item("QryType")) = "M", "HRDMASTQRY", "HRDHISTQRY") & "." & cmbFor.SelectedValue & "='" & Chk(cmbForVal.SelectedValue) & "'"
            '    End If
            'End If

            'If TxtCriteria.Text <> "" Then
            '    If InStr(UCase(StrQ), "WHERE") <> 0 Then
            '        StrQ = StrQ & " AND " & TxtCriteria.Text.Trim
            '    Else
            '        StrQ = StrQ & " Where " & TxtCriteria.Text.Trim
            '    End If
            'End If

            'If cmbLTYPE.SelectedValue <> 0 Then
            '    If InStr(UCase(StrQ), "WHERE") <> 0 Then
            '        StrQ = StrQ & " AND " & IIf(Chk(dtRpt.Rows(0).Item("QryType")) = "M", "HRDMASTQRY", "HRDHISTQRY") & ".LTYPE=" & Chk(cmbLTYPE.SelectedValue)
            '    Else
            '        StrQ = StrQ & " Where " & IIf(Chk(dtRpt.Rows(0).Item("QryType")) = "M", "HRDMASTQRY", "HRDHISTQRY") & ".LTYPE=" & Chk(cmbLTYPE.SelectedValue)
            '    End If
            'End If

            'ReportType.Text = "frmHTMLReports.aspx"
            'frmHTMLReports.argStrSql = StrQ
            'End If
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (cmdSetValues_Click)")
        Finally
            dtRpt.Dispose()
        End Try
    End Sub

    Private Sub cmbFor_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFor.SelectedIndexChanged
        cmbForVal.Items.Clear()
        Dim dtFlds As New DataTable
        Dim dtCmb As New DataTable
        'Response.Write(cmbFor.SelectedValue)
        If Trim(cmbFor.SelectedValue).ToUpper = "ALL" Then Exit Sub
        TxtFroVal.Text = ""
        Try
            strSql = "SELECT * FROM FieldFor WHERE Field_Code = '" & Trim(cmbFor.SelectedValue) & "'"
            Session("DalObj").GetSqlDataTable(dtFlds, strSql)
            'Response.Write(strSql)
            If dtFlds.Rows.Count > 0 Then
                If dtFlds.Rows(0).Item("F_Type") = "F" Then
                    Dim StrQ As String
                    If UCase(dtFlds.Rows(0).Item("Field_Code")) = "EMP_CODE" Then
                        '=================================Code By Rajeev =================================
                        TxtFroVal.Visible = True
                        cmbForVal.Visible = False
                        CmdFor.Enabled = True
                        ViewState("For") = "EMP_CODE"
                        'StrQ = " Select " & dtFlds.Rows(0).Item("Field_Code") & ",   " & dtFlds.Rows(0).Item("Field_Code") & "  + ' -' +  " & dtFlds.Rows(0).Item("Field_Name") & " as EMP_NAME From " & dtFlds.Rows(0).Item("Field_Table") & " Where 1=1 " & Session("UserCodes") & " Order By " & dtFlds.Rows(0).Item("Field_Name")
                        StrQ = " Select " & dtFlds.Rows(0).Item("Field_Code") & ",   " & dtFlds.Rows(0).Item("Field_Code") & "  + ' -' +  " & dtFlds.Rows(0).Item("Field_Name") & " as EMP_NAME From " & dtFlds.Rows(0).Item("Field_Table") & " Where 1=1 " & Session("UserCodes") & " Order By Emp_Name"
                        BAlObj.FillCombo(cmbForVal, StrQ, False)
                        '=================================Code By Rajeev Ends Here ======================
                    Else
                        TxtFroVal.Visible = False
                        cmbForVal.Visible = True
                        CmdFor.Enabled = False
                        ViewState("For") = "NULL"
                        StrQ = " Select  " & dtFlds.Rows(0).Item("Field_Code") & ", " & dtFlds.Rows(0).Item("Field_Name") & " From " & dtFlds.Rows(0).Item("Field_Table") & " Order By " & dtFlds.Rows(0).Item("Field_Name")
                        BAlObj.FillCombo(cmbForVal, StrQ, False)
                    End If

                Else
                    Session("BalObj").FillCombo(cmbForVal, dtFlds.Rows(0).Item("SqlQry"))
                End If
            End If
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (cmbFor_SelectedIndexChanged)")
        Finally
            dtCmb.Dispose()
        End Try
    End Sub
    Private Sub GetGroups(ByVal strOrders As String)
        If Trim(strOrders) = "" Then cmbOrdBy.Items.Clear() : Exit Sub
        Dim dtPaySetup As DataTable
        Try
            Dim OrdArr() As String, FldsArr() As String, i As Integer, j As Integer
            Dim strOrd As String
            OrdArr = Split(strOrders, ";")
            cmbOrdBy.Items.Clear()
            For i = 0 To UBound(OrdArr)
                FldsArr = Split(OrdArr(i), "+")
                strOrd = ""
                For j = 0 To UBound(FldsArr)
                    dtPaySetup = New DataTable
                    If UCase(FldsArr(j)) = "EMP_CODE" Or UCase(FldsArr(j)) = "EMP_NAME" Then
                        strOrd += "Employee" + "+"
                    ElseIf UCase(FldsArr(j)) = "DEPT_CODE" Or UCase(FldsArr(j)) = "DEPT_NAME" Then
                        strOrd += "Department" + "+"
                    ElseIf UCase(FldsArr(j)) = "DSG_CODE" Or UCase(FldsArr(j)) = "DSG_NAME" Then
                        strOrd += "Designation" + "+"
                    Else
                        Session("DalObj").GetSqlDataTable(dtPaySetup, "Select Print_Name FROM PaySetup WHERE Field_Name = '" & FldsArr(j) & "'")
                        If dtPaySetup.Rows.Count > 0 Then
                            strOrd += dtPaySetup.Rows(0).Item(0) + "+"
                        End If
                    End If
                Next
                If strOrd <> "" Then
                    strOrd = IIf(Right(strOrd, 1) = "+", Mid(strOrd, 1, strOrd.Length - 1), strOrd)
                End If
                cmbOrdBy.Items.Add(New ListItem(strOrd, OrdArr(i)))
            Next

        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (GetGroups())")
        End Try
    End Sub

    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
        'IsPrint = True
        'cmdSetValues_Click(sender, e)
    End Sub

    Private Sub cmdRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRefresh.Click
        Session("BalObj").FillCombo(LstReports, " Select SrNo,RptName from MstReports Where RptType = '" & ViewState("strQString") & "' and Active = 1")
        If LstReports.Items.Count > 0 Then
            LstReports.SelectedIndex = 0
            LstReports_SelectedIndexChanged(Nothing, Nothing)
        End If
    End Sub

#Region "  Pay Fields (RptQuery) "
    Public Sub ReportFields(ByVal FuncList As String, ByVal StartPos As Integer, ByVal QryType As String)
        Try

            Dim StrPos1 As Integer, StrPos2 As Integer, FldName As String, FldFormula As String, _
            ParaList As String, FldsList As String, FldPreFix As String, MaxRows As Integer, MaxCols As Integer, AddFlds As String

            Do While InStr(FuncList, "(") > 0
                StrPos1 = InStr(FuncList, "(")
                If StrPos1 = 0 Then Exit Do

                StrPos2 = InStr(StrPos1, FuncList, ")")
                ParaList = Mid(FuncList, StrPos1 + 1, StrPos2 - StrPos1 - 1)
                FuncList = Replace(FuncList, "(" & ParaList & ")", "")

                StrPos1 = InStr(ParaList, ",")
                FldsList = Mid(ParaList, 1, StrPos1 - 1)

                StrPos2 = InStr(StrPos1 + 1, ParaList, ",")
                FldPreFix = Mid(ParaList, StrPos1 + 1, StrPos2 - StrPos1 - 1)

                StrPos1 = StrPos2
                StrPos2 = InStr(StrPos1 + 1, ParaList, ",")
                MaxRows = Mid(ParaList, StrPos1 + 1, StrPos2 - StrPos1 - 1)

                StrPos1 = StrPos2
                StrPos2 = InStr(StrPos1 + 1, ParaList, ",")

                If StrPos2 = 0 Then
                    MaxCols = Mid(ParaList, StrPos1 + 1)
                    AddFlds = ""
                Else
                    MaxCols = Mid(ParaList, StrPos1 + 1, StrPos2 - StrPos1 - 1)
                    AddFlds = Mid(ParaList, StrPos2 + 1)
                End If

                Do While InStr(FldsList, "[") > 0
                    StrPos1 = InStr(FldsList, "[")
                    If StrPos1 = 0 Then Exit Do
                    StrPos2 = InStr(StrPos1, FldsList, "]")
                    FldName = Mid(FldsList, StrPos1 + 1, StrPos2 - StrPos1 - 1)
                    FldFormula = GetFormula(FldName, PayDate)
                    FldsList = Replace(FldsList, "[" & FldName & "]", FldFormula)
                Loop
                'FldsToArr(FldsList, FldPreFix, MaxRows, MaxCols, StartPos, AddFlds, "")
                FldsToRep(FldsList, FldPreFix, MaxRows, MaxCols, StartPos, AddFlds, QryType)
            Loop
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (Set ReportFields)")
        End Try
    End Sub

    Public Sub FldsToRep(ByVal Flds As String, ByVal FldPreFix As String, ByVal MaxRows As Integer, ByVal MaxCols As Integer, ByVal StartPos As Integer, ByVal AddFlds As String, ByVal QryType As String)
        Try

            Dim i As Int16, J, K As Int16, FldNo As Integer, Line As String, FldName As String, ArrAddFlds(,) As String
            Dim DtPaySetup As New DataTable, tableName As String, BlankFlds As Int16

            'ParArray = Array.CreateInstance(GetType(String), (Occurs(Line, "+") + 1) * 2)

            CType(Session("DalObj"), DAL.DataLayer).GetSqlDataTable(DtPaySetup, "SELECT Field_Name, Field_Desc, Print_Name, Fld_PayHist, Fld_PayMast FROM PaySetup")

            StrToArr(AddFlds, ArrAddFlds, ",")
            FldNo = 0
            If InStr(Flds, "+") = 0 Then
                Flds = GetFormula(Flds, PayDate)
            End If

            Flds = Replace(Replace(Flds, "{", ""), "}", "")
            If Left(Flds, 1) = "+" Then Flds = Mid(Flds, 2)

            If Occurs(Replace(Flds, ";", "+"), "+") < MaxCols Or MaxRows = 1 Then
                Line = Replace(Flds, ";", "+")
                For i = 1 To Occurs(Line, "+") + 1
                    SRNO = SRNO + 2
                    FldName = Mid(Line + "+", At("+" + Line + "+", "+", i), At(Line + "+", "+", i) - At("+" + Line + "+", "+", i))
                    tableName = ""
                    If FldName <> "" Then
                        If adoFind(DtPaySetup, "Field_Name='" & FldName & "'").Count > 0 Then
                            If QryType = "H" And adoFind(DtPaySetup, "Field_Name='" & FldName & "'").Item(0).Item("Fld_PayHist") = "Y" Then
                                tableName = "PAYHIST"
                            Else
                                If adoFind(DtPaySetup, "Field_Name='" & FldName & "'").Item(0).Item("Fld_PayMast") = "Y" Then
                                    tableName = "PAYMAST"
                                End If
                            End If
                        End If
                    End If
                    If tableName <> "" Then
                        FldNo = FldNo + 1
                        If FldNo > MaxCols * MaxRows Then Exit For
                        ReDim Preserve ParArray(SRNO)

                        ParArray(SRNO - 1) = FldPreFix & FldNo & "1" & "='" & adoFind(DtPaySetup, "Field_Name='" & FldName & "'").Item(0).Item("Print_Name") & "'"
                        ParArray(SRNO) = FldPreFix & FldNo & "2" & "={" & tableName & "." & FldName & "}"

                        '==============Arrears Formula Setting==================
                        If AddFlds <> "" And InStr(FldName, "ARR_") = 0 Then
                            For K = 0 To UBound(ArrAddFlds, 2) - 1
                                FldName = IIf(ArrAddFlds(0, K) = "RAT", "", ArrAddFlds(0, K) & "_") & IIf(InStr(FldName, "_") = 0, FldName, Mid(FldName, InStr(FldName, "_") + 1))
                                If adoFind(DtPaySetup, "Field_Name='" & FldName & "'").Count > 0 Then
                                    If ArrAddFlds(0, K) <> "RAT" Or adoFind(DtPaySetup, "Field_Name='" & FldName & "'").Item(0).Item("Fld_PayMast") = "Y" Then
                                        SRNO = SRNO + 1
                                        ReDim Preserve ParArray(SRNO)
                                        ParArray(SRNO) = FldPreFix & FldNo & K + 3 & "={PAYHIST." & FldName & "}"
                                    End If
                                Else
                                    If InStr(FldName, "TAX_") > 0 Then
                                        FldName = Replace(FldName, "TAX_", "PRJ_")
                                        If adoFind(DtPaySetup, "Field_Name='" & FldName & "'").Count > 0 Then
                                            SRNO = SRNO + 1
                                            ReDim Preserve ParArray(SRNO)
                                            ParArray(SRNO) = FldPreFix & FldNo & K + 3 & "={PAYHIST." & FldName & "}"
                                        Else
                                            FldName = Replace(FldName, "PRJ_", "YTD_")
                                            If adoFind(DtPaySetup, "Field_Name='" & FldName & "'").Count > 0 Then
                                                SRNO = SRNO + 1
                                                ReDim Preserve ParArray(SRNO)
                                                ParArray(SRNO) = FldPreFix & FldNo & K + 3 & "={PAYHIST." & FldName & "}"
                                            End If
                                        End If
                                    End If
                                End If
                            Next
                        End If
                    End If
                    '==============================================================

                Next
            Else
                For J = 1 To Occurs(Flds + ";", ";")
                    Line = Mid(Flds + ";", At(";" + Flds + ";", ";", J), At(Flds + ";", ";", J) - At(";" + Flds + ";", ";", J))
                    For i = 1 To Occurs(Line, "+")
                        tableName = ""
                        FldName = Mid(Line + "+", At("+" + Line + "+", "+", i), At(Line + "+", "+", i) - At("+" + Line + "+", "+", i))
                        If FldName <> "" Then
                            If adoFind(DtPaySetup, "Field_Name='" & FldName & "'").Count > 0 Then
                                If QryType = "H" And adoFind(DtPaySetup, "Field_Name='" & FldName & "'").Item(0).Item("Fld_PayHist") = "Y" Then
                                    tableName = "PAYHIST"
                                Else
                                    If adoFind(DtPaySetup, "Field_Name='" & FldName & "'").Item(0).Item("Fld_PayMast") = "Y" Then
                                        tableName = "PAYMAST"
                                    End If
                                End If
                            End If
                        End If
                        If tableName <> "" Then
                            FldNo = FldNo + 1
                            If FldNo > MaxCols * MaxRows Then Exit For
                            If FldNo Mod MaxCols = 0 Then
                                SRNO = SRNO + 2
                                ReDim Preserve ParArray(SRNO)
                                ParArray(SRNO - 1) = FldPreFix & FldNo & "1" & "=''"
                                ParArray(SRNO) = FldPreFix & FldNo & "2" & "=0"
                                FldNo = FldNo + 1
                                If FldNo > MaxCols * MaxRows Then Exit For
                            End If
                            SRNO = SRNO + 2
                            ReDim Preserve ParArray(SRNO)
                            ParArray(SRNO - 1) = FldPreFix & FldNo & "1" & "='" & adoFind(DtPaySetup, "Field_Name='" & FldName & "'").Item(0).Item("Print_Name") & "'"
                            ParArray(SRNO) = FldPreFix & FldNo & "2" & "={" & tableName & "." & FldName & "}"

                            If AddFlds <> "" And InStr(FldName, "ARR_") = 0 Then
                                For K = 0 To UBound(ArrAddFlds, 2) - 1
                                    FldName = IIf(ArrAddFlds(0, K) = "RAT", "", ArrAddFlds(0, K) & "_") & IIf(InStr(FldName, "_") = 0, FldName, Mid(FldName, InStr(FldName, "_") + 1))
                                    If adoFind(DtPaySetup, "Field_Name='" & FldName & "'").Count > 0 Then
                                        If ArrAddFlds(0, K) <> "RAT" Or adoFind(DtPaySetup, "Field_Name='" & FldName & "'").Item(0).Item("Fld_PayMast") = "Y" Then
                                            SRNO = SRNO + 1
                                            ReDim Preserve ParArray(SRNO)
                                            ParArray(SRNO) = FldPreFix & FldNo & K + 3 & "={PAYHIST." & FldName & "}"
                                        End If
                                    Else
                                        If InStr(FldName, "TAX_") > 0 Then
                                            FldName = Replace(FldName, "TAX_", "PRJ_")
                                            If adoFind(DtPaySetup, "Field_Name='" & FldName & "'").Count > 0 Then
                                                SRNO = SRNO + 1
                                                ReDim Preserve ParArray(SRNO)
                                                ParArray(SRNO) = FldPreFix & FldNo & K + 3 & "={PAYHIST." & FldName & "}"
                                            Else
                                                FldName = Replace(FldName, "PRJ_", "YTD_")
                                                If adoFind(DtPaySetup, "Field_Name='" & FldName & "'").Count > 0 Then
                                                    SRNO = SRNO + 1
                                                    ReDim Preserve ParArray(SRNO)
                                                    ParArray(SRNO) = FldPreFix & FldNo & K + 3 & "={PAYHIST." & FldName & "}"
                                                End If
                                            End If
                                        End If
                                    End If
                                Next
                            End If
                        End If
                    Next
                    tableName = ""
                    FldName = Mid(Line + "+", At("+" + Line + "+", "+", i), At(Line + "+", "+", i) - At("+" + Line + "+", "+", i))
                    If FldName <> "" Then
                        If adoFind(DtPaySetup, "Field_Name='" & FldName & "'").Count <> 0 Then
                            If QryType = "H" And adoFind(DtPaySetup, "Field_Name='" & FldName & "'").Item(0).Item("Fld_PayHist") = "Y" Then
                                tableName = "PAYHIST"
                            Else
                                If adoFind(DtPaySetup, "Field_Name='" & FldName & "'").Item(0).Item("Fld_PayMast") = "Y" Then
                                    tableName = "PAYMAST"
                                End If
                            End If
                        End If
                    End If
                    If tableName <> "" Then
                        FldNo = FldNo + 1
                        If FldNo > MaxCols * MaxRows Then Exit For
                        SRNO = SRNO + 2
                        ReDim Preserve ParArray(SRNO)
                        ParArray(SRNO - 1) = FldPreFix & MaxCols * J & "1" & "='" & adoFind(DtPaySetup, "Field_Name='" & FldName & "'").Item(0).Item("Print_Name") & "'"
                        ParArray(SRNO) = FldPreFix & MaxCols * J & "2" & "={" & tableName & "." & FldName & "}"

                        If FldNo Mod MaxCols > 0 Or FldNo < MaxCols * J Then
                            If FldNo < MaxCols * J Then
                                BlankFlds = MaxCols * J - 1
                            Else
                                BlankFlds = FldNo + MaxCols - FldNo Mod MaxCols - 1
                            End If
                            For FldNo = FldNo + 1 To BlankFlds
                                SRNO = SRNO + 2
                                ReDim Preserve ParArray(SRNO)
                                ParArray(SRNO - 1) = FldPreFix & FldNo & "1" & "=''"
                                ParArray(SRNO) = FldPreFix & FldNo & "2" & "=0"
                            Next
                        End If
                    End If
                Next
            End If
            For i = FldNo + 1 To MaxCols * MaxRows
                SRNO = SRNO + 2
                ReDim Preserve ParArray(SRNO)
                ParArray(SRNO - 1) = FldPreFix & i & "1" & "=''"
                ParArray(SRNO) = FldPreFix & i & "2" & "=0"
            Next

        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (Set FldsToRep)")
        End Try
    End Sub

    'Public Sub FldsToArr(ByVal Flds As String, ByVal FldPreFix As String, ByVal MaxRows As Integer, ByVal MaxCols As Integer, ByVal StartPos As Integer, ByVal AddFlds As String, ByVal Group2 As String)
    '    Try

    '        Dim i As Integer, J As Integer, FldNo As Integer, FldName As String, ArrAddFlds() As String
    '        Dim rsPaySetup As New ADODB.Recordset
    '        On Error Resume Next
    '        rsPaySetup = OpnRst("SELECT Field_Name, Field_Desc, Print_Name, Fld_PayHist FROM PaySetup WHERE InStr('" & Flds & "', RTRIM(Field_Name))>0", adUseClient, adOpenDynamic, adLockReadOnly)
    '        StrToArr(AddFlds, ArrAddFlds, "+")
    '        FldNo = 0
    '        If InStr(Flds, "+") = 0 Then
    '            Flds = GetFormula(Flds, PayDate)
    '        End If
    '        Flds = Replace(Replace(Flds, "{", ""), "}", "")
    '        If Left(Flds, 1) = "+" Then Flds = Mid(Flds, 2)
    '        If Occurs(Replace(Flds, ";", "+"), "+") < MaxCols Or MaxRows = 1 Then
    '            Line = Replace(Flds, ";", "+")
    '            For i = 1 To Occurs(Line, "+") + 1
    '                FldNo = FldNo + 1
    '                If FldNo > MaxCols * MaxRows Then Exit For
    '                FldName = Mid(Line + "+", At("+" + Line + "+", "+", i), At(Line + "+", "+", i) - At("+" + Line + "+", "+", i))
    '                If adoFind(rsPaySetup, "Field_Name='" & FldName & "'") Then
    '                    FrmMain.cryRep.Formulas(StartPos + 1) = FldPreFix & FldNo & "1" & "='" & rsPaySetup!Print_Name & "'"
    '                    If rsPaySetup!Fld_PayHist = "Y" Then
    '                        FrmMain.cryRep.Formulas(StartPos + 2) = FldPreFix & FldNo & "2" & "={PAYHIST." & FldName & "}"
    '                    Else
    '                        FrmMain.cryRep.Formulas(StartPos + 2) = FldPreFix & FldNo & "2" & "={PAYMAST." & FldName & "}"
    '                    End If
    '                Else
    '                    FrmMain.cryRep.Formulas(StartPos + 1) = FldPreFix & FldNo & "1" & "='" & FldName & "'"
    '                    FrmMain.cryRep.Formulas(StartPos + 2) = FldPreFix & FldNo & "2" & "={PAYHIST." & FldName & "}"
    '                End If
    '                StartPos = StartPos + 2
    '            Next
    '        Else
    '            For J = 1 To Occurs(Flds + ";", ";")
    '                Line = Mid(Flds + ";", At(";" + Flds + ";", ";", J), At(Flds + ";", ";", J) - At(";" + Flds + ";", ";", J))
    '                For i = 1 To Occurs(Line, "+")
    '                    FldName = Mid(Line + "+", At("+" + Line + "+", "+", i), At(Line + "+", "+", i) - At("+" + Line + "+", "+", i))
    '                    FldNo = FldNo + 1
    '                    If FldNo Mod MaxCols = 0 Then
    '                        FrmMain.cryRep.Formulas(StartPos + 1) = FldPreFix & FldNo & "1" & "=''"
    '                        FrmMain.CryRep.Formulas(StartPos + 2) = FldPreFix & FldNo & "2" & "=0"
    '                        FldNo = FldNo + 1
    '                        StartPos = StartPos + 2
    '                    End If
    '                    If FldNo < MaxRows * MaxCols Then
    '                        If adoFind(rsPaySetup, "Field_Name='" & FldName & "'") Then
    '                            FrmMain.cryRep.Formulas(StartPos + 1) = FldPreFix & FldNo & "1" & "='" & rsPaySetup!Print_Name & "'"
    '                        Else
    '                            FrmMain.cryRep.Formulas(StartPos + 1) = FldPreFix & FldNo & "1" & "='" & FldName & "'"
    '                        End If
    '                        FrmMain.cryRep.Formulas(StartPos + 2) = FldPreFix & FldNo & "2" & "={PAYHIST." & FldName & "}"
    '                        StartPos = StartPos + 2
    '                    End If
    '                Next
    '                FldName = Mid(Line + "+", At("+" + Line + "+", "+", i), At(Line + "+", "+", i) - At("+" + Line + "+", "+", i))
    '                If adoFind(rsPaySetup, "Field_Name='" & FldName & "'") Then
    '                    FrmMain.cryRep.Formulas(StartPos + 1) = FldPreFix & MaxCols * J & "1" & "='" & rsPaySetup!Print_Name & "'"
    '                Else
    '                    FrmMain.cryRep.Formulas(StartPos + 1) = FldPreFix & MaxCols * J & "1" & "='" & FldName & "'"
    '                End If
    '                FrmMain.cryRep.Formulas(StartPos + 2) = FldPreFix & MaxCols * J & "2" & "={PAYHIST." & FldName & "}"
    '                StartPos = StartPos + 2
    '                If FldNo Mod MaxCols > 0 Or FldNo < MaxCols * J Then
    '                    If FldNo < MaxCols * J Then
    '                        BlankFlds = MaxCols * J - 1
    '                    Else
    '                        BlankFlds = FldNo + MaxCols - FldNo Mod MaxCols - 1
    '                    End If
    '                    For FldNo = FldNo + 1 To BlankFlds
    '                        FrmMain.cryRep.Formulas(StartPos + 1) = FldPreFix & FldNo & "1" & "=''"
    '                        FrmMain.CryRep.Formulas(StartPos + 2) = FldPreFix & FldNo & "2" & "=0"
    '                        StartPos = StartPos + 2
    '                    Next
    '                End If
    '            Next
    '        End If
    '        For i = FldNo + 1 To MaxCols * MaxRows
    '            FrmMain.cryRep.Formulas(StartPos + 1) = FldPreFix & i & "1" & "=''"
    '            FrmMain.CryRep.Formulas(StartPos + 2) = FldPreFix & i & "2" & "=0"
    '            StartPos = StartPos + 2
    '        Next
    '        rsPaySetup.Close()
    '        rsPaySetup = Nothing
    '    Catch ex As Exception
    '        SetMsg(LblMsg, ex.Message & " : (Set FldsToArr)")
    '    End Try
    'End Sub


    Public Function GetFormula(ByVal FldName As String, ByVal FormulaDate As Date) As String
        Try
            Dim FldCalc As String
            GetFormula = ""
            FldCalc = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand("SELECT Fld_Calc FROM Formula WHERE Field_Name='" & FldName & "' AND " & " Fld_Date " & "<='" & Format(FormulaDate, "dd/MMM/yyyy") & "' ORDER BY Fld_Date DESC", , DAL.DataLayer.ExecutionType.ExecuteScalar)
            If FldCalc <> "" Then GetFormula = Trim(FldCalc)
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (Set GetFormula)")
        End Try
    End Function

    Public Function At(ByVal SearchIn As String, ByVal SearchFor As String, ByVal Occurance As Integer)
        Dim Occ, i As Int16
        Occ = 0
        For i = 1 To Len(SearchIn)
            If InStr(i, SearchIn, SearchFor) Then
                If Occ + 1 = Occurance Then
                    At = InStr(i, SearchIn, SearchFor)
                    Exit For
                End If
                Occ = Occ + 1
                i = InStr(i, SearchIn, SearchFor)
            End If
        Next
    End Function
    Public Function Occurs(ByVal SearchIn As String, ByVal SearchFor As String)
        Dim Occ, i As Int16
        Occurs = 0
        For i = 1 To Len(SearchIn)
            If InStr(i, SearchIn, SearchFor) Then
                Occurs = Occurs + 1
                i = InStr(i, SearchIn, SearchFor)
            End If
        Next
    End Function
    Public Function Stuff(ByVal SearchIn As String, ByVal SearchFor As String, ByVal ReplaceWith As String) As String
        Stuff = SearchIn
        Do While InStr(Stuff, SearchFor) > 0
            Stuff = Mid(Stuff, 1, InStr(Stuff, SearchFor) - 1) & ReplaceWith & _
                    Mid(Stuff, InStr(Stuff, SearchFor) + Len(SearchFor))
        Loop
    End Function

    Public Sub StrToArr(ByVal FldStr As String, ByRef ArrName(,) As String, ByVal Seprator As String)
        Dim StrPos1, StrPos2 As Int16, FldName As String
        ReDim ArrName(0, 0)
        StrPos1 = 0
        Do While True
            StrPos1 = InStr(StrPos1 + 1, Seprator & FldStr & Seprator, Seprator)
            If StrPos1 = 0 Then Exit Do
            StrPos2 = InStr(StrPos1 + 1, Seprator & FldStr & Seprator, Seprator)
            If StrPos2 = 0 Then Exit Do
            FldName = Mid(Seprator & FldStr & Seprator, StrPos1 + 1, StrPos2 - StrPos1 - 1)
            ArrName(0, UBound(ArrName, 2)) = FldName
            ReDim Preserve ArrName(0, UBound(ArrName, 2) + 1)
        Loop
    End Sub

#End Region

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub
    Private Sub CmdFor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdFor.Click
        Try
            If ViewState("For") = "EMP_CODE" Then
                TxtFroVal.Visible = False
                cmbForVal.Visible = True
            End If
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & "CmdFor_Click()")
        End Try
    End Sub
    Private Sub cmbForVal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbForVal.SelectedIndexChanged
        Try
            If ViewState("For") = "EMP_CODE" Then
                TxtFroVal.Visible = True
                cmbForVal.Visible = False
                TxtFroVal.Text = Chk(cmbForVal.SelectedValue)
            End If
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & "cmbForVal_SelectedIndexChanged()")
        End Try
    End Sub


    Private Sub cmdView_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdView.ServerClick

    End Sub

    Private Sub ReportType_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReportType.TextChanged

    End Sub
End Class
