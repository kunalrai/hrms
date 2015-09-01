<%@ Page Language="vb" AutoEventWireup="false" Codebehind="AttendenceUpload.aspx.vb" Inherits="eHRMS.Net.AttendenceUpload"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>AttendenceUpload</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" style="Z-INDEX: 101; LEFT: 152px; POSITION: absolute; TOP: 104px" borderColor="gray"
				cellSpacing="0" cellPadding="0" width="600" align="center" border="0">
				<TR>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Import 
						File&nbsp;
					</TD>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</TR>
			</TABLE>
			<asp:panel id="Panel1" style="Z-INDEX: 103; LEFT: 160px; POSITION: absolute; TOP: 392px" runat="server"
				Width="592px" Height="40px"></asp:panel>
			<TABLE id="Table3" style="Z-INDEX: 102; LEFT: 152px; POSITION: absolute; TOP: 128px" borderColor="gray"
				cellSpacing="0" cellPadding="0" rules="none" width="600" align="center" border="1"
				frame="border">
				<TR>
					<TD width="25%"></TD>
					<TD width="75%"></TD>
				</TR>
				<TR>
					<TD width="100%" colSpan="2"><asp:label id="LblErrMsg" runat="server" ForeColor="Red"></asp:label></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 20px">Import Description</TD>
					<TD style="HEIGHT: 20px"><asp:dropdownlist id="DropDownList1" runat="server"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD>File Name</TD>
					<TD><INPUT id="ImFile" style="WIDTH: 432px; HEIGHT: 24px" type="file" size="52" name="File"
							runat="server"></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 36px"></TD>
					<TD style="HEIGHT: 36px"></TD>
				</TR>
				<TR>
					<TD class="Header3" background="Images\headstripe.jpg" colSpan="2"><B></B></TD>
				</TR>
				<TR>
					<TD vAlign="top" colSpan="2"></TD>
				</TR>
				<TR>
					<TD colSpan="2">
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
							<TR>
								<TD align="right" width="70%"><asp:button id="CmdUpload" runat="server" Width="75px" Text="Upload"></asp:button></TD>
								<TD align="right" width="15%"><asp:button id="cmdSave" runat="server" Width="75px" Text="Save"></asp:button></TD>
								<TD align="right" width="15%"><asp:button id="cmdClose" runat="server" Width="75px" Text="Close"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
