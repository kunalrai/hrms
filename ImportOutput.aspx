<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ImportOutput.aspx.vb" Inherits="eHRMS.Net.ImportOutput"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ImportOutput</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			&nbsp;
			<HR>
			<DIV align="center"><asp:datagrid id="grdImport" runat="server" AutoGenerateColumns="True">
					<HeaderStyle Font-Bold="True" ForeColor="Black" BorderStyle="Solid" BorderColor="Black" BackColor="#C0C0FF"></HeaderStyle>
				</asp:datagrid><BR>
			</DIV>
		</form>
	</body>
</HTML>
