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
using System.Data.Common;
using Apq.TreeListView;

namespace ApqDBManager.Forms.SrvsMgr
{
	public partial class SqlInstance : Apq.Windows.Forms.DockForm
	{
		//数据库连接
		private SqlConnection _SqlConn = new SqlConnection();
		public SrvsMgr_XSD Sqls
		{
			get
			{
				DBServer dbServer = Apq.Windows.Forms.SingletonForms.GetInstance(typeof(DBServer)) as DBServer;
				return dbServer.SrvsMgr_XSD;
			}
		}
		private Form formDBC = null;

		public SqlInstance()
		{
			InitializeComponent();
		}

		private TreeListViewHelper tlvHelper;

		private void SqlInstance_Load(object sender, EventArgs e)
		{
			#region 添加图标
			this.tsbSaveToDB.Image = System.Drawing.Image.FromFile(@"Res\png\File\Save.png");
			#endregion
		}

		private void SqlInstance_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (formDBC != null)
			{
				formDBC.Close();
			}
			Apq.Windows.Forms.SingletonForms.ReleaseInstance(this.GetType());
		}

		#region treeListView1

		private void treeListView1_BeforeLabelEdit(object sender, TreeListViewBeforeLabelEditEventArgs e)
		{
			if (e.Item.ListView.Columns[e.ColumnIndex].Text == "服务器")
			{
				ComboBox cb = new ComboBox();
				cb.DisplayMember = "ComputerName";
				cb.ValueMember = "ComputerID";
				cb.DataSource = Sqls.Computer;

				e.Editor = cb;
			}
		}

		private void treeListView1_AfterLabelEdit(object sender, TreeListViewLabelEditEventArgs e)
		{
			ColumnHeader ch = e.Item.ListView.Columns[e.ColumnIndex];
			DataColumnMapping dcm = tlvHelper.TableMapping.ColumnMappings[ch.Text];

			long SqlID = Apq.Convert.ChangeType<long>(e.Item.SubItems[e.Item.ListView.Columns.Count].Text);
			DataRow[] drs = Sqls.SqlInstance.Select("SqlID = " + SqlID);
			if (drs.Length > 0)
			{
				drs[0][dcm.DataSetColumn] = e.Label;

				if (ch.Text == "服务器")
				{
					drs[0]["ComputerID"] = Sqls.Computer.FindByComputerName(e.Label)["ComputerID"];
				}
			}
		}

		private void treeListView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (e.Node != null)
			{
				tsslOutInfo.Text = e.Node.Text;
			}
		}

		private void tsmiAdd_Click(object sender, EventArgs e)
		{
			DataRow dr = Sqls.SqlInstance.NewRow();
			Sqls.SqlInstance.Rows.Add(dr);
		}

		private void tsmiDel_Click(object sender, EventArgs e)
		{
			if (treeListView1.FocusedItem != null)
			{
				treeListView1.BeginUpdate();
				treeListView1.Items.Remove(treeListView1.FocusedItem);
				treeListView1.EndUpdate();
			}
		}

		private void tsmiTestOpen_Click(object sender, EventArgs e)
		{
			TreeListViewItem tln = treeListView1.FocusedItem;
			if (tln != null && tln.Parent != null)
			{
				string strPwdD = Apq.Convert.ChangeType<string>(tln.SubItems["PwdD"]);
				if (string.IsNullOrEmpty(strPwdD))
				{
					strPwdD = Apq.Security.Cryptography.DESHelper.DecryptString(tln.SubItems["PwdC"].ToString(), GlobalObject.RegConfigChain["Crypt", "DESKey"], GlobalObject.RegConfigChain["Crypt", "DESIV"]);
				}

				string strServerName = Apq.Convert.ChangeType<string>(tln.SubItems["IP"]);
				if (Apq.Convert.ChangeType<int>(tln.SubItems["SqlPort"]) > 0)
				{
					strServerName += "," + Apq.Convert.ChangeType<int>(tln.SubItems["SqlPort"]);
				}
				string strConn = Apq.ConnectionStrings.SQLServer.SqlConnection.GetConnectionString(
					strServerName,
					Apq.Convert.ChangeType<string>(tln.SubItems["UserId"]),
					strPwdD
					);
				SqlConnection sc = new SqlConnection(strConn);
				try
				{
					Apq.Data.Common.DbConnectionHelper.Open(sc);
					tsslTest.Text = tln.SubItems["SqlName"] + "-->连接成功.";
				}
				catch
				{
					tsslTest.Text = tln.SubItems["SqlName"] + "-X-连接失败!";
				}
				finally
				{
					Apq.Data.Common.DbConnectionHelper.Close(sc);
				}
			}
		}
		#endregion

		private void tsbDBC_Click(object sender, EventArgs e)
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
			tlvHelper = new TreeListViewHelper(treeListView1);
			tlvHelper.TableMapping.ColumnMappings.Add("名称", "SqlName");
			tlvHelper.TableMapping.ColumnMappings.Add("服务器", "ComputerName");
			tlvHelper.TableMapping.ColumnMappings.Add("登录名", "UserId");
			tlvHelper.TableMapping.ColumnMappings.Add("密码", "PwdD");
			tlvHelper.TableMapping.ColumnMappings.Add("IP", "IP");
			tlvHelper.TableMapping.ColumnMappings.Add("SQL端口", "SqlPort");
			tlvHelper.TableMapping.ColumnMappings.Add("编号", "SqlID");
			tlvHelper.TableMapping.ColumnMappings.Add("上级编号", "ParentID");
			tlvHelper.Key = "SqlID";
			tlvHelper.HiddenColNames = new List<string>(new string[] { "SqlID" });

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
			// 密码解密
			Common.PwdC2D(Sqls.SqlInstance);
			Sqls.SqlInstance.AcceptChanges();
			tsslOutInfo.Text = "加载成功";
		}
		/// <summary>
		/// 显示数据
		/// </summary>
		public override void ShowData()
		{
			#region 设置Lookup
			//luComputer.DisplayMember = "ComputerName";
			//luComputer.ValueMember = "ComputerID";
			//luComputer.DataSource = Sqls.Computer;
			//luSqlType.DisplayMember = "TypeCaption";
			//luSqlType.ValueMember = "SqlType";
			//luSqlType.DataSource = Sqls.SqlType;
			#endregion

			//treeListView1.DataMember = "SqlInstance";
			//treeListView1.DataSource = Sqls;

			treeListView1.Items.Clear();
			if (Sqls.SqlInstance.Rows.Count > 0)
			{
				tlvHelper.BindDataTable(Sqls.SqlInstance);
				treeListView1.ExpandAll();
			}
		}

		#endregion

		private void tsbSelectAll_Click(object sender, EventArgs e)
		{
		}

		private void tsbReverse_Click(object sender, EventArgs e)
		{
		}

		//刷新
		private void tsbRefresh_Click(object sender, EventArgs e)
		{
			LoadData(FormDataSet);
		}

		//保存
		private void tsbSaveToDB_Click(object sender, EventArgs e)
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
			tsslOutInfo.Text = "更新成功";
		}

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
	}
}