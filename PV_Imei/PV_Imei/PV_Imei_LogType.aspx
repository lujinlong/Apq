<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PV_Imei_LogType.aspx.cs" Inherits="PV_Imei.PV_Imei_LogType" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
	<style type="text/css">
		.style1
		{
			width: 100%;
		}
	</style>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<table class="style1">
			<tr>
				<td>
					Imei:
				</td>
				<td>
					<asp:TextBox ID="txtImei" runat="server">019d3110a2145081</asp:TextBox>
				</td>
				<td>
					LogType:
				</td>
				<td>
					<asp:TextBox ID="txtLogType" runat="server">1</asp:TextBox>
				</td>
			</tr>
			<tr>
				<td colspan="4">
					<asp:LinkButton ID="btnQuery" runat="server" OnClick="btnQuery_Click">查询</asp:LinkButton>
				</td>
			</tr>
			<tr>
				<td colspan="4">
					<asp:TextBox ID="txtOut" runat="server" TextMode="MultiLine" Height="310px" Width="911px"></asp:TextBox>
				</td>
			</tr>
		</table>
	</div>
	</form>
</body>
</html>
