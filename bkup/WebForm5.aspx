<%@ Page Language="vb" AutoEventWireup="false" Codebehind="WebForm5.aspx.vb" Inherits="eHRMS.Net.WebForm5"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>WebForm5</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server" enctype="multipart/Form-data">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="30%" align="center" border="0">
				<TR>
					<TD>
						<P align="center">
							<asp:Label id="LblErrMsg" runat="server" ForeColor="Blue" Font-Bold="True" Font-Size="X-Small"
								Width="265px"></asp:Label></P>
					</TD>
				</TR>
				<TR>
					<TD>
						<P align="center"><INPUT id="inpFileUpload" type="file" runat="Server"></P>
					</TD>
				</TR>
				<TR>
					<TD></TD>
				</TR>
				<TR>
					<TD>
						<P align="center">
							<asp:Button id="BtnSaveImage" runat="server" Text="Upload Me !"></asp:Button></P>
					</TD>
				</TR>
				<TR>
					<TD></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
