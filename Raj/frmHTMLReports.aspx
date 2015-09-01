<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmHTMLReports.aspx.vb" Inherits="eHRMS.Net.frmHTMLReports" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Reports</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<style>TH { FONT-WEIGHT: bold; FONT-SIZE: 12px; BACKGROUND: #e7e8e9; COLOR: navy; TEXT-ALIGN: center }
	TD { FONT-SIZE: 10px; COLOR: black; TEXT-ALIGN: left }
		</style>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			&nbsp;
			<asp:label id="LblErrMsg" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 40px" runat="server"
				Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" ForeColor="Red" Width="904px"></asp:label><asp:button id="CmdExport" style="Z-INDEX: 102; LEFT: 16px; POSITION: absolute; TOP: 8px" runat="server"
				Text="Export To Excel"></asp:button></form>
	</body>
</HTML>
