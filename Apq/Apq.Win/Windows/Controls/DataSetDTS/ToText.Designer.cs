namespace Apq.Windows.Controls.DataSetDTS
{
	partial class ToText
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
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.beFile = new DevExpress.XtraEditors.ButtonEdit();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
			this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
			this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
			this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
			this.cbeFileEncoding = new DevExpress.XtraEditors.ComboBoxEdit();
			this.ceColName = new DevExpress.XtraEditors.CheckEdit();
			this.teSpliterCol = new DevExpress.XtraEditors.TextEdit();
			this.teSpliterRow = new DevExpress.XtraEditors.TextEdit();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.beFile.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cbeFileEncoding.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ceColName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.teSpliterCol.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.teSpliterRow.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// labelControl1
			// 
			this.labelControl1.Location = new System.Drawing.Point(17, 48);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(232, 14);
			this.labelControl1.TabIndex = 0;
			this.labelControl1.Text = "请选择一个文件并指定文件属性和文件格式:";
			// 
			// beFile
			// 
			this.beFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.beFile.Location = new System.Drawing.Point(93, 78);
			this.beFile.Name = "beFile";
			this.beFile.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.beFile.Size = new System.Drawing.Size(466, 21);
			this.beFile.TabIndex = 1;
			this.beFile.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.beFile_ButtonClick);
			this.beFile.EditValueChanged += new System.EventHandler(this.beFile_EditValueChanged);
			// 
			// labelControl2
			// 
			this.labelControl2.Location = new System.Drawing.Point(35, 81);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(52, 14);
			this.labelControl2.TabIndex = 2;
			this.labelControl2.Text = "选择文件:";
			// 
			// labelControl3
			// 
			this.labelControl3.Location = new System.Drawing.Point(35, 108);
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Size = new System.Drawing.Size(52, 14);
			this.labelControl3.TabIndex = 3;
			this.labelControl3.Text = "编码格式:";
			// 
			// labelControl4
			// 
			this.labelControl4.Location = new System.Drawing.Point(35, 168);
			this.labelControl4.Name = "labelControl4";
			this.labelControl4.Size = new System.Drawing.Size(52, 14);
			this.labelControl4.TabIndex = 4;
			this.labelControl4.Text = "列分隔符:";
			// 
			// labelControl5
			// 
			this.labelControl5.Location = new System.Drawing.Point(35, 195);
			this.labelControl5.Name = "labelControl5";
			this.labelControl5.Size = new System.Drawing.Size(52, 14);
			this.labelControl5.TabIndex = 5;
			this.labelControl5.Text = "行分隔符:";
			// 
			// cbeFileEncoding
			// 
			this.cbeFileEncoding.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cbeFileEncoding.EditValue = "UTF-8";
			this.cbeFileEncoding.Enabled = false;
			this.cbeFileEncoding.Location = new System.Drawing.Point(93, 105);
			this.cbeFileEncoding.Name = "cbeFileEncoding";
			this.cbeFileEncoding.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.cbeFileEncoding.Properties.Items.AddRange(new object[] {
            "UTF-8"});
			this.cbeFileEncoding.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.cbeFileEncoding.Size = new System.Drawing.Size(466, 21);
			this.cbeFileEncoding.TabIndex = 6;
			// 
			// ceColName
			// 
			this.ceColName.EditValue = true;
			this.ceColName.Enabled = false;
			this.ceColName.Location = new System.Drawing.Point(54, 219);
			this.ceColName.Name = "ceColName";
			this.ceColName.Properties.Caption = "首行为列名称(&F)";
			this.ceColName.Size = new System.Drawing.Size(115, 19);
			this.ceColName.TabIndex = 7;
			// 
			// teSpliterCol
			// 
			this.teSpliterCol.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.teSpliterCol.EditValue = ",";
			this.teSpliterCol.Enabled = false;
			this.teSpliterCol.Location = new System.Drawing.Point(93, 165);
			this.teSpliterCol.Name = "teSpliterCol";
			this.teSpliterCol.Size = new System.Drawing.Size(349, 21);
			this.teSpliterCol.TabIndex = 8;
			// 
			// teSpliterRow
			// 
			this.teSpliterRow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.teSpliterRow.EditValue = "\\r\\n";
			this.teSpliterRow.Enabled = false;
			this.teSpliterRow.Location = new System.Drawing.Point(93, 192);
			this.teSpliterRow.Name = "teSpliterRow";
			this.teSpliterRow.Size = new System.Drawing.Size(349, 21);
			this.teSpliterRow.TabIndex = 9;
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// Text1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.teSpliterRow);
			this.Controls.Add(this.teSpliterCol);
			this.Controls.Add(this.ceColName);
			this.Controls.Add(this.cbeFileEncoding);
			this.Controls.Add(this.labelControl5);
			this.Controls.Add(this.labelControl4);
			this.Controls.Add(this.labelControl3);
			this.Controls.Add(this.labelControl2);
			this.Controls.Add(this.beFile);
			this.Controls.Add(this.labelControl1);
			this.Name = "Text1";
			this.Size = new System.Drawing.Size(580, 360);
			this.Load += new System.EventHandler(this.Text1_Load);
			((System.ComponentModel.ISupportInitialize)(this.beFile.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cbeFileEncoding.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ceColName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.teSpliterCol.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.teSpliterRow.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.LabelControl labelControl1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private DevExpress.XtraEditors.LabelControl labelControl2;
		private DevExpress.XtraEditors.LabelControl labelControl3;
		private DevExpress.XtraEditors.LabelControl labelControl4;
		private DevExpress.XtraEditors.LabelControl labelControl5;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		/// <summary>
		/// 文件路径
		/// </summary>
		public DevExpress.XtraEditors.ButtonEdit beFile;
		/// <summary>
		/// 文件编码格式
		/// </summary>
		public DevExpress.XtraEditors.ComboBoxEdit cbeFileEncoding;
		/// <summary>
		/// 首行是否为列名
		/// </summary>
		public DevExpress.XtraEditors.CheckEdit ceColName;
		/// <summary>
		/// 列分隔符
		/// </summary>
		public DevExpress.XtraEditors.TextEdit teSpliterCol;
		/// <summary>
		/// 行分隔符
		/// </summary>
		public DevExpress.XtraEditors.TextEdit teSpliterRow;
	}
}
