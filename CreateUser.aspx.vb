Partial Class CreateUser
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ChkHRADMIN As System.Web.UI.WebControls.CheckBox
    Protected WithEvents ChkVpOperation As System.Web.UI.WebControls.CheckBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim ModuleCode() As String
#Region "  On Load  "
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If
        'By Ravi 21 Nov

        Dim SrNo As Int16
        SrNo = Request.QueryString.Item("SrNo") + Request.QueryString.Item("MODULEID")
        Dim bSuccess As Boolean
        Select Case CheckRight(SrNo)
            Case MdlHRMS.AccessType.FullAccess, MdlHRMS.AccessType.SuperUser
                bSuccess = True
            Case MdlHRMS.AccessType.ReadonlyAccess, MdlHRMS.AccessType.Restricted
                bSuccess = False
        End Select
        CmdSave.Visible = bSuccess
        '------------------------------------

        'comment by Ravi
        'If CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules <> "" Then
        '    If InStr(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, SrNo & "-") <> 0 Then
        '        Dim int As Int16, st As String
        '        int = InStr(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, SrNo)
        '        st = Right(Mid(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, int, Len(SrNo & "-") + 1), 1)

        '        If st = "S" Then
        '            CmdSave.Visible = True
        '        Else
        '            'txtEM_CD.ReadOnly = True
        '            CmdSave.Visible = False
        '        End If
        '    Else
        '        Response.Redirect("Main.aspx")
        '    End If
        'End If

        FillModules()
        FillAccess()
        FillReports()
        If Not IsPostBack Then
            FillComboBoxes()
            btnNew_Click(sender, Nothing)
            Modules.Style.Item("display") = "none"
            ModulesHD.Style.Item("display") = "none"
        End If
    End Sub
    Sub FillComboBoxes()
        Try
            Dim Strs As String, dt As New DataTable, i As Int16, LItem As ListItem
            Strs = "Select UserID,UserName from WebUsers Where Type = 'G' Order by USERNAME"
            Session("DalObj").GetSqlDataTable(dt, Strs)
            CmbGroupName.Items.Clear()
            For i = 0 To dt.Rows.Count - 1
                LItem = New ListItem
                LItem.Text = Decrypt(dt.Rows(i).Item("UserName"))
                LItem.Value = Decrypt(dt.Rows(i).Item("UserID"))
                CmbGroupName.Items.Add(LItem)
            Next
            CmbGroupName.DataBind()

            dt = New DataTable
            'comment by Ravi
            Strs = "Select UserID, UserName, Type from WebUsers Order by USERNAME"
            'by Ravi
            ' Strs = "select * from (SELECT '' AS UserId ,' Select User/Group Id'  as UserName ,'' as type union  Select UserID, UserName, Type from WebUsers )tmp Order by USERNAME"
            Session("DalObj").GetSqlDataTable(dt, Strs)
            cmbUserID.Items.Clear()
            cmbUserID.Items.Insert(0, "")
            For i = 0 To dt.Rows.Count - 1
                'LItem = New ListItem
                If dt.Rows(i).Item("Type") = "G" Then
                    dt.Rows(i).Item("UserName") = Decrypt(dt.Rows(i).Item("UserName")) & " (Group)"
                Else
                    dt.Rows(i).Item("UserName") = Decrypt(dt.Rows(i).Item("UserName"))
                End If

                dt.Rows(i).Item("UserID") = Decrypt(dt.Rows(i).Item("UserID"))
                dt.AcceptChanges()
                'cmbUserID.Items.Add(LItem)
            Next

            '==================================
            'Dim DtNew As New DataTable
            'Session("DalObj").GetSqlDataTable(DtNew, " Select EMP_CODE,EMP_NAME,SECT_CODE FROM HRDMASTQRY WHERE 1=1 " & Session("UserCodes"))

            'Dim parentCol As DataColumn
            'Dim childCol As DataColumn

            'Dim Dset As New DataSet
            'Dset.Tables.Add(dt)
            'Dset.Tables.Add(DtNew)

            'parentCol = Dset.Tables("Table2").Columns("EMP_CODE")
            'childCol = Dset.Tables("Table1").Columns("UserID")
            '' Create DataRelation.
            'Dim relWebUsers As DataRelation
            'relWebUsers = New DataRelation("WebUsers", parentCol, childCol, False)
            '' Add the relation to the DataSet.
            'Dset.Relations.Add(relWebUsers)

            'Dim DView As New DataView(Dset.Tables("Table1"))

            'If Session("UserCodes") <> "" Then
            '    DView.RowFilter = Mid(LTrim(Session("UserCodes")), 4, Len(Session("UserCodes")) - 3)
            'End If

            '====================================

            Dim DView As New DataView(dt)
            DView.Sort = " UserName ASC"
            cmbUserID.DataValueField = "UserID"
            cmbUserID.DataTextField = "UserName"
            cmbUserID.DataSource = DView
            cmbUserID.DataBind()
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Fill ComboBoxes")
        End Try
    End Sub

#End Region

#Region "    Add New Records    "
    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNew.Click
        Try
            BlankRecords()
            DisplayRecords(TxtUserID.Text)
            Viewstate("Status") = "ADDNEW"
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Add New")
        End Try
    End Sub
    Sub BlankRecords()
        Try
            Dim i, j As Int16, DtTemp As New DataTable, dt As DataTable, sqlstr As String
            TxtUserID.Text = ""
            TxtUserName.Text = ""
            TxtPass.Text = ""
            TxtConPass.Text = ""
            ChkGroup.Checked = False
            Modules.Style.Item("display") = "none"
            ModulesHD.Style.Item("display") = "none"
            'TBHeadAccess.Style.Item("display") = "none"
            CmbGroupName.Enabled = True

            '===============Modules Rights===========================
            sqlstr = "Select MODULE_CODE, MODULE_DESC from WebModules Where Active =1 "
            Session("DalObj").GetSqlDataTable(DtTemp, sqlstr)

            If DtTemp.Rows.Count = 0 Then Exit Sub
            For i = 0 To DtTemp.Rows.Count - 1
                With DtTemp.Rows(i)
                    If Not Session("TblModules").FindControl("Chk" & .Item("MODULE_CODE")) Is Nothing Then
                        CType(Session("TblModules").FindControl("Chk" & .Item("MODULE_CODE")), HtmlInputCheckBox).Checked = False
                    End If
                    If Not Session("TblModules").FindControl("cmb" & .Item("MODULE_CODE")) Is Nothing Then
                        CType(Session("TblModules").FindControl("cmb" & .Item("MODULE_CODE")), HtmlSelect).Style.Item("display") = "none"
                        CType(Session("TblModules").FindControl("cmb" & .Item("MODULE_CODE")), HtmlSelect).SelectedIndex = 0
                    End If
                End With
            Next

            '===============Access Rights===========================
            DtTemp = New DataTable
            Session("DalObj").GetSqlDataTable(DtTemp, "Select * From AccessTable")

            For i = 0 To DtTemp.Rows.Count - 1
                dt = New DataTable
                Session("DalObj").GetSqlDataTable(dt, "Select * From " & Chk(DtTemp.Rows(i).Item("Tab_Name")))
                For j = 0 To dt.Rows.Count - 1
                    With dt.Rows(j)
                        If Not Session("TBLAccess").FindControl("Chk" & "`" & DtTemp.Rows(i).Item("ShortCut") & Chk(.Item(Chk(DtTemp.Rows(i).Item("Code")))) & DtTemp.Rows(i).Item("ShortCut") & "`") Is Nothing Then
                            CType(Session("TBLAccess").FindControl("Chk" & "`" & DtTemp.Rows(i).Item("ShortCut") & Chk(.Item(Chk(DtTemp.Rows(i).Item("Code")))) & DtTemp.Rows(i).Item("ShortCut") & "`"), HtmlInputCheckBox).Checked = False
                        End If
                    End With
                Next
            Next

            '===============Reports Rights===========================
            DtTemp = New DataTable
            Session("DalObj").GetSqlDataTable(DtTemp, "Select SrNo From MstReports Where isnull(Active,0)=1 Order By RptType")

            If DtTemp.Rows.Count = 0 Then Exit Sub
            For i = 0 To DtTemp.Rows.Count - 1
                With DtTemp.Rows(i)
                    If Not Session("TBlReports").FindControl("Chk_" & .Item("SrNo")) Is Nothing Then
                        CType(Session("TBlReports").FindControl("Chk_" & .Item("SrNo")), HtmlInputCheckBox).Checked = False
                    End If
                End With
            Next

        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Blank Records")
        End Try
    End Sub

    Private Sub btnList_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnList.Click
        BlankRecords()

        'By Ravi  on 2 Dec 06
        FillComboBoxes()
        btnList.Visible = False
        TxtUserID.Visible = False
        btnNew.Visible = False
        cmbUserID.Visible = True
        ChkGroup.Checked = False
    End Sub
#End Region

#Region "   Display Records  "

    Private Sub CmbUserID_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUserID.SelectedIndexChanged
        If cmbUserID.SelectedValue = "" Then Exit Sub
        LblErrMsg.Text = ""
        btnList.Visible = True
        TxtUserID.Visible = True
        cmbUserID.Visible = False
        btnNew.Visible = True
        TxtUserID.Text = cmbUserID.SelectedValue
        DisplayRecords(Chk(TxtUserID.Text))
        Viewstate("Status") = "MODIFY"
    End Sub

    'Private Sub TxtUserID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtUserID.TextChanged
    '    Try
    '        BlankRecords()
    '        DisplayRecords(Chk(TxtUserID.Text))
    '        ViewState("Action") = "MODIFY"
    '    Catch ex As Exception
    '        SetMsg(LblErrMsg, ex.Message & " : " & "Code Changed")
    '    End Try
    'End Sub

    Private Sub DisplayRecords(ByVal Code As String)
        Try
            Dim dttemp As New DataTable
            Dim i As Int16, SqlStr As String
            Dim ModCode As String, Count() As String
            SqlStr = " Select Username,Group_id,Type from WebUsers where UserId = '" & Encrypt(Chk(TxtUserID.Text), "+") & "'"
            Session("DalObj").GetSqlDataTable(dttemp, SqlStr)
            If dttemp.Rows.Count = 0 Then Exit Sub
            TxtUserName.Text = Decrypt(Chk(dttemp.Rows(0).Item("UserName")))
            ChkCombo(CmbGroupName, Decrypt(Chk(dttemp.Rows(0).Item("Group_Id"))))


            If Not IsDBNull(dttemp.Rows(0).Item("Type")) Then
                ChkGroup.Checked = IIf(Chk(dttemp.Rows(0).Item("Type")) = "G", True, False)
            End If

            If ChkGroup.Checked Then
                Modules.Style.Item("display") = "block"
                'TBHeadAccess.Style.Item("display") = "block"
                CmbGroupName.Enabled = False
                ModulesHD.Style.Item("display") = "block"
            Else
                Modules.Style.Item("display") = "none"
                'TBHeadAccess.Style.Item("display") = "none"
                CmbGroupName.Enabled = True
                ModulesHD.Style.Item("display") = "none"
            End If


            ''''''''''''''''''============Modules Rights==============''''''''''''''''''''''''
            SqlStr = " Select isnull(Modules,'') From WebUsers Where USERID = '" & Encrypt(Chk(TxtUserID.Text), "+") & "'"
            ModCode = Session("DalObj").ExecuteCommand(SqlStr, , DAL.DataLayer.ExecutionType.ExecuteScalar)

            If ModCode <> "" Then
                Count = Split(ModCode, ",")
                For i = 0 To Count.Length - 1
                    If Not Session("TblModules").FindControl("Chk" & Mid(Count(i), 1, Len(Count(i)) - 2)) Is Nothing Then
                        Dim IsSave As String
                        IsSave = Right(Count(i), 1)
                        CType(Session("TblModules").FindControl("Chk" & Mid(Count(i), 1, Len(Count(i)) - 2)), HtmlInputCheckBox).Checked = True
                        CType(Session("TblModules").FindControl("cmb" & Mid(Count(i), 1, Len(Count(i)) - 2)), HtmlSelect).Style.Item("display") = "block"
                        If IsSave = "S" Then
                            CType(Session("TblModules").FindControl("cmb" & Mid(Count(i), 1, Len(Count(i)) - 2)), HtmlSelect).SelectedIndex = 0
                        ElseIf IsSave = "V" Then
                            CType(Session("TblModules").FindControl("cmb" & Mid(Count(i), 1, Len(Count(i)) - 2)), HtmlSelect).SelectedIndex = 1
                        End If

                    End If
                Next
            End If

            ''''''''''''''''''============Access Rights==============''''''''''''''''''''''''
            SqlStr = " Select isnull(Codes,'') From WebUsers Where USERID = '" & Encrypt(Chk(TxtUserID.Text), "+") & "'"
            ModCode = Session("DalObj").ExecuteCommand(SqlStr, , DAL.DataLayer.ExecutionType.ExecuteScalar)

            If ModCode <> "" Then
                Count = Split(ModCode, ",")
                For i = 0 To Count.Length - 1
                    If Not Session("TBLAccess").FindControl("Chk" & Count(i)) Is Nothing Then
                        CType(Session("TBLAccess").FindControl("Chk" & Count(i)), HtmlInputCheckBox).Checked = True
                    End If
                Next
            End If

            ''''''''''''''==============Repoprts Rights===================='''''''''''''''''''''
            SqlStr = " Select isnull(Reports,'') From WebUsers Where USERID = '" & Encrypt(Chk(TxtUserID.Text), "+") & "'"
            ModCode = Session("DalObj").ExecuteCommand(SqlStr, , DAL.DataLayer.ExecutionType.ExecuteScalar)

            If ModCode <> "" Then
                Count = Split(ModCode, "|")
                For i = 0 To Count.Length - 1
                    If Not Session("TBLReports").FindControl("Chk_" & Count(i)) Is Nothing Then
                        CType(Session("TBLReports").FindControl("Chk_" & Count(i)), HtmlInputCheckBox).Checked = True
                    End If
                Next
            End If

        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Display Records")
        End Try
    End Sub

#End Region

#Region "    Fill User Rights  "
    Public Sub FillModules()
        Try
            Dim i As Int16, dt As New DataTable, str As String
            Dim TblRow As HtmlTableRow, RowCell1 As HtmlTableCell, RowCell2 As HtmlTableCell, Tbl As HtmlTable
            Session("TblModules") = New HtmlTable
            Session("TblModules") = TblModules
            str = " Select MODULE_CODE, MODULE_DESC from WebModules Where MODULE_GRP = '-1' AND Active =1 ORDER BY OrderNo"
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

                    img.ID = "Img" & .Item("MODULE_CODE")
                    img.Src = "images/Plus.gif"
                    img.Style.Item("CURSOR") = "hand"
                    img.Attributes.Add("OnClick", "ShowMenu('" & .Item("MODULE_CODE") & "')")
                    img.Width = 9
                    img.Height = 9

                    RowCell1.Controls.Add(img)


                    '=================Second Cell==================
                    Tbl.ID = "Tbl" & .Item("MODULE_CODE")
                    Tbl.CellPadding = 2
                    Tbl.CellSpacing = 2
                    Tbl.Border = 0
                    Tbl.Style.Item("display") = "none"
                    ChildNode(.Item("MODULE_CODE"), Tbl)

                    RowCell2.Align = "Left"
                    RowCell2.VAlign = "Top"
                    RowCell2.InnerText = .Item("MODULE_DESC")
                    RowCell2.Style.Item("FONT-WEIGHT") = "bold"
                    RowCell2.Style.Item("FONT-SIZE") = "8pt"
                    RowCell2.Controls.Add(Tbl)

                    TblRow.Cells.Add(RowCell1)
                    TblRow.Cells.Add(RowCell2)
                    Session("TblModules").Rows.Add(TblRow)

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

            Dim strSql = "Select MODULE_CODE, MODULE_DESC, MODULE_GRP, IsNull(HasChild,0) as HasChild from WebModules Where Active =1 and  MODULE_GRP ='" & CD & "' ORDER BY OrderNo"
            Session("DalObj").GetSqlDataTable(rsChild, strSql)

            For cnt = 0 To rsChild.Rows.Count - 1
                With rsChild.Rows(cnt)
                    TblRow = New HtmlTableRow
                    RowCell2 = New HtmlTableCell
                    RowCell3 = New HtmlTableCell
                    RowCell4 = New HtmlTableCell
                    img = New HtmlImage

                    Tbl = New HtmlTable

                    If (.Item("MODULE_GRP") = "0") Or (.Item("HasChild") = "1") Then

                        '=================First Cell==================
                        RowCell1 = New HtmlTableCell

                        RowCell1.Align = "Left"
                        RowCell1.Width = "5px"
                        RowCell1.VAlign = "Top"

                        img.Src = "images/Plus.gif"
                        img.ID = "img" & .Item("MODULE_CODE")
                        img.Attributes.Add("OnClick", "ShowMenu('" & .Item("MODULE_CODE") & "')")
                        img.Style.Item("CURSOR") = "hand"
                        img.Width = 9
                        img.Height = 9
                        RowCell1.Controls.Add(img)
                        TblRow.Cells.Add(RowCell1)

                    Else

                        '=================Second Cell==================
                        Chk = New HtmlInputCheckBox
                        Chk.ID = "Chk" & .Item("MODULE_CODE")
                        Chk.Style.Item("width") = "15px"
                        Chk.Style.Item("height") = "15px"
                        Chk.Attributes.Add("onclick", "ShowCombo('" & .Item("MODULE_CODE") & "')")
                        Chk.Checked = False
                        RowCell2.Align = "Left"
                        RowCell2.VAlign = "Top"
                        RowCell2.Controls.Add(Chk)


                        '=====================Fourth Cell===============
                        cmb = New HtmlSelect
                        cmb.Attributes.Add("runat", "server")
                        cmb.ID = "cmb" & .Item("MODULE_CODE")
                        cmb.Style.Item("width") = "60px"
                        cmb.Style.Item("display") = "none"
                        cmb.Items.Add(New ListItem("Save", "S"))
                        cmb.Items.Add(New ListItem("View", "V"))
                        cmb.DataBind()

                        RowCell4.Align = "Left"
                        RowCell4.VAlign = "Top"
                        RowCell4.Controls.Add(cmb)

                    End If

                    '=================Third Cell===========================

                    Tbl.ID = "Tbl" & .Item("MODULE_CODE")
                    Tbl.CellPadding = 0
                    Tbl.CellSpacing = 3
                    Tbl.Style.Item("display") = "none"
                    ChildNode(.Item("MODULE_CODE"), Tbl)

                    RowCell3.Align = "Left"
                    RowCell3.VAlign = "Top"
                    RowCell3.InnerText = MdlHRMS.Chk(.Item("MODULE_DESC"))
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

#Region "   Save Records   "

    Private Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdSave.Click
        Dim Tran As SqlClient.SqlTransaction
        Try
            If Not isValidate() Then Exit Sub
            CountModules()
            CountAccess()
            CountReportsRights()
            Tran = Session("DalObj").StartTransaction("Save")
            Dim sqlstr As String

            If ChkGroup.Checked = False Then
                ViewState("MODULECODE") = ""
            End If

            If Viewstate("Status") = "ADDNEW" Then
                sqlstr = " insert  WEBUSERS (USERID,USERNAME,Group_ID, USERPASSWORD, Modules, Reports, Codes , Type) Values('" & _
                               Encrypt(UCase(Chk(TxtUserID.Text)), "+") & "', '" & _
                               Encrypt(UCase(Chk(TxtUserName.Text)), "+") & "', '" & _
                               Encrypt(Chk(CmbGroupName.SelectedValue), "+") & "', '" & _
                               Encrypt(Chk(TxtPass.Text), "+") & "', '" & _
                               ViewState("MODULECODE") & "', '" & _
                               ViewState("REPORTCODE") & "', '" & _
                               ViewState("CODES") & "', '" & _
                               Chk(IIf(ChkGroup.Checked, "G", "U")) & "' )"
                Session("DalObj").ExecuteCommand(sqlstr, Tran)

            ElseIf Viewstate("Status") = "MODIFY" Then
                sqlstr = " Update WEBUSERS set  " & _
                               "UserName = '" & Encrypt(UCase(Chk(TxtUserName.Text)), "+") & "', " & _
                               "Group_Id = '" & Encrypt(Chk(CmbGroupName.SelectedValue), "+") & "', " & _
                               "UserPassword = '" & Encrypt(Chk(TxtPass.Text), "+") & "', " & _
                               "Modules = '" & ViewState("MODULECODE") & "', " & _
                               "Reports = '" & ViewState("REPORTCODE") & "', " & _
                               "Codes = '" & ViewState("CODES") & "', " & _
                               "Type  = '" & Chk(IIf(ChkGroup.Checked, "G", "U")) & "' " & _
                               "where  UserID  = '" & Encrypt(UCase(Chk(TxtUserID.Text)), "+") & "'"
                Session("DalObj").ExecuteCommand(sqlstr, Tran)
                'Dim DAL As DAL.DataLayer
                'DAL = Session("DalObj")
                'DAL.ExecuteCommand(sqlstr, Tran, DAL.ConnProvider.SQL, DAL.ExecutionType.ExecuteNonQuery)
            End If
            Tran.Commit()
            FillComboBoxes()
            btnNew_Click(sender, Nothing)
            SetMsg(LblErrMsg, "Record Saved Successfully.")
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & ex.Source)
            Tran.Rollback()
        Finally
            If Not IsNothing(Tran) Then Tran.Dispose()
        End Try
    End Sub

    Private Function isValidate() As Boolean
        Try
            Dim Count As Int16
            If Chk(TxtUserID.Text) = "" Then
                SetMsg(LblErrMsg, "User ID Required.")
                Return False
            End If
            If Chk(TxtUserName.Text) = "" Then
                SetMsg(LblErrMsg, "User Name Required.")
                Return False
            End If

            If Chk(TxtPass.Text) = "" Then
                SetMsg(LblErrMsg, "Password Required.")
                Return False
            End If

            If String.Compare(Chk(TxtPass.Text), Chk(TxtConPass.Text)) Then
                SetMsg(LblErrMsg, "Both Type of The Password Must be Same.")
                Return False
            End If
            If Viewstate("Status") = "ADDNEW" Then
                Count = Session("DalObj").ExecuteCommand("Select Count(*) From WebUsers Where UserId =  '" & Encrypt(Chk(TxtUserID.Text), "+") & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
                If Count <> 0 Then
                    SetMsg(LblErrMsg, " UserID Already Exist.")
                    Return False
                End If

                Count = Session("DalObj").ExecuteCommand("Select Count(*) From WebUsers Where UserName =  '" & Encrypt(Chk(TxtUserName.Text), "+") & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
                If Count <> 0 Then
                    SetMsg(LblErrMsg, " User Name Already Exist.")

                    Return False
                End If
            End If

            Return True
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : isValidate()")
            isValidate = False
        End Try
    End Function

    Public Sub CountModules()
        Try
            Dim i As Int16, DtTemp As New DataTable
            DtTemp = New DataTable
            Session("DalObj").GetSqlDataTable(DtTemp, "Select MODULE_CODE, MODULE_DESC from WebModules Where MODULE_GRP <> '*' and MODULE_GRP <> '0'")

            ViewState("MODULECODE") = ""
            For i = 0 To DtTemp.Rows.Count - 1
                With DtTemp.Rows(i)
                    If Not Session("TblModules").FindControl("Chk" & .Item("MODULE_CODE")) Is Nothing Then
                        If CType(Session("TblModules").FindControl("Chk" & .Item("MODULE_CODE")), HtmlInputCheckBox).Checked = True Then
                            If CType(Session("TblModules").FindControl("cmb" & .Item("MODULE_CODE")), HtmlSelect).Value = "S" Then
                                ViewState("MODULECODE") = ViewState("MODULECODE") & .Item("MODULE_CODE") & "-S,"
                            ElseIf CType(Session("TblModules").FindControl("cmb" & .Item("MODULE_CODE")), HtmlSelect).Value = "V" Then
                                ViewState("MODULECODE") = ViewState("MODULECODE") & .Item("MODULE_CODE") & "-V,"
                            End If
                        End If
                    End If
                End With
            Next

            If ViewState("MODULECODE") = "" Then Exit Sub
            ViewState("MODULECODE") = Mid(ViewState("MODULECODE"), 1, Len(ViewState("MODULECODE")) - 1)

        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Count Modules")
        End Try

    End Sub

    Public Sub CountAccess()
        Try
            Dim i, j As Int16, DtTemp As New DataTable, Dt As DataTable
            DtTemp = New DataTable
            Session("DalObj").GetSqlDataTable(DtTemp, "Select * From AccessTable")

            ViewState("CODES") = ""
            For i = 0 To DtTemp.Rows.Count - 1
                Dt = New DataTable
                Session("DalObj").GetSqlDataTable(Dt, "Select * From " & DtTemp.Rows(i).Item("Tab_Name"))
                For j = 0 To Dt.Rows.Count - 1
                    With Dt.Rows(j)
                        If Not Session("TBLAccess").FindControl("Chk" & "`" & DtTemp.Rows(i).Item("ShortCut") & Chk(.Item(DtTemp.Rows(i).Item("Code"))) & DtTemp.Rows(i).Item("ShortCut") & "`") Is Nothing Then
                            If CType(Session("TBLAccess").FindControl("Chk" & "`" & DtTemp.Rows(i).Item("ShortCut") & Chk(.Item(DtTemp.Rows(i).Item("Code"))) & DtTemp.Rows(i).Item("ShortCut") & "`"), HtmlInputCheckBox).Checked = True Then
                                ViewState("CODES") = ViewState("CODES") & "`" & DtTemp.Rows(i).Item("ShortCut") & Chk(.Item(DtTemp.Rows(i).Item("Code"))) & DtTemp.Rows(i).Item("ShortCut") & "`" & ","
                            End If
                        End If
                    End With
                Next
            Next

            If ViewState("CODES") = "" Then Exit Sub
            ViewState("CODES") = Mid(ViewState("CODES"), 1, Len(ViewState("CODES")) - 1)

        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Count Access")
        End Try

    End Sub

    Public Sub CountReportsRights()
        Try
            Dim i As Int16, DtTemp As New DataTable
            DtTemp = New DataTable
            Session("DalObj").GetSqlDataTable(DtTemp, "Select SrNo from MstReports Where isnull(Active,0)=1")

            ViewState("REPORTCODE") = ""
            For i = 0 To DtTemp.Rows.Count - 1
                With DtTemp.Rows(i)
                    If Not Session("TblReports").FindControl("Chk_" & .Item("SrNo")) Is Nothing Then
                        If CType(Session("TblReports").FindControl("Chk_" & .Item("SrNo")), HtmlInputCheckBox).Checked = True Then
                            ViewState("REPORTCODE") = ViewState("REPORTCODE") & ChkN(.Item("SrNo")) & "|"
                        End If
                    End If
                End With
            Next

            If ViewState("REPORTCODE") = "" Then Exit Sub
            ViewState("REPORTCODE") = Mid(ViewState("REPORTCODE"), 1, Len(ViewState("REPORTCODE")) - 1)

        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Count ReportsRights")
        End Try

    End Sub

#End Region

#Region "    Fill Access   "

    Public Sub FillAccess()
        Try
            Dim i As Int16, dt As New DataTable, str As String
            Dim TblRow As HtmlTableRow, RowCell1 As HtmlTableCell, RowCell2 As HtmlTableCell, Tbl As HtmlTable
            Session("TBLAccess") = New HtmlTable
            Session("TBLAccess") = TBLAccess
            '  str = "Select Tab_Name,Tab_Desc,Code,Description, ShortCut From AccessTable"
            str = "Select Tab_Id,Tab_Name,Tab_Desc,Code,Description, ShortCut From AccessTable"  'BY RAVI
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

                    '' img.ID = "Img" & Chk(.Item("Tab_Name"))
                    img.ID = "Img" & .Item("Tab_Id") & Chk(.Item("Tab_Name")) ' By Ravi
                    img.Src = "images/Plus.gif"
                    img.Style.Item("CURSOR") = "hand"
                    'img.Attributes.Add("OnClick", "ShowMenu('" & Chk(.Item("Tab_Name")) & "')")
                    img.Attributes.Add("OnClick", "ShowMenu('" & .Item("Tab_Id") & Chk(.Item("Tab_Name")) & "')")  ' By Ravi
                    img.Width = 9
                    img.Height = 9
                    RowCell1.Controls.Add(img)

                    '=================Second Cell==================
                    Tbl.ID = "Tbl" & .Item("Tab_Id") & Chk(.Item("Tab_Name"))
                    Tbl.CellPadding = 2
                    Tbl.CellSpacing = 2
                    Tbl.Border = 0
                    Tbl.Style.Item("display") = "none"
                    ChildNode(.Item("Tab_Name"), Tbl, Chk(.Item("Code")), Chk(.Item("Description")), Chk(.Item("ShortCut")))

                    RowCell2.Align = "Left"
                    RowCell2.VAlign = "Top"
                    RowCell2.InnerText = Chk(.Item("Tab_Desc"))
                    RowCell2.Style.Item("FONT-WEIGHT") = "bold"
                    RowCell2.Style.Item("FONT-SIZE") = "8pt"
                    RowCell2.Controls.Add(Tbl)

                    TblRow.Cells.Add(RowCell1)
                    TblRow.Cells.Add(RowCell2)
                    Session("TBLAccess").Rows.Add(TblRow)

                End With
            Next
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Fill Access")
        End Try
    End Sub

    Public Function ChildNode(ByVal CD As String, ByRef HtmlTbl As HtmlTable, ByVal sCode As String, ByVal Desc As String, ByVal ShortCut As String) As String
        Try
            Dim rsChild As New DataTable, cnt As Int16, StrQuery As String, Code As Int16
            Dim TblRow As HtmlTableRow, RowCell1 As HtmlTableCell, RowCell2 As HtmlTableCell, RowCell3 As HtmlTableCell
            Dim Tbl As HtmlTable, img As HtmlImage, cmb As HtmlSelect, Chk As HtmlInputCheckBox, RowCell4 As HtmlTableCell
            Dim CmbSkill As DropDownList, dt As DataTable

            Dim strSql = "Select " & sCode & ", " & Desc & " From " & CD
            Session("DalObj").GetSqlDataTable(rsChild, strSql)

            For cnt = 0 To rsChild.Rows.Count - 1

                With rsChild.Rows(cnt)
                    TblRow = New HtmlTableRow
                    RowCell2 = New HtmlTableCell
                    RowCell3 = New HtmlTableCell
                    RowCell4 = New HtmlTableCell
                    img = New HtmlImage

                    Tbl = New HtmlTable

                    '=================First Cell==================
                    Chk = New HtmlInputCheckBox
                    Chk.ID = "Chk" & "`" & ShortCut & MdlHRMS.Chk(.Item(0)) & ShortCut & "`"
                    Chk.Style.Item("width") = "15px"
                    Chk.Style.Item("height") = "15px"
                    Chk.Checked = False
                    RowCell2.Align = "Left"
                    RowCell2.VAlign = "Top"
                    RowCell2.Controls.Add(Chk)

                    '=================Second Cell===========================
                    Tbl.ID = "Tbl" & "`" & ShortCut & MdlHRMS.Chk(.Item(0)) & ShortCut & "`"
                    Tbl.Border = 0
                    Tbl.CellPadding = 0
                    Tbl.CellSpacing = 3
                    Tbl.Style.Item("display") = "none"
                    'ChildNode(.Item(0), Tbl)

                    RowCell3.Align = "Left"
                    RowCell3.VAlign = "Top"
                    RowCell3.InnerText = MdlHRMS.Chk(.Item(1))
                    RowCell3.Controls.Add(Tbl)

                    '======================Adding Cells & Rows in Table====== 
                    If Not RowCell2 Is Nothing Then TblRow.Cells.Add(RowCell2)
                    TblRow.Cells.Add(RowCell3)
                    HtmlTbl.Rows.Add(TblRow)
                End With
            Next
            rsChild.Dispose()
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Fill Child Access Node")
        End Try
    End Function

#End Region

#Region "   Reports Rights "

    Public Sub FillReports()
        Try
            Dim i As Int16, dt As New DataTable, str As String
            Dim TblRow As HtmlTableRow, RowCell1 As HtmlTableCell, RowCell2 As HtmlTableCell, Tbl As HtmlTable
            Session("TBLReports") = New HtmlTable
            Session("TBLReports") = TBLReports
            str = "Select * From ReportsType Where isnull(Active,0)=1"
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

                    img.ID = "Img" & Chk(.Item("RptType"))
                    img.Src = "images/Plus.gif"
                    img.Style.Item("CURSOR") = "hand"
                    img.Attributes.Add("OnClick", "ShowMenu('" & Chk(.Item("RptType")) & "')")
                    img.Width = 9
                    img.Height = 9
                    RowCell1.Controls.Add(img)

                    '=================Second Cell==================
                    Tbl.ID = "Tbl" & Chk(.Item("RptType"))
                    Tbl.CellPadding = 2
                    Tbl.CellSpacing = 2
                    Tbl.Border = 0
                    Tbl.Style.Item("display") = "none"
                    ReportsChildNode(.Item("RptType"), Tbl)

                    RowCell2.Align = "Left"
                    RowCell2.VAlign = "Top"
                    RowCell2.InnerText = Chk(.Item("RptTypeDesc"))
                    RowCell2.Style.Item("FONT-WEIGHT") = "bold"
                    RowCell2.Style.Item("FONT-SIZE") = "8pt"
                    RowCell2.Controls.Add(Tbl)

                    TblRow.Cells.Add(RowCell1)
                    TblRow.Cells.Add(RowCell2)
                    Session("TBLReports").Rows.Add(TblRow)

                End With
            Next
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Fill Reports Rights")
        End Try
    End Sub

    Public Function ReportsChildNode(ByVal CD As String, ByRef HtmlTbl As HtmlTable) As String
        Try
            Dim rsChild As New DataTable, cnt As Int16, StrQuery As String, Code As Int16
            Dim TblRow As HtmlTableRow, RowCell1 As HtmlTableCell, RowCell2 As HtmlTableCell, RowCell3 As HtmlTableCell
            Dim Tbl As HtmlTable, img As HtmlImage, cmb As HtmlSelect, Chk As HtmlInputCheckBox, RowCell4 As HtmlTableCell
            Dim CmbSkill As DropDownList, dt As DataTable

            Dim strSql = "Select SrNo, RptType, RptName From MstReports Where RptType = '" & CD & "'"
            Session("DalObj").GetSqlDataTable(rsChild, strSql)

            For cnt = 0 To rsChild.Rows.Count - 1

                With rsChild.Rows(cnt)
                    TblRow = New HtmlTableRow
                    RowCell2 = New HtmlTableCell
                    RowCell3 = New HtmlTableCell
                    RowCell4 = New HtmlTableCell
                    img = New HtmlImage

                    Tbl = New HtmlTable

                    '=================First Cell==================
                    Chk = New HtmlInputCheckBox
                    Chk.ID = "Chk_" & ChkN(.Item("SrNo"))
                    Chk.Style.Item("width") = "15px"
                    Chk.Style.Item("height") = "15px"
                    Chk.Checked = False
                    RowCell2.Align = "Left"
                    RowCell2.VAlign = "Top"
                    RowCell2.Controls.Add(Chk)

                    '=================Second Cell===========================
                    Tbl.ID = "Tbl" & MdlHRMS.Chk(.Item("RptType")) & MdlHRMS.ChkN(.Item("SrNo"))
                    Tbl.Border = 0
                    Tbl.CellPadding = 0
                    Tbl.CellSpacing = 3
                    Tbl.Style.Item("display") = "none"
                    'ChildNode(.Item(0), Tbl)

                    RowCell3.Align = "Left"
                    RowCell3.VAlign = "Top"
                    RowCell3.InnerText = .Item("RptName")
                    RowCell3.Controls.Add(Tbl)

                    '======================Adding Cells & Rows in Table====== 
                    If Not RowCell2 Is Nothing Then TblRow.Cells.Add(RowCell2)
                    TblRow.Cells.Add(RowCell3)
                    HtmlTbl.Rows.Add(TblRow)
                End With
            Next
            rsChild.Dispose()
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Fill Report Rights Node")
        End Try
    End Function

#End Region

    Private Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub

End Class
