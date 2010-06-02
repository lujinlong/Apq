/* 引用注意事项
* 需要使用本功能的页面不能使用以下验证
* <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
* */
ApqJS.using("ApqJS.Html");
ApqJS.namespace("ApqJS.Web.UI.WebControls");

if (!ApqJS.Web.UI.WebControls.ScrollTable) {
	ApqJS.Web.UI.WebControls.ScrollTable = ApqJS.Class("ApqJS.Web.UI.WebControls.ScrollTable", [ApqJS.Html.WebControl]);
	ApqJS.Web.UI.WebControls.ScrollTable.prototype.ctor = function() {
		this.base();

		this._BorderWidth = 1;
		this._BorderColor = "gray";
		this._BorderStyle = "solid";

		///#region Events
		this.Events.HBorderWidthChanged = ApqJS.Event();
		this.Events.VBorderWidthChanged = ApqJS.Event();
		this.Events.BorderWidthChanged = ApqJS.Event();
		this.Events.HBorderColorChanged = ApqJS.Event();
		this.Events.VBorderColorChanged = ApqJS.Event();
		this.Events.BorderColorChanged = ApqJS.Event();
		this.Events.HBorderStyleChanged = ApqJS.Event();
		this.Events.VBorderStyleChanged = ApqJS.Event();
		this.Events.BorderStyleChanged = ApqJS.Event();
		this.Events.HSBorderWidthChanged = ApqJS.Event();
		this.Events.VSBorderWidthChanged = ApqJS.Event();
		this.Events.SBorderWidthChanged = ApqJS.Event();
		this.Events.HSBorderColorChanged = ApqJS.Event();
		this.Events.VSBorderColorChanged = ApqJS.Event();
		this.Events.SBorderColorChanged = ApqJS.Event();
		this.Events.HSBorderStyleChanged = ApqJS.Event();
		this.Events.VSBorderStyleChanged = ApqJS.Event();
		this.Events.SBorderStyleChanged = ApqJS.Event();
		///#endregion Events
	};

	ApqJS.Web.UI.WebControls.ScrollTable.zIndex = 30000;

	ApqJS.Web.UI.WebControls.ScrollTable.prototype._Attach = function(table, iRows, iCols, zIndex) {
		this.table = table;
		this.Rows = iRows || 0;
		this.Cols = iCols || 0;
		this.zIndex = zIndex || ApqJS.Web.UI.WebControls.ScrollTable.zIndex;
	};

	ApqJS.Web.UI.WebControls.ScrollTable.prototype._Init = function() {
		this.table.border = 0;
		this.table.cellSpacing = 0;
		this.table.style.borderCollapse = "separate";

		for (var r = 0; r < this.Rows; r++) {
			// 列头
			ApqJS.Web.UI.WebControls.ScrollTable.SetCH(this.table.rows[r], this.zIndex);

			// 表头
			for (var d = 0; d < this.Cols; d++) {
				ApqJS.Web.UI.WebControls.ScrollTable.SetTH(this.table.rows[r].cells[d], this.zIndex + 5);
			}
		}

		// 行头(一格)
		for (var r = this.Rows; r < this.table.rows.length; r++) {
			for (var d = 0; d < this.Cols; d++) {
				ApqJS.Web.UI.WebControls.ScrollTable.SetRH(this.table.rows[r].cells[d], this.zIndex);
			}
		}

		for (var r = 0; r < this.table.rows.length; r++) {
			for (var d = 0; d < this.table.rows[r].cells.length; d++) {
				this.SetBorder(this.table.rows[r].cells[d]);
			}
		}
	};

	/// 设置列头
	ApqJS.Web.UI.WebControls.ScrollTable.SetCH = function(tr, zIndex) {
		tr.style.zIndex = zIndex || ApqJS.Web.UI.WebControls.ScrollTable.zIndex;
		tr.style.position = "relative";
		tr.style.removeExpression("top");
		tr.style.setExpression("top", "this.offsetParent.scrollTop"); // tr.offsetParent --> DIV
		for (var i = 0; i < tr.cells.length; i++) {
			tr.cells[i].style.position = "relative";
		}
	};

	/// 设置表头
	ApqJS.Web.UI.WebControls.ScrollTable.SetTH = function(td, zIndex) {
		td.style.zIndex = zIndex || ApqJS.Web.UI.WebControls.ScrollTable.zIndex + 5;
		td.style.position = "relative";
		td.style.removeExpression("left");
		td.style.setExpression("left", "this.parentElement.offsetParent.scrollLeft"); // td.parentElement.offsetParent --> DIV
	};

	/// 设置行头(一格)
	ApqJS.Web.UI.WebControls.ScrollTable.SetRH = function(td, zIndex) {
		td.style.zIndex = zIndex || ApqJS.Web.UI.WebControls.ScrollTable.zIndex;
		td.style.position = "relative";
		td.style.removeExpression("left");
		td.style.setExpression("left", "this.parentElement.offsetParent.parentElement.scrollLeft"); // td.parentElement.offsetParent --> TABLE
	};

	/// 设置一格
	ApqJS.Web.UI.WebControls.ScrollTable.prototype.SetBorder = function(td) {
		td.style.borderTopWidth = 0;
		td.style.borderBottomWidth = this.HBorderWidth_get();
		td.style.borderLeftWidth = 0;
		td.style.borderRightWidth = this.VBorderWidth_get();

		td.style.borderBottomColor = this.HBorderColor_get();
		td.style.borderRightColor = this.VBorderColor_get();

		td.style.borderBottomStyle = this.HBorderStyle_get();
		td.style.borderRightStyle = this.VBorderStyle_get();
	};

	///#region 属性
	ApqJS.Object.pAdd(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "HBorderWidth", 2);
	ApqJS.Object.pAdd(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "VBorderWidth", 2);
	ApqJS.Object.pAdd(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "BorderWidth", 3);
	ApqJS.Object.pAdd(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "HBorderColor", 2);
	ApqJS.Object.pAdd(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "VBorderColor", 2);
	ApqJS.Object.pAdd(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "BorderColor", 3);
	ApqJS.Object.pAdd(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "HBorderStyle", 2);
	ApqJS.Object.pAdd(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "VBorderStyle", 2);
	ApqJS.Object.pAdd(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "BorderStyle", 3);
	ApqJS.Object.pAdd(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "HSBorderWidth", 2);
	ApqJS.Object.pAdd(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "VSBorderWidth", 2);
	ApqJS.Object.pAdd(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "SBorderWidth", 3);
	ApqJS.Object.pAdd(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "HSBorderColor", 2);
	ApqJS.Object.pAdd(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "VSBorderColor", 2);
	ApqJS.Object.pAdd(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "SBorderColor", 3);
	ApqJS.Object.pAdd(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "HSBorderStyle", 2);
	ApqJS.Object.pAdd(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "VSBorderStyle", 2);
	ApqJS.Object.pAdd(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "SBorderStyle", 3);
	ApqJS.Function.Create(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "HBorderWidth_get",
		function() {
			return this._HBorderWidth || this._BorderWidth;
		}
		, true);
	ApqJS.Function.Create(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "VBorderWidth_get",
		function() {
			return this._VBorderWidth || this.HBorderWidth_get();
		}
		, true);
	ApqJS.Function.Create(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "HBorderColor_get",
		function() {
			return this._HBorderColor || this._BorderColor;
		}
		, true);
	ApqJS.Function.Create(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "VBorderColor_get",
		function() {
			return this._VBorderColor || this.HBorderColor_get();
		}
		, true);
	ApqJS.Function.Create(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "HBorderStyle_get",
		function() {
			return this._HBorderStyle || this._BorderStyle;
		}
		, true);
	ApqJS.Function.Create(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "VBorderStyle_get",
		function() {
			return this._VBorderStyle || this.HBorderStyle_get();
		}
		, true);
	ApqJS.Function.Create(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "HSBorderWidth_get",
		function() {
			return this._HSBorderWidth || this._SBorderWidth;
		}
		, true);
	ApqJS.Function.Create(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "VSBorderWidth_get",
		function() {
			return this._VSBorderWidth || this.HSBorderWidth_get();
		}
		, true);
	ApqJS.Function.Create(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "HSBorderColor_get",
		function() {
			return this._HSBorderColor || this._SBorderColor;
		}
		, true);
	ApqJS.Function.Create(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "VSBorderColor_get",
		function() {
			return this._VSBorderColor || this.HSBorderColor_get();
		}
		, true);
	ApqJS.Function.Create(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "HSBorderStyle_get",
		function() {
			return this._HSBorderStyle || this._SBorderStyle;
		}
		, true);
	ApqJS.Function.Create(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "VSBorderStyle_get",
		function() {
			return this._VSBorderStyle || this.HSBorderStyle_get();
		}
		, true);

	ApqJS.Function.Create(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "_OnHBorderWidthChanged",
		function(e) {
			for (var i = 0; i < this.table.cells.length; i++) {
				var td = this.table.cells[i];
				td.style.borderTopWidth = 0;
				td.style.borderBottomWidth = this.HBorderWidth_get();
			}

			ApqJS.Object.fireEvent(this, "HBorderWidthChanged", e);
		}
		, true);
	///#endregion 属性

	///#region 事件
	ApqJS.Function.Create(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "_OnHBorderWidthChanged",
		function(e) {
			for (var i = 0; i < this.table.cells.length; i++) {
				var td = this.table.cells[i];
				td.style.borderTopWidth = 0;
				td.style.borderBottomWidth = this.HBorderWidth_get();
			}

			ApqJS.Object.fireEvent(this, "HBorderWidthChanged", e);
		}
		, true);

	ApqJS.Function.Create(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "_OnVBorderWidthChanged",
		function(e) {
			for (var i = 0; i < this.table.cells.length; i++) {
				var td = this.table.cells[i];
				td.style.borderLeftWidth = 0;
				td.style.borderRightWidth = this.VBorderWidth_get();
			}

			ApqJS.Object.fireEvent(this, "VBorderWidthChanged", e);
		}
		, true);

	ApqJS.Function.Create(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "_OnBorderWidthChanged",
		function(e) {
			this.HBorderWidth_set(this.BorderWidth_get());
			this.VBorderWidth_set(this.BorderWidth_get());

			ApqJS.Object.fireEvent(this, "BorderWidthChanged", e);
		}
		, true);

	ApqJS.Function.Create(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "_OnHBorderColorChanged",
		function(e) {
			for (var i = 0; i < this.table.cells.length; i++) {
				this.table.cells[i].style.borderBottomColor = this.HBorderColor_get();
			}

			ApqJS.Object.fireEvent(this, "HBorderColorChanged", e);
		}
		, true);

	ApqJS.Function.Create(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "_OnVBorderColorChanged",
		function(e) {
			for (var i = 0; i < this.table.cells.length; i++) {
				this.table.cells[i].style.borderRightColor = this.VBorderColor_get();
			}

			ApqJS.Object.fireEvent(this, "VBorderColorChanged", e);
		}
		, true);

	ApqJS.Function.Create(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "_OnBorderColorChanged",
		function(e) {
			this.HBorderColor_set(this.BorderColor_get());
			this.VBorderColor_set(this.BorderColor_get());

			ApqJS.Object.fireEvent(this, "BorderColorChanged", e);
		}
		, true);

	ApqJS.Function.Create(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "_OnHBorderStyleChanged",
		function(e) {
			for (var i = 0; i < this.table.cells.length; i++) {
				this.table.cells[i].style.borderBottomStyle = this.HBorderStyle_get();
			}

			ApqJS.Object.fireEvent(this, "HBorderStyleChanged", e);
		}
		, true);

	ApqJS.Function.Create(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "_OnVBorderStyleChanged",
		function(e) {
			for (var i = 0; i < this.table.cells.length; i++) {
				this.table.cells[i].style.borderRightStyle = this.VBorderStyle_get();
			}

			ApqJS.Object.fireEvent(this, "VBorderStyleChanged", e);
		}
		, true);

	ApqJS.Function.Create(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "_OnBorderStyleChanged",
		function(e) {
			this.HBorderStyle_set(this.BorderStyle_get());
			this.VBorderStyle_set(this.BorderStyle_get());

			ApqJS.Object.fireEvent(this, "BorderStyleChanged", e);
		}
		, true);

	///#region 分割线
	ApqJS.Function.Create(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "_OnHSBorderWidthChanged",
		function(e) {
			if (this.Rows > 0) {
				var tr = this.table.rows[this.Rows - 1];
				for (var d = 0; d < tr.cells.length; d++) {
					tr.cells[d].style.borderBottomWidth = this.HSBorderWidth_get();
				}

				ApqJS.Object.fireEvent(this, "HSBorderWidthChanged", e);
			}
		}
		, true);

	ApqJS.Function.Create(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "_OnVSBorderWidthChanged",
		function(e) {
			if (this.Cols > 0) {
				for (var r = 0; r < this.table.rows.length; r++) {
					this.table.rows[r].cells[this.Cols - 1].style.borderRightWidth = this.VSBorderWidth_get();
				}

				ApqJS.Object.fireEvent(this, "VSBorderWidthChanged", e);
			}
		}
		, true);

	ApqJS.Function.Create(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "_OnSBorderWidthChanged",
		function(e) {
			this.HSBorderWidth_set(this.SBorderWidth_get());
			this.VSBorderWidth_set(this.SBorderWidth_get());

			ApqJS.Object.fireEvent(this, "SBorderWidthChanged", e);
		}
		, true);

	ApqJS.Function.Create(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "_OnHSBorderColorChanged",
		function(e) {
			if (this.Rows > 0) {
				var tr = this.table.rows[this.Rows - 1];
				for (var d = 0; d < tr.cells.length; d++) {
					tr.cells[d].style.borderBottomColor = this.HSBorderColor_get();
				}

				ApqJS.Object.fireEvent(this, "HSBorderColorChanged", e);
			}
		}
		, true);

	ApqJS.Function.Create(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "_OnVSBorderColorChanged",
		function(e) {
			if (this.Cols > 0) {
				for (var r = 0; r < this.table.rows.length; r++) {
					this.table.rows[r].cells[this.Cols - 1].style.borderRightColor = this.VSBorderColor_get();
				}

				ApqJS.Object.fireEvent(this, "VSBorderColorChanged", e);
			}
		}
		, true);

	ApqJS.Function.Create(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "_OnSBorderColorChanged",
		function(e) {
			this.HSBorderColor_set(this.SBorderColor_get());
			this.VSBorderColor_set(this.SBorderColor_get());

			ApqJS.Object.fireEvent(this, "SBorderColorChanged", e);
		}
		, true);

	ApqJS.Function.Create(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "_OnHSBorderStyleChanged",
		function(e) {
			if (this.Rows > 0) {
				var tr = this.table.rows[this.Rows - 1];
				for (var d = 0; d < tr.cells.length; d++) {
					tr.cells[d].style.borderBottomStyle = this.HSBorderStyle_get();
				}

				ApqJS.Object.fireEvent(this, "HSBorderStyleChanged", e);
			}
		}
		, true);

	ApqJS.Function.Create(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "_OnVSBorderStyleChanged",
		function(e) {
			if (this.Cols > 0) {
				for (var r = 0; r < this.table.rows.length; r++) {
					this.table.rows[r].cells[this.Cols - 1].style.borderRightStyle = this.VSBorderStyle_get();
				}

				ApqJS.Object.fireEvent(this, "VSBorderStyleChanged", e);
			}
		}
		, true);

	ApqJS.Function.Create(ApqJS.Web.UI.WebControls.ScrollTable.prototype, "_OnSBorderStyleChanged",
		function(e) {
			this.HSBorderStyle_set(this.SBorderStyle_get());
			this.VSBorderStyle_set(this.SBorderStyle_get());

			ApqJS.Object.fireEvent(this, "SBorderStyleChanged", e);
		}
		, true);
	///#endregion 分割线
	///#endregion 事件

	ApqJS.Web.UI.WebControls.ScrollTable.prototype.InsertRow = function(index, row) {
		// 设置行头,边框,分割线
		ApqJS.Web.UI.WebControls.ScrollTable.SetRH(row, this.zIndex);
	};
}
