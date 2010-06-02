using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.Configuration;

namespace Apq.Configuration
{
	/// <summary>
	/// 提供配置文件管理
	/// </summary>
	public class Configs
	{
		private static string _AppConfigFolder;
		/// <summary>
		/// 获取 AppConfig 配置文件夹
		/// </summary>
		/// <param name="Assembly">程序集</param>
		/// <returns></returns>
		public static string GetAppConfigFolder(System.Reflection.Assembly Assembly)
		{
			if (string.IsNullOrEmpty(_AppConfigFolder))
			{
				_AppConfigFolder = System.Configuration.ConfigurationManager.AppSettings["Apq.Configuration.Configs.AppConfigFolder"];
				if (string.IsNullOrEmpty(_AppConfigFolder))
				{
					_AppConfigFolder = System.IO.Path.GetFullPath(Assembly.Location);
				}
			}
			return _AppConfigFolder;
		}

		/// <summary>
		/// 获取一个配置文件
		/// </summary>
		/// <param name="Path">配置文件的完整路径</param>
		/// <returns></returns>
		public static System.Configuration.Configuration GetConfig(string Path)
		{
			if (!Apq.GlobalObject.Configs.ContainsKey(Path))
			{
				object LockFile = Apq.Locks.GetFileLock(Path);
				lock (LockFile)
				{
					Apq.GlobalObject.Configs.Add(Path, ConfigurationManager.Open(Path));
				}
			}
			return Apq.GlobalObject.Configs[Path];
		}

		/// <summary>
		/// 获取一个程序集的配置文件
		/// </summary>
		/// <param name="Assembly">程序集</param>
		/// <returns></returns>
		public static System.Configuration.Configuration GetConfig(System.Reflection.Assembly Assembly)
		{
			return GetConfig(GetAppConfigFolder(Assembly) + System.IO.Path.GetFileName(Assembly.Location) + ".config");
		}

		/// <summary>
		/// 保存所有配置文件
		/// </summary>
		public static void SaveAll()
		{
			foreach (System.Configuration.Configuration Config in Apq.GlobalObject.Configs.Values)
			{
				object LockFile = Apq.Locks.GetFileLock(Config.FilePath);
				lock (LockFile)
				{
					Config.Save(System.Configuration.ConfigurationSaveMode.Minimal);
				}
			}
		}
	}
}
