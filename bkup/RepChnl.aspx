<%@ Page Language="vb" AutoEventWireup="false" Codebehind="RepChnl.aspx.vb" Inherits="eHRMS.Net.RepChnl"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ORGCHART</title>
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
			<table cellSpacing="0" cellPadding="0" width="100%" align="center">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" style="FONT-SIZE: 18px; WIDTH: 49px" noWrap align="left" width="49"
						background="Images/TableMid.gif" height="19">&nbsp;</TD>
					<TD class="headingCont" style="FONT-SIZE: 18px" noWrap align="left" background="Images/TableMid.gif"
						height="19">
						Reporting Channel
					</TD>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</table>
			<br>
			<asp:Table id="tblOrg" runat="server" Width="90%"></asp:Table>
		</form>
	</body>
</HTML>
