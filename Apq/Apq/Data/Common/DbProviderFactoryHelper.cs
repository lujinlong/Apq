using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apq.Data.Common
{
	/// <summary>
	/// 数据库组件工厂Helper
	/// </summary>
	public class DbProviderFactoryHelper
	{
		/// <summary>
		/// 创建或更新本地数据库组件工厂信息
		/// </summary>
		public static void CreateDbProviderLocal()
		{
			//System.Data.Common.DbProviderFactory dbProviderFactory = System.Data.Common.DbProviderFactories.GetFactory(strClient);
			System.Data.DataTable dt = System.Data.Common.DbProviderFactories.GetFactoryClasses();
			GlobalObject.XmlAsmConfig.SetTableConfig("Apq.Data.Common.DbProviderFactoryHelper", "DbProvider", dt);
			GlobalObject.XmlAsmConfig.Save();
		}
	}
}
