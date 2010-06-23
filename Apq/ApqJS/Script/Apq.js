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
