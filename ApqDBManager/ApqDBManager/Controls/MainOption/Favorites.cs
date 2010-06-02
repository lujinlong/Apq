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

		private void beCFolder_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			string strFullName = Apq.Convert.ChangeType<string>(beCFolder.EditValue);
			if (!string.IsNullOrEmpty(strFullName)) fbdFavorites.SelectedPath = strFullName;
			if (fbdFavorites.ShowDialog(this) == DialogResult.OK)
			{
				beCFolder.EditValue = fbdFavorites.SelectedPath;
			}
		}

		private void beCFolder_EditValueChanged(object sender, EventArgs e)
		{
			string strFullName = Apq.Convert.ChangeType<string>(beCFolder.EditValue);
			if (!string.IsNullOrEmpty(strFullName)) fbdFavorites.SelectedPath = strFullName;
		}

		public void InitValue(Apq.Config.ConfigChain cfg)
		{
			if (!Apq.Convert.HasMean(beCFolder.EditValue))
			{
				beCFolder.EditValue = cfg[this.GetType(), "CFolder"];
			}
		}

		public void Confirm(Apq.Config.ConfigChain cfg)
		{
			if (Apq.Convert.HasMean(beCFolder.EditValue))
			{
				cfg[this.GetType(), "CFolder"] = Apq.Convert.ChangeType<string>(beCFolder.EditValue);
			}
			cfg.UserConfig.Save();
		}
	}
}
