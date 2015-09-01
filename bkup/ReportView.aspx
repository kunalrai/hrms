<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ReportView.aspx.vb" Inherits="eHRMS.Net.ReportView" %>
<%@ Register TagPrefix="cr" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=9.1.5000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ReportView</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="javascript">
		</SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:button id="CmdExport" style="Z-INDEX: 106; LEFT: 16px; POSITION: absolute; TOP: 8px" runat="server"
				Text="Export" Width="56px"></asp:button><asp:textbox id="TxtTO" style="Z-INDEX: 104; LEFT: 896px; POSITION: absolute; TOP: 8px" runat="server"
				Width="40px" Visible="False">1</asp:textbox><asp:button id="CmdPage" style="Z-INDEX: 102; LEFT: 680px; POSITION: absolute; TOP: 8px" runat="server"
				Text="Print" Width="59px"></asp:button><asp:textbox id="TxtFrom" style="Z-INDEX: 103; LEFT: 824px; POSITION: absolute; TOP: 8px" runat="server"
				Width="40px" Visible="False">1</asp:textbox><asp:label id="LblPages" style="Z-INDEX: 105; LEFT: 16px; POSITION: absolute; TOP: 40px" runat="server"
				Width="976px" Font-Size="X-Small" ForeColor="Blue"></asp:label>
			<asp:dropdownlist id="CmbExport" style="Z-INDEX: 107; LEFT: 80px; POSITION: absolute; TOP: 8px" runat="server"
				Width="80px" Height="24px">
				<asp:ListItem Value="EXL" Selected="True">Excel</asp:ListItem>
				<asp:ListItem Value="PDF">PDF</asp:ListItem>
				<asp:ListItem Value="Doc">Word Document</asp:ListItem>
			</asp:dropdownlist><asp:label id="Label1" style="Z-INDEX: 108; LEFT: 744px; POSITION: absolute; TOP: 8px" runat="server"
				Visible="False" Font-Size="X-Small" Font-Bold="True">Pages From</asp:label><asp:label id="Label2" style="Z-INDEX: 109; LEFT: 872px; POSITION: absolute; TOP: 8px" runat="server"
				Visible="False" Font-Size="X-Small" Font-Bold="True">To</asp:label><asp:textbox id="TxtPrint" style="Z-INDEX: 112; LEFT: 1040px; POSITION: absolute; TOP: 8px" runat="server"
				Width="0px"></asp:textbox></form>
	</body>
</HTML>
