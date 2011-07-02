using Apq_LocalTools.Forms;
namespace Apq_LocalTools
{
	partial class TxtEncoding
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
			System.Windows.Forms.TreeListViewItemCollection.TreeListViewItemCollectionComparer treeListViewItemCollectionComparer1 = new System.Windows.Forms.TreeListViewItemCollection.TreeListViewItemCollectionComparer();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TxtEncoding));
			this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.tspb = new System.Windows.Forms.ToolStripProgressBar();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.treeListView1 = new System.Windows.Forms.TreeListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.imgList = new System.Windows.Forms.ImageList(this.components);
			this.label2 = new System.Windows.Forms.Label();
			this.cbSrcEncoding = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cbDefaultEncoding = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtExt = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.cbContainsChildren = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.txtCustomer = new System.Windows.Forms.TextBox();
			this.rbCustomer = new System.Windows.Forms.RadioButton();
			this.rbEncodeName = new System.Windows.Forms.RadioButton();
			this.rbKeep = new System.Windows.Forms.RadioButton();
			this.label1 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.cbDstEncoding = new System.Windows.Forms.ComboBox();
			this.btnTrans = new System.Windows.Forms.Button();
			this.statusStrip1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
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
			this.statusStrip1.Location = new System.Drawing.Point(0, 406);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(760, 22);
			this.statusStrip1.TabIndex = 1;
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
            this.columnHeader3,
            this.columnHeader5,
            this.columnHeader4});
			treeListViewItemCollectionComparer1.Column = 0;
			treeListViewItemCollectionComparer1.SortOrder = System.Windows.Forms.SortOrder.Ascending;
			this.treeListView1.Comparer = treeListViewItemCollectionComparer1;
			this.treeListView1.Location = new System.Drawing.Point(0, 0);
			this.treeListView1.Name = "treeListView1";
			this.treeListView1.Size = new System.Drawing.Size(760, 203);
			this.treeListView1.SmallImageList = this.imgList;
			this.treeListView1.TabIndex = 2;
			this.treeListView1.UseCompatibleStateImageBehavior = false;
			this.treeListView1.BeforeExpand += new System.Windows.Forms.TreeListViewCancelEventHandler(this.treeListView1_BeforeExpand);
			this.treeListView1.BeforeCollapse += new System.Windows.Forms.TreeListViewCancelEventHandler(this.treeListView1_BeforeCollapse);
			this.treeListView1.SelectedIndexChanged += new System.EventHandler(this.treeListView1_SelectedIndexChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "名称";
			this.columnHeader1.Width = 400;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "大小(B)";
			this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader2.Width = 150;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "类型";
			this.columnHeader3.Width = 100;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "创建日期";
			this.columnHeader5.Width = 140;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "修改日期";
			this.columnHeader4.Width = 140;
			// 
			// imgList
			// 
			this.imgList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imgList.ImageSize = new System.Drawing.Size(16, 16);
			this.imgList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 70);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 12);
			this.label2.TabIndex = 6;
			this.label2.Text = "默认编码：";
			// 
			// cbSrcEncoding
			// 
			this.cbSrcEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbSrcEncoding.FormattingEnabled = true;
			this.cbSrcEncoding.Items.AddRange(new object[] {
            "自动检测",
            "UTF-8",
            "GB2312",
            "ASCII"});
			this.cbSrcEncoding.Location = new System.Drawing.Point(77, 35);
			this.cbSrcEncoding.Name = "cbSrcEncoding";
			this.cbSrcEncoding.Size = new System.Drawing.Size(282, 20);
			this.cbSrcEncoding.TabIndex = 5;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 38);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(65, 12);
			this.label3.TabIndex = 8;
			this.label3.Text = "原始编码：";
			// 
			// cbDefaultEncoding
			// 
			this.cbDefaultEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbDefaultEncoding.FormattingEnabled = true;
			this.cbDefaultEncoding.Items.AddRange(new object[] {
            "UTF-8",
            "GB2312",
            "ASCII"});
			this.cbDefaultEncoding.Location = new System.Drawing.Point(77, 67);
			this.cbDefaultEncoding.Name = "cbDefaultEncoding";
			this.cbDefaultEncoding.Size = new System.Drawing.Size(282, 20);
			this.cbDefaultEncoding.TabIndex = 7;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox1.Controls.Add(this.txtExt);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.cbDefaultEncoding);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.cbSrcEncoding);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.cbContainsChildren);
			this.groupBox1.Location = new System.Drawing.Point(12, 209);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(365, 191);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "读取参数";
			// 
			// txtExt
			// 
			this.txtExt.Location = new System.Drawing.Point(77, 135);
			this.txtExt.Name = "txtExt";
			this.txtExt.Size = new System.Drawing.Size(279, 21);
			this.txtExt.TabIndex = 17;
			this.txtExt.Text = "*.txt;*.sql;*.xml;*.ini;";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 138);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(65, 12);
			this.label5.TabIndex = 16;
			this.label5.Text = "文件类型：";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.ForeColor = System.Drawing.Color.DarkGreen;
			this.label4.Location = new System.Drawing.Point(75, 94);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(281, 12);
			this.label4.TabIndex = 9;
			this.label4.Text = "自动检测无法确定编码时使用默认编码读取原始文件";
			// 
			// cbContainsChildren
			// 
			this.cbContainsChildren.AutoSize = true;
			this.cbContainsChildren.Checked = true;
			this.cbContainsChildren.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbContainsChildren.Location = new System.Drawing.Point(77, 162);
			this.cbContainsChildren.Name = "cbContainsChildren";
			this.cbContainsChildren.Size = new System.Drawing.Size(84, 16);
			this.cbContainsChildren.TabIndex = 9;
			this.cbContainsChildren.Text = "包含子目录";
			this.cbContainsChildren.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox2.Controls.Add(this.txtCustomer);
			this.groupBox2.Controls.Add(this.rbCustomer);
			this.groupBox2.Controls.Add(this.rbEncodeName);
			this.groupBox2.Controls.Add(this.rbKeep);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.cbDstEncoding);
			this.groupBox2.Location = new System.Drawing.Point(383, 209);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(365, 156);
			this.groupBox2.TabIndex = 10;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "转换参数";
			// 
			// txtCustomer
			// 
			this.txtCustomer.Location = new System.Drawing.Point(94, 112);
			this.txtCustomer.Name = "txtCustomer";
			this.txtCustomer.Size = new System.Drawing.Size(133, 21);
			this.txtCustomer.TabIndex = 15;
			this.txtCustomer.Text = "out";
			// 
			// rbCustomer
			// 
			this.rbCustomer.AutoSize = true;
			this.rbCustomer.Location = new System.Drawing.Point(77, 90);
			this.rbCustomer.Name = "rbCustomer";
			this.rbCustomer.Size = new System.Drawing.Size(89, 16);
			this.rbCustomer.TabIndex = 14;
			this.rbCustomer.Text = "原名_自定义";
			this.rbCustomer.UseVisualStyleBackColor = true;
			// 
			// rbEncodeName
			// 
			this.rbEncodeName.AutoSize = true;
			this.rbEncodeName.Location = new System.Drawing.Point(77, 68);
			this.rbEncodeName.Name = "rbEncodeName";
			this.rbEncodeName.Size = new System.Drawing.Size(77, 16);
			this.rbEncodeName.TabIndex = 13;
			this.rbEncodeName.Text = "原名_编码";
			this.rbEncodeName.UseVisualStyleBackColor = true;
			// 
			// rbKeep
			// 
			this.rbKeep.AutoSize = true;
			this.rbKeep.Checked = true;
			this.rbKeep.Location = new System.Drawing.Point(77, 46);
			this.rbKeep.Name = "rbKeep";
			this.rbKeep.Size = new System.Drawing.Size(47, 16);
			this.rbKeep.TabIndex = 12;
			this.rbKeep.TabStop = true;
			this.rbKeep.Text = "原名";
			this.rbKeep.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 48);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 12);
			this.label1.TabIndex = 11;
			this.label1.Text = "重命名：";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 23);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(65, 12);
			this.label6.TabIndex = 8;
			this.label6.Text = "目标编码：";
			// 
			// cbDstEncoding
			// 
			this.cbDstEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbDstEncoding.FormattingEnabled = true;
			this.cbDstEncoding.Items.AddRange(new object[] {
            "UTF-8",
            "GB2312",
            "ASCII"});
			this.cbDstEncoding.Location = new System.Drawing.Point(77, 20);
			this.cbDstEncoding.Name = "cbDstEncoding";
			this.cbDstEncoding.Size = new System.Drawing.Size(150, 20);
			this.cbDstEncoding.TabIndex = 5;
			// 
			// btnTrans
			// 
			this.btnTrans.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnTrans.Location = new System.Drawing.Point(383, 371);
			this.btnTrans.Name = "btnTrans";
			this.btnTrans.Size = new System.Drawing.Size(109, 23);
			this.btnTrans.TabIndex = 11;
			this.btnTrans.Text = "开始转换(&T)";
			this.btnTrans.UseVisualStyleBackColor = true;
			this.btnTrans.Click += new System.EventHandler(this.btnTrans_Click);
			// 
			// TxtEncoding
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(760, 428);
			this.Controls.Add(this.btnTrans);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.treeListView1);
			this.Controls.Add(this.statusStrip1);
			this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(768, 462);
			this.Name = "TxtEncoding";
			this.TabText = "文本文件编码转换";
			this.Text = "文本文件编码转换";
			this.Activated += new System.EventHandler(this.TxtEncoding_Activated);
			this.Deactivate += new System.EventHandler(this.TxtEncoding_Deactivate);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TxtEncoding_FormClosing);
			this.Load += new System.EventHandler(this.TxtEncoding_Load);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.TreeListView treeListView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cbSrcEncoding;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cbDefaultEncoding;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox cbDstEncoding;
		private System.Windows.Forms.Button btnTrans;
		private System.Windows.Forms.ToolStripStatusLabel tsslStatus;
		private System.Windows.Forms.ToolStripProgressBar tspb;
		private System.Windows.Forms.TextBox txtCustomer;
		private System.Windows.Forms.RadioButton rbCustomer;
		private System.Windows.Forms.RadioButton rbEncodeName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox cbContainsChildren;
		private System.Windows.Forms.RadioButton rbKeep;
		private System.Windows.Forms.TextBox txtExt;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ImageList imgList;
	}
}