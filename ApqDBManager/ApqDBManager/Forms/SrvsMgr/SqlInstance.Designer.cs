namespace ApqDBManager.Forms.SrvsMgr
{
	partial class SqlInstance
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
			System.Windows.Forms.TreeListViewItemCollection.TreeListViewItemCollectionComparer treeListViewItemCollectionComparer1 = new System.Windows.Forms.TreeListViewItemCollection.TreeListViewItemCollectionComparer();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SqlInstance));
			this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
			this.bar3 = new DevExpress.XtraBars.Bar();
			this.bsiOutInfo = new DevExpress.XtraBars.BarStaticItem();
			this.bsiTest = new DevExpress.XtraBars.BarStaticItem();
			this.bar1 = new DevExpress.XtraBars.Bar();
			this.bbiRefresh = new DevExpress.XtraBars.BarButtonItem();
			this.bbiSaveToDB = new DevExpress.XtraBars.BarButtonItem();
			this.bbiSelectAll = new DevExpress.XtraBars.BarButtonItem();
			this.bbiReverse = new DevExpress.XtraBars.BarButtonItem();
			this.bbiExpandAll = new DevExpress.XtraBars.BarButtonItem();
			this.bbiDBC = new DevExpress.XtraBars.BarButtonItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.cmTreeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiTestOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiDel = new System.Windows.Forms.ToolStripMenuItem();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.sda = new System.Data.SqlClient.SqlDataAdapter();
			this.scDelete = new System.Data.SqlClient.SqlCommand();
			this.scInsert = new System.Data.SqlClient.SqlCommand();
			this.scSelect = new System.Data.SqlClient.SqlCommand();
			this.scUpdate = new System.Data.SqlClient.SqlCommand();
			this.treeListView1 = new System.Windows.Forms.TreeListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.imageList2 = new System.Windows.Forms.ImageList(this.components);
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
			this.cmTreeMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// barManager1
			// 
			this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar3,
            this.bar1});
			this.barManager1.DockControls.Add(this.barDockControlTop);
			this.barManager1.DockControls.Add(this.barDockControlBottom);
			this.barManager1.DockControls.Add(this.barDockControlLeft);
			this.barManager1.DockControls.Add(this.barDockControlRight);
			this.barManager1.Form = this;
			this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiSelectAll,
            this.bbiReverse,
            this.bsiOutInfo,
            this.bsiTest,
            this.bbiExpandAll,
            this.bbiSaveToDB,
            this.bbiDBC,
            this.bbiRefresh});
			this.barManager1.MaxItemId = 13;
			this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
			this.barManager1.StatusBar = this.bar3;
			// 
			// bar3
			// 
			this.bar3.BarName = "Status bar";
			this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
			this.bar3.DockCol = 0;
			this.bar3.DockRow = 0;
			this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
			this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bsiOutInfo),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsiTest)});
			this.bar3.OptionsBar.AllowQuickCustomization = false;
			this.bar3.OptionsBar.DrawDragBorder = false;
			this.bar3.OptionsBar.UseWholeRow = true;
			this.bar3.Text = "Status bar";
			// 
			// bsiOutInfo
			// 
			this.bsiOutInfo.AutoSize = DevExpress.XtraBars.BarStaticItemSize.None;
			this.bsiOutInfo.Id = 5;
			this.bsiOutInfo.Name = "bsiOutInfo";
			this.bsiOutInfo.TextAlignment = System.Drawing.StringAlignment.Near;
			this.bsiOutInfo.Width = 300;
			// 
			// bsiTest
			// 
			this.bsiTest.Id = 6;
			this.bsiTest.Name = "bsiTest";
			this.bsiTest.TextAlignment = System.Drawing.StringAlignment.Near;
			// 
			// bar1
			// 
			this.bar1.BarName = "Custom 4";
			this.bar1.DockCol = 0;
			this.bar1.DockRow = 0;
			this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiRefresh),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiSaveToDB),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiSelectAll, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiReverse),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiExpandAll),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiDBC, true)});
			this.bar1.Text = "Custom 4";
			// 
			// bbiRefresh
			// 
			this.bbiRefresh.Caption = "刷新";
			this.bbiRefresh.Id = 11;
			this.bbiRefresh.Name = "bbiRefresh";
			this.bbiRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiRefresh_ItemClick);
			// 
			// bbiSaveToDB
			// 
			this.bbiSaveToDB.Caption = "保存";
			this.bbiSaveToDB.Hint = "保存";
			this.bbiSaveToDB.Id = 8;
			this.bbiSaveToDB.Name = "bbiSaveToDB";
			this.bbiSaveToDB.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiSaveToDB_ItemClick);
			// 
			// bbiSelectAll
			// 
			this.bbiSelectAll.Caption = "全选";
			this.bbiSelectAll.Id = 0;
			this.bbiSelectAll.Name = "bbiSelectAll";
			this.bbiSelectAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiSelectAll_ItemClick);
			// 
			// bbiReverse
			// 
			this.bbiReverse.Caption = "反选";
			this.bbiReverse.Id = 1;
			this.bbiReverse.Name = "bbiReverse";
			this.bbiReverse.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiReverse_ItemClick);
			// 
			// bbiExpandAll
			// 
			this.bbiExpandAll.Caption = "全部展开(&D)";
			this.bbiExpandAll.Id = 7;
			this.bbiExpandAll.Name = "bbiExpandAll";
			this.bbiExpandAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiExpandAll_ItemClick);
			// 
			// bbiDBC
			// 
			this.bbiDBC.Caption = "数据库连接管理";
			this.bbiDBC.Id = 10;
			this.bbiDBC.Name = "bbiDBC";
			this.bbiDBC.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiDBC_ItemClick);
			// 
			// repositoryItemTextEdit1
			// 
			this.repositoryItemTextEdit1.AutoHeight = false;
			this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
			// 
			// cmTreeMenu
			// 
			this.cmTreeMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTestOpen,
            this.tsmiAdd,
            this.tsmiDel});
			this.cmTreeMenu.Name = "contextMenuStrip1";
			this.cmTreeMenu.Size = new System.Drawing.Size(113, 70);
			// 
			// tsmiTestOpen
			// 
			this.tsmiTestOpen.Name = "tsmiTestOpen";
			this.tsmiTestOpen.Size = new System.Drawing.Size(112, 22);
			this.tsmiTestOpen.Text = "测试(&T)";
			this.tsmiTestOpen.Click += new System.EventHandler(this.tsmiTestOpen_Click);
			// 
			// tsmiAdd
			// 
			this.tsmiAdd.Name = "tsmiAdd";
			this.tsmiAdd.Size = new System.Drawing.Size(112, 22);
			this.tsmiAdd.Text = "添加(&A)";
			this.tsmiAdd.Click += new System.EventHandler(this.tsmiAdd_Click);
			// 
			// tsmiDel
			// 
			this.tsmiDel.Name = "tsmiDel";
			this.tsmiDel.Size = new System.Drawing.Size(112, 22);
			this.tsmiDel.Text = "删除(&D)";
			this.tsmiDel.Click += new System.EventHandler(this.tsmiDel_Click);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "UnChecked.png");
			this.imageList1.Images.SetKeyName(1, "Checked.png");
			this.imageList1.Images.SetKeyName(2, "Indeterminate.png");
			// 
			// sda
			// 
			this.sda.DeleteCommand = this.scDelete;
			this.sda.InsertCommand = this.scInsert;
			this.sda.SelectCommand = this.scSelect;
			this.sda.UpdateCommand = this.scUpdate;
			// 
			// scSelect
			// 
			this.scSelect.CommandText = "\r\nSELECT * FROM dic_SQLInstanceType;\r\nSELECT * FROM dic_IPType;\r\nSELECT * FROM DB" +
				"Server;\r\nSELECT * FROM SQLInstance;\r\nSELECT * FROM DBC;\r\nSELECT * FROM DBServerI" +
				"P;";
			// 
			// treeListView1
			// 
			this.treeListView1.CheckBoxes = System.Windows.Forms.CheckBoxesTypes.Simple;
			this.treeListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
			treeListViewItemCollectionComparer1.Column = 0;
			treeListViewItemCollectionComparer1.SortOrder = System.Windows.Forms.SortOrder.Ascending;
			this.treeListView1.Comparer = treeListViewItemCollectionComparer1;
			this.treeListView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeListView1.LabelEdit = true;
			this.treeListView1.Location = new System.Drawing.Point(0, 21);
			this.treeListView1.Name = "treeListView1";
			this.treeListView1.Size = new System.Drawing.Size(692, 320);
			this.treeListView1.SmallImageList = this.imageList2;
			this.treeListView1.TabIndex = 5;
			this.treeListView1.UseCompatibleStateImageBehavior = false;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "名称";
			this.columnHeader1.Width = 153;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "服务器";
			this.columnHeader2.Width = 106;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "登录名";
			this.columnHeader3.Width = 76;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "密码";
			this.columnHeader4.Width = 70;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "IP";
			this.columnHeader5.Width = 84;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "SQL端口";
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "编号";
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "上级编号";
			this.columnHeader8.Width = 72;
			// 
			// imageList2
			// 
			this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
			this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList2.Images.SetKeyName(0, "");
			this.imageList2.Images.SetKeyName(1, "");
			this.imageList2.Images.SetKeyName(2, "");
			this.imageList2.Images.SetKeyName(3, "");
			// 
			// SqlInstance
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.ClientSize = new System.Drawing.Size(692, 366);
			this.Controls.Add(this.treeListView1);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "SqlInstance";
			this.TabText = "实例管理";
			this.Text = "实例管理";
			this.Load += new System.EventHandler(this.SqlInstance_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SqlInstance_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
			this.cmTreeMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraBars.BarManager barManager1;
		private DevExpress.XtraBars.Bar bar3;
		private DevExpress.XtraBars.BarDockControl barDockControlTop;
		private DevExpress.XtraBars.BarDockControl barDockControlBottom;
		private DevExpress.XtraBars.BarDockControl barDockControlLeft;
		private DevExpress.XtraBars.BarDockControl barDockControlRight;
		private DevExpress.XtraBars.BarButtonItem bbiSelectAll;
		private DevExpress.XtraBars.BarButtonItem bbiReverse;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
		private DevExpress.XtraBars.BarStaticItem bsiOutInfo;
		private DevExpress.XtraBars.BarStaticItem bsiTest;
		private DevExpress.XtraBars.BarButtonItem bbiExpandAll;
		private DevExpress.XtraBars.BarButtonItem bbiSaveToDB;
		private System.Windows.Forms.ContextMenuStrip cmTreeMenu;
		private System.Windows.Forms.ToolStripMenuItem tsmiTestOpen;
		private System.Windows.Forms.ToolStripMenuItem tsmiAdd;
		private System.Windows.Forms.ToolStripMenuItem tsmiDel;
		private DevExpress.XtraBars.BarButtonItem bbiDBC;
		private System.Data.SqlClient.SqlDataAdapter sda;
		private System.Data.SqlClient.SqlCommand scDelete;
		private System.Data.SqlClient.SqlCommand scInsert;
		private System.Data.SqlClient.SqlCommand scSelect;
		private System.Data.SqlClient.SqlCommand scUpdate;
		private DevExpress.XtraBars.BarButtonItem bbiRefresh;
		private DevExpress.XtraBars.Bar bar1;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.TreeListView treeListView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ImageList imageList2;

	}
}