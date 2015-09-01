Partial Class LeavMast
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
        Try


            'If CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules <> "" Then
            '    If InStr(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, SrNo & "-") <> 0 Then
            '        Dim int As Int16, st As String
            '        int = InStr(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, SrNo)
            '        st = Right(Mid(CType(Session("LoginUser"), DAL.DataLayer.Users).UserModules, int, Len(SrNo & "-") + 1), 1)

            '        If st <> "S" Then
            '            CmdSave.Visible = False
            '        End If
            '    Else
            '        Response.Redirect("Main.aspx")
            '    End If
            'End If

            'By Ravi 17 nov 2006
            Dim SrNo As Int16
            SrNo = Request.QueryString.Item("SrNo") + Request.QueryString.Item("MODULEID")
            Dim bSuccess As Boolean
            Select Case CheckRight(SrNo)
                Case MdlHRMS.AccessType.FullAccess, MdlHRMS.AccessType.SuperUser
                    bSuccess = True
                Case MdlHRMS.AccessType.ReadonlyAccess, MdlHRMS.AccessType.Restricted
                    bSuccess = False
            End Select
            CmdSave.Visible = bSuccess

            If Not IsPostBack Then
                Session("BalObj").fillcombo(cmbEmp, "Select Emp_Code, Emp_Name as FNAME from HrdMastQry where Ltype =1 " & Session("UserCodes") & " Order by FNAME", True)
                BindLeavGrid("dsd")
            End If
            CmdSave.Attributes.Add("onclick", "return ValidateCtrl();")
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : Page_Load")
        End Try
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub

#Region "    Bind Leave Grid   "

    Private Sub cmbEmp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbEmp.SelectedIndexChanged
        Try
            If cmbEmp.SelectedValue = "" Then Exit Sub
            TxtCode.Visible = True
            cmbEmp.Visible = False
            btnList.Visible = True
            TxtCode.Text = cmbEmp.SelectedValue
            LblName.Text = cmbEmp.SelectedItem.Text
            BindLeavGrid(Chk(cmbEmp.SelectedValue))
            lblMsg.Text = ""
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message)
        End Try
    End Sub
    Sub BindLeavGrid(ByVal Code As String, Optional ByVal Init As Boolean = False)
        Try
            Dim SQLStr As String
            ViewState("Leav") = New DataTable

            SQLStr = " SELECT LT.LVTYPE, LT.LVDESC, " & _
                     " (right(('00'+datename(d,LM.WEF)),2)+'/'+left(datename(m,LM.WEF),3)+'/'+datename(yy,LM.WEF)) as WEF," & _
                     " LM.Opening, isnull(LM.Earned,0) as Credited, isnull(LM.AVAILED,0) as Availed, isnull(LM.Tran_In,0) as TrfIn, " & _
                     " isnull(LM.Tran_Out,0) as TrfOut, " & _
                     " ((isnull(LM.Opening,0)+isnull(LM.Earned,0)+isnull(LM.Tran_In,0)) - (isnull(LM.AVAILED,0)+isnull(LM.Tran_Out,0))) as Balance " & _
                     " FROM LVTYPE LT LEFT JOIN (SELECT * FROM LEAVMAST WHERE EMP_CODE='" & Chk(Code) & "' AND LEVYEAR= '" & Session("LeavYear") & "') AS LM ON LT.LVTYPE = LM.LVTYPE"
            Session("DalObj").GetSqlDataTable(ViewState("Leav"), SQLStr)
            GrdLeavBal.DataSource = ViewState("Leav")
            GrdLeavBal.DataBind()
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : Bind Leave Grid ")
        End Try
    End Sub
#End Region
    Private Sub btnList_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnList.Click
        cmbEmp.Visible = True
        TxtCode.Visible = False
        btnList.Visible = False
    End Sub

    Private Sub TxtCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtCode.TextChanged
        ChkCombo(cmbEmp, Chk(TxtCode.Text))
        If cmbEmp.SelectedItem.Text <> "" Then
            LblName.Text = cmbEmp.SelectedItem.Text
        Else
            LblName.Text = ""
        End If
        BindLeavGrid(Chk(TxtCode.Text))
    End Sub

#Region "    Save Records   "

    Private Sub CmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdSave.Click
        Dim Tran As SqlClient.SqlTransaction
        Try
            Dim StrQuery As String, i As Int16

            If TxtCode.Text = "" Then SetMsg(lblMsg, "Code can not be left blank.") : Exit Sub

            If Not IsValidate() Then Exit Sub

            Tran = Session("DalObj").StartTransaction("Save")

            StrQuery = " Delete From LeavMast Where EMP_CODE = '" & Chk(TxtCode.Text) & "' and LEVYEAR = '" & Session("LeavYear") & "'"

            Session("DalObj").ExecuteCommand(StrQuery, Tran)

            For i = 0 To GrdLeavBal.Items.Count - 1

                If Chk(CType(GrdLeavBal.Items(i).Controls(2).Controls(1), TextBox).Text) <> "" Then

                    StrQuery = " Insert LeavMast ( LEVYEAR,EMP_CODE,LVTYPE,WEF,OPENING,EARNED,AVAILED,TRAN_IN,TRAN_OUT) Values ('" & _
                                        Session("LeavYear") & "', '" & _
                                        Chk(TxtCode.Text) & "', '" & _
                                        Chk(GrdLeavBal.Items(i).Cells(0).Text) & "', '" & _
                                        Chk(CType(GrdLeavBal.Items(i).Controls(2).Controls(1), TextBox).Text) & "', '" & _
                                        Chk(CType(GrdLeavBal.Items(i).Controls(3).Controls(1), TextBox).Text) & "', '" & _
                                        Chk(CType(GrdLeavBal.Items(i).Controls(4).Controls(1), TextBox).Text) & "', '" & _
                                        Chk(GrdLeavBal.Items(i).Cells(5).Text) & "', '" & _
                                        Chk(GrdLeavBal.Items(i).Cells(6).Text) & "', '" & _
                                        Chk(GrdLeavBal.Items(i).Cells(7).Text) & "' )"

                    Session("DalObj").ExecuteCommand(StrQuery, Tran)

                End If
            Next
            Tran.Commit()
            AfterSave()
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : Save Records ")
            If Not IsNothing(Tran) Then Tran.Rollback()
        Finally
            If Not IsNothing(Tran) Then Tran.Dispose()
        End Try
    End Sub


    Function IsValidate() As Boolean

        Dim Code As Int16

        'If Chk(TxtCode.Text) = "" Then
        '    SetMsg(lblMsg, "Employee Code Can not be left blank.")
        '    Return False
        'End If

        Code = Session("DalObj").ExecuteCommand(" Select Count(*) From HRDMAST Where EMP_CODE = '" & Chk(TxtCode.Text) & "'" & Session("UserCodes"), , DAL.DataLayer.ExecutionType.ExecuteScalar)
        If Code = 0 Then
            SetMsg(lblMsg, "Employee Code " & Chk(TxtCode.Text) & " does not exist.")
            Return False
        End If

        Return True

    End Function

    Sub AfterSave()
        Try
            BindLeavGrid("dasd")
            TxtCode.Text = ""
            cmbEmp.SelectedIndex = cmbEmp.Items.Count - 1
            SetMsg(lblMsg, "Record Saved Successfully")
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message)
        End Try
    End Sub

#End Region

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub
End Class
