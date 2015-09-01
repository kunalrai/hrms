Imports DAL.DataLayer
Imports System.Runtime.InteropServices
Imports System.Runtime.Remoting.Messaging
Public Delegate Sub FNFCalculation(ByVal strSql As String, ByRef objCn As SqlClient.SqlConnection)
Partial Class FinalSettlement
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents grdAdjust As System.Web.UI.WebControls.DataGrid

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
    Dim oDal As DAL.DataLayer
    Dim oBal As BAL.BLayer
    Dim tmLng As Long
    Dim empCnt As Long
    Dim vFor As String
    Dim PayDate As Date
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Dim Item As ListItem
        Dim FYS As Date
        Dim dtFilterFlds As DataTable
        Try
            oDal = Session("DalObj")
            oBal = Session("BalObj")
            SetMsg(lblMsg, "")
            If Chk(Session("strMsg")) <> "" Then
                SetMsg(lblMsg, Chk(Session("strMsg")))
                Session("strMsg") = ""
            End If
            If Not IsPostBack Then
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
                    cmdSelAll.Visible = bSuccess
                    cmdDeSelAll.Visible = bSuccess
                    cmdCalc.Visible = bSuccess

                End If
                '---------------------------------------------
                FYS = FY_Start
                For i = 1 To 12
                    Value = MonthName(Month(FYS)) & " " & DatePart(DateInterval.Year, FYS)
                    Item = New ListItem(Value, Month(FYS) + 1 & "/" & Year(FYS))
                    cmbMonth.Items.Add(Item)
                    FYS = DateAdd(DateInterval.Month, 1, FYS)
                Next
                cmbMonth.SelectedValue = Month(Date.Today) + 1 & "/" & Year(Date.Today)
                PayDate = EOM(DateAdd(DateInterval.Month, cmbMonth.SelectedIndex, FY_Start))
                GetEMpList()
                Session("strMsg") = ""
            End If
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : (Load)")
        Finally
            'cn.Close()
            'cn = Nothing
        End Try
    End Sub
    Private Sub GetEMpList()
        Dim dtEmpList As New DataTable
        Try
            'StrSql = "SELECT Emp_Code,Emp_Name,DOJ,DOL,Set_Date,LNotice FROM HRDMASTQRY WHERE LType NOT IN (1,5) AND Month(Set_Date)=" & PayDate.Month & " AND Year(Set_Date)=" & PayDate.Year
            StrSql = "SELECT 0 as Calc,Emp_Code,Emp_Name,DOJ,DOL,Set_Date,LNotice,U.MONUPDATE FROM HRDMASTQRY M LEFT JOIN (SELECT * FROM MONUPDATE WHERE PAYDATE='" & Format(PayDate, "dd/MMM/yyyy") & "') U ON U.COMP_CODE=M.COMP_CODE AND M.LOC_CODE=U.LOC_CODE WHERE  M.LType NOT IN (1,5) " & Session("UserCodes") & " AND MONTH(SET_DATE)=" & PayDate.Month & " AND YEAR(SET_DATE)=" & PayDate.Year & "  AND (U.PAYDATE IS NULL OR U.PAYDATE='" & Format(PayDate, "dd/MMM/yyyy") & "') AND (U.MONUPDATE IS NULL OR U.MONUPDATE<>'Y')"
            oDal.GetSqlDataTable(dtEmpList, StrSql)
            grdFNF.DataSource = dtEmpList
            grdFNF.DataBind()
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : (PAYCAL)")
        Finally
            'cn.Close()
            'cn = Nothing
        End Try
    End Sub

    Private Sub cmbMonth_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbMonth.SelectedIndexChanged
        PayDate = EOM(DateAdd(DateInterval.Month, cmbMonth.SelectedIndex, FY_Start))
        GetEMpList()
    End Sub
    Private Function GetCodeList() As String
        Dim sCodes As String
        Try
            For i = 0 To grdFNF.Items.Count - 1
                If CType(grdFNF.Items(i).Cells(0).Controls(1), CheckBox).Checked Then
                    sCodes = sCodes & ",'" & grdFNF.Items(i).Cells(1).Text & "'"
                End If
            Next
            GetCodeList = sCodes
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : (PAYCAL)")
        Finally
            'cn.Close()
            'cn = Nothing
        End Try
    End Function

    Private Sub cmdCalc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCalc.Click
        Dim sCodes As String
        Dim PayDate As Date
        Dim mdlPayCal As PayCal
        Dim strMessage As String
        Dim cn As New SqlClient.SqlConnection
        Try
            Session("strMsg") = ""
            If Application("isCalcOn") = True Then
                SetMsg(lblMsg, "Another Session of Pay Calculation is Running! Please run this process after some time.")
                Exit Sub
            End If
            PayDate = EOM(DateAdd(DateInterval.Month, cmbMonth.SelectedIndex, FY_Start))
            mdlPayCal = New PayCal(Session("LoginUser"), oDal)
            If mdlPayCal.chkMon(CType(Format(PayDate, "dd/MMM/yyyy"), Date), strMessage) Then
                tmLng = Timer
                StrSql = "UPDATE PAYBAR SET FIELD_NAME='[STARTING]'"
                oDal.ExecuteCommand(StrSql, DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteNonQuery)
                sCodes = Mid(GetCodeList(), 2)
                vFor = " And LType NOT IN (1,5) AND Emp_Code IN (" & sCodes & ")" & Session("UserCodes")
                StrSql = mdlPayCal.getPayCalString(vFor, PayDate)

                'This code has been written by mukesh as on 16/10/2006
                ' Description = There is a condition in SqlString in Compmast When Insert in PayCal Table
                'Where lType In (1,5) and in Full and final And Emp_Code Not In (1,5) and conditions gets false
                'each time.
                'StrSql = Replace(StrSql, "LTYPE IN (1,5)", " LTYPE NOT IN (1,5) ")

                cn.ConnectionString = oDal.SqlConnectionString
                cn.Open()
                Dim CallBack As AsyncCallback
                CallBack = New AsyncCallback(AddressOf PayCallBack)
                Dim dlgPay As FNFCalculation
                dlgPay = New FNFCalculation(AddressOf RunPayCal)
                dlgPay.BeginInvoke(StrSql, cn, CallBack, 0)
                '**********
                Application("isCalcOn") = True
                '**********
                RegisterStartupScript("Hell", "<Script Language=JavaScript>window.document.getElementById(""cmdCalc"").disabled=true;window.open ('ProgressBar.aspx','','height=100,width=300,status=no,toolbar=no,menubar=no,location=no')</Script>")
            Else
                SetMsg(lblMsg, strMessage)
            End If
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : (PAYCAL)")
        Finally
            'cn.Close()
            'cn = Nothing
        End Try

    End Sub

    Private Sub cmdSelAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSelAll.Click
        For i = 0 To grdFNF.Items.Count - 1
            CType(grdFNF.Items(i).Cells(0).Controls(1), CheckBox).Checked = True
        Next
    End Sub

    Private Sub cmdDeSelAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDeSelAll.Click
        For i = 0 To grdFNF.Items.Count - 1
            CType(grdFNF.Items(i).Cells(0).Controls(1), CheckBox).Checked = False
        Next
    End Sub
    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Public Sub RunPayCal(ByVal strSql As String, ByRef objCn As SqlClient.SqlConnection)
        Dim cmd As New SqlClient.SqlCommand
        Try
            Session("strMsg") = ""
            'cmdCalc.Text = "Wait..."
            cmd.CommandText = strSql
            cmd.CommandTimeout = 86400
            cmd.CommandType = CommandType.Text
            cmd.Connection = objCn
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Session("strMsg") = ex.Message
            SetMsg(lblMsg, ex.Message & " : (PayCallBack)")
        Finally
            cmd.Dispose()
            objCn.Close()
            objCn.Dispose()
            objCn = Nothing
        End Try
    End Sub
    Public Sub PayCallBack(ByVal result As IAsyncResult)
        Dim dr As AsyncResult
        Dim strMessage As String
        Dim PayDate As Date
        Try
            cmdCalc.Text = "Calculate"
            If Trim(Session("strMsg")) <> "" Then
                SetMsg(lblMsg, Session("strMsg") & " : (RunPayCal)")
                Exit Sub
            End If
            RegisterStartupScript("Hell", "<Script Language=JavaScript>alert(""hell"");window.document.getElementById(""cmdCalc"").disabled=false;</Script>")
            dr = result
            Application("isCalcOn") = False
            StrSql = "UPDATE PAYBAR SET FIELD_NAME='[PAYDONE]'"
            oDal.ExecuteCommand(StrSql, ConnProvider.SQL, ExecutionType.ExecuteNonQuery)
            PayDate = EOM(DateAdd(DateInterval.Month, cmbMonth.SelectedIndex, FY_Start))
            StrSql = "DELETE FROM MONUPDATE WHERE PayDate='" & Format(PayDate, "dd/MMM/yyyy") & "' AND Loc_Code IN (SELECT DISTINCT LOC_CODE From HRDMAST WHERE 1=1 " & vFor & ")"
            oDal.ExecuteCommand(StrSql, ConnProvider.SQL, ExecutionType.ExecuteNonQuery)
            StrSql = "INSERT INTO MONUPDATE (PayDate,Comp_Code,Loc_Code) SELECT DISTINCT '" & Format(PayDate, "dd/MMM/yyyy") & "' AS PayDate,Comp_Code,Loc_Code FROM HRDMAST WHERE 1=1 " & vFor
            oDal.ExecuteCommand(StrSql, ConnProvider.SQL, ExecutionType.ExecuteNonQuery)
            StrSql = "SELECT Count(emp_Code) FROM HRDMAST WHERE 1=1 " & vFor
            empCnt = Session("DalObj").ExecuteCommand(StrSql, ConnProvider.SQL, ExecutionType.ExecuteScalar)
            Session("strMsg") = "Total " & empCnt.ToString & " Employees Processed in " & Math.Round((Timer - tmLng) / 60, 0) & " Minutes"
            SetMsg(lblMsg, Session("strMsg"))
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : (PayCallBack)")
        End Try
    End Sub
End Class
