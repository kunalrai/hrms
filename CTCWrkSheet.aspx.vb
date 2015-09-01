Partial Class CTCWrkSheet
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
    Dim Value, StrSql As String
    Dim i, j, k As Int16
    Dim PayDate As Date
    Public WithEvents txtP As TextBox
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Dim dDate As Date
        Dim Item As ListItem
        Dim dtFilterFlds As DataTable

        If Not IsPostBack Then
            Dim StrMsg As String
            txtNumRec.Text = 10
            FillCombo()
            dDate = Session("DalObj").ExecuteCommand("Select FIN_YR_ST From FinYear where FIN_YR_CUR='Y'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
            For i = 1 To 12
                Value = MonthName(Month(dDate)) & " " & DatePart(DateInterval.Year, dDate)
                Item = New ListItem(Value, Month(dDate) + 1 & "/" & Year(dDate))
                cmbPayDate.Items.Add(Item)
                dDate = DateAdd(DateInterval.Month, 1, dDate)
            Next
            DtpWefDate.Text = Format(Now, "dd/MMM/yyyy")
            StrSql = "SELECT PaySetup.Field_Name,paysetup.field_desc,replace(paysetup.field_name,'_CODE','_NAME') as display_field FROM SYSCOLUMNS INNER JOIN PAYSETUP on PAYSETUP.Field_Name=SYSCOLUMNS.[NAME]  WHERE [id]=object_id(N'HRDMASTQRY') and [Name] like ('%_CODE')"
            dtFilterFlds = New DataTable
            Session("DalObj").GetSqlDataTable(dtFilterFlds, StrSql)
            For i = 0 To dtFilterFlds.Rows.Count - 1
                cmbSearchFld.Items.Add(New ListItem(Chk(dtFilterFlds.Rows(i).Item("Field_Desc")), Chk(dtFilterFlds.Rows(i).Item("Field_Name"))))
            Next
            cmbSearchFld.Items.Add("All") : cmbSearchFld.SelectedIndex = cmbSearchFld.Items.Count - 1
        End If
    End Sub
    Private Sub FillCombo()
        Try

        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (FillCombo())")
        End Try
    End Sub
#Region "    Search Text Coding    "

    Private Sub txtSearchText_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            'If (GrdEmployee.CurrentPageIndex > GrdEmployee.PageCount Or GrdEmployee.CurrentPageIndex < 0) Then
            'GrdEmployee.CurrentPageIndex = 0
            'Else
            '    GrdEmployee.CurrentPageIndex = e.NewPageIndex
            'End If
            'If (GrdEmployee.CurrentPageIndex > GrdEmployee.PageCount Or GrdEmployee.CurrentPageIndex < 0) Then GrdEmployee.CurrentPageIndex = 0

        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " (GrdEmployee_EditCommand)")
        End Try
    End Sub


#End Region

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
    Private Sub RefreshGrid()
        Dim GrdCol As BoundColumn
        Dim dtCTC As DataTable
        Dim dr As DataRow
        PayDate = EOM(DateAdd(DateInterval.Month, cmbPayDate.SelectedIndex, FY_Start))
        StrSql = "SELECT M.EMP_CODE ,M.EMP_NAME ,M.DOB,M.DOJ, " & _
             "ISNULL(AI.SAF_YN,M.SAF_YN) AS SAF_YN, " & _
             "ISNULL(AI.GRD_CODE,M.Grd_Code) AS GRD_CODE, " & _
             "ISNULL(AI.DEPT_CODE,M.Dept_Code) AS DEPT_CODE, " & _
             "ISNULL(AI.GRD_NAME,M.GRD_NAME) AS GRD_NAME , " & _
             "ISNULL(AI.DSG_NAME,M.DSG_NAME) AS DSG_NAME, " & _
             "ISNULL(AI.DEPT_NAME,M.DEPT_NAME) AS DEPT_NAME , " & _
             "ISNULL(AI.LOC_NAME,M.LOC_NAME) AS LOC_NAME ,  " & _
             "ISNULL(AI.INCRE_P,0) AS INCRE_P,  " & _
             "ISNULL(ISNULL(AI.BASIC_P,AI.BASIC),P.BASIC*12) AS BASIC_P,  " & _
             "ISNULL(ISNULL(AI.SA_P,AI.SA),P.PROF_FEE*12) AS SA_P,  " & _
             "ISNULL(ISNULL(AI.HRA_P,AI.HRA),P.HRA*12) AS HRA_P,  " & _
             "ISNULL(ISNULL(AI.CONV_P,AI.CONV),P.CONV*12) AS CONV_P,  " & _
             "ISNULL(ISNULL(AI.VEH_P,AI.VEH),CASE WHEN (M.GRD_CODE='BC') THEN 63000 WHEN (M.GRD_CODE='BB') THEN 78000 WHEN (M.GRD_CODE='BA') THEN 95000 WHEN (M.GRD_CODE='B1') THEN 120000 WHEN (M.GRD_CODE='B2') THEN 130000 WHEN (M.GRD_CODE='B3') THEN 150000 ELSE 0 END) AS VEH_P,  " & _
             "ISNULL(ISNULL(AI.PF_P,AI.PF),ROUND(P.BASIC*12*.12,0)) AS PF_P,  " & _
             "ISNULL(ISNULL(AI.SAF_P,AI.SAF),CASE WHEN (M.SAF_YN='Y') THEN ROUND(P.BASIC*12*0.13,0) ELSE 0 END) AS SAF_P,  " & _
             "ISNULL(ISNULL(AI.GRATUITY_P,AI.GRATUITY),ROUND(P.BASIC*12*0.0481,0)) AS GRATUITY_P,  " & _
             "ISNULL(ISNULL(AI.MIP_P,AI.MIP),CASE WHEN M.DEPT_CODE IN ('301','302','303','304','305') THEN CASE WHEN (M.GRD_CODE='BD') THEN 87000 WHEN (M.GRD_CODE='BE') THEN 61000 WHEN (M.GRD_CODE='BF') THEN 38400 WHEN (M.GRD_CODE='BG') THEN 32000 WHEN (M.GRD_CODE='BH') THEN 16500 ELSE 0 END ELSE ROUND(CASE WHEN (M.GRD_CODE='B3') THEN 0.45 WHEN (M.GRD_CODE='B2') THEN 0.35 WHEN (M.GRD_CODE='B1') THEN 0.25 WHEN (M.GRD_CODE='BA') THEN 0.15 ELSE 0.1 END * (CASE WHEN (M.GRD_CODE IN ('B1','B2','B3','BA','BB','BC')) THEN ISNULL(P.BASIC,0) + ISNULL(P.PROF_FEE,0) ELSE ISNULL(P.BASIC,0) + ISNULL(P.PROF_FEE,0) + ISNULL(P.HRA,0) END)*12 ,0) END) AS MIP_P,  " & _
             "ISNULL(ISNULL(AI.PEP_P,AI.PEP),CASE WHEN M.DEPT_CODE IN ('301','302','303','304','305') THEN CASE WHEN (M.GRD_CODE='BD') THEN 87000 WHEN (M.GRD_CODE='BE') THEN 61000 WHEN (M.GRD_CODE='BF') THEN 38400 WHEN (M.GRD_CODE='BG') THEN 32000 WHEN (M.GRD_CODE='BH') THEN 16500 ELSE 0 END ELSE ROUND(.05 * (ISNULL(P.BASIC,0) + ISNULL(P.PROF_FEE,0))*12,0) END) AS PEP_P, " & _
             " 0 as CTC_P,   " & _
             "ISNULL(AI.BASIC,P.BASIC*12) AS BASIC,  " & _
             "ISNULL(AI.SA,P.PROF_FEE*12) AS SA,  " & _
             "ISNULL(AI.HRA,P.HRA*12) AS HRA,  " & _
             "ISNULL(AI.CONV,P.CONV*12) AS CONV,  " & _
             "ISNULL(AI.VEH,CASE WHEN (M.GRD_CODE='BC') THEN 63000 WHEN (M.GRD_CODE='BB') THEN 78000 WHEN (M.GRD_CODE='BA') THEN 95000 WHEN (M.GRD_CODE='B1') THEN 120000 WHEN (M.GRD_CODE='B2') THEN 130000 WHEN (M.GRD_CODE='B3') THEN 150000 ELSE 0 END) AS VEH,  " & _
             "ISNULL(AI.PF,ROUND(P.BASIC*12*.12,0)) AS PF,  " & _
             "ISNULL(AI.SAF,CASE WHEN (M.SAF_YN='Y') THEN ROUND(P.BASIC*12*0.13,0) ELSE 0 END) AS SAF,  " & _
             "ISNULL(AI.GRATUITY,ROUND(P.BASIC*12*0.0481,0)) AS GRATUITY,  " & _
             "ISNULL(AI.MIP,CASE WHEN M.DEPT_CODE IN ('301','302','303','304','305') THEN CASE WHEN (M.GRD_CODE='BD') THEN 87000 WHEN (M.GRD_CODE='BE') THEN 61000 WHEN (M.GRD_CODE='BF') THEN 38400 WHEN (M.GRD_CODE='BG') THEN 32000 WHEN (M.GRD_CODE='BH') THEN 16500 ELSE 0 END ELSE ROUND(CASE WHEN (M.GRD_CODE='B3') THEN 0.45 WHEN (M.GRD_CODE='B2') THEN 0.35 WHEN (M.GRD_CODE='B1') THEN 0.25 WHEN (M.GRD_CODE='BA') THEN 0.15 ELSE 0.1 END * (CASE WHEN (M.GRD_CODE IN ('B1','B2','B3','BA','BB','BC')) THEN ISNULL(P.BASIC,0) + ISNULL(P.PROF_FEE,0) ELSE ISNULL(P.BASIC,0) + ISNULL(P.PROF_FEE,0) + ISNULL(P.HRA,0) END)*12 ,0) END) AS MIP,  " & _
             "ISNULL(AI.PEP,CASE WHEN M.DEPT_CODE IN ('301','302','303','304','305') THEN CASE WHEN (M.GRD_CODE='BD') THEN 87000 WHEN (M.GRD_CODE='BE') THEN 61000 WHEN (M.GRD_CODE='BF') THEN 38400 WHEN (M.GRD_CODE='BG') THEN 32000 WHEN (M.GRD_CODE='BH') THEN 16500 ELSE 0 END ELSE ROUND(.05 * (ISNULL(P.BASIC,0) + ISNULL(P.PROF_FEE,0))*12,0) END) AS PEP, " & _
             " 0 as CTC " & _
             "FROM HRDMASTQRY M INNER JOIN PAYMAST P ON P.EMP_CODE=M.EMP_CODE   " & _
             "                  LEFT JOIN ANNUALINCRE AI ON AI.EMP_CODE=M.EMP_CODE AND P.FINYEAR=AI.FINYEAR   " & _
             "WHERE P.FINYEAR=" & Year(FY_Start) & " AND (AI.PAYDATE IS NULL OR AI.PAYDATE='" & Format(PayDate, "dd/MMM/yyyy") & "') AND (AI.WEFDATE IS NULL OR AI.WEFDATE='" & Format(CDate(DtpWefDate.Text), "dd/MMM/yyyy") & "')"

        If cmbSearchFld.SelectedValue = "All" Then
        Else
            StrSql = StrSql & " AND M." & cmbSearchFld.SelectedValue & " = '" & cmbMastList.SelectedValue & "'"
        End If

        Session("dtCTC") = New DataTable
        Session("DalObj").GetSqlDataTable(Session("dtCTC"), StrSql)
        For i = 0 To CType(Session("dtCTC"), DataTable).Rows.Count - 1
            dr = CType(Session("dtCTC"), DataTable).Rows(i)
            dr.Item("CTC_P") = dr.Item("BASIC_P") + dr.Item("SA_P") + dr.Item("HRA_P") + dr.Item("CONV_P") + dr.Item("VEH_P") + dr.Item("PF_P") + dr.Item("SAF_P") + dr.Item("GRATUITY_P") + dr.Item("MIP_P") + dr.Item("PEP_P")
            dr.Item("CTC") = dr.Item("BASIC") + dr.Item("SA") + dr.Item("HRA") + dr.Item("CONV") + dr.Item("VEH") + dr.Item("PF") + dr.Item("SAF") + dr.Item("GRATUITY") + dr.Item("MIP") + dr.Item("PEP")
            dr.AcceptChanges()
        Next
        Grdctc.PageSize = ChkN(txtNumRec.Text)
        Grdctc.PagerStyle.Mode = PagerMode.NextPrev
        Grdctc.DataSource = Session("dtCTC")
        Grdctc.DataBind()
    End Sub
    Private Sub CmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdSearch.Click
        RefreshGrid()
    End Sub
    Private Sub Grdctc_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grdctc.ItemDataBound
        Try
            If e.Item.ItemIndex >= 0 Then
                If Session("LoginUser").UserGroup = "ADMIN" Then
                    CType(e.Item.Cells(8).Controls(1), TextBox).Visible = True
                    txtP = CType(e.Item.Cells(8).Controls(1), TextBox)
                Else
                    Dim tmpLbl As Label
                    tmpLbl = e.Item.FindControl("Lblincre_P")
                    If Not tmpLbl Is Nothing Then
                        tmpLbl.Visible = True
                    End If
                End If
            End If
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (GrdCTC_ItemDataBound)")
        End Try
    End Sub
    Private Sub Grdctc_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Grdctc.UpdateCommand
    End Sub
    Sub OnTextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Item As DataGridItem
        Dim dr As DataRow
        Dim dt As DataTable
        Dim IncreP As Double
        Dim drIncre As Double
        Dim MaxSA, OldSA, MIP, PEP As Double
        Item = CType(sender, Control).NamingContainer()
        IncreP = ChkN(CType(Item.Cells(8).Controls(1), TextBox).Text)
        Dim Code As String = Item.Cells(0).Text
        dt = Session("dtCTC")
        dr = dt.Select("[EMP_CODE]='" & Code & "'").GetValue(0)
        Dim Name As String = dr.Item("EMP_NAME")
        dr.Item("Basic") = Math.Round(((dr.Item("Basic") / (100 + dr.Item("Incre_P")) * 100)) * (100 + IncreP) / 100, 0)
        dr.Item("SA") = Math.Round(((dr.Item("SA") / (100 + dr.Item("Incre_P")) * 100)) * (100 + IncreP) / 100, 0)
        OldSA = dr.Item("SA")
        MaxSA = Math.Round(dr.Item("Basic") * 0.4, 0)
        If dr.Item("SA") > MaxSA Then
            dr.Item("SA") = MaxSA
            dr.Item("Basic") = Math.Round(dr.Item("Basic") + OldSA - MaxSA, 0)
        End If
        dr.Item("PF") = Math.Round(Math.Round(ChkN(dr.Item("Basic")) * 0.12, 0), 0)
        dr.Item("SAF") = IIf(Chk(dr.Item("SAF_YN"), True, True) = "Y", Math.Round(ChkN(dr.Item("Basic")) * 0.13, 0), 0)
        dr.Item("GRATUITY") = Math.Round(ChkN(dr.Item("Basic")) * 0.0481, 0)
        Select Case dr.Item("Dept_Code")
            Case "301", "302", "303", "304", "305"
                Select Case dr.Item("Grd_Code")
                    Case "BD"
                        MIP = 87000
                        PEP = 87000
                    Case "BE"
                        MIP = 61000
                        PEP = 61000
                    Case "BF"
                        MIP = 38400
                        PEP = 38400
                    Case "BG"
                        MIP = 32000
                        PEP = 32000
                    Case "BH"
                        MIP = 16500
                        PEP = 16500
                    Case Else
                        MIP = 0
                        PEP = 0
                End Select
            Case Else
                PEP = 0.05 * (ChkN(dr.Item("Basic")) + ChkN(dr.Item("SA")))
                Select Case dr.Item("Grd_Code")
                    Case "B3"
                        MIP = 0.45 * (ChkN(dr.Item("Basic")) + ChkN(dr.Item("SA")))
                    Case "B2"
                        MIP = 0.35 * (ChkN(dr.Item("Basic")) + ChkN(dr.Item("SA")))
                    Case "B1"
                        MIP = 0.25 * (ChkN(dr.Item("Basic")) + ChkN(dr.Item("SA")))
                    Case "BA"
                        MIP = 0.15 * (ChkN(dr.Item("Basic")) + ChkN(dr.Item("SA")))
                    Case "BC", "BB"
                        MIP = 0.1 * (ChkN(dr.Item("Basic")) + ChkN(dr.Item("SA")))
                    Case Else
                        MIP = 0.1 * (ChkN(dr.Item("Basic")) + ChkN(dr.Item("SA")) + ChkN(dr.Item("HRA")))
                End Select
        End Select
        PEP = Math.Round(PEP, 0)
        MIP = Math.Round(MIP, 0)
        dr.Item("MIP") = MIP
        dr.Item("PEP") = PEP
        dr.Item("CTC") = dr.Item("BASIC") + dr.Item("SA") + dr.Item("HRA") + dr.Item("CONV") + dr.Item("VEH") + dr.Item("PF") + dr.Item("SAF") + dr.Item("GRATUITY") + dr.Item("MIP") + dr.Item("PEP")
        dr.Item("Incre_P") = IncreP
        dr.AcceptChanges()
        dt.AcceptChanges()
        Grdctc.DataSource = dt
        Grdctc.DataBind()
    End Sub
    Sub OnTextChanged_Old(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Item As DataGridItem
        Dim dr As DataRow
        Dim dt As DataTable
        Dim IncreP As Int16
        Dim MaxSA, MIP, PEP As Double
        Item = CType(sender, Control).NamingContainer()
        IncreP = ChkN(CType(Item.Cells(8).Controls(1), TextBox).Text)
        Dim Code As String = Item.Cells(0).Text
        'Item.Cells(9).Text = 
        dt = Session("dtCTC")
        dr = dt.Select("[Employee Code]='" & Code & "'").GetValue(0)
        ''Response.Write(dr.Item("Employee Name"))
        Dim Name As String = dr.Item("Employee Name")
        MaxSA = (dr.Item("Basic") * (100 + IncreP) / 100) * 0.4
        Item.Cells(10).Text = dr.Item("Basic") * (100 + IncreP) / 100
        Item.Cells(12).Text = dr.Item("SA") * (100 + IncreP) / 100
        If (dr.Item("SA") * (100 + IncreP) / 100) > MaxSA Then
            Item.Cells(12).Text = MaxSA
            Item.Cells(10).Text = ChkN(Item.Cells(10).Text) + (dr.Item("SA") * (100 + IncreP) / 100) - MaxSA
        End If
        Item.Cells(20).Text = Math.Round(ChkN(Item.Cells(10).Text) * 0.12, 0)
        Item.Cells(22).Text = IIf(Chk(dr.Item("SAF_YN"), True, True) = "Y", Math.Round(ChkN(Item.Cells(10).Text) * 0.13, 0), 0)
        Item.Cells(24).Text = Math.Round(ChkN(Item.Cells(10).Text) * 0.0481, 0)
        Select Case dr.Item("Dept_Code")
            Case "301", "302", "303", "304", "305"
                Select Case dr.Item("Grd_Code")
                    Case "BD"
                        MIP = 87000
                        PEP = 87000
                    Case "BE"
                        MIP = 61000
                        PEP = 61000
                    Case "BF"
                        MIP = 38400
                        PEP = 38400
                    Case "BG"
                        MIP = 32000
                        PEP = 32000
                    Case "BH"
                        MIP = 16500
                        PEP = 16500
                    Case Else
                        MIP = 0
                        PEP = 0
                End Select
            Case Else
                PEP = 0.05 * (ChkN(Item.Cells(10).Text) + ChkN(Item.Cells(12).Text))
                Select Case dr.Item("Grd_Code")
                    Case "B3"
                        MIP = 0.45 * (ChkN(Item.Cells(10).Text) + ChkN(Item.Cells(12).Text))
                    Case "B2"
                        MIP = 0.35 * (ChkN(Item.Cells(10).Text) + ChkN(Item.Cells(12).Text))
                    Case "B1"
                        MIP = 0.25 * (ChkN(Item.Cells(10).Text) + ChkN(Item.Cells(12).Text))
                    Case "BA"
                        MIP = 0.15 * (ChkN(Item.Cells(10).Text) + ChkN(Item.Cells(12).Text))
                    Case "BC", "BB"
                        MIP = 0.1 * (ChkN(Item.Cells(10).Text) + ChkN(Item.Cells(12).Text))
                    Case Else
                        MIP = 0.1 * (ChkN(Item.Cells(10).Text) + ChkN(Item.Cells(12).Text) + ChkN(Item.Cells(14).Text))
                End Select
        End Select
        PEP = Math.Round(PEP, 0)
        MIP = Math.Round(MIP, 0)
        Item.Cells(26).Text = MIP
        Item.Cells(28).Text = PEP
        dr.AcceptChanges()
        dt.AcceptChanges()
        Response.Write(dr.Item("Basic"))
    End Sub

    Private Sub cmbPayDate_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPayDate.SelectedIndexChanged
        RefreshGrid()
    End Sub

    Private Sub DtpWefDate_Changed(ByVal Sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles DtpWefDate.Changed
        RefreshGrid()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim dt As DataTable
        dt = Session("dtCTC")
        PayDate = EOM(DateAdd(DateInterval.Month, cmbPayDate.SelectedIndex, FY_Start))
        For i = 0 To dt.Rows.Count - 1
            StrSql = "DELETE FROM ANNUALINCRE WHERE PayDate='" & Format(PayDate, "dd/MMM/yyyy") & "' AND WEFDATE='" & Format(CDate(DtpWefDate.Text), "dd/MMM/yyyy") & "' AND Emp_Code='" & dt.Rows(i).Item("Emp_Code") & "'"
            Session("DalObj").ExecuteCommand(StrSql)
            StrSql = "INSERT INTO ANNUALINCRE (FINYEAR,PAYDATE,WEFDATE,EMP_CODE,EMP_NAME,DOB,DOJ,SAF_YN,Grd_Code,Dept_Code,GRD_NAME," & _
                     "DSG_NAME,DEPT_NAME,LOC_NAME,INCRE_P,BASIC_P,BASIC,SA_P,SA,HRA_P,HRA,CONV_P,CONV,VEH_P,VEH,PF_P,PF,SAF_P,SAF,GRATUITY_P,GRATUITY,MIP_P,MIP,PEP_P,PEP,CTC,CTC_P) VALUES (" & _
                     Year(FY_Start) & ",'" & _
                     Format(PayDate, "dd/MMM/yyyy") & "','" & _
                     Format(CDate(DtpWefDate.Text), "dd/MMM/yyyy") & "','" & _
                     dt.Rows(i).Item("Emp_Code") & "','" & _
                     dt.Rows(i).Item("Emp_Name") & "','" & _
                     Format(dt.Rows(i).Item("DOB"), "dd/MMM/yyyy") & "','" & _
                     Format(dt.Rows(i).Item("DOJ"), "dd/MMM/yyyy") & "','" & _
                     dt.Rows(i).Item("SAF_YN") & "','" & _
                     dt.Rows(i).Item("GRD_Code") & "','" & _
                     dt.Rows(i).Item("Dept_Code") & "','" & _
                     dt.Rows(i).Item("GRD_NAME") & "','" & _
                     dt.Rows(i).Item("DSG_NAME") & "','" & _
                     dt.Rows(i).Item("DEPT_NAME") & "','" & _
                     dt.Rows(i).Item("LOC_NAME") & "'," & _
                     ChkN(dt.Rows(i).Item("INCRE_P")) & "," & _
                     ChkN(dt.Rows(i).Item("BASIC_P")) & "," & _
                     ChkN(dt.Rows(i).Item("BASIC")) & "," & _
                     ChkN(dt.Rows(i).Item("SA_P")) & "," & _
                     ChkN(dt.Rows(i).Item("SA")) & "," & _
                     ChkN(dt.Rows(i).Item("HRA_P")) & "," & _
                     ChkN(dt.Rows(i).Item("HRA")) & "," & _
                     ChkN(dt.Rows(i).Item("CONV_P")) & "," & _
                     ChkN(dt.Rows(i).Item("CONV")) & "," & _
                     ChkN(dt.Rows(i).Item("VEH_P")) & "," & _
                     ChkN(dt.Rows(i).Item("VEH")) & "," & _
                     ChkN(dt.Rows(i).Item("PF_P")) & "," & _
                     ChkN(dt.Rows(i).Item("PF")) & "," & _
                     ChkN(dt.Rows(i).Item("SAF_P")) & "," & _
                     ChkN(dt.Rows(i).Item("SAF")) & "," & _
                     ChkN(dt.Rows(i).Item("GRATUITY_P")) & "," & _
                     ChkN(dt.Rows(i).Item("GRATUITY")) & "," & _
                     ChkN(dt.Rows(i).Item("MIP_P")) & "," & _
                     ChkN(dt.Rows(i).Item("MIP")) & "," & _
                     ChkN(dt.Rows(i).Item("PEP_P")) & "," & _
                     ChkN(dt.Rows(i).Item("PEP")) & "," & _
                     ChkN(dt.Rows(i).Item("CTC")) & "," & _
                     ChkN(dt.Rows(i).Item("CTC_P")) & ")"
            Session("DalObj").ExecuteCommand(StrSql)
        Next
    End Sub

    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        PayDate = EOM(DateAdd(DateInterval.Month, cmbPayDate.SelectedIndex, FY_Start))
        StrSql = "DELETE FROM ANNUALINCRE WHERE PayDate='" & Format(PayDate, "dd/MMM/yyyy") & "' AND WEFDATE='" & Format(CDate(DtpWefDate.Text), "dd/MMM/yyyy") & "'"
        Session("DalObj").ExecuteCommand(StrSql)
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Private Sub cmdShowExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShowExcel.Click
        If RdoList.SelectedValue = 1 Then
            Dim Dt As DataTable
            Dt = Session("dtCTC")
            frmHTMLReports.argDataTable = Dt
        Else
            Dim StrQry As String
            StrQry = " SELECT EMP_CODE as Code ,EMP_NAME as [NAME] ,DOB,DOJ,GRD_NAME as Band ,DSG_NAME as Title,DEPT_NAME as [Function] " & _
                     ",LOC_NAME as Location ,INCRE_P as [Incerement (%)],BASIC_P as BASIC,SA_P as SA,HRA_P as HRA,CONV_P as CONV,VEH_P as VEH" & _
                     " ,PF_P as PF,SAF_P as SAF,GRATUITY_P as GRATUITY,MIP_P as MIP,PEP_P as PEP,CTC_P as CTC,BASIC as [BASIC New],SA as [SA New]" & _
                     " ,HRA as [HRA New],CONV as [CONV New],VEH as [VEH New],PF as [PF New],SAF as [SAF New],GRATUITY as [GRATUITY New]," & _
                     " MIP as [MIP New],PEP as [PEP New],CTC as [CTC New] From ANNUALINCRE " & _
                     " WHERE PAYDATE ='" & Format(EOM(DateAdd(DateInterval.Month, cmbPayDate.SelectedIndex, FY_Start)), "dd/MMM/yyyy") & "' " & _
                     " and WEFDATE = '" & Format(CDate(DtpWefDate.Text), "dd/MMM/yyyy") & "' and FINYEAR = " & Year(FY_Start)
            frmHTMLReports.argDataTable = Nothing
            frmHTMLReports.argStrSql = StrQry
        End If

        'Dim Dt As DataTable = Session("dtCTC")
    End Sub
    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub
End Class
