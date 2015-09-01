<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="BalanceUpdation.aspx.vb" Inherits="eHRMS.Net.BalanceUpdation" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>BalanceUpdation</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx -->
			<br>
			<br>
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="475" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="15">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Balance 
						Updation
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" height="19" src="Images/TableRight.gif" width="27"></TD>
				</tr>
			</TABLE>
			<TABLE cellSpacing="1" cellPadding="0" rules="none" width="475" align="center" border="1"
				frame="box">
				<tr>
					<td width="20%"></td>
					<td width="30%"></td>
					<td width="25%"></td>
					<td width="25%"></td>
				</tr>
				<tr>
					<td colSpan="4"><asp:label id="LblErrMsg" runat="server" ForeColor="Red" Width="100%"></asp:label></td>
				</tr>
				<tr>
					<td colSpan="4">
						<table height="40" cellSpacing="1" cellPadding="0" width="100%" border="1">
							<tr>
								<td align="left">&nbsp;<asp:label id="Label1" text="AS On Date" Runat="server"></asp:label></td>
								<td align="center">
									<cc1:dtpcombo id="DtpDate" runat="server" Width="112px" ToolTip="Balance Update Date" DateValue="2005-08-30"></cc1:dtpcombo></td>
								<td align="left">&nbsp;<asp:label id="LblType" text="Updation Type" Runat="server"></asp:label></td>
								<td align="center">
									<asp:dropdownlist id="cmbUPType" runat="server" Width="136px">
										<asp:ListItem Selected="True" Value="1">Leave</asp:ListItem>
										<asp:ListItem Value="2">Reimbursement</asp:ListItem>
										<asp:ListItem Value="3">Loan & Advance</asp:ListItem>
										<asp:ListItem Value="4">Master & transaction</asp:ListItem>
										<asp:ListItem Value="5">Reim Prodate Update</asp:ListItem>
									</asp:dropdownlist></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td align="left">&nbsp;<asp:label id="Label2" text="Criteria" Runat="server"></asp:label></td>
				</tr>
				<tr>
					<td colSpan="4">
						<table cellSpacing="2" cellPadding="0" width="100%" border="1">
							<tr>
								<td colSpan="4"><asp:textbox id="TxtCriteria" Width="460px" Font-Size="12px" Runat="server" TextMode="MultiLine"
										Height="75px" ForeColor="#003366"></asp:textbox></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="4" height="10"></td>
				</tr>
				<tr>
					<td colspan="4">
						<table cellpadding="0" cellspacing="1" border="1" frame="box" width="100%">
							<tr>
								<td width="40">&nbsp;<asp:label id="Label4" Runat="server" text="For"></asp:label>
								<td width="200"><asp:DropDownList ID="cmbSearchFld" Runat="server" Width="100%" AutoPostBack="True"></asp:DropDownList></td>
								<td width="210"><asp:DropDownList ID="cmbMastList" Width="100%" Runat="server"></asp:DropDownList></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="4" height="5"></td>
				</tr>
				<tr>
					<td align="left"></td>
					<td align="left"></td>
					<td align="right" colspan="2">
						<asp:button id="cmdok" runat="server" Width="75px" Text="OK"></asp:button>&nbsp;
						<asp:button id="cmdcancel" runat="server" Width="75px" Text="Cancel"></asp:button>&nbsp;
					</td>
				</tr>
				<tr>
					<td colspan="4" height="5"></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
