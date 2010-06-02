<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserTaskList.aspx.cs" Inherits="dtxc.Admin.UserTaskList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>任务结算</title>
	<meta http-equiv="X-UA-Compatible" content="IE=7" />
	<link type="text/css" rel="stylesheet" href="/Ext-3.0.3/resources/css/ext-all.css" />
	<link type="text/css" rel="stylesheet" href="~/CSS/ExtMain.css" />
</head>
<body>
	<form id="formUserTaskList" runat="server">
	</form>
	<div id="divUserTaskList">
	</div>

	<script type="text/javascript">
		{
			var ds, pagingBar, grid, gv;
			if (!Ext.state.Manager.get("User_UserTaskList_Status"))
				Ext.state.Manager.set("User_UserTaskList_Status", [1, 2]);

			// 页面控件生成 -----------------------------------------------------------------------
			ds = new Ext.data.Store({// 不能使用JsonStore
				proxy: new Ext.data.HttpProxy({
					url: "../WS/User/WS1.asmx/TaskListSelf",
					ws: true, // 表明调用WS
					method: "POST"
				}),
				reader: new Ext.data.JsonReader({
					idProperty: 'jReader',
					root: "d.FNReturn.rows",
					totalProperty: "d.POuts[1]",
					fields: [
						{ name: "TaskID" },
						{ name: "TaskName" },
						{ name: "TaskMoney" },
						{ name: 'Status' }
					]
				})
			});

			pagingBar = new Ext.PagingToolbar({
				store: ds,
				pageSize: 20,
				displayInfo: true
			});

			ds.on('beforeload', function(o, opt) {
				var Status = Ext.state.Manager.get("User_UserTaskList_Status");
				this.baseParams = Ext.apply(this.baseParams || {}, { "Status": Status });
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
						{header: "任务ID", dataIndex: 'TaskID' },
						{ header: "任务名", dataIndex: 'TaskName' },
						{ header: "任务总价", dataIndex: 'TaskMoney' },
						{ header: "状态", dataIndex: 'Status', renderer: RenderStatus }
					]
				}),
				store: ds,
				autoHeight: true,
				viewConfig: {
					forceFit: true,
					getRowClass: function(record, rowIndex, rp, ds) {
						rp.tstyle = ds.getAt(rowIndex).get("Status") == "1" ? 'color:red' : '';
						return "";
					}
				},
				//width: 600,
				//height: 300,
				frame: true,
				tbar: [
					{
						xtype: "checkbox",
						boxLabel: '未结算',
						checked: Ext.state.Manager.get("User_UserTaskList_Status").indexOf(1) != -1,
						handler: function(cb, checked) {
							if (checked) {
								ApqJS.Array.AddUnique(Ext.state.Manager.get("User_UserTaskList_Status"), 1);
							}
							else {
								Ext.state.Manager.get("User_UserTaskList_Status").remove(1);
							}
							pagingBar.moveFirst();
						}
					},
					'-',
					{
						xtype: "checkbox",
						boxLabel: '已结算',
						checked: Ext.state.Manager.get("User_UserTaskList_Status").indexOf(2) != -1,
						handler: function(cb, checked) {
							if (checked) {
								ApqJS.Array.AddUnique(Ext.state.Manager.get("User_UserTaskList_Status"), 2);
							}
							else {
								Ext.state.Manager.get("User_UserTaskList_Status").remove(2);
							}
							pagingBar.moveFirst();
						}
					}
				],
				bbar: pagingBar
			});

			function RenderStatus(value, metaData, record, rowIndex, colIndex, ds) {
				if (value == '1') {
					return "未结算";
				}
				if (value == '2') {
					return "已结算";
				}
				return value;
			}

			grid.render("divUserTaskList");
			gv = grid.getView();

			// 首次进入页面自动加载数据 -----------------------------------------------------------
			ds.load({
				params: {
					start: 0,
					limit: pagingBar.pageSize
				}
			});
		}
	</script>

</body>
</html>
