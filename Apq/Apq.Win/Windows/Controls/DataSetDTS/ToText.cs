using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;

namespace Apq.Windows.Controls.DataSetDTS
{
	/// <summary>
	/// Text
	/// </summary>
	public partial class ToText : DTSRoot
	{
		/// <summary>
		/// Text1
		/// </summary>
		/// <param name="ds">DataSet</param>
		/// <param name="DataSetMapping">DataSet 映射</param>
		/// <param name="Direct">方向</param>
		public ToText(DataSet ds, Direction Direct, DataTableMappingCollection DataSetMapping)
			: base(ds, Direct, DataSetMapping)
		{
			InitializeComponent();
			StepIndex = 2;
		}

		private void Text1_Load(object sender, EventArgs e)
		{

		}

		private void beFile_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			if (beFile.Text.Trim().Length > 0)
			{
				openFileDialog1.FileName = beFile.Text.Trim();
				saveFileDialog1.FileName = beFile.Text.Trim();
			}
			if (DTSDirection == Direction.Export)
			{
				saveFileDialog1.ShowDialog(this);
				beFile.Text = saveFileDialog1.FileName;
			}
			else if (DTSDirection == Direction.Import)
			{
				openFileDialog1.ShowDialog(this);
				beFile.Text = openFileDialog1.FileName;
			}
		}

		private void beFile_EditValueChanged(object sender, EventArgs e)
		{
			bool UIEnabled = beFile.Text.ToString() != string.Empty;

			cbeFileEncoding.Enabled = UIEnabled;
			teSpliterCol.Enabled = UIEnabled;
			teSpliterRow.Enabled = UIEnabled;
			ceColName.Enabled = UIEnabled;
		}
	}
}
