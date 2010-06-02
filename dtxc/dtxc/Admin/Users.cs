using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace dtxc.Admin
{
	public class Users
	{
		/// <summary>
		/// 获取某个用户及其下级
		/// </summary>
		/// <param name="NReturn"></param>
		/// <param name="ExMsg"></param>
		/// <param name="UserID">UserID</param>
		/// <param name="ContainsSelf">是否包含自己</param>
		/// <param name="ContainsGrand">是否包含子孙</param>
		/// <returns></returns>
		public static DataTable ListChild(ref int NReturn, ref string ExMsg, long UserID, bool ContainsSelf, bool ContainsGrand)
		{
			DataSet ds = new DataSet();

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
			{
				SqlDataAdapter sda = new SqlDataAdapter("dtxc.Apq_Users_ListChild", SqlConn);
				sda.SelectCommand.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sda.SelectCommand);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", ExMsg, DbType.String, -1);
				dch.AddParameter("UserID", UserID, DbType.Int64);
				dch.AddParameter("ContainsSelf", ContainsSelf, DbType.Byte);
				dch.AddParameter("ContainsGrand", ContainsGrand, DbType.Byte);
				sda.SelectCommand.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
				sda.SelectCommand.Parameters["ExMsg"].Direction = ParameterDirection.InputOutput;
				SqlConn.Open();
				sda.Fill(ds);

				NReturn = System.Convert.ToInt32(sda.SelectCommand.Parameters["rtn"].Value);
				ExMsg = sda.SelectCommand.Parameters["ExMsg"].Value.ToString();

				sda.Dispose();
				SqlConn.Close();
			}

			return ds.Tables.Count > 0 ? ds.Tables[0] : null;
		}
	}
}
