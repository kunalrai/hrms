<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Reports.aspx.vb" Inherits="eHRMS.Net.Reports" %>
<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Reports</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<script language="vbscript">
				Sub Open1()
					document.getElementById("ee").click
					window.open "frmHTMLReports.aspx"
				End Sub
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="600" align="center">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" style="FONT-SIZE: 18px" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" style="FONT-SIZE: 18px" noWrap align="left" background="Images/TableMid.gif"
						height="19">Report Wizard
					</TD>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</table>
			<table cellSpacing="0" cellPadding="0" width="600" align="center">
				<tr>
					<td colSpan="4">
						<table cellSpacing="0" cellPadding="0" rules="none" width="100%" border="1" frame="border">
							<tr>
								<td width="15%"></td>
								<td width="35%"></td>
								<td width="15%"></td>
								<td width="35%"></td>
							</tr>
							<tr>
								<td>Reports
								</td>
								<td colSpan="3"><asp:listbox id="LstReports" runat="server" Width="100%" Rows="10" AutoPostBack="True"></asp:listbox></td>
							</tr>
							<tr>
								<td style="HEIGHT: 13px">From</td>
								<td style="HEIGHT: 13px"><cc1:dtpcombo id="DtpFrom" runat="server" Width="100%" ToolTip="Date of Birth" Height="20px"></cc1:dtpcombo></td>
								<td style="HEIGHT: 13px">To</td>
								<td style="HEIGHT: 13px"><cc1:dtpcombo id="DtpTo" runat="server" Width="100%" ToolTip="Date of Birth" Height="20px"></cc1:dtpcombo></td>
							</tr>
							<tr>
								<td>For</td>
								<td><asp:dropdownlist id="cmbFor" runat="server" Width="200px" AutoPostBack="True">
										<asp:ListItem Value="1" Selected="True">All</asp:ListItem>
										<asp:ListItem Value="2">Employee</asp:ListItem>
										<asp:ListItem Value="3">Region</asp:ListItem>
										<asp:ListItem Value="4">Location</asp:ListItem>
										<asp:ListItem Value="5">Employee Type</asp:ListItem>
										<asp:ListItem Value="6">Section</asp:ListItem>
										<asp:ListItem Value="7">Function</asp:ListItem>
										<asp:ListItem Value="8">Cost Centre</asp:ListItem>
										<asp:ListItem Value="9">Responsibility</asp:ListItem>
										<asp:ListItem Value="10">Payment Mode</asp:ListItem>
										<asp:ListItem Value="11">Level</asp:ListItem>
										<asp:ListItem Value="12">Designation</asp:ListItem>
										<asp:ListItem Value="13">Pay Bucket</asp:ListItem>
										<asp:ListItem Value="14">Sub Function</asp:ListItem>
										<asp:ListItem Value="15">Sub Sub Function</asp:ListItem>
										<asp:ListItem Value="16">Category</asp:ListItem>
									</asp:dropdownlist></td>
								<td>Select</td>
								<td><asp:dropdownlist id="cmbSelect" runat="server" Width="200px"></asp:dropdownlist></td>
							</tr>
							<tr>
								<td colSpan="4" class="Header3" background="Images\headstripe.jpg"><b>Creteria</b></td>
							</tr>
							<tr>
								<td colSpan="4"><asp:textbox id="TxtCriteria" runat="server" Width="100%" TextMode="MultiLine" Rows="3"></asp:textbox></td>
							</tr>
							<tr>
								<td colSpan="2"></td>
								<td align="right" colSpan="2"><INPUT onclick="Open1()" id="cmdPrintView" type="button" value="Print View" name="cmdPrintView"
										runat="server">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:button id="cmdClose" runat="server" Text="Close"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<asp:TextBox id="TxtBox" style="Z-INDEX: 101; LEFT: 760px; POSITION: absolute; TOP: 8px" runat="server"
				Width="0px"></asp:TextBox>
			<asp:Button id="ee" style="Z-INDEX: 102; LEFT: 648px; POSITION: absolute; TOP: 312px" Width="0px"
				runat="server" Text=""></asp:Button></form>
	</body>
</HTML>
