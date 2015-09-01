<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Promotion.aspx.vb" Inherits="eHRMS.Net.Promotion"%>
<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Promotion</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<br>
			<TABLE cellSpacing="0" cellPadding="0" align="center" width="550" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Trainee 
						Promotion....
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="1" cellPadding="1" rules="none" align="center" width="550" border="1"
				frame="box">
				<tr>
					<td width="16%"></td>
					<td width="25%"></td>
					<td width="21%"></td>
					<td width="23%"></td>
				</tr>
				<tr>
					<td colSpan="4"><asp:label id="lblMsg" runat="server" Width="100%" ForeColor="Red" Font-Size="11px"></asp:label></td>
				</tr>
				<tr>
					<td class="Header3" colspan="4">Existing Records</td>
				</tr>
				<tr>
					<td>&nbsp;Code</td>
					<td><asp:textbox id="TxtCode" Width="80" CssClass="textbox" AutoPostBack="True" Runat="server" Enabled="False"
							ForeColor="#003366"></asp:textbox><asp:dropdownlist id="cmbEmp" runat="server" Width="150" Visible="False" AutoPostBack="True"></asp:dropdownlist><asp:imagebutton id="btnList" Width="18px" Runat="server" Height="19px" ImageUrl="Images\Find.gif"
							ImageAlign="AbsMiddle"></asp:imagebutton></td>
					<td colSpan="2"><asp:label id="LblName" Runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="header3">&nbsp;DOJ</td>
					<td><asp:Label Runat="server" ID="lbldoj" Width="100%"></asp:Label></td>
					<td class="header3">&nbsp;DOC</td>
					<td><asp:Label Runat="server" ID="lbldoc" Width="100%" ForeColor="Red"></asp:Label></td>
				</tr>
				<tr>
					<td colspan="4">&nbsp;</td>
				</tr>
				<tr>
					<td class="Header3" colspan="4">Updated Records</td>
				</tr>
				<tr>
					<td>&nbsp;New Code</td>
					<td><asp:textbox id="TxtNewCode" Width="80" CssClass="textbox" Runat="server" ForeColor="#003366"></asp:textbox></td>
					<td>Employment Type</td>
					<td><asp:DropDownList id="cmbEmpType" runat="server" Width="100%"></asp:DropDownList></td>
				</tr>
				<tr>
					<td>&nbsp;Date of Joining</td>
					<td><cc1:dtp id="DtpNewDoj" runat="server" Width="125px" ToolTip="New Date Of Joining"></cc1:dtp></td>
					<td>Date of Regularisation</td>
					<td><cc1:dtp id="DtpDDOR" runat="server" Width="125px" ToolTip="Date Of Regularisation"></cc1:dtp></td>
				</tr>
				<tr>
					<td colspan="4">&nbsp;</td>
				</tr>
				<TR height="15">
					<TD style="MARGIN-LEFT: 15px; MARGIN-RIGHT: 15px; BORDER-BOTTOM: #993300 thin groove; HEIGHT: 7px"
						colSpan="4"></TD>
				</TR>
				<tr>
					<td align="right" colSpan="4"><asp:button id="CmdSave" runat="server" Width="80px" Text="Save"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:button id="cmdClose" runat="server" Width="80px" Text="Close"></asp:button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
