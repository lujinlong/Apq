using System;
using System.Collections.Generic;
using System.Web;
using System.Data;


namespace dtxc
{
	public class CheckLoginPage : LoginFromCookiePage
	{
		/// <summary>
		/// LoginFromCookiePage
		/// </summary>
		public CheckLoginPage()
		{
		}

		protected DataSet ds = new DataSet();

		protected string LoginDirectMsg = "请登录";
		protected string urlLogin = "../Login.aspx";

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			// 未登录
			Apq.STReturn stReturn = new Apq.STReturn();
			if (!CheckLogin(ref stReturn, ApqSession))
			{
				LoginDirectMsg = "请登录";
				urlLogin = "../Login.aspx";
				ClientScript.RegisterStartupScript(this.GetType(), "scdtxc_CheckLoginPage", string.Format(@"
alert(""{0}"");
top.location = ""{1}"";
", LoginDirectMsg, urlLogin), true);
			}
		}

		#region 登录检测
		/// <summary>
		/// 检测是否登录
		/// </summary>
		/// <param name="stReturn"></param>
		public static bool CheckLogin(ref Apq.STReturn stReturn, Apq.Web.SessionState.HttpSessionState ApqSession)
		{
			if (ApqSession.User == null || ApqSession.User.Rows.Count == 0)
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
		public static bool CheckPwdExpire(ref Apq.STReturn stReturn, Apq.Web.SessionState.HttpSessionState ApqSession)
		{
			if (!CheckLogin(ref stReturn, ApqSession))
			{
				return false;
			}

			/*
			if (Convert.ToDateTime(ApqSession.User.Rows[0]["LoginPwdExpire"]) > DateTime.Now)
			{
				stReturn.NReturn = 2;
				stReturn.ExMsg = "密码已过期,请修改密码";
				return false;
			}
			 */
			return true;
		}

		/// <summary>
		/// 检测是否管理员
		/// </summary>
		/// <param name="stReturn"></param>
		public static bool CheckAdmin(ref Apq.STReturn stReturn, Apq.Web.SessionState.HttpSessionState ApqSession)
		{
			if (!CheckPwdExpire(ref stReturn, ApqSession))
			{
				return false;
			}

			if (!System.Convert.ToBoolean(ApqSession.User.Rows[0]["IsAdmin"]))
			{
				stReturn.NReturn = -1;
				stReturn.ExMsg = "非管理员不能使用该功能";
				return false;
			}
			return true;
		}
		#endregion
	}
}
