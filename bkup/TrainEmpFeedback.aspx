<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TrainEmpFeedback.aspx.vb" Inherits="eHRMS.Net.TrainEmpFeedback" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>EmpFeedback</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx -->
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="700" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Employee 
						Training Feedback....
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellspacing="0" cellpadding="0" width="700" border="1" rules="none" frame="border">
				<tr>
					<td colSpan="2"><asp:label id="LblErrMsg" runat="server" ForeColor="Red" Width="100%" Font-Size="8pt"></asp:label></td>
				</tr>
				<tr>
					<td>Training Session</td>
					<td><asp:dropdownlist id="cmbCode" runat="server" Width="200"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td colspan="2">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="2">
						<asp:datagrid id="GrdFeedback" Width="100%" AutoGenerateColumns="False" Runat="server">
							<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
							<HeaderStyle Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Top"
								BackColor="Gray"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="TrainFPId" Visible=False HeaderText="">
									<ItemStyle Width="0px"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="TrainFPDesc" HeaderText="The Training Program">
									<ItemStyle Width="300px"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn ItemStyle-Width="75" HeaderText="Strongly Disagree" HeaderStyle-HorizontalAlign="Center"
									ItemStyle-HorizontalAlign="Center">
									<ItemTemplate>
										<input type="checkbox" runat="server" ID="Checkbox1" NAME="Checkbox1">
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-Width="75" HeaderText="Disagree" HeaderStyle-HorizontalAlign="Center"
									ItemStyle-HorizontalAlign="Center">
									<ItemTemplate>
										<input type="checkbox" runat="server" ID="Checkbox2" NAME="Checkbox1">
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-Width="75" HeaderText="Somewhere Agree" HeaderStyle-HorizontalAlign="Center"
									ItemStyle-HorizontalAlign="Center">
									<ItemTemplate>
										<input type="checkbox" runat="server" ID="Checkbox3" NAME="Checkbox1">
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-Width="75" HeaderText="Agreed" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
									<ItemTemplate>
										<input type="checkbox" runat="server" ID="Checkbox4" NAME="Checkbox1">
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-Width="75" HeaderText="Strongly Agree" HeaderStyle-HorizontalAlign="Center"
									ItemStyle-HorizontalAlign="Center">
									<ItemTemplate>
										<input type="checkbox" runat="server" ID="Checkbox5" NAME="Checkbox1">
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle NextPageText="&amp;minus;&amp;gt;" Font-Underline="True" Font-Names="Verdana" PrevPageText="&amp;lt;&amp;minus;"
								ForeColor="#CB3908" BackColor="#E0E0E0" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
					</td>
				</tr>
				<tr><td colspan=2 bgcolor="darkgray" >&nbsp;</td></tr>
				<TR>
					<td align="right" colSpan="2"><asp:button id="cmdSave" runat="server" Width="80px" Text="Save"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:button id="cmdClose" runat="server" Width="80px" Text="Close"></asp:button></td>
				</TR>
			</table>
		</form>
	</body>
</HTML>
