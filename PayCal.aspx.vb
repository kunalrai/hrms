Imports DAL.DataLayer
Imports System.Runtime.InteropServices
Imports System.Runtime.Remoting.Messaging
Public Delegate Sub PayCalculation(ByVal strSql As String, ByRef objCn As SqlClient.SqlConnection)

Partial Class PayCalForm
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

    Dim SQLStr, StrSql, Value, LocCode, WOFF As String, i As Int16, OkForSave As Boolean
    Dim oDal As DAL.DataLayer
    Dim oBal As BAL.BLayer
    Dim tmLng As Long
    Dim empCnt As Long
    Dim vFor As String
    Dim ObjSender As Object
    Dim eEventargs As System.EventArgs
    Dim strMsg As String
    Dim cmd As SqlClient.SqlCommand
    Dim bCancelProcess As Boolean

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Dim Item As ListItem
        Dim FYS As Date
        Dim dtFilterFlds As DataTable
        oDal = Session("DalObj")
        oBal = Session("BalObj")
        Try
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
                    CmdView.Visible = bSuccess
                    cmdCalc.Visible = bSuccess

                End If
                '---------------------------------------------
                FYS = FY_Start
                For i = 1 To 12
                    Value = MonthName(Month(FYS)) & " " & DatePart(DateInterval.Year, FYS)
                    Item = New ListItem(Value, Month(FYS) & "/" & Year(FYS))
                    cmbMonth.Items.Add(Item)
                    FYS = DateAdd(DateInterval.Month, 1, FYS)
                Next
                StrSql = "SELECT PaySetup.Field_Name,paysetup.field_desc,replace(paysetup.field_name,'_CODE','_NAME') as display_field FROM SYSCOLUMNS INNER JOIN PAYSETUP on PAYSETUP.Field_Name=SYSCOLUMNS.[NAME]  WHERE [id]=object_id(N'HRDMASTQRY') and [Name] like ('%_CODE')"
                dtFilterFlds = New DataTable
                Session("DalObj").GetSqlDataTable(dtFilterFlds, StrSql)
                For i = 0 To dtFilterFlds.Rows.Count - 1
                    cmbSearchFld.Items.Add(New ListItem(Chk(dtFilterFlds.Rows(i).Item("Field_Desc")), Chk(dtFilterFlds.Rows(i).Item("Field_Name"))))
                Next
                cmbSearchFld.Items.Add("All") : cmbSearchFld.SelectedIndex = cmbSearchFld.Items.Count - 1
                cmbMonth.SelectedIndex = IIf(Date.Today.Month - 4 < 0, Date.Today.Month - 4 + 12, Date.Today.Month - 4)
                Session("strMsg") = ""
            End If
        Catch EX As Exception
            lblMsg.Text = EX.Message & "Page Load"
        End Try
    End Sub
    Private Sub cmbSearchFld_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSearchFld.SelectedIndexChanged
        Dim sTable, sCode, sName As String
        If cmbSearchFld.SelectedValue = "All" Then
        Else
            sTable = Replace(UCase(cmbSearchFld.SelectedValue), "_CODE", "MAST")
            sName = Replace(UCase(cmbSearchFld.SelectedValue), "_CODE", "_NAME")
            sCode = UCase(cmbSearchFld.SelectedValue)
            ''------------BY RAVI ON 26 DEC 2006-------------------------------------
            'If sName = "EMP_NAME" Or sName = "MNGR_NAME" Then
            '    sName = sName & "+'('+" & sCode & "+')'"
            'End If
            ''-------------------------------------------------
            Select Case sCode
                Case "EMP_CODE", "MNGR_CODE"
                    sTable = "HRDMASTQRY WHERE LTYPE=1"
                Case "TYPE_CODE"
                    sTable = "EMPTYPE"
            End Select
            oBal.FillCombo(cmbMastList, sCode, sName, sTable, True)
        End If
        If UCase(cmbSearchFld.SelectedValue) = "EMP_CODE" Then
            CmdView.Enabled = True
        End If
    End Sub
    Private Sub cmdCalc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCalc.Click

        Dim PayDate As Date
        Dim mdlPayCal As PayCal
        Dim strMessage As String
        Dim cn As New SqlClient.SqlConnection
        Dim sLocCodes As String
        Try
            Session("strMsg") = ""
            oDal = Session("DalObj")
            If Application("isCalcOn") = True Then
                SetMsg(lblMsg, "Another Session of Pay Calculation is Running! Please run this process after some time.")
                Exit Sub
            End If
            PayDate = EOM(DateAdd(DateInterval.Month, cmbMonth.SelectedIndex, FY_Start))
            mdlPayCal = New PayCal(Session("LoginUser"), oDal)
            If cmbSearchFld.SelectedValue = "All" Then
                vFor = " AND LTYPE IN (1,5) "
            Else
                vFor = " AND LTYPE IN (1,5) AND HrdMast." & cmbSearchFld.SelectedValue & " = '" & cmbMastList.SelectedValue & "'"
            End If
            If Trim(TxtFilter.Text) <> "" Then
                vFor = vFor & " AND " & TxtFilter.Text
            End If
            vFor = vFor & " " & Session("UserCodes")
            sLocCodes = GetLocCodes(vFor)
            If mdlPayCal.chkMon(CType(Format(PayDate, "dd/MMM/yyyy"), Date), strMessage, sLocCodes) Then
                tmLng = Timer
                StrSql = "UPDATE PAYBAR SET FIELD_NAME='[STARTING]'"
                oDal.ExecuteCommand(StrSql, ConnProvider.SQL, ExecutionType.ExecuteNonQuery)
                StrSql = mdlPayCal.getPayCalString(vFor, PayDate)
                cn.ConnectionString = oDal.SqlConnectionString
                cn.Open()
                Dim CallBack As AsyncCallback
                CallBack = New AsyncCallback(AddressOf PayCallBack)
                Dim dlgPay As PayCalculation
                dlgPay = New PayCalculation(AddressOf RunPayCal)
                '**********
                Application("isCalcOn") = True
                '**********
                dlgPay.BeginInvoke(StrSql, cn, CallBack, 0)
                RegisterStartupScript("Hell", "<Script Language=JavaScript>window.document.getElementById(""cmdCalc"").disabled=true;window.open ('ProgressBar.aspx','','height=100,width=300,status=no,toolbar=no,menubar=no,location=no')</Script>")
                'RegisterStartupScript("Hell", "<Script Language=JavaScript>window.open ('ProgressBar.aspx','','height=100,width=300,status=no,toolbar=no,menubar=no,location=no')</Script>")                
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

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Public Sub RunPayCal(ByVal strSql As String, ByRef objCn As SqlClient.SqlConnection)
        Session("Cmd") = New SqlClient.SqlCommand
        Try
            Session("strMsg") = ""
            'cmdCalc.Text = "Wait..."
            Session("Cmd").CommandText = strSql
            Session("Cmd").CommandTimeout = 86400
            Session("Cmd").CommandType = CommandType.Text
            Session("Cmd").Connection = objCn
            'Application("isCalcOn") = True
            Session("Cmd").ExecuteNonQuery()
        Catch ex As Exception
            If InStr(ex.Message, "Exception") = 0 Then
                Session("strMsg") = ex.Message
                SetMsg(lblMsg, ex.Message & " : (PayCallBack)")
            End If
            'PayCallBack()
        Finally
            Application("isCalcOn") = False
            cmd.Dispose()
            objCn.Close()
            objCn.Dispose()
            objCn = Nothing
        End Try
    End Sub

    Public Sub PayCallBack(Optional ByVal result As IAsyncResult = Nothing)
        Dim dr As AsyncResult
        Dim strMessage As String
        Dim PayDate As Date
        Try
            cmdCalc.Text = "Calculate"
            If Trim(Session("strMsg")) <> "" Then
                SetMsg(lblMsg, Session("strMsg") & " : (RunPayCal)")
                Exit Sub
            End If
            dr = result
            StrSql = "UPDATE PAYBAR SET FIELD_NAME='[PAYDONE]'"
            oDal.ExecuteCommand(StrSql, ConnProvider.SQL, ExecutionType.ExecuteNonQuery)
            If Not Session("bCancelProcess") Then
                PayDate = EOM(DateAdd(DateInterval.Month, cmbMonth.SelectedIndex, FY_Start))
                StrSql = "DELETE FROM MONUPDATE WHERE PayDate='" & Format(PayDate, "dd/MMM/yyyy") & "' AND Loc_Code IN (SELECT DISTINCT LOC_CODE From HRDMAST WHERE 1=1 " & vFor & ")"
                oDal.ExecuteCommand(StrSql, ConnProvider.SQL, ExecutionType.ExecuteNonQuery)
                StrSql = "INSERT INTO MONUPDATE (PayDate,Comp_Code,Loc_Code) SELECT DISTINCT '" & Format(PayDate, "dd/MMM/yyyy") & "' AS PayDate,Comp_Code,Loc_Code FROM HRDMAST WHERE 1=1 " & vFor
                oDal.ExecuteCommand(StrSql, ConnProvider.SQL, ExecutionType.ExecuteNonQuery)
                StrSql = "SELECT Count(emp_Code) FROM HRDMAST WHERE 1=1 " & vFor
                empCnt = Session("DalObj").ExecuteCommand(StrSql, ConnProvider.SQL, ExecutionType.ExecuteScalar)
                Session("strMsg") = "Total " & empCnt.ToString & " Employees Processed in " & Math.Round((Timer - tmLng) / 60, 0) & " Minutes"
                SetMsg(lblMsg, Session("strMsg"))
            Else
                SetMsg(lblMsg, "Pay Calculation Process Cancelled Successfully")
            End If
            RegisterStartupScript("Hell", "<Script Language=JavaScript>SayHello1()</Script>")
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : (PayCallBack)")
        End Try
    End Sub
    Private Function GetLocCodes(ByVal vFor As String) As String
        Dim dtTmp As New DataTable
        StrSql = "SELECT DISTINCT LOC_CODE From HRDMAST WHERE 1=1 " & vFor
        oDal.GetSqlDataTable(dtTmp, StrSql)
        If dtTmp.Rows.Count > 0 Then
            For i = 0 To dtTmp.Rows.Count - 1
                GetLocCodes += ",'" & Chk(dtTmp.Rows(i).Item("Loc_Code")) & "'"
            Next
            GetLocCodes = Mid(GetLocCodes, 2)
        Else
            GetLocCodes = "''"
        End If
    End Function

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Dim cmdCancel As SqlClient.SqlCommand
        cmdCancel = Session("cmd")
        Try
            If Not IsNothing(cmdCancel) Then
                cmdCancel.Cancel()
                Session("bCancelProcess") = True
            End If
            'StrSql = "UPDATE PAYBAR SET FIELD_NAME='[PAYDONE]'"
            'oDal.ExecuteCommand(StrSql, ConnProvider.SQL, ExecutionType.ExecuteNonQuery)            
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : (Cancel Command)")
        End Try
    End Sub

End Class
