using System;
using System.Collections.Generic;
using System.Text;

namespace Apq.ConnectionStrings.Excel
{
	/// <summary>
	/// OLEDB 连接字符串
	/// </summary>
	public class OleDbConnection
	{
		/// <summary>
		/// 带格式的连接字符串
		/// </summary>
		private static string ConnectionStringFormat = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR={1};IMEX={2}""";

		#region GetConnectionString
		/// <summary>
		/// 获取连接字符串
		/// </summary>
		/// <param name="dataSource"></param>
		/// <returns></returns>
		public static string GetConnectionString(string dataSource)
		{
			return GetConnectionString(dataSource, "Yes", "1");
		}
		/// <summary>
		/// 获取连接字符串
		/// </summary>
		/// <param name="dataSource"></param>
		/// <param name="HDR">首行是否为列名["Yes"]</param>
		/// <param name="IMEX">是否全以文本格式读取["1"]</param>
		/// <returns></returns>
		public static string GetConnectionString(string dataSource, string HDR, string IMEX)
		{
			return string.Format(ConnectionStringFormat, dataSource, HDR, IMEX);
		}
		#endregion
	}
}
