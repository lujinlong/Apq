using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apq.Windows.Controls
{
	/// <summary>
	/// Control 扩展方法
	/// 在扩展方法中, Tag 统一扩展成 Dictionary&lt;string, object&gt;,作为数据缓存使用
	/// </summary>
	public static class ControlExtension
	{
		/// <summary>
		/// 获取Control数据(Control.Tag 是 Dictionary&lt;string, object&gt;)
		/// </summary>
		public static object GetControlValues(this System.Windows.Forms.Control Ctrl, string Name)
		{
			if (Ctrl.Tag == null)
			{
				Ctrl.Tag = new Dictionary<string, object>();
			}

			Dictionary<string, object> NamedValues = Ctrl.Tag as Dictionary<string, object>;
			if (NamedValues != null && NamedValues.ContainsKey(Name))
			{
				return NamedValues[Name];
			}
			return null;
		}

		/// <summary>
		/// 设置Control数据(Control.Tag 是 Dictionary&lt;string, object&gt;)
		/// </summary>
		public static void SetControlValues(this System.Windows.Forms.Control Ctrl, string Name, object Value)
		{
			if (Ctrl.Tag == null)
			{
				Ctrl.Tag = new Dictionary<string, object>();
			}

			Dictionary<string, object> NamedValues = Ctrl.Tag as Dictionary<string, object>;
			if (NamedValues != null)
			{
				if (NamedValues.ContainsKey(Name))
				{
					NamedValues[Name] = Value;
				}
				else
				{
					NamedValues.Add(Name, Value);
				}
			}
		}
	}
}
