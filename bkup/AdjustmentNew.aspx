<%@ Page Language="vb" AutoEventWireup="false" Codebehind="AdjustmentNew.aspx.vb" Inherits="eHRMS.Net.AdjustmentNew"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>AdjustmentNew</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
		<SCRIPT language="javascript" src="Common.js"></SCRIPT>
		<script language="vbscript"> 
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
		
			function ValidateCtrl()		
				If trim(document.getElementById("TxtCode").Value) = "" Then
					msgbox ("Employee Code Can not be left blank.")
					ValidateCtrl = false
					exit function
				End If
				
				If trim(document.getElementById("cmbPaydate").Value) = "" Then
					msgbox("Month Can't be Left Blank.")
					ValidateCtrl = false
					exit function
				end if
			end function
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx --><br>
			<br>
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="520" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Adjustment....
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="2" cellPadding="0" rules="none" width="520" align="center" border="1"
				frame="box">
				<tr>
					<td width="60"></td>
					<td width="250"></td>
					<td width="60"></td>
					<td width="150"></td>
				</tr>
				<tr>
					<td colSpan="4"><asp:label id="LblErrMsg" runat="server" Width="100%" ForeColor="Red"></asp:label></td>
				</tr>
				<tr>
					<td>&nbsp;<asp:label id="Label1" Runat="server" text="Employee"></asp:label></td>
					<td><asp:textbox id="TxtCode" Width="80" ForeColor="#003366" Runat="server" CssClass="textbox" AutoPostBack="True"></asp:textbox><asp:dropdownlist id="cmbEmp" runat="server" Width="132px" AutoPostBack="True" Visible="False"></asp:dropdownlist><asp:imagebutton id="btnList" Width="18px" Runat="server" ImageAlign="AbsMiddle" ImageUrl="Images\Find.gif"
							Height="19px"></asp:imagebutton><asp:imagebutton id="btnNew" Width="18px" Runat="server" ImageAlign="AbsMiddle" ImageUrl="Images\NewFile.ico"
							Height="19px" Visible="False"></asp:imagebutton></td>
					<td><asp:label id="Label2" Runat="server" text="Name"></asp:label></td>
					<td><asp:label id="LblName" Width="150" ForeColor="#003366" Runat="server" Font-Size="9" Font-Bold="True"></asp:label></td>
				</tr>
				<tr>
					<td>&nbsp;<asp:label id="LblPayDate" Runat="server" text="Month"></asp:label></td>
					<td><asp:dropdownlist id="cmbPaydate" runat="server" Width="150" AutoPostBack="True"></asp:dropdownlist></td>
					<td><asp:label id="Label3" Runat="server" text="Remarks"></asp:label></td>
					<td><asp:textbox id="TxtRemarks" Width="150" ForeColor="#003366" Runat="server" CssClass="TextBox"></asp:textbox></td>
				<tr>
					<td colSpan="4" height="10"></td>
				</tr>
				<tr>
					<td colSpan="4">
						<table cellSpacing="2" cellPadding="0" rules="none" width="100%" align="center" border="1">
							<tr>
								<td>
									<div style="BORDER-RIGHT: 1px groove; BORDER-TOP: 1px groove; OVERFLOW: auto; BORDER-LEFT: 1px groove; BORDER-BOTTOM: 1px groove; HEIGHT: 150px"><asp:datagrid id="grdAdjust" runat="server" AutoGenerateColumns="False">
											<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
											<HeaderStyle Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Top"
												BackColor="Gray"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="A/C Heads">
													<HeaderStyle Width="110px"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:DropDownList ID="CmbField" OnSelectedIndexChanged="DisplayDesc" Runat="server" Width="110px"
															AutoPostBack="True"></asp:DropDownList>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Description">
													<HeaderStyle Width="300px"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<asp:TextBox id=TxtDesc runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' ReadOnly="True">
														</asp:TextBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Amount">
													<HeaderStyle Width="100px"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:TextBox id=TxtAmt onblur="CheckNum(this.id)" runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Amount") %>'>
														</asp:TextBox>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid></div>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td align="left"><asp:button id="cmdReqAdd" runat="server" Width="75px" Text="Add"></asp:button></td>
					<td colspan="3" align="right">
						<asp:Button ID="CmdSave" Runat="server" Width="75px" Text="Save"></asp:Button>&nbsp;&nbsp;
						<asp:Button ID="CmdClose" Runat="server" Width="75px" Text="Close"></asp:Button>&nbsp;
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
