<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TrfUtility.aspx.vb" Inherits="eHRMS.Net.TrfUtility"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>TrfUtility</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx --><br>
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="500" border="0" align="center">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="center" background="Images/TableMid.gif" height="19">Transfer 
						Utility...
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" rules="none" width="500" border="1" align="center">
				<tr>
					<td width="25%"></td>
					<td width="75%"></td>
				</tr>
				<tr>
					<td colspan="2"><asp:label id="lblMsg" runat="server" Font-Size="11px" Width="100%" Visible="False" ForeColor="Red"></asp:label></td>
				</tr>
				<tr>
					<td>&nbsp;For the Month
					</td>
					<td><asp:DropDownList id="CmbFor" runat="server" Width="150px"></asp:DropDownList></td>
				</tr>
				<tr>
					<td>&nbsp;From Database</td>
					<td>
						<asp:TextBox id="TxtDatabase" runat="server" Width="150px" CssClass="textbox"></asp:TextBox></td>
				</tr>
				<tr>
					<td>&nbsp;Transfer</td>
					<td><asp:DropDownList id="cmbTransfer" runat="server" Width="150px" AutoPostBack="True">
							<asp:ListItem Value="1" Selected="True">PayHist</asp:ListItem>
							<asp:ListItem Value="2">HrdHist</asp:ListItem>
							<asp:ListItem Value="3">PayMast</asp:ListItem>
							<asp:ListItem Value="4">ReimMast</asp:ListItem>
							<asp:ListItem Value="5">ReimTran</asp:ListItem>
						</asp:DropDownList></td>
				</tr>
				<tr>
					<td colspan="2">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="2">
						<asp:TextBox id="TxtQuery" runat="server" Width="100%" TextMode="MultiLine" Rows="8"></asp:TextBox></td>
				</tr>
				<tr>
					<td colspan="2">&nbsp;</td>
				</tr>
				<tr>
					<td><asp:Button id="cmdReset" runat="server" Text="Reset"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;
					</td>
					<td align="right">
						<asp:Button id="cmdExe" runat="server" Text="Execute" Visible="False"></asp:Button>&nbsp;&nbsp;
						<asp:Button id="CmdSet" runat="server" Text="Set Query"></asp:Button>&nbsp;&nbsp;
						<asp:Button id="CmdCose" runat="server" Text="Close"></asp:Button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
