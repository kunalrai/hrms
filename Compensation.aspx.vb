Partial Class Compensation
    Inherits System.Web.UI.Page
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Dim oDal As DAL.DataLayer
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents File1 As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents lblDesc As System.Web.UI.WebControls.Label
    Protected WithEvents TXTTXT As System.Web.UI.HtmlControls.HtmlSelect

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
#Region "  On Load "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Server.GetLastError Is Nothing Then SetMsg(LblMsg, "")
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If
        oDal = Session("DalObj")
        Try
            Dim SrNo As Int16
            SrNo = Request.QueryString.Item("SrNo") + Request.QueryString.Item("MODULEID")
            If Not IsPostBack Then
                Dim bSuccess As Boolean
                Select Case CheckRight(SrNo)
                    Case MdlHRMS.AccessType.FullAccess, MdlHRMS.AccessType.SuperUser
                        bSuccess = True
                    Case MdlHRMS.AccessType.ReadonlyAccess
                        bSuccess = False
                    Case MdlHRMS.AccessType.Restricted
                        Response.Redirect(Request.UrlReferrer.ToString)
                        Exit Sub
                End Select
                cmdSave.Visible = bSuccess
            End If

            '''If CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules <> "" Then
            '''    If InStr(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, SrNo & "-") <> 0 Then
            '''        Dim int As Int16, st As String
            '''        int = InStr(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, SrNo)
            '''        st = Right(Mid(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, int, Len(SrNo & "-") + 1), 1)

            '''        If st = "S" Then
            '''            txtEM_CD.ReadOnly = False
            '''        Else
            '''            txtEM_CD.ReadOnly = True
            '''            cmdSave.Visible = False
            '''        End If
            '''    Else
            '''        txtEM_CD.ReadOnly = True
            '''        cmdSave.Visible = False
            '''        SetMsg(LblRights, "You are not authorised to view data on this page.")
            '''        'Response.Write("<BR><center><B></B></center>")
            '''        Exit Sub
            '''    End If
            '''End If
            'If Session("LoginUser").UserGroup <> "ADMIN" Then
            '    txtEM_CD.ReadOnly = True
            '    cmdSave.Visible = False
            'Else
            '    txtEM_CD.ReadOnly = False
            'End If
            If Not IsPostBack Then
                txtEM_CD.Text = Session("EM_CD")
                GetEarningsData()
                GetDeduData()
                GetReimData()
                GetPerksData()
                GetInvestmentData()
                'BY Santosh 27/11/2006
                ' GetFurnData()
                ''''''''''''''''
                GetOthersData()
                DisplayRecord()
            End If
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (Page_Load)")
        End Try
    End Sub

#End Region

#Region " Getting Data From PaySetUp : Private Functions "

    Private Sub GetEarningsData()
        Try
            'request.Form.Item  
            Session("dtEarnings") = New DataTable
            Dim StrSql As String
            'comment by Ravi
            ' StrSql = " SELECT SNO,FIELD_NAME,FIELD_DESC,PRINT_NAME,FIELD_TYPE,FLD_Categ,FIELD_LEN,Fld_PayMast,Fld_HrdMast,Case WHEN Fld_PayMast = 'Y' Then 'PayMast' WHEN Fld_HrdMast = 'Y' Then 'HrdMast' End As TableName,cast(NULL as SmallDatetime) AS Field_D,0 AS Field_N,'' AS Field_C FROM PaySetup Where FIELD_TYPE = 'N' And FLD_PAYMAST = 'Y' And FLD_Categ = 1 ORDER BY SNO"
            'By Ravi on 30 Nov 2006
            StrSql = " SELECT SNO,FIELD_NAME,FIELD_DESC,PRINT_NAME,FIELD_TYPE,FLD_Categ,FIELD_LEN,Fld_PayMast,Fld_HrdMast,Case WHEN Fld_PayMast = 'Y' Then 'PayMast' WHEN Fld_HrdMast = 'Y' Then 'HrdMast' End As TableName,cast(NULL as SmallDatetime) AS Field_D,cast(0.00 as float )AS Field_N,'' AS Field_C FROM PaySetup Where FIELD_TYPE = 'N' And FLD_PAYMAST = 'Y' And FLD_Categ = 1 ORDER BY SNO"
            Session("DalObj").GetSqlDataTable(Session("dtEarnings"), StrSql)
            GrdEarnings.DataSource = Session("dtEarnings")
            GrdEarnings.DataBind()
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (GetEarningsData)")
        End Try
    End Sub

    Private Sub GetDeduData()
        Try
            Session("dtDeductions") = New DataTable
            Dim StrSql As String
            'Comment By Ravi on 30 nov 2006
            ' StrSql = " SELECT SNO,FIELD_NAME,FIELD_DESC,PRINT_NAME,FIELD_TYPE,FLD_Categ,FIELD_LEN,Fld_PayMast,Fld_HrdMast,Case WHEN Fld_PayMast = 'Y' Then 'PayMast' WHEN Fld_HrdMast = 'Y' Then 'HrdMast' End As TableName,cast(NULL as SmallDatetime) AS Field_D,0 AS Field_N,'' AS Field_C FROM PaySetup Where FIELD_TYPE = 'N' And FLD_PAYMAST = 'Y' And FLD_Categ = 2 ORDER BY SNO"
            'By Ravi on 30 nov 2006
            StrSql = " SELECT SNO,FIELD_NAME,FIELD_DESC,PRINT_NAME,FIELD_TYPE,FLD_Categ,FIELD_LEN,Fld_PayMast,Fld_HrdMast,Case WHEN Fld_PayMast = 'Y' Then 'PayMast' WHEN Fld_HrdMast = 'Y' Then 'HrdMast' End As TableName,cast(NULL as SmallDatetime) AS Field_D,cast(0.00 as float)AS Field_N,'' AS Field_C FROM PaySetup Where FIELD_TYPE = 'N' And FLD_PAYMAST = 'Y' And FLD_Categ = 2 ORDER BY SNO"
            Session("DalObj").GetSqlDataTable(Session("dtDeductions"), StrSql)
            GrdDeductions.DataSource = Session("dtDeductions")
            GrdDeductions.DataBind()
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (GetDeduData())")
        End Try
    End Sub
    Private Sub GetReimData()
        Try
            Session("dtReim") = New DataTable
            Dim StrSql As String
            StrSql = "SELECT SNO,FIELD_NAME,FIELD_DESC,'' As WEF,cast(0 as float) As Opening,0 As Budget,0 As Prorata,0 As SplBudget,0 As Reimbursed,0 As Balance,FLD_Month,FLD_Rated FROM PaySetup Where FLD_Categ = 4 ORDER BY SNO"
            Session("DalObj").GetSqlDataTable(Session("dtReim"), StrSql)
            GrdReim.DataSource = Session("dtReim")
            GrdReim.DataBind()
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (GetReimData())")
        End Try
    End Sub
    ''********************************
    ''By SANTOSH 27/11/20006
    ''********************************
    'Private Sub GetFurnData()
    '    Try
    '        'request.Form.Item  
    '        Session("dtFurn") = New DataTable
    '        Dim StrSql As String
    '        StrSql = " SELECT SNO,FIELD_NAME,FIELD_DESC,PRINT_NAME,FIELD_TYPE,FLD_Categ,FIELD_LEN,Fld_PayMast,Fld_HrdMast,Case WHEN Fld_PayMast = 'Y' Then 'PayMast' WHEN Fld_HrdMast = 'Y' Then 'HrdMast' End As TableName,cast(NULL as SmallDatetime) AS Field_D,0 AS Field_N,'' AS Field_C FROM PaySetup Where FIELD_TYPE = 'N' And FLD_PAYMAST = 'Y' And FLD_Categ = 5 ORDER BY SNO"
    '        Session("DalObj").GetSqlDataTable(Session("dtFurn"), StrSql)
    '        GrdFurn.DataSource = Session("dtFurn")
    '        GrdFurn.DataBind()
    '    Catch ex As Exception
    '        SetMsg(LblMsg, ex.Message & " : (GetFurnData)")
    '    End Try
    'End Sub
    '****************************


    Private Sub GetPerksData()
        Try
            Session("dtPerquisites") = New DataTable
            Dim StrSql As String
            'comment by Ravi
            'StrSql = "SELECT SNO,FIELD_NAME,FIELD_DESC,PRINT_NAME,FIELD_TYPE,FLD_Categ,FIELD_LEN,Fld_PayMast,Fld_HrdMast,Case WHEN Fld_PayMast = 'Y' Then 'PayMast' WHEN Fld_HrdMast = 'Y' Then 'HrdMast' End As TableName,cast(NULL as SmallDatetime) AS Field_D,0 AS Field_N,'' AS Field_C FROM PaySetup Where FIELD_TYPE = 'N' And FLD_PAYMAST = 'Y' And FLD_Categ = 5 ORDER BY SNO"
            'By Ravi 30 Nov 
            StrSql = "SELECT SNO,FIELD_NAME,FIELD_DESC,PRINT_NAME,FIELD_TYPE,FLD_Categ,FIELD_LEN,Fld_PayMast,Fld_HrdMast,Case WHEN Fld_PayMast = 'Y' Then 'PayMast' WHEN Fld_HrdMast = 'Y' Then 'HrdMast' End As TableName,cast(NULL as SmallDatetime) AS Field_D,cast(0 as float) AS Field_N,'' AS Field_C FROM PaySetup Where FIELD_TYPE = 'N' And FLD_PAYMAST = 'Y' And FLD_Categ = 5 ORDER BY SNO"

            Session("DalObj").GetSqlDataTable(Session("dtPerquisites"), StrSql)
            GrdPerquisites.DataSource = Session("dtPerquisites")
            GrdPerquisites.DataBind()
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (GetPerks())")
        End Try


    End Sub
    Private Sub GetInvestmentData()
        Try
            Session("dtInvestment") = New DataTable
            Dim StrSql As String
            'comment by Ravi
            'StrSql = "SELECT SNO,FIELD_NAME,FIELD_DESC,PRINT_NAME,FIELD_TYPE,FLD_Categ,FIELD_LEN,Fld_PayMast,Fld_HrdMast,Case WHEN Fld_PayMast = 'Y' Then 'PayMast' WHEN Fld_HrdMast = 'Y' Then 'HrdMast' End As TableName,cast(NULL as SmallDatetime) AS Field_D,0 AS Field_N,'' AS Field_C FROM PaySetup Where FIELD_TYPE = 'N' And FLD_PAYMAST = 'Y' And FLD_Categ = 6 ORDER BY SNO"
            'By Ravi
            StrSql = "SELECT SNO,FIELD_NAME,FIELD_DESC,PRINT_NAME,FIELD_TYPE,FLD_Categ,FIELD_LEN,Fld_PayMast,Fld_HrdMast,Case WHEN Fld_PayMast = 'Y' Then 'PayMast' WHEN Fld_HrdMast = 'Y' Then 'HrdMast' End As TableName,cast(NULL as SmallDatetime) AS Field_D,cast(0 as float)AS Field_N,'' AS Field_C FROM PaySetup Where FIELD_TYPE = 'N' And FLD_PAYMAST = 'Y' And FLD_Categ = 6 ORDER BY SNO"
            Session("DalObj").GetSqlDataTable(Session("dtInvestment"), StrSql)
            GrdInvestment.DataSource = Session("dtInvestment")
            GrdInvestment.DataBind()
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (GetInvestment())")
        End Try

    End Sub

    Private Sub GetOthersData()
        Try
            Session("dtOthers") = New DataTable
            Dim StrSql As String
            'comment By Ravi
            'StrSql = " Select SNO,FIELD_NAME,FIELD_DESC,PRINT_NAME,FIELD_TYPE,FLD_Categ,FIELD_LEN,Fld_PayMast,Fld_HrdMast,Case WHEN Fld_PayMast = 'Y' Then 'PayMast' WHEN Fld_HrdMast = 'Y' Then 'HrdMast' End As TableName,GetDate() AS Field_D,NULL AS Field_N,'' AS Field_C From PaySetUp " & _
            '         " Inner Join " & _
            '         " (Select [Name] From SysColumns Where [ID] IN (Select [ID] From SysObjects Where [Name] Like 'HrdMast' Or [Name] Like 'PayMast' )) SysQry " & _
            '         " On SysQry.[Name] = PaySetUp.FIELD_NAME " & _
            '         " Where (Fld_Default is Null Or Fld_Default <> 'Y') And (Fld_HrdMast = 'Y' Or Fld_PayMast = 'Y') And (Field_Type <> 'N' Or Fld_Categ = 8) " & _
            '         " ORDER BY SNO "
            'By Ravi

            StrSql = " Select SNO,FIELD_NAME,FIELD_DESC,PRINT_NAME,FIELD_TYPE,FLD_Categ,FIELD_LEN,Fld_PayMast,Fld_HrdMast,Case WHEN Fld_PayMast = 'Y' Then 'PayMast' WHEN Fld_HrdMast = 'Y' Then 'HrdMast' End As TableName,GetDate() AS Field_D,cast(NULL as float)AS Field_N,'' AS Field_C From PaySetUp " & _
                    " Inner Join " & _
                    " (Select [Name] From SysColumns Where [ID] IN (Select [ID] From SysObjects Where [Name] Like 'HrdMast' Or [Name] Like 'PayMast' )) SysQry " & _
                    " On SysQry.[Name] = PaySetUp.FIELD_NAME " & _
                    " Where (Fld_Default is Null Or Fld_Default <> 'Y') And (Fld_HrdMast = 'Y' Or Fld_PayMast = 'Y') And (Field_Type <> 'N' Or Fld_Categ = 8) " & _
                    " ORDER BY SNO "
            Session("DalObj").GetSqlDataTable(Session("dtOthers"), StrSql)
            GrdOthers.DataSource = Session("dtOthers")
            GrdOthers.DataBind()
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (GetOthersData())")
        End Try
    End Sub
    Private Sub DisplayRecord()
        Dim dtPayMast As New DataTable
        Dim dtHrdMast As New DataTable
        Dim dtReimMast As New DataTable
        Dim dtFurnBill As New DataTable
        Dim DR As DataRow
        Try
            Dim StrSql As String, EmpName As Object
            Dim i As Int16
            'StrSql = "SELECT * FROM PayMast WHERE FinYear = " & Year(FY_Start) & " AND Emp_Code = '" & Chk(txtEM_CD.Text) & "'"
            EmpName = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(" Select Emp_Name From HrdMastQry Where Emp_Code = '" & Chk(txtEM_CD.Text) & "'" & Session("UserCodes"), , DAL.DataLayer.ExecutionType.ExecuteScalar)
            If (Not IsDBNull(EmpName)) And (Not IsNothing(EmpName)) Then
                LblName.Text = EmpName
                ViewState("Value") = True
            Else
                LblName.Text = ""

                Dim Code As Object
                Code = Session("DalObj").ExecuteCommand("Select Emp_Code From HrdMastQry Where Emp_Code = '" & Chk(txtEM_CD.Text) & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)

                If Code <> "" Then
                    'by ravi
                    SetMsg(LblMsg, "Access is dinied because ,This Employee Exist For Other Location.")
                    'Else
                    ' SetMsg(LblMsg, "This Employee does not exist .")
                End If

                ViewState("Value") = False
            End If

            StrSql = "SELECT * FROM PayMast Inner Join HrdMast On PayMast.Emp_Code = HrdMast.Emp_Code WHERE PayMast.FinYear = " & Year(FY_Start) & " AND PayMast.Emp_Code = '" & Chk(txtEM_CD.Text) & "'" & Session("UserCodes")
            Session("DalObj").GetSqlDataTable(dtPayMast, StrSql)



            '*************  Earning  Data  Display  *************
            If dtPayMast.Rows.Count > 0 Then
                For i = 0 To Session("dtEarnings").Rows.Count - 1
                    Session("dtEarnings").Rows(i).Item("Field_N") = ChkN(dtPayMast.Rows(0).Item(Session("dtEarnings").Rows(i).Item("FIELD_NAME")))
                Next
            Else
                For i = 0 To Session("dtEarnings").Rows.Count - 1
                    Session("dtEarnings").Rows(i).Item("Field_N") = 0
                Next
            End If
            GrdEarnings.DataSource = Session("dtEarnings")
            GrdEarnings.DataBind()


            '*************  Deductions  Data  Display  *************

            If dtPayMast.Rows.Count > 0 Then
                For i = 0 To Session("dtDeductions").Rows.Count - 1
                    Session("dtDeductions").Rows(i).Item("Field_N") = ChkN(dtPayMast.Rows(0).Item(Session("dtDeductions").Rows(i).Item("FIELD_NAME")))
                Next
            Else
                For i = 0 To Session("dtDeductions").Rows.Count - 1
                    Session("dtDeductions").Rows(i).Item("Field_N") = 0
                Next
            End If
            GrdDeductions.DataSource = Session("dtDeductions")
            GrdDeductions.DataBind()


            '*************  Perquisites  Data  Display  *************

            If dtPayMast.Rows.Count > 0 Then
                For i = 0 To Session("dtPerquisites").Rows.Count - 1
                    Session("dtPerquisites").Rows(i).Item("Field_N") = ChkN(dtPayMast.Rows(0).Item(Session("dtPerquisites").Rows(i).Item("FIELD_NAME")))
                Next
            Else
                For i = 0 To Session("dtPerquisites").Rows.Count - 1
                    Session("dtPerquisites").Rows(i).Item("Field_N") = 0
                Next
            End If
            GrdPerquisites.DataSource = Session("dtPerquisites")
            GrdPerquisites.DataBind()


            '*************  Investment  Data  Display  *************

            If dtPayMast.Rows.Count > 0 Then
                For i = 0 To Session("dtInvestment").Rows.Count - 1
                    Session("dtInvestment").Rows(i).Item("Field_N") = ChkN(dtPayMast.Rows(0).Item(Session("dtInvestment").Rows(i).Item("FIELD_NAME")))
                Next
            Else
                For i = 0 To Session("dtInvestment").Rows.Count - 1
                    Session("dtInvestment").Rows(i).Item("Field_N") = 0
                Next
            End If
            GrdInvestment.DataSource = Session("dtInvestment")
            GrdInvestment.DataBind()


            '*************  Reimbursment  Data  Display  *************

            StrSql = "SELECT * FROM ReimMast Inner Join HrdMast On ReimMast.Emp_Code = HrdMast.Emp_Code WHERE ReimMast.RimYear = " & Year(RY_Start) & " AND ReimMast.Emp_Code = '" & Chk(txtEM_CD.Text) & "'" & Session("UserCodes")
            Session("DalObj").GetSqlDataTable(dtReimMast, StrSql)

            'Dim Dview As New DataView(dtReimMast)

            '****************************************************************
            'WORKING *****BY RAVI
            '22 NOV 2006
            '****************************************************************
            For i = 0 To Session("dtReim").Rows.Count - 1
                If dtReimMast.Select("FIELD_NAME='" & Session("dtReim").Rows(i).Item("FIELD_NAME") & "'").GetUpperBound(0) >= 0 Then
                    DR = dtReimMast.Select("FIELD_NAME='" & Session("dtReim").Rows(i).Item("FIELD_NAME") & "'")(0)
                    ' If Not IsDBNull(dtReimMast.Rows(i).Item("Budget_WEF")) Then
                    'BY RAVI ON 22 NOV 2006
                    If Not IsDBNull(DR.Item("Budget_WEF")) Then
                        Session("dtReim").Rows(i).Item("WEF") = Format(CDate(DR.Item("Budget_WEF")), "dd/MMM/yyyy")
                    Else
                        Session("dtReim").Rows(i).Item("WEF") = ""
                    End If
                    Session("dtReim").Rows(i).Item("Opening") = ChkN(DR.Item("OPN_BAL"))
                    Session("dtReim").Rows(i).Item("Budget") = ChkN(DR.Item("Budget"))
                    Session("dtReim").Rows(i).Item("Prorata") = ChkN(DR.Item("Prorata"))
                    Session("dtReim").Rows(i).Item("SplBudget") = ChkN(DR.Item("SplBudget"))
                    Session("dtReim").Rows(i).Item("Reimbursed") = ChkN(DR.Item("Reimbursed"))
                    Session("dtReim").Rows(i).Item("Balance") = ChkN(DR.Item("OPN_BAL")) + ChkN(DR.Item("Prorata")) + ChkN(DR.Item("SplBudget")) - ChkN(DR.Item("Reimbursed"))
                Else
                    Session("dtReim").Rows(i).Item("WEF") = ""
                    Session("dtReim").Rows(i).Item("Opening") = 0
                    Session("dtReim").Rows(i).Item("Budget") = 0
                    Session("dtReim").Rows(i).Item("Prorata") = 0
                    Session("dtReim").Rows(i).Item("SplBudget") = 0
                    Session("dtReim").Rows(i).Item("Reimbursed") = 0
                    Session("dtReim").Rows(i).Item("Balance") = 0
                End If
            Next

            GrdReim.DataSource = Session("dtReim")
            GrdReim.DataBind()


            '*************  Furniture  Data  Display  *************
            StrSql = "Select Furn_Desc,Right('00' + DateName(dd,Bill_Date),2) + '/' + Left(DateName(mm,Bill_Date),3) + '/' + DateName(yyyy,Bill_Date) As Bill_Date,Bill_Cost,Perk_Per From FurnBill Inner Join HrdMast On FurnBill.Emp_Code = HrdMast.Emp_Code Where FurnBill.FINYEAR = " & Year(FY_Start) & " And Datediff(yy,BILL_DATE,Getdate()) < 5 And FurnBill.Emp_Code = '" & Chk(txtEM_CD.Text) & "'" & Session("UserCodes")
            Session("DalObj").GetSqlDataTable(dtFurnBill, StrSql)
            GrdFurn.DataSource = dtFurnBill
            GrdFurn.DataBind()


            '''**********************************************
            '''WORKING *****BY SANTOSH
            '''27 NOV 2006
            '''**********************************************
            'If Session("dtFurn").Rows.Count > 0 Then
            '    For i = 0 To Session("dtFurn").Rows.Count - 1
            '        If dtFurnBill.Select("FIELD_NAME='" & Session("dtFurn").Rows(i).Item("FIELD_NAME") & "'").GetUpperBound(0) >= 0 Then
            '            DR = dtFurnBill.Select("FIELD_NAME='" & Session("dtFurn").Rows(i).Item("FIELD_NAME") & "'")(0)
            '            ' If Not IsDBNull(dtReimMast.Rows(i).Item("Budget_WEF")) Then
            '            'BY SANTOSH ON 22 NOV 2006
            '            If Not IsDBNull(DR.Item("Bill_Date")) Then
            '                Session("dtFurn").Rows(i).Item("Furn_Date") = Format(CDate(DR.Item("Bill_Date")), "dd/MMM/yyyy")
            '            Else
            '                Session("dtFurn").Rows(i).Item("Furn_Date") = ""
            '            End If
            '            Session("dtFurn").Rows(i).Item("Furn_Cost") = ChkN(DR.Item("Bill_Cost"))
            '            Session("dtFurn").Rows(i).Item("Furn_Perk") = ChkN(DR.Item("Perk_per"))
            '        Else
            '            Session("dtFurn").Rows(i).Item("Furn_Date") = ""
            '            Session("dtFurn").Rows(i).Item("Furn_Cost") = ""
            '            Session("dtFurn").Rows(i).Item("Furn_perk") = 0
            '        End If
            '    Next
            'Else
            '    Session("dtFurn").Rows(0).Item("Furn_Date") = ""
            '    Session("dtFurn").Rows(0).Item("Furn_Cost") = ""
            '    Session("dtFurn").Rows(0).Item("Furn_perk") = 0
            'End If

            ''GrdReim.DataSource = Session("dtReim")
            'GrdFurn.DataSource = Session("dtFurn")
            'GrdFurn.DataBind()

            ''**************************************************************************************



            '*************  Others  Data  Display  *************

            StrSql = "SELECT * FROM HrdMastQry Where Emp_Code = '" & Chk(txtEM_CD.Text) & "'" & Session("UserCodes")
            Session("DalObj").GetSqlDataTable(dtHrdMast, StrSql)

            For i = 0 To Session("dtOthers").Rows.Count - 1
                Session("dtOthers").Rows(i).Item("Field_N") = DBNull.Value
                Session("dtOthers").Rows(i).Item("Field_D") = DBNull.Value
                Session("dtOthers").Rows(i).Item("Field_C") = DBNull.Value
            Next
            If dtPayMast.Rows.Count > 0 Then
                For i = 0 To Session("dtOthers").Rows.Count - 1
                    If Chk(Session("dtOthers").Rows(i).Item("Fld_PayMast")) = "Y" Then
                        If Session("dtOthers").Rows(i).Item("FIELD_TYPE") = "N" Then
                            Session("dtOthers").Rows(i).Item("Field_N") = ChkN(dtPayMast.Rows(0).Item(Session("dtOthers").Rows(i).Item("FIELD_NAME")))
                        ElseIf Session("dtOthers").Rows(i).Item("FIELD_TYPE") = "C" Then
                            Session("dtOthers").Rows(i).Item("Field_C") = Chk(dtPayMast.Rows(0).Item(Session("dtOthers").Rows(i).Item("FIELD_NAME")))
                        ElseIf Session("dtOthers").Rows(i).Item("FIELD_TYPE") = "D" Then
                            If Not IsDBNull(dtPayMast.Rows(0).Item(Session("dtOthers").Rows(i).Item("FIELD_NAME"))) Then
                                Session("dtOthers").Rows(i).Item("Field_D") = Format(CDate(dtPayMast.Rows(0).Item(Session("dtOthers").Rows(i).Item("FIELD_NAME"))), "dd/MMM/yyyy")
                            End If
                        End If
                    Else
                        If Session("dtOthers").Rows(i).Item("FIELD_TYPE") = "N" Then
                            Session("dtOthers").Rows(i).Item("Field_N") = ChkN(dtHrdMast.Rows(0).Item(Session("dtOthers").Rows(i).Item("FIELD_NAME")))
                        ElseIf Session("dtOthers").Rows(i).Item("FIELD_TYPE") = "C" Then
                            Session("dtOthers").Rows(i).Item("Field_C") = Chk(dtHrdMast.Rows(0).Item(Session("dtOthers").Rows(i).Item("FIELD_NAME")))
                        ElseIf Session("dtOthers").Rows(i).Item("FIELD_TYPE") = "D" Then
                            If Not IsDBNull(dtHrdMast.Rows(0).Item(Session("dtOthers").Rows(i).Item("FIELD_NAME"))) Then
                                Session("dtOthers").Rows(i).Item("Field_D") = Format(CDate(dtHrdMast.Rows(0).Item(Session("dtOthers").Rows(i).Item("FIELD_NAME"))), "dd/MMM/yyyy")
                            End If
                        End If
                    End If
                Next
            End If


            GrdOthers.DataSource = Session("dtOthers")
            GrdOthers.DataBind()


        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (DisplayEarning)")
        Finally
            dtPayMast.Dispose()
            dtHrdMast.Dispose()
            dtReimMast.Dispose()
            dtFurnBill.Dispose()
        End Try


    End Sub
    Private Sub SaveRecord()
        If ViewState("Value") = False Then Exit Sub
        Dim dtPayMast As New DataTable
        Dim dtHrdMast As New DataTable
        Dim dtReimMast As New DataTable
        Dim dtFurnBill As New DataTable
        Dim dt As DataTable
        Try
            Dim StrSql As String
            Dim strUpdate As String
            Dim strUpdateHRDMast As String
            Dim i As Int16
            StrSql = "SELECT * FROM PayMast WHERE FinYear = " & Year(FY_Start) & " AND Emp_Code = '" & Chk(txtEM_CD.Text) & "'"
            Session("DalObj").GetSqlDataTable(dtPayMast, StrSql)

            If dtPayMast.Rows.Count = 0 Then
                Session("DalObj").ExecuteCommand("Insert InTo PayMast (Emp_Code,FinYear) Values ('" & Chk(txtEM_CD.Text) & "'," & Year(FY_Start) & ")")
                StrSql = "SELECT * FROM PayMast WHERE FinYear = " & Year(FY_Start) & " AND Emp_Code = '" & Chk(txtEM_CD.Text) & "'"
                Session("DalObj").GetSqlDataTable(dtPayMast, StrSql)
            End If
            '*************  Saving  Data  in  PayMast *************
            If dtPayMast.Rows.Count > 0 Then
                strUpdate = " UpDate PayMast Set "
                '*************  Saving  Earning  Dat6a  *************
                dt = Session("dtEarnings")
                For i = 0 To dt.Rows.Count - 1
                    'If TypeOf GrdEarnings.Items(i).Cells(1).Controls(1) Is TextBox Then
                    'Comment by Ravi
                    ' strUpdate &= dt.Rows(i).Item("FIELD_NAME") & " = " & ChkN(dt.Rows(i).Item("Field_N")) & ", "  
                    'BY Ravi 30 nov
                    strUpdate &= dt.Rows(i).Item("FIELD_NAME") & " = " & ChkN(dt.Rows(i).Item("Field_N"), True) & ", " 'by Ravi
                    'End If
                Next

                '*************  Saving  Deductions  Data  *************
                dt = Session("dtDeductions")
                For i = 0 To dt.Rows.Count - 1
                    'comment by Ravi 
                    'strUpdate &= dt.Rows(i).Item("FIELD_NAME") & " = " & ChkN(dt.Rows(i).Item("Field_N")) & ", "
                    'By Ravi
                    strUpdate &= dt.Rows(i).Item("FIELD_NAME") & " = " & ChkN(dt.Rows(i).Item("Field_N"), True) & ", "
                Next

                '*************  Saving  Perquisites  Data  *************
                dt = Session("dtPerquisites")
                For i = 0 To dt.Rows.Count - 1
                    'comment by Ravi 
                    'strUpdate &= dt.Rows(i).Item("FIELD_NAME") & " = " & ChkN(dt.Rows(i).Item("Field_N")) & ", "
                    'BY Ravi 
                    strUpdate &= dt.Rows(i).Item("FIELD_NAME") & " = " & ChkN(dt.Rows(i).Item("Field_N"), True) & ", "

                Next

                '*************  Saving  Investment  Data  *************
                dt = Session("dtInvestment")
                For i = 0 To dt.Rows.Count - 1
                    'comment by Ravi 
                    'strUpdate &= dt.Rows(i).Item("FIELD_NAME") & " = " & ChkN(dt.Rows(i).Item("Field_N")) & ", "
                    'by Ravi
                    strUpdate &= dt.Rows(i).Item("FIELD_NAME") & " = " & ChkN(dt.Rows(i).Item("Field_N"), True) & ", "
                Next

                If Right(strUpdate, 2) = ", " Then
                    strUpdate = Mid(strUpdate, 1, strUpdate.Length - 2) & " Where Emp_Code = '" & Chk(txtEM_CD.Text) & "' And FinYear = " & Year(FY_Start)
                End If
                Session("DalObj").ExecuteCommand(strUpdate)
            End If


            '*************  Saving  Reimbursement  Data  *************
            dt = Session("dtReim")
            For i = 0 To dt.Rows.Count - 1
                Dim strReim As String
                If Not IsDBNull(dt.Rows(i).Item("WEF")) And Not Trim(Chk(dt.Rows(i).Item("WEF"))) = "" Then
                    'Dim FldName As String = Chk(CType(GrdReim.Items(i).Cells(8).Controls(1), TextBox).Text)
                    StrSql = "SELECT * FROM ReimMast WHERE FIELD_NAME = '" & dt.Rows(i).Item("FIELD_NAME") & "' And RimYear = " & Year(RY_Start) & " AND Emp_Code = '" & Chk(txtEM_CD.Text) & "'"
                    dtReimMast.Rows.Clear()
                    Session("DalObj").GetSqlDataTable(dtReimMast, StrSql)
                    If dtReimMast.Rows.Count = 0 Then
                        strReim = " Insert InTo ReimMast " & _
                                  " (RimYear,Emp_Code,Field_Name,Budget,Budget_WEF,OPN_BAL,PRORATA,REIMBURSED,SPLBUDGET) " & _
                                  " Values " & _
                                  " (" & Year(RY_Start) & ",'" & Chk(txtEM_CD.Text) & "','" & Chk(dt.Rows(i).Item("FIELD_NAME")) & "'," & _
                                  ChkN(dt.Rows(i).Item("BUDGET")) & ",'" & _
                                  Format(CDate(dt.Rows(i).Item("WEF")), "dd/MMM/yyyy") & "'," & _
                                  ChkN(dt.Rows(i).Item("Opening")) & "," & _
                                  ChkN(dt.Rows(i).Item("PRORATA")) & "," & _
                                  ChkN(dt.Rows(i).Item("REIMBURSED")) & "," & _
                                  ChkN(dt.Rows(i).Item("SplBudget")) & ")"
                    Else
                        strReim = " Update ReimMast Set " & _
                                  " Budget = " & ChkN(dt.Rows(i).Item("BUDGET")) & ", " & _
                                  " Budget_WEF = '" & Format(CDate(dt.Rows(i).Item("WEF")), "dd/MMM/yyyy") & "', " & _
                                  " OPN_BAL = " & ChkN(dt.Rows(i).Item("Opening")) & ", " & _
                                  " PRORATA = " & ChkN(dt.Rows(i).Item("PRORATA")) & ", " & _
                                  " REIMBURSED = " & ChkN(dt.Rows(i).Item("REIMBURSED")) & ", " & _
                                  " SPLBUDGET = " & ChkN(dt.Rows(i).Item("SplBudget")) & " " & _
                                  " Where RimYear = " & Year(RY_Start) & " And Emp_Code = '" & Chk(txtEM_CD.Text) & "' And Field_Name = '" & Chk(dt.Rows(i).Item("FIELD_NAME")) & "'"
                    End If
                    Session("DalObj").ExecuteCommand(strReim)
                End If
            Next


            '*************  Saving  Others  Data  *************
            strUpdate = " Update PayMast Set "
            strUpdateHRDMast = " Update HrdMast Set "
            dt = Session("dtOthers")
            For i = 0 To dt.Rows.Count - 1
                If dt.Rows(i).Item("TableName") = "HrdMast" Then
                    If Chk(dt.Rows(i).Item("Field_Type")) = "C" Then
                        strUpdateHRDMast &= Chk(dt.Rows(i).Item("Field_Name")) & " = '" & Chk(dt.Rows(i).Item("Field_C")) & "', "
                    ElseIf Chk(dt.Rows(i).Item("Field_Type")) = "N" Then
                        strUpdateHRDMast &= Chk(dt.Rows(i).Item("Field_Name")) & " = " & ChkN(dt.Rows(i).Item("Field_N")) & ", "
                    ElseIf Chk(dt.Rows(i).Item("Field_Type")) = "D" Then
                        If Not IsDBNull(dt.Rows(i).Item("Field_D")) Then
                            strUpdateHRDMast &= Chk(dt.Rows(i).Item("Field_Name")) & " = '" & Format(CDate(dt.Rows(i).Item("Field_D")), "dd/MMM/yyyy") & "', "
                        End If
                    End If
                ElseIf dt.Rows(i).Item("TableName") = "PayMast" Then
                    If Chk(dt.Rows(i).Item("Field_Type")) = "C" Then
                        strUpdate &= Chk(dt.Rows(i).Item("Field_Name")) & " = '" & Chk(dt.Rows(i).Item("Field_C")) & "', "
                    ElseIf Chk(dt.Rows(i).Item("Field_Type")) = "N" Then
                        strUpdate &= Chk(dt.Rows(i).Item("Field_Name")) & " = " & ChkN(dt.Rows(i).Item("Field_N")) & ", "
                    ElseIf Chk(dt.Rows(i).Item("Field_Type")) = "D" Then
                        If Not IsDBNull(dt.Rows(i).Item("Field_D")) Then
                            strUpdate &= Chk(dt.Rows(i).Item("Field_Name")) & " = '" & Format(CDate(dt.Rows(i).Item("Field_D")), "dd/MMM/yyyy") & "', "
                        End If
                    End If
                End If
            Next
            If Right(strUpdate, 2) = ", " Then
                strUpdate = Mid(strUpdate, 1, strUpdate.Length - 2) & " Where Emp_Code = '" & Chk(txtEM_CD.Text) & "' And FinYear = " & Year(FY_Start)
            End If
            Session("DalObj").ExecuteCommand(strUpdate)
            If Right(strUpdateHRDMast, 2) = ", " Then
                strUpdateHRDMast = Mid(strUpdateHRDMast, 1, strUpdateHRDMast.Length - 2) & " Where Emp_Code = '" & Chk(txtEM_CD.Text) & "'"
            End If
            Session("DalObj").ExecuteCommand(strUpdateHRDMast)
            SetMsg(LblMsg, "Transaction Saved Successfully.")
            DisplayRecord()
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (SaveRecord())")
        Finally
            dtPayMast.Dispose()
            dtReimMast.Dispose()
            dtFurnBill.Dispose()
            dtHrdMast.Dispose()
        End Try
    End Sub

#End Region

#Region " Events of Other Controls "
    Public Function GetWEFDate(ByVal strDate As Object) As String
        Try
            If IsDBNull(strDate) Then GetWEFDate = "" : Exit Function
            If CStr(strDate) = "" Then
                GetWEFDate = ""
            Else
                GetWEFDate = Format(CDate(strDate), "dd/MMM/yyyy")
            End If
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (GetWefDate)")
        End Try
    End Function
    Private Sub txtEM_CD_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEM_CD.TextChanged
        Try
            If Trim(txtEM_CD.Text) <> "" Then Session("EM_CD") = Trim(txtEM_CD.Text)
            DisplayRecord()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            If ViewState("Value") = False Then Exit Sub
            SaveRecord()
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (GrdEarnings_ItemDataBound)")
        End Try
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("EmpExplorer.aspx?SrNo=61")
    End Sub
    Sub OnAmountChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Item As DataGridItem
        Dim dt As DataTable
        Dim dr As DataRow
        Try
            Item = CType(sender, Control).NamingContainer()
            Select Case UCase(CType(Item.Parent.Parent, DataGrid).ID)
                Case UCase("GrdEarnings")
                    dt = Session("dtEarnings")
                Case UCase("GrdDeductions")
                    dt = Session("dtDeductions")
                Case UCase("GrdPerquisites")
                    dt = Session("dtPerquisites")
                Case UCase("GrdInvestment")
                    dt = Session("dtInvestment")
            End Select
            dr = dt.Select("FIELD_NAME='" & Chk(CType(Item.Cells(2).Controls(1), TextBox).Text) & "'")(0)

            '  dr.Item("Field_N") = ChkN(CType(Item.Cells(1).Controls(1), TextBox).Text) comment by ravi
            'By Ravi on 30 Nov 2006
            dr.Item("Field_N") = ChkN(CType(Item.Cells(1).Controls(1), TextBox).Text, True)

            dr.AcceptChanges()
            dt.AcceptChanges()
            GetVarMast(Chk(CType(Item.Cells(2).Controls(1), TextBox).Text))
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (onAmountChanged)")
        End Try
    End Sub
    Sub OnOthersChanged_N(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Item As DataGridItem
        Dim dt As DataTable
        Dim dr As DataRow
        Try
            Item = CType(sender, Control).NamingContainer()
            dt = Session("dtOthers")
            dr = dt.Select("FIELD_NAME='" & Chk(CType(Item.Cells(4).Controls(1), TextBox).Text) & "'")(0)
            dr.Item("Field_N") = ChkN(CType(Item.Cells(2).Controls(1), TextBox).Text)
            dr.AcceptChanges()
            dt.AcceptChanges()
            GetVarMast(Chk(CType(Item.Cells(4).Controls(1), TextBox).Text))
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (onAmountChanged)")
        End Try
    End Sub
    Sub OnOthersChanged_C(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Item As DataGridItem
        Dim dt As DataTable
        Dim dr As DataRow
        Try
            Item = CType(sender, Control).NamingContainer()
            dt = Session("dtOthers")
            dr = dt.Select("FIELD_NAME='" & Chk(CType(Item.Cells(4).Controls(1), TextBox).Text) & "'")(0)
            dr.Item("Field_C") = Chk(CType(Item.Cells(3).Controls(1), TextBox).Text)
            dr.AcceptChanges()
            dt.AcceptChanges()
            GetVarMast(Chk(CType(Item.Cells(4).Controls(1), TextBox).Text))
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (onAmountChanged)")
        End Try
    End Sub
    Sub OnOthersChanged_D(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Item As DataGridItem
        Dim dt As DataTable
        Dim dr As DataRow
        Try
            Item = CType(sender, Control).NamingContainer()
            dt = Session("dtOthers")
            dr = dt.Select("FIELD_NAME='" & Chk(CType(Item.Cells(4).Controls(1), TextBox).Text) & "'")(0)
            If Chk(CType(Item.Cells(1).Controls(1), TextBox).Text) = "" Then
                dr.Item("Field_D") = DBNull.Value
            Else
                dr.Item("Field_D") = Format(CDate(Chk(CType(Item.Cells(1).Controls(1), TextBox).Text)), "dd/MMM/yyyy")
            End If
            dr.AcceptChanges()
            dt.AcceptChanges()
            GetVarMast(Chk(CType(Item.Cells(4).Controls(1), TextBox).Text))
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (onAmountChanged)")
        End Try
    End Sub
    Sub OnReimChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Item As DataGridItem
        Dim dt As DataTable
        Dim dr As DataRow
        Dim dtEmployee As New DataTable
        Dim WEF_Date, stDt, StartDt, EndDt As Date
        Dim i, j, k, a As Int16
        Dim MonLeft, Prorata As Double
        Try
            Item = CType(sender, Control).NamingContainer()
            dt = Session("dtReim")
            dr = dt.Select("FIELD_NAME='" & Chk(CType(Item.Cells(8).Controls(1), TextBox).Text) & "'")(0)
            If Not dr Is Nothing Then
                oDal.GetSqlDataTable(dtEmployee, "SELECT * FROM HRDMASTQRY WHERE EMP_CODE='" & Chk(txtEM_CD.Text) & "'")
                If Chk(CType(Item.Cells(1).Controls(1), TextBox).Text) <> "" And IsDate(Chk(CType(Item.Cells(1).Controls(1), TextBox).Text)) Then
                    dr.Item("WEF") = CDate(CType(Item.Cells(1).Controls(1), TextBox).Text)
                Else
                    dr.Item("WEF") = DBNull.Value
                End If
                dr.Item("Prorata") = 0
                Prorata = 0
                If IsDBNull(dr.Item("WEF")) Then
                    dr.Item("Opening") = 0
                    dr.Item("Budget") = 0
                    dr.Item("SplBudget") = 0
                    dr.Item("WEF") = DBNull.Value
                    GoTo MoveAhead
                End If
                If Not IsDBNull(dr.Item("WEF")) And dr.Item("WEF") < DateSerial(Year(RY_Start), dr.Item("Fld_Month"), 1) Then
                    SetMsg(LblMsg, "W.E.F Date can not be less than " & DateSerial(Year(RY_Start), dr.Item("Fld_Month"), 1))
                    dr.Item("WEF") = DateSerial(Year(RY_Start), dr.Item("Fld_Month"), 1)
                End If
                If Not IsDBNull(dr.Item("WEF")) And dr.Item("WEF") > DateSerial(Year(RY_Start) + 1, dr.Item("Fld_Month"), 1).AddDays(-1) Then
                    SetMsg(LblMsg, "W.E.F Date can not be greater than " & DateSerial(Year(RY_Start) + 1, dr.Item("Fld_Month"), 1).AddDays(-1))
                    dr.Item("WEF") = DateSerial(Year(RY_Start), dr.Item("Fld_Month"), 1)
                End If
                If dr.Item("WEF") < dtEmployee.Rows(0).Item("DOJ") Then
                    SetMsg(LblMsg, "W.E.F Date can not be Less than DOJ: " & dtEmployee.Rows(0).Item("DOJ"))
                    dr.Item("WEF") = dtEmployee.Rows(0).Item("DOJ")
                End If
                dr.Item("Opening") = ChkN(CType(Item.Cells(2).Controls(1), TextBox).Text)
                dr.Item("Budget") = ChkN(CType(Item.Cells(3).Controls(1), TextBox).Text)
                dr.Item("SplBudget") = ChkN(CType(Item.Cells(5).Controls(1), TextBox).Text)
                WEF_Date = dr.Item("WEF")
                a = IIf(dr.Item("Fld_Rated") = 2, 1, IIf(dr.Item("Fld_Rated") = 3, 3, IIf(dr.Item("Fld_Rated") = 4, 6, 12)))
                stDt = DateAdd("m", dr.Item("Fld_Month") - Month(WEF_Date), WEF_Date.AddDays(-Day(WEF_Date) + 1))
                stDt = IIf(stDt > WEF_Date, DateAdd(DateInterval.Month, -12, stDt), stDt)
                For i = 1 To 12 / a
                    StartDt = DateAdd("m", a * (i - 1), stDt)
                    EndDt = DateAdd(DateInterval.Month, a * i, stDt).AddDays(-1)
                    If Betw(xMax(stDt, WEF_Date), StartDt, EndDt) Then
                        MonLeft = MonDiff(xMax(stDt, WEF_Date), xMin(IIf(IsDBNull(dtEmployee.Rows(0).Item("DOL")), EndDt, dtEmployee.Rows(0).Item("DOL")), EndDt))
                        Prorata = ChkN(dr.Item("Budget")) / a * MonLeft
                    End If
                Next
                dr.Item("Prorata") = ChkN(Prorata)
MoveAhead:
                dr.Item("Balance") = dr.Item("Opening") + dr.Item("Prorata") + dr.Item("SplBudget") - dr.Item("Reimbursed")
                dr.AcceptChanges()
                dt.AcceptChanges()
                GrdReim.DataSource = Session("dtReim")
                GrdReim.DataBind()
            End If
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (onReimChanged)")
        Finally

        End Try
    End Sub
#End Region

#Region " Grid Display Settings "

    Private Sub GrdEarnings_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles GrdEarnings.ItemDataBound
        Try
            If e.Item.ItemIndex >= 0 Then
                If Session("LoginUser").UserGroup = "ADMIN" Then
                    CType(e.Item.Cells(1).Controls(1), TextBox).Visible = True
                Else
                    Dim tmpLbl As Label
                    tmpLbl = e.Item.FindControl("LblAmount_Ern")
                    If Not tmpLbl Is Nothing Then
                        tmpLbl.Visible = True
                    End If
                End If
            End If
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (GrdEarnings_ItemDataBound)")
        End Try
    End Sub

    Private Sub GrdDeductions_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles GrdDeductions.ItemDataBound
        Try
            If e.Item.ItemIndex >= 0 Then
                If Session("LoginUser").UserGroup = "ADMIN" Then
                    CType(e.Item.Cells(1).Controls(1), TextBox).Visible = True
                Else
                    Dim tmpLbl As Label
                    tmpLbl = e.Item.FindControl("lblAmount_Ded")
                    If Not tmpLbl Is Nothing Then
                        tmpLbl.Visible = True
                    End If
                End If
            End If
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (GrdDeductions_ItemDataBound)")
        End Try
    End Sub

    Private Sub GrdInvestment_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles GrdInvestment.ItemDataBound
        Try
            If e.Item.ItemIndex >= 0 Then
                If Session("LoginUser").UserGroup = "ADMIN" Then
                    CType(e.Item.Cells(1).Controls(1), TextBox).Visible = True
                Else
                    Dim tmpLbl As Label
                    tmpLbl = e.Item.FindControl("lblAmount_Inv")
                    If Not tmpLbl Is Nothing Then
                        tmpLbl.Visible = True
                    End If
                End If
            End If
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (GrdInvestment_ItemDataBound)")
        End Try
    End Sub

    Private Sub GrdPerquisites_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles GrdPerquisites.ItemDataBound
        Try
            If e.Item.ItemIndex >= 0 Then
                If Session("LoginUser").UserGroup = "ADMIN" Then
                    CType(e.Item.Cells(1).Controls(1), TextBox).Visible = True
                Else
                    Dim tmpLbl As Label
                    tmpLbl = e.Item.FindControl("lblAmount_Perk")
                    If Not tmpLbl Is Nothing Then
                        tmpLbl.Visible = True
                    End If
                End If
            End If
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (GrdPerquisites_ItemDataBound)")
        End Try
    End Sub

    'Private Sub GrdOthers_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
    '    'txtDataType_Oth
    '    Try
    '        If e.Item.ItemIndex >= 0 Then
    '            If Session("LoginUser").UserGroup <> "Admin" Then
    '                CType(e.Item.Cells(1).Controls(1), TextBox).ReadOnly = True
    '                CType(e.Item.Cells(2).Controls(1), TextBox).ReadOnly = True
    '                CType(e.Item.Cells(3).Controls(1), TextBox).ReadOnly = True
    '            End If
    '            If CType(e.Item.Cells(5).Controls(1), TextBox).Text = "C" Then
    '                CType(e.Item.Cells(3).Controls(1), TextBox).Visible = True
    '                CType(e.Item.Cells(2).Controls(1), TextBox).Visible = False
    '                CType(e.Item.Cells(1).Controls(1), TextBox).Visible = False
    '            ElseIf CType(e.Item.Cells(5).Controls(1), TextBox).Text = "N" Then
    '                CType(e.Item.Cells(3).Controls(1), TextBox).Visible = False
    '                CType(e.Item.Cells(2).Controls(1), TextBox).Visible = True
    '                CType(e.Item.Cells(1).Controls(1), TextBox).Visible = False
    '            ElseIf CType(e.Item.Cells(5).Controls(1), TextBox).Text = "D" Then
    '                CType(e.Item.Cells(3).Controls(1), TextBox).Visible = False
    '                CType(e.Item.Cells(2).Controls(1), TextBox).Visible = False
    '                CType(e.Item.Cells(1).Controls(1), TextBox).Visible = True
    '            End If

    '        End If
    '    Catch ex As Exception
    '        SetMsg(LblMsg, ex.Message & " : (GrdOthers_ItemDataBound)")
    '    End Try
    'End Sub



    '*****************************
    'By santosh 27/11/2006
    '*****************************

    Private Sub GrdFurn_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles GrdFurn.ItemDataBound
        Try
            If e.Item.ItemIndex >= 0 Then
                If Session("LoginUser").UserGroup = "ADMIN" Then
                    CType(e.Item.Cells(1).Controls(1), TextBox).Visible = True
                Else
                    Dim tmpLbl As Label
                    tmpLbl = e.Item.FindControl("lblCost_Furn")
                    If Not tmpLbl Is Nothing Then
                        tmpLbl.Visible = True
                    End If
                End If
            End If
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (GrdFurn_ItemDataBound)")
        End Try
    End Sub
    '***********************************************
#End Region

#Region " Evaluating Formula for Master Heads"
    Private Function CopyRowData(ByVal dtFrom As DataTable, ByVal dtTo As DataTable)
        Dim drs() As DataRow
        Dim dr As DataRow
        Dim strField As String
        Try
            For i As Int16 = 0 To dtFrom.Rows.Count - 1
                strField = dtFrom.Rows(i).Item("Field_Name")
                dr = Nothing
                drs = dtTo.Select("FIELD_NAME='" & dtFrom.Rows(i).Item("Field_Name") & "'")
                If drs.GetUpperBound(0) >= 0 Then
                    dr = drs(0)
                End If
                If Not IsNothing(dr) Then
                    dr.Item("Field_N") = dtFrom.Rows(i).Item("Field_N")
                    dr.Item("Field_C") = dtFrom.Rows(i).Item("Field_C")
                    dr.Item("Field_D") = dtFrom.Rows(i).Item("Field_D")
                    dr.AcceptChanges()
                End If
            Next
            dtTo.AcceptChanges()
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (GetVarMast)" & vbCrLf & ex.Source)
        End Try
    End Function
    Private Sub GetVarMast(ByVal fldName As String)
        'MUKESH
        Dim objSCM As SCM.Payroll
        Dim dtTmp As New DataTable
        Dim dtComp As DataTable
        Dim i, j, k As Int16
        Dim sErr As String
        Dim dv As DataView
        Try
            objSCM = GetSCMPayroll(oDal, LblMsg, Session("LoginUser"))
            If Not IsNothing(objSCM) Then
                dtComp = Session("dtEarnings").copy
                MdlHRMS.MergeTable(Session("dtDeductions"), dtComp)
                MdlHRMS.MergeTable(Session("dtPerquisites"), dtComp)
                MdlHRMS.MergeTable(Session("dtInvestment"), dtComp)
                MdlHRMS.MergeTable(Session("dtOthers"), dtComp)


                'objSCM.MstFldCal(dtComp)
                objSCM.MstFldCal(dtComp)

                CopyRowData(dtComp, Session("dtEarnings"))
                CopyRowData(dtComp, Session("dtDeductions"))
                CopyRowData(dtComp, Session("dtPerquisites"))
                CopyRowData(dtComp, Session("dtInvestment"))
                CopyRowData(dtComp, Session("dtOthers"))

                GrdEarnings.DataSource = Session("dtEarnings")
                GrdEarnings.DataBind()
                GrdDeductions.DataSource = Session("dtDeductions")
                GrdDeductions.DataBind()
                GrdPerquisites.DataSource = Session("dtPerquisites")
                GrdPerquisites.DataBind()
                GrdInvestment.DataSource = Session("dtInvestment")
                GrdInvestment.DataBind()
                GrdOthers.DataSource = Session("dtOthers")
                GrdOthers.DataBind()
            Else
                SetMsg(LblMsg, LblMsg.Text & vbCrLf & "objScm is nothing" & vbCrLf & sErr)
            End If
            'SetMsg(LblMsg, " : (GetVarMast)" & vbCrLf & sErr)
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (GetVarMast)" & vbCrLf & objSCM.ErrMessage)
        Finally
            objSCM = Nothing
            dtComp = Nothing
            dtTmp = Nothing
        End Try
        'Response.Write(objSCM.sErrMsg)
    End Sub


#End Region
    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        GetVarMast("BASIC")
    End Sub

    Private Sub BtnFirst_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFirst.ServerClick
        Try
            Dim strqry As String = "SELECT DISTINCT TOP 1 PayMast.EMP_CODE FROM PayMast Inner Join HrdMast On PayMast.Emp_Code = HrdMast.Emp_Code WHERE PayMast.FinYear = " & Year(FY_Start) & Session("UserCodes") & " Order By PayMast.Emp_Code "
            strqry = Replace(strqry, "AND", " AND hrdmast.")
            txtEM_CD.Text = Chk(Session("DalObj").ExecuteCommand(strqry, DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
            DisplayRecord()
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (BtnFirst_ServerClick)")
        End Try
    End Sub

    Private Sub BtnPre_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPre.ServerClick
        Try
            Dim strqry As String
            strqry = "SELECT DISTINCT TOP 1 PayMast.EMP_CODE FROM PayMast Inner Join HrdMast On PayMast.Emp_Code = HrdMast.Emp_Code WHERE PayMast.FinYear = " & Year(FY_Start) & " AND Emp_Code <'" & Chk(txtEM_CD.Text) & "' " & Session("UserCodes") & " Order By PayMast.Emp_Code desc"
            strqry = Replace(strqry, "AND", " AND hrdmast.")
            txtEM_CD.Text = Chk(Session("DalObj").ExecuteCommand(strqry, DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
            DisplayRecord()
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (BtnPre_ServerClick)")
        End Try
    End Sub
    Private Sub BtnNext_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNext.ServerClick
        Try
            Dim strqry As String
            strqry = "SELECT DISTINCT TOP 1 PayMast.EMP_CODE FROM PayMast Inner Join HrdMast On PayMast.Emp_Code = HrdMast.Emp_Code WHERE PayMast.FinYear = " & Year(FY_Start) & " AND Emp_Code >'" & Chk(txtEM_CD.Text) & "' " & Session("UserCodes") & " Order By PayMast.Emp_Code "
            strqry = Replace(strqry, "AND", " AND hrdmast.")
            txtEM_CD.Text = Chk(Session("DalObj").ExecuteCommand(strqry, DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
            DisplayRecord()
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (BtnNext_ServerClick)")
        End Try
    End Sub

    Private Sub BtnLast_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLast.ServerClick
        Try
            Dim strqry As String = "SELECT DISTINCT TOP 1 PayMast.EMP_CODE FROM PayMast Inner Join HrdMast On PayMast.Emp_Code = HrdMast.Emp_Code WHERE PayMast.FinYear = " & Year(FY_Start) & Session("UserCodes") & " Order By PayMast.Emp_Code desc"
            strqry = Replace(strqry, "AND", " AND hrdmast.")
            txtEM_CD.Text = Chk(Session("DalObj").ExecuteCommand(strqry, DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
            DisplayRecord()
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (BtnLast_ServerClick)")
        End Try
    End Sub

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
        Session("EM_CD") = Trim(txtEM_CD.Text)
    End Sub
End Class
