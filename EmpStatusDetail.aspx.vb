Imports System.Runtime.Remoting.Messaging
Partial Class EmpStatusDetail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents LblReg As System.Web.UI.WebControls.Label

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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            oDal = Session("DalObj")
            Dim CntTot, CntLive, CntReg As Int16
            Dim CurDate As String


            'comment by ravi
            'CntTot = oDal.ExecuteCommand("Select Count(*) from HrdMast WHERE ", , DAL.DataLayer.ExecutionType.ExecuteScalar)
            'CntLive = oDal.ExecuteCommand("Select Count(*) from HrdMast where  Ltype='1'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
            'CntReg = oDal.ExecuteCommand("Select Count(*) from HrdMast where  Ltype='2'", , DAL.DataLayer.ExecutionType.ExecuteScalar)

            'Comment By Ravi
            'CntTot = oDal.ExecuteCommand("Select Count(*) from HrdMast WHERE DOJ IS NOT NULL", , DAL.DataLayer.ExecutionType.ExecuteScalar)
            'CntLive = oDal.ExecuteCommand("Select Count(*) from HrdMast where DOJ IS NOT NULL and Ltype='1'", , DAL.DataLayer.ExecutionType.ExecuteScalar)

            CntTot = oDal.ExecuteCommand("Select Count(*) from HrdMast ", , DAL.DataLayer.ExecutionType.ExecuteScalar)
            CntLive = oDal.ExecuteCommand("Select Count(*) from HrdMast where Ltype='1'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
            CntReg = oDal.ExecuteCommand("Select Count(*) from HrdMast where DOJ IS NOT NULL and Ltype='2'", , DAL.DataLayer.ExecutionType.ExecuteScalar)


            CurDate = Format(Date.Today, "dd/MMM/yyyy")

            LblDate.Text = CurDate

            If CntTot > 0 Then
                lblMsg.Text = CntTot
            Else
                lblMsg.Text = "Zero"
            End If
            If CntLive > 0 Then
                LblLive.Text = CntLive
            Else
                LblLive.Text = "Zero"
            End If

            'SetMsg(LblDate, "<Font Size=4 align=center COLOR='#003366'>Employee Status As On : </Font><Font Size=4 COLOR='#003366'> " & CurDate & " </Font>")

            'If CntTot > 0 Then
            '    SetMsg(lblMsg, "<Font Size=3 COLOR=Navy>Total No of  Employee : </Font><Font Size=4 color=Navy>" & CntTot & "</Font>")
            'Else
            '    SetMsg(lblMsg, "<Font Size=3 COLOR=Navy>Total No of  Employee : </Font><Font Size=4 color=Navy> Zero </Font>")
            'End If

            'If CntLive > 0 Then
            '    SetMsg(LblLive, "<Font Size=3 COLOR=Navy>Total No of  Live Employee : </Font><Font Size=4 color=Navy>" & CntLive & "</Font>")
            'Else
            '    SetMsg(LblLive, "<Font Size=3 COLOR=Navy>Total No of  Live Employee : </Font><Font Size=4 color=Navy> Zero </Font>")
            'End If

        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & "Page_Load()")
        End Try
    End Sub
End Class
