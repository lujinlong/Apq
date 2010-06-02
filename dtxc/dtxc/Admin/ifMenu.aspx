<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ifMenu.aspx.cs" Inherits="dtxc.Admin.ifMenu" %>

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
		<div>
			<a href="ifAddinList.aspx" target="ifCenter">插件管理</a></div>
		<div>
			<a href="ifTaskList.aspx" target="ifCenter">任务管理</a></div>
		<div>
			<a href="ifUserList.aspx" target="ifCenter">会员管理</a></div>
		<div>
			<a href="ifPayoutList.aspx" target="ifCenter">支付管理</a></div>
		<div>
			<a href="ifLog_PayoutList.aspx" target="ifCenter">支付历史</a></div>
	</div>
	</form>
</body>
</html>
