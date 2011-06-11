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
	}
}
