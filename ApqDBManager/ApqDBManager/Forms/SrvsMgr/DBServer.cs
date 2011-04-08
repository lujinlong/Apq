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
		private Form formSqlInstance = null;

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

			Sqls.Computer.TableNewRow += new DataTableNewRowEventHandler(Computer_TableNewRow);
		}

		void Computer_TableNewRow(object sender, DataTableNewRowEventArgs e)
		{
			e.Row["ComputerID"] = 0;
			e.Row["ComputerName"] = "新建服务器[名称]";
			e.Row["ComputerType"] = 1;
		}

		private void DBServer_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (formSqlInstance != null)
			{
				formSqlInstance.Close();
			}
			Apq.Windows.Forms.SingletonForms.ReleaseInstance(this.GetType());
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
			formSqlInstance = Apq.Windows.Forms.SingletonForms.GetInstance(typeof(SqlInstance));
			SqlInstance f = formSqlInstance as SqlInstance;
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
			_SqlConn.ConnectionString = GlobalObject.SqlConn;
			#endregion

			// 为sda设置SqlCommand
			sqlCommand1.CommandText = "SELECT ComputerType, TypeCaption FROM dic.ComputerType";
			sqlCommand1.CommandType = CommandType.Text;
			sqlCommand1.Connection = _SqlConn;

			sqlCommand2.CommandText = "SELECT [SqlType],[TypeCaption] FROM [dic].[SqlType]";
			sqlCommand2.CommandType = CommandType.Text;
			sqlCommand2.Connection = _SqlConn;

			sqlCommand3.CommandText = "SELECT [DBCType],[TypeCaption] FROM [dic].[DBCType]";
			sqlCommand3.CommandType = CommandType.Text;
			sqlCommand3.Connection = _SqlConn;

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
			Sqls.SqlInstance.Columns.Add("CheckState", typeof(int));
			Sqls.SqlInstance.Columns.Add("IsReadyToGo", typeof(bool));
			Sqls.SqlInstance.Columns.Add("Err", typeof(bool));
			Sqls.SqlInstance.Columns.Add("DBConnectionString");

			Sqls.SqlInstance.Columns["CheckState"].DefaultValue = 0;
			Sqls.SqlInstance.Columns["IsReadyToGo"].DefaultValue = false;
			Sqls.SqlInstance.Columns["Err"].DefaultValue = false;
			#endregion

			#region 加载所有字典表
			sqlDataAdapter1.Fill(Sqls.ComputerType);
			sqlDataAdapter2.Fill(Sqls.SqlType);
			sqlDataAdapter3.Fill(Sqls.DBCType);
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
			Sqls.Computer.Clear();
			sda.Fill(Sqls.Computer);
			Sqls.Computer.AcceptChanges();
			bsiOutInfo.Caption = "加载成功";
		}
		/// <summary>
		/// 显示数据
		/// </summary>
		public override void ShowData()
		{
			#region 设置Lookup
			luComputerType.DisplayMember = "TypeCaption";
			luComputerType.ValueMember = "ComputerType";
			luComputerType.DataSource = Sqls.ComputerType;
			#endregion

			gridControl1.DataSource = Sqls;
			gridControl1.DataMember = "Computer";
		}

		#endregion

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