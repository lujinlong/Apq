using System;
using System.Collections.Generic;
using System.Web;
using System.Data;


namespace Dinner
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


		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			// 登录检测
			Apq.STReturn stReturn = CheckLogin();
			if (stReturn.NReturn != 1)
			{
				string strJS = "Ext.Msg.alert(\"错误\",\"" + stReturn.ExMsg + "\",function(){ top.location = \"" + stReturn.POuts[0] + "\"; });";
				ClientScript.RegisterStartupScript(this.GetType(), "scDinner_CheckLoginPage", strJS, true);
			}
		}

		protected virtual Apq.STReturn CheckLogin()
		{
			// 登录检测
			Apq.STReturn stReturn = new Apq.STReturn();
			CheckLogin(ref stReturn, ApqSession);
			return stReturn;
		}

		#region 登录检测静态方法
		/// <summary>
		/// 检测是否登录
		/// </summary>
		/// <param name="stReturn"></param>
		public static bool CheckLogin(ref Apq.STReturn stReturn, DinnerSession ApqSession)
		{
			if (ApqSession.Employee.EmID == 0)
			{
				stReturn.NReturn = -1;
				stReturn.ExMsg = "请登录";
				stReturn.POuts.Add("Login.aspx");
				return false;
			}

			stReturn.NReturn = 1;
			return true;
		}

		/// <summary>
		/// 检测密码过期
		/// </summary>
		/// <param name="stReturn"></param>
		public static bool CheckPwdExpire(ref Apq.STReturn stReturn, DinnerSession ApqSession)
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
				stReturn.POuts.Add("../User/main.aspx?tp=ChgPwd");
				return false;
			}
			 */

			stReturn.NReturn = 1;
			return true;
		}

		/// <summary>
		/// 检测是否管理员
		/// </summary>
		/// <param name="stReturn"></param>
		public static bool CheckAdmin(ref Apq.STReturn stReturn, DinnerSession ApqSession)
		{
			if (!CheckPwdExpire(ref stReturn, ApqSession))
			{
				return false;
			}

			if (!ApqSession.Employee.IsAdmin)
			{
				stReturn.NReturn = -1;
				stReturn.ExMsg = "非管理员不能使用该功能";
				stReturn.POuts.Add("../User/main.aspx");
				return false;
			}

			stReturn.NReturn = 1;
			return true;
		}
		#endregion
	}
}
