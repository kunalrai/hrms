Partial Class RunSQLCommand
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
            Dim SrNo As String
            SrNo = Request.QueryString.Item("SrNo")

            If CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules <> "" Then
                If InStr(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, SrNo & "-") <> 0 Then
                    Dim int As Int16, st As String
                    int = InStr(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, SrNo)
                    st = Right(Mid(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, int, Len(SrNo & "-") + 1), 1)

                    If st = "S" Then
                    Else
                        cmdView.Visible = False
                    End If
                Else
                    Response.Redirect("Main.aspx")
                End If
            End If

            If Not IsPostBack Then

            End If
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message)
        End Try
    End Sub

    Private Sub cmdView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdView.Click
        Try

        Catch ex As Exception
            SetMsg(LblMsg, ex.Message)
        End Try
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Private Sub cmdExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExport.Click

    End Sub
End Class
