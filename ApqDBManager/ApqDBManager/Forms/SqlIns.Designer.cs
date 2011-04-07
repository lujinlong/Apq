namespace ApqDBManager.Forms
{
	partial class SqlIns
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SqlIns));
			this.treeList1 = new DevExpress.XtraTreeList.TreeList();
			this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.cmTreeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
			this.bar2 = new DevExpress.XtraBars.Bar();
			this.bsiSelect = new DevExpress.XtraBars.BarSubItem();
			this.bbiSelectAll = new DevExpress.XtraBars.BarButtonItem();
			this.bbiReverse = new DevExpress.XtraBars.BarButtonItem();
			this.bar1 = new DevExpress.XtraBars.Bar();
			this.bbiExpandAll = new DevExpress.XtraBars.BarButtonItem();
			this.bbiFail = new DevExpress.XtraBars.BarButtonItem();
			this.bbiResult = new DevExpress.XtraBars.BarButtonItem();
			this.bar4 = new DevExpress.XtraBars.Bar();
			this.bsiInfo = new DevExpress.XtraBars.BarStaticItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
			this.cmTreeMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
			this.SuspendLayout();
			// 
			// treeList1
			// 
			this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
			this.treeList1.ContextMenuStrip = this.cmTreeMenu;
			this.treeList1.DataMember = "SqlInstance";
			this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeList1.KeyFieldName = "SqlID";
			this.treeList1.Location = new System.Drawing.Point(0, 24);
			this.treeList1.Name = "treeList1";
			this.treeList1.OptionsBehavior.Editable = false;
			this.treeList1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
			this.treeList1.Size = new System.Drawing.Size(300, 345);
			this.treeList1.StateImageList = this.imageList1;
			this.treeList1.TabIndex = 0;
			this.treeList1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeList1_KeyDown);
			this.treeList1.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeList1_FocusedNodeChanged);
			this.treeList1.EditorKeyUp += new System.Windows.Forms.KeyEventHandler(this.treeList1_EditorKeyUp);
			this.treeList1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeList1_MouseClick);
			this.treeList1.GetStateImage += new DevExpress.XtraTreeList.GetStateImageEventHandler(this.treeList1_GetStateImage);
			this.treeList1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.treeList1_KeyUp);
			// 
			// treeListColumn1
			// 
			this.treeListColumn1.Caption = "名称";
			this.treeListColumn1.FieldName = "SqlName";
			this.treeListColumn1.MinWidth = 37;
			this.treeListColumn1.Name = "treeListColumn1";
			this.treeListColumn1.OptionsColumn.AllowMove = false;
			this.treeListColumn1.OptionsColumn.AllowSort = false;
			this.treeListColumn1.OptionsColumn.ReadOnly = true;
			this.treeListColumn1.Visible = true;
			this.treeListColumn1.VisibleIndex = 0;
			// 
			// cmTreeMenu
			// 
			this.cmTreeMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCopy});
			this.cmTreeMenu.Name = "contextMenuStrip1";
			this.cmTreeMenu.Size = new System.Drawing.Size(113, 26);
			// 
			// tsmiCopy
			// 
			this.tsmiCopy.Name = "tsmiCopy";
			this.tsmiCopy.Size = new System.Drawing.Size(112, 22);
			this.tsmiCopy.Text = "复制(&C)";
			this.tsmiCopy.Click += new System.EventHandler(this.tsmiCopy_Click);
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
            this.bar2,
            this.bar1,
            this.bar4});
			this.barManager1.DockControls.Add(this.barDockControlTop);
			this.barManager1.DockControls.Add(this.barDockControlBottom);
			this.barManager1.DockControls.Add(this.barDockControlLeft);
			this.barManager1.DockControls.Add(this.barDockControlRight);
			this.barManager1.Form = this;
			this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiReverse,
            this.bbiSelectAll,
            this.bbiFail,
            this.bbiResult,
            this.bbiExpandAll,
            this.bsiSelect,
            this.bsiInfo});
			this.barManager1.MainMenu = this.bar2;
			this.barManager1.MaxItemId = 23;
			this.barManager1.StatusBar = this.bar4;
			// 
			// bar2
			// 
			this.bar2.BarName = "Main menu";
			this.bar2.DockCol = 0;
			this.bar2.DockRow = 0;
			this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bsiSelect),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiSelectAll),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiReverse)});
			this.bar2.OptionsBar.MultiLine = true;
			this.bar2.OptionsBar.UseWholeRow = true;
			this.bar2.Text = "Main menu";
			// 
			// bsiSelect
			// 
			this.bsiSelect.Caption = "选择(&L)";
			this.bsiSelect.Id = 8;
			this.bsiSelect.Name = "bsiSelect";
			// 
			// bbiSelectAll
			// 
			this.bbiSelectAll.Caption = "全选(&A)";
			this.bbiSelectAll.Id = 4;
			this.bbiSelectAll.Name = "bbiSelectAll";
			this.bbiSelectAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiSelectAll_ItemClick);
			// 
			// bbiReverse
			// 
			this.bbiReverse.Caption = "反选(&S)";
			this.bbiReverse.Id = 2;
			this.bbiReverse.Name = "bbiReverse";
			this.bbiReverse.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiReverse_ItemClick);
			// 
			// bar1
			// 
			this.bar1.BarName = "Custom 1";
			this.bar1.DockCol = 0;
			this.bar1.DockRow = 1;
			this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
			this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiExpandAll),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiFail),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiResult)});
			this.bar1.OptionsBar.AllowRename = true;
			this.bar1.Text = "Custom 1";
			// 
			// bbiExpandAll
			// 
			this.bbiExpandAll.Caption = "全部展开(&D)";
			this.bbiExpandAll.Id = 7;
			this.bbiExpandAll.Name = "bbiExpandAll";
			this.bbiExpandAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiExpandAll_ItemClick);
			// 
			// bbiFail
			// 
			this.bbiFail.Caption = "失败(&F)";
			this.bbiFail.Id = 5;
			this.bbiFail.Name = "bbiFail";
			this.bbiFail.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiFail_ItemClick);
			// 
			// bbiResult
			// 
			this.bbiResult.Caption = "结果(&T)";
			this.bbiResult.Id = 6;
			this.bbiResult.Name = "bbiResult";
			this.bbiResult.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiResult_ItemClick);
			// 
			// bar4
			// 
			this.bar4.BarName = "Custom 5";
			this.bar4.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
			this.bar4.DockCol = 0;
			this.bar4.DockRow = 0;
			this.bar4.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
			this.bar4.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bsiInfo)});
			this.bar4.OptionsBar.AllowQuickCustomization = false;
			this.bar4.OptionsBar.DrawDragBorder = false;
			this.bar4.OptionsBar.UseWholeRow = true;
			this.bar4.Text = "Custom 5";
			// 
			// bsiInfo
			// 
			this.bsiInfo.AutoSize = DevExpress.XtraBars.BarStaticItemSize.None;
			this.bsiInfo.Id = 22;
			this.bsiInfo.Name = "bsiInfo";
			this.bsiInfo.TextAlignment = System.Drawing.StringAlignment.Near;
			this.bsiInfo.Width = 300;
			// 
			// SqlIns
			// 
			this.ClientSize = new System.Drawing.Size(300, 416);
			this.Controls.Add(this.treeList1);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft)
						| WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)
						| WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop)
						| WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
			this.HideOnClose = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "SqlIns";
			this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockLeft;
			this.TabText = "Sql实例";
			this.Text = "Sql实例";
			this.Load += new System.EventHandler(this.SqlIns_Load);
			this.Shown += new System.EventHandler(this.SolutionExplorer_Shown);
			((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
			this.cmTreeMenu.ResumeLayout(false);
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
		private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
		private DevExpress.XtraBars.BarButtonItem bbiReverse;
		private System.Windows.Forms.ImageList imageList1;
		private DevExpress.XtraBars.BarButtonItem bbiSelectAll;
		private DevExpress.XtraBars.Bar bar1;
		private DevExpress.XtraBars.BarButtonItem bbiFail;
		private DevExpress.XtraBars.BarButtonItem bbiResult;
		private DevExpress.XtraBars.BarButtonItem bbiExpandAll;
		private System.Windows.Forms.ContextMenuStrip cmTreeMenu;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopy;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
		private DevExpress.XtraBars.BarSubItem bsiSelect;
		private DevExpress.XtraBars.Bar bar4;
		private DevExpress.XtraBars.BarStaticItem bsiInfo;

	}
}