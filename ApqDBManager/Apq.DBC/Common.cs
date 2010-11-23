using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apq.DBC
{
	public static class Common
	{
		private static Apq.Config.XmlConfig csFile = new Apq.Config.XmlConfig();

		public static Common()
		{
			csFile.Path = GlobalObject.XmlConfigChain[typeof(Apq.DBC.Common), "csFile"];
		}

		public static string GetConnectoinString()
		{
			string cs = string.Empty;


			return cs;
		}
	}
}
