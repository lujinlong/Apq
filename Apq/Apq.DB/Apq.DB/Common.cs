using System;
using System.Collections.Generic;
using System.Text;

namespace Apq.DB
{
	/// <summary>
	/// Common
	/// </summary>
	public class Common
	{
		/// <summary>
		/// 获取命名连接字符串(从配置文件读取)
		/// </summary>
		/// <param name="ItemName"></param>
		/// <returns></returns>
		public static string GetSqlConnectionString(string ItemName)
		{
			string SqlConnectionString = GlobalObject.XmlUserConfig["Apq.DB.GlobalObject", "SqlConnectionStrings", "SqlConnectionString", ItemName, "InnerText"];
			return SqlConnectionString ?? GlobalObject.XmlAsmConfig["Apq.DB.GlobalObject", "SqlConnectionStrings", "SqlConnectionString", ItemName, "InnerText"];
		}
	}
}
