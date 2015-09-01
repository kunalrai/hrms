Option Explicit On 
Imports BAL.BLayer
Imports FoxServer.FoxApplicationClass
Public Class Payroll
    Private strErrMsg As String
    Private oBAl As BAL.BLayer
    Private oDAL As DAL.DataLayer
    Private strSql As String
    Private i, j, k As Int16
    Private dPayDate As Date
    Private dFY_START As Date
    Private dFY_END As Date
    Private dRY_START As Date
    Private dRY_END As Date
    Private dLY_START As Date
    Private dLY_END As Date
    Private rsVarMast As DataTable
    Private rsVarMast1 As DataTable
    Public FoxApp As FoxServer.FoxApplicationClass
    Private Const BackEnd As String = "SQLSERVER"
#Region "  Public properties  "
    Public ReadOnly Property ErrMessage()
        Get
            ErrMessage = strErrMsg
        End Get
    End Property
    Public WriteOnly Property FY_Start() As Date
        Set(ByVal Value As Date)
            dFY_START = Value
            dFY_END = Value.AddYears(1).AddDays(-1)
        End Set
    End Property
    Public WriteOnly Property RY_Start() As Date
        Set(ByVal Value As Date)
            dRY_START = Value
            dRY_END = Value.AddYears(1).AddDays(-1)
        End Set
    End Property
    Public WriteOnly Property LY_Start() As Date
        Set(ByVal Value As Date)
            dLY_START = Value
            dLY_END = Value.AddYears(1).AddDays(-1)
        End Set
    End Property
#End Region
#Region "  Constructor and Destructor  "
    Public Sub New(ByRef objDal As DAL.DataLayer, ByRef objBal As BAL.BLayer)
        oBAl = objBal
        oDAL = objDal
    End Sub
    Public Sub New(ByRef objDal As DAL.DataLayer)
        oDAL = objDal
    End Sub
#End Region
#Region "   Supporting Functions (Non Database Access)  "
    Private Sub SetErrMessage(ByVal Ex As Exception, ByVal sProcName As String)
        strErrMsg = "SCM.Payroll--" & sProcName & vbCrLf & Ex.Source & vbCrLf & Ex.Message
    End Sub
    Public Function MonDiff(ByVal Fdt As Date, ByVal Tdt As Date) As Double
        Dim MonDFdt, MonDTdt As Int32
        Try
            MonDiff = 0
            If IsDBNull(Fdt) Or IsDBNull(Tdt) Then Exit Function
            If Fdt < Tdt Then
                MonDFdt = Fdt.AddMonths(1).AddDays(Fdt.AddMonths(1).Day * -1).Day
                MonDTdt = Tdt.AddMonths(1).AddDays(Tdt.AddMonths(1).Day * -1).Day
                Do While DateAdd("m", MonDiff, Fdt) <= Tdt
                    MonDiff = MonDiff + 1
                Loop
                MonDiff = MonDiff - CInt(IIf(Day(Tdt) < Day(Fdt), 1, 2)) _
                                  + (MonDFdt - Day(Fdt) + 1) / MonDFdt _
                                  + Day(Tdt) / MonDTdt
            End If
        Catch ex As Exception
            SetErrMessage(ex, "MonDiff")
        End Try
    End Function
    Private Function EOM(ByVal DateValue As Date) As Date
        EOM = DateValue.AddDays(DateValue.Day * -1).AddDays(1).AddMonths(1).AddDays(-1)
    End Function
    Private Function BOM(ByVal DateValue As Date) As Date
        BOM = DateValue.AddDays(DateValue.Day * -1).AddDays(1)
    End Function
    Public Function Prorate(ByVal DiffAmt As Double, ByVal Prorata As Double, ByVal FromDate As Date, ByVal ToDate As Date, ByVal Rated As Integer, ByVal StartMonth As Integer) As Double
        Dim A As Integer, StDt As Date, StartDt As Date, EndDt As Date, WEF_Date As Date
        Dim MonLeft As Double
        A = CType(IIf(Rated = 1, 1, IIf(Rated = 2, 3, IIf(Rated = 3, 6, 12))), Int32)
        StDt = DateAdd(DateInterval.Month, StartMonth - Month(FromDate), FromDate.AddDays(FromDate.Day * -1).AddDays(1))
        StDt = CType(IIf(StDt > FromDate, DateAdd(DateInterval.Month, -12, StDt), StDt), Date)
        For i = 1 To CType(12 / A, Int16)
            StartDt = DateAdd("m", A * (i - 1), StDt)
            EndDt = StDt.AddMonths(A * i).AddDays(-1) ' DateAdd("m", A * i, StDt) - 1
            If Betw(xMax(StDt, FromDate), StartDt, EndDt) Then
                MonLeft = MonDiff(CType(xMax(StDt, FromDate), Date), CType(xMin(ToDate, EndDt), Date))
                Prorata = Prorata + DiffAmt / A * MonLeft
                Exit For
            End If
        Next
        Prorate = Prorata
    End Function
    Private Function xMax(ByVal x1 As Object, ByVal x2 As Object, Optional ByVal x3 As Object = Nothing, Optional ByVal x4 As Object = Nothing, Optional ByVal x5 As Object = Nothing, Optional ByVal x6 As Object = Nothing, Optional ByVal x7 As Object = Nothing, Optional ByVal x8 As Object = Nothing, Optional ByVal x9 As Object = Nothing, Optional ByVal x10 As Object = Nothing) As Object
        On Error Resume Next
        If x1 > x2 Then
            xMax = x1
        Else
            xMax = x2
        End If
        If Not IsNothing(x3) Then xMax = IIf(x3 > xMax, x3, xMax)
        If Not IsNothing(x4) Then xMax = IIf(x4 > xMax, x4, xMax)
        If Not IsNothing(x5) Then xMax = IIf(x5 > xMax, x5, xMax)
        If Not IsNothing(x6) Then xMax = IIf(63 > xMax, x6, xMax)
        If Not IsNothing(x7) Then xMax = IIf(x7 > xMax, x7, xMax)
        If Not IsNothing(x8) Then xMax = IIf(x8 > xMax, x8, xMax)
        If Not IsNothing(x9) Then xMax = IIf(x9 > xMax, x9, xMax)
        If Not IsNothing(x10) Then xMax = IIf(x10 > xMax, x10, xMax)
    End Function
    Private Function xMin(ByVal x1, ByVal x2, Optional ByVal x3 = Nothing, Optional ByVal x4 = Nothing, Optional ByVal x5 = Nothing, Optional ByVal x6 = Nothing, Optional ByVal x7 = Nothing, Optional ByVal x8 = Nothing, Optional ByVal x9 = Nothing, Optional ByVal x10 = Nothing)
        On Error Resume Next
        If x1 < x2 Then
            xMin = x1
        Else
            xMin = x2
        End If
        If Not IsNothing(x3) Then xMin = IIf(x3 < xMin, x3, xMin)
        If Not IsNothing(x4) Then xMin = IIf(x4 < xMin, x4, xMin)
        If Not IsNothing(x5) Then xMin = IIf(x5 < xMin, x5, xMin)
        If Not IsNothing(x6) Then xMin = IIf(63 < xMin, x6, xMin)
        If Not IsNothing(x7) Then xMin = IIf(x7 < xMin, x7, xMin)
        If Not IsNothing(x8) Then xMin = IIf(x8 < xMin, x8, xMin)
        If Not IsNothing(x9) Then xMin = IIf(x9 < xMin, x9, xMin)
        If Not IsNothing(x10) Then xMin = IIf(x10 < xMin, x10, xMin)
    End Function
    Private Function MinV(ByVal ParamArray ValuesArray())
        On Error Resume Next
        Dim ValNo As Integer
        MinV = ValuesArray(0)
        For ValNo = 0 To UBound(ValuesArray, 1)
            If ValuesArray(ValNo) < MinV Then MinV = ValuesArray(ValNo)
        Next
    End Function
    Private Function MaxV(ByVal ParamArray ValuesArray())
        On Error Resume Next
        Dim ValNo As Integer
        MaxV = ValuesArray(0)
        For ValNo = 0 To UBound(ValuesArray, 1)
            If ValuesArray(ValNo) > MaxV Then MaxV = ValuesArray(ValNo)
        Next
    End Function
    Private Function Betw(ByVal vVal, ByVal LowrLmt, ByVal UpperLmt) As Boolean
        On Error Resume Next
        If vVal >= LowrLmt And vVal <= UpperLmt Then
            Betw = True
        Else
            Betw = False
        End If
    End Function
#End Region
#Region "  Paysetup & Reimbursement Related Functions  "
    Public Function FindField(ByVal TableName As String, ByVal FieldName As String) As Boolean
        Try
            strSql = "SELECT " & FieldName & " FROM " & TableName
            oDAL.ExecuteCommand(strSql, DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar)
            FindField = True
        Catch ex As Exception
            FindField = False
        End Try
    End Function

    Public Function AddField(ByVal TableName As String, ByVal FieldName As String, ByVal FieldType As String, ByVal FieldLen As Integer, ByVal FieldDec As Integer) As Boolean
        strErrMsg = ""
        Try
            If FindField(TableName, FieldName) Then
                strSql = "ALTER TABLE " & TableName & " ALTER COLUMN " & FieldName & " "
            Else
                strSql = "ALTER TABLE " & TableName & " ADD " & FieldName & " "
            End If
            Select Case UCase(BackEnd)
                Case "ORACLE"
                    Select Case FieldType
                        Case "D"
                            strSql = strSql & "Date"
                        Case "N"
                            strSql = strSql & "Number (" & FieldLen & "," & FieldDec & ")"
                        Case "M"
                            strSql = strSql & "VarChar2 (2000)"
                        Case Else
                            strSql = strSql & "VarChar2 (" & FieldLen & ")"
                    End Select
                Case "SQLSERVER"
                    Select Case FieldType
                        Case "D"
                            strSql = strSql & "SmallDateTime"
                        Case "N"
                            If FieldDec = 0 Then
                                strSql = strSql & "Int"
                            Else
                                strSql = strSql & "Numeric (" & FieldLen & "," & FieldDec & ")"
                            End If
                        Case "M"
                            strSql = strSql & "NText"
                        Case Else
                            strSql = strSql & "NVarChar (" & FieldLen & ")"
                    End Select
                Case Else
                    Select Case FieldType
                        Case "D"
                            strSql = strSql & "Date"
                        Case "N"
                            strSql = strSql & "Number (" & FieldLen & "," & FieldDec & ")"
                        Case "M"
                            strSql = strSql & "VarChar (2000)"
                        Case Else
                            strSql = strSql & "VarChar (" & FieldLen & ")"
                    End Select
            End Select
            oDAL.ExecuteCommand(strSql, DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteNonQuery)
            AddField = True
        Catch ex As Exception
            AddField = False
            SetErrMessage(ex, "AddField")
        End Try
    End Function


    Public Function calcLwopAmt(ByVal Amt As Double, ByVal FromDt As Date, ByVal ToDt As Date, ByVal EmpCd As String, Optional ByVal bGetMasterRate As Boolean = False, Optional ByVal FldName As String = "", Optional ByVal dWEFDate As Object = Nothing) As Double
        Dim i, j, lwpAmt As Integer
        Dim StrSql As String
        Dim rsTmpDays As New DataTable
        Try
            j = MonDiff(FromDt, ToDt)
            If IsNothing(dWEFDate) Then dWEFDate = FromDt
            For i = 0 To j - 1
                StrSql = "SELECT ISNULL(LWOP,0) AS LWOP,ISNULL(MONDAYS,0) as MONDAYS FROM PAYHIST WHERE EMP_CODE = '" & EmpCd & "' AND PAYDATE = '" & Format(EOM(DateAdd("m", i, FromDt)), "dd/mmm/yyyy") & "'"
                oDAL.GetSqlDataTable(rsTmpDays, StrSql)
                If Not rsTmpDays.Rows.Count = 0 Then
                    If Not rsTmpDays.Rows(0).Item("MONDAYS") = 0 Then
                        If bGetMasterRate = False Then
                            calcLwopAmt = calcLwopAmt + ((Amt / 12) / rsTmpDays.Rows(0).Item("MONDAYS")) * rsTmpDays.Rows(0).Item("LWOP")
                        Else
                            If dRY_START < dWEFDate Then
                                calcLwopAmt = calcLwopAmt + ((GetMasterReimRate(EmpCd, EOM(FromDt.AddMonths(1)), FldName, Amt, EOM(FromDt.AddMonths(1))) / 12) / rsTmpDays.Rows(0).Item("MONDAYS")) * rsTmpDays.Rows(0).Item("LWOP")
                            Else
                                calcLwopAmt = calcLwopAmt + ((Amt / 12) / rsTmpDays.Rows(0).Item("MONDAYS")) * rsTmpDays.Rows(0).Item("LWOP")
                            End If
                        End If
                    End If
                End If
            Next
            calcLwopAmt = Math.Round(calcLwopAmt, 2)
        Catch ex As Exception
            SetErrMessage(ex, "MonDiff")
        Finally
            rsTmpDays.Dispose()
        End Try
    End Function
    Public Function GetMasterReimRate(ByVal Code As String, ByVal Paydate As Date, ByVal Field_Name As String, ByVal DefaultVal As Double, ByVal CurMonth As Date) As Double
        Dim dtArr As New DataTable
        Dim dtFrom, dtTo, dtPrev As Date
        Dim Amount, PrevAmt As Double
        Try
            dtFrom = BOM(CurMonth)
            dtTo = EOM(CurMonth)
            strSql = "SELECT * FROM ARREAR WHERE Emp_Code='" & Code & "' AND Field_Name='" & Field_Name & "' AND FLD_WEF<='" & Format(CurMonth, "dd/mmm/yyyy") & "' AND  PayDate<='" & Format(Paydate, "dd/mmm/yyyy") & "' ORDER BY Fld_Wef"
            oDAL.GetSqlDataTable(dtArr, strSql)
            If dtArr.Rows.Count = 0 Then
                GetMasterReimRate = DefaultVal
            Else
                dtPrev = BOM(dtFrom)
                PrevAmt = DefaultVal
                For i = 0 To dtArr.Rows.Count - 1
                    If dtArr.Rows(i).Item("Fld_WEF") < dtFrom Then
                        Amount = dtArr.Rows(i).Item("Cur_Amt")
                        GetMasterReimRate = Amount
                    ElseIf dtArr.Rows(i).Item("Fld_WEF") >= dtFrom Then
                        If dtPrev < dtFrom Then

                            GetMasterReimRate = ((DateDiff(DateInterval.Day, CDate(dtArr.Rows(i).Item("Fld_WEF")), dtFrom)) / Day(dtTo)) * PrevAmt
                        Else
                            GetMasterReimRate = GetMasterReimRate + (DateDiff(DateInterval.Day, CDate(dtArr.Rows(i).Item("Fld_WEF")), dtPrev) / Day(dtTo)) * PrevAmt
                        End If
                    End If
                    PrevAmt = dtArr.Rows(i).Item("Cur_Amt")
                    dtPrev = dtArr.Rows(i).Item("Fld_WEF")
                Next
                If dtPrev >= dtFrom Then
                    GetMasterReimRate = GetMasterReimRate + ((DateDiff(DateInterval.Day, dtTo, dtPrev) + 1) / Day(dtTo)) * PrevAmt
                End If
            End If
        Catch ex As Exception
            SetErrMessage(ex, "GetMasterReimRate")
        End Try
    End Function
#End Region
#Region "  Pay calculation Related Functions  "
    Public Function CheckFox() As Int16
        Dim ObjFox As FoxServer.FoxApplicationClass
        Try
            ObjFox = New FoxServer.FoxApplicationClass
            CheckFox = ObjFox.Eval("1+1")
        Catch ex As Exception
            SetErrMessage(ex, "CheckFox")
        Finally
            ObjFox.Quit()
        End Try
    End Function
    Private Function GetVarMast(ByRef dtVarMast As DataTable, ByRef rsVarMast As DataTable, ByRef rsVarMast1 As DataTable) As DataTable
        Dim dcCol As DataColumn
        Dim dr, dr1 As DataRow
        Dim rsPaySetup As New DataTable
        Try
            oDAL.GetSqlDataTable(rsPaySetup, "SELECT * FROM PaySetup ORDER BY SNO")
            For i = 0 To rsPaySetup.Rows.Count - 1
                dr = rsPaySetup.Rows(i)
                'Debug.WriteLine(rsPaySetup.Rows(i).Item("Field_Name"))
                Select Case dr.Item("Field_Type")
                    Case "C"
                        dcCol = New DataColumn(dr.Item("Field_Name"), System.Type.GetType("System.String"))
                        dcCol.MaxLength = dr.Item("Field_Len")
                        rsVarMast1.Columns.Add(dcCol)
                    Case "N"
                        dcCol = New DataColumn(dr.Item("Field_Name"), System.Type.GetType("System.Double"))
                        rsVarMast.Columns.Add(dcCol)
                    Case "D"
                        dcCol = New DataColumn(dr.Item("Field_Name"), System.Type.GetType("System.DateTime"))
                        rsVarMast1.Columns.Add(dcCol)
                End Select
            Next
            dr = rsVarMast.NewRow
            dr1 = rsVarMast1.NewRow
            For i = 0 To dtVarMast.Rows.Count - 1
                Select Case dtVarMast.Rows(i).Item("Field_Type")
                    Case "C"
                        dr1.Item(dtVarMast.Rows(i).Item("Field_Name")) = Mid(ChkS(dtVarMast.Rows(i).Item("Field_C")), 1, dtVarMast.Rows(i).Item("Field_Len"))
                    Case "N"
                        dr.Item(dtVarMast.Rows(i).Item("Field_Name")) = ChkN(dtVarMast.Rows(i).Item("Field_N"))
                    Case "D"
                        dr1.Item(dtVarMast.Rows(i).Item("Field_Name")) = dtVarMast.Rows(i).Item("Field_D")
                End Select
            Next
            rsVarMast.Rows.Add(dr)
            rsVarMast1.Rows.Add(dr1)
        Catch ex As Exception
            SetErrMessage(ex, "GetVarMAst")
        Finally

        End Try
    End Function
    Private Function WriteBackVals(ByRef dtVarMast As DataTable, ByRef rsVarMast As DataTable, ByRef rsVarMast1 As DataTable)
        Dim dr As DataRow
        For i = 0 To dtVarMast.Rows.Count - 1
            dr = dtVarMast.Rows(i)
            Select Case dr.Item("Field_Type")
                Case "C"
                    dtVarMast.Rows(i).Item("Field_C") = rsVarMast1.Rows(0).Item(dtVarMast.Rows(i).Item("Field_Name"))
                Case "N"
                    dtVarMast.Rows(i).Item("Field_N") = rsVarMast.Rows(0).Item(dtVarMast.Rows(i).Item("Field_Name"))
                Case "D"
                    dtVarMast.Rows(i).Item("Field_D") = rsVarMast1.Rows(0).Item(dtVarMast.Rows(i).Item("Field_Name"))
            End Select
            dtVarMast.Rows(i).AcceptChanges()            
        Next
        dtVarMast.AcceptChanges()
    End Function
    Public Sub MstFldCal(ByRef dtVarMast As DataTable, Optional ByVal FldName As String = "")

        Dim FldTypex, FldCalc, FldValid As String
        Dim FldRated, FldMonth As Int16
        Dim FldRnd, FldRnd_M As Double
        Dim rsPayCal As New DataTable
        Dim drPayCal As DataRow
        Dim FldValue, FldCalVal As Object
        Try
            rsVarMast = New DataTable
            rsVarMast1 = New DataTable
            'mukesh
            '
            'Dim a As New FoxServer.FoxApplicationClass



            FoxApp = New FoxServer.FoxApplicationClass

            GetVarMast(dtVarMast, rsVarMast, rsVarMast1)
            oDAL.GetSqlDataTable(rsPayCal, "SELECT PaySetup.SNo, Formula.*, Field_Type, Fld_Categ, Fld_Month, Fld_Rated  FROM Formula INNER JOIN PaySetup ON Formula.Field_Name=PaySetup.Field_Name INNER JOIN (SELECT Field_Name,Max(Fld_Date) as Fld_Date FROM FORMULA GROUP BY Field_Name) as QRY ON Qry.Field_Name=Formula.Field_Name AND Qry.Fld_date=Formula.Fld_Date WHERE  (Fld_PayMast='Y' Or Fld_Categ=4) And Fld_Categ In(1,2,4,5,6) ORDER BY PaySetup.SNo, PaySetup.Field_Name, Formula.Fld_Date DESC")
            For i = 0 To rsPayCal.Rows.Count - 1
                drPayCal = rsPayCal.Rows(i)

                FldName = drPayCal.Item("Field_Name")
                If drPayCal.Item("Field_Type") = "N" Then
                    FldValue = rsVarMast.Rows(0).Item(Trim(drPayCal.Item("Field_Name")))
                Else
                    FldValue = rsVarMast1.Rows(0).Item(Trim(drPayCal.Item("Field_Name")))
                End If
                FldTypex = ChkS(drPayCal.Item("Field_Type"))
                FldCalc = ChkS(drPayCal.Item("Fld_Calc"))
                FldValid = ChkS(drPayCal.Item("Fld_Valid"))
                FldRated = ChkN(drPayCal.Item("Fld_Rated"))
                FldMonth = ChkN(drPayCal.Item("Fld_Month"))
                FldRnd = ChkN(drPayCal.Item("Fld_Rnd"))
                FldRnd_M = ChkN(drPayCal.Item("Fld_Rnd_M"))
                FldCalVal = FldCal(FldName, FldTypex, FldValue, FldCalc, FldValid, FldRated, FldMonth, FldRnd, FldRnd_M, "P")
                If ChkS(drPayCal.Item("Field_Type")) = "N" Then
                    rsVarMast.Rows(0).Item(ChkS(drPayCal.Item("Field_Name"))) = IIf(IsNothing(FldCalVal), DBNull.Value, FldCalVal)
                Else
                    rsVarMast1.Rows(0).Item(ChkS(drPayCal.Item("Field_Name"))) = IIf(IsNothing(FldCalVal), DBNull.Value, FldCalVal)
                End If
            Next
            WriteBackVals(dtVarMast, rsVarMast, rsVarMast1)
        Catch ex As Exception

            SetErrMessage(ex, "MstFldCal")
        Finally
            rsPayCal.Dispose()
            rsPayCal = Nothing
            rsVarMast.Rows(0).AcceptChanges()
            rsVarMast1.Rows(0).AcceptChanges()
            rsVarMast.AcceptChanges()
            rsVarMast1.AcceptChanges()
            FoxApp.Quit()
            FoxApp = Nothing
        End Try
    End Sub
    Private Function FldCal(ByRef FldName As String, ByRef FldType As String, ByRef FldValue As Object, ByRef FldFormula As String, ByRef FldValid As String, ByRef FldRated As Integer, ByRef FldMonth As Integer, ByRef FldRndType As Integer, ByRef FldRndMultiple As Double, ByRef FldTable As String) As Object
        Dim CalFlg As Boolean
        Try
            If FldName = "DEDU_80CCC" Then
                MsgBox("HELL")
            End If
            If Trim(FldValid) <> "" Then
                CalFlg = Eval(UCase(FldValid), "L", 1, 1, 1, 1, "")
            Else
                CalFlg = True
            End If
            If CalFlg Then
                If Not ChkS(FldFormula) = "" Then
                    FldCal = Eval(UCase(FldFormula), FldType, FldRated, FldMonth, FldRndType, FldRndMultiple, FldTable)
                End If
            Else
                FldCal = System.DBNull.Value
            End If
        Catch ex As Exception
            SetErrMessage(ex, "FldCal")
        Finally
        End Try
    End Function
    Public Function Eval(ByRef expr As String, ByRef ExprType As String, ByRef FldRated As Integer, ByRef FldMonth As Integer, ByRef FldRndType As Integer, ByRef FldRndMultiple As Double, ByRef FldTable As String, Optional ByRef rsFieldsValues As DataTable = Nothing) As Object
        Eval = Evaluate(expr, ExprType, FldRated, FldMonth, FldRndType, FldRndMultiple, FldTable, rsFieldsValues)
        Exit Function
    End Function
    Public Function ChkType(ByVal FieldType As String) As System.Data.DbType
        'On Error Resume Next
        'If VarType(FieldType) = vbString Then
        FieldType = IIf(FieldType = "N", "System.Double", IIf(FieldType = "D", "System.DateTime", IIf(FieldType = "L", "System.Boolean", IIf(FieldType = "C", "System.String", FieldType))))
        'End If

        Select Case FieldType
            Case "System.Int16", "System.Int32", "System.Int64", "System.Decimal", "System.Double"
                ChkType = DbType.Double ' Numbers
                'Case DbType.Date, DbType.DateTime, System.Type.GetType("System.DateTime") ' Date
            Case "System.DateTime" ' Date    
                ChkType = DbType.Date
                'Case DbType.Boolean, System.Type.GetType("System.Boolean") ' Boolean
            Case "System.Boolean" ' Boolean
                ChkType = DbType.Boolean
            Case Else   'Character or Memo
                ChkType = DbType.String
        End Select
    End Function

    Public Function ConvertFoxFormat(ByVal FieldValue As Object, ByVal FieldType As String)
        'On Error Resume Next
        FieldValue = IIf(IsDBNull(FieldValue), Nothing, FieldValue)
        Select Case ChkType(FieldType)
            Case DbType.Double 'Number
                ConvertFoxFormat = IIf(FieldValue Is Nothing, 0, FieldValue)
            Case DbType.Date ' Date
                ConvertFoxFormat = "#^" & IIf(FieldValue Is Nothing, "", Format(FieldValue, "yyyy/MM/dd")) & "#"
            Case DbType.Boolean 'Logical
                ConvertFoxFormat = IIf(FieldValue Is Nothing, ".F.", ".T.")
            Case Else   'Character
                ConvertFoxFormat = "'" & IIf(IsDBNull(FieldValue), "", FieldValue) & "'"
        End Select
    End Function
    Public Function ConvertVBFormat(ByVal FieldValue As Object, ByVal FieldType As Object)
        'On Error Resume Next
        FieldValue = IIf(IsDBNull(FieldValue), Nothing, FieldValue)
        Select Case ChkType(FieldType)
            Case DbType.Double 'Number
                ConvertVBFormat = IIf(FieldValue Is Nothing, 0, FieldValue)
            Case DbType.Date ' Date
                ConvertVBFormat = IIf(FieldValue Is Nothing, DBNull.Value, CDate(FieldValue))
            Case DbType.Boolean 'Logical
                ConvertVBFormat = IIf(IsNothing(FieldValue), False, FieldValue)
            Case Else   'Character
                ConvertVBFormat = IIf(FieldValue Is Nothing, DBNull.Value, FieldValue)
        End Select
    End Function
    Private Function ChkField(ByVal rsFields As DataTable, ByVal FieldName As String) As Boolean
        ChkField = rsFields.Columns.Contains(FieldName)
    End Function

    Private Function Evaluate(ByRef ExpString As String, ByRef ExpType As String, Optional ByRef FldRated As Integer = 1, Optional ByRef FldMonth As Integer = 4, Optional ByRef RndOffType As Integer = 2, Optional ByRef RndOffMultiple As Double = 1.0, Optional ByRef FldTable As String = "", Optional ByRef rsFieldsValues As DataTable = Nothing, Optional ByRef FieldForExp As String = "")
        Dim FieldName As String, FieldValue As Object, StrPos1 As Integer, StrPos2 As Integer, _
            Para1 As Object, Para2 As Object, Para3 As Object, Para4 As Object, _
            Para5 As Object, Para6 As Object

        Try
            If Trim(ExpString) = "" Then
                Exit Function
            End If


            'Replace Junk Character
            ExpString = Replace(Replace(ExpString, Chr(13), ""), Chr(10), "")

            'Replace FY_Start, FY_End, RY_Start, RY_End, LY_Start, LY_End
            If InStr(ExpString, "{@PAYDATE}") > 0 Then ExpString = Replace(ExpString, "{@PAYDATE}", ConvertFoxFormat(dPayDate, "D"))
            If InStr(ExpString, "{@FY_START}") > 0 Then ExpString = Replace(ExpString, "{@FY_START}", ConvertFoxFormat(dFY_START, "D"))
            If InStr(ExpString, "{@FY_END}") > 0 Then ExpString = Replace(ExpString, "{@FY_END}", ConvertFoxFormat(dFY_END, "D"))
            If InStr(ExpString, "{@RY_START}") > 0 Then ExpString = Replace(ExpString, "{@RY_START}", ConvertFoxFormat(dRY_START, "D"))
            If InStr(ExpString, "{@RY_END}") > 0 Then ExpString = Replace(ExpString, "{@RY_END}", ConvertFoxFormat(dRY_END, "D"))
            If InStr(ExpString, "{@LY_START}") > 0 Then ExpString = Replace(ExpString, "{@LY_START}", ConvertFoxFormat(dLY_START, "D"))
            If InStr(ExpString, "{@LY_END}") > 0 Then ExpString = Replace(ExpString, "{@LY_END}", ConvertFoxFormat(dLY_END, "D"))

            'Replace MonDiff
            Do While True
                StrPos1 = InStr(ExpString, "MONDIFF([")
                If StrPos1 = 0 Then Exit Do
                StrPos2 = InStr(StrPos1 + 9, ExpString, "]")
                Para1 = Mid(ExpString, StrPos1 + 9, StrPos2 - StrPos1 - 9)
                Para1 = Evaluate(Para1, "D")
                StrPos1 = InStr(StrPos2 + 1, ExpString, "[")
                StrPos2 = InStr(StrPos1 + 1, ExpString, "]")
                Para2 = Mid(ExpString, StrPos1 + 1, StrPos2 - StrPos1 - 1)
                Para2 = Evaluate(Para2, "D")
                StrPos1 = InStr(ExpString, "MONDIFF(")
                If VarType(Para1) = vbDate And VarType(Para2) = vbDate Then
                    FieldValue = MonDiff(Para1, Para2)
                Else
                    FieldValue = 0
                End If
                FieldValue = IIf(IsNothing(FieldValue), 0, FieldValue)
                ExpString = Mid(ExpString, 1, StrPos1 - 1) & FieldValue & Mid(ExpString, StrPos2 + 2)
                FieldValue = Nothing
            Loop

            'Replace EOM() End of the Month
            Do While True
                StrPos1 = InStr(ExpString, "EOM([")
                If StrPos1 = 0 Then Exit Do
                StrPos2 = InStr(StrPos1 + 1, ExpString, "]")
                Para1 = Mid(ExpString, StrPos1 + 5, StrPos2 - StrPos1 - 5)
                Para1 = Evaluate(Para1, "D")
                If VarType(Para1) = vbDate Then FieldValue = EOM(Para1)
                FieldValue = ConvertFoxFormat(FieldValue, "D")
                ExpString = Mid(ExpString, 1, StrPos1 - 1) & FieldValue & Mid(ExpString, StrPos2 + 2)
                FieldValue = Nothing
            Loop

            'Replace BOM() Begning of the Month
            Do While True
                StrPos1 = InStr(ExpString, "BOM([")
                If StrPos1 = 0 Then Exit Do
                StrPos2 = InStr(StrPos1 + 1, ExpString, "]")
                Para1 = Mid(ExpString, StrPos1 + 5, StrPos2 - StrPos1 - 5)
                Para1 = Evaluate(Para1, "D")
                If VarType(Para1) = vbDate Then FieldValue = BOM(Para1)
                FieldValue = ConvertFoxFormat(FieldValue, "D")
                ExpString = Mid(ExpString, 1, StrPos1 - 1) & FieldValue & Mid(ExpString, StrPos2 + 2)
                FieldValue = Nothing
            Loop

            'Replace GetTrn
            Do While True
                StrPos1 = InStr(ExpString, "GETTRN([")
                If StrPos1 = 0 Then Exit Do
                StrPos2 = InStr(StrPos1 + 8, ExpString, "]")
                Para1 = Mid(ExpString, StrPos1 + 8, StrPos2 - StrPos1 - 8)
                If InStr(StrPos2 + 1, ExpString, "[") = 0 Or InStr(StrPos2 + 1, ExpString, "[") > InStr(StrPos2 + 1, ExpString, ")") Then
                    FieldValue = GetTrn("A", Para1)
                Else
                    StrPos1 = InStr(StrPos2 + 1, ExpString, "[")
                    StrPos2 = InStr(StrPos1 + 1, ExpString, "]")
                    Para2 = Mid(ExpString, StrPos1 + 1, StrPos2 - StrPos1 - 1)
                    If InStr(StrPos2 + 1, ExpString, "[") = 0 Or InStr(StrPos2 + 1, ExpString, "[") > InStr(StrPos2 + 1, ExpString, ")") Then
                        FieldValue = GetTrn(Para1, Para2)
                    Else
                        StrPos1 = InStr(StrPos2 + 1, ExpString, "[")
                        StrPos2 = InStr(StrPos1 + 1, ExpString, "]")
                        Para3 = Mid(ExpString, StrPos1 + 1, StrPos2 - StrPos1 - 1)
                        If InStr(StrPos2 + 1, ExpString, "[") = 0 Or InStr(StrPos2 + 1, ExpString, "[") > InStr(StrPos2 + 1, ExpString, ")") Then
                            FieldValue = GetTrn(Para1, Para2, Para3)
                        Else
                            StrPos1 = InStr(StrPos2 + 1, ExpString, "[")
                            StrPos2 = InStr(StrPos1 + 1, ExpString, "]")
                            Para4 = Mid(ExpString, StrPos1 + 1, StrPos2 - StrPos1 - 1)
                            FieldValue = GetTrn(Para1, Para2, Para3, Para4)
                        End If
                    End If
                    StrPos1 = InStr(ExpString, "GETTRN([")
                End If
                FieldValue = IIf(IsNothing(FieldValue), 0, FieldValue)
                ExpString = Mid(ExpString, 1, StrPos1 - 1) & FieldValue & Mid(ExpString, StrPos2 + 2)
                FieldValue = Nothing
            Loop

            'Replace GetAtt
            Do While True
                StrPos1 = InStr(ExpString, "GETATT([")
                If StrPos1 = 0 Then Exit Do
                StrPos2 = InStr(StrPos1 + 8, ExpString, "]")
                If StrPos2 - StrPos1 - 8 > 0 Then
                    Para1 = Mid(ExpString, StrPos1 + 8, StrPos2 - StrPos1 - 8)
                Else
                    Para1 = ""
                End If
                If InStr(StrPos2 + 1, ExpString, "[") = 0 Or InStr(StrPos2 + 1, ExpString, "[") > InStr(StrPos2 + 1, ExpString, ")") Then
                    Para2 = ""
                Else
                    StrPos1 = InStr(StrPos2 + 1, ExpString, "[")
                    StrPos2 = InStr(StrPos1 + 1, ExpString, "]")
                    If StrPos2 - StrPos1 - 1 > 0 Then
                        Para2 = Mid(ExpString, StrPos1 + 1, StrPos2 - StrPos1 - 1)
                    Else
                        Para2 = ""
                    End If
                    StrPos1 = InStr(ExpString, "GETATT([")
                End If
                FieldValue = GetAtt(Para1, Para2)
                FieldValue = IIf(IsNothing(FieldValue), 0, FieldValue)
                ExpString = Mid(ExpString, 1, StrPos1 - 1) & FieldValue & Mid(ExpString, StrPos2 + 2)
                FieldValue = Nothing
            Loop

            'Replace PrvAmt
            Do While True
                StrPos1 = InStr(ExpString, "PRVAMT([")
                If StrPos1 = 0 Then Exit Do
                StrPos2 = InStr(StrPos1 + 8, ExpString, "])")
                FieldName = Mid(ExpString, StrPos1 + 8, StrPos2 - StrPos1 - 8)
                FieldValue = PrvAmt(FieldName, FldRated, FldMonth, FldTable)
                If FldTable = "P" Then
                    FieldValue = ConvertFoxFormat(FieldValue, "N")
                Else
                    FieldValue = ConvertFoxFormat(FieldValue, "N")
                End If
                ExpString = Mid(ExpString, 1, StrPos1 - 1) & FieldValue & Mid(ExpString, StrPos2 + 2)
                FieldValue = Nothing
            Loop

            'Replace PrdAmt
            Do While True
                Para1 = Nothing
                Para2 = Nothing
                Para3 = Nothing
                StrPos1 = InStr(ExpString, "PRDAMT([")
                If StrPos1 = 0 Then Exit Do
                StrPos2 = InStr(StrPos1 + 8, ExpString, "]")
                Para1 = Mid(ExpString, StrPos1 + 8, StrPos2 - StrPos1 - 8)
                StrPos1 = InStr(StrPos2 + 1, ExpString, "[")
                If StrPos1 > 0 Then
                    StrPos2 = InStr(StrPos1 + 1, ExpString, "]")
                    Para2 = Evaluate(Mid(ExpString, StrPos1 + 1, StrPos2 - StrPos1 - 1), "D")
                    StrPos1 = InStr(StrPos2 + 1, ExpString, "[")
                    If StrPos1 > 0 Then
                        StrPos2 = InStr(StrPos1, ExpString, "]")
                        Para3 = Evaluate(Mid(ExpString, StrPos1 + 1, StrPos2 - StrPos1 - 1), "D")
                    End If
                End If
                FieldValue = PrdAmt(Para1, FldRated, FldMonth, Para2, Para3)
                FieldValue = IIf(IsNothing(FieldValue), 0, FieldValue)
                StrPos1 = InStr(ExpString, "PRDAMT([")
                ExpString = Mid(ExpString, 1, StrPos1 - 1) & FieldValue & Mid(ExpString, StrPos2 + 2)
                FieldValue = Nothing
            Loop
            'Replace YTDAdj
            Do While True
                StrPos1 = InStr(ExpString, "YTDADJ([")
                If StrPos1 = 0 Then Exit Do
                StrPos2 = InStr(StrPos1 + 8, ExpString, "],")
                Para1 = Mid(ExpString, StrPos1 + 8, StrPos2 - StrPos1 - 8)

                StrPos1 = StrPos2 + 3
                StrPos2 = InStr(StrPos1, ExpString, "],")
                Para2 = Mid(ExpString, StrPos1, StrPos2 - StrPos1)

                StrPos1 = StrPos2 + 3
                StrPos2 = InStr(StrPos1, ExpString, "],")
                Para3 = Mid(ExpString, StrPos1, StrPos2 - StrPos1)

                StrPos1 = StrPos2 + 3
                StrPos2 = InStr(StrPos1, ExpString, "],")
                Para4 = Mid(ExpString, StrPos1, StrPos2 - StrPos1)

                StrPos1 = StrPos2 + 3
                StrPos2 = InStr(StrPos1, ExpString, "],")
                Para5 = Mid(ExpString, StrPos1, StrPos2 - StrPos1)

                StrPos1 = StrPos2 + 3
                StrPos2 = InStr(StrPos1, ExpString, "])")
                Para6 = Mid(ExpString, StrPos1, StrPos2 - StrPos1)

                FieldValue = YTDAdj(Para1, Para2, Para3, Para4, Para5, Para6)
                FieldValue = IIf(IsNothing(FieldValue), 0, FieldValue)
                StrPos1 = InStr(ExpString, "YTDADJ([")
                ExpString = Mid(ExpString, 1, StrPos1 - 1) & FieldValue & Mid(ExpString, StrPos2 + 2)
                FieldValue = Nothing
            Loop

            'Replace GetArr()
            Do While True
                StrPos1 = InStr(ExpString, "GETARR([")
                If StrPos1 = 0 Then Exit Do
                StrPos2 = InStr(StrPos1 + 8, ExpString, "])")
                If StrPos2 - StrPos1 - 8 > 0 Then
                    Para1 = Mid(ExpString, StrPos1 + 8, StrPos2 - StrPos1 - 8)
                Else
                    Para1 = ""
                End If
                FieldValue = GetArr(Para1)
                FieldValue = IIf(IsNothing(FieldValue), 0, FieldValue)
                ExpString = Mid(ExpString, 1, StrPos1 - 1) & FieldValue & Mid(ExpString, StrPos2 + 2)
                FieldValue = Nothing
            Loop
            'Replace GetArr30()
            Do While True
                StrPos1 = InStr(ExpString, "GETARR30([")
                If StrPos1 = 0 Then Exit Do
                StrPos2 = InStr(StrPos1 + 10, ExpString, "])")
                If StrPos2 - StrPos1 - 10 > 0 Then
                    Para1 = Mid(ExpString, StrPos1 + 10, StrPos2 - StrPos1 - 10)
                Else
                    Para1 = ""
                End If
                FieldValue = GetArr30(Para1)
                FieldValue = IIf(IsNothing(FieldValue), 0, FieldValue)
                ExpString = Mid(ExpString, 1, StrPos1 - 1) & FieldValue & Mid(ExpString, StrPos2 + 2)
                FieldValue = Nothing
            Loop

            'Replace GetArrO()
            Do While True
                StrPos1 = InStr(ExpString, "GETARRO([")
                If StrPos1 = 0 Then Exit Do
                StrPos2 = InStr(StrPos1 + 9, ExpString, "])")
                If StrPos2 - StrPos1 - 9 > 0 Then
                    Para1 = Mid(ExpString, StrPos1 + 9, StrPos2 - StrPos1 - 9)
                Else
                    Para1 = ""
                End If
                FieldValue = GetArrO(Para1)
                FieldValue = IIf(IsNothing(FieldValue), 0, FieldValue)
                ExpString = Mid(ExpString, 1, StrPos1 - 1) & FieldValue & Mid(ExpString, StrPos2 + 2)
                FieldValue = Nothing
            Loop

            'Replace GetArr_N()
            Do While True
                StrPos1 = InStr(ExpString, "GETARR_N([")
                If StrPos1 = 0 Then Exit Do
                StrPos2 = InStr(StrPos1 + 10, ExpString, "])")
                If StrPos2 - StrPos1 - 10 > 0 Then
                    Para1 = Mid(ExpString, StrPos1 + 10, StrPos2 - StrPos1 - 10)
                Else
                    Para1 = ""
                End If
                FieldValue = GetArr_N(Para1)
                FieldValue = IIf(IsNothing(FieldValue), 0, FieldValue)
                ExpString = Mid(ExpString, 1, StrPos1 - 1) & FieldValue & Mid(ExpString, StrPos2 + 2)
                FieldValue = Nothing
            Loop

            'Replace LWOP()
            If InStr(ExpString, "LWOP()") > 0 Then
                FieldValue = LWOP()
                ExpString = Replace(ExpString, "LWOP()", FieldValue)
                FieldValue = Nothing
            End If

            'Replace LWOPOLD()
            If InStr(ExpString, "LWOPOLD()") > 0 Then
                FieldValue = LWOP_old()
                ExpString = Replace(ExpString, "LWOPOLD()", FieldValue)
                FieldValue = Nothing
            End If

            'Replace PDDOJ()
            If InStr(ExpString, "PDDOJ()") > 0 Then
                FieldValue = CStr(PD_DOJ())
                ExpString = Replace(ExpString, "PDDOJ()", FieldValue)
                FieldValue = Nothing
            End If

            'Replace Furniture()
            Do While True
                'Dim FDate As Date, TDate As Date
                StrPos1 = InStr(ExpString, "FURNITURE([")
                If StrPos1 = 0 Then Exit Do
                StrPos2 = InStr(StrPos1 + 11, ExpString, "],")
                Para1 = Mid(ExpString, StrPos1 + 11, StrPos2 - StrPos1 - 11)
                StrPos1 = StrPos2 + 3
                StrPos2 = InStr(StrPos1, ExpString, "],")
                Para2 = Mid(ExpString, StrPos1, StrPos2 - StrPos1)
                '**********
                StrPos1 = StrPos2 + 3
                StrPos2 = InStr(StrPos1, ExpString, "])")
                If StrPos2 <> 0 Then
                    Para3 = Mid(ExpString, StrPos1, StrPos2 - StrPos1)
                Else
                    Para3 = ""
                End If
                '*********
                FieldValue = Furniture(Para1, Para2, Para3)
                StrPos1 = InStr(ExpString, "FURNITURE([")
                FieldValue = IIf(IsNothing(FieldValue), 0, FieldValue)
                ExpString = Mid(ExpString, 1, StrPos1 - 1) & FieldValue & Mid(ExpString, StrPos2 + 2)
                FieldValue = Nothing
            Loop

            'Replace FurnP360()
            Do While True
                'Dim FDate As Date, TDate As Date
                StrPos1 = InStr(ExpString, "FURNP360([")
                If StrPos1 = 0 Then Exit Do
                StrPos2 = InStr(StrPos1 + 10, ExpString, "],")
                Para1 = Mid(ExpString, StrPos1 + 10, StrPos2 - StrPos1 - 10)
                StrPos1 = StrPos2 + 3
                StrPos2 = InStr(StrPos1, ExpString, "],")
                Para2 = Mid(ExpString, StrPos1, StrPos2 - StrPos1)
                '**********
                StrPos1 = StrPos2 + 3
                StrPos2 = InStr(StrPos1, ExpString, "])")
                If StrPos2 <> 0 Then
                    Para3 = Mid(ExpString, StrPos1, StrPos2 - StrPos1)
                Else
                    Para3 = ""
                End If
                '*********
                FieldValue = FurnP360(Para1, Para2, Para3)
                StrPos1 = InStr(ExpString, "FURNP360([")
                FieldValue = IIf(IsNothing(FieldValue), 0, FieldValue)
                ExpString = Mid(ExpString, 1, StrPos1 - 1) & FieldValue & Mid(ExpString, StrPos2 + 2)
                FieldValue = Nothing
            Loop


            'Replace PTAX()
            If InStr(ExpString, "PTAX()") > 0 Then
                FieldValue = PTax(ChkS(rsVarMast1.Rows(0).Item("Loc_Code")))
                ExpString = Replace(ExpString, "PTAX()", FieldValue)
                FieldValue = Nothing
            End If

            'Replace LonBal()
            Do While True
                StrPos1 = InStr(ExpString, "LONBAL([")
                If StrPos1 = 0 Then Exit Do
                StrPos2 = InStr(StrPos1 + 8, ExpString, "]")
                Para1 = Mid(ExpString, StrPos1 + 8, StrPos2 - StrPos1 - 8)
                StrPos1 = InStr(StrPos2, ExpString, "[")
                If InStr(StrPos2 + 1, ExpString, "[") > 0 And InStr(StrPos2 + 1, ExpString, "[") < InStr(StrPos2 + 1, ExpString, ")") Then
                    StrPos1 = InStr(StrPos2 + 1, ExpString, "[")
                    StrPos2 = InStr(StrPos1 + 1, ExpString, "]")
                    Para2 = Mid(ExpString, StrPos1 + 1, StrPos2 - StrPos1 - 1)
                Else
                    Para2 = ""
                End If
                FieldValue = LonBal(Para1, Para2)
                FieldValue = IIf(IsNothing(FieldValue), 0, FieldValue)
                StrPos1 = InStr(ExpString, "LONBAL([")
                ExpString = Mid(ExpString, 1, StrPos1 - 1) & FieldValue & Mid(ExpString, StrPos2 + 2)
                FieldValue = Nothing
            Loop

            'Replace LonPrk()
            Do While True
                StrPos1 = InStr(ExpString, "LONPRK([")
                If StrPos1 = 0 Then Exit Do
                StrPos2 = InStr(StrPos1 + 8, ExpString, "]")
                Para1 = Mid(ExpString, StrPos1 + 8, StrPos2 - StrPos1 - 8)
                'StrPos1 = InStr(StrPos2, ExpString, "[")
                If InStr(StrPos2 + 1, ExpString, "[") > 0 And InStr(StrPos2 + 1, ExpString, "[") < InStr(StrPos2 + 1, ExpString, ")") Then
                    StrPos1 = InStr(StrPos2 + 1, ExpString, "[")
                    StrPos2 = InStr(StrPos1 + 1, ExpString, "]")
                    Para2 = Val(Mid(ExpString, StrPos1 + 1, StrPos2 - StrPos1 - 1))
                    If InStr(StrPos2 + 1, ExpString, "[") > 0 And InStr(StrPos2 + 1, ExpString, "[") < InStr(StrPos2 + 1, ExpString, ")") Then
                        StrPos1 = InStr(StrPos2 + 1, ExpString, "[")
                        StrPos2 = InStr(StrPos1 + 1, ExpString, "]")
                        Para3 = Val(Mid(ExpString, StrPos1 + 1, StrPos2 - StrPos1 - 1))
                    Else
                        Para3 = 20000
                    End If
                Else
                    Para2 = 13
                    Para3 = 20000
                End If
                FieldValue = LonPrk(Para1, Para2, Para3)
                FieldValue = IIf(IsNothing(FieldValue), 0, FieldValue)
                StrPos1 = InStr(ExpString, "LONPRK([")
                ExpString = Mid(ExpString, 1, StrPos1 - 1) & FieldValue & Mid(ExpString, StrPos2 + 2)
                FieldValue = Nothing
            Loop

            'Replace LoanPerk()
            Do While True
                StrPos1 = InStr(ExpString, "LOANPERK([")
                If StrPos1 = 0 Then Exit Do
                StrPos2 = InStr(StrPos1 + 10, ExpString, "]")
                Para1 = Mid(ExpString, StrPos1 + 10, StrPos2 - StrPos1 - 10)
                'StrPos1 = InStr(StrPos2, ExpString, "[")
                If InStr(StrPos2 + 1, ExpString, "[") > 0 And InStr(StrPos2 + 1, ExpString, "[") < InStr(StrPos2 + 1, ExpString, ")") Then
                    StrPos1 = InStr(StrPos2 + 1, ExpString, "[")
                    StrPos2 = InStr(StrPos1 + 1, ExpString, "]")
                    Para2 = Val(Mid(ExpString, StrPos1 + 1, StrPos2 - StrPos1 - 1))
                    If InStr(StrPos2 + 1, ExpString, "[") > 0 And InStr(StrPos2 + 1, ExpString, "[") < InStr(StrPos2 + 1, ExpString, ")") Then
                        StrPos1 = InStr(StrPos2 + 1, ExpString, "[")
                        StrPos2 = InStr(StrPos1 + 1, ExpString, "]")
                        Para3 = Val(Mid(ExpString, StrPos1 + 1, StrPos2 - StrPos1 - 1))
                    Else
                        Para3 = 20000
                    End If
                Else
                    Para2 = 13
                    Para3 = 20000
                End If
                FieldValue = LoanPerk(Para1, Para2, Para3)
                FieldValue = IIf(IsNothing(FieldValue), 0, FieldValue)
                StrPos1 = InStr(ExpString, "LOANPERK([")
                ExpString = Mid(ExpString, 1, StrPos1 - 1) & FieldValue & Mid(ExpString, StrPos2 + 2)
                FieldValue = Nothing
            Loop

            'Replace Loan_Perk()
            Do While True
                StrPos1 = InStr(ExpString, "LOAN_PERK([")
                If StrPos1 = 0 Then Exit Do
                StrPos2 = InStr(StrPos1 + 11, ExpString, "],")
                Para1 = Mid(ExpString, StrPos1 + 11, StrPos2 - StrPos1 - 11)

                StrPos1 = StrPos2 + 3
                StrPos2 = InStr(StrPos1, ExpString, "],")
                Para2 = Mid(ExpString, StrPos1, StrPos2 - StrPos1)

                StrPos1 = StrPos2 + 3
                StrPos2 = InStr(StrPos1, ExpString, "],")
                Para3 = Mid(ExpString, StrPos1, StrPos2 - StrPos1)

                StrPos1 = StrPos2 + 3
                StrPos2 = InStr(StrPos1, ExpString, "]")
                Para4 = ChkS(Mid(ExpString, StrPos1, StrPos2 - StrPos1))


                StrPos1 = StrPos2 + 3
                StrPos2 = InStr(StrPos1, ExpString, "]")
                Para5 = Mid(ExpString, StrPos1, StrPos2 - StrPos1)

                FieldValue = Loan_Perk(Para1, Para2, Para3, CStr(Para4), CStr(Para5)) 'YTDAdj(Para1, Para2, Para3, Para4, Para5, Para6)
                FieldValue = IIf(IsNothing(FieldValue), 0, FieldValue)
                StrPos1 = InStr(ExpString, "LOAN_PERK([")
                ExpString = Mid(ExpString, 1, StrPos1 - 1) & FieldValue & Mid(ExpString, StrPos2 + 2)
                FieldValue = Nothing
            Loop

            'Replace PTAX()
            If InStr(ExpString, "PD260()") > 0 Then
                FieldValue = PD260()
                ExpString = Replace(ExpString, "PD260()", FieldValue)
                FieldValue = Nothing
            End If
            'replace GetLvBal(LvType)
            Do While True
                StrPos1 = InStr(ExpString, "GETLVBAL([")
                If StrPos1 = 0 Then Exit Do
                StrPos2 = InStr(StrPos1 + 10, ExpString, "])")
                FieldName = Mid(ExpString, StrPos1 + 10, StrPos2 - StrPos1 - 10)
                FieldValue = GetLvBal(FieldName)
                ExpString = Mid(ExpString, 1, StrPos1 - 1) & FieldValue & Mid(ExpString, StrPos2 + 2)
                FieldValue = Nothing
            Loop


            'Replace Fields with Values
            Do While InStr(ExpString, "{") > 0
                StrPos1 = InStr(ExpString, "{")
                StrPos2 = InStr(StrPos1 + 1, ExpString, "}")
                If StrPos2 = 0 Then Exit Do
                FieldName = Mid(ExpString, StrPos1 + 1, StrPos2 - StrPos1 - 1)
                If ChkField(rsVarMast, FieldName) Then
                    FieldValue = ConvertFoxFormat(rsVarMast.Rows(0).Item(FieldName), rsVarMast.Columns(FieldName).DataType.ToString)
                Else
                    FieldValue = ConvertFoxFormat(rsVarMast1.Rows(0).Item(FieldName), rsVarMast1.Columns(FieldName).DataType.ToString)
                End If
                ExpString = Replace(ExpString, "{" & FieldName & "}", FieldValue)
                FieldValue = Nothing
            Loop
            ExpString = Replace(Replace(Replace(ExpString, "#^#", "{}"), "#^", "{^"), "#", "}")
            If ExpType <> "D" Then
                Evaluate = FoxApp.Eval(ExpString)
            End If
            If ExpType = "N" And RndOffType > 0 And RndOffMultiple > 0 Then                
                Evaluate = RndOff(Evaluate, RndOffType, RndOffMultiple)
            End If
            If ExpType = "D" Then
                Evaluate = FoxApp.Eval("DTOC(" & ExpString & ")")
                If Evaluate = "  /  /  " Then
                    Evaluate = Nothing
                End If
            End If
            Evaluate = ConvertVBFormat(Evaluate, ExpType)
        Catch ex As Exception
            SetErrMessage(ex, "Evaluate")
        Finally
        End Try
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
    Private Function Remainder(ByVal Nominator As Double, ByVal Denominator As Double)
        Remainder = Nominator - Int(Nominator / Denominator) * Denominator
    End Function
    Private Function Furniture(ByVal C_P As String, ByVal A_P As String, Optional ByVal UptoDate As String = "") As Double
        Furniture = 0
    End Function
    Private Function FurnP360(ByVal C_P As String, ByVal A_P As String, Optional ByVal UptoDate As String = "") As Double
        FurnP360 = 0
    End Function

    Private Function PrdAmt(ByVal Formula As String, ByVal FldRated As Integer, ByVal FldMonth As Integer, Optional ByRef FrmDate As Object = Nothing, Optional ByRef ToDate As Object = Nothing) As Double
        Dim A As Integer, StDt As Date, i As Integer, CurMonAmt As Double
        'On Error Resume Next
        PrdAmt = 0
    End Function
    Private Function GetTrn(ByRef TrnType As String, ByRef FldName As String, Optional ByRef AmtFld As String = "", Optional ByRef PayMode As String = "") As Double
        GetTrn = 0
        Exit Function
    End Function
    Private Function GetArr(Optional ByRef FldName As String = "") As Double
        GetArr = 0
    End Function
    Private Function GetArr30(Optional ByRef FldName As String = "") As Double
        GetArr30 = 0
    End Function

    Private Function GetArrO(Optional ByRef FldName As String = "") As Double
        GetArrO = 0
    End Function

    Private Function GetArr_N(Optional ByRef FldName As String = "") As Double
        GetArr_N = 0
    End Function

    Private Function LWOP() As Double
        LWOP = 0
    End Function

    Private Function LWOP_old() As Double
        LWOP_old = 0
    End Function
    Private Function GetAtt(Optional ByRef LvType As String = "", Optional ByRef FldName As String = "") As Double
        GetAtt = 0
    End Function
    Public Function PTax(ByRef Loc_Code As String) As Double
        On Error Resume Next
        PTax = 0
    End Function
    Private Function PrvAmt(ByRef FldName As String, ByRef FldRated As Integer, ByRef FldMonth As Integer, Optional ByRef TableType As String = "P")
        PrvAmt = 0
    End Function
    Private Function YTDAdj(ByRef YTD_Fld As String, ByRef DOA_Fld As String, ByRef DOL_Fld As String, ByRef WEF_Fld As String, ByRef Mon_Fld As String, ByRef Formula As String) As Double
        YTDAdj = 0
    End Function
    Private Function PD_DOJ() As Double
        PD_DOJ = 0
    End Function
    Private Function LonBal(ByRef FldName As String, ByRef AmtType As String) As Double
        LonBal = 0
    End Function

    Private Function LonPrk(ByRef FldName As String, Optional ByRef PerkPer As Double = 13, Optional ByRef LimitForExempt As Double = 20000, Optional ByRef EffectiveDate As Object = Nothing) As Double
        LonPrk = 0
    End Function
    Private Function LoanPerk(ByRef FieldList As String, Optional ByRef PerkPer As Double = 13, Optional ByRef LimitForExempt As Double = 20000, Optional ByRef EffectiveDate As Object = Nothing) As Double
        LoanPerk = 0
    End Function
    Private Function Loan_Perk(ByRef FieldList10Per As String, ByRef FieldList13Per As String, Optional ByRef LimitForExempt As Double = 0, Optional ByVal PerkP As String = "", Optional ByVal BalType As String = "") As Double
        Loan_Perk = 0
    End Function
    Private Function PD260() As Double
        PD260 = 0
    End Function

    Public Function GetLvBal(ByRef LvType As String) As Double
        Dim rsLvMast As New DataTable
        Dim StrSql As String
        StrSql = "SELECT isnull(Opening,0)+isnull(Earned,0)-isnull(Availed,0) as ClBal FROM LeavMast WHERE Emp_Code='" & rsVarMast1.Rows(0).Item("Emp_Code") & "' AND LevYear=" & Year(dLY_START) & " AND LVType='" & LvType & "'"
        oDAL.GetSqlDataTable(rsLvMast, StrSql)
        If Not rsLvMast.Rows.Count = 0 Then
            GetLvBal = ChkN(rsLvMast.Rows(0).Item(0))
        Else
            GetLvBal = 0
        End If
    End Function
#End Region
#Region "   Balance Updation Pocedures   "
    Private Function CreateCursor(Optional ByVal FromRecordset As DataTable = Nothing, Optional ByVal FldsName As String = "") As DataTable
        Dim FldStr As String, FldDec As Integer, FldName As String, FldLen As Integer, FldType As String, StrPos1 As Integer, StrPos2 As Integer, TableExist As Boolean
        CreateCursor = New DataTable
        Dim dc As DataColumn
        If Not FromRecordset Is Nothing Then 'Or Not IsMissing(FromRecordset) Then
            With FromRecordset
                For i = 0 To FromRecordset.Columns.Count - 1
                    CreateCursor.Columns.Add(.Columns(i))
                Next
            End With
        End If
        If FldsName <> "" Then
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
                        dc = New DataColumn(FldName, System.Type.GetType("System.Double"))
                        dc.MaxLength = FldLen
                        CreateCursor.Columns.Add(dc)
                    Case "D"
                        dc = New DataColumn(FldName, System.Type.GetType("System.Date"))
                        dc.MaxLength = FldLen
                        CreateCursor.Columns.Add(dc)
                    Case Else
                        dc = New DataColumn(FldName, System.Type.GetType("System.String"))
                        dc.MaxLength = FldLen
                        CreateCursor.Columns.Add(dc)
                End Select
            Loop
        End If
    End Function

    Public Sub LevUpdate(ByVal Comp_Code As String, ByVal Emp_Code As String, ByVal AsOnDate As Date)
        Dim VarFlds, VarFlds1 As String
        Dim rsLvType As New DataTable, _
            rsLeavMast As New DataTable, _
            rsLeavTran As New DataTable, _
            rsPaySetup As New DataTable, _
            rsTransfer As New DataTable, _
            BgtDays As Double, ErnDays As Double, _
            LvDays As Double
        Dim YosNum As Double
        Dim YOS, YSN As Double
        Dim rsEmployee As New DataTable
        Dim drVarMast, drVarMast1 As DataRow
        Dim drLeavMast As DataRow
        Dim FDate, TDate As Date
        Try
            strSql = "SELECT * FROM HRDMASTQRY WHERE EMP_CODE='" & Emp_Code & "'"
            oDAL.GetSqlDataTable(rsEmployee, strSql)
            If rsVarMast Is Nothing Then rsVarMast = New DataTable : rsVarMast1 = New DataTable
            VarFlds = "" : VarFlds1 = ""
            strSql = "SELECT * FROM PaySetup WHERE Fld_HRDMAST='Y'"
            oDAL.GetSqlDataTable(rsPaySetup, strSql)
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
            rsPaySetup.Dispose()
            rsPaySetup = Nothing
            rsVarMast = CreateCursor(, Mid(VarFlds, 2))
            rsVarMast1 = CreateCursor(, Mid(VarFlds1, 2))
            drVarMast = rsVarMast.NewRow
            drVarMast1 = rsVarMast1.NewRow
            drVarMast1.Item("Comp_Code") = Comp_Code
            For i = 0 To rsVarMast.Rows.Count - 1
                drVarMast.Item(rsVarMast.Columns(i).ColumnName) = rsEmployee.Rows(0).Item(rsVarMast.Columns(i).ColumnName)
            Next
            For i = 0 To rsVarMast1.Rows.Count - 1
                drVarMast1.Item(rsVarMast1.Columns(i).ColumnName) = rsEmployee.Rows(0).Item(rsVarMast1.Columns(i).ColumnName)
            Next
            drVarMast.AcceptChanges()
            drVarMast1.AcceptChanges()
            rsVarMast.Rows.Add(drVarMast)
            rsVarMast1.Rows.Add(drVarMast1)
            oDAL.GetSqlDataTable(rsLvType, "SELECT * FROM LvType")
            oDAL.GetSqlDataTable(rsLeavMast, "SELECT * FROM LeavMast WHERE LevYear='" & Year(dLY_START) & "' AND Emp_Code='" & Emp_Code & "' ORDER BY Emp_Code, LvType")
            oDAL.GetSqlDataTable(rsLeavTran, "SELECT * FROM LeavTran WHERE LevYear='" & Year(dLY_START) & "' AND Emp_Code='" & Emp_Code & "' AND AtDate<='" & Format(AsOnDate, "dd/MMM/yyyy") & "' ORDER BY Emp_Code, LvType,atdate")
            oDAL.GetSqlDataTable(rsTransfer, "SELECT * FROM Transfer WHERE Emp_Code='" & Emp_Code & "' AND TransferWEF BETWEEN '" & Format(dLY_START, "dd/MMM/yyyy") & "' AND '" & Format(dLY_END, "dd/MMM/yyyy") & "' ORDER BY TransferWEF DESC")
            For i = 0 To rsLvType.Rows.Count - 1
                ErnDays = 0
                If rsLeavMast.Select("LvType='" & rsLvType.Rows(i).Item("LvType") & "'").GetUpperBound(0) >= 0 Then
                    drLeavMast = rsLeavMast.Select("LvType='" & rsLvType.Rows(i).Item("LvType") & "'")(0)
                    If ChkS(rsLvType.Rows(i).Item("Cr_Days")) <> "" Then
                        If rsEmployee.Rows(0).Item("DOJ") <= AsOnDate And (IsDBNull(rsEmployee.Rows(0).Item("DOL")) Or rsEmployee.Rows(0).Item("DOL") > dLY_START) Then
                            TDate = MinV(AsOnDate, dLY_END, IIf(IsDBNull(rsEmployee.Rows(0).Item("DOL")), dLY_END, rsEmployee.Rows(0).Item("DOL")))
                            For j = 0 To rsTransfer.Rows.Count - 1
                                If Not TDate <= AsOnDate Then
                                    rsVarMast1.Rows(0).Item("Grd_Code") = rsTransfer.Rows(j).Item("Grd_Code")
                                    rsVarMast1.Rows(0).Item("Dsg_Code") = rsTransfer.Rows(j).Item("Dsg_Code")
                                    rsVarMast1.Rows(0).Item("Loc_Code") = rsTransfer.Rows(j).Item("Loc_Code")
                                    rsVarMast1.Rows(0).Item("Cost_Code") = rsTransfer.Rows(j).Item("Cost_Code")
                                    rsVarMast1.Rows(0).Item("Proc_Code") = rsTransfer.Rows(j).Item("Proc_Code")
                                    FDate = MaxV(FDate, rsTransfer.Rows(j).Item("TransferWEF"))
                                    YOS = Fix((MaxV(rsEmployee.Rows(0).Item("DOJ"), dLY_START) - rsEmployee.Rows(0).Item("DOJ")) / 365)
                                    YSN = Fix((DateDiff(DateInterval.Day, DateSerial(Year(MaxV(rsEmployee.Rows(0).Item("DOJ"), dLY_START)), Month(rsEmployee.Rows(0).Item("DOJ")), Day(rsEmployee.Rows(0).Item("DOJ"))), rsEmployee.Rows(0).Item("DOJ"))) / 365)
                                    BgtDays = FldCal("", "N", ChkN(ErnDays), Replace(Replace(ChkS(rsLvType.Rows(i).Item("Cr_Days")), "{@YOS}", YOS), "{@YSN}", YSN), "", ChkN(rsLvType.Rows(i).Item("Cr_Period")), ChkN(rsLvType.Rows(i).Item("Cr_Month")), 2, 0.05, "")
                                    ErnDays = ErnDays + CreditLeave(BgtDays, ChkN(rsLvType.Rows(i).Item("Cr_Period")), ChkN(rsLvType.Rows(i).Item("Cr_Month")), FDate, TDate)
                                    rsVarMast1.Rows(0).Item("Grd_Code") = rsTransfer.Rows(j).Item("Grd_Prv").Value
                                    rsVarMast1.Rows(0).Item("Dsg_Code") = rsTransfer.Rows(j).Item("Dsg_Prv").Value
                                    rsVarMast1.Rows(0).Item("Loc_Code") = rsTransfer.Rows(j).Item("Loc_Prv").Value
                                    rsVarMast1.Rows(0).Item("Cost_Code") = rsTransfer.Rows(j).Item("Cost_Prv").Value
                                    rsVarMast1.Rows(0).Item("Proc_Code") = rsTransfer.Rows(j).Item("Proc_Prv").Value
                                End If
                                TDate = FDate.AddDays(-1)
                            Next
                            FDate = MaxV(rsEmployee.Rows(0).Item("DOJ"), dLY_START)
                            YosNum = (DateDiff(DateInterval.Day, FDate, rsEmployee.Rows(0).Item("DOJ"))) / 365
                            YOS = IIf(YosNum > Fix(YosNum), Fix(YosNum + 1), Fix(YosNum))     'Fix((FDate - rsEmployee!DOJ) / 365)
                            YSN = Fix(DateDiff(DateInterval.Day, (DateSerial(Year(FDate), Month(rsEmployee.Rows(0).Item("DOJ")), Day(rsEmployee.Rows(0).Item("DOJ")))), rsEmployee.Rows(0).Item("DOJ")) / 365)
                            BgtDays = FldCal("", "N", BgtDays, Replace(Replace(ChkS(rsLvType.Rows(i).Item("Cr_Days")), "{@YOS}", YOS), "{@YSN}", YSN), "", ChkN(rsLvType.Rows(i).Item("Cr_Period")), ChkN(rsLvType.Rows(i).Item("Cr_Month")), 2, 0.05, "")
                            ErnDays = ErnDays + CreditLeave(BgtDays, ChkN(rsLvType.Rows(i).Item("Cr_Period")), ChkN(rsLvType.Rows(i).Item("Cr_Month")), FDate, TDate)
                            drLeavMast.Item("Earned") = ErnDays
                        Else
                            drLeavMast.Item("Earned") = 0
                        End If
                    End If
                    drLeavMast.Item("Availed") = 0
                    For k = 0 To rsLeavTran.Rows.Count - 1
                        If Mid(rsLeavTran.Rows(k).Item("LvType"), 1, 1) = rsLvType.Rows(i).Item("LvType") Then
                            If Mid(rsLeavTran.Rows(k).Item("LvType"), 1, 1) = Mid(rsLeavTran.Rows(k).Item("LvType"), 2, 1) Then
                                drLeavMast.Item("Availed") = drLeavMast.Item("Availed") + IIf(rsLvType.Rows(i).Item("hourly") = "Y", 8, 1)
                            Else
                                drLeavMast.Item("Availed") = drLeavMast.Item("Availed") + ChkN(rsLeavTran.Rows(k).Item("LvDays")) * IIf(rsLvType.Rows(i).Item("hourly") = "Y", IIf(IsDBNull(rsLeavTran.Rows(k).Item("LvHours")), 4, rsLeavTran.Rows(k).Item("LvHours")), 0.5)
                            End If
                        End If
                        If Mid(rsLeavTran.Rows(k).Item("LvType"), 2, 1) = rsLvType.Rows(i).Item("LvType") Then
                            If Mid(rsLeavTran.Rows(k).Item("LvType"), 1, 1) = Mid(rsLeavTran.Rows(k).Item("LvType"), 2, 1) Then
                            Else
                                drLeavMast.Item("Availed") = drLeavMast.Item("Availed") + ChkN(rsLeavTran.Rows(k).Item("LvDays")) * IIf(rsLvType.Rows(i).Item("hourly") = "Y", IIf(IsDBNull(rsLeavTran.Rows(k).Item("LvHours")), 4, rsLeavTran.Rows(k).Item("LvHours")), 0.5)
                            End If
                        End If
                        drLeavMast.AcceptChanges()
                    Next
                    drLeavMast.AcceptChanges()
                    strSql = "UPDATE LeavMast SET Earned=" & ChkN(drLeavMast.Item("Earned")) & ",Availed=" & ChkN(drLeavMast.Item("Availed")) & " WHERE Emp_Code='" & ChkS(drLeavMast.Item("Emp_Code")) & "' AND LeavYear=" & ChkN(drLeavMast.Item("LeavYear")) & " AND LvType='" & drLeavMast.Item("LvType") & "'"
                    oDAL.ExecuteCommand(strSql, DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteNonQuery)
                End If
            Next
        Catch ex As Exception
            SetErrMessage(ex, "LevUpdate")
        Finally
            rsLvType.Dispose()
            rsLeavMast.Dispose()
            rsLeavTran.Dispose()
            rsEmployee.Dispose()
            rsLvType = Nothing
            rsLeavMast = Nothing
            rsLeavTran = Nothing
            rsEmployee = Nothing
        End Try
    End Sub


    Private Function CreditLeave(ByRef Entitlement As Double, ByRef Cr_Period As Integer, ByRef Cr_Month As Integer, ByRef FromDate As Date, ByRef ToDate As Date) As Double
        Dim Months As Integer, StartDate As Date, EndDate As Date, i As Integer
        Dim StartPeriod As Date
        CreditLeave = 0
        If Cr_Period < 2 Then Exit Function
        Months = IIf(Cr_Period = 2, 1, IIf(Cr_Period = 3, 3, IIf(Cr_Period = 4, 6, 12)))
        StartPeriod = DateAdd("m", Cr_Month - Month(FromDate), FromDate.AddDays(-Day(FromDate) + 1))
        StartPeriod = IIf(StartPeriod > ToDate, DateAdd("m", -12, StartPeriod), StartPeriod)
        For i = 1 To 12 / Months
            StartDate = MaxV(DateAdd("m", Months * (i - 1), StartPeriod), FromDate)
            EndDate = MinV(DateAdd("m", Months * i, StartPeriod).AddDays(-1), ToDate)
            CreditLeave = CreditLeave + Entitlement / Months * MonDiff(StartDate, EndDate)
        Next
        'CreditLeave = FormatNumber(CreditLeave, 1)
        CreditLeave = Math.Round(RndOff(CreditLeave, 2, 0.5), 2)
    End Function

    Public Sub RimUpdate(ByVal AsOnDate As Date, ByVal Emp_Code As String, ByVal DOJ As Date, ByVal DOL As Date, ByVal bLwopEffect As Boolean)
        Dim rsReimMast As New DataTable, rsReimTran As New DataTable, _
            rsPaySetup As New DataTable, rsArrear As New DataTable, _
            RimAmt As Double, A As Integer, StDt As Date, StartDt As Date, EndDt As Date, YTD_EndDt, dtLWOP As Date
        Dim YTD_MonLeft, YTD_Prorata, iLwpLoss, MonLeft, Prorata As Double
        Dim RY_Start_WEF As Date
        Dim drReimMast, drTmp As DataRow
        Try
            oDAL.GetSqlDataTable(rsPaySetup, "SELECT Field_Name, Fld_Categ, Fld_PayMast, Fld_Rated, Fld_Month FROM PaySetup WHERE Fld_Categ=4 ORDER BY SNO")
            'If adoFind(rsEmployee, "Emp_Code='" & Emp_Code & "'") Then
            For i = 0 To rsPaySetup.Rows.Count - 1
                rsReimMast.Rows.Clear()
                rsReimTran.Rows.Clear()
                rsArrear.Rows.Clear()
                oDAL.GetSqlDataTable(rsReimMast, "SELECT * FROM ReimMast WHERE RimYear='" & Year(dRY_START) & "' AND Emp_Code='" & Emp_Code & "' AND Field_Name='" & rsPaySetup.Rows(i).Item("Field_Name") & "' ORDER BY Emp_Code, Field_Name")
                oDAL.GetSqlDataTable(rsReimTran, "SELECT * FROM ReimTran WHERE RimYear='" & Year(dRY_START) & "' AND Emp_Code='" & Emp_Code & "' AND ReimDate<='" & Format(AsOnDate, "dd/MMM/yyyy") & "' AND Field_Name='" & rsPaySetup.Rows(i).Item("Field_Name") & "' ORDER BY Emp_Code, Field_Name, ReimDate")
                oDAL.GetSqlDataTable(rsArrear, "SELECT * FROM Arrear WHERE Emp_Code='" & Emp_Code & "' AND PayDate BETWEEN '" & Format(dRY_START, "dd/MMM/yyyy") & "' AND '" & Format(AsOnDate, "dd/MMM/yyyy") & "' AND Field_Name='" & rsPaySetup.Rows(i).Item("Field_Name") & "' ORDER BY Field_Name, PayDate, Fld_WEF")
                RY_Start_WEF = DateSerial(Year(dRY_START), rsPaySetup.Rows(i).Item("Fld_Month"), 1)
                drReimMast = rsReimMast.Rows(0)
                If rsReimMast.Rows.Count = 0 Then 'Not rsArrear.EOF Then
                    strSql = "INSERT INTO ReimMast(RimYear,Emp_Code,Field_Name) VALUES (" & dRY_START.Year & ",'" & Emp_Code & "','" & rsPaySetup.Rows(i).Item("Field_Name") & "')"
                    oDAL.ExecuteCommand(strSql)
                    rsReimMast = New DataTable
                    oDAL.GetSqlDataTable(rsReimMast, "SELECT * FROM ReimMast WHERE RimYear='" & Year(dRY_START) & "' AND Emp_Code='" & Emp_Code & "' AND Field_Name='" & rsPaySetup.Rows(i).Item("Field_Name") & "' ORDER BY Emp_Code, Field_Name")
                    drReimMast = rsReimMast.Rows(0)
                End If
                A = IIf(rsPaySetup.Rows(i).Item("Fld_Rated") = 2, 1, IIf(rsPaySetup.Rows(i).Item("Fld_Rated") = 3, 3, _
                    IIf(rsPaySetup.Rows(i).Item("Fld_Rated") = 4, 6, 12)))
                StDt = DateAdd("m", -Month(RY_Start_WEF) + rsPaySetup.Rows(i).Item("Fld_Month"), RY_Start_WEF)
                StDt = IIf(StDt > RY_Start_WEF, DateAdd(DateInterval.Month, -12, StDt), StDt)
                iLwpLoss = 0
                For j = 1 To 12 / A
                    StartDt = DateAdd(DateInterval.Month, A * (j - 1), StDt)
                    EndDt = DateAdd(DateInterval.Month, A * j, StDt).AddDays(-1)
                    YTD_EndDt = AsOnDate
                    If rsArrear.Rows.Count <> 0 Then
                        If Not IsDBNull(rsReimMast.Rows(0).Item("BUDGET_WEF")) And rsArrear.Rows(0).Item("Fld_WEF") <= rsReimMast.Rows(0).Item("BUDGET_WEF") Then
                            drReimMast.Item("Budget") = ChkN(rsArrear.Rows(0).Item("Prv_Amt"))
                            drReimMast.Item("Prorata") = ChkN(rsArrear.Rows(0).Item("Prv_Amt")) / 12 * MonDiff(MaxV(DOJ, StartDt), xMin(IIf(IsDBNull(DOL), EndDt, DOL), EndDt))
                            drReimMast.Item("YTD_Prorata") = ChkN(rsArrear.Rows(0).Item("Prv_Amt")) / 12 * MonDiff(MaxV(DOJ, StartDt), xMin(IIf(IsDBNull(DOL), YTD_EndDt, DOL), YTD_EndDt))
                        End If
                        For k = 0 To rsArrear.Rows.Count - 1
                            If rsArrear.Rows(k).Item("Fld_WEF") <= StartDt Then
                                drReimMast.Item("BUDGET_WEF") = StartDt
                                drReimMast.Item("Budget") = ChkN(rsArrear.Rows(k).Item("Cur_Amt"))
                                drReimMast.Item("Prorata") = ChkN(rsArrear.Rows(k).Item("Cur_Amt"))
                                drReimMast.Item("YTD_Prorata") = ChkN(rsArrear.Rows(k).Item("Cur_Amt")) / 12 * MonDiff(MaxV(DOJ, StartDt), xMin(IIf(IsDBNull(DOL), YTD_EndDt, DOL), YTD_EndDt))
                            ElseIf rsArrear.Rows(k).Item("Fld_WEF") <= EndDt Then
                                MonLeft = MonDiff(rsArrear.Rows(k).Item("Fld_WEF"), MinV(IIf(IsDBNull(DOL), EndDt, DOL), EndDt))
                                YTD_MonLeft = MonDiff(rsArrear.Rows(k).Item("Fld_WEF"), MinV(IIf(IsDBNull(DOL), YTD_EndDt, DOL), YTD_EndDt))
                                Prorata = ChkN(drReimMast.Item("Prorata")) - _
                                        ChkN(drReimMast.Item("Budget")) / A * MonLeft + _
                                        ChkN(rsArrear.Rows(k).Item("Cur_Amt")) / A * MonLeft
                                YTD_Prorata = ChkN(drReimMast.Item("YTD_Prorata")) - _
                                        ChkN(drReimMast.Item("Budget")) / A * YTD_MonLeft + _
                                        ChkN(rsArrear.Rows(k).Item("Cur_Amt")) / A * YTD_MonLeft
                                drReimMast.Item("BUDGET_WEF") = rsArrear.Rows(k).Item("Fld_WEF")
                                drReimMast.Item("Budget") = ChkN(rsArrear.Rows(k).Item("Cur_Amt"))
                                drReimMast.Item("Prorata") = FormatNumber(Prorata, 0)
                                drReimMast.Item("YTD_Prorata") = FormatNumber(YTD_Prorata, 0)
                            End If
                        Next
                    Else
                        drReimMast.Item("Prorata") = ChkN(drReimMast.Item("Budget")) / 12 * MonDiff(MaxV(DOJ, StartDt), xMin(IIf(IsDBNull(DOL), EndDt, DOL), EndDt))
                        drReimMast.Item("YTD_Prorata") = ChkN(drReimMast.Item("Budget")) / 12 * MonDiff(MaxV(DOJ, StartDt), xMin(IIf(IsDBNull(DOL), YTD_EndDt, DOL), YTD_EndDt))
                    End If
                Next
                'Dim iLwpLoss As Double
                If Not IsDBNull(drReimMast.Item("BUDGET_WEF")) Then
                    If bLwopEffect Then
                        iLwpLoss = calcLwopAmt(drReimMast.Item("Budget"), dRY_START, AsOnDate, Emp_Code, True, rsPaySetup.Rows(i).Item("Field_Name"), drReimMast.Item("BUDGET_WEF"))
                        drReimMast.Item("Prorata") = Math.Round(drReimMast.Item("Prorata") - iLwpLoss, 0)
                        drReimMast.Item("YTD_Prorata") = Math.Round(drReimMast.Item("YTD_Prorata") - iLwpLoss, 0)
                        drReimMast.Item("LWPLoss") = Math.Round(iLwpLoss, 0)
                    End If
                End If
                RimAmt = 0
                For Each drTmp In rsReimTran.Rows
                    RimAmt = RimAmt + ChkN(drTmp.Item("Amount"))
                    drTmp.Item("reimbursed") = RimAmt
                    strSql = "UPDATE ReimTran SET Reimbursed=" & ChkN(RimAmt) & " WHERE Emp_Code='" & drTmp.Item("Emp_Code") & "' AND RimYear=" & ChkN(drTmp.Item("RimYear")) & " AND Field_Name='" & ChkS(drTmp.Item("Field_Name")) & "' AND ReimDate='" & Format(drTmp.Item("ReimDate")) & "'"
                    oDAL.ExecuteCommand(strSql)
                Next
                drReimMast.Item("reimbursed") = RimAmt
                strSql = "UPDATE ReimMast SET Budget_Wef='" & Format(drReimMast.Item("Budget_WEF"), "dd/MMM/yyyy") & "',Budget=" & ChkN(drReimMast.Item("Budget")) & "," & _
                         "Prorata=" & ChkN(drReimMast.Item("Prorata")) & ",YTD_Prorata=" & ChkN(drReimMast.Item("YTD_Prorata")) & ",Reimbursed=" & ChkN(drReimMast.Item("Reimbursed")) & _
                         " WHERE Emp_Code='" & ChkS(drReimMast.Item("Emp_Code")) & "' AND Field_Name='" & ChkS(drReimMast.Item("Field_Name")) & "' AND RimYear=" & ChkN(drReimMast.Item("RimYear"))
                oDAL.ExecuteCommand(strSql)
            Next
        Catch ex As Exception
            SetErrMessage(ex, "RimUpdate")
        Finally
            If Not IsNothing(rsPaySetup) Then rsPaySetup.Dispose()
            If Not IsNothing(rsReimMast) Then rsReimMast.Dispose()
            If Not IsNothing(rsReimTran) Then rsReimTran.Dispose()
            If Not IsNothing(rsArrear) Then rsArrear.Dispose()
        End Try
    End Sub
    Public Sub LonUpdate(ByVal Emp_Code As String, ByVal AsOnDate As Date)
        Dim rsLaMast As New DataTable, rsLaTran As DataTable, vL_Rec As Double, vI_Rec As Double
        Try
            strSql = ""
            oDAL.GetSqlDataTable(rsLaMast, "SELECT * FROM LaMast WHERE Emp_Code='" & Emp_Code & "' ORDER BY Emp_Code, L_Code, L_Date")
            For i = 0 To rsLaMast.Rows.Count - 1
                vL_Rec = 0
                vI_Rec = 0
                If Not IsNothing(rsLaTran) Then rsLaTran.Dispose()
                'rsLaTran = Nothing
                rsLaTran = New DataTable
                oDAL.GetSqlDataTable(rsLaTran, "SELECT * FROM LaTran WHERE Emp_Code='" & Emp_Code & "' AND L_Date='" & Format(rsLaMast.Rows(i).Item("L_Date"), "dd/MMM/yyyy") & "' AND L_RDate<='" & Format(AsOnDate, "dd/MMM/yyyy") & "' AND L_Code='" & rsLaMast.Rows(i).Item("L_Code") & "' ORDER BY Emp_Code, L_Code, L_Date, L_RDate")
                For j = 0 To rsLaTran.Rows.Count - 1
                    vL_Rec = vL_Rec + ChkN(rsLaTran.Rows(j).Item("Prin"))
                    vI_Rec = vI_Rec + ChkN(rsLaTran.Rows(j).Item("int"))
                    strSql = strSql & "UPDATE LATran SET L_Rec=" & vL_Rec & ",I_Rec=" & vI_Rec & " WHERE Emp_Code='" & Emp_Code & "' AND L_Code='" & ChkS(rsLaTran.Rows(j).Item("L_Code")) & "' AND L_Date='" & Format(rsLaMast.Rows(i).Item("L_Date"), "dd/MMM/yyyy") & "' AND L_RDate='" & Format(rsLaTran.Rows(j).Item("L_RDate"), "dd/MMM/yyyy") & "'" & vbCrLf
                    'Debug.WriteLine(strSql)
                    'oDAL.OpenConnection()
                    'oDAL.ExecuteCommand(strSql, DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteNonQuery)
                    'oDAL.CloseConnection()
                Next
                'oDAL.OpenConnection()
                strSql = strSql & "UPDATE LAMast SET L_Rec=" & vL_Rec & ",I_Rec=" & vI_Rec & " WHERE Emp_Code='" & Emp_Code & "' AND L_Code='" & ChkS(rsLaMast.Rows(i).Item("L_Code")) & "' AND L_Date='" & Format(rsLaMast.Rows(i).Item("L_Date"), "dd/MMM/yyyy") & "'" & vbCrLf
                'oDAL.CloseConnection()
                'Debug.Write(strSql)
                'Debug.WriteLine(strSql)
                'oDAL.ExecuteCommand(strSql, DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteNonQuery)
            Next
            If strSql.Trim <> "" Then
                'Debug.Write(strSql.Trim)
                oDAL.ExecuteCommand(strSql, DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteNonQuery)
            End If
        Catch ex As Exception
            SetErrMessage(ex, "LonUpdate")
        Finally
            If Not IsNothing(rsLaMast) Then rsLaMast.Dispose()
            If Not IsNothing(rsLaTran) Then rsLaTran.Dispose()
        End Try
    End Sub

#End Region
End Class
