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
	/// 1.选择目标
	/// {
	///		//内存DataSet,
	///		Excel,
	///		Text,
	/// }
	/// </summary>
	public partial class DTSSelectOut : DTSRoot
	{
		/// <summary>
		/// 1.选择目标
		/// </summary>
		/// <param name="ds">DataSet</param>
		/// <param name="DataSetMapping">DataSet 映射</param>
		/// <param name="Direct">方向</param>
		public DTSSelectOut(DataSet ds, Direction Direct, DataTableMappingCollection DataSetMapping)
			: base(ds, Direct, DataSetMapping)
		{
			DTSDirection = Direction.Export;
			StepIndex = 1;

			InitializeComponent();
		}

		private void DTSSelect_Load(object sender, EventArgs e)
		{
			if (comboBoxEdit1.SelectedIndex > -1)
			{
				comboBoxEdit1_SelectedIndexChanged(comboBoxEdit1, e);
			}
		}

		private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxEdit1.SelectedIndex == 0)
			{
				panel1.Controls.Clear();
			}

			// 根据选择的项加载不同的控件
			if (comboBoxEdit1.SelectedIndex == 1)
			{	// Excel
				ToExcel te = new ToExcel(ds, DTSDirection, DataSetMapping);
			}

			if (comboBoxEdit1.SelectedIndex == 2)
			{	// Text

			}
		}
	}
}
