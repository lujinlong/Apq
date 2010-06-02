using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Web.Script.Services;
using Apq.Web.AJAX;
using System.Net;

namespace dtxc.WS.Admin
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
		/// 列表会员
		/// </summary>
		/// <param name="UserID">帐号ID</param>
		/// <param name="ContainsSelf">是否包含自己</param>
		/// <param name="ContainsGrand">是否包含子孙</param>
		[WebMethod(EnableSession = true, Description = "会员列表")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public STReturn dtxc_Users_ListChild_Pager(int Pager_Page, int Pager_PageSize, long UserID)
		{
			STReturn stReturn = new STReturn();
			Apq.Web.SessionState.HttpSessionState ApqSession = new Apq.Web.SessionState.HttpSessionState(Session);

			if (!CheckLoginPage.CheckAdmin(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			DataSet ds = new DataSet();

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.Common.GetSqlConnectionString("SqlConnectionString2")))
			{
				SqlDataAdapter sda = new SqlDataAdapter("dtxc.dtxc_Users_ListChild_Pager", SqlConn);
				sda.SelectCommand.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sda.SelectCommand);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("Pager_Page", Pager_Page, DbType.Int32);
				dch.AddParameter("Pager_PageSize", Pager_PageSize);
				dch.AddParameter("Pager_RowCount", 0, DbType.Int32);

				dch.AddParameter("UserID", UserID);

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

		/// <summary>
		/// 帐号状态切换
		/// </summary>
		/// <param name="UserID">帐号ID</param>
		/// <returns></returns>
		[WebMethod(EnableSession = true, Description = "帐号状态切换")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public STReturn dtxc_User_Toggle(long UserID)
		{
			STReturn stReturn = new STReturn();
			Apq.Web.SessionState.HttpSessionState ApqSession = new Apq.Web.SessionState.HttpSessionState(Session);

			if (!CheckLoginPage.CheckAdmin(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			long _OperID = Convert.ToInt64(ApqSession.User.Rows[0]["UserID"]);

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.Common.GetSqlConnectionString("SqlConnectionString2")))
			{
				SqlCommand sc = new SqlCommand("dtxc.dtxc_User_Toggle", SqlConn);
				sc.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sc);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("_OperID", _OperID);
				dch.AddParameter("_OpTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
				IPAddress ipa;
				if (IPAddress.TryParse(HttpContext.Current.Request.UserHostAddress, out ipa))
				{
					dch.AddParameter("_OperIP", ipa.GetAddressBytes());
				}

				dch.AddParameter("UserID", UserID);

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
		/// 会员修改
		/// </summary>
		[WebMethod(EnableSession = true, Description = "会员修改")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public STReturn UserEdit(long UserID, string Name, string UserName, string LoginPwd, short Sex, string PhotoUrl, DateTime Expire, bool Status
			, bool IsAdmin, DateTime Birthday, int UserType, string IDCard, string Alipay)
		{
			STReturn stReturn = new STReturn();
			Apq.Web.SessionState.HttpSessionState ApqSession = new Apq.Web.SessionState.HttpSessionState(Session);

			if (!CheckLoginPage.CheckAdmin(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.Common.GetSqlConnectionString("SqlConnectionString2")))
			{
				SqlCommand sc = new SqlCommand("dtxc.dtxc_Users_Update", SqlConn);
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
				dch.AddParameter("UserName", UserName);
				dch.AddParameter("LoginPwd", LoginPwd);
				dch.AddParameter("Sex", Sex);
				dch.AddParameter("PhotoUrl", PhotoUrl);
				dch.AddParameter("Expire", Expire.ToString("yyyy-MM-dd HH:mm:ss.fff"));
				dch.AddParameter("Status", Status);
				dch.AddParameter("IsAdmin", IsAdmin);
				dch.AddParameter("UserType", UserType);
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
		#endregion
	}
}
