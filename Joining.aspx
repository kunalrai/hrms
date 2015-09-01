<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Joining.aspx.vb" Inherits="eHRMS.Net.Joining"%>
<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Joining</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
		<script language="vbscript">
			Sub Val(argid)
				IF document.getElementById(argid).Checked = False THEN
					document.getElementById(replace(argid,"Chk","Dtp")).disabled = true
					document.getElementById(replace(argid,"Chk","Dtp") & "cmbDD").disabled = true
					document.getElementById(replace(argid,"Chk","Dtp") & "cmbMM").disabled = true
					document.getElementById(replace(argid,"Chk","Dtp") & "cmbYY").disabled = true
				ELSE
					document.getElementById(replace(argid,"Chk","Dtp")).disabled = false
					document.getElementById(replace(argid,"Chk","Dtp") & "cmbDD").disabled = false
					document.getElementById(replace(argid,"Chk","Dtp") & "cmbMM").disabled = false
					document.getElementById(replace(argid,"Chk","Dtp") & "cmbYY").disabled = false
				End If
			END SUB
		</script>
	</HEAD>
	<body topMargin="5" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx --><br>
			<br>
			<TABLE cellSpacing="0" cellPadding="0" align="center" width="600" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Joining....
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" rules="none" align="center" width="600" border="1"
				frame="border">
				<tr>
					<td width="20%"></td>
					<td width="30"></td>
					<td width="20%"></td>
					<td width="30%"></td>
				</tr>
				<tr>
					<td colSpan="5"><asp:label id="LblErrMsg" runat="server" ForeColor="Red" Width="100%"></asp:label></td>
				</tr>
				<tr>
					<td>&nbsp;Resumes</td>
					<td colSpan="3"><asp:dropdownlist id="cmbResume" Width="100%" AutoPostBack="True" Runat="server"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td align="left">&nbsp;Designation</td>
					<td><asp:dropdownlist id="cmbDesg" Width="178px" Runat="server"></asp:dropdownlist></td>
					<td align="left">&nbsp;Grade/Level</td>
					<td><asp:dropdownlist id="cmbGrd" Width="100%" Runat="server"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td>&nbsp;Department</td>
					<td><asp:dropdownlist id="cmbDept" Width="100%" Runat="server"></asp:dropdownlist></td>
					<td align="left">&nbsp;Reporting Mngr</td>
					<td><asp:dropdownlist id="cmbReptMngr" Width="100%" Runat="server"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td>&nbsp;Section</td>
					<td><asp:dropdownlist id="cmbSection" Width="100%" Runat="server"></asp:dropdownlist></td>
					<td>&nbsp;Work Location</td>
					<td><asp:dropdownlist id="cmbLoc" Width="100%" Runat="server"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td>&nbsp;Division</td>
					<td><asp:dropdownlist id="cmbDivision" Width="100%" Runat="server"></asp:dropdownlist></td>
					<td align="left">&nbsp;Cost Center</td>
					<td><asp:dropdownlist id="cmbCostCenter" Width="100%" Runat="server"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td>&nbsp;Region</td>
					<td><asp:dropdownlist id="CmbRegion" Width="100%" Runat="server"></asp:dropdownlist></td>
					<td align="left">&nbsp;Employee Type</td>
					<td><asp:dropdownlist id="cmbEmpType" Width="100%" Runat="server"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td>&nbsp;Process</td>
					<td><asp:dropdownlist id="cmbProcess" Width="100%" Runat="server"></asp:dropdownlist></td>
					<td><asp:label id="lblADOJ" runat="server" Width="100%">&nbsp;Actual DOJ</asp:label></td>
					<td><input id="ChkADOJ" onclick="Val(this.id)" type="checkbox" name="ChkADOJ" runat="server"><cc1:dtpcombo id="dtpADOJ" runat="server" Width="152px" Enabled="False" DateValue="2006-02-10"
							ToolTip="Actual DOJ"></cc1:dtpcombo></td>
				</tr>
				<tr>
					<td>&nbsp;Emp.Code</td>
					<td><asp:textbox id="TxtEmpCode" Width="98%" Runat="server" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
					<td><asp:label id="lblDOC" runat="server" Width="100%" ToolTip="Date of Confirmation">Date of Confirmation</asp:label></td>
					<td><input id="ChkDOC" onclick="Val(this.id)" type="checkbox" name="ChkDOC" runat="server"><cc1:dtpcombo id="dtpDOC" runat="server" Width="150px" Enabled="False" DateValue="2005-08-30"
							ToolTip="Date of Confirmation"></cc1:dtpcombo></td>
				</tr>
				<tr>
					<td>&nbsp;PF. No.</td>
					<td><asp:textbox id="TxtPFNo" Width="98%" Runat="server" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
					<td><asp:label id="Label1" runat="server" Width="100%">&nbsp;Contract End Date</asp:label></td>
					<td><input id="ChkDOCE" onclick="Val(this.id)" type="checkbox" name="ChkDOCE" runat="server"><cc1:dtpcombo id="dtpDOCE" runat="server" Width="150px" Enabled="False" DateValue="2005-08-30"
							ToolTip="Contract End Date"></cc1:dtpcombo></td>
				</tr>
				<tr>
					<td><asp:label id="lblDOJ" runat="server" Width="100%">&nbsp;Planned DOJ</asp:label></td>
					<td><cc1:dtp id="dtpDOJ" runat="server" Width="168px" ToolTip="Planned DOJ"></cc1:dtp></td>
					<td></td>
					<td><asp:CheckBox ID="ChkDelete" Runat="server" Checked="False" Text="Replace if Exist" ForeColor="#003366"></asp:CheckBox></td>
				</tr>
				<tr>
					<td colSpan="4">&nbsp;</td>
				</tr>
				<tr>
					<td colSpan="3"></td>
					<td align="right"><asp:button id="btnSave" Width="75px" Runat="server" Text="Save"></asp:button>&nbsp;
						<asp:button id="btnClose" Width="75px" Runat="server" Text="Close"></asp:button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
