/* 系统设置
*
* 每个项目均应单独建立此文件,每个页面均应在onload前加载该文件
*
* 2009-04-06 黄宗银
* */

/// ApqJS 框架初始化设置 ----------------------------------------------------------------------------------------------------------------------
/// 含使用框架必须的信息
window.Apq_InitConfig = {
	// ApqJS 脚本库根目录
	ApqJSFolder: "/ApqJS/",
	// 站点脚本库根目录,一般为该文件所在目录
	SiteScript: "Script/",
	// 起始页面地址(ApqJS.aspx使用)
	Home: "testApqJS.aspx.htm"
};

/// 以下一般不用修改 --------------------------------------------------------------------------------------------------------------------------
/// Apq_TopWindow
window.Apq_TopWindow = parent.Apq_TopWindow || window;
window.Apq_TopIFrameWindow = parent.Apq_TopIFrameWindow || window;
window.Apq_JSXH = parent.Apq_JSXH;
if (!window.Apq_JSXH) {
	try {
		try {
			window.Apq_JSXH = new XMLHttpRequest();
		}
		catch (e) {
			window.Apq_JSXH = new ActiveXObject("MsXml2.XMLHttp");
		}
	}
	catch (e) { }
}
if (!window.Apq_JSXH && window.confirm('系统需要启用 ActiveX 运行权限,请检查安全设置.\n\n' +
	'同时还需要安装Microsoft XML 分析器 MSXML 6.0\n' +
	'如果你未安装,请点击"确定"打开下载\n' +
	'注意:安装后可能需要重新打开IE')) {
	window.open("http://download.microsoft.com/download/2/e/0/2e01308a-e17f-4bf9-bf48-161356cf9c81/msxml6.msi", "_blank");
}

// 初始化脚本容器
window.Apq_JSContainer = parent.Apq_JSContainer || {};

// 引入Apq_GlobalConfig.js
document.write('<script type="text/javascript" src = "');
document.write(Apq_InitConfig.ApqJSFolder);
document.write('Apq_GlobalConfig.js">');
document.writeln('<\/script>');
// 引入Apq_SiteConfig.js
document.write('<script type="text/javascript" src = "');
document.write(Apq_InitConfig.SiteScript);
document.write('Apq_SiteConfig.js">');
document.writeln('<\/script>');
// 引入ApqJS.js
document.write('<script type="text/javascript" src = "');
document.write(Apq_InitConfig.ApqJSFolder);
document.write('ApqJS.js">');
document.writeln('<\/script>');
