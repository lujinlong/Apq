<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="dtxc.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>欢迎登录</title>
	<meta http-equiv="X-UA-Compatible" content="IE=7" />
	<link type="text/css" rel="stylesheet" href="/Ext-3.0.3/resources/css/ext-all.css" />
	<link type="text/css" rel="stylesheet" href="~/CSS/ExtMain.css" />
</head>
<body>
	<form id="form1" runat="server">
	<asp:ScriptManager ID="scLogin" runat="server">
		<Scripts>
			<%--<asp:ScriptReference Path="/Ext-3.0.3/vswd-ext_2.2.js" />--%>
			<asp:ScriptReference Path="/Ext-3.0.3/adapter/ext/ext-base.js" />
			<asp:ScriptReference Path="/Ext-3.0.3/ext-all.js" />
			<asp:ScriptReference Path="/Ext-3.0.3/ext-basex.js" />
			<asp:ScriptReference Path="/Ext-3.0.3/src/locale/ext-lang-zh_CN.js" />
			<asp:ScriptReference Path="~/Script/ShowEx.js" />
			<asp:ScriptReference Path="/ApqJS/ExtJS.js" />
			<asp:ScriptReference Path="/ApqJS/ApqJS.js" />
		</Scripts>
		<Services>
			<asp:ServiceReference Path="~/WS/WS2.asmx" />
		</Services>
	</asp:ScriptManager>
	</form>
	<div id="pnlLogin" style="text-align: center">
	</div>

	<script type="text/javascript">
		///<reference path="vswd-ext_2.2.js" />

		Ext.QuickTips.init();
		Ext.form.Field.prototype.msgTarget = 'side'; ///提示的方式,枚举值为"qtip","title","under","side",id(元素id)

		// 生成页面
		Ext.onReady(function() {
			var formLogin = new Ext.form.FormPanel({
				id: "formLogin",
				title: '欢迎登录',
				renderTo: "pnlLogin",
				frame: true,
				width: 300, height: 130,
				bodyStyle: "margin-right: auto; margin-left: auto;",
				items: [
					{ id: "txtLoginName", name: "txtLoginName", xtype: "textfield", fieldLabel: "登录名",
						allowBlank: false, blankText: "登录名不能为空!"
					},
					{ id: "txtLoginPwd", name: "txtLoginPwd", xtype: "textfield", fieldLabel: "密&nbsp;&nbsp;&nbsp;码",
						inputType: "password",
						allowBlank: false, blankText: "密码不能为空!"
					}
				],
				buttonAlign: "center",
				buttons: [
					{ id: "btnLogin", text: "登录", handler: btnLogin_Click },
					{ id: "btnReg", text: "注册", handler: function() { top.location = "Reg.aspx"; } }
				]
			});

			// 默认值[开发使用]
			Ext.getDom("txtLoginName").value = "Apq";
			Ext.getDom("txtLoginPwd").value = "apq";
		});

		function btnLogin_Click() {
			var strLoginName = Ext.getDom("txtLoginName").value;
			var strLoginPwd = Ext.getDom("txtLoginPwd").value;

			if (strLoginName && strLoginPwd) {
				// 先清空 Cookie
				Ext.state.Cookies.clear("LoginName");
				Ext.state.Cookies.clear("SqlLoginPwd");

				dtxc.WS.WS2.Login_LoginName(strLoginName, strLoginPwd, Login_Success, Apq_WS_Faild);
			}
		}

		function Login_Success(stReturn) {
			if (stReturn.NReturn == 2) {
				alert(stReturn.ExMsg);
				top.location = "User/Main.aspx.aspx?NeedChgPwd=1";
				return;
			}

			if (stReturn.NReturn != 1) {
				alert(stReturn.ExMsg);
				return;
			}

			// Cookie 操作
			Ext.state.Cookies.set("LoginName", stReturn.FNReturn.rows[0]["LoginName"]);
			Ext.state.Cookies.set("SqlLoginPwd", stReturn.POuts[1]);

			top.location = stReturn.FNReturn.rows[0]["IsAdmin"] ? "Admin/Main.aspx" : "User/Main.aspx";
			//top.location = "User/Main.aspx?NeedChgPwd=1";
		}
	</script>

</body>
</html>
