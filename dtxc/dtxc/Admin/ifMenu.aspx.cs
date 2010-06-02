using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dtxc.Admin
{
	public partial class ifMenu : CheckAdminPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				spanName.InnerText = ApqSession.User.Rows[0]["Name"].ToString();
			}
			catch { }
		}
	}
}
