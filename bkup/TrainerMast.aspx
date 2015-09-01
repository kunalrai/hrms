<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TrainerMast.aspx.vb" Inherits="eHRMS.Net.TrainerMast"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>TrainerMast</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
		<SCRIPT language="javascript" src="Common.js"></SCRIPT>
		<script>
			function InChanged()
			{
					if (document.getElementById("RdInternal").checked == true)
						{
						document.getElementById("TxtDescription").style.display = "none"; 
						document.getElementById("cmbEmp").style.display = "block"; 
						}						
					else
						{
						document.getElementById("TxtDescription").style.display = "block"; 	
						document.getElementById("cmbEmp").style.display = "none";
						}
			}
		</script>
	</HEAD>
	<body onload="InChanged()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx -->
			<br>
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="600" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Trainer 
						Master....
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" rules="none" width="600" border="1" frame="border">
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
					<td>Code</td>
					<td><asp:textbox id="TxtCode" runat="server" Width="75px" CssClass="TextBox" AutoPostBack="True"
							ForeColor="#003366"></asp:textbox><asp:dropdownlist id="cmbCode" runat="server" Width="200" AutoPostBack="True" Visible="False"></asp:dropdownlist><asp:imagebutton id="btnList" Width="18px" Height="19px" ImageUrl="Images\Find.gif" ImageAlign="AbsMiddle"
							Runat="server"></asp:imagebutton><asp:imagebutton id="btnNew" Width="18px" Height="19px" ImageUrl="Images\NewFile.ico" ImageAlign="AbsMiddle"
							Runat="server"></asp:imagebutton></td>
					<td colSpan="2"><input id="RdInternal" onclick="InChanged();" type="radio" value="RdInternal" name="RadioGroup"
							runat="server"><label>Internal</label> <input id="RdExternal" onclick="InChanged();" type="radio" CHECKED value="RdExternal" name="RadioGroup"
							runat="server"><label>External</label>
					</td>
				</tr>
				<tr>
					<td>Trainer Name</td>
					<td colSpan="3"><asp:textbox id="TxtDescription" runat="server" Width="90%" CssClass="TextBox"></asp:textbox><asp:dropdownlist id="cmbEmp" runat="server" Width="90%" AutoPostBack="True"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td>Education</td>
					<td colSpan="3"><asp:textbox id="TxtEducation" runat="server" Width="90%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
				</tr>
				<tr>
					<td>Designation</td>
					<td colSpan="3"><asp:textbox id="TxtDesg" runat="server" Width="90%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
				</tr>
				<tr>
					<td>Department</td>
					<td colSpan="3"><asp:textbox id="TxtDept" runat="server" Width="90%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
				</tr>
				<tr>
					<td>Location</td>
					<td colSpan="3"><asp:textbox id="TxtLoc" runat="server" Width="90%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
				</tr>
				<tr>
					<td>Organisation</td>
					<td colSpan="3"><asp:textbox id="TxtOrganisation" runat="server" Width="90%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
				</tr>
				<tr>
					<td>Phone No.</td>
					<td colSpan="3"><asp:textbox id="TxtPhone" runat="server" Width="90%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
				</tr>
				<tr>
					<td class="Header3" background="Images\headstripe.jpg" colSpan="4"><b>Skills</b></td>
				</tr>
				<tr>
					<td colSpan="4">
						<table id="TblSkills" cellSpacing="0" cellPadding="0" rules="none" width="100%" bgColor="#f3f3f3"
							border="1" frame="border" runat="server">
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="4"><asp:CheckBox ID="chkDelete" Text="Delete Trainer" Checked="False" Runat="server" Width="100%"></asp:CheckBox></td>
				</tr>
				<tr>
					<td align="right" colSpan="4"><asp:button id="cmdSave" runat="server" Width="80px" Text="Save"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:button id="cmdClose" runat="server" Width="80px" Text="Close"></asp:button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
