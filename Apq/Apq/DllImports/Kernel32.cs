using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Apq.DllImports
{
	/// <summary>
	/// Kernel32
	/// </summary>
	public class Kernel32
	{
		#region PrivateProfileString
		/// <summary>
		/// 写
		/// </summary>
		/// <param name="section">INI文件中的段落</param>
		/// <param name="key">INI文件中的关键字</param>
		/// <param name="val">INI文件中关键字的值</param>
		/// <param name="filePath">INI文件的完整的路径和名称</param>
		/// <returns></returns>
		[DllImport("Kernel32.dll")]
		public static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
		/// <summary>
		/// 读取
		/// </summary>
		/// <param name="section">INI文件中的段落名称</param>
		/// <param name="key">INI文件中的关键字</param>
		/// <param name="def">无法读取时候时候的缺省值</param>
		/// <param name="retVal">读取值</param>
		/// <param name="size">值的大小</param>
		/// <param name="filePath">INI文件的完整路径和名称</param>
		/// <returns></returns>
		[DllImport("Kernel32.dll")]
		public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
		#endregion

		/// <summary>
		/// 获取驱动类型
		/// </summary>
		/// <param name="driveLetter"></param>
		/// <returns></returns>
		[DllImport("Kernel32.dll")]
		public static extern uint GetDriveType(String driveLetter);

		/// <summary>
		/// 获取卷信息
		/// </summary>
		/// <param name="root"></param>
		/// <param name="name"></param>
		/// <param name="nameLen"></param>
		/// <param name="serial"></param>
		/// <param name="maxCompLen"></param>
		/// <param name="flags"></param>
		/// <param name="fileSysName"></param>
		/// <param name="fileSysNameLen"></param>
		/// <returns></returns>
		[DllImport("Kernel32.dll")]
		public static extern bool GetVolumeInformation(String root, byte[] name, int nameLen,
			out int serial, out int maxCompLen, out int flags, byte[] fileSysName,
			int fileSysNameLen);

		/// <summary>
		/// 进程暂停毫秒数
		/// </summary>
		[DllImport("Kernel32.dll")]
		public static extern void Sleep(uint dwMilliseconds);
	}
}
