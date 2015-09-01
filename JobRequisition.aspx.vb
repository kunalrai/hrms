Partial Class JobRequisition
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents cmbCrntStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cmbCngStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents TxtOtherSkills As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim CountQual() As String

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If

        If Not IsPostBack Then
            FillCombo()
            BindgrdReq()
            btnNew_Click(sender, Nothing)
            Dim DtVp As New DataTable, DTHR As New DataTable

            Dim StrSQl As String = "Select * From JobReq Where Emp_Code = '" & (Session("LoginUser").UserID) & "' and Type_Code='VP' "
            Session("DalObj").GetSqlDataTable(DtVp, StrSQl)


            Dim StrHR As String = "Select * From JobReq Where  Emp_Code = '" & (Session("LoginUser").UserID) & "' and Type_Code='HRADMIN' "
            Session("DalObj").GetSqlDataTable(DTHR, StrHR)

            If DtVp.Rows.Count > 0 Then
                ChkVP.Enabled = True
                ChkHRADMIN.Enabled = False
                ChkHOD.Enabled = False

            ElseIf DTHR.Rows.Count > 0 Then
                ChkHRADMIN.Enabled = True
                ChkHOD.Enabled = True
                ChkVP.Enabled = False

            ElseIf Session("LoginUser").UserGroup = "HOD" Then
                ChkHOD.Enabled = True
                ChkHRADMIN.Enabled = False
                ChkVP.Enabled = False
            Else
                ChkHOD.Enabled = True
                ChkHRADMIN.Enabled = True
                ChkVP.Enabled = True
            End If
        End If
    End Sub

    Private Sub FillCombo()
        Try
            Session("BalObj").FillCombo(cmbrefid, "Select Vacancy_Code, Vacancy_RefNo from Vacancy", True)
            Session("BalObj").FillCombo(cmbDept, "Dept_Code", "Dept_Name", "DeptMast", True)
            Session("BalObj").FillCombo(LstQual, "Select Qual_Code, Qual_Name from QualMast order by Qual_Name")
            Session("BalObj").FillCombo(cmbTemplete, "TempId", "TempDesc", "RecTempMast", True)
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (FillCombo)")
        End Try
    End Sub

    Private Sub BindgrdReq()
        Try
            ViewState("dtreq") = New DataTable
            Dim StrSql As String
            StrSql = "Select Dsg_Code,Exp_from, Exp_to, NoOfPost, isnull(Gender,0) as Gender, isnull(Salary,0) as Salary, isnull(IsPermanent,1) as IsPermanent, isnull(Age,0) as Age, JobRole, ReqSkills, '' as Qual, '' as QualCode, OthQual from vacancydesg where Vacancy_Code = '" & Chk(Txtrefno.Text) & "'"
            Session("DalObj").GetSqlDataTable(ViewState("dtreq"), StrSql)
            grdReq.DataSource = ViewState("dtreq")
            grdReq.DataBind()
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (BindGrdreq)")
        Finally
        End Try
    End Sub

    Private Sub grdReq_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdReq.ItemDataBound
        Try
            Dim dtreq As DataTable
            Dim tmpCmb As HtmlSelect

            Dim i As Int16 = e.Item.ItemIndex
            If i < 0 Then i = 0

            tmpCmb = e.Item.FindControl("cmbDsg")
            If Not tmpCmb Is Nothing Then
                dtreq = New DataTable
                Session("DalObj").GetSqlDataTable(dtreq, "Select Dsg_Code,Dsg_Name From DsgMast Order By Dsg_Name")

                tmpCmb.DataSource = dtreq
                tmpCmb.DataValueField = "Dsg_Code"
                tmpCmb.DataTextField = "Dsg_Name"
                tmpCmb.DataBind()
                tmpCmb.Value = Chk(grdReq.DataSource.rows(i).item("Dsg_Code"))
            End If
            GC.Collect()
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (Grdreq_ItemCreated)")
        End Try
    End Sub

    Private Sub cmdReqAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReqAdd.Click
        Try
            Dim i As Int16
            If grdReq.Items.Count > 0 Then
                If Chk(CType(grdReq.Items(grdReq.Items.Count - 1).Controls(0).Controls(1), HtmlSelect).Value) <> "" Then

                    For i = 0 To grdReq.Items.Count - 1
                        ViewState("dtreq").Rows(i).Item("Dsg_Code") = Chk(CType(grdReq.Items(i).Controls(0).Controls(1), HtmlSelect).Value)
                        ViewState("dtreq").Rows(i).Item("Exp_from") = ChkN(CType(grdReq.Items(i).Controls(1).Controls(1), TextBox).Text)
                        ViewState("dtreq").Rows(i).Item("Exp_to") = ChkN(CType(grdReq.Items(i).Controls(2).Controls(1), TextBox).Text)
                        ViewState("dtreq").Rows(i).Item("NoOfPost") = ChkN(CType(grdReq.Items(i).Controls(3).Controls(1), TextBox).Text)
                        ViewState("dtreq").Rows(i).Item("ReqSkills") = Chk(CType(grdReq.Items(i).Controls(4).Controls(1), TextBox).Text)
                        'ViewState("dtreq").Rows(i).Item("Gender") = Chk(CType(grdReq.Items(i).Controls(5).Controls(1), DropDownList).SelectedValue)
                        ViewState("dtreq").Rows(i).Item("Salary") = ChkN(CType(grdReq.Items(i).Controls(5).Controls(1), TextBox).Text)
                        ViewState("dtreq").Rows(i).Item("JobRole") = Chk(CType(grdReq.Items(i).Controls(6).Controls(1), TextBox).Text)
                        ViewState("dtreq").Rows(i).Item("Qual") = Chk(CType(grdReq.Items(i).Controls(8).Controls(1), TextBox).Text)
                        ViewState("dtreq").Rows(i).Item("QualCode") = Chk(grdReq.Items(i).Cells(9).Text)
                        ViewState("dtreq").Rows(i).Item("OthQual") = Chk(CType(grdReq.Items(i).Controls(10).Controls(1), TextBox).Text)
                    Next
                    Dim tmpTr As DataRow = ViewState("dtreq").NewRow()
                    ViewState("dtreq").Rows.Add(tmpTr)
                    grdReq.DataSource = ViewState("dtreq")
                    grdReq.DataBind()
                End If
            Else
                Dim tmpTr As DataRow = ViewState("dtreq").NewRow()
                ViewState("dtreq").Rows.Add(tmpTr)
                grdReq.DataSource = ViewState("dtreq")
                grdReq.DataBind()
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (cmdReqAdd_Click)")
        End Try
    End Sub

    Private Sub btnList_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnList.Click
        LblErrMsg.Text = ""
        cmbrefid.Visible = True
        Txtrefno.Visible = False
        btnList.Visible = False
        btnNew.Visible = False
    End Sub

    Private Sub cmbrefid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbrefid.SelectedIndexChanged
        Try
            If cmbrefid.SelectedIndex <> cmbrefid.Items.Count - 1 Then
                Txtrefno.Visible = True
                btnList.Visible = True
                btnNew.Visible = True
                cmbrefid.Visible = False
                Txtrefno.ToolTip = cmbrefid.SelectedValue
                Txtrefno.Text = cmbrefid.SelectedItem.Text
                ViewState("ReqCode") = Txtrefno.Text
                DisplayRecords(ChkN(Txtrefno.ToolTip))
                ViewState("Action") = "MODIFY"
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message)
        End Try

    End Sub

    Sub DisplayRecords(ByVal Code As String)
        Try
            Dim dttemp As DataTable, Dv As DataView
            Dim i, j As Int16, SqlStr, QualName, QualCode As String

            dttemp = New DataTable
            SqlStr = " Select Dept_Code, Vacancy_Desc, TempId,AprFlag from Vacancy Where VACANCY_CODE='" & Code & "'"
            Session("DalObj").GetSqlDataTable(dttemp, SqlStr)
            If dttemp.Rows.Count <> 0 Then
                ChkCombo(cmbDept, Chk(dttemp.Rows(0).Item("Dept_Code")))
                TxtDesc.Text = Chk(dttemp.Rows(0).Item("Vacancy_Desc"))
                cmbTemplete.SelectedValue = ChkN(dttemp.Rows(0).Item("TempID"))
                If Chk(dttemp.Rows(0).Item("AprFlag")) = "H" Then
                    ChkHOD.Checked = True
                ElseIf Chk(dttemp.Rows(0).Item("AprFlag")) = "HA" Then
                    ChkHOD.Checked = True
                    ChkHRADMIN.Checked = True
                ElseIf Chk(dttemp.Rows(0).Item("AprFlag")) = "Y" Then
                    ChkHOD.Checked = True
                    ChkHRADMIN.Checked = True
                    ChkVP.Checked = True
                ElseIf Chk(dttemp.Rows(0).Item("AprFlag")) = "N" Then
                    ChkHOD.Checked = False
                    ChkHRADMIN.Checked = False
                    ChkVP.Checked = False
                End If
            End If
            SqlStr = " Select Dsg_Code,Exp_from, Exp_to, NoOfPost, isnull(Gender,0) as Gender, isnull(Salary,0) as Salary, isnull(IsPermanent,1) as IsPermanent, isnull(Age,0) as Age,OthQual, JobRole, ReqSkills, '' as Qual, '' as QualCode from vacancydesg where Vacancy_Code = '" & Code & "'"
            ViewState("dtreq") = New DataTable
            Session("DalObj").GetSqlDataTable(ViewState("dtreq"), SqlStr)
            grdReq.DataSource = ViewState("dtreq")
            grdReq.DataBind()

            '==============Display Requisition Qualification==============

            SqlStr = " Select Vacancy_Code, Dsg_Code, VQ.Qual_Code, QM.Qual_Name from VacancyQual VQ Inner Join QualMast QM On VQ.Qual_Code = QM.Qual_Code where VQ.Vacancy_Code = '" & Code & "'"
            Dv = Session("DalObj").GetDataView(SqlStr, , "Dsg_Code")

            If Dv.Count = 0 Then Exit Sub

            For i = 0 To grdReq.Items.Count - 1
                QualName = ""
                QualCode = ""
                Dv.RowFilter = " Dsg_Code = '" & Chk(CType(grdReq.Items(i).Controls(0).Controls(1), HtmlSelect).Value) & "'"
                If Dv.Count <> 0 Then
                    For j = 0 To Dv.Count - 1
                        QualName = QualName & Dv.Item(j).Item("Qual_Name") & ", "
                        QualCode = QualCode & Dv.Item(j).Item("Qual_Code") & "|"
                    Next
                End If

                If QualName <> "" Then
                    QualName = Mid(QualName, 1, Len(Trim(QualName)) - 1)
                    QualCode = Mid(QualCode, 1, Len(Trim(QualCode)) - 1)
                End If

                CType(grdReq.Items(i).Controls(8).Controls(1), TextBox).Text = QualName
                grdReq.Items(i).Cells(9).Text = QualCode
            Next

        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Display Records")
        End Try
    End Sub

    Private Sub cmdReqSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdReqSave.Click

        Try
            Dim Code, Strsql, IsHOD, DEPT_HRDMASTQRY As String
            Dim dt As New DataTable

            Code = Session("LoginUser").UserID
            Strsql = "Select Type_Code from JobReq where Emp_Code='" & Code & "'"
            Session("DalObj").GetSqlDatatable(Dt, Strsql)


            DEPT_HRDMASTQRY = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand("select DEPT_CODE from hrdmastqry where emp_code='" & Chk(Code) & "'" & Session("UserCodes"), , DAL.DataLayer.ExecutionType.ExecuteScalar)
            If Session("LoginUser").UserGroup = "HOD" Then
                If DEPT_HRDMASTQRY = cmbDept.SelectedValue Then
                    IsHOD = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand("select emp_code from deptmast where dept_code in( select dept_code from hrdmastqry where emp_code='" & Chk(Code) & "')" & Session("UserCodes"), , DAL.DataLayer.ExecutionType.ExecuteScalar)
                    If IsHOD = Code Then
                        SaveRecords(Nothing)
                        SendMail()
                    Else
                        SetMsg(LblErrMsg, "You are not the HOD of Selected Department")
                        Exit Sub
                    End If
                Else
                    SetMsg(LblErrMsg, "You Can Raise Requisition only For Your Department.")
                    Exit Sub
                End If
            End If

            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item("Type_Code") = "VP" Then
                    If ((ChkHOD.Checked = True) And (ChkHRADMIN.Checked = True)) Then
                        SaveRecords(Nothing)
                        SendMail()
                    Else
                        SetMsg(LblErrMsg, "This Requisition is not Approved by Either HOD Or HR Admin.")
                        Exit Sub
                    End If
                End If
                If dt.Rows(0).Item("Type_Code") = "HRADMIN" Then
                    If ChkHOD.Checked = True Then
                        SaveRecords(Nothing)
                        SendMail()
                    Else
                        SetMsg(LblErrMsg, "This Requisition is not Approved by HOD.")
                        Exit Sub
                    End If
                End If
            End If
            If (Session("LoginUser").UserGroup <> "HOD" And dt.Rows.Count = 0) Then
                SaveRecords(Nothing)
            End If

        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " :(cmdReqSave_Click) ")
        End Try
    End Sub

    Function IsValidate() As Boolean
        Try
            Dim cnt As Int16
            If Trim(Txtrefno.Text) = "" Then
                SetMsg(LblErrMsg, "Requisition Id Can't Be Left Blank.")
                Return False
            End If

            If Trim(TxtDesc.Text) = "" Then
                SetMsg(LblErrMsg, "Description Can't Be Left Blank.")
                Return False
            End If
            If Trim(cmbDept.SelectedValue) = "" Then
                SetMsg(LblErrMsg, "Departmenet Can't Be Left Blank.")
                Return False
            End If
            If Trim(cmbTemplete.SelectedValue) = "" Then
                SetMsg(LblErrMsg, "Process Templete Can't Be Left Blank.")
                Return False
            End If
            If grdReq.Items.Count = 0 Then
                SetMsg(LblErrMsg, "Please Select atleast one Requisition from the list.")
                Return False
            End If
            If CType(grdReq.Items(cnt).Controls(6).Controls(1), TextBox).Text = "" Then
                SetMsg(LblErrMsg, "Job Role Can't Be Left Blank.")
                Return False
            End If
            If CType(grdReq.Items(cnt).Controls(4).Controls(1), TextBox).Text = "" Then
                SetMsg(LblErrMsg, "Please Enter Some Required Skills.")
                Return False
            End If
            If CType(grdReq.Items(cnt).Controls(8).Controls(1), TextBox).Text = "" Then
                SetMsg(LblErrMsg, "Please Select atleast one Qualification from the list.")
                Return False
            End If
            Return True

        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Validate Records")
        End Try
    End Function

#Region "    Save Record Function     "
    Private Sub SaveRecords(ByVal sender As Object)
        Dim Tran As SqlClient.SqlTransaction
        Try
            Dim cnt As Int16
            Dim strSQl, StrQual() As String, i, j As Int16
            If Not IsValidate() Then Exit Sub
            Tran = Session("DalObj").StartTransaction("Save")

            If ViewState("Action") = "ADDNEW" Then
                Txtrefno.Text = Chk(cmbDept.SelectedValue) & "-" & Right(Session("FinYear"), 2) & "/" & ViewState("ReqCode")

                '=====================Insert Vacancy ( Requisition Master )========================

                strSQl = " Insert InTo vacancy (vacancy_Code, Vacancy_RefNo,Vacancy_desc, dept_code,TempId, RequiredBy,AprFlag) Values ('" & _
                         Chk(Txtrefno.ToolTip) & "', '" & _
                         Chk(Txtrefno.Text) & "', '" & _
                         Chk(TxtDesc.Text) & "', '" & _
                         Chk(cmbDept.SelectedValue) & "', '" & _
                         Chk(cmbTemplete.SelectedValue) & "', '" & _
                         Session("LoginUser").UserId & "', "

                If ChkHOD.Checked = True Then
                    If ChkHRADMIN.Checked = True Then
                        If ChkVP.Checked = True Then
                            strSQl = strSQl & "'Y')"
                        Else
                            strSQl = strSQl & "'HA')"
                        End If
                    Else
                        strSQl = strSQl & "'H')"
                    End If
                Else
                    strSQl = strSQl & "'N')"
                End If
                Session("DalObj").ExecuteCommand(strSQl, Tran)

                'Gender, IsPermanent, Age,
                'Chk(CType(grdReq.Items(cnt).Controls(5).Controls(1), DropDownList).SelectedValue) & "','" & _
                'Chk(CType(grdReq.Items(cnt).Controls(6).Controls(1), DropDownList).SelectedValue) & "','" & _
                'Chk(CType(grdReq.Items(cnt).Controls(7).Controls(1), DropDownList).SelectedValue) & "','" & _
                '=====================Insert VacancyDesg ( Vacancy Designation )========================
                For cnt = 0 To grdReq.Items.Count - 1
                    If Chk(CType(grdReq.Items(cnt).Controls(0).Controls(1), HtmlSelect).Value) <> "" Then
                        strSQl = " Insert InTo VacancyDesg (vacancy_Code, Dsg_Code, Exp_from, Exp_to, NoOfPost, ReqSkills,OthQual,  Salary, JobRole) Values " & _
                                 " ('" & Chk(Txtrefno.ToolTip) & "','" & _
                                 Chk(CType(grdReq.Items(cnt).Controls(0).Controls(1), HtmlSelect).Value) & "','" & _
                                 Chk(CType(grdReq.Items(cnt).Controls(1).Controls(1), TextBox).Text) & "','" & _
                                 Chk(CType(grdReq.Items(cnt).Controls(2).Controls(1), TextBox).Text) & "','" & _
                                 Chk(CType(grdReq.Items(cnt).Controls(3).Controls(1), TextBox).Text) & "','" & _
                                 Chk(CType(grdReq.Items(cnt).Controls(4).Controls(1), TextBox).Text) & "','" & _
                                 Chk(CType(grdReq.Items(cnt).Controls(10).Controls(1), TextBox).Text) & "','" & _
                                 ChkN(CType(grdReq.Items(cnt).Controls(5).Controls(1), TextBox).Text) & "','" & _
                                 Chk(CType(grdReq.Items(cnt).Controls(6).Controls(1), TextBox).Text) & "')"

                        Session("DalObj").ExecuteCommand(strSQl, Tran)
                    End If
                Next

                '=====================Insert VacancyQual ( Vacancy Qualification )========================

                For i = 0 To grdReq.Items.Count - 1
                    If Chk(grdReq.Items(i).Cells(9).Text) <> "" Then
                        StrQual = Split(Chk(grdReq.Items(i).Cells(9).Text), "|")

                        For j = 0 To StrQual.Length - 1

                            strSQl = " Insert VacancyQual (Vacancy_Code,Dsg_Code,Qual_Code) Values ('" & _
                                     Chk(Txtrefno.ToolTip) & "', '" & _
                                     Chk(CType(grdReq.Items(i).Controls(0).Controls(1), HtmlSelect).Value) & "', '" & _
                                     Chk(StrQual(j)) & "' )"

                            Session("DalObj").ExecuteCommand(strSQl, Tran)
                        Next
                    End If
                Next


            ElseIf ViewState("Action") = "MODIFY" Then

                '=====================Update Vacancy ( Vacancy Master )========================

                strSQl = " Update vacancy set " & _
                            "vacancy_Code = " & ChkN(Txtrefno.ToolTip) & ", " & _
                            "Vacancy_RefNo = '" & Chk(Txtrefno.Text) & "', " & _
                            "Vacancy_Desc = '" & Chk(TxtDesc.Text) & "', " & _
                            "Dept_code = '" & Chk(cmbDept.SelectedValue) & "', " & _
                            "TempId = '" & Chk(cmbTemplete.SelectedValue) & "', " & _
                            "RequiredBy = '" & Session("LoginUser").UserId & "', "

                If ChkHOD.Checked = True Then
                    If ChkHRADMIN.Checked = True Then
                        If ChkVP.Checked = True Then
                            strSQl = strSQl & " APRFLAG = 'Y'"
                        Else
                            strSQl = strSQl & " APRFLAG = 'HA'"
                        End If
                    Else
                        strSQl = strSQl & " APRFLAG = 'H'"
                    End If
                Else
                    strSQl = strSQl & " APRFLAG = 'N'"
                End If

                strSQl = strSQl & " Where vacancy_Code = '" & Chk(Txtrefno.ToolTip) & "'"
                Session("DalObj").ExecuteCommand(strSQl, Tran)

                'Gender, IsPermanent, Age, 
                'Chk(CType(grdReq.Items(cnt).Controls(5).Controls(1), DropDownList).SelectedValue) & "','" & _
                'Chk(CType(grdReq.Items(cnt).Controls(6).Controls(1), DropDownList).SelectedValue) & "','" & _
                'Chk(CType(grdReq.Items(cnt).Controls(7).Controls(1), DropDownList).SelectedValue) & "','" & _
                ' =====================Update VacancyDesg ( Vacancy Designation )========================

                strSQl = " Delete from VacancyDesg where Vacancy_Code = '" & Chk(Txtrefno.ToolTip) & "'"
                Session("DalObj").ExecuteCommand(strSQl, Tran)

                For cnt = 0 To grdReq.Items.Count - 1
                    If Chk(CType(grdReq.Items(cnt).Controls(0).Controls(1), HtmlSelect).Value) <> "" Then
                        strSQl = " Insert InTo VacancyDesg (vacancy_Code, Dsg_Code, Exp_from, Exp_to, NoOfPost, ReqSkills,OthQual, Salary, JobRole) Values " & _
                                 " ('" & Chk(Txtrefno.ToolTip) & "','" & _
                                 Chk(CType(grdReq.Items(cnt).Controls(0).Controls(1), HtmlSelect).Value) & "','" & _
                                 Chk(CType(grdReq.Items(cnt).Controls(1).Controls(1), TextBox).Text) & "','" & _
                                 Chk(CType(grdReq.Items(cnt).Controls(2).Controls(1), TextBox).Text) & "','" & _
                                 Chk(CType(grdReq.Items(cnt).Controls(3).Controls(1), TextBox).Text) & "','" & _
                                 Chk(CType(grdReq.Items(cnt).Controls(4).Controls(1), TextBox).Text) & "','" & _
                                 Chk(CType(grdReq.Items(cnt).Controls(10).Controls(1), TextBox).Text) & "','" & _
                                 ChkN(CType(grdReq.Items(cnt).Controls(5).Controls(1), TextBox).Text) & "','" & _
                                 Chk(CType(grdReq.Items(cnt).Controls(6).Controls(1), TextBox).Text) & "' )"

                        Session("DalObj").ExecuteCommand(strSQl, Tran)
                    End If
                Next


                '=====================Update VacancyQual ( Vacancy Qualification )========================


                strSQl = " Delete from VacancyQual where Vacancy_Code = '" & Chk(Txtrefno.ToolTip) & "'"
                Session("DalObj").ExecuteCommand(strSQl, Tran)

                For i = 0 To grdReq.Items.Count - 1
                    If Chk(grdReq.Items(i).Cells(9).Text) <> "" Then
                        StrQual = Split(Chk(grdReq.Items(i).Cells(9).Text), "|")

                        For j = 0 To StrQual.Length - 1
                            strSQl = " Insert VacancyQual (Vacancy_Code,Dsg_Code,Qual_Code) Values ('" & _
                                     Chk(Txtrefno.ToolTip) & "', '" & _
                                     Chk(CType(grdReq.Items(i).Controls(0).Controls(1), HtmlSelect).Value) & "', '" & _
                                     Chk(StrQual(j)) & "' )"
                            Session("DalObj").ExecuteCommand(strSQl, Tran)
                        Next
                    End If
                Next
            End If
            Tran.Commit()
            btnNew_Click(sender, Nothing)
            SetMsg(LblErrMsg, " Records Saved Sucessfully.")
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (cmdreqSave_Click)")
            Tran.Rollback()
        Finally
            If Not IsNothing(Tran) Then Tran.Dispose()
        End Try
    End Sub
#End Region

    Private Sub cmdReqclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdReqclose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNew.Click
        Try
            ClearAll(Me)
            Txtrefno.Text = Session("BalObj").GetNextNumber("Vacancy", "Vacancy_Code")
            Txtrefno.ToolTip = Txtrefno.Text
            Txtrefno.Text = Txtrefno.Text.PadLeft(4, "0")
            ViewState("ReqCode") = Txtrefno.Text
            FillCombo()
            BindgrdReq()
            ViewState("Action") = "ADDNEW"
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (btnNew_Click)")
        End Try
    End Sub

#Region "  Qualification Related Coding   "

    Private Sub grdReq_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdReq.SelectedIndexChanged
        Dim i, j As Int16, StrCount() As String
        For i = 0 To LstQual.Items.Count - 1
            LstQual.Items(i).Selected = False
        Next

        If grdReq.SelectedItem.Cells(9).Text <> "" Then
            StrCount = Split(grdReq.SelectedItem.Cells(9).Text, "|")

            For i = 0 To StrCount.Length - 1
                If Not IsNothing(LstQual.Items.FindByValue(StrCount(i))) Then
                    LstQual.Items.FindByValue(StrCount(i)).Selected = True
                End If
            Next
        End If

        TrQual.Style.Item("display") = "block"
        LblDspl.Text = "Select Qualification for " & Chk(CType(grdReq.SelectedItem.Controls(0).Controls(1), HtmlSelect).Items(CType(grdReq.SelectedItem.Controls(0).Controls(1), HtmlSelect).SelectedIndex).Text) & " Designation. "
        'CType(grdReq.SelectedItem.Controls(0).Controls(1), TextBox).Text = LblDspl
    End Sub

    Private Sub cmdSaveQual_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSaveQual.Click
        Dim i As Int16, QualCode, QualName As String

        For i = 0 To LstQual.Items.Count - 1
            If LstQual.Items(i).Selected = True Then
                QualCode = QualCode & Chk(LstQual.Items(i).Value) & "|"
                QualName = QualName & Chk(LstQual.Items(i).Text) & ", "
            End If
        Next

        If QualCode <> "" Then
            QualCode = Mid(QualCode, 1, Len(QualCode) - 1)
            'CountQual = Split(QualCode, "|")
            QualName = Mid(QualName, 1, Len(Trim(QualName)) - 1)
            CType(grdReq.SelectedItem.Cells(8).Controls(1), TextBox).Text = QualName
            grdReq.SelectedItem.Cells(9).Text = QualCode
        End If
        LblDspl.Text = ""
        TrQual.Style.Item("display") = "none"
    End Sub

#End Region

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub

    Sub SendMail()
        Dim Id1, Id2, Strsql, Subject, Body, Name As String
        Dim dt As New DataTable
        Id1 = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(" Select Isnull(EmailId,IsNull(PEmailId,'')) From Hrdmast Where EMP_CODE='" & Session("Loginuser").userId & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)

        If Session("LoginUser").UserGroup = "HOD" Then
            Id2 = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand("select EmailId from JobReq where Type_code='HRADMIN'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
        Else
            Strsql = "Select Type_Code, EmailId  from JobReq where Emp_Code='" & Session("LoginUser").UserId & "'"
            Session("DalObj").GetSqlDatatable(dt, Strsql)
            If dt.Rows(0).Item("Type_Code") = "HRADMIN" Then
                Id2 = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand("select EmailId from JobReq where Type_code='VP'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
            ElseIf dt.Rows(0).Item("Type_Code") = "VP" Then
                id2 = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand("select EmailId from JobReq where Type_code='ADMINREC'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
            End If
        End If
        Name = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(" Select IsNull(EMP_NAME,'') From HRDMASTQRY Where EMP_CODE='" & Session("Loginuser").userId & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)

        If Id1 = "" Then
            LblErrMsg.Text = LblErrMsg.Text & "<BR>" & " Your Email Id is not defined, Unable to send mail."
            Exit Sub
        End If
        If Id2 = "" Then
            LblErrMsg.Text = LblErrMsg.Text & "<BR>" & " Destination Email Id is not defined, Unable to send mail."
            Exit Sub
        End If
        Dim DTSetup As New DataTable
        CType(Session("DalObj"), DAL.DataLayer).GetSqlDataTable(DTSetup, " Select Subj, Message From EmailReminderSetup Where FormKey=7 and Active = 1")

        If DTSetup.Rows.Count <> 0 Then
            Subject = Chk(DTSetup.Rows(0).Item("Subj"))
            Subject = Replace(Subject, "@EMPNAME", Name)
            Body = Chk(DTSetup.Rows(0).Item("Message"))
            'Body = Replace(Body, "@Days", Days)
            'Body = Replace(Body, "@REASON", IIf(txtReason.Text.Trim = "", "Not Defined", txtReason.Text.Trim))
            'Body = Replace(Body, "@CONTACT", IIf(TxtContAdd.Text.Trim = "", "Not Defined", TxtContAdd.Text.Trim))
        Else
            LblErrMsg.Text = LblErrMsg.Text & "<BR>" & " Email Reminder is not defined for Job Requisition, Unable to send mail."
            Exit Sub
        End If

        Dim MyMail As New Mail.MailMessage, SmtpServer As String
        SmtpServer = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand("Select isnull(PLACE,'') From COMPMAST", , DAL.DataLayer.ExecutionType.ExecuteScalar)

        MyMail.From = Id1
        MyMail.To = Id2
        MyMail.Subject = Subject
        MyMail.Body = Body
        Mail.SmtpMail.SmtpServer = SmtpServer.Trim.ToString
        ' Mail.SmtpMail.SmtpServer = "Localhost"
        Mail.SmtpMail.Send(MyMail)
    End Sub
End Class
