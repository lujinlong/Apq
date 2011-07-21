<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Left.aspx.cs" Inherits="pdbp.Left" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>管理中心-下左</title>
	<link href="/Ext-3.2.1/resources/css/ext-all.css" rel="stylesheet" type="text/css" />

	<script src="/Ext-3.2.1/vswd-ext_2.2.js" type="text/javascript"></script>

	<script src="/Ext-3.2.1/adapter/ext/ext-base.js" type="text/javascript"></script>

	<script src="/Ext-3.2.1/ext-all.js" type="text/javascript"></script>

	<script src="/Ext-3.2.1/ext-basex.js" type="text/javascript"></script>

	<script src="/Ext-3.2.1/src/locale/ext-lang-zh_CN.js" type="text/javascript"></script>

	<script src="/ApqJS/ExtJS.js" type="text/javascript"></script>

	<script src="Script/Apq.js" type="text/javascript"></script>

	<script src="Script/prototype.lite.js" type="text/javascript"></script>

	<script src="Script/moo.fx.js" type="text/javascript"></script>

	<script src="Script/moo.fx.pack.js" type="text/javascript"></script>

	<script type="text/javascript">
		var App_Themes = 'App_Themes';
		ApqJS.CSS.swapThemeStyleSheet("css/main.css", App_Themes);
		ApqJS.CSS.swapThemeStyleSheet("css/left.css", App_Themes);
		//ApqJS.CSS.swapThemeStyleSheet("css/skin.css", App_Themes);

		Ext.onReady(function() {
			Ext.QuickTips.init();
			Ext.form.Field.prototype.msgTarget = 'side'; ///提示的方式,枚举值为"qtip","title","under","side",id(元素id)

			// 页面内图片
			$get("img1").src = ApqJS.Img.getThemeImgUrl("img/menu_topline.gif", App_Themes);
			$get("img2").src = ApqJS.Img.getThemeImgUrl("img/menu_topline.gif", App_Themes);
			$get("img3").src = ApqJS.Img.getThemeImgUrl("img/menu_topline.gif", App_Themes);
			$get("img4").src = ApqJS.Img.getThemeImgUrl("img/menu_topline.gif", App_Themes);
			$get("img5").src = ApqJS.Img.getThemeImgUrl("img/menu_topline.gif", App_Themes);
		});

		function btnLogin_Click() {
			var strLoginName = Ext.getDom("txtLoginName").value;
			var strLoginPwd = Ext.getDom("txtLoginPwd").value;

			if (!strLoginName) {
				Ext.Msg.alert("输入检测", "必须输入用户名");
				Ext.getDom("txtLoginName").focus();
				return;
			}
			if (!strLoginPwd) {
				Ext.Msg.alert("输入检测", "必须输入密码");
				Ext.getDom("txtLoginPwd").focus();
				return;
			}

			PageMethods.Login_LoginName(strLoginName, strLoginPwd, btnLogin_Click_Success);
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
	<form id="form1" runat="server">
	<asp:ScriptManager ID="scLeft" runat="server" EnablePageMethods="true">
	</asp:ScriptManager>
	<table width="100%" height="280" border="0" cellpadding="0" cellspacing="0" bgcolor="#EEF2FB">
		<tr>
			<td width="182" valign="top">
				<div id="container">
					<h1 class="type">
						<a href="javascript:void(0)">网站常规管理</a></h1>
					<div class="content">
						<table width="100%" border="0" cellspacing="0" cellpadding="0">
							<tr>
								<td>
									<img id="img1" width="182" height="5" />
								</td>
							</tr>
						</table>
						<ul class="MM">
							<li><a href="AddinList.aspx" target="pdbp_Main">插件管理</a></li>
							<li><a href="http://www.865171.cn" target="pdbp_Main">邮件设置</a></li>
							<li><a href="http://www.865171.cn" target="pdbp_Main">广告设置</a></li>
							<li><a href="http://www.865171.cn" target="pdbp_Main">114增加</a></li>
							<li><a href="http://www.865171.cn" target="pdbp_Main">114管理 </a></li>
							<li><a href="http://www.865171.cn" target="pdbp_Main">联系方式</a></li>
							<li><a href="http://www.865171.cn" target="pdbp_Main">汇款方式</a></li>
							<li><a href="http://www.865171.cn" target="pdbp_Main">增加链接</a></li>
							<li><a href="http://www.865171.cn" target="pdbp_Main">管理链接</a></li>
						</ul>
					</div>
					<h1 class="type">
						<a href="javascript:void(0)">栏目分类管理</a></h1>
					<div class="content">
						<table width="100%" border="0" cellspacing="0" cellpadding="0">
							<tr>
								<td>
									<img id="img2" width="182" height="5" />
								</td>
							</tr>
						</table>
						<ul class="MM">
							<li><a href="http://www.865171.cn" target="pdbp_Main">信息分类</a></li>
							<li><a href="http://www.865171.cn" target="pdbp_Main">信息类型</a></li>
							<li><a href="http://www.865171.cn" target="pdbp_Main">资讯分类</a></li>
							<li><a href="http://www.865171.cn" target="pdbp_Main">地区设置</a></li>
							<li><a target="pdbp_Main" href="http://www.865171.cn">市场联盟</a></li>
							<li><a href="http://www.865171.cn" target="pdbp_Main">商家类型</a></li>
							<li><a href="http://www.865171.cn" target="pdbp_Main">商家星级</a></li>
							<li><a href="http://www.865171.cn" target="pdbp_Main">商品分类</a></li>
							<li><a href="http://www.865171.cn" target="pdbp_Main">商品类型</a></li>
						</ul>
					</div>
					<h1 class="type">
						<a href="javascript:void(0)">栏目内容管理</a></h1>
					<div class="content">
						<table width="100%" border="0" cellspacing="0" cellpadding="0">
							<tr>
								<td>
									<img id="img3" width="182" height="5" />
								</td>
							</tr>
						</table>
						<ul class="MM">
							<li><a href="http://www.865171.cn" target="pdbp_Main">信息管理</a></li>
							<li><a href="http://www.865171.cn" target="pdbp_Main">张贴管理</a></li>
							<li><a href="http://www.865171.cn" target="pdbp_Main">增加商家</a></li>
							<li><a href="http://www.865171.cn" target="pdbp_Main">管理商家</a></li>
							<li><a href="http://www.865171.cn" target="pdbp_Main">发布资讯</a></li>
							<li><a href="http://www.865171.cn" target="pdbp_Main">资讯管理</a></li>
							<li><a href="http://www.865171.cn" target="pdbp_Main">市场联盟</a></li>
							<li><a href="http://www.865171.cn" target="pdbp_Main">名片管理</a></li>
							<li><a href="http://www.865171.cn" target="pdbp_Main">商城管理</a></li>
							<li><a href="http://www.865171.cn" target="pdbp_Main">商品管理</a></li>
							<li><a href="http://www.865171.cn" target="pdbp_Main">商城留言</a></li>
							<li><a href="http://www.865171.cn" target="pdbp_Main">商城公告</a></li>
						</ul>
					</div>
					<h1 class="type">
						<a href="javascript:void(0)">注册用户管理</a></h1>
					<div class="content">
						<table width="100%" border="0" cellspacing="0" cellpadding="0">
							<tr>
								<td>
									<img id="img4" width="182" height="5" />
								</td>
							</tr>
						</table>
						<ul class="MM">
							<li><a href="http://www.865171.cn" target="pdbp_Main">会员管理</a></li>
							<li><a href="http://www.865171.cn" target="pdbp_Main">留言管理</a></li>
							<li><a href="http://www.865171.cn" target="pdbp_Main">回复管理</a></li>
							<li><a href="http://www.865171.cn" target="pdbp_Main">订单管理</a></li>
							<li><a href="http://www.865171.cn" target="pdbp_Main">举报管理</a></li>
							<li><a href="http://www.865171.cn" target="pdbp_Main">评论管理</a></li>
						</ul>
					</div>
				</div>
				<h1 class="type">
					<a href="javascript:void(0)">个人信息</a></h1>
				<div class="content">
					<table width="100%" border="0" cellspacing="0" cellpadding="0">
						<tr>
							<td>
								<img id="img5" width="182" height="5" />
							</td>
						</tr>
					</table>
					<ul class="MM">
						<li><a href="http://www.865171.cn" target="pdbp_Main">基本信息</a></li>
						<li><a href="http://www.865171.cn" target="pdbp_Main">修改密码</a></li>
						<li><a href="http://www.865171.cn" target="pdbp_Main">投票历史</a></li>
						<li><a href="http://www.865171.cn" target="pdbp_Main">运行状态</a></li>
						<li><a href="http://www.865171.cn" target="pdbp_Main">申请支付</a></li>
					</ul>
				</div>
			</td>
		</tr>
	</table>

	<script type="text/javascript">
		var contents = document.getElementsByClassName('content');
		var toggles = document.getElementsByClassName('type');

		var myAccordion = new fx.Accordion(
			toggles, contents, { opacity: true, duration: 400 }
		);
		myAccordion.showThisHideOpen(contents[0]);
	</script>

	</form>
</body>
</html>
