namespace Apq.Windows.Controls.DataSetDTS
{
	partial class ToExcel
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

		#region 组件设计器生成的代码

		/// <summary> 
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.ceColName = new DevExpress.XtraEditors.CheckEdit();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.beFile = new DevExpress.XtraEditors.ButtonEdit();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.ceColName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.beFile.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// ceColName
			// 
			this.ceColName.EditValue = true;
			this.ceColName.Enabled = false;
			this.ceColName.Location = new System.Drawing.Point(59, 150);
			this.ceColName.Name = "ceColName";
			this.ceColName.Properties.Caption = "首行为列名称(&F)";
			this.ceColName.Size = new System.Drawing.Size(115, 19);
			this.ceColName.TabIndex = 4;
			// 
			// labelControl1
			// 
			this.labelControl1.Location = new System.Drawing.Point(29, 75);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(84, 14);
			this.labelControl1.TabIndex = 3;
			this.labelControl1.Text = "Excel 文件路径:";
			// 
			// beFile
			// 
			this.beFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.beFile.Location = new System.Drawing.Point(61, 95);
			this.beFile.Name = "beFile";
			this.beFile.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.beFile.Size = new System.Drawing.Size(488, 21);
			this.beFile.TabIndex = 5;
			this.beFile.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.beFile_ButtonClick);
			this.beFile.EditValueChanged += new System.EventHandler(this.beFile_EditValueChanged);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// Excel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.beFile);
			this.Controls.Add(this.ceColName);
			this.Controls.Add(this.labelControl1);
			this.Name = "Excel";
			this.Size = new System.Drawing.Size(580, 360);
			this.Load += new System.EventHandler(this.Excel_Load);
			((System.ComponentModel.ISupportInitialize)(this.ceColName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.beFile.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.LabelControl labelControl1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		/// <summary>
		/// 首行是否为列名
		/// </summary>
		public DevExpress.XtraEditors.CheckEdit ceColName;
		/// <summary>
		/// 文件路径
		/// </summary>
		public DevExpress.XtraEditors.ButtonEdit beFile;
	}
}
