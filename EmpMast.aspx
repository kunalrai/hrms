<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="EmpMast.aspx.vb" Inherits="eHRMS.Net.EmpMast"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>Employee Master</title>
<meta content=False name=vs_snapToGrid>
<meta content="Microsoft Visual Studio .NET 7.1" name=GENERATOR>
<meta content="Visual Basic .NET 7.1" name=CODE_LANGUAGE>
<meta content=JavaScript name=vs_defaultClientScript>
<meta content=http://schemas.microsoft.com/intellisense/ie5 name=vs_targetSchema>
<LINK href="HCStyles.css" type=text/css rel=stylesheet >
<SCRIPT language=javascript src="coolmenus4.js"></SCRIPT>

<script language=javascript>
	
//This function is hides the combo
		function combohide()
			{
				//hides and Show SAVE Button
				
				if(document.getElementById('TxtRights').value=="S")
				
					document.getElementById ('cmdSave').style.display='';
				//if(document.getElementById('TxtRights').value=="V")
				else
				
					document.getElementById ('cmdSave').style.display='none';
						
				document.getElementById('cmbDesignation').style .display='none';document.getElementById('txtDesignation').style .display='';
				document.getElementById('cmbDepartment').style .display='none';document.getElementById('txtDepartment').style .display='';
				document.getElementById('cmbLocation').style .display='none';document.getElementById('txtLocation').style .display='';
				document.getElementById('cmbALocation').style .display='none';document.getElementById('txtALocation').style .display='';
				document.getElementById('cmbPLocation').style .display='none';document.getElementById('txtPLocation').style .display='';
				document.getElementById('cmbEmpType').style .display='none';document.getElementById('txtEmpType').style .display='';
				document.getElementById('cmbJobName').style .display='none';document.getElementById('txtJobName').style .display='';
				document.getElementById('cmbEmpClass').style .display='none';document.getElementById('txtEmpClass').style .display='';
				document.getElementById('cmbHrMngr').style .display='none';document.getElementById('txtHrMngr').style .display='';
				document.getElementById('cmbGrade').style .display='none';document.getElementById('txtGrade').style .display='';
				document.getElementById('cmbRegion').style .display='none';document.getElementById('txtRegion').style .display='';
				document.getElementById('cmbSection').style .display='none';document.getElementById('txtSection').style .display='';
				document.getElementById('cmbDivision').style .display='none';document.getElementById('txtDivision').style .display='';
				document.getElementById('cmbFull').value="";
				document.getElementById('cmbCosting').style .display='none';document.getElementById('txtCosting').style .display='';
				document.getElementById('cmbUnit').style .display='none';document.getElementById('txtUnit').style .display='';
				document.getElementById('cmbContType').style .display='none';document.getElementById('txtContType').style .display='';
				document.getElementById('cmbProcess').style .display='none';document.getElementById('txtProcess').style .display='';
				document.getElementById('cmbManager').style .display='none';document.getElementById('txtManager').style .display='';
				document.getElementById('cmbCostCenter').style .display='none';document.getElementById('txtCostCenter').style .display='';
				
			}
		
		function showdate(fldname,colname)
			{
				var response=EmpMast.GetEmpRec(document.getElementById("TxtEmpCode").value);
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
		
		function chks(str)
			{			
				if(str==null)
					{	return("");		}
				else
					{	return str;		}
			}
			
		function movenext()
			{
				combohide();
				var EmpCode;
				EmpCode=document.getElementById("TxtEmpCode").value;
				//alert('tesu');
				var response=EmpMast.GetNextEmpRec(EmpCode)
				EmpCode=response.value;
				document.getElementById("TxtEmpCode").value=EmpCode;
				//EmpMast.SetEmpCode(EmpCode);
				document.getElementById("txtEM_CD").value= document.getElementById("TxtEmpCode").value
				EmpMast.SetEmpCode(EmpCode);
				EmpMast.GetEmpRec(CallBack);
			}
		function save()
			{	
				if (document.getElementById("txtEM_CD").value=='')
				{
					alert('Please Select the Code.')
					return 
				
				}
				var ecode=document.getElementById("txtEM_CD").value;			
				var fnam=document.getElementById("txtFName").value;
				var lnme=document.getElementById("txtLName").value;
				var comp=document.getElementById("TxtComp").value;
				var sap=document.getElementById("TxtSalAdminPlan").value;
				var jr=document.getElementById("TxtJobRanking").value;
				var bt=document.getElementById("TxtBTitle").value;				
						
				var deg=document.getElementById("txtDesignation").toolTip;
				var dep=document.getElementById("txtDepartment").toolTip;
				var loc=document.getElementById("txtLocation").toolTip;	
				
				var alo=document.getElementById("txtALocation").toolTip;
				var plo=document.getElementById("txtPLocation").toolTip;
				var et=document.getElementById("txtEmpType").toolTip;
				
				var jn=document.getElementById("txtJobName").toolTip;
				var ec=document.getElementById("txtEmpClass").toolTip;
				var hm=document.getElementById("txtHrMngr").toolTip;
				var man=document.getElementById("txtManager").toolTip;
				var gr=document.getElementById("txtGrade").toolTip;
				var cc=document.getElementById("txtCostCenter").toolTip;
				var pro=document.getElementById("txtProcess").toolTip;						
				var reg=document.getElementById("txtRegion").toolTip;
				var sec=document.getElementById("txtSection").toolTip;
				var div=document.getElementById("txtDivision").toolTip;
				
				var cos=document.getElementById("txtCosting").toolTip;
				var unt=document.getElementById("txtUnit").toolTip;
				var ct=document.getElementById("txtContType").toolTip;
			
				var full=document.getElementById("cmbFull").value;
				
				var doc
				if(document.getElementById("ChkDOC").checked==true)
				//{alert('doctype');
				doc=document.getElementById("dtpDOCcmbMM").value + "/" + document.getElementById("dtpDOCcmbDD").value + "/" +  document.getElementById("dtpDOCcmbYY").value; else doc="";
				var ddedpe
				if(document.getElementById("ChkDDEDPE").checked==true)
				ddedpe= document.getElementById("dtpDDEDPEcmbMM").value + "/" + document.getElementById("dtpDDEDPEcmbDD").value +  "/" + document.getElementById("dtpDDEDPEcmbYY").value; else 	ddedpe="";
				var docdue
				if(document.getElementById("ChkDOCDUE").checked==true)
				docdue= document.getElementById("dtpDOCDUEcmbMM").value + "/" +document.getElementById("dtpDOCDUEcmbDD").value + "/" + document.getElementById("dtpDOCDUEcmbYY").value; else docdue="";
				var ddepp
				if(document.getElementById("ChkDDEPP").checked==true)
				ddepp= document.getElementById("dtpDDEPPcmbMM").value + "/" +document.getElementById("dtpDDEPPcmbDD").value + "/" + document.getElementById("dtpDDEPPcmbYY").value; else ddepp="";
				var doce
				if(document.getElementById("ChkDOCE").checked==true)
				doce= document.getElementById("dtpDOCEcmbMM").value + "/" + document.getElementById("dtpDOCEcmbDD").value + "/" +document.getElementById("dtpDOCEcmbYY").value; else doce="";
				var ceupto
				if(document.getElementById("ChkCEUPTO").checked==true)
				ceupto= document.getElementById("dtpCEUPTOcmbMM").value + "/" + document.getElementById("dtpCEUPTOcmbDD").value + "/" +document.getElementById("dtpCEUPTOcmbYY").value; else ceupto="";
				var ddr
				if(document.getElementById("ChkDDR").checked==true)
				ddr= document.getElementById("dtpDDRcmbMM").value + "/" + document.getElementById("dtpDDRcmbDD").value + "/" +document.getElementById("dtpDDRcmbYY").value; else ddr="";
				var doj
				if(document.getElementById("ChkDOJ").checked==true)
				doj= document.getElementById("dtpDOJcmbMM").value + "/" + document.getElementById("dtpDOJcmbDD").value + "/" +document.getElementById("dtpDOJcmbYY").value; else doj="";
				
												
				
				
				//alert(document.getElementById("dtpDOCDUEcmbDD").value);
				//var dd=document.getElementById("dtpDOCDUEcmbDD").value + "/" + document.getElementById("dtpDOCDUEcmbMM").value + "/" + document.getElementById("dtpDOCDUEcmbYY").value
				//var dd=document.getElementById("DtpDDEPPcmbDD").value + "/" + document.getElementById("DtpDDEPPcmbMM").value + "/" + document.getElementById("dtpDtpDDEPPcmbYY").value
				
				//alert(dd);
			
				var save=EmpMast.Save_Record(ecode,fnam ,lnme, comp, sap ,jr, bt ,deg, dep, loc ,alo , plo , et , jn , ec , hm , man ,gr, cc , pro , reg, sec , div , cos , unt , ct , full,doc, ddedpe , docdue , ddepp , doce , ceupto , ddr , doj);
				//var response=EmpMast.Save_Record(dep);
				alert(save.value);	
				clear();		
			
			
			}
			
		function moveprevious()
			{
				var EmpCode;
				EmpCode=document.getElementById("TxtEmpCode").value;
				//alert('tesu');
				var response=EmpMast.GetPreviousEmpRec(EmpCode)
				EmpCode=response.value;
				document.getElementById("TxtEmpCode").value=EmpCode;
				//EmpMast.SetEmpCode(EmpCode);
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
				//EmpMast.SetEmpCode(EmpCode);
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
				//EmpMast.SetEmpCode(EmpCode);
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
								
							//Text box binding	
								document.getElementById("txtFName").value= chks(dt.Tables[0].Rows[0]["FNAME"]);
								document.getElementById("txtLName").value= chks(dt.Tables[0].Rows[0]['LNAME']);
								document.getElementById("TxtComp").value= chks(dt.Tables[0].Rows[0]['COMPANY']);
								document.getElementById("TxtSalAdminPlan").value= chks(dt.Tables[0].Rows[0]['LSA_PLAN']);
								document.getElementById("TxtJobRanking").value= chks(dt.Tables[0].Rows[0]['JOBRANK']);
								document.getElementById("TxtBTitle").value= chks(dt.Tables[0].Rows[0]['BUSS_TITLE']);
							//drop down bining which has a Test box	
								//document.getElementById("cmbDepartment").value= dt.Tables[0].Rows[0]['DEPT_CODE'];
								// document.getElementById("cmbDesignation").value= dt.Tables[0].Rows[0]['DSG_CODE'];
								document.getElementById("txtDesignation").value= chks(dt.Tables[0].Rows[0]['DSG_NAME']);
								//alert(document.getElementById("txtDesignation").toolTip);
								document.getElementById("txtDesignation").toolTip= chks(dt.Tables[0].Rows[0]['DSG_CODE']);
								document.getElementById("txtDepartment").value= chks(dt.Tables[0].Rows[0]['DEPT_NAME']);
								document.getElementById("txtDepartment").toolTip= chks(dt.Tables[0].Rows[0]['DEPT_CODE']);
								document.getElementById("txtLocation").value= chks(dt.Tables[0].Rows[0]['LOC_NAME']);
								document.getElementById("txtLocation").toolTip= chks(dt.Tables[0].Rows[0]['LOC_CODE']);
							    document.getElementById("txtALocation").value= chks(dt.Tables[0].Rows[0]['ADMINLOCATION']);
							    document.getElementById("txtALocation").toolTip= chks(dt.Tables[0].Rows[0]['ALCODE']);
								document.getElementById("txtPLocation").value= chks(dt.Tables[0].Rows[0]['PAYLOCATION']);
								document.getElementById("txtPLocation").toolTip= chks(dt.Tables[0].Rows[0]['PLCODE']);
								document.getElementById("txtEmpType").value= chks(dt.Tables[0].Rows[0]['TYPE_NAME']);
								document.getElementById("txtEmpType").toolTip= chks(dt.Tables[0].Rows[0]['TYPE_CODE']);
								document.getElementById("txtJobName").value= chks(dt.Tables[0].Rows[0]['JOB_DESC']);
								document.getElementById("txtJobName").toolTip= chks(dt.Tables[0].Rows[0]['JOB_CODE']);
								document.getElementById("txtEmpClass").value= chks(dt.Tables[0].Rows[0]['EMP_DESC']);
								document.getElementById("txtEmpClass").toolTip= chks(dt.Tables[0].Rows[0]['EMP_CLASS']);
								document.getElementById("txtHrMngr").value= chks(dt.Tables[0].Rows[0]['HRMNGR_NAME']);
								document.getElementById("txtHrMngr").toolTip= chks(dt.Tables[0].Rows[0]['HR_MNGR']);
								document.getElementById("txtManager").value= chks(dt.Tables[0].Rows[0]['MNGR_NAME']);
								document.getElementById("txtManager").toolTip= chks(dt.Tables[0].Rows[0]['MNGR_CODE']);
								document.getElementById("txtGrade").value= chks(dt.Tables[0].Rows[0]['GRD_NAME']);
								document.getElementById("txtGrade").toolTip= chks(dt.Tables[0].Rows[0]['GRD_CODE']);
								document.getElementById("txtCostCenter").value= chks(dt.Tables[0].Rows[0]['COST_NAME']);
								document.getElementById("txtCostCenter").toolTip= chks(dt.Tables[0].Rows[0]['COST_CODE']);
								document.getElementById("txtProcess").value= chks(dt.Tables[0].Rows[0]['PROC_NAME']);
								document.getElementById("txtProcess").toolTip= chks(dt.Tables[0].Rows[0]['PROC_Code']);
								document.getElementById("txtRegion").value= chks(dt.Tables[0].Rows[0]['REGN_NAME']);
								document.getElementById("txtRegion").toolTip= chks(dt.Tables[0].Rows[0]['REGN_CODE']);
								document.getElementById("txtSection").value= chks(dt.Tables[0].Rows[0]['SECT_NAME']);
								document.getElementById("txtSection").toolTip= chks(dt.Tables[0].Rows[0]['SECT_CODE']);
								document.getElementById("txtDivision").value= chks(dt.Tables[0].Rows[0]['DIVI_NAME']);
								document.getElementById("txtDivision").toolTip= chks(dt.Tables[0].Rows[0]['DIVI_CODE']);
							
								document.getElementById("txtCosting").value= chks(dt.Tables[0].Rows[0]['COSTTYPE_DESC']);
								document.getElementById("txtCosting").toolTip= chks(dt.Tables[0].Rows[0]['COSTTYPE_CODE']);
								document.getElementById("txtUnit").value= chks(dt.Tables[0].Rows[0]['UNIT_DESC']);
								document.getElementById("txtUnit").toolTip= chks(dt.Tables[0].Rows[0]['Unit_Code']);
								document.getElementById("txtContType").value= chks(dt.Tables[0].Rows[0]['CONT_DESC']);
								document.getElementById("txtContType").toolTip= chks(dt.Tables[0].Rows[0]['CONT_TYPE']);
							//Drop down binding
								document.getElementById("cmbFull").value= chks(dt.Tables[0].Rows[0]['FULL_PART']);
								
								
								
								
							//dtp binding	
								showdate('DOC','DOC'); 
								showdate('DDEDPE','SERV_DATE');
								showdate('DOCDUE','DOC_DUE');
								showdate('DDEPP','DDOEPP');
								showdate('DOCE','DOCE');
								showdate('CEUPTO','DOCExUPTO');
								showdate('DDR','DDOR'); 
								showdate('DOJ','DOJ'); 
								
								
								
								//if(dt.Tables[0].Rows[0]['SERV_DATE']!=null) showdate('DDEDPE','SERV_DATE'); else  hidedate('DDEDPE','SERV_DATE');						  
								//if(dt.Tables[0].Rows[0]['DOC_DUE']!=null) showdate('DOCDUE','DOC_DUE'); else  hidedate('DOCDUE','DOC_DUE');
								//if(dt.Tables[0].Rows[0]['DDOEPP']!=null) showdate('DDEPP','DDOEPP'); else  hidedate('DDEPP','DDOEPP');	
								//if(dt.Tables[0].Rows[0]['DOCE']!=null) showdate('DOCE','DOCE'); else  hidedate('DOCE','DOCE');	
								//if(dt.Tables[0].Rows[0]['DOCExUPTO']!=null) showdate('CEUPTO','DOCExUPTO'); else  hidedate('CEUPTO','DOCExUPTO');	
								//if(dt.Tables[0].Rows[0]['DDOR']!=null) showdate('DDR','DDOR'); else  hidedate('DDR','DDOR');				  					  
																			 
							}
					
						else
						
							{
							clear();												
							}
					  					    
					}	
			}
				
		function clear()
			{
								document.getElementById("txtFName").value= "";
								document.getElementById("txtDepartment").value= "";
								document.getElementById("txtDesignation").value= "";
								document.getElementById("txtLocation").value= "";
								document.getElementById("txtALocation").value= "";
								document.getElementById("txtPLocation").value= "";
								document.getElementById("txtEmpType").value= "";
								document.getElementById("txtJobName").value= "";
								document.getElementById("txtEmpClass").value= "";
								document.getElementById("txtHrMngr").value= "";
								document.getElementById("txtManager").value= "";
								document.getElementById("txtLName").value= "";
								document.getElementById("txtGrade").value= "";
								document.getElementById("txtCostCenter").value= "";
								document.getElementById("txtProcess").value= "";
								document.getElementById("txtRegion").value= "";
								document.getElementById("txtSection").value= "";
								document.getElementById("txtDivision").value= "";
								document.getElementById("cmbFull").value= "";
								document.getElementById("txtCosting").value= "";
								document.getElementById("txtUnit").value= "";
								document.getElementById("txtContType").value= "";
								document.getElementById("TxtComp").value= "";
								document.getElementById("TxtSalAdminPlan").value= "";
								document.getElementById("TxtJobRanking").value= "";
								document.getElementById("TxtBTitle").value= "";
								//hide the date field (DTP)
															
								var ckdt= new Array('DOC','DDEDPE','DOCDUE','DDEPP','DOCE','CEUPTO','DDR','DOJ');
								var cd=new Date();
								for(var i=0; i<ckdt.length; i++)
								{
									document.getElementById("Chk"+ckdt[i]).checked = false;
									document.getElementById("dtp"+ckdt[i]+'cmbDD').disabled = true;
									document.getElementById("dtp"+ckdt[i]+'cmbYY').disabled= true;
									document.getElementById("dtp"+ckdt[i]+'cmbMM').disabled = true; 
									document.getElementById("dtp"+ckdt[i]+'cmbDD').value =cd.getDate();
									document.getElementById("dtp"+ckdt[i]+'cmbYY').value=cd.getFullYear();
									document.getElementById("dtp"+ckdt[i]+'cmbMM').value=cd.getMonth()+1;         
																		
								}
														
								//alert('Either your Employee Code is wrong or Employee Code field is blank.');
			
			}		
		function selChanged(ctrcom,ctrtxt)
			{
				//document.getElementById(document.getElementById("HidCombName").value
		
				// var cmb =document.getElementById("cmbDesignation");
				var cmb =document.getElementById(ctrcom);
				document.getElementById (ctrtxt).style.display ='';
				document.getElementById (ctrcom).style.display ='none';
				//document.getElementById (ctrtxt).value=document.getElementById (ctrcom).options[document.getElementById (ctrcom).selectedindex].text;
				document.getElementById(ctrtxt).value=cmb.options[cmb.selectedIndex].text;
				document.getElementById(ctrtxt).toolTip=cmb.value;
				
				 
			}
		 	
		function combobind(ctrlcombo,ctrltxt,tabName,txtField,valField)
			{
				document.getElementById (ctrlcombo).style.display ='';
				document.getElementById (ctrltxt).style.display ='none';
				//document.getElementById (ctrltxt).value ="";
				var response=EmpMast.GetCombovalue(tabName,txtField,valField)			 
				EmpMast. ComboBind(fillcombo);
				document.getElementById("HidCombName").value=ctrlcombo;
				//document.getElementById("HidTxtField").value=txtField;
				//document.getElementById("HidValField").value=valField;
						 		
			 	 
			}
			 
		function addOption(selectObject,optionText,optionValue) 
			{
				
				var optionObject = new Option(optionText, optionValue);
				var optionRank = selectObject.options.length;
				//alert(optionRank);
				selectObject.options[optionRank]=optionObject;
				
			}

		
					 			 
		function fillcombo(response)
			{
				
				var sText;
				var response=EmpMast.ComboBind();
				var dt = response.value;
												
				//alert(document.getElementById("HidCombName").value);
				//document.getElementById(document.getElementById("HidCombName")).value='none';
				//if(dt.Tables[0].Rows.length==0)	{  alert("Sorry! There is no Record");	}
				if(dt != null && typeof(dt) == "object")
					{					
						
						sText = response.request.responseText;								
						var a;
						var fi = sText.indexOf("'Rows':[")+8;
						var si = sText.length;
						//var si = sText.indexOf("}",fi);
						var sRow = sText.slice(fi+1,si);
						aRow = sRow.split(",");
						for(var i=0; i<aRow.length; i++)
							{
								aRow[i]=aRow[i].slice(1,aRow[i].indexOf("'",2));
								a= a + aRow[i];							
							}
						
						var cmb = document.getElementById(document.getElementById("HidCombName").value);
						cmb.options.length=0;
						
						addOption(document.getElementById(document.getElementById("HidCombName").value)," "," " );						
						for(var i=0; i +1 <aRow.length ; i++)
							{	
								if(dt.Tables[0].Rows[i] != null)
									{
										addOption(document.getElementById(document.getElementById("HidCombName").value),dt.Tables[0].Rows[i][dt.Tables[1].Rows[0]['TF']],dt.Tables[0].Rows[i][dt.Tables[1].Rows[0]['VF']]);
									}
								
							}
							
					}	
				else
					{				
						//alert("Sorry! There is no record.");
					}
			 
			 
			}
			 
		
		</script>

<script language=vbscript>		
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
<body bottomMargin=0 leftMargin=0 topMargin=5 onload=combohide() rightMargin=0 MS_POSITIONING="GridLayout" ;>
<form id=Form1 method=post runat="server">
			<!--#include file=MenuBars.aspx -->
<asp:Label id=Label7 style="Z-INDEX: 103; LEFT: 176px; POSITION: absolute; TOP: 0px" runat="server" Width="112px" Font-Names="Book Antiqua">is Mandatory</asp:Label>
<asp:Label id=Label8 style="Z-INDEX: 104; LEFT: 152px; POSITION: absolute; TOP: 0px" runat="server" ForeColor="Red" Width="8px">*</asp:Label>
<asp:Label id=Label6 style="Z-INDEX: 102; LEFT: 0px; POSITION: absolute; TOP: 0px" runat="server" Width="136px" Font-Names="Book Antiqua">Field Indicated by </asp:Label><BR><BR>
<table height=30 cellSpacing=0 cellPadding=0 width=790 border=0>
  <tr vAlign=bottom>
    <td width=110>
      <table cellSpacing=0 cellPadding=0 width="100%" border=0 
      >
        <tr>
          <td width=10 bgColor=#666666><IMG alt="" src="Images/SplitLeft.gif" border=0 ></td>
          <td style="COLOR: white" align=center bgColor=#666666 
          ><b>Official Info</b></td>
          <td width=10 bgColor=#666666><IMG alt="" src="Images/rightcurve.gif" border=0 ></td></tr></table></td>
    <td width=120>
      <table cellSpacing=0 cellPadding=0 width="100%" border=0 
      >
        <tr>
          <td width=10 bgColor=#cecbce><IMG alt="" src="Images/leftoverlap.gif" border=0 ></td>
          <td style="COLOR: #003366" align=center bgColor=#cecbce 
          ><A href="GeneralInfo.aspx?SrNo=63" >General Info</A></td>
          <td width=10 bgColor=#cecbce><IMG alt="" src="Images/rightcurve.gif" border=0 ></td></tr></table></td>
    <td width=100>
      <table cellSpacing=0 cellPadding=0 width="100%" border=0 
      >
        <tr>
          <td width=10 bgColor=#cecbce><IMG alt="" src="Images/leftoverlap.gif" border=0 ></td>
          <td style="COLOR: #003366" align=center bgColor=#cecbce 
          ><A href="Compensation.aspx?SrNo=64" >Compensation</A></td>
          <td width=10 bgColor=#cecbce><IMG alt="" src="Images/rightcurve.gif" border=0 ></td></tr></table></td>
    <td width=100>
      <table cellSpacing=0 cellPadding=0 width="100%" border=0 
      >
        <tr>
          <td width=10 bgColor=#cecbce><IMG alt="" src="Images/leftoverlap.gif" border=0 ></td>
          <td style="COLOR: #003366" align=center bgColor=#cecbce 
          ><A href="History.aspx?SrNo=65" >History</A></td>
          <td width=10 bgColor=#cecbce><IMG alt="" src="Images/rightcurve.gif" border=0 ></td></tr></table></td>
    <td width=100>
      <table cellSpacing=0 cellPadding=0 width="100%" border=0 
      >
        <tr>
          <td width=10 bgColor=#cecbce><IMG alt="" src="Images/leftoverlap.gif" border=0 ></td>
          <td style="COLOR: #003366" align=center bgColor=#cecbce 
          ><A href="Progression.aspx?SrNo=66" >Progression</A></td>
          <td width=10 bgColor=#cecbce><IMG alt="" src="Images/rightcurve.gif" border=0 ></td></tr></table></td>
    <td width=100>
      <table cellSpacing=0 cellPadding=0 width="100%" border=0 
      >
        <tr>
          <td width=10 bgColor=#cecbce><IMG alt="" src="Images/leftoverlap.gif" border=0 ></td>
          <td style="COLOR: #003366" align=center bgColor=#cecbce 
          ><A href="Others.aspx?SrNo=67" >Others</A></td>
          <td width=10 bgColor=#cecbce><IMG alt="" src="Images/rightcurve.gif" border=0 ></td></tr></table></td>
    <td width=100>
      <table cellSpacing=0 cellPadding=0 width="100%" border=0 
      >
        <tr>
          <td width=10 bgColor=#cecbce><IMG alt="" src="Images/leftoverlap.gif" border=0 ></td>
          <td style="COLOR: #003366" align=center bgColor=#cecbce 
          ><A href="Family.aspx?SrNo=68" >Family</A></td>
          <td width=10 bgColor=#cecbce><IMG alt="" src="Images/rightcurve.gif" border=0 ></td></tr></table></td>
    <td width=100>
      <table cellSpacing=0 cellPadding=0 width="100%" border=0 
      >
        <tr>
          <td width=10 bgColor=#cecbce><IMG alt="" src="Images/leftoverlap.gif" border=0 ></td>
          <td style="COLOR: #003366" align=center bgColor=#cecbce 
          ><A href="EmpSkills.aspx?SrNo=69" >Skills</A></td>
          <td width=10 bgColor=#cecbce><IMG alt="" src="Images/rightcurve.gif" border=0 ></td></tr></table></td>
    <td width=100>
      <table cellSpacing=0 cellPadding=0 width="100%" border=0 
      >
        <tr>
          <td width=10 bgColor=#cecbce><IMG alt="" src="Images/leftoverlap.gif" border=0 ></td>
          <td style="COLOR: #003366" align=center bgColor=#cecbce 
          ><A href="EmpNominees.aspx?SrNo=70" >Nominee</A></td>
          <td width=10 bgColor=#cecbce><IMG alt="" src="Images/rightcurve.gif" border=0 ></td></tr></table></td></tr>
  <tr>
    <td colSpan=9>
      <table borderColor=black cellSpacing=0 cellPadding=0 rules=none 
      width="100%" border=1 frame=box>
        <tr>
          <td colSpan=2><asp:label id=LblMsg runat="server" Width="100%" ForeColor="Red" Font-Size="11px"></asp:label></td></tr>
        <tr>
          <td style="WIDTH: 444px" width=444>
            <table cellSpacing=0 cellPadding=0 width="100%" border=0 
            >
              <tr>
                <td width="40%"><asp:label id=lblCode runat="server" Width="40px">&nbsp;Code</asp:label><FONT 
                  color=#ff3300>*</FONT></td>
                <td align=left width="60%"><INPUT style="FONT-WEIGHT: bold; COLOR: #0033ff; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: #ffffff; BORDER-BOTTOM-STYLE: none" onclick=movefirst(); type=button value="<<">&nbsp; 
                  &nbsp; <INPUT style="FONT-WEIGHT: bold; COLOR: #0033ff; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: #ffffff; BORDER-BOTTOM-STYLE: none" onclick=moveprevious(); type=button value="<">&nbsp; 
                  <INPUT class=TextBox id=TxtEmpCode onblur=Disp(); 
                  style="WIDTH: 134px; HEIGHT: 20px" type=text size=17 
                  name=Text1 runat="server">&nbsp; <INPUT style="FONT-WEIGHT: bold; COLOR: #0033ff; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: #ffffff; BORDER-BOTTOM-STYLE: none" onclick=movenext(); type=button value=">">&nbsp;&nbsp;<INPUT style="FONT-WEIGHT: bold; COLOR: #0033ff; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: #ffffff; BORDER-BOTTOM-STYLE: none" onclick=movelast(); type=button value=">>"></td></tr></table></td>
          <td vAlign=top width="50%"><asp:textbox id=txtEM_CD runat="server" Width="0px" ForeColor="#003366" AutoPostBack="True" CssClass="TextBox" BackColor="#E1E4EB" Font-Bold="True"></asp:textbox></td></tr>
        <tr>
          <td style="WIDTH: 444px; HEIGHT: 423px" width=444>
            <table cellSpacing=0 cellPadding=2 width="100%" border=0 
            >
              <TBODY>
              <tr>
                <td width="40%" style="HEIGHT: 23px"><asp:label id=lblFName runat="server" Width="64px">First Name</asp:label><FONT 
                  color=#ff3300>*</FONT></td>
                <td width="60%" style="HEIGHT: 23px"><asp:textbox id=txtFName tabIndex=1 runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"></asp:textbox></td></tr>
              <tr>
                <td style="HEIGHT: 7px" width="40%"><asp:label id=lblDesignation runat="server" Width="100%">Designation</asp:label></td>
                <td style="HEIGHT: 7px" width="60%"><asp:dropdownlist id=cmbDesignation runat="server" Width="200px"></asp:dropdownlist><asp:textbox id=txtDesignation tabIndex=1 runat="server" Width="200px" ForeColor="#003366" CssClass="TextBox"></asp:textbox><INPUT class=input id=inDesignation onclick="combobind('cmbDesignation','txtDesignation','DsgMast','Dsg_Name','DSG_CODE');" type=button value=&amp;></td></tr>
              <tr>
                <td><asp:label id=lblDepartment runat="server" Width="100%">Department</asp:label></td>
                <td><asp:dropdownlist id=cmbDepartment runat="server" Width="200px"></asp:dropdownlist><asp:textbox id=txtDepartment tabIndex=1 runat="server" Width="200px" ForeColor="#003366" CssClass="TextBox"></asp:textbox><INPUT class=input id=intDepartment onclick="combobind('cmbDepartment','txtDepartment','DeptMast','Dept_Name','Dept_Code');" type=button size=10 value=&amp;></td></tr>
              <tr>
                <td><asp:label id=lblLocation runat="server" Width="100%">Work Location</asp:label></td>
                <td><asp:dropdownlist id=cmbLocation runat="server" Width="200px"></asp:dropdownlist><asp:textbox id=txtLocation tabIndex=1 runat="server" Width="200px" ForeColor="#003366" CssClass="TextBox"></asp:textbox><INPUT class=input id=intLocation onclick="combobind('cmbLocation','txtLocation','LocMast where flag =1 or flag=2','Loc_Name','Loc_Code');" type=button size=10 value=&amp; name=Button1></td></tr>
              <tr>
                <td style="HEIGHT: 7px"><asp:label id=lblALocation runat="server" Width="100%">Admin Location</asp:label></td>
                <td style="HEIGHT: 7px"><asp:dropdownlist id=cmbALocation runat="server" Width="200px"></asp:dropdownlist><asp:textbox id=txtALocation tabIndex=1 runat="server" Width="200px" ForeColor="#003366" CssClass="TextBox"></asp:textbox><INPUT class=input id=intALocation onclick="combobind('cmbALocation','txtALocation','LocMast where flag =1 or flag=3','Loc_Name','Loc_Code');" type=button value=&amp;></td></tr>
              <tr>
                <td style="HEIGHT: 24px"><asp:label id=lblPLocation runat="server" Width="100%">Pay Location</asp:label></td>
                <td style="HEIGHT: 24px"><asp:dropdownlist id=cmbPLocation runat="server" Width="200px"></asp:dropdownlist><asp:textbox id=txtPLocation tabIndex=1 runat="server" Width="200px" ForeColor="#003366" CssClass="TextBox"></asp:textbox><INPUT class=input id=intPLocation onclick="combobind('cmbPLocation','txtPLocation','LocMast where flag =1 or flag=4','Loc_Name','Loc_Code');" type=button value=&amp;></td></tr>
              <tr>
                <td style="HEIGHT: 21px"><asp:label id=lblEmpType runat="server" Width="100%">Regular/Temporary</asp:label></td>
                <td style="HEIGHT: 21px"><asp:dropdownlist id=cmbEmpType runat="server" Width="200px"></asp:dropdownlist><asp:textbox id=txtEmpType tabIndex=1 runat="server" Width="200px" ForeColor="#003366" CssClass="TextBox"></asp:textbox><INPUT class=input id=intEmpType onclick="combobind('cmbEmpType','txtEmpType','EmpType','Type_Name','Type_Code');" type=button value=&amp;></td></tr>
              <tr>
                <td style="HEIGHT: 25px"><asp:label id=LblJOBCode runat="server" Width="100%">Job Code</asp:label></td>
                <td style="HEIGHT: 25px"><asp:dropdownlist id=cmbJobName runat="server" Width="200px"></asp:dropdownlist><asp:textbox id=txtJobName tabIndex=1 runat="server" Width="200px" ForeColor="#003366" CssClass="TextBox"></asp:textbox><INPUT class=input id=Button5 onclick="combobind('cmbJobName','txtJobName','JobMast','job_Desc','job_Code');" type=button value=&amp;></td></tr>
              <tr>
                <td style="HEIGHT: 22px"><asp:label id=LblSup runat="server" Width="100%">Employee Class</asp:label></td>
                <td style="HEIGHT: 22px"><asp:dropdownlist id=cmbEmpClass runat="server" Width="200px"></asp:dropdownlist><asp:textbox id=txtEmpClass tabIndex=1 runat="server" Width="200px" ForeColor="#003366" CssClass="TextBox"></asp:textbox><INPUT class=input id=Button18 onclick="combobind('cmbEmpClass','txtEmpClass','EMPCLASSMAST','EMP_DESC','EMP_CLASS');" type=button value=&amp; name=inDesignation></td></tr>
              <tr>
                <td style="HEIGHT: 1px"><asp:label id=LblHRMANAGER runat="server" Width="100%">HR Manager</asp:label></td>
                <td style="HEIGHT: 1px"><asp:dropdownlist id=cmbHrMngr runat="server" Width="200px"></asp:dropdownlist><asp:textbox id=txtHrMngr tabIndex=1 runat="server" Width="200px" ForeColor="#003366" CssClass="TextBox"></asp:textbox><INPUT class=input id=Button6 onclick="combobind('cmbHrMngr','txtHrMngr','HrdMastQry','Emp_Name','Emp_Code');" type=button value=&amp; name=inDesignation></td></tr>
              <tr>
                <td><asp:label id=lblManager runat="server" Width="100%">Line/Supervisor ID</asp:label></td>
                <td><asp:dropdownlist id=cmbManager runat="server" Width="200px"></asp:dropdownlist><asp:textbox id=txtManager tabIndex=1 runat="server" Width="200px" ForeColor="#003366" CssClass="TextBox"></asp:textbox><INPUT class=input id=Button7 onclick="combobind('cmbManager','txtManager','HrdMastQry','Emp_Name','Emp_Code');" type=button value=&amp; name=inDesignation></td></tr>
              <tr>
                <td><asp:label id=LblComp runat="server" Width="100%">Company</asp:label></td>
                <td><asp:textbox id=TxtComp Width="100%" ForeColor="#003366" CssClass="TextBox" Runat="server"></asp:textbox></td></tr>
              <tr>
                <td width="40%"><asp:label id=LblLocalSal runat="server" Width="100%">Local Salary Admin Plan.</asp:label></td>
                <td width="60%"><asp:textbox id=TxtSalAdminPlan runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"></asp:textbox></td></tr>
              <tr>
                <td><asp:label id=LblDOC runat="server" Width="100%" ToolTip=" Date of Confirmation">Confirmed (Yes/No)</asp:label></td>
                <td><input id=ChkDOC onclick=Val(this.id) 
                  type=checkbox name=ChkDOC runat="server"><cc1:dtpcombo id=DtpDOC runat="server" Width="150px" ToolTip="Date of Confirmation" DateValue="2005-08-30" Enabled="False"></cc1:dtpcombo></td></tr>
              <tr>
                <td><asp:label id=LblBTEDate runat="server" Width="100%"> Service Date</asp:label></td>
												<!--<td><input id="ChkEntry" onclick="Val(this.id)" type="Checkbox" name="ChkENTRY" runat="server"><cc1:dtpcombo id="DtpEDate" runat="server" Width="150px" ToolTip="Business Title Entry Date "
														Enabled="False" DateValue="2005-08-30"></cc1:dtpcombo></td>-->
                <td><input id=ChkDDEDPE 
                  onclick=Val(this.id) type=checkbox name=ChkDDEDPE 
                   runat="server"> <cc1:dtpcombo id=dtpDDEDPE runat="server" Width="150px" ToolTip="Service Date" DateValue="2005-08-30" Enabled="False"></cc1:dtpcombo></td></tr>
              <tr>
                <td style="HEIGHT: 23px"><asp:label id=lblDOCDue runat="server" Width="100%" ToolTip=" Due Date of Confirmation">Due Date of Confirmation</asp:label></td>
                <td style="HEIGHT: 23px"><input 
                  id=ChkDOCDUE onclick=Val(this.id) type=checkbox name=ChkDOCDUE 
                   runat="server"><cc1:dtpcombo id=dtpDOCDUE runat="server" Width="150px" ToolTip="Due Date of Confirmation" DateValue="2005-08-30" Enabled="False"></cc1:dtpcombo></td></tr>
              <tr>
                <td><asp:label id=Label2 runat="server" Width="100%">Probation Period Extended Upto</asp:label></td>
                <td><input id=ChkDDEPP 
                  onclick=Val(this.id) type=checkbox name=ChkDDEPP 
                   runat="server"><cc1:dtpcombo id=dtpDDEPP runat="server" Width="150px" ToolTip="Due Date of Extension of Probation Period" DateValue="2005-08-30" Enabled="False"></cc1:dtpcombo></td></tr></TBODY></table></td>
          <td style="HEIGHT: 423px" width="50%">
            <table cellSpacing=0 cellPadding=2 width="100%" border=0 
            >
              <tr>
                <td style="HEIGHT: 26px" width="40%"><asp:label id=lblLName runat="server" Width="64px">Last Name</asp:label><FONT 
                  color=#ff3300></FONT></td>
                <td style="HEIGHT: 26px" width="60%"><asp:textbox id=txtLName tabIndex=2 runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"></asp:textbox></td></tr>
              <tr>
                <td style="HEIGHT: 5px" width="40%"><asp:label id=lblGrade runat="server" Width="100%">Grade / Level</asp:label></td>
                <td style="HEIGHT: 5px" width="60%"><asp:dropdownlist id=cmbGrade runat="server" Width="200px"></asp:dropdownlist><asp:textbox id=txtGrade tabIndex=1 runat="server" Width="200px" ForeColor="#003366" CssClass="TextBox"></asp:textbox><INPUT class=input id=Button8 onclick="combobind('cmbGrade','txtGrade','GrdMast','Grd_Name','Grd_Code');" type=button value=&amp;></td></tr>
              <tr>
                <td style="HEIGHT: 7px"><asp:label id=lblCostCenter runat="server" Width="100%">Cost Center</asp:label></td>
                <td style="HEIGHT: 7px"><asp:dropdownlist id=cmbCostCenter runat="server" Width="200px"></asp:dropdownlist><asp:textbox id=txtCostCenter tabIndex=1 runat="server" Width="200px" ForeColor="#003366" CssClass="TextBox"></asp:textbox><INPUT class=input id=Button9 onclick="combobind('cmbCostCenter','txtCostCenter','CostMast','Cost_Name','Cost_Code');" type=button value=&amp;></td></tr>
              <tr>
                <td style="HEIGHT: 7px"><asp:label id=lblProcess runat="server" Width="100%"> Sub-Department</asp:label></td>
                <td style="HEIGHT: 7px"><asp:dropdownlist id=cmbProcess runat="server" Width="200px"></asp:dropdownlist><asp:textbox id=txtProcess tabIndex=1 runat="server" Width="200px" ForeColor="#003366" CssClass="TextBox"></asp:textbox><INPUT class=input id=Button10 onclick="combobind('cmbProcess','txtProcess','ProcMast','Proc_Name','Proc_Code');" type=button value=&amp; name=inDesignation></td></tr>
              <tr>
                <td><asp:label id=lblRegion runat="server" Width="100%">Region</asp:label></td>
                <td><asp:dropdownlist id=cmbRegion runat="server" Width="200px"></asp:dropdownlist><asp:textbox id=txtRegion tabIndex=1 runat="server" Width="200px" ForeColor="#003366" CssClass="TextBox"></asp:textbox><INPUT class=input id=Button11 onclick="combobind('cmbRegion','txtRegion','RegnMast','Regn_Name','Regn_Code');" type=button value=&amp; name=inDesignation></td></tr>
              <tr>
                <td style="HEIGHT: 22px"><asp:label id=lblSection runat="server" Width="100%">Section</asp:label></td>
                <td style="HEIGHT: 22px"><asp:dropdownlist id=cmbSection runat="server" Width="200px"></asp:dropdownlist><asp:textbox id=txtSection tabIndex=1 runat="server" Width="200px" ForeColor="#003366" CssClass="TextBox"></asp:textbox><INPUT class=input id=intSection onclick="combobind('cmbSection','txtSection','SectMast','Sect_Name','Sect_Code');" type=button value=&amp; name=inDesignation></td></tr>
              <tr>
                <td style="HEIGHT: 19px"><asp:label id=lblDivision runat="server" Width="100%"> Division</asp:label></td>
                <td style="HEIGHT: 19px"><asp:dropdownlist id=cmbDivision runat="server" Width="200px"></asp:dropdownlist><asp:textbox id=txtDivision tabIndex=1 runat="server" Width="200px" ForeColor="#003366" CssClass="TextBox"></asp:textbox><INPUT class=input id=intDivision onclick="combobind('cmbDivision','txtDivision','DiviMast','Divi_Name','Divi_Code');" type=button value=&amp; name=inDesignation></td></tr>
              <tr>
                <td style="HEIGHT: 12px"><asp:label id=LblFull runat="server" Width="100%">Full/Part</asp:label></td>
                <td style="HEIGHT: 12px"><asp:dropdownlist id=cmbFull runat="server" Width="200px">
<asp:ListItem Value="1" Selected="True">Full-Time</asp:ListItem>
<asp:ListItem Value="2">Part-Time</asp:ListItem>
												</asp:dropdownlist></td></tr>
              <tr>
                <td><asp:label id=LblCost runat="server" Width="100%">Costing</asp:label></td>
                <td><asp:dropdownlist id=cmbCosting runat="server" Width="200px"></asp:dropdownlist><asp:textbox id=txtCosting tabIndex=1 runat="server" Width="200px" ForeColor="#003366" CssClass="TextBox"></asp:textbox><INPUT class=input id=intCosting onclick="combobind('cmbCosting','txtCosting','COSTTYPEMAST','CostType_Desc','CostType_Code');" type=button value=&amp;></td></tr>
              <tr>
                <td style="HEIGHT: 21px"><asp:label id=LblBUnit runat="server" Width="100%">Business Unit</asp:label></td>
                <td style="HEIGHT: 21px"><asp:dropdownlist id=cmbUnit runat="server" Width="200px"></asp:dropdownlist><asp:textbox id=txtUnit tabIndex=1 runat="server" Width="200px" ForeColor="#003366" CssClass="TextBox"></asp:textbox><INPUT class=input id=intUnit onclick="combobind('cmbUnit','txtUnit','BUnitMast','Unit_DESC','Unit_Code');" type=button value=&amp;></td></tr>
              <tr>
                <td style="HEIGHT: 19px"><asp:label id=LblCont runat="server" Width="100%">Contract Type</asp:label></td>
                <td style="HEIGHT: 19px"><asp:dropdownlist id=cmbContType runat="server" Width="200px"></asp:dropdownlist><asp:textbox id=txtContType tabIndex=1 runat="server" Width="200px" ForeColor="#003366" CssClass="TextBox"></asp:textbox><INPUT class=input id=intContType onclick="combobind('cmbContType','txtContType','ContractType','CONT_Desc','CONT_Type');" type=button value=&amp; name=inDesignation></td></tr>
              <tr>
                <td width="40%"><asp:label id=LblJobRank runat="server" Width="100%">Job Ranking</asp:label></td>
                <td width="60%"><asp:textbox id=TxtJobRanking runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"></asp:textbox></td></tr>
              <tr>
                <td width="40%"><asp:label id=LblBTitle runat="server" Width="100%">Business Title</asp:label></td>
                <td width="60%"><asp:textbox id=TxtBTitle runat="server" Width="100%" ForeColor="#003366" CssClass="TextBox"></asp:textbox></td></tr>
              <tr>
                <td><asp:label id=lblDOJ runat="server" Width="88px">Date of Joining</asp:label><FONT 
                  color=#ff3300>*</FONT></td>
                <td><INPUT id=ChkDOJ onclick=Val(this.id) 
                  type=checkbox name=ChkDOJ runat="server"><cc1:dtpcombo id=dtpDOJ runat="server" Width="150px" ToolTip="Date Of Joining" DateValue="2005-08-30" Enabled="False"></cc1:dtpcombo></td></tr>
              <tr>
                <td><asp:label id=Label1 runat="server" Width="100%">Contract End Date</asp:label></td>
                <td><input id=ChkDOCE 
                  onclick=Val(this.id) type=checkbox name=ChkDOCE 
                   runat="server"><cc1:dtpcombo id=dtpDOCE runat="server" Width="150px" ToolTip="Contract End Date" DateValue="2005-08-30" Enabled="False"></cc1:dtpcombo></td></tr>
              <tr>
                <td><asp:label id=LblCEUPTO runat="server" Width="100%">Contract Extended Upto</asp:label></td>
                <td><input id=ChkCEUPTO 
                  onclick=Val(this.id) type=checkbox name=ChkCEUPTO 
                   runat="server"><cc1:dtpcombo id=dtpCEUPTO runat="server" Width="150px" ToolTip="Contract Extended Upto" DateValue="2005-08-30" Enabled="False"></cc1:dtpcombo></td></tr>
              <tr>
                <td><asp:label id=Label3 runat="server" Width="100%">Date of Regularisation</asp:label></td>
                <td><input id=ChkDDR onclick=Val(this.id) 
                  type=checkbox name=ChkDDR runat="server"><cc1:dtpcombo id=dtpDDR runat="server" Width="150px" ToolTip="Contract Extended Upto" DateValue="2005-08-30" Enabled="False"></cc1:dtpcombo></td></tr>
										<!--<tr>
											<td><asp:label id="Label5" runat="server" Width="100%" Visible =False >Due Date of Extension of Training Period</asp:label></td>
											<td><input id="ChkDDETP" onclick="Val(this.id)" type="checkbox" name="ChkDDETP" runat="server"><cc1:dtpcombo id="DtpDDETP" runat="server" Width="150px" ToolTip="Due Date of Extension of Training Period"
													Enabled="False"  visible="False" DateValue="2005-08-30"></cc1:dtpcombo></td>
										</tr>--></table></td></tr>
        <TR height=15>
          <TD 
          style="MARGIN-LEFT: 15px; MARGIN-RIGHT: 15px; BORDER-BOTTOM: #993300 thin groove; HEIGHT: 7px" 
          colSpan=2></TD></TR>
        <tr>
          <td align=right colSpan=2><INPUT style="WIDTH: 60px; HEIGHT: 24px" onclick=save(); type=button value=Save name=cmdSave>&nbsp; 
<asp:button id=cmdClose accessKey=C runat="server" Width="75px" Text="Close"></asp:button></td></tr></table></td></tr>
  <tr>
    <td align=center colSpan=9></td></tr></table><INPUT 
id=HidCombName style="Z-INDEX: 105; LEFT: 24px; POSITION: absolute; TOP: 800px" 
type=hidden name=HidCombName>
    <asp:textbox id=TxtRights style="Z-INDEX: 101; LEFT: 224px; POSITION: absolute; TOP: 832px" runat="server" Width="0px"></asp:textbox>
    </form>
<script language=javascript>
		
			var response=EmpMast.SetCurrentEmpCode();
			var dt = response.value;
			if (dt != null)
			{
				document.getElementById("TxtEmpCode").value = dt;
				//document.getElementById("TxtEmpCode").blur();
				Disp();
			}
			
		</script>
</TR></TBODY></TABLE>
	</body>
</HTML>
