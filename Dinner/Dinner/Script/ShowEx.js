///<reference path="vswd-ext_2.2.js" />
function Apq_WS_Faild(e, o, wsMethodName) {
	var strMsg = "";
	if (e.get_message) {
		strMsg = e.get_message();
	}
	else if (e.responseJSON) {
		strMsg = e.responseJSON.Message;
	}

	strMsg = ApqJS.Common.HtmlEncode(strMsg);
	Ext.Msg.alert("调用WS时返回错误", strMsg);
}
