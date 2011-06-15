using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apq.ConnectionStrings.MySql
{
	/// <summary>
	/// MySqlConnection 连接字符串
	/// 实例使用方法:通过属性设置各项值(Server,Uid,Pwd必选),最后调用GetConnectionString方法获取
	/// </summary>
	class MySqlConnection
	{
		#region 静态成员
		/// <summary>
		/// 带格式的连接字符串
		/// </summary>
		private static string ConnectionStringFormat = @"Server={0};Uid={1};Pwd={2};Database={3};";

		#region GetConnectionString
		/// <summary>
		/// 获取连接字符串
		/// </summary>
		/// <param name="Server">服务器地址</param>
		/// <param name="Uid">用户名</param>
		/// <param name="Pwd">密码</param>
		/// <returns></returns>
		public static string GetConnectionString(string Server, string Uid, string Pwd)
		{
			return GetConnectionString(Server, Uid, Pwd, "master");
		}
		/// <summary>
		/// 获取连接字符串
		/// </summary>
		/// <param name="Server">服务器地址</param>
		/// <param name="Uid">用户名</param>
		/// <param name="Pwd">密码</param>
		/// <param name="dbName">默认数据库名</param>
		/// <returns></returns>
		public static string GetConnectionString(string Server, string Uid, string Pwd, string dbName)
		{
			return string.Format(ConnectionStringFormat, Server, Uid, Pwd, dbName);
		}
		/// <summary>
		/// 获取连接字符串
		/// </summary>
		/// <param name="Server">服务器地址</param>
		/// <param name="Uid">用户名</param>
		/// <param name="Pwd">密码</param>
		/// <param name="dbName">默认数据库名</param>
		/// <param name="Option">其余选项</param>
		/// <returns></returns>
		public static string GetConnectionString(string Server, string Uid, string Pwd, string dbName, string Option)
		{
			string str = string.Format(ConnectionStringFormat, Server, Uid, Pwd, dbName);
			str += Option;
			return str;
		}
		#endregion
		#endregion

		#region 实例成员
		#region 连接字符串分量设置
		private string _Server = string.Empty;
		/// <summary>
		/// Server
		/// </summary>
		public string Server
		{
			get { return _Server; }
			set { _Server = value; }
		}
		private string _Uid = string.Empty;
		/// <summary>
		/// Uid
		/// </summary>
		public string Uid
		{
			get { return _Uid; }
			set { _Uid = value; }
		}
		private string _DBName = string.Empty;
		/// <summary>
		/// DBName
		/// </summary>
		public string DBName
		{
			get { return _DBName; }
			set { _DBName = value; }
		}
		private string _Pwd = string.Empty;
		/// <summary>
		/// 密码
		/// </summary>
		public string Pwd
		{
			get { return _Pwd; }
			set { _Pwd = value; }
		}
		private string _Option = string.Empty;
		/// <summary>
		/// 其余选项(直接添加到连接字符串后面)
		/// </summary>
		public string Option
		{
			get { return _Option; }
			set { _Option = value; }
		}
		#endregion

		/// <summary>
		/// 获取连接字符串
		/// </summary>
		/// <returns></returns>
		public string GetConnectionString()
		{
			string str = string.Empty;
			if (Server != null && Server.Length > 0)
			{
				str += string.Format("Server={0};", Server);
			}
			if (DBName != null && DBName.Length > 0)
			{
				str += string.Format("Database={0};", DBName);
			}
			str += string.Format("Uid={0};Pwd={1};", Uid, Pwd);
			if (Option != null && Option.Length > 0) str += Option;

			return str;
		}
		#endregion
	}
}
