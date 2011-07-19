using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using pdbp.CryptKey;
using System.Net;
using System.Web.Script.Services;
using System.Configuration;
using System.Web.Services;

namespace pdbp
{
	public partial class Reg : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			//txtIntroUserID.Text = Request.QueryString["IntroUserID"] ?? "0";
			//scReg.Services.Add(new ServiceReference(ConfigurationManager.AppSettings["WS_Login"]));
		}

		/// <summary>
		/// 注册
		/// </summary>
		/// <param name="LoginName">登录名</param>
		/// <param name="LoginPwd">密码</param>
		/// <param name="Introducer">介绍人</param>
		[WebMethod]
		public static Apq.STReturn Regist(string LoginName, string LoginPwd, string Introducer)
		{
			string CryptPwd = Apq.Security.Cryptography.DESHelper.EncryptString(LoginPwd, DES.Key, DES.IV);
			IPAddress ipa;
			IPAddress.TryParse(HttpContext.Current.Request.UserHostAddress, out ipa);

			pdbp.WS.Login Lg = new pdbp.WS.Login();
			pdbp.WS.STReturn stReturnWS = Lg.WS_Reg(LoginName, CryptPwd, ipa.GetAddressBytes(), Introducer);
			Apq.STReturn stReturn = new Apq.STReturn();
			stReturn.NReturn = stReturnWS.NReturn;
			stReturn.FNReturn = stReturnWS.FNReturn;
			stReturn.ExMsg = stReturnWS.ExMsg;
			stReturn.POuts.AddRange(stReturnWS.POuts);

			if (stReturn.NReturn == 1)
			{
				// 注册成功,自动登录
				wsLogin wsLogin = new wsLogin();
				wsLogin.Login_LoginName(LoginName, LoginPwd);
			}
			return stReturn;
		}
	}
}
