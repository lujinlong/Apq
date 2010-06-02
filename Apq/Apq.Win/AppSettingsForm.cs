using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Reflection;

namespace Apq.Configuration
{
	/// <summary>
	/// 设置窗口
	/// </summary>
	public partial class AppSettingsForm : Apq.Windows.Forms.ImeForm
	{
		private System.Configuration.Configuration _Config;
		/// <summary>
		/// 获取配置文件
		/// </summary>
		public System.Configuration.Configuration Config
		{
			get { return _Config; }
		}

		/// <summary>
		/// 显示设置窗口
		/// </summary>
		public static void ShowForm(IWin32Window Parent, string ConfigPath)
		{
			AppSettingsForm sf = new AppSettingsForm(Apq.Configuration.Configs.GetConfig(ConfigPath));
			sf.ShowDialog(Parent);
		}
		/// <summary>
		/// 显示 Apq.Win.config 设置窗口
		/// </summary>
		public static void ShowForm(IWin32Window Parent)
		{
			ShowForm(Parent, Apq.Win.GlobalObject.TheAssembly.Location + ".config");
		}

		/// <summary>
		/// AppSettingsForm
		/// </summary>
		public AppSettingsForm(System.Configuration.Configuration config)
		{
			InitializeComponent();

			_Config = config;
		}

		private void AppSettingsForm_Load(object sender, EventArgs e)
		{
			Apq.Xtra.Grid.Common.AddBehaivor(gridView1);

			Text = System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetFileName(Config.FilePath)) + " 配置";

			foreach (KeyValueConfigurationElement elm in Config.AppSettings.Settings)
			{
				DataRow dr = dt.NewRow();
				dr["Name"] = elm.Key;
				dr["Value"] = elm.Value;
				dt.Rows.Add(dr);
			}
			dt.AcceptChanges();
			ds.AcceptChanges();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			Save();
		}

		private bool Save()
		{
			DataTable dtC = dt.GetChanges();
			if (dtC != null)
			{
				// 将设置更改保存到 [this].dll.config
				dtC = dt.GetChanges(DataRowState.Modified | DataRowState.Deleted);
				if (dtC != null)
				{
					foreach (DataRow dr in dtC.Rows)
					{
						Config.AppSettings.Settings.Remove(dr["Name", DataRowVersion.Original].ToString());
					}
				}
				dtC = dt.GetChanges(DataRowState.Modified | DataRowState.Added);
				if (dtC != null)
				{
					foreach (DataRow dr in dtC.Rows)
					{
						Config.AppSettings.Settings.Add(dr["Name"].ToString(), dr["Value"].ToString());
					}
				}

				Config.Save(ConfigurationSaveMode.Minimal);
				dt.AcceptChanges();
				ds.AcceptChanges();
				MessageBox.Show("保存成功");
				return true;
			}
			return false;
		}

		private void AppSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			DataTable dtC = dt.GetChanges();
			if (dtC != null)
			{
				switch (MessageBox.Show("设置已更改,是否保存?", "警告", MessageBoxButtons.YesNoCancel))
				{
					case DialogResult.Yes:
						if (!Save())
						{
							e.Cancel = true;
						}
						break;
					case DialogResult.Cancel:
						e.Cancel = true;
						break;
				}
			}
		}
	}
}