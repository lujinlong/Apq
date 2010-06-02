using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Apq.Web.AJAX;

namespace dtxc.Admin
{
	public partial class ifTaskList : CheckAdminPage
	{
		/// <summary>
		/// 自有QueryString参数说明:
		///		Status:{0:普通,1:已审核,2:已结算,3:已作废,10000:已删除}
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, EventArgs e)
		{
			// 参数解析
			int Pager_PageSize = Apq.Convert.ChangeType<int>(Request.QueryString["ps"], 20);
			int Pager_Page = Apq.Convert.ChangeType<int>(Request.QueryString["p"], 1);
			int[] Status = { 0, 1, 2, 3 };
			if (Request.QueryString["Status"] != null && Request.QueryString["Status"].Length > 0)
			{
				string[] aryStatus = Request.QueryString["Status"].Split(',');
				Status = new int[aryStatus.Length];
				for (int i = 0; i < Status.Length; i++)
				{
					Status[i] = Apq.Convert.ChangeType<int>(aryStatus[i], 1);
				}
			}

			// 设置分页相关值
			txtPager_PageSize.Text = Pager_PageSize.ToString();
			txtPager_Page.Text = Pager_Page.ToString();

			WS.Admin.WS1 ws = new dtxc.WS.Admin.WS1();
			STReturn stReturn = ws.TaskList(Pager_Page, Pager_PageSize, Status);
			if (stReturn.NReturn == 1)
			{
				rpt.DataSource = stReturn.FNReturn;

				//绑定对象

				rpt.DataBind();
				// 设置当前页数
				txtPager_Page.Text = stReturn.POuts[0].ToString();
				txtPager_PageCount.InnerText = Math.Ceiling(Convert.ToDouble(stReturn.POuts[1]) / Pager_PageSize).ToString();
			}
		}
	}
}
