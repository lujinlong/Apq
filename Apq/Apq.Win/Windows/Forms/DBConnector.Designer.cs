namespace Apq.Windows.Forms
{
	partial class DBConnector
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.btnMySqlRefresh = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.cbMySqlDBName = new System.Windows.Forms.ComboBox();
			this.txtMySqlPort = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.cbMySqlSavePwd = new System.Windows.Forms.CheckBox();
			this.txtMySqlPwd = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.btnMySqlSaveName = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.cbMySqlName = new System.Windows.Forms.ComboBox();
			this.txtMySqlUserId = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtMySqlServer = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnMySqlCancel = new System.Windows.Forms.Button();
			this.btnMySqlConfirm = new System.Windows.Forms.Button();
			this.btnMySqlTest = new System.Windows.Forms.Button();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.btnMsSqlCancel = new System.Windows.Forms.Button();
			this.btnMsSqlConfirm = new System.Windows.Forms.Button();
			this.btnMsSqlTest = new System.Windows.Forms.Button();
			this._MySqlConnection = new MySql.Data.MySqlClient.MySqlConnection();
			this._SqlConnection = new System.Data.SqlClient.SqlConnection();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(433, 310);
			this.tabControl1.TabIndex = 0;
			this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.splitContainer1);
			this.tabPage1.Location = new System.Drawing.Point(4, 21);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(425, 285);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "MySql";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer1.IsSplitterFixed = true;
			this.splitContainer1.Location = new System.Drawing.Point(3, 3);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.btnMySqlRefresh);
			this.splitContainer1.Panel1.Controls.Add(this.label6);
			this.splitContainer1.Panel1.Controls.Add(this.cbMySqlDBName);
			this.splitContainer1.Panel1.Controls.Add(this.txtMySqlPort);
			this.splitContainer1.Panel1.Controls.Add(this.label5);
			this.splitContainer1.Panel1.Controls.Add(this.cbMySqlSavePwd);
			this.splitContainer1.Panel1.Controls.Add(this.txtMySqlPwd);
			this.splitContainer1.Panel1.Controls.Add(this.label4);
			this.splitContainer1.Panel1.Controls.Add(this.btnMySqlSaveName);
			this.splitContainer1.Panel1.Controls.Add(this.label3);
			this.splitContainer1.Panel1.Controls.Add(this.cbMySqlName);
			this.splitContainer1.Panel1.Controls.Add(this.txtMySqlUserId);
			this.splitContainer1.Panel1.Controls.Add(this.label2);
			this.splitContainer1.Panel1.Controls.Add(this.txtMySqlServer);
			this.splitContainer1.Panel1.Controls.Add(this.label1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.btnMySqlCancel);
			this.splitContainer1.Panel2.Controls.Add(this.btnMySqlConfirm);
			this.splitContainer1.Panel2.Controls.Add(this.btnMySqlTest);
			this.splitContainer1.Size = new System.Drawing.Size(419, 279);
			this.splitContainer1.SplitterDistance = 223;
			this.splitContainer1.TabIndex = 0;
			// 
			// btnMySqlRefresh
			// 
			this.btnMySqlRefresh.Enabled = false;
			this.btnMySqlRefresh.Location = new System.Drawing.Point(255, 163);
			this.btnMySqlRefresh.Name = "btnMySqlRefresh";
			this.btnMySqlRefresh.Size = new System.Drawing.Size(75, 23);
			this.btnMySqlRefresh.TabIndex = 6;
			this.btnMySqlRefresh.Text = "刷新(&F)";
			this.btnMySqlRefresh.UseVisualStyleBackColor = true;
			this.btnMySqlRefresh.Click += new System.EventHandler(this.btnMySqlRefresh_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(35, 168);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(77, 12);
			this.label6.TabIndex = 16;
			this.label6.Text = "数据库列表：";
			// 
			// cbMySqlDBName
			// 
			this.cbMySqlDBName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbMySqlDBName.FormattingEnabled = true;
			this.cbMySqlDBName.Location = new System.Drawing.Point(128, 165);
			this.cbMySqlDBName.Name = "cbMySqlDBName";
			this.cbMySqlDBName.Size = new System.Drawing.Size(121, 20);
			this.cbMySqlDBName.TabIndex = 7;
			this.cbMySqlDBName.SelectedIndexChanged += new System.EventHandler(this.cbMySqlDBName_SelectedIndexChanged);
			// 
			// txtMySqlPort
			// 
			this.txtMySqlPort.Location = new System.Drawing.Point(128, 138);
			this.txtMySqlPort.Name = "txtMySqlPort";
			this.txtMySqlPort.Size = new System.Drawing.Size(121, 21);
			this.txtMySqlPort.TabIndex = 4;
			this.txtMySqlPort.Text = "3306";
			this.txtMySqlPort.TextChanged += new System.EventHandler(this.txtMySqlPort_TextChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(71, 141);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(41, 12);
			this.label5.TabIndex = 13;
			this.label5.Text = "端口：";
			// 
			// cbMySqlSavePwd
			// 
			this.cbMySqlSavePwd.AutoSize = true;
			this.cbMySqlSavePwd.Checked = true;
			this.cbMySqlSavePwd.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbMySqlSavePwd.Location = new System.Drawing.Point(255, 113);
			this.cbMySqlSavePwd.Name = "cbMySqlSavePwd";
			this.cbMySqlSavePwd.Size = new System.Drawing.Size(72, 16);
			this.cbMySqlSavePwd.TabIndex = 5;
			this.cbMySqlSavePwd.Text = "保存密码";
			this.cbMySqlSavePwd.UseVisualStyleBackColor = true;
			this.cbMySqlSavePwd.CheckedChanged += new System.EventHandler(this.cbMySqlSavePwd_CheckedChanged);
			// 
			// txtMySqlPwd
			// 
			this.txtMySqlPwd.Location = new System.Drawing.Point(128, 111);
			this.txtMySqlPwd.Name = "txtMySqlPwd";
			this.txtMySqlPwd.PasswordChar = '*';
			this.txtMySqlPwd.Size = new System.Drawing.Size(121, 21);
			this.txtMySqlPwd.TabIndex = 3;
			this.txtMySqlPwd.TextChanged += new System.EventHandler(this.txtMySqlPwd_TextChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(71, 114);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(41, 12);
			this.label4.TabIndex = 10;
			this.label4.Text = "密码：";
			// 
			// btnMySqlSaveName
			// 
			this.btnMySqlSaveName.Enabled = false;
			this.btnMySqlSaveName.Location = new System.Drawing.Point(255, 29);
			this.btnMySqlSaveName.Name = "btnMySqlSaveName";
			this.btnMySqlSaveName.Size = new System.Drawing.Size(75, 23);
			this.btnMySqlSaveName.TabIndex = 8;
			this.btnMySqlSaveName.Text = "保存(&S)";
			this.btnMySqlSaveName.UseVisualStyleBackColor = true;
			this.btnMySqlSaveName.Click += new System.EventHandler(this.btnMySqlSaveName_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(71, 34);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 12);
			this.label3.TabIndex = 0;
			this.label3.Text = "名称：";
			// 
			// cbMySqlName
			// 
			this.cbMySqlName.FormattingEnabled = true;
			this.cbMySqlName.Location = new System.Drawing.Point(128, 31);
			this.cbMySqlName.Name = "cbMySqlName";
			this.cbMySqlName.Size = new System.Drawing.Size(121, 20);
			this.cbMySqlName.TabIndex = 0;
			this.cbMySqlName.SelectedIndexChanged += new System.EventHandler(this.cbMySqlName_SelectedIndexChanged);
			this.cbMySqlName.TextUpdate += new System.EventHandler(this.cbMySqlName_TextUpdate);
			// 
			// txtMySqlUserId
			// 
			this.txtMySqlUserId.Location = new System.Drawing.Point(128, 84);
			this.txtMySqlUserId.Name = "txtMySqlUserId";
			this.txtMySqlUserId.Size = new System.Drawing.Size(202, 21);
			this.txtMySqlUserId.TabIndex = 2;
			this.txtMySqlUserId.Text = "root";
			this.txtMySqlUserId.TextChanged += new System.EventHandler(this.txtMySqlUserId_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(59, 87);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 12);
			this.label2.TabIndex = 5;
			this.label2.Text = "用户名：";
			// 
			// txtMySqlServer
			// 
			this.txtMySqlServer.Location = new System.Drawing.Point(128, 57);
			this.txtMySqlServer.Name = "txtMySqlServer";
			this.txtMySqlServer.Size = new System.Drawing.Size(202, 21);
			this.txtMySqlServer.TabIndex = 1;
			this.txtMySqlServer.Text = "localhost";
			this.txtMySqlServer.TextChanged += new System.EventHandler(this.txtMySqlServer_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(59, 60);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 12);
			this.label1.TabIndex = 3;
			this.label1.Text = "服务器：";
			// 
			// btnMySqlCancel
			// 
			this.btnMySqlCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMySqlCancel.Location = new System.Drawing.Point(215, 14);
			this.btnMySqlCancel.Name = "btnMySqlCancel";
			this.btnMySqlCancel.Size = new System.Drawing.Size(75, 23);
			this.btnMySqlCancel.TabIndex = 1;
			this.btnMySqlCancel.Text = "取消(&C)";
			this.btnMySqlCancel.UseVisualStyleBackColor = true;
			this.btnMySqlCancel.Click += new System.EventHandler(this.btnMySqlCancel_Click);
			// 
			// btnMySqlConfirm
			// 
			this.btnMySqlConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMySqlConfirm.Location = new System.Drawing.Point(134, 14);
			this.btnMySqlConfirm.Name = "btnMySqlConfirm";
			this.btnMySqlConfirm.Size = new System.Drawing.Size(75, 23);
			this.btnMySqlConfirm.TabIndex = 0;
			this.btnMySqlConfirm.Text = "确定(&O)";
			this.btnMySqlConfirm.UseVisualStyleBackColor = true;
			this.btnMySqlConfirm.Click += new System.EventHandler(this.btnMySqlConfirm_Click);
			// 
			// btnMySqlTest
			// 
			this.btnMySqlTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMySqlTest.Location = new System.Drawing.Point(296, 14);
			this.btnMySqlTest.Name = "btnMySqlTest";
			this.btnMySqlTest.Size = new System.Drawing.Size(82, 23);
			this.btnMySqlTest.TabIndex = 2;
			this.btnMySqlTest.Text = "测试连接(&T)";
			this.btnMySqlTest.UseVisualStyleBackColor = true;
			this.btnMySqlTest.Click += new System.EventHandler(this.btnMySqlTest_Click);
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.splitContainer2);
			this.tabPage2.Location = new System.Drawing.Point(4, 21);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(425, 285);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "MsSql";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer2.IsSplitterFixed = true;
			this.splitContainer2.Location = new System.Drawing.Point(3, 3);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.btnMsSqlCancel);
			this.splitContainer2.Panel2.Controls.Add(this.btnMsSqlConfirm);
			this.splitContainer2.Panel2.Controls.Add(this.btnMsSqlTest);
			this.splitContainer2.Size = new System.Drawing.Size(419, 279);
			this.splitContainer2.SplitterDistance = 223;
			this.splitContainer2.TabIndex = 1;
			// 
			// btnMsSqlCancel
			// 
			this.btnMsSqlCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMsSqlCancel.Location = new System.Drawing.Point(215, 14);
			this.btnMsSqlCancel.Name = "btnMsSqlCancel";
			this.btnMsSqlCancel.Size = new System.Drawing.Size(75, 23);
			this.btnMsSqlCancel.TabIndex = 4;
			this.btnMsSqlCancel.Text = "取消(&C)";
			this.btnMsSqlCancel.UseVisualStyleBackColor = true;
			// 
			// btnMsSqlConfirm
			// 
			this.btnMsSqlConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMsSqlConfirm.Location = new System.Drawing.Point(134, 14);
			this.btnMsSqlConfirm.Name = "btnMsSqlConfirm";
			this.btnMsSqlConfirm.Size = new System.Drawing.Size(75, 23);
			this.btnMsSqlConfirm.TabIndex = 3;
			this.btnMsSqlConfirm.Text = "确定(&O)";
			this.btnMsSqlConfirm.UseVisualStyleBackColor = true;
			// 
			// btnMsSqlTest
			// 
			this.btnMsSqlTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMsSqlTest.Location = new System.Drawing.Point(296, 14);
			this.btnMsSqlTest.Name = "btnMsSqlTest";
			this.btnMsSqlTest.Size = new System.Drawing.Size(82, 23);
			this.btnMsSqlTest.TabIndex = 5;
			this.btnMsSqlTest.Text = "测试连接(&T)";
			this.btnMsSqlTest.UseVisualStyleBackColor = true;
			// 
			// _SqlConnection
			// 
			this._SqlConnection.FireInfoMessageEventOnUserErrors = false;
			// 
			// DBConnector
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(433, 310);
			this.ControlBox = false;
			this.Controls.Add(this.tabControl1);
			this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.HideOnClose = true;
			this.Name = "DBConnector";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.TabText = "语言设置";
			this.Text = "连接到数据库";
			this.Load += new System.EventHandler(this.DBConnector_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnMySqlTest;
        private System.Windows.Forms.Button btnMySqlCancel;
        private System.Windows.Forms.Button btnMySqlConfirm;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button btnMySqlSaveName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbMySqlName;
        private System.Windows.Forms.TextBox txtMySqlUserId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMySqlServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnMySqlRefresh;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbMySqlDBName;
        private System.Windows.Forms.TextBox txtMySqlPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbMySqlSavePwd;
        private System.Windows.Forms.TextBox txtMySqlPwd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnMsSqlCancel;
        private System.Windows.Forms.Button btnMsSqlConfirm;
        private System.Windows.Forms.Button btnMsSqlTest;
        private MySql.Data.MySqlClient.MySqlConnection _MySqlConnection;
        private System.Data.SqlClient.SqlConnection _SqlConnection;

    }
}