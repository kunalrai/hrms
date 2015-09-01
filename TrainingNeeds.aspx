<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TrainingNeeds.aspx.vb" Inherits="eHRMS.Net.TrainingNeeds" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>TrainingNeeds</title>
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
			<TABLE cellSpacing="0" cellPadding="0" width="700" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Training 
						Needs ....
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" rules="none" width="700" border="1" frame="border">
				<tr>
					<td width="15%"></td>
					<td width="35%"></td>
					<td width="15%"></td>
					<td width="35%"></td>
				</tr>
				<tr>
					<td colSpan="4"><asp:label id="LblErrMsg" runat="server" ForeColor="Red" Width="100%"></asp:label></td>
				</tr>
				<tr>
					<td>Employee</td>
					<td colSpan="3"><asp:dropdownlist id="cmbCode" runat="server" Width="200" AutoPostBack="True"></asp:dropdownlist></td>
				</tr>
				<tr><td colspan =4>&nbsp;</td></tr>
				<tr>
					<td class="Header3" background="Images\headstripe.jpg" colSpan="2"><b>Current Skills</b></td>
					<td class="Header3" background="Images\headstripe.jpg" colSpan="2"><b>Targeted Skills</b></td>
				<tr>
					<td vAlign="top" colSpan="2">
						<table id="TblSkills1" cellSpacing="0" cellPadding="0" rules="none" width="100%" bgColor="#f3f3f3"
							border="1" frame="border" runat="server">
						</table>
					</td>
					<td vAlign="top" colSpan="2">
						<table id="TblSkills2" cellSpacing="0" cellPadding="0" rules="none" width="100%" bgColor="#f3f3f3"
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
