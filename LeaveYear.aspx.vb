Partial Class LeaveYear
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            If Server.GetLastError Is Nothing Then LblErrMsg.Text = ""
            If Not IsNothing(Session("LogOutMsg")) Then Session("LogOutMsg") = Nothing
            If Not IsPostBack Then
                FillCompList()
                cmbYearType.SelectedValue = "FIN_YR_"
                cmbYearType_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (Page_Load)")
        End Try
    End Sub
    Private Sub FillCompList()
        Try
            Dim dsYear As New DataSet
            Session("DalObj").GetSqlDataSet(dsYear, "Select Cast(Fin_Yr As VarChar(4)) + ' - ' + Cast(Fin_Yr+1 As VarChar(4)) As PERIOD,FIN_YR From FinYear")
            LstComp.DataSource = dsYear
            LstComp.DataTextField = "PERIOD"
            LstComp.DataValueField = "FIN_YR"
            LstComp.DataBind()
            dsYear.Dispose()

        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (FillCompList)")
        End Try
    End Sub

    Private Sub cmbYearType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbYearType.SelectedIndexChanged
        Try
            Dim str As String, dt As New DataTable
            If cmbYearType.SelectedValue = "FIN_YR_" Then
                str = "Select Fin_Yr from  FinYear where  Fin_YR_CUR = 'Y'"
            End If
            If cmbYearType.SelectedValue = "RIM_YR_" Then
                str = "Select Fin_Yr from  FinYear where  RiM_YR_CUR = 'Y'"
            End If
            If cmbYearType.SelectedValue = "LEV_YR_" Then
                str = "Select Fin_Yr from  FinYear where  Lev_YR_CUR = 'Y'"
            End If
            Session("DalObj").GetSqlDataTable(dt, str)
            LstComp.SelectedValue = dt.Rows(0).Item("Fin_Yr")

            LstComp_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            LblErrMsg.Text = ex.Message & " : (cmbYearType_SelectedIndexChanged)"
        End Try
    End Sub

    Private Sub LstComp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles LstComp.SelectedIndexChanged
        Try
            If LstComp.SelectedValue <> "" Then
                Session("DalObj").ExecuteCommand("Update FinYear  Set " & Chk(cmbYearType.SelectedValue) & "CUR = 'N'")
                Session("DalObj").ExecuteCommand("Update FinYear  Set " & Chk(cmbYearType.SelectedValue) & "CUR = 'Y' Where Fin_YR = " & LstComp.SelectedValue & "")
                Dim DtYear As New DataTable
                Session("DalObj").GetSqlDataTable(DtYear, "Select Fin_YR," & Chk(cmbYearType.SelectedValue) & "ST From FinYear Where " & Chk(cmbYearType.SelectedValue) & "CUR = 'Y'")

                If DtYear.Rows.Count > 0 Then
                    LstComp.SelectedValue = DtYear.Rows(0).Item(0)
                    DtpStartDate.DateValue = Format(DtYear.Rows(0).Item(1), "dd/MMM/yyyy")
                    DtpEndDate.DateValue = Format(DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 12, CDate(DtpStartDate.Text))), "dd/MMM/yyyy")
                End If
                DtYear.Dispose()
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (LstComp_SelectedIndexChanged)")
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            FY_Start = CDate(DtpStartDate.Text)
            FY_End = CDate(DtpEndDate.Text)
            Session("FinYear") = LstComp.SelectedValue
            Session("LeavYear") = LstComp.SelectedValue
            Session("RimYear") = LstComp.SelectedValue
            Response.Redirect("Main.Aspx")
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (cmdOK_Click)")
        End Try
    End Sub

    Private Sub cmdcancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        Response.Redirect("Main.aspx")
    End Sub
End Class
