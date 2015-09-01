<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CompanySetup.aspx.vb" Inherits="eHRMS.Net.CompanySetup" %>
<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>CompanySetup</title>
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
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="600" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="15">&nbsp;
					</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Company 
						Setup (Edit)
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" style="WIDTH: 27px; HEIGHT: 19px" height="19" src="Images/TableRight.gif"
							width="27">
					</TD>
				</tr>
			</table>
			<TABLE  cellSpacing="2" cellPadding="0" rules="none" width="600" align="center"	border="1" frame="box">
				<tr>
					<td width="33%"></td>
					<td width="34%"></td>
					<td width="33%"></td>
				</tr>
				<tr>
					<td colSpan="3"><asp:label id="LblErrMsg" runat="server" ForeColor="Red" Width="100%"></asp:label></td>
				</tr>
				<tr>
					<td>
						<table id="TrGeneral" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/leftoverlap.gif" border="0"></td>
								<td style="CURSOR: hand; COLOR: #003366" onclick="ShowSubTab('TrGeneral')" align="center" bgColor="#cecbce">General</td>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
							</tr>
						</table>
					</td>
					<td>
						<table id="TrSignatory" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/SplitLeft.gif" border="0"></td>
								<td style="CURSOR: hand; COLOR: #003366" onclick="ShowSubTab('TrSignatory')" align="center"	bgColor="#cecbce">Signatory</td>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
							</tr>
						</table>
					</td>
					<td>
						<table id="TrReimbursement" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/SplitLeft.gif" border="0"></td>
								<td style="CURSOR: hand; COLOR: #003366" onclick="ShowSubTab('TrReimbursement')" align="center"	bgColor="#cecbce">Reimbursement</td>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="3" height="5"></td>
				</tr>
				<tr>
				<td align="left">&nbsp;<asp:label id="Label1" text="Code" Runat="server"></asp:label>&nbsp;&nbsp; 
						&nbsp;&nbsp;<asp:textbox id="txtcode" Runat="server" CssClass="textbox"></asp:textbox>
					</td>
					<td align="left">&nbsp;<asp:label id="Label2" text="Classification Code" Runat="server"></asp:label>&nbsp;
						<asp:dropdownlist id="cmbclassification" Width="80px" Runat="server"></asp:dropdownlist>
					</td>
					<td align="left">&nbsp;<asp:label id="Label3" text="Entity" Runat="server"></asp:label>&nbsp;&nbsp;
						<asp:textbox id="txtentity" Runat="server" CssClass="textbox"></asp:textbox>
					</td>
				</tr>
				<tr>
					<td align="left" colSpan="3">&nbsp;<asp:label id="Label4" text="Name" Runat="server"></asp:label>
						&nbsp;&nbsp;&nbsp;<asp:textbox id="txtname" Width="89%" Runat="server" CssClass="textbox"></asp:textbox>
					</td>
				</tr>
				<tr>
					<td align="left" colSpan="3">&nbsp;<asp:label id="Label5" text="Address" Runat="server"></asp:label>
						<asp:textbox id="txtaddress" Width="89%" Runat="server" CssClass="textbox"></asp:textbox>
					</td>
				</tr>
				<tr>
					<td align="left" colSpan="3">&nbsp;<asp:label id="Label6" text="City" Runat="server"></asp:label>&nbsp;&nbsp; 
						&nbsp;&nbsp;&nbsp;&nbsp;<asp:textbox id="txtcity" Width="37%" Runat="server" CssClass="textbox"></asp:textbox>
						&nbsp;<asp:label id="Label7" text="State" Runat="server"></asp:label>
						<asp:dropdownlist id="cmbstate" Width="45%" Runat="server"></asp:dropdownlist>
					</td>
				</tr>
				<tr>
					<td align="left" colSpan="3">&nbsp;<asp:label id="Label8" text="Pin" Runat="server"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:textbox id="txtpin" Width="37%" Runat="server" CssClass="textbox"></asp:textbox>&nbsp;<asp:label id="Label9" text="PF No" Runat="server"></asp:label>
						<asp:textbox id="txtpfno" Width="45%" Runat="server" CssClass="textbox"></asp:textbox>
					</td>
				</tr>
				<tr>
					<td align="left" colSpan="3">&nbsp;<asp:label id="Label10" text="ESI No" Runat="server"></asp:label>&nbsp;&nbsp;
						<asp:textbox id="txtesino" Width="37%" Runat="server" CssClass="textbox"></asp:textbox>&nbsp;<asp:label id="Label11" text="PAN/GIR NO" Runat="server"></asp:label>
						<asp:textbox id="Textbox1" Width="39%" Runat="server" CssClass="textbox"></asp:textbox>
					</td>
				</tr>
				<tr>
					<td align="left" colSpan="3">&nbsp;<asp:label id="Label12" text="TAN No" Runat="server"></asp:label>
						&nbsp;<asp:textbox id="txttanno" Width="37%" Runat="server" CssClass="textbox"></asp:textbox>
						&nbsp;<asp:label id="Label13" text="TDS Circle" Runat="server"></asp:label>&nbsp;&nbsp;
						<asp:textbox id="Textbox2" Width="39%" Runat="server" CssClass="textbox"></asp:textbox>
					</td>
				</tr>
				<tr>
					<td colSpan="3" height="10"></td>
				</tr>
				<tr>
					<td></td>
					<td></td>
					<td width="100%">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0" frame="box">
							<tr>
								<td align="right"><asp:button id="cmdok" Width="75px" Runat="server" Font-Bold="True" Text="OK" Font-Size="10pt"></asp:button>
								</td>
								<td align="center"><asp:button id="cmcancel" runat="server" Width="75px" Font-Bold="True" Text="Cancel" Font-Size="10pt"></asp:button>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				
				
				
				<tr>
					<td colSpan="3" height="10">
					</td>
				</tr>
				<tr>
					<td colSpan="3">
						<table id="TblSignatory" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td width="25%"></td>
								<td width="25%"></td>
								<td width="25%"></td>
								<td width="25%"></td>
							</tr>
							<tr>
								<td>&nbsp;<asp:label id="Label14" text="Signatory" Runat="server"></asp:label>
								</td>
								<td colSpan="3"><asp:textbox id="txtsignatory" Width="96%" Runat="server" CssClass="textbox"></asp:textbox>
								</td>
							</tr>
							<tr>
								<td>&nbsp;<asp:label id="Label15" text="Designation" Runat="server"></asp:label>
								</td>
								<td colSpan="3"><asp:textbox id="txtdesig" Width="96%" Runat="server" CssClass="textbox"></asp:textbox>
								</td>
							</tr>
							<tr>
								<td>&nbsp;<asp:label id="Label16" text="Father's Name" Runat="server"></asp:label>
								</td>
								<td colSpan="3"><asp:textbox id="txtfathersname" Width="96%" Runat="server" CssClass="textbox"></asp:textbox>
								</td>
							</tr>
							<tr>
								<td>&nbsp;<asp:label id="Label17" text="Address" Runat="server"></asp:label>
								</td>
								<td colSpan="3"><asp:textbox id="txtaddres" Width="96%" Runat="server" CssClass="textbox"></asp:textbox>
								</td>
							</tr>
							<tr>
								<td>&nbsp;<asp:label id="Label18" text="City" Runat="server"></asp:label>
								</td>
								<td><asp:textbox id="txtcit" Width="100%" Runat="server" CssClass="textbox"></asp:textbox>
								</td>
								<td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:label id="Label19" text="state" Runat="server"></asp:label>
									&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:dropdownlist id="cmbstat" Width="70%" Runat="server"></asp:dropdownlist>
								</td>
							</tr>
							<tr>
								<td colSpan="2">&nbsp;<asp:label id="Label20" text="Pin" Runat="server"></asp:label>
									&nbsp;
									<asp:textbox id="txtpi" Width="57px" Runat="server" CssClass="textbox"></asp:textbox>&nbsp;&nbsp;&nbsp;<asp:label id="Label21" text="Place" Runat="server"></asp:label>&nbsp; 
									&nbsp;
									<asp:textbox id="txtplace" Width="144px" Runat="server" CssClass="textbox"></asp:textbox>
								</td>
								<td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:label id="Label22" text="Date" Runat="server"></asp:label>
								</td>
								<td>
									<cc1:dtpcombo id="dtpdate" runat="server" ToolTip="Date"></cc1:dtpcombo>
								</td>
							</tr>
							<tr>
								<td colSpan="4" height="10">
								</td>
							</tr>
							<tr>
								<td colSpan="4">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0" frame="box">
										<tr>
											<td align="right"><asp:button id="bttnok" Width="75px" Runat="server" Font-Bold="True" Text="OK" Font-Size="10pt"></asp:button>&nbsp;
												<asp:button id="bttbcancel" runat="server" Width="75px" Font-Bold="True" Text="Cancel" Font-Size="10pt"></asp:button>&nbsp;&nbsp;&nbsp;
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
					</td>
				</tr>
				<tr>
					<td colspan="3">
						<table id="TblReimbursement" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td>&nbsp;<asp:checkbox id="chkusesppc" Runat="server"></asp:checkbox>
									&nbsp;&nbsp;<asp:label id="Label23" text="Use Stored Procedure for Prorata Calculation" Runat="server"></asp:label>
								</td>
							</tr>
							<tr>
								<td>&nbsp;<asp:checkbox id="chkarmte" Runat="server"></asp:checkbox>
									&nbsp;&nbsp;<asp:label id="Label24" text="Allow Reimbursement more than Entitlement" Runat="server"></asp:label>
								</td>
							</tr>
							<tr>
								<td>&nbsp;<asp:checkbox id="chktelr" Runat="server"></asp:checkbox>
									&nbsp;&nbsp;<asp:label id="Label25" text="Take Effect of LWOP on Reimbursement" Runat="server"></asp:label>
								</td>
							</tr>
							<tr>
								<td colSpan="3" height="10">
								</td>
							</tr>
							<tr>
								<td>
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td>&nbsp;<asp:label id="Label26" text="Reimbursement Entitlement Calculated Based On" Runat="server"></asp:label>
											</td>
										</tr>
										<tr>
											<td>
												&nbsp;<asp:RadioButton ID="rbtbmed" Runat="server"></asp:RadioButton>
												&nbsp;<asp:label id="Label27" text="Beginning of Month Entry Date " Runat="server"></asp:label>
											</td>
										</tr>
										<tr>
											<td>
												&nbsp;<asp:RadioButton ID="Rbtted" Runat="server"></asp:RadioButton>
												&nbsp;<asp:label id="Label28" text="Till Entry Date" Runat="server"></asp:label>
											</td>
										</tr>
										<tr>
											<td>
												&nbsp;<asp:RadioButton ID="Rbtemed" Runat="server"></asp:RadioButton>
												&nbsp;<asp:label id="Label29" text="End of Month of Entry date" Runat="server"></asp:label>
											</td>
										</tr>
										<tr>
											<td height="10"></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td width="100%">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0" frame="box">
										<tr>
											<td align="right"><asp:button id="Button1" Width="75px" Runat="server" Font-Bold="True" Text="OK" Font-Size="10pt"></asp:button>&nbsp;&nbsp;
												<asp:button id="Button2" runat="server" Width="75px" Font-Bold="True" Text="Cancel" Font-Size="10pt"></asp:button>&nbsp;&nbsp;&nbsp;
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td height="10">
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
