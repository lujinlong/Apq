using System;
using System.Collections.Generic;
using System.Text;

namespace Apq.Data.Common
{
	/// <summary>
	/// DbParameterCollectionHelper
	/// </summary>
	public sealed class DbParameterCollectionHelper
	{
		private System.Data.Common.DbParameterCollection _Parameters;
		/// <summary>
		/// 获取参数集合
		/// </summary>
		public System.Data.Common.DbParameterCollection Parameters
		{
			get { return _Parameters; }
		}

		/// <summary>
		/// DbParameterCollectionHelper
		/// </summary>
		/// <param name="Parameters"></param>
		public DbParameterCollectionHelper(System.Data.Common.DbParameterCollection Parameters)
		{
			_Parameters = Parameters;
		}

		/// <summary>
		/// 获取或设置指定参数名的参数
		/// </summary>
		/// <param name="ParameterName">参数名</param>
		/// <returns></returns>
		public System.Data.Common.DbParameter this[string ParameterName]
		{
			get { return _Parameters[ParameterName]; }
			set
			{
				if (_Parameters.Contains(ParameterName))
				{
					_Parameters.Remove(_Parameters[ParameterName]);
				}
				_Parameters.Add(value);
			}
		}
	}
}