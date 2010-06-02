using System;
using System.Collections.Generic;
using System.Text;

namespace Apq.Shell
{
	/// <summary>
	/// 解析结果
	/// </summary>
	public class ParseResult
	{
		/// <summary>
		/// 命令
		/// </summary>
		public string cmd;
		/// <summary>
		/// 括号对 集合
		/// </summary>
		public System.Collections.ArrayList Pars = new System.Collections.ArrayList();
	}
}
