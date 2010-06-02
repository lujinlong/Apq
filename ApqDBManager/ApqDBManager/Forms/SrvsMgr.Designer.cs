namespace ApqDBManager.Forms
{
	partial class SrvsMgr
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
			DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
			DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
			DevExpress.XtraGrid.GridLevelNode gridLevelNode3 = new DevExpress.XtraGrid.GridLevelNode();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SrvsMgr));
			this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridControl1 = new DevExpress.XtraGrid.GridControl();
			this.gridView4 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.bar1 = new DevExpress.XtraBars.Bar();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.btnLoadFromDB = new DevExpress.XtraEditors.SimpleButton();
			this.btnSaveToDB = new DevExpress.XtraEditors.SimpleButton();
			this.srvsMgr1 = new ApqDBManager.XSD.SrvsMgr();
			((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.srvsMgr1)).BeginInit();
			this.SuspendLayout();
			// 
			// gridView2
			// 
			this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13});
			this.gridView2.GridControl = this.gridControl1;
			this.gridView2.Name = "gridView2";
			this.gridView2.OptionsView.ShowGroupPanel = false;
			// 
			// gridColumn7
			// 
			this.gridColumn7.Caption = "RDBID";
			this.gridColumn7.FieldName = "RDBID";
			this.gridColumn7.Name = "gridColumn7";
			this.gridColumn7.Visible = true;
			this.gridColumn7.VisibleIndex = 0;
			// 
			// gridColumn8
			// 
			this.gridColumn8.Caption = "数据库";
			this.gridColumn8.FieldName = "DBName";
			this.gridColumn8.Name = "gridColumn8";
			this.gridColumn8.Visible = true;
			this.gridColumn8.VisibleIndex = 1;
			// 
			// gridColumn9
			// 
			this.gridColumn9.Caption = "描述";
			this.gridColumn9.FieldName = "RDBDesc";
			this.gridColumn9.Name = "gridColumn9";
			this.gridColumn9.Visible = true;
			this.gridColumn9.VisibleIndex = 2;
			// 
			// gridColumn10
			// 
			this.gridColumn10.Caption = "&类型";
			this.gridColumn10.FieldName = "RDBType";
			this.gridColumn10.Name = "gridColumn10";
			this.gridColumn10.Visible = true;
			this.gridColumn10.VisibleIndex = 3;
			// 
			// gridColumn11
			// 
			this.gridColumn11.Caption = "全局层级";
			this.gridColumn11.FieldName = "PLevel";
			this.gridColumn11.Name = "gridColumn11";
			this.gridColumn11.Visible = true;
			this.gridColumn11.VisibleIndex = 4;
			// 
			// gridColumn12
			// 
			this.gridColumn12.Caption = "游戏内层级";
			this.gridColumn12.FieldName = "GLevel";
			this.gridColumn12.Name = "gridColumn12";
			this.gridColumn12.Visible = true;
			this.gridColumn12.VisibleIndex = 5;
			// 
			// gridColumn13
			// 
			this.gridColumn13.Caption = "*游戏";
			this.gridColumn13.FieldName = "GameID";
			this.gridColumn13.Name = "gridColumn13";
			this.gridColumn13.Visible = true;
			this.gridColumn13.VisibleIndex = 6;
			// 
			// gridControl1
			// 
			this.gridControl1.DataSource = this.srvsMgr1;
			this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControl1.EmbeddedNavigator.Name = "";
			gridLevelNode1.LevelTemplate = this.gridView2;
			gridLevelNode2.LevelTemplate = this.gridView3;
			gridLevelNode2.RelationName = "DBUser";
			gridLevelNode1.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode2});
			gridLevelNode1.RelationName = "DB";
			gridLevelNode3.LevelTemplate = this.gridView4;
			gridLevelNode3.RelationName = "DBLogin";
			this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1,
            gridLevelNode3});
			this.gridControl1.Location = new System.Drawing.Point(0, 0);
			this.gridControl1.MainView = this.gridView1;
			this.gridControl1.Name = "gridControl1";
			this.gridControl1.Size = new System.Drawing.Size(600, 369);
			this.gridControl1.TabIndex = 0;
			this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView4,
            this.gridView1,
            this.gridView3,
            this.gridView5,
            this.gridView2});
			// 
			// gridView4
			// 
			this.gridView4.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn18,
            this.gridColumn19,
            this.gridColumn20,
            this.gridColumn21,
            this.gridColumn22});
			this.gridView4.GridControl = this.gridControl1;
			this.gridView4.Name = "gridView4";
			this.gridView4.OptionsView.ShowGroupPanel = false;
			// 
			// gridColumn18
			// 
			this.gridColumn18.Caption = "RDBLoginID";
			this.gridColumn18.FieldName = "RDBLoginID";
			this.gridColumn18.Name = "gridColumn18";
			this.gridColumn18.Visible = true;
			this.gridColumn18.VisibleIndex = 0;
			// 
			// gridColumn19
			// 
			this.gridColumn19.Caption = "登录名";
			this.gridColumn19.FieldName = "DBLoginName";
			this.gridColumn19.Name = "gridColumn19";
			this.gridColumn19.Visible = true;
			this.gridColumn19.VisibleIndex = 1;
			// 
			// gridColumn20
			// 
			this.gridColumn20.Caption = "描述";
			this.gridColumn20.FieldName = "DBLoginDesc";
			this.gridColumn20.Name = "gridColumn20";
			this.gridColumn20.Visible = true;
			this.gridColumn20.VisibleIndex = 2;
			// 
			// gridColumn21
			// 
			this.gridColumn21.Caption = "SID";
			this.gridColumn21.FieldName = "SID";
			this.gridColumn21.Name = "gridColumn21";
			this.gridColumn21.Visible = true;
			this.gridColumn21.VisibleIndex = 3;
			// 
			// gridColumn22
			// 
			this.gridColumn22.Caption = "密码";
			this.gridColumn22.FieldName = "LoginPwdD";
			this.gridColumn22.Name = "gridColumn22";
			this.gridColumn22.Visible = true;
			this.gridColumn22.VisibleIndex = 4;
			// 
			// gridView1
			// 
			this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6});
			this.gridView1.GridControl = this.gridControl1;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsView.ShowGroupPanel = false;
			// 
			// gridColumn2
			// 
			this.gridColumn2.Caption = "ID";
			this.gridColumn2.FieldName = "ID";
			this.gridColumn2.Name = "gridColumn2";
			this.gridColumn2.Visible = true;
			this.gridColumn2.VisibleIndex = 0;
			// 
			// gridColumn3
			// 
			this.gridColumn3.Caption = "服务器名";
			this.gridColumn3.FieldName = "Name";
			this.gridColumn3.Name = "gridColumn3";
			this.gridColumn3.Visible = true;
			this.gridColumn3.VisibleIndex = 1;
			// 
			// gridColumn4
			// 
			this.gridColumn4.Caption = "局域网IP";
			this.gridColumn4.FieldName = "IPLan";
			this.gridColumn4.Name = "gridColumn4";
			this.gridColumn4.Visible = true;
			this.gridColumn4.VisibleIndex = 2;
			// 
			// gridColumn5
			// 
			this.gridColumn5.Caption = "电信IP";
			this.gridColumn5.FieldName = "IPWan1";
			this.gridColumn5.Name = "gridColumn5";
			this.gridColumn5.Visible = true;
			this.gridColumn5.VisibleIndex = 3;
			// 
			// gridColumn6
			// 
			this.gridColumn6.Caption = "网通IP";
			this.gridColumn6.FieldName = "IPWan2";
			this.gridColumn6.Name = "gridColumn6";
			this.gridColumn6.Visible = true;
			this.gridColumn6.VisibleIndex = 4;
			// 
			// gridView3
			// 
			this.gridView3.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn14,
            this.gridColumn15,
            this.gridColumn16,
            this.gridColumn17});
			this.gridView3.GridControl = this.gridControl1;
			this.gridView3.Name = "gridView3";
			this.gridView3.OptionsView.ShowGroupPanel = false;
			// 
			// gridColumn14
			// 
			this.gridColumn14.Caption = "RDBUserID";
			this.gridColumn14.FieldName = "RDBUserID";
			this.gridColumn14.Name = "gridColumn14";
			this.gridColumn14.Visible = true;
			this.gridColumn14.VisibleIndex = 0;
			// 
			// gridColumn15
			// 
			this.gridColumn15.Caption = "用户名";
			this.gridColumn15.FieldName = "DBUserName";
			this.gridColumn15.Name = "gridColumn15";
			this.gridColumn15.Visible = true;
			this.gridColumn15.VisibleIndex = 1;
			// 
			// gridColumn16
			// 
			this.gridColumn16.Caption = "描述";
			this.gridColumn16.FieldName = "DBUserDesc";
			this.gridColumn16.Name = "gridColumn16";
			this.gridColumn16.Visible = true;
			this.gridColumn16.VisibleIndex = 2;
			// 
			// gridColumn17
			// 
			this.gridColumn17.Caption = "*登录";
			this.gridColumn17.FieldName = "RDBLoginID";
			this.gridColumn17.Name = "gridColumn17";
			this.gridColumn17.Visible = true;
			this.gridColumn17.VisibleIndex = 3;
			// 
			// gridView5
			// 
			this.gridView5.GridControl = this.gridControl1;
			this.gridView5.Name = "gridView5";
			this.gridView5.OptionsView.ShowGroupPanel = false;
			// 
			// bar1
			// 
			this.bar1.BarName = "Tools";
			this.bar1.DockCol = 0;
			this.bar1.DockRow = 1;
			this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.bar1.Text = "Tools";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.btnLoadFromDB);
			this.splitContainer1.Panel1.Controls.Add(this.btnSaveToDB);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.gridControl1);
			this.splitContainer1.Size = new System.Drawing.Size(600, 423);
			this.splitContainer1.TabIndex = 0;
			// 
			// btnLoadFromDB
			// 
			this.btnLoadFromDB.Location = new System.Drawing.Point(12, 12);
			this.btnLoadFromDB.Name = "btnLoadFromDB";
			this.btnLoadFromDB.Size = new System.Drawing.Size(75, 23);
			this.btnLoadFromDB.TabIndex = 30;
			this.btnLoadFromDB.Text = "加载";
			this.btnLoadFromDB.Click += new System.EventHandler(this.btnLoadFromDB_Click);
			// 
			// btnSaveToDB
			// 
			this.btnSaveToDB.Location = new System.Drawing.Point(93, 12);
			this.btnSaveToDB.Name = "btnSaveToDB";
			this.btnSaveToDB.Size = new System.Drawing.Size(75, 23);
			this.btnSaveToDB.TabIndex = 21;
			this.btnSaveToDB.Text = "保存";
			this.btnSaveToDB.Click += new System.EventHandler(this.btnSaveToDB_Click);
			// 
			// srvsMgr1
			// 
			this.srvsMgr1.DataSetName = "SrvsMgr";
			this.srvsMgr1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// SrvsMgr
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.ClientSize = new System.Drawing.Size(600, 423);
			this.Controls.Add(this.splitContainer1);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "SrvsMgr";
			this.TabText = "服务器管理";
			this.Text = "服务器管理";
			this.Load += new System.EventHandler(this.Random_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Random_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.srvsMgr1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraBars.Bar bar1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private DevExpress.XtraEditors.SimpleButton btnLoadFromDB;
		private DevExpress.XtraEditors.SimpleButton btnSaveToDB;
		private DevExpress.XtraGrid.GridControl gridControl1;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView4;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
		private ApqDBManager.XSD.SrvsMgr srvsMgr1;


	}
}