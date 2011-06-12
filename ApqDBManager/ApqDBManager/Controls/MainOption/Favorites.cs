using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ApqDBManager.Controls.MainOption
{
	public partial class Favorites : UserControl
	{
		public Favorites()
		{
			InitializeComponent();
		}

		public void InitValue(Apq.Config.ConfigChain cfg)
		{
			if (!Apq.Convert.HasMean(cbCFolder.Text))
			{
				cbCFolder.Text = cfg[this.GetType(), "CFolder"];
			}
		}

		public void Confirm(Apq.Config.ConfigChain cfg)
		{
			if (Apq.Convert.HasMean(cbCFolder.Text))
			{
				cfg[this.GetType(), "CFolder"] = cbCFolder.Text;
			}
			cfg.UserConfig.Save();
		}

		private void cbCFolder_DropDown(object sender, EventArgs e)
		{
			string strFullName = Apq.Convert.ChangeType<string>(cbCFolder.Text);
			if (!string.IsNullOrEmpty(strFullName)) fbdFavorites.SelectedPath = strFullName;
			if (fbdFavorites.ShowDialog(this) == DialogResult.OK)
			{
				cbCFolder.Text = fbdFavorites.SelectedPath;
			}
		}

		private void cbCFolder_Leave(object sender, EventArgs e)
		{
			string strFullName = Apq.Convert.ChangeType<string>(cbCFolder.Text);
			if (!string.IsNullOrEmpty(strFullName)) fbdFavorites.SelectedPath = strFullName;
		}
	}
}
