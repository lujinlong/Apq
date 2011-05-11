<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tpEmDinnerLog.aspx.cs"
	Inherits="Dinner.User.tpEmDinnerLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>订餐历史</title>
</head>
<body>
	<div id="divEmDinnerLog">
	</div>

	<script type="text/javascript">
		Ext.onReady(function() {
			var store = new Ext.data.JsonStore({
				root: 'd.FNReturn.rows',
				totalProperty: 'd.POuts[1]',
				id: 'dsEmDinnerLog',
				fields: [
					{ name: 'ID', type: 'int' },
					{ name: 'EmID', type: 'int' },
					{ name: 'FoodID', type: 'int' },
					{ name: 'FoodPrice', type: 'float' },
					{ name: 'FoodName', type: 'string' },
					{ name: 'RestID', type: 'int' },
					{ name: 'RestName', type: 'string' },
					{ name: 'State', type: 'bool' },
					{ name: 'DinnerTime', type: 'date' }
				],
				proxy: new Ext.data.HttpProxy({
					ws: true,
					url: '../WS/User/WS2.asmx/Dinner_EmDinner_ListPager',
					method: "post"
				}),
				reader: new Ext.data.JsonReader(
					{ root: 'd.FNReturn.rows', totalProperty: 'd.total' },
					[
						{ name: 'ID', mapping: 'ID', type: 'int' },
						{ name: 'EmID', mapping: 'EmID', type: 'int' },
						{ name: 'FoodID', mapping: 'FoodID', type: 'int' },
						{ name: 'FoodPrice', mapping: 'FoodPrice', type: 'float' },
						{ name: 'FoodName', mapping: 'FoodName' },
						{ name: 'RestID', mapping: 'RestID', type: 'int' },
						{ name: 'RestName', mapping: 'RestName' },
						{ name: 'State', mapping: 'State' },
						{ name: 'DinnerTime', mapping: 'DinnerTime' }
					]
				)
			});

			store.on('beforeload', function() {
				store.removeAll();
				store.commitChanges();
			});

			var pagingBar = new Ext.PagingToolbar({
				pageSize: 22,
				store: store,
				displayInfo: true,
				displayMsg: '共 {2} 行' //'第 {0} 条到  {1} 条, 一共 {2} 条'
			});

			var EmDinner_Delete_Success = function(stReturn) {
				if (stReturn.NReturn == 1) {
					Ext.Msg.alert("操作成功", "取消成功", function() { pagingBar.doRefresh(); });
				}
				else {
					Ext.Msg.alert("操作失败", stReturn.ExMsg);
				}
			};

			// 取消订餐
			var EmDinner_Delete = function() {
				var grid = Ext.getCmp("gridEmDinnerLog");
				var ds = grid.getStore();
				var sm = grid.getSelectionModel();

				var dr = sm.getSelected();
				Dinner.WS.User.WS2.Dinner_EmDinner_Delete(
					dr.data["ID"], EmDinner_Delete_Success, Apq_WS_Faild
				);
			};

			var tBar = new Ext.Toolbar({
				items: [
					{ xtype: 'button', text: '取消订餐', handler: EmDinner_Delete }
				]
			});

			var summary = new Ext.ux.grid.GridSummary();

			var smCheckBox = new Ext.grid.CheckboxSelectionModel({ checkOnly: true, singleSelect: true });

			var grid = new Ext.grid.GridPanel({
				id: 'gridEmDinnerLog',
				el: 'divEmDinnerLog',
				width: "100%",
				height: 560,
				store: store,
				plugins: summary,
				columns: [
					smCheckBox,
					{ header: "记录编号", dataIndex: "ID" },
					{ header: "餐馆名称", dataIndex: "RestName" },
					{ header: "菜品名称", dataIndex: "FoodName" },
					{ header: "价格", dataIndex: "FoodPrice", summaryType: 'sum' },
					{ header: "点餐时间", dataIndex: "DinnerTime", xtype: "datecolumn", format: 'Y-m-d H:i:s' },
					{ header: "是否已订餐", dataIndex: "State" }
				],
				sm: smCheckBox,
				viewConfig: { forceFit: true },
				tbar: tBar,
				bbar: pagingBar
			});
			grid.render();
			pagingBar.doRefresh();
		});
	</script>

</body>
</html>
