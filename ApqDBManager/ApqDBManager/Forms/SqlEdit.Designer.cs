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
			this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
			this.bar2 = new DevExpress.XtraBars.Bar();
			this.lblDBName = new DevExpress.XtraBars.BarLargeButtonItem();
			this.cbDBName = new DevExpress.XtraBars.BarEditItem();
			this.ricbDBName = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
			this.btnExec = new DevExpress.XtraBars.BarButtonItem();
			this.btnCancel = new DevExpress.XtraBars.BarButtonItem();
			this.bciSingleThread = new DevExpress.XtraBars.BarCheckItem();
			this.bliResult = new DevExpress.XtraBars.BarListItem();
			this.btnExport = new DevExpress.XtraBars.BarLargeButtonItem();
			this.bar3 = new DevExpress.XtraBars.Bar();
			this.bsiState = new DevExpress.XtraBars.BarStaticItem();
			this.beiProgressBar = new DevExpress.XtraBars.BarEditItem();
			this.ripb = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
			this.barAndDockingController1 = new DevExpress.XtraBars.BarAndDockingController(this.components);
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
			this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
			this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
			this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
			this.txtSql = new ICSharpCode.TextEditor.TextEditorControl();
			this.menuUndo = new DevExpress.XtraBars.BarButtonItem();
			this.menuRedo = new DevExpress.XtraBars.BarButtonItem();
			this.menuCut = new DevExpress.XtraBars.BarButtonItem();
			this.menuCopy = new DevExpress.XtraBars.BarButtonItem();
			this.menuPaste = new DevExpress.XtraBars.BarButtonItem();
			this.menuSelectAll = new DevExpress.XtraBars.BarButtonItem();
			this.menuShowNode = new DevExpress.XtraBars.BarButtonItem();
			this.repositoryItemRadioGroup1 = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
			this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
			this.popupMenu2 = new DevExpress.XtraBars.PopupMenu(this.components);
			this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
			this._Servers = new ApqDBManager.XSD.Servers();
			this._UI = new ApqDBManager.XSD.UI();
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ricbDBName)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ripb)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
			this.dockPanel1.SuspendLayout();
			this.dockPanel1_Container.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.popupMenu2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._Servers)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._UI)).BeginInit();
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
			// barManager1
			// 
			this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2,
            this.bar3});
			this.barManager1.Controller = this.barAndDockingController1;
			this.barManager1.DockControls.Add(this.barDockControlTop);
			this.barManager1.DockControls.Add(this.barDockControlBottom);
			this.barManager1.DockControls.Add(this.barDockControlLeft);
			this.barManager1.DockControls.Add(this.barDockControlRight);
			this.barManager1.DockManager = this.dockManager1;
			this.barManager1.Form = this.txtSql;
			this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.lblDBName,
            this.cbDBName,
            this.btnExec,
            this.btnExport,
            this.bciSingleThread,
            this.beiProgressBar,
            this.bsiState,
            this.btnCancel,
            this.bliResult,
            this.menuUndo,
            this.menuRedo,
            this.menuCut,
            this.menuCopy,
            this.menuPaste,
            this.menuSelectAll,
            this.menuShowNode});
			this.barManager1.MaxItemId = 28;
			this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ricbDBName,
            this.ripb,
            this.repositoryItemRadioGroup1,
            this.repositoryItemComboBox1});
			this.barManager1.StatusBar = this.bar3;
			// 
			// bar2
			// 
			this.bar2.BarName = "Tools";
			this.bar2.DockCol = 0;
			this.bar2.DockRow = 0;
			this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.lblDBName),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.cbDBName, "", false, true, true, 144),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnExec),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCancel),
            new DevExpress.XtraBars.LinkPersistInfo(this.bciSingleThread, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bliResult),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnExport, true)});
			this.bar2.Text = "Tools";
			// 
			// lblDBName
			// 
			this.lblDBName.Caption = "数据库名";
			this.lblDBName.Id = 2;
			this.lblDBName.Name = "lblDBName";
			// 
			// cbDBName
			// 
			this.cbDBName.Caption = "barEditItem1";
			this.cbDBName.Edit = this.ricbDBName;
			this.cbDBName.EditValue = "DBA_WH";
			this.cbDBName.Id = 4;
			this.cbDBName.Name = "cbDBName";
			this.cbDBName.EditValueChanged += new System.EventHandler(this.cbDBName_EditValueChanged);
			// 
			// ricbDBName
			// 
			this.ricbDBName.AutoHeight = false;
			this.ricbDBName.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.ricbDBName.Name = "ricbDBName";
			this.ricbDBName.SelectedIndexChanged += new System.EventHandler(this.ricbDBName_SelectedIndexChanged);
			// 
			// btnExec
			// 
			this.btnExec.Caption = "执行(&X)";
			this.btnExec.Id = 5;
			this.btnExec.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5);
			this.btnExec.Name = "btnExec";
			this.btnExec.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExec_ItemClick);
			// 
			// btnCancel
			// 
			this.btnCancel.Caption = "取消(&C)";
			this.btnCancel.Enabled = false;
			this.btnCancel.Id = 11;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCancel_ItemClick);
			// 
			// bciSingleThread
			// 
			this.bciSingleThread.Caption = "单线程";
			this.bciSingleThread.Id = 3;
			this.bciSingleThread.Name = "bciSingleThread";
			// 
			// bliResult
			// 
			this.bliResult.Caption = "显示结果";
			this.bliResult.Id = 18;
			this.bliResult.ItemIndex = 0;
			this.bliResult.Name = "bliResult";
			this.bliResult.ShowChecks = true;
			this.bliResult.Strings.AddRange(new object[] {
            "多页显示",
            "单页显示"});
			// 
			// btnExport
			// 
			this.btnExport.Caption = "导出(&T)";
			this.btnExport.Id = 6;
			this.btnExport.Name = "btnExport";
			this.btnExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExport_ItemClick);
			// 
			// bar3
			// 
			this.bar3.BarName = "Status bar";
			this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
			this.bar3.DockCol = 0;
			this.bar3.DockRow = 0;
			this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
			this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bsiState),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.beiProgressBar, "", false, true, true, 499)});
			this.bar3.OptionsBar.AllowQuickCustomization = false;
			this.bar3.OptionsBar.DrawDragBorder = false;
			this.bar3.OptionsBar.UseWholeRow = true;
			this.bar3.Text = "Status bar";
			// 
			// bsiState
			// 
			this.bsiState.Caption = "状态";
			this.bsiState.Id = 9;
			this.bsiState.Name = "bsiState";
			this.bsiState.TextAlignment = System.Drawing.StringAlignment.Near;
			// 
			// beiProgressBar
			// 
			this.beiProgressBar.Caption = "beiProgressBar";
			this.beiProgressBar.Edit = this.ripb;
			this.beiProgressBar.Id = 8;
			this.beiProgressBar.Name = "beiProgressBar";
			// 
			// ripb
			// 
			this.ripb.Name = "ripb";
			this.ripb.Step = 1;
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
			this.dockPanel1.ID = new System.Guid("b0aebc7c-6cdb-4d07-8260-4c1f6f4ec724");
			this.dockPanel1.Location = new System.Drawing.Point(0, 93);
			this.dockPanel1.Name = "dockPanel1";
			this.dockPanel1.Options.AllowDockFill = false;
			this.dockPanel1.Options.AllowDockLeft = false;
			this.dockPanel1.Options.AllowDockRight = false;
			this.dockPanel1.Options.AllowDockTop = false;
			this.dockPanel1.Options.AllowFloating = false;
			this.dockPanel1.Options.ShowCloseButton = false;
			this.dockPanel1.OriginalSize = new System.Drawing.Size(200, 348);
			this.dockPanel1.Size = new System.Drawing.Size(760, 348);
			this.dockPanel1.TabText = "输出";
			this.dockPanel1.Text = "输出";
			// 
			// dockPanel1_Container
			// 
			this.dockPanel1_Container.Controls.Add(this.xtraTabControl1);
			this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
			this.dockPanel1_Container.Name = "dockPanel1_Container";
			this.dockPanel1_Container.Size = new System.Drawing.Size(754, 320);
			this.dockPanel1_Container.TabIndex = 0;
			// 
			// xtraTabControl1
			// 
			this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
			this.xtraTabControl1.Name = "xtraTabControl1";
			this.barManager1.SetPopupContextMenu(this.xtraTabControl1, this.popupMenu2);
			this.xtraTabControl1.Size = new System.Drawing.Size(754, 320);
			this.xtraTabControl1.TabIndex = 0;
			// 
			// txtSql
			// 
			this.txtSql.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtSql.Encoding = ((System.Text.Encoding)(resources.GetObject("txtSql.Encoding")));
			this.txtSql.IsIconBarVisible = false;
			this.txtSql.Location = new System.Drawing.Point(0, 25);
			this.txtSql.Name = "txtSql";
			this.barManager1.SetPopupContextMenu(this.txtSql, this.popupMenu1);
			this.txtSql.Size = new System.Drawing.Size(760, 68);
			this.txtSql.TabIndex = 0;
			// 
			// menuUndo
			// 
			this.menuUndo.Caption = "撤消(&U)";
			this.menuUndo.Id = 20;
			this.menuUndo.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z));
			this.menuUndo.Name = "menuUndo";
			this.menuUndo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuUndo_ItemClick);
			// 
			// menuRedo
			// 
			this.menuRedo.Caption = "重做(&R)";
			this.menuRedo.Id = 21;
			this.menuRedo.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y));
			this.menuRedo.Name = "menuRedo";
			this.menuRedo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuRedo_ItemClick);
			// 
			// menuCut
			// 
			this.menuCut.Caption = "剪切(&T)";
			this.menuCut.Id = 22;
			this.menuCut.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X));
			this.menuCut.Name = "menuCut";
			this.menuCut.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuCut_ItemClick);
			// 
			// menuCopy
			// 
			this.menuCopy.Caption = "复制(&C)";
			this.menuCopy.Id = 23;
			this.menuCopy.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C));
			this.menuCopy.Name = "menuCopy";
			this.menuCopy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuCopy_ItemClick);
			// 
			// menuPaste
			// 
			this.menuPaste.Caption = "粘贴(&P)";
			this.menuPaste.Id = 24;
			this.menuPaste.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V));
			this.menuPaste.Name = "menuPaste";
			this.menuPaste.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuPaste_ItemClick);
			// 
			// menuSelectAll
			// 
			this.menuSelectAll.Caption = "全选(&A)";
			this.menuSelectAll.Id = 25;
			this.menuSelectAll.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A));
			this.menuSelectAll.Name = "menuSelectAll";
			this.menuSelectAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuSelectAll_ItemClick);
			// 
			// menuShowNode
			// 
			this.menuShowNode.Caption = "定位对应节点(&N)";
			this.menuShowNode.Id = 27;
			this.menuShowNode.Name = "menuShowNode";
			this.menuShowNode.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuShowNode_ItemClick);
			// 
			// repositoryItemRadioGroup1
			// 
			this.repositoryItemRadioGroup1.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "分散"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "集中")});
			this.repositoryItemRadioGroup1.Name = "repositoryItemRadioGroup1";
			// 
			// repositoryItemComboBox1
			// 
			this.repositoryItemComboBox1.AutoHeight = false;
			this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
			// 
			// popupMenu2
			// 
			this.popupMenu2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.menuShowNode)});
			this.popupMenu2.Manager = this.barManager1;
			this.popupMenu2.Name = "popupMenu2";
			// 
			// popupMenu1
			// 
			this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.menuUndo, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuRedo),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuCut, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuCopy),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuPaste),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuSelectAll, true)});
			this.popupMenu1.Manager = this.barManager1;
			this.popupMenu1.Name = "popupMenu1";
			// 
			// _Servers
			// 
			this._Servers.DataSetName = "Servers";
			this._Servers.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// _UI
			// 
			this._UI.DataSetName = "UI";
			this._UI.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// SqlEdit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(760, 466);
			this.Controls.Add(this.txtSql);
			this.Controls.Add(this.dockPanel1);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "SqlEdit";
			this.TabText = "SqlEdit";
			this.Text = "SqlEdit";
			this.Deactivate += new System.EventHandler(this.SqlEdit_Deactivate);
			this.Load += new System.EventHandler(this.SqlEdit_Load);
			this.Activated += new System.EventHandler(this.SqlEdit_Activated);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SqlEdit_FormClosed);
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ricbDBName)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ripb)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
			this.dockPanel1.ResumeLayout(false);
			this.dockPanel1_Container.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.popupMenu2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._Servers)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._UI)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraBars.Bar bar1;
		private DevExpress.XtraBars.BarManager barManager1;
		private DevExpress.XtraBars.Bar bar2;
		private DevExpress.XtraBars.BarLargeButtonItem lblDBName;
		private DevExpress.XtraBars.Bar bar3;
		private DevExpress.XtraBars.BarDockControl barDockControlTop;
		private DevExpress.XtraBars.BarDockControl barDockControlBottom;
		private DevExpress.XtraBars.BarDockControl barDockControlLeft;
		private DevExpress.XtraBars.BarDockControl barDockControlRight;
		private DevExpress.XtraBars.BarEditItem cbDBName;
		private DevExpress.XtraEditors.Repository.RepositoryItemComboBox ricbDBName;
		private DevExpress.XtraBars.BarButtonItem btnExec;
		private DevExpress.XtraBars.BarAndDockingController barAndDockingController1;
		private DevExpress.XtraBars.Docking.DockManager dockManager1;
		private DevExpress.XtraBars.BarLargeButtonItem btnExport;
		private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
		private DevExpress.XtraBars.BarCheckItem bciSingleThread;
		private DevExpress.XtraBars.BarStaticItem bsiState;
		private DevExpress.XtraBars.BarEditItem beiProgressBar;
		private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar ripb;
		private DevExpress.XtraBars.BarButtonItem btnCancel;
		private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
		private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup repositoryItemRadioGroup1;
		private DevExpress.XtraBars.BarListItem bliResult;
		private ICSharpCode.TextEditor.TextEditorControl txtSql;
		private DevExpress.XtraBars.PopupMenu popupMenu1;
		private DevExpress.XtraBars.BarButtonItem menuUndo;
		private DevExpress.XtraBars.BarButtonItem menuRedo;
		private DevExpress.XtraBars.BarButtonItem menuCut;
		private DevExpress.XtraBars.BarButtonItem menuCopy;
		private DevExpress.XtraBars.BarButtonItem menuPaste;
		private DevExpress.XtraBars.BarButtonItem menuSelectAll;
		private DevExpress.XtraBars.PopupMenu popupMenu2;
		private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
		private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
		private DevExpress.XtraBars.BarButtonItem menuShowNode;
		private ApqDBManager.XSD.Servers _Servers;
		private ApqDBManager.XSD.UI _UI;
	}
}