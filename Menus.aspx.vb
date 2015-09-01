Partial Class Menus
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
            RegisterStartupScript("Comp", "<SCRIPT language='javascript'>window.open(""CompSel.aspx"",""Main"") </SCRIPT>")
        Else
            Dim StrQuery, StrQry, StrCodes, Query As String, StrMod(), StrCod() As String, i, j, Count, Count1, k As Int16, Dt As New System.Data.DataTable, Dv As System.Data.DataView, DvTemp As System.Data.DataView
            Dim DtModules As New System.Data.DataTable
            Dim dtTemp As DataTable
            Dim StrPath As String
            Dim grpCode As String
            Dim strGrpCode As String
            Dim StrQueryTemp As String
            Dim rwMenu As TableRow, clMenu As TableCell
            Dim rwMenu1 As TableRow, clMenu1 As TableCell
            strGrpCode = Request.QueryString.Item("GrpCode")
            StrCodes = " Select isnull(Codes,'') from WebUsers Where UserID In (Select Group_ID From WebUsers Where UserID = '" & eHRMS.Net.MdlHRMS.Encrypt(Session("LoginUser").UserId, "+") & "')"
            StrQuery = " Select Modules from WebUsers Where UserID In (Select Group_ID From WebUsers Where UserID = '" & eHRMS.Net.MdlHRMS.Encrypt(Session("LoginUser").UserId, "+") & "')"
            StrQuery = Session("DalObj").ExecuteCommand(StrQuery, , DAL.DataLayer.ExecutionType.ExecuteScalar)
            StrCodes = Session("DalObj").ExecuteCommand(StrCodes, , DAL.DataLayer.ExecutionType.ExecuteScalar)

            'StrQuery = Replace(StrQuery, "-S", "")
            'StrQuery = Replace(StrQuery, "-V", "")
            StrMod = Split(StrQuery, ",")
            StrCod = Split(StrCodes, ",")
            If IsNothing(StrMod) And IsNothing(StrCod) Then Exit Sub
            'Session("DalObj").GetSqlDataTable(DtModules, " Select * from WebModules Where MODULE_CODE In (" & StrQuery & ") Order By MODULE_GRP, OrderNo")
            'Dv = New System.Data.DataView(DtModules)
            'DvTemp = New System.Data.DataView(DtModules)
            'Response.Write("Select * from WebModules Where MODULE_GRP = '" & strGrpCode & "' AND MODULE_CODE In (" & StrQuery & ") Order By Module_Code")
            Try
                StrQueryTemp = Replace(StrQuery, "-S", "")
                StrQueryTemp = Replace(StrQueryTemp, "-V", "")

                If Session("LoginUser").UserId = "EDP" Then
                    If strGrpCode = "0" Then
                        Session("DalObj").GetSqlDataTable(Dt, " Select * from WebModules WHERE (Active=1 AND Shotrcut<>1 AND MODULE_GRP = '" & strGrpCode & "') OR Shotrcut=1 Order By OrderNo")
                        'Session("DalObj").GetSqlDataTable(Dt, " Select * from WebModules WHERE (Active=1 AND Shotrcut<>1 AND MODULE_GRP = '" & strGrpCode & "') OR Shotrcut=1 Order By Module_Code")
                    Else
                        Session("DalObj").GetSqlDataTable(Dt, " Select * from WebModules WHERE (Active=1 AND Shotrcut<>1 AND MODULE_GRP = '" & strGrpCode & "' ) OR Shotrcut=1 Order By OrderNo")
                    End If
                Else
                    If strGrpCode = "0" Then
                        ' WORK Ravi ON 23 NON
                        Dim str As String = "Select * from WebModules WHERE MODULE_CODE IN (SELECT  MODULE_CODE FROM (Select * from WebModules where (Active=1 AND Shotrcut<>1 AND MODULE_GRP = '" & strGrpCode & "' AND MODULE_CODE IN (SELECT MODULE_GRP FROM WEBMODULES WHERE  MODULE_CODE IN( SELECT MODULE_GRP FROM WEBMODULES WHERE MODULE_CODE IN (" & StrQueryTemp & ")))) OR Shotrcut=1 "
                        str &= "union all "
                        str &= " Select * from WebModules WHERE (Active=1 AND Shotrcut<>1 AND MODULE_GRP = '" & strGrpCode & "' AND MODULE_CODE IN ( SELECT MODULE_GRP FROM WEBMODULES  WHERE MODULE_CODE IN (" & StrQueryTemp & "))) OR Shotrcut=1 )TMP) Order By OrderNo "
                        Session("DalObj").GetSqlDataTable(Dt, str)
                        'Session("DalObj").GetSqlDataTable(Dt, " Select * from WebModules WHERE (Active=1 AND Shotrcut<>1 AND MODULE_GRP = '" & strGrpCode & "' AND MODULE_CODE IN ( SELECT MODULE_GRP FROM WEBMODULES WHERE MODULE_CODE IN (" & StrQueryTemp & "))) OR Shotrcut=1 Order By OrderNo")
                        'Session("DalObj").GetSqlDataTable(Dt, " Select * from WebModules WHERE (Active=1 AND Shotrcut<>1 AND MODULE_GRP = '" & strGrpCode & "') OR Shotrcut=1 Order By Module_Code")
                    Else
                        Dim haschild, modulegrp, mdldesc As String
                        Dim strsql As String
                        strsql = "select haschild from webmodules where module_code='" & strGrpCode & "'"
                        haschild = Session("DalObj").ExecuteCommand(strsql, DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar)
                        modulegrp = Session("DalObj").ExecuteCommand("select module_grp from webmodules where module_code='" & strGrpCode & "'", DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar)
                        mdldesc = Session("DalObj").ExecuteCommand("select MODULE_DESC from webmodules where module_code='" & strGrpCode & "'", DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar)
                        ' If haschild = "1" And modulegrp = "0" And (mdldesc = "Payroll" Or mdldesc = "Employee") Then
                        If haschild = "1" And modulegrp = "0" Then
                            If mdldesc = "Payroll" Then
                                ' Dim str1 As String = "Select * from WebModules WHERE (Active=1 AND Shotrcut<>1 AND MODULE_GRP = '" & strGrpCode & "' AND MODULE_CODE IN ( SELECT MODULE_GRP FROM WEBMODULES WHERE MODULE_CODE IN (" & StrQueryTemp & "))) OR Shotrcut=1 Order By OrderNo"
                                Session("DalObj").GetSqlDataTable(Dt, " Select * from WebModules WHERE (Active=1 AND Shotrcut<>1 AND MODULE_GRP = '" & strGrpCode & "' AND MODULE_CODE IN ( SELECT MODULE_GRP FROM WEBMODULES WHERE MODULE_CODE IN (" & StrQueryTemp & "))) OR Shotrcut=1 Order By OrderNo")
                            Else
                                Dim str As String = "SELECT * FROM WEBMODULES  WHERE MODULE_CODE IN (SELECT MODULE_CODE FROM ( Select * from WebModules WHERE (Active=1 AND Shotrcut<>1 AND MODULE_GRP = '" & strGrpCode & "' AND MODULE_CODE In (" & StrQueryTemp & ")) OR Shotrcut=1 "
                                str &= " UNION ALL"
                                str &= " Select * from WebModules WHERE (Active=1 AND Shotrcut<>1 AND MODULE_GRP = '" & strGrpCode & "' AND MODULE_CODE In ( SELECT MODULE_GRP FROM WEBMODULES WHERE MODULE_CODE IN(" & StrQueryTemp & "))) OR Shotrcut=1 )TMP) Order By OrderNo "
                                Session("DalObj").GetSqlDataTable(Dt, str)
                            End If

                        Else
                            ' Dim str2 As String = "Select * from WebModules WHERE (Active=1 AND Shotrcut<>1 AND MODULE_GRP = '" & strGrpCode & "' AND MODULE_CODE In (" & StrQueryTemp & ")) OR Shotrcut=1 Order By OrderNo"
                            Session("DalObj").GetSqlDataTable(Dt, " Select * from WebModules WHERE (Active=1 AND Shotrcut<>1 AND MODULE_GRP = '" & strGrpCode & "' AND MODULE_CODE In (" & StrQueryTemp & ")) OR Shotrcut=1 Order By OrderNo")


                        End If

                        ' Dim str1 As String = "Select * from WebModules WHERE (Active=1 AND Shotrcut<>1 AND MODULE_GRP = '" & strGrpCode & "' AND MODULE_CODE In (" & StrQueryTemp & ")) OR Shotrcut=1 Order By OrderNo"
                        ' Session("DalObj").GetSqlDataTable(Dt, " Select * from WebModules WHERE (Active=1 AND Shotrcut<>1 AND MODULE_GRP = '" & strGrpCode & "' AND MODULE_CODE In (" & StrQueryTemp & ")) OR Shotrcut=1 Order By OrderNo")
                    End If
                End If
                Count = 1
                If Dt.Rows.Count > 0 Then
                    rwMenu = New TableRow
                    For i = 0 To Dt.Rows.Count - 1
                        clMenu = New TableCell
                        Dim Href As String
                        Href = ""
                        If InStr(Chk(Dt.Rows(i).Item("Module_Form")), "?") = 0 Then
                            Href = Chk(Dt.Rows(i).Item("Module_Form")) & "?SrNo=" & Dt.Rows(i).Item("Module_Code")
                        Else
                            Href = Replace(Chk(Dt.Rows(i).Item("Module_Form")), "?", "?SrNo=" & Dt.Rows(i).Item("Module_Code") & "&")
                        End If

                        If Chk(Dt.Rows(i).Item("Module_Form")) = "" Then
                            clMenu.Text = "<A Href=menus.aspx?GrpCode=" & Dt.Rows(i).Item("Module_Code") & " onclick=""LoadBlank""><Font size=2><b>" & Mid(Dt.Rows(i).Item("Module_Desc"), 1, 1) & "</b></Font>" & Mid(Dt.Rows(i).Item("Module_Desc"), 2, Len(Dt.Rows(i).Item("Module_Desc")) - 1) & "</A>"
                        ElseIf Dt.Rows(i).Item("Shotrcut") = "1" Then
                            clMenu.Text = "<A Href=" & Href & " Target=" & Dt.Rows(i).Item("Helptext") & "><Font size=2><b>" & Mid(Dt.Rows(i).Item("Module_Desc"), 1, 1) & "</b></Font>" & Mid(Dt.Rows(i).Item("Module_Desc"), 2, Len(Dt.Rows(i).Item("Module_Desc")) - 1) & "</A>"
                        Else
                            clMenu.Text = "<A Href=" & Href & " Target=" & Dt.Rows(i).Item("Helptext") & "><Font size=2><b>" & Mid(Dt.Rows(i).Item("Module_Desc"), 1, 1) & "</b></Font>" & Mid(Dt.Rows(i).Item("Module_Desc"), 2, Len(Dt.Rows(i).Item("Module_Desc")) - 1) & "</A>"
                        End If

                        rwMenu.Cells.Add(clMenu)
                    Next
                    'tblMenu.Rows.Add(rwMenu)
                    tblMenu.CellPadding = 3
                    tblMenu.CellSpacing = 3
                End If

                dtTemp = New DataTable
                Session("DalObj").GetSqlDataTable(dtTemp, "SELECT * FROM WebModules WHERE Module_Code = '" & strGrpCode & "' ORDER BY OrderNo")
                If dtTemp.Rows.Count > 0 Then
                    Do While dtTemp.Rows.Count > 0
                        If Chk(dtTemp.Rows(0).Item("Module_Form")) = "" Then
                            StrPath = "<A Href=menus.aspx?GrpCode=" & dtTemp.Rows(0).Item("Module_Code") & " onclick=""LoadBlank""><b>" & dtTemp.Rows(0).Item("Module_Desc") & "</b></A>" & IIf(Trim(StrPath) = "", "", "-->") & StrPath
                        Else
                            StrPath = "<A Href=" & dtTemp.Rows(0).Item("Module_Form") & " Target=" & Dt.Rows(i).Item("Helptext") & " onclick=""LoadBlank"">" & dtTemp.Rows(0).Item("Module_Desc") & "</A>" & IIf(Trim(StrPath) = "", "", "-->") & StrPath
                        End If
                        grpCode = dtTemp.Rows(0).Item("Module_Grp")
                        dtTemp = Nothing
                        dtTemp = New DataTable
                        Session("DalObj").GetSqlDataTable(dtTemp, "SELECT * FROM WebModules WHERE Module_Code = '" & grpCode & "' ORDER BY OrderNo")
                        If dtTemp.Rows.Count = 0 Then
                            Exit Do
                        End If
                    Loop
                End If

                'By Ravi 25 nov 2006
                ' Find company name 
                Dim compname As String
                compname = Chk(Session("DalObj").ExecuteCommand("select comp_name from compmast", DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar))
                '--------------------------------------
                Dim clMenu2 As TableCell
                clMenu2 = New TableCell
                clMenu2.Font.Bold = True
                clMenu2.Font.Size = FontUnit.Point(8)
                clMenu2.ForeColor = Color.FromName("#003366")
                clMenu2.HorizontalAlign = HorizontalAlign.Left
                clMenu2.Text = " User : " & Session("LoginUser").UserName & " (" & compname & ")"
                clMenu2.ColumnSpan = 2

                rwMenu1 = New TableRow
                clMenu1 = New TableCell
                clMenu1.Text = StrPath
                clMenu1.ColumnSpan = i - 2
                rwMenu1.Cells.Add(clMenu1)
                rwMenu1.Cells.Add(clMenu2)
                tblMenu.Rows.Add(rwMenu1)
                tblMenu.Rows.Add(rwMenu)
                'tblMenu.Attributes.Add("border", "1")
                'tblMenu.Width = Unit.Percentage(100)

            Catch ex As Exception
                Dim ERR As String
                ERR = ex.Message

            End Try

        End If
    End Sub

    Function FormatText(ByVal Str As String)
        If Trim(Str) <> "" Then
            Dim sChar As Char
            sChar = UCase(Mid(Str, 1, 1))
            Str = sChar & Mid(Str, 2, Len(Str) - 1)
        End If
    End Function
    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub

End Class
