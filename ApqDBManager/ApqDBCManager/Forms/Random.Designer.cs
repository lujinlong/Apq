namespace ApqDBCManager
{
	partial class Random
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Random));
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.lstStrings = new System.Windows.Forms.CheckedListBox();
			this.txtChars = new System.Windows.Forms.TextBox();
			this.txtCount = new System.Windows.Forms.TextBox();
			this.txtLength = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnCopy = new System.Windows.Forms.Button();
			this.btnGUID = new System.Windows.Forms.Button();
			this.btnGo = new System.Windows.Forms.Button();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.lstStrings);
			this.splitContainer1.Panel1.Controls.Add(this.txtChars);
			this.splitContainer1.Panel1.Controls.Add(this.txtCount);
			this.splitContainer1.Panel1.Controls.Add(this.txtLength);
			this.splitContainer1.Panel1.Controls.Add(this.label3);
			this.splitContainer1.Panel1.Controls.Add(this.label2);
			this.splitContainer1.Panel1.Controls.Add(this.label1);
			this.splitContainer1.Panel1.Controls.Add(this.btnCopy);
			this.splitContainer1.Panel1.Controls.Add(this.btnGUID);
			this.splitContainer1.Panel1.Controls.Add(this.btnGo);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.listView1);
			this.splitContainer1.Size = new System.Drawing.Size(600, 423);
			this.splitContainer1.SplitterDistance = 119;
			this.splitContainer1.TabIndex = 0;
			// 
			// lstStrings
			// 
			this.lstStrings.FormattingEnabled = true;
			this.lstStrings.Location = new System.Drawing.Point(217, 42);
			this.lstStrings.Name = "lstStrings";
			this.lstStrings.Size = new System.Drawing.Size(240, 68);
			this.lstStrings.TabIndex = 37;
			this.lstStrings.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lstStrings_ItemCheck);
			// 
			// txtChars
			// 
			this.txtChars.Location = new System.Drawing.Point(217, 14);
			this.txtChars.Name = "txtChars";
			this.txtChars.Size = new System.Drawing.Size(371, 21);
			this.txtChars.TabIndex = 36;
			// 
			// txtCount
			// 
			this.txtCount.Location = new System.Drawing.Point(52, 53);
			this.txtCount.Name = "txtCount";
			this.txtCount.Size = new System.Drawing.Size(100, 21);
			this.txtCount.TabIndex = 35;
			this.txtCount.Text = "10";
			this.txtCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txtLength
			// 
			this.txtLength.Location = new System.Drawing.Point(52, 14);
			this.txtLength.Name = "txtLength";
			this.txtLength.Size = new System.Drawing.Size(100, 21);
			this.txtLength.TabIndex = 34;
			this.txtLength.Text = "16";
			this.txtLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(172, 17);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(29, 12);
			this.label3.TabIndex = 33;
			this.label3.Text = "范围";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(29, 12);
			this.label2.TabIndex = 32;
			this.label2.Text = "个数";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(29, 12);
			this.label1.TabIndex = 31;
			this.label1.Text = "长度";
			// 
			// btnCopy
			// 
			this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCopy.Location = new System.Drawing.Point(485, 87);
			this.btnCopy.Name = "btnCopy";
			this.btnCopy.Size = new System.Drawing.Size(75, 23);
			this.btnCopy.TabIndex = 29;
			this.btnCopy.Text = "复制";
			this.btnCopy.UseVisualStyleBackColor = true;
			this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
			// 
			// btnGUID
			// 
			this.btnGUID.Location = new System.Drawing.Point(12, 87);
			this.btnGUID.Name = "btnGUID";
			this.btnGUID.Size = new System.Drawing.Size(75, 23);
			this.btnGUID.TabIndex = 30;
			this.btnGUID.Text = "GUID";
			this.btnGUID.UseVisualStyleBackColor = true;
			this.btnGUID.Click += new System.EventHandler(this.btnGUID_Click);
			// 
			// btnGo
			// 
			this.btnGo.Location = new System.Drawing.Point(110, 87);
			this.btnGo.Name = "btnGo";
			this.btnGo.Size = new System.Drawing.Size(75, 23);
			this.btnGo.TabIndex = 21;
			this.btnGo.Text = "生成";
			this.btnGo.UseVisualStyleBackColor = true;
			this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(0, 0);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(600, 300);
			this.listView1.TabIndex = 17;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "结果";
			this.columnHeader1.Width = 500;
			// 
			// Random
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.ClientSize = new System.Drawing.Size(600, 423);
			this.Controls.Add(this.splitContainer1);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Random";
			this.TabText = "随机串生成器";
			this.Text = "随机串生成器";
			this.Load += new System.EventHandler(this.Random_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.Button btnCopy;
		private System.Windows.Forms.Button btnGUID;
		private System.Windows.Forms.Button btnGo;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.TextBox txtCount;
		private System.Windows.Forms.TextBox txtLength;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckedListBox lstStrings;
		private System.Windows.Forms.TextBox txtChars;


	}
}