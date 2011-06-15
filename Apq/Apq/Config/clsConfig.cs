using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.Collections;

namespace Apq.Config
{
	/// <summary>
	/// 配置
	/// </summary>
	public abstract class clsConfig : MarshalByRefObject
	{
		#region 设置
		/// <summary>
		/// 文件路径
		/// </summary>
		protected string _Path;
		/// <summary>
		/// 获取或设置文件路径
		/// </summary>
		public virtual string Path
		{
			get { return _Path; }
			set { _Path = value; }
		}
		/// <summary>
		/// 根
		/// </summary>
		protected string _Root = "Root";	// 默认值
		/// <summary>
		/// 获取或设置根
		/// </summary>
		public virtual string Root
		{
			get { return _Root; }
			set { _Root = value; }
		}
		#endregion

		#region 打开,保存,关闭
		/// <summary>
		/// 打开配置
		/// 提供生成接口,用于配置池
		/// </summary>
		/// <param name="Path"></param>
		public void Open(string Path)
		{
			Open(Path, Root);
		}
		/// <summary>
		/// 打开配置
		/// 提供生成接口,用于配置池
		/// </summary>
		/// <param name="Path"></param>
		/// <param name="Root"></param>
		public void Open(string Path, string Root)
		{
			this.Path = Path;
			this.Root = Root;
		}
		/// <summary>
		/// 保存配置
		/// </summary>
		public virtual void Save()
		{
		}
		/// <summary>
		/// 另存为
		/// </summary>
		public virtual void SaveAs(string Path)
		{
		}
		/// <summary>
		/// 关闭配置
		/// </summary>
		public virtual void Close()
		{
		}
		#endregion

		#region 获取或设置配置
		/// <summary>
		/// 获取配置名列表
		/// </summary>
		/// <returns></returns>
		public abstract string[] GetValueNames();

		/// <summary>
		/// 获取或设置配置[推荐使用]
		/// </summary>
		/// <param name="ClassName">类名</param>
		/// <param name="PropertyName">属性名</param>
		/// <returns></returns>
		public abstract string this[string ClassName, string PropertyName] { get; set; }

		/// <summary>
		/// 获取配置值
		/// </summary>
		/// <param name="ClassName">类名</param>
		/// <param name="PropertyName">属性名</param>
		/// <returns>配置值</returns>
		public virtual string GetValue(string ClassName, string PropertyName)
		{
			return this[ClassName, PropertyName];
		}

		/// <summary>
		/// 设置配置值
		/// </summary>
		/// <param name="ClassName">类名</param>
		/// <param name="PropertyName">属性名</param>
		/// <param name="Value">配置值</param>
		public virtual void SetValue(string ClassName, string PropertyName, string Value)
		{
			this[ClassName, PropertyName] = Value;
		}

		/// <summary>
		/// 获取或设置配置
		/// </summary>
		/// <param name="t">类</param>
		/// <param name="PropertyName">属性名</param>
		/// <returns></returns>
		public virtual string this[Type t, string PropertyName]
		{
			get { return this[t.FullName, PropertyName]; }
			set { this[t.FullName, PropertyName] = value; }
		}

		/// <summary>
		/// 获取配置值
		/// </summary>
		/// <param name="t">类</param>
		/// <param name="PropertyName">属性名</param>
		/// <returns>配置值</returns>
		public virtual string GetValue(Type t, string PropertyName)
		{
			return this[t, PropertyName];
		}

		/// <summary>
		/// 设置配置值
		/// </summary>
		/// <param name="t">类</param>
		/// <param name="PropertyName">属性名</param>
		/// <param name="Value">配置值</param>
		public virtual void SetValue(Type t, string PropertyName, string Value)
		{
			this[t, PropertyName] = Value;
		}

		/// <summary>
		/// 获取或设置当前类的配置
		/// </summary>
		/// <param name="PropertyName">属性名</param>
		/// <returns></returns>
		public virtual string this[string PropertyName]
		{
			get
			{
				try
				{
					string strCallingClassName = Apq.Common.GetCallingClass().FullName;
					return this[strCallingClassName, PropertyName];
				}
				catch (System.Exception ex)	// 一般是不可能捕获到异常的,防止编译器优化
				{
					throw (ex);
				}
			}
			set
			{
				try
				{
					string strCallingClassName = Apq.Common.GetCallingClass().FullName;
					this[strCallingClassName, PropertyName] = value;
				}
				catch (System.Exception ex)	// 一般是不可能捕获到异常的,防止编译器优化
				{
					throw (ex);
				}
			}
		}

		/// <summary>
		/// 获取当前类的配置值
		/// </summary>
		/// <param name="PropertyName">属性名</param>
		/// <returns>配置值</returns>
		public virtual string GetValue(string PropertyName)
		{
			try
			{
				string strCallingClassName = Apq.Common.GetCallingClass().FullName;
				return this[strCallingClassName, PropertyName];
			}
			catch (System.Exception ex)	// 一般是不可能捕获到异常的,防止编译器优化
			{
				throw (ex);
			}
		}

		/// <summary>
		/// 设置当前类的配置值
		/// </summary>
		/// <param name="PropertyName">属性名</param>
		/// <param name="Value">配置值</param>
		public virtual void SetValue(string PropertyName, string Value)
		{
			try
			{
				string strCallingClassName = Apq.Common.GetCallingClass().FullName;
				this[strCallingClassName, PropertyName] = Value;
			}
			catch (System.Exception ex)	// 一般是不可能捕获到异常的,防止编译器优化
			{
				throw (ex);
			}
		}
		#endregion
	}
}
