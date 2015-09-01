<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ImportFile.aspx.vb" Inherits="eHRMS.Net.ImportFile"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ImportFile</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<br>
			<br>
			<table borderColor="gray" cellSpacing="0" cellPadding="0" width="600" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Import 
						File&nbsp;
					</TD>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</table>
			<table borderColor="gray" cellSpacing="0" cellPadding="0" rules="none" width="600" align="center"
				border="1" frame="border">
				<tr>
					<td width="25%"></td>
					<td width="75%"></td>
				</tr>
				<tr>
					<td width="100%" colSpan="2"><asp:label id="LblErrMsg" runat="server" ForeColor="Red"></asp:label></td>
				</tr>
				<tr>
					<td style="HEIGHT: 20px">Import Description</td>
					<td style="HEIGHT: 20px"><asp:dropdownlist id="CmbImport" runat="server" Width="352px"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td>File Name</td>
					<td><INPUT id="ImFile" style="WIDTH: 432px; HEIGHT: 24px" type="file" size="52" name="File"
							runat="server"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 36px"></td>
					<td style="HEIGHT: 36px"></td>
				</tr>
				<tr>
					<td class="Header3" background="Images\headstripe.jpg" colSpan="2"><b></b></td>
				</tr>
				<tr>
					<td vAlign="top" colSpan="2"></td>
				</tr>
				<tr>
					<td colSpan="2">
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td align="right" width="70%"><asp:button id="CmdUpload" runat="server" Text="Upload" Width="75px"></asp:button></td>
								<td align="right" width="15%"><asp:button id="cmdSave" runat="server" Text="Save" Width="75px"></asp:button></td>
								<td align="right" width="15%"><asp:button id="cmdClose" runat="server" Text="Close" Width="75px"></asp:button></td>
							</tr>
						</TABLE>
					</td>
				</tr>
			</table>
			<asp:panel id="Panel1" style="Z-INDEX: 101; LEFT: 88px; POSITION: absolute; TOP: 312px" runat="server"
				Width="592px" Height="40px"></asp:panel></form>
	</body>
</HTML>
