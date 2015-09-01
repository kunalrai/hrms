<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Others.aspx.vb" Inherits="eHRMS.Net.Others"%>
<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
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
		<script language="Javascript">
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
		<script language="vbscript">
			Sub Val(argid)
				IF document.getElementById(argid).Checked = False THEN
					document.getElementById(replace(argid,"Chk","Dtp")).disabled = true
					document.getElementById(replace(argid,"Chk","Dtp") & "cmbDD").disabled = true
					document.getElementById(replace(argid,"Chk","Dtp") & "cmbMM").disabled = true
					document.getElementById(replace(argid,"Chk","Dtp") & "cmbYY").disabled = true
				ELSE
					document.getElementById(replace(argid,"Chk","Dtp")).disabled = false
					document.getElementById(replace(argid,"Chk","Dtp") & "cmbDD").disabled = false
					document.getElementById(replace(argid,"Chk","Dtp") & "cmbMM").disabled = false
					document.getElementById(replace(argid,"Chk","Dtp") & "cmbYY").disabled = false
				End If
			END SUB
			
			SUB Change()
			 	IF document.getElementById("cmbLeaveType").value = 2 THEN
			 		document.getElementById("LblDate1").innerText = "Resignated Date"
			 		document.getElementById("LblDate2").innerText = "Releaving Date"
			 		document.getElementById("LblDate3").innerText = "Settlement Date"
			 	ELSEIF document.getElementById("cmbLeaveType").value = 3 THEN
			 		document.getElementById("LblDate1").innerText = "Notice Date"
			 		document.getElementById("LblDate2").innerText = "Termination Date"
			 		document.getElementById("LblDate3").innerText = "Settlement Date"
			 	ELSEIF document.getElementById("cmbLeaveType").value = 4 THEN
			 		document.getElementById("LblDate1").innerText = "Notice Date"
			 		document.getElementById("LblDate2").innerText = "Retirement Date"
			 		document.getElementById("LblDate3").innerText = "Settlement Date"	
			 	ELSEIF document.getElementById("cmbLeaveType").value = 5 THEN
			 		document.getElementById("LblDate1").innerText = ""
			 		document.getElementById("LblDate2").innerText = "Pay Holding Date"
			 		document.getElementById("LblDate3").innerText = ""		
			 	ELSEIF document.getElementById("cmbLeaveType").value = 6 THEN
			 		document.getElementById("LblDate1").innerText = ""
			 		document.getElementById("LblDate2").innerText = ""
			 		document.getElementById("LblDate3").innerText = ""
			 	ELSEIF document.getElementById("cmbLeaveType").value = 7 THEN
			 		document.getElementById("LblDate1").innerText = "Death Date"
			 		document.getElementById("LblDate2").innerText = "Releaving Date"
			 		document.getElementById("LblDate3").innerText = "Settlement Date"					
			 	ELSEIF document.getElementById("cmbLeaveType").value = 8 THEN
			 		document.getElementById("LblDate1").innerText = "Transfer Date"
			 		document.getElementById("LblDate2").innerText = "Releaving Date"
			 		document.getElementById("LblDate3").innerText = "Settlement Date"						
			 	END IF 				
			END SUB
			
		</script>
		<script language="vbscript">
			
			function ValidateCtrl()
				If Len(document.getElementById("TxtPANNO").Value) <> 10 Then
					MsgBox ("PAN No. must be of 10 Digits.")
					ValidateCtrl=False
					Exit function
				End If
				ValidateCtrl=True
			end function
			
			Function CheckPAN(argPAN)
				dim I, s1				
				argPAN = UCASE(TRIM(argPAN))				
				if argPAN = "PANAPPLIED" OR argPAN = "PANNOTAVBL" OR argPAN = "PANINVALID" then
					CheckPAN = TRUE
					exit function
				end if
				IF LEN(argPAN) <> 10 THEN
					CheckPAN = FALSE
					exit function
				END IF
				for i = 1 to 10
					s1 = mid(argPAN,i,1)
					if i <=5 or i = 10 then
						if ChkAlphabet(s1) = False then 
							CheckPAN = FALSE
							exit function
						end if
					else
						if ChkNumeric(s1) = False then 
							CheckPAN = FALSE
							exit function
						End if
					end if
				next
				CheckPAN = TRUE					
			End Function			
			
			Function ChkAlphabet(argC)
				argC = ucase(argC)				
				if argC >="A" and argC <="Z" then
					ChkAlphabet = TRUE
				Else
					ChkAlphabet = False
				end if				
			end Function
			
			Function ChkNumeric(argC)
				if isNumeric(argC) then
					ChkNumeric = True
				Else
					ChkNumeric = False
				end if				
			end Function
			
			Sub ChkPAN()
				dim PANNo
				document.getElementById("TxtPANNo").value = ucase(document.getElementById("TxtPANNo").value)
				PANNo = document.getElementById("TxtPANNo").value
				if trim(PANNO) = "" then 
					document.getElementById("TxtPANNo").value = "PANNOTAVBL" 
					exit sub	
				Else					
					if CheckPAN(PANNo) = False then
						Msgbox "Please enter valid PAN No. ex. ABCDE1234F", 64 ,"Invalid PAN No. !"
						document.getElementById("TxtPANNo").focus
					end if
				End if
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
								<td width="10" bgColor="#666666"><IMG alt="" src="Images/SplitLeft.gif" border="0"></td>
								<td style="COLOR: white" align="center" bgColor="#666666"><b>Others</b></td>
								<td width="10" bgColor="#666666"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
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
						<table borderColor="#000000" cellSpacing="0" cellPadding="0" rules="none" width="100%"
							border="1" frame="border">
							<tr>
								<td width="20%"></td>
								<td width="30%"></td>
								<td width="20%"></td>
								<td width="30%"></td>
							</tr>
							<tr>
								<td colSpan="4"><asp:label id="LblErrMsg" Runat="server" ForeColor="Red" Width="100%"></asp:label></td>
							</tr>
							<tr>
								<td>&nbsp;Code</td>
								<td><asp:textbox id="txtEM_CD" Runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox"
										BackColor="#E1E4EB" Font-Bold="True" AutoPostBack="True"></asp:textbox></td>
								<td align="center" colSpan="2"><asp:label id="LblName" runat="server" ForeColor="#003366" Width="100%" Font-Bold="True" Font-Size="9"></asp:label></td>
							</tr>
							<tr>
								<td>&nbsp;Pay Mode
								</td>
								<td><asp:dropdownlist id="cmbPayMode" runat="server" Width="100%"></asp:dropdownlist></td>
								<td>&nbsp;Contract End Date
								</td>
								<td><asp:checkbox id="ChkContactEnd" onclick="Val(this.id)" Runat="server" Checked="True"></asp:checkbox><cc1:dtpcombo id="DtpContactEnd" runat="server" Width="100%" ToolTip="Date of Birth" Height="20px"></cc1:dtpcombo></td>
							</tr>
							<tr>
								<td>&nbsp;S.B. A/C No.
								</td>
								<td><asp:textbox id="TxtBANKACNO" runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox"></asp:textbox></td>
								<td>&nbsp;Last Promotion Date
								</td>
								<td><INPUT id="ChkLPromo" onclick="Val(this.id)" type="checkbox" CHECKED name="ChkLPromo" runat="server">
									<cc1:dtpcombo id="DtpLPromo" runat="server" Width="100%" ToolTip="Date of Birth" Height="20px"></cc1:dtpcombo></td>
							</tr>
							<tr>
								<td>&nbsp;PF No.
								</td>
								<td><asp:textbox id="TxtPFNO" runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox"></asp:textbox></td>
								<td>&nbsp;Last Increment Date
								</td>
								<td><INPUT id="ChkLInc" onclick="Val(this.id)" type="checkbox" CHECKED name="ChkLInc" runat="server">
									<cc1:dtpcombo id="DtpLInc" runat="server" Width="100%" ToolTip="Date of Birth" Height="20px"></cc1:dtpcombo></td>
							</tr>
							<tr>
								<td>&nbsp;ESI No
								</td>
								<td><asp:textbox id="TxtESINO" runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox"></asp:textbox></td>
								<td>&nbsp;Last Appraisal Date
								</td>
								<td><INPUT id="ChkLAppraisal" onclick="Val(this.id)" type="checkbox" CHECKED name="ChkLAppraisal"
										runat="server">
									<cc1:dtpcombo id="DtpLAppraisal" runat="server" Width="100%" ToolTip="Date of Birth" Height="20px"></cc1:dtpcombo></td>
							</tr>
							<tr>
								<td>&nbsp;PAN No</td>
								<td><asp:textbox id="TxtPANNO" onblur="ChkPAN()" runat="server" ForeColor="#003366" Width="100%"
										CssClass="TextBox" MaxLength="10"></asp:textbox></td>
								<td>&nbsp;Transfer Date</td>
								<td><INPUT id="ChkGrpJoin" onclick="Val(this.id)" type="checkbox" CHECKED name="ChkGrpJoin"
										runat="server">
									<cc1:dtpcombo id="DtpGrpJoin" runat="server" Width="100%" ToolTip="Date of Birth" Height="20px"></cc1:dtpcombo></td>
							</tr>
							<tr>
								<td>&nbsp;TAN No
								</td>
								<td><asp:textbox id="TxtTANNO" runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox"
										MaxLength="10"></asp:textbox></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td>&nbsp;Job Profile
								</td>
								<td colSpan="3"><asp:textbox id="TxtJOBPROFILE" runat="server" Width="100%" TextMode="MultiLine" Rows="3"></asp:textbox></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="9">
						<table borderColor="#000000" cellSpacing="0" cellPadding="0" rules="none" width="100%"
							border="1" frame="border">
							<tr>
								<td class="Header3" background="Images\headstripe.jpg"><IMG id="imgSaperation" style="CURSOR: hand" onclick="ShowHide('Saperation')" src="Images\Minus.gif">&nbsp;<b>Separation</b></td>
							</tr>
							<tr id="trSaperation">
								<td>
									<table cellSpacing="0" cellPadding="0" rules="none" width="100%" border="1" frame="border">
										<tr>
											<td width="20%"></td>
											<td width="30%"></td>
											<td width="20%"></td>
											<td width="30%"></td>
										</tr>
										<tr>
											<td style="HEIGHT: 18px">Leaving Type
											</td>
											<td style="HEIGHT: 18px"><asp:dropdownlist id="cmbLeaveType" runat="server" Width="100%" AutoPostBack="True">
													<asp:ListItem Value="1" Selected="True">None</asp:ListItem>
													<asp:ListItem Value="2">Resigned</asp:ListItem>
													<asp:ListItem Value="3">Terminated</asp:ListItem>
													<asp:ListItem Value="4">Retired</asp:ListItem>
													<asp:ListItem Value="5">Payhold</asp:ListItem>
													<asp:ListItem Value="6">Separated</asp:ListItem>
													<asp:ListItem Value="7">Death</asp:ListItem>
													<asp:ListItem Value="8">Transfer</asp:ListItem>
													<asp:ListItem Value="9">Regularized</asp:ListItem>
												</asp:dropdownlist></td>
											<td style="HEIGHT: 18px"><asp:label id="LblDate1" runat="server" Width="100%"></asp:label></td>
											<td style="HEIGHT: 18px"><INPUT id="ChkNotice" onclick="Val(this.id)" type="checkbox" CHECKED name="ChkNotice" runat="server">
												<cc1:dtpcombo id="DtpNotice" runat="server" Width="100%" Height="20px"></cc1:dtpcombo></td>
										</tr>
										<tr>
											<td><asp:label id="LblDate2" runat="server" Width="100%"></asp:label></td>
											<td><INPUT id="ChkDOL" onclick="Val(this.id)" type="checkbox" CHECKED name="ChkDOL" runat="server">
												<cc1:dtpcombo id="DtpDOL" runat="server" Width="100%" Height="20px"></cc1:dtpcombo></td>
											<td><asp:label id="LblDate3" runat="server" Width="100%"></asp:label></td>
											<td><INPUT id="ChkSettle" onclick="Val(this.id)" type="checkbox" CHECKED name="ChkSettle" runat="server">
												<cc1:dtpcombo id="DtpSettle" runat="server" Width="100%" Height="20px"></cc1:dtpcombo></td>
										</tr>
										<tr id="trNewOrg">
											<td><asp:label id="LblNewOrg" runat="server" Width="100%">New Organisation</asp:label></td>
											<td colSpan="3"><asp:textbox id="TxtNewOrg" runat="server" Width="100%" CssClass="TextBox"></asp:textbox></td>
										</tr>
										<tr>
											<td>Reason
											</td>
											<td colSpan="3"><asp:textbox id="TxtLReason" runat="server" Width="100%" TextMode="MultiLine" Rows="3"></asp:textbox></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td align="right"><asp:button id="cmdSave" Runat="server" Width="75px" Text="Save"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:button id="cmdClose" Runat="server" Width="75px" Text="Close"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td align="center" colSpan="9">
						<asp:Label Font-Size="10" Font-Bold="True" ID="LblRights" Width="100%" Runat="server"></asp:Label></td>
				</tr>
			</table>
		</form>
		</B>
	</body>
</HTML>
