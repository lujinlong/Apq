using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;

namespace pdbp
{
	/// <summary>
	/// wsLogin 的摘要说明
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
	[System.Web.Script.Services.ScriptService]
	public class wsLogin : System.Web.Services.WebService
	{

		[WebMethod]
		public string HelloWorld()
		{
			return "Hello World";
		}

		/// <summary>
		/// 获取验证码(4字符)
		/// </summary>
		[WebMethod(EnableSession = true)]
		public string GetVirifyCode()
		{
			return Session["VirifyCode"] == null ? string.Empty : Session["VirifyCode"].ToString();
		}

		/// <summary>
		/// 登录
		/// </summary>
		/// <param name="LoginName">登录名</param>
		/// <param name="LoginPwd">密码</param>
		[WebMethod(EnableSession = true)]
		public pdbp.WS.STReturn Login_LoginName(string LoginName, string LoginPwd)
		{
			pdbp.WS.Login Lg = new pdbp.WS.Login();
			pdbp.WS.STReturn stReturn = Lg.Login_LoginName(LoginName, LoginPwd);

			if (stReturn.NReturn == 1)
			{
				Apq.Web.SessionState.HttpSessionState ApqSession = new Apq.Web.SessionState.HttpSessionState(HttpContext.Current.Session);
				// 记录登录状态
				ApqSession.UserID = Apq.Convert.ChangeType<long>(stReturn.POuts[0]);
				ApqSession.NickName = Apq.Convert.ChangeType<string>(stReturn.POuts[2]);
				ApqSession.LoginName = LoginName;
				ApqSession.LoginTime = DateTime.Now;

				// 写入客户端Cookie
				if (HttpContext.Current.Response.Cookies.AllKeys.Contains(ConfigurationManager.AppSettings["Cookie-UserSrc"])) HttpContext.Current.Response.Cookies.Remove(ConfigurationManager.AppSettings["Cookie-UserSrc"]);
				if (HttpContext.Current.Response.Cookies.AllKeys.Contains(ConfigurationManager.AppSettings["Cookie-LoginName"])) HttpContext.Current.Response.Cookies.Remove(ConfigurationManager.AppSettings["Cookie-LoginName"]);
				if (HttpContext.Current.Response.Cookies.AllKeys.Contains(ConfigurationManager.AppSettings["Cookie-LoginPwd"])) HttpContext.Current.Response.Cookies.Remove(ConfigurationManager.AppSettings["Cookie-LoginPwd"]);
				HttpCookie cookieUserSrc = new HttpCookie(ConfigurationManager.AppSettings["Cookie-UserSrc"], "1");
				HttpCookie cookieLoginName = new HttpCookie(ConfigurationManager.AppSettings["Cookie-LoginName"], LoginName);
				HttpCookie cookieLoginPwd = new HttpCookie(ConfigurationManager.AppSettings["Cookie-LoginPwd"], Apq.Convert.ChangeType<string>(stReturn.POuts[1]));
				cookieUserSrc.Expires = cookieLoginName.Expires = cookieLoginPwd.Expires = DateTime.Now.AddYears(1);
				HttpContext.Current.Response.Cookies.Add(cookieUserSrc);
				HttpContext.Current.Response.Cookies.Add(cookieLoginName);
				HttpContext.Current.Response.Cookies.Add(cookieLoginPwd);
			}
			return stReturn;
		}
	}
}
