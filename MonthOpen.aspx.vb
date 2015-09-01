Imports DAL.DataLayer
Partial Class MonthOpen
    Inherits System.Web.UI.Page
    Dim dt As DataTable
    Dim ds As New DataSet
    Dim BAL As BAL.BLayer
    Dim DAL As DAL.DataLayer
    Dim StrSql, StrQuery, SqlStr, SqlQuery As String
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
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If
        DAL = Session("DalObj")
        BAL = Session("BalObj")
        'Put user code to initialize the page here
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
                cmdok.Visible = bSuccess

            End If
            '---------------------------------------------
            BAL.FillCombo(cmbmonth, " SELECT DISTINCT paydate,datename(MM,paydate) +'-' + datename(YYYY,paydate)as Mon from MonUpdate where MonUpdate='Y' order By paydate", True)
        End If

        If cmbmonth.SelectedItem.Text = "" Then
            LblErrMsg.Text = "Please Select Month."
        Else
            cmdok.Attributes.Add("onclick", "return ConfirmUpdate();")
        End If

    End Sub
    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            ' Tran = Session("DalObj").StartTransaction("Save")
            Dim StrSql, StrQuery As String
            Dim PayDate As Date
            If cmbmonth.SelectedValue.ToString.Trim = "" Then SetMsg(LblErrMsg, "First Select the Month to Open.") : Exit Sub
            PayDate = CType(cmbmonth.SelectedValue, Date)
            If CreateLog(PayDate, cmbLocation.SelectedValue) Then
                If cmbLocation.SelectedValue = "-1" Then
                    StrSql = "Update MonUpdate set MonUpdate='N' where PayDate='" & Format(PayDate, "dd/MMM/yyyy") & "'"
                    DAL.ExecuteCommand(StrSql)
                Else
                    StrSql = "Update MonUpdate set MonUpdate='N' where PayDate='" & Format(PayDate, "dd/MMM/yyyy") & "' AND Loc_Code='" & Chk(cmbLocation.SelectedValue) & "'"
                    DAL.ExecuteCommand(StrSql)
                End If
                SetMsg(LblErrMsg, "Month Open Successfully After Creating the Log.")
            Else
                SetMsg(LblErrMsg, "Could Not Create the log of Exisiting Data. Cannot Proceed.")
            End If
            '==================Frist Virson Update LoanBalance
            '''''''''==================Second Virson Of Update Salary Virson In ASP.net By Jitender
            'Tran.Commit()
        Catch ex As Exception
            LblErrMsg.Text = ex.Message
            'Tran.Rollback()
        End Try
    End Sub

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        Response.Redirect("Main.aspx")
    End Sub

    Private Sub cmbmonth_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbmonth.SelectedIndexChanged
        Try
            If IsDate(cmbmonth.SelectedValue) Then
                StrSql = "SELECT DISTINCT L.LOC_CODE,L.LOC_NAME FROM MONUPDATE U INNER JOIN LOCMAST L ON U.LOC_CODE=L.LOC_CODE WHERE U.PAYDATE='" & Format(CType(cmbmonth.SelectedValue, Date), "dd/MMM/yyyy") & "' AND ISNULL(U.MONUPDATE,'N')='Y'"
                BAL.FillCombo(cmbLocation, StrSql)
                cmbLocation.Items.Add(New ListItem("<All>", "-1"))
                cmbLocation.SelectedValue = "-1"
            End If
        Catch ex As Exception
            LblErrMsg.Text = ex.Message
            'Tran.Rollback()
        End Try
    End Sub
    Private Function GetFieldList(ByVal Table1 As String, ByVal Table2 As String, ByVal PreFix As String) As String
        Dim dtFields As New DataTable
        Dim sFields As String
        Try
            StrSql = "SELECT QRY1.COLNAME FROM " & _
                     "   (SELECT [NAME] AS COLNAME FROM SYSCOLUMNS WHERE [ID]=OBJECT_ID('" & Table1 & "')) AS QRY1 " & _
                     "INNER JOIN " & _
                     "   (SELECT [NAME] AS COLNAME FROM SYSCOLUMNS WHERE [ID]=OBJECT_ID('" & Table2 & "')) AS QRY2 " & _
                     "ON QRY1.COLNAME=QRY2.COLNAME"
            DAL.GetSqlDataTable(dtFields, StrSql)
            sFields = ""
            For i As Int16 = 0 To dtFields.Rows.Count - 1
                sFields += "," & PreFix & "." & dtFields.Rows(i).Item("ColName")
            Next
            GetFieldList = Mid(sFields, 2)
        Catch ex As Exception
            LblErrMsg.Text = ex.Message
        Finally
            dtFields.Dispose()
        End Try
    End Function
    Private Function CreateLog(ByVal PayDate As Date, ByVal sLocation As String) As Boolean
        Dim BatchID As Int32
        Dim sFieldList As String
        Try
            BatchID = DAL.ExecuteCommand("SELECT IsNull(MAX(BatchID),0)+1 FROM TrcHrdHist", ConnProvider.SQL, ExecutionType.ExecuteScalar)
            '*********Log for HrdHist***********
            sFieldList = GetFieldList("HRDHIST", "TRCHRDHIST", "H")
            StrSql = "INSERT INTO TrcHrdHist (BatchID," & Replace(sFieldList, "H.", "") & ") SELECT " & ChkN(BatchID) & "," & sFieldList & " FROM HrdHist H WHERE PayDate='" & Format(PayDate, "dd/MMM/yyyy") & "'"
            If cmbLocation.SelectedValue.ToString <> "-1" Then
                StrSql += " And Loc_Code='" & cmbLocation.SelectedValue.Trim & "'"
            End If
            DAL.ExecuteCommand(StrSql, ConnProvider.SQL, ExecutionType.ExecuteNonQuery)
            '**********End**********************
            '*********Log for PayHist***********
            sFieldList = GetFieldList("PAYHIST", "TRCPAYHIST", "P")
            StrSql = "INSERT INTO TrcPayHist (BatchID," & Replace(sFieldList, "P.", "") & ") SELECT " & ChkN(BatchID) & "," & sFieldList & " FROM PayHist P INNER Join HrdHist H ON (H.PayDate=P.Paydate AND H.Emp_Code=P.Emp_Code) WHERE P.PayDate='" & Format(PayDate, "dd/MMM/yyyy") & "'"
            If cmbLocation.SelectedValue.ToString <> "-1" Then
                StrSql = StrSql & " And H.Loc_Code='" & sLocation.ToString.Trim & "'"
            End If
            DAL.ExecuteCommand(StrSql, ConnProvider.SQL, ExecutionType.ExecuteNonQuery)
            '**********End**********************
            CreateLog = True
        Catch ex As Exception
            LblErrMsg.Text = ex.Message
        Finally
        End Try
    End Function
End Class




