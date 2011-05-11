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

namespace Dinner.WS.User
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

		#region 个人信息管理
		/// <summary>
		/// 员工个人信息修改
		/// </summary>
		[WebMethod(EnableSession = true, Description = "员工个人信息修改")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public Apq.STReturn UserEditSelf(long UserID, string Name, short Sex, string PhotoUrl, DateTime Birthday
			, string IDCard, string Alipay)
		{
			Apq.STReturn stReturn = new Apq.STReturn();
			DinnerSession ApqSession = new DinnerSession(Session);

			if (!CheckLoginPage.CheckPwdExpire(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			using (SqlConnection SqlConn = new SqlConnection(Apq.DBC.Common.GetDBConnectoinString("Dinner")))
			{
				SqlCommand sc = new SqlCommand("Dinner.Dinner_User_UpdateSelf", SqlConn);
				sc.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sc);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("_OperID", ApqSession);
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
		/// 员工登录密码修改
		/// </summary>
		[WebMethod(EnableSession = true, Description = "员工登录密码修改")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public Apq.STReturn UserEditLoginPwd(string LoginPwd_C, string LoginPwd)
		{
			Apq.STReturn stReturn = new Apq.STReturn();
			DinnerSession ApqSession = new DinnerSession(Session);

			if (!CheckLoginPage.CheckLogin(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			// 检测旧密码
			System.Security.Cryptography.SHA512 SHA512 = System.Security.Cryptography.SHA512.Create();
			byte[] binLoginPwd_C = SHA512.ComputeHash(System.Text.Encoding.Unicode.GetBytes(LoginPwd_C));
			string SqlLoginPwd_C = Apq.Data.SqlClient.Common.ConvertToSqlON(binLoginPwd_C);
			string SqlLoginPwd_DB = Apq.Data.SqlClient.Common.ConvertToSqlON(ApqSession.ApqLogin.LoginPwd);
			if (SqlLoginPwd_C != SqlLoginPwd_DB)
			{
				stReturn.NReturn = -1;
				stReturn.ExMsg = "原密码输入错误";
				return stReturn;
			}

			byte[] binLoginPwd = SHA512.ComputeHash(System.Text.Encoding.Unicode.GetBytes(LoginPwd));
			using (SqlConnection SqlConn = new SqlConnection(Apq.DBC.Common.GetDBConnectoinString("Dinner")))
			{
				SqlCommand sc = new SqlCommand("dbo.Dinner_User_UpdateLoginPwd", SqlConn);
				sc.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sc);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("LoginID", ApqSession.ApqLogin.LoginID);

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
			ApqSession.ApqLogin.LoginPwd = binLoginPwd;

			// 返回客户端
			stReturn.FNReturn = Apq.Data.SqlClient.Common.ConvertToSqlON(binLoginPwd);
			return stReturn;
		}
		#endregion

		#region 点餐
		/// <summary>
		/// 列表餐馆(所有)
		/// </summary>
		[WebMethod(EnableSession = true, Description = "餐馆列表")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public Apq.STReturn Dinner_Restaurant_List()
		{
			Apq.STReturn stReturn = new Apq.STReturn();
			DinnerSession ApqSession = new DinnerSession(Session);

			if (!CheckLoginPage.CheckLogin(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			DataSet ds = new DataSet();

			using (SqlConnection SqlConn = new SqlConnection(Apq.DBC.Common.GetDBConnectoinString("Dinner")))
			{
				SqlDataAdapter sda = new SqlDataAdapter("dbo.Dinner_Restaurant_List", SqlConn);
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
		/// 菜品列表(按餐馆)
		/// </summary>
		[WebMethod(EnableSession = true, Description = "菜品列表(按餐馆)")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public Apq.STReturn Dinner_Food_List(long RestID)
		{
			Apq.STReturn stReturn = new Apq.STReturn();
			DinnerSession ApqSession = new DinnerSession(Session);

			if (!CheckLoginPage.CheckLogin(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			DataSet ds = new DataSet();

			using (SqlConnection SqlConn = new SqlConnection(Apq.DBC.Common.GetDBConnectoinString("Dinner")))
			{
				SqlDataAdapter sda = new SqlDataAdapter("dbo.Dinner_Food_List", SqlConn);
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
		/// 点餐
		/// </summary>
		[WebMethod(EnableSession = true, Description = "点餐")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public Apq.STReturn Dinner_EmDinner_Insert(long FoodID)
		{
			Apq.STReturn stReturn = new Apq.STReturn();
			DinnerSession ApqSession = new DinnerSession(Session);

			if (!CheckLoginPage.CheckLogin(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			DataSet ds = new DataSet();

			using (SqlConnection SqlConn = new SqlConnection(Apq.DBC.Common.GetDBConnectoinString("Dinner")))
			{
				SqlCommand sc = new SqlCommand("dbo.Dinner_EmDinner_Insert", SqlConn);
				sc.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sc);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("EmID", ApqSession.Employee.EmID);
				dch.AddParameter("FoodID", FoodID);

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

		#region 历史记录
		/// <summary>
		/// 历史记录
		/// </summary>
		[WebMethod(EnableSession = true, Description = "历史记录")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public Apq.STReturn Dinner_EmDinner_ListPager(int start, int limit)
		{
			int Pager_Page = start / limit;
			int Pager_PageSize = limit;

			Apq.STReturn stReturn = new Apq.STReturn();
			DinnerSession ApqSession = new DinnerSession(Session);

			if (!CheckLoginPage.CheckLogin(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			DataSet ds = new DataSet();

			using (SqlConnection SqlConn = new SqlConnection(Apq.DBC.Common.GetDBConnectoinString("Dinner")))
			{
				SqlDataAdapter sda = new SqlDataAdapter("dbo.Dinner_EmDinner_ListPager", SqlConn);
				sda.SelectCommand.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sda.SelectCommand);
				dch.AddParameter("rtn", 0, DbType.Int32);
				//dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("Pager_Page", Pager_Page, DbType.Int32);
				dch.AddParameter("Pager_PageSize", Pager_PageSize);
				dch.AddParameter("Pager_RowCount", 0, DbType.Int32);

				dch.AddParameter("EmID", ApqSession.Employee.EmID);

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
		/// 点餐
		/// </summary>
		[WebMethod(EnableSession = true, Description = "点餐")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public Apq.STReturn Dinner_EmDinner_Delete(long ID)
		{
			Apq.STReturn stReturn = new Apq.STReturn();
			DinnerSession ApqSession = new DinnerSession(Session);

			if (!CheckLoginPage.CheckLogin(ref stReturn, ApqSession))
			{
				return stReturn;
			}

			DataSet ds = new DataSet();

			using (SqlConnection SqlConn = new SqlConnection(Apq.DBC.Common.GetDBConnectoinString("Dinner")))
			{
				SqlCommand sc = new SqlCommand("dbo.Dinner_EmDinner_Delete", SqlConn);
				sc.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sc);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("ID", ID);

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
