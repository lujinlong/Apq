using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using WeifenLuo.WinFormsUI.Docking;
using System.Data.SqlClient;
using Apq.TreeListView;

namespace ApqDBManager.Forms
{
	public partial class SqlIns : Apq.Windows.Forms.DockForm
	{
		private ApqDBManager.Forms.SrvsMgr.SrvsMgr_XSD _Sqls = null;
		private TreeListViewHelper tlvHelper;

		public SqlIns()
		{
			InitializeComponent();
		}

		#region UI线程
		private void SqlIns_Load(object sender, EventArgs e)
		{
			// 加载选择菜单
			string[] bciNames = GlobalObject.XmlConfigChain[this.GetType(), "RDBTypes"].Split(',');
			for (int i = 0; i < bciNames.Length; i++)
			{
				ToolStripMenuItem tsmi = new ToolStripMenuItem();
				tsmi.Text = bciNames[i];
				tsmi.Click += new EventHandler(tsmiSelect_Click);
				tssbSelect.DropDownItems.Add(tsmi);
			}

			#region CheckedNames
			string cfgCheckedNames = GlobalObject.XmlConfigChain[this.GetType(), "CheckedServerNames"];
			if (!string.IsNullOrEmpty(cfgCheckedNames))
			{
				string[] aryCheckedServerNames = cfgCheckedNames.Split(',');
				foreach (string strCheckedName in aryCheckedServerNames)
				{
					DataView dv = new DataView(_Sqls.SqlInstance);
					dv.RowFilter = "SqlName = " + Apq.Data.SqlClient.Common.ConvertToSqlON(SqlDbType.VarChar, strCheckedName);
					foreach (DataRowView dr in dv)
					{
						dr["CheckState"] = 1;
					}
				}
				_Sqls.SqlInstance.AcceptChanges();
			}
			#endregion

			Apq.Windows.Controls.Control.AddImeHandler(this);
			//Apq.Xtra.TreeList.Common.AddBehaivor(treeList1);
		}

		private void SolutionExplorer_Shown(object sender, EventArgs e)
		{
			// 展开顶级
			if (treeListView1.Items.Count > 0) treeListView1.Items[0].Expand();
		}

		// 选择选中类型的数据库
		private void tsmiSelect_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
			if (tsmi != null)
			{
				tsmi.Checked = !tsmi.Checked;

				// 获取类型值
				int idxBegin = tsmi.Text.IndexOf("&") + 1;
				int idxLength = tsmi.Text.Length - 1 - idxBegin;
				int idx = Apq.Convert.ChangeType<int>(tsmi.Text.Substring(idxBegin, idxLength), -1);

				DataView dv = new DataView(_Sqls.SqlInstance);
				dv.RowFilter = "SqlType = " + idx;
				if (idx == 0) dv.RowFilter = dv.RowFilter + " OR SqlType IS NULL";

				foreach (DataRowView drv in dv)
				{
					string strSqlID = Apq.Convert.ChangeType<string>(drv["SqlID"]);
					TreeListViewItem node = tlvHelper.FindNodeByKey(strSqlID);
					node.Checked = tsmi.Checked;
				}
			}
		}
		//全选
		private void tsbSelectAll_Click(object sender, EventArgs e)
		{
			foreach (TreeListViewItem root in treeListView1.Items)
			{
				SetCheckedNode(root, true, false, true);
			}
		}
		//反选
		private void tsbReverse_Click(object sender, EventArgs e)
		{
			foreach (TreeListViewItem root in treeListView1.Items)
			{
				ChgCheckedNode(root, false, true);
			}
		}
		//全部展开
		private void tsbExpandAll_Click(object sender, EventArgs e)
		{
			if (tsbExpandAll.Text == "全部展开(&D)")
			{
				treeListView1.ExpandAll();
				tsbExpandAll.Text = "全部收起(&D)";
				return;
			}
			if (tsbExpandAll.Text == "全部收起(&D)")
			{
				treeListView1.CollapseAll();
				tsbExpandAll.Text = "全部展开(&D)";
				return;
			}
		}
		//失败
		private void tsbFail_Click(object sender, EventArgs e)
		{
			tsbSelectAll_Click(sender, e);
			tsbReverse_Click(sender, e);

			DataView dv = new DataView(_Sqls.SqlInstance);
			dv.RowFilter = "Err = 1";
			foreach (DataRowView drv in dv)
			{
				string strSqlID = Apq.Convert.ChangeType<string>(drv["SqlID"]);
				TreeListViewItem node = tlvHelper.FindNodeByKey(strSqlID);
				node.Checked = true;
			}
		}
		//结果
		private void tsbResult_Click(object sender, EventArgs e)
		{
			tsbSelectAll_Click(sender, e);
			tsbReverse_Click(sender, e);

			DataView dv = new DataView(_Sqls.SqlInstance);
			dv.RowFilter = "IsReadyToGo = 1";
			foreach (DataRowView drv in dv)
			{
				string strSqlID = Apq.Convert.ChangeType<string>(drv["SqlID"]);
				TreeListViewItem node = tlvHelper.FindNodeByKey(strSqlID);
				node.Checked = true;
			}
		}

		#endregion

		#region treeList1

		private void treeListView1_AfterExpand(object sender, TreeListViewEventArgs e)
		{
			long SqlID = Apq.Convert.ChangeType<long>(e.Item.SubItems[e.Item.ListView.Columns.Count].Text);

			DataRow[] drs = _Sqls.SqlInstance.Select("SqlID = " + SqlID);
			if (drs.Length > 0)
			{
				drs[0]["_Expanded"] = true;
			}
		}

		private void treeListView1_AfterCollapse(object sender, TreeListViewEventArgs e)
		{
			long SqlID = Apq.Convert.ChangeType<long>(e.Item.SubItems[e.Item.ListView.Columns.Count].Text);

			DataRow[] drs = _Sqls.SqlInstance.Select("SqlID = " + SqlID);
			if (drs.Length > 0)
			{
				drs[0]["_Expanded"] = false;
			}
		}

		private void treeListView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (treeListView1.FocusedItem != null)
			{
				// 显示相应结果
				string ServerName = treeListView1.FocusedItem.Text;
				SqlEdit Editor = GlobalObject.MainForm.ActiveMdiChild as SqlEdit;
				if (Editor != null)
				{
					Editor.ShowTabPage(ServerName);
				}
			}
		}

		private void treeListView1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Space)
			{
				if (Apq.Convert.ChangeType<int>(treeListView1.FocusedItem.SubItems[treeListView1.Columns.Count].Text) == 1)
				{
					ChgCheckedNode(treeListView1.FocusedItem, false, true);
				}
				else
				{
					ChgCheckedNode(treeListView1.FocusedItem, false, false);
				}
			}
		}

		#region 选中状态
		#region SetChecked
		private void SetCheckedNode(TreeListViewItem node, bool Checked, bool checkParent, bool checkChildren)
		{
			treeListView1.EndUpdate();
			node.Checked = Checked;

			if (checkParent)
			{
				SetCheckedParentNodes(node, Checked);
			}
			if (checkChildren)
			{
				SetCheckedChildNodes(node, Checked);
			}
			treeListView1.EndUpdate();
		}
		private void SetCheckedChildNodes(TreeListViewItem node, bool Checked)
		{
			foreach (TreeListViewItem tln in node.Items)
			{
				tln.Checked = Checked;
				SetCheckedChildNodes(tln, Checked);
			}
		}
		private void SetCheckedParentNodes(TreeListViewItem node, bool Checked)
		{
			if (node.Parent != null)
			{
				node.Parent.Checked = Checked;
				SetCheckedParentNodes(node.Parent, Checked);
			}
		}
		#endregion
		#region ChgChecked
		private void ChgCheckedNode(TreeListViewItem node, bool checkParent, bool checkChildren)
		{
			treeListView1.BeginUpdate();
			node.Checked = !node.Checked;

			if (checkParent)
			{
				ChgCheckedParentNodes(node);
			}
			if (checkChildren)
			{
				ChgCheckedChildNodes(node);
			}
			treeListView1.EndUpdate();
		}
		private void ChgCheckedChildNodes(TreeListViewItem node)
		{
			foreach (TreeListViewItem tln in node.Items)
			{
				tln.Checked = !tln.Checked;
				ChgCheckedChildNodes(tln);
			}
		}
		private void ChgCheckedParentNodes(TreeListViewItem node)
		{
			if (node.Parent != null)
			{
				node.Parent.Checked = !node.Parent.Checked;
				ChgCheckedParentNodes(node.Parent);
			}
		}
		#endregion
		#endregion

		#region 选中展开状态By隐藏列
		private void SetStateByHiddenCol(TreeListViewItem node)
		{
			bool Checked = Apq.Convert.ChangeType<bool>(node.SubItems[node.ListView.Columns.Count + 1].Text);
			bool Expanded = Apq.Convert.ChangeType<bool>(node.SubItems[node.ListView.Columns.Count + 2].Text);
			bool Selected = Apq.Convert.ChangeType<bool>(node.SubItems[node.ListView.Columns.Count + 3].Text);

			node.Checked = Checked;
			if (Expanded) node.Expand();
			if (Selected) node.Selected = true;

			foreach (TreeListViewItem tln in node.Items)
			{
				SetStateByHiddenCol(tln);
			}
		}

		private void SaveState2XSD(TreeListViewItem node)
		{
			long SqlID = Apq.Convert.ChangeType<long>(node.SubItems[node.ListView.Columns.Count].Text);

			DataRow[] drs = _Sqls.SqlInstance.Select("SqlID = " + SqlID);
			if (drs.Length > 0)
			{
				drs[0]["CheckState"] = node.Checked;
				drs[0]["_Selected"] = node.Selected;
			}

			foreach (TreeListViewItem tln in node.Items)
			{
				SaveState2XSD(tln);
			}
		}
		#endregion

		private void tsmiCopy_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(treeListView1.FocusedItem.Text);
		}

		/// <summary>
		/// 选中并展开指定ID的节点
		/// </summary>
		/// <param name="ID"></param>
		public void FocusAndExpandByID(int ID)
		{
			TreeListViewItem node = tlvHelper.FindNodeByKey(ID.ToString());
			if (node != null)
			{
				node.Expand();
				node.Selected = true;
			}
		}
		#endregion

		#region IDataShow 成员
		/// <summary>
		/// 前期准备(如数据库连接或文件等)
		/// </summary>
		public override void InitDataBefore()
		{
			tlvHelper = new TreeListViewHelper(treeListView1);
			tlvHelper.TableMapping.ColumnMappings.Add("名称", "SqlName");
			tlvHelper.Key = "SqlID";
			tlvHelper.HiddenColNames = new List<string>(new string[] { "SqlID", "CheckState", "_Expanded", "_Selected" });
		}
		#endregion

		#region UI 公开方法
		/// <summary>
		/// 改变服务器列表
		/// </summary>
		public void SetServers(ApqDBManager.Forms.SrvsMgr.SrvsMgr_XSD Sqls)
		{
			_Sqls = Sqls;

			// 绑定到TreeListView
			treeListView1.Items.Clear();
			if (_Sqls.SqlInstance.Rows.Count > 0)
			{
				tlvHelper.BindDataTable(_Sqls.SqlInstance);

				foreach (TreeListViewItem root in treeListView1.Items)
				{
					SetStateByHiddenCol(root);
				}
			}
			SolutionExplorer_Shown(null, null);
		}

		/// <summary>
		/// 保存选中展开状态到XSD
		/// </summary>
		public void SaveState2XSD()
		{
			foreach (TreeListViewItem root in treeListView1.Items)
			{
				SaveState2XSD(root);
			}
		}
		#endregion
	}
}