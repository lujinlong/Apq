<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ifEmployee.aspx.cs" Inherits="Dinner.Admin.ifEmployee" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>员工管理</title>
</head>
<body>
	<div id="divEmployeeGrid">
	</div>

	<script type="text/javascript">
		function btnEmployeeUpdate_Click() {
			var grid = Ext.getCmp("gridEmployee");
			var ds = grid.getStore();
			var mr = ds.getModifiedRecords(); //获取所有更新过的记录
			var recordCount = ds.getCount(); //获取数据集中记录的数量
			if (mr.length == 0) {  // 确认修改记录数量
				Ext.Msg.alert("操作提示", "没有修改数据!");
				return false;
			}

			for (var i = 0; i < mr.length; i++) {
				Dinner.WS.Admin.WS1.Dinner_Employee_Update(mr[i].data["EmID"], mr[i].data["EmName"], mr[i].data["EmStatus"], mr[i].data["IsAdmin"], mr[i].data["LoginName"]);
			}
			Ext.Msg.alert("操作提示", "操作为异步方式,确认成功与否请点击刷新按钮.");
			ds.commitChanges();
		}

		Ext.onReady(function() {
			var store = new Ext.data.JsonStore({
				root: 'd.FNReturn.rows',
				totalProperty: 'd.POuts[1]',
				id: 'dsEmployee',
				fields: [
					{ name: 'EmID', type: 'int' },
					{ name: 'EmName', type: 'string' },
					{ name: 'IsAdmin', type: 'bool' },
					{ name: 'EmStatus', type: 'bool' },
					{ name: 'LoginID', type: 'int' },
					{ name: 'LoginName', type: 'string' },
					{ name: 'RegTime', type: 'date' }
				],
				proxy: new Ext.data.HttpProxy({
					ws: true,
					url: '../WS/Admin/WS1.asmx/Dinner_Employee_ListPager',
					method: "post"
				}),
				reader: new Ext.data.JsonReader(
					{ root: 'd.FNReturn.rows', totalProperty: 'd.total' },
					[{ name: 'EmID', mapping: 'EmID', type: 'int' },
					{ name: 'EmName', mapping: 'EmName' },
					{ name: 'IsAdmin', mapping: 'IsAdmin', type: 'bool' },
					{ name: 'EmStatus', mapping: 'EmStatus', type: 'bool' },
					{ name: 'LoginID', mapping: 'LoginID', type: 'int' },
					{ name: 'RegTime', mapping: 'RegTime' },
					{ name: 'LoginName', mapping: 'LoginName'}]
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
				displayMsg: '共 {2} 行', //'第 {0} 条到  {1} 条, 一共 {2} 条'
				items: [
					{ xtype: 'button', text: '提交', handler: btnEmployeeUpdate_Click }
				]
//				beforePageText : '页码',
//				afterPageText : '总页数 {0}',
//				firstText : '首页',
//				prevText : '上一页',
//				nextText : '下一页',
//				lastText : '末页',
//				refreshText: '刷新'
			});

			var grid = new Ext.grid.EditorGridPanel({
				id: 'gridEmployee',
				el: 'divEmployeeGrid',
				width: "100%",
				height: 520,
				store: store,
				columns: [
					{ header: "员工编号", dataIndex: "EmID" },
					{ header: "员工姓名", dataIndex: "EmName", editor: new Ext.form.TextField({ allowBlank: false }) },
					{ header: "是否管理员", dataIndex: "IsAdmin",
						editor: new Ext.form.ComboBox({
							mode: 'local',
							editable: false,
							triggerAction: 'all',
							store: new Ext.data.ArrayStore({
								id: "dsYesNo",
								fields: [
									'ID',
									'YesNo'
								],
								data: [[true, 'true'], [false, 'false']]
							}),
							valueField: 'ID',
							displayField: 'YesNo'
						})
					},
					{ header: "是否禁用", dataIndex: "EmStatus",
						editor: new Ext.form.ComboBox({
							mode: 'local',
							editable: false,
							triggerAction: 'all',
							store: new Ext.data.ArrayStore({
								id: "dsYesNo",
								fields: [
									'ID',
									'YesNo'
								],
								data: [[true, 'true'], [false, 'false']]
							}),
							valueField: 'ID',
							displayField: 'YesNo'
						})
					},
					{ header: "登录编号", dataIndex: "LoginID" },
					{ header: "登录名", dataIndex: "LoginName", editor: new Ext.form.TextField({ allowBlank: false }) },
					{ header: "注册时间", dataIndex: "RegTime", xtype: "datecolumn", format: 'Y-m-d H:i:s' }
				],
				sm: new Ext.grid.RowSelectionModel({ singleSelect: true }),
				viewConfig: { forceFit: true },
				bbar: pagingBar
			});
			grid.render();
			pagingBar.doRefresh();
		});
	</script>

</body>
</html>
