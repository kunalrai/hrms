Imports System.Web.UI.WebControls
Imports Microsoft.Web.UI.WebControls
Partial Class TrainingMast
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim SqlStr, Status As String
    Dim DtTemp As DataTable
    Dim SkillCode(), SkillRate() As String

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If
        FillSkillTree()
        If Not IsPostBack Then
            Session("BalObj").FillCombo(cmbTraiGrp, "Select Train_GrpCode, Train_GrpName from TrainGrp", True)
            Session("BalObj").FillCombo(cmbCode, "Select Train_Code, Train_Name from TrainMast", True)
            btnNew_Click(sender, Nothing)
        End If
    End Sub

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

#Region "     Save Records        "

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim Tran As SqlClient.SqlTransaction
        Try

            If Not IsValidate() Then Exit Sub

            Dim i As Int16
            CountSkills()
            Tran = Session("DalObj").StartTransaction("Save")
            If Viewstate("Status") = "ADDNEW" Then

                '=================Insert Training Master=====================

                SqlStr = " Insert TRAINMAST (Train_Code, Train_Name, Train_GrpCode) Values ('" & _
                         Chk(TxtCode.Text) & "', '" & _
                         Chk(TxtDescription.Text) & "', '" & _
                         Chk(cmbTraiGrp.SelectedValue) & "' )"

                Session("DalObj").ExecuteCommand(SqlStr, Tran)

                '=================Insert Training Budget=====================

                SqlStr = " Insert TRAINBUDGET (BudgetYear, Train_Code, BudgetAmount) Values ('" & _
                         Session("FinYear") & "', '" & _
                         Chk(TxtCode.Text) & "', '" & _
                         ChkN(TxtBudget.Text) & "' )"

                Session("DalObj").ExecuteCommand(SqlStr, Tran)

                '=================Insert Training Skills=====================

                If Not IsNothing(SkillCode) Then
                    For i = 0 To SkillCode.Length - 1
                        SqlStr = " Insert TRAINSKILLS (Train_Code, Skill_Code, Skill_Rate_To) Values ('" & _
                                             Chk(TxtCode.Text) & "', '" & _
                                             Chk(SkillCode(i)) & "', '" & _
                                             Chk(SkillRate(i)) & "' )"

                        Session("DalObj").ExecuteCommand(SqlStr, Tran)
                    Next
                End If

            ElseIf Viewstate("Status") = "MODIFY" Then

                '=================Updating Training Master=====================
                SqlStr = " UpDate TRAINMAST Set " & _
                         " Train_Code='" & Chk(TxtCode.Text) & "', " & _
                         " Train_Name='" & Chk(TxtDescription.Text) & "', " & _
                         " Train_GrpCode='" & Chk(cmbTraiGrp.SelectedValue) & "' Where Train_Code='" & Chk(TxtCode.Text) & "'"

                Session("DalObj").ExecuteCommand(SqlStr, Tran)

                '=================Updating Training Budget=====================
                SqlStr = " UpDate TRAINBUDGET Set " & _
                         " BudgetYear='" & Session("FinYear") & "', " & _
                         " Train_Code='" & Chk(TxtCode.Text) & "', " & _
                         " BudgetAmount='" & ChkN(TxtBudget.Text) & "' Where Train_Code='" & Chk(TxtCode.Text) & "'"
                Session("DalObj").ExecuteCommand(SqlStr, Tran)

                '=================Updating Training Skills=====================
                Session("DalObj").ExecuteCommand("Delete From TRAINSKILLS Where Train_Code ='" & Chk(TxtCode.Text) & "'", Tran)

                If Not IsNothing(SkillCode) Then
                    For i = 0 To SkillCode.Length - 1
                        SqlStr = " Insert TRAINSKILLS (Train_Code, Skill_Code, Skill_Rate_To) Values ('" & _
                                             Chk(TxtCode.Text) & "', '" & _
                                             Chk(SkillCode(i)) & "', '" & _
                                             Chk(SkillRate(i)) & "' )"

                        Session("DalObj").ExecuteCommand(SqlStr, Tran)
                    Next
                End If
            End If

            Tran.Commit()
            btnNew_Click(sender, Nothing)
            Session("BalObj").FillCombo(cmbCode, "Select Train_Code, Train_Name from TrainMast", True)
            SetMsg(LblErrMsg, "Record Succesfully Saved.")
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message)
            Tran.Rollback()
        Finally
            If Not Tran Is Nothing Then Tran.Dispose()
        End Try
    End Sub

    Function IsValidate() As Boolean
        Try
            Dim Count As Int16

            If Chk(TxtCode.Text) = "" Then
                SetMsg(LblErrMsg, " Code can not be left blank.")
                Return False
            End If

            If Chk(TxtDescription.Text) = "" Then
                SetMsg(LblErrMsg, " Description can not be left blank.")
                Return False
            End If

            If Viewstate("Status") <> "MODIFY" Then
                Count = Session("DalObj").ExecuteCommand(" Select Count(*) From TRAINMAST Where Train_Code='" & Chk(TxtCode.Text) & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
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

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub

#Region "    Add New Records    "

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNew.Click
        Try
            LblErrMsg.Text = ""
            BlankRecords()
            TxtCode.Text = Session("BalObj").GetNextNumber("TRAINMAST", "TRAIN_CODE")
            TxtCode.Text = TxtCode.Text.PadLeft(3, "0")
            DisplayRecords(TxtCode.Text)
            Viewstate("Status") = "ADDNEW"
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Add New")
        End Try
    End Sub

    Sub BlankRecords()
        Try
            Dim i As Int16
            TxtBudget.Text = ""
            cmbTraiGrp.SelectedValue = ""
            TxtDescription.Text = ""

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

    Private Sub btnList_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnList.Click
        TxtCode.Visible = False
        btnList.Visible = False
        btnNew.Visible = False
        cmbCode.Visible = True
    End Sub

#End Region

#Region "  Display Records  "

    Private Sub cmbCode_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCode.SelectedIndexChanged
        Try
            If cmbCode.SelectedValue = "" Then Exit Sub
            TxtCode.Visible = True
            cmbCode.Visible = False
            btnList.Visible = True
            btnNew.Visible = True
            TxtCode.Text = Chk(cmbCode.SelectedValue)
            BlankRecords()
            DisplayRecords(TxtCode.Text)
            Viewstate("Status") = "MODIFY"
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "cmbCode Changed")
        End Try
    End Sub


    Private Sub TxtCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtCode.TextChanged
        Try
            BlankRecords()
            DisplayRecords(Chk(TxtCode.Text))
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Code Changed")
        End Try
    End Sub

    Sub DisplayRecords(ByVal Code As String)
        Try
            LblErrMsg.Text = ""
            Dim i As Int16
            DtTemp = New DataTable
            SqlStr = " Select TM.Train_Code, TM.Train_Name, TB.BudgetAmount, TM.Train_GrpCode from TrainMast TM Left Join TrainBudget TB on TM.Train_Code = TB.Train_Code Where TM.Train_Code='" & Code & "'"
            Session("DalObj").GetSqlDataTable(DtTemp, SqlStr)
            If DtTemp.Rows.Count = 0 Then Exit Sub
            TxtBudget.Text = ChkN(DtTemp.Rows(0).Item("BudgetAmount"))
            TxtDescription.Text = Chk(DtTemp.Rows(0).Item("Train_Name"))
            ChkCombo(cmbTraiGrp, DtTemp.Rows(0).Item("Train_GrpCode"))

            DtTemp = New DataTable
            SqlStr = " Select Train_Code, Skill_Code, Skill_Rate_To from TRAINSKILLS where Train_Code='" & Code & "'"
            Session("DalObj").GetSqlDataTable(DtTemp, SqlStr)
            If DtTemp.Rows.Count = 0 Then Exit Sub

            For i = 0 To DtTemp.Rows.Count - 1
                With DtTemp.Rows(i)
                    If Not Session("TblSkill").FindControl("Chk" & .Item("Skill_Code")) Is Nothing Then
                        CType(Session("TblSkill").FindControl("Chk" & .Item("Skill_Code")), HtmlInputCheckBox).Checked = True
                        CType(Session("TblSkill").FindControl("cmb" & .Item("Skill_Code")), HtmlSelect).Style.Item("display") = "block"
                        CType(Session("TblSkill").FindControl("cmb" & .Item("Skill_Code")), HtmlSelect).Value = .Item("Skill_Rate_To")
                    End If
                End With
            Next
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Display Records")
        End Try
    End Sub


#End Region

#Region " Dump  "

    'Public Sub CountSkills()
    '    Dim i, j, k As Int16, TempNode As TreeNode

    '    For i = 0 To TrwSkills.Nodes.Count - 1
    '        For j = 0 To TrwSkills.Nodes(i).Nodes.Count - 1
    '            For Each TempNode In TrwSkills.Nodes(i).Nodes
    '                If TempNode.Checked = True Then
    '                    Session("Count") = Session("Count") & TempNode.ID & "|"
    '                End If
    '            Next
    '        Next
    '    Next
    'End Sub


    '#Region "      Fill Skill Tree View     "

    '    Public Sub FillTree()
    '        Try
    '            Dim Str As String, i As Int16
    '            Dim DtView As DataView, TrwNode As TreeNode, dtTempView As DataView

    '            Str = " Select SKILL_CODE, SKILL_NAME, Skill_Grp From SKILLMAST Where Skill_Scale='R'"

    '            Session("DalObj").GetSqlDataTable(DtTemp, Str)
    '            DtView = New DataView(DtTemp)

    '            DtView.RowFilter = " Skill_Grp='*'"
    '            For i = 0 To DtView.Count - 1
    '                TrwNode = New TreeNode
    '                TrwNode.ID = DtView.Item(i).Item("Skill_Code")
    '                TrwNode.Text = DtView.Item(i).Item("Skill_Name")
    '                TrwSkills.Nodes.Add(TrwNode)

    '                DtTemp = New DataTable
    '                Str = " Select SKILL_CODE, SKILL_NAME, Skill_Grp From SKILLMAST Where Skill_Scale='R' and Skill_Grp='" & TrwNode.ID & "'"
    '                Session("DalObj").GetSqlDataTable(DtTemp, Str)
    '                dtTempView = New DataView(DtTemp)
    '                If DtTemp.Rows.Count <> 0 Then FillSubTree(TrwNode, dtTempView)
    '            Next
    '        Catch ex As Exception
    '            SetMsg(LblErrMsg, ex.Message)
    '        End Try
    '    End Sub

    '    Public Sub FillSubTree(ByRef TreeNode As TreeNode, ByVal DtView As DataView)
    '        Try
    '            Dim SubTrwNode As TreeNode, i As Int16, str As String, DtNewView As DataView
    '            DtView.RowFilter = " Skill_Grp='" & TreeNode.ID & "'"

    '            For i = 0 To DtView.Count - 1
    '                SubTrwNode = New TreeNode
    '                SubTrwNode.CheckBox = True
    '                SubTrwNode.ID = DtView.Item(i).Item("Skill_Code")
    '                SubTrwNode.Text = DtView.Item(i).Item("Skill_Name")
    '                TreeNode.Nodes.Add(SubTrwNode)

    '                DtTemp = New DataTable
    '                str = " Select SKILL_CODE, SKILL_NAME, Skill_Grp From SKILLMAST Where Skill_Scale='R' and Skill_Grp='" & SubTrwNode.ID & "'"
    '                Session("DalObj").GetSqlDataTable(DtTemp, str)
    '                DtNewView = New DataView(DtTemp)
    '                If DtTemp.Rows.Count <> 0 Then FillSubTree(SubTrwNode, DtNewView)
    '            Next
    '        Catch ex As Exception
    '            SetMsg(LblErrMsg, ex.Message)
    '        End Try
    '    End Sub

    '#End Region
#End Region

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub

End Class
