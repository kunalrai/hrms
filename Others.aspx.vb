Partial Class Others
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents DtpPAN As System.Web.UI.WebControls.TextBox
    Protected WithEvents TxtFPAN As System.Web.UI.WebControls.TextBox
    Protected WithEvents TxtMPAN As System.Web.UI.WebControls.TextBox
    Protected WithEvents TxtLPAN As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim Str As String
    Dim DTHrd As New DataTable
    Dim b As BAL.BLayer
    Dim d As New DAL.DataLayer
    Dim Ds As New DataSet
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Ajax.Utility.RegisterTypeForAjax(GetType(Others))
        If Server.GetLastError Is Nothing Then SetMsg(LblErrMsg, "")
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If
        Try
            'By Ravi 17 nov 2006
            Dim SrNo As Int16
            SrNo = Request.QueryString.Item("SrNo") + Request.QueryString.Item("MODULEID")
            If Not IsPostBack Then
                Dim bSuccess As Boolean
                Select Case CheckRight(SrNo)
                    Case MdlHRMS.AccessType.FullAccess, MdlHRMS.AccessType.SuperUser
                        bSuccess = True
                    Case MdlHRMS.AccessType.ReadonlyAccess
                        bSuccess = False
                    Case MdlHRMS.AccessType.Restricted
                        Response.Redirect(Request.UrlReferrer.ToString)
                        Exit Sub
                End Select
                cmdSave.Visible = bSuccess
            End If

            '---------------------------------------------
            'If CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules <> "" Then
            '    If InStr(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, SrNo & "-") <> 0 Then
            '        Dim int As Int16, st As String
            '        int = InStr(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, SrNo)
            '        st = Right(Mid(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, int, Len(SrNo & "-") + 1), 1)

            '        If st = "S" Then
            '            txtEM_CD.ReadOnly = False
            '        Else
            '            EnabledDisabled()
            '            txtEM_CD.ReadOnly = True
            '            cmdSave.Visible = False
            '        End If
            '    Else
            '        EnabledDisabled()
            '        txtEM_CD.ReadOnly = True
            '        cmdSave.Visible = False
            '        SetMsg(LblRights, "You are not authorised to view data on this page.")
            '        'Response.Write("<BR><center><B></B></center>")
            '        Exit Sub
            '    End If
            'End If

            If Not IsPostBack Then
                SetTextControlLength()
                txtEM_CD.Text = Session("EM_CD")
                Session("BalObj").FillCombo(cmbPayMode, "Select BANK_CODE, BANK_NAME from BANKMAST", True)
                'DisplayRecord()
                cmbLeaveType.Attributes.Add("onchange", "selChanged();")
            End If
            ' cmdSave.Attributes.Add("onclick", "return ValidateCtrl();")

        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message)
        End Try
    End Sub
    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
    Public Function SetCurrentEmpCode() As String
        Try
            SetCurrentEmpCode = Session("EM_CD")
        Catch ex As Exception
        End Try
    End Function
    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
    Public Function SetEmpCode(ByVal EmpCode As String) As DataSet
        Try
            Session("EM_CD") = EmpCode
        Catch ex As Exception

        End Try
    End Function

    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
    Public Function GetEmpRec() As DataSet
        Try
            Dim strSql As String
            ' strSql = "Select * From HRDMAST Where EMP_CODE='" & Session("EM_CD") & "'"
            strSql = "Select * From HRDMASTQRY Where EMP_CODE=" & "'" & Session("EM_CD") & "'" & Session("UserCodes")
            Session("DalObj").GetSqlDataSet(Ds, strSql)
            GetEmpRec = Ds
        Catch ex As Exception

        End Try
    End Function

    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
    Public Function GetNextEmpRec(ByVal OEmpCode As String) As String
        Try
            Dim strSql As String
            Dim EmpCode As String
            ' strSql = "Select Top 1 Emp_Code From HRDMAST Where EMP_CODE>'" & OEmpCode & "' Order By Emp_Code "
            strSql = "Select Top 1 Emp_Code From HRDMASTQRY Where EMP_CODE >" & "'" & OEmpCode & "' " & Session("UserCodes") & "  Order By Emp_Code "
            EmpCode = Chk(Session("DalObj").ExecuteCommand(strSql, DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
            GetNextEmpRec = EmpCode
        Catch ex As Exception

        End Try
    End Function

    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
    Public Function GetPreviousEmpRec(ByVal OEmpCode As String) As String
        Try
            Dim strSql As String
            Dim EmpCode As String
            ' strSql = "Select Top 1 Emp_Code From HRDMAST Where EMP_CODE<'" & OEmpCode & "' Order By Emp_Code desc "
            strSql = "Select Top 1 Emp_Code From HRDMASTQRY Where EMP_CODE< " & "'" & OEmpCode & "'" & Session("UserCodes") & " Order By Emp_Code desc "
            EmpCode = Chk(Session("DalObj").ExecuteCommand(strSql, DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
            GetPreviousEmpRec = EmpCode
        Catch ex As Exception

        End Try
    End Function

    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
    Public Function GetFirstEmpRec() As String
        Try
            Dim strSql As String
            Dim EmpCode As String
            ' strSql = "Select Top 1 Emp_Code From HRDMAST  Order By Emp_Code "
            strSql = "Select Top 1 Emp_Code From HRDMASTQRY where 1=1" & Session("UserCodes") & " Order By Emp_Code "
            EmpCode = Chk(Session("DalObj").ExecuteCommand(strSql, DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
            GetFirstEmpRec = EmpCode
        Catch ex As Exception

        End Try
    End Function
    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
    Public Function GetLastEmpRec() As String
        Try
            Dim strSql As String
            Dim EmpCode As String
            'strSql = "Select Top 1 Emp_Code From HRDMAST  Order By Emp_Code desc"
            strSql = "Select Top 1 Emp_Code From HRDMASTQRY Where 1=1 " & Session("UserCodes") & " Order By Emp_Code desc"
            EmpCode = Chk(Session("DalObj").ExecuteCommand(strSql, DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
            GetLastEmpRec = EmpCode
        Catch ex As Exception

        End Try
    End Function

#Region " Save Records "
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            'If cmbPayMode.SelectedItem.Text = "" Then
            '    LblErrMsg.Text = "Please select the Paymode"
            '    LblErrMsg.Visible = True
            '    Exit Sub
            'End If

            'If ViewState("Value") = False Then Exit Sub
            If ViewState("Value") = "NULL" Then Exit Sub
            Dim Code As Int16 = 0
            'If Len(TxtPANNO.Text) <> 10 Then
            '    SetMsg(LblErrMsg, "PAN No. must be of 10 Digits.")
            '    Exit Sub
            'End If

            Code = Session("DALObj").ExecuteCommand("Select Count(*) From HrdMast where Emp_Code='" & Chk(txtEM_CD.Text) & "'" & Session("UserCodes"), , DAL.DataLayer.ExecutionType.ExecuteScalar)

            If Code = 0 Then SetMsg(LblErrMsg, " Employee code does not exist.") : Exit Sub
            Dim SQLstr As String
            SQLstr = " Update HRDMAST Set " & _
                     " BANK_CODE='" & Chk(cmbPayMode.SelectedValue) & "', " & _
                     " DOCE = " & IIf(ChkContactEnd.Checked = True, "'" & Format(dtpContactEnd.DateValue, "dd/MMM/yyyy") & "'", "Null") & ", " & _
                     " BANKACNO='" & Chk(TxtBANKACNO.Text) & "', " & _
                     " PFNO = '" & Chk(TxtPFNO.Text) & "', " & _
                     " ESINO = '" & Chk(TxtESINO.Text) & "', " & _
                     " DOL = " & IIf(ChkDOL.Checked = True, "'" & Format(dtpDOL.DateValue, "dd/MMM/yyyy") & "'", "Null") & ", " & _
                     " LNOTICE = " & IIf(ChkNotice.Checked = True, "'" & Format(dtpNotice.DateValue, "dd/MMM/yyyy") & "'", "Null") & ", " & _
                     " SET_DATE = " & IIf(ChkSettle.Checked = True, "'" & Format(dtpSettle.DateValue, "dd/MMM/yyyy") & "'", "Null") & ", " & _
                     " DOJGroup = " & IIf(ChkGrpJoin.Checked = True, "'" & Format(dtpGrpJoin.DateValue, "dd/MMM/yyyy") & "'", "Null") & ", " & _
                     " LTYPE = '" & ChkN(cmbLeaveType.SelectedValue) & "', " & _
                     " PANNO = '" & Chk(TxtPANNO.Text) & "', "

            If cmbLeaveType.SelectedValue = 2 Then
                SQLstr = SQLstr & " NewOrg = '" & Chk(TxtNewOrg.Text) & "', "
            Else
                SQLstr = SQLstr & " NewOrg = Null ,"
            End If

            SQLstr = SQLstr & " LAST_APPR = " & IIf(ChkLAppraisal.Checked = True, "'" & Format(dtpLAppraisal.DateValue, "dd/MMM/yyyy") & "'", "Null") & ", " & _
                     " TANNO = '" & Chk(TxtTANNO.Text) & "', " & _
                     " LReason = '" & Chk(TxtLReason.Text) & "', " & _
                     " JOBPROFILE = '" & Chk(TxtJOBPROFILE.Text) & "'" & _
                     " Where EMP_CODE = '" & Chk(txtEM_CD.Text) & "'"

            '" LAST_INCR = " & IIf(ChkLInc.Checked = True, "'" & Format(DtpLInc.DateValue, "dd/MMM/yyyy") & "'", "Null") & ", " & _
            '" LAST_PROM = " & IIf(ChkLPromo.Checked = True, "'" & Format(DtpLPromo.DateValue, "dd/MMM/yyyy") & "'", "Null") & ", " & _
            Session("DALObj").ExecuteCommand(SQLstr)
            SetMsg(LblErrMsg, " Record Saved Successfully.")
            ' DisplayRecord()
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message)
        End Try
    End Sub

#End Region

#Region " Display Records "

    'Private Sub txtEM_CD_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEM_CD.TextChanged
    '    Try
    '        If Trim(txtEM_CD.Text) <> "" Then Session("EM_CD") = Trim(txtEM_CD.Text)
    '        DisplayRecord()
    '        RegisterStartupScript("txtEM_CD", "<SCRIPT language='javascript'>document.getElementById('cmbPayMode').focus() </SCRIPT>")
    '    Catch ex As Exception
    '        SetMsg(LblErrMsg, ex.Message & " : (txtEM_CD_TextChanged)")
    '    End Try
    'End Sub

    'Public Sub DisplayRecord()
    '    Try
    '        Dim EmpName As Object
    '        Str = "SELECT * FROM HRDMAST Where EMP_CODE='" & Session("EM_CD") & "'" & Session("UserCodes")
    '        Session("DALObj").GetSqlDataTable(DTHrd, Str)

    '        EmpName = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(" Select Emp_Name From HrdMastQry Where Emp_Code = '" & Chk(txtEM_CD.Text) & "'" & Session("UserCodes"), , DAL.DataLayer.ExecutionType.ExecuteScalar)

    '        If (Not IsDBNull(EmpName)) And (Not IsNothing(EmpName)) Then
    '            'LblName.Text = EmpName

    '            ViewState("Value") = True
    '        Else

    '            Dim Code As Object
    '            Code = Session("DalObj").ExecuteCommand("Select Emp_Code From HrdMastQry Where Emp_Code = '" & Chk(txtEM_CD.Text) & "'" & Session("UserCodes"), , DAL.DataLayer.ExecutionType.ExecuteScalar)

    '            If Code <> "" Then
    '                SetMsg(LblErrMsg, "This Employee Code Exist For Other Location.")
    '            End If

    '            'ViewState("Value") = False
    '            ViewState("Value") = False
    '            'LblName.Text = ""

    '        End If

    '        '------------- Initially Clearing & Disbaling of Controls ------------------
    '        ClearAll(Me)
    '        EnabledDisabled()

    '        '----------------------- Display Of Records --------------------------
    '        txtEM_CD.Text = Session("EM_CD")
    '        TxtFname.Text = EmpName
    '        If DTHrd.Rows.Count = 0 Then ViewState("Value") = False : Exit Sub
    '        ChkCombo(cmbPayMode, Chk(DTHrd.Rows(0).Item("BANK_CODE")))
    '        TxtESINO.Text = Chk(DTHrd.Rows(0).Item("ESINO"))
    '        TxtPANNO.Text = Chk(DTHrd.Rows(0).Item("PANNO"))
    '        TxtPFNO.Text = Chk(DTHrd.Rows(0).Item("PFNO"))
    '        TxtLReason.Text = Chk(DTHrd.Rows(0).Item("LReason"))
    '        TxtBANKACNO.Text = Chk(DTHrd.Rows(0).Item("BANKACNO"))
    '        TxtTANNO.Text = Chk(DTHrd.Rows(0).Item("TANNO"))
    '        TxtJOBPROFILE.Text = Chk(DTHrd.Rows(0).Item("JOBPROFILE"))

    '        If Not IsDBNull(DTHrd.Rows(0).Item("DOCE")) Then
    '            DtpContactEnd.Enabled = True
    '            DtpContactEnd.DateValue = DTHrd.Rows(0).Item("DOCE")
    '            ChkContactEnd.Checked = True
    '        End If
    '        'If Not IsDBNull(DTHrd.Rows(0).Item("DOC")) Then
    '        '    DtpConfirmation.Enabled = True
    '        '    DtpConfirmation.DateValue = DTHrd.Rows(0).Item("DOC")
    '        '    ChkConfirmation.Checked = True
    '        'End If
    '        If Not IsDBNull(DTHrd.Rows(0).Item("LAST_APPR")) Then
    '            DtpLAppraisal.Enabled = True
    '            DtpLAppraisal.DateValue = DTHrd.Rows(0).Item("LAST_APPR")
    '            ChkLAppraisal.Checked = True
    '        End If
    '        'If Not IsDBNull(DTHrd.Rows(0).Item("LAST_INCR")) Then
    '        '    DtpLInc.Enabled = True
    '        '    DtpLInc.DateValue = DTHrd.Rows(0).Item("LAST_INCR")
    '        '    ChkLInc.Checked = True
    '        'End If
    '        'If Not IsDBNull(DTHrd.Rows(0).Item("LAST_PROM")) Then
    '        '    DtpLPromo.Enabled = True
    '        '    DtpLPromo.DateValue = DTHrd.Rows(0).Item("LAST_PROM")
    '        '    ChkLPromo.Checked = True
    '        'End If

    '        Dim LastIncr, LastProm As Date

    '        If Not IsDBNull(CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand("Select Max(ToDate)+1 From WorkHistory Where EMp_Code = '" & Session("EM_CD") & "' and (EmpStatus = 'Salary Changed' or EmpStatus='Promotion With Salary Changed' or EmpStatus='Redesignation With Salary Change') ", , DAL.DataLayer.ExecutionType.ExecuteScalar)) Then
    '            LastIncr = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand("Select Max(ToDate)+1  From WorkHistory Where EMp_Code = '" & Session("EM_CD") & "' and (EmpStatus = 'Salary Changed' or EmpStatus='Promotion With Salary Changed' or EmpStatus='Redesignation With Salary Change')", , DAL.DataLayer.ExecutionType.ExecuteScalar)
    '            DtpLInc.Enabled = True
    '            ChkLInc.Checked = True
    '            DtpLInc.DateValue = LastIncr
    '        End If

    '        If Not IsDBNull(CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand("Select Max(ToDate)+1 From WorkHistory Where EMp_Code = '" & Session("EM_CD") & "' and (EmpStatus = 'Promotion' or EmpStatus='Promotion With Salary Changed')", , DAL.DataLayer.ExecutionType.ExecuteScalar)) Then
    '            LastProm = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand("Select Max(ToDate)+1 From WorkHistory Where EMp_Code = '" & Session("EM_CD") & "' and (EmpStatus = 'Promotion' or EmpStatus='Promotion With Salary Changed')", , DAL.DataLayer.ExecutionType.ExecuteScalar)
    '            DtpLPromo.Enabled = True
    '            ChkLPromo.Checked = True
    '            DtpLPromo.DateValue = LastProm
    '        End If

    '        If Not IsDBNull(DTHrd.Rows(0).Item("DOJGroup")) Then
    '            DtpGrpJoin.Enabled = True
    '            DtpGrpJoin.DateValue = DTHrd.Rows(0).Item("DOJGroup")
    '            ChkGrpJoin.Checked = True
    '        End If
    '        SetSaperation()
    '        If cmbLeaveType.SelectedValue = 2 Then
    '            TxtNewOrg.Text = Chk(DTHrd.Rows(0).Item("NewOrg"))
    '        End If
    '    Catch ex As Exception
    '        SetMsg(LblErrMsg, ex.Message & " : (DisplayRecord)")
    '    End Try
    'End Sub

    Private Sub SetSaperation()
        Try
            If Not IsDBNull(DTHrd.Rows(0).Item("LTYPE")) Then
                cmbLeaveType.SelectedValue = DTHrd.Rows(0).Item("LTYPE")
            End If

            cmbLeaveType_SelectedIndexChanged(Nothing, Nothing)
            If Not IsDBNull(DTHrd.Rows(0).Item("DOL")) Then
                dtpDOL.Enabled = True
                ChkDOL.Checked = True
                dtpDOL.DateValue = DTHrd.Rows(0).Item("DOL")
            End If

            If Not IsDBNull(DTHrd.Rows(0).Item("LNOTICE")) Then
                dtpNotice.Enabled = True
                ChkNotice.Checked = True
                dtpNotice.DateValue = DTHrd.Rows(0).Item("LNOTICE")

            End If

            If Not IsDBNull(DTHrd.Rows(0).Item("SET_DATE")) Then
                dtpSettle.Enabled = True
                ChkSettle.Checked = True
                dtpSettle.DateValue = DTHrd.Rows(0).Item("SET_DATE")
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & ex.Source)
        End Try
    End Sub

#End Region

#Region " LeavType ComboBox Changed "

    Private Sub cmbLeaveType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbLeaveType.SelectedIndexChanged
        If cmbLeaveType.SelectedValue = 1 Then
            LblDate1.Text = ""
            LblDate2.Text = ""
            LblDate3.Text = ""
            LblNewOrg.Text = ""

            dtpDOL.Visible = False
            dtpNotice.Visible = False
            dtpSettle.Visible = False

            ChkNotice.Visible = False
            ChkSettle.Visible = False
            ChkDOL.Visible = False
            ChkNotice.Checked = False
            ChkSettle.Checked = False
            ChkDOL.Checked = False

            TxtNewOrg.Visible = False
        ElseIf cmbLeaveType.SelectedValue = 2 Then
            LblDate1.Text = " Resignated Date"
            LblDate2.Text = " Releaving Date"
            LblDate3.Text = " Settlement Date"
            LblNewOrg.Text = "New Organisation"
            TxtNewOrg.Visible = True
            dtpDOL.Visible = True
            dtpNotice.Visible = True
            dtpSettle.Visible = True
            ChkNotice.Visible = True
            ChkSettle.Visible = True
            ChkDOL.Visible = True
        ElseIf cmbLeaveType.SelectedValue = 3 Then
            LblDate1.Text = " Notice Date"
            LblDate2.Text = " Termination Date"
            LblDate3.Text = " Settlement Date"
            LblNewOrg.Text = ""
            dtpDOL.Visible = True
            dtpNotice.Visible = True
            dtpSettle.Visible = True
            ChkNotice.Visible = True
            ChkSettle.Visible = True
            ChkDOL.Visible = True
            TxtNewOrg.Visible = False
        ElseIf cmbLeaveType.SelectedValue = 4 Then
            LblDate1.Text = " Notice Date"
            LblDate2.Text = " Retirement Date"
            LblDate3.Text = " Settlement Date"
            LblNewOrg.Text = ""
            dtpDOL.Visible = True
            dtpNotice.Visible = True
            dtpSettle.Visible = True
            ChkNotice.Visible = True
            ChkSettle.Visible = True
            ChkDOL.Visible = True
            TxtNewOrg.Visible = False
        ElseIf cmbLeaveType.SelectedValue = 5 Then
            LblDate1.Text = ""
            LblDate2.Text = " Pay Holding Date"
            LblDate3.Text = ""
            LblNewOrg.Text = ""
            dtpDOL.Visible = True
            dtpNotice.Visible = False
            dtpSettle.Visible = False
            ChkNotice.Visible = False
            ChkSettle.Visible = False
            ChkDOL.Visible = True
            ChkNotice.Checked = False
            ChkSettle.Checked = False
            TxtNewOrg.Visible = False
        ElseIf cmbLeaveType.SelectedValue = 6 Then
            LblDate1.Text = ""
            LblDate2.Text = ""
            LblDate3.Text = ""
            LblNewOrg.Text = ""
            dtpDOL.Visible = False
            dtpNotice.Visible = False
            dtpSettle.Visible = False
            ChkNotice.Visible = False
            ChkSettle.Visible = False
            ChkDOL.Visible = False
            ChkNotice.Checked = False
            ChkSettle.Checked = False
            ChkDOL.Checked = False
            TxtNewOrg.Visible = False
        ElseIf cmbLeaveType.SelectedValue = 7 Then
            LblDate1.Text = " Death Date"
            LblDate2.Text = " Releaving Date"
            LblDate3.Text = " Settlement Date"
            LblNewOrg.Text = ""
            dtpDOL.Visible = True
            dtpNotice.Visible = True
            dtpSettle.Visible = True
            ChkNotice.Visible = True
            ChkSettle.Visible = True
            ChkDOL.Visible = True
            TxtNewOrg.Visible = False
        Else
            LblDate1.Text = " Transfer Date"
            LblDate2.Text = " Releaving Date"
            LblDate3.Text = " Settlement Date"
            LblNewOrg.Text = ""
            dtpDOL.Visible = True
            dtpNotice.Visible = True
            dtpSettle.Visible = True
            ChkNotice.Visible = True
            ChkSettle.Visible = True
            ChkDOL.Visible = True
            TxtNewOrg.Visible = False
        End If
    End Sub

#End Region
    Private Sub EnabledDisabled()
        'DtpConfirmation.Enabled = False
        'ChkConfirmation.Checked = False
        dtpContactEnd.Enabled = False
        ChkContactEnd.Checked = False

        dtpGrpJoin.Enabled = False
        ChkGrpJoin.Checked = False

        dtpLAppraisal.Enabled = False
        ChkLAppraisal.Checked = False

        dtpLPromo.Enabled = False
        ChkLPromo.Checked = False

        dtpLInc.Enabled = False
        ChkLInc.Checked = False

        dtpDOL.Enabled = False
        ChkDOL.Checked = False

        dtpSettle.Enabled = False
        ChkSettle.Checked = False

        dtpNotice.Enabled = False
        ChkNotice.Checked = False

        cmbLeaveType.SelectedValue = 1
        cmbLeaveType_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("EmpExplorer.aspx?SrNo=61")
    End Sub

    Sub SetTextControlLength()
        Try
            Dim StrQr As String, DtNew As New DataTable, i As Int16
            StrQr = "SELECT SYSCOLUMNS.NAME, SYSCOLUMNS.LENGTH FROM SYSCOLUMNS INNER JOIN SYSOBJECTS ON  SYSCOLUMNS.ID = SYSOBJECTS.ID WHERE SYSOBJECTS.NAME='HRDMAST'"
            CType(Session("DalObj"), DAL.DataLayer).GetSqlDataTable(DtNew, StrQr)

            Dim j As Int16
            For i = 0 To DtNew.Rows.Count - 1
                If Not IsNothing(Page.FindControl("txt" & DtNew.Rows(i).Item(0))) Then
                    CType(Page.FindControl("txt" & DtNew.Rows(i).Item(0)), TextBox).MaxLength = DtNew.Rows(i).Item(1)
                    'j = j + 1
                End If
            Next
            ' Response.Write(j & " Records Set")
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " SetTextControlLength()")
        End Try
    End Sub
    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub
    Private Sub ChkDOL_ServerChange(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkDOL.ServerChange

    End Sub
End Class
