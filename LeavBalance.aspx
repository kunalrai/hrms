<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="LeavBalance.aspx.vb" Inherits="eHRMS.Net.LeavMast"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>LeavMast</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="Common.js"></SCRIPT>
		<script language="VBScript">
			Sub CheckDate(argID)
				Dim TVal
				TVal = document.getElementById(argID).value
				if isdate(TVal) then 
					If Len(TVal) = 11 Then
						If Not ((Mid(TVal, 3, 1) = "/" Or Mid(TVal, 3, 1) = "-") And (Mid(TVal, 7, 1) = "/" Or Mid(TVal, 7, 1) = "-")) Then
							MsgBox "Invalid Format! Please Enter in [dd/MMM/yyyy] Format."
							'document.getElementById(argID).focus()
						End If
					ElseIf Len(TVal) = 10 Then
						document.getElementById(argID).value = Left(TVal,2) & "/" & MonthName(Mid(TVal,4,2),true) & "/" & right(TVal,4)	
					Else
						MsgBox "Invalid Format! Please Enter in [dd/MMM/yyyy] Format.", , "Divergent"
						'document.getElementById(argID).focus()
					End If
				Else
					MsgBox "Invalid Date!", vbokOnly, "Divergent"
					'document.getElementById(argID).focus()
				End if
        				
			End Sub
			Sub DiffYears(argID)
				dim Yr
				Yr = Round(DateDiff("D",cdate(document.getElementById(Replace(argID,"ExpYears","ExpF")).value),cdate(document.getElementById(Replace(argID,"ExpYears","ExpT")).value))/365,2)
				document.getElementById(argID).value = Yr
			End Sub
			function ValidateCtrl()
				If trim(document.getElementById("TxtCode").Value) = "" Then
					msgbox ("Employee Code Can not be left blank.")
					ValidateCtrl = false
					exit function
				End If
				
				ValidateCtrl = true
			end function
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx -->
			<br>
			<TABLE cellSpacing="0" cellPadding="0" align="center" width="700" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" style="FONT-SIZE: 18px" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Leave 
						Balances ....
					</TD>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" rules="none" align="center" width="700" border="1"
				frame="border">
				<tr>
					<td width="15%"></td>
					<td width="45%"></td>
					<td width="40%"></td>
				</tr>
				<tr>
					<td colSpan="3"><asp:label id="lblMsg" runat="server" Width="100%" ForeColor="Red" Font-Size="11px" Visible="False"></asp:label></td>
				</tr>
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colSpan="3"><asp:datagrid id="GrdLeavBal" runat="server" AutoGenerateColumns="False" Width="100%">
							<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
							<HeaderStyle Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Top"
								BackColor="Gray"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="LVTYPE">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" Width="0px"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="LVDESC" HeaderText="Leave Type">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="WEF">
									<ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox id=Textbox3 onblur=CheckDate(this.id) runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "WEF") %>'>
										</asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Opening">
									<ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox id=Textbox1 runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Opening") %>'>
										</asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Credited">
									<ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox id=Textbox2 runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Credited") %>'>
										</asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Availed" HeaderText="Availed">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="TrfIn" HeaderText="Transfer(In)">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="TrfOut" HeaderText="Transfer(Out)">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="12%"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Balance">
									<ItemStyle HorizontalAlign="Left" Width="8%"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=LblClosing runat="server" ForeColor="#003366" Width="100%" Text='<%# DataBinder.Eval(Container.DataItem, "Balance") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td class="Header3">Code</td>
					<td class="Header3"><asp:textbox id="TxtCode" Width="80" CssClass="textbox" AutoPostBack="True" Runat="server"></asp:textbox><asp:dropdownlist id="cmbEmp" runat="server" Width="150" Visible="False" AutoPostBack="True"></asp:dropdownlist><asp:imagebutton id="btnList" Width="18px" Runat="server" Height="19px" ImageUrl="Images\Find.gif"
							ImageAlign="AbsMiddle"></asp:imagebutton></td>
					<td class="Header3"><asp:label id="LblName" Runat="server"></asp:label></td>
				</tr>
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td align="right" colSpan="3"><asp:button id="CmdSave" runat="server" Width="80px" Text="Save"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:button id="cmdClose" runat="server" Width="80px" Text="Close"></asp:button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
