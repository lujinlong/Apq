using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dtxc
{
	public partial class Logout : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Response.Cookies.Add(new HttpCookie("ys-LoginName", "null"));
			Response.Cookies.Add(new HttpCookie("ys-SqlLoginPwd", "null"));
			Response.Cookies["ys-LoginName"].Expires = DateTime.Now;
			Response.Cookies["ys-SqlLoginPwd"].Expires = DateTime.Now;

			Session.Abandon();

			Response.Redirect("Login.aspx");
		}
	}
}
