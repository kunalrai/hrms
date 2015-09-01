Partial Class Family
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtEm_ As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    'By Ravi on 7 dec 2007
    Dim StrSql As String

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Server.GetLastError Is Nothing Then SetMsg(LblMsg, "")
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If
        Try
            'By Ravi 17 nov 2006
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
                cmdFamilyAdd.Visible = bSuccess

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
            '            cmdSave.Visible = False
            '            cmdFamilyAdd.Visible = False
            '        End If
            '    Else
            '        txtEM_CD.ReadOnly = True
            '        cmdSave.Visible = False
            '        SetMsg(LblRights, "You are not authorised to view data on this page.")
            '        cmdFamilyAdd.Visible = False
            '        'Response.Write("<BR><center><B></B></center>")
            '        Exit Sub
            '    End If
            'End If

            'If Session("LoginUser").UserGroup <> "ADMIN" Then
            '    txtEM_CD.ReadOnly = True
            '    'EnableDisable(False, Me)
            '    cmdSave.Visible = False
            '    cmdFamilyAdd.Visible = False
            'Else
            '    txtEM_CD.ReadOnly = False
            'End If
            If Not IsPostBack Then
                txtEM_CD.Text = Session("EM_CD")
                txtEM_CD_TextChanged(sender, e)
            End If
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (Page_Load)")
        End Try
    End Sub
#Region " Family Details "
    Private Sub cmdFamilyAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFamilyAdd.Click
        Try
            If GrdFamily.Items.Count > 0 Then
                If Chk(CType(GrdFamily.Items(GrdFamily.Items.Count - 1).Controls(0).Controls(1), TextBox).Text) <> "" Then
                    cmdSave_Click(sender, e)
                    'By ravi on 7 dec 2007
                    StrSql = " Select HrdFamily.Emp_Code,Relative_Name,Relation, " & _
                            " Right('00' + DateName(dd,Relative_DOB),2) + '/' + Left(DateName(m,Relative_DOB),3) + '/' + DateName(yyyy,Relative_DOB) Relative_DOB " & _
                            " ,isNull(Dependent,0) As Dependent,isNull(Nominee,0) As Nominee,RelativeAge From HrdFamily " & _
                            " Inner Join HrdMast On HrdFamily.Emp_Code = HrdMast.Emp_Code " & _
                            " Where HrdFamily.Emp_Code = '" & Chk(txtEM_CD.Text) & "'" & Session("UserCodes") & " Order By HrdFamily.Relative_DOB Desc"

                    'StrSql = " Select HrdFamily.Emp_Code,Relative_Name,Relation, " & _
                    '       " Right('00' + DateName(dd,Relative_DOB),2) + '/' + Left(DateName(m,Relative_DOB),3) + '/' + DateName(yyyy,Relative_DOB) Relative_DOB " & _
                    '       " ,isNull(Dependent,0) As Dependent,isNull(Nominee,0) As Nominee,RelativeAge From HrdFamily " & _
                    '       " Inner Join HrdMast On HrdFamily.Emp_Code = HrdMast.Emp_Code " & _
                    '       " Where HrdFamily.Emp_Code = '" & Chk(txtEM_CD.Text) & "'" & Session("UserCodes") & " Order By HrdFamily.Relative_DOB Desc"
                    '------------------------------------------------------------
                    BindGridFamily()
                    Dim tmpTr As DataRow = ViewState("dtFamily").NewRow()
                    ViewState("dtFamily").Rows.Add(tmpTr)
                    GrdFamily.DataSource = ViewState("dtFamily")
                    GrdFamily.DataBind()
                End If
            Else
                Dim tmpTr As DataRow = ViewState("dtFamily").NewRow()
                ViewState("dtFamily").Rows.Add(tmpTr)
                GrdFamily.DataSource = ViewState("dtFamily")
                GrdFamily.DataBind()
            End If
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (cmdFamilyAdd_Click)")
        End Try
    End Sub
    Private Sub BindGridFamily()
        Try
            'If Trim(txtEM_CD.Text) <> "" Then

            'comment By Ravi (define StrSql as global)
            ' Dim StrSql As String
            strsql = " Select HrdFamily.Emp_Code,Relative_Name,Relation, " & _
                     " Right('00' + DateName(dd,Relative_DOB),2) + '/' + Left(DateName(m,Relative_DOB),3) + '/' + DateName(yyyy,Relative_DOB) Relative_DOB " & _
                     " ,isNull(Dependent,0) As Dependent,isNull(Nominee,0) As Nominee,RelativeAge From HrdFamily " & _
                     " Inner Join HrdMast On HrdFamily.Emp_Code = HrdMast.Emp_Code " & _
                     " Where HrdFamily.Emp_Code = '" & Chk(txtEM_CD.Text) & "'" & Session("UserCodes") & " Order By HrdFamily.Relative_DOB Desc"
            strsql = Replace(strsql, "AND", " AND hrdmast.")
            ViewState("dtFamily") = New DataTable
            Session("DalObj").GetSqlDataTable(ViewState("dtFamily"), strsql)
            GrdFamily.DataSource = ViewState("dtFamily")
            GrdFamily.DataBind()
            ViewState("Value") = True

            If Trim(txtEM_CD.Text) = "" Then
                ViewState("Value") = False
            End If
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (BindGridFamily)")
        End Try
    End Sub
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If ViewState("Value") = False Then Exit Sub
        Dim trnFamily As SqlClient.SqlTransaction

        Try
            Dim cnt As Int16
            Dim strSQl As String
            trnFamily = Session("DalObj").StartTransaction("Family")
            strSQl = "Delete From HrdFamily Where Emp_Code = '" & Chk(txtEM_CD.Text) & "'"
            Session("DalObj").ExecuteCommand(strSQl, trnFamily)
            For cnt = 0 To GrdFamily.Items.Count - 1
                If Chk(CType(GrdFamily.Items(cnt).Controls(0).Controls(1), TextBox).Text) <> "" Then
                    Dim DOB As String
                    If CType(GrdFamily.Items(cnt).Controls(2).Controls(1), TextBox).Text <> "" Then
                        DOB = "'" & Format(CDate(CType(GrdFamily.Items(cnt).Controls(2).Controls(1), TextBox).Text), "dd/MMM/yyyy") & "'"
                    Else
                        DOB = "Null"
                    End If

                    strSQl = " Insert InTo HrdFamily " & _
                             " (Emp_Code,Relative_Name,Relation,Relative_DOB,RelativeAge,Dependent,Nominee) " & _
                             " Values " & _
                             " ('" & Chk(txtEM_CD.Text) & "','" & _
                             Chk(CType(GrdFamily.Items(cnt).Controls(0).Controls(1), TextBox).Text) & "','" & _
                             Chk(CType(GrdFamily.Items(cnt).Controls(1).Controls(1), TextBox).Text) & "'," & _
                             DOB & "," & _
                             ChkN(CType(GrdFamily.Items(cnt).Controls(3).Controls(1), TextBox).Text) & "," & _
                             IIf(CType(GrdFamily.Items(cnt).Controls(4).Controls(1), CheckBox).Checked, 1, 0) & "," & _
                             IIf(CType(GrdFamily.Items(cnt).Controls(5).Controls(1), CheckBox).Checked, 1, 0) & ")"
                    Session("DalObj").ExecuteCommand(strSQl, trnFamily)
                End If
            Next
            trnFamily.Commit()
            'By ravi on 7 dec 2007
            strSQl = " Select HrdFamily.Emp_Code,Relative_Name,Relation, " & _
                    " Right('00' + DateName(dd,Relative_DOB),2) + '/' + Left(DateName(m,Relative_DOB),3) + '/' + DateName(yyyy,Relative_DOB) Relative_DOB " & _
                    " ,isNull(Dependent,0) As Dependent,isNull(Nominee,0) As Nominee,RelativeAge From HrdFamily " & _
                    " Inner Join HrdMast On HrdFamily.Emp_Code = HrdMast.Emp_Code " & _
                    " Where HrdFamily.Emp_Code = '" & Chk(txtEM_CD.Text) & "'" & Session("UserCodes") & " Order By HrdFamily.Relative_DOB Desc"

            '------------------------------------------------------------
            BindGridFamily()
        Catch ex As Exception
            trnFamily.Rollback()
            SetMsg(LblMsg, ex.Message & " : (cmdSave_Click)")
        End Try

    End Sub
    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("EmpExplorer.aspx?SrNo=61")
    End Sub

#End Region

    Private Sub txtEM_CD_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEM_CD.TextChanged
        Try
            If Trim(txtEM_CD.Text) <> "" Then Session("EM_CD") = Trim(txtEM_CD.Text)
            Dim EmpName As Object

            EmpName = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(" Select Emp_Name From HrdMastQry Where Emp_Code = '" & Chk(txtEM_CD.Text) & "'" & Session("UserCodes"), , DAL.DataLayer.ExecutionType.ExecuteScalar)

            If (Not IsDBNull(EmpName)) And (Not IsNothing(EmpName)) Then
                LblName.Text = EmpName
                ViewState("Value") = True
            Else
                Dim Code As Object
                'By Ravi
                Code = Session("DalObj").ExecuteCommand("Select Emp_Code From HrdMastQry Where Emp_Code = '" & Chk(txtEM_CD.Text) & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
                'Code = Session("DalObj").ExecuteCommand("Select Emp_Code From HrdMastQry Where Emp_Code = '" & Chk(txtEM_CD.Text) & "'" & Session("UserCodes"), , DAL.DataLayer.ExecutionType.ExecuteScalar)

                If Code <> "" Then
                    SetMsg(LblMsg, "Access is dinied because ,This Employee Exist For Other Location.")
                    '  Else
                    'SetMsg(LblMsg, "This Employee does not exist .")
                End If

                ViewState("Value") = False
                LblName.Text = ""
            End If
            'By ravi on 7 dec 2007
            StrSql = " Select HrdFamily.Emp_Code,Relative_Name,Relation, " & _
                    " Right('00' + DateName(dd,Relative_DOB),2) + '/' + Left(DateName(m,Relative_DOB),3) + '/' + DateName(yyyy,Relative_DOB) Relative_DOB " & _
                    " ,isNull(Dependent,0) As Dependent,isNull(Nominee,0) As Nominee,RelativeAge From HrdFamily " & _
                    " Inner Join HrdMast On HrdFamily.Emp_Code = HrdMast.Emp_Code " & _
                    " Where HrdFamily.Emp_Code = '" & Chk(txtEM_CD.Text) & "'" & Session("UserCodes") & " Order By HrdFamily.Relative_DOB Desc"
            '------------------------------------------------------------
            BindGridFamily()
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (txtEM_CD_TextChanged)")
        End Try
    End Sub
    'By Ravi on 7 dec 2006
    Private Sub BtnFirst_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFirst.ServerClick
        Try
            txtEM_CD.Text = Chk(Session("DalObj").ExecuteCommand("SELECT DISTINCT TOP 1 EMP_CODE FROM HRDMAST WHERE 1=1 " & Session("UserCodes") & " Order By Emp_Code ", DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
            BindGridFamily()
            'txtEM_CD.Text = Chk(Session("DalObj").ExecuteCommand("SELECT  DISTINCT TOP 1  HrdFamily.Emp_Code FROM HrdFamily INNER JOIN HrdMast On HrdFamily.Emp_Code = HrdMast.Emp_Code  Order By HrdFamily.Emp_Code", DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))

            'StrSql = " Select  HrdFamily.Emp_Code,Relative_Name,Relation, " & _
            '         " Right('00' + DateName(dd,Relative_DOB),2) + '/' + Left(DateName(m,Relative_DOB),3) + '/' + DateName(yyyy,Relative_DOB) Relative_DOB " & _
            '         " ,isNull(Dependent,0) As Dependent,isNull(Nominee,0) As Nominee,RelativeAge From HrdFamily " & _
            '         " Inner Join HrdMast On HrdFamily.Emp_Code = HrdMast.Emp_Code " & _
            '        " Where HrdFamily.Emp_Code = '" & Chk(txtEM_CD.Text) & "' " & Session("UserCodes") & " Order By HrdFamily.Relative_DOB Desc"
            'BindGridFamily(StrSql)

        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (BtnFirst_ServerClick)")
        End Try
    End Sub
    'By Ravi on 7 dec 2006
    Private Sub BtnLast_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLast.ServerClick
        Try

            txtEM_CD.Text = Chk(Session("DalObj").ExecuteCommand("SELECT DISTINCT TOP 1 EMP_CODE FROM HRDMAST WHERE 1=1 " & Session("UserCodes") & " Order By Emp_Code DESC", DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
            BindGridFamily()
            'txtEM_CD.Text = Chk(Session("DalObj").ExecuteCommand("SELECT  DISTINCT TOP 1  HrdFamily.Emp_Code FROM HrdFamily INNER JOIN HrdMast On HrdFamily.Emp_Code = HrdMast.Emp_Code  Order By HrdFamily.Emp_Code DESC", DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))

            'StrSql = " Select  HrdFamily.Emp_Code,Relative_Name,Relation, " & _
            '         " Right('00' + DateName(dd,Relative_DOB),2) + '/' + Left(DateName(m,Relative_DOB),3) + '/' + DateName(yyyy,Relative_DOB) Relative_DOB " & _
            '         " ,isNull(Dependent,0) As Dependent,isNull(Nominee,0) As Nominee,RelativeAge From HrdFamily " & _
            '         " Inner Join HrdMast On HrdFamily.Emp_Code = HrdMast.Emp_Code " & _
            '        " Where HrdFamily.Emp_Code = '" & Chk(txtEM_CD.Text) & "' " & Session("UserCodes") & " Order By HrdFamily.Relative_DOB Desc"
            'BindGridFamily(StrSql)

        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (BtnLast_ServerClick)")
        End Try
    End Sub
    'By Ravi on 7 dec 2006
    Private Sub BtnPre_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPre.ServerClick
        Try
            txtEM_CD.Text = Chk(Session("DalObj").ExecuteCommand("SELECT DISTINCT TOP 1 EMP_CODE FROM HRDMAST WHERE EMP_CODE < '" & Chk(txtEM_CD.Text) & "' " & Session("UserCodes") & " Order By Emp_Code DESC ", DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
            BindGridFamily()

            'txtEM_CD.Text = Chk(Session("DalObj").ExecuteCommand("SELECT  DISTINCT TOP 1  HrdFamily.Emp_Code FROM HrdFamily INNER JOIN HrdMast On HrdFamily.Emp_Code = HrdMast.Emp_Code WHERE HrdFamily.Emp_Code <'" & Chk(txtEM_CD.Text) & "' Order By HrdFamily.Emp_Code  DESC  ", DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))

            'StrSql = " Select  HrdFamily.Emp_Code,Relative_Name,Relation, " & _
            '         " Right('00' + DateName(dd,Relative_DOB),2) + '/' + Left(DateName(m,Relative_DOB),3) + '/' + DateName(yyyy,Relative_DOB) Relative_DOB " & _
            '         " ,isNull(Dependent,0) As Dependent,isNull(Nominee,0) As Nominee,RelativeAge From HrdFamily " & _
            '         " Inner Join HrdMast On HrdFamily.Emp_Code = HrdMast.Emp_Code " & _
            '        " Where HrdFamily.Emp_Code = '" & Chk(txtEM_CD.Text) & "' " & Session("UserCodes") & " Order By HrdFamily.Relative_DOB Desc"
            'BindGridFamily(StrSql)
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (BindGridFamily)")
        End Try
    End Sub
    'By Ravi on 7 dec 2006
    Private Sub BtnNext_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNext.ServerClick
        Try
            txtEM_CD.Text = Chk(Session("DalObj").ExecuteCommand("SELECT DISTINCT TOP 1 EMP_CODE FROM HRDMAST WHERE EMP_CODE > '" & Chk(txtEM_CD.Text) & "' " & Session("UserCodes") & " Order By Emp_Code ", DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
            BindGridFamily()
            'txtEM_CD.Text = Chk(Session("DalObj").ExecuteCommand("SELECT  DISTINCT TOP 1  HrdFamily.Emp_Code FROM HrdFamily INNER JOIN HrdMast On HrdFamily.Emp_Code = HrdMast.Emp_Code WHERE HrdFamily.Emp_Code >'" & Chk(txtEM_CD.Text) & "' Order By HrdFamily.Emp_Code ", DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))

            'StrSql = " Select  HrdFamily.Emp_Code,Relative_Name,Relation, " & _
            '         " Right('00' + DateName(dd,Relative_DOB),2) + '/' + Left(DateName(m,Relative_DOB),3) + '/' + DateName(yyyy,Relative_DOB) Relative_DOB " & _
            '         " ,isNull(Dependent,0) As Dependent,isNull(Nominee,0) As Nominee,RelativeAge From HrdFamily " & _
            '         " Inner Join HrdMast On HrdFamily.Emp_Code = HrdMast.Emp_Code " & _
            '        " Where HrdFamily.Emp_Code = '" & Chk(txtEM_CD.Text) & "' " & Session("UserCodes") & " Order By HrdFamily.Relative_DOB Desc"

            'BindGridFamily(StrSql)
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (BtnNext_ServerClick)")
        End Try
    End Sub
    'By Ravi on 7 dec 2006
    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
        Session("EM_CD") = Trim(txtEM_CD.Text)
    End Sub
End Class
