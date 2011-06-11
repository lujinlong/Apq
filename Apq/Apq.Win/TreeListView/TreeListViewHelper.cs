﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Apq.TreeListView
{
	/// <summary>
	/// TreeListView助手
	/// </summary>
	public class TreeListViewHelper
	{
		protected System.Windows.Forms.TreeListView _TreeListView;
		/// <summary>
		/// 获取TreeListView
		/// </summary>
		public System.Windows.Forms.TreeListView TreeListView
		{
			get { return _TreeListView; }
		}

		public TreeListViewHelper(System.Windows.Forms.TreeListView TreeListView)
		{
			_TreeListView = TreeListView;
		}

		private DataTableMapping _TableMapping = new DataTableMapping();
		/// <summary>
		/// 获取或设置映射表
		/// </summary>
		public DataTableMapping TableMapping
		{
			get
			{
				if (Apq.Convert.LikeDBNull(_TableMapping))
				{
					_TableMapping = new DataTableMapping();
				}
				return _TableMapping;
			}
			set { _TableMapping = value; }
		}
		/// <summary>
		/// 获取映射表
		/// </summary>
		public DataTableMapping GetTableMapping(DataTable dt)
		{
			if (TableMapping.ColumnMappings.Count == 0 && dt.Columns.Count > 0)
			{
				TableMapping = Apq.Data.DataTable.CreateDefaultTableMapping(dt);
			}

			return TableMapping;
		}

		/// <summary>
		/// 数据主键列名
		/// </summary>
		public string Key = "ID";
		/// <summary>
		/// 数据上级列名
		/// </summary>
		public string Parent = "ParentID";

		/// <summary>
		/// 绑定数据表
		/// </summary>
		public void BindDataTable(DataTable dt)
		{
			TreeListView.Items.Clear();

			List<DataRow> Roots = Apq.Data.DataTable.GetRootRows(dt, Key, Parent);
			foreach (DataRow dr in Roots)
			{
				System.Windows.Forms.TreeListViewItem node = new System.Windows.Forms.TreeListViewItem();
				TreeListView.Items.Add(node);
				BindRow(node, dr);
				BindChildren(node, dr);
			}
		}

		/// <summary>
		/// 绑定指定行到指定节点
		/// </summary>
		public void BindRow(System.Windows.Forms.TreeListViewItem node, DataRow dr)
		{
			DataTableMapping dtm = GetTableMapping(dr.Table);
			for (int i = 0; i < TreeListView.Columns.Count; i++)
			{
				string nodeHeaderName = TreeListView.Columns[i].Text;
				int cmIndex = dtm.ColumnMappings.IndexOf(nodeHeaderName);
				if (cmIndex > -1)
				{
					DataColumnMapping dcm = dtm.ColumnMappings[cmIndex];
					string dsColumnName = dcm.DataSetColumn;
					if (dr.Table.Columns.Contains(dsColumnName))
					{
						string str = Apq.Convert.ChangeType<string>(dr[dsColumnName], string.Empty);
						if (i == 0)
						{
							node.Text = str;
						}
						else
						{
							node.SubItems.Add(str);
						}
					}
					else
					{
						if (i > 0)
						{
							node.SubItems.Add(string.Empty);
						}
					}
				}
			}
		}

		/// <summary>
		/// 绑定指定行的子级
		/// </summary>
		public void BindChildren(System.Windows.Forms.TreeListViewItem node, DataRow dr)
		{
			DataTable dt = dr.Table;
			string strParentSqlValue = Apq.Data.SqlClient.Common.ConvertToSqlON(dr[Key]);
			DataRow[] drs = dt.Select(Parent + " = " + strParentSqlValue);
			if (!Apq.Convert.LikeDBNull(drs) && drs.Length > 0)
			{
				foreach (DataRow drChild in drs)
				{
					System.Windows.Forms.TreeListViewItem n = new System.Windows.Forms.TreeListViewItem();
					node.Items.Add(n);
					BindRow(n, drChild);
					BindChildren(n, drChild);
				}
			}
		}
	}
}
