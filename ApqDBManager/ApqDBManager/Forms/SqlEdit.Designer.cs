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
			WeifenLuo.WinFormsUI.Docking.DockPanelSkin dockPanelSkin2 = new WeifenLuo.WinFormsUI.Docking.DockPanelSkin();
			WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin autoHideStripSkin2 = new WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin();
			WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient4 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
			WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient8 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
			WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin dockPaneStripSkin2 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin();
			WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient dockPaneStripGradient2 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient();
			WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient9 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
			WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient5 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
			WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient10 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
			WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient dockPaneStripToolWindowGradient2 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient();
			WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient11 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
			WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient12 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
			WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient6 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
			WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient13 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
			WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient14 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsbOpen = new System.Windows.Forms.ToolStripButton();
			this.tsbSave = new System.Windows.Forms.ToolStripButton();
			this.tsbSaveAs = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbDBI = new System.Windows.Forms.ToolStripButton();
			this.tsbOut = new System.Windows.Forms.ToolStripButton();
			this.tsbErrList = new System.Windows.Forms.ToolStripButton();
			this.actionList1 = new Crad.Windows.Forms.Actions.ActionList();
			this.acOpen = new Crad.Windows.Forms.Actions.Action();
			this.acSave = new Crad.Windows.Forms.Actions.Action();
			this.acSaveAs = new Crad.Windows.Forms.Actions.Action();
			this.acDBI = new Crad.Windows.Forms.Actions.Action();
			this.acOut = new Crad.Windows.Forms.Actions.Action();
			this.acErrList = new Crad.Windows.Forms.Actions.Action();
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
			// acDBI
			// 
			this.acDBI.Checked = true;
			this.acDBI.CheckState = System.Windows.Forms.CheckState.Checked;
			this.acDBI.Text = "数据库列表(&L)";
			// 
			// acOut
			// 
			this.acOut.Checked = true;
			this.acOut.CheckState = System.Windows.Forms.CheckState.Checked;
			this.acOut.Text = "输出(&O)";
			// 
			// acErrList
			// 
			this.acErrList.Checked = true;
			this.acErrList.CheckState = System.Windows.Forms.CheckState.Checked;
			this.acErrList.Text = "错误列表(&E)";
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
			dockPanelGradient4.EndColor = System.Drawing.SystemColors.ControlLight;
			dockPanelGradient4.StartColor = System.Drawing.SystemColors.ControlLight;
			autoHideStripSkin2.DockStripGradient = dockPanelGradient4;
			tabGradient8.EndColor = System.Drawing.SystemColors.Control;
			tabGradient8.StartColor = System.Drawing.SystemColors.Control;
			tabGradient8.TextColor = System.Drawing.SystemColors.ControlDarkDark;
			autoHideStripSkin2.TabGradient = tabGradient8;
			autoHideStripSkin2.TextFont = new System.Drawing.Font("宋体", 9F);
			dockPanelSkin2.AutoHideStripSkin = autoHideStripSkin2;
			tabGradient9.EndColor = System.Drawing.SystemColors.ControlLightLight;
			tabGradient9.StartColor = System.Drawing.SystemColors.ControlLightLight;
			tabGradient9.TextColor = System.Drawing.SystemColors.ControlText;
			dockPaneStripGradient2.ActiveTabGradient = tabGradient9;
			dockPanelGradient5.EndColor = System.Drawing.SystemColors.Control;
			dockPanelGradient5.StartColor = System.Drawing.SystemColors.Control;
			dockPaneStripGradient2.DockStripGradient = dockPanelGradient5;
			tabGradient10.EndColor = System.Drawing.SystemColors.ControlLight;
			tabGradient10.StartColor = System.Drawing.SystemColors.ControlLight;
			tabGradient10.TextColor = System.Drawing.SystemColors.ControlText;
			dockPaneStripGradient2.InactiveTabGradient = tabGradient10;
			dockPaneStripSkin2.DocumentGradient = dockPaneStripGradient2;
			dockPaneStripSkin2.TextFont = new System.Drawing.Font("宋体", 9F);
			tabGradient11.EndColor = System.Drawing.SystemColors.ActiveCaption;
			tabGradient11.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			tabGradient11.StartColor = System.Drawing.SystemColors.GradientActiveCaption;
			tabGradient11.TextColor = System.Drawing.SystemColors.ActiveCaptionText;
			dockPaneStripToolWindowGradient2.ActiveCaptionGradient = tabGradient11;
			tabGradient12.EndColor = System.Drawing.SystemColors.Control;
			tabGradient12.StartColor = System.Drawing.SystemColors.Control;
			tabGradient12.TextColor = System.Drawing.SystemColors.ControlText;
			dockPaneStripToolWindowGradient2.ActiveTabGradient = tabGradient12;
			dockPanelGradient6.EndColor = System.Drawing.SystemColors.ControlLight;
			dockPanelGradient6.StartColor = System.Drawing.SystemColors.ControlLight;
			dockPaneStripToolWindowGradient2.DockStripGradient = dockPanelGradient6;
			tabGradient13.EndColor = System.Drawing.SystemColors.InactiveCaption;
			tabGradient13.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			tabGradient13.StartColor = System.Drawing.SystemColors.GradientInactiveCaption;
			tabGradient13.TextColor = System.Drawing.SystemColors.InactiveCaptionText;
			dockPaneStripToolWindowGradient2.InactiveCaptionGradient = tabGradient13;
			tabGradient14.EndColor = System.Drawing.Color.Transparent;
			tabGradient14.StartColor = System.Drawing.Color.Transparent;
			tabGradient14.TextColor = System.Drawing.SystemColors.ControlDarkDark;
			dockPaneStripToolWindowGradient2.InactiveTabGradient = tabGradient14;
			dockPaneStripSkin2.ToolWindowGradient = dockPaneStripToolWindowGradient2;
			dockPanelSkin2.DockPaneStripSkin = dockPaneStripSkin2;
			this.dockPanel1.Skin = dockPanelSkin2;
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