using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Web.Script.Services;

using System.Xml;
using System.Net;

namespace dtxc.WS.User
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

		#region 会员管理
		/// <summary>
		/// 会员个人信息修改
		/// </summary>
		[WebMethod(EnableSession = true, Description = "会员个人信息修改")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public Apq.STReturn UserEditSelf(long UserID, string Name, short Sex, string PhotoUrl, DateTime Birthday
			, string IDCard, string Alipay)
		{
			Apq.STReturn stReturn = new Apq.STReturn();
			Apq.Web.SessionState.HttpSessionState ApqSession = new Apq.Web.SessionState.HttpSessionState(Session);

			if (!CheckLoginPage.CheckPwdExpire(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.Common.GetSqlConnectionString("SqlConnectionString2")))
			{
				SqlCommand sc = new SqlCommand("dtxc.dtxc_User_UpdateSelf", SqlConn);
				sc.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sc);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("_OperID", ApqSession.UserID);
				dch.AddParameter("_OpTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
				IPAddress ipa;
				if (IPAddress.TryParse(HttpContext.Current.Request.UserHostAddress, out ipa))
				{
					dch.AddParameter("_OperIP", ipa.GetAddressBytes());
				}

				dch.AddParameter("UserID", UserID);

				dch.AddParameter("Name", Name);
				dch.AddParameter("Sex", Sex);
				dch.AddParameter("PhotoUrl", PhotoUrl);
				//dch.AddParameter("Expire", Expire.ToString("yyyy-MM-dd HH:mm:ss.fff"));
				//dch.AddParameter("Status", Status);
				//dch.AddParameter("IsAdmin", IsAdmin);
				dch.AddParameter("Birthday", Birthday);
				dch.AddParameter("IDCard", IDCard);
				dch.AddParameter("Alipay", Alipay);

				sc.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
				sc.Parameters["ExMsg"].Direction = ParameterDirection.InputOutput;

				SqlConn.Open();
				sc.ExecuteNonQuery();

				stReturn.NReturn = System.Convert.ToInt32(sc.Parameters["rtn"].Value);
				stReturn.ExMsg = sc.Parameters["ExMsg"].Value.ToString();

				sc.Dispose();
				SqlConn.Close();
			}

			return stReturn;
		}

		/// <summary>
		/// 会员登录密码修改
		/// </summary>
		[WebMethod(EnableSession = true, Description = "会员登录密码修改")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public Apq.STReturn UserEditLoginPwd(string LoginPwd_C, string LoginPwd)
		{
			Apq.STReturn stReturn = new Apq.STReturn();
			Apq.Web.SessionState.HttpSessionState ApqSession = new Apq.Web.SessionState.HttpSessionState(Session);

			if (!CheckLoginPage.CheckLogin(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			// 检测旧密码
			System.Security.Cryptography.SHA512 SHA512 = System.Security.Cryptography.SHA512.Create();
			byte[] binLoginPwd_C = SHA512.ComputeHash(System.Text.Encoding.Unicode.GetBytes(LoginPwd_C));
			string SqlLoginPwd_C = Apq.Data.SqlClient.Common.ConvertToSqlON(binLoginPwd_C);
			string SqlLoginPwd_DB = Apq.Data.SqlClient.Common.ConvertToSqlON(ApqSession.User.Rows[0]["LoginPwd"]);
			if (SqlLoginPwd_C != SqlLoginPwd_DB)
			{
				stReturn.NReturn = -1;
				stReturn.ExMsg = "原密码输入错误";
				return stReturn;
			}

			byte[] binLoginPwd = SHA512.ComputeHash(System.Text.Encoding.Unicode.GetBytes(LoginPwd));
			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.Common.GetSqlConnectionString("SqlConnectionString2")))
			{
				SqlCommand sc = new SqlCommand("dtxc.dtxc_User_UpdateLoginPwd", SqlConn);
				sc.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sc);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("_OperID", ApqSession.UserID);
				dch.AddParameter("_OpTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
				IPAddress ipa;
				if (IPAddress.TryParse(HttpContext.Current.Request.UserHostAddress, out ipa))
				{
					dch.AddParameter("_OperIP", ipa.GetAddressBytes());
				}

				dch.AddParameter("UserID", ApqSession.UserID);

				dch.AddParameter("LoginPwd", binLoginPwd);

				sc.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
				sc.Parameters["ExMsg"].Direction = ParameterDirection.InputOutput;

				SqlConn.Open();
				sc.ExecuteNonQuery();

				stReturn.NReturn = System.Convert.ToInt32(sc.Parameters["rtn"].Value);
				stReturn.ExMsg = sc.Parameters["ExMsg"].Value.ToString();

				sc.Dispose();
				SqlConn.Close();
			}

			// 更新Session中的密码信息
			ApqSession.User.Rows[0]["LoginPwd"] = binLoginPwd;

			// 返回客户端
			stReturn.FNReturn = Apq.Data.SqlClient.Common.ConvertToSqlON(binLoginPwd);
			return stReturn;
		}

		/// <summary>
		/// 支付申请
		/// </summary>
		/// <param name="PayMoney">支付金额</param>
		/// <returns></returns>
		[WebMethod(EnableSession = true, Description = "支付申请")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public Apq.STReturn UserPayoutReg(decimal Payout)
		{
			Apq.STReturn stReturn = new Apq.STReturn();
			Apq.Web.SessionState.HttpSessionState ApqSession = new Apq.Web.SessionState.HttpSessionState(Session);

			if (!CheckLoginPage.CheckPwdExpire(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.Common.GetSqlConnectionString("SqlConnectionString2")))
			{
				SqlCommand sc = new SqlCommand("dtxc.dtxc_Payout_Reg", SqlConn);
				sc.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sc);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("_OperID", ApqSession.UserID);
				dch.AddParameter("_OpTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
				IPAddress ipa;
				if (IPAddress.TryParse(HttpContext.Current.Request.UserHostAddress, out ipa))
				{
					dch.AddParameter("_OperIP", ipa.GetAddressBytes());
				}

				dch.AddParameter("Payout", Payout);

				sc.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
				sc.Parameters["ExMsg"].Direction = ParameterDirection.InputOutput;

				SqlConn.Open();
				sc.ExecuteNonQuery();

				stReturn.NReturn = System.Convert.ToInt32(sc.Parameters["rtn"].Value);
				stReturn.ExMsg = sc.Parameters["ExMsg"].Value.ToString();

				sc.Dispose();
				SqlConn.Close();
			}

			return stReturn;
		}
		#endregion
	}
}
