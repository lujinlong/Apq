namespace ApqDBManager.Forms
{
	partial class SqlEditDoc
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SqlEditDoc));
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
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsmiUndo = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiRedo = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiCut = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiPaste = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiSelectAll = new System.Windows.Forms.ToolStripMenuItem();
			this.dsUI = new ApqDBManager.Forms.ErrList_XSD();
			this._Sqls = new Apq.DBC.XSD();
			this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
			this.actionList1 = new Crad.Windows.Forms.Actions.ActionList();
			this.acOpen = new Crad.Windows.Forms.Actions.Action();
			this.acSave = new Crad.Windows.Forms.Actions.Action();
			this.acSaveAs = new Crad.Windows.Forms.Actions.Action();
			this.toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dsUI)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._Sqls)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.actionList1)).BeginInit();
			this.SuspendLayout();
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
			this.toolStrip1.TabIndex = 8;
			this.toolStrip1.Text = "toolStrip1";
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
			// elementHost1
			// 
			this.elementHost1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.elementHost1.Location = new System.Drawing.Point(0, 25);
			this.elementHost1.Name = "elementHost1";
			this.elementHost1.Size = new System.Drawing.Size(760, 441);
			this.elementHost1.TabIndex = 9;
			this.elementHost1.Text = "elementHost1";
			this.elementHost1.Child = null;
			// 
			// actionList1
			// 
			this.actionList1.Actions.Add(this.acOpen);
			this.actionList1.Actions.Add(this.acSave);
			this.actionList1.Actions.Add(this.acSaveAs);
			this.actionList1.ContainerControl = this;
			// 
			// SqlEditDoc
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(760, 466);
			this.Controls.Add(this.elementHost1);
			this.Controls.Add(this.toolStrip1);
			this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
			this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.Name = "SqlEditDoc";
			this.TabText = "Sql编辑";
			this.Text = "Sql编辑";
			this.Load += new System.EventHandler(this.SqlEditDoc_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dsUI)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._Sqls)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.actionList1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

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
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ContextMenuStrip cms1;
		private System.Windows.Forms.ToolStripMenuItem tsmiUndo;
		private System.Windows.Forms.ToolStripMenuItem tsmiRedo;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem tsmiCut;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopy;
		private System.Windows.Forms.ToolStripMenuItem tsmiPaste;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem tsmiSelectAll;
		public ErrList_XSD dsUI;
		public Apq.DBC.XSD _Sqls;
		private System.Windows.Forms.Integration.ElementHost elementHost1;
		private Crad.Windows.Forms.Actions.ActionList actionList1;
		private Crad.Windows.Forms.Actions.Action acOpen;
		private Crad.Windows.Forms.Actions.Action acSave;
		private Crad.Windows.Forms.Actions.Action acSaveAs;


	}
}