Partial Class PasswordChange
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
        If Not IsPostBack Then
            If IsNothing(Session("LoginUser")) Then
                Response.Redirect("CompSel.aspx")
            End If
            Dim Code As String
            TxtCode.Text = Session("LoginUser").UserId
            Code = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand("Select ISNULL(UserName,'') From WebUsers Where UserId = '" & Encrypt(Session("LoginUser").UserId, "+") & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
            If Not IsDBNull(Code) Then
                TxtUserName.Text = Encrypt(Code, "-")
            End If
        End If
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            If Not IsValidate() Then Exit Sub

            Dim SqlStr As String
            SqlStr = " Update WebUsers Set UserPassword='" & Encrypt(TxtNewPass.Text, "+") & "' Where UserId = '" & Encrypt(Session("LoginUser").UserId, "+") & "'"
            Session("DalObj").ExecuteCommand(SqlStr)
            SetMsg(lblMsg, "Password Changed Successfully.")
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message)
        End Try
    End Sub

    Function IsValidate() As Boolean
        Try

            If Chk(TxtCode.Text) = "" Then
                SetMsg(lblMsg, "User Id Required.")
                Return False
            End If

            If Chk(TxtUserName.Text) = "" Then
                SetMsg(lblMsg, "User Name Required.")
                Return False
            End If

            If Chk(TxtPass.Text) = "" Then
                SetMsg(lblMsg, "Old Password Required.")
                Return False
            End If

            Dim Pwd As String

            Pwd = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand("Select isnull(UserPassword,'') From WebUsers Where UserId = '" & Encrypt(Session("LoginUser").UserId, "+") & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)

            If Pwd <> "" Then
                If Encrypt(TxtPass.Text, "+") <> Pwd Then
                    SetMsg(lblMsg, "Incorrect Old Password.")
                    Return False
                End If
            End If

            If Chk(TxtNewPass.Text) = "" Then
                SetMsg(lblMsg, "New Password Required.")
                Return False
            End If

            If Chk(TxtConPass.Text) = "" Then
                SetMsg(lblMsg, "Confirm Password Required.")
                Return False
            End If
            Return True
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message)
        End Try
    End Function

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub

End Class
