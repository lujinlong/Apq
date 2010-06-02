using System;
using System.Collections.Generic;
using System.Text;

namespace Apq.ConnectionStrings.Textfile
{
	/// <summary>
	/// OLEDB 连接字符串
	/// </summary>
	public class OleDbConnection
	{
		/// <summary>
		/// 带格式的连接字符串
		/// </summary>
		private static string ConnectionStringFormat = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""text;HDR={1};FMT={2}""";

		#region GetConnectionString
		/// <summary>
		/// Excel 带格式的连接字符串
		/// </summary>
		/// <summary>
		/// 获取连接字符串
		/// </summary>
		/// <param name="dataSource"></param>
		/// <returns></returns>
		public static string GetConnectionString(string dataSource)
		{
			return GetConnectionString(dataSource, "Yes", "Delimited(,)");
		}
		/// <summary>
		/// 获取连接字符串
		/// </summary>
		/// <param name="dataSource"></param>
		/// <param name="HDR">首行是否为列名["Yes"]</param>
		/// <param name="FMT">指定分隔符或指明文本等宽{Fixed:等宽,TabDelimited:Tab分隔,Delimited(,):以括号内容分隔}</param>
		/// <returns></returns>
		public static string GetConnectionString(string dataSource, string HDR, string FMT)
		{
			return string.Format(ConnectionStringFormat, dataSource, HDR, FMT);
		}
		#endregion
	}
}
