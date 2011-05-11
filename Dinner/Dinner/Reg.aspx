<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reg.aspx.cs" Inherits="Dinner.Reg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>员工注册</title>
	<link type="text/css" rel="stylesheet" href="/ext-3.2.1/resources/css/ext-all.css" />
	<link type="text/css" rel="stylesheet" href="~/CSS/ExtMain.css" />
</head>
<body>
	<form id="form1" runat="server">
	<asp:ScriptManager ID="scReg" runat="server">
		<Scripts>
			<%--<asp:ScriptReference Path="/ext-3.2.1/vswd-ext_2.2.js" />--%>
			<asp:ScriptReference Path="/ext-3.2.1/adapter/ext/ext-base.js" />
			<asp:ScriptReference Path="/ext-3.2.1/ext-all.js" />
			<asp:ScriptReference Path="/ext-3.2.1/ext-basex.js" />
			<asp:ScriptReference Path="/ext-3.2.1/src/locale/ext-lang-zh_CN.js" />
			<asp:ScriptReference Path="~/Script/ShowEx.js" />
			<asp:ScriptReference Path="/ApqJS/ExtJS.js" />
			<asp:ScriptReference Path="/ApqJS/ApqJS.js" />
			<%--<asp:ScriptReference Path="~/Script/Login.js" />--%>
			<%--<asp:ScriptReference Path="~/My97DatePicker/WdatePicker.js" />--%>
		</Scripts>
		<Services>
			<asp:ServiceReference Path="~/WS/WS2.asmx" />
		</Services>
	</asp:ScriptManager>
	<div>
		<div>
			员工姓名:<asp:TextBox ID="txtEmName" runat="server"></asp:TextBox>
		</div>
		<div>
			登录名:<asp:TextBox ID="txtLoginName" runat="server"></asp:TextBox>
		</div>
		<div>
			密码:<input type="password" id="txtLoginPwd" value="" />
		</div>
		<div>
			密码确认:<input type="password" id="txtLoginPwd1" value="" />
		</div>
		<div align="center">
			<input type="button" id="btnReg" onclick="btnReg_Click()" value="注册" />
		</div>
	</div>

	<script type="text/javascript">
		if (window.attachEvent) {
			window.attachEvent("onload", ApqJS.document.iframeAutoFit);
		}
		else if (window.addEventListener) {
			window.addEventListener('load', ApqJS.document.iframeAutoFit, false);
		}

		// 生成页面
		Ext.onReady(function() {
			/*
			// 默认值[开发使用]
			Ext.getDom("txtEmName").value = "黄宗银";
			Ext.getDom("txtLoginName").value = "Apq";
			Ext.getDom("txtLoginPwd").value = "apq";
			Ext.getDom("txtLoginPwd1").value = "apq";
			*/
		});
		
		function mdReg_Load() {
		}
		window.attachEvent("onload", mdReg_Load);

		function btnReg_Click() {
			// 获取页面值
			var LoginName = $get("txtLoginName").value;
			var LoginPwd = $get("txtLoginPwd").value;
			var LoginPwd1 = $get("txtLoginPwd1").value;
			var EmName = $get("txtEmName").value;
//			var Sex = $get("ddlSex").value;
//			var IDCard = $get("txtIDCard").value;
//			var IDCard_Name = $get("txtIDCard_Name").value;
//			var Alipay = $get("txtAlipay").value;
//			var Birthday = $get("txtBirthday").value;
//			var IntroUserID = $get("txtIntroUserID").value;

			// 输入验证
			if (EmName.length < 1) {
				alert("员工姓名不能为空");
				$get("txtUserName").focus();
				return;
			}
			if (LoginName.length < 2) {
				alert("登录名不能少于2个字符");
				$get("txtUserName").focus();
				return;
			}
			if (LoginPwd.length < 3) {
				alert("密码不能少于3个字符");
				$get("txtLoginPwd").focus();
				return;
			}
			if (LoginPwd1 != LoginPwd) {
				alert("两次输入的密码不一致");
				$get("txtLoginPwd").focus();
				return;
			}
//			if (Alipay.length < 1) {
//				alert("支付宝帐号不能为空");
//				$get("txtAlipay").focus();
//				return;
//			}

			// 注册
			Dinner.WS.WS2.Dinner_RegEmployee(EmName, LoginName, LoginPwd, btnReg_Click_Success, Apq_WS_Faild);
		}

		function btnReg_Click_Success(stReturn) {

			var btnReg_Click_Success_Confirm = function(buttonId, text, MsgBoxCfg) {
				if (buttonId == "ok") {
					// 将登录名密码存入cookie,转到main.aspx
					Ext.state.Cookies.set("LoginName", stReturn.POuts[2]);
					Ext.state.Cookies.set("SqlLoginPwd", stReturn.POuts[3]);
					top.location = "User/Main.aspx";
				}
				else {
					Ext.getDom("txtEmName").value = "";
					Ext.getDom("txtLoginName").value = "";
					Ext.getDom("txtLoginPwd").value = "";
					Ext.getDom("txtLoginPwd1").value = "";
				}
			};
			
//			ApqJS.using("ApqJS.Simulator");
//			var MsgBox = new ApqJS.Simulator.Dialog(0, "提示", stReturn.ExMsg, 300, 0, 150, btnReg_Click_Success_Confirm);
//			MsgBox.Show();

			Ext.Msg.show({
				title: '注册成功',
				msg: '注册成功,点击“确定”自动以该员工登录,“取消”再次注册',
				//buttons: Ext.MessageBox.OK,//OKCANCEL
				buttons: { ok: '确定'/*, cancel: 'Bar'*/ },
				fn: btnReg_Click_Success_Confirm
			});
		}
	</script>

	</form>
</body>
</html>
