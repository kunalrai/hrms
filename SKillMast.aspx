<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SKillMast.aspx.vb" Inherits="eHRMS.Net.SKillMast" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>SKillMaster</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCSTyles.Css" type="text/css" rel="stylesheet">
		<script language="javascript" src='coolmenus.js"'></script>
		<script language="javascript" src="common.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx --><br>
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="400" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Skill 
						Master....
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="2" cellPadding="0" rules="none" width="400" align="center" border="1"
				frame="box">
				<tr>
					<td width="23%"></td>
					<td width="47%"></td>
					<td width="30%"></td>
				</tr>
				<tr>
					<td colSpan="3"><asp:label id="LblErrMsg" runat="server" ForeColor="Red" Width="100%"></asp:label></td>
				</tr>
				<tr>
					<td>&nbsp;Skill Code</td>
					<td><asp:textbox id="TxtCode" ForeColor="#003366" CssClass="textbox" Runat="server" AutoPostBack="True"
							width="75px"></asp:textbox><asp:dropdownlist id="cmbCode" runat="server" Width="150" AutoPostBack="True" Visible="False"></asp:dropdownlist><asp:imagebutton id="btnList" Width="18px" Runat="server" ImageAlign="AbsMiddle" ImageUrl="Images\Find.gif"></asp:imagebutton><asp:imagebutton id="btnNew" Width="18px" Runat="server" ImageAlign="AbsMiddle" ImageUrl="Images\NewFile.ico"></asp:imagebutton></td>
					<td><asp:dropdownlist id="CmbType" Width="125" Runat="server" AutoPostBack="True">
							<asp:ListItem Selected="True"></asp:ListItem>
							<asp:ListItem Value="1">Header</asp:ListItem>
							<asp:ListItem Value="2">Set</asp:ListItem>
							<asp:ListItem Value="3">Skills</asp:ListItem>
						</asp:dropdownlist></td>
				</tr>
				<tr>
					<td>&nbsp;Description</td>
					<td colSpan="2"><asp:textbox id="TxtDesc" ForeColor="#003366" Width="100%" CssClass="TextBox" Runat="server"></asp:textbox>
				<tr>
					<td>&nbsp;Header Group</td>
					<td colSpan="2"><asp:dropdownlist id="cmbHgroup" runat="server" Width="100%" CssClass="TextBox" AutoPostBack="True"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td>&nbsp;Under Group</td>
					<td colSpan="2"><asp:dropdownlist id="cmbUgroup" runat="server" Width="100%" CssClass="TextBox"></asp:dropdownlist></td>
				</tr>
				<TR><TD colspan="3" height="5" ></td></tr>
				<tr>
					<td class="Header3" background="Images\headstripe.jpg" colSpan="3"><b>Skills</b></td>
				</tr>
				<tr>
					<td colSpan="3">
						<table id="TblSkills" cellSpacing="0" cellPadding="0" rules="none" width="100%" bgColor="#f3f3f3"
							border="1" frame="border" runat="server">
						</table>
					</td>
				</tr>
				<TR height="15">
					<TD style="MARGIN-LEFT: 15px; MARGIN-RIGHT: 15px; BORDER-BOTTOM: #993300 thin groove; HEIGHT: 7px"
						colSpan="3"></TD>
				</TR>
				<tr>
					<td align="right" colSpan="3"><asp:button id="cmdSave" runat="server" Width="75px" Text="Save"></asp:button>&nbsp;
						<asp:button id="cmdClose" runat="server" Width="75px" Text="Close"></asp:button></td>
					</TD></tr>
			</table>
		</form>
	</body>
</HTML>
