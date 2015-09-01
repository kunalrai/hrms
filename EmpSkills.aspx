<%@ Page Language="vb" AutoEventWireup="false" Codebehind="EmpSkills.aspx.vb" Inherits="eHRMS.Net.EmpSkills"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>EmpSkills</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
		<SCRIPT language="javascript" src="Common.js"></SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="5" rightMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx --><br>
			<br>
			<table height="30" cellSpacing="0" cellPadding="0" width="790" border="0">
				<tr vAlign="bottom">
					<td width="110">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/leftoverlap.gif" border="0"></td>
								<td style="COLOR: #003366" align="center" bgColor="#cecbce"><A href="EmpMast.aspx?SrNo=62">Official 
										Info</A></td>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
							</tr>
						</table>
					</td>
					<td width="120">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/leftoverlap.gif" border="0"></td>
								<td style="COLOR: #003366" align="center" bgColor="#cecbce"><A href="GeneralInfo.aspx?SrNo=63">General 
										Info</A></td>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
							</tr>
						</table>
					</td>
					<td width="100">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/leftoverlap.gif" border="0"></td>
								<td style="COLOR: #003366" align="center" bgColor="#cecbce"><A href="Compensation.aspx?SrNo=64">Compensation</A></td>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
							</tr>
						</table>
					</td>
					<td width="100">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/leftoverlap.gif" border="0"></td>
								<td style="COLOR: #003366" align="center" bgColor="#cecbce"><A href="History.aspx?SrNo=65">History</A></td>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
							</tr>
						</table>
					</td>
					<td width="100">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/leftoverlap.gif" border="0"></td>
								<td style="COLOR: #003366" align="center" bgColor="#cecbce"><A href="Progression.aspx?SrNo=66">Progression</A></td>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
							</tr>
						</table>
					</td>
					<td width="100">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/leftoverlap.gif" border="0"></td>
								<td style="COLOR: #003366" align="center" bgColor="#cecbce"><A href="Others.aspx?SrNo=67">Others</A></td>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
							</tr>
						</table>
					</td>
					<td width="100">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/leftoverlap.gif" border="0"></td>
								<td style="COLOR: #003366" align="center" bgColor="#cecbce"><A href="Family.aspx?SrNo=68">Family</A></td>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
							</tr>
						</table>
					</td>
					<td width="100">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td width="10" bgColor="#666666"><IMG alt="" src="Images/SplitLeft.gif" border="0"></td>
								<td style="COLOR: white" align="center" bgColor="#666666"><b>Skills</b></td>
								<td width="10" bgColor="#666666"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
							</tr>
						</table>
					</td>
					<td width="100">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/leftoverlap.gif" border="0"></td>
								<td style="COLOR: #003366" align="center" bgColor="#cecbce"><A href="EmpNominees.aspx?SrNo=70">Nominee</A></td>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="9">
						<table borderColor="black" cellSpacing="0" cellPadding="0" rules="none" width="100%" border="1"
							frame="border">
							<tr>
								<td colSpan="3"><asp:label id="LblErrMsg" runat="server" ForeColor="Red" Width="100%"></asp:label></td>
							</tr>
							<tr>
								<td width="20%"><asp:label id="lblCode" runat="server" Width="100%">Code</asp:label></td>
								<td width="30%"><INPUT id="BtnFirst" style="FONT-WEIGHT: bold; COLOR: #0033ff; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: #ffffff; BORDER-BOTTOM-STYLE: none"
										type="button" value="<<" name="Button1" runat="server">&nbsp;&nbsp; <INPUT id="BtnPre" style="FONT-WEIGHT: bold; COLOR: #0033ff; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: #ffffff; BORDER-BOTTOM-STYLE: none"
										type="button" value="<" name="Button1" runat="server">&nbsp;
									<asp:textbox id="txtEM_CD" runat="server" ForeColor="#003366" Width="136px" AutoPostBack="True"
										Font-Bold="True" BackColor="#E1E4EB" CssClass="TextBox"></asp:textbox>&nbsp;<INPUT id="BtnNext" style="FONT-WEIGHT: bold; COLOR: #0033ff; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: #ffffff; BORDER-BOTTOM-STYLE: none"
										type="button" value=">" name="Button1" runat="server">&nbsp;&nbsp; <INPUT id="BtnLast" style="FONT-WEIGHT: bold; COLOR: #0033ff; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: #ffffff; BORDER-BOTTOM-STYLE: none"
										type="button" value=">>" name="Button1" runat="server"></td>
								<td align="center" width="50%"><asp:label id="LblName" runat="server" ForeColor="#003366" Width="100%" Font-Bold="True" Font-Size="9"></asp:label></td>
							</tr>
							<tr>
								<td colSpan="3">
									<table id="TblSkills" cellSpacing="0" cellPadding="0" rules="none" width="100%" bgColor="#f3f3f3"
										border="1" frame="border" runat="server">
									</table>
								</td>
							</tr>
							<tr>
								<td align="right" colSpan="3"><asp:button id="cmdSave" accessKey="S" runat="server" Width="75px" Text="Save"></asp:button>&nbsp;
									<asp:button id="cmdClose" runat="server" Width="75px" Text="Close"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td align="center" colSpan="9"><asp:label id="LblRights" Width="100%" Font-Bold="True" Font-Size="10" Runat="server"></asp:label></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
