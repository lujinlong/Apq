using System;
using System.Collections.Generic;
using System.Text;

namespace Apq.Xml
{
	/// <summary>
	/// XmlDocument
	/// </summary>
	public class XmlDocument
	{
		/// <summary>
		/// 新建
		/// </summary>
		/// <returns></returns>
		public static System.Xml.XmlDocument NewXmlDocument()
		{
			System.Xml.XmlDocument xd = new System.Xml.XmlDocument();
			xd.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<Root/>");
			return xd;
		}
	}
}
