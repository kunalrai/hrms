<%@ Page Language="vb" AutoEventWireup="false" Codebehind="WorkHistEntry.aspx.vb" Inherits="eHRMS.Net.WorkHistEntry"%>
<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>WorkHistEntry</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="coolmenus4.js"></script>
		<script language="vbscript">
			Sub CalMonths()
				document.getElementById("TxtMonths").value = datediff("M", cdate(document.getElementById("DtpEffectiveTxt").value) ,cdate(document.getElementById("dtpToTxt").value)) + 1
			End Sub
			
			Function ValidateCtrl()
				dim tmpVal
				
				tmpVal = document.getElementById("TxtCode").value
				If trim(tmpVal) = "" Then
					msgbox("Please select employee code from the list.")
					ValidateCtrl = false
					exit function
				End If
				tmpVal = document.getElementById("TxtASalary").value
				If (Not IsNumeric(tmpVal)) And tmpVal <> "" Then
					msgbox("Annual Salary must be numeric type.")
					ValidateCtrl = false
					exit function
				End If
				tmpVal = document.getElementById("TxtPAnnual").value				
				If (Not IsNumeric(tmpVal)) And tmpVal <> "" Then
					msgbox("Annual Percentage must be numeric type.")
					ValidateCtrl = false
					exit function
				End If
				tmpVal = document.getElementById("TxtPEquity").value				
				If (Not IsNumeric(tmpVal)) And tmpVal <> "" Then
					msgbox("Equity Percentage must be numeric type.")
					ValidateCtrl = false
					exit function
				End If				
				tmpVal = document.getElementById("TxtPBand").value				
				If (Not IsNumeric(tmpVal)) And tmpVal <> "" Then
					msgbox("Band Percentage must be numeric type.")
					ValidateCtrl = false
					exit function
				End If
				tmpVal = document.getElementById("TxtPMerit").value
				If (Not IsNumeric(tmpVal)) And tmpVal <> "" Then
					msgbox("Merit Percentage must be numeric type.")
					ValidateCtrl = false
					exit function
				End If
				tmpVal = document.getElementById("TxtPPromo").value
				If (Not IsNumeric(tmpVal)) And tmpVal <> "" Then
					msgbox("Promotion Percentage must be numeric type.")
					ValidateCtrl = false
					exit function
				End If
				ValidateCtrl = true
			End Function
			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx -->
			<br>
			<br>
			<TABLE cellSpacing="0" cellPadding="0" align="center" width="780" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Employee 
						Work History ....
					</TD>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="1" align="center"  cellPadding="1" width="780" border="1" frame="box" rules="none">
				<tr>
					<td width="15%"></td>
					<td width="35%"></td>
					<td width="15%"></td>
					<td width="35%"></td>
				</tr>
				<tr>
					<td width="15%" colSpan="4"><asp:label id="LblErrMsg" Visible="False" runat="server" ForeColor="Red" Width="100%"></asp:label></td>
				</tr>
				<tr>
					<td>&nbsp;Employee Code</td>
					<td><asp:textbox id="TxtCode" runat="server" CssClass="TextBox" AutoPostBack="True" Width="75PX"
							ForeColor="#003366"></asp:textbox><asp:dropdownlist id="cmbEmp" runat="server" Width="250px" AutoPostBack="True" Visible="False"></asp:dropdownlist><asp:imagebutton id="btnList" Width="18px" Height="19px" ImageUrl="Images\Find.gif" ImageAlign="AbsMiddle"
							Runat="server"></asp:imagebutton></td>
					<td colspan="2">
						<asp:Label id="LblName" CssClass="Header3" runat="server"></asp:Label></td>
				</tr>
				<tr>
					<td>&nbsp;Department</td>
					<td><asp:dropdownlist id="cmbDept" runat="server" Width="250px"></asp:dropdownlist></td>
					<td>Designation</td>
					<td><asp:dropdownlist id="cmbDesg" runat="server" Width="250px"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td>&nbsp;Division</td>
					<td><asp:dropdownlist id="cmbDivi" runat="server" Width="250px"></asp:dropdownlist></td>
					<td>Section</td>
					<td><asp:dropdownlist id="cmbSect" runat="server" Width="250px"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td>&nbsp;Category</td>
					<td><asp:dropdownlist id="cmbCate" runat="server" Width="250px"></asp:dropdownlist></td>
					<td>Responsibility</td>
					<td><asp:dropdownlist id="cmbResp" runat="server" Width="250px"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td>&nbsp;Band</td>
					<td><asp:dropdownlist id="cmbBand" runat="server" Width="250px"></asp:dropdownlist></td>
					<td>Pay Bucket</td>
					<td><asp:dropdownlist id="cmbPayBuck" runat="server" Width="250px"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td>&nbsp;Hire Date</td>
					<td><cc1:dtp id="DtpHire" runat="server" Width="125px"></cc1:dtp></td>
					<td>Effective Date</td>
					<td><cc1:dtp id="DtpEffective" runat="server" Width="125px"></cc1:dtp></td>
				</tr>
				<tr>
					<td>&nbsp;to Date</td>
					<td><cc1:dtp id="dtpTo" runat="server" Width="125px"></cc1:dtp></td>
					<td>Tot Months</td>
					<td><input class="TextBox" id="TxtMonths" onblur="CalMonths()" type="text" onchange="CalMonths()"
							name="TxtMonths" runat="server" style="COLOR: #003366"></td>
				</tr>
				<tr>
					<td>&nbsp;Annual Salary</td>
					<td><asp:textbox id="TxtASalary" runat="server" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
					<td>Annual %</td>
					<td><asp:textbox id="TxtPAnnual" runat="server" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
				</tr>
				<tr>
					<td>&nbsp;Merit %</td>
					<td><asp:textbox id="TxtPMerit" runat="server" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
					<td>Equity %</td>
					<td><asp:textbox id="TxtPEquity" runat="server" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
				</tr>
				<tr>
					<td>&nbsp;Promotion %</td>
					<td><asp:textbox id="TxtPPromo" runat="server" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
					<td>Band %</td>
					<td><asp:textbox id="TxtPBand" runat="server" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
				</tr>
				<tr>
					<td>&nbsp;Bussines Result</td>
					<td><asp:textbox id="TxtBr" runat="server" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
					<td>People Result</td>
					<td><asp:textbox id="TxtPr" runat="server" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
				</tr>
				<tr>
					<td>&nbsp;Comp. Index</td>
					<td><asp:textbox id="TxtCI" runat="server" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
					<td>I/O Score</td>
					<td><asp:textbox id="TxtIO" runat="server" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
				</tr>
				<tr>
					<td style="HEIGHT: 18px">Performance</td>
					<td style="HEIGHT: 18px"><asp:textbox id="TxtPerformance" runat="server" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
					<td style="HEIGHT: 18px">Status</td>
					<td style="HEIGHT: 18px"><asp:dropdownlist id="cmbStatus" runat="server" Width="208px">
							<asp:ListItem Value="" selected="True"></asp:ListItem>
							<asp:ListItem Value="New Hire">New Hire</asp:ListItem>
							<asp:ListItem Value="Voluntary Retirement">Voluntary Retirement</asp:ListItem>
							<asp:ListItem Value="Company Transfer">Company Transfer</asp:ListItem>
							<asp:ListItem Value="Transfer">Transfer</asp:ListItem>
							<asp:ListItem Value="Deputation">Deputation</asp:ListItem>
							<asp:ListItem Value="Involuntary Retirement">Involuntary Retirement</asp:ListItem>
							<asp:ListItem Value="Voluntary Seperation">Voluntary Seperation</asp:ListItem>
							<asp:ListItem Value="Termination/Death">Termination/Death</asp:ListItem>
							<asp:ListItem Value="Promotion">Promotion</asp:ListItem>
							<asp:ListItem Value="Confirmation">Confirmation</asp:ListItem>
							<asp:ListItem Value="Short Term Injury">Short Term Injury</asp:ListItem>
							<asp:ListItem Value="Long Term Injury">Long Term Injury</asp:ListItem>
							<asp:ListItem Value="Salary Imported">Salary Imported</asp:ListItem>
							<asp:ListItem Value="Salary Changed">Salary Changed</asp:ListItem>
							<asp:ListItem Value="Promotion with Salary Changed">Promotion with Salary Changed</asp:ListItem>
							<asp:ListItem Value="Redesignation">Redesignation</asp:ListItem>
							<asp:ListItem Value="Redesignation With Salary Change">Redesignation With Salary Change</asp:ListItem>
						</asp:dropdownlist></td>
				</tr>
				<tr>
					<td>&nbsp;Remarks</td>
					<td colSpan="3"><asp:textbox id="TxtRemarks" runat="server" Width="100%" CssClass="TextBox"></asp:textbox></td>
				</tr>
				<TR height="15">
					<TD style="MARGIN-LEFT: 15px; MARGIN-RIGHT: 15px; BORDER-BOTTOM: #993300 thin groove; HEIGHT: 7px"
						colSpan="4"></TD>
				</TR>
				<tr>
					<td align="right" colspan="4"><asp:button id="cmdSave" Width="75px" Runat="server" Text="Save"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:button id="cmnClose" Width="75px" Runat="server" Text="Close"></asp:button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
