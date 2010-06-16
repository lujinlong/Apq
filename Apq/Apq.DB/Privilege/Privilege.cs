using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Apq.DB.Privilege
{
	/// <summary>
	/// 权限项
	/// </summary>
	public class Privilege
	{
		#region 列表权限项树
		/// <summary>
		/// 列表权限项树
		/// </summary>
		/// <param name="PID">根</param>
		/// <returns></returns>
		public static System.Data.DataSet ApqPrivilege_List(long PID)
		{
			System.Data.DataSet ds = new System.Data.DataSet();

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
			{
				SqlDataAdapter sda = new SqlDataAdapter("dbo.ApqPrivilege_List", SqlConn);
				sda.SelectCommand.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sda.SelectCommand);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("PID", PID, DbType.Int64);
				sda.SelectCommand.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
				SqlConn.Open();
				sda.Fill(ds);
				//stReturn.NReturn = System.Convert.ToInt32(sc.Parameters["rtn"].Value);

				SqlConn.Close();
			}

			return ds;
		}
		#endregion

		#region 编辑
		/// <summary>
		/// 编辑
		/// </summary>
		public static STReturn ApqPrivilege_Edit(long PID, long ParentID, string PName, string Remark)
		{
			STReturn stReturn = new STReturn();

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
			{
				SqlCommand sc = new SqlCommand("dbo.ApqPrivilege_Edit", SqlConn);
				sc.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sc);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("PID", PID, DbType.Int64);
				dch.AddParameter("ParentID", ParentID, DbType.Int64);
				dch.AddParameter("PName", PName);
				dch.AddParameter("Remark", Remark);
				sc.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
				sc.Parameters["PID"].Direction = ParameterDirection.InputOutput;
				sc.Parameters["ParentID"].Direction = ParameterDirection.InputOutput;
				sc.Parameters["PName"].Direction = ParameterDirection.InputOutput;
				sc.Parameters["Remark"].Direction = ParameterDirection.InputOutput;
				SqlConn.Open();
				sc.ExecuteNonQuery();

				stReturn.NReturn = System.Convert.ToInt32(sc.Parameters["rtn"].Value);
				stReturn.POuts = new object[]{
					sc.Parameters["PID"].Value,
					sc.Parameters["ParentID"].Value,
					sc.Parameters["PName"].Value,
					sc.Parameters["Remark"].Value
				};
				stReturn.ExMsg = sc.Parameters["ExMsg"].Value.ToString();

				SqlConn.Close();
			}

			return stReturn;
		}
		#endregion

		#region 删除
		/// <summary>
		/// 删除
		/// </summary>
		public static STReturn ApqUser_Delete(long PID, long ParentID)
		{
			STReturn stReturn = new STReturn();

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
			{
				SqlCommand sc = new SqlCommand("dbo.ApqUser_Delete", SqlConn);
				sc.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sc);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("PID", PID, DbType.Int64);
				dch.AddParameter("ParentID", ParentID, DbType.Int64);
				sc.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
				SqlConn.Open();
				sc.ExecuteNonQuery();

				stReturn.NReturn = System.Convert.ToInt32(sc.Parameters["rtn"].Value);

				SqlConn.Close();
			}

			return stReturn;
		}
		#endregion
	}
}
