using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Apq.Web.SessionState;

namespace Dinner.User
{
	public partial class tpChgPwd : CheckLoginPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				txtEmName.Text = ApqSession.Employee.EmName;
				txtLoginName.Text = ApqSession.ApqLogin.LoginName;
			}
			catch { }
		}
	}
}
