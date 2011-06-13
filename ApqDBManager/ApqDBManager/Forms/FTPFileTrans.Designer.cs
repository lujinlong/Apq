namespace ApqDBManager.Forms
{
	partial class FTPFileTrans
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FTPFileTrans));
			this.beDBFTPFolder_Out = new DevExpress.XtraEditors.ButtonEdit();
			this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
			this.beCFTPFolder_Out = new DevExpress.XtraEditors.ButtonEdit();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.beDBFTPFolder_In = new DevExpress.XtraEditors.ButtonEdit();
			this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
			this.beCFTPFolder_In = new DevExpress.XtraEditors.ButtonEdit();
			this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
			this.btnDistribute = new DevExpress.XtraEditors.SimpleButton();
			this.btnCollect = new DevExpress.XtraEditors.SimpleButton();
			this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
			this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
			this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
			this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
			this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
			this.bar3 = new DevExpress.XtraBars.Bar();
			this.bsiState = new DevExpress.XtraBars.BarStaticItem();
			this.beiPb1 = new DevExpress.XtraBars.BarEditItem();
			this.ripb = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.fbdCFTPFolder_Out = new System.Windows.Forms.FolderBrowserDialog();
			this.fbdCFolder_In = new System.Windows.Forms.FolderBrowserDialog();
			this._UI = new ApqDBManager.Forms.ErrList_XSD();
			this._Sqls = new ApqDBManager.Forms.SrvsMgr.SrvsMgr_XSD();
			((System.ComponentModel.ISupportInitialize)(this.beDBFTPFolder_Out.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.beCFTPFolder_Out.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.beDBFTPFolder_In.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.beCFTPFolder_In.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
			this.groupControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
			this.groupControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ripb)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._UI)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._Sqls)).BeginInit();
			this.SuspendLayout();
			// 
			// beDBFTPFolder_Out
			// 
			this.beDBFTPFolder_Out.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.beDBFTPFolder_Out.EditValue = "";
			this.beDBFTPFolder_Out.Location = new System.Drawing.Point(101, 44);
			this.beDBFTPFolder_Out.Name = "beDBFTPFolder_Out";
			this.beDBFTPFolder_Out.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.beDBFTPFolder_Out.Size = new System.Drawing.Size(582, 21);
			this.beDBFTPFolder_Out.TabIndex = 24;
			this.beDBFTPFolder_Out.EditValueChanged += new System.EventHandler(this.beDBFolder_Out_EditValueChanged);
			// 
			// labelControl4
			// 
			this.labelControl4.Location = new System.Drawing.Point(11, 47);
			this.labelControl4.Name = "labelControl4";
			this.labelControl4.Size = new System.Drawing.Size(84, 14);
			this.labelControl4.TabIndex = 23;
			this.labelControl4.Text = "远程读取根目录";
			// 
			// beCFTPFolder_Out
			// 
			this.beCFTPFolder_Out.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.beCFTPFolder_Out.EditValue = "";
			this.beCFTPFolder_Out.Location = new System.Drawing.Point(101, 44);
			this.beCFTPFolder_Out.Name = "beCFTPFolder_Out";
			this.beCFTPFolder_Out.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.beCFTPFolder_Out.Size = new System.Drawing.Size(582, 21);
			this.beCFTPFolder_Out.TabIndex = 22;
			this.beCFTPFolder_Out.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.beCFolder_Out_ButtonClick);
			this.beCFTPFolder_Out.EditValueChanged += new System.EventHandler(this.beCFolder_Out_EditValueChanged);
			// 
			// labelControl1
			// 
			this.labelControl1.Location = new System.Drawing.Point(11, 47);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(84, 14);
			this.labelControl1.TabIndex = 21;
			this.labelControl1.Text = "本地读取根目录";
			// 
			// beDBFTPFolder_In
			// 
			this.beDBFTPFolder_In.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.beDBFTPFolder_In.EditValue = "";
			this.beDBFTPFolder_In.Location = new System.Drawing.Point(101, 71);
			this.beDBFTPFolder_In.Name = "beDBFTPFolder_In";
			this.beDBFTPFolder_In.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.beDBFTPFolder_In.Size = new System.Drawing.Size(582, 21);
			this.beDBFTPFolder_In.TabIndex = 28;
			this.beDBFTPFolder_In.EditValueChanged += new System.EventHandler(this.beDBFolder_In_EditValueChanged);
			// 
			// labelControl3
			// 
			this.labelControl3.Location = new System.Drawing.Point(11, 74);
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Size = new System.Drawing.Size(84, 14);
			this.labelControl3.TabIndex = 27;
			this.labelControl3.Text = "远程写入根目录";
			// 
			// beCFTPFolder_In
			// 
			this.beCFTPFolder_In.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.beCFTPFolder_In.EditValue = "";
			this.beCFTPFolder_In.Location = new System.Drawing.Point(101, 71);
			this.beCFTPFolder_In.Name = "beCFTPFolder_In";
			this.beCFTPFolder_In.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.beCFTPFolder_In.Size = new System.Drawing.Size(582, 21);
			this.beCFTPFolder_In.TabIndex = 26;
			this.beCFTPFolder_In.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.beCFolder_In_ButtonClick);
			this.beCFTPFolder_In.EditValueChanged += new System.EventHandler(this.beCFolder_In_EditValueChanged);
			// 
			// labelControl2
			// 
			this.labelControl2.Location = new System.Drawing.Point(11, 74);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(84, 14);
			this.labelControl2.TabIndex = 25;
			this.labelControl2.Text = "本地写入根目录";
			// 
			// btnDistribute
			// 
			this.btnDistribute.Location = new System.Drawing.Point(101, 98);
			this.btnDistribute.Name = "btnDistribute";
			this.btnDistribute.Size = new System.Drawing.Size(75, 23);
			this.btnDistribute.TabIndex = 29;
			this.btnDistribute.Text = "分发";
			this.btnDistribute.Click += new System.EventHandler(this.btnDistribute_Click);
			// 
			// btnCollect
			// 
			this.btnCollect.Location = new System.Drawing.Point(101, 98);
			this.btnCollect.Name = "btnCollect";
			this.btnCollect.Size = new System.Drawing.Size(75, 23);
			this.btnCollect.TabIndex = 30;
			this.btnCollect.Text = "收集";
			this.btnCollect.Click += new System.EventHandler(this.btnCollect_Click);
			// 
			// groupControl1
			// 
			this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupControl1.Controls.Add(this.labelControl5);
			this.groupControl1.Controls.Add(this.beCFTPFolder_Out);
			this.groupControl1.Controls.Add(this.labelControl1);
			this.groupControl1.Controls.Add(this.btnDistribute);
			this.groupControl1.Controls.Add(this.labelControl3);
			this.groupControl1.Controls.Add(this.beDBFTPFolder_In);
			this.groupControl1.Location = new System.Drawing.Point(12, 12);
			this.groupControl1.Name = "groupControl1";
			this.groupControl1.Size = new System.Drawing.Size(688, 142);
			this.groupControl1.TabIndex = 31;
			this.groupControl1.Text = "文件分发";
			// 
			// labelControl5
			// 
			this.labelControl5.Appearance.ForeColor = System.Drawing.Color.Coral;
			this.labelControl5.Appearance.Options.UseForeColor = true;
			this.labelControl5.Location = new System.Drawing.Point(11, 24);
			this.labelControl5.Name = "labelControl5";
			this.labelControl5.Size = new System.Drawing.Size(576, 14);
			this.labelControl5.TabIndex = 30;
			this.labelControl5.Text = "[覆盖]分发规则:将本地根目录下按服务器命名的子级文件夹中的所有文件(不含子文件夹)上传到对应的服务器";
			// 
			// groupControl2
			// 
			this.groupControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupControl2.Controls.Add(this.labelControl6);
			this.groupControl2.Controls.Add(this.beDBFTPFolder_Out);
			this.groupControl2.Controls.Add(this.labelControl4);
			this.groupControl2.Controls.Add(this.btnCollect);
			this.groupControl2.Controls.Add(this.labelControl2);
			this.groupControl2.Controls.Add(this.beCFTPFolder_In);
			this.groupControl2.Location = new System.Drawing.Point(12, 200);
			this.groupControl2.Name = "groupControl2";
			this.groupControl2.Size = new System.Drawing.Size(688, 142);
			this.groupControl2.TabIndex = 32;
			this.groupControl2.Text = "文件收集";
			// 
			// labelControl6
			// 
			this.labelControl6.Appearance.ForeColor = System.Drawing.Color.Coral;
			this.labelControl6.Appearance.Options.UseForeColor = true;
			this.labelControl6.Location = new System.Drawing.Point(5, 24);
			this.labelControl6.Name = "labelControl6";
			this.labelControl6.Size = new System.Drawing.Size(552, 14);
			this.labelControl6.TabIndex = 31;
			this.labelControl6.Text = "[覆盖]收集规则:将远程根目录下的所有文件(不含子文件夹)下载到本地根目录下按服务器命名的文件夹中";
			// 
			// barManager1
			// 
			this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar3});
			this.barManager1.DockControls.Add(this.barDockControlTop);
			this.barManager1.DockControls.Add(this.barDockControlBottom);
			this.barManager1.DockControls.Add(this.barDockControlLeft);
			this.barManager1.DockControls.Add(this.barDockControlRight);
			this.barManager1.Form = this;
			this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bsiState,
            this.beiPb1});
			this.barManager1.MaxItemId = 2;
			this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ripb});
			this.barManager1.StatusBar = this.bar3;
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
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.beiPb1, "", false, true, true, 329)});
			this.bar3.OptionsBar.AllowQuickCustomization = false;
			this.bar3.OptionsBar.DrawDragBorder = false;
			this.bar3.OptionsBar.UseWholeRow = true;
			this.bar3.Text = "Status bar";
			// 
			// bsiState
			// 
			this.bsiState.AutoSize = DevExpress.XtraBars.BarStaticItemSize.Spring;
			this.bsiState.Id = 0;
			this.bsiState.Name = "bsiState";
			this.bsiState.TextAlignment = System.Drawing.StringAlignment.Near;
			this.bsiState.Width = 32;
			// 
			// beiPb1
			// 
			this.beiPb1.Caption = "barEditItem1";
			this.beiPb1.Edit = this.ripb;
			this.beiPb1.Id = 1;
			this.beiPb1.Name = "beiPb1";
			// 
			// ripb
			// 
			this.ripb.Name = "ripb";
			// 
			// _UI
			// 
			this._UI.DataSetName = "UI";
			this._UI.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// _Sqls
			// 
			this._Sqls.DataSetName = "Sqls";
			this._Sqls.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// FTPFileTrans
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(712, 416);
			this.Controls.Add(this.groupControl2);
			this.Controls.Add(this.groupControl1);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FTPFileTrans";
			this.TabText = "FTP文件传送";
			this.Text = "FTP文件传送";
			this.Deactivate += new System.EventHandler(this.FileTrans_Deactivate);
			this.Load += new System.EventHandler(this.FileTrans_Load);
			this.Activated += new System.EventHandler(this.FileTrans_Activated);
			((System.ComponentModel.ISupportInitialize)(this.beDBFTPFolder_Out.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.beCFTPFolder_Out.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.beDBFTPFolder_In.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.beCFTPFolder_In.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
			this.groupControl1.ResumeLayout(false);
			this.groupControl1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
			this.groupControl2.ResumeLayout(false);
			this.groupControl2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ripb)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._UI)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._Sqls)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.ButtonEdit beDBFTPFolder_Out;
		private DevExpress.XtraEditors.LabelControl labelControl4;
		private DevExpress.XtraEditors.ButtonEdit beCFTPFolder_Out;
		private DevExpress.XtraEditors.LabelControl labelControl1;
		private DevExpress.XtraEditors.ButtonEdit beDBFTPFolder_In;
		private DevExpress.XtraEditors.LabelControl labelControl3;
		private DevExpress.XtraEditors.ButtonEdit beCFTPFolder_In;
		private DevExpress.XtraEditors.LabelControl labelControl2;
		private DevExpress.XtraEditors.SimpleButton btnDistribute;
		private DevExpress.XtraEditors.SimpleButton btnCollect;
		private DevExpress.XtraEditors.GroupControl groupControl1;
		private DevExpress.XtraEditors.GroupControl groupControl2;
		private DevExpress.XtraBars.BarManager barManager1;
		private DevExpress.XtraBars.Bar bar3;
		private DevExpress.XtraBars.BarDockControl barDockControlTop;
		private DevExpress.XtraBars.BarDockControl barDockControlBottom;
		private DevExpress.XtraBars.BarDockControl barDockControlLeft;
		private DevExpress.XtraBars.BarDockControl barDockControlRight;
		private DevExpress.XtraBars.BarStaticItem bsiState;
		private DevExpress.XtraBars.BarEditItem beiPb1;
		private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar ripb;
		private System.Windows.Forms.FolderBrowserDialog fbdCFTPFolder_Out;
		private System.Windows.Forms.FolderBrowserDialog fbdCFolder_In;
		private DevExpress.XtraEditors.LabelControl labelControl5;
		private DevExpress.XtraEditors.LabelControl labelControl6;
		private ErrList_XSD _UI;
		private ApqDBManager.Forms.SrvsMgr.SrvsMgr_XSD _Sqls;
	}
}