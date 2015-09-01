<%@ Page Language="vb" AutoEventWireup="false" Codebehind="LeaveCredit.aspx.vb" Inherits="eHRMS.Net.LeaveCredit"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>LeaveCredit</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="Common.js"></SCRIPT>
		<script language="javascript">
		</script>
		<script language="vbscript">
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
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="700" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Monthly 
						Leave Credit....
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" align="center" rules="none" width="700" border="1"
				frame="border">
				<tr>
					<td width="15%"></td>
					<td width="45%"></td>
					<td width="25%"></td>
					<td width="15%"></td>
				</tr>
				<tr>
					<td colSpan="4"><asp:label id="LblMsg" runat="server" Width="100%" ForeColor="Red" Font-Size="11px"></asp:label></td>
				</tr>
				<tr>
					<td>For the Month</td>
					<td><asp:dropdownlist id="cmbMonth" runat="server" Width="200px"></asp:dropdownlist></td>
					<td><asp:button id="cmdShow" runat="server" Width="64px" Text="Show"></asp:button></td>
					<td align="right"><asp:checkbox id="IsAll" AutoPostBack="True" Text="Include All" Runat="server" CssClass="textbox"></asp:checkbox></td>
				</tr>
				<tr>
					<td colSpan="4">&nbsp;</td>
				</tr>
				<tr>
					<td colSpan="4">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 400px; BORDER-BOTTOM-STYLE: solid">
							<asp:datagrid id="GrdLeavCr" runat="server" Width="100%" AutoGenerateColumns="False">
								<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
								<HeaderStyle Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Top"
									BackColor="Gray"></HeaderStyle>
								<Columns>
									<asp:BoundColumn DataField="EMP_CODE" HeaderText="Code">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="EMP_NAME" HeaderText="Name">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" Width="25%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="LVDESC" HeaderText="Leave Type">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Opening" HeaderText="Opening">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Availed" HeaderText="Availed">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Balance" HeaderText="Balance">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Credited">
										<ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
										<ItemTemplate>
											<asp:TextBox id=TxtCredited onblur=CheckNum(this.id) runat="server" ForeColor="#003366" Width="100%" Text='<%# DataBinder.Eval(Container.DataItem, "CREDITED") %>' CssClass="Textbox">
											</asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Select">
										<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
										<ItemTemplate>
											<INPUT class="TextBox" id="ChkSelect" style="WIDTH: 20px" type="checkbox" runat="server">
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid>
						</div>
					</td>
				</tr>
				<tr>
					<td colSpan="4">&nbsp;</td>
				</tr>
				<tr>
					<td align="right" colSpan="4"><asp:button id="cmdSave" runat="server" Width="75px" Text="Save"></asp:button>&nbsp;
						<asp:button id="cmdClose" runat="server" Width="75px" Text="Close"></asp:button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
