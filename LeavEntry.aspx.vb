Partial Class LeavEntry
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim SQLStr, StrSql, Value, LocCode, WOFF As String, i As Int16, OkForSave As Boolean
    Dim DtHolidays As New DataTable
    Dim DtHoly As New DataTable
    Dim DtDelete As New DataTable
    Dim Item As ListItem
    Dim TotLVDate() As String
    Dim d As New DAL.DataLayer
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If

        Try
            'By Ravi 21 Nov
            Dim SrNo As String
            SrNo = Request.QueryString.Item("SrNo") + Request.QueryString.Item("MODULEID")
            Dim bSuccess As Boolean
            Select Case CheckRight(SrNo)
                Case MdlHRMS.AccessType.FullAccess, MdlHRMS.AccessType.SuperUser
                    bSuccess = True
                Case MdlHRMS.AccessType.ReadonlyAccess, MdlHRMS.AccessType.Restricted
                    bSuccess = False
            End Select
            cmdSave.Visible = bSuccess
            cmdDelete.Visible = bSuccess
            cmdProceed.Visible = bSuccess

            '------------------------------------

            'If CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules <> "" Then
            '    If InStr(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, SrNo & "-") <> 0 Then
            '        Dim int As Int16, st As String
            '        int = InStr(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, SrNo & "-")
            '        st = Right(Mid(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, int, Len(SrNo & "-") + 1), 1)

            '        'If st = "S" Then
            '        'Else
            '        '   cmdSave.Visible = False
            '        'End If
            'Else
            '    Response.Redirect("Main.aspx")
            'End If
            'End If

            If Not IsPostBack Then

                ViewState("DtTmpHolidays") = New DataTable
                'dDate = Session("DalObj").ExecuteCommand("Select LEV_YR_ST From FinYear where LEV_YR_CUR='Y'", , DAL.DataLayer.ExecutionType.ExecuteScalar)

                For i = 1 To 12
                    'Comment By Ravi
                    'Value = MonthName(Month(LY_Start)) & " " & DatePart(DateInterval.Year, LY_Start)
                    'Item = New ListItem(Value, Month(LY_Start) + 1 & "/" & Year(LY_Start))
                    'cmbLvAdjIn.Items.Add(Item)
                    'LY_Start = DateAdd(DateInterval.Month, 1, LY_Start)

                    'By Ravi on 18 dec 2006
                    Item = New ListItem
                    Item.Text = MonthName(Month(LY_Start)) & " " & Year(LY_Start)
                    Item.Value = EOM(LY_Start)
                    cmbLvAdjIn.Items.Add(Item)
                    LY_Start = Item.Value
                    LY_Start = DateAdd(DateInterval.Month, 1, LY_Start)
                Next


                cmbLvAdjIn.Items.Add("")
                cmbLvAdjIn.SelectedIndex = cmbLvAdjIn.Items.Count - 1
                cmbLvAdjIn.DataBind()

                Session("BalObj").fillcombo(cmbEmp, "Select Emp_Code, Emp_Name as FNAME from HrdMastQry Where LType=1 " & Session("UserCodes") & " Order by FNAME")
                Session("BalObj").fillcombo(cmbLvType, "Select LvType, LvDesc from LvHelp Order By LvDesc")
                Session("BalObj").fillcombo(cmbShift, "Select Shift_Code, Shift_Name from ShiftMast")
                cmbEmp.Items.Add("")
                cmbEmp.SelectedIndex = cmbEmp.Items.Count - 1
                cmbLvType.Items.Add("")
                cmbLvType.SelectedIndex = cmbLvType.Items.Count - 1
                cmbShift.Items.Add("")
                cmbShift.SelectedIndex = cmbShift.Items.Count - 1
                dtpFromDate.DateValue = Format(Date.Today, "dd/MMM/yyyy")
                DtpTo.DateValue = Format(Date.Today, "dd/MMM/yyyy")
                DtpAppDate.DateValue = Format(Date.Today, "dd/MMM/yyyy")
                T1.Visible = False

                TxtCode.Text = 0
                TxtCode_TextChanged(sender, e)
                TxtCode.Text = ""

                'SQLStr = "Select Cast(Datename(D,HDATE) + '/' + Left(datename(M,HDATE),3) + '/' + dateName(yy,HDATE) As VarChar) As HDate, HDESC As Description from Holidays where LOC_CODE='0' and HDate between '01/Jan/1901' AND '01/Jan/1901'"
                SQLStr = "Select HDate, HDESC from Holidays where isnull(TYPE,'')='N' and  LOC_CODE='0' and HDate between '01/Jan/1901' AND '01/Jan/1901'"
                Session("DalObj").GetSqlDataTable(DtHoly, SQLStr)
                GrdHolidays.DataSource = DtHoly
                GrdHolidays.DataBind()
            End If

            cmdSave.Attributes.Add("onclick", "return ValidateCtrl();")

        Catch ex As Exception
            SetMsg(lblMsg, ex.Message)
        End Try
    End Sub

    Private Sub CheckAvailibility()

        If IsDate(dtpFromDate.DateValue) Or IsDate(DtpTo.DateValue) Then 'Change By Arun
            Dim count, Already As Int16, str As String, RHCount As Int16, LvType As String
            Dim atdate As DateTime
            Dim TotalDays As Int16 = 0
            Dim dvTemp As New DataView
            Try
                lblAlready.Text = ""
                lblMsg.Text = ""
                ViewState("Applied") = ""
                ViewState("WeekOff") = ""
                ViewState("LVDATE") = ""

                If Left(cmbLvType.SelectedValue, 1) <> "P" And Right(cmbLvType.SelectedValue, 1) <> "P" Then
                    LvType = Left(cmbLvType.SelectedValue, 1)
                ElseIf Left(cmbLvType.SelectedValue, 1) = "P" Then
                    LvType = Right(cmbLvType.SelectedValue, 1)
                Else
                    LvType = Left(cmbLvType.SelectedValue, 1)
                End If

                Dim Cnt As Int16
                Cnt = ChkN(CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(" Select Count(*) From LvType Where LVTYPE='" & LvType & "' And isnull(AddHolidays,0)=0", , DAL.DataLayer.ExecutionType.ExecuteScalar))


                '=============================If Leave Type is Restricted Holidays(RH)===================================
                If InStr(Chk(cmbLvType.SelectedValue), "R") > 0 Then
                    If CDate(dtpFromDate.DateValue) <> CDate(DtpTo.DateValue) Then
                        SetMsg(lblMsg, "For Restricted Holiday Leave, From and To Date must be same.")
                        Exit Sub
                    End If
                    StrSql = "Select Count(*) From Holidays where isnull(TYPE,'')='RH' and loc_code='" & ViewState("LocCode") & "' and hdate ='" & Format(CDate(dtpFromDate.DateValue), "dd/MMM/yyyy") & "'"
                    RHCount = Session("DalObj").ExecuteCommand(StrSql, , DAL.DataLayer.ExecutionType.ExecuteScalar)
                    If RHCount <> 0 Then
                        str = "Select count(*) from LeavTran Where emp_code='" & ViewState("Code") & "' and  Atdate ='" & Format(CDate(dtpFromDate.DateValue), "dd/MMM/yyyy") & "' AND LEVYEAR='" & Session("LeavYear") & "'"
                        Already = ChkN(Session("DalObj").ExecuteCommand(str, , DAL.DataLayer.ExecutionType.ExecuteScalar))
                        If Already = 0 Then
                            ViewState("LVDATE") = ViewState("LVDATE") & Format(CDate(dtpFromDate.DateValue), "dd/MMM/yyyy") & "|"
                            TotalDays = TotalDays + 1
                        Else
                            ViewState("Applied") = ViewState("Applied") & Format(CDate(dtpFromDate.DateValue), "dd/MMM/yyyy") & "|"
                        End If
                    Else
                        SetMsg(lblMsg, "Selected Date is not defined as Restricted Holiday.")
                    End If
                    GoTo [Continue]
                End If

                '========================If Holidays is not included.================================
                If Cnt = 1 Then
                    For count = 0 To ChkN(DateDiff(DateInterval.Day, CDate(dtpFromDate.DateValue), CDate(DtpTo.DateValue)))
                        atdate = DateAdd(DateInterval.Day, count, CDate(dtpFromDate.DateValue))
                        str = "Select count(*) from LeavTran Where emp_code='" & ViewState("Code") & "' and  Atdate ='" & Format(atdate, "dd/MMM/yyyy") & "' AND LEVYEAR='" & Session("LeavYear") & "'"
                        Already = ChkN(Session("DalObj").ExecuteCommand(str, , DAL.DataLayer.ExecutionType.ExecuteScalar))
                        If Already = 0 Then
                            ViewState("LVDATE") = ViewState("LVDATE") & Format(atdate, "dd/MMM/yyyy") & "|"
                            TotalDays = TotalDays + 1
                        Else
                            ViewState("Applied") = ViewState("Applied") & Format(atdate, "dd/MMM/yyyy") & "|"
                        End If
                    Next
                    GoTo [Continue]
                End If

                '=====================================================================================

                StrSql = "select * from holidays where isnull(TYPE,'')='N' and loc_code='" & ViewState("LocCode") & "' and hdate between '" & Format(CDate(dtpFromDate.DateValue), "dd/MMM/yyyy") & "' and '" & Format(CDate(DtpTo.DateValue), "dd/MMM/yyyy") & "'"
                Session("DalObj").GetSqlDataTable(DtHolidays, StrSql)

                For count = 0 To ChkN(DateDiff(DateInterval.Day, CDate(dtpFromDate.DateValue), CDate(DtpTo.DateValue)))
                    atdate = DateAdd(DateInterval.Day, count, CDate(dtpFromDate.DateValue))

                    StrSql = "select isnull(WeeklyOff,'') from hrdshift where emp_code='" & ViewState("Code") & "' and shift_WEF < '" & Format(atdate, "dd/MMM/yyyy") & "' order by Shift_WEF desc"
                    WOFF = Session("dalobj").ExecuteCommand(StrSql, , DAL.DataLayer.ExecutionType.ExecuteScalar)

                    dvTemp = Session("DalObj").GetDataView(DtHolidays, "", "Hdate='" & Format(atdate, "dd/MMM/yyyy") & "'")
                    If dvTemp.Count = 0 And InStr(WOFF, Weekday(CDate(atdate))) = 0 Then
                        str = "Select count(*) from LeavTran Where emp_code='" & ViewState("Code") & "' and  Atdate ='" & Format(atdate, "dd/MMM/yyyy") & "' AND LEVYEAR='" & Session("LeavYear") & "'"
                        Already = ChkN(Session("DalObj").ExecuteCommand(str, , DAL.DataLayer.ExecutionType.ExecuteScalar))
                        If Already = 0 Then
                            ViewState("LVDATE") = ViewState("LVDATE") & Format(atdate, "dd/MMM/yyyy") & "|"
                            TotalDays = TotalDays + 1
                        Else
                            ViewState("Applied") = ViewState("Applied") & Format(atdate, "dd/MMM/yyyy") & "|"
                        End If
                    ElseIf WOFF <> "" Then
                        ViewState("WeekOff") = ViewState("WeekOff") & Format(atdate, "dd/MMM/yyyy") & "|"
                    End If
                Next

[Continue]:
                TxtDays.Text = ChkN(TotalDays)
                If Not IsNothing(ViewState("Applied")) Then
                    If Len(ViewState("Applied")) <> 0 Then
                        TotLVDate = Split(Mid(ViewState("Applied"), 1, Len(ViewState("Applied")) - 1), "|")
                        Dim sStr As String
                        For i = 0 To TotLVDate.Length - 1
                            sStr = sStr & "Date " & TotLVDate(i) & " : Already Exist. <BR>"
                        Next
                        SetMsg(lblAlready, sStr)
                    End If
                End If
            Catch ex As Exception
                SetMsg(lblMsg, ex.Message)
            Finally
                dvTemp.Dispose()
            End Try
        End If
    End Sub

    Public Sub ShowHolidays()
        Try
            Dim TotSun(), TotWeekOff() As String, i As Int16, TempRow As DataRow

            ViewState("DtTmpHolidays").Rows.Clear()
            TotWeekOff = Split(ViewState("WeekOff"), "|")
            SQLStr = "Select HDate, HDESC from Holidays where isnull(TYPE,'')='N' and LOC_CODE='" & ViewState("LocCode") & "' and HDate between '" & Format(CDate(dtpFromDate.DateValue), "dd/MMM/yyyy") & "' AND '" & Format(CDate(DtpTo.DateValue), "dd/MMM/yyyy") & "'"
            Session("DalObj").GetSqlDataTable(ViewState("DtTmpHolidays"), SQLStr)

            For i = 0 To TotWeekOff.Length - 1
                If TotWeekOff(i) <> "" Then
                    TempRow = ViewState("DtTmpHolidays").NewRow
                    TempRow.Item("HDATE") = Format(CDate(TotWeekOff(i)), "dd/MMM/yyyy")
                    TempRow.Item("HDESC") = "WeeklyOff"
                    ViewState("DtTmpHolidays").Rows.Add(TempRow)
                End If
            Next
            ViewState("DtTmpHolidays").AcceptChanges()
            GrdHolidays.CurrentPageIndex = 0
            BindHoliday()
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " :  ShowHolidays() ")
        End Try
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim Tran As SqlClient.SqlTransaction
        Try
            Tran = Session("DalObj").StartTransaction("Save")
            If Not IsValidate() Then Exit Sub

            Dim Full As Boolean, i As Int16

            If Left(Chk(cmbLvType.SelectedValue), 1) = "P" Or Right(Chk(cmbLvType.SelectedValue), 1) = "P" Then Full = False Else Full = True

            If Left(Chk(cmbLvType.SelectedValue), 1) = "P" And Right(Chk(cmbLvType.SelectedValue), 1) = "P" Then

                SQLStr = " Insert LeavTran ( LEVYEAR, EMP_CODE, AtDate, LVDAYS, LVTYPE, PAYDATE, LVAPPLIED, AVAILED, In_Time, Out_Time, Shift_Code ) Values ('" & _
                          Session("LeavYear") & "', '" & ViewState("Code") & "', '" & Format(CDate(dtpFromDate.DateValue), "dd/MMM/yyyy") & "', '" & _
                          "1', 'PP', Null, Null, '1', '" & Chk(TxtInTime.Value) & "', '" & Chk(TxtOutTime.Value) & "', "

                If cmbShift.SelectedValue = "" Then
                    SQLStr = SQLStr & "Null" & " )"
                Else
                    SQLStr = SQLStr & "'" & Chk(cmbShift.SelectedValue) & "' )"
                End If

                Session("DalObj").ExecuteCommand(SQLStr, Tran)
                Tran.Commit()
                Tran.Dispose()
                SetMsg(lblMsg, " Record Saved Successfully.")
                FillLeaveStatus(TxtCode.Text)
                Exit Sub
            End If

            TotLVDate = Split(Mid(ViewState("LVDATE"), 1, Len(ViewState("LVDATE")) - 1), "|")

            For i = 0 To TotLVDate.Length - 1

                '----------------------------------Insert in LeavTran---------------------------

                SQLStr = "Insert LeavTran ( LEVYEAR, EMP_CODE, AtDate, LVDAYS, LVTYPE, PAYDATE, LVAPPLIED, AVAILED, In_Time, Out_Time, Shift_Code ) Values ('" & _
                          Session("LeavYear") & "', '" & ViewState("Code") & "', '" & TotLVDate(i) & "', '"

                If Full = True Then SQLStr = SQLStr & "1.0" & "', '" Else SQLStr = SQLStr & "0.5" & "', '"

                SQLStr = SQLStr & cmbLvType.SelectedValue & "', "

                If cmbLvAdjIn.SelectedValue = "" Then
                    SQLStr = SQLStr & "Null" & ", '"
                Else
                    SQLStr = SQLStr & "'" & Format(DateAdd(DateInterval.Day, -1, CDate(cmbLvAdjIn.SelectedValue)), "dd/MMM/yyyy") & "', '"
                End If

                SQLStr = SQLStr & Format(CDate(DtpAppDate.DateValue), "dd/MMM/yyyy") & "', '1.0', '" & _
                         Chk(TxtInTime.Value) & "', '" & Chk(TxtOutTime.Value) & "', "

                If cmbShift.SelectedValue = "" Then
                    SQLStr = SQLStr & "Null" & " )"
                Else
                    SQLStr = SQLStr & "'" & Chk(cmbShift.SelectedValue) & "' )"
                End If

                Session("DalObj").ExecuteCommand(SQLStr, Tran)

                '----------------------------------Updating LeavMast---------------------------

                SQLStr = " UpDate LeavMast Set Availed ="
                If Full = True Then SQLStr = SQLStr & " (isnull(Availed, 0) + 1)" Else SQLStr = SQLStr & " (isnull(Availed, 0) + 0.5)"
                SQLStr = SQLStr & " where Emp_Code='" & ViewState("Code") & "' and LEVYEAR='" & Session("LeavYear") & "' and LVTYPE='"

                If Full = True Then
                    SQLStr = SQLStr & Left(Chk(cmbLvType.SelectedValue), 1) & "'"
                Else
                    If Left(Chk(cmbLvType.SelectedValue), 1) = "P" Then
                        SQLStr = SQLStr & Right(Chk(cmbLvType.SelectedValue), 1) & "'"
                    Else
                        SQLStr = SQLStr & Left(Chk(cmbLvType.SelectedValue), 1) & "'"
                    End If
                End If
                Session("DalObj").ExecuteCommand(SQLStr, Tran)
            Next

            Tran.Commit()
            Tran.Dispose()

            SetMsg(lblMsg, TotLVDate.Length & " Record Saved Successfully.")

            If Not IsNothing(ViewState("Applied")) Then
                If Len(ViewState("Applied")) <> 0 Then
                    TotLVDate = Split(Mid(ViewState("Applied"), 1, Len(ViewState("Applied")) - 1), "|")
                    Dim Str As String
                    For i = 0 To TotLVDate.Length - 1
                        Str = Str & "Date " & TotLVDate(i) & " : Already Exist. <BR>"
                    Next
                    SetMsg(lblAlready, Str)
                End If
            End If
            FillLeaveStatus(Chk(TxtCode.Text))
        Catch ex As Exception
            Tran.Rollback()
            SetMsg(lblMsg, ex.Message & "  " & SQLStr)
        Finally
            Tran.Dispose()
        End Try
    End Sub

    Public Function IsValidate() As Boolean
        Try
            Dim Availed, i, Result As Int16, sQuery As String
            'If Chk(TxtCode.Text, , True) = "" Then
            '    SetMsg(lblMsg, "Please select employee code from the list.")
            '    Return False
            'End If

            'If cmbLvType.SelectedIndex = cmbLvType.Items.Count - 1 Then
            '    SetMsg(lblMsg, "Please select leave type from the list")
            '    Return False
            'End If

            'If Left(cmbLvType.SelectedValue, 1) = "P" And Right(cmbLvType.SelectedValue, 1) = "P" Then
            '    If CDate(dtpFromDate.DateValue) <> CDate(DtpTo.DateValue) Then
            '        SetMsg(lblMsg, "For Present Entry,  From & To Date must be same.")
            '        Return False
            '    End If
            'End If

            'If Left(cmbLvType.SelectedValue, 1) = "P" Or Right(cmbLvType.SelectedValue, 1) = "P" Then
            '    If CDate(dtpFromDate.DateValue) <> CDate(DtpTo.DateValue) Then
            '        SetMsg(lblMsg, "For half day leave From & To Date must be same.")
            '        Return False
            '    End If
            'End If

            'If Not (Left(cmbLvType.SelectedValue, 1) = "P" And Right(cmbLvType.SelectedValue, 1) = "P") Then
            '    If ChkN(TxtDays.Text, , True) = 0 Then
            '        SetMsg(lblMsg, "Leave Day(s) must be greater than zero.")
            '        Return False
            '    End If
            'End If
            If Not Chk(cmbLvType.SelectedValue) = "PP" Then
                If InStr(Chk(cmbLvType.SelectedValue), "W") = 0 Then
                    Session("DvLeav") = Session("DalObj").GetDataView(ViewState("DtLvType"), "", "lvtype='" & IIf(Left(Chk(cmbLvType.SelectedValue), 1) = "P", Right(Chk(cmbLvType.SelectedValue), 1), Left(Chk(cmbLvType.SelectedValue), 1)) & "'")
                    If ChkN(Session("DvLeav").count) > 0 Then
                        If ChkN(TxtDays.Text) > ChkN(Session("DvLeav").Item(0).Item("Balance")) Then
                            SetMsg(lblMsg, "Available Balance is not Sufficient in this Leave Type.")
                            Return False
                        End If
                    Else
                        SetMsg(lblMsg, "No Balance Available in this Leave Type.")
                        Return False
                    End If
                End If
            End If
            If Len(ViewState("LVDATE")) <> 0 Then
                TotLVDate = Split(Mid(ViewState("LVDATE"), 1, Len(ViewState("LVDATE")) - 1), "|")
                For i = 0 To TotLVDate.Length - 1
                    sQuery = "Select Count(*) from LeavTran Where Emp_Code='" & ViewState("Code") & "' and LEVYEAR='" & Session("LeavYear") & "' and AtDate='" & Format(CDate(TotLVDate(i)), "dd/MMM/yyyy") & "'"
                    Result = Session("DalObj").ExecuteCommand(sQuery, , DAL.DataLayer.ExecutionType.ExecuteScalar)
                    If Result <> 0 Then
                        SetMsg(lblMsg, " Date : " & TotLVDate(i) & " Record Already Exist, Record Not Saved.")
                        Return False
                    End If
                Next
            Else
                SetMsg(lblMsg, "Please select the date(s) for leave entry.")
                Return False
            End If
            Return True
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message)
        End Try
    End Function

    Private Sub btnList_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnList.Click
        cmbEmp.Visible = True
        TxtCode.Visible = False
        btnList.Visible = False
    End Sub

    Private Sub cmbEmp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEmp.SelectedIndexChanged
        Try
            If cmbEmp.SelectedIndex <> cmbEmp.Items.Count - 1 Then
                TxtCode.Visible = True
                btnList.Visible = True
                cmbEmp.Visible = False
                TxtCode.Text = cmbEmp.SelectedValue
                LblName.Text = Chk(cmbEmp.SelectedItem.Text)
                TxtCode_TextChanged(sender, e)
            End If
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message)
        End Try
    End Sub

    Private Sub TxtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCode.TextChanged
        Try
            ViewState("Code") = Chk(TxtCode.Text)
            FillLeaveStatus(Chk(TxtCode.Text))
            LblName.Text = Chk(Session("DalObj").ExecuteCommand("Select Emp_Name from HrdMastQry where Emp_Code='" & TxtCode.Text & "'" & Session("UserCodes"), , DAL.DataLayer.ExecutionType.ExecuteScalar))
            SQLStr = "Select isnull(LOC_CODE,'') from HrdMast where EMP_CODE='" & ViewState("Code") & "'" & Session("UserCodes")
            ViewState("LocCode") = Session("DalObj").ExecuteCommand(SQLStr, , DAL.DataLayer.ExecutionType.ExecuteScalar)
            lblAlready.Text = ""
            lblMsg.Text = ""
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message)
        End Try
    End Sub

    Private Sub FillLeaveStatus(ByVal Code As String)
        Try
            ViewState("DtLvType") = New DataTable
            SQLStr = "Select LVDESC, (isnull(Opening,0)+isnull(Earned,0)) as Credited, isnull(AVAILED,0) as Availed, " & _
                     "((isnull(Opening,0)+isnull(Earned,0)) - isnull(AVAILED,0)) as Balance, LV.LVTYPE from LvType LV left " & _
                     "join LeavMast LM on LV.LVTYPE=LM.LVTYPE where LM.EMP_CODE='" & Chk(Code) & "' and LM.LEVYEAR='" & Session("LeavYear") & "'"
            Session("DalObj").GetSqlDataTable(ViewState("DtLvType"), SQLStr)
            GrdLeaveType.DataSource = ViewState("DtLvType")
            GrdLeaveType.DataBind()
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message)
        End Try
    End Sub

    Private Sub GrdHolidays_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles GrdHolidays.PageIndexChanged
        GrdHolidays.CurrentPageIndex = e.NewPageIndex
        BindHoliday()
    End Sub

    Private Sub GrdLeaveType_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles GrdLeaveType.PageIndexChanged
        GrdLeaveType.CurrentPageIndex = e.NewPageIndex
        GrdLeaveType.DataBind()
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        Try
            lblAlready.Text = ""
            SQLStr = "Select HM.Emp_Name, LVAPPLIED as AppDate, (case when left(L.LVTYPE,1) = right(L.LVTYPE,1) then '1' else '0.5' end) as LVDays, AtDate," & _
                     " AtDate as AttDate, L.LVTYPE, L.LVDESC from LeavTran LA " & _
                     " inner join hrdmastqry HM on LA.Emp_code = HM.emp_code inner join LvHelp L on La.LVTYPE=L.LVTYPE where HM.Emp_Code= '" & Chk(TxtCode.Text) & "' and AtDate between '" & Format(CDate(dtpFromDate.DateValue), "dd/MMM/yyyy") & "' and '" & Format(CDate(DtpTo.DateValue), "dd/MMM/yyyy") & "'"
            Session("DalObj").GetSqlDataTable(DtDelete, SQLStr)
            If DtDelete.Rows.Count <> 0 Then
                T1.Visible = True
                GrdDelete.DataSource = DtDelete
                GrdDelete.DataBind()
            Else
                SetMsg(lblAlready, "No Record found for this employee code.")
            End If
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message)
        End Try
    End Sub

    Private Sub cmdProceed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdProceed.Click
        Dim Tran As SqlClient.SqlTransaction
        Try
            Dim i As Int16, ATDate, LvType As String, Item As DataGridItem
            Dim TotAtDate(), TotLeav() As String
            LvType = ""
            ATDate = ""
            For Each Item In GrdDelete.Items
                If CType(Item.FindControl("ChkCheck"), CheckBox).Checked = True Then
                    ATDate = ATDate & Item.Cells(4).Text & "|"
                    LvType = LvType & Item.Cells(5).Text & "|"
                End If
            Next

            If Len(ATDate) <> 0 Then
                TotAtDate = Split(Mid(ATDate, 1, Len(ATDate) - 1), "|")
                TotLeav = Split(Mid(LvType, 1, Len(LvType) - 1), "|")
                Tran = Session("DalObj").StartTransaction("Proc")
            Else
                SetMsg(lblAlready, "0 Records Deleted.")
                T1.Visible = False
                Exit Sub
            End If


            '--------------------------- Deleting LeavTran ---------------------------
            SQLStr = " Delete From LeavTran where EMP_CODE='" & Chk(TxtCode.Text) & "' and AtDate in ('"

            For i = 0 To TotAtDate.Length - 1
                SQLStr = SQLStr & Format(CDate(TotAtDate(i)), "dd/MMM/yyyy") & "' ,'"
            Next

            SQLStr = Mid(SQLStr, 1, Len(SQLStr) - 2) & ")"
            Session("DalObj").ExecuteCommand(SQLStr, Tran)


            '----------------------------- Updating LeavMast --------------------------
            For i = 0 To TotAtDate.Length - 1
                SQLStr = " Update LeavMast Set Availed = "

                If Left(TotLeav(i), 1) <> "P" And Right(TotLeav(i), 1) <> "P" Then
                    SQLStr = SQLStr & "(isnull(Availed,0) - 1) Where EMP_CODE='" & Chk(TxtCode.Text) & "' and LEVYEAR='" & Session("LeavYear") & "' and LVTYPE='"
                Else
                    SQLStr = SQLStr & "(isnull(Availed,0) - 0.5) Where EMP_CODE='" & Chk(TxtCode.Text) & "' and LEVYEAR='" & Session("LeavYear") & "' and LVTYPE='"
                End If

                If Left(TotLeav(i), 1) = "P" Then
                    SQLStr = SQLStr & Right(TotLeav(i), 1) & "'"
                Else
                    SQLStr = SQLStr & Left(TotLeav(i), 1) & "'"
                End If
                Session("DalObj").ExecuteCommand(SQLStr, Tran)
            Next
            Tran.Commit()
            Tran.Dispose()
            SetMsg(lblAlready, (TotAtDate.Length) & " Records Deleted.")
            FillLeaveStatus(Chk(TxtCode.Text))
            T1.Visible = False
        Catch ex As Exception
            Tran.Rollback()
            SetMsg(lblMsg, ex.Message)
        End Try
    End Sub

    Private Sub BindHoliday()
        GrdHolidays.DataSource = ViewState("DtTmpHolidays")
        GrdHolidays.DataBind()
    End Sub

    Private Sub cmdCalDays_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCalDays.Click
        Try
            If DateDiff(DateInterval.Day, CDate(dtpFromDate.DateValue), CDate(DtpTo.DateValue)) >= 0 Then
                CheckAvailibility()
                ShowHolidays()
            Else
                SetMsg(lblMsg, "From Date must be less or equal to To Date. : Invalid Date")
                TxtDays.Text = ""
            End If
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message)
        End Try
    End Sub

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub
End Class
