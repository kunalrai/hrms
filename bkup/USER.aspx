<%@ Page Language="vb" AutoEventWireup="false" Codebehind="USER.aspx.vb" Inherits="eHRMS.Net.USER"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>USER</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table cellpadding="0" align="center" border="1" cellspacing="0" width="400">
				<tr>
					<td>
						<asp:Label id="Label1" runat="server">Code</asp:Label></td>
					<td>
						<asp:TextBox id="TxtCode" runat="server" AutoPostBack="True"></asp:TextBox></td>
					<td></td>
				</tr>
				<TR>
					<TD>
						<asp:Label id="Label2" runat="server">Name</asp:Label></TD>
					<TD>
						<asp:TextBox id="TxtName" runat="server" Width="241px"></asp:TextBox></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD colspan="3">
						<asp:Button ID="Cmduser" Runat="server" Width="100px" Text="Create User"></asp:Button>
						<asp:Label id="lBLmSG" runat="server" ForeColor="Red">Label</asp:Label>
					</TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
