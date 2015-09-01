<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="LeavEncash.aspx.vb" Inherits="eHRMS.Net.LeavEncash"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>LeavEncash</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
		<script language="javascript">
			function InChanged()
				{
					//alert(document.getElementById("RdoEncash").checked);
					if (document.getElementById("RdoEncash").checked == true)
						{
						 document.getElementById("cmbTransferTo").style.display = "none";
						 document.getElementById("LblTransfer").style.display = "none"; 	
						}
					else
						{
						 document.getElementById("cmbTransferTo").style.display = "block";
						 document.getElementById("LblTransfer").style.display = "block"; 	 	
		 				}
				}
		</script>
		<Script language="vbscript">
			function ValidateCtrl()       				
				
				If document.getElementById("TxtCode").value = "" Then
					Msgbox (" Employee Code Can not be left blank.")
					ValidateCtrl = false
					exit function
				End If
	            
				If document.getElementById("TxtDays").Value = "" Then
					msgbox (" Days can not be left blank.")
					ValidateCtrl = false
					exit function
				End If
            ValidateCtrl = true
           end function
          </Script>
	</HEAD>
	<body onload="InChanged()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx -->
			<br>
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="500" border="0" align="center">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Leave 
						Transfer/Encashment ....
					</TD>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="2" cellPadding="1" rules="none" width="500" border="1" align="center">
				<tr>
					<td width="10%"></td>
					<td width="25%"></td>
					<td width="22%"></td>
					<td width="23%"></td>
					<td width="20%"></td>
				</tr>
				<tr>
					<td colSpan="5"><asp:label id="lblMsg" runat="server" Font-Size="11px" Width="100%" Visible="False" ForeColor="Red"></asp:label></td>
				</tr>
				<tr>
					<td>&nbsp;Code</td>
					<td colSpan="2"><asp:textbox id="TxtCode" Width="80" Runat="server" AutoPostBack="True" CssClass="textbox" ForeColor="#003366"></asp:textbox><asp:dropdownlist id="cmbEmp" runat="server" Width="150" Visible="False" AutoPostBack="True"></asp:dropdownlist><asp:imagebutton id="btnList" Width="18px" Runat="server" ImageAlign="AbsMiddle" ImageUrl="Images\Find.gif"
							Height="19px"></asp:imagebutton></td>
					<td colSpan="2"><asp:label ForeColor="#003366" Font-Bold="True" Font-Size="9" id="LblName" Visible="False"
							Runat="server"></asp:label></td>
				</tr>
				<tr>
					<td>&nbsp;Type</td>
					<td><asp:radiobutton id="RdoEncash" onclick="InChanged()" runat="server" Checked="True" GroupName="a"
							Text="Encashment"></asp:radiobutton></td>
					<td colSpan="3"><asp:radiobutton id="RdoTransfer" onclick="InChanged()" runat="server" GroupName="a" Text="Transfer"></asp:radiobutton></td>
				</tr>
				<tr>
					<td>&nbsp;Leave</td>
					<td><asp:dropdownlist id="cmbLeaveType" runat="server" Width="150px"></asp:dropdownlist></td>
					<td><asp:label id="LblTransfer" Width="100%" Runat="server">Transfer To</asp:label></td>
					<td><asp:dropdownlist id="cmbTransferTo" runat="server" Width="150px"></asp:dropdownlist></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>&nbsp;Day(s)</td>
					<td colSpan="4"><input class="textbox" id="TxtDays" style="WIDTH: 80px; COLOR: #003366" type="text" runat="server"></td>
				</tr>
				<tr>
					<td>&nbsp;Remarks</td>
					<td colSpan="4"><asp:textbox id="TxtRemarks" runat="server" Width="100%" CssClass="Textbox" ForeColor="#003366"></asp:textbox></td>
				</tr>
			
				<TR height="15">
					<TD style="MARGIN-LEFT: 15px; MARGIN-RIGHT: 15px; BORDER-BOTTOM: #993300 thin groove; HEIGHT: 7px"
						colSpan="5"></TD>
				</TR>
				<tr>
					<td align="right" colSpan="5"><asp:button id="cmdSave" runat="server" Width="80px" Text="Save"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:button id="cmdClose" runat="server" Width="80px" Text="Close"></asp:button>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
