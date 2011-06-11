namespace ApqDBManager.Forms
{
	partial class Favorites
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
			this.listBoxControl1 = new DevExpress.XtraEditors.ListBoxControl();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.tsmiRefresh = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.listBoxControl1)).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// listBoxControl1
			// 
			this.listBoxControl1.DisplayMember = "FileName";
			this.listBoxControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBoxControl1.Location = new System.Drawing.Point(0, 24);
			this.listBoxControl1.Name = "listBoxControl1";
			this.listBoxControl1.Size = new System.Drawing.Size(292, 392);
			this.listBoxControl1.TabIndex = 0;
			this.listBoxControl1.ValueMember = "FullName";
			this.listBoxControl1.DoubleClick += new System.EventHandler(this.listBoxControl1_DoubleClick);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRefresh});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(292, 24);
			this.menuStrip1.TabIndex = 4;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// tsmiRefresh
			// 
			this.tsmiRefresh.Name = "tsmiRefresh";
			this.tsmiRefresh.Size = new System.Drawing.Size(59, 20);
			this.tsmiRefresh.Text = "刷新(&R)";
			this.tsmiRefresh.Click += new System.EventHandler(this.tsmiRefresh_Click);
			// 
			// Favorites
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 416);
			this.Controls.Add(this.listBoxControl1);
			this.Controls.Add(this.menuStrip1);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft)
						| WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)
						| WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop)
						| WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Favorites";
			this.TabText = "收藏夹";
			this.Text = "收藏夹";
			this.Load += new System.EventHandler(this.Favorites_Load);
			((System.ComponentModel.ISupportInitialize)(this.listBoxControl1)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.ListBoxControl listBoxControl1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem tsmiRefresh;

	}
}