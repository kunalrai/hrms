Partial Class FrmESIChallan
    Inherits System.Web.UI.Page
    Dim dt As DataTable
    Dim ds As New DataSet
    Dim BAL As BAL.BLayer
    Dim DAL As DAL.DataLayer
    Dim VarComp_Code As String
    Dim DtDateS, DtDateE As Date
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
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If
        DAL = Session("DalObj")
        BAL = Session("BalObj")
        DtDateS = Format(FY_Start, "dd/MMM/yyyy")
        DtDateE = Format(FY_End, "dd/MMM/yyyy")
        'Put user code to initialize the page here
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

            BAL.FillCombo(cmbChallanNo, "select challanNo,challanNo from EsiChallanMast", True)
            BAL.FillCombo(cmbMonth, "select (Right('0' + DateName(d,PayDate),2) + '/' + Left(DateName(mm,PayDate),3) + '/' + DateName(yy,PayDate)) As paydate, datename(MM,paydate) + ' - ' + datename(yyyy,paydate) as mon FROM monupdate where PayDate>='" & DtDateS & "' and PayDate<='" & DtDateE & "' group by paydate", True)
            BAL.FillCombo(CmbBankName, "Select Bank_Code, Bank_Name from BANKMAST order by Bank_Name", True)
            BAL.FillCombo(CmbLocation, "select Loc_Code, Loc_Name from LocMast order by Loc_Name", True)
            Dtp1.Text = Format(Date.Today, "dd/MMM/yyyy")
            Dtp2.Text = Format(Date.Today, "dd/MMM/yyyy")
            cmbChallanDate.Text = Format(Date.Today, "dd/MMM/yyyy")
            btnNew_Click(sender, Nothing)
        End If
        CmdSave.Attributes.Add("onclick", "return ValidateCtrl();")
        CmdDelete.Attributes.Add("onclick", "return ConfirmDelete();")
    End Sub

    Private Function fillDataGrid()
        Try
            Dim vardate As String
            vardate = cmbMonth.SelectedValue
            Dim dt As New DataTable
            Dim strsql As String
            strsql = "SELECT PayHist.Emp_Code, HrdHistQry.Emp_Name, PayHist.ESISalary, PayHist.ESI, PayHist.EESI FROM PayHist, HrdHistQry  WHERE PayHist.Emp_Code = HrdHistQry.Emp_Code And PayHist.PayDate = HrdHistQry.PayDate And PayHist.ESISalary <> 0 And Payhist.Paydate='" & vardate & "' ORDER BY PayHist.Emp_Code "
            DAL.GetSqlDataTable(dt, strsql)
            If dt.Rows.Count > 0 Then
                GrdRecords.DataSource = dt
                GrdRecords.DataBind()
                Trgrid.Visible = True
                TotalPage.Text = "Total Pages:->    " & GrdRecords.PageCount
                CurrentPage.Text = "Current Page:->   " & GrdRecords.CurrentPageIndex + 1
                NoEmp.Text = "Total Employee:->    " & dt.Rows.Count
                MonthOf.Text = "Month Of   " & cmbMonth.SelectedItem.Text
            End If
        Catch ex As Exception
            LblError.Text = ex.Message
        End Try
    End Function
    Private Function ShowRecords()
        Try
            Dim vardate As String
            vardate = cmbMonth.SelectedValue
            Dim dt As New DataTable
            Dim strsql As String
            strsql = "select distinct Hrd.Emp_Name,E.Emp_Code,E.ESISalary,E.ESI,E.EESI from HrdHistQry Hrd inner join EsiChallanTran E on Hrd.Emp_Code=E.Emp_Code  where ChallanNo='" & Txtrefno.Text & "'"
            DAL.GetSqlDataTable(dt, strsql)

            If dt.Rows.Count > 0 Then
                GrdRecords.DataSource = dt
                GrdRecords.DataBind()
                TotalPage.Text = "Total Pages:->    " & GrdRecords.PageCount
                CurrentPage.Text = "Current Page:->   " & GrdRecords.CurrentPageIndex + 1
                NoEmp.Text = "Total Employee:->    " & dt.Rows.Count
                MonthOf.Text = "Month Of   " & cmbMonth.SelectedItem.Text
            End If
        Catch ex As Exception
            LblError.Text = ex.Message
        End Try
    End Function
    Private Sub cmbMonth_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbMonth.SelectedIndexChanged
        Try

            fillDataGrid()
            FieldSum()
            DisplayComp()
            Cal()

            If cmbMonth.SelectedItem.Text = "" Then
                LblError.Text = "Sorry No Match(s) Found: "
            End If

        Catch ex As Exception
            LblError.Text = ex.Message
        End Try
    End Sub
    Public Function FieldSum()
        Try
            Dim cnt, cnt1 As Integer
            Dim dtdata1 As New DataTable
            Dim strsql As String
            Dim vardate As String
            vardate = cmbMonth.SelectedValue
            strsql = "SELECT PayHist.Emp_Code, HrdHistQry.Emp_Name, PayHist.ESISalary, PayHist.ESI, PayHist.EESI FROM PayHist, HrdHistQry  WHERE PayHist.Emp_Code = HrdHistQry.Emp_Code And PayHist.PayDate = HrdHistQry.PayDate And PayHist.ESISalary <> 0 And Payhist.Paydate='" & vardate & "' ORDER BY PayHist.Emp_Code "
            DAL.GetSqlDataTable(dtdata1, strsql)
            cnt1 = dtdata1.Rows.Count
            Dim Temp1, Temp2, Temp3, temp4 As Integer

            Temp1 = 0
            Temp2 = 0
            Temp3 = 0
            temp4 = 0

            For cnt = 0 To cnt1 - 1
                Temp1 = Temp1 + ChkN(dtdata1.Rows(cnt).Item("ESISALARY"))
                Temp2 = Temp2 + ChkN(dtdata1.Rows(cnt).Item("ESI"))
                Temp3 = Temp3 + ChkN(dtdata1.Rows(cnt).Item("EESI"))
            Next

            TxtWages.Text = Temp1
            TxtTotalWages.Text = Temp1
            TxtEmpCont.Text = Temp2
            TxtEmprCont.Text = Temp3
            TxtTotalContri.Text = Temp2 + Temp3

        Catch ex As Exception
            LblError.Text = ex.Message
        End Try
    End Function
    Private Function CloseGrid()
        Try
            GrdRecords.Visible = False
            TotalPage.Visible = False
            CurrentPage.Visible = False
            NoEmp.Visible = False
            MonthOf.Visible = False
        Catch ex As Exception
            LblError.Text = ex.Message
        End Try
    End Function
    Private Function Cal()
        Try
            'Dim I, q, k, l, m, n, o, p As Integer
            'Dim a, b, c, d, e, f, g As Double
            Dim I, k, l As Integer
            Dim a, b As Double

            I = CType(TxtWages.Text, Integer)

            a = CType(TxtEC1.Text, Double)
            b = CType(TxtEC1.Text, Double)

            k = (I * a) / 100
            TxtEC2.Text = k

            l = (I * b) / 100
            TxtER2.Text = l

        Catch ex As Exception
            LblError.Text = ex.Message
        End Try
    End Function
    Private Function DisplayRecord()
        Try
            Dim sqlstr As String
            Dim dtMast As New DataTable
            sqlstr = "Select * from EsiChallanMast where ChallanNo='" & Chk(Txtrefno.Text) & "'"
            DAL.GetSqlDataTable(dtMast, sqlstr)
            If dtMast.Rows.Count > 0 Then
                With dtMast.Rows(0)
                    cmbChallanDate.Text = Format(CDate(.Item("ChallanDate")), "dd/MMM/yyyy")
                    Dtp1.Text = Format(CDate(.Item("RemittanceDate")), "dd/MMM/yyyy")
                    Dtp2.Text = Format(CDate(.Item("ChqDate")), "dd/MMM/yyyy")
                    TxtWages.Text = ChkN(.Item("ESISalary"))
                    TxtChequeNo.Text = Chk(.Item("ChqNo"))
                    TxtEC1.Text = ChkN(.Item("ESIPer"))
                    TxtER1.Text = ChkN(.Item("EESIPer"))
                    TxtEC2.Text = ChkN(.Item("ESI"))
                    TxtER2.Text = ChkN(.Item("EESI"))
                    ChkCombo(CmbLocation, .Item("Loc_Code"))
                    ChkCombo(CmbBankName, .Item("Bank_Code"))
                    TxtChequeNo.Text = Chk(.Item("ChqNo"))
                    TxtEmpCont.Text = ChkN(.Item("ESI"))
                    TxtEmprCont.Text = ChkN(.Item("EESI"))
                    cmbMonth.SelectedValue = Format((.Item("PAYDATE")), "dd/MMM/yyyy")
                    Dim i, j As Integer
                    i = ChkN(.Item("ESI"))
                    j = ChkN(.Item("EESI"))
                    TxtTotalContri.Text = i + j
                End With
            End If
            ShowRecords()
        Catch ex As Exception
            LblError.Text = ex.Message
        End Try
    End Function
    Private Function DisplayComp()
        Try
            Dim sqlstr As String
            Dim dt As New DataTable
            sqlstr = "Select * from compmast"
            DAL.GetSqlDataTable(dt, sqlstr)
            If dt.Rows.Count > 0 Then
                TxtEC1.Text = ChkN(dt.Rows(0).Item("ESI_PER"))
                TxtER1.Text = ChkN(dt.Rows(0).Item("EESI_PER"))
            End If
        Catch ex As Exception
            LblError.Text = ex.Message
        End Try
    End Function
    Private Sub btnList_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnList.Click
        cmbChallanNo.Visible = True
        Txtrefno.Visible = False
        btnList.Visible = False
        btnNew.Visible = False
    End Sub
    Private Sub cmbChallanNo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbChallanNo.SelectedIndexChanged
        Try
            If cmbChallanNo.SelectedIndex <> cmbChallanNo.Items.Count - 1 Then
                LblError.Text = ""
                Txtrefno.Visible = True
                btnList.Visible = True
                btnNew.Visible = True
                cmbChallanNo.Visible = False
                Txtrefno.ReadOnly = True
                Txtrefno.ToolTip = cmbChallanNo.SelectedValue
                Txtrefno.Text = cmbChallanNo.SelectedItem.Text
                ViewState("ReqCode") = Txtrefno.Text
                ViewState("Action") = "MODIFY"
                DisplayRecord()
                fillDataGrid()
                FieldSum()
                DisplayComp()
            End If
        Catch ex As Exception
            SetMsg(LblError, ex.Message)
        End Try
    End Sub
    '''''Private Sub Txtrefno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txtrefno.TextChanged
    '''''    Try
    '''''        Dim SQLSTR As String
    '''''        Dim cnt As Int16
    '''''        cnt = ChkN(DAL.ExecuteCommand("select Count(ChallanNo)  from ESICHALLANMAST where ChallanNo='" & Chk(Txtrefno.Text) & "'", , DAL.ExecutionType.ExecuteScalar))
    '''''        If cnt > 0 Then
    '''''            'DisplayRecord()
    '''''            DisplayRecord()
    '''''            'ShowRecords()
    '''''            fillDataGrid()
    '''''            FieldSum()
    '''''            DisplayComp()
    '''''            Txtrefno.Visible = True
    '''''            btnList.Visible = True
    '''''            btnNew.Visible = True
    '''''            cmbChallanNo.Visible = False
    '''''            Txtrefno.ToolTip = cmbChallanNo.SelectedValue
    '''''            Txtrefno.Text = cmbChallanNo.SelectedItem.Text
    '''''            ViewState("ReqCode") = Txtrefno.Text
    '''''            ViewState("Action") = "MODIFY"
    '''''        Else
    '''''            SetMsg(LblError, "This Challan No. Does Not Exists.")
    '''''        End If
    '''''    Catch ex As Exception
    '''''        SetMsg(LblError, ex.Message)
    '''''    End Try
    '''''End Sub
    Function IsValidate() As Boolean
        'If Trim(Txtrefno.Text) = "" Then
        '    SetMsg(LblError, "Enter the Challan No.")
        '    Return False
        'End If
        'If Trim(cmbMonth.SelectedValue) = "" Then
        '    SetMsg(LblError, "Please select The Month")
        '    Return False
        'End If

        'If CmbLocation.SelectedItem.Text = "" Then
        '    SetMsg(LblError, "Please Select a Location")
        '    Return False
        'End If
        If Chk(cmbChallanDate.Text) = "" Then
            SetMsg(LblError, "Please Enter The Challan Date.")
            Return False
        End If

        Return True

    End Function
    Private Sub CmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdSave.Click
        Dim Tran As SqlClient.SqlTransaction
        Try
            Dim SqlStr, str1 As String
            Dim cnt, challanno As Integer
            Dim dt As New DataTable
            Dim vardate = cmbMonth.SelectedValue
            If Not IsValidate() Then Exit Sub
            str1 = "SELECT PayHist.Emp_Code, HrdHistQry.Emp_Name, PayHist.ESISalary, PayHist.ESI, PayHist.EESI FROM PayHist, HrdHistQry  WHERE PayHist.Emp_Code = HrdHistQry.Emp_Code And PayHist.PayDate = HrdHistQry.PayDate And PayHist.ESISalary <> 0 And Payhist.Paydate='" & vardate & "' ORDER BY PayHist.Emp_Code "
            DAL.GetSqlDataTable(dt, str1)
            VarComp_Code = Session("DalObj").ExecuteCommand(" Select Comp_code From Compmast", , DAL.ExecutionType.ExecuteScalar)
            If ViewState("iAction") = "ADDNEW" Then
                challanno = DAL.ExecuteCommand("Select Count(ChallanNo) from EsiChallanMast where ChallanNo='" & Chk(Txtrefno.Text) & "'", , DAL.ExecutionType.ExecuteScalar)
                If challanno > 0 Then
                    SetMsg(LblError, "This Challan No. Already Exists. Please Enter Another Challan No.")
                    Exit Sub
                End If
            End If
            Tran = DAL.StartTransaction("Save")
            If ViewState("Action") = "ADDNEW" Then
                SqlStr = " Insert EsiChallanMast(ChallanNo, ChallanDate, PayDate, Comp_Code, Loc_Code, ESISalary, ESIPer, ESI, EESIPer, EESI, RemittanceDate, Bank_Code, ChqNo, ChqDate ) Values ('" & _
                          Chk(Txtrefno.Text) & "', '" & _
                          Chk(cmbChallanDate.Text) & "', '" & _
                          cmbMonth.SelectedValue & "', '" & _
                          VarComp_Code & "', '" & _
                          CmbLocation.SelectedValue & "', '" & _
                          ChkN(TxtWages.Text) & "', '" & _
                          ChkN(TxtEC1.Text) & "', '" & _
                          ChkN(TxtEC2.Text) & "', '" & _
                          ChkN(TxtER1.Text) & "', '" & _
                          ChkN(TxtER2.Text) & "', '" & _
                          Chk(Dtp1.Text) & "', '" & _
                          CmbBankName.SelectedValue & "', '" & _
                          Chk(TxtChequeNo.Text) & "', '" & _
                          Chk(Dtp2.Text) & "')"
                DAL.ExecuteCommand(SqlStr, Tran)
                For cnt = 0 To dt.Rows.Count - 1
                    SqlStr = "Insert into EsiChallanTran(ChallanNo,Emp_Code,EsiSalary,ESI,EESI)"
                    SqlStr &= "Values('"
                    SqlStr &= Chk(Txtrefno.Text) & "','"
                    SqlStr &= Chk(dt.Rows(cnt).Item("Emp_Code")) & "','"
                    SqlStr &= ChkN(dt.Rows(cnt).Item("ESISalary")) & "','"
                    SqlStr &= ChkN(dt.Rows(cnt).Item("ESI")) & "','"
                    SqlStr &= ChkN(dt.Rows(cnt).Item("EESI")) & "')"
                    DAL.ExecuteCommand(SqlStr, Tran)
                Next
            End If
            '***********************Upadte EsiChallanMast ****************************************
            If ViewState("Action") = "MODIFY" Then
                Dim StrQuery As String
                StrQuery = "Update EsiChallanMast Set ChallanDate='" & Chk(cmbChallanDate.Text) & "',Loc_Code='" & CmbLocation.SelectedValue & "',ESISALARY='" & ChkN(TxtTotalWages.Text) & "',ESIPER='" & ChkN(TxtEC1.Text) & "',ESI='" & ChkN(TxtEC2.Text) & "', EESIPer='" & ChkN(TxtER1.Text) & "',EESI='" & ChkN(TxtER2.Text) & "',RemittanceDate='" & Chk(Dtp1.Text) & "',Bank_Code='" & CmbBankName.SelectedValue & "',ChqNo='" & Chk(TxtChequeNo.Text) & "',ChqDate='" & Chk(Dtp2.Text) & "'"
                DAL.ExecuteCommand(StrQuery, Tran)

                '==============================================Update ESI Challan ============================
                StrQuery = "Delete from EsiChallanTran where ChallanNo='" & Chk(Txtrefno.Text) & "'"
                DAL.ExecuteCommand(StrQuery, Tran)
                If dt.Rows.Count > 0 Then
                    For cnt = 0 To dt.Rows.Count - 1
                        SqlStr = "Insert into EsiChallanTran(ChallanNo,Emp_Code,EsiSalary,ESI,EESI)"
                        SqlStr &= "Values('"
                        SqlStr &= Chk(Txtrefno.Text) & "','"
                        SqlStr &= Chk(dt.Rows(cnt).Item("Emp_Code")) & "','"
                        SqlStr &= ChkN(dt.Rows(cnt).Item("ESISalary")) & "','"
                        SqlStr &= ChkN(dt.Rows(cnt).Item("ESI")) & "','"
                        SqlStr &= ChkN(dt.Rows(cnt).Item("EESI")) & "')"
                        DAL.ExecuteCommand(SqlStr, Tran)
                    Next
                End If
            End If
            Tran.Commit()
            btnNew_Click(sender, Nothing)
            SetMsg(LblError, "ESI Challan Entry Has been Saved Successfully.")
        Catch ex As Exception
            SetMsg(LblError, ex.Message & "CmdSave_Click")
            Tran.Rollback()
        Finally
            If Not IsNothing(Tran) Then Tran.Dispose()
        End Try
    End Sub

    Private Sub CmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Private Sub CmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdDelete.Click
        Try
            Dim sqlstr As String
            If Chk(Txtrefno.Text) <> "" Then
                sqlstr = "Delete From EsiChallanMast where ChallanNo='" & Chk(Txtrefno.Text) & "'"
                DAL.ExecuteCommand(sqlstr)
                sqlstr = "Delete From EsiChallanTran where ChallanNo='" & Chk(Txtrefno.Text) & "'"
                DAL.ExecuteCommand(sqlstr)
                btnNew_Click(sender, Nothing)
                SetMsg(LblError, "Challan Has Been Deleted Permanently.")
            Else
                SetMsg(LblError, "Please Select The Challan No. From The List.")
                Exit Sub
            End If
        Catch ex As Exception
            SetMsg(LblError, ex.Message & "CmdDelete_Click")
        End Try
    End Sub
    Sub Blank()
        Try
            ClearAll(Me)
            cmbChallanDate.Text = Format(Date.Today, "dd/MMM/yyyy")
            Dtp1.Text = Format(Date.Today, "dd/MMM/yyyy")
            Dtp2.Text = Format(Date.Today, "dd/MMM/yyyy")
            cmbChallanType.SelectedValue = ""
            cmbMonth.SelectedValue = ""
            CmbLocation.SelectedValue = ""
        Catch ex As Exception
            SetMsg(LblError, ex.Message & "Blank()")
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNew.Click
        Try
            Blank()
            LblError.Text = ""
            ViewState("Action") = "ADDNEW"
            CmbBankName.SelectedValue = ""
            CmbLocation.SelectedValue = ""
            cmbMonth.SelectedValue = ""
            Txtrefno.ReadOnly = False
            Trgrid.Visible = False
            BAL.FillCombo(cmbChallanNo, "select challanNo,challanNo from EsiChallanMast", True)
        Catch ex As Exception
            SetMsg(LblError, ex.Message & "btnNew_Click")
        End Try
    End Sub
End Class
