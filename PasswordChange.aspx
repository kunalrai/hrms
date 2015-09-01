<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PasswordChange.aspx.vb" Inherits="eHRMS.Net.PasswordChange"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PasswordChange</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx -->
			<br>
			<br>
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="350" border="0" align="center">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Change 
						Password....
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" rules="none" width="350" border="1" frame="border"
				align="center">
				<tr>
					<td width="35%">
					</td>
					<td width="65%"></td>
				</tr>
				<tr>
					<td colspan="2"><asp:label id="lblMsg" runat="server" ForeColor="Red" Font-Size="11px"></asp:label>
						<br>
						<asp:CompareValidator id="CompareValidator1" runat="server" ErrorMessage="Password does not mached." ControlToValidate="TxtConPass"
							ControlToCompare="TxtNewPass"></asp:CompareValidator></td>
				</tr>
				<tr>
					<td>&nbsp;User Id</td>
					<td>
						<asp:TextBox id="TxtCode" runat="server" CssClass="textbox" Enabled="False"></asp:TextBox></td>
				</tr>
				<tr>
					<td>&nbsp;User Name</td>
					<td>
						<asp:TextBox id="TxtUserName" runat="server" CssClass="textbox" Enabled="False"></asp:TextBox></td>
				</tr>
				<tr>
					<td>&nbsp;Old Password</td>
					<td>
						<asp:TextBox id="TxtPass" runat="server" CssClass="textbox" TextMode="Password"></asp:TextBox></td>
				</tr>
				<tr>
					<td>&nbsp;New Password</td>
					<td>
						<asp:TextBox id="TxtNewPass" runat="server" CssClass="textbox" TextMode="Password"></asp:TextBox></td>
				</tr>
				<tr>
					<td>&nbsp;Confirm Password</td>
					<td>
						<asp:TextBox id="TxtConPass" runat="server" CssClass="textbox" TextMode="Password"></asp:TextBox></td>
				</tr>
				<tr>
					<td colspan="2">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="2" align="right">
						<asp:Button id="cmdSave" runat="server" Text="Save" Width="80px"></asp:Button>&nbsp;&nbsp;&nbsp;
						<asp:Button id="cmdClose" runat="server" Text="Close" Width="80px"></asp:Button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
