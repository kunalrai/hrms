Partial Class LeavSetup
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
    Dim oDAL As DAL.DataLayer
    Dim oBAL As BAL.BLayer
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If
        oDAL = Session("DalObj")
        oBAL = Session("BalObj")
        If Not IsPostBack Then

            'By Ravi 22 NovEM

            Dim SrNo As Int16
            SrNo = Request.QueryString.Item("SrNo") + Request.QueryString.Item("MODULEID")
            Dim bSuccess As Boolean
            Select Case CheckRight(SrNo)
                Case MdlHRMS.AccessType.FullAccess, MdlHRMS.AccessType.SuperUser
                    bSuccess = True
                Case MdlHRMS.AccessType.ReadonlyAccess, MdlHRMS.AccessType.Restricted
                    bSuccess = False
            End Select
            cmdSave.Visible = bSuccess
            '------------------------------------
            ''''If CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules <> "" Then
            ''''    If InStr(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, SrNo & "-") <> 0 Then
            ''''        Dim int As Int16, st As String
            ''''        int = InStr(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, SrNo)
            ''''        st = Right(Mid(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, int, Len(SrNo & "-") + 1), 1)

            ''''        If st <> "S" Then
            ''''            cmdSave.Visible = False
            ''''        End If
            ''''    Else
            ''''        Response.Redirect("Main.aspx")
            ''''        'Response.Write("<BR><center><B></B></center>")
            ''''        Exit Sub
            ''''    End If
            ''''End If
            'SetFieldSize()
            btnnew_click(sender, Nothing)
        End If
    End Sub
    Private Sub btnList_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnList.Click
        LblErrMsg.Text = ""
        cmbleavtype.Visible = True
        Txtleavtype.Visible = False
        btnList.Visible = False
        btnNew.Visible = False
    End Sub
    Private Sub FillCombo()
        Try
            Session("BalObj").FillCombo(cmbleavtype, "Select LVTYPE, LVDESC from Lvtype", True)
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (FillCombo)")
        End Try
    End Sub

#Region "    Display Records   "

    Private Sub cmbleavtype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbleavtype.SelectedIndexChanged
        Try
            If cmbleavtype.SelectedValue = "" Then Exit Sub
            Txtleavtype.Visible = True
            btnList.Visible = True
            btnNew.Visible = True
            cmbleavtype.Visible = False
            Txtleavtype.Text = cmbleavtype.SelectedValue
            DisplayRecords(Chk(Txtleavtype.Text))
            ViewState("Action") = "MODIFY"
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message)
        End Try
    End Sub

    Private Sub DisplayRecords(ByVal Code As String)
        Try
            Dim dttemp As New DataTable
            Dim i As Int16, SqlStr As String
            SqlStr = " Select * from lvtype where lvtype = '" & Code & "'"
            Session("DalObj").GetSqlDataTable(dttemp, SqlStr)
            If dttemp.Rows.Count = 0 Then Exit Sub

            txtcrdays.Text = Chk(dttemp.Rows(0).Item("Cr_days"))
            Txtcrlimit.Text = Chk(dttemp.Rows(0).Item("Cr_limit"))
            txtDesc.Text = Chk(dttemp.Rows(0).Item("LvDesc"))
            Txtencashlimit.Text = ChkN(dttemp.Rows(0).Item("ENCASH_LMT"))

            If ChkN(dttemp.Rows(0).Item("Cr_period")) <> 0 Then
                cmbcrfreq.SelectedValue = ChkN(dttemp.Rows(0).Item("Cr_period"))
            Else
                cmbcrfreq.SelectedValue = 1
            End If

            If ChkN(dttemp.Rows(0).Item("Cr_month")) <> 0 Then
                cmbcrmonths.SelectedValue = ChkN(dttemp.Rows(0).Item("Cr_month"))
            End If


            If Not IsDBNull(dttemp.Rows(0).Item("Lapse")) Then
                Chklapse.Checked = IIf(Chk(dttemp.Rows(0).Item("Lapse")) = "N", False, True)
            Else
                Chklapse.Checked = False
            End If

            If Not IsDBNull(dttemp.Rows(0).Item("AddHolidays")) Then
                ChkIncHolidays.Checked = IIf(ChkN(dttemp.Rows(0).Item("AddHolidays")) = "0", False, True)
            Else
                ChkIncHolidays.Checked = False
            End If

            If Not IsDBNull(dttemp.Rows(0).Item("Paid")) Then
                Chklpaid.Checked = IIf(Chk(dttemp.Rows(0).Item("Paid")) = "N", False, True)
            Else
                Chklpaid.Checked = False
            End If

        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Display Records")
        End Try
    End Sub

#End Region

    Private Sub btnnew_click(ByVal sender As System.Object, ByVal e As System.web.ui.ImageClickEventArgs) Handles btnNew.Click
        Try
            LblErrMsg.Text = ""
            ClearAll(Me)
            FillCombo()
            ViewState("Action") = "ADDNEW"
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message)
        End Try
    End Sub

#Region "     Save Records     "

    Private Sub cmdsave_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim tran As SqlClient.SqlTransaction
        Try
            Dim strsql As String
            If Not IsValidate() Then Exit Sub
            tran = Session("DalObj").StartTransaction("Save")
            If ViewState("Action") = "ADDNEW" Then

                strsql = " insert into lvtype(lvtype,lvdesc,cr_period,cr_month,cr_days,cr_limit,encash_lmt,paid,AddHolidays,lapse) values ('" & _
                        Chk(Txtleavtype.Text) & "', '" & _
                        Chk(txtDesc.Text) & "', " & _
                        ChkN(cmbcrfreq.SelectedValue) & ", " & _
                        ChkN(cmbcrmonths.SelectedValue) & ", '" & _
                        Chk(txtcrdays.Text) & "', '" & _
                        Chk(Txtcrlimit.Text) & "', " & _
                        ChkN(Txtencashlimit.Text) & ", '" & _
                        Chk(IIf(Chklpaid.Checked, "Y", "N")) & "', '" & _
                        Chk(IIf(ChkIncHolidays.Checked, "1", "0")) & "', '" & _
                        Chk(IIf(Chklapse.Checked, "Y", "N")) & "')"

                Session("dalobj").executecommand(strsql, tran)

            ElseIf ViewState("Action") = "MODIFY" Then

                strsql = " update lvtype set " & _
                         " lvtype= '" & Chk(Txtleavtype.Text) & "', " & _
                         " lvDesc= '" & Chk(txtDesc.Text) & "', " & _
                         " cr_period = " & ChkN(cmbcrfreq.SelectedValue) & ", " & _
                         " cr_month = " & ChkN(cmbcrmonths.SelectedValue) & ", " & _
                         " cr_days = '" & Chk(txtcrdays.Text) & "', " & _
                         " cr_limit = '" & Chk(Txtcrlimit.Text) & "', " & _
                         " encash_lmt   = " & ChkN(Txtencashlimit.Text) & ", " & _
                         " lapse = '" & Chk(IIf(Chklapse.Checked, "Y", "N")) & "', " & _
                         " AddHolidays = '" & Chk(IIf(ChkIncHolidays.Checked, "1", "0")) & "', " & _
                         " paid = '" & Chk(IIf(Chklpaid.Checked, "Y", "N")) & "' " & _
                         " where lvtype= '" & Chk(Txtleavtype.Text) & "'"
                Session("dalobj").executecommand(strsql, tran)

            End If
            tran.Commit()
            btnnew_click(sender, Nothing)
            SetMsg(LblErrMsg, "Record Saved Successfully.")
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (cmdsave_click)")
            tran.Rollback()
        Finally
            If Not IsNothing(tran) Then tran.Dispose()
        End Try
    End Sub

    Function IsValidate() As Boolean
        Try
            Dim Code As Int16

            If (Txtleavtype.Text) = "" Then
                SetMsg(LblErrMsg, "Leave Type Can Not Be Blank.")
                Return False
                'Else if(Txtleavtype.Text)="" then
                SetMsg(LblErrMsg, " This Leave already Exist.")
            End If

            If ViewState("Action") = "ADDNEW" Then
                Code = Session("DalObj").ExecuteCommand(" Select Count(*) From LVTYPE Where LvType = '" & Chk(Txtleavtype.Text) & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
                If Code <> 0 Then
                    SetMsg(LblErrMsg, "Leave Type " & Chk(Txtleavtype.Text) & " already exist.")
                    Return False
                End If
            End If

            If (txtDesc.Text) = "" Then
                SetMsg(LblErrMsg, "Description Can Not Be Blank.")
                Return False
            End If
            Return True

        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Validate Records")
        End Try
    End Function
#End Region

    Private Sub cmdclose_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub
    '''Public Sub SetFieldSize()
    '''    Dim dt As New DataTable
    '''    Try
    '''        oDAL.GetSqlDataTable(dt, "SELECT * FROM LvType Where 1<>1")
    '''        Txtleavtype.MaxLength = dt.Columns("lvtype").MaxLength
    '''        txtDesc.MaxLength = dt.Columns("lvDesc").MaxLength
    '''        txtcrdays.MaxLength = dt.Columns("cr_days").MaxLength
    '''        Txtcrlimit.MaxLength = dt.Columns("cr_limit").MaxLength

    '''        dt.Dispose()
    '''    Catch ex As Exception
    '''        SetMsg(LblErrMsg, ex.Message)
    '''    End Try
    '''End Sub
End Class
