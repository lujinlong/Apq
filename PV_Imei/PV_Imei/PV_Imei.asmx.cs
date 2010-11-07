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
	public class PV_Imei : System.Web.Services.WebService
	{

		[WebMethod]
		public string HelloWorld()
		{
			return "Hello World";
		}

		[WebMethod]
		public void PV_Imei_LogType(string Imei, int LogType
			, ref DateTime FirstTime, ref int FirstSource, ref string FirstProvince
			, ref DateTime LastTime, ref int LastSource, ref string LastProvince
			, ref int VisitCountTotal, ref int VisitCountWeek, ref int VisitCountDWeek, ref int VisitCountMonth, ref int VisitCountNMonth)
		{
			DataSet ds = new DataSet();

			SqlDataAdapter sda = new SqlDataAdapter("dbo.Wb_PV_Get", ConfigurationManager.ConnectionStrings["znlyDW"].ConnectionString);
			sda.SelectCommand.CommandType = CommandType.StoredProcedure;
			sda.SelectCommand.Parameters.Add("rtn", SqlDbType.Int);
			sda.SelectCommand.Parameters.Add("ExMsg", SqlDbType.NVarChar, -1);

			sda.SelectCommand.Parameters.Add("Imei", SqlDbType.NVarChar, 50);
			sda.SelectCommand.Parameters.Add("LogType", SqlDbType.Int);

			sda.SelectCommand.Parameters.Add("FirstTime", SqlDbType.DateTime);
			sda.SelectCommand.Parameters.Add("FirstSource", SqlDbType.Int);
			sda.SelectCommand.Parameters.Add("FirstProvince", SqlDbType.NVarChar, 50);
			sda.SelectCommand.Parameters.Add("LastTime", SqlDbType.DateTime);
			sda.SelectCommand.Parameters.Add("LastSource", SqlDbType.Int);
			sda.SelectCommand.Parameters.Add("LastProvince", SqlDbType.NVarChar, 50);
			sda.SelectCommand.Parameters.Add("VisitCountTotal", SqlDbType.Int);
			sda.SelectCommand.Parameters.Add("VisitCountWeek", SqlDbType.Int);
			sda.SelectCommand.Parameters.Add("VisitCountDWeek", SqlDbType.Int);
			sda.SelectCommand.Parameters.Add("VisitCountMonth", SqlDbType.Int);
			sda.SelectCommand.Parameters.Add("VisitCountNMonth", SqlDbType.Int);

			sda.SelectCommand.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
			sda.SelectCommand.Parameters["ExMsg"].Direction = ParameterDirection.InputOutput;
			sda.SelectCommand.Parameters["FirstTime"].Direction = ParameterDirection.InputOutput;
			sda.SelectCommand.Parameters["FirstSource"].Direction = ParameterDirection.InputOutput;
			sda.SelectCommand.Parameters["FirstProvince"].Direction = ParameterDirection.InputOutput;
			sda.SelectCommand.Parameters["LastTime"].Direction = ParameterDirection.InputOutput;
			sda.SelectCommand.Parameters["LastSource"].Direction = ParameterDirection.InputOutput;
			sda.SelectCommand.Parameters["LastProvince"].Direction = ParameterDirection.InputOutput;
			sda.SelectCommand.Parameters["VisitCountTotal"].Direction = ParameterDirection.InputOutput;
			sda.SelectCommand.Parameters["VisitCountWeek"].Direction = ParameterDirection.InputOutput;
			sda.SelectCommand.Parameters["VisitCountDWeek"].Direction = ParameterDirection.InputOutput;
			sda.SelectCommand.Parameters["VisitCountMonth"].Direction = ParameterDirection.InputOutput;
			sda.SelectCommand.Parameters["VisitCountNMonth"].Direction = ParameterDirection.InputOutput;

			sda.SelectCommand.Parameters["Imei"].Value = Imei;
			sda.SelectCommand.Parameters["LogType"].Value = LogType;

			sda.Fill(ds);

			if (!(1.Equals(sda.SelectCommand.Parameters["rtn"].Value)))
			{
				throw new Exception("数据库内部错误");
			}

			FirstTime = sda.SelectCommand.Parameters["FirstTime"].Value == null ? new DateTime(9999, 12, 31) : Convert.ToDateTime(sda.SelectCommand.Parameters["FirstTime"].Value);
			FirstSource = sda.SelectCommand.Parameters["FirstSource"].Value == null ? 0 : Convert.ToInt32(sda.SelectCommand.Parameters["FirstSource"].Value);
			FirstProvince = sda.SelectCommand.Parameters["FirstProvince"].Value == null ? "未知" : sda.SelectCommand.Parameters["FirstProvince"].Value.ToString();
			LastTime = sda.SelectCommand.Parameters["LastTime"].Value == null ? new DateTime(9999, 12, 31) : Convert.ToDateTime(sda.SelectCommand.Parameters["LastTime"].Value);
			LastSource = sda.SelectCommand.Parameters["LastSource"].Value == null ? 0 : Convert.ToInt32(sda.SelectCommand.Parameters["LastSource"].Value);
			LastProvince = sda.SelectCommand.Parameters["LastProvince"].Value == null ? "未知" : sda.SelectCommand.Parameters["LastProvince"].Value.ToString();
			VisitCountTotal = sda.SelectCommand.Parameters["VisitCountTotal"].Value == null ? 0 : Convert.ToInt32(sda.SelectCommand.Parameters["VisitCountTotal"].Value);
			VisitCountWeek = sda.SelectCommand.Parameters["VisitCountWeek"].Value == null ? 0 : Convert.ToInt32(sda.SelectCommand.Parameters["VisitCountWeek"].Value);
			VisitCountDWeek = sda.SelectCommand.Parameters["VisitCountDWeek"].Value == null ? 0 : Convert.ToInt32(sda.SelectCommand.Parameters["VisitCountDWeek"].Value);
			VisitCountMonth = sda.SelectCommand.Parameters["VisitCountMonth"].Value == null ? 0 : Convert.ToInt32(sda.SelectCommand.Parameters["VisitCountMonth"].Value);
			VisitCountNMonth = sda.SelectCommand.Parameters["VisitCountNMonth"].Value == null ? 0 : Convert.ToInt32(sda.SelectCommand.Parameters["VisitCountNMonth"].Value);
		}
	}
}
