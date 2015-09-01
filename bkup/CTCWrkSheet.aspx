<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CTCWrkSheet.aspx.vb" Inherits="eHRMS.Net.CTCWrkSheet" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>CTC Worksheet</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
		<script language="javascript">
		</script>
		<script language="vbscript">
			sub ShowExcel()
				'document.getElementById("TxtQry").value = document.getElementById("tdGrdCTC").innerHTML
				document.getElementById("cmdShowExcel").click 
				window.open "frmHTMLReports.aspx","","status=yes,toolbar=yes,menubar=yes,scrollbars=yes,resizable=yes"
			End Sub
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="5" rightMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx --><br>
			<TABLE cellSpacing="0" cellPadding="0"  width="750" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">
						<% If IsNothing(Session("LoginUser")) Then
							Response.Redirect("CompSel.aspx")
						End If 
						%>
						CTC Worksheet....
					</TD>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table borderColor="black" cellSpacing="0" cellPadding="0" width="750" align="left" border="0">
				<tr>
					<td width="100%" colSpan="9"><asp:label id="LblMsg" runat="server" Visible="False" ForeColor="Red" Font-Size="11px" Width="100%"></asp:label></td>
				</tr>
				<TR>
					<td class="Header3" align="center" width="25%" colSpan="1">PayDate</td>
					<td class="Header3" width="40%" colSpan="1"><asp:dropdownlist id="cmbPayDate" runat="server" AutoPostBack="True"></asp:dropdownlist></td>
					<td class="Header3" align="center" width="25%" colSpan="1">WEF date</td>
					<td class="Header3" width="25%" colSpan="1"><cc1:dtp id="DtpWefDate" runat="server" Width="125px" DESIGNTIMEDRAGDROP="1527" TextBoxPostBack="True"></cc1:dtp></td>
					<td class="Header3" width="25%" colSpan="5"></td>
				</TR>
				<tr>
					<td id="tdGrdCTC" colSpan="9" runat="server"><asp:datagrid id="Grdctc" runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True">
							<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
							<HeaderStyle Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Top"
								BackColor="Gray"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="EMP_CODE" HeaderText="Code"></asp:BoundColumn>
								<asp:BoundColumn DataField="EMP_NAME" HeaderText="Name">
									<HeaderStyle Width="100px"></HeaderStyle>
									<ItemStyle Width="100px"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DOB" ReadOnly="True" HeaderText="DOB" DataFormatString="{0:dd/MMM/yyyy}"></asp:BoundColumn>
								<asp:BoundColumn DataField="DOJ" ReadOnly="True" HeaderText="DOJ" DataFormatString="{0:dd/MMM/yyyy}"></asp:BoundColumn>
								<asp:BoundColumn DataField="GRD_NAME" ReadOnly="True" HeaderText="Band"></asp:BoundColumn>
								<asp:BoundColumn DataField="DSG_NAME" ReadOnly="True" HeaderText="Title"></asp:BoundColumn>
								<asp:BoundColumn DataField="DEPT_NAME" ReadOnly="True" HeaderText="Function"></asp:BoundColumn>
								<asp:BoundColumn DataField="LOC_NAME" ReadOnly="True" HeaderText="Location"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Increment(%)">
									<ItemStyle HorizontalAlign="Right" Width="15%"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox id=txtIncre_P style="TEXT-ALIGN: right" runat="server" Width="100%" Visible="False" AutoPostBack="True" Text='<%# DataBinder.Eval(Container.DataItem, "Incre_P") %>' CssClass="TextBox" MaxLength="20" tooltip='<%# DataBinder.Eval(Container.DataItem, "EMP_CODE") %>' OnTextChanged="OnTextChanged">
										</asp:TextBox>
										<asp:Label id="lblIncre_P" runat="server" Visible="False">
											<%# DataBinder.Eval(Container.DataItem, "Incre_P") %>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Basic_P" HeaderText="Basic"></asp:BoundColumn>
								<asp:BoundColumn DataField="SA_P" ReadOnly="True" HeaderText="SA"></asp:BoundColumn>
								<asp:BoundColumn DataField="HRA_P" HeaderText="HRA"></asp:BoundColumn>
								<asp:BoundColumn DataField="CONV_P" ReadOnly="True" HeaderText="CONV"></asp:BoundColumn>
								<asp:BoundColumn DataField="VEH_P" ReadOnly="True" HeaderText="VEH"></asp:BoundColumn>
								<asp:BoundColumn DataField="PF_P" ReadOnly="True" HeaderText="PF"></asp:BoundColumn>
								<asp:BoundColumn DataField="SAF_P" ReadOnly="True" HeaderText="SAF"></asp:BoundColumn>
								<asp:BoundColumn DataField="GRATUITY_P" ReadOnly="True" HeaderText="GRATUITY"></asp:BoundColumn>
								<asp:BoundColumn DataField="MIP_P" ReadOnly="True" HeaderText="MIP"></asp:BoundColumn>
								<asp:BoundColumn DataField="PEP_P" ReadOnly="True" HeaderText="PEP"></asp:BoundColumn>
								<asp:BoundColumn DataField="CTC_P" ReadOnly="True" HeaderText="CTC"></asp:BoundColumn>
								<asp:BoundColumn DataField="Basic" HeaderText="Basic New"></asp:BoundColumn>
								<asp:BoundColumn DataField="SA" ReadOnly="True" HeaderText="SA New"></asp:BoundColumn>
								<asp:BoundColumn DataField="HRA" HeaderText="HRA New"></asp:BoundColumn>
								<asp:BoundColumn DataField="CONV" ReadOnly="True" HeaderText="CONV New"></asp:BoundColumn>
								<asp:BoundColumn DataField="VEH" ReadOnly="True" HeaderText="VEH New"></asp:BoundColumn>
								<asp:BoundColumn DataField="PF" ReadOnly="True" HeaderText="PF New"></asp:BoundColumn>
								<asp:BoundColumn DataField="SAF" ReadOnly="True" HeaderText="SAF New"></asp:BoundColumn>
								<asp:BoundColumn DataField="GRATUITY" ReadOnly="True" HeaderText="GRATUITY New"></asp:BoundColumn>
								<asp:BoundColumn DataField="MIP" ReadOnly="True" HeaderText="MIP New"></asp:BoundColumn>
								<asp:BoundColumn DataField="PEP" ReadOnly="True" HeaderText="PEP New"></asp:BoundColumn>
								<asp:BoundColumn DataField="CTC" ReadOnly="True" HeaderText="CTC New"></asp:BoundColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td colSpan="9">
						<table cellSpacing="0" cellPadding="0" width="100%" border="1">
							<tr vAlign="middle">
								<td class="Header3" width="5%">For</td>
								<td width="10%"><asp:dropdownlist id="cmbSearchFld" runat="server" Width="120px" AutoPostBack="True"></asp:dropdownlist></td>
								<td width="10%"><asp:dropdownlist id="cmbMastList" runat="server" Width="135px"></asp:dropdownlist></td>
								<td align="left" width="5%"><asp:button id="CmdSearch" runat="server" Width="75px" Text="Go"></asp:button></td>
								<td class="Header3" width="20%">Records/Page :</td>
								<td width="10%"><asp:textbox id="txtNumRec" runat="server" Width="75px" AutoPostBack="True" CssClass="TextBox"
										ForeColor="#003366"></asp:textbox></td>
								<td width="10%"><asp:button id="cmdSave" Width="75px" Text="Save" Runat="server"></asp:button></td>
								<td width="7.5%"><asp:button id="cmdDelete" Width="75px" Text="Delete" Runat="server"></asp:button></td>
								<td class="Header3" width="7.5%"><INPUT id="cmdExcelV" style="WIDTH: 80px" onclick="ShowExcel()" type="button" value="Excel View"></td>
								<td class="Header3" width="10%"><asp:radiobuttonlist id="RdoList" runat="server" Width="180px" Height="16px" RepeatDirection="Horizontal">
										<asp:ListItem Value="1" Selected="True">Temperary</asp:ListItem>
										<asp:ListItem Value="2">Saved</asp:ListItem>
									</asp:radiobuttonlist></td>
								<td width="5%"><asp:button id="cmdClose" Width="75px" Text="Close" Runat="server"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td align="center" colSpan="3"><asp:label id="LblRights" Font-Size="10" Width="100%" Runat="server" Font-Bold="True"></asp:label></td>
				</tr>
			</table>
			<asp:button id="cmdShowExcel" style="Z-INDEX: 101; LEFT: 1024px; POSITION: absolute; TOP: 24px"
				runat="server" Width="0px"></asp:button><INPUT id="TxtQry" style="Z-INDEX: 102; LEFT: 1040px; POSITION: absolute; TOP: 16px" type="hidden"
				runat="server"></form>
	</body>
</HTML>
