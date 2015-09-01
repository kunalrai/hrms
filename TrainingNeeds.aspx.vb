Partial Class TrainingNeeds
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
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If

        FillSkillTree1()
        FillSkillTree2()
        If Not IsPostBack Then
            Try
                Session("BalObj").FillCombo(cmbCode, "Select Emp_Code, (isnull(fname,'') + ' ' +isnull(lname,'')) as EName from HrdMastQry Where Mngr_Code='" & Session("LoginUser").UserId & "' Order By FNAME", True)
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End If
    End Sub

#Region "    Fill Skill Tree  1 "

    Public Sub FillSkillTree1()
        Try
            Dim i As Int16, dt As New DataTable, str As String
            Dim TblRow As HtmlTableRow, RowCell1 As HtmlTableCell, RowCell2 As HtmlTableCell, Tbl As HtmlTable
            Session("TblSkill1") = New HtmlTable
            Session("TblSkill1") = TblSkills1
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
                    ChildNode1(.Item("Skill_Code"), Tbl)

                    RowCell2.Align = "Left"
                    RowCell2.VAlign = "Top"
                    RowCell2.InnerText = .Item("SKILL_NAME")
                    RowCell2.Style.Item("FONT-WEIGHT") = "bold"
                    RowCell2.Style.Item("FONT-SIZE") = "8pt"
                    RowCell2.Controls.Add(Tbl)

                    TblRow.Cells.Add(RowCell1)
                    TblRow.Cells.Add(RowCell2)
                    Session("TblSkill1").Rows.Add(TblRow)

                End With
            Next
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Fill Parent TreeNode1")
        End Try
    End Sub

    Public Function ChildNode1(ByVal CD As String, ByRef HtmlTbl As HtmlTable) As String
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

                        RowCell4.Align = "Left"
                        RowCell4.VAlign = "Top"
                        RowCell4.Controls.Add(cmb)

                    End If

                    '=================Third Cell===========================
                    Tbl.ID = "Tbl" & .Item("Skill_Code")
                    Tbl.CellPadding = 0
                    Tbl.CellSpacing = 3
                    Tbl.Style.Item("display") = "none"
                    ChildNode1(.Item("Skill_Code"), Tbl)

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
            SetMsg(LblErrMsg, ex.Message & " : " & "Fill Child TreeNode1")
        End Try
    End Function

#End Region

#Region "    Fill Skill Tree 2  "

    Public Sub FillSkillTree2()
        Try
            Dim i As Int16, dt As New DataTable, str As String
            Dim TblRow As HtmlTableRow, RowCell1 As HtmlTableCell, RowCell2 As HtmlTableCell, Tbl As HtmlTable
            Session("TblSkill2") = New HtmlTable
            Session("TblSkill2") = TblSkills2
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

                    img.ID = "Img" & .Item("SKILL_CODE") & "1"
                    img.Src = "images/Plus.gif"
                    img.Attributes.Add("OnClick", "ShowMenu('" & .Item("SKILL_CODE") & "1')")
                    img.Width = 9
                    img.Height = 9
                    RowCell1.Controls.Add(img)

                    '=================Second Cell==================
                    Tbl.ID = "Tbl" & .Item("Skill_Code") & "1"
                    Tbl.CellPadding = 2
                    Tbl.CellSpacing = 3
                    Tbl.Border = 0
                    Tbl.Style.Item("display") = "none"
                    ChildNode2(.Item("Skill_Code"), Tbl)

                    RowCell2.Align = "Left"
                    RowCell2.VAlign = "Top"
                    RowCell2.InnerText = .Item("SKILL_NAME")
                    RowCell2.Style.Item("FONT-WEIGHT") = "bold"
                    RowCell2.Style.Item("FONT-SIZE") = "8pt"
                    RowCell2.Controls.Add(Tbl)

                    TblRow.Cells.Add(RowCell1)
                    TblRow.Cells.Add(RowCell2)
                    Session("TblSkill2").Rows.Add(TblRow)

                End With
            Next
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Fill Parent TreeNode2")
        End Try
    End Sub

    Public Function ChildNode2(ByVal CD As String, ByRef HtmlTbl As HtmlTable) As String
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
                        img.ID = "img" & .Item("Skill_Code") & "1"
                        img.Attributes.Add("OnClick", "ShowMenu('" & .Item("SKILL_CODE") & "1')")
                        img.Width = 9
                        img.Height = 9
                        RowCell1.Controls.Add(img)
                        TblRow.Cells.Add(RowCell1)
                    Else
                        '=================Second Cell==================
                        Chk = New HtmlInputCheckBox
                        Chk.ID = "Chk" & .Item("Skill_Code") & "1"
                        Chk.Style.Item("width") = "15px"
                        Chk.Style.Item("height") = "15px"
                        Chk.Attributes.Add("onclick", "ShowCombo('" & .Item("SKILL_CODE") & "1')")
                        Chk.Checked = False
                        RowCell2.Align = "Left"
                        RowCell2.VAlign = "Top"
                        RowCell2.Controls.Add(Chk)

                        '=================Fourth Cell (Leaf Node) ==================
                        dt = New DataTable
                        cmb = New HtmlSelect
                        cmb.Attributes.Add("runat", "server")
                        cmb.ID = "cmb" & .Item("Skill_Code") & "1"
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
                    Tbl.ID = "Tbl" & .Item("Skill_Code") & "1"
                    Tbl.CellPadding = 0
                    Tbl.CellSpacing = 3
                    Tbl.Style.Item("display") = "none"
                    ChildNode2(.Item("Skill_Code"), Tbl)

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

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub

#Region "  Display Records  "

    Private Sub cmbCode_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCode.SelectedIndexChanged
        Try
            If cmbCode.SelectedValue = "" Then Exit Sub
            DisplayRecords(cmbCode.SelectedValue)
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "cmbCode Changed")
        End Try
    End Sub

    Sub DisplayRecords(ByVal Code As String)
        Try
            Dim i As Int16
            Dim dttemp As DataTable
            Dim sqlstr As String
            dttemp = New DataTable
            sqlstr = " Select Emp_code, Skill_Code, Skill_Rate from HRDSKILLS where emp_Code='" & Code & "'"
            Session("DalObj").GetSqlDataTable(dttemp, sqlstr)
            If dttemp.Rows.Count = 0 Then Exit Sub

            For i = 0 To dttemp.Rows.Count - 1
                With dttemp.Rows(i)
                    If Not Session("TblSkill1").FindControl("Chk" & .Item("Skill_Code")) Is Nothing Then
                        CType(Session("TblSkill1").FindControl("Chk" & .Item("Skill_Code")), HtmlInputCheckBox).Checked = True
                        CType(Session("TblSkill1").FindControl("cmb" & .Item("Skill_Code")), HtmlSelect).Style.Item("display") = "block"
                        CType(Session("TblSkill1").FindControl("cmb" & .Item("Skill_Code")), HtmlSelect).Value = .Item("Skill_Rate")
                    End If
                End With
            Next
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Display Records")
        End Try
    End Sub

#End Region

#Region "   Save Records   "

    Private Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim Tran As SqlClient.SqlTransaction
        Try
            Dim sqlstr2 As String
            Dim i As Int16
            CountSkills()
            Tran = Session("DalObj").StartTransaction("Save")

            '=================Insert Training Skills=====================

            Session("DalObj").ExecuteCommand("Delete From EMPTRAINNEEDS Where EMP_Code ='" & Chk(cmbCode.SelectedValue) & "' and  year = '" & Session("finyear") & "'", Tran)

            If Not IsNothing(SkillCode) Then
                For i = 0 To SkillCode.Length - 1
                    sqlstr2 = " Insert EMPTRAINNEEDS (year,Emp_Code, skillCode,skill_rate) Values ('" & Session("finyear") & "', '" & _
                                Chk(cmbCode.SelectedValue) & "', '" & _
                                Chk(SkillCode(i)) & "', '" & _
                                Chk(SkillRate(i)) & "' )"

                    Session("DalObj").ExecuteCommand(sqlstr2, Tran)
                Next
            End If
            Tran.Commit()
            SetMsg(LblErrMsg, "Record Succesfully Saved.")
            DisplayRecords(cmbCode.SelectedValue)
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message)
            Tran.Rollback()
        Finally
            If Not Tran Is Nothing Then Tran.Dispose()
        End Try
    End Sub

    Public Sub CountSkills()
        Try
            Dim i As Int16
            Dim DtTemp As New DataTable
            Dim sqlstr1 As String
            sqlstr1 = "Select  skill_code, skill_name from SKILLMAST Where SKILL_SCALE='R' and SKILL_TYPE='KS'"
            Session("DalObj").GetSqlDataTable(DtTemp, sqlstr1)
            For i = 0 To DtTemp.Rows.Count - 1
                With DtTemp.Rows(i)
                    If Not Session("TblSkill2").FindControl("Chk" & .Item("Skill_Code") & "1") Is Nothing Then
                        If CType(Session("TblSkill2").FindControl("Chk" & .Item("Skill_Code") & "1"), HtmlInputCheckBox).Checked = True Then
                            Session("SkillCode") = Session("SkillCode") & .Item("Skill_Code") & "|"
                            Session("SkillRate") = Session("SkillRate") & CType(Session("TblSkill2").FindControl("cmb" & .Item("Skill_Code") & "1"), HtmlSelect).Value & "|"
                        End If
                    End If
                End With
            Next

            If Session("SkillCode") <> "" Then
                SkillCode = Split(Mid(Session("SkillCode"), 1, Len(Session("SkillCode")) - 1), "|")
                SkillRate = Split(Mid(Session("SkillRate"), 1, Len(Session("SkillRate")) - 1), "|")
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message)
        End Try
    End Sub

#End Region

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub

End Class
