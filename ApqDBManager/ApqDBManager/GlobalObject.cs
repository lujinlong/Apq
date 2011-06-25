using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ApqDBManager.Forms;
using System.Data.SqlClient;
using System.Windows.Forms;

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

		#region xsdDBC
		private static Apq.DBC.XSD _xsdDBC;
		/// <summary>
		/// 获取服务器集
		/// </summary>
		public static Apq.DBC.XSD xsdDBC
		{
			get
			{
				if (_xsdDBC == null)
				{
					_xsdDBC = Apq.DBC.Common.XSD.Copy() as Apq.DBC.XSD;
					xsdDBCAdjust();
				}
				return _xsdDBC;
			}
		}

		public static void xsdDBCAdjust()
		{
#if Debug_Home
			if (_xsdDBC != null)
			foreach (Apq.DBC.XSD.DBIRow dr in _Sqls.DBI.Rows)
			{
				dr["DBConnectionString"] = "Data Source=.;User ID=apq;Password=f;";
				dr.IP = "127.0.0.1";
			}
#endif
		}
		#endregion

		#region AboutBox
		private static Apq.Windows.Forms.AboutBox _AboutBox;
		/// <summary>
		/// 获取选项窗口
		/// </summary>
		public static Apq.Windows.Forms.AboutBox AboutBox
		{
			get
			{
				if (_AboutBox == null)
				{
					_AboutBox = new Apq.Windows.Forms.AboutBox();
				}
				return _AboutBox;
			}
		}
		#endregion
	}
}
