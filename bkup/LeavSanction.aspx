<%@ Page Language="vb" AutoEventWireup="false" Codebehind="LeavSanction.aspx.vb" Inherits="eHRMS.Net.LeavSanction"%>
<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>LeavSanction</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="800" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Leave 
						Applications From Subordinates ....
					</TD>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" rules="none" width="800" align="left" border="1"
				frame="border">
				<tr borderColor="white">
					<td colSpan="6"><asp:label id="lblMsg" runat="server" Width="100%" ForeColor="Red" Font-Size="11px" Visible="False"></asp:label></td>
				</tr>
				<tr>
					<td colSpan="7"><asp:datagrid id="grdLeavPending" runat="server" Width="100%" AllowSorting="True" AllowPaging="True"
							AutoGenerateColumns="False" PagerStyle-Mode="NumericPages">
							<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
							<HeaderStyle Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Top"
								BackColor="Gray"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Select">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="5px"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox ID="ChkSelect" Runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Emp_Name" HeaderText="Name">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Emp_Code" HeaderText="Code">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="AppDate" DataFormatString="{0:dd/MMM/yyyy}" HeaderText="Application Date">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="130px"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="LVDAYS" HeaderText="Days">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="AtDate">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="0px"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="AAtDate" HeaderText="Leave Date">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="LVTYPE">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="0px"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="LVDESC" HeaderText="Leave Type">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" Width="250px"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Status">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="5px"></ItemStyle>
									<ItemTemplate>
										<asp:DropDownList Runat="server" Width="100" SelectedIndex='<%# DataBinder.Eval(Container.DataItem, "SIndex") %>'>
											<asp:ListItem Selected="True" Value="A">Unprocessed</asp:ListItem>
											<asp:ListItem Value="S">Sanctioned</asp:ListItem>
											<asp:ListItem Value="R">Rejected</asp:ListItem>
										</asp:DropDownList>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Without Pay">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="5px"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox ID="ChkIsAdvance" Runat=server Enabled='<%# DataBinder.Eval(Container.DataItem, "Advance") %>'>
										</asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Leave Records">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="5px"></ItemStyle>
									<ItemTemplate>
										<a href="EmpLeavStatus.aspx?Code=<%# DataBinder.Eval(Container.DataItem,"Emp_Code") %>&Name=<%# DataBinder.Eval(Container.DataItem,"EMp_Name") %>" target=<%# DataBinder.Eval(Container.DataItem,"Emp_Code") %>>Status</a>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td colSpan="7">&nbsp;</td>
				</tr>
				<tr>
					<td class="Header3" width="15%"><asp:dropdownlist id="cmbJuniors" runat="server" Width="100%"></asp:dropdownlist></td>
					<td class="Header3" width="15%"><asp:dropdownlist id="cmbStatus" runat="server" Width="100%"></asp:dropdownlist></td>
					<td class="Header3" width="10%">Filter on
					</td>
					<td class="Header3" width="14%"><asp:dropdownlist id="cmbFilter" runat="server" Width="100%">
							<asp:ListItem Value="L">Leave  Date</asp:ListItem>
							<asp:ListItem Value="A" Selected="True">Application Date</asp:ListItem>
						</asp:dropdownlist></td>
					<td class="Header3" width="18%"><cc1:dtp id="dtpFromDate" runat="server" Width="125px" TextBoxPostBack="False"></cc1:dtp></td>
					<td class="Header3" width="18%"><cc1:dtp id="dtpToDate" runat="server" Width="125px" TextBoxPostBack="False"></cc1:dtp></td>
					<td class="Header3" width="10%"><asp:button id="cmdShow" runat="server" Width="60px" Text="Show"></asp:button></td>
				</tr>
				<tr>
					<td colSpan="7">&nbsp;</td>
				</tr>
				<tr>
					<td class="Header3" align="right" colSpan="6"><asp:button id="cmdSave" runat="server" Width="60px" Text="Save"></asp:button></td>
					<td class="Header3" align="center"><asp:button id="cmdClose" runat="server" Width="60px" Text="Close"></asp:button></td>
				</tr>
				<TR>
					<TD colSpan="7"><asp:label id="lblAlready" runat="server" Width="784px" ForeColor="#003366" Font-Size="11px"
							Visible="False" Height="5px"></asp:label></TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
