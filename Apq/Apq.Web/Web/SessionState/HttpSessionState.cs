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

		#region User
		/// <summary>
		/// 在 Session 中获取或设置已登录用户表
		/// </summary>
		public System.Data.DataTable User
		{
			get { return Session["Apq_User.User"] as System.Data.DataTable; }
			set { Session["Apq_User.User"] = value; }
		}

		/// <summary>
		/// 获取 UserID
		/// </summary>
		public long UserID
		{
			get
			{
				if (User != null && User.Rows.Count > 0)
				{
					return System.Convert.ToInt64(User.Rows[0]["UserID"]);
				}
				return -1;
			}
		}
		#endregion

		/// <summary>
		/// 在 Session 中获取或设置登录用户名
		/// </summary>
		public string LoginName
		{
			get
			{
				return Session["Apq_User.LoginName"] != null ? Session["Apq_User.LoginName"].ToString() : string.Empty;
			}
			set { Session["Apq_User.LoginName"] = value; }
		}

		/// <summary>
		/// 在 Session 中获取或设置登录时间
		/// </summary>
		public DateTime LoginTime
		{
			get
			{
				DateTime dt = DateTime.Now.AddYears(1);
				if (Session["Apq_User.LoginTime"] != null)
				{
					dt = System.Convert.ToDateTime(Session["Apq_User.LoginTime"]);
				}
				return dt;
			}
			set { Session["Apq_User.LoginTime"] = value; }
		}
	}
}
