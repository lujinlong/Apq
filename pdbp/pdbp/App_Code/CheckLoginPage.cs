using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace pdbp.App_Code
{
	public class CheckLoginPage : LoginFromCookiePage
	{
		protected DataSet ds = new DataSet();

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			// 未登录
			Apq.STReturn stReturn = new Apq.STReturn();
			if (!CheckLogin(ref stReturn, ApqSession))
			{
				string strUrlLogin = VirtualPathUtility.ToAbsolute("~/Login.aspx", Request.ApplicationPath);
				ClientScript.RegisterStartupScript(this.GetType(), "scGlobal_CheckLoginPage", string.Format(@"
alert(""{0}"");
top.location = ""{1}"";
", stReturn.ExMsg, strUrlLogin), true);
			}
		}

		/// <summary>
		/// 检测是否登录
		/// </summary>
		/// <param name="stReturn"></param>
		public static bool CheckLogin(ref Apq.STReturn stReturn, Apq.Web.SessionState.HttpSessionState ApqSession)
		{
			if (ApqSession.UserID == 0)
			{
				stReturn.NReturn = -1;
				stReturn.ExMsg = "请登录";
				return false;
			}
			return true;
		}

		/// <summary>
		/// 检测密码过期
		/// </summary>
		/// <param name="stReturn"></param>
		[Obsolete("未完成")]
		public static bool CheckPwdExpire(ref Apq.STReturn stReturn, Apq.Web.SessionState.HttpSessionState ApqSession)
		{
			/*
			if (Convert.ToDateTime(ApqSession.User.Rows[0]["LoginPwdExpire"]) > DateTime.Now)
			{
				stReturn.NReturn = -1
				stReturn.ExMsg = "密码已过期,请修改密码";
				return false;
			}
			 */
			return true;
		}
	}
}
