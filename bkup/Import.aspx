<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Import.aspx.vb" Inherits="eHRMS.Net.Import" %>
<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>MonthEndProcess</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<!--#include file=MenuBars.aspx --><br>
		<br>
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="500" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="15">&nbsp;
					</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Import 
						Data From Excel
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" style="WIDTH: 27px; HEIGHT: 19px" height="19" src="Images/TableRight.gif"
							width="27">
					</TD>
				</tr>
			</TABLE>
			<TABLE cellSpacing="1" cellPadding="2" rules="none" width="500" align="center" border="1"
				frame="box">
				<tr>
					<td colSpan="4"><asp:label id="LblErrMsg" runat="server" Width="100%" ForeColor="Red"></asp:label></td>
				</tr>
				<tr>
					<td align="left" width="25%"><asp:label id="Label4" Runat="server" text="Month" Font-Size="10pt">Format</asp:label></td>
					<td align="left" width="75%" colSpan="3"><asp:dropdownlist id="cmbFormat" runat="server" Width="100%" AutoPostBack="True"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td align="left" width="25%"><asp:label id="Label1" Runat="server" text="File" Font-Size="10pt">Excel File</asp:label></td>
					<td align="left" width="75%" colSpan="3"><INPUT id="flImport" title="Import" style="WIDTH: 100%" type="file" align="middle" name="flImport"
							runat="server"></td>
				</tr>
				<tr>
					<td align="left" width="25%"><asp:label id="Label5" Runat="server" text="File" Font-Size="10pt">WorkSheet Name</asp:label></td>
					<td align="left" width="75%" colSpan="3"><asp:textbox id="txtSheetName" runat="server" Width="100%"></asp:textbox></td>
				</tr>
				<tr>
					<td align="left" width="25%"><asp:label id="Label2" Runat="server" text="File" Font-Size="10pt">From Date</asp:label></td>
					<td align="left" width="25%"><cc1:dtp id="DtpFDate" runat="server" Width="100%" ToolTip="From Date"></cc1:dtp></td>
					<td align="left" width="25%"><asp:label id="Label3" Runat="server" text="File" Font-Size="10pt">To Date</asp:label></td>
					<td align="left" width="25%"><cc1:dtp id="DtpTDate" runat="server" Width="100%" ToolTip="To Date"></cc1:dtp></td>
				</tr>
				<tr>
					<td align="center" width="25%"><asp:button id="cmdImport" runat="server" Width="80%" Text="Import"></asp:button></td>
					<td align="center" width="25%"><asp:button id="cmdFormat" runat="server" Width="80%" Text=" Format"></asp:button></td>
					<td align="center" width="25%"><asp:button id="cmdUpload" runat="server" Width="80%" Text="Up-Load"></asp:button></td>
					<td align="center" width="25%"><asp:button id="cmdClose" runat="server" Width="80%" Text="Exit"></asp:button></td>
				</tr>
			</TABLE>
			<HR>
			<DIV align="center">
				<asp:datagrid id="grdImport" style="Z-INDEX: 101" runat="server" AutoGenerateColumns="True">
					<HeaderStyle Font-Bold="True" ForeColor="Black" BorderStyle="Solid" BorderColor="Black" BackColor="#C0C0FF"></HeaderStyle>
				</asp:datagrid>
				<br>
			</DIV>
		</form>
	</body>
</HTML>
