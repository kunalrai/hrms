<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TestMast.aspx.vb" Inherits="eHRMS.Net.TestMast"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>TestMast</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="coolmenus4.js"></script>
		<script language="javascript" src="Common.js"></script>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<br>
			<br>
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="420" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Test 
						Master ....
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table style="WIDTH: 420px; HEIGHT: 160px" cellSpacing="0" cellPadding="0" rules="none"
				width="500" align="center" border="1" frame="border">
				<tr>
					<td style="WIDTH: 140px" width="140"></td>
					<td width="10%"></td>
					<td style="WIDTH: 180px" width="180"></td>
					<td width="180"></td>
				</tr>
				<tr>
					<td colSpan="4"><asp:label id="LblErrMsg" runat="server" ForeColor="Red" Width="100%"></asp:label></td>
				</tr>
				<tr>
					<td style="WIDTH: 100px" width="100" colSpan="1">&nbsp;Test Code</td>
					<td colSpan="3"><asp:textbox id="TxtCode" runat="server" CssClass="TextBox" AutoPostBack="True" ForeColor="#003366"></asp:textbox><asp:imagebutton id="btnNew" Width="18px" Runat="server" ImageAlign="AbsMiddle" ImageUrl="Images\NewFile.ico"
							Height="19px"></asp:imagebutton><asp:imagebutton id="btnList" Width="18px" Runat="server" ImageAlign="AbsMiddle" ImageUrl="Images\Find.gif"
							Height="19px"></asp:imagebutton><asp:dropdownlist id="cmbCode" runat="server" Width="184px" AutoPostBack="True" Visible="False"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td style="WIDTH: 100px" width="100" colSpan="1">&nbsp;Test Name</td>
					<td width="200" colSpan="3"><asp:textbox id="Txtname" Width="300px" CssClass="TextBox" Runat="server" ForeColor="#003366"></asp:textbox></td>
				</tr>
				<tr>
					<td style="WIDTH: 100px" colSpan="1">&nbsp;Max. Mark</td>
					<td width="72" colSpan="1"><asp:textbox id="Txtmaxmark" onblur="CheckNum(this.id)" Width="72px" CssClass="TextBox" Runat="server"
							ForeColor="#003366"></asp:textbox></td>
					<td style="WIDTH: 180px" colSpan="1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Min. Mark</td>
					<td style="WIDTH: 80px" align="center" colSpan="1"><asp:textbox id="Txtminmark" onblur="CheckNum(this.id)" Width="96" CssClass="TextBox" Runat="server"
							ForeColor="#003366"></asp:textbox></td>
				</tr>
				<tr>
					<td style="WIDTH: 100px; HEIGHT: 22px" width="100" colSpan="1">&nbsp;Min.Req.&nbsp;&nbsp;&nbsp; 
						Qualifying Mark
					</td>
					<td style="HEIGHT: 22px" align="left" width="72" colSpan="1"><asp:textbox id="TxtQual" onblur="CheckNum(this.id)" Width="72px" CssClass="TextBox" Runat="server"
							ForeColor="#003366"></asp:textbox></td>
					<td style="WIDTH: 180px; HEIGHT: 22px" colSpan="1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Min.Mark 
						Req.to &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;reattend</td>
					<td style="WIDTH: 80px; HEIGHT: 22px" align="center" colSpan="1"><asp:textbox id="Txtobtain" onblur="CheckNum(this.id)" Width="96" CssClass="TextBox" Runat="server"
							ForeColor="#003366"></asp:textbox></td>
				</tr>
				<tr>
					<td style="WIDTH: 100%" align="right" colSpan="4"><asp:button id="Btnsave" Width="64px" Runat="server" Text="Save"></asp:button>&nbsp;&nbsp;<asp:button id="BtnClose" Width="64px" Runat="server" Text="Close"></asp:button>&nbsp;</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
