<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mdTaskEdit.aspx.cs" Inherits="dtxc.Admin.mdTaskEdit" %>

<!-- 阻止文档格式声明 -->
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>任务编辑</title>
	<link type="text/css" href="../CSS/main.css" />
</head>
<body>
	<form id="form1" runat="server" style="margin: 0px;">
	<asp:ScriptManager ID="scTaskEdit" runat="server">
		<Services>
			<asp:ServiceReference Path="~/WS/User/WS1.asmx" />
		</Services>
		<Scripts>
			<asp:ScriptReference Path="~/My97DatePicker/WdatePicker.js" />
			<asp:ScriptReference Path="~/Script/Apq.js" />
		</Scripts>
	</asp:ScriptManager>
	<div style="width: 100%">
		<div>
			任务编号:<asp:TextBox ID="txtTaskID" runat="server" Enabled="false">0</asp:TextBox>
			任务名称:<asp:TextBox ID="txtTaskName" runat="server"></asp:TextBox>
			<asp:CheckBox ID="cbNeedChangeIP" runat="server" Text="需要更改IP" Checked="true" />
			<asp:CheckBox ID="cbIsAutoStart" runat="server" Text="自动开始" Checked="true" />
		</div>
		<div>
			插件选择:<asp:TextBox ID="txtAddinID" runat="server" onclick="txtAddinID_Click(event);"
				ReadOnly="true" Width="400">0</asp:TextBox>
		</div>
		<div>
			开始时间:<asp:TextBox ID="txtBTime" runat="server" CssClass="Wdate" onclick="WdatePicker({minDate:'%y-%M-#{%d}',maxDate:'#F{$dp.$D(\'txtETime\')}'})"></asp:TextBox>
			结束时间:<asp:TextBox ID="txtETime" runat="server" CssClass="Wdate" onclick="WdatePicker({minDate:'#F{$dp.$D(\'txtBTime\')}'})"></asp:TextBox>
		</div>
		<div>
			任务内容:</div>
		<div>
			<asp:TextBox ID="txtTaskContent" runat="server" Style="width: 100%" Rows="12" TextMode="MultiLine"></asp:TextBox>
		</div>
		<div>
			总价:<asp:TextBox ID="txtTaskMoney" runat="server">0</asp:TextBox>
			单价:<asp:TextBox ID="txtPrice" runat="server">0</asp:TextBox>
			上级单价:<asp:TextBox ID="txtParentPrice" runat="server">0</asp:TextBox>
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

		function mdTaskEdit_Load() {
		}
		window.attachEvent("onload", mdTaskEdit_Load);

		function btnConfirm_Click() {
			var m = ApqJS.location.QueryString["m"];
			if (m == "a" || m == "A"
				|| m == "e" || m == "E") {
				// 获取页面值
				var TaskID = $get("txtTaskID").value;
				var TaskName = $get("txtTaskName").value;
				var NeedChangeIP = $get("cbNeedChangeIP").checked;
				var IsAutoStart = $get("cbIsAutoStart").checked;
				var BTime = $get("txtBTime").value;
				var ETime = $get("txtETime").value;
				var TaskContent = $get("txtTaskContent").value;
				var AddinID = $get("txtAddinID").value;
				var TaskMoney = $get("txtTaskMoney").value;
				var Price = $get("txtPrice").value;
				var ParentPrice = $get("txtParentPrice").value;

				switch (m) {
					// 添加 
					case "a":
					case "A":
						dtxc.WS.User.WS1.TaskAdd(TaskName, TaskContent, BTime, ETime, AddinID, Price, ParentPrice, NeedChangeIP, IsAutoStart, TaskMoney
						, btnConfirm_Click_Success);
						break;
					// 修改 
					default:
						dtxc.WS.User.WS1.TaskEdit(TaskID, TaskName, TaskContent, BTime, ETime, AddinID, Price, ParentPrice, NeedChangeIP, IsAutoStart, TaskMoney
						, btnConfirm_Click_Success);
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

		function txtAddinID_Click(e) {
			var txtAddinID = ApqJS.$("txtAddinID");
			if (!txtAddinID.disabled) {
				var objLookup = txtAddinID.Lookup;
				if (!objLookup) {
					ApqJS.using("ApqJS.Simulator");
					var strUrl = "ifAddinList.aspx";
					var objQueryString = ApqJS.Object.Copy(ApqJS.location.QueryString);
					objQueryString.t = Math.random();
					objQueryString.l = 1;
					objQueryString.ll = txtAddinID.value || 0;
					objQueryString.pe = "txtAddinID";

					// 计算宽度和位置
					var nWidth = txtAddinID.offsetWidth;
					var nLeft = ApqJS.document.GetAbsLeft(txtAddinID);
					var nTop = ApqJS.document.GetAbsTop(txtAddinID) + txtAddinID.offsetHeight;
					objLookup = new ApqJS.Simulator.Dialog(2, "插件选择", strUrl + "?" + ApqJS.location.BuildSearch(objQueryString), nWidth, nLeft, nTop, btnConfirm_Click_Success_Confirm);
					txtAddinID.Lookup = objLookup;
				}
				objLookup.Show();
			}
		}
	</script>

	</form>
</body>
</html>
