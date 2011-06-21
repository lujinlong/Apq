namespace ApqDBCManager.Forms
{
	partial class MainOption
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
			this.btnConfirm = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.txtDesKey = new System.Windows.Forms.TextBox();
			this.btnShowPwd = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnConfirm
			// 
			this.btnConfirm.Location = new System.Drawing.Point(244, 320);
			this.btnConfirm.Name = "btnConfirm";
			this.btnConfirm.Size = new System.Drawing.Size(75, 23);
			this.btnConfirm.TabIndex = 4;
			this.btnConfirm.Text = "确定";
			this.btnConfirm.UseVisualStyleBackColor = true;
			this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(384, 320);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "取消";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(28, 53);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(143, 12);
			this.label1.TabIndex = 6;
			this.label1.Text = "加解密密码(至少9个字符)";
			// 
			// txtDesKey
			// 
			this.txtDesKey.Location = new System.Drawing.Point(177, 50);
			this.txtDesKey.Name = "txtDesKey";
			this.txtDesKey.PasswordChar = '*';
			this.txtDesKey.Size = new System.Drawing.Size(260, 21);
			this.txtDesKey.TabIndex = 7;
			// 
			// btnShowPwd
			// 
			this.btnShowPwd.Location = new System.Drawing.Point(452, 48);
			this.btnShowPwd.Name = "btnShowPwd";
			this.btnShowPwd.Size = new System.Drawing.Size(75, 23);
			this.btnShowPwd.TabIndex = 8;
			this.btnShowPwd.Text = "显示密码";
			this.btnShowPwd.UseVisualStyleBackColor = true;
			this.btnShowPwd.Click += new System.EventHandler(this.btnShowPwd_Click);
			// 
			// MainOption
			// 
			this.AcceptButton = this.btnConfirm;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(592, 366);
			this.Controls.Add(this.btnShowPwd);
			this.Controls.Add(this.txtDesKey);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnConfirm);
			this.Controls.Add(this.btnCancel);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(600, 400);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(600, 400);
			this.Name = "MainOption";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "选项";
			this.Load += new System.EventHandler(this.MainOption_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnConfirm;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtDesKey;
		private System.Windows.Forms.Button btnShowPwd;





	}
}