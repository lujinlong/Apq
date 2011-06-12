namespace ApqDBManager.Forms.SrvsMgr
{
	partial class DBC
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBC));
			this.cmGridMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiTestOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.sda = new System.Data.SqlClient.SqlDataAdapter();
			this.scDelete = new System.Data.SqlClient.SqlCommand();
			this.scInsert = new System.Data.SqlClient.SqlCommand();
			this.scSelect = new System.Data.SqlClient.SqlCommand();
			this.scUpdate = new System.Data.SqlClient.SqlCommand();
			this.sfd = new System.Windows.Forms.SaveFileDialog();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.DBID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.computerIDDataGridViewComBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.computerBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.srvsMgr_XSD = new ApqDBManager.Forms.SrvsMgr.SrvsMgr_XSD();
			this.sqlIDDataGridViewComboBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.sqlInstanceBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.dBNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dBCTypeDataGridViewComboBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.dBCTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.userIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.pwdDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.useTrustedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.mirrorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.optionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dBCBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
			this.tsbSaveToDB = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbSelectAll = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tstbStr = new System.Windows.Forms.ToolStripTextBox();
			this.tssbSlts = new System.Windows.Forms.ToolStripSplitButton();
			this.tsmiSltsUserId = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiSltsPwdD = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tssbCreateCSFile = new System.Windows.Forms.ToolStripSplitButton();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.tsslOutInfo = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsslTest = new System.Windows.Forms.ToolStripStatusLabel();
			this.cmGridMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.computerBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.srvsMgr_XSD)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sqlInstanceBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dBCTypeBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dBCBindingSource)).BeginInit();
			this.toolStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmGridMenu
			// 
			this.cmGridMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTestOpen});
			this.cmGridMenu.Name = "contextMenuStrip1";
			this.cmGridMenu.Size = new System.Drawing.Size(113, 26);
			// 
			// tsmiTestOpen
			// 
			this.tsmiTestOpen.Name = "tsmiTestOpen";
			this.tsmiTestOpen.Size = new System.Drawing.Size(112, 22);
			this.tsmiTestOpen.Text = "测试(&T)";
			this.tsmiTestOpen.Click += new System.EventHandler(this.tsmiTestOpen_Click);
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
			// sfd
			// 
			this.sfd.Filter = "DBC文件(*.res)|*.res";
			// 
			// dataGridView1
			// 
			this.dataGridView1.AutoGenerateColumns = false;
			this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
			this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DBID,
            this.computerIDDataGridViewComBoxColumn,
            this.sqlIDDataGridViewComboBoxColumn,
            this.dBNameDataGridViewTextBoxColumn,
            this.dBCTypeDataGridViewComboBoxColumn,
            this.userIdDataGridViewTextBoxColumn,
            this.pwdDDataGridViewTextBoxColumn,
            this.useTrustedDataGridViewCheckBoxColumn,
            this.mirrorDataGridViewTextBoxColumn,
            this.optionDataGridViewTextBoxColumn});
			this.dataGridView1.ContextMenuStrip = this.cmGridMenu;
			this.dataGridView1.DataSource = this.dBCBindingSource;
			this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView1.Location = new System.Drawing.Point(0, 25);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowTemplate.Height = 23;
			this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView1.Size = new System.Drawing.Size(692, 319);
			this.dataGridView1.TabIndex = 6;
			// 
			// DBID
			// 
			this.DBID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.DBID.DataPropertyName = "DBID";
			this.DBID.HeaderText = "编号";
			this.DBID.Name = "DBID";
			this.DBID.ReadOnly = true;
			this.DBID.Width = 60;
			// 
			// computerIDDataGridViewComBoxColumn
			// 
			this.computerIDDataGridViewComBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.computerIDDataGridViewComBoxColumn.DataPropertyName = "ComputerID";
			this.computerIDDataGridViewComBoxColumn.DataSource = this.computerBindingSource;
			this.computerIDDataGridViewComBoxColumn.DisplayMember = "ComputerName";
			this.computerIDDataGridViewComBoxColumn.HeaderText = "服务器";
			this.computerIDDataGridViewComBoxColumn.Name = "computerIDDataGridViewComBoxColumn";
			this.computerIDDataGridViewComBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.computerIDDataGridViewComBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.computerIDDataGridViewComBoxColumn.ValueMember = "ComputerID";
			// 
			// computerBindingSource
			// 
			this.computerBindingSource.DataMember = "Computer";
			this.computerBindingSource.DataSource = this.srvsMgr_XSD;
			// 
			// srvsMgr_XSD
			// 
			this.srvsMgr_XSD.DataSetName = "SrvsMgr_XSD";
			this.srvsMgr_XSD.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// sqlIDDataGridViewComboBoxColumn
			// 
			this.sqlIDDataGridViewComboBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.sqlIDDataGridViewComboBoxColumn.DataPropertyName = "SqlID";
			this.sqlIDDataGridViewComboBoxColumn.DataSource = this.sqlInstanceBindingSource;
			this.sqlIDDataGridViewComboBoxColumn.DisplayMember = "SqlName";
			this.sqlIDDataGridViewComboBoxColumn.HeaderText = "实例";
			this.sqlIDDataGridViewComboBoxColumn.Name = "sqlIDDataGridViewComboBoxColumn";
			this.sqlIDDataGridViewComboBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.sqlIDDataGridViewComboBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.sqlIDDataGridViewComboBoxColumn.ValueMember = "SqlID";
			// 
			// sqlInstanceBindingSource
			// 
			this.sqlInstanceBindingSource.DataMember = "SqlInstance";
			this.sqlInstanceBindingSource.DataSource = this.srvsMgr_XSD;
			// 
			// dBNameDataGridViewTextBoxColumn
			// 
			this.dBNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dBNameDataGridViewTextBoxColumn.DataPropertyName = "DBName";
			this.dBNameDataGridViewTextBoxColumn.HeaderText = "数据库名";
			this.dBNameDataGridViewTextBoxColumn.MinimumWidth = 100;
			this.dBNameDataGridViewTextBoxColumn.Name = "dBNameDataGridViewTextBoxColumn";
			// 
			// dBCTypeDataGridViewComboBoxColumn
			// 
			this.dBCTypeDataGridViewComboBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.dBCTypeDataGridViewComboBoxColumn.DataPropertyName = "DBCType";
			this.dBCTypeDataGridViewComboBoxColumn.DataSource = this.dBCTypeBindingSource;
			this.dBCTypeDataGridViewComboBoxColumn.DisplayMember = "TypeCaption";
			this.dBCTypeDataGridViewComboBoxColumn.HeaderText = "DB连接类型";
			this.dBCTypeDataGridViewComboBoxColumn.Name = "dBCTypeDataGridViewComboBoxColumn";
			this.dBCTypeDataGridViewComboBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dBCTypeDataGridViewComboBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.dBCTypeDataGridViewComboBoxColumn.ValueMember = "DBCType";
			// 
			// dBCTypeBindingSource
			// 
			this.dBCTypeBindingSource.DataMember = "DBCType";
			this.dBCTypeBindingSource.DataSource = this.srvsMgr_XSD;
			// 
			// userIdDataGridViewTextBoxColumn
			// 
			this.userIdDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.userIdDataGridViewTextBoxColumn.DataPropertyName = "UserId";
			this.userIdDataGridViewTextBoxColumn.HeaderText = "登录名";
			this.userIdDataGridViewTextBoxColumn.Name = "userIdDataGridViewTextBoxColumn";
			this.userIdDataGridViewTextBoxColumn.Width = 90;
			// 
			// pwdDDataGridViewTextBoxColumn
			// 
			this.pwdDDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.pwdDDataGridViewTextBoxColumn.DataPropertyName = "PwdD";
			this.pwdDDataGridViewTextBoxColumn.HeaderText = "密码";
			this.pwdDDataGridViewTextBoxColumn.Name = "pwdDDataGridViewTextBoxColumn";
			// 
			// useTrustedDataGridViewCheckBoxColumn
			// 
			this.useTrustedDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.useTrustedDataGridViewCheckBoxColumn.DataPropertyName = "UseTrusted";
			this.useTrustedDataGridViewCheckBoxColumn.HeaderText = "使用信任连接";
			this.useTrustedDataGridViewCheckBoxColumn.Name = "useTrustedDataGridViewCheckBoxColumn";
			this.useTrustedDataGridViewCheckBoxColumn.Width = 90;
			// 
			// mirrorDataGridViewTextBoxColumn
			// 
			this.mirrorDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.mirrorDataGridViewTextBoxColumn.DataPropertyName = "Mirror";
			this.mirrorDataGridViewTextBoxColumn.HeaderText = "镜像";
			this.mirrorDataGridViewTextBoxColumn.Name = "mirrorDataGridViewTextBoxColumn";
			this.mirrorDataGridViewTextBoxColumn.Width = 110;
			// 
			// optionDataGridViewTextBoxColumn
			// 
			this.optionDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.optionDataGridViewTextBoxColumn.DataPropertyName = "Option";
			this.optionDataGridViewTextBoxColumn.HeaderText = "选项";
			this.optionDataGridViewTextBoxColumn.Name = "optionDataGridViewTextBoxColumn";
			this.optionDataGridViewTextBoxColumn.Width = 120;
			// 
			// dBCBindingSource
			// 
			this.dBCBindingSource.DataMember = "DBC";
			this.dBCBindingSource.DataSource = this.srvsMgr_XSD;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbRefresh,
            this.tsbSaveToDB,
            this.toolStripSeparator1,
            this.tsbSelectAll,
            this.toolStripSeparator2,
            this.tstbStr,
            this.tssbSlts,
            this.toolStripSeparator3,
            this.tssbCreateCSFile});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(692, 25);
			this.toolStrip1.TabIndex = 7;
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
            this.tsmiSltsPwdD});
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
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// tssbCreateCSFile
			// 
			this.tssbCreateCSFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tssbCreateCSFile.Image = ((System.Drawing.Image)(resources.GetObject("tssbCreateCSFile.Image")));
			this.tssbCreateCSFile.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tssbCreateCSFile.Name = "tssbCreateCSFile";
			this.tssbCreateCSFile.Size = new System.Drawing.Size(87, 22);
			this.tssbCreateCSFile.Text = "生成文件(&G)";
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslOutInfo,
            this.tsslTest});
			this.statusStrip1.Location = new System.Drawing.Point(0, 344);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(692, 22);
			this.statusStrip1.TabIndex = 8;
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
			// DBC
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.ClientSize = new System.Drawing.Size(692, 366);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.statusStrip1);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "DBC";
			this.TabText = "DB连接管理";
			this.Text = "DB连接管理";
			this.Load += new System.EventHandler(this.DBC_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DBC_FormClosing);
			this.cmGridMenu.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.computerBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.srvsMgr_XSD)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sqlInstanceBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dBCTypeBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dBCBindingSource)).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Data.SqlClient.SqlDataAdapter sda;
		private System.Data.SqlClient.SqlCommand scDelete;
		private System.Data.SqlClient.SqlCommand scInsert;
		private System.Data.SqlClient.SqlCommand scSelect;
		private System.Data.SqlClient.SqlCommand scUpdate;
		private System.Windows.Forms.ContextMenuStrip cmGridMenu;
		private System.Windows.Forms.ToolStripMenuItem tsmiTestOpen;
		private System.Windows.Forms.SaveFileDialog sfd;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.BindingSource dBCBindingSource;
		private SrvsMgr_XSD srvsMgr_XSD;
		private System.Windows.Forms.BindingSource computerBindingSource;
		private System.Windows.Forms.BindingSource sqlInstanceBindingSource;
		private System.Windows.Forms.BindingSource dBCTypeBindingSource;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsbRefresh;
		private System.Windows.Forms.ToolStripButton tsbSaveToDB;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton tsbSelectAll;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripTextBox tstbStr;
		private System.Windows.Forms.ToolStripSplitButton tssbSlts;
		private System.Windows.Forms.ToolStripMenuItem tsmiSltsUserId;
		private System.Windows.Forms.ToolStripMenuItem tsmiSltsPwdD;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripSplitButton tssbCreateCSFile;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel tsslOutInfo;
		private System.Windows.Forms.ToolStripStatusLabel tsslTest;
		private System.Windows.Forms.DataGridViewTextBoxColumn DBID;
		private System.Windows.Forms.DataGridViewComboBoxColumn computerIDDataGridViewComBoxColumn;
		private System.Windows.Forms.DataGridViewComboBoxColumn sqlIDDataGridViewComboBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn dBNameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewComboBoxColumn dBCTypeDataGridViewComboBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn userIdDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn pwdDDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewCheckBoxColumn useTrustedDataGridViewCheckBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn mirrorDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn optionDataGridViewTextBoxColumn;

	}
}