namespace ApqDBManager.Forms
{
	partial class SqlOut
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.cms2 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiShowNode = new System.Windows.Forms.ToolStripMenuItem();
			this.cms2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(787, 266);
			this.tabControl1.TabIndex = 0;
			// 
			// cms2
			// 
			this.cms2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiShowNode});
			this.cms2.Name = "contextMenuStrip2";
			this.cms2.Size = new System.Drawing.Size(161, 26);
			// 
			// tsmiShowNode
			// 
			this.tsmiShowNode.Name = "tsmiShowNode";
			this.tsmiShowNode.Size = new System.Drawing.Size(160, 22);
			this.tsmiShowNode.Text = "定位对应节点(&N)";
			// 
			// SqlOut
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(787, 266);
			this.CloseButton = false;
			this.CloseButtonVisible = false;
			this.Controls.Add(this.tabControl1);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
			this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.Name = "SqlOut";
			this.TabText = "输出";
			this.Text = "输出";
			this.Load += new System.EventHandler(this.SqlOut_Load);
			this.cms2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.ContextMenuStrip cms2;
		private System.Windows.Forms.ToolStripMenuItem tsmiShowNode;
	}
}