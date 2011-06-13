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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SqlEdit));
			this.bar1 = new DevExpress.XtraBars.Bar();
			this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
			this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
			this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
			this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
			this.cms2 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiShowNode = new System.Windows.Forms.ToolStripMenuItem();
			this.txtSql = new ICSharpCode.TextEditor.TextEditorControl();
			this.cms1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiUndo = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiRedo = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiCut = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiPaste = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiSelectAll = new System.Windows.Forms.ToolStripMenuItem();
			this.dsUI = new ApqDBManager.Forms.ErrList_XSD();
			this._Sqls = new ApqDBManager.Forms.SrvsMgr.SrvsMgr_XSD();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.tspb = new System.Windows.Forms.ToolStripProgressBar();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tslDBName = new System.Windows.Forms.ToolStripLabel();
			this.tscbDBName = new System.Windows.Forms.ToolStripComboBox();
			this.tsbExec = new System.Windows.Forms.ToolStripButton();
			this.tsbCancel = new System.Windows.Forms.ToolStripButton();
			this.tsbSingleThread = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tssbResult = new System.Windows.Forms.ToolStripSplitButton();
			this.tsmiResult0 = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiResult1 = new System.Windows.Forms.ToolStripMenuItem();
			this.tsbExport = new System.Windows.Forms.ToolStripButton();
			((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
			this.dockPanel1.SuspendLayout();
			this.dockPanel1_Container.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
			this.cms2.SuspendLayout();
			this.cms1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dsUI)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._Sqls)).BeginInit();
			this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bar1
			// 
			this.bar1.BarName = "Tools";
			this.bar1.DockCol = 0;
			this.bar1.DockRow = 1;
			this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.bar1.Text = "Tools";
			// 
			// dockManager1
			// 
			this.dockManager1.Form = this;
			this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel1});
			this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
			// 
			// dockPanel1
			// 
			this.dockPanel1.Controls.Add(this.dockPanel1_Container);
			this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
			this.dockPanel1.FloatVertical = true;
			this.dockPanel1.ID = new System.Guid("b0aebc7c-6cdb-4d07-8260-4c1f6f4ec724");
			this.dockPanel1.Location = new System.Drawing.Point(0, 166);
			this.dockPanel1.Name = "dockPanel1";
			this.dockPanel1.Options.AllowDockFill = false;
			this.dockPanel1.Options.AllowDockLeft = false;
			this.dockPanel1.Options.AllowDockRight = false;
			this.dockPanel1.Options.AllowDockTop = false;
			this.dockPanel1.Options.AllowFloating = false;
			this.dockPanel1.Options.ShowCloseButton = false;
			this.dockPanel1.OriginalSize = new System.Drawing.Size(200, 300);
			this.dockPanel1.Size = new System.Drawing.Size(760, 300);
			this.dockPanel1.TabText = "输出";
			this.dockPanel1.Text = "输出";
			// 
			// dockPanel1_Container
			// 
			this.dockPanel1_Container.Controls.Add(this.xtraTabControl1);
			this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
			this.dockPanel1_Container.Name = "dockPanel1_Container";
			this.dockPanel1_Container.Size = new System.Drawing.Size(754, 272);
			this.dockPanel1_Container.TabIndex = 0;
			// 
			// xtraTabControl1
			// 
			this.xtraTabControl1.ContextMenuStrip = this.cms2;
			this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
			this.xtraTabControl1.Name = "xtraTabControl1";
			this.xtraTabControl1.Size = new System.Drawing.Size(754, 272);
			this.xtraTabControl1.TabIndex = 0;
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
			this.tsmiShowNode.Click += new System.EventHandler(this.tsmiShowNode_Click);
			// 
			// txtSql
			// 
			this.txtSql.ContextMenuStrip = this.cms1;
			this.txtSql.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtSql.Encoding = ((System.Text.Encoding)(resources.GetObject("txtSql.Encoding")));
			this.txtSql.IsIconBarVisible = false;
			this.txtSql.Location = new System.Drawing.Point(0, 0);
			this.txtSql.Name = "txtSql";
			this.txtSql.ShowEOLMarkers = true;
			this.txtSql.ShowSpaces = true;
			this.txtSql.ShowTabs = true;
			this.txtSql.ShowVRuler = true;
			this.txtSql.Size = new System.Drawing.Size(760, 119);
			this.txtSql.TabIndex = 0;
			// 
			// cms1
			// 
			this.cms1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiUndo,
            this.tsmiRedo,
            this.toolStripSeparator1,
            this.tsmiCut,
            this.tsmiCopy,
            this.tsmiPaste,
            this.toolStripSeparator2,
            this.tsmiSelectAll});
			this.cms1.Name = "contextMenuStrip1";
			this.cms1.Size = new System.Drawing.Size(154, 148);
			// 
			// tsmiUndo
			// 
			this.tsmiUndo.Name = "tsmiUndo";
			this.tsmiUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
			this.tsmiUndo.Size = new System.Drawing.Size(153, 22);
			this.tsmiUndo.Text = "撤消(&U)";
			this.tsmiUndo.Click += new System.EventHandler(this.tsmiUndo_Click);
			// 
			// tsmiRedo
			// 
			this.tsmiRedo.Name = "tsmiRedo";
			this.tsmiRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
			this.tsmiRedo.Size = new System.Drawing.Size(153, 22);
			this.tsmiRedo.Text = "重做(&R)";
			this.tsmiRedo.Click += new System.EventHandler(this.tsmiRedo_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(150, 6);
			// 
			// tsmiCut
			// 
			this.tsmiCut.Name = "tsmiCut";
			this.tsmiCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.tsmiCut.Size = new System.Drawing.Size(153, 22);
			this.tsmiCut.Text = "剪切(&T)";
			this.tsmiCut.Click += new System.EventHandler(this.tsmiCut_Click);
			// 
			// tsmiCopy
			// 
			this.tsmiCopy.Name = "tsmiCopy";
			this.tsmiCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.tsmiCopy.Size = new System.Drawing.Size(153, 22);
			this.tsmiCopy.Text = "复制(&C)";
			this.tsmiCopy.Click += new System.EventHandler(this.tsmiCopy_Click);
			// 
			// tsmiPaste
			// 
			this.tsmiPaste.Name = "tsmiPaste";
			this.tsmiPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.tsmiPaste.Size = new System.Drawing.Size(153, 22);
			this.tsmiPaste.Text = "粘贴(&P)";
			this.tsmiPaste.Click += new System.EventHandler(this.tsmiPaste_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(150, 6);
			// 
			// tsmiSelectAll
			// 
			this.tsmiSelectAll.Name = "tsmiSelectAll";
			this.tsmiSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
			this.tsmiSelectAll.Size = new System.Drawing.Size(153, 22);
			this.tsmiSelectAll.Text = "全选(&A)";
			this.tsmiSelectAll.Click += new System.EventHandler(this.tsmiSelectAll_Click);
			// 
			// dsUI
			// 
			this.dsUI.DataSetName = "UI";
			this.dsUI.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// _Sqls
			// 
			this._Sqls.DataSetName = "Sqls";
			this._Sqls.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.BottomToolStripPanel
			// 
			this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this.txtSql);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(760, 119);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.LeftToolStripPanelVisible = false;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 25);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.RightToolStripPanelVisible = false;
			this.toolStripContainer1.Size = new System.Drawing.Size(760, 141);
			this.toolStripContainer1.TabIndex = 5;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// statusStrip1
			// 
			this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslStatus,
            this.tspb});
			this.statusStrip1.Location = new System.Drawing.Point(0, 0);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(760, 22);
			this.statusStrip1.TabIndex = 8;
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
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslDBName,
            this.tscbDBName,
            this.tsbExec,
            this.tsbCancel,
            this.tsbSingleThread,
            this.toolStripSeparator3,
            this.tssbResult,
            this.tsbExport});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(760, 25);
			this.toolStrip1.TabIndex = 7;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tslDBName
			// 
			this.tslDBName.Name = "tslDBName";
			this.tslDBName.Size = new System.Drawing.Size(53, 22);
			this.tslDBName.Text = "数据库名";
			// 
			// tscbDBName
			// 
			this.tscbDBName.Name = "tscbDBName";
			this.tscbDBName.Size = new System.Drawing.Size(121, 25);
			this.tscbDBName.Leave += new System.EventHandler(this.tscbDBName_Leave);
			// 
			// tsbExec
			// 
			this.tsbExec.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbExec.Image = ((System.Drawing.Image)(resources.GetObject("tsbExec.Image")));
			this.tsbExec.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbExec.Name = "tsbExec";
			this.tsbExec.Size = new System.Drawing.Size(51, 22);
			this.tsbExec.Text = "执行(&X)";
			this.tsbExec.Click += new System.EventHandler(this.tsbExec_Click);
			// 
			// tsbCancel
			// 
			this.tsbCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbCancel.Image = ((System.Drawing.Image)(resources.GetObject("tsbCancel.Image")));
			this.tsbCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbCancel.Name = "tsbCancel";
			this.tsbCancel.Size = new System.Drawing.Size(51, 22);
			this.tsbCancel.Text = "取消(&C)";
			this.tsbCancel.Click += new System.EventHandler(this.tsbCancel_Click);
			// 
			// tsbSingleThread
			// 
			this.tsbSingleThread.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbSingleThread.Image = ((System.Drawing.Image)(resources.GetObject("tsbSingleThread.Image")));
			this.tsbSingleThread.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbSingleThread.Name = "tsbSingleThread";
			this.tsbSingleThread.Size = new System.Drawing.Size(45, 22);
			this.tsbSingleThread.Text = "单线程";
			this.tsbSingleThread.Click += new System.EventHandler(this.tsbSingleThread_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// tssbResult
			// 
			this.tssbResult.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tssbResult.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiResult0,
            this.tsmiResult1});
			this.tssbResult.Image = ((System.Drawing.Image)(resources.GetObject("tssbResult.Image")));
			this.tssbResult.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tssbResult.Name = "tssbResult";
			this.tssbResult.Size = new System.Drawing.Size(93, 22);
			this.tssbResult.Text = "结果显示方式";
			this.tssbResult.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tssbResult_DropDownItemClicked);
			// 
			// tsmiResult0
			// 
			this.tsmiResult0.Checked = true;
			this.tsmiResult0.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsmiResult0.Name = "tsmiResult0";
			this.tsmiResult0.Size = new System.Drawing.Size(94, 22);
			this.tsmiResult0.Text = "分列";
			// 
			// tsmiResult1
			// 
			this.tsmiResult1.Name = "tsmiResult1";
			this.tsmiResult1.Size = new System.Drawing.Size(94, 22);
			this.tsmiResult1.Text = "合并";
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
			// SqlEdit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(760, 466);
			this.Controls.Add(this.toolStripContainer1);
			this.Controls.Add(this.dockPanel1);
			this.Controls.Add(this.toolStrip1);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "SqlEdit";
			this.TabText = "SqlEdit";
			this.Text = "SqlEdit";
			this.Deactivate += new System.EventHandler(this.SqlEdit_Deactivate);
			this.Load += new System.EventHandler(this.SqlEdit_Load);
			this.Activated += new System.EventHandler(this.SqlEdit_Activated);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SqlEdit_FormClosed);
			((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
			this.dockPanel1.ResumeLayout(false);
			this.dockPanel1_Container.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
			this.cms2.ResumeLayout(false);
			this.cms1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dsUI)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._Sqls)).EndInit();
			this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraBars.Bar bar1;
		private DevExpress.XtraBars.Docking.DockManager dockManager1;
		private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
		private ICSharpCode.TextEditor.TextEditorControl txtSql;
		private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
		private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
		public ErrList_XSD dsUI;
		public ApqDBManager.Forms.SrvsMgr.SrvsMgr_XSD _Sqls;
		private System.Windows.Forms.ContextMenuStrip cms1;
		private System.Windows.Forms.ToolStripMenuItem tsmiUndo;
		private System.Windows.Forms.ToolStripMenuItem tsmiRedo;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem tsmiCut;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopy;
		private System.Windows.Forms.ToolStripMenuItem tsmiPaste;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem tsmiSelectAll;
		private System.Windows.Forms.ContextMenuStrip cms2;
		private System.Windows.Forms.ToolStripMenuItem tsmiShowNode;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel tsslStatus;
		private System.Windows.Forms.ToolStripProgressBar tspb;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripLabel tslDBName;
		private System.Windows.Forms.ToolStripComboBox tscbDBName;
		private System.Windows.Forms.ToolStripButton tsbExec;
		private System.Windows.Forms.ToolStripButton tsbCancel;
		private System.Windows.Forms.ToolStripButton tsbSingleThread;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripSplitButton tssbResult;
		private System.Windows.Forms.ToolStripMenuItem tsmiResult0;
		private System.Windows.Forms.ToolStripMenuItem tsmiResult1;
		private System.Windows.Forms.ToolStripButton tsbExport;
	}
}