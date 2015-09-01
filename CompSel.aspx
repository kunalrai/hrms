<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CompSel.aspx.vb" Inherits="eHRMS.Net.CompSelReLogin"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>CompSelReLogin</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<br>
			<br>
			<br>
			<br>
			<table cellpadding="0" cellspacing="0" width="600" style="BORDER-RIGHT: double; BORDER-TOP: double; BORDER-LEFT: double; BORDER-BOTTOM: double"
				height="250" border="1" rules="none" align="center" frame="border">
				<tr valign="middle">
					<td height="20">
						<table cellpadding="0" cellspacing="0" width="100%" border =1 rules =none frame =below>
							<tr>
								<td align="center">
									<p><font color="#003366" size="4">Your Session has been expired.</font></p>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td height="20"></td>
				</tr>
				<tr valign="top">
					<td>
						<p><font size="2">There would be two reason, Why your session expired ?</font></p>
						<p><font size="2">1. You are accessing site for more than 12 hours.</font>
						</p>
						<p><font size="2">2. Your session has been idle for more than 20 minutes.</font>
						</p>
					</td>
				</tr>
				<tr>
					<td class="Header3" height="20" align="center"><a href="login.aspx" target="Main"><font size="2"><b>---- Relogin HRMS ----</b></font></a></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
