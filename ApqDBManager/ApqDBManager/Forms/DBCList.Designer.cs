namespace ApqDBManager.Forms
{
	partial class DBCList
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBCList));
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.gridControl1 = new DevExpress.XtraGrid.GridControl();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.ribeName = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.ribePwd = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
			this.bar3 = new DevExpress.XtraBars.Bar();
			this.bar1 = new DevExpress.XtraBars.Bar();
			this.bbiAdd = new DevExpress.XtraBars.BarButtonItem();
			this.bbiEdit = new DevExpress.XtraBars.BarButtonItem();
			this.bbiView = new DevExpress.XtraBars.BarButtonItem();
			this.bbiDelete = new DevExpress.XtraBars.BarButtonItem();
			this.bbiCs = new DevExpress.XtraBars.BarButtonItem();
			this.bbiOld = new DevExpress.XtraBars.BarButtonItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ribeName)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ribePwd)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
			this.SuspendLayout();
			// 
			// gridControl1
			// 
			this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControl1.Location = new System.Drawing.Point(0, 24);
			this.gridControl1.MainView = this.gridView1;
			this.gridControl1.Name = "gridControl1";
			this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ribePwd,
            this.ribeName});
			this.gridControl1.Size = new System.Drawing.Size(600, 377);
			this.gridControl1.TabIndex = 4;
			this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
			// 
			// gridView1
			// 
			this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn6,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
			this.gridView1.GridControl = this.gridControl1;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
			this.gridView1.OptionsView.ShowGroupPanel = false;
			// 
			// gridColumn1
			// 
			this.gridColumn1.Caption = "名称";
			this.gridColumn1.ColumnEdit = this.ribeName;
			this.gridColumn1.FieldName = "name";
			this.gridColumn1.Name = "gridColumn1";
			this.gridColumn1.Visible = true;
			this.gridColumn1.VisibleIndex = 0;
			// 
			// ribeName
			// 
			this.ribeName.AutoHeight = false;
			this.ribeName.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Undo)});
			this.ribeName.Name = "ribeName";
			this.ribeName.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.ribeName_ButtonClick);
			// 
			// gridColumn2
			// 
			this.gridColumn2.Caption = "数据库名";
			this.gridColumn2.FieldName = "DBName";
			this.gridColumn2.Name = "gridColumn2";
			this.gridColumn2.Visible = true;
			this.gridColumn2.VisibleIndex = 1;
			// 
			// gridColumn6
			// 
			this.gridColumn6.Caption = "服务器名";
			this.gridColumn6.FieldName = "ServerName";
			this.gridColumn6.Name = "gridColumn6";
			this.gridColumn6.Visible = true;
			this.gridColumn6.VisibleIndex = 2;
			// 
			// gridColumn3
			// 
			this.gridColumn3.Caption = "用户名";
			this.gridColumn3.FieldName = "UserId";
			this.gridColumn3.Name = "gridColumn3";
			this.gridColumn3.Visible = true;
			this.gridColumn3.VisibleIndex = 3;
			// 
			// gridColumn4
			// 
			this.gridColumn4.Caption = "密码";
			this.gridColumn4.ColumnEdit = this.ribePwd;
			this.gridColumn4.FieldName = "Pwd";
			this.gridColumn4.Name = "gridColumn4";
			this.gridColumn4.Visible = true;
			this.gridColumn4.VisibleIndex = 4;
			// 
			// ribePwd
			// 
			this.ribePwd.AutoHeight = false;
			this.ribePwd.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinRight)});
			this.ribePwd.Name = "ribePwd";
			this.ribePwd.PasswordChar = '*';
			this.ribePwd.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.ribePwd_ButtonClick);
			// 
			// gridColumn5
			// 
			this.gridColumn5.Caption = "选项";
			this.gridColumn5.FieldName = "Option";
			this.gridColumn5.Name = "gridColumn5";
			this.gridColumn5.Visible = true;
			this.gridColumn5.VisibleIndex = 5;
			// 
			// barManager1
			// 
			this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar3,
            this.bar1});
			this.barManager1.DockControls.Add(this.barDockControlTop);
			this.barManager1.DockControls.Add(this.barDockControlBottom);
			this.barManager1.DockControls.Add(this.barDockControlLeft);
			this.barManager1.DockControls.Add(this.barDockControl1);
			this.barManager1.Form = this;
			this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiAdd,
            this.bbiEdit,
            this.bbiView,
            this.bbiDelete,
            this.bbiCs,
            this.bbiOld});
			this.barManager1.MaxItemId = 6;
			this.barManager1.StatusBar = this.bar3;
			// 
			// bar3
			// 
			this.bar3.BarName = "Status bar";
			this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
			this.bar3.DockCol = 0;
			this.bar3.DockRow = 0;
			this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
			this.bar3.OptionsBar.AllowQuickCustomization = false;
			this.bar3.OptionsBar.DrawDragBorder = false;
			this.bar3.OptionsBar.UseWholeRow = true;
			this.bar3.Text = "Status bar";
			// 
			// bar1
			// 
			this.bar1.BarName = "Custom 4";
			this.bar1.DockCol = 0;
			this.bar1.DockRow = 0;
			this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiAdd),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiView),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiDelete),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiCs, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiOld)});
			this.bar1.Text = "Custom 4";
			// 
			// bbiAdd
			// 
			this.bbiAdd.Caption = "添加";
			this.bbiAdd.Id = 0;
			this.bbiAdd.Name = "bbiAdd";
			this.bbiAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiAdd_ItemClick);
			// 
			// bbiEdit
			// 
			this.bbiEdit.Caption = "编辑";
			this.bbiEdit.Id = 2;
			this.bbiEdit.Name = "bbiEdit";
			this.bbiEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiEdit_ItemClick);
			// 
			// bbiView
			// 
			this.bbiView.Caption = "查看";
			this.bbiView.Id = 3;
			this.bbiView.Name = "bbiView";
			this.bbiView.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiView_ItemClick);
			// 
			// bbiDelete
			// 
			this.bbiDelete.Caption = "删除";
			this.bbiDelete.Id = 5;
			this.bbiDelete.Name = "bbiDelete";
			this.bbiDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiDelete_ItemClick);
			// 
			// bbiCs
			// 
			this.bbiCs.Caption = "查看连接字符串";
			this.bbiCs.Id = 4;
			this.bbiCs.Name = "bbiCs";
			this.bbiCs.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiCs_ItemClick);
			// 
			// bbiOld
			// 
			this.bbiOld.Caption = "另存为旧版文件";
			this.bbiOld.Id = 1;
			this.bbiOld.Name = "bbiOld";
			this.bbiOld.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiOld_ItemClick);
			// 
			// DBCList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(600, 423);
			this.Controls.Add(this.gridControl1);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControl1);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "DBCList";
			this.TabText = "DB连接";
			this.Text = "DB连接";
			this.Load += new System.EventHandler(this.DBCList_Load);
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ribeName)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ribePwd)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraBars.BarDockControl barDockControlRight;
		private DevExpress.XtraGrid.GridControl gridControl1;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit ribePwd;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit ribeName;
		private DevExpress.XtraBars.BarManager barManager1;
		private DevExpress.XtraBars.BarButtonItem bbiAdd;
		private DevExpress.XtraBars.Bar bar3;
		private DevExpress.XtraBars.BarDockControl barDockControlTop;
		private DevExpress.XtraBars.BarDockControl barDockControlBottom;
		private DevExpress.XtraBars.BarDockControl barDockControlLeft;
		private DevExpress.XtraBars.BarDockControl barDockControl1;
		private DevExpress.XtraBars.Bar bar1;
		private DevExpress.XtraBars.BarButtonItem bbiOld;
		private DevExpress.XtraBars.BarButtonItem bbiEdit;
		private DevExpress.XtraBars.BarButtonItem bbiView;
		private DevExpress.XtraBars.BarButtonItem bbiCs;
		private DevExpress.XtraBars.BarButtonItem bbiDelete;
	}
}