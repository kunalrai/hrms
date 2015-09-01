<%@ Page Language="vb" AutoEventWireup="false" Codebehind="LeavSetup.aspx.vb" Inherits="eHRMS.Net.LeavSetup" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>LeavSetup</title>
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
			<TABLE cellSpacing="0" cellPadding="0" width="600" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Leave 
						Setup....
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="1" cellPadding="1" rules="none" width="600" border="1" frame="border"
				align="center">
				<tr>
					<td width="20%"></td>
					<td width="45%"></td>
					<td width="20%"></td>
				</tr>
				<tr>
					<td colSpan="3"><asp:label id="LblErrMsg" runat="server" Width="100%" ForeColor="Red"></asp:label></td>
				</tr>
				<tr>
					<td>&nbsp;Leave Type</td>
					<td><asp:textbox id="Txtleavtype" Width="120px" AutoPostBack="True" Runat="server" CssClass="textbox"
							ForeColor="#003366" MaxLength="1"></asp:textbox><asp:dropdownlist id="cmbleavtype" runat="server" Width="176px" AutoPostBack="True" Visible="False"></asp:dropdownlist><asp:imagebutton id="btnList" Width="18px" Runat="server" ImageUrl="Images\Find.gif" ImageAlign="AbsMiddle"></asp:imagebutton><asp:imagebutton id="btnNew" Width="18px" Runat="server" ImageUrl="Images\NewFile.ico" ImageAlign="AbsMiddle"
							Height="19px"></asp:imagebutton></td>
					<td align="center"><asp:checkbox id="Chklpaid" runat="server" Text="Paid Leave"></asp:checkbox></td>
				</tr>
				<tr>
					<td>&nbsp;Description</td>
					<td colSpan="2"><asp:textbox id="txtDesc" Width="100%" Runat="server" CssClass="textbox" ForeColor="#003366"
							MaxLength="254"></asp:textbox></td>
				</tr>
				<tr>
					<td>&nbsp;Credit Frequency</td>
					<td colSpan="2"><asp:dropdownlist id="cmbcrfreq" runat="server" Width="100%">
							<asp:ListItem Selected="True" Value="1">None</asp:ListItem>
							<asp:ListItem Selected="False" Value="2">Monthly</asp:ListItem>
							<asp:ListItem Selected="False" Value="3">Quaterly</asp:ListItem>
							<asp:ListItem Selected="False" Value="4">Half-Yearly</asp:ListItem>
							<asp:ListItem Selected="False" Value="5">Yearly</asp:ListItem>
						</asp:dropdownlist></td>
				</tr>
				<tr>
					<td>&nbsp;Credit Months</td>
					<td colSpan="2"><asp:dropdownlist id="cmbcrmonths" runat="server" Width="100%">
							<asp:ListItem Value="1" Selected="True">January</asp:ListItem>
							<asp:ListItem Value="2">February</asp:ListItem>
							<asp:ListItem Value="3">March</asp:ListItem>
							<asp:ListItem Value="4">April</asp:ListItem>
							<asp:ListItem Value="5">May</asp:ListItem>
							<asp:ListItem Value="6">June</asp:ListItem>
							<asp:ListItem Value="7">July</asp:ListItem>
							<asp:ListItem Value="8">August</asp:ListItem>
							<asp:ListItem Value="9">September</asp:ListItem>
							<asp:ListItem Value="10">October</asp:ListItem>
							<asp:ListItem Value="11">November</asp:ListItem>
							<asp:ListItem Value="12">December</asp:ListItem>
						</asp:dropdownlist></td>
				</tr>
				<tr>
					<td>&nbsp;Credit days</td>
					<td colSpan="2"><asp:textbox id="txtcrdays" Width="100%" Runat="server" TextMode="MultiLine" Rows="3" ForeColor="#003366"
							MaxLength="2000"></asp:textbox></td>
				</tr>
				<tr>
					<td>&nbsp;Credit Limit</td>
					<td colSpan="2"><asp:textbox id="Txtcrlimit" Width="100%" Runat="server" CssClass="textbox" ForeColor="#003366"
							MaxLength="2000"></asp:textbox></td>
				</tr>
				<tr>
					<td>&nbsp;Encashment Limit</td>
					<td><asp:textbox id="Txtencashlimit" Width="100%" Runat="server" CssClass="textbox" ForeColor="#003366"></asp:textbox></td>
					<td align="center"><asp:checkbox id="Chklapse" runat="server" Text="Lapse"></asp:checkbox></td>
				</tr>
				<tr>
					<td>&nbsp;Include Holidays</td>
					<td colspan="2"><asp:checkbox id="ChkIncHolidays" runat="server" ForeColor="#003366"></asp:checkbox></td>
				</tr>
				<TR height="15">
					<TD style="MARGIN-LEFT: 15px; MARGIN-RIGHT: 15px; BORDER-BOTTOM: #993300 thin groove; HEIGHT: 7px"
						colSpan="3"></TD>
				</TR>
				<tr>
					<td align="right" colSpan="3"><asp:button id="cmdSave" runat="server" Width="75px" Text="Save"></asp:button>&nbsp;<asp:button id="cmdClose" runat="server" Width="75px" Text="Close"></asp:button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
