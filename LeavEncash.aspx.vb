Partial Class LeavEncash
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
        Try
            'comment by Ravi
            'If CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules <> "" Then
            '    If InStr(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, SrNo & "-") <> 0 Then
            '        Dim int As Int16, st As String
            '        int = InStr(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, SrNo)
            '        st = Right(Mid(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, int, Len(SrNo & "-") + 1), 1)

            '        If st = "S" Then
            '        Else
            '            cmdSave.Visible = False
            '        End If
            '    Else
            '        Response.Redirect("Main.aspx")
            '    End If
            'End If

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
                cmdSave.Visible = bSuccess
                '-----------------------------------------------------------------

                Session("BalObj").fillcombo(cmbEmp, "Select Emp_Code, Emp_Name as FNAME from HrdMastQry where Ltype=1  " & Session("UserCodes") & " Order by FNAME", True)
                Session("BalObj").fillcombo(cmbLeaveType, "Select LvType, LVDESC From LVTYPE")
                Session("BalObj").fillcombo(cmbTransferTo, "Select LvType, LVDESC From LVTYPE")
            End If

            cmdSave.Attributes.Add("onclick", "return ValidateCtrl();")
        Catch ex As Exception
            lblMsg.Text = ex.Message & "Page Load"

        End Try
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub
#Region "    Save Records   "

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim Tran As SqlClient.SqlTransaction
        Try
            Dim StrQuery As String, Code As Long

            If Not IsValidate() Then Exit Sub

            Tran = Session("DalObj").StartTransaction("Save")

            Code = Session("BalObj").GetNextNumber("LVENCASH", "RECID")

            StrQuery = " Insert LVENCASH (RECID, LVYEAR,EMP_CODE,LVTYPE,LVDAYS,REMARKS,TRF_ENCASH,TRF_LVTYPE ) Values (" & _
                        Code & ", '" & _
                        Session("LeavYear") & "', '" & _
                        Chk(TxtCode.Text) & "', '" & _
                        Chk(cmbLeaveType.SelectedValue) & "', '" & _
                        ChkN(TxtDays.Value) & "', '" & _
                        Chk(TxtRemarks.Text) & "', '"

            If RdoEncash.Checked Then
                StrQuery = StrQuery & "E', '' )"

                Session("DalObj").ExecuteCommand(StrQuery, Tran)

                StrQuery = " Update LEAVMAST Set TRAN_OUT = isnull(TRAN_OUT,0) + " & ChkN(TxtDays.Value) & " Where  " & _
                           " LEVYEAR = '" & Session("LeavYear") & "' and LVTYPE = '" & Chk(cmbLeaveType.SelectedValue) & "' " & _
                           " and EMP_CODE = '" & Chk(TxtCode.Text) & "'"

                Session("DalObj").ExecuteCommand(StrQuery, Tran)
            Else


                StrQuery = StrQuery & "T" & "', '" & Chk(cmbTransferTo.SelectedValue) & "' )"
                Session("DalObj").ExecuteCommand(StrQuery, Tran)

                StrQuery = " Update LEAVMAST Set TRAN_OUT = isnull(TRAN_OUT,0) + " & ChkN(TxtDays.Value) & " Where  " & _
                           " LEVYEAR = '" & Session("LeavYear") & "' and LVTYPE = '" & Chk(cmbLeaveType.SelectedValue) & "' " & _
                           " and EMP_CODE = '" & Chk(TxtCode.Text) & "'"

                Session("DalObj").ExecuteCommand(StrQuery, Tran)

                StrQuery = " Update LEAVMAST Set TRAN_IN = isnull(TRAN_IN,0) + " & ChkN(TxtDays.Value) & " Where  " & _
                           " LEVYEAR = '" & Session("LeavYear") & "' and LVTYPE = '" & Chk(cmbTransferTo.SelectedValue) & "' " & _
                           " and EMP_CODE = '" & Chk(TxtCode.Text) & "'"

                Session("DalObj").ExecuteCommand(StrQuery, Tran)
            End If
            Tran.Commit()
            BlankRecords()
            SetMsg(lblMsg, "Record Saved Successfully.")
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : Save Records ")
            Tran.Rollback()
        Finally
            If Not IsNothing(Tran) Then Tran.Dispose()
        End Try
    End Sub


    Function IsValidate() As Boolean
        Try

            Dim Code As Double, SqlQry As String

            'If Chk(TxtCode.Text) = "" Then
            '    SetMsg(lblMsg, " Employee Code Can not be left blank.")
            '    Return False
            'End If
            'If Chk(TxtDays.Value) = "" Then
            '    SetMsg(lblMsg, " Days can not be left blank.")
            '    Return False
            'End If

            Code = Session("DalObj").ExecuteCommand("Select Count(*) From LeavMast Where EMP_CODE = '" & Chk(TxtCode.Text) & "' AND LVTYPE='" & Chk(cmbLeaveType.SelectedValue) & "' AND LEVYEAR = '" & Session("LeavYear") & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)

            If Code = 0 Then
                SetMsg(lblMsg, " Leave Type doesn't exist for this employee.")
                Return False
            End If


            If RdoTransfer.Checked = True Then

                Code = Session("DalObj").ExecuteCommand("Select Count(*) From LeavMast Where EMP_CODE = '" & Chk(TxtCode.Text) & "' AND LVTYPE='" & Chk(cmbTransferTo.SelectedValue) & "' AND LEVYEAR = '" & Session("LeavYear") & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)

                If Code = 0 Then
                    SetMsg(lblMsg, " Leave Type '" & Chk(cmbTransferTo.SelectedItem.Text) & "'  doesn't exist for this employee.")
                    Return False
                End If

            End If

            SqlQry = " Select ((isnull(OPENING,0)+isnull(EARNED,0)+isnull(Tran_In,0)) - " & _
                   " (isnull(AVAILED,0)+isnull(Tran_Out,0))) as Balance From LEAVMAST WHERE EMP_CODE = '" & Chk(TxtCode.Text) & "' " & _
                   " AND LEVYEAR = '" & Session("LeavYear") & "' AND LVTYPE ='" & Chk(cmbLeaveType.SelectedValue) & "'"

            Code = Session("DalObj").ExecuteCommand(SqlQry, , DAL.DataLayer.ExecutionType.ExecuteScalar)

            If Code < ChkN(TxtDays.Value) Then
                SetMsg(lblMsg, "Balance is not Sufficient in this Leave Type.")
                Return False
            End If
            Return True
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : Validate Records")
        End Try
    End Function

#End Region

    Private Sub btnList_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnList.Click
        btnList.Visible = False
        TxtCode.Visible = False
        cmbEmp.Visible = True
    End Sub

#Region "   Employee Code Changed  "

    Private Sub cmbEmp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbEmp.SelectedIndexChanged
        Try
            If cmbEmp.SelectedValue = "" Then Exit Sub
            btnList.Visible = True
            TxtCode.Visible = True
            cmbEmp.Visible = False
            TxtCode.Text = cmbEmp.SelectedValue
            TxtCode_TextChanged(sender, e)
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : Employee Changed ()")
        End Try
    End Sub

    Private Sub TxtCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtCode.TextChanged
        Try
            ChkCombo(cmbEmp, Chk(TxtCode.Text))
            'LblName.Visible = True

            If cmbEmp.SelectedItem.Text <> "" Then
                LblName.Text = cmbEmp.SelectedItem.Text
            Else
                LblName.Text = ""
            End If


            If LblName.Text = "" Then
                LblName.Visible = False
            Else
                LblName.Visible = True
            End If
            lblMsg.Text = ""
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : TextChanged ()")
        End Try
    End Sub

#End Region
    Sub BlankRecords()
        TxtCode.Text = ""
        cmbEmp.SelectedIndex = cmbEmp.Items.Count - 1
        TxtDays.Value = ""
        TxtRemarks.Text = ""
        RdoEncash.Checked = True
    End Sub

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub

End Class
