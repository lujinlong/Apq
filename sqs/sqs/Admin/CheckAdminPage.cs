using System;
using System.Collections.Generic;
using System.Web;
using Apq.Web.AJAX;

namespace dtxc.Admin
{
	public class CheckAdminPage : CheckPwdExpirePage
	{
		/// <summary>
		/// LoginFromCookiePage
		/// </summary>
		public CheckAdminPage()
		{
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			// 非管理员提示后跳转到会员首页
			STReturn stReturn = new STReturn();
			if (!CheckAdmin(ref stReturn, ApqSession))
			{
				ClientScript.RegisterStartupScript(this.GetType(), "dtxc_CheckAdminPage", @"
alert(""您不是管理员,请访问会员页面"");
top.location = ""../Main.htm""
", true);
			}
		}
	}
}
