ApqJS.using("ApqJS.Xml");

if (!ApqJS.Xml.Menu) {
	/// ApqJS.Xml.Menu --------------------------------------------------------------------------------------------------------------------------
	ApqJS.Xml.Menu = ApqJS.Class("ApqJS.Xml.Menu", [ApqJS.Xml.XmlNode]);
	ApqJS.Xml.Menu.prototype.ctor = function(xn, ParentMenuItem) {
		this.base(xn);

		this.ParentMenuItem = ParentMenuItem;
		this.Items = [];
	};

	ApqJS.Xml.Menu.prototype.Init = function(c) {
		this.ReadFromNode(null, "Width", "Height", "backgroundImage", "Icon", "PupOutIcon", "backgroundColor");

		this.Items.length = this.xn.childNodes.length;
		for (var i = 0; i < this.Items.length; i++) {
			this.Items[i] = new ApqJS.Xml.MenuItem(this.xn.childNodes[i], this);
			this.Items[i].Init(c);
		}
		this.IsInited = true;
	};

	/// ApqJS.Xml.MenuItem ----------------------------------------------------------------------------------------------------------------------
	ApqJS.Xml.MenuItem = ApqJS.Class("ApqJS.Xml.MenuItem", [ApqJS.Xml.XmlNode]);
	ApqJS.Xml.MenuItem.prototype.ctor = function(xn, Menu) {
		this.base(xn);

		this.Menu = Menu;

		this.ContextMenu = null; // 留待实现
		this.Depth = 0; // 留待实现
	};

	/// [Flag]菜单条目状态
	ApqJS.Xml.MenuItem.State = {
		"none": 0x0,
		"disabled": 0x1,
		"Selectable": 0x2,
		"Selected": 0x4
	};

	ApqJS.Xml.MenuItem.prototype.Init = function(c) {
		c = c == null ? true : c;

		this.ReadFromNode(null, "Icon", "Text", "PupOutIcon", "Height", "title", "onclick", "state", "Separator");

		this.HasChidMenu = this.xn.hasChildNodes();
		if (this.HasChidMenu) {
			this.ChildMenu = new ApqJS.Xml.Menu(this.xn.firstChild, this);
			if (c) {
				this.ChildMenu.Init(c);
			}
		}
		this.IsInited = true;
	};
}
