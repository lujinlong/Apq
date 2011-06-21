using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Services;
using System.Net;
using System.Xml.Serialization;
using System.Data.Common;

namespace Dinner.WS
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
		/// 员工注册
		/// </summary>
		[WebMethod(EnableSession = true, Description = "员工注册")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public Apq.STReturn Dinner_RegEmployee(string EmName, string LoginName, string LoginPwd)
		{
			Apq.STReturn stReturn = new Apq.STReturn();

			if (EmName.Length < 1)
			{
				stReturn.NReturn = -1;
				stReturn.ExMsg = "姓名不允许为空";
				return stReturn;
			}
			if (LoginName.Length < 1)
			{
				stReturn.NReturn = -1;
				stReturn.ExMsg = "登录不允许为空";
				return stReturn;
			}
			if (LoginPwd.Length < 1)
			{
				stReturn.NReturn = -1;
				stReturn.ExMsg = "密码不允许为空";
				return stReturn;
			}

			System.Security.Cryptography.SHA512 SHA512 = System.Security.Cryptography.SHA512.Create();
			byte[] binLoginPwd = SHA512.ComputeHash(System.Text.Encoding.Unicode.GetBytes(LoginPwd));
			string SqlLoginPwd = Apq.Data.SqlClient.Common.ConvertToSqlON(binLoginPwd);

			DbConnection SqlConn = null;
			using (SqlConn = Apq.DBC.Common.CreateDBConnection("Dinner", ref SqlConn))
			{
				Apq.Data.Common.DbConnectionHelper dbch = new Apq.Data.Common.DbConnectionHelper(SqlConn);
				DbCommand sc = SqlConn.CreateCommand();
				sc.CommandText = "dbo.Dinner_RegEmployee";
				sc.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sc);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				//IPAddress ipa;
				//if (IPAddress.TryParse(HttpContext.Current.Request.UserHostAddress, out ipa))
				//{
				//    dch.AddParameter("RegIP", ipa.GetAddressBytes());
				//}

				dch.AddParameter("EmName", EmName);
				dch.AddParameter("LoginName", LoginName);
				dch.AddParameter("LoginPwd", binLoginPwd);
				//dch.AddParameter("Sex", Sex);
				//dch.AddParameter("PhotoUrl", PhotoUrl);
				//dch.AddParameter("Birthday", Birthday.ToString("yyyy-MM-dd HH:mm:ss.fff"));
				//dch.AddParameter("Alipay", Alipay);
				//dch.AddParameter("IntroUserID", IntroUserID);
				//dch.AddParameter("UserType", UserType);
				//dch.AddParameter("Expire", DateTime.Now.AddYears(10).ToString("yyyy-MM-dd HH:mm:ss.fff"));

				//dch.AddParameter("IDCard", IDCard);
				//dch.AddParameter("IDCard_Name", IDCard_Name);
				//dch.AddParameter("IDCard_Sex", IDCard_Sex);
				//dch.AddParameter("IDCard_PhotoUrl", IDCard_PhotoUrl);

				dch.AddParameter("EmID", 0, DbType.Int64);
				dch.AddParameter("LoginID", 0, DbType.Int64);

				sc.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
				sc.Parameters["ExMsg"].Direction = ParameterDirection.InputOutput;
				sc.Parameters["EmID"].Direction = ParameterDirection.InputOutput;
				sc.Parameters["LoginID"].Direction = ParameterDirection.InputOutput;

				SqlConn.Open();
				sc.ExecuteNonQuery();

				stReturn.NReturn = System.Convert.ToInt32(sc.Parameters["rtn"].Value);
				stReturn.ExMsg = sc.Parameters["ExMsg"].Value.ToString();
				stReturn.POuts.Add(sc.Parameters["EmID"].Value);
				stReturn.POuts.Add(sc.Parameters["LoginID"].Value);
				stReturn.POuts.Add(LoginName);
				stReturn.POuts.Add(SqlLoginPwd);

				sc.Dispose();
				SqlConn.Close();
			}

			return stReturn;
		}

		/// <summary>
		/// 执行登录存储过程
		/// </summary>
		/// <param name="LoginName">用户名</param>
		/// <param name="binLoginPwd">密码</param>
		/// <returns></returns>
		private Apq.STReturn DBLogin(string LoginName, byte[] binLoginPwd)
		{
			Apq.STReturn stReturn = new Apq.STReturn();
			DataSet ds = new DataSet();
			
			DbConnection SqlConn = null;
			using (SqlConn = Apq.DBC.Common.CreateDBConnection("Dinner", ref SqlConn))
			{
				Apq.Data.Common.DbConnectionHelper dbch = new Apq.Data.Common.DbConnectionHelper(SqlConn);
				DbDataAdapter sda = dbch.CreateAdapter();
				sda.SelectCommand.CommandText = "dbo.Dinner_Login";
				sda.SelectCommand.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sda.SelectCommand);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("LoginName", LoginName);
				dch.AddParameter("LoginPwd", binLoginPwd);

				sda.SelectCommand.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
				sda.SelectCommand.Parameters["ExMsg"].Direction = ParameterDirection.InputOutput;

				SqlConn.Open();
				sda.Fill(ds);

				stReturn.NReturn = System.Convert.ToInt32(sda.SelectCommand.Parameters["rtn"].Value);
				stReturn.ExMsg = sda.SelectCommand.Parameters["ExMsg"].Value.ToString();
				if (ds.Tables.Count > 0)
				{
					stReturn.FNReturn = ds.Tables[0];
				}

				sda.Dispose();
				SqlConn.Close();
			}

			// 返回调用者
			string SqlLoginPwd = Apq.Data.SqlClient.Common.ConvertToSqlON(binLoginPwd);
			if (stReturn.NReturn > 0 && ds.Tables[0].Rows.Count > 0)
			{
				stReturn.NReturn = 1;
				stReturn.FNReturn = ds.Tables[0];
				stReturn.POuts.Add(SqlLoginPwd);
			}
			return stReturn;
		}

		/// <summary>
		/// 登录成功,Session操作
		/// </summary>
		public void Login_Session(DinnerSession ApqSession, DataTable dt)
		{
			ApqSession.ApqLogin.LoginID = Apq.Convert.ChangeType<long>(dt.Rows[0]["LoginID"]);
			ApqSession.ApqLogin.LoginName = Apq.Convert.ChangeType<string>(dt.Rows[0]["LoginName"]);
			ApqSession.ApqLogin.LoginPwd = Apq.Convert.ChangeType<byte[]>(dt.Rows[0]["LoginPwd"]);
			ApqSession.ApqLogin.PwdExpire = Apq.Convert.ChangeType<DateTime>(dt.Rows[0]["PwdExpire"]);
			ApqSession.ApqLogin.LoginStatus = Apq.Convert.ChangeType<int>(dt.Rows[0]["LoginStatus"]);
			ApqSession.ApqLogin.RegTime = Apq.Convert.ChangeType<DateTime>(dt.Rows[0]["RegTime"]);
			ApqSession.ApqLogin.LoginTime = DateTime.Now;

			ApqSession.Employee.EmID = Apq.Convert.ChangeType<long>(dt.Rows[0]["EmID"]);
			ApqSession.Employee.EmName = Apq.Convert.ChangeType<string>(dt.Rows[0]["EmName"]);
			ApqSession.Employee.LoginID = ApqSession.ApqLogin.LoginID;
			ApqSession.Employee.IsAdmin = Apq.Convert.ChangeType<bool>(dt.Rows[0]["IsAdmin"]);
		}

		/// <summary>
		/// 登录
		/// </summary>
		/// <param name="LoginName">用户名</param>
		/// <param name="Pwd">密码</param>
		/// <returns>{&lt;0:匿名登录,0:登录失败,>0:登录成功}</returns>
		[WebMethod(EnableSession = true, Description = "登录")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml)]
		public Apq.STReturn Login_LoginName(string LoginName, string LoginPwd)
		{
			// 先清除已登录数据
			Session.Clear();

			Apq.STReturn stReturn = new Apq.STReturn();
			DinnerSession ApqSession = new DinnerSession(Session);

			if (LoginPwd.Length < 1)
			{
				stReturn.NReturn = -1;
				stReturn.ExMsg = "密码不允许为空";
				return stReturn;
			}

			System.Security.Cryptography.SHA512 SHA512 = System.Security.Cryptography.SHA512.Create();
			byte[] binLoginPwd = SHA512.ComputeHash(System.Text.Encoding.Unicode.GetBytes(LoginPwd));
			stReturn = DBLogin(LoginName, binLoginPwd);

			// Session操作
			if (stReturn.NReturn > 0)
			{// 登录成功
				DataTable dt = stReturn.FNReturn as DataTable;
				Login_Session(ApqSession, dt);
			}

			return stReturn;
		}

		/// <summary>
		/// 以 Cookie 登录
		/// </summary>
		/// <param name="LoginName">用户名</param>
		/// <param name="Pwd">密码</param>
		/// <returns>{&lt;0:匿名登录,0:登录失败,>0:登录成功}</returns>
		[WebMethod(EnableSession = true, Description = "以 Cookie 登录")]
		public Apq.STReturn Login_LoginNameFromCookie(string LoginName, string SqlLoginPwd)
		{
			// 先清除已登录数据
			Session.Clear();

			Apq.STReturn stReturn = new Apq.STReturn();
			DinnerSession ApqSession = new DinnerSession(Session);

			if (SqlLoginPwd.Length < 63)
			{
				stReturn.NReturn = -1;
				stReturn.ExMsg = "密码不允许为空";
				return stReturn;
			}

			byte[] binLoginPwd = Apq.Data.SqlClient.Common.ParseSqlON<byte[]>(System.Data.SqlDbType.VarBinary, SqlLoginPwd) as byte[];
			stReturn = DBLogin(LoginName, binLoginPwd);

			// Session操作
			if (stReturn.NReturn > 0)
			{
				DataTable dt = stReturn.FNReturn as DataTable;
				Login_Session(ApqSession, dt);
			}

			return stReturn;
		}
		#endregion
	}
}
