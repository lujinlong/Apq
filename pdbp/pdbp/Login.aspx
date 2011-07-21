<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="pdbp.Login" %>

<!-- 阻止声明 -->
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>权限管理子系统 - 登录</title>
	<link href="/Ext-3.2.1/resources/css/ext-all.css" rel="stylesheet" type="text/css" />

	<script src="/Ext-3.2.1/vswd-ext_2.2.js" type="text/javascript"></script>

	<script src="/Ext-3.2.1/adapter/ext/ext-base.js" type="text/javascript"></script>

	<script src="/Ext-3.2.1/ext-all.js" type="text/javascript"></script>

	<script src="/Ext-3.2.1/ext-basex.js" type="text/javascript"></script>

	<script src="/Ext-3.2.1/src/locale/ext-lang-zh_CN.js" type="text/javascript"></script>

	<script src="/ApqJS/ExtJS.js" type="text/javascript"></script>

	<script src="Script/Apq.js" type="text/javascript"></script>

	<script type="text/javascript">
		var App_Themes = 'App_Themes';
		ApqJS.CSS.swapThemeStyleSheet("css/main.css", App_Themes);
		ApqJS.CSS.swapThemeStyleSheet("css/div_table.css", App_Themes);
		ApqJS.CSS.swapThemeStyleSheet("css/Login.css", App_Themes);
		ApqJS.CSS.swapThemeStyleSheet("css/skin.css", App_Themes);

		Ext.onReady(function() {
			Ext.QuickTips.init();
			Ext.form.Field.prototype.msgTarget = 'side'; ///提示的方式,枚举值为"qtip","title","under","side",id(元素id)

			// 页面内图片
			$get("imgLogo").src = ApqJS.Img.getThemeImgUrl("img/logo.png", App_Themes);
			$get("imgLock").src = ApqJS.Img.getThemeImgUrl("img/lock.gif", App_Themes);
			$get("img1").src = ApqJS.Img.getThemeImgUrl("img/icon-demo.gif", App_Themes);
			$get("img2").src = ApqJS.Img.getThemeImgUrl("img/icon-login-seaver.gif", App_Themes);
			$get("img3").src = ApqJS.Img.getThemeImgUrl("img/login-wel.gif", App_Themes);
		});

		function btnLogin_Click() {
			var strLoginName = Ext.getDom("txtLoginName").value;
			var strLoginPwd = Ext.getDom("txtLoginPwd").value;

			if (!strLoginName) {
				Ext.Msg.alert("输入检测", "必须输入用户名");
				Ext.getDom("txtLoginName").focus();
				return false;
			} //╳
			if (!strLoginPwd) {
				Ext.Msg.alert("输入检测", "必须输入密码");
				Ext.getDom("txtLoginPwd").focus();
				return false;
			}

			pdbp.wsLogin.Login_LoginName(strLoginName, strLoginPwd, btnLogin_Click_Success, Apq_Server_Faild);
			return false;
		}
		function btnLogin_Click_Success(stReturn) {
			if (stReturn.NReturn == 1) {
				top.location = "main.htm";
				return;
			}
			Ext.Msg.alert("登录失败", stReturn.ExMsg);
		}
	</script>

</head>
<body>
	<form id="form1" runat="server" submitdisabledcontrols="true">
	<asp:ScriptManager ID="scLogin" runat="server" EnablePageMethods="true">
		<Services>
			<asp:ServiceReference Path="~/wsLogin.asmx" />
		</Services>
	</asp:ScriptManager>
	<table width="100%" height="166" border="0" cellpadding="0" cellspacing="0">
		<tr>
			<td height="42" valign="top">
				<table width="100%" height="42" border="0" cellpadding="0" cellspacing="0" class="login_top_bg">
					<tr>
						<td width="1%" height="21">
						</td>
						<td height="42">
						</td>
						<td width="17%">
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td valign="top">
				<table width="100%" height="532" border="0" cellpadding="0" cellspacing="0" class="login_bg">
					<tr>
						<td width="49%" align="right">
							<table width="91%" height="532" border="0" cellpadding="0" cellspacing="0" class="login_bg2">
								<tr>
									<td height="138" valign="top">
										<table width="89%" height="427" border="0" cellpadding="0" cellspacing="0">
											<tr>
												<td height="149">
												</td>
											</tr>
											<tr>
												<td height="80" align="right" valign="top">
													<img id="imgLogo" width="279" height="68">
												</td>
											</tr>
											<tr>
												<td height="198" align="right" valign="top">
													<table width="100%" border="0" cellpadding="0" cellspacing="0">
														<tr>
															<td width="35%">
															</td>
															<td height="25" colspan="2" class="left_txt">
																<p>
																	权限管理子系统 - 登录</p>
															</td>
														</tr>
														<tr style="display: none">
															<td width="35%">
															</td>
															<td height="25" colspan="2" class="left_txt">
																<p>
																	1- 地区商家信息网门户站建立的首选方案...</p>
															</td>
														</tr>
														<tr style="display: none">
															<td>
															</td>
															<td height="25" colspan="2" class="left_txt">
																<p>
																	2- 一站通式的整合方式，方便用户使用...</p>
															</td>
														</tr>
														<tr style="display: none">
															<td>
															</td>
															<td height="25" colspan="2" class="left_txt">
																<p>
																	3- 强大的后台系统，管理内容易如反掌...</p>
															</td>
														</tr>
														<tr style="display: none">
															<td>
															</td>
															<td width="30%" height="40">
																<img id="img1" width="16" height="16"><a href="http://www.865171.cn" target="_blank"
																	class="left_txt3"> 使用说明</a>
															</td>
															<td width="35%">
																<img id="img2" width="16" height="16"><a href="http://www.865171.cn" class="left_txt3">
																	在线客服</a>
															</td>
														</tr>
													</table>
												</td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</td>
						<td width="1%">
						</td>
						<td width="50%" valign="bottom">
							<table width="100%" height="59" border="0" align="center" cellpadding="0" cellspacing="0">
								<tr>
									<td width="4%">
									</td>
									<td width="96%" height="38">
										<span class="login_txt_bt">登录权限管理子系统</span>
									</td>
								</tr>
								<tr>
									<td>
									</td>
									<td height="21">
										<table cellspacing="0" cellpadding="0" width="100%" border="0" id="table211" height="328">
											<tr>
												<td height="164" colspan="2" align="middle">
													<form name="myform" action="index.html" method="post">
													<table cellspacing="0" cellpadding="0" width="100%" border="0" height="143" id="table212">
														<tr>
															<td width="13%" height="38" class="top_hui_text">
																<span class="login_txt">用户名：</span>
															</td>
															<td height="38" colspan="2" class="top_hui_text">
																<input type="text" id="txtLoginName" name="txtLoginName" class="editbox4" size="20" />
														</tr>
														<tr>
															<td width="13%" height="35" class="top_hui_text">
																<span class="login_txt">密 码：</span>
															</td>
															<td height="35" colspan="2" class="top_hui_text">
																<input type="password" id="txtLoginPwd" name="txtLoginPwd" class="editbox4" size="20" />
																<img id="imgLock" width="19" height="18" />
															</td>
														</tr>
														<tr style="display: none">
															<td width="13%" height="35">
																<span class="login_txt">验证码：</span>
															</td>
															<td height="35" colspan="2" class="top_hui_text">
																<input type="text" id="txtVerifyCode" name="txtVerifyCode" class="wenbenkuang" maxlength="4"
																	size="10" />
															</td>
														</tr>
														<tr>
															<td height="35">
															</td>
															<td width="20%" height="35">
																<input type="submit" id="btnLogin" name="btnLogin" class="button" value="登 陆" onclick="btnLogin_Click();return false;">
															</td>
															<td width="67%" class="top_hui_text">
																<input type="button" id="btnReg" name="btnReg" class="button" value="注 册" onclick="top.location = 'Reg.aspx';">
															</td>
														</tr>
													</table>
													<br>
													</form>
												</td>
											</tr>
											<tr>
												<td width="433" height="164" align="right" valign="bottom">
													<img id="img3" width="242" height="138">
												</td>
												<td width="57" align="right" valign="bottom">
												</td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td height="20">
				<table width="100%" border="0" cellspacing="0" cellpadding="0" class="login-buttom-bg">
					<tr>
						<td align="center">
							<span class="login-buttom-txt">Copyright &copy; 2009-2010 www.865171.cn</span>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	</form>
</body>
</html>
