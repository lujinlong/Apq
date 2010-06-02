using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Apq
{
	/// <summary>
	/// 代理及其示例方法
	/// </summary>
	public static class Delegates
	{
		#region 属性设置
		/// <summary>
		/// 设置 obj 的 PropertyName 属性值为 Value
		/// </summary>
		/// <param name="obj">对象</param>
		/// <param name="PropertyName">属性名</param>
		/// <param name="Value">值</param>
		public delegate void SetProperty(object obj, string PropertyName, object Value);

		/// <summary>
		/// [反射]设置对象属性
		/// </summary>
		/// <param name="obj">对象</param>
		/// <param name="PropertyName">属性名</param>
		/// <param name="Value">值</param>
		public static void SetProperty_M(object obj, string PropertyName, object Value)
		{
			Type t = obj.GetType();
			FieldInfo fi = t.GetField(PropertyName, System.Reflection.BindingFlags.SetField);
			if (fi != null)
			{
				fi.SetValue(obj, Value);
			}
			PropertyInfo pi = t.GetProperty(PropertyName, System.Reflection.BindingFlags.SetProperty);
			if (pi != null)
			{
				pi.SetValue(obj, Value, null);
			}
		}
		#endregion
	}
}
