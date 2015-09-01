Imports DAL
Public Class PayCal
    Dim JSExp As New JScriptEval
    Dim dtPayHist As DataTable
    Dim dtHrdHist As DataTable
    Dim DalUser As DAL.DataLayer.Users
    Dim ObjDal As DAL.DataLayer
    Public Function FldCal(ByVal FldName As String, ByVal FldType As String, ByVal FldValue As Object, ByVal FldFormula As String, ByVal FldValid As String, ByVal FldRated As Integer, ByVal FldMonth As Integer, ByVal FldRndType As Integer, ByVal FldRndMultiple As Double, ByVal FldTable As String) As Object
        'On Error Resume Next
        Dim CalFlg As Boolean
        If Trim(FldValid) <> "" Then
            CalFlg = Evaluate(UCase(FldValid), "L", 1, 1, 1, 1, "")
        Else
            CalFlg = True
        End If
        If CalFlg Then
            If Chk(FldFormula, True, True) <> "" Then
                FldCal = Evaluate(UCase(FldFormula), FldType, FldRated, FldMonth, FldRndType, FldRndMultiple, FldTable)
            End If
        Else
            FldCal = Nothing
        End If
    End Function

    Public Function Evaluate(ByVal ExpString As String, ByVal ExpType As String, Optional ByVal FldRated As Integer = 1, Optional ByVal FldMonth As Integer = 4, Optional ByVal RndOffType As Integer = 1, Optional ByVal RndOffMultiple As Double = 1.0, Optional ByVal FldTable As String = "", Optional ByVal dtFieldsValues As DataTable = Nothing, Optional ByVal FieldForExp As String = "")
        Dim FieldName As String, FieldValue As Object, StrPos1 As Integer, StrPos2 As Integer, _
            Para1 As Object, Para2 As Object, Para3 As Object, Para4 As Object, _
            Para5 As Object, Para6 As Object
        If Trim(ExpString) = "" Then
            If ExpType = "L" Then
                Evaluate = True
                Exit Function
            Else
                GoTo ExitFunction
            End If
        End If

        'Replace Junk Character
        ExpString = Replace(Replace(ExpString, Chr(13), ""), Chr(10), "")

        'Replace FY_Start, FY_End, RY_Start, RY_End, LY_Start, LY_End
        If InStr(ExpString, "{@PAYDATE}") > 0 Then ExpString = Replace(ExpString, "{@PAYDATE}", ConvertFoxFormat(PayDate, "D"))
        If InStr(ExpString, "{@FY_START}") > 0 Then ExpString = Replace(ExpString, "{@FY_START}", ConvertFoxFormat(FY_Start, "D"))
        If InStr(ExpString, "{@FY_END}") > 0 Then ExpString = Replace(ExpString, "{@FY_END}", ConvertFoxFormat(FY_End, "D"))
        If InStr(ExpString, "{@RY_START}") > 0 Then ExpString = Replace(ExpString, "{@RY_START}", ConvertFoxFormat(RY_Start, "D"))
        If InStr(ExpString, "{@RY_END}") > 0 Then ExpString = Replace(ExpString, "{@RY_END}", ConvertFoxFormat(RY_End, "D"))
        If InStr(ExpString, "{@LY_START}") > 0 Then ExpString = Replace(ExpString, "{@LY_START}", ConvertFoxFormat(LY_Start, "D"))
        If InStr(ExpString, "{@LY_END}") > 0 Then ExpString = Replace(ExpString, "{@LY_END}", ConvertFoxFormat(LY_End, "D"))

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
            If VarType(Para1) = VariantType.Date And VarType(Para2) = VariantType.Date Then
                FieldValue = MonDiff(Para1, Para2)
            Else
                FieldValue = 0
            End If
            FieldValue = IIf(FieldValue Is Nothing, 0, FieldValue)
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
            If VarType(Para1) = VariantType.Date Then FieldValue = EOM(Para1)
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
            If VarType(Para1) = VariantType.Date Then FieldValue = BOM(Para1)
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
            FieldValue = IIf(FieldValue Is Nothing, 0, FieldValue)
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
            FieldValue = IIf(FieldValue Is Nothing, 0, FieldValue)
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
                FieldValue = ConvertFoxFormat(FieldValue, dtPayHist.Rows(0).Item(FieldName).GetType)
            Else
                FieldValue = ConvertFoxFormat(FieldValue, dtHrdHist.Rows(0).Item(FieldName).GetType)
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
            FieldValue = IIf(FieldValue Is Nothing, 0, FieldValue)
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
            FieldValue = IIf(FieldValue Is Nothing, 0, FieldValue)
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
            FieldValue = IIf(FieldValue Is Nothing, 0, FieldValue)
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
            FieldValue = IIf(FieldValue Is Nothing, 0, FieldValue)
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
            FieldValue = IIf(FieldValue Is Nothing, 0, FieldValue)
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
            FieldValue = IIf(FieldValue Is Nothing, 0, FieldValue)
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
            FieldValue = LWOP_Old()
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
            FieldValue = IIf(FieldValue Is Nothing, 0, FieldValue)
            ExpString = Mid(ExpString, 1, StrPos1 - 1) & FieldValue & Mid(ExpString, StrPos2 + 2)
            FieldValue = Nothing
        Loop

        'Replace LESCAL()
        Do While True
            StrPos1 = InStr(ExpString, "LESCAL([")
            If StrPos1 = 0 Then Exit Do
            StrPos2 = InStr(StrPos1 + 8, ExpString, "])")
            If StrPos2 - StrPos1 - 8 > 0 Then
                Para1 = Mid(ExpString, StrPos1 + 8, StrPos2 - StrPos1 - 8)
            Else
                Para1 = ""
            End If
            FieldValue = LesCal(Para1)
            FieldValue = IIf(FieldValue Is Nothing, 0, FieldValue)
            ExpString = Mid(ExpString, 1, StrPos1 - 1) & FieldValue & Mid(ExpString, StrPos2 + 2)
            FieldValue = Nothing
        Loop

        'Replace PTAX()
        If InStr(ExpString, "PTAX()") > 0 Then
            FieldValue = PTax(Chk(dtVarMast1.Rows(0).Item("Loc_Code")))
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
            FieldValue = IIf(FieldValue Is Nothing, 0, FieldValue)
            StrPos1 = InStr(ExpString, "LONBAL([")
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
            Para4 = Chk(Mid(ExpString, StrPos1, StrPos2 - StrPos1))


            StrPos1 = StrPos2 + 3
            StrPos2 = InStr(StrPos1, ExpString, "]")
            Para5 = Mid(ExpString, StrPos1, StrPos2 - StrPos1)

            FieldValue = Loan_Perk(Para1, Para2, Para3, CStr(Para4), CStr(Para5)) 'YTDAdj(Para1, Para2, Para3, Para4, Para5, Para6)
            FieldValue = IIf(FieldValue Is Nothing, 0, FieldValue)
            StrPos1 = InStr(ExpString, "LOAN_PERK([")
            ExpString = Mid(ExpString, 1, StrPos1 - 1) & FieldValue & Mid(ExpString, StrPos2 + 2)
            FieldValue = Nothing
        Loop

        'Replace PD260()
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
            If ChkField(New DataView(dtVarMast), FieldName) Then
                If dtFieldsValues Is Nothing Then
                    FieldValue = ConvertFoxFormat(dtVarMast.Rows(0).Item(FieldName).Value, dtVarMast.Columns(FieldName).DataType)
                Else
                    FieldValue = ConvertFoxFormat(dtFieldsValues.Rows(0).Item(FieldName).Value, dtFieldsValues.Columns(FieldName).DataType)
                End If
            Else
                If dtFieldsValues Is Nothing Then
                    FieldValue = ConvertFoxFormat(dtVarMast1.Rows(0).Item(FieldName).Value, dtVarMast1.Columns(FieldName).DataType)
                Else
                    FieldValue = ConvertFoxFormat(dtFieldsValues.Rows(0).Item(FieldName).Value, dtFieldsValues.Columns(FieldName).DataType)
                End If
            End If
            ExpString = Replace(ExpString, "{" & FieldName & "}", FieldValue)
            FieldValue = Nothing
        Loop
        ExpString = Replace(Replace(Replace(ExpString, "#^#", "{}"), "#^", "{^"), "#", "}")
        'Evaluate = FoxApp.Eval(ExpString)
        Evaluate = JSExp.Evaluate(ExpString)
        If ExpType = "N" And RndOffType > 0 And RndOffMultiple > 0 Then
            Evaluate = RndOff(Evaluate, RndOffType, RndOffMultiple)
        End If
        If ExpType = "D" Then
            If JSExp.Evaluate("DTOC(" & ExpString & ")") = "  /  /  " Then
                Evaluate = Nothing
            End If
        End If
ExitFunction:
        On Error Resume Next
        Evaluate = ConvertFoxFormat(Evaluate, ExpType)
        'Set FoxApp = Nothing
        Exit Function

    End Function


    Private Function ConvertFoxFormat(ByVal FieldValue As Object, ByVal FieldType As Object)
        FieldValue = IIf(FieldValue Is Nothing, Nothing, FieldValue)
        Select Case ChkType(FieldType)
            Case DbType.Int64  'Number
                ConvertFoxFormat = IIf(FieldValue Is Nothing, 0, FieldValue)
            Case DbType.Date ' Date
                ConvertFoxFormat = "#^" & IIf(FieldValue Is Nothing, "", Format(FieldValue, "YYYY/MM/DD")) & "#"
            Case DbType.Boolean 'Logical
                ConvertFoxFormat = IIf(FieldValue Is Nothing, ".F.", ".T.")
            Case Else   'Character
                ConvertFoxFormat = "'" & IIf(FieldValue Is Nothing, "", FieldValue) & "'"
        End Select
    End Function
    Private Function GetTrn(ByVal TrnType As String, ByVal FldName As String, Optional ByVal AmtFld As String = "", Optional ByVal PayMode As String = "") As Double
        GetTrn = 0
    End Function
    Private Function GetAtt(Optional ByVal LvType As String = "", Optional ByVal FldName As String = "") As Double
        GetAtt = 0
    End Function
    Private Function PrvAmt(ByVal FldName As String, ByVal FldRated As Integer, ByVal FldMonth As Integer, Optional ByVal TableType As String = "") As Double
        PrvAmt = 0
    End Function
    Private Function PrdAmt(ByVal Formula As String, ByVal FldRated As Integer, ByVal FldMonth As Integer, ByVal FrmDate As Date, ByVal ToDate As Date) As Double
        PrdAmt = 0
    End Function
    Private Function YTDAdj(ByVal YTD_Fld As String, ByVal DOA_Fld As String, ByVal DOL_Fld As String, ByVal WEF_Fld As String, ByVal Mon_Fld As String, ByVal Formula As String) As Double
        YTDAdj = 0
    End Function
    Private Function GetArr(Optional ByVal FldName As String = "") As Double
        GetArr = 0
    End Function
    Private Function GetArr30(Optional ByVal FldName As String = "") As Double
        GetArr30 = 0
    End Function
    Private Function GetArrO(Optional ByVal FldName As String = "") As Double
        GetArrO = 0
    End Function
    Private Function GetArr_N(Optional ByVal FldName As String = "") As Double
        GetArr_N = 0
    End Function
    Private Function LWOP() As Double
        LWOP = 0
    End Function
    Private Function LWOP_Old() As Double
        LWOP_Old = 0
    End Function
    Private Function PD_DOJ() As Double
        PD_DOJ = 0
    End Function
    Public Function Furniture(ByVal C_P As String, ByVal A_P As String, Optional ByVal UptoDate As String = "") As Double
        Furniture = 0
    End Function
    Public Function LesCal(Optional ByVal FldLst As String = "") As Double
        LesCal = 0
    End Function
    Public Function PTax(ByVal Loc_Code As String) As Double
        PTax = 0
    End Function
    Public Function LonBal(ByVal FldName As String, ByVal AmtType As String) As Double
        LonBal = 0
    End Function
    Public Function Loan_Perk(ByVal FieldList10Per As String, ByVal FieldList13Per As String, Optional ByVal LimitForExempt As Double = 20000, Optional ByVal PerkP As String = "", Optional ByVal BalType As String = "O") As Double
        Loan_Perk = 0
    End Function
    Public Function PD260() As Double
        PD260 = 0
    End Function
    Public Function GetLvBal(ByVal LvType As String) As Double
        GetLvBal = 0
    End Function
    Public Function chkMon(ByVal ChkMonth As Date, ByRef Message As String, Optional ByVal LocCodes As String = "''") As Boolean
        Dim strSql As String
        Dim dtMonth As New DataTable
        Dim drMonth As DataRow
        Dim LocCode() As String
        LocCode = Split(LocCodes, ",")
        If LocCodes <> "''" Then
            For i As Int16 = 0 To LocCode.GetUpperBound(0)
                strSql = "SELECT DISTINCT datename(mm,PayDate) + '-' + datename(yyyy,paydate) as MonthName, PayDate, MonUpdate FROM MonUpdate WHERE Comp_Code='" & DalUser.CurrentCompID & "' AND Loc_Code= " & LocCode(i) & " AND PayDate BETWEEN '" & Format(FY_Start, "dd/MMM/yyyy") & "' AND '" & Format(FY_End, "dd/MMM/yyyy") & "' ORDER BY PayDate"
                ObjDal.GetSqlDataTable(dtMonth, strSql)
                If dtMonth.Select("PayDate='" & Format(ChkMonth, "dd/MMM/yyyy") & "'").GetUpperBound(0) > -1 Then
                    drMonth = dtMonth.Select("PayDate='" & Format(ChkMonth, "dd/MMM/yyyy") & "'")(0)
                    If Chk(drMonth.Item("MonUpdate")) = "Y" Then
                        chkMon = False
                        Message = "Month has been closed for Location " & LocCode(i)
                        Exit For
                    Else
                        chkMon = True
                    End If
                ElseIf dtMonth.Select("PayDate='" & Format(ChkMonth.AddDays(-1 * Day(ChkMonth)), "dd/MMM/yyyy") & "'").GetUpperBound(0) > -1 Then
                    drMonth = dtMonth.Select("PayDate='" & Format(ChkMonth.AddDays(-1 * Day(ChkMonth)), "dd/MMM/yyyy") & "'")(0)
                    If Chk(drMonth.Item("MonUpdate")) <> "Y" Then
                        chkMon = False
                        Message = "Previous Month has not been closed for Location " & LocCode(i)
                        Exit For
                    Else
                        chkMon = True
                    End If
                Else
                    If ChkMonth.AddDays((-1 * Day(ChkMonth)) + 1) = FY_Start Then
                        chkMon = True
                    Else
                        chkMon = False
                        Message = "Previous Month has not been Processed for Location " & LocCode(i)
                        Exit For
                    End If
                End If
            Next
        Else
            strSql = "SELECT DISTINCT datename(mm,PayDate) + '-' + datename(yyyy,paydate) as MonthName, PayDate, MonUpdate FROM MonUpdate WHERE Comp_Code='" & DalUser.CurrentCompID & "' AND PayDate BETWEEN '" & Format(FY_Start, "dd/MMM/yyyy") & "' AND '" & Format(FY_End, "dd/MMM/yyyy") & "' ORDER BY PayDate"
            ObjDal.GetSqlDataTable(dtMonth, strSql)
            If dtMonth.Select("PayDate='" & Format(ChkMonth, "dd/MMM/yyyy") & "'").GetUpperBound(0) > -1 Then
                drMonth = dtMonth.Select("PayDate='" & Format(ChkMonth, "dd/MMM/yyyy") & "'")(0)
                If Chk(drMonth.Item("MonUpdate")) = "Y" Then
                    chkMon = False
                    Message = "Month has been closed."
                Else
                    chkMon = True
                End If
            ElseIf dtMonth.Select("PayDate='" & Format(ChkMonth.AddDays(-1 * Day(ChkMonth)), "dd/MMM/yyyy") & "'").GetUpperBound(0) > -1 Then
                drMonth = dtMonth.Select("PayDate='" & Format(ChkMonth.AddDays(-1 * Day(ChkMonth)), "dd/MMM/yyyy") & "'")(0)
                If Chk(drMonth.Item("MonUpdate")) <> "Y" Then
                    chkMon = False
                    Message = "Previous Month has not been closed."
                Else
                    chkMon = True
                End If
            Else
                If ChkMonth.AddDays((-1 * Day(ChkMonth)) + 1) = FY_Start Then
                    chkMon = True
                Else
                    chkMon = False
                    Message = "Previous Month has not been Processed."
                End If
            End If
        End If
    End Function

    Public Sub New(ByVal varDalUser As DAL.DataLayer.Users, ByVal varObjDal As DAL.DataLayer)
        DalUser = varDalUser
        ObjDal = varObjDal
    End Sub
    Public Function getPayCalString(ByVal vFor As String, ByVal PayDate As Date) As String
        Dim dtSql As New DataTable
        Dim SqlStr As String
        ObjDal.GetSqlDataTable(dtSql, "SELECT SQLString FROM CompMast")
        SqlStr = Chk(dtSql.Rows(0).Item(0))
        SqlStr = Replace(SqlStr, "[@VFOR]", "'" & Replace(vFor, "'", "''") & "'")
        SqlStr = Replace(SqlStr, "[@PAYDATE]", "'" & Format(PayDate, "dd/MMM/yyyy") & "'")
        SqlStr = Replace(SqlStr, "[@FY_START]", "'" & Format(FY_Start, "dd/MMM/yyyy") & "'")
        SqlStr = Replace(SqlStr, "[@FY_END]", "'" & Format(FY_End, "dd/MMM/yyyy") & "'")
        SqlStr = Replace(SqlStr, "[@LY_START]", "'" & Format(LY_Start, "dd/MMM/yyyy") & "'")
        SqlStr = Replace(SqlStr, "[@LY_END]", "'" & Format(LY_End, "dd/MMM/yyyy") & "'")
        SqlStr = Replace(SqlStr, "[@RY_START]", "'" & Format(RY_Start, "dd/MMM/yyyy") & "'")
        SqlStr = Replace(SqlStr, "[@RY_END]", "'" & Format(RY_End, "dd/MMM/yyyy") & "'")
        SqlStr = Replace(SqlStr, "[@COMP_CODE]", "'" & DalUser.CurrentCompID & "'")
        getPayCalString = SqlStr
    End Function
End Class
