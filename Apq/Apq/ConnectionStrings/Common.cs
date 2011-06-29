using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apq.Data.Common;

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
		public static string GetConnectionString(DBProduct p, string Server, int Port, string Uid, string Pwd, string dbName = "master", string Option = "")
		{
			string str = string.Empty;
			switch (p)
			{
				case DBProduct.MySql:
					str = MySql.MySqlConnection.GetConnectionString(Server, Port, Uid, Pwd, dbName);
					break;

				case DBProduct.MSSql:
				default:
					str = SQLServer.SqlConnection.GetConnectionString(Server, Port, Uid, Pwd, dbName);
					break;
			}

			if (!string.IsNullOrEmpty(Option))
			{
				str += Option;
			}
			return str;
		}
		#endregion
	}
}
