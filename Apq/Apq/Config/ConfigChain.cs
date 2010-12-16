using System;
using System.Collections.Generic;
using System.Text;

namespace Apq.Config
{
	/// <summary>
	/// 配置链,用于关联用户配置文件和程序集配置文件
	/// </summary>
	public sealed class ConfigChain : clsConfig
	{
		/// <summary>
		/// 配置链
		/// </summary>
		/// <param name="AsmConfig">程序集配置文件</param>
		/// <param name="UserConfig">用户配置文件</param>
		public ConfigChain(clsConfig AsmConfig, clsConfig UserConfig)
		{
			_AsmConfig = AsmConfig;
			_UserConfig = UserConfig;
		}

		#region AsmConfig
		/// <summary>
		/// 程序集配置文件
		/// </summary>
		private clsConfig _AsmConfig;
		/// <summary>
		/// 获取程序集配置文件
		/// </summary>
		public clsConfig AsmConfig
		{
			get { return _AsmConfig; }
		}
		#endregion

		#region UserConfig
		/// <summary>
		/// 用户配置文件
		/// </summary>
		private clsConfig _UserConfig;
		/// <summary>
		/// 获取用户配置文件
		/// </summary>
		public clsConfig UserConfig
		{
			get { return _UserConfig; }
		}
		#endregion

		#region 方法传递
		/// <summary>
		/// 保存配置
		/// </summary>
		public override void Save()
		{
			_UserConfig.Save();
			_AsmConfig.Save();
		}

		/// <summary>
		/// 另存为
		/// </summary>
		public override void SaveAs(string Path)
		{
			_UserConfig.SaveAs(Path);
			_AsmConfig.SaveAs(Path);
		}

		/// <summary>
		/// 关闭配置
		/// </summary>
		public override void Close()
		{
			_UserConfig.Close();
			_AsmConfig.Close();
		}
		#endregion

		#region 获取配置
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
				string str = UserConfig[ClassName, PropertyName];

				if (str == null)
				{
					str = AsmConfig[ClassName, PropertyName];
				}

				return str;
			}
			set
			{
				UserConfig[ClassName, PropertyName] = value;
			}
		}
		#endregion

		/// <summary>
		/// 获取配置名列表
		/// </summary>
		/// <returns></returns>
		public override string[] GetValueNames()
		{
			string[] lstUser = UserConfig.GetValueNames();
			string[] lstAsm = AsmConfig.GetValueNames();
			List<string> lst = new List<string>(lstUser.Length);
			lst.AddRange(lstUser);
			foreach (string str in lstAsm)
			{
				if (!lst.Contains(str))
				{
					lst.Add(str);
				}
			}
			return lst.ToArray();
		}

		/// <summary>
		/// 获取表类型配置[仅支持XmlConfig]
		/// </summary>
		/// <param name="ClassName">类名</param>
		/// <param name="TableElementName">表元素名</param>
		/// <returns></returns>
		public System.Data.DataTable GetTableConfig(string ClassName, string TableElementName)
		{
			XmlConfig AsmCfg = AsmConfig as XmlConfig;
			XmlConfig UserCfg = UserConfig as XmlConfig;

			if (AsmCfg != null && UserCfg != null)
			{
				System.Data.DataTable dtAsm = AsmCfg.GetTableConfig(ClassName, TableElementName);
				System.Data.DataTable dtUser = UserCfg.GetTableConfig(ClassName, TableElementName);
				if (dtAsm != null && dtUser != null)
				{
					dtUser.Merge(dtAsm);
					return Apq.Data.DataTable.GetDistinct(dtUser, null);
				}
				if (dtAsm != null)
				{
					return dtAsm;
				}
				return dtUser;
			}
			if (AsmCfg != null)
			{
				return AsmCfg.GetTableConfig(ClassName, TableElementName);
			}
			return UserCfg.GetTableConfig(ClassName, TableElementName);
		}

		#region Obsolete
		/// <summary>
		/// 不可使用
		/// </summary>
		[Obsolete("不可使用")]
		public new void Open(string Path, string Root)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// 不可使用
		/// </summary>
		[Obsolete("不可使用")]
		public override string Path
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>
		/// 不可使用
		/// </summary>
		[Obsolete("不可使用")]
		public override string Root
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>
		/// 不可使用
		/// </summary>
		[Obsolete("不可使用")]
		public override string this[string PropertyName]
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>
		/// 不可使用
		/// </summary>
		[Obsolete("不可使用")]
		public override string GetValue(string PropertyName)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// 不可使用
		/// </summary>
		[Obsolete("不可使用")]
		public override void SetValue(string PropertyName, string Value)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
