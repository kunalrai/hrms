<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmLoanPayment.aspx.vb" Inherits="eHRMS.Net.FrmLoanPayment"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmLoanPayment</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
		<script language="vbscript">
			Sub Val(argid)
				IF document.getElementById(argid).Checked = False THEN
					document.getElementById(replace(argid,"Chk","Dtp")).disabled = true
					document.getElementById(replace(argid,"Chk","Dtp") & "cmbDD").disabled = true
					document.getElementById(replace(argid,"Chk","Dtp") & "cmbMM").disabled = true
					document.getElementById(replace(argid,"Chk","Dtp") & "cmbYY").disabled = true
				ELSE
					document.getElementById(replace(argid,"Chk","Dtp")).disabled = false
					document.getElementById(replace(argid,"Chk","Dtp") & "cmbDD").disabled = false
					document.getElementById(replace(argid,"Chk","Dtp") & "cmbMM").disabled = false
					document.getElementById(replace(argid,"Chk","Dtp") & "cmbYY").disabled = false
				End If
			END SUB
			Sub CheckNum(argID)
				Dim TVal
				TVal = document.getElementById(argID).value
				if trim(TVal) = "" then 
					document.getElementById(argID).value = 0
				Exit Sub
				End if
				if Not IsNumeric(TVal) then
						MsgBox "Invalid Value! Please Enter numeric Value.", , "Divergent"
						document.getElementById(argID).value = 0
				End if
			End Sub	
			function ValidateCtrl()
				If document.getElementById("TxtCode").Value = "" Then
					msgbox ("Employee Code Can't be Left Blank.")
					ValidateCtrl=false
					exit function
				End If

				If document.getElementById("CmbLoanType").Value = "" Then
					msgbox ("Loan Type Can't be Left Blank.")
					ValidateCtrl=false
					exit function
				End If
				
				If document.getElementById("cmbinstmnttype").Value = "" Then
					msgbox ("Installment Type Can't be Left Blank.")
					ValidateCtrl=false
					exit function
				End If
				
				ValidateCtrl=true
			end function
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="5" rightMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx --><br>
			<br>
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="583" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Loan 
						&amp; Advance Payment....
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="2" cellPadding="0" rules="none" width="580" align="center" border="1"
				frame="box">
				<tr>
					<td width="25%"></td>
					<td width="30%"></td>
					<td width="25%"></td>
					<td width="20%"></td>
				</tr>
				<tr>
					<td colSpan="4"><asp:label id="LblErrMsg" runat="server" ForeColor="Red" Width="100%"></asp:label></td>
				</tr>
				<tr>
					<td>&nbsp;<asp:label id="Label1" text="Employee" Runat="server"></asp:label></td>
					<td colSpan="3"><asp:textbox id="TxtCode" ForeColor="#003366" Width="110" Runat="server" AutoPostBack="True"
							CssClass="textbox"></asp:textbox><asp:dropdownlist id="cmbEmp" runat="server" Width="150px" AutoPostBack="True" Visible="False"></asp:dropdownlist><asp:imagebutton id="btnList" Width="18px" Runat="server" Height="19px" ImageUrl="Images\Find.gif"
							ImageAlign="AbsMiddle"></asp:imagebutton>&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:label id="Label2" text="Name" Runat="server"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:label id="LblName" ForeColor="#003366" Width="200" Runat="server" Font-Bold="True" Font-Size="9"></asp:label></td>
				</tr>
				<tr>
					<td>&nbsp;<asp:label id="Label3" text="Loan Type" Runat="server"></asp:label></td>
					<td colSpan="3"><asp:dropdownlist id="CmbLoanType" Width="99%" Runat="server" AutoPostBack="True"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td style="HEIGHT: 20px">&nbsp;<asp:label id="Label4" text="Sanction Date" Runat="server"></asp:label></td>
					<td style="HEIGHT: 20px">
						<cc1:dtpcombo id="DtpSanction" runat="server" Width="112px" DateValue="2005-08-30" ToolTip="Date Of Sanction"></cc1:dtpcombo></td>
					<td style="HEIGHT: 20px">&nbsp;<asp:label id="Label5" text="Intrest Rate" Runat="server"></asp:label></td>
					<td style="HEIGHT: 20px"><asp:textbox id="TxtIntRate" onblur="CheckNum(this.id)" ForeColor="#003366" Width="110" Runat="server"
							CssClass="TextBox"></asp:textbox></td>
				</tr>
				<tr>
					<td>&nbsp;<asp:label id="Label13" text="Installment Type" Runat="server"></asp:label></td>
					<td colSpan="3"><asp:dropdownlist id="cmbinstmnttype" Width="99%" Runat="server">
							<asp:ListItem Value="" Selected="True"></asp:ListItem>
							<asp:ListItem Value="1">Reducing Monthly Installment (RMI)</asp:ListItem>
							<asp:ListItem Value="2">Equal Monthly Installment (EMI)</asp:ListItem>
						</asp:dropdownlist></td>
				</tr>
				<tr>
					<td>&nbsp;<asp:label id="Label7" text="Loan Amount" Runat="server"></asp:label></td>
					<td><asp:textbox id="txtloanamt" onblur="CheckNum(this.id)" ForeColor="#003366" Width="110" Runat="server"
							AutoPostBack="True" CssClass="textbox"></asp:textbox></td>
					<td>&nbsp;<asp:label id="Label8" text="Interest Amount" Runat="server"></asp:label></td>
					<td><asp:textbox id="txtintamt" onblur="CheckNum(this.id)" AutoPostBack="True" ForeColor="#003366"
							Width="110" Runat="server" CssClass="textbox"></asp:textbox></td>
				</tr>
				<tr>
					<td>&nbsp;<asp:label id="Label9" text="Installment Amount" Runat="server"></asp:label></td>
					<td><asp:textbox id="txtLinstlamt" onblur="CheckNum(this.id)" ForeColor="#003366" Width="110" Runat="server"
							AutoPostBack="True" CssClass="textbox"></asp:textbox></td>
					<td>&nbsp;<asp:label id="Label10" text="Installment Amount" Runat="server"></asp:label></td>
					<td><asp:textbox id="txtIinstlamt" onblur="CheckNum(this.id)" ForeColor="#003366" Width="110" Runat="server"
							AutoPostBack="True" CssClass="textbox"></asp:textbox></td>
				</tr>
				<tr>
					<td>&nbsp;<asp:label id="Label11" text="No. of Installment" Runat="server"></asp:label></td>
					<td><asp:textbox id="TxtLinstlNo" onblur="CheckNum(this.id)" ForeColor="#003366" Width="110" Runat="server"
							CssClass="textbox"></asp:textbox></td>
					<td>&nbsp;<asp:label id="Label12" text="No. of Installment" Runat="server"></asp:label></td>
					<td><asp:textbox id="TxtIinstlNo" onblur="CheckNum(this.id)" ForeColor="#003366" Width="110" Runat="server"
							CssClass="textbox"></asp:textbox></td>
				</tr>
				<tr>
					<td>&nbsp;<asp:label id="Label14" text="Loan Recovered" Runat="server"></asp:label></td>
					<td><asp:textbox id="TxtLRecover" onblur="CheckNum(this.id)" ForeColor="#003366" Width="110" Runat="server"
							AutoPostBack="True" CssClass="textbox" ReadOnly="True"></asp:textbox></td>
					<td>&nbsp;<asp:label id="Label15" text="Interest Recovered" Runat="server"></asp:label></td>
					<td><asp:textbox id="TxtIRrecover" onblur="CheckNum(this.id)" ForeColor="#003366" Width="110" Runat="server"
							CssClass="textbox" ReadOnly="True"></asp:textbox></td>
				</tr>
				<tr>
					<td>&nbsp;<asp:label id="Lbel6" text="Loan Balance" Runat="server"></asp:label></td>
					<td><asp:textbox id="TxtLBalance" onblur="CheckNum(this.id)" ForeColor="#003366" Width="110" Runat="server"
							CssClass="textbox" ReadOnly="True"></asp:textbox></td>
					<td>&nbsp;<asp:label id="Lbl16" text="Interest Balance" Runat="server"></asp:label></td>
					<td><asp:textbox id="TxtIBalance" onblur="CheckNum(this.id)" ForeColor="#003366" Width="110" Runat="server"
							CssClass="textbox" ReadOnly="True"></asp:textbox></td>
				</tr>
				<tr>
					<td colSpan="4">
						<table cellSpacing="0" cellPadding="0" width="580" border="0">
							<tr>
								<td>&nbsp;<asp:label id="Label16" runat="server">Recovery Start From</asp:label></td>
								<td><input id="ChkLRecFrom" onclick="Val(this.id)" type="checkbox" name="ChkLRecFrom" runat="server">
									<cc1:dtpcombo id="DtpLRecFrom" runat="server" DateValue="2006-06-30" Enabled="False"></cc1:dtpcombo></td>
								<td>&nbsp;<asp:label id="Label17" runat="server">Recovery Start From</asp:label></td>
								<td><input id="ChkIRecFrom" onclick="Val(this.id)" type="checkbox" name="ChkIRecFrom" runat="server">
									<cc1:dtpcombo id="DtpIRecFrom" runat="server" DateValue="2006-06-30" Enabled="False"></cc1:dtpcombo></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<TD>&nbsp;<asp:label id="LblExpDateL" Runat="server">Expected End Date</asp:label></TD>
					<td colSpan="3">&nbsp;<asp:label id="LblExpDate" ForeColor="#003366" Width="150" Runat="server" Font-Bold="True"
							Font-Size="9"></asp:label></td>
				</TR>
				<TR height="15">
					<TD style="MARGIN-LEFT: 15px; MARGIN-RIGHT: 15px; BORDER-BOTTOM: #993300 thin groove; HEIGHT: 7px"
						colSpan="4"></TD>
				</TR>
				<tr>
					<td></td>
					<td align="right" colSpan="3"><asp:button id="BtnSave" Width="75" Runat="server" Text="Save"></asp:button>&nbsp;
						<asp:button id="BtnCancel" Width="75" Runat="server" Text="Cancel"></asp:button>&nbsp;
						<asp:button id="BtnClose" Width="75" Runat="server" Text="Close"></asp:button>&nbsp;
					</td>
				</tr>
				<tr height="5">
					<td colSpan="4"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
