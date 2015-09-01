Imports BAL.BLayer

Partial Class TreeMenu
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

    Dim blnAnyModule As Boolean
    Dim TMPSQL As String
    Dim StrQueryTemp As String
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If IsNothing(Session("LoginUser")) Then
            LblUser.Text = ""
        Else
            LblUser.Text = "User : <B>" & ChkS(Session("LoginUser").UserName) & "</B><br>"
        End If
        Session("TblModules") = TblModules
        blnAnyModule = False


        Dim strGrpCode As String
        Dim StrQuery As String
        strGrpCode = Request.QueryString.Item("GrpCode")
        StrQuery = " Select Modules from WebUsers Where UserID In (Select Group_ID From WebUsers Where UserID = '" & eHRMS.Net.MdlHRMS.Encrypt(Session("LoginUser").UserId, "+") & "')"
        StrQuery = Session("DalObj").ExecuteCommand(StrQuery, , DAL.DataLayer.ExecutionType.ExecuteScalar)
        StrQueryTemp = Replace(StrQuery, "-S", "")
        StrQueryTemp = Replace(StrQueryTemp, "-V", "")
        'Session("DalObj").GetSqlDataTable(Dt, " Select * from WebModules WHERE (Active=1 AND Shotrcut<>1 AND MODULE_GRP = '" & strGrpCode & "' AND MODULE_CODE IN ( SELECT MODULE_GRP FROM WEBMODULES WHERE MODULE_CODE IN (" & StrQueryTemp & "))) OR Shotrcut=1 Order By OrderNo")

        FillMenu()
    End Sub
    Private Sub FillMenu()
        Dim mTbl As HtmlTable, TblRow As HtmlTableRow
        Dim ROWCell1 As HtmlTableCell, ROWCell2 As HtmlTableCell, ROWCell3 As HtmlTableCell, ROWCell4 As HtmlTableCell
        Dim img As New HtmlImage, SubTbl As HtmlTable
        mTbl = New HtmlTable

        TblRow = New HtmlTableRow
        ROWCell1 = New HtmlTableCell
        ROWCell2 = New HtmlTableCell
        ROWCell1.Width = "15"
        ROWCell1.VAlign = "Top"
        'ROWCell2.Width = "95%"

        img = New HtmlImage
        img.ID = "Img000"
        img.Src = "images/Minus.gif"
        img.Style.Item("CURSOR") = "hand"
        img.Attributes.Add("OnClick", "ShowMenu('000')")
        img.Width = 9
        img.Height = 9
        ROWCell1.Controls.Add(img)

        ROWCell2.InnerHtml = "&nbsp;&nbsp;<b>MODULE</B>"
        'ROWCell2.Style.Item("CURSOR") = "hand"
        'ROWCell2.Attributes.Add("OnClick", "ShowMenu('000')")

        SubTbl = New HtmlTable
        SubTbl.ID = "Tbl000"
        SubTbl.CellPadding = 2
        SubTbl.CellSpacing = 2
        SubTbl.Border = 0
        SubTbl.Style.Item("display") = "block"
        FillSubMenu(0, SubTbl)

        ROWCell2.Controls.Add(SubTbl)

        TblRow.Cells.Add(ROWCell1)
        TblRow.Cells.Add(ROWCell2)

        mTbl.Rows.Add(TblRow)
        Session("TblModules").Rows.Add(TblRow)


        ''''''Blank Line''''''''
        Dim abtRow1 As HtmlTableRow
        abtRow1 = New HtmlTableRow
        ROWCell1 = New HtmlTableCell

        ROWCell1.ColSpan = 2
        ROWCell1.InnerHtml = "&nbsp;"
        abtRow1.Cells.Add(ROWCell1)
        mTbl.Rows.Add(abtRow1)
        Session("TblModules").Rows.Add(abtRow1)


        ''''''''About us...'''''''
        ''Dim abtRow As HtmlTableRow
        ''abtRow = New HtmlTableRow
        ''ROWCell1 = New HtmlTableCell
        '''ROWCell2 = New HtmlTableCell
        '''ROWCell1.InnerText = " "
        ''ROWCell1.ColSpan = 2
        ''ROWCell1.Align = "Center"
        ''ROWCell1.InnerHtml = "<A href=FrmAboutus.aspx?MODULEID=999  Target = main> <b>About us...</b></A>"
        ''abtRow.Cells.Add(ROWCell1)
        '''abtRow.Cells.Add(ROWCell2)
        ''mTbl.Rows.Add(abtRow)
        ''Session("TblModules").Rows.Add(abtRow)

    End Sub
    Private Sub FillSubMenu(ByVal Module_Grp As Int32, ByRef SubTBL As HtmlTable)

        Dim mTbl As HtmlTable, TblRow As HtmlTableRow
        Dim ROWCell1 As HtmlTableCell, ROWCell2 As HtmlTableCell, ROWCell3 As HtmlTableCell, ROWCell4 As HtmlTableCell
        Dim img As HtmlImage, Tbl As HtmlTable
        Dim dtTmp As DataTable, T As Int32

        If ChkS(Session("LoginUser").UserName) = "EDP" Then
            TMPSQL = " SELECT I.Module_Desc, I.OrderNo, I.Module_Code, I.Module_Grp, I.Module_Form, i.HelpText  " & _
                     " FROM  WebModules  I  " & _
                     " WHERE I.Module_Grp = " & Module_Grp & " And (I.Active=1)  Order By OrderNo "
        Else
            'TMPSQL = " SELECT I.Module_Desc, I.OrderNo, I.Module_Code, I.Module_Grp, I.Module_Form, i.HelpText  " & _
            '         " FROM AD_SYS_USERRIGHT  U INNER JOIN WebModules   I ON U.Module_Code = I.Module_Code " & _
            '         " WHERE I.Module_Grp = " & Module_Grp & " And (((U.iUserID)=" & ChkN(Session("LoginUser").UserID) & ") AND ((I.Active)=1) AND ((U.iView)=1))  Order By OrderNo "

            'COMMENT BY RAVI
            'TMPSQL = " SELECT I.Module_Desc, I.OrderNo, I.Module_Code, I.Module_Grp, I.Module_Form, I.HelpText   FROM  WebModules  I " & _
            '" WHERE I.Module_Grp = " & Module_Grp & " And ((I.Active)=1)  AND " & _
            '" ( MODULE_CODE IN ( SELECT MODULE_GRP FROM WEBMODULES WHERE MODULE_CODE IN (" & StrQueryTemp & "))  " & _
            '"  OR  MODULE_CODE IN ( SELECT MODULE_CODE FROM WEBMODULES WHERE MODULE_CODE IN (" & StrQueryTemp & "))  ) " & _
            '" Order By OrderNo "
            '------------------------------------------------
            'BY RAVI ON 27 NOV 2006
            If Module_Grp = 0 Then
                'Dim haschild, modulegrp, mdldesc As String
                'Dim strsql As String
                'strsql = "select haschild from webmodules where module_code='" & Module_Grp & "'"
                'haschild = Session("DalObj").ExecuteCommand(strsql, DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar)
                'modulegrp = Session("DalObj").ExecuteCommand("select module_grp from webmodules where module_code='" & Module_Grp & "'", DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar)
                'mdldesc = Session("DalObj").ExecuteCommand("select MODULE_DESC from webmodules where module_code='" & Module_Grp & "'", DAL.DataLayer.ConnProvider.SQL, DAL.DataLayer.ExecutionType.ExecuteScalar)
                ' If haschild = "1" And modulegrp = "0" And mdldesc = "Payroll" Then
                TMPSQL = "Select * from WebModules WHERE MODULE_CODE IN (SELECT  MODULE_CODE FROM("
                TMPSQL &= "  SELECT * FROM  WebModules   " & _
                            " WHERE Module_Grp = " & Module_Grp & " And ((Active)=1)  AND " & _
                            " ( MODULE_CODE IN ( SELECT MODULE_GRP FROM WEBMODULES WHERE MODULE_CODE IN (" & StrQueryTemp & "))  " & _
                            "  OR  MODULE_CODE IN ( SELECT MODULE_CODE FROM WEBMODULES WHERE MODULE_CODE IN (" & StrQueryTemp & "))  ) "

                TMPSQL &= "UNION ALL "
                TMPSQL &= " SELECT * FROM WEBMODULES WHERE MODULE_CODE IN ( SELECT MODULE_GRP FROM WEBMODULES WHERE  MODULE_CODE IN (SELECT MODULE_GRP FROM WEBMODULES WHERE MODULE_CODE IN (" & StrQueryTemp & "))))TMP  WHERE MODULE_CODE >0  ) Order By OrderNo"
            Else

                TMPSQL = " SELECT I.Module_Desc, I.OrderNo, I.Module_Code, I.Module_Grp, I.Module_Form, I.HelpText   FROM  WebModules  I " & _
                           " WHERE I.Module_Grp = " & Module_Grp & " And ((I.Active)=1)  AND " & _
                           " ( MODULE_CODE IN ( SELECT MODULE_GRP FROM WEBMODULES WHERE MODULE_CODE IN (" & StrQueryTemp & "))  " & _
                           "  OR  MODULE_CODE IN ( SELECT MODULE_CODE FROM WEBMODULES WHERE MODULE_CODE IN (" & StrQueryTemp & "))  ) " & _
                           " Order By OrderNo "
            End If

            '--------------------------------------------

            'Session("DalObj").GetSqlDataTable(Dt, " Select * from WebModules WHERE (Active=1 AND Shotrcut<>1 AND MODULE_GRP = '" & strGrpCode & "' AND MODULE_CODE IN ( SELECT MODULE_GRP FROM WEBMODULES WHERE MODULE_CODE IN (" & StrQueryTemp & "))) OR Shotrcut=1 Order By OrderNo")
        End If

        'If Module_Grp = 0 Then tmpSQL = tmpSQL & " Union All Select 'About Us...' as [Module_Desc], '999' as OrderNo, 999 as Module_Code, 0 as Module_Grp, 'FrmAboutUs.aspx' as Module_Form, 'main' as HelpText "
        'tmpsql = " Select * From (" & tmpSQL & ") as M Order By OrderNo "


        dtTmp = New DataTable
        Session("DalObj").GetSqlDataTable(dtTmp, TMPSQL)

        For T = 0 To dtTmp.Rows.Count - 1
            TblRow = New HtmlTableRow
            With dtTmp.Rows(T)
                TMPSQL = "Select Count(*) From WebModules  Where Active = 1 And Module_Grp = " & ChkN(.Item("Module_Code"))
                If ChkN(Session("DalObj").ExecuteCommand(TMPSQL, , DAL.DataLayer.ExecutionType.ExecuteScalar)) <> 0 Then
                    ROWCell1 = New HtmlTableCell
                    ROWCell2 = New HtmlTableCell
                    ROWCell1.Align = "Left"
                    ROWCell1.Width = "15"
                    ROWCell1.VAlign = "Top"
                    'ROWCell2.Width = "150"
                    img = New HtmlImage
                    img.Src = "images/Plus.gif"
                    img.ID = "img" & Format(ChkN(.Item("Module_Code")), "000")
                    img.Attributes.Add("OnClick", "ShowMenu('" & Format(ChkN(.Item("Module_Code")), "000") & "')")
                    img.Style.Item("CURSOR") = "hand"
                    img.Width = 9
                    img.Height = 9
                    ROWCell1.Controls.Add(img)
                    ROWCell2.InnerText = ""

                    'TblRow.Cells.Add(ROWCell1)
                    ROWCell2.InnerText = " " & ChkS(.Item("Module_Desc"))
                    ROWCell2.NoWrap = True

                    Tbl = New HtmlTable
                    Tbl.ID = "Tbl" & Format(ChkN(.Item("Module_Code")), "000")
                    Tbl.CellPadding = 2
                    Tbl.CellSpacing = 2
                    Tbl.Border = 0
                    Tbl.Style.Item("display") = "none"
                    FillSubMenu(ChkN(.Item("Module_Code")), Tbl)
                    ROWCell2.Controls.Add(Tbl)
                    blnAnyModule = True
                Else

                    ROWCell1 = New HtmlTableCell
                    ROWCell1.ColSpan = 2
                    ROWCell1.ID = "TR" & ChkN(.Item("Module_Code"), True, "000")
                    If InStr(Chk(dtTmp.Rows(T).Item("Module_Form")), "?") = 0 Then
                        ROWCell1.InnerHtml = "<A href=" & ChkS(.Item("Module_Form")) & "?MODULEID=" & ChkN(.Item("Module_Code")) & "  " & IIf(ChkS(.Item("HelpText")) = "", "", " Target = " & ChkS(.Item("HelpText"))) & ">" & ChkS(.Item("Module_Desc")) & "</A>"
                    Else
                        ROWCell1.InnerHtml = "<A href=" & ChkS(.Item("Module_Form")) & "&" & "MODULEID=" & ChkN(.Item("Module_Code")) & "  " & IIf(ChkS(.Item("HelpText")) = "", "", " Target = " & ChkS(.Item("HelpText"))) & ">" & ChkS(.Item("Module_Desc")) & "</A>"
                        'comment by Ravi
                        'ROWCell1.InnerHtml = "<A href=" & ChkS(.Item("Module_Form")) & "&" & "SrNo=" & ChkN(.Item("Module_Code")) & "  " & IIf(ChkS(.Item("HelpText")) = "", "", " Target = " & ChkS(.Item("HelpText"))) & ">" & ChkS(.Item("Module_Desc")) & "</A>"
                    End If


                    ROWCell1.NoWrap = True
                    ROWCell2 = Nothing
                End If

                TblRow.Cells.Add(ROWCell1)
                If Not ROWCell2 Is Nothing Then TblRow.Cells.Add(ROWCell2)
                SubTBL.Rows.Add(TblRow)
            End With
        Next
        dtTmp.Dispose()

        If blnAnyModule = False And Module_Grp = 0 Then
            ROWCell1 = New HtmlTableCell
            ROWCell1.ColSpan = 2
            ROWCell1.InnerHtml = "No rights available"
            TblRow = New HtmlTableRow
            TblRow.Cells.Add(ROWCell1)
            SubTBL.Rows.Add(TblRow)
        End If

    End Sub

End Class
