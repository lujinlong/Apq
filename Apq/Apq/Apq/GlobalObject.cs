using System;
using System.Collections.Generic;
using System.Text;

namespace Apq
{
	/// <summary>
	/// 全局对象
	/// </summary>
	public static class GlobalObject
	{
		#region TheAssembly
		private static System.Reflection.Assembly _TheAssembly;
		/// <summary>
		/// 该程序集配置文件(程序集名.后缀.xml)
		/// </summary>
		public static System.Reflection.Assembly TheAssembly
		{
			get
			{
				if (_TheAssembly == null)
				{
					_TheAssembly = System.Reflection.Assembly.GetExecutingAssembly();
				}
				return _TheAssembly;
			}
		}
		#endregion

		#region XmlAsmConfig
		private static Apq.Config.XmlConfig _XmlAsmConfig;
		/// <summary>
		/// 该程序集配置文件(程序集名.后缀.xml)
		/// </summary>
		public static Apq.Config.XmlConfig XmlAsmConfig
		{
			get
			{
				if (Properties.Settings.Default.XmlAsmConfigFolder.Length > 2)
				{
					_XmlAsmConfig = Apq.Config.ApqConfigs.GetAsmConfig(Properties.Settings.Default.XmlAsmConfigFolder + "Apq.dll.apq") as Apq.Config.XmlConfig;
				}
				if (_XmlAsmConfig == null)
				{
					_XmlAsmConfig = Apq.Config.ApqConfigs.GetAsmConfig(TheAssembly) as Apq.Config.XmlConfig;
				}
				return _XmlAsmConfig;
			}
		}
		#endregion

		#region XmlUserConfig
		private static Apq.Config.XmlConfig _XmlUserConfig;
		/// <summary>
		/// 该程序集用户配置文件(程序集名.后缀.xml)
		/// </summary>
		public static Apq.Config.XmlConfig XmlUserConfig
		{
			get
			{
				if (Properties.Settings.Default.XmlAsmConfigFolder.Length > 2)
				{
					_XmlUserConfig = Apq.Config.ApqConfigs.GetUserConfig(Properties.Settings.Default.XmlAsmConfigFolder + "Apq.dll." + Environment.UserName + ".apq") as Apq.Config.XmlConfig;
				}
				if (_XmlUserConfig == null)
				{
					_XmlUserConfig = Apq.Config.ApqConfigs.GetUserConfig(TheAssembly) as Apq.Config.XmlConfig;
				}
				return _XmlUserConfig;
			}
		}
		#endregion

		#region XmlConfigChain
		private static Apq.Config.ConfigChain _XmlConfigChain;
		/// <summary>
		/// 该程序集配置文件链
		/// </summary>
		public static Apq.Config.ConfigChain XmlConfigChain
		{
			get
			{
				if (_XmlConfigChain == null)
				{
					_XmlConfigChain = new Apq.Config.ConfigChain(XmlAsmConfig, XmlUserConfig);
				}
				return _XmlConfigChain;
			}
		}
		#endregion

		#region ApqLog
		private static log4net.ILog _ApqLog;
		/// <summary>
		/// Logger
		/// </summary>
		public static log4net.ILog ApqLog
		{
			get
			{
				if (_ApqLog == null)
				{
					_ApqLog = log4net.LogManager.GetLogger("Apq");
				}
				return _ApqLog;
			}
		}
		#endregion

		#region AsmConfigs
		private static Dictionary<string, Apq.Config.clsConfig> _AsmConfigs;
		/// <summary>
		/// 程序集配置文件集合
		/// </summary>
		public static Dictionary<string, Apq.Config.clsConfig> AsmConfigs
		{
			get
			{
				if (_AsmConfigs == null)
				{
					_AsmConfigs = new Dictionary<string, Apq.Config.clsConfig>();
				}
				return _AsmConfigs;
			}
		}
		#endregion

		#region UserConfigs
		private static Dictionary<string, Apq.Config.clsConfig> _UserConfigs;
		/// <summary>
		/// 用户配置文件集合
		/// </summary>
		public static Dictionary<string, Apq.Config.clsConfig> UserConfigs
		{
			get
			{
				if (_UserConfigs == null)
				{
					_UserConfigs = new Dictionary<string, Apq.Config.clsConfig>();
				}
				return _UserConfigs;
			}
		}
		#endregion

		#region Configs
		private static Dictionary<string, System.Configuration.Configuration> _Configs;
		/// <summary>
		/// 配置文件集合
		/// </summary>
		public static Dictionary<string, System.Configuration.Configuration> Configs
		{
			get
			{
				if (_Configs == null)
				{
					_Configs = new Dictionary<string, System.Configuration.Configuration>();
				}
				return _Configs;
			}
		}
		#endregion

		#region NamedInstances
		private static Dictionary<string, object> _NamedInstances;
		/// <summary>
		/// 命名实例集合
		/// </summary>
		public static Dictionary<string, object> NamedInstances
		{
			get
			{
				if (_NamedInstances == null)
				{
					_NamedInstances = new Dictionary<string, object>();
				}
				return _NamedInstances;
			}
		}
		#endregion

		#region SingletonInstances
		private static Dictionary<string, object> _SingletonInstances;
		/// <summary>
		/// 单态实例集合
		/// </summary>
		public static Dictionary<string, object> SingletonInstances
		{
			get
			{
				if (_SingletonInstances == null)
				{
					_SingletonInstances = new Dictionary<string, object>();
				}
				return _SingletonInstances;
			}
		}
		#endregion

		#region Locks
		private static Dictionary<string, object> _Locks;
		/// <summary>
		/// 资源锁对象集合(供lock使用)
		/// </summary>
		public static Dictionary<string, object> Locks
		{
			get
			{
				if (_Locks == null)
				{
					_Locks = new Dictionary<string, object>();
				}
				return _Locks;
			}
		}
		#endregion
	}
}
