Partial Class FrmTdsChallan
    Inherits System.Web.UI.Page
    Dim dt As DataTable
    Dim ds As New DataSet
    Dim BAL As BAL.BLayer
    Dim DAL As DAL.DataLayer
    Dim VarComp_Code As String
    Dim Challan_No As Integer
    Dim DtDateS, DtDateE As Date

#Region " Web Form Designer Generated Code "

#End Region

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
        DAL = Session("DalObj")
        BAL = Session("BalObj")
        Try
            DtDateS = Format(FY_Start, "dd/MMM/yyyy")
            DtDateE = Format(FY_End, "dd/MMM/yyyy")
            'ViewState("Add1") = "Grid"
            If Not IsPostBack Then
                'By Ravi 21 Nov
                Dim SrNo As Int16
                SrNo = Request.QueryString.Item("SrNo") + Request.QueryString.Item("MODULEID")
                Dim bSuccess As Boolean
                Select Case CheckRight(SrNo)
                    Case MdlHRMS.AccessType.FullAccess, MdlHRMS.AccessType.SuperUser
                        bSuccess = True
                    Case MdlHRMS.AccessType.ReadonlyAccess, MdlHRMS.AccessType.Restricted
                        bSuccess = False
                End Select
                BtnOk.Visible = bSuccess

                '------------------------------------

                FillDataGrid()
                BAL.FillCombo(DrlChallanNo, "CHALLAN_No", "CHALLAN_No", "TDSCHALLAN", True)
                BAL.FillCombo(DrlBankName, "Bank_Code", "Bank_Name", "BANKMAST", True)

                BAL.FillCombo(DrlMonth, "select (Right('0' + DateName(d,PayDate),2) + '/' + Left(DateName(mm,PayDate),3) + '/' + DateName(yy,PayDate)) As paydate, datename(MM,paydate) + ' - ' + datename(yyyy,paydate) as mon FROM monupdate where PayDate>='" & DtDateS & "' and PayDate<='" & DtDateE & "' group by paydate", True)

                btnNew_Click(sender, Nothing)
                FillDataGrid()
                Dtppaydate.DateValue = Date.Today
            End If
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & "Page Load")
        End Try

    End Sub
    Private Function FillDataGrid()
        Try
            Dim StrSql, str As String
            Session("dt") = New DataTable
            If ChkShow.Checked = True Then
                StrSql = "SELECT P.Emp_Code,P.ITAX,P.Surch,P.CessPm,Isnull(P.ITAX,0)+Isnull(P.Surch,0)+Isnull(P.CessPm,0) As Total," & _
                         "H.Emp_Name from Payhist P INNER JOIN HRDHISTQRY H ON P.Emp_Code=H.Emp_Code " & _
                         "AND P.Paydate=H.Paydate Where P.Paydate='" & DrlMonth.SelectedValue & "'"
            Else
                StrSql = "SELECT P.Emp_Code,P.ITAX,P.Surch,P.CessPm,Isnull(P.ITAX,0)+Isnull(P.Surch,0)+Isnull(P.CessPm,0) As Total, " & _
                         "H.Emp_Name from Payhist P INNER JOIN HRDHISTQRY H ON P.Emp_Code=H.Emp_Code" & _
                         "AND P.Paydate=H.Paydate Where P.Paydate='" & DrlMonth.SelectedValue & "'"
            End If
            DAL.GetSqlDataTable(Session("dt"), StrSql)
            If Session("dt").Rows.Count > 0 Then
                TxtIntrest.Enabled = True
                TxtIntrest.Text = ""
                ShowRecords.DataSource = Session("dt")
                ShowRecords.DataBind()
                TotalPage.Text = "Total Pages:->    " & ShowRecords.PageCount
                CurrentPage.Text = "Current Page:->   " & ShowRecords.CurrentPageIndex + 1
                NoEmp.Text = "Total Employee:->    " & Session("dt").Rows.Count
                MonthOf.Text = "Month Of   " & DrlMonth.SelectedItem.Text
                TotalGrid()
                FieldSum()
            Else
                CloseGrid()
            End If
        Catch ex As Exception
            LblMsg.Text = ex.Message & "FillDataGrid"

        End Try
    End Function
    Private Function FillForm16()
        Try
            Dim ChNo As Integer
            Dim Paydate As String
            ChNo = ChkN(Txtrefno.Text)
            Dim StrSql1 As String
            Session("dt") = New DataTable
            StrSql1 = "Select H.EMP_NAME,F.TDS_AMT as itax,F.SURCH_AMT AS SURCH,H.EMP_CODE,F.CESS_AMT AS CessPm, Isnull(F.TDS_AMT,0)+IsNull(F.SURCH_AMT,0)+IsNull(F.CESS_AMT,0) AS Total  FROM HRDMASTQRY H INNER JOIN FORM16 F ON H.EMP_CODE=F.EMP_CODE WHERE F.CHALLAN_NO='" & ChNo & "' and Paydate='" & DrlMonth.SelectedValue & "'"
            DAL.GetSqlDataTable(Session("dt"), StrSql1)
            ShowRecords.DataSource = Session("dt")
            ShowRecords.DataBind()
        Catch ex As Exception
            LblMsg.Text = ex.Message & "FillForm16"
        End Try
    End Function
    Private Sub BtnPayRoll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPayRoll.Click
        Try
            LblMsg.Text = ""
            If DrlMonth.SelectedItem.Text = "" Then
                SetMsg(LblMsg, "Please Select the Month from the List")
                Exit Sub
            Else
                FillDataGrid()
                ShowRecords.Visible = True
                TotalPage.Visible = True
                CurrentPage.Visible = True
                NoEmp.Visible = True
                MonthOf.Visible = True
            End If
        Catch ex As Exception
            LblMsg.Text = ex.Message & "BtnPayRoll_Click"
        End Try

    End Sub
    Private Sub ShowRecords_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs)
        Try
            ShowRecords.CurrentPageIndex = e.NewPageIndex
            FillDataGrid()
        Catch ex As Exception
            LblMsg.Text = ex.Message & "ShowRecords_PageIndexChanged"
        End Try

    End Sub
    Private Sub DrlBankName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DrlBankName.SelectedIndexChanged
        Try
            Dim Str, StrSql As String
            dt = New DataTable
            StrSql = "SELECT BSR_CODE FROM BANKMAST WHERE Bank_Code='" & Chk(DrlBankName.SelectedValue) & "'"
            DAL.GetSqlDataTable(dt, StrSql)
            If dt.Rows.Count > 0 Then
                TxtBSRCode.Text = Chk(dt.Rows(0).Item("BSR_CODE"))
            End If
        Catch ex As Exception
            LblMsg.Text = ex.Message & "DrlBankName_SelectedIndexChanged"
        End Try
    End Sub
    Private Sub BtnCencel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCencel.Click
        CloseGrid()
    End Sub
    Private Function CloseGrid()
        Try
            TotalPage.Visible = False
            CurrentPage.Visible = False
            NoEmp.Visible = False
            MonthOf.Visible = False
        Catch ex As Exception
            LblMsg.Text = ex.Message & "CloseGrid()"
        End Try
    End Function
    Sub OnTextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim Item As DataGridItem
            Dim dr As DataRow
            Dim dt As DataTable
            Dim TDS, Surch, Cess As Integer
            Item = CType(sender, Control).NamingContainer()
            TDS = ChkN(CType(Item.Cells(2).Controls(1), TextBox).Text)
            Surch = ChkN(CType(Item.Cells(3).Controls(1), TextBox).Text)
            Cess = ChkN(CType(Item.Cells(4).Controls(1), TextBox).Text)
            Dim Code As String = Item.Cells(0).Text
            dt = Session("dt")
            dr = dt.Select("[EMP_CODE]='" & Code & "'").GetValue(0)
            Dim fName As String = dr.Item("EMP_NAME")
            dr.Item("Total") = TDS + Surch + Cess
            dr.Item("ITAX") = TDS
            dr.Item("Surch") = Surch
            dr.Item("CessPm") = Cess
            dt.AcceptChanges()
            dr.AcceptChanges()
            ShowRecords.DataSource = dt
            ShowRecords.DataBind()
            TotalGrid()
        Catch ex As Exception
            LblMsg.Text = ex.Message & "OnTextChanged()"
        End Try
    End Sub

    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Private Function SaveData()
        Dim Tran As SqlClient.SqlTransaction
        Try
            Dim cnt As Integer
            'Dim dt As DataTable
            Dim i, j As Integer
            Dim StrSql As String
            If Not IsValidate() Then Exit Function
            Tran = DAL.StartTransaction("Save")

            If ViewState("Action") = "ADDNEW" Then
                '======================Submit The Records In Header Format in TDSCHALLAN Table
                VarComp_Code = Session("DalObj").ExecuteCommand(" Select Comp_code From Compmast", , DAL.ExecutionType.ExecuteScalar)
                Dim StrQuery As String
                StrQuery = "insert into TDSCHALLAN(Challan_No,Challan_Date,PayDate,Bank_Code,Tds_Amt,BSR_Code,Surch_Amt,Cess_Amt,Comp_code,interest,ChqDDNo)"
                StrQuery &= " Values('"
                StrQuery &= Txtrefno.Text & "','"
                StrQuery &= Format(CDate(Dtppaydate.Text), "dd/MMM/yyyy") & "','"
                StrQuery &= DrlMonth.SelectedValue & "','"
                StrQuery &= Chk(DrlBankName.SelectedValue) & "','"
                StrQuery &= ChkN(TxtTDS.Text) & "','"
                StrQuery &= Chk(TxtBSRCode.Text) & "','"
                StrQuery &= ChkN(TxtSurcharge.Text) & "','"
                StrQuery &= ChkN(TxtCess.Text) & "','"
                StrQuery &= VarComp_Code & "','"
                StrQuery &= ChkN(TxtIntrest.Text) & "','"
                StrQuery &= Chk(TxtCheckNo.Text) & "')"
                DAL.ExecuteCommand(StrQuery, Tran)
                '======================Submit The Records In Form 16  Table
                For cnt = 0 To ShowRecords.Items.Count - 1
                    StrSql = "Insert into form16(Challan_No,Comp_Code,Emp_Code,Tds_Amt,Surch_Amt,Paydate,Cess_Amt)"
                    StrSql &= "Values('"
                    StrSql &= Chk(Txtrefno.Text) & "','"
                    StrSql &= Chk(VarComp_Code) & "','"
                    StrSql &= Chk(ShowRecords.Items(cnt).Cells(0).Text) & "','"
                    StrSql &= ChkN(CType(ShowRecords.Items(cnt).Cells(2).Controls(1), TextBox).Text) & "','"
                    StrSql &= ChkN(CType(ShowRecords.Items(cnt).Cells(3).Controls(1), TextBox).Text) & "','"
                    StrSql &= DrlMonth.SelectedValue & "','"
                    StrSql &= ChkN(CType(ShowRecords.Items(cnt).Cells(4).Controls(1), TextBox).Text) & "')"
                    DAL.ExecuteCommand(StrSql, Tran)
                Next
            Else
                If ViewState("Action") = "MODIFY" Then
                    '==============Update FrmTdsChallan
                    For cnt = 0 To ShowRecords.Items.Count - 1
                        Dim QlsStr As String
                        Dim paydt As Date
                        paydt = Format(Dtppaydate.DateValue, "dd/MMM/yyyy")

                        QlsStr = "Update TdsChallan Set TDS_AMT='" & ChkN(TxtTDS.Text) & "', Surch_Amt='" & ChkN(TxtSurcharge.Text) & "',Paydate='" & CDate(DrlMonth.SelectedValue) & "',BSR_CODE='" & Chk(TxtBSRCode.Text) & "',Cess_Amt='" & Chk(TxtCess.Text) & "',INTEREST='" & ChkN(TxtIntrest.Text) & "',ChqDDNo='" & Chk(TxtCheckNo.Text) & "',Challan_Date='" & paydt & "' Where Challan_No = '" & Chk(Txtrefno.Text) & "' "
                        DAL.ExecuteCommand(QlsStr, Tran)
                        '============================Submit 
                        QlsStr = "Update Form16 set TDS_AMT='" & ChkN(CType(ShowRecords.Items(cnt).Cells(2).Controls(1), TextBox).Text) & "', SURCH_AMT='" & ChkN(CType(ShowRecords.Items(cnt).Cells(3).Controls(1), TextBox).Text) & "',CESS_AMT='" & ChkN(CType(ShowRecords.Items(cnt).Cells(3).Controls(1), TextBox).Text) & "' where CHALLAN_NO='" & Chk(Txtrefno.Text) & "'"
                        DAL.ExecuteCommand(QlsStr, Tran)
                    Next
                End If
            End If
            Tran.Commit()
            SetMsg(LblMsg, "Records Saved Sucessfully.")
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & "SaveData")
            Tran.Rollback()
        Finally
            If Not IsNothing(Tran) Then Tran.Dispose()
        End Try
    End Function
    Function IsValidate() As Boolean
        Dim Code As Int16
        Try
            If Chk(DrlMonth.SelectedItem.Text) = "" Then
                SetMsg(LblMsg, "Please Select The Month.")
                Return False
            End If
            Dim cnt As Int16
            cnt = Session("DalObj").ExecuteCommand("select count(Challan_No) from Tdschallan where challan_no='" & Chk(Txtrefno.Text) & "'", , DAL.ExecutionType.ExecuteScalar)
            If cnt > 0 Then
                SetMsg(LblMsg, "This Challan No Already Exist. Please Enter Another Challan No.")
                Return False
            End If
            Return True
        Catch ex As Exception
            LblMsg.Text = ex.Message & "IsValidate()"
        End Try
    End Function
    Private Sub BtnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOk.Click
        IsValidate()
        SaveData()
    End Sub
    Private Sub btnList_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnList.Click
        DrlChallanNo.Visible = True
        Txtrefno.Visible = False
        btnList.Visible = False
        btnNew.Visible = False
    End Sub
    Private Sub DrlChallanNo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DrlChallanNo.SelectedIndexChanged
        Try
            If DrlChallanNo.SelectedIndex <> DrlChallanNo.Items.Count - 1 Then
                Txtrefno.Visible = True
                btnList.Visible = True
                btnNew.Visible = True
                DrlChallanNo.Visible = False
                Txtrefno.ToolTip = DrlChallanNo.SelectedValue
                Txtrefno.Text = DrlChallanNo.SelectedItem.Text
                ViewState("ReqCode") = Txtrefno.Text
                ViewState("Action") = "MODIFY"
                TDSChallan()
                FillForm16()
                TotalGrid()
                FieldSum()
            End If
        Catch ex As Exception
            LblMsg.Text = ex.Message & "DrlChallanNo_SelectedIndexChanged()"
        End Try
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNew.Click
        Try
            ClearAll(Me)
            Txtrefno.Text = BAL.GetNextNumber("TdsChallan", "Challan_No")
            Txtrefno.ToolTip = Chk(Txtrefno.Text)
            Txtrefno.Text = Txtrefno.Text.PadLeft(4, "0")
            ViewState("ReqCode") = Chk(Txtrefno.Text)
            DrlMonth.SelectedValue = ""
            BAL.FillCombo(DrlChallanNo, "CHALLAN_No", "CHALLAN_No", "TDSCHALLAN", True)
            BAL.FillCombo(DrlMonth, "select (Right('0' + DateName(d,PayDate),2) + '/' + Left(DateName(mm,PayDate),3) + '/' + DateName(yy,PayDate)) As paydate, datename(MM,paydate) + ' - ' + datename(yyyy,paydate) as mon FROM monupdate where PayDate>='" & DtDateS & "' and PayDate<='" & DtDateE & "' group by paydate", True)
            Txtrefno.Text = ""
            ShowRecords.Visible = False
            ViewState("Action") = "ADDNEW"
            LblMsg.Visible = False
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (btnNew_Click)")
        End Try
    End Sub
    '''''Private Sub Txtrefno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txtrefno.TextChanged
    '''''    Dim StrQl As String
    '''''    Dim Dt As New DataTable
    '''''    Dim paydate = DrlMonth.SelectedValue
    '''''    Dim cnt As Int16
    '''''    'StrQl = "select count(Challan_No) from Tdschallan where challan_no='" & Txtrefno.Text & "'"
    '''''    cnt = Session("DalObj").ExecuteCommand("select count(Challan_No) from Tdschallan where challan_no='" & Chk(Txtrefno.Text) & "'", , DAL.ExecutionType.ExecuteScalar)
    '''''    If cnt > 0 Then
    '''''        LblMsg.Text = "This Challan No. Already Exist."
    '''''        TDSChallan()
    '''''        FillForm16()
    '''''        ViewState("Action") = "MODIFY"
    '''''    Else
    '''''        ViewState("Action") = "ADDNEW"
    '''''        Dim var As String
    '''''        var = Txtrefno.Text
    '''''        'clearAll(Me)
    '''''        Txtrefno.Text = var
    '''''        LblMsg.Text = ""
    '''''    End If
    '''''    'StrQl = "Select Challan_No from form16 where Challan_No='" & Txtrefno.Text & "' and Paydate='" & paydate & "'"
    '''''End Sub

    Private Function TotalGrid()
        Try
            Dim cnt, cnt1 As Integer
            cnt1 = ShowRecords.Items.Count
            Dim Temp1, Temp2, Temp3, temp4 As Integer
            Temp1 = 0
            Temp2 = 0
            Temp3 = 0
            For cnt = 0 To cnt1 - 1
                If Chk(CType(ShowRecords.Items(cnt).Controls(2).Controls(1), TextBox).Text) = "" Then
                    CType(ShowRecords.Items(cnt).Controls(2).Controls(1), TextBox).Text = 0
                End If
                If Chk(CType(ShowRecords.Items(cnt).Controls(3).Controls(1), TextBox).Text) = "" Then
                    CType(ShowRecords.Items(cnt).Controls(3).Controls(1), TextBox).Text = 0
                End If
                If Chk(CType(ShowRecords.Items(cnt).Controls(4).Controls(1), TextBox).Text) = "" Then
                    CType(ShowRecords.Items(cnt).Controls(4).Controls(1), TextBox).Text = 0
                End If
                Temp1 = Temp1 + CType(ShowRecords.Items(cnt).Controls(2).Controls(1), TextBox).Text
                Temp2 = Temp2 + CType(ShowRecords.Items(cnt).Controls(3).Controls(1), TextBox).Text
                Temp3 = Temp3 + CType(ShowRecords.Items(cnt).Controls(4).Controls(1), TextBox).Text
                temp4 = Temp1 + Temp3 + Temp2
                TxtTDS1.Text = Temp1
                TxtCess1.Text = Temp3
                TxtSurchage1.Text = Temp2
            Next
            TxtTDS1.Text = Temp1
            TxtSurchage1.Text = Temp2
            TxtCess1.Text = Temp3
            TxtTotal1.Text = temp4

        Catch ex As Exception
            LblMsg.Text = ex.Message & " TotalGrid()"
        End Try
    End Function
    Public Function FieldSum()
        Try
            Dim cnt, cnt1 As Integer
            Dim dtdata1 As New DataTable
            Dim strsql As String
            If ChkShow.Checked = True Then
                strsql = "SELECT P.Emp_Code,P.ITAX,P.Surch,P.CessPm,Isnull(P.ITAX,0)+Isnull(P.Surch,0)+Isnull(P.CessPm,0) AS Total,H.Emp_Name from Payhist P INNER JOIN HRDHISTQRY H ON P.Emp_Code=H.Emp_Code AND P.Paydate=H.Paydate Where P.Paydate='" & DrlMonth.SelectedValue & "' "
            Else
                strsql = "SELECT P.Emp_Code,P.ITAX,P.Surch,P.CessPm,Isnull(P.ITAX,0)+Isnull(P.Surch,0)+Isnull(P.CessPm,0) AS Total,H.Emp_Name from Payhist P INNER JOIN HRDHISTQRY H ON P.Emp_Code=H.Emp_Code AND P.Paydate=H.Paydate Where P.Paydate='" & DrlMonth.SelectedValue & "' and H.Ltype='1' "
            End If
            DAL.GetSqlDataTable(dtdata1, strsql)
            cnt1 = dtdata1.Rows.Count
            Dim Temp1, Temp2, Temp3, temp4 As Integer
            Temp1 = 0
            Temp2 = 0
            Temp3 = 0
            For cnt = 0 To cnt1 - 1
                If IsDBNull(dtdata1.Rows(cnt).Item("ITAX")) Then
                    dtdata1.Rows(cnt).Item("ITAX") = 0
                End If
                If IsDBNull(dtdata1.Rows(cnt).Item("Surch")) Then
                    dtdata1.Rows(cnt).Item("Surch") = 0
                End If
                If IsDBNull(dtdata1.Rows(cnt).Item("CessPm")) Then
                    dtdata1.Rows(cnt).Item("CessPm") = 0
                End If
                Temp1 = Temp1 + dtdata1.Rows(cnt).Item("Itax")
                Temp2 = Temp2 + dtdata1.Rows(cnt).Item("Surch")
                Temp3 = Temp3 + dtdata1.Rows(cnt).Item("CessPm")
                temp4 = Temp1 + Temp3 + Temp2

                TxtTDS2.Text = Temp1
                TxtCess2.Text = Temp3
                TxtSurchage2.Text = Temp2
            Next
            TxtTDS2.Text = Temp1
            TxtSurchage2.Text = Temp2
            TxtCess2.Text = Temp3
            TxtTotal2.Text = temp4

            TxtTDS.Text = Temp1
            TxtSurcharge.Text = Temp2
            TxtCess.Text = Temp3
            TxtTotal.Text = temp4
        Catch ex As Exception
            LblMsg.Text = ex.Message & " FieldSum()"
        End Try
    End Function
    Private Function TDSChallan()
        Try
            Dim ChNo As Integer
            Dim Paydate As String
            ChNo = ChkN(Txtrefno.Text)
            Dim StrSql1 As String
            Dim cnt As String
            Dim dttemp = New DataTable
            StrSql1 = "select * from tdschallan WHERE CHALLAN_NO='" & ChNo & "'"
            DAL.GetSqlDataTable(dttemp, StrSql1)
            If dttemp.Rows.Count > 0 Then
                'BAL.FillCombo(DrlMonth, "Select Distinct Paydate,datename(MM,paydate) + '-' +datename(YYYY,paydate) as Mon from TDSCHALLAN where Challan_No='" & ChNo & "'")

                DrlBankName.SelectedValue = Chk(dttemp.Rows(0).Item("Bank_Code"))
                TxtBSRCode.Text = Chk(dttemp.Rows(0).Item("BSR_CODE"))
                TxtTDS.Text = ChkN(dttemp.Rows(0).Item("TDS_AMT"))
                TxtCheckNo.Text = Chk(dttemp.Rows(0).Item("ChqDDNo"))
                TxtSurcharge.Text = ChkN(dttemp.Rows(0).Item("SURCH_AMT"))
                TxtCess.Text = ChkN(dttemp.Rows(0).Item("CESS_AMT"))
                DrlMonth.SelectedValue = Format(dttemp.Rows(0).Item("PAYDATE"), "dd/MMM/yyyy")
                'DrlMonth.SelectedItem.Text = (dttemp.Rows(0).Item("PAYDATE"))
                TxtIntrest.Text = ChkN(dttemp.Rows(0).Item("INTEREST"))
                'TxtTotal.Text = ChkN(dttemp.Rows(0).Item("TOTAL_Amt"))
                With dttemp.Rows(0)
                    If Not IsDBNull(.Item("Challan_Date")) Then
                        Dtppaydate.DateValue = .Item("Challan_Date")
                    End If
                End With
            End If
        Catch ex As Exception
            LblMsg.Text = ex.Message & " TDSChallan()"
        End Try
    End Function
    Private Sub TxtIntrest_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtIntrest.TextChanged
        Try
            If TxtTotal.Text = "" Then
                TxtIntrest.Enabled = False
            Else
                TxtIntrest.Enabled = True
                Dim i As Integer
                i = ChkN(TxtIntrest.Text)
                TxtTotal.Text = TxtTotal.Text + i
            End If
        Catch ex As Exception
            LblMsg.Text = ex.Message & " TxtIntrest_TextChanged"
        End Try
    End Sub
    Private Sub ShowRecords_PageIndexChanged1(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs)
        ShowRecords.CurrentPageIndex = e.NewPageIndex
        FillDataGrid()
    End Sub
End Class

