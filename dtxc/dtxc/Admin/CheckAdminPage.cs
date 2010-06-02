using System;
using System.Collections.Generic;
using System.Web;


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

		protected override void OnPreInit(EventArgs e)
		{
			base.OnPreInit(e);

			// 非管理员提示后跳转到会员首页
			Apq.STReturn stReturn = new Apq.STReturn();
			if (!CheckAdmin(ref stReturn, ApqSession))
			{
				LoginDirectMsg = "您不是管理员,请访问会员页面";
				urlLogin = "../User/Main.aspx";
			}
		}
	}
}
