<%@ Page Language="vb" AutoEventWireup="false" Codebehind="EmpLeavStatus.aspx.vb" Inherits="eHRMS.Net.EmpLeavStatus"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>EmpLeavStatus</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table cellpadding="0" cellspacing="0" width="600">
				<tr>
					<td width="30%"></td>
					<td width="70%"></td>
				</tr>
				<tr>
					<td align="center" colspan="2" class="Header3">Leave Status</td>
				</tr>
				<tr>
					<td colspan="2">&nbsp;</td>
				</tr>
				<tr>
					<td>Employee Code</td>
					<td><asp:Label Width="100%" Runat="server" ID="LblCode"></asp:Label></td>
				</tr>
				<tr>
					<td>Employee Name</td>
					<td><asp:Label Width="100%" Runat="server" ID="LblName"></asp:Label></td>
				</tr>
				<tr>
					<td colspan="2">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="2">
						<asp:datagrid id="grdLeavBal" runat="server" Width="100%" AutoGenerateColumns="False">
							<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
							<HeaderStyle Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Top"
								BackColor="Gray"></HeaderStyle>
							<Columns>
								<asp:BoundColumn ItemStyle-Width="10%" DataField="levyear" HeaderText="Leave Year" HeaderStyle-HorizontalAlign="Center"
									ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
								<asp:BoundColumn ItemStyle-Width="25%" DataField="LvDesc" HeaderText="Leave Type" HeaderStyle-HorizontalAlign="Center"
									ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
								<asp:BoundColumn ItemStyle-Width="15%" DataField="Opening" HeaderText="Opening" HeaderStyle-HorizontalAlign="Center"
									ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
								<asp:BoundColumn ItemStyle-Width="15%" DataField="Earned" HeaderText="Earned" HeaderStyle-HorizontalAlign="Center"
									ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
								<asp:BoundColumn ItemStyle-Width="15%" DataField="Availed" HeaderText="Availed" HeaderStyle-HorizontalAlign="Center"
									ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
								<asp:BoundColumn ItemStyle-Width="15%" DataField="Balance" HeaderText="Balance" HeaderStyle-HorizontalAlign="Center"
									ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
							</Columns>
						</asp:datagrid>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
