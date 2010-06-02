using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dtxc.Ext1
{
	public partial class Main : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				spanUserName.InnerText = ApqSession.User.Rows[0]["Name"].ToString();
			}
			catch { }
		}

		protected Apq.Web.SessionState.HttpSessionState ApqSession = null;
	}
}
