using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apq.Windows.Forms
{
	/// <summary>
	/// DataGridView助手
	/// </summary>
	public class DataGridViewHelper
	{
		/// <summary>
		/// 返回DataGridView列中是否包含指定列名
		/// </summary>
		public static bool ContainsHeader(System.Windows.Forms.DataGridView dgv, string ColHeaderName)
		{
			foreach (System.Windows.Forms.DataGridViewColumn dgvc in dgv.Columns)
			{
				if (dgvc.HeaderText == ColHeaderName)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// 返回指定列名的索引位置
		/// </summary>
		public static int IndexOfHeader(System.Windows.Forms.DataGridViewColumnCollection dgvcs, string ColHeaderName)
		{
			int idx = -1;

			for (int i = 0; i < dgvcs.Count; i++)
			{
				if (dgvcs[i].HeaderText == ColHeaderName)
				{
					idx = i;
					break;
				}
			}
			return idx;
		}
	}
}
