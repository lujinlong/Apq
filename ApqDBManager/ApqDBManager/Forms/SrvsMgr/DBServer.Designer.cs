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
			this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
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
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.tsmiRefresh = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiSave = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiSelectAll = new System.Windows.Forms.ToolStripMenuItem();
			this.tstbStr = new System.Windows.Forms.ToolStripTextBox();
			this.tsmiSlts = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiSqlInstance = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.tsslOutInfo = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsslTest = new System.Windows.Forms.ToolStripStatusLabel();
			this.textColumn1 = new XPTable.Models.TextColumn();
			this.textColumn2 = new XPTable.Models.TextColumn();
			this.comboBoxColumn1 = new XPTable.Models.ComboBoxColumn();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.computerTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.srvsMgr_XSD = new ApqDBManager.Forms.SrvsMgr.SrvsMgr_XSD();
			this.computerBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.computerIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.computerNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.computerTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.menuStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.computerTypeBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.srvsMgr_XSD)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.computerBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// gridColumn18
			// 
			this.gridColumn18.Name = "gridColumn18";
			// 
			// gridColumn19
			// 
			this.gridColumn19.Name = "gridColumn19";
			// 
			// gridColumn20
			// 
			this.gridColumn20.Name = "gridColumn20";
			// 
			// gridColumn21
			// 
			this.gridColumn21.Name = "gridColumn21";
			// 
			// gridColumn22
			// 
			this.gridColumn22.Name = "gridColumn22";
			// 
			// gridColumn14
			// 
			this.gridColumn14.Name = "gridColumn14";
			// 
			// gridColumn15
			// 
			this.gridColumn15.Name = "gridColumn15";
			// 
			// gridColumn16
			// 
			this.gridColumn16.Name = "gridColumn16";
			// 
			// gridColumn17
			// 
			this.gridColumn17.Name = "gridColumn17";
			// 
			// gridColumn7
			// 
			this.gridColumn7.Name = "gridColumn7";
			// 
			// gridColumn8
			// 
			this.gridColumn8.Name = "gridColumn8";
			// 
			// gridColumn9
			// 
			this.gridColumn9.Name = "gridColumn9";
			// 
			// gridColumn10
			// 
			this.gridColumn10.Name = "gridColumn10";
			// 
			// gridColumn11
			// 
			this.gridColumn11.Name = "gridColumn11";
			// 
			// gridColumn12
			// 
			this.gridColumn12.Name = "gridColumn12";
			// 
			// gridColumn13
			// 
			this.gridColumn13.Name = "gridColumn13";
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
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRefresh,
            this.tsmiSave,
            this.tsmiSelectAll,
            this.tstbStr,
            this.tsmiSlts,
            this.tsmiSqlInstance});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(692, 25);
			this.menuStrip1.TabIndex = 5;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// tsmiRefresh
			// 
			this.tsmiRefresh.Name = "tsmiRefresh";
			this.tsmiRefresh.Size = new System.Drawing.Size(59, 21);
			this.tsmiRefresh.Text = "刷新(&R)";
			this.tsmiRefresh.Click += new System.EventHandler(this.tsmiRefresh_Click);
			// 
			// tsmiSave
			// 
			this.tsmiSave.Name = "tsmiSave";
			this.tsmiSave.Size = new System.Drawing.Size(59, 21);
			this.tsmiSave.Text = "保存(&S)";
			this.tsmiSave.Click += new System.EventHandler(this.tsmiSave_Click);
			// 
			// tsmiSelectAll
			// 
			this.tsmiSelectAll.Name = "tsmiSelectAll";
			this.tsmiSelectAll.Size = new System.Drawing.Size(59, 21);
			this.tsmiSelectAll.Text = "全选(&A)";
			this.tsmiSelectAll.Click += new System.EventHandler(this.tsmiSelectAll_Click);
			// 
			// tstbStr
			// 
			this.tstbStr.Name = "tstbStr";
			this.tstbStr.Size = new System.Drawing.Size(200, 21);
			// 
			// tsmiSlts
			// 
			this.tsmiSlts.Name = "tsmiSlts";
			this.tsmiSlts.Size = new System.Drawing.Size(95, 21);
			this.tsmiSlts.Text = "设置选中格(&E)";
			this.tsmiSlts.Click += new System.EventHandler(this.tsmiSlts_Click);
			// 
			// tsmiSqlInstance
			// 
			this.tsmiSqlInstance.Name = "tsmiSqlInstance";
			this.tsmiSqlInstance.Size = new System.Drawing.Size(83, 21);
			this.tsmiSqlInstance.Text = "实例管理(&I)";
			this.tsmiSqlInstance.Click += new System.EventHandler(this.tsmiSqlInstance_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslOutInfo,
            this.tsslTest});
			this.statusStrip1.Location = new System.Drawing.Point(0, 344);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(692, 22);
			this.statusStrip1.TabIndex = 6;
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
			// textColumn1
			// 
			this.textColumn1.Editable = false;
			this.textColumn1.Text = "服务器编号";
			// 
			// textColumn2
			// 
			this.textColumn2.Text = "服务器名";
			// 
			// comboBoxColumn1
			// 
			this.comboBoxColumn1.Text = "服务器类型";
			// 
			// dataGridView1
			// 
			this.dataGridView1.AutoGenerateColumns = false;
			this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.computerIDDataGridViewTextBoxColumn,
            this.computerNameDataGridViewTextBoxColumn,
            this.computerTypeDataGridViewTextBoxColumn});
			this.dataGridView1.DataSource = this.computerBindingSource;
			this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView1.Location = new System.Drawing.Point(0, 25);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowTemplate.Height = 23;
			this.dataGridView1.Size = new System.Drawing.Size(692, 341);
			this.dataGridView1.TabIndex = 8;
			this.dataGridView1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_ColumnHeaderMouseClick);
			// 
			// computerTypeBindingSource
			// 
			this.computerTypeBindingSource.DataMember = "ComputerType";
			this.computerTypeBindingSource.DataSource = this.srvsMgr_XSD;
			// 
			// srvsMgr_XSD
			// 
			this.srvsMgr_XSD.DataSetName = "srvsMgr_XSD";
			this.srvsMgr_XSD.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// computerBindingSource
			// 
			this.computerBindingSource.DataMember = "Computer";
			this.computerBindingSource.DataSource = this.srvsMgr_XSD;
			// 
			// computerIDDataGridViewTextBoxColumn
			// 
			this.computerIDDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.computerIDDataGridViewTextBoxColumn.DataPropertyName = "ComputerID";
			this.computerIDDataGridViewTextBoxColumn.HeaderText = "编号";
			this.computerIDDataGridViewTextBoxColumn.Name = "computerIDDataGridViewTextBoxColumn";
			this.computerIDDataGridViewTextBoxColumn.ReadOnly = true;
			this.computerIDDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.computerIDDataGridViewTextBoxColumn.Width = 35;
			// 
			// computerNameDataGridViewTextBoxColumn
			// 
			this.computerNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.computerNameDataGridViewTextBoxColumn.DataPropertyName = "ComputerName";
			this.computerNameDataGridViewTextBoxColumn.HeaderText = "服务器名称";
			this.computerNameDataGridViewTextBoxColumn.Name = "computerNameDataGridViewTextBoxColumn";
			this.computerNameDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// computerTypeDataGridViewTextBoxColumn
			// 
			this.computerTypeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.computerTypeDataGridViewTextBoxColumn.DataPropertyName = "ComputerType";
			this.computerTypeDataGridViewTextBoxColumn.DataSource = this.computerTypeBindingSource;
			this.computerTypeDataGridViewTextBoxColumn.DisplayMember = "TypeCaption";
			this.computerTypeDataGridViewTextBoxColumn.HeaderText = "服务器类型";
			this.computerTypeDataGridViewTextBoxColumn.Name = "computerTypeDataGridViewTextBoxColumn";
			this.computerTypeDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.computerTypeDataGridViewTextBoxColumn.ValueMember = "ComputerType";
			this.computerTypeDataGridViewTextBoxColumn.Width = 71;
			// 
			// DBServer
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.ClientSize = new System.Drawing.Size(692, 366);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.menuStrip1);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "DBServer";
			this.TabText = "服务器管理";
			this.Text = "服务器管理";
			this.Load += new System.EventHandler(this.DBServer_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DBServer_FormClosing);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.computerTypeBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.srvsMgr_XSD)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.computerBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
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
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem tsmiRefresh;
		private System.Windows.Forms.ToolStripMenuItem tsmiSave;
		private System.Windows.Forms.ToolStripMenuItem tsmiSelectAll;
		private System.Windows.Forms.ToolStripTextBox tstbStr;
		private System.Windows.Forms.ToolStripMenuItem tsmiSlts;
		private System.Windows.Forms.ToolStripMenuItem tsmiSqlInstance;
		private System.Windows.Forms.ToolStripStatusLabel tsslOutInfo;
		private System.Windows.Forms.ToolStripStatusLabel tsslTest;
		private XPTable.Models.TextColumn textColumn1;
		private XPTable.Models.TextColumn textColumn2;
		private XPTable.Models.ComboBoxColumn comboBoxColumn1;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.BindingSource computerBindingSource;
		private SrvsMgr_XSD srvsMgr_XSD;
		private System.Windows.Forms.BindingSource computerTypeBindingSource;
		private System.Windows.Forms.DataGridViewTextBoxColumn computerIDDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn computerNameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewComboBoxColumn computerTypeDataGridViewTextBoxColumn;
	}
}