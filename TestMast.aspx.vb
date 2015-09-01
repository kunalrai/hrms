Partial Class TestMast
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

#Region "Load"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If
        If Not IsPostBack Then
            NewRecord()

        End If
    End Sub
#End Region
    Private Sub btnList_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnList.Click
        LblErrMsg.Text = ""
        cmbCode.Visible = True
        TxtCode.Visible = False
        btnList.Visible = False
        btnNew.Visible = False
    End Sub
#Region "New Button Event"

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNew.Click
        NewRecord()
    End Sub
    Private Sub NewRecord()

        Try
            LblErrMsg.Text = ""
            Session("BalObj").FillCombo(cmbCode, "Select TestCode, TestName from TESTMST ORDER BY TestName", True)
            ClearAll(Me)
            TxtCode.Text = Session("BalObj").GetNextNumber("TESTMST", "TestCode")


            TxtCode.Text = TxtCode.Text.PadLeft(4, "0")
            ViewState("Action") = "ADDNEW"
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & ex.Source)
        End Try
    End Sub
#End Region
    '************  Select items from DropDown List******************8888'
    Private Sub cmbCode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCode.SelectedIndexChanged
        Try
            If cmbCode.SelectedValue = "" Then Exit Sub
            TxtCode.Visible = True
            btnList.Visible = True
            cmbCode.Visible = False
            btnNew.Visible = True
            TxtCode.Text = cmbCode.SelectedValue
            Dim StrSql As String
            Dim DtTemp As DataTable

            DtTemp = New DataTable
            StrSql = " Select  TestName, MaxMarks, MinMarks,Mark From testmst Where TestCode = '" & Chk(TxtCode.Text) & "'"
            Session("DalObj").GetSqlDataTable(DtTemp, StrSql)
            If DtTemp.Rows.Count = 0 Then Exit Sub
            Txtname.Text = Chk(DtTemp.Rows(0).Item("TestName"))

            Txtmaxmark.Text = Convert.ToString(DtTemp.Rows(0).Item("MaxMarks"))
            Txtminmark.Text = Convert.ToString(DtTemp.Rows(0).Item("MinMarks"))
            TxtQual.Text = Convert.ToString(DtTemp.Rows(0).Item("Mark"))

            ViewState("Action") = "MODIFY"
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Code Changed()")
        End Try
    End Sub
#Region " Save and Update "

    Private Sub Btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnsave.Click
        Dim Tran As SqlClient.SqlTransaction
        Try
            If Not IsValidate() Then Exit Sub
            Dim SqlStr As String, i As Int16
            If ViewState("Action") = "ADDNEW" Then

                SqlStr = "Insert into testmst( TestCode, TestName, MaxMarks,MinMarks,Mark ) Values ('" & _
                         Chk(TxtCode.Text) & "', '" & _
                         Chk(Txtname.Text) & "', '" & _
                         ChkN(Txtmaxmark.Text) & "','" & _
                         ChkN(Txtminmark.Text) & "', '" & _
                         ChkN(TxtQual.Text) & "')"
                Session("DalObj").ExecuteCommand(SqlStr)

            ElseIf ViewState("Action") = "MODIFY" Then

                SqlStr = "Update testmst Set " & _
                         "TestCode = '" & Chk(TxtCode.Text) & "', " & _
                         "TestName = '" & Chk(Txtname.Text) & "', " & _
                         "MaxMarks = '" & ChkN(Txtmaxmark.Text) & " ', " & _
                         "MinMarks='" & ChkN(Txtminmark.Text) & " ', " & _
                         "Mark= '" & ChkN(TxtQual.TextMode) & "'Where TestCode = '" & Chk(TxtCode.Text) & "'"

                Session("DalObj").ExecuteCommand(SqlStr)
            End If
            NewRecord()

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
            If Txtname.Text = "" Then
                SetMsg(LblErrMsg, " Test Name can not be left blank, Record Not Saved.")
                Return False
            End If
            If ChkN(Txtminmark.Text) > ChkN(Txtmaxmark.Text) Then
                SetMsg(LblErrMsg, "Minimum Marks Can not greater then Maximum Marks.")
                Return False
            End If
            If ChkN(TxtQual.Text) > ChkN(Txtmaxmark.Text) Then
                SetMsg(LblErrMsg, "Qualifying Marks can not be greater then Maximum Marks.")
                Return False
            End If
            If ChkN(Txtobtain.Text) > ChkN(Txtmaxmark.Text) Then
                SetMsg(LblErrMsg, " Obtain Marks can not be greater than Maximum Marks.")
                Return False
            End If

            If ViewState("Action") = "ADDNEW" Then
                Code = Session("DalObj").Executecommand(" Select Count(*) From testmst Where TestCode = '" & Chk(TxtCode.Text) & "'")
                If Code > 0 Then
                    SetMsg(LblErrMsg, " This TestCode Already exist, Record Not Saved.")
                    Return False
                End If
            End If

            Return True
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "Validate Records")
        End Try
    End Function
#End Region
    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Response.Redirect("Main.aspx")
    End Sub
End Class
