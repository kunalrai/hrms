			function ShowMenu(MenuType)
				{
				Menu = new String(MenuType)
				if (document.getElementById('Tbl' + Menu).style.display == "none")
					{
					document.getElementById('Tbl' + Menu).style.display = "block";
					document.getElementById('Img' + Menu).src = "images/Minus.gif";
					}
				else
					{
					document.getElementById('Tbl' + Menu).style.display = "none";
					document.getElementById('Img' + Menu).src = "images/Plus.gif";
					}
				}
				
			function ShowCombo(MenuType)
				{
				Menu = new String(MenuType)
				if (document.getElementById('cmb' + Menu).style.display == "none")
					document.getElementById('cmb' + Menu).style.display = "block";
				else 
					document.getElementById('cmb' + Menu).style.display = "none";
				}

			function ShowRow(argName)
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
				
			function CheckNum(args)
				{
				 	if (isNaN(document.getElementById(args).value)==true)
				 		{
				 			document.getElementById(args).value = "";
				 			alert("This Field must be numeric type.");
				 			document.getElementById(args).focus()
				 		}
				}
			
			//create by Ravi
			//Show message before delete record
		function DeleteRecord()
		
			{
			
			if(confirm("Are You Sure To Delete This Record?"+"...[HRMS]")==true)
				return true;
			else
				return false;
			
			}