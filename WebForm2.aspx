<%@ Page Language="vb" AutoEventWireup="false" Codebehind="WebForm2.aspx.vb" Inherits="eHRMS.Net.WebForm2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Export Excel/CSV</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<P align="center"><STRONG><U>Dummy Datatable</U></STRONG></P>
			<asp:DataGrid id="DataGrid1" runat="server" BorderStyle="Solid" BorderColor="#404040" Width="100%"
				CellPadding="5"></asp:DataGrid>
			<P>
				<asp:Button id="Button1" runat="server" Text="Export to excel"></asp:Button>&nbsp;
				<asp:Button id="Button2" runat="server" Text="Export to CSV"></asp:Button></P>
		</form>
	</body>
</HTML>
