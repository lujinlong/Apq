﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Apq.Web.AJAX;

namespace dtxc.Admin
{
	public partial class ifPayoutList : CheckAdminPage
	{
		/// <summary>
		/// 自有QueryString参数说明:无
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, EventArgs e)
		{
			// 参数解析
			int Pager_PageSize = Apq.Convert.ChangeType<int>(Request.QueryString["ps"], 20);
			int Pager_Page = Apq.Convert.ChangeType<int>(Request.QueryString["p"], 1);
			long UserID = Apq.Convert.ChangeType<long>(Request.QueryString["UserID"], 0);

			// 设置分页相关值
			txtPager_PageSize.Text = Pager_PageSize.ToString();
			txtPager_Page.Text = Pager_Page.ToString();

			WS.Admin.WS1 ws = new dtxc.WS.Admin.WS1();
			STReturn stReturn = ws.PayoutList(Pager_Page, Pager_PageSize, UserID, 0);
			if (stReturn.NReturn == 1)
			{
				rpt.DataSource = stReturn.FNReturn;

				//绑定对象

				rpt.DataBind();
				// 设置当前页数
				txtPager_Page.Text = stReturn.POuts[0].ToString();
				txtPager_PageCount.InnerText = Math.Ceiling(Convert.ToDouble(stReturn.POuts[1]) / Pager_PageSize).ToString();
			}

			//绑定对象
		}
	}
}
