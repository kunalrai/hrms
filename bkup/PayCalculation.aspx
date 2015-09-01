<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PayCalculation.aspx.vb" Inherits="eHRMS.Net.PayCalculation" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PayCalculation</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="450" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Pay 
						Calculation....
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" rules="none" width="450" align="center" border="1"
				frame="border">
				<tr>
					<td width="15%"></td>
					<td width="35%"></td>
					<td width="15%"></td>
					<td width="35%"></td>
				</tr>
				<tr>
					<td colSpan="4"><asp:label id="lblMsg" runat="server" Width="100%" Visible="False" ForeColor="Red" Font-Size="11px"></asp:label></td>
				</tr>
				<tr>
					<td colspan="4">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="4">
						<table cellpadding="0" cellspacing="0" width="90%" align="center" border="1" frame="border"
							rules="none">
							<tr>
								<td>Month</td>
								<td><asp:dropdownlist id="CmbMonth" Width="100px" Runat="server"></asp:dropdownlist></td>
								<td>Year</td>
								<td><asp:dropdownlist id="CmbYear" Width="100px" Runat="server">
										<asp:ListItem Value="2004">2004</asp:ListItem>
										<asp:ListItem Value="2005">2005</asp:ListItem>
										<asp:ListItem Value="2006">2006</asp:ListItem>
										<asp:ListItem Value="2007">2007</asp:ListItem>
										<asp:ListItem Value="2008">2008</asp:ListItem>
									</asp:dropdownlist></td>
							</tr>
							<tr>
								<td colspan="4">&nbsp;</td>
							</tr>
							<tr>
								<td colSpan="2"><asp:dropdownlist id="CmbFilter" Width="160px" Runat="server" AutoPostBack="True"></asp:dropdownlist></td>
								<td colSpan="2"><asp:textbox id="TxtCode" Width="130" Runat="server" AutoPostBack="True" CssClass="textbox"></asp:textbox><asp:dropdownlist id="cmbList" runat="server" Width="150" AutoPostBack="True" Visible="False"></asp:dropdownlist><asp:imagebutton id="btnList" Width="18px" Runat="server" ImageAlign="AbsMiddle" ImageUrl="Images\Find.gif"
										Height="19px"></asp:imagebutton></td>
							</tr>
							<tr>
								<td colspan="4">&nbsp;</td>
							</tr>
							<tr>
								<td colspan="4">Creteria</td>
							</tr>
							<tr>
								<td align="center" colSpan="4"><asp:textbox id="TxtCretaria" Width="100%" Runat="server" TextMode="MultiLine" Rows="3"></asp:textbox></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="4">&nbsp;</td>
				</tr>
				<tr>
					<td align="center" colSpan="4"><asp:button id="cmdCalculate" runat="server" Text="Calculate" Width="75px"></asp:button>&nbsp;
						<asp:button id="CmdView" runat="server" Text="View" Width="75px"></asp:button>&nbsp;
						<asp:button id="cmdclose" runat="server" Text="Close" Width="75px"></asp:button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
