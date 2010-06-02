using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace Apq.Windows
{
	/// <summary>
	/// 代理及其示例方法
	/// </summary>
	public static class Delegates
	{
		#region 属性设置
		/// <summary>
		/// 设置 ctrl 的 PropertyName 属性值为 Value
		/// </summary>
		/// <param name="ctrl">控件</param>
		/// <param name="PropertyName">属性名</param>
		/// <param name="Value">值</param>
		public delegate void SetProperty(ISynchronizeInvoke ctrl, string PropertyName, object Value);

		/// <summary>
		/// [UI线程]设置对象属性
		/// </summary>
		/// <param name="ctrl">控件</param>
		/// <param name="PropertyName">属性名</param>
		/// <param name="Value">值</param>
		public static void SetProperty_UI(ISynchronizeInvoke ctrl, string PropertyName, object Value)
		{
			if (ctrl.InvokeRequired)
			{
				object[] Params = { ctrl, PropertyName, Value };
				ctrl.Invoke(new SetProperty(SetProperty_UI), Params);
			}
			else
			{
				Apq.Delegates.SetProperty_M(ctrl, PropertyName, Value);
				System.Windows.Forms.Application.DoEvents();
			}
		}
		#endregion

		#region System.Action<T>
		/// <summary>
		/// [UI线程]必要时在指定控件的创建线程调用代理
		/// </summary>
		/// <typeparam name="T">控件</typeparam>
		/// <param name="ctrl"></param>
		/// <param name="obj"></param>
		/// <param name="a"></param>
		public static void Action_UI<T>(ISynchronizeInvoke ctrl, T obj, System.Action<T> a)
		{
			if (ctrl.InvokeRequired)
			{
				object[] Params = { obj };
				ctrl.Invoke(a, Params);
			}
			else
			{
				a(obj);
			}

			System.Windows.Forms.Application.DoEvents();
		}
		#endregion
	}
}
