namespace ApqDBManager.Controls
{
	partial class ResultTable
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

		#region 组件设计器生成的代码

		/// <summary> 
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
			this.bar2 = new DevExpress.XtraBars.Bar();
			this.btnExport = new DevExpress.XtraBars.BarLargeButtonItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tcRt = new DevExpress.XtraTab.XtraTabControl();
			this.tpRt = new DevExpress.XtraTab.XtraTabPage();
			this.tpInfo = new DevExpress.XtraTab.XtraTabPage();
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tcRt)).BeginInit();
			this.tcRt.SuspendLayout();
			this.SuspendLayout();
			// 
			// barManager1
			// 
			this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
			this.barManager1.DockControls.Add(this.barDockControlTop);
			this.barManager1.DockControls.Add(this.barDockControlBottom);
			this.barManager1.DockControls.Add(this.barDockControlLeft);
			this.barManager1.DockControls.Add(this.barDockControlRight);
			this.barManager1.Form = this;
			this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnExport});
			this.barManager1.MainMenu = this.bar2;
			this.barManager1.MaxItemId = 0;
			// 
			// bar2
			// 
			this.bar2.BarName = "Main menu";
			this.bar2.DockCol = 0;
			this.bar2.DockRow = 0;
			this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnExport)});
			this.bar2.OptionsBar.MultiLine = true;
			this.bar2.OptionsBar.UseWholeRow = true;
			this.bar2.Text = "Main menu";
			// 
			// btnExport
			// 
			this.btnExport.Caption = "导出(&T)";
			this.btnExport.Id = 6;
			this.btnExport.Name = "btnExport";
			this.btnExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExport_ItemClick);
			// 
			// panel1
			// 
			this.panel1.AutoScroll = true;
			this.panel1.Controls.Add(this.tcRt);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 24);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(680, 394);
			this.panel1.TabIndex = 4;
			// 
			// tcRt
			// 
			this.tcRt.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcRt.Location = new System.Drawing.Point(0, 0);
			this.tcRt.Name = "tcRt";
			this.tcRt.SelectedTabPage = this.tpRt;
			this.tcRt.Size = new System.Drawing.Size(680, 394);
			this.tcRt.TabIndex = 2;
			this.tcRt.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpRt,
            this.tpInfo});
			// 
			// tpRt
			// 
			this.tpRt.AutoScroll = true;
			this.tpRt.Name = "tpRt";
			this.tpRt.ShowCloseButton = DevExpress.Utils.DefaultBoolean.False;
			this.tpRt.Size = new System.Drawing.Size(673, 364);
			this.tpRt.Text = "结果";
			// 
			// tpInfo
			// 
			this.tpInfo.AutoScroll = true;
			this.tpInfo.Name = "tpInfo";
			this.tpInfo.ShowCloseButton = DevExpress.Utils.DefaultBoolean.False;
			this.tpInfo.Size = new System.Drawing.Size(673, 367);
			this.tpInfo.Text = "消息";
			// 
			// ResultTable
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "ResultTable";
			this.Size = new System.Drawing.Size(680, 418);
			this.Load += new System.EventHandler(this.ResultTable_Load);
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.tcRt)).EndInit();
			this.tcRt.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraBars.BarManager barManager1;
		private DevExpress.XtraBars.Bar bar2;
		private DevExpress.XtraBars.BarDockControl barDockControlTop;
		private DevExpress.XtraBars.BarDockControl barDockControlBottom;
		private DevExpress.XtraBars.BarDockControl barDockControlLeft;
		private DevExpress.XtraBars.BarDockControl barDockControlRight;
		private DevExpress.XtraBars.BarLargeButtonItem btnExport;
		private System.Windows.Forms.Panel panel1;
		private DevExpress.XtraTab.XtraTabControl tcRt;
		private DevExpress.XtraTab.XtraTabPage tpRt;
		private DevExpress.XtraTab.XtraTabPage tpInfo;
	}
}
