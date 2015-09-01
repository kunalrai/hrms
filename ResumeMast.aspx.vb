Partial Class ResumeMast
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents BtnUpload As System.Web.UI.WebControls.LinkButton
    Protected WithEvents dtpPDOJ As DITWebLibrary.DTPCombo
    Protected WithEvents ChkPDOJ As System.Web.UI.HtmlControls.HtmlInputCheckBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim SkillCode(), SkillRate() As String

#Region "     On Load     "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If
        'dtpResEntryDate.Text = Format(Date.Today, "dd/MMM/yyyy")
        CreateLanguages()
        FillSkillTree()
        If Not IsPostBack Then
            dtpResEntryDate.Text = Format(Date.Today, "dd/MMM/yyyy")
            If Request.QueryString.Item("Code") <> "" Then
                DisplayRecords(Request.QueryString.Item("Code"))
            End If
            FillComboBox()
            BindGridQual()
            BindGridExp()
            TrQual.Style.Item("FONT-WEIGHT") = "bold"
            TblExp.Style.Item("display") = "none"

            TrPermanent.Style.Item("FONT-WEIGHT") = "bold"
            TblMailing.Style.Item("display") = "none"
            TxtRefreeName.Style.Item("display") = "none"
            CmdNew_Click(sender, e)
        End If
    End Sub

#End Region

#Region " Qualfication Related Coding "

    Private Sub BindGridQual()
        Try
            ViewState("dtQual") = New DataTable
            Dim StrSql As String
            StrSql = "Select QualMast.Qual_Code,UnivMast.Univ_Code, " & _
                     " RESQUAL.College,RESQUAL.Place,RESQUAL.Passing_Year,RESQUAL.Marks_Per, " & _
                     " RESQUAL.Grade,RESQUAL.Subjects From RESQUAL " & _
                     " Left Outer Join QualMast On QualMast.Qual_Code = RESQUAL.Qual_Code " & _
                     " Left Outer Join UnivMast On UnivMast.Univ_Code = RESQUAL.Univ_Code " & _
                     " Where Res_Code = '" & Chk(TxtRefNo.Text) & "'"
            Session("DalObj").GetSqlDataTable(ViewState("dtQual"), StrSql)
            GrdQual.DataSource = ViewState("dtQual")
            GrdQual.DataBind()
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : (BindGridQual)")
        Finally
        End Try
    End Sub

    Private Sub CmdAddQual_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdAddQual.Click
        Try
            Dim i As Int16
            If GrdQual.Items.Count > 0 Then
                If Chk(CType(GrdQual.Items(GrdQual.Items.Count - 1).Controls(0).Controls(1), DropDownList).SelectedValue) <> "" Then

                    For i = 0 To GrdQual.Items.Count - 1
                        ViewState("dtQual").Rows(i).Item("Qual_Code") = Chk(CType(GrdQual.Items(i).Controls(0).Controls(1), DropDownList).SelectedValue)
                        ViewState("dtQual").Rows(i).Item("Univ_Code") = Chk(CType(GrdQual.Items(i).Controls(1).Controls(1), DropDownList).SelectedValue)
                        ViewState("dtQual").Rows(i).Item("College") = Chk(CType(GrdQual.Items(i).Controls(2).Controls(1), TextBox).Text)
                        ViewState("dtQual").Rows(i).Item("Place") = Chk(CType(GrdQual.Items(i).Controls(3).Controls(1), TextBox).Text)
                        ViewState("dtQual").Rows(i).Item("Passing_Year") = Chk(CType(GrdQual.Items(i).Controls(4).Controls(1), TextBox).Text)
                        ViewState("dtQual").Rows(i).Item("Marks_Per") = ChkN(CType(GrdQual.Items(i).Controls(5).Controls(1), TextBox).Text)
                        ViewState("dtQual").Rows(i).Item("Grade") = Chk(CType(GrdQual.Items(i).Controls(6).Controls(1), TextBox).Text)
                        ViewState("dtQual").Rows(i).Item("Subjects") = Chk(CType(GrdQual.Items(i).Controls(7).Controls(1), TextBox).Text)
                    Next

                    Dim tmpTr As DataRow = ViewState("dtQual").NewRow()
                    ViewState("dtQual").Rows.Add(tmpTr)
                    ViewState("dtQual").AcceptChanges()
                    GrdQual.DataSource = ViewState("dtQual")
                    GrdQual.DataBind()
                End If
            Else
                Dim tmpTr As DataRow = ViewState("dtQual").NewRow()
                ViewState("dtQual").Rows.Add(tmpTr)
                ViewState("dtQual").AcceptChanges()
                GrdQual.DataSource = ViewState("dtQual")
                GrdQual.DataBind()
            End If

            TrExp.Style.Item("FONT-WEIGHT") = "normal"
            TblExp.Style.Item("display") = "none"
            TblQual.Style.Item("display") = "block"
            TrQual.Style.Item("FONT-WEIGHT") = "bold"
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : (cmdAddQual_Click)")
        End Try
    End Sub

    'Private Sub cmdQualSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim trnQual As SqlClient.SqlTransaction
    '    Try
    '        Dim cnt As Int16
    '        Dim strSQl As String
    '        trnQual = Session("DalObj").StartTransaction("Qual")
    '        strSQl = "Delete From HrdQual Where Emp_Code = '" & Chk(txtEM_CD.Text) & "'"
    '        Session("DalObj").ExecuteCommand(strSQl, trnQual)
    '        For cnt = 0 To GrdQual.Items.Count - 1
    '            If Chk(CType(GrdQual.Items(cnt).Controls(0).Controls(1), DropDownList).SelectedValue) <> "" Then
    '                strSQl = " Insert InTo HrdQual " & _
    '                         " (Emp_Code,Qual_Code,Univ_Code,College,Place,Passing_Year,Marks_Per,Grade,Subjects) " & _
    '                         " Values " & _
    '                         " ('" & Chk(txtEM_CD.Text) & "','" & _
    '                         Chk(CType(GrdQual.Items(cnt).Controls(0).Controls(1), DropDownList).SelectedValue) & "','" & _
    '                         Chk(CType(GrdQual.Items(cnt).Controls(1).Controls(1), DropDownList).SelectedValue) & "','" & _
    '                         Chk(CType(GrdQual.Items(cnt).Controls(2).Controls(1), TextBox).Text) & "','" & _
    '                         Chk(CType(GrdQual.Items(cnt).Controls(3).Controls(1), TextBox).Text) & "','" & _
    '                         Chk(CType(GrdQual.Items(cnt).Controls(4).Controls(1), TextBox).Text) & "'," & _
    '                         ChkN(CType(GrdQual.Items(cnt).Controls(5).Controls(1), TextBox).Text) & ",'" & _
    '                         Chk(CType(GrdQual.Items(cnt).Controls(6).Controls(1), TextBox).Text) & "','" & _
    '                         Chk(CType(GrdQual.Items(cnt).Controls(7).Controls(1), TextBox).Text) & "')"
    '                Session("DalObj").ExecuteCommand(strSQl, trnQual)
    '            End If
    '        Next
    '        trnQual.Commit()
    '        BindGridQual()
    '    Catch ex As Exception
    '        trnQual.Rollback()
    '        SetMsg(LblMsg, ex.Message & " : (cmdQualSave_Click)")
    '    End Try
    'End Sub

    Private Sub GrdQual_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles GrdQual.ItemDataBound
        Try
            Dim dtQual As New DataTable
            Dim tmpCmb As DropDownList

            Dim i As Int16 = e.Item.ItemIndex
            If i < 0 Then i = 0

            tmpCmb = e.Item.FindControl("cmbQual")
            If Not tmpCmb Is Nothing Then
                Session("BaLObj").FillCombo(tmpCmb, "Select Qual_Code,Qual_Name From QualMast Order By Qual_Name", True)
                ChkCombo(tmpCmb, GrdQual.DataSource.rows(i).item("Qual_Code"))
            End If

            tmpCmb = e.Item.FindControl("cmbUniv")
            If Not tmpCmb Is Nothing Then
                Session("BaLObj").FillCombo(tmpCmb, "Select Univ_Code,Univ_Name From UnivMast Order By Univ_Name", True)
                ChkCombo(tmpCmb, GrdQual.DataSource.rows(i).item("Univ_Code"))
            End If

            GC.Collect()
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : (GrdQual_ItemCreated)")
        End Try
    End Sub

#End Region


#Region " Experience Related Coding "

    Private Sub BindGridExp()
        Try
            ViewState("dtExp") = New DataTable
            Dim StrSql As String
            If Not Session("BalObj").ExistColumn("Dsg_Name", "RESEXP") Then
                StrSql = " Alter Table RESEXP Add Dsg_Name Varchar(50)"
                Session("DalObj").ExecuteCommand(StrSql)
            End If
            StrSql = " Select Right('0' + DateName(d,Exp_From),2) + '/' + Left(DateName(mm,Exp_From),3) + '/' + DateName(yy,Exp_From) As Exp_From, " & _
                     " Right('0' + DateName(d,Exp_To),2) + '/' + Left(DateName(mm,Exp_To),3) + '/' + DateName(yy,Exp_To) As Exp_To, " & _
                     " round(Exp_Years,2) as Exp_Years,Org_Name,DSG_Name,Drawn_Sal,LeavingReason, JobResponsiblities From RESEXP " & _
                     " Where Res_Code = '" & Chk(TxtRefNo.Text) & "' Order By Cast(Exp_From As SmallDateTime)"
            Session("DalObj").GetSqlDataTable(ViewState("dtExp"), StrSql)
            GrdExp.DataSource = ViewState("dtExp")
            GrdExp.DataBind()
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : (BindGridExp)")
        Finally
        End Try
    End Sub
    Private Sub cmdAddExp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddExp.Click
        Try
            Dim i As Int16
            If GrdExp.Items.Count > 0 Then
                If Chk(CType(GrdExp.Items(GrdExp.Items.Count - 1).Controls(0).Controls(1), TextBox).Text) <> "" And Chk(CType(GrdExp.Items(GrdExp.Items.Count - 1).Controls(1).Controls(1), TextBox).Text) <> "" Then
                    For i = 0 To GrdExp.Items.Count - 1
                        ViewState("dtExp").Rows(i).Item("Exp_From") = Chk(CType(GrdExp.Items(i).Controls(0).Controls(1), TextBox).Text)
                        ViewState("dtExp").Rows(i).Item("Exp_To") = Chk(CType(GrdExp.Items(i).Controls(1).Controls(1), TextBox).Text)
                        ViewState("dtExp").Rows(i).Item("Exp_Years") = ChkN(CType(GrdExp.Items(i).Controls(2).Controls(1), TextBox).Text)
                        ViewState("dtExp").Rows(i).Item("Org_Name") = Chk(CType(GrdExp.Items(i).Controls(3).Controls(1), TextBox).Text)
                        ViewState("dtExp").Rows(i).Item("DSG_Name") = Chk(CType(GrdExp.Items(i).Controls(4).Controls(1), TextBox).Text)
                        ViewState("dtExp").Rows(i).Item("JobResponsiblities") = Chk(CType(GrdExp.Items(i).Controls(5).Controls(1), TextBox).Text)
                        ViewState("dtExp").Rows(i).Item("Drawn_Sal") = ChkN(CType(GrdExp.Items(i).Controls(6).Controls(1), TextBox).Text)
                        ViewState("dtExp").Rows(i).Item("LeavingReason") = Chk(CType(GrdExp.Items(i).Controls(7).Controls(1), TextBox).Text)
                    Next
                    Dim tmpTr As DataRow = ViewState("dtExp").NewRow()
                    ViewState("dtExp").Rows.Add(tmpTr)
                    ViewState("dtExp").AcceptChanges()
                    GrdExp.DataSource = ViewState("dtExp")
                    GrdExp.DataBind()
                End If
            Else
                Dim tmpTr As DataRow = ViewState("dtExp").NewRow()
                ViewState("dtExp").Rows.Add(tmpTr)
                GrdExp.DataSource = ViewState("dtExp")
                GrdExp.DataBind()
            End If

            TrQual.Style.Item("FONT-WEIGHT") = "normal"
            TblQual.Style.Item("display") = "none"
            TrExp.Style.Item("FONT-WEIGHT") = "bold"
            TblExp.Style.Item("display") = "block"
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : (cmdAddExp_Click)")
        End Try
    End Sub

    'Private Sub cmdSaveExp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSaveExp.Click
    '    Dim trnExp As SqlClient.SqlTransaction
    '    Try
    '        Dim cnt As Int16
    '        Dim strSQl As String
    '        trnExp = Session("DalObj").StartTransaction("Exp")
    '        strSQl = "Delete From HrdExp Where Emp_Code = '" & Chk(txtEM_CD.Text) & "'"
    '        Session("DalObj").ExecuteCommand(strSQl, trnExp)
    '        For cnt = 0 To GrdExp.Items.Count - 1
    '            Dim expFrom As String = Chk(CType(GrdExp.Items(cnt).Controls(0).Controls(1), TextBox).Text)
    '            Dim expTo As String = Chk(CType(GrdExp.Items(cnt).Controls(1).Controls(1), TextBox).Text)
    '            If expFrom <> "" And expTo <> "" Then
    '                Dim ExpYr As Double
    '                ExpYr = DateDiff(DateInterval.Year, CDate(expFrom), CDate(expTo))
    '                If ExpYr > 0 Then
    '                    strSQl = " Insert InTo HrdExp " & _
    '                             " (Emp_Code,Exp_From,Exp_To,Exp_Years,Org_Name,Dsg_Name,JobProfile,Drawn_Sal,LeavingReason )" & _
    '                             " Values " & _
    '                             " ('" & Chk(txtEM_CD.Text) & "','" & _
    '                             expFrom & "','" & _
    '                             expTo & " '," & _
    '                             ExpYr & ",'" & _
    '                             Chk(CType(GrdExp.Items(cnt).Controls(3).Controls(1), TextBox).Text) & "', '" & _
    '                             Chk(CType(GrdExp.Items(cnt).Controls(4).Controls(1), TextBox).Text) & "', '" & _
    '                             Chk(CType(GrdExp.Items(cnt).Controls(5).Controls(1), TextBox).Text) & " ', '" & _
    '                             ChkN(CType(GrdExp.Items(cnt).Controls(6).Controls(1), TextBox).Text) & "', '" & _
    '                             Chk(CType(GrdExp.Items(cnt).Controls(7).Controls(1), TextBox).Text) & "')"
    '                    Session("DalObj").ExecuteCommand(strSQl, trnExp)
    '                End If
    '            End If
    '        Next
    '        trnExp.Commit()
    '        BindGridExp()
    '    Catch ex As Exception
    '        trnExp.Rollback()
    '        SetMsg(LblMsg, ex.Message & " : (cmdSaveExp_Click)")
    '    End Try
    'End Sub

#End Region

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub

#Region "    Save Records   "

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim Tran As SqlClient.SqlTransaction
        Try
            Dim StrQuery As String, i As Int16

            Dim strPath As String = HttpContext.Current.Request.ServerVariables("APPL_PHYSICAL_PATH") & "Resume\"

            If Not IsValidate() Then Exit Sub


            '=====================Images Code =============
            Dim strFileNameOnly, strFileNamePath As String, intFileNameLength As Integer
            If Trim(ResUpload.Value) <> "" Then
                Dim strFormat As String = Right(UCase(ResUpload.PostedFile.FileName), ResUpload.PostedFile.FileName.Length - ResUpload.PostedFile.FileName.LastIndexOf("."))

                'If InStr(".BMP.JPG.GIF.TEXT.DOC.RTF", strFormat) = 0 Then
                '    SetMsg(lblMsg, "Invalid Format of File.")
                '    Exit Sub
                'End If

                strFileNamePath = ResUpload.PostedFile.FileName
                'Response.Write(strFileNamePath)
                intFileNameLength = InStr(1, StrReverse(strFileNamePath), "\")
                strFileNameOnly = Mid(strFileNamePath, (Len(strFileNamePath) - intFileNameLength) + 2)
                ResUpload.PostedFile.SaveAs(strPath & strFileNameOnly)
                strFileNameOnly = "Resume\" & strFileNameOnly
            End If

            '===========================
            CountSkills()
            CountKnownLanguage()

            Tran = Session("DalObj").StartTransaction("Save")
            If ChkSaveAs.Checked = True Then
                TxtRefNo.Text = Session("BalObj").GetNextNumber("ResMast", "Res_Code")
                TxtRefNo.ToolTip = TxtRefNo.Text
                TxtRefNo.Text = TxtRefNo.Text.PadLeft(4, "0")
                TxtRefNo.Text = "RSM/" & cmbReqNo.SelectedItem.Text & "/" & TxtRefNo.Text
                ViewState("Action") = "ADDNEW"
            End If

            If ViewState("Action") = "ADDNEW" Then

                '===========================Insert ResMast========================

                StrQuery = " Insert ResMast (ResPath,Res_Code,Res_No,Res_NameF,Res_NameM,Res_NameL,SalExpect,Skills,PassportNo,Dept_Code,Dsg_Code,Vacancy_Code,Vacancy_RefNo,Status_Code,Emp_Code, Res_References) Values ('" & _
                            Chk(strFileNameOnly) & "', '" & _
                            ChkN(TxtRefNo.ToolTip) & "', '" & _
                            Chk(TxtRefNo.Text) & "', '" & _
                            Chk(TxtFirstN.Text) & "', '" & _
                            Chk(TxtMiddleN.Text) & "', '" & _
                            Chk(TxtLastN.Text) & "', " & _
                            ChkN(TxtExpSalary.Text) & ", '" & _
                            Chk(TxtSkills.Text) & "', '" & _
                            Chk(TxtPassport.Text) & "', '" & _
                            Chk(cmbDepartment.SelectedValue) & "', '" & _
                            Chk(cmbDesignation.SelectedValue) & "', '" & _
                            ChkN(cmbReqNo.SelectedValue) & "', '" & _
                            Chk(cmbReqNo.SelectedItem.Text) & "', '" & _
                            Chk(cmbStatus.SelectedValue) & "', "
                If cmbRefType.Value = 0 Then
                    StrQuery = StrQuery & "'" & Chk(cmbRefreeName.SelectedValue) & "', Null )"
                ElseIf cmbRefType.Value = 1 Then
                    StrQuery = StrQuery & "Null, '" & Chk(TxtRefreeName.Text) & "' )"
                Else
                    StrQuery = StrQuery & "Null, Null )"
                End If
                Session("DalObj").ExecuteCommand(StrQuery, Tran)

                '===========================Insert ResQual (Qualification)========================

                For i = 0 To GrdQual.Items.Count - 1

                    If Chk(CType(GrdQual.Items(i).Controls(0).Controls(1), DropDownList).SelectedValue) <> "" Then

                        StrQuery = " Insert ResQual (Qual_Code,Univ_Code,College,Place,Passing_Year,Marks_Per,Grade,Subjects,Res_Code) Values ('" & _
                                                       Chk(CType(GrdQual.Items(i).Controls(0).Controls(1), DropDownList).SelectedValue) & "', '" & _
                                                       Chk(CType(GrdQual.Items(i).Controls(1).Controls(1), DropDownList).SelectedValue) & "', '" & _
                                                       Chk(CType(GrdQual.Items(i).Controls(2).Controls(1), TextBox).Text) & "', '" & _
                                                       Chk(CType(GrdQual.Items(i).Controls(3).Controls(1), TextBox).Text) & "', '" & _
                                                       Chk(CType(GrdQual.Items(i).Controls(4).Controls(1), TextBox).Text) & "', " & _
                                                       ChkN(CType(GrdQual.Items(i).Controls(5).Controls(1), TextBox).Text) & ", '" & _
                                                       Chk(CType(GrdQual.Items(i).Controls(6).Controls(1), TextBox).Text) & "', '" & _
                                                       Chk(CType(GrdQual.Items(i).Controls(7).Controls(1), TextBox).Text) & "', '" & _
                                                       Chk(TxtRefNo.ToolTip) & "' )"

                        Session("DalObj").ExecuteCommand(StrQuery, Tran)

                    End If
                Next

                '===========================Insert ResExp (Experiences)========================

                For i = 0 To GrdExp.Items.Count - 1

                    Dim expFrom As String = Chk(CType(GrdExp.Items(i).Controls(0).Controls(1), TextBox).Text)
                    Dim expTo As String = Chk(CType(GrdExp.Items(i).Controls(1).Controls(1), TextBox).Text)
                    If expFrom <> "" And expTo <> "" Then
                        Dim ExpYr As Double, ExpMonth As Double

                        ExpMonth = DateDiff(DateInterval.Month, CDate(expFrom), CDate(expTo))
                        ExpYr = Math.Round(ExpMonth / 12, 2)
                        If ExpYr > 0 Then
                            StrQuery = " Insert InTo RESEXP " & _
                                       " (Res_Code,Exp_From,Exp_To,Exp_Years,Org_Name,Dsg_Name,JobResponsiblities,Drawn_Sal,LeavingReason) Values ('" & _
                                       Chk(TxtRefNo.ToolTip) & "', '" & _
                                       expFrom & "', '" & _
                                       expTo & " ', '" & _
                                       ExpYr & "', '" & _
                                       Chk(CType(GrdExp.Items(i).Controls(3).Controls(1), TextBox).Text) & "', '" & _
                                       Chk(CType(GrdExp.Items(i).Controls(4).Controls(1), TextBox).Text) & "', '" & _
                                       Chk(CType(GrdExp.Items(i).Controls(5).Controls(1), TextBox).Text) & "'," & _
                                       ChkN(CType(GrdExp.Items(i).Controls(6).Controls(1), TextBox).Text) & ", '" & _
                                       Chk(CType(GrdExp.Items(i).Controls(7).Controls(1), TextBox).Text) & "')"

                            Session("DalObj").ExecuteCommand(StrQuery, Tran)
                        End If
                    End If

                Next

                '===========================Insert ResOthers (Other Details)========================

                StrQuery = " Insert RESOTHERS (Res_Code,Res_MAddr1,Res_MAddr2,EnteredDate,Res_MAddr3,Res_MCity,Res_MState,Res_MCountry,Res_MPin,Res_MPhone, " & _
                           " Res_PAddr1,Res_PAddr2,Res_PAddr3,Res_PCity,Res_PState,Res_PCountry,Res_PPin,Res_PPhone,Res_MStatus,Res_DOB,Res_Sex,Res_Nationality,Res_FathHusbName,Languages,Res_MobileNo,Res_EMailId,Res_PhoneNo,NCR) Values ('" & _
                             ChkN(TxtRefNo.ToolTip) & "', '" & _
                             Chk(TxtMAdd1.Text) & "', '" & _
                             Chk(TxtMAdd2.Text) & "', '" & _
                             Chk(dtpResEntryDate.Text) & "', '" & _
                             Chk(TxtMAdd3.Text) & "', '" & _
                             Chk(TxtMCity.Text) & "', '" & _
                             Chk(TxtMState.Text) & "', '" & _
                             Chk(TxtMCountry.Text) & "', '" & _
                             Chk(TxtMPin.Text) & "', '" & _
                             Chk(TxtMPhone.Text) & "', '" & _
                             Chk(TxtPAdd1.Text) & "', '" & _
                             Chk(TxtPAdd2.Text) & "', '" & _
                             Chk(TxtPAdd3.Text) & "', '" & _
                             Chk(TxtPCity.Text) & "', '" & _
                             Chk(TxtPState.Text) & "', '" & _
                             Chk(TxtPCountry.Text) & "', '" & _
                             Chk(TxtPPin.Text) & "', '" & _
                             Chk(TxtPPhone.Text) & "', '" & _
                             Chk(cmbMarital.SelectedValue) & "', '" & _
                             Chk(dtpDOB.Text) & "', '" & _
                             IIf(RdoFemale.Checked, "2", "1") & "', '" & _
                             Chk(TxtNationality.Text) & "', '" & _
                             Chk(TxtFather.Text) & "', '" & _
                             Chk(ViewState("Languages")) & "', '" & _
                             Chk(TxtMobile.Text) & "', '" & _
                             Chk(TxtEmail.Text) & "', '" & _
                             Chk(TxtPhone.Text) & "', '" & _
                             IIf(cmbNCR.SelectedValue = "0", "0", "1") & "' )"


                Session("DalObj").ExecuteCommand(StrQuery, Tran)

                '================Insert ResSkills (Skills)======================

                If Not IsNothing(SkillCode) Then
                    For i = 0 To SkillCode.Length - 1
                        StrQuery = " Insert ResSkills (Res_Code, Skill_Code, Skill_Rate) Values ('" & _
                                             Chk(TxtRefNo.ToolTip) & "', '" & _
                                             Chk(SkillCode(i)) & "', '" & _
                                             Chk(SkillRate(i)) & "' )"

                        Session("DalObj").ExecuteCommand(StrQuery, Tran)
                    Next
                End If


            ElseIf ViewState("Action") = "MODIFY" Then

                '================Update ResMast (Resume Master)======================

                StrQuery = " Update ResMast Set " & _
                        " ResPath = '" & Chk(strFileNameOnly) & "', " & _
                        " Res_Code = '" & ChkN(TxtRefNo.ToolTip) & "', " & _
                        " Res_No = '" & Chk(TxtRefNo.Text) & "', " & _
                        " Res_NameF = '" & Chk(TxtFirstN.Text) & "', " & _
                        " Res_NameM = '" & Chk(TxtMiddleN.Text) & "', " & _
                        " Res_NameL = '" & Chk(TxtLastN.Text) & "', " & _
                        " SalExpect = '" & ChkN(TxtExpSalary.Text) & "', " & _
                        " Skills = '" & Chk(TxtSkills.Text) & "', " & _
                        " PassportNo = '" & Chk(TxtPassport.Text) & "', " & _
                        " Dept_Code = '" & Chk(cmbDepartment.SelectedValue) & "', " & _
                        " Dsg_Code = '" & Chk(cmbDesignation.SelectedValue) & "', " & _
                        " Vacancy_Code = '" & ChkN(cmbReqNo.SelectedValue) & "', " & _
                        " Vacancy_RefNo = '" & Chk(cmbReqNo.SelectedItem.Text) & "', " & _
                        " Status_Code = '" & Chk(cmbStatus.SelectedValue) & "', "

                If cmbRefType.Value = 0 Then
                    StrQuery = StrQuery & "Emp_Code = '" & Chk(cmbRefreeName.SelectedValue) & "', Res_References = Null "
                ElseIf cmbRefType.Value = 1 Then
                    StrQuery = StrQuery & "Emp_Code = Null, Res_References ='" & Chk(TxtRefreeName.Text) & "' "
                Else
                    StrQuery = StrQuery & "Emp_Code = Null, Res_References = Null "
                End If

                StrQuery = StrQuery & " Where Res_Code = '" & ChkN(TxtRefNo.ToolTip) & "'"

                Session("DalObj").ExecuteCommand(StrQuery, Tran)


                '===========================Update ResQual (Qualification)========================

                StrQuery = "Delete From ResQual Where Res_Code = '" & Chk(TxtRefNo.ToolTip) & "'"
                Session("DalObj").ExecuteCommand(StrQuery, Tran)

                For i = 0 To GrdQual.Items.Count - 1

                    If Chk(CType(GrdQual.Items(i).Controls(0).Controls(1), DropDownList).SelectedItem.Text) <> "" Then

                        StrQuery = " Insert ResQual (Qual_Code,Univ_Code,College,Place,Passing_Year,Marks_Per,Grade,Subjects,Res_Code) Values ('" & _
                                   Chk(CType(GrdQual.Items(i).Controls(0).Controls(1), DropDownList).SelectedValue) & "', '" & _
                                   Chk(CType(GrdQual.Items(i).Controls(1).Controls(1), DropDownList).SelectedValue) & "', '" & _
                                   Chk(CType(GrdQual.Items(i).Controls(2).Controls(1), TextBox).Text) & "', '" & _
                                   Chk(CType(GrdQual.Items(i).Controls(3).Controls(1), TextBox).Text) & "', '" & _
                                   Chk(CType(GrdQual.Items(i).Controls(4).Controls(1), TextBox).Text) & "', " & _
                                   ChkN(CType(GrdQual.Items(i).Controls(5).Controls(1), TextBox).Text) & ", '" & _
                                   Chk(CType(GrdQual.Items(i).Controls(6).Controls(1), TextBox).Text) & "', '" & _
                                   Chk(CType(GrdQual.Items(i).Controls(7).Controls(1), TextBox).Text) & "', '" & _
                                   Chk(TxtRefNo.ToolTip) & "' )"

                        Session("DalObj").ExecuteCommand(StrQuery, Tran)

                    End If
                Next


                '===========================Update ResExp (Experiences)========================


                StrQuery = "Delete From RESEXP Where Res_Code = '" & Chk(TxtRefNo.ToolTip) & "'"

                Session("DalObj").ExecuteCommand(StrQuery, Tran)

                For i = 0 To GrdExp.Items.Count - 1

                    Dim expFrom As String = Chk(CType(GrdExp.Items(i).Controls(0).Controls(1), TextBox).Text)
                    Dim expTo As String = Chk(CType(GrdExp.Items(i).Controls(1).Controls(1), TextBox).Text)
                    If expFrom <> "" And expTo <> "" Then
                        Dim ExpYr As Double, ExpMonth As Double
                        ExpMonth = DateDiff(DateInterval.Month, CDate(expFrom), CDate(expTo))
                        ExpYr = Math.Round(ExpMonth / 12, 2)
                        If ExpYr > 0 Then
                            StrQuery = " Insert InTo RESEXP " & _
                                       " (Res_Code,Exp_From,Exp_To,Exp_Years,Org_Name,Dsg_Name,JobResponsiblities,Drawn_Sal,LeavingReason) Values ('" & _
                                       Chk(TxtRefNo.ToolTip) & "', '" & _
                                       expFrom & "', '" & _
                                       expTo & " ', '" & _
                                       ExpYr & "', '" & _
                                       Chk(CType(GrdExp.Items(i).Controls(3).Controls(1), TextBox).Text) & "', '" & _
                                       Chk(CType(GrdExp.Items(i).Controls(4).Controls(1), TextBox).Text) & "', '" & _
                                       Chk(CType(GrdExp.Items(i).Controls(5).Controls(1), TextBox).Text) & "'," & _
                                       ChkN(CType(GrdExp.Items(i).Controls(6).Controls(1), TextBox).Text) & ", '" & _
                                       Chk(CType(GrdExp.Items(i).Controls(7).Controls(1), TextBox).Text) & "')"

                            Session("DalObj").ExecuteCommand(StrQuery, Tran)
                        End If
                    End If

                Next


                '======================Update ResOthers==============================

                StrQuery = " Update RESOTHERS Set " & _
                        " Res_Code = '" & ChkN(TxtRefNo.ToolTip) & "'," & _
                        " Res_MAddr1 ='" & Chk(TxtMAdd1.Text) & "', " & _
                        " Res_MAddr2 = '" & Chk(TxtMAdd2.Text) & "', " & _
                        " EditedDate = '" & Chk(dtpResEntryDate.Text) & "', " & _
                        " Res_MAddr3 = '" & Chk(TxtMAdd3.Text) & "', " & _
                        " Res_MCity = '" & Chk(TxtMCity.Text) & "', " & _
                        " Res_MState = '" & Chk(TxtMState.Text) & "', " & _
                        " Res_MCountry = '" & Chk(TxtMCountry.Text) & "', " & _
                        " Res_MPin = '" & Chk(TxtMPin.Text) & "', " & _
                        " Res_MPhone = '" & Chk(TxtMPhone.Text) & "', " & _
                        " Res_PAddr1 = '" & Chk(TxtPAdd1.Text) & "', " & _
                        " Res_PAddr2 = '" & Chk(TxtPAdd2.Text) & "', " & _
                        " Res_PAddr3 = '" & Chk(TxtPAdd3.Text) & "', " & _
                        " Res_PCity = '" & Chk(TxtPCity.Text) & "', " & _
                        " Res_PState = '" & Chk(TxtPState.Text) & "', " & _
                        " Res_PCountry = '" & Chk(TxtPCountry.Text) & "', " & _
                        " Res_PPin = '" & Chk(TxtPPin.Text) & "'," & _
                        " Res_PPhone = '" & Chk(TxtPPhone.Text) & "', " & _
                        " Res_MStatus = '" & Chk(cmbMarital.SelectedValue) & "', " & _
                        " Res_DOB = '" & Chk(dtpDOB.Text) & "', " & _
                        " Res_Sex = '" & IIf(RdoFemale.Checked, "2", "1") & "', " & _
                        " Res_FathHusbName = '" & Chk(TxtFather.Text) & "', " & _
                        " Languages = '" & Chk(ViewState("Languages")) & "', " & _
                        " Res_MobileNo = '" & Chk(TxtMobile.Text) & "', " & _
                        " Res_EMailId = '" & Chk(TxtEmail.Text) & "', " & _
                        " Res_PhoneNo = '" & Chk(TxtPhone.Text) & "', " & _
                        " NCR = '" & IIf(cmbNCR.SelectedValue = "0", "0", "1") & "', " & _
                        " Res_Nationality  = '" & Chk(TxtNationality.Text) & "' Where Res_Code = '" & Chk(TxtRefNo.ToolTip) & "'"

                Session("DalObj").ExecuteCommand(StrQuery, Tran)


                '========================Update ResSkills (Skills)======================

                StrQuery = "Delete From RESSkills Where Res_Code = '" & Chk(TxtRefNo.ToolTip) & "'"
                Session("DalObj").ExecuteCommand(StrQuery, Tran)

                If Not IsNothing(SkillCode) Then
                    For i = 0 To SkillCode.Length - 1
                        StrQuery = " Insert ResSkills (Res_Code, Skill_Code, Skill_Rate) Values ('" & _
                                             Chk(TxtRefNo.ToolTip) & "', '" & _
                                             Chk(SkillCode(i)) & "', '" & _
                                             Chk(SkillRate(i)) & "' )"

                        Session("DalObj").ExecuteCommand(StrQuery, Tran)
                    Next
                End If

            End If
            Tran.Commit()

            CmdNew_Click(sender, e)
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : " & ex.Source)
            Tran.Rollback()
        Finally
            If Not IsNothing(Tran) Then Tran.Dispose()
        End Try
    End Sub

    Function IsValidate() As Boolean

        Dim Code As Int16
        If Chk(TxtRefNo.Text) = "" Then
            SetMsg(lblMsg, " Ref No. Can not be left blank.")
            Return False
        End If
        If Chk(TxtEmail.Text) = "" Then
            SetMsg(lblMsg, "E-Mail ID can not be left blank.")
            Return False
        End If
        If ChkSaveAs.Checked = True Then
            Code = Session("DalObj").ExecuteCommand(" Select Count(*) From RESMAST Where Res_Code = '" & Chk(TxtRefNo.ToolTip) & "' AND Vacancy_Code= '" & Chk(cmbReqNo.SelectedValue) & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
            If Code <> 0 Then
                SetMsg(lblMsg, "Requisition No. and Reference No. Can't be Same.")
                Return False
            End If
        End If
        If Chk(cmbReqNo.SelectedValue) = "" Then
            SetMsg(lblMsg, " Please Select Requisition No. from the list.")
            Return False
        End If

        If Chk(TxtFirstN.Text) = "" Then
            SetMsg(lblMsg, " First Name Can not be left blank.")
            Return False
        End If

        Return True
    End Function

    Public Sub CountSkills()
        Try
            Dim i As Int16, DtTemp As DataTable, SklCode, SklRate As String

            DtTemp = New DataTable
            SklRate = ""
            SklCode = ""
            Session("DalObj").GetSqlDataTable(DtTemp, "Select * from SKILLMAST Where SKILL_SCALE='R' and SKILL_TYPE='KS'")
            For i = 0 To DtTemp.Rows.Count - 1
                With DtTemp.Rows(i)
                    If Not Session("TblSkill").FindControl("Chk" & .Item("Skill_Code")) Is Nothing Then
                        If CType(Session("TblSkill").FindControl("Chk" & .Item("Skill_Code")), HtmlInputCheckBox).Checked = True Then
                            SklCode = SklCode & .Item("Skill_Code") & "|"
                            SklRate = SklRate & CType(Session("TblSkill").FindControl("cmb" & .Item("Skill_Code")), HtmlSelect).Value & "|"
                        End If
                    End If
                End With
            Next

            If SklCode <> "" Then
                SkillCode = Split(Mid(SklCode, 1, Len(SklCode) - 1), "|")
                SkillRate = Split(Mid(SklRate, 1, Len(SklRate) - 1), "|")
            End If
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message)
        End Try

    End Sub

#End Region

#Region "   Fill ComboBox   "

    Sub FillComboBox()
        Session("BalObj").FillCombo(cmbStatus, "Select STATUS_CODE, STATUS_NAME from STATUSMAST", True)
        Session("BalObj").FillCombo(cmbRefreeName, "Select Emp_Code,Emp_Name From HrdMastQry Order by Emp_Name", True)
        Session("BalObj").FillCombo(cmbReqNo, "Select Vacancy_Code, Vacancy_RefNo from Vacancy where AprFlag='Y'", True)
        Session("BalObj").FillCombo(cmbDesignation, "Select Dsg_Code, Dsg_Name From DsgMast", True)
        Session("BalObj").FillCombo(cmbDepartment, "Select Dept_Code, Dept_Name From DeptMast", True)
    End Sub

#End Region

#Region "    Requisition Changed    "

    Private Sub cmbReqNo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbReqNo.SelectedIndexChanged
        Try

            Dim DeptCode As String, DtTemp As New DataTable, i As Int16, DesgCode As String
            If cmbReqNo.SelectedValue = "" Then Exit Sub
            'Str = "select Count(Res_code) from resmast Vacancy_Code = '" & cmbReqNo.SelectedValue & "'"
            LblvTotRes.Text = Session("DalObj").ExecuteCommand("select Count(Res_code) from resmast where Vacancy_Code = '" & cmbReqNo.SelectedValue & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
            LblvInProcess.Text = ChkN(Session("DalObj").ExecuteCommand("select Count(Res_code) from resmast where Vacancy_Code = '" & cmbReqNo.SelectedValue & "' and Status_Code ='101'", , DAL.DataLayer.ExecutionType.ExecuteScalar))
            LblvRejected.Text = ChkN(Session("DalObj").ExecuteCommand("select Count(Res_code) from resmast where Vacancy_Code = '" & cmbReqNo.SelectedValue & "' and Status_Code='110' ", , DAL.DataLayer.ExecutionType.ExecuteScalar))
            LblvSuitable.Text = ChkN(Session("DalObj").ExecuteCommand("select Count(Res_code) from resmast where Vacancy_Code = '" & cmbReqNo.SelectedValue & "' and Status_Code <>'110' ", , DAL.DataLayer.ExecutionType.ExecuteScalar))
            LblVHold.Text = ChkN(Session("DalObj").ExecuteCommand("select Count(Res_code) from resmast where Vacancy_Code = '" & cmbReqNo.SelectedValue & "' and Status_Code='109' ", , DAL.DataLayer.ExecutionType.ExecuteScalar))
            If ViewState("Action") = "ADDNEW" Then
                TxtRefNo.Text = Session("BalObj").GetNextNumber("ResMast", "Res_Code")
                TxtRefNo.ToolTip = TxtRefNo.Text
                TxtRefNo.Text = TxtRefNo.Text.PadLeft(4, "0")
                TxtRefNo.Text = "RSM/" & cmbReqNo.SelectedItem.Text & "/" & TxtRefNo.Text
            End If

            '=============Fill Department On the basis of Requisistion Selected=================
            DeptCode = Session("DalObj").ExecuteCommand("Select Dept_Code From Vacancy Where Vacancy_Code = '" & cmbReqNo.SelectedValue & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
            ChkCombo(cmbDepartment, DeptCode)

            'Session("BalObj").FillCombo(cmbDepartment, "Select Dept_Code, Dept_Name From DeptMast Where Dept_Code = '" & DeptCode & "'")

            '=============================Fill Designation==================================
            DeptCode = ""
            DeptCode = Session("DalObj").ExecuteCommand("Select Top 1 DSG_CODE From VacancyDesg Where Vacancy_Code = '" & cmbReqNo.SelectedValue & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
            ChkCombo(cmbDesignation, DeptCode)
            'For i = 0 To DtTemp.Rows.Count - 1
            '    DesgCode = DesgCode & DtTemp.Rows(i).Item("DSG_CODE") & "', '"
            'Next
            'DesgCode = "'" & Mid(DesgCode, 1, Len(DesgCode.Trim) - 3)
            'Session("BalObj").FillCombo(cmbDesignation, "Select Dsg_Code, Dsg_Name From DsgMast Where Dsg_Code in (" & DesgCode & ")")
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : " & ex.Source)
        End Try
    End Sub

#End Region

#Region "   New and Edit Code  "

    Private Sub CmdNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdNew.Click
        Try
            lblMsg.Text = ""
            ClearAll(Me)
            ChkSaveAs.Visible = False
            BlankRecords()
            BindGridExp()
            BindGridQual()
            RdoMale.Checked = True
            TxtRefNo.Visible = True
            cmbRefNo.Visible = False
            Hypres.Visible = False
            ViewState("Action") = "ADDNEW"
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : " & ex.Source)
        End Try
    End Sub

    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        Try
            lblMsg.Text = ""
            ClearAll(Me)
            ChkSaveAs.Visible = False
            BlankRecords()
            BindGridExp()
            BindGridQual()
            TxtRefNo.Visible = False
            cmbRefNo.Visible = True
            Hypres.Visible = True
            Session("BalObj").FillCombo(cmbRefNo, " Select Res_Code, (vacancy_RefNo+'  ' + (isnull(Res_NameF,'')+' '+isnull(Res_NameM,'')+' '+isnull(Res_NameL,''))) as SName From ResMast", True)
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : " & ex.Source)
        End Try
    End Sub

    Sub BlankRecords()
        Try
            Dim DtTemp As New DataTable, SqlStr As String, i As Int16

            SqlStr = " Select Skill_Code from SkillMast where Skill_Type='KS'and Skill_Scale='R'"
            Session("DalObj").GetSqlDataTable(DtTemp, SqlStr)
            If DtTemp.Rows.Count = 0 Then Exit Sub

            For i = 0 To DtTemp.Rows.Count - 1
                With DtTemp.Rows(i)
                    If Not Session("TblSkill").FindControl("Chk" & .Item("Skill_Code")) Is Nothing Then
                        CType(Session("TblSkill").FindControl("Chk" & .Item("Skill_Code")), HtmlInputCheckBox).Checked = False
                        CType(Session("TblSkill").FindControl("cmb" & .Item("Skill_Code")), HtmlSelect).Style.Item("display") = "none"
                        CType(Session("TblSkill").FindControl("cmb" & .Item("Skill_Code")), HtmlSelect).Value = ""
                    End If
                End With
            Next
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : " & "Blank Records")
        End Try
    End Sub

#End Region

#Region "   Display Records   "

    Private Sub cmbRefNo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbRefNo.SelectedIndexChanged
        Try
            If cmbRefNo.SelectedValue = "" Then Exit Sub
            BlankRecords()
            TxtRefNo.Visible = True
            TxtRefNo.ToolTip = cmbRefNo.SelectedValue
            TxtRefNo.Text = cmbRefNo.SelectedItem.Text
            cmbRefNo.Visible = False
            ChkSaveAs.Visible = True
            ViewState("Action") = "MODIFY"
            DisplayRecords(TxtRefNo.ToolTip)
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : " & ex.Source)
        End Try
    End Sub

    Sub DisplayRecords(ByVal Code As String)
        Try
            Dim DtTemp As New DataTable, StrQuery As String, i As Int16

            StrQuery = " Select RM.RESPATH,RM.Res_Code,Res_No,Res_NameF,Res_NameM,Skills,Res_NameL,RM.PassportNo,Vacancy_Code,Vacancy_RefNo,Status_Code,Emp_Code,Res_References, RM.Dept_Code, RM.Dsg_Code, SalExpect, " & _
                       " Res_MAddr1,Res_MAddr2,Res_MAddr3,Res_MCity,Res_MState,Res_MCountry,Res_MPin,Res_MPhone,Res_PAddr1,Res_PAddr2,Res_PAddr3, " & _
                       " Res_PCity,Res_PState,Res_PCountry,Res_PPin,Res_PPhone,Res_MStatus,Res_DOB,Res_Sex,Res_Nationality,Res_FathHusbName, NCR, Languages, Res_MobileNo, " & _
                       " Res_EMailId,Res_PhoneNo from ResMast RM inner Join ResOthers RO On RM.Res_Code = RO.Res_Code Where RM.Res_Code = '" & Code & "'"

            Session("DalObj").GetSqlDataTable(DtTemp, StrQuery)

            If DtTemp.Rows.Count = 0 Then Exit Sub

            With DtTemp.Rows(0)
                TxtRefNo.ToolTip = ChkN(.Item("Res_Code"))
                TxtRefNo.Text = Chk(.Item("Res_No"))
                TxtFirstN.Text = Chk(.Item("Res_NameF"))
                TxtMiddleN.Text = Chk(.Item("Res_NameM"))
                TxtLastN.Text = Chk(.Item("Res_NameL"))
                TxtSkills.Text = Chk(.Item("Skills"))
                cmbReqNo.SelectedValue = ChkN(.Item("Vacancy_Code"))
                cmbReqNo_SelectedIndexChanged(cmbReqNo, Nothing)
                cmbStatus.SelectedValue = Chk(.Item("Status_Code"))
                ChkCombo(cmbDepartment.SelectedValue, Chk(.Item("Dept_Code")))
                ChkCombo(cmbDesignation.SelectedValue, Chk(.Item("Dsg_Code")))

                If ChkN(.Item("Res_Sex")) = 1 Then
                    RdoMale.Checked = True
                ElseIf ChkN(.Item("Res_Sex")) = 2 Then
                    RdoFemale.Checked = True
                End If

                HypRes.NavigateUrl = Chk(.Item("ResPath"))
                TxtMAdd1.Text = Chk(.Item("Res_MAddr1"))
                TxtMAdd2.Text = Chk(.Item("Res_MAddr2"))
                TxtMAdd3.Text = Chk(.Item("Res_MAddr3"))
                TxtMCity.Text = Chk(.Item("Res_MCity"))
                TxtMState.Text = Chk(.Item("Res_MState"))
                TxtMCountry.Text = Chk(.Item("Res_MCountry"))
                TxtMPin.Text = Chk(.Item("Res_MPin"))
                TxtMPhone.Text = Chk(.Item("Res_MPhone"))
                TxtPAdd1.Text = Chk(.Item("Res_PAddr1"))
                TxtPAdd2.Text = Chk(.Item("Res_PAddr2"))
                TxtPAdd3.Text = Chk(.Item("Res_PAddr3"))
                TxtPCity.Text = Chk(.Item("Res_PCity"))
                TxtPState.Text = Chk(.Item("Res_PState"))
                TxtPCountry.Text = Chk(.Item("Res_PCountry"))
                TxtPPin.Text = Chk(.Item("Res_PPin"))
                TxtPPhone.Text = Chk(.Item("Res_PPhone"))
                TxtExpSalary.Text = ChkN(.Item("SalExpect"))
                TxtPassport.Text = Chk(.Item("PassportNo"))
                TxtMobile.Text = Chk(.Item("Res_MobileNo"))

                ChkCombo(cmbMarital.SelectedValue, ChkN(.Item("Res_MStatus")))

                If Not IsDBNull(.Item("Res_DOB")) Then
                    dtpDOB.DateValue = .Item("Res_DOB")
                End If

                'If Not IsDBNull(.Item("Res_DOJ")) Then
                '    ChkPDOJ.Checked = True
                '    dtpPDOJ.Enabled = True
                '    dtpPDOJ.DateValue = .Item("Res_DOJ")
                'End If

                'If IsDBNull(.Item("Res_DOJ")) Then
                '    ChkPDOJ.Checked = False
                '    dtpPDOJ.Enabled = False
                '    dtpPDOJ.DateValue = Date.Today
                'Else
                '    ChkPDOJ.Checked = True
                '    dtpPDOJ.Enabled = True
                '    dtpPDOJ.DateValue = CDate(.Item("Res_DOJ"))
                'End If

                If Not IsDBNull(.Item("NCR")) Then
                    cmbNCR.SelectedValue = IIf(.Item("NCR") = True, "1", "0")
                End If

                TxtNationality.Text = Chk(.Item("Res_Nationality"))
                TxtFather.Text = Chk(.Item("Res_FathHusbName"))
                TxtEmail.Text = Chk(.Item("Res_EMailId"))
                TxtPhone.Text = Chk(.Item("Res_PhoneNo"))

                If Not (IsDBNull(.Item("Emp_Code")) = True And IsDBNull(.Item("Res_References")) = True) Then
                    If Not IsDBNull(.Item("Res_References")) Then
                        cmbRefType.Value = "1"
                        TxtRefreeName.Style.Item("display") = "block"
                        cmbRefreeName.Style.Item("display") = "none"
                        TxtRefreeName.Text = Chk(.Item("Res_References"))
                    End If
                    If Not IsDBNull(.Item("Emp_Code")) Then
                        cmbRefType.Value = "0"
                        TxtRefreeName.Style.Item("display") = "none"
                        cmbRefreeName.Style.Item("display") = "block"
                        cmbRefreeName.SelectedValue = Chk(.Item("Emp_Code"))
                    End If
                End If

                BlankLanguage()
                If Chk(.Item("Languages")) <> "" Then
                    FillLanguage(Chk(.Item("Languages")))
                End If

                ViewState("dtQual") = New DataTable
                Session("DalObj").GetSqlDataTable(ViewState("dtQual"), " Select * from ResQual Where Res_Code = '" & Code & "'") 'and Dsg_Code = '" & Chk(.Item("Dsg_Code")) & "'

                If ViewState("dtQual").Rows.Count <> 0 Then
                    GrdQual.DataSource = ViewState("dtQual")
                    GrdQual.DataBind()
                End If

                ViewState("dtExp") = New DataTable

                StrQuery = "Select Right('0' + DateName(d,Exp_From),2) + '/' + Left(DateName(mm,Exp_From),3) + '/' + DateName(yy,Exp_From) As Exp_From, " & _
                           " Right('0' + DateName(d,Exp_To),2) + '/' + Left(DateName(mm,Exp_To),3) + '/' + DateName(yy,Exp_To) As Exp_To, " & _
                           " Exp_Years,Org_Name,DSG_Name,Drawn_Sal,LeavingReason, JobResponsiblities From RESEXP " & _
                          " Where Res_Code = '" & Chk(Code) & "' Order By Cast(Exp_From As SmallDateTime)"

                Session("DalObj").GetSqlDataTable(ViewState("dtExp"), StrQuery) 'and Dsg_Code = '" & Chk(.Item("Dsg_Code")) & "'

                If ViewState("dtExp").Rows.Count <> 0 Then
                    GrdExp.DataSource = ViewState("dtExp")
                    GrdExp.DataBind()
                End If

            End With

            '===========================Display Skill Tree=================================
            DtTemp = New DataTable
            StrQuery = " Select Res_Code, Skill_Code, Skill_Rate from ResSkills where Res_Code='" & Code & "'"
            Session("DalObj").GetSqlDataTable(DtTemp, StrQuery)
            If DtTemp.Rows.Count = 0 Then Exit Sub

            For i = 0 To DtTemp.Rows.Count - 1
                With DtTemp.Rows(i)
                    If Not Session("TblSkill").FindControl("Chk" & .Item("Skill_Code")) Is Nothing Then
                        CType(Session("TblSkill").FindControl("Chk" & .Item("Skill_Code")), HtmlInputCheckBox).Checked = True
                        CType(Session("TblSkill").FindControl("cmb" & .Item("Skill_Code")), HtmlSelect).Style.Item("display") = "block"
                        CType(Session("TblSkill").FindControl("cmb" & .Item("Skill_Code")), HtmlSelect).Value = .Item("Skill_Rate")
                    End If
                End With
            Next

        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : DisplayRecords()")
        End Try

    End Sub

#End Region

#Region "    Fill Skill Tree   "

    Public Sub FillSkillTree()
        Try
            Dim i As Int16, dt As New DataTable, str As String
            Dim TblRow As HtmlTableRow, RowCell1 As HtmlTableCell, RowCell2 As HtmlTableCell, Tbl As HtmlTable
            Session("TblSkill") = New HtmlTable
            Session("TblSkill") = TblSkills
            str = "Select SKILL_CODE, SKILL_NAME, Skill_Grp From SKILLMAST Where Skill_Scale='R' and Skill_Grp='*'"
            Session("DalObj").GetSqlDataTable(dt, str)

            For i = 0 To dt.Rows.Count - 1
                With dt.Rows(i)
                    TblRow = New HtmlTableRow
                    RowCell1 = New HtmlTableCell
                    RowCell2 = New HtmlTableCell
                    Dim img As New HtmlImage
                    Tbl = New HtmlTable


                    '=================First Cell==================
                    RowCell1.Align = "Left"
                    RowCell1.Width = "5%"
                    RowCell1.VAlign = "Top"

                    img.ID = "Img" & .Item("SKILL_CODE")
                    img.Src = "images/Plus.gif"
                    img.Attributes.Add("OnClick", "ShowMenu('" & .Item("SKILL_CODE") & "')")
                    img.Width = 9
                    img.Height = 9

                    RowCell1.Controls.Add(img)


                    '=================Second Cell==================
                    Tbl.ID = "Tbl" & .Item("Skill_Code")
                    Tbl.CellPadding = 2
                    Tbl.CellSpacing = 3
                    Tbl.Border = 0
                    Tbl.Style.Item("display") = "none"
                    ChildNode(.Item("Skill_Code"), Tbl)

                    RowCell2.Align = "Left"
                    RowCell2.VAlign = "Top"
                    RowCell2.InnerText = .Item("SKILL_NAME")
                    RowCell2.Style.Item("FONT-WEIGHT") = "bold"
                    RowCell2.Style.Item("FONT-SIZE") = "8pt"
                    RowCell2.Controls.Add(Tbl)

                    TblRow.Cells.Add(RowCell1)
                    TblRow.Cells.Add(RowCell2)
                    Session("TblSkill").Rows.Add(TblRow)

                End With
            Next
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : " & "Fill Parent TreeNode")
        End Try
    End Sub

    Public Function ChildNode(ByVal CD As String, ByRef HtmlTbl As HtmlTable) As String
        Try
            Dim rsChild As New DataTable, cnt As Int16, StrQuery As String, Code As Int16
            Dim TblRow As HtmlTableRow, RowCell1 As HtmlTableCell, RowCell2 As HtmlTableCell, RowCell3 As HtmlTableCell
            Dim Tbl As HtmlTable, img As HtmlImage, cmb As HtmlSelect, Chk As HtmlInputCheckBox, RowCell4 As HtmlTableCell
            Dim CmbSkill As DropDownList, dt As DataTable

            Dim strSql = "Select SKILL_CODE, SKILL_NAME, Skill_Grp, Skill_Type From SKILLMAST Where Skill_Scale='R' and Skill_Grp='" & CD & "'"
            Session("DalObj").GetSqlDataTable(rsChild, strSql)

            For cnt = 0 To rsChild.Rows.Count - 1
                With rsChild.Rows(cnt)
                    TblRow = New HtmlTableRow
                    RowCell2 = New HtmlTableCell
                    RowCell3 = New HtmlTableCell
                    RowCell4 = New HtmlTableCell
                    img = New HtmlImage

                    Tbl = New HtmlTable

                    If .Item("Skill_Type") <> "KS" Then
                        '=================First Cell==================
                        RowCell1 = New HtmlTableCell

                        RowCell1.Align = "Left"
                        RowCell1.Width = "5px"
                        RowCell1.VAlign = "Top"

                        img.Src = "images/Plus.gif"
                        img.ID = "img" & .Item("Skill_Code")
                        img.Attributes.Add("OnClick", "ShowMenu('" & .Item("SKILL_CODE") & "')")
                        img.Width = 9
                        img.Height = 9
                        RowCell1.Controls.Add(img)
                        TblRow.Cells.Add(RowCell1)
                    Else
                        '=================Second Cell==================
                        Chk = New HtmlInputCheckBox
                        Chk.ID = "Chk" & .Item("Skill_Code")
                        Chk.Style.Item("width") = "15px"
                        Chk.Style.Item("height") = "15px"
                        Chk.Attributes.Add("onclick", "ShowCombo('" & .Item("SKILL_CODE") & "')")
                        Chk.Checked = False
                        RowCell2.Align = "Left"
                        RowCell2.VAlign = "Top"
                        RowCell2.Controls.Add(Chk)


                        '=================Fourth Cell (Leaf Node) ==================
                        dt = New DataTable
                        cmb = New HtmlSelect
                        cmb.Attributes.Add("runat", "server")
                        cmb.ID = "cmb" & .Item("Skill_Code")
                        cmb.Style.Item("width") = "95px"
                        cmb.Style.Item("display") = "none"
                        Session("DalObj").GetSqlDataTable(dt, "Select Skill_Rate, Skill_Rate_Desc from SkillRate")
                        cmb.DataSource = dt
                        cmb.DataTextField = "Skill_Rate_Desc"
                        cmb.DataValueField = "Skill_Rate"
                        cmb.DataBind()

                        'CmbSkill = New DropDownList
                        'CmbSkill.ID = "cmb" & .Item("SKILL_CODE")
                        'CmbSkill.Width = New Unit(95)
                        'CmbSkill.Visible = False
                        'Session("BalObj").FillCombo(CmbSkill, "Select Skill_Rate, Skill_Rate_Desc from SkillRate")
                        RowCell4.Align = "Left"
                        RowCell4.VAlign = "Top"
                        RowCell4.Controls.Add(cmb)

                    End If

                    '=================Third Cell===========================

                    Tbl.ID = "Tbl" & .Item("Skill_Code")
                    Tbl.CellPadding = 0
                    Tbl.CellSpacing = 3
                    Tbl.Style.Item("display") = "none"
                    ChildNode(.Item("Skill_Code"), Tbl)

                    RowCell3.Align = "Left"
                    RowCell3.VAlign = "Top"
                    RowCell3.InnerText = .Item("SKILL_NAME")
                    RowCell3.Controls.Add(Tbl)

                    '======================Adding Cells & Rows in Table====== 
                    If Not RowCell2 Is Nothing Then TblRow.Cells.Add(RowCell2)
                    TblRow.Cells.Add(RowCell3)
                    If Not RowCell4 Is Nothing Then TblRow.Cells.Add(RowCell4)
                    HtmlTbl.Rows.Add(TblRow)
                End With
            Next
            rsChild.Dispose()
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : " & "Fill Child TreeNode")
        End Try
    End Function

#End Region

#Region "      Coding Related to Language      "

    Sub CreateLanguages()
        Try
            Dim StrQry As String, i As Int16

            ViewState("DtLang") = New DataTable


            StrQry = "Select Lang_Code, Lang_Name from LangMast"
            Session("DalObj").GetSqlDataTable(ViewState("DtLang"), StrQry)

            Dim TableCell1 As HtmlTableCell, TableCell2 As HtmlTableCell
            Dim Chk1 As CheckBox, Chk2 As CheckBox, Chk3 As CheckBox
            Dim TableCell3 As HtmlTableCell, TableCell4 As HtmlTableCell
            Dim TableCell5 As HtmlTableCell, TableCell6 As HtmlTableCell
            Dim TableCell7 As HtmlTableCell, TableCell8 As HtmlTableCell
            Dim TablRow As HtmlTableRow, LblName As Label

            Session("TBLLang") = TblLanguages

            For i = 0 To ViewState("DtLang").Rows.Count - 1

                Chk1 = New CheckBox
                Chk1.ID = "Chk1" & ViewState("DtLang").Rows(i).Item("Lang_Code")
                Chk2 = New CheckBox
                Chk2.ID = "Chk2" & ViewState("DtLang").Rows(i).Item("Lang_Code")
                Chk3 = New CheckBox
                Chk3.ID = "Chk3" & ViewState("DtLang").Rows(i).Item("Lang_Code")

                LblName = New Label
                LblName.ID = "Lbl" & i
                LblName.Style.Item("runat") = "server"
                LblName.Width = Unit.Pixel(80)

                If (i Mod 2) = 0 Then

                    TableCell1 = New HtmlTableCell
                    TableCell1.Width = "20%"
                    LblName.Text = "  " & ViewState("DtLang").Rows(i).Item("Lang_Name")
                    TableCell1.Controls.Add(LblName)

                    TableCell2 = New HtmlTableCell
                    TableCell2.Width = "10%"
                    TableCell2.InnerHtml = "&nbsp;&nbsp;"
                    TableCell2.Controls.Add(Chk1)

                    TableCell3 = New HtmlTableCell
                    TableCell3.Width = "10%"
                    TableCell3.InnerHtml = "&nbsp;&nbsp;"
                    TableCell3.Controls.Add(Chk2)

                    TableCell4 = New HtmlTableCell
                    TableCell4.Width = "10%"
                    TableCell4.InnerHtml = "&nbsp;&nbsp;"
                    TableCell4.Controls.Add(Chk3)

                Else
                    TablRow = New HtmlTableRow

                    TableCell5 = New HtmlTableCell
                    TableCell5.Width = "20%"
                    LblName.Text = "  " & ViewState("DtLang").Rows(i).Item("Lang_Name")
                    TableCell5.Controls.Add(LblName)

                    TableCell6 = New HtmlTableCell
                    TableCell6.Width = "10%"
                    TableCell6.InnerHtml = "&nbsp;&nbsp;"
                    TableCell6.Controls.Add(Chk1)

                    TableCell7 = New HtmlTableCell
                    TableCell7.Width = "10%"
                    TableCell7.InnerHtml = "&nbsp;&nbsp;"
                    TableCell7.Controls.Add(Chk2)

                    TableCell8 = New HtmlTableCell
                    TableCell8.Width = "10%"
                    TableCell8.InnerHtml = "&nbsp;&nbsp;"
                    TableCell8.Controls.Add(Chk3)

                    TablRow.Cells.Add(TableCell1)
                    TablRow.Cells.Add(TableCell2)
                    TablRow.Cells.Add(TableCell3)
                    TablRow.Cells.Add(TableCell4)
                    TablRow.Cells.Add(TableCell5)
                    TablRow.Cells.Add(TableCell6)
                    TablRow.Cells.Add(TableCell7)
                    TablRow.Cells.Add(TableCell8)

                    Session("TBLLang").Rows.Add(TablRow)

                End If

            Next

            If (ViewState("DtLang").Rows.Count Mod 2) <> 0 Then
                TablRow = New HtmlTableRow
                TableCell5 = New HtmlTableCell
                TableCell5.ColSpan = 4

                TableCell5.Width = "50%"
                TableCell5.InnerHtml = "&nbsp;"
                TablRow.Cells.Add(TableCell1)
                TablRow.Cells.Add(TableCell2)
                TablRow.Cells.Add(TableCell3)
                TablRow.Cells.Add(TableCell4)
                TablRow.Cells.Add(TableCell5)
                Session("TBLLang").Rows.Add(TablRow)
            End If

        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : (FillLanguages)")
        End Try
    End Sub

    Sub CountKnownLanguage()
        Try
            Dim i As Int16, StrLang As String, TotLang As String, CountLanguage() As String

            For i = 0 To ViewState("DtLang").Rows.Count - 1

                StrLang = ""

                If ((CType(Session("TBLLang").FindControl("Chk1" & ViewState("DtLang").Rows(i).Item("Lang_Code")), CheckBox).Checked = True) Or (CType(Session("TBLLang").FindControl("Chk2" & ViewState("DtLang").Rows(i).Item("Lang_Code")), CheckBox).Checked = True) Or (CType(Session("TBLLang").FindControl("Chk3" & ViewState("DtLang").Rows(i).Item("Lang_Code")), CheckBox).Checked = True)) Then

                    If CType(Session("TBLLang").FindControl("Chk1" & ViewState("DtLang").Rows(i).Item("Lang_Code")), CheckBox).Checked = True Then
                        StrLang = ViewState("DtLang").Rows(i).Item("Lang_Code") & "^1"
                    Else
                        StrLang = ViewState("DtLang").Rows(i).Item("Lang_Code") & "^0"
                    End If

                    If CType(Session("TBLLang").FindControl("Chk2" & ViewState("DtLang").Rows(i).Item("Lang_Code")), CheckBox).Checked = True Then
                        StrLang = StrLang & "1"
                    Else
                        StrLang = StrLang & "0"
                    End If

                    If CType(Session("TBLLang").FindControl("Chk3" & ViewState("DtLang").Rows(i).Item("Lang_Code")), CheckBox).Checked = True Then
                        StrLang = StrLang & "1"
                    Else
                        StrLang = StrLang & "0"
                    End If
                End If

                If StrLang <> "" Then
                    TotLang = TotLang & StrLang & "|"
                End If

            Next

            If TotLang <> "" Then
                ViewState("Languages") = Mid(TotLang, 1, Len(TotLang) - 1)
            Else
                ViewState("Languages") = ""
            End If

        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : (CountKnownLanguage)")
        End Try
    End Sub

    Sub FillLanguage(ByVal Value As String)
        Try
            Dim Countstr() As String, LangKnown() As String, i As Int16, j As Int16
            Dim Str1 As String
            Countstr = Split(Value, "|")

            For i = 0 To Countstr.Length - 1
                LangKnown = Split(Countstr(i), "^")
                For j = 0 To LangKnown(1).Length - 1
                    If Left(LangKnown(1), 1) = 1 Then
                        CType(Session("TBLLang").FindControl("Chk" & j + 1 & LangKnown(0)), CheckBox).Checked = True
                    End If
                    LangKnown(1) = Right(LangKnown(1), LangKnown(1).Length - 1)
                Next
            Next
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : (Page_Load)")
        End Try
    End Sub

#Region "     Blank Language    "

    Sub BlankLanguage()
        Try
            Dim i As Int16

            For i = 0 To Session("TBLLang").Controls.Count - 1
                If TypeOf Session("TBLLang").Controls(i) Is CheckBox Then CType(Session("TBLLang").Controls(i), CheckBox).Checked = False
                If Session("TBLLang").Controls(i).HasControls Then
                    BlankSubLanguage(Session("TBLLang").Controls)
                End If
            Next

        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : (BlankLanguage)")
        End Try
    End Sub

    Sub BlankSubLanguage(ByVal ctrls As System.Web.UI.ControlCollection)
        Dim i As Int16

        For i = 0 To ctrls.Count - 1
            If TypeOf ctrls.Item(i) Is CheckBox Then CType(ctrls.Item(i), CheckBox).Checked = False
            If ctrls.Item(i).HasControls Then
                BlankSubLanguage(ctrls.Item(i).Controls)
            End If
        Next
    End Sub

#End Region


#End Region

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub

End Class
