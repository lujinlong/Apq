using System;
using System.Collections.Generic;
using System.Text;

namespace Apq.Web.SessionState
{
	/// <summary>
	/// HttpSessionState 统一管理
	/// </summary>
	public class HttpSessionState
	{
		/// <summary>
		/// 
		/// </summary>
		public HttpSessionState(System.Web.SessionState.HttpSessionState Session)
		{
			_Session = Session;
		}

		private System.Web.SessionState.HttpSessionState _Session;
		/// <summary>
		/// 获取/*或设置*/ Session
		/// </summary>
		public System.Web.SessionState.HttpSessionState Session
		{
			get { return _Session; }
			//set { _Session = value; }
		}

		/// <summary>
		/// 在 Session 中获取登录信息
		/// </summary>
		public ApqLogin ApqLogin
		{
			get
			{
				ApqLogin ApqLogin = Session["ApqLogin"] as ApqLogin;
				if (ApqLogin == null)
				{
					Session["ApqLogin"] = ApqLogin = new ApqLogin();
				}
				return ApqLogin;
			}
		}
	}

	/// <summary>
	/// 登录信息
	/// </summary>
	public class ApqLogin
	{
		/// <summary>
		/// LoginID
		/// </summary>
		public long LoginID;
		/// <summary>
		/// 登录名
		/// </summary>
		public string LoginName;
		/// <summary>
		/// 登录密码
		/// </summary>
		public byte[] LoginPwd;
		/// <summary>
		/// 密码过期时间
		/// </summary>
		public DateTime PwdExpire;
		/// <summary>
		/// 密码状态
		/// </summary>
		public int PwdStatus;
		/// <summary>
		/// 注册时间
		/// </summary>
		public DateTime RegTime;
		/// <summary>
		/// 登录时间
		/// </summary>
		public DateTime LoginTime;
	}
}
