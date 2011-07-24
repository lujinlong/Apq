using Apq_DBTools.Forms;
namespace Apq_DBTools
{
	partial class SqlGen
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SqlGen));
			this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.tspb = new System.Windows.Forms.ToolStripProgressBar();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.imgList = new System.Windows.Forms.ImageList(this.components);
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsbConnectDB = new System.Windows.Forms.ToolStripSplitButton();
			this.tsmiRefresh = new System.Windows.Forms.ToolStripMenuItem();
			this.tssbGenSql = new System.Windows.Forms.ToolStripSplitButton();
			this.tsmiMeta = new System.Windows.Forms.ToolStripMenuItem();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.statusStrip1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// tsslStatus
			// 
			this.tsslStatus.Name = "tsslStatus";
			this.tsslStatus.Size = new System.Drawing.Size(29, 17);
			this.tsslStatus.Text = "状态";
			// 
			// tspb
			// 
			this.tspb.Name = "tspb";
			this.tspb.Size = new System.Drawing.Size(300, 16);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslStatus,
            this.tspb});
			this.statusStrip1.Location = new System.Drawing.Point(0, 413);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(760, 22);
			this.statusStrip1.TabIndex = 1;
			// 
			// imgList
			// 
			this.imgList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imgList.ImageSize = new System.Drawing.Size(16, 16);
			this.imgList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbConnectDB,
            this.tssbGenSql});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(760, 25);
			this.toolStrip1.TabIndex = 4;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tsbConnectDB
			// 
			this.tsbConnectDB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbConnectDB.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRefresh});
			this.tsbConnectDB.Image = ((System.Drawing.Image)(resources.GetObject("tsbConnectDB.Image")));
			this.tsbConnectDB.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbConnectDB.Name = "tsbConnectDB";
			this.tsbConnectDB.Size = new System.Drawing.Size(45, 22);
			this.tsbConnectDB.Text = "连接";
			this.tsbConnectDB.ButtonClick += new System.EventHandler(this.tsbConnectDB_ButtonClick);
			// 
			// tsmiRefresh
			// 
			this.tsmiRefresh.Name = "tsmiRefresh";
			this.tsmiRefresh.Size = new System.Drawing.Size(112, 22);
			this.tsmiRefresh.Text = "刷新(&F)";
			this.tsmiRefresh.Click += new System.EventHandler(this.tsbRefresh_Click);
			// 
			// tssbGenSql
			// 
			this.tssbGenSql.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tssbGenSql.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMeta});
			this.tssbGenSql.Image = ((System.Drawing.Image)(resources.GetObject("tssbGenSql.Image")));
			this.tssbGenSql.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tssbGenSql.Name = "tssbGenSql";
			this.tssbGenSql.Size = new System.Drawing.Size(45, 22);
			this.tssbGenSql.Text = "生成";
			// 
			// tsmiMeta
			// 
			this.tsmiMeta.Name = "tsmiMeta";
			this.tsmiMeta.Size = new System.Drawing.Size(152, 22);
			this.tsmiMeta.Text = "元数据脚本(&M)";
			this.tsmiMeta.Click += new System.EventHandler(this.tsmiMeta_Click);
			// 
			// dataGridView1
			// 
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView1.Location = new System.Drawing.Point(0, 25);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowTemplate.Height = 23;
			this.dataGridView1.Size = new System.Drawing.Size(760, 388);
			this.dataGridView1.TabIndex = 5;
			// 
			// SqlGen
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(760, 435);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.toolStrip1);
			this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(768, 462);
			this.Name = "SqlGen";
			this.TabText = "文本文件编码转换";
			this.Text = "脚本生成";
			this.Activated += new System.EventHandler(this.SqlGen_Activated);
			this.Deactivate += new System.EventHandler(this.SqlGen_Deactivate);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SqlGen_FormClosing);
			this.Load += new System.EventHandler(this.SqlGen_Load);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel tsslStatus;
        private System.Windows.Forms.ToolStripProgressBar tspb;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSplitButton tsbConnectDB;
        private System.Windows.Forms.ToolStripMenuItem tsmiRefresh;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripSplitButton tssbGenSql;
        private System.Windows.Forms.ToolStripMenuItem tsmiMeta;
	}
}