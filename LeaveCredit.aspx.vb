Partial Class LeaveCredit
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents cmbDepartment As System.Web.UI.WebControls.DropDownList

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

        Try
            Dim SrNo As String
            SrNo = Request.QueryString.Item("SrNo") + Request.QueryString.Item("MODULEID")

            If CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules <> "" Then
                If InStr(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, SrNo & "-") <> 0 Then
                    Dim int As Int16, st As String
                    int = InStr(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, SrNo)
                    st = Right(Mid(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, int, Len(SrNo & "-") + 1), 1)

                    If st = "S" Then
                    Else
                        cmdSave.Visible = False
                    End If
                Else
                    Response.Redirect("Main.aspx")
                End If
            End If

            If Not IsPostBack Then
                Dim StartDate As Date, i As Int16, Item As ListItem
                StartDate = LY_Start

                For i = 0 To 11
                    Item = New ListItem(Format(StartDate, "MMMM,yyyy"), Month(StartDate))
                    cmbMonth.Items.Add(Item)
                    StartDate = DateAdd(DateInterval.Month, 1, StartDate)
                Next
                cmbMonth.DataBind()
                FillLeaveGrid(0)
            End If
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (Page_Load)")
        End Try

    End Sub

    Sub FillLeaveGrid(ByVal Month As Int16, Optional ByVal AllEmployee As Boolean = False)
        Try
            Dim StrQry As String, DtTemp As New DataTable

            'RIGHT('00' + DATENAME(dd, GetDate()), 2) + '/' + LEFT(DATENAME(mm, GetDate()), 3) + '/' + DATENAME(yyyy, GetDate()) as CurrDate, 

            StrQry = " SELECT LM.EMP_CODE,HQ.EMP_NAME, " & _
                     " LT.LVDESC,(ISNULL(LM.OPENING,0)+ISNULL(LM.EARNED,0)) AS OPENING,ISNULL(AVAILED,0) AS AVAILED, " & _
                     " ((ISNULL(LM.OPENING,0)+ISNULL(LM.EARNED,0)+ISNULL(TRAN_IN,0)) -(ISNULL(AVAILED,0)+ISNULL(TRAN_OUT,0))) AS  BALANCE, 0 AS CREDITED, '1' as Checked " & _
                     " FROM LEAVMAST LM INNER JOIN HRDMASTQRY HQ ON LM.EMP_CODE=HQ.EMP_CODE INNER JOIN LVTYPE LT ON " & _
                     " LM.LVTYPE=LT.LVTYPE WHERE LT.LVTYPE='E' "

            If Not AllEmployee Then
                StrQry = StrQry & " AND ( MONTH(DOJ) = " & Month & " Or MONTH(DateAdd(m,6,DOJ)) = " & Month & " ) "
            End If

            CType(Session("DalObj"), DAL.DataLayer).GetSqlDataTable(DtTemp, StrQry)

            GrdLeavCr.DataSource = DtTemp
            GrdLeavCr.DataBind()
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " FillLeaveGrid()")
        End Try
    End Sub

    Private Sub cmdShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShow.Click
        Try
            FillLeaveGrid(cmbMonth.SelectedValue)
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " Show()")
        End Try
    End Sub

    Private Sub IsAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IsAll.CheckedChanged
        If IsAll.Checked Then
            FillLeaveGrid(cmbMonth.SelectedValue, True)
        Else
            FillLeaveGrid(cmbMonth.SelectedValue)
        End If
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            Dim i, TotCount As Int16, Item As DataGridItem, StrQry As String

            For Each Item In GrdLeavCr.Items
                If Not IsNothing(Item.FindControl("ChkSelect")) Then
                    If CType(Item.FindControl("ChkSelect"), HtmlInputCheckBox).Checked Then
                        StrQry = " Update LeavMast Set Earned= IsNull(Earned,0)+" & ChkN(CType(Item.FindControl("TxtCredited"), TextBox).Text) & _
                                 " Where EMP_CODE='" & Chk(Item.Cells(0).Text) & "' and LEVYEAR='" & Session("LeavYear") & "' and LVTYPE = 'E'"

                        CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(StrQry, , DAL.DataLayer.ExecutionType.ExecuteNonQuery)
                        TotCount = TotCount + 1
                    End If
                End If
            Next

            SetMsg(LblMsg, TotCount & " Records updated successfully.")
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " On Save")
        End Try
    End Sub


    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub
End Class
