using System;
using System.Collections.Generic;
using System.Text;

namespace Apq.Shell
{
	/// <summary>
	/// 字符
	/// </summary>
	public class Char
	{
		/// <summary>
		/// 值
		/// </summary>
		public string Value = "";
		/// <summary>
		/// 类型
		/// </summary>
		public int Type;

		/// <summary>
		/// 获取指定位置的字符[若为转义符自动取下一位置]
		/// </summary>
		/// <param name="str"></param>
		/// <param name="state"></param>
		/// <param name="ESCAPE"></param>
		/// <returns></returns>
		public static Char GetChar(string str, State state, string ESCAPE)
		{
			if (state.Index >= str.Length)
			{
				return null;
			}
			Char c = new Char();
			c.Value = str[state.Index].ToString();
			if (c.Value == ESCAPE)
			{
				try
				{
					c.Value = Transform(str[++state.Index].ToString());
				}
				catch { }
				c.Type = 1;
			}
			else
			{
				c.Type = Apq.Collections.IList.Contains(Apq.Shell.Common.SpecialChars, c.Value) ? 2 : 1;
			}
			return c;
		}

		/// <summary>
		/// 可以在此增加转义字符的定义
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		public static string Transform(string c)
		{
			/* 例如:
			if( c == 'n' )
			{
				return '\n';
			}
			if( c == 'v' )
			{
				return '\v';
			}
			*/
			return c;
		}
	}
}
