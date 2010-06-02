<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="dtxc.User.Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>神枪手欢迎您</title>
	<meta http-equiv="X-UA-Compatible" content="IE=7" />
	<link type="text/css" rel="stylesheet" href="/Ext-3.0.3/resources/css/ext-all.css" />
	<link type="text/css" rel="stylesheet" href="~/CSS/ExtMain.css" />
	<%--
QueryString{
	NeedChgPwd(bool): 是否需要修改密码
}
	--%>
</head>
<body>
	<form id="formMain" runat="server">
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
			<asp:ServiceReference Path="~/WS/User/WS1.asmx" />
			<asp:ServiceReference Path="~/WS/User/WS2.asmx" />
		</Services>
	</asp:ScriptManager>
	</form>

	<script type="text/javascript">
		///<reference path="vswd-ext_2.2.js" />

		Ext.QuickTips.init();
		Ext.form.Field.prototype.msgTarget = 'side'; ///提示的方式,枚举值为"qtip","title","under","side",id(元素id)

		Ext.ns('dtxc_ExtMain');

		dtxc_ExtMain.Header = new Ext.form.FormPanel({
			id: "dtxc_ExtMain_Header",
			region: 'north',
			height: 65,
			margins: '5',
			bodyStyle: "text-align: right;",
			items: [
				{ xtype: "box",
					autoEl: {
						tag: 'img',
						src: '../img/logo.jpg',
						style: "float: left"
					}
				},
				{ xtype: "button",
					id: 'btnLogout',
					text: "退出",
					handler: function() { top.location = "../logout.aspx?t=" + Math.random(); }
				}
			]
		});

		dtxc_ExtMain.Footer = new Ext.form.FormPanel({
			id: "dtxc_ExtMain_Footer",
			region: 'south',
			height: 50,
			margins: '0 5 0 5',
			bodyStyle: "text-align: center;",
			items: [{ xtype: "label", text: '版权所有：神枪手'}]
		});

		dtxc_ExtMain.Menu = new Ext.form.FormPanel({
			id: "dtxc_ExtMain_Menu",
			region: 'west',
			autoScroll: true,
			containerScroll: true,
			width: 185,
			//minSize: 175,
			//maxSize: 300,
			margins: '0 0 5 5',
			bodyStyle: "text-align: center;",
			items: [
				{ xtype: "panel", border: false, items: [{ xtype: "box", autoEl: { tag: "a", href: "javascript:;UserTaskList()", html: "任务结算"}}] },
				{ xtype: "panel", border: false, items: [{ xtype: "box", autoEl: { tag: "a", href: "javascript:;ifPayoutReg.aspx", html: "申请付款"}}] },
				{ xtype: "panel", border: false, items: [{ xtype: "box", autoEl: { tag: "a", href: "javascript:;ifUserInfo.aspx", html: "个人信息"}}] },
				{ xtype: "panel", border: false, items: [{ xtype: "box", autoEl: { tag: "a", href: "javascript:;ChgPwd()", html: "修改密码"}}] }
			]
		});

		dtxc_ExtMain.Main = new Ext.TabPanel({
			id: "dtxc_ExtMain_Main",
			region: 'center',
			margins: '0 5 5 0',
			activeItem: 0,
			items: [
				{
					id: "tp_Default",
					title: "默认",
					autoLoad: { url: "tpdefault.aspx" }
				}
			],
			listeners: {
				/*
				//传进去的三个参数分别为:这个tabpanel(dtxc_ExtMain.Main),当前标签页,事件对象e
				"contextmenu": function(tabpanel, tp, e) {
					menu = new Ext.menu.Menu([
						{
							text: "关闭当前页",
							handler: function() {
								tabpanel.remove(tp);
							}
						},
						{
							text: "关闭其他所有页",
							handler: function() {
								//循环遍历
								tabpanel.items.each(function(item) {
									if (item.closable && item != tp) {
										//可以关闭的其他所有标签页全部关掉
										tabpanel.remove(item);
									}
								});
							}
						}
					]);
					//显示在当前位置
					menu.showAt(e.getPoint());
				}
				*/
			}
		});

		Ext.onReady(function() {
			var myView = new Ext.Viewport({
				layout: 'border',
				border: false,
				items: [dtxc_ExtMain.Header, dtxc_ExtMain.Footer, dtxc_ExtMain.Menu, dtxc_ExtMain.Main]
			});

			dtxc_ExtMain.loadMask = new Ext.LoadMask(dtxc_ExtMain.Main.body, {
				msg: "页面加载中……"
			});

			if (ApqJS.location.QueryString["NeedChgPwd"]) {
				Ext.Msg.alert("提示", "密码已过期,请修改密码!");
				ChgPwd();
			}
		});

		function ChgPwd() {
			if (!Ext.get("tp_ChgPwd")) {
				dtxc_ExtMain.Main.add({
					id: "tp_ChgPwd",
					title: "修改密码",
					closable: true,
					autoLoad: { url: '../ChgPwd.aspx', text: '加载中...', scripts: true }
				});
			}
			dtxc_ExtMain.Main.setActiveTab("tp_ChgPwd");
		}

		function UserTaskList() {
			if (!Ext.get("tp_UserTaskList")) {
				dtxc_ExtMain.Main.add({
					id: "tp_UserTaskList",
					title: "任务结算",
					closable: true,
					autoLoad: { url: 'UserTaskList.aspx', text: '加载中...', scripts: true }
				});
			}
			dtxc_ExtMain.Main.setActiveTab("tp_UserTaskList");
		}
	</script>

</body>
</html>
