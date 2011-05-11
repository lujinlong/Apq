<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tpRest.aspx.cs" Inherits="Dinner.Admin.tpRest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>餐馆管理</title>
</head>
<body>
	<div id="divRestGrid">
	</div>

	<script type="text/javascript">
		// 添加
		function btnRestAdd_Click() {
			var grid = Ext.getCmp("gridRest");
			var ds = grid.getStore();
			var dr = new Ext.data.Record({ RestID: 0, RestName: "餐馆名称", RestAddr: "餐馆地址" });
			ds.add(dr);
		}
		// 删除
		function btnRestDelete_Click() {
			var grid = Ext.getCmp("gridRest");
			var sm = grid.getSelectionModel();
			var ds = grid.getStore();
			var dr = sm.getSelected();

			if (dr) {
				Dinner.WS.Admin.WS1.Dinner_Restaurant_Delete(dr.data["RestID"]);
				ds.remove(dr);
				ds.commitChanges();
			}
		}
		// 提交
		function btnRestUpdate_Click() {
			var grid = Ext.getCmp("gridRest");
			var ds = grid.getStore();

			// 改动部分 --------------------
			var mr = ds.getModifiedRecords(); //获取所有更新过的记录
			var recordCount = ds.getCount(); //获取数据集中记录的数量

			for (var i = 0; i < mr.length; i++) {
				Dinner.WS.Admin.WS1.Dinner_Restaurant_Save(mr[i].data["RestID"], mr[i].data["RestName"], mr[i].data["RestAddr"]);
			}
			// ==============================

			ds.commitChanges();
			Ext.Msg.alert("操作提示", "操作为异步方式,确认成功与否请点击刷新按钮.");
			ds.load();
		}
		// 编辑菜单
		function btnFood_Click() {
			var grid = Ext.getCmp("gridRest");
			var sm = grid.getSelectionModel();
			var ds = grid.getStore();
			var dr = sm.getSelected();

			if (dr) {
				var tpID = "tp_RestFood"; // + dr.data["RestID"];

				if (!Ext.get(tpID)) {
					Dinner_ExtMain.Main.add({
						id: tpID,
						title: "菜单 - " + dr.data["RestName"],
						closable: true,
						autoLoad: { url: 'tpRestFood.aspx', text: '加载中...', scripts: true }
					});
				}
				else {
					var tpRestFood = Ext.getCmp(tpID);
					tpRestFood.setTitle("菜单 - " + dr.data["RestName"]);
					tpRestFood.load({ url: 'tpRestFood.aspx', text: '加载中...', scripts: true });
				}
				Dinner_ExtMain.Main.setActiveTab(tpID);
			}
		}

		Ext.onReady(function() {
			var store = new Ext.data.JsonStore({
				root: 'd.FNReturn.rows',
				id: 'dsRest',
				fields: [
					{ name: 'RestID', type: 'int' },
					{ name: 'RestName', type: 'string' },
					{ name: 'RestAddr', type: 'string' }
				],
				proxy: new Ext.data.HttpProxy({
					ws: true,
					url: '../WS/Admin/WS1.asmx/Dinner_Restaurant_List',
					method: "post"
				}),
				reader: new Ext.data.JsonReader(
					{ root: 'd.FNReturn.rows', totalProperty: 'd.total' },
					[{ name: 'RestID', mapping: 'RestID', type: 'int' },
					{ name: 'RestName', mapping: 'RestName' },
					{ name: 'RestAddr', mapping: 'RestAddr'}]
				)
			});

			store.on('beforeload', function() {
				store.removeAll();
				store.commitChanges();
			});

			var tBar = new Ext.Toolbar({
				items: [
					{ xtype: 'button', text: '添加', handler: btnRestAdd_Click },
					{ xtype: 'button', text: '删除', handler: btnRestDelete_Click },
					{ xtype: 'button', text: '编辑菜单', handler: btnFood_Click }
				]
			});

			var bBar = new Ext.Toolbar({
				items: [
					{ xtype: 'button', text: '刷新', handler: function() { store.load(); } },
					{ xtype: 'button', text: '提交', handler: btnRestUpdate_Click }
				]
			});

			var grid = new Ext.grid.EditorGridPanel({
				id: 'gridRest',
				el: 'divRestGrid',
				width: "100%",
				height: 520,
				store: store,
				columns: [
					{ header: "餐馆编号", dataIndex: "RestID" },
					{ header: "餐馆名称", dataIndex: "RestName", editor: new Ext.form.TextField({ allowBlank: false }) },
					{ header: "餐馆地址", dataIndex: "RestAddr", editor: new Ext.form.TextField() }
				],
				sm: new Ext.grid.RowSelectionModel({ singleSelect: true }),
				viewConfig: { forceFit: true },
				tbar: tBar,
				bbar: bBar
			});
			grid.render();
			store.load();
		});
	</script>

</body>
</html>
