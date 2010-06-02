ApqJS.namespace("ApqJS.Xml");

if (!ApqJS.Xml.XmlDocument) {
	/// ApqJS.Xml.XmlDocument -------------------------------------------------------------------------------------------------------------------
	ApqJS.Xml.XmlDocument = ApqJS.Class("ApqJS.Xml.XmlDocument");
	ApqJS.Xml.XmlDocument.prototype.ctor = function(Xd) {
		this._Xd = Xd;

		this.Events = {
			"XdChanging": ApqJS.Event(),
			"XdChanged": ApqJS.Event()
		};
	};

	ApqJS.Object.pAdd(ApqJS.Xml.XmlDocument.prototype, "Xd", 3, true);

	/// ApqJS.Xml.XmlNode -----------------------------------------------------------------------------------------------------------------------
	ApqJS.Xml.XmlNode = ApqJS.Class("ApqJS.Xml.XmlNode");
	ApqJS.Xml.XmlNode.prototype.ctor = function(Xn) {
		ApqJS.Argument.CheckNull("Xn", Xn);

		this._Xn = Xn;
		this.Excludes = ["Events", "_Xn", "Excludes"];

		this.Events = {
			"XnChanging": ApqJS.Event(),
			"XnChanged": ApqJS.Event()
		};
	};

	ApqJS.Object.pAdd(ApqJS.Xml.XmlNode.prototype, "Xn", 3, true);

	/// <Excludes>需要排除的属性名列表</Excludes>
	ApqJS.Xml.XmlNode.prototype.ReadFromNode = function(Excludes) {
		Excludes = Excludes || [];

		if (arguments.length > 1) {
			for (var i = 1; i < arguments.length; i++) {
				if (arguments[i].indexOf("__") == 0) {
					continue;
				}

				if (this[arguments[i]] != null && !this.hasOwnProperty(arguments[i])) {
					continue;
				}

				if (ApqJS.Array.Contains(this.Excludes, arguments[i]) || ApqJS.Array.Contains(Excludes, arguments[i])) {
					continue;
				}

				var xnAttr = this._Xn.attributes.getNamedItem(arguments[i]);
				if (xnAttr) {
					this[arguments[i]] = xnAttr.value;
				}
			}
		}
		else {
			for (var i = 0; i < this._Xn.attributes.length; i++) {
				if (this._Xn.attributes[i].name.indexOf("__") == 0) {
					continue;
				}

				if (this[this._Xn.attributes[i].name] != null && !this.hasOwnProperty(this._Xn.attributes[i].name)) {
					continue;
				}

				if (ApqJS.Array.Contains(this.Excludes, this._Xn.attributes[i].name) || ApqJS.Array.Contains(Excludes, this._Xn.attributes[i].name)) {
					continue;
				}

				this[this._Xn.attributes[i].name] = this._Xn.attributes[i].value;
			}
		}
	};

	ApqJS.Xml.XmlNode.prototype.ToDSO = function(doc) {
		var DSO = ApqJS.document.CreateElement("xml", doc);
		doc.appendChild(DSO);
		DSO.async = false;
		DSO.loadXML(this._Xn.xml);
		return DSO;
	};

	/// ApqJS.Xml.XNTable -----------------------------------------------------------------------------------------------------------------------
	ApqJS.Xml.XNTable = ApqJS.Class("ApqJS.Xml.XNTable", [ApqJS.Xml.XmlNode]);
	//	ApqJS.Xml.XNTable.prototype.ctor = function( _Xn ){
	//		this.base(_Xn);
	//	};

	ApqJS.Function.Create(ApqJS.Xml.XNTable.prototype, "GetColumns",
		function() {
			if (!this.Columns) {
				this.Columns = [];
				if (this._Xn.hasChildNodes()) {
					var tr0 = this._Xn.firstChild;
					for (var i = 0; i < tr0.childNodes.length; i++) {
						this.Columns.push(tr0.childNodes[i].nodeName);
					}
				}
			}
			return this.Columns;
		}
		, false);

	ApqJS.Xml.XNTable.prototype.GetRow = function(index, cols) {
		cols = cols || this.GetColumns();

		var row = [];
		var tr = this._Xn.childNodes[index];
		if (tr) {
			for (var i = 0; i < cols.length; i++) {
				row.push(tr.selectSingleNode(cols[i]).text);
			}
		}
		return row;
	};

	/// <cols>列名列表</cols>
	ApqJS.Xml.XNTable.prototype.IndexOf = function(row, cols) {
		cols = cols || this.GetColumns();

		for (var i = 0; i < this._Xn.childNodes.length; i++) {
			var tr = this._Xn.childNodes[i];
			var fnd = true;
			for (var j = 0; j < cols.length; j++) {
				fnd = tr.selectSingleNode(cols[j]).text == row[j];
				if (!fnd) {
					break;
				}
			}
			if (fnd) {
				return i;
			}
		}
		return -1;
	};
}
