Imports DITWebLibrary
Partial Class AdjustmentNew
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
    Dim DtDisplay As New DataTable
    Dim DtBind As New DataTable
    Dim SQLStr, StrSql, Value, LocCode, WOFF As String, i As Int16, OkForSave As Boolean
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Dim FYS As Date
        Dim Item As ListItem
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If
        Try
            If Not IsPostBack Then

                'By Ravi 22 nov 2006
                Dim SrNo As Int16
                SrNo = Request.QueryString.Item("SrNo") + Request.QueryString.Item("MODULEID")
                Dim bSuccess As Boolean
                Select Case CheckRight(SrNo)
                    Case MdlHRMS.AccessType.FullAccess, MdlHRMS.AccessType.SuperUser
                        bSuccess = True
                    Case MdlHRMS.AccessType.ReadonlyAccess, MdlHRMS.AccessType.Restricted
                        bSuccess = False
                End Select
                CmdSave.Visible = bSuccess
                cmdReqAdd.Visible = bSuccess
                '-----------------------------------------------------------------

                FillComboBox()
                Session("BalObj").FillCombo(cmbEmp, "Select Emp_Code,Emp_Name as EMP_NAME From HrdMastQry Where  LType=1 " & Session("UserCodes") & " Order by Emp_Name", True)
                btnNew_Click(sender, Nothing)
                cmbPaydate.SelectedIndex = IIf(Date.Today.Month - 4 < 0, Date.Today.Month - 4 + 12, Date.Today.Month - 4)
            End If
            CmdSave.Attributes.Add("onclick", "return ValidateCtrl();")
        Catch ex As Exception
            LblErrMsg.Text = ex.Message & "Page Load"
        End Try

    End Sub
    Private Function FillComboBox()
        Try
            Dim LItem As ListItem, i As Int16
            Dim DtDate As Date
            DtDate = FY_Start
            For i = 1 To 12
                LItem = New ListItem
                LItem.Text = MonthName(Month(DtDate)) & " " & Year(DtDate)
                LItem.Value = EOM(DtDate)
                cmbPaydate.Items.Add(LItem)
                DtDate = LItem.Value
                DtDate = DateAdd(DateInterval.Month, 1, DtDate)
            Next
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & ": FillComboBox()")
        End Try
    End Function
    Private Sub btnList_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnList.Click
        TxtCode.Visible = False
        btnList.Visible = False
        cmbEmp.Visible = True
        LblErrMsg.Text = ""
    End Sub

    Private Sub cmbEmp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbEmp.SelectedIndexChanged
        If cmbEmp.SelectedValue <> "" Then
            TxtCode.Text = cmbEmp.SelectedValue
        Else
            TxtCode.Text = ""
        End If
        TxtCode_TextChanged(sender, e)
        TxtCode.Visible = True
        btnList.Visible = True
        cmbEmp.Visible = False
        BindgrdAdjust()
    End Sub

    Private Sub TxtCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtCode.TextChanged
        TxtRemarks.Text = ""
        DisplayRecords()
    End Sub

    Sub DisplayRecords()
        Try
            Dim StrSQl As String, dtEmp As New DataTable
            Dim EmpName As Object
            LblErrMsg.Text = ""
            TxtRemarks.Text = ""
            StrSQl = "Select * From HrdMastQry Where Emp_Code = '" & Chk(TxtCode.Text) & "'" & Session("UserCodes")
            Session("DalObj").GetSqlDataTable(dtEmp, StrSQl)
            EmpName = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(" Select Emp_Name From HrdMastQry Where Emp_Code = '" & Chk(TxtCode.Text) & "'" & Session("UserCodes"), , DAL.DataLayer.ExecutionType.ExecuteScalar)

            If (Not IsDBNull(EmpName)) And (Not IsNothing(EmpName)) Then
                LblName.Text = EmpName
            Else
                Dim Code As Object
                Code = Session("DalObj").ExecuteCommand("Select Emp_Code From HrdMastQry Where Emp_Code = '" & Chk(TxtCode.Text) & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
                If Code <> "" Then
                    SetMsg(LblErrMsg, "This Employee Code Exist For Other Location.")
                End If
                LblName.Text = ""
            End If
            dtEmp = Nothing
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " Display Records")
        End Try
    End Sub

    Private Sub BindgrdAdjust()
        Try
            ViewState("dtAdjust") = New DataTable
            Dim Dttemp As New DataTable
            Dim StrSql As String

            'StrSql = "select AdjTran.Field_Name as [Field Name], AdjTran.Amount, paysetup.Field_Desc As [Description] from AdjTran inner join paysetup on AdjTran.field_name=Paysetup.Field_Name Where emp_code='" & Chk(TxtCode.Text) & "' and Paydate='" & (cmbPaydate.SelectedValue) & "'"
            StrSql = "select AdjTran.Field_Name as [Field Name], AdjTran.Amount,AdjTran.Remarks, paysetup.Field_Desc As [Description] from AdjTran inner join paysetup on AdjTran.field_name=Paysetup.Field_Name Where emp_code='" & Chk(TxtCode.Text) & "' and Paydate='" & cmbPaydate.SelectedValue & "'"
            'StrSql = "select AdjTran.Field_Name as [Field Name], AdjTran.Amount, paysetup.Field_Desc As [Description] from AdjTran inner join paysetup on AdjTran.field_name=Paysetup.Field_Name Where emp_code='" & Chk(TxtCode.Text) & "'"
            Session("DalObj").GetSqlDataTable(ViewState("dtAdjust"), StrSql)
            Session("DalObj").GetSqlDataTable(Dttemp, StrSql)
            If Dttemp.Rows.Count > 0 Then
                'Comment by Ravi
                'TxtRemarks.Text = Chk(Dttemp.Rows(1).Item("Remarks"))  
                'by Ravi on dec 2 2006
                TxtRemarks.Text = Chk(Dttemp.Rows(0).Item("Remarks"))
            End If
            grdAdjust.DataSource = ViewState("dtAdjust")
            grdAdjust.DataBind()
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (BindGrdAdjust)")
        Finally
        End Try
    End Sub

    Private Sub grdAdjust_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdAdjust.ItemDataBound
        Try
            Dim dtreq As DataTable
            Dim tmpCmb As DropDownList

            Dim i As Int16 = e.Item.ItemIndex
            If i < 0 Then i = 0
            tmpCmb = e.Item.FindControl("cmbField")
            If Not tmpCmb Is Nothing Then
                If Not tmpCmb.Items.Count > 0 Then
                    dtreq = New DataTable
                    '"select distinct formula.field_name as [Field Name], paysetup.Field_Desc As [Description] from formula inner join paysetup on formula.field_name=Paysetup.Field_Name where charindex('GETTRN([A],', cast(fld_calc as varchar(8000)),1)>0 order by formula.field_name")
                    'Session("DalObj").GetSqlDataTable(dtreq, "select AdjTran.Field_Name as [Field Name], AdjTran.Amount, paysetup.Field_Desc As [Description] from AdjTran inner join paysetup on AdjTran.field_name=Paysetup.Field_Name Where emp_code='" & Chk(TxtCode.Text) & "' ")
                    Session("DalObj").GetSqlDataTable(dtreq, "select distinct formula.field_name as [Field Name], paysetup.Field_Desc As [Description] from formula inner join paysetup on formula.field_name=Paysetup.Field_Name where charindex('GETTRN([A],', cast(fld_calc as varchar(8000)),1)>0 order by formula.field_name")
                    tmpCmb.DataSource = dtreq
                    tmpCmb.DataValueField = "Field Name"
                    tmpCmb.DataTextField = "Field Name"
                    tmpCmb.DataBind()
                End If
                If Not IsNothing(tmpCmb.Items.FindByValue(Chk(grdAdjust.DataSource.rows(i).item("Field Name")))) Then
                    tmpCmb.SelectedValue = Chk(grdAdjust.DataSource.rows(i).item("Field Name"))
                    tmpCmb.SelectedItem.Text = Chk(grdAdjust.DataSource.rows(i).item("Field Name"))
                End If
            End If
            GC.Collect()
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (grdAdjust_ItemCreated)")
        End Try
    End Sub

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub
    Private Sub cmdReqAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdReqAdd.Click
        Try
            Dim i As Int16
            If grdAdjust.Items.Count > 0 Then
                If Chk(CType(grdAdjust.Items(grdAdjust.Items.Count - 1).Controls(0).Controls(1), DropDownList).SelectedValue) <> "" Then

                    For i = 0 To grdAdjust.Items.Count - 1
                        ViewState("dtAdjust").Rows(i).Item("Field Name") = Chk(CType(grdAdjust.Items(i).Controls(0).Controls(1), DropDownList).SelectedValue)
                        ViewState("dtAdjust").Rows(i).Item("Description") = Chk(CType(grdAdjust.Items(i).Controls(1).Controls(1), TextBox).Text)
                        ViewState("dtAdjust").Rows(i).Item("Amount") = ChkN(CType(grdAdjust.Items(i).Controls(2).Controls(1), TextBox).Text)
                    Next
                    Dim tmpTr As DataRow = ViewState("dtAdjust").NewRow()
                    ViewState("dtAdjust").Rows.Add(tmpTr)
                    grdAdjust.DataSource = ViewState("dtAdjust")
                    grdAdjust.DataBind()
                End If
            Else
                Dim tmpTr As DataRow = ViewState("dtAdjust").NewRow()
                ViewState("dtAdjust").Rows.Add(tmpTr)
                grdAdjust.DataSource = ViewState("dtAdjust")
                grdAdjust.DataBind()
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (cmdReqAdd_Click)")
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNew.Click
        Try
            ClearAll(Me)
            cmbEmp.Visible = False
            TxtCode.Visible = True
            TxtCode.Text = ""
            LblErrMsg.Text = ""
            LblName.Text = ""
            BindgrdAdjust()
            ViewState("Action") = "ADDNEW"
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (btnNew_Click)")
        End Try
    End Sub

    Private Sub CmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdSave.Click
        Dim Tran As SqlClient.SqlTransaction
        Dim cnt, cnt1 As Int16
        Dim strSQl As String, i, j As Int16
        Try
            If Not IsValidate() Then Exit Sub
            Tran = Session("DalObj").StartTransaction("Save")
            strSQl = "Delete from AdjTran where emp_code='" & Chk(TxtCode.Text) & "' and Paydate='" & (cmbPaydate.SelectedValue) & "' "
            Session("DalObj").ExecuteCommand(strSQl, Tran)
            cnt1 = grdAdjust.Items.Count
            If cnt1 = 0 Then
                SetMsg(LblErrMsg, "Please Select Atleast One A/C Head.")
                Exit Sub
            Else
                For cnt = 0 To grdAdjust.Items.Count - 1
                    If Chk(CType(grdAdjust.Items(cnt).Controls(0).Controls(1), DropDownList).SelectedValue) <> "" Then
                        If Chk(CType(grdAdjust.Items(cnt).Controls(2).Controls(1), TextBox).Text) = 0 Then
                            SetMsg(LblErrMsg, "Records of Zero Amount has Deleted.")
                        Else
                            strSQl = "Insert InTo AdjTran(Emp_code,Field_Name,Amount,PayDate,Remarks)"
                            strSQl &= "Values('"
                            strSQl &= TxtCode.Text & "','"
                            strSQl &= Chk(CType(grdAdjust.Items(cnt).Controls(0).Controls(1), DropDownList).SelectedValue) & "','"
                            strSQl &= Chk(CType(grdAdjust.Items(cnt).Controls(2).Controls(1), TextBox).Text) & "','"
                            strSQl &= cmbPaydate.SelectedValue & "','"
                            strSQl &= TxtRemarks.Text & "')"
                            Session("DalObj").ExecuteCommand(strSQl, Tran)
                        End If
                    End If
                Next
            End If
            Tran.Commit()
            btnNew_Click(sender, Nothing)
            SetMsg(LblErrMsg, "Records Saved Sucessfully.")
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (CmdSave_Click)")
            Tran.Rollback()
        End Try
    End Sub

    Private Sub cmbPaydate_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPaydate.SelectedIndexChanged
        LblErrMsg.Text = ""
        BindgrdAdjust()
        DisplayRemarks()
    End Sub
    Sub DisplayRemarks()
        Try
            Dim StrSQl As String, dtRem As New DataTable
            StrSQl = "select Remarks from AdjTran Where emp_code='" & Chk(TxtCode.Text) & "' and Paydate='" & cmbPaydate.SelectedValue & "'"
            Session("DalObj").GetSqlDataTable(dtRem, StrSQl)
            If dtRem.Rows.Count > 0 Then
                'comment by Ravi
                'TxtRemarks.Text = Chk(dtRem.Rows(1).Item("Remarks"))
                'By Ravi on dec 2 2006
                TxtRemarks.Text = Chk(dtRem.Rows(0).Item("Remarks"))
            Else
                TxtRemarks.Text = ""
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " DisplayRemarks")
        End Try
    End Sub

    Private Sub CmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub
    Sub DisplayDesc(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim StrSQl As String, dtDesc As New DataTable
            Dim Item As DataGridItem
            Dim FldName As String

            Item = CType(sender, Control).NamingContainer()
            FldName = Chk(CType(Item.Cells(0).Controls(1), DropDownList).SelectedItem.Text)
            If FldName <> "" Then
                StrSQl = "select Field_Desc from Paysetup Where Field_Name='" & FldName & "'"
                Session("DalObj").GetSqlDataTable(dtDesc, StrSQl)
                If dtDesc.Rows.Count > 0 Then
                    CType(Item.Cells(1).Controls(1), TextBox).Text = dtDesc.Rows(0).Item(0)
                End If
            Else
                SetMsg(LblErrMsg, "Please Select the Field Name")
                CType(Item.Cells(1).Controls(1), TextBox).Text = ""
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " DisplayDesc")
        End Try
    End Sub
    Function IsValidate() As Boolean
        Dim Code As Int16
        'If Chk(TxtCode.Text) = "" Then
        '    SetMsg(LblErrMsg, "Employee Code Can't be Left Blank.")
        '    Return False
        'End If
        'If cmbPaydate.SelectedValue = "" Then
        '    SetMsg(LblErrMsg, "Month Can't be Left Blank.")
        '    Return False
        'End If
        Return True
    End Function
End Class
