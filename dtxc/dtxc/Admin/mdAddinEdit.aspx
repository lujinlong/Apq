<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mdAddinEdit.aspx.cs" Inherits="dtxc.Admin.mdAddinEdit" %>

<!-- 阻止文档格式声明 -->
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>插件编辑</title>
	<meta http-equiv="X-UA-Compatible" content="IE=7" />
	<link type="text/css" rel="stylesheet" href="/Ext-3.0.3/resources/css/ext-all.css" />
	<link type="text/css" rel="stylesheet" href="~/CSS/ExtMain.css" />
</head>
<body>
	<form id="formAddinEdit" runat="server">
	</form>
	<div id="div_mdAddinEdit">
	</div>

	<script type="text/javascript">
		{
			var m = Ext.state.Manager.get("winAddinEdit_m");

			// 4.操作成功
			var btnConfirm_Click_Success = function(stReturn) {
				Ext.Msg.alert("提示", stReturn.ExMsg);

				// 刷新列表
				Ext.getCmp("pbAddinList").doRefresh();

				// 隐藏窗口
				Ext.getCmp("winAddinEdit").hide();
			};

			// 3.确定
			var btnConfirm_Click = function() {
				if (m == "a" || m == "A"
				|| m == "e" || m == "E") {
					// 获取页面值
					var AddinID = Ext.getDom("mdAddinEdit_txtAddinID").value;
					var AddinName = Ext.getDom("mdAddinEdit_txtAddinName").value;
					var AddinUrl = Ext.getDom("mdAddinEdit_txtAddinUrl").value;
					var AddinDescript = Ext.getDom("mdAddinEdit_txtAddinDescript").innerText;

					switch (m) {
						// 添加     
						case "a":
						case "A":
							dtxc.WS.Admin.WS1.AddinAdd(AddinName, AddinUrl, AddinDescript, btnConfirm_Click_Success, Apq_WS_Faild);
							break;
						// 修改     
						default:
							dtxc.WS.Admin.WS1.AddinEdit(AddinID, AddinName, AddinUrl, AddinDescript, btnConfirm_Click_Success, Apq_WS_Faild);
							break;
					}
				}
				else {
					Ext.getCmp("winAddinEdit").hide();
				}
			};

			// 2.取得数据以后的处理
			var AddinListOne_Success = function(stReturn) {
				var mdAddinEdit = new Ext.form.FormPanel({
					id: "mdAddinEdit",
					defaults: { readOnly: (Ext.state.Manager.get("winAddinEdit_m") == "v" || Ext.state.Manager.get("winAddinEdit_m") == "V") },
					items: [
					{ xtype: "textfield", id: "mdAddinEdit_txtAddinID", fieldLabel: "插件编号", value: stReturn.FNReturn.rows[0]["AddinID"] },
					{ xtype: "textfield", id: "mdAddinEdit_txtAddinName", fieldLabel: "插件名称", value: stReturn.FNReturn.rows[0]["AddinName"] },
					{ xtype: "textfield", id: "mdAddinEdit_txtAddinUrl", fieldLabel: "插件地址", value: stReturn.FNReturn.rows[0]["AddinUrl"] },
					{ xtype: "textarea", id: "mdAddinEdit_txtAddinDescript", fieldLabel: "插件描述", value: stReturn.FNReturn.rows[0]["AddinDescript"] }
				],
					buttons: [
					{ xtype: "button", text: "确定", handler: btnConfirm_Click },
					{ xtype: "button", text: "取消", handler: function() { Ext.getCmp("winAddinEdit").hide(); } }
				],
					renderTo: "div_mdAddinEdit"
				});
			};

			// 1.获取数据
			if (m == "a" || m == "A"
				|| m == "e" || m == "E") {
				dtxc.WS.Admin.WS1.AddinListOne(Ext.state.Manager.get("winAddinEdit_AddinID"), AddinListOne_Success, Apq_WS_Faild);
			}
		}
	</script>

</body>
</html>
