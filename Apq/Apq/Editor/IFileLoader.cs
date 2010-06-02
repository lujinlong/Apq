using System;
using System.Collections.Generic;
using System.Text;

namespace Apq.Editor
{
	/// <summary>
	/// 文件加载器(对一个文件进行加载或保存)
	/// </summary>
	public interface IFileLoader
	{
		/// <summary>
		/// 获取或设置文件名
		/// </summary>
		string FileName { get;set;}

		/// <summary>
		/// 打开由 FileName 指定的文件
		/// </summary>
		void Open();
		/// <summary>
		/// 保存到由 FileName 指定的文件
		/// </summary>
		void Save();
		/// <summary>
		/// 另存为
		/// </summary>
		/// <param name="FileName">保存到由参数 FileName 指定的文件</param>
		void SaveAs(string FileName);
	}
}
