Imports System.IO
Imports System.Data.SqlClient

Partial Class WebForm11
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

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim DT As New DataTable
            Dim StrQuery As String, i, Count As Int16

            StrQuery = TxtQuery.Text

            CType(Session("DalObj"), DAL.DataLayer).GetSqlDataTable(DT, StrQuery)

            Dim strPath As String = HttpContext.Current.Request.ServerVariables("APPL_PHYSICAL_PATH")
            If Right(Trim(strPath), 1) = "\" Or Right(Trim(strPath), 1) = "/" Then
                strPath = Mid(strPath, 1, strPath.Length - 1)
            End If

            Dim myConnection As New SqlConnection(CType(Session("DalObj"), DAL.DataLayer).SqlConnectionString)
            myConnection.Open()

            For i = 0 To DT.Rows.Count - 1
                Dim Strq As String
                Dim intImageSize As Int64
                Dim strImageType, StrQry As String

                Strq = strPath & "\" & Chk(DT.Rows(i).Item("EMP_PICT"))

                Dim FStream As New FileStream(Strq, FileMode.OpenOrCreate)
                strImageType = FStream.GetType.ToString
                intImageSize = FStream.Length

                Dim ImageContent(intImageSize) As Byte
                Dim intStatus As Integer
                intStatus = FStream.Read(ImageContent, 0, intImageSize)

                StrQry = " Update HrdMast Set EmpPictureType=@PersonImageType, Emppicture=@PersonImage Where EMP_CODE='" & Chk(DT.Rows(i).Item("EMP_CODE")) & "'"

                Dim myCommand As New SqlCommand(StrQry, myConnection)

                ' Add Parameters to SPROC
                Dim prmPersonImage As New SqlParameter("@PersonImage", SqlDbType.Image)
                prmPersonImage.Value = ImageContent
                myCommand.Parameters.Add(prmPersonImage)

                Dim prmPersonImageType As New SqlParameter("@PersonImageType", SqlDbType.VarChar, 255)
                prmPersonImageType.Value = strImageType
                myCommand.Parameters.Add(prmPersonImageType)

                myCommand.ExecuteNonQuery()
                Count = Count + 1
            Next
            myConnection.Close()
            Response.Write(Count & " Records Saved Successfully.")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

End Class
