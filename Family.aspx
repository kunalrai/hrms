<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Family.aspx.vb" Inherits="eHRMS.Net.Family"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Employee Master</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
		<script language="VBScript">
			Sub CheckDate(argID)
				Dim TVal
				TVal = document.getElementById(argID).value
				if TVal="" then Exit Sub
				if isdate(TVal) then 
					If Len(TVal) = 11 Then
						If Not ((Mid(TVal, 3, 1) = "/" Or Mid(TVal, 3, 1) = "-") And (Mid(TVal, 7, 1) = "/" Or Mid(TVal, 7, 1) = "-")) Then
							MsgBox "Invalid Format! Please Enter in [dd/MMM/yyyy] Format.", , "Date Format"
							document.getElementById(argID).value = ""
						else
							DiffYears(Replace(argID,"DOB","Age"))
						End If
					ElseIf Len(TVal) = 10 Then
						document.getElementById(argID).value = Left(TVal,2) & "/" & MonthName(Mid(TVal,4,2),true) & "/" & right(TVal,4)		
						DiffYears(Replace(argID,"DOB","Age"))
					Else
						MsgBox "Invalid Format! Please Enter in [dd/MMM/yyyy] Format.", , "Date Format"
						document.getElementById(argID).value = ""
					End If
				Else
					MsgBox "Invalid Date!", vbokOnly, "Date Format"
					document.getElementById(argID).value = ""
				End if
			End Sub
			Sub DiffYears(argID)
				if document.getElementById(Replace(argID,"Age","DOB")).value = "" then Exit Sub
				dim Yr
				Yr = Round(DateDiff("D",cdate(document.getElementById(Replace(argID,"Age","DOB")).value),Now())/365,2)
				document.getElementById(argID).value = Yr
			End Sub
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
								<td width="10" bgColor="#666666"><IMG alt="" src="Images/SplitLeft.gif" border="0"></td>
								<td style="COLOR: white" align="center" bgColor="#666666"><b>Family</b></td>
								<td width="10" bgColor="#666666"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
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
							frame="box">
							<tr>
								<td width="100%"><asp:label id="LblMsg" runat="server" Font-Size="11px" ForeColor="Red" Width="100%"></asp:label></td>
							</tr>
							<tr>
								<td width="100%">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td width="20%"><asp:label id="lblCode" runat="server" Width="100%">Code</asp:label></td>
											<td width="30%"><INPUT id="BtnFirst" style="FONT-WEIGHT: bold; COLOR: #0033ff; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: #ffffff; BORDER-BOTTOM-STYLE: none"
													type="button" value="<<" name="Button1" runat="server">&nbsp;&nbsp;<INPUT id="BtnPre" style="FONT-WEIGHT: bold; COLOR: #0033ff; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: #ffffff; BORDER-BOTTOM-STYLE: none"
													type="button" value="<" name="Button1" runat="server">
												<asp:textbox id="txtEM_CD" runat="server" ForeColor="#003366" Width="136px" BackColor="#E1E4EB"
													CssClass="TextBox" Font-Bold="True" AutoPostBack="True"></asp:textbox>&nbsp;<INPUT id="BtnNext" style="FONT-WEIGHT: bold; COLOR: #0033ff; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: #ffffff; BORDER-BOTTOM-STYLE: none"
													type="button" value=">" name="Button1" runat="server">&nbsp;&nbsp; <INPUT id="BtnLast" style="FONT-WEIGHT: bold; COLOR: #0033ff; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: #ffffff; BORDER-BOTTOM-STYLE: none"
													type="button" value=">>" name="Button1" runat="server"></td>
											<td align="center" width="50%"><asp:label id="LblName" runat="server" Font-Size="9" ForeColor="#003366" Width="100%" Font-Bold="True"></asp:label></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td vAlign="top" width="100%"><asp:datagrid id="GrdFamily" runat="server" Width="100%" AllowSorting="True" AllowPaging="True"
										AutoGenerateColumns="False">
										<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
										<HeaderStyle HorizontalAlign="Center" CssClass="Header3"></HeaderStyle>
										<Columns>
											<asp:TemplateColumn HeaderText="Relative Name">
												<ItemStyle HorizontalAlign="Center" Width="30%"></ItemStyle>
												<ItemTemplate>
													<asp:TextBox id=RelName runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Relative_Name") %>'>
													</asp:TextBox>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Relation">
												<ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
												<ItemTemplate>
													<asp:TextBox id=Relation runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Relation") %>'>
													</asp:TextBox>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Relative DOB">
												<ItemStyle HorizontalAlign="Right" Width="15%"></ItemStyle>
												<ItemTemplate>
													<asp:TextBox id=DOB onblur=CheckDate(this.id) runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Relative_DOB") %>'>
													</asp:TextBox>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Relative Age">
												<ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
												<ItemTemplate>
													<asp:TextBox id=Age onfocus=DiffYears(this.id) runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "RelativeAge") %>'>
													</asp:TextBox>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Dependent">
												<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
												<ItemTemplate>
													<asp:CheckBox id=chkDependent ForeColor="#003366" Width="20" CssClass="TextBox" Runat="server" Checked='<%# iif(isDbNull(DataBinder.Eval(Container.DataItem, "Dependent")),0,DataBinder.Eval(Container.DataItem, "Dependent"))  %>' Height="20">
													</asp:CheckBox>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Nominee">
												<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
												<ItemTemplate>
													<asp:CheckBox id=chkNominee ForeColor="#003366" Width="20" CssClass="TextBox" Runat="server" Checked='<%# iif(isDbNull(DataBinder.Eval(Container.DataItem, "Nominee")),0,DataBinder.Eval(Container.DataItem, "Nominee"))  %>' Height="20">
													</asp:CheckBox>
												</ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
										<PagerStyle NextPageText="&amp;minus;&amp;gt;" Font-Underline="True" Font-Names="Verdana" PrevPageText="&amp;lt;&amp;minus;"
											ForeColor="#CB3908" BackColor="#E0E0E0" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></td>
							</tr>
							<tr>
								<td align="right"><asp:button id="cmdFamilyAdd" runat="server" Width="75px" Text="Add"></asp:button>&nbsp;
									<asp:button id="cmdSave" accessKey="S" runat="server" Width="75px" Text="Save"></asp:button>&nbsp;
									<asp:button id="cmdClose" accessKey="C" runat="server" Width="75px" Text="Close"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td align="center" colSpan="9"><asp:label id="LblRights" Font-Size="10" Width="100%" Font-Bold="True" Runat="server"></asp:label></td>
				</tr>
			</table>
		</form>
		</TR></TBODY></TABLE></TR></TBODY></TABLE>
	</body>
</HTML>
