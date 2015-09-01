Partial Class History

    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "
    'By Ravi on 7 dec 2007
    Dim StrSql As String
    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents TextStdRate As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim dtExp As New DataTable
    Dim cnt As Int16 = 1
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Server.GetLastError Is Nothing Then SetMsg(LblMsg, "")
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If

        Try
            'By Ravi 17 nov 2006
            Dim SrNo As Int16
            SrNo = Request.QueryString.Item("SrNo") + Request.QueryString.Item("MODULEID")
            If Not IsPostBack Then
                Dim bSuccess As Boolean
                Select Case CheckRight(SrNo)
                    Case MdlHRMS.AccessType.FullAccess, MdlHRMS.AccessType.SuperUser
                        bSuccess = True
                    Case MdlHRMS.AccessType.ReadonlyAccess
                        bSuccess = False
                    Case MdlHRMS.AccessType.Restricted
                        Response.Redirect(Request.UrlReferrer.ToString)
                        Exit Sub
                End Select
                cmdQualSave.Visible = bSuccess
                cmdAddQual.Visible = bSuccess
                cmdSaveExp.Visible = bSuccess
                cmdAddQual.Visible = bSuccess
                cmdAddExp.Visible = bSuccess


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
            '            cmdQualSave.Visible = False
            '            cmdAddExp.Visible = False
            '            cmdSaveExp.Visible = False
            '            cmdAddQual.Visible = False
            '        End If
            '    Else
            '        txtEM_CD.ReadOnly = True
            '        cmdQualSave.Visible = False
            '        cmdAddExp.Visible = False
            '        cmdSaveExp.Visible = False
            '        cmdAddQual.Visible = False
            '        SetMsg(LblRights, "You are not authorised to view data on this page.")
            '        'Response.Write("<BR><center><B></B></center>")
            '        Exit Sub
            '    End If
            'End If

            'If Session("LoginUser").UserGroup <> "ADMIN" Then
            '    txtEM_CD.ReadOnly = True
            '    'EnableDisable(False, Me)
            '    cmdQualSave.Visible = False
            '    cmdAddExp.Visible = False
            '    cmdSaveExp.Visible = False
            '    cmdAddQual.Visible = False
            'Else
            '    txtEM_CD.ReadOnly = False
            'End If
            If Not IsPostBack Then
                txtEM_CD.Text = Session("EM_CD")
                txtEM_CD_TextChanged(sender, e)
            End If
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (Page_Load) ")
        End Try
    End Sub

#Region " Qualfication Related Coding "

    Private Sub BindGridQual()
        Try
            'Edit By Ravi on 8 dec 2006
            If Trim(txtEM_CD.Text) <> "" Then

                '-----------------------
                ViewState("dtQual") = New DataTable
                Dim StrSql As String
                StrSql = "Select QualMast.Qual_Code,UnivMast.Univ_Code, " & _
                            " HrdQual.College,HrdQual.Place,HrdQual.Passing_Year,HrdQual.Marks_Per, " & _
                            " HrdQual.Grade,HrdQual.Subjects From HrdQual " & _
                            " Left Outer Join QualMast On QualMast.Qual_Code = HrdQual.Qual_Code " & _
                            " Left Outer Join UnivMast On UnivMast.Univ_Code = HrdQual.Univ_Code " & _
                            " Inner Join HrdMast On HrdQual.Emp_Code = HrdMast.Emp_Code " & _
                            " Where HrdQual.Emp_Code = '" & Chk(txtEM_CD.Text) & "'" & Session("UserCodes") & " Order By HrdQual.Passing_Year "

                'By Ravi 21 nov 2006
                StrSql = Replace(StrSql, "AND", "AND hrdmast.")
                '--------------------------------------------------------------
                Session("DalObj").GetSqlDataTable(ViewState("dtQual"), StrSql)
                GrdQual.DataSource = ViewState("dtQual")
                GrdQual.DataBind()
            Else
                Exit Sub
            End If

        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (BindGridQual)")
        Finally
        End Try
    End Sub

    Private Sub cmdAddQual_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddQual.Click
        Try
            LblMsg.Text = ""
            If GrdQual.Items.Count > 0 Then
                If Chk(CType(GrdQual.Items(GrdQual.Items.Count - 1).Controls(0).Controls(1), DropDownList).SelectedValue) <> "" Then
                    cmdQualSave_Click(sender, e)
                    BindGridQual()
                    Dim tmpTr As DataRow = ViewState("dtQual").NewRow()
                    ViewState("dtQual").Rows.Add(tmpTr)
                    GrdQual.DataSource = ViewState("dtQual")
                    GrdQual.DataBind()
                End If
            Else
                Dim tmpTr As DataRow = ViewState("dtQual").NewRow()
                ViewState("dtQual").Rows.Add(tmpTr)
                GrdQual.DataSource = ViewState("dtQual")
                GrdQual.DataBind()
            End If
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (cmdAddQual_Click)")
        End Try
    End Sub

    Private Sub cmdQualSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdQualSave.Click
        If ViewState("Value") = False Then Exit Sub
        Dim trnQual As SqlClient.SqlTransaction
        Try
            Dim cnt As Int16
            Dim strSQl As String
            trnQual = Session("DalObj").StartTransaction("Qual")
            strSQl = "Delete From HrdQual Where Emp_Code = '" & Chk(txtEM_CD.Text) & "'"
            Session("DalObj").ExecuteCommand(strSQl, trnQual)
            For cnt = 0 To GrdQual.Items.Count - 1
                If Chk(CType(GrdQual.Items(cnt).Controls(0).Controls(1), DropDownList).SelectedValue) <> "" Then
                    strSQl = " Insert InTo HrdQual " & _
                             " (Emp_Code,Qual_Code,Univ_Code,College,Place,Passing_Year,Marks_Per,Grade,Subjects) " & _
                             " Values " & _
                             " ('" & Chk(txtEM_CD.Text) & "','" & _
                             Chk(CType(GrdQual.Items(cnt).Controls(0).Controls(1), DropDownList).SelectedValue) & "','" & _
                             Chk(CType(GrdQual.Items(cnt).Controls(1).Controls(1), DropDownList).SelectedValue) & "','" & _
                             Chk(CType(GrdQual.Items(cnt).Controls(2).Controls(1), TextBox).Text) & "','" & _
                             Chk(CType(GrdQual.Items(cnt).Controls(3).Controls(1), TextBox).Text) & "','" & _
                             Chk(CType(GrdQual.Items(cnt).Controls(4).Controls(1), TextBox).Text) & "'," & _
                             ChkN(CType(GrdQual.Items(cnt).Controls(5).Controls(1), TextBox).Text) & ",'" & _
                             Chk(CType(GrdQual.Items(cnt).Controls(6).Controls(1), TextBox).Text) & "','" & _
                             Chk(CType(GrdQual.Items(cnt).Controls(7).Controls(1), TextBox).Text) & "')"
                    Session("DalObj").ExecuteCommand(strSQl, trnQual)
                End If
            Next
            trnQual.Commit()
            BindGridQual()
            'SetMsg(LblMsg, "Records Have Been Saved Successfully.")
        Catch ex As Exception
            trnQual.Rollback()
            SetMsg(LblMsg, ex.Message & " : (cmdQualSave_Click)")
        End Try
    End Sub

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
            SetMsg(LblMsg, ex.Message & " : (GrdQual_ItemCreated)")
        End Try
    End Sub

#End Region

#Region " Experience Related Coding "

    Private Sub BindGridExp()
        Try
            'Edit By Ravi on 8 dec 2006
            If Trim(txtEM_CD.Text) <> "" Then

                '-----------------------
                ViewState("dtExp") = New DataTable

                If Not Session("BalObj").ExistColumn("Dsg_Name", "HrdExp") Then
                    StrSql = " Alter Table HrdExp Add Dsg_Name Varchar(50)"
                    Session("DalObj").ExecuteCommand(StrSql)
                End If
                StrSql = " Select Right('0' + DateName(d,Exp_From),2) + '/' + Left(DateName(mm,Exp_From),3) + '/' + DateName(yy,Exp_From) As Exp_From, " & _
                         " Right('0' + DateName(d,Exp_To),2) + '/' + Left(DateName(mm,Exp_To),3) + '/' + DateName(yy,Exp_To) As Exp_To, " & _
                         " round(Exp_Years,2) as Exp_Years,Org_Name,DSG_Name,Drawn_Sal,LeavingReason, HrdExp.JobProfile From HrdExp " & _
                         " Inner Join HrdMast On HrdExp.Emp_Code = HrdMast.Emp_Code " & _
                         " Where HrdExp.Emp_Code = '" & Chk(txtEM_CD.Text) & "'" & Session("UserCodes") & " Order By Cast(HrdExp.Exp_From As SmallDateTime)"
                'By Ravi 21 nov 2006


                StrSql = Replace(StrSql, "AND", "AND hrdmast.")
                '--------------------------------------------------------------
                Session("DalObj").GetSqlDataTable(ViewState("dtExp"), StrSql)
                GrdExp.DataSource = ViewState("dtExp")
                GrdExp.DataBind()
            Else

            End If
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (BindGridExp)")
        Finally
        End Try
    End Sub
    Private Sub cmdAddExp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddExp.Click
        Try
            LblMsg.Text = ""
            If GrdExp.Items.Count > 0 Then
                If Chk(CType(GrdExp.Items(GrdExp.Items.Count - 1).Controls(0).Controls(1), TextBox).Text) <> "" Then
                    cmdSaveExp_Click(sender, e)
                    BindGridExp()
                    Dim tmpTr As DataRow = ViewState("dtExp").NewRow()
                    ViewState("dtExp").Rows.Add(tmpTr)
                    GrdExp.DataSource = ViewState("dtExp")
                    GrdExp.DataBind()
                End If
            Else
                Dim tmpTr As DataRow = ViewState("dtExp").NewRow()
                ViewState("dtExp").Rows.Add(tmpTr)
                GrdExp.DataSource = ViewState("dtExp")
                GrdExp.DataBind()
            End If
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (cmdAddExp_Click)")
        End Try
    End Sub

    Private Sub cmdSaveExp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSaveExp.Click
        If ViewState("Value") = False Then Exit Sub
        Dim trnExp As SqlClient.SqlTransaction
        Try
            Dim cnt As Int16
            Dim strSQl As String
            trnExp = Session("DalObj").StartTransaction("Exp")
            strSQl = "Delete From HrdExp Where Emp_Code = '" & Chk(txtEM_CD.Text) & "'"
            Session("DalObj").ExecuteCommand(strSQl, trnExp)
            For cnt = 0 To GrdExp.Items.Count - 1
                Dim expFrom As String = Chk(CType(GrdExp.Items(cnt).Controls(0).Controls(1), TextBox).Text)
                Dim expTo As String = Chk(CType(GrdExp.Items(cnt).Controls(1).Controls(1), TextBox).Text)
                If expFrom <> "" And expTo <> "" Then
                    Dim ExpYr As Double
                    Dim ExpMon As Double
                    ExpMon = DateDiff(DateInterval.Month, CDate(expFrom), CDate(expTo))
                    If ExpMon > 0 Then
                        ExpYr = ExpMon / 12
                        strSQl = " Insert InTo HrdExp " & _
                                 " (Emp_Code,Exp_From,Exp_To,Exp_Years,Org_Name,Dsg_Name,JobProfile,Drawn_Sal,LeavingReason )" & _
                                 " Values " & _
                                 " ('" & Chk(txtEM_CD.Text) & "','" & _
                                 expFrom & "','" & _
                                 expTo & " '," & _
                                 ExpYr & ",'" & _
                                 Chk(CType(GrdExp.Items(cnt).Controls(3).Controls(1), TextBox).Text) & "', '" & _
                                 Chk(CType(GrdExp.Items(cnt).Controls(4).Controls(1), TextBox).Text) & "', '" & _
                                 Chk(CType(GrdExp.Items(cnt).Controls(5).Controls(1), TextBox).Text) & " ', '" & _
                                 ChkN(CType(GrdExp.Items(cnt).Controls(6).Controls(1), TextBox).Text) & "', '" & _
                                 Chk(CType(GrdExp.Items(cnt).Controls(7).Controls(1), TextBox).Text) & "')"
                        Session("DalObj").ExecuteCommand(strSQl, trnExp)
                    End If
                End If
            Next
            trnExp.Commit()
            BindGridExp()
            'SetMsg(LblMsg, "Records Have Been Saved Successfully.")
        Catch ex As Exception
            trnExp.Rollback()
            SetMsg(LblMsg, ex.Message & " : (cmdSaveExp_Click)")
        End Try
    End Sub

#End Region

#Region " Others "

    Private Sub DisplayQual()
        Dim dtQual As New DataTable
        Try
            Dim StrSql As String = "Select QualMast.Qual_Name,UnivMast.Univ_Name, " & _
                                    " HrdQual.College,HrdQual.Place,HrdQual.Passing_Year,HrdQual.Marks_Per, " & _
                                    " HrdQual.Grade,HrdQual.Subjects From HrdQual  " & _
                                    " Left Outer Join QualMast On QualMast.Qual_Code = HrdQual.Qual_Code " & _
                                    " Left Outer Join UnivMast On UnivMast.Univ_Code = HrdQual.Univ_Code " & _
                                    " Where Emp_Code = '" & Chk(txtEM_CD.Text) & "'"
            Session("DalObj").GetSqlDataTable(dtQual, StrSql)

            Dim Cnt As Integer, Col As Integer
            For Cnt = 0 To dtQual.Rows.Count - 1
                Dim tmpTr As New HtmlTableRow
                For Col = 0 To TabQual.Controls(0).Controls.Count - 1
                    Dim tmpTd As New HtmlTableCell
                    Select Case Col
                        Case 0      'Qualification Masters
                            Dim tmpCmb As New DropDownList
                            'tmpCmb.ID = CType(TabQual.Controls(0).Controls(Cnt).Controls(0), LiteralControl).Text & Cnt
                            Session("BalObj").FillCombo(tmpCmb, "Select Qual_Code,Qual_Name From QualMast Order By Qual_Name")

                            tmpCmb.Items.Add("")
                            tmpCmb.Selecteditem.Text = Chk(dtQual.Rows(Cnt).Item(Col).ToString)
                            tmpCmb.Width = Unit.Percentage(100)
                            tmpTd.Controls.Add(tmpCmb)
                            tmpTr.Controls.Add(tmpTd)
                        Case 1       'University Masters
                            Dim tmpCmb As New DropDownList
                            'tmpCmb.ID = CType(TabQual.Controls(0).Controls(Cnt).Controls(0), LiteralControl).Text & Cnt
                            Session("BalObj").FillCombo(tmpCmb, "Select Univ_Code,Univ_Name From UnivMast Order By Univ_Name")
                            tmpCmb.Items.Add("")
                            tmpCmb.Selecteditem.Text = Chk(dtQual.Rows(Cnt).Item(Col).ToString)
                            tmpCmb.Width = Unit.Percentage(100)
                            tmpTd.Controls.Add(tmpCmb)
                            tmpTr.Controls.Add(tmpTd)
                        Case Else
                            Dim tmpText As New TextBox
                            'tmpText.ID = CType(TabQual.Controls(0).Controls(Cnt).Controls(0), LiteralControl).Text & Cnt
                            tmpText.CssClass = "TextBox"
                            tmpText.Text = Chk(dtQual.Rows(Cnt).Item(Col).ToString)
                            tmpText.Width = Unit.Percentage(100)
                            tmpTd.Controls.Add(tmpText)
                            tmpTr.Controls.Add(tmpTd)

                    End Select
                Next
                TabQual.Controls.Add(tmpTr)
            Next

        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (DisplayQual)")
        Finally
            dtQual.Dispose()
        End Try
    End Sub

    Private Sub GrdQual_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GrdQual.PreRender
        Try
            Dim I As Integer
            For I = 0 To GrdQual.DataSource.Rows.Count - 1
                If GrdQual.Controls.Count > 0 And I < CType(GrdQual.Controls(0), Table).Rows.Count Then
                    'Response.Write(CType(GrdQual.Controls(0), Table).Rows(I).Controls.Count())
                    'Response.Write("-----GrdQual_PreRender-----<br>")
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

    '''  ---------  GrdQual_ItemCreated
    '''Dim i As Single, j As Single, k As Single, l As Single, m As Single, n As Single
    '''cnt = cnt + 1
    '''For i = 0 To GrdQual.Controls.Count - 1
    '''    For j = 0 To GrdQual.Controls(i).Controls.Count - 1
    '''        For k = 0 To GrdQual.Controls(i).Controls(j).Controls.Count - 1
    '''            For l = 0 To GrdQual.Controls(i).Controls(j).Controls(k).Controls.Count - 1
    '''                If TypeOf GrdQual.Controls(i).Controls(j).Controls(k).Controls(l) Is DropDownList Then
    '''                    Dim dtQual As New DataTable
    '''                    Dim dtUniv As New DataTable
    '''                    If InStr(GrdQual.Controls(i).Controls(j).Controls(k).Controls(l).UniqueID, "cmbQual") Then
    '''                        '*********  Fill Combo Qual 
    '''                        Session("DalObj").GetSqlDataTable(dtQual, "Select Qual_Code,Qual_Name From QualMast Order By Qual_Name")
    '''                        CType(GrdQual.Controls(i).Controls(j).Controls(k).Controls(l), DropDownList).DataSource = dtQual
    '''                        CType(GrdQual.Controls(i).Controls(j).Controls(k).Controls(l), DropDownList).DataTextField = "Qual_Name"
    '''                        CType(GrdQual.Controls(i).Controls(j).Controls(k).Controls(l), DropDownList).DataValueField = "Qual_Code"
    '''                        CType(GrdQual.Controls(i).Controls(j).Controls(k).Controls(l), DropDownList).DataBind()
    '''                        CType(GrdQual.Controls(i).Controls(j).Controls(k).Controls(l), DropDownList).Items.Add("")
    '''                        CType(GrdQual.Controls(i).Controls(j).Controls(k).Controls(l), DropDownList).SelectedIndex = CType(GrdQual.Controls(i).Controls(j).Controls(k).Controls(l), DropDownList).Items.Count - 1
    '''                        Dim X As Int16
    '''                        For X = 0 To GrdQual.Items.Count - 1
    '''                            If Not IsDBNull(GrdQual.DataSource.rows(X).item("Qual_Code")) Then
    '''                                CType(GrdQual.Items(X).Controls(0).Controls(1), DropDownList).SelectedValue = GrdQual.DataSource.rows(X).item("Qual_Code")
    '''                            Else
    '''                                If CType(GrdQual.Items(X).Controls(0).Controls(1), DropDownList).Items.Contains(New ListItem("")) Then
    '''                                    CType(GrdQual.Items(X).Controls(0).Controls(1), DropDownList).SelectedValue = ""
    '''                                End If
    '''                            End If
    '''                        Next
    '''                    ElseIf InStr(GrdQual.Controls(i).Controls(j).Controls(k).Controls(l).UniqueID, "cmbUniv") Then
    '''                        '*********  Fill Combo Univ
    '''                        Session("DalObj").GetSqlDataTable(dtUniv, "Select Univ_Code,Univ_Name From UnivMast Order By Univ_Name")
    '''                        CType(GrdQual.Controls(i).Controls(j).Controls(k).Controls(l), DropDownList).DataSource = dtUniv
    '''                        CType(GrdQual.Controls(i).Controls(j).Controls(k).Controls(l), DropDownList).DataTextField = "Univ_Name"
    '''                        CType(GrdQual.Controls(i).Controls(j).Controls(k).Controls(l), DropDownList).DataValueField = "Univ_Code"
    '''                        CType(GrdQual.Controls(i).Controls(j).Controls(k).Controls(l), DropDownList).DataBind()
    '''                        CType(GrdQual.Controls(i).Controls(j).Controls(k).Controls(l), DropDownList).Items.Add("")
    '''                        CType(GrdQual.Controls(i).Controls(j).Controls(k).Controls(l), DropDownList).SelectedIndex = CType(GrdQual.Controls(i).Controls(j).Controls(k).Controls(l), DropDownList).Items.Count - 1
    '''                        Dim X As Int16
    '''                        For X = 0 To GrdQual.Items.Count - 1
    '''                            If Not IsDBNull(GrdQual.DataSource.rows(X).item("Univ_Code")) Then
    '''                                CType(GrdQual.Items(X).Controls(1).Controls(1), DropDownList).SelectedValue = GrdQual.DataSource.rows(X).item("Univ_Code")
    '''                            Else
    '''                                If CType(GrdQual.Items(X).Controls(0).Controls(1), DropDownList).Items.Contains(New ListItem("")) Then
    '''                                    CType(GrdQual.Items(X).Controls(1).Controls(1), DropDownList).SelectedValue = ""
    '''                                End If
    '''                            End If
    '''                        Next
    '''                    End If
    '''                    dtQual.Dispose()
    '''                    dtUniv.Dispose()
    '''                End If
    '''            Next
    '''        Next
    '''    Next
    '''Next


#End Region

    Private Sub txtEM_CD_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEM_CD.TextChanged
        Try
            If Trim(txtEM_CD.Text) <> "" Then Session("EM_CD") = Trim(txtEM_CD.Text)

            Dim EmpName As Object

            EmpName = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(" Select Emp_Name From HrdMastQry Where Emp_Code = '" & Chk(txtEM_CD.Text) & "'" & Session("UserCodes"), , DAL.DataLayer.ExecutionType.ExecuteScalar)
            If (Not IsDBNull(EmpName)) And (Not IsNothing(EmpName)) Then
                LblName.Text = EmpName
                ViewState("Value") = True
            Else
                Dim Code As Object
                'By Ravi
                'Code = Session("DalObj").ExecuteCommand("Select Emp_Code From HrdMastQry Where Emp_Code = '" & Chk(txtEM_CD.Text) & "'" & Session("UserCodes"), , DAL.DataLayer.ExecutionType.ExecuteScalar)
                Code = Session("DalObj").ExecuteCommand("Select Emp_Code From HrdMastQry Where Emp_Code = '" & Chk(txtEM_CD.Text) & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)

                If (Not IsDBNull(Code)) And (Not IsNothing(Code)) Then
                    SetMsg(LblMsg, "Access is dinied because ,This Employee Exist For Other Location.")
                    'Else
                    ' SetMsg(LblMsg, "This Employee does not exist .")
                End If

                'ViewState("Value") = False

                ViewState("Value") = False
                LblName.Text = ""
            End If

            BindGridExp()
            BindGridQual()
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (txtEM_CD_TextChanged) ")
        End Try
    End Sub

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub
    'By Ravi on 8 dec 2006
    Private Sub BtnFirst_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFirst.ServerClick
        Try
            'txtEM_CD.Text = Chk(Session("DalObj").ExecuteCommand("SELECT DISTINCT TOP 1 HRDEXP.EMP_CODE FROM HRDMAST INNER JOIN HRDEXP ON HRDMAST.EMP_CODE=HRDEXP.EMP_CODE WHERE 1=1 " & Session("UserCodes") & " Order By HRDEXP.Emp_Code ", DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
            txtEM_CD.Text = Chk(Session("DalObj").ExecuteCommand("SELECT DISTINCT TOP 1 EMP_CODE FROM HRDMAST WHERE 1=1 " & Session("UserCodes") & " Order By Emp_Code ", DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
            BindGridExp()
            BindGridQual()
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (BtnFirst_ServerClick)")

        End Try
    End Sub
    'By Ravi on 8 dec 2006
    Private Sub BtnPre_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPre.ServerClick
        Try
            ' txtEM_CD.Text = Chk(Session("DalObj").ExecuteCommand("SELECT DISTINCT TOP 1 HRDEXP.EMP_CODE FROM HRDMAST INNER JOIN HRDEXP ON HRDMAST.EMP_CODE=HRDEXP.EMP_CODE WHERE 1=1 " & Session("UserCodes") & " Order By HRDEXP.Emp_Code ", DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
            txtEM_CD.Text = Chk(Session("DalObj").ExecuteCommand("SELECT DISTINCT TOP 1 EMP_CODE FROM HRDMAST WHERE EMP_CODE < '" & Chk(txtEM_CD.Text) & "' " & Session("UserCodes") & " Order By Emp_Code DESC ", DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
            BindGridExp()
            BindGridQual()

        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (BtnPre_ServerClick)")
        End Try
    End Sub
    'By Ravi on 8 dec 2006
    Private Sub BtnNext_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNext.ServerClick
        Try
            'txtEM_CD.Text = Chk(Session("DalObj").ExecuteCommand("SELECT DISTINCT TOP 1 HRDEXP.EMP_CODE FROM HRDMAST INNER JOIN HRDEXP ON HRDMAST.EMP_CODE=HRDEXP.EMP_CODE WHERE HRDEXP.EMP_CODE > '" & Chk(txtEM_CD.Text) & "'" & Session("UserCodes") & " Order By HRDEXP.Emp_Code ", DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
            txtEM_CD.Text = Chk(Session("DalObj").ExecuteCommand("SELECT DISTINCT TOP 1 EMP_CODE FROM HRDMAST WHERE EMP_CODE > '" & Chk(txtEM_CD.Text) & "' " & Session("UserCodes") & " Order By Emp_Code ", DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
            BindGridExp()
            BindGridQual()

        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (BtnNext_ServerClick)")
        End Try
    End Sub
    'By Ravi on 8 dec 2006
    Private Sub BtnLast_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLast.ServerClick
        Try
            'txtEM_CD.Text = Chk(Session("DalObj").ExecuteCommand("SELECT DISTINCT TOP 1 HRDEXP.EMP_CODE FROM HRDMAST INNER JOIN HRDEXP ON HRDMAST.EMP_CODE=HRDEXP.EMP_CODE WHERE 1=1 " & Session("UserCodes") & " Order By HRDEXP.Emp_Code DESC", DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
            txtEM_CD.Text = Chk(Session("DalObj").ExecuteCommand("SELECT DISTINCT TOP 1 EMP_CODE FROM HRDMAST WHERE 1=1 " & Session("UserCodes") & " Order By Emp_Code DESC", DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
            BindGridExp()
            BindGridQual()
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (BtnLast_ServerClick)")
        End Try
    End Sub
    'By Ravi on 8 dec 2006
    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
        Session("EM_CD") = Trim(txtEM_CD.Text)
    End Sub
End Class
