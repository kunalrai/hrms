<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TestForm.aspx.vb" Inherits="eHRMS.Net.TestForm"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>TestForm</title>
		<script language="javascript">
		function addOption(selectObject,optionText,optionValue) 
		{
			var optionObject = new Option(optionText, optionValue)
			var optionRank = selectObject.options.length
			selectObject.options[optionRank]=optionObject
		}
		
		function test()
		{
			addOption(document.getElementById("DropDownList2"),document.getElementById("DropDownList1").value,document.getElementById("DropDownList1").value);
			
		}

		function fill()
		{	
			TestForm.GetList(CallBack);		
		}
		function CallBack(response)
			{
				
				var sText;
				
				var response=TestForm.GetList();
				var dt = response.value;

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
					for(var i=0; i +1 <aRow.length ; i++)
					{	
							if(dt.Tables[0].Rows[i] != null)
							{
								addOption(document.getElementById("DropDownList1"),dt.Tables[0].Rows[i]['Emp_Code'],dt.Tables[0].Rows[i]['Emp_Code']);
							}
					}
				}	
				else
				{
					alert("yes");
				}
			}

		</script>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:dropdownlist id="DropDownList1" onblur="test();" style="Z-INDEX: 100; LEFT: 64px; POSITION: absolute; TOP: 32px"
				runat="server" Width="184px" Height="16px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist224" onblur="test();" style="Z-INDEX: 327; LEFT: 536px; POSITION: absolute; TOP: 592px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist223" onblur="test();" style="Z-INDEX: 131; LEFT: 808px; POSITION: absolute; TOP: 168px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist222" onblur="test();" style="Z-INDEX: 127; LEFT: 808px; POSITION: absolute; TOP: 144px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist221" onblur="test();" style="Z-INDEX: 124; LEFT: 808px; POSITION: absolute; TOP: 120px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist220" onblur="test();" style="Z-INDEX: 119; LEFT: 808px; POSITION: absolute; TOP: 96px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist219" onblur="test();" style="Z-INDEX: 116; LEFT: 808px; POSITION: absolute; TOP: 72px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist218" onblur="test();" style="Z-INDEX: 112; LEFT: 808px; POSITION: absolute; TOP: 48px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist217" onblur="test();" style="Z-INDEX: 106; LEFT: 808px; POSITION: absolute; TOP: 24px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist216" onblur="test();" style="Z-INDEX: 137; LEFT: 808px; POSITION: absolute; TOP: 216px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist215" onblur="test();" style="Z-INDEX: 134; LEFT: 808px; POSITION: absolute; TOP: 192px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist214" onblur="test();" style="Z-INDEX: 130; LEFT: 808px; POSITION: absolute; TOP: 168px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist213" onblur="test();" style="Z-INDEX: 125; LEFT: 808px; POSITION: absolute; TOP: 144px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist212" onblur="test();" style="Z-INDEX: 121; LEFT: 808px; POSITION: absolute; TOP: 120px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist211" onblur="test();" style="Z-INDEX: 117; LEFT: 808px; POSITION: absolute; TOP: 96px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist210" onblur="test();" style="Z-INDEX: 114; LEFT: 808px; POSITION: absolute; TOP: 72px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist209" onblur="test();" style="Z-INDEX: 109; LEFT: 808px; POSITION: absolute; TOP: 48px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist208" onblur="test();" style="Z-INDEX: 108; LEFT: 808px; POSITION: absolute; TOP: 24px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist207" onblur="test();" style="Z-INDEX: 326; LEFT: 576px; POSITION: absolute; TOP: 536px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist206" onblur="test();" style="Z-INDEX: 148; LEFT: 808px; POSITION: absolute; TOP: 168px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist205" onblur="test();" style="Z-INDEX: 145; LEFT: 808px; POSITION: absolute; TOP: 144px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist204" onblur="test();" style="Z-INDEX: 141; LEFT: 808px; POSITION: absolute; TOP: 120px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist203" onblur="test();" style="Z-INDEX: 136; LEFT: 808px; POSITION: absolute; TOP: 96px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist202" onblur="test();" style="Z-INDEX: 126; LEFT: 808px; POSITION: absolute; TOP: 72px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist201" onblur="test();" style="Z-INDEX: 120; LEFT: 808px; POSITION: absolute; TOP: 48px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist200" onblur="test();" style="Z-INDEX: 107; LEFT: 808px; POSITION: absolute; TOP: 24px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist199" onblur="test();" style="Z-INDEX: 154; LEFT: 808px; POSITION: absolute; TOP: 216px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist198" onblur="test();" style="Z-INDEX: 150; LEFT: 808px; POSITION: absolute; TOP: 192px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist197" onblur="test();" style="Z-INDEX: 146; LEFT: 808px; POSITION: absolute; TOP: 168px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist196" onblur="test();" style="Z-INDEX: 142; LEFT: 808px; POSITION: absolute; TOP: 144px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist195" onblur="test();" style="Z-INDEX: 139; LEFT: 808px; POSITION: absolute; TOP: 120px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist194" onblur="test();" style="Z-INDEX: 129; LEFT: 808px; POSITION: absolute; TOP: 96px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist193" onblur="test();" style="Z-INDEX: 122; LEFT: 808px; POSITION: absolute; TOP: 72px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist192" onblur="test();" style="Z-INDEX: 115; LEFT: 808px; POSITION: absolute; TOP: 48px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist191" onblur="test();" style="Z-INDEX: 111; LEFT: 808px; POSITION: absolute; TOP: 24px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist190" onblur="test();" style="Z-INDEX: 325; LEFT: 624px; POSITION: absolute; TOP: 488px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist189" onblur="test();" style="Z-INDEX: 165; LEFT: 808px; POSITION: absolute; TOP: 168px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist188" onblur="test();" style="Z-INDEX: 161; LEFT: 808px; POSITION: absolute; TOP: 144px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist187" onblur="test();" style="Z-INDEX: 156; LEFT: 808px; POSITION: absolute; TOP: 120px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist186" onblur="test();" style="Z-INDEX: 152; LEFT: 808px; POSITION: absolute; TOP: 96px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist185" onblur="test();" style="Z-INDEX: 144; LEFT: 808px; POSITION: absolute; TOP: 72px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist184" onblur="test();" style="Z-INDEX: 133; LEFT: 808px; POSITION: absolute; TOP: 48px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist183" onblur="test();" style="Z-INDEX: 110; LEFT: 808px; POSITION: absolute; TOP: 24px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist182" onblur="test();" style="Z-INDEX: 170; LEFT: 808px; POSITION: absolute; TOP: 216px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist181" onblur="test();" style="Z-INDEX: 166; LEFT: 808px; POSITION: absolute; TOP: 192px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist180" onblur="test();" style="Z-INDEX: 163; LEFT: 808px; POSITION: absolute; TOP: 168px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist179" onblur="test();" style="Z-INDEX: 159; LEFT: 808px; POSITION: absolute; TOP: 144px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist178" onblur="test();" style="Z-INDEX: 153; LEFT: 808px; POSITION: absolute; TOP: 120px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist177" onblur="test();" style="Z-INDEX: 147; LEFT: 808px; POSITION: absolute; TOP: 96px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist176" onblur="test();" style="Z-INDEX: 138; LEFT: 808px; POSITION: absolute; TOP: 72px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist175" onblur="test();" style="Z-INDEX: 128; LEFT: 808px; POSITION: absolute; TOP: 48px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist174" onblur="test();" style="Z-INDEX: 118; LEFT: 808px; POSITION: absolute; TOP: 24px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist173" onblur="test();" style="Z-INDEX: 324; LEFT: 632px; POSITION: absolute; TOP: 432px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist172" onblur="test();" style="Z-INDEX: 180; LEFT: 808px; POSITION: absolute; TOP: 168px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist171" onblur="test();" style="Z-INDEX: 176; LEFT: 808px; POSITION: absolute; TOP: 144px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist170" onblur="test();" style="Z-INDEX: 173; LEFT: 808px; POSITION: absolute; TOP: 120px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist169" onblur="test();" style="Z-INDEX: 167; LEFT: 808px; POSITION: absolute; TOP: 96px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist168" onblur="test();" style="Z-INDEX: 160; LEFT: 808px; POSITION: absolute; TOP: 72px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist167" onblur="test();" style="Z-INDEX: 151; LEFT: 808px; POSITION: absolute; TOP: 48px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist166" onblur="test();" style="Z-INDEX: 113; LEFT: 808px; POSITION: absolute; TOP: 24px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist165" onblur="test();" style="Z-INDEX: 186; LEFT: 808px; POSITION: absolute; TOP: 216px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist164" onblur="test();" style="Z-INDEX: 183; LEFT: 808px; POSITION: absolute; TOP: 192px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist163" onblur="test();" style="Z-INDEX: 179; LEFT: 808px; POSITION: absolute; TOP: 168px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist162" onblur="test();" style="Z-INDEX: 174; LEFT: 808px; POSITION: absolute; TOP: 144px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist161" onblur="test();" style="Z-INDEX: 171; LEFT: 808px; POSITION: absolute; TOP: 120px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist160" onblur="test();" style="Z-INDEX: 162; LEFT: 808px; POSITION: absolute; TOP: 96px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist159" onblur="test();" style="Z-INDEX: 155; LEFT: 808px; POSITION: absolute; TOP: 72px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist158" onblur="test();" style="Z-INDEX: 140; LEFT: 808px; POSITION: absolute; TOP: 48px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist157" onblur="test();" style="Z-INDEX: 123; LEFT: 808px; POSITION: absolute; TOP: 24px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist156" onblur="test();" style="Z-INDEX: 323; LEFT: 648px; POSITION: absolute; TOP: 392px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist155" onblur="test();" style="Z-INDEX: 197; LEFT: 808px; POSITION: absolute; TOP: 168px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist154" onblur="test();" style="Z-INDEX: 193; LEFT: 808px; POSITION: absolute; TOP: 144px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist153" onblur="test();" style="Z-INDEX: 188; LEFT: 808px; POSITION: absolute; TOP: 120px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist152" onblur="test();" style="Z-INDEX: 182; LEFT: 808px; POSITION: absolute; TOP: 96px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist151" onblur="test();" style="Z-INDEX: 177; LEFT: 808px; POSITION: absolute; TOP: 72px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist150" onblur="test();" style="Z-INDEX: 168; LEFT: 808px; POSITION: absolute; TOP: 48px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist149" onblur="test();" style="Z-INDEX: 132; LEFT: 808px; POSITION: absolute; TOP: 24px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist148" onblur="test();" style="Z-INDEX: 202; LEFT: 808px; POSITION: absolute; TOP: 216px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist147" onblur="test();" style="Z-INDEX: 199; LEFT: 808px; POSITION: absolute; TOP: 192px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist146" onblur="test();" style="Z-INDEX: 195; LEFT: 808px; POSITION: absolute; TOP: 168px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist145" onblur="test();" style="Z-INDEX: 191; LEFT: 808px; POSITION: absolute; TOP: 144px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist144" onblur="test();" style="Z-INDEX: 185; LEFT: 808px; POSITION: absolute; TOP: 120px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist143" onblur="test();" style="Z-INDEX: 178; LEFT: 808px; POSITION: absolute; TOP: 96px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist142" onblur="test();" style="Z-INDEX: 169; LEFT: 808px; POSITION: absolute; TOP: 72px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist141" onblur="test();" style="Z-INDEX: 158; LEFT: 808px; POSITION: absolute; TOP: 48px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist140" onblur="test();" style="Z-INDEX: 149; LEFT: 808px; POSITION: absolute; TOP: 24px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist139" onblur="test();" style="Z-INDEX: 322; LEFT: 632px; POSITION: absolute; TOP: 336px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist138" onblur="test();" style="Z-INDEX: 212; LEFT: 808px; POSITION: absolute; TOP: 168px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist137" onblur="test();" style="Z-INDEX: 209; LEFT: 808px; POSITION: absolute; TOP: 144px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist136" onblur="test();" style="Z-INDEX: 204; LEFT: 808px; POSITION: absolute; TOP: 120px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist135" onblur="test();" style="Z-INDEX: 200; LEFT: 808px; POSITION: absolute; TOP: 96px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist134" onblur="test();" style="Z-INDEX: 190; LEFT: 808px; POSITION: absolute; TOP: 72px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist133" onblur="test();" style="Z-INDEX: 184; LEFT: 808px; POSITION: absolute; TOP: 48px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist132" onblur="test();" style="Z-INDEX: 143; LEFT: 808px; POSITION: absolute; TOP: 24px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist131" onblur="test();" style="Z-INDEX: 218; LEFT: 808px; POSITION: absolute; TOP: 216px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist130" onblur="test();" style="Z-INDEX: 215; LEFT: 808px; POSITION: absolute; TOP: 192px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist129" onblur="test();" style="Z-INDEX: 211; LEFT: 808px; POSITION: absolute; TOP: 168px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist128" onblur="test();" style="Z-INDEX: 207; LEFT: 808px; POSITION: absolute; TOP: 144px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist127" onblur="test();" style="Z-INDEX: 203; LEFT: 808px; POSITION: absolute; TOP: 120px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist126" onblur="test();" style="Z-INDEX: 194; LEFT: 808px; POSITION: absolute; TOP: 96px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist125" onblur="test();" style="Z-INDEX: 187; LEFT: 808px; POSITION: absolute; TOP: 72px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist124" onblur="test();" style="Z-INDEX: 172; LEFT: 808px; POSITION: absolute; TOP: 48px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist123" onblur="test();" style="Z-INDEX: 164; LEFT: 808px; POSITION: absolute; TOP: 24px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist122" onblur="test();" style="Z-INDEX: 321; LEFT: 344px; POSITION: absolute; TOP: 152px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist121" onblur="test();" style="Z-INDEX: 228; LEFT: 808px; POSITION: absolute; TOP: 168px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist120" onblur="test();" style="Z-INDEX: 225; LEFT: 808px; POSITION: absolute; TOP: 144px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist119" onblur="test();" style="Z-INDEX: 220; LEFT: 808px; POSITION: absolute; TOP: 120px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist118" onblur="test();" style="Z-INDEX: 214; LEFT: 808px; POSITION: absolute; TOP: 96px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist117" onblur="test();" style="Z-INDEX: 208; LEFT: 808px; POSITION: absolute; TOP: 72px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist116" onblur="test();" style="Z-INDEX: 196; LEFT: 808px; POSITION: absolute; TOP: 48px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist115" onblur="test();" style="Z-INDEX: 157; LEFT: 808px; POSITION: absolute; TOP: 24px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist114" onblur="test();" style="Z-INDEX: 234; LEFT: 808px; POSITION: absolute; TOP: 216px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist113" onblur="test();" style="Z-INDEX: 230; LEFT: 808px; POSITION: absolute; TOP: 192px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist112" onblur="test();" style="Z-INDEX: 227; LEFT: 808px; POSITION: absolute; TOP: 168px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist111" onblur="test();" style="Z-INDEX: 223; LEFT: 808px; POSITION: absolute; TOP: 144px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist110" onblur="test();" style="Z-INDEX: 217; LEFT: 808px; POSITION: absolute; TOP: 120px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist109" onblur="test();" style="Z-INDEX: 210; LEFT: 808px; POSITION: absolute; TOP: 96px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist108" onblur="test();" style="Z-INDEX: 205; LEFT: 808px; POSITION: absolute; TOP: 72px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist107" onblur="test();" style="Z-INDEX: 192; LEFT: 808px; POSITION: absolute; TOP: 48px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist106" onblur="test();" style="Z-INDEX: 181; LEFT: 808px; POSITION: absolute; TOP: 24px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist105" onblur="test();" style="Z-INDEX: 320; LEFT: 592px; POSITION: absolute; TOP: 32px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist104" onblur="test();" style="Z-INDEX: 245; LEFT: 808px; POSITION: absolute; TOP: 168px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist103" onblur="test();" style="Z-INDEX: 241; LEFT: 808px; POSITION: absolute; TOP: 144px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist102" onblur="test();" style="Z-INDEX: 236; LEFT: 808px; POSITION: absolute; TOP: 120px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist101" onblur="test();" style="Z-INDEX: 231; LEFT: 808px; POSITION: absolute; TOP: 96px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist100" onblur="test();" style="Z-INDEX: 224; LEFT: 808px; POSITION: absolute; TOP: 72px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist99" onblur="test();" style="Z-INDEX: 216; LEFT: 808px; POSITION: absolute; TOP: 48px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist98" onblur="test();" style="Z-INDEX: 175; LEFT: 808px; POSITION: absolute; TOP: 24px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist97" onblur="test();" style="Z-INDEX: 250; LEFT: 808px; POSITION: absolute; TOP: 216px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist96" onblur="test();" style="Z-INDEX: 247; LEFT: 808px; POSITION: absolute; TOP: 192px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist95" onblur="test();" style="Z-INDEX: 243; LEFT: 808px; POSITION: absolute; TOP: 168px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist94" onblur="test();" style="Z-INDEX: 239; LEFT: 808px; POSITION: absolute; TOP: 144px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist93" onblur="test();" style="Z-INDEX: 235; LEFT: 808px; POSITION: absolute; TOP: 120px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist92" onblur="test();" style="Z-INDEX: 226; LEFT: 808px; POSITION: absolute; TOP: 96px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist91" onblur="test();" style="Z-INDEX: 219; LEFT: 808px; POSITION: absolute; TOP: 72px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist90" onblur="test();" style="Z-INDEX: 201; LEFT: 808px; POSITION: absolute; TOP: 48px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist89" onblur="test();" style="Z-INDEX: 189; LEFT: 808px; POSITION: absolute; TOP: 24px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist88" onblur="test();" style="Z-INDEX: 319; LEFT: 368px; POSITION: absolute; TOP: 16px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist87" onblur="test();" style="Z-INDEX: 260; LEFT: 808px; POSITION: absolute; TOP: 168px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist86" onblur="test();" style="Z-INDEX: 256; LEFT: 808px; POSITION: absolute; TOP: 144px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist85" onblur="test();" style="Z-INDEX: 253; LEFT: 808px; POSITION: absolute; TOP: 120px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist84" onblur="test();" style="Z-INDEX: 246; LEFT: 808px; POSITION: absolute; TOP: 96px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist83" onblur="test();" style="Z-INDEX: 240; LEFT: 808px; POSITION: absolute; TOP: 72px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist82" onblur="test();" style="Z-INDEX: 232; LEFT: 808px; POSITION: absolute; TOP: 48px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist81" onblur="test();" style="Z-INDEX: 198; LEFT: 808px; POSITION: absolute; TOP: 24px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist80" onblur="test();" style="Z-INDEX: 265; LEFT: 808px; POSITION: absolute; TOP: 216px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist79" onblur="test();" style="Z-INDEX: 262; LEFT: 808px; POSITION: absolute; TOP: 192px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist78" onblur="test();" style="Z-INDEX: 259; LEFT: 808px; POSITION: absolute; TOP: 168px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist77" onblur="test();" style="Z-INDEX: 254; LEFT: 808px; POSITION: absolute; TOP: 144px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist76" onblur="test();" style="Z-INDEX: 251; LEFT: 808px; POSITION: absolute; TOP: 120px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist75" onblur="test();" style="Z-INDEX: 244; LEFT: 808px; POSITION: absolute; TOP: 96px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist74" onblur="test();" style="Z-INDEX: 233; LEFT: 808px; POSITION: absolute; TOP: 72px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist73" onblur="test();" style="Z-INDEX: 221; LEFT: 808px; POSITION: absolute; TOP: 48px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist72" onblur="test();" style="Z-INDEX: 206; LEFT: 808px; POSITION: absolute; TOP: 24px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist71" onblur="test();" style="Z-INDEX: 318; LEFT: 608px; POSITION: absolute; TOP: 168px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist70" onblur="test();" style="Z-INDEX: 277; LEFT: 808px; POSITION: absolute; TOP: 168px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist69" onblur="test();" style="Z-INDEX: 272; LEFT: 808px; POSITION: absolute; TOP: 144px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist68" onblur="test();" style="Z-INDEX: 269; LEFT: 808px; POSITION: absolute; TOP: 120px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist67" onblur="test();" style="Z-INDEX: 263; LEFT: 808px; POSITION: absolute; TOP: 96px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist66" onblur="test();" style="Z-INDEX: 255; LEFT: 808px; POSITION: absolute; TOP: 72px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist65" onblur="test();" style="Z-INDEX: 248; LEFT: 808px; POSITION: absolute; TOP: 48px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist64" onblur="test();" style="Z-INDEX: 213; LEFT: 808px; POSITION: absolute; TOP: 24px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist63" onblur="test();" style="Z-INDEX: 282; LEFT: 808px; POSITION: absolute; TOP: 216px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist62" onblur="test();" style="Z-INDEX: 279; LEFT: 808px; POSITION: absolute; TOP: 192px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist61" onblur="test();" style="Z-INDEX: 274; LEFT: 808px; POSITION: absolute; TOP: 168px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist60" onblur="test();" style="Z-INDEX: 271; LEFT: 808px; POSITION: absolute; TOP: 144px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist59" onblur="test();" style="Z-INDEX: 267; LEFT: 808px; POSITION: absolute; TOP: 120px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist58" onblur="test();" style="Z-INDEX: 261; LEFT: 808px; POSITION: absolute; TOP: 96px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist57" onblur="test();" style="Z-INDEX: 249; LEFT: 808px; POSITION: absolute; TOP: 72px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist56" onblur="test();" style="Z-INDEX: 238; LEFT: 808px; POSITION: absolute; TOP: 48px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist55" onblur="test();" style="Z-INDEX: 222; LEFT: 808px; POSITION: absolute; TOP: 24px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist54" onblur="test();" style="Z-INDEX: 317; LEFT: 608px; POSITION: absolute; TOP: 136px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist53" onblur="test();" style="Z-INDEX: 293; LEFT: 808px; POSITION: absolute; TOP: 168px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist52" onblur="test();" style="Z-INDEX: 288; LEFT: 808px; POSITION: absolute; TOP: 144px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist51" onblur="test();" style="Z-INDEX: 285; LEFT: 808px; POSITION: absolute; TOP: 120px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist50" onblur="test();" style="Z-INDEX: 278; LEFT: 808px; POSITION: absolute; TOP: 96px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist49" onblur="test();" style="Z-INDEX: 273; LEFT: 808px; POSITION: absolute; TOP: 72px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist48" onblur="test();" style="Z-INDEX: 258; LEFT: 808px; POSITION: absolute; TOP: 48px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist47" onblur="test();" style="Z-INDEX: 229; LEFT: 808px; POSITION: absolute; TOP: 24px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist46" onblur="test();" style="Z-INDEX: 298; LEFT: 808px; POSITION: absolute; TOP: 216px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist45" onblur="test();" style="Z-INDEX: 295; LEFT: 808px; POSITION: absolute; TOP: 192px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist44" onblur="test();" style="Z-INDEX: 290; LEFT: 808px; POSITION: absolute; TOP: 168px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist43" onblur="test();" style="Z-INDEX: 286; LEFT: 808px; POSITION: absolute; TOP: 144px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist42" onblur="test();" style="Z-INDEX: 281; LEFT: 808px; POSITION: absolute; TOP: 120px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist41" onblur="test();" style="Z-INDEX: 276; LEFT: 808px; POSITION: absolute; TOP: 96px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist40" onblur="test();" style="Z-INDEX: 268; LEFT: 808px; POSITION: absolute; TOP: 72px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist39" onblur="test();" style="Z-INDEX: 257; LEFT: 808px; POSITION: absolute; TOP: 48px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist38" onblur="test();" style="Z-INDEX: 242; LEFT: 808px; POSITION: absolute; TOP: 24px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist37" onblur="test();" style="Z-INDEX: 316; LEFT: 608px; POSITION: absolute; TOP: 104px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist36" onblur="test();" style="Z-INDEX: 309; LEFT: 808px; POSITION: absolute; TOP: 168px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist35" onblur="test();" style="Z-INDEX: 305; LEFT: 808px; POSITION: absolute; TOP: 144px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist34" onblur="test();" style="Z-INDEX: 301; LEFT: 808px; POSITION: absolute; TOP: 120px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist33" onblur="test();" style="Z-INDEX: 296; LEFT: 808px; POSITION: absolute; TOP: 96px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist32" onblur="test();" style="Z-INDEX: 289; LEFT: 808px; POSITION: absolute; TOP: 72px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist31" onblur="test();" style="Z-INDEX: 275; LEFT: 808px; POSITION: absolute; TOP: 48px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist30" onblur="test();" style="Z-INDEX: 237; LEFT: 808px; POSITION: absolute; TOP: 24px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist29" onblur="test();" style="Z-INDEX: 313; LEFT: 808px; POSITION: absolute; TOP: 216px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist28" onblur="test();" style="Z-INDEX: 310; LEFT: 808px; POSITION: absolute; TOP: 192px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist27" onblur="test();" style="Z-INDEX: 307; LEFT: 808px; POSITION: absolute; TOP: 168px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist26" onblur="test();" style="Z-INDEX: 302; LEFT: 808px; POSITION: absolute; TOP: 144px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist25" onblur="test();" style="Z-INDEX: 299; LEFT: 808px; POSITION: absolute; TOP: 120px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist24" onblur="test();" style="Z-INDEX: 292; LEFT: 808px; POSITION: absolute; TOP: 96px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist23" onblur="test();" style="Z-INDEX: 284; LEFT: 808px; POSITION: absolute; TOP: 72px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist22" onblur="test();" style="Z-INDEX: 266; LEFT: 808px; POSITION: absolute; TOP: 48px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist21" onblur="test();" style="Z-INDEX: 252; LEFT: 808px; POSITION: absolute; TOP: 24px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist20" onblur="test();" style="Z-INDEX: 315; LEFT: 608px; POSITION: absolute; TOP: 72px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist19" onblur="test();" style="Z-INDEX: 312; LEFT: 808px; POSITION: absolute; TOP: 192px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist18" onblur="test();" style="Z-INDEX: 308; LEFT: 808px; POSITION: absolute; TOP: 168px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist17" onblur="test();" style="Z-INDEX: 304; LEFT: 808px; POSITION: absolute; TOP: 144px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist16" onblur="test();" style="Z-INDEX: 300; LEFT: 808px; POSITION: absolute; TOP: 120px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist15" onblur="test();" style="Z-INDEX: 294; LEFT: 808px; POSITION: absolute; TOP: 96px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist14" onblur="test();" style="Z-INDEX: 287; LEFT: 808px; POSITION: absolute; TOP: 72px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist13" onblur="test();" style="Z-INDEX: 280; LEFT: 808px; POSITION: absolute; TOP: 48px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist12" onblur="test();" style="Z-INDEX: 105; LEFT: 808px; POSITION: absolute; TOP: 24px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist11" onblur="test();" style="Z-INDEX: 314; LEFT: 808px; POSITION: absolute; TOP: 216px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist10" onblur="test();" style="Z-INDEX: 311; LEFT: 808px; POSITION: absolute; TOP: 192px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist9" onblur="test();" style="Z-INDEX: 306; LEFT: 808px; POSITION: absolute; TOP: 168px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist8" onblur="test();" style="Z-INDEX: 303; LEFT: 808px; POSITION: absolute; TOP: 144px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist7" onblur="test();" style="Z-INDEX: 297; LEFT: 808px; POSITION: absolute; TOP: 120px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist6" onblur="test();" style="Z-INDEX: 291; LEFT: 808px; POSITION: absolute; TOP: 96px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist5" onblur="test();" style="Z-INDEX: 283; LEFT: 808px; POSITION: absolute; TOP: 72px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist4" onblur="test();" style="Z-INDEX: 270; LEFT: 808px; POSITION: absolute; TOP: 48px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist>
			<asp:dropdownlist id="Dropdownlist3" onblur="test();" style="Z-INDEX: 264; LEFT: 808px; POSITION: absolute; TOP: 24px"
				runat="server" Height="16px" Width="184px"></asp:dropdownlist><INPUT style="Z-INDEX: 101; LEFT: 104px; WIDTH: 152px; POSITION: absolute; TOP: 280px; HEIGHT: 24px"
				onclick="fill();" type="button" value="Button"> <INPUT style="Z-INDEX: 102; LEFT: 376px; WIDTH: 168px; POSITION: absolute; TOP: 288px; HEIGHT: 24px"
				onclick="test();" type="button" value="Button">
			<asp:DropDownList OnBlur="test();" id="DropDownList2" style="Z-INDEX: 103; LEFT: 400px; POSITION: absolute; TOP: 192px"
				runat="server" Width="200px" Height="16px"></asp:DropDownList>
			<asp:Button id="Button1" style="Z-INDEX: 104; LEFT: 240px; POSITION: absolute; TOP: 400px" runat="server"
				Height="80px" Width="176px" Text="Button"></asp:Button></form>
	</body>
</HTML>
