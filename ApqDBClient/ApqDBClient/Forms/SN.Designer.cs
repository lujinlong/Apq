namespace ApqDBClient.Forms
{
	partial class SN
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SN));
			this.btnCreate = new DevExpress.XtraEditors.SimpleButton();
			this.txtSN = new ICSharpCode.TextEditor.TextEditorControl();
			this.txtName = new DevExpress.XtraEditors.TextEdit();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
			this.btnVerify = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// btnCreate
			// 
			this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCreate.Location = new System.Drawing.Point(572, 11);
			this.btnCreate.Name = "btnCreate";
			this.btnCreate.Size = new System.Drawing.Size(75, 23);
			this.btnCreate.TabIndex = 1;
			this.btnCreate.Text = "创建";
			this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
			// 
			// txtSN
			// 
			this.txtSN.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtSN.Encoding = ((System.Text.Encoding)(resources.GetObject("txtSN.Encoding")));
			this.txtSN.IsIconBarVisible = false;
			this.txtSN.Location = new System.Drawing.Point(0, 73);
			this.txtSN.Name = "txtSN";
			this.txtSN.ShowEOLMarkers = true;
			this.txtSN.ShowSpaces = true;
			this.txtSN.ShowTabs = true;
			this.txtSN.ShowVRuler = true;
			this.txtSN.Size = new System.Drawing.Size(760, 392);
			this.txtSN.TabIndex = 5;
			// 
			// txtName
			// 
			this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtName.Location = new System.Drawing.Point(54, 12);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(512, 21);
			this.txtName.TabIndex = 6;
			// 
			// labelControl1
			// 
			this.labelControl1.Location = new System.Drawing.Point(12, 15);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(36, 14);
			this.labelControl1.TabIndex = 7;
			this.labelControl1.Text = "用户名";
			// 
			// labelControl2
			// 
			this.labelControl2.Location = new System.Drawing.Point(12, 53);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(36, 14);
			this.labelControl2.TabIndex = 8;
			this.labelControl2.Text = "注册串";
			// 
			// btnVerify
			// 
			this.btnVerify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnVerify.Location = new System.Drawing.Point(653, 11);
			this.btnVerify.Name = "btnVerify";
			this.btnVerify.Size = new System.Drawing.Size(75, 23);
			this.btnVerify.TabIndex = 9;
			this.btnVerify.Text = "验证";
			this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
			// 
			// SN
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(760, 466);
			this.Controls.Add(this.btnVerify);
			this.Controls.Add(this.labelControl2);
			this.Controls.Add(this.labelControl1);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.txtSN);
			this.Controls.Add(this.btnCreate);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.Name = "SN";
			this.TabText = "SN";
			this.Text = "SN";
			this.Load += new System.EventHandler(this.RSAKey_Load);
			((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.SimpleButton btnCreate;
		private ICSharpCode.TextEditor.TextEditorControl txtSN;
		private DevExpress.XtraEditors.TextEdit txtName;
		private DevExpress.XtraEditors.LabelControl labelControl1;
		private DevExpress.XtraEditors.LabelControl labelControl2;
		private DevExpress.XtraEditors.SimpleButton btnVerify;
	}
}