using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Data;

namespace Apq.DBC
{
	/// <summary>
	/// Apq.DBC.Common
	/// </summary>
	public static class Common
	{
		private static FileSystemWatcher fsw = new FileSystemWatcher();

		private static XSD _xsd = null;

		static Common()
		{
			_xsd = new XSD();
			_xsd.SqlInstance.Columns.Add("DBConnectionString");
			_xsd.DBC.Columns.Add("DBConnectionString");

			fsw.Changed += new FileSystemEventHandler(fsw_Changed);

			// 准备完成,读取cs.res文件
			ReadFile();
		}

		// 文件改变自动刷新内存
		private static void fsw_Changed(object sender, FileSystemEventArgs e)
		{
			ReadFile();
		}

		private static void ReadFile()
		{
			// 从.NET配置文件读取cs.res文件路径
			string _csFilePath = ConfigurationManager.AppSettings["Apq.DBC.csFile"] ?? @"D:\DBA\cs\cs.res";
			string strFolder = Path.GetDirectoryName(_csFilePath);
			string strFileName = Path.GetFileName(_csFilePath);
			if (fsw.Path != strFolder)
			{
				fsw.Path = strFolder;
				fsw.EnableRaisingEvents = true;
			}
			if (fsw.Filter != strFileName)
			{
				fsw.Filter = strFileName;
			}

			#region 读取密钥
			//string desKey = GlobalObject.XmlConfigChain[typeof(Apq.DBC.Common), "DESKey"];
			//string desIV = GlobalObject.XmlConfigChain[typeof(Apq.DBC.Common), "DESIV"];

			// UM
			//string desKey = "~JD7(1vy";
			//string desIV = "]$ik7WB)";

			// 雪羽
			string desKey = "pD?y/Mn^";
			string desIV = "$`5iNL8j";
			#endregion

			string strCs = File.ReadAllText(_csFilePath, Encoding.UTF8);
			string str = Apq.Security.Cryptography.DESHelper.DecryptString(strCs, desKey, desIV);
			StringReader sr = new StringReader(str);
			_xsd.Clear();
			_xsd.ReadXml(sr);

			// 读取完成,计算所有连接字符串
			foreach (XSD.SqlInstanceRow dr in _xsd.SqlInstance.Rows)
			{
				Apq.ConnectionStrings.SQLServer.SqlConnection sc = new Apq.ConnectionStrings.SQLServer.SqlConnection();
				sc.ServerName = dr.IP;
				if (dr.SqlPort > 0)
				{
					sc.ServerName += "," + dr.SqlPort;
				}
				sc.DBName = "master";
				sc.UserId = dr.UserId;
				sc.Pwd = dr.PwdD;
				dr["DBConnectionString"] = sc.GetConnectionString();
			}
			foreach (XSD.DBCRow dr in _xsd.DBC.Rows)
			{
				Apq.ConnectionStrings.SQLServer.SqlConnection sc = new Apq.ConnectionStrings.SQLServer.SqlConnection();
				XSD.SqlInstanceRow sqlInstance = _xsd.SqlInstance.FindBySqlID(dr.SqlID);
				sc.ServerName = sqlInstance.IP;
				if (sqlInstance.SqlPort > 0)
				{
					sc.ServerName += "," + sqlInstance.SqlPort;
				}
				sc.DBName = dr.DBName;
				sc.Mirror = dr.Mirror;
				sc.UseTrusted = dr.UseTrusted;
				sc.UserId = dr.UserId;
				sc.Pwd = dr.PwdD;
				sc.Option = dr.Option;
				dr["DBConnectionString"] = sc.GetConnectionString();
			}
		}

		#region 获取连接字符串
		/// <summary>
		/// 获取数据库连接字符串
		/// </summary>
		/// <returns></returns>
		[Obsolete("已过时,请使用GetDBConnectoinString")]
		public static string GetConnectoinString(string DBName)
		{
			return GetDBConnectoinString(DBName);
		}

		/// <summary>
		/// 获取数据库连接字符串
		/// </summary>
		/// <returns></returns>
		public static string GetDBConnectoinString(string DBName)
		{
			string cs = string.Empty;

			System.Data.DataRow[] drs = _xsd.DBC.Select(string.Format("DBName={0}", Apq.Data.SqlClient.Common.ConvertToSqlON(DBName)));
			if (drs != null && drs.Length > 0)
			{
				cs = drs[0]["DBConnectionString"].ToString();
			}

			return cs;
		}

		/// <summary>
		/// 获取实例连接字符串(Apq管理器使用)
		/// </summary>
		/// <returns></returns>
		public static string GetInstanceConnectoinString(string SQLName)
		{
			string cs = string.Empty;

			System.Data.DataRow[] drs = _xsd.SqlInstance.Select(string.Format("SqlName={0}", Apq.Data.SqlClient.Common.ConvertToSqlON(SQLName)));
			if (drs != null && drs.Length > 0)
			{
				cs = drs[0]["DBConnectionString"].ToString();
			}

			return cs;
		}
		#endregion
	}
}
