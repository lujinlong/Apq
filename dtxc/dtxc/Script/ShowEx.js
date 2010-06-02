///<reference path="vswd-ext_2.2.js" />
function Apq_WS_Faild(e, o, wsMethodName) {
	Ext.Msg.alert("调用WS时返回错误", ApqJS.Common.HtmlEncode(e.get_message()));
}
