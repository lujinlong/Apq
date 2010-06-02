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
	/// 2.选择表
	/// </summary>
	public partial class FromDataSet : DTSRoot
	{
		/// <summary>
		/// 2.选择表
		/// </summary>
		/// <param name="ds">DataSet</param>
		/// <param name="DataSetMapping">DataSet 映射</param>
		/// <param name="Direct">方向</param>
		public FromDataSet(DataSet ds, Direction Direct, DataTableMappingCollection DataSetMapping)
			: base(ds, Direct, DataSetMapping)
		{
			StepIndex = 2;
			this.ds = ds;

			InitializeComponent();
		}

		/// <summary>
		/// 获取或设置引用的数据集
		/// </summary>
		public System.Data.DataSet DS
		{
			get { return base.ds; }
			set
			{
				base.ds = value;

				dsTableName.Clear();
				foreach (DataTable dt in base.ds.Tables)
				{
					DataRow dr = dsTableName.Tables[0].NewRow();
					dr["Name"] = dt.TableName;
					dsTableName.Tables[0].Rows.Add(dr);
				}

				dsTableName.AcceptChanges();
			}
		}

		private void DataSet_Load(object sender, EventArgs e)
		{

		}
	}
}
