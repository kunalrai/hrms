<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ReportView.aspx.vb" Inherits="eHRMS.Net.ReportView" %>
<%@ Register TagPrefix="cr" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=9.1.5000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>
<!DOCTYPE HTML PUBLIC`"-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ReportView</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<CR:CRYSTALREPORTVIEWER id="CrRep" style="Z-INDEX: 101; LEFT: 16px; POSITION: absolute; TOP: 56px" runat="server"
				Height="50px" Width="350px"></CR:CRYSTALREPORTVIEWER><asp:button id="CmdExport" style="Z-INDEX: 106; LEFT: 16px; POSITION: absolute; TOP: 8px" runat="server"
				Width="56px" Text="Export"></asp:button><asp:textbox id="TxtTO" style="Z-INDEX: 104; LEFT: 896px; POSITION: absolute; TOP: 8px" runat="server"
				Width="40px" Visible="False">1</asp:textbox><asp:button id="CmdPage" style="Z-INDEX: 102; LEFT: 680px; POSITION: absolute; TOP: 8px" runat="server"
				Width="59px" Text="Print"></asp:button><asp:textbox id="TxtFrom" style="Z-INDEX: 103; LEFT: 824px; POSITION: absolute; TOP: 8px" runat="server"
				Width="40px" Visible="False">1</asp:textbox><asp:label id="LblPages" style="Z-INDEX: 105; LEFT: 16px; POSITION: absolute; TOP: 40px" runat="server"
				Width="976px" ForeColor="Blue" Font-Size="X-Small"></asp:label><asp:dropdownlist id="CmbExport" style="Z-INDEX: 107; LEFT: 80px; POSITION: absolute; TOP: 8px" runat="server"
				Height="24px" Width="80px">
				<asp:ListItem Value="EXL" Selected="True">Excel</asp:ListItem>
				<asp:ListItem Value="PDF">PDF</asp:ListItem>
				<asp:ListItem Value="Doc">Word Document</asp:ListItem>
			</asp:dropdownlist>
			<asp:Label id="Label1" style="Z-INDEX: 108; LEFT: 744px; POSITION: absolute; TOP: 8px" runat="server"
				Font-Bold="True" Font-Size="X-Small" Visible="False">Pages From</asp:Label>
			<asp:Label id="Label2" style="Z-INDEX: 109; LEFT: 872px; POSITION: absolute; TOP: 8px" runat="server"
				Font-Bold="True" Font-Size="X-Small" Visible="False">To</asp:Label>
			<asp:TextBox id="TxtPrint" style="Z-INDEX: 112; LEFT: 1040px; POSITION: absolute; TOP: 8px" runat="server"
				Width="0px"></asp:TextBox></form>
	</body>
</HTML>
