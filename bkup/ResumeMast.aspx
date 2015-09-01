<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ResumeMast.aspx.vb" Inherits="eHRMS.Net.ResumeMast"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ResumeMast</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
		<SCRIPT language="javascript" src="Common.js"></SCRIPT>
		<script language="vbscript">
			Sub ShowSubTab(argID)
					document.getElementById("TblPermanent").style.display="none"
					document.getElementById("TblMailing").style.display="none"

					document.getElementById("TrPermanent").style.fontWeight = "normal"
					document.getElementById("TrMailing").style.fontWeight = "normal"
					
					document.getElementById(Replace(argID,"Tr","Tbl")).style.display="block"
					document.getElementById(argID).style.fontWeight = "bold"
			End Sub
			
			Sub ShowSubTab1(argID)
					document.getElementById("TblQual").style.display="none"
					document.getElementById("TblExp").style.display="none"

					document.getElementById("TrQual").style.fontWeight = "normal"
					document.getElementById("TrExp").style.fontWeight = "normal"
					
					document.getElementById(Replace(argID,"Tr","Tbl")).style.display="block"
					document.getElementById(argID).style.fontWeight = "bold"
			End Sub
			
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
			
			Sub MaritalStatus()
				if document.getElementById("cmbRefType").value = 0 Then
					document.getElementById("cmbRefreeName").style.display = "block" 
					document.getElementById("TxtRefreeName").style.display = "none" 
				Elseif document.getElementById("cmbRefType").value = 1 Then
					document.getElementById("cmbRefreeName").style.display = "none" 
					document.getElementById("TxtRefreeName").style.display = "block"
				End if 
			End Sub
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx --><br>
			<TABLE cellSpacing="0" cellPadding="0" align="center" width="700" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19">
						<IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Resume 
						Master ....
					</TD>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" rules="none" width="700" align="center" border="1"
				frame="border">
				<tr>
					<td width="14%"></td>
					<td width="20%"></td>
					<td width="13%"></td>
					<td width="23%"></td>
					<td width="10%"></td>
					<td width="20%"></td>
				</tr>
				<tr>
					<td colSpan="6"><asp:label id="lblMsg" runat="server" ForeColor="Red" Visible="False" Width="100%" Font-Size="11px"></asp:label></td>
				</tr>
				<tr>
					<td class="Header3" colSpan="6"><b>Requisition Details</b></td>
				</tr>
				<tr>
					<td>Requisition No*</td>
					<td><asp:dropdownlist id="cmbReqNo" accessKey="r" runat="server" Width="100%" Height="20px" AutoPostBack="True"></asp:dropdownlist></td>
					<td>Ref No.*</td>
					<td colSpan="3"><asp:textbox id="TxtRefNo" runat="server" Width="100%" CssClass="TextBox" ReadOnly="True"></asp:textbox><asp:dropdownlist id="cmbRefNo" runat="server" Visible="False" Width="100%" AutoPostBack="True"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td>Department</td>
					<td><asp:dropdownlist id="cmbDepartment" runat="server" Width="100%"></asp:dropdownlist></td>
					<td>Designation</td>
					<td><asp:dropdownlist id="cmbDesignation" runat="server" Width="100%"></asp:dropdownlist></td>
					<td>Status</td>
					<td><asp:dropdownlist id="cmbStatus" runat="server" Width="100%" Height="20px"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td colSpan="6">&nbsp;&nbsp;&nbsp;&nbsp;<asp:label id="LblRes" Runat="server">Total Resumes:</asp:label>&nbsp;
						<asp:label id="LblvTotRes" Width="50" Runat="server"></asp:label>&nbsp;
						<asp:label id="LblOnhold" Runat="server">In Process:</asp:label>&nbsp;
						<asp:label id="LblvInProcess" Runat="server" width="50px"></asp:label>&nbsp;
						<asp:label id="LblRejected" Runat="server">Rejected:</asp:label>&nbsp;
						<asp:label id="LblvRejected" Width="50px" Runat="server"></asp:label>&nbsp;
						<asp:label id="LblSuitable" Runat="server">Suitable:</asp:label>&nbsp;
						<asp:label id="LblvSuitable" Width="50px" Runat="server"></asp:label>&nbsp;
						<asp:label id="LblHold" Width="50px" Runat="server">On Hold:</asp:label>&nbsp;
						<asp:label id="LblVHold" Runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="Header3" colSpan="6"><b>Personal Details</b></td>
				</tr>
				<tr>
					<td>First Name*</td>
					<td><asp:textbox id="TxtFirstN" runat="server" Width="100%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
					<td>Middle</td>
					<td><asp:textbox id="TxtMiddleN" runat="server" Width="100%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
					<td>Last</td>
					<td><asp:textbox id="TxtLastN" runat="server" Width="100%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
				</tr>
				<tr>
					<td>Father</td>
					<td><asp:textbox id="TxtFather" runat="server" Width="100%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
					<td>Marital Status</td>
					<td><asp:dropdownlist id="cmbMarital" runat="server" Width="100%">
							<asp:ListItem Value="1">Married</asp:ListItem>
							<asp:ListItem Value="2">Single</asp:ListItem>
							<asp:ListItem Value="3">Widow</asp:ListItem>
							<asp:ListItem Value="4">Widover</asp:ListItem>
							<asp:ListItem Value="5">Divorcee</asp:ListItem>
							<asp:ListItem Value="0" Selected="True">&nbsp;</asp:ListItem>
						</asp:dropdownlist></td>
					<td>Spouse</td>
					<td><asp:textbox id="TxtSpouse" runat="server" Width="100%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
				</tr>
				<tr>
					<td>E-Mail ID*</td>
					<td><asp:textbox id="TxtEmail" runat="server" Width="100%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
					<td>Phone No.</td>
					<td><asp:textbox id="TxtPhone" runat="server" Width="100%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
					<td>Mobile</td>
					<td><asp:textbox id="TxtMobile" runat="server" Width="100%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
				</tr>
				<tr>
					<td>DOB</td>
					<td><cc1:dtpcombo id="dtpDOB" runat="server" Width="150px" ToolTip="Date Of Birth" DateValue="2005-01-01"></cc1:dtpcombo></td>
					<td>Gender</td>
					<td><asp:radiobutton id="RdoMale" runat="server" Checked="True" GroupName="a" Text="Male"></asp:radiobutton></td>
					<td colSpan="2"><asp:radiobutton id="RdoFemale" runat="server" GroupName="a" Text="Female"></asp:radiobutton></td>
				</tr>
				<tr>
					<td colSpan="3">
						<table id="TrPermanent" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/leftoverlap.gif" border="0"></td>
								<td style="CURSOR: hand; COLOR: #003366" onclick="ShowSubTab('TrPermanent')" align="center"
									bgColor="#cecbce">PermanentAddress</td>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
							</tr>
						</table>
					</td>
					<td colSpan="3">
						<table id="TrMailing" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/SplitLeft.gif" border="0"></td>
								<td style="CURSOR: hand; COLOR: #003366" onclick="ShowSubTab('TrMailing')" align="center"
									bgColor="#cecbce">Mailing Address</td>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="6">
						<table id="TblPermanent" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td width="10%"></td>
								<td width="23%"></td>
								<td width="10%"></td>
								<td width="23%"></td>
								<td width="11%"></td>
								<td width="23%"></td>
							</tr>
							<tr>
								<td>Address</td>
								<td><asp:textbox id="TxtPAdd1" runat="server" Width="100%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
								<td>City</td>
								<td><asp:textbox id="TxtPCity" runat="server" Width="100%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
								<td>Country</td>
								<td><asp:textbox id="TxtPCountry" runat="server" Width="100%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
							</tr>
							<tr>
								<td></td>
								<td><asp:textbox id="TxtPAdd2" runat="server" Width="100%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
								<td>State</td>
								<td><asp:textbox id="TxtPState" runat="server" Width="100%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
								<td>Phone No.</td>
								<td><asp:textbox id="TxtPPhone" runat="server" Width="100%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
							</tr>
							<tr>
								<td></td>
								<td><asp:textbox id="TxtPAdd3" runat="server" Width="100%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
								<td>Zip/Pin</td>
								<td><asp:textbox id="TxtPPin" runat="server" Width="100%" CssClass="TextBox" MaxLength="6" ForeColor="#003366"></asp:textbox></td>
								<td></td>
								<td></td>
							</tr>
						</table>
						<table id="TblMailing" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td width="10%"></td>
								<td width="23%"></td>
								<td width="10%"></td>
								<td width="23%"></td>
								<td width="11%"></td>
								<td width="23%"></td>
							</tr>
							<tr>
								<td>Address</td>
								<td><asp:textbox id="TxtMAdd1" runat="server" Width="100%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
								<td>City</td>
								<td><asp:textbox id="TxtMCity" runat="server" Width="100%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
								<td>Country</td>
								<td><asp:textbox id="TxtMCountry" runat="server" Width="100%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
							</tr>
							<tr>
								<td></td>
								<td><asp:textbox id="TxtMAdd2" runat="server" Width="100%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
								<td>State</td>
								<td><asp:textbox id="TxtMState" runat="server" Width="100%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
								<td>Phone No.</td>
								<td><asp:textbox id="TxtMPhone" runat="server" Width="100%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
							</tr>
							<tr>
								<td></td>
								<td><asp:textbox id="TxtMAdd3" runat="server" Width="100%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
								<td>Zip/Pin</td>
								<td><asp:textbox id="TxtMPin" runat="server" Width="100%" CssClass="TextBox" MaxLength="6" ForeColor="#003366"></asp:textbox></td>
								<td>Delhi\NCR</td>
								<td><asp:dropdownlist id="cmbNCR" runat="server" Width="100%">
										<asp:ListItem Selected="True" Value="0">No</asp:ListItem>
										<asp:ListItem Value="1">Yes</asp:ListItem>
									</asp:dropdownlist></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="3">
						<table id="TrQual" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/leftoverlap.gif" border="0"></td>
								<td style="CURSOR: hand; COLOR: #003366" onclick="ShowSubTab1('TrQual')" align="center"
									bgColor="#cecbce">Qualification</td>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
							</tr>
						</table>
					</td>
					<td colSpan="3">
						<table id="TrExp" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/SplitLeft.gif" border="0"></td>
								<td style="CURSOR: hand; COLOR: #003366" onclick="ShowSubTab1('TrExp')" align="center"
									bgColor="#cecbce">Experience</td>
								<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="6">
						<table id="TblQual" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td><asp:datagrid id="GrdQual" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
										AllowSorting="True">
										<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
										<HeaderStyle HorizontalAlign="Center" CssClass="Header3"></HeaderStyle>
										<Columns>
											<asp:TemplateColumn HeaderText="Qualification">
												<ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
												<ItemTemplate>
													<asp:DropDownList id="cmbQual" runat="server" Width="100%"></asp:DropDownList>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="University">
												<ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
												<ItemTemplate>
													<asp:DropDownList id="cmbUniv" runat="server" Width="100%"></asp:DropDownList>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="College">
												<ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
												<ItemTemplate>
													<asp:TextBox id=txtCollege runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "College") %>'>
													</asp:TextBox>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Place">
												<ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
												<ItemTemplate>
													<asp:TextBox id=txtPlace runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Place") %>'>
													</asp:TextBox>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Year">
												<ItemStyle HorizontalAlign="Left" Width="6%"></ItemStyle>
												<ItemTemplate>
													<asp:TextBox id=txtYr runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Passing_Year") %>'>
													</asp:TextBox>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Marks">
												<ItemStyle HorizontalAlign="Right" Width="7%"></ItemStyle>
												<ItemTemplate>
													<asp:TextBox id=txtMark onblur=CheckNum(this.id) runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Marks_Per") %>'>
													</asp:TextBox>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Div/Grade">
												<ItemStyle HorizontalAlign="Left" Width="7%"></ItemStyle>
												<ItemTemplate>
													<asp:TextBox id=txtGrade runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Grade") %>'>
													</asp:TextBox>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Subjects">
												<ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
												<ItemTemplate>
													<asp:TextBox id=txtSub runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Subjects") %>'>
													</asp:TextBox>
												</ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
										<PagerStyle NextPageText="&amp;minus;&amp;gt;" Font-Underline="True" Font-Names="Verdana" PrevPageText="&amp;lt;&amp;minus;"
											ForeColor="#CB3908" BackColor="#E0E0E0" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></td>
							</tr>
							<tr>
								<td align="right" width="100%"><asp:button id="CmdAddQual" runat="server" Width="75px" Text="Add"></asp:button></td>
							</tr>
						</table>
						<table id="TblExp" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td><asp:datagrid id="GrdExp" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
										AllowSorting="True">
										<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
										<HeaderStyle HorizontalAlign="Center" CssClass="Header3"></HeaderStyle>
										<Columns>
											<asp:TemplateColumn HeaderText="From Date">
												<ItemStyle HorizontalAlign="Center" Width="12%"></ItemStyle>
												<ItemTemplate>
													<asp:TextBox id=ExpF onblur=CheckDate(this.id) runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Exp_From") %>'>
													</asp:TextBox>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="To Date">
												<ItemStyle HorizontalAlign="Center" Width="12%"></ItemStyle>
												<ItemTemplate>
													<asp:TextBox id=ExpT onblur=CheckDate(this.id) runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Exp_To") %>'>
													</asp:TextBox>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Years">
												<ItemStyle HorizontalAlign="Right" Width="8%"></ItemStyle>
												<ItemTemplate>
													<asp:TextBox id=ExpYears onfocus=DiffYears(this.id) runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Exp_Years") %>'>
													</asp:TextBox>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Organization">
												<ItemStyle HorizontalAlign="Left" Width="14%"></ItemStyle>
												<ItemTemplate>
													<asp:TextBox id=Textbox17 runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Org_Name") %>'>
													</asp:TextBox>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Designation">
												<ItemStyle HorizontalAlign="Left" Width="12%"></ItemStyle>
												<ItemTemplate>
													<asp:TextBox id=Textbox18 runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "DSG_Name") %>'>
													</asp:TextBox>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Area of work">
												<ItemStyle HorizontalAlign="Right" Width="14%"></ItemStyle>
												<ItemTemplate>
													<asp:TextBox id=TxtAreaWork runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "JobResponsiblities") %>'>
													</asp:TextBox>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Salary/Stipend">
												<ItemStyle HorizontalAlign="Right" Width="12%"></ItemStyle>
												<ItemTemplate>
													<asp:TextBox id=Textbox19 runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Drawn_Sal") %>'>
													</asp:TextBox>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Leaving Reason">
												<ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
												<ItemTemplate>
													<asp:TextBox id=Textbox20 runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "LeavingReason") %>'>
													</asp:TextBox>
												</ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
										<PagerStyle NextPageText="&amp;minus;&amp;gt;" Font-Underline="True" Font-Names="Verdana" PrevPageText="&amp;lt;&amp;minus;"
											ForeColor="#CB3908" BackColor="#E0E0E0" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></td>
							</tr>
							<tr>
								<td align="right" width="100%"><asp:button id="cmdAddExp" runat="server" Width="75px" Text="Add"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="Header3" colSpan="6"><IMG id="imgSkills" style="CURSOR: hand" onclick="ShowRow('Skills')" src="Images\Plus.gif">&nbsp;<b>Skills</b></td>
				</tr>
				<tr id="trSkills" style="DISPLAY: none" runat="server">
					<td colSpan="6">
						<table id="TblSkills" cellSpacing="0" cellPadding="0" rules="none" width="100%" border="1"
							frame="border" runat="server">
						</table>
					</td>
				</tr>
				<tr>
					<td vAlign="top" colSpan="1">Other Skill(s)</td>
					<td colSpan="5"><asp:textbox id="TxtSkills" runat="server" Width="100%" Rows="3" TextMode="MultiLine" ForeColor="#003366"></asp:textbox></td>
				</tr>
				<tr>
					<td class="Header3" align="left" colSpan="6"><IMG id="imgLanguages" style="CURSOR: hand" onclick="ShowRow('Languages')" src="Images\Plus.gif">&nbsp;<B>Languages</B></td>
				</tr>
				<tr id="trLanguages" style="DISPLAY: none" runat="server">
					<td colSpan="6">
						<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="1" runat="server">
							<tr class="Header3">
								<td width="20%">Language</td>
								<td width="10%">Read</td>
								<td width="10%">Write</td>
								<td width="10%">Speak</td>
								<td width="20%">Language</td>
								<td width="10%">Read</td>
								<td width="10%">Write</td>
								<td width="10%">Speak</td>
							</tr>
						</table>
						<table id="TblLanguages" cellSpacing="0" cellPadding="0" width="100%" border="1" runat="server">
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="6">&nbsp;</td>
				</tr>
				<tr>
					<td>Upload Resume</td>
					<td colSpan="4"><input id="ResUpload" style="WIDTH: 410px" type="file" runat="server">
					<td align="left"><asp:hyperlink id="HypRes" runat="server" Width="100%" Target="_top ">Show</asp:hyperlink></td>
				</tr>
				<tr>
					<td class="Header3" colSpan="6"><b>Others Details</b></td>
				</tr>
				<tr>
					<td>Expected Salary</td>
					<td><asp:textbox id="TxtExpSalary" runat="server" Width="100%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
					<td>Nationality</td>
					<td><asp:textbox id="TxtNationality" runat="server" Width="100%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
					<td>Passport</td>
					<td colSpan="3"><asp:textbox id="TxtPassport" runat="server" Width="100%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
				</tr>
				<tr>
					<td>Refrence Type</td>
					<td><select id="cmbRefType" style="WIDTH: 180px" onchange="MaritalStatus()" runat="server">
							<option value="0">Internal</option>
							<option value="1">External</option>
							<option value="2" selected>&nbsp;</option>
						</select>
					</td>
					<td>Refree Name</td>
					<td><asp:dropdownlist id="cmbRefreeName" runat="server" Width="100%"></asp:dropdownlist><asp:textbox id="TxtRefreeName" runat="server" Width="100%" CssClass="TextBox"></asp:textbox></td>
					<td><asp:label id="LblResEntryDate" Visible="False" Width="100%" Runat="server">ResEntryDate</asp:label></td>
					<td><cc1:dtp id="dtpResEntryDate" runat="server" Visible="False" width="136px" ToolTip="Interview Date"></cc1:dtp></td>
				</tr>
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td align="left" colSpan="3"><asp:button id="CmdNew" runat="server" Width="80px" Text="New"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:button id="cmdEdit" runat="server" Width="80px" Text="Edit"></asp:button></td>
					<td align="right"><asp:checkbox id="ChkSaveAs" runat="server" Visible="False" Font-Size="Larger" Checked="False"
							Text="Save As"></asp:checkbox></td>
					<td align="right" colSpan="2"><asp:button id="cmdSave" runat="server" Width="80px" Text="Save"></asp:button>&nbsp;&nbsp;&nbsp;
						<asp:button id="cmdClose" runat="server" Width="80px" Text="Close"></asp:button></td>
				</tr>
				<tr>
					<td colspan="8" class="Header3" align="right">* Mandatory Fields</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
