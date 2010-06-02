using System;
using System.Collections.Generic;
using System.Text;

namespace Apq
{
	/// <summary>
	/// Apq.String
	/// </summary>
	public class String
	{
		/// <summary>
		/// 易于辨识的字母数字范围(小写优先)
		/// </summary>
		public static string SimpleChars = "0123456789acdefhijkmnprtuvwxyABCDEFGHJLMNRTY";
		/// <summary>
		/// 易于辨识的字母数字范围(大写优先)
		/// </summary>
		public static string SimpleChars2 = "0123456789ABCDEFGHJKLMNPRTUVWXYacdefhijmnrty";

		#region 随机字符串
		/// <summary>
		/// 生成随机字符串
		/// </summary>
		/// <param name="Length">指定长度</param>
		/// <param name="Repeat">是否允许重复</param>
		/// <param name="Seed">种子值[只需要第0个值,null表示使用默认种子值]</param>
		/// <param name="All">字符集</param>
		/// <returns></returns>
		public static string Random(int Length, bool Repeat, int? Seed, params char[] All)
		{
			if (All == null || All.Length == 0)
			{
				return null;
			}

			Random rnd = new Random();
			if (Seed != null)
			{
				rnd = new Random((int)Seed);
			}

			char[] aryAll = All;
			if (!Repeat)
			{
				// 调整长度
				Length = Math.Min(Length, All.Length);
				// 复制字符集
				aryAll = new char[All.Length];
				All.CopyTo(aryAll, 0);
			}

			string strReturn = string.Empty;
			for (int i = 0; i < Length; i++)
			{
				int? arySeed = rnd.Next();
				char ch = Apq.Char.Random(arySeed, aryAll);
				strReturn += ch;

				#region 不允许重复
				if (!Repeat)
				{
					for (int j = 0; j < aryAll.Length; j++)
					{
						if (aryAll[j] == ch)
						{
							aryAll[j] = aryAll[aryAll.Length - 1];
							break;
						}
					}

					char[] aryTemp = new char[aryAll.Length - 1];
					for (int j = 0; j < aryAll.Length - 1; j++)
					{
						aryTemp[j] = aryAll[j];
					}
					aryAll = aryTemp;
				}
				#endregion
			}

			return strReturn;
		}

		/// <summary>
		/// 生成随机字符串
		/// </summary>
		/// <param name="Length">指定长度</param>
		/// <param name="Repeat">是否允许重复</param>
		/// <param name="Seed">种子值[只需要第0个值,null表示使用默认种子值]</param>
		/// <param name="All">字符集</param>
		/// <returns></returns>
		public static string Random(int Length, bool Repeat, int? Seed, string All)
		{
			return Random(Length, Repeat, Seed, All.ToCharArray());
		}

		/// <summary>
		/// 返回指定数量,指定长度的随机字符串
		/// </summary>
		/// <param name="Count">指定的数量</param>
		/// <param name="Length">指定的长度</param>
		/// <param name="Repeat">是否允许重复</param>
		/// <param name="Seed">种子值[只需要第0个值,null表示使用默认种子值]</param>
		/// <param name="All">字符集</param>
		/// <returns></returns>
		public static string[] Random(uint Count, int Length, bool Repeat, int? Seed, params char[] All)
		{
			if (Count == 0)
			{
				return null;
			}

			Random rnd = new Random();
			if (Seed != null)
			{
				rnd = new Random((int)Seed);
			}

			string[] aryReturn = new string[Count];
			for (int i = 0; i < Count; i++)
			{
				int? nSeed = rnd.Next();
				aryReturn[i] = Random(Length, Repeat, nSeed, All);
			}

			return aryReturn;
		}

		/// <summary>
		/// 返回指定数量,指定长度的随机字符串
		/// </summary>
		/// <param name="Count">指定的数量</param>
		/// <param name="Length">指定的长度</param>
		/// <param name="Repeat">是否允许重复</param>
		/// <param name="Seed">种子值[只需要第0个值,null表示使用默认种子值]</param>
		/// <param name="All">字符集</param>
		/// <returns></returns>
		public static string[] Random(uint Count, int Length, bool Repeat, int? Seed, string All)
		{
			return Random(Count, Length, Repeat, Seed, All.ToCharArray());
		}
		#endregion
	}
}
