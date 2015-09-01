Imports DITWebLibrary

Partial Class GenMast
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents Table1 As System.Web.UI.HtmlControls.HtmlTable


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim DtDisplay As New DataTable
    Dim DtBind As New DataTable
    Dim dal1 As New DAL.DataLayer
    Dim BAL As BAL.BLayer
    Dim i, Length As Int16
    Dim TblName, KeyField As String
    Dim sStr, sFieldString As String
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            If IsNothing(Session("LoginUser")) Then
                Response.Redirect("CompSel.aspx")
            End If

            If Not IsPostBack Then
                'By Ravi 17 nov 2006
                Dim SrNo As Int16
                SrNo = Request.QueryString.Item("SrNo") + Request.QueryString.Item("MODULEID")
                If Not IsPostBack Then
                    Dim bSuccess As Boolean
                    Select Case CheckRight(SrNo)
                        Case MdlHRMS.AccessType.FullAccess, MdlHRMS.AccessType.SuperUser
                            bSuccess = True
                        Case MdlHRMS.AccessType.ReadonlyAccess, MdlHRMS.AccessType.Restricted
                            bSuccess = False
                    End Select
                    cmdSave.Visible = bSuccess
                End If
                '---------------------------------------------

                TGrid.Visible = False
                'cmdNew_Click(sender, e)  comment by ravi
                btnNew_Click(sender, Nothing)
            End If
            Display()
        Catch ex As Exception
            LblErrMsg.Text = ex.Message & " " & "Page_Load()"
        End Try
    End Sub

#Region "    AddNew     "
    'Comment by Ravi
    'Private Sub cmdNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNew.Click
    '    Try
    '        ClearAll(Me)
    '        Length = Session("DalObj").ExecuteCommand("Select EMP_CODE_LEN from COMPMAST", , DAL.DataLayer.ExecutionType.ExecuteScalar)
    '        Dim x, y As Int16, OutPut As String
    '        OutPut = Session("BALObj").GetNextNumber(TblName, KeyField)
    '        y = Len(OutPut)
    '        If y <> Length Then
    '            For x = y To Length
    '                OutPut = "0" & OutPut
    '                x = Len(OutPut)
    '            Next
    '        End If

    '        'CType(T1.Rows(0).Controls(1).Controls(0), TextBox).Text = Chk(OutPut)
    '        For x = 0 To T1.Rows.Count - 1
    '            If TypeOf (T1.Rows(x).Controls(1).Controls(0)) Is DTP Then
    '                CType(T1.Rows(x).Controls(1).Controls(0), DTP).Text = Format(Date.Today, "dd/MMM/yyyy")
    '            End If
    '        Next
    '        TGrid.Visible = False
    '        txtAction.Text = "ADDNEW"
    '        LblErrMsg.Text = ""
    '    Catch ex As Exception
    '        LblErrMsg.Text = ex.Message & " " & ex.Source
    '    End Try
    'End Sub

#End Region

#Region "  Display Records   "

    Public Sub Display()
        Try
            TblName = CHK(Request.QueryString.Item("TblName"))
            sStr = "Select Code, TableName, ContType, DataType, MaxLength, Mandatory, FieldName,  SQLString, AliaseName, ValField, DisplayField From MasterControls Where TableName='" & TblName & "'"
            Session("DalObj").GetSqlDataTable(DtDisplay, sStr)
            'Head.Cells(1).InnerText = CHK(Request.QueryString.Item("Desc")) & " Master"
            Head.Cells(2).InnerText = CHK(Request.QueryString.Item("Desc")) & " Master"

            If DtDisplay.Rows.Count <> 0 Then
                KeyField = DtDisplay.Rows(0).Item("FieldName")
                For i = 0 To DtDisplay.Rows.Count - 1
                    Dim R1 As New HtmlTableRow
                    R1.ID = "R" & DtDisplay.Rows(i).Item("FieldName")

                    Dim Td1 As New HtmlTableCell
                    Dim Td2 As New HtmlTableCell

                    If UCase(DtDisplay.Rows(i).Item("ContType")) = UCase("TXT") Then
                        Dim txt As New TextBox
                        txt.Width = Unit.Percentage(100)
                        txt.ID = "Txt" & i
                        txt.CssClass = "TextBox"
                        Td2.ID = "TD" & DtDisplay.Rows(i).Item("FieldName") & i + 1
                        Td2.Width = "70%"
                        Td2.Controls.Add(txt)
                    ElseIf UCase(DtDisplay.Rows(i).Item("ContType")) = UCase("CMB") Then
                        Dim Cmb As New DropDownList
                        Session("BALObj").FillCombo(Cmb, DtDisplay.Rows(i).Item("SQLString"))
                        Cmb.Items.Add("")
                        Cmb.SelectedIndex = Cmb.Items.Count - 1
                        Cmb.Width = Unit.Percentage(100)
                        Cmb.ID = "Cmb" & i
                        Td2.ID = "TD" & DtDisplay.Rows(i).Item("FieldName") & i + 1
                        Td2.Width = "70%"
                        Td2.Controls.Add(Cmb)
                    ElseIf UCase(DtDisplay.Rows(i).Item("ContType")) = UCase("LST") Then
                        Dim LST As New ListBox
                        Session("BALObj").FillCombo(LST, DtDisplay.Rows(i).Item("SQLString"))
                        LST.SelectionMode = ListSelectionMode.Multiple
                        LST.Rows = 6
                        '  LST.Width = Unit.Percentage(60)
                        LST.Width = Unit.Percentage(100)
                        LST.ID = "Lst" & i
                        Td2.ID = "TD" & DtDisplay.Rows(i).Item("FieldName") & i + 1
                        Td2.Width = "70%"
                        Td2.Controls.Add(LST)
                    ElseIf UCase(DtDisplay.Rows(i).Item("ContType")) = UCase("DTP") Then
                        Dim DateTime As New DTP
                        DateTime.Width = Unit.Pixel(125)
                        DateTime.ID = "Dtp" & i
                        DateTime.TextBoxPostBack = False
                        Td2.ID = "TD" & DtDisplay.Rows(i).Item("FieldName") & i + 1
                        Td2.Width = "70%"
                        Td2.Controls.Add(DateTime)
                    ElseIf UCase(DtDisplay.Rows(i).Item("ContType")) = UCase("LBL") Then
                        Dim LBL As New Label
                        LBL.Width = Unit.Percentage(60)
                        LBL.ID = "Lbl" & i
                        Td2.ID = "TD" & DtDisplay.Rows(i).Item("FieldName") & i + 1
                        Td2.Width = "70%"
                        Td2.Controls.Add(LBL)
                    ElseIf UCase(DtDisplay.Rows(i).Item("ContType")) = UCase("CHK") Then
                        Dim CHK As New CheckBox
                        CHK.Width = Unit.Percentage(60)
                        CHK.TextAlign = TextAlign.Left
                        CHK.ID = "Chk" & i
                        Td2.ID = "TD" & DtDisplay.Rows(i).Item("FieldName") & i + 1
                        Td2.Width = "70%"
                        Td2.Controls.Add(CHK)
                    End If

                    Td1.ID = "TD" & DtDisplay.Rows(i).Item("FieldName") & i
                    Td1.InnerText = DtDisplay.Rows(i).Item("AliaseName")
                    Td1.Width = "30%"
                    R1.Cells.Add(Td1)
                    R1.Cells.Add(Td2)
                    T1.Rows.Add(R1)
                Next
            End If

            sStr = ""
            If DtDisplay.Rows.Count <> 0 Then
                For i = 0 To DtDisplay.Rows.Count - 1
                    sStr = sStr & CHK(DtDisplay.Rows(i).Item("FieldName")) & " as '" & CHK(DtDisplay.Rows(i).Item("AliaseName")) & "', "
                    sFieldString = sFieldString & CHK(DtDisplay.Rows(i).Item("FieldName")) & ", "
                Next
            End If

            sFieldString = Mid(sFieldString, 1, Len(sFieldString.Trim) - 1)
            sStr = Mid(sStr, 1, Len(sStr.Trim) - 1)

            Session("DalObj").GetSqlDataTable(DtBind, "Select " & sStr & " from " & CHK(TblName) & "")
            'Cmment By Ravi 
            '  If DtBind.Rows.Count - 1 <> 0 Then GrdDisplay.DataSource = DtBind
            'by Ravi on 5 dec 2006
            If DtBind.Rows.Count > 0 Then GrdDisplay.DataSource = DtBind
            GrdDisplay.DataBind()
        Catch ex As Exception
            LblErrMsg.Text = ex.Message & " " & "Display()"
        End Try
    End Sub

#End Region

#Region "   Save Records    "

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            If Not IsValidate() Then
                Exit Sub
            End If
            Dim sQuery, sSearch, Qry, Find As String, i, x, y, j, k As Int16
            i = 0 : j = 0 : Qry = ""
            Dim sQry() As String

            Find = ""

            If txtAction.Text = "ADDNEW" Then

                sSearch = "Select " & DtDisplay.Rows(0).Item("FieldName") & " from " & TblName & " where " & DtDisplay.Rows(0).Item("FieldName") & " = '" & CType(T1.Rows(0).Controls(1).Controls(0), TextBox).Text.Trim & "'"
                Find = Session("DalObj").ExecuteCommand(sSearch, , DAL.DataLayer.ExecutionType.ExecuteScalar)

                If Find <> "" Then
                    SetMsg(LblErrMsg, "This Code Already Exist., Record Not Saved")
                    CType(T1.Rows(0).Controls(1).Controls(0), TextBox).Text = ""
                    TGrid.Visible = False
                    Exit Sub
                End If

                sQuery = " Insert " & TblName & " ( " & sFieldString & " ) "
                sQuery = sQuery & " values ('"

                For x = 0 To T1.Rows.Count - 1
                    If TypeOf (T1.Rows(x).Controls(1).Controls(0)) Is TextBox Then
                        sQuery = sQuery & CType(T1.Rows(x).Controls(1).Controls(0), TextBox).Text.Trim & "', '"
                    ElseIf TypeOf (T1.Rows(x).Controls(1).Controls(0)) Is DropDownList Then
                        sQuery = sQuery & CType(T1.Rows(x).Controls(1).Controls(0), DropDownList).SelectedValue & "', '"
                    ElseIf TypeOf (T1.Rows(x).Controls(1).Controls(0)) Is DTP Then
                        sQuery = sQuery & CType(T1.Rows(x).Controls(1).Controls(0), DTP).Text.Trim & "', '"
                    ElseIf TypeOf (T1.Rows(x).Controls(1).Controls(0)) Is Label Then
                        sQuery = sQuery & CType(T1.Rows(x).Controls(1).Controls(0), Label).Text.Trim & "', '"
                    ElseIf TypeOf (T1.Rows(x).Controls(1).Controls(0)) Is CheckBox Then
                        sQuery = sQuery & IIf(CType(T1.Rows(x).Controls(1).Controls(0), CheckBox).Checked = True, "1", "0") & "', '"
                    ElseIf TypeOf (T1.Rows(x).Controls(1).Controls(0)) Is ListBox Then
                        For y = 0 To CType(T1.Rows(x).Controls(1).Controls(0), ListBox).Items.Count - 1
                            If CType(T1.Rows(x).Controls(1).Controls(0), ListBox).Items(y).Selected = True Then
                                Qry = Qry & CType(T1.Rows(x).Controls(1).Controls(0), ListBox).Items(y).Value & "| "
                                i += 1
                            End If
                        Next
                        If Qry <> "" Then
                            Qry = Mid(Qry, 1, Len(Qry.Trim) - 1)
                        End If
                        sQry = Split(Qry, "|")
                        j = 1
                        Exit For
                    End If
                Next

Repeat:
                If j = 1 Then
                    sQuery = ""
                    For k = 0 To sQry.Length - 1
                        sQuery = sQuery & " Insert " & TblName & " ( " & sFieldString & " ) "
                        sQuery = sQuery & " values ('"
                        For x = 0 To T1.Rows.Count - 1
                            If TypeOf (T1.Rows(x).Controls(1).Controls(0)) Is TextBox Then
                                sQuery = sQuery & CType(T1.Rows(x).Controls(1).Controls(0), TextBox).Text.Trim & "', '"
                            ElseIf TypeOf (T1.Rows(x).Controls(1).Controls(0)) Is DropDownList Then
                                sQuery = sQuery & CType(T1.Rows(x).Controls(1).Controls(0), DropDownList).SelectedValue & "', '"
                            ElseIf TypeOf (T1.Rows(x).Controls(1).Controls(0)) Is DTP Then
                                sQuery = sQuery & CType(T1.Rows(x).Controls(1).Controls(0), DTP).Text.Trim & "', '"
                            ElseIf TypeOf (T1.Rows(x).Controls(1).Controls(0)) Is Label Then
                                sQuery = sQuery & CType(T1.Rows(x).Controls(1).Controls(0), Label).Text.Trim & "', '"
                            ElseIf TypeOf (T1.Rows(x).Controls(1).Controls(0)) Is CheckBox Then
                                sQuery = sQuery & IIf(CType(T1.Rows(x).Controls(1).Controls(0), CheckBox).Checked = True, "1", "0") & "', '"
                            ElseIf TypeOf (T1.Rows(x).Controls(1).Controls(0)) Is ListBox Then
                                sQuery = sQuery & sQry(k) & "', '"
                            End If
                        Next
                        sQuery = Mid(sQuery, 1, Len(sQuery.Trim) - 2)
                        sQuery = sQuery & " )"
                    Next
                Else
                    sQuery = Mid(sQuery, 1, Len(sQuery.Trim) - 2)
                    sQuery = sQuery & " )"
                End If

                Session("DalObj").ExecuteCommand(sQuery)

            ElseIf txtAction.Text = "MODIFY" Then

                sQuery = "Update " & TblName & " Set "
                For x = 0 To DtDisplay.Rows.Count - 1
                    sQuery = sQuery & DtDisplay.Rows(x).Item("FieldName") & " = "
                    If TypeOf (T1.Rows(x).Controls(1).Controls(0)) Is TextBox Then
                        sQuery = sQuery & "'" & CType(T1.Rows(x).Controls(1).Controls(0), TextBox).Text.Trim & "', "
                    ElseIf TypeOf (T1.Rows(x).Controls(1).Controls(0)) Is DropDownList Then
                        sQuery = sQuery & "'" & IIf(CType(T1.Rows(x).Controls(1).Controls(0), DropDownList).SelectedValue = "", "0", CType(T1.Rows(x).Controls(1).Controls(0), DropDownList).SelectedValue) & "', "
                    ElseIf TypeOf (T1.Rows(x).Controls(1).Controls(0)) Is DTP Then
                        sQuery = sQuery & "'" & CType(T1.Rows(x).Controls(1).Controls(0), DTP).Text.Trim & "', "
                    ElseIf TypeOf (T1.Rows(x).Controls(1).Controls(0)) Is Label Then
                        sQuery = sQuery & "'" & CType(T1.Rows(x).Controls(1).Controls(0), Label).Text.Trim & "', "
                    ElseIf TypeOf (T1.Rows(x).Controls(1).Controls(0)) Is CheckBox Then
                        sQuery = sQuery & "'" & IIf(CType(T1.Rows(x).Controls(1).Controls(0), CheckBox).Checked = True, "1", "0") & "', "
                    ElseIf TypeOf (T1.Rows(x).Controls(1).Controls(0)) Is ListBox Then
                        For y = 0 To CType(T1.Rows(x).Controls(1).Controls(0), ListBox).Items.Count - 1
                            If CType(T1.Rows(x).Controls(1).Controls(0), ListBox).Items(y).Selected = True Then
                                Qry = Qry & CType(T1.Rows(x).Controls(1).Controls(0), ListBox).Items(y).Value & "|"
                                i += 1
                            End If
                        Next
                        sQuery = "Delete " & TblName & " Where " & DtDisplay.Rows(0).Item("FieldName") & " = '" & CType(T1.Rows(0).Controls(1).Controls(0), TextBox).Text & "'"
                        Session("DalObj").ExecuteCommand(sQuery)
                        If Qry <> "" Then
                            Qry = Mid(Qry, 1, Len(Qry.Trim) - 1)
                        End If
                        sQry = Split(Qry, "|")
                        j = 1
                        GoTo Repeat
                    End If
                Next

                sQuery = Mid(sQuery, 1, Len(sQuery.Trim) - 1)
                sQuery = sQuery & " Where " & KeyField & " = '" & CType(T1.Rows(0).Controls(1).Controls(0), TextBox).Text & "'"
                Session("DalObj").ExecuteCommand(sQuery)
            End If
            ClearAll(Me)
            'cmdNew_Click(sender, e) comment by Ravi
            btnNew_Click(sender, Nothing)
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & ex.Source)
        End Try
    End Sub

#End Region

#Region "    Validate Records   "

    Public Function IsValidate() As Boolean
        Try
            IsValidate = False
            Dim x, y As Int16, Selected As Boolean = False
            For x = 0 To DtDisplay.Rows.Count - 1

                '---------------------------------Validate Mandatory Fialds-----------------------
                If ChkN(DtDisplay.Rows(x).Item("Mandatory")) = 1 Then
                    If TypeOf (T1.Rows(x).Controls(1).Controls(0)) Is TextBox Then
                        If CType(T1.Rows(x).Controls(1).Controls(0), TextBox).Text = "" Then
                            SetMsg(LblErrMsg, DtDisplay.Rows(x).Item("AliaseName") & " cannot be left Blank.")
                            IsValidate = False
                            Exit Function
                        End If
                    ElseIf TypeOf (T1.Rows(x).Controls(1).Controls(0)) Is DropDownList Then
                        If CType(T1.Rows(x).Controls(1).Controls(0), DropDownList).SelectedIndex = CType(T1.Rows(x).Controls(1).Controls(0), DropDownList).Items.Count - 1 Then SetMsg(LblErrMsg, "Please select " & DtDisplay.Rows(x).Item("AliaseName") & " from the list.") : IsValidate = False : Exit Function
                    ElseIf TypeOf (T1.Rows(x).Controls(1).Controls(0)) Is DTP Then
                        If Trim(CType(T1.Rows(x).Controls(1).Controls(0), DTP).Text) = "0" Or Trim(CType(T1.Rows(x).Controls(1).Controls(0), DTP).Text) = "" Then SetMsg(LblErrMsg, DtDisplay.Rows(x).Item("AliaseName") & " cannot be left Blank.") : IsValidate = False : Exit Function
                    ElseIf TypeOf (T1.Rows(x).Controls(1).Controls(0)) Is ListBox Then
                        For y = 0 To CType(T1.Rows(x).Controls(1).Controls(0), ListBox).Items.Count - 1
                            If CType(T1.Rows(x).Controls(1).Controls(0), ListBox).Items(y).Selected = True Then
                                Selected = True
                            End If
                        Next
                        If Selected = False Then SetMsg(LblErrMsg, "Please select " & DtDisplay.Rows(x).Item("AliaseName") & " from the list.") : IsValidate = False : Exit Function
                    End If
                End If

                '---------------------------------Validate DataTypes of Fields-----------------------
                If Chk(DtDisplay.Rows(x).Item("DataType"), True, True) = "N" Then
                    If TypeOf (T1.Rows(x).Controls(1).Controls(0)) Is TextBox Then
                        If CType(T1.Rows(x).Controls(1).Controls(0), TextBox).Text <> "" And ChkN(CType(T1.Rows(x).Controls(1).Controls(0), TextBox).Text) = 0 Then
                            SetMsg(LblErrMsg, DtDisplay.Rows(x).Item("AliaseName") & " must be numeric type.")
                            IsValidate = False : Exit Function
                        End If
                    End If
                ElseIf Chk(DtDisplay.Rows(x).Item("DataType"), True, True) = "D" Then
                    If TypeOf (T1.Rows(x).Controls(1).Controls(0)) Is DTP Then
                        If Not IsDate((CType(T1.Rows(x).Controls(1).Controls(0), DTP).Text)) Then
                            SetMsg(LblErrMsg, "Please select valid " & DtDisplay.Rows(x).Item("AliaseName") & " , Invalid Date")
                            IsValidate = False : Exit Function
                        End If
                    End If
                End If
                '---------------------------------Validate Lenghth of Fields-----------------------

                If ChkN(DtDisplay.Rows(x).Item("MaxLength")) > 0 And Chk(DtDisplay.Rows(x).Item("DataType")) = "S" Then
                    If TypeOf (T1.Rows(x).Controls(1).Controls(0)) Is TextBox Then
                        If Len(CType(T1.Rows(x).Controls(1).Controls(0), TextBox).Text.Trim) > ChkN(DtDisplay.Rows(x).Item("MaxLength")) Then
                            SetMsg(LblErrMsg, "Length of " & DtDisplay.Rows(x).Item("AliaseName") & " must be less than Or equal to " & DtDisplay.Rows(x).Item("MaxLength"))
                            IsValidate = False : Exit Function
                        End If
                    End If
                End If

            Next
            Return True
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & ex.Source)
        End Try
    End Function

#End Region

    Private Sub cmdShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShow.Click
        If TGrid.Visible = True Then
            TGrid.Visible = False
        Else
            TGrid.Visible = True
        End If
        LblErrMsg.Text = ""
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub
    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNew.Click
        'By Ravi on 24 nov
        Try
            ClearAll(Me)
            Length = Session("DalObj").ExecuteCommand("Select EMP_CODE_LEN from COMPMAST", , DAL.DataLayer.ExecutionType.ExecuteScalar)
            Dim x, y As Int16, OutPut As String
            OutPut = Session("BALObj").GetNextNumber(TblName, KeyField)
            y = Len(OutPut)
            If y <> Length Then
                For x = y To Length
                    OutPut = "0" & OutPut
                    x = Len(OutPut)
                Next
            End If

            'CType(T1.Rows(0).Controls(1).Controls(0), TextBox).Text = Chk(OutPut)
            For x = 0 To T1.Rows.Count - 1
                If TypeOf (T1.Rows(x).Controls(1).Controls(0)) Is DTP Then
                    CType(T1.Rows(x).Controls(1).Controls(0), DTP).Text = Format(Date.Today, "dd/MMM/yyyy")
                End If
            Next
            TGrid.Visible = False
            txtAction.Text = "ADDNEW"
            LblErrMsg.Text = ""
        Catch ex As Exception
            LblErrMsg.Text = ex.Message & " " & ex.Source
        End Try
    End Sub
    Private Sub GrdDisplay_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles GrdDisplay.Init
        Try
            Dim cmdEdit As New ButtonColumn
            cmdEdit.ButtonType = ButtonColumnType.LinkButton
            cmdEdit.Text = "Edit"
            cmdEdit.HeaderText = "Modify"
            cmdEdit.CommandName = "Edit"
            GrdDisplay.Columns.Add(cmdEdit)
        Catch ex As Exception
            LblErrMsg.Text = ex.Message & " " & ex.Source
        End Try
    End Sub
#Region "   Click For Edit   "
    Private Sub GrdDisplay_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles GrdDisplay.EditCommand
        Try
            ClearAll(Me)
            txtAction.Text = "MODIFY"
            Dim j, i, K As Int16, sData As String, DView As DataView
            For j = 0 To T1.Rows.Count - 1
                If TypeOf (T1.Rows(j).Controls(1).Controls(0)) Is TextBox Then
                    CType(T1.Rows(j).Controls(1).Controls(0), TextBox).Text = Replace(e.Item.Cells(j + 1).Text(), "&nbsp;", "")
                ElseIf TypeOf (T1.Rows(j).Controls(1).Controls(0)) Is DropDownList Then
                    If e.Item.Cells(j + 1).Text() = "&nbsp;" Then
                        CType(T1.Rows(j).Controls(1).Controls(0), DropDownList).SelectedIndex = CType(T1.Rows(j).Controls(1).Controls(0), DropDownList).Items.Count - 1
                    Else
                        CType(T1.Rows(j).Controls(1).Controls(0), DropDownList).SelectedValue = e.Item.Cells(j + 1).Text()
                    End If
                ElseIf TypeOf (T1.Rows(j).Controls(1).Controls(0)) Is DTP Then
                    CType(T1.Rows(j).Controls(1).Controls(0), DTP).Text = Format(CDate(e.Item.Cells(j + 1).Text()), "dd/MMM/yyyy")
                ElseIf TypeOf (T1.Rows(j).Controls(1).Controls(0)) Is Label Then
                    CType(T1.Rows(j).Controls(1).Controls(0), Label).Text = Chk(e.Item.Cells(j + 1).Text())
                ElseIf TypeOf (T1.Rows(j).Controls(1).Controls(0)) Is CheckBox Then
                    If e.Item.Cells(j + 1).Text() = True Then
                        CType(T1.Rows(j).Controls(1).Controls(0), CheckBox).Checked = True
                    Else
                        CType(T1.Rows(j).Controls(1).Controls(0), CheckBox).Checked = False
                    End If
                ElseIf TypeOf (T1.Rows(j).Controls(1).Controls(0)) Is ListBox Then
                    DView = New DataView(DtBind)
                    DView.RowFilter = " CODE ='" & e.Item.Cells(1).Text() & "'"
                    For i = 0 To DView.Count - 1
                        For K = 0 To CType(T1.Rows(j).Controls(1).Controls(0), ListBox).Items.Count - 1
                            If Chk(DView.Item(i).Row.Item("Location")) = Chk(CType(T1.Rows(j).Controls(1).Controls(0), ListBox).Items(K).Value) Then
                                CType(T1.Rows(j).Controls(1).Controls(0), ListBox).Items(K).Selected = True
                            End If
                        Next
                    Next
                End If
            Next
            TGrid.Visible = False
        Catch ex As Exception
            LblErrMsg.Text = ex.Message & " " & ex.Source
        End Try
    End Sub
#End Region

End Class
