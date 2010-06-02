ApqJS.using("ApqJS.Html");
ApqJS.namespace("ApqJS.Web.UI.WebControls");

if (!ApqJS.Web.UI.WebControls.TextEdit) {
	ApqJS.using("ApqJS.Web.UI.WebControls.ScrollTable");
	ApqJS.using("ApqJS.Xml");

	/// ApqJS.Web.UI.WebControls.TextEdit -------------------------------------------------------------------------------------------------------
	ApqJS.Web.UI.WebControls.TextEdit = ApqJS.Class("ApqJS.Web.UI.WebControls.TextEdit", [ApqJS.Html.WebControl]);
	ApqJS.Web.UI.WebControls.TextEdit.prototype.ctor = function() {
		this.base();

		this.Popup = window.createPopup();
		this.Popup.document.body.style.margin = 1;
		this.Popup.document.body.style.backgroundColor = "black";
		this.div = ApqJS.document.CreateElement("div", this.Popup.document); this.Popup.document.body.appendChild(this.div);
		this.div.style.backgroundColor = "white";
		this.div.style.overflow = "auto";
		this.div.style.cursor = "default";
		this.table = ApqJS.document.CreateElement("table", this.Popup.document); this.div.appendChild(this.table);
		this.table.style.width = "100%";
		this.div.attachEvent("onselectstart", ApqJS.Event.ReturnFalse);
		this.div.attachEvent("oncontextmenu", ApqJS.Event.ReturnFalse);

		///#region Events
		this.Events.DataSourceChanged = ApqJS.Event();
		this.Events.XNHeadChanged = ApqJS.Event();
		this.Events.XNFootChanged = ApqJS.Event();

		this.Events.SelectedIndexChanging = ApqJS.Event();
		this.Events.SelectedIndexChanged = ApqJS.Event();
		this.Events.MouseIndexChanging = ApqJS.Event();
		this.Events.MouseIndexChanged = ApqJS.Event();
		///#endregion Events

		this._XNHead = null; // 只包含列标题
		this.XNTable = null; this._DataSource = null;
		this._XNFoot = null; // tFoot
		this.ValueMember = null;
		this.TextMember = null;
		this.LockCols = 0;

		this._SelectedIndex = -1;
		this._MouseIndex = -1; // 鼠标在this.table.rows中的索引位置
		this._SelectedText = "";
		this._Text = "";

		this.RowBgColor = ApqJS.Web.UI.WebControls.TextEdit.RowBgColor;
		this.RowColor = ApqJS.Web.UI.WebControls.TextEdit.RowColor;
		this.SelectedRowBgColor = ApqJS.Web.UI.WebControls.TextEdit.SelectedRowBgColor;
		this.SelectedRowColor = ApqJS.Web.UI.WebControls.TextEdit.SelectedRowColor;
		this.TextSplitor = ApqJS.Web.UI.WebControls.TextEdit.TextSplitor;

		this.AddRow = function() { };
		this.RemoveRow = function() { };
		this.Clear = function() { };
		this.AddColumn = function() { };
	};

	ApqJS.Web.UI.WebControls.TextEdit.Height = 150;
	ApqJS.Web.UI.WebControls.TextEdit.RowBgColor = "transparent";
	ApqJS.Web.UI.WebControls.TextEdit.RowColor = "";
	ApqJS.Web.UI.WebControls.TextEdit.SelectedRowBgColor = "midnightblue";
	ApqJS.Web.UI.WebControls.TextEdit.SelectedRowColor = "white";
	ApqJS.Web.UI.WebControls.TextEdit.TextSplitor = "|";

	ApqJS.Web.UI.WebControls.TextEdit.prototype._Attach = function(input_text, Height) {
		var me = this;

		this.div.style.posHeight = Height || ApqJS.Web.UI.WebControls.TextEdit.Height;
		this.input_text = input_text || this.input_text;

		this.input_text.attachEvent("onchange", function(e) {
			if (me._SelectedText != e.srcElement.value) {
				me._Text = e.srcElement.value; // 不需要引发事件
				me.SelectedText_set(me._Text);
			}
		});
		this.input_text.attachEvent("onclick", function(e) {
			me.Show();
		});
		this.input_text.attachEvent("onkeyup", function(e) {
			if (e.keyCode == 40)	// ↓
			{
				me.Show();
			}
			if (e.keyCode == 27)	// Esc
			{
				me.Popup.hide();
			}
		});
	};

	ApqJS.Web.UI.WebControls.TextEdit.prototype.Init = function() {
		var me = this;

		this.div.style.posWidth = this.input_text.offsetWidth - 2; // 重要语句
		this.table.attachEvent("onmouseup", function(e) { me.table_onmouseup(e) });
		this.table.attachEvent("onreadystatechange", function(e) {
			me._Init(); // 待修改
		});

		if (this._DataSource) {
			this.DataBind();
		}
		else {
			this.IsBinding = true; // 虚拟为"绑定中"
			this._Init();
		}
	};

	ApqJS.Web.UI.WebControls.TextEdit.prototype.DataBind = function() {
		var me = this;

		if (this._DataSource) {
			var dataSrc = this.table.dataSrc;
			this.table.dataSrc = "";

			this.IsBinding = true; // 设定"绑定中"
			if (this.table.rows.length > (this.table.tHead ? this.table.tHead.rows.length : 0) + (this.table.tFoot ? this.table.tFoot.rows.length : 0)) {
				this.table.dataSrc = dataSrc;
			}
			else {
				var A_XN = new ApqJS.Xml.XmlNode(this._DataSource);
				this.XNTable = new ApqJS.Xml.XNTable(A_XN.ToDSO(this.Popup.document).documentElement);
				var DisplayMember = this.DisplayMember || this.XNTable.GetColumns();
				var tBody;
				if (this.table.tBodies.length) {
					tBody = this.table.tBodies[this.table.tBodies.length - 1];
				}
				else {
					tBody = ApqJS.document.CreateElement("tbody", this.Popup.document); this.table.appendChild(tBody);
				}
				var tr = tBody.insertRow();
				for (var i = 0; i < DisplayMember.length; i++) {
					var td = tr.insertCell();
					if (i == 0 || i == 1) {
						td.style.borderTopWidth = td.style.borderBottomWidth = 1;
						td.style.borderLeftWidth = 0;
						td.style.borderRightWidth = 1;
						td.style.borderStyle = "solid";
						td.style.borderColor = "gray";
					}
					else {
						td.style.borderWidth = 1;
						td.style.borderStyle = "solid";
						td.style.borderColor = "gray";
					}
					var div = ApqJS.document.CreateElement("div", this.Popup.document); td.appendChild(div);
					div.dataFld = DisplayMember[i];
				}
				this.table.dataSrc = "#" + xml.id;
			}
		}
	};

	ApqJS.Web.UI.WebControls.TextEdit.prototype._Init = function() {
		var me = this;

		if (this.table.readyState == "complete" && this.IsBinding) {
			this.IsBinding = false; // 取消"绑定中"

			for (var i = this.table.tHead ? this.table.tHead.rows.length : 0; i < this.table.rows.length - (this.table.tFoot ? this.table.tFoot.rows.length : 0); i++) {
				var tr = this.table.rows[i];
				tr.attachEvent("onmouseover", function(e) {
					me.MouseIndex_set(ApqJS.Web.UI.WebControls.Table.GetRowIndex(e.srcElement));
				});
			}

			if (this.table.tHead || this.LockCols) {
				var st = new ApqJS.Web.UI.WebControls.ScrollTable();
				st.Attach(this.table, (this.table.tHead ? this.table.tHead.rows.length : 0), this.LockCols);
				st.Init();
			}

			this.IsInited = true;
			this.Events["Inited"].Fire(this, null);
		}
	};

	ApqJS.Web.UI.WebControls.TextEdit.prototype._Show = function() {
		if (!this.Popup.isOpen) {
			this.MouseIndex_set(this._SelectedIndex + (this.table.tHead ? this.table.tHead.rows.length : 0));
			this.Popup.show(0, this.input_text.offsetHeight, this.input_text.offsetWidth, this.div.style.posHeight + 2, this.input_text);
		}
	};

	ApqJS.Web.UI.WebControls.TextEdit.prototype._Render = function() {
		return this.div.outerHTML;
	};

	///#region 属性
	ApqJS.Object.pAdd(ApqJS.Web.UI.WebControls.TextEdit.prototype, "DataSource", 3);
	ApqJS.Object.pAdd(ApqJS.Web.UI.WebControls.TextEdit.prototype, "XNHead", 1);
	ApqJS.Web.UI.WebControls.TextEdit.prototype.XNHead_set = function(value) {
		if (value == this._XNHead) {
			return;
		}

		ApqJS.Object.Set(this, "_XNHead", value);

		if (this.table.tHead) {
			this.table.tHead.removeNode(true);
		}
		this.table.createTHead();
		for (var r = 0; r < this._XNHead.childNodes.length; r++) {
			var tr = this.table.tHead.insertRow();
			tr.style.backgroundColor = "menu";
			var xnRow = this._XNHead.childNodes[r];
			for (var d = 0; d < xnRow.childNodes.length; d++) {
				var td = tr.insertCell();
				td.style.borderBottomStyle = td.style.borderRightStyle = "solid";
				td.style.borderBottomWidth = td.style.borderRightWidth = 1;
				td.style.borderBottomColor = td.style.borderRightColor = "gray";
				td.innerText = xnRow.childNodes[d].text;
			}
		}
	};
	ApqJS.Object.pAdd(ApqJS.Web.UI.WebControls.TextEdit.prototype, "XNFoot", 1);
	ApqJS.Web.UI.WebControls.TextEdit.prototype.XNFoot_set = function(value) {
		if (value == this._XNFoot) {
			return;
		}

		ApqJS.Object.Set(this, "_XNFoot", value);

		if (this.table.tFoot) {
			this.table.tFoot.removeNode(true);
		}
		this.table.createTFoot();
		for (var r = 0; r < this._XNFoot.childNodes.length; r++) {
			var tr = this.table.tFoot.insertRow();
			tr.style.backgroundColor = "menu";
			var xnRow = this._XNFoot.childNodes[r];
			for (var d = 0; d < xnRow.childNodes.length; d++) {
				var td = tr.insertCell();
				td.style.borderBottomStyle = td.style.borderRightStyle = "solid";
				td.style.borderBottomWidth = td.style.borderRightWidth = 1;
				td.style.borderBottomColor = td.style.borderRightColor = "gray";
				td.innerText = xnRow.childNodes[d].text;
			}
		}
	};

	ApqJS.Object.pAdd(ApqJS.Web.UI.WebControls.TextEdit.prototype, "SelectedIndex", 1);
	ApqJS.Web.UI.WebControls.TextEdit.prototype.SelectedIndex_set = function(value) {
		if (value == this._SelectedIndex) {
			return;
		}
		ApqJS.Object.Set(this, "_SelectedIndex", value);
		// 增加设置 SelectedValue, SelectedText(直接设置)
	};
	ApqJS.Object.pAdd(ApqJS.Web.UI.WebControls.TextEdit.prototype, "SelectedValue", 1);
	ApqJS.Web.UI.WebControls.TextEdit.prototype.SelectedValue_set = function(value) {
		// 查找出索引位置,用SelectedIndex_set来设置,然后引发属性已更改事件
	};
	ApqJS.Object.pAdd(ApqJS.Web.UI.WebControls.TextEdit.prototype, "SelectedText", 1);
	ApqJS.Web.UI.WebControls.TextEdit.prototype.SelectedText_set = function(value) {
		var index = this.XNTable.IndexOf(value.split(this.TextSplitor), this.TextMember);
		if (index >= 0) {
			this.SelectedIndex_set(index);
		}
	};

	ApqJS.Object.pAdd(ApqJS.Web.UI.WebControls.TextEdit.prototype, "MouseIndex", 3);
	///#endregion 属性

	///#region 默认事件处理
	ApqJS.Function.Create(ApqJS.Web.UI.WebControls.TextEdit.prototype, "OnSelectedIndexChanged",
		function(e) {
			this.MouseIndex_set(this._SelectedIndex + (this.table.tHead ? this.table.tHead.rows.length : 0));
			this.SelectedValue = this.XNTable.GetRow(this._SelectedIndex, this.ValueMember);
			this._SelectedText = this.XNTable.GetRow(this._SelectedIndex, this.TextMember).join(this.TextSplitor);
			this.input_text_value_set(this.SelectedText); // 待修改

			ApqJS.Object.fireEvent(this, "SelectedIndexChanged", e);
		}
		, true);

	ApqJS.Function.Create(ApqJS.Web.UI.WebControls.TextEdit.prototype, "OnMouseIndexChanging",
		function(e) {
			if (e.Original == e.ProposedValue) {
				return;
			}
			if (e.ProposedValue >= (this.table.tHead ? this.table.tHead.rows.length : 0)) {
				if (e.Original >= (this.table.tHead ? this.table.tHead.rows.length : 0)) {
					this.table.rows[e.Original].style.backgroundColor = this.RowBgColor;
					this.table.rows[e.Original].style.color = this.RowColor;
				}
				this.table.rows[e.ProposedValue].style.backgroundColor = this.SelectedRowBgColor;
				this.table.rows[e.ProposedValue].style.color = this.SelectedRowColor;
			}
			else {
				e.cancel = true;
				return;
			}

			ApqJS.Object.fireEvent(this, "MouseIndexChanging", e);
		}
		, true);

	ApqJS.Web.UI.WebControls.TextEdit.prototype.table_onmouseup = function(e) {
		if (e.button == 1) {
			this.SelectedIndex_set(ApqJS.Web.UI.WebControls.Table.GetRowIndex(e.srcElement) - (this.table.tHead ? this.table.tHead.rows.length : 0));
			this.Popup.hide();
		}
	};
	///#endregion 默认事件处理

	///#region internal
	ApqJS.Web.UI.WebControls.TextEdit.prototype.input_text_value_set = function(value) {
		if (this.input_text.value != value) {
			this.input_text.value = value;
			this.input_text.fireEvent("onchange");
		}
	};
	///#endregion internal

	/// ApqJS.Web.UI.WebControls.TextArea -------------------------------------------------------------------------------------------------------
	ApqJS.Web.UI.WebControls.TextArea = ApqJS.Class("ApqJS.Web.UI.WebControls.TextArea", [ApqJS.Html.WebControl]);
	ApqJS.Web.UI.WebControls.TextArea.prototype.ctor = function() {
		this.base();
	};

	ApqJS.Web.UI.WebControls.TextArea.prototype._Attach = function() {

	};

	ApqJS.Web.UI.WebControls.TextArea.prototype._Init = function() {

	};

	ApqJS.Web.UI.WebControls.TextArea.prototype._Show = function() {

	};

	ApqJS.Web.UI.WebControls.TextArea.prototype._Render = function() {

	};

	ApqJS.Web.UI.WebControls.TextArea.AutoHeight = function(TextArea, MaxHeight) {
		TextArea.attachEvent("onpropertychange", ApqJS.Web.UI.WebControls.TextArea._AutoHeight);
		TextArea.MaxHeight = MaxHeight;
		TextArea.minHeight = 60;
	};

	ApqJS.Web.UI.WebControls.TextArea._AutoHeight = function(e) {
		e.srcElement.style.height = (e.srcElement.scrollHeight > e.srcElement.MaxHeight ? e.srcElement.MaxHeight : Math.max(e.srcElement.minHeight, e.srcElement.scrollHeight)) +
			(e.srcElement.offsetHeight - e.srcElement.clientHeight);
	};
}
