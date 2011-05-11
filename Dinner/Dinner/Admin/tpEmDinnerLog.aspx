<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tpEmDinnerLog.aspx.cs"
	Inherits="Dinner.Admin.tpEmDinnerLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>点餐记录</title>
</head>
<body>
	<div id="divEmDinnerLog">
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
					url: '../WS/Admin/WS1.asmx/Dinner_Restaurant_List',
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

			var store = new Ext.data.JsonStore({
				root: 'd.FNReturn.rows',
				totalProperty: 'd.POuts[1]',
				id: 'dsEmDinnerLog',
				fields: [
					{ name: 'ID', type: 'int' },
					{ name: 'EmID', type: 'int' },
					{ name: 'EmName', type: 'string' },
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
					url: '../WS/Admin/WS1.asmx/Dinner_Admin_EmDinner_ListPager',
					method: "post"
				}),
				reader: new Ext.data.JsonReader(
					{ root: 'd.FNReturn.rows', totalProperty: 'd.total' },
					[
						{ name: 'ID', mapping: 'ID', type: 'int' },
						{ name: 'EmID', mapping: 'EmID', type: 'int' },
						{ name: 'EmName', mapping: 'EmName' },
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

			var tBar = new Ext.Toolbar({
				items: [
					{ xtype: 'label', text: '起止日期' },
					{ xtype: 'datefield', id: 'dBTime', format: 'Y-m-d', value: new Date() },
					'至',
					{ xtype: 'datefield', id: 'dETime', format: 'Y-m-d', value: new Date() },
					{ xtype: 'label', text: '餐馆' },
					{ xtype: 'combo', id: "cbRest",
						mode: 'remote',
						editable: false,
						triggerAction: 'all',
						store: dsRest,
						valueField: 'RestID',
						displayField: 'RestName',
						width: 200
					},
					{ xtype: 'checkbox', id: 'EmDinnerLog_qIsDoDinner' },
					{ xtype: 'label', text: '已确认', forId: 'EmDinnerLog_qIsDoDinner' },
					{ xtype: 'button', text: "查询", handler: function() { pagingBar.doRefresh(); } }
				]
			});

			store.on('beforeload', function() {
				store.removeAll();
				store.commitChanges();

				var RestID = tBar.get("cbRest").getValue() || 0;
				var BTime = tBar.get("dBTime").getValue();
				var ETime = tBar.get("dETime").getValue();
				var IsDoDinner = tBar.get("EmDinnerLog_qIsDoDinner").getValue() || false;
				ETime.setHours(24);

				Ext.apply(this.baseParams, { RestID: RestID, BTime: BTime, ETime: ETime, IsDoDinner: IsDoDinner });
			});

			var pagingBar = new Ext.PagingToolbar({
				pageSize: 20,
				store: store,
				displayInfo: true,
				displayMsg: '共 {2} 行' //'第 {0} 条到  {1} 条, 一共 {2} 条'
			});

			var summary = new Ext.ux.grid.GridSummary();

			var grid = new Ext.grid.EditorGridPanel({
				id: 'gridEmDinnerLog',
				el: 'divEmDinnerLog',
				width: "100%",
				height: 520,
				store: store,
				plugins: summary,
				columns: [
					{ header: "记录编号", dataIndex: "ID" },
					{ header: "姓名", dataIndex: "EmName" },
					{ header: "餐馆名称", dataIndex: "RestName"/*, sortable: true*/ },
					{ header: "菜品名称", dataIndex: "FoodName"/*, sortable: true*/ },
					{ header: "价格", dataIndex: "FoodPrice", summaryType: 'sum' },
					{ header: "点餐时间", dataIndex: "DinnerTime", xtype: "datecolumn", format: 'Y-m-d H:i:s' },
					{ header: "是否已订餐", dataIndex: "State" }
				],
				sm: new Ext.grid.RowSelectionModel({ singleSelect: true }),
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
