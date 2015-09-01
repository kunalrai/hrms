<%@ Page Language="vb" AutoEventWireup="false" Codebehind="RunSQLCommand.aspx.vb" Inherits="eHRMS.Net.RunSQLCommand"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>RunSQLCommand</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="Common.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="700" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Run 
						SQL Command....
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" rules="none" width="700" border="1" frame="border">
				<tr>
					<td width="15%"></td>
					<td width="45%"></td>
					<td width="25%"></td>
					<td width="15%"></td>
				</tr>
				<tr>
					<td colSpan="4"><asp:label id="LblMsg" runat="server" Width="100%" ForeColor="Red" Font-Size="11px"></asp:label></td>
				</tr>
				<tr>
					<td>Command</td>
					<td colspan="3"><asp:dropdownlist id="cmbMonth" runat="server" Width="80%"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td>For</td>
					<td colspan="3"><asp:dropdownlist id="Dropdownlist1" runat="server" Width="150px"></asp:dropdownlist>&nbsp;<asp:dropdownlist id="Dropdownlist2" runat="server" Width="50%"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td colspan="4">&nbsp;</td>
				</tr>
				<tr>
					<td align="left" colSpan="3">&nbsp;&nbsp;<asp:button id="cmdView" runat="server" Width="75px" Text="View"></asp:button>&nbsp;&nbsp;&nbsp;<asp:button id="cmdExport" runat="server" Width="96px" Text="Excel Export"></asp:button>
					</td>
					<td><asp:button id="cmdClose" runat="server" Width="75px" Text="Close"></asp:button></td>
				</tr>
				<tr>
					<td colspan="4">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="4"><asp:datagrid id="GrdSqlCom" runat="server" Width="100%">
							<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
							<HeaderStyle Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Top"
								BackColor="Gray"></HeaderStyle>
						</asp:datagrid>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
