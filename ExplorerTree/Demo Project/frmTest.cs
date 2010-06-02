using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using WindowsExplorer;
namespace TestExplorer
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class frmTest : System.Windows.Forms.Form
	{
		private Microsoft.VisualBasic.Compatibility.VB6.FileListBox fileListBox1;
		private System.Windows.Forms.ComboBox cmbSelect;
		private WindowsExplorer.ExplorerTree explorerTree2;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem mnuSA;
		private System.Windows.Forms.MenuItem mnuST;
		private System.Windows.Forms.MenuItem mnuSMD;
		private System.Windows.Forms.MenuItem mnuSMN;
		private System.Windows.Forms.MenuItem mnuMF;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private IContainer components;

		public frmTest()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTest));
			this.fileListBox1 = new Microsoft.VisualBasic.Compatibility.VB6.FileListBox();
			this.cmbSelect = new System.Windows.Forms.ComboBox();
			this.explorerTree2 = new WindowsExplorer.ExplorerTree();
			this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.mnuSA = new System.Windows.Forms.MenuItem();
			this.mnuST = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.mnuSMD = new System.Windows.Forms.MenuItem();
			this.mnuSMN = new System.Windows.Forms.MenuItem();
			this.mnuMF = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// fileListBox1
			// 
			this.fileListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.fileListBox1.FormattingEnabled = true;
			this.fileListBox1.Location = new System.Drawing.Point(494, 29);
			this.fileListBox1.Name = "fileListBox1";
			this.fileListBox1.Pattern = "*.*";
			this.fileListBox1.Size = new System.Drawing.Size(240, 700);
			this.fileListBox1.TabIndex = 1;
			// 
			// cmbSelect
			// 
			this.cmbSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cmbSelect.Items.AddRange(new object[] {
            "*.bmp;*.jpg;*.jpeg;*.gif",
            "*.doc;*.rtf;*.txt",
            "*.*"});
			this.cmbSelect.Location = new System.Drawing.Point(494, 3);
			this.cmbSelect.Name = "cmbSelect";
			this.cmbSelect.Size = new System.Drawing.Size(240, 20);
			this.cmbSelect.TabIndex = 2;
			this.cmbSelect.Text = "*.*";
			this.cmbSelect.SelectedIndexChanged += new System.EventHandler(this.cmbSelect_SelectedIndexChanged);
			// 
			// explorerTree2
			// 
			this.explorerTree2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.explorerTree2.BackColor = System.Drawing.Color.White;
			this.explorerTree2.Location = new System.Drawing.Point(0, 0);
			this.explorerTree2.Name = "explorerTree2";
			this.explorerTree2.SelectedPath = "C:\\Program Files";
			this.explorerTree2.ShowAddressbar = true;
			this.explorerTree2.ShowMyDocuments = true;
			this.explorerTree2.ShowMyFavorites = true;
			this.explorerTree2.ShowMyNetwork = true;
			this.explorerTree2.ShowToolbar = true;
			this.explorerTree2.Size = new System.Drawing.Size(494, 759);
			this.explorerTree2.TabIndex = 3;
			this.explorerTree2.PathChanged += new WindowsExplorer.ExplorerTree.PathChangedEventHandler(this.explorerTree2_PathChanged);
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuSA,
            this.mnuST,
            this.menuItem4,
            this.mnuSMD,
            this.mnuSMN,
            this.mnuMF,
            this.menuItem2,
            this.menuItem3});
			this.menuItem1.Text = "Options";
			// 
			// mnuSA
			// 
			this.mnuSA.Checked = true;
			this.mnuSA.Index = 0;
			this.mnuSA.Text = "Show Addressbar";
			this.mnuSA.Click += new System.EventHandler(this.mnuSA_Click);
			// 
			// mnuST
			// 
			this.mnuST.Checked = true;
			this.mnuST.Index = 1;
			this.mnuST.Text = "Show Toolbar";
			this.mnuST.Click += new System.EventHandler(this.mnuST_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 2;
			this.menuItem4.Text = "-";
			// 
			// mnuSMD
			// 
			this.mnuSMD.Checked = true;
			this.mnuSMD.Index = 3;
			this.mnuSMD.Text = "Show My Documents";
			this.mnuSMD.Click += new System.EventHandler(this.mnuSMD_Click);
			// 
			// mnuSMN
			// 
			this.mnuSMN.Checked = true;
			this.mnuSMN.Index = 4;
			this.mnuSMN.Text = "Show My Networks";
			this.mnuSMN.Click += new System.EventHandler(this.mnuSMN_Click);
			// 
			// mnuMF
			// 
			this.mnuMF.Checked = true;
			this.mnuMF.Index = 5;
			this.mnuMF.Text = "Show My Favorites";
			this.mnuMF.Click += new System.EventHandler(this.mnuMF_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 6;
			this.menuItem2.Text = "-";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 7;
			this.menuItem3.Text = "About";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// frmTest
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(744, 757);
			this.Controls.Add(this.explorerTree2);
			this.Controls.Add(this.cmbSelect);
			this.Controls.Add(this.fileListBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenu1;
			this.MinimizeBox = false;
			this.Name = "frmTest";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Windows Explorer Test";
			this.Load += new System.EventHandler(this.frmTest_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new frmTest());
		}

		

		private void cmbSelect_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fileListBox1.Pattern = cmbSelect.Text ;  
		}

		
	

		private void explorerTree2_PathChanged(object sender, System.EventArgs e)
		{
			fileListBox1.Path = explorerTree2.SelectedPath;   
		
		}

		private void mnuSA_Click(object sender, System.EventArgs e)
		{
			mnuSA.Checked = ! (mnuSA.Checked);

			explorerTree2.ShowAddressbar = mnuSA.Checked;
			explorerTree2.refreshView();

		}

		private void mnuST_Click(object sender, System.EventArgs e)
		{
			mnuST.Checked = ! (mnuST.Checked);

			explorerTree2.ShowToolbar  = mnuST.Checked;
			explorerTree2.refreshView();
		}

		private void mnuSMD_Click(object sender, System.EventArgs e)
		{
			mnuSMD.Checked = ! (mnuSMD.Checked);

			explorerTree2.ShowMyDocuments = mnuSMD.Checked;
			explorerTree2.refreshFolders();
		}

		private void mnuSMN_Click(object sender, System.EventArgs e)
		{
			mnuSMN.Checked = ! (mnuSMN.Checked);
			explorerTree2.ShowMyNetwork = mnuSMN.Checked;
			explorerTree2.refreshFolders();
		}

		private void mnuMF_Click(object sender, System.EventArgs e)
		{
			mnuMF.Checked = ! (mnuMF.Checked);
			explorerTree2.ShowMyFavorites = mnuMF.Checked;
			explorerTree2.refreshFolders();
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			explorerTree2.AboutExplorerTree();
		}

		private void frmTest_Load(object sender, System.EventArgs e)
		{
			explorerTree2.setCurrentPath(@"C:\Program Files" );
			explorerTree2.btnGo_Click(this,e);

 
		}

		
		
	}
}
