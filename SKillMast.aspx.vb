Partial Class SKillMast
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
        'Put user code to initialize the page here
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If
        FillSkillTree()
        Try

            If Not IsPostBack Then
                '****************       by Ravi
                Dim SrNo As String
                SrNo = Request.QueryString.Item("SrNo") + Request.QueryString.Item("MODULEID")
                If Not IsPostBack Then
                    Dim bSuccess As Boolean
                    Select Case CheckRight(SrNo)
                        Case MdlHRMS.AccessType.FullAccess, MdlHRMS.AccessType.SuperUser
                            bSuccess = True
                        Case MdlHRMS.AccessType.ReadonlyAccess, MdlHRMS.AccessType.Restricted
                            bSuccess = False
                    End Select
                    cmdSave.Visible = bSuccess
                    '****************
                End If
                btnNew_Click(sender, Nothing)
                Session("BalObj").FillCombo(cmbCode, "Select Skill_Code, Skill_Name from SkillMast", True)
                Session("BalObj").FillCombo(cmbHgroup, "select Skill_Code, Skill_Name from skillmast where Skill_type='PG'", True)
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (Page_Load)")
        End Try

    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNew.Click
        Try
            LblErrMsg.Text = ""
            ClearAll(Me)
            CmbType.SelectedIndex = 0
            cmbCode.Visible = False
            TxtCode.Visible = True
            TxtCode.Text = Session("BalObj").GetNextNumber("SkillMast", "Skill_Code")
            TxtCode.Text = TxtCode.Text.PadLeft(6, "0")
            TxtCode.ToolTip = TxtCode.Text
            ViewState("Action") = "ADDNEW"
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (btnNew_Click)")
        End Try
    End Sub
    Private Sub btnList_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnList.Click
        LblErrMsg.Text = ""
        cmbCode.Visible = True
        TxtCode.Visible = False
        btnList.Visible = False
        btnNew.Visible = False
    End Sub

    Private Sub cmbCode_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCode.SelectedIndexChanged
        LblErrMsg.Text = ""
        TxtCode.Visible = True
        cmbCode.Visible = False
        btnNew.Visible = True
        btnList.Visible = True
        TxtCode.ToolTip = cmbCode.SelectedValue
        TxtCode.Text = TxtCode.ToolTip
        DisplayRecords()
        ViewState("Action") = "MODIFY"
    End Sub

    Private Sub CmbType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbType.SelectedIndexChanged
        Try
            LblErrMsg.Text = ""
            Select Case CmbType.SelectedIndex
                Case 0
                    SetMsg(LblErrMsg, "Please Select The Type.")
                Case 1
                    cmbHgroup.Enabled = False
                    cmbUgroup.Enabled = False
                Case 2
                    cmbHgroup.Enabled = True
                    cmbUgroup.Enabled = False
                Case 3
                    cmbHgroup.Enabled = True
                    cmbUgroup.Enabled = True
            End Select

        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & "CmbType_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            Dim SqlStr As String
            If Not IsValidate() Then Exit Sub
            If ViewState("Action") = "ADDNEW" Then
                Select Case CmbType.SelectedIndex
                    Case 1
                        SqlStr = "Insert into SkillMast(Skill_Code, Skill_Name,Skill_Grp, Skill_Type,Skill_Scale) values('" & Chk(TxtCode.Text) & "','" & Chk(TxtDesc.Text) & "','*','PG','R')"
                        Session("DalObj").ExecuteCommand(SqlStr)
                    Case 2
                        SqlStr = "insert into SkillMast(Skill_Code, Skill_Name,Skill_Grp, Skill_Type,Skill_Scale) Values('" & Chk(TxtCode.Text) & "','" & Chk(TxtDesc.Text) & "','" & Chk(cmbHgroup.SelectedValue) & "','KG','R')"
                        Session("DalObj").ExecuteCommand(SqlStr)
                    Case 3
                        SqlStr = "insert into SkillMast(Skill_Code, Skill_Name,Skill_Grp, Skill_Type,Skill_Scale) Values('" & Chk(TxtCode.Text) & "','" & Chk(TxtDesc.Text) & "','" & Chk(cmbUgroup.SelectedValue) & "','KS','R')"
                        Session("DalObj").ExecuteCommand(SqlStr)
                End Select
                SetMsg(LblErrMsg, "Record Has Been Saved Successfully.")
                ClearAll(Me)
                btnNew_Click(sender, Nothing)
            End If
            If ViewState("Action") = "MODIFY" Then
                Select Case CmbType.SelectedIndex
                    Case 1
                        SqlStr = "Update SkillMast Set Skill_Name= '" & Chk(TxtDesc.Text) & "' Where Skill_Code= '" & Chk(TxtCode.Text) & "'"
                        Session("DalObj").ExecuteCommand(SqlStr)
                    Case 2
                        SqlStr = "Update SkillMast Set Skill_Name= '" & Chk(TxtDesc.Text) & "', Skill_Grp='" & Chk(cmbHgroup.SelectedValue) & "' where Skill_Code='" & Chk(TxtCode.Text) & "'"
                        Session("DalObj").ExecuteCommand(SqlStr)
                    Case 3
                        SqlStr = "Update SkillMast Set Skill_Name= '" & Chk(TxtDesc.Text) & "', Skill_Grp='" & Chk(cmbUgroup.SelectedValue) & "'  where Skill_Code='" & Chk(TxtCode.Text) & "'"
                        Session("DalObj").ExecuteCommand(SqlStr)
                End Select
                ClearAll(Me)
                btnNew_Click(sender, Nothing)
                Session("BalObj").FillCombo(cmbCode, "Select Skill_Code, Skill_Name from SkillMast", True)
                SetMsg(LblErrMsg, "Record Has Been Modified Successfully.")
            End If

        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & "cmdSave_Click")
        End Try
    End Sub
    Function IsValidate() As Boolean
        Try
            Dim cnt As Int16
            If Trim(TxtCode.Text) = "" Then
                SetMsg(LblErrMsg, "Skill Code Can't Be Left Blank.")
                Return False
            End If

            If Trim(TxtDesc.Text) = "" Then
                SetMsg(LblErrMsg, "Description Can't Be Left Blank.")
                Return False
            End If
            If Trim(CmbType.SelectedValue) = "" Then
                SetMsg(LblErrMsg, "Type Can't Be Left Blank.")
                Return False
            End If
            If Trim(CmbType.SelectedIndex) = 2 Then
                SetMsg(LblErrMsg, "Header Group Can't Be Left Blank.")
                Return False
            End If

            If Trim(CmbType.SelectedIndex) = 3 Then
                If cmbHgroup.SelectedValue = "" Then
                    SetMsg(LblErrMsg, "Header Group Can't Be Left Blank.")
                    Return False
                Else
                    If cmbUgroup.SelectedValue = "" Then
                        SetMsg(LblErrMsg, "Under Group Can't Be Left Blank.")
                        Return False
                    End If
                End If
            End If
            Return True
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Validate Records")
        End Try
    End Function

    Private Sub cmbHgroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbHgroup.SelectedIndexChanged
        Try
            Session("BalObj").FillCombo(cmbUgroup, "select Skill_Code, Skill_Name from skillmast where Skill_type='KG' and Skill_Grp='" & Chk(cmbHgroup.SelectedValue) & "'", True)
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & "cmbHgroup_SelectedIndexChanged")
        End Try
    End Sub

#Region "   Display Record "
    Sub DisplayRecords()
        Try
            Dim dt As New DataTable
            Dim SqlStr As String

            SqlStr = " Select * from SkillMast where Skill_Code='" & Chk(TxtCode.Text) & "'"
            Session("DalObj").GetSqlDataTable(dt, SqlStr)
            If dt.Rows.Count <> 0 Then
                If dt.Rows(0).Item("Skill_Type") = "PG" Then
                    CmbType.SelectedIndex = 1
                    TxtDesc.Text = dt.Rows(0).Item("Skill_Name")
                    cmbHgroup.Enabled = False
                    cmbUgroup.Enabled = False
                End If
                If dt.Rows(0).Item("Skill_Type") = "KG" Then
                    CmbType.SelectedIndex = 2
                    TxtDesc.Text = dt.Rows(0).Item("Skill_Name")
                    cmbHgroup.Enabled = True
                    cmbHgroup.SelectedValue = dt.Rows(0).Item("Skill_Grp")
                    cmbUgroup.Enabled = False
                End If
                If dt.Rows(0).Item("Skill_Type") = "KS" Then
                    CmbType.SelectedIndex = 3
                    TxtDesc.Text = dt.Rows(0).Item("Skill_Name")
                    cmbHgroup.Enabled = True
                    cmbUgroup.Enabled = True
                    Dim Tmp As String, Temp As New DataTable
                    Tmp = dt.Rows(0).Item("Skill_Grp")
                    SqlStr = "SELECT SKILL_GRP from SKILLMAST where Skill_Code= '" & Chk(Tmp) & "' "
                    Session("DalObj").GetSqlDatatable(Temp, SqlStr)
                    cmbHgroup.SelectedValue = Temp.Rows(0).Item("Skill_Grp")
                    Session("BalObj").FillCombo(cmbUgroup, "select Skill_Code, Skill_Name from skillmast where Skill_type='KG' and Skill_Grp='" & Chk(Temp.Rows(0).Item("Skill_Grp")) & "'", True)
                    cmbUgroup.SelectedValue = dt.Rows(0).Item("Skill_Grp")
                End If
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & "Display Record")
        End Try
    End Sub
#End Region

    Private Sub cmdClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub

#Region " Fill Skill Tree "
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
            SetMsg(LblErrMsg, ex.Message & " : " & "Fill Child TreeNode")
        End Try
    End Function
#End Region

End Class
