using Apq_LocalTools.Forms;
namespace Apq_LocalTools
{
	partial class FSRename
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
			System.Windows.Forms.TreeListViewItemCollection.TreeListViewItemCollectionComparer treeListViewItemCollectionComparer1 = new System.Windows.Forms.TreeListViewItemCollection.TreeListViewItemCollectionComparer();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FSRename));
			this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.tspb = new System.Windows.Forms.ToolStripProgressBar();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.cbMatchType = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnTrans = new System.Windows.Forms.Button();
			this.txtReplace = new System.Windows.Forms.TextBox();
			this.txtLook = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.txtExt = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.cbContainsFileExt = new System.Windows.Forms.CheckBox();
			this.cbContainsFolder = new System.Windows.Forms.CheckBox();
			this.cbRecursive = new System.Windows.Forms.CheckBox();
			this.fsExplorer1 = new Apq.TreeListView.FSExplorer();
			this.statusStrip1.SuspendLayout();
			this.groupBox1.SuspendLayout();
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
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 158);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 12);
			this.label2.TabIndex = 6;
			this.label2.Text = "匹配方式：";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 33);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(65, 12);
			this.label3.TabIndex = 8;
			this.label3.Text = "查找内容：";
			// 
			// cbMatchType
			// 
			this.cbMatchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbMatchType.FormattingEnabled = true;
			this.cbMatchType.Items.AddRange(new object[] {
            "普通",
            "正则表达式"});
			this.cbMatchType.Location = new System.Drawing.Point(77, 155);
			this.cbMatchType.Name = "cbMatchType";
			this.cbMatchType.Size = new System.Drawing.Size(168, 20);
			this.cbMatchType.TabIndex = 7;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox1.Controls.Add(this.btnTrans);
			this.groupBox1.Controls.Add(this.txtReplace);
			this.groupBox1.Controls.Add(this.txtLook);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.txtExt);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.cbMatchType);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.cbContainsFileExt);
			this.groupBox1.Controls.Add(this.cbContainsFolder);
			this.groupBox1.Controls.Add(this.cbRecursive);
			this.groupBox1.Location = new System.Drawing.Point(12, 191);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(601, 212);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "查找和替换";
			// 
			// btnTrans
			// 
			this.btnTrans.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnTrans.Location = new System.Drawing.Point(454, 167);
			this.btnTrans.Name = "btnTrans";
			this.btnTrans.Size = new System.Drawing.Size(109, 23);
			this.btnTrans.TabIndex = 11;
			this.btnTrans.Text = "开始替换(&H)";
			this.btnTrans.UseVisualStyleBackColor = true;
			this.btnTrans.Click += new System.EventHandler(this.btnTrans_Click);
			// 
			// txtReplace
			// 
			this.txtReplace.Location = new System.Drawing.Point(77, 62);
			this.txtReplace.Name = "txtReplace";
			this.txtReplace.Size = new System.Drawing.Size(201, 21);
			this.txtReplace.TabIndex = 21;
			// 
			// txtLook
			// 
			this.txtLook.Location = new System.Drawing.Point(77, 30);
			this.txtLook.Name = "txtLook";
			this.txtLook.Size = new System.Drawing.Size(201, 21);
			this.txtLook.TabIndex = 20;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 65);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(53, 12);
			this.label6.TabIndex = 19;
			this.label6.Text = "替换为：";
			// 
			// txtExt
			// 
			this.txtExt.Location = new System.Drawing.Point(401, 30);
			this.txtExt.Name = "txtExt";
			this.txtExt.Size = new System.Drawing.Size(180, 21);
			this.txtExt.TabIndex = 17;
			this.txtExt.Text = "*.txt;*.sql;*.xml;*.ini;";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(330, 33);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(65, 12);
			this.label5.TabIndex = 16;
			this.label5.Text = "文件类型：";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.ForeColor = System.Drawing.Color.DarkGreen;
			this.label4.Location = new System.Drawing.Point(75, 188);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(125, 12);
			this.label4.TabIndex = 9;
			this.label4.Text = "两种方式均为匹配整串";
			// 
			// cbContainsFileExt
			// 
			this.cbContainsFileExt.AutoSize = true;
			this.cbContainsFileExt.Checked = true;
			this.cbContainsFileExt.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbContainsFileExt.Location = new System.Drawing.Point(401, 108);
			this.cbContainsFileExt.Name = "cbContainsFileExt";
			this.cbContainsFileExt.Size = new System.Drawing.Size(108, 16);
			this.cbContainsFileExt.TabIndex = 9;
			this.cbContainsFileExt.Text = "包含文件扩展名";
			this.cbContainsFileExt.UseVisualStyleBackColor = true;
			// 
			// cbContainsFolder
			// 
			this.cbContainsFolder.AutoSize = true;
			this.cbContainsFolder.Checked = true;
			this.cbContainsFolder.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbContainsFolder.Location = new System.Drawing.Point(401, 86);
			this.cbContainsFolder.Name = "cbContainsFolder";
			this.cbContainsFolder.Size = new System.Drawing.Size(84, 16);
			this.cbContainsFolder.TabIndex = 9;
			this.cbContainsFolder.Text = "包含文件夹";
			this.cbContainsFolder.UseVisualStyleBackColor = true;
			// 
			// cbRecursive
			// 
			this.cbRecursive.AutoSize = true;
			this.cbRecursive.Checked = true;
			this.cbRecursive.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbRecursive.Location = new System.Drawing.Point(401, 64);
			this.cbRecursive.Name = "cbRecursive";
			this.cbRecursive.Size = new System.Drawing.Size(84, 16);
			this.cbRecursive.TabIndex = 9;
			this.cbRecursive.Text = "包含子目录";
			this.cbRecursive.UseVisualStyleBackColor = true;
			// 
			// fsExplorer1
			// 
			this.fsExplorer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fsExplorer1.CheckBoxes = System.Windows.Forms.CheckBoxesTypes.Recursive;
			treeListViewItemCollectionComparer1.Column = 2;
			treeListViewItemCollectionComparer1.SortOrder = System.Windows.Forms.SortOrder.Ascending;
			this.fsExplorer1.Comparer = treeListViewItemCollectionComparer1;
			this.fsExplorer1.Location = new System.Drawing.Point(0, 0);
			this.fsExplorer1.Name = "fsExplorer1";
			this.fsExplorer1.Size = new System.Drawing.Size(760, 182);
			this.fsExplorer1.TabIndex = 10;
			this.fsExplorer1.UseCompatibleStateImageBehavior = false;
			this.fsExplorer1.SelectedIndexChanged += new System.EventHandler(this.fsExplorer1_SelectedIndexChanged);
			// 
			// FSRename
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(760, 428);
			this.Controls.Add(this.fsExplorer1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.statusStrip1);
			this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(768, 462);
			this.Name = "FSRename";
			this.TabText = "批量重命名";
			this.Text = "批量重命名";
			this.Activated += new System.EventHandler(this.TxtEncoding_Activated);
			this.Deactivate += new System.EventHandler(this.TxtEncoding_Deactivate);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TxtEncoding_FormClosing);
			this.Load += new System.EventHandler(this.TxtEncoding_Load);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cbMatchType;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnTrans;
		private System.Windows.Forms.ToolStripStatusLabel tsslStatus;
		private System.Windows.Forms.ToolStripProgressBar tspb;
		private System.Windows.Forms.CheckBox cbRecursive;
		private System.Windows.Forms.TextBox txtReplace;
		private System.Windows.Forms.TextBox txtLook;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtExt;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox cbContainsFileExt;
		private System.Windows.Forms.CheckBox cbContainsFolder;
		private Apq.TreeListView.FSExplorer fsExplorer1;
	}
}