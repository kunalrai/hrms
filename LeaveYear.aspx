<%@ Page Language="vb" AutoEventWireup="false" Codebehind="LeaveYear.aspx.vb" Inherits="eHRMS.Net.LeaveYear" %>
<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>LeaveYear</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="0" leftmargin="0">
		<!--#include file=MenuBars.aspx --><br>
		<br>
		<br>
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="300" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif"></TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="15">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Year 
						Selection ....</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" height="19" src="Images/TableRight.gif" width="27"></TD>
				</tr>
			</TABLE>
			<TABLE cellSpacing="2" cellPadding="0" rules="none" width="300" align="center" border="1"
				frame="box">
				<tr>
					<td width="30%"></td>
					<td width="70%"></td>
				</tr>
				<tr>
					<td colSpan="2"><asp:label id="LblErrMsg" runat="server" ForeColor="Red" Width="100%"></asp:label></td>
				</tr>
				<tr>
					<td colspan="2" align="center">
						<table cellpadding="0" cellspacing="1" border="1" width="100%">
							<tr>
								<td align="left" width="36%">&nbsp;<asp:label id="LblYrType" Font-Size="12px" Runat="server" Font-Bold="True" text="Year Type"></asp:label></td>
								<td align="center" width="64%">
									<asp:DropDownList id="cmbYearType" Width="100%" runat="server" AutoPostBack="True" ForeColor="#003366">
										<asp:ListItem Value="FIN_YR_" Selected="True">Financial year</asp:ListItem>
										<asp:ListItem Value="RIM_YR_">Rim Year</asp:ListItem>
										<asp:ListItem Value="LEV_YR_">Leave Year</asp:ListItem>
									</asp:DropDownList>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="5">
					<td colspan="2"></td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="1" cellPadding="0" width="90" border="1">
							<tr>
								<td><asp:listbox id="LstComp" AutoPostBack="True" Width="100px" Font-Size="10px" Rows="12" Font-Names="Verdana"
										Font-Overline="True" ForeColor="#003366" Height="140px" Runat="server"></asp:listbox></td>
							</tr>
						</table>
						<br>
					</td>
					<td align="center">
						<table cellSpacing="1" cellPadding="0" width="180" border="1">
							<tr>
								<td align="center"><asp:label id="Label1" Font-Size="12px" Runat="server" Font-Bold="True" text="Starting Date"></asp:label></td>
							</tr>
							<tr>
								<td align="center"><cc1:dtpcombo id="DtpStartDate" runat="server" Width="200px" ToolTip="startdate" ForeColor="#003366"></cc1:dtpcombo></td>
							</tr>
						</table>
						&nbsp;
						<table cellSpacing="1" cellPadding="0" width="180" border="1">
							<tr>
								<td align="center"><asp:label id="Label2" Font-Size="12px" Runat="server" Font-Bold="True" text="Ending Date"></asp:label></td>
							</tr>
							<tr>
								<td align="center"><cc1:dtpcombo id="DtpEndDate" runat="server" Width="200px" ToolTip="enddate " Enabled="False"
										ForeColor="#003366"></cc1:dtpcombo></td>
							</tr>
						</table>
						&nbsp;&nbsp;
						<table cellSpacing="1" cellPadding="0" width="180" border="1">
							<tr>
								<td align="center"><asp:button id="cmdok" runat="server" Width="75px" Text="OK"></asp:button></td>
								<td align="center"><asp:button id="cmdcancel" runat="server" Width="75px" Text="Close"></asp:button></td>
							</tr>
						</table>
						<br>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
