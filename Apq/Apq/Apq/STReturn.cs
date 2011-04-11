using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Apq
{
	/// <summary>
	/// 通用返回类(结构)
	/// </summary>
	[Serializable]
	public class STReturn
	{
		/// <summary>
		/// STReturn
		/// </summary>
		public STReturn()
		{
		}

		/// <summary>
		/// 返回值(主要思想:1代表成功,-1代表失败,其余未知,也可另行自定义)默认0
		/// </summary>
		public int NReturn = 0;
		/// <summary>
		/// 返回信息(一般是非成功时使用)
		/// </summary>
		public string ExMsg = string.Empty;

		private object _FNReturn;
		/// <summary>
		/// 方法返回值
		/// </summary>
		public object FNReturn
		{
			get { return _FNReturn; }
			set { _FNReturn = value; }
		}

		private ArrayList _POuts = new ArrayList();
		/// <summary>
		/// 输出参数列表
		/// </summary>
		public ArrayList POuts
		{
			get { return _POuts; }
		}
	}
}
