using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace pdbp
{
	public partial class Logout : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Session.Clear();

			// 写入客户端Cookie
			if (HttpContext.Current.Response.Cookies.AllKeys.Contains(ConfigurationManager.AppSettings["Cookie-UserSrc"])) HttpContext.Current.Response.Cookies.Remove(ConfigurationManager.AppSettings["Cookie-UserSrc"]);
			if (HttpContext.Current.Response.Cookies.AllKeys.Contains(ConfigurationManager.AppSettings["Cookie-LoginName"])) HttpContext.Current.Response.Cookies.Remove(ConfigurationManager.AppSettings["Cookie-LoginName"]);
			if (HttpContext.Current.Response.Cookies.AllKeys.Contains(ConfigurationManager.AppSettings["Cookie-LoginPwd"])) HttpContext.Current.Response.Cookies.Remove(ConfigurationManager.AppSettings["Cookie-LoginPwd"]);
			HttpCookie cookieUserSrc = new HttpCookie(ConfigurationManager.AppSettings["Cookie-UserSrc"], "0");
			HttpCookie cookieLoginName = new HttpCookie(ConfigurationManager.AppSettings["Cookie-LoginName"], string.Empty);
			HttpCookie cookieLoginPwd = new HttpCookie(ConfigurationManager.AppSettings["Cookie-LoginPwd"], string.Empty);
			cookieUserSrc.Expires = cookieLoginName.Expires = cookieLoginPwd.Expires = DateTime.Now;
			HttpContext.Current.Response.Cookies.Add(cookieUserSrc);
			HttpContext.Current.Response.Cookies.Add(cookieLoginName);
			HttpContext.Current.Response.Cookies.Add(cookieLoginPwd);
		}
	}
}
