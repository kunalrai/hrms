Partial Class frmHTMLReports
    Inherits System.Web.UI.Page
    'Dim dtable As DataTable

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
    Dim DALOBJ As New DAL.DataLayer
    Public Shared argStrSql As String
    Public Shared argStrHTML As String
    Public Shared argDataTable As DataTable

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            If Server.GetLastError Is Nothing Then LblErrMsg.Text = ""
            If argStrSql = "EXECUTE" Then
                SetMsg(LblErrMsg, " Command Executed Success Fully.")
            End If
            Text()
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message)
            'FRMREP = Nothing
        End Try
    End Sub
    Private Function DtFormat(ByVal argStr As DateTime) As String
        If argStr <> CDate("01/01/1900") Then
            DtFormat = Format(argStr, "dd/MMM/yyyy")
        Else
            DtFormat = ""
        End If
    End Function

    Sub Text()

        Dim StrQry As String
        StrQry = "<table width=1000 style =""MARGIN-TOP: 50px"">"
        StrQry = StrQry & "<tr bgColor='#e7e8e9'>"
        StrQry = StrQry & "<td width=500><FONT face=Verdana, Arial size=2><asp:label id=Label1 runat=server Width=300px><b>Reports .....<b></asp:label></Font></td>"
        StrQry = StrQry & "<td align=right width=250><FONT face=Verdana, Arial size=2><A Style=""CURSOR: hand"" OnClick='window.close()'><b>Close Window</b></A></FONT></td>"
        StrQry = StrQry & "</tr>"

        StrQry = StrQry & "<tr>"
        StrQry = StrQry & "<td colSpan=2>"
        StrQry = StrQry & "<HR width=1000 color=navy noShade SIZE=2>"
        StrQry = StrQry & "</tr>"
        StrQry = StrQry & "</table>"
        StrQry = StrQry & "<BR><BR>"

        If argStrHTML <> "" Then
            StrQry = StrQry & " " & argStrHTML
        Else
            Dim DSREP As New DataSet
            Dim CCOUNT As Int16
            Dim RCOUNT As Int16
            Dim Dt As New DataTable
            If IsNothing(argDataTable) Then
                DSREP.Tables.Add(Dt)

                Dim Col As New DataColumn
                Col.ColumnName = "SrNo"
                Col.AutoIncrement = True
                Col.AutoIncrementSeed = 1
                Col.AutoIncrementStep = 1
                Col.Caption = "SrNo"

                DSREP.Tables(0).Columns.Add(Col)
                CType(Session("DALOBJ"), DAL.DataLayer).GetSqlDataTable(DSREP.Tables(0), argStrSql)
            Else
                Dt = argDataTable
                DSREP.Tables.Add(argDataTable)
                'DSREP.Tables(0) = argDataTable
            End If



            Dim ROWS As Int16 = DSREP.Tables(0).Rows.Count
            Dim COLS As Int16 = DSREP.Tables(0).Columns.Count
            StrQry = StrQry & "<table  width=1000 CELLSPACING=0 CELLPADDING=1 BORDER=1 BORDERCOLOR=BLACK>"
            StrQry = StrQry & "<TR>"
            For CCOUNT = 0 To COLS - 1
                StrQry = StrQry & "<TH>"
                StrQry = StrQry & DSREP.Tables(0).Columns.Item(CCOUNT).ColumnName()
                StrQry = StrQry & "</TH>"
            Next
            StrQry = StrQry & "</TR>"
            For RCOUNT = 0 To ROWS - 1
                StrQry = StrQry & "<TR>"
                For CCOUNT = 0 To COLS - 1
                    StrQry = StrQry & "<TD>&nbsp;"
                    If DSREP.Tables(0).Rows(RCOUNT).Item(CCOUNT).GetType().FullName = "System.DateTime" Then
                        StrQry = StrQry & DtFormat(DSREP.Tables(0).Rows(RCOUNT).Item(CCOUNT))
                    ElseIf DSREP.Tables(0).Rows(RCOUNT).Item(CCOUNT).GetType().FullName = "System.String" Then
                        StrQry = StrQry & Chk(DSREP.Tables(0).Rows(RCOUNT).Item(CCOUNT), , False)
                    Else
                        StrQry = StrQry & DSREP.Tables(0).Rows(RCOUNT).Item(CCOUNT)
                    End If

                    StrQry = StrQry & "</TD>"
                Next
                StrQry = StrQry & "</TR>"
            Next
            StrQry = StrQry & "</table>"
        End If
        ViewState("PayStr") = StrQry
        Response.Write(StrQry)
    End Sub

    Private Sub CmdExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdExport.Click
        Try
            Dim Sw As IO.StreamWriter
            Dim Str, Strq As String
            Str = Request.ServerVariables.Item("APPL_PHYSICAL_PATH") & "Export\" & "PayRollInput" & Format(Date.Today, "ddMMyy") & "_" & Format(Now.Hour, "00") & Format(Now.Minute, "00") & Format(Now.Second, "00")
            Str = Str & ".xls"
            Sw = New IO.StreamWriter(Str)
            Sw.Write(ViewState("PayStr"))
            Sw.Close()
            Strq = Mid(Request.ServerVariables.Item("PATH_INFO"), 2, Len(Request.ServerVariables.Item("PATH_INFO")) - 1)
            Strq = Left(Strq, InStr(Strq, "/") - 1)
            Strq = "http://" & Request.ServerVariables("SERVER_NAME") & "/" & Strq & "/Export/" & Dir(Str)
            'Strq = "http://" & Request.ServerVariables("SERVER_NAME") & "/eHRMS.NET/Export/" & Dir(Str)
            Response.Redirect(Strq)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub
End Class
