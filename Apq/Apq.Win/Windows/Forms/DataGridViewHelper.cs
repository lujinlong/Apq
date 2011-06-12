using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
		public static bool ContainsHeader(DataGridView dgv, string ColHeaderName)
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

		#region Behaivor

		/// <summary>
		/// 常用扩展,添加常用功能
		/// </summary>
		/// <param name="gv"></param>
		public static void AddBehaivor(DataGridView gv)
		{
			// DataError忽略
			gv.DataError += new DataGridViewDataErrorEventHandler(gv_DataError);

			gv.KeyUp += new KeyEventHandler(gv_KeyUp);
		}

		/// <summary>
		/// 常用扩展,去除常用功能
		/// </summary>
		/// <param name="gv"></param>
		public static void RemoveBehaivor(DataGridView gv)
		{
			gv.DataError -= new DataGridViewDataErrorEventHandler(gv_DataError);
			gv.KeyUp -= new KeyEventHandler(gv_KeyUp);
		}

		#endregion

		#region DataGridView 事件处理
		private static void gv_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			e.ThrowException = false;
		}

		private static void gv_KeyUp(object sender, KeyEventArgs e)
		{
			DataGridView gv = sender as DataGridView;
			if (gv != null && gv.Focused)
			{
				#region Ctrl C
				if (e.Control && (e.KeyCode == Keys.C))
				{
					Clipboard.SetDataObject(gv.GetClipboardContent());
				}
				#endregion
			}
		}
		#endregion
	}
}
