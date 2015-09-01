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
			
			Sub Val(argid)							
				IF document.getElementById(argid).Checked = False THEN
					document.getElementById(replace(argid,"Chk","dtp")).disabled = true
					document.getElementById(replace(argid,"Chk","dtp") & "cmbDD").disabled = true
					document.getElementById(replace(argid,"Chk","dtp") & "cmbMM").disabled = true
					document.getElementById(replace(argid,"Chk","dtp") & "cmbYY").disabled = true
				ELSE
					document.getElementById(replace(argid,"Chk","dtp")).disabled = false
					document.getElementById(replace(argid,"Chk","dtp") & "cmbDD").disabled = false
					document.getElementById(replace(argid,"Chk","dtp") & "cmbMM").disabled = false
					document.getElementById(replace(argid,"Chk","dtp") & "cmbYY").disabled = false
				End If
			END SUB
		</script>
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
						<table borderColor="black" cellSpacing="0" cellPadding="0" rules="none" width="100%" border="1"
							frame="border">
							<tr>
								<td colSpan="4"><asp:label id="LblMsg" runat="server" ForeColor="Red" Font-Size="11px" Width="100%"></asp:label></td>
							</tr>
							<tr>
								<td width="20%"><asp:label id="lblCode" runat="server" Width="100%">Code</asp:label></td>
								<td width="30%"><INPUT id="BtnFirst" style="FONT-WEIGHT: bold; COLOR: #0033ff; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: #ffffff; BORDER-BOTTOM-STYLE: none"
										type="button" value="<<" name="Button1" runat="server">&nbsp;&nbsp; <INPUT id="BtnPre" style="FONT-WEIGHT: bold; COLOR: #0033ff; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: #ffffff; BORDER-BOTTOM-STYLE: none"
										type="button" value="<" name="Button1" runat="server">
									<asp:textbox id="txtEM_CD" runat="server" ForeColor="#003366" Width="128px" AutoPostBack="True"
										Font-Bold="True" CssClass="TextBox" BackColor="#E1E4EB"></asp:textbox>&nbsp;<INPUT id="BtnNext" style="FONT-WEIGHT: bold; COLOR: #0033ff; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: #ffffff; BORDER-BOTTOM-STYLE: none"
										type="button" value=">" name="Button1" runat="server">&nbsp;&nbsp; <INPUT id="BtnLast" style="FONT-WEIGHT: bold; COLOR: #0033ff; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: #ffffff; BORDER-BOTTOM-STYLE: none"
										type="button" value=">>" name="Button1" runat="server"></td>
								<td align="center" width="50%" colSpan="2"><asp:label id="LblName" runat="server" ForeColor="#003366" Font-Size="9" Width="100%" Font-Bold="True"></asp:label></td>
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
								<td colSpan="4">&nbsp;</td>
							</tr>
							<tr>
								<td class="header3" background="Images\headstripe.jpg" colSpan="4"><b>HDFC Group 
										Details</b></td>
								<td>&nbsp;</td>
							</tr>
							<tr>
								<td style="HEIGHT: 32px">CertNo</td>
								<td style="HEIGHT: 32px"><asp:textbox id="txt_cerno" ForeColor="#003366" Width="125px" CssClass="textbox" Runat="server"></asp:textbox></td>
								<td style="HEIGHT: 32px">Date of Eligibility</td>
								<td style="HEIGHT: 32px"><INPUT id="ChkDOE" onclick="Val(this.id)" type="checkbox" name="ChkDOE" runat="server"><cc1:dtpcombo id="dtpDOE" runat="server" Width="150px" BorderStyle="None" ToolTip="Date of Eligibility"
										Enabled="False"></cc1:dtpcombo></td>
							</tr>
							<tr>
								<td>Sum Assured</td>
								<td><asp:textbox id="txt_sumassu" onblur="CheckNum(this.id)" style="TEXT-ALIGN: right" ForeColor="#003366"
										Width="125px" CssClass="textbox" Runat="server"></asp:textbox></td>
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
					<td align="center" colSpan="9"><asp:label id="LblRights" Font-Size="10" Width="100%" Font-Bold="True" Runat="server"></asp:label></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
