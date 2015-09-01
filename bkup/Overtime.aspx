<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Overtime.aspx.vb" Inherits="eHRMS.Net.Overtime"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Overtime</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="750" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Overtime...
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="1" cellPadding="1" rules="none" align="center" width="750" border="1"
				frame="border">
				<tr>
					<td colSpan="2"><asp:label id="LblMsg" runat="server" Width="100%" ForeColor="Red" Font-Size="11px"></asp:label></td>
				</tr>
				<tr>
					<td colspan="2">
						<div style="BORDER-RIGHT: gainsboro thin solid; BORDER-TOP: gainsboro thin solid; OVERFLOW: auto; BORDER-LEFT: gainsboro thin solid; WIDTH: 100%; BORDER-BOTTOM: gainsboro thin solid; HEIGHT: 250px">
							<asp:datagrid id="GrdEmployee" Width="100%" runat="server" AutoGenerateColumns="False">
								<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
								<HeaderStyle Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Top"
									BackColor="Gray"></HeaderStyle>
								<Columns>
									<asp:BoundColumn DataField="EMP_CODE" HeaderText="Code" ItemStyle-Width="10%"></asp:BoundColumn>
									<asp:BoundColumn DataField="OT_DATE" HeaderText="Overtime Date" ItemStyle-Width="15%"></asp:BoundColumn>
									<asp:BoundColumn DataField="OT_From" HeaderText="Overtime From" ItemStyle-Width="10%"></asp:BoundColumn>
									<asp:BoundColumn DataField="OT_To" HeaderText="Overtime To" ItemStyle-Width="10%"></asp:BoundColumn>
									<asp:BoundColumn DataField="OT_HOURS" HeaderText="Overtime Hours" ItemStyle-Width="10%"></asp:BoundColumn>
									<asp:BoundColumn DataField="OT_Amount" HeaderText="Overtime Amount" ItemStyle-Width="15%"></asp:BoundColumn>
									<asp:BoundColumn DataField="Taxable" HeaderText="Taxable" ItemStyle-Width="15%"></asp:BoundColumn>
									<asp:BoundColumn DataField="TDS" HeaderText="TDS" ItemStyle-Width="15%"></asp:BoundColumn>
								</Columns>
							</asp:datagrid>
						</div>
					</td>
				</tr>
				</tr>
				<TR height="15">
					<TD style="MARGIN-LEFT: 15px; MARGIN-RIGHT: 15px; BORDER-BOTTOM: #993300 thin groove; HEIGHT: 7px"
						colSpan="2"></TD>
				</TR>
				<tr>
					<td align="left">&nbsp;Month&nbsp;&nbsp;<asp:dropdownlist id="CmbMonth" Width="120px" Runat="server" AutoPostBack="True"></asp:dropdownlist></td>
					<td align="right"><asp:button id="cmdSave" runat="server" Width="75px" Text="Save"></asp:button>&nbsp;&nbsp;<asp:button id="CmdDelete" runat="server" Width="75px" Text="Delete"></asp:button>&nbsp;&nbsp;<asp:button id="CmdCancel" runat="server" Width="75px" Text="Cancel"></asp:button>&nbsp;&nbsp;
						<asp:button id="cmdClose" accessKey="C" runat="server" Width="75px" Text="Close"></asp:button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
