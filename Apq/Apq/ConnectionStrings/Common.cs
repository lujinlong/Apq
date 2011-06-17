using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apq.ConnectionStrings
{
	/// <summary>
	/// 连接字符串公用类
	/// </summary>
	public class Common
	{
		#region GetConnectionString
		/// <summary>
		/// 获取连接字符串
		/// </summary>
		public static string GetConnectionString(DBProduct p, string Server, int Port, string Uid, string Pwd)
		{
			return GetConnectionString(p, Server, Port, Uid, Pwd, "master");
		}
		/// <summary>
		/// 获取连接字符串
		/// </summary>
		public static string GetConnectionString(DBProduct p, string Server, int Port, string Uid, string Pwd, string dbName)
		{
			switch (p)
			{
				case DBProduct.MySql:
					return MySql.MySqlConnection.GetConnectionString(Server, Port, Uid, Pwd, dbName);

				case DBProduct.MSSql:
				default:
					return SQLServer.SqlConnection.GetConnectionString(Server, Port, Uid, Pwd, dbName);
			}
		}
		/// <summary>
		/// 获取连接字符串
		/// </summary>
		public static string GetConnectionString(DBProduct p, string Server, int Port, string Uid, string Pwd, string dbName, string Option)
		{
			string str = GetConnectionString(p, Server, Port, Uid, Pwd, dbName);
			str += Option;
			return str;
		}
		#endregion
	}

	/// <summary>
	/// 数据库产品
	/// </summary>
	public enum DBProduct
	{
		/// <summary>
		/// MSSql
		/// </summary>
		MSSql = 1,
		/// <summary>
		/// MySql
		/// </summary>
		MySql = 2,
	}
}
