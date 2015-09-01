<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ShiftAllocation.aspx.vb" Inherits="eHRMS.Net.ShiftAllocation" %>
<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ShiftAllocation</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
		<SCRIPT language="javascript">
			function ShowRow(argName)
				{
					Menu = new String(argName)
					if (document.getElementById('tr' + Menu).style.display == "none")
						{
						 document.getElementById('tr' + Menu).style.display = "block";
						 document.getElementById('img' + Menu).src = "Minus.gif";
						 //document.getElementById("trCode").style.display = "none";
						 //document.getElementById("trGrid").style.display = "none";
						}
					else
						{
						 document.getElementById('tr' + Menu).style.display = "none";
						 document.getElementById('img' + Menu).src = "plus.gif";
						 //document.getElementById("trCode").style.display = "block";
						 //document.getElementById("trGrid").style.display = "block";
						}
				}
		</SCRIPT>
		<script language="VBScript">
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
					Else
						MsgBox "Invalid Format! Please Enter in [dd/MMM/yyyy] Format.", , "Date Format"
						document.getElementById(argID).value = ""
					End If
				Else
					MsgBox "Invalid Date!", vbokOnly, "Date Format"
					document.getElementById(argID).value = ""
				End if
			End Sub
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx --><br>
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="800" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Shift 
						Allocation ....
					</TD>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" rules="none" width="800" align="left" border="1"
				frame="border">
				<tr>
					<td width="10%"></td>
					<td width="30%"></td>
					<td width="20%"></td>
					<td width="30%"></td>
					<td width="10%"></td>
				</tr>
				<tr borderColor="white">
					<td colSpan="5"><asp:label id="lblMsg" runat="server" Width="100%" ForeColor="Red" Font-Size="11px"></asp:label></td>
				</tr>
				<tr>
					<td class="header3" colspan="5"><font size="2"><u>Select Employee(s)</u></font></td>
				</tr>
				<tr id="trCode">
					<td class="Header3">Code</td>
					<td class="Header3"><asp:textbox id="TxtCode" runat="server" Width="75px" CssClass="TextBox" ForeColor="#003366"></asp:textbox><asp:dropdownlist id="cmbCode" runat="server" Width="200" Visible="False" AutoPostBack="True"></asp:dropdownlist><asp:imagebutton id="btnList" Width="18px" Runat="server" ImageAlign="AbsMiddle" ImageUrl="Images\Find.gif"
							Height="19px"></asp:imagebutton></td>
					<td align="center" class="Header3">Department</td>
					<td class="Header3"><asp:dropdownlist id="cmbDept" runat="server" Width="200"></asp:dropdownlist></td>
					<td align="center" class="Header3">
						<asp:Button id="cmdShow" runat="server" Width="55px" Text="Show"></asp:Button></td>
				</tr>
				<tr>
					<td colspan="5">&nbsp;</td>
				</tr>
				<tr>
					<td class="header3" colspan="5"><font size="2"><u>Select Shift</u></font></td>
				</tr>
				<tr id="trShift" runat="server">
					<td colSpan="5">
						<table borderColor="darkgray" cellSpacing="0" cellPadding="0" rules="none" width="100%"
							border="1" frame="border">
							<tr>
								<td width="15%" class="Header3">With Effect</td>
								<td width="35%" class="Header3"><cc1:dtpcombo id="dtpWEF" runat="server" Width="150px" ToolTip="Date Of Birth" DateValue="2005-01-01"></cc1:dtpcombo></td>
								<td width="15%" class="Header3">Shift</td>
								<td width="35%" class="Header3"><asp:dropdownlist id="cmbFShift" runat="server" Width="150px"></asp:dropdownlist></td>
							</tr>
							<tr>
								<td colSpan="4"><asp:checkboxlist id="ChkWeeklyOff" runat="server" Width="100%" CssClass="Header3" RepeatDirection="Horizontal">
										<asp:ListItem Value="1">Sunday</asp:ListItem>
										<asp:ListItem Value="2">Monday</asp:ListItem>
										<asp:ListItem Value="3">Tuesday</asp:ListItem>
										<asp:ListItem Value="4">Wednesday</asp:ListItem>
										<asp:ListItem Value="5">Thursday</asp:ListItem>
										<asp:ListItem Value="6">Friday</asp:ListItem>
										<asp:ListItem Value="7">Saturday</asp:ListItem>
									</asp:checkboxlist></td>
							</tr>
							<tr>
								<td align="right" colSpan="4"><asp:button id="cmdSave" runat="server" Width="80px" Text="Save"></asp:button>&nbsp;&nbsp;<asp:button id="cmdClose" runat="server" Width="80px" Text="Close"></asp:button>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="5">&nbsp;</td>
				</tr>
				<tr id="trGrid">
					<td colSpan="5"><asp:datagrid id="GrdShift" runat="server" Width="100%" PageSize="15" AllowPaging="True" AutoGenerateColumns="False">
							<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
							<HeaderStyle Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Top"
								BackColor="Gray"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="EMP_CODE" HeaderText="Code">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="EMP_NAME" HeaderText="Name">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="WEF" HeaderText="WEF">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Shift_Name" HeaderText="Shift">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" Width="80px"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Sun">
									<ItemStyle HorizontalAlign="Right" Width="30px"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox CssClass="CheckBox" Enabled="False" ID="ChkSun" Runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Mon">
									<ItemStyle HorizontalAlign="Right" Width="30px"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox CssClass="Textbox" Enabled="False" ID="ChkMon" Runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Tue">
									<ItemStyle HorizontalAlign="Right" Width="30px"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox CssClass="Textbox" ID="ChkTue" Enabled="False" Runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Wed">
									<ItemStyle HorizontalAlign="Right" Width="30px"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox CssClass="Textbox" ID="ChkWed" Runat="server" Enabled="False"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Thu">
									<ItemStyle HorizontalAlign="Right" Width="30px"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox CssClass="Textbox" ID="ChkThu" Runat="server" Enabled="False"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Fri">
									<ItemStyle HorizontalAlign="Right" Width="30px"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox CssClass="Textbox" ID="ChkFri" Enabled="False" Runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Sat">
									<ItemStyle HorizontalAlign="Right" Width="30px"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox CssClass="Textbox" Enabled="False" ID="ChkSat" Runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td colSpan="5">&nbsp;</td>
				</tr>
				<tr>
					<td colSpan="5">&nbsp;</td>
				</tr>
				<tr>
					<td class="Header3" align="right" colSpan="5">&nbsp;&nbsp;&nbsp;
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
