<%@ Register TagPrefix="cc1" Namespace="DITWebLibrary" Assembly="DITWebLibrary" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="LeaveApplication.aspx.vb" Inherits="eHRMS.Net.LeaveApplication" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Leave Application</title>
		<SCRIPT language="javascript" src="coolmenus4.js"></SCRIPT>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="HCStyles.css" type="text/css" rel="stylesheet">
		<Script language="vbscript">
		function ValidateCtrl()
       				

			If document.getElementById("cmbLvType").value = "" Then
                msgbox ("Please select Leave Type From the list, Record Not Saved.")
                ValidateCtrl = false
				exit function
            End If

            If document.getElementById("cmbManager").Value = "" Then
                msgbox ("Please select Manager from the list, Record Not Saved.")
                ValidateCtrl = false
				exit function
            End If
			
			dim dtFromDate, dtTo
			dtFromDate = cdate(document.getElementById("dtpFromDatecmbDD").Value & "/" & document.getElementById("dtpFromDatecmbMM").Value & "/" & document.getElementById("dtpFromDatecmbYY").Value)
			dtTo = cdate(document.getElementById("dtpToDatecmbDD").Value & "/" & document.getElementById("dtpToDatecmbMM").Value & "/" & document.getElementById("dtpToDatecmbYY").Value)
				
            If  dtFromDate > dtTo Then
                msgbox ("From Date Can Not Be after To Date, Record Not Saved.")
                ValidateCtrl = false
				exit function
            End If
            
            If document.getElementById("cmbLvFor").Value <> "11" And dtFromDate <> dtTo Then
                msgbox ("From Date and To Date Should be Same For Half Day Leave, Record Not Saved.")
                ValidateCtrl = false
				exit function				
            End If
		
		end function
		</Script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="0" rightmargin="0" leftmargin="0" bottommargin="0">
		<form id="Form1" method="post" runat="server">
			<br>
			<TABLE cellSpacing="0" cellPadding="0" align="center" width="750" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="19">&nbsp;</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">Leave 
						Application ....
					</TD>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableRight.gif">
					</TD>
				</tr>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" rules="none" width="750" align="center" border="1"
				frame="border">
				<tr>
					<td width="15%"></td>
					<td width="18%"></td>
					<td width="33%"></td>
					<td width="34%"></td>
				</tr>
				<tr borderColor="white">
					<td colSpan="4"><asp:label id="lblMsg" runat="server" ForeColor="Red" Visible="False" Width="100%" Font-Size="11px"></asp:label></td>
				</tr>
				<tr>
					<td colSpan="4">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 120px">
							<asp:datagrid id="grdLeavBal" runat="server" Width="100%" AutoGenerateColumns="False">
								<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
								<HeaderStyle Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Top"
									BackColor="Gray"></HeaderStyle>
								<Columns>
									<asp:BoundColumn ItemStyle-Width="10%" ItemStyle-Font-Size="8" DataField="levyear" HeaderText="Leave Year"
										HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
									<asp:BoundColumn ItemStyle-Width="25%" ItemStyle-Font-Size="8" DataField="LvDesc" HeaderText="Leave Type"
										HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
									<asp:BoundColumn ItemStyle-Width="15%" ItemStyle-Font-Size="8" DataField="Opening" HeaderText="Opening"
										HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
									<asp:BoundColumn ItemStyle-Width="15%" ItemStyle-Font-Size="8" DataField="Earned" HeaderText="Earned"
										HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
									<asp:BoundColumn ItemStyle-Width="15%" ItemStyle-Font-Size="8" DataField="Availed" HeaderText="Availed"
										HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
									<asp:BoundColumn ItemStyle-Width="15%" ItemStyle-Font-Size="8" DataField="Balance" HeaderText="Balance"
										HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
								</Columns>
							</asp:datagrid>
						</div>
					</td>
				</tr>
				<tr>
					<td colSpan="4">&nbsp;</td>
				</tr>
				<tr>
					<td class="Header3"><b>Leave Type<FONT color="red">*</FONT></b></td>
					<td class="Header3" colSpan="3"><asp:dropdownlist id="cmbLvType" runat="server" Width="250px"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td colSpan="4">&nbsp;</td>
				</tr>
				<tr>
					<td class="Header3" style="HEIGHT: 2px">Leave For<FONT color="red">*</FONT></td>
					<td class="Header3" colSpan="3" style="HEIGHT: 2px"><asp:dropdownlist id="cmbLvFor" runat="server" Width="250px"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td colSpan="4">&nbsp;</td>
				</tr>
				<tr>
					<td class="Header3" colspan="2" style="HEIGHT: 19px">From Date&nbsp;&nbsp;&nbsp;
						<cc1:dtpcombo id="dtpFromDate" runat="server" Width="176px" DateValue="2005-08-30" ToolTip="Leave Start Date"></cc1:dtpcombo></td>
					<td class="Header3" style="HEIGHT: 19px">To Date&nbsp;
						<cc1:dtpcombo id="dtpToDate" runat="server" Width="176px" DateValue="2005-08-30" ToolTip="Leave Start Date"></cc1:dtpcombo></td>
					<td class="Header3" style="HEIGHT: 19px"><asp:button id="cmdCalDays" runat="server" Width="100px" Text="Calculate Days"></asp:button>&nbsp;&nbsp;Days<FONT color="red">*
						</FONT>
						<asp:textbox id="txtDays" runat="server" Width="50px" CssClass="TextBox" ReadOnly="True" ForeColor="#003366"></asp:textbox>
					</td>
				</tr>
				<tr>
					<td colSpan="4">&nbsp;</td>
				</tr>
				<tr>
					<td class="Header3" style="HEIGHT: 9px">Manager<FONT color="red">*</FONT></td>
					<td colspan="3" class="Header3" style="HEIGHT: 9px"><asp:dropdownlist id="cmbManager" runat="server" Width="250px"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td colSpan="4">&nbsp;</td>
				</tr>
				<tr>
					<td class="Header3">Address &amp; Contact No.<FONT color="red">*</FONT></td>
					<td class="Header3" vAlign="top" align="left" colSpan="3">
						<asp:textbox id="TxtContAdd" runat="server" Width="100%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
				</tr>
				<tr>
					<td class="Header3">Reason</td>
					<td class="Header3" vAlign="top" align="left" colSpan="3">
						<asp:textbox id="txtReason" runat="server" Width="100%" CssClass="TextBox" ForeColor="#003366"></asp:textbox></td>
				</tr>
				<tr>
					<td colSpan="4">&nbsp;</td>
				</tr>
				<tr>
					<td class="Header3" colspan="2">&nbsp;</td>
					<td class="Header3" align="right"><asp:button id="cmdSave" runat="server" Width="80px" Text="Apply"></asp:button></td>
					<td class="Header3" align="center"><asp:button id="cmdClear" runat="server" Width="80px" Text="Clear"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:button id="cmdClose" runat="server" Width="80px" Text="Close"></asp:button>
					</td>
				</tr>
				<tr>
					<td colspan="4" class="Header3" align="right"><FONT color="red">*</FONT>&nbsp;<FONT color="red">Mandatory 
							Fields</FONT></td>
				</tr>
				<TR>
					<TD colspan="4">
						<asp:label id="lblAlready" runat="server" ForeColor="#003366" Visible="False" Width="784px"
							Font-Size="11px" Height="5px"></asp:label></TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
