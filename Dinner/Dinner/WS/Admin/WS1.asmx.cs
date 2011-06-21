using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Web.Script.Services;

using System.Net;
using System.Data.Common;

namespace Dinner.WS.Admin
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

		#region 餐馆管理
		/// <summary>
		/// 列表餐馆(所有)
		/// </summary>
		[WebMethod(EnableSession = true, Description = "餐馆列表")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public Apq.STReturn Dinner_Restaurant_List()
		{
			Apq.STReturn stReturn = new Apq.STReturn();
			DinnerSession ApqSession = new DinnerSession(Session);

			if (!CheckLoginPage.CheckAdmin(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			DataSet ds = new DataSet();
			DbConnection SqlConn = null;
			using (SqlConn = Apq.DBC.Common.CreateDBConnection("Dinner", ref SqlConn))
			{
				Apq.Data.Common.DbConnectionHelper dbch = new Apq.Data.Common.DbConnectionHelper(SqlConn);
				DbDataAdapter sda = dbch.CreateAdapter();
				sda.SelectCommand.CommandText = "dbo.Dinner_Restaurant_List";
				sda.SelectCommand.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sda.SelectCommand);
				dch.AddParameter("rtn", 0, DbType.Int32);

				sda.SelectCommand.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;

				SqlConn.Open();
				sda.Fill(ds);

				stReturn.NReturn = System.Convert.ToInt32(sda.SelectCommand.Parameters["rtn"].Value);
				stReturn.FNReturn = ds.Tables[0];

				sda.Dispose();
				SqlConn.Close();
			}

			return stReturn;
		}

		/// <summary>
		/// 餐馆保存
		/// </summary>
		/// <param name="TaskID">餐馆ID</param>
		/// <returns></returns>
		[WebMethod(EnableSession = true, Description = "餐馆保存")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public Apq.STReturn Dinner_Restaurant_Save(long RestID, string RestName, string RestAddr)
		{
			Apq.STReturn stReturn = new Apq.STReturn();
			DinnerSession ApqSession = new DinnerSession(Session);

			if (!CheckLoginPage.CheckAdmin(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			DbConnection SqlConn = null;
			using (SqlConn = Apq.DBC.Common.CreateDBConnection("Dinner", ref SqlConn))
			{
				Apq.Data.Common.DbConnectionHelper dbch = new Apq.Data.Common.DbConnectionHelper(SqlConn);
				DbCommand sc = SqlConn.CreateCommand();
				sc.CommandText = "dbo.Dinner_Restaurant_Save";
				sc.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sc);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("RestID", RestID);
				dch.AddParameter("RestName", RestName);
				dch.AddParameter("RestAddr", RestAddr);

				sc.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
				sc.Parameters["ExMsg"].Direction = ParameterDirection.InputOutput;
				sc.Parameters["RestID"].Direction = ParameterDirection.InputOutput;

				SqlConn.Open();
				sc.ExecuteNonQuery();

				stReturn.NReturn = System.Convert.ToInt32(sc.Parameters["rtn"].Value);
				stReturn.ExMsg = Apq.Convert.ChangeType<string>(sc.Parameters["ExMsg"].Value);
				stReturn.POuts.Add(sc.Parameters["RestID"]);

				sc.Dispose();
				SqlConn.Close();
			}

			return stReturn;
		}

		/// <summary>
		/// 餐馆删除
		/// </summary>
		/// <param name="TaskID">餐馆ID</param>
		/// <returns></returns>
		[WebMethod(EnableSession = true, Description = "餐馆删除")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public Apq.STReturn Dinner_Restaurant_Delete(long RestID)
		{
			Apq.STReturn stReturn = new Apq.STReturn();
			DinnerSession ApqSession = new DinnerSession(Session);

			if (!CheckLoginPage.CheckAdmin(ref stReturn, ApqSession))
			{
				return stReturn;
			}
			
			DbConnection SqlConn = null;
			using (SqlConn = Apq.DBC.Common.CreateDBConnection("Dinner", ref SqlConn))
			{
				Apq.Data.Common.DbConnectionHelper dbch = new Apq.Data.Common.DbConnectionHelper(SqlConn);
				DbCommand sc = SqlConn.CreateCommand();
				sc.CommandText = "dbo.Dinner_Restaurant_Delete";
				sc.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sc);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("RestID", RestID);

				sc.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
				sc.Parameters["ExMsg"].Direction = ParameterDirection.InputOutput;

				SqlConn.Open();
				sc.ExecuteNonQuery();

				stReturn.NReturn = System.Convert.ToInt32(sc.Parameters["rtn"].Value);
				stReturn.ExMsg = Apq.Convert.ChangeType<string>(sc.Parameters["ExMsg"].Value);

				sc.Dispose();
				SqlConn.Close();
			}

			return stReturn;
		}
		#endregion

		#region 员工管理
		/// <summary>
		/// 列表员工
		/// </summary>
		[WebMethod(EnableSession = true, Description = "员工列表")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public Apq.STReturn Dinner_Employee_ListPager(int start, int limit)
		{
			int Pager_Page = start / limit;
			int Pager_PageSize = limit;

			Apq.STReturn stReturn = new Apq.STReturn();
			DinnerSession ApqSession = new DinnerSession(Session);

			if (!CheckLoginPage.CheckAdmin(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			DataSet ds = new DataSet();
			
			DbConnection SqlConn = null;
			using (SqlConn = Apq.DBC.Common.CreateDBConnection("Dinner", ref SqlConn))
			{
				Apq.Data.Common.DbConnectionHelper dbch = new Apq.Data.Common.DbConnectionHelper(SqlConn);
				DbDataAdapter sda = dbch.CreateAdapter();
				sda.SelectCommand.CommandText = "dbo.Dinner_Employee_ListPager";
				sda.SelectCommand.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sda.SelectCommand);
				dch.AddParameter("rtn", 0, DbType.Int32);
				//dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("Pager_Page", Pager_Page, DbType.Int32);
				dch.AddParameter("Pager_PageSize", Pager_PageSize);
				dch.AddParameter("Pager_RowCount", 0, DbType.Int32);

				sda.SelectCommand.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
				//sda.SelectCommand.Parameters["ExMsg"].Direction = ParameterDirection.InputOutput;

				sda.SelectCommand.Parameters["Pager_RowCount"].Direction = ParameterDirection.InputOutput;

				SqlConn.Open();
				sda.Fill(ds);

				stReturn.NReturn = System.Convert.ToInt32(sda.SelectCommand.Parameters["rtn"].Value);
				//stReturn.ExMsg = sda.SelectCommand.Parameters["ExMsg"].Value.ToString();
				stReturn.FNReturn = ds.Tables[0];
				stReturn.POuts.Add(sda.SelectCommand.Parameters["Pager_Page"].Value);
				stReturn.POuts.Add(sda.SelectCommand.Parameters["Pager_RowCount"].Value);

				sda.Dispose();
				SqlConn.Close();
			}

			return stReturn;
		}

		/*
		/// <summary>
		/// 员工状态切换
		/// </summary>
		/// <param name="EmID">员工ID</param>
		/// <returns></returns>
		[WebMethod(EnableSession = true, Description = "员工状态切换")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public Apq.STReturn UserToggle(long EmID)
		{
			Apq.STReturn stReturn = new Apq.STReturn();
			DinnerSession ApqSession = new DinnerSession(Session);

			if (!CheckLoginPage.CheckAdmin(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
			{
				SqlCommand sc = new SqlCommand("Apq_User.Apq_Toggle_User", SqlConn);
				sc.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sc);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("_OperID", ApqSession.Employee.EmID);
				dch.AddParameter("_OpTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
				IPAddress ipa;
				if (IPAddress.TryParse(HttpContext.Current.Request.UserHostAddress, out ipa))
				{
					dch.AddParameter("_OperIP", ipa.GetAddressBytes());
				}

				dch.AddParameter("EmID", EmID);

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
		/// <param name="EmID">员工ID</param>
		/// <returns></returns>
		[WebMethod(EnableSession = true, Description = "登录名状态切换")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public Apq.STReturn LoginNameToggle(long EmID)
		{
			Apq.STReturn stReturn = new Apq.STReturn();
			DinnerSession ApqSession = new DinnerSession(Session);

			if (!CheckLoginPage.CheckAdmin(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
			{
				SqlCommand sc = new SqlCommand("Apq_User.Apq_Toggle_LoginName", SqlConn);
				sc.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sc);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("_OperID", ApqSession.Employee.EmID);
				dch.AddParameter("_OpTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
				IPAddress ipa;
				if (IPAddress.TryParse(HttpContext.Current.Request.UserHostAddress, out ipa))
				{
					dch.AddParameter("_OperIP", ipa.GetAddressBytes());
				}

				dch.AddParameter("EmID", EmID);

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
		*/

		/// <summary>
		/// 修改员工
		/// </summary>
		[WebMethod(EnableSession = true, Description = "修改员工")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public Apq.STReturn Dinner_Employee_Update(long EmID, string EmName, bool EmStatus, bool IsAdmin, string LoginName)
		{
			Apq.STReturn stReturn = new Apq.STReturn();
			DinnerSession ApqSession = new DinnerSession(Session);

			if (!CheckLoginPage.CheckAdmin(ref stReturn, ApqSession))
			{
				return stReturn;
			}
			
			DbConnection SqlConn = null;
			using (SqlConn = Apq.DBC.Common.CreateDBConnection("Dinner", ref SqlConn))
			{
				Apq.Data.Common.DbConnectionHelper dbch = new Apq.Data.Common.DbConnectionHelper(SqlConn);
				DbCommand sc = SqlConn.CreateCommand();
				sc.CommandText = "dbo.Dinner_Employee_Update";
				sc.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sc);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("EmID", EmID);

				dch.AddParameter("EmName", EmName);
				dch.AddParameter("EmStatus", EmStatus, DbType.Int32);
				dch.AddParameter("IsAdmin", IsAdmin);
				dch.AddParameter("LoginName", LoginName);

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

		#region 菜品管理
		/// <summary>
		/// 菜品列表(按餐馆)
		/// </summary>
		[WebMethod(EnableSession = true, Description = "菜品列表(按餐馆)")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public Apq.STReturn Dinner_Food_List(long RestID)
		{
			Apq.STReturn stReturn = new Apq.STReturn();
			DinnerSession ApqSession = new DinnerSession(Session);

			if (!CheckLoginPage.CheckAdmin(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			DataSet ds = new DataSet();
			
			DbConnection SqlConn = null;
			using (SqlConn = Apq.DBC.Common.CreateDBConnection("Dinner", ref SqlConn))
			{
				Apq.Data.Common.DbConnectionHelper dbch = new Apq.Data.Common.DbConnectionHelper(SqlConn);
				DbDataAdapter sda = dbch.CreateAdapter();
				sda.SelectCommand.CommandText = "dbo.Dinner_Food_List";
				sda.SelectCommand.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sda.SelectCommand);
				dch.AddParameter("rtn", 0, DbType.Int32);
				//dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("RestID", RestID);

				sda.SelectCommand.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
				//sda.SelectCommand.Parameters["ExMsg"].Direction = ParameterDirection.InputOutput;

				SqlConn.Open();
				sda.Fill(ds);

				stReturn.NReturn = System.Convert.ToInt32(sda.SelectCommand.Parameters["rtn"].Value);
				//stReturn.ExMsg = sda.SelectCommand.Parameters["ExMsg"].Value.ToString();
				stReturn.FNReturn = ds.Tables[0];

				sda.Dispose();
				SqlConn.Close();
			}

			return stReturn;
		}

		/// <summary>
		/// 菜品保存
		/// </summary>
		/// <param name="TaskID">菜品ID</param>
		/// <returns></returns>
		[WebMethod(EnableSession = true, Description = "菜品保存")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public Apq.STReturn Dinner_Food_Save(long FoodID, long RestID, string FoodName, decimal FoodPrice)
		{
			Apq.STReturn stReturn = new Apq.STReturn();
			DinnerSession ApqSession = new DinnerSession(Session);

			if (!CheckLoginPage.CheckAdmin(ref stReturn, ApqSession))
			{
				return stReturn;
			}
			
			DbConnection SqlConn = null;
			using (SqlConn = Apq.DBC.Common.CreateDBConnection("Dinner", ref SqlConn))
			{
				Apq.Data.Common.DbConnectionHelper dbch = new Apq.Data.Common.DbConnectionHelper(SqlConn);
				DbCommand sc = SqlConn.CreateCommand();
				sc.CommandText = "dbo.Dinner_Food_Save";
				sc.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sc);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("FoodID", FoodID);
				dch.AddParameter("RestID", RestID);
				dch.AddParameter("FoodName", FoodName);
				dch.AddParameter("FoodPrice", FoodPrice);

				sc.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
				sc.Parameters["ExMsg"].Direction = ParameterDirection.InputOutput;
				sc.Parameters["FoodID"].Direction = ParameterDirection.InputOutput;

				SqlConn.Open();
				sc.ExecuteNonQuery();

				stReturn.NReturn = System.Convert.ToInt32(sc.Parameters["rtn"].Value);
				stReturn.ExMsg = Apq.Convert.ChangeType<string>(sc.Parameters["ExMsg"].Value);
				stReturn.POuts.Add(sc.Parameters["FoodID"]);

				sc.Dispose();
				SqlConn.Close();
			}

			return stReturn;
		}

		/// <summary>
		/// 菜品删除
		/// </summary>
		/// <param name="TaskID">菜品ID</param>
		/// <returns></returns>
		[WebMethod(EnableSession = true, Description = "菜品删除")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public Apq.STReturn Dinner_Food_Delete(long FoodID)
		{
			Apq.STReturn stReturn = new Apq.STReturn();
			DinnerSession ApqSession = new DinnerSession(Session);

			if (!CheckLoginPage.CheckAdmin(ref stReturn, ApqSession))
			{
				return stReturn;
			}
			
			DbConnection SqlConn = null;
			using (SqlConn = Apq.DBC.Common.CreateDBConnection("Dinner", ref SqlConn))
			{
				Apq.Data.Common.DbConnectionHelper dbch = new Apq.Data.Common.DbConnectionHelper(SqlConn);
				DbCommand sc = SqlConn.CreateCommand();
				sc.CommandText = "dbo.Dinner_Food_Delete";
				sc.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sc);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("FoodID", FoodID);

				sc.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
				sc.Parameters["ExMsg"].Direction = ParameterDirection.InputOutput;

				SqlConn.Open();
				sc.ExecuteNonQuery();

				stReturn.NReturn = System.Convert.ToInt32(sc.Parameters["rtn"].Value);
				stReturn.ExMsg = Apq.Convert.ChangeType<string>(sc.Parameters["ExMsg"].Value);

				sc.Dispose();
				SqlConn.Close();
			}

			return stReturn;
		}

		#endregion

		#region 点餐统计
		/// <summary>
		/// 历史记录
		/// </summary>
		[WebMethod(EnableSession = true, Description = "历史记录")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public Apq.STReturn Dinner_Admin_EmDinner_ListPager(int start, int limit, DateTime BTime, DateTime ETime, long RestID, bool IsDoDinner)
		{
			int Pager_Page = start / limit;
			int Pager_PageSize = limit;

			Apq.STReturn stReturn = new Apq.STReturn();
			DinnerSession ApqSession = new DinnerSession(Session);

			if (!CheckLoginPage.CheckAdmin(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			DataSet ds = new DataSet();
			
			DbConnection SqlConn = null;
			using (SqlConn = Apq.DBC.Common.CreateDBConnection("Dinner", ref SqlConn))
			{
				Apq.Data.Common.DbConnectionHelper dbch = new Apq.Data.Common.DbConnectionHelper(SqlConn);
				DbDataAdapter sda = dbch.CreateAdapter();
				sda.SelectCommand.CommandText = "dbo.Dinner_Admin_EmDinner_ListPager";
				sda.SelectCommand.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sda.SelectCommand);
				dch.AddParameter("rtn", 0, DbType.Int32);
				//dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("Pager_Page", Pager_Page, DbType.Int32);
				dch.AddParameter("Pager_PageSize", Pager_PageSize);
				dch.AddParameter("Pager_RowCount", 0, DbType.Int32);

				dch.AddParameter("BTime", BTime);
				dch.AddParameter("ETime", ETime);
				dch.AddParameter("RestID", RestID);
				dch.AddParameter("State", IsDoDinner);

				sda.SelectCommand.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
				//sda.SelectCommand.Parameters["ExMsg"].Direction = ParameterDirection.InputOutput;

				sda.SelectCommand.Parameters["Pager_RowCount"].Direction = ParameterDirection.InputOutput;

				SqlConn.Open();
				sda.Fill(ds);

				stReturn.NReturn = System.Convert.ToInt32(sda.SelectCommand.Parameters["rtn"].Value);
				//stReturn.ExMsg = sda.SelectCommand.Parameters["ExMsg"].Value.ToString();
				stReturn.FNReturn = ds.Tables[0];
				stReturn.POuts.Add(sda.SelectCommand.Parameters["Pager_Page"].Value);
				stReturn.POuts.Add(sda.SelectCommand.Parameters["Pager_RowCount"].Value);

				sda.Dispose();
				SqlConn.Close();
			}

			return stReturn;
		}

		/// <summary>
		/// 菜品统计
		/// </summary>
		[WebMethod(EnableSession = true, Description = "菜品统计")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public Apq.STReturn Dinner_Stat_EmDinner_Food(DateTime BTime, DateTime ETime, bool State)
		{
			Apq.STReturn stReturn = new Apq.STReturn();
			DinnerSession ApqSession = new DinnerSession(Session);

			if (!CheckLoginPage.CheckAdmin(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			DataSet ds = new DataSet();
			
			DbConnection SqlConn = null;
			using (SqlConn = Apq.DBC.Common.CreateDBConnection("Dinner", ref SqlConn))
			{
				Apq.Data.Common.DbConnectionHelper dbch = new Apq.Data.Common.DbConnectionHelper(SqlConn);
				DbDataAdapter sda = dbch.CreateAdapter();
				sda.SelectCommand.CommandText = "dbo.Dinner_Stat_EmDinner_Food";
				sda.SelectCommand.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sda.SelectCommand);
				dch.AddParameter("rtn", 0, DbType.Int32);
				//dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("BTime", BTime);
				dch.AddParameter("ETime", ETime);
				dch.AddParameter("State", State);

				sda.SelectCommand.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
				//sda.SelectCommand.Parameters["ExMsg"].Direction = ParameterDirection.InputOutput;

				SqlConn.Open();
				sda.Fill(ds);

				stReturn.NReturn = System.Convert.ToInt32(sda.SelectCommand.Parameters["rtn"].Value);
				//stReturn.ExMsg = sda.SelectCommand.Parameters["ExMsg"].Value.ToString();
				stReturn.FNReturn = ds.Tables[0];

				sda.Dispose();
				SqlConn.Close();
			}

			return stReturn;
		}

		/// <summary>
		/// 确认订餐
		/// </summary>
		[WebMethod(EnableSession = true, Description = "确认订餐")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public Apq.STReturn Dinner_Admin_EmDinner_DoDinner(DateTime BTime, DateTime ETime)
		{
			Apq.STReturn stReturn = new Apq.STReturn();
			DinnerSession ApqSession = new DinnerSession(Session);

			if (!CheckLoginPage.CheckAdmin(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			DataSet ds = new DataSet();
			
			DbConnection SqlConn = null;
			using (SqlConn = Apq.DBC.Common.CreateDBConnection("Dinner", ref SqlConn))
			{
				Apq.Data.Common.DbConnectionHelper dbch = new Apq.Data.Common.DbConnectionHelper(SqlConn);
				DbCommand sc = SqlConn.CreateCommand();
				sc.CommandText = "dbo.Dinner_Admin_EmDinner_DoDinner";
				sc.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sc);
				dch.AddParameter("rtn", 0, DbType.Int32);
				//dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("BTime", BTime);
				dch.AddParameter("ETime", ETime);

				sc.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
				//sc.Parameters["ExMsg"].Direction = ParameterDirection.InputOutput;

				SqlConn.Open();
				sc.ExecuteNonQuery();

				stReturn.NReturn = System.Convert.ToInt32(sc.Parameters["rtn"].Value);
				//stReturn.ExMsg = sc.Parameters["ExMsg"].Value.ToString();

				sc.Dispose();
				SqlConn.Close();
			}

			return stReturn;
		}
		#endregion
	}
}
