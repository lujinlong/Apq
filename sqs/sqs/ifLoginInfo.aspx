<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ifLoginInfo.aspx.cs"
	Inherits="dtxc.ifLoginInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>登录信息</title>
	<link type="text/css" href="CSS/main.css" />
</head>
<body>
	<form id="form1" runat="server">
	<asp:ScriptManager ID="scLoginInfo" runat="server">
		<Services>
			<asp:ServiceReference Path="~/WS/WS2.asmx" />
			<asp:ServiceReference Path="~/WS/User/WS2.asmx" />
		</Services>
		<Scripts>
			<asp:ScriptReference Path="~/Script/Apq.js" />
			<asp:ScriptReference Path="~/Script/Login.js" />
		</Scripts>
	</asp:ScriptManager>
	<div>
		<div>
			登录名:<asp:TextBox ID="txtUserName" runat="server" Enabled="false">0</asp:TextBox>
		</div>
		<div>
			原密码:<input type="password" id="txtLoginPwd_C" value="" />
		</div>
		<div>
			新密码:<input type="password" id="txtLoginPwd" value="" />
		</div>
		<div>
			密码确认:<input type="password" id="txtLoginPwd1" value="" />
		</div>
		<div align="center">
			<input type="button" id="btnConfirm" onclick="btnConfirm_Click()" value="修改" />
		</div>
	</div>

	<script type="text/javascript">
		if (window.attachEvent) {
			window.attachEvent("onload", ApqJS.document.iframeAutoFit);
		}
		else if (window.addEventListener) {
			window.addEventListener('load', ApqJS.document.iframeAutoFit, false);
		}

		function mdUserNameInfo_Load() {
		}
		window.attachEvent("onload", mdUserNameInfo_Load);

		function btnConfirm_Click() {
			// 获取页面值
			var LoginPwd_C = $get("txtLoginPwd_C").value;
			var LoginPwd = $get("txtLoginPwd").value;
			var LoginPwd1 = $get("txtLoginPwd1").value;

			// 验证值
			if (LoginPwd_C.length < 3) {
				alert("密码不能少于3个字符");
				$get("txtLoginPwd_C").focus();
				return;
			}
			if (LoginPwd.length < 3) {
				alert("密码不能少于3个字符");
				$get("txtLoginPwd").focus();
				return;
			}
			if (LoginPwd1 != LoginPwd) {
				alert("两次输入的密码不一致");
				$get("txtLoginPwd").focus();
				return;
			}

			// 修改
			dtxc.WS.User.WS2.UserEditLoginPwd(LoginPwd_C, LoginPwd, btnConfirm_Click_Success);
		}
		function btnConfirm_Click_Success(stReturn) {
			ApqJS.using("ApqJS.Simulator");
			var MsgBox = new ApqJS.Simulator.Dialog(0, "提示", stReturn.ExMsg, 300, 0, 150, btnConfirm_Click_Success_Confirm);
			MsgBox.Show();

			function btnConfirm_Click_Success_Confirm(evt) {
				if (stReturn.NReturn == 1) {
					// 执行登录
					var strUserName = $get("txtUserName").value;
					var strLoginPwd = $get("txtLoginPwd").value;
					Login(strUserName, strLoginPwd);
				}
			}
		}
	</script>

	</form>
</body>
</html>
