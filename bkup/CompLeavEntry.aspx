<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CompLeavEntry.aspx.vb" Inherits="eHRMS.Net.CompLeavEntry" %>
<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>CompLeavEntry</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx --><br>
			<br>
			<TABLE cellSpacing="0" cellPadding="0" align="center"  width="450" border="0">
				<tr id="Head" runat="server">
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="header3" noWrap width="95%" background="Images/TableMid.gif" height="19"><b>Compensatory 
							Leave Entry...</b></TD>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table borderColor="gray" cellSpacing="0" align="center" cellPadding="0" rules="none" width="450" border="1"
				frame="border">
				<tr>
					<td width="15%"></td>
					<td width="50%"></td>
					<td width="35%"></td>
				</tr>
				<tr>
					<td colSpan="3"><asp:label id="LblErrMsg" runat="server" ForeColor="Red"></asp:label></td>
				</tr>
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Code</td>
					<td><asp:textbox id="TxtCode" CssClass="textbox" AutoPostBack="True" Runat="server" Width="80"></asp:textbox><asp:dropdownlist id="cmbCode" runat="server" AutoPostBack="True" Width="150" Visible="False"></asp:dropdownlist><asp:imagebutton id="btnList" Runat="server" Width="18px" Height="19px" ImageUrl="Images\Find.gif"
							ImageAlign="AbsMiddle"></asp:imagebutton>&nbsp;&nbsp;&nbsp;</td>
					<td class="Header3"><asp:label id="LblName" Runat="server" Visible="False"></asp:label></td>
				</tr>
				<tr>
					<td>Date</td>
					<td><cc1:dtpcombo id="dtpDOCompLv" runat="server" Width="200px" ToolTip="Date Of Compensatory Leave"
							Enabled="true" DateValue="2005-10-05"></cc1:dtpcombo></td>
					<td><asp:radiobutton id="RdoFull" runat="server" Text="Full Day" GroupName="a" Checked="True"></asp:radiobutton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:radiobutton id="RdoHalf" runat="server" Text="Half Day" GroupName="a"></asp:radiobutton></td>
				</tr>
				<tr>
					<td>Remarks</td>
					<td colSpan="2"><asp:textbox id="TxtRemarks" runat="server" Width="100%" CssClass="textbox"></asp:textbox></td>
				</tr>
				<tr>
					<td align="right" colSpan="3"><asp:button id="CmdSave" runat="server" Width="70px" Text="Save"></asp:button>&nbsp;&nbsp;
						<asp:button id="CmdClose" runat="server" Width="70px" Text="Close"></asp:button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
