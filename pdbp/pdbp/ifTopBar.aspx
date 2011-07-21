<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ifTopBar.aspx.cs" Inherits="pdbp.ifTopBar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>顶部工具条</title>
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
		ApqJS.CSS.swapThemeStyleSheet("css/skin.css", App_Themes);

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
	</script>

</head>
<body>
	<form id="form1" runat="server">
	<asp:ScriptManager ID="scTopBar" runat="server">
	</asp:ScriptManager>
	</form>
</body>
</html>
