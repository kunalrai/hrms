<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PayView.aspx.vb" Inherits="eHRMS.Net.PayView"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PayView</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
		<script language=javascript>
		function CloseWindow()
		{
			window.close();
		}
		</script>	
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx --><br>
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="650" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif"></TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="15">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Salary 
						Detail ....</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" height="19" src="Images/TableRight.gif" width="27"></TD>
				</tr>
			</TABLE>
			<TABLE borderColor="gray" cellSpacing="1" cellPadding="1" rules="none" width="650" align="center"
				border="1" frame="border">
				<tr>
					<td width="10%"></td>
					<td width="15%"></td>
					<td width="12%"></td>
					<td width="15%"></td>
					<td width="15%"></td>
					<td width="15%"></td>
					<td width="18%"></td>
				</tr>
				<tr>
					<td colSpan="7"><asp:label id="LblErrMsg" runat="server" Width="100%" ForeColor="Red"></asp:label></td>
				</tr>
				<tr>
					<TD style="BORDER-RIGHT: #000000 thin solid; BORDER-TOP: #000000 thin solid; BORDER-LEFT: #000000 thin solid; BORDER-BOTTOM: #000000 thin solid"
						class="Header3">&nbsp;Code:</TD>
					<td style="BORDER-RIGHT: #000000 thin solid; BORDER-TOP: #000000 thin solid; BORDER-BOTTOM: #000000 thin solid"
						class="Header3"><asp:label id="LblCode" runat="server" Width="100%" ForeColor="#003366"></asp:label></td>
					<td style="BORDER-RIGHT: #000000 thin solid; BORDER-TOP: #000000 thin solid; BORDER-BOTTOM: #000000 thin solid"
						class="Header3">&nbsp;Name:</td>
					<td style="BORDER-RIGHT: #000000 thin solid; BORDER-TOP: #000000 thin solid; BORDER-BOTTOM: #000000 thin solid"
						colSpan="2" class="Header3"><asp:label id="LblName" runat="server" Width="100%"></asp:label></td>
					<td style="BORDER-RIGHT: #000000 thin solid; BORDER-TOP: #000000 thin solid; BORDER-BOTTOM: #000000 thin solid"
						class="Header3">&nbsp;Pay Date:</td>
					<td style="BORDER-RIGHT: #000000 thin solid; BORDER-TOP: #000000 thin solid; BORDER-BOTTOM: #000000 thin solid"
						class="Header3"><asp:label id="LblPayDate" runat="server" Width="100%"></asp:label></td>
				</tr>
				<tr id="trAcHeads">
					<td colSpan="7">
						<div style="OVERFLOW: auto; WIDTH: 640px; COLOR: #cccccc; HEIGHT: 441px"><asp:datagrid id="GrdAcHeads" runat="server" Width="100%" AllowSorting="True" AllowPaging="False"
								AutoGenerateColumns="False">
								<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
								<HeaderStyle BackColor="GRAY" HorizontalAlign="Center"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderStyle-Font-Bold="True" HeaderStyle-ForeColor="White" HeaderText="AC Head">
										<ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="LblFldName" runat="server">
												<%# DataBinder.Eval(Container.DataItem, "FIELD_NAME") %>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderStyle-Font-Bold="True" HeaderStyle-ForeColor="White" HeaderText="Description">
										<ItemStyle HorizontalAlign="Left" Width="60%"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDesc_Ern" runat="server">
												<%# DataBinder.Eval(Container.DataItem, "FIELD_DESC") %>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderStyle-Font-Bold="True" HeaderStyle-ForeColor="White" HeaderText="Amount" ItemStyle-HorizontalAlign="Right">
										<ItemStyle HorizontalAlign="Right" Width="20%"></ItemStyle>
										<ItemTemplate>
											<asp:TextBox Width="100%" ForeColor="#003366" runat="server" ReadOnly=True style="TEXT-ALIGN: right" ID="txtAmount_Ern" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Field_N") %>' />
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid></div>
					</td>
				</tr>
				<TR height="15">
					<TD style="MARGIN-LEFT: 15px; MARGIN-RIGHT: 15px; BORDER-BOTTOM: #993300 thin groove; HEIGHT: 7px"
						colSpan="7"></TD>
				</TR>
				<tr>
					<td colSpan="5"></td>
					<td align="right" colSpan="2"><asp:button id="BtnClose" Width="75"   Text="Close" Runat="server"></asp:button>&nbsp;
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
