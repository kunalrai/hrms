<%@ Page Language="vb" AutoEventWireup="false" Codebehind="LeaveStatus.aspx.vb" Inherits="eHRMS.Net.LeaveStatus"%>
<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Leave Status</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx --><br>
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Leave 
						Application Status ....
					</TD>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" rules="none" width="800" align="center" border="1"
				frame="border">
				<tr borderColor="white">
					<td colSpan="6"><asp:label id="lblMsg" runat="server" Visible="False" Font-Size="11px" ForeColor="Red" Width="100%"></asp:label></td>
				</tr>
				<tr>
					<td width="100%" colSpan="6"><asp:datagrid id="grdLeavStatus" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
							AllowSorting="True">
							<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
							<HeaderStyle Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Top"
								BackColor="Gray"></HeaderStyle>
							<Columns>
								<asp:BoundColumn ItemStyle-Width="70" DataField="levyear" HeaderText="Leave Year" HeaderStyle-HorizontalAlign="Center"
									ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
								<asp:BoundColumn ItemStyle-Width="200" DataField="LvDesc" HeaderText="Leave Type" HeaderStyle-HorizontalAlign="Center"
									ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
								<asp:BoundColumn ItemStyle-Width="50" DataField="LvDays" HeaderText="Days" HeaderStyle-HorizontalAlign="Center"
									ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
								<asp:BoundColumn ItemStyle-Width="100" DataField="atDate" HeaderText="Leave Date" HeaderStyle-HorizontalAlign="Center"
									ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
								<asp:BoundColumn ItemStyle-Width="100" DataField="appDate" DataFormatString="{0:dd/MMM/yyyy}" HeaderText="Application Date"
									HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
								<asp:BoundColumn ItemStyle-Width="100" DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Center"
									ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
								<asp:TemplateColumn ItemStyle-Width="90" HeaderText="Delete" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
									<ItemTemplate>
										<input type="checkbox" runat="server" ID="Checkbox1">
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td class="Header3" colSpan="6"></td>
				</tr>
				<tr>
					<td class="Header3" width="20%"><asp:dropdownlist id="cmbType" runat="server"></asp:dropdownlist></td>
					<td class="Header3" width="10%">From</td>
					<td class="Header3" width="20%"><cc1:dtp id="dtpFromDate" runat="server" Width="125px"></cc1:dtp></td>
					<td class="Header3" width="10%">To</td>
					<td class="Header3" width="20%"><cc1:dtp id="dtpToDate" runat="server" Width="125px" ReadOnlyText="False"></cc1:dtp></td>
					<td class="Header3" width="20%"><asp:button id="cmdShow" runat="server" Width="60px" Text="Show"></asp:button></td>
				</tr>
				<tr>
					<td colSpan="6"></td>
				</tr>
				<tr>
					<td class="Header3" colSpan="3"></td>
					<td class="Header3" align="right" colSpan="3"><asp:button id="cmdPrint" runat="server" Width="60px" Text="Print"></asp:button>&nbsp;
						<asp:button id="cmdDelete" runat="server" Width="60px" Text="Delete"></asp:button>&nbsp;
						<asp:button id="cmdClose" runat="server" Width="60px" Text="Close"></asp:button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
