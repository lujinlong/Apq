<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tpAddinUp.aspx.cs" Inherits="dtxc.Admin.tpAddinUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>插件上传</title>
	<meta http-equiv="X-UA-Compatible" content="IE=7" />
	<link type="text/css" rel="stylesheet" href="/Ext-3.0.3/resources/css/ext-all.css" />
	<link type="text/css" rel="stylesheet" href="~/CSS/ExtMain.css" />
</head>
<body>
	<div id="divAddinUp">
		<form id="formAddinUp" runat="server">
		</form>
	</div>

	<script type="text/javascript">
		{
			// 1.页面初始化
			var tpAddinUp = new Ext.form.FormPanel({
				id: "formAddinUp",
				url: "tpAddinUp.aspx",
				fileUpload: true,
				items: [
					{ xtype: "textfield", inputType: "file", id: "tpAddinUp_fuAddin", fieldLabel: "请选择将要上传的插件",
						labelStyle: "width: 130px;"
					}
				],
				buttonAlign: "center",
				buttons: [
					{ xtype: "button", text: "上传", handler: btnUpload_Click }
				],
				renderTo: "divAddinUp"
			});

			// 2.上传
			function btnUpload_Click() {
				var formAddinUp = tpAddinUp.getForm();
				forAddinUp.action = "fuAddin.aspx";
				formAddinUp.submit({
					//waitTitle : "请稍候",
					waitMsg: "正在上传...",
					success: function(form, action) { },
					failure: function(form, action) {
						Ext.Msg.alert("业务错误", '上传失败！');
					}
				})
			}
		}
	</script>

</body>
</html>
