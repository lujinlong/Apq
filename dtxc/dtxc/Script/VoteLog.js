if (window.attachEvent) {
	window.attachEvent("onload", ApqJS.document.iframeAutoFit);
}
else if (window.addEventListener) {
	window.addEventListener('load', ApqJS.document.iframeAutoFit, false);
}

var VoteLog_Load = function() {
	if (ApqJS.location.QueryString["TaskName"]) document.getElementById("txtTaskName").value = ApqJS.location.QueryString["TaskName"];
	if (ApqJS.location.QueryString["UserNameBegin"]) document.getElementById("txtUserNameBegin").value = ApqJS.location.QueryString["UserNameBegin"];
};
window.attachEvent("onload", VoteLog_Load);
