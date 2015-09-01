<%@ Register TagPrefix="cr" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=9.1.5000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="WebForm11.aspx.vb" Inherits="eHRMS.Net.WebForm11" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta name="vs_snapToGrid" content="False">
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript">
		
		</script>
		<Script Language='VBScript'>
				Sub Check(arg)
					Dim TVal
					TVal = document.getElementById(arg).value
					If isdate(TVal) then 
						If Len(TVal) = 11 Then
							If Not ((Mid(TVal, 3, 1) = "/" Or Mid(TVal, 3, 1) = "-") And (Mid(TVal, 7, 1) = "/" Or Mid(TVal, 7, 1) = "-")) Then
								MsgBox "Invalid Format! Please Enter in [dd/MMM/yyyy] Format1."
							End If
						ElseIf Len(TVal) = 10 Then
							document.getElementById(arg).value = Left(TVal,2) & "/" & MonthName(Mid(TVal,4,2),true) & "/" & right(TVal,4)
						Else
							MsgBox "Invalid Format! Please Enter in [dd/MMM/yyyy] Format."	
						End If
					Else
						MsgBox "Invalid Date!", vbokOnly, "Divergent"
						document.getElementById(arg).value = Right("00" & Day(Now()),2) & "/" & MonthName(Month(Now()),true) & "/" & Year(Now())
					End if
				End Sub
		</Script>
	</HEAD>
	<body ms_positioning="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table cellpadding="0" cellspacing="0" width="700">
				<tr>
					<td width="20%">File Path</td>
					<td width="80%">
						<asp:TextBox id="TxtQuery" runat="server" Width="100%"> Select EMP_CODE, EMP_PICT FROM HRDMASTQRY WHERE isnull(EMP_PICT,'')&lt;&gt;''</asp:TextBox></td>
				</tr>
				<tr>
					<td colspan="2">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="2" align="right">
						<asp:Button id="Button1" runat="server" Text="Store Images"></asp:Button>&nbsp;&nbsp;&nbsp;</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
