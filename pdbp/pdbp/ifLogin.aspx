<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ifLogin.aspx.cs" Inherits="pdbp.ifLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>登陆框</title>
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
		ApqJS.CSS.swapThemeStyleSheet("css/main.css", App_Themes);
		ApqJS.CSS.swapThemeStyleSheet("css/div_table.css", App_Themes);

		Ext.onReady(function() {
			Ext.QuickTips.init();
			Ext.form.Field.prototype.msgTarget = 'side'; ///提示的方式,枚举值为"qtip","title","under","side",id(元素id)

			// 页面内图片
			$get("imgLogo").src = ApqJS.Img.getThemeImgUrl("img/logo.png", App_Themes);
			$get("imgLock").src = ApqJS.Img.getThemeImgUrl("img/lock.gif", App_Themes);
			$get("img1").src = ApqJS.Img.getThemeImgUrl("img/icon-demo.gif", App_Themes);
			$get("img2").src = ApqJS.Img.getThemeImgUrl("img/icon-login-seaver.gif", App_Themes);
			$get("img3").src = ApqJS.Img.getThemeImgUrl("img/login-wel.gif", App_Themes);
		});

		function btnLogin_Click() {
			var strLoginName = Ext.getDom("txtLoginName").value;
			var strLoginPwd = Ext.getDom("txtLoginPwd").value;

			if (!strLoginName) {
				Ext.Msg.alert("输入检测", "必须输入用户名");
				Ext.getDom("txtLoginName").focus();
				return false;
			}
			if (!strLoginPwd) {
				Ext.Msg.alert("输入检测", "必须输入密码");
				Ext.getDom("txtLoginPwd").focus();
				return false;
			}

			pdbp.wsLogin.Login_LoginName(strLoginName, strLoginPwd, btnLogin_Click_Success, Apq_Server_Faild);
			return false;
		}
		function btnLogin_Click_Success(stReturn) {
			if (stReturn.NReturn == 1) {
				top.location = "main.htm";
				return;
			}
			Ext.Msg.alert("登录失败", stReturn.ExMsg);
		}
	</script>

</head>
<body>
	<form id="form1" runat="server">
	<asp:ScriptManager ID="scLogin" runat="server">
		<Services>
			<asp:ServiceReference Path="~/wsLogin.asmx" />
		</Services>
	</asp:ScriptManager>
	<div>
		<div style="display: block" id="web_login">
			<ul id="g_list">
				<li id="g_u"><span><u id="label_uin">QQ帐号：</u></span><input onblur="try{ptui_onUserBlue('u', '#CCCCCC');}catch(e){}check();"
					style="ime-mode: disabled; background: #ffffcc" id="u" class="inputstyle" onfocus="try{ptui_onUserFocus('u', '#000000')}catch(e){}"
					tabindex="1" value="12766385" name="u">
					<label>
						<a id="label_newreg" tabindex="7" href="http://id.qq.com?from=pt" target="_blank">注册新帐号</a></label>
					<li id="g_p"><span><u id="label_pwd">QQ密码：</u></span><input style="background: #ffffcc"
						id="p" class="inputstyle" onfocus="check();" tabindex="2" value="" maxlength="16"
						type="password" name="p" maxlength="32">
						<label>
							<a id="label_forget_pwd" tabindex="8" onclick="onClickForgetPwd()" href="http://ptlogin2.qq.com/ptui_forgetpwd"
								target="_blank">忘了密码？</a></label>
						<li id="verifyinput"><span for="code"><u id="label_vcode">验证码：</u></span><input style="ime-mode: disabled;
							background: #ffffcc" id="verifycode" class="inputstyle" tabindex="3" maxlength="4"
							name="verifycode">
							<li id="verifytip"><span>&nbsp;</span> <u id="label_vcode_tip">输入下图中的字符，不区分大小写</u>
								<li id="verifyshow"><span for="pic">&nbsp;</span>
									<img id="imgVerify" src="http://ptlogin2.qq.com/getimage?aid=15004501&amp;r=0.2334838269986083&amp;uin=12766385"
										width="130" onload="imgLoadReport()" height="53">
									<label>
										<a id="changeimg_link" tabindex="6" href="javascript:ptui_changeImg('qq.com', 15004501, true);">
											看不清，换一张</a></label></li></ul>
			<div class="login_button">
				<input id="login_button" class="btn" tabindex="5" value="登 录" type="submit">
			</div>
			<div id="label_unable_tips" class="lineright">
			</div>
			<input value="15004501" type="hidden" name="aid">
			<input value="http://ctc.qzs.qq.com/qzone/v5/loginsucc.html#para=mall&amp;" type="hidden"
				name="u1">
			<input value="http://ctc.qzs.qq.com/qzone/v5/loginerr.html" type="hidden" name="fp">
			<input value="1" type="hidden" name="h">
			<input value="0" type="hidden" name="ptredirect">
			<input value="2052" type="hidden" name="ptlang">
			<input value="1" type="hidden" name="from_ui">
			<input type="hidden" name="dumy">
		</div>
		<div style="display: block" id="switch" class="lineright">
			<a style="cursor: pointer" onclick="switchpage();">切换到快速登录模</a></div>
	</div>
	</form>
</body>
</html>
