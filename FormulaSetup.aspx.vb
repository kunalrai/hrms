
Imports System.Web.UI.Page

Partial Class FormulaSetup
    Inherits System.Web.UI.Page
    Dim Dt As New DataTable
    Dim DAL As DAL.DataLayer
    Dim BAL As BAL.BLayer
    Protected WithEvents LblTotal As System.Web.UI.WebControls.Label

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents LblDesc As System.Web.UI.WebControls.Label
    Protected WithEvents LblEntrydate As System.Web.UI.WebControls.Label
    Protected WithEvents LblEffdate As System.Web.UI.WebControls.Label
    Protected WithEvents RbtAaitis As System.Web.UI.WebControls.RadioButton
    Protected WithEvents LblFormula As System.Web.UI.WebControls.Label
    Protected WithEvents CmdExpression As System.Web.UI.WebControls.Button
    Protected WithEvents LblCond As System.Web.UI.WebControls.Label
    Protected WithEvents CmdExpress As System.Web.UI.WebControls.Button
    Protected WithEvents CmdNew As System.Web.UI.WebControls.Button
    Protected WithEvents LblRounoff As System.Web.UI.WebControls.Label

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
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If
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


                FillDataGrid()
                BAL.FillCombo(CmbDescription, "Field_Name", "Field_Name", "PaySetup", True)
                DtpEffectiveDate.Text = Format(Date.Today, "dd/MMM/yyyy")
                DtpEntryDate.Text = Format(Date.Today, "dd/MMM/yyyy")
            End If
            CmdDelete.Attributes.Add("onclick", "return ConfirmDelete();")
        Catch ex As Exception
            LblErrMsg.Text = ex.Message & "Page Load"
        End Try

    End Sub
    Private Function FillDataGrid()
        Try
            Dim DtData As New DataTable
            Dim StrQuery As String
            StrQuery = "Select distinct formula.field_name,(datename(d,formula.Fld_Date)+'/'+ left(datename(MM,formula.Fld_Date),3)+'/'+ datename(yyyy,Formula.Fld_Date)) as Fld_Date,formula.FLD_WEF, paysetup.Field_Desc from formula formula inner join paysetup paysetup on formula.field_name=Paysetup.Field_Name order by formula.field_name"
            DAL.GetSqlDataTable(DtData, StrQuery)
            If DtData.Rows.Count > 0 Then
                GrdFormula.DataSource = DtData
                GrdFormula.DataBind()
            End If
        Catch ex As Exception
            LblErrMsg.Text = ex.Message
        End Try
    End Function
    Private Sub GrdFormula_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles GrdFormula.ItemCommand
        If e.CommandName = "Select" Then
            LblErrMsg.Text = ""
            Try
                If GrdFormula.Items.Count > 0 Then
                    ViewState("Temp1") = e.CommandSource.Text
                    ViewState("Temp2") = e.Item.Cells(1).Text
                    ShowDetails(ViewState("Temp1"), ViewState("Temp2"))
                End If
            Catch ex As Exception
                LblErrMsg.Text = ex.Message
            End Try
        End If
    End Sub
    Private Sub ShowDetails(ByVal Field_Name As String, ByVal Fld_Date As Date)
        Dim SearchQry As String
        Dim DttData As New DataTable
        SearchQry = "Select F.Field_Name,F.Fld_Date,F.Fld_WEF,F.Fld_Rnd,F.Fld_Rnd_M,F.Fld_Calc,F.Fld_Valid,P.Field_Desc,P.bSysFormula From Formula F inner Join PaySetup p on F.Field_Name=P.Field_Name where F.Field_Name='" & Field_Name & "' And F.Fld_Date='" & Format(Fld_Date, "dd/MMM/yyyy") & "'"
        DAL.GetSqlDataTable(DttData, SearchQry)
        If DttData.Rows.Count > 0 Then
            '===========Assigning The Value in TextBoxes
            TxtFieldName.Text = Chk(DttData.Rows(0).Item("Field_Name"))
            TxtDescription.Text = Chk(DttData.Rows(0).Item("Field_Desc"))
            TxtMultiple.Text = ChkN(DttData.Rows(0).Item("Fld_Rnd_M"))
            TxtFormula.Text = Chk(DttData.Rows(0).Item("Fld_Calc"))
            TxtCondition.Text = Chk(DttData.Rows(0).Item("Fld_Valid"))
            DtpEntryDate.Text = Format(CDate(DttData.Rows(0).Item("Fld_Date")), "dd/MMM/yyyy")
            DtpEffectiveDate.Text = Format(CDate(DttData.Rows(0).Item("Fld_WEF")), "dd/MMM/yyyy")
            '==========Fill Radio Button List
            If DttData.Rows(0).Item("Fld_Rnd") = "1" Then
                RblRoundOff.SelectedIndex = 0
            End If
            If DttData.Rows(0).Item("Fld_Rnd") = "2" Then
                RblRoundOff.SelectedIndex = 1
            End If
            If DttData.Rows(0).Item("Fld_Rnd") = "3" Then
                RblRoundOff.SelectedIndex = 2
            End If
            If DttData.Rows(0).Item("Fld_Rnd") = "4" Then
                RblRoundOff.SelectedIndex = 3
            End If
            If IIf(IsDBNull(DttData.Rows(0).Item("bSysFormula")), False, DttData.Rows(0).Item("bSysFormula")) Then
                CmdSave.Enabled = False
                CmdDelete.Enabled = False
            Else
                CmdSave.Enabled = True
                CmdDelete.Enabled = True
            End If
            ViewState("Action") = "EDIT"
        Else
            ViewState("Action") = "SAVE"
        End If
    End Sub
    Private Function DeleteRecord()
        Try
            Dim DelQuery As String
            Dim DeleQuery As String
            DelQuery = "Delete From Formula where Field_Name='" & TxtFieldName.Text & "' And Fld_Wef='" & DtpEffectiveDate.Text & "'"
            DAL.ExecuteCommand(DelQuery)
            LblErrMsg.Text = "Your Record Has been Deleted"
        Catch ex As Exception
            LblErrMsg.Text = ex.Message
        End Try
    End Function

    Private Sub CmdDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdDelete.Click
        Try
            If Chk(TxtFieldName.Text) = "" Then
                SetMsg(LblErrMsg, "Please Select The Formula From the List.")
                Exit Sub
            Else
                DeleteRecord()
                Clear()
                FillDataGrid()
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & "CmdDelete_Click")
        End Try

    End Sub
    Private Function SaveRecord()
        Dim Tran As SqlClient.SqlTransaction
        Try
            Dim StrQuery, UpdQuery As String
            Tran = DAL.StartTransaction("Save")
            If ViewState("Action") = "SAVE" Then
                StrQuery = "insert into Formula(Field_Name,Fld_Date,Fld_WEF,Fld_Calc,Fld_Valid,Fld_Rnd,Fld_Rnd_M)"
                StrQuery &= " Values('"
                StrQuery &= Chk(TxtFieldName.Text) & "','"
                StrQuery &= Chk(DtpEntryDate.Text) & "','"
                StrQuery &= Chk(DtpEffectiveDate.Text) & "','"
                StrQuery &= Chk(TxtFormula.Text) & "','"
                StrQuery &= Chk(TxtCondition.Text) & "','"
                StrQuery &= ChkN(RblRoundOff.SelectedValue) & "','"
                StrQuery &= ChkN(TxtMultiple.Text) & "')"
                DAL.ExecuteCommand(StrQuery, Tran)
            Else
                UpdQuery = " Update Formula Set " & _
                " Fld_WEF = '" & Chk(DtpEffectiveDate.Text) & "', " & _
                " Fld_Calc = '" & Chk(TxtFormula.Text) & "', " & _
                " Fld_Valid = '" & Chk(TxtCondition.Text) & "', " & _
                " Fld_Rnd = '" & ChkN(RblRoundOff.SelectedValue) & "', " & _
                " Fld_Rnd_M = '" & ChkN(TxtMultiple.Text) & "'" & _
                " where Field_Name = '" & Chk(TxtFieldName.Text) & "' and Fld_Date = '" & Chk(DtpEntryDate.Text) & "'"
                DAL.ExecuteCommand(UpdQuery, Tran)
            End If
            Tran.Commit()
            SetMsg(LblErrMsg, "Records Saved Sucessfully.")
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (CmdSave_Click)")
            Tran.Rollback()
        Finally
            If Not IsNothing(Tran) Then Tran.Dispose()
        End Try
        ClearAll(Me)
        DtpEffectiveDate.Text = Format(Date.Today, "dd/MMM/yyyy")
        DtpEntryDate.Text = Format(Date.Today, "dd/MMM/yyyy")
    End Function

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNew.Click
        CmbDescription.Visible = True
        TxtFieldName.Visible = False
        'btnList.Visible = False
        btnNew.Visible = False
        LblErrMsg.Text = ""
        Clear()
    End Sub
    Private Function ShowDescription()
        Try
            Dim Search As String
            Dim DtTable As New DataTable
            Search = "Select distinct Field_Desc from paySetup Where Field_Name='" & TxtFieldName.Text & "'"
            DAL.GetSqlDataTable(DtTable, Search)
            If DtTable.Rows.Count > 0 Then
                TxtDescription.Text = Chk(DtTable.Rows(0).Item("Field_Desc"))
            End If
        Catch ex As Exception
            LblErrMsg.Text = ex.Message
        End Try
    End Function

    Private Sub CmbDescription_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbDescription.SelectedIndexChanged
        Try
            If CmbDescription.SelectedIndex <> CmbDescription.Items.Count - 1 Then
                TxtFieldName.Visible = True
                btnNew.Visible = True
                CmbDescription.Visible = False
                TxtFieldName.ToolTip = CmbDescription.SelectedValue
                TxtFieldName.Text = CmbDescription.SelectedItem.Text
                ViewState("ReqCode") = TxtFieldName.Text
                ViewState("Action") = "SAVE"
                ShowDescription()
                If Trim(DtpEffectiveDate.Text) <> "" Then
                    If IsDate(Trim(DtpEffectiveDate.Text)) Then
                        ShowDetails(TxtFieldName.Text, CType(Trim(DtpEffectiveDate.Text), Date))
                    End If
                End If
            End If
        Catch ex As Exception
            LblErrMsg.Text = ex.Message
        End Try
    End Sub
    Private Function Clear()
        DtpEffectiveDate.Text = Format(Date.Today, "dd/MMM/yyyy")
        DtpEntryDate.Text = Format(Date.Today, "dd/MMM/yyyy")
        TxtFormula.Text = ""
        TxtFieldName.Text = ""
        TxtCondition.Text = ""
        TxtMultiple.Text = ""
        TxtDescription.Text = ""
        RblRoundOff.SelectedIndex = 0
    End Function

    Private Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdSave.Click
        Try
            If Chk(TxtFieldName.Text) = "" Then
                SetMsg(LblErrMsg, "First Enter The Formula Name.")
                Exit Sub
            End If
            If Chk(DtpEffectiveDate.Text) = "" Then
                SetMsg(LblErrMsg, "Please Enter Effective Date.")
                Exit Sub
            End If
            If Chk(DtpEntryDate.Text) = "" Then
                SetMsg(LblErrMsg, "Please Enter Entry Date.")
                Exit Sub
            End If
            SaveRecord()
            FillDataGrid()
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & "CmdSave_Click")
        End Try
    End Sub

    Private Sub CmdOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdOpen.Click
        Try
            Response.Redirect("Main.aspx")
        Catch ex As Exception
            LblErrMsg.Text = ex.Message
        End Try
    End Sub

    Private Sub DtpEntryDate_DisplayDates(ByVal Sender As Object, ByVal e As System.EventArgs) Handles DtpEntryDate.DisplayDates
        If Trim(DtpEntryDate.Text) <> "" And Trim(DtpEntryDate.Text) <> "" Then
            If IsDate(Trim(DtpEntryDate.Text)) Then
                ShowDetails(TxtFieldName.Text, CType(Trim(DtpEntryDate.Text), Date))
            End If
        End If
    End Sub
End Class
