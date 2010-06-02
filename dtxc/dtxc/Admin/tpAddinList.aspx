<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tpAddinList.aspx.cs" Inherits="dtxc.Admin.tpAddinList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>插件列表</title>
	<meta http-equiv="X-UA-Compatible" content="IE=7" />
	<link type="text/css" rel="stylesheet" href="/Ext-3.0.3/resources/css/ext-all.css" />
	<link type="text/css" rel="stylesheet" href="~/CSS/ExtMain.css" />
</head>
<body>
	<form id="formAddinList" runat="server">
	</form>
	<div id="divAddinList">
	</div>

	<script type="text/javascript">
		{
			var ds, pagingBar, grid, gv, winAddinEdit;

			// 页面控件生成 -----------------------------------------------------------------------
			ds = new Ext.data.Store({// 不能使用JsonStore
				proxy: new Ext.data.HttpProxy({
					url: "../WS/Admin/WS1.asmx/AddinList",
					ws: true, // 表明调用WS
					method: "POST"
				}),
				reader: new Ext.data.JsonReader({
					idProperty: 'jReader',
					root: "d.FNReturn.rows",
					totalProperty: "d.POuts[1]",
					fields: [
						{ name: "AddinID" },
						{ name: "AddinName" },
						{ name: "AddinDescript" }
					]
				})
			});

			pagingBar = new Ext.PagingToolbar({
				id: "pbAddinList",
				store: ds,
				pageSize: 20,
				displayInfo: true,
				prependButtons: true,
				items: [
					{ xtype: "tbbutton", text: "添加", handler: AddinAdd },
					{ xtype: "tbbutton", text: "修改", handler: AddinEdit },
					{ xtype: "tbbutton", text: "删除", handler: AddinDelete }
				]
			});

			ds.on('beforeload', function(o, opt) {
				this.baseParams = Ext.apply(this.baseParams || {}, { "IsLookup": 0, "LookupID": 0 });
			});

			//var sm = new Ext.grid.CheckboxSelectionModel({ singleSelect: true });
			grid = new Ext.grid.GridPanel({
				//title: '任务结算',
				//sm: sm,
				cm: new Ext.grid.ColumnModel({
					defaults: {
						width: 80,
						sortable: true
					},
					columns: [
					//sm,
						{header: "插件ID", dataIndex: 'AddinID' },
						{ header: "插件名", dataIndex: 'AddinName' },
						{ header: "插件说明", dataIndex: 'AddinDescript' }
					]
				}),
				store: ds,
				autoHeight: true,
				viewConfig: {
					forceFit: true
				},
				frame: true,
				bbar: pagingBar
			});

			grid.render("divAddinList");
			gv = grid.getView();

			// 首次进入页面自动加载数据 -----------------------------------------------------------
			ds.load({
				params: {
					start: 0,
					limit: pagingBar.pageSize
				}
			});

			// 编辑窗体
			winAddinEdit = new Ext.Window({
				id: "winAddinEdit",
				title: "插件编辑",
				modal: true,
				width: 800
			});

			function AddinAdd() {
				winAddinEdit.removeAll();
				winAddinEdit.show();
				Ext.state.Manager.set("winAddinEdit_m", "a");
				winAddinEdit.load({ url: "mdAddinEdit.aspx", scripts: true });
			}

			function AddinEdit() {
				var Record = grid.getSelectionModel().getSelected();
				if (!Record) Ext.Msg.alert("提示", "请选择一行");
				else {
					winAddinEdit.removeAll();
					winAddinEdit.show();
					Ext.state.Manager.set("winAddinEdit_AddinID", Record.get("AddinID"));
					Ext.state.Manager.set("winAddinEdit_m", "e");
					winAddinEdit.load({ url: "mdAddinEdit.aspx", scripts: true });
				}
			}

			function AddinDelete() {
				var Record = grid.getSelectionModel().getSelected();
				if (Record) dtxc.WS.Admin.WS1.AddinDelete(Record.get("AddinID"), AddinDelete_Success);
				else Ext.Msg.alert("提示", "请选择一行");
			}
			function AddinDelete_Success(stReturn) {
				Ext.Msg.alert("提示", stReturn.ExMsg, function() { pagingBar.doRefresh(); });
			}
		}
	</script>

</body>
</html>
