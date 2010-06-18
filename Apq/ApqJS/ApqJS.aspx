<!-- Apq_TopWindow,Apq_IsApqJS-->
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<title></title>

	<script type="text/jscript">
		window.Apq_IsApqJS = true; // 申明为主框架页
	</script>

</head>
<body style="margin: 0" scroll="no">
	<form id="form1" runat="server">
	<asp:ScriptManager ID="sm" runat="server">
		<Scripts>
			<asp:ScriptReference Path="Script/Apq.js" />
		</Scripts>
	</asp:ScriptManager>
	<iframe id="ifMain" width="100%" frameborder="0"></iframe>

	<script type="text/jscript">
		function window_onload() {
			// 取消本处理
			window.detachEvent("onload", window_onload);

			ifMain_onresize();
			Apq_TopWindow.document.getElementById("ifMain").attachEvent("onresize", ifMain_onresize);
			Apq_TopWindow.document.getElementById("ifMain").attachEvent("onload", ifMain_onload);

			// 打开起始页面
			Apq_TopWindow.document.getElementById("ifMain").src = Apq_InitConfig.Home;
		}
		window.attachEvent("onload", window_onload);

		function ifMain_onresize() {
			Apq_TopWindow.document.getElementById("ifMain").style.posHeight = Apq_TopWindow.document.documentElement.offsetHeight;
		}

		function ifMain_onload(e) {
			try {
				Apq_TopWindow.Apq_TopIFrameWindow = Apq_TopWindow.document.getElementById("ifMain").contentWindow;
				// 这里不能是以下语句
				//Apq_TopWindow.document.title = Apq_TopWindow.document.getElementById('ifMain').contentWindow.document.title;
				setTimeout("Apq_TopWindow.document.title = Apq_TopWindow.document.getElementById('ifMain').contentWindow.document.title", 0);
			} catch (e) { }
		}
	</script>

	</form>
</body>
</html>
