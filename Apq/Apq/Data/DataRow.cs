using System;
using System.Collections.Generic;
using System.Text;

namespace Apq.Data
{
	/// <summary>
	/// DataRow
	/// </summary>
	public class DataRow
	{
		/// <summary>
		/// 返回一行是否全与 DBNull 具有相似意义
		/// </summary>
		/// <param name="dr"></param>
		/// <returns></returns>
		public static bool LikeDBNull(System.Data.DataRow dr)
		{
			for (int i = 0; i < dr.Table.Columns.Count; i++)
			{
				if (!Apq.Convert.LikeDBNull(dr[i]))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// [反射]判断两行是否相等
		/// </summary>
		/// <param name="dr1"></param>
		/// <param name="dr2"></param>
		/// <returns></returns>
		public static bool Equals(System.Data.DataRow dr1, System.Data.DataRow dr2)
		{
			if (dr1.Table.Columns.Count != dr2.Table.Columns.Count)
			{
				return false;
			}
			for (int c = 0; c < dr1.Table.Columns.Count; c++)
			{
				System.Type tc = dr1.Table.Columns[c].DataType;
				System.Reflection.MethodInfo mi = tc.GetMethod("Equals",
					System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
				if (mi != null)
				{
					if (!System.Convert.ToBoolean(mi.Invoke(null, new object[] { dr1[c], dr2[c] })))
					{
						return false;
					}
				}
			}
			return true;
		}
	}
}
