namespace ApqDBManager
{
	partial class Form1
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
			this.components = new System.ComponentModel.Container();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.multiColumnComboBox1 = new MultiColumnCombo.InheritedCombo.MultiColumnComboBox(this.components);
			this.刷新RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.保存SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.全选AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.反选VToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.全部展开DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.数据库连接管理CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.tsslOutInfo = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsslTest = new System.Windows.Forms.ToolStripStatusLabel();
			this.menuStrip1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.刷新RToolStripMenuItem,
            this.保存SToolStripMenuItem,
            this.全选AToolStripMenuItem,
            this.反选VToolStripMenuItem,
            this.全部展开DToolStripMenuItem,
            this.数据库连接管理CToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(763, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// multiColumnComboBox1
			// 
			this.multiColumnComboBox1.FormattingEnabled = true;
			this.multiColumnComboBox1.Location = new System.Drawing.Point(12, 37);
			this.multiColumnComboBox1.Name = "multiColumnComboBox1";
			this.multiColumnComboBox1.Size = new System.Drawing.Size(121, 20);
			this.multiColumnComboBox1.TabIndex = 2;
			// 
			// 刷新RToolStripMenuItem
			// 
			this.刷新RToolStripMenuItem.Name = "刷新RToolStripMenuItem";
			this.刷新RToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
			this.刷新RToolStripMenuItem.Text = "刷新(&R)";
			// 
			// 保存SToolStripMenuItem
			// 
			this.保存SToolStripMenuItem.Name = "保存SToolStripMenuItem";
			this.保存SToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
			this.保存SToolStripMenuItem.Text = "保存(&S)";
			// 
			// 全选AToolStripMenuItem
			// 
			this.全选AToolStripMenuItem.Name = "全选AToolStripMenuItem";
			this.全选AToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
			this.全选AToolStripMenuItem.Text = "全选(&A)";
			// 
			// 反选VToolStripMenuItem
			// 
			this.反选VToolStripMenuItem.Name = "反选VToolStripMenuItem";
			this.反选VToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
			this.反选VToolStripMenuItem.Text = "反选(&V)";
			// 
			// 全部展开DToolStripMenuItem
			// 
			this.全部展开DToolStripMenuItem.Name = "全部展开DToolStripMenuItem";
			this.全部展开DToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
			this.全部展开DToolStripMenuItem.Text = "全部展开(&D)";
			// 
			// 数据库连接管理CToolStripMenuItem
			// 
			this.数据库连接管理CToolStripMenuItem.Name = "数据库连接管理CToolStripMenuItem";
			this.数据库连接管理CToolStripMenuItem.Size = new System.Drawing.Size(119, 20);
			this.数据库连接管理CToolStripMenuItem.Text = "数据库连接管理(&C)";
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1});
			this.toolStrip1.Location = new System.Drawing.Point(0, 24);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(763, 25);
			this.toolStrip1.TabIndex = 3;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(47, 22);
			this.toolStripLabel1.Text = "刷新(&F)";
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslOutInfo,
            this.tsslTest});
			this.statusStrip1.Location = new System.Drawing.Point(0, 440);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(763, 22);
			this.statusStrip1.TabIndex = 4;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// tsslOutInfo
			// 
			this.tsslOutInfo.AutoSize = false;
			this.tsslOutInfo.Name = "tsslOutInfo";
			this.tsslOutInfo.Size = new System.Drawing.Size(300, 17);
			this.tsslOutInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tsslTest
			// 
			this.tsslTest.Name = "tsslTest";
			this.tsslTest.Size = new System.Drawing.Size(0, 17);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(763, 462);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.multiColumnComboBox1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Form1";
			this.Text = "Form1";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private MultiColumnCombo.InheritedCombo.MultiColumnComboBox multiColumnComboBox1;
		private System.Windows.Forms.ToolStripMenuItem 刷新RToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 保存SToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 全选AToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 反选VToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 全部展开DToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 数据库连接管理CToolStripMenuItem;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel tsslOutInfo;
		private System.Windows.Forms.ToolStripStatusLabel tsslTest;

	}
}