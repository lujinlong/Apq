using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dtxc.User
{
	public partial class ifDefault : CheckLoginPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			spanNow.InnerText = DateTime.Now.ToLongTimeString();
		}
	}
}
