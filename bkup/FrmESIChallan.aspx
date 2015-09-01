<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmESIChallan.aspx.vb" Inherits="eHRMS.Net.FrmESIChallan"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmESIChallan</title>
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
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">ESI 
						Challan Entry
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" height="19" src="Images/TableRight.gif" width="27"></TD>
				</tr>
			</TABLE>
			<table cellSpacing="1" cellPadding="" rules="none" width="680" align="center" border="1"
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
														<td><asp:textbox id="Txtrefno" Width="75px" ForeColor="#003366" Runat="server" CssClass="textbox"></asp:textbox><asp:dropdownlist id="cmbChallanNo" runat="server" Width="75px" AutoPostBack="True" Visible="False"></asp:dropdownlist><asp:imagebutton id="btnList" Width="18px" Runat="server" ToolTip="Click Here For Search Exists Record!"
																ImageUrl="Images\Find.gif" ImageAlign="AbsMiddle"></asp:imagebutton><asp:imagebutton id="btnNew" Width="18px" Runat="server" ToolTip="Click Here For Adding New Record!"
																ImageUrl="Images\NewFile.ico" ImageAlign="AbsMiddle" Height="18px"></asp:imagebutton></td>
														<td style="WIDTH: 102px"><cc1:dtp id="cmbChallanDate" runat="server" Width="100px" ForeColor="#003366"></cc1:dtp></td>
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
											<td style="WIDTH: 199px" width="199"><asp:label id="NoOfEmp" runat="server" Width="120px">No. of Employee's</asp:label></td>
											<td style="WIDTH: 248px" align="left" width="248">&nbsp;
												<asp:textbox id="TxtEmployee" runat="server" Width="78px" ForeColor="#003366" CssClass="Textbox"></asp:textbox></td>
											<td align="left" width="145"><asp:label id="LblRemi" runat="server">Remitance Date</asp:label></td>
											<td align="left" width="145"><cc1:dtp id="Dtp1" runat="server" Width="100px" ForeColor="#003366"></cc1:dtp></td>
										</tr>
										<tr>
											<TD style="WIDTH: 199px; HEIGHT: 18px" align="left"><asp:label id="LblTotalWages" runat="server">Total Wages</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
											<TD style="WIDTH: 248px; HEIGHT: 18px">&nbsp;
												<asp:textbox id="TxtWages" runat="server" Width="135px" ForeColor="#003366" CssClass="Textbox"
													ReadOnly="True"></asp:textbox></TD>
											<TD style="HEIGHT: 18px" align="left"><asp:label id="Lblbank" runat="server">Bank Name</asp:label></TD>
											<TD style="HEIGHT: 18px"><asp:dropdownlist id="CmbBankName" runat="server" Width="126px" ForeColor="#003366"></asp:dropdownlist></TD>
										</tr>
										<TR>
											<TD style="WIDTH: 199px; HEIGHT: 5px" align="left"><asp:label id="LblEmpContri" runat="server">Employee's Countribuation</asp:label></TD>
											<TD style="WIDTH: 248px; HEIGHT: 5px">&nbsp;&nbsp;<asp:textbox id="TxtEC1" runat="server" Width="44px" ForeColor="#003366" CssClass="Textbox" ReadOnly="True"></asp:textbox>%
												<asp:textbox id="TxtEC2" runat="server" Width="110px" ForeColor="#003366" CssClass="Textbox"
													ReadOnly="True"></asp:textbox></TD>
											<TD style="HEIGHT: 5px" align="left"><asp:label id="LblCheque" runat="server">Cheque No</asp:label></TD>
											<td style="HEIGHT: 5px"><asp:textbox id="TxtChequeNo" onblur="CheckNum(this.id)" runat="server" Width="100px" ForeColor="#003366"
													CssClass="Textbox" MaxLength="6"></asp:textbox></td>
										</TR>
										<TR>
											<TD style="WIDTH: 199px" align="left"><asp:label id="LblEmperCountri" runat="server">Employer's Countribuation</asp:label></TD>
											<TD style="WIDTH: 248px">&nbsp;
												<asp:textbox id="TxtER1" runat="server" Width="44px" ForeColor="#003366" CssClass="Textbox" ReadOnly="True"></asp:textbox>%
												<asp:textbox id="TxtER2" runat="server" Width="110px" ForeColor="#003366" CssClass="Textbox"
													ReadOnly="True"></asp:textbox></TD>
											<TD align="left"><asp:label id="LblCheqeDate" runat="server">Cheque Date</asp:label></TD>
											<TD><cc1:dtp id="Dtp2" runat="server" Width="100px" ForeColor="#003366"></cc1:dtp></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 199px" align="left"><asp:label id="LblTotal" runat="server">Total Contributation</asp:label></TD>
											<TD style="WIDTH: 248px">&nbsp;
												<asp:textbox id="TxtTotalContri" runat="server" Width="135px" ForeColor="#003366" CssClass="Textbox"
													ReadOnly="True"></asp:textbox></TD>
											<TD align="left"></TD>
											<TD></TD>
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
				<tr>
					<td colSpan="5"><br>
					</td>
				</tr>
				<tr height="10">
					<td colSpan="5"></td>
				</tr>
				<TR runat="server" id="Trgrid">
					<TD vAlign="top" colSpan="5">
						<TABLE id="Table11" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
							<TR>
								<TD borderColor="#000000" align="center" bgColor="gray"><asp:label id="LblEmpDetails" runat="server" ForeColor="White" Font-Size="11" Font-Bold="True">Employee's Details</asp:label></TD>
							</TR>
							<TR>
								<TD height="3"></TD>
							</TR>
							<TR>
								<TD>&nbsp;&nbsp;&nbsp;<asp:label id="NoEmp" runat="server" ForeColor="Gray" Font-Bold="True"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:label id="TotalPage" runat="server" ForeColor="Gray" Font-Bold="True"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:label id="CurrentPage" runat="server" ForeColor="Gray" Font-Bold="True"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:label id="MonthOf" runat="server" ForeColor="Gray" Font-Bold="True"></asp:label></TD>
							</TR>
							<TR>
								<TD height="3"></TD>
							</TR>
							<TR>
								<TD><asp:datagrid id="GrdRecords" Visible="True" runat="server" Width="680px" AllowPaging="True" AutoGenerateColumns="False">
										<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
										<HeaderStyle HorizontalAlign="Center" CssClass="Header3"></HeaderStyle>
										<Columns>
											<asp:BoundColumn DataField="Emp_Code" HeaderText="Code">
												<HeaderStyle Font-Bold="True"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="Emp_Name" HeaderText="Name">
												<HeaderStyle Font-Bold="True"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="ESISalary" HeaderText="Wages">
												<HeaderStyle Font-Bold="True"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="ESI" HeaderText="Employee's Contri.">
												<HeaderStyle Font-Bold="True"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="EESI" HeaderText="Employer's Contri.">
												<HeaderStyle Font-Bold="True"></HeaderStyle>
											</asp:BoundColumn>
										</Columns>
										<PagerStyle NextPageText="" Font-Size="X-Small" Font-Bold="True" PrevPageText="" HorizontalAlign="Center"
											ForeColor="#330066" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></TD>
							</TR>
							<TR>
								<TD><asp:label id="LblWages" Runat="server" Font-Bold="True"> Total: Wages</asp:label>&nbsp;
									<asp:textbox id="TxtTotalWages" runat="server" Width="100px" CssClass="Textbox" Font-Bold="True"
										BackColor="WhiteSmoke" Enabled="False" ReadOnly="True"></asp:textbox><asp:label id="LblEmplCobntri" Runat="server" Font-Bold="True"> Employee Contribuation</asp:label>&nbsp;
									<asp:textbox id="TxtEmpCont" runat="server" Width="100px" CssClass="Textbox" Font-Bold="True"
										BackColor="WhiteSmoke" Enabled="False" ReadOnly="True"></asp:textbox><asp:label id="LblEmperContri" Width="112px" Runat="server" Font-Bold="True"> Employer Contri</asp:label>&nbsp;
									<asp:textbox id="TxtEmprCont" runat="server" Width="100px" CssClass="Textbox" Font-Bold="True"
										BackColor="WhiteSmoke" Enabled="False" ReadOnly="True"></asp:textbox></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
