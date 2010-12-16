namespace ApqDBManager.Forms
{
	partial class CryptCS
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CryptCS));
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
			this.beDFile = new DevExpress.XtraEditors.ButtonEdit();
			this.beEFile = new DevExpress.XtraEditors.ButtonEdit();
			this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
			this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
			this.btnDecryptFile = new DevExpress.XtraEditors.SimpleButton();
			this.btnEncryptFile = new DevExpress.XtraEditors.SimpleButton();
			this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
			this.btnDecryptString = new DevExpress.XtraEditors.SimpleButton();
			this.btnEncryptString = new DevExpress.XtraEditors.SimpleButton();
			this.meOutput = new DevExpress.XtraEditors.MemoEdit();
			this.meInput = new DevExpress.XtraEditors.MemoEdit();
			this.ofdDFile = new System.Windows.Forms.OpenFileDialog();
			this.ofdEFile = new System.Windows.Forms.OpenFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.beDFile.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.beEFile.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
			this.xtraTabControl1.SuspendLayout();
			this.xtraTabPage1.SuspendLayout();
			this.xtraTabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.meOutput.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.meInput.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// labelControl1
			// 
			this.labelControl1.Location = new System.Drawing.Point(18, 50);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(48, 14);
			this.labelControl1.TabIndex = 1;
			this.labelControl1.Text = "明文文件";
			// 
			// labelControl2
			// 
			this.labelControl2.Location = new System.Drawing.Point(18, 104);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(48, 14);
			this.labelControl2.TabIndex = 2;
			this.labelControl2.Text = "密文文件";
			// 
			// beDFile
			// 
			this.beDFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.beDFile.Location = new System.Drawing.Point(72, 47);
			this.beDFile.Name = "beDFile";
			this.beDFile.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.beDFile.Size = new System.Drawing.Size(495, 21);
			this.beDFile.TabIndex = 5;
			this.beDFile.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.beDFile_ButtonClick);
			this.beDFile.EditValueChanged += new System.EventHandler(this.beDFile_EditValueChanged);
			// 
			// beEFile
			// 
			this.beEFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.beEFile.Location = new System.Drawing.Point(72, 101);
			this.beEFile.Name = "beEFile";
			this.beEFile.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.beEFile.Size = new System.Drawing.Size(495, 21);
			this.beEFile.TabIndex = 6;
			this.beEFile.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.beEFile_ButtonClick);
			this.beEFile.EditValueChanged += new System.EventHandler(this.beEFile_EditValueChanged);
			// 
			// xtraTabControl1
			// 
			this.xtraTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.xtraTabControl1.Location = new System.Drawing.Point(3, 12);
			this.xtraTabControl1.Name = "xtraTabControl1";
			this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
			this.xtraTabControl1.Size = new System.Drawing.Size(593, 410);
			this.xtraTabControl1.TabIndex = 7;
			this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
			// 
			// xtraTabPage1
			// 
			this.xtraTabPage1.Controls.Add(this.btnDecryptFile);
			this.xtraTabPage1.Controls.Add(this.btnEncryptFile);
			this.xtraTabPage1.Controls.Add(this.beDFile);
			this.xtraTabPage1.Controls.Add(this.beEFile);
			this.xtraTabPage1.Controls.Add(this.labelControl1);
			this.xtraTabPage1.Controls.Add(this.labelControl2);
			this.xtraTabPage1.Name = "xtraTabPage1";
			this.xtraTabPage1.Size = new System.Drawing.Size(586, 380);
			this.xtraTabPage1.Text = "文件";
			// 
			// btnDecryptFile
			// 
			this.btnDecryptFile.Location = new System.Drawing.Point(327, 179);
			this.btnDecryptFile.Name = "btnDecryptFile";
			this.btnDecryptFile.Size = new System.Drawing.Size(75, 23);
			this.btnDecryptFile.TabIndex = 8;
			this.btnDecryptFile.Text = "解密";
			this.btnDecryptFile.Click += new System.EventHandler(this.btnDecryptFile_Click);
			// 
			// btnEncryptFile
			// 
			this.btnEncryptFile.Location = new System.Drawing.Point(175, 179);
			this.btnEncryptFile.Name = "btnEncryptFile";
			this.btnEncryptFile.Size = new System.Drawing.Size(75, 23);
			this.btnEncryptFile.TabIndex = 7;
			this.btnEncryptFile.Text = "加密";
			this.btnEncryptFile.Click += new System.EventHandler(this.btnEncryptFile_Click);
			// 
			// xtraTabPage2
			// 
			this.xtraTabPage2.Controls.Add(this.btnDecryptString);
			this.xtraTabPage2.Controls.Add(this.btnEncryptString);
			this.xtraTabPage2.Controls.Add(this.meOutput);
			this.xtraTabPage2.Controls.Add(this.meInput);
			this.xtraTabPage2.Name = "xtraTabPage2";
			this.xtraTabPage2.Size = new System.Drawing.Size(586, 380);
			this.xtraTabPage2.Text = "字符串";
			// 
			// btnDecryptString
			// 
			this.btnDecryptString.Location = new System.Drawing.Point(341, 137);
			this.btnDecryptString.Name = "btnDecryptString";
			this.btnDecryptString.Size = new System.Drawing.Size(75, 23);
			this.btnDecryptString.TabIndex = 10;
			this.btnDecryptString.Text = "解密";
			this.btnDecryptString.Click += new System.EventHandler(this.btnDecryptString_Click);
			// 
			// btnEncryptString
			// 
			this.btnEncryptString.Location = new System.Drawing.Point(215, 137);
			this.btnEncryptString.Name = "btnEncryptString";
			this.btnEncryptString.Size = new System.Drawing.Size(75, 23);
			this.btnEncryptString.TabIndex = 9;
			this.btnEncryptString.Text = "加密";
			this.btnEncryptString.Click += new System.EventHandler(this.btnEncryptString_Click);
			// 
			// meOutput
			// 
			this.meOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.meOutput.Location = new System.Drawing.Point(6, 166);
			this.meOutput.Name = "meOutput";
			this.meOutput.Size = new System.Drawing.Size(577, 208);
			this.meOutput.TabIndex = 1;
			// 
			// meInput
			// 
			this.meInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.meInput.Location = new System.Drawing.Point(6, 3);
			this.meInput.Name = "meInput";
			this.meInput.Size = new System.Drawing.Size(577, 128);
			this.meInput.TabIndex = 0;
			this.meInput.EditValueChanged += new System.EventHandler(this.meInput_EditValueChanged);
			// 
			// ofdDFile
			// 
			this.ofdDFile.CheckFileExists = false;
			this.ofdDFile.CheckPathExists = false;
			this.ofdDFile.DefaultExt = "xml";
			this.ofdDFile.Filter = "所有文件(*.*)|*.*";
			this.ofdDFile.RestoreDirectory = true;
			// 
			// ofdEFile
			// 
			this.ofdEFile.CheckFileExists = false;
			this.ofdEFile.CheckPathExists = false;
			this.ofdEFile.DefaultExt = "res";
			this.ofdEFile.Filter = "所有文件(*.*)|*.*";
			this.ofdEFile.RestoreDirectory = true;
			// 
			// CryptCS
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(600, 423);
			this.Controls.Add(this.xtraTabControl1);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "CryptCS";
			this.TabText = "CryptCS";
			this.Text = "CryptCS";
			this.Load += new System.EventHandler(this.CryptCS_Load);
			((System.ComponentModel.ISupportInitialize)(this.beDFile.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.beEFile.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
			this.xtraTabControl1.ResumeLayout(false);
			this.xtraTabPage1.ResumeLayout(false);
			this.xtraTabPage1.PerformLayout();
			this.xtraTabPage2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.meOutput.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.meInput.Properties)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.LabelControl labelControl1;
		private DevExpress.XtraEditors.LabelControl labelControl2;
		private DevExpress.XtraEditors.ButtonEdit beDFile;
		private DevExpress.XtraEditors.ButtonEdit beEFile;
		private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
		private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
		private DevExpress.XtraEditors.SimpleButton btnDecryptFile;
		private DevExpress.XtraEditors.SimpleButton btnEncryptFile;
		private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
		private DevExpress.XtraEditors.SimpleButton btnDecryptString;
		private DevExpress.XtraEditors.SimpleButton btnEncryptString;
		private DevExpress.XtraEditors.MemoEdit meOutput;
		private DevExpress.XtraEditors.MemoEdit meInput;
		private System.Windows.Forms.OpenFileDialog ofdDFile;
		private System.Windows.Forms.OpenFileDialog ofdEFile;
	}
}