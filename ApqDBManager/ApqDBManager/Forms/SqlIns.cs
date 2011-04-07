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
		//数据库连接
		private SqlConnection _SqlConn = new SqlConnection();
		public Apq.DBC.XSD Sqls
		{
			get
			{
				if (!(FormDataSet is Apq.DBC.XSD))
				{
					FormDataSet = new Apq.DBC.XSD();
				}
				return FormDataSet as Apq.DBC.XSD;
			}
		}

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
				DevExpress.XtraBars.BarCheckItem bci = new DevExpress.XtraBars.BarCheckItem();
				bci.Caption = bciNames[i];
				bci.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(bci_CheckedChanged);
				bsiSelect.AddItem(bci);
			}

			#region CheckedNames
			string cfgCheckedNames = GlobalObject.XmlConfigChain[this.GetType(), "CheckedServerNames"];
			if (!string.IsNullOrEmpty(cfgCheckedNames))
			{
				string[] aryCheckedServerNames = cfgCheckedNames.Split(',');
				foreach (string strCheckedName in aryCheckedServerNames)
				{
					DataView dv = new DataView(_Sqls.SqlInstance);
					dv.RowFilter = "[Name] = " + Apq.Data.SqlClient.Common.ConvertToSqlON(SqlDbType.VarChar, strCheckedName);
					foreach (DataRowView dr in dv)
					{
						dr["CheckState"] = 1;
					}
				}
				_Sqls.SqlInstance.AcceptChanges();
			}
			#endregion

			Apq.Windows.Controls.Control.AddImeHandler(this);
			Apq.Xtra.TreeList.Common.AddBehaivor(treeList1);
		}

		private void SolutionExplorer_Shown(object sender, EventArgs e)
		{
			// 展开顶级
			if (treeList1.Nodes.FirstNode != null) treeList1.Nodes.FirstNode.Expanded = true;
		}

		// 选择选中类型的数据库
		private void bci_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			DevExpress.XtraBars.BarCheckItem bci = sender as DevExpress.XtraBars.BarCheckItem;
			if (bci != null)
			{
				// 获取类型值
				int idxBegin = bci.Caption.IndexOf("&") + 1;
				int idxLength = bci.Caption.Length - 1 - idxBegin;
				int idx = Apq.Convert.ChangeType<int>(bci.Caption.Substring(idxBegin, idxLength), -1);

				treeList1.BeginUpdate();
				DataView dv = new DataView(_Sqls.SqlInstance);
				dv.RowFilter = "Type = " + idx;
				if (idx == 0) dv.RowFilter = dv.RowFilter + " OR Type IS NULL";

				foreach (DataRowView drv in dv)
				{
					drv["CheckState"] = Apq.Convert.ChangeType<int>(bci.Checked);
				}

				_Sqls.SqlInstance.AcceptChanges();
				treeList1.EndUpdate();
			}
		}
		//全选
		private void bbiSelectAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
		private void bbiReverse_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
		//重新加载
		private void bbiRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			LoadData(FormDataSet);
		}
		//全部展开
		private void bbiExpandAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (bbiExpandAll.Caption == "全部展开(&D)")
			{
				treeList1.ExpandAll();
				bbiExpandAll.Caption = "全部收起(&D)";
				return;
			}
			if (bbiExpandAll.Caption == "全部收起(&D)")
			{
				treeList1.CollapseAll();
				bbiExpandAll.Caption = "全部展开(&D)";
				return;
			}
		}
		//失败
		private void bbiFail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
		private void bbiResult_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

		#region IDataShow 成员
		/// <summary>
		/// 前期准备(如数据库连接或文件等)
		/// </summary>
		public override void InitDataBefore()
		{
			#region 数据库连接
			_SqlConn.ConnectionString = GlobalObject.SqlConn;
			#endregion

			// 为sda设置SqlCommand
			scSelect.Connection = _SqlConn;
			scSelect.CommandText = "dbo.ApqDBMgr_SqlInstance_List";
			scSelect.CommandType = CommandType.StoredProcedure;

			scDelete.Connection = _SqlConn;
			scDelete.CommandText = "dbo.ApqDBMgr_SqlInstance_Delete";
			scDelete.CommandType = CommandType.StoredProcedure;
			scDelete.Parameters.Add("@SqlID", SqlDbType.Int, 4, "SqlID");

			scUpdate.Connection = _SqlConn;
			scUpdate.CommandText = "dbo.ApqDBMgr_SqlInstance_Save";
			scUpdate.CommandType = CommandType.StoredProcedure;
			scUpdate.Parameters.Add("@ComputerID", SqlDbType.Int, 4, "ComputerID");
			scUpdate.Parameters.Add("@SqlID", SqlDbType.Int, 4, "SqlID");
			scUpdate.Parameters.Add("@SqlName", SqlDbType.NVarChar, 50, "SqlName");
			scUpdate.Parameters.Add("@ParentID", SqlDbType.Int, 4, "ParentID");
			scUpdate.Parameters.Add("@SqlType", SqlDbType.Int, 4, "SqlType");
			scUpdate.Parameters.Add("@IP", SqlDbType.NVarChar, 50, "IP");
			scUpdate.Parameters.Add("@SqlPort", SqlDbType.Int, 4, "SqlPort");
			scUpdate.Parameters.Add("@UserId", SqlDbType.NVarChar, 50, "UserId");
			scUpdate.Parameters.Add("@PwdC", SqlDbType.NVarChar, 500, "PwdC");
			scUpdate.Parameters["@SqlID"].Direction = ParameterDirection.InputOutput;

			scInsert.Connection = _SqlConn;
			scInsert.CommandText = "dbo.ApqDBMgr_SqlInstance_Save";
			scInsert.CommandType = CommandType.StoredProcedure;
			scInsert.Parameters.Add("@ComputerID", SqlDbType.Int, 4, "ComputerID");
			scInsert.Parameters.Add("@SqlID", SqlDbType.Int, 4, "SqlID");
			scInsert.Parameters.Add("@SqlName", SqlDbType.NVarChar, 50, "SqlName");
			scInsert.Parameters.Add("@ParentID", SqlDbType.Int, 4, "ParentID");
			scInsert.Parameters.Add("@SqlType", SqlDbType.Int, 4, "SqlType");
			scInsert.Parameters.Add("@IP", SqlDbType.NVarChar, 50, "IP");
			scInsert.Parameters.Add("@SqlPort", SqlDbType.Int, 4, "SqlPort");
			scInsert.Parameters.Add("@UserId", SqlDbType.NVarChar, 50, "UserId");
			scInsert.Parameters.Add("@PwdC", SqlDbType.NVarChar, 500, "PwdC");
			scInsert.Parameters["@SqlID"].Direction = ParameterDirection.InputOutput;
		}
		/// <summary>
		/// 初始数据(如Lookup数据等)
		/// </summary>
		/// <param name="ds"></param>
		public override void InitData(DataSet ds)
		{
			#region 准备数据集结构
			#endregion

			#region 加载所有字典表
			//computerTypeTableAdapter1.Fill(Sqls.ComputerType);
			//sqlTypeTableAdapter1.Fill(Sqls.SqlType);
			//dbcTypeTableAdapter1.Fill(Sqls.DBCType);
			#endregion
		}
		/// <summary>
		/// 加载数据
		/// </summary>
		/// <param name="ds"></param>
		public override void LoadData(DataSet ds)
		{
			/* 多表填充示例代码
			sda = new SqlDataAdapter(@"
EXEC dbo.ApqDBMgr_Computer_List;
EXEC dbo.ApqDBMgr_SqlInstance_List;
EXEC dbo.ApqDBMgr_DBC_List;
", _SqlConn);
			sda.TableMappings.Add("Computer1", "SqlInstance");
			sda.TableMappings.Add("Computer2", "DBC");
			 * */
			Sqls.SqlInstance.Clear();
			sda.Fill(Sqls.SqlInstance);
			Sqls.SqlInstance.AcceptChanges();
			bsiInfo.Caption = "加载成功";
		}
		/// <summary>
		/// 显示数据
		/// </summary>
		public override void ShowData()
		{
			#region 设置Lookup
			//luComputerType.DataSource = Sqls;
			//luComputerType.DisplayMember = "ComputerType.TypeCaption";
			//luComputerType.ValueMember = "ComputerType.ComputerType";
			#endregion

			//gridControl1.DataSource = Sqls;
			//gridControl1.DataMember = "Computer";
		}

		#endregion
	}
}