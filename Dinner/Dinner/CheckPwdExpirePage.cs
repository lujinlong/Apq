using System;
using System.Collections.Generic;
using System.Web;


namespace Dinner
{
	public class CheckPwdExpirePage : CheckLoginPage
	{
		/// <summary>
		/// LoginFromCookiePage
		/// </summary>
		public CheckPwdExpirePage()
		{
		}

		protected override Apq.STReturn  CheckLogin()
		{
			// 密码过期
			Apq.STReturn stReturn = new Apq.STReturn();
			CheckPwdExpire(ref stReturn, ApqSession);
			return stReturn;
		}
	}
}
