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
	/// SaveFileDialog
	/// </summary>
	[DefaultProperty("FileName")]
	public partial class SaveFile : UserControl
	{
		/// <summary>
		/// SaveFile
		/// </summary>
		public SaveFile()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 选择的路径
		/// </summary>
		[Category("数据")]
		public string FileName
		{
			get
			{
				return beFileName.Text;
			}
			set
			{
				beFileName.Text = value;
			}
		}

		private void SaveFile_Load(object sender, EventArgs e)
		{
			SaveFileDialog.Filter = "文本文件(*.txt; *.csv; *.prn)|*.txt; *.csv; *.prn|Excel文件(*.xls;*.xl*)|*.xls;*.xl*|所有文件(*.*)|*.*";

			string cfgFileName = Apq.Win.GlobalObject.XmlConfigChain[this.GetType(), "FileName"];
			if (!string.IsNullOrEmpty(cfgFileName))
			{
				beFileName.Text = cfgFileName;
			}
		}

		private void beFileName_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			if (!string.IsNullOrEmpty(beFileName.Text))
			{
				SaveFileDialog.FileName = beFileName.Text;
			}
			if (SaveFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				beFileName.Text = SaveFileDialog.FileName;
			}
		}

		private void beFileName_EditValueChanged(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(beFileName.Text))
			{
				Apq.Win.GlobalObject.XmlUserConfig.SetValue("FileName", beFileName.Text);
			}
		}
	}
}