using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apq.Windows.Forms
{
	/// <summary>
	/// ListView助手
	/// </summary>
	public class ListViewHelper
	{
		/// <summary>
		/// 返回ListView列中是否包含指定列名
		/// </summary>
		public static bool ContainsHeader(System.Windows.Forms.ListView lv, string ColHeaderName)
		{
			foreach (System.Windows.Forms.ColumnHeader ch in lv.Columns)
			{
				if (ch.Text == ColHeaderName)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// 返回指定列名的索引位置
		/// </summary>
		public static int IndexOfHeader(System.Windows.Forms.ListView.ColumnHeaderCollection chc, string ColHeaderName)
		{
			int idx = -1;

			for (int i = 0; i < chc.Count; i++)
			{
				if (chc[i].Text == ColHeaderName)
				{
					idx = i;
					break;
				}
			}
				return idx;
		}
	}
}
