Imports System.IO
Partial Class FileSetup
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
    Dim dtval As New DataTable
    Dim i, j As Integer
    Dim flg As Boolean = True


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            FillComboBox_cmdGenerate()
        End If
    End Sub
    Sub FillComboBox_cmdGenerate()
        Try
            Session("BalObj").FillCombo(CmbFile, "SELECT OFileId,Ofiledesc FROM OUTPUTFILESETUP order by ofiledesc")
            '  Session("BalObj").FillCombo(cmbALocation, "Select Loc_Code, Loc_Name From LocMast Order by Loc_Name", True)
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : (FillComboBox)")
        End Try

    End Sub
    Private Sub cmdGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGenerate.Click

        Filegenerate()

        '        Dim dt As New DataTable
        '        Dim i, j, k, l As Integer
        '        Dim dtFname As New DataTable
        '        Dim strSql, qry, fileName, strpath, Dpath, Ftype As String

        '        Try

        '            qry = "SELECT top 1 Query FROM OUTPUTFILESETUP where oFileid='" & CmbFile.SelectedValue & "'"

        '            strSql = Session("DalObj").ExecuteCommand(qry, DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar)

        '            strSql = strSql.Replace("[FDATE]", "'" & Chk(Format(dtpPdate.DateValue, "yyyy-MM-dd")) & "'")
        '            'Response.Write(strSql)
        '            Session("DalObj").GetSqlDataTable(dt, strSql)


        '            qry = "SELECT top 1 * FROM OUTPUTFILESETUP where oFileid='" & CmbFile.SelectedValue & "'"
        '            Session("DalObj").GetSqlDataTable(dtFname, qry)


        '            Dim strsql1 As String

        '            'fileName = dtFname.Rows(0).Item("FileNamePrefix") & dtFname.Rows(0).Item("FileName") & Format(Date.Today, "ddMMMyyyy")
        '            fileName = dtFname.Rows(0).Item("FileNamePrefix") & dtFname.Rows(0).Item("FileName") & Format(Date.Today, dtFname.Rows(0).Item("FileNameSuffix"))
        '            strpath = HttpContext.Current.Request.ServerVariables("APPL_PHYSICAL_PATH")
        '            '*******
        '            'Dim fcou As String
        '            ' fcou = dt.Columns.Item(0).ColumnName



        '            '********
        '            ' Dim ObjStreamWriter As New StreamWriter(strpath & "\Files\" & fileName & ".csv")
        '            Dpath = dtFname.Rows(0).Item("DefaultPath")
        '            Ftype = "." & dtFname.Rows(0).Item("FileType")
        '            Dim ObjStreamWriter As New StreamWriter(strpath & Dpath & fileName & Ftype)
        '            'Find column name and formating it
        '            Dim dtcol As New DataTable

        '            Session("DalObj").GetSqlDataTable(dtcol, "select  fieldname,fieldformat from fieldformat where OFileid='" & CmbFile.SelectedValue & "' order by fieldname")


        '            For i = 0 To dt.Rows.Count - 1 Step 1
        '                For j = 0 To dt.Columns.Count - 1 Step 1
        '                    If dtcol.Rows.Count > 0 Then
        '                        While k < dtcol.Rows.Count

        '                            If dt.Columns.Item(j).ColumnName = dtcol.Rows(k).Item(0) Then
        '                                'Dim str As String = dt.Rows(i).Item(dt.Columns(j).ColumnName).ToString
        '                                ' Dim str1 As String = dtcol.Rows(k).Item(1).ToString
        '                                'strsql1 = strsql1 & Format(CDate(dt.Rows(i).Item(dt.Columns(j).ColumnName)), dtcol.Rows(k).Item(1)) & ";"
        '                                If IsDBNull(dt.Rows(i).Item(j)) Then
        '                                    strsql1 = strsql1 & dt.Rows(i).Item(dt.Columns(j).ColumnName).ToString & ";"
        '                                    GoTo qq
        '                                Else
        '                                    strsql1 = strsql1 & Format(dt.Rows(i).Item(j), dtcol.Rows(k).Item(1)).ToString & ";"
        '                                    GoTo qq
        '                                End If
        '                                'strsql1 = strsql1 & IIf(Chk(dt.Rows(i).Item(j)) = "", "", (Format(dt.Rows(i).Item(j), dtcol.Rows(k).Item(1)))).ToString & ";"
        '                                ' strsql1 = strsql1 & IIf(Chk(dt.Rows(i).Item(j)) = "", "", "KK").ToString & ";"
        '                                GoTo qq
        '                            End If
        '                            k = k + 1
        '                        End While
        '                        GoTo pp


        '                        'While l < dt.Rows.Count
        '                        '    While k < dtcol.Rows.Count
        '                        '        If dt.Columns.Item(l).ColumnName = dtcol.Columns.Item(k).ColumnName Then
        '                        '            strsql1 = strsql1 & Format(Chk(dt.Rows(i).Item(dt.Columns(j).ColumnName).ToString), dtcol.Rows(k).Item(1)) & ";"
        '                        '        End If
        '                        '        k = k + 1
        '                        '    End While

        '                        '    Dim str As String = dt.Columns.Item(j).ColumnName
        '                        '    Dim str1 As String = dtcol.Rows(k).Item(0)

        '                        '    If dt.Columns.Item(j).ColumnName = dtcol.Rows(k).Item(0) Then

        '                        '        strsql1 = strsql1 & Format(Chk(dt.Rows(i).Item(dt.Columns(j).ColumnName).ToString), dtcol.Rows(k).Item(1)) & ";"

        '                        '    Else
        '                        '        strsql1 = strsql1 & Chk(dt.Rows(i).Item(dt.Columns(j).ColumnName).ToString) & ";"

        '                        '    End If
        '                        '    k = k + 1

        '                        'End While

        '                        ' Next

        '                    Else
        'PP:                     strsql1 = strsql1 & Chk(dt.Rows(i).Item(dt.Columns(j).ColumnName).ToString) & ";"
        'qq:                     k = 0

        '                    End If


        '                Next

        '                strsql1 = Mid(strsql1, 1, (strsql1.Length - 1))
        '                ObjStreamWriter.WriteLine(strsql1)
        '                strsql1 = ""
        '            Next
        '            ObjStreamWriter.Close()
        '            SetMsg(lblMsg, "File : (" & fileName & Ftype & ") is generated.")

        '            'Dim fileName As String = System.Guid.NewGuid().ToString().Replace(";", "") + ".csv"
        '        Catch ex As Exception
        '            SetMsg(lblMsg, ex.Message & " : (cmdGenerate_Click)")
        '        End Try

    End Sub
    Sub Filegenerate()
        Dim dt As New DataTable
        Dim k, l As Integer
        Dim dtFname As New DataTable
        Dim strSql, qry, fileName, strpath, Dpath, Ftype As String
        Dim strsql1 As String
        Try

            qry = "SELECT top 1 Query FROM OUTPUTFILESETUP where oFileid='" & CmbFile.SelectedValue & "'"
            strSql = Session("DalObj").ExecuteCommand(qry, DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar)
            strSql = strSql.Replace("[FDATE]", "'" & Chk(Format(dtpPdate.DateValue, "yyyy-MM-dd")) & "'")
            viewstate("qryset") = strSql

            Session("DalObj").GetSqlDataTable(dt, strSql)
            qry = "SELECT top 1 * FROM OUTPUTFILESETUP where oFileid='" & CmbFile.SelectedValue & "'"
            Session("DalObj").GetSqlDataTable(dtFname, qry)

            '***********************1.1  CHECK FIELD FORMAT (Tables-fieldformat)*************
            Dim dtcol As New DataTable
            Session("DalObj").GetSqlDataTable(dtcol, "select  fieldname,fieldformat from fieldformat where OFileid='" & CmbFile.SelectedValue & "' order by fieldname")

            If dtcol.Rows.Count <> 0 Then
                For i = 0 To dtcol.Rows.Count - 1
                    For j = 0 To dt.Columns.Count - 1
                        If dtcol.Rows(i).Item(0) = dt.Columns.Item(j).ColumnName Then
                            Exit For
                        End If
                    Next
                    If j = dt.Columns.Count Then
                        SetMsg(lblMsg, "Your column name that is define in formating Table(fieldformat) :" & dtcol.Rows(i).Item(0) & " is not correct according to your query field")
                        Exit Sub
                    End If
                Next
            End If


            '*********************** 2.1  check blank ,unique and <>0  (Tables-Filevalidsetup)*************

            qry = "select fieldname ,checktype from filevalidsetup where ofileid='" & CmbFile.SelectedValue & "'"
            Session("DalObj").GetSqlDataTable(dtval, qry)
            '2.2 Check valid field  
            If dtval.Rows.Count <> 0 Then
                For i = 0 To dtval.Rows.Count - 1
                    For j = 0 To dt.Columns.Count - 1
                        If dtval.Rows(i).Item(0) = dt.Columns.Item(j).ColumnName Then
                            Exit For
                        End If
                    Next
                    If j = dt.Columns.Count Then
                        SetMsg(lblMsg, "Your column name that is define in validation Table(FileValidSetup) :" & dtval.Rows(i).Item(0) & " is not correct according to yorr query field")
                        Exit Sub
                    Else
                        filevalidsetup()
                        If flg = False Then
                            Exit Sub
                        End If
                    End If
                Next
            End If


            fileName = dtFname.Rows(0).Item("FileNamePrefix") & dtFname.Rows(0).Item("FileName") & Format(Date.Today, dtFname.Rows(0).Item("FileNameSuffix"))
            strpath = HttpContext.Current.Request.ServerVariables("APPL_PHYSICAL_PATH")
            Dpath = dtFname.Rows(0).Item("DefaultPath")
            Ftype = "." & dtFname.Rows(0).Item("FileType")
            Dim ObjStreamWriter As New StreamWriter(strpath & Dpath & fileName & Ftype)
            '*******Find column name and formating it
            'Dim dtcol As New DataTable
            'Session("DalObj").GetSqlDataTable(dtcol, "select  fieldname,fieldformat from fieldformat where OFileid='" & CmbFile.SelectedValue & "' order by fieldname")
            For i = 0 To dt.Rows.Count - 1 Step 1
                For j = 0 To dt.Columns.Count - 1 Step 1
                    If dtcol.Rows.Count > 0 Then
                        While k < dtcol.Rows.Count

                            If dt.Columns.Item(j).ColumnName = dtcol.Rows(k).Item(0) Then
                                If IsDBNull(dt.Rows(i).Item(j)) Then
                                    'strsql1 = strsql1 & dt.Rows(i).Item(dt.Columns(j).ColumnName).ToString & ""
                                    strsql1 = strsql1 & dt.Rows(i).Item(dt.Columns(j).ColumnName).ToString & dtFname.Rows(0).Item("CharType")
                                    GoTo qq
                                Else
                                    strsql1 = strsql1 & Format(dt.Rows(i).Item(j), dtcol.Rows(k).Item(1)).ToString & dtFname.Rows(0).Item("CharType")
                                    GoTo qq

                                End If
                                GoTo qq
                            End If
                            k = k + 1
                        End While
                        GoTo pp

                    Else
PP:                     strsql1 = strsql1 & Chk(dt.Rows(i).Item(dt.Columns(j).ColumnName).ToString, False, False) & dtFname.Rows(0).Item("CharType")
                        'dtFname.Rows(0).Item("CharType")
qq:                     k = 0

                    End If

                Next

                ' PENDING     strsql1 = Mid(strsql1, 1, (strsql1.Length - 1))
                Dim tot As Int16 = Len(dtFname.Rows(0).Item("CharType"))
                strsql1 = Mid(strsql1, 1, strsql1.Length - tot)

                ObjStreamWriter.WriteLine(strsql1)
                strsql1 = ""
            Next
            ObjStreamWriter.Close()
            SetMsg(lblMsg, "File : (" & fileName & Ftype & ") is generated.")
        Catch ex As Exception
            SetMsg(lblMsg, ex.Message & " : (cmdGenerate_Click)")
        End Try

    End Sub
    Sub filevalidsetup()
        'viewstate("qryset")
        Dim dt As New DataTable
        Dim chqry As String
        Try
            Select Case dtval.Rows(i).Item(1)
                Case "UNQ"
                    chqry = "select count ( " & dtval.Rows(i).Item(0) & "),count(distinct " & dtval.Rows(i).Item(0) & ") from (" & viewstate("qryset") & " )as tmp"
                    Session("DalObj").GetSqlDataTable(dt, chqry)
                    If dt.Rows(0).Item(0) <> dt.Rows(0).Item(1) Then
                        SetMsg(lblMsg, "Your field: " & dtval.Rows(i).Item(0) & "; that is define in your query is not Uniqe")
                        flg = False
                        Exit Sub
                    End If
                Case "BLANK"
                Case "NULL"


                Case "<>0"
                    chqry = "select count ( " & dtval.Rows(i).Item(0) & ") from ( " & viewstate("qryset") & " )as tmp where " & dtval.Rows(i).Item(0) & " ='0'"
                    Session("DalObj").GetSqlDataTable(dt, chqry)
                    If dt.Rows(0).Item(0).ToString <> "0" Then
                        SetMsg(lblMsg, "Your field: " & dtval.Rows(i).Item(0) & "; that is define in your query has value 0")
                        flg = False
                        Exit Sub
                    End If
                Case Else
                    SetMsg(lblMsg, "Please check the Field:checktype in Table FileValidSetup for " & dtval.Rows(i).Item(1))
                    flg = False
                    Exit Sub
            End Select
        Catch ex As Exception
            SetMsg(lblMsg, " filevalidsetup()")
            flg = False
        End Try
    End Sub


End Class
