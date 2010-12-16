using System;
using System.Collections.Generic;
using System.Text;

namespace Apq.ConnectionStrings.SQLServer
{
	/// <summary>
	/// SqlConnection 连接字符串
	/// </summary>
	public class SqlConnection
	{
		/// <summary>
		/// 带格式的连接字符串
		/// </summary>
		private static string ConnectionStringFormat = @"Data Source={0};User Id={1};Password={2};Initial Catalog={3};";

		#region GetConnectionString
		/// <summary>
		/// 获取连接字符串
		/// </summary>
		/// <param name="dataSource">服务器地址</param>
		/// <param name="UserName">用户名</param>
		/// <param name="pwd">密码</param>
		/// <returns></returns>
		public static string GetConnectionString(string dataSource, string UserName, string pwd)
		{
			return GetConnectionString(dataSource, UserName, pwd, "master");
		}
		/// <summary>
		/// 获取连接字符串
		/// </summary>
		/// <param name="dataSource">服务器地址</param>
		/// <param name="UserName">用户名</param>
		/// <param name="pwd">密码</param>
		/// <param name="dbName">默认数据库名</param>
		/// <returns></returns>
		public static string GetConnectionString(string dataSource, string UserName, string pwd, string dbName)
		{
			return string.Format(ConnectionStringFormat, dataSource, UserName, pwd, dbName);
		}
		/// <summary>
		/// 获取连接字符串
		/// </summary>
		/// <param name="dataSource">服务器地址</param>
		/// <param name="UserName">用户名</param>
		/// <param name="pwd">密码</param>
		/// <param name="dbName">默认数据库名</param>
		/// <param name="Option">其余选项</param>
		/// <returns></returns>
		public static string GetConnectionString(string dataSource, string UserName, string pwd, string dbName, string Option)
		{
			string str = string.Format(ConnectionStringFormat, dataSource, UserName, pwd, dbName);
			str += Option;
			return str;
		}
		#endregion
	}
}
