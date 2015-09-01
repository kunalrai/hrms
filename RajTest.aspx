<%@ Page Language="vb" AutoEventWireup="false" Codebehind="RajTest.aspx.vb" Inherits="eHRMS.Net.RajTest" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>RajTest</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:TextBox id="TextBox2" style="Z-INDEX: 102; LEFT: 664px; POSITION: absolute; TOP: 416px"
				runat="server"></asp:TextBox>
			<br>
			<br>
			<TABLE cellSpacing="0" cellPadding="0" width="704" align="center" border="0">
				<tr>
					<TD noWrap align="right" width="10" height="19"><IMG class="SetImageFace" src="Images/TableLeft.gif">
					</TD>
					<TD class="headingCont" noWrap align="left" width="5%" background="Images/TableMid.gif"
						height="15">&nbsp;
					</TD>
					<TD class="headingCont" noWrap align="left" background="Images/TableMid.gif" height="19">raje 
						Test
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
					<td colSpan="6"><asp:label id="LblErrMsg" runat="server" Width="100%" ForeColor="Red"></asp:label></td>
				</tr>
				<tr>
					<td colSpan="6"><asp:datagrid id="GrdSetUp" Width="100%" AllowPaging="True" Runat="server" AutoGenerateColumns="False">
							<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
							<HeaderStyle Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Top"
								BackColor="Gray"></HeaderStyle>
							<Columns>
								<asp:ButtonColumn CommandName="Edit" HeaderText="Select" Text="Select" ButtonType="LinkButton"></asp:ButtonColumn>
								<asp:BoundColumn DataField="FIELD_NAME" HeaderText="Field Name">
									<HeaderStyle Width="17%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="FIELD_DESC" HeaderText="Description">
									<HeaderStyle Width="43%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="FLD_CATEG" HeaderText="Category">
									<HeaderStyle Width="30%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="SNO" HeaderText="Sequence">
									<HeaderStyle Width="10%"></HeaderStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
								ForeColor="#003366" PageButtonCount="10" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</TABLE>
			<asp:TextBox id="TextBox1" style="Z-INDEX: 101; LEFT: 216px; POSITION: absolute; TOP: 416px"
				runat="server"></asp:TextBox>
		</form>
	</body>
</HTML>
