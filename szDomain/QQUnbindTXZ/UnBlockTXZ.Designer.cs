namespace QQUnbindTXZ
{
	partial class UnBlockTXZ
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
			this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
			this.bar3 = new DevExpress.XtraBars.Bar();
			this.bsiState = new DevExpress.XtraBars.BarStaticItem();
			this.beiProgressBar = new DevExpress.XtraBars.BarEditItem();
			this.ripb = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.btnGo = new DevExpress.XtraEditors.SimpleButton();
			this.gc = new DevExpress.XtraGrid.GridControl();
			this.dataSet1BindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.dataSet1 = new QQUnbindTXZ.xsd.DataSet1();
			this.gv = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.colstr = new DevExpress.XtraGrid.Columns.GridColumn();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.btnSaveAs = new DevExpress.XtraEditors.SimpleButton();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ripb)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gc)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataSet1BindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gv)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// barManager1
			// 
			this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar3});
			this.barManager1.DockControls.Add(this.barDockControlTop);
			this.barManager1.DockControls.Add(this.barDockControlBottom);
			this.barManager1.DockControls.Add(this.barDockControlLeft);
			this.barManager1.DockControls.Add(this.barDockControlRight);
			this.barManager1.Form = this;
			this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bsiState,
            this.beiProgressBar});
			this.barManager1.MaxItemId = 2;
			this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ripb});
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
            new DevExpress.XtraBars.LinkPersistInfo(this.bsiState),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.beiProgressBar, "", false, true, true, 429)});
			this.bar3.OptionsBar.AllowQuickCustomization = false;
			this.bar3.OptionsBar.DrawDragBorder = false;
			this.bar3.OptionsBar.UseWholeRow = true;
			this.bar3.Text = "Status bar";
			// 
			// bsiState
			// 
			this.bsiState.Caption = "状态";
			this.bsiState.Id = 0;
			this.bsiState.Name = "bsiState";
			this.bsiState.TextAlignment = System.Drawing.StringAlignment.Near;
			// 
			// beiProgressBar
			// 
			this.beiProgressBar.Caption = "barEditItem1";
			this.beiProgressBar.Edit = this.ripb;
			this.beiProgressBar.Id = 1;
			this.beiProgressBar.Name = "beiProgressBar";
			// 
			// ripb
			// 
			this.ripb.Name = "ripb";
			// 
			// btnGo
			// 
			this.btnGo.Location = new System.Drawing.Point(12, 12);
			this.btnGo.Name = "btnGo";
			this.btnGo.Size = new System.Drawing.Size(75, 23);
			this.btnGo.TabIndex = 4;
			this.btnGo.Text = "开始";
			this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
			// 
			// gc
			// 
			this.gc.DataMember = "DataTable1";
			this.gc.DataSource = this.dataSet1BindingSource;
			this.gc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gc.EmbeddedNavigator.Name = "";
			this.gc.Location = new System.Drawing.Point(0, 0);
			this.gc.MainView = this.gv;
			this.gc.Name = "gc";
			this.gc.Size = new System.Drawing.Size(600, 350);
			this.gc.TabIndex = 5;
			this.gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv});
			// 
			// dataSet1BindingSource
			// 
			this.dataSet1BindingSource.DataSource = this.dataSet1;
			this.dataSet1BindingSource.Position = 0;
			// 
			// dataSet1
			// 
			this.dataSet1.DataSetName = "DataSet1";
			this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// gv
			// 
			this.gv.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colstr});
			this.gv.GridControl = this.gc;
			this.gv.Name = "gv";
			this.gv.OptionsBehavior.Editable = false;
			this.gv.OptionsSelection.MultiSelect = true;
			this.gv.OptionsView.ShowGroupPanel = false;
			// 
			// colstr
			// 
			this.colstr.Caption = "输出信息";
			this.colstr.FieldName = "str";
			this.colstr.Name = "colstr";
			this.colstr.Visible = true;
			this.colstr.VisibleIndex = 0;
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// btnSaveAs
			// 
			this.btnSaveAs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSaveAs.Location = new System.Drawing.Point(513, 12);
			this.btnSaveAs.Name = "btnSaveAs";
			this.btnSaveAs.Size = new System.Drawing.Size(75, 23);
			this.btnSaveAs.TabIndex = 6;
			this.btnSaveAs.Text = "另存为";
			this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.IsSplitterFixed = true;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.btnGo);
			this.splitContainer1.Panel1.Controls.Add(this.btnSaveAs);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.gc);
			this.splitContainer1.Size = new System.Drawing.Size(600, 400);
			this.splitContainer1.SplitterDistance = 46;
			this.splitContainer1.TabIndex = 7;
			// 
			// UnBlockTXZ
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(600, 423);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.KeyPreview = true;
			this.Name = "UnBlockTXZ";
			this.TabText = "通行证解封";
			this.Text = "通行证解封";
			this.Load += new System.EventHandler(this.UnBlockTXZ_Load);
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ripb)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gc)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataSet1BindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gv)).EndInit();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraBars.BarManager barManager1;
		private DevExpress.XtraBars.Bar bar3;
		private DevExpress.XtraBars.BarStaticItem bsiState;
		private DevExpress.XtraBars.BarEditItem beiProgressBar;
		private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar ripb;
		private DevExpress.XtraBars.BarDockControl barDockControlTop;
		private DevExpress.XtraBars.BarDockControl barDockControlBottom;
		private DevExpress.XtraBars.BarDockControl barDockControlLeft;
		private DevExpress.XtraBars.BarDockControl barDockControlRight;
		private DevExpress.XtraEditors.SimpleButton btnGo;
		private DevExpress.XtraGrid.GridControl gc;
		private DevExpress.XtraGrid.Views.Grid.GridView gv;
		private System.Windows.Forms.BindingSource dataSet1BindingSource;
		private QQUnbindTXZ.xsd.DataSet1 dataSet1;
		private DevExpress.XtraGrid.Columns.GridColumn colstr;
		private System.Windows.Forms.Timer timer1;
		private DevExpress.XtraEditors.SimpleButton btnSaveAs;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.SplitContainer splitContainer1;

	}
}