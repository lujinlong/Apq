namespace ApqDBManager.Controls
{
	partial class ResultTable
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
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.tsmiExport = new System.Windows.Forms.ToolStripMenuItem();
			this.tc = new System.Windows.Forms.TabControl();
			this.tpRt = new System.Windows.Forms.TabPage();
			this.tpInfo = new System.Windows.Forms.TabPage();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.tc.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripContainer1
			// 
			this.toolStripContainer1.BottomToolStripPanelVisible = false;
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this.tc);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(680, 394);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.LeftToolStripPanelVisible = false;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.RightToolStripPanelVisible = false;
			this.toolStripContainer1.Size = new System.Drawing.Size(680, 418);
			this.toolStripContainer1.TabIndex = 5;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiExport});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(680, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// tsmiExport
			// 
			this.tsmiExport.Name = "tsmiExport";
			this.tsmiExport.Size = new System.Drawing.Size(59, 20);
			this.tsmiExport.Text = "导出(&T)";
			this.tsmiExport.Click += new System.EventHandler(this.tsmiExport_Click);
			// 
			// tc
			// 
			this.tc.Controls.Add(this.tpRt);
			this.tc.Controls.Add(this.tpInfo);
			this.tc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tc.Location = new System.Drawing.Point(0, 0);
			this.tc.Name = "tc";
			this.tc.SelectedIndex = 0;
			this.tc.Size = new System.Drawing.Size(680, 394);
			this.tc.TabIndex = 0;
			// 
			// tpRt
			// 
			this.tpRt.Location = new System.Drawing.Point(4, 21);
			this.tpRt.Name = "tpRt";
			this.tpRt.Padding = new System.Windows.Forms.Padding(3);
			this.tpRt.Size = new System.Drawing.Size(672, 369);
			this.tpRt.TabIndex = 0;
			this.tpRt.Text = "结果";
			this.tpRt.UseVisualStyleBackColor = true;
			// 
			// tpInfo
			// 
			this.tpInfo.Location = new System.Drawing.Point(4, 21);
			this.tpInfo.Name = "tpInfo";
			this.tpInfo.Padding = new System.Windows.Forms.Padding(3);
			this.tpInfo.Size = new System.Drawing.Size(672, 369);
			this.tpInfo.TabIndex = 1;
			this.tpInfo.Text = "消息";
			this.tpInfo.UseVisualStyleBackColor = true;
			// 
			// ResultTable
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.toolStripContainer1);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "ResultTable";
			this.Size = new System.Drawing.Size(680, 418);
			this.Load += new System.EventHandler(this.ResultTable_Load);
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.tc.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem tsmiExport;
		private System.Windows.Forms.TabControl tc;
		private System.Windows.Forms.TabPage tpRt;
		private System.Windows.Forms.TabPage tpInfo;
	}
}
