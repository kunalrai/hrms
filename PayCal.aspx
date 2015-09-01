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
			<table borderColor="gray" cellSpacing="1" cellPadding="1" rules="none" width="400" align="center"
				border="1" frame="border">
				<tr>
					<td colSpan="4"><asp:label id="lblMsg" runat="server" ForeColor="Red" Font-Size="11px"></asp:label><br>
					</td>
				<tr>
					<td width="30%" colSpan="1">&nbsp;Month</td>
					<td colSpan="3"><asp:dropdownlist id="cmbMonth" runat="server" Width="100%"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td style="HEIGHT: 4px" width="30%">&nbsp;For</td>
					<td style="HEIGHT: 4px" colSpan="3"><asp:dropdownlist id="cmbSearchFld" runat="server" Width="100%" AutoPostBack="True" Height="24px"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td style="HEIGHT: 12px">&nbsp;</td>
					<td style="HEIGHT: 12px" colSpan="3"><asp:dropdownlist id="cmbMastList" runat="server" Width="100%"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td vAlign="top" align="left">&nbsp;Criteria</td>
					<td align="center" colSpan="3"><asp:textbox id="TxtFilter" runat="server" ForeColor="#003366" Width="100%" Height="85px" CssClass="textbox"
							TextMode="MultiLine" Rows="4"></asp:textbox></td>
				</tr>
				<TR height="15">
					<TD style="MARGIN-LEFT: 15px; MARGIN-RIGHT: 15px; BORDER-BOTTOM: #993300 thin groove; HEIGHT: 7px"
						colSpan="4"></TD>
				</TR>
				<tr>
					<td>&nbsp;
						<asp:button id="CmdView" runat="server" Width="75px" Enabled="False" Text="View"></asp:button></td>
					<td align="center" colSpan="1"><asp:button id="cmdCalc" runat="server" Width="80px" Text="Calculate"></asp:button></td>
					<td align="center" colSpan="1"><asp:button id="cmdCancel" runat="server" Width="80px" Text="Cancel"></asp:button></td>
					<td align="center" colSpan="1"><asp:button id="cmdClose" runat="server" Width="80px" Text="Close"></asp:button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
