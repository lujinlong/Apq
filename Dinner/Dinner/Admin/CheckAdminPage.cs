using System;
using System.Collections.Generic;
using System.Web;


namespace Dinner.Admin
{
	public class CheckAdminPage : CheckPwdExpirePage
	{
		/// <summary>
		/// LoginFromCookiePage
		/// </summary>
		public CheckAdminPage()
		{
		}

		protected override Apq.STReturn CheckLogin()
		{
			// 管理员检测
			Apq.STReturn stReturn = new Apq.STReturn();
			CheckAdmin(ref stReturn, ApqSession);
			return stReturn;
		}
	}
}
