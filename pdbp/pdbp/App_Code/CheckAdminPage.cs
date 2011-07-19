using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pdbp.App_Code
{
	public class CheckAdminPage : CheckLoginPage
	{
		/// <summary>
		/// LoginFromCookiePage
		/// </summary>
		public CheckAdminPage()
		{
		}

		protected override void OnPreInit(EventArgs e)
		{
			base.OnPreInit(e);

			// 非管理员提示后跳转到后台登录页面
			Apq.STReturn stReturn = new Apq.STReturn();
			if (!CheckAdmin(ref stReturn, ApqSession))
			{
				string strUrlLogin = VirtualPathUtility.ToAbsolute("~/Login.aspx", Request.ApplicationPath);
				ClientScript.RegisterStartupScript(this.GetType(), "scGlobal_CheckAdminPage", string.Format(@"
alert(""{0}"");
top.location = ""{1}"";
", stReturn.ExMsg, strUrlLogin), true);
			}
		}

		/// <summary>
		/// 检测是否管理员
		/// </summary>
		/// <param name="stReturn"></param>
		public static bool CheckAdmin(ref Apq.STReturn stReturn, Apq.Web.SessionState.HttpSessionState ApqSession)
		{
			if (!ApqSession.IsAdmin)
			{
				stReturn.NReturn = -1;
				stReturn.ExMsg = "只有管理员才能访问该页面";
				return false;
			}
			return true;
		}
	}
}
