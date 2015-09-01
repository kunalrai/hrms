Partial Class TrainCalExplorer
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
    Dim StrQry As String
    Dim WithEvents DtTemp As New DataTable
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If
        Try
            If Not IsPostBack Then
                Viewstate("DtTemp") = New DataTable
                FillTrainCalenderGrid()
                FillEmployeeGrid()
                RdoSelected.Checked = True
                RdoSelected_CheckedChanged(RdoSelected, e)
            End If
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message)
        End Try
    End Sub

    Sub FillTrainCalenderGrid()
        Try
            DtTemp = New DataTable
            StrQry = " Select TC.TrainCalCode, TM.Train_Name as [TrainDesc], TM.Train_Code as [Code], " & _
                     " Train_From as [From], Train_To as [To], LM.LOC_NAME as [Location], " & _
                     " TRM.Trainer_Name as [Trainer], TrainFile from TRAINCALENDAR TC INNER JOIN TRAINMAST TM " & _
                     " on TC.Train_Code = TM.Train_Code Left Join LOCMAST LM ON TC.LOC_CODE = LM.LOC_CODE " & _
                     " left Join TrainerMast TRM On TC.Trainer_Code = TRM.Trainer_Code  Order By Train_From Desc"

            Session("DalObj").GetSqlDataTable(DtTemp, StrQry)
            'If DtTemp.Rows.Count = 0 Then Exit Sub
            'GrdTrainCalender.CurrentPageIndex = 0
            GrdTrainCalender.DataSource = DtTemp
            GrdTrainCalender.DataBind()
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message)
        End Try
    End Sub

    Private Sub GrdTrainCalender_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles GrdTrainCalender.PageIndexChanged
        Try
            If (GrdTrainCalender.CurrentPageIndex > GrdTrainCalender.PageCount Or GrdTrainCalender.CurrentPageIndex < 0) Then
                GrdTrainCalender.CurrentPageIndex = 0
            Else
                GrdTrainCalender.CurrentPageIndex = e.NewPageIndex
            End If
            FillTrainCalenderGrid()
            GrdTrainCalender.ItemStyle.ForeColor = Color.Black
            GrdTrainCalender.SelectedIndex = -1
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message)
        End Try
    End Sub

    Private Sub GrdTrainCalender_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles GrdTrainCalender.EditCommand
        Try
            Session("CalCode") = e.Item.Cells(2).Text
            Response.Redirect("TrainingCalender.aspx")
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message)
        End Try
    End Sub

    Private Sub GrdTrainCalender_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GrdTrainCalender.SelectedIndexChanged
        Try
            If GrdTrainCalender.SelectCommandName = "Select" Then
                GrdTrainCalender.ItemStyle.ForeColor = Color.Black
                GrdTrainCalender.SelectedItemStyle.ForeColor = Color.Brown
                ViewState("SelTrainCalCode") = GrdTrainCalender.SelectedItem.Cells(2).Text
                ViewState("SelTrainCode") = GrdTrainCalender.SelectedItem.Cells(4).Text
                ViewState("TrainFrom") = GrdTrainCalender.SelectedItem.Cells(5).Text
                ViewState("TrainTo") = GrdTrainCalender.SelectedItem.Cells(6).Text
                RdoSelected.Checked = True
                RdoAll.Checked = False
                RdoSuggested.Checked = False
                RdoRequested.Checked = False
                RdoSelected_CheckedChanged(RdoSelected, e)
            End If
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message)
        End Try
    End Sub

    Private Sub GrdTrainCalender_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles GrdTrainCalender.ItemDataBound
        Try
            If Not (e.Item.ItemType = ListItemType.Footer Or e.Item.ItemType = ListItemType.Header Or e.Item.ItemType = ListItemType.Pager Or e.Item.ItemType = ListItemType.Separator) Then
                e.Item.Attributes("OnClick") = GetPostBackClientHyperlink(CType(e.Item.Controls(1).Controls(0), LinkButton), "")
                e.Item.Style.Add("CURSOR", "hand")
            End If
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message)
        End Try
    End Sub

    Sub FillEmployeeGrid()
        Try
            Dim i As Int16
            DtTemp = New DataTable
            StrQry = " Select EMP_NAME as [NAME], Emp_Code from HrdMastQry Order By EMP_NAME"

            Session("DalObj").GetSqlDataTable(DtTemp, StrQry)
            GrdEmployee.DataSource = DtTemp
            GrdEmployee.DataBind()

            If IsNothing(Viewstate("DtTemp")) Or Viewstate("DtTemp").Rows.Count = 0 Then Exit Sub
            Dim DView As New DataView(Viewstate("DtTemp"))

            For i = 0 To GrdEmployee.Items.Count - 1
                DView.RowFilter = " Emp_Code = '" & Chk(GrdEmployee.Items(i).Cells(2).Text) & "'"
                If DView.Count <> 0 Then
                    If GrdEmployee.Items(i).Cells(2).Text = DView.Item(0).Item("Emp_Code") Then
                        CType(GrdEmployee.Items(i).Cells(0).Controls(0), LinkButton).ForeColor = Color.Brown
                        CType(GrdEmployee.Items(i).Cells(0).Controls(0), LinkButton).Text = "Selected"
                    End If
                End If
            Next
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message)
        End Try
    End Sub


    Private Sub GrdEmployee_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GrdEmployee.SelectedIndexChanged
        Try
            If CType(GrdEmployee.SelectedItem.Cells(0).Controls(0), LinkButton).Text = "UnSelected" Then
                CType(GrdEmployee.SelectedItem.Cells(0).Controls(0), LinkButton).ForeColor = Color.Brown
                CType(GrdEmployee.SelectedItem.Cells(0).Controls(0), LinkButton).Text = "Selected"
                GrdEmployee.SelectedItem.ForeColor = Color.Brown

                StrQry = " Insert TRAINEMP (Emp_Code,Train_Code,Train_From,Train_To) Values ('" & _
                         Chk(GrdEmployee.SelectedItem.Cells(2).Text) & "', '" & _
                         Chk(ViewState("SelTrainCalCode")) & "', '" & _
                         ViewState("TrainFrom") & "', '" & _
                         ViewState("TrainTo") & "' )"

                Session("DalObj").ExecuteCommand(StrQry)

            Else
                CType(GrdEmployee.SelectedItem.Cells(0).Controls(0), LinkButton).ForeColor = Color.FromName("#003366")
                CType(GrdEmployee.SelectedItem.Cells(0).Controls(0), LinkButton).Text = "UnSelected"
                GrdEmployee.SelectedItem.ForeColor = Color.FromName("#003366")

                StrQry = " Delete From TRAINEMP Where Train_Code='" & Chk(ViewState("SelTrainCalCode")) & "' " & _
                         " and Emp_Code='" & Chk(GrdEmployee.SelectedItem.Cells(2).Text) & "'"

                Session("DalObj").ExecuteCommand(StrQry)

            End If

            Viewstate("DtTemp") = New DataTable
            StrQry = " Select (isnull(FName,'')+' '+isnull(LNAME,'')) as [NAME], HM.Emp_Code From HrdMast HM " & _
                     " inner join TRAINEMP TE On HM.Emp_Code = TE.Emp_Code Where TE.Train_Code = '" & ViewState("SelTrainCalCode") & "'"

            Session("DalObj").GetSqlDataTable(Viewstate("DtTemp"), StrQry)

        Catch ex As Exception
            SetMsg(LblMsg, ex.Message)
        End Try
    End Sub

    Private Sub GrdEmployee_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles GrdEmployee.ItemDataBound
        Try
            If Not (e.Item.ItemType = ListItemType.Footer Or e.Item.ItemType = ListItemType.Header Or e.Item.ItemType = ListItemType.Pager Or e.Item.ItemType = ListItemType.Separator) Then
                e.Item.Attributes("OnClick") = GetPostBackClientHyperlink(CType(e.Item.Controls(0).Controls(0), LinkButton), "")
                e.Item.Style.Add("CURSOR", "hand")
            End If
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message)
        End Try
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Private Sub cmdNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNew.Click
        Response.Redirect("TrainingCalender.aspx")
    End Sub

    Private Sub RdoSelected_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdoAll.CheckedChanged, RdoRequested.CheckedChanged, RdoSuggested.CheckedChanged, RdoSelected.CheckedChanged
        Try
            Dim i As Int16
            Select Case sender.id

                Case "RdoAll"

                    DtTemp = New DataTable
                    StrQry = " Select EMP_NAME as [NAME], Emp_Code From HrdMastQry Order by EMP_NAME"

                    Session("DalObj").GetSqlDataTable(DtTemp, StrQry)
                    GrdEmployee.CurrentPageIndex = 0
                    GrdEmployee.DataSource = DtTemp
                    GrdEmployee.DataBind()

                    If IsNothing(Viewstate("DtTemp")) Or Viewstate("DtTemp").Rows.Count = 0 Then Exit Sub
                    Dim DView As New DataView(Viewstate("DtTemp"))

                    For i = 0 To GrdEmployee.Items.Count - 1
                        DView.RowFilter = " Emp_Code = '" & Chk(GrdEmployee.Items(i).Cells(2).Text) & "'"
                        If DView.Count <> 0 Then
                            If GrdEmployee.Items(i).Cells(2).Text = DView.Item(0).Item("Emp_Code") Then
                                CType(GrdEmployee.Items(i).Cells(0).Controls(0), LinkButton).ForeColor = Color.Brown
                                CType(GrdEmployee.Items(i).Cells(0).Controls(0), LinkButton).Text = "Selected"
                            End If
                        End If
                    Next

                Case "RdoSelected"

                    Viewstate("DtTemp") = New DataTable
                    StrQry = " Select EMP_NAME as [NAME], HM.Emp_Code From HrdMastQry HM " & _
                             " inner join TRAINEMP TE On HM.Emp_Code = TE.Emp_Code Where TE.Train_Code = '" & ViewState("SelTrainCalCode") & "'"

                    Session("DalObj").GetSqlDataTable(Viewstate("DtTemp"), StrQry)
                    GrdEmployee.CurrentPageIndex = 0
                    GrdEmployee.DataSource = Viewstate("DtTemp")
                    GrdEmployee.DataBind()

                    For i = 0 To GrdEmployee.Items.Count - 1
                        CType(GrdEmployee.Items(i).Cells(0).Controls(0), LinkButton).ForeColor = Color.Brown
                        CType(GrdEmployee.Items(i).Cells(0).Controls(0), LinkButton).Text = "Selected"
                    Next

                Case "RdoSuggested"

                    DtTemp = New DataTable
                    StrQry = " Select EMP_NAME as [NAME], HM.Emp_Code from HrdMastQry HM  " & _
                             " inner join EmpTrainNeeds ETN On HM.Emp_Code = ETN.Emp_Code inner Join TrainSkills TS On ETN.SkillCode = TS.Skill_Code " & _
                             " Where TS.Train_Code = '" & ViewState("SelTrainCode") & "'  and TS.Skill_Rate_To > ETN.Skill_Rate Group by HM.Emp_Code, HM.EMP_NAME"

                    Session("DalObj").GetSqlDataTable(DtTemp, StrQry)
                    GrdEmployee.CurrentPageIndex = 0
                    GrdEmployee.DataSource = DtTemp
                    GrdEmployee.DataBind()

                    If IsNothing(Viewstate("DtTemp")) Or Viewstate("DtTemp").Rows.Count = 0 Then Exit Sub
                    Dim DView As New DataView(Viewstate("DtTemp"))

                    For i = 0 To GrdEmployee.Items.Count - 1
                        DView.RowFilter = " Emp_Code = '" & Chk(GrdEmployee.Items(i).Cells(2).Text) & "'"
                        If DView.Count <> 0 Then
                            If GrdEmployee.Items(i).Cells(2).Text = DView.Item(0).Item("Emp_Code") Then
                                CType(GrdEmployee.Items(i).Cells(0).Controls(0), LinkButton).ForeColor = Color.Brown
                                CType(GrdEmployee.Items(i).Cells(0).Controls(0), LinkButton).Text = "Selected"
                            End If
                        End If
                    Next

                Case "RdoRequested"

                    DtTemp = New DataTable
                    StrQry = " Select EMP_NAME as [NAME], HM.Emp_Code From HrdMastQry HM " & _
                             " inner join TrainRequest TR On HM.Emp_Code = TR.Emp_Code Where Train_Code ='" & ViewState("SelTrainCalCode") & "' Order By HM.Emp_Code "

                    Session("DalObj").GetSqlDataTable(DtTemp, StrQry)
                    GrdEmployee.CurrentPageIndex = 0
                    GrdEmployee.DataSource = DtTemp
                    GrdEmployee.DataBind()

                    If IsNothing(Viewstate("DtTemp")) Or Viewstate("DtTemp").Rows.Count = 0 Then Exit Sub
                    Dim DView As New DataView(Viewstate("DtTemp"))

                    For i = 0 To GrdEmployee.Items.Count - 1
                        DView.RowFilter = " Emp_Code = '" & Chk(GrdEmployee.Items(i).Cells(2).Text) & "'"
                        If DView.Count <> 0 Then
                            If GrdEmployee.Items(i).Cells(2).Text = DView.Item(0).Item("Emp_Code") Then
                                CType(GrdEmployee.Items(i).Cells(0).Controls(0), LinkButton).ForeColor = Color.Brown
                                CType(GrdEmployee.Items(i).Cells(0).Controls(0), LinkButton).Text = "Selected"
                            End If
                        End If
                    Next

            End Select
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message)
        End Try
    End Sub

    Private Sub GrdEmployee_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles GrdEmployee.PageIndexChanged
        Try
            If (GrdEmployee.CurrentPageIndex > GrdEmployee.PageCount Or GrdEmployee.CurrentPageIndex < 0) Then
                GrdEmployee.CurrentPageIndex = 0
            Else
                GrdEmployee.CurrentPageIndex = e.NewPageIndex
            End If
            FillEmployeeGrid()
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message)
        End Try
    End Sub

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub

End Class
