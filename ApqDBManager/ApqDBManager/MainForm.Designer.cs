namespace ApqDBManager
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiNew = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiOpenSql = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiSave = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiSaveAs = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiUndo = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiRedo = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiCut = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiPaste = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiSelectAll = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiReverse = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiView = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiToolBar = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiStatusBar = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiInstances = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiFavorites = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiErrList = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiTool = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiOption = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiRSAKey = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiDES = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiRandom = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiFTPFileTrans = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiFTPFileUp = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiDBServer = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiWindow = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCloseAll = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiNewApp = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiContents = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiIndex = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiSearch = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiSN = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsbNew = new System.Windows.Forms.ToolStripButton();
			this.tsbOpen = new System.Windows.Forms.ToolStripButton();
			this.tsbSave = new System.Windows.Forms.ToolStripButton();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.menuStrip1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// dockPanel1
			// 
			this.dockPanel1.ActiveAutoHideContent = null;
			this.dockPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dockPanel1.DockBottomPortion = 150;
			this.dockPanel1.DockLeftPortion = 200;
			this.dockPanel1.DockRightPortion = 200;
			this.dockPanel1.DockTopPortion = 150;
			this.dockPanel1.Location = new System.Drawing.Point(0, 0);
			this.dockPanel1.Name = "dockPanel1";
			this.dockPanel1.Size = new System.Drawing.Size(888, 425);
			this.dockPanel1.TabIndex = 5;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.tsmiEdit,
            this.tsmiView,
            this.tsmiTool,
            this.tsmiWindow,
            this.tsmiHelp});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
			this.menuStrip1.Size = new System.Drawing.Size(888, 24);
			this.menuStrip1.TabIndex = 8;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// tsmiFile
			// 
			this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNew,
            this.tsmiOpen,
            this.toolStripSeparator1,
            this.tsmiSave,
            this.tsmiSaveAs,
            this.toolStripSeparator2,
            this.tsmiExit});
			this.tsmiFile.Name = "tsmiFile";
			this.tsmiFile.Size = new System.Drawing.Size(59, 20);
			this.tsmiFile.Text = "文件(&F)";
			// 
			// tsmiNew
			// 
			this.tsmiNew.Name = "tsmiNew";
			this.tsmiNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.tsmiNew.Size = new System.Drawing.Size(153, 22);
			this.tsmiNew.Text = "新建(&N)";
			this.tsmiNew.Click += new System.EventHandler(this.tsmiNew_Click);
			// 
			// tsmiOpen
			// 
			this.tsmiOpen.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpenSql});
			this.tsmiOpen.Name = "tsmiOpen";
			this.tsmiOpen.Size = new System.Drawing.Size(153, 22);
			this.tsmiOpen.Text = "打开(&O)";
			// 
			// tsmiOpenSql
			// 
			this.tsmiOpenSql.Name = "tsmiOpenSql";
			this.tsmiOpenSql.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.tsmiOpenSql.Size = new System.Drawing.Size(171, 22);
			this.tsmiOpenSql.Text = "Sql文件(&S)";
			this.tsmiOpenSql.Click += new System.EventHandler(this.tsmiOpenSql_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(150, 6);
			// 
			// tsmiSave
			// 
			this.tsmiSave.Name = "tsmiSave";
			this.tsmiSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.tsmiSave.Size = new System.Drawing.Size(153, 22);
			this.tsmiSave.Text = "保存(&S)";
			this.tsmiSave.Click += new System.EventHandler(this.tsmiSave_Click);
			// 
			// tsmiSaveAs
			// 
			this.tsmiSaveAs.Name = "tsmiSaveAs";
			this.tsmiSaveAs.Size = new System.Drawing.Size(153, 22);
			this.tsmiSaveAs.Text = "另存为(&A)";
			this.tsmiSaveAs.Click += new System.EventHandler(this.tsmiSaveAs_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(150, 6);
			// 
			// tsmiExit
			// 
			this.tsmiExit.Name = "tsmiExit";
			this.tsmiExit.Size = new System.Drawing.Size(153, 22);
			this.tsmiExit.Text = "退出(&X)";
			this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
			// 
			// tsmiEdit
			// 
			this.tsmiEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiUndo,
            this.tsmiRedo,
            this.toolStripSeparator3,
            this.tsmiCut,
            this.tsmiCopy,
            this.tsmiPaste,
            this.toolStripSeparator4,
            this.tsmiSelectAll,
            this.tsmiReverse});
			this.tsmiEdit.Name = "tsmiEdit";
			this.tsmiEdit.Size = new System.Drawing.Size(59, 20);
			this.tsmiEdit.Text = "编辑(&E)";
			// 
			// tsmiUndo
			// 
			this.tsmiUndo.Name = "tsmiUndo";
			this.tsmiUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
			this.tsmiUndo.Size = new System.Drawing.Size(153, 22);
			this.tsmiUndo.Text = "撤消(&U)";
			this.tsmiUndo.Click += new System.EventHandler(this.tsmiUndo_Click);
			// 
			// tsmiRedo
			// 
			this.tsmiRedo.Name = "tsmiRedo";
			this.tsmiRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
			this.tsmiRedo.Size = new System.Drawing.Size(153, 22);
			this.tsmiRedo.Text = "重做(&R)";
			this.tsmiRedo.Click += new System.EventHandler(this.tsmiRedo_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(150, 6);
			// 
			// tsmiCut
			// 
			this.tsmiCut.Name = "tsmiCut";
			this.tsmiCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.tsmiCut.Size = new System.Drawing.Size(153, 22);
			this.tsmiCut.Text = "剪切(&T)";
			this.tsmiCut.Click += new System.EventHandler(this.tsmiCut_Click);
			// 
			// tsmiCopy
			// 
			this.tsmiCopy.Name = "tsmiCopy";
			this.tsmiCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.tsmiCopy.Size = new System.Drawing.Size(153, 22);
			this.tsmiCopy.Text = "复制(&C)";
			this.tsmiCopy.Click += new System.EventHandler(this.tsmiCopy_Click);
			// 
			// tsmiPaste
			// 
			this.tsmiPaste.Name = "tsmiPaste";
			this.tsmiPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.tsmiPaste.Size = new System.Drawing.Size(153, 22);
			this.tsmiPaste.Text = "粘贴(&P)";
			this.tsmiPaste.Click += new System.EventHandler(this.tsmiPaste_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(150, 6);
			// 
			// tsmiSelectAll
			// 
			this.tsmiSelectAll.Name = "tsmiSelectAll";
			this.tsmiSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
			this.tsmiSelectAll.Size = new System.Drawing.Size(153, 22);
			this.tsmiSelectAll.Text = "全选(&A)";
			this.tsmiSelectAll.Click += new System.EventHandler(this.tsmiSelectAll_Click);
			// 
			// tsmiReverse
			// 
			this.tsmiReverse.Name = "tsmiReverse";
			this.tsmiReverse.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
			this.tsmiReverse.Size = new System.Drawing.Size(153, 22);
			this.tsmiReverse.Text = "反选(&S)";
			this.tsmiReverse.Click += new System.EventHandler(this.tsmiReverse_Click);
			// 
			// tsmiView
			// 
			this.tsmiView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiToolBar,
            this.tsmiStatusBar,
            this.toolStripSeparator5,
            this.tsmiInstances,
            this.tsmiFavorites,
            this.tsmiErrList});
			this.tsmiView.Name = "tsmiView";
			this.tsmiView.Size = new System.Drawing.Size(59, 20);
			this.tsmiView.Text = "视图(&V)";
			// 
			// tsmiToolBar
			// 
			this.tsmiToolBar.Checked = true;
			this.tsmiToolBar.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsmiToolBar.Name = "tsmiToolBar";
			this.tsmiToolBar.Size = new System.Drawing.Size(136, 22);
			this.tsmiToolBar.Text = "工具栏(&T)";
			this.tsmiToolBar.CheckedChanged += new System.EventHandler(this.tsmiToolBar_CheckedChanged);
			this.tsmiToolBar.Click += new System.EventHandler(this.tsmiToolBar_Click);
			// 
			// tsmiStatusBar
			// 
			this.tsmiStatusBar.Checked = true;
			this.tsmiStatusBar.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsmiStatusBar.Name = "tsmiStatusBar";
			this.tsmiStatusBar.Size = new System.Drawing.Size(136, 22);
			this.tsmiStatusBar.Text = "状态栏(&U)";
			this.tsmiStatusBar.CheckedChanged += new System.EventHandler(this.menuStatusBar_CheckedChanged);
			this.tsmiStatusBar.Click += new System.EventHandler(this.tsmiStatusBar_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(133, 6);
			// 
			// tsmiInstances
			// 
			this.tsmiInstances.Checked = true;
			this.tsmiInstances.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsmiInstances.Name = "tsmiInstances";
			this.tsmiInstances.Size = new System.Drawing.Size(136, 22);
			this.tsmiInstances.Text = "实例(&S)";
			this.tsmiInstances.CheckedChanged += new System.EventHandler(this.menuSolution_CheckedChanged);
			this.tsmiInstances.Click += new System.EventHandler(this.tsmiInstances_Click);
			// 
			// tsmiFavorites
			// 
			this.tsmiFavorites.Checked = true;
			this.tsmiFavorites.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsmiFavorites.Name = "tsmiFavorites";
			this.tsmiFavorites.Size = new System.Drawing.Size(136, 22);
			this.tsmiFavorites.Text = "收藏夹(&D)";
			this.tsmiFavorites.CheckedChanged += new System.EventHandler(this.menuFavorites_CheckedChanged);
			this.tsmiFavorites.Click += new System.EventHandler(this.tsmiFavorites_Click);
			// 
			// tsmiErrList
			// 
			this.tsmiErrList.Checked = true;
			this.tsmiErrList.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsmiErrList.Name = "tsmiErrList";
			this.tsmiErrList.Size = new System.Drawing.Size(136, 22);
			this.tsmiErrList.Text = "错误列表(&R)";
			this.tsmiErrList.CheckedChanged += new System.EventHandler(this.menuErrList_CheckedChanged);
			this.tsmiErrList.Click += new System.EventHandler(this.tsmiErrList_Click);
			// 
			// tsmiTool
			// 
			this.tsmiTool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOption,
            this.toolStripSeparator6,
            this.tsmiRSAKey,
            this.tsmiDES,
            this.toolStripSeparator7,
            this.tsmiRandom,
            this.toolStripSeparator8,
            this.tsmiFTPFileTrans,
            this.tsmiFTPFileUp,
            this.toolStripSeparator9,
            this.tsmiDBServer});
			this.tsmiTool.Name = "tsmiTool";
			this.tsmiTool.Size = new System.Drawing.Size(59, 20);
			this.tsmiTool.Text = "工具(&T)";
			// 
			// tsmiOption
			// 
			this.tsmiOption.Name = "tsmiOption";
			this.tsmiOption.Size = new System.Drawing.Size(160, 22);
			this.tsmiOption.Text = "选项(&O)";
			this.tsmiOption.Click += new System.EventHandler(this.tsmiOption_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(157, 6);
			// 
			// tsmiRSAKey
			// 
			this.tsmiRSAKey.Name = "tsmiRSAKey";
			this.tsmiRSAKey.Size = new System.Drawing.Size(160, 22);
			this.tsmiRSAKey.Text = "RSA密钥对(&R)";
			this.tsmiRSAKey.Click += new System.EventHandler(this.tsmiRSAKey_Click);
			// 
			// tsmiDES
			// 
			this.tsmiDES.Name = "tsmiDES";
			this.tsmiDES.Size = new System.Drawing.Size(160, 22);
			this.tsmiDES.Text = "DES加解密(&D)";
			this.tsmiDES.Click += new System.EventHandler(this.tsmiDES_Click);
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(157, 6);
			// 
			// tsmiRandom
			// 
			this.tsmiRandom.Name = "tsmiRandom";
			this.tsmiRandom.Size = new System.Drawing.Size(160, 22);
			this.tsmiRandom.Text = "随机串生成器(&R)";
			this.tsmiRandom.Click += new System.EventHandler(this.tsmiRandom_Click);
			// 
			// toolStripSeparator8
			// 
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new System.Drawing.Size(157, 6);
			// 
			// tsmiFTPFileTrans
			// 
			this.tsmiFTPFileTrans.Name = "tsmiFTPFileTrans";
			this.tsmiFTPFileTrans.Size = new System.Drawing.Size(160, 22);
			this.tsmiFTPFileTrans.Text = "FTP传送(&F)";
			this.tsmiFTPFileTrans.Click += new System.EventHandler(this.tsmiFTPFileTrans_Click);
			// 
			// tsmiFTPFileUp
			// 
			this.tsmiFTPFileUp.Name = "tsmiFTPFileUp";
			this.tsmiFTPFileUp.Size = new System.Drawing.Size(160, 22);
			this.tsmiFTPFileUp.Text = "FTP上传(&P)";
			this.tsmiFTPFileUp.Click += new System.EventHandler(this.tsmiFTPFileUp_Click);
			// 
			// toolStripSeparator9
			// 
			this.toolStripSeparator9.Name = "toolStripSeparator9";
			this.toolStripSeparator9.Size = new System.Drawing.Size(157, 6);
			// 
			// tsmiDBServer
			// 
			this.tsmiDBServer.Name = "tsmiDBServer";
			this.tsmiDBServer.Size = new System.Drawing.Size(160, 22);
			this.tsmiDBServer.Text = "服务器管理(&M)";
			this.tsmiDBServer.Click += new System.EventHandler(this.tsmiDBServer_Click);
			// 
			// tsmiWindow
			// 
			this.tsmiWindow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCloseAll,
            this.tsmiNewApp});
			this.tsmiWindow.Name = "tsmiWindow";
			this.tsmiWindow.Size = new System.Drawing.Size(59, 20);
			this.tsmiWindow.Text = "窗口(&W)";
			// 
			// tsmiCloseAll
			// 
			this.tsmiCloseAll.Name = "tsmiCloseAll";
			this.tsmiCloseAll.Size = new System.Drawing.Size(159, 22);
			this.tsmiCloseAll.Text = "全部关闭(&L)";
			this.tsmiCloseAll.Click += new System.EventHandler(this.tsmiCloseAll_Click);
			// 
			// tsmiNewApp
			// 
			this.tsmiNewApp.Name = "tsmiNewApp";
			this.tsmiNewApp.ShortcutKeys = System.Windows.Forms.Keys.F12;
			this.tsmiNewApp.Size = new System.Drawing.Size(159, 22);
			this.tsmiNewApp.Text = "另开窗口(&N)";
			this.tsmiNewApp.Click += new System.EventHandler(this.tsmiNewApp_Click);
			// 
			// tsmiHelp
			// 
			this.tsmiHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiContents,
            this.tsmiIndex,
            this.tsmiSearch,
            this.toolStripSeparator10,
            this.tsmiSN,
            this.tsmiAbout});
			this.tsmiHelp.Name = "tsmiHelp";
			this.tsmiHelp.Size = new System.Drawing.Size(59, 20);
			this.tsmiHelp.Text = "帮助(&H)";
			// 
			// tsmiContents
			// 
			this.tsmiContents.Name = "tsmiContents";
			this.tsmiContents.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F1)));
			this.tsmiContents.Size = new System.Drawing.Size(159, 22);
			this.tsmiContents.Text = "目录(&C)";
			this.tsmiContents.Click += new System.EventHandler(this.tsmiContents_Click);
			// 
			// tsmiIndex
			// 
			this.tsmiIndex.Name = "tsmiIndex";
			this.tsmiIndex.Size = new System.Drawing.Size(159, 22);
			this.tsmiIndex.Text = "索引(&I)";
			this.tsmiIndex.Click += new System.EventHandler(this.tsmiIndex_Click);
			// 
			// tsmiSearch
			// 
			this.tsmiSearch.Name = "tsmiSearch";
			this.tsmiSearch.Size = new System.Drawing.Size(159, 22);
			this.tsmiSearch.Text = "搜索(&S)";
			this.tsmiSearch.Click += new System.EventHandler(this.tsmiSearch_Click);
			// 
			// toolStripSeparator10
			// 
			this.toolStripSeparator10.Name = "toolStripSeparator10";
			this.toolStripSeparator10.Size = new System.Drawing.Size(156, 6);
			// 
			// tsmiSN
			// 
			this.tsmiSN.Name = "tsmiSN";
			this.tsmiSN.Size = new System.Drawing.Size(159, 22);
			this.tsmiSN.Text = "注册信息(&R)";
			this.tsmiSN.Click += new System.EventHandler(this.tsmiSN_Click);
			// 
			// tsmiAbout
			// 
			this.tsmiAbout.Name = "tsmiAbout";
			this.tsmiAbout.Size = new System.Drawing.Size(159, 22);
			this.tsmiAbout.Text = "关于(&A)";
			this.tsmiAbout.Click += new System.EventHandler(this.tsmiAbout_Click);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNew,
            this.tsbOpen,
            this.tsbSave});
			this.toolStrip1.Location = new System.Drawing.Point(0, 24);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(888, 25);
			this.toolStrip1.TabIndex = 9;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tsbNew
			// 
			this.tsbNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbNew.Image = ((System.Drawing.Image)(resources.GetObject("tsbNew.Image")));
			this.tsbNew.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbNew.Name = "tsbNew";
			this.tsbNew.Size = new System.Drawing.Size(23, 22);
			this.tsbNew.Text = "新建";
			this.tsbNew.Click += new System.EventHandler(this.tsmiNew_Click);
			// 
			// tsbOpen
			// 
			this.tsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpen.Image")));
			this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbOpen.Name = "tsbOpen";
			this.tsbOpen.Size = new System.Drawing.Size(23, 22);
			this.tsbOpen.Text = "打开";
			this.tsbOpen.Click += new System.EventHandler(this.tsmiOpenSql_Click);
			// 
			// tsbSave
			// 
			this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
			this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbSave.Name = "tsbSave";
			this.tsbSave.Size = new System.Drawing.Size(23, 22);
			this.tsbSave.Text = "保存";
			this.tsbSave.Click += new System.EventHandler(this.tsmiSave_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Location = new System.Drawing.Point(0, 403);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
			this.statusStrip1.Size = new System.Drawing.Size(888, 22);
			this.statusStrip1.TabIndex = 10;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(888, 425);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.dockPanel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.IsMdiContainer = true;
			this.Name = "MainForm";
			this.Text = "Apq多数据库管理器";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		public WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem tsmiFile;
		private System.Windows.Forms.ToolStripMenuItem tsmiNew;
		private System.Windows.Forms.ToolStripMenuItem tsmiOpen;
		private System.Windows.Forms.ToolStripMenuItem tsmiOpenSql;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem tsmiSave;
		private System.Windows.Forms.ToolStripMenuItem tsmiSaveAs;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem tsmiExit;
		private System.Windows.Forms.ToolStripMenuItem tsmiEdit;
		private System.Windows.Forms.ToolStripMenuItem tsmiUndo;
		private System.Windows.Forms.ToolStripMenuItem tsmiRedo;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem tsmiCut;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopy;
		private System.Windows.Forms.ToolStripMenuItem tsmiPaste;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem tsmiSelectAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiReverse;
		private System.Windows.Forms.ToolStripMenuItem tsmiView;
		private System.Windows.Forms.ToolStripMenuItem tsmiToolBar;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem tsmiInstances;
		private System.Windows.Forms.ToolStripMenuItem tsmiFavorites;
		private System.Windows.Forms.ToolStripMenuItem tsmiErrList;
		private System.Windows.Forms.ToolStripMenuItem tsmiTool;
		private System.Windows.Forms.ToolStripMenuItem tsmiOption;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem tsmiRSAKey;
		private System.Windows.Forms.ToolStripMenuItem tsmiDES;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripMenuItem tsmiRandom;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
		private System.Windows.Forms.ToolStripMenuItem tsmiFTPFileTrans;
		private System.Windows.Forms.ToolStripMenuItem tsmiFTPFileUp;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
		private System.Windows.Forms.ToolStripMenuItem tsmiDBServer;
		private System.Windows.Forms.ToolStripMenuItem tsmiWindow;
		private System.Windows.Forms.ToolStripMenuItem tsmiCloseAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiNewApp;
		private System.Windows.Forms.ToolStripMenuItem tsmiHelp;
		private System.Windows.Forms.ToolStripMenuItem tsmiContents;
		private System.Windows.Forms.ToolStripMenuItem tsmiIndex;
		private System.Windows.Forms.ToolStripMenuItem tsmiSearch;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
		private System.Windows.Forms.ToolStripMenuItem tsmiSN;
		private System.Windows.Forms.ToolStripMenuItem tsmiAbout;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsbNew;
		private System.Windows.Forms.ToolStripButton tsbOpen;
		private System.Windows.Forms.ToolStripButton tsbSave;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripMenuItem tsmiStatusBar;
	}
}