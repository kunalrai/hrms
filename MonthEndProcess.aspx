<%@ Page Language="vb" AutoEventWireup="false" Codebehind="MonthEndProcess.aspx.vb" Inherits="eHRMS.Net.MonthEndProcess" %>
<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>MonthEndProcess</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript">
		function ConfirmUpdate()
		{
			if(confirm("Are You Sure ! To Run The Month End Process Transaction?"+"...[Divergent]")==true)
				return true;
			else
				return false;
		}
		</script>
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<!--#include file=MenuBars.aspx --><br>
		<br>
		<br>
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="300" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="15">&nbsp;
					</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Month 
						End Process ....
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" style="WIDTH: 27px; HEIGHT: 19px" height="19" src="Images/TableRight.gif"
							width="27">
					</TD>
				</tr>
			</TABLE>
			<TABLE cellSpacing="3" cellPadding="2" rules="none" width="300" align="center" border="1"
				frame="box">
				<tr>
					<td colSpan="2"><asp:label id="LblErrMsg" runat="server" Width="100%" ForeColor="Red"></asp:label></td>
				</tr>
				<tr>
					<td width="40%"><asp:label id="Label4" Runat="server" text="Month" Font-Size="10pt">Month:</asp:label>&nbsp;</td>
					<td width="60%"><asp:dropdownlist id="cmbmonth" runat="server" Width="100%" ForeColor="#003366" AutoPostBack="True"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td width="40%"><asp:label id="Label1" Runat="server" text="Location" Font-Size="10pt">Location:</asp:label>&nbsp;</td>
					<td width="60%"><asp:dropdownlist id="cmbLocation" runat="server" Width="100%" ForeColor="#003366"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td width="40%"><asp:checkbox id="chkupds" ForeColor="#003366" Runat="server" Text="Update Salary"></asp:checkbox></td>
					<td width="60%"><asp:checkbox id="chkuplo" ForeColor="#003366" Runat="server" Text="Update Loan Balance"></asp:checkbox></td>
				</tr>
				<TR height="10">
					<TD style="MARGIN-LEFT: 15px; MARGIN-RIGHT: 15px; BORDER-BOTTOM: #993300 thin groove; HEIGHT: 7px"
						colSpan="2"></TD>
				</TR>
				<tr>
					<td align="center"><asp:button id="cmdok" runat="server" Width="75px" Text="Update"></asp:button></td>
					<td align="right"><asp:button id="cmdcancel" runat="server" Width="75px" Text="Close"></asp:button>&nbsp;
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
