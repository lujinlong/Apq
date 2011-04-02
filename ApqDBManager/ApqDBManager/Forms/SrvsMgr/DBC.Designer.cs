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
			this.bar2 = new DevExpress.XtraBars.Bar();
			this.bbiSelectAll = new DevExpress.XtraBars.BarButtonItem();
			this.bbiReverse = new DevExpress.XtraBars.BarButtonItem();
			this.beiStr = new DevExpress.XtraBars.BarEditItem();
			this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.bbiSlts = new DevExpress.XtraBars.BarButtonItem();
			this.bar3 = new DevExpress.XtraBars.Bar();
			this.bsiOutInfo = new DevExpress.XtraBars.BarStaticItem();
			this.bsiTest = new DevExpress.XtraBars.BarStaticItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.gridControl1 = new DevExpress.XtraGrid.GridControl();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.ribePwd = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.riceUseTrusted = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.ribeName = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.Sqls = new Apq.DBC.XSD();
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ribePwd)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.riceUseTrusted)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ribeName)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Sqls)).BeginInit();
			this.SuspendLayout();
			// 
			// barManager1
			// 
			this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2,
            this.bar3});
			this.barManager1.DockControls.Add(this.barDockControlTop);
			this.barManager1.DockControls.Add(this.barDockControlBottom);
			this.barManager1.DockControls.Add(this.barDockControlLeft);
			this.barManager1.DockControls.Add(this.barDockControlRight);
			this.barManager1.Form = this;
			this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bsiOutInfo,
            this.bsiTest,
            this.bbiSelectAll,
            this.bbiReverse,
            this.beiStr,
            this.bbiSlts});
			this.barManager1.MainMenu = this.bar2;
			this.barManager1.MaxItemId = 6;
			this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
			this.barManager1.StatusBar = this.bar3;
			// 
			// bar2
			// 
			this.bar2.BarName = "Main menu";
			this.bar2.DockCol = 0;
			this.bar2.DockRow = 0;
			this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiSelectAll),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiReverse),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.beiStr, "", false, true, true, 196),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiSlts)});
			this.bar2.OptionsBar.MultiLine = true;
			this.bar2.OptionsBar.UseWholeRow = true;
			this.bar2.Text = "Main menu";
			// 
			// bbiSelectAll
			// 
			this.bbiSelectAll.Caption = "全选";
			this.bbiSelectAll.Id = 2;
			this.bbiSelectAll.Name = "bbiSelectAll";
			// 
			// bbiReverse
			// 
			this.bbiReverse.Caption = "反选";
			this.bbiReverse.Id = 3;
			this.bbiReverse.Name = "bbiReverse";
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
			// gridControl1
			// 
			this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControl1.Location = new System.Drawing.Point(0, 25);
			this.gridControl1.MainView = this.gridView1;
			this.gridControl1.Name = "gridControl1";
			this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ribePwd,
            this.ribeName,
            this.riceUseTrusted});
			this.gridControl1.Size = new System.Drawing.Size(692, 316);
			this.gridControl1.TabIndex = 5;
			this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
			// 
			// gridView1
			// 
			this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn6,
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
			// gridColumn2
			// 
			this.gridColumn2.Caption = "数据库名";
			this.gridColumn2.FieldName = "DBName";
			this.gridColumn2.Name = "gridColumn2";
			this.gridColumn2.Visible = true;
			this.gridColumn2.VisibleIndex = 0;
			// 
			// gridColumn6
			// 
			this.gridColumn6.Caption = "服务器";
			this.gridColumn6.FieldName = "ServerName";
			this.gridColumn6.Name = "gridColumn6";
			this.gridColumn6.Visible = true;
			this.gridColumn6.VisibleIndex = 1;
			// 
			// gridColumn1
			// 
			this.gridColumn1.Caption = "数据库类型";
			this.gridColumn1.Name = "gridColumn1";
			this.gridColumn1.Visible = true;
			this.gridColumn1.VisibleIndex = 2;
			// 
			// gridColumn9
			// 
			this.gridColumn9.Caption = "实例名";
			this.gridColumn9.Name = "gridColumn9";
			this.gridColumn9.Visible = true;
			this.gridColumn9.VisibleIndex = 3;
			// 
			// gridColumn3
			// 
			this.gridColumn3.Caption = "用户名";
			this.gridColumn3.FieldName = "UserId";
			this.gridColumn3.Name = "gridColumn3";
			this.gridColumn3.Visible = true;
			this.gridColumn3.VisibleIndex = 4;
			// 
			// gridColumn4
			// 
			this.gridColumn4.Caption = "密码";
			this.gridColumn4.ColumnEdit = this.ribePwd;
			this.gridColumn4.FieldName = "Pwd";
			this.gridColumn4.Name = "gridColumn4";
			this.gridColumn4.Visible = true;
			this.gridColumn4.VisibleIndex = 5;
			// 
			// ribePwd
			// 
			this.ribePwd.AutoHeight = false;
			this.ribePwd.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinRight)});
			this.ribePwd.Name = "ribePwd";
			this.ribePwd.PasswordChar = '*';
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
			// Sqls
			// 
			this.Sqls.DataSetName = "Sqls";
			this.Sqls.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
			this.Load += new System.EventHandler(this.Random_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Random_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ribePwd)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.riceUseTrusted)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ribeName)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Sqls)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraBars.BarManager barManager1;
		private DevExpress.XtraBars.Bar bar2;
		private DevExpress.XtraBars.Bar bar3;
		private DevExpress.XtraBars.BarDockControl barDockControlTop;
		private DevExpress.XtraBars.BarDockControl barDockControlBottom;
		private DevExpress.XtraBars.BarDockControl barDockControlLeft;
		private DevExpress.XtraBars.BarDockControl barDockControlRight;
		private DevExpress.XtraBars.BarStaticItem bsiOutInfo;
		private DevExpress.XtraBars.BarStaticItem bsiTest;
		private DevExpress.XtraBars.BarButtonItem bbiSelectAll;
		private DevExpress.XtraBars.BarButtonItem bbiReverse;
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
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit ribePwd;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
		public Apq.DBC.XSD Sqls;

	}
}