
/*** 
This is the menu creation code - place it right after you body tag
Feel free to add this to a stand-alone js file and link it to your page.
**/

//Menu object creation
oCMenu=new makeCM("oCMenu") //Making the menu object. Argument: menuname

//Menu properties   
oCMenu.pxBetween=0
oCMenu.fromLeft=0
oCMenu.fromTop=0  
oCMenu.rows=1 
oCMenu.menuPlacement="Left"
                                                             
oCMenu.offlineRoot="" 
oCMenu.onlineRoot="" 
//oCMenu.offlineRoot="" 
//oCMenu.onlineRoot="" 
oCMenu.resizeCheck=1 
oCMenu.wait=100 
oCMenu.fillImg="cm_fill.gif"
oCMenu.zIndex=0



//Level properties - ALL properties have to be spesified in level 0
oCMenu.level[0]=new cm_makeLevel() //Add this for each new level
oCMenu.level[0].width=100
oCMenu.level[0].height=20 
oCMenu.level[0].regClass="clLevel0"
oCMenu.level[0].overClass="clLevel0over"
oCMenu.level[0].borderX=0
oCMenu.level[0].borderY=0
oCMenu.level[0].borderClass="clLevel0border"
oCMenu.level[0].offsetX=0
oCMenu.level[0].offsetY=0
oCMenu.level[0].rows=0
oCMenu.level[0].arrow=0
oCMenu.level[0].arrowWidth=0
oCMenu.level[0].arrowHeight=0
oCMenu.level[0].align="bottom"


//EXAMPLE SUB LEVEL[1] PROPERTIES - You have to specify the properties you want different from LEVEL[0] - If you want all items to look the same just remove this
oCMenu.level[1]=new cm_makeLevel() //Add this for each new level (adding one to the number)
oCMenu.level[1].width=oCMenu.level[0].width-2
oCMenu.level[1].height=19
oCMenu.level[1].regClass="clLevel1"
oCMenu.level[1].overClass="clLevel1over"
oCMenu.level[1].borderX=0
oCMenu.level[1].borderY=1
oCMenu.level[1].align="right" 
oCMenu.level[1].offsetX=-(oCMenu.level[0].width-2)/2+20
oCMenu.level[1].offsetY=0
oCMenu.level[1].borderClass="clLevel1border"




/******************************************
Menu item creation:
myCoolMenu.makeMenu(name, parent_name, text, link, target, width, height, regImage, overImage, regClass, overClass , align, rows, nolink, onclick, onmouseover, onmouseout) 
*************************************/

oCMenu.makeMenu('top1','','&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Employee','','','120','22')
    oCMenu.makeMenu('sub10','top1','&nbsp;Explorer','Empexplorer.aspx','','116')
oCMenu.makeMenu('top2','','Leaves','','','120','22')
    oCMenu.makeMenu('sub21','top2','&nbsp;Leave Application','LeaveApplication.aspx','','116')
    oCMenu.makeMenu('sub22','top2','&nbsp;Leave Status','LeaveStatus.aspx','','116')
    oCMenu.makeMenu('sub23','top2','&nbsp;Leave Sanction','LeavSanction.aspx','','116')
oCMenu.makeMenu('top3','','Training','','','120','22')
    oCMenu.makeMenu('sub31','top3','&nbsp;Training Request','TrainingRequest.aspx','','116')
    oCMenu.makeMenu('sub32','top3','&nbsp;Training Needs','TrainingNeeds.aspx','','116')    
oCMenu.makeMenu('top4','','Recruitment','','','120','22')
    oCMenu.makeMenu('sub41','top4','&nbsp;Job Requisition','JobRequisition.aspx','','116')    
oCMenu.makeMenu('top5','','Masters','','','120','22')
    oCMenu.makeMenu('sub51','top5','&nbsp;Qualification','GenMast.aspx?TblName=QualMast&Desc=Qualification','','116')
    oCMenu.makeMenu('sub52','top5','&nbsp;University','GenMast.aspx?TblName=UnivMast&Desc=University','','116')
oCMenu.makeMenu('top6','','Download Policies','Policies.aspx','','120','22')    
oCMenu.makeMenu('top7','','System','','','120','22')    
	oCMenu.makeMenu('sub71','top7','&nbsp;Org Chart','OrgChart.aspx','','116')
	oCMenu.makeMenu('sub72','top7','&nbsp;Org Chart','RepChnl.aspx','','116')    
oCMenu.makeMenu('top8','','Logout','Logout.aspx','','120','22')
//Leave this line - it constructs the menu
oCMenu.construct()		
