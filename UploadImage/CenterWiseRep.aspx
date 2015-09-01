<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CenterWiseRep.aspx.vb" Inherits="Billing.CenterWiseRep"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Collection Center Wise Report</title>
		<LINK href="Styles.css" type="text/css" rel="Stylesheet">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body bgColor="#f5f5f5" MS_POSITIONING="GridLayout">
		<BR>
		<BR>
		<BR>
		<BR>
		<BR>
		<form id="FrmCenterwise" name="FrmCenterwise" method="post" runat="server">
			<TABLE borderColor="#98a9ca" cellSpacing="0" cellPadding="0" width="350" align="center"
				border="1">
				<tr>
					<td>
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR bgColor="#98a9ca">
								<td>Collection Center Wise Report</td>
								<td align="right"><input id="cmdX" style="WIDTH: 20px" type="button" value="X" name="cmdX" runat="server"></td>
							</TR>
						</TABLE>
					</td>
				<tr>
					<td>
						<TABLE style="WIDTH: 95%; HEIGHT: 100px" cellSpacing="0" cellPadding="0" width="100%" align="center"
							border="0">
							<TR align="center">
								<td width="50%">Collection Center</td>
								<td width="50%"><asp:dropdownlist id="CmbColCenter" runat="server" Height="56px" Width="104px"></asp:dropdownlist></td>
							</TR>
							<TR align="center">
								<td>From</td>
								<td>To</td>
							</TR>
							<TR align="center">
								<td style="HEIGHT: 16px"><asp:dropdownlist id="FromDD" runat="server" Height="56px" Width="40px"></asp:dropdownlist><asp:dropdownlist id="FromMM" runat="server" Height="56px" Width="45px"></asp:dropdownlist><asp:dropdownlist id="FromYY" runat="server" Height="56px" Width="52px"></asp:dropdownlist></td>
								<td style="HEIGHT: 16px"><asp:dropdownlist id="ToDD" runat="server" Height="56px" Width="40px"></asp:dropdownlist><asp:dropdownlist id="ToMM" runat="server" Height="56px" Width="45px"></asp:dropdownlist><asp:dropdownlist id="ToYY" runat="server" Height="56px" Width="52px"></asp:dropdownlist></td>
							</TR>
							<TR align="center">
								<td>Payment Mode</td>
								<td><asp:dropdownlist id="CmbPayMode" runat="server" Height="56px" Width="104px">
										<asp:ListItem Value="All">All</asp:ListItem>
										<asp:ListItem Value="Cash">Cash</asp:ListItem>
										<asp:ListItem Value="Cheque">Cheque</asp:ListItem>
									</asp:dropdownlist></td>
							</TR>
						</TABLE>
					</td>
				<tr>
					<td>
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR bgColor="#98a9ca">
								<td align="right"><input id="CmdPrint" style="WIDTH: 80px" type="button" value="Print" name="CmdPrint" runat="server">
									<input id="CmdPrev" style="WIDTH: 80px" type="button" value="Preview" name="CmdPrev" runat="server">
									<input id="CmdExit" style="WIDTH: 80px" type="button" value="Exit" name="CmdExit" runat="server">
								</td>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
