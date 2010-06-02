<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ifMenu.aspx.cs" Inherits="dtxc.User.ifMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
	<link type="text/css" href="../CSS/main.css" />

	<script src="../Script/Apq.js" type="text/jscript"></script>

	<script type="text/javascript">
		if (window.attachEvent) {
			window.attachEvent("onload", ApqJS.document.iframeAutoFit);
		}
		else if (window.addEventListener) {
			window.addEventListener('load', ApqJS.document.iframeAutoFit, false);
		}
	</script>

</head>
<body>
	<form id="form1" runat="server">
	<div>
		<div id="divLogined">
			欢迎你,<span id="spanName" runat="server"></span>
		</div>
			<a href="ifUserTaskList.aspx" target="ifCenter">任务结算</a></div>
		<div>
			<a href="ifPayoutReg.aspx" target="ifCenter">申请付款</a></div>
		<div>
			<a href="ifUserInfo.aspx" target="ifCenter">个人信息</a></div>
		<div>
			<a href="../ifLoginInfo.aspx" target="ifCenter">修改密码</a></div>
		<%--<div>
			<a href="ifTaskList.aspx" target="ifCenter">登录日志</a></div>--%>
	</div>
	</form>
</body>
</html>
