using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Apq.Web.AJAX;

namespace dtxc
{
	public partial class VoteLog : System.Web.UI.Page
	{
		/// <summary>
		/// 自有QueryString参数说明:无
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, EventArgs e)
		{
			// 参数解析
			string TaskName = Request.QueryString["TaskName"] ?? string.Empty;
			string UserNameBegin = Request.QueryString["UserNameBegin"] ?? string.Empty;

			// 设置分页相关值

			WS.WS2 ws = new dtxc.WS.WS2();
			STReturn stReturn = ws.dtxc_TaskVote_Log_List(TaskName, UserNameBegin);
			if (stReturn.NReturn == 1)
			{
				rpt.DataSource = stReturn.FNReturn;

				//绑定对象

				rpt.DataBind();
				// 设置当前页数
			}

			//绑定对象
		}
	}
}
