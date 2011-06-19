namespace ApqDBCManager.Forms
{
	partial class DBS
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBS));
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.computerIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.computerNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.computerTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.bsComputerType = new System.Windows.Forms.BindingSource(this.components);
			this._dbS_XSD1 = new ApqDBCManager.Forms.DBS_XSD();
			this.bsDBS = new System.Windows.Forms.BindingSource(this.components);
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsbSave = new System.Windows.Forms.ToolStripButton();
			this.tsbInput = new System.Windows.Forms.ToolStripButton();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.tsslOutInfo = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsslTest = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bsComputerType)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._dbS_XSD1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bsDBS)).BeginInit();
			this.toolStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
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
            this.computerTypeDataGridViewTextBoxColumn});
			this.dataGridView1.DataSource = this.bsDBS;
			this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView1.Location = new System.Drawing.Point(0, 25);
			this.dataGridView1.MultiSelect = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowTemplate.Height = 23;
			this.dataGridView1.Size = new System.Drawing.Size(692, 319);
			this.dataGridView1.TabIndex = 8;
			// 
			// computerIDDataGridViewTextBoxColumn
			// 
			this.computerIDDataGridViewTextBoxColumn.DataPropertyName = "ComputerID";
			this.computerIDDataGridViewTextBoxColumn.HeaderText = "编号";
			this.computerIDDataGridViewTextBoxColumn.Name = "computerIDDataGridViewTextBoxColumn";
			this.computerIDDataGridViewTextBoxColumn.ReadOnly = true;
			this.computerIDDataGridViewTextBoxColumn.Width = 54;
			// 
			// computerNameDataGridViewTextBoxColumn
			// 
			this.computerNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.computerNameDataGridViewTextBoxColumn.DataPropertyName = "ComputerName";
			this.computerNameDataGridViewTextBoxColumn.HeaderText = "名称";
			this.computerNameDataGridViewTextBoxColumn.Name = "computerNameDataGridViewTextBoxColumn";
			// 
			// computerTypeDataGridViewTextBoxColumn
			// 
			this.computerTypeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.computerTypeDataGridViewTextBoxColumn.DataPropertyName = "ComputerType";
			this.computerTypeDataGridViewTextBoxColumn.DataSource = this.bsComputerType;
			this.computerTypeDataGridViewTextBoxColumn.DisplayMember = "TypeCaption";
			this.computerTypeDataGridViewTextBoxColumn.HeaderText = "类型";
			this.computerTypeDataGridViewTextBoxColumn.Name = "computerTypeDataGridViewTextBoxColumn";
			this.computerTypeDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.computerTypeDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.computerTypeDataGridViewTextBoxColumn.ValueMember = "ComputerType";
			// 
			// bsComputerType
			// 
			this.bsComputerType.DataMember = "ComputerType";
			this.bsComputerType.DataSource = this._dbS_XSD1;
			// 
			// _dbS_XSD1
			// 
			this._dbS_XSD1.DataSetName = "DBS_XSD";
			this._dbS_XSD1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// bsDBS
			// 
			this.bsDBS.DataMember = "Computer";
			this.bsDBS.DataSource = this._dbS_XSD1;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSave,
            this.toolStripSeparator1,
            this.tsbInput});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(692, 25);
			this.toolStrip1.TabIndex = 9;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tsbSave
			// 
			this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
			this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbSave.Name = "tsbSave";
			this.tsbSave.Size = new System.Drawing.Size(67, 22);
			this.tsbSave.Text = "保存(&S)";
			this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
			// 
			// tsbInput
			// 
			this.tsbInput.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbInput.Image = ((System.Drawing.Image)(resources.GetObject("tsbInput.Image")));
			this.tsbInput.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbInput.Name = "tsbInput";
			this.tsbInput.Size = new System.Drawing.Size(51, 22);
			this.tsbInput.Text = "导入(&I)";
			this.tsbInput.Click += new System.EventHandler(this.tsbInput_Click);
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
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// DBS
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.ClientSize = new System.Drawing.Size(692, 366);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.statusStrip1);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "DBS";
			this.TabText = "服务器列表";
			this.Text = "服务器列表";
			this.Load += new System.EventHandler(this.DBServer_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DBServer_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bsComputerType)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._dbS_XSD1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bsDBS)).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.BindingSource bsDBS;
		private System.Windows.Forms.BindingSource bsComputerType;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsbSave;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel tsslOutInfo;
		private System.Windows.Forms.ToolStripStatusLabel tsslTest;
		private System.Windows.Forms.ToolStripButton tsbInput;
		private DBS_XSD _dbS_XSD1;
		private System.Windows.Forms.DataGridViewTextBoxColumn computerIDDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn computerNameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewComboBoxColumn computerTypeDataGridViewTextBoxColumn;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
	}
}