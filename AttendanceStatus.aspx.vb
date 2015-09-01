Partial Class AttendanceStatus
    Inherits System.Web.UI.Page
    Dim DAL As DAL.DataLayer
    Dim BAL As BAL.BLayer
    Protected WithEvents Grdctc As System.Web.UI.WebControls.DataGrid
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents TxtCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents Dropdownlist1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents CmdAttendance As System.Web.UI.WebControls.Button

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
        DAL = Session("DALObj")
        BAL = Session("BALObj")
        If Not IsPostBack Then
            DtpFrom.Text = Format(Date.Today, "dd/MMM/yyyy")
            DtpTo.Text = Format(Date.Today, "dd/MMM/yyyy")
            FillDataGrid()
        End If
    End Sub
    Private Sub CmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdClose.Click
        Try
            Response.Redirect("Main.aspx")
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (CmdClose_Click)")
        End Try
    End Sub
    Private Function FillDataGrid()
        LblErrMsg.Text = ""
        GrdAttendance.Visible = True
        Try
            Dim rsAttendance As New DataTable
            Dim StrQuery, Temp1, Temp2 As String
            Dim FromDate As String
            Dim ToDate As String
            Dim var1 As String
            Dim Var2, str As String
            Dim Hours As String
            var1 = CDate(DtpFrom.Text)
            Var2 = CDate(DtpTo.Text)

            Temp1 = CmbFor1.SelectedValue
            Temp2 = CmbFor2.SelectedValue

            StrQuery = "     Select Attendance.LevYear,Attendance.Emp_Code," & _
                       "     (datename(d,Attendance.PayDate)+'/'+ left(datename(MM,Attendance.PayDate),3)+'/'+ datename(yyyy,Attendance.Paydate)) as PayDate," & _
                       "     (datename(d,Attendance.ShiftDate)+'/'+ left(datename(MM,Attendance.ShiftDate),3)+'/'+ datename(yyyy,Attendance.ShiftDate)) as ShiftDate," & _
                       "     (datename(d,Attendance.AtDate)+'/'+ left(datename(MM,Attendance.AtDate),3)+'/'+ datename(yyyy,Attendance.AtDate)) as AtDate," & _
                       "     Attendance.LvDays,Attendance.In_Time,Attendance.Out_Time,Attendance.LvType,Attendance.Shift_Name," & _
                       "     Attendance.Shift_From,Attendance.Shift_To,Attendance.ShiftFromMinuts," & _
                       "     Attendance.ShiftToMinuts,Attendance.InMinuts,Attendance.OutMinuts,Attendance.ShiftMinuts," & _
                       "     Attendance.WorkedMinuts,Attendance.UnApprovedMinuts,Attendance.InLateEarly," & _
                       "     Attendance.OutLateEarly,HrdMastQry.Emp_Code,HrdMastQry.Emp_Name," & _
                       "     CASE WHEN (ISNULL(Attendance.WorkedMinuts,0)>0) THEN CAST(CEILING(Attendance.WorkedMinuts/60) AS VARCHAR) + ':' +  CAST(Attendance.WorkedMinuts-(CEILING(Attendance.WorkedMinuts/60)*60)AS VARCHAR) ELSE '' END AS WORKED_HOURS," & _
                       "     CASE WHEN (InLateEarly>0) THEN InLateEarly ELSE 0 END AS InLate," & _
                       "     CASE WHEN (InLateEarly<0) THEN InLateEarly*-1 ELSE 0 END AS InEarly," & _
                       "     CASE WHEN (OutLateEarly>0) THEN OutLateEarly ELSE 0 END AS OutLate," & _
                       "     CASE WHEN (OutLateEarly<0) THEN OutLateEarly*-1 ELSE 0 END AS OutEarly" & _
                       "     from Attendance Attendance inner join HrdMastQry HrdMastQry " & _
                       "     On Attendance.Emp_Code=HrdMastQry.Emp_Code WHERE ATTENDANCE.AtDate >='" & var1 & "'  and ATTENDANCE.AtDate <='" & Var2 & "'" & Session("UserCodes")

            If Temp1 <> "" And Temp2 <> "" Then
                StrQuery = StrQuery & " And HrdMastQry." & Temp1 & "='" & Temp2 & "' "
            End If
            StrQuery = StrQuery & " ORDER BY ATTENDANCE.Emp_Code, ATTENDANCE.AtDate "
            DAL.GetSqlDataTable(rsAttendance, StrQuery)
            GrdAttendance.DataSource = rsAttendance
            GrdAttendance.DataBind()
            LblErrMsg.Font.Bold = True
            GrdAttendance.Visible = True

            'If rsAttendance.Rows.Count > 0 Then
            '    LblErrMsg.Text = rsAttendance.Rows.Count & "  Match(s) Found "
            'Else
            '    LblErrMsg.Text = rsAttendance.Rows.Count & "   Match(s) Found "
            'End If
            'If rsAttendance.Rows.Count > 0 Then
            '    GrdAttendance.DataSource = rsAttendance
            '    GrdAttendance.DataBind()
            '    LblErrMsg.Text = rsAttendance.Rows.Count & "   Match(s) Found "
            '    LblErrMsg.Font.Bold = True
            'Else
            '    LblErrMsg.Text = rsAttendance.Rows.Count & " Match(s) Found "
            '    GrdAttendance.Visible = False
            'End If
            '====================Total Records Of Grid In For Loop
            '''''Dim j, k As Integer
            '''''j = rsAttendance.Rows.Count
            '''''Dim i As Integer
            '''''For i = 0 To GrdAttendance.Items.Count - 1
            '''''    For k = 0 To j - 1
            '''''        If ChkNull(rsAttendance.Rows(0).Item("IN_Time")) <> "" Then
            '''''            GrdAttendance.Items(k).Cells(5).Text = (rsAttendance.Rows(0).Item("IN_TIME"))
            '''''            Hours = ChkNull(rsAttendance.Rows(0).Item("ShiftFromMinuts")) - ChkNull(rsAttendance.Rows(0).Item("InMinuts"))
            '''''            If Hours > 0 Then
            '''''                Hours = Format(Int(Hours / 60), "00") & ":" & Format(Remainder(Hours, 60), "00")
            '''''                GrdAttendance.Items(k).Cells(6).Text = Hours
            '''''            Else
            '''''                Hours = Format(Int((Hours) / 60), "00") & ":" & Format(Remainder((Hours), 60), "00")
            '''''                GrdAttendance.Items(k).Cells(7).Text = Hours
            '''''            End If
            '''''        End If
            '''''        If ChkNull(Trim((rsAttendance.Rows(0).Item("OUT_Time")))) <> "" Then
            '''''            Hours = ChkNull(rsAttendance.Rows(0).Item("OutMinuts")) - ChkNull(rsAttendance.Rows(0).Item("ShiftToMinuts"))
            '''''            If Hours > 0 Then
            '''''                Hours = Format(Int(Hours / 60), "00") & ":" & Format(Remainder(Hours, 60), "00")
            '''''                GrdAttendance.Items(k).Cells(10).Text = Hours
            '''''            Else
            '''''                Hours = Format(Int((Hours) / 60), "00") & ":" & Format(Remainder((Hours), 60), "00")
            '''''                GrdAttendance.Items(k).Cells(9).Text = Hours
            '''''            End If
            '''''        End If
            '''''        Dim tmpDT1 As Date, tmpDT2 As Date

            '''''        If GrdAttendance.Items(k).Cells(5).Text <> "" Or GrdAttendance.Items(k).Cells(8).Text <> "" Then
            '''''            If GrdAttendance.Items(k).Cells(5).Text = "" Then
            '''''                tmpDT1 = Format(GrdAttendance.Items(k).Cells(2).Text & " " & GrdAttendance.Items(k).Cells(3).Text)
            '''''            Else
            '''''                tmpDT1 = Format(GrdAttendance.Items(k).Cells(2).Text & " " & GrdAttendance.Items(k).Cells(5).Text)
            '''''            End If
            '''''            If GrdAttendance.Items(k).Cells(8).Text = "" Then
            '''''                tmpDT2 = Format(GrdAttendance.Items(k).Cells(2).Text & " " & GrdAttendance.Items(k).Cells(4).Text)
            '''''            Else
            '''''                tmpDT2 = Format(GrdAttendance.Items(k).Cells(2).Text & " " & GrdAttendance.Items(k).Cells(8).Text)
            '''''            End If
            '''''            If tmpDT1 > tmpDT2 Then
            '''''                tmpDT2 = DateAdd(DateInterval.Day, 1, tmpDT2)
            '''''            End If
            '''''            GrdAttendance.Items(k).Cells(11).Text = DateDiff(DateInterval.Hour, tmpDT1, tmpDT2)
            '''''        End If
            '''''        GrdAttendance.Items(k).Cells(12).Text = rsAttendance.Rows(0).Item("LvType")
            '''''        If Not IsDBNull(rsAttendance.Rows(0).Item("Paydate")) Then
            '''''            GrdAttendance.Items(k).Cells(13).Text = rsAttendance.Rows(0).Item("PayDate")
            '''''        End If
            '''''    Next
            '''''Next

        Catch ex As Exception
            LblErrMsg.Text = ex.Message & " : (FillDataGrid) "
        End Try
    End Function
    Private Sub CmdRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdRefresh.Click
        FillDataGrid()
    End Sub
    Private Sub CmdClose_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdClose.Click
        Try
        Catch ex As Exception
            LblErrMsg.Text = ex.Message & " : (CmdClose_Click1) "
        End Try
    End Sub
    Private Function CmbFor1_SelectedChanged()
        LblErrMsg.Text = ""
        Try
            Select Case CmbFor1.SelectedIndex
                Case 0
                    CmbFor2.Enabled = False
                    CmbFor1.SelectedIndex = 0
                    GrdAttendance.Visible = False
                Case 1
                    CmbFor2.Enabled = False
                    CmbFor1.SelectedIndex = 0
                    GrdAttendance.Visible = False
                Case 2
                    CmbFor2.Enabled = True
                    BAL.FillCombo(CmbFor2, "Emp_Code", "Emp_Name", "hrdMastQry", True)
                Case 3
                    CmbFor2.Enabled = True
                    BAL.FillCombo(CmbFor2, "Loc_Code", "Loc_Name", "LocMast", True)
                Case 4
                    CmbFor2.Enabled = True
                    BAL.FillCombo(CmbFor2, "Divi_Code", "Divi_Name", "DiviMast", True)
                Case 5
                    CmbFor2.Enabled = True
                    BAL.FillCombo(CmbFor2, "Sect_Code", "Sect_Name", "SectMast", True)
                Case 6
                    CmbFor2.Enabled = True
                    BAL.FillCombo(CmbFor2, "Dept_Code", "Dept_Name", "DeptMast", True)
                Case 7
                    CmbFor2.Enabled = True
                    BAL.FillCombo(CmbFor2, "Cost_Code", "Cost_Name", "CostMast", True)
                Case 8
                    CmbFor2.Enabled = True
                    BAL.FillCombo(CmbFor2, "Proc_Code", "Proc_Name", "ProcMast", True)
                Case 9
                    CmbFor2.Enabled = True
                    BAL.FillCombo(CmbFor2, "Grd_Code", "Grd_Name", "GrdMast", True)
                Case 10
                    CmbFor2.Enabled = True
                    BAL.FillCombo(CmbFor2, "Dsg_Code", "Dsg_Name", "DsgMast", True)
                Case 11
                    CmbFor2.Enabled = True
                    BAL.FillCombo(CmbFor2, "PB_Code", "PAYBUCKET_Name", "PAYBUCKETMast", True)
                Case 12
                    CmbFor2.Enabled = True
                    BAL.FillCombo(CmbFor2, "SFUNC_Code", "SFUNC_Name", "SFUNCMast", True)
                Case 13
                    CmbFor2.Enabled = True
                    BAL.FillCombo(CmbFor2, "SSFnc_CODE", "SSFnc_Name", "SSFNCMast", True)
            End Select
        Catch ex As Exception
            LblErrMsg.Text = ex.Message
        End Try
    End Function
    Private Sub CmbFor1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbFor1.SelectedIndexChanged
        CmbFor1_SelectedChanged()
    End Sub
End Class
