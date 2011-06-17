using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.Common;
using Apq.Data.Common;

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

			#region 密钥
			//string desKey = GlobalObject.XmlConfigChain[typeof(Apq.DBC.Common), "DESKey"];
			//string desIV = GlobalObject.XmlConfigChain[typeof(Apq.DBC.Common), "DESIV"];

			string desKey = "pD?y/Mn^";
			string desIV = "$`5iNL8j";
			#endregion

			string strCs = File.ReadAllText(_csFilePath, Encoding.UTF8);
			string str = Apq.Security.Cryptography.DESHelper.DecryptString(strCs, desKey, desIV);
			StringReader sr = new StringReader(str);
			_xsd.Clear();
			_xsd.DBC.ReadXml(sr);

			// 读取完成,计算所有连接字符串
			/*
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
			*/
			foreach (Apq.DBC.XSD.DBCRow dr in _xsd.DBC.Rows)
			{
				dr["DBConnectionString"] = Apq.ConnectionStrings.Common.GetConnectionString(
					(DBProduct)dr.DBProduct,
					dr.IP, dr.Port, dr.UserId, dr.PwdD, dr.DBName);
			}
		}

		#region 创建连接
		/// <summary>
		/// 创建数据库连接(类型)
		/// </summary>
		public static DbConnection CreateDbConnection(string DBName, ref DbConnection DbConnection)
		{
			Apq.DBC.XSD.DBCRow dr = _xsd.DBC.FindByDBName(DBName);
			string cs = Apq.Convert.ChangeType<string>(dr["DBConnectionString"]);

			switch (dr.DBProduct)
			{
				case (int)DBProduct.MySql:
					if (!(DbConnection is MySql.Data.MySqlClient.MySqlConnection))
					{
						Apq.Data.Common.DbConnectionHelper.Close(DbConnection);
						DbConnection = new MySql.Data.MySqlClient.MySqlConnection();
					}
					break;
				case (int)DBProduct.MSSql:
				default:
					if (!(DbConnection is System.Data.SqlClient.SqlConnection))
					{
						Apq.Data.Common.DbConnectionHelper.Close(DbConnection);
						DbConnection = new System.Data.SqlClient.SqlConnection();
					}
					break;
			}

			DbConnection.ConnectionString = cs;
			return DbConnection;
		}
		#endregion
	}
}
