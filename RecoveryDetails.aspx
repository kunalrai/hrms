<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="RecoveryDetails.aspx.vb" Inherits="eHRMS.Net.RecoveryDetails" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>RecoveryDetails</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		function CloseWindow()
		{
			window.close();
		}
			
		</script>
		<script language="javascript">
		function ConfirmDelete()
		{
			if(confirm("Are You Sure To Delete This Record?"+"...[HRMS]")==true)
				return true;
			else
				return false;
		}
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
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<asp:linkbutton id="CmdClose" runat="server">Back</asp:linkbutton><br>
			<!--#include file="MenuBars.aspx"-->
			<TABLE cellSpacing="0" cellPadding="0" width="500" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Recovery 
						Details......
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" rules="none" width="500" align="center" border="1"
				frame="border">
				<tr>
				</tr>
				<tr>
					<td colSpan="5"><asp:label id="LblErrMsg" Runat="server" ForeColor="#ff3333" Width="152px"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:label id="TotalPage" runat="server" ForeColor="#003366" Font-Bold="True" Font-Names="Vardana"></asp:label></td>
				</tr>
				<tr>
					<td colSpan="5"><asp:datagrid id="GrdRecoveryDetail" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True">
							<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
							<HeaderStyle Font-Names="Arial" Font-Bold="True" HorizontalAlign="Left" ForeColor="White" VerticalAlign="Top"
								BackColor="Gray"></HeaderStyle>
							<Columns>
								<asp:ButtonColumn Text="Select" HeaderText="Select" CommandName="Select"></asp:ButtonColumn>
								<asp:BoundColumn DataField="L_RDate" HeaderText="Recovery Date"></asp:BoundColumn>
								<asp:BoundColumn DataField="L_Rec" HeaderText="Principal Amt">
									<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Int" HeaderText="Intrest">
									<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle NextPageText="|Next" Font-Size="X-Small" Font-Bold="True" PrevPageText="Prev" HorizontalAlign="Center"
								ForeColor="#003366" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
				<TR>
				</TR>
				<tr>
					<td align="right" colSpan="5">&nbsp;
						<hr style="BORDER-BOTTOM: #993366 thin solid">
						<asp:button id="CmdNew" runat="server" Width="65px" Text="New"></asp:button>&nbsp;
						<asp:button id="CmdDelete" runat="server" Width="65px" Text="Delete"></asp:button>&nbsp;
						<asp:button id="CmdClos" runat="server" Width="65px" Text="Close"></asp:button></td>
				</tr>
				<tr>
					<td colSpan="5" height="8"></td>
				</tr>
			</table>
			<P>
				<TABLE id="TblReco" cellSpacing="0" cellPadding="0" width="500" align="center" border="0"
					runat="server">
					<TR>
						<TD>
							<TABLE cellSpacing="0" cellPadding="0" width="500" align="center" border="0">
								<tr>
									<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
									</TD>
									<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
										height="19">&nbsp;</TD>
									<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Loan 
										&amp; Advance Recovery.....</TD>
									<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
									</TD>
								</tr>
							</TABLE>
							<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" align="left" border="1">
								<TR>
									<TD>
										<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" align="left" border="0">
											<TR>
												<TD></TD>
											</TR>
											<TR>
												<TD><asp:label id="LblErrMsg1" Runat="server" ForeColor="#ff3333" Width="488px"></asp:label></TD>
											</TR>
											<TR>
												<TD>
													<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" align="left" border="0">
														<TR>
															<TD>&nbsp;<asp:label id="LblEmployee" runat="server">Employee</asp:label></TD>
															<TD><asp:dropdownlist id="CmbEmployee" runat="server" Width="384px" Enabled="False"></asp:dropdownlist></TD>
														</TR>
														<TR>
															<TD width="104">&nbsp;<asp:label id="LblLoanType" runat="server">Loan Type</asp:label></TD>
															<TD><asp:dropdownlist id="CmbLoanType" runat="server" Width="384px" Enabled="False"></asp:dropdownlist></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD>
													<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" align="left" border="0">
														<TR>
															<TD>&nbsp;<asp:label id="LblEsctionDate" runat="server">Section Date</asp:label></TD>
															<TD><cc1:dtp id="DtpSectionDate" runat="server" ForeColor="#003366" Enabled="False" width="133px"
																	ToolTip="Entry Date"></cc1:dtp></TD>
															<TD><asp:label id="LblRecoveryDate" runat="server">Recovery Date</asp:label></TD>
															<TD><cc1:dtp id="DtpRecoveryDate" runat="server" ForeColor="#003366" width="130px" ToolTip="Entry Date"></cc1:dtp></TD>
														</TR>
														<TR>
															<TD>&nbsp;<asp:label id="LblPrincipalBal" runat="server">Principal Balance</asp:label></TD>
															<TD><asp:textbox id="TxtPrincipalBal" onblur="CheckNum(this.id)" style="TEXT-ALIGN: right" runat="server"
																	Width="135px" ReadOnly="True" CssClass="TextBox" ForeColor="#003366"></asp:textbox></TD>
															<TD><asp:label id="LblPrincipalReco" runat="server">Principal Recovery</asp:label></TD>
															<TD><asp:textbox id="TxtPrincipalReco" onblur="CheckNum(this.id)" style="TEXT-ALIGN: right" runat="server"
																	Width="135px" CssClass="TextBox" ForeColor="#003366"></asp:textbox></TD>
														</TR>
														<TR>
															<TD>&nbsp;<asp:label id="Label5" runat="server">Intrest Balance</asp:label></TD>
															<TD><asp:textbox id="TxtIntBal" onblur="CheckNum(this.id)" style="TEXT-ALIGN: right" runat="server"
																	Width="135px" CssClass="TextBox" ForeColor="#003366"></asp:textbox></TD>
															<TD><asp:label id="LblIntrestRecovery" runat="server">Intrest Recovery</asp:label></TD>
															<TD><asp:textbox id="TxtIntReco" onblur="CheckNum(this.id)" style="TEXT-ALIGN: right" runat="server"
																	Width="135px" CssClass="TextBox" ForeColor="#003366"></asp:textbox></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD></TD>
											</TR>
											<TR>
												<TD>
													<hr style="BORDER-BOTTOM: #993366 thin solid">
													<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="40%" align="right" border="0">
														<TR>
															<TD><asp:button id="CmdOk" runat="server" Width="62px" Text="Save"></asp:button></TD>
															<TD><asp:button id="CmdCancel" runat="server" Width="62px" Text="Cancel"></asp:button></TD>
															<TD><asp:button id="CmdCl" runat="server" Width="62px" Text="Close"></asp:button></TD>
														</TR>
														<tr>
															<td colSpan="3" height="7"></td>
														</tr>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</P>
			<P>&nbsp;</P>
		</form>
		</TD></TR></TBODY></TABLE></TR></TBODY></TABLE></TR></TBODY>
		<P></P>
		</FORM>
	</body>
</HTML>
