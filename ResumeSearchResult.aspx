<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ResumeSearchResult.aspx.vb" Inherits="eHRMS.Net.ResumeSearchResult"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ResumeSearchResult</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js">
			function Back()
				{
					window.history.back;  
				}
		</SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx --><br>
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="80%" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Resume 
						Search Result....
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" rules="none" width="80%" border="1" frame="border">
				<tr>
					<td width="15%"></td>
					<td width="19%"></td>
					<td width="15%"></td>
					<td width="18%"></td>
					<td width="15%"></td>
					<td width="18%"></td>
				</tr>
				<tr>
					<td colspan="6"><asp:label id="lblMsg" runat="server" Visible="False" Font-Size="11px" ForeColor="Red" Width="100%"></asp:label></td>
				</tr>
				<tr>
					<td colSpan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">
						<table cellpadding="0" cellspacing="0" width="100%" border="1" frame="border" rules="none">
							<tr>
								<td width="13%"></td>
								<td width="12%"></td>
								<td width="13%"></td>
								<td width="12%"></td>
								<td width="13%"></td>
								<td width="12%"></td>
								<td width="13%"></td>
								<td width="12%"></td>
							</tr>
							<tr>
								<td colSpan="8" class="Header3">Modify Your Search</td>
							</tr>
							<tr>
								<td>Text To Search</td>
								<td colSpan="7"><asp:textbox id="TxtSearch" runat="server" Width="100%" CssClass="textbox"></asp:textbox></td>
							</tr>
							<tr>
								<td>Exp.From</td>
								<td><asp:dropdownlist id="cmbExpFrom" runat="server" Width="100%"></asp:dropdownlist></td>
								<td>Exp.To</td>
								<td><asp:dropdownlist id="CmbExpTo" runat="server" Width="100%"></asp:dropdownlist></td>
								<td>Sort By</td>
								<td><asp:dropdownlist id="cmbSort" runat="server" Width="100%">
										<asp:ListItem Value="0" Selected="True">Resume ID</asp:ListItem>
										<asp:ListItem Value="1">Name</asp:ListItem>
										<asp:ListItem Value="2">Experience</asp:ListItem>
									</asp:dropdownlist></td>
								<td>&nbsp;</td>	
								<td align="center"><asp:linkbutton id="BtnSearch" runat="server" Width="80px" Font-Bold="True" BorderStyle="Outset">Search</asp:linkbutton></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colSpan="6"></td>
				</tr>
				<tr>
					<td colspan="6"><asp:datagrid id="GrdResumes" runat="server" Width="100%" AutoGenerateColumns="False" PagerStyle-Mode="NumericPages"
							AllowPaging="True" PageSize="25">
							<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
							<HeaderStyle Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Top"
								BackColor="Gray"></HeaderStyle>
							<Columns>
								<asp:ButtonColumn Text="Show" HeaderText="Select" CommandName="Select">
									<ItemStyle Width="3%"></ItemStyle>
								</asp:ButtonColumn>
								<asp:BoundColumn DataField="Res_No" HeaderText="Resume ID">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" Width="12%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ResName" HeaderText="Name">
									<ItemStyle HorizontalAlign="Left" Width="12%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ResExp" HeaderText="Tot Exp.">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="4%"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Qualification">
									<ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
									<ItemTemplate>
										<asp:Label ID="LblQual" Runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Skills">
									<ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
									<ItemTemplate>
										<asp:Label ID="LblSkills" Runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
