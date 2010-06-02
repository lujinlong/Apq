ApqJS.namespace("ApqJS.Simulator");

if (!ApqJS.Simulator.Dialog) {
	ApqJS.Simulator.Dialog = ApqJS.Class("ApqJS.Simulator.Dialog");
	/// 模拟弹出框(高度自适应)
	/// <Type>类型{0:模拟alert,1:模式窗口,2:Lookup}</Type>
	/// <Title>标题</Title>
	/// <Content>Type==0:对话框内容;Type==1:Url</Content>
	/// <Width>窗口宽度</Width>
	/// <Top>与顶边框的距离[相对win.document.body]</Top>
	/// <Left>与左边框的距离[相对win.document.body]</Left>
	/// <btnConfirm_Click>确定回调函数</btnConfirm_Click>
	/// <btnCancel_Click>取消回调函数</btnCancel_Click>
	/// <win=window>弹出框所在window</win>
	ApqJS.Simulator.Dialog.prototype.ctor = function(Type, Title, Content, Width, Left, Top, btnConfirm_Click, btnCancel_Click, win) {
		var me = this;
		this.ownerWindow = win = win || window;

		switch (Type) {
			case 0:
			case 1:
				// divDialogBg
				this.divDialogBg = ApqJS.document.CreateElement("div", win.document);
				ApqJS.document.insertBefore(win.document.body, this.divDialogBg);
				this.divDialogBg.style.display = "none";
				this.divDialogBg.style.position = "absolute";
				this.divDialogBg.style.backgroundImage = "url(" + ApqJS.Apq_ConfigChain.GetConfig("Dialog_bgPic") + ")";
				this.divDialogBg.style.zIndex = ApqJS.Apq_ConfigChain.GetConfig("Dialog_zIndex");
				ApqJS.Apq_ConfigChain.SetConfig("Dialog_zIndex", this.divDialogBg.style.zIndex + 1);
				this.divDialogBg.style.top = 0;
				break;
			case 2:
				break;
		}

		// divDialog
		this.divDialog = ApqJS.document.CreateElement("div", win.document);
		ApqJS.document.insertBefore(win.document.body, this.divDialog);
		this.divDialog.style.display = "none";
		this.divDialog.style.position = "absolute";
		this.divDialog.style.posWidth = Width;
		this.divDialog.style.borderStyle = "solid";
		this.divDialog.style.borderColor = "#CCC";
		this.divDialog.style.backgroundColor = "white";
		this.divDialog.style.borderWidth = 1;
		this.divDialog.style.zIndex = ApqJS.Apq_ConfigChain.GetConfig("Dialog_zIndex");
		ApqJS.Apq_ConfigChain.SetConfig("Dialog_zIndex", this.divDialog.style.zIndex + 1);
		this.divDialog.style.left = Left || (win.document.body.clientWidth - Width) / 2;
		this.divDialog.style.top = Top || 0;
		this.divDialog.ApqDialog = me; // 用于寻找JS对象

		// divTitle
		this.divTitle = ApqJS.document.CreateElement("div", win.document);
		ApqJS.document.insertBefore(this.divDialog, this.divTitle);
		this.divTitle.style.width = "100%";
		this.divTitle.style.backgroundColor = "#CCC";
		this.spanClose = ApqJS.document.CreateElement("span", win.document);
		ApqJS.document.insertBefore(this.divTitle, this.spanClose);
		this.spanClose.style.styleFloat = "right";
		this.spanClose.innerText = "X";
		this.spanClose.attachEvent("onclick", function(evt) {
			me.Close();
		});
		this.spanTitle = ApqJS.document.CreateElement("span", win.document);
		ApqJS.document.insertBefore(this.divTitle, this.spanTitle);
		this.spanTitle.innerText = Title;

		switch (Type) {
			case 0:
				// divContent
				this.divContent = ApqJS.document.CreateElement("div", win.document);
				ApqJS.document.insertBefore(this.divDialog, this.divContent);
				this.divContent.innerText = Content;

				// divButton
				this.divButton = ApqJS.document.CreateElement("div", win.document);
				ApqJS.document.insertBefore(this.divDialog, this.divButton);
				this.divButton.style.width = "100%";
				this.divButton.align = "center";
				this.btnConfirm = ApqJS.document.CreateElement("input", win.document);
				this.btnConfirm.type = "button";
				ApqJS.document.insertBefore(this.divButton, this.btnConfirm);
				this.btnConfirm.value = "确定";
				if (btnConfirm_Click) this.btnConfirm.attachEvent("onclick", btnConfirm_Click);
				this.btnConfirm.attachEvent("onclick", function() {
					me.spanClose.click();
				});
				this.btnCancel = ApqJS.document.CreateElement("input", win.document);
				this.btnCancel.type = "button";
				ApqJS.document.insertBefore(this.divButton, this.btnCancel);
				this.btnCancel.value = "取消";
				if (btnCancel_Click) this.btnCancel.attachEvent("onclick", btnCancel_Click);
				this.btnCancel.attachEvent("onclick", function() {
					me.spanClose.click();
				});
				break;

			case 1:
			case 2:
				// ifDialog
				this.ifDialog = ApqJS.document.CreateElement("iframe", win.document);
				ApqJS.document.insertBefore(this.divDialog, this.ifDialog);
				this.ifDialog.frameBorder = "0";
				this.ifDialog.width = "100%";
				this.ifDialog.src = Content;
				break;
		}

		// divClear
		this.divClear = ApqJS.document.CreateElement("div", win.document);
		ApqJS.document.insertBefore(this.divDialog, this.divClear);
		this.divClear.style.clear = "both";

		switch (Type) {
			case 0:
			case 1:
				this.divDialogBg.style.posWidth = win.document.body.scrollWidth;
				this.divDialogBg.style.posHeight = win.document.body.scrollHeight;
				break;
		}
	};

	/// 显示
	ApqJS.Simulator.Dialog.prototype.Show = function() {
		if (this.divDialogBg) this.divDialogBg.style.display = "block";
		this.divDialog.style.display = "block";
		ApqJS.document.iframeAutoFit(null, this.ownerWindow);
	};

	/// 隐藏
	ApqJS.Simulator.Dialog.prototype.Hide = function() {
		if (this.divDialogBg) this.divDialogBg.style.display = "none";
		this.divDialog.style.display = "none";
	};

	/// 关闭
	ApqJS.Simulator.Dialog.prototype.Close = function(NeedDispose) {
		if (NeedDispose) this.Dispose();
		else this.Hide();
	};

	/// 释放
	ApqJS.Simulator.Dialog.prototype.Dispose = function() {
		this.Hide();
		this.divDialog.ApqDialog = null;
		this.divDialog.parentNode.removeChild(this.divDialog);
		if (this.divDialogBg) this.divDialogBg.parentNode.removeChild(this.divDialogBg);
	};
}
