using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace pdbp.App_Code
{
	public class LoginFromCookiePage : System.Web.UI.Page
	{
		protected Apq.Web.SessionState.HttpSessionState ApqSession;

		protected override void OnPreInit(EventArgs e)
		{
			base.OnPreInit(e);

			ApqSession = new Apq.Web.SessionState.HttpSessionState(Session);
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			int UserSrc = Request.Cookies[ConfigurationManager.AppSettings["Cookie-UserSrc"]] == null ?
				0 : Apq.Convert.ChangeType<int>(Request.Cookies[ConfigurationManager.AppSettings["Cookie-UserSrc"]].Value);
			string LoginName = Request.Cookies[ConfigurationManager.AppSettings["Cookie-LoginName"]] == null ?
				string.Empty : Request.Cookies[ConfigurationManager.AppSettings["Cookie-LoginName"]].Value;
			string LoginPwd = Request.Cookies[ConfigurationManager.AppSettings["Cookie-LoginPwd"]] == null ?
				string.Empty : Request.Cookies[ConfigurationManager.AppSettings["Cookie-LoginPwd"]].Value;

			if (ApqSession.UserID == 0)
			{
				if (UserSrc == 1 && LoginName.Length > 1 && LoginPwd.Length > 2)
				{
					try
					{
						pdbp.WS.Login ws = new pdbp.WS.Login();
						//LoginName = Apq.Web.ExtJS.state.DecodeValue(LoginName)[1];
						//LoginPwd = Apq.Web.ExtJS.state.DecodeValue(LoginPwd)[1];
						pdbp.WS.STReturn stReturn = ws.Login_LoginNameFromCookie(LoginName, LoginPwd);
						if (stReturn.NReturn == 1)
						{
							// 记录登录状态
							ApqSession.UserID = Apq.Convert.ChangeType<long>(stReturn.POuts[0]);
							ApqSession.NickName = Apq.Convert.ChangeType<string>(stReturn.POuts[2]);
							ApqSession.LoginName = LoginName;
							ApqSession.LoginTime = DateTime.Now;
						}
					}
					catch { }
				}
			}
		}
	}
}
