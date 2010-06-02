using System;
using System.Collections.Generic;
using System.Web;
using Apq.Web.AJAX;

namespace dtxc
{
	public class CheckPwdExpirePage : CheckLoginPage
	{
		/// <summary>
		/// LoginFromCookiePage
		/// </summary>
		public CheckPwdExpirePage()
		{
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			// 非管理员提示后跳转到会员首页
			STReturn stReturn = new STReturn();
			if (!CheckPwdExpire(ref stReturn, ApqSession))
			{
				ClientScript.RegisterStartupScript(this.GetType(), "dtxc_CheckAdminPage", @"
alert(""密码已过期,请修改密码"");
top.location = ""../ifLoginNameInfo.aspx""
", true);
			}
		}
	}
}
