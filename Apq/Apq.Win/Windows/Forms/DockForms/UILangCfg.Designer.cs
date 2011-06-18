namespace Apq.Windows.Forms.DockForms
{
	partial class UILangCfg
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UILangCfg));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsbSave = new System.Windows.Forms.ToolStripButton();
			this.tscbFile = new System.Windows.Forms.ToolStripComboBox();
			this.tsbApply = new System.Windows.Forms.ToolStripButton();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.col1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.col2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.bsUILang = new System.Windows.Forms.BindingSource(this.components);
			this.uiLang1 = new Apq.XSD.UILang();
			this.tsbCur = new System.Windows.Forms.ToolStripButton();
			this.toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bsUILang)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.uiLang1)).BeginInit();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSave,
            this.tscbFile,
            this.tsbCur,
            this.tsbApply});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(767, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tsbSave
			// 
			this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
			this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbSave.Name = "tsbSave";
			this.tsbSave.Size = new System.Drawing.Size(51, 22);
			this.tsbSave.Text = "保存(&S)";
			this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
			// 
			// tscbFile
			// 
			this.tscbFile.Items.AddRange(new object[] {
            ""});
			this.tscbFile.Name = "tscbFile";
			this.tscbFile.Size = new System.Drawing.Size(121, 25);
			this.tscbFile.SelectedIndexChanged += new System.EventHandler(this.tscbFile_SelectedIndexChanged);
			this.tscbFile.TextChanged += new System.EventHandler(this.tscbFile_TextChanged);
			// 
			// tsbApply
			// 
			this.tsbApply.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbApply.Image = ((System.Drawing.Image)(resources.GetObject("tsbApply.Image")));
			this.tsbApply.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbApply.Name = "tsbApply";
			this.tsbApply.Size = new System.Drawing.Size(51, 22);
			this.tsbApply.Text = "应用(&A)";
			this.tsbApply.Click += new System.EventHandler(this.tsbApply_Click);
			// 
			// dataGridView1
			// 
			this.dataGridView1.AutoGenerateColumns = false;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col1,
            this.col2});
			this.dataGridView1.DataSource = this.bsUILang;
			this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView1.Location = new System.Drawing.Point(0, 25);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowTemplate.Height = 23;
			this.dataGridView1.Size = new System.Drawing.Size(767, 382);
			this.dataGridView1.TabIndex = 1;
			// 
			// col1
			// 
			this.col1.DataPropertyName = "name";
			this.col1.HeaderText = "原文";
			this.col1.Name = "col1";
			this.col1.Width = 300;
			// 
			// col2
			// 
			this.col2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.col2.DataPropertyName = "value";
			this.col2.HeaderText = "中文";
			this.col2.Name = "col2";
			// 
			// bsUILang
			// 
			this.bsUILang.DataMember = "UILang";
			this.bsUILang.DataSource = this.uiLang1;
			// 
			// uiLang1
			// 
			this.uiLang1.DataSetName = "UILang";
			this.uiLang1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// tsbCur
			// 
			this.tsbCur.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbCur.Image = ((System.Drawing.Image)(resources.GetObject("tsbCur.Image")));
			this.tsbCur.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbCur.Name = "tsbCur";
			this.tsbCur.Size = new System.Drawing.Size(51, 22);
			this.tsbCur.Text = "当前(&U)";
			this.tsbCur.Click += new System.EventHandler(this.tsbCur_Click);
			// 
			// UILangCfg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(767, 407);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.toolStrip1);
			this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.HideOnClose = true;
			this.Name = "UILangCfg";
			this.ShowIcon = false;
			this.TabText = "语言设置";
			this.Text = "语言设置";
			this.Load += new System.EventHandler(this.UILangCfg_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bsUILang)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.uiLang1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsbSave;
		private System.Windows.Forms.ToolStripComboBox tscbFile;
		private System.Windows.Forms.ToolStripButton tsbApply;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.BindingSource bsUILang;
		private Apq.XSD.UILang uiLang1;
		private System.Windows.Forms.DataGridViewTextBoxColumn col1;
		private System.Windows.Forms.DataGridViewTextBoxColumn col2;
		private System.Windows.Forms.ToolStripButton tsbCur;
	}
}