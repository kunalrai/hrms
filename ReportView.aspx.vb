Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web.Design
Imports System.IO
Partial Class ReportView
    Inherits System.Web.UI.Page
    Private ReportFileName As String
    Private strFormula As String
    Private SelectionFormula As String
    Private OrderBy As String
    Private totNumGrp As Integer
    Private QryType As String
    Private ParameterVal As String
    Private oRpt As ReportDocument
    Protected WithEvents Link As System.Web.UI.WebControls.HyperLink
    Private IsPrint As Boolean
    Private Param() As String
    Private IsParam As Boolean
    Private RepExpOpts As ReportExportOptions

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
            Dim RepVars As ReportVars
            ''CrRep.DisplayGroupTree = False
            ''CrRep.DisplayToolbar = True
            If Not IsNothing(Session("RepVars")) Then
                RepVars = Session("RepVars")
                ReportFileName = RepVars.ReportFileName
                strFormula = RepVars.strFormula
                SelectionFormula = RepVars.SelectionFormula
                OrderBy = RepVars.OrderBy
                totNumGrp = RepVars.totNumGrp
                QryType = RepVars.QryType
                IsPrint = RepVars.IsPrint
                Param = RepVars.Param
                IsParam = RepVars.IsParam
                RepExpOpts = RepVars.ExportOpts
            End If
            If IsParam Then
                CreateReport(Param, True)
            Else
                CreateReport()
            End If
        Catch ex As Exception
            SetMsg(LblPages, "Page Load" & ex.Message)
        End Try
    End Sub


    Public Sub CreateReport(Optional ByVal arParams As Array = Nothing, Optional ByVal DoParams As Boolean = False)
        Dim oSubRpt As New ReportDocument
        Dim Counter As Integer, crSections As Sections
        Dim crSection As Section, crReportObjects As ReportObjects
        Dim crReportObject As ReportObject, crSubreportObject As SubreportObject
        Dim crDatabase As Database, crTables As Tables
        Dim crTable As Table, crLogOnInfo As TableLogOnInfo
        Dim crConnInfo As New ConnectionInfo
        Dim crFFD As FormulaFieldDefinitions
        Dim crParameterValues As ParameterValues
        Dim crParameterDiscreteValue As ParameterDiscreteValue
        Dim crParameterRangeValue As ParameterRangeValue
        Dim crParameterFieldDefinitions As ParameterFieldDefinitions
        Dim crParameterFieldDefinition As ParameterFieldDefinition
        Dim crParameterFieldDefinition2 As ParameterFieldDefinition
        Dim ChartObj As ChartObject
        Dim strFile As String, fi As FileInfo, tstr As String, sPath As String


        Dim sReportPath As String = ReportFileName
        'Dim sReportPath As String = "http://xeon/ehrms.net/FLI/payslp.rpt"

        Dim pos As Integer
        Try
            '*************** Load Report 
            oRpt = New ReportDocument
            'oRpt = New Report.SALREG

            oRpt.Load(sReportPath)

            '*************** Passing the Values to the Formulas in the Report
            Dim sFArray, sFV
            Dim i As Int16
            If Trim(strFormula) <> "" Then
                sFArray = Split(strFormula, "|")
                For i = LBound(sFArray) To UBound(sFArray)
                    sFV = Split(sFArray(i), "^")
                    oRpt.DataDefinition.FormulaFields.Item(Trim(sFV(0))).Text = sFV(1)
                    'oRpt.DataDefinition.ParameterFields  
                Next
            End If


            '*************** Setting up Grouping Option For the Report
            If OrderBy <> "" Then
                SetGroupFormula(OrderBy, 1, totNumGrp, QryType)
            End If

            '*************** Setting up Record Selection Formula For the Report
            oRpt.RecordSelectionFormula = SelectionFormula

            '*************** Log on to SQL server 
            '*************** Report code starts here
            '*************** Set the database and the tables objects to the main report 'oRpt'
            crDatabase = oRpt.Database
            crTables = crDatabase.Tables

            '*************** Loop through each table and set the connection info 
            '*************** Pess the connection info to the logoninfo object then apply the 
            '*************** LogonInFo to the main report
            ''''''For Each crTable In crTables
            ''''''    With crConnInfo
            ''''''        .ServerName = Session("LoginUser").DSNName
            ''''''        .DatabaseName = Session("LoginUser").DBName
            ''''''        .UserID = Session("LoginUser").DBUser
            ''''''        .Password = Session("LoginUser").DBPassword
            ''''''        crTable.LogOnInfo.ConnectionInfo()
            ''''''    End With
            ''''''    crLogOnInfo = crTable.LogOnInfo
            ''''''    crLogOnInfo.ConnectionInfo = crConnInfo
            ''''''    crTable.ApplyLogOnInfo(crLogOnInfo)
            ''''''Next


            '***Suraj***********
            Dim logonInfo As New TableLogOnInfo
            Dim table As Table

            ' Set the logon information for each table.
            For Each table In oRpt.Database.Tables
                ' Get the TableLogOnInfo object.
                logonInfo = table.LogOnInfo
                ' Set the server or ODBC data source name, database name, 
                ' user ID, and password.
                logonInfo.ConnectionInfo.ServerName = Session("LoginUser").DSNName
                logonInfo.ConnectionInfo.DatabaseName = Session("LoginUser").DBName
                logonInfo.ConnectionInfo.UserID = Session("LoginUser").DBUser
                logonInfo.ConnectionInfo.Password = Session("LoginUser").DBPassword
                ' Apply the connection information to the table.
                table.ApplyLogOnInfo(logonInfo)
                table.Location = Session("LoginUser").DBName & ".dbo." & table.Name
                'table.
                'table.Location = table.Name
            Next table


            'CType(Session("LoginUser"), DAL.DataLayer.Users).DBServerName
            'oRpt.SetDatabaseLogon(  

            '*************** Set the Sections Collection with Report Sections
            crSections = oRpt.ReportDefinition.Sections

            '*************** Loop through each section and find all the report objects
            '*************** Loop through all the report objects to find all subreport objects, then set the 
            '*************** Logoninfo to the subreport

            For Each crSection In crSections
                crReportObjects = crSection.ReportObjects
                For Each crReportObject In crReportObjects
                    If crReportObject.Kind = ReportObjectKind.SubreportObject Then
                        '*************** If you find a subreport, typecast the reportobject to a subreport object
                        crSubreportObject = CType(crReportObject, SubreportObject)
                        '*************** Open the subreport
                        oSubRpt = crSubreportObject.OpenSubreport(crSubreportObject.SubreportName)
                        '''''''crDatabase = oSubRpt.Database
                        '''''''crTables = crDatabase.Tables
                        ''''''''*************** Loop through each table and set the connection info 
                        ''''''''*************** Pess the connection info to the logoninfo object then apply the 
                        ''''''''*************** LogonInfo to the subreport
                        '''''''For Each crTable In crTables
                        '''''''    With crConnInfo
                        '''''''        .ServerName = Session("LoginUser").DSNName
                        '''''''        .DatabaseName = Session("LoginUser").DBName
                        '''''''        .UserID = Session("LoginUser").DBUser
                        '''''''        .Password = Session("LoginUser").DBPassword
                        '''''''    End With
                        '''''''    crLogOnInfo = crTable.LogOnInfo
                        '''''''    crLogOnInfo.ConnectionInfo = crConnInfo
                        '''''''    crTable.ApplyLogOnInfo(crLogOnInfo)
                        '''''''Next
                        For Each table In oSubRpt.Database.Tables
                            ' Get the TableLogOnInfo object.
                            logonInfo = table.LogOnInfo
                            ' Set the server or ODBC data source name, database name, 
                            ' user ID, and password.
                            logonInfo.ConnectionInfo.ServerName = Session("LoginUser").DSNName
                            logonInfo.ConnectionInfo.DatabaseName = Session("LoginUser").DBName
                            logonInfo.ConnectionInfo.UserID = Session("LoginUser").DBUser
                            logonInfo.ConnectionInfo.Password = Session("LoginUser").DBPassword
                            ' Apply the connection information to the table.                            
                            table.ApplyLogOnInfo(logonInfo)
                            table.Location = Session("LoginUser").DBName & ".dbo." & table.Name

                            'table.Location = table.Name
                        Next table
                    End If
                Next
            Next
            '*************** Set the parameters
            If DoParams Then
                '*************** Get the Collection of Parameters From the Report
                Dim crFFDTmp As FormulaFieldDefinition
                crFFD = oRpt.DataDefinition.FormulaFields()

                crParameterFieldDefinitions = oRpt.DataDefinition.ParameterFields()
                Dim j As Int16
                If IsNothing(Param) Then Exit Sub
                For Counter = 1 To UBound(Param) - 1
                    If Not IsNothing(Param(Counter)) Then
                        j = InStr(Param(Counter), "=")

                        If Not IsNothing(Param(Counter)) Then
                            crFFDTmp = crFFD.Item("" & Mid(Param(Counter), 1, j - 1))
                            crFFDTmp.Text = Mid(Param(Counter), j + 1, Len(Param(Counter)) - j)
                        End If

                    End If
                Next
            End If

            ''''oRpt.Refresh()
            ' If IsPrint = True Then
            'oRpt.PrintToPrinter(1, False, 1, 1)
            'End If

            'CrRep. 
            '***********Mukesh's Creation to speedup the report
            Dim fname As String
            fname = Request.ServerVariables.Item("APPL_PHYSICAL_PATH") & "Export\" & Format(Date.Today, "ddMMyy") & "_" & Format(Now.Hour, "00") & Format(Now.Minute, "00") & Format(Now.Second, "00")
            Select Case RepExpOpts
                Case MdlHRMS.ReportExportOptions.Excel
                    fname = fname & ".XLS"
                    oRpt.ExportToDisk(ExportFormatType.Excel, fname)
                Case MdlHRMS.ReportExportOptions.PDF
                    fname = fname & ".PDF"
                    oRpt.ExportToDisk(ExportFormatType.PortableDocFormat, fname)
                Case MdlHRMS.ReportExportOptions.Word
                    fname = fname & ".DOC"
                    oRpt.ExportToDisk(ExportFormatType.WordForWindows, fname)
            End Select
            RegisterStartupScript("Rep", "<Script language=javascript>window.open('Export/" & Dir(fname) & "',""_self"")</Script>")
            'Server.Transfer(fname, False)

            'LblPages.Text = "<Input visible=false type=text ID='RepLink' value='Export/" & Dir(fname) & "'>"
            '***************************
            'CrRep.LogOnInfo.Item(0).ConnectionInfo.ServerName = "HRMS_INFO"
        Catch ex As System.Exception
            SetMsg(LblPages, "CryPrinter.CreateReport " & ex.Message)

        Finally
            Erase arParams
        End Try
    End Sub

    ''Public Sub CreateReport(Optional ByVal arParams As Array = Nothing, Optional ByVal DoParams As Boolean = False)
    ''    Dim oSubRpt As New ReportDocument
    ''    Dim Counter As Integer, crSections As Sections
    ''    Dim crSection As Section, crReportObjects As ReportObjects
    ''    Dim crReportObject As ReportObject, crSubreportObject As SubreportObject
    ''    Dim crDatabase As Database, crTables As Tables
    ''    Dim crTable As Table, crLogOnInfo As TableLogOnInfo
    ''    Dim crConnInfo As New ConnectionInfo
    ''    Dim crFFD As FormulaFieldDefinitions
    ''    Dim crParameterValues As ParameterValues
    ''    Dim crParameterDiscreteValue As ParameterDiscreteValue
    ''    Dim crParameterRangeValue As ParameterRangeValue
    ''    Dim crParameterFieldDefinitions As ParameterFieldDefinitions
    ''    Dim crParameterFieldDefinition As ParameterFieldDefinition
    ''    Dim crParameterFieldDefinition2 As ParameterFieldDefinition
    ''    Dim ChartObj As ChartObject
    ''    Dim strFile As String, fi As FileInfo, tstr As String, sPath As String


    ''    Dim sReportPath As String = ReportFileName
    ''    'Dim sReportPath As String = "http://xeon/ehrms.net/FLI/payslp.rpt"

    ''    Dim pos As Integer
    ''    Try
    ''        '*************** Load Report 
    ''        oRpt = New ReportDocument
    ''        oRpt.Load(sReportPath)

    ''        '*************** Passing the Values to the Formulas in the Report
    ''        Dim sFArray, sFV
    ''        Dim i As Int16
    ''        If Trim(strFormula) <> "" Then
    ''            sFArray = Split(strFormula, "|")
    ''            For i = LBound(sFArray) To UBound(sFArray)
    ''                sFV = Split(sFArray(i), "^")
    ''                oRpt.DataDefinition.FormulaFields.Item(Trim(sFV(0))).Text = sFV(1)
    ''                'oRpt.DataDefinition.ParameterFields  
    ''            Next
    ''        End If


    ''        '*************** Setting up Grouping Option For the Report
    ''        If OrderBy <> "" Then
    ''            SetGroupFormula(OrderBy, 1, totNumGrp, QryType)
    ''        End If

    ''        '*************** Setting up Record Selection Formula For the Report
    ''        oRpt.RecordSelectionFormula = SelectionFormula

    ''        '*************** Log on to SQL server 
    ''        '*************** Report code starts here
    ''        '*************** Set the database and the tables objects to the main report 'oRpt'
    ''        crDatabase = oRpt.Database
    ''        crTables = crDatabase.Tables

    ''        '*************** Loop through each table and set the connection info 
    ''        '*************** Pess the connection info to the logoninfo object then apply the 
    ''        '*************** LogonInFo to the main report
    ''        ''''''For Each crTable In crTables
    ''        ''''''    With crConnInfo
    ''        ''''''        .ServerName = Session("LoginUser").DSNName
    ''        ''''''        .DatabaseName = Session("LoginUser").DBName
    ''        ''''''        .UserID = Session("LoginUser").DBUser
    ''        ''''''        .Password = Session("LoginUser").DBPassword
    ''        ''''''        crTable.LogOnInfo.ConnectionInfo()
    ''        ''''''    End With
    ''        ''''''    crLogOnInfo = crTable.LogOnInfo
    ''        ''''''    crLogOnInfo.ConnectionInfo = crConnInfo
    ''        ''''''    crTable.ApplyLogOnInfo(crLogOnInfo)
    ''        ''''''Next


    ''        '***Suraj***********
    ''        Dim logonInfo As New TableLogOnInfo
    ''        Dim table As table

    ''        ' Set the logon information for each table.
    ''        For Each table In oRpt.Database.Tables
    ''            ' Get the TableLogOnInfo object.
    ''            logonInfo = table.LogOnInfo
    ''            ' Set the server or ODBC data source name, database name, 
    ''            ' user ID, and password.
    ''            logonInfo.ConnectionInfo.ServerName = Session("LoginUser").DSNName
    ''            logonInfo.ConnectionInfo.DatabaseName = Session("LoginUser").DBName
    ''            logonInfo.ConnectionInfo.UserID = Session("LoginUser").DBUser
    ''            logonInfo.ConnectionInfo.Password = Session("LoginUser").DBPassword
    ''            ' Apply the connection information to the table.
    ''            table.ApplyLogOnInfo(logonInfo)
    ''            'table.Location = Session("LoginUser").DBName & ".dbo." & table.Name
    ''            'table.
    ''            table.Location = table.Name
    ''        Next table


    ''        'CType(Session("LoginUser"), DAL.DataLayer.Users).DBServerName
    ''        'oRpt.SetDatabaseLogon(  

    ''        '*************** Set the Sections Collection with Report Sections
    ''        crSections = oRpt.ReportDefinition.Sections

    ''        '*************** Loop through each section and find all the report objects
    ''        '*************** Loop through all the report objects to find all subreport objects, then set the 
    ''        '*************** Logoninfo to the subreport

    ''        For Each crSection In crSections
    ''            crReportObjects = crSection.ReportObjects
    ''            For Each crReportObject In crReportObjects
    ''                If crReportObject.Kind = ReportObjectKind.SubreportObject Then
    ''                    '*************** If you find a subreport, typecast the reportobject to a subreport object
    ''                    crSubreportObject = CType(crReportObject, SubreportObject)
    ''                    '*************** Open the subreport
    ''                    oSubRpt = crSubreportObject.OpenSubreport(crSubreportObject.SubreportName)
    ''                    '''''''crDatabase = oSubRpt.Database
    ''                    '''''''crTables = crDatabase.Tables
    ''                    ''''''''*************** Loop through each table and set the connection info 
    ''                    ''''''''*************** Pess the connection info to the logoninfo object then apply the 
    ''                    ''''''''*************** LogonInfo to the subreport
    ''                    '''''''For Each crTable In crTables
    ''                    '''''''    With crConnInfo
    ''                    '''''''        .ServerName = Session("LoginUser").DSNName
    ''                    '''''''        .DatabaseName = Session("LoginUser").DBName
    ''                    '''''''        .UserID = Session("LoginUser").DBUser
    ''                    '''''''        .Password = Session("LoginUser").DBPassword
    ''                    '''''''    End With
    ''                    '''''''    crLogOnInfo = crTable.LogOnInfo
    ''                    '''''''    crLogOnInfo.ConnectionInfo = crConnInfo
    ''                    '''''''    crTable.ApplyLogOnInfo(crLogOnInfo)
    ''                    '''''''Next
    ''                    For Each table In oSubRpt.Database.Tables
    ''                        ' Get the TableLogOnInfo object.
    ''                        logonInfo = table.LogOnInfo
    ''                        ' Set the server or ODBC data source name, database name, 
    ''                        ' user ID, and password.
    ''                        logonInfo.ConnectionInfo.ServerName = Session("LoginUser").DSNName
    ''                        logonInfo.ConnectionInfo.DatabaseName = Session("LoginUser").DBName
    ''                        logonInfo.ConnectionInfo.UserID = Session("LoginUser").DBUser
    ''                        logonInfo.ConnectionInfo.Password = Session("LoginUser").DBPassword
    ''                        ' Apply the connection information to the table.                            
    ''                        table.ApplyLogOnInfo(logonInfo)
    ''                        'table.Location = Session("LoginUser").DBName & ".dbo." & table.Name

    ''                        table.Location = table.Name
    ''                    Next table
    ''                End If
    ''            Next
    ''        Next
    ''        '*************** Set the parameters
    ''        If DoParams Then
    ''            '*************** Get the Collection of Parameters From the Report
    ''            Dim crFFDTmp As FormulaFieldDefinition
    ''            crFFD = oRpt.DataDefinition.FormulaFields()

    ''            crParameterFieldDefinitions = oRpt.DataDefinition.ParameterFields()
    ''            Dim j As Int16
    ''            If IsNothing(Param) Then Exit Sub
    ''            For Counter = 1 To UBound(Param) - 1
    ''                If Not IsNothing(Param(Counter)) Then
    ''                    j = InStr(Param(Counter), "=")

    ''                    If Not IsNothing(Param(Counter)) Then
    ''                        crFFDTmp = crFFD.Item("" & Mid(Param(Counter), 1, j - 1))
    ''                        crFFDTmp.Text = Mid(Param(Counter), j + 1, Len(Param(Counter)) - j)
    ''                    End If

    ''                End If
    ''            Next
    ''        End If

    ''        ''''oRpt.Refresh()
    ''        ' If IsPrint = True Then
    ''        'oRpt.PrintToPrinter(1, False, 1, 1)
    ''        'End If

    ''        'CrRep. 
    ''        '***********Mukesh's Creation to speedup the report
    ''        Dim fname As String
    ''        fname = Request.ServerVariables.Item("APPL_PHYSICAL_PATH") & "\Export\" & Format(Date.Today, "ddMMyy") & "_" & Format(Now.Hour, "00") & Format(Now.Minute, "00") & Format(Now.Second, "00")
    ''        fname = fname & ".PDF"
    ''        'orpt.ExportOptions = 
    ''        oRpt.ExportToDisk(ExportFormatType.PortableDocFormat, fname)
    ''        RegisterStartupScript("Rep", "<Script language=javascript>document.location='Export/" & Dir(fname) & "'</Script>")
    ''        'Server.Transfer(fname, False)

    ''        'LblPages.Text = "<Input visible=false type=text ID='RepLink' value='Export/" & Dir(fname) & "'>"
    ''        '***************************
    ''        'CrRep.LogOnInfo.Item(0).ConnectionInfo.ServerName = "HRMS_INFO"
    ''    Catch ex As System.Exception
    ''        SetMsg(LblPages, "CryPrinter.CreateReport " & ex.Message)

    ''    Finally
    ''        Erase arParams
    ''    End Try
    ''End Sub

    Public Function SetGroupFormula(ByVal strOrders As String, ByVal FormulaNo As Integer, Optional ByVal argGroups As Integer = 0, Optional ByVal QryType As String = "") As Integer
        Dim rsPaySetup As New DataTable
        Try
            Dim GrpArr() As String
            GrpArr = Split(strOrders, "+")

            Dim i, MaxNum As Integer
            If argGroups < UBound(GrpArr) Then
                MaxNum = argGroups
            Else
                MaxNum = UBound(GrpArr)
            End If

            ''************* Setting the Value to Blank
            If Not oRpt.DataDefinition.FormulaFields.Item("G1") Is Nothing Then oRpt.DataDefinition.FormulaFields.Item("G1").Text = ""
            If Not oRpt.DataDefinition.FormulaFields.Item("G2") Is Nothing Then oRpt.DataDefinition.FormulaFields.Item("G2").Text = ""
            If Not oRpt.DataDefinition.FormulaFields.Item("G3") Is Nothing Then oRpt.DataDefinition.FormulaFields.Item("G3").Text = ""
            If Not oRpt.DataDefinition.FormulaFields.Item("GN1") Is Nothing Then oRpt.DataDefinition.FormulaFields.Item("GN1").Text = ""
            If Not oRpt.DataDefinition.FormulaFields.Item("GN2") Is Nothing Then oRpt.DataDefinition.FormulaFields.Item("GN2").Text = ""
            If Not oRpt.DataDefinition.FormulaFields.Item("GN3") Is Nothing Then oRpt.DataDefinition.FormulaFields.Item("GN3").Text = ""

            ''************* Passing the the Value to the Formulas
            For i = 0 To MaxNum
                If UCase(GrpArr(i)) <> "EMP_CODE" Then
                    Session("DalObj").GetSQlDataTable(rsPaySetup, "SELECT Field_Name, Print_Name, Fld_HrdHist FROM PaySetup WHERE Field_Name = '" & GrpArr(i) & "'")
                    If QryType = "H" And rsPaySetup.Rows(0).Item("Fld_HrdHist") = "Y" Then
                        oRpt.DataDefinition.FormulaFields.Item("G" & 3 - i).Text = "{HRDHISTQRY." & Replace(UCase(GrpArr(i)), "_CODE", "_NAME") & "}"
                    Else
                        oRpt.DataDefinition.FormulaFields.Item("G" & 3 - i).Text = "{HRDMASTQRY." & Replace(UCase(GrpArr(i)), "_CODE", "_NAME") & "}"
                    End If
                    oRpt.DataDefinition.FormulaFields.Item("GN" & 3 - i).Text = "'" & rsPaySetup.Rows(0).Item("Print_Name") & "'"
                End If
            Next

        Catch ex As Exception
            'SetMsg(LblPages, ex.Message & " : (SetGroupFormula) ")
        Finally
            rsPaySetup.Dispose()
            rsPaySetup = Nothing
        End Try
    End Function

    Private Sub CmdPage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdPage.Click
        Try
            'Dim D As New System.Drawing.Printing.PrinterSettings

            'If Val(TxtFrom.Text) <= Val(TxtTO.Text) Then
            '    Dim sad As String
            '    'oRpt.PrintOptions.PrinterName = "\\Kobra\Hp LaserJet 1020"
            '    'oRpt.PrintOptions.PrinterName = "\\Server\Server Printer"
            '    'oRpt.PrintOptions.PrinterDuplex = PrinterDuplex.Default
            '    'oRpt.PrintOptions.PrinterName = D.InstalledPrinters.Item(0)
            '    'oRpt.PrintToPrinter(1, True, Val(TxtFrom.Text), Val(TxtTO.Text))
            'End If 

            Dim fName As String

            fName = Request.ServerVariables.Item("APPL_PHYSICAL_PATH") & "Export\" & Format(Date.Today, "ddMMyy") & "_" & Format(Now.Hour, "00") & Format(Now.Minute, "00") & Format(Now.Second, "00")

            fName = fName & ".DOC"
            oRpt.ExportToDisk(ExportFormatType.RichText, fName)
            TxtPrint.Text = "Export/" & Dir(fName)
            Response.Redirect(TxtPrint.Text)
            'Shell(fName, AppWinStyle.MaximizedFocus)
            'Shell("http://" & Request.ServerVariables("SERVER_NAME") & "/eHRMS.NET/Export/" & Dir(fName), AppWinStyle.MaximizedFocus)
        Catch ex As Exception
            SetMsg(LblPages, ex.Message & " : " & " Print" & ex.Source)
        End Try
    End Sub

#Region " Function For Export "



    Private Sub CmdExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdExport.Click
        Try

            'XPort()
            Dim fName As String

            fName = "http://" & Request.ServerVariables("SERVER_NAME") & "/Export/" & Format(Date.Today, "ddMMyy") & "_" & Format(Now.Hour, "00") & Format(Now.Minute, "00") & Format(Now.Second, "00")
            fName = Request.ServerVariables.Item("APPL_PHYSICAL_PATH") & "\Export\" & Format(Date.Today, "ddMMyy") & "_" & Format(Now.Hour, "00") & Format(Now.Minute, "00") & Format(Now.Second, "00")

            If CmbExport.SelectedIndex = 1 Then
                fName = fName & ".PDF"
                oRpt.ExportToDisk(ExportFormatType.PortableDocFormat, fName)
            ElseIf CmbExport.SelectedIndex = 2 Then
                fName = fName & ".DOC"
                oRpt.ExportToDisk(ExportFormatType.RichText, fName)
            Else
                fName = fName & ".XLS"
                oRpt.ExportToDisk(ExportFormatType.Excel, fName)
            End If
            LblPages.Text = "<a href='Export/" & Dir(fName) & "' Target=Blank>Click Here to download file--(" & Dir(fName, FileAttribute.Normal) & ")</A>"
            'LblPages.Text = "<a href='http://" & Request.ServerVariables("SERVER_NAME") & "/eHRMS.NET/Export/" & Dir(fName) & "' Target=Blank>Click Here to download file--(" & Dir(fName, FileAttribute.Normal) & ")</A>"
        Catch ex As Exception
            SetMsg(LblPages, ex.Message & " : " & " Export")
        End Try
    End Sub

#End Region

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub

End Class
