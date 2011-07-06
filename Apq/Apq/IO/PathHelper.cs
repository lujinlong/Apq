using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apq.IO
{
	/// <summary>
	/// 路径串助手
	/// </summary>
	public class PathHelper
	{
		/// <summary>
		/// 文件后缀匹配
		/// </summary>
		/// <param name="FullName">文件(全)名</param>
		/// <param name="strExt">文件类型(不含*时全匹配,否则只验证后缀)</param>
		/// <returns></returns>
		public static bool MatchFileExt(string FullName, string strExt)
		{
			FullName = FullName ?? string.Empty;
			if (string.IsNullOrWhiteSpace(strExt))
			{
				strExt = "*.*";
			}

			bool rtn = false;
			string FileName = System.IO.Path.GetFileName(FullName);
			string[] strExts = strExt.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

			// 匹配文件类型
			if (strExt.Contains("*"))
			{
				foreach (string s in strExts)
				{
					if (s == "*.*")
					{
						rtn = true;
						break;
					}
					if (System.IO.Path.HasExtension(FileName))
					{
						if (System.IO.Path.GetExtension(FileName).Equals(System.IO.Path.GetExtension(s), StringComparison.OrdinalIgnoreCase))
						{
							rtn = true;
							break;
						}
					}
				}
			}
			else
			{
				foreach (string s in strExts)
				{
					if (FileName.Equals(s, StringComparison.OrdinalIgnoreCase))
					{
						rtn = true;
						break;
					}
				}
			}

			return rtn;
		}
	}
}
