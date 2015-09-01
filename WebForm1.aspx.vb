Partial Class WebForm1
    Inherits System.Web.UI.Page
    Dim ODAL As DAL.DataLayer
    Dim OBAL As BAL.BLayer
    Dim StrQuery As String
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
        ODAL = Session("DalObj")
        OBAL = Session("BalObj")
        If Not IsPostBack Then
        End If
    End Sub

    Private Sub TxtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCode.TextChanged
        Dim Cnt As Int16
        Cnt = ODAL.ExecuteCommand("Select Count(Emp_Code) from Hrdmast where Ltype='1'and emp_Code='" & Chk(TxtCode.Text) & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
        If Cnt <> 1 Then
            SetMsg(lblMsg, "This Employee Code Does Not Exists.")
            Exit Sub
        Else
            TxtName.Text = ODAL.ExecuteCommand("Select Emp_name from hrdmastqry where emp_Code='" & Chk(TxtCode.Text) & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
        End If
    End Sub

    Private Sub Cmduser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cmduser.Click
        Try
            Dim cnt As Int16
            cnt = ODAL.ExecuteCommand("select count(*) from webusers where USERID='" & Encrypt(Chk(TxtCode.Text), "+") & "' and USERNAME='" & Encrypt(Chk(TxtName.Text), "+") & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)

            If cnt > 0 Then
                SetMsg(lBLmSG, "This user is already exists.")
                Exit Sub
            End If

            StrQuery = "insert into webusers(USERID, USERNAME,USERPASSWORD,TYPE,GROUP_ID) values ('" & Encrypt(Chk(TxtCode.Text), "+") & "','" & Encrypt(Chk(TxtName.Text), "+") & "','" & Encrypt(Chk(TxtCode.Text), "+") & "','U','…­Ã®À')"
            ODAL.ExecuteCommand(StrQuery)
            lBLmSG.Text = "Id Created Successfully"
        Catch ex As Exception
            lBLmSG.Text = ex.Message & "Cmduser_Click"
        End Try
    End Sub
End Class
