namespace ApqDBManager.Forms.SrvsMgr
{
	partial class SqlInstance
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SqlInstance));
			this.cmTreeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiTestOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiDel = new System.Windows.Forms.ToolStripMenuItem();
			this.sda = new System.Data.SqlClient.SqlDataAdapter();
			this.scDelete = new System.Data.SqlClient.SqlCommand();
			this.scInsert = new System.Data.SqlClient.SqlCommand();
			this.scSelect = new System.Data.SqlClient.SqlCommand();
			this.scUpdate = new System.Data.SqlClient.SqlCommand();
			this.treeListView1 = new System.Windows.Forms.TreeListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.imageList2 = new System.Windows.Forms.ImageList(this.components);
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
			this.tsbSaveToDB = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbSelectAll = new System.Windows.Forms.ToolStripButton();
			this.tsbReverse = new System.Windows.Forms.ToolStripButton();
			this.tsbExpandAll = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tstbStr = new System.Windows.Forms.ToolStripTextBox();
			this.tssbSlts = new System.Windows.Forms.ToolStripSplitButton();
			this.tsmiSltsUserId = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiSltsPwdD = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiSltsSqlPort = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbDBC = new System.Windows.Forms.ToolStripButton();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.tsslOutInfo = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsslTest = new System.Windows.Forms.ToolStripStatusLabel();
			this.cmTreeMenu.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
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
			// sda
			// 
			this.sda.DeleteCommand = this.scDelete;
			this.sda.InsertCommand = this.scInsert;
			this.sda.SelectCommand = this.scSelect;
			this.sda.UpdateCommand = this.scUpdate;
			// 
			// scSelect
			// 
			this.scSelect.CommandText = "\r\nSELECT * FROM dic_SQLInstanceType;\r\nSELECT * FROM dic_IPType;\r\nSELECT * FROM DB" +
				"Server;\r\nSELECT * FROM SQLInstance;\r\nSELECT * FROM DBC;\r\nSELECT * FROM DBServerI" +
				"P;";
			// 
			// treeListView1
			// 
			this.treeListView1.CheckBoxes = System.Windows.Forms.CheckBoxesTypes.Simple;
			this.treeListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
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
			this.columnHeader6.Text = "SQL端口";
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
            this.tsbRefresh,
            this.tsbSaveToDB,
            this.toolStripSeparator1,
            this.tsbSelectAll,
            this.tsbReverse,
            this.tsbExpandAll,
            this.toolStripSeparator2,
            this.tstbStr,
            this.tssbSlts,
            this.toolStripSeparator3,
            this.tsbDBC});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(780, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tsbRefresh
			// 
			this.tsbRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsbRefresh.Image")));
			this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbRefresh.Name = "tsbRefresh";
			this.tsbRefresh.Size = new System.Drawing.Size(51, 22);
			this.tsbRefresh.Text = "刷新(&R)";
			this.tsbRefresh.Click += new System.EventHandler(this.tsbRefresh_Click);
			// 
			// tsbSaveToDB
			// 
			this.tsbSaveToDB.Image = ((System.Drawing.Image)(resources.GetObject("tsbSaveToDB.Image")));
			this.tsbSaveToDB.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbSaveToDB.Name = "tsbSaveToDB";
			this.tsbSaveToDB.Size = new System.Drawing.Size(67, 22);
			this.tsbSaveToDB.Text = "保存(&S)";
			this.tsbSaveToDB.Click += new System.EventHandler(this.tsbSaveToDB_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
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
			this.tsbReverse.Text = "反选(&V)";
			this.tsbReverse.Click += new System.EventHandler(this.tsbReverse_Click);
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
            this.tsmiSltsSqlPort});
			this.tssbSlts.Image = ((System.Drawing.Image)(resources.GetObject("tssbSlts.Image")));
			this.tssbSlts.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tssbSlts.Name = "tssbSlts";
			this.tssbSlts.Size = new System.Drawing.Size(87, 22);
			this.tssbSlts.Text = "批量设置(&E)";
			// 
			// tsmiSltsUserId
			// 
			this.tsmiSltsUserId.Name = "tsmiSltsUserId";
			this.tsmiSltsUserId.Size = new System.Drawing.Size(112, 22);
			this.tsmiSltsUserId.Text = "登录名";
			this.tsmiSltsUserId.Click += new System.EventHandler(this.tsmiSltsUserId_Click);
			// 
			// tsmiSltsPwdD
			// 
			this.tsmiSltsPwdD.Name = "tsmiSltsPwdD";
			this.tsmiSltsPwdD.Size = new System.Drawing.Size(112, 22);
			this.tsmiSltsPwdD.Text = "密码";
			this.tsmiSltsPwdD.Click += new System.EventHandler(this.tsmiSltsPwdD_Click);
			// 
			// tsmiSltsSqlPort
			// 
			this.tsmiSltsSqlPort.Name = "tsmiSltsSqlPort";
			this.tsmiSltsSqlPort.Size = new System.Drawing.Size(112, 22);
			this.tsmiSltsSqlPort.Text = "SQL端口";
			this.tsmiSltsSqlPort.Click += new System.EventHandler(this.tsmiSltsSqlPort_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbDBC
			// 
			this.tsbDBC.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbDBC.Image = ((System.Drawing.Image)(resources.GetObject("tsbDBC.Image")));
			this.tsbDBC.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbDBC.Name = "tsbDBC";
			this.tsbDBC.Size = new System.Drawing.Size(87, 22);
			this.tsbDBC.Text = "DB连接管理(&C)";
			this.tsbDBC.Click += new System.EventHandler(this.tsbDBC_Click);
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
			// SqlInstance
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.ClientSize = new System.Drawing.Size(780, 366);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.treeListView1);
			this.Controls.Add(this.toolStrip1);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "SqlInstance";
			this.TabText = "实例管理";
			this.Text = "实例管理";
			this.Load += new System.EventHandler(this.SqlInstance_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SqlInstance_FormClosing);
			this.cmTreeMenu.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip cmTreeMenu;
		private System.Windows.Forms.ToolStripMenuItem tsmiTestOpen;
		private System.Windows.Forms.ToolStripMenuItem tsmiAdd;
		private System.Windows.Forms.ToolStripMenuItem tsmiDel;
		private System.Data.SqlClient.SqlDataAdapter sda;
		private System.Data.SqlClient.SqlCommand scDelete;
		private System.Data.SqlClient.SqlCommand scInsert;
		private System.Data.SqlClient.SqlCommand scSelect;
		private System.Data.SqlClient.SqlCommand scUpdate;
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
		private System.Windows.Forms.ToolStripButton tsbRefresh;
		private System.Windows.Forms.ToolStripButton tsbDBC;
		private System.Windows.Forms.ToolStripButton tsbExpandAll;
		private System.Windows.Forms.ToolStripButton tsbSaveToDB;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton tsbSelectAll;
		private System.Windows.Forms.ToolStripButton tsbReverse;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripTextBox tstbStr;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripSplitButton tssbSlts;
		private System.Windows.Forms.ToolStripMenuItem tsmiSltsUserId;
		private System.Windows.Forms.ToolStripMenuItem tsmiSltsPwdD;
		private System.Windows.Forms.ToolStripMenuItem tsmiSltsSqlPort;

	}
}