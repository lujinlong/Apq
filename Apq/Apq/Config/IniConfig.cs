using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace Apq.Config
{
	// 有机会需要使用ini文件的时候确定下读写文件是否需要加锁
	/// <summary>
	/// ini操作类
	/// </summary>
	public partial class IniConfig : Apq.Config.clsConfig
	{
		/// <summary>
		/// 写入INI文件
		/// </summary>
		/// <param name="Section">项目名称(如 [ClassName] )</param>
		/// <param name="Key">键</param>
		/// <param name="Value">值</param>
		public void Write(string Section, string Key, string Value)
		{
			Apq.DllImports.Kernel32.WritePrivateProfileString(Section, Key, Value, Path);
		}

		/// <summary>
		/// 读出INI文件
		/// </summary>
		/// <param name="Section">项目名称(如 [ClassName])</param>
		/// <param name="Key">键</param>
		public string Read(string Section, string Key)
		{
			FileInfo fi = new FileInfo(Path);
			StringBuilder temp = new StringBuilder(Apq.Convert.ChangeType<int>(fi.Length, 2048));
			int i = Apq.DllImports.Kernel32.GetPrivateProfileString(Section, Key, "", temp, temp.Capacity, Path);
			return temp.ToString();
		}

		/// <summary>
		/// 获取或设置文件路径
		/// </summary>
		public override string Path
		{
			get { return base.Path; }
			set
			{
				File.AppendAllText(value, string.Empty, Encoding.UTF8);

				base.Path = value;
			}
		}

		/// <summary>
		/// 获取配置名列表
		/// </summary>
		/// <returns></returns>
		public override string[] GetValueNames()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// 获取或设置配置[推荐使用]
		/// </summary>
		/// <param name="ClassName">类名</param>
		/// <param name="PropertyName">属性名</param>
		/// <returns></returns>
		public override string this[string ClassName, string PropertyName]
		{
			get
			{
				return Read(ClassName, PropertyName);
			}
			set
			{
				Write(ClassName, PropertyName, value);
			}
		}
	}
}
