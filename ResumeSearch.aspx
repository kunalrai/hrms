<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ResumeSearch.aspx.vb" Inherits="eHRMS.Net.ResumeSearch"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ResumeSearch</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx --><br>
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="600" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Resume 
						Search ....
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" rules="none" width="600" border="1" frame="border">
				<tr>
					<td width="33%"></td>
					<td width="10%"></td>
					<td width="32%"></td>
					<td width="10%"></td>
					<td width="15%"></td>
				</tr>
				<tr>
					<td colSpan="5"><asp:label id="LblErrMsg" Width="100%" ForeColor="#ff3333" Runat="server"></asp:label></td>
				</tr>
				<!--<tr>
					<td>Saved Search</td>
					<td><asp:dropdownlist id="cmbSSearch" runat="server" Width="100%"></asp:dropdownlist></td>
					<td>Location</td>
					<td><asp:dropdownlist id="cmbLocation" runat="server" Width="100%"></asp:dropdownlist></td>
				</tr>-->
				<tr>
					<td colSpan="5">
						<table cellSpacing="0" cellPadding="0" rules="none" width="100%" border="1" frame="border">
							<tr>
								<td width="33%"></td>
								<td width="10%"></td>
								<td width="32%"></td>
								<td width="10%"></td>
								<td width="15%"></td>
							</tr>
							<tr>
								<td class="Header3" align="center">Field Name</td>
								<td class="Header3" align="center">Condition</td>
								<td class="Header3" align="center">Values</td>
								<td class="Header3" align="center">AND/OR</td>
								<td class="Header3" align="center">Add</td>
							</tr>
							<tr>
								<td align="left"><asp:dropdownlist id="CmbField" runat="server" Width="95%" AutoPostBack="True"></asp:dropdownlist></td>
								<td align="center"><asp:dropdownlist id="cmbCondition" runat="server" Width="90%"></asp:dropdownlist></td>
								<td align="center"><asp:dropdownlist id="cmbValues" runat="server" Width="80%" Visible="False"></asp:dropdownlist><asp:textbox id="TxtValue" runat="server" Width="80%" Visible="False" CssClass="textbox" ForeColor="#003366"></asp:textbox></td>
								<td align="center"><asp:dropdownlist id="cmbAndOr" runat="server" Width="100%"></asp:dropdownlist></td>
								<td align="center"><asp:button id="cmdNew" runat="server" Width="60px" Text="Add"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="5">&nbsp;</td>
				</tr>
				<tr>
					<td colSpan="5"><asp:datagrid id="GrdResumes" runat="server" Width="100%" AutoGenerateColumns="False" PagerStyle-Mode="NumericPages">
							<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
							<HeaderStyle Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Top"
								BackColor="Gray"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="FieldCode" Visible="False" ItemStyle-Width="0px"></asp:BoundColumn>
								<asp:BoundColumn DataField="FieldName" HeaderText="Field Name" ItemStyle-Width="150px"></asp:BoundColumn>
								<asp:BoundColumn DataField="Condition" HeaderText="Condition" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
								<asp:BoundColumn DataField="Val" HeaderText="Value" ItemStyle-Width="200px"></asp:BoundColumn>
								<asp:BoundColumn DataField="AndOr" HeaderText="And/Or" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
								<asp:BoundColumn DataField="ValCOde" Visible="False" ItemStyle-Width="0px"></asp:BoundColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td colSpan="5">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="3"></td>
					<td align="center"><asp:linkbutton id="BtnSearch" runat="server" Width="80px" Font-Bold="True" BorderStyle="Outset">Search</asp:linkbutton></td>
					<td align="center"><asp:linkbutton id="BtnReset" runat="server" Width="80px" Font-Bold="True" BorderStyle="Outset">Reset</asp:linkbutton></td>
				</tr>
				<tr>
					<td colSpan="5">&nbsp;</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
