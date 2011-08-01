namespace Apq.Windows.Forms.DockForms
{
	partial class ErrList
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrList));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsbType = new System.Windows.Forms.ToolStripSplitButton();
			this.tsmiTypeError = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiTypeWarn = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiTypeTrace = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiTypeInfo = new System.Windows.Forms.ToolStripMenuItem();
			this.tsbSeverity = new System.Windows.Forms.ToolStripSplitButton();
			this.tsmiSeverity15 = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiSeverity20 = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiSeverity21 = new System.Windows.Forms.ToolStripMenuItem();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.bsType = new System.Windows.Forms.BindingSource(this.components);
			this._xsd = new Apq.Windows.Forms.DockForms.ErrList_XSD();
			this._dv = new System.Data.DataView();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.colIcon = new System.Windows.Forms.DataGridViewImageColumn();
			this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.col_InTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colType = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.colSeverity = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colMsg = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colAlarmGroupID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colState = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bsType)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._xsd)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._dv)).BeginInit();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbType,
            this.tsbSeverity});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(767, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tsbType
			// 
			this.tsbType.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbType.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTypeError,
            this.tsmiTypeWarn,
            this.tsmiTypeTrace,
            this.tsmiTypeInfo});
			this.tsbType.Image = ((System.Drawing.Image)(resources.GetObject("tsbType.Image")));
			this.tsbType.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbType.Name = "tsbType";
			this.tsbType.Size = new System.Drawing.Size(45, 22);
			this.tsbType.Text = "类型";
			// 
			// tsmiTypeError
			// 
			this.tsmiTypeError.CheckOnClick = true;
			this.tsmiTypeError.Name = "tsmiTypeError";
			this.tsmiTypeError.Size = new System.Drawing.Size(94, 22);
			this.tsmiTypeError.Text = "错误";
			this.tsmiTypeError.Click += new System.EventHandler(this.tsmiTypeError_Click);
			// 
			// tsmiTypeWarn
			// 
			this.tsmiTypeWarn.CheckOnClick = true;
			this.tsmiTypeWarn.Name = "tsmiTypeWarn";
			this.tsmiTypeWarn.Size = new System.Drawing.Size(94, 22);
			this.tsmiTypeWarn.Text = "警告";
			this.tsmiTypeWarn.Click += new System.EventHandler(this.tsmiTypeWarn_Click);
			// 
			// tsmiTypeTrace
			// 
			this.tsmiTypeTrace.CheckOnClick = true;
			this.tsmiTypeTrace.Name = "tsmiTypeTrace";
			this.tsmiTypeTrace.Size = new System.Drawing.Size(94, 22);
			this.tsmiTypeTrace.Text = "跟踪";
			this.tsmiTypeTrace.Click += new System.EventHandler(this.tsmiTypeTrace_Click);
			// 
			// tsmiTypeInfo
			// 
			this.tsmiTypeInfo.CheckOnClick = true;
			this.tsmiTypeInfo.Name = "tsmiTypeInfo";
			this.tsmiTypeInfo.Size = new System.Drawing.Size(94, 22);
			this.tsmiTypeInfo.Text = "信息";
			this.tsmiTypeInfo.Click += new System.EventHandler(this.tsmiTypeInfo_Click);
			// 
			// tsbSeverity
			// 
			this.tsbSeverity.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbSeverity.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSeverity15,
            this.tsmiSeverity20,
            this.tsmiSeverity21});
			this.tsbSeverity.Image = ((System.Drawing.Image)(resources.GetObject("tsbSeverity.Image")));
			this.tsbSeverity.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbSeverity.Name = "tsbSeverity";
			this.tsbSeverity.Size = new System.Drawing.Size(57, 22);
			this.tsbSeverity.Text = "严重度";
			// 
			// tsmiSeverity15
			// 
			this.tsmiSeverity15.CheckOnClick = true;
			this.tsmiSeverity15.Name = "tsmiSeverity15";
			this.tsmiSeverity15.Size = new System.Drawing.Size(106, 22);
			this.tsmiSeverity15.Text = "--15";
			this.tsmiSeverity15.Click += new System.EventHandler(this.tsmiSeverity15_Click);
			// 
			// tsmiSeverity20
			// 
			this.tsmiSeverity20.CheckOnClick = true;
			this.tsmiSeverity20.Name = "tsmiSeverity20";
			this.tsmiSeverity20.Size = new System.Drawing.Size(106, 22);
			this.tsmiSeverity20.Text = "16--20";
			this.tsmiSeverity20.Click += new System.EventHandler(this.tsmiSeverity20_Click);
			// 
			// tsmiSeverity21
			// 
			this.tsmiSeverity21.CheckOnClick = true;
			this.tsmiSeverity21.Name = "tsmiSeverity21";
			this.tsmiSeverity21.Size = new System.Drawing.Size(106, 22);
			this.tsmiSeverity21.Text = "21--";
			this.tsmiSeverity21.Click += new System.EventHandler(this.tsmiSeverity21_Click);
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AutoGenerateColumns = false;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIcon,
            this.colID,
            this.col_InTime,
            this.colType,
            this.colSeverity,
            this.colTitle,
            this.colMsg,
            this.colAlarmGroupID,
            this.colState});
			this.dataGridView1.DataSource = this._dv;
			this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView1.Location = new System.Drawing.Point(0, 25);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.RowTemplate.Height = 23;
			this.dataGridView1.Size = new System.Drawing.Size(767, 382);
			this.dataGridView1.TabIndex = 1;
			// 
			// bsType
			// 
			this.bsType.DataMember = "dic_Type";
			this.bsType.DataSource = this._xsd;
			// 
			// _xsd
			// 
			this._xsd.DataSetName = "ErrList_XSD";
			this._xsd.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// _dv
			// 
			this._dv.Table = this._xsd.ErrList;
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "none.ico");
			this.imageList1.Images.SetKeyName(1, "info.ico");
			this.imageList1.Images.SetKeyName(2, "009_HighPriority_16x16_72.png");
			this.imageList1.Images.SetKeyName(3, "warning.ico");
			this.imageList1.Images.SetKeyName(4, "error.ico");
			// 
			// colIcon
			// 
			this.colIcon.DataPropertyName = "Icon";
			this.colIcon.HeaderText = "*";
			this.colIcon.Name = "colIcon";
			this.colIcon.ReadOnly = true;
			this.colIcon.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.colIcon.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.colIcon.Width = 16;
			// 
			// colID
			// 
			this.colID.DataPropertyName = "ID";
			this.colID.HeaderText = "ID";
			this.colID.Name = "colID";
			this.colID.ReadOnly = true;
			// 
			// col_InTime
			// 
			this.col_InTime.DataPropertyName = "_InTime";
			this.col_InTime.HeaderText = "时间";
			this.col_InTime.Name = "col_InTime";
			this.col_InTime.ReadOnly = true;
			// 
			// colType
			// 
			this.colType.DataPropertyName = "Type";
			this.colType.DataSource = this.bsType;
			this.colType.DisplayMember = "TypeCaption";
			this.colType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
			this.colType.HeaderText = "类型";
			this.colType.Name = "colType";
			this.colType.ReadOnly = true;
			this.colType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.colType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.colType.ValueMember = "Type";
			// 
			// colSeverity
			// 
			this.colSeverity.DataPropertyName = "Severity";
			this.colSeverity.HeaderText = "严重度";
			this.colSeverity.Name = "colSeverity";
			this.colSeverity.ReadOnly = true;
			// 
			// colTitle
			// 
			this.colTitle.DataPropertyName = "Title";
			this.colTitle.HeaderText = "标题";
			this.colTitle.Name = "colTitle";
			this.colTitle.ReadOnly = true;
			// 
			// colMsg
			// 
			this.colMsg.DataPropertyName = "Msg";
			this.colMsg.HeaderText = "内容";
			this.colMsg.Name = "colMsg";
			this.colMsg.ReadOnly = true;
			// 
			// colAlarmGroupID
			// 
			this.colAlarmGroupID.DataPropertyName = "AlarmGroupID";
			this.colAlarmGroupID.HeaderText = "报警";
			this.colAlarmGroupID.Name = "colAlarmGroupID";
			this.colAlarmGroupID.ReadOnly = true;
			// 
			// colState
			// 
			this.colState.DataPropertyName = "State";
			this.colState.HeaderText = "状态";
			this.colState.Name = "colState";
			this.colState.ReadOnly = true;
			// 
			// ErrList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(767, 407);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.toolStrip1);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
			this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.HideOnClose = true;
			this.Name = "ErrList";
			this.ShowIcon = false;
			this.TabText = "错误列表";
			this.Text = "错误列表";
			this.Load += new System.EventHandler(this.ErrorList_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bsType)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._xsd)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._dv)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.ToolStripSplitButton tsbSeverity;
		private System.Windows.Forms.ToolStripSplitButton tsbType;
		private System.Windows.Forms.ToolStripMenuItem tsmiTypeError;
		private System.Windows.Forms.ToolStripMenuItem tsmiTypeWarn;
		private System.Windows.Forms.ToolStripMenuItem tsmiTypeTrace;
		private System.Windows.Forms.ToolStripMenuItem tsmiTypeInfo;
		private System.Windows.Forms.ToolStripMenuItem tsmiSeverity15;
		private System.Windows.Forms.ToolStripMenuItem tsmiSeverity20;
		private System.Windows.Forms.ToolStripMenuItem tsmiSeverity21;
		private ErrList_XSD _xsd;
		private System.Windows.Forms.BindingSource bsType;
		private System.Data.DataView _dv;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.DataGridViewImageColumn colIcon;
		private System.Windows.Forms.DataGridViewTextBoxColumn colID;
		private System.Windows.Forms.DataGridViewTextBoxColumn col_InTime;
		private System.Windows.Forms.DataGridViewComboBoxColumn colType;
		private System.Windows.Forms.DataGridViewTextBoxColumn colSeverity;
		private System.Windows.Forms.DataGridViewTextBoxColumn colTitle;
		private System.Windows.Forms.DataGridViewTextBoxColumn colMsg;
		private System.Windows.Forms.DataGridViewTextBoxColumn colAlarmGroupID;
		private System.Windows.Forms.DataGridViewTextBoxColumn colState;
	}
}