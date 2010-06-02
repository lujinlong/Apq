<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ifPayoutReg.aspx.cs" Inherits="dtxc.User.ifPayoutReg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>申请付款</title>
	<link type="text/css" href="../CSS/main.css" />
</head>
<body>
	<form id="form1" runat="server">
	<asp:ScriptManager ID="scUserInfo" runat="server">
		<Services>
			<asp:ServiceReference Path="../WS/User/WS1.asmx" />
		</Services>
		<Scripts>
			<asp:ScriptReference Path="~/Script/Apq.js" />
		</Scripts>
	</asp:ScriptManager>
	<div>
		<div>
			余额:<span id="txtBalanceMoney" runat="server">0</span>
		</div>
		<div>
			申请金额:<input type="text" id="txtPayout" />
		</div>
		<div align="center">
			<input type="button" id="btnConfirm" onclick="btnConfirm_Click()" value="申请付款" />
		</div>
	</div>

	<script type="text/javascript">
		if (window.attachEvent) {
			window.attachEvent("onload", ApqJS.document.iframeAutoFit);
		}
		else if (window.addEventListener) {
			window.addEventListener('load', ApqJS.document.iframeAutoFit, false);
		}

		function mdUserInfo_Load() {
		}
		window.attachEvent("onload", mdUserInfo_Load);

		function btnConfirm_Click() {
			// 获取页面值
			var Payout = $get("txtPayout").value;

			// 申请
			dtxc.WS.User.WS1.UserPayoutReg(Payout, btnConfirm_Click_Success);
		}
		function btnConfirm_Click_Success(stReturn) {
			ApqJS.using("ApqJS.Simulator");
			var MsgBox = new ApqJS.Simulator.Dialog(0, "提示", stReturn.ExMsg, 300, 0, 150, btnConfirm_Click_Success_Confirm);
			MsgBox.Show();

			function btnConfirm_Click_Success_Confirm(evt) {
				if (stReturn.NReturn == 1) {
					location.reload(true);
				}
			}
		}
	</script>

	</form>
</body>
</html>
