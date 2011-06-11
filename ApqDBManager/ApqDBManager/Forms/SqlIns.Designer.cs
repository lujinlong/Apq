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
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.tsmiSelect = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiSelectAll = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiReverse = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip2 = new System.Windows.Forms.MenuStrip();
			this.tsmiExpandAll = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiFail = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiResult = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
			this.cmTreeMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
			this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.menuStrip2.SuspendLayout();
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
			this.treeList1.Location = new System.Drawing.Point(0, 0);
			this.treeList1.Name = "treeList1";
			this.treeList1.OptionsBehavior.Editable = false;
			this.treeList1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
			this.treeList1.Size = new System.Drawing.Size(300, 368);
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
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.BottomToolStripPanel
			// 
			this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.menuStrip2);
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this.treeList1);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(300, 368);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(300, 416);
			this.toolStripContainer1.TabIndex = 5;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSelect,
            this.tsmiSelectAll,
            this.tsmiReverse});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(300, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// tsmiSelect
			// 
			this.tsmiSelect.Name = "tsmiSelect";
			this.tsmiSelect.Size = new System.Drawing.Size(59, 20);
			this.tsmiSelect.Text = "选择(&L)";
			// 
			// tsmiSelectAll
			// 
			this.tsmiSelectAll.Name = "tsmiSelectAll";
			this.tsmiSelectAll.Size = new System.Drawing.Size(59, 20);
			this.tsmiSelectAll.Text = "全选(&A)";
			this.tsmiSelectAll.Click += new System.EventHandler(this.tsmiSelectAll_Click);
			// 
			// tsmiReverse
			// 
			this.tsmiReverse.Name = "tsmiReverse";
			this.tsmiReverse.Size = new System.Drawing.Size(59, 20);
			this.tsmiReverse.Text = "反选(&S)";
			this.tsmiReverse.Click += new System.EventHandler(this.tsmiReverse_Click);
			// 
			// menuStrip2
			// 
			this.menuStrip2.Dock = System.Windows.Forms.DockStyle.None;
			this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiExpandAll,
            this.tsmiFail,
            this.tsmiResult});
			this.menuStrip2.Location = new System.Drawing.Point(0, 0);
			this.menuStrip2.Name = "menuStrip2";
			this.menuStrip2.Size = new System.Drawing.Size(300, 24);
			this.menuStrip2.TabIndex = 0;
			this.menuStrip2.Text = "menuStrip2";
			// 
			// tsmiExpandAll
			// 
			this.tsmiExpandAll.Name = "tsmiExpandAll";
			this.tsmiExpandAll.Size = new System.Drawing.Size(83, 20);
			this.tsmiExpandAll.Text = "全部展开(&D)";
			this.tsmiExpandAll.Click += new System.EventHandler(this.tsmiExpandAll_Click);
			// 
			// tsmiFail
			// 
			this.tsmiFail.Name = "tsmiFail";
			this.tsmiFail.Size = new System.Drawing.Size(59, 20);
			this.tsmiFail.Text = "失败(&F)";
			this.tsmiFail.Click += new System.EventHandler(this.tsmiFail_Click);
			// 
			// tsmiResult
			// 
			this.tsmiResult.Name = "tsmiResult";
			this.tsmiResult.Size = new System.Drawing.Size(59, 20);
			this.tsmiResult.Text = "结果(&T)";
			this.tsmiResult.Click += new System.EventHandler(this.tsmiResult_Click);
			// 
			// SqlIns
			// 
			this.ClientSize = new System.Drawing.Size(300, 416);
			this.Controls.Add(this.toolStripContainer1);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft)
						| WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)
						| WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop)
						| WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
			this.HideOnClose = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "SqlIns";
			this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockLeft;
			this.TabText = "Sql实例";
			this.Text = "Sql实例";
			this.Load += new System.EventHandler(this.SqlIns_Load);
			this.Shown += new System.EventHandler(this.SolutionExplorer_Shown);
			((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
			this.cmTreeMenu.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
			this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.menuStrip2.ResumeLayout(false);
			this.menuStrip2.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion

		private DevExpress.XtraTreeList.TreeList treeList1;
		private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ContextMenuStrip cmTreeMenu;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopy;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem tsmiSelect;
		private System.Windows.Forms.ToolStripMenuItem tsmiSelectAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiReverse;
		private System.Windows.Forms.MenuStrip menuStrip2;
		private System.Windows.Forms.ToolStripMenuItem tsmiExpandAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiFail;
		private System.Windows.Forms.ToolStripMenuItem tsmiResult;

	}
}