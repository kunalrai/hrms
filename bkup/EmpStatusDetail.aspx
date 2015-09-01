<%@ Page Language="vb" AutoEventWireup="false" Codebehind="EmpStatusDetail.aspx.vb" Inherits="eHRMS.Net.EmpStatusDetail"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Employee Status</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout" leftmargin="15"  bottommargin="0"  rightmargin="15">
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="350" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Employee 
						Status ....</TD>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<TABLE cellSpacing="1" cellPadding="1" width="350" border="1">
				<tr height="30">
					<TD class="header3" width="60%">Status As On:</TD>
					<td class="header3" width="40%"><asp:label id="LblDate" runat="server" Visible="True"></asp:label></td>
				</tr>
				<tr height="30">
					<TD class="header3" width="60%">Total No of Employee:</TD>
					<td class="header3" width="40%"><asp:label id="lblMsg" runat="server" Visible="True"></asp:label></td>
				</tr>
				<tr height="30">
					<TD class="header3" width="60%">Total No of Live Employee:</TD>
					<td class="header3" width="40%"><asp:label id="LblLive" runat="server" Visible="True"></asp:label></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
