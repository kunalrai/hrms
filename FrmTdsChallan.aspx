<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmTdsChallan.aspx.vb" Inherits="eHRMS.Net.FrmTdsChallan" %>
<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmDdsChallan</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
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
		</script>
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx --><br>
			<br>
			<P align="center">
				<TABLE id="Table14" cellSpacing="0" cellPadding="0" width="740" align="center" border="0">
					<TR>
						<TD>
							<TABLE id="Table15" cellSpacing="0" cellPadding="0" width="100%" align="left" border="0">
								<TR>
									<TD>
										<TABLE id="Table17" cellSpacing="0" cellPadding="0" width="100%" align="left" border="0">
											<TR>
												<TD style="WIDTH: 46px"><IMG class="SetImageFace" src="Images/TableLeft.gif"></TD>
												<TD style="WIDTH: 68px" background="Images/TableMid.gif"></TD>
												<TD style="WIDTH: 590px" background="Images/TableMid.gif"><FONT face="Verdana" color="#003366" size="3"><STRONG>TDS 
															Challan Entry.......</STRONG></FONT></TD>
												<TD><IMG class="SetImageFace" src="Images/TableRight.gif"></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD>
										<TABLE id="Table16" cellSpacing="2" cellPadding="0" width="100%" align="left" border="1">
											<TR>
												<TD>
													<TABLE id="Table18" cellSpacing="0" cellPadding="0" width="100%" align="left" border="0">
														<TBODY>
															<TR>
																<TD>
																	<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" align="left" border="0">
																		<TR>
																			<TD>
																				<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" align="left" border="0">
																					<TR>
																						<TD>
																							<TABLE id="Table4" cellSpacing="2" cellPadding="0" width="100%" align="left" border="0">
																								<TR>
																									<TD colSpan="4">
																										<P align="left"><asp:label id="LblMsg" runat="server" ForeColor="Red" Width="100%">ERROR</asp:label></P>
																									</TD>
																								</TR>
																								<TR>
																									<TD style="HEIGHT: 19px">&nbsp;
																										<asp:label id="LblChNo" runat="server">Challan No:</asp:label></TD>
																									<TD style="WIDTH: 361px; HEIGHT: 19px"><FONT face="Verdana" color="#000066" size="2"><asp:textbox id="Txtrefno" Width="120px" Runat="server" CssClass="textbox" ForeColor="#003366"></asp:textbox><asp:dropdownlist id="DrlChallanNo" runat="server" Width="150px" AutoPostBack="True" Visible="False"></asp:dropdownlist><asp:imagebutton id="btnList" Width="18px" Runat="server" ToolTip="Click Here For Search Exists Record!"
																												ImageUrl="Images\Find.gif" ImageAlign="AbsMiddle"></asp:imagebutton><asp:imagebutton id="btnNew" Width="18px" Runat="server" ToolTip="Click Here For Adding New Record!"
																												ImageUrl="Images\NewFile.ico" ImageAlign="AbsMiddle" Height="19px"></asp:imagebutton></FONT></TD>
																									<TD style="HEIGHT: 19px"><asp:label id="LblChDate" runat="server">Challan Date:</asp:label></TD>
																									<TD style="HEIGHT: 19px"><cc1:dtpcombo id="Dtppaydate" runat="server" Width="112px" ToolTip="paydate" Height="20px"></cc1:dtpcombo></TD>
																								</TR>
																								<TR>
																									<TD>&nbsp;
																										<asp:label id="LblBank" runat="server">Bank:</asp:label></TD>
																									<TD style="WIDTH: 361px"><asp:dropdownlist id="DrlBankName" runat="server" Width="320px" AutoPostBack="True" ForeColor="#003366"></asp:dropdownlist><FONT face="Verdana" color="#000066" size="2"></FONT></TD>
																									<TD><asp:label id="LblTDS" runat="server">TDS:</asp:label></TD>
																									<TD><asp:textbox id="TxtTDS" onblur="CheckNum(this.id)" runat="server" ForeColor="#003366" Width="150px"
																											CssClass="TextBox"></asp:textbox><FONT face="Verdana" color="#000066" size="2"></FONT></TD>
																								</TR>
																								<TR>
																									<TD style="WIDTH: 76px">&nbsp;
																										<asp:label id="LblBSR" runat="server" ForeColor="Black">BSR Code:</asp:label></TD>
																									<TD style="WIDTH: 362px">
																										<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="100%" align="left" border="0">
																											<TR>
																												<TD style="WIDTH: 105px"><asp:textbox id="TxtBSRCode" runat="server" ForeColor="#003366" Width="124px" CssClass="Textbox"></asp:textbox></TD>
																												<TD style="WIDTH: 99px">&nbsp;&nbsp;<asp:label id="LblCheq" Runat="server">Chq./DD No.</asp:label>
																												</TD>
																												<TD><asp:textbox id="TxtCheckNo" onblur="CheckNum(this.id)" runat="server" ForeColor="#003366" Width="98px"
																														CssClass="Textbox" MaxLength="6"></asp:textbox></TD>
																											</TR>
																										</TABLE>
																									</TD>
																									<TD style="WIDTH: 91px"><asp:label id="LblSurcharge" runat="server">Surcharge:</asp:label></TD>
																									<TD><asp:textbox id="TxtSurcharge" onblur="CheckNum(this.id)" runat="server" ForeColor="#003366"
																											Width="150px" CssClass="Textbox"></asp:textbox></TD>
																								</TR>
																								<T>
																									<TR>
																										<TD style="WIDTH: 79px; HEIGHT: 12px">&nbsp;
																											<asp:label id="LblMonth" runat="server">Month:</asp:label></TD>
																										<TD style="WIDTH: 361px; HEIGHT: 12px"><asp:dropdownlist id="DrlMonth" runat="server" Width="126px" ForeColor="#003366"></asp:dropdownlist>&nbsp;
																											<asp:label id="LblIntrest" Runat="server">Intrest:</asp:label>&nbsp;&nbsp;<asp:textbox id="TxtIntrest" onblur="CheckNum(this.id)" runat="server" ForeColor="#003366" Width="137px"
																												CssClass="TextBox" AutoPostBack="True" align="Right"></asp:textbox><FONT face="Verdana" color="#000066"></FONT></FONT></TD>
																										<TD style="WIDTH: 91px; HEIGHT: 12px"><asp:label id="LblCess" runat="server">Cess:</asp:label></TD>
																										<TD style="HEIGHT: 12px"><asp:textbox id="TxtCess" onblur="CheckNum(this.id)" runat="server" ForeColor="#003366" Width="150px"
																												CssClass="Textbox"></asp:textbox><FONT face="Verdana" color="#000066" size="2"></FONT></TD>
																									</TR>
																									<TR>
																										<TD style="WIDTH: 79px"></TD>
																										<TD style="WIDTH: 362px"></TD>
																										<TD style="WIDTH: 91px"><asp:label id="LblTotal" runat="server">Total:</asp:label></TD>
																										<TD><asp:textbox id="TxtTotal" runat="server" Width="151px" CssClass="Textbox" AutoPostBack="True"
																												BackColor="Gainsboro" Font-Bold="True" Enabled="False" ForeColor="#003366"></asp:textbox></TD>
																									</TR></TABLE>
																						</TD>
																					</TR>
																					<TR>
																						<TD>
																							<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" align="left" border="0">
																								<TR>
																									<TD>
																										<TABLE id="Table7" cellSpacing="0" cellPadding="0" width="100%" align="left" border="0">
																											<TR>
																												<TD style="HEIGHT: 11px">&nbsp;
																													<asp:label id="NoEmp" runat="server" ForeColor="Gray" Font-Bold="True"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																													<asp:label id="TotalPage" runat="server" ForeColor="Gray" Font-Bold="True"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																													<asp:label id="CurrentPage" runat="server" ForeColor="Gray" Font-Bold="True"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																													<asp:label id="MonthOf" runat="server" ForeColor="Gray" Font-Bold="True"></asp:label>&nbsp;&nbsp;&nbsp;
																												</TD>
																											</TR>
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
																</TD>
															</TR>
															<TR>
																<TD>
																	<DIV style="BORDER-RIGHT: thin solid; BORDER-TOP: thin solid; OVERFLOW: auto; BORDER-LEFT: thin solid; WIDTH: 100%; COLOR: #cccccc; BORDER-BOTTOM: thin solid; HEIGHT: 242px; StyleBORDER-TOP: 1px solid"><asp:datagrid id="ShowRecords" Width="100%" Runat="server" AutoGenerateColumns="False" AllowPaging="True">
																			<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
																			<HeaderStyle Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Top"
																				BackColor="Gray"></HeaderStyle>
																			<Columns>
																				<asp:BoundColumn DataField="Emp_Code" HeaderText="Emp_Code"></asp:BoundColumn>
																				<asp:BoundColumn DataField="Emp_Name" HeaderText="Name"></asp:BoundColumn>
																				<asp:TemplateColumn HeaderText="TDS">
																					<ItemTemplate>
																						<asp:TextBox id=TxtTDSB onblur=CheckNum(this.id) runat="server" Width="153px" ForeColor="#003366" AutoPostBack="True" CssClass="Textbox" OnTextChanged="OnTextChanged" Text='<% # Container.dataitem("ITAX")%>'>
																						</asp:TextBox>
																					</ItemTemplate>
																				</asp:TemplateColumn>
																				<asp:TemplateColumn HeaderText="Surcharge">
																					<ItemTemplate>
																						<asp:TextBox id=TxtSurch onblur=CheckNum(this.id) runat="server" Width="153px" ForeColor="#003366" AutoPostBack="True" CssClass="Textbox" OnTextChanged="OnTextChanged" Text='<% # Container.dataitem("Surch")%>'>
																						</asp:TextBox>
																					</ItemTemplate>
																				</asp:TemplateColumn>
																				<asp:TemplateColumn HeaderText="Cess">
																					<ItemTemplate>
																						<asp:TextBox id=TxtCessM onblur=CheckNum(this.id) runat="server" Width="153px" ForeColor="#003366" AutoPostBack="True" CssClass="Textbox" OnTextChanged="OnTextChanged" Text='<% # Container.dataitem("CessPm")%>'>
																						</asp:TextBox>
																					</ItemTemplate>
																				</asp:TemplateColumn>
																				<asp:BoundColumn DataField="Total" HeaderText="Total"></asp:BoundColumn>
																			</Columns>
																			<PagerStyle Font-Size="X-Small" Font-Bold="True" HorizontalAlign="Center" ForeColor="#003366"
																				Mode="NumericPages"></PagerStyle>
																		</asp:datagrid>
																		<DIV></DIV>
																	</DIV>
																</TD>
															</TR>
															<TR>
																<TD>
																	<TABLE id="Table6" cellSpacing="2" cellPadding="2" width="100%" align="left" border="0">
																		<TR>
																			<TD><asp:checkbox id="ChkShow" runat="server" Font-Bold="True" Text="Show All Employee Including Resigned"></asp:checkbox></TD>
																			<TD><asp:textbox id="TxtTDS1" style="TEXT-ALIGN: right" runat="server" Width="100px" CssClass="Textbox"
																					BackColor="WhiteSmoke" Enabled="False" ForeColor="#003366"></asp:textbox></TD>
																			<TD><asp:textbox id="TxtSurchage1" style="TEXT-ALIGN: right" runat="server" Width="100px" CssClass="Textbox"
																					BackColor="WhiteSmoke" Enabled="False" ForeColor="#003366"></asp:textbox></TD>
																			<TD><asp:textbox id="TxtCess1" style="TEXT-ALIGN: right" runat="server" Width="100px" CssClass="Textbox"
																					BackColor="WhiteSmoke" Enabled="False" ForeColor="#003366"></asp:textbox></TD>
																			<TD><asp:textbox id="TxtTotal1" style="TEXT-ALIGN: right" runat="server" Width="100px" CssClass="Textbox"
																					BackColor="WhiteSmoke" Font-Bold="True" Enabled="False" ForeColor="#003366"></asp:textbox></TD>
																		</TR>
																		<TR>
																			<TD><asp:label id="LblStatus" Runat="server" Font-Bold="True">&nbsp;Total Status:&nbsp;(TDS/Surcharge/Cess/Total)</asp:label></TD>
																			<TD><asp:textbox id="TxtTDS2" style="TEXT-ALIGN: right" runat="server" Width="100px" CssClass="Textbox"
																					BackColor="WhiteSmoke" Font-Bold="True" Enabled="False" ForeColor="#003366"></asp:textbox></TD>
																			<TD><asp:textbox id="TxtSurchage2" style="TEXT-ALIGN: right" runat="server" Width="100px" CssClass="Textbox"
																					BackColor="WhiteSmoke" Font-Bold="True" Enabled="False" ForeColor="#003366"></asp:textbox></TD>
																			<TD><asp:textbox id="TxtCess2" style="TEXT-ALIGN: right" runat="server" Width="100px" CssClass="Textbox"
																					BackColor="WhiteSmoke" Font-Bold="True" Enabled="False" ForeColor="#003366"></asp:textbox></TD>
																			<TD><asp:textbox id="TxtTotal2" runat="server" Width="100px" CssClass="Textbox" BackColor="WhiteSmoke"
																					Font-Bold="True" Enabled="False" ForeColor="#003366"></asp:textbox></TD>
																		</TR>
																	</TABLE>
																</TD>
															</TR>
															<TR>
																<TD></TD>
															</TR>
															<tr>
															</tr>
															<tr height="10">
															</tr>
												</TD>
											</TR>
											<TR>
												<TD><asp:checkbox id="ChkExclude" runat="server" Width="220px" Font-Bold="True" Text="Exclude Existing Records"></asp:checkbox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
													&nbsp;&nbsp;&nbsp;<asp:button id="BtnPayRoll" runat="server" ToolTip="Click here For Payroll !" Text="Receive From Payroll"></asp:button>&nbsp;<asp:button id="BtnOk" runat="server" Width="75px" ToolTip="Click Here For Save !" Text="Save"></asp:button>&nbsp;<asp:button id="BtnCencel" runat="server" Width="75px" ToolTip="Click Here For Hide The Grid !"
														Text="Cencel "></asp:button>&nbsp;<asp:button id="BtnClose" runat="server" Width="75px" ToolTip="Click Here For Close !" Text="Close"></asp:button></TD>
											</TR>
											<TR>
												<TD></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
				</TD></TR></TBODY></TABLE><A name=""></A></P>
		</form>
	</body>
</HTML>
