function Login(UserName, LoginPwd) {
	// 先清空 Cookie
	ApqJS.document.delCookie("UserName");
	ApqJS.document.delCookie("SqlLoginPwd");

	dtxc.WS.WS2.Login_UserName(UserName, LoginPwd, Login_Sucess);
}

function Login_Sucess(stReturn) {
	if (stReturn.NReturn == 2) {
		alert(stReturn.ExMsg);
		top.location = "ifLoginInfo.aspx";
		return;
	}

	if (stReturn.NReturn != 1) {
		alert(stReturn.ExMsg);
		return;
	}

	// Cookie 操作
	var dtExpires = new Date();
	dtExpires.setFullYear(dtExpires.getFullYear() + 1);
	ApqJS.document.setCookie("UserName", stReturn.FNReturn.rows[0]["UserName"], dtExpires);
	ApqJS.document.setCookie("SqlLoginPwd", stReturn.POuts[1], dtExpires);

	top.location = "ApqJS.aspx";
}
