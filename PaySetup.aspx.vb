
Partial Class PaySetup

#Region " PaySetup "
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ds As New DataSet
    Dim DAL As DAL.DataLayer
    Dim BAL As BAL.BLayer
    Dim StrSql As String
    Dim cnSEMS As DataTable
    Protected WithEvents TextBox1 As System.Web.UI.WebControls.TextBox

#End Region

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Tetcategory As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label

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
        DAL = Session("DALObj")
        BAL = Session("BALObj")
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
                CmdDelete.Visible = bSuccess
                '-----------------------------------------------------------------
                ' FillDataGrid()  Comment by Ravi
                FillDataGrid("SNO") 'By Ravi on 2 dec 06
                ViewState("Action") = "ADDNEW"
            End If
            CmdDelete.Attributes.Add("onclick", "return DeleteRecord();")
            CmdSave.Attributes.Add("onclick", "return Valedate();")
            TxtDecimal.Text = 0
        Catch ex As Exception
            LblErrMsg.Text = ex.Message & "Page Load"
        End Try
    End Sub
    Private Function FillDataGrid(ByVal sno)
        Try
            Dim dtTable As New DataTable
            Dim StrSql As String
            Dim str As String
            Dim Cnt As Integer
            'Comment by Ravi
            'StrSql = "select field_name, FIELD_DESC, SNo, (CASE Fld_Categ  WHEN '8' THEN 'Others' WHEN '1' THEN 'Earnings' WHEN '2' THEN 'Deductions' WHEN '3' THEN 'Loan & Advances' WHEN '4' THEN 'Reimbursments' WHEN '5' THEN 'Perquisities' WHEN '6' THEN 'Investments' WHEN '7' THEN 'Taxable' END) as Fld_Categ from PaySetUp order By SNO"
            'By Ravi
            StrSql = "select field_name, FIELD_DESC, SNo, (CASE Fld_Categ  WHEN '8' THEN 'Others' WHEN '1' THEN 'Earnings' WHEN '2' THEN 'Deductions' WHEN '3' THEN 'Loan & Advances' WHEN '4' THEN 'Reimbursments' WHEN '5' THEN 'Perquisities' WHEN '6' THEN 'Investments' WHEN '7' THEN 'Taxable' END) as Fld_Categ from PaySetUp order By " & sno
            DAL.GetSqlDataTable(dtTable, StrSql)
            If dtTable.Rows.Count > 0 Then
                GrdSetUp.DataSource = dtTable
                GrdSetUp.DataBind()
            End If
        Catch ex As Exception
            LblErrMsg.Text = ex.Message
        End Try
    End Function
    Private Sub GrdSetUp_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles GrdSetUp.PageIndexChanged
        GrdSetUp.CurrentPageIndex = e.NewPageIndex
        'FillDataGrid() comment by Ravi
        FillDataGrid("SNO")  'by Ravi on 2 dec 06
    End Sub
    Private Sub CmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Private Sub GrdSetUp_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles GrdSetUp.ItemCommand

        Try
            If e.CommandName = "Edit" Then
                Dim DtData As New DataTable
                Dim Temp, temp1 As String
                Dim strSQl As String
                '  CmdSaveAs.Enabled = True
                Dim btnCol As Control
                LblErrMsg.Text = ""
                ViewState("Temp") = Chk(e.Item.Cells(4).Text)
                strSQl = "SELECT * FROM PAYSETUP Where Field_Name='" & ViewState("Temp") & "'"
                DAL.GetSqlDataTable(DtData, strSQl)

                If DtData.Rows.Count > 0 Then
                    txtSNO.Text = ChkN(DtData.Rows(0).Item("SNO"))
                    TxtFldName.Text = Chk(DtData.Rows(0).Item("FIELD_NAME"))
                    TxtPrintName.Text = Chk(DtData.Rows(0).Item("PRINT_NAME"))
                    CmbCategory.SelectedValue = ChkN(DtData.Rows(0).Item("FLD_CATEG"))
                    TxtDescription.Text = Chk(DtData.Rows(0).Item("FIELD_DESC"))
                    CmbRated.SelectedValue = ChkN(DtData.Rows(0).Item("FLD_RATED"))
                    CmbFieldType.SelectedValue = Chk(DtData.Rows(0).Item("FIELD_TYPE"))
                    TxtWidth.Text = ChkN(DtData.Rows(0).Item("FIELD_LEN"))
                    TxtDecimal.Text = ChkN(DtData.Rows(0).Item("FIELD_DEC"))
                    CmbStartingMonth.SelectedValue = ChkN(DtData.Rows(0).Item("FLD_MONTH"))

                    If Chk(DtData.Rows(0).Item("FLD_HRDMAST")) = "Y" Then
                        ChkHrdMast.Checked = True
                    Else
                        ChkHrdMast.Checked = False
                    End If

                    If Chk(DtData.Rows(0).Item("FLD_PAYMAST")) = "Y" Then
                        ChkPayMast.Checked = True
                    Else
                        ChkPayMast.Checked = False
                    End If

                    If Chk(DtData.Rows(0).Item("FLD_HRDHIST")) = "Y" Then
                        ChkHrdHist.Checked = True
                    Else
                        ChkHrdHist.Checked = False
                    End If

                    If Chk(DtData.Rows(0).Item("FLD_PAYHIST")) = "Y" Then
                        ChkPayHist.Checked = True
                    Else
                        ChkPayHist.Checked = False
                    End If

                    If Chk(DtData.Rows(0).Item("FLD_INCREMENT")) = "Y" Then
                        ChkIncrement.Checked = True
                    Else
                        ChkIncrement.Checked = False
                    End If

                    If Chk(DtData.Rows(0).Item("FLD_DEFAULT")) = "Y" Then
                        ChkDefField.Checked = True
                    Else
                        ChkDefField.Checked = False
                    End If
                    ViewState("Action") = "MODIFY"
                End If
            End If

        Catch ex As Exception
            LblErrMsg.Text = ex.Message
        End Try

    End Sub
    Private Function SaveData()
        Try
            Dim strQuert, SrNo As String
            Dim StrQuery As String

            Dim oSCM As SCM.Payroll

            Dim dtTmp As New DataTable
            Dim i As Int16
            If ChkN(txtSNO.Text) = 0 Then
                SrNo = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand("select Max(SNO) from PaysetUp", , DAL.ExecutionType.ExecuteScalar)
                SrNo = SrNo + 1
            Else
                SrNo = ChkN(txtSNO.Text) - 1
            End If
            If ViewState("Action") = "ADDNEW" Then
                StrQuery = "insert into PAYSETUP(SNO,FIELD_NAME,PRINT_NAME,FLD_CATEG,FIELD_DESC,FLD_RATED,FIELD_TYPE,FIELD_LEN,FIELD_DEC,FLD_MONTH,FLD_HRDMAST,FLD_PAYMAST,FLD_HRDHIST,FLD_PAYHIST,FLD_INCREMENT,FLD_DEFAULT)"
                StrQuery &= " Values('"
                StrQuery &= Chk(SrNo) & "','"
                StrQuery &= Chk(TxtFldName.Text) & "','"
                StrQuery &= Chk(TxtPrintName.Text) & "','"
                StrQuery &= ChkN(CmbCategory.SelectedValue) & "','"
                StrQuery &= Chk(TxtDescription.Text) & "','"
                StrQuery &= ChkN(CmbRated.SelectedValue) & "','"
                StrQuery &= Chk(CmbFieldType.SelectedValue) & "','"
                StrQuery &= ChkN(TxtWidth.Text) & "','"
                StrQuery &= ChkN(TxtDecimal.Text) & "','"
                StrQuery &= ChkN(CmbStartingMonth.SelectedValue) & "',"
                StrQuery &= IIf(ChkHrdMast.Checked, "'Y',", "'N',")
                StrQuery &= IIf(ChkPayMast.Checked, "'Y',", "'N',")
                StrQuery &= IIf(ChkHrdHist.Checked, "'Y',", "'N',")
                StrQuery &= IIf(ChkPayHist.Checked, "'Y',", "'N',")
                StrQuery &= IIf(ChkIncrement.Checked, "'Y',", "'N',")
                StrQuery &= IIf(ChkDefField.Checked, "'Y',", "'N')")
                DAL.ExecuteCommand(StrQuery)
            ElseIf ViewState("Action") = "MODIFY" Then
                StrQuery = "Update PaySetUp Set " & _
                    " FIELD_NAME  = '" & Chk(TxtFldName.Text) & "'," & _
                    " PRINT_NAME = '" & Chk(TxtPrintName.Text) & "'," & _
                    " FLD_CATEG = '" & ChkN(CmbCategory.SelectedValue) & "'," & _
                    " FIELD_DESC= '" & Chk(TxtDescription.Text) & "'," & _
                    " FLD_RATED = '" & ChkN(CmbRated.SelectedValue) & "'," & _
                    " FIELD_TYPE = '" & Chk(CmbFieldType.SelectedValue) & "'," & _
                    " FIELD_LEN = '" & ChkN(TxtWidth.Text) & "'," & _
                    " FIELD_DEC = '" & ChkN(TxtDecimal.Text) & "'," & _
                    " FLD_MONTH  = '" & ChkN(CmbStartingMonth.SelectedValue) & "'," & _
                    " FLD_HRDMAST = " & IIf(ChkHrdMast.Checked, "'Y', ", "'N', ") & _
                    " FLD_PAYMAST = " & IIf(ChkPayMast.Checked, "'Y', ", "'N', ") & _
                    " FLD_HRDHIST = " & IIf(ChkHrdHist.Checked, "'Y', ", "'N', ") & _
                    " FLD_PAYHIST= " & IIf(ChkPayHist.Checked, "'Y', ", "'N', ") & _
                    " FLD_INCREMENT = " & IIf(ChkIncrement.Checked, "'Y', ", "'N', ") & _
                    " FLD_DEFAULT = " & IIf(ChkDefField.Checked, "'Y' ", "'N' ") & _
                    " Where Field_Name = '" & Chk(ViewState("Temp")) & "'"
                DAL.ExecuteCommand(StrQuery)
            End If
            oSCM = GetSCMPayroll(DAL, LblErrMsg, Session("LoginUser"))
            If ChkHrdMast.Checked Then
                oSCM.AddField("HRDMAST", Chk(TxtFldName.Text), Chk(CmbFieldType.SelectedValue), ChkN(TxtWidth.Text), ChkN(TxtDecimal.Text))
            End If
            If ChkHrdHist.Checked Then
                oSCM.AddField("HRDHIST", Chk(TxtFldName.Text), Chk(CmbFieldType.SelectedValue), ChkN(TxtWidth.Text), ChkN(TxtDecimal.Text))
            End If
            If ChkPayMast.Checked Then
                oSCM.AddField("PAYMAST", Chk(TxtFldName.Text), Chk(CmbFieldType.SelectedValue), ChkN(TxtWidth.Text), ChkN(TxtDecimal.Text))
            End If
            If ChkPayHist.Checked Then
                oSCM.AddField("PAYHIST", Chk(TxtFldName.Text), Chk(CmbFieldType.SelectedValue), ChkN(TxtWidth.Text), ChkN(TxtDecimal.Text))
            End If
            oSCM.AddField("PAYCAL", Chk(TxtFldName.Text), Chk(CmbFieldType.SelectedValue), ChkN(TxtWidth.Text), ChkN(TxtDecimal.Text))
            oSCM = Nothing
            DAL.GetSqlDataTable(dtTmp, "SP_HELPTEXT HRDMASTQRY")
            For i = 0 To dtTmp.Rows.Count - 1
                StrSql = StrSql & Chk(dtTmp.Rows(i).Item(0))
            Next
            DAL.ExecuteCommand(Replace(StrSql, "CREATE", "ALTER"))
            StrSql = ""
            dtTmp = Nothing
            dtTmp = New DataTable
            DAL.GetSqlDataTable(dtTmp, "SP_HELPTEXT HRDHISTQRY")
            For i = 0 To dtTmp.Rows.Count - 1
                StrSql = StrSql & Chk(dtTmp.Rows(i).Item(0))
            Next
            DAL.ExecuteCommand(Replace(StrSql, "CREATE", "ALTER"))
            ClearAll(Me)
            'FillDataGrid() 'comment by Ravi 
            FillDataGrid("SNO")   'By Ravi on 2 dec 06
            LblErrMsg.Text = "Record Saved Sucessfully."
            ViewState("Action") = "ADDNEW"
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & "SaveData()")

        End Try
    End Function

    'Private Sub GrdSetUp_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles GrdSetUp.Init
    '    Try
    '        Dim cmdSelect As New ButtonColumn
    '        cmdSelect.ButtonType = ButtonColumnType.LinkButton
    '        cmdSelect.Text = "Select"
    '        cmdSelect.HeaderText = "Select"
    '        cmdSelect.CommandName = "Edit"
    '        GrdSetUp.Columns.Add(cmdSelect)
    '    Catch ex As Exception
    '        LblErrMsg.Text = ex.Message & " " & ex.Source
    '    End Try
    'End Sub

    Private Sub CmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdSave.Click
        If Not isValidate() Then Exit Sub
        SaveData()
    End Sub
    Private Function DeleteReocrds()
        Try
            Dim DelQuery = "Delete From PaySetUp Where Field_Name='" & ViewState("Temp") & "'"
            DAL.ExecuteCommand(DelQuery)
            'FillDataGrid() commeny by Ravi 
            FillDataGrid("SNO")  'By Ravi on 2 dec o6
            LblErrMsg.Text = "Record Has been Deleted Successfully."
        Catch ex As Exception
            LblErrMsg.Text = ex.Message
        End Try
    End Function

    Private Sub CmdDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdDelete.Click
        DeleteReocrds()
        CmdNew_Click(sender, Nothing)
    End Sub

    Private Sub CmdNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdNew.Click
        Dim SNo As Int16
        SNo = ChkN(txtSNO.Text)
        'comment by Ravi
        'ClearAll(Me)
        'ViewState("Action") = "ADDNEW"
        'txtSNO.Text = SNo
        'CmbFieldType.SelectedValue = "N"
        'CmbStartingMonth.SelectedValue = 3
        'CmbRated.SelectedValue = 4
        'CmbCategory.SelectedValue = 8
        'TxtDecimal.Text = 0

        'By Ravi On 2dec 06
        txtSNO.Text = SNo
        ViewState("Action") = "ADDNEW"
        TxtFldName.Text = ""
        TxtDescription.Text = ""
        TxtPrintName.Text = ""


    End Sub
    Private Function isValidate() As Boolean
        Try

            If TxtFldName.Text = "" Then
                isValidate = False
                SetMsg(LblErrMsg, "Please Enter Field Name.")
                Exit Function
            End If

            If TxtPrintName.Text = "" Then
                isValidate = False
                SetMsg(LblErrMsg, "Printable Name Can't be Left Blank.")
                Exit Function
            End If

            If TxtDescription.Text = "" Then
                isValidate = False
                SetMsg(LblErrMsg, "Description Can't be Left Blank.")
                Exit Function
            End If

            If Not IsNumeric(TxtWidth.Text) Then
                isValidate = False
                SetMsg(LblErrMsg, "Width Must be Numeric.")
                Exit Function
            End If
            If Not IsNumeric(TxtDecimal.Text) Then
                isValidate = False
                SetMsg(LblErrMsg, "Decimal Must be Numeric.")
                Exit Function
            End If
            isValidate = True
        Catch ex As Exception
            isValidate = False
        End Try
    End Function

    Private Sub cmdUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUp.Click
        Dim SNOc As Int16
        Try
            SNOc = ChkN(txtSNO.Text) - 1
            If SNOc = 0 Then Exit Sub
            StrSql = "UPDATE PAYSETUP SET SNO=" & ChkN(txtSNO.Text) & " WHERE SNO=" & ChkN(SNOc)
            DAL.ExecuteCommand(StrSql)
            StrSql = "UPDATE PAYSETUP SET SNO=" & ChkN(SNOc) & " WHERE FIELD_NAME='" & Chk(ViewState("Temp")) & "'"
            DAL.ExecuteCommand(StrSql)
            txtSNO.Text = SNOc
            'FillDataGrid() 'comment by Ravi
            FillDataGrid("SNO")  'By Ravi on 2 dec 06
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message)
        End Try
    End Sub

    Private Sub cmdDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDown.Click
        Dim SNOc As Int16
        Try
            SNOc = ChkN(txtSNO.Text) + 1
            If SNOc > DAL.ExecuteCommand("SELECT MAX(SNO) FROM PAYSETUP", 0, 2) Then Exit Sub
            StrSql = "UPDATE PAYSETUP SET SNO=" & ChkN(txtSNO.Text) & " WHERE SNO=" & ChkN(SNOc)
            DAL.ExecuteCommand(StrSql)
            StrSql = "UPDATE PAYSETUP SET SNO=" & ChkN(SNOc) & " WHERE FIELD_NAME='" & Chk(ViewState("Temp")) & "'"
            DAL.ExecuteCommand(StrSql)
            txtSNO.Text = SNOc
            'FillDataGrid() comment by Ravi 
            FillDataGrid("SNO")  'by Ravi on 02 dec 06
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message)
        End Try
    End Sub
    Private Sub CmdSaveAs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & "CmdSaveAs_Click()")
        End Try
    End Sub
    'By Ravi on 02 Dec 06
    Protected Sub Head_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim StrId As String = CType(sender, LinkButton).ID
            StrId = Replace(StrId, "Lnk", "")
            FillDataGrid(StrId)
        Catch EX As Exception
            SetMsg(LblErrMsg, EX.Message & "Head_Click()")
            Exit Sub
        End Try
    End Sub
End Class
