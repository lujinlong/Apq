namespace ApqDBCManager.Forms
{
	partial class DBI
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
			System.Windows.Forms.TreeListViewItemCollection.TreeListViewItemCollectionComparer treeListViewItemCollectionComparer1 = new System.Windows.Forms.TreeListViewItemCollection.TreeListViewItemCollectionComparer();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBI));
			this.cmTreeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiTestOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiDel = new System.Windows.Forms.ToolStripMenuItem();
			this.treeListView1 = new System.Windows.Forms.TreeListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.imageList2 = new System.Windows.Forms.ImageList(this.components);
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsbSave = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbSelectAll = new System.Windows.Forms.ToolStripButton();
			this.tsbReverse = new System.Windows.Forms.ToolStripButton();
			this.tsbExpandAll = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tstbStr = new System.Windows.Forms.ToolStripTextBox();
			this.tssbSlts = new System.Windows.Forms.ToolStripSplitButton();
			this.tsmiSltsUserId = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiSltsPwdD = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiSltsPort = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbSaveFile = new System.Windows.Forms.ToolStripButton();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.tsslOutInfo = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsslTest = new System.Windows.Forms.ToolStripStatusLabel();
			this.sfd = new System.Windows.Forms.SaveFileDialog();
			this.actionList1 = new Crad.Windows.Forms.Actions.ActionList();
			this.acSave = new Crad.Windows.Forms.Actions.Action();
			this.acSelectAll = new Crad.Windows.Forms.Actions.Action();
			this.acReverse = new Crad.Windows.Forms.Actions.Action();
			this.acExpandAll = new Crad.Windows.Forms.Actions.Action();
			this.acCreateFile = new Crad.Windows.Forms.Actions.Action();
			this.cmTreeMenu.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.actionList1)).BeginInit();
			this.SuspendLayout();
			// 
			// cmTreeMenu
			// 
			this.cmTreeMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTestOpen,
            this.tsmiAdd,
            this.tsmiDel});
			this.cmTreeMenu.Name = "contextMenuStrip1";
			this.cmTreeMenu.Size = new System.Drawing.Size(113, 70);
			// 
			// tsmiTestOpen
			// 
			this.tsmiTestOpen.Name = "tsmiTestOpen";
			this.tsmiTestOpen.Size = new System.Drawing.Size(112, 22);
			this.tsmiTestOpen.Text = "测试(&T)";
			this.tsmiTestOpen.Click += new System.EventHandler(this.tsmiTestOpen_Click);
			// 
			// tsmiAdd
			// 
			this.tsmiAdd.Name = "tsmiAdd";
			this.tsmiAdd.Size = new System.Drawing.Size(112, 22);
			this.tsmiAdd.Text = "添加(&A)";
			this.tsmiAdd.Click += new System.EventHandler(this.tsmiAdd_Click);
			// 
			// tsmiDel
			// 
			this.tsmiDel.Name = "tsmiDel";
			this.tsmiDel.Size = new System.Drawing.Size(112, 22);
			this.tsmiDel.Text = "删除(&D)";
			this.tsmiDel.Click += new System.EventHandler(this.tsmiDel_Click);
			// 
			// treeListView1
			// 
			this.treeListView1.CheckBoxes = System.Windows.Forms.CheckBoxesTypes.Simple;
			this.treeListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader7,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
			treeListViewItemCollectionComparer1.Column = 0;
			treeListViewItemCollectionComparer1.SortOrder = System.Windows.Forms.SortOrder.Ascending;
			this.treeListView1.Comparer = treeListViewItemCollectionComparer1;
			this.treeListView1.ContextMenuStrip = this.cmTreeMenu;
			this.treeListView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeListView1.LabelEdit = true;
			this.treeListView1.LabelWrap = false;
			this.treeListView1.Location = new System.Drawing.Point(0, 25);
			this.treeListView1.Name = "treeListView1";
			this.treeListView1.Size = new System.Drawing.Size(780, 341);
			this.treeListView1.SmallImageList = this.imageList2;
			this.treeListView1.TabIndex = 2;
			this.treeListView1.UseCompatibleStateImageBehavior = false;
			this.treeListView1.AfterLabelEdit += new System.Windows.Forms.TreeListViewLabelEditEventHandler(this.treeListView1_AfterLabelEdit);
			this.treeListView1.BeforeLabelEdit += new System.Windows.Forms.TreeListViewBeforeLabelEditEventHandler(this.treeListView1_BeforeLabelEdit);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "名称";
			this.columnHeader1.Width = 189;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "服务器";
			this.columnHeader2.Width = 132;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "DBMS";
			this.columnHeader7.Width = 100;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "登录名";
			this.columnHeader3.Width = 76;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "密码";
			this.columnHeader4.Width = 98;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "IP";
			this.columnHeader5.Width = 130;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "端口";
			this.columnHeader6.Width = 80;
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
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSave,
            this.toolStripSeparator1,
            this.tsbSelectAll,
            this.tsbReverse,
            this.tsbExpandAll,
            this.toolStripSeparator2,
            this.tstbStr,
            this.tssbSlts,
            this.toolStripSeparator3,
            this.tsbSaveFile});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(780, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tsbSave
			// 
			this.actionList1.SetAction(this.tsbSave, this.acSave);
			this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbSave.Name = "tsbSave";
			this.tsbSave.Size = new System.Drawing.Size(23, 22);
			this.tsbSave.Text = "保存(&S)";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbSelectAll
			// 
			this.actionList1.SetAction(this.tsbSelectAll, this.acSelectAll);
			this.tsbSelectAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbSelectAll.Name = "tsbSelectAll";
			this.tsbSelectAll.Size = new System.Drawing.Size(51, 22);
			this.tsbSelectAll.Text = "全选(&A)";
			// 
			// tsbReverse
			// 
			this.actionList1.SetAction(this.tsbReverse, this.acReverse);
			this.tsbReverse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbReverse.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbReverse.Name = "tsbReverse";
			this.tsbReverse.Size = new System.Drawing.Size(51, 22);
			this.tsbReverse.Text = "反选(&V)";
			// 
			// tsbExpandAll
			// 
			this.actionList1.SetAction(this.tsbExpandAll, this.acExpandAll);
			this.tsbExpandAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbExpandAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbExpandAll.Name = "tsbExpandAll";
			this.tsbExpandAll.Size = new System.Drawing.Size(75, 22);
			this.tsbExpandAll.Text = "全部收起(&D)";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// tstbStr
			// 
			this.tstbStr.Name = "tstbStr";
			this.tstbStr.Size = new System.Drawing.Size(200, 25);
			// 
			// tssbSlts
			// 
			this.tssbSlts.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tssbSlts.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSltsUserId,
            this.tsmiSltsPwdD,
            this.tsmiSltsPort});
			this.tssbSlts.Image = ((System.Drawing.Image)(resources.GetObject("tssbSlts.Image")));
			this.tssbSlts.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tssbSlts.Name = "tssbSlts";
			this.tssbSlts.Size = new System.Drawing.Size(87, 22);
			this.tssbSlts.Text = "批量设置(&E)";
			// 
			// tsmiSltsUserId
			// 
			this.tsmiSltsUserId.Name = "tsmiSltsUserId";
			this.tsmiSltsUserId.Size = new System.Drawing.Size(106, 22);
			this.tsmiSltsUserId.Text = "登录名";
			this.tsmiSltsUserId.Click += new System.EventHandler(this.tsmiSltsUserId_Click);
			// 
			// tsmiSltsPwdD
			// 
			this.tsmiSltsPwdD.Name = "tsmiSltsPwdD";
			this.tsmiSltsPwdD.Size = new System.Drawing.Size(106, 22);
			this.tsmiSltsPwdD.Text = "密码";
			this.tsmiSltsPwdD.Click += new System.EventHandler(this.tsmiSltsPwdD_Click);
			// 
			// tsmiSltsPort
			// 
			this.tsmiSltsPort.Name = "tsmiSltsPort";
			this.tsmiSltsPort.Size = new System.Drawing.Size(106, 22);
			this.tsmiSltsPort.Text = "端口";
			this.tsmiSltsPort.Click += new System.EventHandler(this.tsmiSltsPort_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbSaveFile
			// 
			this.actionList1.SetAction(this.tsbSaveFile, this.acCreateFile);
			this.tsbSaveFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbSaveFile.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbSaveFile.Name = "tsbSaveFile";
			this.tsbSaveFile.Size = new System.Drawing.Size(75, 22);
			this.tsbSaveFile.Text = "生成文件(&G)";
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslOutInfo,
            this.tsslTest});
			this.statusStrip1.Location = new System.Drawing.Point(0, 344);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(780, 22);
			this.statusStrip1.TabIndex = 3;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// tsslOutInfo
			// 
			this.tsslOutInfo.AutoSize = false;
			this.tsslOutInfo.Name = "tsslOutInfo";
			this.tsslOutInfo.Size = new System.Drawing.Size(300, 17);
			this.tsslOutInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tsslTest
			// 
			this.tsslTest.Name = "tsslTest";
			this.tsslTest.Size = new System.Drawing.Size(0, 17);
			// 
			// sfd
			// 
			this.sfd.DefaultExt = "res";
			this.sfd.Filter = "DBC文件|*.res";
			this.sfd.RestoreDirectory = true;
			// 
			// actionList1
			// 
			this.actionList1.Actions.Add(this.acSave);
			this.actionList1.Actions.Add(this.acReverse);
			this.actionList1.Actions.Add(this.acExpandAll);
			this.actionList1.Actions.Add(this.acSelectAll);
			this.actionList1.Actions.Add(this.acCreateFile);
			this.actionList1.ContainerControl = this;
			// 
			// acSave
			// 
			this.acSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.acSave.Text = "保存(&S)";
			this.acSave.ToolTipText = "保存";
			this.acSave.Execute += new System.EventHandler(this.acSave_Execute);
			// 
			// acSelectAll
			// 
			this.acSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
			this.acSelectAll.Text = "全选(&A)";
			this.acSelectAll.ToolTipText = "全选";
			this.acSelectAll.Execute += new System.EventHandler(this.acSelectAll_Execute);
			// 
			// acReverse
			// 
			this.acReverse.Text = "反选(&V)";
			this.acReverse.ToolTipText = "反选";
			this.acReverse.Execute += new System.EventHandler(this.acReverse_Execute);
			// 
			// acExpandAll
			// 
			this.acExpandAll.Text = "全部收起(&D)";
			this.acExpandAll.ToolTipText = "全部收起";
			this.acExpandAll.Execute += new System.EventHandler(this.acExpandAll_Execute);
			// 
			// acCreateFile
			// 
			this.acCreateFile.Text = "生成文件(&G)";
			this.acCreateFile.ToolTipText = "生成文件";
			this.acCreateFile.Execute += new System.EventHandler(this.acCreateFile_Execute);
			// 
			// DBI
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.ClientSize = new System.Drawing.Size(780, 366);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.treeListView1);
			this.Controls.Add(this.toolStrip1);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "DBI";
			this.TabText = "数据库列表";
			this.Text = "数据库列表";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DBI_FormClosing);
			this.Load += new System.EventHandler(this.DBI_Load);
			this.cmTreeMenu.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.actionList1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip cmTreeMenu;
		private System.Windows.Forms.ToolStripMenuItem tsmiTestOpen;
		private System.Windows.Forms.ToolStripMenuItem tsmiAdd;
		private System.Windows.Forms.ToolStripMenuItem tsmiDel;
		private System.Windows.Forms.TreeListView treeListView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ImageList imageList2;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel tsslOutInfo;
		private System.Windows.Forms.ToolStripStatusLabel tsslTest;
		private System.Windows.Forms.ToolStripButton tsbExpandAll;
		private System.Windows.Forms.ToolStripButton tsbSaveFile;
		private System.Windows.Forms.ToolStripButton tsbSelectAll;
		private System.Windows.Forms.ToolStripButton tsbReverse;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripTextBox tstbStr;
		private System.Windows.Forms.ToolStripSplitButton tssbSlts;
		private System.Windows.Forms.ToolStripMenuItem tsmiSltsUserId;
		private System.Windows.Forms.ToolStripMenuItem tsmiSltsPwdD;
		private System.Windows.Forms.ToolStripMenuItem tsmiSltsPort;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton tsbSave;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.SaveFileDialog sfd;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private Crad.Windows.Forms.Actions.ActionList actionList1;
		private Crad.Windows.Forms.Actions.Action acSave;
		private Crad.Windows.Forms.Actions.Action acReverse;
		private Crad.Windows.Forms.Actions.Action acExpandAll;
		private Crad.Windows.Forms.Actions.Action acSelectAll;
		private Crad.Windows.Forms.Actions.Action acCreateFile;

	}
}