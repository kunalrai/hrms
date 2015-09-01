<%@ Page Language="vb" AutoEventWireup="false" Codebehind="LogOut.aspx.vb" Inherits="eHRMS.Net.LogOut"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>LogOut</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<script>
		function winClose()
			{
			window.close(); 
			}
		function LogAgain()
			{
			window.location.href = "login.aspx";
			}	
		</script>
		<script language =vbscript >
			sub LoadOpen()
				window.open "Blank.aspx","Footer" 
			End Sub
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table width="100%">
				<tr bgColor="#e7e8e9">
					<td width="50%" align="right">
						<FONT face="Verdana, Arial" size="2"><a style="CURSOR: hand" onClick="LogAgain()">Login</a>
							| <a onClick="winClose()">Close Window</a> </FONT>
					</td>
				</tr>
				<tr>
					<td colspan="2"><HR width="100%" color="navy" noShade SIZE="2">
					</td>
				</tr>
			</table>
			<asp:label id="lblTitle" style="Z-INDEX: 101; POSITION: relative; TOP: 40%; TEXT-ALIGN: center"
				runat="server" Font-Bold="True" Font-Size="Small" Width="100%" Height="16px" Font-Italic="True"
				CssClass="Header3">Divergent Infosoft Technologies Pvt. Ltd.</asp:label></form>
	</body>
</HTML>
