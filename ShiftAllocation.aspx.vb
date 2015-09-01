Partial Class ShiftAllocation
    Inherits System.Web.UI.Page

    Dim DtTemp As DataTable
    Dim StrWeekly() As String
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

        If Not IsPostBack Then
            BindGrid("ds")
            Session("BalObj").FillCombo(cmbCode, "Select Emp_Code, (isnull(fname,'') + ' ' +isnull(lname,'')) as EName from HrdMastQry Order By FNAME", True)
            Session("BalObj").FillCombo(cmbFShift, "Select SHIFT_CODE, SHIFT_NAME From SHIFTMAST", True)
            Session("BalObj").FillCombo(cmbDept, "Select DEPT_CODE, DEPT_NAME from DEPTMAST", True)
            dtpWEF.DateValue = Date.Today
        End If
    End Sub

#Region "  Save Records  "

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim Tran As SqlClient.SqlTransaction

        Try

            cmdShow_Click(sender, e)

            If Not IsValidate() Then Exit Sub

            Tran = Session("DalObj").StartTransaction("Save")

            Dim i As Int16, StrQry, StrWeeklyOff As String

            If ChkWeeklyOff.Items(0).Selected = True Then
                StrWeeklyOff = StrWeeklyOff & "1,"
            End If
            If ChkWeeklyOff.Items(1).Selected = True Then
                StrWeeklyOff = StrWeeklyOff & "2,"
            End If
            If ChkWeeklyOff.Items(2).Selected = True Then
                StrWeeklyOff = StrWeeklyOff & "3,"
            End If
            If ChkWeeklyOff.Items(3).Selected = True Then
                StrWeeklyOff = StrWeeklyOff & "4,"
            End If
            If ChkWeeklyOff.Items(4).Selected = True Then
                StrWeeklyOff = StrWeeklyOff & "5,"
            End If
            If ChkWeeklyOff.Items(5).Selected = True Then
                StrWeeklyOff = StrWeeklyOff & "6,"
            End If
            If ChkWeeklyOff.Items(6).Selected = True Then
                StrWeeklyOff = StrWeeklyOff & "7,"
            End If

            If StrWeeklyOff <> "" Then
                StrWeeklyOff = Mid(StrWeeklyOff, 1, Len(StrWeeklyOff) - 1)
            End If

            For i = 0 To DtTemp.Rows.Count - 1

                Session("DalObj").ExecuteCommand(" Delete From HRDSHIFT Where EMP_CODE = '" & Chk(DtTemp.Rows(i).Item("EMP_CODE")) & "' " & _
                                               " and Shift_WEF='" & Format(dtpWEF.DateValue, "dd/MMM/yyyy") & "'", Tran)

                StrQry = " Insert HRDSHIFT(EMP_CODE,SHIFT_WEF,SHIFT_CODE,WEEKLYOFF) Values ('" & _
                     Chk(DtTemp.Rows(i).Item("EMP_CODE")) & "', '" & _
                     Format(dtpWEF.DateValue, "dd/MMM/yyyy") & "', '" & _
                     Chk(cmbFShift.SelectedValue) & "', '" & _
                     StrWeeklyOff & "' )"

                Session("DalObj").ExecuteCommand(StrQry, Tran)

            Next

            Tran.Commit()
            cmdShow_Click(sender, e)
            SetMsg(lblMsg, "Record(s) Saved Successfully")
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : cmdSave_Click()")
            Tran.Rollback()
        Finally
            If Not IsNothing(Tran) Then Tran.Dispose()
        End Try
    End Sub

    Function IsValidate() As Boolean
        Try
            If DtTemp.Rows.Count = 0 Then
                SetMsg(lblMsg, "No Employee(s) selected, Records Not Saved.")
                Return False
            End If

            If Chk(cmbFShift.SelectedValue) = "" Then
                SetMsg(lblMsg, "Select Shift from the list, Records Not Saved.")
                Return False
            End If

            Return True
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : Validate Records()")
        End Try

    End Function

#End Region

    'Sub CountWeeklyOff()
    '    Try
    '        Dim i As Int16, StrWeeklyOff As String

    '        For i = 0 To GrdShift.Items.Count - 1
    '            If Not IsNothing(CType(GrdShift.Items(i).FindControl("ChkSun"), CheckBox)) Then

    '                If CType(GrdShift.Items(i).FindControl("ChkSun"), CheckBox).Checked Then
    '                    StrWeeklyOff = StrWeeklyOff & "1,"
    '                ElseIf CType(GrdShift.Items(i).FindControl("ChkMon"), CheckBox).Checked Then
    '                    StrWeeklyOff = StrWeeklyOff & "2,"
    '                ElseIf CType(GrdShift.Items(i).FindControl("ChkTue"), CheckBox).Checked Then
    '                    StrWeeklyOff = StrWeeklyOff & "3,"
    '                ElseIf CType(GrdShift.Items(i).FindControl("ChkWed"), CheckBox).Checked Then
    '                    StrWeeklyOff = StrWeeklyOff & "4,"
    '                ElseIf CType(GrdShift.Items(i).FindControl("ChkThu"), CheckBox).Checked Then
    '                    StrWeeklyOff = StrWeeklyOff & "5,"
    '                ElseIf CType(GrdShift.Items(i).FindControl("ChkFri"), CheckBox).Checked Then
    '                    StrWeeklyOff = StrWeeklyOff & "6,"
    '                ElseIf CType(GrdShift.Items(i).FindControl("ChkSat"), CheckBox).Checked Then
    '                    StrWeeklyOff = StrWeeklyOff & "7,"
    '                End If
    '            End If
    '        Next

    '        StrWeeklyOff = Mid(StrWeeklyOff, 1, Len(StrWeeklyOff) - 1)

    '        If StrWeeklyOff <> "" Then
    '            StrWeekly = Split(StrWeeklyOff, ",")
    '        End If
    '    Catch ex As Exception
    '        SetMsg(lblMsg, ex.Message & " : CountWeeklyOff()")
    '    End Try

    'End Sub

    Private Sub GrdShift_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles GrdShift.PageIndexChanged
        Try
            If (GrdShift.CurrentPageIndex > GrdShift.PageCount Or GrdShift.CurrentPageIndex < 0) Then
                GrdShift.CurrentPageIndex = 0
            Else
                GrdShift.CurrentPageIndex = e.NewPageIndex
            End If
            cmdShow_Click(Nothing, Nothing)
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : " & ex.Source)
        End Try
    End Sub

    Private Sub cmbCode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCode.SelectedIndexChanged
        Try
            cmbCode.Visible = False
            btnList.Visible = True
            TxtCode.Visible = True
            TxtCode.Text = Chk(cmbCode.SelectedValue)
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : " & ex.Source)
        End Try
    End Sub

    Private Sub btnList_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnList.Click
        TxtCode.Visible = False
        btnList.Visible = False
        cmbCode.Visible = True
    End Sub

#Region " Fill Employee Shift Grid  "

    Sub BindGrid()
        Try
            lblMsg.Text = ""
            Dim StrQuery As String

            StrQuery = " SELECT HM.EMP_CODE, EMP_NAME, HS.WeeklyOff, (Right('00'+DateName(d,Max(HS.SHIFT_WEF)),2)+'/'+left(DateName(m,Max(HS.SHIFT_WEF)),3)+'/'+DateName(yy,Max(HS.SHIFT_WEF))) as WEF, HM.SHIFT_CODE, HM.SHIFT_NAME, '' as Sun, '' as Mon,'' as Tue,'' as Wed,'' as Thu,'' as Fri,'' as Sat  " & _
                       " FROM HRDMASTQRY HM LEFT JOIN HRDSHIFT HS ON HM.EMP_CODE = HS.EMP_CODE Group By HM.EMP_CODE, EMP_NAME, HM.Shift_Code, HM.SHIFT_NAME,  WeeklyOff Order By HM.EMP_CODE "

            DtTemp = New DataTable
            Session("DalObj").GetSqlDataTable(DtTemp, StrQuery)
            GrdShift.DataSource = DtTemp
            GrdShift.DataBind()
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : BindGrid()")
        End Try
    End Sub

    Sub BindGrid(ByVal Code As String)
        Try
            Dim StrQuery As String

            StrQuery = " SELECT HM.EMP_CODE, EMP_NAME, HS.WeeklyOff, (Right('00'+DateName(d,Max(HS.SHIFT_WEF)),2)+'/'+left(DateName(m,Max(HS.SHIFT_WEF)),3)+'/'+DateName(yy,Max(HS.SHIFT_WEF))) as WEF, HM.SHIFT_CODE, HM.SHIFT_NAME , '' as Sun, '' as Mon,'' as Tue,'' as Wed,'' as Thu,'' as Fri,'' as Sat  " & _
                       " FROM HRDMASTQRY HM LEFT JOIN HRDSHIFT HS ON HM.EMP_CODE = HS.EMP_CODE Where HM.EMP_CODE = '" & Code & "' Group By HM.EMP_CODE, EMP_NAME, HM.Shift_Code,HM.SHIFT_NAME, WeeklyOff Order By HM.EMP_CODE "

            DtTemp = New DataTable
            Session("DalObj").GetSqlDataTable(DtTemp, StrQuery)
            GrdShift.DataSource = DtTemp
            GrdShift.DataBind()
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : BindGrid()")
        End Try
    End Sub

    Private Sub GrdShift_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles GrdShift.ItemDataBound
        Try

            Dim i, j, k As Int16
            i = e.Item.ItemIndex

            'If IsNothing(CType(e.Item.FindControl("cmbShift"), DropDownList)) Then Exit Sub

            'Dim Cmb As DropDownList = CType(e.Item.FindControl("cmbShift"), DropDownList)
            'Session("BalObj").FillCombo(Cmb, " Select SHIFT_CODE, SHIFT_NAME From SHIFTMAST ", True)

            'ChkCombo(Cmb, DtTemp.Rows(i).Item("SHIFT_CODE"))

            'If Not IsNothing(DtTemp.Rows(i).Item("SHIFT_CODE")) Then
            'CType(e.Item.FindControl("TxtWEF"), TextBox).Text = Format(CDate(DtTemp.Rows(i).Item("WEF")), "dd/MMM/yyyy")
            'End If

            If IsNothing(CType(e.Item.FindControl("ChkSun"), CheckBox)) Then Exit Sub
            Dim StrCode() As String
            StrCode = Split(DtTemp.Rows(i).Item("WeeklyOff"), ",")

            For k = 0 To StrCode.Length - 1
                Select Case StrCode(k)
                    Case 1
                        CType(e.Item.FindControl("ChkSun"), CheckBox).Checked = True
                    Case 2
                        CType(e.Item.FindControl("ChkMon"), CheckBox).Checked = True
                    Case 3
                        CType(e.Item.FindControl("ChkTue"), CheckBox).Checked = True
                    Case 4
                        CType(e.Item.FindControl("ChkWed"), CheckBox).Checked = True
                    Case 5
                        CType(e.Item.FindControl("ChkThu"), CheckBox).Checked = True
                    Case 6
                        CType(e.Item.FindControl("ChkFri"), CheckBox).Checked = True
                    Case 7
                        CType(e.Item.FindControl("ChkSat"), CheckBox).Checked = True
                End Select
            Next
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : BindGrid()")
        End Try
    End Sub

#End Region

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Private Sub cmdShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShow.Click
        'qrylatlev = SELECT HM.EMP_CODE, Max(hs.Shift_wef) as Shift_wef FROM HRDMASTQRY HM LEFT JOIN HRDSHIFT HS ON HM.EMP_CODE = HS.EMP_CODE Group By HM.EMP_CODE 

        Try
            lblMsg.Text = ""
            Dim StrQuery As String
            If TxtCode.Text <> "" And cmbDept.SelectedValue <> "" Then

                StrQuery = " SELECT HM.EMP_CODE, EMP_NAME, HS.WeeklyOff, " & _
                           " (Right('00'+DateName(d,Max(HS.SHIFT_WEF)),2)+'/'+left(DateName(m,Max(HS.SHIFT_WEF)),3)+'/'+DateName(yy,Max(HS.SHIFT_WEF))) as WEF, " & _
                           " HM.SHIFT_CODE, HM.SHIFT_NAME, '' as Sun, '' as Mon,'' as Tue,'' as Wed,'' as Thu,'' as Fri,'' as Sat " & _
                           " FROM HRDMASTQRY HM LEFT JOIN HRDSHIFT HS ON HM.EMP_CODE = HS.EMP_CODE  " & _
                           " inner join qrylatlev On qrylatlev.Emp_Code = HM.EMP_CODE and qrylatlev.Shift_WEF = HS.Shift_WEF " & _
                           " Where HM.EMP_CODE = '" & Chk(TxtCode.Text) & "' and HM.Dept_Code= '" & Chk(cmbCode.SelectedValue) & "' Group By HM.EMP_CODE, EMP_NAME, HM.Shift_Code, HM.SHIFT_NAME,WeeklyOff  "

                'StrQuery = " SELECT HM.EMP_CODE, EMP_NAME, max(HS.WeeklyOff), (Right('00'+DateName(d,Max(HS.SHIFT_WEF)),2)+'/'+left(DateName(m,Max(HS.SHIFT_WEF)),3)+'/'+DateName(yy,Max(HS.SHIFT_WEF))) as WEF, HM.SHIFT_CODE, HM.SHIFT_NAME, '' as Sun, '' as Mon,'' as Tue,'' as Wed,'' as Thu,'' as Fri,'' as Sat  " & _
                '          " FROM HRDMASTQRY HM LEFT JOIN HRDSHIFT HS ON HM.EMP_CODE = HS.EMP_CODE Where HM.EMP_CODE = '" & Chk(TxtCode.Text) & "' and HM.Dept_Code= '" & Chk(cmbCode.SelectedValue) & "' Group By HM.EMP_CODE, EMP_NAME, HM.Shift_Code, HM.SHIFT_NAME Order By HM.EMP_CODE "

            ElseIf TxtCode.Text = "" And cmbDept.SelectedValue <> "" Then

                StrQuery = " SELECT HM.EMP_CODE, EMP_NAME, HS.WeeklyOff, " & _
                           " (Right('00'+DateName(d,Max(HS.SHIFT_WEF)),2)+'/'+left(DateName(m,Max(HS.SHIFT_WEF)),3)+'/'+DateName(yy,Max(HS.SHIFT_WEF))) as WEF, " & _
                           " HM.SHIFT_CODE, HM.SHIFT_NAME, '' as Sun, '' as Mon,'' as Tue,'' as Wed,'' as Thu,'' as Fri,'' as Sat " & _
                           " FROM HRDMASTQRY HM LEFT JOIN HRDSHIFT HS ON HM.EMP_CODE = HS.EMP_CODE  " & _
                           " inner join qrylatlev On qrylatlev.Emp_Code = HM.EMP_CODE and qrylatlev.Shift_WEF = HS.Shift_WEF " & _
                           " Where HM.Dept_Code= '" & Chk(cmbDept.SelectedValue) & "' Group By HM.EMP_CODE, EMP_NAME, HM.Shift_Code, HM.SHIFT_NAME,WeeklyOff  "

                'StrQuery = " SELECT HM.EMP_CODE, EMP_NAME, max(HS.WeeklyOff), (Right('00'+DateName(d,Max(HS.SHIFT_WEF)),2)+'/'+left(DateName(m,Max(HS.SHIFT_WEF)),3)+'/'+DateName(yy,Max(HS.SHIFT_WEF))) as WEF, HM.SHIFT_CODE, HM.SHIFT_NAME, '' as Sun, '' as Mon,'' as Tue,'' as Wed,'' as Thu,'' as Fri,'' as Sat  " & _
                '          " FROM HRDMASTQRY HM LEFT JOIN HRDSHIFT HS ON HM.EMP_CODE = HS.EMP_CODE Where HM.Dept_Code= '" & Chk(cmbDept.SelectedValue) & "' Group By HM.EMP_CODE, EMP_NAME, HM.Shift_Code, HM.SHIFT_NAME Order By HM.EMP_CODE "

            Else

                StrQuery = " SELECT HM.EMP_CODE, EMP_NAME, HS.WeeklyOff, " & _
                           " (Right('00'+DateName(d,Max(HS.SHIFT_WEF)),2)+'/'+left(DateName(m,Max(HS.SHIFT_WEF)),3)+'/'+DateName(yy,Max(HS.SHIFT_WEF))) as WEF, " & _
                           " HM.SHIFT_CODE, HM.SHIFT_NAME, '' as Sun, '' as Mon,'' as Tue,'' as Wed,'' as Thu,'' as Fri,'' as Sat " & _
                           " FROM HRDMASTQRY HM LEFT JOIN HRDSHIFT HS ON HM.EMP_CODE = HS.EMP_CODE  " & _
                           " inner join qrylatlev On qrylatlev.Emp_Code = HM.EMP_CODE and qrylatlev.Shift_WEF = HS.Shift_WEF " & _
                           " Where HM.EMP_CODE = '" & Chk(TxtCode.Text) & "' Group By HM.EMP_CODE, EMP_NAME, HM.Shift_Code, HM.SHIFT_NAME,WeeklyOff  "

                'StrQuery = " SELECT HM.EMP_CODE, EMP_NAME, max(HS.WeeklyOff), (Right('00'+DateName(d,Max(HS.SHIFT_WEF)),2)+'/'+left(DateName(m,Max(HS.SHIFT_WEF)),3)+'/'+DateName(yy,Max(HS.SHIFT_WEF))) as WEF, HM.SHIFT_CODE, HM.SHIFT_NAME, '' as Sun, '' as Mon,'' as Tue,'' as Wed,'' as Thu,'' as Fri,'' as Sat  " & _
                '          " FROM HRDMASTQRY HM LEFT JOIN HRDSHIFT HS ON HM.EMP_CODE = HS.EMP_CODE Where HM.EMP_CODE = '" & Chk(TxtCode.Text) & "' Group By HM.EMP_CODE, EMP_NAME, HM.Shift_Code, HM.SHIFT_NAME Order By HM.EMP_CODE "

            End If

            DtTemp = New DataTable
            Session("DalObj").GetSqlDataTable(DtTemp, StrQuery)
            GrdShift.CurrentPageIndex = 0
            GrdShift.DataSource = DtTemp
            GrdShift.DataBind()

        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : Show Grid()")
        End Try

    End Sub

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub

    Private Sub GrdShift_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GrdShift.SelectedIndexChanged

    End Sub
End Class
