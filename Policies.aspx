<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Policies.aspx.vb" Inherits="eHRMS.Net.Policies"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Policies</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<br>
			<table id="A" runat="server">
			</table>
			<TABLE cellSpacing="0" cellPadding="0" width="300" border="0">
				<tr>
					<TD noWrap width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" style="FONT-SIZE: 18px" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" style="FONT-SIZE: 10pt" noWrap align="left" background="Images/TableMid.gif"
						height="19">Download Material....
					</TD>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table id="TblPolicies" cellSpacing="0" cellPadding="0" width="300" border="1" runat="server">
				<tr>
					<td><asp:label id="LblErrMsg" runat="server" ForeColor="Red"></asp:label></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
