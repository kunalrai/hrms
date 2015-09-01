Partial Class Promotion
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
        Try
            If IsNothing(Session("LoginUser")) Then
                Response.Redirect("CompSel.aspx")
            End If

            If Not IsPostBack Then
                CType(Session("BalObj"), BAL.BLayer).FillCombo(cmbEmp, " SELECT EMP_CODE, EMP_NAME FROM HRDMASTQRY WHERE Ltype=1  order by Emp_name", True)
                DtpNewDoj.Text = Format(Date.Today, "dd/MMM/yyyy")
                Session("BalObj").FillCombo(cmbEmpType, "Select Type_Code,Type_Name From EmpType Order By Type_Name", True)
                DtpDDOR.Text = Format(Date.Today, "dd/MMM/yyyy")
            End If

        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " On Load ")
        End Try
    End Sub

    Private Sub cmbEmp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbEmp.SelectedIndexChanged
        Try
            TxtCode.Visible = True
            btnList.Visible = True
            cmbEmp.Visible = False
            TxtCode.Text = cmbEmp.SelectedValue
            TxtCode_TextChanged(sender, e)
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " On Code Changed ")
        End Try
    End Sub

    Private Sub TxtCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            lblMsg.Text = ""
            Dim dt As New DataTable
            CType(Session("DalObj"), DAL.DataLayer).GetSqlDataTable(dt, " Select isnull(EMP_NAME,'') as EMPNAME,DOJ,DOC From HrdMastQry WHERE Ltype=1 and Emp_Code='" & Chk(TxtCode.Text) & "'" & Session("UserCodes"))

            If dt.Rows.Count = 0 Then Exit Sub

            LblName.Text = Chk(dt.Rows(0).Item("EMPNAME"))
            If Not IsDBNull(dt.Rows(0).Item("DOJ")) Then
                lbldoj.Text = Format(dt.Rows(0).Item("DOJ"), "dd/MMM/yyyy")
            Else
                lbldoj.Text = "None"
            End If
            If Not IsDBNull(dt.Rows(0).Item("DOC")) Then
                lbldoc.Text = Format(dt.Rows(0).Item("DOC"), "dd/MMM/yyyy")
                DtpNewDoj.Text = Format(dt.Rows(0).Item("DOC"), "dd/MMM/yyyy")
            Else
                lbldoc.Text = "None"
            End If
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " On Code Changed ")
        End Try
    End Sub

    Private Sub btnList_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnList.Click
        TxtCode.Visible = False
        btnList.Visible = False
        cmbEmp.Visible = True
    End Sub

    Private Sub CmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdSave.Click
        Dim Tran As SqlClient.SqlTransaction
        Try
            If Not IsValidate() Then Exit Sub
            Dim SqlStr As String, i As Int16
            Dim Dt As New DataTable
            Dim EMP_NAME As String
            EMP_NAME = Session("DALOBJ").EXECUTECOMMAND("sELECT EMP_NAME FROM HRDMASTQRY WHERE EMP_CODE='" & Chk(TxtCode.Text) & "'" & Session("UserCodes"), , DAL.DataLayer.ExecutionType.ExecuteScalar)
            Tran = CType(Session("DalObj"), DAL.DataLayer).StartTransaction("PROMO")

            '=====================Insert HrdMast=================================================
            SqlStr = " Select * into #HrdMast From HrdMast Where EMP_CODE='" & TxtCode.Text & "'; " & _
                     " Update #HrdMast Set EMP_CODE='" & TxtNewCode.Text & "', " & _
                     " Type_Code = '" & Chk(cmbEmpType.SelectedValue) & "'," & _
                     " DOJ='" & Format(CDate(DtpNewDoj.Text), "dd/MMM/yyyy") & "'" & _
                     " Where EMP_CODE='" & TxtCode.Text & "'; " & _
                     " Insert HrdMast Select * from #HrdMast Where EMP_Code='" & TxtNewCode.Text & "';" & _
                     " Drop Table #HrdMast"

            CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(SqlStr, Tran)
            CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand("Update HrdMast Set LType = 9, LREASON='Promotion',DOL='" & Format(DateAdd(DateInterval.Day, -1, CDate(DtpNewDoj.Text)), "dd/MMM/yyyy") & "' Where Emp_Code='" & TxtCode.Text & "'", Tran)

            '===========================================================insert HRD_OJT=================================


            SqlStr = "insert into HRD_OJT (NEW_EMP,EMP_NAME,OLD_EMP,CHANGE_DATE) VALUES('" & Chk(TxtNewCode.Text) & "','" & Chk(EMP_NAME) & "','" & Chk(TxtCode.Text) & "','" & Chk(DtpDDOR.Text) & "')"
            CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(SqlStr, Tran)



            '=====================Insert HrdQual=================================================
            SqlStr = " Select * into #HrdQual From HrdQual Where EMP_CODE='" & TxtCode.Text & "'; " & _
                     " Update #HrdQual Set EMP_CODE='" & TxtNewCode.Text & "'" & _
                     " Where EMP_CODE='" & TxtCode.Text & "'; " & _
                     " Insert HrdQual Select * from #HrdQual Where EMP_Code='" & TxtNewCode.Text & "';" & _
                     " Drop Table #HrdQual"
            CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(SqlStr, Tran)

            '=====================Insert HRDEXP=================================================
            SqlStr = " Select * into #HrdExp From HrdExp Where EMP_CODE='" & TxtCode.Text & "'; " & _
                               " Update #HrdExp Set EMP_CODE='" & TxtNewCode.Text & "'" & _
                               " Where EMP_CODE='" & TxtCode.Text & "'; " & _
                               " Insert HrdExp Select * from #HrdExp Where EMP_Code='" & TxtNewCode.Text & "';" & _
                               " Drop Table #HrdExp"
            CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(SqlStr, Tran)

            '=====================Insert HRDFAMILY=================================================
            SqlStr = " Select * into #HRDFAMILY From HRDFAMILY Where EMP_CODE='" & TxtCode.Text & "'; " & _
                               " Update #HRDFAMILY Set EMP_CODE='" & TxtNewCode.Text & "'" & _
                               " Where EMP_CODE='" & TxtCode.Text & "'; " & _
                               " Insert HRDFAMILY Select * from #HRDFAMILY Where EMP_Code='" & TxtNewCode.Text & "';" & _
                               " Drop Table #HRDFAMILY"
            CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(SqlStr, Tran)

            '=====================Insert HRDShift=================================================
            SqlStr = " Select * into #HRDShift From HRDShift Where EMP_CODE='" & TxtCode.Text & "'; " & _
                               " Update #HRDShift Set EMP_CODE='" & TxtNewCode.Text & "'" & _
                               " Where EMP_CODE='" & TxtCode.Text & "'; " & _
                               " Insert HRDShift Select * from #HRDShift Where EMP_Code='" & TxtNewCode.Text & "';" & _
                               " Drop Table #HRDShift"
            CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(SqlStr, Tran)

            '=====================Insert HRDSKILLS=================================================
            SqlStr = " Select * into #HRDSKILLS From HRDSKILLS Where EMP_CODE='" & TxtCode.Text & "'; " & _
                               " Update #HRDSKILLS Set EMP_CODE='" & TxtNewCode.Text & "'" & _
                               " Where EMP_CODE='" & TxtCode.Text & "'; " & _
                               " Insert HRDSKILLS Select * from #HRDSKILLS Where EMP_Code='" & TxtNewCode.Text & "';" & _
                               " Drop Table #HRDSKILLS"
            CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(SqlStr, Tran)

            '=====================Insert PayMast=================================================
            SqlStr = " Select * into #PayMast From PayMast Where EMP_CODE='" & TxtCode.Text & "'; " & _
                               " Update #PayMast Set EMP_CODE='" & TxtNewCode.Text & "'" & _
                               " Where EMP_CODE='" & TxtCode.Text & "'; " & _
                               " Insert PayMast  Select * from #PayMast Where EMP_Code='" & TxtNewCode.Text & "';" & _
                               " Drop Table #PayMast"
            CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(SqlStr, Tran)

            '=====================Insert PayHist=================================================
            SqlStr = " Select * into #PayHist From PayHist Where EMP_CODE='" & TxtCode.Text & "'; " & _
                               " Update #PayHist Set EMP_CODE='" & TxtNewCode.Text & "'" & _
                               " Where EMP_CODE='" & TxtCode.Text & "'; " & _
                               " Insert PayHist  Select * from #PayHist Where EMP_Code='" & TxtNewCode.Text & "';" & _
                               " Drop Table #PayHist"
            CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(SqlStr, Tran)


            Tran.Commit()
            ClearAll(Me)
            SetMsg(lblMsg, " Record Saved Successfully.")
            CType(Session("BalObj"), BAL.BLayer).FillCombo(cmbEmp, " SELECT EMP_CODE, EMP_NAME FROM HRDMASTQRY WHERE Ltype=1", True)
            lbldoc.Text = ""
            lbldoj.Text = ""
            LblName.Text = ""
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " On Save ")
            If Not IsNothing(Tran) Then Tran.Rollback()
        Finally
            If Not IsNothing(Tran) Then Tran.Dispose()
        End Try
    End Sub

    Function IsValidate() As Boolean
        Try
            Dim Count As Int16

            If Chk(TxtCode.Text) = "" Then
                SetMsg(lblMsg, " Existing Employee Code is required.")
                Return False
            End If

            If Chk(TxtNewCode.Text) = "" Then
                SetMsg(lblMsg, " Updated Employee Code is required.")
                Return False
            End If
            If Chk(DtpDDOR.Text) = "" Then
                SetMsg(lblMsg, "Date of Regularisation Can't be Left Blank.")
                Return False
            End If

            Count = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(" Select Count(*) From HrdMast Where EMp_Code='" & TxtNewCode.Text & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
            If Count <> 0 Then
                SetMsg(lblMsg, " Data already exist for Updated Employee Code.")
                Return False
            End If

            Return True
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " On IsValidate ")
        End Try
    End Function

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub
End Class
