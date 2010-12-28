using System;
using System.Collections.Generic;
using System.Text;

namespace Apq.ConnectionStrings.SQLServer
{
	/// <summary>
	/// SqlConnection 连接字符串
	/// 实例使用方法:通过属性设置各项值(ServerName,DBName必选),最后调用GetConnectionString方法获取
	/// </summary>
	public class SqlConnection
	{
		#region 静态成员
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
		#endregion

		#region 实例成员
		#region 连接字符串分量设置
		private string _ServerName = string.Empty;
		/// <summary>
		/// ServerName
		/// </summary>
		public string ServerName
		{
			get { return _ServerName; }
			set { _ServerName = value; }
		}
		private string _UserId = string.Empty;
		/// <summary>
		/// UserId
		/// </summary>
		public string UserId
		{
			get { return _UserId; }
			set { _UserId = value; }
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
		private string _Mirror = string.Empty;
		/// <summary>
		/// 镜像ServerName
		/// </summary>
		public string Mirror
		{
			get { return _Mirror; }
			set { _Mirror = value; }
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
		private bool _UseTrusted = false;
		/// <summary>
		/// 使用信任连接(winodws登录)
		/// </summary>
		public bool UseTrusted
		{
			get { return _UseTrusted; }
			set { _UseTrusted = value; }
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
			if (ServerName != null && ServerName.Length > 0)
			{
				str += string.Format("Data Source={0};", ServerName);
			}
			if (DBName != null && DBName.Length > 0)
			{
				str += string.Format("Initial Catalog={0};", DBName);
			}
			if (Mirror != null && Mirror.Length > 0)
			{
				str += string.Format("Failover Partner={0};", Mirror);
			}
			if (UseTrusted)
			{
				str += "Integrated Security=SSPI;";
			}
			else
			{
				str += string.Format("User Id={0};Password={1};", UserId, Pwd);
			}
			if (Option != null && Option.Length > 0) str += Option;

			return str;
		}
		#endregion
	}
}
