Partial Class ResumeSearch
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If
        If Not IsPostBack Then
            Dim StrQry As String
            FillComboBox()
            ViewState("filter") = New DataTable
            StrQry = "Select '' as FieldCode, '' as FieldName, '' as Condition, '' as ValCode, '' as Val, '' as AndOr Where 1<>1"
            Session("DalObj").GetSqlDataTable(ViewState("filter"), StrQry)
            GrdResumes.DataSource = ViewState("filter")
            GrdResumes.DataBind()
        End If
    End Sub

    Sub FillComboBox()
        Dim i As Int16
        Session("BalObj").FillCombo(CmbField, "Select Field_Code, Field_Name From ResSearch", True)

        cmbAndOr.Items.Add("")
        cmbAndOr.Items.Add("AND")
        cmbAndOr.Items.Add("OR")
        cmbAndOr.DataBind()
    End Sub

    Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        Try
            Dim StrQry, RestQry, StrExp As String, Dt As New DataTable, Dv As DataView, i As Int16

            StrQry = " Select ResMast.Res_Code, Res_No, (isnull(Res_NameF,'')+' '+isnull(Res_NameM,'')+' '+isnull(Res_NameL,'')) as ResName, " & _
                      " Round(Sum(ResExp.Exp_Years),2) as ResExp From ResMast " & _
                      " Left Join ResExp On ResMast.Res_Code = ResExp.Res_Code " & _
                      " Left Join ResQual On ResMast.Res_Code = ResQual.Res_Code " & _
                      " Left Join ResSkills On ResMast.Res_Code = ResSkills.Res_Code "

            ResumeSearchResult.OrgQuery = StrQry

            If GrdResumes.Items.Count <> 0 Then
                StrQry = StrQry & " Where"

                StrExp = ""
                For i = 0 To GrdResumes.Items.Count - 1

                    If Chk(GrdResumes.Items(i).Cells(0).Text) = "ResExp" Then
                        StrExp = " Having (Sum(Exp_Years) " & Chk(GrdResumes.Items(i).Cells(2).Text) & _
                              " '" & Chk(GrdResumes.Items(i).Cells(5).Text) & "') "
                    Else
                        StrQry = StrQry & " (" & Chk(GrdResumes.Items(i).Cells(0).Text) & Chk(GrdResumes.Items(i).Cells(2).Text) & _
                              " '" & Chk(GrdResumes.Items(i).Cells(5).Text) & "') " & Chk(GrdResumes.Items(i).Cells(4).Text) & " "
                    End If


                Next
            End If

            'If cmbExpFrom.SelectedValue <> "" And CmbExpTo.SelectedValue <> "" Then
            '    StrQry = StrQry & " Having (Sum(Exp_Years) >=" & ChkN(cmbExpFrom.SelectedValue) & " and Sum(Exp_Years) <=" & ChkN(CmbExpTo.SelectedValue) & ")"
            '    Dv.RowFilter = " ResExp >= " & cmbExpFrom.SelectedItem.Text & " and ResExp <= " & CmbExpTo.SelectedItem.Text
            '    ResumeSearchResult.DtTemp = Dv.Table
            'Else
            '    ResumeSearchResult.DtTemp = Dt
            'End If

            RestQry = " Group By ResMast.Res_Code, Res_No, Res_NameF, Res_NameM, Res_NameL"
            ResumeSearchResult.RestQuery = RestQry

            StrQry = StrQry & RestQry & StrExp
            ResumeSearchResult.Query = StrQry
            Response.Redirect("ResumeSearchResult.aspx")
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : Search Resume()")
        End Try
    End Sub

    Private Sub cmdNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNew.Click
        Try
            If CmbField.SelectedValue = "" Then SetMsg(LblErrMsg, " Select Field Name from the list.") : Exit Sub

            Dim dRow As DataRow
            dRow = ViewState("filter").NewRow

            dRow("FieldCode") = CmbField.SelectedItem.Value
            dRow("FieldName") = CmbField.SelectedItem.Text
            dRow("Condition") = cmbCondition.SelectedValue

            If cmbValues.Visible = True Then
                dRow("Val") = cmbValues.SelectedItem.Text
                dRow("ValCode") = cmbValues.SelectedValue
            Else
                dRow("Val") = TxtValue.Text
                dRow("ValCode") = TxtValue.Text
            End If
            dRow("AndOr") = cmbAndOr.SelectedValue

            ViewState("filter").Rows.Add(dRow)
            ViewState("filter").AcceptChanges()
            GrdResumes.DataSource = ViewState("filter")
            GrdResumes.DataBind()
            ResetControls()
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : Add ()")
        End Try
    End Sub

#Region " Field Changed "

    Private Sub CmbField_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbField.SelectedIndexChanged
        Try
            Dim Cont, Query As String, Dt As New DataTable, Condition As String, Cond() As String, i As Int16
            Session("DalObj").GetSqlDataTable(Dt, "Select ContType, ContQuery, Condition From ResSearch Where Field_Code = '" & Chk(CmbField.SelectedValue) & "'")

            cmbCondition.Items.Clear()
            If Dt.Rows.Count = 0 Then
                cmbValues.Visible = False
                TxtValue.Visible = False
                Exit Sub
            End If

            Condition = Chk(Dt.Rows(0).Item("Condition"))
            If Condition <> "" Then
                Cond = Split(Condition, "|")
            End If

            For i = 0 To Cond.Length - 1
                Dim Litem As New ListItem
                Litem.Text = Cond(i)
                Litem.Value = Cond(i)
                cmbCondition.Items.Add(Litem)
            Next
            cmbCondition.DataBind()


            Select Case UCase(Dt.Rows(0).Item("ContType"))
                Case "CMB"
                    TxtValue.Visible = False
                    cmbValues.Visible = True
                    Session("BalObj").FillCombo(cmbValues, Dt.Rows(0).Item("ContQuery"))

                Case "TXT"
                    TxtValue.Visible = True
                    cmbValues.Visible = False

            End Select

        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : Field Changed()")
        End Try
    End Sub

#End Region

    Sub ResetControls()
        cmbValues.Visible = False
        TxtValue.Visible = False
        CmbField.SelectedValue = ""
        cmbAndOr.SelectedValue = ""
        cmbCondition.Items.Clear()
    End Sub

    'StrQry = " Select Res_No, (isnull(Res_NameF,'')+' '+isnull(Res_NameM,'')+' '+isnull(Res_NameL,'')) as ResName, SM.Status_Name," & _
    '         " DM.Dept_Name, DG.Dsg_Name, Round(Sum(RE.Exp_Years),2) as ResExp from ResMast RM " & _
    '        " Left Join StatusMast SM ON  RM.Status_Code = SM.Status_Code " & _
    '       " Left Join ResExp RE On RM.Res_Code = RE.Res_Code " & _
    '      " Left Join DeptMast DM ON RM.Dept_Code = DM.Dept_Code " & _
    '     "  Left Join DSGMAST DG ON RM.DSG_CODE = DG.DSG_CODE" & _
    '    " Group By RM.Res_Code, Res_No, Res_NameF, Res_NameM, Res_NameL, DM.Dept_Name, DG.Dsg_Name, SM.Status_Name "

    Private Sub BtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Try
            Dim StrQry As String
            ResetControls()
            ViewState("filter") = New DataTable
            StrQry = "Select '' as FieldCode, '' as FieldName, '' as Condition, '' as ValCode, '' as Val, '' as AndOr Where 1<>1"
            Session("DalObj").GetSqlDataTable(ViewState("filter"), StrQry)
            GrdResumes.DataSource = ViewState("filter")
            GrdResumes.DataBind()
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : Reset()")
        End Try
    End Sub
End Class

