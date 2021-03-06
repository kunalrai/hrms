<%@ Page Language="vb" AutoEventWireup="false" Codebehind="GenMast.aspx.vb" Inherits="eHRMS.Net.GenMast" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Masters</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx --><br>
			<br>
			<br>
			<table borderColor="gray" cellSpacing="0" cellPadding="0" width="60%" align="center" border="0">
				<tr>
					<td align="center" colSpan="2">
						<TABLE cellSpacing="0" cellPadding="0" border="0">
							<tr id="Head" runat="server">
								<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
								</TD>
								<TD class="header3" noWrap align="center" width="95%" background="Images/TableMid.gif"
									height="19"><b>General Masters...</b></TD>
								<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
								</TD>
							</tr>
						</TABLE>
					</td>
				</tr>
			</table>
			<table borderColor="gray" cellSpacing="0" cellPadding="0" width="60%" align="center" border="1">
				<tr>
					<td>
						<TABLE id="T1" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
						</TABLE>
					</td>
				</tr>
				<tr>
					<td>
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td width="100%" colSpan="3"><asp:label id="LblErrMsg" runat="server" ForeColor="Red"></asp:label></td>
							</tr>
							<tr>
								<td align="right" width="70%"><asp:button id="cmdNew" runat="server" Text="New" Width="75px"></asp:button></td>
								<td align="right" width="15%"><asp:button id="cmdSave" runat="server" Text="Save" Width="75px"></asp:button></td>
								<td align="right" width="15%"><asp:button id="cmdClose" runat="server" Text="Close" Width="75px"></asp:button></td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td>
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td align="right" width="100%"><asp:linkbutton id="cmdShow" runat="server" Text="Display Records"></asp:linkbutton></td>
							</tr>
							<tr id="TGrid" runat="server">
								<TD width="100%">
									<div style="OVERFLOW: auto; HEIGHT: 250px">
										<asp:datagrid id="GrdDisplay" runat="server" Width="100%" AllowSorting="True">
											<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
											<HeaderStyle Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Top"
												BackColor="Gray"></HeaderStyle>
											<PagerStyle NextPageText="&amp;minus;&amp;gt;" Font-Underline="True" Font-Names="Verdana" PrevPageText="&amp;lt;&amp;minus;"
												ForeColor="#CB3908" BackColor="#E0E0E0" Mode="NumericPages"></PagerStyle>
										</asp:datagrid>
									</div>
								</TD>
							</tr>
						</TABLE>
					</td>
				</tr>
			</table>
			</OPTION></SELECT><asp:textbox id="txtAction" style="Z-INDEX: 101; LEFT: 24px; POSITION: absolute; TOP: 16px" runat="server"
				Width="0px" ReadOnly="True"></asp:textbox></form>
	</body>
</HTML>
