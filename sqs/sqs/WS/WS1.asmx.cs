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

		#region 新闻
		/// <summary>
		/// 新闻列表
		/// </summary>
		[WebMethod(EnableSession = true, Description = "新闻列表")]
		//[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
		public STReturn NewsList(int Pager_Page, int Pager_PageSize)
		{
			STReturn stReturn = new STReturn();
			Apq.Web.SessionState.HttpSessionState ApqSession = new Apq.Web.SessionState.HttpSessionState(Session);

			//if (!CheckLogin(ref stReturn, ApqSession))
			//{
			//    return stReturn;
			//}

			DataSet ds = new DataSet();
			long UserID = Convert.ToInt64(ApqSession.User.Rows[0]["UserID"]);

			using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
			{
				SqlDataAdapter sda = new SqlDataAdapter("dtxc.News_List", SqlConn);
				sda.SelectCommand.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sda.SelectCommand);
				dch.AddParameter("rtn", 0, DbType.Int32);
				dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

				dch.AddParameter("Pager_Page", Pager_Page);
				dch.AddParameter("Pager_PageSize", Pager_PageSize);
				dch.AddParameter("Pager_RowCount", 0, DbType.Int32);

				sda.SelectCommand.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
				sda.SelectCommand.Parameters["ExMsg"].Direction = ParameterDirection.InputOutput;

				sda.SelectCommand.Parameters["Pager_RowCount"].Direction = ParameterDirection.InputOutput;

				SqlConn.Open();
				sda.Fill(ds);

				stReturn.NReturn = System.Convert.ToInt32(sda.SelectCommand.Parameters["rtn"].Value);
				stReturn.ExMsg = sda.SelectCommand.Parameters["ExMsg"].Value.ToString();
				stReturn.FNReturn = ds.Tables[0];
				stReturn.POuts = new object[]{
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
