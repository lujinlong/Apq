using System;
using System.Collections.Generic;
using System.Text;

namespace Apq.Text
{
	/// <summary>
	/// StringBuilder
	/// </summary>
	public partial class StringBuilder
	{
		/// <summary>
		/// 在指定 StringBuilder 后添加连续字符串
		/// </summary>
		/// <param name="sb">StringBuilder</param>
		/// <param name="ary">需要添加的对象(调用其 ToString() 方法)</param>
		public static void Append(System.Text.StringBuilder sb, params object[] ary)
		{
			foreach (object obj in ary)
			{
				sb.Append(obj.ToString());
			}
		}
	}
}
