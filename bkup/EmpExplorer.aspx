<%@ Page Language="vb" AutoEventWireup="false" Codebehind="EmpExplorer.aspx.vb" Inherits="eHRMS.Net.EmpExplorer"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Employee Explorer</title>
		<SCRIPT language="javascript" src="coolmenus4.js">
		</SCRIPT>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
		<script language="vbscript">
			sub LoadOpen()
				window.open "PageFooter.aspx","Footer" 
			End Sub
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<br>
			<TABLE cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">
						<% If IsNothing(Session("LoginUser")) Then
						Response.Redirect("CompSel.aspx")
						End If 
						if Session("LoginUser").UserGroup = "ADMIN" %>
						Employee Explorer....
						<%  ELSE	%>
						My Profile....
						<% End If %>
					</TD>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
				<tr>
					<td width="1%" bgColor="gray">&nbsp;</td>
					<td width="98%"><asp:label id="LblMsg" runat="server" Width="100%" Font-Size="11px" ForeColor="Red" Visible="False"></asp:label></td>
					<td width="1%" bgColor="gray">&nbsp;</td>
				</tr>
				<tr>
					<td width="1%" bgColor="gray">&nbsp;</td>
					<td width="98%"><asp:datagrid id="GrdEmployee" runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True">
							<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
							<HeaderStyle Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Top"
								BackColor="Gray"></HeaderStyle>
							<PagerStyle NextPageText="&amp;minus;&amp;gt;" Font-Underline="True" Font-Names="Verdana" PrevPageText="&amp;lt;&amp;minus;"
								ForeColor="#CB3908" BackColor="#E0E0E0" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
					<td width="1%" bgColor="gray">&nbsp;</td>
				</tr>
				<tr>
					<td width="1%" bgColor="gray">&nbsp;</td>
					<td class="Header3" width="98%">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="Header3" width="50%">&nbsp;&nbsp;
								</td>
								<td align="center" width="50%"><asp:button id="cmdNew" accessKey="N" runat="server" Width="75px" Text="Add New"></asp:button>&nbsp;&nbsp;&nbsp;
									<asp:button id="cmdClose" accessKey="D" runat="server" Width="75px" Visible="False" Text="Close"></asp:button>&nbsp;&nbsp;&nbsp;
								</td>
							</tr>
						</table>
					</td>
					<td width="1%" bgColor="gray">&nbsp;</td>
				</tr>
				<tr class="Header3" id="TrDisplaySetting" vAlign="bottom" runat="server">
					<td width="1%" bgColor="gray">&nbsp;</td>
					<td width="98%">
						<table cellSpacing="0" cellPadding="0" width="100%" border="1">
							<tr vAlign="middle">
								<td class="Header3" width="40">Find</td>
								<td width="120"><asp:dropdownlist id="cmbSearchFld" runat="server" Width="120px"></asp:dropdownlist></td>
								<td width="135"><asp:dropdownlist id="cmbSign" runat="server" Width="135px"></asp:dropdownlist></td>
								<td width="200">&nbsp;<asp:textbox id="txtSearchText" runat="server" Width="100px" CssClass="TextBox" ForeColor="#003366"></asp:textbox>&nbsp;&nbsp;&nbsp;<asp:Button id="CmdSearch" Width="50px" runat="server" Text="Go"></asp:Button></td>
								<td class="Header3" width="80">Page Style :</td>
								<td width="80"><asp:dropdownlist id="cmbPageStyle" runat="server" Width="80px" AutoPostBack="True"></asp:dropdownlist></td>
								<td class="Header3" width="130">Records Per Page :</td>
								<td><asp:textbox id="txtNumRec" runat="server" Width="40px" CssClass="TextBox" AutoPostBack="True"
										ForeColor="#003366"></asp:textbox></td>
							</tr>
						</table>
					</td>
					<td width="1%" bgColor="gray">&nbsp;
					</td>
				</tr>
				<tr>
					<td colSpan="3">&nbsp;</td>
				</tr>
				<tr id="TrPfLabel" runat="server">
					<td align="center" colSpan="3"><font color="#003366" size="4"><b>To View Provident Fund 
								Details Click below</b></font></td>
				</tr>
				<tr>
					<td colSpan="3">&nbsp;</td>
				</tr>
				<tr id="TrPf" runat="server">
					<td align="center" colSpan="3">
						<table borderColor="gray" cellSpacing="0" cellPadding="0" rules="none" width="250" align="center"
							border="1" frame="border">
							<tr>
								<td width="35%"></td>
								<td width="65%"></td>
							</tr>
							<tr>
								<td>User Id</td>
								<td><asp:label id="TxtUserId" runat="server" Width="100%" CssClass="textbox"></asp:label></td>
							</tr>
							<tr>
								<td>User Password</td>
								<td><asp:Label id="TxtUsrPwd" Width="100%" runat="server" CssClass="textbox"></asp:Label></td>
							</tr>
							<tr>
								<td>Comp_Code</td>
								<td><asp:Label id="TxtCompCode" Width="100%" runat="server" CssClass="textbox"></asp:Label></td>
							</tr>
							<tr align="center">
								<td colspan="2"><b><a href="https://www.dkminfoway.com/Reports.aspx" target="DKM Online"><u>---Click 
												to view PF Details---</u></a></b></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="3"><asp:Label BackColor="gray" id="LblBanner" Font-Size="15" ForeColor="White" Height="25px" Width="100%"
							runat="server"></asp:Label></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
