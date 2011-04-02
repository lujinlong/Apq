using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Data.SqlClient;

namespace ApqDBManager.Forms.SrvsMgr
{
	public partial class DBServer : Apq.Windows.Forms.DockForm
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

		public DBServer()
		{
			InitializeComponent();
		}

		private void DBServer_Load(object sender, EventArgs e)
		{
			#region 添加图标
			this.bbiSaveToDB.Glyph = System.Drawing.Image.FromFile(@"Res\png\File\Save.png");
			#endregion

			Apq.Xtra.Grid.Common.AddBehaivor(gridView1);

			gridView1.NewItemRowText = "0,新建服务器[名称],1";
			//Sqls.Computer.TableNewRow += new DataTableNewRowEventHandler(Computer_TableNewRow);
		}

		void Computer_TableNewRow(object sender, DataTableNewRowEventArgs e)
		{
			e.Row["ComputerID"] = 0;
			e.Row["ComputerName"] = "新建服务器[名称]";
			e.Row["ComputerType"] = 1;
		}

		private void DBServer_FormClosing(object sender, FormClosingEventArgs e)
		{
			//foreach (SqlInstance form in SqlInstances)
			//{
			//    form.Close();
			//}
		}

		//刷新
		private void bbiRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			LoadData(FormDataSet);
		}

		//全选
		private void bbiSelectAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			gridView1.SelectAll();
		}

		//设置多行
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

		//保存
		private void bbiSaveToDB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (sda == null) return;

			sda.Update(Sqls.Computer);
			Sqls.Computer.AcceptChanges();
			bsiOutInfo.Caption = "更新成功";
		}

		private void bbiSqlInstance_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Form win = Apq.Windows.Forms.SingletonForms.GetInstance(typeof(SqlInstance));
			SqlInstance f = win as SqlInstance;
			f.FormDataSet = Sqls;
			win.Show(GlobalObject.MainForm.dockPanel1);
		}

		#region IDataShow 成员
		/// <summary>
		/// 前期准备(如数据库连接或文件等)
		/// </summary>
		public override void InitDataBefore()
		{
			#region 数据库连接
			Apq.ConnectionStrings.SQLServer.SqlConnection scHelper = new Apq.ConnectionStrings.SQLServer.SqlConnection();
			scHelper.DBName = GlobalObject.XmlConfigChain["ApqDBManager.Controls.MainOption.DBC", "DBName"];
			scHelper.ServerName = GlobalObject.XmlConfigChain["ApqDBManager.Controls.MainOption.DBC", "ServerName"];
			scHelper.UserId = GlobalObject.XmlConfigChain["ApqDBManager.Controls.MainOption.DBC", "UserId"];
			string PwdC = GlobalObject.XmlConfigChain["ApqDBManager.Controls.MainOption.DBC", "Pwd"];
			string PwdD = Apq.Security.Cryptography.DESHelper.DecryptString(PwdC, GlobalObject.RegConfigChain["Crypt", "DESKey"], GlobalObject.RegConfigChain["Crypt", "DESIV"]);
			scHelper.Pwd = PwdD;
			_SqlConn.ConnectionString = scHelper.GetConnectionString();
			#endregion

			// 为sda设置SqlCommand
			scSelect.CommandText = "dbo.ApqDBMgr_Computer_List";
			scSelect.CommandType = CommandType.StoredProcedure;
			scSelect.Connection = _SqlConn;

			scDelete.CommandText = "dbo.ApqDBMgr_Computer_Delete";
			scDelete.CommandType = CommandType.StoredProcedure;
			scDelete.Parameters.Add("@ComputerID", SqlDbType.Int, 4, "ComputerID");
			scDelete.Connection = _SqlConn;

			scUpdate.CommandText = "dbo.ApqDBMgr_Computer_Save";
			scUpdate.CommandType = CommandType.StoredProcedure;
			scUpdate.Parameters.Add("@ComputerID", SqlDbType.Int, 4, "ComputerID");
			scUpdate.Parameters.Add("@ComputerName", SqlDbType.NVarChar, 50, "ComputerName");
			scUpdate.Parameters.Add("@ComputerType", SqlDbType.Int, 4, "ComputerType");
			scUpdate.Parameters["@ComputerID"].Direction = ParameterDirection.InputOutput;
			scUpdate.Connection = _SqlConn;

			scInsert.CommandText = "dbo.ApqDBMgr_Computer_Save";
			scInsert.CommandType = CommandType.StoredProcedure;
			scInsert.Parameters.Add("@ComputerID", SqlDbType.Int, 4, "ComputerID");
			scInsert.Parameters.Add("@ComputerName", SqlDbType.NVarChar, 50, "ComputerName");
			scInsert.Parameters.Add("@ComputerType", SqlDbType.Int, 4, "ComputerType");
			scInsert.Parameters["@ComputerID"].Direction = ParameterDirection.InputOutput;
			scInsert.Connection = _SqlConn;
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
			computerTypeTableAdapter1.Fill(Sqls.ComputerType);
			sqlTypeTableAdapter1.Fill(Sqls.SqlType);
			dbcTypeTableAdapter1.Fill(Sqls.DBCType);
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
			sda.Fill(Sqls.Computer);
			bsiOutInfo.Caption = "加载成功";
		}
		/// <summary>
		/// 显示数据
		/// </summary>
		public override void ShowData()
		{
			#region 设置Lookup
			luComputerType.DataSource = Sqls;
			luComputerType.DisplayMember = "ComputerType.TypeCaption";
			luComputerType.ValueMember = "ComputerType.ComputerType";
			#endregion

			gridControl1.DataSource = Sqls;
			gridControl1.DataMember = "Computer";
		}

		#endregion
	}
}