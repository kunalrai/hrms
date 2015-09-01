Partial Class TrainEmpFeedback
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
            FillCombo()
            FillTrainEvalGrid()
        End If
    End Sub

    Sub FillCombo()
        Session("BalObj").FillCombo(cmbCode, "Select TrainCalCode, TrainCalCode From TrainCalendar", True)
    End Sub

    Sub FillTrainEvalGrid()
        Try
            Dim StrQry As String, DtFeedback As New DataTable

            StrQry = " Select TrainFPId,TrainFPDesc From TrainFeedbackPoints Where isnull(IsActive,0)=1 Order By TrainFPId"
            'isnull(PointsFor,'') = 'E' and 
            CType(Session("DalObj"), DAL.DataLayer).GetSqlDataTable(DtFeedback, StrQry)

            GrdFeedback.DataSource = DtFeedback
            GrdFeedback.DataBind()
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message)
        End Try
    End Sub
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            If Not IsValidate() Then Exit Sub
            Dim StrQry As String, i As Int16

            StrQry = " Delete From TRAINFEEDBACK Where EMP_CODE = '" & Session("LoginUser").UserId & "' and TrainCalId='" & cmbCode.SelectedValue & "'"
            CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(StrQry)

            For i = 0 To GrdFeedback.Items.Count - 1
                StrQry = " Insert TRAINFEEDBACK(TrainCalId,EMP_CODE,TrainFPId,StDisagree,Disagree,SmAgee,Agree,StAgree) Values('" & cmbCode.SelectedValue & "', '" & Session("LoginUser").UserId & "'" & _
                         ", '" & ChkN(GrdFeedback.Items(i).Cells(0).Text) & "', '" & _
                           IIf(CType(GrdFeedback.Items(i).Cells(2).Controls(1), HtmlInputCheckBox).Checked = True, "1", "0") & "', '" & _
                           IIf(CType(GrdFeedback.Items(i).Cells(3).Controls(1), HtmlInputCheckBox).Checked = True, "1", "0") & "', '" & _
                           IIf(CType(GrdFeedback.Items(i).Cells(4).Controls(1), HtmlInputCheckBox).Checked = True, "1", "0") & "', '" & _
                           IIf(CType(GrdFeedback.Items(i).Cells(5).Controls(1), HtmlInputCheckBox).Checked = True, "1", "0") & "', '" & _
                           IIf(CType(GrdFeedback.Items(i).Cells(6).Controls(1), HtmlInputCheckBox).Checked = True, "1", "0") & "')"

                CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(StrQry)
            Next

            SetMsg(LblErrMsg, " Records Saved Successfully.")

        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message)
        End Try
    End Sub

    Function IsValidate() As Boolean
        Try
            If cmbCode.SelectedItem.Text = "" Then
                SetMsg(LblErrMsg, " Please Select Training Session fro the list.")
                Return False
            End If
            Return True
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message)
        End Try
    End Function

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub
    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub
End Class
