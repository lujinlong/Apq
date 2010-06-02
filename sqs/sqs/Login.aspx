<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="dtxc.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>登录</title>
	<link type="text/css" href="CSS/main.css" />
</head>
<body>
	<form id="form1" runat="server">
	<asp:ScriptManager ID="scLogin" runat="server">
		<Services>
			<asp:ServiceReference Path="~/WS/WS2.asmx" />
		</Services>
		<Scripts>
			<asp:ScriptReference Path="~/Script/Apq.js" />
			<asp:ScriptReference Path="~/Script/Login.js" />
		</Scripts>
	</asp:ScriptManager>
	<div align="center" style="vertical-align: middle;">
		<div>
			<span>用户名:</span><input type="text" id="txtUserName" value="Apq" />
		</div>
		<div>
			<span style="width: 80px">密 码 :</span><input type="password" id="txtLoginPwd" value="apq"
				style="width: 149px" />
		</div>
		<a id="btnLogin" href="javascript:btnLogin_Click()" style="position: relative; left: 20px">
			登录</a> <a id="btnReg" href="Reg.aspx" style="position: relative; left: 70px" runat="server">
				注册</a>
	</div>

	<script type="text/javascript">
		if (window.attachEvent) {
			window.attachEvent("onload", ApqJS.document.iframeAutoFit);
		}
		else if (window.addEventListener) {
			window.addEventListener('load', ApqJS.document.iframeAutoFit, false);
		}

		function btnLogin_Click() {
			var strUserName = $get("txtUserName").value;
			var strLoginPwd = $get("txtLoginPwd").value;
			Login(strUserName, strLoginPwd);
		}
	</script>

	</form>
</body>
</html>
