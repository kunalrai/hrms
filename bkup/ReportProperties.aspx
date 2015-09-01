<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ReportProperties.aspx.vb" Inherits="eHRMS.Net.ReportProperties"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Report Properties</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<script language="vbscript">
			Sub ClickSave()
				document.getElementById("cmdSave").click
				window.opener.document.getElementById("cmdRefresh").click  
				window.close() 
			End Sub
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="500" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Report 
						Properties ....
					</TD>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellpadding="0" cellspacing="0" border="1" width="500" frame="border" rules="none">
				<tr>
					<td width="35%"></td>
					<td width="15%"></td>
					<td width="25%"></td>
					<td width="25%"></td>
				</tr>
				<tr>
					<td colspan="4"><asp:label id="LblMsg" runat="server" Width="100%" ForeColor="Red" Font-Size="11px"></asp:label></td>
				</tr>
				<tr>
					<td>File Name</td>
					<td colspan="3">
						<asp:TextBox id="TxtFileName" runat="server" CssClass="textbox" Enabled="False" ForeColor="#003366"></asp:TextBox></td>
				</tr>
				<tr>
					<td>Report Name</td>
					<td colspan="3">
						<asp:TextBox id="TxtRptName" runat="server" Width="375" CssClass="textbox" ForeColor="#003366"></asp:TextBox></td>
				</tr>
				<tr>
					<td valign="top">Default Filter</td>
					<td colspan="3">
						<asp:TextBox id="TxtDefFilter" Width="375" runat="server" TextMode="MultiLine" Rows="4" ForeColor="#003366"></asp:TextBox></td>
				</tr>
				<tr>
					<td valign="top">Sorting Order</td>
					<td colspan="3">
						<asp:TextBox id="TxtSortOrder" Width="375" runat="server" TextMode="MultiLine" Rows="4" ForeColor="#003366"></asp:TextBox></td>
				</tr>
				<tr>
					<td>No. Of Group</td>
					<td><asp:TextBox id="TxtNoGroup" Width="100%" runat="server" CssClass="textbox" ForeColor="#003366"></asp:TextBox></td>
					<td>&nbsp;</td>
					<td align="right"><INPUT id="mdSave" type="button" style="WIDTH: 80px" onclick="ClickSave()" value="Save">
					</td>
				</tr>
			</table>
			<asp:Button id="cmdSave" runat="server" Width="0px" style="Z-INDEX: 101; LEFT: 520px; POSITION: absolute; TOP: 16px"></asp:Button>
		</form>
	</body>
</HTML>
