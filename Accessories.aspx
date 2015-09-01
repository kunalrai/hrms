<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Accessories.aspx.vb" Inherits="eHRMS.Net.Accessories" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Accessories</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
		<script language="VBscript">
			Sub CheckDate(argID)
				Dim TVal
				TVal = document.getElementById(argID).value
				if TVal="" then Exit Sub
				if isdate(TVal) then 
					If Len(TVal) = 11 Then
						If Not ((Mid(TVal, 3, 1) = "/" Or Mid(TVal, 3, 1) = "-") And (Mid(TVal, 7, 1) = "/" Or Mid(TVal, 7, 1) = "-")) Then
							MsgBox "Invalid Format! Please Enter in [dd/MMM/yyyy] Format.", , "Date Format"
						document.getElementById(argID).value = ""
					End If
					ElseIf Len(TVal) = 10 Then
						document.getElementById(argID).value = Left(TVal,2) & "/" & MonthName(Mid(TVal,4,2),true) & "/" & right(TVal,4)		
					Else
						MsgBox "Invalid Format! Please Enter in [dd/MMM/yyyy] Format.", , "Date Format"
						document.getElementById(argID).value = ""
					End If
				Else
					MsgBox "Invalid Date!", vbokOnly, "Date Format"
					document.getElementById(argID).value = ""
				End if
			End Sub
			Sub CheckNum(argID)
				Dim TVal
				TVal = document.getElementById(argID).value
				if trim(TVal) = "" then 
					document.getElementById(argID).value = 0
				Exit Sub
				End if
				if Not IsNumeric(TVal) then
						MsgBox "Invalid Value! Please Enter numeric Value.", , "Divergent"
						document.getElementById(argID).value = 0
				End if
			End Sub	
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<!--#include file=MenuBars.aspx --><br>
		<br>
		<br>
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="650" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Accessories 
						Provided To Employee.....
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="2" cellPadding="0" rules="none" width="650" align="center" border="1"
				frame="box">
				<tr>
					<td width="15%"></td>
					<td width="35%"></td>
					<td width="15%"></td>
					<td width="35%"></td>
				</tr>
				<tr>
					<td colSpan="4"><asp:label id="LblErrMsg" runat="server" ForeColor="Red" Width="100%"></asp:label></td>
				</tr>
				<tr>
					<td>&nbsp;Employee</td>
					<td><asp:textbox id="TxtCode" ForeColor="#003366" Width="120" CssClass="textbox" AutoPostBack="True"
							Runat="server"></asp:textbox><asp:dropdownlist id="cmbEmpCode" runat="server" Width="200px" AutoPostBack="True" Visible="False"></asp:dropdownlist><asp:imagebutton id="btnList" Width="18px" Runat="server" ImageAlign="AbsMiddle" ImageUrl="Images\Find.gif"
							Height="19px"></asp:imagebutton></td>
					<td>&nbsp;Name</td>
					<td><asp:label id="LblName" ForeColor="#003366" Width="100%" Runat="server" Font-Size="9" Font-Bold="True"></asp:label></td>
				</tr>
				<tr>
					<td>&nbsp;Designation</td>
					<td><asp:dropdownlist id="CmbDesg" Width="100%" Runat="server"></asp:dropdownlist></td>
					<td>&nbsp;Department</td>
					<td><asp:dropdownlist id="CmbDept" Width="100%" Runat="server"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td>&nbsp;Employee Type</td>
					<td><asp:dropdownlist id="CmbType" Width="100%" Runat="server"></asp:dropdownlist>
					<td>&nbsp;Location</td>
					<td><asp:dropdownlist id="CmbLoc" Width="100%" Runat="server"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td colSpan="4">&nbsp;</td>
				</tr>
				<tr>
					<td colSpan="4">
						<TABLE id="Tabgrdreq" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td>
									<div style="OVERFLOW: auto; WIDTH: 650px; BORDER-TOP-STYLE: solid; BORDER-RIGHT-STYLE: solid; BORDER-LEFT-STYLE: solid; HEIGHT: 200px; BORDER-BOTTOM-STYLE: solid"><asp:datagrid id="GrdItem" runat="server" Width="1000px" AutoGenerateColumns="False">
											<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
											<HeaderStyle Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Top"
												BackColor="Gray"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="Description">
													<ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
													<ItemTemplate>
														<asp:TextBox Width="100%" runat="server" ForeColor="#003366" ID="txtDesc" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "ItemName") %>'/>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Model No.">
													<ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
													<ItemTemplate>
														<asp:TextBox Width="100%" runat="server" ForeColor="#003366" ID="txtModelNo" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "ModelNo") %>'/>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Purchase Date">
													<ItemStyle HorizontalAlign="Left" Width="12%"></ItemStyle>
													<ItemTemplate>
														<asp:TextBox Width="100%" runat="server" ID="txtPDate" OnBlur="CheckDate(this.id)" ForeColor="#003366" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "PurDate") %>' />
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Make">
													<ItemStyle HorizontalAlign="Left" Width="12%"></ItemStyle>
													<ItemTemplate>
														<asp:TextBox Width="100%" runat="server" ID="txtMake" CssClass="TextBox" ForeColor="#003366" Text='<%# DataBinder.Eval(Container.DataItem, "Make") %>'/>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Estimated Cost">
													<ItemStyle HorizontalAlign="Left" Width="12%"></ItemStyle>
													<ItemTemplate>
														<asp:TextBox Width="100%" runat="server" OnBlur="CheckNum(this.id)" ID="TxtCost" ForeColor="#003366" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Cost") %>'/>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Allocation Date">
													<ItemStyle HorizontalAlign="Left" Width="12%"></ItemStyle>
													<ItemTemplate>
														<asp:TextBox ForeColor="#003366" Width="100%" runat="server" OnBlur="CheckDate(this.id)" ID="TxtADate" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "AllocationDate") %>'/>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn ItemStyle-Width="12%" HeaderText="Return" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
													<ItemTemplate>
														<asp:CheckBox ID="ChkReturn" Runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid></div>
								</td>
							</tr>
							<TR>
								<TD align="left"><asp:button id="cmdReqAdd" runat="server" Width="75px" Text="Add"></asp:button></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td colspan="4" align="right"><asp:Button ID="CmdSave" Runat="server" Width="75" Text="Save"></asp:Button>&nbsp;
						<asp:Button ID="CmdClose" Runat="server" Width="75" Text="Close"></asp:Button>&nbsp;</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
