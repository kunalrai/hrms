Imports System.Data.SqlClient
Partial Class EmpExplorer
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents DataGrid1 As System.Web.UI.WebControls.DataGrid
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
        If Server.GetLastError Is Nothing Then SetMsg(LblMsg, "")
        Try
            If IsNothing(Session("LoginUser")) Then
                Response.Redirect("CompSel.aspx")
            End If

            Dim SrNo As String
            SrNo = Request.QueryString.Item("SrNo")

            If CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules <> "" Then
                If InStr(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, SrNo & "-") <> 0 Then
                    Dim int As Int16, st As String
                    int = InStr(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, SrNo)
                    st = Right(Mid(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, int, Len(SrNo & "-") + 1), 1)

                    If st = "S" Then
                        TrDisplaySetting.Visible = True
                        cmdNew.Visible = True
                        ViewState("Condition") = "SAVE"
                    Else
                        TrDisplaySetting.Visible = False
                        cmdNew.Visible = False
                        ViewState("Condition") = "VIEW"
                    End If
                ElseIf InStr(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, "78" & "-") <> 0 Then
                    Response.Redirect("EmpExplorer.aspx?SrNo=78")
                Else
                    Response.Redirect("Main.aspx")
                End If
            Else
                ViewState("Condition") = "SAVE"
            End If


            If Not IsPostBack Then
                Dim StrMsg As String
                txtNumRec.Text = 10

                'If Session("LoginUser").UserGroup = "USER" Then
                '    TrDisplaySetting.Visible = False
                '    cmdNew.Visible = False
                'Else
                '    TrDisplaySetting.Visible = True
                '    cmdNew.Visible = True
                'End If

                cmbPageStyle.Items.Add("NextPrev")
                cmbPageStyle.Items.Add("Numeric")
                cmbPageStyle.SelectedIndex = 0
                FillCombo()
                BindGrid(True)

                If Session("BalObj").ExistInDatabase("PFUSER") Then
                    SetPf()
                Else
                    TrPfLabel.Visible = False
                    TrPf.Visible = False
                End If

                StrMsg = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(" Select Isnull(Card_Code_Field,'') From CompMast ", , DAL.DataLayer.ExecutionType.ExecuteScalar)
                SetMsg(LblBanner, "<MARQUEE behavior=ALTERNATE DIRECTION=RIGHT  scrolldelay=150>" & StrMsg & "</MARQUEE>")
            Else
                BindGrid()
            End If
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " (Page_Load)")
        End Try
    End Sub

#Region "     Bind Data Grid      "

    Private Sub BindGrid(Optional ByVal FillCombo As Boolean = False)
        Dim DT As New DataTable
        Dim dtEmployee As New DataTable
        Try
            Dim StrSql As String
            Dim StrFields As String = ""
            If Not Session("BalObj").ExistInDatabase("EmpExpStructure") Then
                StrSql = " CREATE TABLE EmpExpStructure (ColID int NOT NULL , " & _
                         " ColKey NVARCHAR (20) NULL , ColText NVARCHAR (20) NULL , " & _
                         " ColDataSource NVARCHAR (20) NULL , ColWidth int NULL , " & _
                         " UserID NVARCHAR(30) NULL,ColDataType VarChar(1) ) "
                Session("DalObj").ExecuteCommand(StrSql)
                StrSql = " INSERT EmpExpStructure VALUES(0, 'Name','Name','Emp_Name',3000,'*','S') " & _
                         " INSERT EmpExpStructure VALUES(1,'Emp_Code','Code','Emp_Code',1000,'*','S') " & _
                         " INSERT EmpExpStructure VALUES(2,'FName','First Name','FName',2000,'*','S') " & _
                         " INSERT EmpExpStructure VALUES(3,'MName','Middle Name','MName',2000,'*','S') " & _
                         " INSERT EmpExpStructure VALUES(4,'LName','Last Name','LName',2000,'*','S') " & _
                         " INSERT EmpExpStructure VALUES(5,'DOJ','Joining Date','DOJ',1100,'*','D') " & _
                         " INSERT EmpExpStructure VALUES(6,'Type_Name','Type','Type_Name',2000,'*','S') " & _
                         " INSERT EmpExpStructure VALUES(7,'Grd_Name','Grade','Grd_Name',2000,'*','S') " & _
                         " INSERT EmpExpStructure VALUES(8,'Dsg_Name','Designation','Dsg_Name',2000,'*','S') " & _
                         " INSERT EmpExpStructure VALUES(9,'Mngr_Name','Reports To','Mngr_Name',2000,'*','S') " & _
                         " INSERT EmpExpStructure VALUES(10,'Regn_Name','Region','Regn_Name',2000,'*','S') " & _
                         " INSERT EmpExpStructure VALUES(11,'Loc_Name','Location','Loc_Name',2000,'*','S') " & _
                         " INSERT EmpExpStructure VALUES(12,'Divi_Name','Division','Divi_Name',2000,'*','S') " & _
                         " INSERT EmpExpStructure VALUES(13,'Sect_Name','Section','Sect_Name',2000,'*','S') " & _
                         " INSERT EmpExpStructure VALUES(14,'Dept_Name','Department','Dept_Name',2000,'*','S') " & _
                         " INSERT EmpExpStructure VALUES(15,'Cost_Name','Cost Center','Cost_Name',2000,'*','S') " & _
                         " INSERT EmpExpStructure VALUES(16,'Proc_Name','Process','Proc_Name',2000,'*','S') "
                Session("DalObj").Execute(StrSql)
            End If
            If Session("BalObj").ExistColumn("ColDataType", "EmpExpStructure") = False Then
                Session("Dalobj").ExecuteCommand("Alter Table EmpExpStructure Add ColDataType VarChar(1)")
            End If

            StrSql = "Select * From EmpExpStructure Where UserID = '" & Session("LoginUser").UserID & "' Order By ColID"
            Session("DalObj").GetSqlDataTable(DT, StrSql)

            If Not DT.Rows.Count > 0 Then
                StrSql = "Select * From EmpExpStructure Where UserID = '*' Order By ColID"
                Session("DalObj").GetSqlDataTable(DT, StrSql)
            End If
            Dim i As Int16
            GrdEmployee.Columns.Clear()

            If FillCombo Then cmbSearchFld.Items.Clear()

            Dim GrdTemplCol As New ButtonColumn
            GrdTemplCol.HeaderText = "Edit"
            GrdTemplCol.CommandName = "Edit"
            GrdTemplCol.Text = "Edit"
            GrdTemplCol.HeaderStyle.Width = Unit.Pixel(20)
            GrdTemplCol.ItemStyle.Width = Unit.Pixel(20)

            ''Dim OptCurrentEmp As New RadioButton
            ''OptCurrentEmp.GroupName = "OptEmp"
            ''OptCurrentEmp.ID = "OptEmp"
            ''OptCurrentEmp.Width = Unit.Pixel(20)

            ''GrdTemplCol.ItemTemplate.InstantiateIn(OptCurrentEmp)

            GrdEmployee.Columns.Add(GrdTemplCol)

            Dim GrdColCode As New BoundColumn
            GrdColCode.HeaderStyle.Width = Unit.Pixel(0)
            GrdColCode.ItemStyle.Width = Unit.Pixel(0)
            GrdColCode.HeaderText = "Employee Code"
            GrdColCode.DataField = "Emp_Code"
            GrdColCode.Visible = False
            GrdEmployee.Columns.Add(GrdColCode)


            For i = 0 To DT.Rows.Count - 1
                Dim GrdCol As New BoundColumn
                GrdCol.HeaderStyle.Width = Unit.Pixel(ChkN(DT.Rows(i).Item("ColWidth")))
                GrdCol.ItemStyle.Width = Unit.Pixel(ChkN(DT.Rows(i).Item("ColWidth")))
                GrdCol.HeaderText = Chk(DT.Rows(i).Item("ColText"))
                If Chk(DT.Rows(i).Item("ColDataSource")) = "DOJ" Then
                    GrdCol.DataFormatString = "{0:dd/MMM/yyyy}"
                End If
                GrdCol.DataField = Chk(DT.Rows(i).Item("ColDataSource"))
                GrdEmployee.Columns.Add(GrdCol)
                If FillCombo Then cmbSearchFld.Items.Add(New ListItem(Chk(DT.Rows(i).Item("ColText")), Chk(DT.Rows(i).Item("ColDataSource"))))
            Next

            If FillCombo Then cmbSearchFld.Items.Add("All") : cmbSearchFld.SelectedIndex = cmbSearchFld.Items.Count - 1


            If ViewState("Condition") = "SAVE" Then
                StrSql = "SELECT * FROM HRDMASTQRY WHERE COMP_CODE = '" & Session("LoginUser").CurrentCompID & "'" & Session("UserCodes")
            Else
                StrSql = "SELECT * FROM HRDMASTQRY WHERE COMP_CODE = '" & Session("LoginUser").CurrentCompID & "' AND EMP_CODE = '" & Session("LoginUser").UserId & "'" & Session("UserCodes")
            End If

            'If Session("LoginUser").UserGroup = "ADMIN" Then
            '    StrSql = "SELECT * FROM HRDMASTQRY WHERE COMP_CODE = '" & Session("LoginUser").CurrentCompID & "'" & Session("UserCodes")
            'ElseIf Session("LoginUser").UserGroup = "DIRECTOR" Then
            '    StrSql = "Select * From HrdmastQry Where Comp_Code = '" & Session("LoginUser").CurrentCompID & "'" & Session("UserCodes")
            '    'StrSql = "Select * From HrdmastQry Where Comp_Code = '" & Session("LoginUser").CurrentCompID & "' And (Emp_Code = '" & Session("LoginUser").UserID & "' Or Emp_Code IN (Select Emp_Code From Hrdmast Where Mngr_Code = '" & Session("LoginUser").UserID & "'))" & Session("UserCodes")
            'Else
            '    StrSql = "SELECT * FROM HRDMASTQRY WHERE COMP_CODE = '" & Session("LoginUser").CurrentCompID & "' AND EMP_CODE = '" & Session("LoginUser").UserId & "'" & Session("UserCodes")
            'End If

            If cmbSearchFld.SelectedItem.Text <> "All" Then
                StrSql &= " And " & SearchText()
            End If

            If cmbPageStyle.SelectedIndex >= 0 Then GrdEmployee.PagerStyle.Mode = cmbPageStyle.SelectedIndex
            If Trim(txtNumRec.Text) = "" Or ChkN(txtNumRec.Text) < 1 Then
                txtNumRec.Text = 1
            End If

            GrdEmployee.PageSize = ChkN(txtNumRec.Text)
            Session("DalObj").GetSqlDataTable(dtEmployee, StrSql)
            GrdEmployee.DataSource = dtEmployee
            GrdEmployee.DataBind()

        Catch ex As Exception
            If ex.Source = ".Net SqlClient Data Provider" Then
                SetMsg(LblMsg, "Invalid Input.")
            ElseIf ex.Message = "Invalid CurrentPageIndex value. It must be >= 0 and < the PageCount." Then
                If (GrdEmployee.CurrentPageIndex > GrdEmployee.PageCount Or GrdEmployee.CurrentPageIndex < 0) Then GrdEmployee.CurrentPageIndex = 0
                GrdEmployee.DataBind()
            Else
                SetMsg(LblMsg, ex.Message & " : (BindGrid)")
            End If
        Finally
            dtEmployee.Dispose()
            DT.Dispose()
        End Try
    End Sub

#End Region

#Region "    Employee Grid Events    "

    Private Sub GrdEmployee_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles GrdEmployee.PageIndexChanged
        Try
            If (GrdEmployee.CurrentPageIndex > GrdEmployee.PageCount Or GrdEmployee.CurrentPageIndex < 0) Then
                GrdEmployee.CurrentPageIndex = 0
            Else
                GrdEmployee.CurrentPageIndex = e.NewPageIndex
            End If
            BindGrid()
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " (GrdEmployee_PageIndexChanged)")
        End Try
    End Sub
    Private Sub GrdEmployee_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles GrdEmployee.SortCommand
        Try
            BindGrid()
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " (GrdEmployee_SortCommand)")
        End Try
    End Sub
    Private Sub GrdEmployee_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles GrdEmployee.EditCommand
        Try
            Session("EM_CD") = e.Item.Cells(1).Text
            Response.Redirect("GeneralInfo.aspx?SrNo=63")
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " (GrdEmployee_EditCommand)")
        End Try
    End Sub

    Private Sub GrdEmployee_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles GrdEmployee.ItemDataBound
        If (GrdEmployee.CurrentPageIndex > GrdEmployee.PageCount Or GrdEmployee.CurrentPageIndex < 0) Then GrdEmployee.CurrentPageIndex = 0
    End Sub

#End Region

#Region "    Search Text Coding    "

    Private Sub txtSearchText_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchText.TextChanged
        Try
            'If (GrdEmployee.CurrentPageIndex > GrdEmployee.PageCount Or GrdEmployee.CurrentPageIndex < 0) Then
            'GrdEmployee.CurrentPageIndex = 0
            'Else
            '    GrdEmployee.CurrentPageIndex = e.NewPageIndex
            'End If
            'If (GrdEmployee.CurrentPageIndex > GrdEmployee.PageCount Or GrdEmployee.CurrentPageIndex < 0) Then GrdEmployee.CurrentPageIndex = 0

        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " (GrdEmployee_EditCommand)")
        End Try
    End Sub

    Private Function SearchText() As String
        Try

            Dim strResult As String, strSearchText As String
            strSearchText = Trim(Replace(txtSearchText.Text, "'", ""))
            Select Case cmbSign.SelectedIndex
                Case 1
                    strResult = "[" & cmbSearchFld.SelectedValue & "] Like '" & strSearchText & "%'"
                Case 2
                    strResult = "[" & cmbSearchFld.SelectedValue & "] Like '%" & strSearchText & "'"
                Case 3
                    strResult = "[" & cmbSearchFld.SelectedValue & "] Like '%" & strSearchText & "%'"
                Case Else
                    strResult = "[" & cmbSearchFld.SelectedValue & "] " & cmbSign.SelectedValue & " '" & strSearchText & "'"
            End Select
            Return strResult
        Catch ex As Exception
            SearchText = "1 = 1"
            SetMsg(LblMsg, ex.Message & " : (SearchText())")
        End Try
    End Function


#End Region

    Private Sub FillCombo()
        Try
            cmbSign.Items.Clear()
            cmbSign.Items.Add(New ListItem("Equal to", "="))
            cmbSign.Items.Add(New ListItem("Start With", "Like%"))
            cmbSign.Items.Add(New ListItem("Ends With", "%Like"))
            cmbSign.Items.Add(New ListItem("Contains", "%Like%"))
            cmbSign.Items.Add(New ListItem("Less Than", "<"))
            cmbSign.Items.Add(New ListItem("Less Than Equal To", "<="))
            cmbSign.Items.Add(New ListItem("Greater Than", ">"))
            cmbSign.Items.Add(New ListItem("Greater Than Equal To", ">="))
            cmbSign.Items.Add(New ListItem("Not Equal To", "<>"))
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (FillCombo())")
        End Try
    End Sub

    Private Sub cmdNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNew.Click
        Try
            Dim newEmp_Code As String, strPrefix As String, strLen As Single, EmpBool As String

            EmpBool = Chk(Session("DalObj").ExecuteCommand("Select Emp_Code_GEN From CompMast", , DalObj.ExecutionType.ExecuteScalar))

            If UCase(EmpBool) = "Y" Then
                newEmp_Code = Session("BalObj").GetNextNumber("HrdMast", "Emp_Code")
                strPrefix = Chk(Session("DalObj").ExecuteCommand("Select Emp_Code_Prefix From CompMast", , DalObj.ExecutionType.ExecuteScalar))
                strLen = ChkN(Session("DalObj").ExecuteCommand("Select Emp_Code_Len From CompMast", , DalObj.ExecutionType.ExecuteScalar))
                Session("EM_CD") = Left(strPrefix & newEmp_Code.PadLeft(strLen, "0"), strLen)
            Else
                Session("EM_CD") = ""
            End If

            'newEmp_Code = Session("BalObj").GetNextNumber("HrdMast", "Emp_Code")
            'strPrefix = Chk(Session("DalObj").ExecuteCommand("Select Emp_Code_Prefix From CompMast", , DalObj.ExecutionType.ExecuteScalar))
            'strLen = ChkN(Session("DalObj").ExecuteCommand("Select Emp_Code_Len From CompMast", , DalObj.ExecutionType.ExecuteScalar))
            'Session("EM_CD") = Left(strPrefix & newEmp_Code.PadLeft(strLen, "0"), strLen)
            Response.Redirect("GeneralInfo.aspx?QstrAction=New&SrNo=63")
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " (cmdNew_Click)")
        End Try
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub
    Public Sub SetPf()
        Try
            Dim Count As Int16
            Count = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(" Select Count(*) From HRDMAST WHERE EMP_CODE = '" & Session("LoginUser").UserId & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
            If Count = 0 Then TrPf.Visible = False : TrPfLabel.Visible = False : Exit Sub
            TrPfLabel.Visible = True
            TrPf.Visible = True
            Dim StrQry As String, DtTable As New DataTable
            StrQry = " Select * From PFUSER Where EMP_CODE = '" & Session("LoginUser").UserId & "'"
            CType(Session("DalObj"), DAL.DataLayer).GetSqlDataTable(DtTable, StrQry)
            If DtTable.Rows.Count = 0 Then Exit Sub
            TxtUserId.Text = ChkN(DtTable.Rows(0).Item("UserId"))
            TxtUsrPwd.Text = Chk(DtTable.Rows(0).Item("Password"))
            TxtCompCode.Text = Chk(DtTable.Rows(0).Item("CompCode"))

        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " (Set PF)")
        End Try
    End Sub
    Private Sub CmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdSearch.Click
        Try
            GrdEmployee.CurrentPageIndex = 0
            BindGrid()
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " (Search)")
        End Try
    End Sub
    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub
End Class
