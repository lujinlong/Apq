using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dinner
{
	public class DinnerSession : Apq.Web.SessionState.HttpSessionState
	{
		public DinnerSession(System.Web.SessionState.HttpSessionState HttpSession)
			: base(HttpSession)
		{
		}

		/// <summary>
		/// 在 Session 中获取员工信息
		/// </summary>
		public BLL.Employee Employee
		{
			get
			{
				BLL.Employee _Employee = Session["Employee"] as BLL.Employee;
				if (_Employee == null)
				{
					Session["Employee"] = _Employee = new Dinner.BLL.Employee();
				}
				return _Employee;
			}
		}
	}
}
