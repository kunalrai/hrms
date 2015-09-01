Partial Class TrainingCalender
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents HyperLink1 As System.Web.UI.WebControls.HyperLink

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

    Public Enum Source
        TrainCalenderSkills
        TrainingSkills
    End Enum

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If
        FillSkillTree()
        LblErrMsg.Text = ""
        If Not IsPostBack Then
            Try
                FillComboBox()
                btnNew_Click(sender, Nothing)
                If Session("CalCode") <> "" Then
                    DisplayRecords(Session("CalCode"))
                End If
            Catch ex As Exception
                SetMsg(LblErrMsg, ex.Message & " : " & "On Load")
            End Try
        End If
    End Sub

    Private Sub btnList_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnList.Click
        Try
            TxtCode.Visible = False
            btnList.Visible = False
            btnNew.Visible = False
            cmbCode.Visible = True
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Display Records")
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNew.Click
        Try
            TBHeadSkills.Style.Item("display") = "none"
            BlankRecords()
            Viewstate("Action") = "ADDNEW"
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "New Records")
        End Try
    End Sub

#Region "    Display Records     "

    Private Sub cmbCode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCode.SelectedIndexChanged
        Try
            If cmbCode.SelectedValue = "" Then Exit Sub
            TxtCode.Visible = True
            btnList.Visible = True
            btnNew.Visible = True
            cmbCode.Visible = False
            TxtCode.Text = Chk(cmbCode.SelectedValue)
            DisplayRecords(TxtCode.Text)
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Display Records : Code Changed")
        End Try
    End Sub

    Private Sub TxtCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtCode.TextChanged
        Try
            DisplayRecords(Chk(TxtCode.Text))
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Display Records : Code Changed")
        End Try
    End Sub

    Sub DisplayRecords(ByVal Code As String)
        Try
            Dim Str As String, dt As New DataTable, i As Int16

            Str = " Select * from TrainCalendar Where TrainCalCode='" & Code & "'"
            Session("DalObj").getSqlDataTable(dt, Str)
            If dt.Rows.Count = 0 Then Exit Sub
            With dt.Rows(0)
                If Chk(.Item("Train_Type")) = "I" Then
                    cmbType.SelectedValue = 1
                Else
                    cmbType.SelectedValue = 2
                End If
                TxtCode.Text = Chk(.Item("TrainCalCode"))
                dtpFromDate.Text = Format(CDate(.Item("Train_From")), "dd/MMM/yyyy")
                dtpToDate.Text = Format(CDate(.Item("Train_To")), "dd/MMM/yyyy")
                DtpStart.Text = Format(CDate(.Item("Start_Date")), "dd/MMM/yyyy")
                DtpEnd.Text = Format(CDate(.Item("End_Date")), "dd/MMM/yyyy")
                TxtEMailId.Text = Chk(.Item("Train_Src_ConNo"))
                TxtHours.Text = ChkN(.Item("Total_Hours"))
                TxtCostEmp.Text = ChkN(.Item("Cost_per_Emp"))
                TxtTargetAud.Text = Chk(.Item("Tgt_Audience"))
                TxtVenueCont.Text = Chk(.Item("Train_Vn_ConNo"))
                TxtVenueAdd.Text = Chk(.Item("Train_Vn_addr"))
                TxtVenue.Text = Chk(.Item("Train_Venue"))
                TxtSeats.Text = ChkN(.Item("No_Of_Seats"))
                TxtSource.Text = Chk(.Item("Train_Src"))
                cmbLocation.SelectedValue = Chk(.Item("LOC_CODE"))
                cmbTrainGrp.SelectedValue = ChkN(.Item("TrainGrpId"))
                cmbTrainer.SelectedValue = Chk(.Item("Trainer_Code"))
                cmbTraining.SelectedValue = Chk(.Item("Train_Code"))
            End With
            BlankSkillsTree()
            DisplaySkillTree(Chk(Code), Source.TrainCalenderSkills)
            Viewstate("Action") = "MODIFY"
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Display Records")
        End Try
    End Sub

#End Region

#Region "      Save Records     "

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim Tran As SqlClient.SqlTransaction
        Try
            If Not IsValidate() Then Exit Sub

            CountSkills()
            Tran = Session("DalObj").StartTransaction("Save")
            Dim StrQuery As String, i As Int16, FileName As String, FileLength As Int16

            If FileTrain.PostedFile.FileName <> "" Then
                FileLength = InStr(1, StrReverse(FileTrain.PostedFile.FileName), "\")
                FileName = Mid(FileTrain.PostedFile.FileName, (Len(FileTrain.PostedFile.FileName) - FileLength) + 2)
                FileName = "Files\" & FileName
            End If


            If Viewstate("Action") = "ADDNEW" Then

                '=================Insert Training Calender Master=====================

                StrQuery = " Insert TrainCalendar(TrainCalCode,Train_Code,Train_From,Train_To,Loc_code,Train_Type,TrainGrpId, " & _
                           " Train_Venue,Train_vn_addr,Cost_per_Emp,No_of_Seats,Train_Src,Train_Vn_ConNo,Train_Src_ConNo " & _
                           " ,Tgt_Audience,Trainer_Code,Total_Hours,Start_Date,End_Date,TrainFile) Values ('" & _
                            Chk(TxtCode.Text) & "' ,'" & _
                            Chk(cmbTraining.SelectedValue) & "', '" & _
                            dtpFromDate.Text & "', '" & _
                            dtpToDate.Text & "', '" & _
                            Chk(cmbLocation.SelectedValue) & "', '" & _
                            IIf(ChkN(cmbType.SelectedValue) = 1, "I", "E") & "', '" & _
                            Chk(cmbTrainGrp.SelectedValue) & "', '" & _
                            Chk(TxtVenue.Text) & "', '" & _
                            Chk(TxtVenueAdd.Text) & "', '" & _
                            Chk(TxtCostEmp.Text) & "', '" & _
                            Chk(TxtSeats.Text) & "', '" & _
                            Chk(TxtSource.Text) & "', '" & _
                            Chk(TxtVenueCont.Text) & "', '" & _
                            Chk(TxtEMailId.Text) & "', '" & _
                            Chk(TxtTargetAud.Text) & "', '" & _
                            Chk(cmbTrainer.SelectedValue) & "', " & _
                            ChkN(TxtHours.Text) & ", '" & _
                            DtpStart.Text & "', '" & _
                            DtpEnd.Text & "', "

                If FileTrain.Value = "" Then
                    StrQuery = StrQuery & "Null )"
                Else
                    StrQuery = StrQuery & "'" & FileName & "')"
                End If


                Session("DalObj").ExecuteCommand(StrQuery, Tran)

                '=================Insert Training Calender Skills=====================

                If Not IsNothing(SkillCode) Then
                    For i = 0 To SkillCode.Length - 1
                        StrQuery = " Insert TrainCalSkills (TrainCal_Code, Skill_Code, Skill_Rate) Values ('" & _
                                             Chk(TxtCode.Text) & "', '" & _
                                             Chk(SkillCode(i)) & "', '" & _
                                             Chk(SkillRate(i)) & "' )"

                        Session("DalObj").ExecuteCommand(StrQuery, Tran)
                    Next
                End If

            ElseIf Viewstate("Action") = "MODIFY" Then

                '=================Update Training Calendar Master=====================

                StrQuery = " Update TrainCalendar Set TrainCalCode = '" & Chk(TxtCode.Text) & "' , " & _
                           " Train_Code = '" & Chk(cmbTraining.SelectedValue) & "', " & _
                           " Train_From = '" & dtpFromDate.Text & "', " & _
                           " Train_To = '" & dtpToDate.Text & "', " & _
                           " Loc_code = '" & Chk(cmbLocation.SelectedValue) & "', " & _
                           " Train_Type = '" & IIf(ChkN(cmbType.SelectedValue) = 1, "I", "E") & "', " & _
                           " TrainGrpId = '" & Chk(cmbTrainGrp.SelectedValue) & "', " & _
                           " Train_Venue = '" & Chk(TxtVenue.Text) & "', " & _
                           " Train_vn_addr = '" & Chk(TxtVenueAdd.Text) & "', " & _
                           " Cost_per_Emp = '" & ChkN(TxtCostEmp.Text) & "', " & _
                           " No_of_Seats = '" & ChkN(TxtSeats.Text) & "', " & _
                           " Train_Src ='" & Chk(TxtSource.Text) & "', " & _
                           " Train_Vn_ConNo = '" & Chk(TxtVenueCont.Text) & "', " & _
                           " Train_Src_ConNo = '" & Chk(TxtEMailId.Text) & "', " & _
                           " Tgt_Audience = '" & Chk(TxtTargetAud.Text) & "', " & _
                           " Trainer_Code = '" & Chk(cmbTrainer.SelectedValue) & "', " & _
                           " Total_Hours = " & ChkN(TxtHours.Text) & ", " & _
                           " Start_Date = '" & DtpStart.Text & "', " & _
                           " TrainFile = '" & FileName & "', " & _
                           " End_Date = '" & DtpEnd.Text & "' Where TrainCalCode = '" & Chk(TxtCode.Text) & "'"

                Session("DalObj").ExecuteCommand(StrQuery, Tran)

                '=================Update Training Calender Skills=====================

                Session("DalObj").ExecuteCommand(" Delete TrainCalSkills Where TrainCal_Code = '" & Chk(TxtCode.Text) & "'", Tran)

                If Not IsNothing(SkillCode) Then
                    For i = 0 To SkillCode.Length - 1
                        StrQuery = " Insert TrainCalSkills (TrainCal_Code, Skill_Code, Skill_Rate) Values ('" & _
                                             Chk(TxtCode.Text) & "', '" & _
                                             Chk(SkillCode(i)) & "', '" & _
                                             Chk(SkillRate(i)) & "' )"

                        Session("DalObj").ExecuteCommand(StrQuery, Tran)
                    Next
                End If

            End If
            Tran.Commit()
            FillComboBox()
            btnNew_Click(sender, Nothing)
            SetMsg(LblErrMsg, "Record Saved Successfully.")
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

    Public Function IsValidate() As Boolean
        Try
            Dim Count As Int16
            If Chk(TxtCode.Text) = "" Then
                SetMsg(LblErrMsg, "Code can not be left blank.")
                Return False
            End If

            If Viewstate("Action") <> "MODIFY" Then
                Count = Session("DalObj").ExecuteCommand(" Select Count(*) From TRAINCALENDAR Where TrainCalCode='" & Chk(TxtCode.Text) & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
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

#Region "    Training Skill Tree Display    "

    Private Sub cmbTraining_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTraining.SelectedIndexChanged
        If cmbTraining.SelectedValue = "" Then
            TBHeadSkills.Style.Item("display") = "none"
        Else
            TBHeadSkills.Style.Item("display") = "block"
            BlankSkillsTree()
            DisplaySkillTree(Chk(cmbTraining.SelectedValue), Source.TrainingSkills)
        End If
    End Sub

    Sub DisplaySkillTree(ByVal Code As String, Optional ByVal Source As Source = Source.TrainingSkills)
        Try
            Dim Dt As New DataTable, str As String, i As Int16
            Dt = New DataTable
            TBHeadSkills.Style.Item("display") = "block"
            If Source = Source.TrainingSkills Then
                str = " Select Train_Code, Skill_Code, Skill_Rate_To from TrainSkills where Train_Code='" & Code & "'"
            Else
                str = " Select TrainCal_Code, Skill_Code, Skill_Rate from TrainCalSkills where TrainCal_Code='" & Code & "'"
            End If

            Session("DalObj").GetSqlDataTable(Dt, str)
            If Dt.Rows.Count = 0 Then Exit Sub

            For i = 0 To Dt.Rows.Count - 1
                With Dt.Rows(i)
                    If Not Session("TblSkill").FindControl("Chk" & .Item("Skill_Code")) Is Nothing Then
                        CType(Session("TblSkill").FindControl("Chk" & .Item("Skill_Code")), HtmlInputCheckBox).Checked = True
                        CType(Session("TblSkill").FindControl("cmb" & .Item("Skill_Code")), HtmlSelect).Style.Item("display") = "block"

                        If Source = Source.TrainingSkills Then
                            CType(Session("TblSkill").FindControl("cmb" & .Item("Skill_Code")), HtmlSelect).Value = .Item("Skill_Rate_To")
                        Else
                            CType(Session("TblSkill").FindControl("cmb" & .Item("Skill_Code")), HtmlSelect).Value = .Item("Skill_Rate")
                        End If

                    End If
                End With
            Next

        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "DisplaySkillTree()")
        End Try
    End Sub

#End Region

#Region "    Fill Skill Tree   "

    Public Sub FillSkillTree()
        Try
            Dim i As Int16, dt As New DataTable, str As String
            Dim TblRow As HtmlTableRow, RowCell1 As HtmlTableCell, RowCell2 As HtmlTableCell, Tbl As HtmlTable
            Session("TblSkill") = New HtmlTable
            Session("TblSkill") = TBLSkills
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

#Region "    Clear Records      "

    Sub BlankRecords()
        ClearAll(Me)
        BlankSkillsTree()
        dtpFromDate.Text = Format(Date.Today, "dd/MMM/yyyy")
        dtpToDate.Text = Format(Date.Today, "dd/MMM/yyyy")
        DtpStart.Text = Format(Date.Today, "dd/MMM/yyyy")
        DtpEnd.Text = Format(Date.Today, "dd/MMM/yyyy")
    End Sub

    Sub BlankSkillsTree()
        Try
            Dim Sqlstr As String, i As Int16

            Sqlstr = " Select Skill_Code from SkillMast where Skill_Type='KS'and Skill_Scale='R'"
            DtTemp = New DataTable
            Session("DalObj").GetSqlDataTable(DtTemp, Sqlstr)
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
            SetMsg(LblErrMsg, ex.Message & " : " & "BlankRecords()")
        End Try
    End Sub

#End Region

    Sub FillComboBox()
        Session("BalObj").FillCombo(cmbCode, "Select TrainCalCode, TrainCalCode From TrainCalendar", True)
        Session("BalObj").FillCombo(cmbLocation, "Select LOC_CODE, LOC_NAME From LOCMAST", True)
        Session("BalObj").FillCombo(cmbTraining, "Select Train_Code, Train_Name from TrainMast", True)
        Session("BalObj").FillCombo(cmbTrainer, "SELECT Trainer_Code, Trainer_Name from TRAINERMAST", True)
        Session("BalObj").FillCombo(cmbTrainGrp, "SELECT TrainGrpId, TrainGrpDesc from TRAINGRPMAST", True)
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub
    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub
End Class
