<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TrainingMast.aspx.vb" Inherits="eHRMS.Net.TrainingMast"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>TrainingMast</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="coolmenus4.js"></script>
		<script language="javascript" src="Common.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx -->
			<br>
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="700" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Training 
						Master ....
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" width="700" border="1" frame="border" rules="none">
				<tr>
					<td width="15%"></td>
					<td width="35%"></td>
					<td width="15%"></td>
					<td width="35%"></td>
				</tr>
				<tr>
					<td colspan="4"><asp:label id="LblErrMsg" runat="server" Width="100%" ForeColor="Red"></asp:label>
					</td>
				</tr>
				<tr>
					<td>Code</td>
					<td><asp:textbox id="TxtCode" runat="server" Width="75px" AutoPostBack="True" CssClass="TextBox"
							ForeColor="#003366"></asp:textbox>
						<asp:dropdownlist id="cmbCode" runat="server" Width="200" AutoPostBack="True" Visible="False"></asp:dropdownlist>
						<asp:imagebutton id="btnList" Width="18px" Height="19px" ImageUrl="Images\Find.gif" ImageAlign="AbsMiddle"
							Runat="server"></asp:imagebutton>
						<asp:imagebutton id="btnNew" Width="18px" Height="19px" ImageUrl="Images\NewFile.ico" ImageAlign="AbsMiddle"
							Runat="server"></asp:imagebutton></td>
					<td>Budget</td>
					<td><asp:textbox id="TxtBudget" runat="server" Width="80%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
				</tr>
				<tr>
					<td>Description</td>
					<td colSpan="3"><asp:textbox id="TxtDescription" runat="server" Width="92%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
				</tr>
				<tr>
					<td style="HEIGHT: 14px">Training Group</td>
					<td colSpan="3" style="HEIGHT: 14px"><asp:dropdownlist id="cmbTraiGrp" runat="server" Width="200px"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td class="Header3" background="Images\headstripe.jpg" colSpan="4"><b>Skills</b></td>
				</tr>
				<tr>
					<td colSpan="4">
						<table id="TblSkills" bgcolor="#f3f3f3" cellSpacing="0" cellPadding="0" rules="none" width="100%"
							border="1" frame="border" runat="server">
						</table>
					</td>
				</tr>
				<tr>
					<td align="right" colSpan="4"><asp:button id="cmdSave" runat="server" Width="80px" Text="Save"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:button id="cmdClose" runat="server" Width="80px" Text="Close"></asp:button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
