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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResultTable));
			this.tc = new System.Windows.Forms.TabControl();
			this.tpRt = new System.Windows.Forms.TabPage();
			this.tpInfo = new System.Windows.Forms.TabPage();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsbExport = new System.Windows.Forms.ToolStripButton();
			this.tc.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tc
			// 
			this.tc.Controls.Add(this.tpRt);
			this.tc.Controls.Add(this.tpInfo);
			this.tc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tc.Location = new System.Drawing.Point(0, 25);
			this.tc.Name = "tc";
			this.tc.SelectedIndex = 0;
			this.tc.Size = new System.Drawing.Size(680, 393);
			this.tc.TabIndex = 0;
			// 
			// tpRt
			// 
			this.tpRt.Location = new System.Drawing.Point(4, 21);
			this.tpRt.Name = "tpRt";
			this.tpRt.Padding = new System.Windows.Forms.Padding(3);
			this.tpRt.Size = new System.Drawing.Size(672, 368);
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
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbExport});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(680, 25);
			this.toolStrip1.TabIndex = 6;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tsbExport
			// 
			this.tsbExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbExport.Image = ((System.Drawing.Image)(resources.GetObject("tsbExport.Image")));
			this.tsbExport.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbExport.Name = "tsbExport";
			this.tsbExport.Size = new System.Drawing.Size(51, 22);
			this.tsbExport.Text = "导出(&T)";
			this.tsbExport.Click += new System.EventHandler(this.tsbExport_Click);
			// 
			// ResultTable
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tc);
			this.Controls.Add(this.toolStrip1);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "ResultTable";
			this.Size = new System.Drawing.Size(680, 418);
			this.Load += new System.EventHandler(this.ResultTable_Load);
			this.tc.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TabControl tc;
		private System.Windows.Forms.TabPage tpRt;
		private System.Windows.Forms.TabPage tpInfo;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsbExport;
	}
}
