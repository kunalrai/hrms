<%@ Page Language="vb" AutoEventWireup="false" Codebehind="IntrvSchedule.aspx.vb" Inherits="eHRMS.Net.IntrvSchedule" %>
<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>IntrvSchedule</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
		<SCRIPT language="javascript" src="Common.js"></SCRIPT>
		<script language="vbscript">
			Sub ValTime(S)
					If InStr(1, document.getElementById(S).value, ":") <> 3  Then
						alert ("Please enter time in correct format eg. 22:30 Or 03:25")
						document.getElementById(S).value="00:00"
						Exit Sub
					End If
			End Sub
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<br>
			<br>
			<TABLE cellSpacing="0" cellPadding="0" align="center" width="800" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Interview 
						Schedule....
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" rules="none" align="center" width="800" border="1" frame="border">
				<tr>
					<td width="15%"></td>
					<td width="35%"></td>
					<td width="25%"></td>
					<td width="25%"></td>
				</tr>
				<tr>
					<td colSpan="4"><asp:label id="LblErrMsg" runat="server" ForeColor="Red" Width="100%"></asp:label></td>
				</tr>
				<tr>
					<td>Interview Ref.No.</td>
					<td colSpan="2"><asp:textbox id="TxtIntrvrefno" Width="120px" CssClass="textbox" Runat="server" AutoPostBack="True"
							ReadOnly="True" ForeColor="#003366"></asp:textbox><asp:dropdownlist id="cmbIntrvrefid" runat="server" Width="150px" AutoPostBack="True" Visible="False"></asp:dropdownlist><asp:imagebutton id="btnList" Width="18px" Runat="server" ImageAlign="AbsMiddle" ImageUrl="Images\Find.gif"></asp:imagebutton><asp:imagebutton id="btnNew" Width="18px" Runat="server" ImageAlign="AbsMiddle" ImageUrl="Images\NewFile.ico"
							Height="19px"></asp:imagebutton></td>
				</tr>
				<tr>
					<td>Interviewer Name</td>
					<td><asp:dropdownlist id="cmbIntrvName" runat="server" Width="100%" CssClass="TextBox"></asp:dropdownlist></td>
					<td align="center">Interview Date</td>
					<td><cc1:dtp id="dtpInterview" runat="server" ToolTip="Interview Date" width="136px"></cc1:dtp></td>
				<tr>
					<td>Type</td>
					<td><asp:dropdownlist id="cmbType" runat="server" Width="85%" CssClass="TextBox"></asp:dropdownlist></td>
					<td>Job Requition No.</td>
					<td><asp:dropdownlist id="cmbReqNo" runat="server" Width="100%" CssClass="TextBox" AutoPostBack="True"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td>venue</td>
					<td colSpan="3"><asp:textbox id="TxtVenue" Width="100%" CssClass="TextBox" Runat="server" ForeColor="#003366"></asp:textbox></td>
					<td></td>
				</tr>
				<tr>
					<td>Venue Address</td>
					<td colSpan="3"><asp:textbox id="TxtAddress" Width="100%" CssClass="TextBox" Runat="server" ForeColor="#003366"></asp:textbox></td>
				</tr>
				<tr>
					<td>Venue E-Mail</td>
					<td><asp:textbox id="TxtEMail" Width="100%" CssClass="TextBox" Runat="server" ForeColor="#003366"></asp:textbox></td>
					<td align="center">Venue Contect No.</td>
					<td><asp:textbox id="TxtContectNo" Width="100%" CssClass="TextBox" Runat="server" ForeColor="#003366"></asp:textbox></td>
				</tr>
				<tr>
					<td vAlign="top">Common Note</td>
					<td colSpan="3"><asp:textbox id="TxtCommonNote" Width="100%" Runat="server" TextMode="MultiLine" Rows="3" ForeColor="#003366"></asp:textbox></td>
				</tr>
				<tr>
					<td colSpan="4">&nbsp;</td>
				</tr>
				<tr>
					<td colSpan="4">
						<TABLE id="TabgrdIntrv" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td>
									<div style="OVERFLOW: auto; WIDTH: 800px; HEIGHT: 150px"><asp:datagrid id="grdIntrv" runat="server" Width="784px" PageSize="20" AutoGenerateColumns="false">
											<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
											<HeaderStyle Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Top"
												BackColor="Gray"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="Select">
													<ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
													<ItemTemplate>
														<asp:CheckBox id="ChkSelect" Width="30px" ForeColor="#003366" Runat="server" Enabled="true"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn Visible="False" DataField="Res_Code" HeaderText="ResCode">
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Res_No" HeaderText="Resume ID">
													<ItemStyle HorizontalAlign="Left" Width="175px"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn Visible="False" DataField="Vacancy_Code" HeaderText="V_Code">
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Vacancy_RefNo" HeaderText="Job Requisition">
													<ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="EName" HeaderText="Name">
													<ItemStyle HorizontalAlign="Left" Width="175px"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Time(From)">
													<ItemStyle HorizontalAlign="Center" Width="75px"></ItemStyle>
													<ItemTemplate>
														<asp:TextBox id=TxtFrom onblur=ValTime(this.id) Width="75px" ForeColor="#003366" Runat="server" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Time_From") %>'>
														</asp:TextBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Time(To)">
													<ItemStyle HorizontalAlign="Center" Width="75px"></ItemStyle>
													<ItemTemplate>
														<asp:TextBox id=TxtTo onblur=ValTime(this.id) Width="75px" ForeColor="#003366" Runat="server" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Time_To") %>'>
														</asp:TextBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Note">
													<ItemStyle HorizontalAlign="Center" Width="75px"></ItemStyle>
													<ItemTemplate>
														<asp:TextBox id=TxtNote Width="175px" ForeColor="#003366" Runat="server" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Note") %>'>
														</asp:TextBox>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid></div>
								</td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td align="right" colSpan="4"><asp:button id="cmdSMail" runat="server" Width="90px" Text="Save &amp; Mail"></asp:button>&nbsp;
						<asp:button id="cmdSave" runat="server" Width="75px" Text="Save"></asp:button>&nbsp;
						<asp:button id="cmdClose" runat="server" Width="75px" Text="Close"></asp:button></td>
					</TD></tr>
			</table>
		</form>
	</body>
</HTML>
