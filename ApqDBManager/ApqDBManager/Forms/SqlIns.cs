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

namespace ApqDBManager.Forms
{
	public partial class SqlIns : Apq.Windows.Forms.DockForm
	{
		private Apq.DBC.XSD _Sqls = null;

		public SqlIns()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Form状态
		/// </summary>
		public class UIState
		{
			/// <summary>
			/// 展开的节点ID
			/// </summary>
			public List<int> Node_Expanded = new List<int>();
			/// <summary>
			/// 当前节点ID
			/// </summary>
			public int FocusedServerID = 1;
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
				tsmi.Click += new EventHandler(tsmi_Click);
				tsmiSelect.DropDownItems.Add(tsmi);
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
			if (treeList1.Nodes.FirstNode != null) treeList1.Nodes.FirstNode.Expanded = true;
		}

		// 选择选中类型的数据库
		private void tsmi_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
			if (tsmi != null)
			{
				tsmi.Checked = !tsmi.Checked;

				// 获取类型值
				int idxBegin = tsmi.Text.IndexOf("&") + 1;
				int idxLength = tsmi.Text.Length - 1 - idxBegin;
				int idx = Apq.Convert.ChangeType<int>(tsmi.Text.Substring(idxBegin, idxLength), -1);

				treeList1.BeginUpdate();
				DataView dv = new DataView(_Sqls.SqlInstance);
				dv.RowFilter = "SqlType = " + idx;
				if (idx == 0) dv.RowFilter = dv.RowFilter + " OR SqlType IS NULL";

				foreach (DataRowView drv in dv)
				{
					drv["CheckState"] = Apq.Convert.ChangeType<int>(tsmi.Checked);
				}

				_Sqls.SqlInstance.AcceptChanges();
				treeList1.EndUpdate();
			}
		}
		//全选
		private void tsmiSelectAll_Click(object sender, EventArgs e)
		{
			treeList1.BeginUpdate();
			foreach (DataRow dr in _Sqls.SqlInstance.Rows)
			{
				dr["CheckState"] = 1;
			}
			_Sqls.SqlInstance.AcceptChanges();
			treeList1.EndUpdate();
		}
		//反选
		private void tsmiReverse_Click(object sender, EventArgs e)
		{
			treeList1.BeginUpdate();
			foreach (DataRow dr in _Sqls.SqlInstance.Rows)
			{
				int check = Apq.Convert.ChangeType<int>(dr["CheckState"]);
				if (check == 2 || check == 0)
				{
					check = 1;
				}
				else
				{
					check = 0;
				}
				dr["CheckState"] = check;
			}
			_Sqls.SqlInstance.AcceptChanges();
			treeList1.EndUpdate();
		}
		//全部展开
		private void tsmiExpandAll_Click(object sender, EventArgs e)
		{
			if (tsmiExpandAll.Text == "全部展开(&D)")
			{
				treeList1.ExpandAll();
				tsmiExpandAll.Text = "全部收起(&D)";
				return;
			}
			if (tsmiExpandAll.Text == "全部收起(&D)")
			{
				treeList1.CollapseAll();
				tsmiExpandAll.Text = "全部展开(&D)";
				return;
			}
		}
		//失败
		private void tsmiFail_Click(object sender, EventArgs e)
		{
			treeList1.BeginUpdate();
			foreach (DataRow dr in _Sqls.SqlInstance.Rows)
			{
				dr["CheckState"] = 0;
			}

			DataView dv = new DataView(_Sqls.SqlInstance);
			dv.RowFilter = "Err = 1";
			foreach (DataRowView drv in dv)
			{
				drv["CheckState"] = 1;
			}
			_Sqls.SqlInstance.AcceptChanges();
			treeList1.EndUpdate();
		}
		//结果
		private void tsmiResult_Click(object sender, EventArgs e)
		{
			treeList1.BeginUpdate();
			foreach (DataRow dr in _Sqls.SqlInstance.Rows)
			{
				dr["CheckState"] = 0;
			}

			DataView dv = new DataView(_Sqls.SqlInstance);
			dv.RowFilter = "IsReadyToGo = 1";
			foreach (DataRowView drv in dv)
			{
				drv["CheckState"] = 1;
			}
			_Sqls.SqlInstance.AcceptChanges();
			treeList1.EndUpdate();
		}

		#endregion

		#region treeList1
		// Checked图片
		private void treeList1_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
		{
			e.NodeImageIndex = Apq.Convert.ChangeType<int>(e.Node["CheckState"]);
		}

		#region 选中状态
		private void SetCheckedNode(TreeListNode node, bool checkParent, bool checkChildren)
		{
			int Checked = (int)node["CheckState"];
			if (Checked == 2 || Checked == 0) Checked = 1;
			else Checked = 0;

			treeList1.BeginUpdate();
			node["CheckState"] = Checked;
			if (checkParent)
			{
				SetCheckedParentNodes(node, Checked);
			}
			if (checkChildren)
			{
				SetCheckedChildNodes(node, Checked);
			}
			_Sqls.SqlInstance.AcceptChanges();
			treeList1.EndUpdate();
		}
		private void SetCheckedChildNodes(TreeListNode node, int Checked)
		{
			foreach (TreeListNode tln in node.Nodes)
			{
				tln["CheckState"] = Checked;
				SetCheckedChildNodes(tln, Checked);
			}
		}
		private void SetCheckedParentNodes(TreeListNode node, int Checked)
		{
			if (node.ParentNode != null)
			{
				node.ParentNode["CheckState"] = Checked;
				SetCheckedParentNodes(node.ParentNode, Checked);
			}
		}
		#endregion

		#region 点击
		private void treeList1_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				TreeListHitInfo hInfo = treeList1.CalcHitInfo(new Point(e.X, e.Y));
				if (hInfo.Node != null && hInfo.HitInfoType == HitInfoType.StateImage)//左键点中状态框(Checked)
				{
					short KeyState = Apq.DllImports.User32.GetKeyState(Apq.Utils.VirtKeys.VK_CONTROL);
					bool checkChildren = (KeyState & 0xF0) != 0 || hInfo.Node.ParentNode == null;//按住Ctrl或为根结点
					SetCheckedNode(hInfo.Node, false, checkChildren);
				}
			}
		}

		private void treeList1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyData == Keys.Space)
			{
				if (Apq.Convert.ChangeType<int>(treeList1.FocusedNode["ID"]) == 1)
				{
					SetCheckedNode(treeList1.FocusedNode, false, true);
				}
				else
				{
					SetCheckedNode(treeList1.FocusedNode, false, false);
				}
			}
		}
		private void treeList1_EditorKeyUp(object sender, KeyEventArgs e)
		{
			#region Ctrl&C
			if (e.Control && (e.KeyCode == Keys.C))
			{
				Clipboard.SetData(DataFormats.UnicodeText, treeList1.FocusedNode.GetDisplayText(0));
			}
			#endregion
		}
		private void treeList1_KeyUp(object sender, KeyEventArgs e)
		{
			#region Ctrl&C
			if (e.Control && (e.KeyCode == Keys.C))
			{
				Clipboard.SetData(DataFormats.UnicodeText, treeList1.FocusedNode.GetDisplayText(0));
			}
			#endregion
		}
		#endregion

		private void treeList1_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
		{
			if (e.Node != null)
			{
				// 显示相应结果
				string ServerName = e.Node.GetDisplayText(0);
				SqlEdit Editor = GlobalObject.MainForm.ActiveMdiChild as SqlEdit;
				if (Editor != null)
				{
					Editor.ShowTabPage(ServerName);
				}
			}
		}

		private void tsmiCopy_Click(object sender, EventArgs e)
		{
			Clipboard.SetData(DataFormats.UnicodeText, treeList1.FocusedNode.GetDisplayText(0));
		}

		/// <summary>
		/// 选中并展开指定ID的节点
		/// </summary>
		/// <param name="ID"></param>
		public void FocusAndExpandByID(int ID)
		{
			TreeListNode tln = treeList1.FindNodeByFieldValue("ID", ID);
			if (tln != null)
			{
				treeList1.BeginUpdate();
				if (tln.ParentNode != null)
				{
					FocusAndExpandByID(tln.ParentNode.Id);
					tln.ParentNode.Expanded = true;
				}
				treeList1.EndUpdate();

				treeList1.FocusedNode = tln;
			}
		}
		#endregion

		#region UI 公开方法
		public UIState GetUIState()
		{
			UIState State = new UIState();
			if (treeList1.FocusedNode != null)
			{
				State.FocusedServerID = Apq.Convert.ChangeType<int>(treeList1.FocusedNode["ID"]);
				foreach (DataRow dr in _Sqls.SqlInstance.Rows)
				{
					int ID = Apq.Convert.ChangeType<int>(dr["SqlID"]);
					TreeListNode tln = treeList1.FindNodeByFieldValue("SqlID", ID);
					if (tln != null && tln.Expanded)
					{
						State.Node_Expanded.Add(ID);
					}
				}
			}
			return State;
		}

		/// <summary>
		/// 改变服务器列表
		/// </summary>
		public void SetServers(Apq.DBC.XSD Sqls, UIState UIState)
		{
			treeList1.DataSource = _Sqls = Sqls;

			// 设置新状态
			if (UIState != null)
			{
				foreach (int ID in UIState.Node_Expanded)
				{
					FocusAndExpandByID(ID);
				}
				FocusAndExpandByID(UIState.FocusedServerID);
			}
			SolutionExplorer_Shown(null, null);
		}
		#endregion
	}
}