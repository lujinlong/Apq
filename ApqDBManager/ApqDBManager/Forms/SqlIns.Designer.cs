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
			System.Windows.Forms.TreeListViewItemCollection.TreeListViewItemCollectionComparer treeListViewItemCollectionComparer1 = new System.Windows.Forms.TreeListViewItemCollection.TreeListViewItemCollectionComparer();
			this.cmTreeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.toolStrip2 = new System.Windows.Forms.ToolStrip();
			this.tsbExpandAll = new System.Windows.Forms.ToolStripButton();
			this.tsbFail = new System.Windows.Forms.ToolStripButton();
			this.tsbResult = new System.Windows.Forms.ToolStripButton();
			this.treeListView1 = new System.Windows.Forms.TreeListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.imageList2 = new System.Windows.Forms.ImageList(this.components);
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tssbSelect = new System.Windows.Forms.ToolStripSplitButton();
			this.tsbSelectAll = new System.Windows.Forms.ToolStripButton();
			this.tsbReverse = new System.Windows.Forms.ToolStripButton();
			this.cmTreeMenu.SuspendLayout();
			this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.toolStrip2.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
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
			this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.toolStrip2);
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this.treeListView1);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(300, 366);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.LeftToolStripPanelVisible = false;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.RightToolStripPanelVisible = false;
			this.toolStripContainer1.Size = new System.Drawing.Size(300, 416);
			this.toolStripContainer1.TabIndex = 5;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
			// 
			// toolStrip2
			// 
			this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbExpandAll,
            this.tsbFail,
            this.tsbResult});
			this.toolStrip2.Location = new System.Drawing.Point(3, 0);
			this.toolStrip2.Name = "toolStrip2";
			this.toolStrip2.Size = new System.Drawing.Size(189, 25);
			this.toolStrip2.TabIndex = 8;
			this.toolStrip2.Text = "toolStrip2";
			// 
			// tsbExpandAll
			// 
			this.tsbExpandAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbExpandAll.Image = ((System.Drawing.Image)(resources.GetObject("tsbExpandAll.Image")));
			this.tsbExpandAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbExpandAll.Name = "tsbExpandAll";
			this.tsbExpandAll.Size = new System.Drawing.Size(75, 22);
			this.tsbExpandAll.Text = "全部收起(&D)";
			this.tsbExpandAll.Click += new System.EventHandler(this.tsbExpandAll_Click);
			// 
			// tsbFail
			// 
			this.tsbFail.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbFail.Image = ((System.Drawing.Image)(resources.GetObject("tsbFail.Image")));
			this.tsbFail.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbFail.Name = "tsbFail";
			this.tsbFail.Size = new System.Drawing.Size(51, 22);
			this.tsbFail.Text = "失败(&F)";
			this.tsbFail.Click += new System.EventHandler(this.tsbFail_Click);
			// 
			// tsbResult
			// 
			this.tsbResult.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbResult.Image = ((System.Drawing.Image)(resources.GetObject("tsbResult.Image")));
			this.tsbResult.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbResult.Name = "tsbResult";
			this.tsbResult.Size = new System.Drawing.Size(51, 22);
			this.tsbResult.Text = "结果(&T)";
			this.tsbResult.Click += new System.EventHandler(this.tsbResult_Click);
			// 
			// treeListView1
			// 
			this.treeListView1.CheckBoxes = System.Windows.Forms.CheckBoxesTypes.Simple;
			this.treeListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			treeListViewItemCollectionComparer1.Column = 0;
			treeListViewItemCollectionComparer1.SortOrder = System.Windows.Forms.SortOrder.Ascending;
			this.treeListView1.Comparer = treeListViewItemCollectionComparer1;
			this.treeListView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeListView1.Location = new System.Drawing.Point(0, 0);
			this.treeListView1.Name = "treeListView1";
			this.treeListView1.Size = new System.Drawing.Size(300, 366);
			this.treeListView1.SmallImageList = this.imageList2;
			this.treeListView1.TabIndex = 1;
			this.treeListView1.UseCompatibleStateImageBehavior = false;
			this.treeListView1.AfterCollapse += new System.Windows.Forms.TreeListViewEventHandler(this.treeListView1_AfterCollapse);
			this.treeListView1.SelectedIndexChanged += new System.EventHandler(this.treeListView1_SelectedIndexChanged);
			this.treeListView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeListView1_KeyDown);
			this.treeListView1.AfterExpand += new System.Windows.Forms.TreeListViewEventHandler(this.treeListView1_AfterExpand);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "名称";
			this.columnHeader1.Width = 280;
			// 
			// imageList2
			// 
			this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
			this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList2.Images.SetKeyName(0, "");
			this.imageList2.Images.SetKeyName(1, "");
			this.imageList2.Images.SetKeyName(2, "");
			this.imageList2.Images.SetKeyName(3, "");
			// 
			// toolStrip1
			// 
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssbSelect,
            this.tsbSelectAll,
            this.tsbReverse});
			this.toolStrip1.Location = new System.Drawing.Point(3, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(177, 25);
			this.toolStrip1.TabIndex = 6;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tssbSelect
			// 
			this.tssbSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tssbSelect.Image = ((System.Drawing.Image)(resources.GetObject("tssbSelect.Image")));
			this.tssbSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tssbSelect.Name = "tssbSelect";
			this.tssbSelect.Size = new System.Drawing.Size(63, 22);
			this.tssbSelect.Text = "选择(&L)";
			// 
			// tsbSelectAll
			// 
			this.tsbSelectAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("tsbSelectAll.Image")));
			this.tsbSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbSelectAll.Name = "tsbSelectAll";
			this.tsbSelectAll.Size = new System.Drawing.Size(51, 22);
			this.tsbSelectAll.Text = "全选(&A)";
			this.tsbSelectAll.Click += new System.EventHandler(this.tsbSelectAll_Click);
			// 
			// tsbReverse
			// 
			this.tsbReverse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbReverse.Image = ((System.Drawing.Image)(resources.GetObject("tsbReverse.Image")));
			this.tsbReverse.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbReverse.Name = "tsbReverse";
			this.tsbReverse.Size = new System.Drawing.Size(51, 22);
			this.tsbReverse.Text = "反选(&S)";
			this.tsbReverse.Click += new System.EventHandler(this.tsbReverse_Click);
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
			this.Name = "SqlIns";
			this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockLeft;
			this.TabText = "Sql实例";
			this.Text = "Sql实例";
			this.Load += new System.EventHandler(this.SqlIns_Load);
			this.Shown += new System.EventHandler(this.SolutionExplorer_Shown);
			this.cmTreeMenu.ResumeLayout(false);
			this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion

		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ContextMenuStrip cmTreeMenu;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopy;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsbSelectAll;
		private System.Windows.Forms.ToolStripButton tsbReverse;
		private System.Windows.Forms.ToolStripSplitButton tssbSelect;
		private System.Windows.Forms.ToolStrip toolStrip2;
		private System.Windows.Forms.ToolStripButton tsbExpandAll;
		private System.Windows.Forms.ToolStripButton tsbFail;
		private System.Windows.Forms.ToolStripButton tsbResult;
		private System.Windows.Forms.TreeListView treeListView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ImageList imageList2;

	}
}