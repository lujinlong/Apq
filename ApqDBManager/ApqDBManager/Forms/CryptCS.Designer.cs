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
			this.ofdDFile = new System.Windows.Forms.OpenFileDialog();
			this.ofdEFile = new System.Windows.Forms.OpenFileDialog();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.btnDecryptFile = new System.Windows.Forms.Button();
			this.btnEncryptFile = new System.Windows.Forms.Button();
			this.cbEFile = new System.Windows.Forms.ComboBox();
			this.cbDFile = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.btnDecryptString = new System.Windows.Forms.Button();
			this.btnEncryptString = new System.Windows.Forms.Button();
			this.txtOutput = new System.Windows.Forms.TextBox();
			this.txtInput = new System.Windows.Forms.TextBox();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
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
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(600, 423);
			this.tabControl1.TabIndex = 17;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.btnDecryptFile);
			this.tabPage1.Controls.Add(this.btnEncryptFile);
			this.tabPage1.Controls.Add(this.cbEFile);
			this.tabPage1.Controls.Add(this.cbDFile);
			this.tabPage1.Controls.Add(this.label4);
			this.tabPage1.Controls.Add(this.label3);
			this.tabPage1.Location = new System.Drawing.Point(4, 21);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(592, 398);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "文件";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// btnDecryptFile
			// 
			this.btnDecryptFile.Location = new System.Drawing.Point(354, 194);
			this.btnDecryptFile.Name = "btnDecryptFile";
			this.btnDecryptFile.Size = new System.Drawing.Size(75, 23);
			this.btnDecryptFile.TabIndex = 16;
			this.btnDecryptFile.Text = "解密↑";
			this.btnDecryptFile.UseVisualStyleBackColor = true;
			this.btnDecryptFile.Click += new System.EventHandler(this.btnDecryptFile_Click);
			// 
			// btnEncryptFile
			// 
			this.btnEncryptFile.Location = new System.Drawing.Point(202, 194);
			this.btnEncryptFile.Name = "btnEncryptFile";
			this.btnEncryptFile.Size = new System.Drawing.Size(75, 23);
			this.btnEncryptFile.TabIndex = 15;
			this.btnEncryptFile.Text = "加密↓";
			this.btnEncryptFile.UseVisualStyleBackColor = true;
			this.btnEncryptFile.Click += new System.EventHandler(this.btnEncryptFile_Click);
			// 
			// cbEFile
			// 
			this.cbEFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cbEFile.DropDownHeight = 1;
			this.cbEFile.FormattingEnabled = true;
			this.cbEFile.IntegralHeight = false;
			this.cbEFile.Location = new System.Drawing.Point(64, 118);
			this.cbEFile.Name = "cbEFile";
			this.cbEFile.Size = new System.Drawing.Size(520, 20);
			this.cbEFile.TabIndex = 14;
			// 
			// cbDFile
			// 
			this.cbDFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cbDFile.DropDownHeight = 1;
			this.cbDFile.FormattingEnabled = true;
			this.cbDFile.IntegralHeight = false;
			this.cbDFile.Location = new System.Drawing.Point(64, 64);
			this.cbDFile.Name = "cbDFile";
			this.cbDFile.Size = new System.Drawing.Size(520, 20);
			this.cbDFile.TabIndex = 13;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 121);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(53, 12);
			this.label4.TabIndex = 12;
			this.label4.Text = "加密文件";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 67);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(53, 12);
			this.label3.TabIndex = 11;
			this.label3.Text = "原始文件";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.btnDecryptString);
			this.tabPage2.Controls.Add(this.btnEncryptString);
			this.tabPage2.Controls.Add(this.txtOutput);
			this.tabPage2.Controls.Add(this.txtInput);
			this.tabPage2.Location = new System.Drawing.Point(4, 21);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(592, 398);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "字符串";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// btnDecryptString
			// 
			this.btnDecryptString.Location = new System.Drawing.Point(347, 140);
			this.btnDecryptString.Name = "btnDecryptString";
			this.btnDecryptString.Size = new System.Drawing.Size(75, 23);
			this.btnDecryptString.TabIndex = 17;
			this.btnDecryptString.Text = "解密↑";
			this.btnDecryptString.UseVisualStyleBackColor = true;
			this.btnDecryptString.Click += new System.EventHandler(this.btnDecryptString_Click);
			// 
			// btnEncryptString
			// 
			this.btnEncryptString.Location = new System.Drawing.Point(221, 140);
			this.btnEncryptString.Name = "btnEncryptString";
			this.btnEncryptString.Size = new System.Drawing.Size(75, 23);
			this.btnEncryptString.TabIndex = 16;
			this.btnEncryptString.Text = "加密↓";
			this.btnEncryptString.UseVisualStyleBackColor = true;
			this.btnEncryptString.Click += new System.EventHandler(this.btnEncryptString_Click);
			// 
			// txtOutput
			// 
			this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtOutput.Location = new System.Drawing.Point(7, 177);
			this.txtOutput.Multiline = true;
			this.txtOutput.Name = "txtOutput";
			this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtOutput.Size = new System.Drawing.Size(578, 216);
			this.txtOutput.TabIndex = 12;
			// 
			// txtInput
			// 
			this.txtInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtInput.Location = new System.Drawing.Point(6, 6);
			this.txtInput.Multiline = true;
			this.txtInput.Name = "txtInput";
			this.txtInput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtInput.Size = new System.Drawing.Size(578, 128);
			this.txtInput.TabIndex = 11;
			this.txtInput.WordWrap = false;
			// 
			// CryptCS
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(600, 423);
			this.Controls.Add(this.tabControl1);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "CryptCS";
			this.TabText = "CryptCS";
			this.Text = "CryptCS";
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.OpenFileDialog ofdDFile;
		private System.Windows.Forms.OpenFileDialog ofdEFile;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.Button btnDecryptFile;
		private System.Windows.Forms.Button btnEncryptFile;
		private System.Windows.Forms.ComboBox cbEFile;
		private System.Windows.Forms.ComboBox cbDFile;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Button btnDecryptString;
		private System.Windows.Forms.Button btnEncryptString;
		private System.Windows.Forms.TextBox txtOutput;
		private System.Windows.Forms.TextBox txtInput;
	}
}