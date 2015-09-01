<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Menus.aspx.vb" Inherits="eHRMS.Net.Menus"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Menus</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<Script language="vbscript">
			Sub LoadBlank()
					window.open "Blank.aspx","Screens","",false
			End Sub
		</Script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgcolor="#f1f1f1">
		<form id="Form1" method="post" runat="server">
			<asp:Table id="tblMenu" width="95%" style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 0px"
				runat="server"></asp:Table>
		</form>
	</body>
</HTML>
