<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="pdbp.Logout" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>管理中心</title>

	<script type="text/javascript">
		function win_click() {
			top.location = "Login.aspx";
		}

		window.setTimeout(win_click, 10000);
	</script>

</head>
<body>
	<form id="form1" runat="server">
	<div>
		<p>
			你已退出管理中心!10秒后自动转向登录页面,你也可以直接点击<a href="Login.aspx" target="_top">登录</a>再次登录</p>
	</div>
	</form>
</body>
</html>
