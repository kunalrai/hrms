Partial Class LeaveStatus
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
    Dim StrSql As String
    Dim BalObj As BAL.BLayer
    Dim dtLeave As New DataTable

#Region "    On Load   "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        lblMsg.Text = ""
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If
        If Not IsPostBack Then
            Try
                'By Ravi 21 Nov

                Dim SrNo As Int16
                SrNo = Request.QueryString.Item("SrNo") + Request.QueryString.Item("MODULEID")
                Dim bSuccess As Boolean
                Select Case CheckRight(SrNo)
                    Case MdlHRMS.AccessType.FullAccess, MdlHRMS.AccessType.SuperUser
                        bSuccess = True
                    Case MdlHRMS.AccessType.ReadonlyAccess, MdlHRMS.AccessType.Restricted
                        bSuccess = False
                End Select
                cmdDelete.Visible = bSuccess
                '------------------------------------

                cmbType.Items.Add(New ListItem("All", "All"))
                cmbType.Items.Add(New ListItem("Sanctioned", "S"))
                cmbType.Items.Add(New ListItem("Rejected", "R"))
                cmbType.Items.Add(New ListItem("Unprocessed", "A"))
                dtpFromDate.Text = Format(Date.Today, "dd/MMM/yyyy")
                dtpToDate.Text = Format(Date.Today, "dd/MMM/yyyy")
            Catch ex As Exception
                SetMsg(lblMsg, ex.Message)
            End Try
        End If
    End Sub

#End Region

#Region "   Fill Leave Status Grid   "

    Private Sub cmdShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdShow.Click
        grdLeavStatus.CurrentPageIndex = 0
        FillGrid()
    End Sub

    Private Sub FillGrid()
        Try
            If Chk(cmbType.SelectedValue) = "All" Then
                'StrSql = "select LA.levyear, LA.lvtype, LH.LvDesc, " & _
                '" (case when left(LA.lvtype,1) = right(LA.lvtype,1) then '1' else '0.5' end) as Lvdays," & _
                '" datename(d,AtDate)+'/'+ left(datename(MM,AtDate),3)+'/'+ datename(yyyy,AtDate) as atdate ," & _
                '" datename(d,AppDate)+'/'+ left(datename(MM,AppDate),3)+'/'+ datename(yyyy,AppDate) as appDate , " & _
                '"status=(case when Status ='A' then 'Unprocessed' when Status ='S' then 'Sanctioned' when Status ='R' then 'Rejected' end)" & _
                '"from leavapp LA inner join lvhelp LH on LA.LVTYPE = LH.LVTYPE" & _
                '" Where Emp_Code='" & Session("Loginuser").userId & "' and atdate between '" & dtpFromDate.Text & "' and '" & dtpToDate.Text & "'"

                StrSql = "select LA.levyear, LA.lvtype, LH.LvDesc, " & _
                " (case when left(LA.lvtype,1) = right(LA.lvtype,1) then '1' else '0.5' end) as Lvdays," & _
                " datename(d,AtDate)+'/'+ left(datename(MM,AtDate),3)+'/'+ datename(yyyy,AtDate) as atdate ," & _
                " cast(datename(d,AppDate)+'/'+ left(datename(MM,AppDate),3)+'/'+ datename(yyyy,AppDate)as smalldatetime) as appDate , " & _
                "status=(case when Status ='A' then 'Unprocessed' when Status ='S' then 'Sanctioned' when Status ='R' then 'Rejected' end)" & _
                "from leavapp LA inner join lvhelp LH on LA.LVTYPE = LH.LVTYPE" & _
                " Where Emp_Code='" & Session("Loginuser").userId & "' and atdate between '" & dtpFromDate.Text & "' and '" & dtpToDate.Text & "' order by appdate desc"


            Else
                StrSql = "select LA.levyear, LA.lvtype, LH.LvDesc, " & _
                " (case when left(LA.lvtype,1) = right(LA.lvtype,1) then '1' else '0.5' end) as Lvdays," & _
                " datename(d,AtDate)+'/'+ datename(MM,AtDate)+'/'+ datename(yyyy,AtDate) as atdate ," & _
                " cast(datename(d,AppDate)+'/'+ datename(MM,AppDate)+'/'+ datename(yyyy,AppDate)as smalldatetime) as appDate , " & _
                "status=(case when Status ='A' then 'Unprocessed' when Status ='S' then 'Sanctioned' when Status ='R' then 'Rejected' end)" & _
                " from leavapp LA inner join lvhelp LH on LA.LVTYPE = LH.LVTYPE" & _
                " Where Emp_Code='" & Session("Loginuser").userId & "' " & _
                " and atdate between '" & dtpFromDate.Text & "' and '" & dtpToDate.Text & "' and status='" & Chk(cmbType.SelectedValue) & "' order by appdate desc"

                'StrSql = "Select * from Leavapp Where Emp_Code='" & Session("Loginuser").userId & "' and atdate between '" & dtpFromDate.Text & "' and '" & dtpToDate.Text & "' and status='" & Chk(cmbType.SelectedValue) & "'"
            End If
            Session("DalObj").GetSqlDataTable(dtLeave, StrSql)

            grdLeavStatus.DataSource = dtLeave
            grdLeavStatus.DataBind()
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message)
        End Try
    End Sub

#End Region

#Region "   Delete Records    "

    Private Sub cmdDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        Try
            Dim TotRecords As Int16
            Dim Result As String

            For TotRecords = 0 To grdLeavStatus.Items.Count - 1
                Dim TmpChk As New HtmlInputCheckBox
                TmpChk = grdLeavStatus.Items.Item(TotRecords).Controls(6).Controls(1)
                Dim tmpAtDate As New TableCell
                tmpAtDate = grdLeavStatus.Items.Item(TotRecords).Controls(3)
                Dim tmpStatus As New TableCell
                tmpStatus = grdLeavStatus.Items.Item(TotRecords).Controls(5)

                If TmpChk.Checked Then
                    If tmpStatus.Text = "Unprocessed" Then
                        StrSql = "Delete from LeavApp where Emp_Code='" & Session("LoginUser").UserID & "' and AtDate='" & Format(CDate(tmpAtDate.Text), "dd/MMM/yyyy") & "'"
                        Session("DalObj").ExecuteCommand(StrSql)
                    Else
                        Result = Result & "The Leave of Dated '" & Format(CDate(tmpAtDate.Text), "dd/MMM/yyyy") & "' Can not be Deleted, As it is Already '" & Chk(tmpStatus.Text) & "'! <BR>"
                    End If
                End If
            Next
            SetMsg(lblMsg, Result)
            FillGrid()
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & "  " & ex.Source)
        End Try
    End Sub

#End Region

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Private Sub grdLeavStatus_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles grdLeavStatus.PageIndexChanged
        Try
            If (grdLeavStatus.CurrentPageIndex > grdLeavStatus.PageCount Or grdLeavStatus.CurrentPageIndex < 0) Then
                grdLeavStatus.CurrentPageIndex = 0
            Else
                grdLeavStatus.CurrentPageIndex = e.NewPageIndex
            End If
            FillGrid()
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & "  " & ex.Source)
        End Try
    End Sub

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub


End Class
