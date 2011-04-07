namespace ApqDBManager.Forms.SrvsMgr
{
	partial class DBC
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBC));
			this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
			this.bar3 = new DevExpress.XtraBars.Bar();
			this.bsiOutInfo = new DevExpress.XtraBars.BarStaticItem();
			this.bsiTest = new DevExpress.XtraBars.BarStaticItem();
			this.bar1 = new DevExpress.XtraBars.Bar();
			this.bbiRefresh = new DevExpress.XtraBars.BarButtonItem();
			this.bbiSave = new DevExpress.XtraBars.BarButtonItem();
			this.bbiSelectAll = new DevExpress.XtraBars.BarButtonItem();
			this.beiStr = new DevExpress.XtraBars.BarEditItem();
			this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.bbiSlts = new DevExpress.XtraBars.BarButtonItem();
			this.bsiCreateCSFile = new DevExpress.XtraBars.BarSubItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.gridControl1 = new DevExpress.XtraGrid.GridControl();
			this.cmGridMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiTestOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiDel = new System.Windows.Forms.ToolStripMenuItem();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.luComputer = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
			this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.luSqlInstance = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
			this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.luDBCType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
			this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.ribePwdD = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.riceUseTrusted = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.ribeName = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.sda = new System.Data.SqlClient.SqlDataAdapter();
			this.scDelete = new System.Data.SqlClient.SqlCommand();
			this.scInsert = new System.Data.SqlClient.SqlCommand();
			this.scSelect = new System.Data.SqlClient.SqlCommand();
			this.scUpdate = new System.Data.SqlClient.SqlCommand();
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
			this.cmGridMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.luComputer)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.luSqlInstance)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.luDBCType)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ribePwdD)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.riceUseTrusted)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ribeName)).BeginInit();
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
            this.bsiOutInfo,
            this.bsiTest,
            this.bbiSelectAll,
            this.beiStr,
            this.bbiSlts,
            this.bbiRefresh,
            this.bbiSave,
            this.bsiCreateCSFile});
			this.barManager1.MaxItemId = 11;
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
			this.bsiOutInfo.Id = 0;
			this.bsiOutInfo.Name = "bsiOutInfo";
			this.bsiOutInfo.TextAlignment = System.Drawing.StringAlignment.Near;
			this.bsiOutInfo.Width = 300;
			// 
			// bsiTest
			// 
			this.bsiTest.Id = 1;
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
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiSave),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiSelectAll, true),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.beiStr, "", false, true, true, 196),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiSlts),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsiCreateCSFile, true)});
			this.bar1.Text = "Custom 4";
			// 
			// bbiRefresh
			// 
			this.bbiRefresh.Caption = "刷新";
			this.bbiRefresh.Id = 6;
			this.bbiRefresh.Name = "bbiRefresh";
			this.bbiRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiRefresh_ItemClick);
			// 
			// bbiSave
			// 
			this.bbiSave.Caption = "保存";
			this.bbiSave.Hint = "保存";
			this.bbiSave.Id = 7;
			this.bbiSave.Name = "bbiSave";
			this.bbiSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiSave_ItemClick);
			// 
			// bbiSelectAll
			// 
			this.bbiSelectAll.Caption = "全选";
			this.bbiSelectAll.Id = 2;
			this.bbiSelectAll.Name = "bbiSelectAll";
			this.bbiSelectAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiSelectAll_ItemClick);
			// 
			// beiStr
			// 
			this.beiStr.Caption = "barEditItem1";
			this.beiStr.Edit = this.repositoryItemTextEdit1;
			this.beiStr.Id = 4;
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
			this.bbiSlts.Id = 5;
			this.bbiSlts.Name = "bbiSlts";
			this.bbiSlts.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiSlts_ItemClick);
			// 
			// bsiCreateCSFile
			// 
			this.bsiCreateCSFile.Caption = "生成文件";
			this.bsiCreateCSFile.Id = 9;
			this.bsiCreateCSFile.Name = "bsiCreateCSFile";
			// 
			// gridControl1
			// 
			this.gridControl1.ContextMenuStrip = this.cmGridMenu;
			this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControl1.Location = new System.Drawing.Point(0, 25);
			this.gridControl1.MainView = this.gridView1;
			this.gridControl1.Name = "gridControl1";
			this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ribePwdD,
            this.ribeName,
            this.riceUseTrusted,
            this.luComputer,
            this.luSqlInstance,
            this.luDBCType});
			this.gridControl1.Size = new System.Drawing.Size(692, 316);
			this.gridControl1.TabIndex = 5;
			this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
			// 
			// cmGridMenu
			// 
			this.cmGridMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTestOpen,
            this.tsmiDel});
			this.cmGridMenu.Name = "contextMenuStrip1";
			this.cmGridMenu.Size = new System.Drawing.Size(113, 48);
			// 
			// tsmiTestOpen
			// 
			this.tsmiTestOpen.Name = "tsmiTestOpen";
			this.tsmiTestOpen.Size = new System.Drawing.Size(112, 22);
			this.tsmiTestOpen.Text = "测试(&T)";
			this.tsmiTestOpen.Click += new System.EventHandler(this.tsmiTestOpen_Click);
			// 
			// tsmiDel
			// 
			this.tsmiDel.Name = "tsmiDel";
			this.tsmiDel.Size = new System.Drawing.Size(112, 22);
			this.tsmiDel.Text = "删除(&D)";
			this.tsmiDel.Click += new System.EventHandler(this.tsmiDel_Click);
			// 
			// gridView1
			// 
			this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn6,
            this.gridColumn2,
            this.gridColumn1,
            this.gridColumn9,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn8,
            this.gridColumn7,
            this.gridColumn5});
			this.gridView1.GridControl = this.gridControl1;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
			this.gridView1.OptionsSelection.MultiSelect = true;
			this.gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
			this.gridView1.OptionsView.ShowGroupPanel = false;
			// 
			// gridColumn6
			// 
			this.gridColumn6.Caption = "服务器";
			this.gridColumn6.ColumnEdit = this.luComputer;
			this.gridColumn6.FieldName = "ComputerID";
			this.gridColumn6.Name = "gridColumn6";
			this.gridColumn6.OptionsColumn.ReadOnly = true;
			this.gridColumn6.Visible = true;
			this.gridColumn6.VisibleIndex = 0;
			// 
			// luComputer
			// 
			this.luComputer.AutoHeight = false;
			this.luComputer.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.luComputer.Name = "luComputer";
			// 
			// gridColumn2
			// 
			this.gridColumn2.Caption = "实例名";
			this.gridColumn2.ColumnEdit = this.luSqlInstance;
			this.gridColumn2.FieldName = "SqlID";
			this.gridColumn2.Name = "gridColumn2";
			this.gridColumn2.Visible = true;
			this.gridColumn2.VisibleIndex = 1;
			// 
			// luSqlInstance
			// 
			this.luSqlInstance.AutoHeight = false;
			this.luSqlInstance.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.luSqlInstance.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SqlID", "实例"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SqlName", "实例名")});
			this.luSqlInstance.Name = "luSqlInstance";
			// 
			// gridColumn1
			// 
			this.gridColumn1.Caption = "数据库类型";
			this.gridColumn1.ColumnEdit = this.luDBCType;
			this.gridColumn1.FieldName = "DBCType";
			this.gridColumn1.Name = "gridColumn1";
			this.gridColumn1.Visible = true;
			this.gridColumn1.VisibleIndex = 2;
			// 
			// luDBCType
			// 
			this.luDBCType.AutoHeight = false;
			this.luDBCType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.luDBCType.Name = "luDBCType";
			// 
			// gridColumn9
			// 
			this.gridColumn9.Caption = "数据库名";
			this.gridColumn9.FieldName = "DBName";
			this.gridColumn9.Name = "gridColumn9";
			this.gridColumn9.Visible = true;
			this.gridColumn9.VisibleIndex = 3;
			// 
			// gridColumn3
			// 
			this.gridColumn3.Caption = "登录名";
			this.gridColumn3.FieldName = "UserId";
			this.gridColumn3.Name = "gridColumn3";
			this.gridColumn3.Visible = true;
			this.gridColumn3.VisibleIndex = 4;
			// 
			// gridColumn4
			// 
			this.gridColumn4.Caption = "密码";
			this.gridColumn4.ColumnEdit = this.ribePwdD;
			this.gridColumn4.FieldName = "PwdD";
			this.gridColumn4.Name = "gridColumn4";
			this.gridColumn4.Visible = true;
			this.gridColumn4.VisibleIndex = 5;
			// 
			// ribePwdD
			// 
			this.ribePwdD.AutoHeight = false;
			this.ribePwdD.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinRight)});
			this.ribePwdD.Name = "ribePwdD";
			this.ribePwdD.PasswordChar = '*';
			// 
			// gridColumn8
			// 
			this.gridColumn8.Caption = "信任连接";
			this.gridColumn8.ColumnEdit = this.riceUseTrusted;
			this.gridColumn8.FieldName = "UseTrusted";
			this.gridColumn8.Name = "gridColumn8";
			this.gridColumn8.Visible = true;
			this.gridColumn8.VisibleIndex = 6;
			// 
			// riceUseTrusted
			// 
			this.riceUseTrusted.AutoHeight = false;
			this.riceUseTrusted.Name = "riceUseTrusted";
			// 
			// gridColumn7
			// 
			this.gridColumn7.Caption = "镜像";
			this.gridColumn7.FieldName = "Mirror";
			this.gridColumn7.Name = "gridColumn7";
			this.gridColumn7.Visible = true;
			this.gridColumn7.VisibleIndex = 7;
			// 
			// gridColumn5
			// 
			this.gridColumn5.Caption = "选项";
			this.gridColumn5.FieldName = "Option";
			this.gridColumn5.Name = "gridColumn5";
			this.gridColumn5.Visible = true;
			this.gridColumn5.VisibleIndex = 8;
			// 
			// ribeName
			// 
			this.ribeName.AutoHeight = false;
			this.ribeName.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Undo)});
			this.ribeName.Name = "ribeName";
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
			// DBC
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.ClientSize = new System.Drawing.Size(692, 366);
			this.Controls.Add(this.gridControl1);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "DBC";
			this.TabText = "数据库连接管理";
			this.Text = "数据库连接管理";
			this.Load += new System.EventHandler(this.DBC_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DBC_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
			this.cmGridMenu.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.luComputer)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.luSqlInstance)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.luDBCType)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ribePwdD)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.riceUseTrusted)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ribeName)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraBars.BarManager barManager1;
		private DevExpress.XtraBars.Bar bar3;
		private DevExpress.XtraBars.BarDockControl barDockControlTop;
		private DevExpress.XtraBars.BarDockControl barDockControlBottom;
		private DevExpress.XtraBars.BarDockControl barDockControlLeft;
		private DevExpress.XtraBars.BarDockControl barDockControlRight;
		private DevExpress.XtraBars.BarStaticItem bsiOutInfo;
		private DevExpress.XtraBars.BarStaticItem bsiTest;
		private DevExpress.XtraBars.BarButtonItem bbiSelectAll;
		private DevExpress.XtraBars.BarEditItem beiStr;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
		private DevExpress.XtraBars.BarButtonItem bbiSlts;
		private DevExpress.XtraGrid.GridControl gridControl1;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit ribeName;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
		private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit riceUseTrusted;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit ribePwdD;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
		private DevExpress.XtraBars.Bar bar1;
		private System.Data.SqlClient.SqlDataAdapter sda;
		private System.Data.SqlClient.SqlCommand scDelete;
		private System.Data.SqlClient.SqlCommand scInsert;
		private System.Data.SqlClient.SqlCommand scSelect;
		private System.Data.SqlClient.SqlCommand scUpdate;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit luComputer;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit luSqlInstance;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit luDBCType;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
		private DevExpress.XtraBars.BarButtonItem bbiRefresh;
		private DevExpress.XtraBars.BarButtonItem bbiSave;
		private System.Windows.Forms.ContextMenuStrip cmGridMenu;
		private System.Windows.Forms.ToolStripMenuItem tsmiTestOpen;
		private System.Windows.Forms.ToolStripMenuItem tsmiDel;
		private DevExpress.XtraBars.BarSubItem bsiCreateCSFile;

	}
}