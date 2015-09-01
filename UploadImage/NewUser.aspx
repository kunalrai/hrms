<%@ Page Language="vb" AutoEventWireup="false" Codebehind="NewUser.aspx.vb" Inherits="Billing.NewUser" aspCompat="True"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>NewUser</title>
		<%
if Session("CenterCode") = "1" And Session("Id") ="EDP" then
%>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Styles.css" type="text/css" rel="Stylesheet">
		<script language="vbscript">
		Function FrmSubmit()
			IF document.getElementById("txtPWD").value <> document.getElementById("txtCPWD").value then
				msgbox "Password Does Not Match With Confirm Password."
				document.getElementById("txtPWD").focus()
				Exit Function
			END IF	
			document.getElementById("Form1").submit()
		End Function
		</script>
	</HEAD>
	<body bgColor="whitesmoke" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" action="UserDB.aspx">
			<table style="Z-INDEX: 100; POSITION: relative; TOP: 10%" cellSpacing="0" cellPadding="0"
				width="50%" align="center" border="1">
				<tr>
					<td>
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr bgColor="#98a9ca">
								<td colSpan="2"><font size="Medium">Collection Center Detail</font></td>
							</tr>
							<tr>
								<td width="50%">Collection Center Code</td>
								<td align="center" width="50%"><INPUT id="txtCCode" type="text" name="txtCCode"></td>
							</tr>
							<tr>
								<td width="50%">Collection Center Name</td>
								<td align="center" width="50%"><INPUT id="txtCName" type="text" name="txtCName"></td>
							</tr>
							<tr>
								<td width="50%">Address</td>
								<td align="center" width="50%"><INPUT id="txtAdd" type="text" name="txtAdd"></td>
							</tr>
							<tr>
								<td width="50%">City</td>
								<td align="center" width="50%"><INPUT id="txtCity" type="text" name="txtCity"></td>
							</tr>
							<tr>
								<td width="50%">PIN</td>
								<td align="center" width="50%"><INPUT id="txtPin" type="text" name="txtPin"></td>
							</tr>
							<tr bgColor="#98a9ca">
								<td colSpan="2"><font size="Medium">User Detail</font></td>
							</tr>
							<tr>
								<td width="50%">User ID</td>
								<td align="center" width="50%"><INPUT id="txtUID" type="text" name="txtUID"></td>
							</tr>
							<tr>
								<td width="50%">User Name</td>
								<td align="center" width="50%"><INPUT id="txtUName" type="text" name="txtUName"></td>
							</tr>
							<tr>
								<td width="50%">Password</td>
								<td align="center" width="50%"><INPUT id="txtPWD" type="password" name="txtPWD"></td>
							</tr>
							<tr>
								<td width="50%">Confirm Password</td>
								<td align="center" width="50%"><INPUT id="txtCPWD" type="password" name="txtCPWD"></td>
							</tr>
							<tr>
								<td width="50%">Local Database Path</td>
								<td align="center" width="50%"><INPUT id="txtPath" type="text" name="txtPath"></td>
							</tr>
							<tr>
								<td width="50%">Receipt Prefix</td>
								<td align="center" width="50%"><INPUT id="txtPrefix" type="text" name="txtPrefix"></td>
							</tr>
							<tr>
								<td colSpan="2">&nbsp;</td>
							</tr>
							<tr>
								<td>
								&nbsp;
								<td align="right"><input id="CmdSave" style="WIDTH: 80px" type="button" value="Save" onclick="frmsubmit()">
									&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
		<%
End If
%>
	</body>
</HTML>
