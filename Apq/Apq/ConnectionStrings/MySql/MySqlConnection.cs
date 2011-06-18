using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apq.ConnectionStrings.MySql
{
	/// <summary>
	/// MySqlConnection 连接字符串
	/// 实例使用方法:通过属性设置各项值(IP,Uid,Pwd必选),最后调用GetConnectionString方法获取
	/// </summary>
	class MySqlConnection
	{
		#region 静态成员
		#region GetConnectionString
		/// <summary>
		/// 获取连接字符串
		/// </summary>
		/// <param name="IP">服务器地址</param>
		/// <param name="Port">端口</param>
		/// <param name="Uid">用户名</param>
		/// <param name="Pwd">密码</param>
		/// <returns></returns>
		public static string GetConnectionString(string IP, int Port, string Uid, string Pwd)
		{
			return GetConnectionString(IP, Port, Uid, Pwd, "MySql");
		}
		/// <summary>
		/// 获取连接字符串
		/// </summary>
		/// <param name="IP">服务器地址</param>
		/// <param name="Port">端口</param>
		/// <param name="Uid">用户名</param>
		/// <param name="Pwd">密码</param>
		/// <param name="dbName">默认数据库名</param>
		/// <returns></returns>
		public static string GetConnectionString(string IP, int Port, string Uid, string Pwd, string dbName)
		{
			string ConnectionStringFormat = @"Server={0};Port={1};Uid={2};Pwd={3};Database={4};";
			return string.Format(ConnectionStringFormat, IP, Port, Uid, Pwd, dbName);
		}
		/// <summary>
		/// 获取连接字符串
		/// </summary>
		/// <param name="IP">服务器地址</param>
		/// <param name="Port">端口</param>
		/// <param name="Uid">用户名</param>
		/// <param name="Pwd">密码</param>
		/// <param name="dbName">默认数据库名</param>
		/// <param name="Option">其余选项</param>
		/// <returns></returns>
		public static string GetConnectionString(string IP, int Port, string Uid, string Pwd, string dbName, string Option)
		{
			string str = GetConnectionString(IP, Port, Uid, Pwd, dbName);
			str += Option;
			return str;
		}
		#endregion
		#endregion

		#region 实例成员
		#region 连接字符串分量设置
		private string _IP = string.Empty;
		/// <summary>
		/// IP
		/// </summary>
		public string IP
		{
			get { return _IP; }
			set { _IP = value; }
		}
		private int _Port = 3306;
		/// <summary>
		/// Port
		/// </summary>
		public int Port
		{
			get { return _Port; }
			set { _Port = value; }
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
			if (IP != null && IP.Length > 0)
			{
				str += string.Format("IP={0};Port={1};", IP, Port);
			}
			str += string.Format("Uid={0};Pwd={1};", Uid, Pwd);
			if (DBName != null && DBName.Length > 0)
			{
				str += string.Format("Database={0};", DBName);
			}
			if (Option != null && Option.Length > 0)
			{
				str += Option;
			}

			return str;
		}
		#endregion
	}
}
