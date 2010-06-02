using System;
using System.Collections.Generic;
using System.Web;
using Apq.Web.AJAX;

namespace dtxc
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

		protected Apq.Web.SessionState.HttpSessionState ApqSession;

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			ApqSession = new Apq.Web.SessionState.HttpSessionState(Session);
			string UserName = Request.Cookies["UserName"] == null ? string.Empty : Request.Cookies["UserName"].Value;
			string SqlLoginPwd = Request.Cookies["SqlLoginPwd"] == null ? string.Empty : Request.Cookies["SqlLoginPwd"].Value;

			if (ApqSession.User == null || ApqSession.User.Rows.Count == 0)
			{
				if (UserName.Length > 1 && SqlLoginPwd.Length > 2)
				{
					dtxc.WS.WS2 ws = new dtxc.WS.WS2();
					ws.Login_UserNameFromCookie(UserName, SqlLoginPwd);
				}
			}
		}
	}
}
