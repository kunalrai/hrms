<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FinalSettlement.aspx.vb" Inherits="eHRMS.Net.FinalSettlement"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Pay Calculation</title>
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
			<TABLE cellSpacing="0" cellPadding="0" width="600" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Final 
						Settlement....
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" rules="none" width="600" align="center" border="1"
				frame="border">
				<tr>
					<td colSpan="4"><asp:label id="lblMsg" runat="server" ForeColor="Red" Font-Size="11px"></asp:label><br>
					</td>
				<tr>
					<td style="HEIGHT: 13px" width="50%" colSpan="2">&nbsp;Month</td>
					<td style="HEIGHT: 13px" width="50%" colSpan="2"><asp:dropdownlist id="cmbMonth" runat="server" Width="100%" AutoPostBack="True"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td colSpan="4" align="center">
						<div style="BORDER-RIGHT: 1px groove; BORDER-TOP: 1px groove; OVERFLOW: auto; BORDER-LEFT: 1px groove; BORDER-BOTTOM: 1px groove; HEIGHT: 100%"><asp:datagrid id="grdFNF" runat="server" AutoGenerateColumns="False">
								<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
								<HeaderStyle Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Top"
									BackColor="Gray"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="Select">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="5px"></ItemStyle>
										<ItemTemplate>
											<asp:CheckBox ID="ChkSelect" Runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Calc") %>'>
											</asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn HeaderStyle-Width="10%" DataField="Emp_Code" HeaderText="Code"></asp:BoundColumn>
									<asp:BoundColumn HeaderStyle-Width="50%" DataField="Emp_Name" HeaderText="Name"></asp:BoundColumn>
									<asp:BoundColumn HeaderStyle-Width="10%" DataField="DOJ" HeaderText="DOJ" DataFormatString="{0:dd/MMM/yyyy}"></asp:BoundColumn>
									<asp:BoundColumn HeaderStyle-Width="10%" DataField="DOL" HeaderText="DOL" DataFormatString="{0:dd/MMM/yyyy}"></asp:BoundColumn>
									<asp:BoundColumn HeaderStyle-Width="10%" DataField="LNotice" HeaderText="Resg. Date" DataFormatString="{0:dd/MMM/yyyy}"></asp:BoundColumn>
									<asp:BoundColumn HeaderStyle-Width="10%" DataField="Set_Date" HeaderText="Settlement Date" DataFormatString="{0:dd/MMM/yyyy}"></asp:BoundColumn>
								</Columns>
							</asp:datagrid></div>
					</td>
				<tr>
				<tr>
					<td align="center" width="25%" colSpan="1"><asp:button id="cmdSelAll" runat="server" Width="80px" Text="Select All"></asp:button></td>
					<td align="center" width="25%" colSpan="1"><asp:button id="cmdDeSelAll" runat="server" Width="80px" Text="DeSelect All"></asp:button></td>
					<td align="center" width="25%" colSpan="1"><asp:button id="cmdCalc" runat="server" Width="80px" Text="Calculate"></asp:button></td>
					<td align="center" width="25%" colSpan="1"><asp:button id="cmdClose" runat="server" Width="80px" Text="Close"></asp:button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
