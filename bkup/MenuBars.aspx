<% 
'				Dim StrJs As String
'				StrJs = " <Script languages=javascript > " & Chr(10) & _
'					" oCMenu=new makeCM(" & """oCMenu""" & ");" & Chr(10) & _
'					" oCMenu.pxBetween = 0" & Chr(10) & _
'					" oCMenu.fromLeft = 0" & Chr(10) & _
'					" oCMenu.fromTop = 0" & Chr(10) & _
'					" oCMenu.rows = 1" & Chr(10) & _
'					" oCMenu.menuPlacement = " & """Left""" & Chr(10) & _
'					" oCMenu.offlineRoot = """"" & Chr(10) & _
'					" oCMenu.onlineRoot = """"" & Chr(10) & _
'					" oCMenu.resizeCheck = 1" & Chr(10) & _
'					" oCMenu.wait = 100" & Chr(10) & _
'					" oCMenu.fillImg = " & """cm_fill.gif""" & Chr(10) & _
'					" oCMenu.zIndex = 0" & Chr(10) & _
'					" oCMenu.level[0]=new cm_makeLevel()" & Chr(10) & _
'					" oCMenu.level[0].width=100" & Chr(10) & _
'					" oCMenu.level[0].height=20" & Chr(10) & _
'					" oCMenu.level[0].regClass=" & """clLevel0""" & Chr(10) & _
'					" oCMenu.level[0].overClass=" & """clLevel0over""" & Chr(10) & _
'					" oCMenu.level[0].borderX=0" & Chr(10) & _
'					" oCMenu.level[0].borderY=0" & Chr(10) & _
'					" oCMenu.level[0].borderClass=" & """clLevel0border""" & Chr(10) & _
'					" oCMenu.level[0].offsetX=0" & Chr(10) & _
'					" oCMenu.level[0].offsetY=0" & Chr(10) & _
'					" oCMenu.level[0].rows=0" & Chr(10) & _
'					" oCMenu.level[0].arrow=0" & Chr(10) & _
'					" oCMenu.level[0].arrowWidth=0" & Chr(10) & _
'					" oCMenu.level[0].arrowHeight=0" & Chr(10) & _
'					" oCMenu.level[0].align=" & """bottom""" & Chr(10) & _
'					" oCMenu.level[1]=new cm_makeLevel()" & Chr(10) & _
'					" oCMenu.level[1].width=oCMenu.level[0].width-2" & Chr(10) & _
'					" oCMenu.level[1].height=19" & Chr(10) & _
'					" oCMenu.level[1].regClass=" & """clLevel1""" & Chr(10) & _
'					" oCMenu.level[1].overClass=" & """clLevel1over""" & Chr(10) & _
'					" oCMenu.level[1].borderX=1" & Chr(10) & _
'					" oCMenu.level[1].borderY=1" & Chr(10) & _
'					" oCMenu.level[1].align=" & """right""" & Chr(10) & _
'					" oCMenu.level[1].offsetX=-(oCMenu.level[0].width-2)/2+49" & Chr(10) & _
'					" oCMenu.level[1].offsetY=-1" & Chr(10) & _
'					" oCMenu.level[1].borderClass=" & """clLevel1border""" & Chr(10) 
					
					
			'" oCMenu.makeMenu('top1','','&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Employee','','','120','22');" & Chr(10) & _
			'" oCMenu.makeMenu('sub10','top1','&nbsp;Explorer','Empexplorer.aspx','','116');" & Chr(10) & _
			'" oCMenu.makeMenu('sub11','top1','&nbsp;Work History','WorkHistEntry.aspx','','116');" & Chr(10) & _		
					
'			Dim StrQuery, StrQry, StrCodes, Query As String, StrMod(), StrCod() As String, i, j, Count, Count1, k As Int16, Dt As New System.Data.DataTable, Dv As System.Data.DataView, DvTemp as System.Data.DataView
'			dim DtModules As New System.Data.DataTable
'
'			StrCodes = " Select isnull(Codes,'') from WebUsers Where UserID In (Select Group_ID From WebUsers Where UserID = '" & eHRMS.NET.MdlHRMS.Encrypt(Session("LoginUser").UserId, "+") & "')"
 '           StrQuery = " Select Modules from WebUsers Where UserID In (Select Group_ID From WebUsers Where UserID = '" & eHRMS.NET.MdlHRMS.Encrypt(Session("LoginUser").UserId, "+") & "')"
  '          StrQuery = Session("DalObj").ExecuteCommand(StrQuery, , DAL.DataLayer.ExecutionType.ExecuteScalar)
 '           StrCodes = Session("DalObj").ExecuteCommand(StrCodes, , DAL.DataLayer.ExecutionType.ExecuteScalar)
'
 '           StrMod = Split(StrQuery, ",")
  '          StrCod = Split(StrCodes, ",")
   '         
    '        If IsNothing(StrMod) and IsNothing(StrCod) Then Exit Sub
     '       
      '      Session("DalObj").GetSqlDataTable(DtModules, " Select * from WebModules Where MODULE_CODE In (" & StrQuery & ") Order By MODULE_GRP, OrderNo")
       '     Dv = New System.Data.DataView(DtModules)
        '    DvTemp = New System.Data.DataView(DtModules)
'                        
 '           Session("DalObj").GetSqlDataTable(Dt, " Select * from WebModules Where MODULE_GRP = '0' Order By Module_Code")
  '          
   '         Count = 1 
    '        For i=0 to Dt.Rows.Count-1
	'			Dv.RowFilter = " MODULE_GRP = '" & Dt.Rows(i).Item("MODULE_CODE") & "'"
	'			If Dv.Count > 0 Then
	'					StrJs = StrJs & " oCMenu.makeMenu('top" & Count & "','','" & Dt.Rows(i).Item("MODULE_DESC") & "','','','120','22')" & Chr(10)  
	'					Count1 = Count + 1000 
	'					For j = 0 to Dv.Count-1  
	'						DvTemp.RowFilter = " MODULE_GRP = '" & Dv.Item(j).Item("MODULE_CODE") & "'"
	'						If DvTemp.Count > 0 Then
	'							StrJs = StrJs & " oCMenu.makeMenu('sub" & Count & Dv.Item(j).Item("OrderNo") & "','top" & Count & "','&nbsp;" & Dv.Item(j).Item("MODULE_DESC") & "','" & Dv.Item(j).Item("MODULE_FORM") & "','','116')" & Chr(10)
	'							For k = 0 to DvTemp.Count-1  
	'								StrJs = StrJs & " oCMenu.makeMenu('sub" & Count1 & DvTemp.Item(k).Item("OrderNo") & "','sub" & Count & Dv.Item(j).Item("OrderNo") & "','&nbsp;" & DvTemp.Item(k).Item("MODULE_DESC") & "','" & DvTemp.Item(k).Item("MODULE_FORM") & "','','116')" & Chr(10)  										
	'							Next
	'							Count1 + = 1
	'						Else
	'							StrJs = StrJs & " oCMenu.makeMenu('sub" & Count & Dv.Item(j).Item("OrderNo") & "','top" & Count & "','&nbsp;" & Dv.Item(j).Item("MODULE_DESC") & "','" & Dv.Item(j).Item("MODULE_FORM") & "','','116')" & Chr(10)  	
	'						End If
	'					Next
	'					Count + = 1
	'			End IF				           	
     '       Next
'
'			StrJs = StrJs & "oCMenu.construct()" & Chr(10)
'			StrJs = StrJs & "</Script>"					
'			Response.write (strJs)
'			
			
			'Dim SqlStr, StrTemp as String, Dim Pos1, Pos2 as Int16
			
			'Pos1 = 0
			'StrTemp = ""
			'Do While True
			'	Pos1 = InStr(Pos1 + 1, StrCodes, "`R")
			'	If Pos = 0 Then Exit Do
			'	Pos2 = InStr(Pos1 + 1, StrCodes, "R`")
			'	If Pos2 = 0 Then Exit Do
			'	StrTemp = StrTemp & ",'" & Mid(StrCodes, Pos1 + 2, Pos2 - Pos1 - 2) & "'"
			'	Pos1 = Pos2
			'Loop
			'If StrTemp <> "" Then SqlStr = SqlStr & " AND Regn_Code IN (" & Mid(StrTemp, 2) & ")"
			
			'Response.Write(SqlStr)
			
			'Pos1 = 0
			'StrTemp = ""
			'Do While True
			'	Pos1 = InStr(Pos1 + 1, StrCodes, "`L")
			'	If Pos = 0 Then Exit Do
			'	Pos2 = InStr(Pos1 + 1, StrCodes, "L`")
			'	If Pos2 = 0 Then Exit Do
			'	StrTemp = StrTemp & ",'" & Mid(StrCodes, Pos1 + 2, Pos2 - Pos1 - 2) & "'"
			'	Pos1 = Pos2
			'Loop
			'SqlStr = SqlStr & IIf(StrTemp = "", "", " AND Loc_Code IN (" & Mid(StrTemp, 2) & ")")
		        
			'Pos1 = 0
			'StrTemp = ""
			'Do While True
			'	Pos1 = InStr(Pos1 + 1, StrCodes, "`V")
			'	If Pos = 0 Then Exit Do
			'	Pos2 = InStr(Pos1 + 1, StrCodes, "V`")
			'	If Pos2 = 0 Then Exit Do
			'	StrTemp = StrTemp & ",'" & Mid(StrCodes, Pos1 + 2, Pos2 - Pos1 - 2) & "'"
			'	Pos1 = Pos2
			'Loop
			'SqlStr = SqlStr & IIf(StrTemp = "", "", " AND Divi_Code IN (" & Mid(StrTemp, 2) & ")")
		    
			'Pos1 = 0
			'StrTemp = ""
			'Do While True
			'	Pos1 = InStr(Pos1 + 1, StrCodes, "`S")
			'	If Pos = 0 Then Exit Do
			'	Pos2 = InStr(Pos1 + 1, StrCodes, "S`")
			'	If Pos2 = 0 Then Exit Do
			'	StrTemp = StrTemp & ",'" & Mid(StrCodes, Pos1 + 2, Pos2 - Pos1 - 2) & "'"
			'	Pos1 = Pos2
			'Loop
			'SqlStr = SqlStr & IIf(StrTemp = "", "", " AND Sect_Code IN (" & Mid(StrTemp, 2) & ")")
		    
			'Pos1 = 0
			'StrTemp = ""
			'Do While True
			'	Pos1 = InStr(Pos1 + 1, StrCodes, "`P")
			'	If Pos = 0 Then Exit Do
			'	Pos2 = InStr(Pos1 + 1, StrCodes, "P`")
			'	If Pos2 = 0 Then Exit Do
			'	StrTemp = StrTemp & ",'" & Mid(StrCodes, Pos1 + 2, Pos2 - Pos1 - 2) & "'"
			'	Pos1 = Pos2
			'Loop
			'SqlStr = SqlStr & IIf(StrTemp = "", "", " AND Proc_Code IN (" & Mid(StrTemp, 2) & ")")
		    
			'Pos1 = 0
			'StrTemp = ""
			'Do While True
			'	Pos1 = InStr(Pos1 + 1, StrCodes, "`D")
			'	If Pos = 0 Then Exit Do
			'	Pos2 = InStr(Pos1 + 1, StrCodes, "D`")
			'	If Pos2 = 0 Then Exit Do
			'	StrTemp = StrTemp & ",'" & Mid(StrCodes, Pos1 + 2, Pos2 - Pos1 - 2) & "'"
			'	Pos1 = Pos2
			'Loop
			'SqlStr = SqlStr & IIf(StrTemp = "", "", " AND Dept_Code IN (" & Mid(StrTemp, 2) & ")")
		    
			'Pos1 = 0
			'StrTemp = ""
			'Do While True
			'	Pos1 = InStr(Pos1 + 1, StrCodes, "`C")
			'	If Pos = 0 Then Exit Do
			'	Pos2 = InStr(Pos1 + 1, StrCodes, "C`")
			'	If Pos2 = 0 Then Exit Do
			'	StrTemp = StrTemp & ",'" & Mid(StrCodes, Pos1 + 2, Pos2 - Pos1 - 2) & "'"
			'	Pos1 = Pos2
			'Loop
			'SqlStr = SqlStr & IIf(StrTemp = "", "", " AND Cost_Code IN (" & Mid(StrTemp, 2) & ")")
		    
			'Pos1 = 0
			'StrTemp = ""
			'Do While True
			'	Pos1 = InStr(Pos1 + 1, StrCodes, "`T")
			''	If Pos = 0 Then Exit Do
			'	Pos2 = InStr(Pos1 + 1, StrCodes, "T`")
			'	If Pos2 = 0 Then Exit Do
			''	StrTemp = StrTemp & ",'" & Mid(StrCodes, Pos1 + 2, Pos2 - Pos1 - 2) & "'"
			'	Pos1 = Pos2
			'Loop
			'SqlStr = SqlStr & IIf(StrTemp = "", "", " AND Type_Code IN (" & Mid(StrTemp, 2) & ")")
		    
			'Pos1 = 0
			'StrTemp = ""
			'Do While True
			'	Pos1 = InStr(Pos1 + 1, StrCodes, "`G")
			'	If Pos = 0 Then Exit Do
			'	Pos2 = InStr(Pos1 + 1, StrCodes, "G`")
			'	If Pos2 = 0 Then Exit Do
			'	StrTemp = StrTemp & ",'" & Mid(StrCodes, Pos1 + 2, Pos2 - Pos1 - 2) & "'"
			'	Pos1 = Pos2
			'Loop
			'SqlStr = SqlStr & IIf(StrTemp = "", "", " AND Grd_Code IN (" & Mid(StrTemp, 2) & ")")
		    
			'Pos1 = 0
			'StrTemp = ""
			'Do While True
			'	Pos1 = InStr(Pos1 + 1, StrCodes, "`J")
			'	If Pos = 0 Then Exit Do
			'	Pos2 = InStr(Pos1 + 1, StrCodes, "J`")
			'	If Pos2 = 0 Then Exit Do
			'	StrTemp = StrTemp & ",'" & Mid(StrCodes, Pos1 + 2, Pos2 - Pos1 - 2) & "'"
			'	Pos1 = Pos2
			'Loop
			'SqlStr = SqlStr & IIf(StrTemp = "", "", " AND Dsg_Code IN (" & Mid(StrTemp, 2) & ")")
		    
			'Pos1 = 0
			'StrTemp = ""
			'Do While True
			'	Pos1 = InStr(Pos1 + 1, StrCodes, "`E")
			'	If Pos = 0 Then Exit Do
			'	Pos2 = InStr(Pos1 + 1, StrCodes, "E`")
			'	If Pos2 = 0 Then Exit Do
			'	StrTemp = StrTemp & ",'" & Mid(StrCodes, Pos1 + 2, Pos2 - Pos1 - 2) & "'"
			'	Pos1 = Pos2
			'Loop
			'SqlStr = SqlStr & IIf(StrTemp = "", "", " AND Emp_Code IN (" & Mid(StrTemp, 2) & ")")
			
			'Response.Write(SqlStr)
		%>
