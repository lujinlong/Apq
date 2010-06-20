using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apq.Privilege
{
	/// <summary>
	/// 权限列表辅助类
	/// </summary>
	public class UserP
	{
		/// <summary>
		/// 从表中的权限数据生成权限串
		/// </summary>
		/// <param name="dt">至少含PID,IsDeny两列</param>
		/// <returns></returns>
		public static string GetUserP(System.Data.DataTable dt)
		{
			List<string> lst = new List<string>(dt.Rows.Count);
			foreach (System.Data.DataRow dr in dt.Rows)
			{
				lst.Add(string.Format("{0},{1}", dr["PID"], dr["IsDeny"]));
			}
			return string.Join(";", lst.ToArray());
		}
	}
}
