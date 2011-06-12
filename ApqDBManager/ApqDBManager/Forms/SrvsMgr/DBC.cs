using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Data.SqlClient;
using System.IO;

namespace ApqDBManager.Forms.SrvsMgr
{
	public partial class DBC : Apq.Windows.Forms.DockForm
	{
		private SqlConnection _SqlConn = new SqlConnection();
		public SrvsMgr_XSD Sqls
		{
			get
			{
				DBServer dbServer = Apq.Windows.Forms.SingletonForms.GetInstance(typeof(DBServer)) as DBServer;
				return dbServer.SrvsMgr_XSD;
			}
		}

		public DBC()
		{
			InitializeComponent();
		}

		private void DBC_Load(object sender, EventArgs e)
		{
			#region 添加图标
			this.tsbSaveToDB.Image = System.Drawing.Image.FromFile(@"Res\png\File\Save.png");
			#endregion

			// 加载生成菜单
			foreach (SrvsMgr_XSD.DBCTypeRow dr in Sqls.DBCType.Rows)
			{
				ToolStripMenuItem tsmi = new ToolStripMenuItem();
				tsmi.Text = dr.TypeCaption;
				tsmi.Tag = dr.DBCType;
				tsmi.Click += new EventHandler(tsmiCreatCSFile_Click);
				tssbCreateCSFile.DropDownItems.Add(tsmi);
			}

			Apq.Windows.Forms.DataGridViewHelper.SetDefaultStyle(dataGridView1);
			Apq.Windows.Forms.DataGridViewHelper.AddBehaivor(dataGridView1);
		}

		// 生成数据库连接文件
		void tsmiCreatCSFile_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem tsb = sender as ToolStripMenuItem;
			sfd.InitialDirectory = GlobalObject.XmlConfigChain[this.GetType(), "sfd_InitialDirectory"];
			if (tsb != null && sfd.ShowDialog(this) == DialogResult.OK)
			{
				GlobalObject.XmlConfigChain[this.GetType(), "sfd_InitialDirectory"] = System.IO.Path.GetDirectoryName(sfd.FileName);

				int nDBCType = Apq.Convert.ChangeType<int>(tsb.Tag);
				DataSet ds = new DataSet("XSD");
				ds.Namespace = Sqls.Namespace;
				DataTable dt = Sqls.DBC.Copy();
				ds.Tables.Add(dt);
				dt.Columns.Add("ServerName");
				dt.Columns.Remove("PwdC");

				for (int i = dt.Rows.Count - 1; i >= 0; i--)
				{
					if (Apq.Convert.ChangeType<int>(dt.Rows[i]["DBCType"]) != nDBCType)
					{
						dt.Rows.RemoveAt(i);
						continue;
					}

					// 计算ServerName
					SrvsMgr_XSD.SqlInstanceRow sqlInstance = Sqls.SqlInstance.FindBySqlID(Apq.Convert.ChangeType<int>(dt.Rows[i]["SqlID"]));
					dt.Rows[i]["ServerName"] = sqlInstance.IP;
					if (sqlInstance != null && sqlInstance.SqlPort > 0)
					{
						dt.Rows[i]["ServerName"] += "," + sqlInstance.SqlPort;
					}
				}
				string csStr = ds.GetXml();
				string desKey = GlobalObject.RegConfigChain["Crypt", "DESKey"];
				string desIV = GlobalObject.RegConfigChain["Crypt", "DESIV"];
				string strCs = Apq.Security.Cryptography.DESHelper.EncryptString(csStr, desKey, desIV);
				File.WriteAllText(sfd.FileName, strCs, Encoding.UTF8);
				tsslOutInfo.Text = "保存文件成功";
			}
		}

		private void tsbSaveToDB_Click(object sender, EventArgs e)
		{
			if (sda == null) return;

			sda.Update(Sqls.DBC);
			Sqls.DBC.AcceptChanges();
			tsslOutInfo.Text = "更新成功";
		}

		private void DBC_FormClosing(object sender, FormClosingEventArgs e)
		{
			Apq.Windows.Forms.SingletonForms.ReleaseInstance(this.GetType());

			Apq.Windows.Forms.DataGridViewHelper.RemoveBehaivor(dataGridView1);
		}

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
			scSelect.CommandText = "dbo.ApqDBMgr_DBC_List";
			scSelect.CommandType = CommandType.StoredProcedure;

			scDelete.Connection = _SqlConn;
			scDelete.CommandText = "dbo.ApqDBMgr_DBC_Delete";
			scDelete.CommandType = CommandType.StoredProcedure;
			scDelete.Parameters.Add("@DBID", SqlDbType.Int, 4, "DBID");

			scUpdate.Connection = _SqlConn;
			scUpdate.CommandText = "dbo.ApqDBMgr_DBC_Save";
			scUpdate.CommandType = CommandType.StoredProcedure;
			scUpdate.Parameters.Add("@SqlID", SqlDbType.Int, 4, "SqlID");
			scUpdate.Parameters.Add("@DBID", SqlDbType.Int, 4, "DBID");
			scUpdate.Parameters.Add("@DBCType", SqlDbType.Int, 4, "DBCType");
			scUpdate.Parameters.Add("@UseTrusted", SqlDbType.TinyInt, 1, "UseTrusted");
			scUpdate.Parameters.Add("@DBName", SqlDbType.NVarChar, 50, "DBName");
			scUpdate.Parameters.Add("@UserId", SqlDbType.NVarChar, 50, "UserId");
			scUpdate.Parameters.Add("@PwdC", SqlDbType.NVarChar, 500, "PwdC");
			scUpdate.Parameters.Add("@Mirror", SqlDbType.NVarChar, 1000, "Mirror");
			scUpdate.Parameters.Add("@Option", SqlDbType.NVarChar, 1000, "Option");
			scUpdate.Parameters["@DBID"].Direction = ParameterDirection.InputOutput;

			scInsert.Connection = _SqlConn;
			scInsert.CommandText = "dbo.ApqDBMgr_DBC_Save";
			scInsert.CommandType = CommandType.StoredProcedure;
			scInsert.Parameters.Add("@SqlID", SqlDbType.Int, 4, "SqlID");
			scInsert.Parameters.Add("@DBID", SqlDbType.Int, 4, "DBID");
			scInsert.Parameters.Add("@DBCType", SqlDbType.Int, 4, "DBCType");
			scInsert.Parameters.Add("@UseTrusted", SqlDbType.TinyInt, 1, "UseTrusted");
			scInsert.Parameters.Add("@DBName", SqlDbType.NVarChar, 50, "DBName");
			scInsert.Parameters.Add("@UserId", SqlDbType.NVarChar, 50, "UserId");
			scInsert.Parameters.Add("@PwdC", SqlDbType.NVarChar, 500, "PwdC");
			scInsert.Parameters.Add("@Mirror", SqlDbType.NVarChar, 1000, "Mirror");
			scInsert.Parameters.Add("@Option", SqlDbType.NVarChar, 1000, "Option");
			scInsert.Parameters["@DBID"].Direction = ParameterDirection.InputOutput;
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
			Sqls.DBC.Clear();
			sda.Fill(Sqls.DBC);
			//密码解密
			Common.PwdC2D(Sqls.DBC);
			Sqls.DBC.AcceptChanges();
			tsslOutInfo.Text = "加载成功";
		}
		/// <summary>
		/// 显示数据
		/// </summary>
		public override void ShowData()
		{
			#region 设置Lookup
			computerBindingSource.DataMember = "Computer";
			computerBindingSource.DataSource = Sqls;
			sqlInstanceBindingSource.DataMember = "SqlInstance";
			sqlInstanceBindingSource.DataSource = Sqls;
			dBCTypeBindingSource.DataMember = "DBCType";
			dBCTypeBindingSource.DataSource = Sqls;
			#endregion

			dBCBindingSource.DataMember = "DBC";
			dBCBindingSource.DataSource = Sqls;
		}

		#endregion

		//刷新
		private void tsbRefresh_Click(object sender, EventArgs e)
		{
			LoadData(FormDataSet);
		}

		//保存
		private void tsbSave_Click(object sender, EventArgs e)
		{
			if (sda == null) return;

			// 密码加密
			Common.PwdD2C(Sqls.DBC);

			sda.Update(Sqls.DBC);
			Sqls.DBC.AcceptChanges();
			tsslOutInfo.Text = "保存成功";
		}

		//全选
		private void tsbSelectAll_Click(object sender, EventArgs e)
		{
			dataGridView1.SelectAll();
		}

		#region 批量设置
		// 登录名
		private void tsmiSltsUserId_Click(object sender, EventArgs e)
		{
			int cellIndex = Apq.Windows.Forms.DataGridViewHelper.IndexOfHeader(dataGridView1.Columns, "登录名");

			foreach (DataGridViewRow dgvRow in dataGridView1.SelectedRows)
			{
				dgvRow.Cells[cellIndex].Value = tstbStr.Text;
			}
		}

		// 密码
		private void tsmiSltsPwdD_Click(object sender, EventArgs e)
		{
			int cellIndex = Apq.Windows.Forms.DataGridViewHelper.IndexOfHeader(dataGridView1.Columns, "密码");

			foreach (DataGridViewRow dgvRow in dataGridView1.SelectedRows)
			{
				dgvRow.Cells[cellIndex].Value = tstbStr.Text;
			}
		}
		#endregion

		private void tsmiTestOpen_Click(object sender, EventArgs e)
		{
			if (dataGridView1.CurrentRow != null)
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;

					DataRow[] drs = Sqls.DBC.Select("DBID = " + dataGridView1.CurrentRow.Cells["DBID"].Value);
					string strPwdD = Apq.Convert.ChangeType<string>(drs[0]["PwdD"]);
					if (string.IsNullOrEmpty(strPwdD))
					{
						strPwdD = Apq.Security.Cryptography.DESHelper.DecryptString(drs[0]["PwdC"].ToString(), GlobalObject.RegConfigChain["Crypt", "DESKey"], GlobalObject.RegConfigChain["Crypt", "DESIV"]);
					}

					SrvsMgr_XSD.SqlInstanceRow sr = Sqls.SqlInstance.FindBySqlID(Apq.Convert.ChangeType<int>(drs[0]["SqlID"]));
					string strServerName = sr.IP;
					if (sr.SqlPort > 0)
					{
						strServerName += "," + sr.SqlPort;
					}
					string strConn = Apq.ConnectionStrings.SQLServer.SqlConnection.GetConnectionString(
						strServerName,
						Apq.Convert.ChangeType<string>(drs[0]["UserId"]),
						strPwdD,
						Apq.Convert.ChangeType<string>(drs[0]["DBName"])
						);
					SqlConnection sc = new SqlConnection(strConn);
					try
					{
						Apq.Data.Common.DbConnectionHelper.Open(sc);
						tsslTest.Text = drs[0]["DBName"] + "-->连接成功.";
					}
					catch
					{
						tsslTest.Text = drs[0]["DBName"] + "-X-连接失败!";
					}
					finally
					{
						Apq.Data.Common.DbConnectionHelper.Close(sc);
					}
				}
				finally
				{
					this.Cursor = Cursors.Default;
				}
			}
		}
	}
}