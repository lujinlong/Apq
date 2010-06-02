<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="dtxc.Ext1.Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>神枪手</title>
	<%--<meta http-equiv="X-UA-Compatible" content="IE=7" />--%>
	<link href="css/Extmain.css" rel="stylesheet" type="text/css" />
	<!--导入prototype文件 -->

	<script language="javascript" type="text/javascript" src="js/prototype.js"></script>

</head>
<body>
	<form id="form1" runat="server">
	<!--loading加载 -->
	<div id="loadingTab">
		<div class="loading-indicator">
			<img src="images/public/loader.gif" width="32" height="32" style="margin-right: 8px;
				float: left; vertical-align: top;" />
			<a href="main.aspx">神枪手欢迎你</a><br />
			<span id="loading-msg">加载样式表和图片...</span>
		</div>
	</div>
	<div id="north" style="visibility: hidden;">
		<span class="api-title">欢迎你,</span><span id="spanUserName" runat="server" class="api-title"></span></div>
	<div id="south">
		<div class="power" id="power" style="visibility: hidden;">
			Power By: <a href="http://extjs.com/" target="_blank">ExtJs 2.2.1</a>&nbsp;
		</div>
		<div class="bq" id="banquan" style="visibility: hidden;">
			版权所有：<a href="http://www.9iExt.cn/" target="_blank">9iExt.cn</a>
		</div>
	</div>
	<!--加载ext框架样式-->
	<link href="js/ext-2.2/resources/css/ext-all.css" rel="stylesheet" type="text/css">
	<link href="js/ext-2.2/resources/css/portal.css" rel="stylesheet" type="text/css">

	<script type="text/javascript">		$('loading-msg').innerHTML = '正在加载UI组建...';</script>

	<!--加载ext核心文件-->

	<script language="javascript" type="text/javascript" src="js/ext-2.2/adapter/ext/ext-base.js"></script>

	<script language="javascript" type="text/javascript" src="js/ext-2.2/ext-all.js"></script>

	<script language="javascript" type="text/javascript" src="js/ext-2.2/source/locale/ext-lang-zh_CN.js"></script>

	<script type="text/javascript">		$('loading-msg').innerHTML = '正在初始化...';</script>

	<!--加载ext自定义组件-->

	<script type="text/javascript" src="js/localXHR.js"></script>

	<script language="javascript" type="text/javascript" src="js/extMain.js"></script>

	<script type="text/javascript">
		$('loading-msg').innerHTML = '初始化完毕！！';
		Ext.get('loadingTab').fadeOut({ remove: true }); //让加载标签消失
		$('north').style.visibility = "visible";
		$('power').style.visibility = "visible";
		$('banquan').style.visibility = "visible"
	</script>

	</form>
</body>
</html>
