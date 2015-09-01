<%@ Page Language="vb" AutoEventWireup="false" Codebehind="UpdateMngrs.aspx.vb" Inherits="eHRMS.Net.UpdateMngrs"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>UpdateMngrs</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<br>
			<TABLE cellSpacing="0" align="center"  cellPadding="0" width="600" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Update 
						Managers....
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<TABLE cellSpacing="1"  align="center" cellPadding="1" rules="none" width="600" border="1" frame="border">
				<tr>
					<td width="20%"></td>
					<td width="30%"></td>
					<td width="25%"></td>
					<td width="25%"></td>
				</tr>
				<tr>
					<td colSpan="4"><asp:label id="LblErrMsg" runat="server" ForeColor="Red" Width="100%"></asp:label></td>
				</tr>
				<tr>
					<td class="Header3">Current Manager</td>
					<td class="Header3"><asp:textbox id="TxtCode" Width="80" CssClass="textbox" AutoPostBack="True" Runat="server" ForeColor="#003366"></asp:textbox><asp:dropdownlist id="cmbManager" runat="server" Width="150" AutoPostBack="True" Visible="False"></asp:dropdownlist><asp:imagebutton id="btnList" Width="18px" Runat="server" Height="19px" ImageUrl="Images\Find.gif"
							ImageAlign="AbsMiddle"></asp:imagebutton></td>
					<td class="Header3" colSpan="2"><asp:label id="lblName" Runat="server"></asp:label></td>
				</tr>
				<tr>
					<td colSpan="4">&nbsp;</td>
				</tr>
				<tr>
					<td class="Header3" background="Images\headstripe.jpg" colSpan="4"><B>Subordinates</B></td>
				</tr>
				<tr>
					<td colSpan="4"><asp:datagrid id="GrdSubordinates" runat="server" Width="100%" PagerStyle-Mode="NumericPages"
							AutoGenerateColumns="False">
							<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
							<HeaderStyle Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Top"
								BackColor="Gray"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="EMP_CODE" HeaderText="Code" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
								<asp:BoundColumn DataField="EMP_NAME" HeaderText="Name" ItemStyle-Width="150px"></asp:BoundColumn>
								<asp:BoundColumn DataField="DEPT_NAME" HeaderText="Department" ItemStyle-Width="100px"></asp:BoundColumn>
								<asp:BoundColumn DataField="DIVI_NAME" HeaderText="Division" ItemStyle-Width="80px"></asp:BoundColumn>
								<asp:BoundColumn DataField="DSG_NAME" HeaderText="Designation" ItemStyle-Width="200px"></asp:BoundColumn>
								<asp:BoundColumn DataField="SECT_NAME" HeaderText="Section" ItemStyle-Width="100px"></asp:BoundColumn>
								<asp:BoundColumn DataField="GRD_NAME" HeaderText="Grade" ItemStyle-Width="70px"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Select" ItemStyle-HorizontalAlign="Center">
									<ItemTemplate>
										<asp:CheckBox ID="chk" Runat="server" Width="30px"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td colSpan="4">&nbsp;</td>
				</tr>
				<tr>
					<td class="Header3">New Manager</td>
					<td class="Header3" colSpan="2"><asp:dropdownlist id="cmbChangeMngr" runat="server" Width="200px"></asp:dropdownlist></td>
					<td class="Header3"><asp:label id="LblChName" Runat="server"></asp:label></td>
				</tr>
				<TR height="15">
					<TD style="MARGIN-LEFT: 15px; MARGIN-RIGHT: 15px; BORDER-BOTTOM: #993300 thin groove; HEIGHT: 7px"
						colSpan="4"></TD>
				</TR>
				<tr>
					<td align="right" colSpan="4"><asp:button id="cmdSave" runat="server" Width="75px" Text="Save"></asp:button>&nbsp;
						<asp:button id="cmdClose" runat="server" Width="75px" Text="Close"></asp:button></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
