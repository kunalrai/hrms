<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CreateUser.aspx.vb" Inherits="eHRMS.Net.CreateUser" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>CreateUser</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
		<SCRIPT language="javascript" src="Common.js"></SCRIPT>
		<script language="javascript">
			
			function ShowMenu(MenuType)
				{
				Menu = new String(MenuType)
				if (document.getElementById('Tbl' + Menu).style.display == "none")
					{
					document.getElementById('Tbl' + Menu).style.display = "block";
					document.getElementById('Img' + Menu).src = "images/Minus.gif";
					}
				else
					{
					document.getElementById('Tbl' + Menu).style.display = "none";
					document.getElementById('Img' + Menu).src = "images/Plus.gif";
					}
				}	
			function ShowModules()
				{
					if (document.getElementById("ChkGroup").checked == true)
					 {
						 document.getElementById("ModulesHD").style.display = "block";
						 document.getElementById("Modules").style.display = "block";
						 document.getElementById("CmbGroupName").disabled = true;
					 }
					else
					 {
						document.getElementById("ModulesHD").style.display = "none";
						document.getElementById("Modules").style.display = "none";
						document.getElementById("CmbGroupName").disabled = false;
					 }
				}
			
		</script>
		<script language="vbscript">
			sub ShowHide(args)
					Dim num
					num = args
					document.getElementById("TBHeadGeneral").style.backgroundColor = "#cecbce"   
					document.getElementById("TBHeadAccess").style.backgroundColor = "#cecbce"   
					document.getElementById("TBHeadReports").style.backgroundColor = "#cecbce" 
					  
					document.getElementById("TBHeadAccess").style.color = "#003366"  
					document.getElementById("TBHeadReports").style.color = "#003366" 
					document.getElementById("TBHeadGeneral").style.color = "#003366" 
					
					document.getElementById("TBLAccess").style.display = "none"   
					document.getElementById("TBLGeneral").style.display = "none"   
					document.getElementById("TBLReports").style.display = "none"   
					
					document.getElementById(replace(args,"TBHead","TBL")).style.display = "block"   						
					document.getElementById(args).style.color = "White" 
					document.getElementById(args).style.backgroundColor = "#666666"    
			End sub		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx --><br>
			<br>
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="600" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">User 
						Management
					</TD>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table borderColor="gray" cellSpacing="0" cellPadding="0" rules="none" width="600" align="center"
				border="1" frame="border">
				<tr>
					<td width="30%"></td>
					<td width="30%"></td>
					<td width="40%"></td>
				</tr>
				<tr>
					<td colSpan="3"><asp:label id="LblErrMsg" runat="server" ForeColor="Red"></asp:label></td>
				</tr>
				<tr>
					<td colSpan="3">&nbsp;</td>
				</tr>
				<tr>
					<td>
						<table id="TBHeadGeneral" cellSpacing="0" cellPadding="0" width="100%" bgColor="#666666"
							border="0" runat="server">
							<tr>
								<td width="10"><IMG alt="" src="Images/SplitLeft.gif" border="0"></td>
								<td style="CURSOR: hand" onclick="ShowHide('TBHeadGeneral');" align="center"><b>General</b></td>
								<td width="10"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
							</tr>
						</table>
					</td>
					<td>
						<table id="TBHeadAccess" cellSpacing="0" cellPadding="0" width="100%" bgColor="#cecbce"
							border="0" runat="server">
							<tr>
								<td width="10"><IMG alt="" src="Images/SplitLeft.gif" border="0"></td>
								<td style="CURSOR: hand" onclick="ShowHide('TBHeadAccess');" align="center"><b>Access</b></td>
								<td width="10"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
							</tr>
						</table>
					</td>
					<td>
						<table id="TBHeadReports" cellSpacing="0" cellPadding="0" width="80%" bgColor="#cecbce"
							border="0" runat="server">
							<tr>
								<td width="10"><IMG alt="" src="Images/SplitLeft.gif" border="0"></td>
								<td style="CURSOR: hand" onclick="ShowHide('TBHeadReports');" align="center"><b>Reports</b></td>
								<td width="10"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="3">
						<table id="TBLGeneral" cellSpacing="0" cellPadding="0" width="100%">
							<tr>
								<td width="30%"></td>
								<td width="50%"></td>
								<td width="20%"></td>
							</tr>
							<tr>
								<td style="HEIGHT: 14px">User ID</td>
								<td colSpan="2" style="HEIGHT: 14px"><asp:textbox id="TxtUserID" ForeColor="#003366" Width="100" Runat="server" CssClass="textbox"></asp:textbox><asp:dropdownlist id="cmbUserID" runat="server" Width="200px" Visible="False" AutoPostBack="True"></asp:dropdownlist>&nbsp;
									<asp:imagebutton id="btnList" Width="18px" Runat="server" ImageAlign="AbsMiddle" ImageUrl="Images\Find.gif"
										Height="19px"></asp:imagebutton><asp:imagebutton id="btnNew" Width="18px" Runat="server" ImageAlign="AbsMiddle" ImageUrl="Images\NewFile.ico"></asp:imagebutton></td>
							</tr>
							<tr>
								<td>User Name</td>
								<td colSpan="2"><asp:textbox id="TxtUserName" Width="90%" Runat="server" CssClass="textbox"></asp:textbox></td>
							</tr>
							<tr>
								<td>Group Name</td>
								<td colSpan="2"><asp:dropdownlist id="CmbGroupName" Width="90%" Runat="server"></asp:dropdownlist></td>
							</tr>
							<tr>
								<td>Password</td>
								<td colSpan="2"><asp:textbox id="TxtPass" ForeColor="#003366" Width="90%" Runat="server" CssClass="textbox" TextMode="Password"></asp:textbox></td>
							</tr>
							<tr>
								<td>Confirm Password</td>
								<td><asp:textbox id="TxtConPass" ForeColor="#003366" Width="223px" Runat="server" CssClass="textbox"
										TextMode="Password"></asp:textbox></td>
								<td align="center"><input id="ChkGroup" onclick="ShowModules()" type="checkbox" name="ChkGroup" runat="server">Group</td>
							</tr>
							<tr>
								<td colSpan="3">&nbsp;</td>
							</tr>
							<tr id="ModulesHD" runat="server">
								<td class="Header3" background="Images\headstripe.jpg" colSpan="3"><b>Module(s) Rights</b></td>
							</tr>
							<tr id="Modules" runat="server">
								<td colSpan="3">
									<table id="TblModules" cellSpacing="0" cellPadding="0" rules="none" width="100%" bgColor="#f3f3f3"
										border="1" frame="border" runat="server">
									</table>
								</td>
							</tr>
						</table>
						<table id="TBLAccess" style="DISPLAY: none" cellSpacing="0" cellPadding="0" rules="none"
							width="100%" border="1" frame="border" runat="server">
							<tr>
								<td></td>
							</tr>
						</table>
						<table id="TBLReports" style="DISPLAY: none" cellSpacing="0" cellPadding="0" rules="none"
							width="100%" border="1" frame="border" runat="server">
							<tr>
								<td></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td align="right" colSpan="3"><asp:button id="CmdSave" runat="server" width="75px" Text="Save"></asp:button>&nbsp;&nbsp;<asp:button id="CmdClose" runat="server" width="75px" Text="Close"></asp:button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
