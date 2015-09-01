<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Compensation.aspx.vb" Inherits="eHRMS.Net.Compensation"%>
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
		<script language="VBscript">
			Sub ShowSubTab(argID)
					'document.getElementById("txtTabNum").value = Replace(argID,"div","tr")
					document.getElementById("txtTabNum").value = argID
					
					document.getElementById("divEarnings").style.backgroundColor = "#cecbce"
					document.getElementById("divDeductions").style.backgroundColor = "#cecbce"
					document.getElementById("divReimbursment").style.backgroundColor = "#cecbce"
					document.getElementById("divPerquisites").style.backgroundColor = "#cecbce"
					document.getElementById("divInvestment").style.backgroundColor = "#cecbce"
					document.getElementById("divFurniture").style.backgroundColor = "#cecbce"
					document.getElementById("divOthers").style.backgroundColor = "#cecbce"
					
					document.getElementById("divEarnings").style.color  = "#003366"
					document.getElementById("divDeductions").style.color  = "#003366"
					document.getElementById("divReimbursment").style.color  = "#003366"
					document.getElementById("divPerquisites").style.color  = "#003366"
					document.getElementById("divInvestment").style.color  = "#003366"
					document.getElementById("divFurniture").style.color  = "#003366"
					document.getElementById("divOthers").style.color  = "#003366"
					
					document.getElementById("trEarnings").style.display="none"
					document.getElementById("trDeductions").style.display="none"
					document.getElementById("trReimbursment").style.display="none"
					document.getElementById("trPerquisites").style.display="none"
					document.getElementById("trInvestment").style.display="none"
					document.getElementById("trFurniture").style.display="none"
					document.getElementById("trOthers").style.display="none"
														
					document.getElementById(argID).style.backgroundColor = "#666666"
					document.getElementById(argID).style.color  = "White"	
																
					document.getElementById(replace(argID,"div","tr")).style.display = "block"
										
					'document.getElementById(argID).style.fontWeight = "bold"
							
					
					'document.getElementById(replace(argID,"div","tr")).style.backgroundColor = "#666666"
					'document.getElementById(replace(argID,"div","tr")).style.color = "White"	
			End Sub

			Sub Checkdate(argID)
				Dim TVal
				TVal = document.getElementById(argID).value
				if trim(TVal) = "" then 
					Exit Sub
				End if
				if isdate(TVal) then 
					If Len(TVal) = 11 Then
						If Not ( (Mid(TVal, 3, 1) = "/" Or Mid(TVal, 3, 1) = "-") And (Mid(TVal, 7, 1) = "/" Or Mid(TVal, 7, 1) = "-") ) Then
							MsgBox "Invalid Format! Please Enter in [dd/MMM/yyyy] Format."  , "Divergent"
							document.getElementById(argID).value = ""
						End If
					Else
						MsgBox "Invalid Format! Please Enter in [dd/MMM/yyyy] Format.", , "Divergent"
						document.getElementById(argID).value = ""
					End If
				Else
					MsgBox "Invalid Date!", vbokOnly, "Divergent"
					document.getElementById(argID).value = ""
				End if
			End Sub
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
			
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="5" onload="ShowSubTab(document.getElementById('txtTabNum').value)"
		rightMargin="0" MS_POSITIONING="GridLayout">
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
								<td width="10" bgColor="#666666"><IMG alt="" src="Images/SplitLeft.gif" border="0"></td>
								<td style="COLOR: white" align="center" bgColor="#666666"><b>Compensation</b></td>
								<td width="10" bgColor="#666666"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
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
					<td colSpan="9"><!-- Employee Code -->
						<table borderColor="black" cellSpacing="0" cellPadding="0" rules="none" width="100%" border="1"
							frame="box">
							<tr>
								<td><asp:label id="LblMsg" runat="server" ForeColor="Red" Font-Size="11px" Width="100%"></asp:label></td>
							</tr>
							<tr>
								<td width="100%">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td width="20%"><asp:label id="lblCode" runat="server" Width="100%">Code</asp:label></td>
											<td width="30%"><INPUT id="BtnFirst" style="FONT-WEIGHT: bold; COLOR: #0033ff; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: #ffffff; BORDER-BOTTOM-STYLE: none"
													type="button" value="<<" name="Button1" runat="server">&nbsp;&nbsp; <INPUT id="BtnPre" style="FONT-WEIGHT: bold; COLOR: #0033ff; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: #ffffff; BORDER-BOTTOM-STYLE: none"
													type="button" value="<" name="Button1" runat="server">&nbsp;
												<asp:textbox id="txtEM_CD" runat="server" Width="136px" ForeColor="#003366" AutoPostBack="True"
													Font-Bold="True" BackColor="#E1E4EB" CssClass="TextBox"></asp:textbox>&nbsp;<INPUT id="BtnNext" style="FONT-WEIGHT: bold; COLOR: #0033ff; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: #ffffff; BORDER-BOTTOM-STYLE: none"
													type="button" value=">" name="Button1" runat="server">&nbsp;&nbsp; <INPUT id="BtnLast" style="FONT-WEIGHT: bold; COLOR: #0033ff; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: #ffffff; BORDER-BOTTOM-STYLE: none"
													type="button" value=">>" name="Button1" runat="server"></td>
											<td align="center" width="50%"><asp:label id="LblName" runat="server" ForeColor="#003366" Font-Size="9" Width="100%" Font-Bold="True"></asp:label></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr align="left">
								<td><!-- Sub Table -->
									<table height="30" cellSpacing="0" cellPadding="0" width="100%" align="left" border="0">
										<tr vAlign="bottom">
											<td align="center" width="100">
												<table id="divEarnings" cellSpacing="0" cellPadding="0" width="100%" bgColor="#666666"
													border="0">
													<tr>
														<td width="10"><IMG alt="" src="Images/SplitLeft.gif" border="0"></td>
														<td style="CURSOR: hand" onclick="ShowSubTab('divEarnings');" align="center"><b>Earnings</b></td>
														<td width="10"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
													</tr>
												</table>
											</td>
											<td width="100">
												<table id="divDeductions" cellSpacing="0" cellPadding="0" width="100%" bgColor="#cecbce"
													border="0">
													<tr>
														<td width="10"><IMG alt="" src="Images/SplitLeft.gif" border="0"></td>
														<td style="CURSOR: hand" onclick="ShowSubTab('divDeductions');" align="center"><b>Deductions</b></td>
														<td width="10"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
													</tr>
												</table>
											</td>
											<td width="100">
												<table id="divReimbursment" cellSpacing="0" cellPadding="0" width="100%" bgColor="#cecbce"
													border="0">
													<tr>
														<td width="10"><IMG alt="" src="Images/SplitLeft.gif" border="0"></td>
														<td style="CURSOR: hand" onclick="ShowSubTab('divReimbursment');" align="center"><b>Reimbursment</b></td>
														<td width="10"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
													</tr>
												</table>
											</td>
											<td width="100">
												<table id="divPerquisites" cellSpacing="0" cellPadding="0" width="100%" bgColor="#cecbce"
													border="0">
													<tr>
														<td width="10"><IMG alt="" src="Images/SplitLeft.gif" border="0"></td>
														<td style="CURSOR: hand" onclick="ShowSubTab('divPerquisites');" align="center"><b>Perquisites</b></td>
														<td width="10"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
													</tr>
												</table>
											</td>
											<td width="100">
												<table id="divInvestment" cellSpacing="0" cellPadding="0" width="100%" bgColor="#cecbce"
													border="0">
													<tr>
														<td width="10"><IMG alt="" src="Images/SplitLeft.gif" border="0"></td>
														<td style="CURSOR: hand" onclick="ShowSubTab('divInvestment');" align="center"><b>Investment</b></td>
														<td width="10"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
													</tr>
												</table>
											</td>
											<td width="100">
												<table id="divFurniture" cellSpacing="0" cellPadding="0" width="100%" bgColor="#cecbce"
													border="0">
													<tr>
														<td width="10"><IMG alt="" src="Images/SplitLeft.gif" border="0"></td>
														<td style="CURSOR: hand" onclick="ShowSubTab('divFurniture');" align="center"><b>Furniture</b></td>
														<td width="10"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
													</tr>
												</table>
											</td>
											<td width="80">
												<table id="divOthers" cellSpacing="0" cellPadding="0" width="100%" bgColor="#cecbce" border="0">
													<tr>
														<td width="10"><IMG alt="" src="Images/SplitLeft.gif" border="0"></td>
														<td style="CURSOR: hand" onclick="ShowSubTab('divOthers');" align="center"><b>Others</b></td>
														<td width="10"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
													</tr>
												</table>
											</td>
										</tr>
										<tr id="trEarnings">
											<td colSpan="7"><asp:datagrid id="GrdEarnings" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="False"
													AllowSorting="True">
													<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
													<HeaderStyle HorizontalAlign="Center" CssClass="Header3"></HeaderStyle>
													<Columns>
														<asp:TemplateColumn HeaderText="Description">
															<ItemStyle HorizontalAlign="Left" Width="85%"></ItemStyle>
															<ItemTemplate>
																<asp:Label id=lblDesc_Ern runat="server" text='<%# DataBinder.Eval(Container.DataItem, "FIELD_DESC") %>'>
																</asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Amount">
															<ItemStyle HorizontalAlign="Right" Width="15%"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox id=txtAmount_Ern onblur=CheckNum(this.id) style="TEXT-ALIGN: right" runat="server" Width="100%" AutoPostBack="True" CssClass="TextBox" OnTextChanged="OnAmountChanged" Text='<%# DataBinder.Eval(Container.DataItem, "Field_N") %>' Visible="False" MaxLength='<%# DataBinder.Eval(Container.DataItem, "Field_Len") %>'>
																</asp:TextBox>
																<asp:Label id=LblAmount_Ern runat="server" text='<%# DataBinder.Eval(Container.DataItem, "Field_N") %>' Visible="False">
																</asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn>
															<HeaderStyle Width="0px"></HeaderStyle>
															<ItemStyle HorizontalAlign="Center" Width="0px"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox id=txtField_Name_Ern runat="server" Width="0Px" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "FIELD_NAME") %>' Enabled="False">
																</asp:TextBox>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn>
															<HeaderStyle Width="0px"></HeaderStyle>
															<ItemStyle HorizontalAlign="Center" Width="0px"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox id=txtDataType_Ern runat="server" Width="0Px" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Field_Type") %>' Enabled="False">
																</asp:TextBox>
															</ItemTemplate>
														</asp:TemplateColumn>
													</Columns>
													<PagerStyle NextPageText="&amp;minus;&amp;gt;" Font-Underline="True" Font-Names="Verdana" PrevPageText="&amp;lt;&amp;minus;"
														ForeColor="#CB3908" BackColor="#E0E0E0" Mode="NumericPages"></PagerStyle>
												</asp:datagrid></td>
										</tr>
										<tr id="trDeductions">
											<td colSpan="7"><asp:datagrid id="GrdDeductions" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="False"
													AllowSorting="True">
													<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
													<HeaderStyle CssClass="Header3" HorizontalAlign="Center"></HeaderStyle>
													<Columns>
														<asp:TemplateColumn HeaderText="Description">
															<ItemStyle HorizontalAlign="Left" Width="85%"></ItemStyle>
															<ItemTemplate>
																<asp:Label id="lblDesc_Ded" runat="server" text='<%# DataBinder.Eval(Container.DataItem, "FIELD_DESC") %>'>
																</asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Amount">
															<ItemStyle HorizontalAlign="Right" Width="15%"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox Width="100%" style ="TEXT-ALIGN: right" runat="server" ID="txtAmount_Ded" CssClass="TextBox" AutoPostBack=True OnTextChanged="OnAmountChanged" Text='<%# DataBinder.Eval(Container.DataItem, "Field_N") %>' MaxLength='<%# DataBinder.Eval(Container.DataItem, "Field_Len") %>' Visible=False onBlur="CheckNum(this.id)" />
																<asp:Label id="lblAmount_Ded" runat="server" Visible="False" text='<%# DataBinder.Eval(Container.DataItem, "Field_N") %>'>
																</asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="" ItemStyle-Width="0Px" HeaderStyle-Width="0Px">
															<ItemStyle HorizontalAlign="Center" Width="0Px"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox Width="0Px" Enabled=False runat="server" ID="txtField_Name_Ded" ReadOnly=True CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Field_Name") %>' />
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="" ItemStyle-Width="0Px" HeaderStyle-Width="0Px">
															<ItemStyle HorizontalAlign="Center" Width="0Px"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox Width="0Px" Enabled=False runat="server" ID="txtDataType_Ded" ReadOnly=True CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Field_Type") %>' />
															</ItemTemplate>
														</asp:TemplateColumn>
													</Columns>
													<PagerStyle NextPageText="&amp;minus;&amp;gt;" Font-Underline="True" Font-Names="Verdana" PrevPageText="&amp;lt;&amp;minus;"
														ForeColor="#CB3908" BackColor="#E0E0E0" Mode="NumericPages"></PagerStyle>
												</asp:datagrid></td>
										</tr>
										<tr id="trReimbursment">
											<td colSpan="7"><asp:datagrid id="GrdReim" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="False"
													AllowSorting="True">
													<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
													<HeaderStyle HorizontalAlign="Center" CssClass="Header3"></HeaderStyle>
													<Columns>
														<asp:TemplateColumn HeaderText="Description">
															<ItemStyle HorizontalAlign="Left" Width="25%"></ItemStyle>
															<ItemTemplate>
																<asp:Label id="lblDesc_Reim" runat="server" text='<%# DataBinder.Eval(Container.DataItem, "FIELD_DESC") %>'>
																</asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="WEF Date">
															<ItemStyle HorizontalAlign="Center" Width="12%"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox id=txtWEF_Reim onblur=Checkdate(this.id) style="TEXT-ALIGN: right" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox" AutoPostBack=True OnTextChanged="OnReimChanged" Text='<%# GetWEFDate(DataBinder.Eval(Container.DataItem, "WEF")) %>'>
																</asp:TextBox>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Opening">
															<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox id=txtOpening_Reim onblur=CheckNum(this.id) style="TEXT-ALIGN: right" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox" AutoPostBack=True OnTextChanged="OnReimChanged" Text='<%# DataBinder.Eval(Container.DataItem, "Opening")%>'>
																</asp:TextBox>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Budget">
															<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox id=txtBudget_Reim onblur=CheckNum(this.id) style="TEXT-ALIGN: right" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox" AutoPostBack=True OnTextChanged="OnReimChanged" Text='<%# DataBinder.Eval(Container.DataItem, "Budget") %>'>
																</asp:TextBox>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Prorata">
															<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox id=txtProrata_Reim onblur=CheckNum(this.id) style="TEXT-ALIGN: right" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox" ReadOnly="True" Text='<%# DataBinder.Eval(Container.DataItem, "Prorata") %>'>
																</asp:TextBox>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Spl. Budget">
															<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox id=txtSplBudget_Reim onblur=CheckNum(this.id) style="TEXT-ALIGN: right" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox" AutoPostBack=True OnTextChanged="OnReimChanged" Text='<%# DataBinder.Eval(Container.DataItem, "SplBudget") %>'>
																</asp:TextBox>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Reimbursed">
															<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox id=txtReimbursed_Reim onblur=CheckNum(this.id) style="TEXT-ALIGN: right" runat="server" Width="100%" ForeColor="#003366" ReadOnly="True" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Reimbursed") %>'>
																</asp:TextBox>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Balance">
															<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox id=txtBalance_Reim style="TEXT-ALIGN: right" runat="server" Width="100%" ForeColor="#003366" ReadOnly="True" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Balance") %>'>
																</asp:TextBox>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn>
															<HeaderStyle Width="0px"></HeaderStyle>
															<ItemStyle HorizontalAlign="Center" Width="0px"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox id=txtField_Name_Reim runat="server" Width="0Px" ReadOnly="True" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Field_Name") %>'>
																</asp:TextBox>
															</ItemTemplate>
														</asp:TemplateColumn>
													</Columns>
													<PagerStyle NextPageText="&amp;minus;&amp;gt;" Font-Underline="True" Font-Names="Verdana" PrevPageText="&amp;lt;&amp;minus;"
														ForeColor="#CB3908" BackColor="#E0E0E0" Mode="NumericPages"></PagerStyle>
												</asp:datagrid></td>
										</tr>
										<tr id="trPerquisites">
											<td colSpan="7"><asp:datagrid id="GrdPerquisites" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="False"
													AllowSorting="True">
													<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
													<HeaderStyle CssClass="Header3" HorizontalAlign="Center"></HeaderStyle>
													<Columns>
														<asp:TemplateColumn HeaderText="Description">
															<ItemStyle HorizontalAlign="Left" Width="85%"></ItemStyle>
															<ItemTemplate>
																<asp:Label id="lblDesc_Perk" runat="server" text='<%# DataBinder.Eval(Container.DataItem, "FIELD_DESC") %>'>
																</asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Amount">
															<ItemStyle HorizontalAlign="Right" Width="15%"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox Width="100%" runat="server" style ="TEXT-ALIGN: right" ID="txtAmount_Perk" CssClass="TextBox" AutoPostBack=True OnTextChanged="OnAmountChanged" Text='<%# DataBinder.Eval(Container.DataItem, "Field_N") %>' MaxLength='<%# DataBinder.Eval(Container.DataItem, "Field_Len") %>' Visible=False onBlur="CheckNum(this.id)" />
																<asp:Label id="lblAmount_Perk" runat="server" Visible="False" text='<%# DataBinder.Eval(Container.DataItem, "Field_N") %>'>
																</asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="" ItemStyle-Width="0Px" HeaderStyle-Width="0Px">
															<ItemStyle HorizontalAlign="Center" Width="0Px"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox Width="0Px" Enabled=False runat="server" ID="txtField_Name_Perk" ReadOnly=True CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Field_Name") %>' />
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="" ItemStyle-Width="0Px" HeaderStyle-Width="0Px">
															<ItemStyle HorizontalAlign="Center" Width="0Px"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox Width="0Px" Enabled=False runat="server" ID="txtDataType_Perk" ReadOnly=True CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Field_Type") %>' />
															</ItemTemplate>
														</asp:TemplateColumn>
													</Columns>
													<PagerStyle NextPageText="&amp;minus;&amp;gt;" Font-Underline="True" Font-Names="Verdana" PrevPageText="&amp;lt;&amp;minus;"
														ForeColor="#CB3908" BackColor="#E0E0E0" Mode="NumericPages"></PagerStyle>
												</asp:datagrid></td>
										</tr>
										<tr id="trInvestment">
											<td colSpan="7"><asp:datagrid id="GrdInvestment" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="False"
													AllowSorting="True">
													<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
													<HeaderStyle CssClass="Header3" HorizontalAlign="Center"></HeaderStyle>
													<Columns>
														<asp:TemplateColumn HeaderText="Description">
															<ItemStyle HorizontalAlign="Left" Width="85%"></ItemStyle>
															<ItemTemplate>
																<asp:Label id="lblDesc_Inv" runat="server" text='<%# DataBinder.Eval(Container.DataItem, "FIELD_DESC") %>'>
																</asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Amount">
															<ItemStyle HorizontalAlign="Right" Width="15%"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox Width="100%" runat="server" style ="TEXT-ALIGN: right" ID="txtAmount_Inv" CssClass="TextBox" AutoPostBack=True OnTextChanged="OnAmountChanged" Text='<%# DataBinder.Eval(Container.DataItem, "Field_N") %>' MaxLength='<%# DataBinder.Eval(Container.DataItem, "Field_Len") %>' Visible=False />
																<asp:Label id="lblAmount_Inv" runat="server" Visible="False" text='<%# DataBinder.Eval(Container.DataItem, "Field_N") %>'>
																</asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="" ItemStyle-Width="0Px" HeaderStyle-Width="0Px">
															<ItemStyle HorizontalAlign="Center" Width="0Px"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox Width="0Px" Enabled=False runat="server" ID="txtField_Name_Inv" ReadOnly=True CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Field_Name") %>' />
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="" ItemStyle-Width="0Px" HeaderStyle-Width="0Px">
															<ItemStyle HorizontalAlign="Center" Width="0Px"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox Width="0Px" Enabled=False runat="server" ID="txtDataType_Inv" ReadOnly=True CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Field_Type") %>' />
															</ItemTemplate>
														</asp:TemplateColumn>
													</Columns>
													<PagerStyle NextPageText="&amp;minus;&amp;gt;" Font-Underline="True" Font-Names="Verdana" PrevPageText="&amp;lt;&amp;minus;"
														ForeColor="#CB3908" BackColor="#E0E0E0" Mode="NumericPages"></PagerStyle>
												</asp:datagrid></td>
										</tr>
										<tr id="trFurniture">
											<td colSpan="7"><asp:datagrid id="GrdFurn" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="False"
													AllowSorting="True">
													<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
													<HeaderStyle HorizontalAlign="Center" CssClass="Header3"></HeaderStyle>
													<Columns>
														<asp:TemplateColumn HeaderText="Description">
															<ItemStyle HorizontalAlign="Left" Width="55%"></ItemStyle>
															<ItemTemplate>
																<asp:Label id="lblDesc_Furn" runat="server" text='<%# DataBinder.Eval(Container.DataItem, "Furn_DESC") %>'>
																</asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Bill Date">
															<ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox id=txtBillDate_Furn onblur=Checkdate(this.id) style="TEXT-ALIGN: right" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Bill_Date") %>'>
																</asp:TextBox>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Cost">
															<ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox id=txtCost_Furn onblur=CheckNum(this.id) style="TEXT-ALIGN: right" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Bill_Cost") %>' Visible=False >
																</asp:TextBox>
																<asp:Label id="lblCost_Furn" runat="server" Visible="False" text='<%# DataBinder.Eval(Container.DataItem, "Bill_Cost") %>'>
																</asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Perk (%)">
															<ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox id=txtPerk_Furn style="TEXT-ALIGN: right" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Perk_per") %>'>
																</asp:TextBox>
															</ItemTemplate>
														</asp:TemplateColumn>
													</Columns>
													<PagerStyle NextPageText="&amp;minus;&amp;gt;" Font-Underline="True" Font-Names="Verdana" PrevPageText="&amp;lt;&amp;minus;"
														ForeColor="#CB3908" BackColor="#E0E0E0" Mode="NumericPages"></PagerStyle>
												</asp:datagrid></td>
										</tr>
										<tr id="trOthers">
											<td colSpan="7"><asp:datagrid id="GrdOthers" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="False"
													AllowSorting="True">
													<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
													<HeaderStyle CssClass="Header3" HorizontalAlign="Center"></HeaderStyle>
													<Columns>
														<asp:TemplateColumn HeaderText="Description">
															<ItemStyle HorizontalAlign="Left" Width="55%"></ItemStyle>
															<ItemTemplate>
																<asp:Label id="lblDesc_Oth" runat="server" text='<%# DataBinder.Eval(Container.DataItem, "FIELD_DESC") %>'>
																</asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Date">
															<ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox Width="100%" runat="server" ID="txtDate_Oth" onBlur="Checkdate(this.id)" CssClass="TextBox" AutoPostBack=True OnTextChanged="OnOthersChanged_D" Enabled='<%#iif(DataBinder.Eval(Container.DataItem, "Field_Type")="D",True,false)%>' Text='<%# GetWEFDate(DataBinder.Eval(Container.DataItem, "Field_D")) %>' />
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Number">
															<ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox Width="100%" runat="server" ID="txtNum_Oth" CssClass="TextBox" AutoPostBack=True OnTextChanged="OnOthersChanged_N" Enabled='<%#iif(DataBinder.Eval(Container.DataItem, "Field_Type")="N",True,false)%>' Text='<%# DataBinder.Eval(Container.DataItem, "Field_N") %>' MaxLength='<%# DataBinder.Eval(Container.DataItem, "Field_Len") %>' onBlur="CheckNum(this.id)" />
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Text">
															<ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox Width="100%" runat="server" ID="txtText_Oth" CssClass="TextBox" AutoPostBack=True OnTextChanged="OnOthersChanged_C" Enabled='<%#iif(DataBinder.Eval(Container.DataItem, "Field_Type")="C",True,false)%>' Text='<%# DataBinder.Eval(Container.DataItem, "Field_C") %>' MaxLength='<%# DataBinder.Eval(Container.DataItem, "Field_Len") %>' />
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="" ItemStyle-Width="0Px" HeaderStyle-Width="0Px">
															<ItemStyle HorizontalAlign="Center" Width="0Px"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox Width="0Px" runat="server" ID="txtField_Name_Oth" Enabled=false CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Field_Name") %>' />
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="" ItemStyle-Width="0Px" HeaderStyle-Width="0Px" Visible="False">
															<ItemStyle HorizontalAlign="Center" Width="0Px"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox Width="0Px" Enabled=False runat="server" ID="txtDataType_Oth" ReadOnly=True CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "Field_Type") %>' />
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="" ItemStyle-Width="0Px" HeaderStyle-Width="0Px" Visible="False">
															<ItemStyle HorizontalAlign="Center" Width="0Px"></ItemStyle>
															<ItemTemplate>
																<asp:TextBox Width="0Px" Enabled=False runat="server" ID="txtTableName_Oth" ReadOnly=True CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "TableName") %>' />
															</ItemTemplate>
														</asp:TemplateColumn>
													</Columns>
													<PagerStyle NextPageText="&amp;minus;&amp;gt;" Font-Underline="True" Font-Names="Verdana" PrevPageText="&amp;lt;&amp;minus;"
														ForeColor="#CB3908" BackColor="#E0E0E0" Mode="NumericPages"></PagerStyle>
												</asp:datagrid></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td align="right"><asp:button id="cmdSave" accessKey="S" runat="server" Width="75px" Text="Save"></asp:button>&nbsp;
									<asp:textbox id="txtTabNum" runat="server" Width="0px" ReadOnly="True" Height="0px">divEarnings</asp:textbox><asp:button id="cmdClose" runat="server" Width="75px" Text="Close"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td style="HEIGHT: 14px" align="center" colSpan="9"><asp:label id="LblRights" Font-Size="10" Width="100%" Font-Bold="True" Runat="server"></asp:label></td>
				</tr>
				<tr>
					<td align="center" colSpan="9"><asp:datagrid id="DataGrid1" runat="server"></asp:datagrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
