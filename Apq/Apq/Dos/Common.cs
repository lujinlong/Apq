using System;
using System.Collections.Generic;
using System.Text;

namespace Apq.Dos
{
	/// <summary>
	/// 公用模块
	/// </summary>
	public static class Common
	{
		/// <summary>
		/// 去除参数中的引号并在两端添加引号
		/// </summary>
		/// <param name="Param">参数值</param>
		/// <returns></returns>
		public static string EncodeParam(string Param)
		{
			string str = Param.Replace("\"", "");
			return "\"" + str + "\"";
		}
	}
}
