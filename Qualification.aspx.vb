Public Class Qualification
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents GrdQual As System.Web.UI.WebControls.DataGrid
    Protected WithEvents cmdSave As System.Web.UI.WebControls.Button
    Protected WithEvents LblErrMsg As System.Web.UI.WebControls.Label
    Protected WithEvents LblHeader As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim StrQuery As String

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            Dim DesgName As String
            ViewState("Dsg") = Request.QueryString.Item("DsgCode").ToString

            DesgName = Session("DalObj").ExecuteCommand(" Select Dsg_Name From DSGMAST Where Dsg_Code = '" & ViewState("Dsg") & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
            LblHeader.Text = " Select Qualification(s) For " & "<b>" & DesgName & "</b> " & " Designation "
            Dim DtTemp As New DataTable
            StrQuery = " Select Qual_Code, Qual_Name from QualMast Order By Qual_Name"
            Session("DalObj").GetSqlDataTable(DtTemp, StrQuery)
            GrdQual.DataSource = DtTemp
            GrdQual.DataBind()
        End If
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            Dim i As Int16

            For i = 0 To GrdQual.Items.Count - 1
                If CType(GrdQual.Items(i).FindControl("ChkSelect"), CheckBox).Checked = True Then
                    ViewState("QualCode") = ViewState("QualCode") & GrdQual.Items(i).Cells(1).Text & "|"
                End If
            Next

            If ViewState("QualCode") <> "" Then
                Session("QualCode") = Session("QualCode") & Mid(ViewState("QualCode"), 1, Len(ViewState("QualCode")) - 1) & "^"
            Else
                Session("QualCode") = Session("QualCode") & "^"
            End If

            Session("DesgCode") = Session("DesgCode") & ViewState("Dsg") & "|"

            SetMsg(LblErrMsg, " Record Saved Successfully.")
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : cmdSave_Click")
        End Try
    End Sub
End Class
