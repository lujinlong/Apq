using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Reflection;

namespace Apq
{
	/// <summary>
	/// SingletonInstances
	/// </summary>
	public partial class SingletonInstances
	{
		/// <summary>
		/// 获取单态实例
		/// </summary>
		/// <param name="t">实例类型</param>
		/// <returns></returns>
		public static object GetInstance(Type t)
		{
			if (!Apq.GlobalObject.SingletonInstances.ContainsKey(t.FullName))
			{
				object obj = System.Activator.CreateInstance(t);
				Apq.GlobalObject.SingletonInstances.Add(t.FullName, obj);
			}
			return Apq.GlobalObject.SingletonInstances[t.FullName];
		}

		/// <summary>
		/// 释放单态实例
		/// </summary>
		/// <param name="t">实例类型</param>
		public static void ReleaseInstance(Type t)
		{
			Apq.GlobalObject.SingletonInstances.Remove(t.FullName);
		}
	}
}