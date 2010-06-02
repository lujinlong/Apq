using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors;

namespace Apq.Windows.Controls.Grid
{
	/// <summary>
	/// DataSet 编辑器
	/// </summary>
	public partial class DataSetEditor : UserControl
	{
		/// <summary>
		/// DataSetEditor,请设置DataSet
		/// </summary>
		public DataSetEditor()
		{
			InitializeComponent();
		}

		private DataSet _DataSet;
		/// <summary>
		/// 获取或设置DataSet,设置将引发界面刷新
		/// </summary>
		public DataSet DataSet
		{
			get { return _DataSet; }
			set
			{
				_DataSet = value;

				// 2.更新表名列表,取消表选择
				ricbDTName.Items.Clear();
				foreach (DataTable dt in _DataSet.Tables)
				{
					ricbDTName.Items.Add(dt.TableName);
				}
				beiDTName.EditValue = string.Empty;

				// 3.获取第一个表名,设置当前编辑表
				if (_DataSet.Tables.Count > 0)
				{
					beiDTName.EditValue = _DataSet.Tables[0].TableName;
				}
			}
		}

		private void beiDTName_EditValueChanged(object sender, EventArgs e)
		{
			gridView1.Columns.Clear();
			if (!Apq.Convert.LikeDBNull(beiDTName.EditValue))
			{
				string strTableName = Apq.Convert.ChangeType<string>(beiDTName.EditValue);
				gridControl1.DataSource = _DataSet;
				gridControl1.DataMember = strTableName;
				Apq.Xtra.Grid.Common.ShowTime(gridView1);
			}
		}
	}
}
