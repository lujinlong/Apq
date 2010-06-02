<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ifUserInfo.aspx.cs" Inherits="dtxc.User.ifUserInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>个人信息</title>
	<link type="text/css" href="../CSS/main.css" />
</head>
<body>
	<form id="form1" runat="server">
	<asp:ScriptManager ID="scUserInfo" runat="server">
		<Services>
			<asp:ServiceReference Path="../WS/User/WS2.asmx" />
		</Services>
		<Scripts>
			<asp:ScriptReference Path="~/Script/Apq.js" />
			<asp:ScriptReference Path="~/My97DatePicker/WdatePicker.js" />
		</Scripts>
	</asp:ScriptManager>
	<div>
		<div>
			会员编号:<asp:TextBox ID="txtUserID" runat="server" Enabled="false">0</asp:TextBox>
			会员名:<asp:TextBox ID="txtName" runat="server"></asp:TextBox>
			性别:<asp:DropDownList ID="ddlSex" runat="server">
				<asp:ListItem Value="0" Text="未知"></asp:ListItem>
				<asp:ListItem Value="1" Text="男"></asp:ListItem>
				<asp:ListItem Value="2" Text="女"></asp:ListItem>
			</asp:DropDownList>
		</div>
		<div>
			身份证:<asp:TextBox ID="txtIDCard" runat="server"></asp:TextBox>
		</div>
		<div>
			支付宝帐号:<asp:TextBox ID="txtAlipay" runat="server"></asp:TextBox>
			生日:<asp:TextBox ID="txtBirthday" runat="server" CssClass="Wdate" onclick="WdatePicker({startDate:'1982-04-12',maxDate:'%y-%M-%d'});ApqJS.document.iframeAutoFit();"></asp:TextBox>
		</div>
		<div>
			介绍人:<asp:TextBox ID="txtIntroUserID" runat="server" Enabled="false" Width="400">0</asp:TextBox>
		</div>
		<div>
			帐号过期:<asp:TextBox ID="txtExpire" runat="server" CssClass="Wdate" onclick="WdatePicker()"
				Enabled="false"></asp:TextBox>
			注册时间:<asp:TextBox ID="txtRegTime" runat="server" CssClass="Wdate" onclick="WdatePicker()"
				Enabled="false"></asp:TextBox>
		</div>
		<div align="center">
			<input type="button" id="btnConfirm" onclick="btnConfirm_Click()" value="修改" />
		</div>
	</div>

	<script type="text/javascript">
		if (window.attachEvent) {
			window.attachEvent("onload", ApqJS.document.iframeAutoFit);
		}
		else if (window.addEventListener) {
			window.addEventListener('load', ApqJS.document.iframeAutoFit, false);
		}

		function mdUserInfo_Load() {
		}
		window.attachEvent("onload", mdUserInfo_Load);

		function btnConfirm_Click() {
			// 获取页面值
			var UserID = $get("txtUserID").value;
			var Name = $get("txtName").value;
			var Sex = $get("ddlSex").value;
			var Birthday = $get("txtBirthday").value;
			var IDCard = $get("txtIDCard").value;
			var Alipay = $get("txtAlipay").value;

			// 修改
			dtxc.WS.User.WS2.UserEditSelf(UserID, Name, Sex, "", Birthday, IDCard, Alipay
					, btnConfirm_Click_Success);
		}
		function btnConfirm_Click_Success(stReturn) {
			ApqJS.using("ApqJS.Simulator");
			var MsgBox = new ApqJS.Simulator.Dialog(0, "提示", stReturn.ExMsg, 300, 0, 150, btnConfirm_Click_Success_Confirm);
			MsgBox.Show();

			function btnConfirm_Click_Success_Confirm(evt) {
				if (stReturn.NReturn == 1) {
					location.reload(true);
				}
			}
		}
	</script>

	</form>
</body>
</html>
