ApqJS.namespace("ApqJS.Text");

if (!ApqJS.Text.UTF8Encoding) {
	/// ApqJS.Text.UTF8Encoding ------------------------------------------------------------------------------------------------------------------------
	ApqJS.Text.UTF8Encoding = {
		/// 编码
		/// 将字符串使用UTF8编码表示
		GetBytes: function(str, index, length, Bytes, bindex) {
			index = index || 0;
			length = length || str.length;
			Bytes = Bytes || [];
			bindex = bindex || 0;

			for (var i = index; i < length; i++) {
				var n = str.charCodeAt(i);
				if (n > 0 && n <= 0x007F) {
					Bytes[bindex++] = n;
				}
				else if (n > 0x07FF) {
					Bytes[bindex++] = 0xE0 | ((n >> 12) & 0x0F);
					Bytes[bindex++] = 0x80 | ((n >> 6) & 0x3F);
					Bytes[bindex++] = 0x80 | ((n >> 0) & 0x3F);
				}
				else {
					Bytes[bindex++] = 0xC0 | ((n >> 6) & 0x1F);
					Bytes[bindex++] = 0x80 | ((n >> 0) & 0x3F);
				}
			}
			return Bytes;
		},
		/// 解码
		/// <Bytes>UTF8字节数组</Bytes>
		GetString: function(Bytes, index, length) {
			index = index || 0;
			length = length || Bytes.length;

			var str = "";
			var i = 0;
			while (i < length) {
				var n = Bytes[i++];
				switch (n >>> 4) {
					// 0xxx xxxx 
					case 0:
					case 1:
					case 2:
					case 3:
					case 4:
					case 5:
					case 6:
					case 7:
						str += String.fromCharCode(n);
						break;

					// 110x xxxx    10xx xxxx 
					case 12:
					case 13:
						var n2 = Bytes[i++];
						str += String.fromCharCode(((n & 0x1F) << 6) | (n2 & 0x3F));
						break;

					// 1110 xxxx    10xx xxxx    10xx xxxx 
					case 14:
						var n2 = Bytes[i++];
						var n3 = Bytes[i++];
						str += String.fromCharCode(((n & 0x0F) << 12) | ((n2 & 0x3F) << 6) | (n3 & 0x3F));
						break;
				}
			}
			return str;
		}
	};

	ApqJS.Text.Base64Encoding = {
		strEncode: "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/",
		/// 将字符转为对应的Base64序号('='对应-1)
		getEncode: function(c) {
			var n = c.charCodeAt(0);
			if (n >= 65 && n <= 90) {
				return n - 65;
			}
			else if (n >= 97 && n <= 122) {
				return n - 71; // n - 97 + 26
			}
			else if (n >= 48 && n <= 57) {
				return n + 4; // n - 48 + 52
			}
			else if (c == '+') {
				return 62;
			}
			else if (c == '/') {
				return 63;
			}
			else {
				return -1;
			}
		},

		/// 解码
		/// <str>Base64字符串</str>
		GetBytes: function(str, index, length, Bytes, bindex) {
			index = index || 0;
			length = length || str.length;
			Bytes = Bytes || new Array();
			bindex = bindex || 0;

			var i = 0;
			while (i < length) {
				var cary = new Array();
				for (var j = 0; j < 4; j++) {
					cary.push(this.getEncode(str.charAt(index + i++)));
				}

				Bytes[bindex++] = (cary[0] << 2) | (cary[1] >>> 4);
				var n = ((cary[1] << 4) | (cary[2] >>> 2)) & 0xFF;
				if (n != 0xF) {
					Bytes[bindex++] = n;
					n = ((cary[2] << 6) | (cary[3] & 0x3F)) & 0xFF;
					if (n != 0x3F) {
						Bytes[bindex++] = n;
					}
				}
			}
			return Bytes;
		},
		/// 编码
		/// <Bytes>UFT8数组</Bytes>
		GetString: function(Bytes, index, length) {
			index = index || 0;
			length = length || Bytes.length;

			var ary = new Array();

			var nRest = length % 3;
			var i = 0; // 位置指针
			while (i < length - nRest) {
				var bary = new Array();
				for (var j = 0; j < 3; j++) {
					bary.push(Bytes[index + i + j]);
				}
				var n = i / 3 * 4;
				ary[n] = this.strEncode.charAt(bary[0] >>> 2);
				ary[n + 1] = this.strEncode.charAt(((bary[0] << 4) | (bary[1] >>> 4)) & 0x3F);
				ary[n + 2] = this.strEncode.charAt(((bary[1] << 2) | (bary[2] >>> 6)) & 0x3F);
				ary[n + 3] = this.strEncode.charAt(bary[2] & 0x3F);
				i += 3;
			}

			if (nRest) {
				var bary = new Array();
				for (var j = 0; j < nRest; j++) {
					bary.push(Bytes[index + i + j]);
				}
				bary.push(0);

				var n = i / 3 * 4;
				ary[n] = this.strEncode.charAt(bary[0] >>> 2);
				ary[n + 1] = this.strEncode.charAt(((bary[0] << 4) | (bary[1] >>> 4)) & 0x3F);
				if (nRest == 2) {
					ary[n + 2] = this.strEncode.charAt((bary[1] << 2) & 0x3F);
				}

				for (var j = 0; j < 3 - nRest; j++) {
					ary.push("=");
				}
			}

			return ary.join("");
		}
	};
}
