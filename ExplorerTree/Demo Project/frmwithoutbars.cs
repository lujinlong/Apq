using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace TestExplorer
{
	/// <summary>
	/// Summary description for withoutbars.
	/// </summary>
	public class withoutbars : System.Windows.Forms.Form
	{
		private WindowsExplorer.ExplorerTree explorerTree1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public withoutbars()
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
				if(components != null)
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
			this.explorerTree1 = new WindowsExplorer.ExplorerTree();
			this.SuspendLayout();
			// 
			// explorerTree1
			// 
			this.explorerTree1.BackColor = System.Drawing.Color.White;
			this.explorerTree1.Location = new System.Drawing.Point(16, 16);
			this.explorerTree1.Name = "explorerTree1";
			this.explorerTree1.SelectedPath = "C:\\Program Files\\Microsoft Visual Studio .NET 2003\\Common7\\IDE";
			this.explorerTree1.ShowAddressbar = false;
			this.explorerTree1.ShowMyDocuments = true;
			this.explorerTree1.ShowMyFavorites = true;
			this.explorerTree1.ShowMyNetwork = true;
			this.explorerTree1.ShowToolbar = false;
			this.explorerTree1.Size = new System.Drawing.Size(240, 224);
			this.explorerTree1.TabIndex = 0;
			// 
			// withoutbars
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(312, 278);
			this.Controls.Add(this.explorerTree1);
			this.Name = "withoutbars";
			this.Text = "Form 2";
			this.ResumeLayout(false);

		}
		#endregion
	}
}
