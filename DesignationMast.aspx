<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DesignationMast.aspx.vb" Inherits="eHRMS.Net.DesignationMast"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>DesignationMast</title>
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
			<!--#include file=MenuBars.aspx -->
			<br>
			<br>
			<table borderColor="gray" cellSpacing="0" cellPadding="0" width="600" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19"></TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Designation 
						Master
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
					<td style="HEIGHT: 24px">Code</td>
					<td style="HEIGHT: 24px"><asp:textbox id="TxtCode" runat="server" AutoPostBack="True" CssClass="TextBox" Width="75px"
							ForeColor="#003366"></asp:textbox><asp:dropdownlist id="cmbCode" runat="server" AutoPostBack="True" Width="280px" Visible="False"></asp:dropdownlist><asp:imagebutton id="btnList" Width="18px" Runat="server" ImageAlign="AbsMiddle" ImageUrl="Images\Find.gif"
							Height="19px"></asp:imagebutton>
						<asp:imagebutton id="btnNew" Width="18px" Height="19px" ImageUrl="Images\NewFile.ico" ImageAlign="AbsMiddle"
							Runat="server" ToolTip="Add New Record"></asp:imagebutton></td>
				</tr>
				<tr>
					<td>Designation Name</td>
					<td><asp:textbox id="TxtDesc" tabIndex="1" runat="server" CssClass="TextBox" Width="280px" ForeColor="#003366"></asp:textbox></td>
				</tr>
				<tr>
					<td style="HEIGHT: 37px">Under Designation</td>
					<td style="HEIGHT: 37px"><asp:dropdownlist id="cmbDesignation" runat="server" Width="280px"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td class="Header3" background="Images\headstripe.jpg" colSpan="2"><b>Skills</b></td>
				</tr>
				<tr>
					<td vAlign="top" colSpan="2">
						<table id="TblSkills" cellSpacing="0" cellPadding="0" rules="none" width="100%" bgColor="#f3f3f3"
							border="1" frame="border" runat="server">
						</table>
					</td>
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
