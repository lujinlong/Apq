namespace ApqDBManager
{
	partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.label1 = new System.Windows.Forms.Label();
			this.txtUserName = new System.Windows.Forms.TextBox();
			this.txtSN = new System.Windows.Forms.TextBox();
			this.btnCreate = new System.Windows.Forms.Button();
			this.btnCopy = new System.Windows.Forms.Button();
			this.btnExit = new System.Windows.Forms.Button();
			this.txtRsaKey = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(10, 200);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "用户名";
			// 
			// txtUserName
			// 
			this.txtUserName.Location = new System.Drawing.Point(57, 197);
			this.txtUserName.Name = "txtUserName";
			this.txtUserName.Size = new System.Drawing.Size(205, 21);
			this.txtUserName.TabIndex = 1;
			// 
			// txtSN
			// 
			this.txtSN.Location = new System.Drawing.Point(12, 226);
			this.txtSN.Multiline = true;
			this.txtSN.Name = "txtSN";
			this.txtSN.Size = new System.Drawing.Size(493, 83);
			this.txtSN.TabIndex = 4;
			// 
			// btnCreate
			// 
			this.btnCreate.Location = new System.Drawing.Point(268, 197);
			this.btnCreate.Name = "btnCreate";
			this.btnCreate.Size = new System.Drawing.Size(75, 23);
			this.btnCreate.TabIndex = 2;
			this.btnCreate.Text = "生成";
			this.btnCreate.UseVisualStyleBackColor = true;
			this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
			// 
			// btnCopy
			// 
			this.btnCopy.Location = new System.Drawing.Point(349, 197);
			this.btnCopy.Name = "btnCopy";
			this.btnCopy.Size = new System.Drawing.Size(75, 23);
			this.btnCopy.TabIndex = 3;
			this.btnCopy.Text = "复制";
			this.btnCopy.UseVisualStyleBackColor = true;
			this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
			// 
			// btnExit
			// 
			this.btnExit.Location = new System.Drawing.Point(430, 197);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(75, 23);
			this.btnExit.TabIndex = 5;
			this.btnExit.Text = "退出";
			this.btnExit.UseVisualStyleBackColor = true;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// txtRsaKey
			// 
			this.txtRsaKey.Location = new System.Drawing.Point(59, 6);
			this.txtRsaKey.Multiline = true;
			this.txtRsaKey.Name = "txtRsaKey";
			this.txtRsaKey.Size = new System.Drawing.Size(448, 185);
			this.txtRsaKey.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(24, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(29, 12);
			this.label2.TabIndex = 9;
			this.label2.Text = "密钥";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(522, 319);
			this.ControlBox = false;
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtRsaKey);
			this.Controls.Add(this.btnExit);
			this.Controls.Add(this.btnCopy);
			this.Controls.Add(this.btnCreate);
			this.Controls.Add(this.txtSN);
			this.Controls.Add(this.txtUserName);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "注册机";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtUserName;
		private System.Windows.Forms.TextBox txtSN;
		private System.Windows.Forms.Button btnCreate;
		private System.Windows.Forms.Button btnCopy;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.TextBox txtRsaKey;
		private System.Windows.Forms.Label label2;
	}
}