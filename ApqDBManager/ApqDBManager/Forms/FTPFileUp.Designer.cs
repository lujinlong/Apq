namespace ApqDBManager.Forms
{
	partial class FTPFileUp
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FTPFileUp));
			System.Windows.Forms.TreeListViewItemCollection.TreeListViewItemCollectionComparer treeListViewItemCollectionComparer2 = new System.Windows.Forms.TreeListViewItemCollection.TreeListViewItemCollectionComparer();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this._UI = new ApqDBManager.Forms.ErrList_XSD();
			this._Sqls = new ApqDBManager.Forms.SrvsMgr.SrvsMgr_XSD();
			this.treeListView1 = new System.Windows.Forms.TreeListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.label1 = new System.Windows.Forms.Label();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
			this.tsbUp = new System.Windows.Forms.ToolStripButton();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsslStateFileUp = new System.Windows.Forms.ToolStripStatusLabel();
			this.tspb = new System.Windows.Forms.ToolStripProgressBar();
			this.label2 = new System.Windows.Forms.Label();
			this.txtDBFolder_Up = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this._UI)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._Sqls)).BeginInit();
			this.toolStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
			this.imageList1.Images.SetKeyName(0, "");
			this.imageList1.Images.SetKeyName(1, "");
			this.imageList1.Images.SetKeyName(2, "");
			this.imageList1.Images.SetKeyName(3, "");
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
			// treeListView1
			// 
			this.treeListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.treeListView1.CheckBoxes = System.Windows.Forms.CheckBoxesTypes.Recursive;
			this.treeListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
			treeListViewItemCollectionComparer2.Column = 0;
			treeListViewItemCollectionComparer2.SortOrder = System.Windows.Forms.SortOrder.Ascending;
			this.treeListView1.Comparer = treeListViewItemCollectionComparer2;
			this.treeListView1.Location = new System.Drawing.Point(0, 77);
			this.treeListView1.Name = "treeListView1";
			this.treeListView1.Size = new System.Drawing.Size(712, 358);
			this.treeListView1.SmallImageList = this.imageList1;
			this.treeListView1.TabIndex = 13;
			this.treeListView1.UseCompatibleStateImageBehavior = false;
			this.treeListView1.BeforeExpand += new System.Windows.Forms.TreeListViewCancelEventHandler(this.treeListView1_BeforeExpand);
			this.treeListView1.SelectedIndexChanged += new System.EventHandler(this.treeListView1_SelectedIndexChanged);
			this.treeListView1.BeforeCollapse += new System.Windows.Forms.TreeListViewCancelEventHandler(this.treeListView1_BeforeCollapse);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "名称";
			this.columnHeader1.Width = 500;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "类型";
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "大小(Bytes)";
			this.columnHeader3.Width = 150;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(10, 35);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(107, 12);
			this.label1.TabIndex = 14;
			this.label1.Text = "上传到远程根目录:";
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbRefresh,
            this.tsbUp});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(712, 25);
			this.toolStrip1.TabIndex = 15;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tsbRefresh
			// 
			this.tsbRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsbRefresh.Image")));
			this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbRefresh.Name = "tsbRefresh";
			this.tsbRefresh.Size = new System.Drawing.Size(51, 22);
			this.tsbRefresh.Text = "刷新(&R)";
			this.tsbRefresh.Click += new System.EventHandler(this.tsbRefresh_Click);
			// 
			// tsbUp
			// 
			this.tsbUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbUp.Image = ((System.Drawing.Image)(resources.GetObject("tsbUp.Image")));
			this.tsbUp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbUp.Name = "tsbUp";
			this.tsbUp.Size = new System.Drawing.Size(51, 22);
			this.tsbUp.Text = "上传(&U)";
			this.tsbUp.Click += new System.EventHandler(this.tsbUp_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslStatus,
            this.tsslStateFileUp,
            this.tspb});
			this.statusStrip1.Location = new System.Drawing.Point(0, 444);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(712, 22);
			this.statusStrip1.TabIndex = 16;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// tsslStatus
			// 
			this.tsslStatus.AutoSize = false;
			this.tsslStatus.Name = "tsslStatus";
			this.tsslStatus.Size = new System.Drawing.Size(500, 17);
			this.tsslStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tsslStateFileUp
			// 
			this.tsslStateFileUp.Name = "tsslStateFileUp";
			this.tsslStateFileUp.Size = new System.Drawing.Size(0, 17);
			this.tsslStateFileUp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tspb
			// 
			this.tspb.Name = "tspb";
			this.tspb.RightToLeftLayout = true;
			this.tspb.Size = new System.Drawing.Size(150, 16);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.Color.Coral;
			this.label2.Location = new System.Drawing.Point(10, 57);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(263, 12);
			this.label2.TabIndex = 17;
			this.label2.Text = "本地资源:只会上传已加载到列表中且选中的文件";
			// 
			// txtDBFolder_Up
			// 
			this.txtDBFolder_Up.Location = new System.Drawing.Point(123, 32);
			this.txtDBFolder_Up.Name = "txtDBFolder_Up";
			this.txtDBFolder_Up.Size = new System.Drawing.Size(577, 21);
			this.txtDBFolder_Up.TabIndex = 19;
			this.txtDBFolder_Up.TextChanged += new System.EventHandler(this.txtDBFolder_Up_TextChanged);
			// 
			// FTPFileUp
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(712, 466);
			this.Controls.Add(this.txtDBFolder_Up);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.treeListView1);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.statusStrip1);
			this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(720, 500);
			this.Name = "FTPFileUp";
			this.TabText = "文件上传";
			this.Text = "FTP文件上传";
			this.Deactivate += new System.EventHandler(this.FileUp_Deactivate);
			this.Load += new System.EventHandler(this.FileUp_Load);
			this.Activated += new System.EventHandler(this.FileUp_Activated);
			((System.ComponentModel.ISupportInitialize)(this._UI)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._Sqls)).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ImageList imageList1;
		private ErrList_XSD _UI;
		private ApqDBManager.Forms.SrvsMgr.SrvsMgr_XSD _Sqls;
		private System.Windows.Forms.TreeListView treeListView1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsbRefresh;
		private System.Windows.Forms.ToolStripButton tsbUp;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel tsslStatus;
		private System.Windows.Forms.ToolStripStatusLabel tsslStateFileUp;
		private System.Windows.Forms.ToolStripProgressBar tspb;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtDBFolder_Up;

	}
}