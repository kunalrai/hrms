<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
<%@ Page Language="vb" Debug="true" AutoEventWireup="false" Codebehind="FrmReports.aspx.vb" Inherits="eHRMS.Net.FrmReports" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmReports</title>
		<meta content="False" name="vs_snapToGrid">
		<meta content="True" name="vs_showGrid">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
		<script language="vbscript">
			Sub OpenWin()
				if document.getElementById("OptHtml").checked then
					document.getElementById("ReportType").value = "frmHTMLReports.aspx"
				elseif document.getElementById("optCrystal").checked then
					document.getElementById("ReportType").value = "ReportView.aspx"
				end if	
				document.getElementById("cmdSetValues").click
				window.open document.getElementById("ReportType").value,"","status=yes,toolbar=yes,menubar=yes,scrollbars=yes,resizable=yes"
			End Sub
			
			Sub OpenPrintWin()
				if document.getElementById("OptHtml").checked then
					document.getElementById("ReportType").value = "frmHTMLReports.aspx"
				elseif document.getElementById("optCrystal").checked then
					document.getElementById("ReportType").value = "ReportView.aspx"
				end if	
				document.getElementById("cmdPrint").click
				msgbox document.getElementById("ReportType").value
				window.open document.getElementById("ReportType").value,"","status=yes,scrollbars=yes,resizable=yes"
			End Sub
			
			Sub OpenProp()
				Dim Var
				Var = document.getElementById("TxtProp").value
				if Var  <> "" Then 
					window.open "ReportProperties.aspx?Rpt=" & Var,"","status=yes,scrollbars=yes,resizable=no,width=550,height=275,top=290,left=375"
				Else
					msgbox "Please Select Report From the List."		
				End if
			End Sub
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" runat="server">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx --><br>
			<br>
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
			<TABLE borderColor="#98a9ca" cellSpacing="0" cellPadding="0" rules="none" width="600" align="center"
				border="1" frame="box">
				<tr>
					<td width="100%" colSpan="5"><asp:label id="LblMsg" runat="server" Font-Size="11px" ForeColor="Red" Width="100%"></asp:label></td>
				</tr>
				<tr>
					<td vAlign="top" width="55%" rowSpan="4"><asp:listbox id="LstReports" runat="server" ForeColor="#003366" Width="100%" Rows="7" Height="300px"
							AutoPostBack="True"></asp:listbox></td>
					<td vAlign="top" width="45%">
						<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
							<tr>
								<td class="Header3" align="center" background="Images\headstripe.jpg" colSpan="2"><b>Period</b></td>
							</tr>
						</TABLE>
						<TABLE id="TblMonthRange" borderColor="#98a9ca" cellSpacing="0" cellPadding="0" rules="none"
							width="100%" align="center" border="1" frame="border" runat="server">
							<TR>
								<TD>&nbsp;</TD>
								<TD>Month</TD>
								<TD>Year</TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 11px">From</TD>
								<TD style="HEIGHT: 11px"><asp:dropdownlist id="cmbMonthFrom" runat="server" Width="100px">
										<asp:ListItem Value="1">January</asp:ListItem>
										<asp:ListItem Value="2">February</asp:ListItem>
										<asp:ListItem Value="3">March</asp:ListItem>
										<asp:ListItem Value="4">April</asp:ListItem>
										<asp:ListItem Value="5">May</asp:ListItem>
										<asp:ListItem Value="6">June</asp:ListItem>
										<asp:ListItem Value="7">July</asp:ListItem>
										<asp:ListItem Value="8">August</asp:ListItem>
										<asp:ListItem Value="9">September</asp:ListItem>
										<asp:ListItem Value="10">October</asp:ListItem>
										<asp:ListItem Value="11">November</asp:ListItem>
										<asp:ListItem Value="12">December</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD style="HEIGHT: 11px"><asp:dropdownlist id="cmbYearFrom" runat="server" Width="55px">
										<asp:ListItem Value="1">2003</asp:ListItem>
										<asp:ListItem Value="2">2004</asp:ListItem>
										<asp:ListItem Value="3">2005</asp:ListItem>
										<asp:ListItem Value="4">2006</asp:ListItem>
										<asp:ListItem Value="5">2007</asp:ListItem>
									</asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD>To</TD>
								<TD><asp:dropdownlist id="cmbMonthTo" runat="server" Width="100px">
										<asp:ListItem Value="1">January</asp:ListItem>
										<asp:ListItem Value="2">February</asp:ListItem>
										<asp:ListItem Value="3">March</asp:ListItem>
										<asp:ListItem Value="4">April</asp:ListItem>
										<asp:ListItem Value="5">May</asp:ListItem>
										<asp:ListItem Value="6">June</asp:ListItem>
										<asp:ListItem Value="7">July</asp:ListItem>
										<asp:ListItem Value="8">August</asp:ListItem>
										<asp:ListItem Value="9">September</asp:ListItem>
										<asp:ListItem Value="10">October</asp:ListItem>
										<asp:ListItem Value="11">November</asp:ListItem>
										<asp:ListItem Value="12">December</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD><asp:dropdownlist id="cmbYearTo" runat="server" Width="55px">
										<asp:ListItem Value="1">2003</asp:ListItem>
										<asp:ListItem Value="2">2004</asp:ListItem>
										<asp:ListItem Value="3">2005</asp:ListItem>
										<asp:ListItem Value="4">2006</asp:ListItem>
										<asp:ListItem Value="5">2007</asp:ListItem>
									</asp:dropdownlist></TD>
							</TR>
						</TABLE>
						<TABLE id="TblMonth" borderColor="#98a9ca" cellSpacing="0" cellPadding="0" rules="none"
							width="100%" align="center" border="1" frame="border" runat="server">
							<TR>
								<TD>&nbsp; Month</TD>
								<TD>&nbsp;Year</TD>
							</TR>
							<TR>
								<TD align="center"><asp:dropdownlist id="cmbMonth" runat="server" Width="100px">
										<asp:ListItem Value="1">January</asp:ListItem>
										<asp:ListItem Value="2">February</asp:ListItem>
										<asp:ListItem Value="3">March</asp:ListItem>
										<asp:ListItem Value="4">April</asp:ListItem>
										<asp:ListItem Value="5">May</asp:ListItem>
										<asp:ListItem Value="6">June</asp:ListItem>
										<asp:ListItem Value="7">July</asp:ListItem>
										<asp:ListItem Value="8">August</asp:ListItem>
										<asp:ListItem Value="9">September</asp:ListItem>
										<asp:ListItem Value="10">October</asp:ListItem>
										<asp:ListItem Value="11">November</asp:ListItem>
										<asp:ListItem Value="12">December</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD align="center"><asp:dropdownlist id="cmbYear" runat="server" Width="55px">
										<asp:ListItem Value="1">2003</asp:ListItem>
										<asp:ListItem Value="2">2004</asp:ListItem>
										<asp:ListItem Value="3">2005</asp:ListItem>
										<asp:ListItem Value="4" Selected="True">2006</asp:ListItem>
										<asp:ListItem Value="5">2007</asp:ListItem>
									</asp:dropdownlist></TD>
							</TR>
						</TABLE>
						<TABLE id="TblDate" borderColor="#98a9ca" cellSpacing="0" cellPadding="0" rules="none"
							width="100%" align="center" border="1" frame="border" runat="server">
							<TR>
								<TD align="center"><asp:label id="Label3" runat="server" Width="100px">
										<font color="navy">As on Date</font></asp:label></TD>
							</TR>
							<TR>
								<TD align="center"><cc1:dtpcombo id="dtpAsOn" runat="server" Width="100%" Height="20px" ToolTip="As on Date"></cc1:dtpcombo></TD>
							</TR>
						</TABLE>
						<TABLE id="TblDateRange" borderColor="#98a9ca" cellSpacing="0" cellPadding="0" rules="none"
							width="100%" align="center" border="1" frame="border" runat="server">
							<TR>
								<TD>From</TD>
								<TD><cc1:dtpcombo id="dtpFrom" runat="server" Width="100%" Height="20px" ToolTip="Start Date"></cc1:dtpcombo></TD>
							</TR>
							<TR>
								<TD>To</TD>
								<TD><cc1:dtpcombo id="dtpTo" runat="server" Width="100%" Height="20px" ToolTip="End Date"></cc1:dtpcombo></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td vAlign="top" width="225">
						<TABLE id="TBLFOR" borderColor="#98a9ca" cellSpacing="0" cellPadding="0" rules="none" width="268"
							align="center" border="1" frame="border" runat="server" style="WIDTH: 268px; HEIGHT: 81px">
							<tr>
								<td class="Header3" align="center" background="Images\headstripe.jpg" colSpan="2"><b>For</b></td>
							</tr>
							<tr>
								<td style="HEIGHT: 11px"><asp:dropdownlist id="cmbFor" runat="server" Width="262px" AutoPostBack="True"></asp:dropdownlist></td>
							</tr>
							<tr>
								<td><asp:dropdownlist id="cmbForVal" runat="server" Width="242px" AutoPostBack="True"></asp:dropdownlist><asp:textbox id="TxtFroVal" ForeColor="#003366" Width="237px" Visible="False" Runat="server"
										CssClass="TextBox"></asp:textbox><asp:button id="CmdFor" runat="server" Font-Size="12pt" ForeColor="DimGray" Width="24px" ToolTip="Click to Find Details"
										Text="&amp;" Font-Names="Wingdings" Font-Bold="True"></asp:button></td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td vAlign="top" width="180">
						<TABLE id="TblOrderBy" cellSpacing="0" cellPadding="0" width="269" align="center" border="0"
							runat="server" style="WIDTH: 269px; HEIGHT: 32px">
							<tr>
								<td class="Header3" align="center" background="Images\headstripe.jpg" colSpan="2" style="WIDTH: 271px"><b>Order 
										by</b></td>
							</tr>
							<tr>
								<td style="WIDTH: 271px"><asp:dropdownlist id="cmbOrdBy" runat="server" Width="266px"></asp:dropdownlist></td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td width="180">
						<TABLE id="TblLType" cellSpacing="0" cellPadding="0" width="95%" align="center" border="0"
							runat="server">
							<tr>
								<td class="Header3" align="center" background="Images\headstripe.jpg" colSpan="2"><b>Employee 
										Status</b></td>
							</tr>
							<tr>
								<td><asp:dropdownlist id="cmbLTYPE" runat="server" Width="266px">
										<asp:ListItem Value="0" Selected="True">&nbsp;</asp:ListItem>
										<asp:ListItem Value="1">Active</asp:ListItem>
										<asp:ListItem Value="2">Resigned</asp:ListItem>
										<asp:ListItem Value="3">Terminated</asp:ListItem>
										<asp:ListItem Value="4">Retired</asp:ListItem>
										<asp:ListItem Value="5">Payhold</asp:ListItem>
										<asp:ListItem Value="6">Saperated</asp:ListItem>
										<asp:ListItem Value="7">Death</asp:ListItem>
										<asp:ListItem Value="8">Transfer</asp:ListItem>
										<asp:ListItem Value="9">Regularized</asp:ListItem>
									</asp:dropdownlist></td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td class="Header3" background="Images\headstripe.jpg" colSpan="2"><b>
							<P class="MsoNormal">Criterion</P>
						</b>
					</td>
				</tr>
				<tr>
					<td vAlign="top" align="left" colSpan="2"><asp:textbox id="TxtCriteria" runat="server" Width="100%" Rows="4" TextMode="MultiLine">Not ({HRDMASTQRY.EMP_CODE} IN [''])</asp:textbox></td>
				</tr>
				<tr>
					<td vAlign="top" align="left"><asp:label id="LblPages" style="Z-INDEX: 105" runat="server" Font-Size="X-Small" ForeColor="Blue">Format</asp:label>&nbsp;&nbsp;
						<asp:dropdownlist id="CmbExport" style="Z-INDEX: 107" runat="server" Width="120px" Height="24px">
							<asp:ListItem Value="0" >Excel</asp:ListItem>
							<asp:ListItem Selected="True" Value="1">PDF</asp:ListItem>
							<asp:ListItem Value="2">Word Document</asp:ListItem>
						</asp:dropdownlist></td>
					<td vAlign="top" align="left"><asp:radiobutton id="OptHtml" runat="server" Text="HTML" GroupName="optReportType"></asp:radiobutton><asp:radiobutton id="optCrystal" runat="server" Text="Crystal" GroupName="optReportType" Checked="True"></asp:radiobutton></td>
				</tr>
				<tr>
					<td vAlign="top" align="right" colSpan="2">
						<hr width="100%" color="#98a9ca">
						<asp:button id="cmdPrint" accessKey="P" runat="server" Width="75px" Visible="False" Text="Print"></asp:button>&nbsp;
						<asp:button id="cmdExport" accessKey="E" runat="server" Width="75px" Visible="False" Text="Export"></asp:button>&nbsp;
						<asp:button id="cmdMail" accessKey="M" runat="server" Width="75px" Visible="False" Text="E-Mail"></asp:button>&nbsp;
						<INPUT id="cmdView" onclick="OpenWin();" type="button" value="  View  " name="cmdView"
							runat="server">&nbsp; <INPUT id="cmdProp" onclick="OpenProp();" type="button" size="10" value="Properties" runat="server">&nbsp;
						<asp:button id="cmdClose" runat="server" Width="75px" Text="Close"></asp:button></td>
				</tr>
			</TABLE>
			<asp:button id="cmdRefresh" style="Z-INDEX: 104; LEFT: 864px; POSITION: absolute; TOP: 32px"
				runat="server" Width="0px" Text="Button"></asp:button><asp:button id="cmdSetValues" style="Z-INDEX: 101; LEFT: 184px; POSITION: absolute; TOP: 30px"
				runat="server" Width="0px" Text="Button"></asp:button><asp:textbox id="ReportType" style="Z-INDEX: 102; LEFT: 154px; POSITION: absolute; TOP: 37px"
				runat="server" Width="0px" Height="0px" ReadOnly="True">frmHTMLReports.aspx</asp:textbox><asp:textbox id="TxtProp" style="Z-INDEX: 103; LEFT: 16px; POSITION: absolute; TOP: 8px" runat="server"
				Width="0px"></asp:textbox></form>
	</body>
</HTML>
