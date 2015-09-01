<%@ Page Language="vb" AutoEventWireup="false" Codebehind="UploadImage.aspx.vb" Inherits="eHRMS.Net.UploadImage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>UploadImage</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bgColor="#e1e4eb" leftMargin="5" topMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="310" height="140" border="0">
				<tr height="25">
					<td colspan="2">
						<asp:label id="LblMsg" runat="server" ForeColor="Red" Font-Size="11px"></asp:label></td>
				</tr>
				<tr height="25">
					<td><asp:label id="lblEmp_Code" runat="server" Width="100%" CssClass="Header3"></asp:label></td>
					<td align="right" rowspan="2"><asp:image id="ImgEmp" runat="server" Width="60px"></asp:image></td>
				</tr>
				<tr height="25">
					<td><asp:label id="lblUpload" runat="server" Width="100%">Select File</asp:label></td>
				</tr>
				<TR height="25">
					<td align="right" colspan="2">
						<INPUT id="FileName" style="WIDTH: 100%" type="file" name="File1" runat="server"></td>
				</TR>
				<tr height="40">
					<td align="right" colspan="2">
						<asp:button id="cmdSet" runat="server" Width="75px" Text="Set Image"></asp:button>&nbsp;&nbsp;
						<asp:button id="cmdClear" runat="server" Width="80px" Text="Clear Image"></asp:button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
