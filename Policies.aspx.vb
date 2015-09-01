Partial Class Policies
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
        If Server.GetLastError Is Nothing Then SetMsg(LblErrMsg, "")
        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If
        If Not IsPostBack Then
            'Response.Write(Common)
            FillPolicies()
        End If
    End Sub

    Sub FillPolicies()
        Try
            Dim DtPolicies As New DataTable, TBLRow As HtmlTableRow, TBLCell1 As HtmlTableCell, TBLCell2 As HtmlTableCell, Grade As String, i, j As Int16, Dv As DataView

            Grade = ""
            Grade = Session("DalObj").ExecuteCommand("Select isnull(Grd_Code,'') from HrdMast Where Emp_Code='" & Session("LoginUser").UserId & "'", , DAL.DataLayer.ExecutionType.ExecuteScalar)

            If Grade <> "" Then
                Session("DalObj").GetSqlDataTable(DtPolicies, " Select * from POLICYMAST Where Grd_Code = '" & Grade & "' and Active=1")
                If DtPolicies.Rows.Count <> 0 Then
                    TBLRow = New HtmlTableRow
                    TBLCell1 = New HtmlTableCell

                    For i = 0 To DtPolicies.Rows.Count - 1
                        TBLRow = New HtmlTableRow
                        TBLCell1 = New HtmlTableCell

                        TBLCell1.Style.Item("FONT-SIZE") = "10pt"
                        TBLCell1.Align = "Left"
                        TBLCell1.InnerHtml = "&nbsp;&nbsp;<a Href='" & DtPolicies.Rows(i).Item("MODULE_FORM") & "'  Target=ABX>" & i + 1 & ". " & DtPolicies.Rows(i).Item("MODULE_DESC") & "</a>"
                        TBLRow.Cells.Add(TBLCell1)
                        TblPolicies.Rows.Add(TBLRow)
                    Next
                Else
                    TBLRow = New HtmlTableRow
                    TBLCell1 = New HtmlTableCell

                    TBLCell1.Align = "Center"
                    TBLCell1.Style.Item("FONT-SIZE") = "10pt"
                    TBLCell1.Style.Item("Color") = "#003366"
                    TBLCell1.InnerHtml = "<b>No Policy Defined for your group.</b>"
                    TBLRow.Cells.Add(TBLCell1)
                    TblPolicies.Rows.Add(TBLRow)
                End If
            Else
                Dim CountGrade As Int16

                CountGrade = ChkN(CType(Session("DalObj"), DAL.DataLayer).ExecuteCommand("SELECT isnull(Count(*),0) FROM GRDMAST ", , DAL.DataLayer.ExecutionType.ExecuteScalar))
                If CountGrade = 0 Then

                    Dim DtCountType As New DataTable
                    If IsNothing(Request.QueryString.Item("TYPEID")) Then
                        Session("DalObj").GetSqlDataTable(DtCountType, " SELECT TYPEID,TYPENAME FROM POLICYTYPEMAST WHERE TYPEID=1")
                    Else
                        Session("DalObj").GetSqlDataTable(DtCountType, " SELECT TYPEID,TYPENAME FROM POLICYTYPEMAST WHERE TYPEID not in (" & Request.QueryString.Item("TYPEID") & ")")
                    End If


                    Session("DalObj").GetSqlDataTable(DtPolicies, " Select MODULE_DESC, MODULE_FORM, TYPEID from POLICYMAST Where Active=1")
                    Dv = New DataView(DtPolicies)
                    For j = 0 To DtCountType.Rows.Count - 1
                        TBLRow = New HtmlTableRow
                        TBLCell1 = New HtmlTableCell
                        TBLCell1.Style.Item("FONT-SIZE") = "10pt"
                        TBLCell1.Style.Item("FONT-WEIGHT") = "BOLD"
                        TBLCell1.Align = "Left"
                        TBLCell1.InnerHtml = Chk(DtCountType.Rows(j).Item(1))

                        TBLRow.Cells.Add(TBLCell1)
                        TblPolicies.Rows.Add(TBLRow)

                        Dv.RowFilter = " TYPEID='" & ChkN(DtCountType.Rows(j).Item(0)) & "'"
                        If Dv.Count <> 0 Then
                            TBLRow = New HtmlTableRow
                            TBLCell1 = New HtmlTableCell

                            For i = 0 To Dv.Count - 1
                                TBLRow = New HtmlTableRow
                                TBLCell1 = New HtmlTableCell

                                TBLCell1.Style.Item("FONT-SIZE") = "10pt"
                                TBLCell1.Align = "Left"
                                TBLCell1.InnerHtml = "&nbsp;&nbsp;<a Href='" & Dv.Item(i).Item("MODULE_FORM") & "'  Target=ABX>" & i + 1 & ". " & Dv.Item(i).Item("MODULE_DESC") & "</a>"
                                TBLRow.Cells.Add(TBLCell1)
                                TblPolicies.Rows.Add(TBLRow)
                            Next
                        End If
                    Next

                End If
            End If
        Catch ex As Exception
            SetMsg(LblErrMsg, ex.Message & " : " & "FillPolicies")
        End Try

    End Sub

    Public Overrides Sub Dispose()
        GC.Collect()
    End Sub

End Class
