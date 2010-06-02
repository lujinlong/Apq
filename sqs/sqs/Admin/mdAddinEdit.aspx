<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mdAddinEdit.aspx.cs" Inherits="dtxc.Admin.mdAddinEdit" %>

<!-- 阻止文档格式声明 -->
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>插件编辑</title>
	<link type="text/css" href="../CSS/main.css" />
</head>
<body>
	<form id="form1" runat="server" style="margin: 0px;">
	<asp:ScriptManager ID="scAddinEdit" runat="server">
		<Services>
			<asp:ServiceReference Path="~/WS/Admin/WS1.asmx" />
		</Services>
		<Scripts>
			<%--<asp:ScriptReference Path="~/My97DatePicker/WdatePicker.js" />--%>
			<asp:ScriptReference Path="~/Script/Apq.js" />
		</Scripts>
	</asp:ScriptManager>
	<div style="width: 100%">
		<div>
			插件编号:<asp:TextBox ID="txtAddinID" runat="server" Enabled="false">0</asp:TextBox>
			插件名称:<asp:TextBox ID="txtAddinName" runat="server"></asp:TextBox>
		</div>
		<div>
			插件地址:<asp:TextBox ID="txtAddinUrl" runat="server"></asp:TextBox></div>
		<div>
			插件描述:</div>
		<div>
			<asp:TextBox ID="txtAddinDescript" runat="server" Style="width: 100%" Rows="12" TextMode="MultiLine"></asp:TextBox>
		</div>
		<div align="center">
			<input type="button" id="btnConfirm" onclick="btnConfirm_Click()" value="确定" />
			<input type="button" id="btnCancel" onclick="btnCancel_Click()" value="取消" />
		</div>
	</div>

	<script type="text/javascript">
		if (window.attachEvent) {
			window.attachEvent("onload", ApqJS.document.iframeAutoFit);
		}
		else if (window.addEventListener) {
			window.addEventListener('load', ApqJS.document.iframeAutoFit, false);
		}

		function mdAddinEdit_Load() {
		}
		window.attachEvent("onload", mdAddinEdit_Load);

		function btnConfirm_Click() {
			var m = ApqJS.location.QueryString["m"];
			if (m == "a" || m == "A"
				|| m == "e" || m == "E") {
				// 获取页面值
				var AddinID = $get("txtAddinID").value;
				var AddinName = $get("txtAddinName").value;
				var AddinUrl = $get("txtAddinUrl").value;
				var AddinDescript = $get("txtAddinDescript").value;

				switch (m) {
					// 添加 
					case "a":
					case "A":
						dtxc.WS.Admin.WS1.AddinAdd(AddinName, AddinUrl, AddinDescript, btnConfirm_Click_Success);
						break;
					// 修改 
					default:
						dtxc.WS.Admin.WS1.AddinEdit(AddinID, AddinName, AddinUrl, AddinDescript, btnConfirm_Click_Success);
						break;
				}
			}
			else {
				btnCancel_Click();
			}
		}

		// 关闭模式对话框
		function btnCancel_Click() {
			var iFrame = ApqJS.document.FindIFrameElement();
			if (iFrame) {
				iFrame.parentNode.ApqDialog.Close();
			}
		}

		function btnConfirm_Click_Success(stReturn) {
			ApqJS.using("ApqJS.Simulator");
			var MsgBox = new ApqJS.Simulator.Dialog(0, "提示", stReturn.ExMsg, 300, 0, 150, btnConfirm_Click_Success_Confirm);
			MsgBox.Show();
		}
		function btnConfirm_Click_Success_Confirm() {
			parent.Apq_Pager_First.call(parent);
		}
	</script>

	</form>
</body>
</html>
