using System;
using System.Collections.Generic;
using System.Text;

namespace Apq.ConnectionStrings.Access
{
	/// <summary>
	/// OLEDB 连接字符串
	/// </summary>
	public class OleDbConnection
	{
		/// <summary>
		/// 带格式的连接字符串
		/// </summary>
		private static string ConnectionStringFormat = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};User Id={1};Password={2};{3}";
		/// <summary>
		/// 文件密码部分
		/// </summary>
		private static string ConnectionStringDBPwdFormat = @"Jet OLEDB:Database Password={0};";

		#region GetConnectionString
		/// <summary>
		/// 获取连接字符串
		/// </summary>
		/// <param name="dataSource">文件地址</param>
		/// <param name="UserName">用户名</param>
		/// <param name="pwd">用户密码</param>
		/// <returns></returns>
		public static string GetConnectionString(string dataSource, string UserName, string pwd)
		{
			return GetConnectionString(dataSource, UserName, pwd, string.Empty);
		}
		/// <summary>
		/// 获取连接字符串
		/// </summary>
		/// <param name="dataSource">文件地址</param>
		/// <param name="UserName">用户名</param>
		/// <param name="pwd">用户密码</param>
		/// <param name="dbpwd">文件密码</param>
		/// <returns></returns>
		public static string GetConnectionString(string dataSource, string UserName, string pwd, string dbpwd)
		{
			string strDBPwd = string.IsNullOrEmpty(dbpwd) ? string.Empty : string.Format(ConnectionStringDBPwdFormat, dbpwd);
			return string.Format(ConnectionStringFormat, dataSource, UserName, pwd, strDBPwd);
		}
		#endregion
	}
}
