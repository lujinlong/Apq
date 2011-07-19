<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="top.aspx.cs" Inherits="pdbp.top" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>管理中心</title>
	<meta http-equiv="refresh" content="60">
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
		ApqJS.CSS.swapThemeStyleSheet("css/skin.css", App_Themes);
		ApqJS.CSS.swapThemeStyleSheet("css/main.css", App_Themes);

		Ext.onReady(function() {
			Ext.QuickTips.init();
			Ext.form.Field.prototype.msgTarget = 'side'; ///提示的方式,枚举值为"qtip","title","under","side",id(元素id)

			// 页面内图片
			$get("img1").src = ApqJS.Img.getThemeImgUrl("img/logo.gif", App_Themes);
			$get("img2").src = ApqJS.Img.getThemeImgUrl("img/out.gif", App_Themes);
		});

		function Logout() {
			if (confirm("您确定要退出吗？"))
				top.location = "Logout.aspx";
		}
	</script>

</head>
<body leftmargin="0" topmargin="0">
	<form id="form1" runat="server">
	<asp:ScriptManager ID="scTop" runat="server">
	</asp:ScriptManager>
	<div>
		<table width="100%" height="64" border="0" cellpadding="0" cellspacing="0" class="admin_topbg">
			<tr>
				<td width="61%" height="64">
					<img id="img1" />
				</td>
				<td width="39%" valign="top">
					<table width="100%" border="0" cellspacing="0" cellpadding="0">
						<tr>
							<td width="74%" height="38">
								管理员：<b><%=Session["NickName"]%></b> 您好,感谢登陆使用！
							</td>
							<td width="22%">
								<a href="javascript:void(0)" target="_self" onclick="Logout()">
									<img id="img2" alt="安全退出" /></a>
							</td>
							<td width="4%">
								&nbsp;
							</td>
						</tr>
						<tr>
							<td height="19" colspan="3">
								&nbsp;
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</div>
	</form>
</body>
</html>
