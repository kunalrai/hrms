Public Class Accessories
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents LblErrMsg As System.Web.UI.WebControls.Label
    Protected WithEvents TxtCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents cmbEmpCode As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnList As System.Web.UI.WebControls.ImageButton
    Protected WithEvents LblName As System.Web.UI.WebControls.Label
    Protected WithEvents CmbDept As System.Web.UI.WebControls.DropDownList
    Protected WithEvents CmbLoc As System.Web.UI.WebControls.DropDownList
    Protected WithEvents CmbDesg As System.Web.UI.WebControls.DropDownList
    Protected WithEvents CmbType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cmdReqAdd As System.Web.UI.WebControls.Button
    Protected WithEvents Tabgrdreq As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents GrdItem As System.Web.UI.WebControls.DataGrid
    Protected WithEvents CmdSave As System.Web.UI.WebControls.Button
    Protected WithEvents CmdClose As System.Web.UI.WebControls.Button

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
            Session("BalObj").FillCombo(cmbEmpCode, "Select Emp_Code,Emp_Name as EMP_NAME From HrdMastQry Order by Emp_Name", True)
            FillCombo()
            BindGrdItem()
        End If
    End Sub

    Private Sub FillCombo()
        Try
            Session("BalObj").FillCombo(CmbDept, "select Dept_Code,Dept_Name from DeptMast order by Dept_Name", True)
            Session("BalObj").FillCombo(CmbDesg, "Select Dsg_Code,Dsg_Name from DSGMAST  order by Dsg_Name", True)
            Session("BalObj").FillCombo(CmbType, "Type_Code", "Type_Name", "EmpType", True)
            Session("BalObj").FillCombo(CmbLoc, "Loc_Code", "Loc_Name", "LocMast", True)
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (FillCombo)")
        End Try
    End Sub

    Private Sub btnList_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnList.Click
        TxtCode.Visible = False
        btnList.Visible = False
        cmbEmpCode.Visible = True
    End Sub

    Private Sub TxtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCode.TextChanged
        Try
            LblErrMsg.Text = ""
            DisplayRecords()
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " TxtCode_TextChanged")
        End Try
    End Sub

    Private Sub cmbEmpCode_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEmpCode.SelectedIndexChanged
        Try
            LblErrMsg.Text = ""
            If cmbEmpCode.SelectedValue <> "" Then
                TxtCode.Text = cmbEmpCode.SelectedValue
            Else
                TxtCode.Text = ""
            End If
            '  TxtCode_TextChanged(sender, e)
            TxtCode.Visible = True
            btnList.Visible = True
            cmbEmpCode.Visible = False
            DisplayRecords()
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & "cmbEmp_SelectedIndexChanged")
        End Try
    End Sub

    Sub DisplayRecords()
        Try
            Dim StrSQl, tmp As String, i, j As Int16, dtEmp As New DataTable
            ViewState("dtreq") = New DataTable
            Dim EmpName As Object
            StrSQl = "Select * From HrdMastQry Where Emp_Code = '" & Chk(TxtCode.Text) & "'" & Session("UserCodes")
            Session("DalObj").GetSqlDataTable(dtEmp, StrSQl)
            EmpName = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(" Select Emp_Name From HrdMastQry Where Emp_Code = '" & Chk(TxtCode.Text) & "'" & Session("UserCodes"), , DAL.DataLayer.ExecutionType.ExecuteScalar)

            If (Not IsDBNull(EmpName)) And (Not IsNothing(EmpName)) Then
                LblName.Text = EmpName
            Else
                Dim Code As Object
                Code = Session("DalObj").ExecuteCommand("Select Emp_Code From HrdMastQry Where Emp_Code = '" & Chk(TxtCode.Text) & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
                If Code <> "" Then
                    SetMsg(LblErrMsg, "This Employee Code Exist For Other Location.")
                End If
                LblName.Text = ""
            End If
            dtEmp = Nothing

            Dim DEPTCODE, DSGCODE, EMPTYPE, LOCCODE As String

            DEPTCODE = Session("DalObj").ExecuteCommand("Select Dept_Code From hrdmastqry Where Emp_Code = '" & Chk(TxtCode.Text) & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
            CmbDept.SelectedValue = DEPTCODE

            DSGCODE = Session("DalObj").ExecuteCommand("Select DSG_CODE From hrdmastqry Where Emp_Code = '" & Chk(TxtCode.Text) & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
            CmbDesg.SelectedValue = DSGCODE

            EMPTYPE = Session("DalObj").ExecuteCommand("Select Type_CODE From hrdmastqry Where Emp_Code = '" & Chk(TxtCode.Text) & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
            CmbType.SelectedValue = EMPTYPE


            LOCCODE = Session("DalObj").ExecuteCommand("Select LoC_CODE From hrdmastqry Where Emp_Code = '" & Chk(TxtCode.Text) & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
            CmbLoc.SelectedValue = LOCCODE

            StrSQl = "select ItemName,Right('0' + DateName(d,Purdate),2) + '/' + Left(DateName(mm,Purdate),3) + '/' + DateName(yy,Purdate) As Purdate, " & _
                     "ModelNo,Make,Cost, Right('0' + DateName(d,allocationDate),2) + '/' + Left(DateName(mm,allocationDate),3) + '/' + DateName(yy,allocationDate) As allocationDate, Returned,Returneddate from accessories where Emp_Code = '" & Chk(TxtCode.Text) & "'"

            Session("DalObj").GetSqlDataTable(ViewState("dtreq"), StrSQl)
            GrdItem.DataSource = ViewState("dtreq")
            GrdItem.DataBind()


            If ViewState("dtreq").Rows.Count > 0 Then

                For j = 0 To GrdItem.Items.Count - 1
                    If ViewState("dtreq").Rows(j).Item("Returned") = "Checked" Then
                        CType(GrdItem.Items(j).Cells(6).FindControl("ChkReturn"), CheckBox).Checked = True
                    Else
                        CType(GrdItem.Items(j).Cells(6).FindControl("ChkReturn"), CheckBox).Checked = False
                    End If
                    'j = j + 1
                Next
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " DisplayRecords")
        End Try
    End Sub


    Private Sub BindGrdItem()
        Try
            ViewState("dtreq") = New DataTable
            Dim StrSql As String
            StrSql = "select ItemName,Right('0' + DateName(d,Purdate),2) + '/' + Left(DateName(mm,Purdate),3) + '/' + DateName(yy,Purdate) As Purdate, " & _
                     "ModelNo,Make,Cost, Right('0' + DateName(d,allocationDate),2) + '/' + Left(DateName(mm,allocationDate),3) + '/' + DateName(yy,allocationDate) As allocationDate, Returned,Returneddate from accessories where Emp_Code = '" & Chk(TxtCode.Text) & "'"
            Session("DalObj").GetSqlDataTable(ViewState("dtreq"), StrSql)
            GrdItem.DataSource = ViewState("dtreq")
            GrdItem.DataBind()
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (BindGrdItem)")
        Finally
        End Try
    End Sub

    Private Sub cmdReqAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdReqAdd.Click
        Try
            Dim i As Int16
            If GrdItem.Items.Count > 0 Then
                If Chk(CType(GrdItem.Items(GrdItem.Items.Count - 1).Controls(0).Controls(1), TextBox).Text) <> "" Then
                    For i = 0 To GrdItem.Items.Count - 1
                        ViewState("dtreq").Rows(i).Item("ItemName") = Chk(CType(GrdItem.Items(i).Controls(0).Controls(1), TextBox).Text)
                        ViewState("dtreq").Rows(i).Item("ModelNo") = Chk(CType(GrdItem.Items(i).Controls(1).Controls(1), TextBox).Text)
                        ViewState("dtreq").Rows(i).Item("PurDate") = CType(GrdItem.Items(i).Controls(2).Controls(1), TextBox).Text
                        ViewState("dtreq").Rows(i).Item("Make") = Chk(CType(GrdItem.Items(i).Controls(3).Controls(1), TextBox).Text)
                        ViewState("dtreq").Rows(i).Item("Cost") = ChkN(CType(GrdItem.Items(i).Controls(4).Controls(1), TextBox).Text)
                        ViewState("dtreq").Rows(i).Item("AllocationDate") = CType(GrdItem.Items(i).Controls(5).Controls(1), TextBox).Text
                        ViewState("dtreq").Rows(i).Item("Returned") = CType(GrdItem.Items(i).Controls(6).Controls(1), CheckBox).Checked
                    Next
                    Dim tmpTr As DataRow = ViewState("dtreq").NewRow()
                    ViewState("dtreq").Rows.Add(tmpTr)
                    GrdItem.DataSource = ViewState("dtreq")
                    GrdItem.DataBind()
                End If
            Else
                Dim tmpTr As DataRow = ViewState("dtreq").NewRow()
                ViewState("dtreq").Rows.Add(tmpTr)
                GrdItem.DataSource = ViewState("dtreq")
                GrdItem.DataBind()
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (cmdReqAdd_Click)")
        End Try
    End Sub

    Private Sub SaveRecords(ByVal sender As Object)
        Dim Tran As SqlClient.SqlTransaction
        Try
            Dim cnt As Int16
            Dim strSQl As String, i As Int16
            Dim dt As New DataTable
            If Not IsValidate() Then Exit Sub
            strSQl = "select * from Accessories where emp_Code='" & Chk(TxtCode.Text) & "'"
            Session("DalObj").GetSqlDataTable(dt, strSQl)

            Tran = Session("DalObj").StartTransaction("Save")
            If dt.Rows.Count > 0 Then
                strSQl = "Delete from Accessories where emp_Code='" & Chk(TxtCode.Text) & "'"
                Session("DalObj").ExecuteCommand(strSQl, Tran)
            End If
            '=====================Insert Accessories ========================
            For cnt = 0 To GrdItem.Items.Count - 1
                strSQl = " Insert InTo ACCESSORIES " & _
                                  " (Emp_Code, ITEMNAME, MODELNO, PURDATE, MAKE, COST, ALLOCATIONDATE,RETURNED) Values ('" & _
                                  Chk(TxtCode.Text) & "', '" & _
                                  Chk(CType(GrdItem.Items(cnt).Controls(0).Controls(1), TextBox).Text) & "', '" & _
                                  Chk(CType(GrdItem.Items(cnt).Controls(1).Controls(1), TextBox).Text) & "', '" & _
                                  Chk(CType(GrdItem.Items(cnt).Controls(2).Controls(1), TextBox).Text) & "', '" & _
                                  Chk(CType(GrdItem.Items(cnt).Controls(3).Controls(1), TextBox).Text) & "', '" & _
                                  ChkN(CType(GrdItem.Items(cnt).Controls(4).Controls(1), TextBox).Text) & "', '" & _
                                  Chk(CType(GrdItem.Items(cnt).Controls(5).Controls(1), TextBox).Text) & "',"

                If CType(GrdItem.Items(cnt).Cells(6).FindControl("ChkReturn"), CheckBox).Checked Then
                    strSQl = strSQl & "'Checked')"
                Else
                    strSQl = strSQl & "'')"
                End If
                Session("DalObj").ExecuteCommand(strSQl, Tran)
                ' cnt = cnt + 1
            Next
            Tran.Commit()
            SetMsg(LblErrMsg, " Records Saved Sucessfully.")
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (SAVE FUNCTION)")
            Tran.Rollback()
        Finally
            If Not IsNothing(Tran) Then Tran.Dispose()
        End Try
    End Sub

    Private Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdSave.Click
        Try
            SaveRecords(Nothing)
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & "(CmdSave_Click)")
        End Try
    End Sub
    Function IsValidate() As Boolean
        Try
            Dim cnt As Int16
            If Trim(TxtCode.Text) = "" Then
                SetMsg(LblErrMsg, "Employee Code Can't Be Left Blank.")
                Return False
            End If

            If GrdItem.Items.Count = 0 Then
                SetMsg(LblErrMsg, "Please Enter The Detail of atleast one Item.")
                Return False
            End If
            If CType(GrdItem.Items(cnt).Controls(0).Controls(1), TextBox).Text = "" Then
                SetMsg(LblErrMsg, "Item name Can't be Left Blank.")
                Return False
            End If
            If CType(GrdItem.Items(cnt).Controls(5).Controls(1), TextBox).Text = "" Then
                SetMsg(LblErrMsg, "Allocation Date Can't be Left Blank")
                Return False
            End If
            Return True
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Validate Records")
        End Try
    End Function
End Class
