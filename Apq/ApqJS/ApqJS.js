///<reference path="vswd-ext_2.2.js" />
///<reference path="ExtJS.js" />
/* ExtJS引用顺序
/ext-path/adapter/ext/ext-base.js
/ext-path/ext-all.js
*/
/* ApqJS
*
* 2009-11-26 黄宗银
* */
ApqJS = {
	Version: "0.1",

	/// window.setTimeout扩展
	/// <o>上下文对象</o>
	setTimeout: function(t, fn, args, o) {
		var f = function() {
			fn.apply(o, args);
		};
		return setTimeout(f, t);
	}
};

/// ApqJS.Object ----------------------------------------------------------------------------------
ApqJS.Object = {
	GetExpandoProperties: function(o) {
		var ary = [];
		for (var p in o) {
			if (o.hasOwnProperty(p)) {
				ary.push(p);
			}
		}
		return ary;
	}
}

/// ApqJS.Array -----------------------------------------------------------------------------------
ApqJS.Array = {
	/// 不重复的添加
	/// <return>如果 o 已位于数组中,返回 o 在数组中的索引位置;否则返回数组的新长度</return>
	AddUnique: function(ary, o) {
		var i = ary.indexOf(o);
		if (i == -1) {
			ary.push(o);
			i = ary.length;
		}
		return i;
	},

	/// 对 数组/arguments 应用指定方法
	/// <o>数组/arguments</o>
	/// <fn>方法(名)</fn>
	/// <args> fn 的参数列表</args>
	Apply: function(o, fn, args) {
		if (typeof fn == "string") {
			fn = o[fn] || Array.prototype[fn];
		}
		if (typeof fn == "function") {
			return ApqJS.Function.Apply(fn, o, args);
		}
		throw new Error(-1, "不存在指定函数");
	},

	Contains: function(ary, item) {
		return ary.indexof(item) != -1;
	},
	/// 移除指定位置的元素,并返回移除的元素
	RemoveAt: function(ary, index) {
		return Array.prototype.splice.apply(ary, [index, 1])[0];
	},
	Repeat: function(value, count) {
		var ary = [];
		for (var i = 0; i < count; i++) {
			ary.push(value);
		}
		return ary;
	}
};

/// ApqJS.Function --------------------------------------------------------------------------------
ApqJS.Function = {
	Apply: function(fn, o, args) {
		if (args && args.length) {
			return fn.apply(o, args);
		}
		return fn.apply(o);
	},
	ReturnFalse: function(e) {
		return false;
	}
};

/// ApqJS.String ----------------------------------------------------------------------------------
ApqJS.String = {
	Repeat: function(str, num) {
		var ary = [];
		for (i = 0; i < num; i++) {
			ary.push(str);
		}
		return ary.join("");
	},
	/// 获取随机字符
	/// <str>字符集</str>
	RandomChar: function(str) {
		return str.charAt(parseInt(str.length * Math.random()));
	},

	/// 获取随机字符串
	/// <Length>长度</Length>
	/// <str>字符集</str>
	RandomString: function(Length, str) {
		var ary = [];
		for (var i = 0; i < Length; i++) {
			ary.push(ApqJS.String.RandomChar(str));
		}
		return ary.join("");
	},
	IsEmail: function(str) {
		var reg = /^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
		return reg.test(str);
	},
	/// <fMin>最少小数位数</fMin>
	/// <fMax>最多小数位数</fMax>
	/// <AllowInt>整数是否通过检验</AllowInt>
	IsNumber: function(str, fMin, fMax, AllowInt) {
		var strMin = fMin > 0 ? fMin.toString() : "0";
		var strMax = fMax > 0 ? fMax.toString() : "";
		var strAllowInt = AllowInt ? "?" : "";
		var reg = new RegExp("^\\-?([1-9]\\d*|0)(\\.\\d{" + strMin + "," + strMax + "})" + strAllowInt + "$");
		return reg.test(str);
	}
};

/// ApqJS.using -----------------------------------------------------------------------------------
ApqJS.using = function(url, win) {
	win = win || window;
	Ext.Ajax.request({
		url: url,
		async: false,
		success: function(p, opts) {
			win.eval(p.responseText);
		},
		failure: function(p, opts) {
			console.log('错误!状态码:' + p.status);
		}
	});
};

/// ApqJS.Common ----------------------------------------------------------------------------------
ApqJS.Common = {
	HtmlEncode: function(s) {
		if (!s) return s;
		return s.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
	},

	HtmlDecode: function(s) {
		if (!s) return s;
		return s.replace(/&gt;/g, ">").replace(/&lt;/g, "<").replace(/&amp;/g, "&");
	}
};

/// document --------------------------------------------------------------------------------------
ApqJS.document = {
	__ref: function(ns, dir, ext) {
		dir = dir || "";
		ext = ext || "js";
		document.write('<script type="text/javascript" src="');
		document.write(dir);
		document.write(ns);
		document.write('.' + ext + '"></\script>');
	},

	FindIFrameElement: function(win) {
		win = win || window;
		try {
			if (win != win.parent) {
				for (var i = 0; i < win.parent.frames.length; i++) {
					if (win.parent.frames[i] == win) {
						return win.parent.frames[i].frameElement;
					}
				}
			}
		}
		catch (ex) { }
	},

	iframeAutoFit: function(evt, win) {
		win = win || window;
		try {
			var ife = ApqJS.document.FindIFrameElement(win);
			if (ife) {
				var h1 = 0, h2 = 0, d = win.document, dd = d.documentElement;
				if (dd && dd.scrollHeight) h1 = dd.scrollHeight;
				if (d.body) h2 = d.body.scrollHeight;
				var h = Math.max(h1, h2);
				if (win.document.all) { h += 4; }
				if (win.opera) { h += 1; }
				ife.style.height = h + "px";
			}
			if (win != win.parent) {
				ApqJS.document.iframeAutoFit(evt, win.parent);
			}
		}
		catch (ex) { }
	},

	//获取元素绝对位置Top
	GetAbsTop: function getAbsTop(he) {
		var offset = he.offsetTop;
		if (he.offsetParent) offset += ApqJS.document.GetAbsTop(he.offsetParent);
		return offset;
	},

	//获取元素绝对位置Left
	GetAbsLeft: function getAbsTop(he) {
		var offset = he.offsetLeft;
		if (he.offsetParent) offset += ApqJS.document.GetAbsLeft(he.offsetParent);
		return offset;
	}
};

if (window.attachEvent) {
	window.attachEvent("onload", ApqJS.document.iframeAutoFit);
} else if (window.addEventListener) {
	window.addEventListener("load", ApqJS.document.iframeAutoFit, false);
}

/// location --------------------------------------------------------------------------------------
ApqJS.location = {
	getQueryString: function(str) {
		var QueryString = {};
		var index = str.indexOf('?');
		if (index != -1) {
			var s = str.substr(index + 1);
			var ary = s.split("&");
			for (var i = 0; i < ary.length; i++) {
				var index = ary[i].indexOf("=");
				if (index != -1) {
					QueryString[ary[i].substr(0, index)] = ary[i].substr(index + 1);
				}
			}
		}
		return QueryString;
	},

	BuildSearch: function(QueryString) {
		var ary = [];
		var keys = ApqJS.Object.GetExpandoProperties(QueryString);
		for (var i = 0; i < keys.length; i++) {
			ary.push(keys[i] + "=" + QueryString[keys[i]]);
		}
		return ary.join("&");
	},

	removeSearch: function(str) {
		var index = str.indexOf('?');
		if (index != -1) {
			str = str.substr(0, index);
		}
		return str;
	}
};

/// 计算当前 location 的 QueryString
ApqJS.location.QueryString = ApqJS.location.getQueryString(location.search);

/// CSS --------------------------------------------------------------------------------------------
ApqJS.CSS = {
	cssFileID: 1,
	/// 按主题获取CSS文件
	/// <cssPath>主题后的文件全路径</cssPath>
	/// <App_Theme>主题目录</App_Theme>
	/// <Theme>主题名:不传该参数则从cookie中读取"Apq_Theme",仍没设置则读取网站设置</Theme>
	swapThemeStyleSheet: function(cssFile, App_Theme, Theme, id) {
		id = id || ("ApqJS_CSS" + ApqJS.CSS.cssFileID++);
		Theme = Theme || Ext.state.Cookies.get("Apq_Theme") || Apq_TopWindow.Apq_SiteConfig.Theme || Apq_TopWindow.Apq_GlobalConfig.Theme;

		Ext.util.CSS.swapStyleSheet(id, App_Theme + '/' + Theme + '/' + cssFile);
	}
};

// WebService错误提示框
function Apq_WS_Faild(e, o, wsMethodName) {
	Ext.Msg.alert("调用WS时返回错误", ApqJS.Common.HtmlEncode(e.get_message()));
}
