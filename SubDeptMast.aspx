<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SubDeptMast.aspx.vb" Inherits="eHRMS.Net.SubDeptMast"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Sub-Department</title>
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
			<!--#include file=MenuBars.aspx -->
			<br>
			<br>
			<table borderColor="gray" cellSpacing="0" cellPadding="0" width="500" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Sub-Department 
						Master ....
					</TD>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</table>
			<table borderColor="gray" cellSpacing="1" cellPadding="0" rules="none" width="500" align="center"
				border="1" frame="border">
				<tr>
					<td width="30%"></td>
					<td width="70%"></td>
				</tr>
				<tr>
					<td width="100%" colSpan="2"><asp:label id="LblErrMsg" runat="server" ForeColor="Red"></asp:label></td>
				</tr>
				<tr>
					<td>&nbsp;Code</td>
					<td><asp:textbox id="TxtCode" runat="server" AutoPostBack="True" ForeColor="#003366" CssClass="TextBox" Width="75px"></asp:textbox><asp:dropdownlist id="cmbCode" runat="server" AutoPostBack="True" Width="280px" Visible="False"></asp:dropdownlist><asp:imagebutton id="btnList" Width="18px" Runat="server" ImageAlign="AbsMiddle" ImageUrl="Images\Find.gif"
							Height="19px"></asp:imagebutton></td>
				</tr>
				<tr>
					<td>&nbsp;Sub Department Name</td>
					<td><asp:textbox id="TxtSubDeptDesc" tabIndex="1" ForeColor="#003366"  runat="server" CssClass="TextBox" Width="280px"></asp:textbox></td>
				</tr>
				<tr>
					<td>&nbsp;Under Department</td>
					<td><asp:dropdownlist id="cmbUDept" runat="server" Width="280px"></asp:dropdownlist></td>
				</tr>
				<tr><td colspan="2" height="5"></td></tr> 
				<TR height="15">
					<TD style="MARGIN-LEFT: 15px; MARGIN-RIGHT: 15px; BORDER-BOTTOM: #993300 thin groove; HEIGHT: 1px"
						colSpan="2"></TD>
				</TR>
				
				<tr>
					<td colSpan="2" height="35" >
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td align="right" width="70%"><asp:button id="cmdNew" runat="server" Width="75px" Text="New"></asp:button>&nbsp;</td>
								<td align="right" width="15%"><asp:button id="cmdSave" runat="server" Width="75px" Text="Save"></asp:button>&nbsp;</td>
								<td align="right" width="15%"><asp:button id="cmdClose" runat="server" Width="75px" Text="Close"></asp:button>&nbsp;</td>
							</tr>
						</TABLE>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
