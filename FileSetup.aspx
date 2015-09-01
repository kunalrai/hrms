<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FileSetup.aspx.vb" Inherits="eHRMS.Net.FileSetup" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>TrfUtility</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx --><asp:label id="LblFdate" style="Z-INDEX: 101; LEFT: 328px; POSITION: absolute; TOP: 16px" runat="server"
				Width="88px" Font-Bold="True">Pay Date:</asp:label><cc1:dtpcombo id="dtpPdate" style="Z-INDEX: 103; LEFT: 424px; POSITION: absolute; TOP: 16px" runat="server"
				Width="100%" Height="20px" ToolTip="Pay Date"></cc1:dtpcombo><br>
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="500" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="center" background="Images/TableMid.gif" height="19">File 
						Setup&nbsp;
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" rules="none" width="500" align="center" border="1">
				<tr>
					<td style="HEIGHT: 12px" width="25%"></td>
					<td style="HEIGHT: 12px" width="75%"></td>
				</tr>
				<tr>
					<td colSpan="2"><asp:label id="lblMsg" runat="server" ForeColor="Red" Visible="False" Width="100%" Font-Size="11px"></asp:label></td>
				</tr>
				<tr>
					<td style="HEIGHT: 16px">&nbsp;File Name</td>
					<td style="HEIGHT: 16px"><asp:dropdownlist id="CmbFile" runat="server" Width="150px"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td colSpan="2"></td>
				</tr>
				<tr>
					<td colSpan="2">&nbsp;</td>
				</tr>
				<tr>
					<td>&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:button id="cmdGenerate" runat="server" Text="Generate"></asp:button></td>
					<td align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
