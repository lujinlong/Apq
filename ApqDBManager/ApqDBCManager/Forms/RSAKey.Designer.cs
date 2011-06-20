namespace ApqDBCManager.Forms
{
	partial class RSAKey
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
			this.cbContainsPKey = new System.Windows.Forms.CheckBox();
			this.btnCreate = new System.Windows.Forms.Button();
			this.btnSaveUToFile = new System.Windows.Forms.Button();
			this.btnSavePToFile = new System.Windows.Forms.Button();
			this.sfdU = new System.Windows.Forms.SaveFileDialog();
			this.sfdP = new System.Windows.Forms.SaveFileDialog();
			this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
			this.elementHost2 = new System.Windows.Forms.Integration.ElementHost();
			this.SuspendLayout();
			// 
			// cbContainsPKey
			// 
			this.cbContainsPKey.AutoSize = true;
			this.cbContainsPKey.Checked = true;
			this.cbContainsPKey.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbContainsPKey.Location = new System.Drawing.Point(12, 12);
			this.cbContainsPKey.Name = "cbContainsPKey";
			this.cbContainsPKey.Size = new System.Drawing.Size(72, 16);
			this.cbContainsPKey.TabIndex = 7;
			this.cbContainsPKey.Text = "包含私钥";
			this.cbContainsPKey.UseVisualStyleBackColor = true;
			// 
			// btnCreate
			// 
			this.btnCreate.Location = new System.Drawing.Point(167, 8);
			this.btnCreate.Name = "btnCreate";
			this.btnCreate.Size = new System.Drawing.Size(75, 23);
			this.btnCreate.TabIndex = 8;
			this.btnCreate.Text = "创建";
			this.btnCreate.UseVisualStyleBackColor = true;
			this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
			// 
			// btnSaveUToFile
			// 
			this.btnSaveUToFile.Location = new System.Drawing.Point(290, 8);
			this.btnSaveUToFile.Name = "btnSaveUToFile";
			this.btnSaveUToFile.Size = new System.Drawing.Size(75, 23);
			this.btnSaveUToFile.TabIndex = 9;
			this.btnSaveUToFile.Text = "保存公钥";
			this.btnSaveUToFile.UseVisualStyleBackColor = true;
			this.btnSaveUToFile.Click += new System.EventHandler(this.btnSaveUToFile_Click);
			// 
			// btnSavePToFile
			// 
			this.btnSavePToFile.Location = new System.Drawing.Point(398, 8);
			this.btnSavePToFile.Name = "btnSavePToFile";
			this.btnSavePToFile.Size = new System.Drawing.Size(75, 23);
			this.btnSavePToFile.TabIndex = 10;
			this.btnSavePToFile.Text = "保存私钥";
			this.btnSavePToFile.UseVisualStyleBackColor = true;
			this.btnSavePToFile.Click += new System.EventHandler(this.btnSavePToFile_Click);
			// 
			// sfdU
			// 
			this.sfdU.DefaultExt = "xml";
			this.sfdU.RestoreDirectory = true;
			// 
			// sfdP
			// 
			this.sfdP.DefaultExt = "xml";
			this.sfdP.RestoreDirectory = true;
			// 
			// elementHost1
			// 
			this.elementHost1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.elementHost1.Location = new System.Drawing.Point(0, 42);
			this.elementHost1.Name = "elementHost1";
			this.elementHost1.Size = new System.Drawing.Size(760, 144);
			this.elementHost1.TabIndex = 11;
			this.elementHost1.Text = "elementHost1";
			this.elementHost1.Child = null;
			// 
			// elementHost2
			// 
			this.elementHost2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.elementHost2.Location = new System.Drawing.Point(0, 192);
			this.elementHost2.Name = "elementHost2";
			this.elementHost2.Size = new System.Drawing.Size(760, 217);
			this.elementHost2.TabIndex = 12;
			this.elementHost2.Text = "elementHost2";
			this.elementHost2.Child = null;
			// 
			// RSAKey
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(760, 410);
			this.Controls.Add(this.elementHost2);
			this.Controls.Add(this.elementHost1);
			this.Controls.Add(this.btnSavePToFile);
			this.Controls.Add(this.btnSaveUToFile);
			this.Controls.Add(this.btnCreate);
			this.Controls.Add(this.cbContainsPKey);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.Name = "RSAKey";
			this.TabText = "RSAKey";
			this.Text = "RSA密钥对";
			this.Load += new System.EventHandler(this.RSAKey_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox cbContainsPKey;
		private System.Windows.Forms.Button btnCreate;
		private System.Windows.Forms.Button btnSaveUToFile;
		private System.Windows.Forms.Button btnSavePToFile;
		private System.Windows.Forms.SaveFileDialog sfdU;
		private System.Windows.Forms.SaveFileDialog sfdP;
		private System.Windows.Forms.Integration.ElementHost elementHost1;
		private System.Windows.Forms.Integration.ElementHost elementHost2;
	}
}