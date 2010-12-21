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
		private static string _csFilePath = string.Empty;
		private static string _csStringCrypt = string.Empty;
		private static string _csString = string.Empty;

		private static System.Data.DataSet _ds = null;

		static Common()
		{
			_ds = new System.Data.DataSet();
			System.Data.DataTable dt = _ds.Tables.Add("DBC");
			dt.Columns.Add("name");
			dt.Columns.Add("value");
			dt.Columns.Add("DBName");
			dt.Columns.Add("ServerName");
			dt.Columns.Add("Mirror");
			dt.Columns.Add("UseTrusted", typeof(bool));
			dt.Columns.Add("UserId");
			dt.Columns.Add("Pwd");
			dt.Columns.Add("Option");

			fsw.Changed += new FileSystemEventHandler(fsw_Changed);
			_csFilePath = ConfigurationManager.AppSettings["Apq.DBC.csFile"] ?? @"D:\DBC\cs.res";
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
			ReadFile();
		}

		// 文件改变自动刷新内存
		private static void fsw_Changed(object sender, FileSystemEventArgs e)
		{
			ReadFile();
		}

		private static void ReadFile()
		{
			// 读取密钥
			//string desKey = GlobalObject.XmlConfigChain[typeof(Apq.DBC.Common), "DESKey"];
			//string desIV = GlobalObject.XmlConfigChain[typeof(Apq.DBC.Common), "DESIV"];
			string desKey = "~JD7(1vy";
			string desIV = "]$ik7WB)";

			string strCs = File.ReadAllText(_csFilePath, Encoding.UTF8);
			string str = Apq.Security.Cryptography.DESHelper.DecryptString(strCs, desKey, desIV);
			StringReader sr = new StringReader(str);
			_ds.Clear();
			_ds.ReadXml(sr);

			// 读取完成,计算所有连接字符串
			foreach (DataRow dr in _ds.Tables[0].Rows)
			{
				Apq.ConnectionStrings.SQLServer.SqlConnection sc = new Apq.ConnectionStrings.SQLServer.SqlConnection();
				sc.ServerName = dr["ServerName"].ToString();
				sc.DBName = dr["DBName"].ToString();
				sc.Mirror = dr["Mirror"].ToString();
				sc.UseTrusted = Apq.Convert.ChangeType<bool>(dr["UseTrusted"]);
				sc.UserId = dr["UserId"].ToString();
				sc.Pwd = dr["Pwd"].ToString();
				sc.Option = dr["Option"].ToString();
				dr["value"] = sc.GetConnectionString();
			}
		}

		/// <summary>
		/// 获取连接字符串
		/// </summary>
		/// <returns></returns>
		public static string GetConnectoinString(string Name)
		{
			string cs = string.Empty;

			System.Data.DataRow[] drs = _ds.Tables[0].Select(string.Format("name={0}", Apq.Data.SqlClient.Common.ConvertToSqlON(Name)));
			if (drs != null && drs.Length > 0)
			{
				cs = drs[0]["value"].ToString();
			}

			return cs;
		}
	}
}
