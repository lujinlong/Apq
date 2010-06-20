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
			this.btnSaveToFile = new DevExpress.XtraEditors.SimpleButton();
			this.ceContainsPKey = new DevExpress.XtraEditors.CheckEdit();
			this.txtRSAKey = new ICSharpCode.TextEditor.TextEditorControl();
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
			// btnSaveToFile
			// 
			this.btnSaveToFile.Location = new System.Drawing.Point(280, 12);
			this.btnSaveToFile.Name = "btnSaveToFile";
			this.btnSaveToFile.Size = new System.Drawing.Size(75, 23);
			this.btnSaveToFile.TabIndex = 2;
			this.btnSaveToFile.Text = "保存";
			this.btnSaveToFile.Click += new System.EventHandler(this.btnSaveToFile_Click);
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
			// txtRSAKey
			// 
			this.txtRSAKey.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtRSAKey.Encoding = ((System.Text.Encoding)(resources.GetObject("txtRSAKey.Encoding")));
			this.txtRSAKey.IsIconBarVisible = false;
			this.txtRSAKey.Location = new System.Drawing.Point(0, 42);
			this.txtRSAKey.Name = "txtRSAKey";
			this.txtRSAKey.Size = new System.Drawing.Size(760, 424);
			this.txtRSAKey.TabIndex = 4;
			// 
			// RSAKey
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(760, 466);
			this.Controls.Add(this.txtRSAKey);
			this.Controls.Add(this.ceContainsPKey);
			this.Controls.Add(this.btnSaveToFile);
			this.Controls.Add(this.btnCreate);
			this.Name = "RSAKey";
			this.TabText = "RSAKey";
			this.Text = "RSA密钥对";
			this.Load += new System.EventHandler(this.RSAKey_Load);
			((System.ComponentModel.ISupportInitialize)(this.ceContainsPKey.Properties)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.SimpleButton btnCreate;
		private DevExpress.XtraEditors.SimpleButton btnSaveToFile;
		private DevExpress.XtraEditors.CheckEdit ceContainsPKey;
		private ICSharpCode.TextEditor.TextEditorControl txtRSAKey;
	}
}