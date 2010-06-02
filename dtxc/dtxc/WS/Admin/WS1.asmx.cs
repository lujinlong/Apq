using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Web.Script.Services;

using System.Net;

namespace dtxc.WS.Admin
{
	/// <summary>
	/// WS1 的摘要说明
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	[ScriptService]// 输出只能使用 ref 参数,且传入的时候尽量使用 null
	public class WS1 : System.Web.Services.WebService
	{
		[WebMethod]
		public string HelloWorld()
		{
			return "Hello World";
		}

		#region 插件管理
		/// <summary>
		/// 列表插件
		/// </summary>
		[WebMethod(EnableSession = true, Description = "插件列表")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public Apq.STReturn AddinList(int start, int limit, short IsLookup, long LookupID)
		{
			Apq.STReturn stReturn = new Apq.STReturn();
			Apq.Web.SessionState.HttpSessionState ApqSession = new Apq.Web.SessionState.HttpSessionState(Session);

			if (!CheckLoginPage.CheckAdmin(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			DataSet ds = new DataSet();

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.Common.GetSqlConnectionString("SqlConnectionString2")))
			{
				SqlDataAdapter sda = new SqlDataAdapter("dtxc.Apq_Addin_List", SqlConn);
				sda.SelectCommand.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sda.SelectCommand);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("Pager_Page", start / limit + 1, DbType.Int32);
				dch.AddParameter("Pager_PageSize", limit);
				dch.AddParameter("Pager_RowCount", 0, DbType.Int32);

				dch.AddParameter("IsLookup", IsLookup, DbType.Int16);
				dch.AddParameter("LookupID", LookupID);

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
		/// 列表一个插件
		/// </summary>
		[WebMethod(EnableSession = true, Description = "插件列表")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public Apq.STReturn AddinListOne(long AddinID)
		{
			Apq.STReturn stReturn = new Apq.STReturn();
			Apq.Web.SessionState.HttpSessionState ApqSession = new Apq.Web.SessionState.HttpSessionState(Session);

			if (!CheckLoginPage.CheckAdmin(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			DataSet ds = new DataSet();

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.Common.GetSqlConnectionString("SqlConnectionString2")))
			{
				SqlDataAdapter sda = new SqlDataAdapter("dtxc.Apq_Addin_ListOne", SqlConn);
				sda.SelectCommand.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sda.SelectCommand);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("AddinID", AddinID);

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

		/// <summary>
		/// 插件发布
		/// </summary>
		/// <param name="AddinName">插件名</param>
		/// <param name="AddinUrl">插件地址</param>
		/// <param name="AddinDescript">插件描述内容</param>
		/// <returns></returns>
		[WebMethod(EnableSession = true, Description = "插件发布")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public Apq.STReturn AddinAdd(string AddinName, string AddinUrl, string AddinDescript)
		{
			Apq.STReturn stReturn = new Apq.STReturn();
			Apq.Web.SessionState.HttpSessionState ApqSession = new Apq.Web.SessionState.HttpSessionState(Session);

			if (!CheckLoginPage.CheckAdmin(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			long UserID = System.Convert.ToInt64(ApqSession.User.Rows[0]["UserID"]);

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.Common.GetSqlConnectionString("SqlConnectionString2")))
			{
				SqlCommand sc = new SqlCommand("dtxc.Apq_Addin_Insert", SqlConn);
				sc.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sc);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("_OperID", UserID);
				dch.AddParameter("_OpTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
				IPAddress ipa;
				if (IPAddress.TryParse(HttpContext.Current.Request.UserHostAddress, out ipa))
				{
					dch.AddParameter("_OperIP", ipa.GetAddressBytes());
				}

				dch.AddParameter("AddinName", AddinName);
				dch.AddParameter("AddinUrl", AddinUrl);
				dch.AddParameter("AddinDescript", AddinDescript);
				dch.AddParameter("AddinID", 0);

				sc.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
				sc.Parameters["ExMsg"].Direction = ParameterDirection.InputOutput;
				sc.Parameters["AddinID"].Direction = ParameterDirection.InputOutput;

				SqlConn.Open();
				sc.ExecuteNonQuery();

				stReturn.NReturn = System.Convert.ToInt32(sc.Parameters["rtn"].Value);
				stReturn.ExMsg = sc.Parameters["ExMsg"].Value.ToString();
				stReturn.POuts = new object[] {
					sc.Parameters["AddinID"].Value
				};

				sc.Dispose();
				SqlConn.Close();
			}

			return stReturn;
		}

		/// <summary>
		/// 插件删除
		/// </summary>
		/// <param name="AddinID">插件ID</param>
		/// <returns></returns>
		[WebMethod(EnableSession = true, Description = "插件删除")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public Apq.STReturn AddinDelete(long AddinID)
		{
			Apq.STReturn stReturn = new Apq.STReturn();
			Apq.Web.SessionState.HttpSessionState ApqSession = new Apq.Web.SessionState.HttpSessionState(Session);

			if (!CheckLoginPage.CheckAdmin(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			long UserID = System.Convert.ToInt64(ApqSession.User.Rows[0]["UserID"]);

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.Common.GetSqlConnectionString("SqlConnectionString2")))
			{
				SqlCommand sc = new SqlCommand("dtxc.Apq_Addin_Delete", SqlConn);
				sc.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sc);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("_OperID", UserID);
				dch.AddParameter("_OpTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
				IPAddress ipa;
				if (IPAddress.TryParse(HttpContext.Current.Request.UserHostAddress, out ipa))
				{
					dch.AddParameter("_OperIP", ipa.GetAddressBytes());
				}

				dch.AddParameter("AddinID", AddinID);

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
		/// 插件修改
		/// </summary>
		/// <param name="AddinID">插件ID</param>
		/// <param name="AddinName">插件名</param>
		/// <param name="AddinUrl">插件地址</param>
		/// <param name="AddinDescript">插件描述内容</param>
		/// <returns></returns>
		[WebMethod(EnableSession = true, Description = "插件修改")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public Apq.STReturn AddinEdit(long AddinID, string AddinName, string AddinUrl, string AddinDescript)
		{
			Apq.STReturn stReturn = new Apq.STReturn();
			Apq.Web.SessionState.HttpSessionState ApqSession = new Apq.Web.SessionState.HttpSessionState(Session);

			if (!CheckLoginPage.CheckAdmin(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			long UserID = System.Convert.ToInt64(ApqSession.User.Rows[0]["UserID"]);

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.Common.GetSqlConnectionString("SqlConnectionString2")))
			{
				SqlCommand sc = new SqlCommand("dtxc.Apq_Addin_Update", SqlConn);
				sc.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sc);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("_OperID", UserID);
				dch.AddParameter("_OpTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
				IPAddress ipa;
				if (IPAddress.TryParse(HttpContext.Current.Request.UserHostAddress, out ipa))
				{
					dch.AddParameter("_OperIP", ipa.GetAddressBytes());
				}

				dch.AddParameter("AddinID", AddinID);
				dch.AddParameter("AddinName", AddinName);
				dch.AddParameter("AddinUrl", AddinUrl);
				dch.AddParameter("AddinDescript", AddinDescript);

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

		#region 任务管理
		/// <summary>
		/// 列表任务(所有)
		/// </summary>
		[WebMethod(EnableSession = true, Description = "任务列表(所有)")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public Apq.STReturn TaskList(int Pager_Page, int Pager_PageSize, int[] Status)
		{
			Apq.STReturn stReturn = new Apq.STReturn();
			Apq.Web.SessionState.HttpSessionState ApqSession = new Apq.Web.SessionState.HttpSessionState(Session);

			if (!CheckLoginPage.CheckAdmin(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			DataSet ds = new DataSet();

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.Common.GetSqlConnectionString("SqlConnectionString2")))
			{
				DataTable dtStatus = new DataTable();
				dtStatus.Columns.Add("ID", typeof(int));
				foreach (int i in Status)
				{
					dtStatus.Rows.Add(i);
				}

				SqlDataAdapter sda = new SqlDataAdapter("dtxc.Apq_Task_List", SqlConn);
				sda.SelectCommand.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sda.SelectCommand);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("Pager_Page", Pager_Page, DbType.Int32);
				dch.AddParameter("Pager_PageSize", Pager_PageSize);
				dch.AddParameter("Pager_RowCount", 0, DbType.Int32);

				sda.SelectCommand.Parameters.Add("Status", SqlDbType.Structured);
				sda.SelectCommand.Parameters["Status"].TypeName = "tvp:t_int";
				sda.SelectCommand.Parameters["Status"].Value = dtStatus;

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
		/// 任务审核
		/// </summary>
		/// <param name="TaskID">任务ID</param>
		/// <returns></returns>
		[WebMethod(EnableSession = true, Description = "任务审核")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public Apq.STReturn TaskCheckup(long TaskID)
		{
			Apq.STReturn stReturn = new Apq.STReturn();
			Apq.Web.SessionState.HttpSessionState ApqSession = new Apq.Web.SessionState.HttpSessionState(Session);

			if (!CheckLoginPage.CheckAdmin(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			long UserID = System.Convert.ToInt64(ApqSession.User.Rows[0]["UserID"]);

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.Common.GetSqlConnectionString("SqlConnectionString2")))
			{
				SqlCommand sc = new SqlCommand("dtxc.Apq_Task_Checkup", SqlConn);
				sc.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sc);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("_OperID", UserID);
				dch.AddParameter("_OpTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
				IPAddress ipa;
				if (IPAddress.TryParse(HttpContext.Current.Request.UserHostAddress, out ipa))
				{
					dch.AddParameter("_OperIP", ipa.GetAddressBytes());
				}

				dch.AddParameter("TaskID", TaskID);

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

		#region 会员管理
		/// <summary>
		/// 列表会员
		/// </summary>
		/// <param name="UserID">帐号ID</param>
		/// <param name="ContainsSelf">是否包含自己</param>
		/// <param name="ContainsGrand">是否包含子孙</param>
		[WebMethod(EnableSession = true, Description = "会员列表")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public Apq.STReturn UsersListChild(int Pager_Page, int Pager_PageSize, long UserID, bool ContainsSelf, bool ContainsGrand)
		{
			Apq.STReturn stReturn = new Apq.STReturn();
			Apq.Web.SessionState.HttpSessionState ApqSession = new Apq.Web.SessionState.HttpSessionState(Session);

			if (!CheckLoginPage.CheckAdmin(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			DataSet ds = new DataSet();

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
			{
				SqlDataAdapter sda = new SqlDataAdapter("dtxc.Apq_Users_ListChild", SqlConn);
				sda.SelectCommand.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sda.SelectCommand);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("Pager_Page", Pager_Page, DbType.Int32);
				dch.AddParameter("Pager_PageSize", Pager_PageSize);
				dch.AddParameter("Pager_RowCount", 0, DbType.Int32);

				dch.AddParameter("UserID", UserID);
				dch.AddParameter("ContainsSelf", ContainsSelf);
				dch.AddParameter("ContainsGrand", ContainsGrand);

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
		public Apq.STReturn UserToggle(long UserID)
		{
			Apq.STReturn stReturn = new Apq.STReturn();
			Apq.Web.SessionState.HttpSessionState ApqSession = new Apq.Web.SessionState.HttpSessionState(Session);

			if (!CheckLoginPage.CheckAdmin(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			long _OperID = System.Convert.ToInt64(ApqSession.User.Rows[0]["UserID"]);

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
			{
				SqlCommand sc = new SqlCommand("Apq_User.Apq_Toggle_User", SqlConn);
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
		/// 登录名状态切换
		/// </summary>
		/// <param name="UserID">帐号ID</param>
		/// <returns></returns>
		[WebMethod(EnableSession = true, Description = "登录名状态切换")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public Apq.STReturn LoginNameToggle(long UserID)
		{
			Apq.STReturn stReturn = new Apq.STReturn();
			Apq.Web.SessionState.HttpSessionState ApqSession = new Apq.Web.SessionState.HttpSessionState(Session);

			if (!CheckLoginPage.CheckAdmin(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			long _OperID = System.Convert.ToInt64(ApqSession.User.Rows[0]["UserID"]);

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
			{
				SqlCommand sc = new SqlCommand("Apq_User.Apq_Toggle_LoginName", SqlConn);
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
		public Apq.STReturn UserEdit(long UserID, string UserName, short Sex, string PhotoUrl, DateTime Expire, bool Status, bool IsAdmin, DateTime Birthday
			, string IDCard, string Alipay)
		{
			Apq.STReturn stReturn = new Apq.STReturn();
			Apq.Web.SessionState.HttpSessionState ApqSession = new Apq.Web.SessionState.HttpSessionState(Session);

			if (!CheckLoginPage.CheckAdmin(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
			{
				SqlCommand sc = new SqlCommand("dtxc.Apq_Users_Update", SqlConn);
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

				dch.AddParameter("UserName", UserName);
				dch.AddParameter("Sex", Sex);
				dch.AddParameter("PhotoUrl", PhotoUrl);
				dch.AddParameter("Expire", Expire.ToString("yyyy-MM-dd HH:mm:ss.fff"));
				dch.AddParameter("Status", Status);
				dch.AddParameter("IsAdmin", IsAdmin);
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

		#region 支付管理
		/// <summary>
		/// 列表支付申请
		/// </summary>
		[WebMethod(EnableSession = true, Description = "列表支付申请")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public Apq.STReturn PayoutList(int Pager_Page, int Pager_PageSize, long UserID, long PayoutID)
		{
			Apq.STReturn stReturn = new Apq.STReturn();
			Apq.Web.SessionState.HttpSessionState ApqSession = new Apq.Web.SessionState.HttpSessionState(Session);

			if (!CheckLoginPage.CheckAdmin(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			DataSet ds = new DataSet();

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
			{
				SqlDataAdapter sda = new SqlDataAdapter("dtxc.dtxc_Payout_List", SqlConn);
				sda.SelectCommand.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sda.SelectCommand);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("Pager_Page", Pager_Page, DbType.Int32);
				dch.AddParameter("Pager_PageSize", Pager_PageSize);
				dch.AddParameter("Pager_RowCount", 0, DbType.Int32);

				dch.AddParameter("UserID", UserID);
				dch.AddParameter("PayoutID", PayoutID);

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
		/// 支付确认
		/// </summary>
		/// <param name="PayoutID">插件ID</param>
		/// <returns></returns>
		[WebMethod(EnableSession = true, Description = "支付确认")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public Apq.STReturn PayoutConfirm(long PayoutID)
		{
			Apq.STReturn stReturn = new Apq.STReturn();
			Apq.Web.SessionState.HttpSessionState ApqSession = new Apq.Web.SessionState.HttpSessionState(Session);

			if (!CheckLoginPage.CheckAdmin(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			long UserID = System.Convert.ToInt64(ApqSession.User.Rows[0]["UserID"]);

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
			{
				SqlCommand sc = new SqlCommand("dtxc.dtxc_Payout_Confirm", SqlConn);
				sc.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sc);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("_OperID", UserID);
				dch.AddParameter("_OpTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
				IPAddress ipa;
				if (IPAddress.TryParse(HttpContext.Current.Request.UserHostAddress, out ipa))
				{
					dch.AddParameter("_OperIP", ipa.GetAddressBytes());
				}

				dch.AddParameter("PayoutID", PayoutID);

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
		/// 列表支付历史
		/// </summary>
		[WebMethod(EnableSession = true, Description = "列表支付历史")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public Apq.STReturn PayoutLogList(int Pager_Page, int Pager_PageSize, long UserID, long PayoutID)
		{
			Apq.STReturn stReturn = new Apq.STReturn();
			Apq.Web.SessionState.HttpSessionState ApqSession = new Apq.Web.SessionState.HttpSessionState(Session);

			if (!CheckLoginPage.CheckAdmin(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			DataSet ds = new DataSet();

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
			{
				SqlDataAdapter sda = new SqlDataAdapter("dtxc.dtxc_Log_Payout_List", SqlConn);
				sda.SelectCommand.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sda.SelectCommand);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("Pager_Page", Pager_Page, DbType.Int32);
				dch.AddParameter("Pager_PageSize", Pager_PageSize);
				dch.AddParameter("Pager_RowCount", 0, DbType.Int32);

				dch.AddParameter("UserID", UserID);
				dch.AddParameter("PayoutID", PayoutID);

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
	}
}
