using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Apq.Windows.Forms
{
	/// <summary>
	/// SingletonForms
	/// </summary>
	public partial class SingletonForms
	{
		/// <summary>
		/// 获取单态窗体
		/// </summary>
		/// <param name="t">窗体类型</param>
		/// <returns></returns>
		public static Form GetInstance(Type t)
		{
			Form form1 = (Form)Apq.SingletonInstances.GetInstance(t);
			form1.Disposed += new EventHandler(form1_Disposed);
			return form1;
		}

		/// <summary>
		/// 释放单态窗体
		/// </summary>
		/// <param name="t">窗体类型</param>
		public static void ReleaseInstance(Type t)
		{
			Apq.SingletonInstances.ReleaseInstance(t);
		}

		private static void form1_Disposed(object sender, EventArgs e)
		{
			ReleaseInstance(sender.GetType());
		}
	}
}