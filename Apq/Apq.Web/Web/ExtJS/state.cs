using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Apq.Web.ExtJS
{
	/// <summary>
	/// 提供对Ext.state的Helper
	/// </summary>
	public class state
	{
		/// <summary>
		/// 解析stateValue,最多返回两部分字符串
		/// </summary>
		/// <param name="val"></param>
		/// <returns></returns>
		public static string[] DecodeValue(string val)
		{
			val = HttpUtility.UrlDecode(val);
			return val.Split(new char[] { ':' }, 2);
		}
	}
}
