Partial Class LoanRecovery
    Inherits System.Web.UI.Page
    Dim Dt As New DataTable
    Dim Ds As New DataSet
    Dim DAL As DAL.DataLayer
    Dim BAL As BAL.BLayer
    Protected WithEvents TotalRec As System.Web.UI.WebControls.Label
    Dim VarComp_Name As String
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
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If
        DAL = Session("DALObj")
        BAL = Session("BALObj")
        'Put user code to initialize the page here
        LblErrMsg.Text = ""
        If Not IsPostBack Then
            BAL.FillCombo(CmbAcHead, "SELECT Field_Name, Field_Desc FROM PaySetup WHERE Fld_Categ=3 ORDER BY Field_Desc", True)
            RefreshGrid()
        End If
        'CmdDelete.Attributes.Add("onclick", "return ConfirmDelete();")
    End Sub
    Private Function RefreshGrid()
        GrdLoanStatus.Visible = False
        Try
            Dim rsLaMast As New DataTable
            Dim StrQuery1 As String

            If ChkZero.Checked = True Then

                StrQuery1 = " Select LaMast.Emp_Code,HrdMastQry.Emp_Name,LaMast.L_Code,(datename(d,LaMast.L_Date)+'/'+ left(datename(MM,LaMast.L_Date),3)+'/'+ datename(yyyy,LaMast.L_Date)) as L_Date, " & _
                            " LaMast.I_AMT,LaMast.I_INST_AMT,LaMast.I_INST_NO,LaMast.I_INT,LaMast.I_RATE,LaMast.I_REC,(datename(d,LaMast.I_SDATE)+'/'+ left(datename(MM,LaMast.I_SDATE),3)+'/'+ datename(yyyy,LaMast.I_SDATE)) as I_SDATE, " & _
                            " LaMast.L_AMT,LaMast.L_INST_AMT,LaMast.L_INST_NO,(datename(d,LaMast.L_RDATE)+'/'+ left(datename(MM,LaMast.L_RDATE),3)+'/'+ datename(yyyy,LaMast.L_RDATE)) as L_RDATE," & _
                            " LaMast.L_REC,(datename(d,LaMast.L_SDATE)+'/'+ left(datename(MM,LaMast.L_SDATE),3)+'/'+ datename(yyyy,LaMast.L_SDATE)) as L_SDATE," & _
                            " LaMast.LS_FDT, LaMast.LS_TDT, LaMast.INST_TYPE,LaMast.L_Rec,LaMast.I_Rec, LaMast.L_REFNO " & _
                            " From LaMast LaMast INNER JOIN HrdMastQry HrdMastQry " & _
                            " ON LaMast.Emp_Code=HrdMastQry.Emp_Code Where L_Code='" & CmbAcHead.SelectedValue & "' ORDER BY LaMast.Emp_Code"
            Else

                StrQuery1 = " Select LaMast.Emp_Code,HrdMastQry.Emp_Name,LaMast.L_Code,(datename(d,LaMast.L_Date)+'/'+ left(datename(MM,LaMast.L_Date),3)+'/'+ datename(yyyy,LaMast.L_Date)) as L_Date, " & _
                            " LaMast.I_AMT,LaMast.I_INST_AMT,LaMast.I_INST_NO,LaMast.I_INT,LaMast.I_RATE,LaMast.I_REC,(datename(d,LaMast.I_SDATE)+'/'+ left(datename(MM,LaMast.I_SDATE),3)+'/'+ datename(yyyy,LaMast.I_SDATE)) as I_SDATE, " & _
                            " LaMast.L_AMT,LaMast.L_INST_AMT,LaMast.L_INST_NO,LaMast.L_Rec,LaMast.I_Rec,(datename(d,LaMast.L_RDATE)+'/'+ left(datename(MM,LaMast.L_RDATE),3)+'/'+ datename(yyyy,LaMast.L_RDATE)) as L_RDATE," & _
                            " LaMast.L_REC,(datename(d,LaMast.L_SDATE)+'/'+ left(datename(MM,LaMast.L_SDATE),3)+'/'+ datename(yyyy,LaMast.L_SDATE)) as L_SDATE," & _
                            " LaMast.LS_FDT,LaMast.LS_TDT, LaMast.INST_TYPE, LaMast.L_REFNO " & _
                            " From LaMast LaMast INNER JOIN HrdMastQry HrdMastQry " & _
                            " ON LaMast.Emp_Code=HrdMastQry.Emp_Code Where L_Code='" & CmbAcHead.SelectedValue & "' AND L_Amt+I_Amt>L_Rec+I_Rec"
            End If

            DAL.GetSqlDataTable(rsLaMast, StrQuery1)
            GrdLoanStatus.DataSource = rsLaMast
            GrdLoanStatus.DataBind()
            LblErrMsg.Font.Bold = True
            GrdLoanStatus.Visible = True
            If rsLaMast.Rows.Count > 0 Then
                LblErrMsg.Text = rsLaMast.Rows.Count & "  Match(s) Found "
            Else
                LblErrMsg.Text = rsLaMast.Rows.Count & "   Match(s) Found "
            End If
            Dim i, j, k As Integer
            For j = 0 To GrdLoanStatus.Items.Count - 1
                Dim Temp1, Temp2 As String
                'comment by Ravi
                'by Ravi on 5 dec 2006
                'Temp1 = GrdLoanStatus.Items(j).Cells(3).Text - GrdLoanStatus.Items(j).Cells(7).Text
                Temp1 = ChkN(GrdLoanStatus.Items(j).Cells(3).Text) - ChkN(GrdLoanStatus.Items(j).Cells(7).Text)
                GrdLoanStatus.Items(j).Cells(6).Text = Temp1
                'comment by Ravi
                ' Temp2 = GrdLoanStatus.Items(j).Cells(9).Text - ChkN(GrdLoanStatus.Items(j).Cells(13).Text
                'by Ravi on 5 dec 2006
                Temp2 = ChkN(GrdLoanStatus.Items(j).Cells(9).Text) - ChkN(GrdLoanStatus.Items(j).Cells(13).Text)
                If Temp2 = 0 Then
                    GrdLoanStatus.Items(j).Cells(13).Text = 0
                Else
                    GrdLoanStatus.Items(j).Cells(13).Text = Temp2
                End If
            Next
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (Refresh Grid)")
        End Try
    End Function
    Private Sub CmbAcHead_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbAcHead.SelectedIndexChanged
        RefreshGrid()
    End Sub
    Private Sub ChkZero_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkZero.CheckedChanged
        RefreshGrid()
    End Sub
    Private Sub GrdLoanStatus_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles GrdLoanStatus.ItemCommand
        Dim Tmp, srno As String
        Dim Temp As String
        Temp = e.CommandSource.Text & "," & e.Item.Cells(2).Text & "," & e.Item.Cells(14).Text & "," & e.Item.Cells(8).Text
        Tmp = " <SCRIPT language=javascript >window.open ('RecoveryDetails.aspx?keywords=" & Temp & "' , '')</SCRIPT>"
        RegisterStartupScript("Rajeev", Tmp)
    End Sub
End Class
