Partial Class GeneralInfo
    Inherits System.Web.UI.Page
    Public Ds As New DataSet

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents trFamily As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents GrdFamily As System.Web.UI.WebControls.DataGrid
    Protected WithEvents cmdFamilyAdd As System.Web.UI.WebControls.Button

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
        Ajax.Utility.RegisterTypeForAjax(GetType(GeneralInfo))
        If Server.GetLastError Is Nothing Then SetMsg(LblMsg, "")
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If
        CreateLanguages()
        Try
            'By Ravi 17 nov 2006
            Dim SrNo As String
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
                BtnDelete.Visible = bSuccess
            End If
            'If CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules <> "" Then
            '    If InStr(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, SrNo & "-") <> 0 Then
            '        Dim int As Int16, st As String
            '        int = InStr(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, SrNo)
            '        st = Right(Mid(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, int, Len(SrNo & "-") + 1), 1)

            '        If st = "S" Then
            '            txtEM_CD.ReadOnly = False
            '        Else
            '            txtEM_CD.ReadOnly = True
            '            cmdNew.Visible = False
            '            cmdSave.Visible = False
            '        End If
            '    Else
            '        txtEM_CD.ReadOnly = True
            '        cmdSave.Visible = False
            '        SetMsg(LblRights, "You are not authorised to view data on this page.")
            '        'Response.Write("<BR><center><B></B></center>")
            '        Exit Sub
            '    End If
            'End If

            If Not IsPostBack Then
                trAddress.Style.Add("display", "block")
                trOtherInfo.Style.Add("display", "none")
                trEmergency.Style.Add("display", "none")
                txtEM_CD.Text = Session("EM_CD")
                FillCombo()
                'DisplayRecord()

                SetTextControlLength()

                If Not IsNothing(Request.QueryString.Item("QstrAction")) Then
                    sAction.Text = UCase(Chk(Request.QueryString.Item("QstrAction")))
                    'cmdNew_Click(sender, e) comment by Ravi
                    btnNew_Click(sender, Nothing)

                End If
            End If
            cmdSave.Attributes.Add("onclick", "return ValidateCtrl();")
            BtnDelete.Attributes.Add("onclick", "return DeleteRecord();")
            '    btnNew.Attributes.Add("onclick", "return clear();")

        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (Page_Load)")
        End Try
    End Sub
    Public Sub FCallBack(Optional ByVal result As IAsyncResult = Nothing)
        Response.Write("Process Completed")
    End Sub
    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
    Public Function GetNextEmpRec(ByVal OEmpCode As String) As String
        Try
            Dim strSql As String
            Dim EmpCode As String
            'strSql = "Select Top 1 Emp_Code From HRDMASTQRY Where EMP_CODE>'" & OEmpCode & "' Order By Emp_Code "
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
            'strSql = "Select Top 1 Emp_Code From HRDMASTQRY Where EMP_CODE<'" & OEmpCode & "' Order By Emp_Code desc "
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
            ' strSql = "Select Top 1 Emp_Code From HRDMASTQRY  Order By Emp_Code "
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
            ' strSql = "Select Top 1 Emp_Code From HRDMASTQRY  Order By Emp_Code desc"
            strSql = "Select Top 1 Emp_Code From HRDMASTQRY Where 1=1 " & Session("UserCodes") & " Order By Emp_Code desc"
            EmpCode = Chk(Session("DalObj").ExecuteCommand(strSql, DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
            GetLastEmpRec = EmpCode
        Catch ex As Exception

        End Try
    End Function
    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
    Public Function GetEmpRec() As DataSet
        Try
            Dim strSql As String

            'strSql = "Select * From HRDMASTQRY Where EMP_CODE='" & Session("EM_CD") & "'"
            strSql = "Select * From HRDMASTQRY Where EMP_CODE=" & "'" & Session("EM_CD") & "'" & Session("UserCodes")
            strSql &= " ;SELECT LANG_CODE FROM LANGMAST	"
            strSql &= " ;SELECT COUNT(LANG_CODE)AS Total FROM LANGMAST"


            Session("DalObj").GetSqlDataSet(Ds, strSql)
            GetEmpRec = Ds
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
    Public Function SetCurrentEmpCode() As String
        Try
            SetCurrentEmpCode = Session("EM_CD")
        Catch ex As Exception

        End Try
    End Function

#Region "    Fill Combo Boxes    "

    Private Sub FillCombo()
        Try
            'cmbMStatus.Items.Add(New ListItem("", "0"))
            'cmbMStatus.Items.Add(New ListItem("Married", "1"))
            'cmbMStatus.Items.Add(New ListItem("Single", "2"))
            'cmbMStatus.Items.Add(New ListItem("Widow", 3))
            'cmbMStatus.Items.Add(New ListItem("Widover", 4))
            'cmbMStatus.Items.Add(New ListItem("Divorcee", 5))
            'cmbMStatus.SelectedIndex = 2

            cmbFatherHusband.Items.Add(New ListItem("Father", "F"))
            cmbFatherHusband.Items.Add(New ListItem("Husband", "H"))

            CmbBGroup.Items.Add(New ListItem("", ""))
            CmbBGroup.Items.Add(New ListItem("A+", "A+"))
            CmbBGroup.Items.Add(New ListItem("A-", "A-"))
            CmbBGroup.Items.Add(New ListItem("B+", "B+"))
            CmbBGroup.Items.Add(New ListItem("B-", "B-"))
            CmbBGroup.Items.Add(New ListItem("O+", "O+"))
            CmbBGroup.Items.Add(New ListItem("O-", "O-"))
            CmbBGroup.Items.Add(New ListItem("AB+", "AB+"))
            CmbBGroup.Items.Add(New ListItem("AB-", "AB-"))

            cmbReligion.Items.Add("")
            cmbReligion.Items.Add("Hindu")
            cmbReligion.Items.Add("Muslim")
            cmbReligion.Items.Add("Sikh")
            cmbReligion.Items.Add("Christian")
            cmbReligion.Items.Add("Others")

        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (FillCombo)")
        Finally
            GC.Collect()
        End Try
    End Sub

#End Region

#Region "  Display Records   "
    '<Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
    '    Public Sub DisplayRecord()

    '    Dim DtEmp As New DataTable
    '    Try
    '        Dim strSQL As String
    '        strSQL = "Select * From HrdMast Where Emp_Code = '" & Chk(txtEM_CD.Text) & "' " & HttpContext.Current.Session("UserCodes") & " "

    '        'HttpContext.Current.Server.
    '        ' Response.Write("1")
    '        HttpContext.Current.Session("DalObj").GetSqlDataTable(DtEmp, strSQL)
    '        If DtEmp.Rows.Count > 0 Then
    '            sAction.Text = ""
    '            ViewState("Value") = "MODI"
    '            With DtEmp.Rows(0)

    '                '' ******************** Master ******************************

    '                txtFName.Text = Chk(.Item("FName"))
    '                txtMName.Text = Chk(.Item("MName"))
    '                txtLName.Text = Chk(.Item("LName"))

    '                '' ******************** Image *******************************
    '                ImgEmp.ImageUrl = Chk(.Item("Emp_Pict"))

    '                '' ******************** Mailing Address *********************
    '                txtMAddr1.Text = Chk(.Item("MAddr1"))
    '                txtMAddr2.Text = Chk(.Item("MAddr2"))
    '                txtMAddr3.Text = Chk(.Item("MAddr3"))
    '                txtMCity.Text = Chk(.Item("MCity"))
    '                txtMState.Text = Chk(.Item("MState"))
    '                txtMPIN.Text = Chk(.Item("MPIN"))
    '                txtMCountry.Text = Chk(.Item("MCountry"))
    '                txtMPhone.Text = Chk(.Item("MPhone"))


    '                '' ******************** Permanet Address *******************************
    '                txtPAddr1.Text = Chk(.Item("PAddr1"))
    '                txtPAddr2.Text = Chk(.Item("PAddr2"))
    '                txtPAddr3.Text = Chk(.Item("PAddr3"))
    '                txtPCity.Text = Chk(.Item("PCity"))
    '                txtPState.Text = Chk(.Item("PState"))
    '                txtPPIN.Text = Chk(.Item("PPIN"))
    '                txtPCountry.Text = Chk(.Item("PCountry"))
    '                txtPPhone.Text = Chk(.Item("PPhone"))

    '                '' ******************** Other Information *******************************
    '                If ChkN(.Item("Sex")) = 2 Then
    '                    optFemale.Checked = True
    '                Else
    '                    optMale.Checked = True
    '                End If

    '                If ChkN(.Item("MStatus")) = 2 Then
    '                    DtpDOM.Enabled = False
    '                    CmbMStatus.Value = ChkN(.Item("MStatus"))
    '                Else
    '                    DtpDOM.Enabled = True
    '                    CmbMStatus.Value = ChkN(.Item("MStatus"))
    '                End If

    '                If Not IsDBNull(.Item("DOB")) Then
    '                    dtpDOB.DateValue = .Item("DOB")
    '                Else
    '                    dtpDOB.DateValue = CDate("01/01/1900")

    '                End If

    '                If Not IsDBNull(.Item("DOM")) Then
    '                    DtpDOM.DateValue = .Item("DOM")
    '                Else
    '                    DtpDOM.DateValue = CDate("01/01/1900")
    '                End If

    '                If Chk(.Item("FathHusb")) = "H" Then
    '                    cmbFatherHusband.SelectedValue = "H"
    '                Else
    '                    cmbFatherHusband.SelectedValue = "F"
    '                End If

    '                'dtpPassportExp

    '                If Not IsDBNull(.Item("DOV")) Then
    '                    dtpPassportExp.DateValue = .Item("DOV")
    '                Else
    '                    dtpPassportExp.DateValue = CDate("01/01/1900")
    '                End If


    '                txtFathHusbName.Text = Chk(.Item("FathHusbName"))
    '                txtDomicile.Text = Chk(.Item("Domicile"))
    '                txtNationality.Text = Chk(.Item("Nationality"))
    '                txtBirthPlace.Text = Chk(.Item("BirthPlace"))
    '                txtHobbies.Text = Chk(.Item("Hobbies"))
    '                ChkCombo(CmbBGroup, .Item("BloodGrp"))
    '                ChkCombo(cmbFood, .Item("FoodChoice"))
    '                ChkCombo(cmbReligion, .Item("Religion"))
    '                txtPassportNo.Text = Chk(.Item("PassportNo"))
    '                txtDLNo.Text = Chk(.Item("DLNo"))
    '                txtEmailID.Text = Chk(.Item("EmailID"))
    '                txtPEmailId.Text = Chk(.Item("PEmailId"))

    '                '' ******************** Emergency Contact *******************************
    '                txtEmergencyName.Text = Chk(.Item("EmergencyName"))
    '                txtEmergencyAddress.Text = Chk(.Item("EmergencyAddress"))
    '                txtEmergencyPhoneNo.Text = Chk(.Item("EmergencyPhoneNo"))
    '                txtDrName.Text = Chk(.Item("DrName"))
    '                txtDrAddress.Text = Chk(.Item("DrAddress"))
    '                txtDrPhoneNo.Text = Chk(.Item("DrPhoneNo"))
    '                TxtFathHusbOcupation.Text = Chk(.Item("FathHusbOcupation"))
    '                ' Response.Write("2")
    '                BlankLanguage()
    '                If Chk(.Item("Languages")) <> "" Then
    '                    FillLanguage(Chk(.Item("Languages")))
    '                End If
    '            End With
    '        Else
    '            'Response.Write("3")
    '            Dim Code As Object
    '            Code = HttpContext.Current.Session("DalObj").ExecuteCommand("Select Emp_Code From HrdMastQry Where Emp_Code = '" & Chk(txtEM_CD.Text) & "'" & Session("UserCodes"), , DAL.DataLayer.ExecutionType.ExecuteScalar)

    '            If (Not IsDBNull(Code)) And (Not IsNothing(Code)) Then
    '                ImgEmp.ImageUrl = ""
    '                SetMsg(LblMsg, "This Employee Code Exist For Other Location.")
    '                ViewState("Value") = "NULL"
    '            Else
    '                If txtEM_CD.Text <> "" And ViewState("Value") <> "NEW" Then
    '                    ImgEmp.ImageUrl = ""
    '                    SetMsg(LblMsg, "This Employee Code does not exist.")
    '                    ViewState("Value") = "NULL"
    '                End If
    '                ViewState("Value") = "NEW"
    '            End If
    '            ClearAll(Me)
    '            'ViewState("Value") = False

    '            txtEM_CD.Text = Session("EM_CD")
    '            CmbBGroup.SelectedIndex = 0
    '            cmbFatherHusband.SelectedIndex = 0
    '            cmbReligion.SelectedIndex = 0
    '            CmbMStatus.SelectedIndex = 0
    '        End If

    '    Catch ex As Exception
    '        SetMsg(LblMsg, ex.Message & " : (DisplayRecord)")
    '    Finally
    '        DtEmp.Dispose()
    '    End Try
    'End Sub

#End Region

#Region "    Save Records     "

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim i As Int16
        Try
            If ViewState("Value") = "NULL" Then Exit Sub
            If isValidate() Then

                Dim Cnt As Single
                Dim strSQL As String

                Cnt = Session("DalObj").ExecuteCommand("Select isNull(Count(Emp_Code),0) From HrdMast Where Emp_Code = '" & Chk(txtEM_CD.Text) & "'" & Session("UserCodes"), , DalObj.ExecutionType.ExecuteScalar)
                If Cnt = 0 And sAction.Text.ToUpper = "NEW" Then
                    strSQL = " Insert Into HrdMast (Emp_Code,Comp_Code,Ltype) Values ('" & Chk(txtEM_CD.Text) & "','" & Session("LoginUser").CurrentCompID & "',1)"
                    Session("DalObj").ExecuteCommand(strSQL)

                    If Trim(Session("UserCodes")) <> "" Then
                        Dim Strq As String = Mid(Trim(Session("UserCodes")), 4, Len(Trim(Session("UserCodes"))) - 3)
                        Strq = Replace(Strq, "AND", ",")
                        Strq = Replace(Strq, "IN (", "=")
                        Strq = Replace(Strq, ")", "")
                        Strq = " Update HrdMast Set " & Strq & " Where EMP_CODE = '" & Chk(txtEM_CD.Text) & "'"
                        Session("DalObj").ExecuteCommand(Strq)
                    End If

                    sAction.Text = ""
                End If

                CountKnownLanguage()
                'txtHobbies
                strSQL = " Update HrdMast Set " & _
                         " FName = '" & Chk(txtFName.Text) & "', " & _
                         " MName = '" & Chk(txtMName.Text) & "', " & _
                         " LName = '" & Chk(txtLName.Text) & "', " & _
                         " MAddr1 = '" & Chk(txtMAddr1.Text) & "', " & _
                         " MAddr2 = '" & Chk(txtMAddr2.Text) & "', " & _
                         " MAddr3 = '" & Chk(txtMAddr3.Text) & "', " & _
                         " MCity = '" & Chk(txtMCity.Text) & "', " & _
                         " MState = '" & Chk(txtMState.Text) & "', " & _
                         " MPIN = '" & Chk(txtMPIN.Text) & "', " & _
                         " MCountry = '" & Chk(txtMCountry.Text) & "', " & _
                         " MPhone = '" & Chk(txtMPhone.Text) & "', " & _
                         " PAddr1 = '" & Chk(txtPAddr1.Text) & "', " & _
                         " PAddr2 = '" & Chk(txtPAddr2.Text) & "', " & _
                         " PAddr3 = '" & Chk(txtPAddr3.Text) & "', " & _
                         " EmergencyName = '" & Chk(txtEmergencyName.Text) & "', " & _
                         " EmergencyAddress = '" & Chk(txtEmergencyAddress.Text) & "', " & _
                         " EmergencyPhoneNo = '" & Chk(txtEmergencyPhoneNo.Text) & "', " & _
                         " DrName = '" & Chk(txtDrName.Text) & "', " & _
                         " DrAddress = '" & Chk(txtDrAddress.Text) & "', " & _
                         " DrPhoneNo = '" & Chk(txtDrPhoneNo.Text) & "', " & _
                         " PCity = '" & Chk(txtPCity.Text) & "', " & _
                         " PState = '" & Chk(txtPState.Text) & "', " & _
                         " PPIN = '" & Chk(txtPPIN.Text) & "', " & _
                         " PCountry = '" & Chk(txtPCountry.Text) & "', " & _
                         " PEmailId = '" & Chk(txtPEmailId.Text) & "', " & _
                         " PPhone = '" & Chk(txtPPhone.Text) & "', " & _
                         " SEX = " & IIf(optFemale.Checked, 2, 1) & ", " & _
                         " MStatus = " & ChkN(CmbMStatus.Value) & ", " & _
                         " DOM = " & IIf(CmbMStatus.Value = 2, "Null, ", "'" & dtpDOM.DateValue & "', ") & _
                         " DOB = '" & dtpDOB.DateValue & "', " & _
                         " DOV = " & IIf(ChkPassportExp.Checked = True, " '" & Format(dtpPassportExp.DateValue, "dd/MMM/yyyy") & "'", "Null") & ", " & _
                         " FathHusb = '" & Chk(cmbFatherHusband.SelectedValue) & "', " & _
                         " FathHusbName = '" & Chk(txtFathHusbName.Text) & "', " & _
                         " Domicile = '" & Chk(txtDomicile.Text) & "', " & _
                         " Nationality = '" & Chk(txtNationality.Text) & "', " & _
                         " BirthPlace = '" & Chk(txtBirthPlace.Text) & "', " & _
                         " Hobbies = '" & Chk(txtHobbies.Text) & "', " & _
                         " BloodGrp = '" & Chk(CmbBGroup.SelectedValue) & "', " & _
                         " FoodChoice = '" & Chk(cmbFood.SelectedValue) & "', " & _
                         " Religion = '" & Chk(cmbReligion.SelectedValue) & "', " & _
                         " PassportNo = '" & Chk(txtPassportNo.Text) & "', " & _
                         " DLNo = '" & Chk(txtDLNo.Text) & "', " & _
                         " EmailID = '" & Chk(txtEmailID.Text) & "', " & _
                         " Languages = '" & Chk(ViewState("Languages")) & "', " & _
                         " FathHusbOcupation = '" & Chk(TxtFathHusbOcupation.Text) & "' , " & _
                         " Spouse = '" & Chk(TxtSpouse.Text) & "' " & _
                         " Where Emp_Code = '" & Chk(txtEM_CD.Text) & "'"
                i = Session("DalObj").ExecuteCommand(strSQL)
                If i > 0 Then
                    SetMsg(LblMsg, "Record is saved successfully")

                Else
                    SetMsg(LblMsg, "Record is not  saved successfully")
                End If
            End If


        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (cmdSave_Click)")
        End Try
    End Sub

    Private Function isValidate() As Boolean
        Try
            'If Trim(txtFName.Text) = "" Then
            '    SetMsg(LblMsg, "First Name Can Not Be Left Blank.")
            '    isValidate = False
            '    Exit Function
            'ElseIf IsNumeric(txtFName.Text) Then
            '    SetMsg(LblMsg, "First Name must be String Type.")
            '    isValidate = False
            '    Exit Function
            'End If

            'If IsNumeric(txtLName.Text) Then
            '    SetMsg(LblMsg, "Last Name must be String Type.")
            '    isValidate = False
            '    Exit Function
            'End If
            'If Chk(txtPPIN.Text) <> "" Then
            '    If Len(txtPPIN.Text) <> 6 Then
            '        SetMsg(LblMsg, "Permanent Pin No. Must be of Six Digit.")
            '        isValidate = False
            '        Exit Function
            '    End If
            'End If
            'If Chk(txtMPIN.Text) <> "" Then
            '    If Len(txtMPIN.Text) <> 6 Then
            '        SetMsg(LblMsg, "Mailing Pin No. Must be of Six Digit.")
            '        isValidate = False
            '        Exit Function
            '    End If
            'End If

            'If Not IsDate(dtpDOB.DateValue) Then
            '    SetMsg(LblMsg, "Invalid Date Of Birth.")
            '    isValidate = False
            '    Exit Function
            'End If

            'If Not IsDate(DtpDOM.DateValue) Then
            '    SetMsg(LblMsg, "Invalid Date Of Marriage.")
            '    isValidate = False
            '    Exit Function
            'End If
            isValidate = True
        Catch ex As Exception
            isValidate = False
            SetMsg(LblMsg, ex.Message & " : (cmdSave_Click)")
        End Try
    End Function

#End Region

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("EmpExplorer.aspx?SrNo=61")
    End Sub

#Region "      Coding Related to Language      "

    Sub CreateLanguages()
        Try
            Dim StrQry As String, i As Int16

            ViewState("DtLang") = New DataTable


            StrQry = "Select Lang_Code, Lang_Name from LangMast"
            Session("DalObj").GetSqlDataTable(ViewState("DtLang"), StrQry)

            Dim TableCell1 As HtmlTableCell, TableCell2 As HtmlTableCell
            Dim Chk1 As CheckBox, Chk2 As CheckBox, Chk3 As CheckBox
            Dim TableCell3 As HtmlTableCell, TableCell4 As HtmlTableCell
            Dim TableCell5 As HtmlTableCell, TableCell6 As HtmlTableCell
            Dim TableCell7 As HtmlTableCell, TableCell8 As HtmlTableCell
            Dim TablRow As HtmlTableRow, LblName As Label

            Session("TBLLang") = TblLanguages

            For i = 0 To ViewState("DtLang").Rows.Count - 1

                Chk1 = New CheckBox
                Chk1.ID = "Chk1" & ViewState("DtLang").Rows(i).Item("Lang_Code")
                Chk2 = New CheckBox
                Chk2.ID = "Chk2" & ViewState("DtLang").Rows(i).Item("Lang_Code")
                Chk3 = New CheckBox
                Chk3.ID = "Chk3" & ViewState("DtLang").Rows(i).Item("Lang_Code")

                LblName = New Label
                LblName.ID = "Lbl" & i
                LblName.Style.Item("runat") = "server"
                LblName.Width = Unit.Pixel(80)

                If (i Mod 2) = 0 Then

                    TableCell1 = New HtmlTableCell
                    TableCell1.Width = "20%"
                    LblName.Text = "  " & ViewState("DtLang").Rows(i).Item("Lang_Name")
                    TableCell1.Controls.Add(LblName)

                    TableCell2 = New HtmlTableCell
                    TableCell2.Width = "10%"
                    TableCell2.InnerHtml = "&nbsp;&nbsp;"
                    TableCell2.Controls.Add(Chk1)

                    TableCell3 = New HtmlTableCell
                    TableCell3.Width = "10%"
                    TableCell3.InnerHtml = "&nbsp;&nbsp;"
                    TableCell3.Controls.Add(Chk2)

                    TableCell4 = New HtmlTableCell
                    TableCell4.Width = "10%"
                    TableCell4.InnerHtml = "&nbsp;&nbsp;"
                    TableCell4.Controls.Add(Chk3)

                Else
                    TablRow = New HtmlTableRow

                    TableCell5 = New HtmlTableCell
                    TableCell5.Width = "20%"
                    LblName.Text = "  " & ViewState("DtLang").Rows(i).Item("Lang_Name")
                    TableCell5.Controls.Add(LblName)

                    TableCell6 = New HtmlTableCell
                    TableCell6.Width = "10%"
                    TableCell6.InnerHtml = "&nbsp;&nbsp;"
                    TableCell6.Controls.Add(Chk1)

                    TableCell7 = New HtmlTableCell
                    TableCell7.Width = "10%"
                    TableCell7.InnerHtml = "&nbsp;&nbsp;"
                    TableCell7.Controls.Add(Chk2)

                    TableCell8 = New HtmlTableCell
                    TableCell8.Width = "10%"
                    TableCell8.InnerHtml = "&nbsp;&nbsp;"
                    TableCell8.Controls.Add(Chk3)

                    TablRow.Cells.Add(TableCell1)
                    TablRow.Cells.Add(TableCell2)
                    TablRow.Cells.Add(TableCell3)
                    TablRow.Cells.Add(TableCell4)
                    TablRow.Cells.Add(TableCell5)
                    TablRow.Cells.Add(TableCell6)
                    TablRow.Cells.Add(TableCell7)
                    TablRow.Cells.Add(TableCell8)

                    Session("TBLLang").Rows.Add(TablRow)

                End If

            Next

            If (ViewState("DtLang").Rows.Count Mod 2) <> 0 Then
                TablRow = New HtmlTableRow
                TableCell5 = New HtmlTableCell
                TableCell5.ColSpan = 4

                TableCell5.Width = "50%"
                TableCell5.InnerHtml = "&nbsp;"
                TablRow.Cells.Add(TableCell1)
                TablRow.Cells.Add(TableCell2)
                TablRow.Cells.Add(TableCell3)
                TablRow.Cells.Add(TableCell4)
                TablRow.Cells.Add(TableCell5)
                Session("TBLLang").Rows.Add(TablRow)
            End If

        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (FillLanguages)")
        End Try
    End Sub
    'Binding for Language from workhistory
    Sub CountKnownLanguage()
        Try
            Dim i As Int16, StrLang As String, TotLang As String, CountLanguage() As String

            For i = 0 To ViewState("DtLang").Rows.Count - 1

                StrLang = ""

                If ((CType(Session("TBLLang").FindControl("Chk1" & ViewState("DtLang").Rows(i).Item("Lang_Code")), CheckBox).Checked = True) Or (CType(Session("TBLLang").FindControl("Chk2" & ViewState("DtLang").Rows(i).Item("Lang_Code")), CheckBox).Checked = True) Or (CType(Session("TBLLang").FindControl("Chk3" & ViewState("DtLang").Rows(i).Item("Lang_Code")), CheckBox).Checked = True)) Then

                    If CType(Session("TBLLang").FindControl("Chk1" & ViewState("DtLang").Rows(i).Item("Lang_Code")), CheckBox).Checked = True Then
                        StrLang = ViewState("DtLang").Rows(i).Item("Lang_Code") & "^1"
                    Else
                        StrLang = ViewState("DtLang").Rows(i).Item("Lang_Code") & "^0"
                    End If

                    If CType(Session("TBLLang").FindControl("Chk2" & ViewState("DtLang").Rows(i).Item("Lang_Code")), CheckBox).Checked = True Then
                        StrLang = StrLang & "1"
                    Else
                        StrLang = StrLang & "0"
                    End If

                    If CType(Session("TBLLang").FindControl("Chk3" & ViewState("DtLang").Rows(i).Item("Lang_Code")), CheckBox).Checked = True Then
                        StrLang = StrLang & "1"
                    Else
                        StrLang = StrLang & "0"
                    End If
                End If

                If StrLang <> "" Then
                    TotLang = TotLang & StrLang & "|"
                End If
            Next

            If TotLang <> "" Then
                ViewState("Languages") = Mid(TotLang, 1, Len(TotLang) - 1)
            Else
                ViewState("Languages") = ""
            End If

            'CountLanguage = Split(Mid(TotLang, 1, Len(TotLang) - 1), "|")

            'For i = 0 To CountLanguage.Length - 1
            '    ViewState("Languages") = ViewState("Languages") & CountLanguage(i) & ", "
            'Next

            'ViewState("Languages") = Mid(Trim(ViewState("Languages")), 1, Len(Trim(ViewState("Languages"))) - 1)

        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (CountKnownLanguage)")
        End Try
    End Sub

    Sub FillLanguage(ByVal Value As String)
        Try
            Dim Countstr() As String, LangKnown() As String, i As Int16, j As Int16
            Dim Str1 As String
            Countstr = Split(Value, "|")

            For i = 0 To Countstr.Length - 1
                LangKnown = Split(Countstr(i), "^")
                For j = 0 To LangKnown(1).Length - 1
                    If Left(LangKnown(1), 1) = 1 Then
                        CType(Session("TBLLang").FindControl("Chk" & j + 1 & LangKnown(0)), CheckBox).Checked = True
                    End If
                    'LangKnown(1) = Mid(LangKnown(1), Len(LangKnown(1)), Len(LangKnown(1)) - j + 1)
                    'Str1 = Str1 & " ; " & Left(LangKnown(1), 1)
                    LangKnown(1) = Right(LangKnown(1), LangKnown(1).Length - 1)
                Next
            Next
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (Page_Load)")
        End Try
    End Sub

#Region "     Blank Language    "

    Sub BlankLanguage()
        Try
            Dim i As Int16

            For i = 0 To Session("TBLLang").Controls.Count - 1
                If TypeOf Session("TBLLang").Controls(i) Is CheckBox Then CType(Session("TBLLang").Controls(i), CheckBox).Checked = False
                If Session("TBLLang").Controls(i).HasControls Then
                    BlankSubLanguage(Session("TBLLang").Controls)
                End If
            Next

        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (BlankLanguage)")
        End Try
    End Sub

    Sub BlankSubLanguage(ByVal ctrls As System.Web.UI.ControlCollection)
        Dim i As Int16

        For i = 0 To ctrls.Count - 1
            If TypeOf ctrls.Item(i) Is CheckBox Then CType(ctrls.Item(i), CheckBox).Checked = False
            If ctrls.Item(i).HasControls Then
                BlankSubLanguage(ctrls.Item(i).Controls)
            End If
        Next
    End Sub

#End Region

#End Region

    ' comment by Ravi
    'Private Sub cmdNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNew.Click
    '    Try
    '        Dim newEmp_Code As String, strPrefix As String, strLen As Single, EmpBool As String, sMailId As String

    '        EmpBool = Chk(Session("DalObj").ExecuteCommand("Select Emp_Code_GEN From CompMast", , DalObj.ExecutionType.ExecuteScalar))

    '        If UCase(EmpBool) = "Y" Then
    '            newEmp_Code = Session("BalObj").GetNextNumber("HrdMast", "Emp_Code")
    '            strPrefix = Chk(Session("DalObj").ExecuteCommand("Select Emp_Code_Prefix From CompMast", , DalObj.ExecutionType.ExecuteScalar))
    '            strLen = ChkN(Session("DalObj").ExecuteCommand("Select Emp_Code_Len From CompMast", , DalObj.ExecutionType.ExecuteScalar))
    '            Session("EM_CD") = Left(strPrefix & newEmp_Code.PadLeft(strLen, "0"), strLen)
    '        Else
    '            Session("EM_CD") = ""
    '        End If
    '        ClearAll(Me)
    '        cmbFood.SelectedIndex = 0
    '        CmbBGroup.SelectedIndex = 0
    '        cmbFatherHusband.SelectedIndex = 0
    '        cmbReligion.SelectedIndex = 0
    '        CmbMStatus.SelectedIndex = 0
    '        sMailId = Chk(Session("DalObj").ExecuteCommand("Select IO_Date_Field From CompMast", , DalObj.ExecutionType.ExecuteScalar))
    '        txtEmailID.Text = sMailId
    '        ViewState("Value") = "NEW"
    '        txtEM_CD.Text = Session("EM_CD")
    '        sAction.Text = "NEW"
    '        ImgEmp.ImageUrl = ""
    '    Catch ex As Exception
    '        SetMsg(LblMsg, ex.Message & " (cmdNew_Click)")
    '    End Try
    'End Sub

    Sub SetTextControlLength()
        Try
            Dim StrQr As String, DtNew As New DataTable, i As Int16
            StrQr = "SELECT SYSCOLUMNS.NAME, SYSCOLUMNS.LENGTH FROM SYSCOLUMNS INNER JOIN SYSOBJECTS ON  SYSCOLUMNS.ID = SYSOBJECTS.ID WHERE SYSOBJECTS.NAME='HRDMAST'"
            CType(Session("DalObj"), DAL.DataLayer).GetSqlDataTable(DtNew, StrQr)

            Dim j As Int16
            For i = 0 To DtNew.Rows.Count - 1
                If Not IsNothing(Page.FindControl("txt" & DtNew.Rows(i).Item(0))) Then
                    CType(Page.FindControl("txt" & DtNew.Rows(i).Item(0)), TextBox).MaxLength = DtNew.Rows(i).Item(1)
                End If
            Next

        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " SetTextControlLength()")
        End Try
    End Sub

    Public Overrides Sub Dispose()
        GC.Collect()

    End Sub

    Private Sub txtHobbies_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub txtEM_CD_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Try
        '    If Trim(txtEM_CD.Text) <> "" Then Session("EM_CD") = Trim(txtEM_CD.Text)
        '    DisplayRecord()
        '    RegisterStartupScript("txtEM_CD", "<SCRIPT language='javascript'>document.getElementById('txtFName').focus() </SCRIPT>")
        'Catch ex As Exception
        '    SetMsg(LblMsg, ex.Message & " : (txtEM_CD_TextChanged)")
        'End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNew.Click
        ' By Ravi on 24 nov 2006
        Try
            Dim newEmp_Code As String, strPrefix As String, strLen As Single, EmpBool As String, sMailId As String

            EmpBool = Chk(Session("DalObj").ExecuteCommand("Select Emp_Code_GEN From CompMast", , DalObj.ExecutionType.ExecuteScalar))

            If UCase(EmpBool) = "Y" Then
                'Comment by ravi
                'newEmp_Code = Session("BalObj").GetNextNumber("HrdMast", "Emp_Code")
                'strPrefix = Chk(Session("DalObj").ExecuteCommand("Select Emp_Code_Prefix From CompMast", , DalObj.ExecutionType.ExecuteScalar))
                'strLen = ChkN(Session("DalObj").ExecuteCommand("Select Emp_Code_Len From CompMast", , DalObj.ExecutionType.ExecuteScalar))
                'Session("EM_CD") = Left(strPrefix & newEmp_Code.PadLeft(strLen, "0"), strLen)

                '--------------------------------------By Ravi-----------------------------------------------
                newEmp_Code = Session("Dalobj").ExecuteCommand("select count(emp_code)+1 from hrdmast", , DalObj.ExecutionType.ExecuteScalar)
                strPrefix = Chk(Session("DalObj").ExecuteCommand("Select Emp_Code_Prefix From CompMast", , DalObj.ExecutionType.ExecuteScalar))
                strLen = ChkN(Session("DalObj").ExecuteCommand("Select Emp_Code_Len From CompMast", , DalObj.ExecutionType.ExecuteScalar))
Check:          newEmp_Code = newEmp_Code.PadLeft(strLen - strPrefix.Length, "0")
                newEmp_Code = strPrefix & newEmp_Code
                If ChkN(Session("DalObj").ExecuteCommand("select count(emp_code) from hrdmast where emp_code= '" & newEmp_Code & "'", , DalObj.ExecutionType.ExecuteScalar)) > 0 Then
                    newEmp_Code = Right(newEmp_Code, strLen - strPrefix.Length)
                    newEmp_Code = CType(newEmp_Code, Integer) + 1
                    'newEmp_Code = strPrefix & newEmp_Code
                    GoTo check
                    'Else
                    '  newEmp_Code = strPrefix & newEmp_Code
                End If
                '------------------------------------------------------------------------------------
                Session("EM_CD") = newEmp_Code
            Else
                Session("EM_CD") = ""
            End If
            ClearAll(Me)
            cmbFood.SelectedIndex = 0
            CmbBGroup.SelectedIndex = 0
            cmbFatherHusband.SelectedIndex = 0
            cmbReligion.SelectedIndex = 0
            CmbMStatus.SelectedIndex = 0
            sMailId = Chk(Session("DalObj").ExecuteCommand("Select IO_Date_Field From CompMast", , DalObj.ExecutionType.ExecuteScalar))
            txtEmailID.Text = sMailId
            ViewState("Value") = "NEW"
            txtEM_CD.Text = Session("EM_CD")
            sAction.Text = "NEW"
            ImgEmp.ImageUrl = ""
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " (cmdNew_Click)")
        End Try

    End Sub

    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Try
            Dim strSQL As String
            Dim i As Int16
            strSQL = " Update HrdMast Set " & _
                                     " FName = Null " & _
                                     " MName = Null " & _
                                     " LName = Null " & _
                                     " MAddr1 = Null " & _
                                     " MAddr2 = Null " & _
                                     " MAddr3 = Null " & _
                                     " MCity = Null " & _
                                     " MState = Null " & _
                                     " MPIN = Null " & _
                                     " MCountry = Null " & _
                                     " MPhone = Null " & _
                                     " PAddr1 = Null " & _
                                     " PAddr2 = Null " & _
                                     " PAddr3 = Null " & _
                                     " EmergencyName = Null " & _
                                     " EmergencyAddress = Null " & _
                                     " EmergencyPhoneNo = Null " & _
                                     " DrName = Null " & _
                                     " DrAddress = Null " & _
                                     " DrPhoneNo = Null " & _
                                     " PCity = Null " & _
                                     " PState = Null " & _
                                     " PPIN = Null " & _
                                     " PCountry = Null " & _
                                     " PEmailId = Null " & _
                                     " PPhone = Null " & _
                                     " SEX = Null " & _
                                     " MStatus = Null " & _
                                     " DOM = Null " & _
                                     " DOB = Null " & _
                                     " DOV = Null " & _
                                     " FathHusb = Null " & _
                                     " FathHusbName = Null " & _
                                     " Domicile = Null " & _
                                     " Nationality = Null " & _
                                     " BirthPlace = Null " & _
                                     " Hobbies = Null " & _
                                     " BloodGrp = Null " & _
                                     " FoodChoice = Null " & _
                                     " Religion = Null " & _
                                     " PassportNo = Null " & _
                                     " DLNo = Null " & _
                                     " EmailID = Null " & _
                                     " Languages = Null " & _
                                     " FathHusbOcupation = Null " & _
                                     " Spouse = Null " & _
                                     " Where Emp_Code = '" & Chk(txtEM_CD.Text) & "'"
            i = Session("DalObj").ExecuteCommand(strSQL)
            If i > 0 Then
                SetMsg(LblMsg, "Record is Deleted successfully")

            Else
                SetMsg(LblMsg, "Record is not  Deleted successfully")
            End If


        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " BtnDelete_Click()")
        End Try

    End Sub
End Class