using System;
using System.Collections.Generic;
using System.Text;

namespace Apq
{
	/// <summary>
	/// Exception
	/// </summary>
	public class Exception
	{
		#region 静态
		/// <summary>
		/// 获取原始异常
		/// </summary>
		/// <param name="ex"></param>
		/// <returns></returns>
		public static System.Exception GetOriginalException(System.Exception ex)
		{
			while (ex.InnerException != null)
			{
				ex = ex.InnerException;
			}
			return ex;
		}
		#endregion

		/// <summary>
		/// Exception
		/// </summary>
		/// <param name="ex"></param>
		public Exception(System.Exception ex)
		{
			_Ex = ex;
		}

		private System.Exception _Ex;
		/// <summary>
		/// 获取异常对象
		/// </summary>
		public System.Exception Ex
		{
			get { return _Ex; }
		}

		/// <summary>
		/// 获取或设置异常级别
		/// </summary>
		public int Level
		{
			get { return Apq.Convert.ChangeType<int>(OriginalException.Data["Level"]); }
			set { OriginalException.Data["Level"] = value; }
		}

		private System.Exception _OriginalException;
		/// <summary>
		/// 获取原始异常
		/// </summary>
		/// <returns></returns>
		public System.Exception OriginalException
		{
			get
			{
				if (_OriginalException == null)
				{
					_OriginalException = GetOriginalException(Ex);
				}
				return _OriginalException;
			}
		}
	}
}
