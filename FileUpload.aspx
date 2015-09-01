<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FileUpload.aspx.vb" Inherits="eHRMS.Net.FileUpload"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FileUpload</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 240px; POSITION: absolute; TOP: 88px" cellSpacing="0"
				cellPadding="0" rules="none" width="500" align="center" border="1">
				<TR>
					<TD style="HEIGHT: 12px" width="25%"></TD>
					<TD style="HEIGHT: 12px" width="75%"></TD>
				</TR>
				<TR>
					<TD colSpan="2">
						<asp:label id="lblMsg" runat="server" ForeColor="Red" Visible="False" Font-Size="11px" Width="100%"></asp:label></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 16px">&nbsp;File Name</TD>
					<TD style="HEIGHT: 16px">
						<asp:dropdownlist id="CmbFile" runat="server" Width="150px"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD colSpan="2"></TD>
				</TR>
				<TR>
					<TD colSpan="2">&nbsp;</TD>
				</TR>
				<TR>
					<TD>&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:button id="cmdGenerate" runat="server" Text="Generate"></asp:button></TD>
					<TD align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" style="Z-INDEX: 104; LEFT: 240px; POSITION: absolute; TOP: 72px" cellSpacing="0"
				cellPadding="0" width="500" align="center" border="0">
				<TR>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="center" background="Images/TableMid.gif" height="19">File 
						Upload</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
