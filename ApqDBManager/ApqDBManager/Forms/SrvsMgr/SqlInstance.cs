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
using System.Data.SqlClient;

namespace ApqDBManager.Forms.SrvsMgr
{
	public partial class SQLInstance : Apq.Windows.Forms.DockForm
	{
		protected string _FileName = string.Empty;

		public SQLInstance()
		{
			InitializeComponent();
		}

		private void Random_Load(object sender, EventArgs e)
		{
		}

		private void btnSaveToDB_Click(object sender, EventArgs e)
		{
		}

		private void Random_FormClosing(object sender, FormClosingEventArgs e)
		{
		}
		private void btnLoadFromDB_Click(object sender, EventArgs e)
		{
		}

		#region treeList1
		// Checked图片
		private void treeList1_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
		{
			e.NodeImageIndex = Apq.Convert.ChangeType<int>(e.Node["CheckState"]);
		}

		#region 选中状态
		private void SetCheckedNode(TreeListNode node, bool checkParent, bool checkChildren)
		{
			int Checked = Apq.Convert.ChangeType<int>(node["CheckState"]);
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
				bsiOutInfo.Caption = e.Node.GetDisplayText("SqlName");
			}
		}

		private void tsmiAdd_Click(object sender, EventArgs e)
		{
			DataRow dr = _Sqls.SqlInstance.NewRow();
			_Sqls.SqlInstance.Rows.Add(dr);
		}

		private void tsmiDel_Click(object sender, EventArgs e)
		{
			treeList1.Nodes.Remove(treeList1.FocusedNode);
		}

		private void tsmiTestOpen_Click(object sender, EventArgs e)
		{
			TreeListNode tln = treeList1.FocusedNode;
			if (tln != null && tln.ParentNode != null)
			{
				string strPwdD = Apq.Convert.ChangeType<string>(tln["PwdD"]);
				if (string.IsNullOrEmpty(strPwdD))
				{
					strPwdD = Apq.Security.Cryptography.DESHelper.DecryptString(tln["PwdC"].ToString(), GlobalObject.RegConfigChain["Crypt", "DESKey"], GlobalObject.RegConfigChain["Crypt", "DESIV"]);
				}
				SqlConnection sc = new SqlConnection(string.Format("Data Source={0},{1};User Id={2};Password={3};",
					tln["IPWan1"], tln["SqlPort"], tln["UID"], strPwdD));
				try
				{
					Apq.Data.Common.DbConnectionHelper.Open(sc);
					bsiTest.Caption = tln.GetDisplayText("Name") + "-->连接成功.";
				}
				catch
				{
					bsiTest.Caption = tln.GetDisplayText("Name") + "-X-连接失败!";
				}
				finally
				{
					Apq.Data.Common.DbConnectionHelper.Close(sc);
				}
			}
		}
		#endregion
	}
}