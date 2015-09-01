
Imports System.Data.OleDb
Partial Class Login
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "workstation id=XEON;packet size=4096;user id=sa;pwd=microtel;data source=10.10.1." & _
        "0;persist security info=False;initial catalog=SMPLTD"

    End Sub
    Protected WithEvents imglogin As System.Web.UI.HtmlControls.HtmlImage
    Protected WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection

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
            If Server.GetLastError Is Nothing Then SetMsg(LblMsg, "")
            If Not IsPostBack Then
                FillCompList()
                cmbComp.Items.Add(New ListItem("--Company--", "O"))
                cmbComp.SelectedValue = "O"
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub cmdGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGo.Click
        Dim DrUsers As SqlClient.SqlDataReader
        Dim dtLoc As New DataTable
        Dim dtYear As New DataTable
        Dim DtMod As New DataTable
        Dim Count As Object
        Try
            Dim StrSQl As String = "Select * From WebUsers Where USERID = '" & Encrypt(UCase(txtUID.Text), "+") & "' And Type = 'U'"
            DrUsers = Session("DalObj").ExecuteCommand(StrSQl, , DalObj.ExecutionType.ExecuteReader)
            DrUsers.Read()
            If DrUsers.HasRows Then
                If DrUsers.Item("USERPASSWORD") = Encrypt(txtPWD.Text, "+") Then

                    StrSQl = " Select Count(*) From HrdMast Where Emp_Code='" & UCase(Chk(txtUID.Text)) & "'"
                    Count = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(StrSQl, , DAL.DataLayer.ExecutionType.ExecuteScalar)

                    If Count <> 0 Then
                        StrSQl = " Select Count(*) From HrdMast Where Emp_Code='" & UCase(Chk(txtUID.Text)) & "' And (Ltype=1 or ltype=9)"
                        Count = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(StrSQl, , DAL.DataLayer.ExecutionType.ExecuteScalar)
                        If Count = 0 Then
                            SetMsg(LblMsg, "User ID does not exist.")
                            Exit Sub
                        End If
                    End If

                    StrSQl = "Select Loc_Code,Loc_Name From HrdMastQry Where Emp_Code = '" & UCase(Chk(txtUID.Text)) & "'"
                    Session("DalObj").GetSqlDataTable(dtLoc, StrSQl)

                    Dim Mods As String

                    Mods = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand("Select isnull(Modules,'') From WebUsers Where UserId In ( Select Group_Id From WebUsers Where UserId = '" & Encrypt(UCase(txtUID.Text), "+") & "')", , DAL.DataLayer.ExecutionType.ExecuteScalar)

                    Dim LoginUser As DAL.DataLayer.Users

                    LoginUser = Session("LoginUser")
                    LoginUser.UserID = UCase(Chk(txtUID.Text))
                    LoginUser.UserName = UCase(Encrypt(Chk(DrUsers.Item("USERNAME")), "-"))
                    LoginUser.UserGroup = Encrypt(Chk(DrUsers.Item("GROUP_ID")), "-")
                    'If LoginUser.UserName <> "EDP" Then
                    LoginUser.UserModules = Mods
                    'End If

                    'LoginUser.UserModules = Chk(DrUsers.Item("MODULES"))
                    'LoginUser.UserModuleCodes = Chk(DrUsers.Item("CODES"))
                    If dtLoc.Rows.Count > 0 Then
                        LoginUser.UserLocationCode = Chk(dtLoc.Rows(0).Item("Loc_Code"))
                        LoginUser.UserLocationName = Chk(dtLoc.Rows(0).Item("Loc_Name"))
                    Else
                        LoginUser.UserLocationCode = ""
                        LoginUser.UserLocationName = ""
                    End If
                    Session("LoginUser") = LoginUser
                    Session("BalObj") = New BAL.BLayer(Session("DalObj"))

                    Session("FinYear") = ChkN(Session("DalObj").ExecuteCommand("Select Top 1 FIN_YR From FinYear Where  FIN_YR_CUR = 'Y'", , DAL.DataLayer.ExecutionType.ExecuteScalar))
                    Session("LeavYear") = ChkN(Session("DalObj").ExecuteCommand("Select Top 1 FIN_YR From FinYear Where  LEV_YR_CUR = 'Y'", , DAL.DataLayer.ExecutionType.ExecuteScalar))
                    Session("RimYear") = ChkN(Session("DalObj").ExecuteCommand("Select Top 1 FIN_YR From FinYear Where  RIM_YR_CUR = 'Y'", , DAL.DataLayer.ExecutionType.ExecuteScalar))

                    FY_Start = Session("DalObj").ExecuteCommand("Select Top 1 FIN_YR_ST From FinYear Where  FIN_YR_CUR = 'Y'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
                    RY_Start = Session("DalObj").ExecuteCommand("Select Top 1 RIM_YR_ST From FinYear Where  RIM_YR_CUR = 'Y'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
                    LY_Start = Session("DalObj").ExecuteCommand("Select Top 1 LEV_YR_ST From FinYear Where  LEV_YR_CUR = 'Y'", , DAL.DataLayer.ExecutionType.ExecuteScalar)
                    FY_End = FY_Start.AddYears(1).AddDays(-1)
                    RY_End = RY_Start.AddYears(1).AddDays(-1)
                    LY_End = LY_Start.AddYears(1).AddDays(-1)
                    'Response.Redirect("SelFinYear.aspx")
                    Codes()

                    Response.Redirect("Home1.htm")
                Else
                    SetMsg(LblMsg, "Invalid Password.")
                End If
            Else
                SetMsg(LblMsg, "Invalid User ID.")
            End If

        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " (cmdGo_Click)")
        Finally
            If Not IsNothing(DrUsers) Then DrUsers.Close()
            If Not IsNothing(dtLoc) Then dtLoc.Dispose()
        End Try
    End Sub

    Sub Codes()
        Try

            Dim SqlStr, StrTemp, StrCodes, StrEmpCodes, StrQry, SqlStrRep As String, Pos1, Pos2 As Int16
            Session("UserCodesRep") = ""
            StrCodes = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(" Select isnull(Codes,'') from WebUsers Where UserID In (Select Group_ID From WebUsers Where UserID = '" & eHRMS.Net.MdlHRMS.Encrypt(Session("LoginUser").UserId, "+") & "')", , DAL.DataLayer.ExecutionType.ExecuteScalar)
            StrQry = " Select isnull(Codes,'') from WebUsers Where UserID = '" & Encrypt(UCase(Session("LoginUser").UserId), "+") & "'"
            StrEmpCodes = CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand(StrQry, , DAL.DataLayer.ExecutionType.ExecuteScalar)

            If StrCodes = "" And StrEmpCodes = "" Then Exit Sub

            If StrCodes = "" Then
                StrCodes = StrEmpCodes
            ElseIf StrEmpCodes <> "" Then
                StrCodes = StrCodes & "," & StrEmpCodes
            End If

            Pos1 = 0
            StrTemp = ""
            Do While True
                Pos1 = InStr(Pos1 + 1, StrCodes, "`R")
                If Pos1 = 0 Then Exit Do
                Pos2 = InStr(Pos1 + 1, StrCodes, "R`")
                If Pos2 = 0 Then Exit Do
                StrTemp = StrTemp & ",'" & Mid(StrCodes, Pos1 + 2, Pos2 - Pos1 - 2) & "'"
                Pos1 = Pos2
            Loop
            If StrTemp <> "" Then SqlStr = SqlStr & " AND Regn_Code IN (" & Mid(StrTemp, 2) & ")"
            If StrTemp <> "" Then SqlStrRep = SqlStrRep & " AND {|Q|.Regn_Code} IN [" & Mid(StrTemp, 2) & "]"

            Pos1 = 0
            StrTemp = ""
            Do While True
                Pos1 = InStr(Pos1 + 1, StrCodes, "`L")
                If Pos1 = 0 Then Exit Do
                Pos2 = InStr(Pos1 + 1, StrCodes, "L`")
                If Pos2 = 0 Then Exit Do
                StrTemp = StrTemp & ",'" & Mid(StrCodes, Pos1 + 2, Pos2 - Pos1 - 2) & "'"
                Pos1 = Pos2
            Loop
            SqlStr = SqlStr & IIf(StrTemp = "", "", " AND Loc_Code IN (" & Mid(StrTemp, 2) & ")")
            If StrTemp <> "" Then SqlStrRep = SqlStrRep & " AND {|Q|.Loc_Code} IN [" & Mid(StrTemp, 2) & "]"
            Pos1 = 0
            StrTemp = ""
            Do While True
                Pos1 = InStr(Pos1 + 1, StrCodes, "`V")
                If Pos1 = 0 Then Exit Do
                Pos2 = InStr(Pos1 + 1, StrCodes, "V`")
                If Pos2 = 0 Then Exit Do
                StrTemp = StrTemp & ",'" & Mid(StrCodes, Pos1 + 2, Pos2 - Pos1 - 2) & "'"
                Pos1 = Pos2
            Loop
            SqlStr = SqlStr & IIf(StrTemp = "", "", " AND Divi_Code IN (" & Mid(StrTemp, 2) & ")")
            If StrTemp <> "" Then SqlStrRep = SqlStrRep & " AND {|Q|.Divi_Code} IN [" & Mid(StrTemp, 2) & "]"
            Pos1 = 0
            StrTemp = ""
            Do While True
                Pos1 = InStr(Pos1 + 1, StrCodes, "`S")
                If Pos1 = 0 Then Exit Do
                Pos2 = InStr(Pos1 + 1, StrCodes, "S`")
                If Pos2 = 0 Then Exit Do
                StrTemp = StrTemp & ",'" & Mid(StrCodes, Pos1 + 2, Pos2 - Pos1 - 2) & "'"
                Pos1 = Pos2
            Loop
            SqlStr = SqlStr & IIf(StrTemp = "", "", " AND Sect_Code IN (" & Mid(StrTemp, 2) & ")")
            If StrTemp <> "" Then SqlStrRep = SqlStrRep & " AND {|Q|.Sect_Code} IN [" & Mid(StrTemp, 2) & "]"
            Pos1 = 0
            StrTemp = ""
            Do While True
                Pos1 = InStr(Pos1 + 1, StrCodes, "`P")
                If Pos1 = 0 Then Exit Do
                Pos2 = InStr(Pos1 + 1, StrCodes, "P`")
                If Pos2 = 0 Then Exit Do
                StrTemp = StrTemp & ",'" & Mid(StrCodes, Pos1 + 2, Pos2 - Pos1 - 2) & "'"
                Pos1 = Pos2
            Loop
            SqlStr = SqlStr & IIf(StrTemp = "", "", " AND Proc_Code IN (" & Mid(StrTemp, 2) & ")")
            If StrTemp <> "" Then SqlStrRep = SqlStrRep & " AND {|Q|.Proc_Code} IN [" & Mid(StrTemp, 2) & "]"
            Pos1 = 0
            StrTemp = ""
            Do While True
                Pos1 = InStr(Pos1 + 1, StrCodes, "`D")
                If Pos1 = 0 Then Exit Do
                Pos2 = InStr(Pos1 + 1, StrCodes, "D`")
                If Pos2 = 0 Then Exit Do
                StrTemp = StrTemp & ",'" & Mid(StrCodes, Pos1 + 2, Pos2 - Pos1 - 2) & "'"
                Pos1 = Pos2
            Loop
            SqlStr = SqlStr & IIf(StrTemp = "", "", " AND Dept_Code IN (" & Mid(StrTemp, 2) & ")")
            If StrTemp <> "" Then SqlStrRep = SqlStrRep & " AND {|Q|.Dept_Code} IN [" & Mid(StrTemp, 2) & "]"
            Pos1 = 0
            StrTemp = ""
            Do While True
                Pos1 = InStr(Pos1 + 1, StrCodes, "`C")
                If Pos1 = 0 Then Exit Do
                Pos2 = InStr(Pos1 + 1, StrCodes, "C`")
                If Pos2 = 0 Then Exit Do
                StrTemp = StrTemp & ",'" & Mid(StrCodes, Pos1 + 2, Pos2 - Pos1 - 2) & "'"
                Pos1 = Pos2
            Loop
            SqlStr = SqlStr & IIf(StrTemp = "", "", " AND Cost_Code IN (" & Mid(StrTemp, 2) & ")")
            If StrTemp <> "" Then SqlStrRep = SqlStrRep & " AND {|Q|.Cost_Code} IN [" & Mid(StrTemp, 2) & "]"
            Pos1 = 0
            StrTemp = ""
            Do While True
                Pos1 = InStr(Pos1 + 1, StrCodes, "`T")
                If Pos1 = 0 Then Exit Do
                Pos2 = InStr(Pos1 + 1, StrCodes, "T`")
                If Pos2 = 0 Then Exit Do
                StrTemp = StrTemp & ",'" & Mid(StrCodes, Pos1 + 2, Pos2 - Pos1 - 2) & "'"
                Pos1 = Pos2
            Loop
            SqlStr = SqlStr & IIf(StrTemp = "", "", " AND Type_Code IN (" & Mid(StrTemp, 2) & ")")
            If StrTemp <> "" Then SqlStrRep = SqlStrRep & " AND {|Q|.Type_Code} IN [" & Mid(StrTemp, 2) & "]"
            Pos1 = 0
            StrTemp = ""
            Do While True
                Pos1 = InStr(Pos1 + 1, StrCodes, "`G")
                If Pos1 = 0 Then Exit Do
                Pos2 = InStr(Pos1 + 1, StrCodes, "G`")
                If Pos2 = 0 Then Exit Do
                StrTemp = StrTemp & ",'" & Mid(StrCodes, Pos1 + 2, Pos2 - Pos1 - 2) & "'"
                Pos1 = Pos2
            Loop
            SqlStr = SqlStr & IIf(StrTemp = "", "", " AND Grd_Code IN (" & Mid(StrTemp, 2) & ")")
            If StrTemp <> "" Then SqlStrRep = SqlStrRep & " AND {|Q|.Grd_Code} IN [" & Mid(StrTemp, 2) & "]"
            Pos1 = 0
            StrTemp = ""
            Do While True
                Pos1 = InStr(Pos1 + 1, StrCodes, "`J")
                If Pos1 = 0 Then Exit Do
                Pos2 = InStr(Pos1 + 1, StrCodes, "J`")
                If Pos2 = 0 Then Exit Do
                StrTemp = StrTemp & ",'" & Mid(StrCodes, Pos1 + 2, Pos2 - Pos1 - 2) & "'"
                Pos1 = Pos2
            Loop
            SqlStr = SqlStr & IIf(StrTemp = "", "", " AND Dsg_Code IN (" & Mid(StrTemp, 2) & ")")
            If StrTemp <> "" Then SqlStrRep = SqlStrRep & " AND {|Q|.Dsg_Code} IN [" & Mid(StrTemp, 2) & "]"
            Pos1 = 0
            StrTemp = ""
            Do While True
                Pos1 = InStr(Pos1 + 1, StrCodes, "`E")
                If Pos1 = 0 Then Exit Do
                Pos2 = InStr(Pos1 + 1, StrCodes, "E`")
                If Pos2 = 0 Then Exit Do
                StrTemp = StrTemp & ",'" & Mid(StrCodes, Pos1 + 2, Pos2 - Pos1 - 2) & "'"
                Pos1 = Pos2
            Loop
            SqlStr = SqlStr & IIf(StrTemp = "", "", " AND Emp_Code IN (" & Mid(StrTemp, 2) & ")")
            If StrTemp <> "" Then SqlStrRep = SqlStrRep & " AND {|Q|.Emp_Code} IN [" & Mid(StrTemp, 2) & "]"
            'SqlStr = SqlStr & " AND LTYPE = 1 "
            Session("UserCodes") = SqlStr
            Session("UserCodesRep") = SqlStrRep
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message & " (Codes)")
        End Try
    End Sub

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub
    Private Sub FillCompList()
        Dim cnSys As New OleDbConnection
        Dim myAdapter As New OleDbDataAdapter
        Try
            Dim strPath As String = HttpContext.Current.Request.ServerVariables("APPL_PHYSICAL_PATH")
            MiracleString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strPath & "\SYS.MDB;User ID=Admin;Jet OLEDB:Database Password=DIT683"
            cnSys.ConnectionString = MiracleString
            If cnSys.State = ConnectionState.Open Then cnSys.Close()
            cnSys.Open()
            If cnSys.State = ConnectionState.Open Then
                Dim dsSetUp As New DataSet
                Dim dsSetUp2 As New DataSet

                myAdapter.SelectCommand = New OleDbCommand("Select uCase(SETUP.COMP_NAME) As Company,Comp_Code As [Code] From SetUp ORDER BY SetUp.Comp_Code", cnSys)
                myAdapter.Fill(dsSetUp)

                If dsSetUp.Tables(0).Rows.Count > 0 Then
                    cmbComp.DataSource = dsSetUp
                    cmbComp.DataTextField = "Company"
                    cmbComp.DataValueField = "Code"
                    cmbComp.DataBind()
                Else
                    SetMsg(LblMsg, "No Company Name Found in the Database.")
                End If
            End If
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message)
        Finally
            myAdapter.Dispose()
            If cnSys.State = ConnectionState.Open Then cnSys.Close()
            cnSys.Dispose()
        End Try
    End Sub

    Private Sub cmbComp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComp.SelectedIndexChanged
        If IsNothing(cmbComp.SelectedItem) Then
            SetMsg(LblMsg, "Select a company to continue.")
            Exit Sub
        End If
        If cmbComp.SelectedItem.Value = "O" Then
            SetMsg(LblMsg, "Select a company to continue.")
            Exit Sub
        End If
        Try

            SetLoginUser()
            If Not IsNothing(Session("DalObj")) Then Session("DalObj") = Nothing

            Session("DalObj") = New DAL.DataLayer
            Session("DalObj").SqlConnectionString = Session("LoginUser").ConnectString
            'SqlConnection1.Open()
            'Session("DalObj").SqlConnectionString = SqlConnection1.ConnectionString
            Session("DalObj").OpenConnection(DalObj.ConnProvider.SQL)
            'Response.Redirect("Login.aspx")

        Catch ex As Exception
            SetMsg(LblMsg, ex.Message)
        End Try
    End Sub
    Private Sub SetLoginUser()
        Dim cnSys As New OleDbConnection
        Try
            cnSys.ConnectionString = MiracleString
            If cnSys.State = ConnectionState.Open Then cnSys.Close()
            cnSys.Open()
            If cnSys.State = ConnectionState.Open Then

                Dim adptSys As New OleDbDataAdapter
                Dim DTSys As New DataTable

                adptSys.SelectCommand = New OleDbCommand("Select * From SetUP", cnSys)
                adptSys.Fill(DTSys)

                Dim dvCompany As New DataView(DTSys, " Comp_Code = '" & Chk(cmbComp.SelectedValue) & "'", "Comp_Code", DataViewRowState.CurrentRows)
                If dvCompany.Count > 0 Then
                    Dim LoginUser As DAL.DataLayer.Users
                    LoginUser.CurrentCompID = Chk(dvCompany.Item(0).Item("COMP_CODE"))
                    LoginUser.CurrentCompanyName = Chk(dvCompany.Item(0).Item("COMP_NAME"))
                    LoginUser.DBName = Chk(dvCompany.Item(0).Item("DATABASENAME"))
                    LoginUser.DBType = Chk(dvCompany.Item(0).Item("DATABASETYPE"))
                    LoginUser.DSNName = Chk(dvCompany.Item(0).Item("DSN"))
                    LoginUser.ConnectString = Chk(dvCompany.Item(0).Item("CONNECTSTRING"))
                    LoginUser.ReportFolder = Chk(dvCompany.Item(0).Item("ReportFolder"))
                    LoginUser.ShowError = IIf(dvCompany.Item(0).Item("ShowError") = 1, True, False)
                    LoginUser.DBPassword = Chk(dvCompany.Item(0).Item("USERPASSWORD"))
                    LoginUser.DBUser = Chk(dvCompany.Item(0).Item("UserID"))
                    LoginUser.UserID = ""
                    LoginUser.Password = ""
                    LoginUser.UserGroup = ""
                    Session("LoginUser") = LoginUser
                    LoginUser = Nothing
                End If
            End If
        Catch ex As Exception
            SetMsg(LblMsg, ex.Message)
        Finally
            If cnSys.State = ConnectionState.Open Then cnSys.Close()
            cnSys.Dispose()
        End Try
    End Sub
End Class
