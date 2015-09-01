Partial Class Company_Setup
    Inherits System.Web.UI.Page
    Dim Dt As DataTable
    Dim Ds As New DataSet
    Dim oDAL As DAL.DataLayer
    Dim oBAL As BAL.BLayer
    Dim DalUser As DAL.DataLayer.Users
    Protected WithEvents divGeneralHead As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents divSignatoryHead As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents divReimbursementHead As System.Web.UI.HtmlControls.HtmlTable

    Dim VarComp_Code As String
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    'Protected WithEvents TblGeneral As System.Web.UI.HtmlControls.HtmlTable

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

        If IsNothing(Session("LoginUser")) Then
            Response.Redirect("CompSel.aspx")
        End If
        oDAL = Session("DalObj")
        oBAL = Session("BalObj")
        DalUser = Session("LoginUser")
        'Put user code to initialize the page here
        If Not IsPostBack Then
            'By Ravi 22 nov 2006
            Dim SrNo As Int16
            SrNo = Request.QueryString.Item("SrNo") + Request.QueryString.Item("MODULEID")
            Dim bSuccess As Boolean
            Select Case CheckRight(SrNo)
                Case MdlHRMS.AccessType.FullAccess, MdlHRMS.AccessType.SuperUser
                    bSuccess = True
                Case MdlHRMS.AccessType.ReadonlyAccess, MdlHRMS.AccessType.Restricted
                    bSuccess = False
            End Select
            BtnOk.Visible = bSuccess
            '-----------------------------------------------------------------
            oBAL.FillCombo(cmbstate, "State_Code", "State_Name", "StateMast", True)
            oBAL.FillCombo(cmbstat, "State_Code", "State_Name", "StateMast", True)
            VarComp_Code = DalUser.CurrentCompID
            SearchData()

            '  TrGeneral.Style.Item("FONT-WEIGHT") = "bold"
            ' TblSignatory.Style.Item("display") = "none"
            'TblReimbursement.Style.Item("display") = "none"
        End If
    End Sub
    Private Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        Response.Redirect("Main.aspx")
    End Sub
    Private Function SearchData()
        Try
            Dim StrSql As String
            Dim rsCompMast As New DataTable
            StrSql = "Select * from CompMast where Comp_Code='" & DalUser.CurrentCompID & "'"
            oDAL.GetSqlDataTable(rsCompMast, StrSql)
            If rsCompMast.Rows.Count > 0 Then
                '***********************************cOMMENT BY RAVI 19 OCT 2006***********************
                'txtcode.Text = DalUser.CurrentCompID
                'txtname.Text = rsCompMast.Rows(0).Item("Comp_Name")
                'txtaddress.Text = rsCompMast.Rows(0).Item("Comp_Addr")
                'txtcity.Text = rsCompMast.Rows(0).Item("Comp_City")
                'cmbstate.SelectedValue = rsCompMast.Rows(0).Item("Comp_State_Code")
                'txtpin.Text = rsCompMast.Rows(0).Item("Comp_Pin")
                'txtpfno.Text = rsCompMast.Rows(0).Item("Comp_PFNO")
                'txtesino.Text = rsCompMast.Rows(0).Item("Comp_ESINo")
                'TxtPANNo.Text = rsCompMast.Rows(0).Item("Comp_PANNO")
                'txttanno.Text = rsCompMast.Rows(0).Item("Comp_TANNo")
                'TxtTdsAmt.Text = rsCompMast.Rows(0).Item("Comp_TDSCircle")
                'txtsignatory.Text = rsCompMast.Rows(0).Item("Sign_Name")
                'txtfathersname.Text = rsCompMast.Rows(0).Item("Sign_FName")
                'txtdesig.Text = rsCompMast.Rows(0).Item("Sign_Dsg")
                'txtaddres.Text = rsCompMast.Rows(0).Item("Sign_Addr")
                'txtcit.Text = rsCompMast.Rows(0).Item("Sign_City")
                'cmbstat.SelectedValue = rsCompMast.Rows(0).Item("Sign_State_Code")
                'txtpi.Text = rsCompMast.Rows(0).Item("Sign_Pin")
                'txtplace.Text = rsCompMast.Rows(0).Item("Sign_Place")
                'dtpdate.DateValue = rsCompMast.Rows(0).Item("Sign_Date")
                txtcode.Text = DalUser.CurrentCompID
                txtname.Text = Chk(rsCompMast.Rows(0).Item("Comp_Name"))
                txtaddress.Text = Chk(rsCompMast.Rows(0).Item("Comp_Addr"))
                txtcity.Text = Chk(rsCompMast.Rows(0).Item("Comp_City"))
                cmbstate.SelectedValue = rsCompMast.Rows(0).Item("Comp_State_Code")
                txtpin.Text = Chk(rsCompMast.Rows(0).Item("Comp_Pin"))
                txtpfno.Text = Chk(rsCompMast.Rows(0).Item("Comp_PFNO"))
                txtesino.Text = Chk(rsCompMast.Rows(0).Item("Comp_ESINo"))
                TxtPANNo.Text = Chk(rsCompMast.Rows(0).Item("Comp_PANNO"))
                txttanno.Text = Chk(rsCompMast.Rows(0).Item("Comp_TANNo"))
                TxtTdsAmt.Text = Chk(rsCompMast.Rows(0).Item("Comp_TDSCircle"))
                txtsignatory.Text = Chk(rsCompMast.Rows(0).Item("Sign_Name"))
                txtfathersname.Text = Chk(rsCompMast.Rows(0).Item("Sign_FName"))
                txtdesig.Text = Chk(rsCompMast.Rows(0).Item("Sign_Dsg"))
                txtaddres.Text = Chk(rsCompMast.Rows(0).Item("Sign_Addr"))
                txtcit.Text = Chk(rsCompMast.Rows(0).Item("Sign_City"))
                cmbstat.SelectedValue = rsCompMast.Rows(0).Item("Sign_State_Code")
                txtpi.Text = Chk(rsCompMast.Rows(0).Item("Sign_Pin"))
                txtplace.Text = Chk(rsCompMast.Rows(0).Item("Sign_Place"))
                dtpdate.DateValue = rsCompMast.Rows(0).Item("Sign_Date")
                With rsCompMast.Rows(0)
                    If Not IsDBNull(.Item("Sign_Date")) Then
                        dtpdate.DateValue = .Item("Sign_Date")
                    End If
                End With
            End If
            '=================ChkBoxControls:->> Status

            If ChkNull(rsCompMast.Rows(0).Item("Rating1")) = "Y" Then
                chkusesppc.Checked = True
            Else
                chkusesppc.Checked = False
            End If
            If ChkNull(rsCompMast.Rows(0).Item("Rating2")) = "Y" Then
                chkarmte.Checked = True
            Else
                chkarmte.Checked = False
            End If
            If ChkNull(rsCompMast.Rows(0).Item("Rating3")) = "Y" Then
                chktelr.Checked = True
            Else
                chktelr.Checked = False
            End If

            ' ====================RadioButtonList:->> Status

            If ChkNull(rsCompMast.Rows(0).Item("Rating5")) = "1" Then
                RblCalculate.SelectedIndex = 0
            End If
            If ChkNull(rsCompMast.Rows(0).Item("Rating5")) = "2" Then
                RblCalculate.SelectedIndex = 1
            End If
            If ChkNull(rsCompMast.Rows(0).Item("Rating5")) = "3" Then
                RblCalculate.SelectedIndex = 2
            End If
        Catch ex As Exception
            LblErrMsg.Text = ex.Message
        End Try
    End Function
    Private Function UpdateData()
        Try
            Dim StrUpdate As String
            Dim rsCmpMast As New DataTable
            Dim DtDate As Date
            DtDate = dtpdate.DateValue
            '==========Update Query
            StrUpdate = " Update CompMast Set " & _
             " Comp_Name = '" & Chk(txtname.Text) & "', " & _
             " Comp_Addr = '" & Chk(txtaddress.Text) & "', " & _
             " Comp_City = '" & Chk(txtcity.Text) & "', " & _
             " Comp_State_Code = '" & Chk(cmbstate.SelectedValue) & "', " & _
             " Comp_State = '" & Chk(cmbstate.SelectedItem.Text) & "', " & _
             " Comp_Pin = '" & Chk(txtpin.Text) & "', " & _
             " Comp_PFNo = '" & Chk(txtpfno.Text) & "', " & _
             " Comp_ESINo = '" & Chk(txtesino.Text) & "', " & _
             " Comp_PANNo = '" & Chk(TxtPANNo.Text) & "', " & _
             " Comp_TANNo = '" & Chk(txttanno.Text) & "', " & _
             " Comp_TDSCircle = '" & Chk(TxtTdsAmt.Text) & "', " & _
             " Sign_Name = '" & Chk(txtsignatory.Text) & "', " & _
             " Sign_FName = '" & Chk(txtfathersname.Text) & "', " & _
             " Sign_Dsg = '" & Chk(txtdesig.Text) & "', " & _
             " Sign_Addr = '" & Chk(txtaddres.Text) & "', " & _
             " Sign_City = '" & Chk(txtcit.Text) & "', " & _
             " Sign_Pin = '" & Chk(txtpi.Text) & "', " & _
             " Sign_Place = '" & Chk(txtplace.Text) & "', " & _
             " Sign_State_Code = '" & Chk(cmbstat.SelectedValue) & "', " & _
             " Sign_State = '" & Chk(cmbstat.SelectedItem.Text) & "', " & _
             " Sign_Date = '" & dtpdate.DateValue & "', " & _
             " Rating5 = '" & Chk(RblCalculate.SelectedValue) & "', "
            If chkusesppc.Checked = True Then
                StrUpdate = StrUpdate & "Rating1='Y',"
            Else
                StrUpdate = StrUpdate & "Rating1='N',"
            End If
            If chkarmte.Checked = True Then
                StrUpdate = StrUpdate & "Rating2='Y',"
            Else
                StrUpdate = StrUpdate & "Rating2='N',"
            End If
            If chktelr.Checked = True Then
                StrUpdate = StrUpdate & "Rating3='Y'"
            Else
                StrUpdate = StrUpdate & "Rating3='N'"
            End If

            StrUpdate = StrUpdate & " Where Comp_Code = '" & DalUser.CurrentCompID & "'"
            Session("DalObj").ExecuteCommand(StrUpdate)

            LblErrMsg.Text = "Your Records is Sucessfully Updated!!"

        Catch ex As Exception
            LblErrMsg.Text = ex.Message
        End Try
    End Function

    Private Sub BtnOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOk.Click
        UpdateData()
    End Sub
End Class
