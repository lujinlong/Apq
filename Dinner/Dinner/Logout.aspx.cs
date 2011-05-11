using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dinner
{
	/// <summary>
	/// QueryString:
	/// p:退出后转到的页面
	/// </summary>
	public partial class Logout : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Response.Cookies.Add(new HttpCookie("ys-LoginName", "null"));
			Response.Cookies.Add(new HttpCookie("ys-SqlLoginPwd", "null"));
			Response.Cookies["ys-LoginName"].Expires = DateTime.Now;
			Response.Cookies["ys-SqlLoginPwd"].Expires = DateTime.Now;

			Session.Abandon();

			string strPage = Request.QueryString["p"];
			if (string.IsNullOrEmpty(strPage))
			{
				strPage = "Default.aspx";
			}
			string strJS = string.Format("top.location = '{0}';",strPage);
			ClientScript.RegisterStartupScript(this.GetType(), "scDinner_LogoutPage", strJS, true);
		}
	}
}
