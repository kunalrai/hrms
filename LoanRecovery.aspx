<%@ Page Language="vb" AutoEventWireup="false" Codebehind="LoanRecovery.aspx.vb" Inherits="eHRMS.Net.LoanRecovery" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Loan Recovery</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript">
		function ConfirmDelete()
		{
			if(confirm("Are You Sure To Delete This Record?"+"...[HRMS]")==true)
				return true;
			else
				return false;
		}				</script>
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			web
			<br>
			<!--#include file="MenuBars.aspx"-->
			<TABLE cellSpacing="0" cellPadding="0" width="98%" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Loan 
						Recovery ....
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" rules="none" width="98%" align="center" border="1"
				frame="border">
				<tr>
					<td width="15%"></td>
					<td width="35"></td>
					<td width="15%"></td>
					<td width="35%"></td>
				</tr>
				<tr>
					<td colSpan="4"><asp:label id="LblErrMsg" runat="server" Width="272px" ForeColor="Red"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
				</tr>
				<TR>
					<TD align="center" colSpan="4">
						<DIV style="BORDER-RIGHT: 1px solid; BORDER-TOP: 1px solid; OVERFLOW: auto; BORDER-LEFT: 1px solid; WIDTH: 100%; COLOR: #cccccc; BORDER-BOTTOM: 0px solid; HEIGHT: 242px"><asp:datagrid id="GrdLoanStatus" Width="100%" AutoGenerateColumns="False" Runat="server">
								<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
								<HeaderStyle Font-Names="Arial" Font-Bold="True" Wrap="False" HorizontalAlign="Left" Height="7px"
									BorderWidth="10px" ForeColor="White" VerticalAlign="Top" BackColor="Gray"></HeaderStyle>
								<Columns>
									<asp:ButtonColumn Text="Select" DataTextField="Emp_Code" HeaderText="Code" CommandName="Select"></asp:ButtonColumn>
									<asp:BoundColumn DataField="Emp_Name" HeaderText="Name">
										<HeaderStyle Width="15%"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="L_Date" HeaderText="Sanction Date"></asp:BoundColumn>
									<asp:BoundColumn DataField="L_Amt" HeaderText="Principal Amt"></asp:BoundColumn>
									<asp:BoundColumn DataField="L_Inst_Amt" HeaderText="Installment Amt"></asp:BoundColumn>
									<asp:BoundColumn DataField="L_Inst_No" HeaderText="No of Installment"></asp:BoundColumn>
									<asp:BoundColumn HeaderText="Principal Balance"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="L_Rec" HeaderText="Rece"></asp:BoundColumn>
									<asp:BoundColumn DataField="L_SDate" HeaderText="Recovery Date"></asp:BoundColumn>
									<asp:BoundColumn DataField="I_Amt" HeaderText="Intrest Amt"></asp:BoundColumn>
									<asp:BoundColumn DataField="I_Inst_Amt" HeaderText="Installment Amt"></asp:BoundColumn>
									<asp:BoundColumn DataField="I_Inst_No" HeaderText="No of Installment"></asp:BoundColumn>
									<asp:BoundColumn HeaderText="Intrest Balance"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="I_Rec" HeaderText="Rece1"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="L_Code" HeaderText="LCode"></asp:BoundColumn>
									<asp:BoundColumn DataField="I_SDate" HeaderText="Recovery Date"></asp:BoundColumn>
								</Columns>
								<PagerStyle Font-Size="XX-Small" Font-Bold="True" HorizontalAlign="Center" ForeColor="#003366"
									Mode="NumericPages"></PagerStyle>
							</asp:datagrid></DIV>
					</TD>
				</TR>
			</table>
			<table cellSpacing="0" cellPadding="0" rules="none" width="98%" align="center" border="1"
				frame="box">
				<TR>
					<TD style="HEIGHT: 39px">
						<hr style="BORDER-BOTTOM: #993366 thin solid">
						<asp:dropdownlist id="CmbAcHead" runat="server" Width="176px" AutoPostBack="True"></asp:dropdownlist>&nbsp;
						<asp:checkbox id="ChkZero" runat="server" AutoPostBack="True" Font-Bold="True" Text="Show Zero Balance"></asp:checkbox></TD>
				</TR>
				<tr>
					<td colSpan="4" height="10"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
