<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ProgressBar.aspx.vb" Inherits="eHRMS.Net.ProgressBar"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>SEMS</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<META http-equiv="REFRESH" content="5">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<Script language="vbscript">
		sub SayHello()
			window.opener.document.getelementbyid("Form1").submit
			window.close
		End Sub
		</Script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:label id="lblMsg" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" runat="server"
				Font-Size="11px" ForeColor="Red" Visible="True"></asp:label></form>
	</body>
</HTML>
