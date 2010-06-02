ApqJS.using("ApqJS.Html");

if (!ApqJS.Web.UI.WebControls.Input) {
	/// ApqJS.Web.UI.WebControls.Input -------------------------------------------------------------------------------------------------------
	ApqJS.Web.UI.WebControls.Input = ApqJS.Class("ApqJS.Web.UI.WebControls.Input", [ApqJS.Html.WebControl]);
	//	ApqJS.Web.UI.WebControls.Input.prototype.ctor = function(){
	//		this.base();
	//	};

	ApqJS.Web.UI.WebControls.Input.prototype._Attach = function(_Input) {
		var me = this;

		this._Input = _Input;
	};

	ApqJS.Object.pAdd(ApqJS.Web.UI.WebControls.Input.prototype, "Value", 2);
	ApqJS.Function.Create(ApqJS.Web.UI.WebControls.Input.prototype, "Value_set",
		function(value) {
			if (this._Input.value != value) {
				this._Value = this._Input.value = value;
				this._Input.fireEvent("onchange");
			}
		}
		, true);
}
