<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SectionMast.aspx.vb" Inherits="eHRMS.Net.SectionMast" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>SectionMast</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="450" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="51" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Section 
						Master....
					</TD>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" rules="none" width="450" align="center" border="1"
				frame="border">
				<tr>
					<td width="25%"></td>
					<td width="75%"></td>
				</tr>
				<tr>
					<td colSpan="2"><asp:label id="lblMsg" runat="server" Font-Size="11px" Width="100%" Visible="False" ForeColor="Red"></asp:label></td>
				</tr>
				<tr>
					<td>Section Code</td>
					<td colSpan="2"><asp:textbox id="TxtCode" Width="100px" AutoPostBack="True" CssClass="textbox" Runat="server"
							ForeColor="#003366"></asp:textbox><asp:dropdownlist id="cmbSection" runat="server" Width="150px" Visible="False" AutoPostBack="True"></asp:dropdownlist><asp:imagebutton id="btnList" Width="18px" Runat="server" ImageAlign="AbsMiddle" ImageUrl="Images\Find.gif"></asp:imagebutton><asp:imagebutton id="btnNew" Width="18px" Runat="server" ImageAlign="AbsMiddle" ImageUrl="Images\NewFile.ico"
							Height="19px" ToolTip="Add New Record"></asp:imagebutton></td>
				</tr>
				<tr>
					<td><asp:label id="lblName" Font-Size="11px" Width="100px" Runat="server">Section Name</asp:label></td>
					<td><asp:textbox id="TxtName" Font-Size="11px" Width="160px" CssClass="textbox" Runat="server" ForeColor="#003366"></asp:textbox></td>
				</tr>
				<tr>
					<td><asp:label id="lblUId" Font-Size="11px" Width="100px" Runat="server">Admin User Id</asp:label></td>
					<td><asp:dropdownlist id="CmbUId" runat="server" Width="200px"></asp:dropdownlist></td>
				</tr>
				<TR>
					<td><asp:label id="lblDEmailId" Font-Size="11px" Width="100px" Runat="server">Default Email Id</asp:label></td>
					<td><asp:textbox id="TxtDEmailId" Font-Size="11px" Width="100%" CssClass="textbox" Runat="server"
							ForeColor="#003366"></asp:textbox></td>
				</TR>
				<TR>
					<td><asp:label id="lblTo" Font-Size="11px" Width="100px" Runat="server">To</asp:label></td>
					<td><asp:textbox id="TxtTo" Font-Size="11px" Width="100%" CssClass="textbox" Runat="server" ForeColor="#003366"></asp:textbox></td>
				</TR>
				<TR>
					<td><asp:label id="lblCC" Font-Size="11px" Width="100px" Runat="server">CC</asp:label></td>
					<td><asp:textbox id="TxtCC" Font-Size="11px" Width="336px" CssClass="textbox" Runat="server" ForeColor="#003366"></asp:textbox></td>
				</TR>
				<tr>
					<td colSpan="2">&nbsp;</td>
				</tr>
				<tr>
					<td align="right" colSpan="2"><asp:button id="cmdSave" Runat="server" width="75px" Text="Save"></asp:button>&nbsp;&nbsp;&nbsp;
						<asp:button id="cmdClose" Runat="server" width="75px" Text="Close"></asp:button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
