namespace ApqDBManager.Forms
{
	partial class MainOption
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
			this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
			this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
			this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
			this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
			this.nbiXmlServers = new DevExpress.XtraNavBar.NavBarItem();
			this.nbiFavorites = new DevExpress.XtraNavBar.NavBarItem();
			this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
			this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
			this.splitContainerControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
			this.SuspendLayout();
			// 
			// splitContainerControl1
			// 
			this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
			this.splitContainerControl1.Horizontal = false;
			this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
			this.splitContainerControl1.Name = "splitContainerControl1";
			this.splitContainerControl1.Panel1.Controls.Add(this.panelControl1);
			this.splitContainerControl1.Panel1.Controls.Add(this.navBarControl1);
			this.splitContainerControl1.Panel1.Text = "Panel1";
			this.splitContainerControl1.Panel2.Controls.Add(this.btnCancel);
			this.splitContainerControl1.Panel2.Controls.Add(this.btnConfirm);
			this.splitContainerControl1.Panel2.MinSize = 40;
			this.splitContainerControl1.Panel2.Text = "Panel2";
			this.splitContainerControl1.Size = new System.Drawing.Size(692, 466);
			this.splitContainerControl1.SplitterPosition = 44;
			this.splitContainerControl1.TabIndex = 0;
			this.splitContainerControl1.Text = "splitContainerControl1";
			// 
			// panelControl1
			// 
			this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelControl1.Location = new System.Drawing.Point(131, 0);
			this.panelControl1.Name = "panelControl1";
			this.panelControl1.Size = new System.Drawing.Size(561, 416);
			this.panelControl1.TabIndex = 3;
			// 
			// navBarControl1
			// 
			this.navBarControl1.ActiveGroup = this.navBarGroup1;
			this.navBarControl1.ContentButtonHint = null;
			this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Left;
			this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup1});
			this.navBarControl1.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.nbiXmlServers,
            this.nbiFavorites});
			this.navBarControl1.Location = new System.Drawing.Point(0, 0);
			this.navBarControl1.Name = "navBarControl1";
			this.navBarControl1.OptionsNavPane.ExpandedWidth = 164;
			this.navBarControl1.Size = new System.Drawing.Size(131, 416);
			this.navBarControl1.TabIndex = 2;
			this.navBarControl1.Text = "navBarControl1";
			// 
			// navBarGroup1
			// 
			this.navBarGroup1.Caption = "选项";
			this.navBarGroup1.Expanded = true;
			this.navBarGroup1.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiXmlServers),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiFavorites)});
			this.navBarGroup1.Name = "navBarGroup1";
			// 
			// nbiXmlServers
			// 
			this.nbiXmlServers.Caption = "服务器列表";
			this.nbiXmlServers.Name = "nbiXmlServers";
			this.nbiXmlServers.LinkPressed += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiXmlServers_LinkPressed);
			// 
			// nbiFavorites
			// 
			this.nbiFavorites.Caption = "收藏夹";
			this.nbiFavorites.Name = "nbiFavorites";
			this.nbiFavorites.LinkPressed += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiFavorites_LinkPressed);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(461, 8);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(87, 27);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "取消";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnConfirm
			// 
			this.btnConfirm.Location = new System.Drawing.Point(304, 8);
			this.btnConfirm.Name = "btnConfirm";
			this.btnConfirm.Size = new System.Drawing.Size(87, 27);
			this.btnConfirm.TabIndex = 0;
			this.btnConfirm.Text = "确定";
			this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
			// 
			// MainOption
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(692, 466);
			this.Controls.Add(this.splitContainerControl1);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(700, 500);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(700, 500);
			this.Name = "MainOption";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "选项";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainOption_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
			this.splitContainerControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
		private DevExpress.XtraEditors.PanelControl panelControl1;
		private DevExpress.XtraNavBar.NavBarControl navBarControl1;
		private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
		private DevExpress.XtraEditors.SimpleButton btnCancel;
		private DevExpress.XtraEditors.SimpleButton btnConfirm;
		private DevExpress.XtraNavBar.NavBarItem nbiXmlServers;
		private DevExpress.XtraNavBar.NavBarItem nbiFavorites;




	}
}