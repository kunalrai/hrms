Partial Class ReportWizard
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

    Dim DtTemp As DataTable
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Server.GetLastError Is Nothing Then SetMsg(LblErrMsg, "")
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If
        TxtCon.Text = ""
        FillTableFields()
        If Not IsPostBack Then
            Session("BalObj").FillCombo(CmbSqlDesc, "select id, QryName from Repqueries order by QryName", True)
        End If
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub

#Region "    Fill Table Fields  "

    Public Sub FillTableFields()
        Try

            Dim i As Int16, dt As New DataTable, str As String
            Dim TblRow As HtmlTableRow, RowCell1 As HtmlTableCell, RowCell2 As HtmlTableCell, Tbl As HtmlTable
            Session("TblTableFields") = New HtmlTable
            Session("TblTableFields") = TblTableFields
            str = "Select Distinct(Table_Name) as TBLNAME from QRYREPORTWIZARD"
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

                    img.ID = "Img" & .Item("TBLNAME")
                    img.Src = "images/Plus.gif"
                    img.Style.Item("CURSOR") = "hand"
                    img.Attributes.Add("OnClick", "ShowMenu('" & .Item("TBLNAME") & "')")
                    img.Width = 9
                    img.Height = 9
                    RowCell1.Controls.Add(img)

                    '=================Second Cell==================
                    Tbl.ID = "Tbl" & .Item("TBLNAME")
                    Tbl.CellPadding = 2
                    Tbl.CellSpacing = 3
                    Tbl.Border = 0
                    Tbl.Style.Item("display") = "none"
                    ChildNode(.Item("TBLNAME"), Tbl)

                    RowCell2.Align = "Left"
                    RowCell2.VAlign = "Top"
                    RowCell2.InnerText = .Item("TBLNAME")
                    RowCell2.Style.Item("FONT-WEIGHT") = "bold"
                    RowCell2.Style.Item("FONT-SIZE") = "8pt"
                    RowCell2.Controls.Add(Tbl)

                    TblRow.Cells.Add(RowCell1)
                    TblRow.Cells.Add(RowCell2)
                    Session("TblTableFields").Rows.Add(TblRow)

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

            Dim strSql = "Select * From QRYREPORTWIZARD Where Table_Name='" & CD & "'"
            Session("DalObj").GetSqlDataTable(rsChild, strSql)

            For cnt = 0 To rsChild.Rows.Count - 1
                With rsChild.Rows(cnt)
                    TblRow = New HtmlTableRow
                    RowCell2 = New HtmlTableCell
                    RowCell3 = New HtmlTableCell
                    RowCell4 = New HtmlTableCell
                    img = New HtmlImage

                    Tbl = New HtmlTable

                    Chk = New HtmlInputCheckBox
                    Chk.ID = "Chk" & .Item("TABLE_NAME") & "|" & .Item("COLUMN_NAME")
                    Chk.Attributes.Add("OnClick", "SetFields(this.id)")
                    Chk.Style.Item("width") = "15px"
                    Chk.Style.Item("height") = "15px"
                    Chk.Checked = False
                    RowCell2.Align = "Left"
                    RowCell2.VAlign = "Top"
                    RowCell2.Controls.Add(Chk)

                    '=================Third Cell===========================
                    Tbl.ID = "Tbl" & .Item("TABLE_NAME") & .Item("COLUMN_NAME")
                    Tbl.CellPadding = 0
                    Tbl.CellSpacing = 1
                    Tbl.Style.Item("display") = "none"
                    ChildNode(.Item("TABLE_NAME") & .Item("COLUMN_NAME"), Tbl)

                    RowCell3.Align = "Left"
                    RowCell3.VAlign = "Top"
                    RowCell3.InnerText = .Item("Description")
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
            SetMsg(LblErrMsg, ex.Message & " : " & "Fill Child ReportNode")
        End Try
    End Function

#End Region

    Private Sub cmdExecute_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExecute.Click
        Dim FrmHTMlRpt As frmHTMLReports
        If Left(UCase(TxtSqlQuery.Text.TrimStart), 6) = "SELECT" Then
            FrmHTMlRpt.argStrSql = TxtSqlQuery.Text
        Else
            FrmHTMlRpt.argStrSql = "EXECUTE"
            CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(TxtSqlQuery.Text.Trim)
        End If
    End Sub

    'Private Sub cmdAdd_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.ServerClick
    '    Try
    '        Dim Row As New HtmlTableRow
    '        Dim Col1 As New HtmlTableCell
    '        Dim Col2 As New HtmlTableCell
    '        Dim Col3 As New HtmlTableCell
    '        Dim Col4 As New HtmlTableCell
    '        Dim Col5 As New HtmlTableCell
    '        Dim Col6 As New HtmlTableCell

    '        Col1.InnerText = TxtOpnB.Text
    '        Col2.InnerText = cmbFields.SelectedItem.Text
    '        Col3.InnerText = cmbCompare.SelectedItem.Text
    '        Col4.InnerText = TxtCondition.Text
    '        Col5.InnerText = TxtCloB.Text
    '        Col6.InnerText = cmbAndOr.SelectedItem.Text

    '        Row.Cells.Add(Col1)
    '        Row.Cells.Add(Col2)
    '        Row.Cells.Add(Col3)
    '        Row.Cells.Add(Col4)
    '        Row.Cells.Add(Col5)
    '        Row.Cells.Add(Col6)

    '        TblSelect.Rows.Add(Row)

    '    Catch ex As Exception
    '        SetMsg(LblErrMsg, ex.Message & " : " & "Add Condition")
    '    End Try
    'End Sub

    'Private Sub cmdAddFields_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddFields.Click
    '    Dim StrCom() As String, i As Int16
    '    StrCom = Split(TxtFields.Text, ",")

    '    For i = 0 To StrCom.Length - 1
    '        cmbFields.Items.Add(StrCom(i))
    '    Next
    '    '
    'End Sub
    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub

    Private Sub CmbSqlDesc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbSqlDesc.SelectedIndexChanged
        Try
            LblErrMsg.Text = ""
            TxtSqlQuery.Text = ""
            If CmbSqlDesc.SelectedValue = "" Then
                SetMsg(LblErrMsg, "Please Select The Query Name From The List.")
            Else
                TxtSqlQuery.Text = Session("DalObj").executeCommand("Select query from repqueries where id='" & ChkN(CmbSqlDesc.SelectedValue) & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & "CmbSqlDesc_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub CmdSqlQryDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdSqlQryDel.Click
        Try
            If CmbSqlDesc.SelectedValue = "" Then
                SetMsg(LblErrMsg, "Please Select The Query Name From The List.")
            Else
                Dim Delqry As String
                Delqry = "Delete from RepQueries where id='" & ChkN(CmbSqlDesc.SelectedValue) & "'"
                Session("Dalobj").executeCommand(Delqry)
                SetMsg(LblErrMsg, "Query Has Been Deleted Successfully.")
                TxtSqlQuery.Text = "Select From"
                Session("BalObj").FillCombo(CmbSqlDesc, "select id, QryName from Repqueries order by QryName", True)
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & "CmdSqlQryDel_Click")
        End Try
    End Sub

    Private Sub CmdSaveSqlQry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdSaveSqlQry.Click
        Try
            If Chk(TxtSqlQuery.Text) = "" Then
                SetMsg(LblErrMsg, "Please Write The Query.")
                Exit Sub
            End If
            If Chk(TxtSqlDesc.Text) = "" Then
                SetMsg(LblErrMsg, "Description Can't Be left Blank.")
                Exit Sub
            End If
            Dim MyQuery, txtQry As String
            txtQry = Chk(TxtSqlQuery.Text)
            txtQry = Replace(txtQry, "'", "''")

            MyQuery = "insert into repqueries(QryName, Query) values('" & Chk(TxtSqlDesc.Text) & "','" & txtQry & "') "
            Session("DalObj").ExecuteCommand(MyQuery)
            SetMsg(LblErrMsg, "Query Has Been Saved Successfully ")
            TxtSqlQuery.Text = "Select From"
            TxtSqlDesc.Text = ""
            Session("BalObj").FillCombo(CmbSqlDesc, "select id, QryName from Repqueries order by QryName", True)
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & "CmdSaveSqlQry_Click")
        End Try
    End Sub


End Class
