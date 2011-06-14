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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FTPFileTrans));
			this.beDBFTPFolder_Out = new DevExpress.XtraEditors.ButtonEdit();
			this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
			this.beCFTPFolder_Out = new DevExpress.XtraEditors.ButtonEdit();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.beDBFTPFolder_In = new DevExpress.XtraEditors.ButtonEdit();
			this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
			this.beCFTPFolder_In = new DevExpress.XtraEditors.ButtonEdit();
			this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
			this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
			this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
			this.fbdCFTPFolder_Out = new System.Windows.Forms.FolderBrowserDialog();
			this.fbdCFolder_In = new System.Windows.Forms.FolderBrowserDialog();
			this._UI = new ApqDBManager.Forms.ErrList_XSD();
			this._Sqls = new ApqDBManager.Forms.SrvsMgr.SrvsMgr_XSD();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.tspb = new System.Windows.Forms.ToolStripProgressBar();
			this.btnDistribute = new System.Windows.Forms.Button();
			this.btnCollect = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.beDBFTPFolder_Out.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.beCFTPFolder_Out.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.beDBFTPFolder_In.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.beCFTPFolder_In.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._UI)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._Sqls)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// beDBFTPFolder_Out
			// 
			this.beDBFTPFolder_Out.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.beDBFTPFolder_Out.EditValue = "";
			this.beDBFTPFolder_Out.Location = new System.Drawing.Point(102, 42);
			this.beDBFTPFolder_Out.Name = "beDBFTPFolder_Out";
			this.beDBFTPFolder_Out.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.beDBFTPFolder_Out.Size = new System.Drawing.Size(582, 21);
			this.beDBFTPFolder_Out.TabIndex = 24;
			this.beDBFTPFolder_Out.EditValueChanged += new System.EventHandler(this.beDBFolder_Out_EditValueChanged);
			// 
			// labelControl4
			// 
			this.labelControl4.Location = new System.Drawing.Point(12, 45);
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
			this.beCFTPFolder_Out.Location = new System.Drawing.Point(101, 39);
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
			this.labelControl1.Location = new System.Drawing.Point(11, 42);
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
			this.beDBFTPFolder_In.Location = new System.Drawing.Point(101, 66);
			this.beDBFTPFolder_In.Name = "beDBFTPFolder_In";
			this.beDBFTPFolder_In.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.beDBFTPFolder_In.Size = new System.Drawing.Size(582, 21);
			this.beDBFTPFolder_In.TabIndex = 28;
			this.beDBFTPFolder_In.EditValueChanged += new System.EventHandler(this.beDBFolder_In_EditValueChanged);
			// 
			// labelControl3
			// 
			this.labelControl3.Location = new System.Drawing.Point(11, 69);
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
			this.beCFTPFolder_In.Location = new System.Drawing.Point(102, 69);
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
			this.labelControl2.Location = new System.Drawing.Point(12, 72);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(84, 14);
			this.labelControl2.TabIndex = 25;
			this.labelControl2.Text = "本地写入根目录";
			// 
			// labelControl5
			// 
			this.labelControl5.Appearance.ForeColor = System.Drawing.Color.Coral;
			this.labelControl5.Appearance.Options.UseForeColor = true;
			this.labelControl5.Location = new System.Drawing.Point(11, 19);
			this.labelControl5.Name = "labelControl5";
			this.labelControl5.Size = new System.Drawing.Size(576, 14);
			this.labelControl5.TabIndex = 30;
			this.labelControl5.Text = "[覆盖]分发规则:将本地根目录下按服务器命名的子级文件夹中的所有文件(不含子文件夹)上传到对应的服务器";
			// 
			// labelControl6
			// 
			this.labelControl6.Appearance.ForeColor = System.Drawing.Color.Coral;
			this.labelControl6.Appearance.Options.UseForeColor = true;
			this.labelControl6.Location = new System.Drawing.Point(6, 22);
			this.labelControl6.Name = "labelControl6";
			this.labelControl6.Size = new System.Drawing.Size(552, 14);
			this.labelControl6.TabIndex = 31;
			this.labelControl6.Text = "[覆盖]收集规则:将远程根目录下的所有文件(不含子文件夹)下载到本地根目录下按服务器命名的文件夹中";
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
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnDistribute);
			this.groupBox1.Controls.Add(this.labelControl5);
			this.groupBox1.Controls.Add(this.beCFTPFolder_Out);
			this.groupBox1.Controls.Add(this.beDBFTPFolder_In);
			this.groupBox1.Controls.Add(this.labelControl1);
			this.groupBox1.Controls.Add(this.labelControl3);
			this.groupBox1.Location = new System.Drawing.Point(12, 27);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(688, 142);
			this.groupBox1.TabIndex = 33;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "文件分发";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.btnCollect);
			this.groupBox2.Controls.Add(this.labelControl6);
			this.groupBox2.Controls.Add(this.beDBFTPFolder_Out);
			this.groupBox2.Controls.Add(this.beCFTPFolder_In);
			this.groupBox2.Controls.Add(this.labelControl4);
			this.groupBox2.Controls.Add(this.labelControl2);
			this.groupBox2.Location = new System.Drawing.Point(12, 195);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(688, 142);
			this.groupBox2.TabIndex = 34;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "文件收集";
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslStatus,
            this.tspb});
			this.statusStrip1.Location = new System.Drawing.Point(0, 394);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(712, 22);
			this.statusStrip1.TabIndex = 35;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// tsslStatus
			// 
			this.tsslStatus.AutoSize = false;
			this.tsslStatus.Name = "tsslStatus";
			this.tsslStatus.Size = new System.Drawing.Size(400, 17);
			// 
			// tspb
			// 
			this.tspb.Name = "tspb";
			this.tspb.Size = new System.Drawing.Size(250, 16);
			// 
			// btnDistribute
			// 
			this.btnDistribute.Location = new System.Drawing.Point(101, 93);
			this.btnDistribute.Name = "btnDistribute";
			this.btnDistribute.Size = new System.Drawing.Size(75, 23);
			this.btnDistribute.TabIndex = 31;
			this.btnDistribute.Text = "分发";
			this.btnDistribute.UseVisualStyleBackColor = true;
			this.btnDistribute.Click += new System.EventHandler(this.btnDistribute_Click);
			// 
			// btnCollect
			// 
			this.btnCollect.Location = new System.Drawing.Point(101, 96);
			this.btnCollect.Name = "btnCollect";
			this.btnCollect.Size = new System.Drawing.Size(75, 23);
			this.btnCollect.TabIndex = 32;
			this.btnCollect.Text = "收集";
			this.btnCollect.UseVisualStyleBackColor = true;
			this.btnCollect.Click += new System.EventHandler(this.btnCollect_Click);
			// 
			// FTPFileTrans
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(712, 416);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.statusStrip1);
			this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
			this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
			((System.ComponentModel.ISupportInitialize)(this._UI)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._Sqls)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

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
		private System.Windows.Forms.FolderBrowserDialog fbdCFTPFolder_Out;
		private System.Windows.Forms.FolderBrowserDialog fbdCFolder_In;
		private DevExpress.XtraEditors.LabelControl labelControl5;
		private DevExpress.XtraEditors.LabelControl labelControl6;
		private ErrList_XSD _UI;
		private ApqDBManager.Forms.SrvsMgr.SrvsMgr_XSD _Sqls;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel tsslStatus;
		private System.Windows.Forms.ToolStripProgressBar tspb;
		private System.Windows.Forms.Button btnDistribute;
		private System.Windows.Forms.Button btnCollect;
	}
}