'Private Sub SaveRecord(ByVal Button As String)
'    On Error Resume Next
'    If Trim(DatCmbEmployee.Text) = "" Then
'        MsgBox("Employee can't be blank", vbOKOnly + vbInformation, "Warning....")
'        DatCmbEmployee.SetFocus()
'        Exit Sub
'    End If
'    If Trim(DatCmbAcHeads.Text) = "" Then
'        MsgBox("Loan Type can't be blank", vbOKOnly + vbInformation, "Warning....")
'        DatCmbAcHeads.SetFocus()
'        Exit Sub
'    End If
'    Dim rsLaMast As New ADODB.Recordset
'    rsLaMast = OpnRst("SELECT LaMast.* FROM LaMast WHERE Emp_Code='" & DatCmbEmployee.BoundText & "' AND L_Code='" & DatCmbAcHeads.BoundText & "' AND " & DateFormat("L_Date", "YYYYMMDD") & "='" & Format(txtL_Date.Value, "YYYYMMDD") & "'", adUseClient, adOpenDynamic, adLockOptimistic)
'    If rsLaMast.EOF Then
'        If Not ActionAllowed(Me.Tag, "N") Then Exit Sub
'        rsLaMast.AddNew()
'        rsLaMast!Emp_Code = Trim(DatCmbEmployee.BoundText)
'        rsLaMast!L_Code = Trim(DatCmbAcHeads.BoundText)
'        rsLaMast!L_Date = txtL_Date.Value
'    Else
'        If Not ActionAllowed(Me.Tag, "E") Then Exit Sub
'    End If
'    rsLaMast!Inst_Type = cmbInst_Type.ListIndex
'    rsLaMast!L_Amt = Val(txtL_Amt.Text)
'    rsLaMast!L_Inst_Amt = Val(txtL_Inst_Amt.Text)
'    rsLaMast!L_Inst_No = Val(txtL_Inst_No.Text)
'    rsLaMast!L_SDate = txtL_SDate.Value
'    rsLaMast!L_Rec = Val(txtL_Rec.Text)
'    rsLaMast!I_Rate = Val(txtI_Rate.Text)
'    rsLaMast!I_Amt = Val(txtI_Amt.Text)
'    rsLaMast!I_Inst_Amt = Val(txtI_Inst_Amt.Text)
'    rsLaMast!I_Inst_No = Val(txtI_Inst_No.Text)
'    rsLaMast!I_SDate = txtI_SDate.Value
'    rsLaMast!I_Rec = Val(txtI_Rec.Text)
'    rsLaMast.Update()
'    rsLaMast.Close()
'    rsLaMast = Nothing
'End Sub

'Private Sub txtI_SDate_Validate(ByVal Cancel As Boolean)
'    If txtI_SDate.Value < txtL_Date.Value Then
'        MsgBox("Recovery starting date can't be less than sanction date.", vbInformation)
'        Cancel = True
'    End If
'End Sub



'Private Sub txtL_SDate_Validate(ByVal Cancel As Boolean)
'    If txtL_SDate.Value < txtL_Date.Value Then
'        MsgBox("Recovery starting date can't be less than sanction date.", vbInformation)
'        Cancel = True
'    End If
'    lblEndDate.Caption = DateAdd("m", txtL_Inst_No.Text, txtL_SDate.Value)
'End Sub

