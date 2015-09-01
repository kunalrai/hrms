Partial Class DesignationMast
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

#Region "     On Load    "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
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
                    Case MdlHRMS.AccessType.ReadonlyAccess, MdlHRMS.AccessType.Restricted
                        bSuccess = False
                End Select
                cmdSave.Visible = bSuccess
            End If
            '---------------------------------------------

            'Comment by ravi
            'If CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules <> "" Then
            '    If InStr(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, SrNo & "-") <> 0 Then
            '        Dim int As Int16, st As String
            '        int = InStr(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, SrNo)
            '        st = Right(Mid(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, int, Len(SrNo & "-") + 1), 1)

            '        If st <> "S" Then
            '            cmdSave.Visible = False
            '            'cmdNew.Visible = False  comment by Ravi
            '        End If
            '    Else
            '        Response.Redirect("Main.aspx")
            '    End If
            'End If

            FillSkillTree()
            If Not IsPostBack Then
                ' cmdNew_Click(sender, e) 'commdnt by Ravi
                btnNew_Click(sender, Nothing)
            End If
        Catch ex As Exception
            LblErrMsg.Text = ex.Message & " " & ex.Source
        End Try

    End Sub

    'comment by Ravi on 24 nov 2006
    'Private Sub cmdNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNew.Click
    '    Try
    '        LblErrMsg.Text = ""
    '        Session("BalObj").FillCombo(cmbDesignation, "Select DSG_CODE, DSG_NAME from DSGMAST ORDER BY DSG_NAME", True)
    '        Session("BalObj").FillCombo(cmbCode, "Select DSG_CODE, DSG_NAME from DSGMAST ORDER BY DSG_NAME", True)
    '        ClearAll(Me)
    '        TxtCode.Text = Session("BalObj").GetNextNumber("DSGMAST", "DSG_CODE")
    '        TxtCode.Text = TxtCode.Text.PadLeft(4, "0")
    '        DisplayRecords(TxtCode.Text)
    '        ViewState("Action") = "ADDNEW"
    '    Catch ex As Exception
    '        SetMsg(LblErrMsg, ex.Message & " : " & ex.Source)
    '    End Try
    'End Sub

#End Region

#Region "    Save Records    "

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            If Not IsValidate() Then Exit Sub
            Dim SqlStr As String, i As Int16

            CountSkills()
            If ViewState("Action") = "ADDNEW" Then
                SqlStr = " Insert DSGMAST ( DSG_CODE, DSG_NAME, UNDER_DSG ) Values ('" & _
                         Chk(TxtCode.Text) & "', '" & _
                         Chk(TxtDesc.Text) & "', '" & _
                         Chk(cmbDesignation.SelectedValue) & "' )"

                Session("DalObj").ExecuteCommand(SqlStr)

                If Not IsNothing(SkillCode) Then
                    For i = 0 To SkillCode.Length - 1

                        SqlStr = " Insert DSGSKLREQ ( DSG_CODE, SKILL_CODE, SKILL_RATE ) Values ('" & _
                                                 Chk(TxtCode.Text) & "', '" & _
                                                 Chk(SkillCode(i)) & "', '" & _
                                                 Chk(SkillRate(i)) & "' )"

                        Session("DalObj").ExecuteCommand(SqlStr)
                    Next
                End If
            ElseIf ViewState("Action") = "MODIFY" Then

                SqlStr = " Update DSGMAST Set " & _
                         " DSG_CODE = '" & Chk(TxtCode.Text) & "', " & _
                         " DSG_NAME = '" & Chk(TxtDesc.Text) & "', " & _
                         " UNDER_DSG = '" & Chk(cmbDesignation.SelectedValue) & " ' Where DSG_CODE = '" & Chk(TxtCode.Text) & "'"

                Session("DalObj").ExecuteCommand(SqlStr)

                Session("DalObj").ExecuteCommand(" Delete From DSGSKLREQ Where DSG_CODE = '" & Chk(TxtCode.Text) & "'")

                If Not IsNothing(SkillCode) Then
                    For i = 0 To SkillCode.Length - 1

                        SqlStr = " Insert DSGSKLREQ ( DSG_CODE, SKILL_CODE, SKILL_RATE ) Values ('" & _
                                                 Chk(TxtCode.Text) & "', '" & _
                                                 Chk(SkillCode(i)) & "', '" & _
                                                 Chk(SkillRate(i)) & "' )"

                        Session("DalObj").ExecuteCommand(SqlStr)
                    Next
                End If
            End If
            BlankRecords()
            'cmdNew_Click(sender, e)  comment by ravi
            btnNew_Click(sender, e)
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & ex.Source)
        End Try
    End Sub


    Function IsValidate() As Boolean
        Try
            Dim Code As Int16

            If TxtCode.Text = "" Then
                SetMsg(LblErrMsg, " Code can not be left blank, Record Not Saved.")
                Return False
            End If

            If ViewState("Action") = "ADDNEW" Then
                Code = Session("DalObj").Executecommand(" Select Count(*) From DSGMAST Where DSG_CODE = '" & Chk(TxtCode.Text) & "'")
                If Code > 0 Then
                    SetMsg(LblErrMsg, " This Designation Code Already exist, Record Not Saved.")
                    Return False
                End If
            End If

            If TxtDesc.Text = "" Then
                SetMsg(LblErrMsg, " Designation Name can not be left blank, Record Not Saved.")
                Return False
            End If
            Return True
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Validate Records")
        End Try
    End Function


    Public Sub CountSkills()
        Try
            Dim i As Int16
            Dim DtTemp As New DataTable

            Session("DalObj").GetSqlDataTable(DtTemp, "Select * from SKILLMAST Where SKILL_SCALE='R' and SKILL_TYPE='KS'")
            For i = 0 To DtTemp.Rows.Count - 1
                With DtTemp.Rows(i)
                    If Not Session("TblSkill").FindControl("Chk" & .Item("Skill_Code")) Is Nothing Then
                        If CType(Session("TblSkill").FindControl("Chk" & .Item("Skill_Code")), HtmlInputCheckBox).Checked = True Then
                            Session("SkillCode") = Session("SkillCode") & .Item("Skill_Code") & "|"
                            Session("SkillRate") = Session("SkillRate") & CType(Session("TblSkill").FindControl("cmb" & .Item("Skill_Code")), HtmlSelect).Value & "|"
                        End If
                    End If
                End With
            Next

            If Session("SkillCode") = "" Then Exit Sub
            SkillCode = Split(Mid(Session("SkillCode"), 1, Len(Session("SkillCode")) - 1), "|")
            SkillRate = Split(Mid(Session("SkillRate"), 1, Len(Session("SkillRate")) - 1), "|")
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message)
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

#Region "     Display Records    "

    Private Sub btnList_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnList.Click
        LblErrMsg.Text = ""
        cmbCode.Visible = True
        TxtCode.Visible = False
        btnList.Visible = False
        'cmdNew.Visible = False comment by ravi
    End Sub

    Private Sub cmbCode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCode.SelectedIndexChanged
        Try
            If cmbCode.SelectedValue = "" Then Exit Sub
            TxtCode.Visible = True
            btnList.Visible = True
            cmbCode.Visible = False
            'cmdNew.Visible = True comment by ravi
            TxtCode.Text = cmbCode.SelectedValue
            DisplayRecords(Chk(TxtCode.Text))
            ViewState("Action") = "MODIFY"
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Code Changed()")
        End Try
    End Sub


    Sub DisplayRecords(ByVal Code As String)
        Try
            BlankRecords()
            Dim i As Int16, SqlStr As String
            Dim DtTemp As New DataTable
            SqlStr = " Select DSG_CODE, DSG_NAME, UNDER_DSG from DSGMAST Where DSG_CODE = '" & Code & "'"
            Session("DalObj").GetSqlDataTable(DtTemp, SqlStr)
            If DtTemp.Rows.Count = 0 Then Exit Sub
            TxtDesc.Text = DtTemp.Rows(0).Item("DSG_NAME")

            ChkCombo(cmbDesignation, Chk(DtTemp.Rows(0).Item("UNDER_DSG")))

            'cmbDesignation.SelectedValue = Chk(DtTemp.Rows(0).Item("UNDER_DSG"))

            DtTemp = New DataTable
            SqlStr = " Select Skill_Code, Skill_Rate from DSGSKLREQ where DSG_CODE ='" & Code & "'"
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

    Sub BlankRecords()
        Try
            Dim i As Int16, DtTemp As New DataTable, SqlStr As String

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
            SetMsg(LblErrMsg, ex.Message & " : " & "Blank Records")
        End Try
    End Sub

#End Region

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNew.Click
        Try
            LblErrMsg.Text = ""
            Session("BalObj").FillCombo(cmbDesignation, "Select DSG_CODE, DSG_NAME from DSGMAST ORDER BY DSG_NAME", True)
            Session("BalObj").FillCombo(cmbCode, "Select DSG_CODE, DSG_NAME from DSGMAST ORDER BY DSG_NAME", True)
            ClearAll(Me)
            TxtCode.Text = Session("BalObj").GetNextNumber("DSGMAST", "DSG_CODE")
            TxtCode.Text = TxtCode.Text.PadLeft(4, "0")
            DisplayRecords(TxtCode.Text)
            ViewState("Action") = "ADDNEW"
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & ex.Source)
        End Try
    End Sub
End Class
