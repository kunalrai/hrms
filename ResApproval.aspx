<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ResApproval.aspx.vb" Inherits="eHRMS.Net.ResApproval"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ResApproval</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
	</HEAD>
	<body topMargin="5" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx --><br>
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="600" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif"></TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Resume 
						Approval ....</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" rules="none" align="center" width="600" border="1" frame="border">
				<tr>
					<td width="20%"></td>
					<td width="30"></td>
					<td width="20%"></td>
					<td width="30%"></td>
				</tr>
				<tr>
					<td colSpan="5"><asp:label id="LblErrMsg" runat="server" ForeColor="Red" Width="100%"></asp:label></td>
				</tr>
				<tr>
					<td>Requisition Id</td>
					<td><asp:dropdownlist id="CmbReqId" Width="100%" AutoPostBack="True" Runat="server"></asp:dropdownlist></td>
					<td>&nbsp;Status</td>
					<td><asp:dropdownlist id="CmbStsName" Width="100%" Runat="server"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td>Description</td>
					<td colspan="3"><asp:textbox id="TxtDesc" Width="180" Runat="server" AutoPostBack="True" ReadOnly="True" CssClass="TextBox"
							ForeColor="#003366"></asp:textbox></td>
				</tr>
				<tr>
					<td>Department</td>
					<td><asp:dropdownlist id="CmbDept" Width="100%" Runat="server"></asp:dropdownlist></td>
					<td colspan="2" align="right">
						<asp:Button ID="btnShow" Runat="server" Width="100px" Text="Show Records"></asp:Button>
					</td>
				</tr>
				<tr>
					<td colSpan="4">
						<TABLE id="TabgrdResume" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td>
									<div style="OVERFLOW: auto; WIDTH: 600px; BORDER-TOP-STYLE: solid; BORDER-RIGHT-STYLE: solid; BORDER-LEFT-STYLE: solid; HEIGHT: 200px; BORDER-BOTTOM-STYLE: solid"><asp:datagrid id="grdResume" runat="server" Width="1100" AutoGenerateColumns="false">
											<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
											<HeaderStyle Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Top"
												BackColor="Gray"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="Select">
													<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
													<ItemTemplate>
														<asp:CheckBox ID="chkSelect" Runat="server" Width="100%" Checked="False"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="Res_Code" Visible="False" HeaderText="">
													<ItemStyle HorizontalAlign="Left" Width="0px"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="ResNo" HeaderText="Resume ID">
													<ItemStyle HorizontalAlign="Left" Width="175px"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="ResName" HeaderText="Name">
													<ItemStyle HorizontalAlign="Left" Width="175px"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="DsgName" HeaderText="Designation">
													<ItemStyle HorizontalAlign="Left" Width="175px"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="DeptName" HeaderText="Department">
													<ItemStyle HorizontalAlign="Left" Width="175px"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Skills" HeaderText="Skills">
													<ItemStyle HorizontalAlign="Left" Width="175px"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Status" HeaderText="Status">
													<ItemStyle HorizontalAlign="Left" Width="175px"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="SalExpect" HeaderText="Expected Salary">
													<ItemStyle HorizontalAlign="Left" Width="175px"></ItemStyle>
												</asp:BoundColumn>
											</Columns>
										</asp:datagrid></div>
								</td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td colSpan="3"></td>
					<td align="right"><asp:button id="btnSave" Width="75px" Runat="server" Text="Save"></asp:button>&nbsp;
						<asp:button id="btnClose" Width="75px" Runat="server" Text="Close"></asp:button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
