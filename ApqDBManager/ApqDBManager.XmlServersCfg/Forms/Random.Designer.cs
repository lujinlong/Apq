namespace ApqDBManager
{
	partial class Random
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Random));
			this.bar1 = new DevExpress.XtraBars.Bar();
			this.random1 = new ApqDBManager.xsd.Random();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.btnGUID = new DevExpress.XtraEditors.SimpleButton();
			this.btnCopy = new DevExpress.XtraEditors.SimpleButton();
			this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
			this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
			this.lstStrings = new DevExpress.XtraEditors.CheckedListBoxControl();
			this.spinEdit2 = new DevExpress.XtraEditors.SpinEdit();
			this.spinEdit1 = new DevExpress.XtraEditors.SpinEdit();
			this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.btnGo = new DevExpress.XtraEditors.SimpleButton();
			this.gridControl1 = new DevExpress.XtraGrid.GridControl();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.colstr = new DevExpress.XtraGrid.Columns.GridColumn();
			((System.ComponentModel.ISupportInitialize)(this.random1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lstStrings)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spinEdit2.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// bar1
			// 
			this.bar1.BarName = "Tools";
			this.bar1.DockCol = 0;
			this.bar1.DockRow = 1;
			this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.bar1.Text = "Tools";
			// 
			// random1
			// 
			this.random1.DataSetName = "Random";
			this.random1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
			this.splitContainer1.Panel1.Controls.Add(this.btnGUID);
			this.splitContainer1.Panel1.Controls.Add(this.btnCopy);
			this.splitContainer1.Panel1.Controls.Add(this.textEdit1);
			this.splitContainer1.Panel1.Controls.Add(this.labelControl3);
			this.splitContainer1.Panel1.Controls.Add(this.lstStrings);
			this.splitContainer1.Panel1.Controls.Add(this.spinEdit2);
			this.splitContainer1.Panel1.Controls.Add(this.spinEdit1);
			this.splitContainer1.Panel1.Controls.Add(this.labelControl2);
			this.splitContainer1.Panel1.Controls.Add(this.labelControl1);
			this.splitContainer1.Panel1.Controls.Add(this.btnGo);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.gridControl1);
			this.splitContainer1.Size = new System.Drawing.Size(600, 423);
			this.splitContainer1.SplitterDistance = 119;
			this.splitContainer1.TabIndex = 0;
			// 
			// btnGUID
			// 
			this.btnGUID.Location = new System.Drawing.Point(12, 87);
			this.btnGUID.Name = "btnGUID";
			this.btnGUID.Size = new System.Drawing.Size(75, 23);
			this.btnGUID.TabIndex = 30;
			this.btnGUID.Text = "GUID";
			this.btnGUID.Click += new System.EventHandler(this.btnGUID_Click);
			// 
			// btnCopy
			// 
			this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCopy.Location = new System.Drawing.Point(491, 87);
			this.btnCopy.Name = "btnCopy";
			this.btnCopy.Size = new System.Drawing.Size(75, 23);
			this.btnCopy.TabIndex = 29;
			this.btnCopy.Text = "复制";
			this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
			// 
			// textEdit1
			// 
			this.textEdit1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.textEdit1.Location = new System.Drawing.Point(217, 12);
			this.textEdit1.Name = "textEdit1";
			this.textEdit1.Size = new System.Drawing.Size(371, 21);
			this.textEdit1.TabIndex = 28;
			// 
			// labelControl3
			// 
			this.labelControl3.Location = new System.Drawing.Point(177, 15);
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Size = new System.Drawing.Size(24, 14);
			this.labelControl3.TabIndex = 27;
			this.labelControl3.Text = "范围";
			// 
			// lstStrings
			// 
			this.lstStrings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lstStrings.Location = new System.Drawing.Point(217, 39);
			this.lstStrings.Name = "lstStrings";
			this.lstStrings.Size = new System.Drawing.Size(240, 71);
			this.lstStrings.TabIndex = 26;
			this.lstStrings.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.lstStrings_ItemCheck);
			// 
			// spinEdit2
			// 
			this.spinEdit2.EditValue = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.spinEdit2.Location = new System.Drawing.Point(52, 51);
			this.spinEdit2.Name = "spinEdit2";
			this.spinEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.spinEdit2.Properties.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.spinEdit2.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.spinEdit2.Size = new System.Drawing.Size(100, 21);
			this.spinEdit2.TabIndex = 25;
			// 
			// spinEdit1
			// 
			this.spinEdit1.EditValue = new decimal(new int[] {
            16,
            0,
            0,
            0});
			this.spinEdit1.Location = new System.Drawing.Point(52, 12);
			this.spinEdit1.Name = "spinEdit1";
			this.spinEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.spinEdit1.Properties.MaxValue = new decimal(new int[] {
            8000,
            0,
            0,
            0});
			this.spinEdit1.Properties.MinValue = new decimal(new int[] {
            6,
            0,
            0,
            0});
			this.spinEdit1.Size = new System.Drawing.Size(100, 21);
			this.spinEdit1.TabIndex = 24;
			// 
			// labelControl2
			// 
			this.labelControl2.Location = new System.Drawing.Point(12, 54);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(24, 14);
			this.labelControl2.TabIndex = 23;
			this.labelControl2.Text = "个数";
			// 
			// labelControl1
			// 
			this.labelControl1.Location = new System.Drawing.Point(12, 15);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(24, 14);
			this.labelControl1.TabIndex = 22;
			this.labelControl1.Text = "位数";
			// 
			// btnGo
			// 
			this.btnGo.Location = new System.Drawing.Point(93, 87);
			this.btnGo.Name = "btnGo";
			this.btnGo.Size = new System.Drawing.Size(75, 23);
			this.btnGo.TabIndex = 21;
			this.btnGo.Text = "GO";
			this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
			// 
			// gridControl1
			// 
			this.gridControl1.DataMember = "DataTable1";
			this.gridControl1.DataSource = this.random1;
			this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControl1.EmbeddedNavigator.Name = "";
			this.gridControl1.Location = new System.Drawing.Point(0, 0);
			this.gridControl1.MainView = this.gridView1;
			this.gridControl1.Name = "gridControl1";
			this.gridControl1.Size = new System.Drawing.Size(600, 300);
			this.gridControl1.TabIndex = 16;
			this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
			// 
			// gridView1
			// 
			this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colstr});
			this.gridView1.GridControl = this.gridControl1;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsSelection.MultiSelect = true;
			this.gridView1.OptionsView.ShowGroupPanel = false;
			// 
			// colstr
			// 
			this.colstr.Caption = "结果";
			this.colstr.FieldName = "str";
			this.colstr.Name = "colstr";
			this.colstr.Visible = true;
			this.colstr.VisibleIndex = 0;
			// 
			// Random
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.ClientSize = new System.Drawing.Size(600, 423);
			this.Controls.Add(this.splitContainer1);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Random";
			this.TabText = "随机串生成器";
			this.Text = "随机串生成器";
			this.Load += new System.EventHandler(this.Random_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Random_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.random1)).EndInit();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lstStrings)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spinEdit2.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraBars.Bar bar1;
		private ApqDBManager.xsd.Random random1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private DevExpress.XtraEditors.SimpleButton btnCopy;
		private DevExpress.XtraEditors.TextEdit textEdit1;
		private DevExpress.XtraEditors.LabelControl labelControl3;
		private DevExpress.XtraEditors.CheckedListBoxControl lstStrings;
		private DevExpress.XtraEditors.SpinEdit spinEdit2;
		private DevExpress.XtraEditors.SpinEdit spinEdit1;
		private DevExpress.XtraEditors.LabelControl labelControl2;
		private DevExpress.XtraEditors.LabelControl labelControl1;
		private DevExpress.XtraEditors.SimpleButton btnGo;
		private DevExpress.XtraGrid.GridControl gridControl1;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
		private DevExpress.XtraGrid.Columns.GridColumn colstr;
		private DevExpress.XtraEditors.SimpleButton btnGUID;


	}
}