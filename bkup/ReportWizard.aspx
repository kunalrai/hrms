<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ReportWizard.aspx.vb" Inherits="eHRMS.Net.ReportWizard"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ReportWizard</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
		<SCRIPT language="javascript" src="Common.js"></SCRIPT>
		<script language="VBscript">
			Dim Count
			
			Sub DeleteRow()
				Dim Sel
				Sel = document.getElementById("TxtForDel").value
				if Sel <> "" Then 
					document.getElementById("TxtCon").value = Replace(document.getElementById("TxtCon").value,Sel & "|","")
					document.getElementById(Sel).style.display = "none" 
				Else
					msgbox "Please click on the row first, to delete.",,"HRMS"
					Exit Sub
				End If
			End Sub
			
			Sub Hello(args)
				document.getElementById("TxtForDel").value = args 
			End Sub
			
			Sub ClickSelect()
				Dim StrQry
				Dim InnerHtm
				Dim Table
				
				If document.getElementById("cmbFields").value = "" Then
					msgbox "Please Select field name from the list.",,"HRMS"
					document.getElementById("cmbFields").focus
					Exit Sub
				ElseIf document.getElementById("cmbCompare").value = "1" Then	
					msgbox "Please Select comparison from the list.",,"HRMS"
					document.getElementById("cmbCompare").focus
					Exit sub
				ElseIf document.getElementById("TxtCondition").value = "" Then	
					msgbox "Please enter condition for selection.",,"HRMS"
					document.getElementById("TxtCondition").focus
					Exit sub	
				End If	
				
				Set Table = document.getElementById("TblId")
				Count=Count+1
				StrQry = "<table id='Field*" & Count & "' cellSpacing='0' cellPadding='0' width='100%' border='1' Style='Cursor:hand' OnClick='Hello(this.id)'><TR>"
				StrQry = StrQry & "<td id='Field*" & Count & "-1' width='5%'>" & document.getElementById("TxtOpnB").value & "</td>"
				StrQry = StrQry & "<td id='Field*" & Count & "-2' width='35%'>" & document.getElementById("cmbFields").value & "</td>"
				StrQry = StrQry & "<td id='Field*" & Count & "-3' width='20%'>" & document.getElementById("cmbCompare").value & "</td>"	
				StrQry = StrQry & "<td id='Field*" & Count & "-4' width='25%'>" & document.getElementById("TxtCondition").value & "</td>"
				StrQry = StrQry & "<td id='Field*" & Count & "-5' width='5%'>" & document.getElementById("TxtCloB").value & "</td>"
				StrQry = StrQry & "<td id='Field*" & Count & "-6' width='10%'>" & document.getElementById("cmbAndOr").value & "</td></tr></table>"
				Table.innerHTML = Table.innerHTML & StrQry
				
				document.getElementById("TxtCon").value = document.getElementById("TxtCon").value & "Field*" & Count & "|"
			End Sub
			
			Sub SetSelection()
				Dim sFields
				Dim Str
				Dim i
				Dim oOption
				i=0
				
				if trim(document.getElementById("TxtFields").value) <> "" Then
					sFields = mid(document.getElementById("TxtFields").value,1,len(trim(document.getElementById("TxtFields").value))-1)
					Str = split(sFields,",")
					document.getElementById("cmbFields").innerHTML = ""
					While i <= ubound(Str)
						set oOption = document.createElement("OPTION")
						oOption.Text = TRIM(Str(i))
						oOption.Value = TRIM(Str(i))
						document.getElementById("cmbFields").add(oOption)
						i = i+1
					Wend
				End If	
			End Sub
			
			Sub ShowSubTab(argID)
					if argID = "divSqlQuery" Then
						SetSQLQuery
						
					End If
					
					if argID = "divSelection" Then
						SetSelection
					End If
					 
					document.getElementById("TblFields").style.display="none"
					document.getElementById("TblSelection").style.display="none"
					document.getElementById("TblSqlQuery").style.display="none"
					
				
					document.getElementById("divFields").style.fontWeight = "normal"
					document.getElementById("divSelection").style.fontWeight = "normal"
					document.getElementById("divSqlQuery").style.fontWeight = "normal"
					
					
					document.getElementById(Replace(argID,"div","Tbl")).style.display="block"
					document.getElementById(argID).style.fontWeight = "bold"
			End Sub
			
			Sub SetSQLQuery()
				Dim StrSQL 
				Dim Fields
				Dim Tables
				if document.getElementById("TxtFields").value = "" Then Exit sub 
				if trim(document.getElementById("TxtFields").value) <> "" Then
					Fields = Mid(trim(document.getElementById("TxtFields").value),1,Len(trim(document.getElementById("TxtFields").value))-1) 
					Tables = Mid(trim(document.getElementById("TxtTables").value),1,Len(trim(document.getElementById("TxtTables").value))-1) 
					StrSQL = " Select " & Fields & " From " & Tables	
				Else		
					StrSQL = " Select" & " From " 
				End If
				document.getElementById("TxtSqlQuery").value = StrSQL	
				SetConditions()
			End Sub
			
			Sub SetConditions()
				Dim Con
				Dim StrCon
				Dim ConString
				Con = document.getElementById("TxtCon").value
				if Con = "" Then Exit sub
				'Con = mid(Con,1,len(Con)-1) 
				StrCon = Split(Con,"|")
				ConString = " Where "
				For i=0 to ubound(StrCon)
					if StrCon(i) <> "" Then 
						Dim Temp
						Set Temp = document.getElementById(StrCon(i) & "-6")
						ConString =  ConString & document.getElementById(StrCon(i) & "-1").innerText & document.getElementById(StrCon(i) & "-2").innerText 
						
						If instr(1,document.getElementById(StrCon(i) & "-3").innerText,"LIKE") > 0 Then
							If Right(document.getElementById(StrCon(i) & "-3").innerText,1) = Left(document.getElementById(StrCon(i) & "-3").innerText,1) Then
								ConString = ConString & " " & "LIKE '%" & document.getElementById(StrCon(i) & "-4").innerText & "%' "
							ElseIf Left(document.getElementById(StrCon(i) & "-3").innerText,1) = "%" Then 	
								ConString = ConString & " " & "LIKE '%" & document.getElementById(StrCon(i) & "-4").innerText & "' "
							Else
								ConString = ConString & " " & "LIKE '" & document.getElementById(StrCon(i) & "-4").innerText & "%' "
							End If
						Else
							ConString = ConString & " " & document.getElementById(StrCon(i) & "-3").innerText & " '" & document.getElementById(StrCon(i) & "-4").innerText & "'"
						End If
						
						ConString = ConString & " " & document.getElementById(StrCon(i) & "-5").innerText
						If Temp.innerText <> " " Then
							ConString = ConString & " " & Temp.innerText & " "
						Else
							document.getElementById("TxtSqlQuery").value = document.getElementById("TxtSqlQuery").value & " " & ConString
							Exit Sub 																
						End if
					End If	
				Next				
				document.getElementById("TxtSqlQuery").value = document.getElementById("TxtSqlQuery").value & " " & ConString
			End Sub
			
			Sub OpenPage()
				document.getElementById("cmdExecute").click 
				window.open "frmHTMLReports.aspx",""  
			End Sub
			
			Sub SetFields(arg)
				Dim Tbl
				Dim FieldName
				Tbl = mid(arg,4,instr(1,arg,"|")-4)
				FieldName = mid(arg,instr(1,arg,"|")+1,len(arg)-instr(1,arg,"|")+1)
				if document.getElementById(arg).checked = true then
					document.getElementById("TxtFields").value = document.getElementById("TxtFields").value & Tbl & "." & FieldName & ", "
				Else
					document.getElementById("TxtFields").value = replace(document.getElementById("TxtFields").value,Tbl & "." & FieldName & ",","")
				End if 
				document.getElementById("TxtTables").value = replace(document.getElementById("TxtTables").value, Tbl & ",","")
				document.getElementById("TxtTables").value = document.getElementById("TxtTables").value & Tbl & ", "
			End Sub
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx --><br>
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="700" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Report 
						Wizard....
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" rules="none" width="700" border="1" frame="border">
				<tr>
					<td><asp:label id="LblErrMsg" runat="server" Width="100%" ForeColor="Red"></asp:label></td>
				</tr>
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><!-- Sub Table -->
						<table height="30" cellSpacing="0" cellPadding="0" width="50%" align="left" border="0">
							<tr vAlign="bottom">
								<td align="center" width="100">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr id="divEarningsHead">
											<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/leftoverlap.gif" border="0"></td>
											<td style="COLOR: #003366" align="center" bgColor="#cecbce">
												<DIV id="divFields" style="DISPLAY: inline; FONT-WEIGHT: bold; WIDTH: 70px; CURSOR: hand; HEIGHT: 15px"
													onclick="ShowSubTab(this.id)" ms_positioning="FlowLayout">Fields</DIV>
											</td>
											<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
										</tr>
									</table>
								</td>
								<td width="100">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr id="divDeductionsHead">
											<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/leftoverlap.gif" border="0"></td>
											<td style="COLOR: #003366" align="center" bgColor="#cecbce">
												<DIV id="divSelection" style="DISPLAY: inline; WIDTH: 70px; CURSOR: hand; HEIGHT: 15px"
													onclick="ShowSubTab(this.id)" ms_positioning="FlowLayout">Selection</DIV>
											</td>
											<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
										</tr>
									</table>
								</td>
								<td width="100">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr id="divReimbursmentHead">
											<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/SplitLeft.gif" border="0"></td>
											<td style="COLOR: #003366" align="center" bgColor="#cecbce">
												<DIV id="divSqlQuery" style="DISPLAY: inline; WIDTH: 70px; CURSOR: hand; HEIGHT: 15px"
													onclick="ShowSubTab(this.id)" ms_positioning="FlowLayout">SQL Reports</DIV>
											</td>
											<td width="10" bgColor="#cecbce"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table id="TblFields" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2">&nbsp;</td>
							</tr>
							<tr>
								<td class="Header3" colSpan="2">Step : Select fields to include in report.</td>
							</tr>
							<tr>
								<td colSpan="2">&nbsp;</td>
							</tr>
							<tr>
								<td>
									<div style="OVERFLOW: auto; WIDTH: 350px; BORDER-TOP-STYLE: outset; BORDER-RIGHT-STYLE: outset; BORDER-LEFT-STYLE: outset; HEIGHT: 250px">
										<table id="TblTableFields" cellSpacing="0" cellPadding="0" width="80%" runat="server">
										</table>
									</div>
								</td>
								<td>
									<div style="OVERFLOW: auto; WIDTH: 340px; BORDER-TOP-STYLE: outset; BORDER-RIGHT-STYLE: outset; BORDER-LEFT-STYLE: outset; HEIGHT: 250px"></div>
								</td>
							</tr>
						</table>
						<table id="TblSelection" style="DISPLAY: none" cellSpacing="0" cellPadding="0" width="100%"
							runat="server">
							<tr>
								<td>
									<div style="WIDTH: 695px; BORDER-TOP-STYLE: outset; HEIGHT: 200px">
										<table id="TblSelect" cellSpacing="0" cellPadding="0" width="100%" runat="server">
											<tr>
												<td width="5%"></td>
												<td width="35%"></td>
												<td width="20%"></td>
												<td width="25%"></td>
												<td width="5%"></td>
												<td width="10%"></td>
											</tr>
											<tr>
												<td class="Header3">&nbsp;</td>
												<td class="Header3">Field Name</td>
												<td class="Header3">Comparison</td>
												<td class="Header3">Compare To</td>
												<td class="Header3">&nbsp;</td>
												<td class="Header3">And/Or</td>
											</tr>
											<tr>
												<td id="TblId" colSpan="6"></td>
											</tr>
										</table>
									</div>
									<div style="WIDTH: 695px; BORDER-TOP-STYLE: outset; HEIGHT: 75px">
										<table cellSpacing="0" cellPadding="0" width="100%">
											<tr>
												<td width="5%"></td>
												<td width="35%"></td>
												<td width="20%"></td>
												<td width="25%"></td>
												<td width="5%"></td>
												<td width="10%"></td>
											</tr>
											<tr>
												<td class="Header3"><asp:textbox id="TxtOpnB" Width="100%" ForeColor="#003366" Runat="server"></asp:textbox></td>
												<td class="Header3"><asp:dropdownlist id="cmbFields" Width="100%" Runat="server"></asp:dropdownlist></td>
												<td class="Header3" align="left"><asp:dropdownlist id="cmbCompare" Width="100%" Runat="server">
														<asp:ListItem Value="1" Selected="True">&nbsp;</asp:ListItem>
														<asp:ListItem Value="=">Equal to</asp:ListItem>
														<asp:ListItem Value="LIKE%">Starts With</asp:ListItem>
														<asp:ListItem Value="%LIKE">Endss With</asp:ListItem>
														<asp:ListItem Value="%LIKE%">Contains</asp:ListItem>
														<asp:ListItem Value="<>">Not Equal to</asp:ListItem>
														<asp:ListItem Value=">">Greater Than</asp:ListItem>
														<asp:ListItem Value=">=">Greater Than Or Equal to</asp:ListItem>
														<asp:ListItem Value="<">Less Than</asp:ListItem>
														<asp:ListItem Value="<=">Less Than Or Equal to</asp:ListItem>
													</asp:dropdownlist></td>
												<td class="Header3"><asp:textbox id="TxtCondition" Width="100%" Runat="server"></asp:textbox></SELECT></td>
												<td class="Header3"><asp:textbox id="TxtCloB" Width="100%" ForeColor="#003366" Runat="server"></asp:textbox></td>
												<td class="Header3"><asp:dropdownlist id="cmbAndOr" Width="100%" Runat="server">
														<asp:ListItem Value="&nbsp;" Selected="True">&nbsp;</asp:ListItem>
														<asp:ListItem Value="AND">AND</asp:ListItem>
														<asp:ListItem Value="OR">OR</asp:ListItem>
													</asp:dropdownlist></td>
											</tr>
											<tr>
												<td colSpan="6">&nbsp;</td>
											</tr>
											<tr>
												<td align="right" colSpan="6"><input id="cmdAdd" style="WIDTH: 60px" onclick="ClickSelect()" type="button" value="Add"
														name="cmdAdd" runat="server">&nbsp;&nbsp; <input id="cmdDelete" style="WIDTH: 60px" onclick="DeleteRow()" type="button" value="Delete"
														name="cmdDelete" runat="server">
												</td>
											</tr>
										</table>
									</div>
								</td>
							</tr>
						</table>
						<table id="TblSqlQuery" style="DISPLAY: none" cellSpacing="0" cellPadding="0" width="100%"
							runat="server">
							<tr>
								<td>&nbsp;</td>
							</tr>
							<tr>
								<td>
									<div style="WIDTH: 695px; BORDER-TOP-STYLE: outset; HEIGHT: 275px"><asp:textbox id="TxtSqlQuery" runat="server" Width="100%" Rows="20" TextMode="MultiLine">Select From</asp:textbox></div>
								</td>
							</tr>
							<TR>
								<td>
									&nbsp;Description&nbsp;
									<asp:textbox id="TxtSqlDesc" Width="175px" ForeColor="#003366" Runat="server" CssClass="textBox"></asp:textbox>&nbsp;
									<asp:button id="CmdSaveSqlQry" Width="85px" Runat="server" Text="Save Query"></asp:button>&nbsp;&nbsp;
									<asp:label id="LblSelectQry" Runat="server">Select Query</asp:label>&nbsp;
									<asp:dropdownlist id="CmbSqlDesc" AutoPostBack="True" Width="175px" Runat="server"></asp:dropdownlist>
									<asp:button id="CmdSqlQryDel" Width="85px" Runat="server" Text="Delete Query"></asp:button></td>
							</TR>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="0" cellPadding="0" rules="none" width="100%" border="1" frame="border">
							<tr>
								<td align="right"><input id="CmdOk" style="FONT-WEIGHT: bold; FONT-SIZE: medium; WIDTH: 50px; FONT-FAMILY: Wingdings; HEIGHT: 24px"
										onclick="OpenPage()" type="button" value="ü" name="CmdOk" runat="server">&nbsp;&nbsp;
									<asp:button id="cmdClose" runat="server" Width="50px" Font-Bold="True" Text="X"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<asp:textbox id="TxtForDel" style="Z-INDEX: 107; LEFT: 856px; POSITION: absolute; TOP: 40px"
				runat="server" Width="0px"></asp:textbox><asp:textbox id="TxtCon" style="Z-INDEX: 106; LEFT: 816px; POSITION: absolute; TOP: 48px" runat="server"
				Width="0px"></asp:textbox><asp:button id="cmdAddFields" style="Z-INDEX: 105; LEFT: 800px; POSITION: absolute; TOP: 144px"
				runat="server" Width="0px" Text="Button"></asp:button><asp:textbox id="TxtStrSelect" style="Z-INDEX: 104; LEFT: 792px; POSITION: absolute; TOP: 56px"
				runat="server" Width="0px"></asp:textbox><asp:textbox id="TxtTables" style="Z-INDEX: 103; LEFT: 744px; POSITION: absolute; TOP: 56px"
				runat="server" Width="0px"></asp:textbox><asp:textbox id="TxtFields" style="Z-INDEX: 102; LEFT: 768px; POSITION: absolute; TOP: 56px"
				runat="server" Width="0px"></asp:textbox>
			<asp:Button id="cmdExecute" style="Z-INDEX: 101; LEFT: 720px; POSITION: absolute; TOP: 56px"
				runat="server" Width="0px" Text="Button"></asp:Button></form>
	</body>
</HTML>
