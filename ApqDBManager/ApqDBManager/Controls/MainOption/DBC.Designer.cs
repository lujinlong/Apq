namespace ApqDBManager.Controls.MainOption
{
	partial class DBC
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

		#region 组件设计器生成的代码

		/// <summary> 
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.txtDBName = new System.Windows.Forms.TextBox();
			this.txtServerName = new System.Windows.Forms.TextBox();
			this.txtUserId = new System.Windows.Forms.TextBox();
			this.txtPwd = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// txtDBName
			// 
			this.txtDBName.Location = new System.Drawing.Point(140, 70);
			this.txtDBName.Name = "txtDBName";
			this.txtDBName.Size = new System.Drawing.Size(285, 21);
			this.txtDBName.TabIndex = 34;
			// 
			// txtServerName
			// 
			this.txtServerName.Location = new System.Drawing.Point(140, 105);
			this.txtServerName.Name = "txtServerName";
			this.txtServerName.Size = new System.Drawing.Size(285, 21);
			this.txtServerName.TabIndex = 35;
			// 
			// txtUserId
			// 
			this.txtUserId.Location = new System.Drawing.Point(140, 141);
			this.txtUserId.Name = "txtUserId";
			this.txtUserId.Size = new System.Drawing.Size(285, 21);
			this.txtUserId.TabIndex = 36;
			// 
			// txtPwd
			// 
			this.txtPwd.Location = new System.Drawing.Point(140, 173);
			this.txtPwd.Name = "txtPwd";
			this.txtPwd.Size = new System.Drawing.Size(285, 21);
			this.txtPwd.TabIndex = 37;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(74, 73);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 38;
			this.label1.Text = "数据库";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(74, 108);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(41, 12);
			this.label2.TabIndex = 39;
			this.label2.Text = "服务器";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(74, 144);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 12);
			this.label3.TabIndex = 40;
			this.label3.Text = "用户名";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(74, 176);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(47, 12);
			this.label4.TabIndex = 41;
			this.label4.Text = "密   码";
			// 
			// DBC
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtPwd);
			this.Controls.Add(this.txtUserId);
			this.Controls.Add(this.txtServerName);
			this.Controls.Add(this.txtDBName);
			this.Name = "DBC";
			this.Size = new System.Drawing.Size(480, 290);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtDBName;
		private System.Windows.Forms.TextBox txtServerName;
		private System.Windows.Forms.TextBox txtUserId;
		private System.Windows.Forms.TextBox txtPwd;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
	}
}
