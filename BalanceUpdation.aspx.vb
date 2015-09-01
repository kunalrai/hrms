Imports System.Runtime.InteropServices
Imports System.Runtime.Remoting.Messaging
Public Delegate Sub UpdateBalance()
Partial Class BalanceUpdation
    Inherits System.Web.UI.Page
    Dim DalUser As DAL.DataLayer.Users
    Dim SQLStr, StrSql As String, i As Int64
    Dim bLwopEffect As Boolean
    Dim VarComp_Code As String
    Dim oDAL As DAL.DataLayer
    Dim oBAL As BAL.BLayer
    Dim ExcelIsRunning As Boolean
    Public sMsg As String
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents cmdSave As System.Web.UI.WebControls.Button
    Protected WithEvents cmdClose As System.Web.UI.WebControls.Button

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
        oDAL = Session("DalObj")
        oBAL = Session("BalObj")
        DalUser = Session("LoginUser")
        Dim dtFilterFlds As DataTable
        If Server.GetLastError Is Nothing Then SetMsg(LblErrMsg, "")
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If
        Try
            If Not IsPostBack Then
                DtpDate.DateValue = Format(CDate(Date.Today), "dd/MMM/yyyy")
                StrSql = "SELECT PaySetup.Field_Name,paysetup.field_desc,replace(paysetup.field_name,'_CODE','_NAME') as display_field FROM SYSCOLUMNS INNER JOIN PAYSETUP on PAYSETUP.Field_Name=SYSCOLUMNS.[NAME]  WHERE [id]=object_id(N'HRDMASTQRY') and [Name] like ('%_CODE')"
                dtFilterFlds = New DataTable
                Session("DalObj").GetSqlDataTable(dtFilterFlds, StrSql)
                For i = 0 To dtFilterFlds.Rows.Count - 1
                    cmbSearchFld.Items.Add(New ListItem(Chk(dtFilterFlds.Rows(i).Item("Field_Desc")), Chk(dtFilterFlds.Rows(i).Item("Field_Name"))))
                Next
                cmbSearchFld.Items.Add("All") : cmbSearchFld.SelectedIndex = cmbSearchFld.Items.Count - 1
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (Page_Load)")
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
            Session("BalObj").FillCombo(cmbMastList, sCode, sName, sTable, True)
        End If
    End Sub

    Private Sub cmdClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Private Sub cmdok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Dim CallBack As AsyncCallback
        CallBack = New AsyncCallback(AddressOf BalCallBack)
        Dim dlgPay As UpdateBalance
        dlgPay = New UpdateBalance(AddressOf BalUpdate)
        dlgPay.BeginInvoke(CallBack, 0)
        ''BalUpdate()
        RegisterStartupScript("Hell", "<Script Language=JavaScript>window.open ('ProgressBar.aspx','','height=100,width=300,status=no,toolbar=no,menubar=no,location=no')</Script>")
    End Sub
    Private Sub BalUpdate()
        Try
            Dim vFor, Qry As String
            Dim dtEmployee As New DataTable
            Dim dtTmp As New DataTable

            Dim objSCM As SCM.Payroll

            VarComp_Code = DalUser.CurrentCompID
            'bLwopEffect = IIf(Chk(oDAL.ExecuteCommand("SELECT Rating3 FROM CompMast WHERE Comp_CODE='" & VarComp_Code & "'", DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar)) = "Y", True, False)
            bLwopEffect = False
            objSCM = GetSCMPayroll(oDAL, LblErrMsg, Session("LoginUser"))
            If TxtCriteria.Text <> "" Then
                vFor = "WHERE " & ("Comp_Code='" & VarComp_Code & "' And (") & TxtCriteria.Text & ")"
            Else
                vFor = "WHERE Comp_Code='" & VarComp_Code & "'"
            End If
            If cmbSearchFld.SelectedValue <> "All" Then
                vFor = vFor & " AND " & cmbSearchFld.SelectedValue & "='" & cmbMastList.SelectedValue & "'"
            End If
            Qry = ("SELECT * FROM HrdMastQry " & vFor & " ORDER BY Emp_Code")
            Session("DalObj").GetSqlDataTable(dtEmployee, Qry)
            StrSql = "DELETE FROM PAYBAR"
            oDAL.ExecuteCommand(StrSql, DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteNonQuery)
            StrSql = "INSERT INTO PAYBAR (FIELD_NAME,TOTAL_FIELDS,CURFIELD) VALUES ('Initiallizing...'," & dtEmployee.Rows.Count & ",1)"
            oDAL.ExecuteCommand(StrSql, DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteNonQuery)
            For i = 0 To dtEmployee.Rows.Count - 1
                StrSql = "Update PayBar SET Field_Name='" & dtEmployee.Rows(i).Item("Emp_Name") & "', CurField=" & i
                oDAL.ExecuteCommand(StrSql, DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteNonQuery)
                'SetMsg(LblErrMsg, dtEmployee.Rows(i).Item("Emp_Name"))
                'RegisterStartupScript("Hell", "<Script Language=JavaScript>window.document.submit</Script>")
                Select Case cmbUPType.SelectedIndex
                    Case 0 'Leave
                        objSCM.LevUpdate(DalUser.CurrentCompID, dtEmployee.Rows(i).Item("Emp_Code"), CDate(DtpDate.DateValue))
                    Case 1 'Reimbursements
                        objSCM.RimUpdate(CDate(DtpDate.DateValue), dtEmployee.Rows(i).Item("Emp_Code"), dtEmployee.Rows(i).Item("DOJ"), IIf(IsDBNull(dtEmployee.Rows(i).Item("DOL")), RY_End, dtEmployee.Rows(i).Item("DOL")), bLwopEffect)
                    Case 2 'Loans
                        objSCM.LonUpdate(dtEmployee.Rows(i).Item("Emp_Code"), CDate(DtpDate.DateValue))
                    Case 4 'Reim Prorata
                End Select
            Next
            StrSql = "UPDATE PAYBAR SET FIELD_NAME='[PAYDONE]'"
            oDAL.ExecuteCommand(StrSql, DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteNonQuery)
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (CmdOk_Click)")
        End Try
    End Sub
    Private Sub BalCallBack(ByVal result As IAsyncResult)
        StrSql = "UPDATE PAYBAR SET FIELD_NAME='[PAYDONE]'"
        oDAL.ExecuteCommand(StrSql, DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteNonQuery)
        SetMsg(LblErrMsg, "Process Complete")
    End Sub

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        Response.Redirect("Main.aspx")
    End Sub
End Class
