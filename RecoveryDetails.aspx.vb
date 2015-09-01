Partial Class RecoveryDetails
    Inherits System.Web.UI.Page
    Dim Dt As New DataTable
    Dim ds As New DataSet
    Dim DAL As DAL.DataLayer
    Dim BAL As BAL.BLayer
    Dim arr(2) As String
    Dim Temp As String
    Dim Temp1, Tem2, Temp3 As String
#Region " Inhereted Controls "

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
        DAL = Session("DALObj")
        BAL = Session("BALObj")
        If Not IsPostBack Then
            TblReco.Visible = False
            Temp = Request.QueryString("keywords")
            ViewState("Temp") = Temp
            arr = Split(ViewState("Temp"), ",")
            ViewState("Emp_Code") = arr(0)
            ViewState("L_Date") = arr(1)
            ViewState("L_Code") = arr(2)
            ViewState("L_RDate") = arr(3)
            BAL.FillCombo(CmbEmployee, "Emp_Code", "Emp_Name", "HrdMastQry", True)
            BAL.FillCombo(CmbLoanType, "SELECT Field_Name, Field_Desc FROM PaySetup WHERE Fld_Categ=3 ORDER BY Field_Desc", True)
            FillDataGrid()
        End If
        CmdClose.Attributes.Add("onclick", "return CloseWindow();")
        CmdClos.Attributes.Add("onclick", "return CloseWindow();")
        CmdDelete.Attributes.Add("onclick", "return ConfirmDelete();")
    End Sub
    Private Function FillDataGrid()
        Try
            Dim StrQuery As String
            Dim rsMast As New DataTable
            Dim rsTran As New DataTable
            StrQuery = "Select Int,L_Rec,(datename(d,L_RDate)+'/'+ left(datename(MM,L_RDate),3)+'/'+ datename(yyyy,L_RDate)) as L_RDate,PRIN from LaTran Where Emp_Code='" & ViewState("Emp_Code") & "' And (datename(d,L_Date)+'/'+ left(datename(MM,L_Date),3)+'/'+ datename(yyyy,L_Date))='" & ViewState("L_Date") & "' And L_Code='" & ViewState("L_Code") & "' ORDER BY L_RDate"
            DAL.GetSqlDataTable(rsTran, StrQuery)
            If rsTran.Rows.Count > 0 Then
                GrdRecoveryDetail.DataSource = rsTran
                GrdRecoveryDetail.DataBind()
                TotalPage.Text = "Total Recoverie(s):-> " & rsTran.Rows.Count
            End If
        Catch ex As Exception
            LblErrMsg.Text = ex.Message & FillDataGrid()
        End Try
    End Function
    Private Sub GrdRecoveryDetail_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles GrdRecoveryDetail.PageIndexChanged
        GrdRecoveryDetail.CurrentPageIndex = e.NewPageIndex
        FillDataGrid()
    End Sub
    Private Sub CmdCl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdCl.Click
        TblReco.Visible = False
    End Sub
    Private Sub CmdNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdNew.Click
        ViewState("Action") = "AddNew"
        TblReco.Visible = True
        ShowRecord()
    End Sub
    Private Sub CmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdCancel.Click
        ClearAll(Me)
    End Sub
    Private Function ShowRecord()
        Try
            Dim LaMast As New DataTable
            Dim LaTran As New DataTable
            Dim StrQuery1, StrQuery2 As String
            StrQuery1 = "Select LaMast.* from LaMast Where Emp_Code='" & ViewState("Emp_Code") & "' And L_Code='" & ViewState("L_Code") & "' And (datename(d,L_Date)+'/'+ left(datename(MM,L_Date),3)+'/'+ datename(yyyy,L_Date))='" & ViewState("L_Date") & "'"
            DAL.GetSqlDataTable(LaMast, StrQuery1)
            If LaMast.Rows.Count > 0 Then
                CmbEmployee.SelectedItem.Text = LaMast.Rows(0).Item("Emp_Code")
                CmbLoanType.SelectedValue = LaMast.Rows(0).Item("L_Code")
                TxtPrincipalBal.Text = ChkN(LaMast.Rows(0).Item("L_Amt")) - ChkN(LaMast.Rows(0).Item("L_Rec"))
                TxtIntBal.Text = ChkN(LaMast.Rows(0).Item("I_Amt")) - ChkN(LaMast.Rows(0).Item("I_Rec"))
                DtpSectionDate.Text = Format(CDate(LaMast.Rows(0).Item("L_Date")), "dd/MMM/yyyy")
                DtpRecoveryDate.Text = Format(Date.Today, "dd/MMM/yyyy")
            End If
            StrQuery2 = "Select LaTran.* From LaTran Where Emp_Code='" & CmbEmployee.SelectedValue & "' And L_Code='" & CmbEmployee.SelectedValue & "' and (datename(d,L_Date)+'/'+ left(datename(MM,L_Date),3)+'/'+ datename(yyyy,L_Date))='" & arr(1) & "'"
            DAL.GetSqlDataTable(LaTran, StrQuery2)
            If LaTran.Rows.Count > 0 Then
                TxtPrincipalReco.Text = LaTran.Rows(0).Item("Prin")
                TxtIntReco.Text = LaTran.Rows(0).Item("Int")
            Else
                TxtPrincipalReco.Text = 0
                TxtIntReco.Text = 0
            End If
        Catch ex As Exception
            LblErrMsg1.Text = ex.Message & "Show Records"
        End Try
    End Function
    Private Function DeleteRecord()
        Try
            Dim StrQuery1 As String
            Dim StrQuery2 As String
            StrQuery2 = "Delete From LaMast Where Emp_Code='" & ViewState("Emp_Code") & "' And L_Code='" & ViewState("L_Code") & "' And (datename(d,L_Date)+'/'+ left(datename(MM,L_Date),3)+'/'+ datename(yyyy,L_Date))='" & ViewState("L_Date") & "'  "
            DAL.ExecuteCommand(StrQuery2)
            StrQuery1 = "Delete from LaTran Where Emp_Code='" & ViewState("Emp_Code") & "' And (datename(d,L_Date)+'/'+ left(datename(MM,L_Date),3)+'/'+ datename(yyyy,L_Date))='" & ViewState("L_Date") & "' And L_Code='" & ViewState("L_Code") & "' And L_RDate='" & ViewState("L_RDate") & "'"
            DAL.ExecuteCommand(StrQuery1)
        Catch ex As Exception
            LblErrMsg1.Text = ex.Message & " Delete Record"
        End Try
    End Function
    Private Sub CmdDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdDelete.Click
        DeleteRecord()
    End Sub
    Private Function SaveData()
        Dim Tran As SqlClient.SqlTransaction
        Try
            Dim StrQuery As String
            Tran = DAL.StartTransaction("Save")
            If ViewState("Action") = "AddNew" Then
                'If Not IsValidate() Then Exit Function
                StrQuery = "insert into LaTran(Emp_Code,L_Code,L_Date,L_RDate,PRIN,INT,L_REC,I_REC)"
                StrQuery &= " Values('"
                StrQuery &= ViewState("Emp_Code") & "','"
                StrQuery &= ViewState("L_Code") & "','"
                StrQuery &= Format(CDate(DtpSectionDate.Text), "dd/MMM/yyyy") & "','"
                StrQuery &= Format(CDate(DtpRecoveryDate.Text), "dd/MMM/yyyy") & "','"
                StrQuery &= ChkN(TxtPrincipalBal.Text) & "','"
                StrQuery &= ChkN(TxtIntBal.Text) & "','"
                StrQuery &= ChkN(TxtPrincipalReco.Text) & "','"
                StrQuery &= ChkN(TxtIntReco.Text) & "')"
                DAL.ExecuteCommand(StrQuery, Tran)
            End If
            If ViewState("Action") = "MODIFY" Then
                Dim StrUpdQuery As String
                Dim rsLaTran As New DataTable
                StrUpdQuery = " Update LaTran Set " & _
                               " L_RDate = '" & Chk(Format(CDate(DtpRecoveryDate.Text), "dd/MMM/yyyy")) & "', " & _
                               " Prin = '" & Chk(TxtPrincipalBal.Text) & "', " & _
                               " Int = '" & Chk(TxtIntBal.Text) & "', " & _
                               " L_Rec = '" & Chk(TxtPrincipalReco.Text) & "', " & _
                               " I_Rec = '" & Chk(TxtIntReco.Text) & "'" & _
                               " where Emp_Code='" & ViewState("Emp_Code") & "' And (datename(d,L_RDate)+'/'+ left(datename(MM,L_RDate),3)+'/'+ datename(yyyy,L_RDate))='" & DtpRecoveryDate.Text & "' And L_Code='" & ViewState("L_Code") & "'"
                DAL.ExecuteCommand(StrUpdQuery, Tran)
            End If
            Tran.Commit()
            LblErrMsg1.Text = "Records Saved Sucessfully"
            FillDataGrid()
        Catch ex As Exception
            SetMsg(LblErrMsg1, ex.Message & " : (SaveData())")
            Tran.Rollback()
        Finally
            If Not IsNothing(Tran) Then Tran.Dispose()
        End Try
    End Function
    Private Sub CmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdOk.Click
        SaveData()
    End Sub
#Region " Validation Function "
    Function IsValidate() As Boolean
        If Trim(TxtIntBal.Text) = "" Then
            SetMsg(LblErrMsg1, "Please Enter Intrest Balance.")
            Return False
        End If
        If Trim(TxtIntReco.Text) = "" Then
            SetMsg(LblErrMsg1, "Please Enter Intrest Recive Amount")
            Return False
        End If
        If Trim(TxtPrincipalBal.Text) Then
            SetMsg(LblErrMsg1, "Please Enter Principal Balance")
            Return False
        End If
        If Trim(TxtPrincipalReco.Text) = "" Then
            SetMsg(LblErrMsg1, "Please Enter Principal Recovery Amount")
            Return False
        End If
        If CmbEmployee.SelectedItem.Text = "" Then
            SetMsg(LblErrMsg1, "Please Select Employee Code")
            Return False
        End If
        If CmbLoanType.SelectedItem.Text = "" Then
            SetMsg(LblErrMsg1, "Please Select Loan Type")
            Return False
        End If
        Return True
    End Function
#End Region
    Private Sub GrdRecoveryDetail_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles GrdRecoveryDetail.ItemCommand
        If e.CommandName = "Select" Then
            Try
                Dim L_RDate, Prin, Int As String
                Dim rsLaTran As New DataTable
                If GrdRecoveryDetail.Items.Count > 0 Then
                    L_RDate = e.Item.Cells(1).Text
                    Prin = e.Item.Cells(2).Text
                    Int = e.Item.Cells(3).Text
                    Dim LaMast As New DataTable
                    Dim LaTran As New DataTable
                    Dim StrQuery1, StrQuery2 As String

                    StrQuery1 = "Select * from LaTran Where L_RDate='" & L_RDate & "' And L_Rec='" & Prin & "' And Int='" & Int & "' And Emp_Code='" & ViewState("Emp_Code") & "'"
                    DAL.GetSqlDataTable(rsLaTran, StrQuery1)

                    StrQuery2 = "Select L_Amt from LaMast Where Emp_Code='" & ViewState("Emp_Code") & "' And (datename(d,L_Date)+'/'+ left(datename(MM,L_Date),3)+'/'+ datename(yyyy,L_Date))='" & ViewState("L_Date") & "' And L_Code='" & ViewState("L_Code") & "'"
                    DAL.GetSqlDataTable(LaMast, StrQuery2)
                    If LaMast.Rows.Count > 0 Then
                        TxtPrincipalBal.Text = LaMast.Rows(0).Item("L_Amt")
                        If rsLaTran.Rows.Count > 0 Then
                            CmbEmployee.SelectedItem.Text = rsLaTran.Rows(0).Item("Emp_Code")
                            CmbLoanType.SelectedItem.Text = rsLaTran.Rows(0).Item("L_Code")
                            DtpSectionDate.Text = Format(CDate(rsLaTran.Rows(0).Item("L_Date")), "dd/MMM/yyyy")
                            DtpRecoveryDate.Text = Format(CDate(rsLaTran.Rows(0).Item("L_RDate")), "dd/MMM/yyyy")
                            TxtPrincipalReco.Text = rsLaTran.Rows(0).Item("L_Rec")
                            TxtIntBal.Text = rsLaTran.Rows(0).Item("Int")
                            TxtIntReco.Text = rsLaTran.Rows(0).Item("I_Rec")
                            ViewState("Action") = "MODIFY"
                            TblReco.Visible = True
                            TxtPrincipalBal.ReadOnly = False
                        End If
                    End If
                End If
            Catch ex As Exception
                SetMsg(LblErrMsg, "No Record Found" & "GrdRecoveryDetail_ItemCommand")
            End Try
        End If
    End Sub

    'Private Sub GrdRecoveryDetail_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles GrdRecoveryDetail.DeleteCommand
    '    Try
    '        If GrdRecoveryDetail.Items.Count > 0 Then
    '            Dim L_RDate, Prin, Int As String
    '            L_RDate = e.Item.Cells(1).Text
    '            Prin = e.Item.Cells(2).Text
    '            Int = e.Item.Cells(3).Text
    '            Dim DelQuery As String
    '            Dim rsLaTran1 As New DataTable
    '            DelQuery = "Delete from LaTran Where L_RDate='" & L_RDate & "' And L_Rec='" & Prin & "' And Int='" & Int & "' And Emp_Code='" & ViewState("Emp_Code") & "'"
    '            DAL.ExecuteCommand(DelQuery)
    '            LblErrMsg.Text = "Record Deleted."
    '            FillDataGrid()
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub
End Class
