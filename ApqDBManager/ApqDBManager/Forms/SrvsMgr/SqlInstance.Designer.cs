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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SqlInstance));
			this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
			this.bar3 = new DevExpress.XtraBars.Bar();
			this.bbiExpandAll = new DevExpress.XtraBars.BarButtonItem();
			this.bsiOutInfo = new DevExpress.XtraBars.BarStaticItem();
			this.bsiTest = new DevExpress.XtraBars.BarStaticItem();
			this.bar1 = new DevExpress.XtraBars.Bar();
			this.bbiRefresh = new DevExpress.XtraBars.BarButtonItem();
			this.bbiSaveToDB = new DevExpress.XtraBars.BarButtonItem();
			this.bbiSelectAll = new DevExpress.XtraBars.BarButtonItem();
			this.bbiReverse = new DevExpress.XtraBars.BarButtonItem();
			this.beiStr = new DevExpress.XtraBars.BarEditItem();
			this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.bbiSlts = new DevExpress.XtraBars.BarButtonItem();
			this.bbiDBC = new DevExpress.XtraBars.BarButtonItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.treeList1 = new DevExpress.XtraTreeList.TreeList();
			this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.treeListColumn8 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.luComputer = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
			this.treeListColumn10 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.treeListColumn5 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.luSqlType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
			this.treeListColumn7 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.treeListColumn6 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.treeListColumn12 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.cmTreeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiTestOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiDel = new System.Windows.Forms.ToolStripMenuItem();
			this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.ritePwdD = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.sda = new System.Data.SqlClient.SqlDataAdapter();
			this.scDelete = new System.Data.SqlClient.SqlCommand();
			this.scInsert = new System.Data.SqlClient.SqlCommand();
			this.scSelect = new System.Data.SqlClient.SqlCommand();
			this.scUpdate = new System.Data.SqlClient.SqlCommand();
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.luComputer)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.luSqlType)).BeginInit();
			this.cmTreeMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ritePwdD)).BeginInit();
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
            this.beiStr,
            this.bbiSlts,
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
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiExpandAll),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsiOutInfo),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsiTest)});
			this.bar3.OptionsBar.AllowQuickCustomization = false;
			this.bar3.OptionsBar.DrawDragBorder = false;
			this.bar3.OptionsBar.UseWholeRow = true;
			this.bar3.Text = "Status bar";
			// 
			// bbiExpandAll
			// 
			this.bbiExpandAll.Caption = "全部展开";
			this.bbiExpandAll.Id = 7;
			this.bbiExpandAll.Name = "bbiExpandAll";
			this.bbiExpandAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiExpandAll_ItemClick);
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
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.beiStr, "", false, true, true, 195),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiSlts),
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
			// beiStr
			// 
			this.beiStr.Caption = "barEditItem1";
			this.beiStr.Edit = this.repositoryItemTextEdit1;
			this.beiStr.Id = 3;
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
			this.bbiSlts.Id = 4;
			this.bbiSlts.Name = "bbiSlts";
			this.bbiSlts.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiSlts_ItemClick);
			// 
			// bbiDBC
			// 
			this.bbiDBC.Caption = "数据库连接管理";
			this.bbiDBC.Id = 10;
			this.bbiDBC.Name = "bbiDBC";
			this.bbiDBC.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiDBC_ItemClick);
			// 
			// treeList1
			// 
			this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn2,
            this.treeListColumn8,
            this.treeListColumn10,
            this.treeListColumn4,
            this.treeListColumn5,
            this.treeListColumn7,
            this.treeListColumn6,
            this.treeListColumn12,
            this.treeListColumn1});
			this.treeList1.ContextMenuStrip = this.cmTreeMenu;
			this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeList1.Location = new System.Drawing.Point(0, 25);
			this.treeList1.Name = "treeList1";
			this.treeList1.OptionsBehavior.AllowExpandOnDblClick = false;
			this.treeList1.OptionsBehavior.AutoFocusNewNode = true;
			this.treeList1.OptionsSelection.MultiSelect = true;
			this.treeList1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.luSqlType,
            this.luComputer,
            this.ritePwdD});
			this.treeList1.Size = new System.Drawing.Size(692, 316);
			this.treeList1.StateImageList = this.imageList1;
			this.treeList1.TabIndex = 4;
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
			this.treeListColumn2.FieldName = "SqlName";
			this.treeListColumn2.Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
			this.treeListColumn2.Name = "treeListColumn2";
			this.treeListColumn2.OptionsColumn.AllowMove = false;
			this.treeListColumn2.OptionsColumn.AllowSort = false;
			this.treeListColumn2.Visible = true;
			this.treeListColumn2.VisibleIndex = 0;
			this.treeListColumn2.Width = 55;
			// 
			// treeListColumn8
			// 
			this.treeListColumn8.Caption = "服务器";
			this.treeListColumn8.ColumnEdit = this.luComputer;
			this.treeListColumn8.FieldName = "ComputerID";
			this.treeListColumn8.Name = "treeListColumn8";
			this.treeListColumn8.Visible = true;
			this.treeListColumn8.VisibleIndex = 1;
			// 
			// luComputer
			// 
			this.luComputer.AutoHeight = false;
			this.luComputer.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.luComputer.Name = "luComputer";
			// 
			// treeListColumn10
			// 
			this.treeListColumn10.Caption = "登录名";
			this.treeListColumn10.FieldName = "UserId";
			this.treeListColumn10.Name = "treeListColumn10";
			this.treeListColumn10.Visible = true;
			this.treeListColumn10.VisibleIndex = 2;
			this.treeListColumn10.Width = 56;
			// 
			// treeListColumn4
			// 
			this.treeListColumn4.Caption = "密码";
			this.treeListColumn4.ColumnEdit = this.ritePwdD;
			this.treeListColumn4.FieldName = "PwdD";
			this.treeListColumn4.Name = "treeListColumn4";
			this.treeListColumn4.OptionsColumn.AllowMove = false;
			this.treeListColumn4.OptionsColumn.AllowSort = false;
			this.treeListColumn4.Visible = true;
			this.treeListColumn4.VisibleIndex = 3;
			this.treeListColumn4.Width = 56;
			// 
			// treeListColumn5
			// 
			this.treeListColumn5.Caption = "实例类型";
			this.treeListColumn5.ColumnEdit = this.luSqlType;
			this.treeListColumn5.FieldName = "SqlType";
			this.treeListColumn5.Name = "treeListColumn5";
			this.treeListColumn5.OptionsColumn.AllowMove = false;
			this.treeListColumn5.OptionsColumn.AllowSort = false;
			this.treeListColumn5.Visible = true;
			this.treeListColumn5.VisibleIndex = 4;
			this.treeListColumn5.Width = 56;
			// 
			// luSqlType
			// 
			this.luSqlType.AutoHeight = false;
			this.luSqlType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.luSqlType.Name = "luSqlType";
			// 
			// treeListColumn7
			// 
			this.treeListColumn7.Caption = "ParentID";
			this.treeListColumn7.FieldName = "ParentID";
			this.treeListColumn7.Name = "treeListColumn7";
			this.treeListColumn7.Width = 56;
			// 
			// treeListColumn6
			// 
			this.treeListColumn6.Caption = "实例ID";
			this.treeListColumn6.FieldName = "SqlID";
			this.treeListColumn6.Name = "treeListColumn6";
			this.treeListColumn6.OptionsColumn.ReadOnly = true;
			this.treeListColumn6.Visible = true;
			this.treeListColumn6.VisibleIndex = 5;
			this.treeListColumn6.Width = 56;
			// 
			// treeListColumn12
			// 
			this.treeListColumn12.Caption = "IP";
			this.treeListColumn12.FieldName = "IP";
			this.treeListColumn12.Name = "treeListColumn12";
			this.treeListColumn12.Visible = true;
			this.treeListColumn12.VisibleIndex = 6;
			this.treeListColumn12.Width = 56;
			// 
			// treeListColumn1
			// 
			this.treeListColumn1.Caption = "Sql端口";
			this.treeListColumn1.FieldName = "SqlPort";
			this.treeListColumn1.Name = "treeListColumn1";
			this.treeListColumn1.OptionsColumn.AllowMove = false;
			this.treeListColumn1.OptionsColumn.AllowSort = false;
			this.treeListColumn1.Visible = true;
			this.treeListColumn1.VisibleIndex = 7;
			this.treeListColumn1.Width = 56;
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
			// repositoryItemCheckEdit1
			// 
			this.repositoryItemCheckEdit1.AutoHeight = false;
			this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
			// 
			// ritePwdD
			// 
			this.ritePwdD.AutoHeight = false;
			this.ritePwdD.Name = "ritePwdD";
			this.ritePwdD.PasswordChar = '*';
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
			// SqlInstance
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.ClientSize = new System.Drawing.Size(692, 366);
			this.Controls.Add(this.treeList1);
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
			((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.luComputer)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.luSqlType)).EndInit();
			this.cmTreeMenu.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ritePwdD)).EndInit();
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
		private DevExpress.XtraBars.BarEditItem beiStr;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
		private DevExpress.XtraBars.BarButtonItem bbiSlts;
		private DevExpress.XtraTreeList.TreeList treeList1;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn4;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn5;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit luSqlType;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn7;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn6;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn10;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn12;
		private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
		private DevExpress.XtraBars.BarStaticItem bsiOutInfo;
		private DevExpress.XtraBars.BarStaticItem bsiTest;
		private DevExpress.XtraBars.BarButtonItem bbiExpandAll;
		private DevExpress.XtraBars.BarButtonItem bbiSaveToDB;
		private System.Windows.Forms.ContextMenuStrip cmTreeMenu;
		private System.Windows.Forms.ToolStripMenuItem tsmiTestOpen;
		private System.Windows.Forms.ToolStripMenuItem tsmiAdd;
		private System.Windows.Forms.ToolStripMenuItem tsmiDel;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn8;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit luComputer;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ritePwdD;
		private DevExpress.XtraBars.BarButtonItem bbiDBC;
		private System.Data.SqlClient.SqlDataAdapter sda;
		private System.Data.SqlClient.SqlCommand scDelete;
		private System.Data.SqlClient.SqlCommand scInsert;
		private System.Data.SqlClient.SqlCommand scSelect;
		private System.Data.SqlClient.SqlCommand scUpdate;
		private DevExpress.XtraBars.BarButtonItem bbiRefresh;
		private DevExpress.XtraBars.Bar bar1;
		private System.Windows.Forms.ImageList imageList1;

	}
}