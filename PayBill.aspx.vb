Partial Class PayBill
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
    Dim oDal As DAL.DataLayer
    Dim oBal As BAL.BLayer
    Dim tmLng As Long
    Dim empCnt As Long
    Dim vFor As String
    Dim ObjSender As Object
    Dim eEventargs As System.EventArgs
    Dim strMsg As String
    Dim SQLStr, StrSql, Value, LocCode, WOFF As String, i As Int16, OkForSave As Boolean
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Dim Item As ListItem
        Dim FYS As Date
        Dim dtFilterFlds As DataTable
        oDal = Session("DalObj")
        oBal = Session("BalObj")
        If Chk(Session("strMsg")) <> "" Then
            SetMsg(lblMsg, Chk(Session("strMsg")))
        End If

        If Not IsPostBack Then
            FYS = FY_Start
            For i = 1 To 12
                Value = MonthName(Month(FYS)) & " " & DatePart(DateInterval.Year, FYS)
                Item = New ListItem(Value, Month(FYS) + 1 & "/" & Year(FYS))
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
            Session("strMsg") = ""
        End If
    End Sub

    Private Sub cmdCalc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCalc.Click

        Dim PayDate As Date
        Dim mdlPayCal As PayCal
        Dim strMessage As String
        Dim cn As New SqlClient.SqlConnection
        Dim vFor, strSql As String
        Try
            Session("strMsg") = ""
            oDal = Session("DalObj")
            PayDate = EOM(DateAdd(DateInterval.Month, cmbMonth.SelectedIndex, FY_Start))
            If cmbSearchFld.SelectedValue = "All" Then
                vFor = ""
            Else
                vFor = " AND HrdMastQry." & cmbSearchFld.SelectedValue & " = '" & cmbMastList.SelectedValue & "'"
            End If
            If Trim(TxtFilter.Text) <> "" Then
                vFor = vFor & " AND " & TxtFilter.Text
            End If
            strSql = getJVString(vFor, PayDate)
            oDal.ExecuteCommand(strSql, DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteNonQuery)
            CallReport()
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : (PAYCAL)")
        Finally
            'cn.Close()
            'cn = Nothing
        End Try
    End Sub
    Private Sub cmbSearchFld_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSearchFld.SelectedIndexChanged
        Dim sTable, sCode, sName As String
        If cmbSearchFld.SelectedValue = "All" Then
        Else
            sTable = Replace(UCase(cmbSearchFld.SelectedValue), "_CODE", "MAST")
            sName = Replace(UCase(cmbSearchFld.SelectedValue), "_CODE", "_NAME")
            sCode = UCase(cmbSearchFld.SelectedValue)
            Select Case sCode
                Case "EMP_CODE", "MNGR_CODE"
                    sTable = "HRDMASTQRY WHERE LTYPE=1"
                Case "TYPE_CODE"
                    sTable = "EMPTYPE"
            End Select
            oBal.FillCombo(cmbMastList, sCode, sName, sTable, True)
        End If
    End Sub
    Public Function getJVString(ByVal vFor As String, ByVal PayDate As Date) As String
        Dim dtSql As New DataTable
        Dim SqlStr As String
        oDal.GetSqlDataTable(dtSql, "SELECT cmdText FROM SQLPRoc WHERE [ID]=5")
        SqlStr = Chk(dtSql.Rows(0).Item(0))
        SqlStr = Replace(SqlStr, "|$|", "" & Replace(vFor, "'", "''") & "")
        SqlStr = Replace(SqlStr, "[PAYDATE]", "'" & Format(PayDate, "dd/MMM/yyyy") & "'")
        getJVString = SqlStr
    End Function
    Private Function CallReport()
        Dim sMessage, tmp As String
        sMessage = SetReport(543, "", Session("DalObj"), Session("LoginUser"))
        If sMessage = "Ready" Then
            'Response.RedirectLocation = "_Blank"
            'Response.Redirect("ReportView.aspx")
            tmp = " <SCRIPT language=javascript >window.open ('ReportView.aspx','')</SCRIPT>"
            RegisterStartupScript("Rem", tmp)
        End If
    End Function
End Class
