<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TreeMenu.aspx.vb" Inherits="eHRMS.Net.TreeMenu" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>TreeMenu</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<script language="javascript">
			function ShowMenu(MenuType)
				{
				Menu = new String(MenuType)
				if (document.getElementById('Tbl' + Menu).style.display == "none")
					{
					document.getElementById('Tbl' + Menu).style.display = "block";
					document.getElementById('Img' + Menu).src = "images/Minus.gif";
					}
				else
					{
					document.getElementById('Tbl' + Menu).style.display = "none";
					document.getElementById('Img' + Menu).src = "images/Plus.gif";
					}
				}							
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgcolor="whitesmoke" leftmargin="0">
		<form id="MenuList" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" rules="none" width="100%" border="0" frame="border">
				<tr>
					<td align="left" style="HEIGHT: 20px">
						<asp:Label id="LblUser" runat="server" Width="100%" Height="20px"></asp:Label>
					</td>
				</tr>
			</table>
			<table id="TblModules" cellSpacing="0" cellPadding="0" rules="none" border="0" frame="border"
				runat="server">
			</table>
			<!--table style="cellSpacing: '0'" cellPadding="0" rules="none" width="100%" border="0" frame="border">
				<tr>
					<td align="center" valign="bottom" style="HEIGHT: 25px">
						<a href="FrmAboutUs.aspx" target="main"><b>About us...</b></a>
					</td>
				</tr>
			</table-->
			<!--table style="cellSpacing: '0'" cellPadding="0" rules="none" width="100%" border="0" frame="border">
				<tr>
					<td align="center" valign="bottom" style="HEIGHT: 25px"><a href="FrmLogin.aspx" target="main_1"><font color="crimson"><b>LOGOUT</b></font></a></td>
				</tr>
			</table-->
		</form>
	</body>
</HTML>
