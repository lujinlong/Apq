using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;

namespace QQUnbindTXZ
{
	public partial class SolutionExplorer : DockContent
	{
		public SolutionExplorer()
		{
			InitializeComponent();
		}

		private void SolutionExplorer_Load(object sender, EventArgs e)
		{
			treeList1.DataSource = GlobalObject.Servers;

			Apq.Windows.Controls.Control.AddImeHandler(this);
		}

		// [已停用]
		#region 按钮事件处理
		//全选
		private void bbiSelectAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			foreach (DataRow dr in GlobalObject.Servers._Servers.Rows)
			{
				dr["Checked"] = 1;
			}
			GlobalObject.Servers._Servers.AcceptChanges();
		}
		// 反选
		private void bbiReverse_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			foreach (DataRow dr in GlobalObject.Servers._Servers.Rows)
			{
				int check = Apq.Convert.ChangeType<int>(dr["Checked"]);
				if (check == 2 || check == 0)
				{
					check = 1;
				}
				else
				{
					check = 0;
				}
				dr["Checked"] = check;
			}
			GlobalObject.Servers._Servers.AcceptChanges();
		}
		//区库
		private void bciArea_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			DataView dv = new DataView(GlobalObject.Servers._Servers);
			dv.RowFilter = "Type = 1";

			foreach (DataRowView drv in dv)
			{
				drv["Checked"] = Apq.Convert.ChangeType<int>(bciArea.Checked);
			}

			GlobalObject.Servers._Servers.AcceptChanges();
		}
		//游戏库
		private void bciServer_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			DataView dv = new DataView(GlobalObject.Servers._Servers);
			dv.RowFilter = "Type = 2";

			foreach (DataRowView drv in dv)
			{
				drv["Checked"] = Apq.Convert.ChangeType<int>(bciServer.Checked);
			}

			GlobalObject.Servers._Servers.AcceptChanges();
		}
		#endregion

		// [停用]
		#region treeList1
		private void NodeClick(TreeListNode node)
		{
			int check = Apq.Convert.ChangeType<int>(GlobalObject.Servers._Servers.Rows[node.Id]["Checked"]);
			if (check == 2 || check == 0)
			{
				check = 1;
			}
			else
			{
				check = 0;
			}

			treeList1.BeginUpdate();
			GlobalObject.Servers._Servers.Rows[node.Id]["Checked"] = check;
			SetCheckedChildNodes(node, check);
			//SetCheckedParentNodes(node, check);
			GlobalObject.Servers._Servers.AcceptChanges();
			treeList1.EndUpdate();
		}
		private void SetCheckedChildNodes(TreeListNode node, int check)
		{
			foreach (TreeListNode tln in node.Nodes)
			{
				GlobalObject.Servers._Servers.Rows[tln.Id]["Checked"] = check;
				SetCheckedChildNodes(tln, check);
			}
		}
		private void SetCheckedParentNodes(TreeListNode node, int check)
		{
			//if (node.ParentNode != null)
			//{
			//    bool b = false;
			//    int state;
			//    for (int i = 0; i < node.ParentNode.Nodes.Count; i++)
			//    {
			//        if (node.ParentNode.Nodes[i].Tag == null) state = 0;
			//        else state = (int)node.ParentNode.Nodes[i].Tag;
			//        if (!check.Equals(state))
			//        {
			//            b = !b;
			//            break;
			//        }
			//    }
			//    node.ParentNode.Tag = b ? 2 : check;
			//    SetCheckedParentNodes(node.ParentNode, check);
			//}
		}

		private void treeList1_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
		{
			e.NodeImageIndex = Apq.Convert.ChangeType<int>(GlobalObject.Servers._Servers.Rows[e.Node.Id]["Checked"]);
		}
		private void treeList1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyData == Keys.Space)
			{
				NodeClick(treeList1.FocusedNode);
			}
		}
		#endregion
	}
}