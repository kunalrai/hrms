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
			<table borderColor="gray" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="1">
				<!--tr vAlign="bottom" height="75">
					<td colSpan="2">&nbsp;</td>
				</tr-->
				<tr>
					<td width="75%" align="center">
						<table height="100%" cellSpacing="0" cellPadding="0" width="100%">
							<tr>
								<td><img id="imglogin" runat="server">
								</td>
							</tr>
						</table>
					</td>
					<td width="25%">
						<table cellSpacing="0" align="right" cellPadding="0" width="30%" border="0">
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
									<table borderColor="gray" cellSpacing="0" bgcolor="#ffffff" cellPadding="2" rules="none"
										width="100%" align="center" background="" border="1" frame="border">
										<tr>
											<td align="center" colSpan="2"><asp:label id="LblMsg" runat="server" Visible="False" ForeColor="Red" Font-Size="11px" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="right" width="40%">Company :</td>
											<td align="right" width="60%"><asp:DropDownList id="cmbComp" runat="server" Width="100%" Height="100%" AutoPostBack="True"></asp:DropDownList></td>
										</tr>
										<tr>
											<td align="right" width="40%">ID :</td>
											<td align="right" width="60%"><asp:textbox id="txtUID" runat="server" Width="100px" CssClass="TextBox"></asp:textbox></td>
										</tr>
										<tr>
											<td align="right">Password :</td>
											<td align="right"><asp:textbox id="txtPWD" runat="server" Width="100px" CssClass="TextBox" TextMode="Password"></asp:textbox></td>
										</tr>
										<tr>
											<td style="HEIGHT: 20px" align="center">&nbsp;</td>
											<td align="right"><asp:button id="cmdGo" runat="server" Width="25px" CssClass="Butn" Text="Go"></asp:button></td>
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
