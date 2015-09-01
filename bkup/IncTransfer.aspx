<%@ Page Language="vb" AutoEventWireup="false" Codebehind="IncTransfer.aspx.vb" Inherits="eHRMS.Net.IncTransfer" %>
<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>IncTransfer</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<script language="vbscript">
			sub ShowHide(args)
					Dim num
					num = args
					document.getElementById("TBJobAllocation").style.backgroundColor = "#cecbce"   
					document.getElementById("TBCompensation").style.backgroundColor = "#cecbce"   
					document.getElementById("TBOthers").style.backgroundColor = "#cecbce"   
					document.getElementById("TBJobAllocation").style.color = "#003366"  
					document.getElementById("TBCompensation").style.color = "#003366" 
					document.getElementById("TBOthers").style.color = "#003366" 
					document.getElementById("TBLJobAllocation").style.display = "none"   
					document.getElementById("TBLCompensation").style.display = "none"   
					document.getElementById("TBLOthers").style.display = "none"   
					
					document.getElementById(replace(args,"TB","TBL")).style.display = "block"   						
					document.getElementById(args).style.color = "White" 
					document.getElementById(args).style.backgroundColor = "#666666"    
			End sub		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<br>
			<TABLE cellSpacing="0" cellPadding="0" align=center   width="750" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Increment 
						Transfer...
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="1" cellPadding="1" rules="none"  align=center  width="750" border="1" frame="border">
				<tr>
					<td colSpan="2"><asp:label id="LblMsg" runat="server" Font-Size="11px" ForeColor="Red" Width="100%"></asp:label></td>
				</tr>
				<tr>
					<td colSpan="2">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td width="50">&nbsp;WEF</td>
								<td width="250"><cc1:dtp id="dtpWEF" runat="server" Width="125px" ToolTip="With Effect Date"></cc1:dtp></td>
								<td width="10">Code</td>
								<td width="100" colSpan="3"><asp:textbox id="TxtCode" Width="80" Runat="server" AutoPostBack="True" CssClass="textbox" ForeColor="#003366"></asp:textbox><asp:dropdownlist id="cmbEmp" runat="server" Width="150" AutoPostBack="True" Visible="False"></asp:dropdownlist><asp:imagebutton id="btnList" Width="18px" Runat="server" Height="19px" ImageUrl="Images\Find.gif"
										ImageAlign="AbsMiddle"></asp:imagebutton></td>
								<td width="200" colSpan="2"><asp:label id="LblName" runat="server" Font-Size="9" ForeColor="#003366" Width="100%" Font-Bold="True"></asp:label></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="2">&nbsp;</td>
				</tr>
				<tr>
					<td colSpan="2">
						<table cellSpacing="0" cellPadding="0" width="100%">
							<tr>
								<td width="25%">
									<table id="TBJobAllocation" cellSpacing="0" cellPadding="0" width="100%" bgColor="#666666"
										border="0" runat="server">
										<tr>
											<td width="10"><IMG alt="" src="Images/SplitLeft.gif" border="0"></td>
											<td style="CURSOR: hand" onclick="ShowHide('TBJobAllocation')" align="center"><b>Job 
													Allocation</b></td>
											<td width="10"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
										</tr>
									</table>
								</td>
								<td width="25%">
									<table id="TBCompensation" cellSpacing="0" cellPadding="0" width="100%" bgColor="#cecbce"
										border="0" runat="server">
										<tr>
											<td width="10"><IMG alt="" src="Images/SplitLeft.gif" border="0"></td>
											<td style="CURSOR: hand" onclick="ShowHide('TBCompensation')" align="center"><b>Compensation</b></td>
											<td width="10"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
										</tr>
									</table>
								</td>
								<td width="25%">
									<table id="TBOthers" cellSpacing="0" cellPadding="0" width="100%" bgColor="#cecbce" border="0"
										runat="server">
										<tr>
											<td width="10"><IMG alt="" src="Images/SplitLeft.gif" border="0"></td>
											<td style="CURSOR: hand" onclick="ShowHide('TBOthers')" align="center"><b>Others</b></td>
											<td width="10"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
										</tr>
									</table>
								</td>
								<td width="25%">&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="2">
						<div style="BORDER-RIGHT: gainsboro thin solid; BORDER-TOP: gainsboro thin solid; OVERFLOW: auto; BORDER-LEFT: gainsboro thin solid; WIDTH: 100%; BORDER-BOTTOM: gainsboro thin solid; HEIGHT: 335px">
							<table id="TBLJobAllocation" cellSpacing="0" cellPadding="0" width="100%">
								<tr>
									<td colSpan="2">&nbsp;</td>
								</tr>
								<tr>
									<td class="Header3" align="center">Present</td>
									<td class="Header3" align="center">Proposed</td>
								</tr>
								<tr>
									<td width="50%">
										<table cellSpacing="0" cellPadding="2" width="100%" border="0">
											<tr>
												<td width="40%"><asp:label id="lblDesignation" runat="server" Width="100%">Designation</asp:label></td>
												<td width="60%"><asp:dropdownlist id="cmbPnDesignation" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td><asp:label id="lblDepartment" runat="server" Width="100%">Department</asp:label></td>
												<td><asp:dropdownlist id="cmbPnDepartment" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td><asp:label id="lblLocation" runat="server" Width="100%">Work Location</asp:label></td>
												<td><asp:dropdownlist id="cmbPnLocation" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td><asp:label id="lblALocation" runat="server" Width="100%">Admin Location</asp:label></td>
												<td><asp:dropdownlist id="cmbPnALocation" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td><asp:label id="lblPLocation" runat="server" Width="100%">Pay Location</asp:label></td>
												<td><asp:dropdownlist id="cmbPnPLocation" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td><asp:label id="lblEmpType" runat="server" Width="100%">Employement Type</asp:label></td>
												<td><asp:dropdownlist id="cmbPnEmpType" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td><asp:label id="lblManager" runat="server" Width="100%">Line/Reporting Manager</asp:label></td>
												<td><asp:dropdownlist id="cmbPnManager" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td width="40%"><asp:label id="lblGrade" runat="server" Width="100%">Grade / Level</asp:label></td>
												<td width="60%"><asp:dropdownlist id="cmbPnGrade" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td><asp:label id="lblCostCenter" runat="server" Width="100%">Cost Center</asp:label></td>
												<td><asp:dropdownlist id="cmbPnCostCenter" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td><asp:label id="lblProcess" runat="server" Width="100%">Process / Responsibility</asp:label></td>
												<td><asp:dropdownlist id="cmbPnProcess" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td><asp:label id="lblRegion" runat="server" Width="100%">Region</asp:label></td>
												<td><asp:dropdownlist id="cmbPnRegion" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td><asp:label id="lblSection" runat="server" Width="100%">Section</asp:label></td>
												<td><asp:dropdownlist id="cmbPnSection" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td><asp:label id="lblDivision" runat="server" Width="100%"> Division</asp:label></td>
												<td><asp:dropdownlist id="cmbPnDivision" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
										</table>
									</td>
									<td width="50%">
										<table cellSpacing="0" cellPadding="2" width="100%" border="0">
											<tr>
												<td width="40%"><asp:label id="Label1" runat="server" Width="100%">&nbsp;&nbsp;Designation</asp:label></td>
												<td width="60%"><asp:dropdownlist id="CmbPdDesignation" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td><asp:label id="Label2" runat="server" Width="100%">&nbsp;&nbsp;Department</asp:label></td>
												<td><asp:dropdownlist id="CmbPdDepartment" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td><asp:label id="Label3" runat="server" Width="100%">&nbsp;&nbsp;Work Location</asp:label></td>
												<td><asp:dropdownlist id="CmbPdLocation" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td><asp:label id="Label4" runat="server" Width="100%">&nbsp;&nbsp;Admin Location</asp:label></td>
												<td><asp:dropdownlist id="CmbPdALocation" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td><asp:label id="Label5" runat="server" Width="100%">&nbsp;&nbsp;Pay Location</asp:label></td>
												<td><asp:dropdownlist id="CmbPdPLocation" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td><asp:label id="Label6" runat="server" Width="100%">&nbsp;&nbsp;Employement Type</asp:label></td>
												<td><asp:dropdownlist id="CmbPdEmpType" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td><asp:label id="Label7" runat="server" Width="100%">&nbsp;&nbsp;Line/Reporting Manager</asp:label></td>
												<td><asp:dropdownlist id="CmbPdManager" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td width="40%"><asp:label id="Label8" runat="server" Width="100%">&nbsp;&nbsp;Grade / Level</asp:label></td>
												<td width="60%"><asp:dropdownlist id="CmbPdGrade" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td><asp:label id="Label9" runat="server" Width="100%">&nbsp;&nbsp;Cost Center</asp:label></td>
												<td><asp:dropdownlist id="CmbPdCostCenter" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td><asp:label id="Label10" runat="server" Width="100%">&nbsp;&nbsp;Process / Responsibility</asp:label></td>
												<td><asp:dropdownlist id="CmbPdProcess" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td><asp:label id="Label11" runat="server" Width="100%">&nbsp;&nbsp;Region</asp:label></td>
												<td><asp:dropdownlist id="CmbPdRegion" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td><asp:label id="Label12" runat="server" Width="100%">&nbsp;&nbsp;Section</asp:label></td>
												<td><asp:dropdownlist id="cmbPdSection" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td><asp:label id="Label13" runat="server" Width="100%">&nbsp;&nbsp;Division</asp:label></td>
												<td><asp:dropdownlist id="CmbPdDivision" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
							<table id="TBLCompensation" style="DISPLAY: none" cellSpacing="0" cellPadding="0" width="100%">
								<tr>
									<td>&nbsp;</td>
								</tr>
								<tr>
									<td><asp:datagrid id="GrdEarnings" runat="server" Width="100%" AllowPaging="False" AutoGenerateColumns="False">
											<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
											<HeaderStyle HorizontalAlign="Center" CssClass="Header3"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="Description">
													<ItemStyle HorizontalAlign="Left" Width="70%"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblDesc_Ern" runat="server">
															<%# DataBinder.Eval(Container.DataItem, "FIELD_DESC") %>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Present">
													<ItemStyle HorizontalAlign="Right" Width="15%"></ItemStyle>
													<ItemTemplate>
														<asp:TextBox id=txtAmount_Ern onblur=CheckNum(this.id) style="TEXT-ALIGN: right" runat="server" Width="100%" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Amount") %>' MaxLength='<%# DataBinder.Eval(Container.DataItem, "Field_Len") %>'>
														</asp:TextBox>
														<asp:Label id="LblAmount_Ern" runat="server" Visible="False">
															<%# DataBinder.Eval(Container.DataItem, "Amount") %>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle Width="0px"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Width="0px"></ItemStyle>
													<ItemTemplate>
														<asp:TextBox id=txtField_Name_Ern runat="server" Width="0Px" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "FIELD_NAME") %>'>
														</asp:TextBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle Width="0px"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Width="0px"></ItemStyle>
													<ItemTemplate>
														<asp:TextBox Width="0Px" runat="server" ID="txtDataType_Ern" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Field_Type") %>' />
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Proposed">
													<ItemStyle HorizontalAlign="Right" Width="15%"></ItemStyle>
													<ItemTemplate>
														<asp:TextBox Width="100%" runat="server" style ="TEXT-ALIGN: right" ID="TxtPdAmount" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Amount") %>' MaxLength='<%# DataBinder.Eval(Container.DataItem, "Field_Len") %>' onBlur="CheckNum(this.id)" />
														<asp:Label id="Label14" runat="server" Visible="False">
															<%# DataBinder.Eval(Container.DataItem, "Amount") %>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle NextPageText="&amp;minus;&amp;gt;" Font-Underline="True" Font-Names="Verdana" PrevPageText="&amp;lt;&amp;minus;"
												ForeColor="#CB3908" BackColor="#E0E0E0" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></td>
								</tr>
							</table>
							<table id="TBLOthers" style="DISPLAY: none" cellSpacing="0" cellPadding="0" width="100%">
								<tr>
									<td colSpan="2">&nbsp;</td>
								</tr>
								<tr>
									<td class="Header3" align="center">Present</td>
									<td class="Header3" align="center">Proposed</td>
								</tr>
								<tr>
									<td width="50%">
										<table cellSpacing="0" cellPadding="0" width="100%">
											<tr>
												<td width="20%">Job Profile</td>
												<td width="80%"><asp:textbox id="TxtPnJobProfile" Width="100%" Runat="server" Rows="3" TextMode="MultiLine"></asp:textbox></td>
											</tr>
											<tr>
												<td>Transfer Type</td>
												<td><asp:dropdownlist id="CmbTransferType" Width="130" Runat="server">
														<asp:ListItem Value="" Selected="True">&nbsp;</asp:ListItem>
														<asp:ListItem Value="1">Permanent</asp:ListItem>
														<asp:ListItem Value="2">Temperary</asp:ListItem>
													</asp:dropdownlist></td>
											</tr>
											<tr>
												<td>Reason Type</td>
												<td><asp:dropdownlist id="CmbReason" Width="130" Runat="server">
														<asp:ListItem Value="" Selected="True">&nbsp;</asp:ListItem>
														<asp:ListItem Value="1">Work Load</asp:ListItem>
														<asp:ListItem Value="2">Transfer</asp:ListItem>
														<asp:ListItem Value="3">Promotion</asp:ListItem>
														<asp:ListItem Value="4">Resignation</asp:ListItem>
														<asp:ListItem Value="5">Termination</asp:ListItem>
													</asp:dropdownlist></td>
											</tr>
											<tr>
												<td vAlign="top">Reason</td>
												<td><asp:textbox id="TxtReason" Width="100%" Runat="server" Rows="3" TextMode="MultiLine"></asp:textbox></td>
											</tr>
										</table>
									</td>
									<td vAlign="top" width="50%">
										<table cellSpacing="0" cellPadding="0" width="100%">
											<tr>
												<td width="20%">Job Profile</td>
												<td width="80%"><asp:textbox id="TxtPdJobProfile" Width="100%" Runat="server" Rows="3" TextMode="MultiLine"></asp:textbox></td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
					<TR height="15">
					<TD style="MARGIN-LEFT: 15px; MARGIN-RIGHT: 15px; BORDER-BOTTOM: #993300 thin groove; HEIGHT: 7px"
						colSpan="2"></TD>
				</TR>
				<tr>
					<td align="left">Month&nbsp;&nbsp;<asp:dropdownlist id="CmbMonth" Width="150px" Runat="server"></asp:dropdownlist></td>
					<td align="right"><asp:button id="cmdSave" runat="server" Width="75px" Text="Save"></asp:button>&nbsp;&nbsp;<asp:button id="CmdDelete" runat="server" Width="75px" Text="Delete"></asp:button>&nbsp;&nbsp;<asp:button id="CmdCancel" runat="server" Width="75px" Text="Cancel"></asp:button>&nbsp;&nbsp;
						<asp:button id="cmdClose" accessKey="C" runat="server" Width="75px" Text="Close"></asp:button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
