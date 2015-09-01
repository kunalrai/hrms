<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ResumeStatus.aspx.vb" Inherits="eHRMS.Net.ResumeStatus" %>
<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Resume Status</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
		</script>
	</HEAD>
	<body topMargin="5" ms_positioning="GridLayout">
		<form id="Form2" method="post" runat="server">
			<!--#include file=MenuBars.aspx --><br>
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="700" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Resume 
						Status....
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" rules="none" width="700" border="1" frame="border">
				<TBODY>
					<tr>
						<td width="15%"></td>
						<td width="35"></td>
						<td width="15%"></td>
						<td width="35%"></td>
					</tr>
					<tr>
						<td colSpan="4"><asp:label id="LblErrMsg" runat="server" Width="100%" ForeColor="Red"></asp:label></td>
					</tr>
					<tr>
						<td colSpan="4">
							<TABLE id="TabgrdStatus" cellSpacing="0" cellPadding="2" width="700" border="0" runat="server">
								<tr>
									<td><asp:datagrid id="grdStatus" runat="server" Width="100%" AutoGenerateColumns="false" GridLines="Both">
											<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
											<HeaderStyle HorizontalAlign="Left" CssClass="Header3"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="Select">
													<ItemStyle HorizontalAlign="Left" Width="50px"></ItemStyle>
													<ItemTemplate>
														<asp:CheckBox id="ChkSelect" ForeColor="#003366" Width="30px" Enabled="true" Checked="false" Runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn Visible="False" DataField="Res_Code" HeaderText="Reference No.">
													<ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Res_No" HeaderText="Reference No.">
													<ItemStyle HorizontalAlign="Left" Width="175px"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="EName" HeaderText="Name">
													<ItemStyle HorizontalAlign="Left" Width="165px"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Dept_Name" HeaderText="Department">
													<ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Dsg_Name" HeaderText="Designation">
													<ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="SalExpect" HeaderText="Exp.Salary">
													<ItemStyle HorizontalAlign="Left" Width="80px"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="TotalExp" HeaderText="Total Exp." DataFormatString="{0:0.00}">
													<ItemStyle HorizontalAlign="Left" Width="80px"></ItemStyle>
												</asp:BoundColumn>
											</Columns>
										</asp:datagrid></td>
								</tr>
							</TABLE>
					<tr>
						<td>&nbsp;</td>
					</tr>
					<tr>
						<td class="Header3">Current Status</td>
						<td class="Header3"><asp:dropdownlist id="cmbCrntStatus" runat="server" Width="150px" AutoPostBack="True"></asp:dropdownlist>
						<td class="Header3">Change Status</td>
						<td class="Header3"><asp:dropdownlist id="cmbCngStatus" runat="server" Width="150px"></asp:dropdownlist></td>
					</tr>
					<tr>
						<td>Offer Date</td>
						<td><input id="ChkOfferDate" onclick="Val(this.id)" type="checkbox" name="ChkOfferDate" runat="server"><cc1:dtpcombo id="dtpOfferDate" runat="server" Width="152px" ToolTip="Offer Date" DateValue="2006-03-24"
								Enabled="False"></cc1:dtpcombo></td>
						<td>Ready To Join Date</td>
						<td><input id="ChkRDOJ" onclick="Val(this.id)" type="checkbox" name="ChkRDOJ" runat="server"><cc1:dtpcombo id="dtpRDOJ" runat="server" Width="152px" ToolTip="Ready To Join Date" DateValue="2006-03-24"
								Enabled="False"></cc1:dtpcombo></td>
					</tr>
					<tr>
						<td>&nbsp;</td>
					</tr>
					<tr>
						<td align="right" colSpan="4"><asp:button id="btnStatusSave" runat="server" Width="75px" Text="Save"></asp:button>&nbsp;
							<asp:button id="btnStatusClose" runat="server" Width="75px" Text="Close"></asp:button></td>
					</tr>
				</TBODY>
			</table>
		</form>
		</TD></TR></TBODY></TABLE></FORM>
	</body>
</HTML>
