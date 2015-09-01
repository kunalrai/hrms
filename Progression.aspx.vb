Partial Class Progression
    Inherits System.Web.UI.Page
    'Dim ObjDal As DAL.DataLayer.Users
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents cmdDelete As System.Web.UI.WebControls.Button

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

        Try
            If IsNothing(Session("LoginUser")) Then
                Response.Redirect("CompSel.aspx")
            End If

            'By Ravi 17 nov 2006 This page has not any Save Button

            'Dim SrNo As String
            'SrNo = Request.QueryString.Item("SrNo")

            'If CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules <> "" Then
            '    If InStr(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, SrNo & "-") <> 0 Then
            '        Dim int As Int16, st As String
            '        int = InStr(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, SrNo)
            '        st = Right(Mid(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, int, Len(SrNo & "-") + 1), 1)

            '        If st = "S" Then
            '            txtEM_CD.ReadOnly = False
            '        Else
            '            txtEM_CD.ReadOnly = True
            ' cmdDelete.Visible = False
            '        End If
            '    Else
            '        txtEM_CD.ReadOnly = True
            '        SetMsg(LblRights, "You are not authorised to view data on this page.")
            '        Exit Sub
            '    End If
            'End If

            If Not IsPostBack Then
                txtEM_CD.Text = Session("EM_CD")
            End If
            BindGrid()
        Catch ex As Exception
            SetMsg(LblMsg, "Progression Form" & ex.Message)
        End Try
    End Sub

    Private Sub BindGrid()
        Try
            Dim EmpName As Object, StrQry As String, Dt As DataTable

            Dt = New DataTable

            EmpName = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(" Select Emp_Name From HrdMastQry Where Emp_Code = '" & Chk(txtEM_CD.Text) & "'" & Session("UserCodes"), , DAL.DataLayer.ExecutionType.ExecuteScalar)
            If (Not IsDBNull(EmpName)) And (Not IsNothing(EmpName)) Then
                LblName.Text = EmpName
            Else
                Dim Code As Object
                'By Ravi
                'Code = Session("DalObj").ExecuteCommand("Select Emp_Code From HrdMastQry Where Emp_Code = '" & Chk(txtEM_CD.Text) & "'" & Session("UserCodes"), , DAL.DataLayer.ExecutionType.ExecuteScalar)
                Code = Session("DalObj").ExecuteCommand("Select Emp_Code From HrdMastQry Where Emp_Code = '" & Chk(txtEM_CD.Text) & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)

                If (Not IsDBNull(Code)) And (Not IsNothing(Code)) Then
                    'SetMsg(LblMsg, "This Employee Code Exist For Other Location.")
                    'by ravi
                    SetMsg(LblMsg, "Access is dinied because ,This Employee Exist For Other Location.")
                End If
                LblName.Text = ""
            End If

            StrQry = " SELECT  RIGHT('00' + DATENAME(dd, EffectiveDate), 2) + '/' + LEFT(DATENAME(mm, EffectiveDate), 3) + '/' + DATENAME(yyyy, EffectiveDate) AS [From Date], " & _
                     " RIGHT('00' + DATENAME(dd, ToDate), 2) + '/' + LEFT(DATENAME(mm, ToDate), 3) + '/' + DATENAME(yyyy, ToDate) AS [To Date], Grd_Name AS Band,  " & _
                     " Dsg_Name AS Designation, Dept_Name AS Department, Divi_Name AS [Cost Center], EmpStatus AS [Employee Status], AnnualSal AS [Base Salary],  " & _
                     " BandPerc AS [Band Penetration (%)], Performance AS [Performance Rating], isnull(PerBR,'-') as BR, isnull(PerPR,'-')as PR, isnull(PerCI,'-') as CI, isnull([PerIO],'-') as 'I/O', PerOthers AS [% Merit],  " & _
                     " PerEquity AS [% Equity], PerPromotion AS [% Promotion], Per_Annual AS [% Prog], ISNULL(PerOthers, 0) + ISNULL(PerEquity, 0) + ISNULL(PerPromotion, " & _
                     "  0) + ISNULL(Per_Annual, 0) AS [% Total], Remarks, WorkHistory.Emp_Code AS Code FROM WorkHistory Inner Join HrdMast On WorkHistory.Emp_Code = HrdMast.Emp_Code   where WorkHistory.Emp_Code='" & Trim(txtEM_CD.Text) & "'" & Session("UserCodes") & " ORDER BY Cast(EffectiveDate as SmallDateTime)"
            'Session("DalObj").GetSqlDataTable(ViewState("DtProg"), "Select QryWorkHistory.* From QryWorkHistory Inner Join HrdMast On QryWorkHistory.Code = HrdMast.Emp_Code where [QryWorkHistory].[Code]='" & Trim(txtEM_CD.Text) & "'" & Session("UserCodes") & " ORDER BY Cast([From Date] as SmallDateTime)")
            Session("DalObj").GetSqlDataTable(Dt, StrQry)
            GrdProgression.DataSource = Dt
            GrdProgression.DataBind()
        Catch ex As Exception
            SetMsg(LblMsg, "Progression Form" & ex.Message)
        End Try
    End Sub

    Private Sub txtEM_CD_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEM_CD.TextChanged
        Try
            If Trim(txtEM_CD.Text) <> "" Then Session("EM_CD") = Trim(txtEM_CD.Text)
            BindGrid()
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (txtEM_CD_TextChanged)")
        End Try
    End Sub

    'Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
    '    Try
    '        Dim i As Int16, StrQry As String

    '        For i = 0 To GrdProgression.Items.Count - 1
    '            If Not IsNothing(CType(GrdProgression.Items(i).FindControl("chkDelete"), CheckBox)) Then
    '                If CType(GrdProgression.Items(i).FindControl("chkDelete"), CheckBox).Checked = True Then
    '                    StrQry = " Delete From WorkHistory Where EMP_CODE='" & txtEM_CD.Text & "' and FinYear='" & Session("FinYear") & "' and EffectiveDate='" & GrdProgression.Items(i).Cells(1).Text & "' and ToDate='" & GrdProgression.Items(i).Cells(2).Text & "'"
    '                    CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(StrQry)
    '                End If
    '            End If
    '        Next
    '        BindGrid()
    '    Catch ex As Exception
    '        SetMsg(LblMsg, ex.Message & " : (cmdDelete_Click)")
    '    End Try
    'End Sub

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub

    Private Sub GrdProgression_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GrdProgression.SelectedIndexChanged
        Try
            Dim StrQry As String
            StrQry = " Delete From WorkHistory Where EMP_CODE='" & txtEM_CD.Text & "' and FinYear='" & Session("FinYear") & "' and EffectiveDate='" & GrdProgression.SelectedItem.Cells(1).Text & "' and ToDate='" & GrdProgression.SelectedItem.Cells(2).Text & "'"
            CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(StrQry)
            BindGrid()
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " : (GrdProgression_SelectedIndexChanged)")
        End Try
    End Sub
    Private Sub BtnFirst_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFirst.ServerClick
        txtEM_CD.Text = Chk(Session("DalObj").ExecuteCommand("SELECT DISTINCT TOP 1 EMP_CODE FROM HRDMAST WHERE 1=1 " & Session("UserCodes") & " Order By Emp_Code ", DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
        BindGrid()
    End Sub

    Private Sub BtnPre_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPre.ServerClick
        txtEM_CD.Text = Chk(Session("DalObj").ExecuteCommand("SELECT DISTINCT TOP 1 EMP_CODE FROM HRDMAST WHERE EMP_CODE < '" & Chk(txtEM_CD.Text) & "' " & Session("UserCodes") & " Order By Emp_Code DESC ", DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
        BindGrid()
    End Sub

    Private Sub BtnNext_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNext.ServerClick
        txtEM_CD.Text = Chk(Session("DalObj").ExecuteCommand("SELECT DISTINCT TOP 1 EMP_CODE FROM HRDMAST WHERE EMP_CODE > '" & Chk(txtEM_CD.Text) & "' " & Session("UserCodes") & " Order By Emp_Code ", DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
        BindGrid()
    End Sub

    Private Sub BtnLast_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLast.ServerClick
        txtEM_CD.Text = Chk(Session("DalObj").ExecuteCommand("SELECT DISTINCT TOP 1 EMP_CODE FROM HRDMAST WHERE 1=1 " & Session("UserCodes") & " Order By Emp_Code DESC", DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
        BindGrid()
    End Sub

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
        Session("EM_CD") = Trim(txtEM_CD.Text)
    End Sub
End Class
