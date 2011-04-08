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

		public DBC()
		{
			InitializeComponent();
		}

		private void DBC_Load(object sender, EventArgs e)
		{
			#region 添加图标
			this.bbiSave.Glyph = System.Drawing.Image.FromFile(@"Res\png\File\Save.png");
			#endregion

			// 加载生成菜单
			foreach (Apq.DBC.XSD.DBCTypeRow dr in Sqls.DBCType.Rows)
			{
				DevExpress.XtraBars.BarButtonItem bbi = new DevExpress.XtraBars.BarButtonItem();
				bbi.Caption = dr.TypeCaption;
				bbi.Tag = dr.DBCType;
				bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbi_ItemClick);
				bsiCreateCSFile.AddItem(bbi);
			}

			Apq.Xtra.Grid.Common.AddBehaivor(gridView1);
		}

		// 生成数据库连接文件
		void bbi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int nDBCType = Apq.Convert.ChangeType<int>(e.Item.Tag);
			DevExpress.XtraBars.BarButtonItem bbi = e.Item as DevExpress.XtraBars.BarButtonItem;
			if (bbi != null && sfd.ShowDialog(this) == DialogResult.OK)
			{
				DataSet ds = new DataSet();
				DataTable dt = Sqls.DBC.Copy();
				ds.Tables.Add(dt);
				for (int i = dt.Rows.Count - 1; i >= 0; i--)
				{
					if (Apq.Convert.ChangeType<int>(dt.Rows[i]["DBCType"]) != nDBCType)
					{
						dt.Rows.RemoveAt(i);
					}
				}
				string csStr = ds.GetXml();
				string desKey = GlobalObject.RegConfigChain["Crypt", "DESKey"];
				string desIV = GlobalObject.RegConfigChain["Crypt", "DESIV"];
				string strCs = Apq.Security.Cryptography.DESHelper.EncryptString(csStr, desKey, desIV);
				File.WriteAllText(sfd.FileName, strCs, Encoding.UTF8);
			}
		}

		private void btnSaveToDB_Click(object sender, EventArgs e)
		{
			if (sda == null) return;

			sda.Update(Sqls.DBC);
			Sqls.DBC.AcceptChanges();
			bsiOutInfo.Caption = "更新成功";
		}

		private void btnCopy_Click(object sender, EventArgs e)
		{
			gridView1.SelectAll();
			gridView1.CopyToClipboard();
		}

		private void DBC_FormClosing(object sender, FormClosingEventArgs e)
		{
			Apq.Windows.Forms.SingletonForms.ReleaseInstance(this.GetType());
		}

		private void btnLoadFromDB_Click(object sender, EventArgs e)
		{
			LoadData(FormDataSet);
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
			luSqlInstance.DisplayMember = "SqlName";
			luSqlInstance.ValueMember = "SqlID";
			luSqlInstance.DataSource = Sqls.SqlInstance;
			luDBCType.DisplayMember = "TypeCaption";
			luDBCType.ValueMember = "DBCType";
			luDBCType.DataSource = Sqls.DBCType;
			#endregion

			gridControl1.DataMember = "DBC";
			gridControl1.DataSource = Sqls;
		}

		#endregion

		//刷新
		private void bbiRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			LoadData(FormDataSet);
		}

		//保存
		private void bbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (sda == null) return;

			// 密码加密
			Common.PwdD2C(Sqls.DBC);

			sda.Update(Sqls.DBC);
			Sqls.DBC.AcceptChanges();
			bsiOutInfo.Caption = "更新成功";
		}

		//全选
		private void bbiSelectAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			gridView1.SelectAll();
		}

		private void bbiSlts_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (gridView1.SelectedRowsCount > 0)
			{
				gridView1.BeginUpdate();
				int[] RowHandles = gridView1.GetSelectedRows();
				foreach (int RowHandle in RowHandles)
				{
					gridView1.SetRowCellValue(RowHandle, gridView1.FocusedColumn, beiStr.EditValue);
				}
				gridView1.EndUpdate();
			}
		}

		private void tsmiTestOpen_Click(object sender, EventArgs e)
		{
			if (gridView1.FocusedRowHandle > -1)
			{
				DataRow dr = gridView1.GetFocusedDataRow();
				string strPwdD = Apq.Convert.ChangeType<string>(dr["PwdD"]);
				if (string.IsNullOrEmpty(strPwdD))
				{
					strPwdD = Apq.Security.Cryptography.DESHelper.DecryptString(dr["PwdC"].ToString(), GlobalObject.RegConfigChain["Crypt", "DESKey"], GlobalObject.RegConfigChain["Crypt", "DESIV"]);
				}

				Apq.DBC.XSD.SqlInstanceRow sr = Sqls.SqlInstance.FindBySqlID(Apq.Convert.ChangeType<int>(dr["SqlID"]));
				string strServerName = sr.IP;
				if (sr.SqlPort > 0)
				{
					strServerName += "," + sr.SqlPort;
				}
				string strConn = Apq.ConnectionStrings.SQLServer.SqlConnection.GetConnectionString(
					strServerName,
					Apq.Convert.ChangeType<string>(dr["UserId"]),
					strPwdD,
					Apq.Convert.ChangeType<string>(dr["DBName"])
					);
				SqlConnection sc = new SqlConnection(strConn);
				try
				{
					Apq.Data.Common.DbConnectionHelper.Open(sc);
					bsiTest.Caption = dr["DBName"] + "-->连接成功.";
				}
				catch
				{
					bsiTest.Caption = dr["DBName"] + "-X-连接失败!";
				}
				finally
				{
					Apq.Data.Common.DbConnectionHelper.Close(sc);
				}
			}
		}

		private void tsmiDel_Click(object sender, EventArgs e)
		{
			if (gridView1.FocusedRowHandle > -1)
			{
				gridView1.BeginUpdate();
				gridView1.DeleteRow(gridView1.FocusedRowHandle);
				gridView1.EndUpdate();
			}
		}

	}
}