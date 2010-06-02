ApqJS.using("ApqJS.Web.UI.WebControls");

if (!ApqJS.Web.UI.WebControls.ContextMenu) {
	ApqJS.using("ApqJS.Xml.Menu");

	/// ApqJS.Web.UI.WebControls.ContextMenu ----------------------------------------------------------------------------------------------------
	ApqJS.Web.UI.WebControls.ContextMenu = ApqJS.Class("ApqJS.Web.UI.WebControls.ContextMenu");
	ApqJS.Web.UI.WebControls.ContextMenu.prototype.ctor = function(win, Menu) {
		ApqJS.Argument.CheckNull("win", win);
		ApqJS.Argument.CheckNull("Menu", Menu);

		this.Menu = Menu;
		this.Popup = win.createPopup();

		this.div = ApqJS.document.CreateElement("div");
		Popup.document.body.appendChild(this.div);
	};

	ApqJS.Web.UI.WebControls.ContextMenu.backgroundColor = "Menu";
	ApqJS.Web.UI.WebControls.ContextMenu.color = "MenuText";

	ApqJS.Web.UI.WebControls.ContextMenu.prototype.Init = function(c) {
		if (!this.Menu.IsInited) {
			this.Menu.Init(c);
		}

		this.div.style.backgroundColor = Menu.backgroundColor || ApqJS.Web.UI.WebControls.ContextMenu.backgroundColor;
		this.div.style.color = Menu.color || ApqJS.Web.UI.WebControls.ContextMenu.color;

		for (var i = 0; i < this.Items.length; i++) {
			var ContextMenuItem = new ApqJS.Web.UI.WebControls.ContextMenuItem(this, this.Items[i]);
			if (c) {
				ContextMenuItem.Init(c);
			}
		}

		this.IsInited = true;
	};

	ApqJS.Web.UI.WebControls.ContextMenu.prototype.Render = function() {
		if (!this.IsInited) {
			this.Init();
		}
	};

	ApqJS.Web.UI.WebControls.ContextMenu.prototype.Show = function() {
		this.Popup.hide();
		if (!this.IsInited) {
			this.Init();
		}
		this.Popup.show();
	};

	/// 设置右键菜单
	ApqJS.Web.UI.WebControls.ContextMenu.Init = function(elt) {
		ApqJS.Argument.CheckNull("elt", elt);

		if (elt.ApqJS_contextmenu) {
			elt.attachEvent("oncontextmenu", fn);
		}
	};

	/// ApqJS.Web.UI.WebControls.ContextMenuItem --------------------------------------------------------------------------------------------
	ApqJS.Web.UI.WebControls.ContextMenuItem = ApqJS.Class("ApqJS.Web.UI.WebControls.ContextMenuItem");
	ApqJS.Web.UI.WebControls.ContextMenuItem.prototype.ctor = function(ContextMenu, MenuItem) {
		ApqJS.Argument.CheckNull("ContextMenu", ContextMenu);
		ApqJS.Argument.CheckNull("XmlNode", XmlNode);

		this.ContextMenu = ContextMenu;
		this.MenuItem = MenuItem;

		this.div = ApqJS.document.CreateElement("div");
		ContextMenu.div.appendChild(this.div);
		this.div.style.backgroundColor = this.MenuItem.backgroundColor || ApqJS.Web.UI.WebControls.ContextMenuItem.backgroundColor;
		this.div.style.color = this.MenuItem.color || ApqJS.Web.UI.WebControls.ContextMenuItem.color;

		// 添加分隔行,这里容易处理些
		if (this.MenuItem.Seprator) {
			var div = ApqJS.document.CreateElement("hr");
			ContextMenu.div.appendChild(div);
		}
	};

	ApqJS.Web.UI.WebControls.ContextMenuItem.backgroundColor = ApqJS.Web.UI.WebControls.ContextMenu.backgroundColor;
	ApqJS.Web.UI.WebControls.ContextMenuItem.color = ApqJS.Web.UI.WebControls.ContextMenu.color;
	ApqJS.Web.UI.WebControls.ContextMenuItem.ImgFolder = Apq_SiteConfig.ApqJS + "/Img/Web.UI.WebControls/Menu/";

	ApqJS.Web.UI.WebControls.ContextMenuItem.prototype.Init = function(c) {
		c = c == null ? true : c;

		this.Icon = ApqJS.document.CreateElement("img");
		this.a = ApqJS.document.CreateElement("a");
		this.PopOutIcon = ApqJS.document.CreateElement("img");

		this.div.style.backgroundImage = this.MenuItem.backgroundImage || ApqJS.Web.UI.WebControls.ContextMenuItem.ImgFolder + "line.gif";
		this.Icon.src = this.MenuItem.Icon || ApqJS.Web.UI.WebControls.ContextMenu.ImgFolder + "file.gif";
		this.div.style.IconWidth = Menu.IconWidth || 20;

		if (this.MenuItem.HasChidMenu) {
			this.div.style.PupOutIcon = Menu.PupOutIcon || ApqJS.Web.UI.WebControls.ContextMenu.ImgFolder + "file_mid.gif";
			this.ChildMenu = new ApqJS.Web.UI.WebControls.ContextMenu(this.ContextMenu.Popup, this.MenuItem.ChildMenu);
			if (c) {
				this.ChildMenu.LoadXmlNode(xn.firstChild, c);
			}
		}
		this.IsInited = true;
	};

	ApqJS.Web.UI.WebControls.ContextMenuItem.prototype.Render = function() {
		if (!this.IsInited) {
			this.Init();
		}
		return this.div.outerHTML;
	};
}
