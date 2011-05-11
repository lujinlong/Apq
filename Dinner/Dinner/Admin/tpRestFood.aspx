<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tpRestFood.aspx.cs" Inherits="Dinner.Admin.tpRestFood" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>点菜单</title>
</head>
<body>
	<div id="divRestFoodGrid">
	</div>

	<script type="text/javascript">
		Ext.onReady(function() {
			var RestGrid = Ext.getCmp("gridRest");
			var smRest = RestGrid.getSelectionModel();
			var dsRest = RestGrid.getStore();
			var drRest = smRest.getSelected();
			var RestID = 0;
			if (drRest) RestID = drRest.data["RestID"];

			// 添加
			var btnRestFoodAdd_Click = function() {
				var grid = Ext.getCmp("gridRestFood");
				var ds = grid.getStore();
				var dr = new Ext.data.Record({ FoodID: 0, RestID: RestID, FoodName: "快餐名称", FoodPrice: 10 });
				ds.add(dr);
			};
			// 删除
			var btnRestFoodDelete_Click = function() {
				var grid = Ext.getCmp("gridRestFood");
				var sm = grid.getSelectionModel();
				var ds = grid.getStore();
				var dr = sm.getSelected();

				Dinner.WS.Admin.WS1.Dinner_Food_Delete(dr.data["FoodID"]);
				ds.remove(dr);
				ds.commitChanges();
			};
			// 提交
			var btnRestFoodUpdate_Click = function() {
				var grid = Ext.getCmp("gridRestFood");
				var ds = grid.getStore();

				// 改动部分 --------------------
				var mr = ds.getModifiedRecords(); //获取所有更新过的记录
				var recordCount = ds.getCount(); //获取数据集中记录的数量

				for (var i = 0; i < mr.length; i++) {
					Dinner.WS.Admin.WS1.Dinner_Food_Save(
						mr[i].data["FoodID"],
						mr[i].data["RestID"],
						mr[i].data["FoodName"],
						mr[i].data["FoodPrice"]
					);
				}
				// ==============================

				ds.commitChanges();
				Ext.Msg.alert("操作提示", "操作为异步方式,确认成功与否请点击刷新按钮.");
				ds.load({ params: { RestID: RestID} });
			};

			var store = new Ext.data.JsonStore({
				root: 'd.FNReturn.rows',
				id: 'dsRest',
				fields: [
					{ name: 'FoodID', type: 'int' },
					{ name: 'RestID', type: 'int' },
					{ name: 'FoodName', type: 'string' },
					{ name: 'FoodPrice', type: 'float' }
				],
				proxy: new Ext.data.HttpProxy({
					ws: true,
					url: '../WS/Admin/WS1.asmx/Dinner_Food_List',
					method: "post"
				}),
				reader: new Ext.data.JsonReader(
					{ root: 'd.FNReturn.rows', totalProperty: 'd.total' },
					[{ name: 'FoodID', mapping: 'FoodID', type: 'int' },
					{ name: 'RestID', mapping: 'RestID', type: 'int' },
					{ name: 'FoodName', mapping: 'FoodName' },
					{ name: 'FoodPrice', mapping: 'FoodPrice', type: 'float'}]
				)
			});

			store.on('beforeload', function() {
				store.removeAll();
				store.commitChanges();
			});

			var tBar = new Ext.Toolbar({
				items: [
					{ xtype: 'button', text: '添加', handler: btnRestFoodAdd_Click },
					{ xtype: 'button', text: '删除', handler: btnRestFoodDelete_Click }
				]
			});

			var bBar = new Ext.Toolbar({
				items: [
					{ xtype: 'button', text: '刷新', handler: function() { store.load({ params: { RestID: RestID} }); } },
					{ xtype: 'button', text: '提交', handler: btnRestFoodUpdate_Click }
				]
			});

			var grid = new Ext.grid.EditorGridPanel({
				id: 'gridRestFood',
				el: 'divRestFoodGrid',
				width: "100%",
				height: 520,
				store: store,
				columns: [
					{ header: "菜品编号", dataIndex: "FoodID" },
					{ header: "名称", dataIndex: "FoodName", editor: new Ext.form.TextField({ allowBlank: false }) },
					{ header: "价格", dataIndex: "FoodPrice", editor: new Ext.form.NumberField({ allowBlank: false }) }
				],
				sm: new Ext.grid.RowSelectionModel({ singleSelect: true }),
				viewConfig: { forceFit: true },
				tbar: tBar,
				bbar: bBar
			});
			grid.render();
			store.load({ params: { RestID: RestID} });
		});
	</script>

</body>
</html>
