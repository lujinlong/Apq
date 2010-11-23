using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ApqDBManager.Forms;

namespace ApqDBManager
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

		#region RegSysConfig
		private static Apq.Config.RegConfig _RegSysConfig;
		/// <summary>
		/// 该程序集配置文件(程序集名.后缀.Reg)
		/// </summary>
		public static Apq.Config.RegConfig RegSysConfig
		{
			get
			{
				if (_RegSysConfig == null)
				{
					_RegSysConfig = new Apq.Config.RegConfig();
					_RegSysConfig.Path = "$reg$LocalMachine";
					_RegSysConfig.Root = @"SOFTWARE\Apq\ApqDBManager";
				}
				return _RegSysConfig;
			}
		}
		#endregion

		#region RegUserConfig
		private static Apq.Config.RegConfig _RegUserConfig;
		/// <summary>
		/// 该程序集用户配置文件(程序集名.后缀.Reg)
		/// </summary>
		public static Apq.Config.RegConfig RegUserConfig
		{
			get
			{
				if (_RegUserConfig == null)
				{
					_RegUserConfig = new Apq.Config.RegConfig();
					_RegUserConfig.Path = "$reg$CurrentUser";
					_RegUserConfig.Root = @"SOFTWARE\Apq\ApqDBManager";
				}
				return _RegUserConfig;
			}
		}
		#endregion

		#region RegConfigChain
		private static Apq.Config.ConfigChain _RegConfigChain;
		/// <summary>
		/// 该程序集配置文件链
		/// </summary>
		public static Apq.Config.ConfigChain RegConfigChain
		{
			get
			{
				if (_RegConfigChain == null)
				{
					_RegConfigChain = new Apq.Config.ConfigChain(RegSysConfig, RegUserConfig);
				}
				return _RegConfigChain;
			}
		}
		#endregion

		#region MainForm
		private static MainForm _MainForm;
		/// <summary>
		/// 获取主窗口
		/// </summary>
		public static MainForm MainForm
		{
			get
			{
				if (_MainForm == null)
				{
					_MainForm = new MainForm();
				}
				return _MainForm;
			}
		}
		#endregion

		#region SolutionExplorer
		private static SolutionExplorer _SolutionExplorer;
		/// <summary>
		/// 获取解决方案视图
		/// </summary>
		public static SolutionExplorer SolutionExplorer
		{
			get
			{
				if (_SolutionExplorer == null)
				{
					_SolutionExplorer = new SolutionExplorer();
				}
				return _SolutionExplorer;
			}
		}
		#endregion

		#region Favorites
		private static Favorites _Favorites;
		/// <summary>
		/// 获取收藏夹视图
		/// </summary>
		public static Favorites Favorites
		{
			get
			{
				if (_Favorites == null)
				{
					_Favorites = new Favorites();
				}
				return _Favorites;
			}
		}
		#endregion

		#region ErrList
		private static ErrList _ErrList;
		/// <summary>
		/// 获取错误列表视图
		/// </summary>
		public static ErrList ErrList
		{
			get
			{
				if (_ErrList == null)
				{
					_ErrList = new ErrList();
				}
				return _ErrList;
			}
		}
		#endregion

		#region Servers
		private static XSD.Servers _Servers;
		/// <summary>
		/// 获取服务器集
		/// </summary>
		public static XSD.Servers Servers
		{
			get
			{
				if (_Servers == null)
				{
					ServersReload();
				}
				return _Servers;
			}
		}

		public static void ServersReload()
		{
			if (_Servers == null)
			{
				_Servers = new XSD.Servers();
				_Servers.dtServers.Columns.Add("ConnectionString");
			}
			_Servers.Clear();

			_Servers.dtServers.ReadXml(GlobalObject.XmlConfigChain[typeof(GlobalObject), "XmlServers"]);//此文件中密码已加密
			//解密密码,生成连接字符串
			foreach (XSD.Servers.dtServersRow dr in _Servers.dtServers.Rows)
			{
				if (!Apq.Convert.LikeDBNull(dr["PwdC"]))
				{
					dr.PwdD = Apq.Security.Cryptography.DESHelper.DecryptString(dr.PwdC, GlobalObject.RegConfigChain["Crypt", "DESKey"], GlobalObject.RegConfigChain["Crypt", "DESIV"]);
					dr["ConnectionString"] = string.Format("Data Source={0},{1};User Id={2};Password={3};", dr.IPWan1, dr.SqlPort, dr.UID, dr.PwdD);
				}
				if (!Apq.Convert.LikeDBNull(dr["FTPPC"]))
				{
					dr.FTPPD = Apq.Security.Cryptography.DESHelper.DecryptString(dr.FTPPC, GlobalObject.RegConfigChain["Crypt", "DESKey"], GlobalObject.RegConfigChain["Crypt", "DESIV"]);
				}
			}

#if Debug_Home
			foreach (XSD.Servers.dtServersRow dr in _Servers.dtServers.Rows)
			{
				dr["ConnectionString"] = "Data Source=.;User ID=apq;Password=f;";
				dr.IPWan1 = "127.0.0.1";
			}
#endif

			_Servers.dtServers.AcceptChanges();
		}
		#endregion

		#region MainOption
		private static MainOption _MainOption;
		/// <summary>
		/// 获取选项窗口
		/// </summary>
		public static MainOption MainOption
		{
			get
			{
				if (_MainOption == null)
				{
					_MainOption = new MainOption();
				}
				return _MainOption;
			}
		}
		#endregion
	}
}
