using System;
using System.Collections.Generic;
using System.Text;

namespace Apq.Data
{
	/// <summary>
	/// DataView
	/// </summary>
	public class DataView
	{
		private System.Data.DataView _View;
		/// <summary>
		/// 获取视图
		/// </summary>
		public System.Data.DataView View
		{
			get { return _View; }
		}

		/// <summary>
		/// 装饰
		/// </summary>
		public DataView(System.Data.DataView View)
		{
			_View = View;
		}

		#region 方法
		#endregion
	}
}
