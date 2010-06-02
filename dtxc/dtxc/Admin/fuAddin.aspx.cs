using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dtxc.Admin
{
	public partial class fuAddin : CheckAdminPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (IsPostBack)
			{
				// 保存插件到插件目录
				string path = Server.MapPath("~/Down/Addin");
				if (tpAddinUp_fuAddin.HasFile)
				{
					string fileExtension = System.IO.Path.GetExtension(tpAddinUp_fuAddin.FileName).ToLower();
					if (fileExtension != ".dll")
					{
						throw new Exception("非法文件类型");
					}
					tpAddinUp_fuAddin.PostedFile.SaveAs(path + tpAddinUp_fuAddin.FileName);
				}
			}
		}
	}
}
