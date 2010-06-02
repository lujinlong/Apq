using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using System.Drawing;

namespace Apq.Xtra.TreeList
{
	/// <summary>
	/// 树公用功能
	/// </summary>
	public class Common
	{
		#region Behaivor
		/// <summary>
		/// 常用扩展,添加常用功能
		/// </summary>
		public static void AddBehaivor(DevExpress.XtraTreeList.TreeList tl)
		{
			#region TreeList
			tl.MouseDown += new MouseEventHandler(tl_MouseDown);
			tl.KeyUp += new KeyEventHandler(tl_KeyUp);
			#endregion
		}

		/// <summary>
		/// 常用扩展,去除常用功能
		/// </summary>
		public static void RemoveBehaivor(DevExpress.XtraTreeList.TreeList tl)
		{
			#region GridView
			tl.MouseDown -= new MouseEventHandler(tl_MouseDown);
			tl.KeyUp -= new KeyEventHandler(tl_KeyUp);
			#endregion
		}
		#endregion

		#region TreeList Event
		private static void tl_KeyUp(object sender, KeyEventArgs e)
		{
			DevExpress.XtraTreeList.TreeList tl = sender as DevExpress.XtraTreeList.TreeList;
			if (tl != null)
			{
				#region Ctrl&+
				if (e.Control && (e.KeyCode == Keys.Add))
				{
					tl.BestFitColumns();
				}
				#endregion
			}
		}

		private static void tl_MouseDown(object sender, MouseEventArgs e)
		{
			DevExpress.XtraTreeList.TreeList tl = sender as DevExpress.XtraTreeList.TreeList;
			if (tl != null && (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right))
			{
				TreeListHitInfo HitInfo = tl.CalcHitInfo(new Point(e.X, e.Y));
				if (HitInfo.Node != null)
				{
					tl.FocusedNode = HitInfo.Node;
				}
			}
		}
		#endregion
	}
}
