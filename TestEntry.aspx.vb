Partial Class TestEntry
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
            LblErrMsg.Text = ""
            Session("BalObj").FillCombo(CmbSession, "SELECT TrainMast.Train_Name + '(' + TrainMast.Train_Code + ')' + ' ~ ' + SUBSTRING(CAST(TRainCalendar.TRain_From as VarChar),1,len(CAST(TRainCalendar.TRain_From as VarChar))-7) + ' ~ ' + SUBSTRING(CAST(TRainCalendar.TRain_To as VarChar),1,len(CAST(TRainCalendar.TRain_To as VarChar))-7)  As Training,TRainCalendar.TrainCalCode FROM TRainMast INNER JOIN TRainCalendar ON (TRainCalendar.Train_Code = TRainMast.TRain_Code)", True)
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & ex.Source)
        End Try
    End Sub
    Private Sub CmbSession_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbSession.SelectedIndexChanged
        Try
            LblErrMsg.Text = ""
            Session("BalObj").FillCombo(CmbTname, "SELECT TestMst.TestName,TestMst.TestCode FROM TestMst INNER JOIN TrainCalTest ON (TestMst.TestCode = TrainCalTest.TestCode) WHERE TrainCalTest.TrainCalCode = " & CmbSession.SelectedValue, True)
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & ex.Source)
        End Try
    End Sub
    Private Sub BindGrdTest()
        Dim StrSql As String
        Try
            ViewState("dtTest") = New DataTable
            StrSql = "select * from TrainCalTest WHERE TrainCalCode = " & CmbSession.SelectedValue & " AND TestCode = '" & Chk(CmbTname.SelectedValue) & "'"
            Session("DalObj").GetSqlDataTable(ViewState("dtTest"), StrSql)

            If IsDBNull(ViewState("dtTest")!EnteredOn) Then  'Add New Mode
                StrSql = "DELETE FROM TrainEmpTest WHERE TrainCalCode = " & CmbSession.SelectedValue & " AND TestCode = '" & Chk(CmbTname.SelectedValue) & "'"
                Session("DalObj").ExecuteCommand(StrSql)
                StrSql = "INSERT INTO TrainEmpTest select TrainEmp.Emp_Code,TrainEmp.TrainCalCode,'" & Chk(CmbTname.SelectedValue) & "',0,0 FROM TrainEmp WHERE TrainCalCode = " & CmbSession.SelectedValue

                Session("DalObj").ExecuteCommand(StrSql)
                RadioFtst.Text = True

                RadioRtst.Enabled = False
                GrdTest.Columns(3).Visible = False
            Else
                RadioRtst.Enabled = True
                RadioRtst.Text = True
                GrdTest.Columns(3).Visible = True
            End If
            StrSql = "select TrainEmpTest.*,HrdMastQry.Emp_Name FROM TrainEmpTest INNER JOIN HrdMastQry ON (HrdMastQry.Emp_Code = TrainEmpTest.Emp_Code) WHERE TrainEmpTest.TrainCalCode = " & CmbSession.SelectedValue & " AND TrainEmpTest.TestCode = '" & ChkN(CmbTname.SelectedValue) & "'"
            Session("DalObj").GetSqlDataTable(ViewState("dtTest"), StrSql)
            GrdTest.DataSource = ViewState("dtTest")
            GrdTest.DataBind()
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (BindGridExp)")
        Finally
        End Try
    End Sub

    Private Sub RadioFtst_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioFtst.CheckedChanged
        Try
            If CmbSession.SelectedValue = "" Then
                Exit Sub
            End If
            BindGrdTest()
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (cmdAddExp_Click)")
        End Try
    End Sub

    Private Sub RadioRtst_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioRtst.CheckedChanged
        Try
            If CmbSession.SelectedValue = "" Then
                Exit Sub
            End If

            BindGrdTest()
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : (cmdAddExp_Click)")
        End Try
    End Sub


End Class
