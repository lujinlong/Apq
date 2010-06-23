<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reg.aspx.cs" Inherits="dtxc.Reg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>会员注册</title>
	<link type="text/css" href="CSS/main.css" />
</head>
<body>
	<form id="form1" runat="server">
	<asp:ScriptManager ID="scReg" runat="server">
		<Services>
			<asp:ServiceReference Path="~/WS/WS2.asmx" />
		</Services>
		<Scripts>
			<asp:ScriptReference Path="~/Script/Apq.js" />
			<asp:ScriptReference Path="~/Script/Login.js" />
			<%--<asp:ScriptReference Path="~/My97DatePicker/WdatePicker.js" />--%>
		</Scripts>
	</asp:ScriptManager>
	<div>
		<div>
			<div>
				登录名:<asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
			</div>
			<div>
				密码:<input type="password" id="txtLoginPwd" value="" />
			</div>
			<div>
				密码确认:<input type="password" id="txtLoginPwd1" value="" />
			</div>
			<div>
				会员名(页面显示):<asp:TextBox ID="txtName" runat="server"></asp:TextBox>
				性别:<asp:DropDownList ID="ddlSex" runat="server">
					<asp:ListItem Value="1" Text="男"></asp:ListItem>
					<asp:ListItem Value="2" Text="女"></asp:ListItem>
				</asp:DropDownList>
			</div>
			<div>
				身份证:<asp:TextBox ID="txtIDCard" runat="server"></asp:TextBox>
				姓名:<input type="text" id="txtIDCard_Name" />
			</div>
			<div>
				支付宝帐号:<asp:TextBox ID="txtAlipay" runat="server"></asp:TextBox>
				生日:<asp:TextBox ID="txtBirthday" runat="server" CssClass="Wdate" onclick="WdatePicker({startDate:'1982-04-12',maxDate:'%y-%M-%d'})">1982-04-12</asp:TextBox>
			</div>
			<div>
				介绍人会员ID:<asp:TextBox ID="txtIntroUserID" runat="server" Enabled="false">0</asp:TextBox>
			</div>
		</div>
		<div align="center">
			<input type="button" id="btnReg" onclick="btnReg_Click()" value="注册" />
		</div>
	</div>

	<script type="text/javascript">
		function mdReg_Load() {
		}
		window.attachEvent("onload", mdReg_Load);

		function btnReg_Click() {
			// 获取页面值
			var UserName = $get("txtUserName").value;
			var LoginPwd = $get("txtLoginPwd").value;
			var LoginPwd1 = $get("txtLoginPwd1").value;
			var Name = $get("txtName").value;
			var Sex = $get("ddlSex").value;
			var IDCard = $get("txtIDCard").value;
			var IDCard_Name = $get("txtIDCard_Name").value;
			var Alipay = $get("txtAlipay").value;
			var Birthday = $get("txtBirthday").value;
			var IntroUserID = $get("txtIntroUserID").value;

			// 验证值
			if (UserName.length < 2) {
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
			if (Name.length < 1) {
				alert("会员名不能为空");
				$get("txtUserName").focus();
				return;
			}
			if (Alipay.length < 1) {
				alert("支付宝帐号不能为空");
				$get("txtAlipay").focus();
				return;
			}

			// 注册
			dtxc.WS.WS2.dtxc_Reg_UserName(Name, UserName, LoginPwd, Sex, "", IntroUserID, Alipay, 0, Birthday, IDCard, IDCard_Name, Sex, ""
					, btnReg_Click_Success);
		}

		function btnReg_Click_Success(stReturn) {
			ApqJS.using("ApqJS.Simulator");
			var MsgBox = new ApqJS.Simulator.Dialog(0, "提示", stReturn.ExMsg, 300, 0, 150, btnReg_Click_Success_Confirm);
			MsgBox.Show();
		}
		function btnReg_Click_Success_Confirm() {
			// 执行登录
			var strUserName = $get("txtUserName").value;
			var strLoginPwd = $get("txtLoginPwd").value;
			Login(strUserName, strLoginPwd);
		}
	</script>

	</form>
</body>
</html>
