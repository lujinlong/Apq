using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Base.ViewInfo;
using System.Drawing;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.ViewInfo;

namespace Apq.Xtra.Grid
{
	/// <summary>
	/// 表格公用功能
	/// </summary>
	public class Common
	{
		#region 显示行号
		/// <summary>
		/// 显示行号
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public static void gv_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
		{
			if (e.Info.IsRowIndicator)
			{
				e.Info.DisplayText = (e.RowHandle + 1).ToString();
			}
		}
		#endregion

		#region Behaivor

		/// <summary>
		/// 常用扩展,添加常用功能
		/// </summary>
		/// <param name="bv"></param>
		public static void AddBehaivor(BaseView bv)
		{
			bv.GridControl.EditorKeyUp += new KeyEventHandler(GridControl_EditorKeyUp);

			#region BaseView
			bv.KeyUp += new KeyEventHandler(bv_KeyUp);
			#endregion
		}
		/// <summary>
		/// 常用扩展,添加常用功能
		/// </summary>
		/// <param name="gv"></param>
		public static void AddBehaivor(GridView gv)
		{
			#region BaseView
			BaseView bv = gv as BaseView;
			AddBehaivor(bv);
			#endregion

			#region GridView
			gv.MouseDown += new MouseEventHandler(gv_MouseDown);
			gv.KeyUp += new KeyEventHandler(gv_KeyUp);
			#endregion
		}

		/// <summary>
		/// 常用扩展,去除常用功能
		/// </summary>
		/// <param name="bv"></param>
		public static void RemoveBehaivor(BaseView bv)
		{
			bv.GridControl.EditorKeyUp -= new KeyEventHandler(GridControl_EditorKeyUp);

			#region BaseView
			bv.KeyUp -= new KeyEventHandler(bv_KeyUp);
			#endregion
		}
		/// <summary>
		/// 常用扩展,去除常用功能
		/// </summary>
		/// <param name="gv"></param>
		public static void RemoveBehaivor(GridView gv)
		{
			#region BaseView
			BaseView bv = gv as BaseView;
			RemoveBehaivor(bv);
			#endregion

			#region GridView
			gv.MouseDown -= new MouseEventHandler(gv_MouseDown);
			gv.KeyUp -= new KeyEventHandler(gv_KeyUp);
			#endregion
		}

		#endregion

		#region GridControl Event
		private static void GridControl_EditorKeyUp(object sender, KeyEventArgs e)
		{
			GridControl gc = sender as GridControl;
			if (gc != null && gc.IsFocused)
			{
				#region Ctrl&C
				if (e.Control && (e.KeyCode == Keys.C))
				{
					gc.MainView.CopyToClipboard();
				}
				#endregion
			}
		}
		#endregion

		#region BaseView Event
		private static void bv_KeyUp(object sender, KeyEventArgs e)
		{
			BaseView bv = sender as BaseView;
			if (bv != null && bv.GridControl.IsFocused)
			{
				#region Ctrl&C
				if (e.Control && (e.KeyCode == Keys.C))
				{
					bv.CopyToClipboard();
				}
				#endregion
			}
		}
		#endregion

		#region GridView Event
		private static void gv_KeyUp(object sender, KeyEventArgs e)
		{
			GridView bv = sender as GridView;
			if (bv != null && bv.GridControl.IsFocused)
			{
				#region Ctrl&+
				if (e.Control && (e.KeyCode == Keys.Add))
				{
					bv.BestFitColumns();
				}
				#endregion
			}
		}

		private static void gv_MouseDown(object sender, MouseEventArgs e)
		{
			GridView gv = sender as GridView;
			if (gv != null && e.Button == MouseButtons.Left)
			{
				GridHitInfo HitInfo = gv.CalcHitInfo(new Point(e.X, e.Y));
				if (HitInfo.InRow)
				{
					gv.FocusedRowHandle = HitInfo.RowHandle;
				}

				#region 全选
				int Xmin = 0;
				int Ymin = 0;
				int Xmax = gv.IndicatorWidth;
				int Ymax = gv.ColumnPanelRowHeight;

				// 计算 左上角 位置范围
				PropertyInfo pi = typeof(GridView).GetProperty("ViewInfo", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
				GridViewInfo vi = pi.GetValue(gv, null) as GridViewInfo;
				Xmin = vi.ViewRects.ColumnPanel.Location.X;
				Ymin = vi.ViewRects.ColumnPanel.Location.Y;
				Xmax = Xmin + vi.ViewRects.IndicatorWidth;
				Ymax = Ymin + vi.ViewRects.ColumnPanel.Height;

				// 允许多选且左键点击左上角时全选
				if (gv.OptionsSelection.MultiSelect
					&& Xmin < e.X && e.X < Xmax
					&& Ymin < e.Y && e.Y < Ymax
				)
				{
					gv.SelectAll();
				}
				#endregion
			}
		}
		#endregion

		#region GridView
		/// <summary>
		/// 显示时间
		/// </summary>
		/// <param name="gv"></param>
		public static void ShowTime(GridView gv)
		{
			// 查找 DateTime 列,增加一列显示时间
			for (int i = gv.Columns.Count - 1; i >= 0; i--)
			{
				if (gv.Columns[i].ColumnType == typeof(DateTime))
				{
					GridColumn gvCol = gv.Columns.Add();
					gvCol.Caption = gvCol.FieldName = gv.Columns[i].FieldName;
					gvCol.VisibleIndex = i + 1;
					gvCol.ColumnEdit = new RepositoryItemTimeEdit();
				}
			}
		}
		#endregion
	}
}
