<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mdUserEdit.aspx.cs" Inherits="dtxc.Admin.mdUserEdit" %>

<!-- 阻止文档格式声明 -->
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>会员编辑</title>
	<link type="text/css" href="../CSS/main.css" />
</head>
<body>
	<form id="form1" runat="server" style="margin: 0px;">
	<asp:ScriptManager ID="scUserEdit" runat="server">
		<Services>
			<asp:ServiceReference Path="~/WS/User/WS1.asmx" />
			<asp:ServiceReference Path="~/WS/Admin/WS1.asmx" />
		</Services>
		<Scripts>
			<asp:ScriptReference Path="~/Script/Apq.js" />
			<%--<asp:ScriptReference Path="~/My97DatePicker/WdatePicker.js" />--%>
		</Scripts>
	</asp:ScriptManager>
	<div style="width: 100%">
		<div>
			会员编号:<asp:TextBox ID="txtUserID" runat="server" Enabled="false">0</asp:TextBox>
			会员名:<asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
			性别:<asp:DropDownList ID="ddlSex" runat="server">
				<asp:ListItem Value="0" Text="未知"></asp:ListItem>
				<asp:ListItem Value="1" Text="男"></asp:ListItem>
				<asp:ListItem Value="2" Text="女"></asp:ListItem>
			</asp:DropDownList>
		</div>
		<div>
			<asp:CheckBox ID="cbStatus" runat="server" Text="启用" Checked="true" />
			<asp:CheckBox ID="cbIsAdmin" runat="server" Text="是否管理员" />
			<asp:CheckBox ID="cbIsTeam" runat="server" Text="是否团队" />
			身份证:<asp:TextBox ID="txtIDCard" runat="server"></asp:TextBox>
		</div>
		<div>
			支付宝帐号:<asp:TextBox ID="txtAlipay" runat="server"></asp:TextBox>
			生日:<asp:TextBox ID="txtBirthday" runat="server" CssClass="Wdate" onclick="WdatePicker({startDate:'1982-04-12',maxDate:'%y-%M-%d'});ApqJS.document.iframeAutoFit();"></asp:TextBox>
		</div>
		<div>
			介绍人:<asp:TextBox ID="txtIntroUserID" runat="server" onclick="txtIntroUserID_Click(event);"
				ReadOnly="true" Width="400" Enabled="false">0</asp:TextBox>
		</div>
		<div>
			过期日期:<asp:TextBox ID="txtExpire" runat="server" CssClass="Wdate" onclick="WdatePicker()"></asp:TextBox>
			注册时间:<asp:TextBox ID="txtRegTime" runat="server" CssClass="Wdate" onclick="WdatePicker()"
				Enabled="false"></asp:TextBox>
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

		function mdUserEdit_Load() {
		}
		window.attachEvent("onload", mdUserEdit_Load);

		function btnConfirm_Click() {
			var m = ApqJS.location.QueryString["m"];
			if (m == "a" || m == "A"
				|| m == "e" || m == "E") {
				// 获取页面值
				var UserID = $get("txtUserID").value;
				var UserName = $get("txtUserName").value;
				var Sex = $get("ddlSex").value;
				var Expire = $get("txtExpire").value;
				var Status = !($get("cbStatus").checked);
				var IsAdmin = $get("cbIsAdmin").checked;
				var Birthday = $get("txtBirthday").value;
				var IDCard = $get("txtIDCard").value;
				var Alipay = $get("txtAlipay").value;

				// 修改
				dtxc.WS.Admin.WS1.UserEdit(UserID, UserName, Sex, '', Expire, Status, IsAdmin, Birthday, IDCard, Alipay
					, btnConfirm_Click_Success);
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

		function txtIntroUserID_Click(e) {
			var txtIntroUserID = ApqJS.$("txtIntroUserID");
			if (!txtIntroUserID.disabled) {
				var objLookup = txtIntroUserID.Lookup;
				if (!objLookup) {
					ApqJS.using("ApqJS.Simulator");
					var strUrl = "ifUserList.aspx";
					var objQueryString = ApqJS.Object.Copy(ApqJS.location.QueryString);
					objQueryString.t = Math.random();
					objQueryString.pe = "txtIntroUserID";
					objQueryString.l = 1;
					objQueryString.ll = txtIntroUserID.value || 0;

					// 计算宽度和位置
					var nWidth = txtIntroUserID.offsetWidth;
					var nLeft = ApqJS.document.GetAbsLeft(txtIntroUserID);
					var nTop = ApqJS.document.GetAbsTop(txtIntroUserID) + txtIntroUserID.offsetHeight;
					objLookup = new ApqJS.Simulator.Dialog(2, "介绍人选择", strUrl + "?" + ApqJS.location.BuildSearch(objQueryString), nWidth, nLeft, nTop, btnConfirm_Click_Success_Confirm);
					txtIntroUserID.Lookup = objLookup;
				}
				objLookup.Show();
			}
		}
	</script>

	</form>
</body>
</html>
