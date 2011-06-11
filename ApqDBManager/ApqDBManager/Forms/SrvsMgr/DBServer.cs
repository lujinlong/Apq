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
		private Form formSqlInstance = null;

		public DBServer()
		{
			InitializeComponent();
		}

		private void DBServer_Load(object sender, EventArgs e)
		{
			#region 添加图标
			this.tsmiSave.Image = System.Drawing.Image.FromFile(@"Res\png\File\Save.png");
			#endregion
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
		private void tsmiRefresh_Click(object sender, EventArgs e)
		{
			LoadData(FormDataSet);
		}

		//全选
		private void tsmiSelectAll_Click(object sender, EventArgs e)
		{
			dataGridView1.SelectAll();
		}

		//设置选中格
		private void tsmiSlts_Click(object sender, EventArgs e)
		{
			if (tstbStr.Text.Trim().Length > 0 && dataGridView1.SelectedCells.Count > 0)
			{
				dataGridView1.BeginEdit(false);
				foreach (DataGridViewCell dgvc in dataGridView1.SelectedCells)
				{
					if (!dgvc.ReadOnly && dataGridView1.Columns[dgvc.ColumnIndex] is DataGridViewTextBoxColumn)
					{
						dgvc.Value = tstbStr.Text;
					}
				}
				dataGridView1.EndEdit();
			}
		}

		//保存
		private void tsmiSave_Click(object sender, EventArgs e)
		{
			if (sda == null) return;

			sda.Update(SrvsMgr_XSD.Computer);
			SrvsMgr_XSD.Computer.AcceptChanges();
			tsslOutInfo.Text = "更新成功";
		}

		private void tsmiSqlInstance_Click(object sender, EventArgs e)
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
			SrvsMgr_XSD.SqlInstance.Columns.Add("CheckState", typeof(int));
			SrvsMgr_XSD.SqlInstance.Columns.Add("IsReadyToGo", typeof(bool));
			SrvsMgr_XSD.SqlInstance.Columns.Add("Err", typeof(bool));
			SrvsMgr_XSD.SqlInstance.Columns.Add("DBConnectionString");

			SrvsMgr_XSD.SqlInstance.Columns["CheckState"].DefaultValue = 0;
			SrvsMgr_XSD.SqlInstance.Columns["IsReadyToGo"].DefaultValue = false;
			SrvsMgr_XSD.SqlInstance.Columns["Err"].DefaultValue = false;
			#endregion

			#region 加载所有字典表
			sqlDataAdapter1.Fill(SrvsMgr_XSD.ComputerType);
			sqlDataAdapter2.Fill(SrvsMgr_XSD.SqlType);
			sqlDataAdapter3.Fill(SrvsMgr_XSD.DBCType);
			#endregion
		}
		/// <summary>
		/// 加载数据
		/// </summary>
		/// <param name="ds"></param>
		public override void LoadData(DataSet ds)
		{
			#region Lookup数据绑定
			//computerTypeDataGridViewComboBoxColumn.DataSource = DBServer_XSD.ComputerType;
			#endregion
			/* 多表填充示例代码
			sda = new SqlDataAdapter(@"
EXEC dbo.ApqDBMgr_Computer_List;
EXEC dbo.ApqDBMgr_SqlInstance_List;
EXEC dbo.ApqDBMgr_DBC_List;
", _SqlConn);
			sda.TableMappings.Add("Computer1", "SqlInstance");
			sda.TableMappings.Add("Computer2", "DBC");
			 * */
			SrvsMgr_XSD.Computer.Clear();
			sda.Fill(SrvsMgr_XSD.Computer);
			SrvsMgr_XSD.Computer.AcceptChanges();
			tsslOutInfo.Text = "加载成功";
		}

		#endregion

		private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			dataGridView1.ClearSelection();
			//+选中整列
			//dataGridView1.Columns[e.ColumnIndex].Selected = true;
		}

	}
}