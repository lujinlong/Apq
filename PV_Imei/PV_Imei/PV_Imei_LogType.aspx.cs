using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace PV_Imei
{
	public partial class PV_Imei_LogType : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string Imei = Request.QueryString["Imei"] ?? "019d3110a2145081";

			ws ws = new ws();
			DataSet ds = ws.PV_Imei_LogType(Imei, 1);
		}

		protected void btnQuery_Click(object sender, EventArgs e)
		{
			string Imei = txtImei.Text;
			int LogType = Convert.ToInt32(txtLogType.Text);

			DateTime FirstTime = new DateTime(9999, 12, 31);
			string FirstPlatform = "未知";
			string FirstSMSC = "未知";
			string FirstProvince = "未知";
			DateTime LastTime = new DateTime(9999, 12, 31);
			string LastPlatform = "未知";
			string LastSMSC = "未知";
			string LastProvince = "未知";
			int VisitCountTotal = 0;
			int VisitCountWeek = 0;
			int VisitCountDWeek = 0;
			int VisitCountMonth = 0;
			int VisitCountNMonth = 0;

			ws ws = new ws();
			DataSet ds = ws.PV_Imei_LogType(Imei, LogType);
			DataRow dr = ds.Tables[0].Rows[0];

			FirstTime = dr["FirstTime"] == null ? new DateTime(9999, 12, 31) : Convert.ToDateTime(dr["FirstTime"]);
			FirstPlatform = dr["FirstPlatform"] == null ? "未知" : dr["FirstPlatform"].ToString();
			FirstSMSC = dr["FirstSMSC"] == null ? "未知" : dr["FirstSMSC"].ToString();
			FirstProvince = dr["FirstProvince"] == null ? "未知" : dr["FirstProvince"].ToString();
			LastTime = dr["LastTime"] == null ? new DateTime(9999, 12, 31) : Convert.ToDateTime(dr["LastTime"]);
			LastPlatform = dr["LastPlatform"] == null ? "未知" : dr["LastPlatform"].ToString();
			LastSMSC = dr["LastSMSC"] == null ? "未知" : dr["LastSMSC"].ToString();
			LastProvince = dr["LastProvince"] == null ? "未知" : dr["LastProvince"].ToString();
			VisitCountTotal = dr["VisitCountTotal"] == null ? 0 : Convert.ToInt32(dr["VisitCountTotal"]);
			VisitCountWeek = dr["VisitCountWeek"] == null ? 0 : Convert.ToInt32(dr["VisitCountWeek"]);
			VisitCountDWeek = dr["VisitCountDWeek"] == null ? 0 : Convert.ToInt32(dr["VisitCountDWeek"]);
			VisitCountMonth = dr["VisitCountMonth"] == null ? 0 : Convert.ToInt32(dr["VisitCountMonth"]);
			VisitCountNMonth = dr["VisitCountNMonth"] == null ? 0 : Convert.ToInt32(dr["VisitCountNMonth"]);

			txtOut.Text = FirstTime.ToString("yyyy-MM-dd HH:mm:ss") + "," + FirstPlatform + "," + FirstSMSC + "," + FirstProvince + ","
				+ LastTime.ToString("yyyy-MM-dd HH:mm:ss") + "," + LastPlatform + "," + LastSMSC + "," + LastProvince + ","
				+ VisitCountTotal + "," + VisitCountWeek + "," + VisitCountDWeek + "," + VisitCountMonth + "," + VisitCountNMonth;
		}
	}
}
