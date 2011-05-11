<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tpEmDinner.aspx.cs" Inherits="Dinner.User.tpEmDinner" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>点餐</title>
</head>
<body>
	<div id="divEmDinner">
	</div>

	<script type="text/javascript">
		Ext.onReady(function() {
			var dsRest = new Ext.data.JsonStore({
				root: 'd.FNReturn.rows',
				id: 'dsRest',
				fields: [
					{ name: 'RestID', type: 'int' },
					{ name: 'RestName', type: 'string' },
					{ name: 'RestAddr', type: 'string' }
				],
				proxy: new Ext.data.HttpProxy({
					ws: true,
					url: '../WS/User/WS2.asmx/Dinner_Restaurant_List',
					method: "post"
				}),
				reader: new Ext.data.JsonReader(
					{ root: 'd.FNReturn.rows', totalProperty: 'd.total' },
					[
						{ name: 'RestID', mapping: 'RestID', type: 'int' },
						{ name: 'RestName', mapping: 'RestName' },
						{ name: 'RestAddr', mapping: 'RestAddr' }
					]
				)
			});

			var dsFood = new Ext.data.JsonStore({
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
					url: '../WS/User/WS2.asmx/Dinner_Food_List',
					method: "post"
				}),
				reader: new Ext.data.JsonReader(
					{ root: 'd.FNReturn.rows', totalProperty: 'd.total' },
					[
						{ name: 'FoodID', mapping: 'FoodID', type: 'int' },
						{ name: 'RestID', mapping: 'RestID', type: 'int' },
						{ name: 'FoodName', mapping: 'FoodName' },
						{ name: 'FoodPrice', mapping: 'FoodPrice', type: 'float' }
					]
				)
			});

			dsFood.on('beforeload', function() {
				dsFood.removeAll();
				dsFood.commitChanges();
			});

			// 选择餐馆
			var cbRest_Select = function(combo, dr, rowIndex) {
				var RestID = dr.data["RestID"];
				dsFood.load({ params: { RestID: RestID} });
			};

			// 提交
			var btnEmDinner_Click = function() {
				var grid = Ext.getCmp("gridEmDinner");
				var ds = grid.getStore();
				var sm = grid.getSelectionModel();

				// 选择部分 --------------------
				var mr = sm.getSelections();

				for (var i = 0; i < mr.length; i++) {
					Dinner.WS.User.WS2.Dinner_EmDinner_Insert(
						mr[i].data["FoodID"]
					);
				}
				// ==============================

				Ext.Msg.alert("操作提示", "操作为异步方式,确认成功与否请查看历史记录.");
			};

			var tBar = new Ext.Toolbar({
				items: [
					{ xtype: 'label', text: '选择餐馆' },
					{ xtype: 'combo', id: "cbRest",
						mode: 'remote',
						editable: false,
						triggerAction: 'all',
						store: dsRest,
						valueField: 'RestID',
						displayField: 'RestName',
						width: 200,
						listeners: { "select": cbRest_Select }
					}
				]
			});

			var bBar = new Ext.Toolbar({
				items: [
					{ xtype: 'button', text: '提交', handler: btnEmDinner_Click }
				]
			});

			var smCheckBox = new Ext.grid.CheckboxSelectionModel({ checkOnly: true });

			var grid = new Ext.grid.EditorGridPanel({
				id: 'gridEmDinner',
				el: 'divEmDinner',
				width: "100%",
				height: 560,
				store: dsFood,
				columns: [
					smCheckBox,
					{ header: "菜品编号", dataIndex: "FoodID" },
					{ header: "名称", dataIndex: "FoodName" },
					{ header: "价格", dataIndex: "FoodPrice" }
				],
				sm: smCheckBox,
				viewConfig: { forceFit: true },
				tbar: tBar,
				bbar: bBar
			});
			grid.render();
		});
	</script>

</body>
</html>
