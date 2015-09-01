<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Login.aspx.vb" Inherits="eHRMS.Net.Login"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Login_aspx</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table borderColor="gray" cellSpacing="0" cellPadding="0" width="100%" border="1" height="100%">
				<!--tr vAlign="bottom" height="75">
					<td colSpan="2">&nbsp;</td>
				</tr-->
				<tr>
					<td width="75%" align=center>
						<table align=center width="100%" height="400px" cellpadding="0" cellspacing="0" background="Images\images2.1.jpg">
							<tr>
								<td></td>
							</tr>
						</table>
					</td>
					<td width="25%">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
								</TD>
								<TD class="header3" noWrap align="center" width="95%" background="Images/TableMid.gif"
									height="19">Login
								</TD>
								<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
								</TD>
							</tr>
							<tr>
								<td colSpan="3">
									<table borderColor="gray" cellSpacing="0" cellPadding="2" rules="none" width="100%" align="center"
										border="1" frame="border">
										<tr>
											<td align="center" colSpan="2"><asp:label id="LblMsg" runat="server" Width="100%" Font-Size="11px" ForeColor="Red" Visible="False"></asp:label></td>
										</tr>
										<tr>
											<td align="right" width="40%">ID :</td>
											<td align="right" width="60%"><asp:textbox id="txtUID" runat="server" CssClass="TextBox" Width="100px"></asp:textbox></td>
										</tr>
										<tr>
											<td align="right">Password :</td>
											<td align="right"><asp:textbox id="txtPWD" runat="server" CssClass="TextBox" Width="100px" TextMode="Password"></asp:textbox></td>
										</tr>
										<tr>
											<td style="HEIGHT: 20px" align="center">&nbsp;</td>
											<td align="right"><asp:button id="cmdGo" runat="server" CssClass="Butn" Width="25px" Text="Go"></asp:button></td>
										</tr>
										<tr>
											<td align="center" colSpan="2">&nbsp;</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
