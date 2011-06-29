using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Data.SqlClient;
using Apq.TreeListView;

namespace ApqDBManager.Forms
{
	public partial class DBIs : Apq.Windows.Forms.DockForm
	{
		private SqlEdit _SqlEdit = null;
		public SqlEdit SqlEdit
		{
			get { return _SqlEdit; }
			set { _SqlEdit = value; }
		}

		private TreeListViewHelper tlvHelper;
		public Apq.DBC.XSD dsDBC
		{
			get { return SqlEdit.SqlEditDoc.dsDBC; }
		}

		public DBIs()
		{
			InitializeComponent();
		}

		public override void SetUILang(Apq.UILang.UILang UILang)
		{
			tssbSelect.Text = Apq.GlobalObject.UILang["选择(&L)"];
			tsbSelectAll.Text = Apq.GlobalObject.UILang["全选(&A)"];
			tsbReverse.Text = Apq.GlobalObject.UILang["反选(&S)"];

			tsbExpandAll.Text = Apq.GlobalObject.UILang["全部收起(&D)"];
			tsbFail.Text = Apq.GlobalObject.UILang["失败(&F)"];
			tsbResult.Text = Apq.GlobalObject.UILang["结果(&T)"];
		}

		#region UI线程
		private void SqlIns_Load(object sender, EventArgs e)
		{
			// 加载选择菜单
			foreach (DBIType_XSD.DBITypeRow dr in GlobalObject.Lookup.DBIType.Rows)
			{
				ToolStripMenuItem tsmi = new ToolStripMenuItem();
				tsmi.Text = dr.TypeCaption;
				tsmi.Tag = dr.DBIType;
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
					DataView dv = new DataView(dsDBC.DBI);
					dv.RowFilter = "DBIName = " + Apq.Data.SqlClient.Common.ConvertToSqlON(SqlDbType.VarChar, strCheckedName);
					foreach (DataRowView dr in dv)
					{
						dr["_Checked"] = 1;
					}
				}
				dsDBC.DBI.AcceptChanges();
			}
			#endregion
		}

		private void DBIs_Shown(object sender, EventArgs e)
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
				int DBIType = Apq.Convert.ChangeType<int>(tsmi.Tag, -1);

				DataView dv = new DataView(dsDBC.DBI);
				dv.RowFilter = "DBIType = " + DBIType;
				if (DBIType == 0) dv.RowFilter = dv.RowFilter + " OR DBIType IS NULL";

				foreach (DataRowView drv in dv)
				{
					string strDBIID = Apq.Convert.ChangeType<string>(drv["DBIID"]);
					TreeListViewItem node = tlvHelper.FindNodeByKey(strDBIID);
					node.Checked = tsmi.Checked;
				}
			}
		}
		//全选
		private void tsbSelectAll_Click(object sender, EventArgs e)
		{
			foreach (TreeListViewItem root in treeListView1.Items)
			{
				Apq.TreeListView.TreeListViewHelper.SetCheckedNode(root, true, false, true);
			}
		}
		//反选
		private void tsbReverse_Click(object sender, EventArgs e)
		{
			foreach (TreeListViewItem root in treeListView1.Items)
			{
				Apq.TreeListView.TreeListViewHelper.ChgCheckedNode(root, false, true);
			}
		}
		//全部展开
		private void tsbExpandAll_Click(object sender, EventArgs e)
		{
			if (tsbExpandAll.Text == Apq.GlobalObject.UILang["全部展开(&D)"])
			{
				treeListView1.ExpandAll();
				tsbExpandAll.Text = Apq.GlobalObject.UILang["全部收起(&D)"];
				return;
			}
			if (tsbExpandAll.Text == Apq.GlobalObject.UILang["全部收起(&D)"])
			{
				treeListView1.CollapseAll();
				tsbExpandAll.Text = Apq.GlobalObject.UILang["全部展开(&D)"];
				return;
			}
		}
		//失败
		private void tsbFail_Click(object sender, EventArgs e)
		{
			tsbSelectAll_Click(sender, e);
			tsbReverse_Click(sender, e);

			DataView dv = new DataView(dsDBC.DBI);
			dv.RowFilter = "Err = 1";
			foreach (DataRowView drv in dv)
			{
				string strDBIID = Apq.Convert.ChangeType<string>(drv["DBIID"]);
				TreeListViewItem node = tlvHelper.FindNodeByKey(strDBIID);
				node.Checked = true;
			}
		}
		//结果
		private void tsbResult_Click(object sender, EventArgs e)
		{
			tsbSelectAll_Click(sender, e);
			tsbReverse_Click(sender, e);

			DataView dv = new DataView(dsDBC.DBI);
			dv.RowFilter = "IsReadyToGo = 1";
			foreach (DataRowView drv in dv)
			{
				string strDBIID = Apq.Convert.ChangeType<string>(drv["DBIID"]);
				TreeListViewItem node = tlvHelper.FindNodeByKey(strDBIID);
				node.Checked = true;
			}
		}

		#endregion

		#region treeListView1

		private void treeListView1_AfterExpand(object sender, TreeListViewEventArgs e)
		{
			long DBIID = Apq.Convert.ChangeType<long>(e.Item.SubItems[e.Item.ListView.Columns.Count].Text);

			DataRow[] drs = dsDBC.DBI.Select("DBIID = " + DBIID);
			if (drs.Length > 0)
			{
				drs[0]["_Expanded"] = true;
			}
		}

		private void treeListView1_AfterCollapse(object sender, TreeListViewEventArgs e)
		{
			long DBIID = Apq.Convert.ChangeType<long>(e.Item.SubItems[e.Item.ListView.Columns.Count].Text);

			DataRow[] drs = dsDBC.DBI.Select("DBIID = " + DBIID);
			if (drs.Length > 0)
			{
				drs[0]["_Expanded"] = false;
			}
		}

		private void treeListView1_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			if (e.Item != null && e.Item.SubItems.Count > e.Item.ListView.Columns.Count)
			{
				long DBIID = Apq.Convert.ChangeType<long>(e.Item.SubItems[e.Item.ListView.Columns.Count].Text);
				DataRow[] drs = dsDBC.DBI.Select("DBIID = " + DBIID);
				if (drs.Length > 0)
				{
					drs[0]["_Checked"] = e.Item.Checked;
				}
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
					Editor.SqlOut.ShowTabPage(ServerName);
				}
			}
		}

		private void treeListView1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Space)
			{
				if (Apq.Convert.ChangeType<int>(treeListView1.FocusedItem.SubItems[treeListView1.Columns.Count].Text) == 1)
				{
					Apq.TreeListView.TreeListViewHelper.ChgCheckedNode(treeListView1.FocusedItem, false, true);
				}
				else
				{
					Apq.TreeListView.TreeListViewHelper.ChgCheckedNode(treeListView1.FocusedItem, false, false);
				}
			}
		}

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
			long DBIID = Apq.Convert.ChangeType<long>(node.SubItems[node.ListView.Columns.Count].Text);

			DataRow[] drs = dsDBC.DBI.Select("DBIID = " + DBIID);
			if (drs.Length > 0)
			{
				drs[0]["_Checked"] = node.Checked;
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
			tlvHelper.TableMapping.ColumnMappings.Add("名称", "DBIName");
			tlvHelper.Key = "DBIID";
			tlvHelper.HiddenColNames = new List<string>(new string[] { "DBIID", "_Checked", "_Expanded", "_Selected" });
		}
		/// <summary>
		/// 加载数据
		/// </summary>
		/// <param name="ds"></param>
		public override void LoadData(DataSet ds)
		{
			// 绑定到TreeListView
			treeListView1.Items.Clear();
			if (dsDBC.DBI.Rows.Count > 0)
			{
				tlvHelper.BindDataTable(dsDBC.DBI);
				treeListView1.ExpandAll();
			}
		}
		#endregion
	}
}