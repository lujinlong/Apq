namespace Apq_MP4
{
    partial class Rename
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
			this.components = new System.ComponentModel.Container();
			this.cbChildren = new System.Windows.Forms.CheckBox();
			this.btnStart = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.gb1 = new System.Windows.Forms.GroupBox();
			this.cbFont = new System.Windows.Forms.CheckBox();
			this.txtReplace = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtInput = new System.Windows.Forms.TextBox();
			this.fb = new Apq.Windows.Controls.FolderBrowser();
			this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.panel1.SuspendLayout();
			this.gb1.SuspendLayout();
			this.SuspendLayout();
			// 
			// cbChildren
			// 
			this.cbChildren.AutoSize = true;
			this.cbChildren.Location = new System.Drawing.Point(80, 62);
			this.cbChildren.Name = "cbChildren";
			this.cbChildren.Size = new System.Drawing.Size(86, 17);
			this.cbChildren.TabIndex = 2;
			this.cbChildren.Text = "包含子目录";
			this.cbChildren.UseVisualStyleBackColor = true;
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(145, 231);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(75, 25);
			this.btnStart.TabIndex = 5;
			this.btnStart.Text = "开始";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(14, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "指定路径:";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.gb1);
			this.panel1.Controls.Add(this.btnStart);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(392, 296);
			this.panel1.TabIndex = 5;
			// 
			// gb1
			// 
			this.gb1.Controls.Add(this.cbFont);
			this.gb1.Controls.Add(this.txtReplace);
			this.gb1.Controls.Add(this.label3);
			this.gb1.Controls.Add(this.label2);
			this.gb1.Controls.Add(this.txtInput);
			this.gb1.Controls.Add(this.fb);
			this.gb1.Controls.Add(this.cbChildren);
			this.gb1.Controls.Add(this.label1);
			this.gb1.Dock = System.Windows.Forms.DockStyle.Top;
			this.gb1.Location = new System.Drawing.Point(0, 0);
			this.gb1.Name = "gb1";
			this.gb1.Size = new System.Drawing.Size(392, 191);
			this.gb1.TabIndex = 5;
			this.gb1.TabStop = false;
			this.gb1.Text = "设置";
			// 
			// cbFont
			// 
			this.cbFont.AutoSize = true;
			this.cbFont.Location = new System.Drawing.Point(80, 154);
			this.cbFont.Name = "cbFont";
			this.cbFont.Size = new System.Drawing.Size(86, 17);
			this.cbFont.TabIndex = 9;
			this.cbFont.Text = "繁体转简体";
			this.cbFont.UseVisualStyleBackColor = true;
			// 
			// txtReplace
			// 
			this.txtReplace.CausesValidation = false;
			this.txtReplace.Location = new System.Drawing.Point(80, 121);
			this.txtReplace.Name = "txtReplace";
			this.txtReplace.Size = new System.Drawing.Size(200, 20);
			this.txtReplace.TabIndex = 4;
			this.txtReplace.Text = ".avi";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(15, 126);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(46, 13);
			this.label3.TabIndex = 8;
			this.label3.Text = "替换串:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(15, 93);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(46, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "匹配串:";
			// 
			// txtInput
			// 
			this.txtInput.Location = new System.Drawing.Point(80, 89);
			this.txtInput.Name = "txtInput";
			this.txtInput.Size = new System.Drawing.Size(200, 20);
			this.txtInput.TabIndex = 3;
			this.txtInput.Text = "\\[XVID\\]\\.avi";
			// 
			// fb
			// 
			this.fb.Location = new System.Drawing.Point(79, 22);
			this.fb.Name = "fb";
			this.fb.SelectedPath = "";
			this.fb.Size = new System.Drawing.Size(307, 30);
			this.fb.TabIndex = 1;
			// 
			// Rename
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(392, 296);
			this.Controls.Add(this.panel1);
			this.MinimumSize = new System.Drawing.Size(400, 323);
			this.Name = "Rename";
			this.Text = "批量重命名";
			this.Load += new System.EventHandler(this.Rename_Load);
			this.panel1.ResumeLayout(false);
			this.gb1.ResumeLayout(false);
			this.gb1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox cbChildren;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gb1;
        private Apq.Windows.Controls.FolderBrowser fb;
		private System.Windows.Forms.TextBox txtInput;
		private System.Windows.Forms.TextBox txtReplace;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ToolTip ToolTip;
		private System.Windows.Forms.CheckBox cbFont;

    }
}