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
				 			alert("This field must be numeric type.");
				 			document.getElementById(args).value = "";
				 		}
				}