Partial Class TrainerMast
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents CmbDesg As System.Web.UI.WebControls.DropDownList
    Protected WithEvents CmbDept As System.Web.UI.WebControls.DropDownList
    Protected WithEvents CmbLoc As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim b As BAL.BLayer
    Dim DtTemp As DataTable
    Dim SkillCode(), SkillRate() As String

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If

        FillSkillTree()
        If Not IsPostBack Then
            Session("BalObj").FillCombo(cmbEmp, "Select Emp_Code, Emp_Name as EName from HrdMastQry where  Ltype=1 " & Session("UserCodes") & " Order By FNAME", True)
            Session("BalObj").FillCombo(cmbCode, "SELECT Trainer_Code, Trainer_Name FROM TRAINERMAST ORDER BY TRAINER_NAME", True)
            RdExternal.Checked = True
            cmbEmp.Style.Item("display") = "none"
            btnNew_Click(sender, Nothing)
        End If
    End Sub

#Region "    New Records    "

    '============================Add New Records==================

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNew.Click
        chkDelete.Enabled = False
        BlankRecords()
        TxtCode.Text = Session("BalObj").GetNextNumber("TRAINERMAST", "TRAINER_CODE")
        TxtCode.Text = TxtCode.Text.PadLeft(4, "0")
        DisplayRecords(Chk(TxtCode.Text))
        ViewState("Status") = "ADDNEW"
    End Sub

    '============================Clear Records==================
    Sub BlankRecords()
        Try
            Dim i As Int16
            TxtDescription.Text = ""
            cmbEmp.SelectedValue = ""
            TxtEducation.Text = ""
            TxtOrganisation.Text = ""
            TxtDesg.Text = ""
            TxtDept.Text = ""
            TxtLoc.Text = ""
            TxtPhone.Text = ""
            DtTemp = New DataTable
            chkDelete.Checked = False
            Session("DalObj").GetSqlDataTable(DtTemp, " Select Skill_Code from SkillMast where Skill_Type='KS'and Skill_Scale='R'")
            If DtTemp.Rows.Count = 0 Then Exit Sub

            For i = 0 To DtTemp.Rows.Count - 1
                With DtTemp.Rows(i)
                    If Not Session("TblSkill").FindControl("Chk" & .Item("Skill_Code")) Is Nothing Then
                        CType(Session("TblSkill").FindControl("Chk" & .Item("Skill_Code")), HtmlInputCheckBox).Checked = False
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

#Region "   Display Employee Records    "

    '============================On Employee Combo Changed==================

    Private Sub cmbEmp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbEmp.SelectedIndexChanged
        Try
            If cmbEmp.SelectedValue = "" Then Exit Sub
            DisplayEmpRecords(Chk(cmbEmp.SelectedValue))
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message)
        End Try
    End Sub

    '============================Display Employee Records==================

    Sub DisplayEmpRecords(ByVal Code As String)
        Try
            Dim SqlStr As String, dt As New DataTable, i As Int16
            SqlStr = " Select Hm.EMP_CODE, QM.QUAL_NAME,HM.DSG_NAME, HM.DEPT_NAME,HM.LOC_NAME, HM.MPHONE, HM.COMP_NAME  from HrdMastQry HM inner Join HrdQual HQ on HM.Emp_Code = HQ.Emp_Code Inner Join QUALMAST QM on HQ.Qual_Code = QM.Qual_Code Where HM.EMP_CODE='" & Code & "'"
            Session("DalObj").GetSqlDataTable(dt, SqlStr)
            If dt.Rows.Count = 0 Then Exit Sub
            SqlStr = ""
            For i = 0 To dt.Rows.Count - 1
                SqlStr = SqlStr & dt.Rows(i).Item("Qual_Name") & ", "
            Next
            SqlStr = Mid(SqlStr, 1, Len(Trim(SqlStr)) - 1)
            TxtEducation.Text = SqlStr
            TxtOrganisation.Text = dt.Rows(0).Item("COMP_NAME")
            TxtDesg.Text = dt.Rows(0).Item("DSG_NAME")
            TxtDept.Text = dt.Rows(0).Item("DEPT_NAME")
            TxtLoc.Text = dt.Rows(0).Item("LOC_NAME")
            TxtPhone.Text = dt.Rows(0).Item("MPHONE")

        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message)
        End Try
    End Sub

#End Region

#Region "    Display Records     "

    '============================On Display Button Click==================

    Private Sub btnList_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnList.Click
        LblErrMsg.Text = ""
        TxtCode.Visible = False
        cmbCode.Visible = True
        btnList.Visible = False
        btnNew.Visible = False
    End Sub

    '============================On Display Combo Changed==================

    Private Sub cmbCode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCode.SelectedIndexChanged
        Try
            If cmbCode.SelectedValue = "" Then Exit Sub
            TxtCode.Visible = True
            TxtCode.Text = Chk(cmbCode.SelectedValue)
            btnList.Visible = True
            btnNew.Visible = True
            cmbCode.Visible = False
            BlankRecords()
            DisplayRecords(Chk(TxtCode.Text))
            ViewState("Status") = "MODIFY"
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message)
        End Try
    End Sub

    '============================On Code Text Changed==================

    Private Sub TxtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCode.TextChanged
        Try
            LblErrMsg.Text = ""
            BlankRecords()
            DisplayRecords(Chk(TxtCode.Text))
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Code Changed")
        End Try
    End Sub

    '============================Display Trainer Records==================

    Sub DisplayRecords(ByVal Code As String)
        Try
            Dim i As Int16, SqlStr As String
            DtTemp = New DataTable
            SqlStr = "SELECT Trainer_Type, Trainer_Name, Trainer_Edu, Trainer_Org,Trainer_Desg,Trainer_Dept,Trainer_Loc,Trainer_Phone, Emp_Code From TRAINERMAST Where Trainer_Code='" & Code & "'"
            Session("DalObj").GetSqlDataTable(DtTemp, SqlStr)
            If DtTemp.Rows.Count = 0 Then Exit Sub
            chkDelete.Enabled = True
            TxtEducation.Text = Chk(DtTemp.Rows(0).Item("Trainer_Edu"))
            TxtOrganisation.Text = Chk(DtTemp.Rows(0).Item("Trainer_Org"))
            TxtDesg.Text = Chk(DtTemp.Rows(0).Item("Trainer_Desg"))
            TxtDept.Text = Chk(DtTemp.Rows(0).Item("Trainer_Dept"))
            TxtLoc.Text = Chk(DtTemp.Rows(0).Item("Trainer_Loc"))
            TxtPhone.Text = Chk(DtTemp.Rows(0).Item("Trainer_Phone"))
            If UCase(DtTemp.Rows(0).Item("Trainer_Type")) = UCase("External") Then
                RdInternal.Checked = False
                RdExternal.Checked = True
                cmbEmp.Style.Item("display") = "none"
                TxtDescription.Style.Item("display") = "block"
                TxtDescription.Text = DtTemp.Rows(0).Item("Trainer_Name")
            Else
                RdExternal.Checked = False
                RdInternal.Checked = True
                TxtDescription.Style.Item("display") = "none"
                cmbEmp.Style.Item("display") = "block"
                cmbEmp.SelectedValue = DtTemp.Rows(0).Item("Emp_Code")
            End If

            DtTemp = New DataTable
            SqlStr = "SELECT Trainer_Code, Skill_Code, Skill_Rate_To From TRAINERSKILLS Where Trainer_Code='" & Code & "'"
            Session("DalObj").GetSqlDataTable(DtTemp, SqlStr)
            If DtTemp.Rows.Count = 0 Then Exit Sub

            For i = 0 To DtTemp.Rows.Count - 1
                With DtTemp.Rows(i)
                    If Not Session("TblSkill").FindControl("Chk" & .Item("Skill_Code")) Is Nothing Then
                        CType(Session("TblSkill").FindControl("Chk" & .Item("Skill_Code")), HtmlInputCheckBox).Checked = True
                    End If
                End With
            Next
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Display Records")
        End Try
    End Sub

#End Region

#Region "   Validate & Save Records      "

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim Tran As SqlClient.SqlTransaction
        Try
            Dim StrQuery As String, i As Int16
            CountSkills()
            Tran = Session("DalObj").StartTransaction("Save")

            If ViewState("Status") = "ADDNEW" Then

                '==================Insert Trainer Mast====================

                StrQuery = " Insert TRAINERMAST (Trainer_Code, Trainer_Name, Trainer_type, Emp_Code, Trainer_Edu, Trainer_Org, Trainer_Desg, Trainer_Dept, Trainer_Loc, Trainer_Phone) Values ('" & _
                           Chk(TxtCode.Text) & "', '"

                If RdExternal.Checked = True Then
                    StrQuery = StrQuery & Chk(TxtDescription.Text) & "' , 'External', Null, '"
                Else
                    StrQuery = StrQuery & Chk(cmbEmp.SelectedItem.Text) & "' , 'Internal', '" & Chk(cmbEmp.SelectedValue) & "', '"
                End If

                StrQuery = StrQuery & Chk(TxtEducation.Text) & "', '" & Chk(TxtOrganisation.Text) & "', '"
                StrQuery = StrQuery & Chk(TxtDesg.Text) & "', '" & Chk(TxtDept.Text) & "', '" & Chk(TxtLoc.Text) & "', '" & Chk(TxtPhone.Text) & "')"
                Session("DalObj").ExecuteCommand(StrQuery, Tran)

                '==================Insert Trainer Skills====================

                If Not IsNothing(SkillCode) Then
                    For i = 0 To SkillCode.Length - 1
                        StrQuery = " Insert TRAINERSKILLS (Trainer_Code, Skill_Code) Values ('" & _
                                                               Chk(TxtCode.Text) & "', '" & _
                                                               Chk(SkillCode(i)) & "' )"

                        Session("DalObj").ExecuteCommand(StrQuery, Tran)
                    Next
                End If

            ElseIf ViewState("Status") = "MODIFY" Then

                '==================Updating Trainer Mast====================

                StrQuery = " Update TRAINERMAST Set Trainer_Code = '" & Chk(TxtCode.Text) & "', "

                If RdExternal.Checked = True Then
                    StrQuery = StrQuery & " Trainer_Name = '" & Chk(TxtDescription.Text) & "', Trainer_Type = 'External', Emp_Code = Null, Trainer_Desg = '" & Chk(TxtDesg.Text) & "', Trainer_Dept = '" & Chk(TxtDept.Text) & "', Trainer_Phone = '" & Chk(TxtPhone.Text) & "', Trainer_Loc = '" & Chk(TxtLoc.Text) & "', "
                Else
                    StrQuery = StrQuery & " Trainer_Name = '" & Chk(cmbEmp.SelectedItem.Text) & "', Trainer_Type = 'Internal', Emp_Code = '" & Chk(cmbEmp.SelectedValue) & "', Trainer_Desg = '" & Chk(TxtDesg.Text) & "', Trainer_Dept = '" & Chk(TxtDept.Text) & "', Trainer_Loc = '" & Chk(TxtLoc.Text) & "', "

                End If

                StrQuery = StrQuery & " Trainer_Edu = '" & Chk(TxtEducation.Text) & "', Trainer_Org = '" & Chk(TxtOrganisation.Text) & "' Where Trainer_Code='" & Chk(TxtCode.Text) & "'"

                Session("DalObj").ExecuteCommand(StrQuery, Tran)


                '=================================Delete Trainer======================

                If chkDelete.Checked = True Then
                    Session("DalObj").ExecuteCommand("Delete From TRAINERMAST Where Trainer_Code ='" & Chk(TxtCode.Text) & "'", Tran)
                    Session("DalObj").ExecuteCommand("Delete From TRAINERSKILLS Where Trainer_Code ='" & Chk(TxtCode.Text) & "'", Tran)
                End If



                '==================Updating Trainer Skills====================


                Session("DalObj").ExecuteCommand("Delete From TRAINERSKILLS Where Trainer_Code ='" & Chk(TxtCode.Text) & "'", Tran)


                If Not IsNothing(SkillCode) Then
                    For i = 0 To SkillCode.Length - 1
                        StrQuery = " Insert TRAINERSKILLS (Trainer_Code, Skill_Code) Values ('" & _
                                             Chk(TxtCode.Text) & "', '" & _
                                             Chk(SkillCode(i)) & "' )"

                        Session("DalObj").ExecuteCommand(StrQuery, Tran)
                    Next
                End If


            End If
            Tran.Commit()
            btnNew_Click(sender, Nothing)
            Session("BalObj").FillCombo(cmbCode, "SELECT Trainer_Code, Trainer_Name FROM TRAINERMAST ORDER BY TRAINER_NAME", True)
            SetMsg(LblErrMsg, " Record Saved Success Fully")
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Save Records")
            Tran.Rollback()
        Finally
            If Not Tran Is Nothing Then Tran.Dispose()
        End Try
    End Sub

    '=====================Count Selected Skills=================

    Public Sub CountSkills()
        Try
            Dim i As Int16, StrCode As String

            DtTemp = New DataTable
            Session("DalObj").GetSqlDataTable(DtTemp, "Select * from SKILLMAST Where SKILL_SCALE='R' and SKILL_TYPE='KS'")
            For i = 0 To DtTemp.Rows.Count - 1
                With DtTemp.Rows(i)
                    If Not Session("TblSkill").FindControl("Chk" & .Item("Skill_Code")) Is Nothing Then
                        If CType(Session("TblSkill").FindControl("Chk" & .Item("Skill_Code")), HtmlInputCheckBox).Checked = True Then
                            StrCode = StrCode & .Item("Skill_Code") & "|"
                        End If
                    End If
                End With
            Next

            If StrCode <> "" Then
                SkillCode = Split(Mid(StrCode, 1, Len(StrCode) - 1), "|")
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Count Skills")
        End Try

    End Sub

    '=====================Validate Records=================

    Function IsValidate() As Boolean
        Try
            Dim Count As Int16
            If Chk(TxtCode.Text) = "" Then
                SetMsg(LblErrMsg, " Code can not be left blank.")
                Return False
            End If

            If Chk(TxtDescription.Text) = "" Then
                SetMsg(LblErrMsg, " Trainer Name can not be left blank.")
                Return False
            End If

            If ViewState("Status") <> "MODIFY" Then
                Count = Session("DalObj").ExecuteCommand(" Select Count(*) From TRAINERMAST Where Trainer_Code='" & Chk(TxtCode.Text) & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
                If Count <> 0 Then
                    SetMsg(LblErrMsg, " This Code already exist. Record Not Saved.")
                    Return False
                End If
            End If

            Return True
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Validate Records")
        End Try
    End Function
#End Region

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub
End Class
