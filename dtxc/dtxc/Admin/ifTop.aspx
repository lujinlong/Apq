<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ifTop.aspx.cs" Inherits="dtxc.Admin.ifTop" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>管理员页面顶部</title>
	<link type="text/css" href="../CSS/main.css" />
</head>
<body>
	<form id="form1" runat="server">
	<asp:ScriptManager ID="scTop" runat="server">
		<Scripts>
			<asp:ScriptReference Path="~/Script/Apq.js" />
		</Scripts>
	</asp:ScriptManager>
	<div style="width: 100%">
		<img id="imgLogo" src="~/img/logo.jpg" runat="server" style="float: left" />
		<div align="right">
			<a href="~/Main.htm" id="aHomeMember" target="_top" runat="server">会员首页</a> <a href="../Logout.aspx"
				id="aLogout" target="_top">退出</a>
		</div>
	</div>

	<script type="text/javascript">
		if (window.attachEvent) {
			window.attachEvent("onload", ApqJS.document.iframeAutoFit);
		}
		else if (window.addEventListener) {
			window.addEventListener('load', ApqJS.document.iframeAutoFit, false);
		}
	</script>

	</form>
</body>
</html>
