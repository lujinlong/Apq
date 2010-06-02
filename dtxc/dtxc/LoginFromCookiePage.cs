using System;
using System.Collections.Generic;
using System.Web;


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

		protected override void OnPreInit(EventArgs e)
		{
			base.OnPreInit(e);

			ApqSession = new Apq.Web.SessionState.HttpSessionState(Session);
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			string LoginName = Request.Cookies["ys-LoginName"] == null ? string.Empty : Request.Cookies["ys-LoginName"].Value;
			string SqlLoginPwd = Request.Cookies["ys-SqlLoginPwd"] == null ? string.Empty : Request.Cookies["ys-SqlLoginPwd"].Value;

			if (ApqSession.User == null || ApqSession.User.Rows.Count == 0)
			{
				if (LoginName.Length > 1 && SqlLoginPwd.Length > 2)
				{
					try
					{
						dtxc.WS.WS2 ws = new dtxc.WS.WS2();
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
