using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pdbp.CryptKey
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
			get { return System.Text.Encoding.Unicode.GetBytes("`=4DC(L{"); }
		}

		/// <summary>
		/// 初始向量
		/// </summary>
		public static byte[] IV
		{
			get { return System.Text.Encoding.Unicode.GetBytes("Bi0j6-xr"); }
		}
	}
}
