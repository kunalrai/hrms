<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="LeavEntry.aspx.vb" Inherits="eHRMS.Net.LeavEntry"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>LeavEntry</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
		<script language="vbscript">
			sub Validate(S)
					If InStr(1, document.getElementById(S).value, ":") <> 3  Then
						alert ("Please enter time in correct format eg. 22:30 Or 03:25")
						document.getElementById(S).value="00:00"
						Exit Sub
					End If
			end sub
			
			function ValidateCtrl()
				If trim(document.getElementById("TxtCode").Value) = "" Then
					msgbox ("Employee Code Can not be left blank.")
					ValidateCtrl = false
					exit function
				End If
				
				If document.getElementById("cmbLvType").Value = "" Then
					msgbox ("Please select leave type from the list")
					ValidateCtrl = false
					exit function
				End If
				
				dim dtFromDate, dtTo
				dtFromDate = cdate(document.getElementById("dtpFromDatecmbDD").Value & "/" & document.getElementById("dtpFromDatecmbMM").Value & "/" & document.getElementById("dtpFromDatecmbYY").Value)
				dtTo = cdate(document.getElementById("DtpTocmbDD").Value & "/" & document.getElementById("DtpTocmbMM").Value & "/" & document.getElementById("DtpTocmbYY").Value)
				
				If Left(document.getElementById("cmbLvType").Value, 1) = "P" And Right(document.getElementById("cmbLvType").Value, 1) = "P" Then					
					If CDate(dtFromDate) <> CDate(dtTo) Then
						msgbox  ("For Present Entry,  From & To Date must be same.")
						ValidateCtrl = false
						exit function
					End If
				End If

				If Left(document.getElementById("cmbLvType").Value, 1) = "P" And Right(document.getElementById("cmbLvType").Value, 1) = "P" Then
					If CDate(dtFromDate) <> CDate(dtTo) Then
						msgbox ("For half day leave From & To Date must be same.")
						ValidateCtrl = false
						exit function
					End If
				End If

				If Left(document.getElementById("cmbLvType").Value, 1) = "P" And Right(document.getElementById("cmbLvType").Value, 1) = "P" Then
				
					if IsNumeric(document.getElementById("TxtDays").Value) Then
						if document.getElementById("TxtDays").Value = 0  then
							msgbox ("Leave Day(s) must be greater than zero.")
							ValidateCtrl = false
							exit function
						end if
					else
						document.getElementById("TxtDays").Value = 0
						msgbox ("Leave Day(s) must be greater than zero.")
						ValidateCtrl = false
						exit function
					end if
					
				End If
            
				ValidateCtrl = true
			end function
						
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx --><br>
			<TABLE cellSpacing="0" cellPadding="0" width="800" border="0" align="center">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Leave 
						Entry ....
					</TD>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" width="800" border="1" align="center">
				<tr borderColor="white">
					<td colSpan="6"><asp:label id="lblMsg" runat="server" Width="100%" ForeColor="Red" Font-Size="11px"></asp:label></td>
				</tr>
				<tr>
					<td colSpan="6">
						<table borderColor="#003366" cellSpacing="0" cellPadding="0" rules="all" width="100%" align="left"
							border="1" frame="box">
							<tr>
								<td class="header3" align="center" width="55%">Leave Status</td>
								<td class="header3" align="center" width="40%">Holidays List</td>
							</tr>
							<tr vAlign="top" height="120">
								<td width="55%"><asp:datagrid id="GrdLeaveType" runat="server" Width="100%" PageSize="6" AllowSorting="True" AllowPaging="True"
										AutoGenerateColumns="False">
										<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
										<HeaderStyle Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Top"
											BackColor="Gray"></HeaderStyle>
										<Columns>
											<asp:BoundColumn DataField="LVDESC" HeaderText="Leave Type">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left" Width="120px"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="Credited" HeaderText="Total Credited">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="Availed" HeaderText="Availed">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="Balance" HeaderText="Balance">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
											</asp:BoundColumn>
										</Columns>
										<PagerStyle Mode="NumericPages"></PagerStyle>
									</asp:datagrid></td>
								<td width="45%"><asp:datagrid id="GrdHolidays" runat="server" Width="100%" PageSize="6" AllowPaging="True" AutoGenerateColumns="False">
										<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
										<HeaderStyle Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Top"
											BackColor="Gray"></HeaderStyle>
										<Columns>
											<asp:BoundColumn DataField="HDATE" HeaderText="Date" DataFormatString="{0:dd/MMM/yyyy}">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left" Width="25%"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="HDESC" HeaderText="Description">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center" Width="75%"></ItemStyle>
											</asp:BoundColumn>
										</Columns>
										<PagerStyle Mode="NumericPages"></PagerStyle>
									</asp:datagrid></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="Header3" width="44" style="WIDTH: 44px; HEIGHT: 13px">Code</td>
					<td class="Header3" width="313" colSpan="3" style="WIDTH: 313px; HEIGHT: 13px"><asp:textbox id="TxtCode" Width="80" CssClass="textbox" AutoPostBack="True" Runat="server" ForeColor="#003366"></asp:textbox><asp:dropdownlist id="cmbEmp" runat="server" Width="150" Visible="False" AutoPostBack="True"></asp:dropdownlist><asp:imagebutton id="btnList" Width="18px" Runat="server" Height="19px" ImageUrl="Images\Find.gif"
							ImageAlign="AbsMiddle"></asp:imagebutton></td>
					<td class="Header3" width="10%" colSpan="2" style=" HEIGHT: 13px"><asp:label id="LblName" Runat="server"></asp:label></td>
				</tr>
				<tr>
					<td colSpan="6">&nbsp;</td>
				</tr>
				<tr>
					<td class="Header3" align="center" colSpan="4" style="WIDTH: 373px; HEIGHT: 14px"><b>Leave 
							Type</b></td>
					<td class="Header3" align="left" colSpan="2" style="HEIGHT: 14px"><asp:dropdownlist id="cmbLvType" runat="server" Width="250px"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td colSpan="6">&nbsp;</td>
				</tr>
				<tr>
					<td class="Header3" width="20" style="WIDTH: 20px; HEIGHT: 46px">From</td>
					<td class="Header3" width="155" style="WIDTH: 155px; HEIGHT: 46px">
						<cc1:dtpcombo id="dtpFromDate" runat="server" Width="176px" DateValue="2005-08-30" ToolTip="Leave Start Date"></cc1:dtpcombo></td>
					<td class="Header3" width="2%" style="HEIGHT: 46px">To</td>
					<td class="Header3" style="WIDTH: 155px; HEIGHT: 46px">
						<cc1:dtpcombo id="DtpTo" runat="server" Width="120px" DateValue="2005-08-30" ToolTip="Leave End Date"></cc1:dtpcombo></td>
					<td class="Header3" width="40%" colSpan="2" style="HEIGHT: 46px">&nbsp;
						<table cellSpacing="0" cellPadding="0" width="317" style="WIDTH: 317px; HEIGHT: 24px">
							<tr vAlign="middle">
								<td align="center" style="WIDTH: 231px">&nbsp;<asp:button id="cmdCalDays" runat="server" Width="100px" Text="Calculate Days"></asp:button></td>
								<td>Days</td>
								<td><asp:textbox id="TxtDays" Width="105px" CssClass="textbox" Runat="server" ReadOnly="True" ForeColor="#003366"></asp:textbox></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colSpan="6">
						<table cellSpacing="0" cellPadding="0" width="100%">
							<tr>
								<td class="Header3" align="left" width="114" style="WIDTH: 114px"><b>Shift</b></td>
								<td class="Header3" align="left" width="262" style="WIDTH: 262px"><asp:dropdownlist id="cmbShift" runat="server" Width="200px"></asp:dropdownlist></td>
								<td class="Header3" align="left" width="157" style="WIDTH: 157px">&nbsp;</td>
								<td class="Header3" align="center" width="10%"><b>Timing</b>
								<td class="header3" width="7%"><INPUT class="Textbox" id="TxtInTime" onblur='Validate("TxtInTime")' style="WIDTH: 100%; COLOR: #003366"
										type="text" value="00:00" runat="server"></td>
								<td class="header3" width="7%"><INPUT class="TextBox" id="TxtOutTime" onblur='Validate("TxtOutTime")' style="WIDTH: 100%; COLOR: #003366"
										type="text" value="00:00" runat="server"></td>
								<td class="Header3" align="left" width="33%">&nbsp;</td>
							</tr>
							<tr>
								<td colSpan="7">&nbsp;</td>
							</tr>
							<tr>
								<td class="Header3" width="114" style="WIDTH: 114px">Application Date</td>
								<td class="Header3" width="262" style="WIDTH: 262px">
									<cc1:dtpcombo id="DtpAppDate" runat="server" Width="176px" DateValue="2005-08-30" ToolTip="Leave Application Date"></cc1:dtpcombo></td>
								<td class="Header3" align="center" width="25%" colSpan="2">Leave Adjusted In</td>
								<td class="Header3" width="40%" colSpan="3"><asp:dropdownlist id="cmbLvAdjIn" runat="server"></asp:dropdownlist></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="6">&nbsp;</td>
				</tr>
				<tr>
					<td class="Header3" align="left" style="WIDTH: 55px"><asp:button id="cmdDelete" runat="server" Width="80px" Text="Delete"></asp:button></td>
					<td class="Header3" align="right" colSpan="3" style="WIDTH: 278px">&nbsp;</td>
					<td class="Header3" align="right" colSpan="2"><asp:button id="cmdSave" runat="server" Width="80px" Text="Save"></asp:button>&nbsp;<asp:button id="cmdClose" runat="server" Width="80px" Text="Close"></asp:button></td>
				</tr>
				<TR>
					<TD colSpan="6">
						<table id="T1" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td></td>
							</tr>
							<tr>
								<td><asp:datagrid id="GrdDelete" runat="server" Width="100%" AutoGenerateColumns="False">
										<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
										<HeaderStyle Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Top"
											BackColor="Gray"></HeaderStyle>
										<Columns>
											<asp:BoundColumn DataField="Emp_Name" HeaderText="Name">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="AppDate" HeaderText="Application Date" DataFormatString="{0:dd/MMM/yyyy}">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center" Width="130px"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="LVDAYS" HeaderText="Days">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="AtDate">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center" Width="0px"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="AttDate" HeaderText="Leave Date" DataFormatString="{0:dd/MMM/yyyy}">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="LVTYPE">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center" Width="0px"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="LVDESC" HeaderText="Leave Type">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center" Width="250px"></ItemStyle>
											</asp:BoundColumn>
											<asp:TemplateColumn HeaderText="Delete">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center" Width="5px"></ItemStyle>
												<ItemTemplate>
													<asp:CheckBox id="ChkCheck" ForeColor="#003366" Runat="server"></asp:CheckBox>
												</ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
										<PagerStyle Mode="NumericPages"></PagerStyle>
									</asp:datagrid></td>
							</tr>
							<tr>
								<td class="Header3" align="right"><asp:button id="cmdProceed" runat="server" Width="80px" Text="Proceed"></asp:button></td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<TD align="center" colSpan="6"><asp:label id="lblAlready" runat="server" Width="784px" ForeColor="#003366" Font-Size="11px"
							Visible="False" Height="5px"></asp:label></TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
