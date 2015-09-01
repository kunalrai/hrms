Partial Class ResumeSearchResult
    Inherits System.Web.UI.Page

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

    Public Shared Query As String
    Public Shared OrgQuery As String
    Public Shared RestQuery As String
    Public Shared DtTemp As DataTable
    Public Shared DvTemp As DataView
    Dim DtResume As DataTable
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        If Not IsPostBack Then
            FillComboBox()
            BindResumesGrid()
        End If
    End Sub

    Sub FillComboBox()
        Dim i, j As Int16, Item As ListItem

        cmbExpFrom.Items.Insert(0, "")
        CmbExpTo.Items.Insert(0, "")

        For i = 0 To 25
            Item = New ListItem
            Item.Text = i + 1
            Item.Value = i + 1
            cmbExpFrom.Items.Add(Item)
            CmbExpTo.Items.Add(Item)
        Next

        cmbExpFrom.DataBind()
        CmbExpTo.DataBind()
    End Sub

    Sub BindResumesGrid()

        'If Not IsNothing(DtTemp) Then
        '    DtResume = New DataTable
        '    Session("DalObj").GetSqlDataTable(DtResume, Query)
        '    GrdResumes.DataSource = DtResume
        '    GrdResumes.DataBind()
        'End If
        Try
            If Query <> "" Then
                ViewState("Query") = Query
                DtResume = New DataTable
                Session("DalObj").GetSqlDataTable(DtResume, ViewState("Query"))
                GrdResumes.DataSource = DtResume
                GrdResumes.DataBind()
            End If
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : BindResumesGrid()")
        End Try

    End Sub

    Private Sub GrdResumes_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles GrdResumes.ItemDataBound
        Try
            Dim i, j As Int16, Dt As DataTable, Qual, Skills As String
            i = e.Item.ItemIndex

            If Not IsNothing(CType(e.Item.FindControl("LblQual"), Label)) Then

                '========================================Display Qualification =========================================
                Dim LblQualification As Label
                LblQualification = CType(e.Item.FindControl("LblQual"), Label)
                Dim Code As Int16 = DtResume.Rows(i).Item("Res_Code")

                Dt = New DataTable
                Session("DalObj").GetSqlDataTable(Dt, " Select RQ.Res_Code, RQ.Qual_Code, QM.Qual_Name from ResQual RQ Inner Join QualMast QM On RQ.Qual_Code = QM.Qual_Code Where RQ.Res_Code = '" & Code & "'")


                For j = 0 To Dt.Rows.Count - 1
                    Qual = Qual & Dt.Rows(j).Item("Qual_Name") & ", "
                Next
                If Qual <> "" Then
                    LblQualification.Text = Mid(Qual, 1, Len(Qual.Trim) - 1)
                End If

                '=========================================== Display Skills =========================================
                Dim LblSkill As Label
                LblSkill = CType(e.Item.FindControl("LblSkills"), Label)

                Dt = New DataTable
                Session("DalObj").GetSqlDataTable(Dt, " Select Res_Code, SM.Skill_Name from ResSkills RS Inner Join SkillMast SM On RS.Skill_Code = SM.Skill_Code Where Res_Code = '" & Code & "'")

                For j = 0 To Dt.Rows.Count - 1
                    Skills = Skills & Dt.Rows(j).Item("Skill_Name") & ", "
                Next
                If Skills <> "" Then
                    LblSkill.Text = Mid(Skills, 1, Len(Skills.Trim) - 1)
                End If
            End If
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : Bind DataGrid()")
        End Try
    End Sub

    Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        Try
            Query = OrgQuery

            If cmbExpFrom.SelectedValue <> "" And CmbExpTo.SelectedValue <> "" Then
                Query = Query & " Having (Sum(Exp_Years) >=" & ChkN(cmbExpFrom.SelectedValue) & " and Sum(Exp_Years) <=" & ChkN(CmbExpTo.SelectedValue) & ") "
            End If

            'If cmbSort.SelectedValue = 0 Then
            '    Query = Query & " Order By ResMast.Res_Code"
            'ElseIf cmbSort.SelectedValue = 1 Then
            '    Query = Query & " Order By ResMast.Res_NameF"
            'Else
            '    Query = Query & " Order By ResExp"
            'End If
            Query = Query & RestQuery
            BindResumesGrid()
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : Search Click()")
        End Try
    End Sub

    Private Sub GrdResumes_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles GrdResumes.PageIndexChanged

        Try
            If (GrdResumes.CurrentPageIndex > GrdResumes.PageCount Or GrdResumes.CurrentPageIndex < 0) Then
                GrdResumes.CurrentPageIndex = 0
            Else
                GrdResumes.CurrentPageIndex = e.NewPageIndex
            End If
            BindResumesGrid()
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : Page Changed()")
        End Try
    End Sub

End Class
