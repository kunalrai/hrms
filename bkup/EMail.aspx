<%@ Page Language="vb" AutoEventWireup="false" Codebehind="EMail.aspx.vb" Inherits="eHRMS.Net.EMail" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>EMail</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
		<script language="vbscript">
			sub LoadOpen()
				window.open "PageFooter.aspx","Footer" 
			End Sub
			
			Sub SendMail
				document.getElementById("cmdSend").click
			End sub
			
			Sub SendMail1()
				'msgbox "Good Morning"
				'Dim WithEvents oMailItem As Outlook.MailItem
				
				Dim ObjOut
				Dim oMailItem
				Dim theNameSpace 
				Dim Attach
				
				if document.getElementById("txtSubject").value = "" then
					msgbox "Mail Subject can be left blank.",,"Divergent" 
					exit sub
				end if
				
				if document.getElementById("txtBody").value = "" then
					msgbox "Mail Body can be left blank.",,"Divergent" 
					exit sub
				end if
				
				Set ObjOut = CreateObject("Outlook.Application")
				'msgbox ObjOut.Application.Name
				Set oMailItem = ObjOut.CreateItem(0)
				'msgbox oMailItem.Application.Name
				
				'Set theNameSpace = ObjOut.GetNameSpace("MAPI")
				'msgbox theNameSpace.CurrentUser 
				'msgbox theNameSpace.GetDefaultFolder(olFolderInbox)
				'msgbox theNameSpace 
				
				oMailItem.Recipients.Add(document.getElementById("txtTo").value)
				if document.getElementById("txtCC").value <> "" then
					oMailItem.CC = document.getElementById("txtCC").value	
				end if
				
				oMailItem.Subject = document.getElementById("txtSubject").value
				
				if document.getElementById("TxtAttach").value <> "" then
					msgbox document.getElementById("TxtAttach").value
					oMailItem.Attachments.Add(document.getElementById("TxtAttach").value)					
				end if
				'msgbox oMailItem.Subject 
				oMailItem.Body = document.getElementById("txtBody").value
				'msgbox oMailItem.Body 
				'oMailItem.Attachments.Add(attach,1)
				oMailItem.Send				
				'theNameSpace.Logoff 
				document.getElementById("cmdSend").click
			End Sub
			 
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx --><br>
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="500" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Email....
					</TD>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" rules="none" width="500" align="center" border="1"
				frame="border">
				<tr>
					<td width="25%"></td>
					<td width="75%"></td>
				</tr>
				<tr>
					<td colSpan="2"><asp:label id="lblMsg" runat="server" Font-Size="11px" Width="100%" Visible="False" ForeColor="Red"></asp:label></td>
				</tr>
				<tr style="DISPLAY: none">
					<td colspan="2"><asp:textbox id="TxtFrom" runat="server" Width="100%" ForeColor="#003366"></asp:textbox></td>
				</tr>
				<tr>
					<td><asp:label id="Label1" runat="server" Width="100%">To</asp:label></td>
					<td><asp:textbox id="txtTo" runat="server" ForeColor="#003366" ReadOnly="True" CssClass="TextBox"
							Width="100%">employees@orgltd.com</asp:textbox></td>
				</tr>
				<tr style="DISPLAY: none">
					<td><asp:label id="Label3" runat="server" Width="112px">CC</asp:label></td>
					<td><asp:textbox id="txtCC" runat="server" Width="100%" ForeColor="#003366"></asp:textbox></td>
				</tr>
				<tr>
					<td><asp:label id="Label4" runat="server" Width="112px">Subject</asp:label></td>
					<td><asp:textbox id="txtSubject" runat="server" CssClass="TextBox" Width="100%" ForeColor="#003366"></asp:textbox></td>
				</tr>
				<tr>
					<td><asp:label id="Label2" runat="server" Width="112px">Attachment</asp:label></td>
					<td><input id="TxtAttach" style="WIDTH: 100%" class="Textbox" type="file" size="40" runat="server"></td>
				</tr>
				<tr>
					<td colSpan="2"><asp:textbox id="txtBody" runat="server" Width="100%" Height="160px" TextMode="MultiLine" ForeColor="#003366"></asp:textbox></td>
				</tr>
				<tr>
					<td align="right" colSpan="2"><INPUT id="mdSend" style="WIDTH: 78px; HEIGHT: 24px" type="button" onclick="SendMail()"
							value="Send">&nbsp;&nbsp;&nbsp;
						<asp:button id="cmdClose" runat="server" Width="80px" Text="Close"></asp:button></td>
				</tr>
				<!--onclick="SendMail()" -->
			</table>
			<asp:textbox id="txtServer" style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 464px" runat="server"
				Width="192px" Visible="False"></asp:textbox><asp:button id="cmdSend" style="Z-INDEX: 103; LEFT: 952px; POSITION: absolute; TOP: 32px" runat="server"
				Width="0px" Text="Send" Height="0px"></asp:button></form>
	</body>
</HTML>
