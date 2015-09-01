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
		<script language="javascript">
	
		function selChanged()
			{
				//PAY mode must be blank
				 var com=document.getElementById("cmbLeaveType");
				 if(document.getElementById("cmbLeaveType").value==" " || com.options[com.selectedIndex].text==' ' || com.options[com.selectedIndex].text=="None" || document.getElementById("cmbLeaveType").value=="None" || document.getElementById("cmbLeaveType").value=="Regularized" || com.options[com.selectedIndex].text=="Regularized")
					{
						document.getElementById('LblDate1').style .display='none'
						document.getElementById('LblDate2').style .display='none'
						document.getElementById('LblDate3').style .display='none'	
						document.getElementById('LblNewOrg').style .display='none';	
						
						//if (document.getElementById('LblNewOrg').style .display =='') document.getElementById('LblNewOrg').style .display='none';
						document.getElementById('TxtNewOrg').style .display='none'
						document.getElementById('dtpDOL').style .display='none'
						document.getElementById('dtpNotice').style .display='none'
						document.getElementById('dtpSettle').style .display='none'
						document.getElementById('ChkNotice').style .display='none'
						document.getElementById('ChkSettle').style .display='none'
						document.getElementById('ChkDOL').style .display='none'
													
					}
						  
				  if(com.options[com.selectedIndex].text=="Resigned" || document.getElementById("cmbLeaveType").value=="Resigned")
					{
						binddtp('Releaving Date','Resignated Date','Settlement Date');	
						document.getElementById('LblNewOrg').style .display=''
						document.getElementById('TxtNewOrg').style .display=''
													
					}
					
				if(com.options[com.selectedIndex].text=="Terminated" || document.getElementById("cmbLeaveType").value=="Terminated")
					{
						binddtp('Notice Date','Termination Date','Settlement Date');		
						
																		
					}	
				if(com.options[com.selectedIndex].text=="Retired" || document.getElementById("cmbLeaveType").value=="Retired")
					{
						binddtp('Notice Date','Retirement Date','Settlement Date');	
						
													
					}		
					
				if(com.options[com.selectedIndex].text=="Payhold" || document.getElementById("cmbLeaveType").value=="Payhold")
					{
						binddtp('','Pay Holding Date','');	
						
						document.getElementById('dtpNotice').style .display='none'
						document.getElementById('dtpSettle').style .display='none'
						document.getElementById('ChkNotice').style .display='none'
						document.getElementById('ChkSettle').style .display='none'
													
					}
				if(com.options[com.selectedIndex].text=="Death" || document.getElementById("cmbLeaveType").value=="Death")
					{
						binddtp('Releaving Date','Death Date','Settlement Date');	
						
													
					}	
				if(com.options[com.selectedIndex].text=="Transfer" || document.getElementById("cmbLeaveType").value=="Transfer")
					{
						binddtp('Releaving Date','Transfer Date','Settlement Date');	
						
													
					}			
				if(com.options[com.selectedIndex].text=="Separated" || document.getElementById("cmbLeaveType").value=="Separated")
					{
						binddtp('Releaving Date','Separated Date','Settlement Date');	
						
													
					}			
					
			}
			
		function binddtp(rd,rld,sd)
			{
				document.getElementById('LblDate1').style .display=''
				document.getElementById('LblDate2').style .display=''
				document.getElementById('LblDate3').style .display=''
				
				document.getElementById('dtpDOL').style .display=''
				document.getElementById('dtpNotice').style .display=''
				document.getElementById('dtpSettle').style .display=''
				document.getElementById('ChkNotice').style .display=''
				document.getElementById('ChkSettle').style .display=''
				document.getElementById('ChkDOL').style .display=''	
				document.getElementById('LblNewOrg').style .display='none';
				document.getElementById('TxtNewOrg').style .display='none'
				
						
				document.getElementById('LblDate1').value=rd;
				document.getElementById('LblDate2').value=rld;
				document.getElementById('LblDate3').value=sd;
				
				if(document.getElementById('Txtdtp').value=='bind')
					{
						showdate('Notice','LNOTICE');
						showdate('Settle','SET_DATE');
						showdate('DOL','DOL');						
					}
			}
		
		function chks(str)
			{
				if(str==null)
				{	return(" ");	}
				else
				{	return str;	}
			}
			
			
			function showdate(fldname,colname)
			{
				var response=Others.GetEmpRec(document.getElementById("TxtEmpCode").value);
				var dt = response.value;
				if(dt.Tables[0].Rows[0][colname]!=null)
					{						
						document.getElementById("Chk"+fldname).checked = true;
						document.getElementById("dtp"+fldname+'cmbDD').disabled = false;
						document.getElementById("dtp"+fldname+'cmbYY').disabled= false;
						document.getElementById("dtp"+fldname+'cmbMM').disabled = false;                       			 
						document.getElementById('dtp'+fldname+'cmbDD').value=dt.Tables[0].Rows[0][colname].getDate();
						document.getElementById('dtp'+fldname+'cmbMM').value=dt.Tables[0].Rows[0][colname].getMonth()+1;
						if (dt.Tables[0].Rows[0][colname].getYear()<100)
							document.getElementById('dtp'+fldname+'cmbYY').value=1900+dt.Tables[0].Rows[0][colname].getYear() ;
						else
							document.getElementById('dtp'+fldname+'cmbYY').value=dt.Tables[0].Rows[0][colname].getYear() ;
					}
				else
					{
						document.getElementById("Chk"+fldname).checked = false;
						document.getElementById("dtp"+fldname+'cmbDD').disabled = true;
						document.getElementById("dtp"+fldname+'cmbYY').disabled= true;
						document.getElementById("dtp"+fldname+'cmbMM').disabled = true; 
						var cd = new Date(); 
						document.getElementById("dtp"+fldname+'cmbDD').value =cd.getDate();
						document.getElementById("dtp"+fldname+'cmbYY').value=cd.getFullYear();
						document.getElementById("dtp"+fldname+'cmbMM').value=cd.getMonth()+1;          
						      
								
					}	
					
			}
			
			//Display Records after TxtEmpCode changed
			function Disp()
			{				
				var EmpCode;
				EmpCode=document.getElementById("TxtEmpCode").value;				
				document.getElementById("txtEM_CD").value= document.getElementById("TxtEmpCode").value
				Others.SetEmpCode(EmpCode);
				Others.GetEmpRec(CallBack);
			}
			function movenext()
			{
				var EmpCode;
				EmpCode=document.getElementById("TxtEmpCode").value;
				var response=Others.GetNextEmpRec(EmpCode)
				EmpCode=response.value;
				document.getElementById("TxtEmpCode").value=EmpCode;
				//Others.SetEmpCode(EmpCode);
				document.getElementById("txtEM_CD").value= document.getElementById("TxtEmpCode").value
				Others.SetEmpCode(EmpCode);
				Others.GetEmpRec(CallBack);
			}
			function moveprevious()
			{
				var EmpCode;
				EmpCode=document.getElementById("TxtEmpCode").value;
				var response=Others.GetPreviousEmpRec(EmpCode)
				EmpCode=response.value;
				document.getElementById("TxtEmpCode").value=EmpCode;
				//Others.SetEmpCode(EmpCode);
				document.getElementById("txtEM_CD").value= document.getElementById("TxtEmpCode").value
				Others.SetEmpCode(EmpCode);
				Others.GetEmpRec(CallBack);
			}
			function movefirst()
			{
				var EmpCode;
				EmpCode=document.getElementById("TxtEmpCode").value;
				//alert('tesu');
				var response=Others.GetFirstEmpRec()
				EmpCode=response.value;
				document.getElementById("TxtEmpCode").value=EmpCode;
				//Others.SetEmpCode(EmpCode);
				document.getElementById("txtEM_CD").value= document.getElementById("TxtEmpCode").value
				Others.SetEmpCode(EmpCode);
				Others.GetEmpRec(CallBack);
			}
			function movelast()
			{
				var EmpCode;
				EmpCode=document.getElementById("TxtEmpCode").value;
				//alert('tesu');
				var response=Others.GetLastEmpRec()
				EmpCode=response.value;
				document.getElementById("TxtEmpCode").value=EmpCode;
				//Others.SetEmpCode(EmpCode);
				document.getElementById("txtEM_CD").value= document.getElementById("TxtEmpCode").value
				Others.SetEmpCode(EmpCode);
				Others.GetEmpRec(CallBack);
			}
			
			
			function CallBack(response)
				{
					var sText;
					var response=Others.GetEmpRec(document.getElementById("TxtEmpCode").value);
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
							
						  document.getElementById("TxtFname").value= chks(dt.Tables[0].Rows[0]['FNAME']);
						  document.getElementById("cmbPayMode").value= chks(dt.Tables[0].Rows[0]['BANK_CODE']);
						  document.getElementById("TxtBANKACNO").value= chks(dt.Tables[0].Rows[0]['BANKACNO']);
						  document.getElementById("TxtPFNO").value= chks(dt.Tables[0].Rows[0]['PFNO']);
						  document.getElementById("TxtESINO").value= chks(dt.Tables[0].Rows[0]['ESINO']);
						  document.getElementById("TxtPANNO").value= chks(dt.Tables[0].Rows[0]['PANNO']);
						  document.getElementById("TxtTANNO").value= chks(dt.Tables[0].Rows[0]['TANNO']);
						  document.getElementById("TxtJOBPROFILE").value= chks(dt.Tables[0].Rows[0]['JOBPROFILE']);
						  showdate('ContactEnd','DOCE');
						  showdate('LAppraisal','LAST_APPR');
						  showdate('LInc','LAST_INCR');
						  showdate('LPromo','LAST_PROM');
						  showdate('GrpJoin','DOJGroup');						  
						  document.getElementById("cmbLeaveType").value= chks(dt.Tables[0].Rows[0]['LTYPE']);
						  document.getElementById("TxtLReason").value= chks(dt.Tables[0].Rows[0]['LREASON']);  
						  
						  document.getElementById("Txtdtp").value='bind'; //it is setting for dtp binding when user navigate with >,<,>>or <<
						  
						  selChanged();
						  // Binding  Separation according to Leaving Type
						 
						  
						  
						  
						 // document.getElementById("TxtNewOrg").value= chks(dt.Tables[0].Rows[0]['NewOrg']);
						 
						  
						  
						  
						  //document.getElementById("cmbLeaveType").value= chks(dt.Tables[0].Rows[0]['LTYPE']);
						  //var com=document.getElementById("cmbLeaveType");
						  //if(com.options[com.selectedIndex].text=="None")
							//{
								//if(if(dt.Tables[0].Rows[0]["DOL"]!=null)
									//{
										//document.getElementById("ChkDOL").checked = true;
										//document.getElementById('dtpDOLcmbDD').value=dt.Tables[0].Rows[0]["DOL"].getDate();
										//document.getElementById('dtpDOLcmbMM').value=dt.Tables[0].Rows[0]["DOL"].getMonth()+1;
										//document.getElementById('dtpDOLcmbYY').value=dt.Tables[0].Rows[0]["DOL"].getFullYear();
									
									//}
								
							//}
							

						 
						// showdate('Notice','LNOTICE');
						// showdate('Settle','SET_DATE');
						 				  
						 
						 						  
						 }			  						  						  
						 
						  
						  else 
						  {					                                          
						  document.getElementById("TxtFname").value= "";
						  document.getElementById("cmbPayMode").value="";
						  document.getElementById("TxtBANKACNO").value= "";						  
						  document.getElementById("TxtPFNO").value= "";
						  document.getElementById("TxtESINO").value= "";
						  document.getElementById("TxtPANNO").value= "";
						  document.getElementById("TxtTANNO").value= "";
						  document.getElementById("TxtJOBPROFILE").value= "";
						  document.getElementById("cmbLeaveType").value= "";
						  document.getElementById("TxtLReason").value= ""; 
						 
						//  alert('Either your Employee Code is wrong or Employee Code field is blank.');
						  }
						   	    
					 }   
				}	
		
		</script>
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
					'MsgBox (Len(document.getElementById("TxtPANNO").Value))
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
					'document.getElementById("TxtPANNo").value = "PANNOTAVBL" 
					'exit sub	
				Else					
					if CheckPAN(PANNo) = False then
						Msgbox "Please enter valid PAN No. ex. ABCDE1234F", 64 ,"Invalid PAN No. !"
						document.getElementById("TxtPANNo").focus
					end if
				End if
			End Sub			
						
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="5" onload="selChanged()" rightMargin="0"
		MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx --><asp:label id="Label6" style="Z-INDEX: 102; LEFT: 8px; POSITION: absolute; TOP: 0px" runat="server"
				Font-Names="Book Antiqua" Width="136px">Field Indicated by </asp:label><asp:label id="Label7" style="Z-INDEX: 105; LEFT: 160px; POSITION: absolute; TOP: 0px" runat="server"
				Font-Names="Book Antiqua" Width="112px">is Mandatory</asp:label><asp:label id="Label8" style="Z-INDEX: 104; LEFT: 144px; POSITION: absolute; TOP: 0px" runat="server"
				Width="8px" ForeColor="Red">*</asp:label><br>
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
								<td style="WIDTH: 171px" width="171"></td>
								<td width="30%"></td>
								<td width="20%"></td>
								<td width="30%"></td>
							</tr>
							<tr>
								<td colSpan="4"><asp:label id="LblErrMsg" Width="100%" ForeColor="Red" EnableViewState="False" Runat="server"></asp:label></td>
							</tr>
							<tr>
								<td style="WIDTH: 171px">&nbsp;Code</td>
								<td><INPUT style="FONT-WEIGHT: bold; COLOR: #0033ff; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: #ffffff; BORDER-BOTTOM-STYLE: none"
										onclick="movefirst();" type="button" value="<<">&nbsp; <INPUT style="FONT-WEIGHT: bold; COLOR: #0033ff; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: #ffffff; BORDER-BOTTOM-STYLE: none"
										onclick="moveprevious();" type="button" value="<"><asp:textbox id="txtEM_CD" Width="0px" ForeColor="#003366" Runat="server" AutoPostBack="True"
										Font-Bold="True" BackColor="White" CssClass="TextBox"></asp:textbox><INPUT class="TextBox" id="TxtEmpCode" onblur="Disp();" style="WIDTH: 134px; HEIGHT: 20px"
										type="text" size="17" name="Text1" runat="server"><INPUT style="FONT-WEIGHT: bold; COLOR: #0033ff; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: #ffffff; BORDER-BOTTOM-STYLE: none"
										onclick="movenext();" type="button" value=">">&nbsp;&nbsp; <INPUT style="FONT-WEIGHT: bold; COLOR: #0033ff; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: #ffffff; BORDER-BOTTOM-STYLE: none"
										onclick="movelast();" type="button" value=">>"></td>
								<td align="center" colSpan="2"><asp:textbox id="TxtFname" runat="server" BorderStyle="None" ReadOnly="True"></asp:textbox><asp:label id="LblName" runat="server" Width="155px" ForeColor="#003366" Font-Bold="True" Font-Size="9"></asp:label></td>
							</tr>
							<tr>
								<td style="WIDTH: 171px; HEIGHT: 16px">&nbsp;Pay Mode
								</td>
								<td style="HEIGHT: 16px"><asp:dropdownlist id="cmbPayMode" runat="server" Width="100%"></asp:dropdownlist></td>
								<td style="HEIGHT: 16px">&nbsp;Contract End Date
								</td>
								<td style="HEIGHT: 16px"><INPUT id="ChkContactEnd" onclick="Val(this.id)" type="checkbox" name="ChkContactEnd" runat="server"><cc1:dtpcombo id="dtpContactEnd" runat="server" Width="100%" Enabled="False" Height="20px" ToolTip="Contract End Date"></cc1:dtpcombo></td>
							</tr>
							<tr>
								<td style="WIDTH: 171px">&nbsp;S.B. A/C No.
								</td>
								<td><asp:textbox id="TxtBANKACNO" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"></asp:textbox></td>
								<td>&nbsp;Last Promotion Date
								</td>
								<td><INPUT id="ChkLPromo" onclick="Val(this.id)" type="checkbox" name="ChkLPromo" runat="server">
									<cc1:dtpcombo id="dtpLPromo" runat="server" Width="100%" Enabled="False" Height="20px" ToolTip="Last Promotion Date"></cc1:dtpcombo></td>
							</tr>
							<tr>
								<td style="WIDTH: 171px">&nbsp;PF No.
								</td>
								<td><asp:textbox id="TxtPFNO" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"></asp:textbox></td>
								<td>&nbsp;Last Increment Date
								</td>
								<td><INPUT id="ChkLInc" onclick="Val(this.id)" type="checkbox" name="ChkLInc" runat="server">
									<cc1:dtpcombo id="dtpLInc" runat="server" Width="100%" Enabled="False" Height="20px" ToolTip="Last Increment Date"></cc1:dtpcombo></td>
							</tr>
							<tr>
								<td style="WIDTH: 171px">&nbsp;ESI No
								</td>
								<td><asp:textbox id="TxtESINO" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"></asp:textbox></td>
								<td>&nbsp;Last Appraisal Date
								</td>
								<td><INPUT id="ChkLAppraisal" onclick="Val(this.id)" type="checkbox" name="ChkLAppraisal" runat="server">
									<cc1:dtpcombo id="dtpLAppraisal" runat="server" Width="100%" Enabled="False" Height="20px" ToolTip="Last Appraisal Date"></cc1:dtpcombo></td>
							</tr>
							<tr>
								<td style="WIDTH: 171px">&nbsp;PAN No</td>
								<td><asp:textbox id="TxtPANNO" onblur="ChkPAN()" runat="server" Width="100%" ForeColor="#003366"
										CssClass="TextBox" MaxLength="10"></asp:textbox></td>
								<td>&nbsp;Transfer Date</td>
								<td><INPUT id="ChkGrpJoin" onclick="Val(this.id)" type="checkbox" name="ChkGrpJoin" runat="server">
									<cc1:dtpcombo id="dtpGrpJoin" runat="server" Width="100%" Enabled="False" Height="20px" ToolTip="Transfer Date"></cc1:dtpcombo></td>
							</tr>
							<tr>
								<td style="WIDTH: 171px">&nbsp;TAN No
								</td>
								<td><asp:textbox id="TxtTANNO" runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"
										MaxLength="10"></asp:textbox></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td style="WIDTH: 171px">&nbsp;Job Profile
								</td>
								<td colSpan="3"><asp:textbox id="TxtJOBPROFILE" runat="server" Width="100%" Rows="3" TextMode="MultiLine"></asp:textbox></td>
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
											<td width="20%" style="HEIGHT: 13px"></td>
											<td style="WIDTH: 263px; HEIGHT: 13px" width="263"></td>
											<td width="20%" style="HEIGHT: 13px"></td>
											<td width="30%" style="HEIGHT: 13px"></td>
										</tr>
										<tr>
											<td style="HEIGHT: 36px">Leaving Type
											</td>
											<td style="WIDTH: 263px; HEIGHT: 36px"><asp:dropdownlist id="cmbLeaveType" runat="server" Width="100%">
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
											<td style="HEIGHT: 36px"><asp:textbox id="LblDate1" runat="server" BorderStyle="None" Font-Size="XX-Small">Resigned Date</asp:textbox></td>
											<td style="HEIGHT: 36px"><INPUT id="ChkNotice" onclick="Val(this.id)" type="checkbox" name="ChkNotice" runat="server">
												<cc1:dtpcombo id="dtpNotice" runat="server" Width="100%" Enabled="False" Height="20px"></cc1:dtpcombo></td>
										</tr>
										<tr>
											<td style="HEIGHT: 20px"><asp:textbox id="LblDate2" runat="server" BorderStyle="None" Font-Size="XX-Small">Reliving Date</asp:textbox></td>
											<td style="WIDTH: 263px; HEIGHT: 20px"><INPUT id="ChkDOL" onclick="Val(this.id)" type="checkbox" name="ChkDOL" runat="server">
												<cc1:dtpcombo id="dtpDOL" runat="server" Width="100%" Enabled="False" Height="20px"></cc1:dtpcombo></td>
											<td style="HEIGHT: 20px"><asp:textbox id="LblDate3" runat="server" BorderStyle="None" Font-Size="XX-Small">Settlement Date</asp:textbox></td>
											<td style="HEIGHT: 20px"><INPUT id="ChkSettle" onclick="Val(this.id)" type="checkbox" name="ChkSettle" runat="server">
												<cc1:dtpcombo id="dtpSettle" runat="server" Width="100%" Enabled="False" Height="20px"></cc1:dtpcombo></td>
										</tr>
										<tr id="trNewOrg">
											<td>
												<asp:TextBox id="LblNewOrg" runat="server" BorderStyle="None" Font-Size="XX-Small">New Organisation</asp:TextBox></td>
											<td colSpan="3"><asp:textbox id="TxtNewOrg" runat="server" Width="100%" CssClass="TextBox"></asp:textbox></td>
										</tr>
										<tr>
											<td>Reason
											</td>
											<td colSpan="3"><asp:textbox id="TxtLReason" runat="server" Width="100%" Rows="3" TextMode="MultiLine"></asp:textbox></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td align="right"><asp:button id="cmdSave" Width="75px" Runat="server" Text="Save"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:button id="cmdClose" Width="75px" Runat="server" Text="Close"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td align="center" colSpan="9"><asp:label id="LblRights" Width="100%" Runat="server" Font-Bold="True" Font-Size="10"></asp:label></td>
				</tr>
			</table>
			<asp:textbox id="Txtdtp" style="Z-INDEX: 101; LEFT: 480px; POSITION: absolute; TOP: 552px" runat="server"
				Width="0px">nobind</asp:textbox></form>
		<script language="javascript">
		
			var response=Others.SetCurrentEmpCode();
			var dt = response.value;
			if (dt != null)
			{
				document.getElementById("TxtEmpCode").value = dt;
				//document.getElementById("TxtEmpCode").blur();
				Disp();
			}
			
		</script>
		</B>
	</body>
</HTML>
