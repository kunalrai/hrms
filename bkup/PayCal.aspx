<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PayCal.aspx.vb" Inherits="eHRMS.Net.PayCalForm" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Pay Calculation</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
		<SCRIPT language="javascript">
			function SayHello1()
				{
				alert("hell");
				window.document.getElementById("cmdCalc").disabled=false;
				//window.document.Form1.submit();
				}
		</SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx --><br>
			<br>
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="400" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Pay 
						Calculation....
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" rules="none" width="400" align="center" border="1"
				frame="border">
				<tr>
					<td colSpan="4"><asp:label id="lblMsg" runat="server" Font-Size="11px" ForeColor="Red"></asp:label><br>
					</td>
				<tr>
					<td width="30%" colSpan="1">&nbsp;Month</td>
					<td colSpan="3"><asp:dropdownlist id="cmbMonth" runat="server" Width="100%"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td width="30%" style="HEIGHT: 18px">&nbsp;For</td>
					<td style="HEIGHT: 18px" colSpan="3"><asp:dropdownlist id="cmbSearchFld" runat="server" Width="100%" Height="24px" AutoPostBack="True"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td style="HEIGHT: 12px">&nbsp;</td>
					<td colSpan="3" style="HEIGHT: 12px"><asp:dropdownlist id="cmbMastList" runat="server" Width="100%"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td vAlign="top" align="left">&nbsp;Criteria</td>
					<td align="center" colSpan="3"><asp:textbox id="TxtFilter" runat="server" Width="100%" Height="85px" Rows="4" TextMode="MultiLine"
							CssClass="textbox" ForeColor="#003366"></asp:textbox></td>
				</tr>
				<tr>
					<td>&nbsp;
						<asp:Button id="CmdView" runat="server" Width="75px" Text="View" Enabled="False"></asp:Button></td>
					<td align="center" colSpan="1"><asp:button id="cmdCalc" runat="server" Width="80px" Text="Calculate"></asp:button></td>
					<td align="center" colSpan="1"><asp:button id="cmdCancel" runat="server" Width="80px" Text="Cancel"></asp:button></td>
					<td align="center" colSpan="1"><asp:button id="cmdClose" runat="server" Width="80px" Text="Close"></asp:button>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
