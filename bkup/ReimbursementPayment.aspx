<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ReimbursementPayment.aspx.vb" Inherits="eHRMS.Net.ReimbursementPayment" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ReimbursementPayment</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
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
	<body topMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx --><br>
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="707" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="15">&nbsp;
					</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Reimbursement 
						Payment ....
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" height="19" src="Images/TableRight.gif" width="27">
					</TD>
				</tr>
			</TABLE>
			<TABLE cellSpacing="1" cellPadding="1" rules="none" width="700" align="center" border="1"
				frame="box">
				<tr height="0">
					<td vAlign="top" width="14%"></td>
					<td vAlign="top" width="41%"></td>
					<td vAlign="top" width="12%"></td>
					<td vAlign="top" width="33%"></td>
				</tr>
				<tr>
					<td vAlign="top" colSpan="4"><asp:label id="LblErrMsg" runat="server" Width="100%" ForeColor="Red"></asp:label></td>
				</tr>
				<tr>
					<td colSpan="4">
						<table cellPadding="0" rules="none" width="98%" align="center" border="1" frame="box">
							<tr>
								<td>&nbsp;<asp:label id="Label3" Runat="server" text="Date"></asp:label></td>
								<td style="WIDTH: 125px">&nbsp;<cc1:dtp id="Dtpdate" runat="server" ForeColor="#003366" width="110px" ToolTip="Date" TextBoxPostBack="True"></cc1:dtp></td>
								<td>&nbsp;<asp:label id="Label2" Runat="server" text="Employee"></asp:label></td>
								<td>&nbsp;<asp:textbox id="TxtCode" Width="70" ForeColor="#003366" Runat="server" AutoPostBack="True" CssClass="textbox"></asp:textbox><asp:dropdownlist id="cmbEmp" runat="server" Width="200px" ForeColor="#003366" AutoPostBack="True"
										Visible="False"></asp:dropdownlist><asp:imagebutton id="btnList" Width="18px" Runat="server" Height="19px" ImageUrl="Images\Find.gif"
										ImageAlign="AbsMiddle"></asp:imagebutton>&nbsp;&nbsp;<asp:label id="LblEmpName" Runat="server" text="Name"></asp:label>&nbsp;&nbsp;
									<asp:label id="LblName" Width="200" ForeColor="#003366" Runat="server" Font-Size="9" Font-Bold="True"></asp:label></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="10">
					<td colSpan="4"></td>
				</tr>
				<tr>
					<td>&nbsp;<asp:label id="Lblgrade" Runat="server" text="Grade"></asp:label></td>
					<td>&nbsp;<asp:textbox id="TxtGrade" Width="180px" ForeColor="#003366" Runat="server" CssClass="textbox"
							ReadOnly="True"></asp:textbox></td>
					<td>&nbsp;<asp:label id="Lbllocation" Runat="server" text="Location"></asp:label></td>
					<td>&nbsp;<asp:textbox id="TxtLoc" Width="150" ForeColor="#003366" Runat="server" CssClass="textbox" ReadOnly="True"></asp:textbox></td>
				</tr>
				<tr>
					<td>&nbsp;<asp:label id="Lbldesig" Runat="server" text="Designation"></asp:label></td>
					<td>&nbsp;<asp:textbox id="TxtDesg" Width="180px" ForeColor="#003366" Runat="server" CssClass="textbox"
							ReadOnly="True"></asp:textbox></td>
					<td>&nbsp;<asp:label id="lbldeprt" Runat="server" text="Department"></asp:label></td>
					<td>&nbsp;<asp:textbox id="TxtDept" Width="150" ForeColor="#003366" Runat="server" CssClass="textbox" ReadOnly="True"></asp:textbox>&nbsp;&nbsp;</td>
				</tr>
				<tr>
					<td colSpan="4" height="5"></td>
				<TR height="15">
					<TD style="MARGIN-LEFT: 15px; MARGIN-RIGHT: 15px; BORDER-BOTTOM: #993300 thin groove; HEIGHT: 1px"
						colSpan="4"></TD>
				</TR>
				<tr>
					<td colSpan="4">
						<table id="TblReim" cellSpacing="0" cellPadding="0" rules="none" width="100%" border="0"
							frame="box">
							<tr>
								<td colSpan="4">
									<div style="OVERFLOW: auto; WIDTH: 700px; HEIGHT: 250px"><asp:datagrid id="GrdReim" runat="server" Width="900" PagerStyle-Mode="NumericPages" AutoGenerateColumns="False">
											<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
											<HeaderStyle Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Top"
												BackColor="Gray"></HeaderStyle>
											<Columns>
												<asp:BoundColumn DataField="Print_Name" HeaderText="A/c Head">
													<ItemStyle HorizontalAlign="Left" Width="75px"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="PRORATA" HeaderText="Total Entitlement">
													<ItemStyle HorizontalAlign="Right" Width="75px"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Tot_Opn" HeaderText="Total Opening">
													<ItemStyle HorizontalAlign="Right" Width="75px"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="YTD_Prorata" HeaderText="YTD Entitlement">
													<ItemStyle HorizontalAlign="Right" Width="75px"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="REIMBURSED" HeaderText="YTD Reimbursed">
													<ItemStyle HorizontalAlign="Right" Width="75px"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Opening" HeaderText="Available Till Date">
													<ItemStyle HorizontalAlign="Right" Width="75px"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="CarryFwdAmt" HeaderText="Carry Fwd Amount">
													<ItemStyle HorizontalAlign="Right" Width="75px"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Claimed Amount">
													<HeaderStyle Width="75px"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:TextBox id="TxtCLAmt" onblur="CheckNum(this.id)" ForeColor="#003366" Width="75px" Runat="server" CssClass="TextBox" text='<%# DataBinder.Eval(Container.DataItem, "Claimed")%>'>
														</asp:TextBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Reimbursed Amount">
													<HeaderStyle Width="75px"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:TextBox id="TxtRimAmt" AutoPostBack=true onblur="CheckNum(this.id)" ForeColor="#003366" Width="75px" Runat="server" CssClass="TextBox" text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' OnTextChanged="OnAmountChanged">
														</asp:TextBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Taxable Amount">
													<HeaderStyle Width="75px"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:TextBox id="TxtTaxAmt" AutoPostBack=true onblur="CheckNum(this.id)" ForeColor="#003366" Width="75px" Runat="server" CssClass="TextBox" text='<%# DataBinder.Eval(Container.DataItem, "TAXABLE")%>' OnTextChanged="OnTaxableChanged">
														</asp:TextBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="TDS Amount">
													<HeaderStyle Width="75px"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:TextBox id="TxtTDSAmt" AutoPostBack=true onblur="CheckNum(this.id)" ForeColor="#003366" Width="75px" Runat="server" CssClass="TextBox" text='<%# DataBinder.Eval(Container.DataItem, "TDS")%>' OnTextChanged="OnTDSChanged">
														</asp:TextBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="Closing" HeaderText="Closing">
													<ItemStyle Width="75px"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="FIELD_Name" HeaderText="FIELD_NAME">
													<ItemStyle HorizontalAlign="Left" Width="0px"></ItemStyle>
												</asp:BoundColumn>
											</Columns>
											<PagerStyle Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>&nbsp;<asp:label id="LblRemarks" Runat="server">Remarks</asp:label></td>
					<td colSpan="3">&nbsp;<asp:textbox id="TxtRemarks" Width="99%" Runat="server" Rows="3" TextMode="MultiLine"></asp:textbox></td>
				</tr>
				<tr>
					<td>&nbsp;<asp:label id="LblMonth" Runat="server">Paid Month</asp:label></td>
					<td>&nbsp;<asp:dropdownlist id="CmbMonth" Width="170" Runat="server"></asp:dropdownlist></td>
					<td>&nbsp;<asp:label id="LblPMode" Runat="server">Pay Mode</asp:label></td>
					<td>&nbsp;<asp:dropdownlist id="CmbPMode" Width="182" Runat="server"></asp:dropdownlist>
					</td>
				</tr>
				<tr height="5">
					<td colSpan="4"></td>
				</tr>
				<TR height="15">
					<TD style="MARGIN-LEFT: 15px; MARGIN-RIGHT: 15px; BORDER-BOTTOM: #993300 thin groove; HEIGHT: 1px"
						colSpan="4"></TD>
				</TR>
				<tr height="5">
					<td colSpan="4"></td>
				</tr>
				<tr>
					<td></td>
					<td align="right" colSpan="3">&nbsp;
						<asp:button id="BtnSave" Width="75" Runat="Server" Text="Save"></asp:button>&nbsp;
						<asp:button id="BtnCancel" Width="75" Runat="Server" Text="Cancel"></asp:button>&nbsp;
						<asp:button id="BtnDelete" Width="75" Runat="Server" Text="Delete"></asp:button>&nbsp;
						<asp:button id="BtnClose" Width="75" Runat="Server" Text="Close"></asp:button>&nbsp;
					</td>
				</tr>
				<tr height="5">
					<td colSpan="4"></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
