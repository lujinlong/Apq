using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Apq.Configuration
{
	/// <summary>
	/// 提供对客户端应用程序配置文件的访问。
	/// </summary>
	public class ConfigurationManager
	{
		/// <summary>
		/// 将指定的客户端配置文件作为 System.Configuration.Configuration 对象打开。
		/// </summary>
		/// <param name="Path">配置文件的完整路径</param>
		/// <returns></returns>
		public static System.Configuration.Configuration Open(string Path)
		{
			if (!File.Exists(Path))
			{
				string str = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<configuration/>
";
				File.WriteAllText(Path, str, Encoding.UTF8);
			}

			System.Configuration.ExeConfigurationFileMap ecfm = new System.Configuration.ExeConfigurationFileMap();
			ecfm.ExeConfigFilename = Path;
			object LockFile = Apq.Locks.GetFileLock(Path);
			System.Configuration.Configuration Config = null;
			lock (LockFile)
			{
				Config = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(ecfm, System.Configuration.ConfigurationUserLevel.None);
			}
			return Config;
		}

		/// <summary>
		/// 将指定的程序集的配置文件作为 System.Configuration.Configuration 对象打开。
		/// </summary>
		/// <param name="Assembly">程序集</param>
		/// <returns></returns>
		public static System.Configuration.Configuration Open(System.Reflection.Assembly Assembly)
		{
			return Open(Configs.GetAppConfigFolder(Assembly)+System.IO.Path.GetFileName( Assembly.Location )+ ".config");
			//return Open(Assembly.Location + ".config");
		}
	}
}
