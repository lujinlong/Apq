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
			Session.Abandon();
		}
	}
}
