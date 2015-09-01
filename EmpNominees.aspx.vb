Partial Class family1
    Inherits System.Web.UI.Page

    Dim dt_hrdfamily As New DataTable
    Dim dt_hrdmast As New DataTable
    Dim StrSql As String
    Dim upd_hrdmast, upd_hrdfamily As String
    Dim obj As Object
    Dim rl_name As String
    Dim pf_str As TextBox
    Dim saf_str As TextBox
    Dim gratuity_str As TextBox
    Dim hdfc_str As TextBox
    Dim ct As Integer
    'Dim dr As SqlClient.SqlDataReader
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
        If Server.GetLastError Is Nothing Then SetMsg(LblMsg, "")
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
                    Case MdlHRMS.AccessType.ReadonlyAccess
                        bSuccess = False
                    Case MdlHRMS.AccessType.Restricted
                        Response.Redirect(Request.UrlReferrer.ToString)
                        Exit Sub
                End Select
                cmdsave.Visible = bSuccess
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
            '            cmdsave.Visible = False
            '        End If
            '    Else
            '        txtEM_CD.ReadOnly = True
            '        cmdsave.Visible = False
            '        SetMsg(LblRights, "You are not authorised to view data on this page.")
            '        'Response.Write("<BR><center><B></B></center>")
            '        Exit Sub
            '    End If

            'End If

            'If Session("LoginUser").UserGroup <> "ADMIN" Then
            '    txtEM_CD.ReadOnly = True
            '    'EnableDisable(False, Me)
            '    cmdsave.Visible = False
            'Else
            '    txtEM_CD.ReadOnly = False
            'End If

            txtEM_CD.Text = Session("EM_CD")
            txtEM_CD_TextChanged(sender, e)
        End If
    End Sub
    Private Sub txtEM_CD_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEM_CD.TextChanged
        'By Ravi on 7 Dec 2006
        Try
            Dim strsql = "select relative_name,Relation,hdfcperc,gratuityperc,safperc,pfperc from hrdfamily where emp_code='" & Chk(txtEM_CD.Text) & "'"
            BindGridFamily()

            'comment By Ravi
            'LblMsg.Text = ""
            'Try
            '    If txtEM_CD.Text <> "" Then
            '        Session("EM_CD") = txtEM_CD.Text
            '    End If

            '    obj = CType(Session("dalobj"), DAL.DataLayer).ExecuteCommand("select emp_name from hrdmastqry where emp_code='" & Chk(txtEM_CD.Text) & "'" & Session("UserCodes"), , DAL.DataLayer.ExecutionType.ExecuteScalar)

            '    LblName.Text = CType(obj, String)

            '    CType(Session("dalobj"), DAL.DataLayer).GetSqlDataTable(dt_hrdfamily, "select relative_name,Relation,hdfcperc,gratuityperc,safperc,pfperc from hrdfamily where emp_code='" & Chk(txtEM_CD.Text) & "'")

            '    CType(Session("dalobj"), DAL.DataLayer).GetSqlDataTable(dt_hrdmast, "select HDFCCertNo,HDFCSumAssu,HDFCDOE from hrdmast where emp_code='" & Chk(txtEM_CD.Text) & "'" & Session("UserCodes"))

            '    If dt_hrdmast.Rows.Count > 0 Then

            '        txt_cerno.Text = CType(Chk(dt_hrdmast.Rows(0)(0)), String)
            '        txt_sumassu.Text = CType(ChkN(dt_hrdmast.Rows(0)(1)), String)

            '        If Not IsDBNull(dt_hrdmast.Rows(0)(2)) Then
            '            dtpDOE.DateValue = dt_hrdmast.Rows(0)(2)
            '        End If
            '    Else
            '        txt_cerno.Text = ""
            '        txt_sumassu.Text = ""
            '        dtpDOE.DateValue = CDate("01/Jan/1900")
            '    End If
            '    GrdFamily.DataSource = dt_hrdfamily
            '    GrdFamily.DataBind()


        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & "txtEM_CD_TextChanged")
        End Try
    End Sub
    Private Sub cmdclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclose.Click
        Response.Redirect("EmpExplorer.aspx?SrNo=61")
    End Sub
    Private Sub cmdsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdsave.Click
        If check_code() Then
            Dim trans As SqlClient.SqlTransaction
            Try
                trans = CType(Session("dalobj"), DAL.DataLayer).StartTransaction("SaveNominee")
                'upd_hrdmast = "update hrdmast set hdfccertno='" & _
                '              txt_cerno.Text & "',hdfcsumassu='" & _
                '              ChkN(txt_sumassu.Text) & "',hdfcdoe='" & _
                '              Format(dtpDOE.DateValue, "dd/MMM/yyyy") & _
                '              "' where emp_code='" & Session("EM_CD") & "'"

                'upd_hrdmast = "update hrdmast set hdfccertno='" & txt_cerno.Text & "',hdfcsumassu='" & ChkN(txt_sumassu.Text) & "',hdfcdoe='" & IIf(ChkDOE.Checked = True, Format(dtpDOE.DateValue, "dd/MMM/yyyy"), System.DBNull.Value) & "' where emp_code='" & Session("EM_CD") & "'"


                'comment by Ravi 

                'upd_hrdmast = "update hrdmast set " & _
                '        "hdfccertno='" & txt_cerno.Text & "'," & _
                '        "hdfcsumassu='" & ChkN(txt_sumassu.Text) & "'," & _
                '        "hdfcdoe=" & IIf(ChkDOE.Checked = True, "'" & Format(dtpDOE.DateValue, "dd/MMM/yyyy") & "'", "Null") & _
                '        " where emp_code='" & Session("EM_CD") & "'"


                'CType(Session("dalobj"), DAL.DataLayer).ExecuteCommand(upd_hrdmast, trans)
                'For ct = 0 To GrdFamily.Items.Count - 1

                '    rl_name = GrdFamily.Items(ct).Cells(0).Text
                '    pf_str = CType(GrdFamily.Items(ct).Cells(2).Controls(1), TextBox)
                '    saf_str = CType(GrdFamily.Items(ct).Cells(3).Controls(1), TextBox)
                '    gratuity_str = CType(GrdFamily.Items(ct).Cells(4).Controls(1), TextBox)
                '    hdfc_str = CType(GrdFamily.Items(ct).Cells(5).Controls(1), TextBox)

                '    upd_hrdfamily = "update HrdFamily set PFPerc='" & pf_str.Text & _
                '                    "',SAFPerc='" & saf_str.Text & "',GratuityPerc='" & _
                '                    gratuity_str.Text & "',HDFCPerc='" & hdfc_str.Text & _
                '                    "' where emp_code='" & Session("EM_CD") & _
                '                    "' and relative_name='" & rl_name & "' "

                '    CType(Session("dalobj"), DAL.DataLayer).ExecuteCommand(upd_hrdfamily, trans)

                'Next

                'trans.Commit()
                'txtEM_CD_TextChanged(sender, e)

                'By Ravi 28 dec 2006
                Dim pf, saf, gra, hdfc As Decimal

                upd_hrdmast = "update hrdmast set " & _
                                       "hdfccertno='" & txt_cerno.Text & "'," & _
                                       "hdfcsumassu='" & ChkN(txt_sumassu.Text) & "'," & _
                                       "hdfcdoe=" & IIf(ChkDOE.Checked = True, "'" & Format(dtpDOE.DateValue, "dd/MMM/yyyy") & "'", "Null") & _
                                      " where emp_code='" & Session("EM_CD") & "'"
                CType(Session("dalobj"), DAL.DataLayer).ExecuteCommand(upd_hrdmast, trans)
                For ct = 0 To GrdFamily.Items.Count - 1

                    rl_name = GrdFamily.Items(ct).Cells(0).Text
                    pf_str = CType(GrdFamily.Items(ct).Cells(2).Controls(1), TextBox)
                    saf_str = CType(GrdFamily.Items(ct).Cells(3).Controls(1), TextBox)
                    gratuity_str = CType(GrdFamily.Items(ct).Cells(4).Controls(1), TextBox)
                    hdfc_str = CType(GrdFamily.Items(ct).Cells(5).Controls(1), TextBox)

                    upd_hrdfamily = "update HrdFamily set PFPerc='" & pf_str.Text & _
                                    "',SAFPerc='" & saf_str.Text & "',GratuityPerc='" & _
                                    gratuity_str.Text & "',HDFCPerc='" & hdfc_str.Text & _
                                    "' where emp_code='" & Session("EM_CD") & _
                                    "' and relative_name='" & rl_name & "' "

                    CType(Session("dalobj"), DAL.DataLayer).ExecuteCommand(upd_hrdfamily, trans)
                    pf = pf + Val(pf_str.Text)
                    saf = saf + Val(saf_str.Text)
                    gra = gra + Val(gratuity_str.Text)
                    hdfc = hdfc + Val(hdfc_str.Text)
                Next
                If pf <= 100 Then
                    If saf <= 100 Then
                        If gra <= 100 Then
                            If hdfc <= 100 Then
                                trans.Commit()
                                txtEM_CD_TextChanged(sender, e)
                            Else
                                trans.Rollback()
                                SetMsg(LblMsg, "Total  HDFC% Should not more than 100%")

                            End If
                        Else
                            trans.Rollback()
                            SetMsg(LblMsg, "Total  GRATUITY% Should not more than 100%")
                        End If
                    Else
                        trans.Rollback()
                        SetMsg(LblMsg, "Total  SAF% Should not more than 100%")
                    End If
                Else
                    trans.Rollback()
                    SetMsg(LblMsg, "Total PF% Should be equal to 100%")

                End If

                '----------------------------------------------------------
                '----------------------------------------------------------


            Catch ex As Exception
                trans.Rollback()
                SetMsg(LblMsg, ex.Message)
            End Try

        Else
            SetMsg(LblMsg, "Code Cann't Be Left Blank!")
        End If

    End Sub
    Private Function check_code() As Boolean
        If txtEM_CD.Text = "" Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub
    'By Ravi on 7 dec 2006
    Private Sub BindGridFamily()
        LblMsg.Text = ""
        Try
            StrSql = Replace(StrSql, "AND", " AND hrdmast.")
            ' If Trim(txtEM_CD.Text) <> "" Then
            If txtEM_CD.Text <> "" Then
                Session("EM_CD") = txtEM_CD.Text
            End If

            obj = CType(Session("dalobj"), DAL.DataLayer).ExecuteCommand("select emp_name from hrdmastqry where emp_code='" & Chk(txtEM_CD.Text) & "'" & Session("UserCodes"), , DAL.DataLayer.ExecutionType.ExecuteScalar)

            LblName.Text = CType(obj, String)

            CType(Session("dalobj"), DAL.DataLayer).GetSqlDataTable(dt_hrdfamily, "select relative_name,Relation,hdfcperc,gratuityperc,safperc,pfperc from hrdfamily where nominee=1 and emp_code='" & Chk(txtEM_CD.Text) & "'")
            CType(Session("dalobj"), DAL.DataLayer).GetSqlDataTable(dt_hrdmast, "select HDFCCertNo,HDFCSumAssu,HDFCDOE from hrdmast where emp_code='" & Chk(txtEM_CD.Text) & "'" & Session("UserCodes"))

            If dt_hrdmast.Rows.Count > 0 Then

                txt_cerno.Text = CType(Chk(dt_hrdmast.Rows(0)(0)), String)
                txt_sumassu.Text = CType(ChkN(dt_hrdmast.Rows(0)(1)), String)

                'Comment By Ravi
                'If Not IsDBNull(dt_hrdmast.Rows(0)(2)) Then
                '    dtpDOE.DateValue = dt_hrdmast.Rows(0)(2)
                '    ChkDOE.Checked = False
                'End If

                'By Ravi on 13 dec 2006
                If Not IsDBNull(dt_hrdmast.Rows(0)(2)) Then
                    ChkDOE.Checked = True
                    dtpDOE.Enabled = True
                    dtpDOE.DateValue = dt_hrdmast.Rows(0)(2)
                Else
                    ChkDOE.Checked = False
                    dtpDOE.Enabled = False
                End If
                '--------------------------
            Else
                txt_cerno.Text = ""
                txt_sumassu.Text = ""
                dtpDOE.DateValue = CDate("01/Jan/1900")
            End If

            GrdFamily.DataSource = dt_hrdfamily
            GrdFamily.DataBind()
            GrdFamily.Visible = True

        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " :BindGridFamily()")

        End Try

    End Sub
    'By Ravi on 7 dec 2006
    Private Sub BtnFirst_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFirst.ServerClick
        Try
            txtEM_CD.Text = Chk(Session("DalObj").ExecuteCommand("SELECT DISTINCT TOP 1 EMP_CODE FROM HRDMAST WHERE 1=1 " & Session("UserCodes") & " Order By Emp_Code ", DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
            BindGridFamily()
            'txtEM_CD.Text = Chk(Session("DalObj").ExecuteCommand("SELECT  DISTINCT TOP 1  HrdFamily.Emp_Code FROM HrdFamily INNER JOIN HrdMast On HrdFamily.Emp_Code = HrdMast.Emp_Code  Order By HrdFamily.Emp_Code", DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
            'StrSql = "select relative_name,Relation,hdfcperc,gratuityperc,safperc,pfperc from hrdfamily where emp_code='" & Chk(txtEM_CD.Text) & "'"
            'BindGridFamily(StrSql)

        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (BtnFirst_ServerClick)")
        End Try
    End Sub
    'By Ravi on 7 dec 2006
    Private Sub BtnPre_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPre.ServerClick
        Try
            txtEM_CD.Text = Chk(Session("DalObj").ExecuteCommand("SELECT DISTINCT TOP 1 EMP_CODE FROM HRDMAST WHERE EMP_CODE < '" & Chk(txtEM_CD.Text) & "' " & Session("UserCodes") & " Order By Emp_Code DESC ", DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
            BindGridFamily()
            'txtEM_CD.Text = Chk(Session("DalObj").ExecuteCommand("SELECT  DISTINCT TOP 1  HrdFamily.Emp_Code FROM HrdFamily INNER JOIN HrdMast On HrdFamily.Emp_Code = HrdMast.Emp_Code WHERE HrdFamily.Emp_Code <'" & Chk(txtEM_CD.Text) & "' Order By HrdFamily.Emp_Code  DESC  ", DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
            'StrSql = "select relative_name,Relation,hdfcperc,gratuityperc,safperc,pfperc from hrdfamily where emp_code='" & Chk(txtEM_CD.Text) & "'"
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
            'StrSql = "select relative_name,Relation,hdfcperc,gratuityperc,safperc,pfperc from hrdfamily where emp_code='" & Chk(txtEM_CD.Text) & "'"
            'BindGridFamily(StrSql)
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (BtnNext_ServerClick)")
        End Try
    End Sub
    'By Ravi on 7 dec 2006
    Private Sub BtnLast_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLast.ServerClick
        Try
            txtEM_CD.Text = Chk(Session("DalObj").ExecuteCommand("SELECT DISTINCT TOP 1 EMP_CODE FROM HRDMAST WHERE 1=1 " & Session("UserCodes") & " Order By Emp_Code DESC", DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
            BindGridFamily()
            'txtEM_CD.Text = Chk(Session("DalObj").ExecuteCommand("SELECT  DISTINCT TOP 1  HrdFamily.Emp_Code FROM HrdFamily INNER JOIN HrdMast On HrdFamily.Emp_Code = HrdMast.Emp_Code  Order By HrdFamily.Emp_Code DESC", DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
            'StrSql = "select relative_name,Relation,hdfcperc,gratuityperc,safperc,pfperc from hrdfamily where emp_code='" & Chk(txtEM_CD.Text) & "'"
            'BindGridFamily(StrSql)
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (BtnLast_ServerClick)")
        End Try
    End Sub

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
        Session("EM_CD") = txtEM_CD.Text
    End Sub


End Class
