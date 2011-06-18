using System;
using System.Collections.Generic;
using System.Text;

namespace Apq.ConnectionStrings.SQLServer
{
	/// <summary>
	/// SqlConnection 连接字符串
	/// 实例使用方法:通过属性设置各项值(IP,Port,UserId,Pwd必选),最后调用GetConnectionString方法获取
	/// </summary>
	public class SqlConnection
	{
		#region 静态成员
		#region GetConnectionString
		/// <summary>
		/// 获取连接字符串
		/// </summary>
		/// <param name="IP">服务器地址</param>
		/// <param name="Port">端口</param>
		/// <param name="UserId">用户名</param>
		/// <param name="Pwd">密码</param>
		/// <returns></returns>
		public static string GetConnectionString(string IP, int Port, string UserId, string Pwd)
		{
			return GetConnectionString(IP, Port, UserId, Pwd, "master");
		}
		/// <summary>
		/// 获取连接字符串
		/// </summary>
		/// <param name="IP">服务器地址</param>
		/// <param name="Port">端口</param>
		/// <param name="UserId">用户名</param>
		/// <param name="Pwd">密码</param>
		/// <param name="dbName">默认数据库名</param>
		/// <returns></returns>
		public static string GetConnectionString(string IP, int Port, string UserId, string Pwd, string dbName)
		{
			string ConnectionStringFormat = @"Server={0},{1};User Id={2};Password={3};Database={4};";
			return string.Format(ConnectionStringFormat, IP, Port, UserId, Pwd, dbName);
		}
		/// <summary>
		/// 获取连接字符串
		/// </summary>
		/// <param name="IP">服务器地址</param>
		/// <param name="Port">端口</param>
		/// <param name="UserId">用户名</param>
		/// <param name="Pwd">密码</param>
		/// <param name="dbName">默认数据库名</param>
		/// <param name="Option">其余选项</param>
		/// <returns></returns>
		public static string GetConnectionString(string IP, int Port, string UserId, string Pwd, string dbName, string Option)
		{
			string str = GetConnectionString(IP, Port, UserId, Pwd, dbName);
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
		private int _Port = 1433;
		/// <summary>
		/// Port
		/// </summary>
		public int Port
		{
			get { return _Port; }
			set { _Port = value; }
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
		/// 镜像IP
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
			if (IP != null && IP.Length > 0)
			{
				str += string.Format("Server={0},{1};", IP, Port);
			}
			if (UseTrusted)
			{
				str += "Integrated Security=SSPI;";
			}
			else
			{
				str += string.Format("User Id={0};Password={1};", UserId, Pwd);
			}
			if (DBName != null && DBName.Length > 0)
			{
				str += string.Format("Database={0};", DBName);
			}
			if (Mirror != null && Mirror.Length > 0)
			{
				str += string.Format("Failover Partner={0};", Mirror);
			}
			if (Option != null && Option.Length > 0)
			{
				str += Option;
			}

			return str;
		}

		/// <summary>
		/// 测试连接可用性
		/// </summary>
		/// <returns>成功返回true,否则抛出异常</returns>
		public bool TestAvailable()
		{
			string strCs = this.GetConnectionString();
			System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection(strCs);
			try
			{
				sc.Open();
				return true;
			}
			finally
			{
				Apq.Data.Common.DbConnectionHelper.Close(sc);
			}
		}
		#endregion
	}
}
