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
			Session["ApqUser"] = new ApqUser();
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
		/// 获取或设置 UserID
		/// </summary>
		public long UserID
		{
			get { return Apq.Convert.ChangeType<long>(Session["UserID"]); }
			set { Session["UserID"] = value; }
		}

		/// <summary>
		/// 在 Session 中获取或设置用户昵称
		/// </summary>
		public string NickName
		{
			get { return Apq.Convert.ChangeType<string>(Session["NickName"]); }
			set { Session["NickName"] = value; }
		}

		/// <summary>
		/// 在 Session 中获取或设置登录名
		/// </summary>
		public string LoginName
		{
			get { return Apq.Convert.ChangeType<string>(Session["LoginName"]); }
			set { Session["LoginName"] = value; }
		}

		/// <summary>
		/// 在 Session 中获取或设置登录时间
		/// </summary>
		public DateTime LoginTime
		{
			get { return Apq.Convert.ChangeType<DateTime>(Session["LoginTime"], DateTime.Now.AddYears(1)); }
			set { Session["Apq_User.LoginTime"] = value; }
		}

		/// <summary>
		/// 获取权限系统用户信息
		/// </summary>
		public ApqUser ApqUser
		{
			get { return Session["ApqUser"] as ApqUser; }
		}
	}

	/// <summary>
	/// 权限系统用户信息
	/// </summary>
	public class ApqUser
	{
		/// <summary>
		/// UserID
		/// </summary>
		public long UserID;
		/// <summary>
		/// UserSrc
		/// </summary>
		public int UserSrc;
	}
}
