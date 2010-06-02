namespace QQUnbindTXZ
{
	partial class SolutionExplorer
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SolutionExplorer));
			this.treeList1 = new DevExpress.XtraTreeList.TreeList();
			this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
			this.bar2 = new DevExpress.XtraBars.Bar();
			this.bciArea = new DevExpress.XtraBars.BarCheckItem();
			this.bciServer = new DevExpress.XtraBars.BarCheckItem();
			this.bbiSelectAll = new DevExpress.XtraBars.BarButtonItem();
			this.bbiReverse = new DevExpress.XtraBars.BarButtonItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
			this.SuspendLayout();
			// 
			// treeList1
			// 
			this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn2});
			this.treeList1.DataMember = "Servers";
			this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeList1.Location = new System.Drawing.Point(0, 24);
			this.treeList1.Name = "treeList1";
			this.treeList1.OptionsBehavior.Editable = false;
			this.treeList1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
			this.treeList1.Size = new System.Drawing.Size(329, 394);
			this.treeList1.StateImageList = this.imageList1;
			this.treeList1.TabIndex = 0;
			this.treeList1.GetStateImage += new DevExpress.XtraTreeList.GetStateImageEventHandler(this.treeList1_GetStateImage);
			// 
			// treeListColumn2
			// 
			this.treeListColumn2.Caption = "名称";
			this.treeListColumn2.FieldName = "Name";
			this.treeListColumn2.Name = "treeListColumn2";
			this.treeListColumn2.OptionsColumn.AllowMove = false;
			this.treeListColumn2.OptionsColumn.AllowSort = false;
			this.treeListColumn2.OptionsColumn.ReadOnly = true;
			this.treeListColumn2.Visible = true;
			this.treeListColumn2.VisibleIndex = 0;
			// 
			// repositoryItemCheckEdit1
			// 
			this.repositoryItemCheckEdit1.AutoHeight = false;
			this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "UnChecked.png");
			this.imageList1.Images.SetKeyName(1, "Checked.png");
			this.imageList1.Images.SetKeyName(2, "Indeterminate.png");
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
            this.bciArea,
            this.bciServer,
            this.bbiReverse,
            this.bbiSelectAll});
			this.barManager1.MainMenu = this.bar2;
			this.barManager1.MaxItemId = 5;
			// 
			// bar2
			// 
			this.bar2.BarName = "Main menu";
			this.bar2.DockCol = 0;
			this.bar2.DockRow = 0;
			this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bciArea),
            new DevExpress.XtraBars.LinkPersistInfo(this.bciServer),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiSelectAll),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiReverse)});
			this.bar2.OptionsBar.MultiLine = true;
			this.bar2.OptionsBar.UseWholeRow = true;
			this.bar2.Text = "Main menu";
			// 
			// bciArea
			// 
			this.bciArea.Caption = "区库";
			this.bciArea.Id = 0;
			this.bciArea.Name = "bciArea";
			// 
			// bciServer
			// 
			this.bciServer.Caption = "游戏库";
			this.bciServer.Id = 1;
			this.bciServer.Name = "bciServer";
			// 
			// bbiSelectAll
			// 
			this.bbiSelectAll.Caption = "全选";
			this.bbiSelectAll.Id = 4;
			this.bbiSelectAll.Name = "bbiSelectAll";
			// 
			// bbiReverse
			// 
			this.bbiReverse.Caption = "反选";
			this.bbiReverse.Id = 2;
			this.bbiReverse.Name = "bbiReverse";
			// 
			// SolutionExplorer
			// 
			this.ClientSize = new System.Drawing.Size(329, 418);
			this.Controls.Add(this.treeList1);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((((WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)
						| WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop)
						| WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
			this.HideOnClose = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "SolutionExplorer";
			this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockRight;
			this.TabText = "服务器";
			this.Text = "服务器";
			this.Load += new System.EventHandler(this.SolutionExplorer_Load);
			((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private DevExpress.XtraTreeList.TreeList treeList1;
		private DevExpress.XtraBars.BarManager barManager1;
		private DevExpress.XtraBars.Bar bar2;
		private DevExpress.XtraBars.BarDockControl barDockControlTop;
		private DevExpress.XtraBars.BarDockControl barDockControlBottom;
		private DevExpress.XtraBars.BarDockControl barDockControlLeft;
		private DevExpress.XtraBars.BarDockControl barDockControlRight;
		private DevExpress.XtraBars.BarCheckItem bciArea;
		private DevExpress.XtraBars.BarCheckItem bciServer;
		private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
		private DevExpress.XtraBars.BarButtonItem bbiReverse;
		private System.Windows.Forms.ImageList imageList1;
		private DevExpress.XtraBars.BarButtonItem bbiSelectAll;

	}
}