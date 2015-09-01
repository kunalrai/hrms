Partial Class Overtime
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

        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If

        If Not IsPostBack Then
            'By Ravi 21 Nov

            Dim SrNo As Int16
            SrNo = Request.QueryString.Item("SrNo") + Request.QueryString.Item("MODULEID")
            Dim bSuccess As Boolean
            Select Case CheckRight(SrNo)
                Case MdlHRMS.AccessType.FullAccess, MdlHRMS.AccessType.SuperUser
                    bSuccess = True
                Case MdlHRMS.AccessType.ReadonlyAccess, MdlHRMS.AccessType.Restricted
                    bSuccess = False
            End Select
            cmdSave.Visible = bSuccess
            CmdDelete.Visible = bSuccess


            '------------------------------------

            FillComboBox()
            Dim StrSql As String, Dt As New DataTable
            StrSql = " Select * from OverTime Where 1=2"

            CType(Session("DalObj"), DAL.DataLayer).GetSqlDataTable(Dt, StrSql)
            GrdEmployee.DataSource = Dt
            GrdEmployee.DataBind()
            CmbMonth.SelectedIndex = IIf(Date.Today.Month - 4 < 0, Date.Today.Month - 4 + 12, Date.Today.Month - 4)
        End If
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Private Sub CmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdCancel.Click
        ClearAll(Me)
    End Sub

    Private Sub CmbMonth_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbMonth.SelectedIndexChanged
        Try
            Dim StrSql As String, Dt As New DataTable
            StrSql = " Select * from OverTime Where Month(PAYDATE)='" & CmbMonth.SelectedValue & "'"

            CType(Session("DalObj"), DAL.DataLayer).GetSqlDataTable(Dt, StrSql)
            GrdEmployee.DataSource = Dt
            GrdEmployee.DataBind()
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message)
        End Try
    End Sub

    Sub FillComboBox()
        Try
            Dim LItem As ListItem, i As Int16
            Dim DtDate As Date
            DtDate = FY_Start
            For i = 1 To 12
                LItem = New ListItem
                LItem.Text = MonthName(Month(DtDate)) & " " & Year(DtDate)
                LItem.Value = Month(DtDate)
                CmbMonth.Items.Add(LItem)
                DtDate = DateAdd(DateInterval.Month, 1, DtDate)
            Next
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & ": FillComboBox()")
        End Try
    End Sub
End Class
