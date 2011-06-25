using ApqDBManager.Forms;
namespace ApqDBManager
{
	partial class SqlEdit
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SqlEdit));
			WeifenLuo.WinFormsUI.Docking.DockPanelSkin dockPanelSkin5 = new WeifenLuo.WinFormsUI.Docking.DockPanelSkin();
			WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin autoHideStripSkin5 = new WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin();
			WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient13 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
			WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient29 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
			WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin dockPaneStripSkin5 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin();
			WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient dockPaneStripGradient5 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient();
			WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient30 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
			WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient14 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
			WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient31 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
			WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient dockPaneStripToolWindowGradient5 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient();
			WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient32 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
			WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient33 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
			WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient15 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
			WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient34 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
			WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient35 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsbOpen = new System.Windows.Forms.ToolStripButton();
			this.tsbSave = new System.Windows.Forms.ToolStripButton();
			this.tsbSaveAs = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbDBI = new System.Windows.Forms.ToolStripButton();
			this.tsbOut = new System.Windows.Forms.ToolStripButton();
			this.tsbErrList = new System.Windows.Forms.ToolStripButton();
			this.actionList1 = new Crad.Windows.Forms.Actions.ActionList();
			this.acDBI = new Crad.Windows.Forms.Actions.Action();
			this.acOut = new Crad.Windows.Forms.Actions.Action();
			this.acErrList = new Crad.Windows.Forms.Actions.Action();
			this.acOpen = new Crad.Windows.Forms.Actions.Action();
			this.acSave = new Crad.Windows.Forms.Actions.Action();
			this.acSaveAs = new Crad.Windows.Forms.Actions.Action();
			this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
			this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.tspb = new System.Windows.Forms.ToolStripProgressBar();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.actionList1)).BeginInit();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbOpen,
            this.tsbSave,
            this.tsbSaveAs,
            this.toolStripSeparator1,
            this.tsbDBI,
            this.tsbOut,
            this.tsbErrList});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(760, 25);
			this.toolStrip1.TabIndex = 2;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tsbOpen
			// 
			this.tsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpen.Image")));
			this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbOpen.Name = "tsbOpen";
			this.tsbOpen.Size = new System.Drawing.Size(23, 22);
			this.tsbOpen.Text = "打开(&O)";
			// 
			// tsbSave
			// 
			this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
			this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbSave.Name = "tsbSave";
			this.tsbSave.Size = new System.Drawing.Size(23, 22);
			this.tsbSave.Text = "保存(&S)";
			// 
			// tsbSaveAs
			// 
			this.tsbSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("tsbSaveAs.Image")));
			this.tsbSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbSaveAs.Name = "tsbSaveAs";
			this.tsbSaveAs.Size = new System.Drawing.Size(63, 22);
			this.tsbSaveAs.Text = "另存为(&A)";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbDBI
			// 
			this.actionList1.SetAction(this.tsbDBI, this.acDBI);
			this.tsbDBI.Checked = true;
			this.tsbDBI.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsbDBI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbDBI.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbDBI.Name = "tsbDBI";
			this.tsbDBI.Size = new System.Drawing.Size(87, 22);
			this.tsbDBI.Text = "数据库列表(&L)";
			// 
			// tsbOut
			// 
			this.actionList1.SetAction(this.tsbOut, this.acOut);
			this.tsbOut.Checked = true;
			this.tsbOut.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsbOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbOut.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbOut.Name = "tsbOut";
			this.tsbOut.Size = new System.Drawing.Size(51, 22);
			this.tsbOut.Text = "输出(&O)";
			// 
			// tsbErrList
			// 
			this.actionList1.SetAction(this.tsbErrList, this.acErrList);
			this.tsbErrList.Checked = true;
			this.tsbErrList.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsbErrList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbErrList.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbErrList.Name = "tsbErrList";
			this.tsbErrList.Size = new System.Drawing.Size(75, 22);
			this.tsbErrList.Text = "错误列表(&E)";
			// 
			// actionList1
			// 
			this.actionList1.Actions.Add(this.acOpen);
			this.actionList1.Actions.Add(this.acSave);
			this.actionList1.Actions.Add(this.acSaveAs);
			this.actionList1.Actions.Add(this.acDBI);
			this.actionList1.Actions.Add(this.acOut);
			this.actionList1.Actions.Add(this.acErrList);
			this.actionList1.ContainerControl = this;
			// 
			// acDBI
			// 
			this.acDBI.Checked = true;
			this.acDBI.CheckState = System.Windows.Forms.CheckState.Checked;
			this.acDBI.Text = "数据库列表(&L)";
			this.acDBI.Execute += new System.EventHandler(this.acDBI_Execute);
			// 
			// acOut
			// 
			this.acOut.Checked = true;
			this.acOut.CheckState = System.Windows.Forms.CheckState.Checked;
			this.acOut.Text = "输出(&O)";
			this.acOut.Execute += new System.EventHandler(this.acOut_Execute);
			// 
			// acErrList
			// 
			this.acErrList.Checked = true;
			this.acErrList.CheckState = System.Windows.Forms.CheckState.Checked;
			this.acErrList.Text = "错误列表(&E)";
			this.acErrList.Execute += new System.EventHandler(this.acErrList_Execute);
			// 
			// acOpen
			// 
			this.acOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.acOpen.Text = "打开(&O)";
			// 
			// acSave
			// 
			this.acSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.acSave.Text = "保存(&S)";
			// 
			// acSaveAs
			// 
			this.acSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
						| System.Windows.Forms.Keys.S)));
			this.acSaveAs.Text = "另存为(&A)";
			// 
			// dockPanel1
			// 
			this.dockPanel1.ActiveAutoHideContent = null;
			this.dockPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dockPanel1.DockBackColor = System.Drawing.SystemColors.Control;
			this.dockPanel1.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingSdi;
			this.dockPanel1.Location = new System.Drawing.Point(0, 25);
			this.dockPanel1.Name = "dockPanel1";
			this.dockPanel1.Size = new System.Drawing.Size(760, 419);
			dockPanelGradient13.EndColor = System.Drawing.SystemColors.ControlLight;
			dockPanelGradient13.StartColor = System.Drawing.SystemColors.ControlLight;
			autoHideStripSkin5.DockStripGradient = dockPanelGradient13;
			tabGradient29.EndColor = System.Drawing.SystemColors.Control;
			tabGradient29.StartColor = System.Drawing.SystemColors.Control;
			tabGradient29.TextColor = System.Drawing.SystemColors.ControlDarkDark;
			autoHideStripSkin5.TabGradient = tabGradient29;
			autoHideStripSkin5.TextFont = new System.Drawing.Font("宋体", 9F);
			dockPanelSkin5.AutoHideStripSkin = autoHideStripSkin5;
			tabGradient30.EndColor = System.Drawing.SystemColors.ControlLightLight;
			tabGradient30.StartColor = System.Drawing.SystemColors.ControlLightLight;
			tabGradient30.TextColor = System.Drawing.SystemColors.ControlText;
			dockPaneStripGradient5.ActiveTabGradient = tabGradient30;
			dockPanelGradient14.EndColor = System.Drawing.SystemColors.Control;
			dockPanelGradient14.StartColor = System.Drawing.SystemColors.Control;
			dockPaneStripGradient5.DockStripGradient = dockPanelGradient14;
			tabGradient31.EndColor = System.Drawing.SystemColors.ControlLight;
			tabGradient31.StartColor = System.Drawing.SystemColors.ControlLight;
			tabGradient31.TextColor = System.Drawing.SystemColors.ControlText;
			dockPaneStripGradient5.InactiveTabGradient = tabGradient31;
			dockPaneStripSkin5.DocumentGradient = dockPaneStripGradient5;
			dockPaneStripSkin5.TextFont = new System.Drawing.Font("宋体", 9F);
			tabGradient32.EndColor = System.Drawing.SystemColors.ActiveCaption;
			tabGradient32.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			tabGradient32.StartColor = System.Drawing.SystemColors.GradientActiveCaption;
			tabGradient32.TextColor = System.Drawing.SystemColors.ActiveCaptionText;
			dockPaneStripToolWindowGradient5.ActiveCaptionGradient = tabGradient32;
			tabGradient33.EndColor = System.Drawing.SystemColors.Control;
			tabGradient33.StartColor = System.Drawing.SystemColors.Control;
			tabGradient33.TextColor = System.Drawing.SystemColors.ControlText;
			dockPaneStripToolWindowGradient5.ActiveTabGradient = tabGradient33;
			dockPanelGradient15.EndColor = System.Drawing.SystemColors.ControlLight;
			dockPanelGradient15.StartColor = System.Drawing.SystemColors.ControlLight;
			dockPaneStripToolWindowGradient5.DockStripGradient = dockPanelGradient15;
			tabGradient34.EndColor = System.Drawing.SystemColors.InactiveCaption;
			tabGradient34.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			tabGradient34.StartColor = System.Drawing.SystemColors.GradientInactiveCaption;
			tabGradient34.TextColor = System.Drawing.SystemColors.InactiveCaptionText;
			dockPaneStripToolWindowGradient5.InactiveCaptionGradient = tabGradient34;
			tabGradient35.EndColor = System.Drawing.Color.Transparent;
			tabGradient35.StartColor = System.Drawing.Color.Transparent;
			tabGradient35.TextColor = System.Drawing.SystemColors.ControlDarkDark;
			dockPaneStripToolWindowGradient5.InactiveTabGradient = tabGradient35;
			dockPaneStripSkin5.ToolWindowGradient = dockPaneStripToolWindowGradient5;
			dockPanelSkin5.DockPaneStripSkin = dockPaneStripSkin5;
			this.dockPanel1.Skin = dockPanelSkin5;
			this.dockPanel1.TabIndex = 1;
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
			this.statusStrip1.Location = new System.Drawing.Point(0, 444);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(760, 22);
			this.statusStrip1.TabIndex = 1;
			// 
			// SqlEdit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(760, 466);
			this.Controls.Add(this.dockPanel1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.toolStrip1);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "SqlEdit";
			this.TabText = "SqlEdit";
			this.Text = "SqlEdit";
			this.Activated += new System.EventHandler(this.SqlEdit_Activated);
			this.Deactivate += new System.EventHandler(this.SqlEdit_Deactivate);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SqlEdit_FormClosing);
			this.Load += new System.EventHandler(this.SqlEdit_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.actionList1)).EndInit();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsbOpen;
		private System.Windows.Forms.ToolStripButton tsbSave;
		private System.Windows.Forms.ToolStripButton tsbSaveAs;
		private Crad.Windows.Forms.Actions.ActionList actionList1;
		private Crad.Windows.Forms.Actions.Action acOpen;
		private Crad.Windows.Forms.Actions.Action acSave;
		private Crad.Windows.Forms.Actions.Action acSaveAs;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton tsbDBI;
		private System.Windows.Forms.ToolStripButton tsbOut;
		private System.Windows.Forms.ToolStripButton tsbErrList;
		private Crad.Windows.Forms.Actions.Action acDBI;
		private Crad.Windows.Forms.Actions.Action acOut;
		private Crad.Windows.Forms.Actions.Action acErrList;
		public WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		public System.Windows.Forms.ToolStripStatusLabel tsslStatus;
		public System.Windows.Forms.ToolStripProgressBar tspb;
	}
}