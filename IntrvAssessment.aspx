<%@ Page Language="vb" AutoEventWireup="false" Codebehind="IntrvAssessment.aspx.vb" Inherits="eHRMS.Net.IntrvAssessment"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>IntrvAssessment</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
		<SCRIPT language="javascript" src="Common.js"></SCRIPT>
		<script language="javascript">
			function CheckNum(args)
				{
				 	if (isNaN(document.getElementById(args).value)==true)
				 		{
				 			alert("This field must be numeric type.")
				 			document.getElementById(args).value = "";
				 			return;
				 		}
				 	if ((document.getElementById(args).value > 10) | (document.getElementById(args).value < 0))
				 		{
				 			alert("Value must be between (0-10).")
				 			document.getElementById(args).value = "";
				 			return;
				 		}
				}
				
			function ChangeStatus()
			{
				if (document.getElementById("ChkChange").checked == true) 
				{
					document.getElementById("cmbResStatus").disabled=true; 	
				}
				else
				{
					document.getElementById("cmbResStatus").disabled=false; 	
				}
				
			}	
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="750" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Interview 
						Assessment....
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" rules="none" align="center" width="750" border="1" frame="border">
				<tr>
					<td width="15%"></td>
					<td width="35%"></td>
					<td width="20%"></td>
					<td width="30%"></td>
				</tr>
				<tr>
					<td colSpan="4"><asp:label id="LblErrMsg" runat="server" ForeColor="Red" Width="100%"></asp:label></td>
				</tr>
				<tr>
					<td>Interview Ref.No.</td>
					<td colSpan="3"><asp:dropdownlist id="cmbIntrvrefid" runat="server" Width="180px" AutoPostBack="True"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td>Job Requition No.</td>
					<td colSpan="3"><asp:dropdownlist id="cmbReqNo" runat="server" Width="180px" AutoPostBack="True"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td>Candidates</td>
					<td><asp:dropdownlist id="cmbResumes" runat="server" Width="180px" AutoPostBack="True"></asp:dropdownlist></td>
					<td>Resume Status</td>
					<td><asp:dropdownlist id="cmbResStatus" Width="180px" Runat="server" Enabled="False"></asp:dropdownlist><input id="ChkChange" onclick="ChangeStatus()" type="checkbox" CHECKED runat="server">
					</td>
				</tr>
				<tr>
					<td colSpan="4">&nbsp;</td>
				</tr>
				<tr>
					<td colSpan="4">
						<div style="OVERFLOW: auto; WIDTH: 750px; HEIGHT: 250px"><asp:table id="TblIntrAss" runat="server" cellSpacing="0" cellPadding="0" width="810"></asp:table></div>
					</td>
				</tr>
				<tr>
					<td colSpan="4">&nbsp;</td>
				</tr>
				<tr>
					<td align="right" colSpan="4"><asp:button id="cmdSave" runat="server" Width="75px" Text="Save"></asp:button>&nbsp;
						<asp:button id="cmdClose" runat="server" Width="75px" Text="Close"></asp:button></td>
				</tr>
				<tr>
					<td colspan="4" class="Header3" style ="FONT-STYLE: italic; TEXT-ALIGN: right" >* Evaluate on five point scale 5-maximum 1-minimum</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
