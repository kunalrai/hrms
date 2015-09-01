Imports System.IO
Public Class MenuBars
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Hello As System.Web.UI.HtmlControls.HtmlTableCell

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
        If Not IsNothing(Session("LoginUser")) Then
            'CreateMenu()
            'Dumb()

            Dim StrJs As String
            StrJs = " <Script languages=javascript > " & Chr(10) & _
             " oCMenu=new makeCM(" & """oCMenu""" & ");" & Chr(10) & _
             " oCMenu.pxBetween = 0" & Chr(10) & _
             " oCMenu.fromLeft = 0" & Chr(10) & _
             " oCMenu.fromTop = 0" & Chr(10) & _
             " oCMenu.rows = 1" & Chr(10) & _
             " oCMenu.menuPlacement = " & """Left""" & Chr(10) & _
             " oCMenu.offlineRoot = """"" & Chr(10) & _
             " oCMenu.onlineRoot = """"" & Chr(10) & _
             " oCMenu.resizeCheck = 1" & Chr(10) & _
             " oCMenu.wait = 100" & Chr(10) & _
             " oCMenu.fillImg = " & """cm_fill.gif""" & Chr(10) & _
             " oCMenu.zIndex = 0" & Chr(10) & _
             " oCMenu.level[0]=new cm_makeLevel()" & Chr(10) & _
             " oCMenu.level[0].width=100" & Chr(10) & _
             " oCMenu.level[0].height=20" & Chr(10) & _
             " oCMenu.level[0].regClass=" & """clLevel0""" & Chr(10) & _
             " oCMenu.level[0].overClass=" & """clLevel0over""" & Chr(10) & _
             " oCMenu.level[0].borderX=0" & Chr(10) & _
             " oCMenu.level[0].borderY=0" & Chr(10) & _
             " oCMenu.level[0].borderClass=" & """clLevel0border""" & Chr(10) & _
             " oCMenu.level[0].offsetX=0" & Chr(10) & _
             " oCMenu.level[0].offsetY=0" & Chr(10) & _
             " oCMenu.level[0].rows=0" & Chr(10) & _
             " oCMenu.level[0].arrow=0" & Chr(10) & _
             " oCMenu.level[0].arrowWidth=0" & Chr(10) & _
             " oCMenu.level[0].arrowHeight=0" & Chr(10) & _
             " oCMenu.level[0].align=" & """bottom""" & Chr(10) & _
             " oCMenu.level[1]=new cm_makeLevel()" & Chr(10) & _
             " oCMenu.level[1].width=oCMenu.level[0].width-2" & Chr(10) & _
             " oCMenu.level[1].height=19" & Chr(10) & _
             " oCMenu.level[1].regClass=" & """clLevel1""" & Chr(10) & _
             " oCMenu.level[1].overClass=" & """clLevel1over""" & Chr(10) & _
             " oCMenu.level[1].borderX=0" & Chr(10) & _
             " oCMenu.level[1].borderY=1" & Chr(10) & _
             " oCMenu.level[1].align=" & """right""" & Chr(10) & _
             " oCMenu.level[1].offsetX=-(oCMenu.level[0].width-2)/2+20" & Chr(10) & _
             " oCMenu.level[1].offsetY=0" & Chr(10) & _
             " oCMenu.level[1].borderClass=" & """clLevel1border""" & Chr(10)


            '" oCMenu.makeMenu('top1','','&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Employee','','','120','22');" & Chr(10) & _
            '" oCMenu.makeMenu('sub10','top1','&nbsp;Explorer','Empexplorer.aspx','','116');" & Chr(10) & _
            '" oCMenu.makeMenu('sub11','top1','&nbsp;Work History','WorkHistEntry.aspx','','116');" & Chr(10) & _		

            Dim StrQuery, StrQry, Query As String, StrMod() As String, i, j, Count As Int16, Dt As New System.Data.DataTable, Dv As System.Data.DataView
            Dim DtModules As New System.Data.DataTable


            StrQuery = " Select Modules from WebUsers Where UserID = '" & eHRMS.Net.MdlHRMS.Encrypt(Session("LoginUser").UserId, "+") & "'"
            StrQuery = Session("DalObj").ExecuteCommand(StrQuery, , DAL.DataLayer.ExecutionType.ExecuteScalar)

            StrMod = Split(StrQuery, ",")

            If IsNothing(StrMod) Then Exit Sub

            Session("DalObj").GetSqlDataTable(DtModules, " Select * from WebModules Where MODULE_CODE In (" & StrQuery & ") Order By MODULE_GRP, OrderNo")
            Dv = New System.Data.DataView(DtModules)


            Session("DalObj").GetSqlDataTable(Dt, " Select * from WebModules Where MODULE_GRP = '*'")

            Count = 1
            For i = 0 To Dt.Rows.Count - 1

                Dv.RowFilter = " MODULE_GRP = '" & Dt.Rows(i).Item("MODULE_CODE") & "'"

                If Dv.Count > 0 Then
                    StrJs = StrJs & " oCMenu.makeMenu('top" & Count & "','','" & Dt.Rows(i).Item("MODULE_DESC") & "','','','120','22')" & Chr(10)
                    For j = 0 To Dv.Count - 1
                        StrJs = StrJs & " oCMenu.makeMenu('sub" & Count & Dv.Item(j).Item("OrderNo") & "','top" & Count & "','&nbsp;" & Dv.Item(j).Item("MODULE_DESC") & "',' " & Dv.Item(j).Item("MODULE_FORM") & " ','','116')" & Chr(10)
                    Next
                    Count += 1
                End If

            Next

            StrJs = StrJs & "oCMenu.construct()" & Chr(10)
            StrJs = StrJs & "</Script>"

            Response.Write(StrJs)









        End If
    End Sub

    Sub CreateMenu()
        Try

            Dim StrQuery, StrJs, StrQry As String, StrMod() As String, i As Int16, Dv As DataView, Dt As New DataTable

            StrQuery = " Select Modules from WebUsers Where UserID = '" & Encrypt(Session("LoginUser").UserId, "+") & "'"
            StrQuery = Session("DalObj").ExecuteCommand(StrQuery, , DAL.DataLayer.ExecutionType.ExecuteScalar)

            StrMod = Split(StrQuery, ",")

            If IsNothing(StrMod) Then Exit Sub
            
            Session("DalObj").GetSqlDataTable(Dt, " Select * from WebModules Where MODULE_CODE In (" & StrQuery & ") Order By ")
            Dv = New DataView(Dt)
            Dv.RowFilter = " MODULE_GRP = '*'"

            For i = 0 To Dv.Count - 1
                StrJs = StrJs & " oCMenu.makeMenu('top'" & i & ",'','&nbsp;" & Dv.Item(i).Item("MODULE_DESC") & "','','','120','22');"
            Next



            Dim strPath As String = HttpContext.Current.Request.ServerVariables("APPL_PHYSICAL_PATH")
            strPath = strPath & "Menu.js"
            If Not File.Exists(strPath) Then Exit Sub

            Dim sw As StreamWriter = New StreamWriter(strPath)


            StrJs = " oCMenu.makeMenu('top1','','&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Employee','','','120','22');" & _
                    " oCMenu.makeMenu('sub10','top1','&nbsp;Explorer','Empexplorer.aspx','','116'); " & _
                    " oCMenu.makeMenu('sub11','top1','&nbsp;Work History','WorkHistEntry.aspx','','116');"


            sw.Write(StrJs)
            sw.Close()

        Catch ex As Exception

        End Try
    End Sub

    Sub Dumb()
        Dim StrJs As String
        'Dim strPath As String = HttpContext.Current.Request.ServerVariables("APPL_PHYSICAL_PATH")
        'strPath = strPath & "Menu.js"
        'If Not File.Exists(strPath) Then Exit Sub

        'Dim sw As StreamWriter = New StreamWriter(strPath)
        '" <Script languages=javascript src=coolmenus4.js ></Script> " & Chr(10) & _
        StrJs = " <Script languages=javascript > " & Chr(10) & _
                " oCMenu=new makeCM(" & """oCMenu""" & "); " & Chr(10) & _
                " oCMenu.pxBetween = 0;" & Chr(10) & _
                " oCMenu.fromLeft = 0;" & Chr(10) & _
                " oCMenu.fromTop = 0;" & Chr(10) & _
                " oCMenu.rows = 1;" & Chr(10) & _
                " oCMenu.menuPlacement = " & """Left"";" & Chr(10) & _
                " oCMenu.offlineRoot = """";" & Chr(10) & _
                " oCMenu.onlineRoot = """";" & Chr(10) & _
                " oCMenu.resizeCheck = 1;" & Chr(10) & _
                " oCMenu.wait = 100;" & Chr(10) & _
                " oCMenu.fillImg = " & """cm_fill.gif"";" & Chr(10) & _
                " oCMenu.zIndex = 0;" & Chr(10) & _
                " oCMenu.level[0]=new cm_makeLevel();" & Chr(10) & _
                " oCMenu.level[0].width=100;" & Chr(10) & _
                " oCMenu.level[0].height=20;" & Chr(10) & _
                " oCMenu.level[0].regClass=" & """clLevel0"";" & Chr(10) & _
                " oCMenu.level[0].overClass=" & """clLevel0over"";" & Chr(10) & _
                " oCMenu.level[0].borderX=0;" & Chr(10) & _
                " oCMenu.level[0].borderY=0;" & Chr(10) & _
                " oCMenu.level[0].borderClass=" & """clLevel0border"";" & Chr(10) & _
                " oCMenu.level[0].offsetX=0;" & Chr(10) & _
                " oCMenu.level[0].offsetY=0;" & Chr(10) & _
                " oCMenu.level[0].rows=0;" & Chr(10) & _
                " oCMenu.level[0].arrow=0;" & Chr(10) & _
                " oCMenu.level[0].arrowWidth=0;" & Chr(10) & _
                " oCMenu.level[0].arrowHeight=0;" & Chr(10) & _
                " oCMenu.level[0].align=" & """bottom"";" & Chr(10) & _
                " oCMenu.level[1]=new cm_makeLevel();" & Chr(10) & _
                " oCMenu.level[1].width=oCMenu.level[0].width-2;" & Chr(10) & _
                " oCMenu.level[1].height=19;" & Chr(10) & _
                " oCMenu.level[1].regClass=" & """clLevel1"";" & Chr(10) & _
                " oCMenu.level[1].overClass=" & """clLevel1over"";" & Chr(10) & _
                " oCMenu.level[1].borderX=0;" & Chr(10) & _
                " oCMenu.level[1].borderY=1;" & Chr(10) & _
                " oCMenu.level[1].align=" & """right"";" & Chr(10) & _
                " oCMenu.level[1].offsetX=-(oCMenu.level[0].width-2)/2+20;" & Chr(10) & _
                " oCMenu.level[1].offsetY=0;" & Chr(10) & _
                " oCMenu.level[1].borderClass=" & """clLevel1border"";" & Chr(10) & _
                " oCMenu.makeMenu('top1','','&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Employee','','','120','22');" & Chr(10) & _
                " oCMenu.makeMenu('sub10','top1','&nbsp;Explorer','Empexplorer.aspx','','116');" & Chr(10) & _
                " oCMenu.makeMenu('sub11','top1','&nbsp;Work History','WorkHistEntry.aspx','','116');" & Chr(10) & _
                " oCMenu.construct();" & Chr(10) & _
                "</Script>"


        Response.Write(StrJs)
        'sw.Write(StrJs)
        'sw.Close()

    End Sub

    Sub Comment()
        'Response.Write("oCMenu=New makeCM(" & """oCMenu""" & ")")
        'Response.Write("oCMenu.pxBetween = 0")
        'Response.Write("oCMenu.fromLeft = 0")
        'Response.Write("oCMenu.fromTop = 0")
        'Response.Write("oCMenu.rows = 1")
        'Response.Write("oCMenu.menuPlacement = 'Left'")
        'Response.Write("oCMenu.offlineRoot = """)
        'Response.Write("oCMenu.onlineRoot = """)
        'Response.Write("oCMenu.resizeCheck = 1")
        'Response.Write("oCMenu.wait = 100")
        'Response.Write("oCMenu.fillImg = 'cm_fill.gif'")
        'Response.Write("oCMenu.zIndex = 0")
        'Response.Write("oCMenu.level[0]=new cm_makeLevel()")
        'Response.Write("oCMenu.level[0].width=100")
        'Response.Write("oCMenu.level[0].height=20")
        'Response.Write("oCMenu.level[0].regClass='clLevel0'")
        'Response.Write("oCMenu.level[0].overClass='clLevel0over")
        'Response.Write("oCMenu.level[0].borderX=0")
        'Response.Write("oCMenu.level[0].borderY=0")
        'Response.Write("oCMenu.level[0].borderClass='clLevel0border'")
        'Response.Write("oCMenu.level[0].offsetX=0")
        'Response.Write("oCMenu.level[0].offsetY=0")
        'Response.Write("oCMenu.level[0].rows=0")
        'Response.Write("oCMenu.level[0].arrow=0")
        'Response.Write("oCMenu.level[0].arrowWidth=0")
        'Response.Write("oCMenu.level[0].arrowHeight=0")
        'Response.Write("oCMenu.level[0].align='bottom'")

        'Response.Write("oCMenu.level[1]=new cm_makeLevel()")
        'Response.Write("oCMenu.level[1].width=oCMenu.level[0].width-2")
        'Response.Write("oCMenu.level[1].height=19")
        'Response.Write("oCMenu.level[1].regClass='clLevel1'")
        'Response.Write("oCMenu.level[1].overClass='clLevel1over'")
        'Response.Write("oCMenu.level[1].borderX=0")
        'Response.Write("oCMenu.level[1].borderY=1")
        'Response.Write("oCMenu.level[1].align='right'")
        'Response.Write("oCMenu.level[1].offsetX=-(oCMenu.level[0].width-2)/2+20")
        'Response.Write("oCMenu.level[1].offsetY=0")
        'Response.Write("oCMenu.level[1].borderClass='clLevel1border'")

        'Response.Write("oCMenu.makeMenu('top1','','&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Employee','','','120','22')")
        'Response.Write("oCMenu.makeMenu('sub10','top1','&nbsp;Explorer','Empexplorer.aspx','','116')")
        'Response.Write("oCMenu.makeMenu('sub11','top1','&nbsp;Work History','WorkHistEntry.aspx','','116')")
        'Response.Write("oCMenu.construct()")

        'Response.Write("</script>")
    End Sub
End Class
