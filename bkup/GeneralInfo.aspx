<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="GeneralInfo.aspx.vb" Inherits="eHRMS.Net.GeneralInfo"%>
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
		<script language="javascript">
			/*function ShowDet()
				{
					GeneralInfo.DisplayRecord();
				}
			function CallBack(response)
				{
					alert("completed");
				 //'alert(response.value);	
				 			 
				}
			
			*/	
			
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
				var response=GeneralInfo.GetNextEmpRec(EmpCode)
				EmpCode=response.value;
				document.getElementById("TxtEmpCode").value=EmpCode;
				GeneralInfo.SetEmpCode(EmpCode);
				document.getElementById("txtEM_CD").value= document.getElementById("TxtEmpCode").value
				GeneralInfo.SetEmpCode(EmpCode);
				GeneralInfo.GetEmpRec(CallBack);
			}
			function moveprevious()
			{
				var EmpCode;
				EmpCode=document.getElementById("TxtEmpCode").value;
				//alert('tesu');
				var response=GeneralInfo.GetPreviousEmpRec(EmpCode)
				EmpCode=response.value;
				document.getElementById("TxtEmpCode").value=EmpCode;
				GeneralInfo.SetEmpCode(EmpCode);
				document.getElementById("txtEM_CD").value= document.getElementById("TxtEmpCode").value
				GeneralInfo.SetEmpCode(EmpCode);
				GeneralInfo.GetEmpRec(CallBack);
			}
			function movefirst()
			{
				var EmpCode;
				EmpCode=document.getElementById("TxtEmpCode").value;
				//alert('tesu');
				var response=GeneralInfo.GetFirstEmpRec()
				EmpCode=response.value;
				document.getElementById("TxtEmpCode").value=EmpCode;
				GeneralInfo.SetEmpCode(EmpCode);
				document.getElementById("txtEM_CD").value= document.getElementById("TxtEmpCode").value
				GeneralInfo.SetEmpCode(EmpCode);
				GeneralInfo.GetEmpRec(CallBack);
			}
			function movelast()
			{
				var EmpCode;
				EmpCode=document.getElementById("TxtEmpCode").value;
				//alert('tesu');
				var response=GeneralInfo.GetLastEmpRec()
				EmpCode=response.value;
				document.getElementById("TxtEmpCode").value=EmpCode;
				GeneralInfo.SetEmpCode(EmpCode);
				document.getElementById("txtEM_CD").value= document.getElementById("TxtEmpCode").value
				GeneralInfo.SetEmpCode(EmpCode);
				GeneralInfo.GetEmpRec(CallBack);
			}
			function Disp()
			{
				
				var EmpCode;
				EmpCode=document.getElementById("TxtEmpCode").value;
				
				document.getElementById("txtEM_CD").value= document.getElementById("TxtEmpCode").value
				GeneralInfo.SetEmpCode(EmpCode);
				GeneralInfo.GetEmpRec(CallBack);
			}
				function CallBack(response)
				{
					var sText;
					//alert('test');
					var response=GeneralInfo.GetEmpRec(document.getElementById("TxtEmpCode").value);
					var dt = response.value;
					
					if(dt != null && typeof(dt) == "object")
					{				
						sText = response.request.responseText;
						var a;
						//alert('ooo');
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
							//alert('hi0');
						  document.getElementById("txtFName").value= chks(dt.Tables[0].Rows[0]['FNAME']);
						  document.getElementById("txtMName").value= chks(dt.Tables[0].Rows[0]['MNAME']);
						  document.getElementById("txtLName").value= chks(dt.Tables[0].Rows[0]['LNAME']);
						  /* address */
						  document.getElementById("txtMAddr1").value= chks(dt.Tables[0].Rows[0]['MADDR1']);
						  document.getElementById("txtMAddr2").value= chks(dt.Tables[0].Rows[0]['MADDR2']);
						  document.getElementById("txtMAddr3").value= chks(dt.Tables[0].Rows[0]['MADDR3']);
						  document.getElementById("txtMCity").value= chks(dt.Tables[0].Rows[0]['MCITY']);
						  document.getElementById("txtMState").value= chks(dt.Tables[0].Rows[0]['MSTATE']);
						  document.getElementById("txtMCountry").value= chks(dt.Tables[0].Rows[0]['MCOUNTRY']);
						  document.getElementById("txtMPIN").value= chks(dt.Tables[0].Rows[0]['MPIN']);
						  document.getElementById("txtMPhone").value= chks(dt.Tables[0].Rows[0]['MPHONE']);					  
						  document.getElementById("txtPAddr1").value= chks(dt.Tables[0].Rows[0]['PADDR1']);
						  document.getElementById("txtMAddr2").value= chks(dt.Tables[0].Rows[0]['PADDR2']);
						  document.getElementById("txtMAddr3").value= chks(dt.Tables[0].Rows[0]['PADDR3']);
						  document.getElementById("txtMCity").value= chks(dt.Tables[0].Rows[0]['PCITY']);
						  document.getElementById("txtMState").value= chks(dt.Tables[0].Rows[0]['PSTATE']);
						  document.getElementById("txtMCountry").value= chks(dt.Tables[0].Rows[0]['PCOUNTRY']);
						  document.getElementById("txtMPIN").value= chks(dt.Tables[0].Rows[0]['PPIN']);
						  document.getElementById("txtMPhone").value= chks(dt.Tables[0].Rows[0]['PPHONE']);
						  /*other informatiom */					  
						  
						 if(dt.Tables[0].Rows[0]['SEX']==2) document.getElementById("optFemale").checked=true;
						 if(dt.Tables[0].Rows[0]['SEX']==1) document.getElementById("optMale").checked=true;
						 document.getElementById("CmbMStatus").value= dt.Tables[0].Rows[0]['MSTATUS'];
						 if(dt.Tables[0].Rows[0]['DOB']!=null)	
						 {
						 document.getElementById("dtpDOBcmbDD").DateValue= dt.Tables[0].Rows[0]['DOB'].getDate();
						 document.getElementById("dtpDOBcmbMM").DateValue= dt.Tables[0].Rows[0]['DOB'].getMonth()+1;	
						 document.getElementById("dtpDOBcmbYY").DateValue= dt.Tables[0].Rows[0]['DOB'].getYear();						  
						 }
						  document.getElementById("txtFathHusbName").value= chks(dt.Tables[0].Rows[0]['FathHusbName']);
						  document.getElementById("txtDomicile").value= chks(dt.Tables[0].Rows[0]['DOMICILE']);
						  document.getElementById("txtNationality").value= chks(dt.Tables[0].Rows[0]['Nationality']);
						  document.getElementById("txtBirthPlace").value= chks(dt.Tables[0].Rows[0]['BirthPlace']);
						  if(dt.Tables[0].Rows[0]['DOM']!=null)	
						  {
							document.getElementById("DtpDOMcmbDD").DateValue= dt.Tables[0].Rows[0]['DOM'].getDate();
							document.getElementById("DtpDOMcmbMM").DateValue= dt.Tables[0].Rows[0]['DOM'].getMonth()+1;	
							document.getElementById("DtpDOMcmbYY").DateValue= dt.Tables[0].Rows[0]['DOM'].getYear();
						  }	
						 				 
						  					  
						  document.getElementById("CmbBGroup").value= dt.Tables[0].Rows[0]['BLOODGRP'];
						  document.getElementById("cmbFood").value= dt.Tables[0].Rows[0]['FoodChoice'];						  
						  document.getElementById("cmbReligion").value= dt.Tables[0].Rows[0]['Religion'];
						  document.getElementById("txtPassportNo").value= chks(dt.Tables[0].Rows[0]['PASSPORTNO']);
						  if(dt.Tables[0].Rows[0]['DOV']!=null)	
						  {
						  document.getElementById("dtpPassportExpcmbDD").DateValue= chks(dt.Tables[0].Rows[0]['DOV'].getDate());
						 document.getElementById("dtpPassportExpcmbMM").DateValue= chks(dt.Tables[0].Rows[0]['DOV'].getMonth()+1);	
						 document.getElementById("dtpPassportExpcmbYY").DateValue= chks(dt.Tables[0].Rows[0]['DOV'].getYear());
						}
						  document.getElementById("txtDLNo").value= chks(dt.Tables[0].Rows[0]['DLNO']);
						  document.getElementById("txtEmailID").value= chks(dt.Tables[0].Rows[0]['EmailId']);
						  document.getElementById("txtPEmailId").value= chks(dt.Tables[0].Rows[0]['PEmailId']);
						  document.getElementById("TxtFathHusbOcupation").value= chks(dt.Tables[0].Rows[0]['FathHusbOcupation']);
						  document.getElementById("txtHobbies").value= chks(dt.Tables[0].Rows[0]['Hobbies']);
						  /*emergency contact */
						  document.getElementById("txtEmergencyName").value= chks(dt.Tables[0].Rows[0]['EmergencyName']);
						  document.getElementById("txtEmergencyAddress").value= chks(dt.Tables[0].Rows[0]['EmergencyAddress']);
						  document.getElementById("txtEmergencyPhoneNo").value= chks(dt.Tables[0].Rows[0]['EmergencyPhoneNo']);
						  document.getElementById("txtDrName").value= chks(dt.Tables[0].Rows[0]['DrName']);
						  document.getElementById("txtDrAddress").value= chks(dt.Tables[0].Rows[0]['DrAddress']);
						  document.getElementById("txtDrPhoneNo").value= chks(dt.Tables[0].Rows[0]['DrPhoneNo']);
						  						  
						  }
						  else 
						  {
						  
						  document.getElementById("TxtEmpCode").value= "";
						  document.getElementById("txtFName").value= "";
						  document.getElementById("txtMName").value= "";
						  document.getElementById("txtLName").value= "";
						  /* address */
						  document.getElementById("txtMAddr1").value= "";
						  document.getElementById("txtMAddr2").value= "";
						  document.getElementById("txtMAddr3").value= "";
						  document.getElementById("txtMCity").value= "";
						  document.getElementById("txtMState").value= "";
						  document.getElementById("txtMCountry").value= "";
						  document.getElementById("txtMPIN").value= "";
						  document.getElementById("txtMPhone").value= "";
						  document.getElementById("txtPAddr1").value= "";
						  document.getElementById("txtMAddr2").value= "";
						  document.getElementById("txtMAddr3").value= "";
						  document.getElementById("txtMCity").value= "";
						  document.getElementById("txtMState").value= "";
						  document.getElementById("txtMCountry").value= "";
						  document.getElementById("txtMPIN").value= "";
						  document.getElementById("txtMPhone").value= "";
						  /*other informatiom */
						  
						  document.getElementById("optFemale").checked=false;
						  document.getElementById("optMale").checked=false;
						 
						  document.getElementById("txtFathHusbName").value= "";
						  document.getElementById("txtDomicile").value= "";
						  document.getElementById("txtNationality").value= "";
						  document.getElementById("txtBirthPlace").value= "";
						  document.getElementById("CmbBGroup").value= "";
						  document.getElementById("cmbFood").value= "";
						  document.getElementById("cmbReligion").value= "";
						  document.getElementById("txtPassportNo").value= "";
						  document.getElementById("txtDLNo").value= "";
						  document.getElementById("txtEmailID").value= "";
						  document.getElementById("txtPEmailId").value= "";
						  document.getElementById("TxtFathHusbOcupation").value= "";
						  document.getElementById("txtHobbies").value= "";
						  /*emergency contact */
						  document.getElementById("txtEmergencyName").value= "";
						  document.getElementById("txtEmergencyAddress").value= "";
						  document.getElementById("txtEmergencyPhoneNo").value= "";
						  document.getElementById("txtDrName").value= "";
						  document.getElementById("txtDrAddress").value= "";
						  document.getElementById("txtDrPhoneNo").value= "";
						   alert('Either your Employee Code is wrong or Employee Code field is blank.');
						  }
						
					    	    
					    
					}	
				}		
		</script>
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
			function UploadPage(argid)
				{
					if (document.getElementById("txtEM_CD").value == "") 
						{
						alert("Please Enter Employee Code then Click on Image Button.");
						}
					else	
						{
							var UPOBJ = window.open("UploadImage.aspx?Emp_Code=" + document.getElementById("txtEM_CD").value ,"search1","height=150,width=325,resizable=no,status=no,toolbar=no,menubar=no,location=no");
						}
				}	
		</script>
		<script language="vbscript">
			Sub Val(argid)
				IF document.getElementById("cmbmstatus").value = 2 THEN
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
					msgbox ("Last Name must be string type.")
					ValidateCtrl = false
					exit function
				end if
			
				If document.getElementById("txtPPIN").Value <> "" Then
					If Len(document.getElementById("txtPPIN").Value) <> 6 Then
						msgbox ("Permanent Pin No. Must be of Six Digit.")
						ValidateCtrl = False
						Exit Function
					End If
				End If
				
				If document.getElementById("txtMPIN").Value <> "" Then
					If Len(document.getElementById("txtMPIN").Value) <> 6 Then
						msgbox ("Mailing Pin No. Must be of Six Digit.")
						ValidateCtrl = False
						Exit Function
					End If
				End If
				
				dim dtDOB,dtDOM
				
				dtDOB = cdate(document.getElementById("dtpDOBcmbDD").Value & "/" & document.getElementById("dtpDOBcmbMM").Value & "/" & document.getElementById("dtpDOBcmbYY").Value)
				dtDOM = cdate(document.getElementById("dtpDOMcmbDD").Value & "/" & document.getElementById("dtpDOMcmbMM").Value & "/" & document.getElementById("dtpDOMcmbYY").Value)
				
				if Not IsDate(dtDOB) Then
					msgbox ("Invalid Date of Birth.")
					ValidateCtrl = false
					exit function
				end if
				
				If Not IsDate(dtDOM) Then
					MsgBOX ("Invalid Date Of Marriage.")
					ValidateCtrl = False
					Exit Function
				End If
				
            
				ValidateCtrl = true
			end function
		
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="5" rightMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx --><asp:label id="sAction" runat="server" Visible="False" Height="0px" Width="0px"></asp:label><br>
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
								<td width="10" bgColor="#666666"><IMG alt="" src="Images/SplitLeft.gif" border="0"></td>
								<td style="COLOR: white" align="center" bgColor="#666666"><b>General Info</b></td>
								<td width="10" bgColor="#666666"><IMG alt="" src="Images/rightcurve.gif" border="0"></td>
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
					<td width="87" style="WIDTH: 87px">
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
					<td colSpan="9" style="WIDTH: 790px">
						<table borderColor="black" cellSpacing="0" cellPadding="0" rules="none" width="100%" border="1"
							frame="box">
							<tr>
								<td colSpan="2"><asp:label id="LblMsg" runat="server" Width="100%" ForeColor="Red" Font-Size="11px" EnableViewState="False"></asp:label></td>
							</tr>
							<tr>
								<td width="50%">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td width="40%" style="HEIGHT: 23px"><asp:label id="lblCode" runat="server" Width="100%">Code</asp:label></td>
											<td width="60%" style="HEIGHT: 23px">
												<asp:textbox id="txtEM_CD" runat="server" Width="0px" ForeColor="#003366" CssClass="TextBox"
													BackColor="#E1E4EB" Font-Bold="True" AutoPostBack="True"></asp:textbox>&nbsp;
												<INPUT style="FONT-WEIGHT: bold; COLOR: #0033ff; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: #ffffff; BORDER-BOTTOM-STYLE: none"
													onclick="movefirst();" type="button" value="<<"><INPUT style="FONT-WEIGHT: bold; WIDTH: 22px; COLOR: #0033ff; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: #ffffff; BORDER-BOTTOM-STYLE: none"
													onclick="moveprevious();" type="button" value="<"> <INPUT class="TextBox" id="TxtEmpCode" onblur="Disp();" type="text" size="16" name="Text1"
													runat="server" style="WIDTH: 129px; HEIGHT: 20px"><INPUT style="FONT-WEIGHT: bold; COLOR: #0033ff; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: #ffffff; BORDER-BOTTOM-STYLE: none"
													onclick="movenext();" type="button" value=">"><INPUT style="FONT-WEIGHT: bold; COLOR: #0033ff; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: #ffffff; BORDER-BOTTOM-STYLE: none"
													onclick="movelast();" type="button" value=">>"></td>
										</tr>
										<tr>
											<td><asp:label id="lblFName" runat="server" Width="100%">First Name</asp:label></td>
											<td><asp:textbox id="txtFName" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"></asp:textbox></td>
										</tr>
										<tr>
											<td style="HEIGHT: 22px"><asp:label id="lblMName" runat="server" Width="100%">Middle Name</asp:label></td>
											<td style="HEIGHT: 22px"><asp:textbox id="txtMName" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"></asp:textbox></td>
										</tr>
										<tr>
											<td><asp:label id="lblLName" runat="server" Width="100%">Last Name</asp:label></td>
											<td><asp:textbox id="txtLName" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"></asp:textbox></td>
										</tr>
									</table>
								</td>
								<td vAlign="top" width="50%">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr vAlign="top">
											<td style="WIDTH: 198px" vAlign="top" width="198"><asp:button id="cmdNew" runat="server" Width="50px" Text="New"></asp:button></td>
											<td style="CURSOR: hand" width="50%"><asp:image id="ImgEmp" onclick="UploadPage(this.id)" runat="server" Width="80px" AlternateText="Click to Set Image."
													ToolTip="Click to Set Image."></asp:image></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<table borderColor="black" cellSpacing="0" cellPadding="0" rules="none" width="100%" border="1"
							frame="box">
							<tr>
								<td class="Header3" background="Images\headstripe.jpg" colSpan="2"><IMG id="imgAddress" style="CURSOR: hand" onclick="ShowHide('Address')" src="Images\Minus.gif">&nbsp;<b>Address
									</b>
								</td>
							</tr>
							<tr id="trAddress" runat="server">
								<td vAlign="top" width="50%">
									<table cellSpacing="0" cellPadding="2" width="100%" border="0">
										<tr class="Header3">
											<td align="center" colSpan="2"><B>Mailing Address</B></td>
										</tr>
										<tr>
										<tr>
											<td width="40%"><asp:label id="lblAddress" runat="server" Width="100%">Address</asp:label></td>
											<td width="60%"><asp:textbox id="txtMAddr1" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"
													MaxLength="40"></asp:textbox></td>
										</tr>
										<tr>
											<td></td>
											<td><asp:textbox id="txtMAddr2" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"
													MaxLength="40"></asp:textbox></td>
										</tr>
										<tr>
											<td></td>
											<td><asp:textbox id="txtMAddr3" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"
													MaxLength="40"></asp:textbox></td>
										</tr>
										<tr>
											<td><asp:label id="lblCity" runat="server" Width="100%">City</asp:label></td>
											<td><asp:textbox id="txtMCity" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"></asp:textbox></td>
										</tr>
										<tr>
											<td><asp:label id="lblState" runat="server" Width="100%">State</asp:label></td>
											<td><asp:textbox id="txtMState" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"
													MaxLength="40"></asp:textbox></td>
										</tr>
										<tr>
											<td><asp:label id="lblZIP" runat="server" Width="100%">ZIP/PIN</asp:label></td>
											<td><asp:textbox id="txtMPIN" onblur="CheckNum(this.id)" runat="server" Width="100%" ForeColor="#003366"
													CssClass="TextBox" MaxLength="6"></asp:textbox></td>
										</tr>
										<tr>
											<td><asp:label id="lblCountry" runat="server" Width="100%">Country</asp:label></td>
											<td><asp:textbox id="txtMCountry" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"
													MaxLength="20"></asp:textbox></td>
										</tr>
										<tr>
											<td><asp:label id="lblMPhone" runat="server" Width="100%">Phone/Mobile</asp:label></td>
											<td><asp:textbox id="txtMPhone" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"
													MaxLength="20"></asp:textbox></td>
										</tr>
									</table>
								</td>
								<td vAlign="top" width="50%">
									<table cellSpacing="0" cellPadding="2" width="100%" border="0">
										<tr class="Header3">
											<td align="center" colSpan="2"><B>Permanent Address</B></td>
										</tr>
										<tr>
										<tr>
											<td width="40%"><asp:label id="lblPAdd1" runat="server" Width="100%">Address</asp:label></td>
											<td width="60%"><asp:textbox id="txtPAddr1" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"
													MaxLength="40"></asp:textbox></td>
										</tr>
										<tr>
											<td></td>
											<td><asp:textbox id="txtPAddr2" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"
													MaxLength="40"></asp:textbox></td>
										</tr>
										<tr>
											<td style="HEIGHT: 26px"></td>
											<td style="HEIGHT: 26px"><asp:textbox id="txtPAddr3" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"
													MaxLength="40"></asp:textbox></td>
										</tr>
										<tr>
											<td><asp:label id="lblPCity" runat="server" Width="100%">City</asp:label></td>
											<td><asp:textbox id="txtPCity" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"
													MaxLength="40"></asp:textbox></td>
										</tr>
										<tr>
											<td><asp:label id="lblPState" runat="server" Width="100%">State</asp:label></td>
											<td><asp:textbox id="txtPState" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"
													MaxLength="40"></asp:textbox></td>
										</tr>
										<tr>
											<td><asp:label id="lblPZIP" runat="server" Width="100%">ZIP/PIN</asp:label></td>
											<td><asp:textbox id="txtPPIN" onblur="CheckNum(this.id)" runat="server" Width="100%" ForeColor="#003366"
													CssClass="TextBox" MaxLength="6"></asp:textbox></td>
										</tr>
										<tr>
											<td><asp:label id="lblPCountry" runat="server" Width="100%">Country</asp:label></td>
											<td><asp:textbox id="txtPCountry" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"
													MaxLength="20"></asp:textbox></td>
										</tr>
										<tr>
											<td><asp:label id="lblPPhone" runat="server" Width="100%">Phone/Mobile</asp:label></td>
											<td><asp:textbox id="txtPPhone" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"
													MaxLength="20"></asp:textbox></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<table borderColor="black" cellSpacing="0" cellPadding="0" rules="none" width="100%" border="1"
							frame="box">
							<tr>
								<td class="Header3" align="left" background="Images\headstripe.jpg" colSpan="2"><IMG id="imgOtherInfo" style="CURSOR: hand" onclick="ShowHide('OtherInfo')" src="Images\Plus.gif">&nbsp;<B>Other 
										Information </B>
								</td>
							</tr>
							<tr id="trOtherInfo" runat="server">
								<td vAlign="top" width="50%">
									<table cellSpacing="0" cellPadding="2" width="100%" border="0">
										<tr>
											<td width="40%"><asp:label id="lblGender" runat="server" Width="100%">Gender</asp:label></td>
											<td width="60%"><asp:radiobutton id="optMale" runat="server" Height="20px" Width="48%" Text="Male" Checked="True"
													GroupName="Gender"></asp:radiobutton><asp:radiobutton id="optFemale" runat="server" Height="20px" Width="48%" Text="Female" GroupName="Gender"></asp:radiobutton></td>
										</tr>
										<tr>
											<td style="HEIGHT: 23px"><asp:label id="lblMStatus" runat="server" Width="100%">Marital Status</asp:label></td>
											<td style="HEIGHT: 23px"><select id="CmbMStatus" style="WIDTH: 100%" onchange="Val('ChkDOM')" name="CmbMStatus" runat="server">
													<option value="0">&nbsp;</option>
													<option value="1">Married</option>
													<option value="2" selected>Single</option>
													<option value="3">Widow</option>
													<option value="4">Widover</option>
													<option value="5">Divorcee</option>
												</select>
											</td>
										</tr>
										<tr>
											<td style="HEIGHT: 19px"><asp:label id="lblDOB" runat="server" Width="100%">Date of Birth</asp:label></td>
											<td style="HEIGHT: 19px"><cc1:dtpcombo id="dtpDOB" runat="server" Height="20px" Width="100%" ToolTip="Date of Birth"></cc1:dtpcombo></td>
										</tr>
										<tr>
											<td><asp:dropdownlist id="cmbFatherHusband" runat="server" Height="20px" Width="100%"></asp:dropdownlist></td>
											<td><asp:textbox id="txtFathHusbName" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"
													ToolTip="Father's / Husband's Name"></asp:textbox></td>
										</tr>
										<tr>
											<td><asp:label id="lblDomicile" runat="server" Width="100%" ToolTip="Domicile">Domicile</asp:label></td>
											<td><asp:textbox id="txtDomicile" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"
													ToolTip="Domicile"></asp:textbox></td>
										</tr>
										<tr>
											<td><asp:label id="lblNationality" runat="server" Width="100%"> Nationality</asp:label></td>
											<td><asp:textbox id="txtNationality" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"></asp:textbox></td>
										</tr>
										<tr>
											<td><asp:label id="lblBirthPlace" runat="server" Width="100%">Birth Place</asp:label></td>
											<td><asp:textbox id="txtBirthPlace" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"></asp:textbox></td>
										</tr>
										<tr>
											<td><asp:label id="LblDOM" runat="server" Width="100%" ToolTip="Date of Marriage">Date of Marriage</asp:label></td>
											<td><cc1:dtpcombo id="DtpDOM" runat="server" Height="20px" Width="100%" ToolTip="Date of Birth"></cc1:dtpcombo></td>
										</tr>
									</table>
								</td>
								<td vAlign="top" width="50%">
									<table cellSpacing="0" cellPadding="2" width="100%" border="0">
										<tr>
											<td style="HEIGHT: 18px" width="40%"><asp:label id="lblBGroup" runat="server" Width="100%" ToolTip="Blood Group">Blood Group</asp:label></td>
											<td style="HEIGHT: 18px" width="60%"><asp:dropdownlist id="CmbBGroup" runat="server" Height="20px" Width="25%"></asp:dropdownlist>Food 
												Choice&nbsp;<asp:dropdownlist id="cmbFood" runat="server" Height="20px" Width="38%">
													<asp:ListItem Selected="True" Value="">&nbsp;</asp:ListItem>
													<asp:ListItem Value="Veg">Veg</asp:ListItem>
													<asp:ListItem Value="Non-Veg">Non-Veg</asp:ListItem>
												</asp:dropdownlist></td>
										</tr>
										<tr>
											<td><asp:label id="lblReligion" runat="server" Width="100%" ToolTip="Religion">Religion</asp:label></td>
											<td><asp:dropdownlist id="cmbReligion" runat="server" Height="20px" Width="100%"></asp:dropdownlist></td>
										</tr>
										<tr>
											<td colSpan="2"><asp:label id="lblPassport" runat="server" Width="112px" ToolTip="Passport No.">Passport No</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:textbox id="txtPassportNo" runat="server" Width="75px" CssClass="TextBox" MaxLength="20"></asp:textbox>-
												<cc1:dtpcombo id="dtpPassportExp" runat="server" Height="20px" Width="100PX" ToolTip="Date of Birth"
													DESIGNTIMEDRAGDROP="8123"></cc1:dtpcombo></td>
										</tr>
										<tr>
											<td><asp:label id="lblDLNum" runat="server" Width="100%" ToolTip="DL. NUM">DL. NUM</asp:label></td>
											<td><asp:textbox id="txtDLNo" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"></asp:textbox></td>
										</tr>
										<tr>
											<td><asp:label id="lblEmailID" runat="server" Width="100%" ToolTip="E-mail ID">E-mail ID</asp:label></td>
											<td><asp:textbox id="txtEmailID" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"></asp:textbox></td>
										</tr>
										<tr>
											<td><asp:label id="lblPersEmailID" runat="server" Width="100%" ToolTip="Pers. Email ID">Pers. Email ID</asp:label></td>
											<td><asp:textbox id="txtPEmailId" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"></asp:textbox></td>
										</tr>
										<tr>
											<td style="HEIGHT: 10px"><asp:label id="LblSpouse" runat="server" Width="100%" ToolTip="Spouse Name">Spouse Name</asp:label></td>
											<td style="HEIGHT: 10px"><asp:textbox id="TxtFathHusbOcupation" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"></asp:textbox></td>
										</tr>
										<tr>
											<td><asp:label id="lblHobbies" runat="server" Width="100%" ToolTip="Hobbies">Hobbies</asp:label></td>
											<td><asp:textbox id="txtHobbies" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"
													MaxLength="50"></asp:textbox></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<table borderColor="black" cellSpacing="0" cellPadding="0" rules="none" width="100%" border="1"
							frame="box">
							<tr>
								<td class="Header3" align="left" background="Images\headstripe.jpg" colSpan="2"><IMG id="imgEmergency" style="CURSOR: hand" onclick="ShowHide('Emergency')" src="Images\Plus.gif">&nbsp;<B>Emergency 
										Contact</B></td>
							</tr>
							<tr id="trEmergency" runat="server">
								<td vAlign="top" width="50%">
									<table cellSpacing="0" cellPadding="2" width="100%" border="0">
										<tr>
											<td width="40%"><asp:label id="lblPerson" runat="server" Width="100%">Person</asp:label></td>
											<td width="60%"><asp:textbox id="txtEmergencyName" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"></asp:textbox></td>
										</tr>
										<tr>
											<td><asp:label id="lblEmrAdd" runat="server" Width="100%">Address</asp:label></td>
											<td><asp:textbox id="txtEmergencyAddress" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"></asp:textbox></td>
										</tr>
										<tr>
											<td><asp:label id="lblEmrPhone" runat="server" Width="100%">Phone/Mobile</asp:label></td>
											<td><asp:textbox id="txtEmergencyPhoneNo" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"></asp:textbox></td>
										</tr>
									</table>
								</td>
								<td vAlign="top" width="50%">
									<table cellSpacing="0" cellPadding="2" width="100%" border="0">
										<tr>
											<td width="40%"><asp:label id="lblDocName" runat="server" Width="100%">Family Doctor Name</asp:label></td>
											<td width="60%"><asp:textbox id="txtDrName" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"></asp:textbox></td>
										</tr>
										<tr>
											<td><asp:label id="lblDocAdd" runat="server" Width="100%">Family Doctor Address</asp:label></td>
											<td><asp:textbox id="txtDrAddress" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"></asp:textbox></td>
										</tr>
										<tr>
											<td><asp:label id="lblDocPhone" runat="server" Width="100%">Family Doctor Phone</asp:label></td>
											<td><asp:textbox id="txtDrPhoneNo" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"></asp:textbox></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<table borderColor="black" cellSpacing="0" cellPadding="0" rules="none" width="100%" border="1"
							frame="box">
							<tr>
								<td class="Header3" align="left" background="Images\headstripe.jpg" colSpan="2"><IMG id="imgLanguages" style="CURSOR: hand" onclick="ShowHide('Languages')" src="Images\Plus.gif">&nbsp;<B>Languages</B></td>
							</tr>
							<tr id="trLanguages" style="DISPLAY: none" runat="server">
								<td colSpan="2" style="HEIGHT: 29px">
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
									<table id="TblLanguages" cellSpacing="0" cellPadding="0" rules="rows" width="100%" border="1"
										frame="border" runat="server">
									</table>
								</td>
							</tr>
							<tr>
								<td align="right" colSpan="2"><asp:button id="cmdSave" accessKey="S" runat="server" Width="75px" Text="Save"></asp:button>&nbsp;
									<asp:button id="cmdClose" runat="server" Width="75px" Text="Close"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td align="center" colSpan="9" style="WIDTH: 789px"><asp:label id="LblRights" Width="100%" Font-Size="10" Font-Bold="True" Runat="server"></asp:label></td>
				</tr>
			</table>
		</form>
		<script language="javascript">
			var response=GeneralInfo.SetCurrentEmpCode();
			var dt = response.value;
			if (dt != null)
			{
				document.getElementById("TxtEmpCode").value = dt;
				document.getElementById("TxtEmpCode").blur();
			}
			
		</script>
	</body>
</HTML>
