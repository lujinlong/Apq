using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;

namespace Apq.TreeListView
{
	/// <summary>
	/// TreeListView助手
	/// </summary>
	public class TreeListViewHelper
	{
		/// <summary>
		/// _TreeListView
		/// </summary>
		protected System.Windows.Forms.TreeListView _TreeListView;
		/// <summary>
		/// 获取TreeListView
		/// </summary>
		public System.Windows.Forms.TreeListView TreeListView
		{
			get { return _TreeListView; }
		}

		/// <summary>
		/// TreeListView助手
		/// </summary>
		/// <param name="TreeListView"></param>
		public TreeListViewHelper(System.Windows.Forms.TreeListView TreeListView)
		{
			_TreeListView = TreeListView;
			_TreeListView.KeyUp += new System.Windows.Forms.KeyEventHandler(_TreeListView_KeyUp);
		}

		private void _TreeListView_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			#region Ctrl&C
			if (e.Control && (e.KeyCode == System.Windows.Forms.Keys.C))
			{
				System.Windows.Forms.Clipboard.SetText(TreeListView.FocusedItem.Text);
			}
			#endregion
		}

		#region TableMapping
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
		#endregion

		#region HiddenColNames
		private List<string> _HiddenColNames = new List<string>();
		/// <summary>
		/// 设置隐藏列
		/// </summary>
		public List<string> HiddenColNames
		{
			set { _HiddenColNames = value; }
		}

		/// <summary>
		/// 获取隐藏到TreeListView的数据列名
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public List<string> GetHiddenColumns(DataTable dt)
		{
			if (_HiddenColNames.Count > 0)
			{
				return _HiddenColNames;
			}

			DataTableMapping dtm = GetTableMapping(dt);

			foreach (DataColumn dc in dt.Columns)
			{
				bool Exists = false;
				foreach (DataColumnMapping dcm in dtm.ColumnMappings)
				{
					if (dcm.DataSetColumn == dc.ColumnName)
					{
						Exists = true;
						break;
					}
				}

				if (!Exists)
				{
					_HiddenColNames.Add(dc.ColumnName);
				}
			}

			return _HiddenColNames;
		}
		#endregion

		/// <summary>
		/// 数据主键列名
		/// </summary>
		public string Key = "ID";
		/// <summary>
		/// 数据上级列名
		/// </summary>
		public string Parent = "ParentID";
		/// <summary>
		/// 绑定时是否包含所有数据列(默认false. true:无对应列头的数据列按表中顺序添加到节点最后)
		/// </summary>
		public bool BindContainsAllDataColumns = false;

		#region DataBind
		/// <summary>
		/// 绑定数据表
		/// </summary>
		public void BindDataTable(DataTable dt)
		{
			TreeListView.Items.Clear();

			// 记录下需要添加的列
			List<string> hCols = GetHiddenColumns(dt);

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

			// 添加隐藏数据
			foreach (string ColName in _HiddenColNames)
			{
				string str = Apq.Convert.ChangeType<string>(dr[ColName], string.Empty);
				node.SubItems.Add(str);
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
		#endregion

		#region FindNodeByKey
		/// <summary>
		/// 查找指定主键值的节点(假定主键为第一个隐藏列)
		/// </summary>
		public System.Windows.Forms.TreeListViewItem FindNodeByKey(string ID)
		{
			foreach (System.Windows.Forms.TreeListViewItem root in TreeListView.Items)
			{
				System.Windows.Forms.TreeListViewItem n = FindNodeByKey(root, ID);
				if (n != null) return n;
			}
			return null;
		}

		/// <summary>
		/// 从指定节点及其下级中查找指定主键值的节点(假定主键为第一个隐藏列)
		/// </summary>
		public System.Windows.Forms.TreeListViewItem FindNodeByKey(System.Windows.Forms.TreeListViewItem node, string ID)
		{
			if (node.SubItems[node.ListView.Columns.Count].Text == ID) return node;

			foreach (System.Windows.Forms.TreeListViewItem tln in node.Items)
			{
				System.Windows.Forms.TreeListViewItem n = FindNodeByKey(tln, ID);
				if (n != null) return n;
			}
			return null;
		}
		#endregion

		#region 选中状态
		#region SetChecked
		/// <summary>
		/// 设置指定节点的选中状态
		/// </summary>
		public static void SetCheckedNode(TreeListViewItem node, bool Checked, bool checkParent, bool checkChildren)
		{
			node.Checked = Checked;

			if (checkParent)
			{
				SetCheckedParentNodes(node, Checked);
			}
			if (checkChildren)
			{
				SetCheckedChildNodes(node, Checked);
			}
		}
		/// <summary>
		/// 设置指定节点下级的选中状态
		/// </summary>
		public static void SetCheckedChildNodes(TreeListViewItem node, bool Checked)
		{
			foreach (TreeListViewItem tln in node.Items)
			{
				tln.Checked = Checked;
				SetCheckedChildNodes(tln, Checked);
			}
		}
		/// <summary>
		/// 设置指定节点上级的选中状态
		/// </summary>
		public static void SetCheckedParentNodes(TreeListViewItem node, bool Checked)
		{
			if (node.Parent != null)
			{
				node.Parent.Checked = Checked;
				SetCheckedParentNodes(node.Parent, Checked);
			}
		}
		#endregion
		#region ChgChecked
		/// <summary>
		/// 切换指定节点的选中状态
		/// </summary>
		public static void ChgCheckedNode(TreeListViewItem node, bool checkParent, bool checkChildren)
		{
			node.Checked = !node.Checked;

			if (checkParent)
			{
				ChgCheckedParentNodes(node);
			}
			if (checkChildren)
			{
				ChgCheckedChildNodes(node);
			}
		}
		/// <summary>
		/// 切换指定节点下级的选中状态
		/// </summary>
		public static void ChgCheckedChildNodes(TreeListViewItem node)
		{
			foreach (TreeListViewItem tln in node.Items)
			{
				tln.Checked = !tln.Checked;
				ChgCheckedChildNodes(tln);
			}
		}
		/// <summary>
		/// 切换指定节点下级的选中状态
		/// </summary>
		public static void ChgCheckedParentNodes(TreeListViewItem node)
		{
			if (node.Parent != null)
			{
				node.Parent.Checked = !node.Parent.Checked;
				ChgCheckedParentNodes(node.Parent);
			}
		}
		#endregion
		#endregion

	}
}
