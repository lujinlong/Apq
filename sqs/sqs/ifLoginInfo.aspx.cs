using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Apq.Web.AJAX;
using Apq.Web.SessionState;

namespace dtxc
{
	public partial class ifLoginInfo : CheckLoginPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			STReturn stReturn = new STReturn();
			HttpSessionState ApqSession = new HttpSessionState(Session);
			if (CheckLoginPage.CheckLogin(ref stReturn, ApqSession))
			{
				txtUserName.Text = ApqSession.User.Rows[0]["UserName"].ToString();
			}
			else
			{
				ClientScript.RegisterStartupScript(this.GetType(), "scdtxc_CheckLogin", @"
alert(""请登录"");
top.location = ""Login.aspx"";
", true);
			}
		}
	}
}
