namespace ApqDBManager.Controls.MainOption
{
	partial class Favorites
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
			this.beCFolder = new DevExpress.XtraEditors.ButtonEdit();
			this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
			this.fbdFavorites = new System.Windows.Forms.FolderBrowserDialog();
			((System.ComponentModel.ISupportInitialize)(this.beCFolder.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// beCFolder
			// 
			this.beCFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.beCFolder.Location = new System.Drawing.Point(81, 91);
			this.beCFolder.Name = "beCFolder";
			this.beCFolder.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.beCFolder.Size = new System.Drawing.Size(396, 21);
			this.beCFolder.TabIndex = 12;
			this.beCFolder.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.beCFolder_ButtonClick);
			this.beCFolder.EditValueChanged += new System.EventHandler(this.beCFolder_EditValueChanged);
			// 
			// labelControl3
			// 
			this.labelControl3.Location = new System.Drawing.Point(15, 94);
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Size = new System.Drawing.Size(60, 14);
			this.labelControl3.TabIndex = 11;
			this.labelControl3.Text = "收藏夹目录";
			// 
			// Favorites
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.beCFolder);
			this.Controls.Add(this.labelControl3);
			this.Name = "Favorites";
			this.Size = new System.Drawing.Size(480, 380);
			((System.ComponentModel.ISupportInitialize)(this.beCFolder.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.ButtonEdit beCFolder;
		private DevExpress.XtraEditors.LabelControl labelControl3;
		private System.Windows.Forms.FolderBrowserDialog fbdFavorites;
	}
}
