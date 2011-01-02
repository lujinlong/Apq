namespace ApqDBManager.Forms
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
			this.btnCreate = new DevExpress.XtraEditors.SimpleButton();
			this.btnSaveUToFile = new DevExpress.XtraEditors.SimpleButton();
			this.ceContainsPKey = new DevExpress.XtraEditors.CheckEdit();
			this.txtRSAUKey = new ICSharpCode.TextEditor.TextEditorControl();
			this.txtRSAPKey = new ICSharpCode.TextEditor.TextEditorControl();
			this.btnSavePToFile = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.ceContainsPKey.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// btnCreate
			// 
			this.btnCreate.Location = new System.Drawing.Point(168, 12);
			this.btnCreate.Name = "btnCreate";
			this.btnCreate.Size = new System.Drawing.Size(75, 23);
			this.btnCreate.TabIndex = 1;
			this.btnCreate.Text = "创建";
			this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
			// 
			// btnSaveUToFile
			// 
			this.btnSaveUToFile.Location = new System.Drawing.Point(280, 12);
			this.btnSaveUToFile.Name = "btnSaveUToFile";
			this.btnSaveUToFile.Size = new System.Drawing.Size(75, 23);
			this.btnSaveUToFile.TabIndex = 2;
			this.btnSaveUToFile.Text = "保存公钥";
			this.btnSaveUToFile.Click += new System.EventHandler(this.btnSaveUToFile_Click);
			// 
			// ceContainsPKey
			// 
			this.ceContainsPKey.EditValue = true;
			this.ceContainsPKey.Location = new System.Drawing.Point(12, 14);
			this.ceContainsPKey.Name = "ceContainsPKey";
			this.ceContainsPKey.Properties.Caption = "包含私钥";
			this.ceContainsPKey.Size = new System.Drawing.Size(75, 19);
			this.ceContainsPKey.TabIndex = 3;
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
			this.txtRSAUKey.ShowSpaces = true;
			this.txtRSAUKey.ShowTabs = true;
			this.txtRSAUKey.ShowVRuler = true;
			this.txtRSAUKey.Size = new System.Drawing.Size(760, 189);
			this.txtRSAUKey.TabIndex = 4;
			// 
			// txtRSAPKey
			// 
			this.txtRSAPKey.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtRSAPKey.Encoding = ((System.Text.Encoding)(resources.GetObject("txtRSAPKey.Encoding")));
			this.txtRSAPKey.IsIconBarVisible = false;
			this.txtRSAPKey.Location = new System.Drawing.Point(0, 252);
			this.txtRSAPKey.Name = "txtRSAPKey";
			this.txtRSAPKey.ShowEOLMarkers = true;
			this.txtRSAPKey.ShowSpaces = true;
			this.txtRSAPKey.ShowTabs = true;
			this.txtRSAPKey.ShowVRuler = true;
			this.txtRSAPKey.Size = new System.Drawing.Size(760, 189);
			this.txtRSAPKey.TabIndex = 5;
			// 
			// btnSavePToFile
			// 
			this.btnSavePToFile.Location = new System.Drawing.Point(389, 13);
			this.btnSavePToFile.Name = "btnSavePToFile";
			this.btnSavePToFile.Size = new System.Drawing.Size(75, 23);
			this.btnSavePToFile.TabIndex = 6;
			this.btnSavePToFile.Text = "保存私钥";
			this.btnSavePToFile.Click += new System.EventHandler(this.btnSavePToFile_Click);
			// 
			// RSAKey
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(760, 466);
			this.Controls.Add(this.btnSavePToFile);
			this.Controls.Add(this.txtRSAPKey);
			this.Controls.Add(this.txtRSAUKey);
			this.Controls.Add(this.ceContainsPKey);
			this.Controls.Add(this.btnSaveUToFile);
			this.Controls.Add(this.btnCreate);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.Name = "RSAKey";
			this.TabText = "RSAKey";
			this.Text = "RSA密钥对";
			this.Load += new System.EventHandler(this.RSAKey_Load);
			((System.ComponentModel.ISupportInitialize)(this.ceContainsPKey.Properties)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.SimpleButton btnCreate;
		private DevExpress.XtraEditors.SimpleButton btnSaveUToFile;
		private DevExpress.XtraEditors.CheckEdit ceContainsPKey;
		private ICSharpCode.TextEditor.TextEditorControl txtRSAUKey;
		private ICSharpCode.TextEditor.TextEditorControl txtRSAPKey;
		private DevExpress.XtraEditors.SimpleButton btnSavePToFile;
	}
}