using System;
using System.Collections.Generic;
using System.Web;


namespace Dinner
{
	/// <summary>
	/// 自动从 Cookie 登录
	/// </summary>
	public class LoginFromCookiePage : System.Web.UI.Page
	{
		/// <summary>
		/// LoginFromCookiePage
		/// </summary>
		public LoginFromCookiePage()
		{
		}

		protected DinnerSession ApqSession;

		protected override void OnPreInit(EventArgs e)
		{
			base.OnPreInit(e);

			ApqSession = new DinnerSession(Session);
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			// 未登录时自动登录
			if (ApqSession.Employee.EmID <= 0)
			{
				string LoginName = Request.Cookies["ys-LoginName"] == null ? string.Empty : Request.Cookies["ys-LoginName"].Value;
				string SqlLoginPwd = Request.Cookies["ys-SqlLoginPwd"] == null ? string.Empty : Request.Cookies["ys-SqlLoginPwd"].Value;

				if (LoginName.Length > 1 && SqlLoginPwd.Length > 2)
				{
					try
					{
						Dinner.WS.WS2 ws = new Dinner.WS.WS2();
						LoginName = Apq.Web.ExtJS.state.DecodeValue(LoginName)[1];
						SqlLoginPwd = Apq.Web.ExtJS.state.DecodeValue(SqlLoginPwd)[1];
						ws.Login_LoginNameFromCookie(LoginName, SqlLoginPwd);
					}
					catch { }
				}
			}
		}
	}
}
