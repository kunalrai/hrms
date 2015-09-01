<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PaySetup.aspx.vb" Inherits="eHRMS.Net.PaySetup" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PaySetup_View_</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		function DeleteRecord()
		{
			if(confirm("Are You Sure Want To Delete This Record?")==true)
				return true;
			else
				return false;
		}
		function Valedate()
		{
			//Taking Variable For Putting The Control's Values
			var FldName =document.getElementById("TxtFldName");
			var PrintName =document.getElementById("TxtPrintName");
			
			if(FldName.value.length==0)
				{
					alert("Please Enter The Field Name:");
					FldName.focus();
					return false;
				}
			else
					return true;				
		   if(PrintName.value.length==0)
				{
					alert("Please Enter The Print Name:");
					PrintName.focus();
					return false;
				}
			else
					return true;	
		}
			
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--#include file=MenuBars.aspx --><br>
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="700" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="15">&nbsp;
					</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Pay 
						Setup
					</TD>
					<TD align="right" width="10" height="19"><IMG class="SetImageFace" style="WIDTH: 27px; HEIGHT: 19px" height="19" src="Images/TableRight.gif"
							width="27">
					</TD>
				</tr>
			</TABLE>
			<TABLE cellSpacing="2" cellPadding="0" rules="none" width="700" align="center" border="1"
				frame="box">
				<tr>
					<td width="15%"></td>
					<td width="15%"></td>
					<td width="15%"></td>
					<td width="15%"></td>
					<td width="15%"></td>
					<td width="25%"></td>
				</tr>
				<tr>
					<td colSpan="6">
						<asp:label id="LblErrMsg" runat="server" Width="100%" ForeColor="Red"></asp:label></td>
				</tr>
				<TR>
					<TD colSpan="6">
						<TABLE id="Table1" cellSpacing="2" cellPadding="0" width="100%" bgColor="#666666" border="1"
							align="center" style="FONT-WEIGHT: bold">
							<TR align="center">
								<TD style="WIDTH: 119px"><asp:LinkButton id="LnkFIELD_NAME" runat="server" ForeColor="White" OnClick="Head_Click">Field Name</asp:LinkButton></STRONG></FONT></TD>
								<TD align="center" style="WIDTH: 282px">
									<asp:LinkButton id="LnkFIELD_DESC" runat="server" ForeColor="White" OnClick="Head_Click">Description</asp:LinkButton></TD>
								<TD align="center" style="WIDTH: 199px">
									<asp:LinkButton id="LnkFLD_CATEG" runat="server" ForeColor="White" OnClick="Head_Click">Category</asp:LinkButton></TD>
								<TD align="center"><FONT color="#ffffff" size="2"><STRONG>
											<asp:LinkButton id="LnkSNO" runat="server" ForeColor="White" OnClick="Head_Click">Sequence</asp:LinkButton></STRONG></FONT></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<tr>
					<td colSpan="6">
						<div style="OVERFLOW: auto; WIDTH: 700px; COLOR: #cccccc; BORDER-TOP-STYLE: solid; BORDER-RIGHT-STYLE: solid; BORDER-LEFT-STYLE: solid; HEIGHT: 250px; BORDER-BOTTOM-STYLE: solid"><asp:datagrid id="GrdSetUp" style="OVERFLOW: auto" Width="100%" AutoGenerateColumns="False" Runat="server"
								AllowSorting="True">
								<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
								<HeaderStyle Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Top"
									BackColor="Gray"></HeaderStyle>
								<Columns>
									<asp:ButtonColumn DataTextField="FIELD_NAME" SortExpression="field_name" CommandName="Edit">
										<HeaderStyle Width="17%"></HeaderStyle>
									</asp:ButtonColumn>
									<asp:BoundColumn DataField="FIELD_DESC" SortExpression="FIELD_DESC">
										<HeaderStyle Width="43%"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="FLD_CATEG" SortExpression="FLD_CATEG">
										<HeaderStyle Width="30%"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SNO" SortExpression="SNO">
										<HeaderStyle Width="10%"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="Field_Name">
										<HeaderStyle Width="0%"></HeaderStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
									ForeColor="#003366" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</td>
				</tr>
				<tr>
					<td>&nbsp;<asp:label id="Label1" Runat="server" text="Field Name"></asp:label></td>
					<td><asp:textbox id="TxtFldName" ForeColor="#003366" Width="100px" Runat="server" CssClass="textbox"
							MaxLength="10"></asp:textbox></td>
					<td>&nbsp;<asp:label id="Label2" Runat="server" text="Printable Name"></asp:label></td>
					<td><asp:textbox id="TxtPrintName" ForeColor="#003366" Width="100px" Runat="server" CssClass="textbox"></asp:textbox></td>
					<td>&nbsp;<asp:label id="Label3" Runat="server" text="Category"></asp:label></td>
					<td align="center"><asp:dropdownlist id="CmbCategory" ForeColor="#003366" Width="150px" Runat="server">
							<asp:ListItem Value="1">Earnings</asp:ListItem>
							<asp:ListItem Value="2">Deductions</asp:ListItem>
							<asp:ListItem Value="3">Loan &amp; Advances</asp:ListItem>
							<asp:ListItem Value="4">Reimbursments</asp:ListItem>
							<asp:ListItem Value="5">Perquisities</asp:ListItem>
							<asp:ListItem Value="6">Investments</asp:ListItem>
							<asp:ListItem Value="7">Taxable</asp:ListItem>
							<asp:ListItem Value="8" Selected="True">Others</asp:ListItem>
						</asp:dropdownlist></td>
				</tr>
				<tr>
					<td>&nbsp;<asp:label id="Label4" Runat="server" text="Description"></asp:label></td>
					<td colSpan="3"><asp:textbox id="TxtDescription" ForeColor="#003366" Width="310px" Runat="server" CssClass="textbox"></asp:textbox></td>
					<td>&nbsp;<asp:label id="Label6" Runat="server" text="Rated"></asp:label>
						<asp:textbox id="TxtCell" runat="server" Width="0px" BorderStyle="None" Visible="False"></asp:textbox></td>
					<td align="center"><asp:dropdownlist id="CmbRated" ForeColor="#003366" Width="150px" Runat="server">
							<asp:ListItem Value="4" Selected="True">Yearly</asp:ListItem>
							<asp:ListItem Value="0">Daily</asp:ListItem>
							<asp:ListItem Value="1">Monthly</asp:ListItem>
							<asp:ListItem Value="2">Quaterly</asp:ListItem>
							<asp:ListItem Value="3">Half-Yearly</asp:ListItem>
						</asp:dropdownlist></td>
				</tr>
				<tr>
					<td>&nbsp;<asp:label id="Labl5" Runat="server" text="Field Type"></asp:label></td>
					<td><asp:dropdownlist id="CmbFieldType" ForeColor="#003366" Width="100px" Runat="server">
							<asp:ListItem Value="N" Selected="True">Numeric</asp:ListItem>
							<asp:ListItem Value="C">Character</asp:ListItem>
							<asp:ListItem Value="D">Date</asp:ListItem>
							<asp:ListItem Value="B">Boolean</asp:ListItem>
						</asp:dropdownlist></td>
					<td>&nbsp;<asp:label id="Label7" Runat="server" text="Width"></asp:label>&nbsp;&nbsp;
						<asp:textbox id="TxtWidth" ForeColor="#003366" Width="50px" Runat="server" CssClass="textbox"></asp:textbox></td>
					<td>&nbsp;<asp:label id="Label9" Runat="server" text="Decimal"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:textbox id="TxtDecimal" ForeColor="#003366" Width="30px" Runat="server" CssClass="textbox"></asp:textbox></td>
					<td>&nbsp;<asp:label id="Label8" Runat="server" text="Starting Month"></asp:label></td>
					<td align="center"><asp:dropdownlist id="CmbStartingMonth" ForeColor="#003366" Width="150px" Runat="server" CssClass="dropdownlist">
							<asp:ListItem Value="0">January</asp:ListItem>
							<asp:ListItem Value="1">Febuary</asp:ListItem>
							<asp:ListItem Value="2">March</asp:ListItem>
							<asp:ListItem Value="3" Selected="True">April</asp:ListItem>
							<asp:ListItem Value="4">May</asp:ListItem>
							<asp:ListItem Value="5">June</asp:ListItem>
							<asp:ListItem Value="6">July</asp:ListItem>
							<asp:ListItem Value="7">August</asp:ListItem>
							<asp:ListItem Value="8">September</asp:ListItem>
							<asp:ListItem Value="9">October</asp:ListItem>
							<asp:ListItem Value="10">November</asp:ListItem>
							<asp:ListItem Value="11">December</asp:ListItem>
						</asp:dropdownlist></td>
				</tr>
				<tr>
					<td colSpan="6" height="5"></td>
				<tr>
				<tr>
					<td align="center" colSpan="6">
						<table height="40" cellSpacing="2" cellPadding="0" rules="none" width="98%" border="1"
							frame="box">
							<tr>
								<td>&nbsp;&nbsp;<asp:checkbox id="ChkHrdMast" ForeColor="#003366" Width="100px" Runat="server" Text="HRD Master"></asp:checkbox></td>
								<td>&nbsp;&nbsp;<asp:checkbox id="ChkPayMast" ForeColor="#003366" Width="100px" Runat="server" Text="Pay Master"></asp:checkbox></td>
								<td>&nbsp;&nbsp;<asp:checkbox id="ChkHrdHist" ForeColor="#003366" Width="100px" Runat="server" Text="HRD History"></asp:checkbox></td>
								<td>&nbsp;&nbsp;<asp:checkbox id="ChkPayHist" ForeColor="#003366" Width="100px" Runat="server" Text="Pay History"></asp:checkbox></td>
								<td>&nbsp;&nbsp;<asp:checkbox id="ChkIncrement" ForeColor="#003366" Width="100px" Runat="server" Text="Increment"></asp:checkbox></td>
								<td>&nbsp;&nbsp;<asp:checkbox id="ChkDefField" ForeColor="#003366" Width="120px" Runat="server" Text="Default Field"></asp:checkbox></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="6" height="5"></td>
				</tr>
				<TR height="20">
					<TD style="MARGIN-LEFT: 15px; MARGIN-RIGHT: 15px; BORDER-BOTTOM: #993300 thin groove; HEIGHT: 7px"
						colSpan="6"></TD>
				</TR>
				<tr>
					<td colSpan="6" height="5"></td>
				</tr>
				<tr>
					<td width="100%" colSpan="6">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0" frame="box">
							<tr>
								<td align="right" colSpan="6"><asp:button id="cmdUp" Width="75px" Runat="server" Text="Up" ToolTip="Click Here To Move Upward."
										Font-Size="10pt"></asp:button>&nbsp;&nbsp;&nbsp;
									<asp:button id="cmdDown" Width="75px" Runat="server" Text="Down" ToolTip="Click Here To Move Down."
										Font-Size="10pt"></asp:button>&nbsp;&nbsp;&nbsp;
									<asp:button id="CmdNew" Width="75px" Runat="server" Text="New" ToolTip="Click Here For New Record."
										Font-Size="10pt"></asp:button>&nbsp;&nbsp;&nbsp;
									<asp:button id="CmdDelete" runat="server" Width="75px" Text="Delete" ToolTip="Click Here For Delete."
										Font-Size="10pt"></asp:button>&nbsp;
									<asp:button id="CmdSave" runat="server" Width="75px" Text="Save" ToolTip="Click Here For Save."
										Font-Size="10pt"></asp:button>&nbsp;&nbsp;
									<asp:button id="CmdClose" runat="server" Width="75px" Text="Close" ToolTip="Click Here For Close Form."
										Font-Size="10pt"></asp:button>&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="6" height="15"><asp:textbox id="txtSNO" runat="server" Visible="False"></asp:textbox></td>
				</tr>
			</TABLE>
		</form>
		</TR></TABLE></FORM></TABLE>
	</body>
</HTML>