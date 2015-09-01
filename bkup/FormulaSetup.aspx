<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FormulaSetup.aspx.vb" Inherits="eHRMS.Net.FormulaSetup" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FormulaSetup</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript">
		function ConfirmDelete()
		{
			if(confirm("Are You Sure To Delete This Record?"+"...[HRMS]")==true)
				return true;
			else
				return false;
		}
		</script>
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<br>
			<!--#include file=MenuBars.aspx -->
			<TABLE cellSpacing="0" cellPadding="0" width="700" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Formula 
						Setup....
					</TD>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<TABLE cellSpacing="2" cellPadding="0" rules="none" width="700" align="center" border="1"
				frame="box">
				<tr>
					<td width="50%"></td>
					<td width="50%"></td>
				</tr>
				<tr>
					<td colSpan="2"><asp:label id="LblErrMsg" runat="server" ForeColor="Red" Width="100%"></asp:label></td>
				</tr>
				<tr>
					<td vAlign="top">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0" frame="box">
							<tr>
								<td>
									<div style="OVERFLOW: auto; WIDTH: 350px; COLOR: #cccccc; BORDER-TOP-STYLE: solid; BORDER-RIGHT-STYLE: solid; BORDER-LEFT-STYLE: solid; HEIGHT: 480px; BORDER-BOTTOM-STYLE: solid">
										<asp:datagrid id="GrdFormula" Width="120%" AutoGenerateColumns="False" Runat="server">
											<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
											<HeaderStyle Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Top"
												BackColor="Gray"></HeaderStyle>
											<Columns>
												<asp:ButtonColumn HeaderText="Field Name" DataTextField="Field_Name" HeaderStyle-Width="25%" CommandName="Select"></asp:ButtonColumn>
												<asp:BoundColumn DataField="Fld_Date" HeaderText="Entry Date">
													<HeaderStyle Width="21%"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="FIELD_DESC" HeaderStyle-HorizontalAlign="Left" HeaderText="Description">
													<HeaderStyle Width="54%"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn Visible="False" HeaderStyle-Width="0%" DataField="Fld_WEF" HeaderText="Fld_WEF"></asp:BoundColumn>
											</Columns>
										</asp:datagrid></div>
								</td>
							</tr>
						</table>
					</td>
					<td>
						<table cellSpacing="2" cellPadding="2" width="340" borderColor="buttonface" border="1">
							<tr>
								<td width="30%" style="HEIGHT: 13px"></td>
								<td width="70%" style="HEIGHT: 13px"></td>
							</tr>
							<tr>
								<td>&nbsp;<asp:label id="LblFieldname" Runat="server" text="Field Name"></asp:label></td>
								<TD style="WIDTH: 361px; HEIGHT: 19px"><FONT face="Verdana" color="#000066" size="2"><asp:textbox id="TxtFieldName" Width="144px" AutoPostBack="True" CssClass="textbox" Runat="server"
											ReadOnly="True" ForeColor="#003366"></asp:textbox><asp:dropdownlist id="CmbDescription" runat="server" Width="150px" AutoPostBack="True" Visible="False"></asp:dropdownlist><asp:imagebutton id="btnNew" Width="18px" Runat="server" ImageAlign="AbsMiddle" ImageUrl="Images\NewFile.ico"
											ToolTip="Click Here For Adding New Record!" Height="19px"></asp:imagebutton></FONT></TD>
							</tr>
							<tr>
								<td>&nbsp;<asp:label id="LblDescription" Runat="server" text="Description"></asp:label></td>
								<td><asp:textbox id="TxtDescription" ForeColor="#003366" Width="100%" Runat="server" CssClass="textbox"
										ReadOnly="True"></asp:textbox></td>
							</tr>
							<tr>
								<td colSpan="2" height="5"></td>
							</tr>
							<tr>
								<td colSpan="2">
									<table cellSpacing="0" cellPadding="0" width="100%">
										<tr>
											<td width="30%"></td>
											<td width="70%"></td>
										</tr>
										<TR>
											<TD colSpan="2">
												<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" align="left" border="0">
													<TR>
														<TD><asp:label id="Label1" Width="57" Runat="server" text="Entry Date"></asp:label></TD>
														<TD><cc1:dtp id="DtpEntryDate" runat="server" ForeColor="#003366" ToolTip="Entry Date" width="95px"></cc1:dtp></TD>
														<TD><asp:label id="Label2" Runat="server" Width="75" text="Effected Date">Effected Date</asp:label></TD>
														<TD><cc1:dtp id="DtpEffectiveDate" runat="server" ForeColor="#003366" ToolTip="Effective Date"
																width="95px" TextBoxPostBack="True"></cc1:dtp></TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
									</table>
								</td>
							</tr>
							<tr>
								<td colSpan="2" height="10"></td>
							</tr>
							<TR>
								<td colSpan="2" align="left">
									<table borderColor="#cccccc" cellSpacing="0" cellPadding="0" width="275" align="right"
										border="0" frame="box">
										<tr>
											<td class="Header3" align="center" background="Images\headstripe.jpg" colSpan="2"><b>Round 
													Off</b></td>
										</tr>
										<tr>
											<td align="left"><asp:radiobuttonlist id="RblRoundOff" Runat="server" RepeatDirection="Vertical" Width="330px" RepeatColumns="2">
													<asp:ListItem Value="1">As It Is</asp:ListItem>
													<asp:ListItem Value="2">Nearest Multiple</asp:ListItem>
													<asp:ListItem Value="3">Previous Multiple</asp:ListItem>
													<asp:ListItem Value="4">Next Multiple</asp:ListItem>
												</asp:radiobuttonlist></td>
										</tr>
									</table>
								</td>
							</TR>
							<tr>
								<td align="right" colSpan="2" style="HEIGHT: 18px"><asp:label id="LblMul" Runat="server" text="Multiple"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;<asp:textbox id="TxtMultiple" runat="server" ForeColor="#003366" Width="50px" CssClass="TextBox"></asp:textbox>
								</td>
							</tr>
							<TR>
								<TD style="HEIGHT: 18px" align="right" colSpan="2"></TD>
							</TR>
							<tr>
								<td colSpan="2">
									<table cellSpacing="0" cellPadding="0" width="100%" frame="box" align="left">
										<tr>
											<td class="Header3" align="center" background="Images\headstripe.jpg"><b>Expression</b></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td colSpan="2"><asp:textbox id="TxtFormula" ForeColor="#003366" Width="100%" Runat="server" TextMode="MultiLine"
										Height="75px"></asp:textbox></td>
							</tr>
							<tr>
								<td colSpan="2">
									<table cellSpacing="0" cellPadding="0" width="100%" frame="box" align="left">
										<tr>
											<td class="Header3" align="center" background="Images\headstripe.jpg"><b>Condition</b></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td colSpan="2"><asp:textbox id="TxtCondition" ForeColor="#003366" Width="100%" Runat="server" TextMode="MultiLine"
										Height="75px"></asp:textbox></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR height="15">
					<TD style="MARGIN-LEFT: 15px; MARGIN-RIGHT: 15px; BORDER-BOTTOM: #993300 thin groove; HEIGHT: 7px"
						colSpan="3"></TD>
				</TR>
				<tr>
					<td colspan="3" height="5"></td>
				</tr>
				<tr>
					<td align="right" colSpan="2">
						<table cellSpacing="0" cellPadding="0" border="0" frame="box">
							<tr>
								<td>&nbsp;&nbsp;
									<asp:button id="CmdDelete" runat="server" Width="75px" Text="Delete"></asp:button>&nbsp;&nbsp;
									<asp:button id="CmdSave" runat="server" Width="75px" Text="Save"></asp:button>&nbsp;&nbsp;
									<asp:button id="CmdCancel" runat="server" Width="75px" Text="Cancel"></asp:button>&nbsp;&nbsp;
									<asp:button id="CmdOpen" runat="server" Width="75px" Text="Close"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="2" height="5"></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
