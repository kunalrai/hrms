<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ResumeAssess.aspx.vb" Inherits="eHRMS.Net.ResumeAssess"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ResumeAssess</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<SCRIPT language="javascript" src="MenuAdmin.js"></SCRIPT>
			<br>
			<br>
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="700" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Resume 
						Assessment....
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
					<td colSpan="4"><asp:label id="LblErrMsg" runat="server" Width="100%" ForeColor="Red"></asp:label></td>
				</tr>
				<tr>
					<td>Resume Code</td>
					<td colspan="3"><asp:textbox id="TxtCode" runat="server" Width="200px" CssClass="TextBox" AutoPostBack="True"></asp:textbox><asp:dropdownlist id="cmbCode" runat="server" Width="350" AutoPostBack="True" Visible="False"></asp:dropdownlist><asp:imagebutton id="btnList" Width="18px" Height="19px" ImageUrl="Images\Find.gif" ImageAlign="AbsMiddle"
							Runat="server"></asp:imagebutton></td>
				</tr>
				<tr>
					<td>Name</td>
					<td colspan="3"><asp:Label ID="LblName" Runat="server" Width="100%"></asp:Label></td>
				</tr>
				<tr>
					<td colspan="4">&nbsp;
					</td>
				</tr>
				<tr>
					<td colspan="4">
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
