using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Management;
using System.Configuration;

namespace Apq.Config
{
	/// <summary>
	/// 配置管理池
	/// </summary>
	public partial class ApqConfigs
	{
		/// <summary>
		/// Xml 配置文件名后缀["apq"]
		/// </summary>
		private static string XmlConfigExt;

		private static string _XmlConfigFolder;
		/// <summary>
		/// 获取 Xml 配置文件夹
		/// </summary>
		/// <param name="Assembly">程序集</param>
		/// <returns></returns>
		public static string GetXmlConfigFolder(System.Reflection.Assembly Assembly)
		{
			if (string.IsNullOrEmpty(_XmlConfigFolder))
			{
				_XmlConfigFolder = ConfigurationManager.AppSettings["Apq.Config.ApqConfigs.XmlConfigFolder"];
				if (string.IsNullOrEmpty(_XmlConfigFolder))
				{
					_XmlConfigFolder = System.IO.Path.GetDirectoryName(Assembly.Location) + "\\";
				}
			}
			return _XmlConfigFolder;
		}

		#region AsmConfig
		/// <summary>
		/// [反射]获取一个程序集配置文件
		/// </summary>
		/// <param name="Path">配置文件的完整路径</param>
		/// <param name="Root"></param>
		/// <param name="ClsName"></param>
		/// <returns></returns>
		public static clsConfig GetAsmConfig(string Path, string Root, string ClsName)
		{
			if (!Apq.GlobalObject.AsmConfigs.ContainsKey(Path))
			{
				clsConfig Config = Activator.CreateInstance(Type.GetType(ClsName)) as clsConfig;
				if (Config != null)
				{
					Config.Open(Path, Root);
					Path = Config.Path;	// 兼容注册表
					try
					{
						Apq.GlobalObject.AsmConfigs.Add(Path, Config);
					}
					catch { }
				}
			}
			return Apq.GlobalObject.AsmConfigs[Path] as clsConfig;
		}

		/// <summary>
		/// 获取一个程序集配置文件(XmlConfig)
		/// </summary>
		/// <param name="Path">配置文件的完整路径</param>
		/// <param name="Root">Root</param>
		/// <returns></returns>
		public static clsConfig GetAsmConfig(string Path, string Root)
		{
			return GetAsmConfig(Path, Root, "Apq.Config.XmlConfig");
		}

		/// <summary>
		/// 获取一个程序集配置文件(XmlConfig)
		/// </summary>
		/// <param name="Path">配置文件的完整路径</param>
		/// <returns></returns>
		public static clsConfig GetAsmConfig(string Path)
		{
			return GetAsmConfig(Path, "Root");
		}

		/// <summary>
		/// 获取一个程序集的程序集配置文件(XmlConfig)
		/// </summary>
		/// <param name="Assembly">程序集</param>
		/// <returns></returns>
		public static clsConfig GetAsmConfig(System.Reflection.Assembly Assembly)
		{
			if (string.IsNullOrEmpty(XmlConfigExt))
			{
				XmlConfigExt = ConfigurationManager.AppSettings["Apq.Config.ApqConfigs.XmlConfigExt"];
				if (string.IsNullOrEmpty(XmlConfigExt))
				{
					XmlConfigExt = "apq";
				}
			}
			return GetAsmConfig(GetXmlConfigFolder(Assembly) + System.IO.Path.GetFileName(Assembly.Location) + "." + XmlConfigExt);
		}

		/// <summary>
		/// 保存所有程序集配置文件
		/// </summary>
		public static void SaveAllAsmConfig()
		{
			foreach (clsConfig Config in Apq.GlobalObject.AsmConfigs.Values)
			{
				Config.Save();
			}
		}
		#endregion

		#region UserConfig
		/// <summary>
		/// [反射]获取一个用户配置文件
		/// </summary>
		/// <param name="Path">配置文件的完整路径</param>
		/// <param name="Root"></param>
		/// <param name="ClsName"></param>
		/// <returns></returns>
		public static clsConfig GetUserConfig(string Path, string Root, string ClsName)
		{
			if (!Apq.GlobalObject.UserConfigs.ContainsKey(Path))
			{
				clsConfig Config = Activator.CreateInstance(Type.GetType(ClsName)) as clsConfig;
				if (Config != null)
				{
					Config.Open(Path, Root);
					Path = Config.Path;	// 兼容注册表
					try
					{
						Apq.GlobalObject.UserConfigs.Add(Path, Config);
					}
					catch { }
				}
			}
			return Apq.GlobalObject.UserConfigs[Path] as clsConfig;
		}

		/// <summary>
		/// 获取一个用户配置文件(XmlConfig)
		/// </summary>
		/// <param name="Path">配置文件的完整路径</param>
		/// <param name="Root">Root</param>
		/// <returns></returns>
		public static clsConfig GetUserConfig(string Path, string Root)
		{
			return GetUserConfig(Path, Root, "Apq.Config.XmlConfig");
		}

		/// <summary>
		/// 获取一个用户配置文件(XmlConfig)
		/// </summary>
		/// <param name="Path">配置文件的完整路径</param>
		/// <returns></returns>
		public static clsConfig GetUserConfig(string Path)
		{
			return GetUserConfig(Path, "Root");
		}

		/// <summary>
		/// 获取一个程序集的用户配置文件(XmlConfig)
		/// </summary>
		/// <param name="Assembly">程序集</param>
		/// <returns></returns>
		public static clsConfig GetUserConfig(System.Reflection.Assembly Assembly)
		{
			if (string.IsNullOrEmpty(XmlConfigExt))
			{
				XmlConfigExt = ConfigurationManager.AppSettings["Apq.Config.ApqConfigs.XmlConfigExt"];
				if (string.IsNullOrEmpty(XmlConfigExt))
				{
					XmlConfigExt = "apq";
				}
			}
			return GetUserConfig(GetXmlConfigFolder(Assembly) + System.IO.Path.GetFileName(Assembly.Location) + "." + Environment.UserName + "." + XmlConfigExt);
		}

		/// <summary>
		/// 保存所有用户配置文件
		/// </summary>
		public static void SaveAllUserConfig()
		{
			foreach (clsConfig Config in Apq.GlobalObject.UserConfigs.Values)
			{
				Config.Save();
			}
		}
		#endregion

		#region SaveAll
		/// <summary>
		/// 保存所有配置文件
		/// </summary>
		public static void SaveAll()
		{
			SaveAllAsmConfig();
			SaveAllUserConfig();
		}
		#endregion
	}
}
