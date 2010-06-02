namespace ApqDBManager.XmlServersCfg
{
	partial class MainForm
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
			this.bar1 = new DevExpress.XtraBars.Bar();
			this.blbiNew = new DevExpress.XtraBars.BarLargeButtonItem();
			this.blbiOpen = new DevExpress.XtraBars.BarLargeButtonItem();
			this.blbiSave = new DevExpress.XtraBars.BarLargeButtonItem();
			this.bar2 = new DevExpress.XtraBars.Bar();
			this.bsiFile = new DevExpress.XtraBars.BarSubItem();
			this.menuNew = new DevExpress.XtraBars.BarButtonItem();
			this.menuOpen = new DevExpress.XtraBars.BarButtonItem();
			this.menuSave = new DevExpress.XtraBars.BarButtonItem();
			this.menuSaveAs = new DevExpress.XtraBars.BarButtonItem();
			this.menuExit = new DevExpress.XtraBars.BarButtonItem();
			this.bsiEdit = new DevExpress.XtraBars.BarSubItem();
			this.menuUndo = new DevExpress.XtraBars.BarButtonItem();
			this.menuRedo = new DevExpress.XtraBars.BarButtonItem();
			this.menuCut = new DevExpress.XtraBars.BarButtonItem();
			this.menuCopy = new DevExpress.XtraBars.BarButtonItem();
			this.menuPaste = new DevExpress.XtraBars.BarButtonItem();
			this.menuSelectAll = new DevExpress.XtraBars.BarButtonItem();
			this.menuReverse = new DevExpress.XtraBars.BarButtonItem();
			this.bsiView = new DevExpress.XtraBars.BarSubItem();
			this.menuToolBar = new DevExpress.XtraBars.BarCheckItem();
			this.menuStatusBar = new DevExpress.XtraBars.BarCheckItem();
			this.bsiTool = new DevExpress.XtraBars.BarSubItem();
			this.menuDES = new DevExpress.XtraBars.BarButtonItem();
			this.menuRandom = new DevExpress.XtraBars.BarButtonItem();
			this.bsiWindow = new DevExpress.XtraBars.BarSubItem();
			this.menuCloseAll = new DevExpress.XtraBars.BarButtonItem();
			this.menuNewApp = new DevExpress.XtraBars.BarButtonItem();
			this.bsiHelp = new DevExpress.XtraBars.BarSubItem();
			this.menuContents = new DevExpress.XtraBars.BarButtonItem();
			this.menuIndex = new DevExpress.XtraBars.BarButtonItem();
			this.menuSearch = new DevExpress.XtraBars.BarButtonItem();
			this.menuAbout = new DevExpress.XtraBars.BarButtonItem();
			this.bar3 = new DevExpress.XtraBars.Bar();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
			this.SuspendLayout();
			// 
			// barManager1
			// 
			this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar2,
            this.bar3});
			this.barManager1.DockControls.Add(this.barDockControlTop);
			this.barManager1.DockControls.Add(this.barDockControlBottom);
			this.barManager1.DockControls.Add(this.barDockControlLeft);
			this.barManager1.DockControls.Add(this.barDockControlRight);
			this.barManager1.Form = this;
			this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bsiFile,
            this.menuNew,
            this.menuOpen,
            this.bsiEdit,
            this.menuUndo,
            this.menuRedo,
            this.menuCopy,
            this.menuCut,
            this.menuPaste,
            this.menuSelectAll,
            this.menuReverse,
            this.menuSave,
            this.menuSaveAs,
            this.menuExit,
            this.bsiView,
            this.menuToolBar,
            this.menuStatusBar,
            this.bsiTool,
            this.bsiWindow,
            this.bsiHelp,
            this.menuCloseAll,
            this.menuContents,
            this.menuIndex,
            this.menuSearch,
            this.menuAbout,
            this.blbiNew,
            this.blbiOpen,
            this.menuRandom,
            this.blbiSave,
            this.menuNewApp,
            this.menuDES});
			this.barManager1.MainMenu = this.bar2;
			this.barManager1.MaxItemId = 4;
			this.barManager1.StatusBar = this.bar3;
			// 
			// bar1
			// 
			this.bar1.BarName = "Tools";
			this.bar1.DockCol = 0;
			this.bar1.DockRow = 1;
			this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.blbiNew),
            new DevExpress.XtraBars.LinkPersistInfo(this.blbiOpen),
            new DevExpress.XtraBars.LinkPersistInfo(this.blbiSave)});
			this.bar1.Text = "Tools";
			// 
			// blbiNew
			// 
			this.blbiNew.Caption = "新建";
			this.blbiNew.Id = 43;
			this.blbiNew.Name = "blbiNew";
			this.blbiNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuNew_ItemClick);
			// 
			// blbiOpen
			// 
			this.blbiOpen.Caption = "打开";
			this.blbiOpen.Id = 44;
			this.blbiOpen.Name = "blbiOpen";
			this.blbiOpen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuOpen_ItemClick);
			// 
			// blbiSave
			// 
			this.blbiSave.Caption = "保存";
			this.blbiSave.Id = 47;
			this.blbiSave.Name = "blbiSave";
			this.blbiSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuSave_ItemClick);
			// 
			// bar2
			// 
			this.bar2.BarName = "Main menu";
			this.bar2.DockCol = 0;
			this.bar2.DockRow = 0;
			this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bsiFile),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsiEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsiView),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsiTool),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsiWindow),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsiHelp)});
			this.bar2.OptionsBar.MultiLine = true;
			this.bar2.OptionsBar.UseWholeRow = true;
			this.bar2.Text = "Main menu";
			// 
			// bsiFile
			// 
			this.bsiFile.Caption = "文件(&F)";
			this.bsiFile.Id = 0;
			this.bsiFile.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.menuNew),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuOpen),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuSave),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuSaveAs),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuExit)});
			this.bsiFile.Name = "bsiFile";
			// 
			// menuNew
			// 
			this.menuNew.Caption = "新建(&N)";
			this.menuNew.Id = 10;
			this.menuNew.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N));
			this.menuNew.Name = "menuNew";
			this.menuNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuNew_ItemClick);
			// 
			// menuOpen
			// 
			this.menuOpen.Caption = "打开(&O)";
			this.menuOpen.Id = 11;
			this.menuOpen.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O));
			this.menuOpen.Name = "menuOpen";
			this.menuOpen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuOpen_ItemClick);
			// 
			// menuSave
			// 
			this.menuSave.Caption = "保存(&S)";
			this.menuSave.Id = 20;
			this.menuSave.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S));
			this.menuSave.Name = "menuSave";
			this.menuSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuSave_ItemClick);
			// 
			// menuSaveAs
			// 
			this.menuSaveAs.Caption = "另存为(&A)";
			this.menuSaveAs.Id = 21;
			this.menuSaveAs.Name = "menuSaveAs";
			this.menuSaveAs.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuSaveAs_ItemClick);
			// 
			// menuExit
			// 
			this.menuExit.Caption = "退出(&X)";
			this.menuExit.Id = 22;
			this.menuExit.Name = "menuExit";
			this.menuExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuExit_ItemClick);
			// 
			// bsiEdit
			// 
			this.bsiEdit.Caption = "编辑(&E)";
			this.bsiEdit.Id = 12;
			this.bsiEdit.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.menuUndo),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuRedo),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuCut),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuCopy),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuPaste),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuSelectAll),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuReverse)});
			this.bsiEdit.Name = "bsiEdit";
			// 
			// menuUndo
			// 
			this.menuUndo.Caption = "撤消(&U)";
			this.menuUndo.Id = 13;
			this.menuUndo.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z));
			this.menuUndo.Name = "menuUndo";
			this.menuUndo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuUndo_ItemClick);
			// 
			// menuRedo
			// 
			this.menuRedo.Caption = "重复(&R)";
			this.menuRedo.Enabled = false;
			this.menuRedo.Id = 14;
			this.menuRedo.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y));
			this.menuRedo.Name = "menuRedo";
			this.menuRedo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuRedo_ItemClick);
			// 
			// menuCut
			// 
			this.menuCut.Caption = "剪切(&T)";
			this.menuCut.Id = 16;
			this.menuCut.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X));
			this.menuCut.Name = "menuCut";
			this.menuCut.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuCut_ItemClick);
			// 
			// menuCopy
			// 
			this.menuCopy.Caption = "复制(&C)";
			this.menuCopy.Id = 15;
			this.menuCopy.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C));
			this.menuCopy.Name = "menuCopy";
			this.menuCopy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuCopy_ItemClick);
			// 
			// menuPaste
			// 
			this.menuPaste.Caption = "粘贴(&P)";
			this.menuPaste.Id = 17;
			this.menuPaste.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V));
			this.menuPaste.Name = "menuPaste";
			this.menuPaste.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuPaste_ItemClick);
			// 
			// menuSelectAll
			// 
			this.menuSelectAll.Caption = "全选(&A)";
			this.menuSelectAll.Id = 18;
			this.menuSelectAll.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A));
			this.menuSelectAll.Name = "menuSelectAll";
			this.menuSelectAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuSelectAll_ItemClick);
			// 
			// menuReverse
			// 
			this.menuReverse.Caption = "反选(&S)";
			this.menuReverse.Enabled = false;
			this.menuReverse.Id = 19;
			this.menuReverse.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R));
			this.menuReverse.Name = "menuReverse";
			this.menuReverse.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuReverse_ItemClick);
			// 
			// bsiView
			// 
			this.bsiView.Caption = "视图(&V)";
			this.bsiView.Id = 24;
			this.bsiView.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.menuToolBar),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuStatusBar)});
			this.bsiView.Name = "bsiView";
			// 
			// menuToolBar
			// 
			this.menuToolBar.Caption = "工具栏(&T)";
			this.menuToolBar.Checked = true;
			this.menuToolBar.Id = 25;
			this.menuToolBar.Name = "menuToolBar";
			this.menuToolBar.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.menuToolBar_CheckedChanged);
			// 
			// menuStatusBar
			// 
			this.menuStatusBar.Caption = "状态栏(&S)";
			this.menuStatusBar.Checked = true;
			this.menuStatusBar.Id = 26;
			this.menuStatusBar.Name = "menuStatusBar";
			this.menuStatusBar.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.menuStatusBar_CheckedChanged);
			// 
			// bsiTool
			// 
			this.bsiTool.Caption = "工具(&T)";
			this.bsiTool.Id = 27;
			this.bsiTool.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.menuDES),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuRandom)});
			this.bsiTool.Name = "bsiTool";
			// 
			// menuDES
			// 
			this.menuDES.Caption = "DES加解密(&D)";
			this.menuDES.Id = 3;
			this.menuDES.Name = "menuDES";
			this.menuDES.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuDES_ItemClick);
			// 
			// menuRandom
			// 
			this.menuRandom.Caption = "随机串生成器(&R)";
			this.menuRandom.Id = 50;
			this.menuRandom.Name = "menuRandom";
			this.menuRandom.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuRandom_ItemClick);
			// 
			// bsiWindow
			// 
			this.bsiWindow.Caption = "窗口(&W)";
			this.bsiWindow.Id = 29;
			this.bsiWindow.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.menuCloseAll),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuNewApp)});
			this.bsiWindow.Name = "bsiWindow";
			// 
			// menuCloseAll
			// 
			this.menuCloseAll.Caption = "全部关闭(&L)";
			this.menuCloseAll.Enabled = false;
			this.menuCloseAll.Id = 35;
			this.menuCloseAll.Name = "menuCloseAll";
			this.menuCloseAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuCloseAll_ItemClick);
			// 
			// menuNewApp
			// 
			this.menuNewApp.Caption = "另开窗口(&N)";
			this.menuNewApp.Id = 2;
			this.menuNewApp.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12);
			this.menuNewApp.Name = "menuNewApp";
			this.menuNewApp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuNewApp_ItemClick);
			// 
			// bsiHelp
			// 
			this.bsiHelp.Caption = "帮助(&H)";
			this.bsiHelp.Id = 30;
			this.bsiHelp.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.menuContents),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuIndex),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuSearch),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuAbout)});
			this.bsiHelp.Name = "bsiHelp";
			// 
			// menuContents
			// 
			this.menuContents.Caption = "目录(&C)";
			this.menuContents.Id = 37;
			this.menuContents.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F1));
			this.menuContents.Name = "menuContents";
			this.menuContents.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuContents_ItemClick);
			// 
			// menuIndex
			// 
			this.menuIndex.Caption = "索引(&I)";
			this.menuIndex.Id = 38;
			this.menuIndex.Name = "menuIndex";
			this.menuIndex.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuIndex_ItemClick);
			// 
			// menuSearch
			// 
			this.menuSearch.Caption = "搜索(&S)";
			this.menuSearch.Id = 39;
			this.menuSearch.Name = "menuSearch";
			this.menuSearch.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuSearch_ItemClick);
			// 
			// menuAbout
			// 
			this.menuAbout.Caption = "关于(&A)";
			this.menuAbout.Id = 40;
			this.menuAbout.Name = "menuAbout";
			this.menuAbout.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuAbout_ItemClick);
			// 
			// bar3
			// 
			this.bar3.BarName = "Status bar";
			this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
			this.bar3.DockCol = 0;
			this.bar3.DockRow = 0;
			this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
			this.bar3.OptionsBar.AllowQuickCustomization = false;
			this.bar3.OptionsBar.DrawDragBorder = false;
			this.bar3.OptionsBar.UseWholeRow = true;
			this.bar3.Text = "Status bar";
			// 
			// dockPanel1
			// 
			this.dockPanel1.ActiveAutoHideContent = null;
			this.dockPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dockPanel1.DockBottomPortion = 150;
			this.dockPanel1.DockLeftPortion = 200;
			this.dockPanel1.DockRightPortion = 200;
			this.dockPanel1.DockTopPortion = 150;
			this.dockPanel1.Location = new System.Drawing.Point(0, 49);
			this.dockPanel1.Name = "dockPanel1";
			this.dockPanel1.Size = new System.Drawing.Size(888, 354);
			this.dockPanel1.TabIndex = 5;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(888, 425);
			this.Controls.Add(this.dockPanel1);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.IsMdiContainer = true;
			this.Name = "MainForm";
			this.Text = "ApqDBManager服务器编辑器";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraBars.BarManager barManager1;
		private DevExpress.XtraBars.Bar bar1;
		private DevExpress.XtraBars.Bar bar2;
		private DevExpress.XtraBars.Bar bar3;
		private DevExpress.XtraBars.BarDockControl barDockControlTop;
		private DevExpress.XtraBars.BarDockControl barDockControlBottom;
		private DevExpress.XtraBars.BarDockControl barDockControlLeft;
		private DevExpress.XtraBars.BarDockControl barDockControlRight;
		private DevExpress.XtraBars.BarSubItem bsiFile;
		private DevExpress.XtraBars.BarButtonItem menuNew;
		private DevExpress.XtraBars.BarButtonItem menuOpen;
		private DevExpress.XtraBars.BarButtonItem menuSave;
		private DevExpress.XtraBars.BarButtonItem menuSaveAs;
		private DevExpress.XtraBars.BarButtonItem menuExit;
		private DevExpress.XtraBars.BarSubItem bsiEdit;
		private DevExpress.XtraBars.BarButtonItem menuUndo;
		private DevExpress.XtraBars.BarButtonItem menuRedo;
		private DevExpress.XtraBars.BarButtonItem menuCopy;
		private DevExpress.XtraBars.BarButtonItem menuCut;
		private DevExpress.XtraBars.BarButtonItem menuPaste;
		private DevExpress.XtraBars.BarButtonItem menuSelectAll;
		private DevExpress.XtraBars.BarButtonItem menuReverse;
		private DevExpress.XtraBars.BarSubItem bsiView;
		private DevExpress.XtraBars.BarCheckItem menuToolBar;
		private DevExpress.XtraBars.BarCheckItem menuStatusBar;
		private DevExpress.XtraBars.BarSubItem bsiTool;
		private DevExpress.XtraBars.BarButtonItem menuDES;
		private DevExpress.XtraBars.BarButtonItem menuRandom;
		private DevExpress.XtraBars.BarSubItem bsiWindow;
		private DevExpress.XtraBars.BarButtonItem menuCloseAll;
		private DevExpress.XtraBars.BarSubItem bsiHelp;
		private DevExpress.XtraBars.BarButtonItem menuContents;
		private DevExpress.XtraBars.BarButtonItem menuIndex;
		private DevExpress.XtraBars.BarButtonItem menuSearch;
		private DevExpress.XtraBars.BarButtonItem menuAbout;
		private DevExpress.XtraBars.BarLargeButtonItem blbiNew;
		private DevExpress.XtraBars.BarLargeButtonItem blbiOpen;
		private DevExpress.XtraBars.BarLargeButtonItem blbiSave;
		private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel1;
		private DevExpress.XtraBars.BarButtonItem menuNewApp;
	}
}