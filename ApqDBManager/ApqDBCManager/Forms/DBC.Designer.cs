namespace ApqDBCManager.Forms
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
			this.sfd = new System.Windows.Forms.SaveFileDialog();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.bsComputer = new System.Windows.Forms.BindingSource(this.components);
			this.xsdDBC = new ApqDBCManager.Forms.DBS_XSD();
			this.bsDBI = new System.Windows.Forms.BindingSource(this.components);
			this.bsDBCUseType = new System.Windows.Forms.BindingSource(this.components);
			this.bsDBC = new System.Windows.Forms.BindingSource(this.components);
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsbOpenFile = new System.Windows.Forms.ToolStripButton();
			this.tsbSave = new System.Windows.Forms.ToolStripButton();
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
			this.ofd = new System.Windows.Forms.OpenFileDialog();
			this.actionList1 = new Crad.Windows.Forms.Actions.ActionList();
			this.acOpenFile = new Crad.Windows.Forms.Actions.Action();
			this.dBCIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dBCNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.computerIDDataGridViewComBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.dBIIDDataGridViewComboBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.dBNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dBCUseTypeDataGridViewComboBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.userIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.pwdDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.useTrustedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.mirrorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.optionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cmGridMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bsComputer)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xsdDBC)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bsDBI)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bsDBCUseType)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bsDBC)).BeginInit();
			this.toolStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.actionList1)).BeginInit();
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
			// sfd
			// 
			this.sfd.DefaultExt = "res";
			this.sfd.Filter = "DBC文件(*.res)|*.res|所有文件(*.*)|*.*";
			this.sfd.RestoreDirectory = true;
			// 
			// dataGridView1
			// 
			this.dataGridView1.AutoGenerateColumns = false;
			this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
			this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dBCIDDataGridViewTextBoxColumn,
            this.dBCNameDataGridViewTextBoxColumn,
            this.computerIDDataGridViewComBoxColumn,
            this.dBIIDDataGridViewComboBoxColumn,
            this.dBNameDataGridViewTextBoxColumn,
            this.dBCUseTypeDataGridViewComboBoxColumn,
            this.userIdDataGridViewTextBoxColumn,
            this.pwdDDataGridViewTextBoxColumn,
            this.useTrustedDataGridViewCheckBoxColumn,
            this.mirrorDataGridViewTextBoxColumn,
            this.optionDataGridViewTextBoxColumn});
			this.dataGridView1.ContextMenuStrip = this.cmGridMenu;
			this.dataGridView1.DataSource = this.bsDBC;
			this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView1.Location = new System.Drawing.Point(0, 25);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowTemplate.Height = 23;
			this.dataGridView1.Size = new System.Drawing.Size(692, 319);
			this.dataGridView1.TabIndex = 6;
			// 
			// bsComputer
			// 
			this.bsComputer.DataMember = "Computer";
			this.bsComputer.DataSource = this.xsdDBC;
			// 
			// xsdDBC
			// 
			this.xsdDBC.DataSetName = "DBS_XSD";
			this.xsdDBC.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// bsDBI
			// 
			this.bsDBI.DataMember = "DBI";
			this.bsDBI.DataSource = this.xsdDBC;
			// 
			// bsDBCUseType
			// 
			this.bsDBCUseType.DataMember = "DBCUseType";
			this.bsDBCUseType.DataSource = this.xsdDBC;
			// 
			// bsDBC
			// 
			this.bsDBC.DataMember = "DBC";
			this.bsDBC.DataSource = this.xsdDBC;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbOpenFile,
            this.tsbSave,
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
			// tsbOpenFile
			// 
			this.actionList1.SetAction(this.tsbOpenFile, this.acOpenFile);
			this.tsbOpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbOpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbOpenFile.Name = "tsbOpenFile";
			this.tsbOpenFile.Size = new System.Drawing.Size(23, 22);
			this.tsbOpenFile.Text = "打开(&O)";
			// 
			// tsbSave
			// 
			this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
			this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbSave.Name = "tsbSave";
			this.tsbSave.Size = new System.Drawing.Size(23, 22);
			this.tsbSave.Text = "保存(&S)";
			this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
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
			// ofd
			// 
			this.ofd.DefaultExt = "res";
			this.ofd.Filter = "DBC文件(*.res)|*.res|所有文件(*.*)|*.*";
			// 
			// actionList1
			// 
			this.actionList1.Actions.Add(this.acOpenFile);
			this.actionList1.ContainerControl = this;
			// 
			// acOpenFile
			// 
			this.acOpenFile.Text = "打开(&O)";
			this.acOpenFile.Execute += new System.EventHandler(this.acOpenFile_Execute);
			// 
			// dBCIDDataGridViewTextBoxColumn
			// 
			this.dBCIDDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.dBCIDDataGridViewTextBoxColumn.DataPropertyName = "DBCID";
			this.dBCIDDataGridViewTextBoxColumn.HeaderText = "编号";
			this.dBCIDDataGridViewTextBoxColumn.Name = "dBCIDDataGridViewTextBoxColumn";
			this.dBCIDDataGridViewTextBoxColumn.ReadOnly = true;
			this.dBCIDDataGridViewTextBoxColumn.Width = 60;
			// 
			// dBCNameDataGridViewTextBoxColumn
			// 
			this.dBCNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dBCNameDataGridViewTextBoxColumn.DataPropertyName = "DBCName";
			this.dBCNameDataGridViewTextBoxColumn.HeaderText = "连接名";
			this.dBCNameDataGridViewTextBoxColumn.MinimumWidth = 100;
			this.dBCNameDataGridViewTextBoxColumn.Name = "dBCNameDataGridViewTextBoxColumn";
			// 
			// computerIDDataGridViewComBoxColumn
			// 
			this.computerIDDataGridViewComBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.computerIDDataGridViewComBoxColumn.DataPropertyName = "ComputerID";
			this.computerIDDataGridViewComBoxColumn.DataSource = this.bsComputer;
			this.computerIDDataGridViewComBoxColumn.DisplayMember = "ComputerName";
			this.computerIDDataGridViewComBoxColumn.HeaderText = "服务器";
			this.computerIDDataGridViewComBoxColumn.Name = "computerIDDataGridViewComBoxColumn";
			this.computerIDDataGridViewComBoxColumn.ReadOnly = true;
			this.computerIDDataGridViewComBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.computerIDDataGridViewComBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.computerIDDataGridViewComBoxColumn.ValueMember = "ComputerID";
			// 
			// dBIIDDataGridViewComboBoxColumn
			// 
			this.dBIIDDataGridViewComboBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.dBIIDDataGridViewComboBoxColumn.DataPropertyName = "DBIID";
			this.dBIIDDataGridViewComboBoxColumn.DataSource = this.bsDBI;
			this.dBIIDDataGridViewComboBoxColumn.DisplayMember = "DBIName";
			this.dBIIDDataGridViewComboBoxColumn.HeaderText = "数据库实例";
			this.dBIIDDataGridViewComboBoxColumn.Name = "dBIIDDataGridViewComboBoxColumn";
			this.dBIIDDataGridViewComboBoxColumn.ValueMember = "DBIID";
			// 
			// dBNameDataGridViewTextBoxColumn
			// 
			this.dBNameDataGridViewTextBoxColumn.DataPropertyName = "DBName";
			this.dBNameDataGridViewTextBoxColumn.HeaderText = "数据库名";
			this.dBNameDataGridViewTextBoxColumn.MinimumWidth = 100;
			this.dBNameDataGridViewTextBoxColumn.Name = "dBNameDataGridViewTextBoxColumn";
			// 
			// dBCUseTypeDataGridViewComboBoxColumn
			// 
			this.dBCUseTypeDataGridViewComboBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.dBCUseTypeDataGridViewComboBoxColumn.DataPropertyName = "DBCUseType";
			this.dBCUseTypeDataGridViewComboBoxColumn.DataSource = this.bsDBCUseType;
			this.dBCUseTypeDataGridViewComboBoxColumn.DisplayMember = "TypeCaption";
			this.dBCUseTypeDataGridViewComboBoxColumn.HeaderText = "使用类型";
			this.dBCUseTypeDataGridViewComboBoxColumn.Name = "dBCUseTypeDataGridViewComboBoxColumn";
			this.dBCUseTypeDataGridViewComboBoxColumn.ValueMember = "DBCUseType";
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
			// DBC
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.ClientSize = new System.Drawing.Size(692, 366);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.statusStrip1);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "DBC";
			this.TabText = "DB连接管理";
			this.Text = "DB连接管理";
			this.Load += new System.EventHandler(this.DBC_Load);
			this.cmGridMenu.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bsComputer)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xsdDBC)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bsDBI)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bsDBCUseType)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bsDBC)).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.actionList1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip cmGridMenu;
		private System.Windows.Forms.ToolStripMenuItem tsmiTestOpen;
		private System.Windows.Forms.SaveFileDialog sfd;
		private System.Windows.Forms.DataGridView dataGridView1;
		private DBS_XSD xsdDBC;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsbSelectAll;
		private System.Windows.Forms.ToolStripTextBox tstbStr;
		private System.Windows.Forms.ToolStripSplitButton tssbSlts;
		private System.Windows.Forms.ToolStripMenuItem tsmiSltsUserId;
		private System.Windows.Forms.ToolStripMenuItem tsmiSltsPwdD;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripSplitButton tssbCreateCSFile;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel tsslOutInfo;
		private System.Windows.Forms.ToolStripStatusLabel tsslTest;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton tsbOpenFile;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton tsbSave;
		private System.Windows.Forms.OpenFileDialog ofd;
		private System.Windows.Forms.BindingSource bsComputer;
		private System.Windows.Forms.BindingSource bsDBI;
		private System.Windows.Forms.BindingSource bsDBC;
		private System.Windows.Forms.BindingSource bsDBCUseType;
		private Crad.Windows.Forms.Actions.ActionList actionList1;
		private Crad.Windows.Forms.Actions.Action acOpenFile;
		private System.Windows.Forms.DataGridViewTextBoxColumn dBCIDDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn dBCNameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewComboBoxColumn computerIDDataGridViewComBoxColumn;
		private System.Windows.Forms.DataGridViewComboBoxColumn dBIIDDataGridViewComboBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn dBNameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewComboBoxColumn dBCUseTypeDataGridViewComboBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn userIdDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn pwdDDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewCheckBoxColumn useTrustedDataGridViewCheckBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn mirrorDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn optionDataGridViewTextBoxColumn;

	}
}