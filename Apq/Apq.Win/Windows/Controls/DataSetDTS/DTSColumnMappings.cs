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
	/// 列映射设置
	/// </summary>
	public partial class DTSColumnMappings : DTSRoot
	{
		/// <summary>
		/// DTSColumnMappings
		/// </summary>
		/// <param name="ds">DataSet</param>
		/// <param name="DataSetMapping">DataSet 映射</param>
		/// <param name="Direct">方向</param>
		public DTSColumnMappings(DataSet ds, Direction Direct, DataTableMappingCollection DataSetMapping)
			: base(ds, Direct, DataSetMapping)
		{
			InitializeComponent();
		}
	}
}
