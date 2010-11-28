using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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

		private static System.Xml.XmlDocument xd = new System.Xml.XmlDocument();

		static Common()
		{
			fsw.Changed += new FileSystemEventHandler(fsw_Changed);
			_csFilePath = GlobalObject.RegConfigChain["ConnectionFile", "csFile"];
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
			_csStringCrypt = System.IO.File.ReadAllText(_csFilePath, Encoding.UTF8);

			// 读取密钥
			string desKey = GlobalObject.RegConfigChain["Crypt", "DESKey"];
			string desIV = GlobalObject.RegConfigChain["Crypt", "DESIV"];

			_csString = Apq.Security.Cryptography.DESHelper.DecryptString(_csStringCrypt, desKey, desIV);
			//_csString = _csStringCrypt;
			xd.LoadXml(_csString);
		}

		/// <summary>
		/// 获取连接字符串
		/// </summary>
		/// <returns></returns>
		public static string GetConnectoinString(string Name)
		{
			string cs = string.Empty;

			foreach (System.Xml.XmlNode xn in xd.DocumentElement.ChildNodes)
			{
				if (Name.Equals(xn.Attributes["name"].Value))
				{
					cs = xn.Attributes["value"].Value;
					break;
				}
			}

			return cs;
		}
	}
}
