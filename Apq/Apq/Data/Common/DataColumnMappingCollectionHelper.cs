using System;
using System.Collections.Generic;
using System.Text;

namespace Apq.Data.Common
{
	/// <summary>
	/// 
	/// </summary>
	public class DataColumnMappingCollectionHelper
	{
		/// <summary>
		/// 获取 DataSet 列名集合
		/// </summary>
		/// <param name="dcmc"></param>
		/// <returns></returns>
		public static string[] GetDataSetColNames(System.Data.Common.DataColumnMappingCollection dcmc)
		{
			string[] rtn = new string[dcmc.Count];
			for (int i = 0; i < rtn.Length; i++)
			{
				rtn[i] = dcmc[i].DataSetColumn;
			}
			return rtn;
		}
		/// <summary>
		/// 获取 Source 列名集合
		/// </summary>
		/// <param name="dcmc"></param>
		/// <returns></returns>
		public static string[] GetSourceColNames(System.Data.Common.DataColumnMappingCollection dcmc)
		{
			string[] rtn = new string[dcmc.Count];
			for (int i = 0; i < rtn.Length; i++)
			{
				rtn[i] = dcmc[i].SourceColumn;
			}
			return rtn;
		}
	}
}
