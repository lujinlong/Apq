using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace PV_Imei
{
	/// <summary>
	/// Service1 的摘要说明
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
	// [System.Web.Script.Services.ScriptService]
	public class ws : System.Web.Services.WebService
	{
		[WebMethod]
		public DataSet PV_Imei(string Imei)
		{
			DataSet ds = new DataSet();

			SqlDataAdapter sda = new SqlDataAdapter("dbo.Wb_PV_List", ConfigurationManager.ConnectionStrings["znlyDW"].ConnectionString);
			sda.SelectCommand.CommandType = CommandType.StoredProcedure;
			sda.SelectCommand.Parameters.Add("rtn", SqlDbType.Int);
			sda.SelectCommand.Parameters.Add("ExMsg", SqlDbType.NVarChar, -1);

			sda.SelectCommand.Parameters.Add("Imei", SqlDbType.NVarChar, 50);

			sda.SelectCommand.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
			sda.SelectCommand.Parameters["ExMsg"].Direction = ParameterDirection.InputOutput;

			sda.SelectCommand.Parameters["Imei"].Value = Imei;

			sda.Fill(ds);

			if (!(1.Equals(sda.SelectCommand.Parameters["rtn"].Value)))
			{
				throw new Exception("数据库内部错误");
			}
			return ds;
		}


		[WebMethod]
		public DataSet PV_Imei_LogType(string Imei, int LogType)
		{
			DataSet ds = new DataSet();

			SqlDataAdapter sda = new SqlDataAdapter("dbo.Wb_PV_LogType_List", ConfigurationManager.ConnectionStrings["znlyDW"].ConnectionString);
			sda.SelectCommand.CommandType = CommandType.StoredProcedure;
			sda.SelectCommand.Parameters.Add("rtn", SqlDbType.Int);
			sda.SelectCommand.Parameters.Add("ExMsg", SqlDbType.NVarChar, -1);

			sda.SelectCommand.Parameters.Add("Imei", SqlDbType.NVarChar, 50);
			sda.SelectCommand.Parameters.Add("LogType", SqlDbType.Int);

			sda.SelectCommand.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
			sda.SelectCommand.Parameters["ExMsg"].Direction = ParameterDirection.InputOutput;

			sda.SelectCommand.Parameters["Imei"].Value = Imei;
			sda.SelectCommand.Parameters["LogType"].Value = LogType;

			sda.Fill(ds);

			if (!(1.Equals(sda.SelectCommand.Parameters["rtn"].Value)))
			{
				throw new Exception("数据库内部错误");
			}
			return ds;
		}

	}
}
