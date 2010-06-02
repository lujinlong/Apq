ApqJS.namespace("ApqJS.Html");

if (!ApqJS.Html.WebControl) {
	/// ApqJS.Html.WebControl -------------------------------------------------------------------------------------------------------------------
	ApqJS.Html.WebControl = ApqJS.Class("ApqJS.Html.WebControl", null, true);
	ApqJS.Html.WebControl.prototype.ctor = function() {
		this.Events = {
			"Attached": ApqJS.Event(),
			"Inited": ApqJS.Event(),
			"Loaded": ApqJS.Event(),
			"Showing": ApqJS.Event(),
			"Rendering": ApqJS.Event()
		};
	};

	/// 任意参数
	ApqJS.Html.WebControl.prototype.Attach = function() {
		if (!this.IsAttached) {
			if (this._Attach) {
				this._Attach.apply(this, arguments);
			}
			this.IsAttached = true;

			this.Events["Attached"].Fire(this, null);
		}
	};

	ApqJS.Html.WebControl.prototype.Init = function() {
		if (!this.IsInited) {
			if (this._Init) {
				this._Init.apply(this, arguments);
			}
			this.IsInited = true;

			this.Events["Inited"].Fire(this, null);
		}
	};

	ApqJS.Html.WebControl.prototype.Show = function() {
		if (!this.IsInited) {
			this.Init();
		}

		this.Events["Showing"].Fire(this, null);

		if (this._Show) {
			this._Show.apply(this, arguments);
		}
	};

	ApqJS.Html.WebControl.prototype.Render = function() {
		if (!this.IsInited) {
			this.Init();
		}

		this.Events["Rendering"].Fire(this, null);

		if (this._Render) {
			return this._Render.apply(this, arguments);
		}
		return null;
	};

	/// ApqJS.Html.Table ------------------------------------------------------------------------------------------------------------------------
	ApqJS.Html.Table = ApqJS.Class("ApqJS.Html.Table", [ApqJS.Html.WebControl]);
	//	ApqJS.Html.Table.prototype.ctor = function(){
	//		this.base();
	//	};

	ApqJS.Html.Table.GetRowIndex = function(elt) {
		for (var tr = elt; tr && tr.parentElement != tr; tr = tr.parentElement) {
			if (tr.tagName == "TR") {
				return tr.rowIndex;
			}
		}
		return -1;
	};

	ApqJS.Html.Table.SetBorder = function(elt) {
		for (var tr = elt; tr && tr.parentElement != tr; tr = tr.parentElement) {
			if (tr.tagName == "TR") {
				return tr.rowIndex;
			}
		}
		return -1;
	};
}
