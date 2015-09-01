<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Qualification.aspx.vb" Inherits="eHRMS.Net.Qualification" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Qualification</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="400" border="1" frame="border" rules="rows">
				<tr>
					<td class="Header3" align="right"><asp:button id="cmdSave" runat="server" Width="80px" Text="Save"></asp:button></td>
					<td class="Header3" align="right"><INPUT onclick="window.close();" style="WIDTH: 80px" id="cmdClose" type="button" value="Close"
							name="cmdClose"></td>
				</tr>
				<tr>
					<td colSpan="2"><asp:label id="LblHeader" runat="server" Width="100%" ForeColor="#003366"></asp:label></td>
				</tr>
				<tr>
					<td colSpan="2"><asp:label id="LblErrMsg" runat="server" Width="100%" ForeColor="Red"></asp:label></td>
				</tr>
				<tr>
					<td colspan="2">
						<asp:datagrid id="GrdQual" runat="server" Width="100%" AutoGenerateColumns="False">
							<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
							<HeaderStyle HorizontalAlign="Center" CssClass="Header3"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Select">
									<ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox ID="ChkSelect" Runat="server" Width="100%"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Qual_Code" ItemStyle-HorizontalAlign="Center" HeaderText="Code" ItemStyle-Width="25%"></asp:BoundColumn>
								<asp:BoundColumn DataField="Qual_Name" HeaderText="Name" ItemStyle-Width="65%"></asp:BoundColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
