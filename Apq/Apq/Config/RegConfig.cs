using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Microsoft.Win32;

namespace Apq.Config
{
	/// <summary>
	/// 注册表配置
	/// </summary>
	public partial class RegConfig : clsConfig
	{
		private RegistryKey rk;		// 注册表基项
		private RegistryKey rkRoot;	// 子项

		/// <summary>
		/// 获取或设置[反射]注册表基项
		/// <remarks>可选值:"$reg$" + {ClassesRoot, CurrentConfig, CurrentUser, DynData, LocalMachine, PerformanceData, Users}</remarks>
		/// </summary>
		public override string Path
		{
			get { return base.Path; }
			set
			{
				rk = null;
				rkRoot = null;

				if (!value.StartsWith("$reg$"))
				{
					throw new ArgumentOutOfRangeException("Path", value, @"可选值为:
{
	 $reg$ClassesRoot
	,$reg$CurrentConfig
	,$reg$CurrentUser
	,$reg$DynData
	,$reg$LocalMachine
	,$reg$PerformanceData
	,$reg$Users
}
");
				}

				string str = value.Substring(5);
				Type t = typeof(Registry);
				FieldInfo fi = t.GetField(str);
				if (fi != null)
				{
					rk = fi.GetValue(null) as RegistryKey;
				}

				base.Path = value;
			}
		}
		/// <summary>
		/// 获取或设置注册表子项的名称或路径
		/// </summary>
		public override string Root
		{
			get
			{
				return base.Root;
			}
			set
			{
				if (rk == null)
				{
					throw new System.Exception("请先设置 Path。");
				}

				rkRoot = rk.CreateSubKey(value, RegistryKeyPermissionCheck.ReadWriteSubTree);

				base.Root = value;
			}
		}

		/// <summary>
		/// 获取配置名列表
		/// </summary>
		/// <returns></returns>
		public override string[] GetValueNames()
		{
			if (rkRoot != null)
			{
				return rkRoot.GetValueNames();
			}
			return null;
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
				if (rkRoot != null)
				{
					RegistryKey rk1 = rkRoot.OpenSubKey(ClassName);
					return rk1.GetValue(PropertyName).ToString();
				}
				return null;
			}
			set
			{
				if (rkRoot == null)
				{
					throw new System.Exception("请先设置 Root。");
				}

				RegistryKey rk1 = rkRoot.CreateSubKey(ClassName, RegistryKeyPermissionCheck.ReadWriteSubTree);
				rk1.SetValue(PropertyName, value);
			}
		}
	}
}
