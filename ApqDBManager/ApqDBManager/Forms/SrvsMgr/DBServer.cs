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
		//private Dictionary<string, SqlInstance> SqlInstances = new Dictionary<string,SqlInstance>();

		protected string _FileName = string.Empty;
		private SqlConnection _SqlConn = new SqlConnection();

		public DBServer()
		{
			InitializeComponent();
		}

		private void DBServer_Load(object sender, EventArgs e)
		{
			#region 添加图标
			this.bbiSaveToDB.Glyph = System.Drawing.Image.FromFile(@"Res\png\File\Save.png");
			#endregion

			#region 准备数据集结构
			DataTable dt = _Sqls.Tables.Add("Computer");
			dt.Columns.Add("ComputerID", typeof(int));
			dt.Columns.Add("ComputerName");
			dt.Columns.Add("ComputerType", typeof(int));
			#endregion

			Apq.Xtra.Grid.Common.AddBehaivor(gridView1);

			LoadFromDB();
		}

		private void DBServer_FormClosing(object sender, FormClosingEventArgs e)
		{
			//foreach (SqlInstance form in SqlInstances)
			//{
			//    form.Close();
			//}
		}

		/// <summary>
		/// 从数据库加载
		/// </summary>
		public void LoadFromDB()
		{
			#region 从数据库加载服务器列表
			Apq.ConnectionStrings.SQLServer.SqlConnection scHelper = new Apq.ConnectionStrings.SQLServer.SqlConnection();
			scHelper.DBName = GlobalObject.XmlConfigChain["ApqDBManager.Controls.MainOption.DBC", "DBName"];
			scHelper.ServerName = GlobalObject.XmlConfigChain["ApqDBManager.Controls.MainOption.DBC", "ServerName"];
			scHelper.UserId = GlobalObject.XmlConfigChain["ApqDBManager.Controls.MainOption.DBC", "UserId"];
			string PwdC = GlobalObject.XmlConfigChain["ApqDBManager.Controls.MainOption.DBC", "Pwd"];
			string PwdD = Apq.Security.Cryptography.DESHelper.DecryptString(PwdC, GlobalObject.RegConfigChain["Crypt", "DESKey"], GlobalObject.RegConfigChain["Crypt", "DESIV"]);
			scHelper.Pwd = PwdD;
			_SqlConn.ConnectionString = scHelper.GetConnectionString();

			_Sqls.Clear();

			SqlDataAdapter sda = new SqlDataAdapter(@"EXEC dbo.ApqDBMgr_Computer_List;", _SqlConn);
			/* 多表填充示例代码
			SqlDataAdapter sda = new SqlDataAdapter(@"
EXEC dbo.ApqDBMgr_Computer_List;
EXEC dbo.ApqDBMgr_SqlInstance_List;
EXEC dbo.ApqDBMgr_DBC_List;
", _SqlConn);
			sda.TableMappings.Add("Computer1", "SqlInstance");
			sda.TableMappings.Add("Computer2", "DBC");
			 * */
			sda.Fill(_Sqls, "Computer");
			#endregion

			gridControl1.DataSource = _Sqls;

			bsiOutInfo.Caption = "加载成功";

			// 为sda设置SqlCommand
			sda.UpdateCommand = sda.InsertCommand = sda.DeleteCommand = new SqlCommand("dbo.ApqDBMgr_Computer_Save");
			sda.UpdateCommand.CommandType = CommandType.StoredProcedure;
			sda.UpdateCommand.Parameters.Add("@ComputerID", SqlDbType.Int, 4, "ComputerID");
			sda.UpdateCommand.Parameters.Add("@ComputerName", SqlDbType.NVarChar, 50, "ComputerName");
			sda.UpdateCommand.Parameters.Add("@ComputerType", SqlDbType.Int, 4, "ComputerType");
			sda.UpdateCommand.Connection = _SqlConn;
		}

		private void bbiSaveToDB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (sda == null) return;

			sda.Update(_Sqls.Tables["Computer"]);
			_Sqls.AcceptChanges();
			bsiOutInfo.Caption = "更新成功";
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
	}
}