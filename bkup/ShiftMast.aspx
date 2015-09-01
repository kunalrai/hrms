<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ShiftMast.aspx.vb" Inherits="eHRMS.Net.ShiftMast" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ShiftMast</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
		<SCRIPT language="javascript" src="Common.js"></SCRIPT>
		<script language="vbscript">
			sub Validate(S)
					If InStr(1, document.getElementById(S).value, ":") <> 3  Then
						alert ("Please enter time in correct format eg. 22:30 Or 03:25")
						document.getElementById(S).value="00:00"
						Exit Sub
					End If
			end sub
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx --><br>
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="400" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Shift 
						Master...
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" rules="none" width="400" align="left" border="1">
				<tr>
					<td width="15%"></td>
					<td width="35%"></td>
					<td width="15%"></td>
					<td width="35%"></td>
				</tr>
				<tr>
					<td colSpan="4"><asp:label id="lblMsg" runat="server" Font-Size="11px" Width="100%" Visible="False" ForeColor="Red"></asp:label></td>
				</tr>
				<tr>
					<td>Code</td>
					<td colSpan="4"><asp:textbox id="TxtCode" Runat="server" AutoPostBack="True" CssClass="textbox" ForeColor="#003366"></asp:textbox><asp:dropdownlist id="cmbShift" runat="server" Width="175" Visible="False" AutoPostBack="True"></asp:dropdownlist><asp:imagebutton id="btnList" Width="18px" Runat="server" ImageAlign="AbsMiddle" ImageUrl="Images\Find.gif"></asp:imagebutton><asp:imagebutton id="btnNew" Width="18px" Runat="server" ImageAlign="AbsMiddle" ImageUrl="Images\NewFile.ico"
							Height="19px"></asp:imagebutton></td>
				</tr>
				<tr>
					<td>Description</td>
					<td colSpan="4"><asp:textbox id="TxtDesc" Width="100%" Runat="server" ForeColor="#003366"></asp:textbox></td>
				</tr>
				<tr>
					<td colSpan="2">
						<table cellSpacing="0" cellPadding="0" rules="none" width="100%" border="1" frame="border">
							<tr class="Header3">
								<td align="left" colSpan="4"><b>Shift Period:</b></td>
							</tr>
							<tr>
								<td>From</td>
								<td align="center"><asp:textbox id="TxtSpFrom" onblur="Validate(this.id)" Width="50PX" Runat="server" ForeColor="#003366"></asp:textbox></td>
								<td>To</td>
								<td align="right"><asp:textbox id="TxtSpTo" onblur="Validate(this.id)" Width="50px" Runat="server" ForeColor="#003366"></asp:textbox></td>
							</tr>
						</table>
					</td>
					<td colSpan="2">
						<table cellSpacing="0" cellPadding="0" rules="none" width="100%" border="1" frame="border">
							<tr class="Header3">
								<td align="left" colSpan="4"><b>Lunch Period:</b></td>
							</tr>
							<tr>
								<td>From</td>
								<td align="center"><asp:textbox id="TxtLpFrom" onblur="Validate(this.id)" Width="50PX" Runat="server" ForeColor="#003366"></asp:textbox></td>
								<td>To</td>
								<td align="right"><asp:textbox id="TxtLpTo" onblur="Validate(this.id)" Width="50px" Runat="server" ForeColor="#003366"></asp:textbox></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="4">&nbsp;</td>
				</tr>
				<tr>
					<td align="right" colSpan="4"><asp:button id="cmdSave" Runat="server" Text="Save" width="75px"></asp:button>&nbsp;&nbsp;&nbsp;
						<asp:button id="cmdClose" Runat="server" Text="Close" width="75px"></asp:button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
