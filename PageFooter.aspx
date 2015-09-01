<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PageFooter.aspx.vb" Inherits="eHRMS.Net.PageFooter"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PageFooter</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<!--<META HTTP-EQUIV="REFRESH" CONTENT=1>-->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Frm" method="post" runat="server">
			<table cellpadding="0" cellspacing="0" width="100%">
				<% if Not isnothing(Session("LoginUser")) Then  %>
				<tr>
					<td align="center">
						<asp:HyperLink id="HYLPVarification" runat="server" Width="128px" Height="8px" Target="Exp" NavigateUrl="Email.aspx">
					Update Information</asp:HyperLink>
					</td>
				</tr>
				<% End if %>
			</table>
		</form>
	</body>
</HTML>
