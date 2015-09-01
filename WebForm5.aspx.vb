Partial Class WebForm5
    Inherits System.Web.UI.Page
    Dim ImageName As String

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
        Button1.Attributes.Add("onclick", "fns();")

    End Sub
    Private Function UploadImage()
        Try
            ImageName = inpFileUpload.PostedFile.FileName.Substring(inpFileUpload.PostedFile.FileName.LastIndexOf("\") + 1)
            inpFileUpload.PostedFile.SaveAs(Server.MapPath("UploadImage\") & ImageName)
        Catch ex As Exception
            LblErrMsg.Text = ex.Message & "UploadImage"
        End Try
    End Function

    Private Sub BtnSaveImage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSaveImage.Click
        UploadImage()
    End Sub

End Class
