using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Apq.DB.Privilege
{
	/// <summary>
	/// 用户(或角色)
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
		public static void Apq_Reg_LoginName(ref int NReturn, ref string ExMsg, string LoginName, byte[] binPwd, string IDCard, string IDCardName, byte Sex
			, string IDCardPhotoUrl, string Users_Name, string Users_PhotoUrl, DateTime Expire, byte IsAdmin, DateTime Birthday, ref long UserID)
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
			dch.AddParameter("Sex", Sex, DbType.Byte);
			dch.AddParameter("IDCardPhotoUrl", IDCardPhotoUrl);
			dch.AddParameter("Users_Name", Users_Name);
			dch.AddParameter("Users_PhotoUrl", Users_PhotoUrl);
			dch.AddParameter("Expire", Expire);
			dch.AddParameter("IsAdmin", IsAdmin, DbType.Byte);
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
		public static STReturn ApqUser_Login(int UserSrc, string UserName)
		{
			STReturn stReturn = new STReturn();

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
			{
				SqlCommand sc = new SqlCommand("dbo.ApqUser_Login", SqlConn);
				sc.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sc);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", string.Empty, DbType.String, -1);
				dch.AddParameter("UserSrc", UserSrc, DbType.Int32);
				dch.AddParameter("UserName", UserName);
				dch.AddParameter("UserID", 0, DbType.Int64);
				sc.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
				sc.Parameters["ExMsg"].Direction = ParameterDirection.InputOutput;
				sc.Parameters["UserID"].Direction = ParameterDirection.InputOutput;
				SqlConn.Open();
				sc.ExecuteNonQuery();

				stReturn.NReturn = System.Convert.ToInt32(sc.Parameters["rtn"].Value);
				stReturn.POuts = new object[]{
					sc.Parameters["UserID"].Value
				};
				stReturn.ExMsg = sc.Parameters["ExMsg"].Value.ToString();

				SqlConn.Close();
			}

			return stReturn;
		}
		#endregion

		#region 列表用户
		/// <summary>
		/// 列表用户
		/// </summary>
		public static System.Data.DataSet ApqUser_ListPager(int pSize, int pNumber)
		{
			System.Data.DataSet ds = new System.Data.DataSet();

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
			{
				SqlDataAdapter sda = new SqlDataAdapter("dbo.ApqUser_ListPager", SqlConn);
				sda.SelectCommand.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sda.SelectCommand);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("pSize", pSize, DbType.Int32);
				dch.AddParameter("pNumber", pNumber, DbType.Int32);
				sda.SelectCommand.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
				SqlConn.Open();
				sda.Fill(ds);
				//stReturn.NReturn = System.Convert.ToInt32(sc.Parameters["rtn"].Value);

				SqlConn.Close();
			}

			return ds;
		}
		#endregion

		#region 用户信息
		/// <summary>
		/// 用户信息
		/// </summary>
		public static System.Data.DataSet ApqUser_ListOne(long UserID)
		{
			System.Data.DataSet ds = new System.Data.DataSet();

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
			{
				SqlDataAdapter sda = new SqlDataAdapter("dbo.ApqUser_ListOne", SqlConn);
				sda.SelectCommand.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sda.SelectCommand);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("UserID", UserID, DbType.Int64);
				sda.SelectCommand.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
				SqlConn.Open();
				sda.Fill(ds);
				//stReturn.NReturn = System.Convert.ToInt32(sc.Parameters["rtn"].Value);

				SqlConn.Close();
			}

			return ds;
		}
		#endregion

		#region 查找
		/// <summary>
		/// 查找
		/// </summary>
		public static STReturn ApqUser_FindOne(int UserSrc, string UserName)
		{
			STReturn stReturn = new STReturn();

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
			{
				SqlCommand sc = new SqlCommand("dbo.ApqUser_FindOne", SqlConn);
				sc.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sc);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("UserSrc", UserSrc, DbType.Int32);
				dch.AddParameter("UserName", UserName);
				dch.AddParameter("UserID", 0, DbType.Int64);
				sc.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
				sc.Parameters["UserID"].Direction = ParameterDirection.InputOutput;
				SqlConn.Open();
				sc.ExecuteNonQuery();

				stReturn.NReturn = System.Convert.ToInt32(sc.Parameters["rtn"].Value);
				stReturn.POuts = new object[]{
					sc.Parameters["UserID"].Value
				};

				SqlConn.Close();
			}

			return stReturn;
		}
		#endregion

		#region 编辑
		/// <summary>
		/// 编辑
		/// </summary>
		public static STReturn ApqUser_Edit(long UserID, int UserSrc, string UserName, byte AllowLogin)
		{
			STReturn stReturn = new STReturn();

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
			{
				SqlCommand sc = new SqlCommand("dbo.ApqUser_Edit", SqlConn);
				sc.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sc);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("UserID", UserID, DbType.Int64);
				dch.AddParameter("UserSrc", UserSrc, DbType.Int32);
				dch.AddParameter("UserName", UserName);
				dch.AddParameter("AllowLogin", AllowLogin, DbType.Byte);
				sc.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
				sc.Parameters["UserID"].Direction = ParameterDirection.InputOutput;
				sc.Parameters["UserSrc"].Direction = ParameterDirection.InputOutput;
				sc.Parameters["UserName"].Direction = ParameterDirection.InputOutput;
				sc.Parameters["AllowLogin"].Direction = ParameterDirection.InputOutput;
				SqlConn.Open();
				sc.ExecuteNonQuery();

				stReturn.NReturn = System.Convert.ToInt32(sc.Parameters["rtn"].Value);
				stReturn.POuts = new object[]{
					sc.Parameters["UserID"].Value,
					sc.Parameters["UserSrc"].Value,
					sc.Parameters["UserName"].Value,
					sc.Parameters["AllowLogin"].Value
				};

				SqlConn.Close();
			}

			return stReturn;
		}
		#endregion

		#region 删除
		/// <summary>
		/// 删除
		/// </summary>
		public static STReturn ApqUser_Delete(long UserID, int UserSrc)
		{
			STReturn stReturn = new STReturn();

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
			{
				SqlCommand sc = new SqlCommand("dbo.ApqUser_Delete", SqlConn);
				sc.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sc);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("UserID", UserID, DbType.Int64);
				dch.AddParameter("UserSrc", UserSrc, DbType.Int32);
				sc.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
				SqlConn.Open();
				sc.ExecuteNonQuery();

				stReturn.NReturn = System.Convert.ToInt32(sc.Parameters["rtn"].Value);

				SqlConn.Close();
			}

			return stReturn;
		}
		#endregion

		#region 列表权限
		/// <summary>
		/// 列表权限
		/// </summary>
		/// <param name="UserID"></param>
		/// <param name="PID">{-1:所有权限项},判断权限时不能使用-1</param>
		public static System.Data.DataSet ApqUser_ListPrivilege(long UserID, long PID)
		{
			System.Data.DataSet ds = new System.Data.DataSet();

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
			{
				SqlDataAdapter sda = new SqlDataAdapter("dbo.ApqUser_ListPrivilege", SqlConn);
				sda.SelectCommand.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sda.SelectCommand);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("UserID", UserID, DbType.Int64);
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

		#region 判断权限
		/// <summary>
		/// 判断权限
		/// </summary>
		/// <param name="UserID">UserID(ApqUser)</param>
		/// <param name="PID">权限项ID</param>
		/// <returns>true:授予,false:拒绝</returns>
		public bool ApqUser_HasPrivilege(long UserID, long PID)
		{
			if (PID == -1)
			{
				throw new ArgumentException("PID不能使用-1", "PID");
			}

			DataSet ds = ApqUser_ListPrivilege(UserID, PID);
			if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
				return !Apq.Convert.ChangeType<bool>(ds.Tables[0].Rows[0]["IsDeny"]);
			return false;
		}
		#endregion

		#region 设置权限
		/// <summary>
		/// 设置权限
		/// </summary>
		/// <param name="UserID"></param>
		/// <param name="strUserP">权限串,格式:PID,IsDeny;...</param>
		public static STReturn ApqUser_SetP(long UserID, string strUserP)
		{
			STReturn stReturn = new STReturn();

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
			{
				SqlCommand sc = new SqlCommand("dbo.ApqUser_SetP", SqlConn);
				sc.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sc);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("UserID", UserID, DbType.Int64);
				dch.AddParameter("strUserP", strUserP);
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
