using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PV_Imei
{
	public partial class test : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

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
			ws.PV_Imei_LogType(Imei, LogType
				, ref FirstTime, ref FirstPlatform, ref FirstSMSC, ref FirstProvince
				, ref LastTime, ref LastPlatform, ref LastSMSC, ref LastProvince
				, ref VisitCountTotal, ref VisitCountWeek, ref VisitCountDWeek, ref VisitCountMonth, ref VisitCountNMonth);

			txtOut.Text = FirstTime.ToString("yyyy-MM-dd HH:mm:ss") + "," + FirstPlatform + "," + FirstSMSC + "," + FirstProvince + ","
				+ LastTime.ToString("yyyy-MM-dd HH:mm:ss") + "," + LastPlatform + "," + LastSMSC + "," + LastProvince + ","
				+ VisitCountTotal + "," + VisitCountWeek + "," + VisitCountDWeek + "," + VisitCountMonth + "," + VisitCountNMonth;
		}
	}
}
