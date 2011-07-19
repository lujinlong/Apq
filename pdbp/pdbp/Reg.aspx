<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reg.aspx.cs" Inherits="pdbp.Reg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>会员注册</title>
	<link href="/Ext-3.2.1/resources/css/ext-all.css" rel="stylesheet" type="text/css" />

	<script src="/Ext-3.2.1/vswd-ext_2.2.js" type="text/javascript"></script>

	<script src="/Ext-3.2.1/adapter/ext/ext-base.js" type="text/javascript"></script>

	<script src="/Ext-3.2.1/ext-all.js" type="text/javascript"></script>

	<script src="/Ext-3.2.1/ext-basex.js" type="text/javascript"></script>

	<script src="/Ext-3.2.1/src/locale/ext-lang-zh_CN.js" type="text/javascript"></script>

	<script src="/ApqJS/ExtJS.js" type="text/javascript"></script>

	<script src="Script/Apq.js" type="text/javascript"></script>

	<script type="text/javascript">
		var App_Themes = 'App_Themes';
		//ApqJS.CSS.swapThemeStyleSheet("css/skin.css", App_Themes);
		ApqJS.CSS.swapThemeStyleSheet("css/main.css", App_Themes);
		ApqJS.CSS.swapThemeStyleSheet("css/div_table.css", App_Themes);
		ApqJS.CSS.swapThemeStyleSheet("css/Reg.css", App_Themes);

		Ext.onReady(function() {
			Ext.QuickTips.init();
			Ext.form.Field.prototype.msgTarget = 'side'; ///提示的方式,枚举值为"qtip","title","under","side",id(元素id)

			if (ApqJS.location.QueryString["i"]) {
				$get("trIntroducer").style.display = "none";
			}

			var btnReg = new Ext.Button({
				id: "btnReg",
				text: "提交",
				renderTo: $get("btn_Reg"),
				handler: btnReg_Click
			});
			var btnLogin = new Ext.Button({
				id: "btnLogin",
				text: "登录",
				renderTo: $get("btn_Login"),
				handler: function() { top.location = 'Login.aspx'; }
			});
		});

		function btnReg_Click() {
			// 获取页面值
			var LoginName = $get("txtLoginName").value;
			var LoginPwd = $get("txtLoginPwd").value;
			var LoginPwdS = $get("txtLoginPwdS").value;
			var Introducer = $get("txtIntroducer").value;

			// 验证值
			if (LoginName.length < 2) {
				Ext.Msg.alert("输入检测", "登录名不能少于2个字符");
				$get("txtLoginName").focus();
				return;
			}
			if (LoginPwd.length < 3) {
				Ext.Msg.alert("输入检测", "密码不能少于3个字符");
				$get("txtLoginPwd").focus();
				return;
			}
			if (LoginPwdS != LoginPwd) {
				Ext.Msg.alert("输入检测", "两次输入的密码不一致");
				$get("txtLoginPwd").focus();
				return;
			}
			//"\w+([-+.']\w+)*@\w+([-.']\w+)*\.\w+([-.]\w+)*"

			// 注册
			PageMethods.Regist(LoginName, LoginPwd, Introducer,
				btnReg_Click_Success, Apq_Server_Faild);
		}

		function btnReg_Click_Success(stReturn) {
			if (stReturn.NReturn == 1) {
				//Ext.Msg.alert("注册成功", stReturn.ExMsg);
				alert("注册成功");
				top.location = "main.htm";
			}
			else {
				Ext.Msg.alert("注册失败", stReturn.ExMsg);
			}
		}
	</script>

</head>
<body>
	<form id="form1" runat="server">
	<div id="div_ph_Toolbar" style="height: 28px; width: 100%; line-height: 28px; background-color: #f3f3f2;
		padding-left: 5px;">
	</div>
	<asp:ScriptManager ID="scReg" runat="server" EnablePageMethods="True">
	</asp:ScriptManager>
	<div class="div_table" style="width: 500px; margin-left: auto; margin-right: auto">
		<div class="div_tcaption" style="background-color: #336633;">
			<div style="color: White; font-size: 36pt;">
				会员注册</div>
		</div>
		<div class="div_tbody">
			<div class="div_tr">
				<div class="div_td">
					用 &nbsp;户 &nbsp;名：</div>
				<div class="div_td">
					<input type="text" id="txtLoginName" name="txtLoginName" /></div>
			</div>
			<div class="div_tr">
				<div class="div_td">
					密 &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;码：</div>
				<div class="div_td">
					<input type="password" id="txtLoginPwd" name="txtLoginPwd" /></div>
			</div>
			<div class="div_tr">
				<div class="div_td">
					确认密码：</div>
				<div class="div_td">
					<input type="password" id="txtLoginPwdS" name="txtLoginPwdS" /></div>
			</div>
			<div id="trIntroducer" class="div_tr">
				<div class="div_td">
					介绍人：</div>
				<div class="div_td">
					<input type="text" id="txtIntroducer" name="txtIntroducer" /></div>
			</div>
		</div>
		<div id="div_Btn" style="width: 500px;">
			<span id="btn_Reg" style="display: inline"></span><span id="btn_Login" style="display: inline">
			</span>
		</div>
	</div>
	</form>
</body>
</html>
