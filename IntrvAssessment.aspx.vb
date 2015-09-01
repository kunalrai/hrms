Partial Class IntrvAssessment
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

    Dim DtTemp As DataTable

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If
        If Not IsPostBack Then
            FillCombo()
        End If
    End Sub

    Private Sub FillCombo()
        Try
            Session("BalObj").FillCombo(cmbIntrvrefid, "Select IntrvID, IntrvNo from IntrvShedule ", True)
            Session("BalObj").FillCombo(cmbResStatus, "Select Status_Code, Status_Name from StatusMast ", True)
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (FillCombo)")
        End Try
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try

            If Not IsValidate() Then Exit Sub

            Dim StrQry, TempStr, TempValue As String, i, j As Int16, DtEvalPoint As New DataTable, DtTempType As New DataTable

            StrQry = " Delete From IntervDetail Where IntrvId ='" & cmbIntrvrefid.SelectedValue & "' and  Res_Code='" & cmbResumes.SelectedValue & "' "

            Dim TempTable As Table
            TempTable = Session("TblIntrAss")
            CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(StrQry)

            CType(Session("DalObj"), DAL.DataLayer).GetSqlDataTable(DtEvalPoint, " Select * From EvalPointMast Where Isnull(Active,0)=1 Order By EvalPointRank")

            StrQry = " Select IC.IntrvId, Qry.TTMId, Qry.TTMDesc, V.Vacancy_Code From Vacancy V " & _
                     " Inner Join IntrvCandidates IC On IC.Vacancy_Code = V.Vacancy_Code " & _
                     " inner Join( " & _
                     " Select RTM.TempId, RTD.TTMId, TTM.TTMDesc From RecTempMast RTM " & _
                     " Left Join RecTempDetail RTD On RTM.TempId = RTD.TempId " & _
                     " Left Join TempTypeMast TTM On TTM.TTMId = RTD.TTMId) as Qry On V.TempId = Qry.TempId " & _
                     " WHERE IC.IntrvId= '" & cmbIntrvrefid.SelectedValue & "'" & _
                     " Group By IC.IntrvId, Qry.TTMId, Qry.TTMDesc, V.Vacancy_Code "

            CType(Session("DalObj"), DAL.DataLayer).GetSqlDataTable(DtTempType, StrQry)

            For i = 0 To DtTempType.Rows.Count - 1
                StrQry = " Insert IntervDetail(IntrvId,Vacancy_Code,Res_Code,TTMId"
                TempStr = ""
                TempValue = ""
                For j = 0 To DtEvalPoint.Rows.Count - 1
                    TempStr = TempStr & "," & "EvalPoint" & j + 1
                    TempValue = TempValue & "'" & Request.Form.Item("txt" & DtTempType.Rows(i).Item("TTMId") & "|" & DtEvalPoint.Rows(j).Item("EvalPointId")) & "',"
                Next
                TempValue = Mid(TempValue, 1, Len(TempValue) - 1)
                StrQry = StrQry & TempStr & ") Values ('" & cmbIntrvrefid.SelectedValue & "','" & ViewState("VCode") & "', '" & _
                         cmbResumes.SelectedValue & "', '" & DtTempType.Rows(i).Item("TTMId") & "', " & TempValue & ")"

                CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(StrQry)
            Next

            SetMsg(LblErrMsg, "Record Saved Successfully")
            cmbIntrvrefid.SelectedValue = ""
            cmbReqNo.SelectedValue = ""
            cmbResStatus.SelectedValue = ""
            cmbResumes.SelectedValue = ""
            Session("TblIntrAss") = Nothing
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (Save Records)")
        End Try
    End Sub

    Function IsValidate() As Boolean
        Try
            If cmbIntrvrefid.SelectedItem.Text = "" Then
                SetMsg(LblErrMsg, "Please select Interview Ref. Id from the list.")
                Return False
            End If

            If cmbResumes.SelectedItem.Text = "" Then
                SetMsg(LblErrMsg, "Please select candidate for assessment from the list.")
                Return False
            End If

            Return True
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (IsValidate)")
        End Try
    End Function

    Private Sub cmbIntrvrefid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbIntrvrefid.SelectedIndexChanged
        Try
            LblErrMsg.Text = ""
            Dim VacancyCodes As String, Dt As New DataTable, i As Int16
            Session("DalObj").GetSqlDataTable(Dt, " Select Distinct Vacancy_Code From IntrvCandidates Where IntrvID='" & cmbIntrvrefid.SelectedValue & "'")

            For i = 0 To Dt.Rows.Count - 1
                VacancyCodes = VacancyCodes & Dt.Rows(i).Item(0) & ", "
            Next
            VacancyCodes = Mid(VacancyCodes, 1, Len(VacancyCodes.Trim) - 1)
            Session("BalObj").FillCombo(cmbReqNo, "Select Vacancy_Code, Vacancy_RefNo from Vacancy Where Vacancy_Code In (" & VacancyCodes & ")", True)

            ViewState("TTMId") = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(" Select TTMId From IntrvShedule Where IntrvId= '" & cmbIntrvrefid.SelectedValue & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
            Session("BalObj").FillCombo(cmbResumes, "Select IC.Res_Code, (isnull(RM.Res_NameF,'')+' '+isnull(RM.Res_NameM,'')+' '+isnull(RM.Res_NameL,'')) as ResName From IntrvCandidates IC Inner Join ResMast RM On IC.Res_Code = RM.Res_Code Where IC.IntrvId = '" & cmbIntrvrefid.SelectedValue & "'", True)

        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : cmbIntrvrefid_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub cmbReqNo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbReqNo.SelectedIndexChanged
        Try
            LblErrMsg.Text = ""
            Session("BalObj").FillCombo(cmbResumes, "Select IC.Res_Code, (isnull(RM.Res_NameF,'')+' '+isnull(RM.Res_NameM,'')+' '+isnull(RM.Res_NameL,'')) as ResName From IntrvCandidates IC Inner Join ResMast RM On IC.Res_Code = RM.Res_Code Where IC.IntrvId = '" & cmbIntrvrefid.SelectedValue & "' and IC.Vacancy_Code='" & cmbReqNo.SelectedValue & "'", True)
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : cmbReqNo_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub cmbResumes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbResumes.SelectedIndexChanged
        Try
            LblErrMsg.Text = ""

            Dim StrQry As String, Count, i, j As Int16, DtEvalPoint As New DataTable, DtTempType As New DataTable
            Dim TempCell As TableCell, TxtBox As TextBox

            ViewState("VCode") = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(" Select Vacancy_Code From Resmast Where Res_Code= '" & cmbResumes.SelectedValue & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)

            cmbResStatus.SelectedValue = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(" Select Status_Code From ResMast Where Res_Code= '" & cmbResumes.SelectedValue & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)

            CType(Session("DalObj"), DAL.DataLayer).GetSqlDataTable(DtEvalPoint, " Select * From EvalPointMast Where Isnull(Active,0)=1 Order By EvalPointRank")

            StrQry = " Select IC.IntrvId, Qry.TTMId, Qry.TTMDesc, V.Vacancy_Code From Vacancy V " & _
                     " Inner Join IntrvCandidates IC On IC.Vacancy_Code = V.Vacancy_Code " & _
                     " inner Join( " & _
                     " Select RTM.TempId, RTD.TTMId, TTM.TTMDesc From RecTempMast RTM " & _
                     " Left Join RecTempDetail RTD On RTM.TempId = RTD.TempId " & _
                     " Left Join TempTypeMast TTM On TTM.TTMId = RTD.TTMId) as Qry On V.TempId = Qry.TempId " & _
                     " WHERE IC.IntrvId= '" & cmbIntrvrefid.SelectedValue & "' " & _
                     " Group By IC.IntrvId, Qry.TTMId, Qry.TTMDesc, V.Vacancy_Code "

            CType(Session("DalObj"), DAL.DataLayer).GetSqlDataTable(DtTempType, StrQry)

            If DtTempType.Rows.Count = 0 Then
                TblIntrAss.Rows.Clear()
                Exit Sub
            End If

            For i = 0 To DtTempType.Rows.Count - 1
                Dim Table1 As New Table
                Dim Table1Row1 As New TableRow
                Dim Table1Row11 As New TableRow
                Dim Table1Row2 As New TableRow

                Dim RowU As New TableRow
                Dim CellU As New TableCell

                Dim Row As New TableRow
                Dim Cell As New TableCell

                Dim RowL As New TableRow
                Dim CellL As New TableCell

                For j = 0 To DtEvalPoint.Rows.Count - 1
                    TempCell = New TableCell
                    TempCell.Width = Unit.Percentage(15)
                    TempCell.Text = DtEvalPoint.Rows(j).Item("EvalPointDesc")
                    Table1Row1.Cells.Add(TempCell)
                Next
                If DtTempType.Rows(i).Item("TTMId") = ViewState("TTMId") Then
                    Table1Row1.BackColor = Color.Gray
                    Table1Row1.Style.Item("color") = "white"
                    Table1Row1.Style.Item("FONT-WEIGHT") = "bold"
                Else
                    Table1Row1.BackColor = Color.LightGray
                    Table1Row1.Style.Item("color") = "black"
                End If


                For j = 0 To DtEvalPoint.Rows.Count - 1
                    TempCell = New TableCell
                    TxtBox = New TextBox
                    TxtBox.ID = "txt" & DtTempType.Rows(i).Item("TTMId") & "|" & DtEvalPoint.Rows(j).Item("EvalPointId")
                    TxtBox.Attributes.Add("runat", "server")
                    TxtBox.TextMode = TextBoxMode.MultiLine
                    TxtBox.Rows = 2
                    TxtBox.Width = Unit.Percentage(100)
                    If DtTempType.Rows(i).Item("TTMId") = ViewState("TTMId") Then
                        TxtBox.Enabled = True
                    Else
                        TxtBox.Enabled = False
                    End If

                    TempCell.Width = Unit.Percentage(15)
                    TempCell.Controls.Add(TxtBox)
                    Table1Row2.Cells.Add(TempCell)
                Next

                Table1.Attributes.Add("Border", "1")
                Table1.Width = Unit.Percentage(100)

                'Dim TableCell11 As New TableCell
                'TableCell11.Text = "Evaluate on five point scale 5-maximum 1-minimum"
                'Table1Row11.Cells.Add(TableCell11)

                Table1.Rows.Add(Table1Row1)
                'Table1.Rows.Add(Table1Row11)
                Table1.Rows.Add(Table1Row2)

                CellU.Text = DtTempType.Rows(i).Item("TTMDesc")
                If DtTempType.Rows(i).Item("TTMId") = ViewState("TTMId") Then
                    CellU.Style.Item("FONT-WEIGHT") = "bold"
                    Table1.BackColor = Color.LightGray
                    'Else
                    '  Table1.BackColor = Color.LightGray
                End If
                RowU.Cells.Add(CellU)

                Cell.Controls.Add(Table1)
                Row.Cells.Add(Cell)

                CellL.Text = ""
                RowL.Cells.Add(CellL)

                RowU.ID = "rowU" & i
                RowU.Attributes.Add("runat", "server")

                Row.ID = "row" & i
                Row.Attributes.Add("runat", "server")

                RowL.ID = "rowL" & i
                RowL.Attributes.Add("runat", "server")

                TblIntrAss.Rows.Add(RowU)
                TblIntrAss.Rows.Add(Row)
                TblIntrAss.Rows.Add(RowL)
            Next

            DtTemp = New DataTable
            CType(Session("DalObj"), DAL.DataLayer).GetSqlDataTable(DtTemp, " Select * From IntervDetail Where Vacancy_Code='" & ViewState("VCode") & "' and Res_Code='" & cmbResumes.SelectedValue & "'")

            If DtTemp.Rows.Count <> 0 Then
                For i = 0 To DtTemp.Rows.Count - 1
                    For j = 0 To DtEvalPoint.Rows.Count - 1
                        CType(TblIntrAss.FindControl("txt" & DtTemp.Rows(i).Item("TTMId") & "|" & DtEvalPoint.Rows(j).Item("EvalPointId")), TextBox).Text = Chk(DtTemp.Rows(i).Item("EvalPoint" & j + 1))
                        'CType(TblIntrAss.FindControl("txt" & DtTemp.Rows(i).Item("TTMId") & "|" & DtEvalPoint.Rows(j).Item("EvalPointId")), TextBox).Enabled = True
                    Next
                Next
            End If

            Session("TblIntrAss") = TblIntrAss
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : cmbResumes_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub

End Class
