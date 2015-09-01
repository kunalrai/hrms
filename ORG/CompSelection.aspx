<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CompSelection.aspx.vb" Inherits="eHRMS.Net.CompSel" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Divergent Infosoft Technologies Pvt. Ltd.--</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table borderColor="gray" height="475" align="center" cellSpacing="0" cellPadding="0" width="790" border="1"
				BACKGROUND="iMAGES\OrgBackGround.GIF">
				<tr>
					<td vAlign="middle" align="left">
						<TABLE width="300" cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
								</TD>
								<TD class="header3" noWrap align="center" width="95%" background="Images/TableMid.gif"
									height="19">Company Selection ....
								</TD>
								<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
								</TD>
							</tr>
							<tr>
								<td colSpan="3">
									<table borderColor="gray" cellSpacing="0" cellPadding="2" rules="none" width="100%" align="center"
										border="1" frame="border">
										<tr vAlign="top" height="100%">
											<td><asp:listbox id="LstComp" runat="server" AutoPostBack="True" Width="100%" Font-Bold="True" Font-Names="Verdana"
													Font-Overline="True" ForeColor="#003366" Rows="15"></asp:listbox></td>
										</tr>
										<tr>
											<td align="right"><asp:button id="CmdOk" runat="server" Width="24px" Font-Bold="True" Font-Names="Wingdings" Font-Size="Medium"
													CssClass="Butn" Height="24px" Text="ü"></asp:button>&nbsp;
												<asp:button id="CmdExit" runat="server" Width="24px" Font-Bold="True" Font-Names="Wingdings 2"
													Font-Size="Medium" CssClass="Butn" Height="24px" Text="Ð"></asp:button></td>
										</tr>
										<tr>
											<td><asp:label id="LblMsg" runat="server" Width="100%" ForeColor="Red" Font-Size="11px"></asp:label></td>
										</tr>
									</table>
								</td>
							</tr>
						</TABLE>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
