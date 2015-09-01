<%@ Page Language="vb" AutoEventWireup="false" Codebehind="StateMast.aspx.vb" Inherits="eHRMS.Net.StateMast"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Location Master</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
		<SCRIPT language="javascript" src="Common.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx --><br>
			<br>
			<table borderColor="gray" cellSpacing="0" cellPadding="0" width="600" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">State&nbsp;Master
					</TD>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</table>
			<table borderColor="gray" cellSpacing="0" cellPadding="0" rules="none" width="600" align="center"
				border="1" frame="border">
				<tr>
					<td width="25%"></td>
					<td width="75%"></td>
				</tr>
				<tr>
					<td width="100%" colSpan="2"><asp:label id="LblErrMsg" runat="server" ForeColor="Red"></asp:label></td>
				</tr>
				<tr>
					<td>Code</td>
					<td><asp:textbox id="TxtCode" runat="server" ForeColor="#003366" Width="75px" CssClass="TextBox"
							AutoPostBack="True"></asp:textbox><asp:dropdownlist id="cmbCode" runat="server" Width="280px" AutoPostBack="True" Visible="False"></asp:dropdownlist><asp:imagebutton id="btnList" Width="18px" Height="19px" ImageUrl="Images\Find.gif" ImageAlign="AbsMiddle"
							Runat="server"></asp:imagebutton><asp:imagebutton id="btnNew" Width="18px" Height="19px" ImageUrl="Images\NewFile.ico" ImageAlign="AbsMiddle"
							Runat="server" ToolTip="Add New Record"></asp:imagebutton></td>
				</tr>
				<tr>
					<td>Description</td>
					<td><asp:textbox id="TxtDesc" tabIndex="1" runat="server" ForeColor="#003366" Width="280px" CssClass="TextBox"></asp:textbox></td>
				</tr>
				<tr>
					<td class="Header3" background="Images\headstripe.jpg" colSpan="2"><b>Professional Tax 
							Formula</b></td>
				</tr>
				<tr>
					<td vAlign="top" colSpan="2"><asp:textbox id="txtPTax" runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox"
							AutoPostBack="True" Height="100px" Rows="0"></asp:textbox></td>
				</tr>
				<tr>
					<td colSpan="2">
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td align="right" width="70%"></td>
								<td align="right" width="15%"><asp:button id="cmdSave" runat="server" Width="75px" Text="Save"></asp:button></td>
								<td align="right" width="15%"><asp:button id="cmdClose" runat="server" Width="75px" Text="Close"></asp:button></td>
							</tr>
						</TABLE>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
