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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.multiColumnComboBox1 = new MultiColumnCombo.InheritedCombo.MultiColumnComboBox(this.components);
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.tsslOutInfo = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsslTest = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
			this.tsbSaveToDB = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbSelectAll = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tstbStr = new System.Windows.Forms.ToolStripTextBox();
			this.tssbSlts = new System.Windows.Forms.ToolStripSplitButton();
			this.tsmiSltsUserId = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiSltsPwdD = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tssbCreateCSFile = new System.Windows.Forms.ToolStripSplitButton();
			this.statusStrip1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// multiColumnComboBox1
			// 
			this.multiColumnComboBox1.FormattingEnabled = true;
			this.multiColumnComboBox1.Location = new System.Drawing.Point(12, 37);
			this.multiColumnComboBox1.Name = "multiColumnComboBox1";
			this.multiColumnComboBox1.Size = new System.Drawing.Size(121, 20);
			this.multiColumnComboBox1.TabIndex = 2;
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
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbRefresh,
            this.tsbSaveToDB,
            this.toolStripSeparator1,
            this.tsbSelectAll,
            this.toolStripSeparator2,
            this.tstbStr,
            this.tssbSlts,
            this.toolStripSeparator3,
            this.tssbCreateCSFile});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(763, 25);
			this.toolStrip1.TabIndex = 5;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tsbRefresh
			// 
			this.tsbRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsbRefresh.Image")));
			this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbRefresh.Name = "tsbRefresh";
			this.tsbRefresh.Size = new System.Drawing.Size(51, 22);
			this.tsbRefresh.Text = "刷新(&R)";
			// 
			// tsbSaveToDB
			// 
			this.tsbSaveToDB.Image = ((System.Drawing.Image)(resources.GetObject("tsbSaveToDB.Image")));
			this.tsbSaveToDB.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbSaveToDB.Name = "tsbSaveToDB";
			this.tsbSaveToDB.Size = new System.Drawing.Size(67, 22);
			this.tsbSaveToDB.Text = "保存(&S)";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbSelectAll
			// 
			this.tsbSelectAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("tsbSelectAll.Image")));
			this.tsbSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbSelectAll.Name = "tsbSelectAll";
			this.tsbSelectAll.Size = new System.Drawing.Size(51, 22);
			this.tsbSelectAll.Text = "全选(&A)";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// tstbStr
			// 
			this.tstbStr.Name = "tstbStr";
			this.tstbStr.Size = new System.Drawing.Size(200, 25);
			// 
			// tssbSlts
			// 
			this.tssbSlts.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tssbSlts.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSltsUserId,
            this.tsmiSltsPwdD});
			this.tssbSlts.Image = ((System.Drawing.Image)(resources.GetObject("tssbSlts.Image")));
			this.tssbSlts.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tssbSlts.Name = "tssbSlts";
			this.tssbSlts.Size = new System.Drawing.Size(87, 22);
			this.tssbSlts.Text = "批量设置(&E)";
			// 
			// tsmiSltsUserId
			// 
			this.tsmiSltsUserId.Name = "tsmiSltsUserId";
			this.tsmiSltsUserId.Size = new System.Drawing.Size(152, 22);
			this.tsmiSltsUserId.Text = "登录名";
			// 
			// tsmiSltsPwdD
			// 
			this.tsmiSltsPwdD.Name = "tsmiSltsPwdD";
			this.tsmiSltsPwdD.Size = new System.Drawing.Size(152, 22);
			this.tsmiSltsPwdD.Text = "密码";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// tssbCreateCSFile
			// 
			this.tssbCreateCSFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tssbCreateCSFile.Image = ((System.Drawing.Image)(resources.GetObject("tssbCreateCSFile.Image")));
			this.tssbCreateCSFile.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tssbCreateCSFile.Name = "tssbCreateCSFile";
			this.tssbCreateCSFile.Size = new System.Drawing.Size(87, 22);
			this.tssbCreateCSFile.Text = "生成文件(&G)";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(763, 462);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.multiColumnComboBox1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private MultiColumnCombo.InheritedCombo.MultiColumnComboBox multiColumnComboBox1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel tsslOutInfo;
		private System.Windows.Forms.ToolStripStatusLabel tsslTest;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsbRefresh;
		private System.Windows.Forms.ToolStripButton tsbSaveToDB;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton tsbSelectAll;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripTextBox tstbStr;
		private System.Windows.Forms.ToolStripSplitButton tssbSlts;
		private System.Windows.Forms.ToolStripMenuItem tsmiSltsUserId;
		private System.Windows.Forms.ToolStripMenuItem tsmiSltsPwdD;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripSplitButton tssbCreateCSFile;

	}
}