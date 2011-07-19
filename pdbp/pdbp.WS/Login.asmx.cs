using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using System.Net;

namespace pdbp.WS
{
	/// <summary>
	/// Login 的摘要说明
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	[System.Web.Script.Services.ScriptService]// 输出只能使用 ref 参数,且传入的时候尽量使用 null
	public class Login : System.Web.Services.WebService
	{

		[WebMethod]
		public string HelloWorld()
		{
			return "Hello World";
		}

		#region 注册登录
		/// <summary>
		/// 登录权限系统
		/// </summary>
		/// <returns></returns>
		[WebMethod(Description = "登录权限系统")]
		public Apq.STReturn ApqUser_Login(int UserSrc, string UserName)
		{
			Apq.STReturn stReturn = new Apq.STReturn();

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
			{
				SqlCommand sc = new SqlCommand("dbo.ApqUser_Login", SqlConn);
				sc.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sc);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("UserSrc", UserSrc, DbType.Int32);
				dch.AddParameter("UserName", UserName);
				dch.AddParameter("UserID", 0, DbType.Int64);

				sc.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
				sc.Parameters["ExMsg"].Direction = ParameterDirection.InputOutput;
				sc.Parameters["UserID"].Direction = ParameterDirection.InputOutput;

				SqlConn.Open();
				sc.ExecuteNonQuery();

				stReturn.NReturn = System.Convert.ToInt32(sc.Parameters["rtn"].Value);
				stReturn.ExMsg = Apq.Convert.ChangeType<string>(sc.Parameters["ExMsg"].Value);
				stReturn.POuts.Add(sc.Parameters["UserID"].Value);
			}

			return stReturn;
		}

		/// <summary
		/// <summary>
		/// 注册
		/// </summary>
		[WebMethod(Description = "会员注册")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public Apq.STReturn dtxc_Reg_UserName(string UserName, string LoginName, string LoginPwd, short Sex, string PhotoUrl, long IntroUserID, string Alipay, int UserType
			, DateTime Birthday, string IDCard, string IDCard_Name, short IDCard_Sex, string IDCard_PhotoUrl)
		{
			Apq.STReturn stReturn = new Apq.STReturn();

			if (LoginPwd.Length < 1)
			{
				stReturn.NReturn = -1;
				stReturn.ExMsg = "密码不允许为空";
				return stReturn;
			}

			System.Security.Cryptography.SHA512 SHA512 = System.Security.Cryptography.SHA512.Create();
			byte[] binLoginPwd = SHA512.ComputeHash(System.Text.Encoding.Unicode.GetBytes(LoginPwd));
			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
			{
				SqlCommand sc = new SqlCommand("dbo.WS_Reg", SqlConn);
				sc.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sc);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				IPAddress ipa;
				if (IPAddress.TryParse(HttpContext.Current.Request.UserHostAddress, out ipa))
				{
					dch.AddParameter("RegIP", ipa.GetAddressBytes());
				}

				dch.AddParameter("UserName", UserName);
				dch.AddParameter("LoginName", LoginName);
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
				stReturn.ExMsg = Apq.Convert.ChangeType<string>(sc.Parameters["ExMsg"].Value);
				stReturn.POuts.Add(sc.Parameters["UserID"].Value);

				sc.Dispose();
				SqlConn.Close();
			}

			return stReturn;
		}

		/// <summary
		/// <summary>
		/// 注册
		/// </summary>
		[WebMethod(Description = "会员注册")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public Apq.STReturn WS_Reg(string LoginName, string LoginPwd, byte[] Ipa, string Introducer)
		{
			Apq.STReturn stReturn = new Apq.STReturn();

			if (LoginPwd.Length < 1)
			{
				stReturn.NReturn = -1;
				stReturn.ExMsg = "密码不允许为空";
				return stReturn;
			}

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
			{
				SqlCommand sc = new SqlCommand("dbo.WS_Reg", SqlConn);
				sc.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sc);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("RegIP", Ipa, DbType.Binary);

				dch.AddParameter("LoginName", LoginName);
				dch.AddParameter("LoginPwd", LoginPwd);
				dch.AddParameter("Introducer", Introducer);

				dch.AddParameter("UserID", 0, DbType.Int64);

				sc.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
				sc.Parameters["ExMsg"].Direction = ParameterDirection.InputOutput;
				sc.Parameters["UserID"].Direction = ParameterDirection.InputOutput;

				SqlConn.Open();
				sc.ExecuteNonQuery();

				stReturn.NReturn = System.Convert.ToInt32(sc.Parameters["rtn"].Value);
				stReturn.ExMsg = Apq.Convert.ChangeType<string>(sc.Parameters["ExMsg"].Value);
				stReturn.POuts.Add(sc.Parameters["UserID"].Value);

				sc.Dispose();
				SqlConn.Close();
			}

			return stReturn;
		}

		/// <summary>
		/// 以 Cookie 登录
		/// </summary>
		/// <param name="LoginName">用户名</param>
		/// <param name="CryptLoginPwd">密码</param>
		/// <returns>{&lt;0:匿名登录,0:登录失败,&gt;0:登录成功}</returns>
		[WebMethod(Description = "以 Cookie 登录")]
		public Apq.STReturn Login_LoginNameFromCookie(string LoginName, string CryptLoginPwd)
		{
			Apq.STReturn stReturn = new Apq.STReturn();

			if (CryptLoginPwd.Length < 1)
			{
				stReturn.NReturn = -1;
				stReturn.ExMsg = "密码不允许为空";
				return stReturn;
			}

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
			{
				SqlCommand sc = new SqlCommand("dbo.WS_Login", SqlConn);
				sc.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sc);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("LoginName", LoginName);
				dch.AddParameter("LoginPwd", CryptLoginPwd);
				dch.AddParameter("UserID", 0, DbType.Int64);
				dch.AddParameter("IsAdmin", 0, DbType.Byte);
				dch.AddParameter("NickName", string.Empty, DbType.String, 50);

				sc.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
				sc.Parameters["ExMsg"].Direction = ParameterDirection.InputOutput;
				sc.Parameters["UserID"].Direction = ParameterDirection.InputOutput;
				sc.Parameters["IsAdmin"].Direction = ParameterDirection.InputOutput;
				sc.Parameters["NickName"].Direction = ParameterDirection.InputOutput;

				SqlConn.Open();
				sc.ExecuteNonQuery();

				stReturn.NReturn = System.Convert.ToInt32(sc.Parameters["rtn"].Value);
				stReturn.ExMsg = Apq.Convert.ChangeType<string>(sc.Parameters["ExMsg"].Value);
				stReturn.POuts.Add(sc.Parameters["UserID"].Value);	//POuts[0]:UserID
				stReturn.POuts.Add(CryptLoginPwd);					//POuts[1]:CryptLoginPwd
				stReturn.POuts.Add(sc.Parameters["NickName"].Value);//POuts[2]:NickName
				stReturn.POuts.Add(sc.Parameters["IsAdmin"].Value);//POuts[3]:IsAdmin
			}

			return stReturn;
		}

		/// <summary>
		/// 登录
		/// </summary>
		/// <param name="LoginName">用户名</param>
		/// <param name="Pwd">密码</param>
		/// <returns>{&lt;0:匿名登录,0:登录失败,&gt;0:登录成功}</returns>
		[WebMethod(Description = "登录")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml)]
		public Apq.STReturn Login_LoginName(string LoginName, string LoginPwd)
		{
			string CryptLoginPwd = Apq.Security.Cryptography.DESHelper.EncryptString(LoginPwd, pdbp.CryptKey.DES.Key, pdbp.CryptKey.DES.IV);
			return Login_LoginNameFromCookie(LoginName, CryptLoginPwd);
		}
		#endregion
	}
}
