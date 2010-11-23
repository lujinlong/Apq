using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apq.CryptKey
{
	/// <summary>
	/// DES加密Key
	/// </summary>
	public class DES
	{
		/// <summary>
		/// 加密密钥
		/// </summary>
		public static byte[] Key
		{
			//冰川	~JD7(1vy]$ik7WB)
			//UM	`G#1y{~89a\W*@)n
			get { return System.Text.Encoding.Unicode.GetBytes(@"`G#1y{~8"); }
		}

		/// <summary>
		/// 初始向量
		/// </summary>
		public static byte[] IV
		{
			get { return System.Text.Encoding.Unicode.GetBytes(@"9a\W*@)n"); }
		}
	}
}
