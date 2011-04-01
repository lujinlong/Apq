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
			this.txtPwd = new DevExpress.XtraEditors.TextEdit();
			this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
			this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
			this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
			this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
			this.txtDBName = new DevExpress.XtraEditors.TextEdit();
			this.txtServerName = new DevExpress.XtraEditors.TextEdit();
			this.txtUserId = new DevExpress.XtraEditors.TextEdit();
			((System.ComponentModel.ISupportInitialize)(this.txtPwd.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtDBName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtServerName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtUserId.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// txtPwd
			// 
			this.txtPwd.EditValue = "";
			this.txtPwd.Location = new System.Drawing.Point(153, 196);
			this.txtPwd.Name = "txtPwd";
			this.txtPwd.Properties.PasswordChar = '*';
			this.txtPwd.Size = new System.Drawing.Size(285, 21);
			this.txtPwd.TabIndex = 30;
			// 
			// labelControl5
			// 
			this.labelControl5.Location = new System.Drawing.Point(88, 199);
			this.labelControl5.Name = "labelControl5";
			this.labelControl5.Size = new System.Drawing.Size(36, 14);
			this.labelControl5.TabIndex = 26;
			this.labelControl5.Text = "密   码";
			// 
			// labelControl4
			// 
			this.labelControl4.Location = new System.Drawing.Point(88, 167);
			this.labelControl4.Name = "labelControl4";
			this.labelControl4.Size = new System.Drawing.Size(36, 14);
			this.labelControl4.TabIndex = 25;
			this.labelControl4.Text = "用户名";
			// 
			// labelControl3
			// 
			this.labelControl3.Location = new System.Drawing.Point(88, 131);
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Size = new System.Drawing.Size(36, 14);
			this.labelControl3.TabIndex = 24;
			this.labelControl3.Text = "服务器";
			// 
			// labelControl2
			// 
			this.labelControl2.Location = new System.Drawing.Point(88, 96);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(36, 14);
			this.labelControl2.TabIndex = 23;
			this.labelControl2.Text = "数据库";
			// 
			// txtDBName
			// 
			this.txtDBName.EditValue = "";
			this.txtDBName.Location = new System.Drawing.Point(152, 93);
			this.txtDBName.Name = "txtDBName";
			this.txtDBName.Size = new System.Drawing.Size(285, 21);
			this.txtDBName.TabIndex = 31;
			// 
			// txtServerName
			// 
			this.txtServerName.EditValue = "";
			this.txtServerName.Location = new System.Drawing.Point(152, 128);
			this.txtServerName.Name = "txtServerName";
			this.txtServerName.Size = new System.Drawing.Size(285, 21);
			this.txtServerName.TabIndex = 32;
			// 
			// txtUserId
			// 
			this.txtUserId.EditValue = "";
			this.txtUserId.Location = new System.Drawing.Point(152, 164);
			this.txtUserId.Name = "txtUserId";
			this.txtUserId.Size = new System.Drawing.Size(285, 21);
			this.txtUserId.TabIndex = 33;
			// 
			// DBC
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.txtUserId);
			this.Controls.Add(this.txtServerName);
			this.Controls.Add(this.txtDBName);
			this.Controls.Add(this.txtPwd);
			this.Controls.Add(this.labelControl5);
			this.Controls.Add(this.labelControl4);
			this.Controls.Add(this.labelControl3);
			this.Controls.Add(this.labelControl2);
			this.Name = "DBC";
			this.Size = new System.Drawing.Size(480, 380);
			((System.ComponentModel.ISupportInitialize)(this.txtPwd.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtDBName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtServerName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtUserId.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.TextEdit txtPwd;
		private DevExpress.XtraEditors.LabelControl labelControl5;
		private DevExpress.XtraEditors.LabelControl labelControl4;
		private DevExpress.XtraEditors.LabelControl labelControl3;
		private DevExpress.XtraEditors.LabelControl labelControl2;
		private DevExpress.XtraEditors.TextEdit txtDBName;
		private DevExpress.XtraEditors.TextEdit txtServerName;
		private DevExpress.XtraEditors.TextEdit txtUserId;
	}
}
