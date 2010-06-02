using System;
using System.Collections.Generic;
using System.Text;

namespace Apq
{
	/// <summary>
	/// Apq.Char
	/// </summary>
	public class Char
	{
		/// <summary>
		/// 获取随机字符
		/// </summary>
		/// <param name="Seed">种子值[只需要第0个值,null表示使用默认种子值]</param>
		/// <param name="All">字符集</param>
		/// <returns></returns>
		public static char Random(int? Seed, params char[] All)
		{
			if (All == null || All.Length == 0)
			{
				return '\0';
			}

			Random rnd;
			if (Seed == null)
			{
				rnd = new Random();
			}
			else
			{
				rnd = new Random((int)Seed);
			}

			return All[rnd.Next(All.Length)];
		}

		/// <summary>
		/// 获取随机字符
		/// </summary>
		/// <param name="Seed">种子值[只需要第0个值,null表示使用默认种子值]</param>
		/// <param name="All">字符集</param>
		/// <returns></returns>
		public static char Random(int? Seed, string All)
		{
			return Random(Seed, All.ToCharArray());
		}
	}
}
