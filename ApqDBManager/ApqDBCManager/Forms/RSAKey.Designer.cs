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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RSAKey));
			this.txtRSAUKey = new ICSharpCode.TextEditor.TextEditorControl();
			this.txtRSAPKey = new ICSharpCode.TextEditor.TextEditorControl();
			this.cbContainsPKey = new System.Windows.Forms.CheckBox();
			this.btnCreate = new System.Windows.Forms.Button();
			this.btnSaveUToFile = new System.Windows.Forms.Button();
			this.btnSavePToFile = new System.Windows.Forms.Button();
			this.sfdU = new System.Windows.Forms.SaveFileDialog();
			this.sfdP = new System.Windows.Forms.SaveFileDialog();
			this.SuspendLayout();
			// 
			// txtRSAUKey
			// 
			this.txtRSAUKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtRSAUKey.Encoding = ((System.Text.Encoding)(resources.GetObject("txtRSAUKey.Encoding")));
			this.txtRSAUKey.IsIconBarVisible = false;
			this.txtRSAUKey.Location = new System.Drawing.Point(0, 42);
			this.txtRSAUKey.Name = "txtRSAUKey";
			this.txtRSAUKey.ShowEOLMarkers = true;
			this.txtRSAUKey.ShowInvalidLines = false;
			this.txtRSAUKey.ShowSpaces = true;
			this.txtRSAUKey.ShowTabs = true;
			this.txtRSAUKey.ShowVRuler = true;
			this.txtRSAUKey.Size = new System.Drawing.Size(760, 144);
			this.txtRSAUKey.TabIndex = 4;
			// 
			// txtRSAPKey
			// 
			this.txtRSAPKey.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtRSAPKey.Encoding = ((System.Text.Encoding)(resources.GetObject("txtRSAPKey.Encoding")));
			this.txtRSAPKey.IsIconBarVisible = false;
			this.txtRSAPKey.Location = new System.Drawing.Point(0, 192);
			this.txtRSAPKey.Name = "txtRSAPKey";
			this.txtRSAPKey.ShowEOLMarkers = true;
			this.txtRSAPKey.ShowInvalidLines = false;
			this.txtRSAPKey.ShowSpaces = true;
			this.txtRSAPKey.ShowTabs = true;
			this.txtRSAPKey.ShowVRuler = true;
			this.txtRSAPKey.Size = new System.Drawing.Size(760, 217);
			this.txtRSAPKey.TabIndex = 5;
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
			// RSAKey
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(760, 410);
			this.Controls.Add(this.btnSavePToFile);
			this.Controls.Add(this.btnSaveUToFile);
			this.Controls.Add(this.btnCreate);
			this.Controls.Add(this.cbContainsPKey);
			this.Controls.Add(this.txtRSAPKey);
			this.Controls.Add(this.txtRSAUKey);
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

		private ICSharpCode.TextEditor.TextEditorControl txtRSAUKey;
		private ICSharpCode.TextEditor.TextEditorControl txtRSAPKey;
		private System.Windows.Forms.CheckBox cbContainsPKey;
		private System.Windows.Forms.Button btnCreate;
		private System.Windows.Forms.Button btnSaveUToFile;
		private System.Windows.Forms.Button btnSavePToFile;
		private System.Windows.Forms.SaveFileDialog sfdU;
		private System.Windows.Forms.SaveFileDialog sfdP;
	}
}