<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="OffInfo.aspx.vb" Inherits="eHRMS.Net.OffInfo"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>OffInfo</title>
		<meta content="False" name="vs_snapToGrid">
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
		<script language="vbscript">
			Sub Val(argid)
				IF document.getElementById(argid).Checked = False THEN
					document.getElementById(replace(argid,"Chk","Dtp")).disabled = true
					document.getElementById(replace(argid,"Chk","Dtp") & "cmbDD").disabled = true
					document.getElementById(replace(argid,"Chk","Dtp") & "cmbMM").disabled = true
					document.getElementById(replace(argid,"Chk","Dtp") & "cmbYY").disabled = true
				ELSE
					document.getElementById(replace(argid,"Chk","Dtp")).disabled = false
					document.getElementById(replace(argid,"Chk","Dtp") & "cmbDD").disabled = false
					document.getElementById(replace(argid,"Chk","Dtp") & "cmbMM").disabled = false
					document.getElementById(replace(argid,"Chk","Dtp") & "cmbYY").disabled = false
				End If
			END SUB
			
			function ValidateCtrl()
			
				if Trim(document.getElementById("txtFName").Value) = "" Then
					msgbox("Please Enter First Name of Employee.")
					ValidateCtrl = false
					exit function
				end if
					
				if isnumeric(document.getElementById("txtFName").Value) Then
					msgbox("First Name must be string type.")
					ValidateCtrl = false
					exit function
				end if

				if IsNumeric(document.getElementById("txtLName").Value) Then
					msgbox("Last Name must be string type.")
					ValidateCtrl = false
					exit function
				end if

				
				dim i,j,k
				dim dtDOCE, dtDOJ, dtDOCDUE, DtDDEPP, DtCEUPTO, DtDDR
				
				dtDOJ = cdate(document.getElementById("dtpDOJcmbDD").Value & "/" & document.getElementById("dtpDOJcmbMM").Value & "/" & document.getElementById("dtpDOJcmbYY").Value)
				dtDOCE = cdate(document.getElementById("dtpDOCEcmbDD").Value & "/" & document.getElementById("dtpDOCEcmbMM").Value & "/" & document.getElementById("dtpDOCEcmbYY").Value)
				dtDOCDUE = cdate(document.getElementById("dtpDOCDUEcmbDD").Value & "/" & document.getElementById("dtpDOCDUEcmbMM").Value & "/" & document.getElementById("dtpDOCDUEcmbYY").Value)
				DtDDEPP = cdate(document.getElementById("DtpDDEPPcmbDD").Value & "/" & document.getElementById("DtpDDEPPcmbMM").Value & "/" & document.getElementById("DtpDDEPPcmbYY").Value)
				DtCEUPTO = cdate(document.getElementById("DtpCEUPTOcmbDD").Value & "/" & document.getElementById("DtpCEUPTOcmbMM").Value & "/" & document.getElementById("DtpCEUPTOcmbYY").Value)
				DtDDR = cdate(document.getElementById("DtpDDRcmbDD").Value & "/" & document.getElementById("DtpDDRcmbMM").Value & "/" & document.getElementById("DtpDDRcmbYY").Value)
				
				if Not IsDate(dtDOJ) Then
					msgbox("Invalid Date of Joining.")
					ValidateCtrl = false
					exit function
				end if
								
				if document.getElementById("ChkDOCE").Checked = True Then
					i = datediff("d",dtDOCE,dtDOJ)
					if i > 0 Then
						msgbox("Contract End Date Can't Before the Date of Joining.")
						ValidateCtrl = false
						exit function
					end if
				end if
				if document.getElementById("ChkDOCDue").Checked = True Then
					j = datediff("d",dtDOCDUE,dtDOJ)
					if j > 0 Then
						msgbox("Due Date of Confirmation Can't Before the Date of Joining.")
						ValidateCtrl = false
						exit function
					end if
				end if
				if document.getElementById("ChkDDEPP").Checked = True Then
					k = datediff("d",dtDDEPP,dtDOJ)
					if k > 0 Then
						msgbox("Probation Period Extended Upto Can't Before the Date of Joining.")
						ValidateCtrl = false
						exit function
					end if
				end if
				if document.getElementById("ChkCEUPTO").Checked = True Then
					k = datediff("d",dtCEUPTO,dtDOJ)
					if k > 0 Then
						msgbox("Contract Extended Upto Can't Before the Date of Joining.")
						ValidateCtrl = false
						exit function
					end if
				end if
				if document.getElementById("ChkDDR").Checked = True Then
					k = datediff("d",dtDDR,dtDOJ)
					if k > 0 Then
						msgbox("Date of Regularisation Can't Before the Date of Joining.")
						ValidateCtrl = false
						exit function
					end if
				end if	
				ValidateCtrl = true
			end function
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="5" rightMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx --><br>
			<br>
			<table height="30" cellSpacing="0" align="center" cellPadding="0" width="790" border="0">
				<tr vAlign="bottom">
					<td width="110">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td width="10" bgColor="#666666"><IMG alt="" src="Images/SplitLeft.gif" border="0"></td>
								<td style="COLOR: white" align="center" bgColor="#666666"><b>Official Info</b></td>
								<td width="10" bgColor="#666666"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
							</tr>
						</table>
					</td>
					<td width="120">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/leftoverlap.gif" border="0"></td>
								<td style="COLOR: #003366" align="center" bgColor="#cecbce"><A href="GeneralInfo.aspx?SrNo=63">General 
										Info</A></td>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
							</tr>
						</table>
					</td>
					<td width="100">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/leftoverlap.gif" border="0"></td>
								<td style="COLOR: #003366" align="center" bgColor="#cecbce"><A href="Compensation.aspx?SrNo=64">Compensation</A></td>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
							</tr>
						</table>
					</td>
					<td width="100">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/leftoverlap.gif" border="0"></td>
								<td style="COLOR: #003366" align="center" bgColor="#cecbce"><A href="History.aspx?SrNo=65">History</A></td>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
							</tr>
						</table>
					</td>
					<td width="100">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/leftoverlap.gif" border="0"></td>
								<td style="COLOR: #003366" align="center" bgColor="#cecbce"><A href="Progression.aspx?SrNo=66">Progression</A></td>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
							</tr>
						</table>
					</td>
					<td width="100">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/leftoverlap.gif" border="0"></td>
								<td style="COLOR: #003366" align="center" bgColor="#cecbce"><A href="Others.aspx?SrNo=67">Others</A></td>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
							</tr>
						</table>
					</td>
					<td width="100">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/leftoverlap.gif" border="0"></td>
								<td style="COLOR: #003366" align="center" bgColor="#cecbce"><A href="Family.aspx?SrNo=68">Family</A></td>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
							</tr>
						</table>
					</td>
					<td width="100">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/leftoverlap.gif" border="0"></td>
								<td style="COLOR: #003366" align="center" bgColor="#cecbce"><A href="EmpSkills.aspx?SrNo=69">Skills</A></td>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
							</tr>
						</table>
					</td>
					<td width="100">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/leftoverlap.gif" border="0"></td>
								<td style="COLOR: #003366" align="center" bgColor="#cecbce"><A href="EmpNominees.aspx?SrNo=70">Nominee</A></td>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="9">
						<table borderColor="black" cellSpacing="0" cellPadding="0" rules="none" width="100%" border="1"
							frame="box">
							<tr>
								<td colSpan="2"><asp:label id="LblMsg" runat="server" Width="100%" ForeColor="Red" Font-Size="11px"></asp:label></td>
							</tr>
							<tr>
								<td width="50%">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td width="40%"><asp:label id="lblCode" runat="server" Width="100%">&nbsp;Code</asp:label></td>
											<td width="60%"><asp:textbox id="txtEM_CD" runat="server" Width="100%" ForeColor="#003366" Font-Bold="True" BackColor="#E1E4EB"
													CssClass="TextBox" AutoPostBack="True"></asp:textbox></td>
										</tr>
									</table>
								</td>
								<td width="50%"></td>
							</tr>
							<tr>
								<td width="50%">
									<table cellSpacing="0" cellPadding="2" width="100%" border="0">
										<TBODY>
											<tr>
												<td width="40%"><asp:label id="lblFName" runat="server" Width="100%">First Name</asp:label></td>
												<td width="60%"><asp:textbox id="txtFName" tabIndex="1" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"></asp:textbox></td>
											</tr>
											<tr>
												<td width="40%"><asp:label id="lblDesignation" runat="server" Width="100%">Designation</asp:label></td>
												<td width="60%"><asp:dropdownlist id="cmbDesignation" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td><asp:label id="lblDepartment" runat="server" Width="100%">Department</asp:label></td>
												<td><asp:dropdownlist id="cmbDepartment" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td><asp:label id="lblLocation" runat="server" Width="100%">Work Location</asp:label></td>
												<td><asp:dropdownlist id="cmbLocation" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td style="HEIGHT: 7px"><asp:label id="lblALocation" runat="server" Width="100%">Admin Location</asp:label></td>
												<td style="HEIGHT: 7px"><asp:dropdownlist id="cmbALocation" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td style="HEIGHT: 17px"><asp:label id="lblPLocation" runat="server" Width="100%">Pay Location</asp:label></td>
												<td style="HEIGHT: 17px"><asp:dropdownlist id="cmbPLocation" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td><asp:label id="lblEmpType" runat="server" Width="100%">Employment Type</asp:label></td>
												<td><asp:dropdownlist id="cmbEmpType" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td><asp:label id="lblManager" runat="server" Width="100%">Reporting Manager</asp:label></td>
												<td><asp:dropdownlist id="cmbManager" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td><asp:label id="LblDOC" runat="server" Width="100%" ToolTip=" Date of Confirmation">Confirmed (Yes/No)</asp:label></td>
												<td><input id="ChkDOC" type="checkbox" name="ChkDOC" runat="server"><cc1:dtpcombo id="DtpDOC" runat="server" Width="150px" ToolTip="Date of Confirmation" DateValue="2005-08-30"
														Enabled="False" visible="FAlse"></cc1:dtpcombo></td>
											</tr>
											<tr>
												<td><asp:label id="lblDOCDue" runat="server" Width="100%" ToolTip=" Due Date of Confirmation">Due Date of Confirmation</asp:label></td>
												<td><input id="ChkDOCDue" onclick="Val(this.id)" type="checkbox" name="ChkDOCDue" runat="server"><cc1:dtpcombo id="dtpDOCDUE" runat="server" Width="150px" ToolTip="Due Date of Confirmation" DateValue="2005-08-30"
														Enabled="False"></cc1:dtpcombo></td>
											</tr>
											<tr>
												<td><asp:label id="Label2" runat="server" Width="100%">Probation Period Extended Upto</asp:label></td>
												<td><input id="ChkDDEPP" onclick="Val(this.id)" type="checkbox" name="ChkDDEPP" runat="server"><cc1:dtpcombo id="DtpDDEPP" runat="server" Width="150px" ToolTip="Due Date of Extension of Probation Period"
														DateValue="2005-08-30" Enabled="False"></cc1:dtpcombo></td>
											</tr>
											<!--<tr>
												<td><asp:label id="Label4" runat="server" Width="100%" Visible="False">Due Date of Extension of Definite Period Employment</asp:label></td>
												<td><input id="ChkDDEDPE" onclick="Val(this.id)" type="checkbox" name="ChkDDEDPE"  runat="server">
												<cc1:dtpcombo id="DtpDDEDPE" runat="server" Width="150px"  visible="False" ToolTip="Due Date of Extension of Definite Period Employment"
														Enabled="False" DateValue="2005-08-30"></cc1:dtpcombo></td>
											</tr>--></TBODY></table>
								</td>
								<td width="50%">
									<table cellSpacing="0" cellPadding="2" width="100%" border="0">
										<tr>
											<td width="40%"><asp:label id="lblLName" runat="server" Width="100%">Last Name</asp:label></td>
											<td width="60%"><asp:textbox id="txtLName" tabIndex="2" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"></asp:textbox></td>
										</tr>
										<tr>
											<td width="40%"><asp:label id="lblGrade" runat="server" Width="100%">Grade / Level</asp:label></td>
											<td width="60%"><asp:dropdownlist id="cmbGrade" runat="server" Width="100%"></asp:dropdownlist></td>
										</tr>
										<tr>
											<td><asp:label id="lblCostCenter" runat="server" Width="100%">Cost Center</asp:label></td>
											<td><asp:dropdownlist id="cmbCostCenter" runat="server" Width="100%"></asp:dropdownlist></td>
										</tr>
										<tr>
											<td><asp:label id="lblProcess" runat="server" Width="100%"> Sub-Department</asp:label></td>
											<td><asp:dropdownlist id="cmbProcess" runat="server" Width="100%"></asp:dropdownlist></td>
										</tr>
										<tr>
											<td><asp:label id="lblRegion" runat="server" Width="100%">Region</asp:label></td>
											<td><asp:dropdownlist id="cmbRegion" runat="server" Width="100%"></asp:dropdownlist></td>
										</tr>
										<tr>
											<td><asp:label id="lblSection" runat="server" Width="100%">Section</asp:label></td>
											<td><asp:dropdownlist id="cmbSection" runat="server" Width="100%"></asp:dropdownlist></td>
										</tr>
										<tr>
											<td><asp:label id="lblDivision" runat="server" Width="100%"> Division</asp:label></td>
											<td><asp:dropdownlist id="cmbDivision" runat="server" Width="100%"></asp:dropdownlist></td>
										</tr>
										<tr>
											<td><asp:label id="lblDOJ" runat="server" Width="100%">Date of Joining</asp:label></td>
											<td><cc1:dtpcombo id="dtpDOJ" runat="server" Width="150px" ToolTip="Date Of Joining" DateValue="2005-08-30"></cc1:dtpcombo></td>
										</tr>
										<tr>
											<td><asp:label id="Label1" runat="server" Width="100%">Contract End Date</asp:label></td>
											<td><input id="ChkDOCE" onclick="Val(this.id)" type="checkbox" name="ChkDOCE" runat="server"><cc1:dtpcombo id="dtpDOCE" runat="server" Width="150px" ToolTip="Contract End Date" DateValue="2005-08-30"
													Enabled="False"></cc1:dtpcombo></td>
										</tr>
										<tr>
											<td><asp:label id="LblCEUPTO" runat="server" Width="100%">Contract Extended Upto</asp:label></td>
											<td><input id="ChkCEUPTO" onclick="Val(this.id)" type="checkbox" name="ChkCEUPTO" runat="server"><cc1:dtpcombo id="DtpCEUPTO" runat="server" Width="150px" ToolTip="Contract Extended Upto" DateValue="2005-08-30"
													Enabled="False"></cc1:dtpcombo></td>
										</tr>
										<tr>
											<td><asp:label id="Label3" runat="server" Width="100%">Date of Regularisation</asp:label></td>
											<td><input id="ChkDDR" onclick="Val(this.id)" type="checkbox" name="ChkDDR" runat="server"><cc1:dtpcombo id="DtpDDR" runat="server" Width="150px" ToolTip="Contract Extended Upto" DateValue="2005-08-30"
													Enabled="False"></cc1:dtpcombo></td>
										</tr>
										<!--<tr>
											<td><asp:label id="Label5" runat="server" Width="100%" Visible =False >Due Date of Extension of Training Period</asp:label></td>
											<td><input id="ChkDDETP" onclick="Val(this.id)" type="checkbox" name="ChkDDETP" runat="server"><cc1:dtpcombo id="DtpDDETP" runat="server" Width="150px" ToolTip="Due Date of Extension of Training Period"
													Enabled="False"  visible="False" DateValue="2005-08-30"></cc1:dtpcombo></td>
										</tr>--></table>
								</td>
							</tr>
							<TR height="15">
								<TD style="MARGIN-LEFT: 15px; MARGIN-RIGHT: 15px; BORDER-BOTTOM: #993300 thin groove; HEIGHT: 7px"
									colSpan="2"></TD>
							</TR>
							<tr>
								<td align="right" colSpan="2"><asp:button id="cmdSave" accessKey="S" runat="server" Width="75px" Text="Save"></asp:button>&nbsp;
									<asp:button id="cmdClose" accessKey="C" runat="server" Width="75px" Text="Close"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td align="center" colSpan="9"><asp:label id="LblRights" Width="100%" Font-Size="10" Font-Bold="True" Runat="server"></asp:label></td>
				</tr>
			</table>
			</TR></TABLE>
		</form>
	</body>
</HTML>
