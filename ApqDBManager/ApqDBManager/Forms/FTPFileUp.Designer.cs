namespace ApqDBManager.Forms
{
	partial class FTPFileUp
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FTPFileUp));
			System.Windows.Forms.TreeListViewItemCollection.TreeListViewItemCollectionComparer treeListViewItemCollectionComparer2 = new System.Windows.Forms.TreeListViewItemCollection.TreeListViewItemCollectionComparer();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
			this.bar3 = new DevExpress.XtraBars.Bar();
			this.bsiState = new DevExpress.XtraBars.BarStaticItem();
			this.bsiStateFileUp = new DevExpress.XtraBars.BarStaticItem();
			this.beiPb1 = new DevExpress.XtraBars.BarEditItem();
			this.ripb = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
			this.bar1 = new DevExpress.XtraBars.Bar();
			this.btnRefresh = new DevExpress.XtraBars.BarButtonItem();
			this.btnUp = new DevExpress.XtraBars.BarButtonItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.beDBFolder_Up = new DevExpress.XtraEditors.ButtonEdit();
			this._UI = new ApqDBManager.Forms.ErrList_XSD();
			this._Sqls = new ApqDBManager.Forms.SrvsMgr.SrvsMgr_XSD();
			this.treeListView1 = new System.Windows.Forms.TreeListView();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ripb)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.beDBFolder_Up.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._UI)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._Sqls)).BeginInit();
			this.SuspendLayout();
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
			this.imageList1.Images.SetKeyName(0, "");
			this.imageList1.Images.SetKeyName(1, "");
			this.imageList1.Images.SetKeyName(2, "");
			this.imageList1.Images.SetKeyName(3, "");
			// 
			// labelControl1
			// 
			this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Coral;
			this.labelControl1.Appearance.Options.UseForeColor = true;
			this.labelControl1.Location = new System.Drawing.Point(12, 57);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(256, 14);
			this.labelControl1.TabIndex = 3;
			this.labelControl1.Text = "本地资源:只会上传已加载到列表中且选中的文件";
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
            this.bsiState,
            this.bsiStateFileUp,
            this.btnRefresh,
            this.btnUp,
            this.beiPb1});
			this.barManager1.MaxItemId = 6;
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
            new DevExpress.XtraBars.LinkPersistInfo(this.bsiStateFileUp),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.beiPb1, "", false, true, true, 459)});
			this.bar3.OptionsBar.AllowQuickCustomization = false;
			this.bar3.OptionsBar.DrawDragBorder = false;
			this.bar3.OptionsBar.UseWholeRow = true;
			this.bar3.Text = "Status bar";
			// 
			// bsiState
			// 
			this.bsiState.AutoSize = DevExpress.XtraBars.BarStaticItemSize.Spring;
			this.bsiState.Id = 0;
			this.bsiState.Name = "bsiState";
			this.bsiState.TextAlignment = System.Drawing.StringAlignment.Near;
			this.bsiState.Width = 32;
			// 
			// bsiStateFileUp
			// 
			this.bsiStateFileUp.Id = 2;
			this.bsiStateFileUp.Name = "bsiStateFileUp";
			this.bsiStateFileUp.TextAlignment = System.Drawing.StringAlignment.Near;
			// 
			// beiPb1
			// 
			this.beiPb1.Caption = "barEditItem1";
			this.beiPb1.Edit = this.ripb;
			this.beiPb1.Id = 5;
			this.beiPb1.Name = "beiPb1";
			// 
			// ripb
			// 
			this.ripb.Name = "ripb";
			// 
			// bar1
			// 
			this.bar1.BarName = "Custom 3";
			this.bar1.DockCol = 0;
			this.bar1.DockRow = 0;
			this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnRefresh),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnUp)});
			this.bar1.Text = "Custom 3";
			// 
			// btnRefresh
			// 
			this.btnRefresh.Caption = "刷新";
			this.btnRefresh.Id = 3;
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRefresh_ItemClick);
			// 
			// btnUp
			// 
			this.btnUp.Caption = "上传";
			this.btnUp.Id = 4;
			this.btnUp.Name = "btnUp";
			this.btnUp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnUp_ItemClick);
			// 
			// beDBFolder_Up
			// 
			this.beDBFolder_Up.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.beDBFolder_Up.Location = new System.Drawing.Point(123, 30);
			this.beDBFolder_Up.Name = "beDBFolder_Up";
			this.beDBFolder_Up.Size = new System.Drawing.Size(577, 21);
			this.beDBFolder_Up.TabIndex = 12;
			this.beDBFolder_Up.EditValueChanged += new System.EventHandler(this.beDBFolder_Up_EditValueChanged);
			// 
			// _UI
			// 
			this._UI.DataSetName = "UI";
			this._UI.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// _Sqls
			// 
			this._Sqls.DataSetName = "Sqls";
			this._Sqls.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// treeListView1
			// 
			this.treeListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			treeListViewItemCollectionComparer2.Column = 0;
			treeListViewItemCollectionComparer2.SortOrder = System.Windows.Forms.SortOrder.Ascending;
			this.treeListView1.Comparer = treeListViewItemCollectionComparer2;
			this.treeListView1.Location = new System.Drawing.Point(0, 77);
			this.treeListView1.Name = "treeListView1";
			this.treeListView1.Size = new System.Drawing.Size(712, 358);
			this.treeListView1.SmallImageList = this.imageList1;
			this.treeListView1.TabIndex = 13;
			this.treeListView1.UseCompatibleStateImageBehavior = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(10, 35);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(107, 12);
			this.label1.TabIndex = 14;
			this.label1.Text = "上传到远程根目录:";
			// 
			// FTPFileUp
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(712, 466);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.treeListView1);
			this.Controls.Add(this.beDBFolder_Up);
			this.Controls.Add(this.labelControl1);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(720, 500);
			this.Name = "FTPFileUp";
			this.TabText = "文件上传";
			this.Text = "FTP文件上传";
			this.Deactivate += new System.EventHandler(this.FileUp_Deactivate);
			this.Load += new System.EventHandler(this.FileUp_Load);
			this.Activated += new System.EventHandler(this.FileUp_Activated);
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ripb)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.beDBFolder_Up.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._UI)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._Sqls)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.LabelControl labelControl1;
		private DevExpress.XtraBars.BarManager barManager1;
		private DevExpress.XtraBars.Bar bar3;
		private DevExpress.XtraBars.BarDockControl barDockControlTop;
		private DevExpress.XtraBars.BarDockControl barDockControlBottom;
		private DevExpress.XtraBars.BarDockControl barDockControlLeft;
		private DevExpress.XtraBars.BarDockControl barDockControlRight;
		private DevExpress.XtraBars.BarStaticItem bsiState;
		private System.Windows.Forms.ImageList imageList1;
		private DevExpress.XtraBars.BarStaticItem bsiStateFileUp;
		private DevExpress.XtraBars.Bar bar1;
		private DevExpress.XtraBars.BarButtonItem btnRefresh;
		private DevExpress.XtraBars.BarButtonItem btnUp;
		private DevExpress.XtraBars.BarEditItem beiPb1;
		private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar ripb;
		private DevExpress.XtraEditors.ButtonEdit beDBFolder_Up;
		private ErrList_XSD _UI;
		private ApqDBManager.Forms.SrvsMgr.SrvsMgr_XSD _Sqls;
		private System.Windows.Forms.TreeListView treeListView1;
		private System.Windows.Forms.Label label1;

	}
}