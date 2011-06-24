using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ApqDBCManager.Forms;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ApqDBCManager
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

		#region 注册表配置示例
		/*
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
					_RegSysConfig.Root = @"SOFTWARE\Apq\ApqDBCManager";
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
					_RegUserConfig.Root = @"SOFTWARE\Apq\ApqDBCManager";
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
		*/
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

		#region Lookup数据集
		private static DBS_XSD _Lookup;
		/// <summary>
		/// 获取Lookup数据集(DBC为空)
		/// </summary>
		public static DBS_XSD Lookup
		{
			get
			{
				if (_Lookup == null)
				{
					_Lookup = new DBS_XSD();

					// 从文档加载数据
					string strFile = GlobalObject.XmlConfigChain["ApqDBCManager", "file_Lookup"];
					if (System.IO.File.Exists(strFile))
					{
						try
						{
							_Lookup.ReadXml(strFile);
							// 密码解密
							Common.PwdC2D(_Lookup.DBI);
						}
						catch { }
					}

					// 加载基本Lookup表
					_Lookup.ComputerType.InitData();
					_Lookup.DBProduct.InitData();
					_Lookup.DBCUseType.InitData();

					// 增加TreeListView需要的Lookup列及取值
					_Lookup.DBI.Columns.Add("ComputerName");
					_Lookup.DBI.Columns.Add("DBITypeName");
					_Lookup.DBI.Columns.Add("DBMS");
					foreach (DBS_XSD.DBIRow dr in _Lookup.DBI.Rows)
					{
						try
						{
							DBS_XSD.ComputerRow drDBS = _Lookup.Computer.FindByComputerID(dr.ComputerID);
							dr["ComputerName"] = drDBS.ComputerName;
						}
						catch { }

						try
						{
							DBS_XSD.DBITypeRow drDBIType = _Lookup.DBIType.FindByDBIType(dr.DBIType);
							dr["DBITypeName"] = drDBIType.TypeCaption;
						}
						catch { }

						try
						{
							DBS_XSD.DBProductRow drDBMS = _Lookup.DBProduct.FindByDBProduct(dr.DBProduct);
							dr["DBMS"] = drDBMS.DBProductName;
						}
						catch { }
					}

					_Lookup.DBI.ColumnChanged += new DataColumnChangeEventHandler(DBI_ColumnChanged);
				}
				return _Lookup;
			}
		}

		private static void DBI_ColumnChanged(object sender, DataColumnChangeEventArgs e)
		{
			// 密码加密
			if (e.Column.ColumnName == "PwdD" && !Apq.Convert.LikeDBNull(e.Row["PwdD"]))
			{
				e.Row["PwdC"] = Apq.Security.Cryptography.DESHelper.EncryptString(Apq.Convert.ChangeType<string>(e.Row["PwdD"]),
					XmlConfigChain["Crypt", "DESKey"], XmlConfigChain["Crypt", "DESIV"]);
			}

			if (e.Column.ColumnName == "ComputerName" && !Apq.Convert.LikeDBNull(e.Row["ComputerName"]))
			{
				e.Row["ComputerID"] = _Lookup.Computer.FindByComputerName(e.Row["ComputerName"].ToString())["ComputerID"];
			}

			if (e.Column.ColumnName == "DBITypeName" && !Apq.Convert.LikeDBNull(e.Row["DBITypeName"]))
			{
				e.Row["DBIType"] = _Lookup.DBIType.FindByTypeCaption(e.Row["DBITypeName"].ToString())["DBIType"];
			}

			if (e.Column.ColumnName == "DBMS" && !Apq.Convert.LikeDBNull(e.Row["DBMS"]))
			{
				e.Row["DBProduct"] = Enum.Parse(typeof(Apq.Data.Common.DBProduct), e.Row["DBMS"].ToString());
			}
		}

		/// <summary>
		/// 保存Lookup数据集(不含DBC数据)
		/// </summary>
		public static void Lookup_Save()
		{
			DBS_XSD xsd = new DBS_XSD();
			xsd.Merge(GlobalObject.Lookup);

			xsd.Tables.Remove(xsd.DBC);
			xsd.DBI.Columns.Remove("PwdD");
			xsd.DBI.Columns.Remove("ComputerName");
			xsd.DBI.Columns.Remove("DBITypeName");
			xsd.DBI.Columns.Remove("DBMS");

			xsd.WriteXml(XmlConfigChain["ApqDBCManager", "file_Lookup"], XmlWriteMode.IgnoreSchema);
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
