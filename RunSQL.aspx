<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary"%>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="RunSQL.aspx.vb" Inherits="eHRMS.Net.RunSQL"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>RunSQL</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx--><br>
			<br>
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="600" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">
						Run SQL Command.....
					</TD>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="2" cellPadding="0" rules="none" width="605" align="center" border="1"
				style="WIDTH: 605px; HEIGHT: 243px">
				<tr>
					<td style="HEIGHT: 1px" colSpan="4"><asp:label id="LblErrMsg" runat="server" Width="320px" ForeColor="Red"></asp:label></td>
				</tr>
				<TR>
					<TD style="HEIGHT: 1px" colSpan="4">
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" align="left" border="1">
							<TR>
								<TD style="HEIGHT: 11px" width="20%">&nbsp;<asp:label id="Label3" Width="100%" text="SQL Command" Runat="server" Font-Bold="True">SQL Command</asp:label></TD>
								<TD width="70%" colSpan="3"><asp:dropdownlist id="cmbCommand" runat="server" Width="100%" AutoPostBack="True"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 11px" width="20%">&nbsp;<asp:label id="LbldtpFrom" Width="100%" text="From" Runat="server" Font-Bold="True">From</asp:label></TD>
								<TD width="30%"><cc1:dtp id="DtpFrom" runat="server" ForeColor="#003366" ToolTip="From" width="100%"></cc1:dtp></TD>
								<TD style="HEIGHT: 11px" width="20%">&nbsp;<asp:label id="lblDtpTo" Width="100%" text="From" Runat="server" Font-Bold="True">To</asp:label></TD>
								<TD width="30%"><cc1:dtp id="dtpTo" runat="server" ForeColor="#003366" ToolTip="To Date" width="100%"></cc1:dtp></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 11px" width="20%">&nbsp;<asp:label id="Label1" Width="100%" text="For" Runat="server" Font-Bold="True">For</asp:label></TD>
								<TD width="30%"><FONT face="Verdana" color="#000066" size="2"><asp:dropdownlist id="CmbFor" runat="server" Width="100%" AutoPostBack="True"></asp:dropdownlist></FONT></TD>
								<TD style="HEIGHT: 11px" width="20%">&nbsp;</TD>
								<TD width="30%"><asp:dropdownlist id="CmbFilter" runat="server" Width="100%" AutoPostBack="True" Height="27px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 11px" colSpan="4">
									<TABLE id="TableCmd" cellSpacing="2" cellPadding="2" width="100%" align="center" border="0">
										<TR>
											<TD align="center" width="20%"><asp:button id="cmdDetail" runat="server" Width="100%" Text="Detail View"></asp:button></TD>
											<TD align="center" width="20%"><asp:button id="cmdCons" runat="server" Width="100%" Text="Consolidate View"></asp:button></TD>
											<TD align="center" width="20%"><asp:button id="cmdProceed" runat="server" Width="100%" Text="Proceed"></asp:button></TD>
											<TD align="center" width="20%"><asp:button id="cmdExcelView" runat="server" Width="100%" Text="Excel View"></asp:button></TD>
											<TD align="center" width="20%"><asp:button id="cmdClose" runat="server" Width="100%" Text="Exit"></asp:button></TD>
										</TR>
									</TABLE>
								</TD>
								<TD vAlign="middle" colSpan="4">
									<hr style="BORDER-BOTTOM: #993366 thin solid">
								</TD>
							</TR>
						</TABLE>
					</TD>
				<TR>
					<TD align="center" colSpan="4"><asp:datagrid id="grdList" runat="server" EnableViewState="False"></asp:datagrid></TD>
				</TR>
				</TD></TR></table>
		</form>
	</body>
</HTML>
