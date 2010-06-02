namespace ApqDBManager
{
	partial class Export
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
			this.saveFile1 = new Apq.Windows.Controls.SaveFile();
			this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
			this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
			this.cbExportType = new DevExpress.XtraEditors.ComboBoxEdit();
			this.cbColSpliter = new DevExpress.XtraEditors.ComboBoxEdit();
			this.cbRowSpliter = new DevExpress.XtraEditors.ComboBoxEdit();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
			this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
			this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
			this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
			this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
			this.bar3 = new DevExpress.XtraBars.Bar();
			this.bsiStatus = new DevExpress.XtraBars.BarStaticItem();
			this.beiProcess = new DevExpress.XtraBars.BarEditItem();
			this.ripb = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.ceContainsColName = new DevExpress.XtraEditors.CheckEdit();
			this.lcRowCount = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)(this.cbExportType.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cbColSpliter.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cbRowSpliter.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
			this.groupControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ripb)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ceContainsColName.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// saveFile1
			// 
			this.saveFile1.FileName = "";
			this.saveFile1.Location = new System.Drawing.Point(119, 71);
			this.saveFile1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.saveFile1.Name = "saveFile1";
			this.saveFile1.Size = new System.Drawing.Size(265, 21);
			this.saveFile1.TabIndex = 0;
			// 
			// btnConfirm
			// 
			this.btnConfirm.Location = new System.Drawing.Point(66, 194);
			this.btnConfirm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.btnConfirm.Name = "btnConfirm";
			this.btnConfirm.Size = new System.Drawing.Size(87, 27);
			this.btnConfirm.TabIndex = 1;
			this.btnConfirm.Text = "确定";
			this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(207, 194);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(87, 27);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "取消";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// cbExportType
			// 
			this.cbExportType.EditValue = "文本文件";
			this.cbExportType.Location = new System.Drawing.Point(119, 42);
			this.cbExportType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.cbExportType.Name = "cbExportType";
			this.cbExportType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.cbExportType.Properties.Items.AddRange(new object[] {
            "文本文件",
            "Excel文件"});
			this.cbExportType.Size = new System.Drawing.Size(176, 21);
			this.cbExportType.TabIndex = 3;
			this.cbExportType.SelectedIndexChanged += new System.EventHandler(this.cbExportType_SelectedIndexChanged);
			// 
			// cbColSpliter
			// 
			this.cbColSpliter.EditValue = ",";
			this.cbColSpliter.Location = new System.Drawing.Point(93, 27);
			this.cbColSpliter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.cbColSpliter.Name = "cbColSpliter";
			this.cbColSpliter.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.cbColSpliter.Properties.Items.AddRange(new object[] {
            ",",
            "\\t"});
			this.cbColSpliter.Size = new System.Drawing.Size(176, 21);
			this.cbColSpliter.TabIndex = 4;
			this.cbColSpliter.SelectedIndexChanged += new System.EventHandler(this.cbColSpliter_SelectedIndexChanged);
			// 
			// cbRowSpliter
			// 
			this.cbRowSpliter.EditValue = "\\r\\n";
			this.cbRowSpliter.Location = new System.Drawing.Point(93, 56);
			this.cbRowSpliter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.cbRowSpliter.Name = "cbRowSpliter";
			this.cbRowSpliter.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.cbRowSpliter.Properties.Items.AddRange(new object[] {
            "\\r\\n",
            "~*$"});
			this.cbRowSpliter.Size = new System.Drawing.Size(176, 21);
			this.cbRowSpliter.TabIndex = 5;
			this.cbRowSpliter.SelectedIndexChanged += new System.EventHandler(this.cbRowSpliter_SelectedIndexChanged);
			// 
			// labelControl1
			// 
			this.labelControl1.Location = new System.Drawing.Point(41, 45);
			this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(52, 14);
			this.labelControl1.TabIndex = 6;
			this.labelControl1.Text = "导出类型:";
			// 
			// labelControl2
			// 
			this.labelControl2.Location = new System.Drawing.Point(41, 74);
			this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(52, 14);
			this.labelControl2.TabIndex = 7;
			this.labelControl2.Text = "导出文件:";
			// 
			// labelControl3
			// 
			this.labelControl3.Location = new System.Drawing.Point(16, 30);
			this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Size = new System.Drawing.Size(52, 14);
			this.labelControl3.TabIndex = 8;
			this.labelControl3.Text = "列分隔符:";
			// 
			// labelControl4
			// 
			this.labelControl4.Location = new System.Drawing.Point(16, 59);
			this.labelControl4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.labelControl4.Name = "labelControl4";
			this.labelControl4.Size = new System.Drawing.Size(52, 14);
			this.labelControl4.TabIndex = 9;
			this.labelControl4.Text = "行分隔符:";
			// 
			// groupControl1
			// 
			this.groupControl1.Controls.Add(this.cbColSpliter);
			this.groupControl1.Controls.Add(this.labelControl4);
			this.groupControl1.Controls.Add(this.cbRowSpliter);
			this.groupControl1.Controls.Add(this.labelControl3);
			this.groupControl1.Location = new System.Drawing.Point(25, 96);
			this.groupControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.groupControl1.Name = "groupControl1";
			this.groupControl1.Size = new System.Drawing.Size(358, 90);
			this.groupControl1.TabIndex = 10;
			this.groupControl1.Text = "文本选项";
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
            this.beiProcess,
            this.bsiStatus});
			this.barManager1.MaxItemId = 4;
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
            new DevExpress.XtraBars.LinkPersistInfo(this.bsiStatus),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.beiProcess, "", false, true, true, 272)});
			this.bar3.OptionsBar.AllowQuickCustomization = false;
			this.bar3.OptionsBar.DrawDragBorder = false;
			this.bar3.OptionsBar.UseWholeRow = true;
			this.bar3.Text = "Status bar";
			// 
			// bsiStatus
			// 
			this.bsiStatus.Caption = "状态";
			this.bsiStatus.Id = 3;
			this.bsiStatus.Name = "bsiStatus";
			this.bsiStatus.TextAlignment = System.Drawing.StringAlignment.Near;
			// 
			// beiProcess
			// 
			this.beiProcess.Caption = "barEditItem1";
			this.beiProcess.Edit = this.ripb;
			this.beiProcess.Id = 2;
			this.beiProcess.Name = "beiProcess";
			// 
			// ripb
			// 
			this.ripb.Name = "ripb";
			this.ripb.Step = 1;
			// 
			// barDockControlTop
			// 
			this.barDockControlTop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			// 
			// barDockControlBottom
			// 
			this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			// 
			// barDockControlLeft
			// 
			this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			// 
			// barDockControlRight
			// 
			this.barDockControlRight.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			// 
			// ceContainsColName
			// 
			this.ceContainsColName.EditValue = true;
			this.ceContainsColName.Location = new System.Drawing.Point(301, 43);
			this.ceContainsColName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.ceContainsColName.Name = "ceContainsColName";
			this.ceContainsColName.Properties.AutoWidth = true;
			this.ceContainsColName.Properties.Caption = "首行为列名";
			this.ceContainsColName.Size = new System.Drawing.Size(83, 19);
			this.ceContainsColName.TabIndex = 11;
			this.ceContainsColName.CheckedChanged += new System.EventHandler(this.ceContainsColName_CheckedChanged);
			// 
			// lcRowCount
			// 
			this.lcRowCount.Location = new System.Drawing.Point(66, 13);
			this.lcRowCount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.lcRowCount.Name = "lcRowCount";
			this.lcRowCount.Size = new System.Drawing.Size(60, 14);
			this.lcRowCount.TabIndex = 13;
			this.lcRowCount.Text = "所有行数:  ";
			// 
			// Export
			// 
			this.AcceptButton = this.btnConfirm;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(403, 254);
			this.Controls.Add(this.lcRowCount);
			this.Controls.Add(this.ceContainsColName);
			this.Controls.Add(this.groupControl1);
			this.Controls.Add(this.labelControl2);
			this.Controls.Add(this.labelControl1);
			this.Controls.Add(this.cbExportType);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnConfirm);
			this.Controls.Add(this.saveFile1);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(411, 288);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(411, 288);
			this.Name = "Export";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "导出";
			this.Load += new System.EventHandler(this.Export_Load);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Export_FormClosed);
			((System.ComponentModel.ISupportInitialize)(this.cbExportType.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cbColSpliter.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cbRowSpliter.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
			this.groupControl1.ResumeLayout(false);
			this.groupControl1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ripb)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ceContainsColName.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.SimpleButton btnConfirm;
		private DevExpress.XtraEditors.SimpleButton btnCancel;
		public Apq.Windows.Controls.SaveFile saveFile1;
		private DevExpress.XtraEditors.ComboBoxEdit cbExportType;
		private DevExpress.XtraEditors.ComboBoxEdit cbColSpliter;
		private DevExpress.XtraEditors.ComboBoxEdit cbRowSpliter;
		private DevExpress.XtraEditors.LabelControl labelControl1;
		private DevExpress.XtraEditors.LabelControl labelControl2;
		private DevExpress.XtraEditors.LabelControl labelControl3;
		private DevExpress.XtraEditors.LabelControl labelControl4;
		private DevExpress.XtraEditors.GroupControl groupControl1;
		private DevExpress.XtraBars.BarManager barManager1;
		private DevExpress.XtraBars.Bar bar3;
		private DevExpress.XtraBars.BarDockControl barDockControlTop;
		private DevExpress.XtraBars.BarDockControl barDockControlBottom;
		private DevExpress.XtraBars.BarDockControl barDockControlLeft;
		private DevExpress.XtraBars.BarDockControl barDockControlRight;
		private DevExpress.XtraBars.BarStaticItem bsiStatus;
		private DevExpress.XtraBars.BarEditItem beiProcess;
		private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar ripb;
		private DevExpress.XtraEditors.CheckEdit ceContainsColName;
		private DevExpress.XtraEditors.LabelControl lcRowCount;

	}
}