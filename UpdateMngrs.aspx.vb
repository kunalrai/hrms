Partial Class UpdateMngrs
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
            If Not IsPostBack Then
                'By Ravi 21 Nov
                Dim SrNo As Int16
                SrNo = Request.QueryString.Item("SrNo") + Request.QueryString.Item("MODULEID")
                Dim bSuccess As Boolean
                Select Case CheckRight(SrNo)
                    Case MdlHRMS.AccessType.FullAccess, MdlHRMS.AccessType.SuperUser
                        bSuccess = True
                    Case MdlHRMS.AccessType.ReadonlyAccess, MdlHRMS.AccessType.Restricted
                        bSuccess = False
                End Select
                cmdSave.Visible = bSuccess

                '------------------------------------

                ClearAll(Me)
                Session("BalObj").fillcombo(cmbManager, "Select Emp_Code, Emp_Name as FNAME from HrdMastQry Where 1=1 " & Session("UserCodes") & " Order by FNAME", True)
                Session("BalObj").fillcombo(cmbChangeMngr, "Select Emp_Code, (Emp_Name + ' (' + Emp_Code + ')') as FNAME from HrdMastQry Where 1=1 " & Session("UserCodes") & " Order by FNAME", True)
                TxtCode_TextChanged(sender, e)
                lblName.Text = ""
            End If


        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message)
        End Try
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            If Not IsValidate() Then Exit Sub
            Dim StrQry As String, i As Int16

            For i = 0 To GrdSubordinates.Items.Count - 1
                If CType(GrdSubordinates.Items(i).Cells(7).Controls(1), CheckBox).Checked = True Then
                    StrQry = " Update HRDMAST Set Mngr_Code='" & Chk(cmbChangeMngr.SelectedValue) & "' Where Emp_Code='" & GrdSubordinates.Items(i).Cells(0).Text & "'"
                    Session("DalObj").ExecuteCommand(StrQry)
                End If
            Next
            SetMsg(LblErrMsg, " Records Updated Successfully.")
            ClearAll(Me)
            TxtCode_TextChanged(sender, e)
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message)
        End Try
    End Sub

    Function IsValidate() As Boolean
        Try

            If Chk(TxtCode.Text) = "" Then
                SetMsg(LblErrMsg, " Please Select Current Manager from the List.")
                Return False
            End If

            If GrdSubordinates.Items.Count = 0 Then
                SetMsg(LblErrMsg, " No Subordinates found for this Employee Code, Record Not Saved.")
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

    Private Sub TxtCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtCode.TextChanged
        Try

            LblErrMsg.Text = ""
            Dim StrQry As String, Dt As New DataTable, Code As String
            'By Ravi on 15 dec 2006 for shoing no record if TxtCode is Blank
            If TxtCode.Text = "" Then
                StrQry = "SELECT EMP_CODE,EMP_NAME,DEPT_NAME,DIVI_NAME,DSG_NAME,SECT_NAME,GRD_NAME FROM HRDMASTQRY WHERE MNGR_CODE = '" & "**" & "'"
            Else
                StrQry = "SELECT EMP_CODE,EMP_NAME,DEPT_NAME,DIVI_NAME,DSG_NAME,SECT_NAME,GRD_NAME FROM HRDMASTQRY WHERE MNGR_CODE = '" & Chk(TxtCode.Text) & "'"

            End If
            '----------------------------------------------------------------------------
            'Comment by Ravi
            'StrQry = "SELECT EMP_CODE,EMP_NAME,DEPT_NAME,DIVI_NAME,DSG_NAME,SECT_NAME,GRD_NAME FROM HRDMASTQRY WHERE MNGR_CODE = '" & Chk(TxtCode.Text) & "'"

            CType(Session("DalObj"), DAL.DataLayer).GetSqlDataTable(Dt, StrQry)

            Code = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand("Select Isnull(EMP_NAME,'') From HRDMASTQRY Where EMP_CODE = '" & ChkN(TxtCode.Text) & "'" & Session("UserCodes"), , DAL.DataLayer.ExecutionType.ExecuteScalar)
            lblName.Text = Code
            cmbChangeMngr.SelectedIndex = cmbChangeMngr.Items.Count - 1
            GrdSubordinates.DataSource = Dt
            GrdSubordinates.DataBind()
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message)
        End Try
    End Sub

    Private Sub cmbManager_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbManager.SelectedIndexChanged
        Try
            TxtCode.Visible = True
            btnList.Visible = True
            cmbManager.Visible = False
            TxtCode.Text = cmbManager.SelectedValue
            TxtCode_TextChanged(sender, e)
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message)
        End Try
    End Sub

    Private Sub btnList_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnList.Click
        cmbManager.Visible = True
        TxtCode.Visible = False
        btnList.Visible = False
    End Sub

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub
End Class
