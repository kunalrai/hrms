Public Class SubDeptMast
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents LblErrMsg As System.Web.UI.WebControls.Label
    Protected WithEvents TxtCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents cmbCode As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnList As System.Web.UI.WebControls.ImageButton
    Protected WithEvents TxtSubDeptDesc As System.Web.UI.WebControls.TextBox
    Protected WithEvents cmbUDept As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cmdNew As System.Web.UI.WebControls.Button
    Protected WithEvents cmdSave As System.Web.UI.WebControls.Button
    Protected WithEvents cmdClose As System.Web.UI.WebControls.Button
    Protected WithEvents Table1 As System.Web.UI.HtmlControls.HtmlTable

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
            cmdNew_Click(sender, e)
        End If
    End Sub
    Private Sub cmdNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNew.Click
        Try
            LblErrMsg.Text = ""
            Session("BalObj").FillCombo(cmbCode, "Select SubDept_CODE, SubDept_NAME from SUBDEPTMast ORDER BY SubDept_NAME", True)
            Session("BalObj").FillCombo(cmbUDept, "Select DEPT_CODE, DEPT_NAME from DEPTMAST ORDER BY DEPT_NAME", True)
            ClearAll(Me)
            TxtCode.Text = Session("BalObj").GetNextNumber("SUBDEPTMAST", "SUBDEPT_CODE")
            TxtCode.Text = TxtCode.Text.PadLeft(4, "0")
            DisplayRecords(TxtCode.Text)
            ViewState("Action") = "ADDNEW"
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & ex.Source)
        End Try
    End Sub
    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("Main.aspx")
    End Sub
    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub

    Private Sub btnList_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnList.Click
        LblErrMsg.Text = ""
        cmbCode.Visible = True
        TxtCode.Visible = False
        btnList.Visible = False
        cmdNew.Visible = False
    End Sub
    Private Sub cmbCode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCode.SelectedIndexChanged
        Try
            If cmbCode.SelectedValue = "" Then Exit Sub
            TxtCode.Visible = True
            btnList.Visible = True
            cmbCode.Visible = False
            cmdNew.Visible = True
            TxtCode.Text = cmbCode.SelectedValue
            DisplayRecords(Chk(TxtCode.Text))
            ViewState("Action") = "MODIFY"
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Code Changed()")
        End Try
    End Sub
    Sub DisplayRecords(ByVal Code As String)
        Try
            Dim i As Int16, SqlStr As String
            Dim DtTemp As New DataTable
            SqlStr = " Select SUBDEPT_CODE, SUBDEPT_NAME, DEPT_CODE from SUBDEPTMAST Where SUBDEPT_CODE = '" & Code & "'"
            Session("DalObj").GetSqlDataTable(DtTemp, SqlStr)
            If DtTemp.Rows.Count = 0 Then Exit Sub
            TxtSubDeptDesc.Text = DtTemp.Rows(0).Item("SUBDEPT_NAME")
            ChkCombo(cmbUDept, Chk(DtTemp.Rows(0).Item("Dept_Code")))
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Display Records")
        End Try
    End Sub
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            If Not IsValidate() Then Exit Sub
            Dim SqlStr As String, i As Int16

            If ViewState("Action") = "ADDNEW" Then
                SqlStr = " Insert SUBDEPTMAST ( SUBDEPT_CODE, SUBDEPT_NAME, DEPT_Code ) Values ('" & _
                         Chk(TxtCode.Text) & "', '" & _
                         Chk(TxtSubDeptDesc.Text) & "', '" & _
                         Chk(cmbUDept.SelectedValue) & "' )"
                Session("DalObj").ExecuteCommand(SqlStr)
                LblErrMsg.Text = "Record Saved Successfully."
                
            ElseIf ViewState("Action") = "MODIFY" Then

                SqlStr = " Update SUBDEPTMAST Set " & _
                         " SUBDEPT_CODE = '" & Chk(TxtCode.Text) & "', " & _
                         " SUBDEPT_NAME = '" & Chk(TxtSubDeptDesc.Text) & "', " & _
                         " DEPT_CODE = '" & Chk(cmbUDept.SelectedValue) & " ' Where SUBDEPT_CODE = '" & Chk(TxtCode.Text) & "'"

                Session("DalObj").ExecuteCommand(SqlStr)
                LblErrMsg.Text = "Record Modified Successfully."

            End If
            ' cmdNew_Click(sender, e)
            ViewState("Action") = "ADDNEW"
            TxtCode.Text = Session("BalObj").GetNextNumber("SUBDEPTMAST", "SUBDEPT_CODE")
            TxtCode.Text = TxtCode.Text.PadLeft(4, "0")
            TxtSubDeptDesc.Text = ""
            Session("BalObj").FillCombo(cmbUDept, "Select DEPT_CODE, DEPT_NAME from DEPTMAST ORDER BY DEPT_NAME", True)
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & ex.Source)
        End Try
    End Sub


    Function IsValidate() As Boolean
        Try
            Dim Code As Int16

            If TxtCode.Text = "" Then
                SetMsg(LblErrMsg, " Code can not be left blank, Record Not Saved.")
                Return False
            End If

            If TxtSubDeptDesc.Text = "" Then
                SetMsg(LblErrMsg, " Sub Department Name can not be left blank, Record Not Saved.")
                Return False
            End If
            Return True
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Validate Records")
        End Try
    End Function
End Class
