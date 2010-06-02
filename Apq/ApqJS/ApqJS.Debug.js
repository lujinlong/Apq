if (!Apq_TopWindow.ApqJS.Debug.IsInited) {
	ApqJS.using("ApqJS.Web.UI.WebControls.ScrollTable");

	Apq_TopWindow.ApqJS.Debug.IsInited = true;
	var ID = 1;
	var imgWidth = 16, posHeight = screen.availHeight / 5;
	var div, tbl,
			ImgDown, ImgRight, ImgUp, ImgLeft;
	var Apq_TopWindow_onload = function() {
		div = Apq_TopWindow.document.createElement("div"); Apq_TopWindow.document.body.appendChild(div);
		div.style.zIndex = 65535;
		div.style.position = "absolute";
		div.style.posHeight = posHeight;
		div.style.removeExpression("posWidth");
		div.style.setExpression("posWidth", "Apq_TopWindow.document.body.clientWidth");
		div.style.removeExpression("posLeft");
		div.style.setExpression("posLeft", "Apq_TopWindow.document.body.scrollLeft + Apq_TopWindow.document.body.clientWidth - this.offsetWidth");
		div.style.removeExpression("posTop");
		div.style.setExpression("posTop", "Apq_TopWindow.document.body.scrollTop + Apq_TopWindow.document.body.clientHeight - this.offsetHeight");
		div.style.backgroundColor = "White";
		div.style.overflow = "auto";
		div.style.borderStyle = "solid";
		div.style.borderWidth = 2;
		div.style.borderColor = "gray";
		tbl = Apq_TopWindow.document.createElement("table"); div.appendChild(tbl);
		tbl.style.removeExpression("posWidth");
		tbl.style.setExpression("posWidth", "this.parentElement.clientWidth");

		var tr = tbl.insertRow();
		tr.style.backgroundColor = "menu";
		var td0 = tr.insertCell();
		var td1 = tr.insertCell();
		var td2 = tr.insertCell();
		var td3 = tr.insertCell();
		tbl_SetCell(td0);
		tbl_SetCell(td1);
		tbl_SetCell(td2);
		tbl_SetCell(td3);
		td0.style.posWidth = 2 * imgWidth;
		td1.style.posWidth = 3 * imgWidth;
		td2.style.removeExpression("posWidth");
		td2.style.setExpression("posWidth", "this.parentElement.parentElement.clientWidth - " + td0.offsetWidth + " - " + td1.offsetWidth + " - " + td3.offsetWidth);
		td0.style.borderTopColor = td1.style.borderTopColor = td2.style.borderTopColor = td3.style.borderTopColor = "gray";
		td0.style.borderRightColor = td1.style.borderRightColor = td2.style.borderRightColor = td3.style.borderRightColor = "gray";
		td0.noWrap = td1.noWrap = td2.noWrap = td3.noWrap = true;
		td0.align = td1.align = td2.align = "center";
		td3.align = "right";
		ImgLeft = Apq_TopWindow.document.createElement("img"); td0.appendChild(ImgLeft);
		ImgRight = Apq_TopWindow.document.createElement("img"); td0.appendChild(ImgRight);
		ImgUp = Apq_TopWindow.document.createElement("img"); td0.appendChild(ImgUp);
		ImgDown = Apq_TopWindow.document.createElement("img"); td0.appendChild(ImgDown);
		ImgDown.style.display = ImgRight.style.display = ImgUp.style.display = ImgLeft.style.display = "none";
		ImgDown.style.display = ImgRight.style.display = "inline";
		ImgLeft.src = Apq_SiteConfig.Apq + "Img/Debug/left.png";
		ImgRight.src = Apq_SiteConfig.Apq + "Img/Debug/right.png";
		ImgUp.src = Apq_SiteConfig.Apq + "Img/Debug/up.png";
		ImgDown.src = Apq_SiteConfig.Apq + "Img/Debug/down.png";
		td1.innerText = "序号";
		td2.innerText = "说明";
		var btnClear = Apq_TopWindow.document.createElement("input"); btnClear.type = "button"; td2.appendChild(btnClear);
		btnClear.value = "清空";
		btnClear.attachEvent("onclick", tbl_Clear);
		var btnHidden = Apq_TopWindow.document.createElement("input"); btnHidden.type = "button"; td3.appendChild(btnHidden);
		btnHidden.value = "隐藏";
		td3.style.posWidth = btnHidden.offsetWidth;
		btnHidden.attachEvent("onclick", function(e) { div.style.display = "none"; });

		var st = new ApqJS.Web.UI.WebControls.ScrollTable();
		st.Attach(tbl, 1);
		st.Init();
		td2.style.borderRightWidth = 0;
		ImgUp.attachEvent("onclick", ImgUp_onclick);
		ImgDown.attachEvent("onclick", ImgDown_onclick);
		ImgRight.attachEvent("onclick", ImgRight_onclick);
		ImgLeft.attachEvent("onclick", ImgLeft_onclick);

		Apq_TopWindow.document.attachEvent("onkeyup",
			function(e) {
				if (e.ctrlKey && e.altKey && e.keyCode == 68)	// Ctrl+Alt+D
				{
					div.style.display = "block";
				}
			}
		);
		//		Apq_TopWindow.attachEvent("onscroll", Apq_TopWindow_onscroll);
	};

	if (Apq_TopWindow.Apq_IsApqJS) {
		setTimeout(Apq_TopWindow_onload, 0);
	}
	else {
		Apq_TopWindow.attachEvent("onload", Apq_TopWindow_onload);
	}

	var tbl_SetCell = function(td) {
		td.style.borderBottomStyle = td.style.borderRightStyle = "solid";
		td.style.borderBottomWidth = td.style.borderRightWidth = 1;
		td.style.borderBottomColor = td.style.borderRightColor = "gray";
	};

	var Apq_TopWindow_onscroll = function(e) {
		ApqJS.Debug.write(Apq_TopWindow.document.body.scrollTop);
	};

	var ImgUp_onclick = function(e) {
		for (var r = 1; r < tbl.rows.length; r++) {
			tbl.rows[r].style.display = "inline";
		}
		div.style.posHeight = posHeight;
		ImgUp.style.display = "none";
		ImgDown.style.display = "inline";
	};

	var ImgDown_onclick = function(e) {
		for (var r = 1; r < tbl.rows.length; r++) {
			tbl.rows[r].style.display = "none";
		}
		div.style.posHeight = 0;
		ImgDown.style.display = "none";
		ImgUp.style.display = "inline";
	};

	var ImgRight_onclick = function(e) {
		div.style.overflowX = "hidden";
		div.style.removeExpression("posWidth");
		div.style.setExpression("posWidth", tbl.rows[0].cells[0].style.posWidth + " + this.offsetWidth - this.clientWidth + \
			(parseFloat(this.style.marginLeftWidth,10)||0) + (parseFloat(this.style.marginRightWidth,10)||0)");
		for (var r = 0; r < tbl.rows.length; r++) {
			for (var d = 1; d < tbl.rows[r].cells.length; d++) {
				tbl.rows[r].cells[d].style.display = "none";
			}
		}
		ImgRight.style.display = "none";
		ImgLeft.style.display = "inline";
	};

	var ImgLeft_onclick = function(e) {
		div.style.removeExpression("posWidth");
		div.style.setExpression("posWidth", "Apq_TopWindow.document.body.clientWidth");
		for (var r = 0; r < tbl.rows.length; r++) {
			for (var d = 1; d < tbl.rows[r].cells.length; d++) {
				tbl.rows[r].cells[d].style.display = "inline";
			}
		}
		ImgLeft.style.display = "none";
		ImgRight.style.display = "inline";
		div.style.overflowX = "auto";
	};

	var tbl_Clear = function(e) {
		while (tbl.rows.length > 1) {
			tbl.rows[1].removeNode(true);
		}
	};

	Apq_TopWindow.ApqJS.Debug.__onwrite = Apq_TopWindow.ApqJS.Debug.__onwriteln = function(obj, e) {
		if (!tbl) {
			return;
		}
		var tr = tbl.insertRow(1);
		tr.style.display = ImgDown.style.display;
		var td0 = tr.insertCell();
		var td1 = tr.insertCell();
		var td2 = tr.insertCell();
		td2.colSpan = 2;
		tbl_SetCell(td0);
		tbl_SetCell(td1);
		tbl_SetCell(td2);
		td0.vAlign = td1.vAlign = "top";
		td0.align = "center";
		td1.align = "right";
		var img = Apq_TopWindow.document.createElement("img");
		td0.appendChild(img);
		img.src = Apq_SiteConfig.Apq + "Img/Debug/error.png";

		td1.innerText = ID++;
		td2.innerHTML = e.Value == null ? "(null)" : ApqJS.Common.HtmlEncode(e.Value.toString());
	};
}

ApqJS.Object.attachEvent(ApqJS.Debug, "onwrite", Apq_TopWindow.ApqJS.Debug.__onwrite);
ApqJS.Object.attachEvent(ApqJS.Debug, "onwriteln", Apq_TopWindow.ApqJS.Debug.__onwriteln);
