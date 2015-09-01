<%@ Page Language="vb" AutoEventWireup="false" Codebehind="History.aspx.vb" Inherits="eHRMS.Net.History" aspCompat="True"%>
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
		<SCRIPT language="javascript" src="Common.js"></SCRIPT>
		<script language="javaScript">
			function ShowHide(argName)
				{
					Menu = new String(argName)
					if (document.getElementById('tr' + Menu).style.display == "none")
						{
						document.getElementById('tr' + Menu).style.display = "block";
						document.getElementById('img' + Menu).src = "Minus.gif";
						}
					else
						{
						document.getElementById('tr' + Menu).style.display = "none";
						document.getElementById('img' + Menu).src = "plus.gif";
						}
				}
		</script>
		<script language="VBScript">
			Sub CheckDate(argID)
				Dim TVal
				TVal = document.getElementById(argID).value
				if isdate(TVal) then 
					If Len(TVal) = 11 Then
						If Not ((Mid(TVal, 3, 1) = "/" Or Mid(TVal, 3, 1) = "-") And (Mid(TVal, 7, 1) = "/" Or Mid(TVal, 7, 1) = "-")) Then
							MsgBox "Invalid Format! Please Enter in [dd/MMM/yyyy] Format."
							'document.getElementById(argID).focus()
						End If
					ElseIf Len(TVal) = 10 Then
						document.getElementById(argID).value = Left(TVal,2) & "/" & MonthName(Mid(TVal,4,2),true) & "/" & right(TVal,4)	
					Else
						MsgBox "Invalid Format! Please Enter in [dd/MMM/yyyy] Format.", , "Divergent"
						'document.getElementById(argID).focus()
					End If
				Else
					MsgBox "Invalid Date!", vbokOnly, "Divergent"
					'document.getElementById(argID).focus()
				End if
        				
			End Sub
			
			Sub DiffYears(argID)
				dim Yr
				Yr = Round(DateDiff("D",cdate(document.getElementById(Replace(argID,"ExpYears","ExpF")).value),cdate(document.getElementById(Replace(argID,"ExpYears","ExpT")).value))/365,2)
				document.getElementById(argID).value = Yr
			End Sub
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="5" rightMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx -->
			<br>
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
								<td width="10" bgColor="#666666"><IMG alt="" src="Images/SplitLeft.gif" border="0"></td>
								<td style="COLOR: white" align="center" bgColor="#666666"><b>History</b></td>
								<td width="10" bgColor="#666666"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
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
								<td colSpan="2"><asp:label id="LblMsg" runat="server" Width="100%" ForeColor="Red" Font-Size="11px"></asp:label></td>
							</tr>
							<tr>
								<td width="50%">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td width="40%"><asp:label id="lblCode" runat="server" Width="100%">Code</asp:label></td>
											<td width="60%"><INPUT id="BtnFirst" style="FONT-WEIGHT: bold; COLOR: #0033ff; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: #ffffff; BORDER-BOTTOM-STYLE: none"
													type="button" value="<<" name="Button1" runat="server">&nbsp;&nbsp; <INPUT id="BtnPre" style="FONT-WEIGHT: bold; COLOR: #0033ff; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: #ffffff; BORDER-BOTTOM-STYLE: none"
													type="button" value="<" name="Button1" runat="server">&nbsp;
												<asp:textbox id="txtEM_CD" runat="server" ForeColor="#003366" Width="128px" AutoPostBack="True"
													Font-Bold="True" BackColor="#E1E4EB" CssClass="TextBox"></asp:textbox>&nbsp;<INPUT id="BtnNext" style="FONT-WEIGHT: bold; COLOR: #0033ff; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: #ffffff; BORDER-BOTTOM-STYLE: none"
													type="button" value=">" name="Button1" runat="server">&nbsp;&nbsp; <INPUT id="BtnLast" style="FONT-WEIGHT: bold; COLOR: #0033ff; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: #ffffff; BORDER-BOTTOM-STYLE: none"
													type="button" value=">>" name="Button1" runat="server"></td>
										</tr>
									</table>
								</td>
								<td width="50%" align="center"><asp:label id="LblName" runat="server" Width="100%" ForeColor="#003366" Font-Bold="True" Font-Size="9"></asp:label></td>
							</tr>
							<tr>
								<td class="Header3" align="left" background="Images\headstripe.jpg" colSpan="2"><IMG style="CURSOR: hand" id="imgQual" onclick="ShowHide('Qual')" src="Images\Minus.gif">&nbsp;<B>Qualifications</B></td>
							</tr>
							<tr>
								<td>&nbsp;</td>
							</tr>
							<tr id="trQual" runat="server">
								<td vAlign="top" colSpan="2">
									<table id="TabQual" cellSpacing="0" cellPadding="2" width="100%" border="0" runat="server">
										<tr>
											<td><asp:datagrid id="GrdQual" runat="server" Width="100%" AllowSorting="True" AllowPaging="True"
													AutoGenerateColumns="False">
													<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
													<HeaderStyle HorizontalAlign="Center" CssClass="Header3"></HeaderStyle>
													<Columns>
														<asp:TemplateColumn HeaderText="Qualification">
															<ItemStyle HorizontalAlign="Center" Width="24%"></ItemStyle>
															<ItemTemplate>
																<asp:DropDownList id="cmbQual" runat="server" Width="100%"></asp:DropDownList>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="University">
															<ItemStyle HorizontalAlign="Center" Width="18%"></ItemStyle>
															<ItemTemplate>
																<asp:DropDownList Width="100%" id="cmbUniv" runat="server"></asp:DropDownList>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="College">
															<ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox id=txtCollege runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "College") %>'>
																</asp:TextBox>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Place">
															<ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox id=txtPlace runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Place") %>'>
																</asp:TextBox>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Year">
															<ItemStyle HorizontalAlign="Left" Width="6%"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox id=txtYr runat="server" ForeColor="#003366" Width="100%" MaxLength="4" onblur=CheckNum(this.id) CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Passing_Year") %>'>
																</asp:TextBox>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Marks(%)">
															<ItemStyle HorizontalAlign="Right" Width="8%"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox id=txtMark runat="server" onblur=CheckNum(this.id) ForeColor="#003366" Width="100%" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Marks_Per") %>'>
																</asp:TextBox>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Div/Grade">
															<ItemStyle HorizontalAlign="Left" Width="7%"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox id=txtGrade runat="server" MaxLength="3" ForeColor="#003366" Width="100%" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Grade") %>'>
																</asp:TextBox>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Subjects">
															<ItemStyle HorizontalAlign="Left" Width="17%"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox id=txtSub runat="server" ForeColor="#003366" MaxLength="49" Width="100%" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Subjects") %>'>
																</asp:TextBox>
															</ItemTemplate>
														</asp:TemplateColumn>
													</Columns>
													<PagerStyle NextPageText="&amp;minus;&amp;gt;" Font-Underline="True" Font-Names="Verdana" PrevPageText="&amp;lt;&amp;minus;"
														ForeColor="#CB3908" BackColor="#E0E0E0" Mode="NumericPages"></PagerStyle>
												</asp:datagrid></td>
										</tr>
									</table>
									<table cellSpacing="0" cellPadding="2" width="100%" border="0">
										<tr>
											<td align="right" width="100%"><asp:button id="cmdAddQual" runat="server" Width="75px" Text="Add"></asp:button>&nbsp;
												<asp:button id="cmdQualSave" runat="server" Width="75px" Text="Save"></asp:button></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="9">
						<table borderColor="black" cellSpacing="0" cellPadding="0" rules="none" width="100%" border="1"
							frame="box">
							<tr>
								<td colSpan="2">&nbsp;</td>
							</tr>
							<tr>
								<td class="Header3" align="left" background="Images\headstripe.jpg" colSpan="2"><IMG id="imgExp" style="CURSOR: hand" onclick="ShowHide('Exp')" src="Images\Minus.gif">&nbsp;<B>Experience</B></td>
							</tr>
							<tr>
								<td>&nbsp;</td>
							</tr>
							<tr id="TrExp" runat="server">
								<td vAlign="top" colSpan="2">
									<table id="TabExp" cellSpacing="0" cellPadding="2" width="100%" border="0" runat="server">
										<tr>
											<td width="100%"><asp:datagrid id="GrdExp" runat="server" Width="100%" AllowSorting="True" AllowPaging="True" AutoGenerateColumns="False">
													<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
													<HeaderStyle HorizontalAlign="Center" CssClass="Header3"></HeaderStyle>
													<Columns>
														<asp:TemplateColumn HeaderText="From Date">
															<ItemStyle HorizontalAlign="Center" Width="12%"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox id=ExpF onblur=CheckDate(this.id) runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Exp_From") %>'>
																</asp:TextBox>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="To Date">
															<ItemStyle HorizontalAlign="Center" Width="12%"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox id=ExpT onblur=CheckDate(this.id) runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Exp_To") %>'>
																</asp:TextBox>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Years">
															<ItemStyle HorizontalAlign="Right" Width="8%"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox id=ExpYears onfocus=DiffYears(this.id) runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Exp_Years") %>'>
																</asp:TextBox>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Organization">
															<ItemStyle HorizontalAlign="Left" Width="14%"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox id=Textbox8 runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Org_Name") %>'>
																</asp:TextBox>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Designation">
															<ItemStyle HorizontalAlign="Left" Width="12%"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox id=Textbox9 runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "DSG_Name") %>'>
																</asp:TextBox>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Job Profile">
															<ItemStyle HorizontalAlign="Right" Width="14%"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox id=TxtAreaWork runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "JobProfile") %>'>
																</asp:TextBox>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Gr. Salary">
															<ItemStyle HorizontalAlign="Right" Width="12%"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox id=Textbox10 runat="server" onblur=CheckNum(this.id) MaxLength="9" ForeColor="#003366" Width="100%" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Drawn_Sal") %>'>
																</asp:TextBox>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Leaving Reason">
															<ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox id=Textbox11 runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "LeavingReason") %>'>
																</asp:TextBox>
															</ItemTemplate>
														</asp:TemplateColumn>
													</Columns>
													<PagerStyle NextPageText="&amp;minus;&amp;gt;" Font-Underline="True" Font-Names="Verdana" PrevPageText="&amp;lt;&amp;minus;"
														ForeColor="#CB3908" BackColor="#E0E0E0" Mode="NumericPages"></PagerStyle>
												</asp:datagrid></td>
										</tr>
									</table>
									<table cellSpacing="0" cellPadding="2" width="100%" border="0">
										<tr>
											<td align="right" width="100%"><asp:button id="cmdAddExp" runat="server" Width="75px" Text="Add"></asp:button>&nbsp;
												<asp:button id="cmdSaveExp" runat="server" Width="75px" Text="Save"></asp:button></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="9" align="center"><asp:Label Font-Size="10" Font-Bold="True" ID="LblRights" Width="100%" Runat="server"></asp:Label></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
