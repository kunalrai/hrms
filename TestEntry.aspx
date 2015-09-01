<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TestEntry.aspx.vb" Inherits="eHRMS.Net.TestEntry"%>
<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>TestEntry</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
		<SCRIPT language="javascript" src="Common.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<br>
			<br>
			<table cellSpacing="0" cellPadding="0" width="500" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Test 
						Entry Detail ....
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</table>
			<table cellSpacing="0" cellPadding="0" rules="none" width="500" align="center" border="1">
				<tr>
					<td width="20%"></td>
					<td width="45%"></td>
					<td width="18%"></td>
					<td width="17%"></td>
				</tr>
				<tr>
					<td colSpan="4"><asp:label id="LblErrMsg" runat="server" Width="100%" ForeColor="Red"></asp:label></td>
				</tr>
				<tr>
					<td colSpan="1">&nbsp; Training &nbsp;&nbsp;Session</td>
					<td colSpan="3"><asp:dropdownlist id="CmbSession" Width="296px" Runat="server" AutoPostBack="True"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td>&nbsp;&nbsp;Test Name</td>
					<td><asp:dropdownlist id="CmbTname" Width="216px" Runat="server" AutoPostBack="True"></asp:dropdownlist></td>
					<td style="WIDTH: 108px"><asp:radiobutton id="RadioFtst" Runat="server" AutoPostBack="True" Text="First Test"></asp:radiobutton>&nbsp;&nbsp;</td>
					<td><asp:radiobutton id="RadioRtst" Runat="server" AutoPostBack="True" Text=" Re Test"></asp:radiobutton>&nbsp;&nbsp;</td>
				</tr>
				<tr>
					<td class="Header3" colSpan="4"><b>Test Details...</b></td>
				</tr>
				<tr>
					<td align="right" colSpan="4">Test Date&nbsp;
						<cc1:dtp id="dtpInterview" runat="server" width="136px" ToolTip="Interview Date"></cc1:dtp>&nbsp;&nbsp;
					</td>
				</tr>
				<tr>
					<td colSpan="4"><asp:datagrid id="GrdTest" runat="server" Width="100%" PageSize="4" AllowPaging="True" AutoGenerateColumns="False">
							<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
							<HeaderStyle HorizontalAlign="Center" CssClass="Header3"></HeaderStyle>
							<Columns>
								<asp:BoundColumn HeaderText="Trainee Name" DataField="Emp_Name">
									<ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="Trainee Code" DataField="Emp_Code">
									<ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="Marks Obtained" DataField="Marks">
									<ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="Re Test" DataField="ReTestMarks">
									<ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle NextPageText="&amp;minus;&amp;gt;" Font-Underline="True" Font-Names="Verdana" PrevPageText="&amp;lt;&amp;minus;"
								ForeColor="#CB3908" BackColor="#E0E0E0" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td align="center" colSpan="2"></td>
					<td align="right" colSpan="2">&nbsp;&nbsp;</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
