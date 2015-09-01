<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TrainingCalender.aspx.vb" Inherits="eHRMS.Net.TrainingCalender"%>
<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>TrainingCalender</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
		<SCRIPT language="javascript" src="Common.js"></SCRIPT>
		<script>
			function ShowHide1()
			{
				document.getElementById("TBHeadGeneral").style.backgroundColor = "#666666" ;  
				document.getElementById("TBHeadSkills").style.backgroundColor = "#cecbce" ;  
				document.getElementById("TBHeadSkills").style.color = "#003366" ; 
				document.getElementById("TBHeadGeneral").style.color = "White" ;
				document.getElementById("TBLGeneral").style.display = "block" ;
				document.getElementById("TBLSkills").style.display = "none" ;  
			}
			
			function ShowHide()
			{
				document.getElementById("TBHeadGeneral").style.backgroundColor = "#cecbce" ;  
				document.getElementById("TBHeadSkills").style.backgroundColor = "#666666" ;  
				document.getElementById("TBHeadSkills").style.color = "White" ; 
				document.getElementById("TBHeadGeneral").style.color = "#003366" ;
				document.getElementById("TBLGeneral").style.display = "none" ;  
				document.getElementById("TBLSkills").style.display = "block" ;  
			}
			
		</script>
	</HEAD>
	<body onload="ShowHide1();" MS_POSITIONING="GridLayout">
		<br>
		<br>
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="800" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Training 
						Calendar....
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" rules="none" width="800" border="1" frame="border">
				<tr>
					<td width="15%"></td>
					<td width="30%"></td>
					<td width="15%"></td>
					<td width="20%"></td>
					<td width="20%"></td>
				</tr>
				<tr>
					<td colSpan="5"><asp:label id="LblErrMsg" runat="server" ForeColor="Red" Width="100%" Font-Size="8pt"></asp:label></td>
				</tr>
				<tr>
					<td width="100" style="HEIGHT: 16px">
						<table id="TBHeadGeneral" cellSpacing="0" cellPadding="0" width="100%" bgColor="#666666"
							border="0" runat="server">
							<tr>
								<td width="10"><IMG alt="" src="Images/SplitLeft.gif" border="0"></td>
								<td style="CURSOR: hand" onclick="ShowHide1();" align="center"><b>General</b></td>
								<td width="10"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
							</tr>
						</table>
					</td>
					<td width="100" colSpan="4" style="HEIGHT: 16px">
						<table id="TBHeadSkills" style="DISPLAY: none" cellSpacing="0" cellPadding="0" width="100%"
							bgColor="#cecbce" border="0" runat="server">
							<tr>
								<td width="10"><IMG alt="" src="Images/SplitLeft.gif" border="0"></td>
								<td style="CURSOR: hand" onclick="ShowHide();" align="center"><b>Skills</b></td>
								<td width="10"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="5">
						<table id="TBLGeneral" cellSpacing="0" cellPadding="0" rules="none" width="800" border="1"
							frame="border" runat="server">
							<tr>
								<td width="15%"></td>
								<td width="30%"></td>
								<td width="15%"></td>
								<td width="20%"></td>
								<td width="20%"></td>
							</tr>
							<tr>
								<td>Code</td>
								<td colSpan="2"><asp:textbox id="TxtCode" runat="server" Width="75px" CssClass="TextBox" ForeColor="#003366"></asp:textbox><asp:dropdownlist id="cmbCode" runat="server" Width="200" AutoPostBack="True" Visible="False"></asp:dropdownlist><asp:imagebutton id="btnList" Width="18px" Runat="server" ImageAlign="AbsMiddle" ImageUrl="Images\Find.gif"
										Height="19px"></asp:imagebutton><asp:imagebutton id="btnNew" Width="18px" Runat="server" ImageAlign="AbsMiddle" ImageUrl="Images\NewFile.ico"
										Height="19px"></asp:imagebutton></td>
								<td>Type</td>
								<td><asp:dropdownlist id="cmbType" Width="100%" Runat="server">
										<asp:ListItem Selected="True" Value="1">Internal</asp:ListItem>
										<asp:ListItem Value="2">External</asp:ListItem>
										<asp:ListItem Value="3">International</asp:ListItem>
									</asp:dropdownlist></td>
							</tr>
							<tr>
								<td>Training Name</td>
								<td><asp:dropdownlist id="cmbTraining" runat="server" Width="200px" AutoPostBack="True"></asp:dropdownlist></td>
								<td></td>
								<td>Training Group</td>
								<td><asp:dropdownlist id="cmbTrainGrp" runat="server" Width="200px"></asp:dropdownlist></td>
							</tr>
							<tr>
								<td>From</td>
								<td><cc1:dtp id="dtpFromDate" runat="server" Width="125px"></cc1:dtp></td>
								<td>To</td>
								<td colSpan="2"><cc1:dtp id="dtpToDate" runat="server" Width="125px"></cc1:dtp></td>
							</tr>
							<tr>
								<td>Location</td>
								<td colSpan="4"><asp:dropdownlist id="cmbLocation" runat="server" Width="200px"></asp:dropdownlist></td>
							</tr>
							<tr>
								<td>Trainer</td>
								<td colSpan="4"><asp:dropdownlist id="cmbTrainer" runat="server" Width="200px"></asp:dropdownlist></td>
							</tr>
							<tr>
								<td>Actaul Start</td>
								<td><cc1:dtp id="DtpStart" runat="server" Width="125px"></cc1:dtp></td>
								<td>End Date</td>
								<td colSpan="2"><cc1:dtp id="DtpEnd" runat="server" Width="125px"></cc1:dtp></td>
							</tr>
							<tr>
								<td>Seats</td>
								<td><asp:textbox id="TxtSeats" runat="server" Width="75px" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
								<td>Hours</td>
								<td colSpan="2"><asp:textbox id="TxtHours" runat="server" Width="75px" CssClass="TextBox" ForeColor="#003366"></asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
									Cost Per Employee&nbsp;&nbsp;&nbsp;&nbsp;<asp:textbox id="TxtCostEmp" runat="server" Width="75px" CssClass="TextBox"></asp:textbox></td>
							</tr>
							<tr>
								<td>Venue</td>
								<td colSpan="4"><asp:textbox id="TxtVenue" runat="server" Width="100%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
							</tr>
							<tr>
								<td>Venue Address</td>
								<td colSpan="4"><asp:textbox id="TxtVenueAdd" runat="server" Width="100%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
							</tr>
							<tr>
								<td>Source</td>
								<td colSpan="4"><asp:textbox id="TxtSource" runat="server" Width="100%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
							</tr>
							<tr>
								<td>Target Audience</td>
								<td colSpan="4"><asp:textbox id="TxtTargetAud" runat="server" Width="100%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
							</tr>
							<tr>
								<td>Venue Contact No.</td>
								<td><asp:textbox id="TxtVenueCont" runat="server" Width="100%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
								<td>E-mail Id</td>
								<td colSpan="2"><asp:textbox id="TxtEMailId" runat="server" Width="100%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
							</tr>
							<tr>
								<td>Training Details File</td>
								<td colspan="4"><input type="file" id="FileTrain" runat="server" style="WIDTH: 75%"></td>
							</tr>
						</table>
						<table id="TBLSkills" style="DISPLAY: none" cellSpacing="0" cellPadding="0" rules="none"
							width="800" bgColor="#f3f3f3" border="1" frame="border" runat="server">
							<tr>
								<td></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<td align="right" colSpan="5"><asp:button id="cmdSave" runat="server" Width="80px" Text="Save"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:button id="cmdClose" runat="server" Width="80px" Text="Close"></asp:button></td>
				</TR>
			</table>
		</form>
	</body>
</HTML>
