///<reference path="vswd-ext_2.2.js" />
/* ExtJS扩展
* 2009-12-03 黄宗银
* */
/// Ext.data.Types ---------------------------------------------------------------------------------
// 支持WebService输出的Date类型JSON格式
Ext.data.Types.DATE.convertExt = Ext.data.Types.DATE.convert; //保存原版
Ext.data.Types.DATE.convert = function(v) {
	var df = this.dateFormat;
	if (!v) {
		return null;
	}
	if (Ext.isDate(v)) {
		return v;
	}
	if (df) {
		if (df == 'timestamp') {
			return new Date(v * 1000);
		}
		if (df == 'time') {
			return new Date(parseInt(v, 10));
		}
		return Date.parseDate(v, df);
	}
	var parsed = Date.parse(v);
	// 改变的部分 ---
	if (parsed) return new Date(parsed);
	
	if (v.substr(0, 6) == '/Date(' && v.substring(v.length - 1) == '/') {
		parsed = eval(v.substr(1, v.length - 2));
	}
	return parsed ? parsed : null;
	// ===============
};

/// Ext.Element ------------------------------------------------------------------------------------
// 改变:忽略execScript的异常
Ext.Element.addMethods({
	update: function(html, loadScripts, callback) {
		if (!this.dom) {
			return this;
		}
		html = html || "";

		if (loadScripts !== true) {
			this.dom.innerHTML = html;
			if (typeof callback == 'function') {
				callback();
			}
			return this;
		}

		var id = Ext.id(),
            dom = this.dom;

		html += '<span id="' + id + '"></span>';

		Ext.lib.Event.onAvailable(id, function() {
			var DOC = document,
                hd = DOC.getElementsByTagName("head")[0],
            	re = /(?:<script([^>]*)?>)((\n|\r|.)*?)(?:<\/script>)/ig,
            	srcRe = /\ssrc=([\'\"])(.*?)\1/i,
            	typeRe = /\stype=([\'\"])(.*?)\1/i,
            	match,
            	attrs,
            	srcMatch,
            	typeMatch,
            	el,
            	s;

			while ((match = re.exec(html))) {
				attrs = match[1];
				srcMatch = attrs ? attrs.match(srcRe) : false;
				if (srcMatch && srcMatch[2]) {
					s = DOC.createElement("script");
					s.src = srcMatch[2];
					typeMatch = attrs.match(typeRe);
					if (typeMatch && typeMatch[2]) {
						s.type = typeMatch[2];
					}
					hd.appendChild(s);
				} else if (match[2] && match[2].length > 0) {
					if (window.execScript) {
						try { window.execScript(match[2]); } catch (err) { } //增加try
					} else {
						window.eval(match[2]);
					}
				}
			}
			el = DOC.getElementById(id);
			if (el) { Ext.removeNode(el); }
			if (typeof callback == 'function') {
				callback();
			}
		});
		dom.innerHTML = html.replace(/(?:<script.*?>)((\n|\r|.)*?)(?:<\/script>)/ig, "");
		return this;
	}
})

/// Ext.state.CookieProvider ----------------------------------------------------------------------
// 改进clearCookie
Ext.state.CookieProvider.prototype.clearCookieExt = Ext.state.CookieProvider.prototype.clearCookie;
Ext.override(Ext.state.CookieProvider, {
	clearCookie: function(name) {
		var exp = new Date();
		exp.setTime(exp.getTime() - 1);
		document.cookie = "ys-" + name + "=null; expires=" + exp.toGMTString()
		+ ((this.path == null) ? "" : ("; path=" + this.path))
		+ ((this.domain == null) ? "" : ("; domain=" + this.domain))
		+ ((this.secure == true) ? "; secure" : "");
	}
});

// 全局Cookies
Ext.state.Cookies = new Ext.state.CookieProvider();
{
	var _dtExpires = new Date();
	_dtExpires.setFullYear(_dtExpires.getFullYear() + 1);
	Ext.state.Cookies.expires = _dtExpires;

	// 全局状态管理器
	Ext.state.Manager.setProvider(
		new Ext.state.CookieProvider({
			expires: _dtExpires //一年后
		})
	);
}

/// Ext.data.JsonReader ---------------------------------------------------------------------------
// 让JsonReader支持子属性为null
Ext.data.JsonReader.prototype.getJsonAccessorExt = Ext.data.JsonReader.prototype.getJsonAccessor;
Ext.override(Ext.data.JsonReader, {
	getJsonAccessor: function() {
		var re = /[\[\.]/;
		return function(expr) {
			try {
				return (re.test(expr)) ?
                new Function("obj", "try { return  obj." + expr + ";}catch(e){}return undefined;") :
                function(obj) {
                	return obj[expr];
                };
			} catch (e) { }
			return Ext.emptyFn;
		};
	} ()
});

/// Ext.data.Connection ---------------------------------------------------------------------------
// 扩展Ext.data.Connection以支持WebService,选项参数中增加ws=true表示调用WebService
Ext.override(Ext.data.Connection, {
	request: function(o) {
		var me = this;
		if (me.fireEvent("beforerequest", me, o)) {
			if (o.el) {
				if (!Ext.isEmpty(o.indicatorText)) {
					me.indicatorText = '<div class="loading-indicator">' + o.indicatorText + "</div>";
				}
				if (me.indicatorText) {
					Ext.getDom(o.el).innerHTML = me.indicatorText;
				}
				o.success = (Ext.isFunction(o.success) ? o.success : function() { }).createInterceptor(function(response) {
					Ext.getDom(o.el).innerHTML = response.responseText;
				});
			}

			var p = o.params,
                url = o.url || me.url, method,
                cb = { success: me.handleResponse,
                	failure: me.handleFailure,
                	scope: me,
                	argument: { options: o },
                	timeout: o.timeout || me.timeout
                },
                form,
                serForm;

			if (Ext.isFunction(p)) {
				p = p.call(o.scope || WINDOW, o);
			}

			/// 调用 ws 时使用
			if (o.ws) {
				p = Ext.encode(p);
			}
			else {
				p = Ext.urlEncode(me.extraParams, Ext.isObject(p) ? Ext.urlEncode(p) : p);
			}

			if (Ext.isFunction(url)) {
				url = url.call(o.scope || WINDOW, o);
			}

			if ((form = Ext.getDom(o.form))) {
				url = url || form.action;
				if (o.isUpload || /multipart\/form-data/i.test(form.getAttribute("enctype"))) {
					return me.doFormUpload.call(me, o, p, url);
				}
				serForm = Ext.lib.Ajax.serializeForm(form);
				p = p ? (p + '&' + serForm) : serForm;
			}

			method = o.method || me.method || ((p || o.xmlData || o.jsonData) ? "POST" : "GET");

			if (method === "GET" && (me.disableCaching && o.disableCaching !== false) || o.disableCaching === true) {
				var dcp = o.disableCachingParam || me.disableCachingParam;
				url = Ext.urlAppend(url, dcp + '=' + (new Date().getTime()));
			}

			o.headers = Ext.apply(o.headers || {}, me.defaultHeaders || {});
			// 调用 ws 时,使用固定Content-Type
			if (o.ws) {
				o.headers = Ext.apply(o.headers || {}, { "Content-Type": "application/json; charset=utf-8" });
			}

			if (o.autoAbort === true || me.autoAbort) {
				me.abort();
			}

			if ((method == "GET" || o.xmlData || o.jsonData) && p) {
				url = Ext.urlAppend(url, p);
				p = '';
			}
			return (me.transId = Ext.lib.Ajax.request(method, url, cb, p, o));
		} else {
			return o.callback ? o.callback.apply(o.scope, [o, undefined, undefined]) : null;
		}
	}
});
