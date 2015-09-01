Imports System.IO
Imports System.Data.SqlClient
Partial Class UploadImage
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label

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
            If Server.GetLastError Is Nothing Then SetMsg(LblMsg, "")

            If Not Trim(Request.QueryString.Item("Emp_Code")) = "" Then
                lblEmp_Code.Text = "Code :" & Trim(Request.QueryString.Item("Emp_Code"))
                ImgEmp.ImageUrl = Chk(CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand("Select isnull(Emp_Pict,'') From HrdMast Where Emp_Code='" & Trim(Request.QueryString.Item("Emp_Code")) & "'" & Session("UserCodes"), , DAL.DataLayer.ExecutionType.ExecuteScalar))
            End If

            If CType(Session("LoginUser"), DAL.DataLayer.Users).UserGroup = "USER" Then
                lblEmp_Code.Text = lblEmp_Code.Text & ", You are not authorised to modify image."
                cmdSet.Enabled = False
                FileName.Disabled = True
                cmdClear.Enabled = False
            End If
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (Page_Load)")
        End Try
    End Sub

    Private Sub cmdSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSet.Click
        Try
            If Trim(FileName.Value) = "" Then
                SetMsg(LblMsg, "Please Select The File to set.")
                Exit Sub
            End If
            Dim strFormat As String = Right(UCase(FileName.PostedFile.FileName), FileName.PostedFile.FileName.Length - FileName.PostedFile.FileName.LastIndexOf("."))

            If InStr(".BMP.JPG.GIF", strFormat) = 0 Then
                SetMsg(LblMsg, "Invalid Format of File.")
                Exit Sub
            End If
            Dim strPath As String = HttpContext.Current.Request.ServerVariables("APPL_PHYSICAL_PATH")
            If Right(Trim(strPath), 1) = "\" Or Right(Trim(strPath), 1) = "/" Then
                strPath = Mid(strPath, 1, strPath.Length - 1)
            End If
            ImgEmp.ImageUrl = FileName.PostedFile.FileName
            FileName.PostedFile.SaveAs(strPath & "\Upload\" & Session("EM_CD") & strFormat)
            'Session("DalObj").ExecuteCommand("Update HrdMast Set Emp_Pict = '" & strPath & "\Upload\" & Session("EM_CD") & strFormat & "' Where Emp_Code = '" & Session("EM_CD") & "'")
            Session("DalObj").ExecuteCommand("Update HrdMast Set Emp_Pict = 'Upload\" & Session("EM_CD") & strFormat & "' Where Emp_Code = '" & Session("EM_CD") & "'")
            SaveImageInnSQLServer()
            SetMsg(LblMsg, "Close the Window. you can view Your Image Next Time You visit the Application.")
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (cmdSet_Click())")
        End Try
    End Sub

    Private Sub cmdClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClear.Click
        Try
            Session("DalObj").ExecuteCommand("Update HrdMast Set Emp_Pict = '', Emppicture=Null, EmpPictureType=Null Where Emp_Code = '" & Session("EM_CD") & "'")
            lblEmp_Code.Text = "Code :" & Session("EM_CD") & " Image cleared successfully."
            ImgEmp.ImageUrl = ""
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (cmdClear_Click())")
        End Try
    End Sub

    Sub SaveImageInnSQLServer()
        Try
            Dim intImageSize As Int64
            Dim strImageType, StrQry As String
            Dim ImageStream As Stream

            ' Gets the Size of the Image
            intImageSize = FileName.PostedFile.ContentLength

            ' Gets the Image Type
            strImageType = FileName.PostedFile.ContentType

            ' Reads the Image
            ImageStream = FileName.PostedFile.InputStream

            Dim ImageContent(intImageSize) As Byte
            Dim intStatus As Integer
            intStatus = ImageStream.Read(ImageContent, 0, intImageSize)

            ' Create Instance of Connection and Command Object
            StrQry = " Update HrdMast Set EmpPictureType=@PersonImageType, Emppicture=@PersonImage Where EMP_CODE='" & Session("EM_CD") & "'"

            Dim myConnection As New SqlConnection(CType(Session("DalObj"), DAL.DataLayer).SqlConnectionString)
            Dim myCommand As New SqlCommand(StrQry, myConnection)

            ' Add Parameters to SPROC
            Dim prmPersonImage As New SqlParameter("@PersonImage", SqlDbType.Image)
            prmPersonImage.Value = ImageContent
            myCommand.Parameters.Add(prmPersonImage)

            Dim prmPersonImageType As New SqlParameter("@PersonImageType", SqlDbType.VarChar, 255)
            prmPersonImageType.Value = strImageType
            myCommand.Parameters.Add(prmPersonImageType)

            'StrQry = " Update HrdMast Set Emp_ImageType='" & strImageType & ", Emppicture=" & ImageContent(0) & " Where EMP_CODE='" & Session("EM_CD") & "'"
            'Session("DalObj").ExecuteCommand(StrQry)

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()

        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : SaveImageInnSQLServer()")
        End Try
    End Sub
    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub
End Class
