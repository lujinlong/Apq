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
			this.fbdCFTPFolder_Out = new System.Windows.Forms.FolderBrowserDialog();
			this.fbdCFolder_In = new System.Windows.Forms.FolderBrowserDialog();
			this._UI = new ApqDBManager.Forms.ErrList_XSD();
			this._Sqls = new ApqDBManager.Forms.SrvsMgr.SrvsMgr_XSD();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.cbDBFTPFolder_In = new System.Windows.Forms.ComboBox();
			this.cbCFTPFolder_Out = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnDistribute = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.cbCFTPFolder_In = new System.Windows.Forms.ComboBox();
			this.cbDBFTPFolder_Out = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.btnCollect = new System.Windows.Forms.Button();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.tspb = new System.Windows.Forms.ToolStripProgressBar();
			((System.ComponentModel.ISupportInitialize)(this._UI)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._Sqls)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
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
			this.groupBox1.Controls.Add(this.cbDBFTPFolder_In);
			this.groupBox1.Controls.Add(this.cbCFTPFolder_Out);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.btnDistribute);
			this.groupBox1.Location = new System.Drawing.Point(12, 27);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(688, 142);
			this.groupBox1.TabIndex = 33;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "文件分发";
			// 
			// cbDBFTPFolder_In
			// 
			this.cbDBFTPFolder_In.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cbDBFTPFolder_In.DropDownHeight = 1;
			this.cbDBFTPFolder_In.FormattingEnabled = true;
			this.cbDBFTPFolder_In.IntegralHeight = false;
			this.cbDBFTPFolder_In.Location = new System.Drawing.Point(101, 68);
			this.cbDBFTPFolder_In.Name = "cbDBFTPFolder_In";
			this.cbDBFTPFolder_In.Size = new System.Drawing.Size(582, 20);
			this.cbDBFTPFolder_In.TabIndex = 36;
			this.cbDBFTPFolder_In.Leave += new System.EventHandler(this.cbDBFTPFolder_In_Leave);
			// 
			// cbCFTPFolder_Out
			// 
			this.cbCFTPFolder_Out.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cbCFTPFolder_Out.DropDownHeight = 1;
			this.cbCFTPFolder_Out.FormattingEnabled = true;
			this.cbCFTPFolder_Out.IntegralHeight = false;
			this.cbCFTPFolder_Out.Location = new System.Drawing.Point(101, 41);
			this.cbCFTPFolder_Out.Name = "cbCFTPFolder_Out";
			this.cbCFTPFolder_Out.Size = new System.Drawing.Size(582, 20);
			this.cbCFTPFolder_Out.TabIndex = 35;
			this.cbCFTPFolder_Out.Leave += new System.EventHandler(this.cbCFTPFolder_Out_Leave);
			this.cbCFTPFolder_Out.DropDown += new System.EventHandler(this.cbCFTPFolder_Out_DropDown);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(10, 71);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(89, 12);
			this.label4.TabIndex = 34;
			this.label4.Text = "远程写入根目录";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(10, 44);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(89, 12);
			this.label3.TabIndex = 33;
			this.label3.Text = "本地读取根目录";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.Color.Coral;
			this.label1.Location = new System.Drawing.Point(6, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(587, 12);
			this.label1.TabIndex = 32;
			this.label1.Text = "[覆盖]分发规则:将本地根目录下按服务器命名的子级文件夹中的所有文件(不含子文件夹)上传到对应的服务器";
			// 
			// btnDistribute
			// 
			this.btnDistribute.Location = new System.Drawing.Point(101, 94);
			this.btnDistribute.Name = "btnDistribute";
			this.btnDistribute.Size = new System.Drawing.Size(75, 23);
			this.btnDistribute.TabIndex = 31;
			this.btnDistribute.Text = "分发";
			this.btnDistribute.UseVisualStyleBackColor = true;
			this.btnDistribute.Click += new System.EventHandler(this.btnDistribute_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.cbCFTPFolder_In);
			this.groupBox2.Controls.Add(this.cbDBFTPFolder_Out);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.btnCollect);
			this.groupBox2.Location = new System.Drawing.Point(12, 195);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(688, 142);
			this.groupBox2.TabIndex = 34;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "文件收集";
			// 
			// cbCFTPFolder_In
			// 
			this.cbCFTPFolder_In.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cbCFTPFolder_In.DropDownHeight = 1;
			this.cbCFTPFolder_In.FormattingEnabled = true;
			this.cbCFTPFolder_In.IntegralHeight = false;
			this.cbCFTPFolder_In.Location = new System.Drawing.Point(101, 70);
			this.cbCFTPFolder_In.Name = "cbCFTPFolder_In";
			this.cbCFTPFolder_In.Size = new System.Drawing.Size(582, 20);
			this.cbCFTPFolder_In.TabIndex = 38;
			this.cbCFTPFolder_In.Leave += new System.EventHandler(this.cbCFTPFolder_In_Leave);
			this.cbCFTPFolder_In.DropDown += new System.EventHandler(this.cbCFTPFolder_In_DropDown);
			// 
			// cbDBFTPFolder_Out
			// 
			this.cbDBFTPFolder_Out.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cbDBFTPFolder_Out.DropDownHeight = 1;
			this.cbDBFTPFolder_Out.FormattingEnabled = true;
			this.cbDBFTPFolder_Out.IntegralHeight = false;
			this.cbDBFTPFolder_Out.Location = new System.Drawing.Point(101, 44);
			this.cbDBFTPFolder_Out.Name = "cbDBFTPFolder_Out";
			this.cbDBFTPFolder_Out.Size = new System.Drawing.Size(582, 20);
			this.cbDBFTPFolder_Out.TabIndex = 37;
			this.cbDBFTPFolder_Out.Leave += new System.EventHandler(this.cbDBFTPFolder_Out_Leave);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(10, 47);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(89, 12);
			this.label5.TabIndex = 35;
			this.label5.Text = "远程读取根目录";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(10, 74);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(89, 12);
			this.label6.TabIndex = 36;
			this.label6.Text = "本地写入根目录";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.Color.Coral;
			this.label2.Location = new System.Drawing.Point(6, 17);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(563, 12);
			this.label2.TabIndex = 33;
			this.label2.Text = "[覆盖]收集规则:将远程根目录下的所有文件(不含子文件夹)下载到本地根目录下按服务器命名的文件夹中";
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

		private System.Windows.Forms.FolderBrowserDialog fbdCFTPFolder_Out;
		private System.Windows.Forms.FolderBrowserDialog fbdCFolder_In;
		private ErrList_XSD _UI;
		private ApqDBManager.Forms.SrvsMgr.SrvsMgr_XSD _Sqls;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel tsslStatus;
		private System.Windows.Forms.ToolStripProgressBar tspb;
		private System.Windows.Forms.Button btnDistribute;
		private System.Windows.Forms.Button btnCollect;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cbCFTPFolder_Out;
		private System.Windows.Forms.ComboBox cbDBFTPFolder_In;
		private System.Windows.Forms.ComboBox cbCFTPFolder_In;
		private System.Windows.Forms.ComboBox cbDBFTPFolder_Out;
	}
}