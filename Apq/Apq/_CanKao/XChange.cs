using System;
using System.Collections.Generic;
using System.Text;

namespace Apq.参考
{
	class XChange
	{
		public static void XChangeInt()
		{
			int a = 5;
			int b = 6;

			// 1
			//a = b | (b = a) & 0;
			// 2
			//a ^= b ^ (b ^= a ^ b);
			// 3
			a ^= b;
			b ^= a;
			a ^= b;
		}
	}
}
