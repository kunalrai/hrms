Public Class PayCalculation
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents cmdclose As System.Web.UI.WebControls.Button
    Protected WithEvents TxtCretaria As System.Web.UI.WebControls.TextBox
    Protected WithEvents CmdView As System.Web.UI.WebControls.Button
    Protected WithEvents CmbFilter As System.Web.UI.WebControls.DropDownList
    Protected WithEvents TxtCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnList As System.Web.UI.WebControls.ImageButton
    Protected WithEvents cmdCalculate As System.Web.UI.WebControls.Button
    Protected WithEvents lblMsg As System.Web.UI.WebControls.Label
    Protected WithEvents cmbList As System.Web.UI.WebControls.DropDownList
    Protected WithEvents CmbMonth As System.Web.UI.WebControls.DropDownList
    Protected WithEvents CmbYear As System.Web.UI.WebControls.DropDownList

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
            FillComboBox()
        End If
    End Sub

    Sub FillComboBox()
        Try
            Dim LItem As ListItem, i As Int16
            Dim DtDate As Date
            DtDate = FY_Start
            For i = 1 To 12
                LItem = New ListItem
                LItem.Text = MonthName(Month(DtDate))
                LItem.Value = Month(DtDate)
                CmbMonth.Items.Add(LItem)
                DtDate = DateAdd(DateInterval.Month, 1, DtDate)
            Next

            CmbMonth.SelectedValue = Month(Date.Today)
            CmbYear.SelectedValue = Year(Date.Today)

            Dim StrSql As String, dtFilterFlds As DataTable

            StrSql = "SELECT PaySetup.Field_Name,paysetup.field_desc,replace(paysetup.field_name,'_CODE','_NAME') as display_field FROM SYSCOLUMNS INNER JOIN PAYSETUP on PAYSETUP.Field_Name=SYSCOLUMNS.[NAME]  WHERE [id]=object_id(N'HRDMASTQRY') and [Name] like ('%_CODE')"
            dtFilterFlds = New DataTable
            Session("DalObj").GetSqlDataTable(dtFilterFlds, StrSql)
            For i = 0 To dtFilterFlds.Rows.Count - 1
                CmbFilter.Items.Add(New ListItem(Chk(dtFilterFlds.Rows(i).Item("Field_Desc")), Chk(dtFilterFlds.Rows(i).Item("Field_Name"))))
            Next
            CmbFilter.Items.Add("All") : CmbFilter.SelectedIndex = CmbFilter.Items.Count - 1
            dtFilterFlds = Nothing
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & ": FillComboBox()")
        End Try
    End Sub
    Private Sub cmdclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Private Sub CmbFilter_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbFilter.SelectedIndexChanged
        Dim sTable, sCode, sName As String
        If CmbFilter.SelectedValue = "All" Then
        Else
            sTable = Replace(UCase(CmbFilter.SelectedValue), "_CODE", "MAST")
            sName = Replace(UCase(CmbFilter.SelectedValue), "_CODE", "_NAME")
            sCode = UCase(CmbFilter.SelectedValue)
            Select Case sCode
                Case "EMP_CODE", "MNGR_CODE"
                    sTable = "HRDMASTQRY WHERE LTYPE=1"
                Case "TYPE_CODE"
                    sTable = "EMPTYPE"
            End Select
            Session("BalObj").FillCombo(cmbList, sCode, sName, sTable, True)
        End If
        TxtCode.Text = ""
    End Sub


    Private Sub cmbList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbList.SelectedIndexChanged
        If cmbList.SelectedValue <> "" Then
            TxtCode.Text = cmbList.SelectedValue
        Else
            TxtCode.Text = ""
        End If
        cmbList.Visible = True
        TxtCode.Visible = True
        cmbList.Visible = False
    End Sub

    Private Sub btnList_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnList.Click
        cmbList.Visible = False
        TxtCode.Visible = False
        cmbList.Visible = True
    End Sub

End Class
