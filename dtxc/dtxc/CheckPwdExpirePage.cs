using System;
using System.Collections.Generic;
using System.Web;


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

		protected override void OnPreInit(EventArgs e)
		{
			base.OnPreInit(e);

			// 密码过期
			Apq.STReturn stReturn = new Apq.STReturn();
			if (!CheckPwdExpire(ref stReturn, ApqSession))
			{
				LoginDirectMsg = "密码已过期,请修改密码";
				urlLogin = "Main.aspx?NeedChgPwd=1";
			}
		}
	}
}
