Partial Class TrainingRequest
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
    Dim EmpCode() As String
    Dim StrQry As String
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If
        If Not IsPostBack Then
            Try
                dtpFromDate.Text = Format(Date.Today, "dd/MMM/yyyy")
                dtpToDate.Text = Format(Date.Today, "dd/MMM/yyyy")
                Session("BalObj").FillCombo(cmbDepartment, "SELECT Dept_Code, Dept_Name FROM DEPTMAST Order By Dept_Name")
                cmbDepartment.Items.Insert(0, New ListItem("All", "0"))
                cmbDepartment.SelectedValue = "0"
                SetTrainCalenderGrid()
                SetEmployeeGrid()
            Catch ex As Exception
                SetMsg(LblErrMsg, ex.Message & " : " & "On Load")
            End Try
        End If
    End Sub
    Private Sub cmdShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShow.Click
        SetTrainCalenderGrid()
    End Sub

#Region "    Set Data Grid    "

    Sub SetTrainCalenderGrid()
        Try
            ViewState("DtTrainCal") = New DataTable

            StrQry = " SELECT TrainCalCode, TM.Train_Name as TrainName, Start_Date as StartDate, End_Date as EndDate, " & _
                  " (case when Train_Type = 'E' then 'External' When Train_Type = 'I' then 'Internal' end) as TrainType, No_Of_Seats as Seats, " & _
                  " LOC_NAME as Location, Total_Hours as Hours FROM TRAINCALENDAR TC Inner Join TrainMast TM on TC.Train_Code = TM.Train_Code " & _
                  "  Left Join LocMast LM on TC.LOC_CODE = LM.LOC_CODE Where Start_Date >= '" & dtpFromDate.Text & "' and End_Date <= '" & dtpToDate.Text & "' Order By Start_Date"

            Session("DalObj").GetSqlDataTable(ViewState("DtTrainCal"), StrQry)
            GrdTrainReq.DataSource = ViewState("DtTrainCal")
            GrdTrainReq.DataBind()
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Set Grid")
        End Try
    End Sub

    Sub SetEmployeeGrid()
        Try
            ViewState("DtEmp") = New DataTable

            If Session("LoginUser").UserGroup = "USER" Then
                StrQry = "Select EMP_CODE as Code, isnull(FNAME,'')+' '+isnull(LNAME,'') as EMPNAME, DEPT_NAME, DSG_NAME  from HrdMastQry Where LTYPE = 1 and ( MNGR_CODE = '" & Session("LoginUser").UserId & "' Or Emp_Code = '" & Session("LoginUser").UserId & "') Order By FNAME"
            Else
                trDepartment.Style.Item("Display") = "block"
                StrQry = "Select EMP_CODE as Code, isnull(FNAME,'')+' '+isnull(LNAME,'') as EMPNAME, DEPT_NAME, DSG_NAME  from HrdMastQry Where LTYPE = 1 " & Session("UserCodes") & " Order By FNAME"
            End If

            Session("DalObj").GetSqlDataTable(ViewState("DtEmp"), StrQry)
            If ViewState("DtEmp").Rows.Count = 0 Then Exit Sub
            EmployeeTr.Style.Item("display") = "block"
            GrdEmployee.DataSource = ViewState("DtEmp")
            GrdEmployee.DataBind()
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "SetEmployeeGrid()")
        End Try
    End Sub

#End Region

#Region "     Validate & Save Records      "

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim Tran As SqlClient.SqlTransaction
        Try
            Dim d As DAL.DataLayer
            If Not IsValidate() Then Exit Sub
            Tran = Session("DalObj").StartTransaction("Save")
            Dim i As Int16
            TrainingCode()

            If EmployeeTr.Style.Item("display") <> "none" Then
                If CoutEmployeeCode() = False Then SetMsg(LblErrMsg, " Please select at least one employee from the list.") : Exit Sub
                For i = 0 To EmpCode.Length - 1
                    StrQry = " Insert TRAINREQUEST ( Req_Date, Emp_Code, Train_Code ) Values ('" & _
                               Format(Date.Today, "dd/MMM/yyyy") & "', '" & Chk(EmpCode(i)) & "', '" & Chk(ViewState("TrainCode")) & "' )"

                    Session("DalObj").ExecuteCommand(StrQry, Tran)
                Next
            Else
                StrQry = " Insert TRAINREQUEST ( Req_Date, Emp_Code, Train_Code ) Values ('" & _
                Format(Date.Today, "dd/MMM/yyyy") & "', '" & Chk(Session("LoginUser").UserId) & "', '" & Chk(ViewState("TrainCode")) & "' )"

                Session("DalObj").ExecuteCommand(StrQry, Tran)
            End If

            Tran.Commit()
            SetMsg(LblErrMsg, " Record Saved Successfully.")
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Save Records")
            Tran.Rollback()
        Finally
            If Not Tran Is Nothing Then Tran.Dispose()
        End Try
    End Sub

    Public Function IsValidate() As Boolean
        Try
            Dim i As Int16, Count As Int16
            LblErrMsg.Text = ""
            Count = 0
            For i = 0 To GrdTrainReq.Items.Count - 1
                If Not CType(GrdTrainReq.Items(i).FindControl("ChkEmp"), HtmlInputCheckBox) Is Nothing Then
                    If CType(GrdTrainReq.Items(i).FindControl("ChkEmp"), HtmlInputCheckBox).Checked = True Then
                        If Count = 1 Then SetMsg(LblErrMsg, " You can select only one Training Session at a Time.") : Return False
                        Count = Count + 1
                    End If
                End If
            Next
            If Count = 0 Then SetMsg(LblErrMsg, " Please Select Training Session from the list.") : Return False
            Return True
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Validate")
        End Try
    End Function

    Function CoutEmployeeCode() As Boolean
        Try
            Dim i As Int16
            ViewState("Code") = ""
            For i = 0 To GrdEmployee.Items.Count - 1
                If Not CType(GrdEmployee.Items(i).FindControl("ChkSubEmp"), HtmlInputCheckBox) Is Nothing Then
                    If CType(GrdEmployee.Items(i).FindControl("ChkSubEmp"), HtmlInputCheckBox).Checked = True Then
                        ViewState("Code") = ViewState("Code") & GrdEmployee.Items(i).Cells(0).Text & "|"
                    End If
                End If
            Next
            If Len(ViewState("Code")) = 0 Then Return False
            EmpCode = Split(Mid(ViewState("Code"), 1, Len(ViewState("Code")) - 1), "|")
            Return True
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Count_EmployeeCode()")
        End Try
    End Function

    Sub TrainingCode()
        Try
            Dim i As Int16
            For i = 0 To GrdTrainReq.Items.Count - 1
                If Not CType(GrdTrainReq.Items(i).FindControl("ChkEmp"), HtmlInputCheckBox) Is Nothing Then
                    If CType(GrdTrainReq.Items(i).FindControl("ChkEmp"), HtmlInputCheckBox).Checked = True Then
                        ViewState("TrainCode") = GrdTrainReq.Items(i).Cells(0).Text
                        Exit For
                    End If
                End If
            Next

        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "CountTrainingCode()")
        End Try
    End Sub

#End Region

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Private Sub cmbDepartment_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDepartment.SelectedIndexChanged
        Try
            If cmbDepartment.SelectedValue = "0" Then
                StrQry = "Select EMP_CODE as Code, isnull(FNAME,'')+' '+isnull(LNAME,'') as EMPNAME, DEPT_NAME, DSG_NAME  from HrdMastQry Where LTYPE = 1 " & Session("UserCodes") & " Order By FNAME"
            Else
                StrQry = "Select EMP_CODE as Code, isnull(FNAME,'')+' '+isnull(LNAME,'') as EMPNAME, DEPT_NAME, DSG_NAME  from HrdMastQry Where LTYPE = 1 " & Session("UserCodes") & " and Dept_Code = '" & Chk(cmbDepartment.SelectedValue) & "' Order By FNAME"
            End If

            ViewState("DtEmp") = New DataTable
            Session("DalObj").GetSqlDataTable(ViewState("DtEmp"), StrQry)
            '            If ViewState("DtEmp").Rows.Count = 0 Then Exit Sub
            GrdEmployee.CurrentPageIndex = 0
            GrdEmployee.DataSource = ViewState("DtEmp")
            GrdEmployee.DataBind()
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Department Changed")
        End Try
    End Sub

    Private Sub GrdEmployee_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles GrdEmployee.PageIndexChanged
        Try
            If (GrdEmployee.CurrentPageIndex > GrdEmployee.PageCount Or GrdEmployee.CurrentPageIndex < 0) Then
                GrdEmployee.CurrentPageIndex = 0
            Else
                GrdEmployee.CurrentPageIndex = e.NewPageIndex
            End If
            SetEmployeeGrid()
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Department Changed")
        End Try
    End Sub

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub
End Class
