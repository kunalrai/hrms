Imports System.IO
Imports System
Imports DAL.DataLayer
Imports DITWebLibrary
Public Module MdlHRMS
    Public MiracleString As String = ""
    Public CompString As String    

#Region "Dal_Bal_Obj_Temp"
    Public DalObj As New DAL.DataLayer
    ' Public BalObj As New BAL.BLayer("", "")
#End Region

    Public Enum ChkComboOption
        Value
        ItemText
    End Enum

    Public Enum TreeViewType
        Skills
        Modules
    End Enum
    Public Enum AccessType
        SuperUser = 0
        FullAccess = 1
        ReadonlyAccess = 2
        Restricted = 3
    End Enum
    Public Enum ReportExportOptions
        Excel = 0
        PDF = 1
        Word = 2
    End Enum
    Public Structure ReportVars
        Dim strFormula As String
        Dim SelectionFormula As String
        Dim OrderBy As String
        Dim totNumGrp As Integer
        Dim QryType As String
        Dim ParameterVal As String
        Dim IsPrint As Boolean
        Dim Param() As String
        Dim IsParam As Boolean
        Dim ReportFileName As String
        Dim ExportOpts As ReportExportOptions
    End Structure


    Public FY_Start As Date, FY_End As Date
    Public RY_Start As Date, RY_End As Date
    Public LY_Start As Date, LY_End As Date
    Public PayDate As Date
    Public dtVarMast As DataTable
    Public dtVarMast1 As DataTable
    Public rsEmployee As DataTable
    Public VarFlds As String, VarFlds1 As String

#Region "  Chk, ChkN, ChkNull, ChkField, ChkType, ChkCombo, ChkDate  "

    Public Function Chk(ByVal TextToCheck As Object, Optional ByVal ConvertUpperCase As Boolean = False, Optional ByVal TrimResult As Boolean = True, Optional ByVal RmSplChr As Boolean = False)
        If IsDBNull(TextToCheck) Then
            Chk = " "
        ElseIf TextToCheck = "" Then
            Chk = " "
        Else
            Chk = TextToCheck
        End If
        If Chk = "" Then Chk = " "
        Chk = Replace(Chk, "&nbsp;", "")
        If ConvertUpperCase = True Then
            Chk = StrConv(Chk, vbUpperCase)
        End If
        If TrimResult Then Chk = Trim(Chk)
        If RmSplChr = True Then Chk = RemoveSpecialChar(Chk)
    End Function

    Public Function ChkN(ByVal TextToCheck As Object, Optional ByVal FormatNumber As Boolean = False, Optional ByVal Fmt As String = "00.00")
        On Error Resume Next
        If IsDBNull(TextToCheck) Then
            ChkN = 0
        ElseIf Not IsNumeric(TextToCheck) Then
            ChkN = 0
        Else
            If FormatNumber Then
                ChkN = Format(Val(TextToCheck), Fmt)
            Else
                ChkN = Val(Format(Val(TextToCheck), "00.00"))
            End If
        End If
    End Function

    Public Function ChkNull(ByVal FieldValue As Object, Optional ByVal FieldType As String = "")
        Dim FldDate As Date, FldChar As String, FldNumeric As Integer
        If IsDBNull(FieldValue) Then
            Select Case FieldType
                Case "adInteger", "adSmallInt", "adBigInt", "adSingle", "adDouble", "adNumeric", "adVarNumeric"
                    ChkNull = 0
                Case "adDate", "adDBDate", "adDBTime", "adDBTimeStamp"
                    ChkNull = Format(Date.Today, "dd/MMM/yyyy")
                Case Else
                    ChkNull = ""
            End Select
        Else
            ChkNull = FieldValue
        End If
    End Function

    Public Function ChkField(ByVal rsFields As DataView, ByVal FieldName As String) As Boolean
        Dim FieldType As Object
        On Error Resume Next
        FieldType = rsFields.Item(0).Row.Item(FieldName)
        If Err.Number = 0 Then
            ChkField = True
        Else
            ChkField = False
        End If
    End Function


    Public Sub ChkDate(ByVal argDate As Object, ByVal argControlName As String)
        If Len(argDate.Text) = 11 Then
            If Not IsDate(argDate.Text) Then
                Throw New Exception("Invalid Date (" & argControlName & "). Please Enter Valid Date.")
                Exit Sub
            End If
        Else
            Throw New Exception("Invalid Date Format of (" & argControlName & "). Please Enter in [DD/MMM/YYYY] Format.")
            Exit Sub
        End If
    End Sub

    Public Function ChkCombo(ByRef argControl As Object, ByVal argText As Object, Optional ByVal argSetValueOrText As ChkComboOption = ChkComboOption.Value)
        '''''  argControl is the Name of control for which We want to set the Value
        '''''  argText is The Value or ItemText Which We Want to set in argControl
        '''''  argSetValueOrText is The string which tells that We Want to set Value or ItemText in argControl
        Try
            If argSetValueOrText = ChkComboOption.Value Then
                If Not argControl.Items.FindByValue(Chk(argText)) Is Nothing Then
                    argControl.SelectedValue = Chk(argText)
                Else
                    argControl.SelectedIndex = argControl.Items.Count - 1
                End If
            ElseIf argSetValueOrText = ChkComboOption.ItemText Then
                If Not argControl.Items.FindByText(Chk(argText)) Is Nothing Then
                    argControl.SelectedItem.Text = Chk(argText)
                End If
            End If
        Catch ex As Exception
        End Try
    End Function
    Public Function ChkType(ByVal FieldType As Object) As System.Data.DbType
        'On Error Resume Next
        If VarType(FieldType) = VariantType.String Then
            FieldType = IIf(FieldType = "N", DbType.Int64, IIf(FieldType = "D", DbType.Date, IIf(FieldType = "L", DbType.Boolean, DbType.String)))
        End If
        Select Case FieldType
            'Case adInteger, adSmallInt, adBigInt, adSingle, adDouble, adNumeric, adVarNumeric
        Case DbType.Int16, DbType.Int32, DbType.Int64, DbType.Currency, DbType.Decimal, DbType.Double, DbType.VarNumeric
                ChkType = DbType.Double
            Case DbType.Date, DbType.DateTime, DbType.Time
                ChkType = DbType.Date
            Case DbType.Boolean ' Boolean
                ChkType = DbType.Boolean
            Case Else   'Character or Memo
                ChkType = DbType.String
        End Select
    End Function


#End Region

#Region "   Clear Page   "

    Public Sub ClearAll(ByRef frmPage As System.Web.UI.Page)
        Try
            Dim txtBox As TextBox
            Dim chkBox As CheckBox
            Dim HchkBox As HtmlInputCheckBox
            Dim dtCmbBox As DropDownList
            Dim lstBox As ListBox
            Dim ctrl As ControlCollection
            Dim i As Int16, J As Int16, K As Int16
            For i = 0 To frmPage.Controls.Count - 1
                If TypeOf frmPage.Controls(i) Is TextBox Then txtBox = frmPage.Controls(i) : IIf(InStr(LCase(txtBox.ToolTip), "num") > 0, "0", "")
                If TypeOf frmPage.Controls(i) Is CheckBox Then chkBox = frmPage.Controls(i) : chkBox.Checked = False
                If TypeOf frmPage.Controls(i) Is HtmlInputCheckBox Then HchkBox = frmPage.Controls(i) : HchkBox.Checked = False
                If TypeOf frmPage.Controls(i) Is DropDownList Then dtCmbBox = frmPage.Controls(i) : dtCmbBox.SelectedIndex = dtCmbBox.Items.Count - 1
                If TypeOf frmPage.Controls(i) Is ListBox Then lstBox = frmPage.Controls(i) : lstBox.SelectedIndex = -1
                If frmPage.Controls(i).HasControls Then
                    ClearAllSub(frmPage.Controls)
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ClearAllSub(ByVal ctrls As System.Web.UI.ControlCollection)
        Try
            Dim txtBox As TextBox
            Dim chkBox As CheckBox
            Dim HchkBox As HtmlInputCheckBox
            Dim dtCmbBox As DropDownList
            Dim lstBox As ListBox
            Dim a As String
            Dim ctrl As ControlCollection
            Dim i As Int16, J As Int16, K As Int16
            For i = 0 To ctrls.Count - 1
                If TypeOf ctrls.Item(i) Is TextBox Then txtBox = ctrls.Item(i) : txtBox.Text = IIf(InStr(LCase(txtBox.ToolTip), "num") > 0, "0", "")
                If TypeOf ctrls.Item(i) Is CheckBox Then chkBox = ctrls.Item(i) : chkBox.Checked = False
                If TypeOf ctrls.Item(i) Is HtmlInputCheckBox Then HchkBox = ctrls.Item(i) : HchkBox.Checked = False
                If TypeOf ctrls.Item(i) Is DropDownList Then dtCmbBox = ctrls.Item(i) : dtCmbBox.SelectedIndex = dtCmbBox.Items.Count - 1
                If TypeOf ctrls.Item(i) Is ListBox Then lstBox = ctrls.Item(i) : lstBox.SelectedIndex = -1
                ClearAllSub(ctrls.Item(i).Controls)
            Next
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#Region "   Enable - Disabled  "

    Public Sub EnableDisable(ByVal EnableDisable As Boolean, ByVal frmPage As System.Web.UI.Page)
        Try
            Dim txtBox As TextBox
            Dim HtxtBox As HtmlInputText
            Dim HCmbBox As HtmlSelect
            Dim HChk As HtmlInputCheckBox
            Dim chkBox As CheckBox
            Dim dtCmbBox As DropDownList
            Dim dtpCmb As DTPCombo
            Dim ctrl As ControlCollection
            Dim i As Int16, J As Int16, K As Int16
            For i = 0 To frmPage.Controls.Count - 1
                If TypeOf frmPage.Controls(i) Is TextBox Then txtBox = frmPage.Controls(i) : txtBox.Enabled = EnableDisable
                If TypeOf frmPage.Controls(i) Is HtmlInputText Then HtxtBox = frmPage.Controls(i) : HtxtBox.Disabled = EnableDisable
                If TypeOf frmPage.Controls(i) Is CheckBox Then chkBox = frmPage.Controls(i) : chkBox.Enabled = EnableDisable
                If TypeOf frmPage.Controls(i) Is DropDownList Then dtCmbBox = frmPage.Controls(i) : dtCmbBox.Enabled = EnableDisable
                If TypeOf frmPage.Controls(i) Is HtmlSelect Then HCmbBox = frmPage.Controls(i) : HCmbBox.Disabled = Not EnableDisable
                If TypeOf frmPage.Controls(i) Is HtmlInputCheckBox Then HChk = frmPage.Controls(i) : HChk.Disabled = Not EnableDisable
                If TypeOf frmPage.Controls(i) Is DTPCombo Then dtpCmb = frmPage.Controls(i) : dtpCmb.Enabled = EnableDisable
                If frmPage.Controls(i).HasControls Then
                    EnableDisableSub(EnableDisable, frmPage.Controls)
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub EnableDisableSub(ByVal EnableDisable As Boolean, ByVal ctrls As System.Web.UI.ControlCollection)
        Try
            Dim txtBox As TextBox
            Dim chkBox As CheckBox
            Dim dtCmbBox As DropDownList
            Dim HtxtBox As HtmlInputText
            Dim HCmbBox As HtmlSelect
            Dim HChk As HtmlInputCheckBox
            Dim dtpCmb As DTPCombo
            Dim a As String
            Dim ctrl As ControlCollection
            Dim i As Int16, J As Int16, K As Int16
            For i = 0 To ctrls.Count - 1
                If TypeOf ctrls.Item(i) Is TextBox Then txtBox = ctrls.Item(i) : txtBox.Enabled = EnableDisable
                If TypeOf ctrls.Item(i) Is HtmlInputText Then HtxtBox = ctrls.Item(i) : HtxtBox.Disabled = EnableDisable
                If TypeOf ctrls.Item(i) Is CheckBox Then chkBox = ctrls.Item(i) : chkBox.Enabled = EnableDisable
                If TypeOf ctrls.Item(i) Is DropDownList Then dtCmbBox = ctrls.Item(i) : dtCmbBox.Enabled = EnableDisable
                If TypeOf ctrls.Item(i) Is HtmlSelect Then HCmbBox = ctrls.Item(i) : HCmbBox.Disabled = Not EnableDisable
                If TypeOf ctrls.Item(i) Is HtmlInputCheckBox Then HChk = ctrls.Item(i) : HChk.Disabled = Not EnableDisable
                If TypeOf ctrls.Item(i) Is DTPCombo Then dtpCmb = ctrls.Item(i) : dtpCmb.Enabled = EnableDisable
                EnableDisableSub(EnableDisable, ctrls.Item(i).Controls)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "  SetMeg, ExistInDatabase "

    Public Sub SetMsg(ByRef argControl As Object, ByVal StrMsg As String)
        argControl.Text = StrMsg
        If StrMsg = "" Then
            argControl.Visible = False
        Else
            argControl.Visible = True
        End If
    End Sub

    Public Function ExistInDatabase(ByVal Name As String, ByVal NameType As SchemaType) As Boolean
        Dim CN = CreateObject("ADODB.Connection")
        Dim RS = CreateObject("ADODB.Recordset")
        CN.Open("Persist Security Info=TRUE;Integrated Security=SSPI;database=FLIIND;server=XEON;Connect Timeout=30;Trusted_Connection=Yes;")
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim rsExistInDatabase As SqlClient.SqlDataAdapter
        Try
            dt = rsExistInDatabase.FillSchema(dt, NameType)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "   Encrypt, Decrypt and RemoveSpecialChar  "

    Public Function Encrypt(ByVal txt, ByVal Sign) As String
        txt = Trim(txt)
        Dim Encrchar, lntxt, ctr, i
        Encrchar = "DivergentInformationTechnologies"
        lntxt = Len(txt)
        Do While Len(Encrchar) <= lntxt
            Encrchar = Encrchar + Encrchar
        Loop
        For i = 1 To lntxt
            If Sign = "+" Then
                Encrypt = Encrypt + Chr(Asc(Mid(txt, i, 1)) + Asc(Mid(Encrchar, i, 1)))
            Else
                Encrypt = Encrypt + Chr(Asc(Mid(txt, i, 1)) - Asc(Mid(Encrchar, i, 1)))
            End If
        Next
    End Function

    Public Function Decrypt(ByVal argStringToEncrypt As String)
        argStringToEncrypt = Trim(argStringToEncrypt)
        Dim EncryptionChar As String
        Dim lnTxt As Integer, I As Integer
        Dim TmpEncr As String
        EncryptionChar = "DivergentInformationTechnologies"
        If argStringToEncrypt <> "" Then
            lnTxt = Len(argStringToEncrypt)
            Do While Len(EncryptionChar) <= lnTxt
                EncryptionChar = EncryptionChar + EncryptionChar
            Loop
            For I = 1 To Len(argStringToEncrypt)
                TmpEncr = TmpEncr + Chr(Asc(Mid(argStringToEncrypt, I, 1)) - Asc(Mid(EncryptionChar, I, 1)))
            Next
            Return TmpEncr
        End If
    End Function

    Public Function RemoveSpecialChar(ByVal strString As String) As String
        Dim SPChar As String
        Dim I As Int16
        For I = 1 To 5
            If I = 1 Then SPChar = "'"
            If I = 2 Then SPChar = ";"
            If I = 3 Then SPChar = "%"
            If I = 4 Then SPChar = "|"
            If I = 5 Then SPChar = ","
            Do While InStr(1, strString, SPChar) > 0
                Mid(strString, InStr(1, strString, SPChar), 1) = " "
            Loop
        Next
        RemoveSpecialChar = strString
    End Function

#End Region

#Region " Calculate MonDiff, EOM, BOM, Rndoff, Remainder, AdoFind, CreateCursor, MinV, MaxV, Credit Leave, LevUpdate "

    Public Function MonDiff(ByVal Fdt As Date, ByVal Tdt As Date) As Double
        Dim MonDFdt As Integer
        Dim MonDTdt As Integer
        MonDiff = 0
        If Not IsDate(Fdt) Or Not IsDate(Tdt) Then
            Exit Function
        End If
        If Fdt < Tdt Then
            MonDFdt = Day(DateAdd(DateInterval.Day, -1 * Day(DateAdd(DateInterval.Month, 1, Fdt)), DateAdd(DateInterval.Month, 1, Fdt)))
            MonDTdt = Day(DateAdd(DateInterval.Day, -1 * Day(DateAdd(DateInterval.Month, 1, Tdt)), DateAdd(DateInterval.Month, 1, Tdt)))
            Do While DateAdd(DateInterval.Month, MonDiff, Fdt) <= Tdt
                MonDiff = MonDiff + 1
            Loop
            MonDiff = MonDiff - IIf(Day(Tdt) < Day(Fdt), 1, 2) _
                              + (MonDFdt - Day(Fdt) + 1) / MonDFdt _
                              + Day(Tdt) / MonDTdt
        End If
    End Function

    Function EOM(ByVal DateValue As Date) As Date
        'EOM = DateAdd("m", 1, DateValue - Day(DateValue) + 1) - 1
        EOM = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, DateAdd(DateInterval.Day, (-1 * Day(DateValue)) + 1, DateValue)))
    End Function

    Function BOM(ByVal DateValue As Date) As Date
        BOM = DateAdd(DateInterval.Day, (-1 * Day(DateValue)) + 1, DateValue)
    End Function

    Public Function RndOff(ByVal vAmt As Double, ByVal vRnd As Integer, ByVal vMult As Double)
        On Error Resume Next
        If vMult > 0 And vRnd > 1 Then
            Select Case vRnd
                Case 2
                    RndOff = vAmt - Remainder(vAmt, vMult) + IIf(Remainder(vAmt, vMult) >= vMult / 2, vMult, 0)
                Case 3
                    RndOff = vAmt - Remainder(vAmt, vMult)
                Case 4
                    RndOff = vAmt - Remainder(vAmt, vMult) + IIf(Remainder(vAmt, vMult) > 0, vMult, 0)
            End Select
        Else
            RndOff = vAmt
        End If
    End Function

    Public Function Remainder(ByVal Nominator As Double, ByVal Denominator As Double)
        Remainder = Nominator - Int(Nominator / Denominator) * Denominator
    End Function
    Public Function adoFind(ByVal dtFindIn As DataTable, ByVal strFindIn As String, Optional ByVal SortFlds As String = "") As DataView
        adoFind = New DataView(dtFindIn, strFindIn, SortFlds, DataViewRowState.CurrentRows)
    End Function
    Public Function CreateCursor(Optional ByVal FromRecordset As DataTable = Nothing, Optional ByVal FldsName As String = "") As DataTable
        Dim FldStr As String, FldName As String, FldLen As Integer, FldDec As Integer, FldType As String, StrPos1 As Integer, StrPos2 As Integer, TableExist As Boolean
        Dim i As Int32
        CreateCursor = New DataTable
        If Not FromRecordset Is Nothing Then 'Or Not IsMissing(FromRecordset) Then
            With FromRecordset
                For i = 0 To FromRecordset.Columns.Count - 1
                    FldName = FromRecordset.Columns(i).ColumnName
                    FldType = IIf(ChkType(FromRecordset.Columns(i).DataType) = DbType.Double, "N", IIf(ChkType(FromRecordset.Columns(i).DataType) = DbType.Date, "D", "C"))
                    FldLen = FromRecordset.Columns(i).MaxLength
                    FldDec = 5
                    Select Case FldType
                        Case "N"
                            CreateCursor.Columns.Add(FldName, System.Type.GetType("Double"), "")
                        Case "D"
                            CreateCursor.Columns.Add(FldName, System.Type.GetType("Date"), "")
                        Case Else
                            CreateCursor.Columns.Add(FldName, System.Type.GetType("String"), "")
                    End Select
                Next
            End With
        End If
        If Chk(FldsName, True, True) <> "" Then
            StrPos1 = 0
            Do While True
                StrPos1 = InStr(StrPos1 + 1, ":" & FldsName & ":", ":")
                If StrPos1 = 0 Then Exit Do
                StrPos2 = InStr(StrPos1 + 1, ":" & FldsName & ":", ":")
                If StrPos2 = 0 Then Exit Do
                FldStr = Mid(":" & FldsName & ":", StrPos1 + 1, StrPos2 - StrPos1 - 1)
                FldName = Trim(Mid(FldStr, 1, InStr(FldStr, "(") - 2))
                FldType = Mid(FldStr, InStr(FldStr, "(") - 1, 1)
                FldLen = Val(Mid(FldStr, InStr(FldStr, "(") + 1))
                FldDec = Val(Mid(FldStr, InStr(FldStr, ",") + 1))
                Select Case FldType
                    Case "N"
                        CreateCursor.Columns.Add(FldName, System.Type.GetType("Double"), "")
                    Case "D"
                        CreateCursor.Columns.Add(FldName, System.Type.GetType("Date"), "")
                    Case Else
                        CreateCursor.Columns.Add(FldName, System.Type.GetType("String"), "")
                End Select
            Loop
        End If
    End Function
    Public Function MinV(ByVal ParamArray ValuesArray())
        On Error Resume Next
        Dim ValNo As Integer
        MinV = ValuesArray(0)
        For ValNo = 0 To UBound(ValuesArray, 1)
            If ValuesArray(ValNo) < MinV Then MinV = ValuesArray(ValNo)
        Next
    End Function
    Public Function MaxV(ByVal ParamArray ValuesArray())
        On Error Resume Next
        Dim ValNo As Integer
        MaxV = ValuesArray(0)
        For ValNo = 0 To UBound(ValuesArray, 1)
            If ValuesArray(ValNo) > MaxV Then MaxV = ValuesArray(ValNo)
        Next
    End Function
    Public Function xMax(ByVal x1, ByVal x2)
        On Error Resume Next
        If x1 > x2 Then
            xMax = x1
        Else
            xMax = x2
        End If
    End Function
    Public Function xMin(ByVal x1, ByVal x2)
        On Error Resume Next
        If x1 < x2 Then
            xMin = x1
        Else
            xMin = x2
        End If
    End Function
    Public Function Betw(ByVal vVal, ByVal LowrLmt, ByVal UpperLmt) As Boolean
        On Error Resume Next
        If vVal >= LowrLmt And vVal <= UpperLmt Then
            Betw = True
        Else
            Betw = False
        End If
    End Function

    Private Function CreditLeave(ByVal Entitlement As Double, ByVal Cr_Period As Integer, ByVal Cr_Month As Integer, ByVal FromDate As Date, ByVal ToDate As Date) As Double
        Dim Months As Integer, StartDate As Date, EndDate As Date, i As Integer
        Dim StartPeriod As Date
        CreditLeave = 0
        If Cr_Period < 2 Then Exit Function
        Months = IIf(Cr_Period = 2, 1, IIf(Cr_Period = 3, 3, IIf(Cr_Period = 4, 6, 12)))
        StartPeriod = DateAdd(DateInterval.Month, Cr_Month - Month(FromDate), DateAdd(DateInterval.Day, 1 - Day(FromDate), FromDate))
        StartPeriod = IIf(StartPeriod > ToDate, DateAdd(DateInterval.Month, -12, StartPeriod), StartPeriod)
        For i = 1 To 12 / Months
            StartDate = MaxV(DateAdd("m", Months * (i - 1), StartPeriod), FromDate)
            EndDate = MinV(DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, Months * i, StartPeriod)), ToDate)
            CreditLeave = CreditLeave + Entitlement / Months * MonDiff(StartDate, EndDate)
        Next
        'CreditLeave = FormatNumber(CreditLeave, 1)
        CreditLeave = Math.Round(RndOff(CreditLeave, 2, 0.05), 2)
    End Function

    Public Sub LevUpdate(ByVal Emp_Code As String, ByVal AsOnDate As Date)
        Dim DalObj As DAL.DataLayer
        Dim rsLvType As New DataTable, dvLvType As DataView, _
            rsLeavMast As New DataTable, _
            rsLeavTran As New DataTable, _
            rsPaySetup As New DataTable, _
            rsTransfer As New DataTable, _
            BgtDays As Double, ErnDays As Double, _
            LvDays As Double
        Dim TDate, Fdate As Date
        Dim YosNum, YOS, YSN As Double
        Dim i, j, k As Int32
        Dim drVarMast As DataRow, dvEmployee As DataView
        Dim drVarMast1 As DataRow
        On Error Resume Next
        dvEmployee = adoFind(rsEmployee, "Emp_Code='" & Emp_Code & "'")
        If dvEmployee.Count = 0 Then Exit Sub
        If dtVarMast Is Nothing Then
            VarFlds = "" : VarFlds1 = ""
            DalObj.GetSqlDataTable(rsPaySetup, "SELECT * FROM PaySetup WHERE Fld_HRDMAST='Y'")
            For i = 0 To rsPaySetup.Rows.Count - 1
                If rsPaySetup.Rows(i).Item("Field_Type") = "N" Then
                    VarFlds = VarFlds & ":" & Trim(rsPaySetup.Rows(i).Item("Field_Name")) & _
                        " " & Trim(rsPaySetup.Rows(i).Item("Field_Type")) & _
                        "(" & rsPaySetup.Rows(i).Item("Field_Len") & _
                        "," & rsPaySetup.Rows(i).Item("Field_Dec") & ")"
                Else
                    VarFlds1 = VarFlds1 & ":" & Trim(rsPaySetup.Rows(i).Item("Field_Name")) & _
                        " " & Trim(rsPaySetup.Rows(i).Item("Field_Type")) & _
                        "(" & rsPaySetup.Rows(i).Item("Field_Len") & _
                        "," & rsPaySetup.Rows(i).Item("Field_Dec") & ")"
                End If
            Next
            rsPaySetup = Nothing
            dtVarMast = CreateCursor(, Mid(VarFlds, 2))
            dtVarMast1 = CreateCursor(, Mid(VarFlds1, 2))
        End If
        drVarMast = dtVarMast.NewRow
        drVarMast1 = dtVarMast1.NewRow
        'rsVarMast1!Comp_Code = LoginUser.Company
        For i = 0 To dtVarMast.Columns.Count - 1 'rsVarMast.Fields.Count - 1
            drVarMast.Item(dtVarMast.Columns(i).ColumnName) = dvEmployee.Item(0).Item(dtVarMast.Columns(i).ColumnName)
        Next
        For i = 0 To dtVarMast1.Columns.Count - 1 'rsVarMast.Fields.Count - 1
            drVarMast1.Item(dtVarMast1.Columns(i).ColumnName) = dvEmployee.Item(0).Item(dtVarMast1.Columns(i).ColumnName)
        Next
        drVarMast.AcceptChanges()
        drVarMast1.AcceptChanges()
        dtVarMast.Rows.Add(drVarMast)
        dtVarMast1.Rows.Add(drVarMast1)

        DalObj.GetSqlDataTable(rsLvType, "SELECT * FROM LvType")
        DalObj.GetSqlDataTable(rsLeavMast, "SELECT * FROM LeavMast WHERE LevYear='" & Year(LY_Start) & "' AND Emp_Code='" & Emp_Code & "' ORDER BY Emp_Code, LvType")
        'DalObj.GetSqlDataTable(rsLeavTran, "SELECT * FROM LeavTran WHERE LevYear='" & Year(LY_Start) & "' AND Emp_Code='" & Emp_Code & "' AND Format(AtDate,'YYYYMMDD')<='" & Format(AsOnDate.Value, "YYYYMMDD") & "' ORDER BY Emp_Code, LvType,atdate")
        DalObj.GetSqlDataTable(rsTransfer, "SELECT * FROM Transfer WHERE Emp_Code='" & Emp_Code & "' AND Format(TransferWEF,'YYYYMMDD') BETWEEN '" & Format(LY_Start, "YYYYMMDD") & "' AND '" & Format(LY_End, "YYYYMMDD") & "' ORDER BY TransferWEF DESC")
        For i = 0 To rsLvType.Rows.Count - 1
            ErnDays = 0
            dvLvType = adoFind(rsLeavMast, "LvType='" & rsLvType.Rows(i).Item("LvType") & "'")
            If dvLvType.Count > 0 Then
                If Chk(Trim(dvLvType.Item(i).Item("Cr_Days"))) <> "" Then
                    If dvEmployee.Item(0).Item("DOJ") <= AsOnDate And (IsDBNull(dvEmployee.Item(0).Item("DOL")) Or dvEmployee.Item(0).Item("DOL") > LY_Start) Then
                        TDate = MinV(AsOnDate, LY_End, IIf(IsDBNull(dvEmployee.Item(0).Item("DOL")), LY_End, dvEmployee.Item(0).Item("DOL")))
                        '''''            If rsTransfer.RecordCount > 0 Then rsTransfer.MoveFirst()
                        ''''Do While Not rsTransfer.EOF And TDate <= AsOnDate.Value
                        For j = 0 To rsTransfer.Rows.Count - 1
                            If TDate <= AsOnDate Then
                                dtVarMast1.Rows(0).Item("Grd_Code") = rsTransfer.Rows(j).Item("Grd_Code")
                                dtVarMast1.Rows(0).Item("Dsg_Code") = rsTransfer.Rows(j).Item("Dsg_Code")
                                dtVarMast1.Rows(0).Item("Loc_Code") = rsTransfer.Rows(j).Item("Loc_Code")
                                dtVarMast1.Rows(0).Item("Cost_Code") = rsTransfer.Rows(j).Item("Cost_Code")
                                dtVarMast1.Rows(0).Item("Proc_Code") = rsTransfer.Rows(j).Item("Proc_Code")
                                Fdate = MaxV(Fdate, rsTransfer.Rows(j).Item("TransferWEF"))
                                YOS = Fix((MaxV(dvEmployee.Item(0).Item("DOJ"), LY_Start) - dvEmployee.Item(0).Item("DOJ")) / 365)
                                YSN = Fix(DateDiff(DateInterval.Day, DateSerial(Year(MaxV(dvEmployee.Item(0).Item("DOJ"), LY_Start)), Month(dvEmployee.Item(0).Item("DOJ")), Day(dvEmployee.Item(0).Item("DOJ"))), dvEmployee.Item(0).Item("DOJ")) / 365)
                                'BgtDays = FldCal("", "N", ChkN(ErnDays), Replace(Replace(Chk(dvLvType.Item(i).Item("Cr_Days")), "{@YOS}", YOS), "{@YSN}", YSN), "", ChkN(dvLvType.Item(i).Item("Cr_Period")), ChkN(dvLvType.Item(i).Item("Cr_Month")), 2, 0.05, "")
                                ErnDays = ErnDays + CreditLeave(BgtDays, ChkN(dvLvType.Item(i).Item("Cr_Period")), ChkN(dvLvType.Item(i).Item("Cr_Month")), Fdate, TDate)
                                'ErnDays = ErnDays + BgtDays / 12 * MonDiff(FDate, TDate)
                                dtVarMast1.Rows(0).Item("Grd_Code") = rsTransfer.Rows(j).Item("Grd_Prv")
                                dtVarMast1.Rows(0).Item("Dsg_Code") = rsTransfer.Rows(j).Item("Dsg_Prv")
                                dtVarMast1.Rows(0).Item("Loc_Code") = rsTransfer.Rows(j).Item("Loc_Prv")
                                dtVarMast1.Rows(0).Item("Cost_Code") = rsTransfer.Rows(j).Item("Cost_Prv")
                                dtVarMast1.Rows(0).Item("Proc_Code") = rsTransfer.Rows(j).Item("Proc_Prv")
                                TDate = DateAdd(DateInterval.Day, -1, Fdate)
                            End If
                        Next
                        Fdate = MaxV(dvEmployee.Item(0).Item("DOJ"), LY_Start)
                        YosNum = DateDiff(DateInterval.Day, Fdate, dvEmployee.Item(0).Item("DOJ")) / 365
                        YOS = IIf(YosNum > Fix(YosNum), Fix(YosNum + 1), Fix(YosNum))     'Fix((FDate - rsEmployee!DOJ) / 365)
                        YSN = Fix(DateDiff(DateInterval.Day, DateSerial(Year(Fdate), Month(dvEmployee.Item(0).Item("DOJ")), Day(dvEmployee.Item(0).Item("DOJ"))), dvEmployee.Item(0).Item("DOJ")) / 365)
                        'BgtDays = FldCal("", "N", BgtDays, Replace(Replace(Chk(dvLvType.Item(i).Item("Cr_Days")), "{@YOS}", YOS), "{@YSN}", YSN), "", ChkN(dvLvType.Item(i).Item("Cr_Period")), ChkN(dvLvType.Item(i).Item("Cr_Month")), 2, 0.05, "")
                        ErnDays = ErnDays + CreditLeave(BgtDays, ChkN(dvLvType.Item(i).Item("Cr_Period")), ChkN(dvLvType.Item(i).Item("Cr_Month")), Fdate, TDate)
                        'rsLeavMast!Earned = CreditLeave(Earned, ChkNull(adNumeric, rsLvType!Cr_Period), ChkNull(adNumeric, rsLvType!Cr_Month), xMax(rsLeavMast!WEF, rsEmployee!DOJ, LY_Start), xMin(LY_End, IIf(IsNull(rsEmployee!DOL), AsOnDate.Value, rsEmployee!DOL), AsOnDate.Value, LY_End))
                    Else
                        ''''rsLeavMast!Earned = Null
                        ErnDays = 0
                    End If
                End If
                '''''    rsLeavMast!Availed = 0
                '''''    If rsLeavTran.RecordCount > 0 Then rsLeavTran.MoveFirst()
                '''''    Do While Not rsLeavTran.EOF
                '''''        If Mid(rsLeavTran!LvType, 1, 1) = rsLvType!LvType Then
                '''''            If Mid(rsLeavTran!LvType, 1, 1) = Mid(rsLeavTran!LvType, 2, 1) Then
                '''''                rsLeavMast!Availed = rsLeavMast!Availed + IIf(rsLvType!hourly = "Y", 8, 1)
                '''''            Else
                '''''                rsLeavMast!Availed = rsLeavMast!Availed + ChkNull(adNumeric, rsLeavTran!LvDays) * IIf(rsLvType!hourly = "Y", IIf(IsNull(rsLeavTran!LvHours), 4, rsLeavTran!LvHours), 0.5)
                '''''            End If
                '''''        End If
                '''''        If Mid(rsLeavTran!LvType, 2, 1) = rsLvType!LvType Then
                '''''            Debug.Print(rsLeavTran("ATDATE"))

                '''''            If Mid(rsLeavTran!LvType, 1, 1) = Mid(rsLeavTran!LvType, 2, 1) Then
                '''''                'rsLeavMast!Availed = rsLeavMast!Availed + 8
                '''''            Else
                '''''                rsLeavMast!Availed = rsLeavMast!Availed + ChkNull(adNumeric, rsLeavTran!LvDays) * IIf(rsLvType!hourly = "Y", IIf(IsNull(rsLeavTran!LvHours), 4, rsLeavTran!LvHours), 0.5)
                '''''                '                   rsLeavMast!Availed = rsLeavMast!Availed + ChkNull(adNumeric, rsLeavTran!LvDays) * IIf(rsLvType!hourly = "Y", IIf(IsNull(rsLeavTran!LvHours), 4, rsLeavTran!LvHours), 0.5)
                '''''            End If
                '''''        End If
                '''''        rsLeavTran.MoveNext()
                '''''    Loop
                '''''    rsLeavMast.Update()
            End If
            '''''rsLvType.MoveNext()
        Next
        rsLvType = Nothing
        rsLeavMast = Nothing
        rsLeavTran = Nothing
    End Sub
    Public Function MergeTable(ByRef dtFrom As DataTable, ByRef dtTo As DataTable)
        Dim i As Int16
        For i = 0 To dtFrom.Rows.Count - 1
            dtTo.ImportRow(dtFrom.Rows(i))
        Next
    End Function
#End Region

#Region "  Create Tree View "

    Public Function CreateTreeView(ByRef Table As HtmlTable, Optional ByVal TreeType As TreeViewType = TreeViewType.Skills, Optional ByVal DisplayCombo As Boolean = False, Optional ByVal ConString As String = "")

        If ConString = "" Then Exit Function

        DalObj.SqlConnectionString = ConString
        DalObj.OpenConnection(ConnProvider.SQL)

        Dim i As Int16, dt As New DataTable, str As String
        Dim TblRow As HtmlTableRow, RowCell1 As HtmlTableCell, RowCell2 As HtmlTableCell, Tbl As HtmlTable
        'Session("TblSkill") = New HtmlTable
        'Session("TblSkill") = TblSkills
        str = "Select SKILL_CODE, SKILL_NAME, Skill_Grp From SKILLMAST Where Skill_Scale='R' and Skill_Grp='*'"
        DalObj.GetSqlDataTable(dt, str)

        For i = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                TblRow = New HtmlTableRow
                RowCell1 = New HtmlTableCell
                RowCell2 = New HtmlTableCell
                Dim img As New HtmlImage
                Tbl = New HtmlTable

                '=================First Cell==================
                RowCell1.Align = "Left"
                RowCell1.Width = "5%"
                RowCell1.VAlign = "Top"

                img.ID = "Img" & .Item("SKILL_CODE")
                img.Src = "images/Plus.gif"
                img.Attributes.Add("OnClick", "ShowMenu('" & .Item("SKILL_CODE") & "')")
                img.Width = 9
                img.Height = 9

                RowCell1.Controls.Add(img)

                '=================Second Cell==================
                Tbl.ID = "Tbl" & .Item("Skill_Code")
                Tbl.CellPadding = 2
                Tbl.CellSpacing = 3
                Tbl.Border = 0
                Tbl.Style.Item("display") = "none"
                CreateChildNode(.Item("Skill_Code"), Tbl, DisplayCombo)

                RowCell2.Align = "Left"
                RowCell2.VAlign = "Top"
                RowCell2.InnerText = .Item("SKILL_NAME")
                RowCell2.Style.Item("FONT-WEIGHT") = "bold"
                RowCell2.Style.Item("FONT-SIZE") = "8pt"
                RowCell2.Controls.Add(Tbl)

                TblRow.Cells.Add(RowCell1)
                TblRow.Cells.Add(RowCell2)
                Table.Rows.Add(TblRow)

            End With
        Next
        Return Table
    End Function

    Public Function CreateChildNode(ByVal CD As String, ByRef HtmlTbl As HtmlTable, Optional ByVal DisplayCombo As Boolean = False) As String
        Dim rsChild As New DataTable, cnt As Int16, StrQuery As String, Code As Int16
        Dim TblRow As HtmlTableRow, RowCell1 As HtmlTableCell, RowCell2 As HtmlTableCell, RowCell3 As HtmlTableCell
        Dim Tbl As HtmlTable, img As HtmlImage, cmb As HtmlSelect, Chk As HtmlInputCheckBox, RowCell4 As HtmlTableCell
        Dim CmbSkill As DropDownList, dt As DataTable

        Dim strSql = "Select SKILL_CODE, SKILL_NAME, Skill_Grp, Skill_Type From SKILLMAST Where Skill_Scale='R' and Skill_Grp='" & CD & "'"
        DalObj.GetSqlDataTable(rsChild, strSql)

        For cnt = 0 To rsChild.Rows.Count - 1
            With rsChild.Rows(cnt)
                TblRow = New HtmlTableRow
                RowCell2 = New HtmlTableCell
                RowCell3 = New HtmlTableCell
                RowCell4 = New HtmlTableCell
                img = New HtmlImage

                Tbl = New HtmlTable

                If .Item("Skill_Type") <> "KS" Then
                    '=================First Cell==================
                    RowCell1 = New HtmlTableCell

                    RowCell1.Align = "Left"
                    RowCell1.Width = "5px"
                    RowCell1.VAlign = "Top"

                    img.Src = "images/Plus.gif"
                    img.ID = "img" & .Item("Skill_Code")

                    img.Attributes.Add("OnClick", "ShowMenu('" & .Item("SKILL_CODE") & "')")
                    img.Width = 9
                    img.Height = 9
                    RowCell1.Controls.Add(img)
                    TblRow.Cells.Add(RowCell1)

                Else
                    '=================Second Cell==================
                    Chk = New HtmlInputCheckBox
                    Chk.ID = "Chk" & .Item("Skill_Code")
                    Chk.Style.Item("width") = "15px"
                    Chk.Style.Item("height") = "15px"

                    If DisplayCombo = True Then
                        Chk.Attributes.Add("onclick", "ShowCombo('" & .Item("SKILL_CODE") & "')")
                    End If

                    Chk.Checked = False
                    RowCell2.Align = "Left"
                    RowCell2.VAlign = "Top"
                    RowCell2.Controls.Add(Chk)


                    If DisplayCombo = True Then
                        '=================Fourth Cell (Leaf Node) ==================
                        dt = New DataTable
                        cmb = New HtmlSelect
                        cmb.Attributes.Add("runat", "server")
                        cmb.ID = "cmb" & .Item("Skill_Code")
                        cmb.Style.Item("width") = "95px"
                        cmb.Style.Item("display") = "none"
                        DalObj.GetSqlDataTable(dt, "Select Skill_Rate, Skill_Rate_Desc from SkillRate")
                        cmb.DataSource = dt
                        cmb.DataTextField = "Skill_Rate_Desc"
                        cmb.DataValueField = "Skill_Rate"
                        cmb.DataBind()

                        RowCell4.Align = "Left"
                        RowCell4.VAlign = "Top"
                        RowCell4.Controls.Add(cmb)
                    End If

                End If

                '=================Third Cell===========================

                Tbl.ID = "Tbl" & .Item("Skill_Code")
                Tbl.CellPadding = 0
                Tbl.CellSpacing = 3
                Tbl.Style.Item("display") = "none"
                CreateChildNode(.Item("Skill_Code"), Tbl, DisplayCombo)

                RowCell3.Align = "Left"
                RowCell3.VAlign = "Top"
                RowCell3.InnerText = .Item("SKILL_NAME")
                RowCell3.Controls.Add(Tbl)

                '======================Adding Cells & Rows in Table====== 
                If Not RowCell2 Is Nothing Then TblRow.Cells.Add(RowCell2)
                TblRow.Cells.Add(RowCell3)
                If Not RowCell4 Is Nothing Then TblRow.Cells.Add(RowCell4)
                HtmlTbl.Rows.Add(TblRow)
            End With
        Next
        rsChild.Dispose()

    End Function

#End Region
#Region "  Report Genration"
    Public Function GetExcelFile(ByVal dt As DataTable) As String
        Dim sFilePath As String
        Dim sFileName As String
        Dim sw As StreamWriter
        Try
            sFilePath = HttpContext.Current.Request.ServerVariables("APPL_PHYSICAL_PATH")
            sFileName = "Export\Exp" & Format(Date.Now, "yyyyMMddhhmmss") & ".XLS"
            sFilePath += "\" & sFileName
            sw = New StreamWriter(sFilePath)
            ' Add some text to the file.
            For i As Int16 = 0 To dt.Columns.Count - 1
                sw.Write(dt.Columns(i).ColumnName & vbTab)
            Next
            sw.WriteLine("")
            For j As Int16 = 0 To dt.Rows.Count - 1
                For i As Int16 = 0 To dt.Columns.Count - 1
                    sw.Write(dt.Rows(j).Item(i) & vbTab)
                Next
                sw.WriteLine("")
            Next
            GetExcelFile = sFileName
        Catch ex As Exception

        Finally
            sw.Close()
        End Try
    End Function
    Public Function SetReport(ByVal SrNo As Int16, ByVal sCreteria As String, ByRef oDAL As DAL.DataLayer, ByRef oUser As DAL.DataLayer.Users, Optional ByRef Report As FrmReports = Nothing) As String
        'Dim RV As ReportView
        'RV = New ReportView
        Dim Str As String
        Dim dtRpt As New DataTable
        Dim RepVars As ReportVars
        Try
            oDAL.GetSqlDataTable(dtRpt, "Select * From MstReports Where SrNo=" & ChkN(SrNo))
            If Chk(dtRpt.Rows(0).Item("RptFile")) <> "" Then
                'RV = New ReportView
                RepVars.SelectionFormula = ""
                RepVars.strFormula = ""
                'strFormula()
                Dim RptName = Chk(dtRpt.Rows(0).Item("RptFile"))
                Dim txtCond As String = Chk(dtRpt.Rows(0).Item("RptFor"))

                If RptName = "" Then
                    SetReport = "Report File Not Set."
                    Exit Function
                End If

                If Dir(HttpContext.Current.Request.ServerVariables("APPL_PHYSICAL_PATH") & oUser.ReportFolder & "\" & RptName & ".rpt") = "" Then
                    SetReport = "Report File [" & HttpContext.Current.Request.ServerVariables("APPL_PHYSICAL_PATH") & oUser.ReportFolder & "\" & RptName & ".rpt" & "] Not Found ."
                    Exit Function
                End If
                RepVars.ReportFileName = HttpContext.Current.Request.ServerVariables("APPL_PHYSICAL_PATH") & oUser.ReportFolder & "\" & RptName & ".rpt"
                'RepVars.ReportFileName = oUser.ReportFolder & "\" & RptName & ".rpt"
                'Response.Write(RepVars.ReportFileName)
                'SetMsg(LblMsg, RepVars.ReportFileName)

                If txtCond <> "" Then
                    txtCond = Replace(txtCond, "@FY_START", "DATE(" & Format(FY_Start, "yyyy,MM,dd") & ")")
                    txtCond = Replace(txtCond, "@FY_END", "DATE(" & Format(FY_End, "yyyy,MM,dd") & ")")
                    txtCond = Replace(txtCond, "@RY_START", "DATE(" & Format(RY_Start, "yyyy,MM,dd") & ")")
                    txtCond = Replace(txtCond, "@RY_END", "DATE(" & Format(RY_End, "yyyy,MM,dd") & ")")
                    txtCond = Replace(txtCond, "@LY_START", "DATE(" & Format(LY_Start, "yyyy,MM,dd") & ")")
                    txtCond = Replace(txtCond, "@LY_END", "DATE(" & Format(LY_End, "yyyy,MM,dd") & ")")
                End If

                If Trim(sCreteria) <> "" Then
                    txtCond = txtCond & " AND " & Trim(sCreteria)
                End If

                If Chk(dtRpt.Rows(0).Item("QryType")) <> "" Then
                    txtCond = txtCond & " AND {" & IIf(Chk(dtRpt.Rows(0).Item("QryType")) = "M", "HRDMASTQRY", "HRDHISTQRY") & ".Comp_Code}='" & oUser.CurrentCompID & "'"
                    If oUser.UserGroup = "USER" Then
                        If Chk(dtRpt.Rows(0).Item("QryType")) = "M" Then
                            txtCond = txtCond & " AND {HRDMASTQRY.EMP_CODE} = '" & oUser.UserID & "'"
                        Else
                            txtCond = txtCond & " AND {HRDHISTQRY.EMP_CODE} = '" & oUser.UserID & "'"
                        End If
                    End If
                    RepVars.SelectionFormula = txtCond
                End If
                If InStr(Chk(dtRpt.Rows(0).Item("RptFor")), "{@DATE}") > 0 Then
                    RepVars.strFormula = "DATE^" & "DATE(" & Format(Date.Today, "yyyy,MM,dd") & ")"
                ElseIf InStr(Chk(dtRpt.Rows(0).Item("RptFor")), "{@FDATE}") > 0 Then
                    RepVars.strFormula = "FDATE^" & "DATE(" & Format(Date.Today, "yyyy,MM,dd") & ")|" & "TDATE^" & "DATE(" & Format(Date.Today, "yyyy,MM,dd") & ")"
                ElseIf InStr(Chk(dtRpt.Rows(0).Item("RptFor")), "{@MONTH}") > 0 Then
                    RepVars.strFormula = "MONTH^" & "Date(" & Format(DateAdd(DateInterval.Day, -Day(CDate("01/" & MonthName(Date.Today.Month) & "/" & Date.Today.Year.ToString)), DateAdd(DateInterval.Month, 1, CDate("01/" & MonthName(Date.Today.Month) & "/" & Date.Today.Year.ToString))), "yyyy,MM,dd") & ")"
                ElseIf InStr(Chk(dtRpt.Rows(0).Item("RptFor")), "{@FMONTH}") > 0 Then
                    RepVars.strFormula = "FMONTH^" & "Date(" & Format(DateAdd(DateInterval.Day, -Day(CDate("01/" & MonthName(Date.Today.Month) & "/" & Date.Today.Year.ToString)), DateAdd(DateInterval.Month, 1, CDate("01/" & MonthName(Date.Today.Month) & "/" & Date.Today.Year.ToString))), "yyyy,MM,dd") & ")"
                    RepVars.strFormula = RepVars.strFormula & " | " & "TMONTH^" & "Date(" & Format(DateAdd(DateInterval.Day, -Day(CDate("01/" & MonthName(Date.Today.Month) & "/" & Date.Today.Year.ToString)), DateAdd(DateInterval.Month, 1, CDate("01/" & MonthName(Date.Today.Month) & "/" & Date.Today.Year.ToString))), "yyyy,MM,dd") & ")"
                End If
                PayDate = CDate(Format(DateAdd(DateInterval.Day, -Day(CDate("01/" & MonthName(Date.Today.Month) & "/" & Date.Today.Year.ToString)), DateAdd(DateInterval.Month, 1, CDate("01/" & MonthName(Date.Today.Month) & "/" & Date.Today.Year.ToString))), "yyyy,MM,dd"))
                'Else
                '    RepVars.SelectionFormula = txtCond
                '    If InStr(Chk(dtRpt.Rows(0).Item("RptFor")), "{@DATE}") > 0 Then
                '        RepVars.strFormula = "DATE^" & "DATE(" & Format(dtpAsOn.DateValue, "yyyy,MM,dd") & ")"
                '    ElseIf InStr(Chk(dtRpt.Rows(0).Item("RptFor")), "{@FDATE}") > 0 Then
                '        RepVars.strFormula = "FDATE^" & "DATE(" & Format(dtpFrom.DateValue, "yyyy,MM,dd") & ")|" & "TDATE^" & "DATE(" & Format(dtpTo.DateValue, "yyyy,MM,dd") & ")"
                '    ElseIf InStr(Chk(dtRpt.Rows(0).Item("RptFor")), "{@MONTH}") > 0 Then
                '        RepVars.strFormula = "MONTH^" & "Date(" & Format(DateAdd(DateInterval.Day, -Day(CDate("01/" & MonthName(cmbMonth.SelectedValue) & "/" & cmbYear.SelectedItem.Text)), DateAdd(DateInterval.Month, 1, CDate("01/" & MonthName(cmbMonth.SelectedValue) & "/" & cmbYear.SelectedItem.Text))), "yyyy,MM,dd") & ")"
                '    ElseIf InStr(Chk(dtRpt.Rows(0).Item("RptFor")), "{@FMONTH}") > 0 Then
                '        RepVars.strFormula = "FMONTH^" & "Date(" & Format(DateAdd(DateInterval.Day, -Day(CDate("01/" & MonthName(cmbMonthFrom.SelectedValue) & "/" & cmbYearFrom.SelectedItem.Text)), DateAdd(DateInterval.Month, 1, CDate("01/" & MonthName(cmbMonthFrom.SelectedValue) & "/" & cmbYearFrom.SelectedItem.Text))), "yyyy,MM,dd") & ")"
                '        RepVars.strFormula = RepVars.strFormula & " |" & "TMONTH^" & "Date(" & Format(DateAdd(DateInterval.Day, -Day(CDate("01/" & MonthName(cmbMonthTo.SelectedValue) & "/" & cmbYearTo.SelectedItem.Text)), DateAdd(DateInterval.Month, 1, CDate("01/" & MonthName(cmbMonthTo.SelectedValue) & "/" & cmbYearTo.SelectedItem.Text))), "yyyy,MM,dd") & ")"
                '    End If 
                ''''End If

                If Chk(dtRpt.Rows(0).Item("RptQuery")) <> "" Then
                    RepVars.IsParam = True
                Else
                    RepVars.IsParam = False
                End If
                '***************** Passing Parameters For Groups********************8
                If Not Report Is Nothing Then
                    If CType(Report.FindControl("cmbOrdBy"), DropDownList).Items.Count > 0 Then
                        RepVars.OrderBy = CType(Report.FindControl("cmbOrdBy"), DropDownList).SelectedValue
                        RepVars.totNumGrp = ChkN(dtRpt.Rows(0).Item("Groups"))
                        RepVars.QryType = Chk(dtRpt.Rows(0).Item("QryType"))
                    End If
                End If
            Else
            End If
            HttpContext.Current.Session("RepVars") = RepVars
            SetReport = "Ready"
        Catch ex As Exception
            SetReport = ex.Message & " : (cmdSetValues_Click)"
        Finally
            dtRpt.Dispose()
        End Try

    End Function
    ''Public Function GetReminders(ByVal sCreteria As String, ByRef oDAL As DAL.DataLayer, ByRef oUser As DAL.DataLayer.Users, Optional ByRef Report As FrmReports = Nothing)
    ''    Dim dtRem As New DataTable
    ''    Dim strSql As String
    ''    strSql = "SELECT SrNo FROM mstReports WHERE SrNo "
    ''End Function
#End Region

#Region "   SCM Related Functions  "
    Public Function GetSCMPayroll(ByRef oDAL As DAL.DataLayer, ByRef argControl As Object, ByVal oUser As DAL.DataLayer.Users) As SCM.Payroll
        Try

            Dim objSCM As New SCM.Payroll(oDAL)

            objSCM.FY_Start = CDate(FY_Start)
            objSCM.LY_Start = CDate(LY_Start)
            objSCM.RY_Start = CDate(RY_Start)
            SetMsg(argControl, objSCM.ErrMessage)
            GetSCMPayroll = objSCM
        Catch ex As Exception
            SetMsg(argControl, ex.Message & " (GetSCMPayroll)")
        End Try
    End Function
#End Region
#Region "   User Access Related Functions   "
    Public Function CheckRight(ByVal SrNo As Int32) As AccessType
        'By Ravi on 11 dec 2006
        Dim Sess As System.Web.SessionState.HttpSessionState
        Dim sno As String = "," + SrNo.ToString + "-"
        Sess = HttpContext.Current.Session
        If CType(Sess("LoginUser"), DAL.DataLayer.Users).UserModules <> "" Then
            'Dim STR As String = CType(Sess("LoginUser"), DAL.DataLayer.Users).UserModules
            If InStr(CType(Sess("LoginUser"), DAL.DataLayer.Users).UserModules, sno) <> 0 Then
                Dim int As Int16, st As String
                int = InStr(CType(Sess("LoginUser"), DAL.DataLayer.Users).UserModules, sno)
                st = Right(Mid(CType(Sess("LoginUser"), DAL.DataLayer.Users).UserModules, int + 1, Len(SrNo & "-") + 1), 1)
                If st = "S" Then
                    CheckRight = AccessType.FullAccess
                Else
                    CheckRight = AccessType.ReadonlyAccess
                End If
                'Comment By Ravi
                'Dim Sess As System.Web.SessionState.HttpSessionState
                'Sess = HttpContext.Current.Session
                'If CType(Sess("LoginUser"), DAL.DataLayer.Users).UserModules <> "" Then
                '    If InStr(CType(Sess("LoginUser"), DAL.DataLayer.Users).UserModules, SrNo.ToString & "-") <> 0 Then
                '        Dim int As Int16, st As String
                '        int = InStr(CType(Sess("LoginUser"), DAL.DataLayer.Users).UserModules, SrNo)
                '        st = Right(Mid(CType(Sess("LoginUser"), DAL.DataLayer.Users).UserModules, int, Len(SrNo & "-") + 1), 1)
                '        If st = "S" Then
                '            CheckRight = AccessType.FullAccess
                '        Else
                '            CheckRight = AccessType.ReadonlyAccess
                '        End If
            Else
                CheckRight = AccessType.Restricted
            End If
        Else
            If CType(Sess("LoginUser"), DAL.DataLayer.Users).UserID.Trim = "EDP" Then
                CheckRight = AccessType.SuperUser
            Else
                CheckRight = AccessType.Restricted
            End If
        End If
    End Function

#End Region
End Module
