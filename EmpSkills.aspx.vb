Partial Class EmpSkills
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

    Dim SkillCode(), SkillRate() As String
    Dim DtTemp As DataTable

#Region "  On Load  "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Server.GetLastError Is Nothing Then SetMsg(LblErrMsg, "")
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If
        Try
            'By Ravi 17 nov 2006
            Dim SrNo As String
            SrNo = Request.QueryString.Item("SrNo")
            If Not IsPostBack Then
                Dim bSuccess As Boolean
                Select Case CheckRight(SrNo)
                    Case MdlHRMS.AccessType.FullAccess, MdlHRMS.AccessType.SuperUser
                        bSuccess = True
                    Case MdlHRMS.AccessType.ReadonlyAccess, MdlHRMS.AccessType.Restricted
                        bSuccess = False
                End Select
                cmdSave.Enabled = bSuccess

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
            '            cmdSave.Visible = False
            '        End If
            '    Else
            '        txtEM_CD.ReadOnly = True
            '        cmdSave.Visible = False
            '        SetMsg(LblRights, "You are not authorised to view data on this page.")
            '        'Response.Write("<BR><center><B></B></center>")
            '        Exit Sub
            '    End If
            'End If

            'If Session("LoginUser").UserGroup <> "ADMIN" Then
            '    txtEM_CD.ReadOnly = True
            '    'EnableDisable(False, Me)
            '    cmdSave.Visible = False
            'Else
            '    txtEM_CD.ReadOnly = False
            'End If
            'ViewState("TblSkill") = TblSkills
            'CreateTreeView(ViewState("TblSkill"), MdlHRMS.TreeViewType.Skills, True, Session("LoginUser").ConnectString)

            FillSkillTree()
            If Not IsPostBack Then
                txtEM_CD.Text = Session("EM_CD")
                txtEM_CD_TextChanged(sender, e)
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (Page_Load) ")
        End Try


    End Sub
    Sub BlankRecords()
        Try
            Dim SqlStr As String, i As Int16
            SqlStr = " Select Skill_Code from SkillMast where Skill_Type='KS'and Skill_Scale='R'"

            DtTemp = New DataTable
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
            SetMsg(LblErrMsg, ex.Message & " : " & "Blank Records")
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
            SetMsg(LblErrMsg, ex.Message & " : " & "Fill Parent TreeNode")
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
                        cmb.Style.Item("width") = "195px"
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
            SetMsg(LblErrMsg, ex.Message & " : " & "Fill Child TreeNode")
        End Try
    End Function

#End Region

    Sub DisplayRecords(ByVal Code As String)
        Try
            Dim SqlStr As String, i As Int16
            BlankRecords()

            'By Ravi 17 nov 2006
            Dim SrNo As Int16
            SrNo = Request.QueryString.Item("SrNo") + Request.QueryString.Item("MODULEID")

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
            '----------------------------------

            DtTemp = New DataTable
            SqlStr = " Select Skill_Code, Skill_Rate from HrdSkills Inner Join HrdMast On HrdSkills.Emp_Code = HrdMast.Emp_Code where HrdSkills.Emp_Code = '" & Code & "'" & Session("UserCodes")
            Session("DalObj").GetSqlDataTable(DtTemp, SqlStr)
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
            SetMsg(LblErrMsg, ex.Message & " : " & "Display Records")
        End Try
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("EmpExplorer.aspx?SrNo=61")
    End Sub

#Region "  Save Records  "

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If ViewState("Value") = False Then Exit Sub
        Dim Tran As SqlClient.SqlTransaction
        Try
            Dim SqlStr As String, i As Int16

            CountSkills()
            Tran = Session("DalObj").StartTransaction("Save")

            '=================Insert Training Skills=====================

            SqlStr = " Delete From HRDSKILLS Where Emp_Code = '" & Chk(txtEM_CD.Text) & "'"
            Session("DalObj").ExecuteCommand(SqlStr, Tran)

            If Not IsNothing(SkillCode) Then
                For i = 0 To SkillCode.Length - 1
                    SqlStr = " Insert HRDSKILLS (Emp_Code, Skill_Code, Skill_Rate) Values ('" & _
                                         Chk(txtEM_CD.Text) & "', '" & _
                                         Chk(SkillCode(i)) & "', '" & _
                                         Chk(SkillRate(i)) & "' )"

                    Session("DalObj").ExecuteCommand(SqlStr, Tran)
                Next
            End If
            Tran.Commit()
            DisplayRecords(txtEM_CD.Text)
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Save Records")
            Tran.Rollback()
        Finally
            If Not IsNothing(Tran) Then Tran.Dispose()
        End Try
    End Sub


    Public Sub CountSkills()
        Try
            Dim i As Int16, StrCode, StrRate As String

            DtTemp = New DataTable
            Session("DalObj").GetSqlDataTable(DtTemp, "Select * from SKILLMAST Where SKILL_SCALE='R' and SKILL_TYPE='KS'")

            For i = 0 To DtTemp.Rows.Count - 1
                With DtTemp.Rows(i)
                    If Not Session("TblSkill").FindControl("Chk" & .Item("Skill_Code")) Is Nothing Then
                        If CType(Session("TblSkill").FindControl("Chk" & .Item("Skill_Code")), HtmlInputCheckBox).Checked = True Then
                            StrCode = StrCode & .Item("Skill_Code") & "|"
                            StrRate = StrRate & CType(Session("TblSkill").FindControl("cmb" & .Item("Skill_Code")), HtmlSelect).Value & "|"
                        End If
                    End If
                End With
            Next

            If StrCode <> "" Then
                SkillCode = Split(Mid(StrCode, 1, Len(StrCode) - 1), "|")
                SkillRate = Split(Mid(StrRate, 1, Len(StrRate) - 1), "|")
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message)
        End Try
    End Sub

#End Region

    Private Sub txtEM_CD_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEM_CD.TextChanged
        Try
            If Trim(txtEM_CD.Text) <> "" Then Session("EM_CD") = Trim(txtEM_CD.Text)
            BlankRecords()

            Dim EmpName As Object
            EmpName = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(" Select Emp_Name From HrdMastQry Where Emp_Code = '" & Chk(txtEM_CD.Text) & "'" & Session("UserCodes"), , DAL.DataLayer.ExecutionType.ExecuteScalar)

            If (Not IsDBNull(EmpName)) And (Not IsNothing(EmpName)) Then
                LblName.Text = EmpName
                ViewState("Value") = True
            Else
                LblName.Text = ""
                Dim Code As Object
                Code = Session("DalObj").ExecuteCommand("Select Emp_Code From HrdMastQry Where Emp_Code = '" & Chk(txtEM_CD.Text) & "'" & Session("UserCodes"), , DAL.DataLayer.ExecutionType.ExecuteScalar)

                If (Not IsDBNull(Code)) And (Not IsNothing(Code)) Then
                    'By Ravi
                    ' SetMsg(LblErrMsg, "This Employee Code Exist For Other Location.")
                    SetMsg(LblErrMsg, "Access is dinied because ,This Employee Exist For Other Location.")
                End If
                ViewState("Value") = False
            End If
            DisplayRecords(txtEM_CD.Text)
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Code Changed")
        End Try
    End Sub
    Private Sub BtnFirst_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFirst.ServerClick
        txtEM_CD.Text = Chk(Session("DalObj").ExecuteCommand("SELECT DISTINCT TOP 1 EMP_CODE FROM HRDMAST WHERE 1=1 " & Session("UserCodes") & " Order By Emp_Code ", DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
        txtEM_CD_TextChanged(sender, e)

    End Sub

    Private Sub BtnPre_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPre.ServerClick
        txtEM_CD.Text = Chk(Session("DalObj").ExecuteCommand("SELECT DISTINCT TOP 1 EMP_CODE FROM HRDMAST WHERE EMP_CODE < '" & Chk(txtEM_CD.Text) & "' " & Session("UserCodes") & " Order By Emp_Code DESC ", DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
        txtEM_CD_TextChanged(sender, e)
    End Sub

    Private Sub BtnNext_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNext.ServerClick
        txtEM_CD.Text = Chk(Session("DalObj").ExecuteCommand("SELECT DISTINCT TOP 1 EMP_CODE FROM HRDMAST WHERE EMP_CODE > '" & Chk(txtEM_CD.Text) & "' " & Session("UserCodes") & " Order By Emp_Code ", DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
        txtEM_CD_TextChanged(sender, e)
    End Sub

    Private Sub BtnLast_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLast.ServerClick
        txtEM_CD.Text = Chk(Session("DalObj").ExecuteCommand("SELECT DISTINCT TOP 1 EMP_CODE FROM HRDMAST WHERE 1=1 " & Session("UserCodes") & " Order By Emp_Code DESC", DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
        txtEM_CD_TextChanged(sender, e)
    End Sub

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
        Session("EM_CD") = Trim(txtEM_CD.Text)
    End Sub
End Class
