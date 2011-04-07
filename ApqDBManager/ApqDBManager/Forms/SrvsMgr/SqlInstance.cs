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
	public partial class SqlInstance : Apq.Windows.Forms.DockForm
	{
		//数据库连接
		private SqlConnection _SqlConn = new SqlConnection();
		public Apq.DBC.XSD Sqls
		{
			get
			{
				if (!(FormDataSet is Apq.DBC.XSD))
				{
					DBServer dbServer = Apq.Windows.Forms.SingletonForms.GetInstance(typeof(DBServer)) as DBServer;
					FormDataSet = dbServer.Sqls;
				}
				return FormDataSet as Apq.DBC.XSD;
			}
		}
		private Form formDBC = null;

		public SqlInstance()
		{
			InitializeComponent();
		}

		private void SqlInstance_Load(object sender, EventArgs e)
		{
			#region 添加图标
			this.bbiSaveToDB.Glyph = System.Drawing.Image.FromFile(@"Res\png\File\Save.png");
			#endregion

			Apq.Xtra.TreeList.Common.AddBehaivor(treeList1);

			Sqls.SqlInstance.TableNewRow += new DataTableNewRowEventHandler(SqlInstance_TableNewRow);
		}

		void SqlInstance_TableNewRow(object sender, DataTableNewRowEventArgs e)
		{
			e.Row["ComputerID"] = 0;
			e.Row["SqlID"] = 0;
			if (treeList1.FocusedNode != null)
			{
				e.Row["ParentID"] = treeList1.FocusedNode["SqlID"];
			}
			e.Row["SqlName"] = "新建实例[名称]";
			e.Row["SqlType"] = 1;
			e.Row["IP"] = string.Empty;
			e.Row["SqlPort"] = 0;
			e.Row["UserId"] = "apq";
			e.Row["PwdD"] = "f";
		}

		private void SqlInstance_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (formDBC != null)
			{
				formDBC.Close();
			}
			Apq.Windows.Forms.SingletonForms.ReleaseInstance(this.GetType());
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
			DataRow dr = Sqls.SqlInstance.NewRow();
			Sqls.SqlInstance.Rows.Add(dr);
		}

		private void tsmiDel_Click(object sender, EventArgs e)
		{
			if (treeList1.FocusedNode != null)
			{
				treeList1.BeginUpdate();
				treeList1.Nodes.Remove(treeList1.FocusedNode);
				treeList1.EndUpdate();
			}
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

				string strServerName = Apq.Convert.ChangeType<string>(tln["IP"]);
				if (Apq.Convert.ChangeType<int>(tln["SqlPort"]) > 0)
				{
					strServerName += "," + Apq.Convert.ChangeType<int>(tln["SqlPort"]);
				}
				string strConn = Apq.ConnectionStrings.SQLServer.SqlConnection.GetConnectionString(
					strServerName,
					Apq.Convert.ChangeType<string>(tln["UserId"]),
					strPwdD
					);
				SqlConnection sc = new SqlConnection(strConn);
				try
				{
					Apq.Data.Common.DbConnectionHelper.Open(sc);
					bsiTest.Caption = tln.GetDisplayText("SqlName") + "-->连接成功.";
				}
				catch
				{
					bsiTest.Caption = tln.GetDisplayText("SqlName") + "-X-连接失败!";
				}
				finally
				{
					Apq.Data.Common.DbConnectionHelper.Close(sc);
				}
			}
		}
		#endregion

		private void bbiDBC_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			formDBC = Apq.Windows.Forms.SingletonForms.GetInstance(typeof(DBC));
			DBC f = formDBC as DBC;
			if (f != null)
			{
				f.Show(GlobalObject.MainForm.dockPanel1);
			}
		}

		#region IDataShow 成员
		/// <summary>
		/// 前期准备(如数据库连接或文件等)
		/// </summary>
		public override void InitDataBefore()
		{
			#region 数据库连接
			_SqlConn.ConnectionString = GlobalObject.SqlConn
;
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
			Sqls.SqlInstance.Clear();
			sda.Fill(Sqls.SqlInstance);
			#region 密码解密
			//解密密码,生成连接字符串
			foreach (Apq.DBC.XSD.SqlInstanceRow dr in Sqls.SqlInstance.Rows)
			{
				if (!Apq.Convert.LikeDBNull(dr["PwdC"]))
				{
					dr.PwdD = Apq.Security.Cryptography.DESHelper.DecryptString(dr.PwdC, GlobalObject.RegConfigChain["Crypt", "DESKey"], GlobalObject.RegConfigChain["Crypt", "DESIV"]);
				}
			}
			#endregion
			Sqls.SqlInstance.AcceptChanges();
			bsiOutInfo.Caption = "加载成功";
		}
		/// <summary>
		/// 显示数据
		/// </summary>
		public override void ShowData()
		{
			#region 设置Lookup
			luComputer.DisplayMember = "ComputerName";
			luComputer.ValueMember = "ComputerID";
			luComputer.DataSource = Sqls.Computer;
			luSqlType.DisplayMember = "TypeCaption";
			luSqlType.ValueMember = "SqlType";
			luSqlType.DataSource = Sqls.SqlType;
			#endregion

			treeList1.DataMember = "SqlInstance";
			treeList1.DataSource = Sqls;
		}

		#endregion

		private void bbiSelectAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			treeList1.BeginUpdate();
			foreach (DataRow dr in Sqls.SqlInstance.Rows)
			{
				dr["CheckState"] = 1;
			}
			treeList1.EndUpdate();
		}

		private void bbiReverse_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			treeList1.BeginUpdate();
			foreach (DataRow dr in Sqls.SqlInstance.Rows)
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
			treeList1.EndUpdate();
		}

		private void bbiSlts_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{

		}

		//刷新
		private void bbiRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			LoadData(FormDataSet);
		}

		//保存
		private void bbiSaveToDB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (sda == null) return;

			// 密码加密
			DataRow[] drs = Sqls.SqlInstance.Select("1=1", "SqlID ASC", DataViewRowState.Added | DataViewRowState.ModifiedCurrent);
			if (drs != null && drs.Length > 0)
			{
				foreach (DataRow dr in drs)
				{
					if (!Apq.Convert.LikeDBNull(dr["PwdD"]))
					{
						dr["PwdC"] = Apq.Security.Cryptography.DESHelper.EncryptString(Apq.Convert.ChangeType<string>(dr["PwdD"]),
							GlobalObject.RegConfigChain["Crypt", "DESKey"], GlobalObject.RegConfigChain["Crypt", "DESIV"]);
					}
				}

				sda.Update(Sqls.SqlInstance);
				Sqls.SqlInstance.AcceptChanges();
			}
			bsiOutInfo.Caption = "更新成功";
		}

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
	}
}