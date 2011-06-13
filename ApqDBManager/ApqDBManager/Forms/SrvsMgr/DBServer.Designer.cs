namespace ApqDBManager.Forms.SrvsMgr
{
	partial class DBServer
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBServer));
			this.scSelect = new System.Data.SqlClient.SqlCommand();
			this.sda = new System.Data.SqlClient.SqlDataAdapter();
			this.scDelete = new System.Data.SqlClient.SqlCommand();
			this.scInsert = new System.Data.SqlClient.SqlCommand();
			this.scUpdate = new System.Data.SqlClient.SqlCommand();
			this.sqlCommand1 = new System.Data.SqlClient.SqlCommand();
			this.sqlDataAdapter1 = new System.Data.SqlClient.SqlDataAdapter();
			this.sqlDataAdapter2 = new System.Data.SqlClient.SqlDataAdapter();
			this.sqlCommand2 = new System.Data.SqlClient.SqlCommand();
			this.sqlDataAdapter3 = new System.Data.SqlClient.SqlDataAdapter();
			this.sqlCommand3 = new System.Data.SqlClient.SqlCommand();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.computerTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.SrvsMgr_XSD = new ApqDBManager.Forms.SrvsMgr.SrvsMgr_XSD();
			this.computerBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
			this.tsbSaveToDB = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbSqlInstance = new System.Windows.Forms.ToolStripButton();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.tsslOutInfo = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsslTest = new System.Windows.Forms.ToolStripStatusLabel();
			this.computerIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.computerNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.computerTypeDataGridViewComboBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.computerTypeBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SrvsMgr_XSD)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.computerBindingSource)).BeginInit();
			this.toolStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// scSelect
			// 
			this.scSelect.CommandText = "\r\nSELECT * FROM dic_SQLType;\r\nSELECT * FROM dic_IPType;\r\nSELECT * FROM DBServer;\r" +
				"\nSELECT * FROM SQLInstance;\r\nSELECT * FROM DBC;\r\nSELECT * FROM DBServerIP;";
			// 
			// sda
			// 
			this.sda.DeleteCommand = this.scDelete;
			this.sda.InsertCommand = this.scInsert;
			this.sda.SelectCommand = this.scSelect;
			this.sda.UpdateCommand = this.scUpdate;
			// 
			// sqlDataAdapter1
			// 
			this.sqlDataAdapter1.SelectCommand = this.sqlCommand1;
			this.sqlDataAdapter1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "ComputerType", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("ComputerType", "ComputerType"),
                        new System.Data.Common.DataColumnMapping("TypeCaption", "TypeCaption")})});
			// 
			// sqlDataAdapter2
			// 
			this.sqlDataAdapter2.SelectCommand = this.sqlCommand2;
			this.sqlDataAdapter2.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "ComputerType", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("ComputerType", "ComputerType"),
                        new System.Data.Common.DataColumnMapping("TypeCaption", "TypeCaption")})});
			// 
			// sqlDataAdapter3
			// 
			this.sqlDataAdapter3.SelectCommand = this.sqlCommand3;
			this.sqlDataAdapter3.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "ComputerType", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("ComputerType", "ComputerType"),
                        new System.Data.Common.DataColumnMapping("TypeCaption", "TypeCaption")})});
			// 
			// dataGridView1
			// 
			this.dataGridView1.AutoGenerateColumns = false;
			this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
			this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.computerIDDataGridViewTextBoxColumn,
            this.computerNameDataGridViewTextBoxColumn,
            this.computerTypeDataGridViewComboBoxColumn});
			this.dataGridView1.DataSource = this.computerBindingSource;
			this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView1.Location = new System.Drawing.Point(0, 25);
			this.dataGridView1.MultiSelect = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowTemplate.Height = 23;
			this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView1.Size = new System.Drawing.Size(692, 319);
			this.dataGridView1.TabIndex = 8;
			// 
			// computerTypeBindingSource
			// 
			this.computerTypeBindingSource.DataMember = "ComputerType";
			this.computerTypeBindingSource.DataSource = this.SrvsMgr_XSD;
			// 
			// SrvsMgr_XSD
			// 
			this.SrvsMgr_XSD.DataSetName = "DBServer_XSD";
			this.SrvsMgr_XSD.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// computerBindingSource
			// 
			this.computerBindingSource.DataMember = "Computer";
			this.computerBindingSource.DataSource = this.SrvsMgr_XSD;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbRefresh,
            this.tsbSaveToDB,
            this.toolStripSeparator2,
            this.tsbSqlInstance});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(692, 25);
			this.toolStrip1.TabIndex = 9;
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
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbSqlInstance
			// 
			this.tsbSqlInstance.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbSqlInstance.Image = ((System.Drawing.Image)(resources.GetObject("tsbSqlInstance.Image")));
			this.tsbSqlInstance.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbSqlInstance.Name = "tsbSqlInstance";
			this.tsbSqlInstance.Size = new System.Drawing.Size(75, 22);
			this.tsbSqlInstance.Text = "实例管理(&I)";
			this.tsbSqlInstance.Click += new System.EventHandler(this.tsbSqlInstance_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslOutInfo,
            this.tsslTest});
			this.statusStrip1.Location = new System.Drawing.Point(0, 344);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(692, 22);
			this.statusStrip1.TabIndex = 10;
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
			// computerIDDataGridViewTextBoxColumn
			// 
			this.computerIDDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.computerIDDataGridViewTextBoxColumn.DataPropertyName = "ComputerID";
			this.computerIDDataGridViewTextBoxColumn.HeaderText = "编号";
			this.computerIDDataGridViewTextBoxColumn.Name = "computerIDDataGridViewTextBoxColumn";
			this.computerIDDataGridViewTextBoxColumn.ReadOnly = true;
			this.computerIDDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.computerIDDataGridViewTextBoxColumn.Width = 60;
			// 
			// computerNameDataGridViewTextBoxColumn
			// 
			this.computerNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.computerNameDataGridViewTextBoxColumn.DataPropertyName = "ComputerName";
			this.computerNameDataGridViewTextBoxColumn.HeaderText = "服务器名称";
			this.computerNameDataGridViewTextBoxColumn.MinimumWidth = 120;
			this.computerNameDataGridViewTextBoxColumn.Name = "computerNameDataGridViewTextBoxColumn";
			this.computerNameDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// computerTypeDataGridViewComboBoxColumn
			// 
			this.computerTypeDataGridViewComboBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.computerTypeDataGridViewComboBoxColumn.DataPropertyName = "ComputerType";
			this.computerTypeDataGridViewComboBoxColumn.DataSource = this.computerTypeBindingSource;
			this.computerTypeDataGridViewComboBoxColumn.DisplayMember = "TypeCaption";
			this.computerTypeDataGridViewComboBoxColumn.HeaderText = "服务器类型";
			this.computerTypeDataGridViewComboBoxColumn.Name = "computerTypeDataGridViewComboBoxColumn";
			this.computerTypeDataGridViewComboBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.computerTypeDataGridViewComboBoxColumn.ValueMember = "ComputerType";
			// 
			// DBServer
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.ClientSize = new System.Drawing.Size(692, 366);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.statusStrip1);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "DBServer";
			this.TabText = "服务器管理";
			this.Text = "服务器管理";
			this.Load += new System.EventHandler(this.DBServer_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DBServer_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.computerTypeBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SrvsMgr_XSD)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.computerBindingSource)).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Data.SqlClient.SqlCommand scSelect;
		private System.Data.SqlClient.SqlDataAdapter sda;
		private System.Data.SqlClient.SqlCommand scDelete;
		private System.Data.SqlClient.SqlCommand scInsert;
		private System.Data.SqlClient.SqlCommand scUpdate;
		private System.Data.SqlClient.SqlCommand sqlCommand1;
		private System.Data.SqlClient.SqlDataAdapter sqlDataAdapter1;
		private System.Data.SqlClient.SqlDataAdapter sqlDataAdapter2;
		private System.Data.SqlClient.SqlCommand sqlCommand2;
		private System.Data.SqlClient.SqlDataAdapter sqlDataAdapter3;
		private System.Data.SqlClient.SqlCommand sqlCommand3;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.BindingSource computerBindingSource;
		private System.Windows.Forms.BindingSource computerTypeBindingSource;
		public SrvsMgr_XSD SrvsMgr_XSD;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsbRefresh;
		private System.Windows.Forms.ToolStripButton tsbSaveToDB;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton tsbSqlInstance;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel tsslOutInfo;
		private System.Windows.Forms.ToolStripStatusLabel tsslTest;
		private System.Windows.Forms.DataGridViewTextBoxColumn computerIDDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn computerNameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewComboBoxColumn computerTypeDataGridViewComboBoxColumn;
	}
}