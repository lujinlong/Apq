using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Apq.Windows.Controls
{
	/// <summary>
	/// FolderBrowserDialog
	/// </summary>
	[DefaultProperty("SelectedPath")]
	public partial class FolderBrowser : UserControl
	{
		/// <summary>
		/// 
		/// </summary>
		public FolderBrowser()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 选择的路径
		/// </summary>
		[Category("数据")]
		public string SelectedPath
		{
			get
			{
				return FolderBrowserDialog.SelectedPath;
			}
			set
			{
				FolderBrowserDialog.SelectedPath = value;
			}
		}

		/// <summary>
		/// 选择的路径
		/// </summary>
		[Category("数据")]
		public string Value
		{
			get
			{
				return txtPath.Text;
			}
			set
			{
				txtPath.Text = value;
			}
		}

		private void FolderBrowser_Load(object sender, EventArgs e)
		{
			txtPath.DataBindings.Add("Text", this.FolderBrowserDialog, "SelectedPath");

			string cfgSelectedPath = Apq.Win.GlobalObject.XmlConfigChain[this.GetType(), "SelectedPath"];
			if (cfgSelectedPath != null)
			{
				FolderBrowserDialog.SelectedPath = cfgSelectedPath;
			}
		}

		private void btnPath_Click(object sender, EventArgs e)
		{
			if (FolderBrowserDialog.ShowDialog(this) == DialogResult.OK)
			{
				txtPath.Text = FolderBrowserDialog.SelectedPath;
			}
		}

		private void txtPath_TextChanged(object sender, EventArgs e)
		{
			Apq.Win.GlobalObject.XmlUserConfig.SetValue("SelectedPath", txtPath.Text);
		}
	}
}