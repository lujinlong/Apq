ApqJS.using("ApqJS.Web.UI.WebControls.ScrollTable");

if (!ApqJS.Web.UI.WebControls.TextEdit) {
	ApqJS.using("ApqJS.Xml");

	/// ApqJS.Web.UI.WebControls.TextEdit -------------------------------------------------------------------------------------------------------
	ApqJS.Web.UI.WebControls.TextEdit = ApqJS.Class("ApqJS.Web.UI.WebControls.TextEdit", [ApqJS.Html.WebControl]);
	ApqJS.Web.UI.WebControls.TextEdit.prototype.ctor = function() {
		this.base();

		this.Popup = window.createPopup();
		this.div = ApqJS.document.CreateElement("div", this.Popup.document); this.Popup.document.body.appendChild(this.div);
		this.table = ApqJS.document.CreateElement("table", this.Popup.document); this.div.appendChild(this.table);
		this.table.attachEvent("onselectstart", ApqJS.Event.ReturnFalse);
		this.table.attachEvent("oncontextmenu", ApqJS.Event.ReturnFalse);

		///#region Events
		this.Events.DataSourceChanged = ApqJS.Event();
		this.Events.HeadXNChanged = ApqJS.Event();
		this.Events.FootXNChanged = ApqJS.Event();

		this.Events.SelectedIndexChanging = ApqJS.Event();
		this.Events.SelectedIndexChanged = ApqJS.Event();
		this.Events.MouseIndexChanging = ApqJS.Event();
		this.Events.MouseIndexChanged = ApqJS.Event();

		this.Events.SelectedIndexChanged.add(this._SelectedIndexChanged, this);
		this.Events.MouseIndexChanging.add(this._MouseIndexChanging, this);
		///#endregion Events

		this.DataSource = null;
		this.XmlTable = null;
		this.HeadXN = null; // 只包含列标题
		this.FootXN = null; // tFoot
		this.ValueMember = null;
		this.TextMember = null;
		this.LockCols = 0;
		this.ValueSplitor = ApqJS.Web.UI.WebControls.TextEdit.ValueSplitor;
		this.TextSplitor = ApqJS.Web.UI.WebControls.TextEdit.TextSplitor;

		this.SelectedIndex = -1;
		this.MouseIndex = -1; // 鼠标在this.table.rows中的索引位置
		this.SelectedText = "";
		this.Text = "";

		this.RowBgColor = ApqJS.Web.UI.WebControls.TextEdit.RowBgColor;
		this.RowColor = ApqJS.Web.UI.WebControls.TextEdit.RowColor;
		this.SelectedRowBgColor = ApqJS.Web.UI.WebControls.TextEdit.SelectedRowBgColor;
		this.SelectedRowColor = ApqJS.Web.UI.WebControls.TextEdit.SelectedRowColor;

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
	ApqJS.Web.UI.WebControls.TextEdit.ValueSplitor = "|";
	ApqJS.Web.UI.WebControls.TextEdit.TextSplitor = "|";

	ApqJS.Web.UI.WebControls.TextEdit.prototype._Attach = function(input$text, Height) {
		var me = this;

		this.div.style.posHeight = Height || ApqJS.Web.UI.WebControls.TextEdit.Height;

		this.input$text = input$text || this.input$text;
		this.input$text.attachEvent("onchange", function(e) {
			if (me.SelectedText != this.Text) {
				me.Text = e.srcElement.value;
			}
		});
		this.input$text.attachEvent("onclick", function(e) {
			me.Show();
		});
		this.input$text.attachEvent("onkeyup", function(e) {
			if (event.keyCode == 40) {
				me.Show();
			}
			if (event.keyCode == 27)	// Esc
			{
				me.Popup.hide();
			}
		});
	};

	ApqJS.Web.UI.WebControls.TextEdit.prototype.Init = function() {
		var me = this;

		this.Popup.document.body.style.margin = 1;
		this.Popup.document.body.style.backgroundColor = "black";
		this.div.style.posWidth = this.input$text.offsetWidth - 2; // 重要语句
		this.div.style.backgroundColor = "white";
		this.div.style.overflow = "auto";
		this.div.style.cursor = "default";
		this.table.style.width = "100%";
		this.table.style.borderCollapse = "collapse";
		this.table.attachEvent("onmouseup", function(e) { me.table_onmouseup(e) });
		this.table.attachEvent("onreadystatechange", function(e) {
			me._Init();
		});

		if (this.DataSource) {
			this.DataBind();
		}
		else {
			this.IsBinding = true; // 虚拟为"绑定中"
			this._Init();
		}
	};

	ApqJS.Web.UI.WebControls.TextEdit.prototype.DataBind = function() {
		var me = this;

		if (this.DataSource) {
			var dataSrc = this.table.dataSrc;
			this.table.dataSrc = "";

			this.IsBinding = true; // 设定"绑定中"
			if (this.table.rows.length > (this.table.tHead ? this.table.tHead.rows.length : 0) + (this.table.tFoot ? this.table.tFoot.rows.length : 0)) {
				this.table.dataSrc = dataSrc;
			}
			else {
				var xml = ApqJS.document.CreateElement("xml", this.Popup.document); this.div.insertAdjacentElement("beforeBegin", xml);
				xml.async = false;
				xml.loadXML(this.DataSource.xml);

				var A_XN = new ApqJS.Xml.XmlNode(this.DataSource);
				this.XmlTable = new ApqJS.Xml.XmlTable(A_XN.ToDSO(this.Popup.document).documentElement);
				var DisplayMember = this.DisplayMember || this.XmlTable.GetColumns();
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
					td.style.borderBottomStyle = td.style.borderRightStyle = "solid";
					td.style.borderBottomWidth = td.style.borderRightWidth = 1;
					td.style.borderBottomColor = td.style.borderRightColor = "gray";
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
				var st = new ApqJS.Web.UI.WebControls.ScrollTable(this.table, this.table.tHead.rows.length, this.LockCols);
				st.Init();
			}

			this.IsInited = true;
			this.Events["OnInit"].Fire(this, null);
		}
	};

	ApqJS.Web.UI.WebControls.TextEdit.prototype._Show = function() {
		if (!this.Popup.isOpen) {
			this.MouseIndex_set(this.SelectedIndex + (this.table.tHead ? this.table.tHead.rows.length : 0));
			this.Popup.show(0, this.input$text.offsetHeight, this.input$text.offsetWidth, this.div.style.posHeight + 2, this.input$text);
		}
	};

	ApqJS.Web.UI.WebControls.TextEdit.prototype._Render = function() {
		return this.div.outerHTML;
	};

	///#region 属性
	ApqJS.Object.pAdd(ApqJS.Web.UI.WebControls.TextEdit.prototype, "DataSource", 3);
	ApqJS.Object.pAdd(ApqJS.Web.UI.WebControls.TextEdit.prototype, "HeadXN", 1);
	ApqJS.Web.UI.WebControls.TextEdit.prototype.HeadXN_set = function(value) {
		if (value == this.HeadXN) {
			return;
		}

		ApqJS.Object.Set(this, "HeadXN", value);

		if (this.table.tHead) {
			this.table.tHead.removeNode(true);
		}
		this.table.createTHead();
		for (var r = 0; r < this.HeadXN.childNodes.length; r++) {
			var tr = this.table.tHead.insertRow();
			tr.style.backgroundColor = "menu";
			var xnRow = this.HeadXN.childNodes[r];
			for (var d = 0; d < xnRow.childNodes.length; d++) {
				var td = tr.insertCell();
				td.style.borderBottomStyle = td.style.borderRightStyle = "solid";
				td.style.borderBottomWidth = td.style.borderRightWidth = 1;
				td.style.borderBottomColor = td.style.borderRightColor = "gray";
				td.innerText = xnRow.childNodes[d].text;
			}
		}
	};
	ApqJS.Object.pAdd(ApqJS.Web.UI.WebControls.TextEdit.prototype, "FootXN", 1);
	ApqJS.Web.UI.WebControls.TextEdit.prototype.FootXN_set = function(value) {
		if (value == this.FootXN) {
			return;
		}

		ApqJS.Object.Set(this, "FootXN", value);

		if (this.table.tFoot) {
			this.table.tFoot.removeNode(true);
		}
		this.table.createTFoot();
		for (var r = 0; r < this.FootXN.childNodes.length; r++) {
			var tr = this.table.tFoot.insertRow();
			tr.style.backgroundColor = "menu";
			var xnRow = this.FootXN.childNodes[r];
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
		if (value == this.SelectedIndex) {
			return;
		}
		ApqJS.Object.Set(this, "SelectedIndex", value);
		// 增加设置 SelectedValue, SelectedText(直接设置)
	};
	ApqJS.Object.pAdd(ApqJS.Web.UI.WebControls.TextEdit.prototype, "SelectedValue", 1);
	ApqJS.Web.UI.WebControls.TextEdit.prototype.SelectedValue_set = function(value) {
		// 查找出索引位置,用SelectedIndex_set来设置,然后引发属性也更改事件
	};
	ApqJS.Object.pAdd(ApqJS.Web.UI.WebControls.TextEdit.prototype, "SelectedText", 1);
	ApqJS.Web.UI.WebControls.TextEdit.prototype.SelectedText_set = function(value) {
		// 查找出索引位置,用SelectedIndex_set来设置,然后引发属性也更改事件
	};

	ApqJS.Object.pAdd(ApqJS.Web.UI.WebControls.TextEdit.prototype, "MouseIndex", 3);
	///#endregion 属性

	///#region 默认事件处理
	ApqJS.Web.UI.WebControls.TextEdit.prototype._SelectedIndexChanged = function(sender, e) {
		this.MouseIndex_set(this.SelectedIndex + (this.table.tHead ? this.table.tHead.rows.length : 0));
		this.input$text_value_set(this.SelectedIndex); // 待修改
	};

	ApqJS.Web.UI.WebControls.TextEdit.prototype._MouseIndexChanging = function(sender, e) {
		if (e.Original == e.ProposedValue) {
			return;
		}
		if (e.Original >= (this.table.tHead ? this.table.tHead.rows.length : 0)) {
			this.table.rows[e.Original].style.backgroundColor = this.RowBgColor;
			this.table.rows[e.Original].style.color = this.RowColor;
		}
		if (e.ProposedValue >= (this.table.tHead ? this.table.tHead.rows.length : 0)) {
			this.table.rows[e.ProposedValue].style.backgroundColor = this.SelectedRowBgColor;
			this.table.rows[e.ProposedValue].style.color = this.SelectedRowColor;
		}
	};

	ApqJS.Web.UI.WebControls.TextEdit.prototype.table_onmouseup = function(e) {
		if (e.button == 1) {
			this.SelectedIndex_set(ApqJS.Web.UI.WebControls.Table.GetRowIndex(e.srcElement) - (this.table.tHead ? this.table.tHead.rows.length : 0));
			this.Popup.hide();
		}
	};
	///#endregion 默认事件处理

	///#region internal
	ApqJS.Web.UI.WebControls.TextEdit.prototype.input$text_value_set = function(value) {
		if (this.input$text.value != value) {
			this.input$text.value = value;
			this.input$text.fireEvent("onchange");
		}
	};
	///#endregion internal

	ApqJS.Web.UI.WebControls.TextEdit.AutoHeight = function(TextArea, MaxHeight) {
		TextArea.attachEvent("onpropertychange", ApqJS.Web.UI.WebControls.TextEdit._AutoHeight);
		TextArea.MaxHeight = MaxHeight;
		TextArea.minHeight = 60;
	};

	ApqJS.Web.UI.WebControls.TextEdit._AutoHeight = function(e) {
		e.srcElement.style.height = (e.srcElement.scrollHeight > e.srcElement.MaxHeight ? e.srcElement.MaxHeight : Math.max(e.srcElement.minHeight, e.srcElement.scrollHeight)) +
			(e.srcElement.offsetHeight - e.srcElement.clientHeight);
	};
}
