<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TrainingRequest.aspx.vb" Inherits="eHRMS.Net.TrainingRequest"%>
<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>TrainingRequest</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
		<script>
			function ShowHide(argName)
				{
					Menu = new String(argName)
					if (document.getElementById('tr' + Menu).style.display == "none")
						{
						document.getElementById('tr' + Menu).style.display = "block";
						document.getElementById('img' + Menu).src = "Minus.gif";
						}
					else
						{
						document.getElementById('tr' + Menu).style.display = "none";
						document.getElementById('img' + Menu).src = "plus.gif";
						}
				}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx -->
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="800" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Training 
						Request....
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" rules="none" width="800" border="1" frame="border">
				<tr>
					<td></td>
					<td></td>
					<td></td>
					<td></td>
					<td></td>
				</tr>
				<tr>
					<td colSpan="5"><asp:label id="LblErrMsg" runat="server" ForeColor="Red" Width="100%" Font-Size="8pt"></asp:label></td>
				</tr>
				<tr>
					<td class="Header3" width="10%">From</td>
					<td class="Header3" width="20%"><cc1:dtp id="dtpFromDate" runat="server" Width="125px"></cc1:dtp></td>
					<td class="Header3" width="10%">To</td>
					<td class="Header3" width="20%"><cc1:dtp id="dtpToDate" runat="server" Width="125px" ReadOnlyText="False"></cc1:dtp></td>
					<td class="Header3" width="20%"><asp:button id="cmdShow" runat="server" Width="60px" Text="Show"></asp:button></td>
				</tr>
				<tr>
					<td class="Header3" background="Images\headstripe.jpg" colSpan="5">&nbsp;<B>Training 
							Session(s)</B></td>
				</tr>
				<tr>
					<td colSpan="5">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 120px">
							<asp:datagrid id="GrdTrainReq" runat="server" Width="100%" AutoGenerateColumns="False">
								<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
								<HeaderStyle Font-Names="Arial" Font-Size="10px" Font-Bold="True" HorizontalAlign="Center" ForeColor="White"
									VerticalAlign="Top" BackColor="Gray" Height="20px"></HeaderStyle>
								<Columns>
									<asp:BoundColumn DataField="TrainCalCode" HeaderText="Code">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="TrainName" HeaderText="Training Name">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="StartDate" DataFormatString="{0:dd/MMM/yyyy}" HeaderText="Start Date">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="EndDate" DataFormatString="{0:dd/MMM/yyyy}" HeaderText="End Date">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="TrainType" HeaderText="Type">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="75px"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Seats" HeaderText="Total Seats">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Hours" HeaderText="Hours">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Location" HeaderText="Location">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Select">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
										<ItemTemplate>
											<input type="checkbox" ID="ChkEmp" runat="server">
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid>
						</div>
					</td>
				</tr>
				<tr>
					<td colSpan="5">&nbsp;</td>
				</tr>
				<tr id="trDepartment" style="DISPLAY: none" runat="server">
					<td class="Header3">Department</td>
					<td class="Header3"><asp:dropdownlist id="cmbDepartment" runat="server" Width="200px" AutoPostBack="True"></asp:dropdownlist></td>
					<td class="Header3" width="10%" colSpan="3" style="HEIGHT: 33px"></td>
				</tr>
				<tr id="EmployeeTr" style="DISPLAY: none" runat="server">
					<td class="Header3" background="Images\headstripe.jpg" colSpan="5" style="HEIGHT: 5px"><IMG id="imgEmployee" onclick="ShowHide('Employee')" src="Images\Minus.gif">&nbsp;<B>Subordinate(s)</B></td>
				</tr>
				<tr id="trEmployee" runat="server">
					<td colSpan="5" runat="server">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 300px; BORDER-BOTTOM-STYLE: solid">
							<asp:datagrid id="GrdEmployee" runat="server" Width="100%" AutoGenerateColumns="False">
								<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
								<HeaderStyle Font-Size="10px" Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" Height="20px"
									ForeColor="White" VerticalAlign="Top" BackColor="Gray"></HeaderStyle>
								<Columns>
									<asp:BoundColumn DataField="Code" HeaderText="Code">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="EMPNAME" HeaderText="Name">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="DEPT_NAME" HeaderText="Department">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="DSG_NAME" HeaderText="Designation">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Select">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
										<ItemTemplate>
											<input type="checkbox" runat="server" ID="ChkSubEmp">
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid>
						</div>
					</td>
				</tr>
				<tr>
					<td align="right" colSpan="5"><asp:button id="cmdSave" runat="server" Width="80px" Text="Save"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:button id="cmdClose" runat="server" Width="80px" Text="Close"></asp:button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
