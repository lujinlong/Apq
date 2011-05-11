<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Dinner.Admin.Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>雪羽点餐系统管理后台</title>
	<meta http-equiv="X-UA-Compatible" content="IE=7" />
	<link type="text/css" rel="stylesheet" href="/ext-3.2.1/resources/css/ext-all.css" />
	<link type="text/css" rel="stylesheet" href="/ext-3.2.1/Ext.ux.grid.GridSummary/Ext.ux.grid.GridSummary.css" />
	<link type="text/css" rel="stylesheet" href="~/CSS/ExtMain.css" />
	<%--
QueryString{
	NeedChgPwd(bool): 是否需要修改密码
}
	--%>
</head>
<body>
	<form id="formMain" runat="server">
	<asp:ScriptManager ID="sc" runat="server">
		<Scripts>
			<%--<asp:ScriptReference Path="/ext-3.2.1/vswd-ext_2.2.js" />--%>
			<asp:ScriptReference Path="/ext-3.2.1/adapter/ext/ext-base-debug.js" />
			<asp:ScriptReference Path="/ext-3.2.1/ext-all-debug.js" />
			<asp:ScriptReference Path="/ext-3.2.1/ext-basex.js" />
			<asp:ScriptReference Path="/ext-3.2.1/src/locale/ext-lang-zh_CN.js" />
			<asp:ScriptReference Path="/ext-3.2.1/Ext.ux.grid.GridSummary/Ext.ux.grid.GridSummary.js" />
			<asp:ScriptReference Path="~/Script/ShowEx.js" />
			<asp:ScriptReference Path="/ApqJS/ExtJS.js" />
			<asp:ScriptReference Path="/ApqJS/ApqJS.js" />
		</Scripts>
		<Services>
			<asp:ServiceReference Path="~/WS/WS2.asmx" />
			<asp:ServiceReference Path="~/WS/Admin/WS1.asmx" />
			<asp:ServiceReference Path="~/WS/User/WS2.asmx" />
		</Services>
	</asp:ScriptManager>
	</form>

	<script type="text/javascript">
		///<reference path="vswd-ext_2.2.js" />

		Ext.QuickTips.init();
		Ext.form.Field.prototype.msgTarget = 'side'; ///提示的方式,枚举值为"qtip","title","under","side",id(元素id)

		Ext.ns('Dinner_ExtMain');

		Dinner_ExtMain.Header = new Ext.form.FormPanel({
			id: "Dinner_ExtMain_Header",
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
					handler: function() { top.location = "../logout.aspx?p=admin/login.aspx&t=" + Math.random(); }
				}
			]
		});

		Dinner_ExtMain.Footer = new Ext.form.FormPanel({
			id: "Dinner_ExtMain_Footer",
			region: 'south',
			height: 20,
			margins: '0 5 0 5',
			bodyStyle: "text-align: center;",
			items: [{ xtype: "label", text: '版权所有：雪羽'}]
		});

		Dinner_ExtMain.Menu = new Ext.form.FormPanel({
			id: "Dinner_ExtMain_Menu",
			region: 'west',
			autoScroll: true,
			containerScroll: true,
			width: 100,
			//minSize: 175,
			//maxSize: 300,
			margins: '0 0 5 5',
			bodyStyle: "text-align: center;",
			items: [
				{ xtype: "panel", border: false, items: [{ xtype: "box", autoEl: { tag: "a", href: "javascript:;", onclick: "tp_Employee()", html: "员工管理"}}] },
				{ xtype: "panel", border: false, items: [{ xtype: "box", autoEl: { tag: "a", href: "javascript:;", onclick: "tp_Rest()", html: "餐馆管理"}}] },
				{ xtype: "panel", border: false, items: [{ xtype: "box", autoEl: { tag: "a", href: "javascript:;", onclick: "tp_EmDinnerLog()", html: "点餐记录"}}] },
				{ xtype: "panel", border: false, items: [{ xtype: "box", autoEl: { tag: "a", href: "javascript:;", onclick: "tp_StatFood()", html: "点餐统计"}}] }
			]
		});

		Dinner_ExtMain.Main = new Ext.TabPanel({
			id: "Dinner_ExtMain_Main",
			region: 'center',
			margins: '0 5 5 0',
			activeItem: 0,
			items: [
				{
					id: "tp_Default",
					title: "系统信息",
					autoLoad: { url: "tpDefault.aspx" }
				}
			],
			listeners: {
				/*
				//传进去的三个参数分别为:这个tabpanel(Dinner_ExtMain.Main),当前标签页,事件对象e
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
				items: [Dinner_ExtMain.Header, Dinner_ExtMain.Footer, Dinner_ExtMain.Menu, Dinner_ExtMain.Main]
			});

			Dinner_ExtMain.loadMask = new Ext.LoadMask(Dinner_ExtMain.Main.body, {
				msg: "页面加载中……"
			});

			if (ApqJS.location.QueryString["NeedChgPwd"]) {
				Ext.Msg.alert("提示", "密码已过期,请修改密码!");
				ChgPwd();
			}
		});

		function ChgPwd() {
			if (!Ext.get("tp_ChgPwd")) {
				Dinner_ExtMain.Main.add({
					id: "tp_ChgPwd",
					title: "修改密码",
					closable: true,
					autoLoad: { url: '../ChgPwd.aspx?t=' + Math.random(), text: '加载中...', scripts: true }
				});
			}
			Dinner_ExtMain.Main.setActiveTab("tp_ChgPwd");
		}

		function tp_Employee() {
			if (!Ext.get("tp_Employee")) {
				Dinner_ExtMain.Main.add({
					id: "tp_Employee",
					title: "员工管理",
					closable: true,
					autoLoad: { url: 'ifEmployee.aspx?t=' + Math.random(), text: '加载中...', scripts: true }
				});
			}
			Dinner_ExtMain.Main.setActiveTab("tp_Employee");
		}

		function tp_Rest() {
			if (!Ext.get("tp_Rest")) {
				Dinner_ExtMain.Main.add({
					id: "tp_Rest",
					title: "餐馆管理",
					closable: true,
					autoLoad: { url: 'tpRest.aspx?t=' + Math.random(), text: '加载中...', scripts: true }
				});
			}
			Dinner_ExtMain.Main.setActiveTab("tp_Rest");
		}

		function tp_EmDinnerLog() {
			if (!Ext.get("tp_EmDinnerLog")) {
				Dinner_ExtMain.Main.add({
					id: "tp_EmDinnerLog",
					title: "点餐记录",
					closable: true,
					autoLoad: { url: 'tpEmDinnerLog.aspx?t=' + Math.random(), text: '加载中...', scripts: true }
				});
			}
			Dinner_ExtMain.Main.setActiveTab("tp_EmDinnerLog");
		}

		function tp_StatFood() {
			if (!Ext.get("tp_StatFood")) {
				Dinner_ExtMain.Main.add({
					id: "tp_StatFood",
					title: "点餐统计",
					closable: true,
					autoLoad: { url: 'tpStatFood.aspx?t=' + Math.random(), text: '加载中...', scripts: true }
				});
			}
			Dinner_ExtMain.Main.setActiveTab("tp_StatFood");
		}
	</script>

</body>
</html>
