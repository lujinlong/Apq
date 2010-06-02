using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Apq.Data.Dev
{
	/// <summary>
	/// DataSet
	/// </summary>
	public sealed class DataSet
	{
		/// <summary>
		/// 数据库连接
		/// </summary>
		public System.Data.Common.DbConnection Connection;
		/// <summary>
		/// 获取存储表的表名("Apq_Tables")
		/// </summary>
		public string Apq_Tables
		{
			get { return "Apq_Tables"; }
		}
		/// <summary>
		/// 获取存储列的表名("Apq_Columns")
		/// </summary>
		public string Apq_Columns
		{
			get { return "Apq_Columns"; }
		}

		/// <summary>
		/// 须先设置 Connection
		/// </summary>
		public DataSet()
		{
		}

		/// <summary>
		/// 更新 Apq_Tables, Apq_Columns 表内容
		/// </summary>
		/// <param name="DBName">目标数据库名</param>
		/// <returns>影响行数</returns>
		public int Refresh(string DBName)
		{
			#region 参数检测
			if (string.IsNullOrEmpty(DBName))
			{
				throw new ArgumentNullException("DBName");
			}
			#endregion

			System.Data.Common.DbCommand Command = Connection.CreateCommand();
			Apq.Data.Common.DbCommandHelper CommandHelper = new Common.DbCommandHelper(Command);
			CommandHelper.AddParameter("@Apq_Tables", Apq_Tables);
			Command.CommandText = "SELECT OBJECT_ID( @Apq_Tables )";

			Common.DbConnectionHelper.Open(Connection);
			#region Apq_Tables
			if (System.Convert.IsDBNull(Command.ExecuteScalar()))
			{
				Command.CommandText = string.Format(Sqls.Apq_TablesCreate,
					Apq.Data.SqlClient.Common.EncodeString(Apq_Tables),
					Apq.Data.SqlClient.Common.EncodeString(Apq_Columns),
					Apq.Data.SqlClient.Common.EncodeString(DBName)
				);
				Command.ExecuteNonQuery();
			}

			Command.CommandText = string.Format(Sqls.Apq_TablesUpdate,
				Apq.Data.SqlClient.Common.EncodeString(Apq_Tables),
				Apq.Data.SqlClient.Common.EncodeString(Apq_Columns),
				Apq.Data.SqlClient.Common.EncodeString(DBName)
			);
			Command.ExecuteNonQuery();
			#endregion

			#region Apq_Columns
			CommandHelper.AddParameter("@Columns", Apq_Columns);
			Command.CommandText = "SELECT OBJECT_ID( @Columns )";
			if (System.Convert.IsDBNull(Command.ExecuteScalar()))
			{
				Command.CommandText = string.Format(Sqls.Apq_ColumnsCreate,
					Apq.Data.SqlClient.Common.EncodeString(Apq_Tables),
					Apq.Data.SqlClient.Common.EncodeString(Apq_Columns),
					Apq.Data.SqlClient.Common.EncodeString(DBName)
				);
				Command.ExecuteNonQuery();
			}

			Command.CommandText = string.Format(Sqls.Apq_ColumnsUpdate,
				Apq.Data.SqlClient.Common.EncodeString(Apq_Tables),
				Apq.Data.SqlClient.Common.EncodeString(Apq_Columns),
				Apq.Data.SqlClient.Common.EncodeString(DBName)
			);
			return Command.ExecuteNonQuery();
			#endregion
		}

		/// <summary>
		/// 填充 DataSet
		/// </summary>
		/// <param name="ds">目标 DataSet</param>
		/// <returns></returns>
		public int Fill(System.Data.DataSet ds)
		{
			int nRtn = 0;

			Common.DbConnectionHelper ConnectionHelper = new Common.DbConnectionHelper(Connection);
			System.Data.Common.DbDataAdapter Adapter = ConnectionHelper.CreateAdapter();
			ConnectionHelper.Open();

			#region 表
			Adapter.SelectCommand.CommandText = string.Format("SELECT * FROM {0}",
				Apq.Data.SqlClient.Common.EncodeString(Apq_Tables)
			);
			nRtn += Adapter.Fill(ds, Apq_Tables);
			#endregion

			#region 列
			Adapter.SelectCommand.CommandText = string.Format("SELECT * FROM {0}",
				Apq.Data.SqlClient.Common.EncodeString(Apq_Columns)
			);
			nRtn += Adapter.Fill(ds, Apq_Columns);
			#endregion

			return nRtn;
		}
	}
}
