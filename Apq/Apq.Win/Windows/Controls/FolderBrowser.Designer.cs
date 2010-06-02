namespace Apq.Windows.Controls
{
    partial class FolderBrowser
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
			this.FolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.txtPath = new System.Windows.Forms.TextBox();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtPath
			// 
			this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.txtPath.Location = new System.Drawing.Point(0, 4);
			this.txtPath.Name = "txtPath";
			this.txtPath.Size = new System.Drawing.Size(221, 21);
			this.txtPath.TabIndex = 100;
			this.txtPath.TextChanged += new System.EventHandler(this.txtPath_TextChanged);
			// 
			// btnBrowse
			// 
			this.btnBrowse.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.btnBrowse.Location = new System.Drawing.Point(227, 3);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(75, 23);
			this.btnBrowse.TabIndex = 101;
			this.btnBrowse.Text = "浏览";
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new System.EventHandler(this.btnPath_Click);
			// 
			// FolderBrowser
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.txtPath);
			this.Controls.Add(this.btnBrowse);
			this.Name = "FolderBrowser";
			this.Size = new System.Drawing.Size(305, 29);
			this.Load += new System.EventHandler(this.FolderBrowser_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		/// <summary>
		/// 
		/// </summary>
		public System.Windows.Forms.TextBox txtPath;
		/// <summary>
		/// 
		/// </summary>
		public System.Windows.Forms.Button btnBrowse;
		/// <summary>
		/// 
		/// </summary>
		public System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog;


    }
}
