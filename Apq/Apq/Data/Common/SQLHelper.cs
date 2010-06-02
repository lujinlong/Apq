using System;
using System.Collections.Generic;
using System.Text;

namespace Apq.Data.Common
{
	/// <summary>
	/// 辅助生成 SQL 语句
	/// </summary>
	public class SQLHelper
	{
		#region BuildSegment

		/// <summary>
		/// 构建 VALUES 片段
		/// </summary>
		/// <param name="vals">常量列表(不带括号)</param>
		/// <returns></returns>
		public static string BuildValuesSegment(string vals)
		{
			return string.Format("\r\nVALUES({0})", vals);
		}

		/// <summary>
		/// 构建 INSERT 片段
		/// </summary>
		/// <param name="TabeName">表(别)名</param>
		/// <param name="ColNames">列名</param>
		/// <returns></returns>
		public static string BuildInsertSegment(string TabeName, string ColNames)
		{
			return BuildInsertSegment(TabeName, ColNames, false);
		}
		/// <summary>
		/// 构建 INSERT 片段
		/// </summary>
		/// <param name="TabeName">表(别)名</param>
		/// <param name="ColNames">列名</param>
		/// <param name="ContainsInto">是否包含 INTO 在 INSERT 后面</param>
		/// <returns></returns>
		public static string BuildInsertSegment(string TabeName, string ColNames, bool ContainsInto)
		{
			string strInto = ContainsInto ? "INTO" : string.Empty;
			return string.Format("\r\nINSERT {2} {0}({1})", TabeName, ColNames, strInto);
		}

		/// <summary>
		/// 构建 SELECT 片段
		/// </summary>
		/// <param name="ColNames">列名/常量</param>
		/// <returns></returns>
		public static string BuildSelectSegment(string ColNames)
		{
			return string.Format("\r\nSELECT {0}", ColNames);
		}

		/// <summary>
		/// 构建 FROM 片段
		/// </summary>
		/// <param name="TabeName">表名 / [子查询+表别名]</param>
		/// <returns></returns>
		public static string BuildFromSegment(string TabeName)
		{
			return string.Format("\r\n  FROM {0}", TabeName);
		}

		/// <summary>
		/// 构建 WHERE 片段
		/// </summary>
		/// <param name="Conditions">条件</param>
		/// <returns></returns>
		public static string BuildWhereSegment(string Conditions)
		{
			return string.Format("\r\n WHERE {0}", Conditions);
		}

		/// <summary>
		/// 构建 GROUP 片段
		/// </summary>
		/// <param name="ColNames">列名/分组列</param>
		/// <returns></returns>
		public static string BuildGroupSegment(string ColNames)
		{
			return string.Format("\r\n GROUP BY {0}", ColNames);
		}

		/// <summary>
		/// 构建 HAVING 片段
		/// </summary>
		/// <param name="Conditions">条件</param>
		/// <returns></returns>
		public static string BuildHavingSegment(string Conditions)
		{
			return string.Format("\r\nHAVING {0}", Conditions);
		}

		/// <summary>
		/// 构建 ORDER 片段
		/// </summary>
		/// <param name="ColNames">列名/分组列</param>
		/// <returns></returns>
		public static string BuildOrderSegment(string ColNames)
		{
			return string.Format("\r\n ORDER BY {0}", ColNames);
		}

		/// <summary>
		/// 构建 UPDATE 片段
		/// </summary>
		/// <param name="TabeName">表(别)名</param>
		/// <returns></returns>
		public static string BuildUpdateSegment(string TabeName)
		{
			return string.Format("\r\nUPDATE {0}", TabeName);
		}

		/// <summary>
		/// 构建 SET 片段
		/// </summary>
		/// <param name="Cols">对列赋值串</param>
		/// <returns></returns>
		public static string BuildSetSegment(string Cols)
		{
			return string.Format("\r\n   SET {0}", Cols);
		}
		#endregion
	}
}
