<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPFChallanNew.aspx.vb" Inherits="eHRMS.Net.FrmPFChallanNew"%>
<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmPFChallanNew</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
		<script language="javascript">
		function ConfirmDelete()
		{
			if(confirm("Are You Sure To Delete This Record?"+"...[HRMS]")==true)
				return true;
			else
				return false;
		}
		</script>
		<script language="VBscript">
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
			function ValidateCtrl()
			        
				If trim(document.getElementById("Txtrefno").Value) = "" Then
					msgbox ("Enter the Challan No.")
					ValidateCtrl = false
					exit function
				End If
				
				If trim(document.getElementById("cmbMonth").Value) = "" Then
					msgbox ("Please select a month")
					ValidateCtrl = false
					exit function
				End If
				
				If trim(document.getElementById("CmbLocation").Value) = "" Then
					msgbox ("Please Select a location")
					ValidateCtrl = false
					exit function
				End If
				

				ValidateCtrl = true
			end function
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx --><br>
			<TABLE cellSpacing="0" cellPadding="0" width="680" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="15">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">PF 
						Challan Entry
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" height="19" src="Images/TableRight.gif" width="27"></TD>
				</tr>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" rules="none" width="680" align="center" border="1"
				frame="box">
				<tr>
					<td width="160"></td>
					<td width="160"></td>
					<td width="130"></td>
					<td width="130"></td>
					<td width="100"></td>
				</tr>
				<tr>
					<td colSpan="5"><asp:label id="LblError" runat="server" Width="100%" ForeColor="Red"></asp:label></td>
				</tr>
				<tr>
					<td colSpan="4">
						<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0" frame="box">
							<tr>
								<td width="180"></td>
								<td width="160"></td>
								<td width="120"></td>
								<td width="120"></td>
							</tr>
							<tr>
								<td colSpan="4">
									<table style="BORDER-RIGHT: #cccccc 1px solid; BORDER-TOP: #cccccc 1px solid; LEFT: 1px; BORDER-LEFT: #cccccc 1px solid; WIDTH: 570px; BORDER-BOTTOM: #cccccc 1px solid"
										cellSpacing="0" cellPadding="0" width="100%" align="center" border="0" frame="box">
										<tr>
											<td width="180"></td>
											<td width="160"></td>
											<td width="120"></td>
											<td width="120"></td>
										</tr>
										<tr height="15">
											<td colSpan="2"><asp:label id="LblChallanNo" runat="server">&nbsp;&nbsp;Challan No</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
												<asp:label id="LblChallanDate" runat="server">Challan Date</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
												<asp:label id="LblChallanType" runat="server">Challan Type</asp:label></td>
											<td><asp:label id="LblMonth" runat="server">&nbsp;Month</asp:label></td>
											<td><asp:label id="LblLocation" runat="server">&nbsp;Location:</asp:label></td>
										</tr>
										<tr>
											<td colSpan="2">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0">
													<tr>
														<td><asp:textbox id="Txtrefno" onblur="CheckNum(this.id)" Width="75px" ForeColor="#003366" Runat="server"
																CssClass="textbox" AutoPostBack="True"></asp:textbox><asp:dropdownlist id="cmbChallanNo" runat="server" Width="75px" ForeColor="#003366" AutoPostBack="True"
																Visible="False"></asp:dropdownlist><asp:imagebutton id="btnList" Width="18px" Runat="server" ToolTip="Click Here For Search Exists Record!"
																ImageUrl="Images\Find.gif" ImageAlign="AbsMiddle"></asp:imagebutton><asp:imagebutton id="btnNew" Width="18px" Runat="server" ToolTip="Click Here For Adding New Record!"
																ImageUrl="Images\NewFile.ico" ImageAlign="AbsMiddle" Height="18px"></asp:imagebutton></td>
														<td><cc1:dtp id="DTPChallanDate" runat="server" Width="100px" ForeColor="#003366"></cc1:dtp></td>
														<td><asp:dropdownlist id="cmbChallanType" runat="server" Width="75px" ForeColor="#003366">
																<asp:ListItem Value="Payroll" Selected="True">Payroll</asp:ListItem>
															</asp:dropdownlist></td>
													</tr>
												</table>
											</td>
											<td align="center"><asp:dropdownlist id="cmbMonth" runat="server" Width="120px" ForeColor="#003366" AutoPostBack="True"></asp:dropdownlist>&nbsp;</td>
											<TD align="center"><asp:dropdownlist id="CmbLocation" runat="server" Width="120px" ForeColor="#003366"></asp:dropdownlist>&nbsp;&nbsp;</TD>
										</tr>
										<tr height="10">
											<td colSpan="4"></td>
										</tr>
									</table>
								</td>
							</tr>
							<TR height="10">
								<td colSpan="4"></td>
							</TR>
							<tr>
								<td colSpan="4">
									<table style="BORDER-RIGHT: #cccccc 1px solid; BORDER-TOP: #cccccc 1px solid; LEFT: 1px; BORDER-LEFT: #cccccc 1px solid; WIDTH: 570px; BORDER-BOTTOM: #cccccc 1px solid"
										cellSpacing="0" cellPadding="0" width="100%" align="center" border="0" frame="box">
										<tr height="20">
											<td width="145">&nbsp;<asp:label id="AC" runat="server">A/c</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
												<asp:label id="LblWages" runat="server">Wages</asp:label></td>
											<td align="center" width="145"><asp:label id="LblEmpShare" runat="server">Employee Share</asp:label></td>
											<td align="center" width="145"><asp:label id="LblEmperShare" runat="server">Employer Share</asp:label></td>
											<td align="center" width="145"><asp:label id="LblAdmin" runat="server">Admin Charges</asp:label></td>
										</tr>
										<tr>
											<TD>&nbsp;<asp:label id="LblEPF" runat="server">EPF</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
												<asp:textbox id="TxtWages1" onblur="CheckNum(this.id)" runat="server" Width="75" ForeColor="#003366"
													CssClass="Textbox"></asp:textbox></TD>
											<TD>&nbsp;<asp:textbox id="TxtES1" onblur="CheckNum(this.id)" runat="server" Width="44px" ForeColor="#003366"
													CssClass="Textbox"></asp:textbox>%
												<asp:textbox id="TxtES3" runat="server" Width="75px" ForeColor="#003366" CssClass="Textbox"></asp:textbox></TD>
											<TD>&nbsp;<asp:textbox id="TxtER1" onblur="CheckNum(this.id)" runat="server" Width="44px" ForeColor="#003366"
													CssClass="Textbox"></asp:textbox>%
												<asp:textbox id="TxtER4" onblur="CheckNum(this.id)" runat="server" Width="75px" ForeColor="#003366"
													CssClass="Textbox"></asp:textbox></TD>
											<TD>&nbsp;<asp:textbox id="TxtAC1" onblur="CheckNum(this.id)" runat="server" Width="44px" ForeColor="#003366"
													CssClass="Textbox"></asp:textbox>%
												<asp:textbox id="TxtAC3" onblur="CheckNum(this.id)" runat="server" Width="75px" ForeColor="#003366"
													CssClass="Textbox"></asp:textbox></TD>
										</tr>
										<TR>
											<TD>&nbsp;<asp:label id="LblEPS" runat="server">EPS</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
												<asp:textbox id="TxtWages2" onblur="CheckNum(this.id)" runat="server" Width="75" CssClass="Textbox"></asp:textbox></TD>
											<TD>&nbsp;<asp:textbox id="TxtES2" onblur="CheckNum(this.id)" runat="server" Width="44px" ForeColor="#003366"
													CssClass="Textbox"></asp:textbox>%
												<asp:textbox id="TxtES4" onblur="CheckNum(this.id)" runat="server" Width="75px" ForeColor="#003366"
													CssClass="Textbox"></asp:textbox></TD>
											<TD>&nbsp;<asp:textbox id="TxtER2" onblur="CheckNum(this.id)" runat="server" Width="44px" ForeColor="#003366"
													CssClass="Textbox"></asp:textbox>%
												<asp:textbox id="TxtER5" onblur="CheckNum(this.id)" runat="server" Width="75px" ForeColor="#003366"
													CssClass="Textbox"></asp:textbox></TD>
											<td></td>
										</TR>
										<TR>
											<TD>&nbsp;<asp:label id="LblDLI" runat="server">DLI</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
												<asp:textbox id="TxtWages3" onblur="CheckNum(this.id)" runat="server" Width="75px" ForeColor="#003366"
													CssClass="Textbox"></asp:textbox></TD>
											<TD></TD>
											<TD>&nbsp;<asp:textbox id="TxtER3" onblur="CheckNum(this.id)" runat="server" Width="44px" ForeColor="#003366"
													CssClass="Textbox"></asp:textbox>%
												<asp:textbox id="TxtER6" onblur="CheckNum(this.id)" runat="server" Width="75px" ForeColor="#003366"
													CssClass="Textbox"></asp:textbox></TD>
											<TD>&nbsp;<asp:textbox id="TxtAC2" onblur="CheckNum(this.id)" runat="server" Width="44px" ForeColor="#003366"
													CssClass="Textbox"></asp:textbox>%
												<asp:textbox id="TxtAC4" onblur="CheckNum(this.id)" runat="server" Width="75px" ForeColor="#003366"
													CssClass="Textbox"></asp:textbox></TD>
										</TR>
									</table>
								</td>
							</tr>
						</table>
					</td>
					<td>
						<table style="BORDER-RIGHT: #cccccc 1px solid; BORDER-TOP: #cccccc 1px solid; BORDER-LEFT: #cccccc 1px solid; MARGIN-RIGHT: 2px; BORDER-BOTTOM: #cccccc 1px solid"
							cellSpacing="11" cellPadding="0" width="100%" align="center" border="0">
							<tr>
								<td align="center" width="100%"><asp:button id="CmdSave" Width="75" Runat="server" Text="Save"></asp:button></td>
							</tr>
							<tr>
								<td align="center" width="100%"><asp:button id="CmdDelete" Width="75" Runat="server" Text="Delete"></asp:button></td>
							</tr>
							<tr>
								<td align="center" width="100%"><asp:button id="CmdClose" Width="75" Runat="server" Text="Close"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="10">
					<td colSpan="5"></td>
				</tr>
				<tr width="100%">
					<td colSpan="5">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0" frame="box">
							<tr>
								<td>&nbsp;<asp:label id="LblRemitance" runat="server">Remitance Date</asp:label></td>
								<td>&nbsp;<input id="ChkRemDate" onclick="Val(this.id)" type="checkbox" name="ChkRemDate" runat="server">
									<cc1:dtpcombo id="DTPRemitance" runat="server" ForeColor="#003366" DateValue="2006-06-30" enable="false"></cc1:dtpcombo></td>
								<td>&nbsp;<asp:label id="LblJoiness" runat="server">Joinee's</asp:label></td>
								<td><asp:textbox id="TxtJoinee" onblur="CheckNum(this.id)" runat="server" Width="50" ForeColor="#003366"
										CssClass="Textbox"></asp:textbox></td>
								<td>&nbsp;<asp:label id="LblRegi" runat="server">Resignee's</asp:label></td>
								<td><asp:textbox id="TxtResignee" onblur="CheckNum(this.id)" runat="server" Width="50" ForeColor="#003366"
										CssClass="Textbox"></asp:textbox></td>
								<td>&nbsp;<asp:label id="LblHead" runat="server">Net Head Count</asp:label></td>
								<td><asp:textbox id="TxtNetHead" onblur="CheckNum(this.id)" runat="server" Width="50" ForeColor="#003366"
										CssClass="Textbox"></asp:textbox></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="5"><br>
						<table cellSpacing="0" cellPadding="0" width="100%" border="0" frame="box">
							<tr>
								<td align="left" width="15%">&nbsp;<asp:label id="LblBankName" runat="server">Bank Name</asp:label></td>
								<td align="left" width="25%"><asp:dropdownlist id="cmbBankName" runat="server" Width="140px" ForeColor="#003366"></asp:dropdownlist></td>
								<td width="15%">&nbsp;<asp:label id="LblChequeNo" runat="server">Cheque No</asp:label></td>
								<td align="left" width="15%"><asp:textbox id="TxtChequeNo" onblur="CheckNum(this.id)" runat="server" Width="120px" ForeColor="#003366"
										CssClass="Textbox" MaxLength="6"></asp:textbox></td>
								<td width="15%">&nbsp;<asp:label id="LblChequeDate" runat="server">Cheque Date</asp:label></td>
								<td align="left" width="15%"><cc1:dtp id="DTPChequeDate" runat="server" Width="100px"></cc1:dtp></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="10">
					<td colSpan="5"></td>
				</tr>
				<TR>
					<TD vAlign="top" colSpan="5">
						<TABLE id="Table11" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
							<TR>
								<TD borderColor="#000000" align="center" bgColor="gray"><asp:label id="LblEmpDetails" runat="server" ForeColor="White" Visible="False" Font-Size="11"
										Font-Bold="True">Employee's Details</asp:label></TD>
							</TR>
							<TR>
								<TD height="3"></TD>
							</TR>
							<TR>
								<TD>&nbsp;&nbsp;&nbsp;<asp:label id="NoEmp" runat="server" ForeColor="Gray" Visible="False" Font-Bold="True"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:label id="TotalPage" runat="server" ForeColor="Gray" Visible="False" Font-Bold="True"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:label id="CurrentPage" runat="server" ForeColor="Gray" Visible="False" Font-Bold="True"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:label id="MonthOf" runat="server" ForeColor="Gray" Visible="False" Font-Bold="True"></asp:label></TD>
							</TR>
							<TR>
								<TD height="3"></TD>
							</TR>
							<TR>
								<TD><asp:datagrid id="GrdRecords" runat="server" Width="680px" AllowPaging="True" AutoGenerateColumns="False">
										<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
										<HeaderStyle HorizontalAlign="Center" CssClass="Header3"></HeaderStyle>
										<Columns>
											<asp:BoundColumn DataField="Emp_Code" HeaderStyle-Font-Bold="True" HeaderText="Code"></asp:BoundColumn>
											<asp:BoundColumn DataField="Emp_Name" HeaderStyle-Font-Bold="True" HeaderText="Name"></asp:BoundColumn>
											<asp:BoundColumn DataField="PFSALARY" HeaderStyle-Font-Bold="True" HeaderText="Base For EPF"></asp:BoundColumn>
											<asp:BoundColumn DataField="FPFSALARY" HeaderText="Base of EPS" HeaderStyle-Font-Bold="True"></asp:BoundColumn>
											<asp:BoundColumn DataField="FPF" HeaderStyle-Font-Bold="True" HeaderText="EPF [Employee]"></asp:BoundColumn>
											<asp:BoundColumn DataField="PF" HeaderStyle-Font-Bold="True" HeaderText="EPS [Employee]"></asp:BoundColumn>
											<asp:BoundColumn DataField="EPF" HeaderStyle-Font-Bold="True" HeaderText="EPF [Employer]"></asp:BoundColumn>
											<asp:BoundColumn DataField="EFPF" HeaderStyle-Font-Bold="True" HeaderText="EPS [Employer]"></asp:BoundColumn>
										</Columns>
										<PagerStyle NextPageText="" Font-Size="X-Small" Font-Bold="True" PrevPageText="" HorizontalAlign="Center"
											ForeColor="#330066" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></TD>
							</TR>
							<TR>
								<TD><asp:label id="LblStatus" Runat="server" Font-Bold="True">&nbsp;Total:&nbsp;&nbsp;&nbsp;Base of EPF</asp:label>&nbsp;
									<asp:textbox id="TxtTDS2" onblur="CheckNum(this.id)" runat="server" Width="100px" CssClass="Textbox"
										Font-Bold="True" BackColor="WhiteSmoke" Enabled="False"></asp:textbox><asp:label id="lblEPSt" Runat="server" Font-Bold="True">&nbsp;&nbsp;&nbsp;Base of EPS</asp:label>&nbsp;
									<asp:textbox id="TxtSurchage2" onblur="CheckNum(this.id)" runat="server" Width="100px" CssClass="Textbox"
										Font-Bold="True" BackColor="WhiteSmoke" Enabled="False"></asp:textbox><asp:label id="total" Runat="server" Font-Bold="True">&nbsp;&nbsp;&nbsp;Total (EPF & EPS)</asp:label>&nbsp;
									<asp:textbox id="TxtTotal2" onblur="CheckNum(this.id)" runat="server" Width="100px" CssClass="Textbox"
										Font-Bold="True" BackColor="WhiteSmoke" Enabled="False"></asp:textbox></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
