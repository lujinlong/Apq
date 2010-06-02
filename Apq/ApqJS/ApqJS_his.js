/* ApqJS
*
* 2006-06-27	黄宗银
* */

if (!window.ApqJS) {
	window.ApqJS = {
		"$key": "namespace",
		"$type": "ApqJS"
	};

	/// 主页面配置文件链(获取 当前/所有 站点配置)
	ApqJS.Apq_ConfigChain = {
		GetGlobal: function(Name) { return Apq_TopWindow.Apq_GlobalConfig[Name]; },
		GetSite: function(Name) { return Apq_TopWindow.Apq_SiteConfig[Name]; },
		GetConfig: function(Name) {
			if (Apq_TopWindow.Apq_SiteConfig && Apq_TopWindow.Apq_SiteConfig.hasOwnProperty(Name))
				return Apq_TopWindow.Apq_SiteConfig[Name];
			else
				return Apq_TopWindow.Apq_GlobalConfig[Name];
		},
		SetConfig: function(Name, Value) {
			Apq_TopWindow.Apq_SiteConfig[Name] = Value;
		}
	};

	if (!ApqJS.$Object_p) {
		/// 内置对象原型扩展 ----------------------------------------------------------------------------------------------------------------------
		ApqJS.$Object_p = {};
		ApqJS.$Array_p = {};
		ApqJS.$Function_p = {};
		ApqJS.$String_p = {};

		ApqJS.$Boolean_p = {};
		ApqJS.$Number_p = {};

		ApqJS.$Date_p = {};
		ApqJS.$Error_p = {};
		ApqJS.$Enumerator_p = {};
		ApqJS.$RegExp_p = {};

		/// 模拟内置对象(调用所有成员的接口) ------------------------------------------------------------------------------------------------------
		ApqJS.Object = {};
		ApqJS.Array = {};
		ApqJS.Function = {};
		ApqJS.String = {};

		ApqJS.Boolean = {};
		ApqJS.Number = {};

		ApqJS.Date = {};
		ApqJS.Error = {};
		ApqJS.Enumerator = {};
		ApqJS.RegExp = {};

		/// 备份属性值(引用)到新属性 --------------------------------------------------------------------------------------------------------------
		/// <o>一般为原型</o>
		/// <c=true>是否强制覆盖</c>
		ApqJS.Property_Bak = function(o, po, pn, c) {
			c = c == null ? true : c;
			if (o.hasOwnProperty(po) && (!o.hasOwnProperty(pn) || c)) {
				o[pn] = o[po];
			}
		};

		/// Function ------------------------------------------------------------------------------------------------------------------------------
		ApqJS.$Function_p.Apply = function(o, args) {
			if (args && args.length) {
				return this.apply(o, args);
			}
			return this.apply(o);
		};

		/// 将函数映射为目标对象(d)的成员(成员名由 p 指定),并设置 context 为指定对象 o 
		/// <o=d/>
		ApqJS.$Function_p.bind = function(d, p, o) {
			o = o || d;
			var me = this;
			d[p] = function() {
				return me.apply(o, arguments);
			};
		};

		ApqJS.Function.Empty = function() { };

		/// Object --------------------------------------------------------------------------------------------------------------------------------
		ApqJS.$Object_p.CopyFrom = function(o, c) {
			return ApqJS.Object.Copy(o, this, c);
		};

		ApqJS.$Object_p.CopyTo = function(o, c) {
			return ApqJS.Object.Copy(this, o, c);
		};

		ApqJS.Object.Copy = function(s, d, c) {
			d = d || {};
			for (var p in s) {
				if (typeof (s[p]) != "unknown") {
					ApqJS.Object.Set(d, p, s[p], c, false);
				}
			}
			return d;
		};

		ApqJS.Object.Get = function(o, p) {
			return o == null ? ApqJS.$o(p) : o[p];
		};

		ApqJS.Object.Set = function(o, p, v, c, e) {
			e = e == null ? true : e;

			if (o == null) {
				throw new Error(-1, "参数 [o] 不能为 null");
			}
			c = c == null ? true : c;
			if (!c && typeof o[p] != "undefined") {
				return;
			}

			var oe = { "Original": o[p], "ProposedValue": v };
			var pe = p.indexOf("_") == 0 ? p.substr(1) : p;
			if (e) {
				ApqJS.Object.fireEvent(o, pe + "Changing", oe);
			}

			if (oe.cancel) {
				return;
			}

			o[p] = oe.ProposedValue;
			if (e) {
				ApqJS.Object.fireEvent(o, pe + "Changed", oe);
			}
		};

		ApqJS.Object.GetPrototypeProperties = function(o) {
			var ary = [];
			for (var p in o) {
				if (!o.hasOwnProperty(p)) {
					ary.push(p);
				}
			}
			return ary;
		};

		ApqJS.Object.GetExpandoProperties = function(o) {
			var ary = [];
			for (var p in o) {
				if (o.hasOwnProperty(p)) {
					ary.push(p);
				}
			}
			return ary;
		};

		ApqJS.$Object_p.attachEvent = function(n, fn, oe) {
			if (this[n] && ApqJS.GetTypeName(this[n]) == "ApqJS.Event" && fn) {
				this[n].add(fn, oe);
				return;
			}
			if (this.Events && this.Events[n] && fn) {
				this.Events[n].add(fn, oe);
			}
		};

		ApqJS.$Object_p.detachEvent = function(n, fn, oe) {
			if (this[n] && ApqJS.GetTypeName(this[n]) == "ApqJS.Event") {
				this[n].remove(fn, oe);
				return;
			}
			if (this.Events && this.Events[n]) {
				this.Events[n].remove(fn, oe);
			}
		};

		ApqJS.$Object_p.fireEvent = function(n, e) {
			if (this["_On" + n]) {
				return this["_On" + n](e);
			}
			if (this.Events && this.Events[n]) {
				return this.Events[n].Fire(this, e);
			}
			if (this[n] && ApqJS.GetTypeName(this[n]) == "ApqJS.Event") {
				return this[n].Fire(this, e);
			}
		};

		/// 属性操作器 ----------------------------------------------------------------------------------------------------------------------------
		/// 添加
		/// <rw=3>按位从右到左:
		///		1: r
		///		2: w
		/// </rw>
		/// <v>是否为虚属性</v>
		ApqJS.$Object_p.pAdd = function(p, rw, v) {
			rw = rw || 3;

			if (rw & 1) {
				ApqJS.Function.Create(this, p + "_get",
				function() {
					return ApqJS.Object.Get(this, "_" + p);
				}
				, v);
			}
			if (rw & 2) {
				ApqJS.Function.Create(this, p + "_set",
				function(v, c) {
					ApqJS.Object.Set(this, "_" + p, v);
				}
				, v);
			}
		};

		/// 移除
		ApqJS.$Object_p.pRemove = function(p, rw) {
			if (rw & 1) {
				delete this[p + "_get"];
			}
			if (rw & 2) {
				delete this[p + "_set"];
			}
		};

		/// toJSON --------------------------------------------------------------------------------------------------------------------------------
		ApqJS.toJSON = function(o) {
			var strClassName = ApqJS.GetTypeName(o).toLowerCase();
			if (strClassName == "undefined" || strClassName == "null") {
				return strClassName;
			}
			if (o.toJSON && o.toJSON != (new Object()).toJSON) {
				return o.toJSON();
			}
			if (strClassName == "system.xml.xmldocument" || strClassName == "system.xml.xmlnode") {
				// Xml 相关类
				return o.xml;
			}
			switch (strClassName) {
				case "array":
					var a = [];
					for (var i = 0; i < o.length; i++) {
						a.push(ApqJS.toJSON(o[i]));
					}
					return "[ " + a.join(", ") + " ]";
				case "boolean":
				case "regexp":
					return o.toString();
					break;
				case "number":
					if (isFinite(o)) {
						return o.toString();
					}
					else if (isNaN(o)) {
						return "NaN";
					}
					else {
						return "Number." + (o > 0 ? "POSITIVE_INFINITY" : "NEGATIVE_INFINITY");
					}
				case "string":
					var s = o.replace(/(["\\])/g, '\\$1');
					s = s.replace(/\n/g, "\\n");
					s = s.replace(/\r/g, "\\r");
					return '"' + s + '"';
				case "date":
					return ApqJS.String.Format("(new Date( {0}, {1}, {2}, {3}, {4}, {5}, {6} ))", this.getFullYear(), this.getMonth(), this.getDate(),
					this.getHours(), this.getMinutes(), this.getSeconds(), this.getMilliseconds()
				);
				case "error":
					return ApqJS.String.Format("(new Error( {0}, {1} ))", this.number, this.message.toJSON());
			}
			throw new Error(-1, "未定义该方法");
		};

		/// 运算符 --------------------------------------------------------------------------------------------------------------------------------
		/// Equals
		ApqJS.$Object_p.Equals = function(o) {
			return this == o;
		};

		ApqJS.Object.Equals = function(o1, o2) {
			if (o1 == null) {
				o1 = o2;
				o2 = null;
			}
			if (o1 == null) {
				return true;
			}
			return ApqJS.Function.Apply(ApqJS.$Object_p.Equals, [o1, [o2]]);
		};

		ApqJS.$Date_p.Equals = function(d) {
			if (d instanceof Date) {
				return this.getTime() == d.getTime();
			}
			return false;
		};
		ApqJS.Date.Equals = function(d1, d2) {
			if (d1 == null) {
				d1 = d2;
				d2 = null;
			}
			if (d1 == null) {
				return true;
			}
			return ApqJS.Function.Apply(ApqJS.$Date_p.Equals, [d1, [d2]]);
		};
		ApqJS.$Error_p.Equals = function(e) {
			if (e instanceof Error) {
				return this.number == e.number && this.message == e.message;
			}
			return false;
		};
		ApqJS.Error.Equals = function(e1, e2) {
			if (e1 == null) {
				e1 = e2;
				e2 = null;
			}
			if (e1 == null) {
				return true;
			}
			return ApqJS.Function.Apply(ApqJS.$Date_p.Equals, [e1, [e2]]);
		};
		ApqJS.$RegExp_p.Equals = function(r) {
			if (r instanceof RegExp) {
				return this.toString() == r.toString();
			}
			return false;
		};
		ApqJS.RegExp.Equals = function(r1, r2) {
			if (r1 == null) {
				r1 = r2;
				r2 = null;
			}
			if (r1 == null) {
				return true;
			}
			return ApqJS.Function.Apply(ApqJS.$RegExp_p.Equals, [r1, [r2]]);
		};

		/// foreach
		/// 遍历数组下标/对象成员,调用 fn.call( o, ary[i], i, ary )
		ApqJS.$Object_p.foreach = function(fn, o) {
			for (var p in this) {
				fn.call(o, this[p], p, this);
			}
		};
		ApqJS.$Array_p.foreach = function(fn, o) {
			for (var i = 0; i < this.length; i++) {
				fn.call(o, this[i], i, this);
			}
		};

		/// Array(兼容 arguments ) ----------------------------------------------------------------------------------------------------------------
		ApqJS.$Array_p.IndexOf = function(item, start) {
			for (var i = start || 0; i < this.length; i++) {
				if (this[i] == item) {
					return i;
				}
			}
			return -1;
		};

		ApqJS.$Array_p.LastIndexOf = function(item, end) {
			var l = this.length - 1;
			end = end || l;
			end = end < l ? end : l;
			for (var i = end; i >= 0; i--) {
				if (this[i] == item) {
					return i;
				}
			}
			return -1;
		};

		/// 不重复的添加
		/// <return>如果 o 已位于数组中,返回 o 在数组中的索引位置;否则返回数组的新长度</return>
		ApqJS.$Array_p.AddUnique = function(o) {
			var i = ApqJS.Array.IndexOf(this, o);
			if (i == -1) {
				Array.prototype.push.apply(this, [o]);
				i = this.length;
			}
			return i;
		};

		ApqJS.$Array_p.AddRange = function(ary) {
			ApqJS.Array.InsertRange(this, this.length, ary);
		};

		ApqJS.$Array_p.Clear = function() {
			return Array.prototype.splice.apply(this, [0, this.length]);
		};

		ApqJS.$Array_p.Contains = function(item) {
			return ApqJS.Array.IndexOf(this, item) != -1;
		};

		ApqJS.$Array_p.GetRange = function(index, count) {
			return Array.prototype.slice.apply(this, [index, index + count]);
		};

		ApqJS.$Array_p.SetRange = function(index, ary) {
			Array.prototype.splice.apply(this, [index, ary.length]);
			ApqJS.Array.InsertRange(this, index, ary);
		};

		ApqJS.$Array_p.Insert = function(index, item) {
			return Array.prototype.splice.apply(this, [index, 0, item]);
		};

		ApqJS.$Array_p.InsertRange = function(index, ary) {
			var args = Array.prototype.concat.apply(ary);
			Array.prototype.unshift.apply(args, [index, 0]);
			return Array.prototype.splice.apply(this, args);
		};

		/// 移除指定元素
		ApqJS.$Array_p.Remove = function(item) {
			var index = ApqJS.Array.IndexOf(this, item);
			if (index != -1) {
				return ApqJS.Array.RemoveAt(this, index);
			}
			return null;
		};

		/// 移除指定位置的元素,并返回移除的元素
		ApqJS.$Array_p.RemoveAt = function(index) {
			return Array.prototype.splice.apply(this, [index, 1])[0];
		};

		/// 交换位置
		ApqJS.$Array_p.Swap = function(item1, item2) {
			var n1 = ApqJS.Array.IndexOf(this, item1);
			var n2 = ApqJS.Array.IndexOf(this, item2);

			if (n1 != -1 && n2 != -1) {
				this[n1] = item2;
				this[n2] = item1;
			}
		};

		/// 对 数组/arguments 应用指定方法
		/// <o>数组/arguments</o>
		/// <fn>方法(名)</fn>
		/// <args> fn 的参数列表</args>
		ApqJS.Array.Apply = function(o, fn, args) {
			if (typeof fn == "string") {
				fn = o[fn] || Array.prototype[fn] || ApqJS.$Array_p[fn];
			}
			if (typeof fn == "function") {
				return ApqJS.Function.Apply(fn, [o, args]);
			}
			throw new Error(-1, "不存在指定函数");
		};

		ApqJS.Array.Repeat = function(value, count) {
			var ary = [];
			for (var i = 0; i < count; i++) {
				ary.push(value);
			}
			return ary;
		};

		/// Number --------------------------------------------------------------------------------------------------------------------------------
		ApqJS.Number.parse = function(value) {
			if (!value || (value.length == 0)) {
				return 0;
			}
			return parseFloat(value);
		}

		ApqJS.Number.MAPPING = {
			'0': '零',
			'1': '一',
			'2': '二',
			'3': '三',
			'4': '四',
			'5': '五',
			'6': '六',
			'7': '七',
			'8': '八',
			'9': '九'
		};

		ApqJS.Number.Quantity = ['', '十', '百', '千', '万', '十', '百', '千', '亿'];

		ApqJS.Number.NumberFormat = {
			"CurrencyDecimalDigits": 2,
			"CurrencyDecimalSeparator": ".",
			"CurrencyGroupSizes": [3],
			"NumberGroupSizes": [3],
			"PercentGroupSizes": [3],
			"CurrencyGroupSeparator": ",",
			"CurrencySymbol": "$",
			"CurrencyNegativePattern": 0,
			"NumberNegativePattern": 1,
			"PercentPositivePattern": 0,
			"PercentNegativePattern": 0,
			"NegativeSign": "-",
			"NumberDecimalDigits": 2,
			"NumberDecimalSeparator": ".",
			"NumberGroupSeparator": ",",
			"CurrencyPositivePattern": 0,
			"PercentDecimalDigits": 2,
			"PercentDecimalSeparator": ".",
			"PercentGroupSeparator": ",",
			"PercentSymbol": "%"
			//		"IsReadOnly":				false,
			//		"NaNSymbol":				"NaN",
			//		"NegativeInfinitySymbol":	"NEGATIVE_INFINITY",	//"-Infinity"
			//		"PositiveInfinitySymbol":	"POSITIVE_INFINITY",	//"Infinity"
			//		"PositiveSign":				"+",
			//		"PerMilleSymbol":			"",
			//		"NativeDigits":				["0","1","2","3","4","5","6","7","8","9"],
			//		"DigitSubstitution":		1
		}

		/// <format=null>true:中文习惯形式,false:中文发票形式,其余:非中文输出</format>
		ApqJS.$Number_p.ToString = function(format) {
			if (typeof format == "boolean") {
				var str = this.toString();
				var aryN = str.split('.');
				var strDec;

				if (aryN.length > 1) {
					var dec = ApqJS.String.ToCharArray(this);
					var ary = [];
					for (var i = 0; i < dec.length; i++) {
						ary.push(ApqJS.Number.MAPPING[dec[i]]);
					}
					strDec = ary.join('');
				}

				var ary = ApqJS.String.ToCharArray(aryN[0]).reverse();
				var aryInt = [];
				for (var k = 0; k < ary.length; k++) {
					var i8 = k & 0x07;
					var o = {};
					o.value = parseInt(ary[k]);
					o.val = ApqJS.Number.MAPPING[o.value];
					if (i8 == 0) {
						o.Quantity = ApqJS.String.Repeat(ApqJS.Number.Quantity[8], parseInt(k >>> 3));
						if (format && !o.value) {
							o.val = '';
						}
					}
					else {
						o.Quantity = ApqJS.Number.Quantity[i8];
						if (format && !o.value) {
							if (k & 0x03) {
								o.Quantity = '';
							}
							else {
								o.val = '';
							}
						}
					}
					aryInt.push(o);
				}

				ary.splice(0, ary.length);
				aryInt.reverse();
				for (var i = 0; i < aryInt.length; i++) {
					ary.push(aryInt[i].val + aryInt[i].Quantity);
				}
				str = ary.join('');
				if (format) {
					str = ApqJS.String.Trim(str, ApqJS.Number.MAPPING[0]).replace(/零+/g, ApqJS.Number.MAPPING[0]).replace("零万", "万").replace("亿万", "亿零");
				}
				return strDec ? str + '点' + strDec : str;
			}
			var _percentPositivePattern = ["n %", "n%", "%n"];
			var _percentNegativePattern = ["-n %", "-n%", "-%n"];
			var _numberNegativePattern = ["(n)", "-n", "- n", "n-", "n -"];
			var _currencyPositivePattern = ["$n", "n$", "$ n", "n $"];
			var _currencyNegativePattern = ["($n)", "-$n", "$-n", "$n-", "(n$)", "-n$", "n-$", "n$-", "-n $", "-$ n", "n $-", "$ n-", "$ -n", "n- $", "($ n)", "(n $)"];

			function expandNumber(number, precision, groupSizes, sep, decimalChar) {
				if (!(groupSizes.length)) {
					throw new Error(-1, "groupSizes must be an array of at least 1");
				}

				var curSize = groupSizes[0];
				var curGroupIndex = 1;

				var numberString = "" + number;
				var decimalIndex = numberString.indexOf('.');
				var right = "";
				if (decimalIndex > 0) {
					right = numberString.slice(decimalIndex + 1);
					numberString = numberString.slice(0, decimalIndex);
				}

				if (precision > 0) {
					var rightDifference = right.length - precision;
					if (rightDifference > 0) {
						right = right.slice(0, precision);
					}
					else if (rightDifference < 0) {
						for (var i = 0; i < Math.abs(rightDifference); i++) {
							right += '0';
						}
					}

					right = decimalChar + right;
				}
				else {
					right = "";
				}

				var stringIndex = numberString.length - 1;
				var ret = "";
				while (stringIndex >= 0) {

					if (curSize == 0 || curSize > stringIndex) {
						if (ret.length > 0)
							return numberString.slice(0, stringIndex + 1) + sep + ret + right;
						else
							return numberString.slice(0, stringIndex + 1) + right;
					}

					if (ret.length > 0)
						ret = numberString.slice(stringIndex - curSize + 1, stringIndex + 1) + sep + ret;
					else
						ret = numberString.slice(stringIndex - curSize + 1, stringIndex + 1);

					stringIndex -= curSize;

					if (curGroupIndex < groupSizes.length) {
						curSize = groupSizes[curGroupIndex];
						curGroupIndex++;
					}
				}
				return numberString.slice(0, stringIndex + 1) + sep + ret + right;
			}
			var nf = ApqJS.Number.NumberFormat;

			var number = Math.abs(this);

			if (!format)
				format = "D";

			var precision = -1;
			if (format.length > 1)
				precision = parseInt(format.slice(1));

			var pattern;
			switch (format.charAt(0)) {
				case "d":
				case "D":
					pattern = 'n';

					if (precision != -1) {
						var numberStr = "" + number;
						var zerosToAdd = precision - numberStr.length;
						if (zerosToAdd > 0) {
							for (var i = 0; i < zerosToAdd; i++) {
								numberStr = '0' + numberStr;
							}
						}
						number = numberStr;
					}

					if (this < 0)
						number = -number;
					break;
				case "c":
				case "C":
					if (this < 0)
						pattern = _currencyNegativePattern[nf.CurrencyNegativePattern];
					else
						pattern = _currencyPositivePattern[nf.CurrencyPositivePattern];
					if (precision == -1)
						precision = nf.CurrencyDecimalDigits;
					number = expandNumber(Math.abs(this), precision, nf.CurrencyGroupSizes, nf.CurrencyGroupSeparator, nf.CurrencyDecimalSeparator);
					break;
				case "n":
				case "N":
					if (this < 0)
						pattern = _numberNegativePattern[nf.NumberNegativePattern];
					else
						pattern = 'n';
					if (precision == -1)
						precision = nf.NumberDecimalDigits;
					number = expandNumber(Math.abs(this), precision, nf.NumberGroupSizes, nf.NumberGroupSeparator, nf.NumberDecimalSeparator);
					break;
				case "p":
				case "P":
					if (this < 0)
						pattern = _percentNegativePattern[nf.PercentNegativePattern];
					else
						pattern = _percentPositivePattern[nf.PercentPositivePattern];
					if (precision == -1)
						precision = nf.PercentDecimalDigits;
					number = expandNumber(Math.abs(this), precision, nf.PercentGroupSizes, nf.PercentGroupSeparator, nf.PercentDecimalSeparator);
					break;
				default:
					throw new Error(-1, '"' + format + '" 不是一个有效的格式');
			}

			var regex = /n|\$|-|%/g;

			var ret = "";

			for (; ; ) {
				var index = regex.lastIndex;
				var ar = regex.exec(pattern);
				ret += pattern.slice(index, ar ? ar.index : pattern.length);
				if (!ar)
					break;

				switch (ar[0]) {
					case "n":
						ret += number;
						break;
					case "$":
						ret += nf.CurrencySymbol;
						break;
					case "-":
						ret += nf.NegativeSign;
						break;
					case "%":
						ret += nf.PercentSymbol;
						break;
					default:
						throw new Error(-1, "我还不清楚是什么错误……");
				}
			}

			return ret;
		};

		/// String --------------------------------------------------------------------------------------------------------------------------------
		ApqJS.$String_p.PadLeft = function(width, c) {
			if (width <= this.length) {
				return this;
			}
			c = c || " ";
			var str = this;
			var ary = [];
			for (var i = this.length; i < width; i++) {
				ary.push(c);
			}
			return ary.join("") + str;
		};

		ApqJS.$String_p.PadRight = function(width, c) {
			if (width <= this.length) {
				return this;
			}
			c = c || " ";
			var str = this;
			var ary = [];
			for (var i = this.length; i < width; i++) {
				ary.push(c);
			}
			return str + ary.join("");
		};

		ApqJS.$String_p.ToCharArray = function(start, length) {
			start = start || 0;
			length = length || this.length;

			var end = start + length;
			end = end < this.length ? end : this.length;

			var ary = [];
			for (var i = start; i < end; i++) {
				ary.push(this.charAt(i));
			}
			return ary;
		};

		/// 空白字符集
		ApqJS.String.WhitespaceChars = [' ', '\f', '\n', '\r', '\t', '\v'];

		ApqJS.$String_p.TrimStart = function(chs) {
			return ApqJS.String._Trim(this, 0, chs);
		};

		ApqJS.$String_p.TrimEnd = function(chs) {
			return ApqJS.String._Trim(this, 1, chs);
		};

		ApqJS.$String_p.Trim = function(chs) {
			return ApqJS.String._Trim(this, 2, chs);
		};

		/// <type>修整方式[0:头,1:尾,2:两者]</type>
		ApqJS.String._Trim = function(str, type, chars) {
			chars = chars || ApqJS.String.WhitespaceChars;
			type = type == null ? 2 : type;

			if (typeof chars == "string") {
				chars = ApqJS.String.ToCharArray(chars);
			}
			else if (!(chars instanceof Array)) {
				throw new Error(1, "参数错误: [chars] 类型不匹配.");
			}

			var num1 = str.length - 1;
			var num2 = 0;
			if (type != 1) {
				for (num2 = 0; num2 < str.length; num2++) {
					var num3 = 0;
					var ch1 = str.charAt(num2);
					while (num3 < chars.length) {
						if (chars[num3] == ch1) {
							break;
						}
						num3++;
					}
					if (num3 == chars.length) {
						break;
					}
				}
			}
			if (type != 0) {
				for (num1 = str.length - 1; num1 >= num2; num1--) {
					var num4 = 0;
					var ch2 = str.charAt(num1);
					num4 = 0;
					while (num4 < chars.length) {
						if (chars[num4] == ch2) {
							break;
						}
						num4++;
					}
					if (num4 == chars.length) {
						break;
					}
				}
			}
			var num5 = (num1 - num2) + 1;
			if (num5 == str.length) {
				return str;
			}
			if (num5 == 0) {
				return "";
			}
			return str.substr(num2, num5);
		};

		ApqJS.String.Repeat = function(str, num) {
			var ary = [];
			for (i = 0; i < num; i++) {
				ary.push(str);
			}
			return ary.join("");
		};

		/// 格式化字符串
		ApqJS.String.Format = function(format) {
			var ary = [];

			for (var i = 0; ; ) {
				var next = format.indexOf("{", i);
				if (next < 0) {
					ary.push(format.slice(i));
					break;
				}

				ary.push(format.slice(i, next));
				i = next + 1;

				if (format.charAt(i) == '{') {
					ary.push('{');
					i++;
					continue;
				}

				next = format.indexOf("}", i);

				var brace = format.slice(i, next).split(':');

				var argNumber = ApqJS.Number.parse(brace[0]) + 1;
				var arg = arguments[argNumber];
				if (arg == null) {
					arg = '';
				}
				ary.push(arg.toString(brace[1] || 10));
				i = next + 1;
			}

			return ary.join("");
		};

		/// 第一个字母大小写切换
		ApqJS.String.ChangeFirst = function(str) {
			var nc = str.charCodeAt(0);
			var nu = nc & 0xDF;
			var nl = nc | 0x20;
			var n = nc == nu ? nl : nu;
			var c = String.fromCharCode(n);
			return c + str.substr(1);
		};

		/// 第一个字母大写转换
		ApqJS.String.ChangeFirstUpper = function(str) {
			return str.charAt(0).toUpperCase() + str.substr(1);
		};

		/// 第一个字母小写转换
		ApqJS.String.ChangeFirstLower = function(str) {
			return str.charAt(0).toLowerCase() + str.substr(1);
		};

		/// 获取随机字符
		/// <str>字符集</str>
		ApqJS.String.RandomChar = function(str) {
			return str.charAt(parseInt(str.length * Math.random()));
		};

		/// 获取随机字符串
		/// <Length>长度</Length>
		/// <str>字符集</str>
		ApqJS.String.RandomString = function(Length, str) {
			var ary = [];
			for (var i = 0; i < Length; i++) {
				ary.push(String.RandomChar(str));
			}
			return ary.join("");
		};

		ApqJS.String.IsEmail = function(str) {
			var reg = /^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
			return reg.test(str);
		};

		/// <fMin>最少小数位数</fMin>
		/// <fMax>最多小数位数</fMax>
		/// <AllowInt>整数是否通过检验</AllowInt>
		ApqJS.String.IsNumber = function(str, fMin, fMax, AllowInt) {
			var strMin = fMin > 0 ? fMin.toString() : "0";
			var strMax = fMax > 0 ? fMax.toString() : "";
			var strAllowInt = AllowInt ? "?" : "";
			var reg = new RegExp("^\\-?([1-9]\\d*|0)(\\.\\d{" + strMin + "," + strMax + "})" + strAllowInt + "$");
			return reg.test(str);
		};

		/// Date ----------------------------------------------------------------------------------------------------------------------------------
		ApqJS.Date.WEEK = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];

		ApqJS.$Date_p.ToString = function(strF) {
			strF = strF || "[yyyy]-[MM]-[dd] [HH]:[mm]:[ss]";
			var reg = /\[(\w+)\]/g;
			var ary = [];
			var sar = strF.split("[[");

			for (var i = 0; i < sar.length; i++) {
				var str = sar[i];
				if (str) {
					var a = 0;
					var b = 0;
					var arr;
					while (arr = reg.exec(str)) {
						if (arr.index > b) {
							ary.push(str.substring(b, arr.index));
						}
						ary.push(ApqJS.Date.GetFormat(this, arr[1]));

						a = arr.index;
						b = arr.lastIndex;
					}
					if (b < str.length) {
						ary.push(str.substring(b, str.length));
					}
				}
				if (i < sar.length - 1) {
					ary.push("[");
				}
			}

			return ary.join("");
		};

		ApqJS.$Date_p.GetFormat = function(f) {
			switch (f) {
				case "yyyy":
					return this.getFullYear().toString();
				case "yy":
					return this.getFullYear().toString().substr(2, 2);
				case "y":
					return ApqJS.String.TrimStart(this.getFullYear().toString().substr(2, 2), '0');
				case "M":
					return (this.getMonth() + 1).toString();
				case "MM":
					return ApqJS.String.PadLeft((this.getMonth() + 1).toString(), 2, '0');
				case "MMM":
				case "MMMM":
					var nM = this.getMonth() + 1;
					if (nM < 10) {
						return ApqJS.Number.MAPPING[nM] + "月";
					}
					if (nM == 10) {
						return "十月";
					}
					else {
						return "十" + ApqJS.Number.MAPPING[nM - 10] + "月";
					}
				case "d":
					return this.getDate().toString();
				case "dd":
					return ApqJS.String.PadLeft(this.getDate().toString(), 2, '0');
				case "ddd":
					return ApqJS.Number.MAPPING[this.getDay()];
				case "dddd":
					return "星期" + ApqJS.Number.MAPPING[this.getDay()];
				case "H":
					return this.getHours().toString();
				case "HH":
					return ApqJS.String.PadLeft(this.getHours().toString(), 2, '0');
				case "h":
					return (this.getHours() - 12).toString();
				case "hh":
					return (this.getHours() - 12).toString().PadLeft(2, '0');
				case "m":
					return this.getMinutes().toString();
				case "mm":
					return ApqJS.String.PadLeft(this.getMinutes().toString(), 2, '0');
				case "s":
					return this.getSeconds().toString();
				case "ss":
					return ApqJS.String.PadLeft(this.getSeconds().toString(), 2, '0');
				case "FFF":
					return (this.getMilliseconds() / 1000).toString().substr(2);
				case "fff":
					return ApqJS.String.PadRight((this.getMilliseconds() / 1000).toString().substr(2), 3, '0');
				case "ffff":
					return ApqJS.String.PadRight((this.getMilliseconds() / 1000).toString().substr(2), 3, '0') + "0";
				case "fffff":
					return ApqJS.String.PadRight((this.getMilliseconds() / 1000).toString().substr(2), 3, '0') + "00";
				case "ffffff":
					return ApqJS.String.PadRight((this.getMilliseconds() / 1000).toString().substr(2), 3, '0') + "000";
				case "fffffff":
					return ApqJS.String.PadRight((this.getMilliseconds() / 1000).toString().substr(2), 3, '0') + "0000";
				case "f":
					return ApqJS.String.PadRight((this.getMilliseconds() / 1000).toString().substr(2), 3, '0').substr(0, 1);
				case "fff":
					return ApqJS.String.PadRight((this.getMilliseconds() / 1000).toString().substr(2), 3, '0').substr(0, 2);
				case "F":
					return ApqJS.String.TrimEnd(ApqJS.String.PadRight((this.getMilliseconds() / 1000).toString().substr(2), 3, '0').substr(0, 1), '0');
				case "FF":
					return ApqJS.String.TrimEnd(ApqJS.String.PadRight((this.getMilliseconds() / 1000).toString().substr(2), 3, '0').substr(0, 2), '0');
				case "t":
					return this.getHours() > 12 ? '下' : '上';
				case "tt":
					return (this.getHours() > 12 ? '下' : '上') + "午";
				case "z":
					var tzo = this.getTimezoneOffset();
					var sign = tzo > 0 ? '-' : '+';
					tzo = Math.abs(tzo);
					return ApqJS.String.Format("{0}{1}", sign, tzo / 60);
				case "zz":
					var tzo = this.getTimezoneOffset();
					var sign = tzo > 0 ? '-' : '+';
					tzo = Math.abs(tzo);
					return ApqJS.String.Format("{0}{1}", sign, ApqJS.String.PadLeft(parseInt(tzo / 60).toString(), 2, '0'));
				case "zzz":
					var tzo = this.getTimezoneOffset();
					var sign = tzo > 0 ? '-' : '+';
					tzo = Math.abs(tzo);
					return ApqJS.String.Format("{0}{1}:{2}", sign, ApqJS.String.PadLeft(parseInt(tzo / 60).toString(), 2, '0'), ApqJS.String.PadLeft(parseInt(tzo % 60).toString(), 2, '0'));
				default:
					return "";
			}
		};

		/// 是否闰年
		ApqJS.$Date_p.IsLeapYear = function() {
			var nYear = this.getFullYear();
			return (nYear % 4 == 0 && nYear % 100 != 0) || nYear % 400 == 0;
		};

		/// Error ---------------------------------------------------------------------------------------------------------------------------------
		ApqJS.$Error_p.ToString = function() {
			return ApqJS.String.Format("错误号码:{0}\n错误类型:{1}\n错误信息:{2}", this.number & 0xFFFF, this.name, this.message);
		};

		/// 简写且增强的 document.getElementById 
		ApqJS.$ = function() {
			var ary = [];
			for (var i = 0; i < arguments.length; i++) {
				var o = arguments[i];
				if (typeof o == 'string') {
					o = document.getElementById(o);
				}
				if (arguments.length <= 1) {
					return o;
				}
				ary.push(o);
			}
			return ary;
		};

		ApqJS.$o = function() {
			var ary = [];
			for (var i = 0; i < arguments.length; i++) {
				var o = arguments[i];
				if (typeof o == 'string' && ApqJS.String.Trim(o) != "") {
					var ps = o.split(".");
					o = window;
					for (var j = 0; j < ps.length; j++) {
						if (typeof o[ps[j]] != "undefined" && typeof o[ps[j]] != "unkown") {
							o = o[ps[j]];
						}
						else {
							o = null;
							break;
						}
					}
				}
				if (arguments.length == 1) {
					return o;
				}
				ary.push(o);
			}
			return ary;
		};

		/// 完成模拟内置对象 ----------------------------------------------------------------------------------------------------------------------
		ApqJS.Prototype = {};
		/// <pf>源(模拟原型对象)</pf>
		/// <pt>目的(真实原型对象)<pt>
		ApqJS.Prototype.Copy = function(pf, pt) {
			for (var n in pf) {
				if (pf.hasOwnProperty(n) && typeof pt[n] == "undefined") {
					pt[n] = ApqJS.Prototype.$Copy(pf[n]);
				}
			}
		};

		ApqJS.Prototype.$Copy = function(fn) {
			return function() {
				var o = Array.prototype.shift.apply(arguments);
				return fn.apply(o, arguments);
			};
		};

		ApqJS.Prototype.Copy(ApqJS.$Object_p, ApqJS.Object);
		ApqJS.Prototype.Copy(ApqJS.$Array_p, ApqJS.Array);
		ApqJS.Prototype.Copy(ApqJS.$Function_p, ApqJS.Function);
		ApqJS.Prototype.Copy(ApqJS.$String_p, ApqJS.String);

		ApqJS.Prototype.Copy(ApqJS.$Boolean_p, ApqJS.Boolean);
		ApqJS.Prototype.Copy(ApqJS.$Number_p, ApqJS.Number);

		ApqJS.Prototype.Copy(ApqJS.$Date_p, ApqJS.Date);
		ApqJS.Prototype.Copy(ApqJS.$Error_p, ApqJS.Error);
		ApqJS.Prototype.Copy(ApqJS.$Enumerator_p, ApqJS.Enumerator);
		ApqJS.Prototype.Copy(ApqJS.$RegExp_p, ApqJS.RegExp);

		/// 系统"关键字"定义 ----------------------------------------------------------------------------------------------------------------------
		/// namespace
		ApqJS.namespace = function(ns) {
			var nss = ns.split(".");
			var root = window;
			for (var i = 0; i < nss.length; i++) {
				if (typeof (root[nss[i]]) == "undefined") {
					root[nss[i]] = {
						"$key": "namespace",
						"$type": (root.$type ? (root.$type + ".") : "") + nss[i]
					};
				}
				root = root[nss[i]];
			}
			return root;
		};

		/// using
		/// <g=true>是否将该空间定义保存到主框架</g>
		ApqJS.using = function(ns, url, g) {
			if (ns == "ApqJS") {
				return;
			}

			if (Apq_JSContainer[ns]) {
				eval(Apq_JSContainer[ns]);
				return;
			}

			url = url || (ns + ".js");
			if (ns.indexOf("ApqJS") == 0) {
				switch (String.fromCharCode(ns.charCodeAt(5))) {
					case "/":
						url = Apq_InitConfig.ApqJSFolder + ns.substr(6) + ".js";
						break;
					case ".":
						url = Apq_InitConfig.ApqJSFolder + url;
						break;
				}
			}

			if (!window.Apq_JSXH) {
				return;
			}

			Apq_JSXH.open("GET", url, false);
			Apq_JSXH.send();
			if (Apq_JSXH.status == 200) {
				Apq_JSContainer[ns] = Apq_JSXH.responseText;
				eval(Apq_JSXH.responseText);
				return;
			}
		};

		/// Class
		/// <$type>类全名</$type>
		/// <ihs>(单)继承列表</ihs>
		/// <$abstract>是否抽象类</$abstract>
		/// <$sealed>是否密封类</$sealed>
		ApqJS.Class = function($type, ihs, $abstract, $sealed) {
			var fn = function() {
				// 调用构造函数
				// 直接调用
				if (this.ctor == fn.prototype.ctor) {
					if (fn.$abstract) {
						throw new Error(-1, "抽象类不能创建实例");
					}
					this.ctor.apply(this, arguments);
				}
				// 由派生类调用
				else {
					fn.prototype.ctor.apply(this, arguments);
				}
			};

			/// 关键语句,不能删除
			fn.prototype.constructor = fn;

			// 设置类信息
			ApqJS.Object.Set(fn.prototype, "$key", "class", false, false);
			ApqJS.Object.Set(fn.prototype, "$type", $type, false, false);
			ApqJS.Object.Set(fn, "$key", "class", false, false);
			ApqJS.Object.Set(fn, "$type", $type, false, false);
			ApqJS.Object.Set(fn, "$abstract", $abstract || false, false, false);
			ApqJS.Object.Set(fn, "$sealed", $sealed || false, false, false);

			// 提供默认构造函数和析构函数
			fn.prototype.ctor = function() {
				if (fn.base) {
					fn.base.prototype.ctor.apply(this, arguments);
				}
			};
			fn.prototype.Finalize = function() {
				if (fn.base) {
					fn.base.prototype.Finalize.apply(this, arguments);
				}
			};
			// 提供基类初始化函数
			fn.prototype.base = function() {
				if (fn.base) {
					fn.base.prototype.ctor.apply(this, arguments);
				}
			};

			// 继承列表
			var base;
			fn.$ifs = ihs;
			if (ihs && ihs.length) {
				// 分离基类
				base = ihs[0].$key == "class" ? ihs.shift() : null;

				// 实现接口
				if (fn.$ifs) {
					for (var i = 0; i < fn.$ifs.length; i++) {
						ApqJS.Object.CopyFrom(fn.prototype, fn.$ifs[i], false);
					}
				}
			}
			// 设置基类
			ApqJS.Class.base_set(fn, base);
			return fn;
		};

		/// 设置基类
		ApqJS.Class.base_set = function(cls, base) {
			cls.base = base;
			if (!base) {
				return cls;
			}

			if (base.$sealed) {
				throw new Error(-1, "不能继承密封类: " + base.$type);
			}
			for (var p in base.prototype) {
				if (typeof (cls.prototype[p]) == "undefined" && p.indexOf("$")) {
					cls.prototype[p] = base.prototype[p];
				}
			}
			ApqJS.Object.CopyFrom(cls, base, false);
			return cls;
		};

		/// 获取基类列表
		ApqJS.Class.bases_get = function(cls) {
			var ary = [];
			for (var cb = cls.base; cb; cb = cb.base) {
				ary.push(cb);
			}
			return ary;
		};

		/// 获取运行时类层次列表
		ApqJS.Class.Runtime_get = function(o) {
			var ary = ApqJS.Class.bases_get(o.constructor);
			ary.unshift(o.constructor);
			return ary;
		};

		/// 根据运行时对象 o 获取指定类 cls 的运行时直接派生类,无派生类则返回 null
		ApqJS.Class.Runtime_child = function(o, cls) {
			for (var cb = o.constructor; cb; cb = cb.base) {
				if (ApqJS.Class.Equals(cb.base, cls)) {
					return cb;
				}
			}
			return null;
		};

		/// 类是否相同
		ApqJS.Class.Equals = function(c1, c2) {
			var t1 = ApqJS.String.ChangeFirstUpper(ApqJS.GetTypeName(c1));
			var t2 = ApqJS.String.ChangeFirstUpper(ApqJS.GetTypeName(c2));
			return t1 == t2;
		};

		/// interface
		/// <$type>接口全名</$type>
		/// <$ifs>继承列表</$ifs>
		/// <fns>方法名列表</fns>
		ApqJS.interface = function($type, $ifs, fns) {
			var it = {
				"$key": "interface",
				"$type": $type
			};
			// 继承接口
			it.$ifs = $ifs;
			if (it.$ifs) {
				for (var i = 0; i < it.$ifs.length; i++) {
					ApqJS.Object.CopyFrom(it, it.$ifs[i], false);
				}
			}
			// 定义方法
			if (fns) {
				for (var i = 0; i < fns.length; i++) {
					it[fns[i]] = ApqJS.Function.Abstract;
				}
			}
			return it;
		};

		/// delegate
		/// <fn>需要扩展为委托的函数(一定会执行)</fn>
		/// <o> fn 的 Context 对象</o>
		ApqJS.delegate = function(fn, o) {
			var d = function() {
				if (fn) {
					fn.apply(o == null ? window : o, arguments);
				}
				return d._delegate.Invoke.apply(d._delegate, arguments);
			};
			d.$key = "delegate";
			d.$type = "ApqJS.delegate";
			d._delegate = new ApqJS.delegate._delegate();
			ApqJS.Function.bind(d._delegate.add, d, "add", d._delegate);
			ApqJS.Function.bind(d._delegate.remove, d, "remove", d._delegate);
			ApqJS.Function.bind(d._delegate.InvokeAll, d, "InvokeAll", d._delegate);
			return d;
		};

		ApqJS.delegate._delegate = ApqJS.Class("ApqJS.delegate");

		ApqJS.delegate._delegate.prototype.ctor = function() {
			this.methods = [];
			this.contexts = [];
			this.Enable = true; // 用于 启用/禁用 委托
		};

		ApqJS.delegate._delegate.prototype.add = function(f, o) {
			if (typeof f == "string") {
				f = ApqJS.Object.Get(o, f);
			}
			if (typeof f == "function") {
				this.methods.push(f);
				this.contexts.push(o);
			}
			else {
				ApqJS.Debug.writeln("未找到方法,此次向委托添加方法已忽略");
			}
		};

		/// <f>方法 或 成员名</f>
		ApqJS.delegate._delegate.prototype.remove = function(f, o) {
			if (typeof f == "string") {
				f = ApqJS.Object.Get(o, f);
			}
			if (typeof f == "function") {
				for (var i = this.methods.length - 1; i >= 0; i--) {
					if (this.methods[i] == f && this.contexts[i] == o) {
						this.methods.RemoveAt(i);
						this.contexts.RemoveAt(i);
					}
				}
			}
		};

		/// 普通调用
		ApqJS.delegate._delegate.prototype.Invoke = function() {
			if (this.Enable) {
				var v;
				for (var i = 0; i < this.methods.length; i++) {
					v = this.methods[i].apply(this.contexts[i], arguments);
				}
				return v;
			}
		};

		/// 引发事件
		ApqJS.delegate._delegate.prototype.Fire = function() {
			if (this.Enable) {
				var v;
				for (var i = this.methods.length - 1; i >= 0; i--) {
					v = this.methods[i].apply(this.contexts[i], arguments);
				}
				return v;
			}
		};
		ApqJS.delegate._delegate.prototype.InvokeAll = function() {
			if (this.Enable) {
				for (var i = 0; i < this.methods.length; i++) {
					ApqJS.setTimeout(0, this.methods[i], arguments, this.contexts[i]);
				}
			}
		};
		ApqJS.delegate._delegate.prototype.Equals = function(di) {
			if (ApqJS.GetTypeName(di) == this.$type) {
				for (var i = 0; i < this.methods.length; i++) {
					if (this.methods[i] != di.methods[i] || this.contexts[i] != di.contexts[i]) {
						return false;
					}
				}
				return true;
			}
			return false;
		};

		ApqJS.setTimeout = function(t, fn, args, o) {
			var f = function() {
				fn.apply(o, args);
			};
			return setTimeout(f, t);
		};

		ApqJS.Event = function() {
			var d = ApqJS.delegate.apply(ApqJS, arguments);
			d.$key = "Event";
			d.$type = "ApqJS.Event";
			ApqJS.Function.bind(d._delegate.Fire, d, "Fire", d._delegate);
			return d;
		};

		ApqJS.Event.ReturnFalse = function(e) {
			return false;
		};

		/// is
		ApqJS.is = function(o, cls) {
			if (o != null) {
				// 递归搜索基类及接口
				if (ApqJS.Class.Equals(o, cls)) {
					return true;
				}
				if (o.base) {
					if (ApqJS.is(o.base, cls)) {
						return true;
					}
				}
				if (o.$ifs) {
					for (var i = 0; i < o.$ifs.length; i++) {
						if (ApqJS.is(o.$ifs[i], cls)) {
							return true;
						}
					}
				}
			}
			return cls == null;
		};

		/// 函数构造器 ----------------------------------------------------------------------------------------------------------------------------
		/// <prt>需要增加方法的对象,一般为原型</prt>
		ApqJS.Function.Create = function(prt, n, fn, v) {
			var rfn = function() {
				// 虚函数 且 由派生类对象调用
				if (v && this.$type != prt.$type)	// this.$type 为运行时对象的类名
				{
					var c = ApqJS.Class.Runtime_child(this, prt); // 查找 prt 的运行时直接派生类
					// 未找到 prt 的直接派生类
					if (!c) {
						throw new Error(-1, "类层次结构异常.请检查类定义: " + this.$type);
					}
					// 已找到 prt 的直接派生类
					if (c.prototype[n]) {
						return c.prototype[n].apply(this, arguments);
					}
					// 默认采用对象的同名成员运行
					return this[n].apply(this, arguments);
				}
				// 非虚函数 或 由本类对象调用
				return fn.apply(this, arguments);
			};
			rfn.$mn = n;
			prt[n] = rfn;
			return rfn;
		};

		/// 抽象方法
		ApqJS.Function.Abstract = function() {
			throw new Error(-1, '抽象方法应由子类实现.');
		};

		/// 模拟反射 ------------------------------------------------------------------------------------------------------------------------------
		/// 获取类全名
		ApqJS.GetTypeName = function(o) {
			if (typeof o == "undefined") {
				return "undefined";
			}
			if (o == null) {
				return "null";
			}
			if (o.$type) {
				return o.$type;
			}
			if (typeof o != "object") {
				// boolean, number, string, function
				return typeof o;
			}
			switch (o.constructor) {
				case Array:
					return "Array";
				case Boolean:
					return "Boolean";
				case Date:
					return "Date";
				case Enumerator:
					return "Enumerator";
				case Error:
					return "Error";
				case Function:
					return "Function";
				case Number:
					return "Number";
				case RegExp:
					return "RegExp";
				case String:
					return "String";
				case VBArray:
					return "VBArray";
			}
			// Xml 相关类
			if (o.documentElement) {
				return "System.Xml.XmlDocument";
			}
			if (o.xml) {
				return "System.Xml.XmlNode";
			}
			return "object";
		};

		/// ApqJS.Convert ---------------------------------------------------------------------------------------------------------------------------
		ApqJS.Convert = {
			ToBoolean: function(o) {
				if (!o) {
					return false;
				}

				switch (ApqJS.GetTypeName(o).toLowerCase()) {
					case "number":
						return isNaN(o) ? false : o != 0;

					case "string":
						return o.toLowerCase() == "true";

					default: // array, bool, date, object, regexp...
						return true;
				}
			},

			ToDate: function(o) {
				switch (ApqJS.GetTypeName(o).toLowerCase()) {
					case "date":
						return o;

					case "number":
						return new Date(o);

					case "string":
						// yyyy-MM-dd[( |T)HH:mm:ss.fffffff]
						var ds = o.split(/[:T \.\+-]/);
						var ns = [];
						var n = ds.length < 7 ? ds.length : 7;
						for (var i = 0; i < n; i++) {
							ns.push(parseInt(ds[i], 10));
						}
						return new Date(ns[0], ns[1] - 1, ns[2], ns[3] || null, ns[4] || null, ns[5] || null, ns[6] / 10000 || null);

					default: // array, bool, object, regexp...
						return null;
				}
			}
		};

		/// ApqJS.Common ----------------------------------------------------------------------------------------------------------------------------
		ApqJS.Common = {};

		ApqJS.Common.HtmlEncode = function(s) {
			if (!s) return s;
			return s.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
		};

		ApqJS.Common.HtmlDecode = function(s) {
			if (!s) return s;
			return s.replace(/&gt;/g, ">").replace(/&lt;/g, "<").replace(/&amp;/g, "&");
		};

		/// ApqJS.EventArgs -------------------------------------------------------------------------------------------------------------------------
		ApqJS.EventArgs = ApqJS.Class("ApqJS.EventArgs");
		ApqJS.EventArgs.prototype.ctor = function(Value) {
			this.cancel = false;
			this.Value = Value;
		};

		/// ApqJS.Trace & ApqJS.Debug -----------------------------------------------------------------------------------------------------------------
		ApqJS.clsTrace = function() {
			this.win = window;
			this.Events = {
				"onwrite": ApqJS.Event(),
				"onwriteln": ApqJS.Event()
			};
		};

		ApqJS.clsTrace.prototype.write = function(o) {
			var e = new ApqJS.EventArgs(o);
			this.Events["onwrite"].Fire(this, e);
		};

		ApqJS.clsTrace.prototype.writeln = function(o) {
			var e = new ApqJS.EventArgs(o);
			this.Events["onwriteln"].Fire(this, e);
		};

		ApqJS.Debug = new ApqJS.clsTrace();

		/// ApqJS.Argument --------------------------------------------------------------------------------------------------------------------------
		/// 提供参数常规检验功能
		ApqJS.Argument = {
			/// 返回 o1 与 o2 在应用运算 op 后的结果
			"Check": function(op, o1, o2) {
				return eval("o1 " + op + " o2");
			},
			"CheckNull": function(n, v) {
				if (v == null) {
					throw new Error(1, ApqJS.String.Format("参数 [{0}] 不能为 null", n));
				}
			},

			/// <n>参数名</n>
			/// <a>参数</a>
			/// <cls>期望类型<cls>
			"CheckType": function(n, v, cls) {
				if (!ApqJS.is(v, cls)) {
					throw new Error(2, ApqJS.String.Format("参数 [{0}] 类型不匹配,期望类型为: {1}", n, ApqJS.GetTypeName(cls)));
				}
			}
		};

		/// ApqJS.Cookie ----------------------------------------------------------------------------------------------------------------------------
		ApqJS.Cookie = ApqJS.Class("ApqJS.Cookie");
		ApqJS.Cookie.prototype.ctor = function(name, value, expires, path, domain, secure) {
			this.name = name || ""; 	// Cookie名
			this.value = value; 		// 值
			this.expires = expires; 	// 期满
			this.path = path || "/"; // 目录
			this.domain = domain; 	// 访问域
			this.secure = secure; 	// 保护
		};

		/// 从字符串加载 Cookie
		ApqJS.Cookie.prototype.LoadStr = function(str) {
			var ary = str.split("=");
			if (ary.length) {
				this.name = ary[0];
				this.value = ary[1];
			}
		};

		ApqJS.Cookie.prototype.Save = function() {
			var strname = this.name + "=" + escape(this.value) + " ;path = " + this.path;
			var strexpires = this.expires ? " ;expires = " + this.expires.toGMTString() : "";
			var strdomain = this.domain ? " ;domain = " + this.domain : "";
			var strsecure = this.secure ? ";secure" : "";
			document.cookie = strname + strexpires + strdomain + strsecure;
		};

		/// document ------------------------------------------------------------------------------------------------------------------------------
		ApqJS.document = {};

		ApqJS.document.__ref = function(ns, dir, ext) {
			dir = dir || "";
			ext = ext || "js";
			document.write('<script type="text/javascript" src="');
			document.write(dir);
			document.write(ns);
			document.write('.' + ext + '"></\script>');
		};

		/// <fn>回调函数</fn>
		ApqJS.document.addBehavior = function(o, ns, fn, dir) {
			dir = dir || "";
			if (ns.toLowerCase() == "webservice") {
				dir = dir || Apq_InitConfig.ApqJSFolder;
			}
			if (fn) {
				o.onreadystatechange = function() {
					ApqJS.document.__addBehavior(fn, o);
				};
			}
			o.addBehavior(dir + ns + ".htc");
		};

		ApqJS.document.__addBehavior = function(fn, o) {
			if (o.readyState == "complete") {
				fn.call(o);
			}
		};

		ApqJS.document.FindIFrameElement = function(win) {
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
		};

		ApqJS.document.iframeAutoFit = function(evt, win) {
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
		}

		/// 创建元素
		ApqJS.document.CreateElement = function(sTag, doc) {
			doc = doc || document;
			var he = doc.createElement(sTag);
			he.id = he.uniqueID || doc.uniqueID;
			return he;
		};

		/// 添加/修改 属性
		ApqJS.document.setNamedItem = function(he, name, value) {
			var ha = document.createAttribute(name);
			ha.value = value;
			he.attributes.setNamedItem(ha);
			return ha;
		};

		/// 添加子级元素
		/// <sWhere="beforeEnd"></sWhere>
		ApqJS.document.insertAdjacentElement = function(he, sWhere, oc) {
			he.insertAdjacentElement(sWhere || "beforeEnd", oc);
		};

		/// 添加 HTML 到标签内
		/// <sWhere="beforeEnd"></sWhere>
		ApqJS.document.insertAdjacentHTML = function(he, sWhere, sHTML) {
			he.insertAdjacentHTML(sWhere || "beforeEnd", sHTML);
		};

		/// 添加子元素 ne 到 he 的已有子元素 hc 之前
		/// <hc>为 null 时添加到所有子元素之前</hc>
		ApqJS.document.insertBefore = function(he, ne, hc) {
			if (!hc) hc = null;
			return he.insertBefore(ne, hc);
		};

		//获取元素绝对位置Top
		ApqJS.document.GetAbsTop = function getAbsTop(he) {
			var offset = he.offsetTop;
			if (he.offsetParent) offset += ApqJS.document.GetAbsTop(he.offsetParent);
			return offset;
		}

		//获取元素绝对位置Left
		ApqJS.document.GetAbsLeft = function getAbsTop(he) {
			var offset = he.offsetLeft;
			if (he.offsetParent) offset += ApqJS.document.GetAbsLeft(he.offsetParent);
			return offset;
		}

		/// 获取指定Cookie名的值
		/// <name>Cookie名[ASP.NET_SessionId]</name>
		ApqJS.document.getCookie = function(name) {
			for (var i = 0; i < ApqJS.document.Cookies.length; i++) {
				if (ApqJS.document.Cookies[i].name == name) {
					return ApqJS.document.Cookies[i].value;
				}
			}
			var aryCookie = document.cookie.split(/; |=/);
			for (var i = 0; i < aryCookie.length; i++) {
				if (aryCookie[i++] == name) {
					return unescape(aryCookie[i])
				}
			}
			return null;
		};

		/// 设置Cookie
		/// <name>Cookie名</name>
		/// <value>值</value>
		/// <expires>期满</expires>
		/// <path>目录</path>
		/// <domain>域</domain>
		/// <secure>保护</secure>
		ApqJS.document.setCookie = function(name, value, expires, path, domain, secure) {
			var cookie;
			var exist = false;
			for (var i = 0; i < ApqJS.document.Cookies.length; i++) {
				if (exist = ApqJS.document.Cookies[i].name == name) {
					cookie = ApqJS.document.Cookies[i] = new ApqJS.Cookie(name, value, expires, path, domain, secure);
					break;
				}
			}
			if (!exist) {
				cookie = new ApqJS.Cookie(name, value, expires, path, domain, secure);
				ApqJS.document.Cookies.push(cookie);
			}
			cookie.Save();
		};

		/// 删除指定的Cookie
		/// <name>Cookie名</name>
		ApqJS.document.delCookie = function(name) {
			var exp = new Date();
			exp.setTime(exp.getTime() - 1);
			document.cookie = name + "=Deleted; expires=" + exp.toGMTString();
			for (var i = 0; i < ApqJS.document.Cookies.length; i++) {
				if (ApqJS.document.Cookies[i].name == name) {
					ApqJS.Array.RemoveAt(ApqJS.document.Cookies, i);
					break;
				}
			}
		};

		ApqJS.document.refreshCookies = function() {
			ApqJS.document.Cookies = [];
			if (document.cookie.length) {
				var ary = document.cookie.split("; ");
				for (var i = 0; i < ary.length; i++) {
					var cookie = new ApqJS.Cookie();
					cookie.LoadStr(unescape(ary[i]));
					ApqJS.document.Cookies.push(cookie);
				}
			}
		};

		/// 获取当前的 Cookies
		ApqJS.document.refreshCookies();

		/// location --------------------------------------------------------------------------------------------------------------
		ApqJS.location = {};

		ApqJS.location.getQueryString = function(str) {
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
		};

		ApqJS.location.BuildSearch = function(QueryString) {
			var ary = [];
			var keys = ApqJS.Object.GetExpandoProperties(QueryString);
			for (var i = 0; i < keys.length; i++) {
				ary.push(keys[i] + "=" + QueryString[keys[i]]);
			}
			return ary.join("&");
		};

		/// 计算当前 location 的 QueryString
		ApqJS.location.QueryString = ApqJS.location.getQueryString(location.search);

		ApqJS.location.removeSearch = function(str) {
			var index = str.indexOf('?');
			if (index != -1) {
				str = str.substr(0, index);
			}
			return str;
		};

		/// 对象属性遍历器 ------------------------------------------------------------------------------------------------------------------------
		/// 可以 获取指定位置的遍历项，修改当前遍历项
		ApqJS.Enumerator = ApqJS.Class("ApqJS.Enumerator");
		/// <o>要遍历的 Object </o>
		/// <k>跳过列表</k>
		/// <f>是否包含方法</f>
		ApqJS.Enumerator.prototype.ctor = function(o, k, f) {
			ApqJS.Argument.CheckNull("o", o);
			this.o = o; 	// 当前遍历的对象
			this.a = []; // 属性值列表
			this.n = []; // 属性名列表
			for (var p in o) {
				if (k && k.Contains(p)) {
					continue;
				}
				if (!f && typeof o[p] == "function") {
					continue;
				}
				this.a.push(o[p]);
				this.n.push(p);
			}
			this.moveFirst();
		};

		ApqJS.Enumerator.prototype.GetPosition = function(i) {
			if (i >= 0 && i < this.a.length) {
				return this.a[i];
			}
			return null;
		};

		ApqJS.Enumerator.prototype.SetPosition = function(i) {
			if (i == null) {
				i = this.i;
			}
			if (i < 0 || i > this.a.length) {
				throw new Error(1, "已超出集合范围.");
			}
			this.i = i;
		};

		ApqJS.Enumerator.prototype.GetItem = function() {
			return this.GetPosition(this.i);
		};

		ApqJS.Enumerator.prototype.SetItem = function(m) {
			this.a[this.i] = this.o[this.n[this.i]] = m;
		};

		ApqJS.Enumerator.prototype.moveFirst = function() {
			this.SetPosition(0);
		};

		ApqJS.Enumerator.prototype.moveNext = function() {
			this.SetPosition(this.i + 1);
		};

		ApqJS.Enumerator.prototype.item = ApqJS.Enumerator.prototype.GetItem;

		ApqJS.Enumerator.prototype.atEnd = function() {
			return this.i >= this.a.length;
		};

		ApqJS.Enumerator.prototype.ItemName = function() {
			return this.n[this.i];
		};

		/// ApqJS.Shell --------------------------------------------------------------------------------------------------------------------------------
		/// 接受格式:命令([列表])*
		/// 列表分隔用","
		/// 特殊字符集:{(),;}
		ApqJS.Shell = function(str, ESCAPE) {
			var Sentences = ApqJS.Shell.parse(str, ESCAPE);
			return ApqJS.Shell.exec(Sentences);
		};

		/// 默认转义字符
		ApqJS.Shell.ESCAPE = '\\';
		ApqJS.Shell.SpecialChars = ['(', ')', ',', ';']; // 所有特殊字符合集

		/// 解析
		/// <return>中间结果</return>
		ApqJS.Shell.parse = function(str, ESCAPE) {
			if (!str.length) {
				return 0;
			}

			ESCAPE = ESCAPE || ApqJS.Shell.ESCAPE;

			var Sentences = [];
			var state = {
				"index": 0	// 当前扫描位置
			};
			while (state.index < str.length) {
				var Chars = ApqJS.Shell.Sentence.getChars(str, state, ESCAPE);
				state.index++;
				var Sentence = new ApqJS.Shell.Sentence();
				Sentence.Load(Chars, ESCAPE);
				Sentences.push(Sentence);
			}

			return Sentences;
		};

		/// 语句
		ApqJS.Shell.Sentence = ApqJS.Class("ApqJS.Shell.Sentence");
		ApqJS.Shell.Sentence.prototype.ctor = function() {
			this.cmd = "";
			this.Params = [];
		};

		ApqJS.Shell.Sentence.Spacer = [';'];

		ApqJS.Shell.Sentence.getChars = function(str, state, ESCAPE) {
			var Chars = [];
			while (state.index < str.length) {
				var c = ApqJS.Shell.Char.getChar(str, state, ESCAPE);
				state.index++;
				if (c.type == 2) {
					if (ApqJS.Array.Contains(ApqJS.Shell.Sentence.Spacer, c.value)) {
						state.index--;
						break;
					}
				}
				Chars.push(c);
			}
			return Chars;
		};

		ApqJS.Shell.Sentence.prototype.Load = function(Chars) {
			var cmds = [];
			var i = 0;
			for (; i < Chars.length; i++) {
				if (Chars[i].type == 2) {
					if (Chars[i].value == "(") break;
					throw new Error(-1, "命令中不能含有特殊字符!");
				}
				cmds.push(Chars[i].value);
			}
			this.cmd = ApqJS.String.Trim(cmds.join("")).toString();

			this.Params = [];
			var Param = [];
			for (; i < Chars.length; i++) {
				if (Chars[i].type == 2) {
					if (Chars[i].value == ")") {
						this.Params.push(Param.join(""));
						break;
					}
					switch (Chars[i].value) {
						case "(":
							Param = [];
							continue;
						case ",":
							this.Params.push(Param.join(""));
							Param = [];
							continue;
						default:
							ApqJS.Debug.writeln("不期望的特殊字符:" + Chars[i].value);
							break;
					}
				}
				Param.push(Chars[i].value);
			}
		};

		/// 字符
		ApqJS.Shell.Char = ApqJS.Class("ApqJS.Shell.Char");
		ApqJS.Shell.Char.prototype.ctor = function() {
			this.value = "";
			this.type = 0; // 类型{0:未知,1:普通,2:特殊}
		};

		/// 获取指定位置的字符[若为转义符自动取下一位置]
		/// state = { 
		///		index: 0 // 当前位置
		/// }
		ApqJS.Shell.Char.getChar = function(str, state, ESCAPE) {
			if (state.index >= str.length) {
				return null;
			}
			var c = new ApqJS.Shell.Char();
			c.value = String.fromCharCode(str.charCodeAt(state.index));
			if (c.value == ESCAPE) {
				try {
					c.value = ApqJS.Shell.Char.Transform(String.fromCharCode(str.charCodeAt(++state.index)));
				} catch (e) { }
				c.type = 1;
			}
			else {
				c.type = ApqJS.Array.Contains(ApqJS.Shell.SpecialChars, c.value) ? 2 : 1;
			}
			return c;
		};

		/// 可以在此增加转义字符的定义
		ApqJS.Shell.Char.Transform = function(c) {
			/* 例如:
			if( c == 'n' )
			{
			return '\n';
			}
			if( c == 'v' )
			{
			return '\v';
			}
			*/
			return c;
		};

		/// 执行分析结果
		ApqJS.Shell.exec = function(ary) {
			var r = null;
			for (var i = 0; i < ary.length; i++) {
				if (!ary[i].cmd) {
					ApqJS.Debug.writeln("未指定方法,已跳过.");
					continue;
				}
				try {
					var fn = ApqJS.$o(ary[i].cmd);
					var lp = ary[i].cmd.lastIndexOf(".");
					var o = window;
					if (lp > 0) {
						o = ApqJS.$o(ary[i].cmd.substring(0, lp));
					}
					r = fn.apply(o, ary[i].Params);
				}
				catch (e) {
					ApqJS.Debug.writeln(e.message);
				}
			}
			return r;
		};
	}
}