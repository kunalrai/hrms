Imports System.Data.SqlClient
Partial Class TrfUtility
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

        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If


        Dim SrNo As String
        SrNo = Request.QueryString.Item("SrNo")

        If CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules <> "" Then
            If InStr(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, SrNo & "-") <> 0 Then
                Dim int As Int16, st As String
                int = InStr(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, SrNo)
                st = Right(Mid(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, int, Len(SrNo & "-") + 1), 1)

                If st <> "S" Then
                    cmdExe.Visible = False
                    cmdReset.Visible = False
                    CmdSet.Visible = False
                End If
            Else
                Response.Redirect("Main.aspx")
            End If
        End If

        If Not IsPostBack Then
            FillComboBox()
            Page.DataBind()
            cmbTransfer_SelectedIndexChanged(sender, e)
        End If
    End Sub

    Sub FillComboBox()
        Try
            Dim LItem As ListItem, i As Int16
            Dim DtDate As Date
            DtDate = FY_Start
            For i = 1 To 12
                LItem = New ListItem
                LItem.Text = MonthName(Month(DtDate)) & " " & Year(DtDate)
                LItem.Value = Month(DtDate)
                CmbFor.Items.Add(LItem)
                DtDate = DateAdd(DateInterval.Month, 1, DtDate)
            Next
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & ": FillComboBox()")
        End Try
    End Sub

    Private Sub CmdCose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdCose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Private Sub CmdSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdSet.Click
        Try
            Dim PDate As Date
            If Chk(TxtDatabase.Text) = "" Then
                SetMsg(lblMsg, " Enter Database Name.")
                Exit Sub
            End If
            TxtQuery.Text = Replace(TxtQuery.Text, "@DATABASE", TxtDatabase.Text)
            PDate = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, CDate("01/" & Left(CmbFor.SelectedItem.Text, 3) & "/" & Right(CmbFor.SelectedItem.Text, 4))))
            If cmbTransfer.SelectedValue = 1 Or cmbTransfer.SelectedValue = 2 Then
                TxtQuery.Text = Replace(TxtQuery.Text, "@DATE", "'" & Format(PDate, "dd/MMM/yyyy") & "'")
            Else
                TxtQuery.Text = Replace(TxtQuery.Text, "@DATE", "'" & Format(PDate, "yyyy") & "'")
            End If
            cmdExe.Visible = True
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message)
        End Try
    End Sub

    Private Sub cmdExe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExe.Click
        Try
            CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(TxtQuery.Text)
            cmdReset_Click(sender, e)
            SetMsg(lblMsg, " Data Transfered Successfully.")
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & ": Save Record")
        End Try
    End Sub

    Private Sub cmbTransfer_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTransfer.SelectedIndexChanged
        lblMsg.Text = ""
        Dim StrQry As String
        If cmbTransfer.SelectedValue = 1 Then
            StrQry = " Delete From eHRMSFLI.dbo.PayHist Where PayDate = @DATE and Emp_Code in (Select Emp_Code From @DATABASE.dbo.PayHist Where PayDate = @DATE) ; Insert eHRMSFLI.dbo.PayHist Select * from @DATABASE.dbo.PayHist Where PayDate = @DATE "
            TxtQuery.Text = StrQry
        ElseIf cmbTransfer.SelectedValue = 2 Then
            StrQry = " Delete From eHRMSFLI.dbo.HrdHist Where PayDate = @DATE and Emp_Code in (Select Emp_Code From @DATABASE.dbo.HrdHist Where PayDate = @DATE) ; Insert eHRMSFLI.dbo.HrdHist Select * from @DATABASE.dbo.HrdHist Where PayDate = @DATE "
            TxtQuery.Text = StrQry
        ElseIf cmbTransfer.SelectedValue = 3 Then
            StrQry = " Delete From eHRMSFLI.dbo.PayMast Where FINYEAR = @DATE ;Insert eHRMSFLI.dbo.PayMast Select * from @DATABASE.dbo.PayMast Where FINYEAR = @DATE"
            TxtQuery.Text = StrQry
        ElseIf cmbTransfer.SelectedValue = 4 Then
            StrQry = " Delete From eHRMSFLI.dbo.ReimMast Where RIMYEAR = @DATE ;Insert eHRMSFLI.dbo.ReimMast Select * from @DATABASE.dbo.ReimMast Where RIMYEAR = @DATE"
            TxtQuery.Text = StrQry
        Else
            StrQry = " Delete From eHRMSFLI.dbo.ReimTran Where RIMYEAR = @DATE ;Insert eHRMSFLI.dbo.ReimTran Select * from @DATABASE.dbo.ReimTran Where RIMYEAR = @DATE"
            TxtQuery.Text = StrQry
        End If
    End Sub

    Private Sub cmdReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReset.Click
        cmbTransfer_SelectedIndexChanged(sender, e)
        TxtDatabase.Text = ""
        cmdExe.Visible = False
        CmbFor.SelectedIndex = 0
    End Sub

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub

End Class
