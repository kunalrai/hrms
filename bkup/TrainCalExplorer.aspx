<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TrainCalExplorer.aspx.vb" Inherits="eHRMS.Net.TrainCalExplorer"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>TrainCalExplorer</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx -->
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Training 
						Explorer....
					</TD>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" rules="none" width="100%" border="1" frame="border">
				<tr>
					<td width="1%" bgColor="gray">&nbsp;</td>
					<td width="98%" colSpan="3"><asp:label id="LblMsg" runat="server" Width="100%" Font-Size="11px" ForeColor="Red" Visible="False"></asp:label></td>
					<td width="1%" bgColor="gray">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="5" bgColor="darkgray">&nbsp;</td>
				</tr>
				<tr>
					<td width="1%" bgColor="gray">&nbsp;</td>
					<td vAlign="top" width="75%">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 350px">
							<asp:datagrid id="GrdTrainCalender" runat="server" Width="100%" PageSize="15" AutoGenerateColumns="False">
								<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
								<HeaderStyle Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Top"
									BackColor="Gray"></HeaderStyle>
								<Columns>
									<asp:ButtonColumn Text="Edit" ButtonType="LinkButton" ItemStyle-ForeColor="#003366" HeaderText="Edit"
										CommandName="Edit">
										<ItemStyle Width="50px"></ItemStyle>
									</asp:ButtonColumn>
									<asp:ButtonColumn Text="Select" Visible="False" HeaderText="Select" CommandName="Select">
										<ItemStyle Width="50px"></ItemStyle>
									</asp:ButtonColumn>
									<asp:BoundColumn DataField="TrainCalCode" HeaderText="Code" ItemStyle-Width="60px"></asp:BoundColumn>
									<asp:BoundColumn DataField="TrainDesc" HeaderText="Training Description" ItemStyle-Width="300px"></asp:BoundColumn>
									<asp:BoundColumn DataField="Code" HeaderText="Traininig ID" ItemStyle-Width="75px"></asp:BoundColumn>
									<asp:BoundColumn DataField="From" DataFormatString="{0:dd/MMM/yyyy}" HeaderText="From" ItemStyle-Width="75px"></asp:BoundColumn>
									<asp:BoundColumn DataField="To" DataFormatString="{0:dd/MMM/yyyy}" HeaderText="To" ItemStyle-Width="75px"></asp:BoundColumn>
									<asp:BoundColumn DataField="Location" HeaderText="Location" ItemStyle-Width="100px"></asp:BoundColumn>
									<asp:BoundColumn DataField="Trainer" HeaderText="Trainer" ItemStyle-Width="100px"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Details">
										<ItemTemplate>
											<a href='<%# DataBinder.Eval(Container.DataItem, "TrainFile")%>'  target='<%# DataBinder.Eval(Container.DataItem, "Code")%>'>
												Down</a>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle NextPageText="&amp;minus;&amp;gt;" Font-Underline="True" Font-Names="Verdana" PrevPageText="&amp;lt;&amp;minus;"
									ForeColor="#CB3908" BackColor="#E0E0E0" Mode="NumericPages"></PagerStyle>
							</asp:datagrid>
						</div>
					</td>
					<td width="1%" bgColor="gray">&nbsp;</td>
					<td vAlign="top" width="22%">
						<table cellSpacing="0" cellPadding="0" width="100%">
							<tr>
								<td class="Header3" width="20%"><asp:radiobutton id="RdoAll" runat="server" Text="All" AutoPostBack="True" GroupName="a"></asp:radiobutton></td>
								<td class="Header3" width="27%"><asp:radiobutton id="RdoSelected" runat="server" Text="Selected" Checked="True" AutoPostBack="True"
										GroupName="a"></asp:radiobutton></td>
								<td class="Header3" width="27%"><asp:radiobutton id="RdoSuggested" runat="server" Text="Suggested" AutoPostBack="True" GroupName="a"></asp:radiobutton></td>
								<td class="Header3" width="26%"><asp:radiobutton id="RdoRequested" runat="server" Text="Requested" AutoPostBack="True" GroupName="a"></asp:radiobutton></td>
							</tr>
							<tr>
								<td colSpan="4"><asp:datagrid id="GrdEmployee" runat="server" Width="100%" PageSize="18" AllowPaging="True" AllowSorting="True"
										AutoGenerateColumns="False">
										<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
										<HeaderStyle Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Top"
											BackColor="Gray"></HeaderStyle>
										<Columns>
											<asp:ButtonColumn Text="UnSelected" HeaderText="Select" ItemStyle-Width="75PX" CommandName="Select"></asp:ButtonColumn>
											<asp:BoundColumn DataField="Name" HeaderText="Employee Name">
												<ItemStyle Width="200px"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="Emp_Code" HeaderText="Code">
												<ItemStyle Width="50px"></ItemStyle>
											</asp:BoundColumn>
										</Columns>
										<PagerStyle NextPageText="&amp;minus;&amp;gt;" Font-Underline="True" Font-Names="Verdana" PrevPageText="&amp;lt;&amp;minus;"
											ForeColor="#CB3908" BackColor="#E0E0E0" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></td>
							</tr>
						</table>
					</td>
					<td width="1%" bgColor="gray">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="5" bgColor="darkgray">&nbsp;</td>
				</tr>
				<tr>
					<td width="1%" bgColor="gray">&nbsp;</td>
					<td class="Header3" align="left" colSpan="2"><asp:button id="cmdNew" runat="server" Width="80px" Text="New"></asp:button></td>
					<td class="Header3" align="right"><asp:button id="cmdClose" runat="server" Width="80px" Text="Close"></asp:button></td>
					<td width="1%" bgColor="gray">&nbsp;</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
