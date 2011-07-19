using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace pdbp
{
	public partial class VerifyGif : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			// 创建验证码图片并记录验证码到Session
			string strVerifyCode = Apq.String.Random(4, true, null, Apq.String.SimpleChars.ToCharArray());
			Session["VerifyCode"] = strVerifyCode;
			byte[] gifVerifyCode = Apq.Drawing.GraphicsHelper.CreateVerifyGif(strVerifyCode, 80, 20);
			Response.Clear();
			Response.ContentType = "image/gif";
			Response.BinaryWrite(gifVerifyCode);
		}
	}
}
