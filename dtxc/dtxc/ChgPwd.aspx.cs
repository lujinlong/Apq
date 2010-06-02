using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Apq.Web.SessionState;

namespace dtxc
{
	public partial class ChgPwd : CheckLoginPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				txtUserName.Text = ApqSession.User.Rows[0]["UserName"].ToString();
			}
			catch { }
		}

		protected override void OnPreInit(EventArgs e)
		{
			base.OnPreInit(e);

			Apq.STReturn stReturn = new Apq.STReturn();
			if (!CheckLoginPage.CheckLogin(ref stReturn, ApqSession))
			{
				urlLogin = "Login.aspx";
			}
		}
	}
}
