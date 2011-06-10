using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonTools;
using System.Data;

namespace Apq.Windows.Controls
{
	/// <summary>
	/// 多列树助手
	/// </summary>
	public class TreeWithColumnHelper
	{
		/// <summary>
		/// 绑定数据表
		/// </summary>
		public static void BindDataTable(TreeListView tree, DataTable dt, string Key, string Parent)
		{
			tree.Nodes.Clear();

			List<DataRow> Roots = Apq.Data.DataTable.GetRootRows(dt, Key, Parent);
			foreach (DataRow dr in Roots)
			{
				Node node = new Node();
				tree.Nodes.Add(node);
				BindRow(tree, node, dr);
				BindChildren(tree, node, dt, dr, Key, Parent);
			}
		}

		/// <summary>
		/// 绑定指定行到指定节点
		/// </summary>
		public static void BindRow(TreeListView tree, Node node, DataRow dr)
		{
			foreach (DataColumn dc in dr.Table.Columns)
			{
				if (ContainsColumn(tree, dc.ColumnName))
				{
					node[dc.ColumnName] = dr[dc];
				}
			}
		}

		/// <summary>
		/// 绑定指定行的子级
		/// </summary>
		public static void BindChildren(TreeListView tree, Node node, DataTable dt, DataRow dr, string Key, string Parent)
		{
			string strParentSqlValue = Apq.Data.SqlClient.Common.ConvertToSqlON(dr[Key]);
			DataRow[] drs = dt.Select(Parent + " = " + strParentSqlValue);
			if (!Apq.Convert.LikeDBNull(drs) && drs.Length > 0)
			{
				foreach (DataRow drChild in drs)
				{
					Node n = new Node();
					node.Nodes.Add(n);
					BindRow(tree, n, drChild);
					BindChildren(tree, n, dt, drChild, Key, Parent);
				}
			}
		}

		/// <summary>
		/// 返回树中是否包含指定列
		/// </summary>
		public static bool ContainsColumn(TreeListView tree, string FieldName)
		{
			foreach (TreeListColumn tlc in tree.Columns)
			{
				if (tlc.Fieldname == FieldName)
				{
					return true;
				}
			}
			return false;
		}
	}
}
