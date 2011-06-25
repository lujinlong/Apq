namespace ApqDBManager
{
	partial class Export
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
			this.lblRowCount = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.cbRowSpliter = new System.Windows.Forms.ComboBox();
			this.cbColSpliter = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.cbContainsColName = new System.Windows.Forms.CheckBox();
			this.cbExportType = new System.Windows.Forms.ComboBox();
			this.btnConfirm = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.tspbProcess = new System.Windows.Forms.ToolStripProgressBar();
			this.sfd = new System.Windows.Forms.SaveFileDialog();
			this.groupBox1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblRowCount
			// 
			this.lblRowCount.AutoSize = true;
			this.lblRowCount.Location = new System.Drawing.Point(61, 9);
			this.lblRowCount.Name = "lblRowCount";
			this.lblRowCount.Size = new System.Drawing.Size(71, 12);
			this.lblRowCount.TabIndex = 14;
			this.lblRowCount.Text = "共有行数:  ";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(33, 39);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(59, 12);
			this.label1.TabIndex = 15;
			this.label1.Text = "导出类型:";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.cbRowSpliter);
			this.groupBox1.Controls.Add(this.cbColSpliter);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.cbContainsColName);
			this.groupBox1.Location = new System.Drawing.Point(12, 71);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(271, 102);
			this.groupBox1.TabIndex = 17;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "文本选项";
			// 
			// cbRowSpliter
			// 
			this.cbRowSpliter.FormattingEnabled = true;
			this.cbRowSpliter.Items.AddRange(new object[] {
            "\\r\\n",
            "~*$"});
			this.cbRowSpliter.Location = new System.Drawing.Point(86, 45);
			this.cbRowSpliter.Name = "cbRowSpliter";
			this.cbRowSpliter.Size = new System.Drawing.Size(151, 20);
			this.cbRowSpliter.TabIndex = 21;
			this.cbRowSpliter.Text = "\\r\\n";
			// 
			// cbColSpliter
			// 
			this.cbColSpliter.FormattingEnabled = true;
			this.cbColSpliter.Items.AddRange(new object[] {
            ",",
            "\\t"});
			this.cbColSpliter.Location = new System.Drawing.Point(86, 20);
			this.cbColSpliter.Name = "cbColSpliter";
			this.cbColSpliter.Size = new System.Drawing.Size(151, 20);
			this.cbColSpliter.TabIndex = 20;
			this.cbColSpliter.Text = ",";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(21, 48);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(59, 12);
			this.label3.TabIndex = 17;
			this.label3.Text = "行分隔符:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(21, 23);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(59, 12);
			this.label2.TabIndex = 16;
			this.label2.Text = "列分隔符:";
			// 
			// cbContainsColName
			// 
			this.cbContainsColName.AutoSize = true;
			this.cbContainsColName.Checked = true;
			this.cbContainsColName.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbContainsColName.Location = new System.Drawing.Point(86, 71);
			this.cbContainsColName.Name = "cbContainsColName";
			this.cbContainsColName.Size = new System.Drawing.Size(84, 16);
			this.cbContainsColName.TabIndex = 18;
			this.cbContainsColName.Text = "首行为列名";
			this.cbContainsColName.UseVisualStyleBackColor = true;
			this.cbContainsColName.CheckedChanged += new System.EventHandler(this.cbContainsColName_CheckedChanged);
			// 
			// cbExportType
			// 
			this.cbExportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbExportType.FormattingEnabled = true;
			this.cbExportType.Items.AddRange(new object[] {
            "文本文件",
            "Excel文件"});
			this.cbExportType.Location = new System.Drawing.Point(98, 36);
			this.cbExportType.Name = "cbExportType";
			this.cbExportType.Size = new System.Drawing.Size(151, 20);
			this.cbExportType.TabIndex = 19;
			this.cbExportType.SelectedIndexChanged += new System.EventHandler(this.cbExportType_SelectedIndexChanged);
			// 
			// btnConfirm
			// 
			this.btnConfirm.Location = new System.Drawing.Point(60, 179);
			this.btnConfirm.Name = "btnConfirm";
			this.btnConfirm.Size = new System.Drawing.Size(75, 23);
			this.btnConfirm.TabIndex = 20;
			this.btnConfirm.Text = "确定";
			this.btnConfirm.UseVisualStyleBackColor = true;
			this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(163, 179);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 21;
			this.btnCancel.Text = "取消";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslStatus,
            this.tspbProcess});
			this.statusStrip1.Location = new System.Drawing.Point(0, 210);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(305, 22);
			this.statusStrip1.TabIndex = 22;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// tsslStatus
			// 
			this.tsslStatus.Name = "tsslStatus";
			this.tsslStatus.Size = new System.Drawing.Size(29, 17);
			this.tsslStatus.Text = "状态";
			// 
			// tspbProcess
			// 
			this.tspbProcess.Name = "tspbProcess";
			this.tspbProcess.Size = new System.Drawing.Size(300, 16);
			// 
			// sfd
			// 
			this.sfd.DefaultExt = "txt";
			this.sfd.Filter = "文本文件(*.txt; *.csv; *.prn)|*.txt; *.csv; *.prn|Excel文件(*.xls;*.xl*)|*.xls;*.xl*|所有" +
				"文件(*.*)|*.*";
			// 
			// Export
			// 
			this.AcceptButton = this.btnConfirm;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(305, 232);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnConfirm);
			this.Controls.Add(this.cbExportType);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lblRowCount);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(313, 266);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(313, 266);
			this.Name = "Export";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "导出";
			this.Load += new System.EventHandler(this.Export_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblRowCount;
		private System.Windows.Forms.CheckBox cbContainsColName;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cbExportType;
		private System.Windows.Forms.ComboBox cbColSpliter;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnConfirm;
		private System.Windows.Forms.ComboBox cbRowSpliter;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel tsslStatus;
		private System.Windows.Forms.ToolStripProgressBar tspbProcess;
		private System.Windows.Forms.SaveFileDialog sfd;

	}
}