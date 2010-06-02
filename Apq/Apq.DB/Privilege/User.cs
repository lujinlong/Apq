using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Apq.DB.Privilege
{
	/// <summary>
	/// 用户
	/// </summary>
	public class User
	{
		#region 注册
		/// <summary>
		/// 以用户名注册用户
		/// </summary>
		/// <param name="NReturn">返回值</param>
		/// <param name="ExMsg">返回信息</param>
		/// <param name="LoginName">用户名</param>
		/// <param name="binPwd">密码</param>
		/// <param name="IDCard">身份证号</param>
		/// <param name="IDCardName">身份证姓名</param>
		/// <param name="Sex">性别</param>
		/// <param name="IDCardPhotoUrl">身份证照片</param>
		/// <param name="Users_Name">名字</param>
		/// <param name="Users_PhotoUrl">头像</param>
		/// <param name="Expire">过期时间</param>
		/// <param name="IsAdmin">是否管理员</param>
		/// <param name="Birthday">生日</param>
		/// <param name="UserID">用户编号</param>
		/// <returns></returns>
		public static void Apq_Reg_LoginName(ref int NReturn, ref string ExMsg, string LoginName, byte[] binPwd, string IDCard, string IDCardName, short Sex
			, string IDCardPhotoUrl, string Users_Name, string Users_PhotoUrl, DateTime Expire, short IsAdmin, DateTime Birthday, ref long UserID)
		{
			SqlCommand sc = new SqlCommand("Apq_User.Apq_Reg_LoginName");
			sc.CommandType = CommandType.StoredProcedure;
			Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sc);
			dch.AddParameter("rtn", 0, DbType.Int32);
			dch.AddParameter("ExMsg", ExMsg, DbType.String, -1);
			dch.AddParameter("LoginName", LoginName);
			dch.AddParameter("binPwd", binPwd);
			dch.AddParameter("IDCard", IDCard);
			dch.AddParameter("IDCardName", IDCardName);
			dch.AddParameter("Sex", Sex.ToString());
			dch.AddParameter("IDCardPhotoUrl", IDCardPhotoUrl);
			dch.AddParameter("Users_Name", Users_Name);
			dch.AddParameter("Users_PhotoUrl", Users_PhotoUrl);
			dch.AddParameter("Expire", Expire);
			dch.AddParameter("IsAdmin", IsAdmin);
			dch.AddParameter("Birthday", Birthday);
			dch.AddParameter("UserID", UserID, DbType.Int64);
			sc.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
			sc.Parameters["ExMsg"].Direction = ParameterDirection.InputOutput;
			sc.Parameters["UserID"].Direction = ParameterDirection.InputOutput;

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
			{
				sc.Connection = SqlConn;
				SqlConn.Open();
				sc.ExecuteNonQuery();

				NReturn = System.Convert.ToInt32(sc.Parameters["rtn"].Value);
				ExMsg = sc.Parameters["ExMsg"].Value.ToString();
				UserID = System.Convert.ToInt64(sc.Parameters["UserID"].Value);

				sc.Dispose();
				SqlConn.Close();
			}
		}
		#endregion

		#region 登录
		/// <summary>
		/// 用户名登录
		/// </summary>
		/// <param name="NReturn">返回值</param>
		/// <param name="ExMsg">返回信息</param>
		/// <param name="LoginName">用户名</param>
		/// <param name="binPwd">密码</param>
		/// <returns></returns>
		public static DataSet Apq_Login_LoginName(ref int NReturn, ref string ExMsg, string LoginName, byte[] binPwd)
		{
			DataSet ds = new DataSet();

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
			{
				SqlDataAdapter sda = new SqlDataAdapter("Apq_User.Apq_Login_LoginName", SqlConn);
				sda.SelectCommand.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sda.SelectCommand);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", ExMsg, DbType.String, -1);
				dch.AddParameter("LoginName", LoginName);
				dch.AddParameter("binPwd", binPwd);
				sda.SelectCommand.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
				sda.SelectCommand.Parameters["ExMsg"].Direction = ParameterDirection.InputOutput;
				SqlConn.Open();
				sda.Fill(ds);

				NReturn = System.Convert.ToInt32(sda.SelectCommand.Parameters["rtn"].Value);
				ExMsg = sda.SelectCommand.Parameters["ExMsg"].Value.ToString();

				sda.Dispose();
				SqlConn.Close();
			}

			return ds;
		}
		#endregion
	}
}
