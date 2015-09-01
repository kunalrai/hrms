<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="EmpMast.aspx.vb" Inherits="eHRMS.Net.EmpMast"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Employee Master</title>
		<meta content="False" name="vs_snapToGrid">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
		<script language="javascript">

				function chks(str)
			{
				
				if(str==null)
				{
					return("");
				}
				else
				{
					return str;
				}
			}
			function movenext()
			{
				var EmpCode;
				EmpCode=document.getElementById("TxtEmpCode").value;
				//alert('tesu');
				var response=EmpMast.GetNextEmpRec(EmpCode)
				EmpCode=response.value;
				document.getElementById("TxtEmpCode").value=EmpCode;
				EmpMast.SetEmpCode(EmpCode);
				document.getElementById("txtEM_CD").value= document.getElementById("TxtEmpCode").value
				EmpMast.SetEmpCode(EmpCode);
				EmpMast.GetEmpRec(CallBack);
			}
			function moveprevious()
			{
				var EmpCode;
				EmpCode=document.getElementById("TxtEmpCode").value;
				//alert('tesu');
				var response=EmpMast.GetPreviousEmpRec(EmpCode)
				EmpCode=response.value;
				document.getElementById("TxtEmpCode").value=EmpCode;
				EmpMast.SetEmpCode(EmpCode);
				document.getElementById("txtEM_CD").value= document.getElementById("TxtEmpCode").value
				EmpMast.SetEmpCode(EmpCode);
				EmpMast.GetEmpRec(CallBack);
			}
			function movefirst()
			{
				var EmpCode;
				EmpCode=document.getElementById("TxtEmpCode").value;
				//alert('tesu');
				var response=EmpMast.GetFirstEmpRec()
				EmpCode=response.value;
				document.getElementById("TxtEmpCode").value=EmpCode;
				EmpMast.SetEmpCode(EmpCode);
				document.getElementById("txtEM_CD").value= document.getElementById("TxtEmpCode").value
				EmpMast.SetEmpCode(EmpCode);
				EmpMast.GetEmpRec(CallBack);
			}
			function movelast()
			{
				var EmpCode;
				EmpCode=document.getElementById("TxtEmpCode").value;
				//alert('tesu');
				var response=EmpMast.GetLastEmpRec()
				EmpCode=response.value;
				document.getElementById("TxtEmpCode").value=EmpCode;
				EmpMast.SetEmpCode(EmpCode);
				document.getElementById("txtEM_CD").value= document.getElementById("TxtEmpCode").value
				EmpMast.SetEmpCode(EmpCode);
				EmpMast.GetEmpRec(CallBack);
			}

			function Disp()
			{
				var EmpCode;
				EmpCode=document.getElementById("TxtEmpCode").value;
				document.getElementById("txtEM_CD").value= document.getElementById("TxtEmpCode").value
				EmpMast.SetEmpCode(EmpCode);
				EmpMast.GetEmpRec(CallBack);
			}

			function CallBack(response)
				{
					var sText;
					
					var response=EmpMast.GetEmpRec(document.getElementById("TxtEmpCode").value);
					var dt = response.value;
					
					if(dt != null && typeof(dt) == "object")
					{				
						sText = response.request.responseText;
						var a;
						
						var fi = sText.indexOf("'Rows':[")+8;
						var si = sText.indexOf("}",fi);
						var sRow = sText.slice(fi+1,si);
						aRow = sRow.split(",");
						for(var i=0; i<aRow.length; i++)
							{
							aRow[i]=aRow[i].slice(1,aRow[i].indexOf("'",2));
							a= a + aRow[i];
							}
						if (aRow.length > 1)
						{
												
						  document.getElementById("txtFName").value= chks(dt.Tables[0].Rows[0]['FNAME']);
						  document.getElementById("cmbDepartment").value= dt.Tables[0].Rows[0]['DEPT_CODE'];
						  document.getElementById("cmbDesignation").value= dt.Tables[0].Rows[0]['DSG_CODE'];
						  document.getElementById("cmbLocation").value= dt.Tables[0].Rows[0]['LOC_CODE'];
						  document.getElementById("cmbALocation").value= dt.Tables[0].Rows[0]['ADMINLOC_CODE'];
						  document.getElementById("cmbPLocation").value= dt.Tables[0].Rows[0]['PAY_CODE'];
						  document.getElementById("cmbEmpType").value= dt.Tables[0].Rows[0]['TYPE_CODE'];
						  document.getElementById("CmbJobName").value= dt.Tables[0].Rows[0]['JOB_CODE'];
						  document.getElementById("CmbEmpClass").value= dt.Tables[0].Rows[0]['EMP_CLASS'];
						  document.getElementById("CmbHrMngr").value= dt.Tables[0].Rows[0]['HR_MNGR'];
						  document.getElementById("cmbManager").value= dt.Tables[0].Rows[0]['MNGR_CODE'];
						  document.getElementById("txtLName").value= chks(dt.Tables[0].Rows[0]['LNAME']);
						  document.getElementById("cmbGrade").value= dt.Tables[0].Rows[0]['GRD_CODE'];
						  document.getElementById("cmbCostCenter").value= dt.Tables[0].Rows[0]['COST_CODE'];
						  document.getElementById("cmbProcess").value= dt.Tables[0].Rows[0]['PROC_CODE'];
						  document.getElementById("cmbRegion").value= dt.Tables[0].Rows[0]['REGION_CODE'];
						  document.getElementById("cmbSection").value= dt.Tables[0].Rows[0]['SECT_CODE'];
						  document.getElementById("cmbDivision").value= dt.Tables[0].Rows[0]['DIVI_CODE'];
						  document.getElementById("CmbFull").value= dt.Tables[0].Rows[0]['FULL_PART'];
						  document.getElementById("CmbCosting").value= dt.Tables[0].Rows[0]['COSTING'];
						  document.getElementById("CmbUnit").value= dt.Tables[0].Rows[0]['UNIT_CODE'];
						  document.getElementById("CmbContType").value= dt.Tables[0].Rows[0]['CONT_TYPE'];
						  document.getElementById("TxtComp").value= chks(dt.Tables[0].Rows[0]['COMPANY']);
						  document.getElementById("TxtSalAdminPlan").value= chks(dt.Tables[0].Rows[0]['LSA_PLAN']);
						  document.getElementById("TxtJobRanking").value= chks(dt.Tables[0].Rows[0]['JOB_RANK']);
						  document.getElementById("TxtBTitle").value= chks(dt.Tables[0].Rows[0]['BUSS_TITLE']);
						  if(dt.Tables[0].Rows[0]['DOJ']!=null)
						  {
						  document.getElementById("dtpDOJcmbDD").value= dt.Tables[0].Rows[0]['DOJ'].getDate();
						  document.getElementById("dtpDOJcmbYY").value= dt.Tables[0].Rows[0]['DOJ'].getYear();						  
						// var mon= new Array("Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec");
						 document.getElementById("dtpDOJcmbMM").value= dt.Tables[0].Rows[0]['DOJ'].getMonth()+1;	
						 //dt.Tables[0].Rows[0]['DOJ'].getMonth()	
						 }
						 else
						 {				  
						  document.getElementById("dtpDOJcmbDD").value= '01';
						  document.getElementById("dtpDOJcmbYY").value= '1900';	
						  document.getElementById("dtpDOJcmbMM").value= 'jan';						  
						  }						  
						  }
						  
						else
						{
						  
						  document.getElementById("txtFName").value= "";
						  document.getElementById("cmbDepartment").value= "";
						  document.getElementById("cmbDesignation").value= "";
						  document.getElementById("cmbLocation").value= "";
						  document.getElementById("cmbALocation").value= "";
						  document.getElementById("cmbPLocation").value= "";
						  document.getElementById("cmbEmpType").value= "";
						  document.getElementById("CmbJobName").value= "";
						  document.getElementById("CmbEmpClass").value= "";
						  document.getElementById("CmbHrMngr").value= "";
						  document.getElementById("cmbManager").value= "";
						  document.getElementById("txtLName").value= "";
						  document.getElementById("cmbGrade").value= "";
						  document.getElementById("cmbCostCenter").value= "";
						  document.getElementById("cmbProcess").value= "";
						  document.getElementById("cmbRegion").value= "";
						  document.getElementById("cmbSection").value= "";
						  document.getElementById("cmbDivision").value= "";
						  document.getElementById("CmbFull").value= "";
						  document.getElementById("CmbCosting").value= "";
						  document.getElementById("CmbUnit").value= "";
						  document.getElementById("CmbContType").value= "";
						  document.getElementById("TxtComp").value= "";
						  document.getElementById("TxtSalAdminPlan").value= "";
						  document.getElementById("TxtJobRanking").value= "";
						  document.getElementById("TxtBTitle").value= "";
						  alert('Either your Employee Code is wrong or Employee Code field is blank.');
						
					    }
					       			    
					    
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
			
			function ValidateCtrl()
			
				if Trim(document.getElementById("txtFName").Value) = "" Then
					msgbox("Please Enter First Name of Employee.")
					ValidateCtrl = false
					exit function
				end if
					
				if isnumeric(document.getElementById("txtFName").Value) Then
					msgbox("First Name must be string type.")
					ValidateCtrl = false
					exit function
				end if

				if IsNumeric(document.getElementById("txtLName").Value) Then
					msgbox("Last Name must be string type.")
					ValidateCtrl = false
					exit function
				end if

				
				dim i,j,k
				dim dtDOCE, dtDOJ, dtDOCDUE, DtDDEPP, DtCEUPTO, DtDDR
				
				dtDOJ = cdate(document.getElementById("dtpDOJcmbDD").Value & "/" & document.getElementById("dtpDOJcmbMM").Value & "/" & document.getElementById("dtpDOJcmbYY").Value)
				dtDOCE = cdate(document.getElementById("dtpDOCEcmbDD").Value & "/" & document.getElementById("dtpDOCEcmbMM").Value & "/" & document.getElementById("dtpDOCEcmbYY").Value)
				dtDOCDUE = cdate(document.getElementById("dtpDOCDUEcmbDD").Value & "/" & document.getElementById("dtpDOCDUEcmbMM").Value & "/" & document.getElementById("dtpDOCDUEcmbYY").Value)
				DtDDEPP = cdate(document.getElementById("DtpDDEPPcmbDD").Value & "/" & document.getElementById("DtpDDEPPcmbMM").Value & "/" & document.getElementById("DtpDDEPPcmbYY").Value)
				DtCEUPTO = cdate(document.getElementById("DtpCEUPTOcmbDD").Value & "/" & document.getElementById("DtpCEUPTOcmbMM").Value & "/" & document.getElementById("DtpCEUPTOcmbYY").Value)
				DtDDR = cdate(document.getElementById("DtpDDRcmbDD").Value & "/" & document.getElementById("DtpDDRcmbMM").Value & "/" & document.getElementById("DtpDDRcmbYY").Value)
				
				if Not IsDate(dtDOJ) Then
					msgbox("Invalid Date of Joining.")
					ValidateCtrl = false
					exit function
				end if
								
				if document.getElementById("ChkDOCE").Checked = True Then
					i = datediff("d",dtDOCE,dtDOJ)
					if i > 0 Then
						msgbox("Contract End Date Can't Before the Date of Joining.")
						ValidateCtrl = false
						exit function
					end if
				end if
				if document.getElementById("ChkDOCDue").Checked = True Then
					j = datediff("d",dtDOCDUE,dtDOJ)
					if j > 0 Then
						msgbox("Due Date of Confirmation Can't Before the Date of Joining.")
						ValidateCtrl = false
						exit function
					end if
				end if
				if document.getElementById("ChkDDEPP").Checked = True Then
					k = datediff("d",dtDDEPP,dtDOJ)
					if k > 0 Then
						msgbox("Probation Period Extended Upto Can't Before the Date of Joining.")
						ValidateCtrl = false
						exit function
					end if
				end if
				if document.getElementById("ChkCEUPTO").Checked = True Then
					k = datediff("d",dtCEUPTO,dtDOJ)
					if k > 0 Then
						msgbox("Contract Extended Upto Can't Before the Date of Joining.")
						ValidateCtrl = false
						exit function
					end if
				end if
				if document.getElementById("ChkDDR").Checked = True Then
					k = datediff("d",dtDDR,dtDOJ)
					if k > 0 Then
						msgbox("Date of Regularisation Can't Before the Date of Joining.")
						ValidateCtrl = false
						exit function
					end if
				end if	
				ValidateCtrl = true
			end function
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
								<td width="10" bgColor="#666666"><IMG alt="" src="Images/SplitLeft.gif" border="0"></td>
								<td style="COLOR: white" align="center" bgColor="#666666"><b>Official Info</b></td>
								<td width="10" bgColor="#666666"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
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
								<td colSpan="2"><asp:label id="LblMsg" runat="server" Font-Size="11px" ForeColor="Red" Width="100%"></asp:label></td>
							</tr>
							<tr>
								<td style="WIDTH: 444px" width="444">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td width="40%"><asp:label id="lblCode" runat="server" Width="100%">&nbsp;Code</asp:label></td>
											<td align="left" width="60%"><INPUT style="FONT-WEIGHT: bold; COLOR: #0033ff; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: #ffffff; BORDER-BOTTOM-STYLE: none"
													onclick="movefirst();" type="button" value="<<"><INPUT style="FONT-WEIGHT: bold; WIDTH: 22px; COLOR: #0033ff; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: #ffffff; BORDER-BOTTOM-STYLE: none"
													type="button" value="<" onclick="moveprevious();">&nbsp; <INPUT class="TextBox" id="TxtEmpCode" onblur="Disp();" style="WIDTH: 134px; HEIGHT: 20px"
													type="text" size="17" name="Text1" runat="server">&nbsp; <INPUT style="FONT-WEIGHT: bold; COLOR: #0033ff; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: #ffffff; BORDER-BOTTOM-STYLE: none"
													onclick="movenext();" type="button" value=">">
												<asp:textbox id="txtEM_CD" runat="server" ForeColor="#003366" Width="0px" AutoPostBack="True"
													CssClass="TextBox" BackColor="#E1E4EB" Font-Bold="True"></asp:textbox><INPUT style="FONT-WEIGHT: bold; COLOR: #0033ff; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: #ffffff; BORDER-BOTTOM-STYLE: none"
													onclick="movelast();" type="button" value=">>"></td>
										</tr>
									</table>
								</td>
								<td vAlign="top" width="50%"></td>
							</tr>
							<tr>
								<td style="WIDTH: 444px; HEIGHT: 423px" width="444">
									<table cellSpacing="0" cellPadding="2" width="100%" border="0">
										<TBODY>
											<tr>
												<td width="40%"><asp:label id="lblFName" runat="server" Width="100%">First Name</asp:label></td>
												<td width="60%"><asp:textbox id="txtFName" tabIndex="1" runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox"></asp:textbox></td>
											</tr>
											<tr>
												<td style="HEIGHT: 21px" width="40%"><asp:label id="lblDesignation" runat="server" Width="100%">Designation</asp:label></td>
												<td style="HEIGHT: 21px" width="60%"><asp:dropdownlist id="cmbDesignation" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td><asp:label id="lblDepartment" runat="server" Width="100%">Department</asp:label></td>
												<td><asp:dropdownlist id="cmbDepartment" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td><asp:label id="lblLocation" runat="server" Width="100%">Work Location</asp:label></td>
												<td><asp:dropdownlist id="cmbLocation" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td style="HEIGHT: 7px"><asp:label id="lblALocation" runat="server" Width="100%">Admin Location</asp:label></td>
												<td style="HEIGHT: 7px"><asp:dropdownlist id="cmbALocation" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td style="HEIGHT: 21px"><asp:label id="lblPLocation" runat="server" Width="100%">Pay Location</asp:label></td>
												<td style="HEIGHT: 21px"><asp:dropdownlist id="cmbPLocation" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td><asp:label id="lblEmpType" runat="server" Width="100%">Regular/Temporary</asp:label></td>
												<td><asp:dropdownlist id="cmbEmpType" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td style="HEIGHT: 25px"><asp:label id="LblJOBCode" runat="server" Width="100%">Job Code</asp:label></td>
												<td style="HEIGHT: 25px"><asp:dropdownlist id="CmbJobName" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td><asp:label id="LblSup" runat="server" Width="100%">Employee Class</asp:label></td>
												<td><asp:dropdownlist id="CmbEmpClass" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td><asp:label id="LblHRMANAGER" runat="server" Width="100%">HR Manager</asp:label></td>
												<td><asp:dropdownlist id="CmbHrMngr" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td><asp:label id="lblManager" runat="server" Width="100%">Line/Supervisor ID</asp:label></td>
												<td><asp:dropdownlist id="cmbManager" runat="server" Width="100%"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td><asp:label id="LblComp" runat="server" Width="100%">Company</asp:label></td>
												<td><asp:textbox id="TxtComp" ForeColor="#003366" Width="100%" CssClass="TextBox" Runat="server"></asp:textbox></td>
											</tr>
											<tr>
												<td width="40%"><asp:label id="LblLocalSal" runat="server" Width="100%">Local Salary Admin Plan.</asp:label></td>
												<td width="60%"><asp:textbox id="TxtSalAdminPlan" runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox"></asp:textbox></td>
											</tr>
											<tr>
												<td><asp:label id="LblDOC" runat="server" Width="100%" ToolTip=" Date of Confirmation">Confirmed (Yes/No)</asp:label></td>
												<td><input id="ChkDOC" type="checkbox" name="ChkDOC" runat="server"><cc1:dtpcombo id="DtpDOC" runat="server" Width="150px" ToolTip="Date of Confirmation" visible="FAlse"
														Enabled="False" DateValue="2005-08-30"></cc1:dtpcombo></td>
											</tr>
											<tr>
												<td><asp:label id="LblBTEDate" runat="server" Width="100%"> Service Date</asp:label></td>
												<!--<td><input id="ChkEntry" onclick="Val(this.id)" type="Checkbox" name="ChkENTRY" runat="server"><cc1:dtpcombo id="DtpEDate" runat="server" Width="150px" ToolTip="Business Title Entry Date "
														Enabled="False" DateValue="2005-08-30"></cc1:dtpcombo></td>-->
												<td><input id="ChkDDEDPE" onclick="Val(this.id)" type="checkbox" name="ChkDDEDPE" runat="server">
													<cc1:dtpcombo id="DtpDDEDPE" runat="server" Width="150px" ToolTip="Service Date" Enabled="False"
														DateValue="2005-08-30"></cc1:dtpcombo></td>
											</tr>
											<tr>
												<td><asp:label id="lblDOCDue" runat="server" Width="100%" ToolTip=" Due Date of Confirmation">Due Date of Confirmation</asp:label></td>
												<td><input id="ChkDOCDue" onclick="Val(this.id)" type="checkbox" name="ChkDOCDue" runat="server"><cc1:dtpcombo id="dtpDOCDUE" runat="server" Width="150px" ToolTip="Due Date of Confirmation" Enabled="False"
														DateValue="2005-08-30"></cc1:dtpcombo></td>
											</tr>
											<tr>
												<td><asp:label id="Label2" runat="server" Width="100%">Probation Period Extended Upto</asp:label></td>
												<td><input id="ChkDDEPP" onclick="Val(this.id)" type="checkbox" name="ChkDDEPP" runat="server"><cc1:dtpcombo id="DtpDDEPP" runat="server" Width="150px" ToolTip="Due Date of Extension of Probation Period"
														Enabled="False" DateValue="2005-08-30"></cc1:dtpcombo></td>
											</tr>
										</TBODY>
									</table>
								</td>
								<td width="50%" style="HEIGHT: 423px">
									<table cellSpacing="0" cellPadding="2" width="100%" border="0">
										<tr>
											<td style="HEIGHT: 26px" width="40%"><asp:label id="lblLName" runat="server" Width="100%">Last Name</asp:label></td>
											<td style="HEIGHT: 26px" width="60%"><asp:textbox id="txtLName" tabIndex="2" runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox"></asp:textbox></td>
										</tr>
										<tr>
											<td style="HEIGHT: 5px" width="40%"><asp:label id="lblGrade" runat="server" Width="100%">Grade / Level</asp:label></td>
											<td style="HEIGHT: 5px" width="60%"><asp:dropdownlist id="cmbGrade" runat="server" Width="100%"></asp:dropdownlist></td>
										</tr>
										<tr>
											<td style="HEIGHT: 7px"><asp:label id="lblCostCenter" runat="server" Width="100%">Cost Center</asp:label></td>
											<td style="HEIGHT: 7px"><asp:dropdownlist id="cmbCostCenter" runat="server" Width="100%"></asp:dropdownlist></td>
										</tr>
										<tr>
											<td style="HEIGHT: 7px"><asp:label id="lblProcess" runat="server" Width="100%"> Sub-Department</asp:label></td>
											<td style="HEIGHT: 7px"><asp:dropdownlist id="cmbProcess" runat="server" Width="100%"></asp:dropdownlist></td>
										</tr>
										<tr>
											<td><asp:label id="lblRegion" runat="server" Width="100%">Region</asp:label></td>
											<td><asp:dropdownlist id="cmbRegion" runat="server" Width="100%"></asp:dropdownlist></td>
										</tr>
										<tr>
											<td style="HEIGHT: 23px"><asp:label id="lblSection" runat="server" Width="100%">Section</asp:label></td>
											<td style="HEIGHT: 23px"><asp:dropdownlist id="cmbSection" runat="server" Width="100%"></asp:dropdownlist></td>
										</tr>
										<tr>
											<td><asp:label id="lblDivision" runat="server" Width="100%"> Division</asp:label></td>
											<td><asp:dropdownlist id="cmbDivision" runat="server" Width="100%"></asp:dropdownlist></td>
										</tr>
										<tr>
											<td><asp:label id="LblFull" runat="server" Width="100%">Full/Part</asp:label></td>
											<td><asp:dropdownlist id="CmbFull" runat="server" Width="100%">
													<asp:ListItem Selected="True" Value="1">Full-Time</asp:ListItem>
													<asp:ListItem Value="2">Part-Time</asp:ListItem>
												</asp:dropdownlist></td>
										</tr>
										<tr>
											<td><asp:label id="LblCost" runat="server" Width="100%">Costing</asp:label></td>
											<td><asp:dropdownlist id="CmbCosting" runat="server" Width="100%"></asp:dropdownlist></td>
										</tr>
										<tr>
											<td><asp:label id="LblBUnit" runat="server" Width="100%">Business Unit</asp:label></td>
											<td><asp:dropdownlist id="CmbUnit" runat="server" Width="100%"></asp:dropdownlist></td>
										</tr>
										<tr>
											<td><asp:label id="LblCont" runat="server" Width="100%">Contract Type</asp:label></td>
											<td><asp:dropdownlist id="CmbContType" runat="server" Width="100%"></asp:dropdownlist></td>
										</tr>
										<tr>
											<td width="40%"><asp:label id="LblJobRank" runat="server" Width="100%">Job Ranking</asp:label></td>
											<td width="60%"><asp:textbox id="TxtJobRanking" runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox"></asp:textbox></td>
										</tr>
										<tr>
											<td width="40%"><asp:label id="LblBTitle" runat="server" Width="100%">Business Title</asp:label></td>
											<td width="60%"><asp:textbox id="TxtBTitle" runat="server" ForeColor="#003366" Width="100%" CssClass="TextBox"></asp:textbox></td>
										</tr>
										<tr>
											<td><asp:label id="lblDOJ" runat="server" Width="100%">Date of Joining</asp:label></td>
											<td><cc1:dtpcombo id="dtpDOJ" runat="server" Width="150px" ToolTip="Date Of Joining" DateValue="2005-08-30"></cc1:dtpcombo></td>
										</tr>
										<tr>
											<td><asp:label id="Label1" runat="server" Width="100%">Contract End Date</asp:label></td>
											<td><input id="ChkDOCE" onclick="Val(this.id)" type="checkbox" name="ChkDOCE" runat="server"><cc1:dtpcombo id="dtpDOCE" runat="server" Width="150px" ToolTip="Contract End Date" Enabled="False"
													DateValue="2005-08-30"></cc1:dtpcombo></td>
										</tr>
										<tr>
											<td><asp:label id="LblCEUPTO" runat="server" Width="100%">Contract Extended Upto</asp:label></td>
											<td><input id="ChkCEUPTO" onclick="Val(this.id)" type="checkbox" name="ChkCEUPTO" runat="server"><cc1:dtpcombo id="DtpCEUPTO" runat="server" Width="150px" ToolTip="Contract Extended Upto" Enabled="False"
													DateValue="2005-08-30"></cc1:dtpcombo></td>
										</tr>
										<tr>
											<td><asp:label id="Label3" runat="server" Width="100%">Date of Regularisation</asp:label></td>
											<td><input id="ChkDDR" onclick="Val(this.id)" type="checkbox" name="ChkDDR" runat="server"><cc1:dtpcombo id="DtpDDR" runat="server" Width="150px" ToolTip="Contract Extended Upto" Enabled="False"
													DateValue="2005-08-30"></cc1:dtpcombo></td>
										</tr>
										<!--<tr>
											<td><asp:label id="Label5" runat="server" Width="100%" Visible =False >Due Date of Extension of Training Period</asp:label></td>
											<td><input id="ChkDDETP" onclick="Val(this.id)" type="checkbox" name="ChkDDETP" runat="server"><cc1:dtpcombo id="DtpDDETP" runat="server" Width="150px" ToolTip="Due Date of Extension of Training Period"
													Enabled="False"  visible="False" DateValue="2005-08-30"></cc1:dtpcombo></td>
										</tr>--></table>
								</td>
							</tr>
							<TR height="15">
								<TD style="MARGIN-LEFT: 15px; MARGIN-RIGHT: 15px; BORDER-BOTTOM: #993300 thin groove; HEIGHT: 7px"
									colSpan="2"></TD>
							</TR>
							<tr>
								<td align="right" colSpan="2"><asp:button id="cmdSave" accessKey="S" runat="server" Width="75px" Text="Save"></asp:button>&nbsp;
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
		<script language="javascript">
			var response=EmpMast.SetCurrentEmpCode();
			var dt = response.value;
			if (dt != null)
			{
				document.getElementById("TxtEmpCode").value = dt;
				document.getElementById("TxtEmpCode").blur();
			}
			
		</script>
		</TR></TBODY></TABLE>
	</body>
</HTML>
