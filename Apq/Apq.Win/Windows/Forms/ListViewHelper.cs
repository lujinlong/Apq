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
		#region Behaivor

		/// <summary>
		/// 常用扩展,添加常用功能
		/// </summary>
		/// <param name="lv"></param>
		public static void AddBehaivor(System.Windows.Forms.ListView lv)
		{
			lv.KeyDown += new System.Windows.Forms.KeyEventHandler(lv_KeyDown);
			lv.KeyUp += new System.Windows.Forms.KeyEventHandler(lv_KeyUp);
		}

		/// <summary>
		/// 常用扩展,去除常用功能
		/// </summary>
		/// <param name="lv"></param>
		public static void RemoveBehaivor(System.Windows.Forms.ListView lv)
		{
			lv.KeyDown -= new System.Windows.Forms.KeyEventHandler(lv_KeyDown);
			lv.KeyUp -= new System.Windows.Forms.KeyEventHandler(lv_KeyUp);
		}

		#endregion

		#region ListView 事件处理
		private static void lv_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			System.Windows.Forms.ListView lv = sender as System.Windows.Forms.ListView;
			if (lv != null && lv.Focused)
			{
				#region Ctrl C
				if (e.Control && (e.KeyCode == System.Windows.Forms.Keys.C))
				{
					System.Collections.IList lst = lv.SelectedItems.Count > 0 ? (System.Collections.IList)lv.SelectedItems : (System.Collections.IList)lv.Items;
					List<string> lstStrs = new List<string>();
					foreach (System.Windows.Forms.ListViewItem lvi in lst)
					{
						lstStrs.Add(lvi.Text);
					}
					System.Windows.Forms.Clipboard.SetText(string.Join("\r\n", lstStrs.ToArray()));
				}
				#endregion
			}
		}

		private static void lv_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
		}
		#endregion
	}
}
