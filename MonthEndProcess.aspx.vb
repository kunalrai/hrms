Partial Class MonthEndProcess
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
    Protected WithEvents chkupdsal As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkupdloanbal As System.Web.UI.WebControls.CheckBox

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
            BAL.FillCombo(cmbmonth, " SELECT DISTINCT paydate,datename(MM,paydate) +'-' + datename(YYYY,paydate)as Mon from MonUpdate where MonUpdate is null Or MonUpdate='N' order By paydate", True)
        End If
        cmdok.Attributes.Add("onclick", "return ConfirmUpdate();")
    End Sub


    Private Function updateLoanbalance()
        '        Dim Tran As SqlClient.SqlTransaction
        Try
            ' Tran = Session("DalObj").StartTransaction("Save")
            Dim StrSql, StrQuery As String
            If cmbmonth.SelectedValue.ToString.Trim = "" Then SetMsg(LblErrMsg, "First Select the Month to Close.") : Exit Function
            If cmbLocation.SelectedValue = "-1" Then
                StrSql = "Update MonUpdate set MonUpdate='Y' where PayDate='" & cmbmonth.SelectedValue & "'"
                DAL.ExecuteCommand(StrSql)
            Else
                StrSql = "Update MonUpdate set MonUpdate='Y' where PayDate='" & cmbmonth.SelectedValue & "' AND Loc_Code='" & Chk(cmbLocation.SelectedValue) & "'"
                DAL.ExecuteCommand(StrSql)
            End If
            '==================Frist Virson Update LoanBalance
            If chkuplo.Checked = True Then
                Dim rsLaMast As New DataTable
                Dim rsLaTran As New DataTable
                Dim Str1, Str2, Str3, Str4, str5 As String

                Str1 = "SELECT * FROM LaTran WHERE  L_RDate='" & cmbmonth.SelectedValue & "'"
                DAL.GetSqlDataTable(rsLaTran, Str1)

                If rsLaTran.Rows.Count > 0 Then
                    Str2 = rsLaTran.Rows(0).Item("Emp_Code")
                    Str3 = rsLaTran.Rows(0).Item("L_Code")
                    Str4 = rsLaTran.Rows(0).Item("L_Date")

                    StrSql = "SELECT * FROM LaMast WHERE Emp_Code='" & Str2 & "' AND L_Code='" & Str3 & "' AND   L_Date='" & Str4 & "'"
                    DAL.GetSqlDataTable(rsLaMast, StrSql)

                    If rsLaMast.Rows.Count > 0 Then
                        Dim StrUpdate As String
                        StrUpdate = "UPDATE LAMAST SET L_REC=QRY.PRIN,I_REC=QRY.INTEREST FROM LAMAST INNER JOIN (SELECT EMP_CODE,L_CODE,L_DATE,SUM(PRIN) AS PRIN,SUM([INT]) AS INTEREST FROM LATRAN 	GROUP BY EMP_CODE,L_CODE,L_DATE) AS QRY ON QRY.EMP_CODE=LAMAST.EMP_CODE AND QRY.L_CODE=LAMAST.L_CODE AND QRY.L_DATE='" & cmbmonth.SelectedValue & "'"
                        DAL.ExecuteCommand(StrUpdate)
                        SetMsg(LblErrMsg, "Records Saved Sucessfully.")
                    End If
                End If
            End If


            '''''''''==================Second Virson Of Update Salary Virson In ASP.net By Jitender

            Dim rsArrear As New DataTable
            Dim rsPayMast As New DataTable
            Dim rsReimMast As New DataTable
            Dim StrEmp_Code, year As String
            year = Right(cmbmonth.SelectedItem.Text, 4)
            If chkupds.Checked = True Then

                StrSql = "SELECT Arrear.*, PaySetup.Fld_Categ, PaySetup.Fld_PayMast, PaySetup.Fld_Rated, PaySetup.Fld_Month FROM PaySetup, Arrear WHERE PaySetup.Field_Name=Arrear.Field_Name AND Arrear.PayDate='" & cmbmonth.SelectedValue & "' ORDER BY Emp_Code "
                DAL.GetSqlDataTable(rsArrear, StrSql)

                Do While Not rsArrear.Rows.Count - 1
                    StrEmp_Code = rsArrear.Rows(0).Item("Emp_Code")

                    StrQuery = "SELECT * FROM PayMast WHERE Emp_Code='" & StrEmp_Code & "' AND FinYear= '" & year & "'"
                    DAL.GetSqlDataTable(rsPayMast, StrQuery)

                    If rsPayMast.Rows.Count - 1 Then
                        Do While rsArrear.Rows(0).Item("Emp_Code") = StrEmp_Code
                            If rsArrear.Rows(0).Item("Fld_PayMast") = "Y" Then

                            End If
                        Loop
                    End If
                Loop
            End If
            'Tran.Commit()
        Catch ex As Exception
            LblErrMsg.Text = ex.Message
            'Tran.Rollback()
        End Try
    End Function

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        If cmbmonth.SelectedItem.Text = "" Then
            SetMsg(LblErrMsg, "Plese Select The Month Name From The List.")
        Else
            updateLoanbalance()
        End If

    End Sub

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        Response.Redirect("Main.aspx")
    End Sub

    Private Sub cmbmonth_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbmonth.SelectedIndexChanged
        Try
            LblErrMsg.Text = ""
            If IsDate(cmbmonth.SelectedValue) Then
                StrSql = "SELECT DISTINCT L.LOC_CODE,L.LOC_NAME FROM MONUPDATE U INNER JOIN LOCMAST L ON U.LOC_CODE=L.LOC_CODE WHERE U.PAYDATE='" & Format(CType(cmbmonth.SelectedValue, Date), "dd/MMM/yyyy") & "' AND ISNULL(U.MONUPDATE,'N')<>'Y'"
                BAL.FillCombo(cmbLocation, StrSql)
                cmbLocation.Items.Add(New ListItem("<All>", "-1"))
                cmbLocation.SelectedValue = "-1"
            End If
        Catch ex As Exception
            LblErrMsg.Text = ex.Message
            'Tran.Rollback()
        End Try
    End Sub
End Class

