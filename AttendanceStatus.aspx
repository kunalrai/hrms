<%@ Page Language="vb" AutoEventWireup="false" Codebehind="AttendanceStatus.aspx.vb" Inherits="eHRMS.Net.AttendanceStatus" %>
<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>AttendanceStatus</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<br>
			<br>
			<!--#include file=MenuBars.aspx-->
			<table cellSpacing="0" cellPadding="0" width="96%" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="15">&nbsp; 
						Attendance Status.....
					</TD>
					<TD class="headingCont" style="WIDTH: 576px" noWrap align="left" background="Images/TableMid.gif"
						height="19"></TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" style="WIDTH: 27px; HEIGHT: 19px" height="19" src="Images/TableRight.gif"
							width="27">
					</TD>
				</tr>
			</table>
			<table cellSpacing="2" cellPadding="0" rules="none" width="96%" align="center" border="1">
				<tr>
					<td colSpan="3"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 1px" colSpan="3"><asp:label id="LblErrMsg" runat="server" Width="320px" ForeColor="Red"></asp:label></td>
				</tr>
				<TR>
					<TD style="HEIGHT: 1px" colSpan="3" background="Images\headstripe.jpg">
						<asp:label id="LblPeriod" Runat="server" Text="Period" Font-Bold="True">Select Period</asp:label></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 1px" colSpan="3">
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" align="left" border="1">
							<TR>
								<TD width="20%" style="HEIGHT: 11px" colSpan="4">&nbsp;
									<asp:label id="LblFrom" Width="56px" Font-Bold="True" Runat="server" text="From">From</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
									&nbsp;
									<asp:label id="Label1" Font-Bold="True" Runat="server" text="To">To</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:label id="LblFor" Font-Bold="True" Runat="server" text="For"></asp:label></TD>
							</TR>
							<TR>
								<TD width="20%"><cc1:dtp id="DtpFrom" runat="server" ForeColor="#003366" ToolTip="From" width="150px"></cc1:dtp></TD>
								<TD width="20%"><cc1:dtp id="DtpTo" runat="server" ForeColor="#003366" ToolTip="To" width="150px"></cc1:dtp></TD>
								<TD width="30%"><FONT face="Verdana" color="#000066" size="2"><asp:dropdownlist id="CmbFor1" runat="server" Width="250px" AutoPostBack="True">
											<asp:ListItem></asp:ListItem>
											<asp:ListItem Value="All">All</asp:ListItem>
											<asp:ListItem Value="Emp_Code">Employee</asp:ListItem>
											<asp:ListItem Value="Loc_Code">Location</asp:ListItem>
											<asp:ListItem Value="Divi_Code">Employee Type</asp:ListItem>
											<asp:ListItem Value="Sect_Code">Section</asp:ListItem>
											<asp:ListItem Value="Dept_Code">Function</asp:ListItem>
											<asp:ListItem Value="Cost_Code">Cost Center</asp:ListItem>
											<asp:ListItem Value="Proc_Code">Responsibility</asp:ListItem>
											<asp:ListItem Value="Grd_Code">Lavel</asp:ListItem>
											<asp:ListItem Value="Dsg_Code">Designation</asp:ListItem>
											<asp:ListItem Value="PB_Code">Pay Bucket</asp:ListItem>
											<asp:ListItem Value="SFunc_Code">Sub Function</asp:ListItem>
											<asp:ListItem Value="SSFun_Code">Sub Sub Function</asp:ListItem>
										</asp:dropdownlist></FONT></TD>
								<TD width="30%"><asp:dropdownlist id="CmbFor2" runat="server" Width="250px" AutoPostBack="True" Height="27px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD width="20%" colSpan="4"><hr style="BORDER-BOTTOM: #993366 thin solid">
								</TD>
							</TR>
							<TR>
								<TD width="20%" colSpan="4">
									<DIV style="OVERFLOW: auto; WIDTH: 100%; COLOR: #cccccc; BORDER-TOP-STYLE: solid; BORDER-RIGHT-STYLE: solid; BORDER-LEFT-STYLE: solid; HEIGHT: 100%; BORDER-BOTTOM-STYLE: solid">
										<asp:datagrid id="GrdAttendance" Width="100%" Runat="server" AutoGenerateColumns="False">
											<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
											<HeaderStyle Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Top"
												BackColor="Gray"></HeaderStyle>
											<Columns>
												<asp:BoundColumn DataField="Emp_Code" HeaderText="Code"></asp:BoundColumn>
												<asp:BoundColumn DataField="Emp_Name" HeaderStyle-Width="13%" HeaderText="Name"></asp:BoundColumn>
												<asp:BoundColumn DataField="AtDate" HeaderText="Date"></asp:BoundColumn>
												<asp:BoundColumn DataField="Shift_From" HeaderText="Shift Timing From"></asp:BoundColumn>
												<asp:BoundColumn DataField="Shift_To" HeaderText="Shift Timing To"></asp:BoundColumn>
												<asp:BoundColumn DataField="In_Time" HeaderText="Arrival Time"></asp:BoundColumn>
												<asp:BoundColumn DataField="InEarly" HeaderText="Arrival Time Early"></asp:BoundColumn>
												<asp:BoundColumn DataField="InLate" HeaderText="Arrival Time Late"></asp:BoundColumn>
												<asp:BoundColumn DataField="Out_Time" HeaderText="Leaving Time"></asp:BoundColumn>
												<asp:BoundColumn DataField="OutEarly" HeaderText="Leaving Time Early"></asp:BoundColumn>
												<asp:BoundColumn DataField="OutLate" HeaderText="Leaving Time Late"></asp:BoundColumn>
												<asp:BoundColumn DataField="WORKED_HOURS" HeaderText="Worked Hour"></asp:BoundColumn>
												<asp:BoundColumn DataField="LvType" HeaderText="Status"></asp:BoundColumn>
												<asp:BoundColumn DataField="PayDate" HeaderText="Adjusted In"></asp:BoundColumn>
											</Columns>
											<PagerStyle Font-Size="XX-Small" Font-Bold="True" HorizontalAlign="Center" ForeColor="#003366"
												Mode="NumericPages"></PagerStyle>
										</asp:datagrid>
										<DIV></DIV>
									</DIV>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<tr>
				</tr>
				<tr>
					<td align="center" colSpan="3">
						<hr style="BORDER-BOTTOM: #993366 thin solid">
						&nbsp;
						<table cellSpacing="0" cellPadding="0" width="180" align="right" border="0" frame="box">
							<tr>
								<td><asp:button id="CmdRefresh" Width="75px" Text="Refresh" Runat="server"></asp:button>&nbsp;<asp:button id="CmdClose" runat="server" Width="75px" Text="Close"></asp:button></td>
							</tr>
						</table>
						&nbsp;
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
