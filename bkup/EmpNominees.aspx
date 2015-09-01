<%@ Page Language="vb" AutoEventWireup="false" Codebehind="EmpNominees.aspx.vb" Inherits="eHRMS.Net.family1" %>
<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>family1</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
		<script language="javascript">
		</script>
		<script language="vbscript">
			Sub CheckNum(argID)
				Dim TVal
				TVal = document.getElementById(argID).value
				if trim(TVal) = "" then 
					document.getElementById(argID).value = 0
					Exit Sub
				End if
				if Not IsNumeric(TVal) then
						MsgBox "Invalid Value! Please Enter numeric Value.", , "Divergent"
						document.getElementById(argID).value = 0
				End if
			End Sub
			
			Sub CheckPerc(argID)
				Dim TVal
				TVal = document.getElementById(argID).value
				if trim(TVal) = "" then 
					document.getElementById(argID).value = 0
					Exit Sub
				End if
				if Not IsNumeric(TVal) then
					MsgBox "Invalid Value! Please Enter numeric Value.", , "Divergent"
					document.getElementById(argID).value = 0
					Exit Sub
				End if
				
				if (TVal < 0 Or TVal > 100) Then
					MsgBox "Invalid Value! Percentage must be between 0 - 100.", , "Divergent"
					document.getElementById(argID).value = 0
					Exit Sub
				End If
				
			End Sub
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="5" rightMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx -->
			<br>
			<br>
			<table height="30" cellSpacing="0"  align="center" cellPadding="0" width="790" border="0">
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
								<td style="WIDTH: 7px" width="7" bgColor="#cecbce"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
							</tr>
						</table>
					</td>
					<td width="100">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td style="WIDTH: 8px" width="8" bgColor="#cecbce"><IMG alt="" src="Images/leftoverlap.gif" border="0"></td>
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
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/SplitLeft.gif" border="0"></td>
								<td style="COLOR: #003366" align="center" bgColor="#cecbce"><A href="Family.aspx?SrNo=68">Family</A></td>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
							</tr>
						</table>
					</td>
					<td width="100">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/leftoverlap.gif" border="0"></td>
								<td style="COLOR: #003366" align="center" bgColor="#cecbce"><A href="EmpSkills.aspx?SrNo=69">Skills</A></td>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
							</tr>
						</table>
					</td>
					<td width="100">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td width="10" bgColor="#666666"><IMG alt="" src="Images/SplitLeft.gif" border="0"></td>
								<td style="COLOR: white" align="center" bgColor="#666666"><b>Nominees</b></td>
								<td width="10" bgColor="#666666"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<td colSpan="9">
						<table cellSpacing="0" cellPadding="0" width="100%" border="1" frame="border" rules="none"
							borderColor="black">
							<tr>
								<td colSpan="4"><asp:label id="LblMsg" runat="server" Width="100%" Font-Size="11px" ForeColor="Red"></asp:label></td>
							</tr>
							<tr>
								<td width="20%"><asp:label id="lblCode" runat="server" Width="100%">Code</asp:label></td>
								<td width="30%"><asp:textbox id="txtEM_CD" runat="server" Width="100%" ForeColor="#003366" ReadOnly="False" BackColor="#E1E4EB"
										CssClass="TextBox" Font-Bold="True" AutoPostBack="True"></asp:textbox></td>
								<td align="center" width="50%" colSpan="2"><asp:label id="LblName" runat="server" Width="100%" Font-Size="9" ForeColor="#003366" Font-Bold="True"></asp:label></td>
							</tr>
							<tr>
								<td colSpan="4">&nbsp;</td>
							</tr>
							<tr>
								<td vAlign="top" width="100%" colSpan="4"><asp:datagrid id="GrdFamily" runat="server" Width="100%" AutoGenerateColumns="False">
										<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
										<HeaderStyle HorizontalAlign="Center" CssClass="Header3"></HeaderStyle>
										<Columns>
											<asp:BoundColumn DataField="Relative_Name" HeaderText="Relative Name">
												<ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="Relation" HeaderText="Relation">
												<ItemStyle HorizontalAlign="Left" Width="16%"></ItemStyle>
											</asp:BoundColumn>
											<asp:TemplateColumn HeaderText="PF%">
												<ItemStyle HorizontalAlign="Center" Width="16%"></ItemStyle>
												<ItemTemplate>
													<asp:TextBox id=PF onblur=CheckPerc(this.id) style="TEXT-ALIGN: right" runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "PFPerc") %>'>
													</asp:TextBox>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="SAF%">
												<ItemStyle HorizontalAlign="Right" Width="16%"></ItemStyle>
												<ItemTemplate>
													<asp:TextBox id=TxtSAFPerc onblur=CheckPerc(this.id) style="TEXT-ALIGN: right" runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "SAFPerc") %>'>
													</asp:TextBox>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="GRATUITY%">
												<ItemStyle HorizontalAlign="Left" Width="16%"></ItemStyle>
												<ItemTemplate>
													<asp:TextBox id=TxtGratPerc onblur=CheckPerc(this.id) style="TEXT-ALIGN: right" runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "GratuityPerc") %>'>
													</asp:TextBox>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="HDFC%">
												<ItemStyle HorizontalAlign="Left" Width="16%"></ItemStyle>
												<ItemTemplate>
													<asp:TextBox id=TxtHDFCPerc onblur=CheckPerc(this.id) style="TEXT-ALIGN: right" runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "HDFCPerc") %>'>
													</asp:TextBox>
												</ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
										<PagerStyle NextPageText="&amp;minus;&amp;gt;" Font-Underline="True" Font-Names="Verdana" PrevPageText="&amp;lt;&amp;minus;"
											ForeColor="#CB3908" BackColor="#E0E0E0" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></td>
							</tr>
							<tr>
								<td colspan="4">&nbsp;</td>
							</tr>
							<tr>
								<td class="header3" background="Images\headstripe.jpg" colspan="4"><b>HDFC Group 
										Details</b></td>
								<td>&nbsp;</td>
							</tr>
							<tr>
								<td>CertNo</td>
								<td><asp:textbox id="txt_cerno" Width="125px" CssClass="textbox" Runat="server" ForeColor="#003366"></asp:textbox></td>
								<td>Date of Eligibility</td>
								<td><cc1:dtpcombo id="dtpDOE" runat="server" Width="150px" ToolTip="Date of Eligibility" BorderStyle="None"></cc1:dtpcombo></td>
							</tr>
							<tr>
								<td>Sum Assured</td>
								<td><asp:textbox id="txt_sumassu" onBlur="CheckNum(this.id)" style="TEXT-ALIGN: right" Width="125px"
										CssClass="textbox" Runat="server" ForeColor="#003366"></asp:textbox></td>
								<td colSpan="2"></td>
							</tr>
							<tr>
								<td align="right" colSpan="4"><asp:button id="cmdsave" Width="75" Runat="server" Text="Save"></asp:button>&nbsp;
									<asp:button id="cmdclose" Width="75" Runat="server" Text="Close"></asp:button></td>
							</tr>
						</table>
					</td>
				</TR>
				<tr>
					<td colspan="9" align="center"><asp:Label Font-Size="10" Font-Bold="True" ID="LblRights" Width="100%" Runat="server"></asp:Label></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
