Imports System.Runtime.Remoting.Messaging
Partial Class ProgressBar
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
    Dim oDal As DAL.DataLayer
    Dim empCnt As Int32
    Dim strSql As String
    Public Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            oDal = Session("DalObj")
            Dim dt As New DataTable
            'Commented by Mukesh 06-12-2006 because rights not required for this form
            'Written By Ravi as on 17 nov 2006
            'Dim SrNo As Int16
            'SrNo = Request.QueryString.Item("SrNo") + Request.QueryString.Item("MODULEID")
            'Dim bSuccess As Boolean
            'Select Case CheckRight(SrNo)
            '    Case MdlHRMS.AccessType.FullAccess, MdlHRMS.AccessType.SuperUser
            'bSuccess = True
            '    Case MdlHRMS.AccessType.ReadonlyAccess
            'bSuccess = False
            '    Case MdlHRMS.AccessType.Restricted
            'Response.Redirect(Request.UrlReferrer.ToString)
            'Exit Sub
            'End Select
            '-------------------------------------------
            strSql = "SELECT * FROM PAYBAR"
            oDal.GetSqlDataTable(dt, strSql)
            If dt.Rows.Count > 0 Then
                If Chk(dt.Rows(0).Item("FIELD_NAME")) = "[PAYDONE]" Then
                    RegisterStartupScript("Hell1", "<Script Language=JavaScript>SayHello()</Script>")
                    'SetMsg(lblMsg, "Complete")
                ElseIf InStr(Chk(dt.Rows(0).Item("FIELD_NAME")), "STARTING") > 0 Then
                    SetMsg(lblMsg, "<Font Size=4 COLOR=Navy>Pay Calculation: </Font><br><Font Size=3 color=Navy>Initializing...</Font>")
                Else
                    SetMsg(lblMsg, "<Font Size=4 COLOR=Navy>Pay Calculation: </Font><br><Font Size=3 color=Navy>Current A/c Head: " & dt.Rows(0).Item("FIELD_NAME") & "</Font><BR> <Font color=Red size=2>" & ChkN(dt.Rows(0).Item("CURFIELD")) & " OF " & ChkN(dt.Rows(0).Item("TOTAL_FIELDS")) & "&nbsp;&nbsp;(" & Math.Round(ChkN(dt.Rows(0).Item("CURFIELD")) / ChkN(dt.Rows(0).Item("TOTAL_FIELDS")) * 100, 2) & " % Complete)" & "</Font>")
                End If
            End If
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : (Progress Bar)")
        End Try
    End Sub
End Class