using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using Apq.Web.AJAX;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Services;
using System.Net;

namespace dtxc.WS
{
	/// <summary>
	/// WS2 的摘要说明
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	[ScriptService]// 输出只能使用 ref 参数,且传入的时候尽量使用 null
	public class WS2 : System.Web.Services.WebService
	{

		[WebMethod]
		public string HelloWorld()
		{
			return "Hello World";
		}

		#region 注册登录
		/// <summary>
		/// 会员注册
		/// </summary>
		[WebMethod(EnableSession = true, Description = "会员注册")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public STReturn dtxc_Reg_UserName(string Name, string UserName, string LoginPwd, short Sex, string PhotoUrl, long IntroUserID, string Alipay, int UserType
			, DateTime Birthday, string IDCard, string IDCard_Name, short IDCard_Sex, string IDCard_PhotoUrl)
		{
			STReturn stReturn = new STReturn();

			if (LoginPwd.Length < 1)
			{
				stReturn.NReturn = -1;
				stReturn.ExMsg = "密码不允许为空";
				return stReturn;
			}

			System.Security.Cryptography.SHA512 SHA512 = System.Security.Cryptography.SHA512.Create();
			byte[] binLoginPwd = SHA512.ComputeHash(System.Text.Encoding.Unicode.GetBytes(LoginPwd));
			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.Common.GetSqlConnectionString("SqlConnectionString2")))
			{
				SqlCommand sc = new SqlCommand("dtxc.dtxc_Reg_UserName", SqlConn);
				sc.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sc);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				IPAddress ipa;
				if (IPAddress.TryParse(HttpContext.Current.Request.UserHostAddress, out ipa))
				{
					dch.AddParameter("RegIP", ipa.GetAddressBytes());
				}

				dch.AddParameter("Name", Name);
				dch.AddParameter("UserName", UserName);
				dch.AddParameter("LoginPwd", binLoginPwd);
				dch.AddParameter("Sex", Sex);
				dch.AddParameter("PhotoUrl", PhotoUrl);
				dch.AddParameter("Birthday", Birthday.ToString("yyyy-MM-dd HH:mm:ss.fff"));
				dch.AddParameter("Alipay", Alipay);
				dch.AddParameter("IntroUserID", IntroUserID);
				dch.AddParameter("UserType", UserType);
				dch.AddParameter("Expire", DateTime.Now.AddYears(10).ToString("yyyy-MM-dd HH:mm:ss.fff"));

				dch.AddParameter("IDCard", IDCard);
				dch.AddParameter("IDCard_Name", IDCard_Name);
				dch.AddParameter("IDCard_Sex", IDCard_Sex);
				dch.AddParameter("IDCard_PhotoUrl", IDCard_PhotoUrl);

				dch.AddParameter("UserID", 0, DbType.Int64);

				sc.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
				sc.Parameters["ExMsg"].Direction = ParameterDirection.InputOutput;
				sc.Parameters["UserID"].Direction = ParameterDirection.InputOutput;

				SqlConn.Open();
				sc.ExecuteNonQuery();

				stReturn.NReturn = System.Convert.ToInt32(sc.Parameters["rtn"].Value);
				stReturn.ExMsg = sc.Parameters["ExMsg"].Value.ToString();
				stReturn.POuts = new object[] {
					sc.Parameters["UserID"].Value
				};

				sc.Dispose();
				SqlConn.Close();
			}

			return stReturn;
		}

		/// <summary>
		/// 执行登录存储过程
		/// </summary>
		/// <param name="UserName">用户名</param>
		/// <param name="binLoginPwd">密码</param>
		/// <returns></returns>
		private STReturn DBLogin(string UserName, byte[] binLoginPwd)
		{
			STReturn stReturn = new STReturn();
			DataSet ds = new DataSet();

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.Common.GetSqlConnectionString("SqlConnectionString2")))
			{
				SqlDataAdapter sda = new SqlDataAdapter("dtxc.dtxc_Login_UserName", SqlConn);
				sda.SelectCommand.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sda.SelectCommand);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("UserName", UserName);
				dch.AddParameter("LoginPwd", binLoginPwd);

				sda.SelectCommand.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
				sda.SelectCommand.Parameters["ExMsg"].Direction = ParameterDirection.InputOutput;

				SqlConn.Open();
				sda.Fill(ds);

				stReturn.NReturn = System.Convert.ToInt32(sda.SelectCommand.Parameters["rtn"].Value);
				stReturn.ExMsg = sda.SelectCommand.Parameters["ExMsg"].Value.ToString();
				stReturn.FNReturn = ds.Tables[0];

				sda.Dispose();
				SqlConn.Close();
			}

			// 返回客户端
			string SqlLoginPwd = Apq.Data.SqlClient.Common.ConvertToSqlON(binLoginPwd);
			if (stReturn.NReturn > 0)
			{
				stReturn.FNReturn = ds.Tables[0];
				stReturn.POuts = new object[] { UserName, SqlLoginPwd };
			}
			return stReturn;
		}

		/// <summary>
		/// 登录
		/// </summary>
		/// <param name="LoginName">用户名</param>
		/// <param name="Pwd">密码</param>
		/// <returns>{&lt;0:匿名登录,0:登录失败,>0:登录成功}</returns>
		[WebMethod(EnableSession = true, Description = "登录")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml)]
		public STReturn Login_UserName(string UserName, string LoginPwd)
		{
			// 先清除已登录数据
			Session.Clear();

			STReturn stReturn = new STReturn();
			Apq.Web.SessionState.HttpSessionState ApqSession = new Apq.Web.SessionState.HttpSessionState(Session);

			if (LoginPwd.Length < 1)
			{
				stReturn.NReturn = -1;
				stReturn.ExMsg = "密码不允许为空";
				return stReturn;
			}

			System.Security.Cryptography.SHA512 SHA512 = System.Security.Cryptography.SHA512.Create();
			byte[] binLoginPwd = SHA512.ComputeHash(System.Text.Encoding.Unicode.GetBytes(LoginPwd));
			stReturn = DBLogin(UserName, binLoginPwd);

			// Session操作
			if (stReturn.NReturn > 0)
			{
				ApqSession.User = stReturn.FNReturn as DataTable;
			}
			ApqSession.LoginTime = DateTime.Now;

			return stReturn;
		}

		/// <summary>
		/// 以 Cookie 登录
		/// </summary>
		/// <param name="LoginName">用户名</param>
		/// <param name="Pwd">密码</param>
		/// <returns>{&lt;0:匿名登录,0:登录失败,>0:登录成功}</returns>
		[WebMethod(EnableSession = true, Description = "以 Cookie 登录")]
		public STReturn Login_UserNameFromCookie(string UserName, string SqlLoginPwd)
		{
			// 先清除已登录数据
			Session.Clear();

			STReturn stReturn = new STReturn();
			Apq.Web.SessionState.HttpSessionState ApqSession = new Apq.Web.SessionState.HttpSessionState(Session);

			if (SqlLoginPwd.Length < 63)
			{
				stReturn.NReturn = -1;
				stReturn.ExMsg = "密码不允许为空";
				return stReturn;
			}

			byte[] binLoginPwd = Apq.Data.SqlClient.Common.ParseSqlON<byte[]>(System.Data.SqlDbType.VarBinary, SqlLoginPwd) as byte[];
			stReturn = DBLogin(UserName, binLoginPwd);

			// Session操作
			if (stReturn.NReturn > 0)
			{
				ApqSession.User = stReturn.FNReturn as DataTable;
			}
			ApqSession.LoginTime = DateTime.Now;

			return stReturn;
		}
		#endregion

		#region 任务管理
		/// <summary>
		/// 任务列表
		/// </summary>
		[WebMethod(EnableSession = true, Description = "任务列表(可领取)")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public STReturn TaskListCanTake(int Pager_Page, int Pager_PageSize)
		{
			STReturn stReturn = new STReturn();
			Apq.Web.SessionState.HttpSessionState ApqSession = new Apq.Web.SessionState.HttpSessionState(Session);

			DataSet ds = new DataSet();

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
			{
				SqlDataAdapter sda = new SqlDataAdapter("dtxc.dtxc_Task_ListCanTake", SqlConn);
				sda.SelectCommand.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sda.SelectCommand);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("Pager_Page", Pager_Page, DbType.Int32);
				dch.AddParameter("Pager_PageSize", Pager_PageSize);
				dch.AddParameter("Pager_RowCount", 0, DbType.Int32);

				dch.AddParameter("UserID", ApqSession.UserID);

				sda.SelectCommand.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
				sda.SelectCommand.Parameters["ExMsg"].Direction = ParameterDirection.InputOutput;

				sda.SelectCommand.Parameters["Pager_Page"].Direction = ParameterDirection.InputOutput;
				sda.SelectCommand.Parameters["Pager_RowCount"].Direction = ParameterDirection.InputOutput;

				SqlConn.Open();
				sda.Fill(ds);

				stReturn.NReturn = System.Convert.ToInt32(sda.SelectCommand.Parameters["rtn"].Value);
				stReturn.ExMsg = sda.SelectCommand.Parameters["ExMsg"].Value.ToString();
				stReturn.FNReturn = ds.Tables[0];
				stReturn.POuts = new object[]{
					sda.SelectCommand.Parameters["Pager_Page"].Value,
					sda.SelectCommand.Parameters["Pager_RowCount"].Value
				};

				sda.Dispose();
				SqlConn.Close();
			}

			return stReturn;
		}
		#endregion

		#region 投票记录
		/// <summary>
		/// 列表投票记录
		/// </summary>
		[WebMethod(EnableSession = true, Description = "列表投票记录")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public STReturn dtxc_TaskVote_Log_List(string TaskName, string UserNameBegin)
		{
			STReturn stReturn = new STReturn();
			DataSet ds = new DataSet();

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.Common.GetSqlConnectionString("SqlConnectionString2")))
			{
				SqlDataAdapter sda = new SqlDataAdapter("dtxc.dtxc_TaskVote_Log_List", SqlConn);
				sda.SelectCommand.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sda.SelectCommand);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("TaskName", TaskName);
				dch.AddParameter("UserNameBegin", UserNameBegin);

				sda.SelectCommand.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
				sda.SelectCommand.Parameters["ExMsg"].Direction = ParameterDirection.InputOutput;

				SqlConn.Open();
				sda.Fill(ds);

				stReturn.NReturn = System.Convert.ToInt32(sda.SelectCommand.Parameters["rtn"].Value);
				stReturn.ExMsg = sda.SelectCommand.Parameters["ExMsg"].Value.ToString();
				stReturn.FNReturn = ds.Tables[0];

				sda.Dispose();
				SqlConn.Close();
			}

			return stReturn;
		}
		#endregion
	}
}
