<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary"%>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Company_Setup.aspx.vb" Inherits="eHRMS.Net.Company_Setup" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>CompanySetup</title>
		<meta name="vs_showGrid" content="False">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
		<SCRIPT language="javascript" src="Common.js"></SCRIPT>
		<script language="vbscript">
			Sub ShowSubTab(argID)
					document.getElementById("TblGeneral").style.display="none"
					document.getElementById("TblSignatory").style.display="none"
					document.getElementById("TblReimbursement").style.display="none"

					document.getElementById("TrGeneral").style.fontWeight = "normal"
					document.getElementById("TrSignatory").style.fontWeight = "normal"
					document.getElementById("TrReimbursement").style.fontWeight = "normal"
					
					document.getElementById(Replace(argID,"Tr","Tbl")).style.display="block"
					document.getElementById(argID).style.fontWeight = "bold"
			End Sub

					
				Sub CheckDate(argID)
				Dim TVal
				TVal = document.getElementById(argID).value
				if TVal="" then Exit Sub
				if isdate(TVal) then 
					If Len(TVal) = 11 Then
						If Not ((Mid(TVal, 3, 1) = "/" Or Mid(TVal, 3, 1) = "-") And (Mid(TVal, 7, 1) = "/" Or Mid(TVal, 7, 1) = "-")) Then
							MsgBox "Invalid Format! Please Enter in [dd/MMM/yyyy] Format.", , "Date Format"
							document.getElementById(argID).value = ""
						else
							DiffYears(Replace(argID,"DOB","Age"))
						End If
					ElseIf Len(TVal) = 10 Then
						document.getElementById(argID).value = Left(TVal,2) & "/" & MonthName(Mid(TVal,4,2),true) & "/" & right(TVal,4)		
						DiffYears(Replace(argID,"DOB","Age"))
					Else
						MsgBox "Invalid Format! Please Enter in [dd/MMM/yyyy] Format.", , "Date Format"
						document.getElementById(argID).value = ""
					End If
				Else
					MsgBox "Invalid Date!", vbokOnly, "Date Format"
					document.getElementById(argID).value = ""
				End if
			End Sub	
		</script>
		<script language="vbscript">
			Sub CheckNum(argID)
				Dim TVal
				TVal = document.getElementById(argID).value
				if trim(TVal) = "" then 
					document.getElementById(argID).value = 0
				Exit Sub
				End if
				if Not IsNumeric(TVal) then
						MsgBox "Invalid Value! Please Enter numeric Value.", , "Divergent"
						document.getElementById(argID).value = 0
				End if
			End Sub	
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx --><br>
			<br>
			<br>
			<table cellSpacing="0" cellPadding="0" width="600" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="15">&nbsp;
					</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Company 
						Setup....
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" style="WIDTH: 27px; HEIGHT: 19px" height="19" src="Images/TableRight.gif"
							width="27">
					</TD>
				</tr>
			</table>
			<TABLE cellSpacing="2" cellPadding="0" rules="none" width="600" align="center" border="1"
				frame="box">
				<tr>
					<td width="33%"></td>
					<td width="34%"></td>
					<td width="33%"></td>
				</tr>
				<tr>
					<td colSpan="3" valign="top"><asp:label id="LblErrMsg" runat="server" Width="100%" ForeColor="Red"></asp:label></td>
				</tr>
				<tr>
					<td>
						<table id="TrGeneral" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/leftoverlap.gif" border="0">
								</td>
								<td style="CURSOR: hand; COLOR: #003366" onclick="ShowSubTab('TrGeneral')" align="center"
									bgColor="#cecbce">General
								</td>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/rightcurve.gif" border="0">
								</td>
							</tr>
						</table>
					</td>
					<td>
						<table id="TrSignatory" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/SplitLeft.gif" border="0"></td>
								<td style="CURSOR: hand; COLOR: #003366" onclick="ShowSubTab('TrSignatory')" align="center"
									bgColor="#cecbce">Signatory</td>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/rightcurve.gif" border="0">
								</td>
							</tr>
						</table>
					</td>
					<td>
						<table id="TrReimbursement" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/SplitLeft.gif" border="0"></td>
								<td style="CURSOR: hand; COLOR: #003366" onclick="ShowSubTab('TrReimbursement')" align="center"
									bgColor="#cecbce">Reimbursement
								</td>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="3" height="7"></td>
				</tr>
				<tr>
					<td colSpan="3">
						<table id="TblGeneral" cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td width="15%"></td>
								<td width="35%"></td>
								<td width="15%"></td>
								<td width="35%"></td>
							</tr>
							<tr>
								<td>&nbsp;<asp:label id="Label1" Runat="server" text="Code"></asp:label></td>
								<td><asp:textbox id="txtcode" ForeColor="#003366" Runat="server" CssClass="textbox"></asp:textbox></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td>&nbsp;<asp:label id="Label4" Runat="server" text="Name"></asp:label></td>
								<td colspan="3"><asp:textbox id="txtname" Width="100%" ForeColor="#003366" Runat="server" CssClass="textbox"></asp:textbox></td>
							</tr>
							<tr>
								<td>&nbsp;<asp:label id="Label5" Runat="server" text="Address"></asp:label></td>
								<td colspan="3"><asp:textbox id="txtaddress" Width="100%" ForeColor="#003366" Runat="server" CssClass="textbox"></asp:textbox></td>
							</tr>
							<TR>
								<TD>&nbsp;<asp:label id="Label6" text="City" Runat="server"></asp:label></TD>
								<TD><asp:textbox id="txtcity" ForeColor="#003366" Width="98%" Runat="server" CssClass="textbox"></asp:textbox></TD>
								<TD><asp:label id="Label7" text="State" Runat="server"></asp:label></TD>
								<TD><asp:dropdownlist id="cmbstate" Width="100%" Runat="server" ForeColor="#003366"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD>&nbsp;<asp:label id="Label8" text="Pin" Runat="server"></asp:label></TD>
								<TD><asp:textbox id="txtpin" onblur="CheckNum(this.id)" ForeColor="#003366" Width="98%" Runat="server"
										CssClass="textbox" MaxLength="6"></asp:textbox></TD>
								<TD><asp:label id="Label9" text="PF No" Runat="server"></asp:label></TD>
								<TD><asp:textbox id="txtpfno" ForeColor="#003366" Width="100%" Runat="server" CssClass="textbox"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>&nbsp;<asp:label id="Label10" text="ESI No" Runat="server"></asp:label></TD>
								<TD><asp:textbox id="txtesino" ForeColor="#003366" Width="98%" Runat="server" CssClass="textbox"></asp:textbox></TD>
								<TD><asp:label id="Label11" text="PAN/GIR NO" Runat="server"></asp:label></TD>
								<TD><asp:textbox id="TxtPANNo" ForeColor="#003366" Width="100%" Runat="server" CssClass="textbox"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>&nbsp;<asp:label id="Label12" text="TAN No" Runat="server"></asp:label></TD>
								<TD><asp:textbox id="txttanno" ForeColor="#003366" Width="98%" Runat="server" CssClass="textbox"></asp:textbox></TD>
								<TD><asp:label id="Label13" text="TDS Circle" Runat="server"></asp:label></TD>
								<TD><asp:textbox id="TxtTdsAmt" ForeColor="#003366" Width="100%" onblur="CheckNum(this.id)" Runat="server"
										CssClass="textbox"></asp:textbox></TD>
							</TR>
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="3">
						<table id="TblSignatory" cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td width="17%"></td>
								<td width="33%"></td>
								<td width="15%"></td>
								<td width="35%"></td>
							</tr>
							<tr>
								<td>&nbsp;<asp:label id="Label14" Runat="server" text="Signatory"></asp:label></td>
								<td colSpan="3"><asp:textbox id="txtsignatory" Width="98%" ForeColor="#003366" Runat="server" CssClass="textbox"></asp:textbox></td>
							</tr>
							<tr>
								<td>&nbsp;<asp:label id="Label15" Runat="server" text="Designation"></asp:label></td>
								<td colSpan="3"><asp:textbox id="txtdesig" Width="98%" ForeColor="#003366" Runat="server" CssClass="textbox"></asp:textbox></td>
							</tr>
							<tr>
								<td>&nbsp;<asp:label id="Label16" Runat="server" text="Father's Name"></asp:label></td>
								<td colSpan="3"><asp:textbox id="txtfathersname" Width="98%" ForeColor="#003366" Runat="server" CssClass="textbox"></asp:textbox></td>
							</tr>
							<tr>
								<td>&nbsp;<asp:label id="Label17" Runat="server" text="Address"></asp:label></td>
								<td colSpan="3"><asp:textbox id="txtaddres" Width="98%" ForeColor="#003366" Runat="server" CssClass="textbox"></asp:textbox></td>
							</tr>
							<tr>
								<td>&nbsp;<asp:label id="Label18" Runat="server" text="City"></asp:label></td>
								<td><asp:textbox id="txtcit" Width="100%" ForeColor="#003366" Runat="server" CssClass="textbox"></asp:textbox></td>
								<td>&nbsp;<asp:label id="Label19" Runat="server" text="State"></asp:label></td>
								<td align="left"><asp:dropdownlist id="cmbstat" Width="96%" Runat="server" ForeColor="#003366"></asp:dropdownlist></td>
							</tr>
							<TR>
								<TD style="HEIGHT: 5px">&nbsp;<asp:label id="Label20" text="Pin" Runat="server"></asp:label></TD>
								<TD style="HEIGHT: 5px"><asp:textbox id="txtpi" onblur="CheckNum(this.id)" ForeColor="#003366" Width="55px" Runat="server"
										CssClass="textbox" MaxLength="6"></asp:textbox>
									&nbsp;<asp:label id="Label21" text="Place" Runat="server"></asp:label>
									<asp:textbox id="txtplace" ForeColor="#003366" Width="97px" Runat="server" CssClass="textbox"></asp:textbox></TD>
								<td style="HEIGHT: 5px">&nbsp;<asp:label id="Label22" text="Date" Runat="server"></asp:label></td>
								<td style="HEIGHT: 5px"><cc1:dtpcombo id="dtpdate" runat="server" ToolTip="Date" visible="true" Width="176px"></cc1:dtpcombo></td>
							</TR>
							<tr>
								<td colSpan="4" height="10"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="3">
						<table id="TblReimbursement" cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td>&nbsp;<asp:checkbox id="chkusesppc" Runat="server" Text="Use Stored Procedure for Prorata Calculation"
										ForeColor="#003366"></asp:checkbox></td>
							</tr>
							<tr>
								<td>&nbsp;<asp:checkbox id="chkarmte" Runat="server" Text="Allow Reimbursement more than Entitlement" ForeColor="#003366"></asp:checkbox></td>
							</tr>
							<tr>
								<td>&nbsp;<asp:checkbox id="chktelr" Runat="server" Text="Take Effect of LWOP on Reimbursement" ForeColor="#003366"></asp:checkbox></td>
							</tr>
							<tr>
								<td height="20"></td>
							</tr>
							<tr>
								<td>&nbsp;<asp:label id="Label26" Runat="server" text="Reimbursement Entitlement Calculated Based On">Reimbursement Entitlement Calculated Based On:-</asp:label></td>
							</tr>
							<tr>
								<td height="10"></td>
							</tr>
							<tr>
								<td>
									<asp:radiobuttonlist id="RblCalculate" runat="server" Width="250px" ForeColor="#003366">
										<asp:ListItem Value="1">Begining of Month Entry Day.</asp:ListItem>
										<asp:ListItem Value="2">Till Entry Date.</asp:ListItem>
										<asp:ListItem Value="3">End of Month of Entry Date.</asp:ListItem>
									</asp:radiobuttonlist>
								</td>
							</tr>
							<tr>
								<td height="10"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<TD style="MARGIN-LEFT: 15px; MARGIN-RIGHT: 15px; BORDER-BOTTOM: #993300 thin groove; HEIGHT: 7px"
						colSpan="3"></TD>
				</TR>
				<tr>
					<td height="5" colspan="3"></td>
				</tr>
				<tr>
					<td align="right" colspan="3"><asp:button id="BtnOk" Width="75px" Runat="server" Text="Update" ToolTip="Click Here For Update Company Details."></asp:button>&nbsp;
						<asp:button id="BtnCancel" runat="server" Width="75px" Text="Close" ToolTip="Click Here For Close."></asp:button>&nbsp;
					</td>
				</tr>
				<tr>
					<td colSpan="3" height="5"></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
