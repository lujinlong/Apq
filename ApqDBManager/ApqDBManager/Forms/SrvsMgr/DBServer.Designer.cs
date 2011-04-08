namespace ApqDBManager.Forms.SrvsMgr
{
	partial class DBServer
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBServer));
			this.gridControl1 = new DevExpress.XtraGrid.GridControl();
			this.cmGridMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiDel = new System.Windows.Forms.ToolStripMenuItem();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.luComputerType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
			this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
			this.bar1 = new DevExpress.XtraBars.Bar();
			this.bbiRefresh = new DevExpress.XtraBars.BarButtonItem();
			this.bbiSaveToDB = new DevExpress.XtraBars.BarButtonItem();
			this.bbiSelectAll = new DevExpress.XtraBars.BarButtonItem();
			this.beiStr = new DevExpress.XtraBars.BarEditItem();
			this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.bbiSlts = new DevExpress.XtraBars.BarButtonItem();
			this.bbiSqlInstance = new DevExpress.XtraBars.BarButtonItem();
			this.bar3 = new DevExpress.XtraBars.Bar();
			this.bsiOutInfo = new DevExpress.XtraBars.BarStaticItem();
			this.bsiTest = new DevExpress.XtraBars.BarStaticItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.scSelect = new System.Data.SqlClient.SqlCommand();
			this.sda = new System.Data.SqlClient.SqlDataAdapter();
			this.scDelete = new System.Data.SqlClient.SqlCommand();
			this.scInsert = new System.Data.SqlClient.SqlCommand();
			this.scUpdate = new System.Data.SqlClient.SqlCommand();
			this.sqlCommand1 = new System.Data.SqlClient.SqlCommand();
			this.sqlDataAdapter1 = new System.Data.SqlClient.SqlDataAdapter();
			this.sqlDataAdapter2 = new System.Data.SqlClient.SqlDataAdapter();
			this.sqlCommand2 = new System.Data.SqlClient.SqlCommand();
			this.sqlDataAdapter3 = new System.Data.SqlClient.SqlDataAdapter();
			this.sqlCommand3 = new System.Data.SqlClient.SqlCommand();
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
			this.cmGridMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.luComputerType)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
			this.SuspendLayout();
			// 
			// gridControl1
			// 
			this.gridControl1.ContextMenuStrip = this.cmGridMenu;
			this.gridControl1.DataMember = "Computer";
			this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControl1.Location = new System.Drawing.Point(0, 25);
			this.gridControl1.MainView = this.gridView1;
			this.gridControl1.Name = "gridControl1";
			this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.luComputerType});
			this.gridControl1.Size = new System.Drawing.Size(692, 316);
			this.gridControl1.TabIndex = 4;
			this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
			// 
			// cmGridMenu
			// 
			this.cmGridMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDel});
			this.cmGridMenu.Name = "contextMenuStrip1";
			this.cmGridMenu.Size = new System.Drawing.Size(113, 26);
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
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn1});
			this.gridView1.GridControl = this.gridControl1;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
			this.gridView1.OptionsSelection.MultiSelect = true;
			this.gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
			this.gridView1.OptionsView.ShowGroupPanel = false;
			// 
			// gridColumn2
			// 
			this.gridColumn2.Caption = "服务器编号";
			this.gridColumn2.FieldName = "ComputerID";
			this.gridColumn2.Name = "gridColumn2";
			this.gridColumn2.Visible = true;
			this.gridColumn2.VisibleIndex = 0;
			// 
			// gridColumn3
			// 
			this.gridColumn3.Caption = "服务器名";
			this.gridColumn3.FieldName = "ComputerName";
			this.gridColumn3.Name = "gridColumn3";
			this.gridColumn3.Visible = true;
			this.gridColumn3.VisibleIndex = 1;
			// 
			// gridColumn1
			// 
			this.gridColumn1.Caption = "服务器类型";
			this.gridColumn1.ColumnEdit = this.luComputerType;
			this.gridColumn1.FieldName = "ComputerType";
			this.gridColumn1.Name = "gridColumn1";
			this.gridColumn1.Visible = true;
			this.gridColumn1.VisibleIndex = 2;
			// 
			// luComputerType
			// 
			this.luComputerType.AutoHeight = false;
			this.luComputerType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.luComputerType.DisplayMember = "ComputerType.TypeCaption";
			this.luComputerType.Name = "luComputerType";
			// 
			// gridColumn18
			// 
			this.gridColumn18.Name = "gridColumn18";
			// 
			// gridColumn19
			// 
			this.gridColumn19.Name = "gridColumn19";
			// 
			// gridColumn20
			// 
			this.gridColumn20.Name = "gridColumn20";
			// 
			// gridColumn21
			// 
			this.gridColumn21.Name = "gridColumn21";
			// 
			// gridColumn22
			// 
			this.gridColumn22.Name = "gridColumn22";
			// 
			// gridColumn14
			// 
			this.gridColumn14.Name = "gridColumn14";
			// 
			// gridColumn15
			// 
			this.gridColumn15.Name = "gridColumn15";
			// 
			// gridColumn16
			// 
			this.gridColumn16.Name = "gridColumn16";
			// 
			// gridColumn17
			// 
			this.gridColumn17.Name = "gridColumn17";
			// 
			// barManager1
			// 
			this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar3});
			this.barManager1.DockControls.Add(this.barDockControlTop);
			this.barManager1.DockControls.Add(this.barDockControlBottom);
			this.barManager1.DockControls.Add(this.barDockControlLeft);
			this.barManager1.DockControls.Add(this.barDockControlRight);
			this.barManager1.Form = this;
			this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiSaveToDB,
            this.bbiSelectAll,
            this.beiStr,
            this.bbiSlts,
            this.bsiOutInfo,
            this.bsiTest,
            this.bbiSqlInstance,
            this.bbiRefresh});
			this.barManager1.MaxItemId = 11;
			this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
			this.barManager1.StatusBar = this.bar3;
			// 
			// bar1
			// 
			this.bar1.BarName = "Tools";
			this.bar1.DockCol = 0;
			this.bar1.DockRow = 0;
			this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiRefresh),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiSaveToDB),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiSelectAll, true),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.beiStr, "", false, true, true, 235),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiSlts),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiSqlInstance, true)});
			this.bar1.Text = "Tools";
			// 
			// bbiRefresh
			// 
			this.bbiRefresh.Caption = "刷新";
			this.bbiRefresh.Id = 10;
			this.bbiRefresh.Name = "bbiRefresh";
			this.bbiRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiRefresh_ItemClick);
			// 
			// bbiSaveToDB
			// 
			this.bbiSaveToDB.Caption = "保存";
			this.bbiSaveToDB.Hint = "保存";
			this.bbiSaveToDB.Id = 1;
			this.bbiSaveToDB.Name = "bbiSaveToDB";
			this.bbiSaveToDB.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiSaveToDB_ItemClick);
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
			this.beiStr.Caption = "a";
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
			this.bbiSlts.Id = 6;
			this.bbiSlts.Name = "bbiSlts";
			this.bbiSlts.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiSlts_ItemClick);
			// 
			// bbiSqlInstance
			// 
			this.bbiSqlInstance.Caption = "实例管理";
			this.bbiSqlInstance.Id = 9;
			this.bbiSqlInstance.Name = "bbiSqlInstance";
			this.bbiSqlInstance.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiSqlInstance_ItemClick);
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
			this.bsiOutInfo.Id = 7;
			this.bsiOutInfo.Name = "bsiOutInfo";
			this.bsiOutInfo.TextAlignment = System.Drawing.StringAlignment.Near;
			this.bsiOutInfo.Width = 300;
			// 
			// bsiTest
			// 
			this.bsiTest.Id = 8;
			this.bsiTest.Name = "bsiTest";
			this.bsiTest.TextAlignment = System.Drawing.StringAlignment.Near;
			// 
			// gridColumn7
			// 
			this.gridColumn7.Name = "gridColumn7";
			// 
			// gridColumn8
			// 
			this.gridColumn8.Name = "gridColumn8";
			// 
			// gridColumn9
			// 
			this.gridColumn9.Name = "gridColumn9";
			// 
			// gridColumn10
			// 
			this.gridColumn10.Name = "gridColumn10";
			// 
			// gridColumn11
			// 
			this.gridColumn11.Name = "gridColumn11";
			// 
			// gridColumn12
			// 
			this.gridColumn12.Name = "gridColumn12";
			// 
			// gridColumn13
			// 
			this.gridColumn13.Name = "gridColumn13";
			// 
			// scSelect
			// 
			this.scSelect.CommandText = "\r\nSELECT * FROM dic_SQLType;\r\nSELECT * FROM dic_IPType;\r\nSELECT * FROM DBServer;\r" +
				"\nSELECT * FROM SQLInstance;\r\nSELECT * FROM DBC;\r\nSELECT * FROM DBServerIP;";
			// 
			// sda
			// 
			this.sda.DeleteCommand = this.scDelete;
			this.sda.InsertCommand = this.scInsert;
			this.sda.SelectCommand = this.scSelect;
			this.sda.UpdateCommand = this.scUpdate;
			// 
			// sqlDataAdapter1
			// 
			this.sqlDataAdapter1.SelectCommand = this.sqlCommand1;
			this.sqlDataAdapter1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "ComputerType", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("ComputerType", "ComputerType"),
                        new System.Data.Common.DataColumnMapping("TypeCaption", "TypeCaption")})});
			// 
			// sqlDataAdapter2
			// 
			this.sqlDataAdapter2.SelectCommand = this.sqlCommand2;
			this.sqlDataAdapter2.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "ComputerType", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("ComputerType", "ComputerType"),
                        new System.Data.Common.DataColumnMapping("TypeCaption", "TypeCaption")})});
			// 
			// sqlDataAdapter3
			// 
			this.sqlDataAdapter3.SelectCommand = this.sqlCommand3;
			this.sqlDataAdapter3.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "ComputerType", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("ComputerType", "ComputerType"),
                        new System.Data.Common.DataColumnMapping("TypeCaption", "TypeCaption")})});
			// 
			// DBServer
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
			this.Name = "DBServer";
			this.TabText = "服务器管理";
			this.Text = "服务器管理";
			this.Load += new System.EventHandler(this.DBServer_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DBServer_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
			this.cmGridMenu.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.luComputerType)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraBars.BarManager barManager1;
		private DevExpress.XtraBars.Bar bar1;
		private DevExpress.XtraBars.Bar bar3;
		private DevExpress.XtraBars.BarDockControl barDockControlTop;
		private DevExpress.XtraBars.BarDockControl barDockControlBottom;
		private DevExpress.XtraBars.BarDockControl barDockControlLeft;
		private DevExpress.XtraBars.BarDockControl barDockControlRight;
		private DevExpress.XtraBars.BarButtonItem bbiSaveToDB;
		private DevExpress.XtraBars.BarButtonItem bbiSelectAll;
		private DevExpress.XtraBars.BarEditItem beiStr;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
		private DevExpress.XtraBars.BarButtonItem bbiSlts;
		private DevExpress.XtraGrid.GridControl gridControl1;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
		private DevExpress.XtraBars.BarStaticItem bsiOutInfo;
		private DevExpress.XtraBars.BarStaticItem bsiTest;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
		private System.Data.SqlClient.SqlCommand scSelect;
		private System.Data.SqlClient.SqlDataAdapter sda;
		private System.Data.SqlClient.SqlCommand scDelete;
		private System.Data.SqlClient.SqlCommand scInsert;
		private System.Data.SqlClient.SqlCommand scUpdate;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit luComputerType;
		private DevExpress.XtraBars.BarButtonItem bbiSqlInstance;
		private DevExpress.XtraBars.BarButtonItem bbiRefresh;
		private System.Windows.Forms.ContextMenuStrip cmGridMenu;
		private System.Windows.Forms.ToolStripMenuItem tsmiDel;
		private System.Data.SqlClient.SqlCommand sqlCommand1;
		private System.Data.SqlClient.SqlDataAdapter sqlDataAdapter1;
		private System.Data.SqlClient.SqlDataAdapter sqlDataAdapter2;
		private System.Data.SqlClient.SqlCommand sqlCommand2;
		private System.Data.SqlClient.SqlDataAdapter sqlDataAdapter3;
		private System.Data.SqlClient.SqlCommand sqlCommand3;
	}
}