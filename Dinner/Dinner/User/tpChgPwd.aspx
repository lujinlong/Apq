<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tpChgPwd.aspx.cs" Inherits="Dinner.User.tpChgPwd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>登录信息</title>
</head>
<body>
	<form id="formChgPwd" runat="server">
	<div>
		<div>
			<span>姓 名:</span><asp:TextBox ID="txtEmName" runat="server" Enabled="false">0</asp:TextBox>
		</div>
		<div>
			<span>登录名:</span><asp:TextBox ID="txtLoginName" runat="server" Enabled="false">0</asp:TextBox>
		</div>
		<div>
			<span>原密码:</span><input type="password" id="txtLoginPwd_C" value="" />
		</div>
		<div>
			<span>新密码:</span><input type="password" id="txtLoginPwd" value="" />
		</div>
		<div>
			<span>密码确认:</span><input type="password" id="txtLoginPwd1" value="" />
		</div>
		<div align="center">
			<input type="button" id="btnConfirm" onclick="btnConfirm_Click()" value="修改" />
		</div>
	</div>
	</form>

	<script type="text/javascript">
		function btnConfirm_Click() {
			// 获取页面值
			var LoginPwd_C = Ext.getDom("txtLoginPwd_C").value;
			var LoginPwd = Ext.getDom("txtLoginPwd").value;
			var LoginPwd1 = Ext.getDom("txtLoginPwd1").value;

			if (!LoginPwd_C) {
				Ext.Msg.alert("输入有误", "原密码不能为空", function() { Ext.getDom("txtLoginPwd_C").focus(); });
				return;
			}
			if (!LoginPwd) {
				Ext.Msg.alert("输入有误", "新密码不能为空", function() { Ext.getDom("txtLoginPwd").focus(); });
				return;
			}
			if (!LoginPwd1) {
				Ext.Msg.alert("输入有误", "密码确认不能为空", function() { Ext.getDom("txtLoginPwd1").focus(); });
				return;
			}
			if (LoginPwd != LoginPwd1) {
				Ext.Msg.alert("输入有误", "新密码两次输入不一致", function() { Ext.getDom("txtLoginPwd").focus(); });
				return;
			}

			if (LoginPwd_C && LoginPwd && LoginPwd1 && LoginPwd == LoginPwd1) {
				// 修改
				Dinner.WS.User.WS2.UserEditLoginPwd(LoginPwd_C, LoginPwd, btnConfirm_Click_Success, Apq_WS_Faild);
			}
		}

		function btnConfirm_Click_Success(stReturn) {
			Ext.Msg.alert("修改结果", stReturn.ExMsg, btnConfirm_Click_Success_alertClick, stReturn);
		}

		function btnConfirm_Click_Success_alertClick() {
			if (this.NReturn == 1) {
				// 执行登录
				var strLoginName = Ext.state.Cookies.get("LoginName");
				var strLoginPwd = Ext.getDom("txtLoginPwd").value;
				if (strLoginName && strLoginPwd) {
					// 先清空 Cookie
					Ext.state.Cookies.clearCookie("SqlLoginPwd");

					Dinner.WS.WS2.Login_LoginName(strLoginName, strLoginPwd, Login_Sucess, Apq_WS_Faild);
				}
			}
		}

		function Login_Sucess(stReturn) {
			if (stReturn.NReturn != 1) {
				Ext.Msg.alert("错误", stReturn.ExMsg);
				return;
			}

			// Cookie 操作
			Ext.state.Cookies.set("SqlLoginPwd", stReturn.FNReturn);
		}
	</script>

</body>
</html>
