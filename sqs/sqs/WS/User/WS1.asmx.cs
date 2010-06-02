using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Web.Script.Services;
using Apq.Web.AJAX;
using System.Xml;
using System.Net;

namespace dtxc.WS.User
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

		#region 登出
		/// <summary>
		/// 登出
		/// </summary>
		[WebMethod(EnableSession = true, Description = "登出")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml)]
		public STReturn Logout()
		{
			STReturn stReturn = new STReturn();

			// 结束会话
			Session.Abandon();

			stReturn.NReturn = 1;
			stReturn.ExMsg = "登出成功";
			return stReturn;
		}
		#endregion

		#region 菜单
		/// <summary>
		/// 获取菜单
		/// </summary>
		//[WebMethod(EnableSession = true, Description = "获取Xml表示的菜单")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public STReturn GetXmlMenu()
		{
			STReturn stReturn = new STReturn();

			stReturn.NReturn = 1;
			stReturn.ExMsg = "登出成功";
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

			if (!CheckLoginPage.CheckPwdExpire(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			DataSet ds = new DataSet();

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
			{
				SqlDataAdapter sda = new SqlDataAdapter("dtxc.Apq_Task_ListCanTake", SqlConn);
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

		/// <summary>
		/// 列表自身任务
		/// </summary>
		[WebMethod(EnableSession = true, Description = "任务列表")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public STReturn TaskListSelf(int Pager_Page, int Pager_PageSize, int[] Status)
		{
			STReturn stReturn = new STReturn();
			Apq.Web.SessionState.HttpSessionState ApqSession = new Apq.Web.SessionState.HttpSessionState(Session);

			if (!CheckLoginPage.CheckPwdExpire(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			DataSet ds = new DataSet();

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
			{
				DataTable dtStatus = new DataTable();
				dtStatus.Columns.Add("ID", typeof(int));
				foreach (int i in Status)
				{
					dtStatus.Rows.Add(i);
				}

				SqlDataAdapter sda = new SqlDataAdapter("dtxc.Apq_Task_ListSelf", SqlConn);
				sda.SelectCommand.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sda.SelectCommand);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("Pager_Page", Pager_Page, DbType.Int32);
				dch.AddParameter("Pager_PageSize", Pager_PageSize);
				dch.AddParameter("Pager_RowCount", 0, DbType.Int32);

				dch.AddParameter("UserID", ApqSession.UserID);
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
		/// 任务发布
		/// </summary>
		/// <param name="TaskName">任务名</param>
		/// <param name="TaskContent">任务描述内容</param>
		/// <param name="BTime">开始时间</param>
		/// <param name="ETime">结束时间</param>
		/// <param name="AddinID">插件ID</param>
		/// <param name="Price">单价</param>
		/// <param name="ParentPrice">上级单价</param>
		/// <param name="NeedChangeIP">是否需要更改IP</param>
		/// <param name="IsAutoStart">是否自动开始</param>
		/// <param name="TaskMoney">发布总价</param>
		/// <returns></returns>
		[WebMethod(EnableSession = true, Description = "任务发布")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public STReturn TaskAdd(string TaskName, string TaskContent, DateTime BTime, DateTime ETime, long AddinID, decimal Price, decimal ParentPrice
			, bool NeedChangeIP, bool IsAutoStart, decimal TaskMoney)
		{
			STReturn stReturn = new STReturn();
			Apq.Web.SessionState.HttpSessionState ApqSession = new Apq.Web.SessionState.HttpSessionState(Session);

			if (!CheckLoginPage.CheckPwdExpire(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			long UserID = Convert.ToInt64(ApqSession.User.Rows[0]["UserID"]);

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
			{
				SqlCommand sc = new SqlCommand("dtxc.Apq_Task_Insert", SqlConn);
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

				dch.AddParameter("TaskName", TaskName);
				dch.AddParameter("TaskContent", TaskContent);
				dch.AddParameter("BTime", BTime);
				dch.AddParameter("ETime", ETime);
				dch.AddParameter("AddinID", AddinID);
				dch.AddParameter("Price", Price);
				dch.AddParameter("ParentPrice", ParentPrice);
				dch.AddParameter("NeedChangeIP", NeedChangeIP);
				dch.AddParameter("IsAutoStart", IsAutoStart);
				dch.AddParameter("TaskMoney", TaskMoney);
				dch.AddParameter("TaskID", 0);

				sc.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
				sc.Parameters["ExMsg"].Direction = ParameterDirection.InputOutput;
				sc.Parameters["TaskID"].Direction = ParameterDirection.InputOutput;

				SqlConn.Open();
				sc.ExecuteNonQuery();

				stReturn.NReturn = System.Convert.ToInt32(sc.Parameters["rtn"].Value);
				stReturn.ExMsg = sc.Parameters["ExMsg"].Value.ToString();
				stReturn.POuts = new object[] {
					sc.Parameters["TaskID"].Value
				};

				sc.Dispose();
				SqlConn.Close();
			}

			return stReturn;
		}

		/// <summary>
		/// 任务删除
		/// </summary>
		/// <param name="TaskID">任务ID</param>
		/// <returns></returns>
		[WebMethod(EnableSession = true, Description = "任务删除")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public STReturn TaskDelete(long TaskID)
		{
			STReturn stReturn = new STReturn();
			Apq.Web.SessionState.HttpSessionState ApqSession = new Apq.Web.SessionState.HttpSessionState(Session);

			if (!CheckLoginPage.CheckPwdExpire(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			long UserID = Convert.ToInt64(ApqSession.User.Rows[0]["UserID"]);

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
			{
				SqlCommand sc = new SqlCommand("dtxc.Apq_Task_Delete", SqlConn);
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


		/// <summary>
		/// 任务修改
		/// </summary>
		/// <param name="TaskID">任务ID</param>
		/// <param name="TaskName">任务名</param>
		/// <param name="TaskContent">任务描述内容</param>
		/// <param name="BTime">开始时间</param>
		/// <param name="ETime">结束时间</param>
		/// <param name="AddinID">插件ID</param>
		/// <param name="Price">单价</param>
		/// <param name="ParentPrice">上级单价</param>
		/// <param name="NeedChangeIP">是否需要更改IP</param>
		/// <param name="IsAutoStart">是否自动开始</param>
		/// <param name="TaskMoney">发布总价</param>
		/// <returns></returns>
		[WebMethod(EnableSession = true, Description = "任务修改")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public STReturn TaskEdit(long TaskID, string TaskName, string TaskContent, DateTime BTime, DateTime ETime, long AddinID, decimal Price, decimal ParentPrice
			, bool NeedChangeIP, bool IsAutoStart, decimal TaskMoney)
		{
			STReturn stReturn = new STReturn();
			Apq.Web.SessionState.HttpSessionState ApqSession = new Apq.Web.SessionState.HttpSessionState(Session);

			if (!CheckLoginPage.CheckPwdExpire(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			long UserID = Convert.ToInt64(ApqSession.User.Rows[0]["UserID"]);

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
			{
				SqlCommand sc = new SqlCommand("dtxc.Apq_Task_Update", SqlConn);
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
				dch.AddParameter("TaskName", TaskName);
				dch.AddParameter("TaskContent", TaskContent);
				dch.AddParameter("BTime", BTime);
				dch.AddParameter("ETime", ETime);
				dch.AddParameter("AddinID", AddinID);
				dch.AddParameter("Price", Price);
				dch.AddParameter("ParentPrice", ParentPrice);
				dch.AddParameter("NeedChangeIP", NeedChangeIP);
				dch.AddParameter("IsAutoStart", IsAutoStart);
				dch.AddParameter("TaskMoney", TaskMoney);

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
		/// 任务领取
		/// </summary>
		/// <param name="TaskID">任务ID</param>
		/// <returns></returns>
		[WebMethod(EnableSession = true, Description = "任务领取")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public STReturn TaskTake(long TaskID)
		{
			STReturn stReturn = new STReturn();
			Apq.Web.SessionState.HttpSessionState ApqSession = new Apq.Web.SessionState.HttpSessionState(Session);

			if (!CheckLoginPage.CheckPwdExpire(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
			{
				SqlCommand sc = new SqlCommand("dtxc.Apq_Task_Take", SqlConn);
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

		/// <summary>
		/// 任务投票
		/// </summary>
		/// <param name="TaskID">任务ID</param>
		/// <returns></returns>
		[WebMethod(EnableSession = true, Description = "任务投票")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public STReturn TaskVote(long TaskID)
		{
			STReturn stReturn = new STReturn();
			Apq.Web.SessionState.HttpSessionState ApqSession = new Apq.Web.SessionState.HttpSessionState(Session);

			if (!CheckLoginPage.CheckPwdExpire(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
			{
				SqlCommand sc = new SqlCommand("dtxc.dtxc_Task_Vote", SqlConn);
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

		/// <summary>
		/// 任务结算
		/// </summary>
		/// <param name="TaskID">任务ID</param>
		/// <returns></returns>
		[WebMethod(EnableSession = true, Description = "任务结算")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public STReturn TaskBalance(long TaskID)
		{
			STReturn stReturn = new STReturn();
			Apq.Web.SessionState.HttpSessionState ApqSession = new Apq.Web.SessionState.HttpSessionState(Session);

			if (!CheckLoginPage.CheckPwdExpire(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
			{
				SqlCommand sc = new SqlCommand("dtxc.Apq_Task_BalanceOne", SqlConn);
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
	}
}
