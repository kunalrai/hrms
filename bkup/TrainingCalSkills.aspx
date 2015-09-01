<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TrainingCalSkills.aspx.vb" Inherits="eHRMS.Net.TrainingCalSkills"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>TrainingCalSkills</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="800" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" style="FONT-SIZE: 18px" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" style="FONT-SIZE: 18px" noWrap align="left" background="Images/TableMid.gif"
						height="19">Training Calendar....
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" width="800" border="1">
				<tr>
					<td width="15%"></td>
					<td width="30%"></td>
					<td width="15%"></td>
					<td width="20%"></td>
					<td width="20%"></td>
				</tr>
				<tr>
					<td colSpan="5"><asp:label id="LblErrMsg" runat="server" ForeColor="Red" Width="100%" Font-Bold="True" Font-Size="8"></asp:label></td>
				</tr>
				<tr>
					<td width="100">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/leftoverlap.gif" border="0"></td>
								<td style="COLOR: #003366" align="center" bgColor="#cecbce"><b>General</b></td>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
							</tr>
						</table>
					</td>
					<td colspan="4" width="100">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td width="10" bgColor="#666666"><IMG alt="" src="Images/SplitLeft.gif" border="0"></td>
								<td style="COLOR: white" align="center" bgColor="#666666"><b>Skills</b></td>
								<td width="10" bgColor="#666666"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="5"></td>
				</tr>
				<tr>
					<td align="right" colSpan="5"><asp:button id="cmdSave" runat="server" Width="80px" Text="Save"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:button id="cmdClose" runat="server" Width="80px" Text="Close"></asp:button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
