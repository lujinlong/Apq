namespace ApqDBManager.XmlServersCfg.Forms
{
	partial class ServersEditor
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServersEditor));
			this.treeList1 = new DevExpress.XtraTreeList.TreeList();
			this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.treeListColumn8 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.treeListColumn5 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.luType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
			this.treeListColumn7 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.treeListColumn6 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.treeListColumn9 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.treeListColumn10 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.treeListColumn11 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.treeListColumn12 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.treeListColumn13 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiTestOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiDel = new System.Windows.Forms.ToolStripMenuItem();
			this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
			this.bar3 = new DevExpress.XtraBars.Bar();
			this.bbiSelectAll = new DevExpress.XtraBars.BarButtonItem();
			this.bbiReverse = new DevExpress.XtraBars.BarButtonItem();
			this.beiStr = new DevExpress.XtraBars.BarEditItem();
			this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.bbiSlts = new DevExpress.XtraBars.BarButtonItem();
			this.bbiExpandAll = new DevExpress.XtraBars.BarButtonItem();
			this.bar2 = new DevExpress.XtraBars.Bar();
			this.bbiLoadFromDB = new DevExpress.XtraBars.BarButtonItem();
			this.bbiSaveToDB = new DevExpress.XtraBars.BarButtonItem();
			this.bar4 = new DevExpress.XtraBars.Bar();
			this.bsiOutInfo = new DevExpress.XtraBars.BarStaticItem();
			this.bsiTest = new DevExpress.XtraBars.BarStaticItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.bciArea = new DevExpress.XtraBars.BarCheckItem();
			this.bciServer = new DevExpress.XtraBars.BarCheckItem();
			this.Servers = new ApqDBManager.XSD.Servers();
			this.tsmiFTPTest = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.luType)).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Servers)).BeginInit();
			this.SuspendLayout();
			// 
			// treeList1
			// 
			this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn2,
            this.treeListColumn8,
            this.treeListColumn1,
            this.treeListColumn3,
            this.treeListColumn4,
            this.treeListColumn5,
            this.treeListColumn7,
            this.treeListColumn6,
            this.treeListColumn9,
            this.treeListColumn10,
            this.treeListColumn11,
            this.treeListColumn12,
            this.treeListColumn13});
			this.treeList1.ContextMenuStrip = this.contextMenuStrip1;
			this.treeList1.DataMember = "dtServers";
			this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeList1.Location = new System.Drawing.Point(0, 50);
			this.treeList1.Name = "treeList1";
			this.treeList1.OptionsBehavior.AllowExpandOnDblClick = false;
			this.treeList1.OptionsBehavior.AutoFocusNewNode = true;
			this.treeList1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.luType});
			this.treeList1.Size = new System.Drawing.Size(692, 291);
			this.treeList1.StateImageList = this.imageList1;
			this.treeList1.TabIndex = 0;
			this.treeList1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeList1_KeyDown);
			this.treeList1.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeList1_FocusedNodeChanged);
			this.treeList1.EditorKeyUp += new System.Windows.Forms.KeyEventHandler(this.treeList1_EditorKeyUp);
			this.treeList1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeList1_MouseClick);
			this.treeList1.GetStateImage += new DevExpress.XtraTreeList.GetStateImageEventHandler(this.treeList1_GetStateImage);
			this.treeList1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.treeList1_KeyUp);
			// 
			// treeListColumn2
			// 
			this.treeListColumn2.Caption = "名称";
			this.treeListColumn2.FieldName = "Name";
			this.treeListColumn2.Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
			this.treeListColumn2.Name = "treeListColumn2";
			this.treeListColumn2.OptionsColumn.AllowMove = false;
			this.treeListColumn2.OptionsColumn.AllowSort = false;
			this.treeListColumn2.Visible = true;
			this.treeListColumn2.VisibleIndex = 0;
			// 
			// treeListColumn8
			// 
			this.treeListColumn8.Caption = "IPWan1";
			this.treeListColumn8.FieldName = "IPWan1";
			this.treeListColumn8.Name = "treeListColumn8";
			this.treeListColumn8.OptionsColumn.AllowMove = false;
			this.treeListColumn8.OptionsColumn.AllowSort = false;
			this.treeListColumn8.Visible = true;
			this.treeListColumn8.VisibleIndex = 7;
			// 
			// treeListColumn1
			// 
			this.treeListColumn1.Caption = "Sql端口";
			this.treeListColumn1.FieldName = "SqlPort";
			this.treeListColumn1.Name = "treeListColumn1";
			this.treeListColumn1.OptionsColumn.AllowMove = false;
			this.treeListColumn1.OptionsColumn.AllowSort = false;
			this.treeListColumn1.Visible = true;
			this.treeListColumn1.VisibleIndex = 1;
			// 
			// treeListColumn3
			// 
			this.treeListColumn3.Caption = "用户";
			this.treeListColumn3.FieldName = "UID";
			this.treeListColumn3.Name = "treeListColumn3";
			this.treeListColumn3.OptionsColumn.AllowMove = false;
			this.treeListColumn3.OptionsColumn.AllowSort = false;
			this.treeListColumn3.Visible = true;
			this.treeListColumn3.VisibleIndex = 2;
			// 
			// treeListColumn4
			// 
			this.treeListColumn4.Caption = "密码";
			this.treeListColumn4.FieldName = "PwdD";
			this.treeListColumn4.Name = "treeListColumn4";
			this.treeListColumn4.OptionsColumn.AllowMove = false;
			this.treeListColumn4.OptionsColumn.AllowSort = false;
			this.treeListColumn4.Visible = true;
			this.treeListColumn4.VisibleIndex = 3;
			// 
			// treeListColumn5
			// 
			this.treeListColumn5.Caption = "类型";
			this.treeListColumn5.ColumnEdit = this.luType;
			this.treeListColumn5.FieldName = "Type";
			this.treeListColumn5.Name = "treeListColumn5";
			this.treeListColumn5.OptionsColumn.AllowMove = false;
			this.treeListColumn5.OptionsColumn.AllowSort = false;
			this.treeListColumn5.Visible = true;
			this.treeListColumn5.VisibleIndex = 4;
			// 
			// luType
			// 
			this.luType.AutoHeight = false;
			this.luType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.luType.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Type", "Type"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TypeCaption", "类型")});
			this.luType.DisplayMember = "TypeCaption";
			this.luType.Name = "luType";
			this.luType.ValueMember = "Type";
			// 
			// treeListColumn7
			// 
			this.treeListColumn7.Caption = "ParentID";
			this.treeListColumn7.FieldName = "ParentID";
			this.treeListColumn7.Name = "treeListColumn7";
			this.treeListColumn7.Visible = true;
			this.treeListColumn7.VisibleIndex = 6;
			// 
			// treeListColumn6
			// 
			this.treeListColumn6.Caption = "ID";
			this.treeListColumn6.FieldName = "ID";
			this.treeListColumn6.Name = "treeListColumn6";
			this.treeListColumn6.OptionsColumn.ReadOnly = true;
			this.treeListColumn6.Visible = true;
			this.treeListColumn6.VisibleIndex = 5;
			// 
			// treeListColumn9
			// 
			this.treeListColumn9.Caption = "FTP端口";
			this.treeListColumn9.FieldName = "FTPPort";
			this.treeListColumn9.Name = "treeListColumn9";
			this.treeListColumn9.Visible = true;
			this.treeListColumn9.VisibleIndex = 8;
			// 
			// treeListColumn10
			// 
			this.treeListColumn10.Caption = "FTP用户名";
			this.treeListColumn10.FieldName = "FTPU";
			this.treeListColumn10.Name = "treeListColumn10";
			this.treeListColumn10.Visible = true;
			this.treeListColumn10.VisibleIndex = 9;
			// 
			// treeListColumn11
			// 
			this.treeListColumn11.Caption = "FTP密码";
			this.treeListColumn11.FieldName = "FTPPD";
			this.treeListColumn11.Name = "treeListColumn11";
			this.treeListColumn11.Visible = true;
			this.treeListColumn11.VisibleIndex = 10;
			// 
			// treeListColumn12
			// 
			this.treeListColumn12.Caption = "IPWan2";
			this.treeListColumn12.FieldName = "IPWan2";
			this.treeListColumn12.Name = "treeListColumn12";
			this.treeListColumn12.Visible = true;
			this.treeListColumn12.VisibleIndex = 11;
			// 
			// treeListColumn13
			// 
			this.treeListColumn13.Caption = "IPLan";
			this.treeListColumn13.FieldName = "IPLan";
			this.treeListColumn13.Name = "treeListColumn13";
			this.treeListColumn13.Visible = true;
			this.treeListColumn13.VisibleIndex = 12;
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTestOpen,
            this.tsmiAdd,
            this.tsmiDel,
            this.tsmiFTPTest});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(153, 114);
			// 
			// tsmiTestOpen
			// 
			this.tsmiTestOpen.Name = "tsmiTestOpen";
			this.tsmiTestOpen.Size = new System.Drawing.Size(152, 22);
			this.tsmiTestOpen.Text = "测试(&T)";
			this.tsmiTestOpen.Click += new System.EventHandler(this.tsmiTestOpen_Click);
			// 
			// tsmiAdd
			// 
			this.tsmiAdd.Name = "tsmiAdd";
			this.tsmiAdd.Size = new System.Drawing.Size(152, 22);
			this.tsmiAdd.Text = "添加(&A)";
			this.tsmiAdd.Click += new System.EventHandler(this.tsmiAdd_Click);
			// 
			// tsmiDel
			// 
			this.tsmiDel.Name = "tsmiDel";
			this.tsmiDel.Size = new System.Drawing.Size(152, 22);
			this.tsmiDel.Text = "删除(&D)";
			this.tsmiDel.Click += new System.EventHandler(this.tsmiDel_Click);
			// 
			// repositoryItemCheckEdit1
			// 
			this.repositoryItemCheckEdit1.AutoHeight = false;
			this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "UnChecked.png");
			this.imageList1.Images.SetKeyName(1, "Checked.png");
			this.imageList1.Images.SetKeyName(2, "Indeterminate.png");
			// 
			// barManager1
			// 
			this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar3,
            this.bar2,
            this.bar4});
			this.barManager1.DockControls.Add(this.barDockControlTop);
			this.barManager1.DockControls.Add(this.barDockControlBottom);
			this.barManager1.DockControls.Add(this.barDockControlLeft);
			this.barManager1.DockControls.Add(this.barDockControlRight);
			this.barManager1.Form = this;
			this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bciArea,
            this.bciServer,
            this.bbiExpandAll,
            this.bbiSelectAll,
            this.bbiReverse,
            this.bbiLoadFromDB,
            this.beiStr,
            this.bbiSlts,
            this.bbiSaveToDB,
            this.bsiOutInfo,
            this.bsiTest});
			this.barManager1.MaxItemId = 22;
			this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
			this.barManager1.StatusBar = this.bar4;
			// 
			// bar3
			// 
			this.bar3.BarName = "Custom 4";
			this.bar3.DockCol = 0;
			this.bar3.DockRow = 1;
			this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiSelectAll),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiReverse),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.beiStr, "", false, true, true, 216),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiSlts),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiExpandAll)});
			this.bar3.Text = "Custom 4";
			// 
			// bbiSelectAll
			// 
			this.bbiSelectAll.Caption = "全选";
			this.bbiSelectAll.Id = 10;
			this.bbiSelectAll.Name = "bbiSelectAll";
			this.bbiSelectAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiSelectAll_ItemClick);
			// 
			// bbiReverse
			// 
			this.bbiReverse.Caption = "反选";
			this.bbiReverse.Id = 11;
			this.bbiReverse.Name = "bbiReverse";
			this.bbiReverse.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiReverse_ItemClick);
			// 
			// beiStr
			// 
			this.beiStr.Caption = "beiStr";
			this.beiStr.Edit = this.repositoryItemTextEdit1;
			this.beiStr.Id = 14;
			this.beiStr.Name = "beiStr";
			// 
			// repositoryItemTextEdit1
			// 
			this.repositoryItemTextEdit1.AutoHeight = false;
			this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
			// 
			// bbiSlts
			// 
			this.bbiSlts.Caption = "设置多行";
			this.bbiSlts.Id = 16;
			this.bbiSlts.Name = "bbiSlts";
			this.bbiSlts.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiSlts_ItemClick);
			// 
			// bbiExpandAll
			// 
			this.bbiExpandAll.Caption = "全部展开(&D)";
			this.bbiExpandAll.Id = 7;
			this.bbiExpandAll.Name = "bbiExpandAll";
			this.bbiExpandAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiExpandAll_ItemClick);
			// 
			// bar2
			// 
			this.bar2.BarName = "Custom 5";
			this.bar2.DockCol = 0;
			this.bar2.DockRow = 0;
			this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.bar2.FloatLocation = new System.Drawing.Point(318, 189);
			this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiLoadFromDB),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiSaveToDB)});
			this.bar2.Text = "Custom 5";
			// 
			// bbiLoadFromDB
			// 
			this.bbiLoadFromDB.Caption = "从数据库加载";
			this.bbiLoadFromDB.Id = 12;
			this.bbiLoadFromDB.Name = "bbiLoadFromDB";
			this.bbiLoadFromDB.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiLoadFromDB_ItemClick);
			// 
			// bbiSaveToDB
			// 
			this.bbiSaveToDB.Caption = "保存到数据库";
			this.bbiSaveToDB.Id = 17;
			this.bbiSaveToDB.Name = "bbiSaveToDB";
			this.bbiSaveToDB.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiSaveToDB_ItemClick);
			// 
			// bar4
			// 
			this.bar4.BarName = "Custom 6";
			this.bar4.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
			this.bar4.DockCol = 0;
			this.bar4.DockRow = 0;
			this.bar4.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
			this.bar4.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bsiOutInfo),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsiTest)});
			this.bar4.OptionsBar.AllowQuickCustomization = false;
			this.bar4.OptionsBar.DrawDragBorder = false;
			this.bar4.OptionsBar.UseWholeRow = true;
			this.bar4.Text = "Custom 6";
			// 
			// bsiOutInfo
			// 
			this.bsiOutInfo.AutoSize = DevExpress.XtraBars.BarStaticItemSize.None;
			this.bsiOutInfo.Id = 18;
			this.bsiOutInfo.Name = "bsiOutInfo";
			this.bsiOutInfo.TextAlignment = System.Drawing.StringAlignment.Near;
			this.bsiOutInfo.Width = 300;
			// 
			// bsiTest
			// 
			this.bsiTest.Id = 20;
			this.bsiTest.Name = "bsiTest";
			this.bsiTest.TextAlignment = System.Drawing.StringAlignment.Near;
			// 
			// bciArea
			// 
			this.bciArea.Caption = "区库(&E)";
			this.bciArea.Id = 0;
			this.bciArea.Name = "bciArea";
			// 
			// bciServer
			// 
			this.bciServer.Caption = "游戏库(&G)";
			this.bciServer.Id = 1;
			this.bciServer.Name = "bciServer";
			// 
			// Servers
			// 
			this.Servers.DataSetName = "Servers";
			this.Servers.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// tsmiFTPTest
			// 
			this.tsmiFTPTest.Name = "tsmiFTPTest";
			this.tsmiFTPTest.Size = new System.Drawing.Size(152, 22);
			this.tsmiFTPTest.Text = "FTP测试(&F)";
			this.tsmiFTPTest.Click += new System.EventHandler(this.tsmiFTPTest_Click);
			// 
			// ServersEditor
			// 
			this.ClientSize = new System.Drawing.Size(692, 366);
			this.Controls.Add(this.treeList1);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.HideOnClose = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ServersEditor";
			this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
			this.TabText = "未加载文件";
			this.Text = "未加载文件";
			this.Load += new System.EventHandler(this.ServersEditor_Load);
			((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.luType)).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Servers)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private DevExpress.XtraTreeList.TreeList treeList1;
		private DevExpress.XtraBars.BarManager barManager1;
		private DevExpress.XtraBars.BarDockControl barDockControlTop;
		private DevExpress.XtraBars.BarDockControl barDockControlBottom;
		private DevExpress.XtraBars.BarDockControl barDockControlLeft;
		private DevExpress.XtraBars.BarDockControl barDockControlRight;
		private DevExpress.XtraBars.BarCheckItem bciArea;
		private DevExpress.XtraBars.BarCheckItem bciServer;
		private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
		private System.Windows.Forms.ImageList imageList1;
		private DevExpress.XtraBars.BarButtonItem bbiExpandAll;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem tsmiAdd;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn4;
		private DevExpress.XtraBars.Bar bar3;
		private DevExpress.XtraBars.BarButtonItem bbiReverse;
		private DevExpress.XtraBars.BarButtonItem bbiSelectAll;
		private DevExpress.XtraBars.Bar bar2;
		private DevExpress.XtraBars.BarButtonItem bbiLoadFromDB;
		private ApqDBManager.XSD.Servers Servers;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn5;
		private System.Windows.Forms.ToolStripMenuItem tsmiDel;
		private DevExpress.XtraBars.BarEditItem beiStr;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
		private DevExpress.XtraBars.BarButtonItem bbiSlts;
		private DevExpress.XtraBars.BarButtonItem bbiSaveToDB;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn7;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn6;
		private DevExpress.XtraBars.Bar bar4;
		private DevExpress.XtraBars.BarStaticItem bsiOutInfo;
		private System.Windows.Forms.ToolStripMenuItem tsmiTestOpen;
		private DevExpress.XtraBars.BarStaticItem bsiTest;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit luType;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn8;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn9;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn10;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn11;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn12;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn13;
		private System.Windows.Forms.ToolStripMenuItem tsmiFTPTest;

	}
}