<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tpStatFood.aspx.cs" Inherits="Dinner.Admin.tpStatFood" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>点餐记录</title>
</head>
<body>
	<div id="divStatFood">
	</div>

	<script type="text/javascript">
		Ext.onReady(function() {
			var store = new Ext.data.JsonStore({
				root: 'd.FNReturn.rows',
				totalProperty: 'd.POuts[1]',
				id: 'dsStatFood',
				fields: [
					{ name: 'FoodID', type: 'int' },
					{ name: 'FoodMoney', type: 'float' },
					{ name: 'FoodName', type: 'string' },
					{ name: 'RestID', type: 'int' },
					{ name: 'RestName', type: 'string' },
					{ name: 'FoodCount', type: 'int' },
					{ name: 'DinnerTime', type: 'date' }
				],
				proxy: new Ext.data.HttpProxy({
					ws: true,
					url: '../WS/Admin/WS1.asmx/Dinner_Stat_EmDinner_Food',
					method: "post"
				}),
				reader: new Ext.data.JsonReader(
					{ root: 'd.FNReturn.rows', totalProperty: 'd.total' },
					[
						{ name: 'FoodID', mapping: 'FoodID', type: 'int' },
						{ name: 'FoodMoney', mapping: 'FoodMoney', type: 'float' },
						{ name: 'FoodName', mapping: 'FoodName' },
						{ name: 'RestID', mapping: 'RestID', type: 'int' },
						{ name: 'RestName', mapping: 'RestName' },
						{ name: 'FoodCount', mapping: 'FoodCount', type: 'int' },
						{ name: 'DinnerTime', mapping: 'DinnerTime' }
					]
				)
			});

			var tBar;

			var doDinner_Success = function(stReturn) {
				if (stReturn.NReturn == 1) {
					tBar.get("StatFood_qIsDoDinner").setValue(true);
					store.load();
				}
				else Ext.Msg.alert("错误", stReturn.ExMsg);
			};

			//确认订餐
			var doDinner = function(BTime, ETime) {
				Dinner.WS.Admin.WS1.Dinner_Admin_EmDinner_DoDinner(BTime, ETime, doDinner_Success, Apq_WS_Faild);
			};

			var initBTime = new Date();
			var initETime = new Date();
			initBTime.setHours(initBTime.getHours() - 4);

			initBTime = initBTime.getHours() + ":" + initBTime.getMinutes();
			initETime = initETime.getHours() + ":" + initETime.getMinutes();
			tBar = new Ext.Toolbar({
				items: [
					{ xtype: 'label', text: '起止时间' },
					{ xtype: 'datefield', id: 'StatFood_dBDate', format: 'Y-m-d', value: new Date() },
					{ xtype: 'timefield', id: 'StatFood_dBTime', width: 60, editable: false, format: 'H:i', value: initBTime },
					'至',
					{ xtype: 'datefield', id: 'StatFood_dEDate', format: 'Y-m-d', value: new Date() },
					{ xtype: 'timefield', id: 'StatFood_dETime', width: 60, editable: false, format: 'H:i', value: initETime },
					{ xtype: 'checkbox', id: 'StatFood_qIsDoDinner' },
					{ xtype: 'label', text: '已确认', forId: 'StatFood_qIsDoDinner' },
					{ xtype: 'button', text: "查询", handler: function() { store.load(); } },
					{ xtype: 'button', text: "确认订餐",
						handler: function() {
							var BDate = tBar.get("StatFood_dBDate").getValue(); //Date
							var BTime = tBar.get("StatFood_dBTime").getValue(); //String
							var EDate = tBar.get("StatFood_dEDate").getValue();
							var ETime = tBar.get("StatFood_dETime").getValue();

							BTime = BDate.format("Y-m-d") + " " + BTime + ":00";
							ETime = EDate.format("Y-m-d") + " " + ETime + ":00";
							doDinner(BTime, ETime);
						}
					}
				]
			});

			store.on('beforeload', function() {
				store.removeAll();
				store.commitChanges();

				var BDate = tBar.get("StatFood_dBDate").getValue(); //Date
				var BTime = tBar.get("StatFood_dBTime").getValue(); //String
				var EDate = tBar.get("StatFood_dEDate").getValue();
				var ETime = tBar.get("StatFood_dETime").getValue();
				var IsDoDinner = tBar.get("StatFood_qIsDoDinner").getValue() || false;

				BTime = BDate.format("Y-m-d") + " " + BTime + ":00";
				ETime = EDate.format("Y-m-d") + " " + ETime + ":00";

				Ext.apply(this.baseParams, { BTime: BTime, ETime: ETime, State: IsDoDinner });
			});

			var summary = new Ext.ux.grid.GridSummary();

			var grid = new Ext.grid.EditorGridPanel({
				id: 'gridStatFood',
				el: 'divStatFood',
				width: "100%",
				height: 520,
				store: store,
				plugins: summary,
				columns: [
					{ header: "点餐日期", dataIndex: "DinnerTime", xtype: "datecolumn", format: 'Y-m-d' },
					{ header: "餐馆名称", dataIndex: "RestName", sortable: true },
					{ header: "菜品名称", dataIndex: "FoodName", sortable: true },
					{ header: "份数", dataIndex: "FoodCount", summaryType: 'sum' },
					{ header: "总价", dataIndex: "FoodMoney", summaryType: 'sum' }
				],
				sm: new Ext.grid.RowSelectionModel({ singleSelect: true }),
				viewConfig: { forceFit: true },
				tbar: tBar
			});
			grid.render();
			store.load();
		});
	</script>

</body>
</html>
