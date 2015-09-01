<%@ Page Language="vb" AutoEventWireup="false" Codebehind="JobRequisition.aspx.vb" Inherits="eHRMS.Net.JobRequisition" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>JobRequisition</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
		<script language="javascript">
			function CheckNum(args)
				{
				 	if (isNaN(document.getElementById(args).value)==true)
				 		{
				 			alert("This field must be numeric type.");
				 			document.getElementById(args).value = "";
				 		}
				}
		</script>
	</HEAD>
	<body topMargin="5" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx --><br>
			<br>
			<br>
			<TABLE cellSpacing="0" cellPadding="0" align="center" width="700" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Job 
						Requisition....
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" align="center" rules="none" width="700" border="1"
				frame="border">
				<tr>
					<td width="15%"></td>
					<td width="35%"></td>
					<td width="15%"></td>
					<td width="35%"></td>
				</tr>
				<tr>
					<td colSpan="4"><asp:label id="LblErrMsg" runat="server" Width="100%" ForeColor="Red"></asp:label></td>
				</tr>
				<tr>
					<td style="HEIGHT: 20px">&nbsp;Requisition ID*</td>
					<td colSpan="3" style="HEIGHT: 20px"><asp:textbox id="Txtrefno" Width="120px" ReadOnly="True" AutoPostBack="True" Runat="server" CssClass="textbox"
							ForeColor="#003366"></asp:textbox>
						<asp:dropdownlist id="cmbrefid" runat="server" Width="150px" AutoPostBack="True" Visible="False"></asp:dropdownlist>
						<asp:imagebutton id="btnList" Width="18px" Runat="server" ImageUrl="Images\Find.gif" ImageAlign="AbsMiddle"></asp:imagebutton>
						<asp:imagebutton id="btnNew" Width="18px" Runat="server" ImageUrl="Images\NewFile.ico" ImageAlign="AbsMiddle"
							Height="19px"></asp:imagebutton></td>
				</tr>
				<tr>
					<td>&nbsp;Description*</td>
					<td><asp:textbox id="TxtDesc" runat="server" Width="100%" CssClass="textbox" ForeColor="#003366"></asp:textbox></td>
					<td>&nbsp;Department*</td>
					<td><asp:dropdownlist id="cmbDept" runat="server" Width="100%"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td>&nbsp;Process Templete*</td>
					<td colSpan="1"><asp:dropdownlist id="cmbTemplete" Width="100%" Runat="server"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td colSpan="4">&nbsp;</td>
				</tr>
				<tr>
					<td colSpan="4">
						<TABLE id="Tabgrdreq" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td>
									<div style="OVERFLOW: auto; WIDTH: 700px; BORDER-TOP-STYLE: solid; BORDER-RIGHT-STYLE: solid; BORDER-LEFT-STYLE: solid; HEIGHT: 200px; BORDER-BOTTOM-STYLE: solid"><asp:datagrid id="grdReq" runat="server" Width="1300px" AutoGenerateColumns="False">
											<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
											<HeaderStyle Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Top"
												BackColor="Gray"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="Designation">
													<ItemStyle HorizontalAlign="Center" Width="13%"></ItemStyle>
													<ItemTemplate>
														<select style="WIDTH: 100%" id="cmbDsg" runat="server">
														</select>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Exp.From">
													<ItemStyle HorizontalAlign="Left" Width="3%"></ItemStyle>
													<ItemTemplate>
														<asp:TextBox id=txtFrom onblur=CheckNum(this.id) runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Exp_From") %>' MaxLength="2">
														</asp:TextBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Exp.To">
													<ItemStyle HorizontalAlign="Left" Width="4%"></ItemStyle>
													<ItemTemplate>
														<asp:TextBox id=txtTo onblur=CheckNum(this.id) runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Exp_To") %>' MaxLength="2">
														</asp:TextBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="No. of Req.">
													<ItemStyle HorizontalAlign="Left" Width="4%"></ItemStyle>
													<ItemTemplate>
														<asp:TextBox id=txtPost onblur=CheckNum(this.id) runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "NoOfPost") %>' MaxLength="3">
														</asp:TextBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Skills Detail*">
													<ItemStyle HorizontalAlign="Left" Width="12%"></ItemStyle>
													<ItemTemplate>
														<asp:TextBox id=txtSkills runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Reqskills") %>' TextMode="MultiLine">
														</asp:TextBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Salary (Upto)">
													<ItemStyle HorizontalAlign="Left" Width="5%"></ItemStyle>
													<ItemTemplate>
														<asp:TextBox id=TxtSalary onblur=CheckNum(this.id) runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Salary") %>' MaxLength="8">
														</asp:TextBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Job Role*">
													<ItemStyle HorizontalAlign="Left" Width="12%"></ItemStyle>
													<ItemTemplate>
														<asp:TextBox id=TxtRole runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "JobRole") %>' TextMode="MultiLine">
														</asp:TextBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:ButtonColumn Text="Add" CommandName="Select">
													<ItemStyle HorizontalAlign="Center" Width="2%"></ItemStyle>
												</asp:ButtonColumn>
												<asp:TemplateColumn HeaderText="Qualification*">
													<ItemStyle HorizontalAlign="Left" Width="13%"></ItemStyle>
													<ItemTemplate>
														<asp:TextBox id=TxtQual runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox" ReadOnly="True" Text='<%# DataBinder.Eval(Container.DataItem, "Qual") %>' TextMode="MultiLine">
														</asp:TextBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn Visible="False" DataField="QualCode">
													<ItemStyle Width="0px"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Other Qualification">
													<ItemStyle HorizontalAlign="Left" Width="11%"></ItemStyle>
													<ItemTemplate>
														<asp:TextBox id=TxtOtherQual runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "OthQual") %>' TextMode="MultiLine">
														</asp:TextBox>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid></div>
								</td>
							</tr>
							<tr>
								<td align="left"><asp:button id="cmdReqAdd" runat="server" Width="75px" Text="Add"></asp:button></td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr id="TrQual" style="DISPLAY: none" runat="server">
					<td colSpan="4">
						<table borderColor="gainsboro" cellSpacing="0" cellPadding="0" rules="none" width="100%"
							border="1" frame="border">
							<tr>
								<td><asp:label id="LblDspl" Runat="server" forecolor="#003366" Font-Bold="True" Font-Size="12px"
										width="100%"></asp:label></td>
							</tr>
							<tr>
								<td><asp:listbox id="LstQual" runat="server" Width="100%" SelectionMode="Multiple" Rows="8"></asp:listbox></td>
							</tr>
							<tr>
								<td align="right"><asp:button id="cmdSaveQual" runat="server" Width="120px" Text="Save &amp; Close"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td><B>APPROVED BY:</B></td>
					<td><asp:checkbox id="ChkHOD" Width="75" Runat="server" Text="HOD" Font-Bold="True"></asp:checkbox></td>
					<td><asp:checkbox id="ChkHRADMIN" Width="96px" Runat="server" Text="HR ADMIN." Font-Bold="True"></asp:checkbox></td>
					<td align="center"><asp:checkbox id="ChkVP" Width="120px" Runat="server" Text="VP OPERATION" Font-Bold="True"></asp:checkbox></td>
				</tr>
				<tr>
					<td colSpan="4">&nbsp;</td>
				</tr>
				<tr>
					<td align="right" colSpan="4">&nbsp;&nbsp;&nbsp;
						<asp:button id="cmdReqSave" runat="server" Width="75px" Text="Save"></asp:button>&nbsp;
						<asp:button id="cmdReqclose" runat="server" Width="75px" Text="Close"></asp:button></td>
				</tr>
				<tr>
					<td class="Header3" align="right" colSpan="4">* Mandatory Fields</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
