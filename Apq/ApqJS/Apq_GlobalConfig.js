/*
最上层窗口(不一定是ApqJS.aspx)保存的全局变量初始值
*/
if (Apq_TopWindow && !Apq_TopWindow.Apq_GlobalConfig) {
	Apq_TopWindow.Apq_GlobalConfig = {
		//模拟弹出框初始值
		Dialog_zIndex: 40000,
		Dialog_bgPic: Apq_InitConfig.ApqJSFolder + "Img/ApqJS.Simulator/bg_Dialog.png",
		Theme: "default"
	};
}

if (Apq_TopWindow && !Apq_TopWindow.Apq_SiteConfig) {
	Apq_TopWindow.Apq_SiteConfig = {};
}
